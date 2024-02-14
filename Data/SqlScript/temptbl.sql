 
/****** Object:  Table [dbo].[InvTmpReportDetail]    Script Date: 07/26/2014 14:40:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvTmpReportDetail](
	[InvTmpReportDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[DocumentDate] [datetime] NOT NULL,
	[SupplierID] [bigint] NOT NULL,
	[SupplierCode] [nvarchar](20) NULL,
	[SupplierName] [nvarchar](100) NULL,
	[CustomerID] [bigint] NOT NULL,
	[CustomerCode] [nvarchar](20) NULL,
	[CustomerName] [nvarchar](100) NULL,
	[ProductID] [bigint] NOT NULL,
	[ProductCode] [nvarchar](20) NULL,
	[ProductName] [nvarchar](100) NULL,
	[CategoryID] [bigint] NOT NULL,
	[CategoryCode] [nvarchar](20) NULL,
	[CategoryName] [nvarchar](100) NULL,
	[DepartmentID] [bigint] NOT NULL,
	[DepartmentCode] [nvarchar](20) NULL,
	[DepartmentName] [nvarchar](100) NULL,
	[SubCategoryID] [bigint] NOT NULL,
	[SubCategoryCode] [nvarchar](20) NULL,
	[SubCategoryName] [nvarchar](100) NULL,
	[SubCategory2ID] [bigint] NOT NULL,
	[SubCategory2Code] [nvarchar](20) NULL,
	[SubCategory2Name] [nvarchar](100) NULL,
	[BatchNo] [nvarchar](25) NULL,
	[CostPrice] [decimal](18, 2) NOT NULL,
	[SellingPrice] [decimal](18, 2) NOT NULL,
	[AverageCost] [decimal](18, 2) NOT NULL,
	[GrossProfit] [decimal](18, 2) NOT NULL,
	[Qty01] [decimal](18, 0) NOT NULL,
	[Value01] [decimal](18, 2) NOT NULL,
	[Qty02] [decimal](18, 0) NOT NULL,
	[Value02] [decimal](18, 2) NOT NULL,
	[Qty03] [decimal](18, 0) NOT NULL,
	[Value03] [decimal](18, 2) NOT NULL,
	[Qty04] [decimal](18, 0) NOT NULL,
	[Value04] [decimal](18, 2) NOT NULL,
	[Qty05] [decimal](18, 0) NOT NULL,
	[Value05] [decimal](18, 2) NOT NULL,
	[Qty06] [decimal](18, 0) NOT NULL,
	[Value06] [decimal](18, 2) NOT NULL,
	[Qty07] [decimal](18, 0) NOT NULL,
	[Value07] [decimal](18, 2) NOT NULL,
	[Qty08] [decimal](18, 0) NOT NULL,
	[Value08] [decimal](18, 2) NOT NULL,
	[Qty09] [decimal](18, 0) NOT NULL,
	[Value09] [decimal](18, 2) NOT NULL,
	[Qty10] [decimal](18, 0) NOT NULL,
	[Value10] [decimal](18, 2) NOT NULL,
	[Qty11] [decimal](18, 0) NOT NULL,
	[Value11] [decimal](18, 2) NOT NULL,
	[Qty12] [decimal](18, 0) NOT NULL,
	[Value12] [decimal](18, 2) NOT NULL,
	[Qty13] [decimal](18, 0) NOT NULL,
	[Value13] [decimal](18, 2) NOT NULL,
	[Qty14] [decimal](18, 0) NOT NULL,
	[Value14] [decimal](18, 2) NOT NULL,
	[Qty15] [decimal](18, 0) NOT NULL,
	[Value15] [decimal](18, 2) NOT NULL,
	[Qty16] [decimal](18, 0) NOT NULL,
	[Value16] [decimal](18, 2) NOT NULL,
	[Qty17] [decimal](18, 0) NOT NULL,
	[Value17] [decimal](18, 2) NOT NULL,
	[Qty18] [decimal](18, 0) NOT NULL,
	[Value18] [decimal](18, 2) NOT NULL,
	[Qty19] [decimal](18, 0) NOT NULL,
	[Value19] [decimal](18, 2) NOT NULL,
	[Qty20] [decimal](18, 0) NOT NULL,
	[Value20] [decimal](18, 2) NOT NULL,
	[Qty21] [decimal](18, 0) NOT NULL,
	[Value21] [decimal](18, 2) NOT NULL,
	[Qty22] [decimal](18, 0) NOT NULL,
	[Value22] [decimal](18, 2) NOT NULL,
	[Qty23] [decimal](18, 0) NOT NULL,
	[Value23] [decimal](18, 2) NOT NULL,
	[Qty24] [decimal](18, 0) NOT NULL,
	[Value24] [decimal](18, 2) NOT NULL,
	[Qty25] [decimal](18, 0) NOT NULL,
	[Value25] [decimal](18, 2) NOT NULL,
	[Qty26] [decimal](18, 0) NOT NULL,
	[Value26] [decimal](18, 2) NOT NULL,
	[Qty27] [decimal](18, 0) NOT NULL,
	[Value27] [decimal](18, 2) NOT NULL,
	[Qty28] [decimal](18, 0) NOT NULL,
	[Value28] [decimal](18, 2) NOT NULL,
	[Qty29] [decimal](18, 0) NOT NULL,
	[Value29] [decimal](18, 2) NOT NULL,
	[Qty30] [decimal](18, 0) NOT NULL,
	[Value30] [decimal](18, 2) NOT NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
	[DocumentNo] [nvarchar](20) NULL,
	[UnitNo] [int] NOT NULL,
	[ZNo] [bigint] NOT NULL,
	[Ext1] [nvarchar](50) NULL,
	[Ext2] [nvarchar](50) NULL,
	[Ext3] [nvarchar](50) NULL,
	[Ext4] [nvarchar](50) NULL,
	[Ext5] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.InvTmpReportDetail] PRIMARY KEY CLUSTERED 
(
	[InvTmpReportDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvTmpProductStockDetails]    Script Date: 07/26/2014 14:40:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvTmpProductStockDetails](
	[InvTmpProductStockDetailsID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[LocationID] [int] NULL,
	[GivenDate] [datetime] NULL,
	[ProductID] [bigint] NULL,
	[ProductCode] [nvarchar](20) NULL,
	[ProductName] [nvarchar](100) NULL,
	[TransactionType] [int] NULL,
	[TransactionNo] [nvarchar](20) NULL,
	[TransactionDate] [datetime] NULL,
	[CostPrice] [decimal](18, 2) NULL,
	[SellingPrice] [decimal](18, 2) NULL,
	[AverageCost] [decimal](18, 2) NULL,
	[DepartmentID] [bigint] NULL,
	[CategoryID] [bigint] NULL,
	[SubCategoryID] [bigint] NULL,
	[SubCategory2ID] [bigint] NULL,
	[SupplierID] [bigint] NULL,
	[StockQty] [decimal](18, 0) NULL,
	[UserID] [bigint] NULL,
	[UniqueID] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[GroupOfCompanyID] [int] NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DataTransfer] [int] NULL,
	[ToLocationName] [nvarchar](50) NULL,
	[BatchNo] [nvarchar](25) NULL,
	[Qty1] [decimal](18, 0) NULL,
	[Qty2] [decimal](18, 0) NULL,
	[Qty3] [decimal](18, 0) NULL,
	[Qty4] [decimal](18, 0) NULL,
	[Qty5] [decimal](18, 0) NULL,
	[CustomerID] [bigint] NULL,
	[Qty6] [decimal](18, 0) NULL,
	[Qty7] [decimal](18, 0) NULL,
	[Qty8] [decimal](18, 0) NULL,
	[Qty9] [decimal](18, 0) NULL,
	[Qty10] [decimal](18, 0) NULL,
	[GrossProfit] [decimal](18, 2) NULL,
 CONSTRAINT [PK_dbo.InvTmpProductStockDetails] PRIMARY KEY CLUSTERED 
(
	[InvTmpProductStockDetailsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_CompanyID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_CompanyID]  DEFAULT ((1)) FOR [CompanyID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_LocationID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_LocationID]  DEFAULT ((1)) FOR [LocationID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_ProductID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_TransactionType]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_TransactionType]  DEFAULT ((0)) FOR [TransactionType]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_CostPrice]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_CostPrice]  DEFAULT ((0)) FOR [CostPrice]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_SellingPrice]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_SellingPrice]  DEFAULT ((0)) FOR [SellingPrice]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_AverageCost]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_AverageCost]  DEFAULT ((0)) FOR [AverageCost]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_DepartmentID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_DepartmentID]  DEFAULT ((0)) FOR [DepartmentID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_CategoryID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_CategoryID]  DEFAULT ((0)) FOR [CategoryID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_SubCategoryID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_SubCategoryID]  DEFAULT ((0)) FOR [SubCategoryID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_SubCategory2ID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_SubCategory2ID]  DEFAULT ((0)) FOR [SubCategory2ID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_SupplierID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_SupplierID]  DEFAULT ((0)) FOR [SupplierID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_StockQty]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_StockQty]  DEFAULT ((0)) FOR [StockQty]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_UserID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_UserID]  DEFAULT ((0)) FOR [UserID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_UniqueID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_UniqueID]  DEFAULT ((0)) FOR [UniqueID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_IsDelete]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_GroupOfCompanyID]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_GroupOfCompanyID]  DEFAULT ((1)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF_InvTmpProductStockDetails_DataTransfer]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF_InvTmpProductStockDetails_DataTransfer]  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  Default [DF__InvTmpProd__Qty1__1BE81D6E]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty1__1BE81D6E]  DEFAULT ((0)) FOR [Qty1]
GO
/****** Object:  Default [DF__InvTmpProd__Qty2__1CDC41A7]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty2__1CDC41A7]  DEFAULT ((0)) FOR [Qty2]
GO
/****** Object:  Default [DF__InvTmpProd__Qty3__1DD065E0]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty3__1DD065E0]  DEFAULT ((0)) FOR [Qty3]
GO
/****** Object:  Default [DF__InvTmpProd__Qty4__1EC48A19]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty4__1EC48A19]  DEFAULT ((0)) FOR [Qty4]
GO
/****** Object:  Default [DF__InvTmpProd__Qty5__1FB8AE52]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty5__1FB8AE52]  DEFAULT ((0)) FOR [Qty5]
GO
/****** Object:  Default [DF__InvTmpPro__Custo__2C1E8537]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpPro__Custo__2C1E8537]  DEFAULT ((0)) FOR [CustomerID]
GO
/****** Object:  Default [DF__InvTmpProd__Qty6__2D12A970]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty6__2D12A970]  DEFAULT ((0)) FOR [Qty6]
GO
/****** Object:  Default [DF__InvTmpProd__Qty7__2E06CDA9]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty7__2E06CDA9]  DEFAULT ((0)) FOR [Qty7]
GO
/****** Object:  Default [DF__InvTmpProd__Qty8__2EFAF1E2]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty8__2EFAF1E2]  DEFAULT ((0)) FOR [Qty8]
GO
/****** Object:  Default [DF__InvTmpProd__Qty9__2FEF161B]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpProd__Qty9__2FEF161B]  DEFAULT ((0)) FOR [Qty9]
GO
/****** Object:  Default [DF__InvTmpPro__Qty10__30E33A54]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpPro__Qty10__30E33A54]  DEFAULT ((0)) FOR [Qty10]
GO
/****** Object:  Default [DF__InvTmpPro__Gross__31D75E8D]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpProductStockDetails] ADD  CONSTRAINT [DF__InvTmpPro__Gross__31D75E8D]  DEFAULT ((0)) FOR [GrossProfit]
GO
/****** Object:  Default [DF__InvTmpRep__UnitN__375B2DB9]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpReportDetail] ADD  DEFAULT ((0)) FOR [UnitNo]
GO
/****** Object:  Default [DF__InvTmpRepor__ZNo__384F51F2]    Script Date: 07/26/2014 14:40:27 ******/
ALTER TABLE [dbo].[InvTmpReportDetail] ADD  DEFAULT ((0)) FOR [ZNo]
GO
