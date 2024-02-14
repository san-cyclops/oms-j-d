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

using Sarasa.ERP.Report;
using Sarasa.ERP.Report.Com;
using Sarasa.ERP.Report.GV;
using Sarasa.ERP.Report.Inventory.Transactions.Reports;
using Sarasa.ERP.Report.Logistic;
using Sarasa.ERP.Service;
using Sarasa.ERP.Domain;
using Sarasa.ERP.UI.Windows;
using Sarasa.ERP.UI.Windows.Reports;
using Sarasa.ERP.Utility;
using System.Collections;
using Sarasa.ERP.Report.Inventory;
using System.Reflection;
using Sarasa.ERP.Report.Inventory.Reference.Reports;

namespace Sarasa.ERP.UI.Windows
{
    public partial class FrmBinCard: Sarasa.ERP.UI.Windows.FrmBaseMasterForm
    {
        int documentID = 0;
        bool isValidControls = true;
        UserPrivileges accessRights = new UserPrivileges();
        private GroupBox groupBox1;
        private Label label1;
        private DateTimePicker dtpToDate;
        private ComboBox cmbLocation;
        private Label lblLocation;
        private CheckBox ChkAutoComplteTo;
        private CheckBox ChkAutoComplteFrom;
        private TextBox TxtSearchNameTo;
        private TextBox TxtSearchNameFrom;
        private TextBox TxtSearchCodeTo;
        private TextBox TxtSearchCodeFrom;
        private Label LblSerachTo;
        private Label LblSearchFrom;
        private DateTimePicker dtpFromDate;
        private Label lblDateRange;
        private RadioButton RdoBinCard;
        private RadioButton RdoMovement;
        private GroupBox groupBox2;
        private RadioButton RdoStockAging;
        private RadioButton RdoNon;
        private RadioButton RdoSlow;
        private RadioButton RdoFast;
        private RadioButton RdoStockSales;
        private RadioButton RdoGp;
        private RadioButton RdoQty;
        private RadioButton RdoValue;
        private Label label2;

        BinCardService binCardService = new BinCardService();


        public FrmBinCard()
        {
            InitializeComponent();
        }

        public override void FormLoad()
        {
            try
            {

                LocationService locationService = new LocationService();
                Common.LoadLocations(cmbLocation, locationService.GetAllLocations());

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

                cmbLocation.SelectedValue = Common.LoggedLocationID;
                

                LoadSearchCodes();

                base.FormLoad();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);

            }
        }

        private void LoadSearchCodes()
        {
            try
            {
               
                if (ChkAutoComplteFrom.Checked) { LoadProductsFrom(); }
                if (ChkAutoComplteTo.Checked) { LoadProductsTo(); }
               

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }


        private void LoadProductsFrom()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                Common.SetAutoComplete(TxtSearchCodeFrom, invProductMasterService.GetAllProductCodes(), ChkAutoComplteFrom.Checked);
                Common.SetAutoComplete(TxtSearchNameFrom, invProductMasterService.GetAllProductNames(), ChkAutoComplteFrom.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void LoadProductsTo()
        {
            try
            {
                InvProductMasterService invProductMasterService = new InvProductMasterService();

                Common.SetAutoComplete(TxtSearchCodeTo, invProductMasterService.GetAllProductCodes(), ChkAutoComplteTo.Checked);
                Common.SetAutoComplete(TxtSearchNameTo, invProductMasterService.GetAllProductNames(), ChkAutoComplteTo.Checked);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }
        

        public override void ClearForm()
        {
            try
            {
                base.ClearForm();

                Common.ClearComboBox(cmbLocation);
                cmbLocation.Focus();

                cmbLocation.SelectedValue = Common.LoggedLocationID;

                TxtSearchCodeFrom.Text = string.Empty;
                TxtSearchCodeTo.Text = string.Empty;
                TxtSearchNameFrom.Text = string.Empty;
                TxtSearchNameTo.Text = string.Empty;

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateFrom;
                DateTime dateTo;

                int locationId = 0;
                int uniqueId = 0;
                int typeId = 0;
                int stockId = 0;


                dateFrom = dtpFromDate.Value;
                dateTo= dtpToDate.Value;

               
                if (ValidateLocationComboBoxes().Equals(false)) { return; }
                

                if (ValidateControls() == false) return;

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

                locationId = cmbLocation.SelectedIndex + 1;

                if (RdoBinCard.Checked == true) { typeId = 1; }
                else if (RdoMovement.Checked == true) { typeId = 2; }
                else if (RdoStockAging.Checked == true) { typeId = 3; }
                else if (RdoFast.Checked == true) { typeId = 4; }
                else if (RdoSlow.Checked == true) { typeId = 5; }
                else if (RdoNon.Checked == true) { typeId = 6; }

                if (RdoStockSales.Checked == true)
                {
                    stockId = 1;
                }
                else
                {
                    stockId = 0;
                }

                this.Cursor = Cursors.WaitCursor;

                if (RdoStockAging.Checked == true)
                {
                    if (binCardService.ViewAging(typeId, locationId, dateFrom, dateTo, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()) == true)
                    {
                        ViewReport(locationId);
                    }
                }
                else if (RdoFast.Checked == true)
                {
                    if (binCardService.ViewMovement(typeId, locationId, dateFrom, dateTo, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim(), stockId) == true)
                    {
                        ViewReport(locationId);
                    }
                }
                else if (RdoSlow.Checked == true)
                {
                    if (binCardService.ViewMovement(typeId, locationId, dateFrom, dateTo, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim(), stockId) == true)
                    {
                        ViewReport(locationId);
                    }
                }
                else if (RdoNon.Checked == true)
                {
                    if (binCardService.ViewMovement(typeId, locationId, dateFrom, dateTo, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim(), stockId) == true)
                    {
                        ViewReport(locationId);
                    }
                }
                else
                {
                    if (binCardService.View(typeId, locationId, dateFrom, dateTo, uniqueId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()) == true)
                    {
                        ViewReport(locationId);
                    }
                }
               
            }

            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
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

        private void ViewReport(int locationId)
        {

            FrmReportViewer objReportView = new FrmReportViewer();
            Cursor.Current = Cursors.WaitCursor;
           

            if (RdoBinCard.Checked == true)
            {
                InvRptBinCard invRptBinCard = new InvRptBinCard();

                invRptBinCard.SetDataSource(binCardService.GetBinCardDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));


                invRptBinCard.SetDataSource(binCardService.DsGetBinCardDetails.Tables["BinCardDetails"]);

                invRptBinCard.SummaryInfo.ReportTitle = "Bin Card Report";
                invRptBinCard.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptBinCard.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptBinCard.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptBinCard.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptBinCard.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptBinCard.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptBinCard.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptBinCard.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptBinCard.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptBinCard.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptBinCard.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptBinCard;

            }

            else if (RdoMovement.Checked == true)
            {
                InvRptStockMovement invRptStockMovement = new InvRptStockMovement();

                invRptStockMovement.SetDataSource(binCardService.GetBinCardDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));


                invRptStockMovement.SetDataSource(binCardService.DsGetBinCardDetails.Tables["BinCardDetails"]);

                invRptStockMovement.SummaryInfo.ReportTitle = "Stock Movement Report";
                invRptStockMovement.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptStockMovement.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptStockMovement.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptStockMovement.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptStockMovement.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptStockMovement.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptStockMovement.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptStockMovement.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptStockMovement.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptStockMovement.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptStockMovement.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptStockMovement;
            }

            else if (RdoStockAging.Checked == true)
            {
                InvRptStockAging invRptStockAging = new InvRptStockAging();

                invRptStockAging.SetDataSource(binCardService.GetBinCardDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));


                invRptStockAging.SetDataSource(binCardService.DsGetBinCardDetails.Tables["BinCardDetails"]);

                invRptStockAging.SummaryInfo.ReportTitle = "Stock Movement Report";
                invRptStockAging.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptStockAging.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptStockAging.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptStockAging.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptStockAging.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptStockAging.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptStockAging.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptStockAging.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptStockAging.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptStockAging.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptStockAging.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptStockAging;
            }

            else if (RdoFast.Checked == true)
            {
                InvRptFastMoving invRptFastMoving = new InvRptFastMoving();

                if (RdoQty.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetFastMovingDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoValue.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetFastMovingValueDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoGp.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetFastMovingGpDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoStockSales.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetFastMovingStockVsSalesDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }

                invRptFastMoving.SetDataSource(binCardService.DsGetBinCardDetails.Tables["FastMovingDetails"]);

                invRptFastMoving.SummaryInfo.ReportTitle = "Fast Moving Report";
                invRptFastMoving.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptFastMoving.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptFastMoving.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptFastMoving.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptFastMoving.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptFastMoving.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptFastMoving.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptFastMoving.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptFastMoving;
            }

            else if (RdoSlow.Checked == true)
            {
                InvRptFastMoving invRptFastMoving = new InvRptFastMoving();
                if (RdoQty.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetSlowMovingDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoValue.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetSlowMovingValueDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoGp.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetSlowMovingGpDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }
                else if (RdoStockSales.Checked == true)
                {
                    invRptFastMoving.SetDataSource(binCardService.GetSlowMovingStockVsSalesDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));
                }

                invRptFastMoving.SetDataSource(binCardService.DsGetBinCardDetails.Tables["FastMovingDetails"]);

                invRptFastMoving.SummaryInfo.ReportTitle = "Slow Moving Report";
                invRptFastMoving.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptFastMoving.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptFastMoving.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptFastMoving.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptFastMoving.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptFastMoving.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptFastMoving.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptFastMoving.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptFastMoving.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptFastMoving;
            }

            else if (RdoNon.Checked == true)
            {
                InvRptNonMoving invRptNonMoving = new InvRptNonMoving();

                invRptNonMoving.SetDataSource(binCardService.GetNonMovingDetails(locationId, TxtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim()));


                invRptNonMoving.SetDataSource(binCardService.DsGetBinCardDetails.Tables["FastMovingDetails"]);

                invRptNonMoving.SummaryInfo.ReportTitle = "Non Moving Report";
                invRptNonMoving.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
                invRptNonMoving.DataDefinition.FormulaFields["LocationName"].Text = "'" + cmbLocation.Text.Trim() + "'";

                invRptNonMoving.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
                invRptNonMoving.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
                invRptNonMoving.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
                invRptNonMoving.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
                invRptNonMoving.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";

                invRptNonMoving.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
                invRptNonMoving.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
                invRptNonMoving.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
                invRptNonMoving.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

                objReportView.crRptViewer.ReportSource = invRptNonMoving;
            }


            objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            Cursor.Current = Cursors.Default;
        }

        

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RdoStockAging = new System.Windows.Forms.RadioButton();
            this.RdoBinCard = new System.Windows.Forms.RadioButton();
            this.RdoMovement = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.ChkAutoComplteTo = new System.Windows.Forms.CheckBox();
            this.ChkAutoComplteFrom = new System.Windows.Forms.CheckBox();
            this.TxtSearchNameTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameFrom = new System.Windows.Forms.TextBox();
            this.TxtSearchCodeTo = new System.Windows.Forms.TextBox();
            this.TxtSearchCodeFrom = new System.Windows.Forms.TextBox();
            this.LblSerachTo = new System.Windows.Forms.Label();
            this.LblSearchFrom = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RdoFast = new System.Windows.Forms.RadioButton();
            this.RdoSlow = new System.Windows.Forms.RadioButton();
            this.RdoNon = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.RdoStockSales = new System.Windows.Forms.RadioButton();
            this.RdoGp = new System.Windows.Forms.RadioButton();
            this.RdoQty = new System.Windows.Forms.RadioButton();
            this.RdoValue = new System.Windows.Forms.RadioButton();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 255);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(438, 255);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RdoNon);
            this.groupBox1.Controls.Add(this.RdoSlow);
            this.groupBox1.Controls.Add(this.RdoFast);
            this.groupBox1.Controls.Add(this.RdoStockAging);
            this.groupBox1.Controls.Add(this.RdoBinCard);
            this.groupBox1.Controls.Add(this.RdoMovement);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.ChkAutoComplteTo);
            this.groupBox1.Controls.Add(this.ChkAutoComplteFrom);
            this.groupBox1.Controls.Add(this.TxtSearchNameTo);
            this.groupBox1.Controls.Add(this.TxtSearchNameFrom);
            this.groupBox1.Controls.Add(this.TxtSearchCodeTo);
            this.groupBox1.Controls.Add(this.TxtSearchCodeFrom);
            this.groupBox1.Controls.Add(this.LblSerachTo);
            this.groupBox1.Controls.Add(this.LblSearchFrom);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 171);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // RdoStockAging
            // 
            this.RdoStockAging.AutoSize = true;
            this.RdoStockAging.Location = new System.Drawing.Point(251, 20);
            this.RdoStockAging.Name = "RdoStockAging";
            this.RdoStockAging.Size = new System.Drawing.Size(93, 17);
            this.RdoStockAging.TabIndex = 117;
            this.RdoStockAging.Tag = "1";
            this.RdoStockAging.Text = "Stock Aging";
            this.RdoStockAging.UseVisualStyleBackColor = true;
            // 
            // RdoBinCard
            // 
            this.RdoBinCard.AutoSize = true;
            this.RdoBinCard.Checked = true;
            this.RdoBinCard.Location = new System.Drawing.Point(23, 20);
            this.RdoBinCard.Name = "RdoBinCard";
            this.RdoBinCard.Size = new System.Drawing.Size(75, 17);
            this.RdoBinCard.TabIndex = 116;
            this.RdoBinCard.TabStop = true;
            this.RdoBinCard.Tag = "1";
            this.RdoBinCard.Text = "Bin Card";
            this.RdoBinCard.UseVisualStyleBackColor = true;
            // 
            // RdoMovement
            // 
            this.RdoMovement.AutoSize = true;
            this.RdoMovement.Location = new System.Drawing.Point(113, 20);
            this.RdoMovement.Name = "RdoMovement";
            this.RdoMovement.Size = new System.Drawing.Size(120, 17);
            this.RdoMovement.TabIndex = 115;
            this.RdoMovement.Tag = "1";
            this.RdoMovement.Text = "Stock Movement";
            this.RdoMovement.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "-";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(287, 52);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 21);
            this.dtpToDate.TabIndex = 113;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(133, 79);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(287, 21);
            this.cmbLocation.TabIndex = 110;
            this.cmbLocation.Click += new System.EventHandler(this.cmbLocation_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(20, 82);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 111;
            this.lblLocation.Text = "Location";
            // 
            // ChkAutoComplteTo
            // 
            this.ChkAutoComplteTo.AutoSize = true;
            this.ChkAutoComplteTo.Location = new System.Drawing.Point(112, 140);
            this.ChkAutoComplteTo.Name = "ChkAutoComplteTo";
            this.ChkAutoComplteTo.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteTo.TabIndex = 106;
            this.ChkAutoComplteTo.Tag = "1";
            this.ChkAutoComplteTo.UseVisualStyleBackColor = true;
            this.ChkAutoComplteTo.CheckedChanged += new System.EventHandler(this.ChkAutoComplteTo_CheckedChanged);
            // 
            // ChkAutoComplteFrom
            // 
            this.ChkAutoComplteFrom.AutoSize = true;
            this.ChkAutoComplteFrom.Location = new System.Drawing.Point(112, 113);
            this.ChkAutoComplteFrom.Name = "ChkAutoComplteFrom";
            this.ChkAutoComplteFrom.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteFrom.TabIndex = 105;
            this.ChkAutoComplteFrom.Tag = "1";
            this.ChkAutoComplteFrom.UseVisualStyleBackColor = true;
            this.ChkAutoComplteFrom.CheckedChanged += new System.EventHandler(this.ChkAutoComplteFrom_CheckedChanged);
            // 
            // TxtSearchNameTo
            // 
            this.TxtSearchNameTo.Location = new System.Drawing.Point(269, 137);
            this.TxtSearchNameTo.Name = "TxtSearchNameTo";
            this.TxtSearchNameTo.Size = new System.Drawing.Size(398, 21);
            this.TxtSearchNameTo.TabIndex = 104;
            // 
            // TxtSearchNameFrom
            // 
            this.TxtSearchNameFrom.Location = new System.Drawing.Point(269, 110);
            this.TxtSearchNameFrom.Name = "TxtSearchNameFrom";
            this.TxtSearchNameFrom.Size = new System.Drawing.Size(398, 21);
            this.TxtSearchNameFrom.TabIndex = 103;
            // 
            // TxtSearchCodeTo
            // 
            this.TxtSearchCodeTo.Location = new System.Drawing.Point(133, 137);
            this.TxtSearchCodeTo.Name = "TxtSearchCodeTo";
            this.TxtSearchCodeTo.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeTo.TabIndex = 102;
            this.TxtSearchCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeTo_KeyDown);
            this.TxtSearchCodeTo.Leave += new System.EventHandler(this.TxtSearchCodeTo_Leave);
            // 
            // TxtSearchCodeFrom
            // 
            this.TxtSearchCodeFrom.Location = new System.Drawing.Point(133, 110);
            this.TxtSearchCodeFrom.Name = "TxtSearchCodeFrom";
            this.TxtSearchCodeFrom.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeFrom.TabIndex = 101;
            this.TxtSearchCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeFrom_KeyDown);
            this.TxtSearchCodeFrom.Leave += new System.EventHandler(this.TxtSearchCodeFrom_Leave);
            // 
            // LblSerachTo
            // 
            this.LblSerachTo.AutoSize = true;
            this.LblSerachTo.Location = new System.Drawing.Point(21, 140);
            this.LblSerachTo.Name = "LblSerachTo";
            this.LblSerachTo.Size = new System.Drawing.Size(67, 13);
            this.LblSerachTo.TabIndex = 98;
            this.LblSerachTo.Text = "Product To";
            // 
            // LblSearchFrom
            // 
            this.LblSearchFrom.AutoSize = true;
            this.LblSearchFrom.Location = new System.Drawing.Point(21, 113);
            this.LblSearchFrom.Name = "LblSearchFrom";
            this.LblSearchFrom.Size = new System.Drawing.Size(83, 13);
            this.LblSearchFrom.TabIndex = 97;
            this.LblSearchFrom.Text = "Product From";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(133, 52);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 96;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(20, 58);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(74, 13);
            this.lblDateRange.TabIndex = 95;
            this.lblDateRange.Text = "Date Range";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RdoStockSales);
            this.groupBox2.Controls.Add(this.RdoGp);
            this.groupBox2.Controls.Add(this.RdoQty);
            this.groupBox2.Controls.Add(this.RdoValue);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(2, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // RdoFast
            // 
            this.RdoFast.AutoSize = true;
            this.RdoFast.Location = new System.Drawing.Point(362, 20);
            this.RdoFast.Name = "RdoFast";
            this.RdoFast.Size = new System.Drawing.Size(91, 17);
            this.RdoFast.TabIndex = 118;
            this.RdoFast.Tag = "1";
            this.RdoFast.Text = "Fast Moving";
            this.RdoFast.UseVisualStyleBackColor = true;
            // 
            // RdoSlow
            // 
            this.RdoSlow.AutoSize = true;
            this.RdoSlow.Location = new System.Drawing.Point(468, 20);
            this.RdoSlow.Name = "RdoSlow";
            this.RdoSlow.Size = new System.Drawing.Size(96, 17);
            this.RdoSlow.TabIndex = 119;
            this.RdoSlow.Tag = "1";
            this.RdoSlow.Text = "Slow Moving";
            this.RdoSlow.UseVisualStyleBackColor = true;
            // 
            // RdoNon
            // 
            this.RdoNon.AutoSize = true;
            this.RdoNon.Location = new System.Drawing.Point(580, 20);
            this.RdoNon.Name = "RdoNon";
            this.RdoNon.Size = new System.Drawing.Size(91, 17);
            this.RdoNon.TabIndex = 120;
            this.RdoNon.Tag = "1";
            this.RdoNon.Text = "Non Moving";
            this.RdoNon.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Based On";
            // 
            // RdoStockSales
            // 
            this.RdoStockSales.AutoSize = true;
            this.RdoStockSales.Location = new System.Drawing.Point(384, 23);
            this.RdoStockSales.Name = "RdoStockSales";
            this.RdoStockSales.Size = new System.Drawing.Size(110, 17);
            this.RdoStockSales.TabIndex = 125;
            this.RdoStockSales.Tag = "1";
            this.RdoStockSales.Text = "Stock Vs Sales";
            this.RdoStockSales.UseVisualStyleBackColor = true;
            // 
            // RdoGp
            // 
            this.RdoGp.AutoSize = true;
            this.RdoGp.Location = new System.Drawing.Point(269, 23);
            this.RdoGp.Name = "RdoGp";
            this.RdoGp.Size = new System.Drawing.Size(92, 17);
            this.RdoGp.TabIndex = 124;
            this.RdoGp.Tag = "1";
            this.RdoGp.Text = "Gross Profit";
            this.RdoGp.UseVisualStyleBackColor = true;
            // 
            // RdoQty
            // 
            this.RdoQty.AutoSize = true;
            this.RdoQty.Checked = true;
            this.RdoQty.Location = new System.Drawing.Point(122, 23);
            this.RdoQty.Name = "RdoQty";
            this.RdoQty.Size = new System.Drawing.Size(45, 17);
            this.RdoQty.TabIndex = 123;
            this.RdoQty.TabStop = true;
            this.RdoQty.Tag = "1";
            this.RdoQty.Text = "Qty";
            this.RdoQty.UseVisualStyleBackColor = true;
            // 
            // RdoValue
            // 
            this.RdoValue.AutoSize = true;
            this.RdoValue.Location = new System.Drawing.Point(191, 23);
            this.RdoValue.Name = "RdoValue";
            this.RdoValue.Size = new System.Drawing.Size(56, 17);
            this.RdoValue.TabIndex = 122;
            this.RdoValue.Tag = "1";
            this.RdoValue.Text = "Value";
            this.RdoValue.UseVisualStyleBackColor = true;
            // 
            // FrmBinCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(679, 304);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmBinCard";
            this.Load += new System.EventHandler(this.FrmBinCard_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void cmbLocation_Click(object sender, EventArgs e)
        {

        }

        private void TxtSearchCodeFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(TxtSearchCodeFrom.Text.Trim())) { TxtSearchNameFrom.Focus(); }
                    TxtSearchCodeTo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeFrom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeFrom.Text.Trim())) { return; }

               
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByCode(TxtSearchCodeFrom.Text.Trim());

                if (invProductMaster != null)
                {
                    TxtSearchCodeFrom.Text = invProductMaster.ProductCode;
                    TxtSearchNameFrom.Text = invProductMaster.ProductName;
                }

                           
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(TxtSearchCodeTo.Text.Trim())) { TxtSearchNameTo.Focus(); }
                    TxtSearchNameTo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void TxtSearchCodeTo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtSearchCodeTo.Text.Trim())) { return; }
                
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductMaster invProductMaster = new InvProductMaster();

                invProductMaster = invProductMasterService.GetProductsByCode(TxtSearchCodeTo.Text.Trim());

                if (invProductMaster != null)
                {
                    TxtSearchCodeTo.Text = invProductMaster.ProductCode;
                    TxtSearchNameTo.Text = invProductMaster.ProductName;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ChkAutoComplteFrom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteFrom.Checked)
                {
                    LoadProductsFrom();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void ChkAutoComplteTo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkAutoComplteTo.Checked)
                {
                    LoadProductsTo();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message.ToString(), MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }

        private void FrmBinCard_Load(object sender, EventArgs e)
        {

        }        
        
    }
}
