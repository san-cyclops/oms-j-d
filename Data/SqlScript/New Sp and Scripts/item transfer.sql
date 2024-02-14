/****** Object:  StoredProcedure [dbo].[spBinCard]    Script Date: 06/06/2014 15:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC spBinCard 1,1,'07-25-2014','07-28-2014',2,'0000000000001','0000000000040',1,0,'admin'
ALTER procedure [dbo].[spBinCard]

--By C.S.Malluwawadu
@CompanyId int,
@SelectedLocationID int,
@FromDate DateTime,
@ToDate DateTime,
@TypeId int, -- 1 - Product, 2 - Department, 3 - Category, 4 - SubCategory, 5 - Supplier
@FromCode varchar(20),
@ToCode varchar(20),
@UserId bigint,
@UniqueId bigint,
@CreatedUser varchar(30)


AS

DECLARE @FromId bigint,
		@ToId bigint


BEGIN
	
	
BEGIN TRANSACTION InProc

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].#tempBinCard') AND type in (N'U'))
		DROP table #tempBinCard
		
	CREATE TABLE #tempBinCard(
	[LocationID] [bigint] NOT NULL,
	[ToLocationName] [nvarchar](25) NULL,
	[ProductID] [bigint] NOT NULL,
	[BatchNo] [nvarchar](25) NULL,
	[Qty] [decimal](18, 0) NOT NULL,
	[CostPrice] [decimal](18, 2),
	[SellingPrice] [decimal](18, 2),
	[TransactionType] [int] NOT NULL,
	[TransactionNo] [nvarchar](20) NULL,
	[TransactionDate] [DateTime] )
	
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].#tempSummary') AND type in (N'U'))
		DROP table #tempSummary
		
	CREATE TABLE #tempSummary(
	[ProductID] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[Qty] [decimal](18, 0) NOT NULL)
	
	
	DELETE FROM InvTmpProductStockDetails WHERE UserId = @UserId
	
	SELECT 	@FromId = InvProductMasterID FROM InvProductMaster WHERE ProductCode = @FromCode
	
	SELECT 	@ToId = InvProductMasterID FROM InvProductMaster WHERE ProductCode = @ToCode
	
	
	IF (@TypeId = 1)  -- Bin Card Report
	BEGIN
	
		---Opening Stock
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT sd.LocationID, sd.ProductID, sd.BatchNo, sd.OrderQty, 1, sh.DocumentNo, sh.DocumentDate, sd.CostPrice, sd.SellingPrice
		FROM OpeningStockDetail sd INNER JOIN OpeningStockHeader sh ON sd.OpeningStockHeaderID = sh.OpeningStockHeaderID AND sd.DocumentID = sh.DocumentID 
		AND sd.LocationID = sh.LocationID AND sd.DocumentStatus = sh.DocumentStatus
		WHERE sd.DocumentStatus = 1 AND sh.OpeningStockType = 1 AND sd.DocumentID = 503 AND sd.LocationID = @SelectedLocationID AND CAST(sd.DocumentDate as date) Between @FromDate AND @ToDate
		AND sd.ProductID Between @FromId And @ToId Order By sh.DocumentDate

		--GRN 
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT pd.LocationID, pd.ProductID, pd.BatchNo, (pd.Qty + pd.FreeQty) AS QTY,	
		2, ph.DocumentNo, ph.DocumentDate, pd.CostPrice, pd.SellingPrice FROM InvPurchaseDetail pd INNER JOIN InvPurchaseHeader ph ON pd.InvPurchaseHeaderID = ph.InvPurchaseHeaderID
		AND pd.LocationID = ph.LocationID AND pd.DocumentID = ph.DocumentID WHERE pd.DocumentStatus = 1 AND pd.DocumentID = 1502 AND pd.LocationID = @SelectedLocationID AND CAST(pd.CreatedDate as date) Between @FromDate AND @ToDate
		AND pd.ProductID Between @FromId And @ToId Order By ph.DocumentDate
		
		--Purchase Returns
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT pd.LocationID, pd.ProductID, pd.BatchNo, ((pd.Qty + pd.FreeQty)*-1) AS QTY,	
		3, ph.DocumentNo, ph.DocumentDate, pd.CostPrice, pd.SellingPrice FROM InvPurchaseDetail pd INNER JOIN InvPurchaseHeader ph ON pd.InvPurchaseHeaderID = ph.InvPurchaseHeaderID
		AND pd.LocationID = ph.LocationID AND pd.DocumentID = ph.DocumentID WHERE pd.DocumentStatus = 1 AND pd.DocumentID = 1503 AND pd.LocationID = @SelectedLocationID AND CAST(pd.CreatedDate as date) Between @FromDate AND @ToDate
		AND pd.ProductID Between @FromId And @ToId Order By ph.DocumentDate

		--TOG IN
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice, ToLocationName)
		SELECT td.ToLocationID, td.ProductID, td.BatchNo, td.Qty, 4, th.DocumentNo, th.DocumentDate, td.CostPrice, td.SellingPrice, l.LocationName
		FROM InvTransferNoteDetail td INNER JOIN InvTransferNoteHeader th ON
		th.InvTransferNoteHeaderID = td.TransferNoteHeaderID AND td.LocationID = th.LocationID AND td.DocumentID = th.DocumentID
		LEFT JOIN Location l ON l.LocationID = td.LocationID WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504	AND td.ToLocationID = @SelectedLocationID AND CAST(td.DocumentDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId Order By th.DocumentDate
									
		--TOG OUT
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice, ToLocationName)
		SELECT td.LocationID, td.ProductID, td.BatchNo, (td.Qty *-1), 5, th.DocumentNo, th.DocumentDate, td.CostPrice, td.SellingPrice, l.LocationName
		FROM InvTransferNoteDetail td INNER JOIN InvTransferNoteHeader th ON
		th.InvTransferNoteHeaderID = td.TransferNoteHeaderID AND td.LocationID = th.LocationID AND td.DocumentID = th.DocumentID
		LEFT JOIN Location l ON l.LocationID = td.ToLocationID WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504 AND td.LocationID = @SelectedLocationID AND CAST(td.DocumentDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId Order By th.DocumentDate
								
		--Stock Adjustment (ADD)
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT ad.LocationID, ad.ProductID, ad.BatchNo, ad.ExcessQty, 6, sh.DocumentNo, sh.DocumentDate, ad.CostPrice, ad.SellingPrice
		FROM InvStockAdjustmentDetail ad INNER JOIN InvStockAdjustmentHeader sh ON
		sh.InvStockAdjustmentHeaderID = ad.InvStockAdjustmentHeaderID AND ad.LocationID = sh.LocationID AND ad.DocumentID = sh.DocumentID WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 1 AND ad.LocationID = @SelectedLocationID  AND CAST(ad.DocumentDate as date) Between @FromDate AND @ToDate
		AND ad.ProductID Between @FromId And @ToId Order By sh.DocumentDate
								
		--Stock Adjustment (REDUSE)
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT ad.LocationID, ad.ProductID, ad.BatchNo, (ad.ExcessQty*-1), 7, sh.DocumentNo, sh.DocumentDate, ad.CostPrice, ad.SellingPrice
		FROM InvStockAdjustmentDetail ad INNER JOIN InvStockAdjustmentHeader sh ON
		sh.InvStockAdjustmentHeaderID = ad.InvStockAdjustmentHeaderID AND ad.LocationID = sh.LocationID AND ad.DocumentID = sh.DocumentID WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 2 AND ad.LocationID = @SelectedLocationID  AND CAST(ad.DocumentDate as date) Between @FromDate AND @ToDate
		AND ad.ProductID Between @FromId And @ToId Order By sh.DocumentDate
								
		--Sales & Returns	
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate, CostPrice, SellingPrice)
		SELECT td.LocationID, td.ProductID, td.BatchNo, SUM(CASE DocumentID WHEN 1 THEN  -Qty WHEN 3 THEN  -Qty WHEN 2 THEN  Qty WHEN 4 THEN  Qty ELSE 0 END),8, td.Receipt, td.RecDate, td.Cost, td.Price
		FROM TransactionDet td WHERE  [Status] = 1  AND TransStatus = 1 AND td.LocationID = @SelectedLocationID AND (DocumentID IN(1,2,3,4)) AND CAST(td.RecDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId
		GROUP BY td.LocationID, td.ProductID, td.BatchNo, td.RecDate, td.Cost, td.Price, td.Receipt Order By td.RecDate			
		
								
		INSERT INTO InvTmpProductStockDetails (GroupOfCompanyID,CompanyID, LocationID, ProductCode, ProductName, TransactionDate, TransactionType, TransactionNo, 
				ProductID, StockQty, UserID, IsDelete, GivenDate, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, DepartmentID, CategoryID, SubCategoryID, SubCategory2ID, SupplierID, DataTransfer, BatchNo, CostPrice, SellingPrice, toLocationName)					
		SELECT 1, @CompanyId, tb.LocationID, pm.ProductCode, pm.ProductName, tb.TransactionDate, tb.TransactionType, tb.TransactionNo,
				tb.ProductID, tb.Qty, @UserId, 0, GETDATE(), @CreatedUser, GETDATE(),  @CreatedUser, GETDATE(),0,0,0,0,0, 0, tb.BatchNo, tb.CostPrice, tb.SellingPrice, tb.ToLocationName FROM #tempBinCard tb
				INNER JOIN InvProductMaster pm ON tb.ProductID = pm.InvProductMasterID  
			
							
		---Opening Stock
			INSERT INTO #tempSummary (ProductID, [Type], Qty)
			SELECT sd.ProductID, 1, SUM(sd.OrderQty)
			FROM OpeningStockDetail sd INNER JOIN OpeningStockHeader sh ON sd.OpeningStockHeaderID = sh.OpeningStockHeaderID AND sd.DocumentID = sh.DocumentID 
			AND sd.LocationID = sh.LocationID AND sd.DocumentStatus = sh.DocumentStatus
			WHERE sd.DocumentStatus = 1 AND sh.OpeningStockType = 1 AND sd.DocumentID = 503 AND sd.LocationID = @SelectedLocationID AND (Cast(sd.DocumentDate As Date) < @FromDate)
			AND sd.ProductID Between @FromId And @ToId 
			GROUP BY sd.ProductID


		--GRN & Purchase Returns
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT pd.ProductID, 1, SUM(CASE pd.DocumentID WHEN 1502 THEN  (pd.Qty + pd.FreeQty) WHEN 1503 THEN  -(pd.Qty + pd.FreeQty) ELSE 0 END) AS QTY	
			FROM InvPurchaseDetail pd WHERE pd.DocumentStatus = 1 AND (pd.DocumentID IN (1502, 1503)) AND pd.LocationID = @SelectedLocationID AND (Cast(pd.CreatedDate As Date) < @FromDate)
			AND pd.ProductID Between @FromId And @ToId
			GROUP BY pd.ProductID


		--TOG IN
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, SUM(td.Qty)
			FROM InvTransferNoteDetail td WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504	AND td.ToLocationID = @SelectedLocationID AND (Cast(td.DocumentDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.ToLocationID, td.ProductID, td.BatchNo
				
				
		--TOG OUT
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, (SUM(td.Qty) *-1)
			FROM InvTransferNoteDetail td WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504 AND td.LocationID = @SelectedLocationID AND (Cast(td.DocumentDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.LocationID, td.ProductID, td.BatchNo	
			
		--Stock Adjustment (ADD)
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT ad.ProductID, 1, SUM(ad.ExcessQty)
			FROM InvStockAdjustmentDetail ad WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 1 AND ad.LocationID = @SelectedLocationID AND (Cast(ad.DocumentDate As Date) < @FromDate)
			AND ad.ProductID Between @FromId And @ToId
			GROUP BY ad.LocationID, ad.ProductID, ad.BatchNo
			
		--Stock Adjustment (REDUSE)
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT ad.ProductID, 1, (SUM(ad.ShortageQty)*-1)
			FROM InvStockAdjustmentDetail ad WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 2 AND ad.LocationID = @SelectedLocationID AND (Cast(ad.DocumentDate As Date) < @FromDate)
			AND ad.ProductID Between @FromId And @ToId
			GROUP BY ad.LocationID, ad.ProductID, ad.BatchNo
			
		--Sales & Returns	
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, SUM(CASE DocumentID WHEN 1 THEN  -Qty WHEN 3 THEN  -Qty WHEN 2 THEN  Qty WHEN 4 THEN  Qty ELSE 0 END)
			FROM TransactionDet td WHERE  [Status] = 1  AND TransStatus = 1 AND td.LocationID = @SelectedLocationID AND (DocumentID IN(1,2,3,4)) AND (Cast(td.RecDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.LocationID, td.ProductID, td.BatchNo
			
			INSERT INTO InvTmpProductStockDetails (GroupOfCompanyID,CompanyID, LocationID, ProductCode, ProductName, TransactionDate, TransactionType, TransactionNo, 
			ProductID, StockQty, UserID, IsDelete, GivenDate, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, DepartmentID, CategoryID, SubCategoryID, SubCategory2ID, SupplierID, DataTransfer)					
			SELECT 1, @CompanyId, @SelectedLocationID, pm.ProductCode, pm.ProductName, (@FromDate-1), 0, '',
			tb.ProductID, SUM(tb.Qty), @UserId, 0, GETDATE(), @CreatedUser, GETDATE(),  @CreatedUser, GETDATE(),0,0,0,0,0, 0 FROM #tempSummary tb
			INNER JOIN InvProductMaster pm ON tb.ProductID = pm.InvProductMasterID WHERE tb.Type = 1
			GROUP BY pm.ProductCode, pm.ProductName, tb.ProductID
			
	
	END
	
	ELSE IF (@TypeId = 2)  -- Stock Movement Report
	BEGIN
	
	---Opening Stock
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT sd.LocationID, sd.ProductID, sd.BatchNo, sd.OrderQty, 1, sh.DocumentNo, sh.DocumentDate
		FROM OpeningStockDetail sd INNER JOIN OpeningStockHeader sh ON sd.OpeningStockHeaderID = sh.OpeningStockHeaderID AND sd.DocumentID = sh.DocumentID 
		AND sd.LocationID = sh.LocationID AND sd.DocumentStatus = sh.DocumentStatus
		WHERE sd.DocumentStatus = 1 AND sh.OpeningStockType = 1 AND sd.DocumentID = 503 AND sd.LocationID = @SelectedLocationID AND CAST(sd.DocumentDate as date) Between @FromDate AND @ToDate
		AND sd.ProductID Between @FromId And @ToId Order By sh.DocumentDate

		--GRN 
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT pd.LocationID, pd.ProductID, pd.BatchNo, (pd.Qty + pd.FreeQty) AS QTY,	
		2, ph.DocumentNo, ph.DocumentDate FROM InvPurchaseDetail pd INNER JOIN InvPurchaseHeader ph ON pd.InvPurchaseHeaderID = ph.InvPurchaseHeaderID
		AND pd.LocationID = ph.LocationID AND pd.DocumentID = ph.DocumentID WHERE pd.DocumentStatus = 1 AND pd.DocumentID = 1502 AND pd.LocationID = @SelectedLocationID AND CAST(pd.CreatedDate as date) Between @FromDate AND @ToDate
		AND pd.ProductID Between @FromId And @ToId Order By ph.DocumentDate
		
		--Purchase Returns
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT pd.LocationID, pd.ProductID, pd.BatchNo, (pd.Qty + pd.FreeQty) AS QTY,	
		3, ph.DocumentNo, ph.DocumentDate FROM InvPurchaseDetail pd INNER JOIN InvPurchaseHeader ph ON pd.InvPurchaseHeaderID = ph.InvPurchaseHeaderID
		AND pd.LocationID = ph.LocationID AND pd.DocumentID = ph.DocumentID WHERE pd.DocumentStatus = 1 AND pd.DocumentID = 1503 AND pd.LocationID = @SelectedLocationID AND CAST(pd.CreatedDate as date) Between @FromDate AND @ToDate
		AND pd.ProductID Between @FromId And @ToId Order By ph.DocumentDate

		--TOG IN
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT td.ToLocationID, td.ProductID, td.BatchNo, td.Qty, 4, th.DocumentNo, th.DocumentDate
		FROM InvTransferNoteDetail td INNER JOIN InvTransferNoteHeader th ON
		th.InvTransferNoteHeaderID = td.TransferNoteHeaderID AND td.LocationID = th.LocationID AND td.DocumentID = th.DocumentID WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504	AND td.ToLocationID = @SelectedLocationID AND CAST(td.DocumentDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId Order By th.DocumentDate
									
		--TOG OUT
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT td.LocationID, td.ProductID, td.BatchNo, td.Qty, 5, th.DocumentNo, th.DocumentDate
		FROM InvTransferNoteDetail td INNER JOIN InvTransferNoteHeader th ON
		th.InvTransferNoteHeaderID = td.TransferNoteHeaderID AND td.LocationID = th.LocationID AND td.DocumentID = th.DocumentID WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504 AND td.LocationID = @SelectedLocationID AND CAST(td.DocumentDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId Order By th.DocumentDate
								
		--Stock Adjustment (ADD)
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT ad.LocationID, ad.ProductID, ad.BatchNo, ad.ExcessQty, 6, sh.DocumentNo, sh.DocumentDate
		FROM InvStockAdjustmentDetail ad INNER JOIN InvStockAdjustmentHeader sh ON
		sh.InvStockAdjustmentHeaderID = ad.InvStockAdjustmentHeaderID AND ad.LocationID = sh.LocationID AND ad.DocumentID = sh.DocumentID WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 1 AND ad.LocationID = @SelectedLocationID  AND CAST(ad.DocumentDate as date) Between @FromDate AND @ToDate
		AND ad.ProductID Between @FromId And @ToId Order By sh.DocumentDate
								
		--Stock Adjustment (REDUSE)
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT ad.LocationID, ad.ProductID, ad.BatchNo, ad.ExcessQty, 7, sh.DocumentNo, sh.DocumentDate
		FROM InvStockAdjustmentDetail ad INNER JOIN InvStockAdjustmentHeader sh ON
		sh.InvStockAdjustmentHeaderID = ad.InvStockAdjustmentHeaderID AND ad.LocationID = sh.LocationID AND ad.DocumentID = sh.DocumentID WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 2 AND ad.LocationID = @SelectedLocationID  AND CAST(ad.DocumentDate as date) Between @FromDate AND @ToDate
		AND ad.ProductID Between @FromId And @ToId Order By sh.DocumentDate
								
		--Sales & Returns	
		INSERT INTO #tempBinCard (LocationID, ProductID, BatchNo, Qty, TransactionType, TransactionNo, TransactionDate)
		SELECT td.LocationID, td.ProductID, td.BatchNo, SUM(CASE DocumentID WHEN 1 THEN  -Qty WHEN 3 THEN  -Qty WHEN 2 THEN  Qty WHEN 4 THEN  Qty ELSE 0 END),8,'Sales',td.RecDate
		FROM TransactionDet td WHERE  [Status] = 1  AND TransStatus = 1 AND td.LocationID = @SelectedLocationID AND (DocumentID IN(1,2,3,4)) AND CAST(td.RecDate as date) Between @FromDate AND @ToDate
		AND td.ProductID Between @FromId And @ToId
		GROUP BY td.LocationID, td.ProductID, td.BatchNo, td.RecDate Order By td.RecDate	
		
								
		INSERT INTO InvTmpProductStockDetails (GroupOfCompanyID,CompanyID, LocationID, ProductCode, ProductName, TransactionDate, TransactionType, TransactionNo, 
				ProductID, StockQty, UserID, IsDelete, GivenDate, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, DepartmentID, CategoryID, SubCategoryID, SubCategory2ID, SupplierID, DataTransfer, BatchNo, CostPrice, SellingPrice)					
		SELECT 1, @CompanyId, @SelectedLocationID, pm.ProductCode, pm.ProductName, tb.TransactionDate, tb.TransactionType, tb.TransactionNo,
				tb.ProductID, tb.Qty, @UserId, 0, GETDATE(), @CreatedUser, GETDATE(),  @CreatedUser, GETDATE(),0,0,0,0,0, 0, tb.BatchNo, pm.CostPrice, pm.SellingPrice FROM #tempBinCard tb
				INNER JOIN InvProductMaster pm ON tb.ProductID = pm.InvProductMasterID  
					
							
		---Opening Stock
			INSERT INTO #tempSummary (ProductID, [Type], Qty)
			SELECT sd.ProductID, 1, SUM(sd.OrderQty)
			FROM OpeningStockDetail sd INNER JOIN OpeningStockHeader sh ON sd.OpeningStockHeaderID = sh.OpeningStockHeaderID AND sd.DocumentID = sh.DocumentID 
			AND sd.LocationID = sh.LocationID AND sd.DocumentStatus = sh.DocumentStatus
			WHERE sd.DocumentStatus = 1 AND sh.OpeningStockType = 1 AND sd.DocumentID = 503 AND sd.LocationID = @SelectedLocationID AND (Cast(sd.DocumentDate As Date) < @FromDate)
			AND sd.ProductID Between @FromId And @ToId 
			GROUP BY sd.ProductID


		--GRN & Purchase Returns
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT pd.ProductID, 1, SUM(CASE pd.DocumentID WHEN 1502 THEN  (pd.Qty + pd.FreeQty) WHEN 1503 THEN  -(pd.Qty + pd.FreeQty) ELSE 0 END) AS QTY	
			FROM InvPurchaseDetail pd WHERE pd.DocumentStatus = 1 AND (pd.DocumentID IN (1502, 1503)) AND pd.LocationID = @SelectedLocationID AND (Cast(pd.CreatedDate As Date) < @FromDate)
			AND pd.ProductID Between @FromId And @ToId
			GROUP BY pd.ProductID


		--TOG IN
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, SUM(td.Qty)
			FROM InvTransferNoteDetail td WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504	AND td.ToLocationID = @SelectedLocationID AND (Cast(td.DocumentDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.ToLocationID, td.ProductID, td.BatchNo
				
				
		--TOG OUT
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, (SUM(td.Qty) *-1)
			FROM InvTransferNoteDetail td WHERE td.DocumentStatus = 1 AND td.DocumentID = 1504 AND td.LocationID = @SelectedLocationID AND (Cast(td.DocumentDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.LocationID, td.ProductID, td.BatchNo	
			
			
		--Stock Adjustment (ADD)
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT ad.ProductID, 1, SUM(ad.ExcessQty)
			FROM InvStockAdjustmentDetail ad WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 1 AND ad.LocationID = @SelectedLocationID AND (Cast(ad.DocumentDate As Date) < @FromDate)
			AND ad.ProductID Between @FromId And @ToId
			GROUP BY ad.LocationID, ad.ProductID, ad.BatchNo
			
			
		--Stock Adjustment (REDUSE)
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT ad.ProductID, 1, (SUM(ad.ShortageQty)*-1)
			FROM InvStockAdjustmentDetail ad WHERE ad.DocumentStatus = 1 AND ad.DocumentID = 1505 AND ad.StockAdjustmentMode = 2 AND ad.LocationID = @SelectedLocationID AND (Cast(ad.DocumentDate As Date) < @FromDate)
			AND ad.ProductID Between @FromId And @ToId
			GROUP BY ad.LocationID, ad.ProductID, ad.BatchNo
			
			
		--Sales & Returns	
			INSERT INTO #tempSummary (ProductID, [Type],Qty)
			SELECT td.ProductID, 1, SUM(CASE DocumentID WHEN 1 THEN  -Qty WHEN 3 THEN  -Qty WHEN 2 THEN  Qty WHEN 4 THEN  Qty ELSE 0 END)
			FROM TransactionDet td WHERE  [Status] = 1  AND TransStatus = 1 AND td.LocationID = @SelectedLocationID AND (DocumentID IN(1,2,3,4)) AND (Cast(td.RecDate As Date) < @FromDate)
			AND td.ProductID Between @FromId And @ToId
			GROUP BY td.LocationID, td.ProductID, td.BatchNo
			
			
			INSERT INTO InvTmpProductStockDetails (GroupOfCompanyID,CompanyID, LocationID, ProductCode, ProductName, TransactionDate, TransactionType, TransactionNo, 
			ProductID, StockQty, UserID, IsDelete, GivenDate, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, DepartmentID, CategoryID, SubCategoryID, SubCategory2ID, SupplierID, DataTransfer, CostPrice, SellingPrice)					
			SELECT 1, @CompanyId, @SelectedLocationID, pm.ProductCode, pm.ProductName, (@FromDate-1), 0, '',
			tb.ProductID, tb.Qty, @UserId, 0, GETDATE(), @CreatedUser, GETDATE(),  @CreatedUser, GETDATE(),0,0,0,0,0, 0,0,0 FROM #tempSummary tb
			INNER JOIN InvProductMaster pm ON tb.ProductID = pm.InvProductMasterID WHERE tb.Type = 1
			
			
	
	END

	
	
	
	if @@TRANCOUNT > 0
		BEGIN
			COMMIT TRANSACTION InProc;
			SELECT 1 AS Result
		END
	ELSE
		BEGIN
			SELECT 0 AS Result
		END


END
GO