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
    public partial class FrmManualPasswordChange : Form
    {
        UserMaster existingUserMaster = new UserMaster();

        public FrmManualPasswordChange()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
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
                    txtConfirmPassword.Enabled = true;
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
                userPrivilegesService.ChangeUserPasswordManually(existingUserMaster, txtNewPassword.Text);
                this.Cursor = Cursors.Default;
                
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
                txtOldPassword.Enabled = false;
                txtNewPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                btnSave.Enabled = false;

                cmbUserName.Text = string.Empty;
                txtUserSescription.Text = string.Empty;

                this.ActiveControl = cmbUserName;
                cmbUserName.Enabled = true;
                cmbUserName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmManualPasswordChange_Load(object sender, EventArgs e)
        {
            try
            {
                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                Common.LoadAllUserAccounts(cmbUserName, userPrivilegesService.GetAllUserAccounts());

                this.ActiveControl = cmbUserName;
                cmbUserName.Focus();
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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    UserMaster userMaster = new UserMaster();
                    UserPrivilegesService userPrivilegesService = new UserPrivilegesService();

                    userMaster = userPrivilegesService.getUserMasterByUserName(cmbUserName.Text.Trim());

                    if (userMaster != null)
                    {
                        existingUserMaster = userMaster;
                        txtUserSescription.Text = userMaster.UserDescription;

                        if (userMaster.IsUserCantChangePassword)
                        {
                            Toast.Show("User- " + userMaster.UserName + "\nDo not have privilages to change password", Toast.messageType.Information, Toast.messageAction.General);
                            cmbUserName.Focus();
                            return;
                        }
                        else
                        {
                            cmbUserName.Enabled = false;
                            txtOldPassword.Enabled = true;
                            txtOldPassword.Focus();
                        }
                    }
                }
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
                        txtNewPassword.Enabled = true;
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
