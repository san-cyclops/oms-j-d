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
    public partial class FrmSupplierGroup : FrmBaseMasterForm
    {
        AutoCompleteStringCollection autoCompleteSupplierGroupCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteSupplierGroupName = new AutoCompleteStringCollection();
        private SupplierGroup existingSupplierGroup;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmSupplierGroup()
        {
            InitializeComponent();
        }

        private void FrmSupplierGroup_Load(object sender, EventArgs e)
        {
            
        }

        #region Override Methods....

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
                SupplierGroupService supplierGroupService = new SupplierGroupService();
                autoCompleteSupplierGroupCode.AddRange(supplierGroupService.GetAllSupplierGroupCodes());
                Common.SetAutoComplete(txtGroupCode, autoCompleteSupplierGroupCode, chkAutoCompleationGroup.Checked);

                autoCompleteSupplierGroupName.AddRange(supplierGroupService.GetAllSupplierGroupNames());
                Common.SetAutoComplete(txtGroupName, autoCompleteSupplierGroupName, chkAutoCompleationGroup.Checked);


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

        public override void Save()
        {
            try
            {
                if (ValidateControls().Equals(false))
                {
                    return;
                }

                SupplierGroupService supplierGroupService = new SupplierGroupService();
                bool isNew = false;
                existingSupplierGroup = supplierGroupService.GetSupplierGroupsByCode(txtGroupCode.Text.Trim());

                if (existingSupplierGroup == null || existingSupplierGroup.SupplierGroupID == 0)
                {
                    existingSupplierGroup = new SupplierGroup();
                    isNew = true;
                }

                existingSupplierGroup.SupplierGroupCode = txtGroupCode.Text.Trim();
                existingSupplierGroup.SupplierGroupName = txtGroupName.Text.Trim();
                existingSupplierGroup.Remark = txtRemark.Text.Trim();

                if (existingSupplierGroup.SupplierGroupID == 0)
                {
                    if ((Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    supplierGroupService.AddSupplierGroup(existingSupplierGroup);

                    if ((Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    supplierGroupService.UpdateSupplierGroup(existingSupplierGroup);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Information, Toast.messageAction.Modify);
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
                if (Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                SupplierGroupService supplierGroupService = new SupplierGroupService();
                existingSupplierGroup = supplierGroupService.GetSupplierGroupsByCode(txtGroupCode.Text.Trim());

                if (existingSupplierGroup != null && existingSupplierGroup.SupplierGroupID != 0)
                {
                    supplierGroupService.DeleteSupplierGroup(existingSupplierGroup);

                    Toast.Show("Supplier Group - " + existingSupplierGroup.SupplierGroupCode + " - " + existingSupplierGroup.SupplierGroupName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                Common.EnableButton(false, btnDelete, btnNew);
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete.Equals(true))
                {
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    txtGroupCode.Text = supplierGroupService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
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

        #endregion

        #region KeyDown and Leave Events....

        private void txtGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    DataView dvAllReferenceData = new DataView(supplierGroupService.GetSupplierGroupDataTable());
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
                txtGroupName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
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

        private void txtGroupCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGroupCode.Text.Trim()))
                {
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    existingSupplierGroup = supplierGroupService.GetSupplierGroupsByCode(txtGroupCode.Text.Trim());

                    if (existingSupplierGroup != null)
                    {
                        txtGroupCode.Text = existingSupplierGroup.SupplierGroupCode.Trim();
                        txtGroupName.Text = existingSupplierGroup.SupplierGroupName.Trim();
                        txtRemark.Text = existingSupplierGroup.Remark;
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtGroupName.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtGroupName, txtRemark);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Supplier Group - " + txtGroupCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }
                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtGroupCode);
                    }
                    //txtGroupName.Focus();
                }
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
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    DataView dvAllReferenceData = new DataView(supplierGroupService.GetSupplierGroupDataTable());
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
                { 
                    return; 
                }
                if (!string.IsNullOrEmpty(txtGroupName.Text.Trim()))
                {
                    SupplierGroupService supplierGroupService = new SupplierGroupService();
                    existingSupplierGroup = supplierGroupService.GetSupplierGroupsByName(txtGroupName.Text.Trim());

                    if (existingSupplierGroup != null)
                    {
                        txtGroupCode.Text = existingSupplierGroup.SupplierGroupCode.Trim();
                        txtGroupName.Text = existingSupplierGroup.SupplierGroupName.Trim();
                        txtRemark.Text = existingSupplierGroup.Remark.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("Supplier Group - " + txtGroupName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtRemark);
                        }
                        txtRemark.Focus(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods....

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtGroupName);
        }

        private void chkAutoCompleationGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtGroupCode, autoCompleteSupplierGroupCode, chkAutoClear.Checked);
                Common.SetAutoComplete(txtGroupName, autoCompleteSupplierGroupName, chkAutoClear.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion
    }
}

