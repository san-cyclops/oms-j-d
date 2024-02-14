using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Report.Inventory;
using Service;
using Utility;
using System.IO;
using System.Reflection;
using System.Linq;
using Microsoft.Office.Core;

namespace UI.Windows
{
    public partial class FrmCashier : UI.Windows.FrmBaseMasterForm
    {
        /// <summary>
        /// Nuwan
        /// </summary>
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
        CashierPermission existingCashierPermission = new CashierPermission();
        //List<CashierPermission> cashierPermissionList = new List<CashierPermission>();
        List<CashierPermission> existingCashierPermissionList = new List<CashierPermission>();
        List<Location> existingLocationList = new List<Location>();
        List<Location> unselectLocationList = new List<Location>(); 

        public FrmCashier()
        {
            InitializeComponent();
        }

        public void GetInitialFunctionList()
        {
            CashierPermissionService cashierPermissionService = new CashierPermissionService();
            existingCashierPermissionList = cashierPermissionService.GetAllCashierFunctionsForCashierPermission();
            dgvCashier.DataSource = existingCashierPermissionList;
            dgvCashier.Refresh();
        }

        public void EnableLine(bool status)
        {
            Common.EnableTextBox(status, txtFunctionName, txtFunctionDescription, txtValue, txtOrderNo);
            Common.EnableComboBox(status, cmbAccess);
        }

        public void ClearLine()
        {
            Common.ClearTextBox(txtFunctionName, txtFunctionDescription, txtValue, txtOrderNo);
            Common.ClearComboBox(cmbAccess);
        }

        public override void FormLoad()
        {
            try
            {
                // Load Locations
                dgvLocationInfo.AutoGenerateColumns = false;
                dgvCashier.AutoGenerateColumns = false;
                dgvCashierLocation.AutoGenerateColumns = false;
                LoadAllLocations();
                //chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                InitializeForm();
                ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();

                // Load Employee Designations
                CashierGroupService cashierGroupService = new CashierGroupService();
                Common.LoadEmployeeDesignations(cmbGroup, cashierGroupService.GetAllEmployeeDesignationTypes());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


                base.FormLoad();
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
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllActiveEmployeeCodesForCashier(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllActiveEmployeeNamesForCashier(), chkAutoCompleationEmployee.Checked);

                EnableLine(false);
                cmbAccess.SelectedIndex = -1;

                LoadAllLocations();

                CashierPermissionService cashierPermissionService = new CashierPermissionService();
                existingCashierPermissionList = cashierPermissionService.GetAllCashierFunctionsForCashierPermission();
                dgvCashier.DataSource = existingCashierPermissionList;
                dgvCashier.Refresh();

                RowPaint();

                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtEmployeeCode);
                Common.ClearTextBox(txtEmployeeCode, txtEmployeeName, txtAddress1, txtAddress2, txtAddress3, txtEmail, txtMobile, txtReferenceNo, txtRemark, txtTelephone);
                tabEmployee.SelectedTab = tbpGeneral;

                dgvCashierLocation.DataSource = null;

                cmbGroup.SelectedIndex = -1;

                this.ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public static void SetAutoBindRecords(ComboBox comboBox, string[] stringCollection)
        {
            try
            {
                AutoCompleteStringCollection autoCompleteCode = new AutoCompleteStringCollection();
                autoCompleteCode.AddRange(stringCollection);
                comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox.AutoCompleteCustomSource = autoCompleteCode;
                comboBox.DataSource = autoCompleteCode;
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "FrmCustomer", Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtEmployeeCode, txtEmployeeName, txtPassword, txtConfirmPassword);
        }

        public void UpdateGrid()
        {
            bool isAccess;
            CashierPermissionService cashierPermissionService = new CashierPermissionService();
            CashierPermission cashierPermission = new CashierPermission();

            if (cmbAccess.Text == "True") { isAccess = true; }
            else { isAccess = false; }
            decimal value = Convert.ToDecimal(txtValue.Text.Trim());
            txtValue.Text = string.Format("{0:0.00}", value);

            cashierPermission.Order = Common.ConvertStringToLong(txtOrderNo.Text.Trim());
            cashierPermission.FunctionName = txtFunctionName.Text.Trim();
            cashierPermission.FunctionDescription = txtFunctionDescription.Text.Trim();
            cashierPermission.Value = Common.ConvertStringToDecimalCurrency(txtValue.Text.Trim());
            cashierPermission.IsAccess = isAccess;

            existingCashierPermissionList = cashierPermissionService.UpdateCashierPermission(existingCashierPermissionList, cashierPermission);
            dgvCashier.DataSource = null;
            dgvCashier.DataSource = existingCashierPermissionList;
            dgvCashier.Refresh();

            ClearLine();
            EnableLine(false);
            dgvCashier.Focus();
        }

        public void UpdateList()
        {
            foreach (DataGridViewRow row in dgvCashier.Rows)
            {
                CashierPermissionService cashierPermissionService = new CashierPermissionService();
                CashierPermission cashierPermission = new CashierPermission();

                if (dgvCashier["FunctionName", row.Index].Value.ToString() != string.Empty) { cashierPermission.FunctionName = dgvCashier["FunctionName", row.Index].Value.ToString().Trim(); }
                else { cashierPermission.FunctionName = string.Empty; }

                if (dgvCashier["FunctionDescription", row.Index].Value.ToString() != string.Empty) { cashierPermission.FunctionDescription = dgvCashier["FunctionDescription", row.Index].Value.ToString().Trim(); }
                else { cashierPermission.FunctionDescription = string.Empty; }

                if (Convert.ToBoolean(dgvCashier["Access", row.Index].Value) == true) { cashierPermission.IsAccess = true; }
                else { cashierPermission.IsAccess = false; }

                if (dgvCashier["Value", row.Index].Value.ToString() != string.Empty) { cashierPermission.Value = Common.ConvertStringToDecimalCurrency(dgvCashier["Value", row.Index].Value.ToString().Trim()); }
                else { cashierPermission.Value = Common.ConvertStringToDecimalCurrency("0"); }

                long order = Common.ConvertStringToLong(dgvCashier["FunctionDescription", row.Index].Value.ToString().Trim()) + 1;
                cashierPermission.Order = order;

                existingCashierPermissionList = cashierPermissionService.UpdateCashierPermission(existingCashierPermissionList, cashierPermission);
            }
        }


        public override void Save()
        {
            try
            {
                if ((Toast.Show("Cashier " + txtEmployeeCode.Text.Trim() + " " + txtEmployeeName.Text.Trim(), Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                {
                    return;
                }

                if (ValidateControls() == false) return;

                if (ConfirmPassword())
                {
                    UpdateList();
                    GetSelectedLocations();
                    FillCashier();
                    CashierPermissionService cashierPermissionService = new CashierPermissionService();

                    this.Cursor = Cursors.WaitCursor;
                    cashierPermissionService.SaveCashierPermoission(existingCashierPermissionList, existingLocationList, existingCashierPermission, unselectLocationList);
                    this.Cursor = Cursors.Default;

                    Toast.Show("Cashier " + txtEmployeeCode.Text.Trim() + " " + txtEmployeeName.Text.Trim(), Toast.messageType.Information, Toast.messageAction.Saved);
                    ClearForm();
                    ClearObjects();
                    InitializeForm();
                }
                else
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.ConfirmPassword);
                    txtConfirmPassword.SelectAll();
                    txtConfirmPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        public void ClearObjects()
        {
            existingCashierPermission = new CashierPermission();
            existingCashierPermissionList.Clear();
            //existingCashierPermissionList.Clear();
            existingLocationList.Clear();
        }

        private CashierPermission FillCashier()
        {
            try
            {
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();

                employee = employeeService.GetEmployeesByCode(txtEmployeeCode.Text.Trim());

                if (employee != null)
                {
                    existingCashierPermission.CashierID = employee.EmployeeID;
                    existingCashierPermission.EmployeeID = employee.EmployeeID;
                    existingCashierPermission.Password = txtPassword.Text.Trim();
                    existingCashierPermission.JournalName = txtJournalName.Text.Trim();
                    existingCashierPermission.EnCode = txtEnCode.Text.Trim();
                    existingCashierPermission.Type = txtType.Text.Trim();
                    existingCashierPermission.IsActive = chkIsActive.Checked;
                    existingCashierPermission.Remarks = txtRemark.Text.Trim();
                    existingCashierPermission.TypeID = int.Parse(cmbGroup.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
            return existingCashierPermission;
        }

        public void GetSelectedLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == true)
                {
                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    int locID = Common.ConvertStringToInt(dgvLocationInfo.Rows[i].Cells["LocationID"].Value.ToString().Trim());

                    location = locationService.GetLocationsByID(locID);
                    existingLocationList.Add(location);
                }
                else
                {
                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    int locID = Common.ConvertStringToInt(dgvLocationInfo.Rows[i].Cells["LocationID"].Value.ToString().Trim());

                    location = locationService.GetLocationsByID(locID);
                    unselectLocationList.Add(location);
                }
            }
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Cashier - " + txtEmployeeCode.Text.Trim() + " - " + txtEmployeeName.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();
                CashierPermissionService cashierPermissionService = new CashierPermissionService();

                employee = employeeService.GetActiveEmployeesByCode(txtEmployeeCode.Text.Trim());

                if (employee != null)
                {
                    cashierPermissionService.DeleteCashierPermoission(employee.EmployeeID);
                }

                Toast.Show("Cashier - " + txtEmployeeCode.Text.Trim() + " - " + txtEmployeeName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Delete);
                ClearForm();
                ClearObjects();
                InitializeForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void View()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmCashier");
                InvReportGenerator invReportGenerator = new InvReportGenerator();
                invReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtEmployeeName.Focus();
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    EmployeeService employeeService = new EmployeeService();
                    DataView dvEmployee = new DataView(employeeService.GetAllEmployeeDataTable());
                    if (dvEmployee.Count != 0)
                    {
                        LoadReferenceSearchForm(dvEmployee, this.Name, this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeCode);
                        txtEmployeeCode_Leave(this, e);
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

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeCode.Text.Trim() != string.Empty)
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee employee = new Employee();

                    employee = employeeService.GetActiveEmployeesByCode(txtEmployeeCode.Text.Trim());

                    if (employee != null)
                    {
                        txtEmployeeCode.Text = employee.EmployeeCode;
                        txtEmployeeName.Text = employee.EmployeeName;

                        txtReferenceNo.Text = employee.ReferenceNo;
                        txtAddress1.Text = employee.Address1;
                        txtAddress2.Text = employee.Address2;
                        txtAddress3.Text = employee.Address3;
                        txtEmail.Text = employee.Email;
                        txtTelephone.Text = employee.Telephone;
                        txtMobile.Text = employee.Mobile;
                        txtDesignation.Text = employee.Designation;

                        CashierPermission cashierPermission = new CashierPermission();
                        CashierPermissionService cashierPermissionService = new CashierPermissionService();

                        cashierPermission = cashierPermissionService.GetCashierPermissionByCashierID(employee.EmployeeID);

                        if (cashierPermission != null)
                        {
                            dgvCashier.DataSource = null;

                            txtJournalName.Text = cashierPermission.JournalName;
                            txtEnCode.Text = cashierPermission.EnCode;
                            txtType.Text = cashierPermission.Type;
                            chkIsActive.Checked = cashierPermission.IsActive;
                            txtPassword.Text = cashierPermission.Password;
                            txtConfirmPassword.Text = cashierPermission.Password;
                            txtRemark.Text = cashierPermission.Remarks;
                            cmbGroup.SelectedValue = cashierPermission.TypeID;

                            existingCashierPermissionList = cashierPermissionService.GetCashierPermissionListByCashierID(employee.EmployeeID);

                            if (existingCashierPermissionList == null || existingCashierPermissionList.Count == 0)
                            {
                                existingCashierPermissionList = cashierPermissionService.GetAllCashierFunctionsForCashierPermission();
                                dgvCashier.DataSource = existingCashierPermissionList;
                                dgvCashier.Refresh();
                            }
                            else
                            {
                                dgvCashier.DataSource = existingCashierPermissionList;
                                dgvCashier.Refresh();
                            }

                            var locationList = cashierPermissionService.GetSelectedLocationsToCashierPermission(employee.EmployeeID);

                            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
                            {
                                foreach (var temp in locationList)
                                {
                                    if (Convert.ToInt64(dgvLocationInfo.Rows[i].Cells["LocationId"].Value) == temp.LocationID)
                                    {
                                        dgvLocationInfo.Rows[i].Cells["Selection"].Value = true;
                                    }
                                }
                            }
                        }

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                    }
                    else
                    {
                        Toast.Show("Employee  - " + txtEmployeeCode.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
                        txtEmployeeCode.Focus();
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtEmployeeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvSalesPersonService invSalesmanService = new InvSalesPersonService();
                    txtEmployeeCode.Text = invSalesmanService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtEmployeeCode);
                    txtEmployeeName.Focus();

                }
                else
                {
                    Common.EnableTextBox(true, txtEmployeeCode);
                    txtEmployeeCode.Focus();
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
                        LoadReferenceSearchForm(dvEmployee, this.Name, this.Text.Trim(), this.ActiveControl.Text.Trim(), txtEmployeeCode);
                        txtEmployeeCode_Leave(this, e);
                    }
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    tabEmployee.SelectedTab = tbpGeneral;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
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

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, tbpContactDetails);
                Common.SetFocus(e, txtAddress1);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress2);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtAddress3);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtEmail);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtTelephone);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Common.SetFocus(e, txtMobile);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCommtionCode_KeyDown(object sender, KeyEventArgs e)
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


        private void chkAutoCompleationSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllActiveEmployeeCodesForCashier(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllActiveEmployeeNamesForCashier(), chkAutoCompleationEmployee.Checked);
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
                //if (txtEmployeeName.Text.Trim() != string.Empty)
                //{
                //    EmployeeService employeeService = new EmployeeService();
                //    Employee employee = new Employee();

                //    employee = employeeService.GetActiveEmployeesByName(txtEmployeeName.Text.Trim());

                //    if (employee != null)
                //    {
                //        txtEmployeeCode.Text = employee.EmployeeCode;
                //        txtEmployeeName.Text = employee.EmployeeName;

                //        txtReferenceNo.Text = employee.ReferenceNo;
                //        txtAddress1.Text = employee.Address1;
                //        txtAddress2.Text = employee.Address2;
                //        txtAddress3.Text = employee.Address3;
                //        txtEmail.Text = employee.Email;
                //        txtTelephone.Text = employee.Telephone;
                //        txtMobile.Text = employee.Mobile;
                //        txtDesignation.Text = employee.Designation;

                //        Common.EnableButton(true, btnSave, btnDelete);
                //        Common.EnableTextBox(false, txtEmployeeCode);
                //    }
                //    else
                //    {
                //        Toast.Show("" + this.Text + " - " + txtEmployeeName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                //        txtEmployeeName.Focus();
                //        return;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateEmailAddress(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateTelephoneNumber(this, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllLocations.Checked) { CheckedAllLocations(); } else { UnCheckedAllLocations(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CheckedAllLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                dgvLocationInfo.Rows[i].Cells["Selection"].Value = true;
            }

        }

        private void UnCheckedAllLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                dgvLocationInfo.Rows[i].Cells["Selection"].Value = false;
            }

        }

        private void LoadAllLocations()
        {
            LocationService locationService = new LocationService();
            List<Location> locations = new List<Location>();

            locations = locationService.GetAllInventoryLocations();
            dgvLocationInfo.DataSource = locations;
            dgvLocationInfo.Refresh();

        }

        private void dgvCashier_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dgvCashier.CurrentCell != null && dgvCashier.CurrentCell.RowIndex >= 0)
            //    {
            //        bool access;
            //        DataGridViewCheckBoxCell dgvCheckBox = new DataGridViewCheckBoxCell();
            //        dgvCheckBox = (DataGridViewCheckBoxCell)dgvCashier.Rows[dgvCashier.CurrentRow.Index].Cells["Access"];

            //        if (dgvCheckBox.Value == null) { access = true; }
            //        else { access = false; }

            //        LoadFunctionDetails(int.Parse(dgvCashier["RowNo", dgvCashier.CurrentCell.RowIndex].Value.ToString()), access);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            //}
        }

        public void LoadFunctionDetails(int rowNo, bool isAccess)
        {
            CashierPermission cashierPermission = new CashierPermission();
            cashierPermission = existingCashierPermissionList.Where(p => p.Order.Equals(rowNo)).FirstOrDefault();

            if (cashierPermission != null)
            {
                txtOrderNo.Text = cashierPermission.Order.ToString();
                txtFunctionName.Text = cashierPermission.FunctionName;
                txtFunctionDescription.Text = cashierPermission.FunctionDescription;

                if (isAccess) { cmbAccess.Text = "True"; }
                else { cmbAccess.Text = "False"; }

                cmbAccess.Enabled = true;
                txtValue.Enabled = true;
                //txtValue.Focus();
                cmbAccess.Focus();
                cmbAccess.SelectedIndex = 0;
            }
        }


        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(txtValue.Text.Trim())) { return; }
                    UpdateGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(txtPassword.Text.Trim())) { return; }
                    txtConfirmPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim())) { return; }

                CashierPermissionService cashierPermissionService = new CashierPermissionService();
                if (cashierPermissionService.ChechExistsPassword(txtPassword.Text.Trim()))
                {
                    Toast.Show("This password already exists", Toast.messageType.Information, Toast.messageAction.General);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    txtConfirmPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim())) { return; }

                    if (ConfirmPassword())
                    {
                        txtJournalName.Focus();
                    }
                    else
                    {
                        Toast.Show("", Toast.messageType.Information, Toast.messageAction.ConfirmPassword);
                        txtConfirmPassword.SelectAll();
                        txtConfirmPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public bool ConfirmPassword()
        {
            if (string.Equals(txtPassword.Text.Trim(), txtConfirmPassword.Text.Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtJournalName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtEnCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtEnCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtType.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (int i = 0; i < dgvCashier.RowCount; i++)
                    {
                        dgvCashier.Rows[i].Cells["Access"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvCashier.RowCount; i++)
                    {
                        dgvCashier.Rows[i].Cells["Access"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbAccess_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtValue.SelectAll();
                    txtValue.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dgvCashier.Rows[e.RowIndex].Cells["IsValue"].Value) == true)
                {
                    //dgvCashier.Rows[e.RowIndex].Cells["Value"].Style.ForeColor = Color.Red;
                    dgvCashier.Rows[e.RowIndex].Cells["Value"].Style.BackColor = Color.SkyBlue;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CashierGroupService cashierGroupService = new CashierGroupService();
                if (cmbGroup.SelectedValue == null) { return; }
                else
                {
                    existingCashierPermissionList = cashierGroupService.GetGroupFunctionsByDesignationID(Common.ConvertStringToInt(cmbGroup.SelectedValue.ToString()));
                    dgvCashier.DataSource = existingCashierPermissionList;
                    dgvCashier.Refresh();
                    RowPaint();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void RowPaint()
        {
            var tempList = existingCashierPermissionList;
            foreach (var item in tempList)
            {
                if (item.IsValue == true)
                {
                    dgvCashier.Rows[((int)item.Order) - 1].Cells["Value"].Style.BackColor = Color.SkyBlue;
                }
            }
        }

        private void dgvCashier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 3)
                {
                    if (Convert.ToBoolean(dgvCashier["Access", dgvCashier.CurrentCell.RowIndex].Value) == true)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].Value = Convert.ToDecimal(0.00);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Random random = new Random();
                int newPassword = random.Next(100000, 999999);

                CashierPermissionService cashierPermissionService = new CashierPermissionService();
                if (cashierPermissionService.ChechExistsPassword(newPassword.ToString()))
                {
                    btnGenerate.PerformClick();
                }
                else
                {
                    txtGeneratedPassword.Text = newPassword.ToString();
                    txtPassword.Text = newPassword.ToString();
                    txtConfirmPassword.Text = newPassword.ToString();

                    System.Threading.Thread.Sleep(1000);
                    tabEmployee.SelectedTab = tbpGeneral;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvLocationInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Common.isHeadOffice)
                {
                    if (dgvLocationInfo.CurrentCell.ColumnIndex == 0)
                    {
                        List<CashierPermission> cashier = new List<CashierPermission>();
                        CashierPermissionService cashierPermissionService = new CashierPermissionService();

                        int locationID = Convert.ToInt32(dgvLocationInfo["LocationId", dgvLocationInfo.CurrentCell.RowIndex].Value);
                        cashier = cashierPermissionService.GetCashiersAccordingToLocation(locationID);
                        dgvCashierLocation.DataSource = cashier;
                        dgvCashierLocation.Refresh();
                        tabEmployee.SelectedTab = tbpCashiers;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashierLocation_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvCashierLocation.CurrentCell != null && dgvCashierLocation.CurrentCell.RowIndex >= 0)
                {
                    txtEmployeeCode.Text = dgvCashierLocation["EmployeeCode", dgvCashierLocation.CurrentCell.RowIndex].Value.ToString();
                    txtEmployeeCode_Leave(this, e);
                    tabEmployee.SelectedTab = tbpGeneral;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void CopyAllToClipBoard()
        {
            dgvCashierLocation.SelectAll();
            DataObject dataObject = dgvCashierLocation.GetClipboardContent();
            if (dataObject != null)
            { Clipboard.SetDataObject(dataObject); }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                DataObject dataObject = dgvCashierLocation.GetClipboardContent();
                if (dataObject != null)
                { Clipboard.SetDataObject(dataObject); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "export.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(dgvCashierLocation, sfd.FileName);
                    Toast.Show("File export successfully", Toast.messageType.Information, Toast.messageAction.General);
                }  
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ExportToExcel(DataGridView dataGridView, string filename) 
        {
            string stOutput = "";
            string sHeaders = "";

            for (int j = 0; j < dataGridView.Columns.Count; j++)
            { sHeaders = sHeaders.ToString() + Convert.ToString(dataGridView.Columns[j].HeaderText) + "\t"; }
            stOutput += sHeaders + "\r\n";

            for (int i = 0; i < dataGridView.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                { stLine = stLine.ToString() + Convert.ToString(dataGridView.Rows[i].Cells[j].Value) + "\t"; }
                stOutput += stLine + "\r\n";
            }
            Encoding endoding = Encoding.GetEncoding(1254);
            byte[] output = endoding.GetBytes(stOutput);
            FileStream fileStream = new FileStream(filename, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(output, 0, output.Length); 
            binaryWriter.Flush();
            binaryWriter.Close();
            fileStream.Close();
        }

        private void dgvCashier_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 4)
                {
                    if (Convert.ToBoolean((dgvCashier["IsValue", dgvCashier.CurrentCell.RowIndex].Value)) == false)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodBase.GetCurrentMethod().Name, this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvCashier_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvCashier.CurrentCell.ColumnIndex == 4)
                {
                    if (Convert.ToBoolean((dgvCashier["IsValue", dgvCashier.CurrentCell.RowIndex].Value)) == false)
                    {
                        dgvCashier["Value", dgvCashier.CurrentCell.RowIndex].ReadOnly = true;
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
