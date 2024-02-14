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
using Report.Logistic;
using Service;
using Domain;
using UI.Windows;
using UI.Windows.Reports;
using Utility;
using System.Collections;
using Report.Inventory;
using System.Reflection;

namespace UI.Windows
{
    public partial class FrmPosSalesSummery : UI.Windows.FrmBaseMasterForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        

        public FrmPosSalesSummery()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {

                // Load Locations
                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
               
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptPosSalesSummery");
               
                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

                base.FormLoad();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void ChkAllTerminals_CheckedChanged(object sender, EventArgs e)
        {
            if(ChkAllTerminals.Checked == true)
            {
                Common.ClearComboBox(cmbTerminal);
                cmbTerminal.Enabled = false;
            }
            else
            {
                cmbTerminal.Enabled = true;
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

        public override void ClearForm()
        {
            base.ClearForm();

            Common.ClearComboBox(cmbTerminal, cmbLocation);
            cmbLocation.Focus();

            ChkAllTerminals.Checked = false;
            ChkAllLocations.Checked = false;

            dtpFromDate.Value = Common.GetSystemDate();
            dtpToDate.Value = Common.GetSystemDate();

        }

        private bool ValidateComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbTerminal, cmbLocation);
        }

        public override void View()
        {

            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            int locationId = 0;
            int terminalId = 0;

            DateTime dateFrom = dtpFromDate.Value;
            DateTime dateTo = dtpToDate.Value;

            if (dateFrom > dateTo)
            {
                Toast.Show("",Toast.messageType.Information, Toast.messageAction.InvalidDateRange);
                return;
            }


            if (ChkLocationWise.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                if (posSalesSummeryService.View(locationId, terminalId, dateFrom, dateTo, true) == true)
                {
                    LocationWiseViewReport();

                }
                else
                {
                    Toast.Show("Error in data Processing.", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }
            }
            else
            {
                if (ChkAllLocations.Checked == true) { locationId = 0; } else { locationId = cmbLocation.SelectedIndex + 1; }
                if (ChkAllTerminals.Checked == true) { terminalId = 0; } else { terminalId = cmbTerminal.SelectedIndex + 1; }

                if (ChkAllTerminals.Checked == false && ChkAllLocations.Checked == false)
                {
                    if (ValidateComboBoxes().Equals(false)) { return; }
                }


                this.Cursor = Cursors.WaitCursor;
                if (posSalesSummeryService.View(locationId, terminalId, dateFrom, dateTo, false) == true)
                {
                    ViewReport();

                }
                else
                {
                    Toast.Show("Error in data Processing.", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }
            }
            this.Cursor = Cursors.Default;
           
        }

        public override void Print()
        {

            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            int locationId = 0;
            int terminalId = 0;

            DateTime dateFrom = dtpFromDate.Value;
            DateTime dateTo = dtpToDate.Value;

            if (dateFrom > dateTo)
            {
                Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDateRange);
                return;
            }

            if (ChkLocationWise.Checked == true)
            {
                this.Cursor = Cursors.WaitCursor;
                if (posSalesSummeryService.View(locationId, terminalId, dateFrom, dateTo, true) == true)
                {
                    LocationWisePrintReport();

                }
                else
                {
                    Toast.Show("Error in data Processing.", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }
            }
            else
            {

                if (ChkAllLocations.Checked == true) { locationId = 0; } else { locationId = cmbLocation.SelectedIndex + 1; }
                if (ChkAllTerminals.Checked == true) { terminalId = 0; } else { terminalId = cmbTerminal.SelectedIndex + 1; }

                if (ChkAllTerminals.Checked == false && ChkAllLocations.Checked == false)
                {
                    if (ValidateComboBoxes().Equals(false)) { return; }
                }


                this.Cursor = Cursors.WaitCursor;
                if (posSalesSummeryService.View(locationId, terminalId, dateFrom, dateTo, false) == true)
                {
                    PrintReport();

                }
                else
                {
                    Toast.Show("Error in data Processing.", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }
            }
            this.Cursor = Cursors.Default;

        }

        private void ViewReport()
        {
           
            FrmReportViewer reportViewer = new FrmReportViewer();
            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            InvRptPosSalesSummery invRptPosSalesSummary = new InvRptPosSalesSummery();

            //DataTable dt = posSalesSummeryService.GetSalesSum();

            invRptPosSalesSummary.SetDataSource(posSalesSummeryService.GetSalesSum());
            invRptPosSalesSummary.SummaryInfo.ReportTitle = "POS Sales Summary";
            invRptPosSalesSummary.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptPosSalesSummary.DataDefinition.FormulaFields["SelectLocation"].Text = "'" + cmbLocation.Text.Trim() + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["SelectTerminal"].Text = "'" + cmbTerminal.Text.Trim() + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["DateFrom"].Text =  "'" + dtpFromDate.Text + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpToDate.Text + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            reportViewer.crRptViewer.ReportSource = invRptPosSalesSummary;
            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;
          
        }

        private void LocationWiseViewReport()
        {

            FrmReportViewer reportViewer = new FrmReportViewer();
            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            InvRptPosSalesSummeryLocationWise invRptPosSalesSummeryLocationWise = new InvRptPosSalesSummeryLocationWise();

            DataTable dt = new DataTable();

            dt = posSalesSummeryService.GetSalesSum();

            invRptPosSalesSummeryLocationWise.SetDataSource(posSalesSummeryService.GetSalesSum());

            invRptPosSalesSummeryLocationWise.SummaryInfo.ReportTitle = "POS Sales Summary";
            invRptPosSalesSummeryLocationWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["SelectLocation"].Text = "'" + cmbLocation.Text.Trim() + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["SelectTerminal"].Text = "'" + cmbTerminal.Text.Trim() + "'";

            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFromDate.Text + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpToDate.Text + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            reportViewer.crRptViewer.ReportSource = invRptPosSalesSummeryLocationWise;
            reportViewer.WindowState = FormWindowState.Maximized;
            reportViewer.Show();
            Cursor.Current = Cursors.Default;

        }

        private void PrintReport()
        {

            FrmReportViewer reportViewer = new FrmReportViewer();
            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            InvRptPosSalesSummery invRptPosSalesSummary = new InvRptPosSalesSummery();

            //DataTable dt = posSalesSummeryService.GetSalesSum();

            invRptPosSalesSummary.SetDataSource(posSalesSummeryService.GetSalesSum());
            invRptPosSalesSummary.SummaryInfo.ReportTitle = "POS Sales Summary";
            invRptPosSalesSummary.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptPosSalesSummary.DataDefinition.FormulaFields["SelectLocation"].Text = "'" + cmbLocation.Text.Trim() + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["SelectTerminal"].Text = "'" + cmbTerminal.Text.Trim() + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFromDate.Text + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpToDate.Text + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptPosSalesSummary.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            
            invRptPosSalesSummary.PrintToPrinter(1, false, 0, 0);
            Cursor.Current = Cursors.Default;

        }

        private void LocationWisePrintReport()
        {

            FrmReportViewer reportViewer = new FrmReportViewer();
            PosSalesSummeryService posSalesSummeryService = new PosSalesSummeryService();

            InvRptPosSalesSummeryLocationWise invRptPosSalesSummeryLocationWise = new InvRptPosSalesSummeryLocationWise();

            //DataTable dt = posSalesSummeryService.GetSalesSum();

            invRptPosSalesSummeryLocationWise.SetDataSource(posSalesSummeryService.GetSalesSum());
            invRptPosSalesSummeryLocationWise.SummaryInfo.ReportTitle = "POS Sales Summary";
            invRptPosSalesSummeryLocationWise.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["SelectLocation"].Text = "'" + cmbLocation.Text.Trim() + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["SelectTerminal"].Text = "'" + cmbTerminal.Text.Trim() + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFromDate.Text + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpToDate.Text + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            invRptPosSalesSummeryLocationWise.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            invRptPosSalesSummeryLocationWise.PrintToPrinter(1, false, 0, 0);
            Cursor.Current = Cursors.Default;

        }

        private void ChkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllLocations.Checked == true)
            {
                Common.ClearComboBox(cmbLocation);
                cmbLocation.Enabled = false;
            }
            else
            {
                cmbLocation.Enabled = true;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void ChkLocationWise_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLocationWise.Checked == true)
            {
                ChkAllLocations.Checked = true;
                ChkAllTerminals.Checked = true;
            }
            else
            {
                ChkAllLocations.Checked = false;
                ChkAllTerminals.Checked = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
