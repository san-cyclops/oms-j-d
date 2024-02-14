using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Utility;
using Service;
using System.Linq;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmCustomerGroup : UI.Windows.FrmBaseMasterForm
    {
        
        private CustomerGroup existingCustomerGroup;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;
                

        public FrmCustomerGroup()
        {
            InitializeComponent();
        }

        private void FrmCustomerGroup_Load(object sender, EventArgs e)
        {
            
        }

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;

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
                CustomerGroupService customerGroupService = new CustomerGroupService();

                Common.SetAutoComplete(txtGroupCode, customerGroupService.GetAllCustomerGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, customerGroupService.GetAllCustomerGroupNames(), chkAutoCompleationGroup.Checked);
                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtGroupCode);
                Common.ClearTextBox(txtGroupCode);

                ActiveControl = txtGroupCode;
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private  bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtGroupName) ;
              
        }

        public override void Save()
        {
            try
            {
                if (ValidateControls()==false) return;
                CustomerGroupService customerGroupService = new CustomerGroupService();
                bool isNew=false;
                existingCustomerGroup = customerGroupService.GetCustomerGroupsByCode(txtGroupCode.Text.Trim());

                if (existingCustomerGroup == null || existingCustomerGroup.CustomerGroupID == 0)
                {
                    existingCustomerGroup = new CustomerGroup();
                    isNew = true;
                }

                existingCustomerGroup.CustomerGroupCode = txtGroupCode.Text.Trim();
                existingCustomerGroup.CustomerGroupName = txtGroupName.Text.Trim();
                existingCustomerGroup.Remark = txtRemark.Text.Trim();

                if (existingCustomerGroup.CustomerGroupID == 0)
                {
                    if ((Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    customerGroupService.AddCustomerGroup(existingCustomerGroup);

                    if ((Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        btnNew.Enabled = true;
                        btnNew.PerformClick();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            ClearForm();
                        }
                        else
                        {
                            InitializeForm();
                        }
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    customerGroupService.UpdateCustomerGroup(existingCustomerGroup);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void Delete()
        {
            try
            {
                if (Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;
                
                CustomerGroupService customerGroupService = new CustomerGroupService();
                existingCustomerGroup = customerGroupService.GetCustomerGroupsByCode(txtGroupCode.Text.Trim());

                if (existingCustomerGroup != null && existingCustomerGroup.CustomerGroupID != 0)
                {


                    existingCustomerGroup.IsDelete = true;
                    customerGroupService.UpdateCustomerGroup(existingCustomerGroup);
                    ClearForm();
                    Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtGroupCode.Focus();
                }
                else
                    Toast.Show("Customer Group - " + existingCustomerGroup.CustomerGroupCode + " - " + existingCustomerGroup.CustomerGroupName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    
        private void txtGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    CustomerGroupService customerGroupService = new CustomerGroupService();
                    DataView dvAllReferenceData = new DataView(customerGroupService.GetAllVustomerGroupDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }
                if (txtGroupCode.Text.Trim() != string.Empty)
                    txtGroupName.Focus();
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

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    CustomerGroupService customerGroupService = new CustomerGroupService();
                    DataView dvAllReferenceData = new DataView(customerGroupService.GetAllVustomerGroupDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtRemark.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            } 
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { return; }
                if (txtGroupName.Text.Trim() != string.Empty)
                {
                    CustomerGroupService customerGroupService = new CustomerGroupService();
                    existingCustomerGroup = customerGroupService.GetCustomerGroupByName(txtGroupName.Text.Trim());

                    if (existingCustomerGroup != null)
                    {
                        txtGroupCode.Text = existingCustomerGroup.CustomerGroupCode;
                        txtGroupName.Text = existingCustomerGroup.CustomerGroupName;
                        txtRemark.Text = existingCustomerGroup.Remark;
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                            Common.ClearTextBox(txtRemark);
                        if (btnNew.Enabled)
                            if (Toast.Show("Customer Group - " + txtGroupCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                                btnNew.PerformClick();


                    }
                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtGroupCode);

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
      

        private void txtGroupCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupCode.Text.Trim() != string.Empty)
                {
                    CustomerGroupService customerGroupService = new CustomerGroupService();

                    existingCustomerGroup = customerGroupService.GetCustomerGroupsByCode(txtGroupCode.Text.Trim());

                    if (existingCustomerGroup != null)
                    {
                        txtGroupCode.Text = existingCustomerGroup.CustomerGroupCode;
                        txtGroupName.Text = existingCustomerGroup.CustomerGroupName;
                        txtRemark.Text = existingCustomerGroup.Remark;
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtGroupName.Focus();
                    }
                    else
                    {
                        if(chkAutoClear.Checked)
                            Common.ClearTextBox(txtGroupName, txtRemark);
                        if (btnNew.Enabled)
                            if (Toast.Show("Customer Group - " + txtGroupCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                                btnNew.PerformClick();                                                   


                    }
                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtGroupCode);
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
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    CustomerGroupService customerGroupService = new CustomerGroupService();
                    txtGroupCode.Text = customerGroupService.GetNewCode(this.Name);
                    Common.EnableTextBox(false, txtGroupCode);
                    txtGroupName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtGroupCode);
                    txtGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CustomerGroupService customerGroupService = new CustomerGroupService();
                Common.SetAutoComplete(txtGroupCode, customerGroupService.GetAllCustomerGroupsArray().ToArray(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, customerGroupService.GetAllCustomerGroupNames(), chkAutoCompleationGroup.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        

        


    }
}
