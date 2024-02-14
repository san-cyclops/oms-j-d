namespace UI.Windows
{
    partial class FrmProductSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductSearch));
            this.btnSearch = new Glass.GlassButton();
            this.btnSearchProduct = new Glass.GlassButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new Glass.GlassButton();
            this.btnClear = new Glass.GlassButton();
            this.tabProduct = new System.Windows.Forms.TabControl();
            this.tbpGneral = new System.Windows.Forms.TabPage();
            this.dgvExistingProducts = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExtendedProperties = new System.Windows.Forms.DataGridView();
            this.ExtendedPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invProductExtendedPropertyValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.txtSubCategory2Code = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.txtSubCategory2Description = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.chkAutoCompleationSubCategory2 = new System.Windows.Forms.CheckBox();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubCategory2 = new System.Windows.Forms.Label();
            this.txtMainSupplierCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSubCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtMainSupplierDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtDepartmentDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.chkAutoCompleationMainSupplier = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationSubCategory = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.lblMainSupplier = new System.Windows.Forms.Label();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabProduct.SuspendLayout();
            this.tbpGneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtendedProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedPropertyValueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Image = global::UI.Windows.Properties.Resources.Mag;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(316, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 55;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSearchProduct.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchProduct.ForeColor = System.Drawing.Color.Black;
            this.btnSearchProduct.Image = global::UI.Windows.Properties.Resources.Mag;
            this.btnSearchProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchProduct.Location = new System.Drawing.Point(6, 11);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(75, 32);
            this.btnSearchProduct.TabIndex = 56;
            this.btnSearchProduct.Text = "&Search";
            this.btnSearchProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchProduct);
            this.groupBox1.Location = new System.Drawing.Point(644, 424);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 46);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.GlowColor = System.Drawing.Color.Blue;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(167, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.GlowColor = System.Drawing.Color.Blue;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(86, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 57;
            this.btnClear.Text = "Cl&ear ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tabProduct
            // 
            this.tabProduct.Controls.Add(this.tbpGneral);
            this.tabProduct.Location = new System.Drawing.Point(5, 4);
            this.tabProduct.Multiline = true;
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.SelectedIndex = 0;
            this.tabProduct.Size = new System.Drawing.Size(889, 425);
            this.tabProduct.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabProduct.TabIndex = 58;
            // 
            // tbpGneral
            // 
            this.tbpGneral.BackColor = System.Drawing.SystemColors.Control;
            this.tbpGneral.Controls.Add(this.txtPropertyValue);
            this.tbpGneral.Controls.Add(this.txtPropertyName);
            this.tbpGneral.Controls.Add(this.dgvExistingProducts);
            this.tbpGneral.Controls.Add(this.dgvExtendedProperties);
            this.tbpGneral.Controls.Add(this.lblSellingPrice);
            this.tbpGneral.Controls.Add(this.txtSubCategory2Code);
            this.tbpGneral.Controls.Add(this.txtSellingPrice);
            this.tbpGneral.Controls.Add(this.lblCostPrice);
            this.tbpGneral.Controls.Add(this.txtSubCategory2Description);
            this.tbpGneral.Controls.Add(this.chkAutoCompleationSubCategory2);
            this.tbpGneral.Controls.Add(this.txtCostPrice);
            this.tbpGneral.Controls.Add(this.lblSubCategory2);
            this.tbpGneral.Controls.Add(this.txtMainSupplierCode);
            this.tbpGneral.Controls.Add(this.txtSubCategoryCode);
            this.tbpGneral.Controls.Add(this.txtCategoryCode);
            this.tbpGneral.Controls.Add(this.txtDepartmentCode);
            this.tbpGneral.Controls.Add(this.txtMainSupplierDescription);
            this.tbpGneral.Controls.Add(this.txtSubCategoryDescription);
            this.tbpGneral.Controls.Add(this.txtCategoryDescription);
            this.tbpGneral.Controls.Add(this.txtDepartmentDescription);
            this.tbpGneral.Controls.Add(this.chkAutoCompleationMainSupplier);
            this.tbpGneral.Controls.Add(this.chkAutoCompleationSubCategory);
            this.tbpGneral.Controls.Add(this.chkAutoCompleationCategory);
            this.tbpGneral.Controls.Add(this.chkAutoCompleationDepartment);
            this.tbpGneral.Controls.Add(this.lblMainSupplier);
            this.tbpGneral.Controls.Add(this.lblSubCategory);
            this.tbpGneral.Controls.Add(this.lblCategory);
            this.tbpGneral.Controls.Add(this.lblDepartment);
            this.tbpGneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGneral.Name = "tbpGneral";
            this.tbpGneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGneral.Size = new System.Drawing.Size(881, 399);
            this.tbpGneral.TabIndex = 0;
            this.tbpGneral.Text = "General";
            // 
            // dgvExistingProducts
            // 
            this.dgvExistingProducts.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvExistingProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExistingProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProductName});
            this.dgvExistingProducts.Location = new System.Drawing.Point(11, 174);
            this.dgvExistingProducts.Name = "dgvExistingProducts";
            this.dgvExistingProducts.Size = new System.Drawing.Size(439, 218);
            this.dgvExistingProducts.TabIndex = 47;
            this.dgvExistingProducts.DoubleClick += new System.EventHandler(this.dgvExistingProducts_DoubleClick);
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 156;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 220;
            // 
            // dgvExtendedProperties
            // 
            this.dgvExtendedProperties.AutoGenerateColumns = false;
            this.dgvExtendedProperties.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvExtendedProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtendedProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExtendedPropertyName,
            this.ValueData});
            this.dgvExtendedProperties.DataSource = this.invProductExtendedPropertyValueBindingSource;
            this.dgvExtendedProperties.Location = new System.Drawing.Point(458, 12);
            this.dgvExtendedProperties.Name = "dgvExtendedProperties";
            this.dgvExtendedProperties.Size = new System.Drawing.Size(417, 353);
            this.dgvExtendedProperties.TabIndex = 46;
            // 
            // ExtendedPropertyName
            // 
            this.ExtendedPropertyName.DataPropertyName = "ExtendedPropertyName";
            this.ExtendedPropertyName.HeaderText = "Property Name";
            this.ExtendedPropertyName.Name = "ExtendedPropertyName";
            this.ExtendedPropertyName.ReadOnly = true;
            this.ExtendedPropertyName.Width = 200;
            // 
            // ValueData
            // 
            this.ValueData.DataPropertyName = "ValueData";
            this.ValueData.HeaderText = "Property Value";
            this.ValueData.Name = "ValueData";
            this.ValueData.Width = 150;
            // 
            // invProductExtendedPropertyValueBindingSource
            // 
            this.invProductExtendedPropertyValueBindingSource.DataSource = typeof(Domain.InvProductExtendedPropertyValue);
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Location = new System.Drawing.Point(223, 146);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(77, 13);
            this.lblSellingPrice.TabIndex = 30;
            this.lblSellingPrice.Text = "Selling Price";
            // 
            // txtSubCategory2Code
            // 
            this.txtSubCategory2Code.IsAutoComplete = false;
            this.txtSubCategory2Code.ItemCollection = null;
            this.txtSubCategory2Code.Location = new System.Drawing.Point(112, 89);
            this.txtSubCategory2Code.MasterCode = "";
            this.txtSubCategory2Code.Name = "txtSubCategory2Code";
            this.txtSubCategory2Code.Size = new System.Drawing.Size(105, 21);
            this.txtSubCategory2Code.TabIndex = 14;
            this.txtSubCategory2Code.Tag = "3";
            this.txtSubCategory2Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Code_KeyDown);
            this.txtSubCategory2Code.Leave += new System.EventHandler(this.txtSubCategory2Code_Leave);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(327, 143);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(125, 21);
            this.txtSellingPrice.TabIndex = 21;
            this.txtSellingPrice.Tag = "3";
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Location = new System.Drawing.Point(8, 146);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(65, 13);
            this.lblCostPrice.TabIndex = 29;
            this.lblCostPrice.Text = "Cost Price";
            // 
            // txtSubCategory2Description
            // 
            this.txtSubCategory2Description.Location = new System.Drawing.Point(222, 89);
            this.txtSubCategory2Description.MasterDescription = "";
            this.txtSubCategory2Description.Name = "txtSubCategory2Description";
            this.txtSubCategory2Description.Size = new System.Drawing.Size(230, 21);
            this.txtSubCategory2Description.TabIndex = 15;
            this.txtSubCategory2Description.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Description_KeyDown);
            this.txtSubCategory2Description.Leave += new System.EventHandler(this.txtSubCategory2Description_Leave);
            // 
            // chkAutoCompleationSubCategory2
            // 
            this.chkAutoCompleationSubCategory2.AutoSize = true;
            this.chkAutoCompleationSubCategory2.Checked = true;
            this.chkAutoCompleationSubCategory2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory2.Location = new System.Drawing.Point(95, 92);
            this.chkAutoCompleationSubCategory2.Name = "chkAutoCompleationSubCategory2";
            this.chkAutoCompleationSubCategory2.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory2.TabIndex = 14;
            this.chkAutoCompleationSubCategory2.Tag = "1";
            this.chkAutoCompleationSubCategory2.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSubCategory2.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSubCategory2_CheckedChanged);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(112, 143);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(105, 21);
            this.txtCostPrice.TabIndex = 20;
            this.txtCostPrice.Tag = "3";
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostPrice_KeyDown);
            this.txtCostPrice.Leave += new System.EventHandler(this.txtCostPrice_Leave);
            // 
            // lblSubCategory2
            // 
            this.lblSubCategory2.AutoSize = true;
            this.lblSubCategory2.Location = new System.Drawing.Point(8, 92);
            this.lblSubCategory2.Name = "lblSubCategory2";
            this.lblSubCategory2.Size = new System.Drawing.Size(86, 13);
            this.lblSubCategory2.TabIndex = 45;
            this.lblSubCategory2.Text = "Sub Category";
            // 
            // txtMainSupplierCode
            // 
            this.txtMainSupplierCode.IsAutoComplete = false;
            this.txtMainSupplierCode.ItemCollection = null;
            this.txtMainSupplierCode.Location = new System.Drawing.Point(112, 116);
            this.txtMainSupplierCode.MasterCode = "";
            this.txtMainSupplierCode.Name = "txtMainSupplierCode";
            this.txtMainSupplierCode.Size = new System.Drawing.Size(105, 21);
            this.txtMainSupplierCode.TabIndex = 16;
            this.txtMainSupplierCode.Tag = "3";
            this.txtMainSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMainSupplierCode_KeyDown);
            this.txtMainSupplierCode.Validated += new System.EventHandler(this.txtMainSupplierCode_Validated);
            // 
            // txtSubCategoryCode
            // 
            this.txtSubCategoryCode.IsAutoComplete = false;
            this.txtSubCategoryCode.ItemCollection = null;
            this.txtSubCategoryCode.Location = new System.Drawing.Point(112, 62);
            this.txtSubCategoryCode.MasterCode = "";
            this.txtSubCategoryCode.Name = "txtSubCategoryCode";
            this.txtSubCategoryCode.Size = new System.Drawing.Size(105, 21);
            this.txtSubCategoryCode.TabIndex = 12;
            this.txtSubCategoryCode.Tag = "3";
            this.txtSubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryCode_KeyDown);
            this.txtSubCategoryCode.Leave += new System.EventHandler(this.txtSubCategoryCode_Leave);
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.IsAutoComplete = false;
            this.txtCategoryCode.ItemCollection = null;
            this.txtCategoryCode.Location = new System.Drawing.Point(112, 37);
            this.txtCategoryCode.MasterCode = "";
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.Size = new System.Drawing.Size(105, 21);
            this.txtCategoryCode.TabIndex = 10;
            this.txtCategoryCode.Tag = "3";
            this.txtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryCode_KeyDown);
            this.txtCategoryCode.Leave += new System.EventHandler(this.txtCategoryCode_Leave);
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.IsAutoComplete = false;
            this.txtDepartmentCode.ItemCollection = null;
            this.txtDepartmentCode.Location = new System.Drawing.Point(112, 12);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(105, 21);
            this.txtDepartmentCode.TabIndex = 8;
            this.txtDepartmentCode.Tag = "3";
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Leave += new System.EventHandler(this.txtDepartmentCode_Leave);
            // 
            // txtMainSupplierDescription
            // 
            this.txtMainSupplierDescription.Location = new System.Drawing.Point(222, 116);
            this.txtMainSupplierDescription.MasterDescription = "";
            this.txtMainSupplierDescription.Name = "txtMainSupplierDescription";
            this.txtMainSupplierDescription.Size = new System.Drawing.Size(230, 21);
            this.txtMainSupplierDescription.TabIndex = 17;
            this.txtMainSupplierDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMainSupplierDescription_KeyDown);
            this.txtMainSupplierDescription.Leave += new System.EventHandler(this.txtMainSupplierDescription_Leave);
            // 
            // txtSubCategoryDescription
            // 
            this.txtSubCategoryDescription.Location = new System.Drawing.Point(222, 62);
            this.txtSubCategoryDescription.MasterDescription = "";
            this.txtSubCategoryDescription.Name = "txtSubCategoryDescription";
            this.txtSubCategoryDescription.Size = new System.Drawing.Size(230, 21);
            this.txtSubCategoryDescription.TabIndex = 13;
            this.txtSubCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryDescription_KeyDown);
            this.txtSubCategoryDescription.Leave += new System.EventHandler(this.txtSubCategoryDescription_Leave);
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Location = new System.Drawing.Point(222, 37);
            this.txtCategoryDescription.MasterDescription = "";
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(230, 21);
            this.txtCategoryDescription.TabIndex = 11;
            this.txtCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryDescription_KeyDown);
            this.txtCategoryDescription.Leave += new System.EventHandler(this.txtCategoryDescription_Leave);
            // 
            // txtDepartmentDescription
            // 
            this.txtDepartmentDescription.Location = new System.Drawing.Point(222, 12);
            this.txtDepartmentDescription.MasterDescription = "";
            this.txtDepartmentDescription.Name = "txtDepartmentDescription";
            this.txtDepartmentDescription.Size = new System.Drawing.Size(230, 21);
            this.txtDepartmentDescription.TabIndex = 9;
            this.txtDepartmentDescription.Tag = "";
            this.txtDepartmentDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentDescription_KeyDown);
            this.txtDepartmentDescription.Leave += new System.EventHandler(this.txtDepartmentDescription_Leave);
            // 
            // chkAutoCompleationMainSupplier
            // 
            this.chkAutoCompleationMainSupplier.AutoSize = true;
            this.chkAutoCompleationMainSupplier.Checked = true;
            this.chkAutoCompleationMainSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationMainSupplier.Location = new System.Drawing.Point(95, 119);
            this.chkAutoCompleationMainSupplier.Name = "chkAutoCompleationMainSupplier";
            this.chkAutoCompleationMainSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationMainSupplier.TabIndex = 31;
            this.chkAutoCompleationMainSupplier.Tag = "1";
            this.chkAutoCompleationMainSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationMainSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationMainSupplier_CheckedChanged);
            // 
            // chkAutoCompleationSubCategory
            // 
            this.chkAutoCompleationSubCategory.AutoSize = true;
            this.chkAutoCompleationSubCategory.Checked = true;
            this.chkAutoCompleationSubCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory.Location = new System.Drawing.Point(95, 65);
            this.chkAutoCompleationSubCategory.Name = "chkAutoCompleationSubCategory";
            this.chkAutoCompleationSubCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory.TabIndex = 30;
            this.chkAutoCompleationSubCategory.Tag = "1";
            this.chkAutoCompleationSubCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSubCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSubCategory_CheckedChanged);
            // 
            // chkAutoCompleationCategory
            // 
            this.chkAutoCompleationCategory.AutoSize = true;
            this.chkAutoCompleationCategory.Checked = true;
            this.chkAutoCompleationCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCategory.Location = new System.Drawing.Point(95, 40);
            this.chkAutoCompleationCategory.Name = "chkAutoCompleationCategory";
            this.chkAutoCompleationCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCategory.TabIndex = 29;
            this.chkAutoCompleationCategory.Tag = "1";
            this.chkAutoCompleationCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCategory_CheckedChanged);
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(95, 15);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 28;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDepartment_CheckedChanged);
            // 
            // lblMainSupplier
            // 
            this.lblMainSupplier.AutoSize = true;
            this.lblMainSupplier.Location = new System.Drawing.Point(8, 119);
            this.lblMainSupplier.Name = "lblMainSupplier";
            this.lblMainSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblMainSupplier.TabIndex = 27;
            this.lblMainSupplier.Text = "Main Supplier";
            // 
            // lblSubCategory
            // 
            this.lblSubCategory.AutoSize = true;
            this.lblSubCategory.Location = new System.Drawing.Point(8, 65);
            this.lblSubCategory.Name = "lblSubCategory";
            this.lblSubCategory.Size = new System.Drawing.Size(86, 13);
            this.lblSubCategory.TabIndex = 18;
            this.lblSubCategory.Text = "Sub Category";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(8, 40);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 13);
            this.lblCategory.TabIndex = 17;
            this.lblCategory.Text = "Category";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(8, 15);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(75, 13);
            this.lblDepartment.TabIndex = 16;
            this.lblDepartment.Text = "Department";
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.Location = new System.Drawing.Point(685, 371);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(190, 21);
            this.txtPropertyValue.TabIndex = 49;
            this.txtPropertyValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyValue_KeyDown);
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(458, 371);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(225, 21);
            this.txtPropertyName.TabIndex = 48;
            this.txtPropertyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyName_KeyDown);
            // 
            // FrmProductSearch
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(895, 473);
            this.ControlBox = false;
            this.Controls.Add(this.tabProduct);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmProductSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Search";
            this.Load += new System.EventHandler(this.FrmProductSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabProduct.ResumeLayout(false);
            this.tbpGneral.ResumeLayout(false);
            this.tbpGneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtendedProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedPropertyValueBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource invProductExtendedPropertyValueBindingSource;
        protected Glass.GlassButton btnSearch;
        protected Glass.GlassButton btnSearchProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        protected Glass.GlassButton btnClear;
        private System.Windows.Forms.TabControl tabProduct;
        private System.Windows.Forms.TabPage tbpGneral;
        private System.Windows.Forms.DataGridView dgvExistingProducts;
        private System.Windows.Forms.DataGridView dgvExtendedProperties;
        private System.Windows.Forms.Label lblSellingPrice;
        private CustomControls.TextBoxMasterCode txtSubCategory2Code;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.Label lblCostPrice;
        private CustomControls.TextBoxMasterDescription txtSubCategory2Description;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory2;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private System.Windows.Forms.Label lblSubCategory2;
        private CustomControls.TextBoxMasterCode txtMainSupplierCode;
        private CustomControls.TextBoxMasterCode txtSubCategoryCode;
        private CustomControls.TextBoxMasterCode txtCategoryCode;
        private CustomControls.TextBoxMasterCode txtDepartmentCode;
        private CustomControls.TextBoxMasterDescription txtMainSupplierDescription;
        private CustomControls.TextBoxMasterDescription txtSubCategoryDescription;
        private CustomControls.TextBoxMasterDescription txtCategoryDescription;
        private CustomControls.TextBoxMasterDescription txtDepartmentDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationMainSupplier;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.Label lblMainSupplier;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDepartment;
        protected Glass.GlassButton btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExtendedPropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.TextBox txtPropertyValue;
        private System.Windows.Forms.TextBox txtPropertyName;
    }
}
