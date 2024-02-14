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
    public partial class FrmPettyCashBillEntry : UI.Windows.FrmBaseTransactionForm
    {
        List<AccPettyCashBillDetail> accPettyCashBillDetailList = new List<AccPettyCashBillDetail>();

        private AccLedgerAccount existingAccLedgerAccount;
        private Employee existingEmployee;
        private AccPettyCashIOUHeader existingAccPettyCashIOUHeader;

        int documentID = 5; 

        public FrmPettyCashBillEntry()
        {
            InitializeComponent();
        }

        #region Form Events
        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                DataView dvAllReferenceData = new DataView(accPettyCashBillService.GetPendingPettyCashBillDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashBillDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    {
                        LoadPettyCashBillDocument();
                    }
                    else
                    {
                        txtDocumentNo.Text = GetDocumentNo(true);
                        cmbLocation.Focus();
                    }
                    //txtDocumentNo_Leave(this, e);
                }                
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                //{
                //    txtDocumentNo.Text = GetDocumentNo(true);
                //    cmbLocation.Focus();
                //}
                //else
                //{
                //    AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                //    AccPettyCashBillHeader accPettyCashBillHeader = new AccPettyCashBillHeader();

                //    accPettyCashBillHeader = accPettyCashBillService.GetAccPettyCashBillHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                //    if (accPettyCashBillHeader == null)
                //    {
                //        //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                //        return;
                //    }
                //    else
                //    {
                //        LoadPettyCashBillDocument(accPettyCashBillHeader);
                //    }
                //}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void btnIOUDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                DataView dvAllReferenceData = new DataView(accPettyCashIOUService.GetPendingPettyCashIOUDocumentsToBill());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtIOUNo);
                    if (!txtIOUNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashIOUDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                    { Common.SetFocus(e, txtEmployeeCode); }
                    else
                    { Common.SetFocus(e, txtPettyCashBookName); }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPettyCashBookCode.Text))
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

                    DataView dvPettyCash = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetHeadOfficeLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvPettyCash.Count != 0)
                    {
                        LoadReferenceSearchForm(dvPettyCash, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtPettyCashBookName);
                        txtPettyCashBookName_Leave(this, e);
                    }
                }
                if (txtPettyCashBookName.Text != string.Empty)
                {Common.SetFocus(e, txtPettyCashBookCode);}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtPettyCashBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPettyCashBookName.Text))
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
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void txtPayeeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtRemark);
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
                Common.SetFocus(e, cmbCostCentre);
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
                Common.SetFocus(e, txtPettyCashBookCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    Common.EnableTextBox(true, txtExpenseLedgerCode, txtExpenseLedgerName);
                    Common.SetFocus(e, txtExpenseLedgerCode);
                    grpBody.Enabled = true;
                    grpFooter.Enabled = true;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    Common.SetFocus(e, dtpDocumentDate);
                }
            }
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
                    //LoadPettyCashBook();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCostCentre_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReferenceNo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCostCentre_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbCostCentre.Text.Trim()))
                { return; }

                if (!IsCostCentreExistsByName(cmbCostCentre.Text.Trim()))
                {
                    cmbCostCentre.Focus();
                    return;
                }
                else
                {

                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtIOUNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
            {
                return;
            }
            else
            {
                //txtIOUNo_Leave(this, e);
                if (!string.IsNullOrEmpty(txtIOUNo.Text))
                {LoadPettyCashIOUDocument();}
            }   
        }

        private void txtIOUNo_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (txtIOUNo.Text.Equals(string.Empty))
                //{ return; }
                //else
                //{
                //    LoadPettyCashIOUDocument();
                    
                //    //AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                //    //existingAccPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashSettlementsPendingIOUHeaderByDocumentNo(txtIOUNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                //    //if (existingAccPettyCashIOUHeader != null)
                //    //{
                //    //    txtIOUAmount.Text = Common.ConvertDecimalToStringCurrency(existingAccPettyCashIOUHeader.Amount);
                        
                //    //}
                //}

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

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
                    //if (!IsValidateExistsValidReimburseRecord()) { return; }

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

        private void chkAutoCompleationBillNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                Common.SetAutoComplete(txtDocumentNo, accPettyCashBillService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationBillNo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationIOUNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                Common.SetAutoComplete(txtIOUNo, accPettyCashIOUService.GetAllDocumentNumbersToBill(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationIOUNo.Checked);
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
                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                // Read Petty Cash Books
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                Common.SetAutoComplete(txtPettyCashBookCode, accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), chkAutoCompleationPettyCash.Checked);

                // Load Cost centers
                CostCentreService costCentreService = new CostCentreService();
                Common.LoadCostCenters(cmbCostCentre, costCentreService.GetAllCostCentres());

                Common.SetDataGridviewColumnsReadOnly(true, dgvBillDetail, dgvBillDetail.Columns[1], dgvBillDetail.Columns[2], dgvBillDetail.Columns[3]);

                Common.EnableTextBox(true, txtPettyCashBookCode, txtPettyCashBookName, txtEmployeeCode, txtEmployeeName, txtDocumentNo, txtIOUNo);
                Common.EnableTextBox(false, txtExpenseLedgerCode, txtExpenseLedgerName, txtExpenseAmount);
                Common.EnableButton(true, btnDocumentDetails, btnIOUDetails);
                Common.EnableComboBox(true, cmbLocation, cmbCostCentre);
                Common.EnableButton(false, btnSave, btnPause);

                dtpDocumentDate.Value = DateTime.Now;

                txtDocumentNo.Text = GetDocumentNo(true);

                existingAccLedgerAccount = null;
                existingEmployee = null;

                accPettyCashBillDetailList = null;

                grpHeader.Enabled = true;
                grpBody.Enabled = false;
                grpFooter.Enabled = true;

                this.ActiveControl = txtPettyCashBookCode;
                txtPettyCashBookCode.Focus();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public override void FormLoad()
        {
            try
            {
                dgvBillDetail.AutoGenerateColumns = false;

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

                base.FormLoad();

                RefreshDocumentNumbers();
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
            Common.SetAutoComplete(txtIOUNo, accPettyCashIOUService.GetAllDocumentNumbersToBill(Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationIOUNo.Checked);

            //Load Bill Document Numbers
            AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
            Common.SetAutoComplete(txtDocumentNo, accPettyCashBillService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationBillNo.Checked);
        }

        /// <summary>
        /// Pause Petty Cash Bill.
        /// </summary>
        public override void Pause()
        {
            if (!IsValidateExistsPausedIOUDocument()) { return; }

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
        /// Save New Petty Cash Bill.
        /// </summary>
        public override void Save()
        {
            if (!ValidateControls()) { return; }
            if (!IsValidateControls()) { return; }
            if (!IsValidateExistsPausedIOUDocument()) { return; }
            //if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim())) { return; }
            if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
            ////??????????? if (!IsValidateExistsValidReimburseAmount()) { return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
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
            dgvBillDetail.DataSource = null;
            dgvBillDetail.Refresh();
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
                GetSummarizeFigures(accPettyCashBillDetailList);

                AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                AccPettyCashBillHeader accPettyCashBillHeader = new AccPettyCashBillHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                CostCentreService costCentreService = new CostCentreService();
                CostCentre costCentre = new CostCentre();

                location = locationService.GetLocationsByName(cmbLocation.Text.Trim());
                costCentre = costCentreService.GetCostCentresByName(cmbCostCentre.Text.Trim());
                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashSettlementsPendingIOUHeaderByDocumentNo(txtIOUNo.Text.Trim(), location.LocationID);
                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);

                accPettyCashBillHeader = accPettyCashBillService.GetAccPettyCashBillHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                if (accPettyCashBillHeader == null)
                {
                    accPettyCashBillHeader = new AccPettyCashBillHeader();
                }
                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                accPettyCashBillHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                accPettyCashBillHeader.CompanyID = location.CompanyID;
                accPettyCashBillHeader.LocationID = location.LocationID;
                accPettyCashBillHeader.CostCentreID = costCentre.CostCentreID;
                accPettyCashBillHeader.DocumentID = documentID;
                accPettyCashBillHeader.DocumentNo = documentNo.Trim();
                accPettyCashBillHeader.DocumentDate = Common.FormatDateTime(dtpDocumentDate.Value);
                if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());
                    accPettyCashBillHeader.EmployeeID = employee.EmployeeID;
                }
                else
                { accPettyCashBillHeader.EmployeeID = 0; }
                accPettyCashBillHeader.PayeeName = txtPayeeName.Text.Trim();
                accPettyCashBillHeader.PettyCashLedgerID = accLedgerAccount.AccLedgerAccountID;
                accPettyCashBillHeader.Reference = txtReferenceNo.Text.Trim();
                accPettyCashBillHeader.Remark = txtRemark.Text.Trim();
                accPettyCashBillHeader.Amount = Common.ConvertStringToDecimalCurrency(txtTotalBillAmount.Text);
                accPettyCashBillHeader.ReturnedAmount = Common.ConvertStringToDecimalCurrency(txtReturnAmount.Text);
                accPettyCashBillHeader.ToSettleAmount = Common.ConvertStringToDecimalCurrency(txtToSettleAmount.Text);
                accPettyCashBillHeader.DocumentStatus = documentStatus;
                accPettyCashBillHeader.CreatedUser = Common.LoggedUser;
                accPettyCashBillHeader.CreatedDate = DateTime.UtcNow;
                accPettyCashBillHeader.ModifiedUser = Common.LoggedUser;
                accPettyCashBillHeader.ModifiedDate = DateTime.UtcNow;
                if (accPettyCashIOUHeader != null)
                {
                    accPettyCashBillHeader.IOUAmount = accPettyCashIOUHeader.Amount;
                    accPettyCashBillHeader.ReferenceDocumentID = accPettyCashIOUHeader.AccPettyCashIOUHeaderID;
                    accPettyCashBillHeader.ReferenceDocumentDocumentID = accPettyCashIOUHeader.DocumentID;
                    accPettyCashBillHeader.ReferenceLocationID = accPettyCashIOUHeader.LocationID;
                }

                AccPettyCashBillDetail accPettyCashBillDetail;
                List<AccPettyCashBillDetail> tempAccPettyCashBillDetailList = new List<AccPettyCashBillDetail>();

                //have to update list of items & update values to detail table
                foreach (AccPettyCashBillDetail tempAccPettyCashBillDetail in accPettyCashBillDetailList)
                {
                    accPettyCashBillDetail = new AccPettyCashBillDetail();
                    accPettyCashBillDetail.AccPettyCashBillDetailID = tempAccPettyCashBillDetail.AccPettyCashBillDetailID;
                    accPettyCashBillDetail.AccPettyCashBillHeaderID = tempAccPettyCashBillDetail.AccPettyCashBillHeaderID;

                    accPettyCashBillDetail.GroupOfCompanyID = accPettyCashBillHeader.GroupOfCompanyID;
                    accPettyCashBillDetail.CompanyID = accPettyCashBillHeader.CompanyID;
                    accPettyCashBillDetail.LocationID = accPettyCashBillHeader.LocationID;
                    accPettyCashBillDetail.CostCentreID = accPettyCashBillHeader.CostCentreID;
                    accPettyCashBillDetail.DocumentID = accPettyCashBillHeader.DocumentID;
                    accPettyCashBillDetail.DocumentStatus = accPettyCashBillHeader.DocumentStatus;
                    accPettyCashBillDetail.CreatedUser = accPettyCashBillHeader.CreatedUser;
                    accPettyCashBillDetail.CreatedDate = accPettyCashBillHeader.CreatedDate;
                    accPettyCashBillDetail.ModifiedUser = accPettyCashBillHeader.ModifiedUser;
                    accPettyCashBillDetail.ModifiedDate = accPettyCashBillHeader.ModifiedDate;
                    accPettyCashBillDetail.LedgerID = tempAccPettyCashBillDetail.LedgerID;
                    accPettyCashBillDetail.Amount = tempAccPettyCashBillDetail.Amount;
                    accPettyCashBillDetail.LedgerCode = tempAccPettyCashBillDetail.LedgerCode;
                    accPettyCashBillDetail.LedgerName = tempAccPettyCashBillDetail.LedgerName;
                    accPettyCashBillDetail.IsUpLoad = tempAccPettyCashBillDetail.IsUpLoad;
                    accPettyCashBillDetail.DataTransfer = tempAccPettyCashBillDetail.DataTransfer;
                    accPettyCashBillDetail.AccPettyCashBillHeader = tempAccPettyCashBillDetail.AccPettyCashBillHeader;
                    tempAccPettyCashBillDetailList.Add(accPettyCashBillDetail);
                }

                return accPettyCashBillService.SavePettyCashBill(accPettyCashBillHeader, tempAccPettyCashBillDetailList, out newDocumentNo, this.Name);
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
                AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                LocationService locationService = new LocationService();
                return accPettyCashBillService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void GetSummarizeFigures(List<AccPettyCashBillDetail> listItem)
        {
            decimal expenseTotalAmount = 0;

            try
            {
                expenseTotalAmount = listItem.GetSummaryAmount(x => x.Amount);

                txtTotalBillAmount.Text = Common.ConvertDecimalToStringCurrency(expenseTotalAmount);
                if ((Common.ConvertStringToDecimalCurrency(txtTotalBillAmount.Text) - Common.ConvertStringToDecimalCurrency(txtIOUAmount.Text)) > 0)
                {
                    txtToSettleAmount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtTotalBillAmount.Text) - Common.ConvertStringToDecimalCurrency(txtIOUAmount.Text));
                    txtCashReturnAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
                else
                {
                    txtCashReturnAmount.Text = Common.ConvertDecimalToStringCurrency(Common.ConvertStringToDecimalCurrency(txtIOUAmount.Text) - Common.ConvertStringToDecimalCurrency(txtTotalBillAmount.Text));
                    txtToSettleAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
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

        // Confirm Cost Centre by Name
        private bool IsCostCentreExistsByID(int costCentreID)
        {
            bool recodFound = false;
            CostCentreService costCentreService = new CostCentreService();
            CostCentre costCentre = new CostCentre();
            costCentre = costCentreService.GetCostCentresByID(costCentreID);
            if (costCentre != null)
            {
                recodFound = true;
                cmbCostCentre.Text = costCentre.CostCentreName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbCostCentre);
                Toast.Show(lblCostCentre.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        // Confirm Cost Centre by Name
        private bool IsCostCentreExistsByName(string costCentreName)
        {
            bool recodFound = false;
            CostCentreService costCentreService = new CostCentreService();
            CostCentre costCentre = new CostCentre();
            costCentre = costCentreService.GetCostCentresByName(costCentreName);
            if (costCentre != null)
            {
                recodFound = true;
                cmbCostCentre.Text = costCentre.CostCentreName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbCostCentre);
                Toast.Show(lblCostCentre.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
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
                    AccPettyCashBillDetail accPettyCashBillDetail = new AccPettyCashBillDetail();

                    accPettyCashBillDetail.LedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtExpenseLedgerCode.Text.Trim()).AccLedgerAccountID;
                    accPettyCashBillDetail.Amount = Common.ConvertStringToDecimalCurrency(txtExpenseAmount.Text.Trim());
                    accPettyCashBillDetail.LedgerCode = txtExpenseLedgerCode.Text.Trim();
                    accPettyCashBillDetail.LedgerName = txtExpenseLedgerName.Text.Trim();

                    UpdateGrid(accPettyCashBillDetail);
                    txtExpenseLedgerCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void UpdateGrid(AccPettyCashBillDetail accPettyCashBillDetail)
        {
            try
            {
                if (accPettyCashBillDetailList == null)
                { accPettyCashBillDetailList = new List<AccPettyCashBillDetail>(); }

                AccPettyCashBillDetail accPettyCashBillDetailRemove = new AccPettyCashBillDetail();

                accPettyCashBillDetailRemove = accPettyCashBillDetailList.Where(p => p.LedgerID.Equals(accPettyCashBillDetail.LedgerID)).FirstOrDefault();

                if (accPettyCashBillDetailRemove != null)
                { accPettyCashBillDetailList.Remove(accPettyCashBillDetailRemove); }

                accPettyCashBillDetailList.Add(accPettyCashBillDetail);
                dgvBillDetail.DataSource = null;
                dgvBillDetail.DataSource = accPettyCashBillDetailList;
                dgvBillDetail.Refresh();

                Common.ClearTextBox(txtExpenseLedgerCode, txtExpenseLedgerName, txtExpenseAmount);
                Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo, txtIOUNo);
                Common.EnableComboBox(false, cmbLocation, cmbCostCentre);
                Common.EnableButton(true, btnSave, btnPause);
                Common.EnableButton(false, btnDocumentDetails);
                GetSummarizeFigures(accPettyCashBillDetailList);
                if (accPettyCashBillDetailList.Count > 0)
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
                { existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerExpenseAccountByName(strLedger); }

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

                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                location = locationService.GetLocationsByName(cmbLocation.Text);
                accPettyCashImprestDetail = accPettyCashImprestService.GetAccPettyCashImprestByPettyCashBookID(accLedgerAccount.AccLedgerAccountID, location.LocationID);

                if (accPettyCashImprestDetail != null)
                {
                    //txtPettyCashBalance.Text = Common.ConvertDecimalToStringCurrency(accPettyCashImprestDetail.BalanceAmount);
                }
                else
                {
                    Toast.Show("Please allocate the Cash Limit. " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) +  "- " + txtPettyCashBookCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load IOU Amount
        /// </summary>
        private void LoadPettyCashIOUDocument()
        {
            try
            {
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();
                location = locationService.GetLocationsByName(cmbLocation.Text);
                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashSettlementsPendingIOUHeaderByDocumentNo(txtIOUNo.Text.Trim(), location.LocationID);
                if (accPettyCashIOUHeader != null)
                {
                    txtIOUNo.Text = accPettyCashIOUHeader.DocumentNo;
                    txtIOUAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashIOUHeader.Amount); 
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(accPettyCashIOUHeader.PettyCashLedgerID);
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    employee = employeeService.GetEmployeesByID(accPettyCashIOUHeader.EmployeeID);
                    txtEmployeeCode.Text = employee.EmployeeCode;
                    txtEmployeeName.Text = employee.EmployeeName;

                    txtDocumentNo.Text = GetDocumentNo(true);
                    txtDocumentNo.Enabled = false;

                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo, txtIOUNo);
                    Common.EnableButton(false, btnDocumentDetails, btnIOUDetails);
                    Common.EnableComboBox(false, cmbLocation);

                    this.ActiveControl = txtEmployeeCode;
                    txtEmployeeCode.Focus();
                }
                else
                {
                    txtIOUAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtIOUNo.Text = string.Empty;
                    Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblIOUNo.Text.ToString()), " - ", txtIOUAmount.Text.Trim()) + "", Toast.messageType.Information, Toast.messageAction.Invalid);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPettyCashBillDocument()
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                AccPettyCashBillHeader accPettyCashBillHeader = new AccPettyCashBillHeader();

                accPettyCashBillHeader = accPettyCashBillService.GetAccPettyCashBillHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                if (accPettyCashBillHeader != null)
                {
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByID(accPettyCashBillHeader.PettyCashLedgerID);
                    accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(accPettyCashBillHeader.ReferenceDocumentDocumentID, accPettyCashBillHeader.ReferenceDocumentID, accPettyCashBillHeader.ReferenceLocationID);

                    txtDocumentNo.Text = accPettyCashBillHeader.DocumentNo;
                    if (accPettyCashBillHeader.EmployeeID != 0)
                    { IsEmployeeExistsByID(accPettyCashBillHeader.EmployeeID); }
                    txtRemark.Text = accPettyCashBillHeader.Remark;
                    dtpDocumentDate.Value = accPettyCashBillHeader.DocumentDate;
                    txtReferenceNo.Text = accPettyCashBillHeader.Reference;
                    cmbLocation.SelectedValue = accPettyCashBillHeader.LocationID;
                    cmbCostCentre.SelectedValue = accPettyCashBillHeader.CostCentreID;

                    txtRemark.Text = accPettyCashBillHeader.Remark;
                    txtPayeeName.Text = accPettyCashBillHeader.PayeeName;
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    txtTotalBillAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashBillHeader.Amount);
                    txtReturnAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashBillHeader.ReturnedAmount);
                    txtToSettleAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashBillHeader.ToSettleAmount);
                    if (accPettyCashIOUHeader != null)
                    {
                        txtIOUNo.Text = accPettyCashIOUHeader.DocumentNo;
                        txtIOUAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashIOUHeader.Amount);
                    }
                    //IsCostCentreExistsByID(accPettyCashBillHeader.)

                    LoadPettyCashLimitBalance();

                    dgvBillDetail.DataSource = null;
                    accPettyCashBillDetailList = accPettyCashBillService.GetAllPettyCashBillDetail(accPettyCashBillHeader);
                    dgvBillDetail.DataSource = accPettyCashBillDetailList;
                    dgvBillDetail.Refresh();

                    GetSummarizeFigures(accPettyCashBillDetailList);

                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo, txtIOUNo);
                    Common.EnableComboBox(false, cmbLocation, cmbCostCentre);

                    if (accPettyCashBillHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        grpFooter.Enabled = true;
                        //EnableLine(true);
                        Common.EnableButton(true, btnSave, btnPause);
                        Common.EnableButton(false, btnDocumentDetails, btnIOUDetails);
                    }
                    else
                    {
                        grpHeader.Enabled = false;
                        grpFooter.Enabled = false;
                    }
                }
                else
                {
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

        #region Validate Logics

        private bool ValidateControls()
        {
            bool isValid = true;
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccPettyCashBillEntryValidator accPettyCashBillEntryValidator = new AccPettyCashBillEntryValidator();
            LocationService locationService = new LocationService();

            if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtPettyCashBookCode, txtPayeeName, cmbLocation, cmbCostCentre))
            { return false; }

            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashBillEntryValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - " + cmbLocation.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(txtPettyCashBookCode.Text.Trim()));
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Validate Parent property of the Child
        /// </summary>
        /// <returns></returns>
        private bool IsValidateControls()
        {
            bool isValidated = false;
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtTotalBillAmount))
            { return false; }

            if (Common.ConvertStringToDecimalCurrency(txtCashReturnAmount.Text.Trim()) > 0 && Common.ConvertStringToDecimalCurrency(txtCashReturnAmount.Text.Trim()) != Common.ConvertStringToDecimalCurrency(txtReturnAmount.Text.Trim()))
            {
                Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblReturnAmount.Text.ToString()), " - ", txtReturnAmount.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        private bool IsValidateExistsPausedIOUDocument()
        {
            bool isValidated = true;
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccPettyCashBillEntryValidator accPettyCashBillEntryValidator = new AccPettyCashBillEntryValidator();
            LocationService locationService = new LocationService();
            AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
            AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();

            if (!string.IsNullOrEmpty(txtIOUNo.Text.Trim()))
            {
                long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
                int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID, txtIOUNo.Text.Trim(), locationID);

                if (!accPettyCashBillEntryValidator.ValidateExistsPausedBillRecordByIOUDocumentID(txtDocumentNo.Text.Trim(), accPettyCashIOUHeader.AccPettyCashIOUHeaderID, accPettyCashIOUHeader.DocumentID, accPettyCashIOUHeader.LocationID))
                {
                    Toast.Show(this.Text + " paused document", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, Common.ConvertStringToDisplayFormat(lblIOUNo.Text.Trim()) + " - " + txtIOUNo.Text.Trim());
                    isValidated = false;
                    return isValidated;
                }
                else
                { isValidated = true; }
            }
            else
            { isValidated = true; }
            return isValidated;
        }

        private bool IsValidateExistsValidReimburseRecord()
        {
            AccPettyCashBillEntryValidator accPettyCashBillEntryValidator = new AccPettyCashBillEntryValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = false;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashBillEntryValidator.ValidateExistsValidReimburseRecord(pettyCashLedgerID, locationID))
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
            AccPettyCashBillEntryValidator accPettyCashBillEntryValidator = new AccPettyCashBillEntryValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = false;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashBillEntryValidator.ValidateExistsValidReimburseAmount(pettyCashLedgerID, locationID, Common.ConvertStringToDecimalCurrency(txtTotalBillAmount.Text)))
            {
                Toast.Show("Total settlement amount", Toast.messageType.Information, Toast.messageAction.Invalid, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + " - " + txtPettyCashBookName.Text.Trim() + ", - " + cmbLocation.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion

        private void dgvBillDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F2))
            {
                try
                {
                    if (dgvBillDetail.CurrentCell != null && dgvBillDetail.CurrentCell.RowIndex >= 0)
                    {
                        if (Toast.Show("Expenses " + dgvBillDetail["LedgerName", dgvBillDetail.CurrentCell.RowIndex].Value.ToString() + " - " + dgvBillDetail["Amount", dgvBillDetail.CurrentCell.RowIndex].Value.ToString() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                        { return; }

                        AccPettyCashBillDetail accPettyCashBillDetailRemove = new AccPettyCashBillDetail();

                        accPettyCashBillDetailRemove = accPettyCashBillDetailList.Where(p => p.LedgerID.Equals(Common.ConvertStringToLong(dgvBillDetail["AccLedgerAccountID", dgvBillDetail.CurrentCell.RowIndex].Value.ToString()))).FirstOrDefault();

                        if (accPettyCashBillDetailRemove != null)
                        { accPettyCashBillDetailList.Remove(accPettyCashBillDetailRemove); }

                        dgvBillDetail.DataSource = null;
                        dgvBillDetail.DataSource = accPettyCashBillDetailList;
                        dgvBillDetail.Refresh();

                        GetSummarizeFigures(accPettyCashBillDetailList);

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

        
        #endregion

        private void FrmPettyCashBillEntry_Load(object sender, EventArgs e)
        {

        }

        

        

        
    }
}
