namespace Report.GUI
{
    partial class FrmCrmLocationAnalysis
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
            this.chkAlllocations = new System.Windows.Forms.CheckBox();
            this.chkAllCostomers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.TxtSearchCodeFrom = new System.Windows.Forms.TextBox();
            this.LblSearchFrom = new System.Windows.Forms.Label();
            this.ChkAutoComplteFrom = new System.Windows.Forms.CheckBox();
            this.LblSerachTo = new System.Windows.Forms.Label();
            this.ChkAutoComplteTo = new System.Windows.Forms.CheckBox();
            this.TxtSearchCodeTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameFrom = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(370, 195);
            this.grpButtonSet2.Size = new System.Drawing.Size(236, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 195);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCardType);
            this.groupBox1.Controls.Add(this.chkAlllocations);
            this.groupBox1.Controls.Add(this.chkAllCostomers);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Controls.Add(this.pnlSelection);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 205);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 139;
            this.label3.Text = "Card Type";
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(126, 155);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(231, 21);
            this.cmbCardType.TabIndex = 138;
            // 
            // chkAlllocations
            // 
            this.chkAlllocations.AutoSize = true;
            this.chkAlllocations.Location = new System.Drawing.Point(419, 50);
            this.chkAlllocations.Name = "chkAlllocations";
            this.chkAlllocations.Size = new System.Drawing.Size(97, 17);
            this.chkAlllocations.TabIndex = 135;
            this.chkAlllocations.Text = "All Locations";
            this.chkAlllocations.UseVisualStyleBackColor = true;
            this.chkAlllocations.CheckedChanged += new System.EventHandler(this.chkAlllocations_CheckedChanged);
            // 
            // chkAllCostomers
            // 
            this.chkAllCostomers.AutoSize = true;
            this.chkAllCostomers.Location = new System.Drawing.Point(105, 135);
            this.chkAllCostomers.Name = "chkAllCostomers";
            this.chkAllCostomers.Size = new System.Drawing.Size(106, 17);
            this.chkAllCostomers.TabIndex = 129;
            this.chkAllCostomers.Text = "All Customers";
            this.chkAllCostomers.UseVisualStyleBackColor = true;
            this.chkAllCostomers.CheckedChanged += new System.EventHandler(this.chkAllCostomers_CheckedChanged);
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
            this.dtpToDate.Size = new System.Drawing.Size(133, 21);
            this.dtpToDate.TabIndex = 127;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(126, 47);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(287, 21);
            this.cmbLocation.TabIndex = 125;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(8, 50);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 126;
            this.lblLocation.Text = "Location";
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
            // pnlSelection
            // 
            this.pnlSelection.Controls.Add(this.TxtSearchCodeFrom);
            this.pnlSelection.Controls.Add(this.LblSearchFrom);
            this.pnlSelection.Controls.Add(this.ChkAutoComplteFrom);
            this.pnlSelection.Controls.Add(this.LblSerachTo);
            this.pnlSelection.Controls.Add(this.ChkAutoComplteTo);
            this.pnlSelection.Controls.Add(this.TxtSearchCodeTo);
            this.pnlSelection.Controls.Add(this.TxtSearchNameTo);
            this.pnlSelection.Controls.Add(this.TxtSearchNameFrom);
            this.pnlSelection.Location = new System.Drawing.Point(6, 65);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(590, 64);
            this.pnlSelection.TabIndex = 131;
            // 
            // TxtSearchCodeFrom
            // 
            this.TxtSearchCodeFrom.Location = new System.Drawing.Point(120, 8);
            this.TxtSearchCodeFrom.Name = "TxtSearchCodeFrom";
            this.TxtSearchCodeFrom.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeFrom.TabIndex = 119;
            this.TxtSearchCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeFrom_KeyDown);
            this.TxtSearchCodeFrom.Leave += new System.EventHandler(this.TxtSearchCodeFrom_Leave);
            // 
            // LblSearchFrom
            // 
            this.LblSearchFrom.AutoSize = true;
            this.LblSearchFrom.Location = new System.Drawing.Point(3, 11);
            this.LblSearchFrom.Name = "LblSearchFrom";
            this.LblSearchFrom.Size = new System.Drawing.Size(96, 13);
            this.LblSearchFrom.TabIndex = 117;
            this.LblSearchFrom.Text = "Customer From";
            // 
            // ChkAutoComplteFrom
            // 
            this.ChkAutoComplteFrom.AutoSize = true;
            this.ChkAutoComplteFrom.Location = new System.Drawing.Point(99, 11);
            this.ChkAutoComplteFrom.Name = "ChkAutoComplteFrom";
            this.ChkAutoComplteFrom.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteFrom.TabIndex = 123;
            this.ChkAutoComplteFrom.Tag = "1";
            this.ChkAutoComplteFrom.UseVisualStyleBackColor = true;
            this.ChkAutoComplteFrom.CheckedChanged += new System.EventHandler(this.ChkAutoComplteFrom_CheckedChanged);
            // 
            // LblSerachTo
            // 
            this.LblSerachTo.AutoSize = true;
            this.LblSerachTo.Location = new System.Drawing.Point(3, 38);
            this.LblSerachTo.Name = "LblSerachTo";
            this.LblSerachTo.Size = new System.Drawing.Size(80, 13);
            this.LblSerachTo.TabIndex = 118;
            this.LblSerachTo.Text = "Customer To";
            // 
            // ChkAutoComplteTo
            // 
            this.ChkAutoComplteTo.AutoSize = true;
            this.ChkAutoComplteTo.Location = new System.Drawing.Point(99, 38);
            this.ChkAutoComplteTo.Name = "ChkAutoComplteTo";
            this.ChkAutoComplteTo.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteTo.TabIndex = 124;
            this.ChkAutoComplteTo.Tag = "1";
            this.ChkAutoComplteTo.UseVisualStyleBackColor = true;
            this.ChkAutoComplteTo.CheckedChanged += new System.EventHandler(this.ChkAutoComplteTo_CheckedChanged);
            // 
            // TxtSearchCodeTo
            // 
            this.TxtSearchCodeTo.Location = new System.Drawing.Point(120, 35);
            this.TxtSearchCodeTo.Name = "TxtSearchCodeTo";
            this.TxtSearchCodeTo.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeTo.TabIndex = 120;
            this.TxtSearchCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeTo_KeyDown);
            this.TxtSearchCodeTo.Leave += new System.EventHandler(this.TxtSearchCodeTo_Leave);
            // 
            // TxtSearchNameTo
            // 
            this.TxtSearchNameTo.Location = new System.Drawing.Point(258, 35);
            this.TxtSearchNameTo.Name = "TxtSearchNameTo";
            this.TxtSearchNameTo.Size = new System.Drawing.Size(329, 21);
            this.TxtSearchNameTo.TabIndex = 122;
            // 
            // TxtSearchNameFrom
            // 
            this.TxtSearchNameFrom.Location = new System.Drawing.Point(258, 8);
            this.TxtSearchNameFrom.Name = "TxtSearchNameFrom";
            this.TxtSearchNameFrom.Size = new System.Drawing.Size(329, 21);
            this.TxtSearchNameFrom.TabIndex = 121;
            // 
            // FrmCrmLocationAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 243);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCrmLocationAnalysis";
            this.Text = "LocationWiseAnalysis";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlSelection.ResumeLayout(false);
            this.pnlSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.CheckBox ChkAutoComplteTo;
        private System.Windows.Forms.CheckBox ChkAutoComplteFrom;
        private System.Windows.Forms.TextBox TxtSearchNameTo;
        private System.Windows.Forms.TextBox TxtSearchNameFrom;
        private System.Windows.Forms.TextBox TxtSearchCodeTo;
        private System.Windows.Forms.TextBox TxtSearchCodeFrom;
        private System.Windows.Forms.Label LblSerachTo;
        private System.Windows.Forms.Label LblSearchFrom;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.CheckBox chkAllCostomers;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.CheckBox chkAlllocations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCardType;
    }
}