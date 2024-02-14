INSERT INTO [ERP].[DBO].[InvGiftVoucherMaster]
  (InvGiftVoucherBookCodeID,InvGiftVoucherGroupID,CompanyID,LocationID,VoucherNo,VoucherNoSerial,VoucherPrefix,SerialLength,GiftVoucherValue,GiftVoucherPercentage,
  StartingNo,VoucherCount,[PageCount],VoucherSerial,VoucherSerialNo,VoucherType,VoucherStatus,ToLocationID,SoldLocationID,SoldCashierID,SoldReceiptNo,SoldUnitID,SoldZNo,SoldDate,RedeemedLocationID,RedeemedCashierID,RedeemedReceiptNo,RedeemedUnitID,RedeemedZNo,RedeemedDate,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer)
  SELECT GB.InvGiftVoucherBookCodeID, GB.InvGiftVoucherGroupID, 1,lc.locationid,(GB.BookPrefix + '00000001'),
  LTRIM(RTRIM(SUBSTRING(SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001'))) , PATINDEX('%[^0 ]%', SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001')))  + ' '), LEN(SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001'))) )))),
  SUBSTRING(MG.[Doc_no],1,2), LEN([Doc_no]), MG.AMOUNT, 0,
  --(SELECT COUNT(MG.[Doc_no]) FROM [Mast_Data].[dbo].[ReloadGift] WHERE [Doc_no] like 'GA%'),
  1,--CHANGE START,
  0,
  0,
  LTRIM(RTRIM(Doc_no)),LTRIM(RTRIM(SUBSTRING(SUBSTRING(Doc_no,3,LEN(Doc_no)) , PATINDEX('%[^0 ]%', SUBSTRING(Doc_no,3,LEN(Doc_no))  + ' '), LEN(SUBSTRING(Doc_no,3,LEN(Doc_no)) )))),
  1, 3 , lc.locationid, 0,0,'',0,0,GETDATE(),0,0,'',0,0,GETDATE(),0,'1','ADMIN', GETDATE(), 'ADMIN', GETDATE(), 0
  FROM [Mast_Data].[dbo].[ReloadGift] MG
  inner join location lc on MG.loca = lc.locationcode 
  INNER JOIN [ERP].[DBO].[InvGiftVoucherBookCode] GB
  ON MG.AMOUNT = GB.GiftVoucherValue 
  AND GB.BookPrefix =  SUBSTRING(MG.[Doc_no],1,2)
    AND ([Doc_no] like 'GA%' or [Doc_no] like 'GB%' OR [Doc_no] like 'GC%' OR [Doc_no] like 'GD%' OR [Doc_no] like 'GE%')
    AND NOT(SAL = 'F' AND REC = 'T') AND (LOCA) IS NOT NULL ORDER BY MG.AMOUNT, SUBSTRING(MG.[Doc_no],1,2), SUBSTRING(Doc_no,3,LEN(Doc_no))
--84416
    
    /*
 select * INTO #TEMP FROM [Mast_Data].[dbo].[ReloadGift]
 WHERE NOT([Doc_no] like 'GA%' OR [Doc_no] like 'GB%' OR [Doc_no] like 'GC%' OR [Doc_no] like 'GD%' OR [Doc_no] like 'GE%')
 AND (LOCA) IS NOT NULL 
*/

SELECT * INTO #TEMPnotINg FROM [Mast_Data].[dbo].[ReloadGift] MG 
WHERE NOT (MG.[Doc_no] like 'G%') AND (LOCA) IS NOT NULL
--809684
INSERT INTO [ERP].[DBO].[InvGiftVoucherMaster]
  (InvGiftVoucherBookCodeID,InvGiftVoucherGroupID,CompanyID,LocationID,VoucherNo,VoucherNoSerial,VoucherPrefix,SerialLength,GiftVoucherValue,GiftVoucherPercentage,
  StartingNo,VoucherCount,[PageCount],VoucherSerial,VoucherSerialNo,VoucherType,VoucherStatus,ToLocationID,SoldLocationID,SoldCashierID,SoldReceiptNo,SoldUnitID,SoldZNo,SoldDate,RedeemedLocationID,RedeemedCashierID,RedeemedReceiptNo,RedeemedUnitID,RedeemedZNo,RedeemedDate,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer)     
 
   SELECT GB.InvGiftVoucherBookCodeID, GB.InvGiftVoucherGroupID, 1 as CompanyID,lc.locationid,(GB.BookPrefix + '00000001')  AS VoucherNo,
  LTRIM(RTRIM(SUBSTRING(SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001'))) , PATINDEX('%[^0 ]%', SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001')))  + ' '), LEN(SUBSTRING((GB.BookPrefix + '00000001'),3,LEN((GB.BookPrefix + '00000001'))) )))) AS VoucherNoSerial,
  SUBSTRING(MG.[Doc_no],1,2) AS VoucherPrefix, LEN([Doc_no]) AS SerialLength, MG.AMOUNT AS GiftVoucherValue, 0 AS GiftVoucherPercentage,
  --(SELECT COUNT(MG.[Doc_no]) FROM [Mast_Data].[dbo].[ReloadGift] WHERE [Doc_no] like 'GA%'),
  1 AS StartingNo,--CHANGE START,
  0 AS VoucherCount,
  0 AS [PageCount],
  LTRIM(RTRIM(Doc_no)) AS VoucherSerial,LTRIM(RTRIM(SUBSTRING(SUBSTRING(Doc_no,3,LEN(Doc_no)) , PATINDEX('%[^0 ]%', SUBSTRING(Doc_no,3,LEN(Doc_no))  + ' '), LEN(SUBSTRING(Doc_no,3,LEN(Doc_no)) )))) AS VoucherSerialNo,
  1 AS VoucherType, 3 AS VoucherStatus , lc.locationid AS ToLocationID, 0 AS SoldLocationID,0 AS SoldCashierID,'' AS SoldReceiptNo,0 AS SoldUnitID,0 AS SoldZNo,GETDATE() AS SoldDate,0,0,'',0,0,GETDATE(),0,'1','ADMIN', GETDATE(), 'ADMIN', GETDATE(), 0
  FROM #TEMPnotINg MG
  inner join location lc on MG.loca = lc.locationcode 
  INNER JOIN [ERP].[DBO].[InvGiftVoucherBookCode] GB ON MG.AMOUNT = GB.GiftVoucherValue 
  INNER JOIN [ERP].[DBO].[InvGiftVoucherGroup] GG ON GB.InvGiftVoucherGroupID = GG.InvGiftVoucherGroupID 
  AND GG.InvGiftVoucherGroupID = 1
  AND NOT (GB.BookPrefix like 'G%')
    AND NOT(SAL = 'F' AND REC = 'T')  AND (LOCA) IS NOT NULL ORDER BY MG.AMOUNT, SUBSTRING(MG.[Doc_no],1,2),SUBSTRING(Doc_no,3,LEN(Doc_no))
 --594954   

/*
 select mg.loca,gm.locationid,lc.locationid, gm.ToLocationID , lc.locationid 
 from InvGiftVoucherMaster gm

 inner join [Mast_Data].[dbo].[ReloadGift] mg on gm.voucherserial = mg.doc_no
 inner join location lc on mg.loca = lc.locationcode 
 --and voucherserial= 'NC154587'
 
 
 UPDATE gm SET gm.locationid=lc.locationid, gm.ToLocationID = lc.locationid 
 from InvGiftVoucherMaster gm
 inner join [Mast_Data].[dbo].[ReloadGift] mg on gm.voucherserial = mg.doc_no
 inner join location lc on mg.loca = lc.locationcode 
 */ 		     
UPDATE GM SET VOUCHERSTATUS = 4
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
[Mast_Data].[dbo].[ReloadGift] MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'T' AND REC = 'F' AND (LOCA) IS NOT NULL 
--142341
UPDATE GM SET VOUCHERSTATUS = 6
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
[Mast_Data].[dbo].[ReloadGift] MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'T' AND REC = 'T' AND (LOCA) IS NOT NULL 
--99573
/*
UPDATE GM SET VOUCHERSTATUS = 3
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
[Mast_Data].[dbo].[ReloadGift] MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'F' AND REC = 'F'
*/

