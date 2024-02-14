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
    public partial class FrmLoginUser : Form
    {
        UserMaster userMaster = new UserMaster();
        public static bool passwordChanged = false;

        public FrmLoginUser()
        {
            InitializeComponent();
        }

        private void FrmLoginUser_Load(object sender, EventArgs e)
        {
            
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    if (txtUserName.Text.Trim() != null && txtUserName.Text.Trim() != "")
                    {
                        txtUserName_Leave(this, e);
                        //txtPassword.Enabled = true; 
                        //txtPassword.Focus(); 
                    }
                }
                else if (e.KeyCode.Equals(Keys.Escape))
                {                 
                    //mdiMainRibbon.LoggedStatus(txtUserName.Text.Trim(), cmbLocation.Text.Trim()); 
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
                passwordChanged = false;
                cmbLocation.DataSource = null;
                Common.ClearComboBox(cmbLocation);
                UserPrivilegesService userPrivilegesService = new UserPrivilegesService();
                userMaster = userPrivilegesService.getUserMasterByUserName(txtUserName.Text.Trim());

                if (userMaster != null)
                {
                    userMaster.UserMasterID = userPrivilegesService.getUserMasterByUserName(txtUserName.Text.Trim()).UserMasterID;
                    if (userMaster.IsUserMustChangePassword)
                    {
                        FrmPasswordChange frmPasswordChange = new FrmPasswordChange(userMaster);
                        frmPasswordChange.ShowDialog();
                        if (passwordChanged)
                        {
                            EnableDesableBtn();
                            txtPassword.Enabled = true;
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        EnableDesableBtn();
                        txtPassword.Enabled = true;
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Application.Exit();
                this.Close();
                this.Dispose();
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
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (txtPassword.Text.Trim() != null && txtPassword.Text.Trim() != "" && txtUserName.Text.Trim() != null && txtUserName.Text.Trim() != "")
                    {
                        LoadUserRelaventLocations();
                        cmbLocation.Enabled = true;
                        cmbLocation.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                EnableDesableBtn();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadUserRelaventLocations()
        {
            LocationService locationService = new LocationService();
            if (userMaster != null)
            {
                cmbLocation.DataSource = locationService.GetLocationsByUserId(Common.ConvertStringToInt(userMaster.UserMasterID.ToString()), txtPassword.Text.Trim());
                cmbLocation.DisplayMember = "LocationName";
                cmbLocation.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbLocation.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbLocation.SelectedIndex = -1;
            }
        }


        public void SetResult(bool isChanged)
        {
            passwordChanged = isChanged;
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                {
                    EnableDesableBtn();
                    btnOk.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void EnableDesableBtn()
        {
            if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "" && cmbLocation.SelectedIndex != -1) { btnOk.Enabled = true; }
            else { btnOk.Enabled = false; }
        }

        

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                EnableDesableBtn();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (IsValidUser() == true)
                {

                    mdiMainRibbon.DisebleMenu();
                    mdiMainRibbon.CheckUserRights(userMaster.UserMasterID);
                    
                    mdiMainRibbon.LoggedStatus(txtUserName.Text.Trim(), cmbLocation.Text.Trim());

                    GetLoginDetails();

                    mdiMainRibbon.CheckHeadOfficeRights(Common.isHeadOffice);

                    mdiMainRibbon.btnLogin.Enabled = false;
                    mdiMainRibbon.btnLogOff.Enabled = true;

                    this.Dispose();



                }
                else
                {
                    Toast.Show("Access is Denied !", Toast.messageType.Information, Toast.messageAction.General);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private MdiMainRibbon mdiMainRibbon;

        public FrmLoginUser(MdiMainRibbon mdiMainRibbon)
            : this()
        {
            // TODO: Complete member initialization
            this.mdiMainRibbon = mdiMainRibbon;
        }

        private bool IsValidUser()
        {            
            UserPrivileges userPrivileges = new UserPrivileges();
            LocationService location = new LocationService();
            userPrivileges = CommonService.GetUserPrivilegesByUserIDLocationAndPassword(Common.ConvertStringToLong(userMaster.UserMasterID.ToString()), location.GetLocationsByName(cmbLocation.Text.Trim()).LocationID, txtPassword.Text.Trim());
            
            if (userPrivileges != null) 
                { return true; }
            else 
                { return false; }
           
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EnableDesableBtn();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void EnableDesableMenus()
        {     

            Program.mdi.mnuTools.Enabled = true;

            Program.mdi.rbnPnlReference.Enabled = true;
            Program.mdi.ribbtnSupplierGroup.Enabled = true;
            Program.mdi.ribbtnCustomerGroup.Enabled = true;

            Program.mdi.rbnPnlTransactions.Enabled = true;
            Program.mdi.ribbtnPayment.Enabled = true;
            Program.mdi.ribbtnReceipt.Enabled = true;

            LocationService location = new LocationService();
            CommonService commonService = new CommonService();
            List<UserPrivileges> userPrivilegesList = new List<UserPrivileges>();
            userPrivilegesList = commonService.GetAccessPrivilegesByUserIDandLocation(Common.ConvertStringToLong(userMaster.UserMasterID.ToString()), location.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);



            foreach (UserPrivileges userPrivilegesTemp in userPrivilegesList)
            {
                switch (userPrivilegesTemp.TransactionCode)
                {
                    //case "PO":
                    //    Program.mdi.purchaseOrderToolStripMenuItem.Visible = true;
                    //    break;

                    //case "GRN":
                    //    Program.mdi.goodsReceivedNoteToolStripMenuItem.Visible = true;
                    //    break;

                    //case "IN":
                    //    Program.mdi.invoiceToolStripMenuItem.Visible = true;
                    //    break;

                    //case "SRN":
                    //    Program.mdi.salesReturnNoteSRNToolStripMenuItem.Visible = true;
                    //    break;

                    //case "TAX":
                    //    Common.tStatus = true;
                    //    break;

                }
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Common.ClearComboBox(cmbLocation);
            Common.ClearTextBox(txtUserName, txtPassword);
            btnOk.Enabled = true;
        }

        private void GetLoginDetails()
        {
            UserPrivileges userPrivileges = new UserPrivileges();
            UserPrivilegesService user = new UserPrivilegesService();
            LocationService location = new LocationService();
            CompanyService company = new CompanyService();
            

            Common.LoggedLocationID = location.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;
            Common.LoggedLocationCode = location.GetLocationsByName(cmbLocation.Text.Trim()).LocationCode;
            Common.LoggedCompanyID = location.GetLocationsByName(cmbLocation.Text.Trim()).CompanyID;
            Common.LoggedCompanyName = company.GetCompaniesByID(Common.LoggedCompanyID).CompanyName;
            Common.LoggedCompanyAddress = company.GetCompaniesByID(Common.LoggedCompanyID).Address;

            Common.isHeadOffice = location.GetLocationsByName(cmbLocation.Text.Trim()).IsHeadOffice;
            Common.LoggedLocationName = cmbLocation.Text.Trim();
            Common.LoggedUser = txtUserName.Text.Trim();
            Common.LoggedUserId = user.GetUserByName(txtUserName.Text.Trim()).UserMasterID;
            Common.UserGroupID = user.GetUserByName(txtUserName.Text.Trim()).UserGroupID;

            Common.GroupOfCompanyID = 1;

            Common.AuthorName = "Cynex Soft";
            Common.AuthorAddress = "#Ambalanogda";
            
        }

    }
}
