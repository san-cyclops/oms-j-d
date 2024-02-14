namespace UI.Windows
{
    partial class FrmProductPriceChange
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlWizard = new System.Windows.Forms.Panel();
            this.pnlLocationSelection = new System.Windows.Forms.Panel();
            this.btnLoadItemOk = new System.Windows.Forms.Button();
            this.btnFinished = new System.Windows.Forms.Button();
            this.dgvLocation = new System.Windows.Forms.DataGridView();
            this.Allow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LocationIDy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAllLocation = new System.Windows.Forms.CheckBox();
            this.tabCritaria = new System.Windows.Forms.TabControl();
            this.tpDepartment = new System.Windows.Forms.TabPage();
            this.pnlDptSelection = new System.Windows.Forms.Panel();
            this.chkDepartment = new System.Windows.Forms.CheckBox();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.DepartmentAllow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DepartmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tpCategory = new System.Windows.Forms.TabPage();
            this.pnlCategorySelection = new System.Windows.Forms.Panel();
            this.chkCategory = new System.Windows.Forms.CheckBox();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.CategoryAllow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.tpSubCategory = new System.Windows.Forms.TabPage();
            this.pnlSubCategorySelection = new System.Windows.Forms.Panel();
            this.chkSubCategory = new System.Windows.Forms.CheckBox();
            this.dgvSubCategory = new System.Windows.Forms.DataGridView();
            this.SubCategoryAllow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.InvSubCategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.tpSubCategory2 = new System.Windows.Forms.TabPage();
            this.pnlSubCategory2 = new System.Windows.Forms.Panel();
            this.chkSubCategory2 = new System.Windows.Forms.CheckBox();
            this.dgvSubCategory2 = new System.Windows.Forms.DataGridView();
            this.SubCategory2Allow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.invSubCategory2Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.tpBatchNos = new System.Windows.Forms.TabPage();
            this.pnlBatchNos = new System.Windows.Forms.Panel();
            this.chkBatchNo = new System.Windows.Forms.CheckBox();
            this.dgvBatchnos = new System.Windows.Forms.DataGridView();
            this.BatchNoAllow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BatchNos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchLocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.tpSupplier = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAllSupplier = new System.Windows.Forms.CheckBox();
            this.dgvSupplier = new System.Windows.Forms.DataGridView();
            this.supAllow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SupplierId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.tpCritaria = new System.Windows.Forms.TabPage();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.txtRecallDocNo = new System.Windows.Forms.TextBox();
            this.btnRecalEffective = new Glass.GlassButton();
            this.chkPriceDecreas = new System.Windows.Forms.CheckBox();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpEffectivDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.chkAutoCompleationPoNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPPChange = new System.Windows.Forms.Label();
            this.txtNewSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtMrp = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtLoca = new UI.Windows.CustomControls.TextBoxNumeric();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtNewCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkUpdateCostPrice = new System.Windows.Forms.CheckBox();
            this.pnlUpdateCostPrice = new System.Windows.Forms.Panel();
            this.chkIsIncrementCost = new System.Windows.Forms.CheckBox();
            this.txtCostDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.btnUpdateCostPrice = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.chkCostPercentage = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkUpdateSellingPrice = new System.Windows.Forms.CheckBox();
            this.pnlUpdateSellingPrice = new System.Windows.Forms.Panel();
            this.chkIsIncrementSelling = new System.Windows.Forms.CheckBox();
            this.txtSellingDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.btnUpdateSellingPrice = new System.Windows.Forms.Button();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.chkSellingPercentage = new System.Windows.Forms.CheckBox();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.LineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasureName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewSellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewCostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationNamex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlWizard.SuspendLayout();
            this.pnlLocationSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).BeginInit();
            this.tabCritaria.SuspendLayout();
            this.tpDepartment.SuspendLayout();
            this.pnlDptSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.tpCategory.SuspendLayout();
            this.pnlCategorySelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.tpSubCategory.SuspendLayout();
            this.pnlSubCategorySelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory)).BeginInit();
            this.tpSubCategory2.SuspendLayout();
            this.pnlSubCategory2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory2)).BeginInit();
            this.tpBatchNos.SuspendLayout();
            this.pnlBatchNos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchnos)).BeginInit();
            this.tpSupplier.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).BeginInit();
            this.tpCritaria.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlUpdateCostPrice.SuspendLayout();
            this.pnlUpdateSellingPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(847, 508);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(3, 508);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlWizard);
            this.panel1.Controls.Add(this.tabCritaria);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Location = new System.Drawing.Point(2, -7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 515);
            this.panel1.TabIndex = 12;
            // 
            // pnlWizard
            // 
            this.pnlWizard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlWizard.Controls.Add(this.pnlLocationSelection);
            this.pnlWizard.Location = new System.Drawing.Point(639, 368);
            this.pnlWizard.Name = "pnlWizard";
            this.pnlWizard.Size = new System.Drawing.Size(427, 321);
            this.pnlWizard.TabIndex = 67;
            this.pnlWizard.Visible = false;
            // 
            // pnlLocationSelection
            // 
            this.pnlLocationSelection.Controls.Add(this.btnLoadItemOk);
            this.pnlLocationSelection.Controls.Add(this.btnFinished);
            this.pnlLocationSelection.Controls.Add(this.dgvLocation);
            this.pnlLocationSelection.Controls.Add(this.label1);
            this.pnlLocationSelection.Controls.Add(this.chkAllLocation);
            this.pnlLocationSelection.Location = new System.Drawing.Point(1, 7);
            this.pnlLocationSelection.Name = "pnlLocationSelection";
            this.pnlLocationSelection.Size = new System.Drawing.Size(419, 307);
            this.pnlLocationSelection.TabIndex = 37;
            this.pnlLocationSelection.Tag = "0";
            // 
            // btnLoadItemOk
            // 
            this.btnLoadItemOk.Location = new System.Drawing.Point(138, 275);
            this.btnLoadItemOk.Name = "btnLoadItemOk";
            this.btnLoadItemOk.Size = new System.Drawing.Size(124, 23);
            this.btnLoadItemOk.TabIndex = 76;
            this.btnLoadItemOk.Text = "Ok";
            this.btnLoadItemOk.UseVisualStyleBackColor = true;
            this.btnLoadItemOk.Click += new System.EventHandler(this.btnLoadItemOk_Click);
            // 
            // btnFinished
            // 
            this.btnFinished.Location = new System.Drawing.Point(156, 37);
            this.btnFinished.Name = "btnFinished";
            this.btnFinished.Size = new System.Drawing.Size(124, 23);
            this.btnFinished.TabIndex = 44;
            this.btnFinished.Text = "Load Items";
            this.btnFinished.UseVisualStyleBackColor = true;
            this.btnFinished.Click += new System.EventHandler(this.btnFinished_Click);
            // 
            // dgvLocation
            // 
            this.dgvLocation.AllowUserToAddRows = false;
            this.dgvLocation.AllowUserToDeleteRows = false;
            this.dgvLocation.AllowUserToResizeColumns = false;
            this.dgvLocation.AllowUserToResizeRows = false;
            this.dgvLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Allow,
            this.LocationIDy,
            this.Location});
            this.dgvLocation.Location = new System.Drawing.Point(22, 62);
            this.dgvLocation.Name = "dgvLocation";
            this.dgvLocation.RowHeadersVisible = false;
            this.dgvLocation.Size = new System.Drawing.Size(374, 209);
            this.dgvLocation.TabIndex = 75;
            // 
            // Allow
            // 
            this.Allow.DataPropertyName = "Allow";
            this.Allow.Frozen = true;
            this.Allow.HeaderText = "Select";
            this.Allow.Name = "Allow";
            this.Allow.Width = 45;
            // 
            // LocationIDy
            // 
            this.LocationIDy.DataPropertyName = "LocationID";
            this.LocationIDy.HeaderText = "LocationIDy";
            this.LocationIDy.Name = "LocationIDy";
            this.LocationIDy.Visible = false;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "LocationName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 180;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "Location Selection";
            // 
            // chkAllLocation
            // 
            this.chkAllLocation.AutoSize = true;
            this.chkAllLocation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllLocation.Location = new System.Drawing.Point(22, 36);
            this.chkAllLocation.Name = "chkAllLocation";
            this.chkAllLocation.Size = new System.Drawing.Size(97, 17);
            this.chkAllLocation.TabIndex = 72;
            this.chkAllLocation.Text = "All Locations";
            this.chkAllLocation.UseVisualStyleBackColor = true;
            this.chkAllLocation.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // tabCritaria
            // 
            this.tabCritaria.Controls.Add(this.tpDepartment);
            this.tabCritaria.Controls.Add(this.tpCategory);
            this.tabCritaria.Controls.Add(this.tpSubCategory);
            this.tabCritaria.Controls.Add(this.tpSubCategory2);
            this.tabCritaria.Controls.Add(this.tpBatchNos);
            this.tabCritaria.Controls.Add(this.tpSupplier);
            this.tabCritaria.Controls.Add(this.tpCritaria);
            this.tabCritaria.Location = new System.Drawing.Point(4, 8);
            this.tabCritaria.Name = "tabCritaria";
            this.tabCritaria.SelectedIndex = 0;
            this.tabCritaria.Size = new System.Drawing.Size(1160, 504);
            this.tabCritaria.TabIndex = 65;
            this.tabCritaria.Click += new System.EventHandler(this.tabCritaria_Click);
            // 
            // tpDepartment
            // 
            this.tpDepartment.Controls.Add(this.pnlDptSelection);
            this.tpDepartment.Location = new System.Drawing.Point(4, 22);
            this.tpDepartment.Name = "tpDepartment";
            this.tpDepartment.Padding = new System.Windows.Forms.Padding(3);
            this.tpDepartment.Size = new System.Drawing.Size(1152, 478);
            this.tpDepartment.TabIndex = 3;
            this.tpDepartment.Text = "Department";
            this.tpDepartment.UseVisualStyleBackColor = true;
            // 
            // pnlDptSelection
            // 
            this.pnlDptSelection.Controls.Add(this.chkDepartment);
            this.pnlDptSelection.Controls.Add(this.dgvDepartment);
            this.pnlDptSelection.Controls.Add(this.label2);
            this.pnlDptSelection.Location = new System.Drawing.Point(6, 6);
            this.pnlDptSelection.Name = "pnlDptSelection";
            this.pnlDptSelection.Size = new System.Drawing.Size(1009, 308);
            this.pnlDptSelection.TabIndex = 40;
            this.pnlDptSelection.Tag = "1";
            // 
            // chkDepartment
            // 
            this.chkDepartment.AutoSize = true;
            this.chkDepartment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDepartment.Location = new System.Drawing.Point(20, 36);
            this.chkDepartment.Name = "chkDepartment";
            this.chkDepartment.Size = new System.Drawing.Size(118, 17);
            this.chkDepartment.TabIndex = 76;
            this.chkDepartment.Text = "All Departments";
            this.chkDepartment.UseVisualStyleBackColor = true;
            this.chkDepartment.CheckedChanged += new System.EventHandler(this.chkDepartment_CheckedChanged);
            // 
            // dgvDepartment
            // 
            this.dgvDepartment.AllowUserToAddRows = false;
            this.dgvDepartment.AllowUserToDeleteRows = false;
            this.dgvDepartment.AllowUserToResizeRows = false;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DepartmentAllow,
            this.DepartmentId,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.dgvDepartment.Location = new System.Drawing.Point(20, 62);
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.RowHeadersVisible = false;
            this.dgvDepartment.RowHeadersWidth = 5;
            this.dgvDepartment.Size = new System.Drawing.Size(379, 233);
            this.dgvDepartment.TabIndex = 75;
            // 
            // DepartmentAllow
            // 
            this.DepartmentAllow.HeaderText = "Allow";
            this.DepartmentAllow.Name = "DepartmentAllow";
            this.DepartmentAllow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DepartmentAllow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DepartmentAllow.Width = 45;
            // 
            // DepartmentId
            // 
            this.DepartmentId.DataPropertyName = "InvDepartmentId";
            this.DepartmentId.HeaderText = "DepartmentId";
            this.DepartmentId.Name = "DepartmentId";
            this.DepartmentId.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DepartmentCode";
            this.dataGridViewTextBoxColumn6.HeaderText = "Dept Code";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 5000;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 130;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "DepartmentName";
            this.dataGridViewTextBoxColumn7.HeaderText = "Department Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 170;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Department Selection";
            // 
            // tpCategory
            // 
            this.tpCategory.Controls.Add(this.pnlCategorySelection);
            this.tpCategory.Location = new System.Drawing.Point(4, 22);
            this.tpCategory.Name = "tpCategory";
            this.tpCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tpCategory.Size = new System.Drawing.Size(1152, 478);
            this.tpCategory.TabIndex = 4;
            this.tpCategory.Text = "Category";
            this.tpCategory.UseVisualStyleBackColor = true;
            // 
            // pnlCategorySelection
            // 
            this.pnlCategorySelection.Controls.Add(this.chkCategory);
            this.pnlCategorySelection.Controls.Add(this.dgvCategory);
            this.pnlCategorySelection.Controls.Add(this.label3);
            this.pnlCategorySelection.Location = new System.Drawing.Point(6, 6);
            this.pnlCategorySelection.Name = "pnlCategorySelection";
            this.pnlCategorySelection.Size = new System.Drawing.Size(553, 308);
            this.pnlCategorySelection.TabIndex = 41;
            this.pnlCategorySelection.Tag = "2";
            // 
            // chkCategory
            // 
            this.chkCategory.AutoSize = true;
            this.chkCategory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCategory.Location = new System.Drawing.Point(20, 37);
            this.chkCategory.Name = "chkCategory";
            this.chkCategory.Size = new System.Drawing.Size(97, 17);
            this.chkCategory.TabIndex = 76;
            this.chkCategory.Text = "All Category";
            this.chkCategory.UseVisualStyleBackColor = true;
            this.chkCategory.CheckedChanged += new System.EventHandler(this.chkCategory_CheckedChanged);
            // 
            // dgvCategory
            // 
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            this.dgvCategory.AllowUserToResizeRows = false;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryAllow,
            this.CategoryId,
            this.dataGridViewTextBoxColumn1,
            this.CategoryName});
            this.dgvCategory.Location = new System.Drawing.Point(20, 61);
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.RowHeadersVisible = false;
            this.dgvCategory.RowHeadersWidth = 5;
            this.dgvCategory.Size = new System.Drawing.Size(373, 232);
            this.dgvCategory.TabIndex = 75;
            // 
            // CategoryAllow
            // 
            this.CategoryAllow.HeaderText = "Allow";
            this.CategoryAllow.Name = "CategoryAllow";
            this.CategoryAllow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CategoryAllow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CategoryAllow.Width = 45;
            // 
            // CategoryId
            // 
            this.CategoryId.DataPropertyName = "InvCategoryId";
            this.CategoryId.HeaderText = "CategoryId";
            this.CategoryId.Name = "CategoryId";
            this.CategoryId.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CategoryCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "Category Code";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 5000;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "Category Name";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.Width = 170;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Category Selection";
            // 
            // tpSubCategory
            // 
            this.tpSubCategory.Controls.Add(this.pnlSubCategorySelection);
            this.tpSubCategory.Location = new System.Drawing.Point(4, 22);
            this.tpSubCategory.Name = "tpSubCategory";
            this.tpSubCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tpSubCategory.Size = new System.Drawing.Size(1152, 478);
            this.tpSubCategory.TabIndex = 5;
            this.tpSubCategory.Text = "Sub Category";
            this.tpSubCategory.UseVisualStyleBackColor = true;
            // 
            // pnlSubCategorySelection
            // 
            this.pnlSubCategorySelection.Controls.Add(this.chkSubCategory);
            this.pnlSubCategorySelection.Controls.Add(this.dgvSubCategory);
            this.pnlSubCategorySelection.Controls.Add(this.label4);
            this.pnlSubCategorySelection.Location = new System.Drawing.Point(6, 6);
            this.pnlSubCategorySelection.Name = "pnlSubCategorySelection";
            this.pnlSubCategorySelection.Size = new System.Drawing.Size(553, 308);
            this.pnlSubCategorySelection.TabIndex = 42;
            // 
            // chkSubCategory
            // 
            this.chkSubCategory.AutoSize = true;
            this.chkSubCategory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSubCategory.Location = new System.Drawing.Point(21, 37);
            this.chkSubCategory.Name = "chkSubCategory";
            this.chkSubCategory.Size = new System.Drawing.Size(123, 17);
            this.chkSubCategory.TabIndex = 76;
            this.chkSubCategory.Text = "All Sub Category";
            this.chkSubCategory.UseVisualStyleBackColor = true;
            this.chkSubCategory.CheckedChanged += new System.EventHandler(this.chkSubCategory_CheckedChanged);
            // 
            // dgvSubCategory
            // 
            this.dgvSubCategory.AllowUserToAddRows = false;
            this.dgvSubCategory.AllowUserToDeleteRows = false;
            this.dgvSubCategory.AllowUserToResizeRows = false;
            this.dgvSubCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubCategoryAllow,
            this.InvSubCategoryId,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvSubCategory.Location = new System.Drawing.Point(20, 60);
            this.dgvSubCategory.Name = "dgvSubCategory";
            this.dgvSubCategory.RowHeadersVisible = false;
            this.dgvSubCategory.RowHeadersWidth = 5;
            this.dgvSubCategory.Size = new System.Drawing.Size(379, 235);
            this.dgvSubCategory.TabIndex = 75;
            // 
            // SubCategoryAllow
            // 
            this.SubCategoryAllow.HeaderText = "Allow";
            this.SubCategoryAllow.Name = "SubCategoryAllow";
            this.SubCategoryAllow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SubCategoryAllow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SubCategoryAllow.Width = 45;
            // 
            // InvSubCategoryId
            // 
            this.InvSubCategoryId.DataPropertyName = "InvSubCategoryId";
            this.InvSubCategoryId.HeaderText = "InvCategoryId";
            this.InvSubCategoryId.Name = "InvSubCategoryId";
            this.InvSubCategoryId.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SubCategoryCode";
            this.dataGridViewTextBoxColumn2.HeaderText = "Sub Category Code";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 5000;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SubCategoryName";
            this.dataGridViewTextBoxColumn3.HeaderText = "Sub Category Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 170;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Sub Category Selection";
            // 
            // tpSubCategory2
            // 
            this.tpSubCategory2.Controls.Add(this.pnlSubCategory2);
            this.tpSubCategory2.Location = new System.Drawing.Point(4, 22);
            this.tpSubCategory2.Name = "tpSubCategory2";
            this.tpSubCategory2.Padding = new System.Windows.Forms.Padding(3);
            this.tpSubCategory2.Size = new System.Drawing.Size(1152, 478);
            this.tpSubCategory2.TabIndex = 6;
            this.tpSubCategory2.Text = "Sub Category2";
            this.tpSubCategory2.UseVisualStyleBackColor = true;
            // 
            // pnlSubCategory2
            // 
            this.pnlSubCategory2.Controls.Add(this.chkSubCategory2);
            this.pnlSubCategory2.Controls.Add(this.dgvSubCategory2);
            this.pnlSubCategory2.Controls.Add(this.label6);
            this.pnlSubCategory2.Location = new System.Drawing.Point(6, 6);
            this.pnlSubCategory2.Name = "pnlSubCategory2";
            this.pnlSubCategory2.Size = new System.Drawing.Size(553, 308);
            this.pnlSubCategory2.TabIndex = 47;
            this.pnlSubCategory2.Tag = "2";
            // 
            // chkSubCategory2
            // 
            this.chkSubCategory2.AutoSize = true;
            this.chkSubCategory2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSubCategory2.Location = new System.Drawing.Point(20, 38);
            this.chkSubCategory2.Name = "chkSubCategory2";
            this.chkSubCategory2.Size = new System.Drawing.Size(134, 17);
            this.chkSubCategory2.TabIndex = 76;
            this.chkSubCategory2.Text = "All Sub Category 2";
            this.chkSubCategory2.UseVisualStyleBackColor = true;
            this.chkSubCategory2.CheckedChanged += new System.EventHandler(this.chkSubCategory2_CheckedChanged);
            // 
            // dgvSubCategory2
            // 
            this.dgvSubCategory2.AllowUserToAddRows = false;
            this.dgvSubCategory2.AllowUserToDeleteRows = false;
            this.dgvSubCategory2.AllowUserToResizeRows = false;
            this.dgvSubCategory2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubCategory2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubCategory2Allow,
            this.invSubCategory2Id,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn8});
            this.dgvSubCategory2.Location = new System.Drawing.Point(20, 62);
            this.dgvSubCategory2.Name = "dgvSubCategory2";
            this.dgvSubCategory2.RowHeadersVisible = false;
            this.dgvSubCategory2.RowHeadersWidth = 5;
            this.dgvSubCategory2.Size = new System.Drawing.Size(379, 227);
            this.dgvSubCategory2.TabIndex = 75;
            // 
            // SubCategory2Allow
            // 
            this.SubCategory2Allow.HeaderText = "Allow";
            this.SubCategory2Allow.Name = "SubCategory2Allow";
            this.SubCategory2Allow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SubCategory2Allow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SubCategory2Allow.Width = 45;
            // 
            // invSubCategory2Id
            // 
            this.invSubCategory2Id.DataPropertyName = "invSubCategory2Id";
            this.invSubCategory2Id.HeaderText = "invSubCategory2Id";
            this.invSubCategory2Id.Name = "invSubCategory2Id";
            this.invSubCategory2Id.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SubCategory2Code";
            this.dataGridViewTextBoxColumn5.HeaderText = "Category Code";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 5000;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "SubCategory2Name";
            this.dataGridViewTextBoxColumn8.HeaderText = "Category Name";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 170;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Sub Category 2 Selection";
            // 
            // tpBatchNos
            // 
            this.tpBatchNos.Controls.Add(this.pnlBatchNos);
            this.tpBatchNos.Location = new System.Drawing.Point(4, 22);
            this.tpBatchNos.Name = "tpBatchNos";
            this.tpBatchNos.Size = new System.Drawing.Size(1152, 478);
            this.tpBatchNos.TabIndex = 7;
            this.tpBatchNos.Text = "Batch Nos";
            this.tpBatchNos.UseVisualStyleBackColor = true;
            // 
            // pnlBatchNos
            // 
            this.pnlBatchNos.Controls.Add(this.chkBatchNo);
            this.pnlBatchNos.Controls.Add(this.dgvBatchnos);
            this.pnlBatchNos.Controls.Add(this.label5);
            this.pnlBatchNos.Location = new System.Drawing.Point(5, 7);
            this.pnlBatchNos.Name = "pnlBatchNos";
            this.pnlBatchNos.Size = new System.Drawing.Size(553, 307);
            this.pnlBatchNos.TabIndex = 46;
            // 
            // chkBatchNo
            // 
            this.chkBatchNo.AutoSize = true;
            this.chkBatchNo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBatchNo.Location = new System.Drawing.Point(20, 38);
            this.chkBatchNo.Name = "chkBatchNo";
            this.chkBatchNo.Size = new System.Drawing.Size(101, 17);
            this.chkBatchNo.TabIndex = 76;
            this.chkBatchNo.Text = "All Batch Nos";
            this.chkBatchNo.UseVisualStyleBackColor = true;
            this.chkBatchNo.CheckedChanged += new System.EventHandler(this.chkBatchNo_CheckedChanged);
            // 
            // dgvBatchnos
            // 
            this.dgvBatchnos.AllowUserToAddRows = false;
            this.dgvBatchnos.AllowUserToDeleteRows = false;
            this.dgvBatchnos.AllowUserToResizeRows = false;
            this.dgvBatchnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchnos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchNoAllow,
            this.BatchNos,
            this.BatchLocationId});
            this.dgvBatchnos.Location = new System.Drawing.Point(20, 61);
            this.dgvBatchnos.Name = "dgvBatchnos";
            this.dgvBatchnos.RowHeadersVisible = false;
            this.dgvBatchnos.RowHeadersWidth = 5;
            this.dgvBatchnos.Size = new System.Drawing.Size(379, 229);
            this.dgvBatchnos.TabIndex = 75;
            // 
            // BatchNoAllow
            // 
            this.BatchNoAllow.HeaderText = "Allow";
            this.BatchNoAllow.Name = "BatchNoAllow";
            this.BatchNoAllow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BatchNoAllow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BatchNoAllow.Width = 45;
            // 
            // BatchNos
            // 
            this.BatchNos.DataPropertyName = "BatchNo";
            this.BatchNos.HeaderText = "Batch Nos";
            this.BatchNos.MaxInputLength = 5000;
            this.BatchNos.Name = "BatchNos";
            this.BatchNos.ReadOnly = true;
            this.BatchNos.Width = 130;
            // 
            // BatchLocationId
            // 
            this.BatchLocationId.DataPropertyName = "LocationId";
            this.BatchLocationId.HeaderText = "BatchLocationId";
            this.BatchLocationId.Name = "BatchLocationId";
            this.BatchLocationId.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Batch No Selection";
            // 
            // tpSupplier
            // 
            this.tpSupplier.Controls.Add(this.panel2);
            this.tpSupplier.Location = new System.Drawing.Point(4, 22);
            this.tpSupplier.Name = "tpSupplier";
            this.tpSupplier.Padding = new System.Windows.Forms.Padding(3);
            this.tpSupplier.Size = new System.Drawing.Size(1152, 478);
            this.tpSupplier.TabIndex = 9;
            this.tpSupplier.Text = "Supplier";
            this.tpSupplier.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkAllSupplier);
            this.panel2.Controls.Add(this.dgvSupplier);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(553, 308);
            this.panel2.TabIndex = 41;
            this.panel2.Tag = "1";
            // 
            // chkAllSupplier
            // 
            this.chkAllSupplier.AutoSize = true;
            this.chkAllSupplier.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllSupplier.Location = new System.Drawing.Point(20, 36);
            this.chkAllSupplier.Name = "chkAllSupplier";
            this.chkAllSupplier.Size = new System.Drawing.Size(91, 17);
            this.chkAllSupplier.TabIndex = 76;
            this.chkAllSupplier.Text = "All Supplier";
            this.chkAllSupplier.UseVisualStyleBackColor = true;
            this.chkAllSupplier.CheckedChanged += new System.EventHandler(this.chkAllSupplier_CheckedChanged);
            // 
            // dgvSupplier
            // 
            this.dgvSupplier.AllowUserToAddRows = false;
            this.dgvSupplier.AllowUserToDeleteRows = false;
            this.dgvSupplier.AllowUserToResizeRows = false;
            this.dgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.supAllow,
            this.SupplierId,
            this.SupplierCode,
            this.SupplierName});
            this.dgvSupplier.Location = new System.Drawing.Point(20, 62);
            this.dgvSupplier.Name = "dgvSupplier";
            this.dgvSupplier.RowHeadersVisible = false;
            this.dgvSupplier.RowHeadersWidth = 5;
            this.dgvSupplier.Size = new System.Drawing.Size(379, 233);
            this.dgvSupplier.TabIndex = 75;
            // 
            // supAllow
            // 
            this.supAllow.DataPropertyName = "supAllow";
            this.supAllow.HeaderText = "Allow";
            this.supAllow.Name = "supAllow";
            this.supAllow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.supAllow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.supAllow.Width = 45;
            // 
            // SupplierId
            // 
            this.SupplierId.DataPropertyName = "SupplierId";
            this.SupplierId.HeaderText = "SupplierId";
            this.SupplierId.Name = "SupplierId";
            this.SupplierId.Visible = false;
            // 
            // SupplierCode
            // 
            this.SupplierCode.DataPropertyName = "SupplierCode";
            this.SupplierCode.HeaderText = "Supplier Code";
            this.SupplierCode.MaxInputLength = 5000;
            this.SupplierCode.Name = "SupplierCode";
            this.SupplierCode.ReadOnly = true;
            this.SupplierCode.Width = 130;
            // 
            // SupplierName
            // 
            this.SupplierName.DataPropertyName = "SupplierName";
            this.SupplierName.HeaderText = "Supplier Name";
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.Width = 170;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Supplier Selection";
            // 
            // tpCritaria
            // 
            this.tpCritaria.Controls.Add(this.pnlProduct);
            this.tpCritaria.Location = new System.Drawing.Point(4, 22);
            this.tpCritaria.Name = "tpCritaria";
            this.tpCritaria.Padding = new System.Windows.Forms.Padding(3);
            this.tpCritaria.Size = new System.Drawing.Size(1152, 478);
            this.tpCritaria.TabIndex = 1;
            this.tpCritaria.Text = "Selection Criteria";
            this.tpCritaria.UseVisualStyleBackColor = true;
            // 
            // pnlProduct
            // 
            this.pnlProduct.Controls.Add(this.dgvItemDetails);
            this.pnlProduct.Controls.Add(this.txtRecallDocNo);
            this.pnlProduct.Controls.Add(this.btnRecalEffective);
            this.pnlProduct.Controls.Add(this.chkPriceDecreas);
            this.pnlProduct.Controls.Add(this.chkTStatus);
            this.pnlProduct.Controls.Add(this.txtReferenceNo);
            this.pnlProduct.Controls.Add(this.lblReferenceNo);
            this.pnlProduct.Controls.Add(this.dtpEffectivDate);
            this.pnlProduct.Controls.Add(this.label8);
            this.pnlProduct.Controls.Add(this.chkAutoCompleationPoNo);
            this.pnlProduct.Controls.Add(this.txtDocumentNo);
            this.pnlProduct.Controls.Add(this.lblPPChange);
            this.pnlProduct.Controls.Add(this.txtNewSellingPrice);
            this.pnlProduct.Controls.Add(this.txtMrp);
            this.pnlProduct.Controls.Add(this.txtLoca);
            this.pnlProduct.Controls.Add(this.dataGridView1);
            this.pnlProduct.Controls.Add(this.checkBox1);
            this.pnlProduct.Controls.Add(this.cmbUnit);
            this.pnlProduct.Controls.Add(this.chkAutoCompleationProduct);
            this.pnlProduct.Controls.Add(this.txtNewCostPrice);
            this.pnlProduct.Controls.Add(this.chkUpdateCostPrice);
            this.pnlProduct.Controls.Add(this.pnlUpdateCostPrice);
            this.pnlProduct.Controls.Add(this.chkUpdateSellingPrice);
            this.pnlProduct.Controls.Add(this.pnlUpdateSellingPrice);
            this.pnlProduct.Controls.Add(this.txtSellingPrice);
            this.pnlProduct.Controls.Add(this.txtBatchNo);
            this.pnlProduct.Controls.Add(this.txtQty);
            this.pnlProduct.Controls.Add(this.txtProductName);
            this.pnlProduct.Controls.Add(this.txtCostPrice);
            this.pnlProduct.Controls.Add(this.txtProductCode);
            this.pnlProduct.Location = new System.Drawing.Point(3, 3);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(1143, 471);
            this.pnlProduct.TabIndex = 75;
            this.pnlProduct.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProduct_Paint);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AllowUserToDeleteRows = false;
            this.dgvItemDetails.AllowUserToOrderColumns = true;
            this.dgvItemDetails.AllowUserToResizeColumns = false;
            this.dgvItemDetails.AllowUserToResizeRows = false;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNo,
            this.ProductId,
            this.ProductCode,
            this.ProductName,
            this.UOMID,
            this.UnitOfMeasureName,
            this.BatchNo,
            this.Qty,
            this.MRP,
            this.SellingPrice,
            this.NewSellingPrice,
            this.CostPrice,
            this.NewCostPrice,
            this.LocationIdx,
            this.LocationNamex});
            this.dgvItemDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItemDetails.Location = new System.Drawing.Point(4, 47);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 15;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1134, 251);
            this.dgvItemDetails.TabIndex = 78;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // txtRecallDocNo
            // 
            this.txtRecallDocNo.Location = new System.Drawing.Point(547, 12);
            this.txtRecallDocNo.Name = "txtRecallDocNo";
            this.txtRecallDocNo.Size = new System.Drawing.Size(133, 21);
            this.txtRecallDocNo.TabIndex = 105;
            this.txtRecallDocNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecallDocNo_KeyDown);
            this.txtRecallDocNo.Leave += new System.EventHandler(this.txtRecallDocNo_Leave);
            // 
            // btnRecalEffective
            // 
            this.btnRecalEffective.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnRecalEffective.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecalEffective.ForeColor = System.Drawing.Color.Black;
            this.btnRecalEffective.Image = global::UI.Windows.Properties.Resources.kdevelop;
            this.btnRecalEffective.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecalEffective.Location = new System.Drawing.Point(459, 6);
            this.btnRecalEffective.Name = "btnRecalEffective";
            this.btnRecalEffective.Size = new System.Drawing.Size(82, 32);
            this.btnRecalEffective.TabIndex = 104;
            this.btnRecalEffective.Text = "&Recall";
            this.btnRecalEffective.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecalEffective.Click += new System.EventHandler(this.btnRecalEffective_Click);
            // 
            // chkPriceDecreas
            // 
            this.chkPriceDecreas.AutoSize = true;
            this.chkPriceDecreas.Location = new System.Drawing.Point(709, 15);
            this.chkPriceDecreas.Name = "chkPriceDecreas";
            this.chkPriceDecreas.Size = new System.Drawing.Size(105, 17);
            this.chkPriceDecreas.TabIndex = 103;
            this.chkPriceDecreas.Text = "Price Decreas";
            this.chkPriceDecreas.UseVisualStyleBackColor = true;
            this.chkPriceDecreas.CheckedChanged += new System.EventHandler(this.chkPriceDecreas_CheckedChanged);
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.Location = new System.Drawing.Point(1117, 14);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(15, 14);
            this.chkTStatus.TabIndex = 102;
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(910, 12);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(201, 21);
            this.txtReferenceNo.TabIndex = 100;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(820, 15);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 101;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpEffectivDate
            // 
            this.dtpEffectivDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectivDate.Location = new System.Drawing.Point(344, 13);
            this.dtpEffectivDate.Name = "dtpEffectivDate";
            this.dtpEffectivDate.Size = new System.Drawing.Size(109, 21);
            this.dtpEffectivDate.TabIndex = 73;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(251, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 72;
            this.label8.Text = "Effective Date";
            // 
            // chkAutoCompleationPoNo
            // 
            this.chkAutoCompleationPoNo.AutoSize = true;
            this.chkAutoCompleationPoNo.Checked = true;
            this.chkAutoCompleationPoNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPoNo.Location = new System.Drawing.Point(68, 19);
            this.chkAutoCompleationPoNo.Name = "chkAutoCompleationPoNo";
            this.chkAutoCompleationPoNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPoNo.TabIndex = 97;
            this.chkAutoCompleationPoNo.Tag = "1";
            this.chkAutoCompleationPoNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPoNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPoNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(85, 15);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(133, 21);
            this.txtDocumentNo.TabIndex = 98;
            this.txtDocumentNo.TextChanged += new System.EventHandler(this.txtDocumentNo_TextChanged);
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblPPChange
            // 
            this.lblPPChange.AutoSize = true;
            this.lblPPChange.Location = new System.Drawing.Point(16, 20);
            this.lblPPChange.Name = "lblPPChange";
            this.lblPPChange.Size = new System.Drawing.Size(42, 13);
            this.lblPPChange.TabIndex = 99;
            this.lblPPChange.Text = "PC No";
            // 
            // txtNewSellingPrice
            // 
            this.txtNewSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNewSellingPrice.Location = new System.Drawing.Point(696, 306);
            this.txtNewSellingPrice.Name = "txtNewSellingPrice";
            this.txtNewSellingPrice.Size = new System.Drawing.Size(96, 21);
            this.txtNewSellingPrice.TabIndex = 96;
            this.txtNewSellingPrice.Tag = "3";
            this.txtNewSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNewSellingPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewSellingPrice_KeyDown);
            this.txtNewSellingPrice.ImeModeChanged += new System.EventHandler(this.txtNewSellingPrice_ImeModeChanged);
            // 
            // txtMrp
            // 
            this.txtMrp.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMrp.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMrp.Location = new System.Drawing.Point(540, 371);
            this.txtMrp.Name = "txtMrp";
            this.txtMrp.Size = new System.Drawing.Size(17, 21);
            this.txtMrp.TabIndex = 95;
            this.txtMrp.Tag = "3";
            this.txtMrp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMrp.Visible = false;
            // 
            // txtLoca
            // 
            this.txtLoca.Location = new System.Drawing.Point(880, 306);
            this.txtLoca.Name = "txtLoca";
            this.txtLoca.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLoca.Size = new System.Drawing.Size(95, 21);
            this.txtLoca.TabIndex = 94;
            this.txtLoca.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn9});
            this.dataGridView1.Location = new System.Drawing.Point(806, 335);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(143, 106);
            this.dataGridView1.TabIndex = 93;
            this.dataGridView1.Visible = false;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Allow";
            this.dataGridViewCheckBoxColumn1.Frozen = true;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "LocationID";
            this.dataGridViewTextBoxColumn4.HeaderText = "LocationID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "LocationName";
            this.dataGridViewTextBoxColumn9.HeaderText = "Location";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(955, 335);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 92;
            this.checkBox1.Text = "All Locations";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(385, 306);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbUnit.TabIndex = 89;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 309);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 88;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // txtNewCostPrice
            // 
            this.txtNewCostPrice.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtNewCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNewCostPrice.Location = new System.Drawing.Point(1075, 309);
            this.txtNewCostPrice.Name = "txtNewCostPrice";
            this.txtNewCostPrice.Size = new System.Drawing.Size(10, 21);
            this.txtNewCostPrice.TabIndex = 86;
            this.txtNewCostPrice.Tag = "3";
            this.txtNewCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNewCostPrice.Visible = false;
            this.txtNewCostPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewCostPrice_KeyDown);
            // 
            // chkUpdateCostPrice
            // 
            this.chkUpdateCostPrice.AutoSize = true;
            this.chkUpdateCostPrice.Location = new System.Drawing.Point(18, 404);
            this.chkUpdateCostPrice.Name = "chkUpdateCostPrice";
            this.chkUpdateCostPrice.Size = new System.Drawing.Size(128, 17);
            this.chkUpdateCostPrice.TabIndex = 84;
            this.chkUpdateCostPrice.Text = "Update Cost Price";
            this.chkUpdateCostPrice.UseVisualStyleBackColor = true;
            this.chkUpdateCostPrice.Visible = false;
            this.chkUpdateCostPrice.CheckedChanged += new System.EventHandler(this.chkUpdateCostPrice_CheckedChanged);
            // 
            // pnlUpdateCostPrice
            // 
            this.pnlUpdateCostPrice.Controls.Add(this.chkIsIncrementCost);
            this.pnlUpdateCostPrice.Controls.Add(this.txtCostDiscount);
            this.pnlUpdateCostPrice.Controls.Add(this.btnUpdateCostPrice);
            this.pnlUpdateCostPrice.Controls.Add(this.label11);
            this.pnlUpdateCostPrice.Controls.Add(this.chkCostPercentage);
            this.pnlUpdateCostPrice.Controls.Add(this.label12);
            this.pnlUpdateCostPrice.Location = new System.Drawing.Point(8, 426);
            this.pnlUpdateCostPrice.Name = "pnlUpdateCostPrice";
            this.pnlUpdateCostPrice.Size = new System.Drawing.Size(396, 44);
            this.pnlUpdateCostPrice.TabIndex = 83;
            this.pnlUpdateCostPrice.Visible = false;
            // 
            // chkIsIncrementCost
            // 
            this.chkIsIncrementCost.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsIncrementCost.AutoSize = true;
            this.chkIsIncrementCost.BackColor = System.Drawing.Color.NavajoWhite;
            this.chkIsIncrementCost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkIsIncrementCost.Location = new System.Drawing.Point(5, 11);
            this.chkIsIncrementCost.Margin = new System.Windows.Forms.Padding(6);
            this.chkIsIncrementCost.Name = "chkIsIncrementCost";
            this.chkIsIncrementCost.Size = new System.Drawing.Size(80, 23);
            this.chkIsIncrementCost.TabIndex = 98;
            this.chkIsIncrementCost.Text = "Decrement";
            this.chkIsIncrementCost.UseVisualStyleBackColor = false;
            this.chkIsIncrementCost.CheckedChanged += new System.EventHandler(this.chkIsIncrementCost_CheckedChanged);
            // 
            // txtCostDiscount
            // 
            this.txtCostDiscount.Location = new System.Drawing.Point(130, 13);
            this.txtCostDiscount.Name = "txtCostDiscount";
            this.txtCostDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostDiscount.Size = new System.Drawing.Size(141, 21);
            this.txtCostDiscount.TabIndex = 97;
            // 
            // btnUpdateCostPrice
            // 
            this.btnUpdateCostPrice.Location = new System.Drawing.Point(280, 12);
            this.btnUpdateCostPrice.Name = "btnUpdateCostPrice";
            this.btnUpdateCostPrice.Size = new System.Drawing.Size(110, 23);
            this.btnUpdateCostPrice.TabIndex = 76;
            this.btnUpdateCostPrice.Text = "Update All Items";
            this.btnUpdateCostPrice.UseVisualStyleBackColor = true;
            this.btnUpdateCostPrice.Click += new System.EventHandler(this.btnUpdateCostPrice_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 75;
            this.label11.Text = "%";
            // 
            // chkCostPercentage
            // 
            this.chkCostPercentage.AutoSize = true;
            this.chkCostPercentage.Location = new System.Drawing.Point(109, 16);
            this.chkCostPercentage.Name = "chkCostPercentage";
            this.chkCostPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkCostPercentage.TabIndex = 74;
            this.chkCostPercentage.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Discount";
            // 
            // chkUpdateSellingPrice
            // 
            this.chkUpdateSellingPrice.AutoSize = true;
            this.chkUpdateSellingPrice.Location = new System.Drawing.Point(18, 335);
            this.chkUpdateSellingPrice.Name = "chkUpdateSellingPrice";
            this.chkUpdateSellingPrice.Size = new System.Drawing.Size(140, 17);
            this.chkUpdateSellingPrice.TabIndex = 82;
            this.chkUpdateSellingPrice.Text = "Update Selling Price";
            this.chkUpdateSellingPrice.UseVisualStyleBackColor = true;
            this.chkUpdateSellingPrice.CheckedChanged += new System.EventHandler(this.chkUpdateSellingPrice_CheckedChanged);
            // 
            // pnlUpdateSellingPrice
            // 
            this.pnlUpdateSellingPrice.Controls.Add(this.chkIsIncrementSelling);
            this.pnlUpdateSellingPrice.Controls.Add(this.txtSellingDiscount);
            this.pnlUpdateSellingPrice.Controls.Add(this.btnUpdateSellingPrice);
            this.pnlUpdateSellingPrice.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.pnlUpdateSellingPrice.Controls.Add(this.chkSellingPercentage);
            this.pnlUpdateSellingPrice.Controls.Add(this.lblSubTotalDiscount);
            this.pnlUpdateSellingPrice.Location = new System.Drawing.Point(8, 354);
            this.pnlUpdateSellingPrice.Name = "pnlUpdateSellingPrice";
            this.pnlUpdateSellingPrice.Size = new System.Drawing.Size(396, 44);
            this.pnlUpdateSellingPrice.TabIndex = 66;
            // 
            // chkIsIncrementSelling
            // 
            this.chkIsIncrementSelling.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsIncrementSelling.AutoSize = true;
            this.chkIsIncrementSelling.BackColor = System.Drawing.Color.NavajoWhite;
            this.chkIsIncrementSelling.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkIsIncrementSelling.Location = new System.Drawing.Point(5, 9);
            this.chkIsIncrementSelling.Margin = new System.Windows.Forms.Padding(6);
            this.chkIsIncrementSelling.Name = "chkIsIncrementSelling";
            this.chkIsIncrementSelling.Size = new System.Drawing.Size(80, 23);
            this.chkIsIncrementSelling.TabIndex = 96;
            this.chkIsIncrementSelling.Text = "Decrement";
            this.chkIsIncrementSelling.UseVisualStyleBackColor = false;
            this.chkIsIncrementSelling.CheckedChanged += new System.EventHandler(this.chkIsIncrementSelling_CheckedChanged);
            // 
            // txtSellingDiscount
            // 
            this.txtSellingDiscount.Location = new System.Drawing.Point(131, 11);
            this.txtSellingDiscount.Name = "txtSellingDiscount";
            this.txtSellingDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingDiscount.Size = new System.Drawing.Size(141, 21);
            this.txtSellingDiscount.TabIndex = 96;
            // 
            // btnUpdateSellingPrice
            // 
            this.btnUpdateSellingPrice.Location = new System.Drawing.Point(280, 11);
            this.btnUpdateSellingPrice.Name = "btnUpdateSellingPrice";
            this.btnUpdateSellingPrice.Size = new System.Drawing.Size(110, 23);
            this.btnUpdateSellingPrice.TabIndex = 76;
            this.btnUpdateSellingPrice.Text = "Update All Items";
            this.btnUpdateSellingPrice.UseVisualStyleBackColor = true;
            this.btnUpdateSellingPrice.Click += new System.EventHandler(this.btnUpdateItem_Click);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(88, 17);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 75;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // chkSellingPercentage
            // 
            this.chkSellingPercentage.AutoSize = true;
            this.chkSellingPercentage.Location = new System.Drawing.Point(110, 16);
            this.chkSellingPercentage.Name = "chkSellingPercentage";
            this.chkSellingPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSellingPercentage.TabIndex = 74;
            this.chkSellingPercentage.UseVisualStyleBackColor = true;
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(7, 16);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 72;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(610, 306);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(84, 21);
            this.txtSellingPrice.TabIndex = 80;
            this.txtSellingPrice.Tag = "3";
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(442, 306);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(115, 21);
            this.txtBatchNo.TabIndex = 74;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtQty.Location = new System.Drawing.Point(560, 306);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(48, 21);
            this.txtQty.TabIndex = 70;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(174, 306);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(208, 21);
            this.txtProductName.TabIndex = 68;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(796, 306);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(81, 21);
            this.txtCostPrice.TabIndex = 72;
            this.txtCostPrice.Tag = "3";
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(21, 306);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(151, 21);
            this.txtProductCode.TabIndex = 67;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(210, 453);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(73, 23);
            this.btnNext.TabIndex = 43;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(131, 453);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(73, 23);
            this.btnBack.TabIndex = 42;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LineNo
            // 
            this.LineNo.DataPropertyName = "LineNo";
            this.LineNo.HeaderText = "Row";
            this.LineNo.Name = "LineNo";
            this.LineNo.Width = 40;
            // 
            // ProductId
            // 
            this.ProductId.DataPropertyName = "ProductID";
            this.ProductId.HeaderText = "Product Id";
            this.ProductId.Name = "ProductId";
            this.ProductId.Visible = false;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 120;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 200;
            // 
            // UOMID
            // 
            this.UOMID.DataPropertyName = "UnitOfMeasureID";
            this.UOMID.HeaderText = "UOMID";
            this.UOMID.Name = "UOMID";
            this.UOMID.Visible = false;
            // 
            // UnitOfMeasureName
            // 
            this.UnitOfMeasureName.DataPropertyName = "UnitOfMeasureName";
            this.UnitOfMeasureName.HeaderText = "UOM";
            this.UnitOfMeasureName.Name = "UnitOfMeasureName";
            this.UnitOfMeasureName.ReadOnly = true;
            this.UnitOfMeasureName.Width = 55;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 120;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 50;
            // 
            // MRP
            // 
            this.MRP.DataPropertyName = "MRP";
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.Visible = false;
            this.MRP.Width = 90;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.SellingPrice.HeaderText = "Old Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Width = 90;
            // 
            // NewSellingPrice
            // 
            this.NewSellingPrice.DataPropertyName = "NewSellingPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NewSellingPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.NewSellingPrice.HeaderText = "New Selling";
            this.NewSellingPrice.Name = "NewSellingPrice";
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 90;
            // 
            // NewCostPrice
            // 
            this.NewCostPrice.DataPropertyName = "NewCostPrice";
            this.NewCostPrice.HeaderText = "New Cost";
            this.NewCostPrice.Name = "NewCostPrice";
            this.NewCostPrice.Visible = false;
            this.NewCostPrice.Width = 90;
            // 
            // LocationIdx
            // 
            this.LocationIdx.DataPropertyName = "LocationId";
            this.LocationIdx.HeaderText = "Location Id";
            this.LocationIdx.Name = "LocationIdx";
            this.LocationIdx.Visible = false;
            // 
            // LocationNamex
            // 
            this.LocationNamex.DataPropertyName = "LocationName";
            this.LocationNamex.HeaderText = "Location";
            this.LocationNamex.Name = "LocationNamex";
            this.LocationNamex.Width = 150;
            // 
            // FrmProductPriceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 557);
            this.Controls.Add(this.panel1);
            this.Name = "FrmProductPriceChange";
            this.Text = "Product Price Change";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlWizard.ResumeLayout(false);
            this.pnlLocationSelection.ResumeLayout(false);
            this.pnlLocationSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).EndInit();
            this.tabCritaria.ResumeLayout(false);
            this.tpDepartment.ResumeLayout(false);
            this.pnlDptSelection.ResumeLayout(false);
            this.pnlDptSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.tpCategory.ResumeLayout(false);
            this.pnlCategorySelection.ResumeLayout(false);
            this.pnlCategorySelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.tpSubCategory.ResumeLayout(false);
            this.pnlSubCategorySelection.ResumeLayout(false);
            this.pnlSubCategorySelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory)).EndInit();
            this.tpSubCategory2.ResumeLayout(false);
            this.pnlSubCategory2.ResumeLayout(false);
            this.pnlSubCategory2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory2)).EndInit();
            this.tpBatchNos.ResumeLayout(false);
            this.pnlBatchNos.ResumeLayout(false);
            this.pnlBatchNos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchnos)).EndInit();
            this.tpSupplier.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).EndInit();
            this.tpCritaria.ResumeLayout(false);
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlUpdateCostPrice.ResumeLayout(false);
            this.pnlUpdateCostPrice.PerformLayout();
            this.pnlUpdateSellingPrice.ResumeLayout(false);
            this.pnlUpdateSellingPrice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCritaria;
        private System.Windows.Forms.TabPage tpCritaria;
        private System.Windows.Forms.Button btnFinished;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TabPage tpDepartment;
        private System.Windows.Forms.TabPage tpCategory;
        private System.Windows.Forms.TabPage tpSubCategory;
        private System.Windows.Forms.TabPage tpSubCategory2;
        private System.Windows.Forms.TabPage tpBatchNos;
        private System.Windows.Forms.Panel pnlDptSelection;
        private System.Windows.Forms.CheckBox chkDepartment;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCategorySelection;
        private System.Windows.Forms.CheckBox chkCategory;
        private System.Windows.Forms.DataGridView dgvCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlSubCategorySelection;
        private System.Windows.Forms.CheckBox chkSubCategory;
        private System.Windows.Forms.DataGridView dgvSubCategory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SubCategoryAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvSubCategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlSubCategory2;
        private System.Windows.Forms.CheckBox chkSubCategory2;
        private System.Windows.Forms.DataGridView dgvSubCategory2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SubCategory2Allow;
        private System.Windows.Forms.DataGridViewTextBoxColumn invSubCategory2Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlBatchNos;
        private System.Windows.Forms.CheckBox chkBatchNo;
        private System.Windows.Forms.DataGridView dgvBatchnos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BatchNoAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNos;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchLocationId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CategoryAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.TabPage tpSupplier;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkAllSupplier;
        private System.Windows.Forms.DataGridView dgvSupplier;
        private System.Windows.Forms.DataGridViewCheckBoxColumn supAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlWizard;
        private System.Windows.Forms.Panel pnlLocationSelection;
        private System.Windows.Forms.DataGridView dgvLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAllLocation;
        private System.Windows.Forms.Button btnLoadItemOk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Allow;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationIDy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DepartmentAllow;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Panel pnlProduct;
        private System.Windows.Forms.CheckBox chkPriceDecreas;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpEffectivDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAutoCompleationPoNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPPChange;
        private CustomControls.TextBoxCurrency txtNewSellingPrice;
        private CustomControls.TextBoxCurrency txtMrp;
        private CustomControls.TextBoxNumeric txtLoca;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private CustomControls.TextBoxCurrency txtNewCostPrice;
        private System.Windows.Forms.CheckBox chkUpdateCostPrice;
        private System.Windows.Forms.Panel pnlUpdateCostPrice;
        private System.Windows.Forms.CheckBox chkIsIncrementCost;
        private CustomControls.TextBoxNumeric txtCostDiscount;
        private System.Windows.Forms.Button btnUpdateCostPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkCostPercentage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkUpdateSellingPrice;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.Panel pnlUpdateSellingPrice;
        private System.Windows.Forms.CheckBox chkIsIncrementSelling;
        private CustomControls.TextBoxNumeric txtSellingDiscount;
        private System.Windows.Forms.Button btnUpdateSellingPrice;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.CheckBox chkSellingPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.TextBox txtBatchNo;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private System.Windows.Forms.TextBox txtProductCode;
        protected Glass.GlassButton btnRecalEffective;
        private System.Windows.Forms.TextBox txtRecallDocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasureName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewSellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationNamex;
    }
}