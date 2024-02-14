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
    public partial class FrmCrmCardUsage : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmCardUsage(string formText)
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

            base.FormLoad();
        }

        public override void InitializeForm()
        {
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());
            base.InitializeForm();
        }

        public override void ClearForm()
        {
            try
            {
                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

                errorProvider.Clear();

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

                if (chkAllTypes.Checked)
                {
                    ViewReportAllType(dateFrom, dateTo);
                }
                else
                {
                    if (ValidateTypeComboBoxes() == false) { return; }
                    ViewReportSelectedType(dateFrom, dateTo, Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateTypeComboBoxes() 
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCardType);
        }


        private void ViewReportAllType(DateTime fromDate, DateTime toDate)   
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardUsageDetails crmRptCardUsageDetails = new CrmRptCardUsageDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetCardUsageAllType(fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCardUsageDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardUsage"]);

            crmRptCardUsageDetails.SummaryInfo.ReportTitle = "Card Usage Details";
            crmRptCardUsageDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCardUsageDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardUsageDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardUsageDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportSelectedType(DateTime fromDate, DateTime toDate, int cardType)   
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardUsageDetails crmRptCardUsageDetails = new CrmRptCardUsageDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetCardUsageSelectedType(fromDate, toDate, cardType);
            this.Cursor = Cursors.Default;

            crmRptCardUsageDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardUsage"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptCardUsageDetails.SummaryInfo.ReportTitle = "Card Usage Details (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptCardUsageDetails.SummaryInfo.ReportTitle = "Card Usage Details (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptCardUsageDetails.SummaryInfo.ReportTitle = "Card Usage Details (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptCardUsageDetails.SummaryInfo.ReportTitle = "Card Usage Details (Arrowana Member)";
            }

            crmRptCardUsageDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCardUsageDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardUsageDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardUsageDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardUsageDetails;
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

    }
}
