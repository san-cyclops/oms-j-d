﻿using System;
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
    public partial class FrmCrmLostAndRenewDetail : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmLostAndRenewDetail(string formText)
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
            CrmRptLostAndReNewSelectedLocation crmRptLostAndReNewSelectedLocation = new CrmRptLostAndReNewSelectedLocation();

            loyaltyCustomerService.GetLostAndRenewDataSourceArapaimaSelectedLocation(locationId, cardType, fromDate, toDate);
            crmRptLostAndReNewSelectedLocation.SetDataSource(loyaltyCustomerService.DsReport.Tables["CrmLostAndReNew"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportTitle = "Lost And Renew (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportTitle = "Lost And Renew (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportTitle = "Lost And Renew (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportTitle = "Lost And Renew (Arrowana Member)";
            }

            crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLostAndReNewSelectedLocation;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReport(int cardType, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLostAndReNew crmRptLostAndReNew = new CrmRptLostAndReNew();

            loyaltyCustomerService.GetLostAndReNewDataSourceArapaima(cardType, fromDate, toDate);
            crmRptLostAndReNew.SetDataSource(loyaltyCustomerService.DsReport.Tables["CrmLostAndReNew"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptLostAndReNew.SummaryInfo.ReportTitle = "Lost And Renew (Arapaima Silver)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptLostAndReNew.SummaryInfo.ReportTitle = "Lost And Renew (Arapaima Gold)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 3)
            {
                crmRptLostAndReNew.SummaryInfo.ReportTitle = "Lost And Renew (Arrowana Association Guide)";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 4)
            {
                crmRptLostAndReNew.SummaryInfo.ReportTitle = "Lost And Renew (Arrowana Member)";
            }

            crmRptLostAndReNew.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLostAndReNew.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            
            crmRptLostAndReNew.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLostAndReNew;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportArapaima(DateTime fromDate, DateTime toDate)  
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLostAndReNew crmRptLostAndReNew = new CrmRptLostAndReNew();

            loyaltyCustomerService.GetLostAndReNewDataSourceArapaima(fromDate, toDate);
            crmRptLostAndReNew.SetDataSource(loyaltyCustomerService.DsReport.Tables["CrmLostAndReNew"]);

            crmRptLostAndReNew.SummaryInfo.ReportTitle = "Lost And Renew Details (Arapaima)";

            crmRptLostAndReNew.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLostAndReNew.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";

            crmRptLostAndReNew.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLostAndReNew.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLostAndReNew;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportArapaimaSelectedLocation(int locationID, DateTime fromDate, DateTime toDate) 
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptLostAndReNewSelectedLocation crmRptLostAndReNewSelectedLocation = new CrmRptLostAndReNewSelectedLocation();

            loyaltyCustomerService.GetLostAndRenewDataSourceArapaimaSelectedLocation(locationID, fromDate, toDate);
            crmRptLostAndReNewSelectedLocation.SetDataSource(loyaltyCustomerService.DsReport.Tables["CrmLostAndReNew"]);

            crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportTitle = "Lost And Renew Details (Arapaima)";

            crmRptLostAndReNewSelectedLocation.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLostAndReNewSelectedLocation.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            objReportView.crRptViewer.ReportSource = crmRptLostAndReNewSelectedLocation;
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
