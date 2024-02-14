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
using Report.Logistic;
using System.Drawing.Printing;


namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmLogisticPurchaseReturnNote: UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();
        LgsProductMaster existingLgsProductMaster;
        LgsPurchaseDetailTemp existingILgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();
       // List<InvProductSerialNoTemp> invProductSerialNoTempList;

        List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();
        LgsProductBatchNoTemp existingLgsProductBatchNoTemp = new LgsProductBatchNoTemp();

        List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

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

        public FrmLogisticPurchaseReturnNote()
        {
            InitializeComponent();
        }

        private void FrmLogisticPurchaseReturnNote_Load(object sender, EventArgs e)
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
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                if (isSupplierProduct)
                {
                    SupplierService supplierService = new SupplierService();
                    Common.SetAutoComplete(txtProductCode, lgsPurchaseService.GetAllProductCodesBySupplier(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsPurchaseService.GetAllProductNamesBySupplier(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID), chkAutoCompleationProduct.Checked);
                }
                else
                {
                    Common.SetAutoComplete(txtProductCode, lgsPurchaseService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsPurchaseService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
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

                //Load return types
                LgsReturnTypeService returnTypeService = new LgsReturnTypeService();
                Common.LoadLgsReturnTypes(cmbReturnType, returnTypeService.GetAllLgsReturnTypes(documentID));

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                poIsMandatory = autoGenerateInfo.PoIsMandatory;
                isInvProduct = false;
                recallGRN = false;

                GetPrintingDetails();

                base.FormLoad();

                //Load document numbers
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

                //Load GRN Document Numbers -- GRN Document ID- 24 (From AutogenerateInfo Table)
                Common.SetAutoComplete(txtGrnNo, lgsPurchaseService.GetAllDocumentNumbersAsReference(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

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
                lgsPurchaseDetailTempList = null;
                existingLgsProductMaster = null;
                existingILgsPurchaseDetailTemp = null;
                lgsProductSerialNoTempList = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                txtDocumentNo.Text = GetDocumentNo(true);
                isInvProduct = false;
                recallGRN = false;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

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
                LgsSupplierService lgssupplierService = new LgsSupplierService();
                LgsSupplier existingLgsSupplier = new LgsSupplier();

                if (isCode)
                {
                    existingLgsSupplier = lgssupplierService.GetSupplierByCode(strsupplier);
                    if (isCode && strsupplier.Equals(string.Empty))
                    {
                        txtSupplierName.Focus();
                        return;
                    }
                }
                else { existingLgsSupplier = lgssupplierService.GetSupplierByName(strsupplier); }

                if (existingLgsSupplier != null)
                {
                    txtSupplierCode.Text = existingLgsSupplier.SupplierCode;
                    txtSupplierName.Text = existingLgsSupplier.SupplierName;
                    chkTStatus.Checked = existingLgsSupplier.IsUpload;
                    if (existingLgsSupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); } else { Common.EnableCheckBox(true, chkTStatus); }

                    txtRemark.Focus();
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
                LgsSupplierService lgssupplierService = new LgsSupplierService();

                if (lgssupplierService.IsExistsLgsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
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
                existingLgsProductMaster = new LgsProductMaster();
                if (strProduct.Equals(string.Empty)) { return; }

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else { existingLgsProductMaster = lgsProductMasterService.GetProductsByName(strProduct); ;}

                if (existingLgsProductMaster != null)
                {
                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();
                    if (lgsPurchaseDetailTempList == null)
                        lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

                    if (recallGRN)
                    {
                        LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                        lgsPurchaseHeader = lgsPurchaseServices.GetLgsPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                        existingILgsPurchaseDetailTemp = lgsPurchaseServices.GetPurchaseDetailTempForPRN(lgsPurchaseHeader, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    }
                    else
                    {
                        existingILgsPurchaseDetailTemp = lgsPurchaseServices.GetPurchaseDetailTemp(lgsPurchaseDetailTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertDateTimeToDate(expiryDate), unitofMeasureID);
                    }
                    
                    if (existingILgsPurchaseDetailTemp != null)
                    {
                        txtProductCode.Text = existingILgsPurchaseDetailTemp.ProductCode;
                        txtProductName.Text = existingILgsPurchaseDetailTemp.ProductName;
                        txtBatchNo.Text = existingILgsPurchaseDetailTemp.BatchNo;
                        cmbUnit.SelectedValue = existingILgsPurchaseDetailTemp.UnitOfMeasureID;
                        txtCostPrice.Text = Common.ConvertDecimalToStringCurrency(existingILgsPurchaseDetailTemp.CostPrice);
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

                        if (unitofMeasureID.Equals(0)) { cmbUnit.Focus(); } 
                    }
                }

                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (isCode) { txtProductCode.Focus(); txtProductCode.SelectAll(); }
                    else { txtProductName.Focus(); txtProductName.SelectAll(); }
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductName.Enabled = true;
                    txtProductName.Focus();
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

                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()), dtpExpiry.Value);

                if (existingLgsProductMaster.IsBatch)
                {
                    txtBatchNo.Enabled = true;
                    txtBatchNo.Focus();
                }
                else if (existingLgsProductMaster.IsExpiry)
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
                        LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                        LocationService locationService = new LocationService();

                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        if (txtBatchNo.Text.Trim() != "")
                        {
                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();

                            CommonService commonService = new CommonService();
                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

                            if (lgsProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = lgsProductBatchNo.CostPrice.ToString();

                                if (existingLgsProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(lgsProductBatchNo.ExpiryDate.ToString());
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
                            LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                            lgsProductBatchNoTempList = lgsPurchaseService.GetBatchNoDetail(existingLgsProductMaster, locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                            if (lgsProductSerialNoTempList == null)
                                lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                            FrmBatchNumber frmBatchNumber = new FrmBatchNumber(lgsProductBatchNoTempList, lgsProductBatchNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty((Common.ConvertStringToDecimal(txtQty.Text.Trim()))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, FrmBatchNumber.transactionType.LogisticPurchaseReturn, existingLgsProductMaster.LgsProductMasterID);
                            frmBatchNumber.ShowDialog();
                            LgsProductBatchNoExpiaryDetail lgsProductBatchNo = new LgsProductBatchNoExpiaryDetail();
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

                            lgsProductBatchNo = commonService.CheckLgsBatchNumber(txtBatchNo.Text.Trim(), existingLgsProductMaster.LgsProductMasterID, locationService.GetLocationsByName(cmbLocation.Text).LocationID, existingLgsProductMaster.UnitOfMeasureID);

                            if (lgsProductBatchNo == null)
                            {
                                if ((Toast.Show("This Batch No Not exists!", Toast.messageType.Error, Toast.messageAction.General).Equals(DialogResult.OK)))
                                    return;
                            }
                            else
                            {
                                txtCostPrice.Text = lgsProductBatchNo.CostPrice.ToString();

                                if (existingLgsProductMaster.IsExpiry)
                                {
                                    dtpExpiry.Enabled = true;
                                    dtpExpiry.Value = Common.ConvertStringToDateTime(lgsProductBatchNo.ExpiryDate.ToString());
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
                        LgsProductBatchNoTemp lgsProductBatchNoTemp = new LgsProductBatchNoTemp();
                        //invProductBatchNoTemp.DocumentID = documentID;
                        lgsProductBatchNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                        lgsProductBatchNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                        LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                        lgsProductBatchNoTempList = lgsPurchaseService.GetExpiryDetail(existingLgsProductMaster, txtBatchNo.Text.Trim());

                        if (lgsProductSerialNoTempList == null)
                            lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                        if (lgsProductBatchNoTempList.Count > 1)
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

        public void SetSerialNoList(List<InvProductSerialNoTemp> setLgsProductSerialNoTemp)
        {
            lgsProductSerialNoTempList = setLgsProductSerialNoTemp;
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

                        if (!existingLgsProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
                        {
                            LgsProductUnitConversionService lgsProductUnitConversionService = new LgsProductUnitConversionService();

                            convertFactor = lgsProductUnitConversionService.GetProductUnitByProductCode(existingLgsProductMaster.LgsProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())).ConvertFactor;

                        }
                        else
                        {
                            convertFactor = 1;
                        }
                        if (commonService.ValidateBatchStock((Common.ConvertStringToDecimalQty(txtQty.Text.Trim())+Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())), existingLgsProductMaster, Convert.ToInt32(cmbLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
                        {
                            if (existingLgsProductMaster.IsSerial)
                            {
                                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();
                                lgsProductSerialNoTemp.ProductID = existingLgsProductMaster.LgsProductMasterID;
                                lgsProductSerialNoTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());

                                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                                lgsProductSerialNoTempList = lgsPurchaseService.GetSerialNoDetail(existingLgsProductMaster);

                                if (lgsProductSerialNoTempList == null)
                                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                                FrmSerialCommon frmSerialCommon = new FrmSerialCommon(lgsProductSerialNoTempList, lgsProductSerialNoTemp, txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "\nQty - " + Common.ConvertDecimalToStringQty(((Common.ConvertStringToDecimal(txtQty.Text.Trim()) + Common.ConvertStringToDecimal(txtFreeQty.Text.Trim())))), (Common.ConvertStringToDecimal(txtQty.Text.Trim())), txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), isInvProduct, documentID, FrmSerialCommon.transactionType.LogisticPurchaseReturn);
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
            try
            {
                 CalculateLine();
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

        private void UpdateGrid(LgsPurchaseDetailTemp lgsPurchaseTempDetail)
        {
            try
            {
                decimal qty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()) > 0))
                {
                    CalculateLine();

                    if (existingLgsProductMaster.IsExpiry.Equals(true)) { lgsPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dtpExpiry.Value.ToString()); }
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
                    }

                    
                    if (!chkOverwrite.Checked)
                    {
                        if (recallGRN)
                        {
                            if ((existingLgsProductMaster != null) && (!txtGrnNo.Text.Trim().Equals(string.Empty)))
                            {
                                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                                lgsPurchaseHeader = lgsPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                                if (!lgsPurchaseService.IsValidNoOfQty(qty, existingLgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseHeader.LgsPurchaseHeaderID))
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
                        lgsPurchaseTempDetail.Qty = Common.ConvertDecimalToDecimalQty(qty);
                        lgsPurchaseTempDetail.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        lgsPurchaseTempDetail.Qty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        lgsPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    }

                    lgsPurchaseTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text);
                    lgsPurchaseTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtCostPrice.Text);
                    lgsPurchaseTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtCostPrice.Text.Trim()));
                    lgsPurchaseTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    lgsPurchaseTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscount.Text);
                    lgsPurchaseTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                    lgsPurchaseTempDetail.BatchNo = txtBatchNo.Text.Trim();

                    LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();

                    dgvItemDetails.DataSource = null;
                    lgsPurchaseDetailTempList = lgsPurchaseServices.getUpdatePurchaseDetailTemp(lgsPurchaseDetailTempList, lgsPurchaseTempDetail, existingLgsProductMaster, false);
                    dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    foreach (DataGridViewRow row in dgvItemDetails.Rows)
                    {
                        if (string.Equals(txtProductCode.Text.Trim(), dgvItemDetails["ProductCode", row.Index].Value.ToString()))
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

        private decimal GettotalQty(List<LgsPurchaseDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.OrderQty);
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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, grossAmount, supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, grossAmount, supplierService.GetSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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
            LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
            Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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

                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                LgsPurchaseHeader lgsGrnHeader = new LgsPurchaseHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService lgssupplierService = new LgsSupplierService();
                LgsSupplier lgssupplier = new LgsSupplier();

                lgssupplier = lgssupplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsPurchaseHeader = lgsPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Location.LocationID);
                if (lgsPurchaseHeader == null)
                    lgsPurchaseHeader = new LgsPurchaseHeader();

                lgsPurchaseHeader.PurchaseHeaderID = lgsPurchaseHeader.LgsPurchaseHeaderID;
                lgsPurchaseHeader.CompanyID = Location.CompanyID;
                lgsPurchaseHeader.CostCentreID = Common.ConvertStringToInt(cmbCostCentre.SelectedValue.ToString());
                lgsPurchaseHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                    lgsPurchaseHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                lgsPurchaseHeader.OtherChargers = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                lgsPurchaseHeader.DocumentDate = Common.ConvertStringToDate(dtpDocumentDate.Value.ToString());
                lgsPurchaseHeader.DocumentID = documentID;
                lgsPurchaseHeader.DocumentStatus = documentStatus;
                lgsPurchaseHeader.DocumentNo = documentNo.Trim();
                lgsPurchaseHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                lgsPurchaseHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsPurchaseHeader.LineDiscountTotal = GetLineDiscountTotal(lgsPurchaseDetailTempList);
                lgsPurchaseHeader.LocationID = Location.LocationID;
                lgsPurchaseHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                lgsPurchaseHeader.ReturnTypeID = Convert.ToInt32(cmbReturnType.SelectedValue);

                if (!txtGrnNo.Text.Trim().Equals(string.Empty))
                {

                    lgsGrnHeader = lgsPurchaseService.GetLgsPurchaseHeaderByDocumentNo((AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID), txtGrnNo.Text.Trim(), locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);

                    lgsPurchaseHeader.ReferenceDocumentDocumentID = lgsGrnHeader.DocumentID;
                    lgsPurchaseHeader.ReferenceDocumentID = lgsGrnHeader.LgsPurchaseHeaderID;

                    lgsPurchaseHeader.BatchNo = lgsGrnHeader.BatchNo;
                }

                lgsPurchaseHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                lgsPurchaseHeader.Remark = txtRemark.Text.Trim();
                lgsPurchaseHeader.SupplierID = lgssupplier.LgsSupplierID;
                lgsPurchaseHeader.IsUpLoad = chkTStatus.Checked;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    lgsPurchaseHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, lgsPurchaseHeader.GrossAmount, lgssupplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    lgsPurchaseHeader.TaxAmount1 = tax1;
                    lgsPurchaseHeader.TaxAmount2 = tax2;
                    lgsPurchaseHeader.TaxAmount3 = tax3;
                    lgsPurchaseHeader.TaxAmount4 = tax4;
                    lgsPurchaseHeader.TaxAmount5 = tax5;

                }

                if (lgsPurchaseDetailTempList == null)
                    lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

                if (lgsProductSerialNoTempList == null)
                    lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

                return lgsPurchaseService.SavePurchaseReturn(lgsPurchaseHeader, lgsPurchaseDetailTempList, lgsProductSerialNoTempList, out newDocumentNo, this.Name);

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
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();

                lgsPurchaseHeader = lgsPurchaseService.getPurchaseHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                if (lgsPurchaseHeader != null)
                {
                    LgsSupplierService lgssupplierService = new LgsSupplierService();
                    LgsSupplier lgssupplier = new LgsSupplier();
                    lgssupplier = lgssupplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                    cmbLocation.SelectedValue = lgsPurchaseHeader.LocationID;
                    cmbLocation.Refresh();
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
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.OtherChargers);
                    dtpDocumentDate.Value = Common.FormatDate(lgsPurchaseHeader.DocumentDate);
                    txtDocumentNo.Text = lgsPurchaseHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseHeader.NetAmount);

                    if (!lgsPurchaseHeader.ReferenceDocumentID.Equals(0))
                    {
                        txtGrnNo.Text = lgsPurchaseService.GetSavedDocumentDetailsByDocumentID(lgsPurchaseHeader.ReferenceDocumentID).DocumentNo;
                    }
                    else
                    {
                        txtGrnNo.Text = string.Empty;
                    }

                    txtReferenceNo.Text = lgsPurchaseHeader.ReferenceNo;
                    txtRemark.Text = lgsPurchaseHeader.Remark;
                    lgssupplier = lgssupplierService.GetSupplierByID(lgsPurchaseHeader.SupplierID);
                    txtSupplierCode.Text = lgssupplier.SupplierCode;
                    txtSupplierName.Text = lgssupplier.SupplierName;

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

                    lgsProductSerialNoTempList = lgsPurchaseService.getPausedPurchaseSerialNoDetail(lgsPurchaseHeader);

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtGrnNo);
                    Common.EnableComboBox(false, cmbLocation);

                    if (lgsPurchaseHeader.DocumentStatus.Equals(0))
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

                        LgsPurchaseDetailTemp lgsPurchaseTempDetail = new LgsPurchaseDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsPurchaseTempDetail.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        if (existingLgsProductMaster.IsExpiry.Equals(true))
                            lgsPurchaseTempDetail.ExpiryDate = Common.ConvertStringToDate(dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString());
                        lgsPurchaseTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsPurchaseService lgsPurchaseServices = new LgsPurchaseService();

                        dgvItemDetails.DataSource = null;
                        lgsPurchaseDetailTempList = lgsPurchaseServices.getDeletePurchaseDetailTemp(lgsPurchaseDetailTempList, lgsPurchaseTempDetail, lgsProductSerialNoTempList, out lgsProductSerialNoTempList);
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

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID, Common.ConvertStringToDate((dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value == null ? DateTime.Now.ToString() : dgvItemDetails["Expiry", dgvItemDetails.CurrentCell.RowIndex].Value.ToString())));
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
                LgsPurchaseHeader lgsGrnHeader = new LgsPurchaseHeader();
                LgsPurchaseService lgsPurchaseService = new Service.LgsPurchaseService();

                lgsGrnHeader = lgsPurchaseService.GetLgsPurchaseHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, txtGrnNo.Text.Trim(), Common.LoggedLocationID);
                if (lgsGrnHeader != null)
                {
                    //EnableControl(true);
                    LgsSupplierService supplierService = new LgsSupplierService();
                    LgsSupplier lgssupplier = new LgsSupplier();
                    lgssupplier = supplierService.GetSupplierByID(lgsGrnHeader.SupplierID);

                    chkTStatus.Checked = lgsGrnHeader.IsUpLoad;
                    if (lgssupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); }{ Common.EnableCheckBox(true, chkTStatus); }

                    cmbLocation.SelectedValue = lgsGrnHeader.LocationID;
                    cmbLocation.Refresh();
                    txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                    chkSubTotalDiscountPercentage.Checked = false;
                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(0);
                    dtpDocumentDate.Value = Common.FormatDate(DateTime.Now);
                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsGrnHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsGrnHeader.NetAmount);
                    txtGrnNo.Text = lgsGrnHeader.DocumentNo;
                    txtReferenceNo.Text = string.Empty; ;
                    txtRemark.Text = lgsGrnHeader.Remark;
                    lgssupplier = supplierService.GetSupplierByID(lgsGrnHeader.SupplierID);
                    txtSupplierCode.Text = lgssupplier.SupplierCode;
                    txtSupplierName.Text = lgssupplier.SupplierName;

                    if (lgsGrnHeader.TaxAmount!=0)
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(lgsGrnHeader.TaxAmount);
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
                        docID = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder").DocumentID;
                        lgsPurchaseDetailTempList = lgsPurchaseService.GetPausedPurchaseDetailToPRN(lgsGrnHeader, docID);
                    }
                    else
                    {
                        lgsPurchaseDetailTempList = lgsPurchaseService.GetPausedPurchaseDetail(lgsGrnHeader);
                    }
                    dgvItemDetails.DataSource = lgsPurchaseDetailTempList;
                    dgvItemDetails.Refresh();

                    dgvAdvanced.DataSource = null;
                    dgvAdvanced.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtGrnNo, txtProductCode, txtProductName);
                    Common.EnableComboBox(false, cmbLocation);

                    tbDetails.Enabled = true;
                    grpFooter.Enabled = true;
                    grpBody.Enabled = true;
                    EnableLine(false);
                    
                    GetSummarizeFigures(lgsPurchaseDetailTempList);
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
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtDocumentNo, lgsPurchaseService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);

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
                LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                Common.SetAutoComplete(txtGrnNo, lgsPurchaseService.GetAllDocumentNumbers(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
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

                
                
                if (commonService.ValidateBatchStock((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())), existingLgsProductMaster, Convert.ToInt32(cmbLocation.SelectedValue), txtBatchNo.Text.Trim(), convertFactor))
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

                    LgsProductMaster lgsProductMaster = new LgsProductMaster();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                    lgsProductMaster = lgsProductMasterService.GetProductsByRefCodes(txtProductCode.Text.Trim());
                    if (lgsProductMaster != null)
                    {
                        LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
                        LgsPurchaseService lgsPurchaseService = new LgsPurchaseService();
                        lgsPurchaseHeader = lgsPurchaseService.GetSavedDocumentDetailsByDocumentNumber(txtGrnNo.Text.Trim());

                        if (!lgsPurchaseService.IsValidNoOfQty(qty, lgsProductMaster.LgsProductMasterID, Convert.ToInt32(cmbUnit.SelectedValue), lgsPurchaseHeader.LgsPurchaseHeaderID))
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
    }
}
