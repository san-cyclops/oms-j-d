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
    public partial class FrmCrmCardIssuedDetails : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmCardIssuedDetails(string formText)
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

                if (chkAllLocations.Checked)
                {
                    if (rdoArapaima.Checked)
                    {
                        ViewReportCardIssueAllLocationArapaima(dateFrom, dateTo);
                    }
                    else
                    {
                        ValidateTypeComboBoxes();
                        ViewReportCardIssueAllLocation(Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                    }
                }
                else
                {
                    if (rdoArapaima.Checked)
                    {
                        ViewReportCardIssueSelectedLocationArapaima(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), dateFrom, dateTo);
                    }
                    else
                    {
                        ValidateTypeComboBoxes();
                        ViewReportCardIssueSelectedLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                    }
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


        private void ViewReportCardIssueSelectedLocation(int locationID, int cardType, DateTime fromDate, DateTime toDate)   
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardIssuedDetails crmRptCardIssuedDetails = new CrmRptCardIssuedDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.ViewReportCardIssueSelectedLocation(locationID, cardType, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCardIssuedDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardIssuedDetails"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptCardIssuedDetails.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptCardIssuedDetails.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptCardIssuedDetails.SummaryInfo.ReportTitle = "Card Issued Details (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptCardIssuedDetails.SummaryInfo.ReportTitle = "Card Issued Details (Arrowana Member)";
            }
            
            crmRptCardIssuedDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text + "'";

            crmRptCardIssuedDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardIssuedDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportCardIssueSelectedLocationArapaima(int locationID, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardIssuedDetails crmRptCardIssuedDetails = new CrmRptCardIssuedDetails();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.ViewReportCardIssueSelectedLocationArapaima(locationID, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCardIssuedDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardIssuedDetails"]);

            crmRptCardIssuedDetails.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima)";

            crmRptCardIssuedDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text + "'";

            crmRptCardIssuedDetails.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardIssuedDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardIssuedDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportCardIssueAllLocation(int cardType, DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardIssuedDetailsAllLocations crmRptCardIssuedDetailsAllLocations = new CrmRptCardIssuedDetailsAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.ViewReportCardIssueAllLocation(cardType, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCardIssuedDetailsAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardIssuedDetails"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportTitle = "Card Issued Details (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportTitle = "Card Issued Details (Arrowana Member)";
            }

            crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardIssuedDetailsAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportCardIssueAllLocationArapaima(DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCardIssuedDetailsAllLocations crmRptCardIssuedDetailsAllLocations = new CrmRptCardIssuedDetailsAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.ViewReportCardIssueAllLocationArapaima(fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCardIssuedDetailsAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["CardIssuedDetails"]);

            crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportTitle = "Card Issued Details (Arapaima)";

            crmRptCardIssuedDetailsAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCardIssuedDetailsAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCardIssuedDetailsAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
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

        private void rdoArapaima_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoArapaima.Checked)
                {
                    cmbCardType.SelectedIndex = -1;
                    cmbCardType.Enabled = false;
                }
                else
                {
                    cmbCardType.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
