namespace UI.Windows
{
    partial class FrmPosSalesSummery
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
            this.ChkLocationWise = new System.Windows.Forms.CheckBox();
            this.ChkAllLocations = new System.Windows.Forms.CheckBox();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.cmbTerminal = new System.Windows.Forms.ComboBox();
            this.ChkAllTerminals = new System.Windows.Forms.CheckBox();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 144);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(388, 144);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkLocationWise);
            this.groupBox1.Controls.Add(this.ChkAllLocations);
            this.groupBox1.Controls.Add(this.lblToDate);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Controls.Add(this.cmbTerminal);
            this.groupBox1.Controls.Add(this.ChkAllTerminals);
            this.groupBox1.Controls.Add(this.lblTerminal);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 155);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // ChkLocationWise
            // 
            this.ChkLocationWise.AutoSize = true;
            this.ChkLocationWise.Location = new System.Drawing.Point(307, 128);
            this.ChkLocationWise.Name = "ChkLocationWise";
            this.ChkLocationWise.Size = new System.Drawing.Size(314, 17);
            this.ChkLocationWise.TabIndex = 71;
            this.ChkLocationWise.Text = "All Location, All Terminals Location Wise Summary";
            this.ChkLocationWise.UseVisualStyleBackColor = true;
            this.ChkLocationWise.CheckedChanged += new System.EventHandler(this.ChkLocationWise_CheckedChanged);
            // 
            // ChkAllLocations
            // 
            this.ChkAllLocations.AutoSize = true;
            this.ChkAllLocations.Location = new System.Drawing.Point(444, 20);
            this.ChkAllLocations.Name = "ChkAllLocations";
            this.ChkAllLocations.Size = new System.Drawing.Size(97, 17);
            this.ChkAllLocations.TabIndex = 70;
            this.ChkAllLocations.Text = "All Locations";
            this.ChkAllLocations.UseVisualStyleBackColor = true;
            this.ChkAllLocations.CheckedChanged += new System.EventHandler(this.ChkAllLocations_CheckedChanged);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(278, 77);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(20, 13);
            this.lblToDate.TabIndex = 69;
            this.lblToDate.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(315, 73);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(117, 21);
            this.dtpToDate.TabIndex = 68;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(145, 73);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(117, 21);
            this.dtpFromDate.TabIndex = 67;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(22, 77);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(107, 13);
            this.lblDateRange.TabIndex = 66;
            this.lblDateRange.Text = "Date Range From";
            // 
            // cmbTerminal
            // 
            this.cmbTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTerminal.FormattingEnabled = true;
            this.cmbTerminal.Items.AddRange(new object[] {
            "Terminal 01",
            "Terminal 02",
            "Terminal 03",
            "Terminal 04",
            "Terminal 05",
            "Terminal 06",
            "Terminal 07",
            "Terminal 08",
            "Terminal 09",
            "Terminal 10",
            "Terminal 11",
            "Terminal 12",
            "Terminal 13",
            "Terminal 14",
            "Terminal 15",
            "Terminal 16",
            "Terminal 17",
            "Terminal 18",
            "Terminal 19",
            "Terminal 20"});
            this.cmbTerminal.Location = new System.Drawing.Point(145, 46);
            this.cmbTerminal.Name = "cmbTerminal";
            this.cmbTerminal.Size = new System.Drawing.Size(287, 21);
            this.cmbTerminal.TabIndex = 65;
            // 
            // ChkAllTerminals
            // 
            this.ChkAllTerminals.AutoSize = true;
            this.ChkAllTerminals.Location = new System.Drawing.Point(444, 48);
            this.ChkAllTerminals.Name = "ChkAllTerminals";
            this.ChkAllTerminals.Size = new System.Drawing.Size(99, 17);
            this.ChkAllTerminals.TabIndex = 64;
            this.ChkAllTerminals.Text = "All Terminals";
            this.ChkAllTerminals.UseVisualStyleBackColor = true;
            this.ChkAllTerminals.CheckedChanged += new System.EventHandler(this.ChkAllTerminals_CheckedChanged);
            // 
            // lblTerminal
            // 
            this.lblTerminal.AutoSize = true;
            this.lblTerminal.Location = new System.Drawing.Point(22, 49);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(56, 13);
            this.lblTerminal.TabIndex = 63;
            this.lblTerminal.Text = "Terminal";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(145, 18);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(287, 21);
            this.cmbLocation.TabIndex = 61;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(22, 20);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // FrmPosSalesSummery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(629, 193);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPosSalesSummery";
            this.Text = "Sales Summary";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.ComboBox cmbTerminal;
        private System.Windows.Forms.CheckBox ChkAllTerminals;
        private System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.CheckBox ChkAllLocations;
        private System.Windows.Forms.CheckBox ChkLocationWise;
    }
}
