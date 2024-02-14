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
using System.Threading;

namespace UI.Windows
{
    public partial class FrmUserPrivileges : UI.Windows.FrmBaseMasterForm
    {

        /// <summary>
        /// User Group Privileges
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 02/10/2013
        /// </summary>
        /// 

        ErrorMessage errorMessage = new ErrorMessage();
        
        List<UserPrivilegesLocations> userPrivilegesLocationsList = new List<UserPrivilegesLocations>();
        UserPrivilegesLocations userPrivilegesLocationsTemp = new UserPrivilegesLocations();
        List<Location> existingLocationList = new List<Location>();
        List<Location> unselectLocationList = new List<Location>();
        List<UserGroupPrivileges> existingUserGroupPrivilegesList = new List<UserGroupPrivileges>();

        List<AutoGenerateInfo> autoGenerateInfoList = new List<AutoGenerateInfo>();

        private UserGroup existingUserGroup;
        private UserMaster existingUser;


        bool isValidControls = true;
        bool isCheckedAllLocations = true;

        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmUserPrivileges()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            this.CausesValidation = false;
            dgvGroupPrivileges.AutoGenerateColumns = false;

            // Load Locations
            dgvLocationInfo.AutoGenerateColumns = false;
            LoadAllLocations();

            // Load User Groups
            UserPrivilegesService userGroups = new UserPrivilegesService();
            Common.LoadAllUserGroups(cmbUserGroupName, userGroups.GetAllUserGroups());

            // Load User Accounts
            UserPrivilegesService userAccounts = new UserPrivilegesService();
            Common.LoadAllUserAccounts(cmbUserName, userAccounts.GetAllUserAccounts());

            InitializeForm();

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

        }

        public override void InitializeForm()
        {
            try
            {
                Employee employee = new Employee();
                EmployeeService employeeService = new EmployeeService();

                Common.SetAutoComplete(txtEmployeeCode, employeeService.GetAllActiveEmployeeCodesForCashier(), chkAutoCompleationEmployee.Checked);
                Common.SetAutoComplete(txtEmployeeName, employeeService.GetAllActiveEmployeeNamesForCashier(), chkAutoCompleationEmployee.Checked);

                LoadAllLocations();

                userPrivilegesLocationsList = new List<UserPrivilegesLocations>();
                existingLocationList = new List<Location>();
                unselectLocationList = new List<Location>();
                existingUserGroupPrivilegesList = new List<UserGroupPrivileges>();

                Common.EnableButton(false, btnSave, btnDelete, btnView, btnPrint);

                this.ActiveControl = txtEmployeeCode;
                txtEmployeeCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadAllLocations()
        {
            LocationService locationService = new LocationService();
            List<Location> locations = new List<Location>();

            locations = locationService.GetAllLocations();
            dgvLocationInfo.DataSource = locations;
            dgvLocationInfo.Refresh();

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

        private void CheckCheckedStatus()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
                if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == false) { chkAllLocations.Checked = false; return; } else { chkAllLocations.Checked = true; }
            }

        }

        private bool RecallUserGroupPrivileges(string userGroupName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                UserGroup userGroup = new UserGroup();

                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                userGroup = userPrivilegesService.GetUserGroupByName(cmbUserGroupName.Text);
                existingUserGroup = new UserGroup();
                //existingUserGroup.UserGroupPrivileges 

                if (existingUser == null)
                    existingUser = new UserMaster();

                autoGenerateInfoList = userPrivilegesService.GetUserGroupPrivileges(userGroup, existingUser.UserMasterID);
                dgvGroupPrivileges.DataSource = autoGenerateInfoList;
                dgvGroupPrivileges.Refresh();

                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                //Common.EnableTextBox(false, txtPromotionCode, txtPromotionDescription);
                //Common.EnableComboBox(false, cmbPromotionType);

                this.Cursor = Cursors.Default;
                return true;


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;

                return false;

            }
        }

        


        public void GetSelectedLocations()
        {
            for (int i = 0; i < dgvLocationInfo.RowCount; i++)
            {
               
                Location location = new Location();
                LocationService locationService = new LocationService();
                int locID = Common.ConvertStringToInt(dgvLocationInfo.Rows[i].Cells["LocationID"].Value.ToString().Trim());

                location = locationService.GetLocationsByID(locID);
                if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == true)
                {
                    location.IsSelect = true;
                }
                else
                {
                    location.IsSelect = false;
                }
                existingLocationList.Add(location);
                
            }
        }

        

        


        private void cmbUserGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbUserGroupName.Text.Trim().Equals(string.Empty))
                        //RecallUserGroupPrivileges(cmbUserGroupName.Text.Trim());
                       // btnSave.Focus();
                    dgvGroupPrivileges.Focus();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void cmbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                    if (cmbUserName.Text.Trim() != null && cmbUserName.Text.Trim() != "")
                    {
                        //RecallUserMaster(cmbUserName.Text.ToString());
                        //RecallUserPrivileges(cmbUserName.Text.Trim());

                        //txtDescription.Focus();

                    }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void RecallUserMaster(string userName)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                UserMaster existingUser = new UserMaster();
                UserPrivilegesService userPrivilegesService = new Service.UserPrivilegesService();

                existingUser = userPrivilegesService.getUserMasterByUserName(cmbUserName.Text.Trim());
                if (existingUser != null)
                {

                    txtDescription.Text = existingUser.UserDescription;
                    txtPassword.Text = existingUser.Password;
                    txtConfirmPassword.Text = existingUser.Password;
                    cmbUserGroupName.SelectedValue = existingUser.UserGroupID;
                    //cmbUserGroupName.Refresh();
                    chkActive.Checked = existingUser.IsActive;
                    ChkCantChgPwd.Checked = existingUser.IsUserCantChangePassword;
                    ChkChangePassword.Checked = existingUser.IsUserMustChangePassword;

                    if (accessRights.IsPause == true) Common.EnableButton(true, btnSave);

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;

            }
        }

        private void RecallUserMasterByEmployeeCode(string employeeCode)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                UserMaster existingUser = new UserMaster();
                UserPrivilegesService userPrivilegesService = new Service.UserPrivilegesService();

                existingUser = userPrivilegesService.getUserMasterByUserEmployeeCode(employeeCode);
                if (existingUser != null)
                {
                    txtDescription.Text = existingUser.UserDescription;
                    txtPassword.Text = existingUser.Password;
                    txtConfirmPassword.Text = existingUser.Password;
                    cmbUserGroupName.SelectedValue = existingUser.UserGroupID;
                    chkActive.Checked = existingUser.IsActive;
                    ChkCantChgPwd.Checked = existingUser.IsUserCantChangePassword;
                    ChkChangePassword.Checked = existingUser.IsUserMustChangePassword;

                    if (accessRights.IsPause == true) Common.EnableButton(true, btnSave);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;

            }
        }

        private bool RecallUserPrivileges(string userName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                UserMaster user = new UserMaster();
                UserPrivilegesService userPrivilegesList = new UserPrivilegesService();
                List<Location> lstLocations = new List<Location>();
                //List<UserPrivilegesLocations> listuserPrivilegesLocations = new List<UserPrivilegesLocations>();


                user = userPrivilegesList.GetUserByName(cmbUserName.Text.ToString());

                existingUser = new UserMaster();
                existingUser = user;
                if (existingUser != null)
                {
                    autoGenerateInfoList = userPrivilegesList.GetUserPrivilegesByUserId(user);
                    dgvGroupPrivileges.DataSource = autoGenerateInfoList;
                    dgvGroupPrivileges.Refresh();

                    lstLocations = userPrivilegesList.GetUserLocationsByUserId(user);

                    dgvLocationInfo.DataSource = null;
                    dgvLocationInfo.DataSource = lstLocations;
                    dgvLocationInfo.Refresh();


                    Common.EnableComboBox(false, cmbUserName);
                    Common.EnableButton(true, btnSave);
                }

                this.Cursor = Cursors.Default;
                return true;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;

                return false;

            }
        }

        private bool RecallUserPrivilegesByEmployeeCode(string employeeCode)  
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                UserMaster user = new UserMaster();
                UserPrivilegesService userPrivilegesList = new UserPrivilegesService();
                List<Location> lstLocations = new List<Location>();

                user = userPrivilegesList.GetUserByEmployeeCode(employeeCode);

                existingUser = new UserMaster();
                existingUser = user;
                if (existingUser != null)
                {
                    autoGenerateInfoList = userPrivilegesList.GetUserPrivilegesByUserId(user);
                    dgvGroupPrivileges.DataSource = autoGenerateInfoList;
                    dgvGroupPrivileges.Refresh();

                    lstLocations = userPrivilegesList.GetUserLocationsByUserId(user);

                    dgvLocationInfo.DataSource = null;
                    dgvLocationInfo.DataSource = lstLocations;
                    dgvLocationInfo.Refresh();

                    Common.EnableComboBox(false, cmbUserName);
                    Common.EnableButton(true, btnSave);
                }

                this.Cursor = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                    if (txtDescription.Text.Trim() != null && txtDescription.Text.Trim() != "") { txtPassword.Focus(); }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtPassword.Text.Trim() != null && txtPassword.Text.Trim() != "") { Common.EnableTextBox(true, txtConfirmPassword); txtConfirmPassword.Focus(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtConfirmPassword.Text.Trim() != null && txtConfirmPassword.Text.Trim() != "")
                    {
                        if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim()) { cmbUserGroupName.Focus(); }
                        else { Toast.Show("The Password was not correctly confirmed. please ensure that the password and confirmation match exactly.", Toast.messageType.Information, Toast.messageAction.General); }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvLocationInfo_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                isCheckedAllLocations = false;
                CheckCheckedStatus();
                isCheckedAllLocations = true;
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
                if (isCheckedAllLocations == true)
                {
                    if (chkAllLocations.Checked) { CheckedAllLocations(); } else { UnCheckedAllLocations(); }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Save()
        {
            try
            {

                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();

                if (ValidateControls().Equals(false))
                { return; }

                if (existingUser == null)
                {
                    existingUser = new UserMaster();
                }
                
                FillPrivileges();
                GetSelectedLocations();

                if (existingUser.UserMasterID == 0)
                {
                    if ((Toast.Show("User Name - " + existingUser.UserName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    userPrivilegesService.AddUser(existingUser, existingLocationList, autoGenerateInfoList);

                    Toast.Show("User Name - " + existingUser.UserName + "", Toast.messageType.Information, Toast.messageAction.Save);
                }
                else
                {
                    if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingUser.UserName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                    if ((Toast.Show("User Name - " + existingUser.UserName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    userPrivilegesService.UpdateUser(existingUser, existingLocationList, autoGenerateInfoList);

                    Toast.Show("User Name - " + existingUser.UserName + "", Toast.messageType.Information, Toast.messageAction.Modify);

                }
                ClearForm();
                InitializeForm();

                cmbUserName.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void FillPrivileges()
        {
            try
            {
                #region Header
                Location LogingLocation = new Location();

                if (existingUser == null)
                {
                    existingUser = new UserMaster();
                }
                existingUser.CompanyID = Common.LoggedCompanyID;
                existingUser.LocationID = Common.LoggedLocationID;
                //existingUser.UserMasterID = Common.ConvertStringToInt(cmbUserName.SelectedValue.ToString());
                existingUser.UserName = cmbUserName.Text.Trim();
                existingUser.UserDescription = txtDescription.Text.Trim();
                existingUser.Password = txtPassword.Text.Trim();
                existingUser.UserGroupID = Common.ConvertStringToInt(cmbUserGroupName.SelectedValue.ToString());
                existingUser.EmployeeCode = txtEmployeeCode.Text.Trim();
                
                existingUser.IsActive = chkActive.Checked;
                existingUser.IsUserMustChangePassword = ChkChangePassword.Checked;
                existingUser.IsUserCantChangePassword = ChkCantChgPwd.Checked;
                existingUser.IsDelete = false;

                #endregion

                //#region Details

                //bool x=false;
                //userPrivilegesLocationsList = new List<UserPrivilegesLocations>();
                //for (int i = 0; i < dgvLocationInfo.RowCount; i++)
                //{

                //    if (Convert.ToBoolean(dgvLocationInfo.Rows[i].Cells["Selection"].Value) == true)
                //    {
                //        x = true;
                //    }
                //    else
                //    {

                //        x = false;
                //    }

                //    var t = new UserPrivilegesLocations()
                //    {

                //        UserGroupID = existingUser.UserGroupID,
                //        UserMasterID = existingUser.UserMasterID,
                //        LocationID = Convert.ToInt32(dgvLocationInfo.Rows[i].Cells["LocationId"].Value),
                //        UserPrivilegesLocationsID = Convert.ToInt32(dgvLocationInfo.Rows[i].Cells["LocationId"].Value),
                //        IsSelect = x
                        
                //    };

                //    userPrivilegesLocationsList.Add(t);                   
                    
                //}

                //existingUser.UserPrivilegesLocations = userPrivilegesLocationsList;


                //#endregion

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        public override void ClearForm()
        {
            dgvGroupPrivileges.DataSource = null;
            dgvGroupPrivileges.Refresh();

            //dgvLocationInfo.DataSource = null;
            //dgvLocationInfo.Refresh();
            cmbUserGroupName.SelectedValue = -1;
            UnCheckedAllLocations();
            Common.ClearComboBox(cmbUserName);
            Common.ClearTextBox(txtDescription, txtPassword, txtConfirmPassword, txtEmployeeCode, txtEmployeeName);
            Common.EnableComboBox(true, cmbUserName);
            Common.EnableTextBox(true, txtEmployeeCode);

            base.ClearForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void chkAutoCompleationEmployee_CheckedChanged(object sender, EventArgs e)
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

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtPassword.Focus();
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

                        cmbUserName.Text = employee.EmployeeName;
                        txtDescription.Text = employee.EmployeeName;

                        RecallUserMasterByEmployeeCode(txtEmployeeCode.Text.Trim());
                        RecallUserPrivilegesByEmployeeCode(txtEmployeeCode.Text.Trim());
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
                    cmbUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void dgvGroupPrivileges_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewUserList_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUserGroupName.Text != string.Empty)
                {
                    UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                    DataView dvAllReferenceData = new DataView(userPrivilegesService.GetAllActiveUsersDataTable(Common.ConvertStringToLong(cmbUserGroupName.SelectedValue.ToString())));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), "", cmbUserName);

                    }
                }
                else
                {
                    Toast.Show("Invalid user group", Toast.messageType.Information, Toast.messageAction.General);
                    cmbUserGroupName.Focus();
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

        private bool ValidateControls()
        {
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, cmbUserName, txtPassword, txtConfirmPassword, cmbUserGroupName, txtEmployeeCode);
        }

        private void cmbUserGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!cmbUserGroupName.Text.Trim().Equals(string.Empty))
                {
                    RecallUserGroupPrivileges(cmbUserGroupName.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
