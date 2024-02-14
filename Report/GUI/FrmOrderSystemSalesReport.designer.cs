namespace Report.GUI
{
    partial class FrmOrderSystemSalesReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSummary = new System.Windows.Forms.CheckBox();
            this.rbnCustomerWise = new System.Windows.Forms.RadioButton();
            this.rbnSalesPersonWise = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSalesPerson = new System.Windows.Forms.Label();
            this.txtSalesPersonName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSalesPerson = new System.Windows.Forms.CheckBox();
            this.txtSalesPersonCode = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.lblVendor = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnPending = new System.Windows.Forms.RadioButton();
            this.rbnActive = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCompanyLocation = new System.Windows.Forms.ComboBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(408, 313);
            this.grpButtonSet2.Size = new System.Drawing.Size(239, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 313);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(82, 11);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(160, 11);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(4, 11);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSummary);
            this.groupBox1.Controls.Add(this.rbnCustomerWise);
            this.groupBox1.Controls.Add(this.rbnSalesPersonWise);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbCompanyLocation);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 314);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkSummary
            // 
            this.chkSummary.AutoSize = true;
            this.chkSummary.Location = new System.Drawing.Point(444, 63);
            this.chkSummary.Name = "chkSummary";
            this.chkSummary.Size = new System.Drawing.Size(124, 17);
            this.chkSummary.TabIndex = 158;
            this.chkSummary.Text = "Summary Report";
            this.chkSummary.UseVisualStyleBackColor = true;
            // 
            // rbnCustomerWise
            // 
            this.rbnCustomerWise.AutoSize = true;
            this.rbnCustomerWise.Location = new System.Drawing.Point(11, 112);
            this.rbnCustomerWise.Name = "rbnCustomerWise";
            this.rbnCustomerWise.Size = new System.Drawing.Size(112, 17);
            this.rbnCustomerWise.TabIndex = 157;
            this.rbnCustomerWise.Text = "Customer Wise";
            this.rbnCustomerWise.UseVisualStyleBackColor = true;
            // 
            // rbnSalesPersonWise
            // 
            this.rbnSalesPersonWise.AutoSize = true;
            this.rbnSalesPersonWise.Location = new System.Drawing.Point(11, 197);
            this.rbnSalesPersonWise.Name = "rbnSalesPersonWise";
            this.rbnSalesPersonWise.Size = new System.Drawing.Size(130, 17);
            this.rbnSalesPersonWise.TabIndex = 156;
            this.rbnSalesPersonWise.Text = "Sales Person Wise";
            this.rbnSalesPersonWise.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblSalesPerson);
            this.groupBox4.Controls.Add(this.txtSalesPersonName);
            this.groupBox4.Controls.Add(this.chkAutoCompleationSalesPerson);
            this.groupBox4.Controls.Add(this.txtSalesPersonCode);
            this.groupBox4.Location = new System.Drawing.Point(11, 220);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(617, 58);
            this.groupBox4.TabIndex = 155;
            this.groupBox4.TabStop = false;
            // 
            // lblSalesPerson
            // 
            this.lblSalesPerson.AutoSize = true;
            this.lblSalesPerson.Location = new System.Drawing.Point(21, 23);
            this.lblSalesPerson.Name = "lblSalesPerson";
            this.lblSalesPerson.Size = new System.Drawing.Size(81, 13);
            this.lblSalesPerson.TabIndex = 28;
            this.lblSalesPerson.Text = "Sales Person";
            // 
            // txtSalesPersonName
            // 
            this.txtSalesPersonName.Location = new System.Drawing.Point(270, 20);
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(338, 21);
            this.txtSalesPersonName.TabIndex = 26;
            this.txtSalesPersonName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonName_KeyDown);
            this.txtSalesPersonName.Leave += new System.EventHandler(this.txtSalesPersonName_Leave);
            // 
            // chkAutoCompleationSalesPerson
            // 
            this.chkAutoCompleationSalesPerson.AutoSize = true;
            this.chkAutoCompleationSalesPerson.Checked = true;
            this.chkAutoCompleationSalesPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(116, 23);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 27;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Location = new System.Drawing.Point(133, 20);
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(136, 21);
            this.txtSalesPersonCode.TabIndex = 25;
            this.txtSalesPersonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonCode_KeyDown);
            this.txtSalesPersonCode.Leave += new System.EventHandler(this.txtSalesPersonCode_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAutoCompleationCustomer);
            this.groupBox3.Controls.Add(this.lblVendor);
            this.groupBox3.Controls.Add(this.txtCustomerName);
            this.groupBox3.Controls.Add(this.txtCustomerCode);
            this.groupBox3.Location = new System.Drawing.Point(9, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(618, 43);
            this.groupBox3.TabIndex = 154;
            this.groupBox3.TabStop = false;
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(118, 16);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 23;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(23, 16);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(63, 13);
            this.lblVendor.TabIndex = 22;
            this.lblVendor.Text = "Customer";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(272, 13);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(338, 21);
            this.txtCustomerName.TabIndex = 19;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(135, 13);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 18;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(288, 62);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(40, 17);
            this.chkAll.TabIndex = 147;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbnPending);
            this.groupBox2.Controls.Add(this.rbnActive);
            this.groupBox2.Location = new System.Drawing.Point(573, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(15, 39);
            this.groupBox2.TabIndex = 145;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // rbnPending
            // 
            this.rbnPending.AutoSize = true;
            this.rbnPending.Checked = true;
            this.rbnPending.Location = new System.Drawing.Point(17, 13);
            this.rbnPending.Name = "rbnPending";
            this.rbnPending.Size = new System.Drawing.Size(70, 17);
            this.rbnPending.TabIndex = 1;
            this.rbnPending.TabStop = true;
            this.rbnPending.Text = "Pending";
            this.rbnPending.UseVisualStyleBackColor = true;
            // 
            // rbnActive
            // 
            this.rbnActive.AutoSize = true;
            this.rbnActive.Location = new System.Drawing.Point(168, 13);
            this.rbnActive.Name = "rbnActive";
            this.rbnActive.Size = new System.Drawing.Size(60, 17);
            this.rbnActive.TabIndex = 0;
            this.rbnActive.Text = "Active";
            this.rbnActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 144;
            this.label2.Text = "Company";
            // 
            // cmbCompanyLocation
            // 
            this.cmbCompanyLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyLocation.FormattingEnabled = true;
            this.cmbCompanyLocation.Items.AddRange(new object[] {
            "POLY-PACKAGING",
            "VENTURE"});
            this.cmbCompanyLocation.Location = new System.Drawing.Point(126, 60);
            this.cmbCompanyLocation.Name = "cmbCompanyLocation";
            this.cmbCompanyLocation.Size = new System.Drawing.Size(156, 21);
            this.cmbCompanyLocation.TabIndex = 143;
            this.cmbCompanyLocation.Tag = "3";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(3, 292);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(627, 10);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 142;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 128;
            this.label1.Text = "-";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(275, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(138, 21);
            this.dtpToDate.TabIndex = 127;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(126, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 116;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(8, 23);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(74, 13);
            this.lblDateRange.TabIndex = 115;
            this.lblDateRange.Text = "Date Range";
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // FrmOrderSystemSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 361);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOrderSystemSalesReport";
            this.Text = "Sales Reports";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCompanyLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnPending;
        private System.Windows.Forms.RadioButton rbnActive;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.RadioButton rbnCustomerWise;
        private System.Windows.Forms.RadioButton rbnSalesPersonWise;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.CheckBox chkSummary;
    }
}