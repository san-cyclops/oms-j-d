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
    public partial class FrmArea : FrmBaseMasterForm
    {
        private Area existingArea;
        AutoCompleteStringCollection autoCompleteArea;
        bool isDependTerritory = false;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmArea()
        {
            InitializeComponent();
        }
           
        private void FrmArea_Load(object sender, EventArgs e)
        {
            
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
                    AreaService areaService = new AreaService();
                    txtAreaCode.Text = areaService.GetNewCode(AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Prefix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).Suffix, AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).CodeLength);
                    Common.EnableTextBox(false, txtAreaCode);
                    txtAreaName.Focus();
                }
                else
                {
                    Common.EnableTextBox(true, txtAreaCode);
                    txtAreaCode.Focus();
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

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Override Methods....

        public override void FormLoad()
        {
            try
            {
                isDependTerritory = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmTerritory").IsDepend;
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
                Area area = new Area();
                AreaService areaService = new AreaService();

                List<Area> areas = new List<Area>();
                areas = areaService.GetAllAreas();

                //// Auto complete area code
                autoCompleteArea = new AutoCompleteStringCollection();
                for (int i = 0; i < areas.Count; i++)
                {
                    autoCompleteArea.Add(areas[i].AreaCode.Trim());
                }
                Common.SetAutoComplete(txtAreaCode, autoCompleteArea, chkAutoCompleationAreaCode.Checked);
                ////

                //// Auto complete area name
                autoCompleteArea = new AutoCompleteStringCollection();
                for (int i = 0; i < areas.Count; i++)
                {
                    autoCompleteArea.Add(areas[i].AreaName.Trim());
                }
                Common.SetAutoComplete(txtAreaName, autoCompleteArea, chkAutoCompleationAreaCode.Checked);
                ////

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtAreaCode);
                Common.ClearTextBox(txtAreaCode);

                ActiveControl = txtAreaCode;
                txtAreaCode.Focus();
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
                AreaService areaService = new AreaService();
                bool isNew = false;
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());

                if (existingArea == null || existingArea.AreaID == 0)
                {
                    existingArea = new Area();
                    isNew = true;
                }

                existingArea.AreaCode = txtAreaCode.Text.Trim();
                existingArea.AreaName = txtAreaName.Text.Trim();
                existingArea.Remark = txtRemark.Text.Trim();

                if (existingArea.AreaID == 0)
                {
                    if ((Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    areaService.AddArea(existingArea);

                    if ((Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
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
                        if ((Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                       
                        if ((Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    areaService.UpdateArea(existingArea);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtAreaCode.Focus();
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
                if (Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }
                
                AreaService areaService = new AreaService();
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());

                if (existingArea != null && existingArea.AreaID != 0)
                {
                    areaService.DeleteArea(existingArea);

                    Toast.Show("Area - " + existingArea.AreaCode + " - " + existingArea.AreaName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtAreaCode.Focus();
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

        private void txtAreaCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AreaService areaService = new AreaService();
                    DataView dvAllReferenceData = new DataView(areaService.GetAllActiveAreasDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtAreaCode);
                        txtAreaCode_Leave(this, e);
                    }
                }

                if (!e.KeyCode.Equals(Keys.Enter))
                { 
                    return; 
                }
                txtAreaName.Focus();
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

        private void txtAreaCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtAreaCode.Text.Trim() != string.Empty)
                {
                    AreaService areaService = new AreaService();
                    existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());

                    if (existingArea != null)
                    {
                        txtAreaCode.Text = existingArea.AreaCode.Trim();
                        txtAreaName.Text = existingArea.AreaName.Trim();
                        txtRemark.Text = existingArea.Remark.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtAreaName.Focus();
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtAreaName, txtRemark);
                        }
                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Area - " + txtAreaCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            {
                                btnNew.PerformClick();
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtAreaCode);
                    }

                    txtAreaName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtAreaName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AreaService areaService = new AreaService();
                    DataView dvAllReferenceData = new DataView(areaService.GetAllActiveAreasDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtAreaCode);
                        txtAreaCode_Leave(this, e);
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

        private void txtAreaName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                { 
                    return; 
                }
                if (!string.IsNullOrEmpty(txtAreaName.Text.Trim()))
                {
                    AreaService areaService = new AreaService();
                    existingArea = areaService.GetAreasByName(txtAreaName.Text.Trim());

                    if (existingArea != null)
                    {
                        txtAreaCode.Text = existingArea.AreaCode.Trim();
                        txtAreaName.Text = existingArea.AreaName.Trim();
                        txtRemark.Text = existingArea.Remark.Trim();

                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtRemark.Focus();
                    }
                    else
                    {
                        Toast.Show("" + this.Text + " - " + txtAreaName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }
                    txtAreaName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtRemark_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods....

        private void chkAutoCompleationArea_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtAreaCode, autoCompleteArea, chkAutoCompleationAreaCode.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtAreaCode, txtAreaName);
        }

        #endregion

    }
}
