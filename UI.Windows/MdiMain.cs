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
using Report.CRM;
using Report.CRM.Reports;
using Report.Com;
using Report.Com.Reference.Reports;
using Report.Inventory;
using Report.Inventory.Reference.Reports;
using Report.Inventory.Transactions.Reports;
using Report.Logistic;
using Report.Logistic.Reference.Reports;
using Service;
using Utility;
using Report;
using Domain;     

namespace UI.Windows
{
    public partial class MdiMain : Form
    {
        private int childFormNumber = 0;

        public MdiMain()
        {
            InitializeComponent();
        }

        private void MdiMain_Load(object sender, EventArgs e)
        {
            tslblLoggedUser.Text = "Admin";
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = mnuInventoryTransaction.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void mnuDepartmentMaster_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            Common.SetMenu(frmDepartment, this);

        }

        private void mnuCategoryMaster_Click(object sender, EventArgs e)
        {
            FrmCategory frmCategory = new FrmCategory();
            Common.SetMenu(frmCategory, this);

        }

        private void mnuSubCategoryMaster_Click(object sender, EventArgs e)
        {
            FrmSubCategory frmSubCategory = new FrmSubCategory();
            Common.SetMenu(frmSubCategory, this);

        }

        private void mnuSubCategory2Master_Click(object sender, EventArgs e)
        {
            FrmSubCategory2 frmSubCategory2 = new FrmSubCategory2();
            Common.SetMenu(frmSubCategory2, this);
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

        private void salesPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalesPerson frmSalesPerson = new FrmSalesPerson();
            Common.SetMenu(frmSalesPerson, this);
        }

        private void mnuCompanyMaster_Click(object sender, EventArgs e)
        {
            FrmCompany frmCompany = new FrmCompany();
            Common.SetMenu(frmCompany, this);
        }

        private void mnuLocationMaster_Click(object sender, EventArgs e)
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

        private void unitOfMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUnitOfMeasure frmUnitOfMeasure = new FrmUnitOfMeasure();
            Common.SetMenu(frmUnitOfMeasure, this);
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPurchaseOrder frmPurchaseOrder = new FrmPurchaseOrder();
            Common.SetMenu(frmPurchaseOrder, this);
        }

        private void goodsReceivedNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGoodsReceivedNote frmGoodsReceivedNote = new FrmGoodsReceivedNote();
            Common.SetMenu(frmGoodsReceivedNote, this);
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInvoice frmInvoice = new FrmInvoice();
            Common.SetMenu(frmInvoice, this);
        }

        private void supplierReturnNoteSRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalesReturnNote frmSalesReturnNote = new FrmSalesReturnNote();
            Common.SetMenu(frmSalesReturnNote, this);
        }

        private void CmdHide_Click(object sender, EventArgs e)
        {
            timer1.Start();
            CmdHide.Visible = false;  
        }

        private void CmdExpand_Click(object sender, EventArgs e)
        {
            treeMenu.Width = 215;
            CmdHide.Visible = true;
            CmdExpand.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (treeMenu.Width <= 20)
            {
                timer1.Stop();
                CmdExpand.Visible = true;
            }
            treeMenu.Width -= 15;
        }

        private void returnTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void customerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            Common.SetMenu(frmCustomer, this);
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExchangeRate frmSize = new FrmExchangeRate();
            Common.SetMenu(frmSize, this);
        }

        private void mnuMasterFiles_Click(object sender, EventArgs e)
        {

        }

        private void mnuProductMaster_Click(object sender, EventArgs e)
        {
            FrmProduct frmProduct = new FrmProduct();
            Common.SetMenu(frmProduct, this);
        }

        private void paymentMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentMethod frmPaymentMethod = new FrmPaymentMethod();
            Common.SetMenu(frmPaymentMethod, this);
        }

        private void customerGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomerGroup frmCustomerGroup = new FrmCustomerGroup();
            Common.SetMenu(frmCustomerGroup, this);
        }

        private void supplierGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSupplierGroup frmSupplierGroup = new FrmSupplierGroup();
            Common.SetMenu(frmSupplierGroup, this);
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTaxBreakdown frmTest = new FrmTaxBreakdown(2, 1, 100);
            Common.SetMenu(frmTest, this);
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticDepartment frmLogisticDepartment = new FrmLogisticDepartment();
            Common.SetMenu(frmLogisticDepartment, this);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticCategory FrmLogisticCategory = new FrmLogisticCategory();
            Common.SetMenu(FrmLogisticCategory, this);
        }

        private void subCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticSubCategory frmLogisticSubCategory = new FrmLogisticSubCategory();
            Common.SetMenu(frmLogisticSubCategory, this);
        }

        private void subCategory2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticSubCategory2 frmLogisticSubCategory2 = new FrmLogisticSubCategory2();
            Common.SetMenu(frmLogisticSubCategory2, this);
        }

        private void supplierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLogisticSupplier frmLogisticSupplier = new FrmLogisticSupplier();
            Common.SetMenu(frmLogisticSupplier, this);
        }

        private void supplierQuotationEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLogisticQuotation frmLogisticQuotation = new FrmLogisticQuotation();
            Common.SetMenu(frmLogisticQuotation, this);
        }

        private void logisticPurchaseOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseOrder frmNonTradingPurchaseOrder = new FrmLogisticPurchaseOrder();
            Common.SetMenu(frmNonTradingPurchaseOrder, this);
        }

        private void sampleOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSampleOut frmSampleOut = new FrmSampleOut();
            Common.SetMenu(frmSampleOut,this);
        }

        private void sampleInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSampleIn frmSampleIn = new FrmSampleIn();
            Common.SetMenu(frmSampleIn, this);
        }

        private void promotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPromotionMaster frmPromotionMaster = new FrmPromotionMaster();
            Common.SetMenu(frmPromotionMaster, this);
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {

        }

        private void transferOfGoodsTOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransferOfGoodsNote frmTransferOfGoodsNoe = new FrmTransferOfGoodsNote();
            Common.SetMenu(frmTransferOfGoodsNoe, this);
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

        private void dispatchNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDispatchNote frmDispatchNote = new FrmDispatchNote();
            Common.SetMenu(frmDispatchNote, this);
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockAdjustment frmStockAdjustment = new FrmStockAdjustment();
            Common.SetMenu(frmStockAdjustment, this);
        }

        private void openingStockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
            Common.SetMenu(frmOpeningStock, this);
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalesOrder frmSalesOrder = new FrmSalesOrder();
            Common.SetMenu(frmSalesOrder, this);
        }


        private void logisticgoodsReceivedNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticGoodsReceivedNote frmLogisticGoodsReceivedNote = new FrmLogisticGoodsReceivedNote();
            Common.SetMenu(frmLogisticGoodsReceivedNote, this);
        }


        private void quotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmQuotation frmQuotation = new FrmQuotation();
            Common.SetMenu(frmQuotation, this);
        }

        private void barCodePrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBarcode FrmBarcode = new FrmBarcode();
            Common.SetMenu(FrmBarcode, this);
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

        private void purchaseOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherPurchaseOrder frmGiftVoucherPurchaseOrder = new FrmGiftVoucherPurchaseOrder();
            Common.SetMenu(frmGiftVoucherPurchaseOrder, this);
        }

        private void goodsReceivedNoteGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherGoodsReceivedNote frmGiftVoucherGoodsReceivedNote = new FrmGiftVoucherGoodsReceivedNote();
            Common.SetMenu(frmGiftVoucherGoodsReceivedNote, this);
        }

        private void giftVoucherTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiftVoucherTransfer frmGiftVoucherTransfer = new FrmGiftVoucherTransfer();
            Common.SetMenu(frmGiftVoucherTransfer, this);
        }

        private void pettyCashMasterCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashMasterCreation frmPettyCashMasterCreation = new FrmPettyCashMasterCreation();
            Common.SetMenu(frmPettyCashMasterCreation, this);
        }

        private void pettyCashIOUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashIOU frmPettyCashIOU = new FrmPettyCashIOU();
            Common.SetMenu(frmPettyCashIOU, this);
        }

        private void pettyCashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashPayment frmPettyCashPayment = new FrmPettyCashPayment();
            Common.SetMenu(frmPettyCashPayment, this);
        }

        private void pettyCashReimbursementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashReimbursement frmPettyCashReimbursement = new FrmPettyCashReimbursement();
            Common.SetMenu(frmPettyCashReimbursement, this);
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

        private void stockAdjustmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLogisticStockAdjustment frmLogisticStockAdjustment = new FrmLogisticStockAdjustment();
            Common.SetMenu(frmLogisticStockAdjustment, this);
        }

        private void loyaltyCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLoyaltyCustomer FrmLoyaltyCustomer = new FrmLoyaltyCustomer();
            Common.SetMenu(FrmLoyaltyCustomer, this);
        }

        private void cardTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardMaster frmCardMaster = new FrmCardMaster();
            Common.SetMenu(frmCardMaster, this);
        }

        private void customerFeedBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomerFeedBack frmCustomerFeedBack = new FrmCustomerFeedBack();
            Common.SetMenu(frmCustomerFeedBack, this);
        }

        private void cardAllocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardAllocation frmCardAllocation = new FrmCardAllocation();
            Common.SetMenu(frmCardAllocation, this);
        }

        private void cardNoGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardNoGeneration frmCardNoGeneration = new FrmCardNoGeneration();
            Common.SetMenu(frmCardNoGeneration, this);
        }

        private void cardIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCardIssue frmCardIssue = new FrmCardIssue();
            Common.SetMenu(frmCardIssue, this);
        }

        private void productPriceChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductPriceChange frmProductPriceChange = new FrmProductPriceChange();
            Common.SetMenu(frmProductPriceChange, this);
        }

        private void pettyCashBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashBillEntry frmPettyCashBillEntry = new FrmPettyCashBillEntry();
            Common.SetMenu(frmPettyCashBillEntry, this);
        }

        private void pettyCashPaymentProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPettyCashPaymentUpdate frmPettyCashPaymentUpdate = new FrmPettyCashPaymentUpdate();
            Common.SetMenu(frmPettyCashPaymentUpdate, this);
        }

        private void transferOfGoodsTOGToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLogisticTransferOfGoodsNote frmLogisticTransferOfGoodsNote = new FrmLogisticTransferOfGoodsNote();
            Common.SetMenu(frmLogisticTransferOfGoodsNote, this);
        }

        private void mnuCrmReport_Click(object sender, EventArgs e)
        {

        }

        private void customerAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerAddress");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);

            //string[] stringfield = { "Code","Customer Name", "Address", "NIC", "Mobile", "Membership No" };
            //string[] decimalfield= { };
            //DataTable dtCustomer = new DataTable();
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Customer Address";
            //CrmRptReportTemplate crmRptReportTemplate = new CrmRptReportTemplate();
            //int x;

            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName, mdiService.GetCustomerAddressDataTable(), 0, crmRptReportTemplate);
            //Common.SetMenu(frmReprotGenerator,this);


        }

        private void customerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerHistory");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            //string[] stringfield = { "Code", "Card No", "Cus.Name", "NIC", "Rec.Date", "Remark", "Loca.Name", "Sent Date", "Sent By", "Designation", "Card Type" };
            //string[] decimalfield = { };
            //DataTable dtCustomer = new DataTable();
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Customer History";
            //CrmRptReportTemplate crmRptReportTemplate = new CrmRptReportTemplate();
            //int x;

            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName, mdiService.GetCustomerHistoryDataTable(), 0, crmRptReportTemplate);
            //Common.SetMenu(frmReprotGenerator, this);
        }

        private void userGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGroupPrivileges frmGroupPrivileges = new FrmGroupPrivileges();
            Common.SetMenu(frmGroupPrivileges, this);
        }

        private void userProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserPrivileges frmUserPrivileges = new FrmUserPrivileges();
            Common.SetMenu(frmUserPrivileges, this);
        }

        private void mnuLogin_Click(object sender, EventArgs e)
        {
            FrmLoginUser frmLoginUser = new FrmLoginUser();
            Common.SetMenu(frmLoginUser, this);
        }

        private void MdiMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void MdiMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void productExtendedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductExtendedProperty frmProductExtendedProperty = new FrmProductExtendedProperty();
            Common.SetMenu(frmProductExtendedProperty, this);
        }

        private void frmProductExtendedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductExtendedValue frmProductExtendedValue = new FrmProductExtendedValue();
            Common.SetMenu(frmProductExtendedValue, this);
        }

        private void categoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Category";
            //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
            //bool isDepend = false;
            //int x;

            //isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
            //string departmentText = "", categoryText = "";

            //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;

            //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory");
            //InvReportGenerator invReportGenerator = new InvReportGenerator();
            //invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();

            //if (isDepend)
            //{
            //    //string[] stringfield = { "Code", categoryText, departmentText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllDepartmentWiseCategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    //string[] stringfield = { "Code", categoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllCategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Sub Category";
            //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
            //bool isDependSubCategory = false;
            //int x;

            //string categoryText = "", subCategoryText = "";

            //isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;

            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;

            //if (isDependSubCategory)
            //{
            //    string[] stringfield = { "Code", subCategoryText, categoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllCategoryWiseSubCategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    string[] stringfield = { "Code", subCategoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllSubCategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void subCategory2ReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Sub Category 02";
            //CryInvColumn5Template cryInvTemplate = new CryInvColumn5Template();
            //bool isDependSubCategory2 = false;
            //int x;

            //string subCategoryText = "", subCategory2Text = "";

            //isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

            //if (isDependSubCategory2)
            //{
            //    //string[] stringfield = { "Code", subCategory2Text, subCategoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllSubCategoryWiseSub2CategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    //string[] stringfield = { "Code", subCategory2Text, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllSub2CategoryDataTable(),
            //    //                                                               0, cryInvTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void productMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Product";
            //CryInvProductTemplate cryInvTemplate = new CryInvProductTemplate();
            //int x;

            //string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";

            //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText;
            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText;
            //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText;

            //string[] stringfield = { "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Remark" };
            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //                                                                mdiService.GetAllProductDataTable(),
            //                                                                0, cryInvTemplate);
            //Common.SetMenu(frmReprotGenerator, this);

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmProduct");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void supplierMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSupplier");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void logiscticCategoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Category";
            //CryLgsColumn5Template cryLgsTemplate = new CryLgsColumn5Template();
            //bool isDepend = false;
            //int x;

            //isDepend = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
            //string departmentText = "", categoryText = "";

            //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;

            //if (isDepend)
            //{
            //    string[] stringfield = { "Code", categoryText, departmentText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsDepartmentWiseCategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    string[] stringfield = { "Code", categoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsCategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void logisticSubCategoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Sub Category";
            //CryLgsColumn5Template cryLgsTemplate = new CryLgsColumn5Template();
            //bool isDependSubCategory = false;
            //int x;

            //string categoryText = "", subCategoryText = "";

            //isDependSubCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;

            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;

            //if (isDependSubCategory)
            //{
            //    string[] stringfield = { "Code", subCategoryText, categoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsCategoryWiseSubCategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    string[] stringfield = { "Code", subCategoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsSubCategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void logisticSubCategory2ReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Sub Category 02";
            //CryLgsColumn5Template cryLgsTemplate = new CryLgsColumn5Template();
            //bool isDependSubCategory2 = false;
            //int x;

            //string subCategoryText = "", subCategory2Text = "";

            //isDependSubCategory2 = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).IsDepend;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
            //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText;

            //if (isDependSubCategory2)
            //{
            //    string[] stringfield = { "Code", subCategory2Text, subCategoryText, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsSubCategoryWiseSub2CategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            //else
            //{
            //    string[] stringfield = { "Code", subCategory2Text, "Remark" };
            //    //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //    //                                                               mdiService.GetAllLgsSub2CategoryDataTable(),
            //    //                                                               0, cryLgsTemplate);
            //    //Common.SetMenu(frmReprotGenerator, this);
            //}
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void logiscticProductMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Product";
            //CryLgsProductTemplate cryLgsTemplate = new CryLgsProductTemplate();
            //int x;

            //string departmentText = "", categoryText = "", subCategoryText = "", subCategory2Text = "";

            //departmentText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment").FormText;
            //categoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").FormText;
            //subCategoryText = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory").FormText;
            //subCategory2Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSubCategory2").FormText;

            //string[] stringfield = { "Code", "Product Name", "Name on Invoice", departmentText, categoryText, subCategoryText, subCategory2Text, "Bar Code", "Re-order Qty", "Cost Price", "Selling Price", "Fixed Discount", "Batch Status", "Remark" };
            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName,
            //                                                                mdiService.GetAllProductDataTable(),
            //                                                                0, cryLgsTemplate);
            //Common.SetMenu(frmReprotGenerator, this);

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticProduct");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] decimalfield = { };
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Purchase Registry";
            //CryInvTransactionTemplate cryInvTransactionTemplate = new CryInvTransactionTemplate();
            
            //string[] stringField = { "Pro.Code", "Pro.Name", "Unit", "P.Size", "Or.Qty", "F.Qty", "C.Price", "Dis.%", "Dis.Amt.", "Net Amt.", 
            //                        "Document", "Date", "Supplier", "Remark", "P Req. No", "Validity Period", "Reference No", "Payment terms",
            //                        "Expected Date", "Location", "Gross Amt.", "Dis.%", "Dis.Amt.", "Tax Amt.", "Oth. Charges", "Net Amt."
            //                       };

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPurchaseRegistry");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
            
            //InvReportGenerator invReportGenerator = new InvReportGenerator();
            //invReportGenerator.GenearateReport(this.Text.Trim(), true, stringField, mdiService.GetPurchaseDataTable());

            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringField, decimalfield, ReprotName, mdiService.GetPurchaseDataTable(), 0, cryInvTransactionTemplate);
            //Common.SetMenu(frmReprotGenerator, this);
        }

        private void locationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLocation");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void logisticSupplierMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string[] stringfield = { "Code", "Sup.Name", "Address", "Telephone", "NIC", "Rec.Date", "Payment Method", "Contact Person", "Remark" };
            //string[] decimalfield = { };
            ////DataTable dtSupplier = new DataTable();
            //MdiService mdiService = new MdiService();
            //string ReprotName = "Supplier";
            //CryLgsTemplate cryLgsTemplate = new CryLgsTemplate();
            //int x;

            //FrmReprotGenerator frmReprotGenerator = new FrmReprotGenerator(stringfield, decimalfield, ReprotName, mdiService.GetAllLgsSupplierDataTable(), 0, cryLgsTemplate);
            //Common.SetMenu(frmReprotGenerator, this);

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSupplier");
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            Common.SetMenu(lgsReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseReturnNotePRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPurchaseReturnNote frmPurchaseReturnNote = new FrmPurchaseReturnNote();
            Common.SetMenu(frmPurchaseReturnNote, this);
        }

        private void productWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptProductWisePurchase");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void categoryWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void subCategoryWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void purchaseOrderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void inActiveCustomerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptInActiveCustomerDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void branchWiseCustomerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBranchWiseCustomerDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void goodReceiveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseReturnNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogisticPurchaseReturnNote frmLogisticPurchaseReturnNote = new FrmLogisticPurchaseReturnNote();
            Common.SetMenu(frmLogisticPurchaseReturnNote, this);
        }

        private void customerVisitDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomervisitdetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void bestCustomerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptBestCustomerDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

 
        private void customerBehaviorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerBehavior");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }
 
        private void supplierWisePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptSupplierWisePurchase");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }
 


        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDriver frmDriver = new FrmDriver();
            Common.SetMenu(frmDriver, this);
        }

        private void noOfVisitCustomerWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptNumberOfVisitsCustomerWise");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void helperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHelper frmHelper = new FrmHelper();
            Common.SetMenu(frmHelper, this);
        }

        private void salesPersonReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesPerson");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }
 
        private void locationWiseSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseSummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVehicle frmvehicle = new FrmVehicle();
            Common.SetMenu(frmvehicle, this);
        }



        private void cashierWiseLoyaltySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCashierWiseLoyaltySummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
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

 
        private void customerStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptCustomerStatement");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void locationWiseLoyaltyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseLoyaltyAnalysis");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void memberShipUpgradeAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMembershipUpgradesAnalysis");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void lostRenewAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLostAndRenewalCardAnalysis");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void freeCardIssueDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptFreeCardIssueDetails");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void locationWiseLoyaltySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseLoyaltySummary");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void monthVsitCustomerWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptMonthVisitCustomerWise");
            CrmReportGenerator crmReportGenerator = new CrmReportGenerator();
            Common.SetMenu(crmReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

 
        private void vehicleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmVehicle");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void driverReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDriver");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void helperReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmHelper");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
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

        private void salesReturnNoteSRNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesReturnNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }


        private void paymentMethodReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPaymentMethod");
            ComReportGenerator comReportGenerator = new ComReportGenerator();
            Common.SetMenu(comReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void purchaseReturnRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPurchaseReturnRegistry");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void userWiseDiscountReportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void mediaSalesSummeryToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void excessReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptExcessStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void shortageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptShortageStockAdjustment");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void quotationToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void stockAdjustmentPercentageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptStockAdjustmentPercentage");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void dispatchNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDispatchNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void salesOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSalesOrder");
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
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSampleOut");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void sampleInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSampleIn");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void lostAndRenewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLostAndRenew frmLostAndRenew = new FrmLostAndRenew();
            Common.SetMenu(frmLostAndRenew, this);
        }

        private void transferOfGoodsTOGToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmTransferOfGoodsNote");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void openingStockBalanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
             
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmOpeningStock");
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            Common.SetMenu(invReportGenerator.OrganizeFormFields(autoGenerateInfo), this);
        }

        private void materialRequestNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMaterialRequestNote frmMaterialRequestNote = new FrmMaterialRequestNote();
            Common.SetMenu(frmMaterialRequestNote, this);
        }

        private void transferTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supplierPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
 
    }
}
