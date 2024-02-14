namespace UI.Windows
{
    partial class FrmGivenDateStock
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
            this.ChkAllLocations = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.RdoSupplier = new System.Windows.Forms.RadioButton();
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
            this.dtpGivenDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 187);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(422, 187);
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
            this.groupBox1.Controls.Add(this.ChkAllLocations);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.RdoSupplier);
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
            this.groupBox1.Controls.Add(this.dtpGivenDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 197);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // ChkAllLocations
            // 
            this.ChkAllLocations.AutoSize = true;
            this.ChkAllLocations.Location = new System.Drawing.Point(432, 48);
            this.ChkAllLocations.Name = "ChkAllLocations";
            this.ChkAllLocations.Size = new System.Drawing.Size(97, 17);
            this.ChkAllLocations.TabIndex = 112;
            this.ChkAllLocations.Text = "All Locations";
            this.ChkAllLocations.UseVisualStyleBackColor = true;
            this.ChkAllLocations.CheckedChanged += new System.EventHandler(this.ChkAllLocations_CheckedChanged);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(133, 46);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(287, 21);
            this.cmbLocation.TabIndex = 110;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(20, 49);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 111;
            this.lblLocation.Text = "Location";
            // 
            // RdoSupplier
            // 
            this.RdoSupplier.AutoSize = true;
            this.RdoSupplier.Location = new System.Drawing.Point(568, 97);
            this.RdoSupplier.Name = "RdoSupplier";
            this.RdoSupplier.Size = new System.Drawing.Size(72, 17);
            this.RdoSupplier.TabIndex = 109;
            this.RdoSupplier.Tag = "1";
            this.RdoSupplier.Text = "Supplier";
            this.RdoSupplier.UseVisualStyleBackColor = true;
            this.RdoSupplier.CheckedChanged += new System.EventHandler(this.RdoSupplier_CheckedChanged);
            // 
            // RdoSubCategory
            // 
            this.RdoSubCategory.AutoSize = true;
            this.RdoSubCategory.Location = new System.Drawing.Point(448, 97);
            this.RdoSubCategory.Name = "RdoSubCategory";
            this.RdoSubCategory.Size = new System.Drawing.Size(104, 17);
            this.RdoSubCategory.TabIndex = 108;
            this.RdoSubCategory.Tag = "1";
            this.RdoSubCategory.Text = "Sub Category";
            this.RdoSubCategory.UseVisualStyleBackColor = true;
            this.RdoSubCategory.CheckedChanged += new System.EventHandler(this.RdoSubCategory_CheckedChanged);
            // 
            // RdoCategory
            // 
            this.RdoCategory.AutoSize = true;
            this.RdoCategory.Location = new System.Drawing.Point(346, 97);
            this.RdoCategory.Name = "RdoCategory";
            this.RdoCategory.Size = new System.Drawing.Size(78, 17);
            this.RdoCategory.TabIndex = 107;
            this.RdoCategory.Tag = "1";
            this.RdoCategory.Text = "Category";
            this.RdoCategory.UseVisualStyleBackColor = true;
            this.RdoCategory.CheckedChanged += new System.EventHandler(this.RdoCategory_CheckedChanged);
            // 
            // ChkAutoComplteTo
            // 
            this.ChkAutoComplteTo.AutoSize = true;
            this.ChkAutoComplteTo.Location = new System.Drawing.Point(112, 157);
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
            this.ChkAutoComplteFrom.Location = new System.Drawing.Point(112, 130);
            this.ChkAutoComplteFrom.Name = "ChkAutoComplteFrom";
            this.ChkAutoComplteFrom.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteFrom.TabIndex = 105;
            this.ChkAutoComplteFrom.Tag = "1";
            this.ChkAutoComplteFrom.UseVisualStyleBackColor = true;
            this.ChkAutoComplteFrom.CheckedChanged += new System.EventHandler(this.ChkAutoComplteFrom_CheckedChanged);
            // 
            // TxtSearchNameTo
            // 
            this.TxtSearchNameTo.Location = new System.Drawing.Point(269, 154);
            this.TxtSearchNameTo.Name = "TxtSearchNameTo";
            this.TxtSearchNameTo.Size = new System.Drawing.Size(376, 21);
            this.TxtSearchNameTo.TabIndex = 104;
            // 
            // TxtSearchNameFrom
            // 
            this.TxtSearchNameFrom.Location = new System.Drawing.Point(269, 127);
            this.TxtSearchNameFrom.Name = "TxtSearchNameFrom";
            this.TxtSearchNameFrom.Size = new System.Drawing.Size(376, 21);
            this.TxtSearchNameFrom.TabIndex = 103;
            // 
            // TxtSearchCodeTo
            // 
            this.TxtSearchCodeTo.Location = new System.Drawing.Point(133, 154);
            this.TxtSearchCodeTo.Name = "TxtSearchCodeTo";
            this.TxtSearchCodeTo.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeTo.TabIndex = 102;
            this.TxtSearchCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeTo_KeyDown);
            this.TxtSearchCodeTo.Leave += new System.EventHandler(this.TxtSearchCodeTo_Leave);
            // 
            // TxtSearchCodeFrom
            // 
            this.TxtSearchCodeFrom.Location = new System.Drawing.Point(133, 127);
            this.TxtSearchCodeFrom.Name = "TxtSearchCodeFrom";
            this.TxtSearchCodeFrom.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeFrom.TabIndex = 101;
            this.TxtSearchCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeFrom_KeyDown);
            this.TxtSearchCodeFrom.Leave += new System.EventHandler(this.TxtSearchCodeFrom_Leave);
            // 
            // RdoProduct
            // 
            this.RdoProduct.AutoSize = true;
            this.RdoProduct.Checked = true;
            this.RdoProduct.Location = new System.Drawing.Point(133, 97);
            this.RdoProduct.Name = "RdoProduct";
            this.RdoProduct.Size = new System.Drawing.Size(68, 17);
            this.RdoProduct.TabIndex = 100;
            this.RdoProduct.TabStop = true;
            this.RdoProduct.Tag = "1";
            this.RdoProduct.Text = "Product";
            this.RdoProduct.UseVisualStyleBackColor = true;
            this.RdoProduct.CheckedChanged += new System.EventHandler(this.RdoProduct_CheckedChanged);
            // 
            // RdoDepartment
            // 
            this.RdoDepartment.AutoSize = true;
            this.RdoDepartment.Location = new System.Drawing.Point(226, 97);
            this.RdoDepartment.Name = "RdoDepartment";
            this.RdoDepartment.Size = new System.Drawing.Size(93, 17);
            this.RdoDepartment.TabIndex = 99;
            this.RdoDepartment.Tag = "1";
            this.RdoDepartment.Text = "Department";
            this.RdoDepartment.UseVisualStyleBackColor = true;
            this.RdoDepartment.CheckedChanged += new System.EventHandler(this.RdoDepartment_CheckedChanged);
            // 
            // LblSerachTo
            // 
            this.LblSerachTo.AutoSize = true;
            this.LblSerachTo.Location = new System.Drawing.Point(21, 157);
            this.LblSerachTo.Name = "LblSerachTo";
            this.LblSerachTo.Size = new System.Drawing.Size(54, 13);
            this.LblSerachTo.TabIndex = 98;
            this.LblSerachTo.Text = "Code To";
            // 
            // LblSearchFrom
            // 
            this.LblSearchFrom.AutoSize = true;
            this.LblSearchFrom.Location = new System.Drawing.Point(21, 130);
            this.LblSearchFrom.Name = "LblSearchFrom";
            this.LblSearchFrom.Size = new System.Drawing.Size(70, 13);
            this.LblSearchFrom.TabIndex = 97;
            this.LblSearchFrom.Text = "Code From";
            // 
            // dtpGivenDate
            // 
            this.dtpGivenDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGivenDate.Location = new System.Drawing.Point(133, 19);
            this.dtpGivenDate.Name = "dtpGivenDate";
            this.dtpGivenDate.Size = new System.Drawing.Size(133, 21);
            this.dtpGivenDate.TabIndex = 96;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(20, 25);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(71, 13);
            this.lblDate.TabIndex = 95;
            this.lblDate.Text = "Given Date";
            // 
            // FrmGivenDateStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(663, 236);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmGivenDateStock";
            this.Text = "Given Date Stock";
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
        private System.Windows.Forms.RadioButton RdoSupplier;
        private System.Windows.Forms.RadioButton RdoSubCategory;
        private System.Windows.Forms.RadioButton RdoCategory;
        private System.Windows.Forms.CheckBox ChkAutoComplteTo;
        private System.Windows.Forms.CheckBox ChkAutoComplteFrom;
        private System.Windows.Forms.TextBox TxtSearchNameTo;
        private System.Windows.Forms.TextBox TxtSearchNameFrom;
        private System.Windows.Forms.TextBox TxtSearchCodeTo;
        private System.Windows.Forms.TextBox TxtSearchCodeFrom;
        private System.Windows.Forms.RadioButton RdoProduct;
        private System.Windows.Forms.RadioButton RdoDepartment;
        private System.Windows.Forms.Label LblSerachTo;
        private System.Windows.Forms.Label LblSearchFrom;
        private System.Windows.Forms.DateTimePicker dtpGivenDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.CheckBox ChkAllLocations;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
    }
}
