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
using Report.Inventory.Transactions.Reports;
using Report.Inventory;

namespace UI.Windows
{


    public partial class FrmPurchaseOrder : UI.Windows.FrmBaseTransactionForm
    {

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailsTempList = new List<InvPurchaseOrderDetailTemp>();
        InvProductMaster existingInvProductMaster;
        InvPurchaseOrderDetailTemp existingInvPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();
        bool isSupplierProduct;
        int documentID = 0;
        int documentState;
        bool isBackDated;
        //bool loadSelectionCriteriaByReorderLevel; 
        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
         
        public FrmPurchaseOrder()
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
                SupplierService supplierService = new SupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked);
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
            if (e.KeyCode.Equals(Keys.Enter))
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
                cmbLocation.Focus();
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
                dtpExpectedDate.Focus();
                dtpExpectedDate.Select();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtValidityPeriod_Validated(object sender, EventArgs e)
        {
            try
            { 

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
                    if (dtpPODate.Enabled)
                    {
                        dtpPODate.Focus();
                    }
                    else if (dtpExpectedDate.Enabled)
                    {
                        dtpExpectedDate.Focus();
                    }
                    else
                    {
                        cmbLocation.Focus();
                    }
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
                Common.SetFocus(e, cmbPaymentTerms); 
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
                SupplierService supplierService = new SupplierService();
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }


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
                    SupplierService supplierService = new SupplierService();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTableForTransactions(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
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
                    SupplierService supplierService = new SupplierService();
                    InvProductMasterService invProductMasterService = new InvProductMasterService();
                    DataView dvAllReferenceData = new DataView(invProductMasterService.GetProductsDataTableForTransactions(supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim()).SupplierID));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtProductCode);
                        txtProductCode_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        private void txtPackSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
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

        private void FrmPurchaseOrder_Activated(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
        }

        private void FrmPurchaseOrder_Deactivate(object sender, EventArgs e)
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
                Supplier supplier = new Supplier();
                SupplierService supplierService = new SupplierService();

                supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                if (supplier != null)
                {
                    if (existingInvProductMaster.SupplierID == supplier.SupplierID)
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
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                    return;

                InvProductMasterService invProductMasterService = new InvProductMasterService();

                if (isCode)
                {
                    existingInvProductMaster = invProductMasterService.GetProductsByRefCodes(strProduct);
                    if (isCode && strProduct.Equals(string.Empty))
                    {
                        txtProductName.Focus();
                        return;
                    }
                }
                else
                    existingInvProductMaster = invProductMasterService.GetProductsByName(strProduct); ;

                if (existingInvProductMaster != null)
                {
                    if (isSupplierProduct)
                    {
                        if (IsProductExistsBySupplier())
                        {
                            InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                            if (invPurchaseOrderDetailsTempList == null)
                                invPurchaseOrderDetailsTempList = new List<InvPurchaseOrderDetailTemp>();
                            existingInvPurchaseOrderDetailTemp = invPurchaseOrderService.GetPurchaseOrderDetailTemp(invPurchaseOrderDetailsTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);

                            if (existingInvPurchaseOrderDetailTemp != null)
                            {
                                txtProductCode.Text = existingInvPurchaseOrderDetailTemp.ProductCode;
                                txtProductName.Text = existingInvPurchaseOrderDetailTemp.ProductName;
                                cmbUnit.SelectedValue = existingInvPurchaseOrderDetailTemp.UnitOfMeasureID;
                                txtRate.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.CostPrice);
                                txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.SellingPrice);
                                txtQty.Text = Common.ConvertDecimalToStringQty(existingInvPurchaseOrderDetailTemp.OrderQty);
                                txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingInvPurchaseOrderDetailTemp.FreeQty);
                                txtPackSize.Text = existingInvPurchaseOrderDetailTemp.PackSize;
                                txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.DiscountAmount);
                                txtProductDiscountPer.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.DiscountPercentage);

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
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        if (invPurchaseOrderDetailsTempList == null)
                            invPurchaseOrderDetailsTempList = new List<InvPurchaseOrderDetailTemp>();
                        existingInvPurchaseOrderDetailTemp = invPurchaseOrderService.GetPurchaseOrderDetailTemp(invPurchaseOrderDetailsTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);

                        if (existingInvPurchaseOrderDetailTemp != null)
                        {
                            txtProductCode.Text = existingInvPurchaseOrderDetailTemp.ProductCode;
                            txtProductName.Text = existingInvPurchaseOrderDetailTemp.ProductName;
                            cmbUnit.SelectedValue = existingInvPurchaseOrderDetailTemp.UnitOfMeasureID;
                            txtRate.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.CostPrice);
                            txtSellingPrice.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.SellingPrice);
                            txtQty.Text = Common.ConvertDecimalToStringQty(existingInvPurchaseOrderDetailTemp.OrderQty);
                            txtFreeQty.Text = Common.ConvertDecimalToStringQty(existingInvPurchaseOrderDetailTemp.FreeQty);
                            txtPackSize.Text = existingInvPurchaseOrderDetailTemp.PackSize;
                            txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.DiscountAmount);
                            txtProductDiscountPer.Text = Common.ConvertDecimalToStringCurrency(existingInvPurchaseOrderDetailTemp.DiscountPercentage);
                            
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
                List<InvProductStockMaster> invProductStockMasterList = new List<InvProductStockMaster>();
                invProductStockMasterList = CommonService.GetInvStockDetailsToStockGrid(existingInvProductMaster);
                dgvStockDetails.DataSource = invProductStockMasterList;
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
            Common.EnableTextBox(enable, txtQty, txtFreeQty, txtPackSize, txtRate, txtSellingPrice, txtProductDiscountAmount, txtProductDiscountPer, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void EnableProductDetails(bool enable)
        {
            Common.EnableTextBox(enable, txtProductCode, txtProductName);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtFreeQty, txtPackSize, txtRate, txtSellingPrice, txtProductDiscountAmount, txtProductDiscountPer, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }
        private void txtProductName_Leave(object sender, EventArgs e)
        {
            loadProductDetails(false, txtProductName.Text.Trim(), 0);
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            if (cmbUnit.SelectedValue == null)
                return;

            if (!existingInvProductMaster.UnitOfMeasureID.Equals(Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())))
            {
                InvProductUnitConversionService invProductUnitConversionService = new InvProductUnitConversionService();
                if (invProductUnitConversionService.GetProductUnitByProductCode(existingInvProductMaster.InvProductMasterID, Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString())) == null)
                {
                    Toast.Show("Unit - " + cmbUnit.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "Product - " + txtProductCode.Text.Trim() + " - " + txtProductName.Text.Trim() + "");
                    cmbUnit.SelectedValue = existingInvProductMaster.UnitOfMeasureID;
                    cmbUnit.Focus();
                    return;
                }
            }
            loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));
        }

        private void txtProductDiscountPer_Leave(object sender, EventArgs e)
        {
            CalculateLine();
            //if ((existingInvProductMaster.MinimumPrice * Common.ConvertStringToDecimalQty(txtQty.Text)) > Common.ConvertStringToDecimalCurrency(txtProductAmount.Text))
            //{
            //    Toast.Show("", Toast.messageType.Information, Toast.messageAction.ProductDiscountAmountExceedMinimum);
            //    txtProductDiscountAmount.Clear();
            //    txtProductDiscountPer.Focus();
            //    txtProductDiscountPer.SelectAll();
            //    return;
            //}
        }

        private void txtProductAmount_Leave(object sender, EventArgs e)
        {
            CalculateLine();
        }

        private void UpdateGrid(InvPurchaseOrderDetailTemp invPurchaseOrderTempDetail)
        {
            try
            {
                decimal qty = 0;
                decimal freeqty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim()) + Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) > 0))
                {
                    invPurchaseOrderTempDetail.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invPurchaseOrderTempDetail.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                        qty = (invPurchaseOrderTempDetail.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                        freeqty = (invPurchaseOrderTempDetail.FreeQty + Common.ConvertStringToDecimalQty(txtFreeQty.Text));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        invPurchaseOrderTempDetail.OrderQty = Common.ConvertDecimalToDecimalQty(qty);
                        invPurchaseOrderTempDetail.FreeQty = Common.ConvertDecimalToDecimalQty(freeqty);
                        invPurchaseOrderTempDetail.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        invPurchaseOrderTempDetail.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text.Trim());
                        invPurchaseOrderTempDetail.FreeQty = Common.ConvertStringToDecimalQty(txtFreeQty.Text.Trim());
                        invPurchaseOrderTempDetail.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                        
                    }

                    invPurchaseOrderTempDetail.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                    invPurchaseOrderTempDetail.SellingPrice = Common.ConvertStringToDecimalCurrency(txtSellingPrice.Text);
                   
                    invPurchaseOrderTempDetail.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPer.Text.Trim());
                    invPurchaseOrderTempDetail.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text);
                    invPurchaseOrderTempDetail.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);

                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();

                    dgvItemDetails.DataSource = null;
                    invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetUpdatePurchaseOrderDetailTemp(invPurchaseOrderDetailsTempList, invPurchaseOrderTempDetail, existingInvProductMaster);
                    dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
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

                    GetSummarizeFigures(invPurchaseOrderDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    ClearLine();
                    if (invPurchaseOrderDetailsTempList.Count > 0)
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
                invPurchaseOrderTempDetail =null;
                txtProductCode.Focus();
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                UpdateGrid(existingInvPurchaseOrderDetailTemp);
                grpFooter2.Enabled = false;
            }
        }

        private void CalculateLine(decimal qty=0)
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

        private decimal GetLineDiscountTotal(List<InvPurchaseOrderDetailTemp> listItemPo)
        {
            return listItemPo.GetSummaryAmount(x => x.DiscountAmount);
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
            if ((Toast.Show("Purchase Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                //if (SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo).Equals(true))
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Purchase Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateTextBoxes().Equals(false)) { return; }
                if (ValidateComboBoxes().Equals(false)) { return; }

                this.Cursor = Cursors.WaitCursor;
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                this.Cursor = Cursors.Default;
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);                    
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    RefreshDocumentNumbers();
                    ClearForm();
                }
                else
                {
                    Toast.Show("Purchase Order  - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
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
                int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                try
                {
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        InvPurchaseOrderDetailTemp invPurchaseOrderTempDetail = new InvPurchaseOrderDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invPurchaseOrderTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        invPurchaseOrderTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();

                        dgvItemDetails.DataSource = null;
                        invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetDeletePurchaseOrderDetailTemp(invPurchaseOrderDetailsTempList, invPurchaseOrderTempDetail);
                        dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(invPurchaseOrderDetailsTempList);
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
                        Supplier supplier = new Supplier();
                        SupplierService supplierService = new SupplierService();
                        supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());
                        if (supplier != null)
                        {
                            txtOrderCircle.Text = supplier.OrderCircle.ToString();
                        }
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 2)
                {
                    EnableSelectionCriteria(true);
                    if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()) && string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpSales;
                        Toast.Show("Please select supplier", Toast.messageType.Information, Toast.messageAction.General);
                        txtSupplierCode.Focus();
                        return;
                    }
                    else
                    {
                        tbMore.Enabled = true;
                        tbMore.SelectedTab = tbpSales;
                        Supplier supplier = new Supplier();
                        SupplierService supplierService = new SupplierService();
                        supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());
                        if (supplier != null)
                        {
                            txtOrderCircle.Text = supplier.OrderCircle.ToString();
                        }
                    }
                }
                else if (cmbSelectionCriteria.SelectedIndex == 3)
                {
                    EnableSelectionCriteria(true);
                    tbMore.Enabled = true;
                    tbMore.SelectedTab = tbpSales;
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
                        Supplier supplier = new Supplier();
                        SupplierService supplierService = new SupplierService();
                        supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                        DateTime fromDate = Common.FormatDate(dtpPurchaseFrom.Value);
                        DateTime toDate = Common.FormatDate(dtpPurchaseTo.Value);

                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetSelectionCriteriaBySupplierAndCurrentLocation(fromDate, toDate, supplier.SupplierID, Convert.ToInt32(cmbLocation.SelectedValue), Convert.ToInt32(txtOrderCircle.Text));
                        dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(invPurchaseOrderDetailsTempList);

                        grpFooter.Enabled = true;
                        grpFooter1.Enabled = true;
                        grpFooter2.Enabled = true;

                        Common.EnableTextBox(true, txtProductCode, txtProductName);

                        if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    }
                    else if (cmbLocationStatus.SelectedIndex == 1) //All Locations
                    {
                        Supplier supplier = new Supplier();
                        SupplierService supplierService = new SupplierService();
                        supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                        DateTime fromDate = Common.FormatDate(dtpPurchaseFrom.Value);
                        DateTime toDate = Common.FormatDate(dtpPurchaseTo.Value);

                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetSelectionCriteriaBySupplierAndAllLocations(fromDate, toDate, supplier.SupplierID, Convert.ToInt32(txtOrderCircle.Text));
                        dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(invPurchaseOrderDetailsTempList);

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

                        Supplier supplier = new Supplier();
                        SupplierService supplierService = new SupplierService();
                        supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetSelectionCriteriaByReorderLevelAndCurrentLocation(Convert.ToInt32(cmbLocation.SelectedValue), supplier.SupplierID);
                        dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
                        dgvItemDetails.Refresh();

                        GetSummarizeFigures(invPurchaseOrderDetailsTempList);

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

                SupplierService supplierService = new SupplierService();
                Common.SetAutoComplete(txtSupplierCode, supplierService.GetSupplierCodes(), chkAutoCompleationSupplier.Checked);
                Common.SetAutoComplete(txtSupplierName, supplierService.GetSupplierNames(), chkAutoCompleationSupplier.Checked);

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

                if (Common.tStatus == true) { chkTStatus.Visible = true; } else { chkTStatus.Visible = false; }

                base.FormLoad();

                ////Load PO Document Numbers
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                Common.SetAutoComplete(txtDocumentNo, invPurchaseOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);

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
                EnableLine(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtSupplierCode, txtSupplierName, txtDocumentNo, txtReferenceNo);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause, btnView);

                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                invPurchaseOrderDetailsTempList = null;
                existingInvProductMaster = null;
                existingInvPurchaseOrderDetailTemp = null;
                cmbLocation.SelectedValue = Common.LoggedLocationID;
                cmbSelectionCriteria.SelectedIndex = 0;
                cmbLocationStatus.SelectedIndex = -1;
                cmbPaymentTerms.SelectedIndex = -1;
                EnableSelectionCriteria(false);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;
                lblVatNo.Text = "";

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
            InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
            Common.SetAutoComplete(txtDocumentNo, invPurchaseOrderService.GetPausedDocumentNumbers(), chkAutoCompleationPoNo.Checked);
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                LocationService locationService = new LocationService();
                return invPurchaseOrderService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                if (isSupplierProduct)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                    if (supplier != null)
                    {
                        Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodesBySupplier(supplier.SupplierID), chkAutoCompleationProduct.Checked);
                        Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNamesBySupplier(supplier.SupplierID), chkAutoCompleationProduct.Checked);
                    }
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


        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                SupplierService supplierService = new SupplierService();
                Supplier supplier = new Supplier();

                supplier = supplierService.GetSupplierByCode(txtSupplierCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invPurchaseOrderHeader = invPurchaseOrderService.GetPausedPurchaseOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invPurchaseOrderHeader == null)
                    invPurchaseOrderHeader = new InvPurchaseOrderHeader();

                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                invPurchaseOrderHeader.CompanyID = Location.CompanyID;
                invPurchaseOrderHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                    invPurchaseOrderHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());
                invPurchaseOrderHeader.DocumentDate = Common.FormatDate(dtpPODate.Value);
                invPurchaseOrderHeader.ExpectedDate = Common.FormatDate(dtpExpectedDate.Value);
                invPurchaseOrderHeader.ExpiryDate = Common.FormatDate(Common.FormatDate(dtpPODate.Value).AddDays(Convert.ToDouble(txtValidityPeriod.Text.Trim())));
                invPurchaseOrderHeader.PaymentExpectedDate = Common.FormatDate(dtpPaymentExpectedDate.Value);
                invPurchaseOrderHeader.DocumentID = documentID;
                invPurchaseOrderHeader.DocumentStatus = documentStatus;
                invPurchaseOrderHeader.DocumentNo = documentNo.Trim();
                invPurchaseOrderHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invPurchaseOrderHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invPurchaseOrderHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invPurchaseOrderHeader.IsConsignmentBasis = chkConsignmentBasis.Checked;
                invPurchaseOrderHeader.ValidityPeriod = Common.ConvertStringToInt(txtValidityPeriod.Text.Trim());
                invPurchaseOrderHeader.PaymentMethodID = Convert.ToInt32(cmbPaymentTerms.SelectedValue);

                invPurchaseOrderHeader.LocationID = Location.LocationID;
                invPurchaseOrderHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());

                invPurchaseOrderHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invPurchaseOrderHeader.Remark = txtRemark.Text.Trim();
                invPurchaseOrderHeader.SupplierID = supplier.SupplierID;
                invPurchaseOrderHeader.IsUpLoad = chkTStatus.Checked;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    invPurchaseOrderHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(1, (invPurchaseOrderHeader.GrossAmount - invPurchaseOrderHeader.DiscountAmount), supplier.SupplierID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invPurchaseOrderHeader.TaxAmount1 = tax1;
                    invPurchaseOrderHeader.TaxAmount2 = tax2;
                    invPurchaseOrderHeader.TaxAmount3 = tax3;
                    invPurchaseOrderHeader.TaxAmount4 = tax4;
                    invPurchaseOrderHeader.TaxAmount5 = tax5;

                }

                if (invPurchaseOrderDetailsTempList == null)
                    invPurchaseOrderDetailsTempList = new List<InvPurchaseOrderDetailTemp>();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                InvPurchaseOrderService.poIsMandatory = autoGenerateInfo.PoIsMandatory;

                return invPurchaseOrderService.Save(invPurchaseOrderHeader, invPurchaseOrderDetailsTempList, out newDocumentNo, this.Name);

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
            InvReportGenerator invReportGenerator = new InvReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);            
        }

        private bool RecallDocument(string documentNo)
        {
            try
            {
                InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();

                invPurchaseOrderHeader = invPurchaseOrderService.GetPausedPurchaseOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invPurchaseOrderHeader != null)
                {
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = new Supplier();
                    PaymentMethod paymentMethod = new PaymentMethod();
                    PaymentMethodService paymentMethodService = new PaymentMethodService();

                    cmbLocation.SelectedValue = invPurchaseOrderHeader.LocationID;
                    cmbLocation.Refresh();

                    cmbPaymentTerms.SelectedValue = invPurchaseOrderHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    documentState = invPurchaseOrderHeader.DocumentStatus;

                    if (!invPurchaseOrderHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.DiscountAmount);
                    dtpPODate.Value = Common.FormatDate(invPurchaseOrderHeader.DocumentDate);
                    dtpExpectedDate.Value = Common.FormatDate(invPurchaseOrderHeader.ExpectedDate);
                    dtpPaymentExpectedDate.Value = Common.FormatDate(invPurchaseOrderHeader.PaymentExpectedDate);

                    txtDocumentNo.Text = invPurchaseOrderHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.NetAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.OtherCharges);

                    txtReferenceNo.Text = invPurchaseOrderHeader.ReferenceNo;
                    txtRemark.Text = invPurchaseOrderHeader.Remark;
                    supplier = supplierService.GetSupplierByID(invPurchaseOrderHeader.SupplierID);
                    txtSupplierCode.Text = supplier.SupplierCode;
                    txtSupplierName.Text = supplier.SupplierName;

                    txtValidityPeriod.Text = invPurchaseOrderHeader.ValidityPeriod.ToString();

                    if (invPurchaseOrderHeader.IsConsignmentBasis) { chkConsignmentBasis.Checked = true; }
                    else { chkConsignmentBasis.Checked = false; }

                    if (!invPurchaseOrderHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invPurchaseOrderHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    invPurchaseOrderDetailsTempList = invPurchaseOrderService.GetPausedPurchaseOrderDetail(invPurchaseOrderHeader);
                    dgvItemDetails.DataSource = invPurchaseOrderDetailsTempList;
                    dgvItemDetails.Refresh();

                    //Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo, txtReferenceNo);
                    Common.EnableTextBox(false, txtSupplierCode, txtSupplierName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);
                    grpFooter2.Enabled = false;

                    if (invPurchaseOrderHeader.DocumentStatus.Equals(0))
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
            tbMore.SelectedTab = tbpSales;
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
                else
                    existingSupplier = supplierService.GetSupplierByName(strsupplier);

                if (existingSupplier != null)
                {
                    txtSupplierCode.Text = existingSupplier.SupplierCode;
                    txtSupplierName.Text = existingSupplier.SupplierName;
                    cmbPaymentTerms.SelectedValue = existingSupplier.PaymentMethod;
                    chkTStatus.Checked = existingSupplier.IsUpload;
                    if (existingSupplier.IsUpload == true) { Common.EnableCheckBox(false, chkTStatus); } else { Common.EnableCheckBox(true, chkTStatus); }
                    
                    txtRemark.Focus();
                    txtOrderCircle.Text = existingSupplier.OrderCircle.ToString();
                    lblVatNo.Text = existingSupplier.TaxNo3.Trim();

                    if (existingSupplier.TaxID1 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID2 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID3 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID4 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingSupplier.TaxID5 != 0) { chkTaxEnable.Checked = true; return; }
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

        private decimal GettotalQty(List<InvPurchaseOrderDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.OrderQty);
        }

        #region SummarizingFigures
        /// <summary>
        /// Update Tax, Discount, Gross amount and Net amount
        /// Note: Read through refreshed List
        /// </summary>
        /// <param name="listItem"></param>
        private void GetSummarizeFigures(List<InvPurchaseOrderDetailTemp> listItem)
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
            dtpPurchaseFrom.Value = DateTime.Now;
            dtpPurchaseTo.Value = DateTime.Now;
            dtpSalesFrom.Value = DateTime.Now;
            dtpSalesTo.Value = DateTime.Now;
            dtpPaymentExpectedDate.Value = DateTime.Now;
        }

        private void RefreshDocumentNumbers()
        {
            ////Load PO Document Numbers
            InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
            Common.SetAutoComplete(txtDocumentNo, invPurchaseOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationPoNo.Checked);
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
                        //txtProductDiscountPer.Enabled = true;
                        //txtProductDiscountPer.Focus();
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
            if (Common.ConvertStringToDecimalCurrency(txtRate.Text) > 0)
            {
                CalculateLine();
            }
            else
            {
                Toast.Show("Amount", Toast.messageType.Information, Toast.messageAction.ZeroAmount);

                txtRate.Focus();
                txtRate.SelectAll();
                return;
            }
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

        private void DisplayStockDetails()
        {
            if (chkViewStokDetails.Checked == false) { dgvStockDetails.DataSource = null; }
        
        }

        private void chkViewStokDetails_CheckedChanged(object sender, EventArgs e)
        {
            DisplayStockDetails();
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
