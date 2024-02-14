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
    public partial class FrmGroupPrivileges : UI.Windows.FrmBaseMasterForm
    {

        /// <summary>
        /// User Group Privileges
        /// Design By - C.S.Malluwawadu
        /// Developed By - C.S.Malluwawadu
        /// Date - 02/10/2013
        /// </summary>
        /// 
        int documentID;
        ErrorMessage errorMessage = new ErrorMessage();
        bool updateAllUsers = false;
        int type;

        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        UserGroup existingUserGroup = new UserGroup();
        private UserGroupPrivileges existingUserGroupPrivileges;
        List<UserGroupPrivileges> userGroupPrivilegesList = new List<UserGroupPrivileges>();
        UserGroupPrivileges existingUserPrivilegesTemp = new UserGroupPrivileges();
        List<TransactionRights> Transaction = new List<TransactionRights>();
        List<AutoGenerateInfo> autoGenerateInfoList = new List<AutoGenerateInfo>();   

        public FrmGroupPrivileges()
        {
            InitializeComponent();
        }

        public override void InitializeForm()
        {
            try
            {
                // Load User Groups
                UserPrivilegesService userGroups = new UserPrivilegesService();
                Common.LoadAllUserGroups(cmbUserGroupName, userGroups.GetAllUserGroups());

                GetAllTransactions();

                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                List<Transaction> invTransaction = new List<Transaction>();

                Common.EnableButton(true, btnNew);
                //if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                //if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                //if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                Common.EnableButton(false, btnSave);
                Common.EnableButton(false, btnDelete);
                Common.EnableButton(false, btnView);

                Common.EnableComboBox(true, cmbUserGroupName);
                Common.EnableCheckBox(false, chkCommon, chkInventory, chkPOS, chkGV, chkFinance, chkHR, chkLogistic, chkCRM);
                Common.ClearComboBox(cmbUserGroupName);

                updateAllUsers = false;

                ActiveControl = cmbUserGroupName;
                cmbUserGroupName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void FormLoad()
        {
            this.CausesValidation = false;
            dgvGroupPrivileges.AutoGenerateColumns = false;

            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

            documentID = autoGenerateInfo.DocumentID;

            updateAllUsers = false;

            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);


            base.FormLoad();
        }

        private void GetAllTransactions()
        {
            UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
            autoGenerateInfoList = userPrivilegesService.GetAllTransactions();
            dgvGroupPrivileges.DataSource = autoGenerateInfoList;
            dgvGroupPrivileges.Refresh();
        }

        private void LoadAllTransactionByTypeId()
        {
            int common = 0;
            int inventory = 0;
            int manufacturing = 0;
            int distribution = 0;
            int finance = 0;
            int crm = 0;
            int hr = 0;
            int logistic = 0;
            int gv = 0;
            int pos = 0;

            /*
         * 1 - Common
         * 2 - Invenoty
         * 3 - Logistic
         * 4 - CRM
         * 5 - Accounts
         * 6 - Gift Voucher
         * 7 - POS  
         * 8 - Reports 
            */


            UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
           
            //existingUserGroup.UserGroupPrivileges = userPrivilegesService.GetTransactionByTypeId(common, inventory, logistic, crm, finance, gv, pos);
            autoGenerateInfoList = userPrivilegesService.GetTransactionByTypeId(autoGenerateInfoList, type);
            dgvGroupPrivileges.DataSource = autoGenerateInfoList;
            dgvGroupPrivileges.Refresh();

        }

        private void UnLoadAllTransactionByTypeId()
        {
            int common = 0;
            int inventory = 0;
            int manufacturing = 0;
            int distribution = 0;
            int finance = 0;
            int crm = 0;
            int hr = 0;
            int logistic = 0;
            int gv = 0;
            int pos = 0;

            /*
         * 1 - Common
         * 2 - Invenoty
         * 3 - Logistic
         * 4 - CRM
         * 5 - Accounts
         * 6 - Gift Voucher
         * 7 - POS  
         * 8 - Reports 
            */


            UserPrivilegesService userPrivilegesService = new UserPrivilegesService();

            //existingUserGroup.UserGroupPrivileges = userPrivilegesService.GetTransactionByTypeId(common, inventory, logistic, crm, finance, gv, pos);
            autoGenerateInfoList = userPrivilegesService.RemoveTransactionByTypeId(autoGenerateInfoList, type);
            dgvGroupPrivileges.DataSource = autoGenerateInfoList;
            dgvGroupPrivileges.Refresh();

        }    


        private void chkInventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 2;
                if (chkInventory.Checked == true)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkFinance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 5;
                if (chkFinance.Checked)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkManufacturing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAllTransactionByTypeId();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkDistribution_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAllTransactionByTypeId();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkHR_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 8;
                if (chkHR.Checked)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkLogistic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 3;
                if (chkLogistic.Checked == true)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkCRM_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 4;
                if (chkCRM.Checked == true)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
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
                Common.EnableButton(false, btnNew);
                if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                Common.ClearComboBox(cmbUserGroupName);
                Common.EnableComboBox(true, cmbUserGroupName);

                Common.EnableCheckBox(true, chkCommon);
                if (Common.ModuleTypes.InventoryAndSales.Equals(true)) { Common.EnableCheckBox(true, chkInventory); Common.ClearCheckBox(chkInventory); } else { Common.EnableCheckBox(false, chkInventory); }
                if (Common.ModuleTypes.PointOfSales.Equals(true)) { Common.EnableCheckBox(true, chkPOS); Common.ClearCheckBox(chkPOS); } else { Common.EnableCheckBox(false, chkPOS); }
                if (Common.ModuleTypes.Manufacture.Equals(true)) { ; } else { ; }
                if (Common.ModuleTypes.CustomerRelationshipModule.Equals(true)) { Common.EnableCheckBox(true, chkCRM); Common.ClearCheckBox(chkCRM); } else { Common.EnableCheckBox(false, chkCRM); }
                if (Common.ModuleTypes.GiftVouchers.Equals(true)) { Common.EnableCheckBox(true, chkGV); Common.ClearCheckBox(chkGV); } else { Common.EnableCheckBox(false, chkGV); }
                if (Common.ModuleTypes.Accounts.Equals(true)) { Common.EnableCheckBox(true, chkFinance); Common.ClearCheckBox(chkFinance); } else { Common.EnableCheckBox(false, chkFinance); }
                if (Common.ModuleTypes.NonTrading.Equals(true)) { Common.EnableCheckBox(true, chkLogistic); Common.ClearCheckBox(chkLogistic); } else { Common.EnableCheckBox(false, chkLogistic); }
                if (Common.ModuleTypes.HrManagement.Equals(true)) { ; } else { ; }
                if (Common.ModuleTypes.HospitalManagement.Equals(true)) { ; } else { ; }
                if (Common.ModuleTypes.HotelManagement.Equals(true)) { ; } else { ; }

                dgvGroupPrivileges.DataSource = null;
                GetAllTransactions();

                dgvGroupPrivileges.Refresh();

                cmbUserGroupName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void dgvGroupPrivileges_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (e.RowIndex != 0)
                {
                    if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 2) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.Blue; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 3) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.Teal; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 4) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.Olive; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 5) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.DarkOrange; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 6) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.SaddleBrown; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 7) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.Purple; }
                    else if (Common.ConvertStringToInt(dgvGroupPrivileges.Rows[e.RowIndex].Cells["ModuleType"].Value.ToString()) == 8) { dgvGroupPrivileges.Rows[e.RowIndex].Cells["FormText"].Style.ForeColor = Color.DodgerBlue; }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        public override void Save()
        {
            try
            {

                if (cmbUserGroupName.Text.Trim() == "")
                {
                    Toast.Show("User Group Name Can Not Be Blank.", Toast.messageType.Information, Toast.messageAction.General);
                    return;
                }
                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                bool isNew = false;

                UserGroup userGroup = new UserGroup();
                userGroup = userPrivilegesService.GetUserGroupByName(cmbUserGroupName.Text);

                
                if (userGroup != null)
                {
                    existingUserGroup.UserGroupID = userGroup.UserGroupID;
                }
                else
                {
                    //existingUserGroup.UserGroupID = 0;
                }
                
                
                existingUserGroup.UserGroupName =cmbUserGroupName.Text.ToString().Trim();
                
                if (existingUserGroup == null )
                {
                    existingUserGroup = new UserGroup();
                }

                if (existingUserGroup.UserGroupID == 0)
                {
                    isNew = true;

                }

                dgvGroupPrivileges.Refresh();
                existingUserGroup.UserGroupName = cmbUserGroupName.Text.Trim();

                if (existingUserGroup.UserGroupID== 0)
                {
                    if ((Toast.Show("User Group - " + existingUserGroup.UserGroupName +  "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }

                    userPrivilegesService.AddUserGroup(existingUserGroup, autoGenerateInfoList, updateAllUsers);


                    if ((Toast.Show("User Group - " + existingUserGroup.UserGroupName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        btnNew.PerformClick();
                    }
                }
                else
                {
                   
                    if (isNew)
                    {
                        if ((Toast.Show("User Group - " + existingUserGroup.UserGroupName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingUserGroup.UserGroupName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("User Group - " + existingUserGroup.UserGroupName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }

                    userPrivilegesService.UpdateUserGroup(existingUserGroup, autoGenerateInfoList, updateAllUsers);                  

                    Toast.Show("User Group - " + existingUserGroup.UserGroupName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                
                }

                ClearForm();

                cmbUserGroupName.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }




        private void cmbUserGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (!cmbUserGroupName.Text.Trim().Equals(string.Empty))
                        RecallUserGroupPrivileges(cmbUserGroupName.Text.Trim());

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
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
                
                if (userGroup != null)
                {
                    autoGenerateInfoList = userPrivilegesService.GetUserGroupPrivilegesByUserGroupId(userGroup);
                    dgvGroupPrivileges.DataSource = autoGenerateInfoList;
                    dgvGroupPrivileges.Refresh();
                }

                Common.EnableComboBox(false, cmbUserGroupName);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

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

        private void grpHeader_Enter(object sender, EventArgs e)
        {

        }

        public override void ClearForm()
        {

            dgvGroupPrivileges.DataSource = null;
            dgvGroupPrivileges.Refresh();

            base.ClearForm();
        }

        private void chkCommon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                type = 1;
                if (chkCommon.Checked == true)
                {
                    LoadAllTransactionByTypeId();
                }
                else
                {
                    UnLoadAllTransactionByTypeId();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkPOS_CheckedChanged(object sender, EventArgs e)
        {
            type = 7;
            if (chkPOS.Checked)
            {
                LoadAllTransactionByTypeId();
            }
            else
            {
                UnLoadAllTransactionByTypeId();
            }
        }

        private void chkGV_CheckedChanged(object sender, EventArgs e)
        {
            type = 6;
            if (chkGV.Checked)
            {
                LoadAllTransactionByTypeId();
            }
            else
            {
                UnLoadAllTransactionByTypeId();
            }
        }

        private void chkSelAllAccess_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelAllAccess.Checked)
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkAccess"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkAccess"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkSelAllPause_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelAllPause.Checked)
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                       dgvGroupPrivileges.Rows[i].Cells["ChkPause"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkPause"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkSelAllSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelAllSave.Checked)
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkSave"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkSave"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
       
        }

        private void chkSelAllDelete_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelAllDelete.Checked)
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkDelete"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkDelete"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { 
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); 
            }
       
        }

        private void chkSelAllView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelAllView.Checked)
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkView"].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvGroupPrivileges.RowCount; i++)
                    {
                        dgvGroupPrivileges.Rows[i].Cells["ChkView"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkUpdateAllUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUpdateAllUser.Checked) { updateAllUsers = true; }
                else { updateAllUsers = false; }
            }
            catch (Exception ex)
            {
               Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

       
       
    }
}
