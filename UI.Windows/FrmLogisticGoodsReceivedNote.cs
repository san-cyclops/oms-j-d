using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;
using Domain;
using Utility;
using Service;
using System.Threading;
using Report.Logistic;

namespace UI.Windows
{
    public partial class FrmLogisticGoodsReceivedNote : UI.Windows.FrmBaseTransactionForm
    {

        /// <summary>
        /// By - Pravin
        /// 26/07/2013
        /// Good Received Note
        /// </summary>
        /// 
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();
        LgsProductMaster existingLgsProductMaster;
        AccLedgerAccount existingAccLedgerAccount;
        LgsPurchaseDetailTemp existingILgsPurchaseDetailTemp =new LgsPurchaseDetailTemp();
        List<InvProductSerialNoTemp> lgsProductSerialNoTempList;
        List<PaymentTemp> paymentTempList;
        List<OtherExpenseTemp> otherExpenceTempList;
        bool isSupplierProduct;
        int documentID;
        int paymentType;
        bool poIsMandatory;
        bool recallPo;
        bool isBackDated;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmLogisticGoodsReceivedNote()
        {
            InitializeComponent();
        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "")
                {
                    LgsSupplierService supplierService = new LgsSupplierService();

                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID, 3, (Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.Trim())));
                    frmTaxBreakdown.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            
            try
            {
                LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                LocationService locationService = new LocationService();
                return lgsPurchaseServices.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }

        private void LoadProducts()
        {
            try
            {
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                if (isSupplierProduct)
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier supplier = new LgsSupplier();
                    supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                    Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodesBySupplier(supplier.LgsSupplierID), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNamesBySupplier(supplier.LgsSupplierID), chkAutoCompleationProduct.Checked);
                }
                else
                {
                    Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void LoadProductsAccordingToPurchaseOrder()
        {
            try
            {
                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                if (lgsPurchaseOrderHeader != null)
                {
                    Common.SetAutoComplete(txtProductCode, lgsPurchaseOrderService.GetProductCodesAccordingToPurchaseOrder(lgsPurchaseOrderHeader), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsPurchaseOrderService.GetProductNamesAccordingToPurchaseOrder(lgsPurchaseOrderHeader), chkAutoCompleationProduct.Checked);
                } 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                dgvItemDetails.AutoGenerateColumns = false;

                dgvPaymentDetails.AutoGenerateColumns = false;
                dgvAdvanced.AutoGenerateColumns = false;
                documentID = autoGenerateInfo.DocumentID;

                // Load Payment methods
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                Common.LoadPaymentMethods(cmbPaymentMethod, paymentMethodService.GetAllPaymentMethods());

                /////Load payment method to Grn Header
                Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtOtherExpenceCode, accLedgerAccountService.GetExpenceLedgerCodes(), chkAutoCompleationOtherExpence.Checked);
                Common.SetAutoComplete(txtOtherExpenceName, accLedgerAccountService.GetExpenceLedgerNames(), chkAutoCompleationOtherExpence.Checked);

                List<AccLedgerAccount> accLedgerAccount = new List<AccLedgerAccount>();
                accLedgerAccount = accLedgerAccountService.GetBankList();
                cmbBankCode.DataSource = accLedgerAccount;
                cmbBankCode.DisplayMember = "LedgerCode";
                cmbBankCode.ValueMember = "AccLedgerAccountID";
                cmbBankCode.Refresh();

                cmbBankName.DataSource = accLedgerAccount;
                cmbBankName.DisplayMember = "LedgerName";
                cmbBankName.ValueMember = "AccLedgerAccountID";
                cmbBankName.Refresh();

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                LgsSupplierService supplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);

                /////Load cost center name to combo
                CostCentreService costCentreService = new CostCentreService();
                List<CostCentre> costCentres = new List<CostCentre>();
                cmbCostCentre.DataSource = costCentreService.GetAllCostCentres();
                cmbCostCentre.DisplayMember = "CostCentreName";
                cmbCostCentre.ValueMember = "CostCentreID";
                cmbCostCentre.Refresh();
                cmbCostCentre.SelectedValue = locationService.GetLocationsByID(Common.LoggedLocationID).CostCentreID;

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                poIsMandatory = autoGenerateInfo.PoIsMandatory;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                isBackDated = autoGenerateInfo.IsBackDated;
                
                base.FormLoad();

                //Load purchase order document numbers
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, lgsPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);

                //Load GRN Document Numbers
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


                if (poIsMandatory)
                {
                    EnableControl(false);
                    this.ActiveControl = txtPurchaseOrderNo;
                    txtPurchaseOrderNo.Focus();
                }
                else
                {
                    EnableControl(true);
                    this.ActiveControl = txtSupplierCode;
                    txtSupplierCode.Focus();
                }

                if (isBackDated)
                {
                    dtpGrnDate.Enabled = true;
                }
                else
                {
                    dtpGrnDate.Enabled = false;
                }

                GetPrintingDetails();

                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                if (Common.tStatus == true) { chkTStatus.Visible = true; } else { chkTStatus.Visible = false; }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 

            }
        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                tabGRN.Enabled = false;
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                //Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
                Common.EnableTextBox(true, txtDocumentNo, txtPurchaseOrderNo);
                Common.EnableButton(true, btnDocumentDetails, btnPoDetails);
                //Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause, btnView);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                dtpExpiry.Value = DateTime.Now;
                //Common.ReadOnlyTextBox(true, txtFreeQty, txtRate);
                lgsPurchaseDetailTempList = null;
                existingLgsProductMaster = null;
                existingILgsPurchaseDetailTemp = null;
                lgsProductSerialNoTempList = null;
                paymentTempList = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                resetPayment();
                txtDocumentNo.Text = GetDocumentNo(true);
                recallPo = false;

                cmbCostCentre.SelectedIndex = 0;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                if (poIsMandatory)
                {
                    EnableControl(false);
                    this.ActiveControl = txtPurchaseOrderNo;
                    txtPurchaseOrderNo.Focus();
                }
                else
                {
                    EnableControl(true);
                    this.ActiveControl = txtSupplierCode;
                    txtSupplierCode.Focus();
                }

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                poIsMandatory = autoGenerateInfo.PoIsMandatory;

                if (isBackDated)
                {
                    dtpGrnDate.Enabled = true;
                }
                else
                {
                    dtpGrnDate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void EnableControl(bool status)
        {
            Common.EnableTextBox(status, txtSupplierCode, txtSupplierName, txtRemark, txtReferenceNo, txtSupplierInvoiceNo);
            Common.EnableComboBox(status, cmbLocation);
            //dtpGrnDate.Enabled = status;
            chkConsignmentBasis.Enabled = status;
            grpBody.Enabled = status;
            grpFooter.Enabled = status;
        }


        private void LoadSupplier(bool isCode, string strsupplier)
        {
            try
            {
                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier existingSupplier = new LgsSupplier();

                if (isCode)
                {
                    existingSupplier = supplierService.GetLgsSupplierByCode(strsupplier);
                    if (isCode && strsupplier.Equals(string.Empty))
                    {
                        txtSupplierName.Focus();
                        return;
                    }
                }
                else { existingSupplier = supplierService.GetLgsSupplierByName(strsupplier); }
                    
                if (existingSupplier != null)
                {
                    txtSupplierCode.Text = existingSupplier.SupplierCode;
                    txtSupplierName.Text = existingSupplier.SupplierName;
                    chkTStatus.Checked = existingSupplier.IsUpload;
                    if (existingSupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); } else { Common.EnableCheckBox(true, chkTStatus); }

                    txtRemark.Focus();
                }
                else
                {
                    Toast.Show("Supplier - " + strsupplier.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    if (isCode) { txtSupplierCode.Focus(); }
                    else { txtSupplierName.Focus(); }
                    return;
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void chkAutoCompleationSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsSupplierService supplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 

            }
        }


        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSupplierName.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveLgsSuppliersDataTable());
                    if (dvAllReferenceData.Count > 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtRemark.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveLgsSuppliersDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSupplierCode);
                        txtSupplierCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (e.KeyCode.Equals(Keys.Enter))
                     txtReferenceNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (e.KeyCode.Equals(Keys.Enter))
                    txtSupplierInvoiceNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (e.KeyCode.Equals(Keys.Enter))
                    cmbPaymentTerms.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    cmbLocation_Validated(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpGrnDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    dtpGrnDate_Leave(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpGrnDate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Common.ValidateDate(Common.FormatDate(dtpGrnDate.Value)))
                {
                    chkConsignmentBasis.Focus();
                    chkConsignmentBasis.Select();
                }
                else
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    dtpGrnDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkConsignmentBasis_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    cmbLocation.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkOverwrite_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode.Equals(Keys.Enter))
            //{
            //    cmbLocation_Validated(this, e);
            //}
        }

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSupplierCode.Text.Trim().Equals(string.Empty)) { LoadSupplier(true, txtSupplierCode.Text.Trim()); }
                else { return; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSupplierName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupplierName.Text.Trim())) { LoadSupplier(false, txtSupplierName.Text.Trim()); }
                else { return; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
            LgsSupplierService supplierService = new LgsSupplierService();

            if (supplierService.IsExistsLgsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
            {
                Toast.Show("Supplier - " + txtSupplierCode.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                txtSupplierCode.Focus();
                return;
            }
            else
            {
                LocationService locationService = new LocationService();
                if (cmbLocation.SelectedValue==null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                    if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }
      
                    tabGRN.Enabled = true;
                    Common.EnableTextBox(true,txtProductCode, txtProductName);
                    LoadProducts();

                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtDocumentNo.Enabled = false;

                    txtProductCode.Focus();
                } 
            }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (recallPo)
                {
                    LoadProductsAccordingToPurchaseOrder();
                }
                else
                {
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkOverwrite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim())) { return; }

                if (recallPo)
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);

                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                    if (!lgsPurchaseServices.ValidateExistingProduct(true, txtProductCode.Text.Trim(), txtPurchaseOrderNo.Text.Trim()))
                    {
                        Toast.Show("Product not exists for purchase order", Toast.messageType.Information, Toast.messageAction.General);
                        ClearLine();
                        return;
                    }
                }
                else
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void loadProductDetails(bool isCode,string strProduct,long unitofMeasureID,DateTime expiryDate)
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();

                if (strProduct.Equals(string.Empty)) { return; }

                LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = LgsProductMasterService.GetProductsByCode(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else { existingLgsProductMaster = LgsProductMasterService.GetProductsByName(strProduct); ;}
                    
                if (existingLgsProductMaster != null)
                {
                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                    if (lgsPurchaseDetailTempList == null) { lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>(); }

                    if (recallPo) 
                    {
                        //InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        //InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        //invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        //existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempForRecallPO(invPurchaseOrderHeader, existingInvProductMaster.InvProductMasterID);
                        existingILgsPurchaseDetailTemp = lgsPurchaseServices.GetPurchaseDetailTemp(lgsPurchaseDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID); 
                    }
                    else { existingILgsPurchaseDetailTemp = lgsPurchaseServices.GetPurchaseDetailTemp(lgsPurchaseDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID); }
                    
                    if (existingILgsPurchaseDetailTemp!= null) 
                    {
                        txtProductCode.Text = existingILgsPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingILgsPurchaseDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingILgsPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingILgsPurchaseDetailTemp.Qty);
                        txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingILgsPurchaseDetailTemp.FreeQty);
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.DiscountPercentage);
                        if (existingLgsProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingILgsPurchaseDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingILgsPurchaseDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        Common.EnableComboBox(true,cmbUnit);
                        if(unitofMeasureID.Equals(0)) 
                        cmbUnit.Focus();
                    }
                }

                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }      
        }


        private void LoadOtherExpences(bool isCode, string strLedger)
        {
            try
            {
                existingAccLedgerAccount = new AccLedgerAccount();

                if (strLedger.Equals(string.Empty))
                    return;

                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

                if (isCode)
                {
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerExpenseAccountByCode(strLedger);
                    if (isCode && strLedger.Equals(string.Empty))
                    {
                        txtOtherExpenceName.Focus();
                        return;
                    }
                }
                else
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerExpenseAccountByName(strLedger); ;

                if (existingAccLedgerAccount != null)
                {

                    if (isCode)
                    {
                        txtOtherExpenceName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        txtOtherExpenceCode.Text = existingAccLedgerAccount.LedgerCode;
                    }
                    txtOtherExpenceValue.Focus();
                }
                else
                {
                    Toast.Show("Ledger - " + strLedger + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtFreeQty, txtCostPrice, txtSellingPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode,txtProductName, txtQty, txtFreeQty, txtCostPrice, txtSellingPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now; ;
            txtProductCode.Focus();
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductCode.Text.Trim().Equals(string.Empty))
                    {
                        txtProductName.Enabled = true;
                        txtProductName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        cmbUnit.Enabled = true;
                        cmbUnit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductName.Text.Trim())) { return; }

                if (recallPo)
                {
                    loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value);

                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                    if (lgsPurchaseServices.ValidateExistingProduct(true, txtProductName.Text.Trim(), txtPurchaseOrderNo.Text.Trim()))
                    {
                        Toast.Show("Product not exists for purchase order", Toast.messageType.Information, Toast.messageAction.General);
                        ClearLine();
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                {
                    loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                    return;

                if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                {
                    LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();
                    if (lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                    {
                        Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                        dtpExpiry.Enabled = false;
                        cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }

                if (recallPo)
                {
                    loadProductDetailsAccordingToPurchaseOrder(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
                }
                else
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
                }

                if (existingLgsProductMaster.IsExpiry.Equals(true))
                {
                    dtpExpiry.Enabled = true;
                    dtpExpiry.Focus();
                }
                else
                {
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        txtQty.Text = "1";
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbUnit_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtFreeQty.Enabled = true;
                    txtFreeQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (recallPo)
                {
                    if (string.IsNullOrEmpty(txtQty.Text.Trim())) { txtQty.Text = "0"; }

                    decimal qty = Convert.ToDecimal(txtQty.Text.Trim());

                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());
                    if (lgsProductMaster != null)
                    {
                        LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                        if (!lgsPurchaseOrderService.IsValidNoOfQty(qty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID))
                        {
                            Toast.Show("Invalid Qty.\nQty cannot grater then purchase order Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                            txtQty.Focus();
                            txtQty.SelectAll();
                        }
                    }
                    else
                    {
                        Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
                else
                {
                    if (Common.ConvertStringToDecimalQty(txtQty.Text) > 0)
                    {
                        CalculateLine();
                    }
                    else
                    {
                        Toast.Show("Qty", Toast.messageType.Information, Toast.messageAction.ZeroQty);

                        txtQty.Focus();
                        txtQty.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if ((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())) > 0)
                    {
                        if (existingLgsProductMaster.IsSerial)
                        {
                            InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                            lgsProductSerialNoTemp.DocumentID = documentID;
                            if (existingLgsProductMaster.IsExpiry.Equals(true))
                                lgsProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                            lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                            lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();

                            if (lgsPurchaseServices.IsValidNoOfSerialNo(lgsProductSerialNoTempList, lgsProductSerialNoTemp, (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))))
                            {
                                txtCostPrice.Enabled = true;
                                txtCostPrice.Focus();
                            }
                            else
                            {
                                FrmSerial frmSerial = new FrmSerial(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.LogisticGoodReceivedNote);
                                frmSerial.ShowDialog();
                            }
                        }
                        else
                        {
                            txtSellingPrice.Enabled = true;
                            txtSellingPrice.Focus();
                            txtSellingPrice.SelectAll();
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (existingLgsProductMaster.IsSerial)
                    {
                        InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                        lgsProductSerialNoTemp.DocumentID = documentID;
                        if (existingLgsProductMaster.IsExpiry.Equals(true))
                            lgsProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                        lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (lgsProductSerialNoTempList == null)
                            lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        FrmSerial frmSerial = new FrmSerial(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()),FrmSerial.transactionType.LogisticGoodReceivedNote);
                        frmSerial.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtFreeQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (recallPo)
                {
                    if (string.IsNullOrEmpty(txtFreeQty.Text.Trim())) { txtFreeQty.Text = "0"; }

                    decimal freeQty = Convert.ToDecimal(txtFreeQty.Text.Trim());

                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByCode(txtProductCode.Text.Trim());
                    if (lgsProductMaster != null)
                    {
                        LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                        if (!lgsPurchaseOrderService.IsValidNoOfFreeQty(freeQty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID))
                        {
                            Toast.Show("Invalid Qty.\nQty cannot grater then purchase order Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                            txtFreeQty.Focus();
                            txtFreeQty.SelectAll();
                            return;
                        }
                        else
                        {
                            if ((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0)
                            {
                                txtSellingPrice.Enabled = true;
                                txtSellingPrice.Focus();
                                txtSellingPrice.SelectAll();
                            }
                            else
                            {
                                txtFreeQty.Focus();
                                txtFreeQty.SelectAll();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                }
                else
                {
                    txtSellingPrice.Enabled = true;
                    txtSellingPrice.Focus();
                    txtSellingPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtSellingPrice.Enabled = true;
                    txtSellingPrice.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSellingPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text.Trim()) <= 0)
                {
                    Toast.Show("Selling price", Toast.messageType.Information, Toast.messageAction.ZeroAmount);
                    txtSellingPrice.Focus();
                    txtSellingPrice.SelectAll();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscountPercentage.Enabled = true;
                    txtProductDiscountPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
             CalculateLine();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
           
        }

        private void txtProductDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscount.Enabled = true;
                    txtProductDiscount.Focus();
                    txtProductDiscount.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscount_Leave(object sender, EventArgs e)
        {
            
            try
            {
                decimal productAmount = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim());
                decimal discountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim());
                if (discountAmount >= productAmount)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.ProductDiscountAmountExceed);
                    txtProductDiscount.Focus();
                }
                else
                {
                    CalculateLine();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductAmount.Enabled = true;
                    txtProductAmount.Focus();
                    txtProductAmount.SelectAll();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_Leave(object sender, EventArgs e)
        {
            //CalculateLine();
        }

        private void UpdateGrid(LgsPurchaseDetailTemp lgsPurchaseTempDetail)
        {
            try
            {
                decimal qty = 0;
                decimal freeQty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0))
                {
                    CalculateLine();


                    if (existingLgsProductMaster.IsExpiry.Equals(true))
                        lgsPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                    lgsPurchaseTempDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    lgsPurchaseTempDetail.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID;
                    lgsPurchaseTempDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

                    if (chkOverwrite.Checked)
                    {
                        string productCode = txtProductCode.Text.Trim();
                        string unit = cmbUnit.Text.Trim();

                        if (dgvItemDetails["ProductCode", 0].Value == null) { }
                        else
                        {
                            for (int i = 0; i < dgvItemDetails.RowCount; i++)
                            {
                                if (productCode.Equals(dgvItemDetails["ProductCode", i].Value.ToString()) && unit.Equals(dgvItemDetails["Unit", i].Value.ToString()))
                                {
                                    if (Toast.Show("", Toast.messageType.Question, Toast.messageAction.OverwriteQty).Equals(DialogResult.Yes)) { }
                                    else { return; }
                                }
                            }
                        }

                    }
                    else
                    {
                        qty = (lgsPurchaseTempDetail.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                        freeQty = (lgsPurchaseTempDetail.FreeQty + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim()));
                    }


                    if (!chkOverwrite.Checked)
                    {
                        if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                        {
                            if ((existingLgsProductMaster != null))
                            {
                                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                                lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                                if (!lgsPurchaseOrderService.IsValidNoOfQty(qty, existingLgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID))
                                {
                                    Toast.Show("Invalid Qty.\nQty cannot grater then purchase order Qty", Toast.messageType.Information, Toast.messageAction.General, "");
                                    txtQty.Focus();
                                    txtQty.SelectAll();
                                    return;
                                }
                            }
                            else
                            {
                                Toast.Show("Product", Toast.messageType.Information, Toast.messageAction.NotExists);
                            }
                        }

                        CalculateLine(qty);
                        lgsPurchaseTempDetail.Qty = Common.ConvertDecimalToDecimalQty(qty);
                        lgsPurchaseTempDetail.FreeQty = Common.ConvertDecimalToDecimalQty(freeQty);
                        lgsPurchaseTempDetail.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                        lgsPurchaseTempDetail.NetAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text));
                    }
                    else
                    {
                        CalculateLine();
                        lgsPurchaseTempDetail.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        lgsPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text);
                        lgsPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                        lgsPurchaseTempDetail.NetAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text));
                    }

                    lgsPurchaseTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    lgsPurchaseTempDetail.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    lgsPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    lgsPurchaseTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    lgsPurchaseTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text);
                    lgsPurchaseTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);
                    
                    LgsPurchaseService LgsPurchaseServices = new Service.LgsPurchaseService();

                    dgvItemDetails.DataSource = null;
                    lgsPurchaseDetailTempList = LgsPurchaseServices.getUpdatePurchaseDetailTemp(lgsPurchaseDetailTempList, lgsPurchaseTempDetail, existingLgsProductMaster, recallPo);
                    dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    if (isUpdateGrid)
                    {
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[selectedRowIndex].Cells[0];
                        isUpdateGrid = false;
                    }
                    else
                    {
                        rowCount = dgvItemDetails.Rows.Count;
                        dgvItemDetails.CurrentCell = dgvItemDetails.Rows[rowCount - 1].Cells[0];
                    }

                    GetSummarizeFigures(lgsPurchaseDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails);
                    ClearLine();
                    if (lgsPurchaseDetailTempList.Count > 0)
                        grpFooter.Enabled = true;

                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                }
                else
                {
                    Toast.Show("Operation", Toast.messageType.Information, Toast.messageAction.Invalid);
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                lgsPurchaseTempDetail = null;
                txtProductCode.Focus();
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                    UpdateGrid(existingILgsPurchaseDetailTemp);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                    {
                        txtProductDiscount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                    }
                    else
                    {
                        txtProductDiscount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim()).ToString();
                    }
                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim())).ToString();
                }
                else
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                    {
                        txtProductDiscount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())) * (Common.ConvertDecimalToDecimalQty(qty)), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                    }
                    else
                    {
                        txtProductDiscount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim()).ToString();
                    }
                    txtProductAmount.Text = ((Common.ConvertDecimalToDecimalQty(qty)) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim())).ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void GetSummarizeFigures(List<LgsPurchaseDetailTemp> listItem)
        {
            CommonService commonService = new CommonService();
            LgsSupplierService supplierService = new LgsSupplierService();

            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;

            //Get Gross Amount
            //Note: apply this for first para- && x.DocumentID == '<required Transaction Id>' && x.LocationID ==  'Transaction Location' && '<userID>' && 'machine id'
            decimal grossAmount = listItem.GetSummaryAmount( x => x.NetAmount);

            grossAmount = Common.ConvertStringToDecimalCurrency(grossAmount.ToString());

            decimal otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;
           
            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (grossAmount - discountAmount), supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);

            txtTotalQty.Text = Common.ConvertDecimalToStringQty(GettotalQty(listItem));
        }

        private decimal GetLineDiscountTotal(List<LgsPurchaseDetailTemp> listItem)
        {           
            return listItem.GetSummaryAmount(x => x.DiscountAmount);          
        }

        private decimal GettotalQty(List<LgsPurchaseDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.Qty);
        }


        private void GetSummarizeSubFigures()
        {
            CommonService commonService = new CommonService();
            LgsSupplierService supplierService = new LgsSupplierService();

            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;

            //Get Gross Amount
            //Note: apply this for first para- && x.DocumentID == '<required Transaction Id>' && x.LocationID ==  'Transaction Location' && '<userID>' && 'machine id'
            decimal grossAmount = 0;

            grossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());

            decimal otherChargersAmount = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());

            //Get Discount Amount
            bool isSubDiscount = chkSubTotalDiscountPercentage.Checked;
            
            decimal subDiscount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
            decimal discountAmount = Common.ConvertStringToDecimalCurrency(Common.GetDiscountAmount(isSubDiscount, grossAmount, subDiscount).ToString());

            //Read from Tax
            decimal taxAmount = 0;
            if (chkTaxEnable.Checked)
            {
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (grossAmount - discountAmount), supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    dtpExpiry_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkTaxEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkSubTotalDiscountPercentage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
                txtSubTotalDiscountPercentage.Focus();
                txtSubTotalDiscountPercentage.SelectAll();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
    
        }

        private void txtTotalTaxAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
    
        }

        private void RefreshDocumentNumber()
        {
            LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
            Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            //Load purchase order document numbers
            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
            Common.SetAutoComplete(txtPurchaseOrderNo, lgsPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
        }

        private bool ValidateTextBoxes()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtSupplierCode);
        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbPaymentTerms, cmbLocation);
        }

        public override void Pause()
        {
            if ((Toast.Show("Good Received Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                if (!lgsPurchaseService.CheckExpiryDates(lgsPurchaseDetailTempList))
                {
                    Toast.Show("Invalid expiry dates", Toast.messageType.Information, Toast.messageAction.General);
                    dgvItemDetails.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;

                if (saveDocument.Equals(true))
                {
                    Toast.Show("Good Received Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Good Received Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
            if ((Toast.Show("Good Received Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                if (!lgsPurchaseService.CheckExpiryDates(lgsPurchaseDetailTempList))
                {
                    Toast.Show("Invalid expiry dates", Toast.messageType.Information, Toast.messageAction.General);
                    dgvItemDetails.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;

                if (saveDocument.Equals(true))
                {
                    Toast.Show("Good Received Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Good Received Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo,out string newDocumentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService supplierService = new LgsSupplierService();
                LgsSupplier supplier = new LgsSupplier();

                supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsPurchaseHeader = lgsPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (lgsPurchaseHeader == null)
                    lgsPurchaseHeader = new LgsPurchaseHeader();
                //////if (documentStatus.Equals(1)) // update paused document
                //////{
                //////    documentNo = GetDocumentNo(false);
                //////    txtDocumentNo.Text = documentNo;
                //////}
                lgsPurchaseHeader.PurchaseHeaderID = lgsPurchaseHeader.LgsPurchaseHeaderID;
                lgsPurchaseHeader.CompanyID = Location.CompanyID;
                lgsPurchaseHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                lgsPurchaseHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)                                
                    lgsPurchaseHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                lgsPurchaseHeader.OtherChargers = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                lgsPurchaseHeader.DocumentDate = Common.FormatDate(dtpGrnDate.Value);
                lgsPurchaseHeader.DocumentID = documentID;
                lgsPurchaseHeader.DocumentStatus = documentStatus;
                lgsPurchaseHeader.DocumentNo = documentNo.Trim();
                lgsPurchaseHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                lgsPurchaseHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsPurchaseHeader.LineDiscountTotal = GetLineDiscountTotal(lgsPurchaseDetailTempList);
                lgsPurchaseHeader.LocationID = Location.LocationID;
                lgsPurchaseHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                lgsPurchaseHeader.PaymentMethodID = Convert.ToInt32(cmbPaymentTerms.SelectedValue);
                
                if(!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                {
                    LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                    LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();

                    lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                    lgsPurchaseHeader.ReferenceDocumentDocumentID = lgsPurchaseOrderHeader.DocumentID;
                    lgsPurchaseHeader.ReferenceDocumentID = lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID;

                    if (poIsMandatory)
                    {
                        //lgsPurchaseHeader.BatchNo = lgsPurchaseOrderService.GetBatchNumberToGRN(lgsPurchaseOrderHeader.DocumentID, lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID);
                        lgsPurchaseHeader.BatchNo = txtPurchaseOrderNo.Text.Trim();
                        //lgsPurchaseHeader.BatchNo = "OSLG00000000001"; // Reason for hardcode--> request a -- batch number for transactions. 
                    }
                    else
                    {
                        lgsPurchaseHeader.BatchNo = lgsPurchaseHeader.DocumentNo;
                    }
                }

                lgsPurchaseHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                lgsPurchaseHeader.Remark = txtRemark.Text.Trim();
                lgsPurchaseHeader.SupplierID = supplier.LgsSupplierID;
                lgsPurchaseHeader.IsUpLoad = chkTStatus.Checked;
                lgsPurchaseHeader.SupplierInvoiceNo = txtSupplierInvoiceNo.Text.Trim();

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService=new CommonService();
                    lgsPurchaseHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (lgsPurchaseHeader.GrossAmount-lgsPurchaseHeader.DiscountAmount), supplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    lgsPurchaseHeader.TaxAmount1 = tax1;
                    lgsPurchaseHeader.TaxAmount2 = tax2;
                    lgsPurchaseHeader.TaxAmount3 = tax3;
                    lgsPurchaseHeader.TaxAmount4 = tax4;
                    lgsPurchaseHeader.TaxAmount5 = tax5;
                   
                }
                
                //Payment
                ////invPurchaseHeader.CurrencyID = Common.ConvertStringToInt(cmbCurrencyType.SelectedValue.ToString());
                ////invPurchaseHeader.CurrencyRate = Common.ConvertStringToDecimalCurrency(txtCurrencyRate.Text.ToString());
                ////invPurchaseHeader.PaymentMethodID = Common.ConvertStringToInt(cmbPaymentMethod.SelectedValue.ToString().Trim());
                
                    //TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew,
                //   new TransactionOptions()
                //   {

                if (lgsPurchaseDetailTempList == null)
                    lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                if (otherExpenceTempList == null)
                    otherExpenceTempList = new List<OtherExpenseTemp>();

                if (paymentTempList == null)
                    paymentTempList = new List<PaymentTemp>();

                LgsPurchaseService.poIsMandatory = poIsMandatory;
                return lgsPurchaseService.Save(lgsPurchaseHeader, lgsPurchaseDetailTempList, lgsProductSerialNoTempList, out newDocumentNo, otherExpenceTempList, paymentTempList, Common.ConvertStringToDecimalCurrency(txtPaidAmount.Text.Trim()), this.Name);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;
                return false;          
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                recallPo = false;
                this.Cursor = Cursors.WaitCursor;
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();

                lgsPurchaseHeader = lgsPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                if (lgsPurchaseHeader != null)
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier supplier = new LgsSupplier();

                    supplier = supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                    cmbLocation.SelectedValue = lgsPurchaseHeader.LocationID;
                    cmbLocation.Refresh();
                    cmbPaymentTerms.SelectedValue = lgsPurchaseHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    cmbCostCentre.SelectedValue = lgsPurchaseHeader.CostCentreID;
                    if (!lgsPurchaseHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.DiscountAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.OtherChargers );

                    dtpGrnDate.Value =Common.FormatDate(lgsPurchaseHeader.DocumentDate);

                    txtDocumentNo.Text = lgsPurchaseHeader.DocumentNo;
                    txtGrossAmount.Text=Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.NetAmount);

                    if (!lgsPurchaseHeader.ReferenceDocumentID.Equals(0))
                    {
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        txtPurchaseOrderNo.Text = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(lgsPurchaseHeader.ReferenceDocumentID).DocumentNo;
                    }
                    else
                    {
                        txtPurchaseOrderNo.Text = string.Empty;
                    }

                    txtReferenceNo.Text=lgsPurchaseHeader.ReferenceNo;
                    txtRemark.Text=lgsPurchaseHeader.Remark;
                    supplier = supplierService.GetLgsSupplierByID(lgsPurchaseHeader.SupplierID);
                    txtSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierName.Text = supplier.SupplierName;
                    txtSupplierInvoiceNo.Text=lgsPurchaseHeader.SupplierInvoiceNo;

                    if (!lgsPurchaseHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    lgsPurchaseDetailTempList = lgsPurchaseService.GetPausedPurchaseDetail(lgsPurchaseHeader);
                    dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    otherExpenceTempList = lgsPurchaseService.getPausedExpence(lgsPurchaseHeader);
                    dgvAdvanced.DataSource = otherExpenceTempList;
                    dgvAdvanced.Refresh();

                    dgvPaymentDetails.DataSource = null;
                    paymentTempList = lgsPurchaseService.getPausedPayment(lgsPurchaseHeader);
                    dgvPaymentDetails.DataSource = paymentTempList;
                    dgvPaymentDetails.Refresh();

                    lgsProductSerialNoTempList = lgsPurchaseService.getPausedPurchaseSerialNoDetail(lgsPurchaseHeader);

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
                    Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);

                    if (lgsPurchaseHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        tabGRN.Enabled = true;
                        grpFooter.Enabled = true;
                        EnableLine(true);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableButton(false, btnDocumentDetails, btnPoDetails);
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                    }
                    else
                    {

                    }

                    dtpExpiry.Value = DateTime.Now;
                    this.Cursor = Cursors.Default;
                    return true;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }


        public override void ClearForm()
        {
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            dgvPaymentDetails.DataSource = null;
            dgvPaymentDetails.Refresh();
            dgvAdvanced.DataSource = null;
            dgvAdvanced.Refresh();
            tabGRN.SelectedTab = tbpGeneral;
            base.ClearForm();
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                        RecallDocument(txtDocumentNo.Text.Trim());
                    else
                    {
                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtSupplierCode.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPoDetails_Click(object sender, EventArgs e)
        {
            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
            DataView dvAllReferenceData = new DataView(lgsPurchaseOrderService.GetAllPODocumentNumbersToGRN(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())));
            if (dvAllReferenceData.Count > 0)
            {
                LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Purchase Orders", "", txtPurchaseOrderNo);
                txtPurchaseOrderNo_Leave(this, e);
            }
        }

        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                {
                    referenceSearch = new FrmReferenceSearch();
                }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        {
                            return;
                        }
                    }
                }

                referenceSearch.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetSerialNoList(List<InvProductSerialNoTemp> setLgsProductSerialNoTemp, bool isValidNoOfSerialNo)
        {
           
            lgsProductSerialNoTempList = setLgsProductSerialNoTemp;
            if (isValidNoOfSerialNo)
            {
                txtFreeQty.Enabled = true;
                //this.ActiveControl = txtFreeQty;
                txtFreeQty.Focus();
                
            }
            else
            {
                txtQty.Focus();
            }
        }

        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                    if (dgvItemDetails.CurrentCell != null &&  dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        LgsPurchaseDetailTemp lgsPurchaseTempDetail = new LgsPurchaseDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsPurchaseTempDetail.ProductID =lgsProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        
                        //if (existingInvProductMaster.IsExpiry.Equals(true))
                        //    invPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        lgsPurchaseTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsPurchaseService LgsPurchaseServices = new LgsPurchaseService();

                        dgvItemDetails.DataSource = null;
                        lgsPurchaseDetailTempList = LgsPurchaseServices.getDeletePurchaseDetailTemp(lgsPurchaseDetailTempList, lgsPurchaseTempDetail, lgsProductSerialNoTempList, out lgsProductSerialNoTempList);
                        dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }


                        GetSummarizeFigures(lgsPurchaseDetailTempList);
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }


        private void resetPayment()
        {
            txtCardChequeNo.Text = string.Empty;
            txtCardChequeNo.Enabled = false;
            dtpChequeDate.Value = Common.FormatDate(DateTime.Now);
            dtpChequeDate.Enabled = false;

            Common.EnableComboBox(false, cmbBankCode, cmbBankName);
            Common.ClearComboBox(cmbBankCode, cmbBankName,cmbPaymentMethod);

            txtPayingAmount.Text = "0.00";
            txtPayingAmount.Enabled = false;
        }

        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPaymentMethod_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmbPaymentMethod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    PaymentMethodService paymentMethodService = new PaymentMethodService();

                    paymentType = paymentMethodService.GetPaymentMethodsByName((cmbPaymentMethod.Text.ToString())).PaymentType;

                    txtCardChequeNo.Enabled = false;
                    dtpChequeDate.Enabled = false;
                    cmbBankCode.Enabled = false;
                    cmbBankName.Enabled = false;

                    // cmbBankCode.SelectedIndex = -1;

                    Common.EnableComboBox(false, cmbBankCode, cmbBankName);
                    Common.ClearComboBox(cmbBankCode, cmbBankName);


                    if (paymentType.Equals(0))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(1))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(2))
                    {
                        txtPayingAmount.Enabled = true;
                        //Common.EnableComboBox(true, cmbBankCode, cmbBankName);
                        cmbBankCode.Enabled = true;
                        cmbBankName.Enabled = true;

                        dtpChequeDate.Enabled = true;
                        txtCardChequeNo.Enabled = true;

                        txtCardChequeNo.Focus();
                        return;
                    }
                    else if (paymentType.Equals(3))
                    {
                        txtPayingAmount.Enabled = true;
                        //dtpChequeDate.Enabled = true;
                        txtCardChequeNo.Enabled = true;
                        //Common.EnableComboBox(true, cmbBankCode, cmbBankName);
                        cmbBankCode.Enabled = true;
                        cmbBankName.Enabled = true;
                        txtCardChequeNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetailsAccordingToPurchaseOrder(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();

                if (strProduct.Equals(string.Empty)) { return; }

                LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = LgsProductMasterService.GetProductsByCode(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else { existingLgsProductMaster = LgsProductMasterService.GetProductsByName(strProduct); ;}

                if (existingLgsProductMaster != null)
                {
                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                    if (lgsPurchaseDetailTempList == null) { lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>(); }

                    existingILgsPurchaseDetailTemp = lgsPurchaseServices.GetPurchaseDetailTempForRecallPO(lgsPurchaseDetailTempList, existingLgsProductMaster, unitofMeasureID);

                    if (existingILgsPurchaseDetailTemp != null)
                    {
                        txtProductCode.Text = existingILgsPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingILgsPurchaseDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingILgsPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingILgsPurchaseDetailTemp.Qty);
                        txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingILgsPurchaseDetailTemp.FreeQty);
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.DiscountPercentage);
                        if (existingLgsProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingILgsPurchaseDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingILgsPurchaseDetailTemp.ExpiryDate.ToString()));
                            dtpExpiry.Enabled = true;
                        }
                        else
                        {
                            dtpExpiry.Value = Common.ConvertDateTimeToDate(DateTime.Now);
                            dtpExpiry.Enabled = false;
                        }
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                            cmbUnit.Focus();
                    }
                    else
                    {
                        Toast.Show("Unit " + cmbUnit.Text.Trim() + " not exists for product", Toast.messageType.Information, Toast.messageAction.General);
                        //cmbUnit.Enabled = true;
                        //cmbUnit.Focus();
                        return;
                    }
                }

                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;

                    if (recallPo)
                    {
                        loadProductDetailsAccordingToPurchaseOrder(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())));
                    }
                    else
                    {
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())));
                    }

                    CalculateLine();
                    if (recallPo)
                    {
                        if (existingLgsProductMaster.IsExpiry)
                        {
                            cmbUnit.Enabled = false;
                            dtpExpiry.Enabled = true;
                            this.ActiveControl = dtpExpiry;
                            dtpExpiry.Focus();
                        }
                        else
                        {
                            cmbUnit.Enabled = false;
                            txtQty.Enabled = true;
                            this.ActiveControl = txtQty;
                            txtQty.Focus();
                        }
                    }
                    else
                    {
                        txtQty.Enabled = true;
                        this.ActiveControl = txtQty;
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpExpiry_Leave(object sender, EventArgs e)
        {
            try
            {
                if (recallPo)
                {
                    loadProductDetailsAccordingToPurchaseOrder(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
                }
                else
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);
                }

                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    txtQty.Text = "1";
                txtQty.Focus();

                if (recallPo) { cmbUnit.Enabled = false; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           // Load
        }

        private void txtCardChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab)) && cmbPaymentMethod.Text.Trim() != string.Empty)
                {
                    if (paymentType.Equals(0))
                    {

                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(1))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                        return;
                    }
                    else if (paymentType.Equals(2))
                    {
                        txtPayingAmount.Enabled = true;
                        dtpChequeDate.Enabled = true;
                        dtpChequeDate.Focus();
                        return;
                    }
                    else if (paymentType.Equals(3))
                    {
                        txtPayingAmount.Enabled = true;
                        cmbBankCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab)) && cmbPaymentMethod.Text.Trim() != string.Empty)
            {
                cmbBankCode.Focus();
            }
        }

        private void dgvPaymentDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvPaymentDetails.CurrentCell != null && dgvPaymentDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Payment " + dgvPaymentDetails["PaymentMethod", dgvPaymentDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvPaymentDetails["PayAmount", dgvPaymentDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        PaymentTemp paymentTemp = new PaymentTemp();
                        paymentTemp = paymentTempList.Where(p => p.PaymentTempID.Equals(Common.ConvertStringToLong(dgvPaymentDetails["PaymentMethod", dgvPaymentDetails.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();

                        if (paymentTemp!=null)
                            paymentTempList.Remove(paymentTemp);
                        dgvPaymentDetails.DataSource = null;
                        dgvPaymentDetails.DataSource = paymentTempList;
                        dgvPaymentDetails.Refresh();

                        txtPaidAmount.Text = Common.ConvertDecimalToStringCurrency(paymentTempList.Sum(p => p.PayAmount));

                        cmbPaymentMethod.SelectedValue = 0;
                        resetPayment();
                        cmbPaymentMethod.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void dgvPaymentDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPaymentDetails.CurrentCell != null && dgvPaymentDetails.CurrentCell.RowIndex >= 0)
                {
                    PaymentTemp paymentTemp = new PaymentTemp();
                    paymentTemp = paymentTempList.Where(p => p.PaymentTempID.Equals(Common.ConvertStringToLong(dgvPaymentDetails["PaymentTempID", dgvPaymentDetails.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();

                    if (paymentTemp != null)
                    {
                        cmbPaymentMethod.SelectedValue = paymentTemp.PaymentMethodID;
                        paymentType = paymentTemp.PaymentMethodID;
                        txtPayingAmount.Text = Common.ConvertDecimalToStringCurrency(paymentTemp.PayAmount);
                        txtCardChequeNo.Text = paymentTemp.CardCheqNo;
                        if (paymentTemp.ChequeDate != null)
                            dtpChequeDate.Value = Common.ConvertStringToDate(paymentTemp.ChequeDate.ToString());
                        else
                            dtpChequeDate.Value = DateTime.Now;

                        cmbBankCode.Text = paymentTemp.BankCode;
                        cmbBankName.Text = paymentTemp.BankName;

                        if (paymentTemp.PaymentMethodID.Equals(2))
                            paymentTemp.ChequeDate = dtpChequeDate.Value;
                        else
                            paymentTemp.ChequeDate = null;


                        txtPayingAmount.Enabled = true;

                        if (paymentType.Equals(2))
                        {
                            cmbBankCode.Enabled = true;
                            cmbBankName.Enabled = true;
                            dtpChequeDate.Enabled = true;
                            txtCardChequeNo.Enabled = true;
                        }
                        else if (paymentType.Equals(3))
                        {
                            txtCardChequeNo.Enabled = true;
                            cmbBankCode.Enabled = true;
                            cmbBankName.Enabled = true;
                        }

                        txtPayingAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationOtherExpence_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtOtherExpenceCode, accLedgerAccountService.GetExpenceLedgerCodes(), chkAutoCompleationOtherExpence.Checked);
                Common.SetAutoComplete(txtOtherExpenceName, accLedgerAccountService.GetExpenceLedgerNames(), chkAutoCompleationOtherExpence.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtOtherExpenceCode.Text.Trim().Equals(string.Empty))
                    {
                        txtOtherExpenceName.Enabled = true;
                        txtOtherExpenceName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceCode_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadOtherExpences(true, txtOtherExpenceCode.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadOtherExpences(false, txtOtherExpenceName.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {

                    if (!txtOtherExpenceName.Text.Trim().Equals(string.Empty))
                    {
                        txtOtherExpenceValue.Enabled = true;
                        txtOtherExpenceValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalCurrency(txtOtherExpenceValue.Text.Trim()) > 0)
                    {
                        OtherExpenseTemp otherExpenceTemp = new OtherExpenseTemp();
                        AccLedgerAccountService accLedgerAccountService = new Service.AccLedgerAccountService();

                        otherExpenceTemp.AccLedgerAccountID = accLedgerAccountService.GetAccLedgerAccountByCode(txtOtherExpenceCode.Text.Trim()).AccLedgerAccountID;
                        otherExpenceTemp.ExpenseAmount = Common.ConvertStringToDecimalCurrency(txtOtherExpenceValue.Text.Trim());
                        otherExpenceTemp.LedgerCode = txtOtherExpenceCode.Text.Trim();
                        otherExpenceTemp.LedgerName = txtOtherExpenceName.Text.Trim();

                        if (otherExpenceTempList == null)
                            otherExpenceTempList = new List<OtherExpenseTemp>();

                        OtherExpenseTemp otherExpenceTempRemove = new OtherExpenseTemp();

                        otherExpenceTempRemove = otherExpenceTempList.Where(p => p.AccLedgerAccountID.Equals(otherExpenceTemp.AccLedgerAccountID)).FirstOrDefault();

                        if (otherExpenceTempRemove != null)
                            otherExpenceTempList.Remove(otherExpenceTempRemove);
                        otherExpenceTempList.Add(otherExpenceTemp);
                        dgvAdvanced.DataSource = null;
                        dgvAdvanced.DataSource = otherExpenceTempList;
                        dgvAdvanced.Refresh();

                        txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherExpenceTempList.Sum(p => p.ExpenseAmount));
                        GetSummarizeSubFigures();

                        Common.ClearTextBox(txtOtherExpenceCode, txtOtherExpenceName, txtOtherExpenceValue);
                        txtOtherExpenceCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvAdvanced_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F2))
                {
                    try
                    {
                        if (dgvAdvanced.CurrentCell != null && dgvAdvanced.CurrentCell.RowIndex >= 0)
                        {
                            if (Toast.Show("Other Expences " + dgvAdvanced["LedgerName", dgvAdvanced.CurrentCell.RowIndex].Value.ToString() + " - " + dgvAdvanced["ExpenseAmount", dgvAdvanced.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                                return;

                            OtherExpenseTemp otherExpenceTempRemove = new OtherExpenseTemp();

                            otherExpenceTempRemove = otherExpenceTempList.Where(p => p.AccLedgerAccountID.Equals(Common.ConvertStringToLong(dgvAdvanced["AccLedgerAccountID", dgvAdvanced.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();
                            if (otherExpenceTempRemove != null)
                                otherExpenceTempList.Remove(otherExpenceTempRemove);
                            dgvAdvanced.DataSource = null;
                            dgvAdvanced.DataSource = otherExpenceTempList;
                            dgvAdvanced.Refresh();

                            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherExpenceTempList.Sum(p => p.ExpenseAmount));
                            GetSummarizeSubFigures();

                            Common.ClearTextBox(txtOtherExpenceCode, txtOtherExpenceName, txtOtherExpenceValue);
                            txtOtherExpenceCode.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvAdvanced_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvAdvanced.CurrentCell != null && dgvAdvanced.CurrentCell.RowIndex >= 0)
                {
                    OtherExpenseTemp otherExpenceTemp = new OtherExpenseTemp();
                    otherExpenceTemp = otherExpenceTempList.Where(p => p.AccLedgerAccountID.Equals(Common.ConvertStringToLong(dgvAdvanced["AccLedgerAccountID", dgvAdvanced.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();

                    if (otherExpenceTemp != null)
                    {
                        Common.ClearTextBox(txtOtherExpenceCode, txtOtherExpenceName, txtOtherExpenceValue);
                        txtOtherExpenceCode.Text = otherExpenceTemp.LedgerCode;
                        txtOtherExpenceName.Text = otherExpenceTemp.LedgerName;
                        txtOtherExpenceValue.Text = Common.ConvertDecimalToStringCurrency(otherExpenceTemp.ExpenseAmount);
                        txtOtherExpenceValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBankCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbBankCode.Text.Trim().Equals(string.Empty))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbBankCode.Text.Trim().Equals(string.Empty))
                    {
                        txtPayingAmount.Enabled = true;
                        txtPayingAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    //if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    //    RecallPurchaseOrder(txtPurchaseOrderNo.Text.Trim());
                    txtReferenceNo.Enabled = true;
                    txtReferenceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallPurchaseOrder(string documentNo)
        {
            try
            {
                recallPo = true;
                this.Cursor = Cursors.WaitCursor;
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();

                lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetLgsPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);
                if (lgsPurchaseOrderHeader != null)
                {
                    if (lgsPurchaseOrderHeader.ExpiryDate >= Common.FormatDate(DateTime.Now))
                    {
                        EnableControl(true);
                        LgsSupplierService supplierService = new LgsSupplierService();
                        LgsSupplier supplier = new LgsSupplier();

                        supplier = supplierService.GetLgsSupplierByID(lgsPurchaseOrderHeader.LgsSupplierID);

                        chkTStatus.Checked = lgsPurchaseOrderHeader.IsUpLoad;
                        if (supplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); }{ Common.EnableCheckBox(true, chkTStatus); }

                        cmbLocation.SelectedValue = lgsPurchaseOrderHeader.LocationID;
                        cmbLocation.Refresh();
                        cmbPaymentTerms.SelectedValue = lgsPurchaseOrderHeader.PaymentMethodID;
                        cmbPaymentTerms.Refresh();

                        supplier = supplierService.GetLgsSupplierByID(lgsPurchaseOrderHeader.LgsSupplierID);
                        txtSupplierCode.Text = supplier.SupplierCode;
                        txtSupplierName.Text = supplier.SupplierName;


                        if (lgsPurchaseOrderHeader.TaxAmount.Equals(0))
                        {
                            chkTaxEnable.Checked = false;
                            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        }
                        else
                        {
                            chkTaxEnable.Checked = true;
                            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.TaxAmount);
                        }


                        if (lgsPurchaseOrderHeader.DiscountPercentage.Equals(0))
                        {
                            chkSubTotalDiscountPercentage.Checked = false;
                            txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.DiscountAmount);
                        }
                        else
                        {
                            chkSubTotalDiscountPercentage.Checked = true;
                            txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.DiscountPercentage);
                        }

                        txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.DiscountAmount);
                        txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.OtherCharges);

                        dtpGrnDate.Value = Common.FormatDate(DateTime.Now);
                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.GrossAmount);
                        txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.NetAmount);
                        txtPurchaseOrderNo.Text = lgsPurchaseOrderHeader.DocumentNo;
                        txtReferenceNo.Text = string.Empty; ;
                        txtRemark.Text = lgsPurchaseOrderHeader.Remark;
                        
                        txtSupplierInvoiceNo.Text = string.Empty;

                        dgvItemDetails.DataSource = null;
                        lgsPurchaseDetailTempList = lgsPurchaseService.GetPurchaseOrderDetail(lgsPurchaseOrderHeader);
                        dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                        dgvItemDetails.Refresh();

                        dgvAdvanced.DataSource = null;
                        dgvAdvanced.Refresh();

                        dgvPaymentDetails.DataSource = null;
                        dgvPaymentDetails.Refresh();

                        Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo, txtProductCode, txtProductName);
                        Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);

                        tabGRN.Enabled = true;
                        grpFooter.Enabled = true;
                        grpBody.Enabled = true;
                        EnableLine(false);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableButton(false, btnDocumentDetails, btnPoDetails);
                        this.ActiveControl = txtProductCode;
                        txtProductCode.Focus();
                        dtpExpiry.Value = DateTime.Now;

                        LoadProductsAccordingToPurchaseOrder();

                        if (chkTaxEnable.Checked) { GetSummarizeSubFigures(); }


                        this.Cursor = Cursors.Default;
                        return true;
                    }
                    else
                    {
                        Toast.Show("PO No : "+ txtPurchaseOrderNo.Text.Trim()+" is Expired", Toast.messageType.Information, Toast.messageAction.General);
                        this.Cursor = Cursors.Default;
                        return false;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        private void chkAutoCompleationPoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load purchase order document numbers
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, lgsPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load GRN Document Numbers
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbPaymentTerms_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    dtpGrnDate.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            GetSummarizeSubFigures();
        }

        private void txtPayingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                try
                {
                    PaymentTemp paymentTemp = new PaymentTemp();
                    paymentTemp.AccLedgerAccountID = Common.ConvertStringToLong(cmbPaymentMethod.SelectedValue.ToString());
                    paymentTemp.PayAmount = Common.ConvertStringToDecimal(txtPayingAmount.Text.Trim());
                    paymentTemp.CardCheqNo = txtCardChequeNo.Text.Trim();
                    if (paymentType.Equals(2))
                        paymentTemp.ChequeDate = dtpChequeDate.Value;
                    else
                        paymentTemp.ChequeDate = null;
                    paymentTemp.PaymentMethod = cmbPaymentMethod.Text.Trim();
                    paymentTemp.PaymentMethodID = paymentType; ;

                    paymentTemp.BankCode = cmbBankCode.Text.ToString();
                    paymentTemp.BankName = cmbBankName.Text.ToString();

                    BankService bankService = new BankService();
                    Bank bank = new Bank();
                    bank = bankService.GetBankByCode(paymentTemp.BankCode);

                    if (bank != null)
                        paymentTemp.BankID = bank.BankID;
                    else
                        paymentTemp.BankID = 0;

                    if (paymentTempList == null)
                        paymentTempList = new List<PaymentTemp>();


                    if (Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.Trim()) < ((Common.ConvertStringToDecimalCurrency(txtPayingAmount.Text.Trim())) + (Common.ConvertStringToDecimalCurrency(txtPaidAmount.Text.Trim()))))
                    {
                        if (Toast.Show("You are trying to enter over payment\nDo you want to continue?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.No))
                            return;

                    }

                    PaymentTemp paymentTempRemove = new PaymentTemp();

                    paymentTempRemove = paymentTempList.Where(p => p.CardCheqNo.Equals(txtCardChequeNo.Text.Trim()) && p.PaymentMethodID.Equals(paymentType)).FirstOrDefault();


                    if (paymentTempRemove != null)
                        paymentTempList.Remove(paymentTempRemove);
                    paymentTempList.Add(paymentTemp);
                    dgvPaymentDetails.DataSource = null;
                    dgvPaymentDetails.DataSource = paymentTempList;
                    dgvPaymentDetails.Refresh();

                    txtPaidAmount.Text = Common.ConvertDecimalToStringCurrency(paymentTempList.Sum(p => p.PayAmount));

                    cmbPaymentMethod.SelectedValue = 0;
                    resetPayment();
                    cmbPaymentMethod.Focus();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSubTotalDiscountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkSubTotalDiscountPercentage.Checked)
                {
                    decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                    if (value >= 101)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountPercentageExceed);
                        //txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(value);
                        txtSubTotalDiscountPercentage.Focus();
                        txtSubTotalDiscountPercentage.SelectAll();
                    }
                }
                else
                {
                    decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                    decimal grossAmt = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim());

                    if (value > grossAmt)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountAmountExceed);
                        //txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(value);
                        txtSubTotalDiscountPercentage.Focus();
                        txtSubTotalDiscountPercentage.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetPrintingDetails()
        {

            foreach (string printname in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(printname);
            }


            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
            {
                cmbPaperSize.Items.Add(size.PaperName);
            }

            rdoPortrait.Checked = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void txtPurchaseOrderNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPurchaseOrderNo.Text.Trim())) { return; }
                RecallPurchaseOrder(txtPurchaseOrderNo.Text.Trim());
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
      
   }
}
