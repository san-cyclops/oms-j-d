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
using Utility;
using Service;
using Report.Inventory;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmSalesOrder : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        List<SalesOrderDetailTemp> salesOrderDetailTempList = new List<SalesOrderDetailTemp>();
        SalesOrderDetailTemp existingSalesOrderDetailTemp = new SalesOrderDetailTemp();
        InvProductMaster existingInvProductMaster;
        int documentID = 0;
        int documentState;

        int rowCount;
        int selectedRowIndex;
        bool isUpdateGrid = false;
        string[] productCodes;
        List<string> productCodeslst = new List<string>();

        public FrmSalesOrder()
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

        private void FrmSalesOrder_Load(object sender, EventArgs e)
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

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

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

                txtSalesPersonName.Focus();
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

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

                txtRemark.Focus();
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
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtReferenceNo.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpSalesOrderDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                dtpDeliver.Focus();
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
                { cmbLocation_Validated(this, e);}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkOverwrite_KeyDown(object sender, KeyEventArgs e)
        {

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
                { RecallDocument(txtDocumentNo.Text.Trim()); }
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

                dtpSalesOrderDate.Focus();
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
                LocationService locationService = new LocationService();
                //accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, accessLocation.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, documentID);
                //if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                if (cmbLocation.SelectedValue == null || locationService.IsExistsLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString().Trim())).Equals(false))
                {
                    Toast.Show("Location - " + cmbLocation.Text.Trim().Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    Common.EnableTextBox(true, txtProductCode, txtProductName);
                    //LoadProducts();
                    CmbUnit1.Focus();
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
                //loadProductDetails(true, txtProductCode.Text.Trim(), 0);
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
                        txtSize.Enabled = true;
                        txtSize.Focus();
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
                //loadProductDetails(false, txtProductName.Text.Trim(), 0);
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
                //loadProductDetails(true, txtProductCode.Text.Trim(), Common.ConvertStringToLong(cmbUnit.SelectedValue.ToString()));

                txtQty.Enabled = true;
                if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) == 0)
                {txtQty.Text = "1";}
                txtQty.Focus();
                txtQty.SelectAll();
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

        private void txtProductDiscountPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    txtProductDiscountAmount.Enabled = true;
                    txtProductDiscountAmount.Focus();
                    txtProductDiscountAmount.SelectAll();
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

        private void loadProductCodeAutoComplete()
        {
            try
            {
                productCodeslst.Add(txtProductCode.Text.Trim());

                Common.SetAutoComplete(txtProductCode, productCodeslst.ToArray(), true);
 

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtProductAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {UpdateGrid(existingSalesOrderDetailTemp);}
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
                        txtQty_Leave(this, e);
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    return;
                }
                else
                {
                    decimal strQty = Convert.ToDecimal(txtQty.Text.Trim());
                    int qty = (int)strQty;
                    if (qty > 0)
                    {
                        txtRate.Enabled = true;
                        txtRate.Focus();
                        txtRate.SelectAll();
                    }
                    else
                    {
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

        private void chkAutoCompleationCustomer_CheckedChanged(object sender, EventArgs e)
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

        private void chkAutoCompleationSalesOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SalesOrderService salesOrderService = new SalesOrderService();
                Common.SetAutoComplete(txtDocumentNo, salesOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSalesOrderNo.Checked);
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

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadCustomer(false, txtCustomerName.Text.Trim());
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
                { LoadSalesPerson(false, txtSalesPersonName.Text.Trim()); }
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
                    selectedRowIndex = dgvItemDetails.CurrentCell.RowIndex;
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    loadProductDetails(true, dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString().Trim());
                    CalculateLine();
                    EnableProductDetails(true);
                    cmbUnit.Enabled = false;

                    if (documentState.Equals(1))
                    {
                        EnableProductDetails(false);
                        EnableLine(false);
                    }
                    else
                    {
                        EnableProductDetails(true);
                        EnableLine(true);
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
                try
                {
                    int currentRow = dgvItemDetails.CurrentCell.RowIndex;
                    if (dgvItemDetails.CurrentCell != null && dgvItemDetails.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Product " + dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + " - " + dgvItemDetails["ProductName", dgvItemDetails.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            return;

                        SalesOrderDetailTemp SalesOrderTempDetail = new SalesOrderDetailTemp();
                        SalesOrderService salesOrderService = new SalesOrderService();

                        string productcode = dgvItemDetails["ProductCode", dgvItemDetails.CurrentCell.RowIndex].Value.ToString();

                        dgvItemDetails.DataSource = null;
                        salesOrderDetailTempList = salesOrderService.GetDeleteSalesOrderDetailTemp(salesOrderDetailTempList, productcode);
                        dgvItemDetails.DataSource = salesOrderDetailTempList;
                        dgvItemDetails.Refresh();

                        if (currentRow == 0) { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow].Cells[0]; }
                        else { dgvItemDetails.CurrentCell = dgvItemDetails.Rows[currentRow - 1].Cells[0]; }

                        GetSummarizeFigures(salesOrderDetailTempList);
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
                Common.EnableTextBox(true, txtProductCode, txtProductName);
                Common.EnableTextBox(true, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);
                salesOrderDetailTempList = null;
                this.ActiveControl = txtCustomerCode;
                txtCustomerCode.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                selectedRowIndex = 0;
                isUpdateGrid = false;
                rowCount = 0;

                txtDocumentNo.Text = GetDocumentNo(true,cmbCompanyLocation.Text.Trim());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private string GetDocumentNo(bool isTemporytNo,string type)
        {
            try
            {
                SalesOrderService salesOrderService = new SalesOrderService();
                LocationService locationService = new LocationService();
                if (type != "POLY-PACKAGING")
                {
                    return salesOrderService.GetDocumentNo(this.Name.ToString() + "V", Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
                }
                else
                {
                    return salesOrderService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
                }
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
             //   InvProductMasterService invProductMasterService = new InvProductMasterService();
             //   Common.SetAutoComplete(txtProductCode, invProductMasterService.GetAllProductCodes(), chkAutoCompleationProduct.Checked);
             //  Common.SetAutoComplete(txtProductName, invProductMasterService.GetAllProductNames(), chkAutoCompleationProduct.Checked);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

                ////Load Sales persons
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                

                this.Text = autoGenerateInfo.FormText;

                //lblDepartment.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmDepartment").FormText + "*";
                //lblCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").FormText + "*";
                //lblSubCategory.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").FormText + "*";
                //lblSubCategory2.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").FormText + "*";

                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                Common.SetAutoComplete(txtDepartmentCode, invDepartmentService.GetInvDepartmentCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentDescription, invDepartmentService.GetInvDepartmentNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCategory").IsDepend), chkAutoCompleationDepartment.Checked);

                InvCategoryService invCategoryService = new Service.InvCategoryService();
                Common.SetAutoComplete(txtCategoryCode, invCategoryService.GetInvCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);
                Common.SetAutoComplete(txtCategoryDescription, invCategoryService.GetInvCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory").IsDepend), chkAutoCompleationCategory.Checked);

                InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
                Common.SetAutoComplete(txtSubCategoryCode, invSubCategoryService.GetInvSubCategoryCodes(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);
                Common.SetAutoComplete(txtSubCategoryDescription, invSubCategoryService.GetInvSubCategoryNames(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSubCategory2").IsDepend), chkAutoCompleationSubCategory.Checked);

                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                Common.SetAutoComplete(txtSubCategory2Code, invSubCategory2Service.GetInvSubCategory2Codes(), chkAutoCompleationSubCategory2.Checked);
                Common.SetAutoComplete(txtSubCategory2Description, invSubCategory2Service.GetInvSubCategory2Names(), chkAutoCompleationSubCategory2.Checked);

                cmbCurrency.SelectedIndex = -1;
                cmbUnit.SelectedIndex = -1;
                CmbUnit1.SelectedIndex = -1;
                cmbCompanyLocation.SelectedIndex = -1;



                chkOverwrite.Checked = autoGenerateInfo.IsOverWriteQty;
                documentID = autoGenerateInfo.DocumentID;
 

                Common.ExchangeRate = autoGenerateInfo.ExchangeRate;

                txtExchangeRate.Text = Common.ConvertDecimalToStringCurrency(autoGenerateInfo.ExchangeRate);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("", Toast.messageType.Information, Toast.messageAction.AccessDenied); return; }

                base.FormLoad();

                ////Load Sales Order Document Numbers
                SalesOrderService salesOrderService = new SalesOrderService();
                Common.SetAutoComplete(txtDocumentNo, salesOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSalesOrderNo.Checked);

                txtExchangeRate.Enabled = false;
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
                    existingCustomer = customerService.GetCustomersByName(strCustomer);

                if (existingCustomer != null)
                {
                    txtCustomerCode.Text = existingCustomer.CustomerCode;
                    txtCustomerName.Text = existingCustomer.CustomerName;
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
                { existingInvSalesPerson = invSalesPersonService.GetInvSalesPersonByName(strsalesPerson); }

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

        private void loadProductDetails(bool isCode, string strProduct)
        {
            try
            {
                existingInvProductMaster = new InvProductMaster();

                if (strProduct.Equals(string.Empty))
                {
                    return;
                }
                else
                {
                    SalesOrderService salesOrderService = new SalesOrderService();
                    if (salesOrderDetailTempList == null)
                        salesOrderDetailTempList = new List<SalesOrderDetailTemp>();
                    existingSalesOrderDetailTemp = salesOrderService.GetSalesOrderDetailTemp(salesOrderDetailTempList, strProduct, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), 1);
                    if (existingSalesOrderDetailTemp != null)
                    {
                        txtProductCode.Text = existingSalesOrderDetailTemp.ProductCode;
                        txtProductName.Text = existingSalesOrderDetailTemp.ProductName;
                        txtQty.Text = Common.ConvertDecimalToStringQty(existingSalesOrderDetailTemp.OrderQty);
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(existingSalesOrderDetailTemp.SellingPrice);
                        txtProductDiscountAmount.Text = Common.ConvertDecimalToStringCurrency(existingSalesOrderDetailTemp.DiscountAmount);
                        txtGauge.Text =existingSalesOrderDetailTemp.Gauge;
                        txtSize.Text =existingSalesOrderDetailTemp.Size;
                        txtProductAmount.Text = Common.ConvertDecimalToStringCurrency(existingSalesOrderDetailTemp.NetAmount);
                        //Common.EnableComboBox(true, cmbUnit);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableLine(bool enable)
        {
            Common.EnableTextBox(enable, txtQty, txtSize, txtRate, txtGauge, txtProductDiscountAmount, txtProductAmount);
            Common.EnableComboBox(enable, cmbUnit);
        }

        private void ClearLine()
        {
            Common.ClearTextBox(txtProductCode, txtProductName, txtQty, txtSize, txtRate, txtGauge, txtProductDiscountAmount, txtProductAmount);
            Common.ClearComboBox(cmbUnit);
            txtProductCode.Focus();
        }

        private void UpdateGrid(SalesOrderDetailTemp salesOrderDetailTemp)
        {
            try
            {
                decimal qty = 0;
                decimal freeQty = 0;

                SalesOrderService salesOrderService = new SalesOrderService();

                dgvItemDetails.DataSource = null;
                salesOrderDetailTempList = salesOrderService.GetUpdateSalesOrderDetailTemp(salesOrderDetailTempList, salesOrderDetailTemp, existingInvProductMaster);
                dgvItemDetails.DataSource = salesOrderDetailTempList;
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

                GetSummarizeFigures(salesOrderDetailTempList);
                EnableLine(false);
                Common.EnableTextBox(false, txtCustomerCode, txtCustomerName);
                Common.EnableComboBox(false, cmbLocation);
                if (accessRights.IsPause == true) Common.EnableButton(true, btnPause);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                ClearLine();
                if (salesOrderDetailTempList.Count > 0)
                { grpFooter.Enabled = true; }

                txtProductCode.Enabled = true;
                txtProductCode.Focus();
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
                SalesOrderService salesOrderService = new SalesOrderService();
                Common.SetAutoComplete(txtDocumentNo, salesOrderService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationSalesOrderNo.Checked);
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
        private void GetSummarizeFigures(List<SalesOrderDetailTemp> listItem)
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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, grossAmount, customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount1.Text = Common.ConvertDecimalToStringCurrency(netAmount);
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
                taxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, grossAmount, customerService.GetCustomersByCode(txtCustomerCode.Text.ToString()).CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());
            }

            //Get Net Amount
            decimal netAmount = Common.ConvertStringToDecimalCurrency((Common.GetTotalAmount(grossAmount, otherChargersAmount, taxAmount) - Common.GetTotalAmount(discountAmount)).ToString());

            //Assign calculated values
            txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(grossAmount);
            txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(otherChargersAmount);
            txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(discountAmount);
            txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(taxAmount);
            txtNetAmount1.Text = Common.ConvertDecimalToStringCurrency(netAmount);
        }

        private void CalculateLine(decimal qty = 0)
        {
            try
            {
                if (qty == 0)
                {
                    if (Common.ConvertStringToDecimalCurrency(txtGauge.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()), Common.ConvertStringToDecimalCurrency(txtGauge.Text.Trim()))).ToString();
                    else
                        txtProductDiscountAmount.Text = Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim()).ToString();

                    txtProductAmount.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) - Common.ConvertStringToDecimalCurrency(txtProductDiscountAmount.Text.Trim())).ToString();
                }
                else
                {
                    if (Common.ConvertStringToDecimalCurrency(txtGauge.Text.Trim()) > 0)
                        txtProductDiscountAmount.Text = (Common.GetDiscountAmount(true, (Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())) * Common.ConvertDecimalToDecimalQty(qty), Common.ConvertStringToDecimalCurrency(txtGauge.Text.Trim()))).ToString();
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
                txtNetAmount1.Text = (Common.ConvertStringToDecimalCurrency(txtQty.Text.Trim()) * Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim())).ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private decimal GetLineDiscountTotal(List<SalesOrderDetailTemp> listItem)
        {
            return listItem.GetSummaryAmount(x => x.DiscountAmount);
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo,txtReferenceNo, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName,txtDepartmentCode,txtCategoryCode,txtSubCategory2Code,txtSubCategoryCode);
        }

        public override void Pause()
        {
            //if (ValidateControls().Equals(false)) { return; }

            //if ((Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            //{
            //    if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
            //    {
            //        Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
            //        GenerateReport(txtDocumentNo.Text.Trim(), 0);
            //        ClearForm();
            //        RefreshDocumentNumber();
            //    }
            //    else
            //    {
            //        Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
            //        return;
            //    }
            //}
        }

        public override void Save()
        {
            if (ValidateControls().Equals(false)) { return; }

            if ((Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show("Sales Order  - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        private bool SaveDocument(int documentStatus, string documentNo)
        {
            try
            {
                GetSummarizeSubFigures();

                SalesOrderService salesOrderService = new SalesOrderService();
                SalesOrderHeader salesOrderHeader = new SalesOrderHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();
                CustomerService customerService = new CustomerService();
                Customer customer = new Customer();
                InvSalesPerson invSalesPerson = new InvSalesPerson();
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();

                customer = customerService.GetCustomersByCode(txtCustomerCode.Text.Trim());
                invSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(txtSalesPersonCode.Text.Trim());
                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                salesOrderHeader = salesOrderService.GetPausedSalesOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (salesOrderHeader == null)
                {salesOrderHeader = new SalesOrderHeader();}

                if (documentStatus.Equals(1)) // update paused document
                {
                    documentNo = GetDocumentNo(false,cmbCompanyLocation.Text.Trim());
                    txtDocumentNo.Text = documentNo;
                }

                salesOrderHeader.CompanyID = location.CompanyID;
                salesOrderHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscount.Text.ToString());
                if (chkSubTotalDiscountPercentage.Checked)
                {salesOrderHeader.DiscountPercentage = Common.ConvertStringToDecimalCurrency(txtSubTotalDiscountPercentage.Text.ToString());}
                salesOrderHeader.DocumentDate = Common.FormatDateTime(dtpSalesOrderDate.Value);
                
                salesOrderHeader.DocumentID = documentID;
                salesOrderHeader.DocumentStatus = documentStatus;
                salesOrderHeader.DocumentNo = documentNo.Trim();
                salesOrderHeader.GrossAmount = Common.ConvertStringToDecimalCurrency(txtGrossAmount.Text.ToString());
                salesOrderHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                salesOrderHeader.LineDiscountTotal = GetLineDiscountTotal(salesOrderDetailTempList);
                salesOrderHeader.LocationID = location.LocationID;
                salesOrderHeader.CostCentreID = location.CostCentreID;
                salesOrderHeader.NetAmount = Common.ConvertStringToDecimalCurrency(txtNetAmount1.Text.ToString());
                salesOrderHeader.ReferenceNo = txtReferenceNo.Text.Trim();
                salesOrderHeader.Remark = txtRemark.Text.Trim();
                salesOrderHeader.CustomerID = customer.CustomerID;
                salesOrderHeader.InvSalesPersonID = invSalesPerson.InvSalesPersonID;
                salesOrderHeader.DiscountAmount = Common.ConvertStringToDecimalCurrency(txtExchangeRate.Text.ToString());
                salesOrderHeader.ReferenceDocumentNo = "";
                salesOrderHeader.Unit = CmbUnit1.Text.Trim();
                salesOrderHeader.Currency = cmbCurrency.Text.Trim();
                salesOrderHeader.Company = cmbCompanyLocation.Text.Trim();
                salesOrderHeader.Colour = txtNoofColor.Text.Trim();
                salesOrderHeader.DeliverDate = Common.FormatDateTime(dtpDeliver.Value);
                salesOrderHeader.OtherCharges = (chkRupee.Checked ? 1 : 0); 

                InvDepartmentService invDepartmentService = new Service.InvDepartmentService();
                InvCategoryService invCategoryService = new Service.InvCategoryService();
                InvSubCategoryService invSubCategoryService = new Service.InvSubCategoryService();
                InvSubCategory2Service invSubCategory2Service = new Service.InvSubCategory2Service();
                salesOrderHeader.DepartmentID = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), false).InvDepartmentID;
                salesOrderHeader.CategoryID = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), false).InvCategoryID;
                salesOrderHeader.SubCategoryID = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), false).InvSubCategoryID;
                salesOrderHeader.SubCategory2ID = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim()).InvSubCategory2ID;

                if (chkTaxEnable.Checked)
                {
                    decimal tax1 = 0;
                    decimal tax2 = 0;
                    decimal tax3 = 0;
                    decimal tax4 = 0;
                    decimal tax5 = 0;

                    CommonService commonService = new CommonService();
                    salesOrderHeader.TaxAmount = Common.ConvertStringToDecimalCurrency(commonService.CalculateTax(2, salesOrderHeader.GrossAmount, customer.CustomerID, out tax1, out tax2, out tax3, out tax4, out tax5).ToString());

                    salesOrderHeader.TaxAmount1 = tax1;
                    salesOrderHeader.TaxAmount2 = tax2;
                    salesOrderHeader.TaxAmount3 = tax3;
                    salesOrderHeader.TaxAmount4 = tax4;
                    salesOrderHeader.TaxAmount5 = tax5;
                }

                if (salesOrderDetailTempList == null)
                {salesOrderDetailTempList = new List<SalesOrderDetailTemp>();}

                return salesOrderService.Save(salesOrderHeader, salesOrderDetailTempList,Common.ConvertStringToDecimal(txtExchangeRate.Text.Trim()));
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
                SalesOrderService salesOrderService = new SalesOrderService();
                SalesOrderHeader salesOrderHeader = new SalesOrderHeader();

                salesOrderHeader = salesOrderService.GetPausedSalesOrderHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
                if (salesOrderHeader != null)
                {
                    CustomerService customerService = new CustomerService();
                    Customer customer = new Customer();

                    InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                    InvSalesPerson invSalesPerson = new InvSalesPerson();

                    documentState = salesOrderHeader.DocumentStatus;

                    cmbLocation.SelectedValue = salesOrderHeader.LocationID;
                    cmbLocation.Refresh();

                    if (!salesOrderHeader.DiscountPercentage.Equals(0))
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.DiscountPercentage);
                        chkSubTotalDiscountPercentage.Checked = true;
                    }
                    else
                    {
                        txtSubTotalDiscountPercentage.Text = Common.ConvertDecimalToStringCurrency(0);
                        chkSubTotalDiscountPercentage.Checked = false;
                    }

                    txtSubTotalDiscount.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.DiscountAmount);
                    dtpSalesOrderDate.Value = Common.FormatDate(salesOrderHeader.DocumentDate);
                    dtpDeliver.Value = Common.FormatDate(salesOrderHeader.DeliverDate);

                    txtDocumentNo.Text = salesOrderHeader.DocumentNo;
                    txtGrossAmount.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.GrossAmount);
                    txtNetAmount1.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.NetAmount);

                    txtReferenceNo.Text = salesOrderHeader.ReferenceNo;
                    txtRemark.Text = salesOrderHeader.Remark;
                    customer = customerService.GetCustomersById(salesOrderHeader.CustomerID);
                    txtCustomerCode.Text = customer.CustomerCode;
                    txtCustomerName.Text = customer.CustomerName;

                    invSalesPerson = invSalesPersonService.GetInvSalesPersonByID(salesOrderHeader.InvSalesPersonID);
                    txtSalesPersonCode.Text = invSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = invSalesPerson.SalesPersonName;
                    txtOtherCharges.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.OtherCharges);

                    if (!salesOrderHeader.TaxAmount.Equals(0))
                    {
                        chkTaxEnable.Checked = true;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(salesOrderHeader.TaxAmount);
                    }
                    else
                    {
                        chkTaxEnable.Checked = false;
                        txtTotalTaxAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }

                    txtNoofColor.Text = salesOrderHeader.Colour;
                    CmbUnit1.Text = salesOrderHeader.Unit;
                    cmbCompanyLocation.Text = salesOrderHeader.Company;
                    cmbCurrency.Text = salesOrderHeader.Currency;

                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvDepartment invDepartment;
                    InvCategory invCategory;
                    InvSubCategory invSubCategory;
                    InvSubCategory2 invSubCategory2;

                    invDepartment = invDepartmentService.GetInvDepartmentsByID(salesOrderHeader.DepartmentID, false);
                    if (invDepartment != null)
                    {
                        txtDepartmentCode.Text = invDepartment.DepartmentCode;
                        txtDepartmentDescription.Text = invDepartment.DepartmentName;
                    }
                    else
                    {
                        Common.ClearTextBox(txtDepartmentCode, txtDepartmentDescription);
                        Toast.Show(lblDepartment.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    invCategory = invCategoryService.GetInvCategoryByID(salesOrderHeader.CategoryID, false);
                    if (invCategory != null)
                    {
                        txtCategoryCode.Text = invCategory.CategoryCode;
                        txtCategoryDescription.Text = invCategory.CategoryName;
                    }
                    else
                    {
                        Common.ClearTextBox(txtCategoryCode, txtCategoryDescription);
                        Toast.Show(lblCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    invSubCategory = invSubCategoryService.GetInvSubCategoryByID(salesOrderHeader.SubCategoryID, false);
                    if (invSubCategory != null)
                    {
                        txtSubCategoryCode.Text = invSubCategory.SubCategoryCode;
                        txtSubCategoryDescription.Text = invSubCategory.SubCategoryName;
                    }
                    else
                    {
                        Common.ClearTextBox(txtSubCategoryCode, txtSubCategoryDescription);
                        Toast.Show(lblSubCategory.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    invSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByID(salesOrderHeader.SubCategory2ID);
                    if (invSubCategory2 != null)
                    {
                        txtSubCategory2Code.Text = invSubCategory2.SubCategory2Code;
                        txtSubCategory2Description.Text = invSubCategory2.SubCategory2Name;
                    }
                    else
                    {
                        Common.ClearTextBox(txtSubCategory2Code, txtSubCategory2Description);
                        Toast.Show(lblSubCategory2.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    dgvItemDetails.DataSource = null;
                    salesOrderDetailTempList = salesOrderService.GetPausedSalesOrderDetail(salesOrderHeader);
                    dgvItemDetails.DataSource = salesOrderDetailTempList;
                    dgvItemDetails.Refresh();

                    Common.EnableTextBox(false, txtCustomerCode, txtCustomerName, txtSalesPersonCode, txtSalesPersonName, txtDocumentNo);
                    Common.EnableComboBox(false, cmbLocation);

                    if (salesOrderHeader.DocumentStatus.Equals(0))
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

        private void EnableProductDetails(bool state)
        {
            txtProductCode.Enabled = state;
            txtProductName.Enabled = state;
        }

        public override void ClearForm()
        {
            errorProvider.Clear();
            dtpSalesOrderDate.Value = DateTime.Now;
            dgvItemDetails.DataSource = null;
            dgvItemDetails.Refresh();
            chkRupee.Checked =false;
            base.ClearForm();
            FormLoad();
        }

        

        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport(string documentNo, int documentStatus)
        {
            try
            {
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                invReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        #endregion

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (Common.ConvertStringToDecimal(txtQty.Text.Trim()) > 0)
                    {
                        txtGauge.Enabled = true;
                        txtGauge.Focus();
                        txtGauge.SelectAll();
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

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtSize.Text.Trim().Equals(string.Empty))
                    {
                        txtGauge.Enabled = true;
                        txtGauge.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGauge_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtGauge.Text.Trim().Equals(string.Empty))
                    {
                        txtQty.Enabled = true;
                        txtQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartmentCode.Text.Trim() != string.Empty)
                {
                    InvDepartmentService invDepartmentService = new InvDepartmentService();
                    InvDepartment existingInvDepartment = new InvDepartment();
                    existingInvDepartment = invDepartmentService.GetInvDepartmentsByCode(txtDepartmentCode.Text.Trim(), false);

                    if (existingInvDepartment != null)
                    {
                        txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                        txtDepartmentDescription.Text = existingInvDepartment.DepartmentName;
                    }
                    else
                    {
                        Toast.Show(lblDepartment.Text + " " + txtDepartmentCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtDepartmentCode);
                        txtDepartmentCode.Focus();
                
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryCode.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByCode(txtCategoryCode.Text.Trim(), false);

                    if (existingInvCategory != null)
                    {
                        if (!false)
                        {
                            txtCategoryDescription.Text = existingInvCategory.CategoryName;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, false).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryDescription.Text = existingInvCategory.CategoryName;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryCode);
                                txtCategoryCode.Focus();
 
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryCode);
                        txtCategoryCode.Focus();
 
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentCode_Leave(this, e);
                    txtCategoryCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategoryCode.Text.Trim() != string.Empty)
                {
                    InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                    InvSubCategory existingInvSubCategory = new InvSubCategory();

                    existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByCode(txtSubCategoryCode.Text.Trim(), false);

                    if (existingInvSubCategory != null)
                    {
                        if (!false)
                        {
                            txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            InvCategoryService invCategoryService = new Service.InvCategoryService();

                            if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, false).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                            {
                                txtSubCategoryDescription.Text = existingInvSubCategory.SubCategoryName;
                                txtSubCategory2Code.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategoryCode);
                                txtSubCategoryCode.Focus();
 
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory.Text + " " + txtSubCategoryCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategoryCode);
                        txtSubCategoryCode.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Code.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByCode(txtSubCategory2Code.Text.Trim());

                    if (existingInvSubCategory2 != null)
                    {
                        if (!false)
                        {
                            txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, false).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Description.Text = existingInvSubCategory2.SubCategory2Name;
                                txtProductCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Code);
                                txtSubCategory2Code.Focus();
 
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Code.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Code);
                        txtSubCategory2Code.Focus();
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Code_Leave(this, e);
                    txtProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryCode_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentDescription_Leave(object sender, EventArgs e)
        {
            if (txtDepartmentDescription.Text.Trim() != string.Empty)
            {
                InvDepartmentService invDepartmentService = new InvDepartmentService();
                InvDepartment existingInvDepartment = new InvDepartment();
                existingInvDepartment = invDepartmentService.GetInvDepartmentsByName(txtDepartmentDescription.Text.Trim(), false);

                if (existingInvDepartment != null)
                {
                    txtDepartmentCode.Text = existingInvDepartment.DepartmentCode;
                    txtCategoryCode.Focus();
                }
                else
                {
                    Toast.Show(lblDepartment.Text + " " + txtDepartmentDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtDepartmentDescription);
                    txtDepartmentDescription.Focus();
                }
            }
        }

        private void txtDepartmentDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtDepartmentDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryDescription.Text.Trim() != string.Empty)
                {
                    InvCategoryService invCategoryService = new InvCategoryService();
                    InvCategory existingInvCategory = new InvCategory();

                    existingInvCategory = invCategoryService.GetInvCategoryByName(txtCategoryDescription.Text.Trim(), false);


                    if (existingInvCategory != null)
                    {
                        if (!false)
                        {
                            txtCategoryCode.Text = existingInvCategory.CategoryCode;
                            txtSubCategoryCode.Focus();
                        }
                        else
                        {
                            InvDepartmentService invDepartmentService = new Service.InvDepartmentService();

                            if (invDepartmentService.GetInvDepartmentsByID(existingInvCategory.InvDepartmentID, false).DepartmentCode.Equals(txtDepartmentCode.Text.Trim()))
                            {
                                txtCategoryCode.Text = existingInvCategory.CategoryCode;
                                txtSubCategoryCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblDepartment.Text + " - " + txtDepartmentCode.Text.Trim() + " - " + txtDepartmentDescription.Text.Trim());
                                Common.ClearTextBox(txtCategoryDescription);
                                txtCategoryDescription.Focus();
                            }
                        }
                    }
                    else
                    {
                        Toast.Show(lblCategory.Text + " " + txtCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtCategoryDescription);
                        txtCategoryDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDescription_Leave(object sender, EventArgs e)
        {
            if (txtSubCategoryDescription.Text.Trim() != string.Empty)
            {
                InvSubCategoryService invSubCategoryService = new InvSubCategoryService();
                InvSubCategory existingInvSubCategory = new InvSubCategory();

                existingInvSubCategory = invSubCategoryService.GetInvSubCategoryByName(txtSubCategoryDescription.Text.Trim(), false);

                if (existingInvSubCategory != null)
                {
                    if (!false)
                    {
                        txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                        txtSubCategory2Code.Focus();
                    }
                    else
                    {
                        InvCategoryService invCategoryService = new Service.InvCategoryService();

                        if (invCategoryService.GetInvCategoryByID(existingInvSubCategory.InvCategoryID, false).CategoryCode.Equals(txtCategoryCode.Text.Trim()))
                        {
                            txtSubCategoryCode.Text = existingInvSubCategory.SubCategoryCode;
                            txtSubCategory2Code.Focus();
                        }
                        else
                        {
                            Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblCategory.Text + " - " + txtCategoryCode.Text.Trim() + " - " + txtCategoryDescription.Text.Trim());
                            Common.ClearTextBox(txtSubCategoryDescription);
                            txtSubCategoryDescription.Focus();
                        }
                    }
                }
                else
                {
                    Toast.Show(lblSubCategory.Text + " " + txtSubCategoryDescription.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    Common.ClearTextBox(txtSubCategoryDescription);
                    txtSubCategoryDescription.Focus();
                }
            }
        }

        private void txtSubCategory2Description_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory2Description.Text.Trim() != string.Empty)
                {
                    InvSubCategory2Service invSubCategory2Service = new InvSubCategory2Service();
                    InvSubCategory2 existingInvSubCategory2 = new InvSubCategory2();

                    existingInvSubCategory2 = invSubCategory2Service.GetInvSubCategory2ByName(txtSubCategory2Description.Text.Trim());


                    if (existingInvSubCategory2 != null)
                    {
                        if (!false)
                        {
                            txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                            txtProductCode.Focus();
                        }
                        else
                        {
                            InvSubCategoryService InvSubCategoryService = new Service.InvSubCategoryService();

                            if (InvSubCategoryService.GetInvSubCategoryByID(existingInvSubCategory2.InvSubCategoryID, false).SubCategoryCode.Equals(txtSubCategoryCode.Text.Trim()))
                            {
                                txtSubCategory2Code.Text = existingInvSubCategory2.SubCategory2Code;
                                txtProductCode.Focus();
                            }
                            else
                            {
                                Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, lblSubCategory.Text + " - " + txtSubCategoryCode.Text.Trim() + " - " + txtSubCategoryDescription.Text.Trim());
                                Common.ClearTextBox(txtSubCategory2Description);
                                txtSubCategory2Description.Focus();
                            }
                        }

                    }
                    else
                    {

                        Toast.Show(lblSubCategory2.Text + " " + txtSubCategory2Description.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                        Common.ClearTextBox(txtSubCategory2Description);
                        txtSubCategory2Description.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategory2Description_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategory2Description_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSubCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtSubCategoryDescription_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dtpDeliver_KeyDown(object sender, KeyEventArgs e)
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

        private void CmbUnit1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                cmbCurrency.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                cmbCompanyLocation.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbCompanyLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                txtNoofColor.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNoofColor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                txtDepartmentCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {

            if (!e.KeyCode.Equals(Keys.Enter))
            { return; }
            else
            {
                if (txtProductCode.Text != string.Empty && txtProductCode.Text != string.Empty && txtQty.Text != string.Empty)
                {
                    if (chkRupee.Checked == true)
                    {
                        AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                        autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                        this.Text = autoGenerateInfo.FormText;

                        Common.ExchangeRate = autoGenerateInfo.ExchangeRate;
                        txtRate.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtRate.Text.Trim()) / Common.ExchangeRate);
                    }
                    loadProductCodeAutoComplete();
                    CalculateLine();
                    existingSalesOrderDetailTemp = new SalesOrderDetailTemp();
                    existingSalesOrderDetailTemp.SellingPrice = Common.ConvertStringToDecimal(txtRate.Text.Trim());
                    existingSalesOrderDetailTemp.NetAmount = Common.ConvertStringToDecimal(txtNetAmount1.Text.Trim());
                    existingSalesOrderDetailTemp.ProductCode = txtProductCode.Text.Trim();
                    existingSalesOrderDetailTemp.ProductName = txtProductName.Text.Trim();
                    existingSalesOrderDetailTemp.OrderQty = Common.ConvertStringToDecimal(txtQty.Text.Trim());
                    existingSalesOrderDetailTemp.Size = txtSize.Text.Trim();
                    existingSalesOrderDetailTemp.Gauge = txtGauge.Text.Trim();


                    UpdateGrid(existingSalesOrderDetailTemp);
                    txtProductCode.Enabled = true;
                    txtProductCode.Focus();
                    txtProductCode.SelectAll();
                }
                else
                {
                    MessageBox.Show("Invalid Entry... Please Enter Product Code and Product Name...");
                    txtProductCode.Focus();
                    txtProductCode.SelectAll();
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (txtDocumentNo.Text != string.Empty)
            {
                GenerateReport(txtDocumentNo.Text.Trim(), 1);
                ClearForm();
                RefreshDocumentNumber();
            }
        }

        private void dgvItemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        { 
            try
            {
                 if (txtDocumentNo.Text != string.Empty)
                    {
               
                            if (Toast.Show("" + this.Text + " - "  + txtDocumentNo.Text , Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                            { return; }

                                SalesOrderService  salesOrderService = new SalesOrderService();
                                salesOrderService.DeleteSalesOrder(txtDocumentNo.Text.Trim());
                                 
                                Toast.Show("" + this.Text + " - " + txtDocumentNo.Text , Toast.messageType.Information, Toast.messageAction.Delete);
                                ClearForm();
                                txtCategoryCode.Focus();
 
                     }
            }
 
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
    }
}
