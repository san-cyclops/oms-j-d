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
using System.Drawing.Printing;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmPasswordChange : Form
    {
        UserMaster existingUserMaster = new UserMaster();
        bool passwordChanged = false;

        public FrmPasswordChange()
        {
            InitializeComponent();
        }

        public FrmPasswordChange(UserMaster userMaster)
        {
            InitializeComponent();

            existingUserMaster = userMaster;

            UserGroup userGroup = new UserGroup();
            UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
            
            userGroup = userPrivilegesService.GetUserGroupByID(userMaster.UserGroupID);

            txtUserName.Text = userMaster.UserName;
            txtUserSescription.Text = userMaster.UserDescription;
            
            if(userGroup!=null)
            {
                txtUserGroup.Text = userGroup.UserGroupName;
            }

            this.ActiveControl = txtOldPassword;
            txtOldPassword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                passwordChanged = false;
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtConfirmPassword.Focus();
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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtConfirmPassword_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.Equals(txtNewPassword.Text.Trim(), txtConfirmPassword.Text.Trim()))
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.ConfirmPassword);
                    txtConfirmPassword.SelectAll();
                    txtConfirmPassword.Focus();
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtNewPassword.Text.Trim()) && !string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                    {
                        btnSave.Enabled = true;
                        btnSave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();

                this.Cursor = Cursors.WaitCursor;
                userPrivilegesService.ChangeUserPassword(existingUserMaster, txtNewPassword.Text);
                this.Cursor = Cursors.Default;

                passwordChanged = true;
                Toast.Show("Password changed successfully", Toast.messageType.Information, Toast.messageAction.General);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmPasswordChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                FrmLoginUser frmLoginUser = new FrmLoginUser();
                frmLoginUser.SetResult(passwordChanged);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                this.ActiveControl = txtNewPassword;
                txtNewPassword.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtOldPassword_Leave(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtOldPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOldPassword.Text.Trim()))
                {
                    if (!string.Equals(existingUserMaster.Password, txtOldPassword.Text.Trim()))
                    {
                        Toast.Show("Incorrect old password", Toast.messageType.Information, Toast.messageAction.General);
                        txtOldPassword.SelectAll();
                        txtOldPassword.Focus();
                        return;
                    }
                    else
                    {
                        txtNewPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
    }
}
