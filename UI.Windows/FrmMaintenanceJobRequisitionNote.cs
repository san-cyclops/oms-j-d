using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Transactions;

using Domain;
using Utility;
using Service;
using Report.Logistic;


namespace UI.Windows
{
    /// <summary>
    ///  Developed by asanka
    /// </summary>
    /// 
    public partial class FrmMaintenanceJobRequisitionNote : UI.Windows.FrmBaseTransactionForm
    {
        int documentID;
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();

        public FrmMaintenanceJobRequisitionNote()
        {
            InitializeComponent();
        }

        #region Form Events

        public override void FormLoad()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);                                
                documentID = autoGenerateInfo.DocumentID;
                this.Text = autoGenerateInfo.FormText.Trim();


                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }
                

                chkAutoCompleationJbrNo.Checked = true;
                InitializeForm();
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtReferenceNo); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtDocumentNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
                { return; }

                LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
                if (lgsMaintenanceJobRequisitionService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()) == null)
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim(), Toast.messageType.Information, Toast.messageAction.NotExists);
                }
                else
                {
                    LoadPausedDocument(lgsMaintenanceJobRequisitionService.GetPausedDocumentDetailsByDocumentNumber(txtDocumentNo.Text.Trim()));
                }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }        
        }

        private void txtReferenceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpExpectedDate);}
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpExpectedDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, dtpDocumentDate); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpExpectedDate_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDocumentDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, cmbLocation); }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void dtpDocumentDate_Validated(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { Common.SetFocus(e, txtJobDescription); }
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
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void chkAutoCompleationJbrNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCompleationJbrNo.Checked)
                { LoadPausedDocuments(); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Setup GUI contols (Set visibility, data binding, auto loading)
        /// </summary>
        public override void InitializeForm()
        {
            // Load Locations            
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            cmbLocation.SelectedValue = Common.LoggedLocationID;
            
            txtDocumentNo.Text = GetDocumentNo(true);

            this.ActiveControl = txtReferenceNo;
            txtReferenceNo.Focus();
        }

        /// <summary>
        /// Get a Document number
        /// </summary>
        /// <param name="isTemporytNo"></param>
        /// <returns></returns>
        private string GetDocumentNo(bool isTemporytNo)
        {
            LgsMaterialRequestService lgsMaterialRequestService = new LgsMaterialRequestService();
            LocationService locationService = new LocationService();
            return lgsMaterialRequestService.GetDocumentNo(this.Name, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString())).LocationCode, documentID, isTemporytNo).Trim();
        }


        /// <summary>
        /// Pause Maintenance Job Requesition Note.
        /// </summary>
        public override void Pause()
        {
            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.PauseTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                if (SaveDocument(0, txtDocumentNo.Text.Trim()).Equals(true))
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.PauseTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 0);
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.PauseTransaction);
                    return;
                }
            }

        }


        /// <summary>
        /// Save New Maintenance Job Requesition Note.
        /// </summary>
        public override void Save()
        {
            if (!ValidateControls())
            { return; }

            if ((Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                if (!IsLocationExistsByName(cmbLocation.Text.Trim())) { return; }
                bool saveDocument = SaveDocument(1, txtDocumentNo.Text.Trim());
                if (saveDocument.Equals(true))
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Information, Toast.messageAction.SaveTransaction);
                    GenerateReport(txtDocumentNo.Text.Trim(), 1);
                    ClearForm();
                }
                else
                {
                    Toast.Show(this.Text + " - " + txtDocumentNo.Text.Trim() + "", Toast.messageType.Error, Toast.messageAction.SaveTransaction);
                    return;
                }
            }
        }

        /// <summary>
        ///  Save data into DB
        /// </summary>
        /// <param name="documentStatus"></param>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        private bool SaveDocument(int documentStatus, string documentNo)
        {

            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader();
            Location Location = new Location();
            LocationService locationService = new LocationService();

            Location = locationService.GetLocationsByID(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
            lgsMaintenanceJobRequisitionHeader = lgsMaintenanceJobRequisitionService.GetPausedLgsMaintenanceJobRequisitionHeaderByDocumentNo(documentID, txtDocumentNo.Text.Trim(), Convert.ToInt32(cmbLocation.SelectedValue));
            if (lgsMaintenanceJobRequisitionHeader == null)
            { lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader(); }
            
            if (documentStatus.Equals(1))
            {
                documentNo = GetDocumentNo(false);
                txtDocumentNo.Text = documentNo;
            }

            lgsMaintenanceJobRequisitionHeader.DocumentStatus = documentStatus;
            lgsMaintenanceJobRequisitionHeader.GroupOfCompanyID = Common.GroupOfCompanyID;
            lgsMaintenanceJobRequisitionHeader.CompanyID = Location.CompanyID;
            lgsMaintenanceJobRequisitionHeader.LocationID = Location.LocationID;
            lgsMaintenanceJobRequisitionHeader.DocumentID = documentID;
            lgsMaintenanceJobRequisitionHeader.DocumentNo = documentNo.Trim();
            lgsMaintenanceJobRequisitionHeader.DocumentDate = dtpDocumentDate.Value;
            lgsMaintenanceJobRequisitionHeader.ExpectedDate = dtpExpectedDate.Value;
            //lgsMaterialRequestHeader.CostCentreID = int.Parse(cmbCostCentre.SelectedValue.ToString());
            lgsMaintenanceJobRequisitionHeader.ExpiryDate = Common.FormatDate(Common.FormatDate(dtpDocumentDate.Value).AddDays(Convert.ToDouble(txtValidityPeriod.Text.Trim())));

            lgsMaintenanceJobRequisitionHeader.RequestedBy = Common.LoggedUser;
            lgsMaintenanceJobRequisitionHeader.ReferenceDocumentNo = txtReferenceNo.Text.Trim();
            lgsMaintenanceJobRequisitionHeader.JobDescription = txtJobDescription.Text.Trim();
            lgsMaintenanceJobRequisitionHeader.CreatedUser = Common.LoggedUser;
            lgsMaintenanceJobRequisitionHeader.CreatedDate = DateTime.UtcNow;
            lgsMaintenanceJobRequisitionHeader.ModifiedUser = Common.LoggedUser;
            lgsMaintenanceJobRequisitionHeader.ModifiedDate = DateTime.UtcNow;
            lgsMaintenanceJobRequisitionHeader.DataTransfer = 0;

            return lgsMaintenanceJobRequisitionService.SaveMaintenanceJobRequisitionNote(lgsMaintenanceJobRequisitionHeader);

        }

        /// <summary>
        /// Recal Paused document
        /// </summary>
        /// <param name="existingLgsMaintenanceJobRequisitionHeader"></param>
        private void LoadPausedDocument(LgsMaintenanceJobRequisitionHeader existingLgsMaintenanceJobRequisitionHeader)
        {
            txtDocumentNo.Text = existingLgsMaintenanceJobRequisitionHeader.DocumentNo;
            cmbLocation.SelectedValue = existingLgsMaintenanceJobRequisitionHeader.LocationID;
            dtpDocumentDate.Value = existingLgsMaintenanceJobRequisitionHeader.DocumentDate;
            dtpExpectedDate.Value = existingLgsMaintenanceJobRequisitionHeader.ExpectedDate;  
            txtReferenceNo.Text = existingLgsMaintenanceJobRequisitionHeader.ReferenceDocumentNo;
            txtJobDescription.Text = existingLgsMaintenanceJobRequisitionHeader.JobDescription.Trim();
        }

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtDocumentNo, txtJobDescription))
            { return false; }
            else if (!Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation))
            { return false; }
            else
            { return true; }
        }

        private void LoadPausedDocuments()
        {
            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            Common.SetAutoComplete(txtDocumentNo, lgsMaintenanceJobRequisitionService.GetPausedDocumentNumbers(), chkAutoCompleationJbrNo.Checked);
        }


        public override void ClearForm()
        {
            base.ClearForm();
            ResetDates();
            ResetAutoCompleteControlData();
        }

        private void ResetDates()
        {
            dtpDocumentDate.Value = DateTime.Now;            
        }


        /// <summary>
        /// Reload auto complete data lists
        /// </summary>
        private void ResetAutoCompleteControlData()
        {
            if (chkAutoCompleationJbrNo.Checked)
            {
                LoadPausedDocuments();
            }

        }


        /// <summary>
        /// Generate Transaction report
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        private void GenerateReport(string documentNo, int documentStatus)
        { 
            LgsReportGenerator lgsReportGenerator = new LgsReportGenerator();
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            lgsReportGenerator.GenearateTransactionReport(autoGenerateInfo, documentNo.Trim(), documentStatus);
        }

        #region Confirm Existing Property Values

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

        #endregion

        

        #endregion
    }
}
