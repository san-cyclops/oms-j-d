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
using System.Threading;

namespace Report.GUI
{
    public partial class FrmOrderSystemSalesReport : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmOrderSystemSalesReport(string formText)
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

            CustomerService customerService = new CustomerService();
            Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
            Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);

            ////Load Sales persons
            InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
            Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
            Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);

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
                cmbCompanyLocation.Focus();
                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();
                rbnCustomerWise.Checked = false;
                rbnSalesPersonWise.Checked = false;
                chkSummary.Checked = false;
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



                if (chkAll.Checked)
                {
                    ViewReportAllCompany(dateFrom, dateTo);
                }
                else
                {
                    if (cmbCompanyLocation.Text != string.Empty)
                    {
                        ViewReportCustomerWiseAllLocation(cmbCompanyLocation.Text.Trim(), dateFrom, dateTo);
                    }
                    else
                    { MessageBox.Show("Please Select Company..."); }
                }
                prgBar.Value = prgBar.Maximum;
 
                 
                bgWorker = null;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

         

        private bool ValidateLocationComboBoxes()
        {
            return Validater.ValidateComboBox(errorProvider, Validater.ValidateType.Empty, cmbCompanyLocation);
        }

        private void ViewReportCustomerWiseAllLocation(string company, DateTime fromDate, DateTime toDate)    
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            InvSalesReportDtls invSalesReport = new InvSalesReportDtls();
            InvSalesReportSum invSalesReportSum = new InvSalesReportSum();
            this.Cursor = Cursors.WaitCursor;

            if (!chkSummary.Checked)
            {
                if (rbnSalesPersonWise.Checked)
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 2, txtSalesPersonCode.Text));
                }
                else if (rbnCustomerWise.Checked)
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 1, txtCustomerCode.Text));
                }
                else
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 3, ""));
                }
                this.Cursor = Cursors.Default;
                if (company == "POLY-PACKAGING")
                {
                    invSalesReport.SummaryInfo.ReportTitle = "POLY-PACKAGING";
                }
                else if (company == "")
                {
                    invSalesReport.SummaryInfo.ReportTitle = "VENTURE";
                }
                invSalesReport.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReport.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReport.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReport.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReport.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReport.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReport.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReport;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if (rbnSalesPersonWise.Checked)
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 2, txtSalesPersonCode.Text));
                }
                else if (rbnCustomerWise.Checked)
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 1, txtCustomerCode.Text));
                }
                else
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(company, fromDate, toDate, 3, ""));
                }

                this.Cursor = Cursors.Default;
                if (company == "POLY-PACKAGING")
                {
                    invSalesReportSum.SummaryInfo.ReportTitle = "POLY-PACKAGING";
                }
                else if (company == "")
                {
                    invSalesReportSum.SummaryInfo.ReportTitle = "VENTURE";
                }
                invSalesReportSum.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReportSum.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReportSum.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReportSum.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReportSum.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReportSum.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReportSum.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReportSum.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReportSum.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReportSum;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
            }
        }



        private void ViewReportAllCompany(DateTime fromDate, DateTime toDate)
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            InvSalesReportDtls invSalesReport = new InvSalesReportDtls();
            InvSalesReportSum invSalesReportSum = new InvSalesReportSum();

            this.Cursor = Cursors.WaitCursor;


            if (!rbnCustomerWise.Checked && !rbnSalesPersonWise.Checked && !chkSummary.Checked)
            {
                invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate,3,""));
                this.Cursor = Cursors.Default;
                invSalesReport.SummaryInfo.ReportTitle = "J & D Group";
                invSalesReport.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReport.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReport.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReport.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReport.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReport.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReport.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReport;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
            }
            else if (rbnCustomerWise.Checked && !chkSummary.Checked)
            {

                invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 1, txtCustomerCode.Text));
                this.Cursor = Cursors.Default;

                invSalesReport.SummaryInfo.ReportTitle = "J & D Group";

                invSalesReport.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

                invSalesReport.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReport.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";

                invSalesReport.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReport.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReport.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReport.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invSalesReport;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;

            }
            else if (rbnSalesPersonWise.Checked && !chkSummary.Checked)
            {
                invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 2, txtSalesPersonCode.Text));
                this.Cursor = Cursors.Default;
                invSalesReport.SummaryInfo.ReportTitle = "J & D Group";
                invSalesReport.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReport.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReport.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReport.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReport.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReport.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReport.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReport;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;

            }
            else if (!chkSummary.Checked)
            {
                if (rbnSalesPersonWise.Checked)
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 2, txtSalesPersonCode.Text));
                }
                else if (rbnCustomerWise.Checked)
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 1, txtCustomerCode.Text));
                }
                else
                {
                    invSalesReport.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 3, ""));
                }
                this.Cursor = Cursors.Default;
                invSalesReport.SummaryInfo.ReportTitle = "J & D Group";
                invSalesReport.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReport.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReport.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReport.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReport.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReport.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReport.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReport.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReport;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if (rbnSalesPersonWise.Checked)
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 2, txtSalesPersonCode.Text));
                }
                else if (rbnCustomerWise.Checked)
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 1, txtCustomerCode.Text));
                }
                else
                {
                    invSalesReportSum.SetDataSource(loyaltyCustomerService.GetSalesDtl(fromDate, toDate, 3, ""));
                }
                this.Cursor = Cursors.Default;
                invSalesReportSum.SummaryInfo.ReportTitle = "J & D Group";
                invSalesReportSum.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invSalesReportSum.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invSalesReportSum.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invSalesReportSum.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invSalesReportSum.DataDefinition.FormulaFields["LocationName"].Text = "'All Locations'";
                invSalesReportSum.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invSalesReportSum.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invSalesReportSum.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invSalesReportSum.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";
                objReportView.crRptViewer.ReportSource = invSalesReportSum;
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
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

        private void btnClear_Click(object sender, EventArgs e)
        {

        }


        private void chkAutoCompleationCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Common.SetAutoComplete(txtCustomerCode, customerService.GetAllCustomerCodes(), chkAutoCompleationCustomer.Checked);
                Common.SetAutoComplete(txtCustomerName, customerService.GetAllCustomerNames(), chkAutoCompleationCustomer.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void chkAutoCompleationSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                Common.SetAutoComplete(txtSalesPersonCode, invSalesPersonService.GetAllInvSalesPersonCodes(), chkAutoCompleationSalesPerson.Checked);
                Common.SetAutoComplete(txtSalesPersonName, invSalesPersonService.GetAllInvSalesPersonNames(), chkAutoCompleationSalesPerson.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void rbnCustomerWise_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtCustomerName.Focus(); }


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtCustomerCode.Text.Trim().Equals(string.Empty))
                { LoadCustomer(true, txtCustomerCode.Text.Trim()); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }

        }
        private void LoadCustomer(bool isCode, string strCustomer)
        {
            try
            {
                CustomerService customerService = new CustomerService();
                Customer existingCustomer = new Customer();

                if (isCode)
                {
                    existingCustomer = customerService.GetCustomersByCode(strCustomer);
                    if (isCode && strCustomer.Equals(string.Empty))
                    {
                        txtCustomerCode.Focus();
                        return;
                    }
                }
                else
                    existingCustomer = customerService.GetCustomersByName(strCustomer);

                if (existingCustomer != null)
                {
                    txtCustomerCode.Text = existingCustomer.CustomerCode;
                    txtCustomerName.Text = existingCustomer.CustomerName;
                    txtSalesPersonCode.Focus();
                }
                else
                {
                    Toast.Show("Customer - " + strCustomer.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadSalesPerson(bool isCode, string strsalesPerson)
        {
            try
            {
                InvSalesPersonService invSalesPersonService = new InvSalesPersonService();
                InvSalesPerson existingInvSalesPerson = new InvSalesPerson();

                if (isCode)
                {
                    existingInvSalesPerson = invSalesPersonService.GetInvSalesPersonByCode(strsalesPerson);
                    if (isCode && strsalesPerson.Equals(string.Empty))
                    {
                        txtSalesPersonCode.Focus();
                        return;
                    }
                }
                else
                { existingInvSalesPerson = invSalesPersonService.GetInvSalesPersonByName(strsalesPerson); }

                if (existingInvSalesPerson != null)
                {
                    txtSalesPersonCode.Text = existingInvSalesPerson.SalesPersonCode;
                    txtSalesPersonName.Text = existingInvSalesPerson.SalesPersonName;
                }
                else
                {
                    Toast.Show("Sales Person - " + strsalesPerson.Trim(), Toast.messageType.Warning, Toast.messageAction.NotExists);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }



        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode.Equals(Keys.Tab))
                { txtSalesPersonCode.Focus(); }


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }



                txtSalesPersonName.Focus();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.KeyCode.Equals(Keys.Enter))
                { return; }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void txtSalesPersonName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonName.Text.Trim().Equals(string.Empty))
                { LoadSalesPerson(false, txtSalesPersonName.Text.Trim()); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadCustomer(false, txtCustomerName.Text.Trim());
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

        private void txtSalesPersonCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtSalesPersonCode.Text.Trim().Equals(string.Empty))
                { LoadSalesPerson(true, txtSalesPersonCode.Text.Trim()); }
            }
            catch (Exception ex)
            { Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID); }
        }

    }
}