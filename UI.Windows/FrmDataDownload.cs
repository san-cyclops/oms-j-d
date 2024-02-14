using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Domain;
using Utility;
using Service;
using Report.Inventory;

namespace UI.Windows
{
    public partial class FrmDataDownload : UI.Windows.FrmBaseTransactionForm
    {
        UserPrivileges accessRights = new UserPrivileges();
        LocationService accessLocation = new LocationService();
        int documentID = 0;

        public FrmDataDownload()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                //Load Stock Adjustment Mode
                LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
                Common.SetAutoBindRecords(cmbTransactionType, lookUpReferenceService.GetLookUpReferenceValues(((int)LookUpReference.DataDownloadMode).ToString()));

                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);
                if (accessRights == null) { Toast.Show("Access Denied", Toast.messageType.Information, Toast.messageAction.General); return; }

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
                this.ActiveControl = cmbTransactionType;
                cmbTransactionType.Focus();
                cmbLocation.SelectedValue = Common.LoggedLocationID;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            base.ClearForm();
            Common.ClearComboBox(cmbTransactionType, cmbLocation);
            cmbTransactionType.Focus();
        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbTransactionType, cmbLocation);
        }

        public override void Save()
        {
            DataDownloadService dataDownloadService = new DataDownloadService();

            if (ValidateComboBoxes().Equals(false)) { return; }

            if ((Toast.Show("Data Download Process ", Toast.messageType.Question, Toast.messageAction.SaveTransaction).Equals(DialogResult.Yes)))
            {
                this.Cursor = Cursors.WaitCursor;
                if (dataDownloadService.Save((cmbTransactionType.SelectedIndex + 1), (cmbLocation.SelectedIndex + 1)) == true)
 
                {
                    Toast.Show("Data Download Process ", Toast.messageType.Information, Toast.messageAction.General);
                    ClearForm();
                }
                else
                {
                    Toast.Show("Data Download Process NOT Compleated", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }
                this.Cursor = Cursors.Default;
            }
        }        
    }
}
