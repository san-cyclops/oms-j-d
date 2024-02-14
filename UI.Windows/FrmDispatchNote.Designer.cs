namespace UI.Windows
{
    partial class FrmDispatchNote
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.txtExchangeRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalesOrderDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationSalesOrderNo = new System.Windows.Forms.CheckBox();
            this.txtSalesOrderNo = new System.Windows.Forms.TextBox();
            this.lblQuotationNo = new System.Windows.Forms.Label();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDispatchDate = new System.Windows.Forms.DateTimePicker();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSalesPerson = new System.Windows.Forms.Label();
            this.txtSalesPersonName = new System.Windows.Forms.TextBox();
            this.txtSalesPersonCode = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblDispatchDate = new System.Windows.Forms.Label();
            this.txtInvoiceDate = new System.Windows.Forms.TextBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.btnInvoiceDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationInvoiceNo = new System.Windows.Forms.CheckBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtDispatchQty = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtIssuedQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(750, 425);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 425);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtExchangeRate);
            this.grpHeader.Controls.Add(this.label6);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.btnSalesOrderDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSalesOrderNo);
            this.grpHeader.Controls.Add(this.txtSalesOrderNo);
            this.grpHeader.Controls.Add(this.lblQuotationNo);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDispatchDate);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonName);
            this.grpHeader.Controls.Add(this.txtSalesPersonCode);
            this.grpHeader.Controls.Add(this.lblCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerName);
            this.grpHeader.Controls.Add(this.txtCustomerCode);
            this.grpHeader.Controls.Add(this.lblDispatchDate);
            this.grpHeader.Controls.Add(this.txtInvoiceDate);
            this.grpHeader.Controls.Add(this.lblInvoiceDate);
            this.grpHeader.Controls.Add(this.btnInvoiceDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationInvoiceNo);
            this.grpHeader.Controls.Add(this.txtInvoiceNo);
            this.grpHeader.Controls.Add(this.lblInvoiceNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1061, 112);
            this.grpHeader.TabIndex = 13;
            this.grpHeader.TabStop = false;
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtExchangeRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExchangeRate.Location = new System.Drawing.Point(865, 89);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Size = new System.Drawing.Size(134, 21);
            this.txtExchangeRate.TabIndex = 109;
            this.txtExchangeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(772, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 108;
            this.label6.Text = "Exchange Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Deliver Address";
            // 
            // btnSalesOrderDetails
            // 
            this.btnSalesOrderDetails.Location = new System.Drawing.Point(530, 11);
            this.btnSalesOrderDetails.Name = "btnSalesOrderDetails";
            this.btnSalesOrderDetails.Size = new System.Drawing.Size(28, 23);
            this.btnSalesOrderDetails.TabIndex = 77;
            this.btnSalesOrderDetails.Text = "...";
            this.btnSalesOrderDetails.UseVisualStyleBackColor = true;
            this.btnSalesOrderDetails.Click += new System.EventHandler(this.btnSalesOrderDetails_Click);
            // 
            // chkAutoCompleationSalesOrderNo
            // 
            this.chkAutoCompleationSalesOrderNo.AutoSize = true;
            this.chkAutoCompleationSalesOrderNo.Checked = true;
            this.chkAutoCompleationSalesOrderNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesOrderNo.Location = new System.Drawing.Point(375, 16);
            this.chkAutoCompleationSalesOrderNo.Name = "chkAutoCompleationSalesOrderNo";
            this.chkAutoCompleationSalesOrderNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesOrderNo.TabIndex = 76;
            this.chkAutoCompleationSalesOrderNo.Tag = "1";
            this.chkAutoCompleationSalesOrderNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesOrderNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesOrderNo_CheckedChanged);
            // 
            // txtSalesOrderNo
            // 
            this.txtSalesOrderNo.Location = new System.Drawing.Point(393, 12);
            this.txtSalesOrderNo.Name = "txtSalesOrderNo";
            this.txtSalesOrderNo.Size = new System.Drawing.Size(136, 21);
            this.txtSalesOrderNo.TabIndex = 75;
            this.txtSalesOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesOrderNo_KeyDown);
            // 
            // lblQuotationNo
            // 
            this.lblQuotationNo.AutoSize = true;
            this.lblQuotationNo.Location = new System.Drawing.Point(281, 16);
            this.lblQuotationNo.Name = "lblQuotationNo";
            this.lblQuotationNo.Size = new System.Drawing.Size(94, 13);
            this.lblQuotationNo.TabIndex = 74;
            this.lblQuotationNo.Text = "Sales Order No";
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(865, 62);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(183, 21);
            this.cmbCostCentre.TabIndex = 73;
            this.cmbCostCentre.Visible = false;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(772, 65);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 72;
            this.lblCostCentre.Text = "Cost Centre";
            this.lblCostCentre.Visible = false;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(244, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 30;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(91, 16);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 29;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 28;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 27;
            this.lblDocumentNo.Text = "Document No";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(865, 38);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(183, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(772, 42);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDispatchDate
            // 
            this.dtpDispatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDispatchDate.Location = new System.Drawing.Point(867, 13);
            this.dtpDispatchDate.Name = "dtpDispatchDate";
            this.dtpDispatchDate.Size = new System.Drawing.Size(181, 21);
            this.dtpDispatchDate.TabIndex = 22;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 84);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(583, 21);
            this.txtRemark.TabIndex = 18;
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
            this.txtSalesPersonName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSalesPersonName.Location = new System.Drawing.Point(244, 60);
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(446, 21);
            this.txtSalesPersonName.TabIndex = 16;
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSalesPersonCode.Location = new System.Drawing.Point(107, 60);
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(136, 21);
            this.txtSalesPersonCode.TabIndex = 14;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(6, 39);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(63, 13);
            this.lblCustomer.TabIndex = 13;
            this.lblCustomer.Text = "Customer";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerName.Location = new System.Drawing.Point(244, 36);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(446, 21);
            this.txtCustomerName.TabIndex = 12;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCustomerCode.Location = new System.Drawing.Point(107, 36);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 10;
            // 
            // lblDispatchDate
            // 
            this.lblDispatchDate.AutoSize = true;
            this.lblDispatchDate.Location = new System.Drawing.Point(772, 19);
            this.lblDispatchDate.Name = "lblDispatchDate";
            this.lblDispatchDate.Size = new System.Drawing.Size(87, 13);
            this.lblDispatchDate.TabIndex = 9;
            this.lblDispatchDate.Text = "Dispatch Date";
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtInvoiceDate.Location = new System.Drawing.Point(453, 90);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Size = new System.Drawing.Size(101, 21);
            this.txtInvoiceDate.TabIndex = 26;
            this.txtInvoiceDate.Visible = false;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(393, 94);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(57, 13);
            this.lblInvoiceDate.TabIndex = 25;
            this.lblInvoiceDate.Text = "Inv Date";
            this.lblInvoiceDate.Visible = false;
            // 
            // btnInvoiceDetails
            // 
            this.btnInvoiceDetails.Location = new System.Drawing.Point(363, 89);
            this.btnInvoiceDetails.Name = "btnInvoiceDetails";
            this.btnInvoiceDetails.Size = new System.Drawing.Size(28, 23);
            this.btnInvoiceDetails.TabIndex = 21;
            this.btnInvoiceDetails.Text = "...";
            this.btnInvoiceDetails.UseVisualStyleBackColor = true;
            this.btnInvoiceDetails.Visible = false;
            // 
            // chkAutoCompleationInvoiceNo
            // 
            this.chkAutoCompleationInvoiceNo.AutoSize = true;
            this.chkAutoCompleationInvoiceNo.Checked = true;
            this.chkAutoCompleationInvoiceNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationInvoiceNo.Location = new System.Drawing.Point(209, 93);
            this.chkAutoCompleationInvoiceNo.Name = "chkAutoCompleationInvoiceNo";
            this.chkAutoCompleationInvoiceNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationInvoiceNo.TabIndex = 4;
            this.chkAutoCompleationInvoiceNo.Tag = "1";
            this.chkAutoCompleationInvoiceNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationInvoiceNo.Visible = false;
            this.chkAutoCompleationInvoiceNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationInvoiceNo_CheckedChanged);
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(239, 91);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(136, 21);
            this.txtInvoiceNo.TabIndex = 2;
            this.txtInvoiceNo.Visible = false;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceNo_KeyDown);
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Location = new System.Drawing.Point(140, 93);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceNo.TabIndex = 0;
            this.lblInvoiceNo.Text = "Invoice No";
            this.lblInvoiceNo.Visible = false;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtSize);
            this.grpBody.Controls.Add(this.txtDispatchQty);
            this.grpBody.Controls.Add(this.txtIssuedQty);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Location = new System.Drawing.Point(2, 113);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1062, 278);
            this.grpBody.TabIndex = 31;
            this.grpBody.TabStop = false;
            // 
            // txtSize
            // 
            this.txtSize.BackColor = System.Drawing.SystemColors.Window;
            this.txtSize.Location = new System.Drawing.Point(494, 251);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(107, 21);
            this.txtSize.TabIndex = 58;
            this.txtSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSize_KeyDown);
            // 
            // txtDispatchQty
            // 
            this.txtDispatchQty.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDispatchQty.Location = new System.Drawing.Point(777, 251);
            this.txtDispatchQty.Name = "txtDispatchQty";
            this.txtDispatchQty.Size = new System.Drawing.Size(78, 21);
            this.txtDispatchQty.TabIndex = 57;
            this.txtDispatchQty.TextChanged += new System.EventHandler(this.txtDispatchQty_TextChanged);
            this.txtDispatchQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDispatchQty_KeyDown);
            this.txtDispatchQty.Validated += new System.EventHandler(this.txtDispatchQty_Validated);
            // 
            // txtIssuedQty
            // 
            this.txtIssuedQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtIssuedQty.Location = new System.Drawing.Point(603, 251);
            this.txtIssuedQty.Name = "txtIssuedQty";
            this.txtIssuedQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtIssuedQty.Size = new System.Drawing.Size(96, 21);
            this.txtIssuedQty.TabIndex = 56;
            this.txtIssuedQty.TextChanged += new System.EventHandler(this.txtIssuedQty_TextChanged);
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtQty.Location = new System.Drawing.Point(700, 251);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(74, 21);
            this.txtQty.TabIndex = 55;
            // 
            // txtProductName
            // 
            this.txtProductName.BackColor = System.Drawing.SystemColors.Window;
            this.txtProductName.Location = new System.Drawing.Point(200, 251);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(221, 21);
            this.txtProductName.TabIndex = 51;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 255);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 50;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtProductCode.Location = new System.Drawing.Point(21, 251);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(177, 21);
            this.txtProductCode.TabIndex = 49;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AllowUserToDeleteRows = false;
            this.dgvItemDetails.AllowUserToResizeColumns = false;
            this.dgvItemDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvItemDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProductID,
            this.ProductName,
            this.InvoiceQty,
            this.Size,
            this.BalanceQty,
            this.IssuedQty,
            this.DispatchQty,
            this.Price,
            this.NetAmount});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 0);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1049, 236);
            this.dgvItemDetails.TabIndex = 20;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Width = 150;
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "SalesOrderDetailID";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.Visible = false;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 225;
            // 
            // InvoiceQty
            // 
            this.InvoiceQty.DataPropertyName = "Qty";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InvoiceQty.DefaultCellStyle = dataGridViewCellStyle10;
            this.InvoiceQty.HeaderText = "Qty";
            this.InvoiceQty.Name = "InvoiceQty";
            this.InvoiceQty.ReadOnly = true;
            this.InvoiceQty.Width = 75;
            // 
            // Size
            // 
            this.Size.DataPropertyName = "Size";
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            // 
            // BalanceQty
            // 
            this.BalanceQty.DataPropertyName = "BalanceQty";
            this.BalanceQty.HeaderText = "BalanceQty";
            this.BalanceQty.Name = "BalanceQty";
            // 
            // IssuedQty
            // 
            this.IssuedQty.DataPropertyName = "IssuedQty";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IssuedQty.DefaultCellStyle = dataGridViewCellStyle11;
            this.IssuedQty.HeaderText = "Issued Qty";
            this.IssuedQty.Name = "IssuedQty";
            this.IssuedQty.ReadOnly = true;
            this.IssuedQty.Width = 75;
            // 
            // DispatchQty
            // 
            this.DispatchQty.DataPropertyName = "DispatchQty";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DispatchQty.DefaultCellStyle = dataGridViewCellStyle12;
            this.DispatchQty.HeaderText = "Disp. Qty";
            this.DispatchQty.Name = "DispatchQty";
            this.DispatchQty.Width = 75;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Rate";
            this.Price.HeaderText = "SellingPrice";
            this.Price.Name = "Price";
            // 
            // NetAmount
            // 
            this.NetAmount.DataPropertyName = "NetAmount";
            this.NetAmount.HeaderText = "Amount";
            this.NetAmount.Name = "NetAmount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(828, 401);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 95;
            this.label5.Text = "Net Amount";
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
            this.txtNetAmount.Location = new System.Drawing.Point(923, 398);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(134, 21);
            this.txtNetAmount.TabIndex = 94;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmDispatchNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1069, 473);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNetAmount);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmDispatchNote";
            this.Text = "Dispatch Note";
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.Controls.SetChildIndex(this.txtNetAmount, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.TextBox txtInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDispatchDate;
        private System.Windows.Forms.Button btnInvoiceDetails;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblDispatchDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationInvoiceNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.Button btnSalesOrderDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesOrderNo;
        private System.Windows.Forms.TextBox txtSalesOrderNo;
        private System.Windows.Forms.Label lblQuotationNo;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtProductCode;
        private CustomControls.TextBoxQty txtIssuedQty;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.Label label5;
        private CustomControls.TextBoxCurrency txtNetAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private CustomControls.TextBoxCurrency txtDispatchQty;
        private CustomControls.TextBoxCurrency txtExchangeRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSize;
    }
}
