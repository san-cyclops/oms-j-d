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
    public partial class FrmPettyCashPayment : UI.Windows.FrmBaseTransactionForm
    {
        List<AccPettyCashPaymentDetail> accPettyCashPaymentDetailList = new List<AccPettyCashPaymentDetail>();
        List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList = new List<AccPettyCashVoucherHeader>();
        List<AccPettyCashVoucherHeader> accPettyCashVoucherApprovedHeaderList = new List<AccPettyCashVoucherHeader>();

        private AccLedgerAccount existingAccLedgerAccount;
        private Employee existingEmployee;

        int documentID = 6;

        public FrmPettyCashPayment()
        {
            InitializeComponent();
        }

        #region Form Events
        private void btnConfirmVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVoucher.RowCount > 0)
                {
                    if ((Toast.Show("", Toast.messageType.Question, Toast.messageAction.ConfirmTransaction).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    ConfirmPettyCashVoucher();

                    Toast.Show("Selected Voucher(s) ", Toast.messageType.Information, Toast.messageAction.ConfirmTransaction);
                    //Common.EnableButton(true, btnPause, btnSave);
                    //Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtPrefix, txtLength, txtStartingNo);
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCancelVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVoucher.RowCount > 0)
                {
                    if ((Toast.Show("", Toast.messageType.Question, Toast.messageAction.ConfirmTransaction).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    CancelPettyCashVoucher();

                    Toast.Show("Selected Voucher(s) ", Toast.messageType.Information, Toast.messageAction.ConfirmTransaction);
                    //Common.EnableButton(true, btnPause, btnSave);
                    //Common.EnableTextBox(false, txtBookCode, txtBookName, txtGroupCode, txtGroupName, txtVoucherValue, txtNoOfVouchersOnBook, txtNoOfVouchers, txtPrefix, txtLength, txtStartingNo);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                if (string.IsNullOrEmpty(txtPettyCashBookCode.Text.Trim()))
                { return; }
                else
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text.Trim());
                    if (existingAccLedgerAccount != null)
                    {
                        txtPettyCashBookCode.Text = existingAccLedgerAccount.LedgerCode;
                        txtPettyCashBookName.Text = existingAccLedgerAccount.LedgerName;
                        bool isValid = LoadPettyCashBookSummary(existingAccLedgerAccount.AccLedgerAccountID);

                        if (isValid)
                        {LoadPettyVoucherDetail();}
                        else
                        { return; }

                        //LoadPettyCashBalance();
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
                        LoadPettyCashBookSummary(existingAccLedgerAccount.AccLedgerAccountID);
                        LoadPettyCashBalance();
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
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
                        LoadPettyVoucherDetail();
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
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblEmployee.Text.Trim()) + " - " + txtEmployeeCode.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
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
                Common.SetFocus(e, cmbCostCentre);
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
                Common.SetFocus(e, txtReferenceNo);
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
                Common.SetFocus(e, txtPettyCashBookCode);
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

        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                DataView dvAllReferenceData = new DataView(accPettyCashPaymentService.GetPendingPettyCashPaymentDocuments());
                if (dvAllReferenceData.Count != 0)
                {
                    LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), "Pending Documents", string.Empty, txtDocumentNo);
                    if (!txtDocumentNo.Text.Trim().Equals(string.Empty))
                    { LoadPettyCashPaymentDocument(); }
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
                    //if (!txtPurchaseOrderNo.Text.Trim().Equals(string.Empty))
                    //    RecallDocument(txtPurchaseOrderNo.Text.Trim());
                    //else
                    //{
                    //    txtPurchaseOrderNo.Text = GetDocumentNo(true);
                    //    txtSupplierCode.Focus();
                    //}
                    txtDocumentNo_Validated(this, e);
                }
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
                    //AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                    //AccPettyCashPaymentHeader accPettyCashPaymentHeader = new AccPettyCashPaymentHeader();

                    //accPettyCashPaymentHeader = accPettyCashPaymentService.GetAccPettyCashPaymentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                    //if (accPettyCashPaymentHeader == null)
                    //{
                    //    //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    //    return;
                    //}
                    //else
                    //{
                        LoadPettyCashPaymentDocument();
                    //}
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dgvConfirmedVoucher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvConfirmedVoucher.CurrentCell = dgvConfirmedVoucher.Rows[dgvConfirmedVoucher.CurrentRow.Index].Cells["Status"];

                if (int.Equals(e.ColumnIndex, 0))
                {
                    if (dgvConfirmedVoucher.CurrentCell != null && dgvConfirmedVoucher.CurrentCell.RowIndex >= 0)
                    {
                        DataGridViewCheckBoxCell chSts = new DataGridViewCheckBoxCell();
                        chSts = (DataGridViewCheckBoxCell)dgvConfirmedVoucher.Rows[dgvConfirmedVoucher.CurrentRow.Index].Cells["Status"];

                        if (chSts.Value == null)
                            chSts.Value = false;

                        switch (chSts.Value.ToString())
                        {
                            case "True":
                                accPettyCashVoucherApprovedHeaderList[dgvConfirmedVoucher.CurrentRow.Index].Status = false;
                                break;
                            case "False":
                                accPettyCashVoucherApprovedHeaderList[dgvConfirmedVoucher.CurrentRow.Index].Status = true;
                                break;
                        }

                        if (dgvConfirmedVoucher["AccPettyCashVoucherHeaderID", dgvConfirmedVoucher.CurrentCell.RowIndex].Value != null)
                        {
                            GetSummarizeFigures(accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1 && v.Status == true).ToList());
                        }
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                Common.SetAutoComplete(txtDocumentNo, accPettyCashPaymentService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
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

                Common.SetDataGridviewColumnsReadOnly(true, dgvVoucher, dgvVoucher.Columns[1], dgvVoucher.Columns[2], dgvVoucher.Columns[3], dgvVoucher.Columns[4], dgvVoucher.Columns[5], dgvVoucher.Columns[6], dgvVoucher.Columns[7]);
                Common.SetDataGridviewColumnsReadOnly(true, dgvConfirmedVoucher, dgvConfirmedVoucher.Columns[1], dgvConfirmedVoucher.Columns[2], dgvConfirmedVoucher.Columns[3], dgvConfirmedVoucher.Columns[4], dgvConfirmedVoucher.Columns[5], dgvConfirmedVoucher.Columns[6], dgvConfirmedVoucher.Columns[7]);

                Common.EnableTextBox(true, txtEmployeeCode, txtEmployeeName, txtDocumentNo, txtPettyCashBookCode, txtPettyCashBookName);
                Common.EnableButton(true, btnDocumentDetails);
                Common.EnableComboBox(true, cmbLocation);
                Common.EnableButton(false, btnConfirmVoucher, btnCancelVoucher, btnSave, btnPause);

                dtpDocumentDate.Value = DateTime.Now;

                txtDocumentNo.Text = GetDocumentNo(true);

                existingAccLedgerAccount = null;
                existingEmployee = null;

                accPettyCashPaymentDetailList = null;
                accPettyCashVoucherApprovedHeaderList = null;

                grpBody.Enabled = false;
                grpFooter.Enabled = false;

                this.ActiveControl = txtPettyCashBookCode;
                txtPettyCashBookCode.Focus();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        public void RefreshDocumentNumbers()
        {
            AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
            Common.SetAutoComplete(txtDocumentNo, accPettyCashPaymentService.GetAllDocumentNumbers(documentID, Convert.ToInt32(cmbLocation.SelectedValue)), chkAutoCompleationDocumentNo.Checked);
        }
        public override void FormLoad()
        {
            try
            {
                dgvVoucher.AutoGenerateColumns = false;
                dgvConfirmedVoucher.AutoGenerateColumns = false;

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

        /// <summary>
        /// Pause Petty Cash IOU.
        /// </summary>
        public override void Pause()
        {
            if (!ValidateControls())
            { return; }

            if (accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1 && v.Status == true).Count() > 0)
            {
                if (!IsValidateExistsPausedPaymentRecord()) { return; }

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
            else
            {
                Toast.Show(this.Text + " selected record(s) ", Toast.messageType.Warning, Toast.messageAction.NotFound);
                return;
            }
        }

        /// <summary>
        /// Save New Petty Cash Payment
        /// </summary>
        public override void Save()
        {
            if (!ValidateControls()) { return; }
            if (!IsValidateExistsValidReimburseAmount()) { return; }
            if (!IsEmployeeExistsByCode(txtEmployeeCode.Text.Trim())) { return; }
            if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }

            if (accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1 && v.Status == true).Count() > 0)
            {
                if (!IsValidateExistsPausedPaymentRecord()) { return; }

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
            else
            {
                Toast.Show(this.Text + " selected record(s) ", Toast.messageType.Warning, Toast.messageAction.NotFound);
                return;
            }
        }

        public override void ClearForm()
        {
            dgvVoucher.DataSource = null;
            dgvVoucher.Refresh();
            dgvConfirmedVoucher.DataSource = null;
            dgvConfirmedVoucher.Refresh();
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
        /// <param name="newDocumentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo, out string newDocumentNo)
        {
            try
            {
                GetSummarizeFigures(accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1 && v.Status == true).ToList());

                AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                AccPettyCashPaymentHeader accPettyCashPaymentHeader = new AccPettyCashPaymentHeader();
                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                accPettyCashPaymentHeader = accPettyCashPaymentService.GetAccPettyCashPaymentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), location.LocationID);
                if (accPettyCashPaymentHeader == null)
                { accPettyCashPaymentHeader = new AccPettyCashPaymentHeader(); }
                //if (documentStatus.Equals(1)) // update paused document
                //{
                //    documentNo = GetDocumentNo(false);
                //    txtDocumentNo.Text = documentNo;
                //}

                accPettyCashPaymentHeader = FillSelectedVouchers(documentStatus, documentNo, accPettyCashPaymentHeader);
            
                return accPettyCashPaymentService.SavePettyCashPayment(accPettyCashPaymentHeader, accPettyCashPaymentDetailList, out newDocumentNo, this.Name);
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
                AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                LocationService locationService = new LocationService();
                return accPettyCashPaymentService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                return string.Empty;
            }
        }

        private void GetSummarizeFigures(List<AccPettyCashVoucherHeader> listItem)
        {
            decimal issuedTotalAmount = 0;
            try
            {
                issuedTotalAmount = listItem.GetSummaryAmount(x => x.BalanceAmount);

                txtTotalSettlement.Text = Common.ConvertDecimalToStringCurrency(issuedTotalAmount);
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }
        
        private void GetSummaryFigures(List<AccPettyCashVoucherHeader> listItem)
        {
            decimal voucherTotalAmount = 0, iouTotalAmount = 0, pendingTotalAmount = 0;
            try
            {
                voucherTotalAmount = listItem.GetSummaryAmount(x => x.Amount);
                iouTotalAmount = listItem.GetSummaryAmount(x => x.IOUAmount);
                pendingTotalAmount = listItem.GetSummaryAmount(x => x.IOUAmount);

                txtTotalVoucherAmount.Text = Common.ConvertDecimalToStringCurrency(voucherTotalAmount);
                txtTotalIOUAmount.Text = Common.ConvertDecimalToStringCurrency(iouTotalAmount);
                txtTotalPendingAmount.Text = Common.ConvertDecimalToStringCurrency(pendingTotalAmount);
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

        /// <summary>
        /// Load paused document
        /// </summary>
        private void LoadPettyCashPaymentDocument()
        {
            try
            {
                AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                AccPettyCashPaymentHeader accPettyCashPaymentHeader = new AccPettyCashPaymentHeader();

                accPettyCashPaymentHeader = accPettyCashPaymentService.GetAccPettyCashPaymentHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));

                if (accPettyCashPaymentHeader == null)
                {
                    //Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
                else
                {
                    accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByID(accPettyCashPaymentHeader.PettyCashLedgerID);
                    txtDocumentNo.Text = accPettyCashPaymentHeader.DocumentNo;
                    IsEmployeeExistsByID(accPettyCashPaymentHeader.EmployeeID);
                    txtRemark.Text = accPettyCashPaymentHeader.Remark;
                    dtpDocumentDate.Value = accPettyCashPaymentHeader.DocumentDate;

                    txtReferenceNo.Text = accPettyCashPaymentHeader.Reference;
                    cmbLocation.SelectedValue = accPettyCashPaymentHeader.LocationID;
                    cmbCostCentre.SelectedValue = accPettyCashPaymentHeader.CostCentreID;
                    txtPayeeName.Text = accPettyCashPaymentHeader.PayeeName;
                    txtPettyCashBookCode.Text = accLedgerAccount.LedgerCode;
                    txtPettyCashBookName.Text = accLedgerAccount.LedgerName;
                    txtTotalSettlement.Text = Common.ConvertDecimalToStringCurrency(accPettyCashPaymentHeader.Amount);

                    LoadPettyCashBalance();
                    LoadPettyCashBookSummary(accPettyCashPaymentHeader.PettyCashLedgerID);

                    dgvConfirmedVoucher.DataSource = null;
                    accPettyCashVoucherApprovedHeaderList = accPettyCashPaymentService.GetAllPettyCashPaymentDetail(accPettyCashPaymentHeader);
                    dgvConfirmedVoucher.DataSource = accPettyCashVoucherApprovedHeaderList;
                    dgvConfirmedVoucher.Refresh();

                    GetSummaryFigures(accPettyCashVoucherApprovedHeaderList);

                    Common.EnableTextBox(false, txtDocumentNo, txtEmployeeCode, txtEmployeeName, txtPettyCashBookCode, txtPettyCashBookName);
                    Common.EnableComboBox(false, cmbLocation);

                    if (accPettyCashPaymentHeader.DocumentStatus.Equals(0))
                    {
                        grpBody.Enabled = true;
                        grpFooter.Enabled = true;
                        //EnableLine(true);
                        Common.EnableButton(true, btnSave, btnPause);
                        Common.EnableButton(false, btnDocumentDetails);
                    }
                    else
                    {
                        grpBody.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private bool LoadPettyCashBookSummary(long pettyCashBookID)
        {
            try
            {
                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                AccPettyCashMaster accPettyCashMaster = new AccPettyCashMaster();
                AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
                AccPettyCashImprestDetail existingAccPettyCashImprestDetail = new AccPettyCashImprestDetail();
                LocationService locationService = new LocationService();
                Location location = new Location();

                location = locationService.GetLocationsByName(cmbLocation.Text.Trim());
                if (pettyCashBookID == 0)
                { return false; }
                
                if (string.IsNullOrEmpty(txtPettyCashBookCode.Text))
                {
                    txtPettyCashLimit.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtUsedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    txtIssuedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    return false;
                }
                else
                {
                    location = locationService.GetLocationsByName(cmbLocation.Text);
                    accPettyCashMaster = accPettyCashMasterService.GetAccPettyCashMasterByLedgerID(pettyCashBookID, location.LocationID);
                    existingAccPettyCashImprestDetail = accPettyCashImprestService.GetAccPettyCashImprestByPettyCashBookID(pettyCashBookID, location.LocationID);

                    if (accPettyCashMaster != null && existingAccPettyCashImprestDetail != null)
                    {
                        txtPettyCashLimit.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount);
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount - existingAccPettyCashImprestDetail.BalanceAmount);
                        txtCashAvailable.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount - existingAccPettyCashImprestDetail.UsedAmount);
                        txtUsedAmount.Text = Common.ConvertDecimalToStringCurrency(existingAccPettyCashImprestDetail.UsedAmount);
                        txtPettyCashBalance.Text = Common.ConvertDecimalToStringCurrency(existingAccPettyCashImprestDetail.BalanceAmount);
                        txtIssuedAmount.Text = Common.ConvertDecimalToStringCurrency(existingAccPettyCashImprestDetail.IssuedAmount);
                    }
                    else if (accPettyCashMaster != null)
                    {
                        txtPettyCashLimit.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount);
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashMaster.Amount);
                        txtUsedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtPettyCashBalance.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtIssuedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                    }
                    else
                    {
                        Toast.Show("Please allocate the Cash Limit. - " + txtPettyCashBookCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtPettyCashLimit.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtImprestAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtUsedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtIssuedAmount.Text = Common.ConvertDecimalToStringCurrency(0);
                        txtPettyCashBookCode.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return true;
        }

        /// <summary>
        /// Load voucher documents
        /// </summary>
        private void LoadPettyVoucherDetail()
        {
            try
            {
                AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                AccLedgerAccount accLedgerAccount = new AccLedgerAccount();
                EmployeeService employeeService = new EmployeeService();
                Employee employee = new Employee();
                LocationService locationService = new LocationService();
                Location location = new Location();

                long ledgerID, employeeID;
                int locationID;

                accLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtPettyCashBookCode.Text);
                if (accLedgerAccount == null)
                {ledgerID = 0;}
                else
                {ledgerID = accLedgerAccount.AccLedgerAccountID;}
                employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text);
                if (employee==null)
                {employeeID = 0;}
                else
                {employeeID = employee.EmployeeID;}
                location = locationService.GetLocationsByName(cmbLocation.Text);
                if (location==null)
                {locationID = 0;}
                else
                {locationID = location.LocationID;}
                 
                //txtDocumentNo.Text = accPettyCashIOUHeader.DocumentNo;
                //IsEmployeeExistsByID(accPettyCashIOUHeader.EmployeeID);
                //txtRemark.Text = accPettyCashIOUHeader.Remark;
                //dtpDocumentDate.Value = accPettyCashIOUHeader.DocumentDate;

                //dtpDocumentDate.Value = accPettyCashIOUHeader.DocumentDate;
                //txtReferenceNo.Text = accPettyCashIOUHeader.Reference;
                //cmbLocation.SelectedValue = accPettyCashIOUHeader.LocationID;

                //txtExpenseTotalAmount.Text = Common.ConvertDecimalToStringCurrency(accPettyCashIOUHeader.Amount);

                dgvVoucher.DataSource = null;
                accPettyCashVoucherHeaderList = accPettyCashVoucherService.GetAllPettyCashVoucherHeader(ledgerID, employeeID, locationID);
                dgvVoucher.DataSource = accPettyCashVoucherHeaderList;
                dgvVoucher.Refresh();

                dgvConfirmedVoucher.DataSource = null;
                accPettyCashVoucherApprovedHeaderList = accPettyCashVoucherService.GetAllPettyCashApprovedVoucherHeader(ledgerID, employeeID, locationID);
                dgvConfirmedVoucher.DataSource = accPettyCashVoucherApprovedHeaderList;
                dgvConfirmedVoucher.Refresh();

                GetSummaryFigures(accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1).ToList());
            
                if(!txtPettyCashBookCode.Text.Equals(string.Empty))
                {Common.EnableTextBox(false, txtPettyCashBookCode, txtPettyCashBookName);}
                if (!txtEmployeeCode.Text.Equals(string.Empty))
                {Common.EnableTextBox(false, txtEmployeeCode, txtEmployeeName);}
           
                Common.SetDataGridviewColumnsReadOnly(false, dgvVoucher, dgvVoucher.Columns[0]);
                Common.SetDataGridviewColumnsReadOnly(false, dgvConfirmedVoucher, dgvConfirmedVoucher.Columns[0]);

                txtDocumentNo.Text = GetDocumentNo(true);
                txtDocumentNo.Enabled = false;

                if (accPettyCashVoucherHeaderList.Count>0)
                {
                    grpBody.Enabled = true;
                    Common.EnableButton(true, btnConfirmVoucher, btnCancelVoucher);
                }
                if (accPettyCashVoucherApprovedHeaderList.Count > 0)
                {
                    grpBody.Enabled = true;
                    grpFooter.Enabled = true;
                    //EnableLine(true);
                    Common.EnableButton(true, btnSave, btnPause);
                    Common.EnableButton(false, btnDocumentDetails);
                }
                else
                {

                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        /// <summary>
        /// Load Petty Cash Balance
        /// </summary>
        private void LoadPettyCashBalance()
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
                    Toast.Show("Please allocate the Cash Limit. - " + txtPettyCashBookCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private AccPettyCashPaymentHeader FillSelectedVouchers(int documentStatus, string documentNo, AccPettyCashPaymentHeader accPettyCashPaymentHeader)
        {
            try
            {
                if (accPettyCashVoucherApprovedHeaderList.Where(v=>v.Status==true).Count() > 0)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccPettyCashPaymentDetail accPettyCashPaymentDetail;
                    LocationService locationService = new LocationService();
                    CostCentreService costCentreService = new CostCentreService();
                    EmployeeService employeeService = new EmployeeService();
                    Location location = new Location();
                    location = locationService.GetLocationsByName(cmbLocation.Text);

                    if (accPettyCashPaymentDetailList == null)
                    { accPettyCashPaymentDetailList = new List<AccPettyCashPaymentDetail>(); }

                    accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID = accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID;
                    accPettyCashPaymentHeader.PettyCashPaymentHeaderID = accPettyCashPaymentHeader.PettyCashPaymentHeaderID;
                    accPettyCashPaymentHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
                    accPettyCashPaymentHeader.CompanyID = location.CompanyID;
                    accPettyCashPaymentHeader.LocationID = location.LocationID;
                    accPettyCashPaymentHeader.CostCentreID = costCentreService.GetCostCentresByName(cmbCostCentre.Text).CostCentreID;
                    accPettyCashPaymentHeader.DocumentID = documentID;
                    accPettyCashPaymentHeader.DocumentNo = documentNo.Trim();
                    accPettyCashPaymentHeader.PettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text).AccLedgerAccountID;
                    accPettyCashPaymentHeader.EmployeeID = employeeService.GetEmployeesByCode(txtEmployeeCode.Text).EmployeeID;
                    accPettyCashPaymentHeader.Amount = Common.ConvertStringToDecimalCurrency(txtTotalSettlement.Text.Trim());
                    accPettyCashPaymentHeader.DocumentDate = Common.FormatDateTime(dtpDocumentDate.Value);
                    accPettyCashPaymentHeader.PaymentDate = Common.FormatDateTime(dtpDocumentDate.Value);
                    accPettyCashPaymentHeader.Reference = txtReferenceNo.Text.Trim();
                    accPettyCashPaymentHeader.Remark = txtRemark.Text.Trim();
                    accPettyCashPaymentHeader.PayeeName = txtPayeeName.Text.Trim();
                    accPettyCashPaymentHeader.PaymentStatus = (documentStatus == 1 ? 2 : 0);
                    accPettyCashPaymentHeader.DocumentStatus = documentStatus;
                    accPettyCashPaymentHeader.CreatedUser = Common.LoggedUser;
                    accPettyCashPaymentHeader.CreatedDate = DateTime.UtcNow;
                    accPettyCashPaymentHeader.ModifiedUser = Common.LoggedUser;
                    accPettyCashPaymentHeader.ModifiedDate = DateTime.UtcNow;

                    foreach (AccPettyCashVoucherHeader accPettyCashVoucherHeader in accPettyCashVoucherApprovedHeaderList.Where(v => v.Status == true))
                    {
                        accPettyCashPaymentDetail = new AccPettyCashPaymentDetail();
                        accPettyCashPaymentDetail.AccPettyCashPaymentDetailID = 0;
                        accPettyCashPaymentDetail.PettyCashPaymentDetailID = 0;
                        accPettyCashPaymentDetail.AccPettyCashPaymentHeaderID = accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID;
                        accPettyCashPaymentDetail.PettyCashPaymentHeaderID = accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID;
                        accPettyCashPaymentDetail.GroupOfCompanyID = accPettyCashPaymentHeader.GroupOfCompanyID;
                        accPettyCashPaymentDetail.CompanyID = accPettyCashPaymentHeader.CompanyID;
                        accPettyCashPaymentDetail.LocationID = accPettyCashPaymentHeader.LocationID;
                        accPettyCashPaymentDetail.CostCentreID = accPettyCashPaymentHeader.CostCentreID;
                        accPettyCashPaymentDetail.DocumentID = accPettyCashPaymentHeader.DocumentID;
                        accPettyCashPaymentDetail.DocumentNo = accPettyCashPaymentHeader.DocumentNo;
                        accPettyCashPaymentDetail.LedgerID = accPettyCashPaymentHeader.PettyCashLedgerID;
                        accPettyCashPaymentDetail.Amount = accPettyCashVoucherHeader.BalanceAmount;
                        accPettyCashPaymentDetail.DocumentDate = accPettyCashPaymentHeader.DocumentDate;
                        accPettyCashPaymentDetail.PaymentDate = accPettyCashPaymentHeader.PaymentDate;
                        accPettyCashPaymentDetail.ChequeDate = accPettyCashPaymentHeader.PaymentDate;
                        accPettyCashPaymentDetail.ReferenceDocumentID = accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID;
                        accPettyCashPaymentDetail.ReferenceDocumentDocumentID = accPettyCashVoucherHeader.DocumentID;
                        accPettyCashPaymentDetail.ReferenceLocationID = accPettyCashVoucherHeader.LocationID;
                        accPettyCashPaymentDetail.PaymentStatus = accPettyCashPaymentHeader.PaymentStatus;
                        accPettyCashPaymentDetail.DocumentStatus = accPettyCashPaymentHeader.DocumentStatus;
                        accPettyCashPaymentDetail.CreatedUser = accPettyCashPaymentHeader.CreatedUser;
                        accPettyCashPaymentDetail.CreatedDate = accPettyCashPaymentHeader.CreatedDate;
                        accPettyCashPaymentDetail.ModifiedUser = accPettyCashPaymentHeader.ModifiedUser;
                        accPettyCashPaymentDetail.ModifiedDate = accPettyCashPaymentHeader.ModifiedDate;
                        accPettyCashPaymentDetail.IsUpLoad = accPettyCashPaymentHeader.IsUpLoad;
                        accPettyCashPaymentDetailList.Add(accPettyCashPaymentDetail);
                    }

                    accPettyCashPaymentHeader.AccPettyCashPaymentDetails = accPettyCashPaymentDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return accPettyCashPaymentHeader;
        }

        private void ConfirmPettyCashVoucher()
        {
            try
            {
                if (dgvVoucher.Rows.Count > 0)
                {
                    AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                    accPettyCashVoucherService.ConfirmAccPettyCashVoucherHeader(accPettyCashVoucherHeaderList);
                }

                LoadPettyVoucherDetail();
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

        private void CancelPettyCashVoucher()
        {
            try
            {
                if (dgvVoucher.Rows.Count > 0)
                {
                    AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                    accPettyCashVoucherService.CancelAccPettyCashVoucherHeader(accPettyCashVoucherHeaderList);
                }

                LoadPettyVoucherDetail();
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
            AccPettyCashPaymentValidator accPettyCashPaymentValidator = new AccPettyCashPaymentValidator();
            LocationService locationService = new LocationService();

            if (!Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtEmployeeCode, txtPettyCashBookCode, cmbLocation, cmbCostCentre))
            { return false; }
            else if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Zero, txtPettyCashBalance))
            { return false; }

            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashPaymentValidator.ValidateExistsPettyCashLedgerByLocationID(pettyCashLedgerID, locationID))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - " + cmbLocation.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + Common.ConvertStringToDisplayFormat(txtPettyCashBookCode.Text.Trim()));
                isValid = false;
            }

            return isValid;
        }

        private bool IsValidateExistsValidReimburseAmount()
        {
            AccPettyCashPaymentValidator accPettyCashPaymentValidator = new AccPettyCashPaymentValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = false;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashPaymentValidator.ValidateExistsValidReimburseAmount(pettyCashLedgerID, locationID, Common.ConvertStringToDecimalCurrency(txtTotalSettlement.Text)))
            {
                Toast.Show("Reimbursement amount", Toast.messageType.Information, Toast.messageAction.Invalid, "selected - " + Common.ConvertStringToDisplayFormat(lblPettyCashBook.Text.Trim()) + " - " + txtPettyCashBookCode.Text.Trim() + " - " + txtPettyCashBookName.Text.Trim() + ", - " + cmbLocation.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        private bool IsValidateExistsPausedPaymentRecord()
        {
            AccPettyCashPaymentValidator accPettyCashPaymentValidator = new AccPettyCashPaymentValidator();
            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
            LocationService locationService = new LocationService();
            bool isValidated = true;
            long pettyCashLedgerID = accLedgerAccountService.GetAccLedgerAccountByCode(txtPettyCashBookCode.Text.Trim()).AccLedgerAccountID;
            int locationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;

            if (!accPettyCashPaymentValidator.ValidateExistsPausedPaymentRecord(txtDocumentNo.Text.Trim(), pettyCashLedgerID, locationID, accPettyCashVoucherApprovedHeaderList.Where(v => v.VoucherStatus == 1 && v.Status == true).ToList()))
            {
                Toast.Show(this.Text + " paused document", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, " selected settlement(s).");
                isValidated = false;
                return isValidated;
            }
            return isValidated;
        }
        #endregion

        

        
    }
}
