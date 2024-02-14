namespace Report.GUI
{
    partial class FrmCrmTopCustomerPerformance
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
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.lblTop = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(269, 109);
            this.grpButtonSet2.Size = new System.Drawing.Size(236, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 109);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCardType);
            this.groupBox1.Controls.Add(this.txtSelect);
            this.groupBox1.Controls.Add(this.lblTop);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 117);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 143;
            this.label3.Text = "Card Type";
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(93, 44);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(282, 21);
            this.cmbCardType.TabIndex = 142;
            // 
            // txtSelect
            // 
            this.txtSelect.Location = new System.Drawing.Point(93, 71);
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(45, 21);
            this.txtSelect.TabIndex = 146;
            this.txtSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSelect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSelect_KeyPress);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(10, 74);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(31, 13);
            this.lblTop.TabIndex = 145;
            this.lblTop.Text = "Top ";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(11, 101);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(481, 10);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 144;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 128;
            this.label1.Text = "-";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(242, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 21);
            this.dtpToDate.TabIndex = 127;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(93, 17);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 147;
            this.label2.Text = "(Customers)";
            // 
            // FrmCrmTopCustomerPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 157);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCrmTopCustomerPerformance";
            this.Text = "FrmCrmTopCustomerPerformance";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label label2;
    }
}