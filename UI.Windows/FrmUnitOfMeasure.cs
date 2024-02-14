using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Domain;
using Service;
using Utility;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmUnitOfMeasure : UI.Windows.FrmBaseMasterForm
    {
        ErrorMessage errorMessage = new ErrorMessage();
        
        private UnitOfMeasure existingUnitOfMeasure;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmUnitOfMeasure()
        {
            InitializeComponent();
        }

        #region Form Events
        private void FrmUnitOfMeasure_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationUnit.Checked = true;
                InitializeForm();
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
                
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    txtUnitOfMeasureCode.Text = unitOfMeasureService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtUnitOfMeasureCode);
                    Common.ClearTextBox(txtUnitOfMeasureName, txtRemark);
                    txtUnitOfMeasureName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtUnitOfMeasureCode);
                    txtUnitOfMeasureCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitOfMeasureCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    DataView dvAllReferenceData = new DataView(unitOfMeasureService.GetAllUnitOfMeasuresDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtUnitOfMeasureCode);
                        txtUnitOfMeasureCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtUnitOfMeasureName);
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

        private void txtUnitOfMeasureName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    DataView dvAllReferenceData = new DataView(unitOfMeasureService.GetAllUnitOfMeasuresDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtUnitOfMeasureCode);
                        txtUnitOfMeasureCode_Leave(this, e);
                    }
                }
                Common.SetFocus(e, txtRemark);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            } 
        }

        private void txtUnitOfMeasureName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtUnitOfMeasureName.Text.Trim() != string.Empty)
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    existingUnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureByName(txtUnitOfMeasureName.Text.Trim());

                    if (existingUnitOfMeasure != null)
                    {
                        txtUnitOfMeasureCode.Text = existingUnitOfMeasure.UnitOfMeasureCode.Trim();
                        txtUnitOfMeasureName.Text = existingUnitOfMeasure.UnitOfMeasureName.Trim();
                        txtRemark.Text = existingUnitOfMeasure.Remark.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtUnitOfMeasureName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtUnitOfMeasureCode);
                    }

                    txtRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitOfMeasureCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtUnitOfMeasureCode.Text.Trim() != string.Empty)
                {
                    UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                    existingUnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureByCode(txtUnitOfMeasureCode.Text.Trim());

                    if (existingUnitOfMeasure != null)
                    {
                        txtUnitOfMeasureCode.Text = existingUnitOfMeasure.UnitOfMeasureCode.Trim();
                        txtUnitOfMeasureName.Text = existingUnitOfMeasure.UnitOfMeasureName.Trim();
                        txtRemark.Text = existingUnitOfMeasure.Remark.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtUnitOfMeasureName.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtUnitOfMeasureName, txtRemark);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show(this.Text + " - " + txtUnitOfMeasureCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes)) 
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtUnitOfMeasureCode);
                    }

                    txtUnitOfMeasureName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                Common.SetAutoComplete(txtUnitOfMeasureCode, unitOfMeasureService.GetUnitOfMeasureCodes(), chkAutoCompleationUnit.Checked);
                Common.SetAutoComplete(txtUnitOfMeasureName, unitOfMeasureService.GetUnitOfMeasureNames(), chkAutoCompleationUnit.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtUnitOfMeasureName_Validated(object sender, EventArgs e)
        {
            try
            {
                txtUnitOfMeasureName.Text = txtUnitOfMeasureName.Text.ToLower();
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
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

                Common.SetAutoComplete(txtUnitOfMeasureCode, unitOfMeasureService.GetUnitOfMeasureCodes(), chkAutoCompleationUnit.Checked);
                Common.SetAutoComplete(txtUnitOfMeasureName, unitOfMeasureService.GetUnitOfMeasureNames(), chkAutoCompleationUnit.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtUnitOfMeasureCode);
                Common.ClearTextBox(txtUnitOfMeasureCode);

                ActiveControl = txtUnitOfMeasureCode;
                txtUnitOfMeasureCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            try
            {
                if (!ValidateControls()) {return;}

                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                bool isNew = false;
                existingUnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureByCode(txtUnitOfMeasureCode.Text.Trim());

                if (existingUnitOfMeasure == null || existingUnitOfMeasure.UnitOfMeasureID == 0)
                {
                    existingUnitOfMeasure = new UnitOfMeasure();
                    isNew = true;
                }

                existingUnitOfMeasure.UnitOfMeasureCode = txtUnitOfMeasureCode.Text.Trim();
                existingUnitOfMeasure.UnitOfMeasureName = txtUnitOfMeasureName.Text.Trim();
                existingUnitOfMeasure.Remark = txtRemark.Text.Trim();

                if (existingUnitOfMeasure.UnitOfMeasureID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    unitOfMeasureService.AddUnitOfMeasure(existingUnitOfMeasure);

                    if ((Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    unitOfMeasureService.UpdateUnitOfMeasure(existingUnitOfMeasure);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtUnitOfMeasureCode.Focus();
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
                if (Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {return;}

                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                existingUnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureByCode(txtUnitOfMeasureCode.Text.Trim());

                if (existingUnitOfMeasure != null && existingUnitOfMeasure.UnitOfMeasureID != 0)
                {
                    existingUnitOfMeasure.IsDelete = true;
                    unitOfMeasureService.UpdateUnitOfMeasure(existingUnitOfMeasure);
                    ClearForm();
                    Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtUnitOfMeasureCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + existingUnitOfMeasure.UnitOfMeasureCode + " - " + existingUnitOfMeasure.UnitOfMeasureName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtUnitOfMeasureCode, txtUnitOfMeasureName);
        }
        #endregion

        
    }
}
