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
using System.Threading;

namespace Report.GUI
{
    public partial class FrmCrmCashierPerformance : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmCashierPerformance(string formText)
        {
            InitializeComponent();
            formDisplayText = formText;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
        }

        public override void InitializeForm()
        {
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            LocationService locationService = new LocationService();

            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());
            Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());

            bgWorker = null;

            prgBar.Value = prgBar.Minimum;
            errorProvider.Clear();

            cmbLocation.SelectedValue = Common.LoggedLocationID;

            base.InitializeForm();
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

            
            //cmbLocation.Enabled = false;

            base.FormLoad();
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

                this.Cursor = Cursors.Default;

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
                if (bgWorker == null) { bgWorker = new BackgroundWorker(); }
                prgBar.Value = prgBar.Minimum;
                bgWorker.RunWorkerAsync();

                DateTime dateFrom;
                DateTime dateTo;

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;

                if (!rdoApapaima.Checked)
                {
                    if (ValidateCardType().Equals(false)) { return; }
                }

                if (chkAlllocations.Checked)
                {
                    if (rdoApapaima.Checked)
                    {
                        ViewReportAllLocationsArapaima(dateFrom, dateTo);
                        prgBar.Value = prgBar.Maximum;
                    }
                    else
                    {
                        ViewReportAllLocations(Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                        prgBar.Value = prgBar.Maximum;
                    }
                }
                else
                {
                    if (rdoApapaima.Checked)
                    {
                        ViewReportSelectedLocationArapaima(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), dateFrom, dateTo);
                        prgBar.Value = prgBar.Maximum;
                    }
                    else
                    {
                        ViewReportSelectedLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                        prgBar.Value = prgBar.Maximum;
                    }
                }


                bgWorker = null;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private bool ValidateCardType()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCardType);
        }

        private bool ValidateLocationComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbLocation);
        }

        private void ViewReportAllLocations(int cardType, DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCashierPerformancesAllLocations crmRptCashierPerformancesAllLocations = new CrmRptCashierPerformancesAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetDataSourceCashierPerformanceAllLocations(cardType, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCashierPerformancesAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["CashierPerformances"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptCashierPerformancesAllLocations.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptCashierPerformancesAllLocations.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptCashierPerformancesAllLocations.SummaryInfo.ReportTitle = "Cashier Performances (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptCashierPerformancesAllLocations.SummaryInfo.ReportTitle = "Cashier Performances (Arrowana Member)";
            }

            crmRptCashierPerformancesAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCashierPerformancesAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportSelectedLocation(int locationID, int cardType, DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCashierPerformances crmRptCashierPerformances = new CrmRptCashierPerformances();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetDataSourceCashierPerformanceSelectedLocations(locationID, cardType, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCashierPerformances.SetDataSource(loyaltyCustomerService.DsReport.Tables["CashierPerformances"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptCashierPerformances.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptCashierPerformances.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptCashierPerformances.SummaryInfo.ReportTitle = "Cashier Performances (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptCashierPerformances.SummaryInfo.ReportTitle = "Cashier Performances (Arrowana Member)";
            }

            crmRptCashierPerformances.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCashierPerformances.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptCashierPerformances.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCashierPerformances;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportAllLocationsArapaima(DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCashierPerformancesAllLocations crmRptCashierPerformancesAllLocations = new CrmRptCashierPerformancesAllLocations();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetDataSourceCashierPerformanceAllLocationsArapaima(fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCashierPerformancesAllLocations.SetDataSource(loyaltyCustomerService.DsReport.Tables["CashierPerformances"]);

            crmRptCashierPerformancesAllLocations.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima)";

            crmRptCashierPerformancesAllLocations.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCashierPerformancesAllLocations.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCashierPerformancesAllLocations;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportSelectedLocationArapaima(int locationID, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptCashierPerformances crmRptCashierPerformances = new CrmRptCashierPerformances();

            this.Cursor = Cursors.WaitCursor;
            loyaltyCustomerService.GetDataSourceCashierPerformanceSelectedLocationsArapaima(locationID, fromDate, toDate);
            this.Cursor = Cursors.Default;

            crmRptCashierPerformances.SetDataSource(loyaltyCustomerService.DsReport.Tables["CashierPerformances"]);

            crmRptCashierPerformances.SummaryInfo.ReportTitle = "Cashier Performances (Arapaima)";

            crmRptCashierPerformances.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptCashierPerformances.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptCashierPerformances.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptCashierPerformances.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptCashierPerformances;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void chkAlllocations_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAlllocations.Checked)
                {
                    cmbLocation.SelectedIndex = -1;
                    cmbLocation.Enabled = false;
                }
                else
                {
                    cmbLocation.Enabled = true;
                    cmbLocation.SelectedValue = Common.LoggedLocationID;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prgBar.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(500);
                    if (bgWorker == null) { break; }
                    bgWorker.ReportProgress(i);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rdoApapaima_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoApapaima.Checked)
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
