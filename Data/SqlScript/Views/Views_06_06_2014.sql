USE [ERP]
GO
/****** Object:  View [dbo].[ExtendedProperty]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExtendedProperty]
AS
SELECT     proVal.ProductID, pep.ExtendedPropertyName, val.ValueData, proVal.InvProductExtendedPropertyID
FROM         dbo.InvProductExtendedPropertyValue AS proVal INNER JOIN
                      dbo.InvProductExtendedValue AS val ON proVal.InvProductExtendedValueID = val.InvProductExtendedValueID INNER JOIN
                      dbo.InvProductExtendedProperty AS pep ON proVal.InvProductExtendedPropertyID = pep.InvProductExtendedPropertyID
WHERE     (proVal.IsDelete = 0) AND (val.IsDelete = 0)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "proVal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 174
               Right = 316
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "val"
            Begin Extent = 
               Top = 37
               Left = 537
               Bottom = 207
               Right = 780
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "pep"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 366
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedProperty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedProperty'
GO
/****** Object:  View [dbo].[ProductFeatureWIseSales]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductFeatureWIseSales]
AS
SELECT     ep.ExtendedPropertyName, pv.ValueData, epv.ProductID, s.ProductCode, s.ProductName, s.DepartmentCode, s.DepartmentName, s.CategoryCode, s.CategoryName, s.SubCategoryCode, 
                      s.SubCategoryName, s.SupplierCode, s.SupplierName, s.Qty, s.NetAmount, s.DocumentDate
FROM         dbo.InvProductExtendedProperty AS ep INNER JOIN
                      dbo.InvProductExtendedValue AS pv ON ep.InvProductExtendedPropertyID = pv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductExtendedPropertyValue AS epv ON ep.InvProductExtendedPropertyID = epv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvSales AS s ON epv.ProductID = s.ProductID
WHERE     (ep.InvProductExtendedPropertyID = 1)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pv"
            Begin Extent = 
               Top = 6
               Left = 317
               Bottom = 126
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "epv"
            Begin Extent = 
               Top = 6
               Left = 596
               Bottom = 126
               Right = 863
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         Sor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'tType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseSales'
GO
/****** Object:  View [dbo].[CountryWiseProducts]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CountryWiseProducts]
AS
SELECT     ep.ExtendedPropertyName, pv.ValueData, epv.ProductID, s.ProductCode, s.ProductName, s.DepartmentCode, s.DepartmentName, s.CategoryCode, s.CategoryName, s.SubCategoryCode, 
                      s.SubCategoryName, s.SupplierCode, s.SupplierName, s.Qty, s.NetAmount, s.DocumentDate
FROM         dbo.InvProductExtendedProperty AS ep INNER JOIN
                      dbo.InvProductExtendedValue AS pv ON ep.InvProductExtendedPropertyID = pv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductExtendedPropertyValue AS epv ON ep.InvProductExtendedPropertyID = epv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvSales AS s ON epv.ProductID = s.ProductID
WHERE     (ep.InvProductExtendedPropertyID = 2)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pv"
            Begin Extent = 
               Top = 6
               Left = 317
               Bottom = 126
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "epv"
            Begin Extent = 
               Top = 6
               Left = 596
               Bottom = 126
               Right = 863
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CountryWiseProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CountryWiseProducts'
GO
/****** Object:  View [dbo].[ExtendedPropertyWiseSales]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExtendedPropertyWiseSales]
AS
SELECT     dbo.InvSales.DepartmentCode, dbo.InvSales.LocationCode, dbo.InvSales.LocationName, dbo.InvSales.LocationID, dbo.InvSales.DocumentNo, 
                      dbo.InvSales.DepartmentID, dbo.InvSales.CategoryID, dbo.InvSales.SubCategoryID, dbo.InvSales.SubCategory2ID, dbo.InvSales.DepartmentName, 
                      dbo.InvSales.CategoryCode, dbo.InvSales.CategoryName, dbo.InvSales.SubCategoryCode, dbo.InvSales.SubCategoryName, dbo.InvSales.SubCategory2Code, 
                      dbo.InvSales.SubCategory2Name, dbo.InvSales.SupplierID, dbo.InvSales.SupplierCode, dbo.InvSales.SupplierName, dbo.InvSales.Qty, dbo.InvSales.SellingPrice, 
                      dbo.ExtendedProperty.ExtendedPropertyName, dbo.ExtendedProperty.ValueData, dbo.InvSales.ProductCode, dbo.InvSales.ProductName, 
                      dbo.InvSales.DocumentDate, dbo.InvSales.DiscountAmount
FROM         dbo.InvSales INNER JOIN
                      dbo.ExtendedProperty ON dbo.InvSales.ProductID = dbo.ExtendedProperty.ProductID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "InvSales"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 198
               Right = 266
            End
            DisplayFlags = 280
            TopColumn = 18
         End
         Begin Table = "ExtendedProperty"
            Begin Extent = 
               Top = 6
               Left = 303
               Bottom = 111
               Right = 507
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyWiseSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyWiseSales'
GO
/****** Object:  View [dbo].[ExtendedPropertyProduct]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExtendedPropertyProduct]
AS
select ProductID,MAX(PRODUCTFEATURE) PRODUCTFEATURE ,MAX(COUNTRY) COUNTRY, MAX(CUT) CUT,MAX(SLEEVE) SLEEVE, MAX(HEEL) HEEL, MAX(EMBELISHMENT) EMBELISHMENT, MAX(FIT) FIT, MAX(LENGTH) LENGTH, MAX(MATERIAL) MATERIAL, MAX(TEXTURE) TEXTURE, MAX(NECK) NECK, MAX(COLLAR) COLLAR, MAX(SIZE) SIZE, MAX(COLOUR) COLOUR, MAX(PATTERNNO) PATTERNNO, MAX(BRAND) BRAND, MAX(SHOP) SHOP
from
(
  select vt.ExtendedPropertyName, vp.ValueData,vp.ProductID,
    row_number() over(partition by vt.ExtendedPropertyName
                      order by vt.InvProductExtendedPropertyID) seq
  from InvProductExtendedProperty vt
  left join ExtendedProperty vp
    on vt.InvProductExtendedPropertyID = vp.InvProductExtendedPropertyID
) d  
pivot
(
  
  max(ValueData)
  for ExtendedPropertyName in (PRODUCTFEATURE, COUNTRY, CUT, SLEEVE, HEEL, EMBELISHMENT, FIT, LENGTH, MATERIAL, TEXTURE, NECK, COLLAR, SIZE, COLOUR, PATTERNNO, BRAND, SHOP) 
) piv GROUP BY ProductID;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyProduct'
GO
/****** Object:  View [dbo].[LOYALTY_INACT]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LOYALTY_INACT]
AS
    SELECT  LocationID, COUNT(CustomerCode) AS NOC, SUM(CPOINTS) AS POINTS
    FROM    dbo.LoyaltyCustomer WHERE Active = 0
    GROUP BY LocationID
GO
/****** Object:  View [dbo].[ProductFeatureWIseStock]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductFeatureWIseStock]
AS
SELECT     ep.ExtendedPropertyName, pv.ValueData, epv.ProductID, p.ProductCode, p.ProductName, d.DepartmentCode, d.DepartmentName, c.CategoryCode, c.CategoryName, 
                      sc.SubCategoryCode, sc.SubCategoryName, su.SupplierCode, su.SupplierName, st.Stock, p.SellingPrice
FROM         dbo.InvProductExtendedProperty AS ep INNER JOIN
                      dbo.InvProductExtendedValue AS pv ON ep.InvProductExtendedPropertyID = pv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductExtendedPropertyValue AS epv ON ep.InvProductExtendedPropertyID = epv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductMaster AS p ON epv.ProductID = p.InvProductMasterID INNER JOIN
                      dbo.InvDepartment AS d ON p.DepartmentID = d.InvDepartmentID INNER JOIN
                      dbo.InvCategory AS c ON p.CategoryID = c.InvCategoryID INNER JOIN
                      dbo.InvSubCategory AS sc ON p.SubCategoryID = sc.InvSubCategoryID INNER JOIN
                      dbo.Supplier AS su ON p.SupplierID = su.SupplierID INNER JOIN
                      dbo.InvProductStockMaster AS st ON st.ProductID = p.InvProductMasterID
WHERE     (ep.InvProductExtendedPropertyID = 1) AND (st.Stock <> 0)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pv"
            Begin Extent = 
               Top = 6
               Left = 317
               Bottom = 126
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "epv"
            Begin Extent = 
               Top = 6
               Left = 596
               Bottom = 126
               Right = 863
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 126
               Left = 305
               Bottom = 246
               Right = 491
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 126
               Left = 529
               Bottom = 246
               Right = 715
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sc"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 366
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         En' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'd
         Begin Table = "su"
            Begin Extent = 
               Top = 246
               Left = 262
               Bottom = 366
               Right = 456
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "st"
            Begin Extent = 
               Top = 246
               Left = 494
               Bottom = 366
               Right = 706
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductFeatureWIseStock'
GO
/****** Object:  View [dbo].[CrmCardUsage]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CrmCardUsage] as
SELECT lt.CustomerID,l.LocationPrefixCode AS UsedLocation,lt.CustomerCode, COUNT(lt.CustomerCode) AS Usage,
lt.CardType,lt.DocumentDate
FROM dbo.InvLoyaltyTransaction lt
INNER JOIN dbo.Location l ON l.LocationID=lt.LocationID
WHERE lt.LocationID<>1
GROUP BY lt.CustomerID,l.LocationPrefixCode,lt.CustomerCode,lt.CardType,lt.DocumentDate
GO
/****** Object:  View [dbo].[CountryWiseStock]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CountryWiseStock]
AS
SELECT     ep.ExtendedPropertyName, pv.ValueData, epv.ProductID, p.ProductCode, p.ProductName, d.DepartmentCode, d.DepartmentName, c.CategoryCode, c.CategoryName, 
                      sc.SubCategoryCode, sc.SubCategoryName, su.SupplierCode, su.SupplierName, st.Stock, p.SellingPrice
FROM         dbo.InvProductExtendedProperty AS ep INNER JOIN
                      dbo.InvProductExtendedValue AS pv ON ep.InvProductExtendedPropertyID = pv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductExtendedPropertyValue AS epv ON ep.InvProductExtendedPropertyID = epv.InvProductExtendedPropertyID INNER JOIN
                      dbo.InvProductMaster AS p ON epv.ProductID = p.InvProductMasterID INNER JOIN
                      dbo.InvDepartment AS d ON p.DepartmentID = d.InvDepartmentID INNER JOIN
                      dbo.InvCategory AS c ON p.CategoryID = c.InvCategoryID INNER JOIN
                      dbo.InvSubCategory AS sc ON p.SubCategoryID = sc.InvSubCategoryID INNER JOIN
                      dbo.Supplier AS su ON p.SupplierID = su.SupplierID INNER JOIN
                      dbo.InvProductStockMaster AS st ON st.ProductID = p.InvProductMasterID
WHERE     (ep.InvProductExtendedPropertyID = 2)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ep"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pv"
            Begin Extent = 
               Top = 6
               Left = 317
               Bottom = 126
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "epv"
            Begin Extent = 
               Top = 6
               Left = 596
               Bottom = 126
               Right = 863
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 126
               Left = 305
               Bottom = 246
               Right = 491
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 126
               Left = 529
               Bottom = 246
               Right = 715
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sc"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 366
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         En' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CountryWiseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'd
         Begin Table = "su"
            Begin Extent = 
               Top = 246
               Left = 262
               Bottom = 366
               Right = 456
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "st"
            Begin Extent = 
               Top = 246
               Left = 494
               Bottom = 366
               Right = 706
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CountryWiseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CountryWiseStock'
GO
/****** Object:  View [dbo].[LOYALTY_ZERO_ABAY]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LOYALTY_ZERO_ABAY]
AS
    SELECT  LocationID, COUNT(CustomerCode) AS NOC, SUM(CPOINTS) AS POINTS
    FROM    dbo.LoyaltyCustomer
    WHERE   LoyaltyCustomerID NOT IN (
            SELECT  CustomerId
            FROM    dbo.InvLoyaltyTransaction
            WHERE   CONVERT(DATETIME, DocumentDate, 103) BETWEEN CONVERT(DATETIME, '10/04/2014', 103)
                                                   AND     CONVERT(DATETIME, '28/04/2014', 103) )
            AND ( CONVERT(DATETIME,IssuedOn, 103) < CONVERT(DATETIME, '10/04/2014', 103) )
    GROUP BY dbo.LoyaltyCustomer.LocationID
GO
/****** Object:  View [dbo].[LOYALTY_ZERO]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LOYALTY_ZERO]
AS
    SELECT  LocationID, COUNT(CustomerCode) AS NOC, SUM(CPOINTS) AS POINTS
    FROM    dbo.LoyaltyCustomer
    WHERE   LoyaltyCustomerID NOT IN (
            SELECT CustomerID  
            FROM    dbo.InvLoyaltyTransaction
            WHERE   CONVERT(DATETIME, DocumentDate, 103) BETWEEN CONVERT(DATETIME, '10/04/2014', 103)
                                                   AND     CONVERT(DATETIME, '28/04/2014', 103) )
            AND ( CONVERT(DATETIME, IssuedOn, 103) >= CONVERT(DATETIME, '10/04/2014', 103) )
    GROUP BY dbo.LoyaltyCustomer.LocationID
GO
/****** Object:  View [dbo].[LOYALTY_USE]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LOYALTY_USE]
AS
    SELECT  LocationID, COUNT(CustomerCode) AS NOC, SUM(CPOINTS) AS POINTS
    FROM    dbo.LoyaltyCustomer
    WHERE   LoyaltyCustomerID IN (
            SELECT  CustomerID
            FROM    dbo.InvLoyaltyTransaction
            WHERE   CONVERT(DATETIME, DocumentDate, 103) BETWEEN CONVERT(DATETIME, '10/04/2014', 103)
                                                   AND     CONVERT(DATETIME, '28/04/2014', 103) )
    GROUP BY dbo.LoyaltyCustomer.LocationID
GO
/****** Object:  View [dbo].[ExtendedPropertyWiseStock]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExtendedPropertyWiseStock]
AS
SELECT     expro.ExtendedPropertyName, expro.ValueData, expro.ProductID, p.ProductCode, p.ProductName, d.DepartmentCode, d.DepartmentName, c.CategoryCode, 
                      c.CategoryName, sc.SubCategoryCode, sc.SubCategoryName, sc2.SubCategory2Code, sc2.SubCategory2Name, su.SupplierCode, su.SupplierName, st.Stock, 
                      p.SellingPrice, l.LocationCode, l.LocationName
FROM         dbo.InvProductMaster AS p INNER JOIN
                      dbo.ExtendedProperty AS expro ON p.InvProductMasterID = expro.ProductID INNER JOIN
                      dbo.InvDepartment AS d ON p.DepartmentID = d.InvDepartmentID INNER JOIN
                      dbo.InvCategory AS c ON p.CategoryID = c.InvCategoryID INNER JOIN
                      dbo.InvSubCategory AS sc ON p.SubCategoryID = sc.InvSubCategoryID INNER JOIN
                      dbo.InvSubCategory2 AS sc2 ON sc2.InvSubCategory2ID = p.SubCategory2ID INNER JOIN
                      dbo.Supplier AS su ON p.SupplierID = su.SupplierID INNER JOIN
                      dbo.InvProductStockMaster AS st ON st.ProductID = p.InvProductMasterID INNER JOIN
                      dbo.Location AS l ON l.LocationID = st.LocationID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[44] 4[17] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "expro"
            Begin Extent = 
               Top = 6
               Left = 305
               Bottom = 111
               Right = 509
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 547
               Bottom = 126
               Right = 733
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 114
               Left = 407
               Bottom = 234
               Right = 593
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sc"
            Begin Extent = 
               Top = 152
               Left = 170
               Bottom = 272
               Right = 356
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sc2"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 366
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "su"
            Begin Extent = 
               Top = 120
               Left = 611
               Bottom = 240
               Right = 805
            End
            DisplayFlags = 280
            TopColumn = 0
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyWiseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  End
         Begin Table = "st"
            Begin Extent = 
               Top = 234
               Left = 262
               Bottom = 354
               Right = 474
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 240
               Left = 512
               Bottom = 359
               Right = 699
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyWiseStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ExtendedPropertyWiseStock'
GO
/****** Object:  View [dbo].[CrmCardUsageSummery]    Script Date: 06/06/2014 15:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CrmCardUsageSummery] as
SELECT l.LocationPrefixCode AS IssuedLocation ,cu.Usage, cu.UsedLocation,cu.DocumentDate,
cu.CustomerCode,cu.CustomerID,cu.CardType
FROM dbo.LoyaltyCustomer lc
INNER JOIN dbo.Location l ON lc.LocationID = l.LocationID
INNER JOIN dbo.CrmCardUsage cu ON lc.LoyaltyCustomerID=cu.CustomerID
WHERE lc.LocationID<>1
GO
