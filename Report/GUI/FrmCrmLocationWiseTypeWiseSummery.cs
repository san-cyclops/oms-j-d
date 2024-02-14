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
    public partial class FrmCrmLocationWiseTypeWiseSummery : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmLocationWiseTypeWiseSummery(string formText)
        {
            InitializeComponent();
            formDisplayText = formText;
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

                if (!rdoApapaima.Checked)
                {
                    if (ValidateCardType().Equals(false)) { return; }
                }

                if (chkAlllocations.Checked)
                {
                    if (rdoApapaima.Checked)
                    {
                        ViewReportArapaima(dateFrom, dateTo);
                    }
                    else
                    {
                        ViewReport(Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                    }
                }
                else
                {
                    if (rdoApapaima.Checked)
                    {
                        ViewReportArapaimaSelectedLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), dateFrom, dateTo);
                    }
                    else
                    {
                        ViewReportSelectedLocation(Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dateFrom, dateTo);
                    }
                }

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

        private void ViewReportSelectedLocation(int locationId, int cardType, DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLocationWiseTypeWiseSummery crmRptLocationWiseTypeWiseSummery = new CrmRptLocationWiseTypeWiseSummery();

            loyaltyCustomerService.GetDataSource(locationId, cardType, fromDate, toDate);
            crmRptLocationWiseTypeWiseSummery.SetDataSource(loyaltyCustomerService.DsReport.Tables["LocationWiseSummery"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Silver Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Gold Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arrowana Association Guide Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arrowana Member Summary";
            }

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLocationWiseTypeWiseSummery;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReport(int cardType, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLocationWiseTypeWiseSummery crmRptLocationWiseTypeWiseSummery = new CrmRptLocationWiseTypeWiseSummery();

            loyaltyCustomerService.GetDataSource(cardType, fromDate, toDate);
            crmRptLocationWiseTypeWiseSummery.SetDataSource(loyaltyCustomerService.DsReport.Tables["LocationWiseSummery"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Silver Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Gold Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arrowana Association Guide Summary";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arrowana Member Summary";
            }

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLocationWiseTypeWiseSummery;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportArapaima(DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLocationWiseTypeWiseSummery crmRptLocationWiseTypeWiseSummery = new CrmRptLocationWiseTypeWiseSummery();

            loyaltyCustomerService.GetDataSourceArapaima(fromDate, toDate);
            crmRptLocationWiseTypeWiseSummery.SetDataSource(loyaltyCustomerService.DsReport.Tables["LocationWiseSummery"]);

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Summary";

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLocationWiseTypeWiseSummery;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportArapaimaSelectedLocation(int locationID, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLocationWiseTypeWiseSummery crmRptLocationWiseTypeWiseSummery = new CrmRptLocationWiseTypeWiseSummery();

            loyaltyCustomerService.GetDataSourceArapaimaSelectedLocation(locationID, fromDate, toDate);
            crmRptLocationWiseTypeWiseSummery.SetDataSource(loyaltyCustomerService.DsReport.Tables["LocationWiseSummery"]);

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportTitle = "Location wise Arapaima Summary";

            crmRptLocationWiseTypeWiseSummery.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLocationWiseTypeWiseSummery.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLocationWiseTypeWiseSummery;
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
