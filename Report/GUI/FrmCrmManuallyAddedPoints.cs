using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Report;
using Report.Com;
using Report.GV;
using Report.Inventory.Transactions.Reports;
using Report.CRM;
using Service;
using Domain;
using UI.Windows;
using UI.Windows.Reports;
using Utility;
using System.Collections;
using Report.Inventory;
using System.Reflection;
using Report.GUI;
using Report.CRM.Reports;
using Service;

namespace Report.GUI
{
    public partial class FrmCrmManuallyAddedPoints : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmManuallyAddedPoints(string formText)
        {
            InitializeComponent();
            formDisplayText = formText;
        }

        public override void FormLoad()
        {
            //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            this.Text = formDisplayText;

            //documentID = autoGenerateInfo.DocumentID;
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

            dtpFromDate.Value = Common.GetSystemDate();
            dtpToDate.Value = Common.GetSystemDate();

            cmbLocation.SelectedValue = Common.LoggedLocationID;

            base.FormLoad();
        }

        public override void InitializeForm()
        {
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            LocationService locationService = new LocationService();

            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());

            cmbLocation.SelectedValue = Common.LoggedLocationID;

            base.InitializeForm();
        }

        public override void ClearForm()
        {
            try
            {
                Common.ClearComboBox(cmbLocation);
                cmbLocation.Focus();

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

                base.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateFrom;
                DateTime dateTo;

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

                if (chkAllLocations.Checked && chkAllTypes.Checked)
                {
                    ViewReportAllCustomersAllType(dateFrom, dateTo);
                }
                else if (chkAllLocations.Checked && !chkAllTypes.Checked)
                {
                    if (ValidateTypeComboBoxes().Equals(false)) { return; }
                    ViewReportAllLocationSelectedType(dateFrom, dateTo, Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()));
                }
                else if (!chkAllLocations.Checked && chkAllTypes.Checked)
                {
                    if (ValidateLocationComboBoxes().Equals(false)) { return; }
                    ViewSelectedLocationAllType(dateFrom, dateTo, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()));
                }
                else
                {
                    if (ValidateLocationComboBoxes().Equals(false)) { return; }
                    if (ValidateTypeComboBoxes().Equals(false)) { return; }
                    ViewSelectedLocationSelectedType(dateFrom, dateTo, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateLocationComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation);
        }

        private bool ValidateTypeComboBoxes() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCardType);
        }


        private void ViewReportAllCustomersAllType(DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptManuallyAddedPointsDetailsAllLocations crmRptManuallyAddedPointsDetailsAllLocations = new CrmRptManuallyAddedPointsDetailsAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetManuallyAddedPointsAllCustomersAllType(fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptManuallyAddedPointsDetailsAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["ManuallyAddedPoints"]);

            crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportTitle = "Manually Added Points Details";
            crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptManuallyAddedPointsDetailsAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportAllLocationSelectedType(DateTime fromDate, DateTime toDate, int cardType)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptManuallyAddedPointsDetailsAllLocations crmRptManuallyAddedPointsDetailsAllLocations = new CrmRptManuallyAddedPointsDetailsAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetManuallyAddedPointsAllLocationsSelectedType(fromDate, toDate, cardType);
            this.Cursor = Cursors.Default;

            crmRptManuallyAddedPointsDetailsAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["ManuallyAddedPoints"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportTitle = "Manually Added Points Details (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportTitle = "Manually Added Points Details (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportTitle = "Manually Added Points Details (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportTitle = "Manually Added Points Details (Arrowana Member)";
            }

            crmRptManuallyAddedPointsDetailsAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptManuallyAddedPointsDetailsAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptManuallyAddedPointsDetailsAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }


        private void ViewSelectedLocationAllType(DateTime fromDate, DateTime toDate, int locationID) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptManuallyAddedPointsDetails crmRptManuallyAddedPointsDetails = new CrmRptManuallyAddedPointsDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetManuallyAddedPointsAllTypesSelectedlocations(fromDate, toDate, locationID);
            this.Cursor = Cursors.Default;

            crmRptManuallyAddedPointsDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["ManuallyAddedPoints"]);

            crmRptManuallyAddedPointsDetails.SummaryInfo.ReportTitle = "Manually Added Points Details";
            crmRptManuallyAddedPointsDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptManuallyAddedPointsDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewSelectedLocationSelectedType(DateTime fromDate, DateTime toDate, int locationID, int cardType)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptManuallyAddedPointsDetails crmRptManuallyAddedPointsDetails = new CrmRptManuallyAddedPointsDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetManuallyAddedPointsSelectedTypesSelectedlocations(fromDate, toDate, locationID, cardType);
            this.Cursor = Cursors.Default;

            crmRptManuallyAddedPointsDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["ManuallyAddedPoints"]);

            crmRptManuallyAddedPointsDetails.SummaryInfo.ReportTitle = "Manually Added Points Details (All Types)";

            crmRptManuallyAddedPointsDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptManuallyAddedPointsDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptManuallyAddedPointsDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }


        private void chkAllTypes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllTypes.Checked)
                {
                    cmbCardType.SelectedIndex = -1;
                    cmbCardType.Enabled = false;
                }
                else
                {
                    cmbCardType.Enabled = true;
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService(); 
                    Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllLocations.Checked)
                {
                    cmbLocation.SelectedIndex = -1;
                    cmbLocation.Enabled = false;
                }
                else
                {
                    cmbLocation.SelectedValue = Common.LoggedLocationID;
                    cmbLocation.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
