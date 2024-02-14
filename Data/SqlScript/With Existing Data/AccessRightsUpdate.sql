INSERT INTO TransactionRights (DocumentID, TransactionCode, TransactionName, TransactionTypeID, IsAccess, IsPause, IsSave, IsModify, IsDelete,IsView)
SELECT DocumentID, PreFix, FormText, ModuleType, 1, 1, 1,1 , 0,1 FROM AutoGenerateInfo
WHERE documentid = '14001'

INSERT INTO UserPrivileges(UserMasterID, TransactionRightsID, FormID, TransactionTypeID, IsAccess, 
IsModify, IsPause, IsSave, IsView)
SELECT 1, TransactionRightsID, DocumentID, TransactionTypeID, 1,1,1,1,1 FROM TransactionRights
WHERE documentid = '14001'

INSERT INTO UserGroupPrivileges ([TransactionRightsID],[UserGroupID] ,[TransactionTypeID] ,[IsAccess]
,[IsPause] ,[IsSave] ,[IsModify],[IsDelete],IsView)
SELECT TransactionRightsID, 1, TransactionTypeID, 1,1,1,1,0,1 FROM TransactionRights
WHERE documentid = '14001'

--139	2	10503	10503	RptProductPriceChangeDetail	Product Price Change Detail	RPT	0	0	True	True	False	False	False	False	True		2	False	False	False
--140	5	14001	14001	RptReceiptsRegister	Receipts Register	RPT	0	1	True	True	False	False	False	False	True		2	False	False	False
--137	2	9002	9002	RptPendingPurchaseOrders	Pending Purchase Orders	RPT	0	1	True	True	False	False	False	False	True		2	False	False	False
--138	2	10012	10012	InvRptBatchStockBalance	Inventory Batch Stock Balance	RPT	0	1	True	True	False	False	False	False	True		2	False	False	False
--140	2	17014	17014	FrmBinCard	Stock Movement Details	RPT	0	0	True	True	False	False	False	False	True		2	False	False	False
--141	2	17015	17015	FrmBasketAnalysisReport	Basket Analysis Report	RPT	0	0	True	True	False	False	False	False	True		2	False	False	False
--140	3	17016	17016	FrmLogistciBinCard	Stock Movement Details	RPT	0	0	True	True	False	False	False	False	True		2	False	False	False
--140	3	2514	2514	FrmLogisticTransactionSearch	Logistic Transaction Summery	RPT	0	0	True	True	False	False	False	False	True		2	False	False	False