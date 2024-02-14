using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;

namespace UI.Windows
{
    public partial class FrmGiftVoucherGroup : UI.Windows.FrmBaseMasterForm
    {
        ErrorMessage errorMessage = new ErrorMessage();

        private InvGiftVoucherGroup existingGiftVoucherGroup;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmGiftVoucherGroup()
        {
            InitializeComponent();
        }

        #region Form Events
        /// <summary>
        /// Get new code on user demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                if (chkAutoClear.Checked)
                { Common.ClearTextBox(txtGroupCode, txtGroupName, txtRemark); }

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    txtGroupCode.Text = invGiftVoucherMasterGroupService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtGroupCode);
                    Common.ClearTextBox(txtGroupName, txtRemark);
                    txtGroupName.Focus();
                }
                else
                {
                    txtGroupCode.Focus();
                }
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
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtGroupName);
                txtGroupName.SelectionStart = txtGroupName.Text.Length;
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
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    DataView dvAllReferenceData = new DataView(invGiftVoucherMasterGroupService.GetAllActiveGiftVoucherGroupsDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), txtGroupCode);
                        txtGroupCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtRemark);
                txtRemark.SelectionStart = txtRemark.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtGroupCode_Validated(object sender, EventArgs e)
        {
            
        }

        private void txtGroupName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtGroupName.Text.Trim() != string.Empty)
                {
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    existingGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByName(txtGroupName.Text.Trim());

                    if (existingGiftVoucherGroup != null)
                    {
                        txtGroupCode.Text = existingGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingGiftVoucherGroup.GiftVoucherGroupName.Trim();
                        txtRemark.Text = existingGiftVoucherGroup.Remark.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtGroupName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    txtRemark.Focus();
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
                    InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                    existingGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

                    if (existingGiftVoucherGroup != null)
                    {
                        txtGroupCode.Text = existingGiftVoucherGroup.GiftVoucherGroupCode.Trim();
                        txtGroupName.Text = existingGiftVoucherGroup.GiftVoucherGroupName.Trim();
                        txtRemark.Text = existingGiftVoucherGroup.Remark.Trim();
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
                            if (Toast.Show(this.Text + " - " + txtGroupCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtGroupCode);
                    }

                    txtGroupName.Focus();
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
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        #endregion

        #region Methods
        public override void InitializeForm()
        {
            try
            {
                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();

                Common.SetAutoComplete(txtGroupCode, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupCodes(), chkAutoCompleationGroup.Checked);
                Common.SetAutoComplete(txtGroupName, invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupNames(), chkAutoCompleationGroup.Checked);

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

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                existingGiftVoucherGroup = null;


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

        public override void Save()
        {
            try
            {
                if (!ValidateControls()) { return; }

                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                bool isNew = false;
                existingGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

                if (existingGiftVoucherGroup == null || existingGiftVoucherGroup.InvGiftVoucherGroupID == 0)
                {
                    existingGiftVoucherGroup = new InvGiftVoucherGroup();
                    isNew = true;
                }

                existingGiftVoucherGroup.GiftVoucherGroupCode = txtGroupCode.Text.Trim();
                existingGiftVoucherGroup.GiftVoucherGroupName = txtGroupName.Text.Trim();
                existingGiftVoucherGroup.Remark = txtRemark.Text.Trim();

                if (existingGiftVoucherGroup.InvGiftVoucherGroupID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    invGiftVoucherMasterGroupService.AddInvGiftVoucherGroup(existingGiftVoucherGroup);
                    
                    if ((Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
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
                        if ((Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    invGiftVoucherMasterGroupService.UpdateInvGiftVoucherGroup(existingGiftVoucherGroup);
                    Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Information, Toast.messageAction.Modify);

                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
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
                if (Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                InvGiftVoucherGroupService invGiftVoucherMasterGroupService = new InvGiftVoucherGroupService();
                existingGiftVoucherGroup = invGiftVoucherMasterGroupService.GetInvGiftVoucherGroupByCode(txtGroupCode.Text.Trim());

                if (existingGiftVoucherGroup != null && existingGiftVoucherGroup.InvGiftVoucherGroupID != 0)
                {
                    existingGiftVoucherGroup.IsDelete = true;
                    invGiftVoucherMasterGroupService.UpdateInvGiftVoucherGroup(existingGiftVoucherGroup);
                    Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtGroupCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + existingGiftVoucherGroup.GiftVoucherGroupCode + " - " + existingGiftVoucherGroup.GiftVoucherGroupName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            existingGiftVoucherGroup = null;
            base.ClearForm();
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

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtGroupCode, txtGroupName);
        }
        #endregion
        
    }
}
