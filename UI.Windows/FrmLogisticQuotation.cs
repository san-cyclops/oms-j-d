using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Domain;
using Utility;
using Service;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmLogisticQuotation : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<LgsQuotationDetailTemp> lgsQuotationDetailsTempList = new List<LgsQuotationDetailTemp>();
        LgsQuotationDetailTemp existingLgsQuotationDetailTemp = new LgsQuotationDetailTemp();
        LgsProductMaster existingLgsProductMaster;
        bool loadSupplierProducts = true; // Remove this variable after getting values from System configuration
        int documentID = 0;
        int documentState;
        bool isSupplierProduct;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmLogisticQuotation()
        {
            InitializeComponent();
        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "" && chkTaxEnable.Checked == true)
                {
                    LgsSupplierService supplierService = new LgsSupplierService();
                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(supplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID, 3, Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()));
                    frmTaxBreakdown.ShowDialog();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                grpFooter.Enabled = false;
                EnableLine(false);
                EnableProductDetails(false);
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName,txtSalesPersonCode,txtSalesPersonName, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation, cmbPaymentTerms);
                Common.EnableButton(false, btnSave, btnPause);
                lgsQuotationDetailsTempList = null;
                this.ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                txtDocumentNo.Text = GetDocumentNo(true);   
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
                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                LocationService locationService = new LocationService();
                return lgsQuotationService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }

        }

        /// <summary>
        /// Load and bind products into text boxes depending on 
        /// system configurations (Products of selected supplier/ All products)
        /// </summary>
        private void LoadProducts()
        {
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            if (loadSupplierProducts)
            {
                LgsSupplierService lgssupplierService = new LgsSupplierService();
                if (lgssupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()) != null)
                {
                    Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodesBySupplier(lgssupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID), chkAutoCompleationProduct.Checked);
                    Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNamesBySupplier(lgssupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID), chkAutoCompleationProduct.Checked);
                }
            }
            else
            {
                Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
        }

        public override void FormLoad()
        {
            dgvItemDetails.AutoGenerateColumns = false;

            // Load Unit of measures
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

            // Load Locations
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;

            LgsSupplierService supplierService = new LgsSupplierService();
            Common.SetAutoComplete(txtSupplierCode, supplierService.GetAllLgsSupplierCodes(), chkAutoCompleationVendor.Checked);
            Common.SetAutoComplete(txtSupplierName, supplierService.GetAllLgsSupplierNames(), chkAutoCompleationVendor.Checked);

            ////Load Sales persons
            InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
            Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
            Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);

            /////Load payment method name to combo
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            List<PaymentMethod> paymentMethod = new List<PaymentMethod>();
            cmbPaymentTerms.DataSource = paymentMethodService.GetAllPaymentMethods();
            cmbPaymentTerms.DisplayMember = "PaymentMethodName";
            cmbPaymentTerms.ValueMember = "PaymentMethodID";
            cmbPaymentTerms.Refresh();

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
            chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
            if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

            base.FormLoad();

            ////Load Quotation Document Numbers
            LgsQuotationService lgsQuotationService = new LgsQuotationService();
            Common.SetAutoComplete(txtDocumentNo, lgsQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);

        }

        private void FrmLogisticQuotation_Load(object sender, EventArgs e)
        {
            
        }


        private void LoadSupplier(bool isCode, string strsupplier)
        {
            try
            {
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier existingLgsSupplier = new LgsSupplier();

                if (isCode)
                {
                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByCode(strsupplier);
                    if (isCode && strsupplier.Equals(string.Empty))
                    {
                        txtSupplierCode.Focus();
                        return;
                    }

                }
                else
                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByName(strsupplier);

                if (existingLgsSupplier != null)
                {
                    txtSupplierCode.Text = existingLgsSupplier.SupplierCode;
                    txtSupplierName.Text = existingLgsSupplier.SupplierName;

                    if (existingLgsSupplier.TaxID1 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID2 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID3 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID4 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID5 != 0) { chkTaxEnable.Checked = true; return; }

                    txtSalesPersonCode.Focus();
                }

                else
                {
                    Toast.Show("Logistic Supplier - " + strsupplier.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }

        }

        private void LoadSalesPerson(bool isCode, string strsalesPerson) 
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                InvSalesPerson existingInvSalesPerson = new InvSalesPerson();

                if (isCode)
                {
                    existingInvSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(strsalesPerson);
                    if (isCode && strsalesPerson.Equals(string.Empty))
                    {
                        txtSalesPersonCode.Focus();
                        return;
                    }

                }
                else
                    existingInvSalesPerson = invSalesPersonService.GetInvSalesPersonByName(strsalesPerson);

                if (existingInvSalesPerson != null)
                {
                    txtSalesPersonCode.Text = existingInvSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = existingInvSalesPerson.SalesPersonName;
                    txtRemark.Focus();
                }

                else
                {
                    Toast.Show("Sales Person - " + strsalesPerson.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
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

        private void txtVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSalesPersonCode.Focus(); }

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

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSalesPersonName.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                { txtRemark.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    DataView dvAllReferenceData = new DataView(invSalesPersonService.GetAllActiveSalesPersonsDataTable());
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtSalesPersonCode);
                        txtSalesPersonCode_Leave(this, e);
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtReferenceNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void dtpQuotationDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                dtpDeliveryDate.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpDeliveryDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                cmbPaymentTerms.Focus();
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                cmbLocation.Focus();
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVendorName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierName.Text.Trim())) { return; }
                LoadSupplier(false, txtSupplierName.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtDocumentNo_Leave(this, e);
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
                if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    RecallDocument(txtDocumentNo.Text.Trim());
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                dtpQuotationDate.Focus();
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
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                InvSalesPersonService salesPersonService = new InvSalesPersonService();

                if (lgsSupplierService.IsExistsLgsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
                {
                    Toast.Show("Logistic Supplier - " + txtSupplierCode.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    txtSupplierCode.Focus();
                    return;
                }
                else if (salesPersonService.IsExistsSalesPerson(txtSalesPersonCode.Text.Trim()).Equals(false))
                {
                    Toast.Show("Sales person - " + txtSalesPersonCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    txtSalesPersonCode.Focus();
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

                        Common.EnableTextBox(true, txtProductCode, txtProductName);
                        LoadProducts();
                        txtProductCode.Focus();
                    }

                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            try
            {
                loadProductDetails(true, txtProductCode.Text.Trim(), 0);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID)
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                LgsProductMasterService LgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = LgsProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingLgsProductMaster = LgsProductMasterService.GetProductsByName(strProduct); ;

                if (existingLgsProductMaster != null)
                {
                    LgsQuotationService lgsQuotationService = new LgsQuotationService();
                    if (lgsQuotationDetailsTempList == null)
                        lgsQuotationDetailsTempList = new List<LgsQuotationDetailTemp>();
                    existingLgsQuotationDetailTemp = lgsQuotationService.getQuotationDetailTemp(lgsQuotationDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                    if (existingLgsQuotationDetailTemp != null)
                    {
                        txtProductCode.Text = existingLgsQuotationDetailTemp.ProductCode;
                        txtProductName.Text = existingLgsQuotationDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingLgsQuotationDetailTemp.UnitOfMeasureID;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsQuotationDetailTemp.OrderQty);
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingLgsQuotationDetailTemp.CostPrice);
                        txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingLgsQuotationDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingLgsQuotationDetailTemp.DiscountPercentage);
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
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

        private void EnableProductDetails(bool enable) 
        {
            Common.EnableTextBox(enable, txtProductCode, txtProductName);
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtRate, txtProductDiscountPercentage, txtProductDiscountAmount, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty,txtRate, txtProductDiscountPercentage,txtProductDiscountAmount, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
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
                loadProductDetails(false, txtProductName.Text.Trim(), 0);
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
                        cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                    txtQty.Text = "1";
                txtQty.Focus();
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
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {
                        txtProductDiscountPercentage.Enabled = true;
                        txtProductDiscountPercentage.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscountAmount.Enabled = true;
                    txtProductDiscountAmount.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        
        private void txtProductDiscountAmount_KeyDown(object sender, KeyEventArgs e)
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductDiscountAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal productAmount = Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim());
                decimal discountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim());
                if (discountAmount >= productAmount)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.ProductDiscountAmountExceed);
                    txtProductDiscountAmount.Focus();
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

        private void txtProductAmount_Leave(object sender, EventArgs e)
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

        private void UpdateGrid(LgsQuotationDetailTemp lgsQuotationDetailTemp)
        {
            try
            {

                lgsQuotationDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                lgsQuotationDetailTemp.BaseUnitID = existingLgsProductMaster.UnitOfMeasureID;
                lgsQuotationDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                    txtQty.Text = (lgsQuotationDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text)).ToString();
                }

                lgsQuotationDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                CalculateLine();
                lgsQuotationDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                lgsQuotationDetailTemp.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                lgsQuotationDetailTemp.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                lgsQuotationDetailTemp.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text);
                lgsQuotationDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                LgsQuotationService lgsQuotationService = new LgsQuotationService();

                dgvItemDetails.DataSource = null;
                lgsQuotationDetailsTempList = lgsQuotationService.getUpdateQuotationDetailTemp(lgsQuotationDetailsTempList, lgsQuotationDetailTemp, existingLgsProductMaster);
                dgvItemDetails.DataSource = lgsQuotationDetailsTempList;
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

                GetSummarizeFigures(lgsQuotationDetailsTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtSalesPersonCode, txtSalesPersonName);
                Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (lgsQuotationDetailsTempList.Count > 0)
                    grpFooter.Enabled = true;

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void chkSubTotalDiscountPercentage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtSubTotalDiscountPercentage.Focus();
                GetSummarizeSubFigures();
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
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #region Methods.....

        /// <summary>
        /// Refresh paused document
        /// </summary>
        private void RefreshDocumentNumber() 
        {
            ////Load Quotation Document Numbers
            LgsQuotationService lgsQuotationService = new LgsQuotationService();
            Common.SetAutoComplete(txtDocumentNo, lgsQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);
        }

        #region SummarizingFigures


        /// <summary>
        /// Update Tax, Discount, Gross amount and Net amount
        /// Note: Read through refreshed List
        /// </summary>
        /// <param name="listItem"></param>
        private void GetSummarizeFigures(List<LgsQuotationDetailTemp> listItem)
        {
            CommonService commonService = new CommonService();
            LgsSupplierService lgsSupplierService = new LgsSupplierService();

            decimal tax1 = 0;
            decimal tax2 = 0;
            decimal tax3 = 0;
            decimal tax4 = 0;
            decimal tax5 = 0;

            //Get Gross Amount
            //Note: apply this for first para- && x.DocumentID == '<required Transaction Id>' && x.LocationID ==  'Transaction Location' && '<userID>' && 'machine id'
            decimal grossAmount = listItem.GetSummaryAmount(x => x.DocumentNo == txtDocumentNo.Text.Trim(), x => x.NetAmount);

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (grossAmount - discountAmount), lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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

        #endregion

        private void GetSummarizeSubFigures()
        {
            CommonService commonService = new CommonService();
            LgsSupplierService lgsSupplierService = new LgsSupplierService();

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (grossAmount - discountAmount), lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.ToString()).LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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

        private void CalculateLine()
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                    txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                else
                    txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                UpdateGrid(existingLgsQuotationDetailTemp);
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQty.Text.Trim())) { return; }
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

        private decimal GetLineDiscountTotal(List<LgsQuotationDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.DiscountAmount);

        }

        public override void Pause()
        {

            if ((Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    //GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }

        }

        public override void Save()
        {
            if ((Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Logistic Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtSupplierCode, txtSupplierName, txtSalesPersonCode, txtSalesPersonName);
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                LgsQuotationHeader lgsQuotationHeader = new LgsQuotationHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();
                InvSalesPerson invSalesPerson = new InvSalesPerson();
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();

                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());
                invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsQuotationHeader = lgsQuotationService.GetPausedQuotationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsQuotationHeader == null)
                    lgsQuotationHeader = new LgsQuotationHeader();

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                lgsQuotationHeader.CompanyID = Location.CompanyID;
                lgsQuotationHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                    lgsQuotationHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                lgsQuotationHeader.DocumentDate = Common.ConvertStringToDate(dtpQuotationDate.Value.ToString());
                lgsQuotationHeader.DeliveryDate = Common.ConvertStringToDate(dtpDeliveryDate.Value.ToString());

                lgsQuotationHeader.DocumentDate = Convert.ToDateTime(dtpQuotationDate.Value);
                lgsQuotationHeader.DeliveryDate = Convert.ToDateTime(dtpDeliveryDate.Value);

                lgsQuotationHeader.DocumentID = documentID;
                lgsQuotationHeader.DocumentStatus = documentStatus;
                lgsQuotationHeader.DocumentNo = documentNo.Trim();
                lgsQuotationHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                lgsQuotationHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsQuotationHeader.LineDiscountTotal = GetLineDiscountTotal(lgsQuotationDetailsTempList);
                lgsQuotationHeader.LocationID = Location.LocationID;
                lgsQuotationHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                lgsQuotationHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                lgsQuotationHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                lgsQuotationHeader.Remark = txtRemark.Text.Trim();
                lgsQuotationHeader.SupplierID = lgsSupplier.LgsSupplierID;
                lgsQuotationHeader.InvSalesPersonID = invSalesPerson.InvSalesPersonID;
                lgsQuotationHeader.PaymentMethodID = Convert.ToInt32(cmbPaymentTerms.SelectedValue);

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    lgsQuotationHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, (lgsQuotationHeader.GrossAmount - lgsQuotationHeader.DiscountAmount), lgsSupplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    lgsQuotationHeader.TaxAmount1 = tax1;
                    lgsQuotationHeader.TaxAmount2 = tax2;
                    lgsQuotationHeader.TaxAmount3 = tax3;
                    lgsQuotationHeader.TaxAmount4 = tax4;
                    lgsQuotationHeader.TaxAmount5 = tax5;

                }

                if (lgsQuotationDetailsTempList == null)
                    lgsQuotationDetailsTempList = new List<LgsQuotationDetailTemp>();

                return lgsQuotationService.Save(lgsQuotationHeader, lgsQuotationDetailsTempList);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;

            }


        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                LgsQuotationHeader lgsQuotationHeader = new LgsQuotationHeader();

                lgsQuotationHeader = lgsQuotationService.GetPausedQuotationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsQuotationHeader != null)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsSupplier lgsSupplier = new LgsSupplier();
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();
                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                    documentState = lgsQuotationHeader.DocumentStatus;
                    cmbLocation.SelectedValue = lgsQuotationHeader.LocationID;
                    cmbLocation.Refresh();

                    cmbPaymentTerms.SelectedValue = lgsQuotationHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    if (!lgsQuotationHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.DiscountAmount);
                    dtpQuotationDate.Value = Common.FormatDate(lgsQuotationHeader.DocumentDate);
                    dtpDeliveryDate.Value = Common.FormatDate(lgsQuotationHeader.DeliveryDate);

                    txtDocumentNo.Text = lgsQuotationHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.NetAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.OtherCharges);
                    txtReferenceNo.Text = lgsQuotationHeader.ReferenceNo;
                    txtRemark.Text = lgsQuotationHeader.Remark;
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(lgsQuotationHeader.SupplierID);
                    txtSupplierCode.Text = lgsSupplier.SupplierCode;
                    txtSupplierName.Text = lgsSupplier.SupplierName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(lgsQuotationHeader.InvSalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    if (!lgsQuotationHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    lgsQuotationDetailsTempList = lgsQuotationService.getPausedQuotationDetail(lgsQuotationHeader);
                    dgvItemDetails.DataSource = lgsQuotationDetailsTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);

                    if (lgsQuotationHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(false);
                        EnableProductDetails(true);
                        LoadProducts();
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {
                        grpFooter.Enabled = false;
                        EnableLine(false);
                        EnableProductDetails(false);
                        Common.EnableButton(false, btnSave, btnPause);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpQuotationDate.Value = DateTime.Now;
            dtpDeliveryDate.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            base.ClearForm();
        }

        #endregion


        private void chkAutoCompleationVendor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsSupplierService lgssupplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, lgssupplierService.GetAllLgsSupplierCodes(), chkAutoCompleationVendor.Checked);
                Common.SetAutoComplete(txtSupplierName, lgssupplierService.GetAllLgsSupplierNames(), chkAutoCompleationVendor.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void chkAutoCompleationQuotationNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                Common.SetAutoComplete(txtDocumentNo, lgsQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



        private void chkAutoCompleationSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
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
                txtProductCode.Focus();
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

        private void txtSalesPersonCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonCode.Text.Trim().Equals(string.Empty))
                    LoadSalesPerson(true, txtSalesPersonCode.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonName.Text.Trim().Equals(string.Empty))
                    LoadSalesPerson(true, txtSalesPersonCode.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    if (dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display", Toast.messageType.Information, Toast.messageAction.General, "");
                        return;
                    }
                    else
                    {
                        selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim(), unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID);
                        CalculateLine();
                        if (documentState.Equals(1))
                        {
                            EnableLine(false);
                            txtProductCode.Enabled = false;
                            txtProductName.Enabled = false;
                        }
                        else
                        {
                            txtQty.Enabled = true;
                            this.ActiveControl = txtQty;
                            txtQty.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void dgvItemDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                try
                {
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        LgsQuotationDetailTemp lgsQuotationTempDetail = new LgsQuotationDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsQuotationTempDetail.ProductID = lgsProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        lgsQuotationTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsQuotationService lgsQuotationService = new LgsQuotationService();

                        dgvItemDetails.DataSource = null;
                        lgsQuotationDetailsTempList = lgsQuotationService.GetDeleteQuotationDetailTemp(lgsQuotationDetailsTempList, lgsQuotationTempDetail);
                        dgvItemDetails.DataSource = lgsQuotationDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(lgsQuotationDetailsTempList);
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

        private void txtSubTotalDiscountPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                if (chkSubTotalDiscountPercentage.Checked)
                {
                    if (value >= 101)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountPercentageExceed);
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(value);
                        txtSubTotalDiscountPercentage.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        


        


    }
}
