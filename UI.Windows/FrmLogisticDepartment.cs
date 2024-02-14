using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Domain;
using Report.Logistic;
using Utility;
using Service;
using System.Reflection;

namespace UI.Windows
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public partial class FrmLogisticDepartment : FrmBaseMasterForm
    {
        private LgsDepartment existingDeparment;
        AutoCompleteStringCollection autoCompleteDepartmentCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteDepartmentName = new AutoCompleteStringCollection();
        UserPrivileges accessRights = new UserPrivileges();
        int documentID;

        bool isDependLgsCategory = false;

        public FrmLogisticDepartment()
        {
            InitializeComponent();
        }

        private void FrmLogisticDepartment_Load(object sender, EventArgs e)
        {

        }

        #region Override Methods...

        public override void FormLoad()
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                isDependLgsCategory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticCategory").IsDepend;
                this.Text = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).FormText.Trim();
                lblDepartmentCode.Text = this.Text + " " + lblDepartmentCode.Text;
                lblDepartmentName.Text = this.Text + " " + lblDepartmentName.Text;

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

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                autoCompleteDepartmentCode.Clear();
                autoCompleteDepartmentCode.AddRange(lgsDepartmentService.GetAllLgsDepartmentCodes(isDependLgsCategory));
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode, chkAutoCompleationDepartment.Checked);
                autoCompleteDepartmentName.Clear();
                autoCompleteDepartmentName.AddRange(lgsDepartmentService.GetAllLgsDepartmentNames(isDependLgsCategory));
                Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName, chkAutoCompleationDepartment.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtDepartmentCode);
                Common.ClearTextBox(txtDepartmentCode);

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights.IsView == true) Common.EnableButton(true, btnView);

                ActiveControl = txtDepartmentCode;
                txtDepartmentCode.Focus();
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
                if (!ValidateControls())
                { return; }

                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                bool isNew = false;
                existingDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependLgsCategory);

                if (existingDeparment == null || existingDeparment.LgsDepartmentID == 0)
                { existingDeparment = new LgsDepartment(); }

                existingDeparment.DepartmentCode = txtDepartmentCode.Text.Trim();
                existingDeparment.DepartmentName = txtDepartmentName.Text.Trim();
                existingDeparment.Remark = txtRemark.Text.Trim();
                existingDeparment.IsDelete = false;

                if (existingDeparment.LgsDepartmentID.Equals(1) && isDependLgsCategory.Equals(false))
                    return;

                if (existingDeparment.LgsDepartmentID.Equals(0))
                {
                    if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { return; }
                    // Create new Department
                    lgsDepartmentService.AddLgsDepartment(existingDeparment);

                    if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        { ClearForm(); }
                        else
                        { InitializeForm(); }
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { return; }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { return; }
                    }
                    // Update Department deatils
                    lgsDepartmentService.UpdateLgsDepartment(existingDeparment);
                    if (chkAutoClear.Checked)
                    { ClearForm(); }
                    else
                    { InitializeForm(); }

                    // Display updated iformation 
                    Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtDepartmentCode.Focus();
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
                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                existingDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependLgsCategory);

                if (Toast.Show("" + this.Text + " - " + existingDeparment.DepartmentCode + " - " + existingDeparment.DepartmentName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                if (existingDeparment != null && existingDeparment.LgsDepartmentID != 0)
                {
                    lgsDepartmentService.DeleteLgsDepartment(existingDeparment);
                    MessageBox.Show("" + this.Text + " - " + existingDeparment.DepartmentCode.Trim() + " successfully deleted", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    InitializeForm();
                    txtDepartmentCode.Focus();
                }

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
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticDepartment");
                LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
                lgsReportGenerator.OrganizeFormFields(autoGenerateInfo).Show();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Button Click Events....

        private void btnNew_Click(object sender, System.EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);

                if (accessRights.IsSave == true)  Common.EnableButton(true, btnSave);

                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    txtDepartmentCode.Text = lgsDepartmentService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength, isDependLgsCategory);
                    Common.EnableTextBox(false, txtDepartmentCode);
                    txtDepartmentName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtDepartmentCode);
                    txtDepartmentCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Key Down and Leave Events....

        private void txtDepartmentCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    DataView dvAllReferenceData = new DataView(lgsDepartmentService.GetAllActiveLgsDepartmentsDataTable(isDependLgsCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDepartmentCode);
                        txtDepartmentCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

                txtDepartmentName.Focus();
                txtDepartmentName.SelectionStart = txtDepartmentName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentCode_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepartmentCode.Text.Trim()))
                { return; }

                LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                existingDeparment = lgsDepartmentService.GetLgsDepartmentsByCode(txtDepartmentCode.Text.Trim(), isDependLgsCategory);

                if (existingDeparment != null)
                {
                    txtDepartmentCode.Text = existingDeparment.DepartmentCode.Trim();
                    txtDepartmentName.Text = existingDeparment.DepartmentName.Trim();
                    txtRemark.Text = existingDeparment.Remark.Trim();
                    
                    if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                    if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);

                    Common.EnableButton(false, btnNew);
                }
                else
                {
                    if (chkAutoClear.Checked)
                    { Common.ClearTextBox(txtDepartmentName, txtRemark); }

                    if (btnNew.Enabled)
                    {
                        if (Toast.Show("" + this.Text + " - " + txtDepartmentCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                        { btnNew.PerformClick(); }
                    }
                }

                if (btnSave.Enabled)
                { Common.EnableTextBox(false, txtDepartmentCode); }

                txtDepartmentName.Focus();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    DataView dvAllReferenceData = new DataView(lgsDepartmentService.GetAllActiveLgsDepartmentsDataTable(isDependLgsCategory));
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtDepartmentCode);
                        txtDepartmentCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                {
                    return;
                }
                txtDepartmentName.Focus();
                txtDepartmentName.SelectionStart = txtDepartmentName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtDepartmentName_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { return; }
                if (!string.IsNullOrEmpty(txtDepartmentName.Text.Trim()))
                {
                    LgsDepartmentService lgsDepartmentService = new LgsDepartmentService();
                    existingDeparment = lgsDepartmentService.GetLgsDepartmentsByName(txtDepartmentName.Text.Trim(), isDependLgsCategory);

                    if (existingDeparment != null)
                    {
                        txtDepartmentCode.Text = existingDeparment.DepartmentCode;
                        txtDepartmentName.Text = existingDeparment.DepartmentName;
                        txtRemark.Text = existingDeparment.Remark;

                        
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);

                        Common.EnableButton(false, btnNew);
                        Common.EnableTextBox(false, txtDepartmentCode);
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtDepartmentName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
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
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDepartmentCode, txtDepartmentName);
        }

        private void chkAutoCompleationDepartment_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtDepartmentCode, autoCompleteDepartmentCode, chkAutoCompleationDepartment.Checked);
                Common.SetAutoComplete(txtDepartmentName, autoCompleteDepartmentName, chkAutoCompleationDepartment.Checked);
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

        #endregion

    }
}
