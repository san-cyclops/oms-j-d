using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Windows;

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
using Utility;
using System.Collections;
using System.Reflection;


namespace Report.GUI
{
    public partial class FrmBasketAnalysisReport : FrmBaseReportsForm
    {
        int documentID = 0;
        bool isValidControls = true;
        UserPrivileges accessRights = new UserPrivileges();
        InvReportService invReportService = new InvReportService();

        public FrmBasketAnalysisReport()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {
                LocationService locationService = new LocationService();
                Common.LoadLocationsToCheckList(chkListLocation, locationService.GetAllInventoryLocations());

                foreach (int i in chkListLocation.CheckedIndices)
                {
                    chkListLocation.SetItemCheckState(i, CheckState.Unchecked);
                }

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                this.Text = autoGenerateInfo.FormText;

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();
                dgvRange.AutoGenerateColumns = false;
                dgvRange.AllowUserToAddRows = true;

                cmbCustomerType.SelectedIndex = 0;

                //LoadSearchCodes();

                base.FormLoad();

            }
            catch (Exception ex)
            {Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);}
        }
        
        public override void Clear()
        {
            foreach (int i in chkListLocation.CheckedIndices)
            {
                chkListLocation.SetItemCheckState(i, CheckState.Unchecked);
            }
            dgvRange.DataSource = null;
            dgvRange.Rows.Clear();
            dgvRange.Refresh();
            base.Clear();
            rdoValue.Checked = true;
        }

        public override void InitializeForm()
        {
            dgvRange.DataSource = invReportService.GetBasketAnalysisValueRange();
            base.InitializeForm();
        }

        private void rdoValue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoValue.Checked)
            {
                dgvRange.DataSource = invReportService.GetBasketAnalysisValueRange(1);
                dgvRange.Refresh();
            }
        }

        private void rdoQty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQty.Checked)
            {
                dgvRange.DataSource = invReportService.GetBasketAnalysisValueRange(2);
                dgvRange.Refresh();
            }
        }

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoItem.Checked)
            {
                dgvRange.DataSource = invReportService.GetBasketAnalysisValueRange(3);
                dgvRange.Refresh();
            }
        }

        public override void View()
        {
            try
            {
                FrmReportViewer objReportView = new FrmReportViewer();
                Cursor.Current = Cursors.WaitCursor;

                int rangeType=1;
                int customerType=1;
                string ReportName = string.Empty;
                string selectedLocation = string.Empty;

                bool isLocationFound = false;

                if(rdoValue.Checked)
                {
                    rangeType=1;
                    ReportName = "Value Wise";
                }
                else if(rdoQty.Checked)
                {
                    rangeType=2;
                    ReportName = "Qty Wise";
                }
                else
                {
                    rangeType=3;
                    ReportName = "No of Item Wise";
                }

                if(cmbCustomerType.Text.Trim()=="Loyalty Customers")
                {
                    customerType=3;
                }
                else if(cmbCustomerType.Text.Trim()=="Staff Purchases")
                {
                    customerType=2;
                }

                if (!invReportService.ClearBasketAnalysisTempFiles())
                {
                    Toast.Show("Tempory File Clearing Failed.\r\nPlease Try Again", Toast.messageType.Error, Toast.messageAction.General);
                    return;
                }

                #region SaveSelectedRange

                if (dgvRange.Rows.Count > 0)
                {
                    InvBasketAnalysisValueRangeTemp invBasketAnalysisValueRangeTemp;

                    foreach (DataGridViewRow row in dgvRange.Rows)
                    {
                        if (dgvRange["RangeFrom", row.Index].Value != null && !string.IsNullOrEmpty(dgvRange["RangeFrom", row.Index].Value.ToString().Trim()))
                        {

                            invBasketAnalysisValueRangeTemp = new InvBasketAnalysisValueRangeTemp();
                            invBasketAnalysisValueRangeTemp.RangeFrom = Common.ConvertStringToDecimalCurrency(dgvRange["RangeFrom", row.Index].Value.ToString());
                            invBasketAnalysisValueRangeTemp.RangeTo = Common.ConvertStringToDecimalCurrency(dgvRange["RangeTo", row.Index].Value.ToString());
                            invBasketAnalysisValueRangeTemp.RangeType = rangeType;
                            invBasketAnalysisValueRangeTemp.UserName = Common.LoggedUser;
                            invBasketAnalysisValueRangeTemp.PcName = Common.LoggedPcName;

                            invReportService.AddBasketAnalysisTempValueRange(invBasketAnalysisValueRangeTemp);
                        }
                    }
                }
                else
                {
                    Toast.Show("Please Enter Valid Value Range For Get This Report", Toast.messageType.Warning, Toast.messageAction.General);
                    return;
                }

                #endregion

                #region SaveSelectedLocation

              
                    InvBasketAnalysisSelectedLocationsTemp invBasketAnalysisSelectedLocationsTemp;
                    Location  location;

                    //for (int i = 0; i < chkListLocation.Items.Count; i++)
                    //{

                    //    if (chkListLocation.GetItemChecked(i))
                    //    {
                    //        location = new Domain.Location();

                    //        location = chkListLocation.Items[i].;

                    //        invBasketAnalysisSelectedLocationsTemp = new InvBasketAnalysisSelectedLocationsTemp();
                    //        invBasketAnalysisSelectedLocationsTemp.LocationID = location.LocationID;
                    //        invBasketAnalysisSelectedLocationsTemp.LocationDescription = location.LocationName;
                    //        invBasketAnalysisSelectedLocationsTemp.UserName = Common.LoggedUser;
                    //        invBasketAnalysisSelectedLocationsTemp.PcName = Common.LoggedPcName;

                    //        invReportService.AddBasketAnalysisTempLocation(invBasketAnalysisSelectedLocationsTemp);
                    //    }
                        
                    //}

                 foreach(Location l in chkListLocation.CheckedItems)
                    {

                        if (chkListLocation.CheckedItems!=null)
                        {
                            if (!isLocationFound)
                                isLocationFound = true;

                            invBasketAnalysisSelectedLocationsTemp = new InvBasketAnalysisSelectedLocationsTemp();
                            invBasketAnalysisSelectedLocationsTemp.LocationID = l.LocationID;
                            invBasketAnalysisSelectedLocationsTemp.LocationDescription = l.LocationName;
                            invBasketAnalysisSelectedLocationsTemp.UserName = Common.LoggedUser;
                            invBasketAnalysisSelectedLocationsTemp.PcName = Common.LoggedPcName;
                            if (selectedLocation != string.Empty)
                                selectedLocation += " / ";
                            selectedLocation +=  l.LocationName;

                            invReportService.AddBasketAnalysisTempLocation(invBasketAnalysisSelectedLocationsTemp);
                        }
                        
                    }
                

                if (!isLocationFound)
                {
                    Toast.Show("Please Select Valid Locations For Get This Report", Toast.messageType.Warning, Toast.messageAction.General);
                    return;
                }

                #endregion

                #region GetReportData

                DataTable dtReportData = new DataTable();
                dtReportData = invReportService.GetBasketAnalysisReport(chkGetLocationWise.Checked, chkExchange.Checked, chkGiftVoucher.Checked, chkDayReport.Checked, customerType, dtpFromDate.Value, dtpToDate.Value, chkSunday.Checked, chkMonday.Checked, chkTuesday.Checked, chkWednesday.Checked, chkThursday.Checked, chkFriday.Checked, chkSaturday.Checked,rangeType);
                if (dtReportData==null)
                {
                    Toast.Show("No Data To Preview\r\nPLEASE TRY AGAIN...", Toast.messageType.Warning, Toast.messageAction.General);
                    return;
                }
                #endregion

                Report.Inventory.Transactions.Reports.InvRptBasketAnalysis invRptBasketAnalysis = new Report.Inventory.Transactions.Reports.InvRptBasketAnalysis();
                invRptBasketAnalysis.SetDataSource(dtReportData);
                invRptBasketAnalysis.SummaryInfo.ReportTitle = "Basket Analysis Report - "+ReportName;
                invRptBasketAnalysis.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                //invRptBasketAnalysis.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptBasketAnalysis.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["DateFrom"].Text = "'" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["DateTo"].Text = "'" + dtpToDate.Value.ToString("dd/MM/yyyy") + "'";
                //invRptBasketAnalysis.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                //invRptBasketAnalysis.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptBasketAnalysis.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                invRptBasketAnalysis.DataDefinition.FormulaFields["Exchange"].Text = "'" + (chkExchange.Checked ? "Exchange Excluded" : "Exchange Included") + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["CustomerType"].Text = "'" + cmbCustomerType.Text.Trim() + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["GVSale"].Text = "'" + (chkGiftVoucher.Checked ? "Gift Voucher Sales Excluded" : "Gift Voucher Sales Included") + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["DayWise"].Text = "'" + (chkDayReport.Checked ? "1" : "0") + "'";
                invRptBasketAnalysis.DataDefinition.FormulaFields["LocationWise"].Text = "'" + (chkGetLocationWise.Checked ? "1" : "0") + "'";
                
                invRptBasketAnalysis.DataDefinition.FormulaFields["SelectedLocations"].Text = "'" + selectedLocation + "'";
                //invRptBasketAnalysis.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";


                objReportView.crRptViewer.ReportSource = invRptBasketAnalysis;

                //base.View();
                objReportView.WindowState = FormWindowState.Maximized;
                objReportView.Show();
                Cursor.Current = Cursors.Default;
                //Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmBasketAnalysisReport_Load(object sender, EventArgs e)
        {

        }

    }
}
