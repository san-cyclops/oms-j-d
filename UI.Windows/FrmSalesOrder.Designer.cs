namespace UI.Windows
{
    partial class FrmSalesOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesOrder));
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.chkRupee = new System.Windows.Forms.CheckBox();
            this.dtpDeliver = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpSalesOrderDate = new System.Windows.Forms.DateTimePicker();
            this.btnInvoiceDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSalesPerson = new System.Windows.Forms.Label();
            this.txtSalesPersonName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSalesPerson = new System.Windows.Forms.CheckBox();
            this.txtSalesPersonCode = new System.Windows.Forms.TextBox();
            this.lblVendor = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblSalesOrderDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationSalesOrderNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblQuotationNo = new System.Windows.Forms.Label();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalTaxAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGauge = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gauge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPersentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscountAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoofColor = new System.Windows.Forms.TextBox();
            this.txtSubCategory2Code = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSubCategory2Description = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.chkAutoCompleationSubCategory2 = new System.Windows.Forms.CheckBox();
            this.lblSubCategory2 = new System.Windows.Forms.Label();
            this.txtSubCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSubCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtDepartmentDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.chkAutoCompleationSubCategory = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCompanyLocation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.CmbUnit1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNetAmount1 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.btnDelete = new Glass.GlassButton();
            this.txtExchangeRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label6 = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(731, 506);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Controls.Add(this.btnDelete);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 506);
            this.grpButtonSet.Size = new System.Drawing.Size(254, 46);
            this.grpButtonSet.Controls.SetChildIndex(this.btnHelp, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnView, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnDelete, 0);
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 29;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(171, 11);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(93, 11);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtExchangeRate);
            this.grpHeader.Controls.Add(this.label6);
            this.grpHeader.Controls.Add(this.chkRupee);
            this.grpHeader.Controls.Add(this.dtpDeliver);
            this.grpHeader.Controls.Add(this.label3);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpSalesOrderDate);
            this.grpHeader.Controls.Add(this.btnInvoiceDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonCode);
            this.grpHeader.Controls.Add(this.lblVendor);
            this.grpHeader.Controls.Add(this.txtCustomerName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerCode);
            this.grpHeader.Controls.Add(this.lblSalesOrderDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSalesOrderNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblQuotationNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1020, 110);
            this.grpHeader.TabIndex = 16;
            this.grpHeader.TabStop = false;
            // 
            // chkRupee
            // 
            this.chkRupee.AutoSize = true;
            this.chkRupee.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRupee.Location = new System.Drawing.Point(951, 86);
            this.chkRupee.Name = "chkRupee";
            this.chkRupee.Size = new System.Drawing.Size(62, 17);
            this.chkRupee.TabIndex = 105;
            this.chkRupee.Tag = "1";
            this.chkRupee.Text = "Rupee";
            this.chkRupee.UseVisualStyleBackColor = true;
            // 
            // dtpDeliver
            // 
            this.dtpDeliver.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliver.Location = new System.Drawing.Point(798, 33);
            this.dtpDeliver.Name = "dtpDeliver";
            this.dtpDeliver.Size = new System.Drawing.Size(215, 21);
            this.dtpDeliver.TabIndex = 9;
            this.dtpDeliver.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDeliver_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(689, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Deliver Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(798, 60);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(215, 21);
            this.cmbLocation.TabIndex = 10;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(689, 60);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 64;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(378, 13);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(215, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(289, 15);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpSalesOrderDate
            // 
            this.dtpSalesOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSalesOrderDate.Location = new System.Drawing.Point(798, 12);
            this.dtpSalesOrderDate.Name = "dtpSalesOrderDate";
            this.dtpSalesOrderDate.Size = new System.Drawing.Size(215, 21);
            this.dtpSalesOrderDate.TabIndex = 8;
            this.dtpSalesOrderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpSalesOrderDate_KeyDown);
            // 
            // btnInvoiceDetails
            // 
            this.btnInvoiceDetails.Location = new System.Drawing.Point(255, 11);
            this.btnInvoiceDetails.Name = "btnInvoiceDetails";
            this.btnInvoiceDetails.Size = new System.Drawing.Size(28, 23);
            this.btnInvoiceDetails.TabIndex = 21;
            this.btnInvoiceDetails.Text = "...";
            this.btnInvoiceDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 87);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(98, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Deliver Address";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(118, 84);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(475, 21);
            this.txtRemark.TabIndex = 6;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblSalesPerson
            // 
            this.lblSalesPerson.AutoSize = true;
            this.lblSalesPerson.Location = new System.Drawing.Point(6, 63);
            this.lblSalesPerson.Name = "lblSalesPerson";
            this.lblSalesPerson.Size = new System.Drawing.Size(81, 13);
            this.lblSalesPerson.TabIndex = 17;
            this.lblSalesPerson.Text = "Sales Person";
            // 
            // txtSalesPersonName
            // 
            this.txtSalesPersonName.Location = new System.Drawing.Point(255, 60);
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(338, 21);
            this.txtSalesPersonName.TabIndex = 5;
            this.txtSalesPersonName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonName_KeyDown);
            this.txtSalesPersonName.Leave += new System.EventHandler(this.txtSalesPersonName_Leave);
            // 
            // chkAutoCompleationSalesPerson
            // 
            this.chkAutoCompleationSalesPerson.AutoSize = true;
            this.chkAutoCompleationSalesPerson.Checked = true;
            this.chkAutoCompleationSalesPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(101, 63);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 15;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Location = new System.Drawing.Point(118, 60);
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(136, 21);
            this.txtSalesPersonCode.TabIndex = 4;
            this.txtSalesPersonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonCode_KeyDown);
            this.txtSalesPersonCode.Leave += new System.EventHandler(this.txtSalesPersonCode_Leave);
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(6, 39);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(63, 13);
            this.lblVendor.TabIndex = 13;
            this.lblVendor.Text = "Customer";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(255, 36);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(338, 21);
            this.txtCustomerName.TabIndex = 3;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(101, 39);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 11;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(118, 36);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 2;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // lblSalesOrderDate
            // 
            this.lblSalesOrderDate.AutoSize = true;
            this.lblSalesOrderDate.Location = new System.Drawing.Point(689, 16);
            this.lblSalesOrderDate.Name = "lblSalesOrderDate";
            this.lblSalesOrderDate.Size = new System.Drawing.Size(106, 13);
            this.lblSalesOrderDate.TabIndex = 9;
            this.lblSalesOrderDate.Text = "Sales Order Date";
            // 
            // chkAutoCompleationSalesOrderNo
            // 
            this.chkAutoCompleationSalesOrderNo.AutoSize = true;
            this.chkAutoCompleationSalesOrderNo.Checked = true;
            this.chkAutoCompleationSalesOrderNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesOrderNo.Location = new System.Drawing.Point(100, 16);
            this.chkAutoCompleationSalesOrderNo.Name = "chkAutoCompleationSalesOrderNo";
            this.chkAutoCompleationSalesOrderNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesOrderNo.TabIndex = 4;
            this.chkAutoCompleationSalesOrderNo.Tag = "1";
            this.chkAutoCompleationSalesOrderNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesOrderNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesOrderNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(118, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblQuotationNo
            // 
            this.lblQuotationNo.AutoSize = true;
            this.lblQuotationNo.Location = new System.Drawing.Point(6, 16);
            this.lblQuotationNo.Name = "lblQuotationNo";
            this.lblQuotationNo.Size = new System.Drawing.Size(94, 13);
            this.lblQuotationNo.TabIndex = 0;
            this.lblQuotationNo.Text = "Sales Order No";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(914, 93);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 76;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.Visible = false;
            this.chkOverwrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkOverwrite_KeyDown);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.chkAutoCompleationProduct);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(1051, 124);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(116, 75);
            this.grpFooter.TabIndex = 17;
            this.grpFooter.TabStop = false;
            this.grpFooter.Visible = false;
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(719, 84);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 89;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(853, 88);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 88;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(739, 112);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 57;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.Window;
            this.txtOtherCharges.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharges.Location = new System.Drawing.Point(878, 109);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(136, 21);
            this.txtOtherCharges.TabIndex = 56;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(831, 40);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(738, 136);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(738, 88);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 39;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(853, 40);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            this.chkSubTotalDiscountPercentage.CheckedChanged += new System.EventHandler(this.chkSubTotalDiscountPercentage_CheckedChanged);
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(878, 37);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(136, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 37;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(739, 40);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(29, 20);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 50;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(739, 16);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount.Location = new System.Drawing.Point(878, 133);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(136, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(878, 85);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(136, 21);
            this.txtTotalTaxAmount.TabIndex = 32;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalTaxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalTaxAmount_KeyUp);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(878, 61);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(136, 21);
            this.txtSubTotalDiscount.TabIndex = 31;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGrossAmount.Location = new System.Drawing.Point(878, 13);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(136, 21);
            this.txtGrossAmount.TabIndex = 30;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGauge);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Controls.Add(this.txtProductAmount);
            this.groupBox1.Controls.Add(this.txtProductDiscountAmount);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.grpFooter);
            this.groupBox1.Location = new System.Drawing.Point(2, 234);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1044, 231);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // txtGauge
            // 
            this.txtGauge.Location = new System.Drawing.Point(621, 201);
            this.txtGauge.Name = "txtGauge";
            this.txtGauge.Size = new System.Drawing.Size(113, 21);
            this.txtGauge.TabIndex = 26;
            this.txtGauge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGauge_KeyDown);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Enabled = false;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(869, 221);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(17, 21);
            this.cmbUnit.TabIndex = 59;
            this.cmbUnit.Visible = false;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(496, 201);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(122, 21);
            this.txtSize.TabIndex = 25;
            this.txtSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSize_KeyDown);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.Size,
            this.Gauge,
            this.Qty,
            this.DiscountPersentage,
            this.Discount,
            this.Price,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(4, 13);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 20;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1040, 186);
            this.dgvItemDetails.TabIndex = 60;
            this.dgvItemDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellClick);
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // Row
            // 
            this.Row.DataPropertyName = "LineNo";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Row.DefaultCellStyle = dataGridViewCellStyle11;
            this.Row.Frozen = true;
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle12;
            this.ProductCode.Frozen = true;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 170;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle13;
            this.ProductName.Frozen = true;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 265;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle14;
            this.Unit.Frozen = true;
            this.Unit.HeaderText = "Unit";
            this.Unit.MaxInputLength = 0;
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Visible = false;
            this.Unit.Width = 5;
            // 
            // Size
            // 
            this.Size.DataPropertyName = "Size";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Size.DefaultCellStyle = dataGridViewCellStyle15;
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Width = 120;
            // 
            // Gauge
            // 
            this.Gauge.DataPropertyName = "Gauge";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Gauge.DefaultCellStyle = dataGridViewCellStyle16;
            this.Gauge.HeaderText = "Gauge";
            this.Gauge.Name = "Gauge";
            this.Gauge.Width = 120;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle17;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 66;
            // 
            // DiscountPersentage
            // 
            this.DiscountPersentage.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPersentage.DefaultCellStyle = dataGridViewCellStyle18;
            this.DiscountPersentage.HeaderText = "Dis%";
            this.DiscountPersentage.Name = "DiscountPersentage";
            this.DiscountPersentage.ReadOnly = true;
            this.DiscountPersentage.Visible = false;
            this.DiscountPersentage.Width = 52;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Discount.DefaultCellStyle = dataGridViewCellStyle19;
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.Visible = false;
            this.Discount.Width = 70;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "SellingPrice";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.Width = 110;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle20;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 120;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Enabled = false;
            this.txtProductAmount.Location = new System.Drawing.Point(918, 201);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.Size = new System.Drawing.Size(126, 21);
            this.txtProductAmount.TabIndex = 52;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            this.txtProductAmount.Leave += new System.EventHandler(this.txtProductAmount_Leave);
            // 
            // txtProductDiscountAmount
            // 
            this.txtProductDiscountAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountAmount.Enabled = false;
            this.txtProductDiscountAmount.Location = new System.Drawing.Point(932, 199);
            this.txtProductDiscountAmount.Name = "txtProductDiscountAmount";
            this.txtProductDiscountAmount.Size = new System.Drawing.Size(10, 21);
            this.txtProductDiscountAmount.TabIndex = 53;
            this.txtProductDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountAmount.Visible = false;
            this.txtProductDiscountAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountAmount_KeyDown);
            this.txtProductDiscountAmount.Leave += new System.EventHandler(this.txtProductDiscountAmount_Leave);
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtRate.Enabled = false;
            this.txtRate.Location = new System.Drawing.Point(807, 201);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(107, 21);
            this.txtRate.TabIndex = 28;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(738, 201);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(66, 21);
            this.txtQty.TabIndex = 27;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(236, 201);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(258, 21);
            this.txtProductName.TabIndex = 24;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(4, 201);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(231, 21);
            this.txtProductCode.TabIndex = 23;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNoofColor);
            this.groupBox2.Controls.Add(this.txtSubCategory2Code);
            this.groupBox2.Controls.Add(this.chkOverwrite);
            this.groupBox2.Controls.Add(this.txtSubCategory2Description);
            this.groupBox2.Controls.Add(this.chkAutoCompleationSubCategory2);
            this.groupBox2.Controls.Add(this.lblSubCategory2);
            this.groupBox2.Controls.Add(this.txtSubCategoryCode);
            this.groupBox2.Controls.Add(this.txtCategoryCode);
            this.groupBox2.Controls.Add(this.txtDepartmentCode);
            this.groupBox2.Controls.Add(this.txtSubCategoryDescription);
            this.groupBox2.Controls.Add(this.txtCategoryDescription);
            this.groupBox2.Controls.Add(this.txtDepartmentDescription);
            this.groupBox2.Controls.Add(this.chkAutoCompleationSubCategory);
            this.groupBox2.Controls.Add(this.chkAutoCompleationCategory);
            this.groupBox2.Controls.Add(this.chkAutoCompleationDepartment);
            this.groupBox2.Controls.Add(this.lblSubCategory);
            this.groupBox2.Controls.Add(this.lblCategory);
            this.groupBox2.Controls.Add(this.lblDepartment);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbCompanyLocation);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbCurrency);
            this.groupBox2.Controls.Add(this.lblUnit);
            this.groupBox2.Controls.Add(this.CmbUnit1);
            this.groupBox2.Location = new System.Drawing.Point(2, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1020, 128);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 104;
            this.label4.Text = "No Of Colour";
            // 
            // txtNoofColor
            // 
            this.txtNoofColor.Location = new System.Drawing.Point(118, 95);
            this.txtNoofColor.Name = "txtNoofColor";
            this.txtNoofColor.Size = new System.Drawing.Size(156, 21);
            this.txtNoofColor.TabIndex = 14;
            this.txtNoofColor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoofColor_KeyDown);
            // 
            // txtSubCategory2Code
            // 
            this.txtSubCategory2Code.IsAutoComplete = false;
            this.txtSubCategory2Code.ItemCollection = null;
            this.txtSubCategory2Code.Location = new System.Drawing.Point(427, 91);
            this.txtSubCategory2Code.MasterCode = "";
            this.txtSubCategory2Code.Name = "txtSubCategory2Code";
            this.txtSubCategory2Code.Size = new System.Drawing.Size(109, 21);
            this.txtSubCategory2Code.TabIndex = 21;
            this.txtSubCategory2Code.Tag = "3";
            this.txtSubCategory2Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Code_KeyDown);
            this.txtSubCategory2Code.Leave += new System.EventHandler(this.txtSubCategory2Code_Leave);
            // 
            // txtSubCategory2Description
            // 
            this.txtSubCategory2Description.Location = new System.Drawing.Point(538, 91);
            this.txtSubCategory2Description.MasterDescription = "";
            this.txtSubCategory2Description.Name = "txtSubCategory2Description";
            this.txtSubCategory2Description.Size = new System.Drawing.Size(184, 21);
            this.txtSubCategory2Description.TabIndex = 22;
            this.txtSubCategory2Description.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Description_KeyDown);
            this.txtSubCategory2Description.Leave += new System.EventHandler(this.txtSubCategory2Description_Leave);
            // 
            // chkAutoCompleationSubCategory2
            // 
            this.chkAutoCompleationSubCategory2.AutoSize = true;
            this.chkAutoCompleationSubCategory2.Checked = true;
            this.chkAutoCompleationSubCategory2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory2.Location = new System.Drawing.Point(410, 94);
            this.chkAutoCompleationSubCategory2.Name = "chkAutoCompleationSubCategory2";
            this.chkAutoCompleationSubCategory2.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory2.TabIndex = 93;
            this.chkAutoCompleationSubCategory2.Tag = "1";
            this.chkAutoCompleationSubCategory2.UseVisualStyleBackColor = true;
            // 
            // lblSubCategory2
            // 
            this.lblSubCategory2.AutoSize = true;
            this.lblSubCategory2.Location = new System.Drawing.Point(307, 94);
            this.lblSubCategory2.Name = "lblSubCategory2";
            this.lblSubCategory2.Size = new System.Drawing.Size(89, 13);
            this.lblSubCategory2.TabIndex = 102;
            this.lblSubCategory2.Text = "Special Option";
            // 
            // txtSubCategoryCode
            // 
            this.txtSubCategoryCode.IsAutoComplete = false;
            this.txtSubCategoryCode.ItemCollection = null;
            this.txtSubCategoryCode.Location = new System.Drawing.Point(427, 64);
            this.txtSubCategoryCode.MasterCode = "";
            this.txtSubCategoryCode.Name = "txtSubCategoryCode";
            this.txtSubCategoryCode.Size = new System.Drawing.Size(109, 21);
            this.txtSubCategoryCode.TabIndex = 19;
            this.txtSubCategoryCode.Tag = "3";
            this.txtSubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryCode_KeyDown);
            this.txtSubCategoryCode.Leave += new System.EventHandler(this.txtSubCategoryCode_Leave);
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.IsAutoComplete = false;
            this.txtCategoryCode.ItemCollection = null;
            this.txtCategoryCode.Location = new System.Drawing.Point(427, 39);
            this.txtCategoryCode.MasterCode = "";
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.Size = new System.Drawing.Size(109, 21);
            this.txtCategoryCode.TabIndex = 17;
            this.txtCategoryCode.Tag = "3";
            this.txtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryCode_KeyDown);
            this.txtCategoryCode.Leave += new System.EventHandler(this.txtCategoryCode_Leave);
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.IsAutoComplete = false;
            this.txtDepartmentCode.ItemCollection = null;
            this.txtDepartmentCode.Location = new System.Drawing.Point(427, 14);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(109, 21);
            this.txtDepartmentCode.TabIndex = 15;
            this.txtDepartmentCode.Tag = "3";
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Leave += new System.EventHandler(this.txtDepartmentCode_Leave);
            // 
            // txtSubCategoryDescription
            // 
            this.txtSubCategoryDescription.Location = new System.Drawing.Point(538, 64);
            this.txtSubCategoryDescription.MasterDescription = "";
            this.txtSubCategoryDescription.Name = "txtSubCategoryDescription";
            this.txtSubCategoryDescription.Size = new System.Drawing.Size(184, 21);
            this.txtSubCategoryDescription.TabIndex = 20;
            this.txtSubCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryDescription_KeyDown);
            this.txtSubCategoryDescription.Leave += new System.EventHandler(this.txtSubCategoryDescription_Leave);
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Location = new System.Drawing.Point(538, 39);
            this.txtCategoryDescription.MasterDescription = "";
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(184, 21);
            this.txtCategoryDescription.TabIndex = 18;
            this.txtCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryDescription_KeyDown);
            this.txtCategoryDescription.Leave += new System.EventHandler(this.txtCategoryDescription_Leave);
            // 
            // txtDepartmentDescription
            // 
            this.txtDepartmentDescription.Location = new System.Drawing.Point(538, 14);
            this.txtDepartmentDescription.MasterDescription = "";
            this.txtDepartmentDescription.Name = "txtDepartmentDescription";
            this.txtDepartmentDescription.Size = new System.Drawing.Size(184, 21);
            this.txtDepartmentDescription.TabIndex = 16;
            this.txtDepartmentDescription.Tag = "";
            this.txtDepartmentDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentDescription_KeyDown);
            this.txtDepartmentDescription.Leave += new System.EventHandler(this.txtDepartmentDescription_Leave);
            // 
            // chkAutoCompleationSubCategory
            // 
            this.chkAutoCompleationSubCategory.AutoSize = true;
            this.chkAutoCompleationSubCategory.Checked = true;
            this.chkAutoCompleationSubCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory.Location = new System.Drawing.Point(410, 67);
            this.chkAutoCompleationSubCategory.Name = "chkAutoCompleationSubCategory";
            this.chkAutoCompleationSubCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory.TabIndex = 101;
            this.chkAutoCompleationSubCategory.Tag = "1";
            this.chkAutoCompleationSubCategory.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationCategory
            // 
            this.chkAutoCompleationCategory.AutoSize = true;
            this.chkAutoCompleationCategory.Checked = true;
            this.chkAutoCompleationCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCategory.Location = new System.Drawing.Point(410, 42);
            this.chkAutoCompleationCategory.Name = "chkAutoCompleationCategory";
            this.chkAutoCompleationCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCategory.TabIndex = 100;
            this.chkAutoCompleationCategory.Tag = "1";
            this.chkAutoCompleationCategory.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(410, 17);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 99;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            // 
            // lblSubCategory
            // 
            this.lblSubCategory.AutoSize = true;
            this.lblSubCategory.Location = new System.Drawing.Point(307, 67);
            this.lblSubCategory.Name = "lblSubCategory";
            this.lblSubCategory.Size = new System.Drawing.Size(78, 13);
            this.lblSubCategory.TabIndex = 98;
            this.lblSubCategory.Text = "Making Type";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(307, 42);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(98, 13);
            this.lblCategory.TabIndex = 97;
            this.lblCategory.Text = "Kind Of Material";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(307, 17);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(34, 13);
            this.lblDepartment.TabIndex = 96;
            this.lblDepartment.Text = "Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Company";
            // 
            // cmbCompanyLocation
            // 
            this.cmbCompanyLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyLocation.FormattingEnabled = true;
            this.cmbCompanyLocation.Items.AddRange(new object[] {
            "POLY-PACKAGING",
            "VENTURE"});
            this.cmbCompanyLocation.Location = new System.Drawing.Point(118, 68);
            this.cmbCompanyLocation.Name = "cmbCompanyLocation";
            this.cmbCompanyLocation.Size = new System.Drawing.Size(156, 21);
            this.cmbCompanyLocation.TabIndex = 13;
            this.cmbCompanyLocation.Tag = "3";
            this.cmbCompanyLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCompanyLocation_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "Currency";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Items.AddRange(new object[] {
            "USD"});
            this.cmbCurrency.Location = new System.Drawing.Point(118, 41);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(156, 21);
            this.cmbCurrency.TabIndex = 12;
            this.cmbCurrency.Tag = "3";
            this.cmbCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCurrency_KeyDown);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(4, 14);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(29, 13);
            this.lblUnit.TabIndex = 82;
            this.lblUnit.Text = "Unit";
            // 
            // CmbUnit1
            // 
            this.CmbUnit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnit1.FormattingEnabled = true;
            this.CmbUnit1.Items.AddRange(new object[] {
            "PCS",
            "KG"});
            this.CmbUnit1.Location = new System.Drawing.Point(118, 14);
            this.CmbUnit1.Name = "CmbUnit1";
            this.CmbUnit1.Size = new System.Drawing.Size(156, 21);
            this.CmbUnit1.TabIndex = 11;
            this.CmbUnit1.Tag = "3";
            this.CmbUnit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbUnit1_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(817, 473);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "Net Amount";
            // 
            // txtNetAmount1
            // 
            this.txtNetAmount1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount1.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtNetAmount1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount1.Location = new System.Drawing.Point(912, 470);
            this.txtNetAmount1.Name = "txtNetAmount1";
            this.txtNetAmount1.ReadOnly = true;
            this.txtNetAmount1.Size = new System.Drawing.Size(134, 21);
            this.txtNetAmount1.TabIndex = 92;
            this.txtNetAmount1.Text = "0.00";
            this.txtNetAmount1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(14, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 94;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtExchangeRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExchangeRate.Location = new System.Drawing.Point(798, 84);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Size = new System.Drawing.Size(134, 21);
            this.txtExchangeRate.TabIndex = 107;
            this.txtExchangeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(689, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 106;
            this.label6.Text = "Exchange Rate";
            // 
            // FrmSalesOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1050, 554);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNetAmount1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSalesOrder";
            this.Text = "Sales Order";
            this.Load += new System.EventHandler(this.FrmSalesOrder_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.txtNetAmount1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.Button btnInvoiceDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblSalesOrderDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesOrderNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblQuotationNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.Label lblOtherCharges;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private CustomControls.TextBoxCurrency txtNetAmount;
        private CustomControls.TextBoxCurrency txtTotalTaxAmount;
        private CustomControls.TextBoxCurrency txtSubTotalDiscount;
        private CustomControls.TextBoxCurrency txtGrossAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbUnit;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private CustomControls.TextBoxCurrency txtProductDiscountAmount;
        private System.Windows.Forms.TextBox txtRate;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.DateTimePicker dtpSalesOrderDate;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.ComboBox CmbUnit1;
        private System.Windows.Forms.DateTimePicker dtpDeliver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCompanyLocation;
        private CustomControls.TextBoxMasterCode txtSubCategory2Code;
        private CustomControls.TextBoxMasterDescription txtSubCategory2Description;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory2;
        private System.Windows.Forms.Label lblSubCategory2;
        private CustomControls.TextBoxMasterCode txtSubCategoryCode;
        private CustomControls.TextBoxMasterCode txtCategoryCode;
        private CustomControls.TextBoxMasterCode txtDepartmentCode;
        private CustomControls.TextBoxMasterDescription txtSubCategoryDescription;
        private CustomControls.TextBoxMasterDescription txtCategoryDescription;
        private CustomControls.TextBoxMasterDescription txtDepartmentDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoofColor;
        private System.Windows.Forms.TextBox txtGauge;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gauge;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPersentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Label label5;
        private CustomControls.TextBoxCurrency txtNetAmount1;
        private System.Windows.Forms.CheckBox chkRupee;
        protected Glass.GlassButton btnDelete;
        private CustomControls.TextBoxCurrency txtExchangeRate;
        private System.Windows.Forms.Label label6;

    }
}
