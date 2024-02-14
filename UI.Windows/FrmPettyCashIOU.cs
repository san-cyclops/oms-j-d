using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Account;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmPettyCashIOU : UI.Windows.FrmBaseTransactionForm
    {
        List<AccPettyCashIOUDetail> accPettyCashIOUDetailList = new List<AccPettyCashIOUDetail>();

        private AccLedgerAccount existingAccLedgerAccount;
        private Employee existingEmployee;

        int documentID = 5; 

        public FrmPettyCashIOU()
        {
            InitializeComponent();
        }

        #region Form Events
        private void txtExpenseLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    DataView dvExpense = new DataView(accLedgerAccountService.GetAllActiveExpenseAccountsDataTable());
                    if (dvExpense.Count != 0)
                    {
                        LoadReferenceSearchForm(dvExpense, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtExpenseLedgerCode);
                        txtExpenseLedgerCode_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtExpenseLedgerCode.Text.Trim().Equals(string.Empty))
                    {
                        txtExpenseAmount.Enabled = true;
                        txtExpenseAmount.Focus();
                        txtExpenseAmount.SelectAll();
                    }
                    else
                    {
                        txtExpenseLedgerName.Enabled = true;
                        txtExpenseLedgerName.Focus();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtExpenseLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadExpenseLedgers(true, txtExpenseLedgerCode.Text.Trim());
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtExpenseLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    DataView dvExpense = new DataView(accLedgerAccountService.GetAllActiveExpenseAccountsDataTable());
                    if (dvExpense.Count != 0)
                    {
                        LoadReferenceSearchForm(dvExpense, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtExpenseLedgerName);
                        txtExpenseLedgerName_Leave(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!txtExpenseLedgerName.Text.Trim().Equals(string.Empty))
                    {
                        txtExpenseAmount.Enabled = true;
                        txtExpenseAmount.Focus();
                        txtExpenseAmount.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtExpenseLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadExpenseLedgers(false, txtExpenseLedgerName.Text.Trim());
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtExpenseAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!ValidateControls()) { return; }
                    if(!IsValidateExistsValidReimburseRecord()) {return;}

                    bool isValid = LoadExpenseLedgers(true, txtExpenseLedgerCode.Text.Trim());
                    if (isValid)
                    { AssignExpenseLedgerProperties(); }
                    else
                    { return; }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    EmployeeService employeeService = new EmployeeService();
                    DataView dvEmployee = new DataView(employeeService.GetAllEmployeeDataTable());
                    if (dvEmployee.Count != 0)
                    {
                        LoadReferenceSearchForm(dvEmployee, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeCode);
                        txtEmployeeCode_Validated(this, e);
                    }
                }
                if (txtEmployeeCode.Text != string.Empty)
                { Common.SetFocus(e, txtPayeeName); }
                else
                { Common.SetFocus(e, txtEmployeeName); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();
                    existingEmployee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());
                    if (existingEmployee != null)
                    {
                        txtEmployeeName.Text = existingEmployee.EmployeeName;
                        txtPayeeName.Text = existingEmployee.EmployeeName;
                        CalculateIOUBalance();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblEmployee.Text.Trim()) + " - " + txtEmployeeCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtEmployeeCode.Text = string.Empty;
                        txtEmployeeName.Text = string.Empty;
                        txtEmployeeCode.Focus();
                        return;
                    }
                }
                else
                {
                    txtEmployeeName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    EmployeeService employeeService = new EmployeeService();
                    DataView dvEmployee = new DataView(employeeService.GetAllEmployeeDataTable());
                    if (dvEmployee.Count != 0)
                    {
                        LoadReferenceSearchForm(dvEmployee, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeName);
                        txtEmployeeName_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtEmployeeCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();
                    existingEmployee = employeeService.GetEmployeesByName(txtEmployeeName.Text.Trim());
                    if (existingEmployee != null)
                    {
                        txtEmployeeCode.Text = existingEmployee.EmployeeCode;
                        txtPayeeName.Text = existingEmployee.EmployeeName;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblEmployee.Text.Trim()) + " - " + txtEmployeeName.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtEmployeeCode.Text = string.Empty;
                        txtEmployeeName.Text = string.Empty;
                        txtEmployeeName.Focus();
                        return;
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
            { Common.SetFocus(e, txtPettyCashBookCode); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbLocation.Text.Trim()))
                { return; }

                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    // Read Petty Cash Books
                    AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                    LocationService locationService = new LocationService();

                    Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                    Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);

                    txtPettyCashBookCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();

                    DataView dvPettyCash = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetHeadOfficeLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvPettyCash.Count != 0)
                    {
                        LoadReferenceSearchForm(dvPettyCash, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPettyCashBookCode);
                        txtPettyCashBookCode_Validated(this, e);
                    }
                }
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtPettyCashBookCode.Text != string.Empty)
                    {
                        Common.SetFocus(e, txtReferenceNo);
                    }
                    else
                    {
                        Common.SetFocus(e, txtPettyCashBookName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPettyCashBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtPettyCashBookCode.Text.Equals(string.Empty))
                { return; }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        txtPettyCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        txtPettyCashBookName.Text = existingAccLedgerAccount.LedgerName;

                        txtDocumentNo.Text = GetDocumentNo(true);
                        txtDocumentNo.Enabled = false;

                        LoadPettyCashLimitBalance();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtPettyCashBookCode.Text = string.Empty;
                        txtPettyCashBookName.Text = string.Empty;
                        txtPettyCashBookCode.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();
                    DataView dvPettyCash = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvPettyCash.Count != 0)
                    {
                        LoadReferenceSearchForm(dvPettyCash, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPettyCashBookName);
                        txtPettyCashBookName_Leave(this, e);
                    }
                }
                if (txtPettyCashBookName.Text != string.Empty)
                {
                    Common.SetFocus(e, txtPettyCashBookCode);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPettyCashBookName.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByName(txtPettyCashBookName.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        txtPettyCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        txtPettyCashBookName.Text = existingAccLedgerAccount.LedgerName;
                        LoadPettyCashLimitBalance();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookName.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtPettyCashBookCode.Text = string.Empty;
                        txtPettyCashBookName.Text = string.Empty;
                        txtPettyCashBookName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRemark);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.EnableTextBox(true, txtExpenseLedgerCode, txtExpenseLedgerName);
                Common.SetFocus(e, txtExpenseLedgerCode);
                grpBody.Enabled = true;
                grpFooter.Enabled = true;
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }

        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                else
                {
                    //if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    //    RecallDocument(txtPurchaseOrderNo.Text.Trim());
                    //else
                    //{
                    //    txtPurchaseOrderNo.Text = GetDocumentNo(true);
                    //    txtSupplierCode.Focus();
                    //}

                    txtDocumentNo_Validated(this, e);
                }

                txtEmployeeCode.Focus();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                {
                    txtDocumentNo.Text = GetDocumentNo(true);
                    cmbLocation.Focus();
                }
                else
                {
                    AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                    AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();

                    accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                    if (accPettyCashIOUHeader == null)
                    {
                        return;
                        //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                    else
                    {
                        //LoadPettyCashIOUDocument(accPettyCashIOUHeader);
                        LoadPettyCashIOUDocument();
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvIOUDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvIOUDetail.CurrentCell != null && dgvIOUDetail.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Expenses " + dgvIOUDetail["LedgerName", dgvIOUDetail.CurrentCell.RowIndex].Value.ToString() + " - " + dgvIOUDetail["Amount", dgvIOUDetail.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                        { return; }

                        AccPettyCashIOUDetail accPettyCashIOUDetailRemove = new AccPettyCashIOUDetail();

                        accPettyCashIOUDetailRemove = accPettyCashIOUDetailList.Where(p => p.LedgerID.Equals(Common.ConvertStringToLong(dgvIOUDetail["AccLedgerAccountID", dgvIOUDetail.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();

                        if (accPettyCashIOUDetailRemove != null)
                        { accPettyCashIOUDetailList.Remove(accPettyCashIOUDetailRemove); }

                        dgvIOUDetail.DataSource = null;
                        dgvIOUDetail.DataSource = accPettyCashIOUDetailList;
                        dgvIOUDetail.Refresh();

                        GetSummarizeFigures(accPettyCashIOUDetailList);

                        Common.ClearTextBox(txtExpenseLedgerCode, txtExpenseLedgerName, txtExpenseAmount);
                        txtExpenseLedgerCode.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                }
            }
        }

        private void txtPayeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbLocation);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationIOUNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                Common.SetAutoComplete(txtDocumentNo, accPettyCashIOUService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();
                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void chkAutoCompleationPettyCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLocation.Text == string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    Common.SetAutoComplete(txtPettyCashBookCode, accLedgerAccountService.GetPettyCashLedgerCodes(), chkAutoCompleationPettyCash.Checked);
                    Common.SetAutoComplete(txtPettyCashBookName, accLedgerAccountService.GetPettyCashLedgerNames(), chkAutoCompleationPettyCash.Checked);
                }
                else
                {
                    // Read Petty Cash Books
                    AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                    LocationService locationService = new LocationService();

                    Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                    Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                DataView dvAllReferenceData = new DataView(accPettyCashIOUService.GetPendingPettyCashIOUDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashIOUDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                // Read Petty Cash Books
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);

                Common.SetDataGridviewColumnsReadOnly(true, dgvIOUDetail, dgvIOUDetail.Columns[1], dgvIOUDetail.Columns[2], dgvIOUDetail.Columns[3]);

                Common.EnableTextBox(true, txtEmployeeCode, txtEmployeeName, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo);
                Common.EnableTextBox(false, txtExpenseLedgerCode, txtExpenseLedgerName, txtExpenseAmount);
                Common.EnableButton(true, btnDocumentDetails);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);

                dtpDocumentDate.Value = DateTime.Now;

                txtDocumentNo.Text = GetDocumentNo(true);

                existingAccLedgerAccount = null;
                existingEmployee = null;

                accPettyCashIOUDetailList = null;

                grpHeader.Enabled = true;
                grpBody.Enabled = false;
                grpFooter.Enabled = false;

                this.ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        
        private void RefreshDocumentNumbers()
        {
            //Load IOU document numbers
            AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
            Common.SetAutoComplete(txtDocumentNo, accPettyCashIOUService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }

        public override void FormLoad()
        {
            try
            {
                dgvIOUDetail.AutoGenerateColumns = false;

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                // Read Employees
                EmployeeService employeeService = new EmployeeService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllEmployeeCodes(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllEmployeeNames(), chkAutoCompleationEmployee.Checked);

                // Read Petty Cash Books
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoComplete(txtPettyCashBookCode, accLedgerAccountService.GetPettyCashLedgerCodes(), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoComplete(txtPettyCashBookName, accLedgerAccountService.GetPettyCashLedgerNames(), chkAutoCompleationPettyCash.Checked);

                // Read Expense Ledgers
                Common.SetAutoComplete(txtExpenseLedgerCode, accLedgerAccountService.GetExpenceLedgerCodes(), chkAutoCompleationExpence.Checked);
                Common.SetAutoComplete(txtExpenseLedgerName, accLedgerAccountService.GetExpenceLedgerNames(), chkAutoCompleationExpence.Checked);

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;

                AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();
                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID);
                if (accTransactionTypeDetail == null)
                {
                    Toast.Show(this.Text + "Please verify the available transaction Entry setup.", Toast.messageType.Warning, Toast.messageAction.General);
                    return;
                }
                base.FormLoad();

                RefreshDocumentNumbers();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Pause Petty Cash IOU.
        /// </summary>
        public override void Pause()
        {
            if (!ValidateControls())
            { return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                string NewDocumentNo;
                bool saveDocument = SaveDocument(0, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 0);
                    ClearForm();
                    RefreshDocumentNumbers();
                }
                else
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        /// <summary>
        /// Save New Petty Cash IOU.
        /// </summary>
        public override void Save()
        {
            if (!IsValidateControls()) { return; }
            if (!IsValidateExistsValidReimburseAmount()) {return;}

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim())) { return; }
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                string NewDocumentNo;
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim(), out NewDocumentNo);
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(NewDocumentNo.Trim(), 1);
                    ClearForm();
                    RefreshDocumentNumbers();
                }
                else
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        public override void ClearForm()
        {
            dgvIOUDetail.DataSource = null;
            dgvIOUDetail.Refresh();
            base.ClearForm();
        }

        private void GenerateReport(string documentNo, int documentStatus)
        {
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            accReportGenerator.GenerateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        /// <summary>
        ///  Save data into DB
        /// </summary>
        /// <param name="documentStatus"></param>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeFigures(accPettyCashIOUDetailList);

                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                AccPettyCashReimbursement accPettyCashReimbursement = new AccPettyCashReimbursement();

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());
                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                accPettyCashReimbursement = accPettyCashReimbursementService.GetAccPettyCashBalancedReimbursementHeader(accLedgerAccount.AccLedgerAccountID, location.LocationID);

                if (accPettyCashIOUHeader == null)
                {accPettyCashIOUHeader = new AccPettyCashIOUHeader();}
                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                accPettyCashIOUHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                accPettyCashIOUHeader.CompanyID = location.CompanyID;
                accPettyCashIOUHeader.LocationID = location.LocationID;
                accPettyCashIOUHeader.DocumentID = documentID;
                accPettyCashIOUHeader.DocumentNo = documentNo.Trim();
                accPettyCashIOUHeader.DocumentDate = Common.FormatDateTime(dtpDocumentDate.Value);
                accPettyCashIOUHeader.EmployeeID = employee.EmployeeID;
                accPettyCashIOUHeader.PayeeName = txtPayeeName.Text.Trim();
                accPettyCashIOUHeader.PettyCashLedgerID = accLedgerAccount.AccLedgerAccountID;
                accPettyCashIOUHeader.Amount = Common.ConvertStringToDecimalCurrency(txtExpenseTotalAmount.Text.Trim());
                accPettyCashIOUHeader.Reference = txtReferenceNo.Text.Trim();
                accPettyCashIOUHeader.Remark = txtRemark.Text.Trim();
                accPettyCashIOUHeader.IOUStatus = 1;
                accPettyCashIOUHeader.ReferenceDocumentID = accPettyCashReimbursement.AccPettyCashReimbursementID;
                accPettyCashIOUHeader.ReferenceDocumentDocumentID = accPettyCashReimbursement.DocumentID;
                accPettyCashIOUHeader.ReferenceLocationID = accPettyCashReimbursement.LocationID;
                accPettyCashIOUHeader.DocumentStatus = documentStatus;
                accPettyCashIOUHeader.CreatedUser = Common.LoggedUser;
                accPettyCashIOUHeader.CreatedDate = Common.GetSystemDateWithTime();
                accPettyCashIOUHeader.ModifiedUser = Common.LoggedUser;
                accPettyCashIOUHeader.ModifiedDate = Common.GetSystemDateWithTime();

                AccPettyCashIOUDetail accPettyCashIOUDetail;
                List<AccPettyCashIOUDetail> tempAccPettyCashIOUDetailList = new List<AccPettyCashIOUDetail>();

                //have to update list of items & update values to detail table
                foreach (AccPettyCashIOUDetail tempAccPettyCashIOUDetail in accPettyCashIOUDetailList)
                {
                    accPettyCashIOUDetail = new AccPettyCashIOUDetail();
                    accPettyCashIOUDetail.GroupOfCompanyID = accPettyCashIOUHeader.GroupOfCompanyID;
                    accPettyCashIOUDetail.CompanyID = accPettyCashIOUHeader.CompanyID;
                    accPettyCashIOUDetail.LocationID = accPettyCashIOUHeader.LocationID;
                    accPettyCashIOUDetail.DocumentID = accPettyCashIOUHeader.DocumentID;
                    accPettyCashIOUDetail.DocumentStatus = accPettyCashIOUHeader.DocumentStatus;
                    accPettyCashIOUDetail.AccPettyCashIOUHeaderID = accPettyCashIOUHeader.AccPettyCashIOUHeaderID;
                    accPettyCashIOUDetail.CreatedUser = accPettyCashIOUHeader.CreatedUser;
                    accPettyCashIOUDetail.CreatedDate = accPettyCashIOUHeader.CreatedDate;
                    accPettyCashIOUDetail.ModifiedUser = accPettyCashIOUHeader.ModifiedUser;
                    accPettyCashIOUDetail.ModifiedDate = accPettyCashIOUHeader.ModifiedDate;
                    accPettyCashIOUDetail.LedgerID = tempAccPettyCashIOUDetail.LedgerID;
                    accPettyCashIOUDetail.Amount = tempAccPettyCashIOUDetail.Amount;
                    accPettyCashIOUDetail.LedgerCode = tempAccPettyCashIOUDetail.LedgerCode;
                    accPettyCashIOUDetail.LedgerName = tempAccPettyCashIOUDetail.LedgerName;
                    tempAccPettyCashIOUDetailList.Add(accPettyCashIOUDetail);
                }

                //check & do this
                //return accPettyCashIOUService.SavePettyCashIOU(accPettyCashIOUHeader, accPettyCashIOUDetailList, out newDocumentNo, this.Name);
                return accPettyCashIOUService.SavePettyCashIOU(accPettyCashIOUHeader, tempAccPettyCashIOUDetailList, out newDocumentNo, this.Name);
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                newDocumentNo = string.Empty;
                return false;  
            }
        }

        private string GetDocumentNo(bool isTemporytNo)
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                LocationService locationService = new LocationService();
                return accPettyCashIOUService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void GetSummarizeFigures(List<AccPettyCashIOUDetail> listItem)
        {
            decimal expenseTotalAmount = 0;
            try
            {
                expenseTotalAmount = listItem.GetSummaryAmount(x => x.Amount);

                txtExpenseTotalAmount.Text = Common.ConvertDecimalToStringCurrency(expenseTotalAmount);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void CalculateIOUBalance()
        {
            decimal iouTotalAmount = 0;
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                EmployeeService employeeService = new EmployeeService();
                List<AccPettyCashIOUHeader> accPettyCashIOUHeaderList = new List<AccPettyCashIOUHeader>();
                long employeeID = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim()).EmployeeID;
                iouTotalAmount = accPettyCashIOUService.GetAllNotSettelledIOUDocuments(employeeID);

                txtBalanceIOUAmount.Text = Common.ConvertDecimalToStringCurrency(iouTotalAmount);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        // Confirm Employee by employeeID
        private bool IsEmployeeExistsByID(long employeeID)
        {
            bool recodFound = false;
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            employee = employeeService.GetEmployeesByID(employeeID);
            if (employee != null)
            {
                recodFound = true;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtEmployeeName.Text = employee.EmployeeName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName);
                Toast.Show(lblEmployee.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Employee by employeeCode
        private bool IsEmployeeExistsByCode(string employeeCode)
        {
            bool recodFound = false;
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            employee = employeeService.GetEmployeesByCode(employeeCode);
            if (employee != null)
            {
                recodFound = true;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtEmployeeName.Text = employee.EmployeeName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName);
                Toast.Show(lblEmployee.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Employee by employeeName
        private bool IsEmployeeExistsByName(string employeeName)
        {
            bool recodFound = false;
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee();
            employee = employeeService.GetEmployeesByName(employeeName);
            if (employee != null)
            {
                recodFound = true;
                txtEmployeeCode.Text = employee.EmployeeCode;
                txtEmployeeName.Text = employee.EmployeeName;
            }
            else
            {
                recodFound = false;
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName);
                Toast.Show(lblEmployee.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Location by Name
        private bool IsLocationExistsByName(string locationName)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByName(locationName);
            if (location != null)
            {
                recodFound = true;
                cmbLocation.Text = location.LocationName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbLocation);
                Toast.Show(lblLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        private void AssignExpenseLedgerProperties()
        {
            try
            {
                if (Common.ConvertStringToDecimalCurrency(txtExpenseAmount.Text.Trim()) > 0)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccPettyCashIOUDetail accPettyCashIOUDetail = new AccPettyCashIOUDetail();

                    accPettyCashIOUDetail.LedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtExpenseLedgerCode.Text.Trim()).AccLedgerAccountID;
                    accPettyCashIOUDetail.Amount = Common.ConvertStringToDecimalCurrency(txtExpenseAmount.Text.Trim());
                    accPettyCashIOUDetail.LedgerCode = txtExpenseLedgerCode.Text.Trim();
                    accPettyCashIOUDetail.LedgerName = txtExpenseLedgerName.Text.Trim();

                    UpdateGrid(accPettyCashIOUDetail);
                    txtExpenseLedgerCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void UpdateGrid(AccPettyCashIOUDetail accPettyCashIOUDetail)
        {
            try
            {
                if (accPettyCashIOUDetailList == null)
                {accPettyCashIOUDetailList = new List<AccPettyCashIOUDetail>();}

                AccPettyCashIOUDetail accPettyCashIOUDetailRemove = new AccPettyCashIOUDetail();

                accPettyCashIOUDetailRemove = accPettyCashIOUDetailList.Where(p => p.LedgerID.Equals(accPettyCashIOUDetail.LedgerID)).FirstOrDefault();

                if (accPettyCashIOUDetailRemove != null)
                {accPettyCashIOUDetailList.Remove(accPettyCashIOUDetailRemove);}

                accPettyCashIOUDetailList.Add(accPettyCashIOUDetail);
                dgvIOUDetail.DataSource = null;
                dgvIOUDetail.DataSource = accPettyCashIOUDetailList;
                dgvIOUDetail.Refresh();

                Common.ClearTextBox(txtExpenseLedgerCode, txtExpenseLedgerName, txtExpenseAmount);
                Common.EnableTextBox(false, txtEmployeeCode, txtEmployeeName, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo);
                Common.EnableComboBox(false, cmbLocation);
                Common.EnableButton(true, btnSave, btnPause);
                Common.EnableButton(false, btnDocumentDetails);
                GetSummarizeFigures(accPettyCashIOUDetailList);
                if (accPettyCashIOUDetailList.Count > 0)
                { grpFooter.Enabled = true; }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool LoadExpenseLedgers(bool isCode, string strLedger)
        {
            bool isValid = false;
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                existingAccLedgerAccount = new AccLedgerAccount();

                if (strLedger.Equals(string.Empty))
                { return isValid; }
                
                if (isCode)
                {
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerExpenseAccountByCode(strLedger);
                    if (isCode && strLedger.Equals(string.Empty))
                    {
                        txtExpenseLedgerName.Focus();
                        return isValid;
                    }
                }
                else
                {existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerExpenseAccountByName(strLedger);}

                if (existingAccLedgerAccount != null)
                {
                    if (isCode)
                    {
                        txtExpenseLedgerName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        txtExpenseLedgerCode.Text = existingAccLedgerAccount.LedgerCode;
                    }

                    txtExpenseAmount.Focus();
                    txtExpenseAmount.SelectAll();
                    isValid = true;
                    return isValid;
                }
                else
                {
                    Toast.Show("Ledger - " + strLedger + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtExpenseLedgerCode.Focus();
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return isValid;
        }

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPettyCashIOUDocument()
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();

                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                if (accPettyCashIOUHeader == null)
                {
                    return;
                    //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByID(accPettyCashIOUHeader.PettyCashLedgerID);
                    txtDocumentNo.Text = accPettyCashIOUHeader.DocumentNo;
                    IsEmployeeExistsByID(accPettyCashIOUHeader.EmployeeID);
                    txtRemark.Text = accPettyCashIOUHeader.Remark;
                    dtpDocumentDate.Value = accPettyCashIOUHeader.DocumentDate;
                    txtReferenceNo.Text = accPettyCashIOUHeader.Reference;
                    cmbLocation.SelectedValue = accPettyCashIOUHeader.LocationID;
                    txtPayeeName.Text = accPettyCashIOUHeader.PayeeName;
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    txtExpenseTotalAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashIOUHeader.Amount);

                    LoadPettyCashLimitBalance();

                    dgvIOUDetail.DataSource = null;
                    accPettyCashIOUDetailList = accPettyCashIOUService.GetAllPettyCashIOUDetail(accPettyCashIOUHeader);
                    dgvIOUDetail.DataSource = accPettyCashIOUDetailList;
                    dgvIOUDetail.Refresh();

                    Common.EnableTextBox(false, txtEmployeeCode, txtEmployeeName, txtPettyCashBookCode, txtPettyCashBookName);
                    Common.EnableComboBox(false, cmbLocation);

                    if (accPettyCashIOUHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        grpFooter.Enabled = true;
                        //EnableLine(true);
                        Common.EnableButton(true, btnSave, btnPause);
                        Common.EnableButton(false, btnDocumentDetails);
                    }
                    else
                    {
                        grpHeader.Enabled = false;
                        grpBody.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load Petty Cash Balance
        /// </summary>
        private void LoadPettyCashLimitBalance()
        {
            try
            {
                AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
                AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                LocationService locationService = new LocationService();
                Location location = new Location();
                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();

                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                location = locationService.GetLocationsByName(cmbLocation.Text);
                accPettyCashImprestDetail = accPettyCashImprestService.GetAccPettyCashImprestByPettyCashBookID(accLedgerAccount.AccLedgerAccountID,location.LocationID);

                decimal pettyCashBookBalance = accGlTransactionService.GetAccLedgerAccountBalanceByAccountID(accLedgerAccount.AccLedgerAccountID, Common.FormatDate(dtpDocumentDate.Value), false);
                txtPettyCashBalance.Text = Common.ConvertDecimalToStringCurrency(pettyCashBookBalance);

                if (accPettyCashImprestDetail != null)
                {
                    //txtPettyCashBalance.Text = Common.ConvertDecimalToStringCurrency(accPettyCashImprestDetail.BalanceAmount);
                }
                else
                {
                    Toast.Show("Please allocate the Cash Limit. " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtPettyCashBookCode.Focus();
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                { referenceSearch = new FrmReferenceSearch(); }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        { return; }
                    }
                }

                referenceSearch.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            bool isValid = true;

            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccPettyCashIOUValidator accPettyCashIOUValidator = new AccPettyCashIOUValidator();
            LocationService locationService = new LocationService();
            
            
            if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtEmployeeCode, txtPettyCashBookCode, cmbLocation))
            { 
                isValid = false;
                return isValid;
            }

            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashIOUValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - " + cmbLocation.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(txtPettyCashBookCode.Text.Trim()));
                isValid = false;
            }
            
            return isValid;
        }

        private bool IsValidateControls()
        {
            bool isValidated = false;
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtExpenseTotalAmount))
            { isValidated = false; }
            else
            {
                isValidated = true;
            }

            return isValidated;
        }

        private bool IsValidateExistsValidReimburseRecord()
        {
            AccPettyCashIOUValidator accPettyCashIOUValidator = new AccPettyCashIOUValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = false;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashIOUValidator.ValidateExistsValidReimburseRecord(pettyCashLedgerID, locationID))
            {
                Toast.Show("Reimbursement ", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + " - " + txtPettyCashBookName.Text.Trim() + " - " + cmbLocation.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        private bool IsValidateExistsValidReimburseAmount()
        {
            AccPettyCashIOUValidator accPettyCashIOUValidator = new AccPettyCashIOUValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = false;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashIOUValidator.ValidateExistsValidReimburseAmount(pettyCashLedgerID, locationID, Common.ConvertStringToDecimalCurrency(txtExpenseTotalAmount.Text)))
            {
                Toast.Show("Total settlement amount", Toast.messageType.Information, Toast.messageAction.Invalid, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + " - " + txtPettyCashBookName.Text.Trim() + ", - " + cmbLocation.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion

        

        
        

    }
}
