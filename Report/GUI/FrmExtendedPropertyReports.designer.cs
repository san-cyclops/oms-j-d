namespace Report.GUI
{
    partial class FrmExtendedPropertyReports
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
            this.grpLocations = new System.Windows.Forms.GroupBox();
            this.txtLocations = new System.Windows.Forms.TextBox();
            this.chkLocations = new System.Windows.Forms.CheckBox();
            this.chkLstLocations = new System.Windows.Forms.CheckedListBox();
            this.grpSuppliers = new System.Windows.Forms.GroupBox();
            this.txtSuppliers = new System.Windows.Forms.TextBox();
            this.chkSuppliers = new System.Windows.Forms.CheckBox();
            this.chkLstSuppliers = new System.Windows.Forms.CheckedListBox();
            this.grpProductProperties = new System.Windows.Forms.GroupBox();
            this.txtProductPropertyValue = new System.Windows.Forms.TextBox();
            this.chkProductProperties = new System.Windows.Forms.CheckBox();
            this.rbtnCategory = new System.Windows.Forms.RadioButton();
            this.rbtnSubCategory2 = new System.Windows.Forms.RadioButton();
            this.rbtnDepartment = new System.Windows.Forms.RadioButton();
            this.rbtnSubCategory = new System.Windows.Forms.RadioButton();
            this.chkLstProductPropertyValues = new System.Windows.Forms.CheckedListBox();
            this.grpProductExtendedProperties = new System.Windows.Forms.GroupBox();
            this.txtProductExtendedProperties = new System.Windows.Forms.TextBox();
            this.chkProductExtendedProperties = new System.Windows.Forms.CheckBox();
            this.chkLstProductExtendedProperties = new System.Windows.Forms.CheckedListBox();
            this.grpDateRange = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.pBoxPreLoader = new System.Windows.Forms.PictureBox();
            this.bgWReport = new System.ComponentModel.BackgroundWorker();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpLocations.SuspendLayout();
            this.grpSuppliers.SuspendLayout();
            this.grpProductProperties.SuspendLayout();
            this.grpProductExtendedProperties.SuspendLayout();
            this.grpDateRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Controls.Add(this.pBoxPreLoader);
            this.grpButtonSet2.Location = new System.Drawing.Point(586, 391);
            this.grpButtonSet2.Size = new System.Drawing.Size(280, 46);
            this.grpButtonSet2.Controls.SetChildIndex(this.btnView, 0);
            this.grpButtonSet2.Controls.SetChildIndex(this.btnClose, 0);
            this.grpButtonSet2.Controls.SetChildIndex(this.btnClear, 0);
            this.grpButtonSet2.Controls.SetChildIndex(this.pBoxPreLoader, 0);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 391);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(123, 11);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(201, 11);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(45, 11);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpLocations);
            this.groupBox1.Controls.Add(this.grpSuppliers);
            this.groupBox1.Controls.Add(this.grpProductProperties);
            this.groupBox1.Controls.Add(this.grpProductExtendedProperties);
            this.groupBox1.Location = new System.Drawing.Point(3, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 377);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // grpLocations
            // 
            this.grpLocations.Controls.Add(this.txtLocations);
            this.grpLocations.Controls.Add(this.chkLocations);
            this.grpLocations.Controls.Add(this.chkLstLocations);
            this.grpLocations.Location = new System.Drawing.Point(461, 8);
            this.grpLocations.Name = "grpLocations";
            this.grpLocations.Size = new System.Drawing.Size(200, 344);
            this.grpLocations.TabIndex = 3;
            this.grpLocations.TabStop = false;
            this.grpLocations.Text = "    Locations";
            // 
            // txtLocations
            // 
            this.txtLocations.Location = new System.Drawing.Point(5, 313);
            this.txtLocations.Name = "txtLocations";
            this.txtLocations.Size = new System.Drawing.Size(189, 21);
            this.txtLocations.TabIndex = 4;
            this.txtLocations.TextChanged += new System.EventHandler(this.txtLocations_TextChanged);
            // 
            // chkLocations
            // 
            this.chkLocations.AutoSize = true;
            this.chkLocations.Location = new System.Drawing.Point(7, 1);
            this.chkLocations.Name = "chkLocations";
            this.chkLocations.Size = new System.Drawing.Size(15, 14);
            this.chkLocations.TabIndex = 2;
            this.chkLocations.UseVisualStyleBackColor = true;
            this.chkLocations.CheckedChanged += new System.EventHandler(this.chkLocations_CheckedChanged);
            // 
            // chkLstLocations
            // 
            this.chkLstLocations.CheckOnClick = true;
            this.chkLstLocations.FormattingEnabled = true;
            this.chkLstLocations.Location = new System.Drawing.Point(5, 20);
            this.chkLstLocations.Name = "chkLstLocations";
            this.chkLstLocations.Size = new System.Drawing.Size(189, 292);
            this.chkLstLocations.TabIndex = 0;
            this.chkLstLocations.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstLocations_ItemCheck);
            // 
            // grpSuppliers
            // 
            this.grpSuppliers.Controls.Add(this.txtSuppliers);
            this.grpSuppliers.Controls.Add(this.chkSuppliers);
            this.grpSuppliers.Controls.Add(this.chkLstSuppliers);
            this.grpSuppliers.Location = new System.Drawing.Point(662, 8);
            this.grpSuppliers.Name = "grpSuppliers";
            this.grpSuppliers.Size = new System.Drawing.Size(198, 344);
            this.grpSuppliers.TabIndex = 2;
            this.grpSuppliers.TabStop = false;
            this.grpSuppliers.Text = "    Suppliers";
            // 
            // txtSuppliers
            // 
            this.txtSuppliers.Location = new System.Drawing.Point(5, 313);
            this.txtSuppliers.Name = "txtSuppliers";
            this.txtSuppliers.Size = new System.Drawing.Size(189, 21);
            this.txtSuppliers.TabIndex = 5;
            this.txtSuppliers.TextChanged += new System.EventHandler(this.txtSuppliers_TextChanged);
            // 
            // chkSuppliers
            // 
            this.chkSuppliers.AutoSize = true;
            this.chkSuppliers.Location = new System.Drawing.Point(7, 1);
            this.chkSuppliers.Name = "chkSuppliers";
            this.chkSuppliers.Size = new System.Drawing.Size(15, 14);
            this.chkSuppliers.TabIndex = 2;
            this.chkSuppliers.UseVisualStyleBackColor = true;
            this.chkSuppliers.CheckedChanged += new System.EventHandler(this.chkSuppliers_CheckedChanged);
            // 
            // chkLstSuppliers
            // 
            this.chkLstSuppliers.CheckOnClick = true;
            this.chkLstSuppliers.FormattingEnabled = true;
            this.chkLstSuppliers.Location = new System.Drawing.Point(5, 20);
            this.chkLstSuppliers.Name = "chkLstSuppliers";
            this.chkLstSuppliers.Size = new System.Drawing.Size(189, 292);
            this.chkLstSuppliers.TabIndex = 0;
            this.chkLstSuppliers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstSuppliers_ItemCheck);
            // 
            // grpProductProperties
            // 
            this.grpProductProperties.Controls.Add(this.txtProductPropertyValue);
            this.grpProductProperties.Controls.Add(this.chkProductProperties);
            this.grpProductProperties.Controls.Add(this.rbtnCategory);
            this.grpProductProperties.Controls.Add(this.rbtnSubCategory2);
            this.grpProductProperties.Controls.Add(this.rbtnDepartment);
            this.grpProductProperties.Controls.Add(this.rbtnSubCategory);
            this.grpProductProperties.Controls.Add(this.chkLstProductPropertyValues);
            this.grpProductProperties.Location = new System.Drawing.Point(205, 8);
            this.grpProductProperties.Name = "grpProductProperties";
            this.grpProductProperties.Size = new System.Drawing.Size(255, 344);
            this.grpProductProperties.TabIndex = 2;
            this.grpProductProperties.TabStop = false;
            this.grpProductProperties.Text = "    Product Properties";
            // 
            // txtProductPropertyValue
            // 
            this.txtProductPropertyValue.Location = new System.Drawing.Point(6, 313);
            this.txtProductPropertyValue.Name = "txtProductPropertyValue";
            this.txtProductPropertyValue.Size = new System.Drawing.Size(244, 21);
            this.txtProductPropertyValue.TabIndex = 3;
            this.txtProductPropertyValue.TextChanged += new System.EventHandler(this.txtProductPropertyValue_TextChanged);
            // 
            // chkProductProperties
            // 
            this.chkProductProperties.AutoSize = true;
            this.chkProductProperties.Location = new System.Drawing.Point(7, 1);
            this.chkProductProperties.Name = "chkProductProperties";
            this.chkProductProperties.Size = new System.Drawing.Size(15, 14);
            this.chkProductProperties.TabIndex = 2;
            this.chkProductProperties.UseVisualStyleBackColor = true;
            this.chkProductProperties.CheckedChanged += new System.EventHandler(this.chkProductProperties_CheckedChanged);
            // 
            // rbtnCategory
            // 
            this.rbtnCategory.AutoSize = true;
            this.rbtnCategory.Location = new System.Drawing.Point(7, 31);
            this.rbtnCategory.Name = "rbtnCategory";
            this.rbtnCategory.Size = new System.Drawing.Size(98, 17);
            this.rbtnCategory.TabIndex = 1;
            this.rbtnCategory.TabStop = true;
            this.rbtnCategory.Text = "radioButton1";
            this.rbtnCategory.UseVisualStyleBackColor = true;
            this.rbtnCategory.CheckedChanged += new System.EventHandler(this.rbtnCategory_CheckedChanged);
            // 
            // rbtnSubCategory2
            // 
            this.rbtnSubCategory2.AutoSize = true;
            this.rbtnSubCategory2.Location = new System.Drawing.Point(140, 31);
            this.rbtnSubCategory2.Name = "rbtnSubCategory2";
            this.rbtnSubCategory2.Size = new System.Drawing.Size(98, 17);
            this.rbtnSubCategory2.TabIndex = 1;
            this.rbtnSubCategory2.TabStop = true;
            this.rbtnSubCategory2.Text = "radioButton1";
            this.rbtnSubCategory2.UseVisualStyleBackColor = true;
            this.rbtnSubCategory2.CheckedChanged += new System.EventHandler(this.rbtnSubCategory2_CheckedChanged);
            // 
            // rbtnDepartment
            // 
            this.rbtnDepartment.AutoSize = true;
            this.rbtnDepartment.Location = new System.Drawing.Point(7, 16);
            this.rbtnDepartment.Name = "rbtnDepartment";
            this.rbtnDepartment.Size = new System.Drawing.Size(98, 17);
            this.rbtnDepartment.TabIndex = 1;
            this.rbtnDepartment.TabStop = true;
            this.rbtnDepartment.Text = "radioButton1";
            this.rbtnDepartment.UseVisualStyleBackColor = true;
            this.rbtnDepartment.CheckedChanged += new System.EventHandler(this.rbtnDepartment_CheckedChanged);
            // 
            // rbtnSubCategory
            // 
            this.rbtnSubCategory.AutoSize = true;
            this.rbtnSubCategory.Location = new System.Drawing.Point(140, 16);
            this.rbtnSubCategory.Name = "rbtnSubCategory";
            this.rbtnSubCategory.Size = new System.Drawing.Size(98, 17);
            this.rbtnSubCategory.TabIndex = 1;
            this.rbtnSubCategory.TabStop = true;
            this.rbtnSubCategory.Text = "radioButton1";
            this.rbtnSubCategory.UseVisualStyleBackColor = true;
            this.rbtnSubCategory.CheckedChanged += new System.EventHandler(this.rbtnSubCategory_CheckedChanged);
            // 
            // chkLstProductPropertyValues
            // 
            this.chkLstProductPropertyValues.CheckOnClick = true;
            this.chkLstProductPropertyValues.FormattingEnabled = true;
            this.chkLstProductPropertyValues.Location = new System.Drawing.Point(6, 52);
            this.chkLstProductPropertyValues.Name = "chkLstProductPropertyValues";
            this.chkLstProductPropertyValues.Size = new System.Drawing.Size(244, 260);
            this.chkLstProductPropertyValues.TabIndex = 0;
            this.chkLstProductPropertyValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductPropertyValues_ItemCheck);
            // 
            // grpProductExtendedProperties
            // 
            this.grpProductExtendedProperties.Controls.Add(this.txtProductExtendedProperties);
            this.grpProductExtendedProperties.Controls.Add(this.chkProductExtendedProperties);
            this.grpProductExtendedProperties.Controls.Add(this.chkLstProductExtendedProperties);
            this.grpProductExtendedProperties.Location = new System.Drawing.Point(4, 8);
            this.grpProductExtendedProperties.Name = "grpProductExtendedProperties";
            this.grpProductExtendedProperties.Size = new System.Drawing.Size(200, 341);
            this.grpProductExtendedProperties.TabIndex = 1;
            this.grpProductExtendedProperties.TabStop = false;
            this.grpProductExtendedProperties.Text = "    Product Extended Properties";
            // 
            // txtProductExtendedProperties
            // 
            this.txtProductExtendedProperties.Location = new System.Drawing.Point(5, 313);
            this.txtProductExtendedProperties.Name = "txtProductExtendedProperties";
            this.txtProductExtendedProperties.Size = new System.Drawing.Size(189, 21);
            this.txtProductExtendedProperties.TabIndex = 4;
            this.txtProductExtendedProperties.TextChanged += new System.EventHandler(this.txtProductExtendedProperties_TextChanged);
            // 
            // chkProductExtendedProperties
            // 
            this.chkProductExtendedProperties.AutoSize = true;
            this.chkProductExtendedProperties.Location = new System.Drawing.Point(8, 1);
            this.chkProductExtendedProperties.Name = "chkProductExtendedProperties";
            this.chkProductExtendedProperties.Size = new System.Drawing.Size(15, 14);
            this.chkProductExtendedProperties.TabIndex = 1;
            this.chkProductExtendedProperties.UseVisualStyleBackColor = true;
            this.chkProductExtendedProperties.CheckedChanged += new System.EventHandler(this.chkProductExtendedProperties_CheckedChanged);
            // 
            // chkLstProductExtendedProperties
            // 
            this.chkLstProductExtendedProperties.CheckOnClick = true;
            this.chkLstProductExtendedProperties.FormattingEnabled = true;
            this.chkLstProductExtendedProperties.Location = new System.Drawing.Point(5, 20);
            this.chkLstProductExtendedProperties.Name = "chkLstProductExtendedProperties";
            this.chkLstProductExtendedProperties.Size = new System.Drawing.Size(189, 292);
            this.chkLstProductExtendedProperties.TabIndex = 0;
            this.chkLstProductExtendedProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstProductExtendedProperties_ItemCheck);
            // 
            // grpDateRange
            // 
            this.grpDateRange.Controls.Add(this.label2);
            this.grpDateRange.Controls.Add(this.dtpDateTo);
            this.grpDateRange.Controls.Add(this.label1);
            this.grpDateRange.Controls.Add(this.dtpDateFrom);
            this.grpDateRange.Location = new System.Drawing.Point(3, 351);
            this.grpDateRange.Name = "grpDateRange";
            this.grpDateRange.Size = new System.Drawing.Size(863, 45);
            this.grpDateRange.TabIndex = 13;
            this.grpDateRange.TabStop = false;
            this.grpDateRange.Text = "Date Range";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Date To";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(286, 14);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(115, 21);
            this.dtpDateTo.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Date From";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(94, 14);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(115, 21);
            this.dtpDateFrom.TabIndex = 23;
            // 
            // pBoxPreLoader
            // 
            this.pBoxPreLoader.Image = global::Report.Properties.Resources.pre_loader;
            this.pBoxPreLoader.Location = new System.Drawing.Point(5, 11);
            this.pBoxPreLoader.Name = "pBoxPreLoader";
            this.pBoxPreLoader.Size = new System.Drawing.Size(38, 33);
            this.pBoxPreLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pBoxPreLoader.TabIndex = 26;
            this.pBoxPreLoader.TabStop = false;
            this.pBoxPreLoader.Visible = false;
            // 
            // bgWReport
            // 
            this.bgWReport.WorkerReportsProgress = true;
            this.bgWReport.WorkerSupportsCancellation = true;
            this.bgWReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWReport_DoWork);
            this.bgWReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWReport_ProgressChanged);
            this.bgWReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWReport_RunWorkerCompleted);
            // 
            // FrmExtendedPropertyReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 439);
            this.Controls.Add(this.grpDateRange);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmExtendedPropertyReports";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpDateRange, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpLocations.ResumeLayout(false);
            this.grpLocations.PerformLayout();
            this.grpSuppliers.ResumeLayout(false);
            this.grpSuppliers.PerformLayout();
            this.grpProductProperties.ResumeLayout(false);
            this.grpProductProperties.PerformLayout();
            this.grpProductExtendedProperties.ResumeLayout(false);
            this.grpProductExtendedProperties.PerformLayout();
            this.grpDateRange.ResumeLayout(false);
            this.grpDateRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkLstProductExtendedProperties;
        private System.Windows.Forms.GroupBox grpProductExtendedProperties;
        private System.Windows.Forms.GroupBox grpSuppliers;
        private System.Windows.Forms.CheckedListBox chkLstSuppliers;
        private System.Windows.Forms.GroupBox grpProductProperties;
        private System.Windows.Forms.CheckedListBox chkLstProductPropertyValues;
        private System.Windows.Forms.RadioButton rbtnCategory;
        private System.Windows.Forms.RadioButton rbtnSubCategory2;
        private System.Windows.Forms.RadioButton rbtnDepartment;
        private System.Windows.Forms.RadioButton rbtnSubCategory;
        private System.Windows.Forms.GroupBox grpLocations;
        private System.Windows.Forms.CheckedListBox chkLstLocations;
        private System.Windows.Forms.GroupBox grpDateRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.CheckBox chkLocations;
        private System.Windows.Forms.CheckBox chkSuppliers;
        private System.Windows.Forms.CheckBox chkProductProperties;
        private System.Windows.Forms.CheckBox chkProductExtendedProperties;
        private System.ComponentModel.BackgroundWorker bgWReport;
        private System.Windows.Forms.TextBox txtProductPropertyValue;
        private System.Windows.Forms.TextBox txtLocations;
        private System.Windows.Forms.TextBox txtSuppliers;
        private System.Windows.Forms.TextBox txtProductExtendedProperties;
        private System.Windows.Forms.PictureBox pBoxPreLoader;
    }
}
