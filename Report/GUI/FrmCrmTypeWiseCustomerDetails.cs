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
    public partial class FrmCrmTypeWiseCustomerDetails : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmTypeWiseCustomerDetails(string formText)
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

            prgBar.Value = prgBar.Minimum;
            errorProvider.Clear();

            Common.LoadCardTypes(cmbCardType, loyaltyCustomerService.GetAllCardTypes());

            base.InitializeForm();
        }

        public override void FormLoad()
        {
            //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            this.Text = formDisplayText;

            //documentID = autoGenerateInfo.DocumentID;
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

            //cmbLocation.Enabled = false;

            LoadSearchCodes();
            base.FormLoad();
        }

        private void LoadSearchCodes()
        {
            try
            {

                if (ChkAutoComplteFrom.Checked) { LoadCustomersFrom(); }
                if (ChkAutoComplteTo.Checked) { LoadCustomersTo(); }


            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void LoadCustomersFrom() 
        {
            try
            {
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                Common.SetAutoComplete(TxtSearchCodeFrom, loyaltyCustomerService.GetAllLoyaltyCustomerCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, loyaltyCustomerService.GetAllLoyaltyCustomerNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadCustomersTo() 
        {
            try
            {
                LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                Common.SetAutoComplete(TxtSearchCodeTo, loyaltyCustomerService.GetAllLoyaltyCustomerCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, loyaltyCustomerService.GetAllLoyaltyCustomerNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        public override void ClearForm()
        {
            try
            {
                TxtSearchCodeFrom.Text = string.Empty;
                TxtSearchCodeTo.Text = string.Empty;
                TxtSearchNameFrom.Text = string.Empty;
                TxtSearchNameTo.Text = string.Empty;

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

                if (pnlSelection.Enabled)
                {
                    //if (ValidateLocationComboBoxes().Equals(false)) { return; }
                    if (ValidateControls() == false) return;
                }

                if (ValidateCardType().Equals(false)) { return; }

                if (chkAllCostomers.Checked)
                {
                    ViewReportAllcustomer(Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()));
                    prgBar.Value = prgBar.Maximum;
                }
                else
                {
                    LoyaltyCustomer loyaltyCustomerFrom = new LoyaltyCustomer();
                    LoyaltyCustomer loyaltyCustomerTo = new LoyaltyCustomer();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    loyaltyCustomerFrom = loyaltyCustomerService.GetLoyaltyCustomerByCode(TxtSearchCodeFrom.Text.Trim());
                    loyaltyCustomerTo = loyaltyCustomerService.GetLoyaltyCustomerByCode(TxtSearchCodeTo.Text.Trim());

                    if (loyaltyCustomerFrom == null)
                    {
                        Toast.Show("Invalid customer code", Toast.messageType.Information, Toast.messageAction.General);
                        TxtSearchCodeFrom.SelectAll();
                        TxtSearchCodeFrom.Focus();
                        return;
                    }

                    if (loyaltyCustomerTo == null)
                    {
                        Toast.Show("Invalid customer code", Toast.messageType.Information, Toast.messageAction.General);
                        TxtSearchCodeTo.SelectAll();
                        TxtSearchCodeTo.Focus();
                        return;
                    }
                    ViewReportSelectedcustomer(loyaltyCustomerFrom.LoyaltyCustomerID, loyaltyCustomerTo.LoyaltyCustomerID, Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()));
                    prgBar.Value = prgBar.Maximum;                
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

        private bool ValidateControls()
        {
            if (!Validater.ValidateTextBox(errorProvider, Validater.ValidateType.Empty, TxtSearchCodeFrom, TxtSearchCodeTo))
            { return false; }

            else
            {
                isValidControls = true;
                this.ValidateChildren();

                return isValidControls;
            }
        }

        private void ViewReportSelectedcustomer(long customerIdFrom, long customerIdTo, int cardType)    
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptTypeWiseCustomerDetails crmRptTypeWiseCustomerDetails = new CrmRptTypeWiseCustomerDetails();

            loyaltyCustomerService.GetTypeWiseSelectedCustomerDetails(customerIdFrom, customerIdTo, cardType);

            crmRptTypeWiseCustomerDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["TypeWiseCustomerDetails"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arapaima Silver Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arapaima Gold Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arrowana Association Guide Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arrowana Member Customer Details";
            }


            crmRptTypeWiseCustomerDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            //crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["AmountGraterThan"].Text = "'" + txtAmount.Text.Trim() + "'";

            objReportView.crRptViewer.ReportSource = crmRptTypeWiseCustomerDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }


        private void ViewReportAllcustomer(int cardType)
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();
            CrmRptTypeWiseCustomerDetails crmRptTypeWiseCustomerDetails = new CrmRptTypeWiseCustomerDetails();

            loyaltyCustomerService.GetTypeWiseAllCustomerDetails(cardType);

            crmRptTypeWiseCustomerDetails.SetDataSource(loyaltyCustomerService.DsReport.Tables["TypeWiseCustomerDetails"]);

            if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 1)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arapaima Silver Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arapaima Gold Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arrowana Association Guide Customer Details";
            }
            else if (Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()) == 2)
            {
                crmRptTypeWiseCustomerDetails.SummaryInfo.ReportTitle = "Arrowana Member Customer Details";
            }


            crmRptTypeWiseCustomerDetails.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;

            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CodeFrom"].Text = "'All Customers'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CodeTo"].Text = "'All Customers'";

            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

            //crmRptTypeWiseCustomerDetails.DataDefinition.FormulaFields["AmountGraterThan"].Text = "'" + txtAmount.Text.Trim() + "'";

            objReportView.crRptViewer.ReportSource = crmRptTypeWiseCustomerDetails;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ChkAutoComplteFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteFrom.Checked)
                {
                    LoadCustomersFrom();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ChkAutoComplteTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteTo.Checked)
                {
                    LoadCustomersTo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    TxtSearchNameFrom.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeFrom.Text))
                {
                    return;
                }
                else
                {
                    LoyaltyCustomer loyaltyCustomerFrom = new LoyaltyCustomer();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    loyaltyCustomerFrom = loyaltyCustomerService.GetLoyaltyCustomerByCode(TxtSearchCodeFrom.Text.Trim());

                    if (loyaltyCustomerFrom != null)
                    {
                        TxtSearchCodeFrom.Text = loyaltyCustomerFrom.CustomerCode.Trim();
                        TxtSearchNameFrom.Text = loyaltyCustomerFrom.CustomerName.Trim();
                        TxtSearchCodeTo.Focus();
                    }
                    else
                    {
                        Toast.Show("Invalid customer code", Toast.messageType.Information, Toast.messageAction.General);
                        TxtSearchCodeFrom.SelectAll();
                        TxtSearchCodeFrom.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    TxtSearchNameTo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeTo.Text))
                {
                    return;
                }
                else
                {
                    LoyaltyCustomer loyaltyCustomerTo = new LoyaltyCustomer();
                    LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

                    loyaltyCustomerTo = loyaltyCustomerService.GetLoyaltyCustomerByCode(TxtSearchCodeTo.Text.Trim());

                    if (loyaltyCustomerTo != null)
                    {
                        TxtSearchCodeTo.Text = loyaltyCustomerTo.CustomerCode.Trim();
                        TxtSearchNameTo.Text = loyaltyCustomerTo.CustomerName.Trim();
                        TxtSearchNameTo.Focus();
                    }
                    else
                    {
                        Toast.Show("Invalid customer code", Toast.messageType.Information, Toast.messageAction.General);
                        TxtSearchCodeTo.SelectAll();
                        TxtSearchCodeTo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ClearSelection()
        {
            TxtSearchCodeFrom.Text = string.Empty;
            TxtSearchNameFrom.Text = string.Empty;
            TxtSearchCodeTo.Text = string.Empty;
            TxtSearchNameTo.Text = string.Empty;

        }

        private void chkAllCostomers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllCostomers.Checked)
                {
                    ClearSelection();
                    pnlSelection.Enabled = false;
                }
                else
                {
                    pnlSelection.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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


    }
}
