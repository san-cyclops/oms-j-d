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
    public partial class FrmCrmPointsBreakdown : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";
        List<PointsBreakdown> updatedPointsBreakdownList = new List<PointsBreakdown>();
        List<PointsBreakdown> InitialPointsBreakdownList = new List<PointsBreakdown>();  

        public FrmCrmPointsBreakdown(string formText)
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
            prgBar.Value = prgBar.Minimum;
            errorProvider.Clear();

            LoyaltyCustomerService loyaltyCustomerService = new Service.LoyaltyCustomerService();
            //List<PointsBreakdown> pointsBreakdownList = new List<PointsBreakdown>();

            InitialPointsBreakdownList = loyaltyCustomerService.GetInitialBreakdown();
            dgvBreakdown.DataSource = InitialPointsBreakdownList;
            dgvBreakdown.Refresh();

            updatedPointsBreakdownList = new List<PointsBreakdown>();

            base.InitializeForm();
        }

        public override void FormLoad()
        {
            this.Text = formDisplayText;
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

            dtpFromDate.Value = Common.GetSystemDate();
            dtpToDate.Value = Common.GetSystemDate();

            dgvBreakdown.AutoGenerateColumns = false;

            base.FormLoad();
        }

        

        public override void ClearForm()
        {
            try
            {
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

                LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                DateTime dateFrom;
                DateTime dateTo;

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

                UpdateList();

                if (loyaltyCustomerService.UpdatePointsBreakdownList(updatedPointsBreakdownList, dateFrom, dateTo))
                {
                    ViewReportAllCustomers();
                    prgBar.Value = prgBar.Maximum;
                }
                else
                {
                    Toast.Show("Error found in report", Toast.messageType.Information, Toast.messageAction.General);
                    return;
                }

                bgWorker = null;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public void UpdateList()
        {
            updatedPointsBreakdownList = new List<PointsBreakdown>();
            foreach (DataGridViewRow row in dgvBreakdown.Rows)
            {
                PointsBreakdown pointsBreakdown = new PointsBreakdown();
                pointsBreakdown.PointsBreakdownID = Common.ConvertStringToLong(dgvBreakdown["PointsBreakdownID", row.Index].Value.ToString());
                pointsBreakdown.RangeFrom = Common.ConvertStringToDecimal(dgvBreakdown["RangeFrom", row.Index].Value.ToString());
                pointsBreakdown.RangeTo = Common.ConvertStringToDecimal(dgvBreakdown["RangeTo", row.Index].Value.ToString());

                updatedPointsBreakdownList.Add(pointsBreakdown);
            }
        }

        private void ViewReportAllCustomers()   
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            CrmRptPointsBreakDown crmRptPointsBreakDown = new CrmRptPointsBreakDown();

            crmRptPointsBreakDown.SetDataSource(loyaltyCustomerService.GetPointsBreakDownDetails());

            crmRptPointsBreakDown.SummaryInfo.ReportTitle = "Points Breakdown";
            crmRptPointsBreakDown.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptPointsBreakDown.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptPointsBreakDown.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            prgBar.Value = prgBar.Maximum;

            objReportView.crRptViewer.ReportSource = crmRptPointsBreakDown;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }


        private void ClearSelection()
        {
            
        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Validater.ValidateAmount(sender, e);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void txtTotalVisit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dgvBreakdown_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.F2))
                {
                    if (dgvBreakdown.CurrentCell != null && dgvBreakdown.CurrentCell.RowIndex >= 0)
                    {
                        LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
                        PointsBreakdown pointsBreakdown = new PointsBreakdown();
                        pointsBreakdown.PointsBreakdownID = Common.ConvertStringToLong(dgvBreakdown["PointsBreakdownID", dgvBreakdown.CurrentCell.RowIndex].Value.ToString().Trim());

                        if (pointsBreakdown != null)
                        {
                            InitialPointsBreakdownList = loyaltyCustomerService.GetDeletePointsRange(InitialPointsBreakdownList, pointsBreakdown);
                        }

                        dgvBreakdown.DataSource = null;
                        dgvBreakdown.DataSource = InitialPointsBreakdownList;
                        dgvBreakdown.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

    }
}
