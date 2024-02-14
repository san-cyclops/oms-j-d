using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;
using Domain;
using Utility;
using Service;
using System.Drawing.Printing;
using Report;
using Report.Logistic.Transactions.Reports;
using Report.Logistic;

namespace UI.Windows
{


    public partial class FrmLogisticPurchaseOrder : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailsTempList = new List<LgsPurchaseOrderDetailTemp>();
        LgsProductMaster existingLgsProductMaster;
        LgsPurchaseOrderDetailTemp existingLgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();
        bool isSupplierProduct;
        int documentID = 0;
        int documentState;
        bool isBackDated;
        //bool loadSelectionCriteriaByReorderLevel; 
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
         
        public FrmLogisticPurchaseOrder()
        {
            InitializeComponent();
        }

        #region Form Events

        private void chkAutoCompleationPoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                LoadPausedDocuments(); 
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void chkAutoCompleationPurchaseRequestNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

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
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);
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

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDocumentNo_Leave(this, e);
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
                if (e.KeyCode.Equals(Keys.Enter))
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
            if (e.KeyCode.Equals(Keys.Enter))
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
                    txtValidityPeriod.Focus();
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void dtpExpectedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    dtpExpectedDate_Leave(this, e);
                }
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void dtpExpectedDate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Common.ValidateDate(Common.FormatDate(dtpExpectedDate.Value)))
                {
                    cmbPaymentTerms.Focus();
                }
                else
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    dtpExpectedDate.Focus();
                    dtpExpectedDate.Select();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpPODate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    dtpPODate_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpPODate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Common.ValidateDate(Common.FormatDate(dtpPODate.Value)))
                {
                    dtpExpectedDate.Focus();
                    dtpExpectedDate.Select();
                }
                else
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    dtpPODate.Focus();
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
                Common.SetFocus(e, dtpPODate);
                dtpPODate.Select();
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
                {
                    cmbLocation.Focus();
                }
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
                {
                    if (dtpPODate.Enabled)
                    {
                        dtpPODate.Focus();
                        dtpPODate.Select();
                    }
                    else if (dtpExpectedDate.Enabled)
                    {
                        dtpExpectedDate.Focus();
                        dtpExpectedDate.Select();
                    }
                    else
                    {
                        cmbPaymentTerms.Focus();
                    }
                }
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
                {
                    cmbLocation_Validated(this, e);
                    txtProductCode.Focus();
                }
                else
                {
                    return;
                }
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
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }


                if (lgsSupplierService.IsExistsLgsSupplier(txtSupplierCode.Text.Trim()).Equals(false))
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
                        Common.EnableTextBox(true, txtProductCode, txtProductName);
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

        private void chkAutoCompleationProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                Common.SetFocus(e, txtProductCode); 
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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductName.Enabled = true;
                    txtProductName.Focus();
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTableForTransactions());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim());
                        txtProductCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;

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


        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!txtProductName.Text.Trim().Equals(string.Empty))
                    {
                        cmbUnit.Enabled = true;
                        cmbUnit.Focus();
                    }
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                    DataView dvAllReferenceData = new DataView(lgsProductMasterService.GetProductsDataTableForTransactions());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim());
                        txtProductCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPackSize_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPackSize_Validated(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void txtProductDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductDiscountAmount.Enabled = true;
                    txtProductDiscountAmount.Focus();
                    txtProductDiscountAmount.SelectAll();
                }
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierCode.Text.Trim() != "" && chkTaxEnable.Checked == true)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim()).LgsSupplierID, 3, (Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.Trim())));
                    frmTaxBreakdown.ShowDialog();
                }
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
                GetSummarizeSubFigures();
                txtSubTotalDiscountPercentage.Focus();
                txtSubTotalDiscountPercentage.SelectAll();
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

        private void FrmLogisticPurchaseOrder_Activated(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void FrmLogisticPurchaseOrder_Deactivate(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void chkOverwrite_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    cmbLocation_Validated(this, e);
                }
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
                if (string.IsNullOrEmpty(txtSupplierName.Text.Trim())) { return; }
                LoadSupplier(false, txtSupplierName.Text.Trim());
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
                {
                    LoadSupplier(true, txtSupplierCode.Text.Trim());
                    LoadProducts();
                }
                else
                {
                    return;
                }
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
                if (!string.IsNullOrEmpty(txtProductCode.Text.Trim()))
                {
                    loadProductDetails(true, txtProductCode.Text.Trim(), 0);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool IsProductExistsBySupplier()
        {
            bool status = false;
            try
            {
                LgsSupplier lgsSupplier = new LgsSupplier();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();

                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                if (lgsSupplier != null)
                {
                    if (existingLgsProductMaster.LgsSupplierID == lgsSupplier.LgsSupplierID)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return status;
        }

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID)
        {
            try
            {
                existingLgsProductMaster = new LgsProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                if (isCode)
                {
                    existingLgsProductMaster = lgsProductMasterService.GetProductsByCode(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingLgsProductMaster = lgsProductMasterService.GetProductsByName(strProduct); ;

                if (existingLgsProductMaster != null)
                {
                    if (isSupplierProduct)
                    {
                        if (IsProductExistsBySupplier())
                        {
                            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                            if (lgsPurchaseOrderDetailsTempList == null)
                                lgsPurchaseOrderDetailsTempList = new List<LgsPurchaseOrderDetailTemp>();
                            existingLgsPurchaseOrderDetailTemp = lgsPurchaseOrderService.GetPurchaseOrderDetailTemp(lgsPurchaseOrderDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                            if (existingLgsPurchaseOrderDetailTemp != null)
                            {
                                txtProductCode.Text = existingLgsPurchaseOrderDetailTemp.ProductCode;
                                txtProductName.Text = existingLgsPurchaseOrderDetailTemp.ProductName;
                                cmbUnit.SelectedValue = existingLgsPurchaseOrderDetailTemp.UnitOfMeasureID;
                                txtRate.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.CostPrice);
                                txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsPurchaseOrderDetailTemp.OrderQty);
                                txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingLgsPurchaseOrderDetailTemp.FreeQty);
                                //SQty
                                //PQty
                                txtPackSize.Text = existingLgsPurchaseOrderDetailTemp.PackSize;
                                txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.DiscountAmount);
                                txtProductDiscountPer.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.DiscountPercentage);
                                Common.EnableComboBox(true, cmbUnit);
                                if (unitofMeasureID.Equals(0))
                                    cmbUnit.Focus();


                                if (chkViewStokDetails.Checked)
                                {
                                    ShowStockDetails();
                                }
                            }
                        }
                        else
                        {
                            Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Supplier - " + txtSupplierCode.Text.Trim());
                            if (isCode) { txtProductCode.Focus(); }
                            else { txtProductName.Focus(); }
                        }
                    }
                    else
                    {
                        //////
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        if (lgsPurchaseOrderDetailsTempList == null)
                            lgsPurchaseOrderDetailsTempList = new List<LgsPurchaseOrderDetailTemp>();
                        existingLgsPurchaseOrderDetailTemp = lgsPurchaseOrderService.GetPurchaseOrderDetailTemp(lgsPurchaseOrderDetailsTempList, existingLgsProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                        if (existingLgsPurchaseOrderDetailTemp != null)
                        {
                            txtProductCode.Text = existingLgsPurchaseOrderDetailTemp.ProductCode;
                            txtProductName.Text = existingLgsPurchaseOrderDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingLgsPurchaseOrderDetailTemp.UnitOfMeasureID;
                            txtRate.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.CostPrice);
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingLgsPurchaseOrderDetailTemp.OrderQty);
                            txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingLgsPurchaseOrderDetailTemp.FreeQty);
                            //SQty
                            //PQty
                            txtPackSize.Text = existingLgsPurchaseOrderDetailTemp.PackSize;
                            txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.DiscountAmount);
                            txtProductDiscountPer.Text = Common.ConvertDecimalToStringCurrency(existingLgsPurchaseOrderDetailTemp.DiscountPercentage);

                            txtProductCode.Leave -= new EventHandler(txtProductCode_Leave);
                            Common.EnableComboBox(true, cmbUnit);
                            if (unitofMeasureID.Equals(0))
                                cmbUnit.Focus();
                            txtProductCode.Leave += new EventHandler(txtProductCode_Leave);

                            if (chkViewStokDetails.Checked)
                            {
                                ShowStockDetails();
                            }
                        }
                        /////
                    }
                }
                else
                {
                    Toast.Show("Product - " + strProduct + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    if (isCode) { txtProductCode.Focus(); }
                    else { txtProductName.Focus(); }
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ShowStockDetails()
        {
            try
            {
                CommonService CommonService = new CommonService();

                dgvStockDetails.DataSource = null;
                List<LgsProductStockMaster> lgsProductStockMasterList = new List<LgsProductStockMaster>();
                lgsProductStockMasterList = CommonService.GetLgsStockDetailsToStockGrid(existingLgsProductMaster);
                dgvStockDetails.DataSource = lgsProductStockMasterList;
                dgvStockDetails.Refresh();
                grpFooter1.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtFreeQty, txtPackSize, txtRate, txtProductDiscountAmount, txtProductDiscountPer, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void EnableProductDetails(bool enable)
        {
            Common.EnableTextBox(enable, txtProductCode, txtProductName);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtFreeQty, txtPackSize, txtRate, txtProductDiscountAmount, txtProductDiscountPer, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
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

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    //cmbUnit_Leave(this, e);
                    txtQty.Enabled = true;
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                        txtQty.Text = "1";
                    txtQty.Focus();
                    txtQty.SelectAll();
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
                        cmbUnit.SelectedValue = existingLgsProductMaster.UnitOfMeasureID;
                        cmbUnit.Focus();
                        return;
                    }
                }
                loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductDiscountPer_Leave(object sender, EventArgs e)
        {
            CalculateLine();
           
        }

        private void txtProductAmount_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void UpdateGrid(LgsPurchaseOrderDetailTemp lgsPurchaseOrderTempDetail)
        {
            try
            {
                decimal qty = 0;
                decimal freeqty = 0;

                 if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) > 0))
                 {
                    lgsPurchaseOrderTempDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    lgsPurchaseOrderTempDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                        qty = (lgsPurchaseOrderTempDetail.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                        freeqty = (lgsPurchaseOrderTempDetail.FreeQty + Common.ConvertStringToDecimalQty(txtFreeQty.Text));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        lgsPurchaseOrderTempDetail.OrderQty = Common.ConvertDecimalToDecimalQty(qty);
                        lgsPurchaseOrderTempDetail.FreeQty = Common.ConvertDecimalToDecimalQty(freeqty);
                        lgsPurchaseOrderTempDetail.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        lgsPurchaseOrderTempDetail.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                        lgsPurchaseOrderTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim());
                        lgsPurchaseOrderTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));

                    }

                    lgsPurchaseOrderTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                            
                    lgsPurchaseOrderTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim());
                    lgsPurchaseOrderTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text);
                    lgsPurchaseOrderTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);
                    
                    LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();

                    dgvItemDetails.DataSource = null;
                    lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetUpdatePurchaseOrderDetailTemp(lgsPurchaseOrderDetailsTempList, lgsPurchaseOrderTempDetail, existingLgsProductMaster);
                    dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
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

                    GetSummarizeFigures(lgsPurchaseOrderDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();
                    if (lgsPurchaseOrderDetailsTempList.Count > 0)
                    {
                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        grpFooter2.Enabled = true;
                    }

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
            if (e.KeyCode.Equals(Keys.Enter))
            {
                UpdateGrid(existingLgsPurchaseOrderDetailTemp);
                grpFooter2.Enabled = false;
            }
        }

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
                }
                else
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertDecimalToDecimalQty(qty), Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private decimal GetLineDiscountTotal(List<LgsPurchaseOrderDetailTemp> listItemPo)
        {
            return listItemPo.GetSummaryAmount(x => x.DiscountAmount);
        }

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
            //txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(netAmount);
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

        private bool ValidateTextBoxes() 
        {
            bool isValidatedBalnk = false;
            bool isValidatedZero = false;
            isValidatedZero = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtValidityPeriod);
            isValidatedBalnk = Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtSupplierCode, txtValidityPeriod);

            if (isValidatedZero.Equals(true) && isValidatedBalnk.Equals(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateComboBoxes() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbPaymentTerms, cmbLocation);
        }

        public override void Pause()
        {
            if ((Toast.Show("Logistic Purchase Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;

                if (saveDocument.Equals(true))
                {
                    Toast.Show("Logistic Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show("Logistic Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Logistic Purchase Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;

                if (saveDocument.Equals(true))
                {
                    Toast.Show("Logistic Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);                    
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show(" Logistic Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        public override void ClearForm()
        {
            base.ClearForm();
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            dgvStockDetails.DataSource = null;
            dgvStockDetails.Refresh();
            ResetDates();
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
            {
                RecallDocument(txtDocumentNo.Text.Trim());
            }
            else
            {
                txtDocumentNo.Text = GetDocumentNo(true);
                txtSupplierCode.Focus();
            }
        }

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
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
                            txtPackSize.Enabled = false;
                            txtRate.Enabled = false;
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

                        LgsPurchaseOrderDetailTemp lgsPurchaseOrderTempDetail = new LgsPurchaseOrderDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

                        lgsPurchaseOrderTempDetail.ProductID = lgsProductMasterService.GetProductsByCode(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).LgsProductMasterID;
                        lgsPurchaseOrderTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();

                        dgvItemDetails.DataSource = null;
                        lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetDeletePurchaseOrderDetailTemp(lgsPurchaseOrderDetailsTempList, lgsPurchaseOrderTempDetail);
                        dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(lgsPurchaseOrderDetailsTempList);
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

        private void cmbSelectionCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSelectionCriteria.SelectedIndex == 1)
                {
                    EnableSelectionCriteria(true);
                    if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()) && string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpPurchase;
                        Toast.Show("Please select supplier", Toast.messageType.Information, Toast.messageAction.General);
                        txtSupplierCode.Focus();
                        return;
                    }
                    else
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpPurchase;
                        LgsSupplier lgsSupplier = new LgsSupplier();
                        LgsSupplierService lgsSupplierService = new LgsSupplierService();
                        lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());
                        if (lgsSupplier != null)
                        {
                            txtOrderCircle.Text = lgsSupplier.OrderCircle.ToString();
                        }
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 2)
                {
                    EnableSelectionCriteria(true);
                    if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()) && string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpTransfer;
                        Toast.Show("Please select supplier", Toast.messageType.Information, Toast.messageAction.General);
                        txtSupplierCode.Focus();
                        return;
                    }
                    else
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpTransfer;
                        LgsSupplier lgsSupplier = new LgsSupplier();
                        LgsSupplierService lgsSupplierService = new LgsSupplierService();
                        lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());
                        if (lgsSupplier != null)
                        {
                            txtOrderCircle.Text = lgsSupplier.OrderCircle.ToString();
                        }
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 3)
                {
                    EnableSelectionCriteria(true);
                    tbMore.Enabled = true;
                    tbMore.SelectedTab = tbpTransfer;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSelectionCriteria.SelectedIndex == 1) //From Supplier
                {
                    if (ValidateTextBoxForSelectionCriteria().Equals(false)) { return; }
                    if (ValidateComboBoxForSelectionCriteria().Equals(false)) { return; }

                    if (cmbLocationStatus.SelectedIndex == 0) //Current Location
                    {
                        LgsSupplier lgsSupplier = new LgsSupplier();
                        LgsSupplierService lgsSupplierService = new LgsSupplierService();
                        lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                        DateTime fromDate = Common.FormatDate(dtpPurchaseFromDate.Value);
                        DateTime toDate = Common.FormatDate(dtpPurchaseToDate.Value);

                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetSelectionCriteriaBySupplierAndCurrentLocation(fromDate, toDate, lgsSupplier.LgsSupplierID, Convert.ToInt32(cmbLocation.SelectedValue), Convert.ToInt32(txtOrderCircle.Text));
                        dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(lgsPurchaseOrderDetailsTempList);

                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        grpFooter2.Enabled = true;

                        Common.EnableTextBox(true, txtProductCode, txtProductName);

                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else if (cmbLocationStatus.SelectedIndex == 1) //All Locations
                    {
                        LgsSupplier lgsSupplier = new LgsSupplier();
                        LgsSupplierService lgsSupplierService = new LgsSupplierService();
                        lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                        DateTime fromDate = Common.FormatDate(dtpPurchaseFromDate.Value);
                        DateTime toDate = Common.FormatDate(dtpPurchaseToDate.Value);

                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetSelectionCriteriaBySupplierAndAllLocations(fromDate, toDate, lgsSupplier.LgsSupplierID, Convert.ToInt32(txtOrderCircle.Text));
                        dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(lgsPurchaseOrderDetailsTempList);

                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        grpFooter2.Enabled = true;

                        Common.EnableTextBox(true, txtProductCode, txtProductName);

                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else if (cmbLocationStatus.SelectedIndex == 2) //Location Group
                    {

                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 2) //From Reorder Level
                {
                    if (cmbLocationStatus.SelectedIndex == 0) //Current Location
                    {
                        //loadSelectionCriteriaByReorderLevel = true;
                        //isSupplierProduct = false;
                        //LoadProducts();

                        LgsSupplier lgsSupplier = new LgsSupplier();
                        LgsSupplierService lgsSupplierService = new LgsSupplierService();
                        lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetSelectionCriteriaByReorderLevelAndCurrentLocation(Convert.ToInt32(cmbLocation.SelectedValue), lgsSupplier.LgsSupplierID);
                        dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(lgsPurchaseOrderDetailsTempList);

                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        grpFooter2.Enabled = true;

                        Common.EnableTextBox(true, txtProductCode, txtProductName);

                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    if (cmbLocationStatus.SelectedIndex == 1) //All Locations
                    {

                    }
                    if (cmbLocationStatus.SelectedIndex == 2) //Location Group
                    {

                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 3) //Base On Average Sales
                {
                    if (cmbLocationStatus.SelectedIndex == 0) //Current Location
                    {

                    }
                    if (cmbLocationStatus.SelectedIndex == 1) //All Locations
                    {

                    }
                    if (cmbLocationStatus.SelectedIndex == 2) //Location Group
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            try
            {
                dgvItemDetails.AutoGenerateColumns = false;
                dgvStockDetails.AutoGenerateColumns = false;

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                /////Load payment method name to combo
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                Common.LoadPaymentMethods(cmbPaymentTerms, paymentMethodService.GetAllPaymentMethods());

                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                Common.SetAutoComplete(txtSupplierCode, lgsSupplierService.GetAllLgsSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, lgsSupplierService.GetAllLgsSupplierNames(), chkAutoCompleationSupplier.Checked);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;
                isBackDated = autoGenerateInfo.IsBackDated;

                if (isBackDated)
                {
                    dtpPODate.Enabled = true;
                }
                else
                {
                    dtpPODate.Enabled = false;
                }

                cmbSelectionCriteria.SelectedIndex = 0;
                Common.EnableTextBox(false, txtProductCode, txtProductName);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                

                if (Common.tStatus == true) { chkTStatus.Visible = true; } else { chkTStatus.Visible = false; }

                base.FormLoad();

                ////Load PO Document Numbers
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                Common.SetAutoComplete(txtDocumentNo, lgsPurchaseOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);

                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                Common.SetAutoComplete(txtQuotationNo, lgsQuotationService.GetAllDocumentNumbersToPurchaseOrder(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotation.Checked);

                GetPrintingDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                grpFooter.Enabled = false;
                grpFooter1.Enabled = false;
                grpFooter2.Enabled = true;
                chkViewStokDetails.Enabled = true;
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtDocumentNo, txtReferenceNo, txtQuotationNo);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause, btnView);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                lgsPurchaseOrderDetailsTempList = null;
                existingLgsProductMaster = null;
                existingLgsPurchaseOrderDetailTemp = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbSelectionCriteria.SelectedIndex = 0;
                cmbLocationStatus.SelectedIndex = -1;
                cmbPaymentTerms.SelectedIndex = -1;
                EnableSelectionCriteria(false);

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;

                if (isBackDated)
                {
                    dtpPODate.Enabled = true;
                }
                else
                {
                    dtpPODate.Enabled = false;
                }

                txtDocumentNo.Text = GetDocumentNo(true);
                this.ActiveControl = txtSupplierCode;
                txtSupplierCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void LoadPausedDocuments()
        {
            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
            Common.SetAutoComplete(txtDocumentNo, lgsPurchaseOrderService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                LocationService locationService = new LocationService();
                return lgsPurchaseOrderService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
            try
            {
                LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
                if (isSupplierProduct)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsSupplier lgsSupplier = new LgsSupplier();
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                    if (lgsSupplier != null)
                    {
                        Common.SetAutoComplete(txtProductCode, lgsProductMasterService.GetAllProductCodesBySupplier(lgsSupplier.LgsSupplierID), chkAutoCompleationProduct.Checked);
                        Common.SetAutoComplete(txtProductName, lgsProductMasterService.GetAllProductNamesBySupplier(lgsSupplier.LgsSupplierID), chkAutoCompleationProduct.Checked);
                    }
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


        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                LgsSupplierService lgsSupplierService = new LgsSupplierService();
                LgsSupplier lgsSupplier = new LgsSupplier();

                lgsSupplier = lgsSupplierService.GetLgsSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetPausedPurchaseOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsPurchaseOrderHeader == null)
                    lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();

                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                lgsPurchaseOrderHeader.CompanyID = Location.CompanyID;
                lgsPurchaseOrderHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                    lgsPurchaseOrderHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                lgsPurchaseOrderHeader.DocumentDate = Common.FormatDate(dtpPODate.Value);
                lgsPurchaseOrderHeader.ExpectedDate = Common.FormatDate(dtpExpectedDate.Value);
                lgsPurchaseOrderHeader.ExpiryDate = Common.FormatDate(Common.FormatDate(dtpPODate.Value).AddDays(Convert.ToDouble(txtValidityPeriod.Text.Trim())));
                lgsPurchaseOrderHeader.PaymentExpectedDate = Common.FormatDate(dtpPaymentExpectedDate.Value);
                lgsPurchaseOrderHeader.DocumentID = documentID;
                lgsPurchaseOrderHeader.DocumentStatus = documentStatus;
                lgsPurchaseOrderHeader.DocumentNo = documentNo.Trim();
                lgsPurchaseOrderHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                lgsPurchaseOrderHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                lgsPurchaseOrderHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                lgsPurchaseOrderHeader.IsConsignmentBasis = chkConsignmentBasis.Checked;
                lgsPurchaseOrderHeader.ValidityPeriod = Common.ConvertStringToInt(txtValidityPeriod.Text.Trim());
                lgsPurchaseOrderHeader.PaymentMethodID = Convert.ToInt32(cmbPaymentTerms.SelectedValue);

                lgsPurchaseOrderHeader.LocationID = Location.LocationID;
                lgsPurchaseOrderHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());

                lgsPurchaseOrderHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                lgsPurchaseOrderHeader.Remark = txtRemark.Text.Trim();
                lgsPurchaseOrderHeader.LgsSupplierID = lgsSupplier.LgsSupplierID;
                lgsPurchaseOrderHeader.IsUpLoad = chkTStatus.Checked;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    lgsPurchaseOrderHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(3, (lgsPurchaseOrderHeader.GrossAmount - lgsPurchaseOrderHeader.DiscountAmount), lgsSupplier.LgsSupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    lgsPurchaseOrderHeader.TaxAmount1 = tax1;
                    lgsPurchaseOrderHeader.TaxAmount2 = tax2;
                    lgsPurchaseOrderHeader.TaxAmount3 = tax3;
                    lgsPurchaseOrderHeader.TaxAmount4 = tax4;
                    lgsPurchaseOrderHeader.TaxAmount5 = tax5;

                }

                if (lgsPurchaseOrderDetailsTempList == null)
                    lgsPurchaseOrderDetailsTempList = new List<LgsPurchaseOrderDetailTemp>();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                LgsPurchaseOrderService.poIsMandatory = autoGenerateInfo.PoIsMandatory;

                return lgsPurchaseOrderService.Save(lgsPurchaseOrderHeader, lgsPurchaseOrderDetailsTempList, out newDocumentNo, this.Name);

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                this.Cursor = Cursors.Default;

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            try
            {
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();

                lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetPausedPurchaseOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (lgsPurchaseOrderHeader != null)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsSupplier lgsSupplier = new LgsSupplier();
                    PaymentMethod paymentMethod = new PaymentMethod();
                    PaymentMethodService paymentMethodService = new PaymentMethodService();

                    cmbLocation.SelectedValue = lgsPurchaseOrderHeader.LocationID;
                    cmbLocation.Refresh();

                    cmbPaymentTerms.SelectedValue = lgsPurchaseOrderHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    documentState = lgsPurchaseOrderHeader.DocumentStatus;

                    if (!lgsPurchaseOrderHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.DiscountAmount);
                    dtpPODate.Value = Common.FormatDate(lgsPurchaseOrderHeader.DocumentDate);
                    dtpExpectedDate.Value = Common.FormatDate(lgsPurchaseOrderHeader.ExpectedDate);
                    dtpPaymentExpectedDate.Value = Common.FormatDate(lgsPurchaseOrderHeader.PaymentExpectedDate);

                    txtDocumentNo.Text = lgsPurchaseOrderHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.NetAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.OtherCharges);

                    txtReferenceNo.Text = lgsPurchaseOrderHeader.ReferenceNo;
                    txtRemark.Text = lgsPurchaseOrderHeader.Remark;
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(lgsPurchaseOrderHeader.LgsSupplierID);
                    txtSupplierCode.Text = lgsSupplier.SupplierCode;
                    txtSupplierName.Text = lgsSupplier.SupplierName;

                    txtValidityPeriod.Text = lgsPurchaseOrderHeader.ValidityPeriod.ToString();

                    if (lgsPurchaseOrderHeader.IsConsignmentBasis) { chkConsignmentBasis.Checked = true; }
                    else { chkConsignmentBasis.Checked = false; }

                    if (!lgsPurchaseOrderHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(lgsPurchaseOrderHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetPausedPurchaseOrderDetail(lgsPurchaseOrderHeader);
                    dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtReferenceNo);
                    Common.EnableComboBox(false, cmbLocation);
                    grpFooter2.Enabled = false;

                    if (lgsPurchaseOrderHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        //grpFooter2.Enabled = true;
                        EnableLine(true);
                        EnableProductDetails(true);
                        LoadProducts();
                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else
                    {

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

        private void EnableSelectionCriteria(bool status)
        {
            //cmbSelectionCriteria.SelectedIndex = 0;
            txtOrderCircle.Enabled = status;
            cmbLocationStatus.Enabled = status;
            cmbGroup.Enabled = status;
            tbMore.Enabled = status;
            btnLoadData.Enabled = status;
            tbMore.SelectedTab = tbpTransfer;
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
                        txtSupplierName.Focus();
                        return;
                    }

                }
                else
                    existingLgsSupplier = lgsSupplierService.GetLgsSupplierByName(strsupplier);

                if (existingLgsSupplier != null)
                {
                    txtSupplierCode.Text = existingLgsSupplier.SupplierCode;
                    txtSupplierName.Text = existingLgsSupplier.SupplierName;
                    cmbPaymentTerms.SelectedValue = existingLgsSupplier.PaymentMethod;
                    chkTStatus.Checked = existingLgsSupplier.IsUpload;
                    if (existingLgsSupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); } else { Common.EnableCheckBox(true, chkTStatus); }
                    
                    txtRemark.Focus();
                    txtOrderCircle.Text = existingLgsSupplier.OrderCircle.ToString();

                    if (existingLgsSupplier.TaxID1 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID2 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID3 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID4 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingLgsSupplier.TaxID5 != 0) { chkTaxEnable.Checked = true; return; }
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

        private decimal GettotalQty(List<LgsPurchaseOrderDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.OrderQty);
        }

        #region SummarizingFigures
        /// <summary>
        /// Update Tax, Discount, Gross amount and Net amount
        /// Note: Read through refreshed List
        /// </summary>
        /// <param name="listItem"></param>
        private void GetSummarizeFigures(List<LgsPurchaseOrderDetailTemp> listItem)
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

            txtTotalQty.Text = Common.ConvertDecimalToStringQty(GettotalQty(listItem));
        }
        #endregion


        #region Validate Logics



        #endregion


        /// <summary>
        /// Validate form controls
        /// </summary>
        /// <returns></returns>
        //private bool ValidateControls()
        //{


        //    if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtProductCode, txtProductName, txtNameOnInvoice, txtDepartmentCode, txtCategoryCode, txtSubCategoryCode, txtSubCategory2Code, txtMainSupplierCode, txtCostPrice, txtSellingPrice, txtMaximumPrice, txtMinimumPrice))
        //    { return false; }
        //    else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbUnit))
        //    { return false; }
        //    else if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtPackSize))
        //    { return false; }

        //    else
        //    {
        //        isValidControls = true;
        //        this.ValidateChildren();


        //        return isValidControls;
        //    }
        //    return true;
        //}


        public void CopyValues<TSource, TTarget>(TSource source, TTarget target)
        {
            var sourceProperties = typeof(TSource).GetProperties().Where(p => p.CanRead);

            foreach (var property in sourceProperties)
            {
                var targetProperty = typeof(TTarget).GetProperty(property.Name);

                if (targetProperty != null && targetProperty.CanWrite && targetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    var value = property.GetValue(source, null);

                    targetProperty.SetValue(target, value, null);
                }
            }
        }


        private void ResetDates()
        {
            dtpPODate.Value = DateTime.Now;
            dtpExpectedDate.Value = DateTime.Now;
            dtpPurchaseFromDate.Value = DateTime.Now;
            dtpPurchaseToDate.Value = DateTime.Now;
            dtpTransferFromDate.Value = DateTime.Now;
            dtpTransferToDate.Value = DateTime.Now;
            dtpPaymentExpectedDate.Value = DateTime.Now;
        }

        private void RefreshDocumentNumbers()
        {
            ////Load PO Document Numbers
            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
            Common.SetAutoComplete(txtDocumentNo, lgsPurchaseOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
        }

        private bool ValidateTextBoxForSelectionCriteria()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Date, txtOrderCircle);
        }

        private bool ValidateComboBoxForSelectionCriteria()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocationStatus);
        }

        #endregion

        private void btnPoDetails_Click(object sender, EventArgs e)
        {
            
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {
                        //txtProductDiscountPer.Enabled = true;
                        //txtProductDiscountPer.Focus();
                        txtFreeQty.Enabled = true;
                        txtFreeQty.Focus();
                        txtFreeQty.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_Leave(object sender, EventArgs e)
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

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {
                        txtRate.Enabled = true;
                        txtRate.Focus();
                        txtRate.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtFreeQty_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void txtProductDiscountAmount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSubTotalDiscountPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            GetSummarizeSubFigures();
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

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                GetSummarizeSubFigures();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtProductDiscountPer.Enabled = true;
                    txtProductDiscountPer.Focus();
                    txtProductDiscountPer.SelectAll();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRate_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void txtValidityPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                Common.SetFocus(e, dtpPaymentExpectedDate); 
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void cmbLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //txtDocumentNo.Text = GetDocumentNo(true);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void GetPrintingDetails()
        {
            try
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
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void DisplayStockDetails()
        {
            try
            {
                if (chkViewStokDetails.Checked == false) { dgvStockDetails.DataSource = null; }
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
                DisplayStockDetails();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQuotationNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtQuotationNo_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtQuotationNo_Leave(object sender, EventArgs e)
        {
            try
            {
                ReCallQuotation(txtQuotationNo.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ReCallQuotation(string quotationNo)
        {
            try
            {
                LgsQuotationHeader lgsQuotationHeader = new LgsQuotationHeader();
                LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();

                lgsQuotationHeader = lgsPurchaseOrderService.GetQuotationHeaderToPurchaseOrder(txtQuotationNo.Text.Trim());
                if (lgsQuotationHeader != null)
                {
                    LgsSupplierService lgsSupplierService = new LgsSupplierService();
                    LgsSupplier lgsSupplier = new LgsSupplier();

                    cmbLocation.SelectedValue = lgsQuotationHeader.LocationID;
                    cmbLocation.Refresh();

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

                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(lgsQuotationHeader.NetAmount);

                    txtReferenceNo.Text = lgsQuotationHeader.ReferenceNo;
                    txtRemark.Text = lgsQuotationHeader.Remark;
                    lgsSupplier = lgsSupplierService.GetLgsSupplierByID(lgsQuotationHeader.SupplierID);
                    txtSupplierCode.Text = lgsSupplier.SupplierCode;
                    txtSupplierName.Text = lgsSupplier.SupplierName;

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
                    lgsPurchaseOrderDetailsTempList = lgsPurchaseOrderService.GetPausedQuotationDetail(lgsQuotationHeader);
                    dgvItemDetails.DataSource = lgsPurchaseOrderDetailsTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtQuotationNo, txtReferenceNo);
                    Common.EnableComboBox(false, cmbLocation);

                    grpFooter.Enabled = true;
                    grpFooter1.Enabled = false;
                    grpFooter2.Enabled = false;
                    EnableLine(false);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

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

        private void chkAutoCompleationQuotation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LgsQuotationService lgsQuotationService = new LgsQuotationService();
                Common.SetAutoComplete(txtQuotationNo, lgsQuotationService.GetAllDocumentNumbersToPurchaseOrder(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotation.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpPaymentExpectedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
