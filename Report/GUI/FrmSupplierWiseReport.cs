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
using Report.Logistic;
using Service;
using Domain;
using UI.Windows;
using UI.Windows.Reports;
using Utility;
using System.Collections;
using Report.Inventory;
using System.Reflection;
using Report.GUI;
using Report.Inventory.Reference.Reports;

namespace UI.Windows
{
    public partial class FrmSupplierWiseReport: FrmBaseReportsForm
    {
        int documentID = 0;
        bool isValidControls = true;
        UserPrivileges accessRights = new UserPrivileges();
        private RadioButton RdoStockAging;
        private RadioButton RdoNon;
        private RadioButton RdoSlow;
        private RadioButton RdoFast;
        private Label lblDateRange;
        private DateTimePicker dtpFromDate;
        private Label lblLocation;
        private DateTimePicker dtpToDate;
        private Label label1;
        private GroupBox groupBox1;
        private CheckBox chkAllLocation;
        private ComboBox cmbCompanyLocation;


        List<Common.CheckedListBoxSelection> productExtendedPropertiesList = new List<Common.CheckedListBoxSelection>();

        public FrmSupplierWiseReport()
        {
            InitializeComponent();
        }

     

        public override void FormLoad()
        {
            try
            {

               

                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(this.Name);

                documentID = autoGenerateInfo.DocumentID;
                accessRights = CommonService.GetUserPrivilegesByUserIDandLocation(Common.LoggedUserId, Common.LoggedLocationID, documentID);

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

           

               
                base.FormLoad();

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
                base.ClearForm();

                

                dtpFromDate.Value = Common.GetSystemDate();
                dtpToDate.Value = Common.GetSystemDate();

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

                int locationId = 0;
                int uniqueId = 0;
                int typeId = 0;
                int stockId = 0;


                dateFrom = dtpFromDate.Value;
                dateTo= dtpToDate.Value;

              

                if (dateFrom > dateTo)
                {
                    Toast.Show("", Toast.messageType.Information, Toast.messageAction.InvalidDate);
                    return;
                }

           

                this.Cursor = Cursors.WaitCursor;

                SupplierService supplierService = new SupplierService();


                if (chkAllLocation.Checked)
                {
                   
                      ViewReport(locationId);
                      this.Cursor = Cursors.Default;
                   
                }
                else
                {
                }

            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), this.Name, Logger.logtype.ErrorLog, Common.LoggedLocationID);
            }
        }




        private void ViewReport(int locationId)
        {

            FrmReportViewer objReportView = new FrmReportViewer();
            SupplierService supplierService = new SupplierService();

            InvRptSupplierWisePerformanceAnalysis invsupWisePer = new InvRptSupplierWisePerformanceAnalysis();


           // invsupWisePer.SetDataSource(supplierService.GetSupplierDetails(locationId,xtSearchCodeFrom.Text.Trim(), TxtSearchCodeTo.Text.Trim(),txtSupplierCodeFrom.Text.Trim(),txtSupplierCodeTo.Text.Trim()));

           //// invsupWisePer.SetDataSource(binCardService.DsGetBinCardDetails.Tables["BinCardDetails"]);

           // invsupWisePer.SummaryInfo.ReportTitle = "Supplier Wise Performance Analysis Report";
           // invsupWisePer.SummaryInfo.ReportAuthor = Common.AuthorName + "  " + Common.AuthorAddress;
           // invsupWisePer.DataDefinition.FormulaFields["LocationName"].Text = "'" + "ALL LOCATION" + "'";

           // invsupWisePer.DataDefinition.FormulaFields["User"].Text = "'" + Common.LoggedUser + "'";
           // invsupWisePer.DataDefinition.FormulaFields["FromDate"].Text = "'" + dtpFromDate.Value + "'";
           // invsupWisePer.DataDefinition.FormulaFields["ToDate"].Text = "'" + dtpToDate.Value + "'";
           // invsupWisePer.DataDefinition.FormulaFields["CodeFrom"].Text = "'" + TxtSearchCodeFrom.Text.Trim() + "   " + TxtSearchNameFrom.Text.Trim() + "'";
           // invsupWisePer.DataDefinition.FormulaFields["CodeTo"].Text = "'" + TxtSearchCodeTo.Text.Trim() + "   " + TxtSearchNameTo.Text.Trim() + "'";
           // invsupWisePer.DataDefinition.FormulaFields["SupFrom"].Text = "'" + txtSupplierCodeFrom.Text.Trim() + "   " + txtSuppilerNameFrom.Text.Trim() + "'";
           // invsupWisePer.DataDefinition.FormulaFields["SupTo"].Text = "'" + txtSupplierCodeTo.Text.Trim() + "   " + txtSupplierNameTo.Text.Trim() + "'";

           // invsupWisePer.DataDefinition.FormulaFields["LogingUser"].Text = "'" + Common.LoggedUser.ToString() + "'";
           // invsupWisePer.DataDefinition.FormulaFields["LogingLocation"].Text = "'" + Common.LoggedLocationName + "'";
           // invsupWisePer.DataDefinition.FormulaFields["CompanyName"].Text = "'" + Common.LoggedCompanyName + "'";
           // invsupWisePer.DataDefinition.FormulaFields["Address"].Text = "'" + Common.LoggedCompanyAddress + "'";

           // objReportView.crRptViewer.ReportSource = invsupWisePer;
           // objReportView.WindowState = FormWindowState.Maximized;
            objReportView.Show();
            //Cursor.Current = Cursors.Default;
        }

        

        private void InitializeComponent()
        {
            this.RdoNon = new System.Windows.Forms.RadioButton();
            this.RdoSlow = new System.Windows.Forms.RadioButton();
            this.RdoFast = new System.Windows.Forms.RadioButton();
            this.RdoStockAging = new System.Windows.Forms.RadioButton();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblLocation = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAllLocation = new System.Windows.Forms.CheckBox();
            this.cmbCompanyLocation = new System.Windows.Forms.ComboBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(304, 109);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Controls.Add(this.RdoStockAging);
            this.grpButtonSet.Controls.Add(this.RdoFast);
            this.grpButtonSet.Controls.Add(this.RdoSlow);
            this.grpButtonSet.Controls.Add(this.RdoNon);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 109);
            this.grpButtonSet.Controls.SetChildIndex(this.RdoNon, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.RdoSlow, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.RdoFast, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.RdoStockAging, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnHelp, 0);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // RdoNon
            // 
            this.RdoNon.AutoSize = true;
            this.RdoNon.Location = new System.Drawing.Point(591, -2);
            this.RdoNon.Name = "RdoNon";
            this.RdoNon.Size = new System.Drawing.Size(91, 17);
            this.RdoNon.TabIndex = 120;
            this.RdoNon.Tag = "1";
            this.RdoNon.Text = "Non Moving";
            this.RdoNon.UseVisualStyleBackColor = true;
            this.RdoNon.Visible = false;
            // 
            // RdoSlow
            // 
            this.RdoSlow.AutoSize = true;
            this.RdoSlow.Location = new System.Drawing.Point(479, -2);
            this.RdoSlow.Name = "RdoSlow";
            this.RdoSlow.Size = new System.Drawing.Size(96, 17);
            this.RdoSlow.TabIndex = 119;
            this.RdoSlow.Tag = "1";
            this.RdoSlow.Text = "Slow Moving";
            this.RdoSlow.UseVisualStyleBackColor = true;
            this.RdoSlow.Visible = false;
            // 
            // RdoFast
            // 
            this.RdoFast.AutoSize = true;
            this.RdoFast.Location = new System.Drawing.Point(373, -2);
            this.RdoFast.Name = "RdoFast";
            this.RdoFast.Size = new System.Drawing.Size(91, 17);
            this.RdoFast.TabIndex = 118;
            this.RdoFast.Tag = "1";
            this.RdoFast.Text = "Fast Moving";
            this.RdoFast.UseVisualStyleBackColor = true;
            this.RdoFast.Visible = false;
            // 
            // RdoStockAging
            // 
            this.RdoStockAging.AutoSize = true;
            this.RdoStockAging.Location = new System.Drawing.Point(262, -2);
            this.RdoStockAging.Name = "RdoStockAging";
            this.RdoStockAging.Size = new System.Drawing.Size(93, 17);
            this.RdoStockAging.TabIndex = 117;
            this.RdoStockAging.Tag = "1";
            this.RdoStockAging.Text = "Stock Aging";
            this.RdoStockAging.UseVisualStyleBackColor = true;
            this.RdoStockAging.Visible = false;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(20, 23);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(74, 13);
            this.lblDateRange.TabIndex = 95;
            this.lblDateRange.Text = "Date Range";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(133, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 96;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(20, 51);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(62, 13);
            this.lblLocation.TabIndex = 111;
            this.lblLocation.Text = "Company";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(287, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 21);
            this.dtpToDate.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCompanyLocation);
            this.groupBox1.Controls.Add(this.chkAllLocation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 153);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // chkAllLocation
            // 
            this.chkAllLocation.AutoSize = true;
            this.chkAllLocation.Location = new System.Drawing.Point(324, 50);
            this.chkAllLocation.Name = "chkAllLocation";
            this.chkAllLocation.Size = new System.Drawing.Size(40, 17);
            this.chkAllLocation.TabIndex = 129;
            this.chkAllLocation.Tag = "1";
            this.chkAllLocation.Text = "All";
            this.chkAllLocation.UseVisualStyleBackColor = true;
            // 
            // cmbCompanyLocation
            // 
            this.cmbCompanyLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyLocation.FormattingEnabled = true;
            this.cmbCompanyLocation.Items.AddRange(new object[] {
            "POLY-PACKAGING",
            "VENTURE"});
            this.cmbCompanyLocation.Location = new System.Drawing.Point(133, 48);
            this.cmbCompanyLocation.Name = "cmbCompanyLocation";
            this.cmbCompanyLocation.Size = new System.Drawing.Size(156, 21);
            this.cmbCompanyLocation.TabIndex = 130;
            this.cmbCompanyLocation.Tag = "3";
            // 
            // FrmSupplierWiseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(544, 158);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSupplierWiseReport";
            this.Text = "Sales Report";
            this.Load += new System.EventHandler(this.FrmBinCard_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void cmbLocation_Click(object sender, EventArgs e)
        {

        }

 

 
 

 

        private void FrmBinCard_Load(object sender, EventArgs e)
        {

        }

 
 
         


    }
}
