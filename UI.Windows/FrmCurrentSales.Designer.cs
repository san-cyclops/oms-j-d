namespace UI.Windows
{
    partial class FrmCurrentSales
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
            this.RdoSubCategory = new System.Windows.Forms.RadioButton();
            this.RdoCategory = new System.Windows.Forms.RadioButton();
            this.ChkAutoComplteTo = new System.Windows.Forms.CheckBox();
            this.ChkAutoComplteFrom = new System.Windows.Forms.CheckBox();
            this.TxtSearchNameTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameFrom = new System.Windows.Forms.TextBox();
            this.TxtSearchCodeTo = new System.Windows.Forms.TextBox();
            this.TxtSearchCodeFrom = new System.Windows.Forms.TextBox();
            this.RdoProduct = new System.Windows.Forms.RadioButton();
            this.RdoDepartment = new System.Windows.Forms.RadioButton();
            this.LblSerachTo = new System.Windows.Forms.Label();
            this.LblSearchFrom = new System.Windows.Forms.Label();
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
            this.grpButtonSet.Location = new System.Drawing.Point(2, 194);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(399, 194);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RdoSubCategory);
            this.groupBox1.Controls.Add(this.RdoCategory);
            this.groupBox1.Controls.Add(this.ChkAutoComplteTo);
            this.groupBox1.Controls.Add(this.ChkAutoComplteFrom);
            this.groupBox1.Controls.Add(this.TxtSearchNameTo);
            this.groupBox1.Controls.Add(this.TxtSearchNameFrom);
            this.groupBox1.Controls.Add(this.TxtSearchCodeTo);
            this.groupBox1.Controls.Add(this.TxtSearchCodeFrom);
            this.groupBox1.Controls.Add(this.RdoProduct);
            this.groupBox1.Controls.Add(this.RdoDepartment);
            this.groupBox1.Controls.Add(this.LblSerachTo);
            this.groupBox1.Controls.Add(this.LblSearchFrom);
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
            this.groupBox1.Size = new System.Drawing.Size(636, 205);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // RdoSubCategory
            // 
            this.RdoSubCategory.AutoSize = true;
            this.RdoSubCategory.Location = new System.Drawing.Point(504, 123);
            this.RdoSubCategory.Name = "RdoSubCategory";
            this.RdoSubCategory.Size = new System.Drawing.Size(104, 17);
            this.RdoSubCategory.TabIndex = 84;
            this.RdoSubCategory.Tag = "1";
            this.RdoSubCategory.Text = "Sub Category";
            this.RdoSubCategory.UseVisualStyleBackColor = true;
            this.RdoSubCategory.CheckedChanged += new System.EventHandler(this.RdoSubCategory_CheckedChanged);
            // 
            // RdoCategory
            // 
            this.RdoCategory.AutoSize = true;
            this.RdoCategory.Location = new System.Drawing.Point(383, 123);
            this.RdoCategory.Name = "RdoCategory";
            this.RdoCategory.Size = new System.Drawing.Size(78, 17);
            this.RdoCategory.TabIndex = 83;
            this.RdoCategory.Tag = "1";
            this.RdoCategory.Text = "Category";
            this.RdoCategory.UseVisualStyleBackColor = true;
            this.RdoCategory.CheckedChanged += new System.EventHandler(this.RdoCategory_CheckedChanged);
            // 
            // ChkAutoComplteTo
            // 
            this.ChkAutoComplteTo.AutoSize = true;
            this.ChkAutoComplteTo.Location = new System.Drawing.Point(124, 176);
            this.ChkAutoComplteTo.Name = "ChkAutoComplteTo";
            this.ChkAutoComplteTo.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteTo.TabIndex = 82;
            this.ChkAutoComplteTo.Tag = "1";
            this.ChkAutoComplteTo.UseVisualStyleBackColor = true;
            this.ChkAutoComplteTo.CheckedChanged += new System.EventHandler(this.ChkAutoComplteTo_CheckedChanged);
            // 
            // ChkAutoComplteFrom
            // 
            this.ChkAutoComplteFrom.AutoSize = true;
            this.ChkAutoComplteFrom.Location = new System.Drawing.Point(124, 149);
            this.ChkAutoComplteFrom.Name = "ChkAutoComplteFrom";
            this.ChkAutoComplteFrom.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteFrom.TabIndex = 81;
            this.ChkAutoComplteFrom.Tag = "1";
            this.ChkAutoComplteFrom.UseVisualStyleBackColor = true;
            this.ChkAutoComplteFrom.CheckedChanged += new System.EventHandler(this.ChkAutoComplteFrom_CheckedChanged);
            // 
            // TxtSearchNameTo
            // 
            this.TxtSearchNameTo.Location = new System.Drawing.Point(281, 173);
            this.TxtSearchNameTo.Name = "TxtSearchNameTo";
            this.TxtSearchNameTo.Size = new System.Drawing.Size(349, 21);
            this.TxtSearchNameTo.TabIndex = 80;
            // 
            // TxtSearchNameFrom
            // 
            this.TxtSearchNameFrom.Location = new System.Drawing.Point(281, 146);
            this.TxtSearchNameFrom.Name = "TxtSearchNameFrom";
            this.TxtSearchNameFrom.Size = new System.Drawing.Size(349, 21);
            this.TxtSearchNameFrom.TabIndex = 79;
            // 
            // TxtSearchCodeTo
            // 
            this.TxtSearchCodeTo.Location = new System.Drawing.Point(145, 173);
            this.TxtSearchCodeTo.Name = "TxtSearchCodeTo";
            this.TxtSearchCodeTo.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeTo.TabIndex = 78;
            this.TxtSearchCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeTo_KeyDown);
            this.TxtSearchCodeTo.Leave += new System.EventHandler(this.TxtSearchCodeTo_Leave);
            // 
            // TxtSearchCodeFrom
            // 
            this.TxtSearchCodeFrom.Location = new System.Drawing.Point(145, 146);
            this.TxtSearchCodeFrom.Name = "TxtSearchCodeFrom";
            this.TxtSearchCodeFrom.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeFrom.TabIndex = 77;
            this.TxtSearchCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeFrom_KeyDown);
            this.TxtSearchCodeFrom.Leave += new System.EventHandler(this.TxtSearchCodeFrom_Leave);
            // 
            // RdoProduct
            // 
            this.RdoProduct.AutoSize = true;
            this.RdoProduct.Checked = true;
            this.RdoProduct.Location = new System.Drawing.Point(145, 123);
            this.RdoProduct.Name = "RdoProduct";
            this.RdoProduct.Size = new System.Drawing.Size(68, 17);
            this.RdoProduct.TabIndex = 76;
            this.RdoProduct.TabStop = true;
            this.RdoProduct.Tag = "1";
            this.RdoProduct.Text = "Product";
            this.RdoProduct.UseVisualStyleBackColor = true;
            this.RdoProduct.CheckedChanged += new System.EventHandler(this.RdoProduct_CheckedChanged);
            // 
            // RdoDepartment
            // 
            this.RdoDepartment.AutoSize = true;
            this.RdoDepartment.Location = new System.Drawing.Point(252, 123);
            this.RdoDepartment.Name = "RdoDepartment";
            this.RdoDepartment.Size = new System.Drawing.Size(93, 17);
            this.RdoDepartment.TabIndex = 73;
            this.RdoDepartment.Tag = "1";
            this.RdoDepartment.Text = "Department";
            this.RdoDepartment.UseVisualStyleBackColor = true;
            this.RdoDepartment.CheckedChanged += new System.EventHandler(this.RdoDepartment_CheckedChanged);
            // 
            // LblSerachTo
            // 
            this.LblSerachTo.AutoSize = true;
            this.LblSerachTo.Location = new System.Drawing.Point(14, 176);
            this.LblSerachTo.Name = "LblSerachTo";
            this.LblSerachTo.Size = new System.Drawing.Size(54, 13);
            this.LblSerachTo.TabIndex = 72;
            this.LblSerachTo.Text = "Code To";
            // 
            // LblSearchFrom
            // 
            this.LblSearchFrom.AutoSize = true;
            this.LblSearchFrom.Location = new System.Drawing.Point(14, 149);
            this.LblSearchFrom.Name = "LblSearchFrom";
            this.LblSearchFrom.Size = new System.Drawing.Size(70, 13);
            this.LblSearchFrom.TabIndex = 71;
            this.LblSearchFrom.Text = "Code From";
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
            this.ChkAllLocations.Visible = false;
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
            this.lblDateRange.Location = new System.Drawing.Point(14, 77);
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
            this.lblTerminal.Location = new System.Drawing.Point(14, 49);
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
            this.lblLocation.Location = new System.Drawing.Point(14, 20);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // FrmCurrentSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 243);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCurrentSales";
            this.Text = "Current Sales";
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
        private System.Windows.Forms.CheckBox ChkAllLocations;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.ComboBox cmbTerminal;
        private System.Windows.Forms.CheckBox ChkAllTerminals;
        private System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox TxtSearchNameTo;
        private System.Windows.Forms.TextBox TxtSearchNameFrom;
        private System.Windows.Forms.TextBox TxtSearchCodeTo;
        private System.Windows.Forms.TextBox TxtSearchCodeFrom;
        private System.Windows.Forms.RadioButton RdoProduct;
        private System.Windows.Forms.RadioButton RdoDepartment;
        private System.Windows.Forms.Label LblSerachTo;
        private System.Windows.Forms.Label LblSearchFrom;
        private System.Windows.Forms.CheckBox ChkAutoComplteTo;
        private System.Windows.Forms.CheckBox ChkAutoComplteFrom;
        private System.Windows.Forms.RadioButton RdoCategory;
        private System.Windows.Forms.RadioButton RdoSubCategory;
    }
}
