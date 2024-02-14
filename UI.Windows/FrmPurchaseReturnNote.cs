using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Utility;
using Service;
using System.Reflection;
using Report.Inventory;
using System.Drawing.Printing;
using System.Linq.Expressions;
using System.Data.Entity;
//using Data;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmPurchaseReturnNote : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();
        InvProductMaster existingInvProductMaster;
        InvPurchaseDetailTemp existingIInvPurchaseDetailTemp = new InvPurchaseDetailTemp();
       // List<InvProductSerialNoTemp> invProductSerialNoTempList;

        List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();
        InvProductBatchNoTemp existingInvProductBatchNoTemp = new InvProductBatchNoTemp();

        List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

        bool isSupplierProduct;
        int documentID;
        static string batchNumber;
        static DateTime expiryDate;
        bool isInvProduct;
        bool recallGRN;
        bool poIsMandatory;
        decimal convertFactor = 1;
        bool isMinusStock = false;
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;


        public FrmPurchaseReturnNote()
        {
            InitializeComponent();
        }

        private void FrmPurchaseReturnNote_Load(object sender, EventArgs e)
        {

        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "")
                {
                    SupplierService supplierService = new SupplierService();

                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID, 1, Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()));
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
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                if (isSupplierProduct)
                {
                    SupplierService supplierService = new SupplierService();
                    Common.SetAutoComplete(txtProductCode, invPurchaseService.GetAllProductCodesBySupplier(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, invPurchaseService.GetAllProductNamesBySupplier(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID), chkAutoCompleationProduct.Checked);
                }
                else
                {
                    Common.SetAutoComplete(txtProductCode, invPurchaseService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, invPurchaseService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

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

        public override void FormLoad()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                dgvItemDetails.AutoGenerateColumns = false;

                dgvAdvanced.AutoGenerateColumns = false;
                documentID = autoGenerateInfo.DocumentID;

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

                //Load return types
                InvReturnTypeService returnTypeService = new InvReturnTypeService();
                Common.LoadInvReturnTypes(cmbReturnType, returnTypeService.GetAllInvReturnTypes(documentID));

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                poIsMandatory = autoGenerateInfo.PoIsMandatory;
                isInvProduct = true;
                recallGRN = false;

                GetPrintingDetails();

                base.FormLoad();

                //Load document numbers
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, invPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                //Common.SetAutoComplete(txtGrnNo, invPurchaseService.GetAllDocumentNumbersAsReference(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                this.ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();

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
                tbDetails.Enabled = false;
                grpFooter.Enabled = false;
                EnableLine(false);
                Common.EnableTextBox(true, txtDocumentNo, txtGrnNo, txtSupplierCode, txtSupplierName);
                Common.EnableButton(true, btnDocumentDetails, btnGrnDetails);
                Common.EnableButton(false, btnSave, btnPause, btnView);
                Common.EnableComboBox(true, cmbLocation);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                dtpExpiry.Value = DateTime.Now;
                invPurchaseDetailTempList = null;
                existingInvProductMaster = null;
                existingIInvPurchaseDetailTemp = null;
                invProductSerialNoTempList = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                txtDocumentNo.Text = GetDocumentNo(true);
                isInvProduct = true;
                recallGRN = false;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                dgvBatchStock.DataSource = null;

                this.ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtBatchNo, txtQty, txtFreeQty, txtCostPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
            dtpExpiry.Enabled = enable;
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtBatchNo, txtQty, txtFreeQty, txtCostPrice, txtProductDiscount, txtProductDiscountPercentage, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            dtpExpiry.Value = DateTime.Now; ;
            txtProductCode.Focus();
        }


        private void LoadSupplier(bool isCode, string strsupplier)
        {
            try
            {
                SupplierService supplierService = new SupplierService();
                Supplier existingSupplier = new Supplier();

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
                    if (existingSupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); } else { Common.EnableCheckBox(true, chkTStatus); }

                    txtRemark.Focus();

                    if (existingSupplier.TaxID1 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID2 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID3 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID4 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID5 != 0) { chkTaxEnable.Checked = true; return; }
                }
                else
                {
                    Toast.Show("Supplier - " + strsupplier.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
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
                    cmbCostCentre.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCostCentre_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    dtpDocumentDate.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpDocumentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                    cmbReturnType.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbReturnType_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSupplierCode.Text.Trim().Equals(string.Empty))
                    LoadSupplier(true, txtSupplierCode.Text.Trim());
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
                LoadSupplier(false, txtSupplierName.Text.Trim());
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

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                SupplierService supplierService = new SupplierService();

                if (supplierService.IsExistsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
                {
                    Toast.Show("Supplier - " + txtSupplierCode.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    txtSupplierCode.Focus();
                    return;
                }
                else
                {
                    LocationService locationService = new LocationService();
                    if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                    {
                        Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbLocation.Focus();
                        return;
                    }
                    else
                    {
                        accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                        if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

                        tbDetails.Enabled = true;
                        Common.EnableTextBox(true, txtProductCode, txtProductName);

                        LoadProducts();
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
                LoadProducts();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                loadProductDetails(true, txtProductCode.Text.Trim(), 0, dtpExpiry.Value);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate)
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
                    if (invPurchaseDetailTempList == null)
                        invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

                    if (recallGRN)
                    {
                        InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                        invPurchaseHeader = invPurchaseServices.GetInvPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                        existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempForPRN(invPurchaseHeader, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    }
                    else
                    {
                        existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempPRNWithBatch(invPurchaseDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, txtBatchNo.Text.Trim());
                    }
                    
                    if (existingIInvPurchaseDetailTemp != null)
                    {
                        txtProductCode.Text = existingIInvPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingIInvPurchaseDetailTemp.ProductName;
                        txtBatchNo.Text = existingIInvPurchaseDetailTemp.BatchNo;
                        cmbUnit.SelectedValue = existingIInvPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.CostPrice);
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

                        if (unitofMeasureID.Equals(0)) { cmbUnit.Focus(); }

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingIInvPurchaseDetailTemp.ProductID); } 
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

        private void loadProductDetailsForBatchDoubleClick(bool isCode, string strProduct, long unitofMeasureID, DateTime expiryDate, string batchNo)
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
                    if (invPurchaseDetailTempList == null)
                        invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

                    if (recallGRN)
                    {
                        InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                        invPurchaseHeader = invPurchaseServices.GetInvPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                        existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempForPRN(invPurchaseHeader, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    }
                    else
                    {
                        existingIInvPurchaseDetailTemp = invPurchaseServices.GetPurchaseDetailTempPRNWithBatch(invPurchaseDetailTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID, batchNo);
                    }

                    if (existingIInvPurchaseDetailTemp != null)
                    {
                        txtProductCode.Text = existingIInvPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingIInvPurchaseDetailTemp.ProductName;
                        txtBatchNo.Text = existingIInvPurchaseDetailTemp.BatchNo;
                        cmbUnit.SelectedValue = existingIInvPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingIInvPurchaseDetailTemp.CostPrice);
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

                        if (unitofMeasureID.Equals(0)) { cmbUnit.Focus(); }

                        if (chkViewStokDetails.Checked) { ShowStockDetails(existingIInvPurchaseDetailTemp.ProductID); }
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

        private void ShowStockDetails(long productID = 0)
        {
            CommonService CommonService = new CommonService();

            dgvBatchStock.DataSource = null;
            dgvBatchStock.AutoGenerateColumns = false;
            List<InvProductBatchNoExpiaryDetail> invProductBatchNoList = new List<InvProductBatchNoExpiaryDetail>();
            invProductBatchNoList = CommonService.GetInvBatchStockDetailsToStockGrid(productID, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
            dgvBatchStock.DataSource = invProductBatchNoList;
            dgvBatchStock.Refresh();
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
                loadProductDetails(false, txtProductName.Text.Trim(), 0, dtpExpiry.Value);
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

                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);

                if (existingInvProductMaster.IsBatch)
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else if (existingInvProductMaster.IsExpiry)
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

        public void SetBatchNumber(string batchNo)
        {
            batchNumber = batchNo;
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    batchNumber = null;

                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        LocationService locationService = new LocationService();

                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();

                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();
                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();
                                }
                                return;
                            }
                        }
                        else
                        {
                            InvPurchaseService invPurchaseService = new InvPurchaseService();
                            invProductBatchNoTempList = invPurchaseService.GetBatchNoDetail(existingInvProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (invProductSerialNoTempList == null)
                                invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            InvProductMasterService invProductMasterService = new InvProductMasterService();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.PurchaseReturn, existingInvProductMaster.InvProductMasterID);
                            frmBatchNumber.ShowDialog();
                            InvProductBatchNoExpiaryDetail invProductBatchNo = new InvProductBatchNoExpiaryDetail();
                            txtBatchNo.Text = batchNumber;
                        
                            //txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();
                            //txtSellingPrice.Text = invProductBatchNo.SellingPrice.ToString();

                            //if (existingInvProductMaster.IsExpiry)
                            //{
                            //    dtpExpiry.Enabled = true;                            

                            //    CommonService commonService = new CommonService();
                            //    invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID);
                            //    dtpExpiry.Value = Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                            //    dtpExpiry.Focus();
                            //}
                            //else
                            //{
                            //    txtQty.Enabled = true;
                            //    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                            //        txtQty.Text = "1";
                            //    txtQty.Focus();
                            //}

                            CommonService commonService = new CommonService();

                            invProductBatchNo = commonService.CheckInvBatchNumber(txtBatchNo.Text.Trim(), existingInvProductMaster.InvProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingInvProductMaster.UnitOfMeasureID);

                            if (invProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = invProductBatchNo.CostPrice.ToString();

                                if (existingInvProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(invProductBatchNo.ExpiryDate.ToString());
                                    dtpExpiry.Focus();
                                }
                                else
                                {
                                    txtQty.Enabled = true;
                                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                                        txtQty.Text = "1";
                                    txtQty.Focus();
                                }
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void SetExpiryDate(DateTime expiary)
        {
            expiryDate = expiary;
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        InvProductBatchNoTemp invProductBatchNoTemp = new InvProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        invProductBatchNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                        invProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        InvPurchaseService invPurchaseService = new InvPurchaseService();
                        invProductBatchNoTempList = invPurchaseService.GetExpiryDetail(existingInvProductMaster, txtBatchNo.Text.Trim());

                        if (invProductSerialNoTempList == null)
                            invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        if (invProductBatchNoTempList.Count > 1)
                        {
                            //FrmExpiary frmExpiry = new FrmExpiary(invProductBatchNoTempList, invProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbFromLocation.SelectedValue.ToString()), isInvProduct, FrmExpiary.transactionType.TransferOfGoods);
                            //frmExpiry.ShowDialog();

                            //dtpExpiry.Value = Common.FormatDate(expiryDate);
                        }

                        txtQty.Enabled = true;
                        if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                            txtQty.Text = "1";
                        txtQty.Focus();
                    }
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

        public void SetSerialNoList(List<InvProductSerialNoTemp> setInvProductSerialNoTemp)
        {
            invProductSerialNoTempList = setInvProductSerialNoTemp;
        }

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
               
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim()) > 0)
                    {

                        CommonService commonService = new CommonService();

                        if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                        {
                            InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();

                            convertFactor = invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;

                        }
                        else
                        {
                            convertFactor = 1;
                        }
                        if (commonService.ValidateBatchStock((Common.ConvertStringToDecimalQty(txtQty.Text.Trim())+Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())), existingInvProductMaster, Convert.ToInt32(cmbLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
                        {
                            if (existingInvProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();
                                invProductSerialNoTemp.ProductID = existingInvProductMaster.InvProductMasterID;
                                invProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                                InvPurchaseService invPurchaseService = new InvPurchaseService();
                                invProductSerialNoTempList = invPurchaseService.GetSerialNoDetail(existingInvProductMaster);

                                if (invProductSerialNoTempList == null)
                                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                FrmSerialCommon frmSerialCommon = new FrmSerialCommon(invProductSerialNoTempList, invProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty(((Common.ConvertStringToDecimal(txtQty.Text.Trim())+Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.TransferOfGoods);
                                frmSerialCommon.ShowDialog();

                                CalculateLine();
                                txtProductDiscountPercentage.Enabled = true;
                                txtProductDiscountPercentage.Focus();
                            }
                            else
                            {
                                CalculateLine();
                                txtProductDiscountPercentage.Enabled = true;
                                txtProductDiscountPercentage.Focus();
                            }
                        }
                        else
                        {
                            Toast.Show("Return", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                            txtProductDiscountPercentage.Enabled = false;
                            this.ActiveControl = txtQty;
                            txtQty.Focus();
                            txtQty.SelectAll();
                            return;
                        }
                        CalculateLine();
                    }
                }
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
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountPercentage_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void txtProductDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductAmount.Enabled = true;
                    txtProductAmount.Focus();
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

        private void UpdateGrid(InvPurchaseDetailTemp invPurchaseTempDetail)
        {
            try
            {
                decimal qty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0))
                {
                    CalculateLine();

                    if (existingInvProductMaster.IsExpiry.Equals(true)) { invPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString()); }
                    invPurchaseTempDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invPurchaseTempDetail.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
                    invPurchaseTempDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                    }

                    

                    if (!chkOverwrite.Checked)
                    {
                        if (recallGRN)
                        {
                            if ((existingInvProductMaster != null) && (!txtGrnNo.Text.Trim().Equals(string.Empty)))
                            {
                                InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                                InvPurchaseService invPurchaseService = new InvPurchaseService();
                                invPurchaseHeader = invPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                                if (!invPurchaseService.IsValidNoOfQty(qty, existingInvProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseHeader.InvPurchaseHeaderID))
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
                        invPurchaseTempDetail.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        invPurchaseTempDetail.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        invPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    }

                    invPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text);
                    invPurchaseTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    invPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    invPurchaseTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    invPurchaseTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text);
                    invPurchaseTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                    invPurchaseTempDetail.BatchNo = txtBatchNo.Text.Trim();

                    InvPurchaseService InvPurchaseServices = new InvPurchaseService();

                    dgvItemDetails.DataSource = null;
                    invPurchaseDetailTempList = InvPurchaseServices.getUpdatePurchaseDetailTempPRN(invPurchaseDetailTempList, invPurchaseTempDetail, existingInvProductMaster, false);
                    dgvItemDetails.DataSource = invPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()) && string.Equals(txtBatchNo.Text.Trim(), dgvItemDetails["BatchNo", row.Index].Value.ToString()))
                        {
                            isUpdateGrid = true;
                            selectedRowIndex = row.Index;
                            break;
                        }
                    }

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

        private decimal GettotalQty(List<InvPurchaseDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.Qty);
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
            decimal grossAmount = listItem.GetSummaryAmount(x => x.NetAmount);

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, grossAmount, supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, grossAmount, supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            GetSummarizeSubFigures();
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

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
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
        }

        public override void Pause()
        {
            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if ((Toast.Show("Purchase Return Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;

                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Purchase Return Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Purchase Return Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        private bool ValidateTextBoxes()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtSupplierCode);
        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbReturnType, cmbLocation);
        }

        public override void Save()
        {

            if (ValidateTextBoxes().Equals(false)) { return; }
            if (ValidateComboBoxes().Equals(false)) { return; }

            if ((Toast.Show("Purchase Return Note  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Purchase Return Note  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Purchase Return Note  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
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
                InvPurchaseHeader invGrnHeader = new InvPurchaseHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invPurchaseHeader = invPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (invPurchaseHeader == null)
                    invPurchaseHeader = new InvPurchaseHeader();

                invPurchaseHeader.PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID;
                invPurchaseHeader.CompanyID = Location.CompanyID;
                invPurchaseHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                invPurchaseHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                    invPurchaseHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                invPurchaseHeader.OtherChargers = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invPurchaseHeader.DocumentDate = Common.ConvertStringToDate(dtpDocumentDate.Value.ToString());
                invPurchaseHeader.DocumentID = documentID;
                invPurchaseHeader.DocumentStatus = documentStatus;
                invPurchaseHeader.DocumentNo = documentNo.Trim();
                invPurchaseHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invPurchaseHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invPurchaseHeader.LineDiscountTotal = GetLineDiscountTotal(invPurchaseDetailTempList);
                invPurchaseHeader.LocationID = Location.LocationID;
                invPurchaseHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invPurchaseHeader.ReturnTypeID = Convert.ToInt32(cmbReturnType.SelectedValue);

                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {

                    invGrnHeader = invPurchaseService.GetInvPurchaseHeaderByDocumentNo((AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID), txtGrnNo.Text.Trim(), locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);

                    invPurchaseHeader.ReferenceDocumentDocumentID = invGrnHeader.DocumentID;
                    invPurchaseHeader.ReferenceDocumentID = invGrnHeader.InvPurchaseHeaderID;

                    invPurchaseHeader.BatchNo = invGrnHeader.BatchNo;
                }

                invPurchaseHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invPurchaseHeader.Remark = txtRemark.Text.Trim();
                invPurchaseHeader.SupplierID = supplier.SupplierID;
                invPurchaseHeader.IsUpLoad = chkTStatus.Checked;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    invPurchaseHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, invPurchaseHeader.GrossAmount, supplier.SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invPurchaseHeader.TaxAmount1 = tax1;
                    invPurchaseHeader.TaxAmount2 = tax2;
                    invPurchaseHeader.TaxAmount3 = tax3;
                    invPurchaseHeader.TaxAmount4 = tax4;
                    invPurchaseHeader.TaxAmount5 = tax5;

                }

                if (invPurchaseDetailTempList == null)
                    invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

                if (invProductSerialNoTempList == null)
                    invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return invPurchaseService.SavePurchaseReturn(invPurchaseHeader, invPurchaseDetailTempList, invProductSerialNoTempList, out newDocumentNo, this.Name);

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
                recallGRN = false;
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
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.OtherChargers);
                    dtpDocumentDate.Value = Common.FormatDate(invPurchaseHeader.DocumentDate);
                    txtDocumentNo.Text = invPurchaseHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseHeader.NetAmount);

                    if (!invPurchaseHeader.ReferenceDocumentID.Equals(0))
                    {
                        txtGrnNo.Text = invPurchaseService.GetSavedDocumentDetailsByDocumentID(invPurchaseHeader.ReferenceDocumentID).DocumentNo;
                    }
                    else
                    {
                        txtGrnNo.Text = string.Empty;
                    }

                    txtReferenceNo.Text = invPurchaseHeader.ReferenceNo;
                    txtRemark.Text = invPurchaseHeader.Remark;
                    supplier = supplierService.GetSupplierByID(invPurchaseHeader.SupplierID);
                    txtSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierName.Text = supplier.SupplierName;

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

                    invProductSerialNoTempList = invPurchaseService.getPausedPurchaseSerialNoDetail(invPurchaseHeader);

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtGrnNo);
                    Common.EnableComboBox(false, cmbLocation);

                    if (invPurchaseHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        tbDetails.Enabled = true;
                        grpFooter.Enabled = true;
                        EnableLine(true);
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
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
            dgvAdvanced.DataSource = null;
            dgvAdvanced.Refresh();
            tbDetails.SelectedTab = tbpGeneral;
            base.ClearForm();
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        InvPurchaseDetailTemp invPurchaseTempDetail = new InvPurchaseDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invPurchaseTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        if (existingInvProductMaster.IsExpiry.Equals(true))
                            invPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
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

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    loadProductDetailsForBatchDoubleClick(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())), dgvItemDetails["BatchNo", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                    CalculateLine();
                    txtQty.Enabled = true;
                    this.ActiveControl = txtQty;
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallGRN(string documentNo) 
        {
            try
            {
                recallGRN = true;
                this.Cursor = Cursors.WaitCursor;
                InvPurchaseHeader invGrnHeader = new InvPurchaseHeader();
                InvPurchaseService invPurchaseService = new Service.InvPurchaseService();

                invGrnHeader = invPurchaseService.GetInvPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                if (invGrnHeader != null)
                {
                    //EnableControl(true);
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    supplier = supplierService.GetSupplierByID(invGrnHeader.SupplierID);

                    chkTStatus.Checked = invGrnHeader.IsUpLoad;
                    if (supplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); }{ Common.EnableCheckBox(true, chkTStatus); }

                    cmbLocation.SelectedValue = invGrnHeader.LocationID;
                    cmbLocation.Refresh();
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                    chkSubTotalDiscountPercentage.Checked = false;
                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(0);
                    dtpDocumentDate.Value = Common.FormatDate(DateTime.Now);
                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invGrnHeader.GrossAmount);
                    
                    //?????????????????? check with net amt
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invGrnHeader.NetAmount);
                    txtGrnNo.Text = invGrnHeader.DocumentNo;
                    txtReferenceNo.Text = string.Empty; ;
                    txtRemark.Text = invGrnHeader.Remark;
                    supplier = supplierService.GetSupplierByID(invGrnHeader.SupplierID);
                    txtSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierName.Text = supplier.SupplierName;

                    if (!invGrnHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invGrnHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;

                    int docID;
                    if (poIsMandatory)
                    {
                        docID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder").DocumentID;
                        invPurchaseDetailTempList = invPurchaseService.GetPausedPurchaseDetailToPRN(invGrnHeader, docID);
                    }
                    else
                    {
                        invPurchaseDetailTempList = invPurchaseService.GetPausedPurchaseDetail(invGrnHeader);
                    }
                    dgvItemDetails.DataSource = invPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    dgvAdvanced.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtGrnNo, txtProductCode, txtProductName);
                    Common.EnableComboBox(false, cmbLocation);

                    tbDetails.Enabled = true;
                    grpFooter.Enabled = true;
                    grpBody.Enabled = true;
                    EnableLine(false);
                    
                    GetSummarizeFigures(invPurchaseDetailTempList);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    Common.EnableButton(false, btnDocumentDetails, btnGrnDetails);
                    this.ActiveControl = txtProductCode;
                    txtProductCode.Focus();
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

        private void txtGrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                        RecallGRN(txtGrnNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            //Load document numbers
            InvPurchaseService invPurchaseService = new InvPurchaseService();
            Common.SetAutoComplete(txtDocumentNo, invPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationGrnNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                InvPurchaseService invPurchaseService = new InvPurchaseService();
                Common.SetAutoComplete(txtGrnNo, invPurchaseService.GetAllDocumentNumbers(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
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

        private void txtFreeQty_Leave(object sender, EventArgs e)
        {
            try
            {
                
                CommonService commonService = new CommonService();

                if (commonService.ValidateBatchStock((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())), existingInvProductMaster, Convert.ToInt32(cmbLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
                {

                }
                else
                {
                    Toast.Show("Return", Toast.messageType.Information, Toast.messageAction.QtyExceed);
                    txtProductDiscountPercentage.Enabled = false;
                    this.ActiveControl = txtQty;
                    txtQty.Focus();
                    txtQty.SelectAll();
                    return;
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
                if (recallGRN)
                {
                    if (string.IsNullOrEmpty(txtQty.Text.Trim())) { txtQty.Text = "0"; }

                    decimal qty = Convert.ToDecimal(txtQty.Text.Trim());

                    InvProductMaster invProductMaster = new InvProductMaster();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();

                    invProductMaster = invProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (invProductMaster != null)
                    {
                        InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
                        InvPurchaseService invPurchaseService = new InvPurchaseService();
                        invPurchaseHeader = invPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                        if (!invPurchaseService.IsValidNoOfQty(qty, invProductMaster.InvProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), invPurchaseHeader.InvPurchaseHeaderID))
                        {
                            Toast.Show("Invalid Qty.\nQty cannot grater then GRN Qty", Toast.messageType.Information, Toast.messageAction.General, "");
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

        private void dgvBatchStock_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvBatchStock.CurrentCell != null && dgvBatchStock.CurrentCell.RowIndex >= 0)
                {
                    if (dgvBatchStock["Batch", dgvBatchStock.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display", Toast.messageType.Information, Toast.messageAction.General, "");
                        return;
                    }
                    else
                    {
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        txtBatchNo.Text = dgvBatchStock["Batch", dgvBatchStock.CurrentCell.RowIndex].Value.ToString().Trim();
                        txtQty.Enabled = true;
                        txtQty.Focus();
                        txtQty.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkViewStokDetails_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkViewStokDetails.Checked)
                {
                    grpStock.Enabled = true;
                }
                else
                {
                    grpStock.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
