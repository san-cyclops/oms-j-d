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
    public partial class FrmTerritory : FrmBaseMasterForm
    {
        private Territory existingTerritory;
        private Area existingArea;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        AutoCompleteStringCollection autoCompleteAreaCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteAreaName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteTerritoryCode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoCompleteTerritoryName = new AutoCompleteStringCollection();

        public FrmTerritory()
        {
            InitializeComponent();
        }

        private void FrmTerritory_Load(object sender, EventArgs e)
        {

        }

        
        #region Button Click Events....

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Common.EnableButton(false, btnNew, btnDelete);
                if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);

                AreaService areaService = new AreaService();
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());

                if (existingArea==null)
                {
                    Toast.Show("Area ", Toast.messageType.Information, Toast.messageAction.NotExists);
                    ClearForm();
                    txtAreaCode.Focus();
                    return;
                }
                else
                {
                    if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                    {
                        TerritoryService territoryService = new TerritoryService();
                        txtTerritoryCode.Text = territoryService.GetNewCode(this.Name, existingArea.AreaID, existingArea.AreaCode);
                        Common.EnableTextBox(false, txtTerritoryCode, txtAreaCode, txtAreaName);
                        txtTerritoryName.Focus();
                    }
                    else
                    {
                        Common.EnableTextBox(true, txtTerritoryCode);
                        txtTerritoryCode.Focus();
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
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

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
                ActiveControl = txtAreaCode;
                txtAreaCode.Focus();

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
                AreaService areaService = new AreaService();
                autoCompleteAreaCode.Clear();
                autoCompleteAreaCode.AddRange(areaService.GetAllAreaCodes());
                Common.SetAutoComplete(txtAreaCode, autoCompleteAreaCode, chkAutoCompleationArea.Checked);

                autoCompleteAreaName.Clear();
                autoCompleteAreaName.AddRange(areaService.GetAllAreaNames());
                Common.SetAutoComplete(txtAreaName, autoCompleteAreaName, chkAutoCompleationArea.Checked);

                TerritoryService territoryService = new TerritoryService();
                autoCompleteTerritoryCode.Clear();
                autoCompleteTerritoryCode.AddRange(territoryService.GetAllTerritoryCodes());
                Common.SetAutoComplete(txtTerritoryCode, autoCompleteTerritoryCode, chkAutoCompleationTerritory.Checked);

                autoCompleteTerritoryName.Clear();
                autoCompleteTerritoryName.AddRange(territoryService.GetAllTerritoryNames());
                Common.SetAutoComplete(txtTerritoryName, autoCompleteTerritoryName, chkAutoCompleationTerritory.Checked);


                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtAreaCode, txtAreaName, txtTerritoryCode, txtTerritoryName);
                Common.ClearTextBox(txtAreaCode, txtAreaName, txtTerritoryCode, txtTerritoryName);

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

                Area area = new Area();
                Territory territory = new Territory();
                AreaService areaService = new AreaService();
                TerritoryService territoryService = new TerritoryService();
                bool isNew = false;

                // Assign values
                area.AreaCode = txtAreaCode.Text.Trim();
                territory.TerritoryCode = txtTerritoryCode.Text.Trim();

                // Check availability of Area
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());
                if (existingArea == null)
                {
                    Toast.Show("Area " + txtAreaCode.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                    txtAreaCode.Focus();
                    return;
                }

                // Check availability of Territory
                existingTerritory = territoryService.GetTerritoryByCode(txtTerritoryCode.Text.Trim());
                if (existingTerritory == null || existingTerritory.TerritoryID == 0)
                {
                    existingTerritory = new Territory();
                    isNew = true;
                }

                existingTerritory.AreaID = existingArea.AreaID;
                existingTerritory.TerritoryCode = txtTerritoryCode.Text.Trim();
                existingTerritory.TerritoryName = txtTerritoryName.Text.Trim();
                existingTerritory.IsDelete = false;

                if (existingTerritory.TerritoryID.Equals(0))
                {
                    if ((Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    { 
                        return; 
                    }
                    // Create new Territory
                    territoryService.AddTerritory(existingTerritory);

                    if ((Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        if (chkAutoClear.Checked)
                        {
                            Common.ClearTextBox(txtTerritoryCode, txtTerritoryName);
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
                        if ((Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        { 
                            return; 
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }
                        if ((Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        { 
                            return; 
                        }
                    }
                    // Update Category deatils
                    territoryService.UpdateTerritory(existingTerritory);
                    if (chkAutoClear.Checked)
                    { 
                        ClearForm(); 
                    }
                    else
                    { 
                        InitializeForm(); 
                    }

                    // Display updated iformation 
                    Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }

                txtTerritoryCode.Focus();
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
                if (Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                {
                    return;
                }

                TerritoryService territoryService = new TerritoryService();
                existingTerritory = territoryService.GetTerritoryByCode(txtTerritoryCode.Text.Trim());

                if (existingTerritory != null && existingTerritory.TerritoryID != 0)
                {
                    territoryService.DeleteTerritory(existingTerritory);

                    Toast.Show("Territory - " + existingTerritory.TerritoryCode + " - " + existingTerritory.TerritoryName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    ClearForm();
                    txtTerritoryCode.Focus();
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


                txtTerritoryCode.Focus();
                txtTerritoryCode.SelectionStart = txtTerritoryCode.Text.Length;
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
                AreaService areaService = new AreaService();
                existingArea = areaService.GetAreasByCode(txtAreaCode.Text.Trim());

                if (existingArea != null)
                {
                    txtAreaName.Text = existingArea.AreaName.Trim();
                }
                else
                {

                }
                txtTerritoryCode.Focus();
                txtTerritoryCode.SelectionStart = txtTerritoryCode.Text.Length;
                
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
                txtTerritoryCode.Focus();
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

        private void txtAreaName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAreaName.Text.Trim()))
                {
                    txtTerritoryCode.Focus();
                }
                else
                {
                    AreaService areaService = new AreaService();
                    existingArea = areaService.GetAreasByName(txtAreaName.Text.Trim());

                    if (existingArea != null)
                    {
                        txtAreaCode.Text = existingArea.AreaCode.Trim();
                        txtAreaName.Text = existingArea.AreaName.Trim();
                    }
                    else 
                    {

                    }
                    txtTerritoryCode.Focus();
                    txtTerritoryCode.SelectionStart = txtTerritoryCode.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
        private void txtTerritoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    TerritoryService territoryService = new TerritoryService();
                    DataView dvAllReferenceData = new DataView(territoryService.GetAllActiveTerritoriesDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtTerritoryCode);
                        txtTerritoryCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                { 
                    return; 
                }

                txtTerritoryName.Focus();
                txtTerritoryName.SelectionStart = txtTerritoryName.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTerritoryCode.Text.Trim() != string.Empty)
                {
                    AreaService areaService=new AreaService();
                    TerritoryService territoryService = new TerritoryService();
                    existingTerritory = territoryService.GetTerritoryByCode(txtTerritoryCode.Text.Trim());

                    if (existingTerritory != null)
                    {
                        existingArea = areaService.GetAreasByID(existingTerritory.AreaID);
                        txtAreaCode.Text = existingArea.AreaCode.Trim();
                        txtAreaName.Text = existingArea.AreaName.Trim();
                        txtTerritoryCode.Text = existingTerritory.TerritoryCode.Trim();
                        txtTerritoryName.Text = existingTerritory.TerritoryName.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                        { 
                            Common.ClearTextBox(txtTerritoryName);
                        }

                        if (btnNew.Enabled)
                        {
                            if (Toast.Show("Territory - " + txtTerritoryCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                            { 
                                btnNew.PerformClick();
                                return;
                            }
                        }
                    }

                    if (btnSave.Enabled)
                    { 
                        Common.EnableTextBox(false, txtTerritoryCode); 
                    }

                    txtTerritoryName.Focus();
                    txtTerritoryName.SelectionStart = txtTerritoryName.Text.Length; 
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    TerritoryService territoryService = new TerritoryService();
                    DataView dvAllReferenceData = new DataView(territoryService.GetAllActiveTerritoriesDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtTerritoryCode);
                        txtTerritoryCode_Leave(this, e);
                    }
                }
                if (!e.KeyCode.Equals(Keys.Enter))
                { 
                    return; 
                }
                txtAreaCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtTerritoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!btnNew.Enabled)
                {
                    return;
                }
                if (txtTerritoryName.Text.Trim() != string.Empty)
                {
                    AreaService areaService = new AreaService();
                    TerritoryService territoryService = new TerritoryService();
                    existingTerritory = territoryService.GetTerritoryByName(txtTerritoryName.Text.Trim());

                    if (existingTerritory != null)
                    {
                        existingArea = areaService.GetAreasByID(existingTerritory.AreaID);
                        txtAreaCode.Text = existingArea.AreaCode.Trim();
                        txtAreaName.Text = existingArea.AreaName.Trim();
                        txtTerritoryCode.Text = existingTerritory.TerritoryCode.Trim();
                        txtTerritoryName.Text = existingTerritory.TerritoryName.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                    }
                    else
                    {
                        Toast.Show("Territory - " + txtTerritoryName.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.NotExists);
                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtTerritoryCode);
                    }

                    txtAreaCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        #endregion

        #region Methods....

        private void chkAutoCompleationArea_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtAreaCode, autoCompleteAreaCode, chkAutoCompleationArea.Checked);
                Common.SetAutoComplete(txtAreaName, autoCompleteAreaName, chkAutoCompleationArea.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationTerritory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SetAutoComplete(txtTerritoryCode, autoCompleteTerritoryCode, chkAutoCompleationTerritory.Checked);
                Common.SetAutoComplete(txtTerritoryName, autoCompleteTerritoryName, chkAutoCompleationTerritory.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtAreaCode, txtAreaName, txtTerritoryCode, txtTerritoryName);
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.DvResults = dvAllReferenceData;

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
