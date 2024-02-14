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
    public partial class FrmCrmLocationAnalysis : FrmBaseReportsForm
    {
        int documentID = 0;
        UserPrivileges accessRights = new UserPrivileges();
        bool isValidControls = true;
        string formDisplayText = "";

        public FrmCrmLocationAnalysis(string formText)
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
            LocationService locationService = new LocationService();
            Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

            //AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            //autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);
            this.Text = formDisplayText;

            //documentID = autoGenerateInfo.DocumentID;
            accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

            dtpFromDate.Value = Common.GetSystemDate();
            dtpToDate.Value = Common.GetSystemDate();

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
                Common.ClearComboBox(cmbLocation);
                cmbLocation.Focus();

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                TxtSearchCodeFrom.Text = string.Empty;
                TxtSearchCodeTo.Text = string.Empty;
                TxtSearchNameFrom.Text = string.Empty;
                TxtSearchNameTo.Text = string.Empty;

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
                decimal amount;

                dateFrom = dtpFromDate.Value;
                dateTo = dtpToDate.Value;



                if (pnlSelection.Enabled)
                {
                    //if (ValidateLocationComboBoxes().Equals(false)) { return; }
                    if (ValidateControls() == false) return;
                }

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

                if (ValidateCardType().Equals(false)) { return; }

                if (chkAllCostomers.Checked && chkAlllocations.Checked)
                {
                    ViewReportLocationWiseAnalysisCardType(1,1,0,0,0);
                }
                else if (chkAllCostomers.Checked)
                {
                    ViewReportLocationWiseAnalysisCardType(0,1, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()),0, 0);
                }
                else if (chkAlllocations.Checked)
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
                    ViewReportLocationWiseAnalysisCardType(1,0, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), loyaltyCustomerFrom.LoyaltyCustomerID, loyaltyCustomerTo.LoyaltyCustomerID);
                    
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

                    ViewReportLocationWiseAnalysisCardType(0,0, Common.ConvertStringToInt(cmbLocation.SelectedValue.ToString()), loyaltyCustomerFrom.LoyaltyCustomerID, loyaltyCustomerTo.LoyaltyCustomerID);
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

        private void ViewReport()
        {
            FrmReportViewer objReportView = new FrmReportViewer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            CrmRptLocationWiseAnalysis crmRptLocationWiseAnalysis = new CrmRptLocationWiseAnalysis();

            crmRptLocationWiseAnalysis.SetDataSource(loyaltyCustomerService.GetLocationWiseTypeWiseSummeryAllLocations());

            crmRptLocationWiseAnalysis.SummaryInfo.ReportTitle = "Location wise Loyalty Analysis";
            crmRptLocationWiseAnalysis.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
            crmRptLocationWiseAnalysis.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";



            objReportView.crRptViewer.ReportSource = crmRptLocationWiseAnalysis;
            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        private void ViewReportLocationWiseAnalysisCardType(int Type,int CustType,int locaID,long fromCust,long toCust)
        {
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            if (loyaltyCustomerService.SpLocationWiseLoyaltyAnalysisCardType(Type,CustType, locaID, Common.ConvertStringToInt(cmbCardType.SelectedValue.ToString()), dtpFromDate.Value, dtpToDate.Value,fromCust, toCust, 0) == true)
                {
                    ViewReport();
                }
       
            
        }
 
        private void ChkAutoComplteFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteFrom.Checked)
                {
                    LoadCustomersFrom();
                }
                else
                {
                    TxtSearchCodeFrom.AutoCompleteCustomSource = null;
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
                else
                {
                    TxtSearchCodeTo.AutoCompleteCustomSource = null;
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
                if (ChkAutoComplteFrom.Checked)
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
                if (ChkAutoComplteTo.Checked)
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

       


    }
}
