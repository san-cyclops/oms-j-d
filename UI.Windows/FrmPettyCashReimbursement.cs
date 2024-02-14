using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Account;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmPettyCashReimbursement : UI.Windows.FrmBaseTransactionForm
    {
        private AccLedgerAccount existingAccLedgerAccount;
        private Employee existingEmployee;

        int documentID = 6;
        decimal balanceReimburseAmount = 0;

        public FrmPettyCashReimbursement()
        {
            InitializeComponent();
        }

        #region Form Events

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
                        txtPettyCashBookCode_Leave(this, e);
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
        
        private void txtPettyCashBookCode_Leave(object sender, EventArgs e)
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

                        // Set Book Balance, Imprest Amount, Reimbursement Amount
                        bool isValid = LoadPettyCashLimitBalance();

                        if (isValid)
                        {
                            txtDocumentNo.Text = GetDocumentNo(true);
                            if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                            {
                                txtDocumentNo.Enabled = false;
                            }
                        }
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
                Common.SetFocus(e, txtPettyCashBookCode);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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
                //if (txtEmployeeCode.Text != string.Empty)
                //{ Common.SetFocus(e, txtPayeeName); }
                //else
                //{ Common.SetFocus(e, txtEmployeeName); }
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
                Common.SetFocus(e, txtPayeeName);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtEmployeeCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
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
                Common.SetFocus(e, dtpDocumentDate);
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
                Common.SetFocus(e, txtReferenceNo);
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
                Common.SetFocus(e, rdoCheque);
                grpBody.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_Leave(object sender, EventArgs e)
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
                    bool isValid = LoadPettyCashLimitBalance();
                    if (!isValid)
                    {
                        txtPettyCashBookCode.Text  = string.Empty;
                        txtPettyCashBookName.Text = string.Empty;
                    }
                    RefreshPettyCashByLocation();

                    //// Read Petty Cash Books
                    //AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                    //LocationService locationService = new LocationService();
                    //string[] accLedgerAccountList = accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID);
                    
                    //Common.SetAutoComplete(txtPettyCashBookCode, accLedgerAccountList, true);
                    //Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), true);
                    //RefreshPettyCashByLocation();
                    //LoadPettyCashBook();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public void RefreshPettyCashByLocation()
        {
            try
            {
                // Read Petty Cash Books
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                LocationService locationService = new LocationService();
                string[] accLedgerAccountList = accPettyCashMasterService.GetPettyCashLedgerCodesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID);

                Common.SetAutoComplete(txtPettyCashBookCode, accLedgerAccountList, true);
                Common.SetAutoComplete(txtPettyCashBookName, accPettyCashMasterService.GetPettyCashLedgerNamesByLocationID(locationService.GetLocationsByName(cmbLocation.Text).LocationID), true);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                RefreshPettyCashByLocation();
                Common.SetFocus(e, txtPettyCashBookCode);
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
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                        LoadPettyCashReimbursementDocument();
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
                //    AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                //    AccPettyCashReimbursement accPettyCashReimbursement = new AccPettyCashReimbursement();

                //    accPettyCashReimbursement = accPettyCashReimbursementService.GetAccPettyCashReimbursementByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                //    if (accPettyCashReimbursement == null)
                //    {
                //        Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                //        return;
                //    }
                //    else
                //    {
                //        LoadPettyCashReimbursementDocument(accPettyCashReimbursement);
                //    }
                //}
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        
        private void rdoCheque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetPayment();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbBankBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (cmbBankBookCode.Text != string.Empty)
                    { Common.SetFocus(e, dtpChequeDate); }
                    else
                    { Common.SetFocus(e, cmbBankBookName); }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbBankBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbBankBookCode.Text.Equals(string.Empty))
                { return; }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerBankBookAccountByCode(cmbBankBookCode.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        cmbBankBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        cmbBankBookName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblBankBook.Text.Trim()) + " - " + cmbBankBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbBankBookCode.Text = string.Empty;
                        cmbBankBookName.Text = string.Empty;
                        cmbBankBookCode.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbBankBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbBankBookCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbBankBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbBankBookName.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerBankBookAccountByName(cmbBankBookName.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        cmbBankBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        cmbBankBookName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblBankBook.Text.Trim()) + " - " + cmbBankBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbBankBookCode.Text = string.Empty;
                        cmbBankBookName.Text = string.Empty;
                        cmbBankBookName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rdoCheque_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rdoCheque.Checked)
                {
                    Common.SetFocus(e, cmbBankBookCode);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rdoCash_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rdoCash.Checked)
                {
                    Common.SetFocus(e, cmbCashBookCode);
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtChequeNo);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtReimbursementAmount);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCashBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (cmbCashBookName.Text != string.Empty)
                    { Common.SetFocus(e, txtReimbursementAmount); }
                    else
                    { Common.SetFocus(e, cmbCashBookName); }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCashBookCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbCashBookCode.Text.Equals(string.Empty))
                { return; }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerCashBookAccountByCode(cmbCashBookCode.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        cmbCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        cmbCashBookName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblCashBook.Text.Trim()) + " - " + cmbCashBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbCashBookCode.Text = string.Empty;
                        cmbCashBookName.Text = string.Empty;
                        cmbCashBookCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCashBookName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, cmbCashBookCode);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbCashBookName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbCashBookName.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerCashBookAccountByName(cmbCashBookName.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        cmbCashBookName.Text = existingAccLedgerAccount.LedgerCode;
                        cmbCashBookName.Text = existingAccLedgerAccount.LedgerName;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblCashBook.Text.Trim()) + " - " + cmbCashBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        cmbCashBookCode.Text = string.Empty;
                        cmbCashBookName.Text = string.Empty;
                        cmbCashBookName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void rdoCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ResetPayment();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationReimbursementNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                Common.SetAutoComplete(txtDocumentNo, accPettyCashReimbursementService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationReimbursementNo.Checked);
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
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

        private void chkAutoCompleationCashBook_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoBindRecords(cmbCashBookCode, accLedgerAccountService.GetCashBookLedgerCodes(), chkAutoCompleationPettyCash.Checked);
                Common.SetAutoBindRecords(cmbCashBookName, accLedgerAccountService.GetCashBookLedgerNames(), chkAutoCompleationPettyCash.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationBankBook_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                Common.SetAutoBindRecords(cmbBankBookCode, accLedgerAccountService.GetBankBookLedgerCodes(), "LedgerCode", "", chkAutoCompleationPettyCash.Checked);
                Common.SetAutoBindRecords(cmbBankBookName, accLedgerAccountService.GetBankBookLedgerNames(), "LedgerName", "", chkAutoCompleationPettyCash.Checked);
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
                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                DataView dvAllReferenceData = new DataView(accPettyCashReimbursementService.GetPendingPettyCashReimbursementDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashReimbursementDocument(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbLocation_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_Move(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
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

                Common.EnableTextBox(true, txtEmployeeCode, txtEmployeeName, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo);
                Common.EnableButton(true, btnDocumentDetails);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnSave, btnPause);

                dtpDocumentDate.Value = DateTime.Now;
                dtpChequeDate.Value = DateTime.Now;

                txtDocumentNo.Text = GetDocumentNo(true);

                existingAccLedgerAccount = null;
                existingEmployee = null;

                grpHeader.Enabled = true;
                grpBody.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                grpFooter.Enabled = false;

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

                // Read Company Banks
                List<AccLedgerAccount> accLedgerAccount = new List<AccLedgerAccount>();
                accLedgerAccount = accLedgerAccountService.GetBankList();
                cmbBankBookCode.DataSource = accLedgerAccount;
                cmbBankBookCode.DisplayMember = "LedgerCode";
                cmbBankBookCode.ValueMember = "AccLedgerAccountID";
                cmbBankBookCode.Refresh();

                cmbBankBookName.DataSource = accLedgerAccount;
                cmbBankBookName.DisplayMember = "LedgerName";
                cmbBankBookName.ValueMember = "AccLedgerAccountID";
                cmbBankBookName.Refresh();

                // Read Company cash books
                accLedgerAccount = accLedgerAccountService.GetCashBookBookList();
                cmbCashBookCode.DataSource = accLedgerAccount;
                cmbCashBookCode.DisplayMember = "LedgerCode";
                cmbCashBookCode.ValueMember = "AccLedgerAccountID";
                cmbCashBookCode.Refresh();

                cmbCashBookName.DataSource = accLedgerAccount;
                cmbCashBookName.DisplayMember = "LedgerName";
                cmbCashBookName.ValueMember = "AccLedgerAccountID";
                cmbCashBookName.Refresh();

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;

                ResetPayment();

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
            // Load Reimbursement Documents
            AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
            Common.SetAutoComplete(txtDocumentNo, accPettyCashReimbursementService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationReimbursementNo.Checked);
        }

        /// <summary>
        /// Pause Petty Cash Reimbursement.
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
                    //RefreshDocumentNumber();
                }
                else
                {
                    Toast.Show(this.Text + " - " + NewDocumentNo + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }
        }

        /// <summary>
        /// Save New Petty Cash Reimbursement.
        /// </summary>
        public override void Save()
        {
            if (!ValidateControls()) { return; }

            LoadPettyCashLimitBalance();

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                //if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim())) { return; }
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
                //GetSummarizeFigures(accPettyCashIOUDetailList);

                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                AccPettyCashReimbursement accPettyCashReimbursement = new AccPettyCashReimbursement();
                LocationService locationService = new LocationService();
                Location location = new Location();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                PaymentMethodService paymentMethodService = new PaymentMethodService();
                AccLedgerAccount accLedgerBankAccount = new AccLedgerAccount();
                AccLedgerAccount accLedgerCashAccount = new AccLedgerAccount();
                int paymentMethodID = 0;

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));                

                accPettyCashReimbursement = accPettyCashReimbursementService.GetAccPettyCashReimbursementByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                if (accPettyCashReimbursement == null)
                { accPettyCashReimbursement = new AccPettyCashReimbursement(); }
                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                accPettyCashReimbursement.GroupOfCompanyID = Common.GroupOfCompanyID;
                accPettyCashReimbursement.CompanyID = location.CompanyID;
                accPettyCashReimbursement.LocationID = location.LocationID;
                accPettyCashReimbursement.DocumentID = documentID;
                accPettyCashReimbursement.DocumentNo = documentNo.Trim();
                accPettyCashReimbursement.DocumentDate = Common.FormatDateTime(dtpDocumentDate.Value);

                LoadPettyCashLimitBalance();
                if (!string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());
                    accPettyCashReimbursement.EmployeeID = employee.EmployeeID;
                }
                else
                { accPettyCashReimbursement.EmployeeID = 0; }

                accPettyCashReimbursement.PettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text).AccLedgerAccountID;
                accPettyCashReimbursement.PayeeName = txtPayeeName.Text.Trim();
                accPettyCashReimbursement.Reference = txtReferenceNo.Text.Trim();
                accPettyCashReimbursement.Remark = txtRemark.Text.Trim();
                accPettyCashReimbursement.DocumentStatus = documentStatus;
                accPettyCashReimbursement.CreatedUser = Common.LoggedUser;
                accPettyCashReimbursement.CreatedDate = Common.GetSystemDateWithTime();
                accPettyCashReimbursement.ModifiedUser = Common.LoggedUser;
                accPettyCashReimbursement.ModifiedDate = Common.GetSystemDateWithTime();
                accPettyCashReimbursement.ImprestAmount = Common.ConvertStringToDecimalCurrency(txtImprestAmount.Text);
                accPettyCashReimbursement.ReimburseAmount = Common.ConvertStringToDecimalCurrency(txtReimbursementAmount.Text);
                accPettyCashReimbursement.BalanceAmount = Common.ConvertStringToDecimalCurrency(txtReimbursementAmount.Text);
                accPettyCashReimbursement.BookBalanceAmount = Common.ConvertStringToDecimalCurrency(txtBookBalance.Text);
                //accPettyCashReimbursement.AvailabledAmount = Common.ConvertStringToDecimalCurrency(balanceReimburseAmount.ToString());
                PaymentMethod paymentMethod = new PaymentMethod();
                if (rdoCash.Checked)
                { 
                    paymentMethod = paymentMethodService.GetPaymentMethodsByName(rdoCash.Text);
                    if(paymentMethod!=null)
                    {paymentMethodID = paymentMethod.PaymentMethodID;}

                    accLedgerCashAccount = accLedgerAccountService.GetAccLedgerCashBookAccountByCode(cmbCashBookCode.Text);
                    accPettyCashReimbursement.LedgerID = accLedgerCashAccount.AccLedgerAccountID;
                }
                else if (rdoCheque.Checked)
                {
                    paymentMethod = paymentMethodService.GetPaymentMethodsByName(rdoCheque.Text);
                    if (paymentMethod != null)
                    { paymentMethodID = paymentMethod.PaymentMethodID; }
                    
                    accLedgerBankAccount = accLedgerAccountService.GetAccLedgerBankBookAccountByCode(cmbBankBookCode.Text);
                    accPettyCashReimbursement.LedgerID = accLedgerBankAccount.AccLedgerAccountID;
                    
                }
                accPettyCashReimbursement.PaymentMethodID = paymentMethodID;
                accPettyCashReimbursement.ChequeDate = Common.FormatDateTime(dtpChequeDate.Value);
                accPettyCashReimbursement.ChequeNo = txtChequeNo.Text;
                //accPettyCashReimbursement.IssuedAmount                
                //accPettyCashReimbursement.UsedAmount 
                //accPettyCashReimbursement.IsUpLoad 

                return accPettyCashReimbursementService.SavePettyCashReimbursement(accPettyCashReimbursement, out newDocumentNo, this.Name);
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
                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                LocationService locationService = new LocationService();
                return accPettyCashReimbursementService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }
        
        private void GenerateReport(string documentNo, int documentStatus)
        {
            AccReportGenerator accReportGenerator = new AccReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            accReportGenerator.GenerateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        private void ResetPayment()
        {
            try
            {
                if (rdoCheque.Checked)
                {
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    grpFooter.Enabled = true;
                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo);
                    Common.EnableButton(false, btnDocumentDetails);
                    Common.EnableComboBox(false, cmbLocation);
                    Common.EnableButton(true, btnSave, btnPause);
                    Common.ClearComboBox(cmbCashBookCode, cmbCashBookName, cmbBankBookCode, cmbBankBookName);
                    Common.ClearTextBox(txtChequeNo);
                    dtpChequeDate.Value = Common.FormatDate(DateTime.Now);
                    txtReimbursementAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
                else if (rdoCash.Checked)
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;
                    grpFooter.Enabled = true;
                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName, txtDocumentNo);
                    Common.EnableButton(false, btnDocumentDetails);
                    Common.EnableComboBox(false, cmbLocation);
                    Common.EnableButton(true, btnSave, btnPause);
                    Common.ClearComboBox(cmbCashBookCode, cmbCashBookName, cmbBankBookCode, cmbBankBookName);
                    Common.ClearTextBox(txtChequeNo);
                    dtpChequeDate.Value = Common.FormatDate(DateTime.Now);
                    txtReimbursementAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                }
                else
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    grpFooter.Enabled = false;
                    dtpChequeDate.Value = Common.FormatDate(DateTime.Now);
                    txtReimbursementAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    Common.ClearComboBox(cmbCashBookCode, cmbCashBookName, cmbBankBookCode, cmbBankBookName);
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

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPettyCashReimbursementDocument()
        {
            try
            {
                AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                AccPettyCashReimbursement accPettyCashReimbursement = new AccPettyCashReimbursement();

                accPettyCashReimbursement = accPettyCashReimbursementService.GetAccPettyCashReimbursementByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                if (accPettyCashReimbursement == null)
                {
                    //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
                else
                {
                    //AccPettyCashReimbursementService accPettyCashReimbursementService = new AccPettyCashReimbursementService();
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                    AccLedgerAccount accLedgerBankAccount = new AccLedgerAccount();
                    AccLedgerAccount accLedgerCashAccount = new AccLedgerAccount();

                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();
                    PaymentMethodService paymentMethodService = new PaymentMethodService();
                    string paymentMethodName;

                    accLedgerAccount = accLedgerAccountService.GetAccLedgerAccountByID(accPettyCashReimbursement.PettyCashLedgerID);

                    txtDocumentNo.Text = accPettyCashReimbursement.DocumentNo;

                    if (accPettyCashReimbursement.EmployeeID != 0)
                    {
                        employee = employeeService.GetEmployeesByID(accPettyCashReimbursement.EmployeeID);
                        IsEmployeeExistsByID(accPettyCashReimbursement.EmployeeID);
                    }

                    dtpDocumentDate.Value = accPettyCashReimbursement.DocumentDate;
                    txtReferenceNo.Text = accPettyCashReimbursement.Reference;
                    txtRemark.Text = accPettyCashReimbursement.Remark;
                    cmbLocation.SelectedValue = accPettyCashReimbursement.LocationID;
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    txtEmployeeCode.Text = employee.EmployeeCode;
                    txtEmployeeName.Text = employee.EmployeeName;
                    txtPayeeName.Text = accPettyCashReimbursement.PayeeName;
                    paymentMethodName = paymentMethodService.GetPaymentMethodsByID(accPettyCashReimbursement.PaymentMethodID).PaymentMethodName;
                    if (paymentMethodName.Equals(rdoCash.Text.ToUpper()))
                    {
                        rdoCash.Checked = true;
                        accLedgerCashAccount = accLedgerAccountService.GetAccLedgerCashBookAccountByID(accPettyCashReimbursement.LedgerID);
                        cmbCashBookCode.Text = accLedgerCashAccount.LedgerCode;
                        cmbCashBookName.Text = accLedgerCashAccount.LedgerName;
                        Common.ClearComboBox(cmbBankBookCode, cmbBankBookName);
                    }
                    if (paymentMethodName.Equals(rdoCheque.Text.ToUpper()))
                    {
                        rdoCheque.Checked = true;
                        accLedgerBankAccount = accLedgerAccountService.GetAccLedgerBankBookAccountByID(accPettyCashReimbursement.LedgerID);
                        cmbBankBookCode.Text = accLedgerBankAccount.LedgerCode;
                        cmbBankBookName.Text = accLedgerBankAccount.LedgerName;
                        txtChequeNo.Text = accPettyCashReimbursement.ChequeNo;
                        dtpChequeDate.Value = Common.FormatDate(accPettyCashReimbursement.ChequeDate);
                        Common.ClearComboBox(cmbCashBookCode, cmbCashBookName);
                    }
                    txtReimbursementAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashReimbursement.ReimburseAmount);

                    Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName);
                    Common.EnableComboBox(false, cmbLocation);

                    if (accPettyCashReimbursement.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        grpFooter.Enabled = true;
                        //EnableLine(true);
                        Common.EnableButton(true, btnSave, btnPause);
                        Common.EnableButton(false, btnDocumentDetails);

                        //? Read Imprest Amount & Update Display Field
                        LoadPettyCashLimitBalance();
                    }
                    else
                    {
                        grpHeader.Enabled = false;
                        grpFooter.Enabled = false;
                        Common.EnableButton(false, btnSave, btnPause);
                    }
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

        /// <summary>
        /// Load Petty Cash Balance
        /// </summary>
        private bool LoadPettyCashLimitBalance()
        {
            try
            {
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                AccPettyCashMaster accPettyCashMaster = new AccPettyCashMaster();
                AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
                AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                LocationService locationService = new LocationService();
                Location location = new Location();
                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();

                if (string.IsNullOrEmpty(txtPettyCashBookCode.Text))
                {
                    txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    return false;
                }
                else
                {
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                    location = locationService.GetLocationsByName(cmbLocation.Text);
                    accPettyCashMaster = accPettyCashMasterService.GetAccPettyCashMasterByLedgerID(accLedgerAccount.AccLedgerAccountID, location.LocationID);
                    accPettyCashImprestDetail = accPettyCashImprestService.GetAccPettyCashImprestByPettyCashBookID(accLedgerAccount.AccLedgerAccountID, location.LocationID);
                    decimal pettyCashBookBalance = accGlTransactionService.GetAccLedgerAccountBalanceByAccountID(accLedgerAccount.AccLedgerAccountID, Common.FormatDate(dtpDocumentDate.Value), false);
                    txtBookBalance.Text = Common.ConvertDecimalToStringCurrency(pettyCashBookBalance);

                    if (accPettyCashMaster != null && accPettyCashImprestDetail != null)
                    {
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount - accPettyCashImprestDetail.BalanceAmount);
                        balanceReimburseAmount = accPettyCashImprestDetail.BalanceAmount;
                    }
                    else if (accPettyCashMaster != null)
                    {
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount);
                    }
                    else
                    {
                        Toast.Show("Please allocate the Cash Limit. - " + txtPettyCashBookCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtPettyCashBookCode.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
            return true;
        }

        private bool ValidateControls()
        {
            bool isValidated = true;
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            AccPettyCashReimbursementValidator accPettyCashReimbursementValidator = new AccPettyCashReimbursementValidator();
            LocationService locationService = new LocationService();
            PaymentMethodService paymentMethodService = new PaymentMethodService();
            
            if (rdoCash.Checked)
            {
                if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, cmbLocation, txtPettyCashBookCode, cmbCashBookCode, txtReimbursementAmount))
                {
                    isValidated = false;
                    return isValidated;
                }
                else
                {
                    PaymentMethod paymentMethod = paymentMethodService.GetPaymentMethodsByName(rdoCash.Text);
                    if (paymentMethod == null)
                    {
                        Toast.Show("Record(s)", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPaymentMethod.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(rdoCash.Text.Trim()));
                        isValidated = false;
                        return isValidated;
                    }
                }
            }
            else if (rdoCheque.Checked)
            {
                if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, cmbLocation, txtPettyCashBookCode, cmbBankBookCode, txtChequeNo, txtReimbursementAmount))
                { 
                    isValidated = false;
                    return isValidated;
                }
                else if (!Validater.ValidateCharacterLength(errorProvider, Validater.ValidateType.Length, txtChequeNo, Common.CharacterLengthChequeNo))
                { 
                    isValidated = false;
                    return isValidated;
                }
                else if (!Validater.ValidateDate(errorProvider, Validater.ValidateType.CompareDate, dtpChequeDate, dtpDocumentDate.Value, dtpChequeDate.Value))
                { return false; }
                else
                {
                    PaymentMethod paymentMethod = paymentMethodService.GetPaymentMethodsByName(rdoCheque.Text);
                    if (paymentMethod == null)
                    {
                        Toast.Show("Record(s)", Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPaymentMethod.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(rdoCheque.Text.Trim()));
                        isValidated = false;
                        return isValidated;
                    }
                }
            }
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtReimbursementAmount))
            { return false; }

            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashReimbursementValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - " + cmbLocation.Text.Trim());
                isValidated = false;
                return isValidated;
            }
            if (!accPettyCashReimbursementValidator.ValidatePettyCashReimburseAmountByLedgerID(pettyCashLedgerID, locationID, Common.ConvertStringToDecimalCurrency(txtReimbursementAmount.Text)))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + ", " + Common.ConvertStringToDisplayFormat(lblReimbursementAmount.Text.Trim()) + " - " + txtReimbursementAmount.Text.Trim(), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
                return isValidated;
            }
            if (!accPettyCashReimbursementValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID, Common.ConvertStringToDecimalCurrency(txtReimbursementAmount.Text)))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + ", " + Common.ConvertStringToDisplayFormat(lblReimbursementAmount.Text.Trim()) + " - " + txtReimbursementAmount.Text.Trim(), Toast.messageType.Information, Toast.messageAction.Invalid);
                isValidated = false;
                return isValidated;
            }
            if (!accPettyCashReimbursementValidator.ValidateExistsPausedReimburseRecord(txtDocumentNo.Text.Trim(), pettyCashLedgerID, locationID))
            {
                Toast.Show(this.Text, Toast.messageType.Information, Toast.messageAction.ExistsForSelected, Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim());
                isValidated = false;
                return isValidated;
            }
            return isValidated;
        }

        #region Validate Logics

        /// <summary>
        /// Validate Parent property of the Child
        /// </summary>
        /// <returns></returns>
        private bool IsValidateControls()
        {
            //InvGiftVoucherMasterValidator invGiftVoucherMasterValidator = new InvGiftVoucherMasterValidator();
            bool isValidated = false;
            //validate reimbursement value with limit
            //if (!invGiftVoucherMasterValidator.ValidateLength(Common.ConvertStringToInt(txtLength.Text.Trim()), string.Concat(txtPrefix.Text.Trim(), (Common.ConvertStringToInt(txtStartingNo.Text.Trim()) + Common.ConvertStringToInt(txtNoOfVouchers.Text.Trim())))))
            //{
            //    Toast.Show(string.Concat(Common.ConvertStringToDisplayFormat(lblSerialLength.Text.ToString()), " - ", txtLength.Text.Trim()), Toast.messageType.Information, Toast.messageAction.Length);
            //    isValidated = false;
            //}
            //else
            {
                isValidated = true;
            }

            return isValidated;
        }

        #endregion

        
        #endregion
        
    }
}
