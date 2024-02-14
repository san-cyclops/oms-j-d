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
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan 
    /// </summary>
    public partial class FrmDesignationType : FrmBaseMasterForm
    {
        private EmployeeDesignationType existingEmployeeDesignationType;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmDesignationType()
        {
            InitializeComponent();
        }
           
        #region Button Click Events....

        /// <summary>
        /// New button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    DesignationTypeService designationTypeService = new DesignationTypeService();
                    txtDesignationCode.Text = designationTypeService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtDesignationCode);
                    txtDesignation.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtDesignationCode);
                    txtDesignationCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
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
                EmployeeDesignationType employeeDesignationType = new EmployeeDesignationType();
                DesignationTypeService designationTypeService = new DesignationTypeService();

                List<EmployeeDesignationType> employeeDesignationTypeList = new List<EmployeeDesignationType>();
                employeeDesignationTypeList = designationTypeService.GetAllDesignationTypes();

                Common.SetAutoComplete(txtDesignationCode, designationTypeService.GetAllDesignationCodes(), chkAutoCompleationDesignationType.Checked);
                Common.SetAutoComplete(txtDesignation, designationTypeService.GetAllDesignations(), chkAutoCompleationDesignationType.Checked);
                ////

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtDesignationCode);
                Common.ClearTextBox(txtDesignationCode);

                ActiveControl = txtDesignationCode;
                txtDesignationCode.Focus();
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

                DesignationTypeService designationTypeService = new DesignationTypeService();
                bool isNew = false;
                existingEmployeeDesignationType = designationTypeService.GetesignationTypeByCode(txtDesignationCode.Text.Trim());

                if (existingEmployeeDesignationType == null || existingEmployeeDesignationType.EmployeeDesignationTypeID == 0)
                {
                    existingEmployeeDesignationType = new EmployeeDesignationType();
                    isNew = true;
                }

                existingEmployeeDesignationType.DesignationCode = txtDesignationCode.Text.Trim();
                existingEmployeeDesignationType.Designation = txtDesignation.Text.Trim();
                existingEmployeeDesignationType.Remarks = txtRemark.Text.Trim();

                if (existingEmployeeDesignationType.EmployeeDesignationTypeID == 0)
                {
                    if ((Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    designationTypeService.AddDesignations(existingEmployeeDesignationType);

                    if ((Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    designationTypeService.UpdateDesignationType(existingEmployeeDesignationType);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtDesignationCode.Focus();
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
                if (Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                DesignationTypeService designationTypeService = new DesignationTypeService();
                existingEmployeeDesignationType = designationTypeService.GetesignationTypeByCode(txtDesignationCode.Text.Trim());

                if (existingEmployeeDesignationType != null && existingEmployeeDesignationType.EmployeeDesignationTypeID != 0)
                {
                    designationTypeService.DeleteDesignationType(existingEmployeeDesignationType);

                    Toast.Show("Designation type - " + existingEmployeeDesignationType.DesignationCode + " - " + existingEmployeeDesignationType.Designation + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtDesignationCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void CloseForm()
        {
            try
            {            
                base.CloseForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region KeyDown and Leave Events....

        private void txtDesignationCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    DesignationTypeService designationTypeService = new DesignationTypeService();
                    DataView dvAllReferenceData = new DataView(designationTypeService.GetAllActiveDesignationTypeDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDesignationCode);
                        txtDesignationCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { 
                    return; 
                }
                txtDesignation.Focus();
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

        private void txtDesignationCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDesignationCode.Text.Trim() != string.Empty)
                {
                    DesignationTypeService designationTypeService = new DesignationTypeService();
                    existingEmployeeDesignationType = designationTypeService.GetesignationTypeByCode(txtDesignationCode.Text.Trim());

                    if (existingEmployeeDesignationType != null)
                    {
                        txtDesignationCode.Text = existingEmployeeDesignationType.DesignationCode.Trim();
                        txtDesignation.Text = existingEmployeeDesignationType.Designation.Trim();
                        txtRemark.Text = existingEmployeeDesignationType.Remarks.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtDesignation.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtDesignation, txtRemark);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Designation type - " + txtDesignationCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtDesignationCode);
                    }

                    txtDesignation.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    DesignationTypeService designationTypeService = new DesignationTypeService();
                    DataView dvAllReferenceData = new DataView(designationTypeService.GetAllActiveDesignationTypeDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDesignationCode);
                        txtDesignationCode_Leave(this, e);
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

        private void txtDesignation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { 
                    return; 
                }
                if (!string.IsNullOrEmpty(txtDesignation.Text.Trim()))
                {
                    DesignationTypeService designationTypeService = new DesignationTypeService();
                    existingEmployeeDesignationType = designationTypeService.GetDesignationTypeByName(txtDesignation.Text.Trim());

                    if (existingEmployeeDesignationType != null)
                    {
                        txtDesignationCode.Text = existingEmployeeDesignationType.DesignationCode.Trim();
                        txtDesignation.Text = existingEmployeeDesignationType.Designation.Trim();
                        txtRemark.Text = existingEmployeeDesignationType.Remarks.Trim();

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtDesignation.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                    txtDesignation.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods....

        private void chkAutoCompleationDesignationType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DesignationTypeService designationTypeService = new DesignationTypeService();
                Common.SetAutoComplete(txtDesignationCode, designationTypeService.GetAllDesignationCodes(), chkAutoCompleationDesignationType.Checked);
                Common.SetAutoComplete(txtDesignationCode, designationTypeService.GetAllDesignations(), chkAutoCompleationDesignationType.Checked); 
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDesignationCode, txtDesignation);
        }

        #endregion

    }
}
