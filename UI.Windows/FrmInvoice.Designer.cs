namespace UI.Windows
{
    partial class FrmInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvoice));
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.txtExchangeRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDispatchDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationDispatchNo = new System.Windows.Forms.CheckBox();
            this.txtDispatchNo = new System.Windows.Forms.TextBox();
            this.lblDispatchNo = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSalesPerson = new System.Windows.Forms.Label();
            this.txtSalesPersonName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSalesPerson = new System.Windows.Forms.CheckBox();
            this.txtSalesPersonCode = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationInvoiceNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tbBody = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.chkRound = new System.Windows.Forms.CheckBox();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.txtTotalTaxAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.btnDelete = new Glass.GlassButton();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.tbBody.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(556, 524);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Controls.Add(this.btnDelete);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 524);
            this.grpButtonSet.Size = new System.Drawing.Size(255, 46);
            this.grpButtonSet.Controls.SetChildIndex(this.btnHelp, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnView, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnDelete, 0);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(168, 11);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(90, 11);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtExchangeRate);
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.btnDispatchDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDispatchNo);
            this.grpHeader.Controls.Add(this.txtDispatchNo);
            this.grpHeader.Controls.Add(this.lblDispatchNo);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.dtpInvoiceDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonCode);
            this.grpHeader.Controls.Add(this.lblCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerCode);
            this.grpHeader.Controls.Add(this.lblInvoiceDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationInvoiceNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(868, 110);
            this.grpHeader.TabIndex = 12;
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
            this.txtExchangeRate.Location = new System.Drawing.Point(690, 68);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Size = new System.Drawing.Size(134, 21);
            this.txtExchangeRate.TabIndex = 81;
            this.txtExchangeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(592, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "Exchange Rate";
            // 
            // btnDispatchDetails
            // 
            this.btnDispatchDetails.Location = new System.Drawing.Point(558, 10);
            this.btnDispatchDetails.Name = "btnDispatchDetails";
            this.btnDispatchDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDispatchDetails.TabIndex = 77;
            this.btnDispatchDetails.Text = "...";
            this.btnDispatchDetails.UseVisualStyleBackColor = true;
            this.btnDispatchDetails.Visible = false;
            this.btnDispatchDetails.Click += new System.EventHandler(this.btnDispatchDetails_Click);
            // 
            // chkAutoCompleationDispatchNo
            // 
            this.chkAutoCompleationDispatchNo.AutoSize = true;
            this.chkAutoCompleationDispatchNo.Checked = true;
            this.chkAutoCompleationDispatchNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDispatchNo.Location = new System.Drawing.Point(404, 14);
            this.chkAutoCompleationDispatchNo.Name = "chkAutoCompleationDispatchNo";
            this.chkAutoCompleationDispatchNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDispatchNo.TabIndex = 76;
            this.chkAutoCompleationDispatchNo.Tag = "1";
            this.chkAutoCompleationDispatchNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDispatchNo.Visible = false;
            this.chkAutoCompleationDispatchNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDispatchNo_CheckedChanged);
            // 
            // txtDispatchNo
            // 
            this.txtDispatchNo.Location = new System.Drawing.Point(421, 11);
            this.txtDispatchNo.Name = "txtDispatchNo";
            this.txtDispatchNo.Size = new System.Drawing.Size(136, 21);
            this.txtDispatchNo.TabIndex = 75;
            this.txtDispatchNo.Visible = false;
            // 
            // lblDispatchNo
            // 
            this.lblDispatchNo.AutoSize = true;
            this.lblDispatchNo.Location = new System.Drawing.Point(318, 14);
            this.lblDispatchNo.Name = "lblDispatchNo";
            this.lblDispatchNo.Size = new System.Drawing.Size(75, 13);
            this.lblDispatchNo.TabIndex = 74;
            this.lblDispatchNo.Text = "Dispatch No";
            this.lblDispatchNo.Visible = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(690, 41);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(165, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.Tag = "1";
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(592, 43);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 64;
            this.lblLocation.Text = "Location";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(733, 14);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(122, 21);
            this.dtpInvoiceDate.TabIndex = 22;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(244, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 87);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 84);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(477, 21);
            this.txtRemark.TabIndex = 18;
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
            this.txtSalesPersonName.Location = new System.Drawing.Point(244, 60);
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(340, 21);
            this.txtSalesPersonName.TabIndex = 16;
            this.txtSalesPersonName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonName_KeyDown);
            this.txtSalesPersonName.Leave += new System.EventHandler(this.txtSalesPersonName_Leave);
            // 
            // chkAutoCompleationSalesPerson
            // 
            this.chkAutoCompleationSalesPerson.AutoSize = true;
            this.chkAutoCompleationSalesPerson.Checked = true;
            this.chkAutoCompleationSalesPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(90, 63);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 15;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Location = new System.Drawing.Point(107, 60);
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(136, 21);
            this.txtSalesPersonCode.TabIndex = 14;
            this.txtSalesPersonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonCode_KeyDown);
            this.txtSalesPersonCode.Leave += new System.EventHandler(this.txtSalesPersonCode_Leave);
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
            this.txtCustomerName.Location = new System.Drawing.Point(244, 36);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(340, 21);
            this.txtCustomerName.TabIndex = 12;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(90, 39);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 11;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(107, 36);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 10;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(592, 15);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(80, 13);
            this.lblInvoiceDate.TabIndex = 9;
            this.lblInvoiceDate.Text = "Invoice Date";
            this.lblInvoiceDate.Visible = false;
            // 
            // chkAutoCompleationInvoiceNo
            // 
            this.chkAutoCompleationInvoiceNo.AutoSize = true;
            this.chkAutoCompleationInvoiceNo.Checked = true;
            this.chkAutoCompleationInvoiceNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationInvoiceNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationInvoiceNo.Name = "chkAutoCompleationInvoiceNo";
            this.chkAutoCompleationInvoiceNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationInvoiceNo.TabIndex = 4;
            this.chkAutoCompleationInvoiceNo.Tag = "1";
            this.chkAutoCompleationInvoiceNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationInvoiceNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationInvoiceNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(410, 84);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(165, 21);
            this.cmbCostCentre.TabIndex = 71;
            this.cmbCostCentre.Visible = false;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(323, 87);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 70;
            this.lblCostCentre.Text = "Cost Centre";
            this.lblCostCentre.Visible = false;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(410, 81);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(165, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.Visible = false;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(323, 84);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            this.lblReferenceNo.Visible = false;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tbBody);
            this.grpBody.Location = new System.Drawing.Point(2, 100);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1000, 249);
            this.grpBody.TabIndex = 13;
            this.grpBody.TabStop = false;
            // 
            // tbBody
            // 
            this.tbBody.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tbBody.Controls.Add(this.tbpGeneral);
            this.tbBody.Location = new System.Drawing.Point(1, 8);
            this.tbBody.Name = "tbBody";
            this.tbBody.SelectedIndex = 0;
            this.tbBody.Size = new System.Drawing.Size(873, 265);
            this.tbBody.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.dgvItemDetails);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(865, 236);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AllowUserToDeleteRows = false;
            this.dgvItemDetails.AllowUserToResizeColumns = false;
            this.dgvItemDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvItemDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProductName,
            this.InvoiceQty,
            this.IssuedQty,
            this.DispatchQty,
            this.Price,
            this.NetAmount});
            this.dgvItemDetails.Location = new System.Drawing.Point(12, 9);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(848, 200);
            this.dgvItemDetails.TabIndex = 21;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Width = 150;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InvoiceQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.InvoiceQty.HeaderText = "Qty";
            this.InvoiceQty.Name = "InvoiceQty";
            this.InvoiceQty.ReadOnly = true;
            this.InvoiceQty.Width = 75;
            // 
            // IssuedQty
            // 
            this.IssuedQty.DataPropertyName = "IssuedQty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IssuedQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.IssuedQty.HeaderText = "Issued Qty";
            this.IssuedQty.Name = "IssuedQty";
            this.IssuedQty.ReadOnly = true;
            this.IssuedQty.Width = 75;
            // 
            // DispatchQty
            // 
            this.DispatchQty.DataPropertyName = "DispatchQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DispatchQty.DefaultCellStyle = dataGridViewCellStyle4;
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
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtGrossAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGrossAmount.Location = new System.Drawing.Point(715, 20);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.Size = new System.Drawing.Size(134, 21);
            this.txtGrossAmount.TabIndex = 30;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGrossAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrossAmount_KeyDown);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtSubTotalDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(715, 68);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(134, 21);
            this.txtSubTotalDiscount.TabIndex = 31;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.txtNetAmount.Location = new System.Drawing.Point(715, 122);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(134, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(561, 23);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(561, 47);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(715, 44);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(134, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 37;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyDown);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(697, 47);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(560, 125);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(675, 47);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // chkRound
            // 
            this.chkRound.AutoSize = true;
            this.chkRound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRound.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRound.Location = new System.Drawing.Point(649, 124);
            this.chkRound.Name = "chkRound";
            this.chkRound.Size = new System.Drawing.Size(62, 17);
            this.chkRound.TabIndex = 90;
            this.chkRound.Text = "Round";
            this.chkRound.UseVisualStyleBackColor = true;
            this.chkRound.Visible = false;
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(588, 95);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 91;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(697, 95);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 92;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(569, 91);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 93;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click_1);
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalTaxAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(716, 95);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(134, 21);
            this.txtTotalTaxAmount.TabIndex = 94;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkRound);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(3, 369);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(869, 157);
            this.grpFooter.TabIndex = 14;
            this.grpFooter.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(12, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 95;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FrmInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(875, 572);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmInvoice";
            this.Text = "Invoice";
            this.Load += new System.EventHandler(this.FrmInvoice_Load);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.tbBody.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationInvoiceNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TabControl tbBody;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.Button btnDispatchDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationDispatchNo;
        private System.Windows.Forms.TextBox txtDispatchNo;
        private System.Windows.Forms.Label lblDispatchNo;
        private CustomControls.TextBoxCurrency txtGrossAmount;
        private CustomControls.TextBoxCurrency txtSubTotalDiscount;
        private CustomControls.TextBoxCurrency txtNetAmount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.CheckBox chkRound;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private CustomControls.TextBoxCurrency txtTotalTaxAmount;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private CustomControls.TextBoxCurrency txtExchangeRate;
        private System.Windows.Forms.Label label2;
        protected Glass.GlassButton btnDelete;
    }
}
