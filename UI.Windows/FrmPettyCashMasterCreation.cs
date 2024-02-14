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
    public partial class FrmPettyCashMasterCreation : UI.Windows.FrmBaseMasterForm
    {
        ErrorMessage errorMessage = new ErrorMessage();

        private AccPettyCashMaster existingAccPettyCashMaster;
        private AccLedgerAccount existingPettyCashAccLedgerAccount;

        public FrmPettyCashMasterCreation()
        {
            InitializeComponent();
        }
        
        #region Form Events
        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    txtLedgerCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbLocation.Text.Trim()))
                { return; }

                if (!IsLocationExistsByName(cmbLocation.Text.Trim()))
                {
                    cmbLocation.Focus();
                    return;
                }
                else
                {
                    txtLedgerCode.Focus();
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();
                    DataView dvAllReferenceData = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvAllReferenceData.Count > 0)
                    { 
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                        txtLedgerCode_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtAmount);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F3))
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    LocationService locationService = new LocationService();
                    DataView dvAllReferenceData = new DataView(accLedgerAccountService.GetAllActivePettyCashAccountsDataTable(locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID));
                    if (dvAllReferenceData.Count > 0)
                    {
                        LoadReferenceSearchForm(dvAllReferenceData, this.Name.Trim(), this.Name.Trim(), this.ActiveControl.Text.Trim(), this.ActiveControl);
                        txtLedgerName_Leave(this, e);
                    }
                }

                Common.SetFocus(e, txtLedgerCode);
                txtLedgerCode.SelectionStart = txtLedgerCode.Text.Length;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLedgerCode.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                    LocationService locationService = new LocationService();
                    existingPettyCashAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtLedgerCode.Text.Trim());
                    existingAccPettyCashMaster = accPettyCashMasterService.GetAccPettyCashMasterByLedgerID(existingPettyCashAccLedgerAccount.AccLedgerAccountID, locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);

                    if (existingAccPettyCashMaster != null)
                    {
                        txtLedgerCode.Text = existingPettyCashAccLedgerAccount.LedgerCode.Trim();
                        txtLedgerName.Text = existingPettyCashAccLedgerAccount.LedgerName.Trim();
                        txtAmount.Text = Common.ConvertDecimalToStringCurrency(existingAccPettyCashMaster.Amount);

                        Common.EnableButton(true, btnSave, btnDelete);
                    }
                    else
                    {
                        existingPettyCashAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtLedgerCode.Text.Trim());

                        if (existingPettyCashAccLedgerAccount != null)
                        {
                            txtLedgerCode.Text = existingPettyCashAccLedgerAccount.LedgerCode.Trim();
                            txtLedgerName.Text = existingPettyCashAccLedgerAccount.LedgerName.Trim();

                            Common.EnableButton(true, btnSave);
                            Common.ClearTextBox(txtAmount);
                        }
                        else
                        {
                            Toast.Show(Common.ConvertStringToDisplayFormat(lblLedger.Text.Trim()) + " - " + txtLedgerName.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                            txtLedgerCode.Text = string.Empty;
                            txtLedgerName.Text = string.Empty;
                            txtLedgerCode.Focus();
                            return;
                        }
                    }

                    txtAmount.Focus();
                    txtAmount.SelectionStart = txtAmount.Text.Length;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLedgerName.Text.Trim() != string.Empty)
                {
                    AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                    existingPettyCashAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByName(txtLedgerName.Text.Trim());

                    if (existingPettyCashAccLedgerAccount != null)
                    {
                        txtLedgerCode.Text = existingPettyCashAccLedgerAccount.LedgerCode.Trim();
                        txtLedgerName.Text = existingPettyCashAccLedgerAccount.LedgerName.Trim();
                        txtLedgerCode.Focus();
                        txtLedgerCode.SelectionStart = txtLedgerCode.Text.Length;
                    }
                    else
                    {
                        Toast.Show(Common.ConvertStringToDisplayFormat(lblLedger.Text.Trim()) + " - " + txtLedgerName.Text.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                        txtLedgerCode.Text = string.Empty;
                        txtLedgerName.Text = string.Empty;
                        txtLedgerName.Focus();
                        return;
                    }
                }
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
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();

                // Load Locations            
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
                cmbLocation.SelectedValue = Common.LoggedLocationID;

                Common.SetAutoComplete(txtLedgerCode, accLedgerAccountService.GetPettyCashLedgerCodes(), chkAutoCompleationLedger.Checked);
                Common.SetAutoComplete(txtLedgerName, accLedgerAccountService.GetPettyCashLedgerNames(), chkAutoCompleationLedger.Checked);

                Common.EnableButton(false, btnSave, btnDelete);
                Common.EnableTextBox(true, txtLedgerCode);
                Common.ClearTextBox(txtLedgerCode, txtLedgerName);

                existingAccPettyCashMaster = null;
                existingPettyCashAccLedgerAccount = null;

                ActiveControl = txtLedgerCode;
                txtLedgerCode.Focus();
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
                this.Text = autoGenerateInfo.FormText;
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

                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                LocationService locationService = new LocationService();
                bool isNew = false;
                existingPettyCashAccLedgerAccount = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtLedgerCode.Text.Trim());
                existingAccPettyCashMaster = accPettyCashMasterService.GetAccPettyCashMasterByLedgerID(existingPettyCashAccLedgerAccount.AccLedgerAccountID, locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);

                if (existingAccPettyCashMaster == null || existingAccPettyCashMaster.AccPettyCashMasterID == 0)
                {
                    existingAccPettyCashMaster = new AccPettyCashMaster();
                    isNew = true;
                }

                existingAccPettyCashMaster.LedgerID = accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtLedgerCode.Text.Trim()).AccLedgerAccountID;
                existingAccPettyCashMaster.LocationID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID;
                existingAccPettyCashMaster.Amount = Common.ConvertStringToDecimalCurrency(txtAmount.Text.Trim());
                existingAccPettyCashMaster.GroupOfCompanyID = Common.GroupOfCompanyID;
                existingAccPettyCashMaster.CompanyID = locationService.GetLocationsByName(cmbLocation.Text.Trim()).CompanyID;

                //existingAccPettyCashMaster.AccLedgerAccount = existingPettyCashAccLedgerAccount;

                if (!IsValidateLocationExistsRecords(existingAccPettyCashMaster)) { return; }
                if (!IsValidateExistsRecords(existingAccPettyCashMaster)) { return; }

                if (existingAccPettyCashMaster.AccPettyCashMasterID == 0)
                {
                    if ((Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Save).Equals(DialogResult.No)))
                    {
                        return;
                    }
                    accPettyCashMasterService.AddAccPettyCashMaster(existingAccPettyCashMaster);

                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    //if ((Toast.Show(this.Text + " - " + existingAccPettyCashMaster.AccLedgerAccount.LedgerCode + "", Toast.messageType.Information, Toast.messageAction.Save).Equals(DialogResult.Yes)))
                    //{
                    //    btnNew.PerformClick();
                    //}
                }
                else
                {
                    if (isNew)
                    {
                        if ((Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.ExistRecord).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if ((Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Modify).Equals(DialogResult.No)))
                        {
                            return;
                        }
                    }
                    accPettyCashMasterService.UpdateAccPettyCashMaster(existingAccPettyCashMaster);
                    if (chkAutoClear.Checked)
                    {
                        ClearForm();
                    }
                    else
                    {
                        InitializeForm();
                    }
                    Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Modify);
                }
                txtLedgerCode.Focus();
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
                if (Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.Delete).Equals(DialogResult.No))
                { return; }

                AccPettyCashMasterService accPettyCashMasterService = new AccPettyCashMasterService();
                AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                LocationService locationService = new LocationService();

                existingAccPettyCashMaster = accPettyCashMasterService.GetAccPettyCashMasterByLedgerID(accLedgerAccountService.GetAccLedgerPettyCashAccountByCode(txtLedgerCode.Text.Trim()).AccLedgerAccountID, locationService.GetLocationsByName(cmbLocation.Text.Trim()).LocationID);

                if (!IsValidateExistsRecords(existingAccPettyCashMaster)) { return; }

                if (existingAccPettyCashMaster != null && existingAccPettyCashMaster.AccPettyCashMasterID != 0)
                {
                    existingAccPettyCashMaster.IsDelete = true;
                    accPettyCashMasterService.UpdateAccPettyCashMaster(existingAccPettyCashMaster);
                    ClearForm();
                    Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.Delete);
                    txtLedgerCode.Focus();
                }
                else
                {
                    Toast.Show(this.Text + " - " + txtLedgerCode.Text.Trim() + "", Toast.messageType.Warning, Toast.messageAction.NotExists);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        // Confirm Location by Name
        private bool IsLocationExistsByName(string locationName)
        {
            bool recodFound = false;
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByName(locationName);
            if (location != null)
            {
                recodFound = true;
                cmbLocation.Text = location.LocationName;
            }
            else
            {
                recodFound = false;
                Common.ClearComboBox(cmbLocation);
                Toast.Show(lblLocation.Text.Trim() + " ", Toast.messageType.Information, Toast.messageAction.NotExists);
            }
            return recodFound;
        }

        /// <summary>
        /// Load Reference Search form on demand
        /// </summary>
        private void LoadReferenceSearchForm(DataView dvAllReferenceData, string parentOfSearch, string searchFormCaption, string searchText, Control focusControl)
        {
            try
            {
                FrmReferenceSearch referenceSearch = new FrmReferenceSearch();
                referenceSearch.DvResults = dvAllReferenceData;
                referenceSearch.ParentOfSearch = parentOfSearch.Trim();
                referenceSearch.FormCaption = searchFormCaption.Trim();
                referenceSearch.SearchText = searchText.Trim();
                referenceSearch.FocusControl = focusControl;

                if (referenceSearch.IsDisposed)
                { referenceSearch = new FrmReferenceSearch(); }

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmReferenceSearch)
                    {
                        FrmReferenceSearch masterSearch2 = (FrmReferenceSearch)frm;
                        if (string.Equals(masterSearch2.ParentOfSearch.Trim(), this.Name.Trim()))
                        { return; }
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
            return Validater.ValidateControls(errorProvider, Validater.ValidateType.Empty, txtLedgerCode, txtAmount, cmbLocation);
        }

        #region Validate Logics

        private bool IsValidateExistsRecords(AccPettyCashMaster accPettyCashMaster)
        {
            AccPettyCashMasterValidator accPettyCashMasterValidator = new AccPettyCashMasterValidator();

            bool isValidated = false;

            if (!accPettyCashMasterValidator.ValidateExistsLedgerTransactions(accPettyCashMaster))
            {
                Toast.Show("Transactions ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblLedger.Text.Trim()) + " - " + txtLedgerCode.Text.Trim() + " - " + Common.ConvertStringToDisplayFormat(txtLedgerName.Text.Trim()));
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }

        private bool IsValidateLocationExistsRecords(AccPettyCashMaster accPettyCashMaster)
        {
            AccPettyCashMasterValidator accPettyCashMasterValidator = new AccPettyCashMasterValidator();

            bool isValidated = false;

            if (!accPettyCashMasterValidator.ValidateExistsPettyCashLedgerLocation(accPettyCashMaster))
            {
                Toast.Show(Common.ConvertStringToDisplayFormat(lblLocation.Text.Trim()) + " - record ", Toast.messageType.Information, Toast.messageAction.ExistsForSelected, "selected - " + Common.ConvertStringToDisplayFormat(lblLedger.Text.Trim()) + " - " + txtLedgerCode.Text.Trim());
                isValidated = false;
            }
            else
            { isValidated = true; }

            return isValidated;
        }
        #endregion
        #endregion
    }
}
