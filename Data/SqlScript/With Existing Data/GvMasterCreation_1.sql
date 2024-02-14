SELECT * INTO #RELOADGIFTVOUCHER
  FROM [Mast_Data].[dbo].[ReloadGift_2] AS sd
  WHERE doc_no NOT in (SELECT doc_no FROM [Mast_Data].[dbo].[ReloadGift_1] as dd) 
  --AND DOC_NO LIKE 'N%'
  SELECT * FROM #RELOADGIFTVOUCHER WHERE DOC_NO LIKE 'G%'

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
  FROM #RELOADGIFTVOUCHER MG
  inner join location lc on MG.loca = lc.locationcode 
  INNER JOIN [ERP].[DBO].[InvGiftVoucherBookCode] GB
  ON MG.AMOUNT = GB.GiftVoucherValue 
  AND GB.BookPrefix =  SUBSTRING(MG.[Doc_no],1,2)
    AND ([Doc_no] like 'GA%' or [Doc_no] like 'GB%' OR [Doc_no] like 'GC%' OR [Doc_no] like 'GD%' OR [Doc_no] like 'GE%')
    AND NOT(SAL = 'F' AND REC = 'T') AND (LOCA) IS NOT NULL ORDER BY MG.AMOUNT, SUBSTRING(MG.[Doc_no],1,2), SUBSTRING(Doc_no,3,LEN(Doc_no))
--2000
--1500 @2014-01-24


    SELECT * FROM location
    /*
 select * INTO #TEMP FROM [Mast_Data].[dbo].[ReloadGift]
 WHERE NOT([Doc_no] like 'GA%' OR [Doc_no] like 'GB%' OR [Doc_no] like 'GC%' OR [Doc_no] like 'GD%' OR [Doc_no] like 'GE%')
 AND (LOCA) IS NOT NULL 
*/



SELECT * INTO #TEMPnotINg 
FROM #RELOADGIFTVOUCHER MG 
WHERE NOT (MG.[Doc_no] like 'G%') AND (LOCA) IS NOT NULL
--7750
--500
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
 --7750
 --500

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
 
 SELECT * FROM dbo.InvGiftVoucherMaster	gm WHERE --gm.createddate = CONVERT(NVARCHAR,'2014-01-24',103) 
 CONVERT(DATETIME,(CONVERT(VARCHAR,gm.createddate,103)),103) =  CONVERT(VARCHAR,'2014-01-24',103)
 --gm.createddate = CONVERT(NVARCHAR,'2014-01-23',103) 
 --CONVERT(DATETIME,(CONVERT(VARCHAR,gm.createddate,103)),103)=CONVERT(VARCHAR,'2014-01-23',103)
 AND voucherstatus = 4
  
UPDATE GM SET VOUCHERSTATUS = 4
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
#RELOADGIFTVOUCHER MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'T' AND REC = 'F' AND RTRIM(ltrim(LOCA)) IS NOT NULL 
AND  CONVERT(DATETIME,(CONVERT(VARCHAR,gm.createddate,103)),103) =  CONVERT(VARCHAR,'2014-01-24',103)
--644
UPDATE GM SET VOUCHERSTATUS = 6
SELECT VOUCHERSTATUS
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
#RELOADGIFTVOUCHER MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'T' AND REC = 'T' AND (LOCA) IS NOT NULL 
AND  CONVERT(DATETIME,(CONVERT(VARCHAR,gm.createddate,103)),103) =  CONVERT(VARCHAR,'2014-01-24',103)
--70
/*
UPDATE GM SET VOUCHERSTATUS = 3
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
[Mast_Data].[dbo].[ReloadGift] MG ON 
MG.DOC_NO = GM.VOUCHERSERIAL
AND SAL = 'F' AND REC = 'F'
*/

-----------------------
-- update data with existing selling data
SELECT  gm.VoucherSerial,gm.voucherstatus , vs.vdate
--UPDATE gm SET gm.voucherstatus = 4
  FROM [ERP].[DBO].[InvGiftVoucherMaster] gm 
  INNER JOIN   [Mast_Data].[dbo].[ReloadGift_2] AS sd ON sd.Doc_no = gm.VoucherSerial
  inner JOIN [Mast_Data].[dbo].vouchersale_1 AS vs ON sd.Doc_no = vs.VOU_CODE
  
  AND (SAL = 'T' AND REC = 'F') --AND RTRIM(ltrim(sd.LOCA)) IS NOT NULL 
  AND sd.LOCA IS NOT NULL 
  AND gm.voucherstatus = 3 --gm.voucherstatus != 4 AND gm.voucherstatus != 6
  
 AND vs.iid = '001' --AND vs.vou_code = 'GA012419'
 
 
-----
SELECT gv.*, gm.ToLocationID, lc.LocationCode, rg.loca FROM [ERP].[DBO].[InvGiftVoucherMaster] GM INNER JOIN 
[Mast_Data].[dbo].GiftVoucher_1 gv ON gm.VoucherSerial = gv.vou_code
INNER JOIN [ERP].[DBO].Location lc ON gm.ToLocationID = lc.LocationID
INNER JOIN [Mast_Data].[dbo].ReloadGift_1 rg ON gm.VoucherSerial = rg.Doc_no
WHERE --gv.loca != isLoca AND 
lc.LocationCode != gv.IsLoca AND RG.LOCA = 17
--AND
 vou_code =  'NC161501'
 
 
 SELECT gm.SoldCashierID
  FROM [ERP].[DBO].[InvGiftVoucherMaster] gm 
  INNER JOIN [ERP].[DBO].Location lc ON gm.LocationID = lc.LocationID
  INNER JOIN [Mast_Data].[dbo].[VOUCHERSALE] vs ON gm.VoucherSerial = vs.vou_code WHERE iid = '001'
  
  

SELECT gm.ToLocationID, gm.* FROM [ERP].[DBO].[InvGiftVoucherMaster] gm 
WHERE --VoucherStatus = 6 
VoucherSerial = 'GB054668'


SELECT * FROM dbo.Location WHERE LocationCode = '20'
SELECT * FROM [Mast_Data].[dbo].GiftVoucher_1 WHERE vou_code = 'GB054668'
SELECT * FROM [Mast_Data].[dbo].ReloadGift_1 WHERE LOCA = '17' ; Doc_no = 'NC159101'

#'NC159100'



SELECT * FROM [Mast_Data].[dbo].ReloadGift_1 WHERE doc_no = 'GB054668'

SELECT * FROM [Mast_Data].[dbo].ReloadGift WHERE doc_no = 'GB054668'



 
 --6819

UPDATE  GM  set GM.ToLocationID = Glc.LocationID, GM.LocationID = Glc.LocationID
FROM [ERP].[DBO].[InvGiftVoucherMaster] GM 
INNER JOIN 
[Mast_Data].[dbo].ReloadGift_2 gv ON gm.VoucherSerial = gv.DOC_NO
INNER JOIN [ERP].[DBO].Location lc ON gm.ToLocationID = lc.LocationID
INNER JOIN [ERP].[DBO].Location Glc ON RTRIM(LTRIM(GV.loca))= Glc.LocationCode
WHERE GM.VoucherStatus = 3 --AND VoucherSerial ='NC162901'
AND LC.LocationCode <> RTRIM(LTRIM(GV.loca))
AND GV.loca = '05'