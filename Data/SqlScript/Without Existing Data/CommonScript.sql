USE IBS

INSERT INTO GroupOfCompany (GroupOfCompanyCode, GroupOfCompanyName, IsInventory, IsLogistic, IsManufacture, IsPayroll, IsHirePurchase, IsGeneralLedger, IsDelete, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer)
VALUES ('1', 'NOLIMIT (PVT) LTD.',1,1,0,0,0,1, 0, 'Admin', '2013-12-01', 'Admin', '2013-12-01', 0 )

USE IBS
INSERT INTO Company (GroupOfCompanyID, CompanyCode, CompanyName, CostCentreID, TaxID1, TaxID2, TaxID3, TaxID4, TaxID5, StartOfFiscalYear, CostingMethod, IsVat, IsDelete, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer)
VALUES ('1', '01', 'XXXXXXXXXXXXX (PVT) LTD.', 1, 0, 1, 1, 0, 0, 4, 'Average Cost', 1,  0, 'Admin', '2013-12-01', 'Admin', '2013-12-01', 0 )

USE IBS
INSERT INTO Location (GroupOfCompanyID, CompanyID, LocationCode, LocationName, Address1, Address2, Address3, CostCentreID, IsStockLocation, IsVat, IsDelete, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer)
VALUES ('1', '1', '001', 'HEAD OFFICE', '','','', 1, 1, 1, 0, 'Admin', '2013-12-01', 'Admin', '2013-12-01', 0 )

USE IBS
INSERT INTO SupplierGroup (SupplierGroupCode, SupplierGroupName, Remark, IsDelete, GroupOfCompanyID, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer)
VALUES ('01', 'SUPPLIER GROUP 01', '', 0, 1, 'Admin', '2013-12-01', 'Admin', '2013-12-01', 0 )

USE IBS
INSERT INTO UserGroup (UserGroupName, IsDelete)
VALUES ('Administrator', 0)

USE IBS
INSERT INTO UserMaster (CompanyID, LocationID, UserName, UserDescription, [Password], UserGroupID, IsActive, IsUserCantChangePassword, IsUserMustChangePassword, IsDelete)
SELECT 1, 1, 'Admin', 'Administrator', 'adminpwd', UserGroupID, 1, 0, 0, 0  FROM UserGroup

USE IBS
INSERT INTO TransactionRights (DocumentID, TransactionCode, TransactionName, TransactionTypeID, IsAccess, IsPause, IsSave, IsModify, IsDelete)
SELECT DocumentID, PreFix, FormText, ModuleType, 1, 1, 1,1 , 0  FROM AutoGenerateInfo

USE IBS
INSERT INTO UserPrivileges(UserMasterID, TransactionRightsID, FormID, TransactionTypeID, IsAccess, IsModify, IsPause, IsSave)
SELECT 1, TransactionRightsID, DocumentID, TransactionTypeID, 1,1,1,1 FROM TransactionRights


USE IBS
INSERT INTO UserPrivilegesLocations (UserMasterID, UserGroupID, LocationID, IsSelect)
SELECT UserMasterID, UserGroupID, 1, 1 FROM UserMaster




USE IBS
INSERT INTO DocumentNumber (DocumentID, DocumentName, CompanyID, LocationID, DocumentNo, TempDocumentNo, DocumentYear, PrefixCode, GroupOfCompanyID, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, IsDataTransfer)
SELECT DocumentID, FormText, 1, 1, 1, 1, 2013, '', 1, 'Admin', '2013-12-01', 'Admin', '2013-12-01', 0  
FROM AutoGenerateInfo WHERE Prefix <> 'RPT'



INSERT INTO ProductCodeDependancy (FormName, DependOnDepartment, DependOnCategory, DependOnSubCategory, DependOnSubCategory2)
VALUES ('InvProduct', 1, 1, 0, 0)