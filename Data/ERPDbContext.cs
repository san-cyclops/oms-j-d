using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data
{
    public class ERPDbContext: DbContext
    {
        #region Constructors and Destructors

        public ERPDbContext() : base("SysConn")
        {

        }

        #endregion

        #region Public properties

        #region Common
        public DbSet<SystemConfig> SystemConfig { get; set; }
        public DbSet<SystemFeature> SystemFeatures { get; set; } 

        public DbSet<AutoGenerateInfo> AutoGenerateInfos { get; set; }

        public DbSet<DocumentNumber> DocumentNumbers { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<GroupOfCompany> GroupOfCompanies { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Helper> Helpers { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<CostCentre> CostCentres { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierGroup> SupplierGroups { get; set; }
        public DbSet<CommissionSchema> CommissionSchemas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationAssignedCostCentre> LocationAssignedCostCentres { get; set; }
        public DbSet<ReferenceType> ReferenceTypes { get; set; }
        public DbSet<SupplierProperty> SupplierProperties { get; set; }
        
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDesignationType> EmployeeDesignationTypes { get; set; }
        public DbSet<EmployeeBackup> EmployeeBackups { get; set; }  

        //User Privileges
        public DbSet<TransactionRights> TransactionRights { get; set; }
        public DbSet<UserGroupPrivileges> UserGroupPrivileges { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<UserPrivileges> UserPrivileges { get; set; }
        public DbSet<UserPrivilegesLocations> UserPrivilegesLocations { get; set; }

        public DbSet<ReportGenerator> ReportGenerators { get; set; }

        #region Transaction

        //Opening Stock
        public DbSet<OpeningStockHeader> OpeningStockHeaders { get; set; }
        public DbSet<OpeningStockDetail> OpeningStockDetails { get; set; }

        //Sample In & Out
        public DbSet<SampleOutHeader> SampleOutHeaders { get; set; }
        public DbSet<SampleOutDetail> SampleOutDetails { get; set; }

        public DbSet<SampleInHeader> SampleInHeaders { get; set; }
        public DbSet<SampleInDetail> SampleInDetails { get; set; }

        #endregion
        #endregion

        #region Inventory
        //Reference
        public DbSet<InvPaymentCardType> InvPaymentCardType { get; set; }
        public DbSet<InvDepartment> InvDepartments { get; set; }
        public DbSet<InvCategory> InvCategories { get; set; }
        public DbSet<InvSubCategory> InvSubCategories { get; set; }
        public DbSet<InvSubCategory2> InvSubCategories2 { get; set; }        
        public DbSet<InvSize> InvSizes { get; set; }   
        
        public DbSet<InvSalesPerson> InvSalesPersons { get; set; }
        public DbSet<InvProductExtendedProperty> InvProductExtendedProperties { get; set; }
        public DbSet<InvProductExtendedValue> InvProductExtendedValues { get; set; }

        public DbSet<InvSales> InvSales { get; set; }
        public DbSet<InvPurchase> InvPurchase { get; set; }

        public DbSet<InvScanner> InvScanner { get; set; }
        public DbSet<InvScannerData> InvScannerData { get; set; }

        //Product
        public DbSet<InvProductLink> InvProductLinks { get; set; }
        public DbSet<InvProductMaster> InvProductMasters { get; set; }
        public DbSet<InvProductStockMaster> InvProductStockMasters { get; set; }
        public DbSet<InvProductSupplierLink> InvProductSupplierLinks { get; set; }
        public DbSet<InvProductUnitConversion> InvProductUnitConversions { get; set; }
        public DbSet<InvProductBatchNoExpiaryDetail> InvProductBatchNoExpiaryDetails { get; set; }
        public DbSet<InvProductSerialNoDetail> InvProductSerialNoDetails { get; set; }
        public DbSet<InvProductSerialNo> InvProductSerialNos { get; set; }

        public DbSet<InvProductExtendedPropertyValue> InvProductExtendedPropertyValues { get; set; }
        public DbSet<ProductCodeDependancy> ProductCodeDependancies { get; set; }
        public DbSet<InvProductPriceChangeDetail> InvProductPriceChangeDetails { get; set; }
        public DbSet<InvProductPriceChangeHeader> InvProductPriceChangeHeaders { get; set; }
        public DbSet<InvProductPriceChangeDetailDamage> InvProductPriceChangeDetailDamages { get; set; }
        public DbSet<InvProductPriceChangeHeaderDamage> InvProductPriceChangeHeaderDamages { get; set; }
        public DbSet<InvDamageType> InvDamageTypes { get; set; }

        public DbSet<InvProductProperty> InvProductProperties { get; set; }

        public DbSet<InvProductPriceLink> InvProductPriceLinks { get; set; }

        // Return Types
        public DbSet<InvReturnType> InvReturnTypes { get; set; }

        public DbSet<InvReportBinCard> InvReportBinCards { get; set; }

        //Transaction
        //Stock Adjustment
        public DbSet<InvStockAdjustmentHeader> InvStockAdjustmentHeaders { get; set; }
        public DbSet<InvStockAdjustmentDetail> InvStockAdjustmentDetails { get; set; }

        

        //Sales Order
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        //Quotation
        public DbSet<InvQuotationHeader> InvQuotationHeaders { get; set; }
        public DbSet<InvQuotationDetail> InvQuotationDetails { get; set; }

        //TOG
        public DbSet<InvTransferNoteHeader> InvTransferNoteHeaders { get; set; }
        public DbSet<InvTransferNoteDetail> InvTransferNoteDetails { get; set; }
        public DbSet<InvTransferType> InvTransferType { get; set; }

        //Invoice
        public DbSet<InvSalesHeader> InvSalesHeaders { get; set; }
        public DbSet<InvSalesDetail> InvSalesDetails { get; set; }

        public DbSet<InvSalesHeader2> InvSalesHeader2s { get; set; }
        public DbSet<InvSalesDetail2> InvSalesDetail2s { get; set; }

        public DbSet<InvSalesPayment> InvSalesPayments { get; set; }

        

        //Promotion Master
        public DbSet<InvPromotionType> InvPromotionTypes { get; set; }
        public DbSet<InvPromotionMaster> InvPromotionMasters { get; set; }
        public DbSet<InvPromotionDetailsBuyXProduct> InvPromotionDetailsBuyXProduct { get; set; }
        public DbSet<InvPromotionDetailsBuyXDepartment> InvPromotionDetailsBuyXDepartment { get; set; }
        public DbSet<InvPromotionDetailsBuyXCategory> InvPromotionDetailsBuyXCategory { get; set; }
        public DbSet<InvPromotionDetailsBuyXSubCategory> InvPromotionDetailsBuyXSubCategory { get; set; }
        public DbSet<InvPromotionDetailsBuyXSubCategory2> InvPromotionDetailsBuyXSubCategory2 { get; set; }
        public DbSet<InvPromotionDetailsGetYDetails> InvPromotionDetailsGetYDetails { get; set; }
        public DbSet<InvPromotionDetailsProductDis> InvPromotionDetailsProductDis { get; set; }
        public DbSet<InvPromotionDetailsDepartmentDis> InvPromotionDetailsDepartmentDis { get; set; }
        public DbSet<InvPromotionDetailsCategoryDis> InvPromotionDetailsCategoryDis { get; set; }
        public DbSet<InvPromotionDetailsSubCategoryDis> InvPromotionDetailsSubCategoryDis { get; set; }
        public DbSet<InvPromotionDetailsSubTotal> InvPromotionDetailsSubTotal { get; set; }
        public DbSet<InvPromotionDetailsAllowTypes> InvPromotionDetailsAllowTypes { get; set; }
        public DbSet<InvPromotionDetailsAllowLocations> InvPromotionDetailsAllowLocations { get; set; }
        public DbSet<InvPromotionDetailsSubCategory2Dis> InvPromotionDetailsSubCategory2Dis { get; set; }


        // Purchase Order
        public DbSet<InvPurchaseOrderHeader> InvPurchaseOrderHeaders { get; set; }
        public DbSet<InvPurchaseOrderDetail> InvPurchaseOrderDetails { get; set; }

        //GRN
        public DbSet<InvPurchaseHeader> InvPurchaseHeaders { get; set; }
        public DbSet<InvPurchaseDetail> InvPurchaseDetails { get; set; }

        //GiftVoucher
        public DbSet<InvGiftVoucherGroup> InvGiftVoucherGroups { get; set; }
        public DbSet<InvGiftVoucherBookCode> InvGiftVoucherBookCodes { get; set; }
        public DbSet<InvGiftVoucherMaster> InvGiftVoucherMasters { get; set; }

        public DbSet<InvGiftVoucherPurchaseOrderHeader> InvGiftVoucherPurchaseOrderHeaders { get; set; }
        public DbSet<InvGiftVoucherPurchaseOrderDetail> InvGiftVoucherPurchaseOrderDetails { get; set; }

        public DbSet<InvGiftVoucherPurchaseHeader> InvGiftVoucherPurchaseHeaders { get; set; }
        public DbSet<InvGiftVoucherPurchaseDetail> InvGiftVoucherPurchaseDetails { get; set; }

        public DbSet<InvGiftVoucherTransferNoteHeader> InvGiftVoucherTransferNoteHeaders { get; set; }
        public DbSet<InvGiftVoucherTransferNoteDetail> InvGiftVoucherTransferNoteDetails { get; set; }

        //Temp
        public DbSet<InvTmpProductStockDetails> InvTmpProductStockDetails { get; set; }
        public DbSet<LgsTmpProductStockDetail> LgsTmpProductStockDetails { get; set; }
        public DbSet<InvTmpReportDetail> InvTmpReportDetails { get; set; }
        //public DbSet<InvTmpSalesProductExtendedProperty> InvTmpSalesProductExtendedProperties { get; set; } 

        #endregion

        #region CRM
        public DbSet<LoyaltyCustomer> LoyaltyCustomers { get; set; }
        public DbSet<LoyaltySuplimentary> LoyaltySuplimentaries { get; set; }
        public DbSet<CardMaster> CardMasters { get; set; }
        public DbSet<LoyaltyCardGenerationHeader> LoyaltyCardGenerationHeaders { get; set; }
        public DbSet<LoyaltyCardGenerationDetail> LoyaltyCardGenerationDetails { get; set; }
        public DbSet<LoyaltyCardAllocation> LoyaltyCardAllocations { get; set; }
        public DbSet<LoyaltyCardIssueHeader> LoyaltyCardIssueHeaders { get; set; }
        public DbSet<LoyaltyCardIssueDetail> LoyaltyCardIssueDetails { get; set; }
        public DbSet<CardGenerationSetting> CardGenerationSettings { get; set; }
        public DbSet<LoyaltyCardAllocationLog> CardAllocationLogs { get; set; }
        public DbSet<CustomerFeedBack> CustomerFeedBacks { get; set; }
        public DbSet<LostAndRenew> LostAndRenews { get; set; }
        
        public DbSet<PointsBreakdown> PointsBreakdowns { get; set; }
        public DbSet<TempPointsBreakdown> TempPointsBreakdowns { get; set; }  

        public DbSet<TempLoyaltyTransactionSummery> TempLoyaltyTransactionSummerys { get; set; }

        #endregion

     

        #region Non Trading
        public DbSet<LgsPurchase> LgsPurchase { get; set; }

        //Logistic
        public DbSet<LgsDepartment> LgsDepartments { get; set; }
        public DbSet<LgsCategory> LgsCategories { get; set; }
        public DbSet<LgsSubCategory> LgsSubCategories { get; set; }
        public DbSet<LgsSubCategory2> LgsSubCategories2 { get; set; }

        public DbSet<LgsSupplier> LgsSuppliers { get; set; }
        public DbSet<LgsSupplierProperty> LgsSupplierProperties { get; set; }
        public DbSet<LgsProductExtendedPropertyValue> LgsProductExtendedPropertyValues { get; set; }

        //Logistic Product
        public DbSet<LgsProductLink> LgsProductLinks { get; set; }
        public DbSet<LgsProductMaster> LgsProductMasters { get; set; }
        public DbSet<LgsProductStockMaster> LgsProductStockMasters { get; set; }
        public DbSet<LgsProductSupplierLink> LgsProductSupplierLinks { get; set; }
        public DbSet<LgsProductUnitConversion> LgsProductUnitConversions { get; set; }
        public DbSet<LgsProductBatchNoExpiaryDetail> LgsProductBatchNoExpiaryDetails { get; set; }
        public DbSet<LgsProductSerialNoDetail> LgsProductSerialNoDetails { get; set; }
        public DbSet<LgsProductSerialNo> LgsProductSerialNos { get; set; }

        // Return Types
        public DbSet<LgsReturnType> LgsReturnTypes { get; set; }

        #region Transaction
        //Service In & Out
        public DbSet<ServiceOutHeader> ServiceOutHeaders { get; set; }
        public DbSet<ServiceOutDetail> ServiceOutDetails { get; set; }
        public DbSet<ServiceInHeader> ServiceInHeaders { get; set; }
        public DbSet<ServiceInDetail> ServiceInDetails { get; set; }

        //Stock Adjustment
       
        public DbSet<LgsStockAdjustmentHeader> LgsStockAdjustmentHeaders { get; set; }
        public DbSet<LgsStockAdjustmentDetail> LgsStockAdjustmentDetails { get; set; }

        public DbSet<LgsQuotationHeader> LgsQuotationHeaders { get; set; }
        public DbSet<LgsQuotationDetail> LgsQuotationDetails { get; set; }

        public DbSet<LgsPurchaseOrderHeader> LgsPurchaseOrderHeaders { get; set; }
        public DbSet<LgsPurchaseOrderDetail> LgsPurchaseOrderDetails { get; set; }


        public DbSet<LgsPurchaseDetail> LgsPurchaseDetails { get; set; }
        public DbSet<LgsPurchaseHeader> LgsPurchaseHeaders { get; set; }

        // Logistic - Material Request Note
        public DbSet<LgsMaterialRequestHeader> LgsMaterialRequestHeaders { get; set; }
        public DbSet<LgsMaterialRequestDetail> LgsMaterialRequestDetails { get; set; }

        // Logistic - Material Allocation Note
        public DbSet<LgsMaterialAllocationHeader> LgsMaterialAllocationHeaders { get; set; }
        public DbSet<LgsMaterialAllocationDetail> LgsMaterialAllocationDetails { get; set; }

        // Logistic - Material Consumption Note
        public DbSet<LgsMaterialConsumptionHeader> LgsMaterialConsumptionHeaders { get; set; }
        public DbSet<LgsMaterialConsumptionDetail> LgsMaterialLgsMaterialConsumptionDetails { get; set; }

        // Logistic - Maintenance Job Requisition Note
        public DbSet<LgsMaintenanceJobRequisitionHeader> LgsMaintenanceJobRequisitionHeaders { get; set; }

        // Logistic - Maintenance Job Assign Note
        public DbSet<LgsMaintenanceJobAssignHeader> LgsMaintenanceJobAssignHeaders { get; set; }
        public DbSet<LgsMaintenanceJobAssignProductDetail> LgsMaintenanceJobAssignProductDetails { get; set; }
        public DbSet<LgsMaintenanceJobAssignEmployeeDetail> LgsMaintenanceJobAssignEmployeeDetails { get; set; }

        // Logistic - TOG
        public DbSet<LgsTransferNoteHeader> LgsTransferNoteHeaders { get; set; }
        public DbSet<LgsTransferNoteDetail> LgsTransferNoteDetails { get; set; }
        public DbSet<LgsTransferType> LgsTransferTypes { get; set; }


        #endregion
        #endregion
        
        #region POS

        //SPOS_DATA
        public DbSet<PaymentDet> PaymentDets { get; set; }
        public DbSet<TransactionDet> TransactionDets { get; set; }
        public DbSet<InvLoyaltyTransaction> InvLoyaltyTransactions { get; set; }
        public DbSet<InvPosConfiguration> InvPosConfigurations { get; set; }
        public DbSet<InvEmployeeTransaction> InvEmployeeTransactions { get; set; }
        public DbSet<InvCreditNoteHed> InvCreditNoteHeds { get; set; }
        public DbSet<InvCreditNoteDet> InvCreditNoteDets { get; set; }
        public DbSet<CashierFunction> CashierFunctions { get; set; }
        public DbSet<CashierPermission> CashierPermissions { get; set; }
        public DbSet<CashierGroup> CashierGroups { get; set; }  
        public DbSet<RSalesSummary> RSalesSummarys { get; set; }
        public DbSet<InvPosTerminalDetails> InvPosTerminalDetails { get; set; }

        public DbSet<PayType> PayTypes { get; set; }
        public DbSet<BankPos> Bankspos { get; set; }
        public DbSet<BankBin> BankBins { get; set; }

        #endregion

        #region General Ledger
        // General Ledger
        public DbSet<AccLedgerSerialNumber> AccLedgerSerialNumbers { get; set; }

        public DbSet<AccLedgerAccount> AccLedgerAccounts { get; set; }
        public DbSet<AccCardCommission> AccCardCommissions { get; set; }

        public DbSet<AccChequeNoRegisty> AccChequeNoRegisties { get; set; }

        public DbSet<AccTransactionTypeHeader> AccTransactionTypeHeaders { get; set; }
        public DbSet<AccTransactionTypeDetail> AccTransactionTypeDetails { get; set; }

        public DbSet<AccPaymentHeader> AccPaymentHeaders { get; set; }
        public DbSet<AccPaymentDetail> AccPaymentDetails { get; set; }

        public DbSet<AccChequeDetail> AccChequeDetails { get; set; }

        public DbSet<AccGlTransactionHeader> AccGlTransactionHeaders { get; set; }
        public DbSet<AccGlTransactionDetail> AccGlTransactionDetails { get; set; }

        #region Petty Cash

        // Petty Cash
        public DbSet<AccPettyCashMaster> AccPettyCashMasters { get; set; }

        // IOU Details
        public DbSet<AccPettyCashIOUHeader> AccPettyCashIOUHeaders { get; set; }
        public DbSet<AccPettyCashIOUDetail> AccPettyCashIOUDetails { get; set; }

        public DbSet<AccPettyCashImprestDetail> AccPettyCashImprestDetails { get; set; }

        public DbSet<AccPettyCashReimbursement> AccPettyCashReimbursements { get; set; }

        // Petty cash Bill Details
        public DbSet<AccPettyCashBillHeader> AccPettyCashBillHeaders { get; set; }
        public DbSet<AccPettyCashBillDetail> AccPettyCashBillDetails { get; set; }

        // Petty cash Voucher Details
        public DbSet<AccPettyCashVoucherHeader> AccPettyCashVoucherHeaders { get; set; }
        public DbSet<AccPettyCashVoucherDetail> AccPettyCashVoucherDetails { get; set; }

        // Petty cash Payment Details
        public DbSet<AccPettyCashPaymentHeader> AccPettyCashPaymentHeaders { get; set; }
        public DbSet<AccPettyCashPaymentDetail> AccPettyCashPaymentDetails { get; set; }

        public DbSet<AccPettyCashPaymentProcessHeader> AccPettyCashPaymentProcessHeaders { get; set; }
        public DbSet<AccPettyCashPaymentProcessDetail> AccPettyCashPaymentProcessDetails { get; set; }

        #endregion

        #endregion

        #region Reports

        public DbSet<InvBasketAnalysisValueRange> InvBasketAnalysisValueRanges { get; set; }
        public DbSet<InvBasketAnalysisValueRangeTemp> InvBasketAnalysisValueRangeTemps { get; set; }
        public DbSet<InvBasketAnalysisSelectedLocationsTemp> InvBasketAnalysisSelectedLocationsTemps { get; set; }
        public DbSet<TempLocationWiseLoyaltyAnalysis> TempLocationsWiseLoyaltyAnalysis { get; set; }
       // public DbSet<InvTmpReportDetail> InvTmpReportDetails { get; set; }
        //public DbSet<InvBasketAnalysisValueRange> InvBasketAnalysisValueRanges { get; set; }


        #endregion

        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ERPDbContext>());

            //****if any one uncomment this, please dont commit****
            //Database.SetInitializer(new ERPDbContextInitializer());

            Database.SetInitializer<ERPDbContext>(null); 

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<decimal>()
                .Configure(x => x.HasPrecision(18, Utility.Common.decimalPointsCurrency));

            modelBuilder.Properties<decimal>()
                .Where(prop => prop.Name.Contains("Qty"))
                //.Configure(config => config.IsKey());
                .Configure(x => x.HasPrecision(18, Utility.Common.decimalPointsQty));

            modelBuilder.Entity<Territory>()
                        .HasRequired(t => t.Area)
                        .WithMany(p => p.Terriotories)
                        .HasForeignKey(t => t.AreaID)
                        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvSalesPerson>()
            //            .HasRequired(t => t.CommissionSchema)
            //            .WithMany(p => p.InvSalesmans)
            //            .HasForeignKey(t => t.CommissionSchemaID)
            //            .WillCascadeOnDelete(false);

            
            #region Gift Voucher
            modelBuilder.Entity<InvGiftVoucherBookCode>()
                        .HasRequired(t => t.InvGiftVoucherGroup)
                        .WithMany(p => p.InvGiftVoucherMasterBooks)
                        .HasForeignKey(t => t.InvGiftVoucherGroupID)
                        .WillCascadeOnDelete(false);

            #endregion

            #region Supplier -

            //should have optional
            //modelBuilder.Entity<AccLedgerAccount>()
            //            .HasMany(u => u.Suppliers)
            //            .WithRequired(ul => ul.AccLedgerAccount)
            //            .HasForeignKey(ul => ul.AccLedgerAccountID);

            //modelBuilder.Entity<Supplier>().HasRequired(s => s.AccLedgerAccount)
            //                       .WithMany(d => d.Suppliers)
            //                       .HasForeignKey(s => s.AccLedgerAccountID);

            //optional
            //modelBuilder.Entity<Supplier>()
            //            .HasOptional(s => s.AccLedgerAccount)
            //            .WithMany(d => d.Suppliers)
            //            .HasForeignKey(s => s.AccLedgerAccountID);

            //modelBuilder.Entity<AccLedgerAccount>()
            //            .HasMany(u => u.OtherSuppliers)
            //            .WithRequired(ul => ul.AccOtherLedgerAccount)
            //            .HasForeignKey(ul => ul.OtherLedgerID)
            //            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                        .HasRequired(t => t.SupplierGroup)
                        .WithMany(p => p.Suppliers)
                        .HasForeignKey(t => t.SupplierGroupID)
                        .WillCascadeOnDelete(false);


            // Configure the primary key for the SupplierProperty
            modelBuilder.Entity<SupplierProperty>()
                        .HasKey(t => t.SupplierID);

            // Map one-to-zero or one relationship
            modelBuilder.Entity<Supplier>()
                .HasRequired(x => x.SupplierProperty)
                .WithRequiredPrincipal();
            
            #endregion

            #region Logistic Supplier....

            //modelBuilder.Entity<AccLedgerAccount>()
            //    .HasMany(u => u.LgsSuppliers)
            //    .WithRequired(ul => ul.AccLedgerAccount)
            //    .HasForeignKey(ul => ul.AccLedgerAccountID);

            //modelBuilder.Entity<AccLedgerAccount>()
            //            .HasMany(u => u.OtherLgsSuppliers)
            //            .WithRequired(ul => ul.AccOtherLedgerAccount)
            //            .HasForeignKey(ul => ul.OtherLedgerID)
            //            .WillCascadeOnDelete(false);

            modelBuilder.Entity<LgsSupplier>()
                        .HasRequired(t => t.SupplierGroup)
                        .WithMany(p => p.LgsSuppliers)
                        .HasForeignKey(t => t.SupplierGroupID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<LgsSupplierProperty>()
                        .HasKey(t => t.LgsSupplierID);

            modelBuilder.Entity<LgsSupplier>()
                .HasRequired(x => x.LgsSupplierProperty)
                .WithRequiredPrincipal();

            #endregion

            #region Customer -

            //modelBuilder.Entity<AccLedgerAccount>()
            //            .HasMany(u => u.Customers)
            //            .WithRequired(ul => ul.AccLedgerAccount)
            //            .HasForeignKey(ul => ul.AccLedgerAccountID);

            //modelBuilder.Entity<AccLedgerAccount>()
            //            .HasMany(u => u.OtherCustomers)
            //            .WithRequired(ul => ul.AccOtherLedgerAccount)
            //            .HasForeignKey(ul => ul.OtherLedgerID)
            //            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                        .HasRequired(t => t.CustomerGroup)
                        .WithMany(p => p.Customers)
                        .HasForeignKey(t => t.CustomerGroupID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                        .HasRequired(t => t.PaymentMethod)
                        .WithMany(p => p.Customers)
                        .HasForeignKey(t => t.PaymentMethodID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                        .HasRequired(t => t.Area)
                        .WithMany(p => p.Customers)
                        .HasForeignKey(t => t.AreaID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                        .HasRequired(t => t.Territory)
                        .WithMany(p => p.Customers)
                        .HasForeignKey(t => t.TerritoryID)
                        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Customer>()
            //           .HasRequired(t => t.Broker)
            //           .WithMany(p => p.Customers)
            //           .HasForeignKey(t => t.BrokerID)
            //           .WillCascadeOnDelete(false);
            #endregion

            #region Loyalty Customer -

            //modelBuilder.Entity<LoyaltySuplimentary>()
            //            .HasMany(u => u.LoyaltyCustomers)
            //            .WithRequired(ul => ul.LoyaltySuplimentary)
            //            .HasForeignKey(ul => ul.LoyaltySuplimentaryID);


           
            #endregion

            #region Inventory Purchase Order

            //modelBuilder.Entity<InvPurchaseOrderDetail>()
            //            .HasRequired(t => t.InvPurchaseOrderHeader)
            //            .WithMany(p => p.InvPurchaseOrderDetails)
            //            .HasForeignKey(t => t.InvPurchaseOrderHeaderID)
            //            .WillCascadeOnDelete(false);


            #endregion

            #region General Ledger

            #region Petty Cash
            // Configure the primary key for the AccPettyCashMaster
            //modelBuilder.Entity<AccPettyCashMaster>()
            //            .HasKey(t => t.AccLedgerAccountID)
            //            .HasRequired(t=> t.AccLedgerAccount)
            //            .WithRequiredPrincipal();

            // IOU
            //modelBuilder.Entity<AccPettyCashIOUDetail>()
            //            .HasRequired(t => t.AccPettyCashIOUHeader)
            //            .WithMany(p => p.AccPettyCashIOUDetails)
            //            .HasForeignKey(t => t.AccPettyCashIOUHeaderID)
            //            .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccPettyCashBillDetail>()
                        .HasRequired(t => t.AccPettyCashBillHeader)
                        .WithMany(p => p.AccPettyCashBillDetails)
                        .HasForeignKey(t => t.AccPettyCashBillHeaderID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccPettyCashVoucherDetail>()
                        .HasRequired(t => t.AccPettyCashVoucherHeader)
                        .WithMany(p => p.AccPettyCashVoucherDetails)
                        .HasForeignKey(t => t.AccPettyCashVoucherHeaderID)
                        .WillCascadeOnDelete(false);

            //iou
            modelBuilder.Entity<AccPettyCashIOUHeader>().HasMany<AccPettyCashIOUDetail>(s => s.AccPettyCashIOUDetails)
            .WithRequired(s => s.AccPettyCashIOUHeader).HasForeignKey(s => s.AccPettyCashIOUHeaderID);

            #endregion
            #endregion

        }
        #endregion


        public string GetConnection()
        {

            ERPDbContext context = new Data.ERPDbContext();
            //System.Data.EntityClient.EntityConnection c = (System.Data.EntityClient.EntityConnection)context.Database.Connection;

            System.Data.Entity.Core.EntityClient.EntityConnection c = (System.Data.Entity.Core.EntityClient.EntityConnection)context.Database.Connection;
            
            return c.StoreConnection.ConnectionString;
            
        }
      
    }
}
