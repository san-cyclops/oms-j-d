

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (1, 18001, 18001, 'ShowCostPrice', 'Show Cost Price', 'RPT',0,0,0,0,0,0,0,0,0,'',1,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (1, 18002, 18002, 'ShowSellingPrice', 'Show Selling Price', 'RPT',0,0,0,0,0,0,0,0,0,'',1,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (3, 2514, 2514, 'FrmLogisticTransactionSearch', 'Logistic Transaction Panel', '',0,0,1,1,0,0,0,0,1,'',2,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (2, 10013, 10013, 'RptExtendedPropertyStockBalance', 'Extended Property Stock Balance Report', 'RPT',0,0,1,1,0,0,0,0,1,'',2,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (2, 10015, 10015, 'FrmGivenDateStock', 'Given Date Stock', 'RPT',0,0,1,1,0,0,0,0,1,'',2,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (2, 10016, 10016, 'FrmBinCard', 'Stock Movement Details', 'RPT',0,0,1,1,0,0,0,0,1,'',2,0,0,0)

Insert Into AutoGenerateInfo (ModuleType, DocumentID, FormId, FormName, FormText, Prefix, CodeLength, Suffix, AutoGenerete, AutoClear, IsDepend, IsDependCode, IsSupplierProduct, IsOverWriteQty, isLocationCode, ReportPrefix, ReportType, PoIsMandatory, IsDispatchRecall, IsBackDated)
Values (1, 24, 24, 'FrmManualPasswordChange', 'Change Password', '',0,0,1,1,0,0,0,0,1,'',1,0,0,0)


Drop table UserPrivilegesLocations
Drop table UserPrivileges
Drop table UserMaster
Drop table UserGroupPrivileges
Drop table UserGroup

truncate table TransactionRights


GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 05/20/2014 15:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[UserName] [nvarchar](15) NULL,
	[UserDescription] [nvarchar](100) NULL,
	[Password] [nvarchar](15) NULL,
	[UserGroupID] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsUserCantChangePassword] [bit] NOT NULL,
	[IsUserMustChangePassword] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[EmployeeCode] [nvarchar](15) NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserMasterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 05/20/2014 15:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserGroupID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserGroupName] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserGroup] PRIMARY KEY CLUSTERED 
(
	[UserGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPrivilegesLocations]    Script Date: 05/20/2014 15:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrivilegesLocations](
	[UserPrivilegesLocationsID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserMasterID] [bigint] NOT NULL,
	[UserGroupID] [bigint] NOT NULL,
	[LocationID] [int] NOT NULL,
	[IsSelect] [bit] NOT NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserPrivilegesLocations] PRIMARY KEY CLUSTERED 
(
	[UserPrivilegesLocationsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPrivileges]    Script Date: 05/20/2014 15:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrivileges](
	[UserPrivilegesID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserMasterID] [bigint] NOT NULL,
	[TransactionRightsID] [bigint] NOT NULL,
	[FormID] [bigint] NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
	[IsAccess] [bit] NOT NULL,
	[IsPause] [bit] NOT NULL,
	[IsSave] [bit] NOT NULL,
	[IsModify] [bit] NOT NULL,
	[IsView] [bit] NOT NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserPrivileges] PRIMARY KEY CLUSTERED 
(
	[UserPrivilegesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroupPrivileges]    Script Date: 05/20/2014 15:16:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupPrivileges](
	[UserGroupPrivilegesID] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionRightsID] [bigint] NOT NULL,
	[UserGroupID] [bigint] NOT NULL,
	[TransactionTypeID] [int] NOT NULL,
	[IsAccess] [bit] NOT NULL,
	[IsPause] [bit] NOT NULL,
	[IsSave] [bit] NOT NULL,
	[IsModify] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsView] [bit] NOT NULL,
	[GroupOfCompanyID] [int] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[DataTransfer] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserGroupPrivileges] PRIMARY KEY CLUSTERED 
(
	[UserGroupPrivilegesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__UserGroup__Group__4DD47EBD]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroup] ADD  DEFAULT ((0)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF__UserGroup__Creat__4EC8A2F6]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroup] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__UserGroup__Modif__4FBCC72F]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroup] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifiedDate]
GO
/****** Object:  Default [DF__UserGroup__DataT__50B0EB68]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroup] ADD  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  Default [DF__UserGroup__IsVie__54817C4C]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges] ADD  DEFAULT ((0)) FOR [IsView]
GO
/****** Object:  Default [DF__UserGroup__Group__5575A085]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges] ADD  DEFAULT ((0)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF__UserGroup__Creat__5669C4BE]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__UserGroup__Modif__575DE8F7]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifiedDate]
GO
/****** Object:  Default [DF__UserGroup__DataT__58520D30]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges] ADD  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  Default [DF__UserMaste__Group__5B2E79DB]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserMaster] ADD  DEFAULT ((0)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF__UserMaste__Creat__5C229E14]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserMaster] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__UserMaste__Modif__5D16C24D]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserMaster] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifiedDate]
GO
/****** Object:  Default [DF__UserMaste__DataT__5E0AE686]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserMaster] ADD  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  Default [DF__UserPrivi__IsVie__61DB776A]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges] ADD  DEFAULT ((0)) FOR [IsView]
GO
/****** Object:  Default [DF__UserPrivi__Group__62CF9BA3]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges] ADD  DEFAULT ((0)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF__UserPrivi__Creat__63C3BFDC]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__UserPrivi__Modif__64B7E415]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifiedDate]
GO
/****** Object:  Default [DF__UserPrivi__DataT__65AC084E]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges] ADD  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  Default [DF__UserPrivi__Group__697C9932]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivilegesLocations] ADD  DEFAULT ((0)) FOR [GroupOfCompanyID]
GO
/****** Object:  Default [DF__UserPrivi__Creat__6A70BD6B]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivilegesLocations] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__UserPrivi__Modif__6B64E1A4]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivilegesLocations] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ModifiedDate]
GO
/****** Object:  Default [DF__UserPrivi__DataT__6C5905DD]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivilegesLocations] ADD  DEFAULT ((0)) FOR [DataTransfer]
GO
/****** Object:  ForeignKey [FK_dbo.UserGroupPrivileges_dbo.UserGroup_UserGroupID]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserGroupPrivileges]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserGroupPrivileges_dbo.UserGroup_UserGroupID] FOREIGN KEY([UserGroupID])
REFERENCES [dbo].[UserGroup] ([UserGroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroupPrivileges] CHECK CONSTRAINT [FK_dbo.UserGroupPrivileges_dbo.UserGroup_UserGroupID]
GO
/****** Object:  ForeignKey [FK_dbo.UserPrivileges_dbo.UserMaster_UserMasterID]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivileges]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserPrivileges_dbo.UserMaster_UserMasterID] FOREIGN KEY([UserMasterID])
REFERENCES [dbo].[UserMaster] ([UserMasterID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPrivileges] CHECK CONSTRAINT [FK_dbo.UserPrivileges_dbo.UserMaster_UserMasterID]
GO
/****** Object:  ForeignKey [FK_dbo.UserPrivilegesLocations_dbo.UserMaster_UserMasterID]    Script Date: 05/20/2014 15:16:18 ******/
ALTER TABLE [dbo].[UserPrivilegesLocations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserPrivilegesLocations_dbo.UserMaster_UserMasterID] FOREIGN KEY([UserMasterID])
REFERENCES [dbo].[UserMaster] ([UserMasterID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPrivilegesLocations] CHECK CONSTRAINT [FK_dbo.UserPrivilegesLocations_dbo.UserMaster_UserMasterID]
GO









INSERT INTO UserGroup (UserGroupName, IsDelete, GroupOfCompanyID, CreatedUser, CreatedDate, ModifiedUser, ModifiedDate, DataTransfer)
VALUES ('ADMINISTRATOR', 0, 1, 'Admin', GETDATE(), 'Admin', GETDATE(), 0)

INSERT INTO UserMaster (CompanyID, LocationID, UserName, UserDescription, [Password], UserGroupID, IsActive, IsUserCantChangePassword, IsUserMustChangePassword, IsDelete)
SELECT 1, 1, 'Admin', 'ADMINISTRATOR', 'AdminPwd', UserGroupID, 1, 0, 0, 0  FROM UserGroup


INSERT INTO TransactionRights (DocumentID, TransactionCode, TransactionName, TransactionTypeID, IsAccess, IsPause, IsSave, IsModify, IsDelete)
SELECT DocumentID, PreFix, FormText, ModuleType, 1, 1, 1,1 , 0  FROM AutoGenerateInfo


INSERT INTO UserPrivileges(UserMasterID, TransactionRightsID, FormID, TransactionTypeID, IsAccess, IsModify, IsPause, IsSave, IsView)
SELECT 1, ai.AutoGenerateInfoID, ai.DocumentID, tr.TransactionTypeID, 1,1,1,1,1 FROM AutoGenerateInfo ai inner join TransactionRights tr
ON tr.DocumentID = ai.DocumentID


INSERT INTO  UserGroupPrivileges ([TransactionRightsID],[UserGroupID] ,[TransactionTypeID] ,[IsAccess]
      ,[IsPause] ,[IsSave] ,[IsModify], [IsView], [IsDelete])
SELECT  ai.AutoGenerateInfoID, 1, tr.TransactionTypeID, 1,1,1,1,1,0 FROM AutoGenerateInfo ai inner join TransactionRights tr
ON tr.DocumentID = ai.DocumentID


INSERT INTO UserPrivilegesLocations (UserMasterID, UserGroupID, LocationID, IsSelect)
SELECT 1, 1, locationId, 1 FROM Location 





