using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Domain;
using Report.Inventory;
using Utility;
using Service;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmQuotation : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<InvQuotationDetailTemp> invQuotationDetailsTempList = new List<InvQuotationDetailTemp>();
        InvQuotationDetailTemp existingInvQuotationDetailTemp = new InvQuotationDetailTemp();
        InvProductMaster existingInvProductMaster;
        bool loadSupplierProducts = true; // Remove this variable after getting values from System configuration
        int documentID = 0;
        int documentState;
        bool isSupplierProduct;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;

        public FrmQuotation()
        {
            InitializeComponent();
        }

        #region Form Events
        private void btnTaxBreakdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerCode.Text.Trim() != "" && chkTaxEnable.Checked == true)
                {
                    CustomerService customerService = new CustomerService();
                    FrmTaxBreakdown frmTaxBreakdown = new FrmTaxBreakdown(customerService.GetCustomersByCode(txtCustomerCode.Text.Trim()).CustomerID, 2, Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.Trim()));
                    frmTaxBreakdown.ShowDialog();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void FrmQuotation_Load(object sender, EventArgs e)
        {

        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtCustomerName.Focus(); }

                if (e.KeyCode.Equals(Keys.F3))
                {
                    CustomerService customerService = new CustomerService();
                    DataView dvAllReferenceData = new DataView(customerService.GetAllActiveCustomersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                        txtCustomerCode_Leave(this, e);
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
                    CustomerService customerService = new CustomerService();
                    DataView dvAllReferenceData = new DataView(customerService.GetAllActiveCustomersDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCustomerCode);
                        txtCustomerCode_Leave(this, e);
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
                {cmbLocation_Validated(this, e);}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtVendorName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomerName.Text.Trim())) { return; }
                LoadCustomer(false, txtCustomerName.Text.Trim());
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                {RecallDocument(txtDocumentNo.Text.Trim());}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                CustomerService customerService = new CustomerService();
                InvSalesPersonService salesPersonService = new InvSalesPersonService();

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }
                
                if (customerService.IsExistsCustomer(txtCustomerCode.Text.Trim()).Equals(false))
                {
                    Toast.Show("Customer - " + txtCustomerCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    txtCustomerCode.Focus();
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
                        Common.EnableTextBox(true, txtProductCode, txtProductName);
                        LoadProducts();

                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtDocumentNo.Enabled = false;

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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedValue == null)
                {return;}

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

                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                {txtQty.Text = "1";}
                txtQty.Focus();
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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
                decimal productAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text.Trim());
                decimal discountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim());
                if (discountAmount > productAmount)
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                CalculateLine();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { UpdateGrid(existingInvQuotationDetailTemp); }
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
                CalculateLine();
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

        private void chkAutoCompleationVendor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);
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
                InvQuotationService invQuotationService = new InvQuotationService();
                Common.SetAutoComplete(txtDocumentNo, invQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);
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

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtCustomerCode.Text.Trim().Equals(string.Empty))
                { LoadCustomer(true, txtCustomerCode.Text.Trim()); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSalesPersonCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonCode.Text.Trim().Equals(string.Empty))
                { LoadSalesPerson(true, txtSalesPersonCode.Text.Trim()); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSalesPersonName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonName.Text.Trim().Equals(string.Empty))
                { LoadSalesPerson(true, txtSalesPersonCode.Text.Trim()); }
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

        private void dgvItemDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                isUpdateGrid = true;
                if (dgvItemDetails.CurrentCell.RowIndex >= 0)
                {
                    if (dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value == null)
                    {
                        Toast.Show("No data available to display.", Toast.messageType.Information, Toast.messageAction.General, "");
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

                        InvQuotationDetailTemp invQuotationTempDetail = new InvQuotationDetailTemp();
                        UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                        InvProductMasterService invProductMasterService = new InvProductMasterService();

                        invQuotationTempDetail.ProductID = invProductMasterService.GetProductsByRefCodes(dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim()).InvProductMasterID;
                        invQuotationTempDetail.UnitOfMeasureID = unitOfMeasureService.GetUnitOfMeasureByName(dgvItemDetails["Unit", dgvItemDetails.CurrentCell.RowIndex].Value.ToString()).UnitOfMeasureID;

                        InvQuotationService invQuotationService = new InvQuotationService();

                        dgvItemDetails.DataSource = null;
                        invQuotationDetailsTempList = invQuotationService.GetDeleteInvQuotationDetailTemp(invQuotationDetailsTempList, invQuotationTempDetail);
                        dgvItemDetails.DataSource = invQuotationDetailsTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(invQuotationDetailsTempList);
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
                if (chkSubTotalDiscountPercentage.Checked)
                {
                    decimal value = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.Trim());
                    if (value >= 101)
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.SubTotalDiscountPercentageExceed);
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
        #endregion

        #region Methods.....

        public override void InitializeForm()
        {
            try
            {
                // Disable product details controls
                grpFooter.Enabled = false;
                EnableLine(false);
                EnableProductDetails(false);
                Common.EnableTextBox(false, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation, cmbPaymentTerms);
                Common.EnableButton(false, btnSave, btnPause);
                invQuotationDetailsTempList = null;
                this.ActiveControl = txtCustomerCode;
                txtCustomerCode.Focus();
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

        private void UpdateGrid(InvQuotationDetailTemp invQuotationDetailTemp)
        {
            try
            {
                decimal qty = 0;

                if (((Common.ConvertStringToDecimalQty(txtQty.Text.Trim())) > 0) && (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) > 0))
                {

                    invQuotationDetailTemp.UnitOfMeasureID = Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString());
                    invQuotationDetailTemp.BaseUnitID = existingInvProductMaster.UnitOfMeasureID;
                    invQuotationDetailTemp.UnitOfMeasure = cmbUnit.Text.ToString().Trim();

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
                        qty = (invQuotationDetailTemp.OrderQty + Common.ConvertStringToDecimalQty(txtQty.Text));
                    }

                    if (!chkOverwrite.Checked)
                    {
                        CalculateLine(qty);
                        invQuotationDetailTemp.OrderQty = Common.ConvertDecimalToDecimalQty(qty);
                        invQuotationDetailTemp.GrossAmount = (Common.ConvertDecimalToDecimalQty(qty) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }
                    else
                    {
                        CalculateLine();
                        invQuotationDetailTemp.OrderQty = Common.ConvertStringToDecimalQty(txtQty.Text);
                        invQuotationDetailTemp.GrossAmount = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()));
                    }

                    invQuotationDetailTemp.CostPrice = Common.ConvertStringToDecimalCurrency(txtRate.Text);
                    invQuotationDetailTemp.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim());
                    invQuotationDetailTemp.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text);
                    invQuotationDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(txtProductAmount.Text);
                    
                    InvQuotationService invQuotationService = new InvQuotationService();

                    dgvItemDetails.DataSource = null;
                    invQuotationDetailsTempList = invQuotationService.GetUpdateQuotationDetailTemp(invQuotationDetailsTempList, invQuotationDetailTemp, existingInvProductMaster);
                    dgvItemDetails.DataSource = invQuotationDetailsTempList;
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

                    GetSummarizeFigures(invQuotationDetailsTempList);
                    EnableLine(false);
                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName);
                    Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);
                    if (accessRights.IsPause == true) {Common.EnableButton(true, btnPause);}
                    if (accessRights.IsSave == true) {Common.EnableButton(true, btnSave);}
                    ClearLine();
                    if (invQuotationDetailsTempList.Count > 0)
                    {grpFooter.Enabled = true;}

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

        private void loadProductDetails(bool isCode, string strProduct, long unitofMeasureID)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                {return;}

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
                    InvQuotationService invQuotationService = new InvQuotationService();
                    if (invQuotationDetailsTempList == null)
                    {invQuotationDetailsTempList = new List<InvQuotationDetailTemp>();}
                    existingInvQuotationDetailTemp = invQuotationService.GetQuotationDetailTemp(invQuotationDetailsTempList, existingInvProductMaster, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), unitofMeasureID);
                    if (existingInvQuotationDetailTemp != null)
                    {
                        txtProductCode.Text = existingInvQuotationDetailTemp.ProductCode;
                        txtProductName.Text = existingInvQuotationDetailTemp.ProductName;
                        cmbUnit.SelectedValue = existingInvQuotationDetailTemp.UnitOfMeasureID;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingInvQuotationDetailTemp.OrderQty);
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingInvQuotationDetailTemp.CostPrice);
                        txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingInvQuotationDetailTemp.DiscountAmount);
                        txtProductDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(existingInvQuotationDetailTemp.DiscountPercentage);
                        Common.EnableComboBox(true, cmbUnit);
                        if (unitofMeasureID.Equals(0))
                        {cmbUnit.Focus();}
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
            Common.EnableTextBox(enable, txtQty, txtProductDiscountPercentage, txtProductDiscountAmount, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtRate, txtProductDiscountPercentage, txtProductDiscountAmount, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                InvQuotationService invQuotationService = new InvQuotationService();
                LocationService locationService = new LocationService();
                return invQuotationService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
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
                Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
                Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
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
                dgvItemDetails.AutoGenerateColumns = false;

                // Load Unit of measures
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.LoadUnitOfMeasures(cmbUnit, unitOfMeasureService.GetAllUnitOfMeasures());

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                // Load Customers
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

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
                this.Text = autoGenerateInfo.FormText;

                isSupplierProduct = autoGenerateInfo.IsSupplierProduct;
                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                base.FormLoad();

                ////Load Quotation Document Numbers
                InvQuotationService invQuotationService = new InvQuotationService();
                Common.SetAutoComplete(txtDocumentNo, invQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void LoadCustomer(bool isCode, string strCustomer)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Customer existingCustomer = new Customer();

                if (isCode)
                {
                    existingCustomer = customerService.GetCustomersByCode(strCustomer);
                    if (isCode && strCustomer.Equals(string.Empty))
                    {
                        txtCustomerCode.Focus();
                        return;
                    }
                }
                else
                {existingCustomer = customerService.GetCustomersByName(strCustomer);}

                if (existingCustomer != null)
                {
                    txtCustomerCode.Text = existingCustomer.CustomerCode;
                    txtCustomerName.Text = existingCustomer.CustomerName;

                    if (existingCustomer.TaxID1 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingCustomer.TaxID2 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingCustomer.TaxID3 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingCustomer.TaxID4 != 0) { chkTaxEnable.Checked = true; return; }
                    else if (existingCustomer.TaxID5 != 0) { chkTaxEnable.Checked = true; return; }

                    txtSalesPersonCode.Focus();
                }
                else
                {
                    Toast.Show("Customer - " + strCustomer.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
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

        /// <summary>
        /// Refresh paused document
        /// </summary>
        private void RefreshDocumentNumber() 
        {
            try
            {
                ////Load Quotation Document Numbers
                InvQuotationService invQuotationService = new InvQuotationService();
                Common.SetAutoComplete(txtDocumentNo, invQuotationService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationQuotationNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #region SummarizingFigures


        /// <summary>
        /// Update Tax, Discount, Gross amount and Net amount
        /// Note: Read through refreshed List
        /// </summary>
        /// <param name="listItem"></param>
        private void GetSummarizeFigures(List<InvQuotationDetailTemp> listItem)
        {
            CommonService commonService = new CommonService();
            CustomerService customerService = new CustomerService();

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, (grossAmount - discountAmount), customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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
            CustomerService customerService = new CustomerService();

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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, (grossAmount - discountAmount), customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
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

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
                }
                else
                {
                    if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertDecimalToDecimalQty(qty), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();
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

        private void CalculateLine()
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()) > 0)
                {txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtProductDiscountPercentage.Text.Trim()))).ToString();}
                else
                {txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();}

                txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
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

        

        private decimal GetLineDiscountTotal(List<InvQuotationDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.DiscountAmount);
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName);
        }

        public override void Pause()
        {
            if ((Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        public override void Save()
        {
            if ((Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (ValidateControls().Equals(false)) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Quotation  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                InvQuotationService invQuotationService = new InvQuotationService();
                InvQuotationHeader invQuotationHeader = new InvQuotationHeader();
                LocationService locationService = new LocationService();
                Location Location = new Location();
                CustomerService customerService = new CustomerService();
                Customer customer = new Customer();
                InvSalesPerson invSalesPerson = new InvSalesPerson();
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();

                customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());
                invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());

                Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                invQuotationHeader = invQuotationService.GetPausedQuotationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invQuotationHeader == null)
                {invQuotationHeader = new InvQuotationHeader();}

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false);
                    txtDocumentNo.Text = documentNo;
                }

                invQuotationHeader.CompanyID = Location.CompanyID;
                invQuotationHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                {invQuotationHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());}

                invQuotationHeader.DocumentDate = Common.FormatDateTime(dtpQuotationDate.Value);
                invQuotationHeader.DeliveryDate = Common.FormatDateTime(dtpDeliveryDate.Value);

                invQuotationHeader.DocumentID = documentID;
                invQuotationHeader.DocumentStatus = documentStatus;
                invQuotationHeader.DocumentNo = documentNo.Trim();
                invQuotationHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                invQuotationHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                invQuotationHeader.LineDiscountTotal = GetLineDiscountTotal(invQuotationDetailsTempList);
                invQuotationHeader.LocationID = Location.LocationID;
                invQuotationHeader.CostCentreID = Location.CostCentreID;
                invQuotationHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount.Text.ToString());
                invQuotationHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                invQuotationHeader.OtherCharges = Common.ConvertStringToDecimalCurrency(txtOtherCharges.Text.ToString());
                invQuotationHeader.Remark = txtRemark.Text.Trim();
                invQuotationHeader.CustomerID = customer.CustomerID;
                invQuotationHeader.InvSalesPersonID = invSalesPerson.InvSalesPersonID;
                invQuotationHeader.PaymentMethodID = Common.ConvertStringToInt(cmbPaymentTerms.SelectedValue.ToString());
                invQuotationHeader.ReferenceDocumentNo = "";
                invQuotationHeader.DeliveryDetail = "";

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    invQuotationHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, (invQuotationHeader.GrossAmount - invQuotationHeader.DiscountAmount), customer.CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    invQuotationHeader.TaxAmount1 = tax1;
                    invQuotationHeader.TaxAmount2 = tax2;
                    invQuotationHeader.TaxAmount3 = tax3;
                    invQuotationHeader.TaxAmount4 = tax4;
                    invQuotationHeader.TaxAmount5 = tax5;
                }

                if (invQuotationDetailsTempList == null)
                {invQuotationDetailsTempList = new List<InvQuotationDetailTemp>();}

                return invQuotationService.Save(invQuotationHeader, invQuotationDetailsTempList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return false;
            }
        }

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
                InvQuotationService invQuotationService = new InvQuotationService();
                InvQuotationHeader invQuotationHeader = new InvQuotationHeader();

                invQuotationHeader = invQuotationService.GetPausedQuotationHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (invQuotationHeader != null)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();
                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    documentState = invQuotationHeader.DocumentStatus;
                    cmbLocation.SelectedValue = invQuotationHeader.LocationID;
                    cmbLocation.Refresh();

                    cmbPaymentTerms.SelectedValue = invQuotationHeader.PaymentMethodID;
                    cmbPaymentTerms.Refresh();

                    if (!invQuotationHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.DiscountAmount);
                    dtpQuotationDate.Value = Common.FormatDate(invQuotationHeader.DocumentDate);
                    dtpDeliveryDate.Value = Common.FormatDate(invQuotationHeader.DeliveryDate);

                    txtDocumentNo.Text = invQuotationHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.GrossAmount);
                    txtNetAmount.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.NetAmount);
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.OtherCharges);
                    txtReferenceNo.Text = invQuotationHeader.ReferenceNo;
                    txtRemark.Text = invQuotationHeader.Remark;
                    
                    customer = customerService.GetCustomersById(invQuotationHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(invQuotationHeader.InvSalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;

                    if (!invQuotationHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(invQuotationHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    dgvItemDetails.DataSource = null;
                    invQuotationDetailsTempList = invQuotationService.GetPausedQuotationDetail(invQuotationHeader);
                    dgvItemDetails.DataSource = invQuotationDetailsTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation, cmbPaymentTerms);

                    if (invQuotationHeader.DocumentStatus.Equals(0))
                    {
                        grpFooter.Enabled = true;
                        EnableLine(false);
                        EnableProductDetails(true);
                        LoadProducts();
                        if (accessRights.IsPause == true) {Common.EnableButton(true, btnPause);}
                        if (accessRights.IsSave == true) {Common.EnableButton(true, btnSave);}
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
    }
}
