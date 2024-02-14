USE [ERP_TEST]
GO
/****** Object:  StoredProcedure [dbo].[spLostAndReNew]    Script Date: 05/29/2014 17:10:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spLostAndReNew]

@oldEncode VARCHAR(7),
@newEncode VARCHAR(7),
@userName VARCHAR(15),
@newCardNo VARCHAR(20),
@remarks VARCHAR(500)

AS

--By Nuwan

SET NOCOUNT ON

BEGIN

	BEGIN TRANSACTION InProc
	
		IF LEN(@oldEncode)=7 AND LEN(@newEncode)=7
		BEGIN
			IF EXISTS(SELECT cardno FROM LoyaltyCustomer WHERE CustomerCode=@newEncode)
			BEGIN
				PRINT 'INVALID NEW ENCODE............'
			END
			ELSE
			BEGIN
				IF NOT EXISTS(select cardno FROM LoyaltyCustomer WHERE CustomerCode=@oldEncode)
				BEGIN
					PRINT 'OLD ENCODE ALREADY EXISTS............'
				END
				ELSE
				BEGIN
					IF EXISTS(SELECT cardno FROM LoyaltyCustomer WHERE CustomerCode=@oldEncode GROUP BY cardno HAVING COUNT(cardno)>1)
					BEGIN
						PRINT 'OLD ENCODE EXCISTS MORE THAN ONE..........'
					END
					ELSE
					BEGIN
						UPDATE LoyaltyCustomer 
						SET CustomerCode=@newEncode,
							ModifiedUser=@userName,
							ModifiedDate=GETDATE(), 
							CardNo=@newCardNo,
							Remark=@remarks
						WHERE CustomerCode=@oldEncode
						PRINT 'ENCODE SUCCESSFULLY UPDATED...........'
					END
				END
			END
		END
		ELSE
		BEGIN
			PRINT 'INVALID ENCODES...............'
		END
	----------------
		

    IF @@TRANCOUNT > 0
	BEGIN
		COMMIT TRANSACTION InProc;
		SELECT 1 AS Result
	END
	ELSE
	BEGIN
		SELECT 0 AS Result
	END


END
