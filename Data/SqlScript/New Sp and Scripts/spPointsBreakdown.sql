USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[spPointsBreakdown]    Script Date: 06/05/2014 13:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPointsBreakdown] 

@DateFrom DATE,
@DateTo DATE

AS

--By Nuwan

SET NOCOUNT ON

BEGIN

	BEGIN TRANSACTION InProc
	
		DECLARE @range1From DECIMAL(18,2)
		DECLARE @range1To DECIMAL(18,2)
		DECLARE @range2From DECIMAL(18,2)
		DECLARE @range2To DECIMAL(18,2)
		DECLARE @range3From DECIMAL(18,2)
		DECLARE @range3To DECIMAL(18,2)
		DECLARE @range4From DECIMAL(18,2)
		DECLARE @range4To DECIMAL(18,2)
		DECLARE @range5From DECIMAL(18,2)
		DECLARE @range5To DECIMAL(18,2)
		DECLARE @range6From DECIMAL(18,2)
		DECLARE @range6To DECIMAL(18,2)
		DECLARE @range7From DECIMAL(18,2)
		DECLARE @range7To DECIMAL(18,2)
		
		SELECT @range1From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=1
		SELECT @range1To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=1
		
		SELECT @range2From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=2
		SELECT @range2To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=2
		
		SELECT @range3From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=3
		SELECT @range3To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=3
		
		SELECT @range4From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=4
		SELECT @range4To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=4
		
		SELECT @range5From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=5
		SELECT @range5To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=5
		
		SELECT @range6From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=6
		SELECT @range6To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=6
		
		SELECT @range7From=RangeFrom FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=7
		SELECT @range7To=RangeTo FROM dbo.TempPointsBreakdown WHERE PointsBreakdownID=7
		
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range1From AND CPoints <= @range1To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range1From AND CPoints <= @range1To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range1From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range1To AS MONEY))
		WHERE PointsBreakdownID=1
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range2From AND CPoints <= @range2To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range2From AND CPoints <= @range2To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range2From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range2To AS MONEY))
		WHERE PointsBreakdownID=2
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range3From AND CPoints <= @range3To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range3From AND CPoints <= @range3To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range3From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range3To AS MONEY))
		WHERE PointsBreakdownID=3
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range4From AND CPoints <= @range4To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range4From AND CPoints <= @range4To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range4From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range4To AS MONEY))
		WHERE PointsBreakdownID=4
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range5From AND CPoints <= @range5To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range5From AND CPoints <= @range5To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range5From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range5To AS MONEY))
		WHERE PointsBreakdownID=5
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range6From AND CPoints <= @range6To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range6From AND CPoints <= @range6To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range6From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range6To AS MONEY))
		WHERE PointsBreakdownID=6
		
		UPDATE TempPointsBreakdown 
		SET CustomerCount=(SELECT SUM(CASE WHEN cpoints >= @range7From AND CPoints <= @range7To THEN 1 ELSE 0 END)FROM dbo.LoyaltyCustomer),
		PointsTotal=(SELECT SUM(CASE WHEN cpoints >= @range7From AND CPoints <= @range7To THEN CPoints ELSE 0 END)FROM dbo.LoyaltyCustomer),
		[Range]=CONVERT(VARCHAR,CAST(@range7From AS MONEY))+' '+'-'+' '+CONVERT(VARCHAR,CAST(@range7To AS MONEY))
		WHERE PointsBreakdownID=7
		
		
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
GO
