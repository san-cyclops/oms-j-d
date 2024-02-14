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
    public partial class FrmCostCentre : UI.Windows.FrmBaseMasterForm
    {
        private CostCentre existingCostCentre;
        UserPrivileges accessRights = new UserPrivileges();
        int documentID = 0;

        public FrmCostCentre()
        {
            InitializeComponent();
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
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name,
                                Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationCostCentre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CostCentreService costCenterService = new CostCentreService();
                Common.SetAutoComplete(txtCostCentreCode, costCenterService.GetAllCostCentreArray().ToArray(), chkAutoCompleationCostCentre.Checked);
                Common.SetAutoComplete(txtCostCentreName, costCenterService.GetAllCostCentreNameArray().ToArray(), chkAutoCompleationCostCentre.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmCostCentre_Load(object sender, EventArgs e)
        {
            try
            {
                chkAutoClear.Checked = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoClear;
                chkAutoCompleationCostCentre.Checked = true;
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
                CostCentreService costCentreService = new CostCentreService();

                Common.SetAutoComplete(txtCostCentreCode, costCentreService.GetAllCostCentreArray().ToArray(), chkAutoCompleationCostCentre.Checked);
                Common.SetAutoComplete(txtCostCentreName, costCentreService.GetAllCostCentreNameArray().ToArray(), chkAutoCompleationCostCentre.Checked);

                Common.EnableButton(true, btnNew);
                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtCostCentreCode);
                Common.ClearTextBox(txtCostCentreCode);

                ActiveControl = txtCostCentreCode;
                txtCostCentreCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private bool ValidateControls()
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtCostCentreCode, txtCostCentreName);

        }

        public override void Save()
        {
            try
            {
                if (ValidateControls() == false) return;
                CostCentreService costCentreService = new CostCentreService();
                bool isNew = false;
                existingCostCentre = costCentreService.GetCostCentresByCode(txtCostCentreCode.Text.Trim());

                if (existingCostCentre == null || existingCostCentre.CostCentreID == 0)
                {
                    existingCostCentre = new CostCentre();
                    isNew = true;
                }

                existingCostCentre.CostCentreCode = txtCostCentreCode.Text.Trim();
                existingCostCentre.CostCentreName = txtCostCentreName.Text.Trim();

                if (existingCostCentre.CostCentreID == 0)
                {
                    if ((Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    costCentreService.AddCostCentre(existingCostCentre);

                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    if ((Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    {
                        btnNew.PerformClick();
                    }
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (accessRights.IsPause == false) { Toast.Show("" + this.Text + " - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Information, Toast.messageAction.Permission).Equals(DialogResult.No); return; }

                        if ((Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    costCentreService.UpdateCostCentre (existingCostCentre);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtCostCentreCode.Focus();
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
                if (Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                    return;

                CostCentreService costCentreService = new CostCentreService();
                existingCostCentre = costCentreService.GetCostCentresByCode(txtCostCentreCode.Text.Trim());

                if (existingCostCentre != null && existingCostCentre.CostCentreID != 0)
                {
                    existingCostCentre.IsDelete = true;
                    costCentreService.UpdateCostCentre(existingCostCentre);
                    ClearForm();
                    Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtCostCentreCode.Focus();
                }
                else
                    Toast.Show("Cost Centre - " + existingCostCentre.CostCentreCode + " - " + existingCostCentre.CostCentreName + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void txtCostCentreCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtCostCentreName.Focus();
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    CostCentreService costCentreService = new CostCentreService();
                    DataView dvAllReferenceData = new DataView(costCentreService.GetAllCostCentreDataTable());
                    if (dvAllReferenceData.Count != 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Text.Trim(), this.ActiveControl.Text.Trim(), txtCostCentreCode);
                        txtCostCentreCode_Leave(this, e);
                    }
                }

                if (txtCostCentreCode.Text.Trim() != string.Empty)
                    txtCostCentreName.Focus();
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

        private void txtCostCentreName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!txtCostCentreCode.Enabled) return;

                    CostCentreService CostCentreService=new Service.CostCentreService();
                    txtCostCentreCode.Text=CostCentreService.GetCostCentresByName(txtCostCentreName.Text.Trim()).CostCentreCode;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            } 
        }

        private void txtCostCentreCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCostCentreCode.Text.Trim() != string.Empty)
                {
                    CostCentreService costCentreService = new CostCentreService();


                    existingCostCentre = costCentreService.GetCostCentresByCode (txtCostCentreCode.Text.Trim());


                    if (existingCostCentre != null)
                    {

                        txtCostCentreCode.Text = existingCostCentre.CostCentreCode.Trim();
                        txtCostCentreName.Text = existingCostCentre.CostCentreName.Trim();
                        if (accessRights.IsSave == true) Common.EnableButton(true, btnSave);
                        if (accessRights.IsModify == true) Common.EnableButton(true, btnDelete);
                        Common.EnableButton(false, btnNew);
                        txtCostCentreName.Focus();

                    }
                    else
                    {
                        if (chkAutoClear.Checked)
                            Common.ClearTextBox(txtCostCentreName);
                        if (btnNew.Enabled)
                            if (Toast.Show("Cost Centre - " + txtCostCentreCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.NotExistsSave).Equals(DialogResult.Yes))
                                btnNew.PerformClick();


                    }

                    if (btnSave.Enabled)
                    {
                        Common.EnableTextBox(false, txtCostCentreCode);

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
                if (chkAutoClear.Checked)
                    Common.ClearTextBox(txtCostCentreName);
                if (AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name).AutoGenerete == true)
                {
                    CostCentreService costCentreService = new CostCentreService();
                    txtCostCentreCode.Text = costCentreService.GetNewCode(this.Name);
                    Common.EnableTextBox(false, txtCostCentreCode);
                    txtCostCentreName.Focus();
                }
                else
                    txtCostCentreCode.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        
    }
}
