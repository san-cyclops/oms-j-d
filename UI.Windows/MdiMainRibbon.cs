using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Report.Account;
using Report.CRM;
using Report.CRM.Reports;
using Report.Com;
using Report.Com.Reference.Reports;
using Report.GV;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Report.Inventory.Transactions.Reports;
using Report.Logistic;
using Report.Logistic.Reference.Reports;
using Service;
using Utility;
using Report;
using Domain;
using System.Windows.Forms.RibbonHelpers;
using System.Reflection;
using Report.GUI;

namespace UI.Windows
{
    public partial class MdiMainRibbon : Form
    {

        List<UserPrivileges> privilegesList = new List<UserPrivileges>();

        public MdiMainRibbon()
        {
            InitializeComponent();
        }

        private void ribbon1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_MenuDeactivate(object sender, EventArgs e)
        {
           
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void rbnTbMenu_PressedChanged(object sender, EventArgs e)
        {
           
        }

        private void ribbtnSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplierGroup frmSupplierGroup = new FrmSupplierGroup();
            Common.SetMenu(frmSupplierGroup, this);
        }

        private void MdiMainRibbon_Load(object sender, EventArgs e)
        {
            //this.clock1.UtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
            this.digitalDisplayControl1.DigitText = DateTime.Now.ToString("HH:mm:ss");
            GetActualName();

            // Mdi menu 
            if (Common.ModuleTypes.InventoryAndSales.Equals(true)) { this.rbnTbInventory.Visible = true; this.mnuInventory.Visible = true; } else { this.rbnTbInventory.Visible = false; this.mnuInventory.Visible = false; }
           // if (Common.ModuleTypes.PointOfSales.Equals(true)) { this.rbnTbPos.Visible = true; this.mnuPos.Visible = true; } else { this.rbnTbPos.Visible = false; this.mnuPos.Visible = false; }
            if (Common.ModuleTypes.Manufacture.Equals(true)) { ; } else { ; }
            //if (Common.ModuleTypes.CustomerRelationshipModule.Equals(true)) { this.rbnTbCusRelation.Visible = true; this.mnuCrm.Visible = true; } else { this.rbnTbCusRelation.Visible = false; this.mnuCrm.Visible = false; }
            //if (Common.ModuleTypes.GiftVouchers.Equals(true)) { this.rbnTbGiftVoucher.Visible = true; this.mnuGiftVoucher.Visible = true; } else { this.rbnTbGiftVoucher.Visible = false; this.mnuGiftVoucher.Visible = false; }
           // if (Common.ModuleTypes.Accounts.Equals(true)) { this.rbnTbAccounts.Visible = true; this.mnuAccounts.Visible = true; } else { this.rbnTbAccounts.Visible = false; this.mnuAccounts.Visible = false; }
            //if (Common.ModuleTypes.NonTrading.Equals(true)) { this.rbnTbNonTrading.Visible = true; this.mnuNonTrading.Visible = true; } else { this.rbnTbNonTrading.Visible = false; this.mnuNonTrading.Visible = false; }
            if (Common.ModuleTypes.HrManagement.Equals(true)) { ; } else { ; }
            if (Common.ModuleTypes.HospitalManagement.Equals(true)) { ; } else { ; }
            if (Common.ModuleTypes.HotelManagement.Equals(true)) { ; } else { ; }

        }

        private void GetActualName()
        {
            rbnPnlBtnCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            rbnPnlBtnSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            rbnPnlBtnSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;
           
            mnuInvRefeDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            mnuInvRefeCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            mnuInvRefeSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            mnuInvRefeSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

            rbnbtnDept.Text = string.Concat(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText, " Details");
            rbnbtnCate.Text = string.Concat(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText, " Details");
            rbnbtnSubCat.Text = string.Concat(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText, " Details");
            rbnbtnSubCat2.Text = string.Concat(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText, " Details");
        }

        private void rbnPnlBtnDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            Common.SetMenu(frmDepartment, this);
        }

        private void rbnPnlBtnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frmCategory = new FrmCategory();
            Common.SetMenu(frmCategory, this);
        }

        private void rbnPnlBtnSubCategory_Click(object sender, EventArgs e)
        {
            FrmSubCategory frmSubCategory = new FrmSubCategory();
            Common.SetMenu(frmSubCategory, this);
        }

        private void rbnPnlBtnSubCategory2_Click(object sender, EventArgs e)
        {
            FrmSubCategory2 frmSubCategory2 = new FrmSubCategory2();
            Common.SetMenu(frmSubCategory2, this);
        }

        private void rbnPnlBtnProduct_Click(object sender, EventArgs e)
        {
            FrmProduct frmProduct = new FrmProduct();
            Common.SetMenu(frmProduct, this);
        }

        private void rbnPnlBtnExPro_Click(object sender, EventArgs e)
        {
            FrmProductExtendedProperty frmProductExtendedProperty = new FrmProductExtendedProperty();
            Common.SetMenu(frmProductExtendedProperty, this);
        }

        private void rbnPnlBtnSalesman_Click(object sender, EventArgs e)
        {
            FrmSalesPerson frmSalesPerson = new FrmSalesPerson();
            Common.SetMenu(frmSalesPerson, this);
        }

        private void rbnPnlInvOs_Click(object sender, EventArgs e)
        {
            FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
            Common.SetMenu(frmOpeningStock, this);
        }

        private void rbnPnlInvPo_Click(object sender, EventArgs e)
        {
            FrmPurchaseOrder frmPurchaseOrder = new FrmPurchaseOrder();
            Common.SetMenu(frmPurchaseOrder, this);
        }

        private void rbnPnlInvGrn_Click(object sender, EventArgs e)
        {
            FrmGoodsReceivedNote frmGoodsReceivedNote = new FrmGoodsReceivedNote();
            Common.SetMenu(frmGoodsReceivedNote, this);
        }

        private void rbnPnlInvPrn_Click(object sender, EventArgs e)
        {
            FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
            Common.SetMenu(frmPurchaseReturnNote, this);
        }

        private void rbnPnlInvTog_Click(object sender, EventArgs e)
        {
            FrmTransferOfGoodsNote frmTransferOfGoodsNote = new FrmTransferOfGoodsNote();
            Common.SetMenu(frmTransferOfGoodsNote, this);
        }

        private void rbnPnlInvInvoice_Click(object sender, EventArgs e)
        {
            FrmInvoice frmInvoice = new FrmInvoice();
            Common.SetMenu(frmInvoice, this);
        }

        private void rbnPnlInvSaReturn_Click(object sender, EventArgs e)
        {
            FrmSalesReturnNote frmSalesReturnNote = new FrmSalesReturnNote();
            Common.SetMenu(frmSalesReturnNote, this);
        }

        private void rbnPnlInvSa_Click(object sender, EventArgs e)
        {
            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
            Common.SetMenu(frmStockAdjustment, this);
        }

        private void rbnPnlInvSO_Click(object sender, EventArgs e)
        {
            FrmSalesOrder frmSalesOrder = new FrmSalesOrder();
            Common.SetMenu(frmSalesOrder, this);
        }

        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void LoggedStatus(string user, string location)
        {
            this.toolStripStatus.Visible = true;
            this.lblDateTime.Visible = true;
            this.toolStripStatus.Text = "Logged Location  :  " + location.ToString() + "        ||        Logged User  :  " + user.ToString();// +"         " + System.DateTime.Now.ToString(); 
            timer1.Start();
          
        }

        public bool IsValidAccesRights(string formName)
        {
            int documentID;
            //UserPrivileges accessRights = new UserPrivileges();
            bool IsAccess = false;

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);

            documentID = autoGenerateInfo.DocumentID;

            IsAccess = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID).IsAccess;
            if (IsAccess == false) { return false; }
            else {return true;}
        }

        public void CheckUserRights(long userID)
        {
            UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
            privilegesList = userPrivilegesService.GetAccessPrivilegesByUserId(userID);

            var lstitem = privilegesList;

            foreach (var item in lstitem)
            {   switch (item.FormName.ToString())
                {

                #region MasterFiles
                    case "FrmArea": // Company :1
                        this.ribbtnReceipt.Enabled = true;
                        break;

                    
                    case "FrmCompany": // Company :1
                        this.companyToolStripMenuItem.Enabled = true;
                        break;

                    case "FrmLocation": // Location :2
                        this.locationToolStripMenuItem.Enabled = true;
                        this.ribbtnLocation.Enabled = true;
                        this.locationDetailsToolStripMenuItem1.Enabled = true;
                        break;

                    case "FrmCostCentre": // Cost Centers :3
                        this.costCentreToolStripMenuItem.Visible = false;
                        break;
                    
                    case "FrmUnitOfMeasure": // Units Of Measure :6
                        this.unitOfMeasureToolStripMenuItem.Visible = true;
                        break;

                    case "FrmTypes": // Return Types And Transfer Types :7,8
                        this.returnTypeToolStripMenuItem.Enabled = false;
                        this.transferTypesToolStripMenuItem.Visible = false;
                        break;
                    case "FrmCustomerGroup": // Customer Group :13
                        this.mnuComRefeCustomerGroup.Visible = false;
                        this.ribbtnCustomerGroup.Visible = false;
                        break;

                    case "FrmSupplierGroup": // Supplier Group :14
                        this.mnuComRefeSupplierGroup.Visible = false;
                        this.ribbtnSupplierGroup.Visible = false;
                        break;

                    case "FrmPaymentMethod": // Payment Method :15
                        this.paymentMethodToolStripMenuItem.Visible = false;
                        this.paymentMethodDetailsToolStripMenuItem1.Visible = false;
                        break;



                    //case "FrmLoginUser": // Login User :20
                    //    this.transferTypesToolStripMenuItem.Enabled = true;
                    //    break;

                    case "FrmEmployee": // Employee :21
                        this.employeeToolStripMenuItem.Enabled = true;
                        this.rbntbEmployee.Enabled = true;
                        break;

                    case "FrmSupplier": // Supplier :1001
                        this.supplierToolStripMenuItem.Enabled = true;
                        this.rbnPnlBtnSupplier.Enabled = true;
                        this.supplierDetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmCustomer": // Customer :1002
                        this.customerToolStripMenuItem.Enabled = true;
                        this.rbnPnlBtnCustomer.Enabled = true;
                        break;

                    case "FrmDepartment": // Department :1003
                        this.mnuInvRefeDepartment.Enabled = true;
                        this.rbnPnlBtnDepartment.Enabled = true;
                        this.departmentDetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmCategory": // Category :1004
                        this.mnuInvRefeCategory.Enabled = true;
                        this.rbnPnlBtnCategory.Enabled = true;
                        this.categoryDetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmSubCategory": // Sub Category :1005
                        this.mnuInvRefeSubCategory.Enabled = true;
                        this.rbnPnlBtnSubCategory.Enabled = true;
                        this.subCategoryDetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmSubCategory2": // Sub Category2 :1006
                        this.mnuInvRefeSubCategory2.Enabled = true;
                        this.rbnPnlBtnSubCategory2.Enabled = true;
                        this.subCategory2DetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmProduct": // Product :1007
                        this.mnuInvRefeProduct.Enabled = true;
                        this.rbnPnlBtnProduct.Enabled = true;
                        this.productDetailsToolStripMenuItem2.Enabled = true;
                        break;

                    case "FrmSalesPerson": // Sales Person :1008
                        this.mnuInvRefeSalesPerson.Enabled = true;
                        //this.rbnPnlBtnSalesman.Enabled = true;
                        break;

                    case "FrmPromotionMaster": // PromotionMaster :1009
                        this.mnuInvRefePromotionMaster.Visible = false;
                        break;

                   
                #endregion

                #region Transactions

                    //case "FrmPurchaseOrder": // Purchase Order :1501    
                    //    this.purchaseOrderToolStripMenuItem.Enabled = false;
                    //    this.purchaseOrderToolStripMenuItem.Visible = false;
                    //    //this.rbnPnlInvPo.Enabled = true;                        
                    //    this.purchaseOrderToolStripMenuItem2.Enabled = false;
                    //    this.purchaseOrderToolStripMenuItem2.Visible = false;  
                     
                        //break;

                    //case "FrmGoodsReceivedNote": // Goods Received Note :1502  
                    //    this.goodsReceivedNoteGRNToolStripMenuItem.Enabled = true;
                    //    this.goodReceiveNoteToolStripMenuItem.Enabled = true;
                    //    this.rbnPnlInvSOrder.Enabled = true;
                    //    break;

                    //case "FrmPurchaseReturnNote": // Purchase Return Note:1503                        
                    //    this.rbnPnlInvDispatch.Enabled = true;
                    //    this.purchaseReturnToolStripMenuItem.Enabled = true;
                    //    this.purchaseReturnNotePRNToolStripMenuItem1.Enabled = true;
                    //    break;

                    //case "FrmTransferOfGoodsNote": // Transfer Of Goods Note :1504
                    //    this.rbnPnlInvTog.Enabled = true;
                    //    this.transferOfGoodsTOGToolStripMenuItem.Enabled = true;
                    //    this.transferOfGoodsTOGToolStripMenuItem2.Enabled = true;
                    //    break;

                    //case "FrmStockAdjustment": // Stock Adjustment :1505
                    //    this.rbnPnlInvSa.Enabled = true;
                    //    this.stockAdjustmentToolStripMenuItem.Enabled = true;
                    //    this.stockAdjustmentToolStripMenuItem2.Enabled = true;
                        //break;

                    //case "FrmQuotation": // Quotation :1506
                    //    this.rbnPnlInvQuotation.Visible = true;
                    //    this.quotationToolStripMenuItem.Visible = true;
                    //    break;

                    case "FrmSalesOrder": // SalesOrder :1507
                        //this.rbnPnlInvSO.Visible = false;
                        this.rbnPnlInvSOrder.Enabled = true;
                        this.salesOrderToolStripMenuItem.Visible = true;
                        this.salesOrderToolStripMenuItem1.Visible = true;
                        break;

                    case "FrmInvoice": // Invoice :1508
                        this.rbnPnlInvInvoice.Enabled = true;
                        this.salesInvoiceToolStripMenuItem.Enabled = true;
                        break;

                    //case "FrmSalesReturnNote": // Sales Return Note :1509
                    //    this.rbnPnlInvSaReturn.Enabled = true;
                    //    this.salesReturnNoteToolStripMenuItem.Enabled = true;
                    //    break;

                    case "FrmDispatchNote": // DispatchNote :1510
                        this.rbnPnlInvDispatch.Enabled = true;
                        this.dispatchNoteToolStripMenuItem.Visible = true;
                        this.dispatchNoteToolStripMenuItem1.Visible = true;
                        break;

                    //case "FrmBarcode": // BarCode :1511
                    //    this.rbnPnlInvBarcode.Visible = false;
                    //    this.barCodePrintingToolStripMenuItem.Visible = false;
                    //    break;

                    //case "FrmProductPriceChange": // Product Price Change :1512
                    //    this.productPriceChangeToolStripMenuItem.Visible = false;
                    //    this.productPriceChangeToolStripMenuItem2.Visible = false;
                    //    break;

                    //case "FrmProductPriceChangeDamage": // Product Price Damage :1513
                    //    this.productPriceChangeToolStripMenuItem1.Visible = true;
                    //    break;

                    

                    case "FrmOpeningStock": // Opening Stock :503
                        this.openingStockBalanceToolStripMenuItem.Enabled = true;
                        this.rbnPnlInvOs.Enabled = true;
                        break;

                    
                    case "FrmLogistciBinCard":
                        this.stockMovementDetailsToolStripMenuItem.Visible = false;
                        break;

                  
#endregion

                    //-------------------------
                    case "FrmPayment": // Payment :6001
                        this.ribbtnPayment.Visible = false;
                        //this.cashierToolStripMenuItem.Visible = false;
                        break;
     

                   
                        
             // CRM           
                    case "FrmLoyaltyCustomer": // Loyalty Customer :3001
                        
                        
                        this.referenceToolStripMenuItem5.Visible = false;
                        this.customerAddressToolStripMenuItem.Visible = false;
                        this.branchWiseCustomerDetailsToolStripMenuItem1.Visible = false;
                        this.inactiveCustomerDetailsToolStripMenuItem1.Visible = false;

                        this.LocationWiseArapaima_ArrowanaSummery.Visible = false;
                        this.CustomerVisitDetails.Visible = false;
                        this.TopCustomerPerformances.Visible = false;
                        this.CashierPerformances.Visible = false;
                        this.CustomerDetails.Visible = false; 
                        
                        this.CardIssuedDetails.Visible = false;
                        this.CardUsageDetails.Visible = false;

                        break;


   

                #region Reports

                    case "RptProductPriceChangeDamageDetail":
                        this.productPriceChangeDamageToolStripMenuItem.Visible = false;
                        break;
                        
                    case "RptLocationWiseLoyaltySummary":
                        this.otherToolStripMenuItem1.Visible = false;
                        this.locationWiseLoToolStripMenuItem.Visible = false;
                        break;

                    case "RptLocationWiseLoyaltyAnalysis":
                        this.otherToolStripMenuItem1.Visible = false;
                        this.loyaltyAnalysisToolStripMenuItem.Visible = false;
                        break;
                    case "RptCRMRegister":
                        this.mnuCrmRegister.Visible = false;
                        break;
                        
                    case "RptGiftVoucherRegister":
                        this.giftVoucherIssuesToolStripMenuItem.Visible = false;
                        this.giftVoucherTransactionToolStripMenuItem.Visible = false;
                        break;
                        
                    case "RptPendingPurchaseOrders":
                        this.pendingPurchaseOrdersToolStripMenuItem.Visible = false;
                        break;

                    case "RptStockAdjustmentPercentage":
                        this.stockAdjustmentSToolStripMenuItem.Visible = false;
                        break;

                    case "RptExcessStockAdjustment":
                        this.excessStockToolStripMenuItem.Visible = false;
                        break;

                    case "RptShortageStockAdjustment":
                        this.shortageStockToolStripMenuItem.Visible = false;
                        break;

                    case "RptPriceChange":
                        this.productPriceChangeDetailToolStripMenuItem.Visible = false;                        
                        break;

                    case "RptProductPriceChangeDetail":
                        this.priceChangeDetailToolStripMenuItem.Visible = false;
                        break;

                    case "LgsRptTransferRegister":
                        this.productTransferRegisterToolStripMenuItem1.Visible = false;
                        break;

                    case "LgsRptStockBalance":
                        this.stockBalancesReportToolStripMenuItem1.Visible = false;
                        break;

                    case "RptSalesRegister": // Sales Register Report : 8003
                        this.mnuSalesRegister.Visible = false;
                        this.reportGeneratorToolStripMenuItem.Visible = false;
                        
                        
                        break;

                    case "RptLocationSales": // Location Sales Report : 8005
                        this.mnuLocationSales.Visible = false;
                        break;
                       
                    case "RptReceiptsRegister":
                        this.receiptsReportsToolStripMenuItem.Visible = false;
                        this.receiptsRegisterToolStripMenuItem.Visible = false;
                        break;          

                    case "RptStaffCreditSales": // Staff Credit Sales Report : 17010
                        this.staffCreditSalesToolStripMenuItem.Visible = false;
                        this.staffPurchaseDetailsToolStripMenuItem1.Visible = false;                        
                        break;

                    case "RptStaffSalaryAdvance": // Staff Salary Advance Report : 17011
                        this.staffSalaryAdvanceToolStripMenuItem.Visible = false;
                        break;

                    case "RptStaffCashCrediCard": // Staff Cash / CrediCard : 17012
                        this.staffCashCreditCardSalesToolStripMenuItem.Visible = false;
                        break;

                    case "RptPurchaseRegister": // Purchase Register Report : 9001
                        this.purchaseRegistryToolStripMenuItem.Visible = false;
                        this.supplierWiseToolStripMenuItem.Visible = false;
                        break;

                    case "InvRptOpeningBalanceRegister": // Inv Opening Balance Register : 10010
                        this.openingBalancesReportsToolStripMenuItem.Visible = false;
                        this.openingBalanceRegisterToolStripMenuItem.Visible = false;
                        break;

                    case "LgsRptOpeningBalanceRegister": // Lgs Opening Balance Register : 12010
                        this.openingBalanceRegisterToolStripMenuItem1.Visible = false;
                        this.openingBalancesReportsToolStripMenuItem1.Visible = false;
                        break;

                    case "InvRptStockBalance": //Stock Balance Report : 10011
                        this.stockBalancesReportToolStripMenuItem.Visible = true;
                        this.agingOfStockBatchWiseToolStripMenuItem.Visible = false;
                        this.batchStockBalancesReportToolStripMenuItem.Visible = false;
                        
                        this.givenDateStock.Visible = false;
                        this.ProductTransferRegisterToolStripMenuItem.Visible = false;
                        this.binCard.Visible = false;
                        break;

                    case "FrmBasketAnalysisReport":
                        this.mnuBasketAnalysisReport.Visible = false;
                        this.mnuSalesAnalysisReport.Visible = false;
                        break;

                    case "FrmCurrentSales": //Current Sales : 17009
                        this.currentSalesToolStripMenuItem.Visible = false;
                        break;

                    case "FrmPosSalesSummery": //Current Sales : 17008
                        this.salesSummeryToolStripMenuItem.Visible = false;
                        break;

                    case "RptExtendedPropertySalesRegister":
                        this.ExtendedPropertySalesRegisterToolStripMenuItem.Visible = false;
                        break;

                    case "RptExtendedPropertyStockBalance":
                        this.ExtendedPropertyStockBalanceToolStripMenuItem.Visible = false;
                        break;
                        
#endregion

                #region Tools

                    case "FrmManualPasswordChange":
                        this.rbnTbChangePassword.Enabled = true;
                        this.changePasswordToolStripMenuItem.Enabled = true;
                        break;

                    case "FrmUserPrivileges": // User Privileges :19
                        this.userPrivilegesToolStripMenuItem.Enabled = true;
                        this.rbnTbUserPrivilages.Enabled = true;
                        break;

                    case "FrmGroupPrivileges": // Group Privileges :22
                        this.groupPriToolStripMenuItem.Enabled = true;
                        this.rbnTbUserGroups.Enabled = true;
                        break;

                    case "FrmDataDownload":
                        this.dataDownloadToolStripMenuItem.Enabled = true;
                        break;

                    #endregion

                default:

                break;

                }

            }
 


            this.mnuCommon.Enabled = true;
            this.mnuInventory.Enabled = true;
 
            rbnPnlBtnSalesman.Enabled =true;
            rbnDeliverCheck.Enabled = true;
            rbnSalesOrderDelete.Enabled = true;
            ribBtnInvReDispatch.Enabled = true;
            //////this.rbnTbPosReference.Enabled = true;
            //////this.rbnTbPosConfig.Enabled = true;
            //////this.rbnTbPosRefCashier.Enabled = true;
            //////this.rbnTbPosConPrint.Enabled = true;
            //////this.rbnTbPosConKey.Enabled = true;
            //////this.rbnTbPosConSys.Enabled = true;

            mnuTools.Enabled = true;
            mnuReports.Enabled = true;
            
        }

        public void CheckHeadOfficeRights(bool isHeadOffice)
        {
            if (!isHeadOffice)
            {
               
            }
        }

        public void DisebleMenu()
        {
            this.mnuReports.Enabled = false;

            this.ribbtnLocation.Enabled = false;
            this.ribbtnSupplierGroup.Enabled = false;
            this.ribbtnCustomerGroup.Enabled = false;
            //this.rbnPnlBtnExPro.Enabled = false;
            //this.rbnPnlBtnExProVal.Enabled = false;
            this.rbntbEmployee.Enabled = false;

            this.ribbtnPayment.Enabled = false;
            this.ribbtnReceipt.Enabled = false;

            this.rbnPnlReference.Enabled = false;
            this.rbnPnlTransactions.Enabled = false;

            this.rbnPnlBtnDepartment.Enabled = false;
            this.rbnPnlBtnCategory.Enabled = false;
            this.rbnPnlBtnSubCategory.Enabled = false;
            this.rbnPnlBtnSubCategory2.Enabled = false;
            this.rbnPnlBtnProduct.Enabled = false;
            //this.rbnPnlBtnSalesman.Enabled = false;
            this.rbnPnlInvReference.Enabled = false;
            this.rbnPnlInvTransactions.Enabled = false;

            this.rbnTbChangePassword.Enabled = false;
            this.rbnTbUserGroups.Enabled = false;
            this.rbnTbUserPrivilages.Enabled = false;


            this.rbnPnlInvOs.Enabled = false;
            //this.rbnPnlInvPo.Enabled = false;
            this.rbnPnlInvSOrder.Enabled = false;
            this.rbnPnlInvDispatch.Enabled = false;
            this.rbnPnlInvInvoice.Enabled = false;
            //this.rbnPnlInvSaReturn.Enabled = false;
            //this.rbnPnlInvTog.Enabled = false;
            //this.rbnPnlInvSa.Enabled = false;
            //this.rbnPnlInvSO.Enabled = false;
            //this.rbnPnlInvBarcode.Visible = false;
            //this.rbnPnlInvQuotation.Visible = false;

            this.mnuCommon.Enabled = false;
            this.mnuInventory.Enabled = false;
           
            this.mnuTools.Enabled = false;

            #region non trading

            rbnPnlInvReference.Enabled = false;
            rbnPnlInvTransactions.Enabled = false;
            //this.rbnPnlLgsReference.Enabled = false;
            //this.rbnPnlLgsTransactions.Enabled = false;
            //this.rbnPnlBtnLgsSupplier.Enabled = false;
            //this.rbnPnlLgsPrn.Enabled = false;
            //this.rbnPnlBtnLgsDepartment.Enabled = false;
            //this.rbnPnlBtnLgsCategory.Enabled = false;
            //this.rbnPnlBtnLgsSubCategory.Enabled = false;
            //this.rbnPnlBtnLgsSubCategory2.Enabled = false;
            //this.rbnPnlBtnLgsProduct.Enabled = false;

            //rbnPnlLgsTOG.Enabled = false;
            //rbnPnlLgsPO.Enabled = false;
            //rbnPnlLgsGRN.Enabled = false;
            //rbnPnlLgsMAN.Enabled = false;
            //rbnPnlLgsMJR.Enabled = false;
            //rbnPnlLgsMJA.Enabled = false;
            //rbnPnlLgsMRN.Enabled = false;

            //this.rbnPnlLgsQuotation.Enabled = false;
            //this.rbnPnlLgsStockAdjustment.Enabled = false;

            #endregion

            #region Common

            this.mnuCommon.Enabled = false;

            //this.productExtendedPropertyToolStripMenuItem1.Enabled = false;
            this.costCentreToolStripMenuItem.Enabled = false;
            //this.productExtendedPropertyValuesToolStripMenuItem.Enabled = false;
            this.companyToolStripMenuItem.Enabled = false;
            this.locationToolStripMenuItem.Enabled = false;
            //this.areaToolStripMenuItem.Enabled = false;
            //this.territoryToolStripMenuItem.Enabled = false;
            this.unitOfMeasureToolStripMenuItem.Enabled = true;
            this.returnTypeToolStripMenuItem.Enabled = false;
            this.transferTypesToolStripMenuItem.Enabled = false;
            this.mnuComRefeCustomerGroup.Enabled = false;
            //this.extendedPropertiesToolStripMenuItem.Enabled = false;
            this.mnuComRefeSupplierGroup.Enabled = false;
            this.employeeToolStripMenuItem.Enabled = false;

            this.paymentMethodToolStripMenuItem.Enabled = false;
            //this.helperToolStripMenuItem.Enabled = false;

            this.supplierPaymentToolStripMenuItem.Enabled = false;
            this.customerReceiptToolStripMenuItem.Enabled = false;


            //this.sampleOutToolStripMenuItem.Enabled = false;
           // this.sampleInToolStripMenuItem.Enabled = false;
            //this.openingStockBalanceToolStripMenuItem.Enabled = false;

            //this.designationTypesToolStripMenuItem.Enabled = false;




            #endregion

            #region Inventory
            {
                this.mnuInventory.Enabled = false;
                this.productPriceChangeDamageToolStripMenuItem.Enabled = false;
               

                this.supplierToolStripMenuItem.Enabled = false;
                this.customerToolStripMenuItem.Enabled = false;
                this.mnuInvRefeDepartment.Enabled = false;
                this.mnuInvRefeCategory.Enabled = false;
                this.mnuInvRefeSubCategory.Enabled = false;
                this.mnuInvRefeSubCategory2.Enabled = false;
                this.mnuInvRefeProduct.Enabled = false;

                this.mnuInvRefeSalesPerson.Enabled = false;
                this.mnuInvRefePromotionMaster.Enabled = false;

                //this.purchaseOrderToolStripMenuItem.Enabled = false;
                //this.goodsReceivedNoteGRNToolStripMenuItem.Enabled = false;
                //this.purchaseReturnToolStripMenuItem.Enabled = false;

                //this.quotationToolStripMenuItem.Enabled = true;
                this.salesInvoiceToolStripMenuItem.Enabled = false;
                //this.salesReturnNoteToolStripMenuItem.Enabled = true;
                this.dispatchNoteToolStripMenuItem.Enabled = true;
                this.salesOrderToolStripMenuItem.Enabled = true;
                //this.transferOfGoodsTOGToolStripMenuItem.Enabled = false;
                //this.stockAdjustmentToolStripMenuItem.Enabled = false;
                //this.barCodePrintingToolStripMenuItem.Enabled = false;
                //this.productPriceChangeToolStripMenuItem.Enabled = false;



            }
            #endregion

            #region POS
            {
                //this.referenceToolStripMenuItem6.Enabled = false;
                //this.configurationsToolStripMenuItem.Enabled = false;
                //this.cashierToolStripMenuItem.Enabled = false;
                //this.printConfigurationsToolStripMenuItem.Enabled = false;
                //this.keyBoardConfigurationsToolStripMenuItem.Enabled = false;
                //this.systemConfigurationsToolStripMenuItem.Enabled = false;

                //this.mnuPos.Enabled = false;
                //this.rbnTbPosReference.Enabled = false;
                //this.rbnTbPosConfig.Enabled = false;
                //this.rbnTbPosRefCashier.Enabled = false;
                //this.rbnTbPosRefCashierGroup.Enabled = false;
                //this.rbnTbPosConPrint.Enabled = false;
                //this.rbnTbPosConKey.Enabled = false;
                //this.rbnTbPosConSys.Enabled = false;

                //this.cashierGroupsToolStripMenuItem.Enabled = false;

            }
            #endregion

            #region Non Trading

            {
                //#region Ribbon
                //this.rbnPnlBtnLgsDepartment.Enabled = false;
                //this.rbnPnlBtnLgsCategory.Enabled = false;
                //this.rbnPnlBtnLgsSubCategory.Enabled = false;
                //this.rbnPnlBtnLgsSubCategory2.Enabled = false;
                //this.rbnPnlBtnLgsProduct.Enabled = false;

                rbnPnlInvReference.Enabled = false;
                rbnPnlInvTransactions.Enabled = false;

                //rbnPnlBtnLgsDepartment.Enabled = false;
                //rbnPnlLgsTOG.Enabled = false;
                //rbnPnlLgsPO.Enabled = false;
                //rbnPnlLgsGRN.Enabled = false;
                //rbnPnlLgsMAN.Enabled = false;
                //rbnPnlLgsMJR.Enabled = false;
                //rbnPnlLgsMJA.Enabled = false;
                //rbnPnlLgsMRN.Enabled = false;


                #endregion

                //this.mnuNonTrading.Enabled = false;


                //this.mnuNtrRefeDepartment.Enabled = false;
                //this.mnuNtrRefeCategory.Enabled = false;
                //this.mnuNtrSubCategory.Enabled = false;
                //this.mnuNtrSubCategory2.Enabled = false;
                //this.mnuNtrRefeProduct.Enabled = false;
                //this.mnuNtrRefeSupplier.Enabled = false;


                //this.mnuLogisticpurchaseOrder.Enabled = false;

                //this.mnuLogisticPurchaseReturnNote.Enabled = false;
                //this.mnuLogisticGoodsReceivedNote.Enabled = false;
                //this.mnuSupplierQuotationEntry.Enabled = false;
                //this.mnuLogisticStockAdjustment.Enabled = false;

                //this.mnuLogisticTransferOfGoods.Enabled = false;


                //this.materialRequestNoteToolStripMenuItem.Enabled = false;
                //this.maintenanceJobRequisitionNoteToolStripMenuItem.Enabled = false;
                //this.maintenanceJobAssignNoteToolStripMenuItem.Enabled = false;
                //this.materialAllocationNoteToolStripMenuItem.Enabled = false;
                //this.materialConsumptionNoteToolStripMenuItem.Enabled = false;
                //this.serviceOutToolStripMenuItem.Enabled = false;
                //this.serviceInToolStripMenuItem.Enabled = false;
                //this.TransactionSummeyPanel.Enabled = false;
                //this.rbnPnlLgsTransactionPanel.Enabled = false;




            }
            //#endregion

            #region Account
            {
                //this.rbnPnlAccReference.Enabled = false;
                //this.rbnPnlAccTrans.Enabled = false;
                //this.rbnAccRefPettyCash.Enabled = false;
                //this.rbnAccTransReimbur.Enabled = false;
                //this.rbnAccTraBill.Enabled = false;

                //this.mnuAccounts.Enabled = false;

                //this.mnuAccRefePettyCash.Enabled = false;
                //this.linkedAccountToolStripMenuItem.Enabled = false;
                //this.pettyCashMasterCreationToolStripMenuItem.Enabled = false;

                //this.mnuAccTransPettyCash.Enabled = false;
                //this.pettyCashReimbursementToolStripMenuItem.Enabled = false;
                //this.pettyCashIOUToolStripMenuItem.Enabled = false;
                //this.pettyCashBillToolStripMenuItem.Enabled = false;
                //this.pettyCashPaymentToolStripMenuItem.Enabled = false;
                //this.pettyCashPaymentProcessToolStripMenuItem.Enabled = false;
            }
            #endregion

            #region Gift Voucher

            {
                //this.rbnPnlGiftVoucherReference.Enabled = false;
                //this.rbnPnlGiftVoucherTransaction.Enabled = false;

                //this.ribBtnGroup.Enabled = false;
                //this.ribBtnBookGeneration.Enabled = false;
                //this.ribBtnVoucherCreation.Enabled = false;

                //this.ribBtnGVPurchaseOrder.Enabled = false;
                //this.ribBtnGVGRN.Enabled = false;
                //this.ribBtnGVTransfer.Enabled = false;

                //this.mnuGiftVoucher.Enabled = false;


                //this.giftVoucherGroupToolStripMenuItem.Enabled = false;
                //this.giftVoucherBookGenerationToolStripMenuItem.Enabled = false;
                //this.giftVoucherVoucherCreationToolStripMenuItem.Enabled = false;

                //this.mnuGvrPurchaseOrder.Enabled = false;
                //this.mnuGvrGoodsReceivedNote.Enabled = false;
                //this.mnuGvrTransfer.Enabled = false;

            }
            #endregion

            #region Customer Relationship

            {
                //this.rbnTbCusRelationReference.Enabled = false;
                //this.rbnTbCusRelationTransactions.Enabled = false;

                //this.rbnTbCusRelationReferenceLoyaltyCustomer.Enabled = false;
                //this.rbnTbCusRelationReferenceCardTypes.Enabled = false;

                //this.rbnTbCusRelationTransactionsCardIssue.Enabled = false;
                //this.rbnTbCusRelationTransactionsLostAndRenew.Enabled = false;
                //this.rbnTbCusRelationTransactionsCustomerFeedback.Enabled = false;

                //this.mnuCrm.Enabled = false;

                //this.loyaltyCustomerToolStripMenuItem.Enabled = false;
                //this.cardTypeToolStripMenuItem.Enabled = false;
                //this.cardNoGeToolStripMenuItem.Enabled = false;

                //this.cardIssueToolStripMenuItem.Enabled = false;
                //this.lostReNewToolStripMenuItem.Enabled = false;
                //this.customerFeedBackToolStripMenuItem.Enabled = false;

                //this.manualPointsAddToolStripMenuItem.Enabled = false;
                //this.ManualPoinsAdd.Enabled = false;
                this.CardIssuedDetails.Enabled = false;
                this.CardUsageDetails.Enabled = false;
                this.CashierPerformances.Enabled = false;
                this.mnuCrmRegister.Enabled = false;

            }
            #endregion

            #region Reports

            {
                this.mnuSalesRegister.Enabled = false;
                this.mnuLocationSales.Enabled = false;
                this.staffCreditSalesToolStripMenuItem.Enabled = false;
                this.staffSalaryAdvanceToolStripMenuItem.Enabled = false;
                this.staffCashCreditCardSalesToolStripMenuItem.Enabled = false;
                this.purchaseRegistryToolStripMenuItem.Enabled = false;
                this.excessStockToolStripMenuItem.Enabled = false;
                this.shortageStockToolStripMenuItem.Enabled = false;
                this.giftVoucherIssuesToolStripMenuItem.Enabled = false;
                this.openingBalanceRegisterToolStripMenuItem.Enabled = true;
                this.openingBalanceRegisterToolStripMenuItem1.Enabled = true;
                this.stockBalancesReportToolStripMenuItem.Enabled = true;
            }
            #endregion

            #region xxx

            //common
            //this.vehicleToolStripMenuItem.Enabled = false;

            //Inventory and Sale, transaction
            //this.productPriceChangeToolStripMenuItem1.Enabled = true;

            //Report ,Common , reference
            this.locationDetailsToolStripMenuItem1.Enabled = false;
            this.vehicleDetailsToolStripMenuItem1.Enabled = false;
            this.driverDetailsToolStripMenuItem1.Enabled = false;
            this.helperDetailsToolStripMenuItem.Enabled = false;
            this.paymentMethodDetailsToolStripMenuItem1.Enabled = false;

            
            //Report , inventory and sale,reference
            this.supplierDetailsToolStripMenuItem2.Enabled = false;
            this.departmentDetailsToolStripMenuItem2.Enabled = false;
            this.productDetailsToolStripMenuItem2.Enabled = false;
            this.categoryDetailsToolStripMenuItem2.Enabled = false;
            this.subCategoryDetailsToolStripMenuItem2.Enabled = false;
            this.subCategory2DetailsToolStripMenuItem2.Enabled = false;
            //this.binCard.Enabled = true;

            //Report , inventory and sale,Purchasing Report
            this.pendingPurchaseOrdersToolStripMenuItem.Enabled = false;
            this.supplierWiseToolStripMenuItem.Enabled = false;

            //Report , inventory and sale,Stock Report
            this.stockAdjustmentSToolStripMenuItem.Enabled = false;
            this.agingOfStockBatchWiseToolStripMenuItem.Enabled = false;
            this.batchStockBalancesReportToolStripMenuItem.Enabled = false;
            this.ExtendedPropertyStockBalanceToolStripMenuItem.Enabled = false;
            this.givenDateStock.Enabled = false;
            this.ProductTransferRegisterToolStripMenuItem.Enabled = false;

            //Report , inventory and sale,Transaction Inquiry
            this.purchaseOrderToolStripMenuItem2.Enabled = false;
            this.goodReceiveNoteToolStripMenuItem.Enabled = false;
            this.purchaseReturnNotePRNToolStripMenuItem1.Enabled = false;
            this.transferOfGoodsTOGToolStripMenuItem2.Enabled = false;
            this.stockAdjustmentToolStripMenuItem2.Enabled = false;
            this.productPriceChangeToolStripMenuItem2.Enabled = false;
            this.dispatchNoteToolStripMenuItem1.Enabled = false;
            this.salesOrderToolStripMenuItem1.Enabled = false;
            this.sampleOutToolStripMenuItem2.Enabled = false;
            this.sampleInToolStripMenuItem2.Enabled = false;

            //Report , inventory and sale,Other
            this.productPriceChangeDetailToolStripMenuItem.Enabled = false;
            this.priceChangeDetailToolStripMenuItem.Enabled = false;

            //Report , inventory and sale
            this.openingBalancesReportsToolStripMenuItem.Enabled = false;
            this.openingBalanceRegisterToolStripMenuItem.Enabled = false;

            //Report , inventory and sale
            this.reportGeneratorToolStripMenuItem.Enabled = false;

            //Report,Point Of sale
            this.referenceToolStripMenuItem1.Enabled = false;

            //Report,Point Of sale, transaction.Enable = false;
            this.salesSummeryToolStripMenuItem.Enabled = false;
            this.currentSalesToolStripMenuItem.Enabled = false;

            //Report, Non Trading, Reference
            this.supplierDetailsToolStripMenuItem.Enabled = false;
            this.departmentDetailsToolStripMenuItem1.Enabled = false;
            this.categoryDetailsToolStripMenuItem1.Enabled = false;
            this.subCategoryDetailsToolStripMenuItem1.Enabled = false;
            this.subCategory2DetailsToolStripMenuItem1.Enabled = false;
            this.productDetailsToolStripMenuItem1.Enabled = false;


            //Report, Non Trading, Transaction Inquiry
            this.toolStripMenuItem5.Enabled = false;
            this.toolStripMenuItem8.Enabled = false;
            this.toolStripMenuItem9.Enabled = false;
            this.toolStripMenuItem10.Enabled = false;
            this.toolStripMenuItem11.Enabled = false;
            this.toolStripMenuItem12.Enabled = false;
            this.mnuMaintenanceJobRequisitionNote.Enabled = false;
            this.mnuMaintenanceJobAssignNote.Enabled = false;
            this.mnuMaterialAllocationNote.Enabled = false;
            this.mnuMaterialRequestNote.Enabled = false;

            //Report, Non Trading
            

            //Report, Non Trading, Stock Reports
            this.stockBalancesReportToolStripMenuItem1.Enabled = false;
            this.stockMovementDetailsToolStripMenuItem.Enabled = false;
            this.productTransferRegisterToolStripMenuItem1.Enabled = false;

            //Report, Accounts, Transaction Inquiry
            this.pettyCashReimbursementToolStripMenuItem1.Enabled = false;
            this.pettyCashIOUToolStripMenuItem1.Enabled = false;
            this.pettyCashBillToolStripMenuItem1.Enabled = false;
            this.pettyCashPaymentToolStripMenuItem1.Enabled = false;

            //Report, Accounts, Receipts Reports
            //this.receiptsReportsToolStripMenuItem.Enabled = false;
            this.receiptsRegisterToolStripMenuItem.Enabled = false;
            //Report,Gift Voucher
            //this.giftVoucherTransactionToolStripMenuItem.Enabled = false;
            this.giftVoucherIssuesToolStripMenuItem.Enabled = false;

            //Report,Gift Voucher,Transaction Inquiry

            this.mnuGvPurchaseOrderToolStripMenuItem.Enabled = false;
            this.mnuGvGoodReceiveNoteGRNToolStripMenuItem.Enabled = false;
            this.mnuGvTransferOfGiftVouchersTOGToolStripMenuItem.Enabled = false;

            //Report, Staff Purchase Details

            this.staffPurchaseDetailsToolStripMenuItem1.Enabled = false;
            this.staffCashCreditCardSalesToolStripMenuItem.Enabled = false;
            this.staffCreditSalesToolStripMenuItem.Enabled = false;
            this.staffSalaryAdvanceToolStripMenuItem.Enabled = false;

            //Report, Customer Relation, References
            this.customerAddressToolStripMenuItem.Enabled = false;
            this.branchWiseCustomerDetailsToolStripMenuItem1.Enabled = false;
            this.inactiveCustomerDetailsToolStripMenuItem1.Enabled = false;

            //Report, Customer Relation, Transaction 
            this.ManuallyAddedpointsdetails.Enabled = false;
            this.LocationWiseArapaima_ArrowanaSummery.Enabled = false;
            this.CustomerDetails.Enabled = false;
            this.loyaltyAnalysisToolStripMenuItem.Enabled = false;
            this.locationWiseLoToolStripMenuItem.Enabled = false;
            this.GoldCardSelection.Enabled = false;
            this.CustomerVisitDetails.Enabled = false;
            this.TopCustomerPerformances.Enabled = false;

            //Tool
            this.groupPriToolStripMenuItem.Enabled = false;
            this.userPrivilegesToolStripMenuItem.Enabled = false;
            this.changePasswordToolStripMenuItem.Enabled = false;
            this.dataDownloadToolStripMenuItem.Enabled = false;

            //Report, Inventory and Sale, Reference,Sales Reports
            this.ExtendedPropertySalesRegisterToolStripMenuItem.Enabled = false;
            this.mnuBasketAnalysisReport.Enabled = false;
            this.mnuSalesAnalysisReport.Enabled = false;

            #endregion
        }

        private void MdiMainRibbon_Activated(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (menuStrip.Visible == true) { menuStrip.Visible = false; }
            else { menuStrip.Visible = true; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.ExitSystem).Equals(DialogResult.Yes))
                {
                    this.Close();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.ExitSystem).Equals(DialogResult.Yes))
                {
                    this.Close();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLoginUser frmLoginUser = new FrmLoginUser(this);
            Common.SetMenu(frmLoginUser, this);
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            this.MdiChildren.OfType<Form>().ToList().ForEach(x => x.Close());

            DisebleMenu();
            btnLogOff.Enabled = false;
            btnLogin.Enabled = true;

            FrmLoginUser frmLoginUser = new FrmLoginUser(this);
            Common.SetMenu(frmLoginUser, this);
        }

        private void mnuInvRefeDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            Common.SetMenu(frmDepartment, this);
        }

        private void mnuInvRefeCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frmCategory = new FrmCategory();
            Common.SetMenu(frmCategory, this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = System.DateTime.Now.ToString();
            this.digitalDisplayControl1.DigitText = DateTime.Now.ToString("HH:mm:ss");
        }


        private void mnuLogisticPurchaseOrder_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseOrder frmLogisticPurchaseOrder = new FrmLogisticPurchaseOrder();
            Common.SetMenu(frmLogisticPurchaseOrder, this);
        }

        private void rbnPnlLgsTOG_Click(object sender, EventArgs e)
        {
            FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
            Common.SetMenu(frmLogisticTransferOfGoodsNote, this);
        }

        private void rbnPnlLgsPO_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseOrder frmLogisticPurchaseOrder = new FrmLogisticPurchaseOrder();
            Common.SetMenu(frmLogisticPurchaseOrder, this);
        }


        private void mnuSupplierQuotationEntry_Click(object sender, EventArgs e)
        {
            FrmLogisticQuotation frmLogisticQuotation = new FrmLogisticQuotation();
            Common.SetMenu(frmLogisticQuotation, this);
        }

        private void rbnPnlLgsQuotation_Click(object sender, EventArgs e)
        {
            FrmLogisticQuotation frmLogisticQuotation = new FrmLogisticQuotation();
            Common.SetMenu(frmLogisticQuotation, this);
        }

        private void ribBtnGroup_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGroup frmGiftVoucherGroup = new FrmGiftVoucherGroup();
            Common.SetMenu(frmGiftVoucherGroup, this);
        }

        private void ribBtnBookGeneration_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherBookCodeGeneration frmGiftVoucherBookCodeGeneration = new FrmGiftVoucherBookCodeGeneration();
            Common.SetMenu(frmGiftVoucherBookCodeGeneration, this);
        }

        private void ribBtnVoucherCreation_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherMaster frmGiftVoucherMaster = new FrmGiftVoucherMaster();
            Common.SetMenu(frmGiftVoucherMaster, this);
        }

        private void ribBtnGVPurchaseOrder_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherPurchaseOrder frmGiftVoucherPurchaseOrder = new FrmGiftVoucherPurchaseOrder();
            Common.SetMenu(frmGiftVoucherPurchaseOrder, this);
        }

        private void ribBtnGVGRN_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGoodsReceivedNote frmGiftVoucherGoodsReceivedNote = new FrmGiftVoucherGoodsReceivedNote();
            Common.SetMenu(frmGiftVoucherGoodsReceivedNote, this);
        }

        private void ribBtnGVTramsaction_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherTransfer frmGiftVoucherTransfer = new FrmGiftVoucherTransfer();
            Common.SetMenu(frmGiftVoucherTransfer, this);
        }

        private void ribBtnGVTransfer_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherTransfer frmGiftVoucherTransfer = new FrmGiftVoucherTransfer();
            Common.SetMenu(frmGiftVoucherTransfer, this);
        }

        private void priceChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductPriceChange frmProductPriceChange = new FrmProductPriceChange();
            Common.SetMenu(frmProductPriceChange, this);
        }

        private void mnuInvRefePromotionMaster_Click(object sender, EventArgs e)
        {
            FrmPromotionMaster frmPromotionMaster = new FrmPromotionMaster();
            Common.SetMenu(frmPromotionMaster, this);
        }        

        private void mnuLogisticTransferOfGoods_Click(object sender, EventArgs e)
        {
            FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
            Common.SetMenu(frmLogisticTransferOfGoodsNote, this);
        }

        private void rbnPnlLgsGRN_Click(object sender, EventArgs e)
        {
            FrmLogisticGoodsReceivedNote frmLogisticGoodsReceivedNote = new FrmLogisticGoodsReceivedNote();
            Common.SetMenu(frmLogisticGoodsReceivedNote, this);
        }

        private void rbnPnlLgsMAN_Click(object sender, EventArgs e)
        {
            FrmMaterialAllocationNote frmMaterialAllocationNote = new FrmMaterialAllocationNote();
            Common.SetMenu(frmMaterialAllocationNote, this);
        }

        private void pettyCashReimbursementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashReimbursement frmPettyCashReimbursement = new FrmPettyCashReimbursement();
            Common.SetMenu(frmPettyCashReimbursement, this);
        }

        private void pettyCashIOUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashIOU frmPettyCashIOU = new FrmPettyCashIOU();
            Common.SetMenu(frmPettyCashIOU, this);
        }

        private void pettyCashBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashBillEntry frmPettyCashBillEntry = new FrmPettyCashBillEntry();
            Common.SetMenu(frmPettyCashBillEntry, this);
        }

        private void pettyCashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashPayment frmPettyCashPayment = new FrmPettyCashPayment();
            Common.SetMenu(frmPettyCashPayment, this);
        }

        private void pettyCashPaymentProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashPaymentUpdate frmPettyCashPaymentUpdate = new FrmPettyCashPaymentUpdate();
            Common.SetMenu(frmPettyCashPaymentUpdate, this);
        }

        private void pettyCashMasterCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashMasterCreation frmPettyCashMasterCreation = new FrmPettyCashMasterCreation();
            Common.SetMenu(frmPettyCashMasterCreation, this);
        }

        private void rbnPnlLgsMJR_Click(object sender, EventArgs e)
        {
            FrmMaintenanceJobRequisitionNote frmMaintenanceJobRequisitionNote = new FrmMaintenanceJobRequisitionNote();
            Common.SetMenu(frmMaintenanceJobRequisitionNote, this);
        }


        private void rbnPnlLgsMJA_Click(object sender, EventArgs e)
        {
            FrmMaintenanceJobAssignNote frmMaintenanceJobAssignNote = new FrmMaintenanceJobAssignNote();
            Common.SetMenu(frmMaintenanceJobAssignNote, this);
        }

        private void rbnPnlLgsMRN_Click(object sender, EventArgs e)
        {
            FrmMaterialRequestNote frmMaterialRequestNote = new FrmMaterialRequestNote();
            Common.SetMenu(frmMaterialRequestNote, this);
        }

        private void mnuLogisticGoodsReceivedNote_Click(object sender, EventArgs e)
        {
            FrmLogisticGoodsReceivedNote frmLogisticGoodsReceivedNote = new FrmLogisticGoodsReceivedNote();
            Common.SetMenu(frmLogisticGoodsReceivedNote, this);
        }

        private void mnuLogisticPurchaseReturnNote_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
            Common.SetMenu(frmLogisticPurchaseReturnNote, this);
        }

        private void mnuLogisticServiceOut_Click(object sender, EventArgs e)
        {
            FrmServiceOut frmServiceOut = new FrmServiceOut();
            Common.SetMenu(frmServiceOut, this);
        }

        private void mnuLogisticServiceIn_Click(object sender, EventArgs e)
        {
            FrmServiceIn frmServiceIn = new FrmServiceIn();
            Common.SetMenu(frmServiceIn, this);
        }

        private void mnuLogisticStockAdjustment_Click(object sender, EventArgs e)
        {
            FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
            Common.SetMenu(frmLogisticStockAdjustment, this);
        }

        private void loyaltyCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLoyaltyCustomer frmLoyaltyCustomer = new FrmLoyaltyCustomer();
            Common.SetMenu(frmLoyaltyCustomer, this);
        }

        private void cardTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardMaster frmCardmaster = new FrmCardMaster();
            Common.SetMenu(frmCardmaster, this);
        }

        private void customerFeedBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomerFeedBack frmCustomerFeedBack = new FrmCustomerFeedBack();
            Common.SetMenu(frmCustomerFeedBack, this);
        }

        private void lostReNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLostAndRenew frmLostAndRenew = new FrmLostAndRenew();
            Common.SetMenu(frmLostAndRenew, this);
        }

        private void cardIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardIssue frmCardIssue = new FrmCardIssue();
            Common.SetMenu(frmCardIssue, this);
        }

        private void cardNoGeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardNoGeneration frmCardNoGeneration = new FrmCardNoGeneration();
            Common.SetMenu(frmCardNoGeneration, this);
        }

        private void customerFeedbackToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmCustomerFeedBack frmCustomerFeedBack = new FrmCustomerFeedBack();
            Common.SetMenu(frmCustomerFeedBack, this);
        }

        private void dispatchNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDispatchNote frmDispatchNote = new FrmDispatchNote();
            Common.SetMenu(frmDispatchNote, this);
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInvoice frmInvoice = new FrmInvoice();
            Common.SetMenu(frmInvoice, this);
        }

        private void salesReturnNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalesReturnNote frmSalesReturnNote = new FrmSalesReturnNote();
            Common.SetMenu(frmSalesReturnNote, this);
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalesOrder frmSalesOrder = new FrmSalesOrder();
            Common.SetMenu(frmSalesOrder, this);
        }

        private void quotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmQuotation frmQuotation = new FrmQuotation();
            Common.SetMenu(frmQuotation, this);
        }

        private void mnuInvRefeExtendedPropertyValue_Click(object sender, EventArgs e)
        {
            FrmProductExtendedValue frmProductExtendedValue = new FrmProductExtendedValue();
            Common.SetMenu(frmProductExtendedValue, this);
        }

        private void rbnPnlBtnLgsDepartment_Click(object sender, EventArgs e)
        {
            FrmLogisticDepartment frmLogisticDepartment = new FrmLogisticDepartment();
            Common.SetMenu(frmLogisticDepartment, this);
        }

        private void rbnPnlBtnLgsCategory_Click(object sender, EventArgs e)
        {
            FrmLogisticCategory frmLogisticCategory = new FrmLogisticCategory();
            Common.SetMenu(frmLogisticCategory, this);
        }

        private void rbnPnlBtnLgsSubCategory_Click(object sender, EventArgs e)
        {
            FrmLogisticSubCategory frmLogisticSubCategory = new FrmLogisticSubCategory();
            Common.SetMenu(frmLogisticSubCategory, this);
        }

        private void rbnPnlBtnLgsSubCategory2_Click(object sender, EventArgs e)
        {
            FrmLogisticSubCategory2 frmLogisticSubCategory2 = new FrmLogisticSubCategory2();
            Common.SetMenu(frmLogisticSubCategory2, this);
        }

        private void rbnPnlBtnLgsProduct_Click(object sender, EventArgs e)
        {
            FrmLogisticProduct frmLogisticProduct = new FrmLogisticProduct();
            Common.SetMenu(frmLogisticProduct, this);
        }

        private void mnuNtrRefeDepartment_Click(object sender, EventArgs e)
        {
            FrmLogisticDepartment frmLogisticDepartment = new FrmLogisticDepartment();
            Common.SetMenu(frmLogisticDepartment, this);
        }

        private void mnuNtrRefeCategory_Click(object sender, EventArgs e)
        {
            FrmLogisticCategory frmLogisticCategory = new FrmLogisticCategory();
            Common.SetMenu(frmLogisticCategory, this);
        }

        private void mnuNtrSubCategory_Click(object sender, EventArgs e) 
        {
            FrmLogisticSubCategory frmLogisticSubCategory = new FrmLogisticSubCategory();
            Common.SetMenu(frmLogisticSubCategory, this);
        }

        private void mnuNtrSubCategory2_Click(object sender, EventArgs e)
        {
            FrmLogisticSubCategory2 frmLogisticSubCategory2 = new FrmLogisticSubCategory2();
            Common.SetMenu(frmLogisticSubCategory2, this);
        }

        private void mnuNtrRefeProduct_Click(object sender, EventArgs e)
        {
            FrmLogisticProduct frmLogisticProduct = new FrmLogisticProduct();
            Common.SetMenu(frmLogisticProduct, this);
        }

        private void ribbtnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomerGroup frmCustomerGroup = new FrmCustomerGroup();
            Common.SetMenu(frmCustomerGroup, this);
        }

        private void mnuNtrRefeSupplier_Click(object sender, EventArgs e)
        {
            FrmLogisticSupplier frmLogisticSupplier = new FrmLogisticSupplier();
            Common.SetMenu(frmLogisticSupplier, this);
        }

        private void rbnPnlLgsStockAdjustment_Click(object sender, EventArgs e)
        {
            FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
            Common.SetMenu(frmLogisticStockAdjustment, this);
        }

       private void mnuComReference_Click(object sender, EventArgs e)
        {

        }
        
        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLocation frmLocation = new FrmLocation();
            Common.SetMenu(frmLocation, this);
        }

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArea frmArea = new FrmArea();
            Common.SetMenu(frmArea, this);
        }

        private void territoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTerritory frmTerritory = new FrmTerritory();
            Common.SetMenu(frmTerritory, this);
        }

        private void unitOfMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUnitOfMeasure frmUnitOfMeasure = new FrmUnitOfMeasure();
            Common.SetMenu(frmUnitOfMeasure, this);
        }

        private void returnTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTypes frmTypes = new FrmTypes();
            Common.SetMenu(frmTypes, this);
        }

        private void transferTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTypes frmTypes = new FrmTypes();
            Common.SetMenu(frmTypes, this);
        }

        private void paymentMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentMethod frmPaymentMethod = new FrmPaymentMethod();
            Common.SetMenu(frmPaymentMethod, this);
        }

        private void vehicleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmVehicle frmvehicle = new FrmVehicle();
            Common.SetMenu(frmvehicle, this);
        }

        private void helperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHelper frmHelper = new FrmHelper();
            Common.SetMenu(frmHelper, this);
        }

        private void giftVoucherGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGroup frmGiftVoucherGroup = new FrmGiftVoucherGroup();
            Common.SetMenu(frmGiftVoucherGroup, this);
        }

        private void giftVoucherBookGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherBookCodeGeneration frmGiftVoucherBookCodeGeneration = new FrmGiftVoucherBookCodeGeneration();
            Common.SetMenu(frmGiftVoucherBookCodeGeneration, this);
        }

        private void giftVoucherVoucherCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherMaster frmGiftVoucherMaster = new FrmGiftVoucherMaster();
            Common.SetMenu(frmGiftVoucherMaster, this);
        }

        private void sampleOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSampleOut frmSampleOut = new FrmSampleOut();
            Common.SetMenu(frmSampleOut, this);
        }

        private void sampleInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSampleIn frmSampleIn = new FrmSampleIn();
            Common.SetMenu(frmSampleIn, this);
        }

        private void openingStockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
            Common.SetMenu(frmOpeningStock, this);
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
            Common.SetMenu(frmStockAdjustment, this);
        }

        private void barCodePrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBarcode FrmBarcode = new FrmBarcode();
            Common.SetMenu(FrmBarcode, this);
        }

        private void productPriceChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductPriceChange frmProductPriceChange = new FrmProductPriceChange();
            Common.SetMenu(frmProductPriceChange, this);
        }

        private void materialRequestNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaterialRequestNote frmMaterialRequestNote = new FrmMaterialRequestNote();
            Common.SetMenu(frmMaterialRequestNote, this);
        }

        private void maintenanceJobRequisitionNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaintenanceJobRequisitionNote frmMaintenanceJobRequisitionNote = new FrmMaintenanceJobRequisitionNote();
            Common.SetMenu(frmMaintenanceJobRequisitionNote, this);
        }

        private void maintenanceJobAssignNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaintenanceJobAssignNote frmMaintenanceJobAssignNote = new FrmMaintenanceJobAssignNote();
            Common.SetMenu(frmMaintenanceJobAssignNote, this);
        }

        private void materialAllocationNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaterialAllocationNote frmMaterialAllocationNote = new FrmMaterialAllocationNote();
            Common.SetMenu(frmMaterialAllocationNote, this);
        }

        private void materialConsumptionNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaterialConsumptionNote frmMaterialConsumptionNote = new FrmMaterialConsumptionNote();
            Common.SetMenu(frmMaterialConsumptionNote, this);
        }

        private void serviceOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmServiceOut frmServiceOut = new FrmServiceOut();
            Common.SetMenu(frmServiceOut, this);
        }

        private void serviceInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmServiceIn frmServiceIn = new FrmServiceIn();
            Common.SetMenu(frmServiceIn, this);
        }

        private void mnuGvrPurchaseOrder_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherPurchaseOrder frmGiftVoucherPurchaseOrder = new FrmGiftVoucherPurchaseOrder();
            Common.SetMenu(frmGiftVoucherPurchaseOrder, this);
        }

        private void mnuGvrGoodsReceivedNote_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGoodsReceivedNote frmGiftVoucherGoodsReceivedNote = new FrmGiftVoucherGoodsReceivedNote();
            Common.SetMenu(frmGiftVoucherGoodsReceivedNote, this);
        }

        private void mnuGvrTransfer_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherTransfer frmGiftVoucherTransfer = new FrmGiftVoucherTransfer();
            Common.SetMenu(frmGiftVoucherTransfer, this);
        }

        private void goodsReceivedNoteGRNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGoodsReceivedNote frmGiftVoucherGoodsReceivedNote = new FrmGiftVoucherGoodsReceivedNote();
            Common.SetMenu(frmGiftVoucherGoodsReceivedNote, this);
        }

        private void cardAllocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardAllocation frmCardAllocation = new FrmCardAllocation();
            Common.SetMenu(frmCardAllocation, this);
        }

        private void productExtendedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductExtendedProperty frmProductExtendedProperty = new FrmProductExtendedProperty();
            Common.SetMenu(frmProductExtendedProperty, this);
        }

        private void productExtendedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductExtendedValue frmProductExtendedValue = new FrmProductExtendedValue();
            Common.SetMenu(frmProductExtendedValue, this);
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExchangeRate frmSize = new FrmExchangeRate();
            Common.SetMenu(frmSize, this);
        }

        private void mnuComRefeCompany_Click(object sender, EventArgs e)
        {

        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCompany frmCompany = new FrmCompany();
            Common.SetMenu(frmCompany, this);

        }

        private void mnuInvRefeSubCategory2_Click(object sender, EventArgs e)
        {
            FrmSubCategory2 frmSubCategory2 = new FrmSubCategory2();
            Common.SetMenu(frmSubCategory2, this);

        }

        private void mnuInvRefeProduct_Click(object sender, EventArgs e)
        {
            FrmProduct frmProduct = new FrmProduct();
            Common.SetMenu(frmProduct, this);
        }

        private void mnuGvrTransactions_Click(object sender, EventArgs e)
        {

        }

        private void mnuComTransactions_Click(object sender, EventArgs e)
        {

        }

        private void mnuInvRefeSubCategory_Click(object sender, EventArgs e)
        {
            FrmSubCategory frmSubCategory = new FrmSubCategory();
            Common.SetMenu(frmSubCategory, this);
        }

        private void mnuInvRefeExtendedProperty_Click(object sender, EventArgs e)
        {

        }

        private void mnuInvRefeSalesPerson_Click(object sender, EventArgs e)
        {
            FrmSalesPerson frmSalesPerson = new FrmSalesPerson();
            Common.SetMenu(frmSalesPerson, this);
        }

        private void mnuComRefeCustomerGroup_Click(object sender, EventArgs e)
        {
            FrmCustomerGroup frmCustomerGroup = new FrmCustomerGroup();
            Common.SetMenu(frmCustomerGroup, this);
        }

        private void mnuComRefeCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            Common.SetMenu(frmCustomer, this);
        }

        private void mnuComRefeSupplierGroup_Click(object sender, EventArgs e)
        {
            FrmSupplierGroup frmSupplierGroup = new FrmSupplierGroup();
            Common.SetMenu(frmSupplierGroup, this);
        }

        private void mnuComRefeSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplier frmSupplier = new FrmSupplier();
            Common.SetMenu(frmSupplier, this);
        }

        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDriver frmDriver = new FrmDriver();
            Common.SetMenu(frmDriver, this);
        }

        private void supplierPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mnuComReports_Click(object sender, EventArgs e)
        {
            
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmColour frmColour = new FrmColour();
            Common.SetMenu(frmColour, this);

        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPurchaseOrder frmPurchaseOrder = new FrmPurchaseOrder();
            Common.SetMenu(frmPurchaseOrder, this);

        }

        private void goodsReceivedNoteGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGoodsReceivedNote frmGoodsReceivedNote = new FrmGoodsReceivedNote();
            Common.SetMenu(frmGoodsReceivedNote, this);
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
            Common.SetMenu(frmPurchaseReturnNote, this);
        }

        private void cardNoGeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmCardNoGeneration frmCardNoGeneration = new FrmCardNoGeneration();
            Common.SetMenu(frmCardNoGeneration, this);
        }

        private void customerFeedBackToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmCustomerFeedBack frmCustomerFeedBack = new FrmCustomerFeedBack();
            Common.SetMenu(frmCustomerFeedBack, this);

        }

        private void locationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLocation");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSupplier");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void vehicleDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmVehicle");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void driverDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDriver");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void helperEtailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmHelper");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCompany");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnPnlBtnSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplier frmSupplier = new FrmSupplier();
            Common.SetMenu(frmSupplier, this);
        }

        private void rbnPnlBtnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            Common.SetMenu(frmCustomer, this);
        }

        private void ribbtnLocation_Click(object sender, EventArgs e)
        {
            FrmLocation frmLocation = new FrmLocation();
            Common.SetMenu(frmLocation, this);
        }

        private void rbnPnlBtnExPro_Click_1(object sender, EventArgs e)
        {
            FrmProductExtendedProperty frmProductExtendedProperty = new FrmProductExtendedProperty();
            Common.SetMenu(frmProductExtendedProperty, this);
        }

        private void rbnPnlBtnExProVal_Click(object sender, EventArgs e)
        {
            FrmProductExtendedValue frmProductExtendedValue = new FrmProductExtendedValue();
            Common.SetMenu(frmProductExtendedValue, this);
        }

        private void rbnPnlInvOs_Click_1(object sender, EventArgs e)
        {
            FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
            Common.SetMenu(frmOpeningStock, this);
        }

        private void ribbtnPayment_Click(object sender, EventArgs e)
        {
            FrmPayment frmPayment = new FrmPayment();
            Common.SetMenu(frmPayment, this);
        }

        private void rbnAccRefPettyCash_Click(object sender, EventArgs e)
        {
            FrmPettyCashMasterCreation frmPettyCashMasterCreation = new FrmPettyCashMasterCreation();
            Common.SetMenu(frmPettyCashMasterCreation, this);
        }

        private void rbnAccTransReimbur_Click(object sender, EventArgs e)
        {
            FrmPettyCashReimbursement frmPettyCashReimbursement = new FrmPettyCashReimbursement();
            Common.SetMenu(frmPettyCashReimbursement, this);
        }

        private void rbnAccTraBill_Click(object sender, EventArgs e)
        {
            FrmPettyCashBillEntry frmPettyCashBillEntry = new FrmPettyCashBillEntry();
            Common.SetMenu(frmPettyCashBillEntry, this);
        }

        private void rbnTbCusRelationReferenceLoyaltyCustomer_Click(object sender, EventArgs e)
        {
            FrmLoyaltyCustomer frmLoyaltyCustomer = new FrmLoyaltyCustomer();
            Common.SetMenu(frmLoyaltyCustomer, this);
        }

        private void rbnTbCusRelationReferenceCardTypes_Click(object sender, EventArgs e)
        {
            FrmCardMaster frmCardMaster = new FrmCardMaster();
            Common.SetMenu(frmCardMaster, this);
        }

        private void rbnTbCusRelationTransactionsCardIssue_Click(object sender, EventArgs e)
        {
            FrmCardIssue frmCardIssue = new FrmCardIssue();
            Common.SetMenu(frmCardIssue, this);
        }

        private void rbnTbCusRelationTransactionsLostAndRenew_Click(object sender, EventArgs e)
        {
            FrmLostAndRenew frmLostAndRenew = new FrmLostAndRenew();
            Common.SetMenu(frmLostAndRenew, this);
        }

        private void rbnTbCusRelationTransactionsCustomerFeedback_Click(object sender, EventArgs e)
        {
            FrmCustomerFeedBack frmCustomerFeedBack = new FrmCustomerFeedBack();
            Common.SetMenu(frmCustomerFeedBack, this);
        }

        private void productExtendedPropertyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProductExtendedProperty frmProductExtendedProperty = new FrmProductExtendedProperty();
            Common.SetMenu(frmProductExtendedProperty, this);
        }

        private void productExtendedPropertyValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductExtendedValue frmProductExtendedValue = new FrmProductExtendedValue();
            Common.SetMenu(frmProductExtendedValue, this);
        }

        private void colourToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmColour frmColour = new FrmColour();
            Common.SetMenu(frmColour, this);
        }

        private void sizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmExchangeRate frmSize = new FrmExchangeRate();
            Common.SetMenu(frmSize, this);
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSupplier frmSupplier = new FrmSupplier();
            Common.SetMenu(frmSupplier, this);
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            Common.SetMenu(frmCustomer, this);
        }

        private void transferOfGoodsTOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransferOfGoodsNote frmTransferOfGoodsNote = new FrmTransferOfGoodsNote();
            Common.SetMenu(frmTransferOfGoodsNote, this);
        }

        private void companyDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void locationDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLocation");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void vehicleDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmVehicle");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void driverDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDriver");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void helperDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmHelper");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void paymentMethodDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPaymentMethod");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void categoryDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void departmentDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSupplier");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategoryDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategory2DetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProduct");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productWiseSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductWiseSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierWiseSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSupplierWiseSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void excessStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptExcessStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void shortageStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptShortageStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void stockAdjustmentSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptStockAdjustmentPercentage");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseRegistryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPurchaseRegister");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductWisePurchase");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSupplierWisePurchase");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseOrderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void goodReceiveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseReturnNotePRNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseReturnNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void invoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmInvoice");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void transferOfGoodsTOGToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmTransferOfGoodsNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void stockAdjustmentToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void sampleOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void sampleInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

     
        private void supplierDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSupplier");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void categoryDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void departmentDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategoryDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategory2DetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticProduct");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseReturnNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {            
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticTransferOfGoodsNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticStockAdjustment");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticQuotation");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void customerAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLoyaltyCustomer");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productPriceChangeDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPriceChange");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnPnlBtnLgsSupplier_Click(object sender, EventArgs e)
        {
            FrmLogisticSupplier frmLogisticSupplier = new FrmLogisticSupplier();
            Common.SetMenu(frmLogisticSupplier, this);
        }

        private void rbnPnlLgsPrn_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
            Common.SetMenu(frmLogisticPurchaseReturnNote, this);
        }

        private void rbnBtnLogIn_Click(object sender, EventArgs e)
        {
            DisebleMenu();
            FrmLoginUser frmLoginUser = new FrmLoginUser(this);
            Common.SetMenu(frmLoginUser, this);
        }


        private void rbnBtnLogOff_Click(object sender, EventArgs e)
        {
            DisebleMenu();
        }

        private void mnuMaintenanceJobRequisitionNote_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobRequisitionNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mnuMaintenanceJobAssignNote_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobAssignNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mnuMaterialAllocationNote_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialAllocationNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mnuMaterialRequestNote_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialRequestNote");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void productWiseStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBatchWiseStockDetails");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void grossProfitReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quotationToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void salesReturnNoteSRNToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dispatchNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void transactionToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void transactionToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void transactionToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        //private void customerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
        //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerHistory");
        //    CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
        //    Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        //}

        private void customerBehaviorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerBehavior");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void CustomervisitdetailstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomervisitdetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void NoofVsitCustomerWisetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptNumberOfVisitsCustomerWise");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void CashierWiseLoyaltySummarytoolStripMenuItem8_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCashierWiseLoyaltySummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void LocationWiseSummarytoolStripMenuItem8_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseSummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void CustomerStatementtoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerStatement");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void BestCustomerDetailstoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBestcustomerdetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

       
        private void MembershipUpgradeAnalysistoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMembershipUpgradesAnalysis");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void Lost_RenewAnalysistoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLostAndRenewalCardAnalysis");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void referenceToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void branchWiseCustomerDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBranchWiseCustomerDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void inactiveCustomerDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptInActiveCustomerDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void batchWiseStockAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBatchWiseStockAnalysis");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void batchWiseStockDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBatchWiseStockDetails");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void batchWiseStockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBatchWiseStockBalance");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productWiseBatchWiseStockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductWiseBatchWiseStockBalance");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void agingOfStockBatchWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptAgingOfStockBatchWise");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseReturnRegistryToolStripMenuItem_Click(object sender, EventArgs e)
        {


            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPurchaseReturnRegister");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptShortagePercentageStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBatchMaster");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FreeCardIssueDetailstoolStripMenuItem8_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptFreeCardIssueDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void LocationWiseLoyaltySummarytoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseLoyaltySummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void MonthVsitCustomerWisetoolStripMenuItem8_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMonthVisitCustomerWise");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void CardInventorytoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCardInventory");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void MemberShipUpgradeProposalReporttoolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMemberShipUpgradeProposalReport");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void userWiseDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptUserWiseDiscount");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void userMediaTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptUserMediaTerminal");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void userWiseSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptUserWiseSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void detailMediaSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptDetailMediaSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mediaSalesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMediaSalesSummary");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void salesDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSalesDiscount");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void creditCardCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCreditCardCollection");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void mnuSalesRegister_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSalesRegister");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void customerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerHistory");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void groupPriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidAccesRights("FrmGroupPrivileges") == false)
            { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

            FrmGroupPrivileges grmGroupPrivileges = new FrmGroupPrivileges();
            Common.SetMenu(grmGroupPrivileges, this);
        }

        private void userPrivilegesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidAccesRights("FrmUserPrivileges") == false)
            { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

            FrmUserPrivileges frmUserPrivileges = new FrmUserPrivileges();
            Common.SetMenu(frmUserPrivileges, this);
        }


        private void cashierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCashier frmCashier = new FrmCashier();
            Common.SetMenu(frmCashier, this);
        }

        private void printConfigurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void keyBoardConfigurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void systemConfigurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void costCentreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCostCentre frmCostCentre = new FrmCostCentre();
            Common.SetMenu(frmCostCentre, this);

        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            Common.SetMenu(frmEmployee, this);
        }

        private void mnuSummarySalesBook_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSalesRegister");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnTbPosRefCashier_Click(object sender, EventArgs e)
        {
            FrmCashier frmCashier = new FrmCashier();
            Common.SetMenu(frmCashier, this);
        }

        private void dataDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidAccesRights("FrmDataDownload") == false)
            { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

            FrmDataDownload frmDataDownload = new FrmDataDownload();
            Common.SetMenu(frmDataDownload, this);
        }

        private void salesSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidAccesRights("FrmPosSalesSummery") == false)
            { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

            FrmPosSalesSummery frmPosSalesSummery = new FrmPosSalesSummery();
            Common.SetMenu(frmPosSalesSummery, this);
        }

        private void mnuLocationSales_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void cashierGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCashierGroup frmCashierGroup = new FrmCashierGroup();
            Common.SetMenu(frmCashierGroup, this);
        }

        private void mnuGvPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherPurchaseOrder");
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            Common.SetMenu(gvReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mnuGvGoodReceiveNoteGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote");
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            Common.SetMenu(gvReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void mnuGvTransferOfGiftVouchersTOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherTransfer");
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            Common.SetMenu(gvReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void giftVoucherIssuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptGiftVoucherRegister");
            GvReportGenerator gvReportGenerator = new GvReportGenerator();
            Common.SetMenu(gvReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void currentSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidAccesRights("FrmCurrentSales") == false)
            { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

            FrmCurrentSales frmCurrentSales = new FrmCurrentSales();
            Common.SetMenu(frmCurrentSales, this);
        }

        private void rbnTbPosRefCashierGroup_Click(object sender, EventArgs e)
        {
            FrmCashierGroup frmCashierGroup = new FrmCashierGroup();
            Common.SetMenu(frmCashierGroup, this);
        }
         

        //private void openingBalancesRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
        //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptOpeningBalanceRegister");
        //    ComReportGenerator comReportGenerator = new ComReportGenerator();
        //    Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        //}

        //private void openingBalancesToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
        //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmOpeningStock");
        //    InvReportGenerator invReportGenerator = new InvReportGenerator();
        //    Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        //}

        private void openingBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openingBalanceRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("InvRptOpeningBalanceRegister");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void openingBalanceRegisterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("LgsRptOpeningBalanceRegister");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void stockBalancesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonService.ExecuteStoredProcedure("spCalculateCurrentStock");
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("InvRptStockBalance");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void staffCashCreditCardSalesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptStaffCashCrediCard");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void staffCreditSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptStaffCreditSales");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void staffSalaryAdvanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptStaffSalaryAdvance");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnPnlInvBarcode_Click(object sender, EventArgs e)
        {
            FrmBarcode frmBarcode = new FrmBarcode();
            Common.SetMenu(frmBarcode, this);
        }

        private void rbnPnlInvQuotation_Click(object sender, EventArgs e)
        {
            FrmQuotation frmQuotation = new FrmQuotation();
            Common.SetMenu(frmQuotation, this);
        }

        private void productPriceChangeToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManualPasswordChange frmManualPasswordChange = new FrmManualPasswordChange();
            Common.SetMenu(frmManualPasswordChange, this);
        }

        private void rbnTbUserGroups_Click(object sender, EventArgs e)
        {
            FrmGroupPrivileges grmGroupPrivileges = new FrmGroupPrivileges();
            Common.SetMenu(grmGroupPrivileges, this);
        }

        private void rbnTbUserPrivilages_Click(object sender, EventArgs e)
        {
            FrmUserPrivileges frmUserPrivileges = new FrmUserPrivileges();
            Common.SetMenu(frmUserPrivileges, this);
        }

        private void rbnTbChangePassword_Click(object sender, EventArgs e)
        {
            FrmManualPasswordChange frmManualPasswordChange = new FrmManualPasswordChange();
            Common.SetMenu(frmManualPasswordChange, this);
        }


        private void productPriceChangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProductPriceChangeDamage frmProductPriceChangeDamage = new FrmProductPriceChangeDamage();
            Common.SetMenu(frmProductPriceChangeDamage, this);
        }

        private void stockBalancesReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("LgsRptStockBalance");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void priceChangeDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductPriceChangeDetail");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void receiptsRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptReceiptsRegister");
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            Common.SetMenu(accReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void batchStockBalancesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("InvRptBatchStockBalance");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void givenDateStock_Click(object sender, EventArgs e)
        {
            FrmGivenDateStock frmGivenDateStock = new FrmGivenDateStock();
            Common.SetMenu(frmGivenDateStock, this);
        }

        private void binCard_Click(object sender, EventArgs e)
        {
            FrmBinCard brmBinCard = new FrmBinCard();
            Common.SetMenu(brmBinCard, this);
        }

        private void extendedPropertySalesRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptExtendedPropertySalesRegister");
            //InvReportGenerator invReportGenerator = new InvReportGenerator();
            ////Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            FrmExtendedPropertyReports frmExtendedPropertyReports = new FrmExtendedPropertyReports(autoGenerateInfo);
            Common.SetMenu(frmExtendedPropertyReports, this);
        }

        private void mnuBasketAnalysisReport_Click(object sender, EventArgs e)
        {
            Report.GUI.FrmBasketAnalysisReport frmBasketAnalysisReport = new Report.GUI.FrmBasketAnalysisReport();
            Common.SetMenu(frmBasketAnalysisReport, this);
        }

        private void pendingPurchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPendingPurchaseOrders");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSupplierWisePerformanceAnalysis");
            //InvReportGenerator invReportGenerator = new InvReportGenerator();
            //Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);

            FrmSupplierWiseReport frmSupplierWiseReport = new FrmSupplierWiseReport();
            Common.SetMenu(frmSupplierWiseReport, this);
        }

        private void stockMovementDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogistciBinCard frmLogistciBinCard = new FrmLogistciBinCard();
                Common.SetMenu(frmLogistciBinCard, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void designationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDesignationType frmDesignationType = new FrmDesignationType();
                Common.SetMenu(frmDesignationType, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rbntbEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEmployee frmEmployee = new FrmEmployee();
                Common.SetMenu(frmEmployee, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void pettyCashReimbursementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashReimbursement");
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            Common.SetMenu(accReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void pettyCashIOUToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU");
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            Common.SetMenu(accReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void pettyCashBillToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashBillEntry");
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            Common.SetMenu(accReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void pettyCashPaymentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashPayment");
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            Common.SetMenu(accReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void ManuallyAddedpointsdetails_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmManuallyAddedPoints frmCrmManuallyAddedPoints = new FrmCrmManuallyAddedPoints("Manually Added Points Details");
                Common.SetMenu(frmCrmManuallyAddedPoints, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ExtendedPropertyStockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptExtendedPropertyStockBalance");
            FrmExtendedPropertyReports frmExtendedPropertyReports = new FrmExtendedPropertyReports(autoGenerateInfo);
            Common.SetMenu(frmExtendedPropertyReports, this);
        }

        private void LocationWiseArapaima_ArrowanaSummery_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmLocationWiseTypeWiseSummery frmCrmLocationWiseTypeWiseSummery = new FrmCrmLocationWiseTypeWiseSummery("Location Wise Arapaima/Arrowana Summery");
                Common.SetMenu(frmCrmLocationWiseTypeWiseSummery, this); 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void locationWiseLoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseSummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void CustomerDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmTypeWiseCustomerDetails frmCrmTypeWiseCustomerDetails = new FrmCrmTypeWiseCustomerDetails("Type Wise Customer Details");
                Common.SetMenu(frmCrmTypeWiseCustomerDetails, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

 
        private void loyaltyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmLocationAnalysis frmCrmLocationAnalysis = new FrmCrmLocationAnalysis("LOCATION WISE LOYALTY ANALYSIS ");
                Common.SetMenu(frmCrmLocationAnalysis, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

 
        private void ProductTransferRegisterToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("InvRptTransferRegister");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void clock1_TimeChanged(object sender, EventArgs e)
        {
            //this.clock1.UtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        }

        private void GoldCardSelection_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmCrmGoldCardSelection frmCrmGoldCardSelection = new FrmCrmGoldCardSelection("Gold Card Selection");
                //Common.SetMenu(frmCrmGoldCardSelection, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void productTransferRegisterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("LgsRptTransferRegister");
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void reportGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportGenerator2 frmReportGenerator2 = new FrmReportGenerator2();
            Common.SetMenu(frmReportGenerator2, this);
        }

        private void CustomerVisitDetails_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmOrderSystemReport frmCrmCustomerVisitDetail = new FrmOrderSystemReport("Customer Visit Details");
                //Common.SetMenu(frmCrmCustomerVisitDetail, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TopCustomerPerformances_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmTopCustomerPerformance frmCrmTopCustomerPerformance = new FrmCrmTopCustomerPerformance("Top Customer Performances");
                Common.SetMenu(frmCrmTopCustomerPerformance, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CashierPerformances_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmCashierPerformance frmCrmCashierPerformance = new FrmCrmCashierPerformance("Cashier Performances");
                Common.SetMenu(frmCrmCashierPerformance, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void manualPointsAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManualPointsAdd frmManualPointsAdd = new FrmManualPointsAdd();
            Common.SetMenu(frmManualPointsAdd, this);
        }

        private void ManualPoinsAdd_Click(object sender, EventArgs e)
        {
            FrmManualPointsAdd frmManualPointsAdd = new FrmManualPointsAdd();
            Common.SetMenu(frmManualPointsAdd, this);
        }

        private void CardIssuedDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmCardIssuedDetails frmCrmCardIssuedDetails = new FrmCrmCardIssuedDetails("Card Issued Details");
                Common.SetMenu(frmCrmCardIssuedDetails, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CardUsageDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCrmCardUsage crmCrmCardUsage = new FrmCrmCardUsage("Card Usage Details");
                Common.SetMenu(crmCrmCardUsage, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TransactionSummeyPanel_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogisticTransactionSearch frmLogisticTransactionSearch = new FrmLogisticTransactionSearch();
                Common.SetMenu(frmLogisticTransactionSearch, this);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void sampleOutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSampleOut");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void sampleInToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSampleIn");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnPnlLgsTransactionPanel_Click(object sender, EventArgs e)
        {
            FrmLogisticTransactionSearch frmLogisticTransactionSearch = new FrmLogisticTransactionSearch();
            Common.SetMenu(frmLogisticTransactionSearch, this);
        }

        private void LostAndRenewHistory_Click(object sender, EventArgs e)
        {
            FrmCrmLostAndRenewDetail frmCrmLostAndRenewDetail = new FrmCrmLostAndRenewDetail("Lost And Renew History");
            Common.SetMenu(frmCrmLostAndRenewDetail, this);
        }

        private void PointsBreakdown_Click(object sender, EventArgs e)
        {
            FrmCrmPointsBreakdown frmCrmPointsBreakdown = new FrmCrmPointsBreakdown("Points Breakdown");
            Common.SetMenu(frmCrmPointsBreakdown, this);
        }

        private void mnuCrmRegister_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCRMRegister");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void linkedAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLinkedAccount frmLinkedAccount = new FrmLinkedAccount();
            Common.SetMenu(frmLinkedAccount, this);
        }

        private void productPriceChangeDamageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductPriceChangeDamageDetail");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void pendingPurchaseOrdersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLgsPendingPurchaseOrders");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void openingBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmOpeningStock");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnPnlInvSOrder_Click(object sender, EventArgs e)
        {
            FrmSalesOrder frmSalesOrder = new FrmSalesOrder();
            Common.SetMenu(frmSalesOrder, this);
        }

        private void rbnPnlInvDispatch_Click(object sender, EventArgs e)
        {
            FrmDispatchNote frmDispatchNote = new FrmDispatchNote();
            Common.SetMenu(frmDispatchNote, this);
            
        }

        private void rbnOrderDtlReprot_Click(object sender, EventArgs e)
        {
            //FrmOrderSystemReportDtl frmOrderSystemDtlRpt = new FrmOrderSystemReportDtl("Active Pending Reprot");
            //Common.SetMenu(frmOrderSystemDtlRpt, this);
        }

        private void mnuInvRefeDepartment_Click_1(object sender, EventArgs e)
        {

        }

        private void rbnbtnDept_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnbtnCate_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnbtnSubCat_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnbtnSubCat2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void rbnbtnOrderDtl_Click(object sender, EventArgs e)
        {
        //    AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
        //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationSales");
        //    InvReportGenerator invReportGenerator = new InvReportGenerator();
        //    Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            FrmOrderSystemSalesReport frmSalesReport = new FrmOrderSystemSalesReport("Sales Report");
            Common.SetMenu(frmSalesReport, this);
        }

        private void rbnbtnOrderSumDtl_Click(object sender, EventArgs e)
        {
            FrmOrderSystemPendingReport frmOrderSystemReport = new FrmOrderSystemPendingReport("Active Pending Report");
            Common.SetMenu(frmOrderSystemReport, this);
        }

        private void rbnDeliverCheck_Click(object sender, EventArgs e)
        {
            //FrmDeliverCheck frmDeliverCheck = new FrmDeliverCheck();
            FrmInvoiceDeliver frmInvoiceDeliver = new FrmInvoiceDeliver();
            Common.SetMenu(frmInvoiceDeliver, this);
        }

        private void mnuInvRefeSalesPerson_Click_1(object sender, EventArgs e)
        {

        }

        private void rbnPnlBtnSalesman_Click_1(object sender, EventArgs e)
        {
            FrmSalesPerson frmSalesPerson = new FrmSalesPerson();
            Common.SetMenu(frmSalesPerson, this);
        }

        private void rbnSalesOrderDelete_Click(object sender, EventArgs e)
        {

            FrmSalesOrderDeliver frmSalesPerson = new FrmSalesOrderDeliver();
            Common.SetMenu(frmSalesPerson, this);
        }

        private void btnMinimize1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.ExitSystem).Equals(DialogResult.Yes))
                {
                    this.Close();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rbnFilterReport_Click(object sender, EventArgs e)
        {
            FrmOrderSystemFilterReport frmOrderSystemFilterReport = new FrmOrderSystemFilterReport();
            Common.SetMenu(frmOrderSystemFilterReport, this);
        }

        private void rbnBtnSalesFilter_Click(object sender, EventArgs e)
        {
            FrmOrderSystemFilterSalesReport frmOrderSystemFilterSalesReport = new FrmOrderSystemFilterSalesReport();
            Common.SetMenu(frmOrderSystemFilterSalesReport, this);
        }

        private void ribBtnInvReDispatch_Click(object sender, EventArgs e)
        {
            FrmInvoiceReorder frmInvoiceReorder = new FrmInvoiceReorder();
            Common.SetMenu(frmInvoiceReorder, this);
        }

        private void ribbtnReceipt_Click(object sender, EventArgs e)
        {
            FrmExchangeRate frmExchangeRate = new FrmExchangeRate();
            Common.SetMenu(frmExchangeRate, this);
        }
 
    }
}
