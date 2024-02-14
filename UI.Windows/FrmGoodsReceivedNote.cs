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
using Report.Inventory;

namespace UI.Windows
{
    public partial class FrmGoodsReceivedNote : UI.Windows.FrmBaseTransactionForm
    {

        /// <summary>
        /// By - Pravin
        /// 26/07/2013
        /// Good Received Note
        /// </summary>
        /// 
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();
        InvProductMaster existingInvProductMaster;
        AccLedgerAccount existingAccLedgerAccount;
        InvPurchaseDetailTemp existingIInvPurchaseDetailTemp =new InvPurchaseDetailTemp();
        List<InvProductSerialNoTemp> invProductSerialNoTempList;
        List<PaymentTemp> paymentTempList;
        List<OtherExpenseTemp> otherExpenceTempList;
        bool isSupplierProduct;
        int documentID;
        int paymentType;
        bool poIsMandatory = true;
        bool recallPo;
        bool isBackDated;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
        Supplier existingSupplier;
        string oldSupplierCode = string.Empty;
        public FrmGoodsReceivedNote()
        {
            InitializeComponent();
        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "")
                {
                    SupplierService supplierService = new SupplierService();

                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID, 1, (Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.Trim())));
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
                InvPurchaseService invPurchaseServices = new InvPurchaseService();
                LocationService locationService = new LocationService();
                return invPurchaseServices.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                if (isSupplierProduct)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                    Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodesBySupplier(supplier.SupplierID), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNamesBySupplier(supplier.SupplierID), chkAutoCompleationProduct.Checked);
                }
                else
                {
                    Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
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
                InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                if (invPurchaseOrderHeader != null)
                {
                    Common.SetAutoComplete(txtProductCode, invPurchaseOrderService.GetProductCodesAccordingToPurchaseOrder(invPurchaseOrderHeader), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, invPurchaseOrderService.GetProductNamesAccordingToPurchaseOrder(invPurchaseOrderHeader), chkAutoCompleationProduct.Checked);
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

                SupplierService supplierService = new SupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked);

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

                if (isBackDated)
                {
                    dtpGrnDate.Enabled = true;
                }
                else
                {
                    dtpGrnDate.Enabled = false;
                }
                
                base.FormLoad();

                //Load purchase order document numbers
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, invPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);

                //Load GRN Document Numbers
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, invPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

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
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                poIsMandatory = autoGenerateInfo.PoIsMandatory;
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
                invPurchaseDetailTempList = null;
                existingInvProductMaster = null;
                existingIInvPurchaseDetailTemp = null;
                invProductSerialNoTempList = null;
                paymentTempList = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                resetPayment();
                txtDocumentNo.Text = GetDocumentNo(true);
                recallPo = false;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;
                tabGRN.TabPages.Remove(tbpAdvanced);

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
                SupplierService supplierService = new SupplierService();
                existingSupplier = new Supplier();

                if (isCode)
                {
                    existingSupplier = supplierService.GetSupplierByCode(strsupplier);
                    if (isCode && strsupplier.Equals(string.Empty))
                    {
                        txtSupplierName.Focus();
                        return;
                    }
                }
                else { existingSupplier = supplierService.GetSupplierByName(strsupplier); }
                    
                if (existingSupplier != null)
                {
                    txtSupplierCode.Text = existingSupplier.SupplierCode;
                    txtSupplierName.Text = existingSupplier.SupplierName;
                    chkTStatus.Checked = existingSupplier.IsUpload;
                    if (existingSupplier.Remark != null)
                    {
                        oldSupplierCode = existingSupplier.Remark;
                    }

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
                SupplierService supplierService = new SupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked);
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
                    SupplierService supplierService = new SupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveSuppliersDataTable());
                    if (dvAllReferenceData.Count != 0)
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
                    SupplierService supplierService = new SupplierService();
                    DataView dvAllReferenceData = new DataView(supplierService.GetAllActiveSuppliersDataTable());
                    if (dvAllReferenceData.Count != 0)
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
                    cmbPaymentTerms.Enabled = true;
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
                SupplierService supplierService = new SupplierService();

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                if (supplierService.IsExistsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
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

                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    if (!invPurchaseServices.ValidateExistingProduct(true, txtProductCode.Text.Trim(), txtPurchaseOrderNo.Text.Trim()))
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
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty)) { return; }
                    
                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {
                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else { existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); ;}
                    
                if (existingInvProductMaster != null)
                {
                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    if (invPurchaseDetailTempList == null) { invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>(); }

                    if (recallPo) 
                    {
                        //InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        //InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        //invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                        //existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempForRecallPO(invPurchaseOrderHeader, existingInvProductMaster.InvProductMasterID);
                        existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTemp(invPurchaseDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID); 
                    }
                    else { existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTemp(invPurchaseDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID); }
                    
                    if (existingIInvPurchaseDetailTemp!= null) 
                    {
                        txtProductCode.Text = existingIInvPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingIInvPurchaseDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingIInvPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingIInvPurchaseDetailTemp.Qty);
                        txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingIInvPurchaseDetailTemp.FreeQty);
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.DiscountPercentage);
                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingIInvPurchaseDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingIInvPurchaseDetailTemp.ExpiryDate.ToString()));
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
            Common.EnableTextBox(enable, txtQty, txtFreeQty, txtCostPrice,txtWavgDis, txtSellingPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode,txtProductName,txtWavgDis,txtOtherDis,txtQty, txtFreeQty, txtCostPrice, txtSellingPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now; ;
            txtProductCode.Focus();
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        public override void View()
        {
            //base.View();
            if ((txtDocumentNo.Text.Trim().Substring(0,3))=="GRN")
            {
                GenerateReport(txtDocumentNo.Text.Trim(), 1);
            }
            else
            {
                GenerateReport(txtDocumentNo.Text.Trim(), 0);
            }

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
                    else
                    {
 
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

                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    if (invPurchaseServices.ValidateExistingProduct(true, txtProductName.Text.Trim(), txtPurchaseOrderNo.Text.Trim()))
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

                if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                {
                    InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                    if (invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                    {
                        Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                        dtpExpiry.Enabled = false;
                        cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
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

                if (existingInvProductMaster.IsExpiry.Equals(true))
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
                    //txtFreeQty.Enabled = true;
                    //txtFreeQty.Focus();
                    txtCostPrice.Enabled = true;
                    txtCostPrice.Focus();
                    txtCostPrice.SelectAll();
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

                    InvProductMaster invProductMaster = new InvProductMaster();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    invProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (invProductMaster != null)
                    {
                        InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                        if (!invPurchaseOrderService.IsValidNoOfQty(qty, invProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseOrderHeader.InvPurchaseOrderHeaderID))
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
                        Toast.Show("Quantity", Toast.messageType.Information, Toast.messageAction.ZeroQty);

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
                    if (existingInvProductMaster.IsSerial)
                    {
                        InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                        invProductSerialNoTemp.DocumentID = documentID;
                        if (existingInvProductMaster.IsExpiry.Equals(true))
                            invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                        invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        InvPurchaseService invPurchaseServices = new InvPurchaseService();

                        if (invPurchaseServices.IsValidNoOfSerialNo(invProductSerialNoTempList, invProductSerialNoTemp, (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))))
                        {
                            txtCostPrice.Enabled = true;
                            txtCostPrice.Focus();
                        }
                        else
                        {
                            FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), FrmSerial.transactionType.GoodReceivedNote);
                            frmSerial.ShowDialog();
                        }
                    }
                    else
                    {
                        //txtSellingPrice.Enabled = true;
                        //txtSellingPrice.Focus();
                        //txtSellingPrice.SelectAll();
                        txtCostPrice.Enabled = true;
                        txtCostPrice.Focus();
                        txtCostPrice.SelectAll();
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F3))
            {
                if (existingInvProductMaster.IsSerial)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                    invProductSerialNoTemp.DocumentID = documentID;
                    if (existingInvProductMaster.IsExpiry.Equals(true))
                        invProductSerialNoTemp.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                    invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                    invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                    if (invProductSerialNoTempList == null)
                        invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                    FrmSerial frmSerial = new FrmSerial(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()),FrmSerial.transactionType.GoodReceivedNote);
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

                    InvProductMaster invProductMaster = new InvProductMaster();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    invProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (invProductMaster != null)
                    {
                        InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                        if (!invPurchaseOrderService.IsValidNoOfFreeQty(freeQty, invProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseOrderHeader.InvPurchaseOrderHeaderID))
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
                    txtCostPrice.SelectAll();
                    txtCostPrice.Enabled = true;
                    txtCostPrice.Focus();
                    txtCostPrice.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text) > 0)
                {
                    CalculateLine();
                }
                else
                {
                    Toast.Show("Amount", Toast.messageType.Information, Toast.messageAction.ZeroAmount);

                    txtCostPrice.Focus();
                    txtCostPrice.SelectAll();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    //txtSellingPrice.Enabled = true;
                    //txtSellingPrice.Focus();
                    //txtProductDiscountPercentage.Enabled = true;
                    //txtProductDiscountPercentage.Focus();
                    //txtProductDiscountPercentage.SelectAll();
                    txtWavgDis.Enabled = true;
                    txtWavgDis.Focus();
                    txtWavgDis.SelectAll();

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

        private void UpdateGrid(InvPurchaseDetailTemp invPurchaseTempDetail)
        {
            try
            {
                decimal qty = 0;
                decimal freeQty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0))
                {
                    CalculateLine();

                    if (existingInvProductMaster.IsExpiry.Equals(true))
                        invPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString());
                    invPurchaseTempDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invPurchaseTempDetail.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
                    invPurchaseTempDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();
                    if (poIsMandatory == true)
                    {
                        invPurchaseTempDetail.BatchNo = txtPurchaseOrderNo.Text.ToString().Trim();
                    }
                    else
                    {
                        invPurchaseTempDetail.BatchNo = txtDocumentNo.Text.ToString().Trim();
                    }

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
                        qty = (invPurchaseTempDetail.Qty + Common.ConvertStringToDecimalQty(txtQty.Text.Trim()));
                        freeQty = (invPurchaseTempDetail.FreeQty + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim()));
                    }
                   

                    if (!chkOverwrite.Checked)
                    {
                        if ((!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty)))
                        {
                            if (existingInvProductMaster != null)
                            {
                                InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                                invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());

                                if (!invPurchaseOrderService.IsValidNoOfQty(qty, existingInvProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseOrderHeader.InvPurchaseOrderHeaderID))
                                {
                                    Toast.Show("Invalid Qty.\nQty cannot grater then GRN Qty", Toast.messageType.Information, Toast.messageAction.General, "");
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
                        invPurchaseTempDetail.Qty = Common.ConvertDecimalToDecimalQty(qty);

                        invPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimal(txtWavgDis.Text.Trim());
                        invPurchaseTempDetail.CurrentQty = 0; //Common.ConvertStringToDecimal(txtOtherDis.Text.Trim()); //Common.ConvertDecimalToDecimalQty(freeQty);

                        invPurchaseTempDetail.GrossAmount = ((Common.ConvertDecimalToDecimalQty(qty)) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                        invPurchaseTempDetail.NetAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text));
                    }
                    else
                    {
                        CalculateLine();
                        invPurchaseTempDetail.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        //invPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text);
                        invPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimal(txtWavgDis.Text.Trim());
                        invPurchaseTempDetail.CurrentQty = 0;//; //Common.ConvertDecimalToDecimalQty(freeQty);

                        invPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                        invPurchaseTempDetail.NetAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text));
                    }

                    invPurchaseTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);
                    invPurchaseTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    invPurchaseTempDetail.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                    invPurchaseTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    invPurchaseTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text);

                    InvPurchaseService InvPurchaseServices = new Service.InvPurchaseService();

                    dgvItemDetails.DataSource = null;
                    invPurchaseDetailTempList = InvPurchaseServices.getUpdatePurchaseDetailTemp(invPurchaseDetailTempList, invPurchaseTempDetail, existingInvProductMaster, recallPo);
                    dgvItemDetails.DataSource = invPurchaseDetailTempList;
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

                    GetSummarizeFigures(invPurchaseDetailTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails);
                    ClearLine();
                    if (invPurchaseDetailTempList.Count > 0)
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
                invPurchaseTempDetail = null;
                txtProductCode.Focus();
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                    UpdateGrid(existingIInvPurchaseDetailTemp);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CalculateLine(decimal qty=0)
        {
            try
            {
                txtProductDiscount.Text = "0.00";
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                    {
                        //txtProductDiscount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()));
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
                        //txtProductDiscount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim())) * (Common.ConvertDecimalToDecimalQty(qty)), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()));

                    }
                    else
                    {
                        txtProductDiscount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim()).ToString();
                    }
                    txtProductAmount.Text = ((Common.ConvertDecimalToDecimalQty(qty)) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim())).ToString();
          
                }
                if (Common.ConvertStringToDecimal(txtWavgDis.Text.Trim()) > 0)
                {

                    txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency((Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim()) + (Common.ConvertStringToDecimal(txtWavgDis.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()))));
                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text.Trim())).ToString();

                
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void GetSummarizeFigures(List<InvPurchaseDetailTemp> listItem)
        {
            CommonService commonService = new CommonService();
            SupplierService supplierService = new SupplierService();

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, (grossAmount - discountAmount), supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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

        private decimal GetLineDiscountTotal(List<InvPurchaseDetailTemp> listItem)
        {           
            return listItem.GetSummaryAmount(x => x.DiscountAmount);          
        }

        private decimal GettotalQty(List<InvPurchaseDetailTemp> listItem) 
        {
            return listItem.GetSummaryAmount(x => x.Qty);     
        }

        private void GetSummarizeSubFigures()
        {
            CommonService commonService = new CommonService();
            SupplierService supplierService = new SupplierService();

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, (grossAmount - discountAmount), supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());


            decimal rDownNetAmount = 0, rDownDiscountAmount = 0;
            if (chkRound.Checked)
            {
                // Get Round down value
                rDownNetAmount = netAmount - netAmount % 10; // Get round off value from data base

                // Add Round down value to discount amount
                rDownDiscountAmount = netAmount - rDownNetAmount;
            }

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(bool.Equals(chkRound.Checked, false) ? discountAmount : (discountAmount + rDownDiscountAmount));
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(bool.Equals(chkRound.Checked, false) ? netAmount : rDownNetAmount);

            //txtTotalQty.Text = Common.ConvertDecimalToStringQty(GettotalQty(invPurchaseDetailTempList));
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
            InvPurchaseService invPurchaseService = new InvPurchaseService();
            Common.SetAutoComplete(txtDocumentNo, invPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

            //Load purchase order document numbers
            InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
            Common.SetAutoComplete(txtPurchaseOrderNo, invPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
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
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                if (!invPurchaseService.CheckExpiryDates(invPurchaseDetailTempList))
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
            InvPurchaseService invPurchaseService = new InvPurchaseService();
            if ((Toast.Show("Good Received Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                if (!invPurchaseService.CheckExpiryDates(invPurchaseDetailTempList))
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
                    //RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Good Received Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                InvPurchaseService invPurchaseService = new InvPurchaseService();
                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invPurchaseHeader = invPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (invPurchaseHeader == null)
                    invPurchaseHeader = new InvPurchaseHeader();
                //////if (documentStatus.Equals(1)) // update paused document
                //////{
                //////    documentNo = GetDocumentNo(false);
                //////    txtDocumentNo.Text = documentNo;
                //////}
                invPurchaseHeader.PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID;
                invPurchaseHeader.CompanyID = Location.CompanyID;
                invPurchaseHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                invPurchaseHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)                                
                    invPurchaseHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                invPurchaseHeader.OtherChargers = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invPurchaseHeader.DocumentDate = Common.FormatDate(dtpGrnDate.Value);
                invPurchaseHeader.DocumentID = documentID;
                invPurchaseHeader.DocumentStatus = documentStatus;
                invPurchaseHeader.DocumentNo = documentNo.Trim();
                invPurchaseHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invPurchaseHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invPurchaseHeader.LineDiscountTotal = GetLineDiscountTotal(invPurchaseDetailTempList);
                invPurchaseHeader.LocationID = Location.LocationID;
                invPurchaseHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invPurchaseHeader.PaymentMethodID = Convert.ToInt32(cmbPaymentTerms.SelectedValue);
                
                if(!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                {
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    InvPurchaseOrderHeader invPurchaseOrderHeader=new InvPurchaseOrderHeader();

                    invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentNumber(txtPurchaseOrderNo.Text.Trim());
                    invPurchaseHeader.ReferenceDocumentDocumentID = invPurchaseOrderHeader.DocumentID;
                    invPurchaseHeader.ReferenceDocumentID = invPurchaseOrderHeader.InvPurchaseOrderHeaderID;

                    if (poIsMandatory)
                    {
                        //invPurchaseHeader.BatchNo = invPurchaseOrderService.GetBatchNumberToGRN(invPurchaseOrderHeader.DocumentID, invPurchaseOrderHeader.InvPurchaseOrderHeaderID);
                        invPurchaseHeader.BatchNo = txtPurchaseOrderNo.Text.Trim();
                    }
                    else
                    {
                        invPurchaseHeader.BatchNo = invPurchaseHeader.DocumentNo;
                    }
                }

                invPurchaseHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invPurchaseHeader.Remark = txtRemark.Text.Trim();
                invPurchaseHeader.SupplierID = supplier.SupplierID;
                invPurchaseHeader.IsUpLoad = chkTStatus.Checked;
                invPurchaseHeader.SupplierInvoiceNo = txtSupplierInvoiceNo.Text.Trim();

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService=new CommonService();
                    invPurchaseHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, (invPurchaseHeader.GrossAmount-invPurchaseHeader.DiscountAmount), supplier.SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invPurchaseHeader.TaxAmount1 = tax1;
                    invPurchaseHeader.TaxAmount2 = tax2;
                    invPurchaseHeader.TaxAmount3 = tax3;
                    invPurchaseHeader.TaxAmount4 = tax4;
                    invPurchaseHeader.TaxAmount5 = tax5;
                   
                }
                
                //Payment
                ////invPurchaseHeader.CurrencyID = Common.ConvertStringToInt(cmbCurrencyType.SelectedValue.ToString());
                ////invPurchaseHeader.CurrencyRate = Common.ConvertStringToDecimalCurrency(txtCurrencyRate.Text.ToString());
                ////invPurchaseHeader.PaymentMethodID = Common.ConvertStringToInt(cmbPaymentMethod.SelectedValue.ToString().Trim());
                
                    //TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew,
                //   new TransactionOptions()
                //   {

                if (invPurchaseDetailTempList == null)
                    invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

                if (invProductSerialNoTempList == null)
                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                if (otherExpenceTempList == null)
                    otherExpenceTempList = new List<OtherExpenseTemp>();

                if (paymentTempList == null)
                    paymentTempList = new List<PaymentTemp>();

                InvPurchaseService.poIsMandatory = poIsMandatory;
                return invPurchaseService.Save(invPurchaseHeader, invPurchaseDetailTempList, invProductSerialNoTempList, out newDocumentNo, otherExpenceTempList, paymentTempList, Common.ConvertStringToDecimalCurrency(txtPaidAmount.Text.Trim()), this.Name);
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
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();

                invPurchaseHeader = invPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                if (invPurchaseHeader != null)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();

                    supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                    cmbLocation.SelectedValue = invPurchaseHeader.LocationID;
                    cmbLocation.Refresh();
                    cmbPaymentTerms.SelectedValue = invPurchaseHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    cmbCostCentre.SelectedValue = invPurchaseHeader.CostCentreID;
                    if (!invPurchaseHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.DiscountAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.OtherChargers );

                    dtpGrnDate.Value =Common.FormatDate(invPurchaseHeader.DocumentDate);

                    txtDocumentNo.Text = invPurchaseHeader.DocumentNo;
                    txtGrossAmount.Text=Common.ConvertDecimalToStringCurrency(invPurchaseHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.NetAmount);

                    if (!invPurchaseHeader.ReferenceDocumentID.Equals(0))
                    {
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        txtPurchaseOrderNo.Text = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(invPurchaseHeader.ReferenceDocumentID).DocumentNo;
                    }
                    else
                    {
                        txtPurchaseOrderNo.Text = string.Empty;
                    }

                    txtReferenceNo.Text=invPurchaseHeader.ReferenceNo;
                    txtRemark.Text=invPurchaseHeader.Remark;
                    supplier = supplierService.GetSupplierByID(invPurchaseHeader.SupplierID);
                    txtSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierName.Text = supplier.SupplierName;
                    txtSupplierInvoiceNo.Text=invPurchaseHeader.SupplierInvoiceNo;

                    if (!invPurchaseHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    invPurchaseDetailTempList = invPurchaseService.GetPausedPurchaseDetail(invPurchaseHeader);
                    dgvItemDetails.DataSource = invPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    otherExpenceTempList = invPurchaseService.getPausedExpence(invPurchaseHeader);
                    dgvAdvanced.DataSource = otherExpenceTempList;
                    dgvAdvanced.Refresh();

                    dgvPaymentDetails.DataSource = null;
                    paymentTempList = invPurchaseService.getPausedPayment(invPurchaseHeader);
                    dgvPaymentDetails.DataSource = paymentTempList;
                    dgvPaymentDetails.Refresh();

                    invProductSerialNoTempList = invPurchaseService.getPausedPurchaseSerialNoDetail(invPurchaseHeader);

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtPurchaseOrderNo);
                    Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);

                    if (invPurchaseHeader.DocumentStatus.Equals(0))
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
                        Common.EnableButton(true, btnView);
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
            existingSupplier = new Supplier();
            oldSupplierCode = string.Empty;
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
            
        }

        private void btnPoDetails_Click(object sender, EventArgs e)
        {
                  
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

        public void SetSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp,bool isValidNoOfSerialNo)
        {
           
            invProductSerialNoTempList = setInvProductSerialNoTemp;
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

                        InvPurchaseDetailTemp invPurchaseTempDetail = new InvPurchaseDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invPurchaseTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        
                        //if (existingInvProductMaster.IsExpiry.Equals(true))
                        //    invPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        invPurchaseTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvPurchaseService InvPurchaseServices = new InvPurchaseService();

                        dgvItemDetails.DataSource = null;
                        invPurchaseDetailTempList = InvPurchaseServices.getDeletePurchaseDetailTemp(invPurchaseDetailTempList, invPurchaseTempDetail, invProductSerialNoTempList, out invProductSerialNoTempList);
                        dgvItemDetails.DataSource = invPurchaseDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }
                        

                        GetSummarizeFigures(invPurchaseDetailTempList);
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
                    //else if (paymentType.Equals(1))
                    //{
                    //    txtPayingAmount.Enabled = true;
                    //    txtPayingAmount.Focus();
                    //    return;
                    //}
                    else if (paymentType.Equals(1))
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
                    else if (paymentType.Equals(2))
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetailsAccordingToPurchaseOrder(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty)) { return; }

                InvProductMasterService InvProductMasterService = new InvProductMasterService();

                if (isCode)
                {
                    existingInvProductMaster = InvProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else { existingInvProductMaster = InvProductMasterService.GetProductsByName(strProduct); ;}

                if (existingInvProductMaster != null)
                {
                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    if (invPurchaseDetailTempList == null) { invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>(); }

                    existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempForRecallPO(invPurchaseDetailTempList, existingInvProductMaster, unitofMeasureID);

                    if (existingIInvPurchaseDetailTemp != null)
                    {
                        txtProductCode.Text = existingIInvPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingIInvPurchaseDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingIInvPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.CostPrice);
                        txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.SellingPrice);
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingIInvPurchaseDetailTemp.Qty);
                        txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingIInvPurchaseDetailTemp.FreeQty);
                        txtProductDiscount.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.DiscountPercentage);
                        if (existingInvProductMaster.IsExpiry)
                        {
                            dtpExpiry.Value = Common.ConvertStringToDate((existingIInvPurchaseDetailTemp.ExpiryDate == null ? dtpExpiry.Value.ToString() : existingIInvPurchaseDetailTemp.ExpiryDate.ToString()));
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
                        if (existingInvProductMaster.IsExpiry)
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
            try
            {
                if ((e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab)) && cmbPaymentMethod.Text.Trim() != string.Empty)
                {
                    cmbBankCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherExpenceValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (Common.ConvertStringToDecimalCurrency(txtOtherExpenceValue.Text.Trim())>0)
                    {
                        OtherExpenseTemp otherExpenceTemp = new OtherExpenseTemp();
                        AccLedgerAccountService accLedgerAccountService=new Service.AccLedgerAccountService();

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

                        Common.ClearTextBox(txtOtherExpenceCode,txtOtherExpenceName,txtOtherExpenceValue);
                        txtOtherExpenceCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvAdvanced_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvAdvanced.CurrentCell!=null && dgvAdvanced.CurrentCell.RowIndex >= 0)
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPurchaseOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                        RecallPurchaseOrder(txtPurchaseOrderNo.Text.Trim());               
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallPurchaseOrder(string documentNo)
        {
            try
            {
                recallPo = true;
                this.Cursor = Cursors.WaitCursor;
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                InvPurchaseService invPurchaseService = new Service.InvPurchaseService();

                invPurchaseOrderHeader = invPurchaseOrderService.GetInvPurchaseOrderHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder").DocumentID, txtPurchaseOrderNo.Text.Trim(), Common.LoggedLocationID);
                if (invPurchaseOrderHeader != null)
                {
                    if (invPurchaseOrderHeader.ExpiryDate >= Common.FormatDate(DateTime.Now) || invPurchaseOrderHeader.IsAuthorized)
                    {
                        EnableControl(true);
                        SupplierService supplierService = new SupplierService();
                        Supplier supplier = new Supplier();

                        supplier = supplierService.GetSupplierByID(invPurchaseOrderHeader.SupplierID);
                        if (supplier.Remark != null)
                        {
                            oldSupplierCode = supplier.Remark;
                        }
                        chkTStatus.Checked = invPurchaseOrderHeader.IsUpLoad;
                        if (supplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); }{ Common.EnableCheckBox(true, chkTStatus); }

                        cmbLocation.SelectedValue = invPurchaseOrderHeader.LocationID;
                        cmbLocation.Refresh();
                        cmbPaymentTerms.SelectedValue = invPurchaseOrderHeader.PaymentMethodID;
                        cmbPaymentTerms.Refresh();

                        supplier = supplierService.GetSupplierByID(invPurchaseOrderHeader.SupplierID);
                        txtSupplierCode.Text = supplier.SupplierCode;
                        txtSupplierName.Text = supplier.SupplierName;


                        if (invPurchaseOrderHeader.TaxAmount.Equals(0))
                        {
                            chkTaxEnable.Checked = false;
                            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        }
                        else
                        {
                            chkTaxEnable.Checked = true;
                            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.TaxAmount);
                        }


                        if (invPurchaseOrderHeader.DiscountPercentage.Equals(0))
                        {
                            chkSubTotalDiscountPercentage.Checked = false;
                            txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.DiscountAmount);
                        }
                        else
                        {
                            chkSubTotalDiscountPercentage.Checked = true;
                            txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.DiscountPercentage);
                        }

                        txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.DiscountAmount);
                        txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.OtherCharges);

                        dtpGrnDate.Value = Common.GetSystemDate();
                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.GrossAmount);
                        txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.NetAmount);
                        txtPurchaseOrderNo.Text = invPurchaseOrderHeader.DocumentNo;
                        //txtReferenceNo.Text = string.Empty; ;
                        txtReferenceNo.Text = invPurchaseOrderHeader.ReferenceNo;
                        txtRemark.Text = invPurchaseOrderHeader.Remark;

                        txtSupplierInvoiceNo.Text = string.Empty;

                        dgvItemDetails.DataSource = null;
                        invPurchaseDetailTempList = invPurchaseService.GetPurchaseOrderDetail(invPurchaseOrderHeader);
                        dgvItemDetails.DataSource = invPurchaseDetailTempList;
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
                        dtpExpiry.Value = Common.GetSystemDate();

                        LoadProductsAccordingToPurchaseOrder();

                        GetSummarizeFigures(invPurchaseDetailTempList);


                        if (chkTaxEnable.Checked) { GetSummarizeSubFigures(); }


                        this.Cursor = Cursors.Default;
                        return true;
                    }
                    else
                    {
                        if (Common.UserGroupID.Equals(1))
                        {
                            if (Toast.Show("PO No : " + txtPurchaseOrderNo.Text.Trim() + " is Expired\nDo you want authorization?", Toast.messageType.Question, Toast.messageAction.General).Equals(DialogResult.Yes))
                            {
                                invPurchaseOrderService.AuthorizePurchaseOrder(invPurchaseOrderHeader);
                                Toast.Show("Authorization complete", Toast.messageType.Information, Toast.messageAction.General);
                                this.Cursor = Cursors.Default;
                            }
                            return false;
                        }
                        else
                        {
                            Toast.Show("PO No : " + txtPurchaseOrderNo.Text.Trim() + " is Expired\nContact administrator", Toast.messageType.Information, Toast.messageAction.General);
                            this.Cursor = Cursors.Default;
                            return false;
                        }
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
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                Common.SetAutoComplete(txtPurchaseOrderNo, invPurchaseOrderService.GetAllDocumentNumbersToGRN(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
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
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, invPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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
                    cmbLocation.Focus();
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
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus, txtSupplierCode.Text.Trim() + " " + txtSupplierName.Text.Trim(), oldSupplierCode, cmbPaymentMethod.Text.Trim(), txtPurchaseOrderNo.Text.Trim(),true);
        }

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void chkRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWavgDis_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    //txtSellingPrice.Enabled = true;
                    //txtSellingPrice.Focus();
                    txtProductDiscountPercentage.Enabled = true;
                    txtProductDiscountPercentage.Focus();
                    txtProductDiscountPercentage.SelectAll();
                    //txtOtherDis.Enabled = true;
                    //txtOtherDis.Focus();
                    //txtOtherDis.SelectAll();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOtherDis_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                  
                    txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringQty(Common.ConvertStringToDecimal(txtOtherDis.Text.Trim()));
                    txtProductDiscountPercentage.Enabled = true;
                    txtProductDiscountPercentage.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtWavgDis_Leave(object sender, EventArgs e)
        {
            if (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text) > 0)
            {
                CalculateLine();
            }
            else
            {
                Toast.Show("Amount", Toast.messageType.Information, Toast.messageAction.ZeroAmount);

                txtCostPrice.Focus();
                txtCostPrice.SelectAll();
                return;
            }
        }
      
   }
}
