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
    public partial class FrmCrmTopCustomerPerformance : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmTopCustomerPerformance(string formText)
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
            Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());

            bgWorker = null;
            prgBar.Value = prgBar.Minimum;
            errorProvider.Clear();

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
                this.Cursor = Cursors.Default;

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
                if (bgWorker == null) { bgWorker = new BackgroundWorker(); }
                prgBar.Value = prgBar.Minimum;
                bgWorker.RunWorkerAsync();

                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                DateTime dateFrom;
                DateTime dateTo;
                int select; 

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;
                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

                if (ValidateCardType().Equals(false)) { bgWorker = null; return; }
                if (ValidateControls().Equals(false)) { bgWorker = null; return; }

                select = Common.ConvertStringToInt(txtSelect.Text.ToString());

                if (!loyaltyCustomerService.GetLoyaltyTransactions(dateFrom, dateTo, Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), select)) 
                {
                    Toast.Show("Error found", Toast.messageType.Information, Toast.messageAction.General);
                    bgWorker = null;
                    return;
                }

                ViewReportAllCustomers(select, dateFrom, dateTo);

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

        private bool ValidateControls() 
        {
            return Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, txtSelect);
        }

        private void ViewReportAllCustomers(int Select, DateTime dateFrom, DateTime dateTo)   
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            CrmRptTopCustomerPerformances crmRptTopCustomerPerformances = new CrmRptTopCustomerPerformances();

            crmRptTopCustomerPerformances.SetDataSource(loyaltyCustomerService.GetTopCustomers());

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()).Equals(1))
            {
                crmRptTopCustomerPerformances.SummaryInfo.ReportTitle = "Top n Customer Performances (Arapaima Silver)";
            }
            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()).Equals(2))
            {
                crmRptTopCustomerPerformances.SummaryInfo.ReportTitle = "Top n Customer Performances (Arapaima Gold)";
            }
            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()).Equals(3))
            {
                crmRptTopCustomerPerformances.SummaryInfo.ReportTitle = "Top n Customer Performances (Arapaima Arrowana Association Guide)";
            }
            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()).Equals(4))
            {
                crmRptTopCustomerPerformances.SummaryInfo.ReportTitle = "Top n Customer Performances (Arrowana Member)"; 
            }
            crmRptTopCustomerPerformances.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["FromDate"].Text = "'" + dateFrom + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["ToDate"].Text = "'" + dateTo + "'";

            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            crmRptTopCustomerPerformances.DataDefinition.FormulaFields["Select"].Text = "'" + Select + "'";

            prgBar.Value = prgBar.Maximum;

            objReportView.crRptViewer.ReportSource = crmRptTopCustomerPerformances;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportSelectedlCustomers(long customerId, decimal amount, int totalVisits)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            CrmRptGoldCardSelection crmRptGoldCardSelection = new CrmRptGoldCardSelection();

            crmRptGoldCardSelection.SetDataSource(loyaltyCustomerService.GetGoldCardSelectionSelectedCustomers(amount, customerId, totalVisits));

            crmRptGoldCardSelection.SummaryInfo.ReportTitle = "Gold Card Selection Critaria";
            crmRptGoldCardSelection.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptGoldCardSelection.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptGoldCardSelection.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptGoldCardSelection.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptGoldCardSelection.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptGoldCardSelection.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            prgBar.Value = prgBar.Maximum;

            objReportView.crRptViewer.ReportSource = crmRptGoldCardSelection;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
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

        private void txtSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateFigure(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
