INSERT INTO [InvProductStockMaster] ([CompanyID] ,[LocationID] ,[CostCentreID] ,[ProductID] ,[Stock] ,[CostPrice] ,[SellingPrice] ,[MinimumPrice] ,[ReOrderLevel] ,[ReOrderQuantity] ,[ReOrderPeriod] ,[IsDelete] ,[GroupOfCompanyID] ,[CreatedUser] ,[CreatedDate] ,[ModifiedUser] ,[ModifiedDate] ,[IsDataTransfer]) 
SELECT 1, 1, 1, InvProductMasterID, 10, CostPrice, SellingPrice, MinimumPrice, ReOrderLevel, ReOrderQty, ReOrderPeriod, IsDelete, GroupOfCompanyID, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer  FROM InvProductMaster   