  use IBS
  insert into [AccTransactionTypeDetail](TransactionTypeDetailID,[AccTransactionTypeHeaderID],[TransactionDefinition],[TransactionModeID],[DrCr],[LedgerID],[IsDelete],[GroupOfCompanyID],[CreatedUser],[CreatedDate],[ModifiedUser],[ModifiedDate],[IsDataTransfer])
  values(0,(SELECT DocumentID FROM AutoGenerateInfo where FormName = 'FrmPettyCashIOU'), 1, 0, 1,
  (select AccLedgerAccountID from AccLedgerAccount where LedgerName = 'ADVANCE PAID'),0,1,'Admin',GETDATE(),'Admin',GETDATE(),0)
  
  

  
  