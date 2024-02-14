namespace UI.Windows
{
    partial class FrmSalesReturnNote
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbReturnType = new System.Windows.Forms.ComboBox();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.btnInvoiceDetails = new System.Windows.Forms.Button();
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
            this.lblCrnDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.chkAutoCompleationInvoiceNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tbBody = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscountAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscountPercentage = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtFreeQty = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FreeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.tbpAdvanced = new System.Windows.Forms.TabPage();
            this.dgvAdvanced = new System.Windows.Forms.DataGridView();
            this.LedgerCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.lblPaidAmount = new System.Windows.Forms.Label();
            this.txtPaidAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPayingAmount = new System.Windows.Forms.Label();
            this.txtPayingAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbBranchName = new System.Windows.Forms.ComboBox();
            this.cmbBankName = new System.Windows.Forms.ComboBox();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.cmbBranchCode = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBankCode = new System.Windows.Forms.ComboBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblCardCheque = new System.Windows.Forms.Label();
            this.txtCardChequeNo = new System.Windows.Forms.TextBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.txtBalanceAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tbpPayment = new System.Windows.Forms.TabPage();
            this.lblLedger = new System.Windows.Forms.Label();
            this.cmbLedgerName = new System.Windows.Forms.ComboBox();
            this.cmbLedgerCode = new System.Windows.Forms.ComboBox();
            this.dgvPaymentDetails = new System.Windows.Forms.DataGridView();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentMethodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpLoyalty = new System.Windows.Forms.TabPage();
            this.lblLoyaltyCustomerName = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.txtLoyaltyCustomerName = new System.Windows.Forms.TextBox();
            this.lblLoyaltyCustomerCode = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.txtLoyaltyCustomerCode = new System.Windows.Forms.TextBox();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.tbpCreditCustomer = new System.Windows.Forms.TabPage();
            this.txtReturnCheques = new System.Windows.Forms.TextBox();
            this.lblReturnCheques = new System.Windows.Forms.Label();
            this.txtNoOfReturnCheques = new System.Windows.Forms.TextBox();
            this.txtPostDatedCheques = new System.Windows.Forms.TextBox();
            this.lblPostDatedCheques = new System.Windows.Forms.Label();
            this.txtNoOfPostDatedCheques = new System.Windows.Forms.TextBox();
            this.lblOutstanding = new System.Windows.Forms.Label();
            this.txtOutstanding = new System.Windows.Forms.TextBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.lblCreditLimit = new System.Windows.Forms.Label();
            this.txtPaymentTerms = new System.Windows.Forms.TextBox();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.tbpPageSetup = new System.Windows.Forms.TabPage();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.txtNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalTaxAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.tbBody.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.tbpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetails)).BeginInit();
            this.tbpLoyalty.SuspendLayout();
            this.tbpCreditCustomer.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(819, 506);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 506);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.cmbReturnType);
            this.grpHeader.Controls.Add(this.lblReturnType);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpReturnDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.btnInvoiceDetails);
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
            this.grpHeader.Controls.Add(this.lblCrnDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtInvoiceNo);
            this.grpHeader.Controls.Add(this.lblInvoiceNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationInvoiceNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1134, 110);
            this.grpHeader.TabIndex = 15;
            this.grpHeader.TabStop = false;
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.Location = new System.Drawing.Point(930, 40);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(88, 17);
            this.chkTStatus.TabIndex = 76;
            this.chkTStatus.Text = "checkBox1";
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Location = new System.Drawing.Point(1047, 40);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(82, 17);
            this.chkOverwrite.TabIndex = 75;
            this.chkOverwrite.Text = "Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(748, 84);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(381, 21);
            this.cmbCostCentre.TabIndex = 73;
            this.cmbCostCentre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostCentre_KeyDown);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(645, 87);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 72;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(748, 61);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(381, 21);
            this.cmbLocation.TabIndex = 67;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(645, 63);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 66;
            this.lblLocation.Text = "Location";
            // 
            // cmbReturnType
            // 
            this.cmbReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnType.FormattingEnabled = true;
            this.cmbReturnType.Location = new System.Drawing.Point(748, 37);
            this.cmbReturnType.Name = "cmbReturnType";
            this.cmbReturnType.Size = new System.Drawing.Size(165, 21);
            this.cmbReturnType.TabIndex = 31;
            this.cmbReturnType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReturnType_KeyDown);
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.Location = new System.Drawing.Point(645, 40);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(76, 13);
            this.lblReturnType.TabIndex = 30;
            this.lblReturnType.Text = "Return Type";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(748, 13);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(165, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(645, 17);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Location = new System.Drawing.Point(1007, 14);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(122, 21);
            this.dtpReturnDate.TabIndex = 22;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(286, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // btnInvoiceDetails
            // 
            this.btnInvoiceDetails.Location = new System.Drawing.Point(598, 11);
            this.btnInvoiceDetails.Name = "btnInvoiceDetails";
            this.btnInvoiceDetails.Size = new System.Drawing.Size(28, 23);
            this.btnInvoiceDetails.TabIndex = 20;
            this.btnInvoiceDetails.Text = "...";
            this.btnInvoiceDetails.UseVisualStyleBackColor = true;
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
            this.txtRemark.Location = new System.Drawing.Point(149, 84);
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
            this.txtSalesPersonName.Location = new System.Drawing.Point(286, 60);
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
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(132, 63);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 15;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Location = new System.Drawing.Point(149, 60);
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
            this.txtCustomerName.Location = new System.Drawing.Point(286, 36);
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
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(132, 39);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 11;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(149, 36);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 10;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // lblCrnDate
            // 
            this.lblCrnDate.AutoSize = true;
            this.lblCrnDate.Location = new System.Drawing.Point(927, 17);
            this.lblCrnDate.Name = "lblCrnDate";
            this.lblCrnDate.Size = new System.Drawing.Size(76, 13);
            this.lblCrnDate.TabIndex = 9;
            this.lblCrnDate.Text = "Return Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(132, 16);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 8;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(461, 12);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(136, 21);
            this.txtInvoiceNo.TabIndex = 7;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceNo_KeyDown);
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Location = new System.Drawing.Point(358, 15);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceNo.TabIndex = 6;
            this.lblInvoiceNo.Text = "Invoice No";
            // 
            // chkAutoCompleationInvoiceNo
            // 
            this.chkAutoCompleationInvoiceNo.AutoSize = true;
            this.chkAutoCompleationInvoiceNo.Checked = true;
            this.chkAutoCompleationInvoiceNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationInvoiceNo.Location = new System.Drawing.Point(442, 15);
            this.chkAutoCompleationInvoiceNo.Name = "chkAutoCompleationInvoiceNo";
            this.chkAutoCompleationInvoiceNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationInvoiceNo.TabIndex = 4;
            this.chkAutoCompleationInvoiceNo.Tag = "1";
            this.chkAutoCompleationInvoiceNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationInvoiceNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationInvoiceNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(149, 12);
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
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tbBody);
            this.grpBody.Location = new System.Drawing.Point(1, 99);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1135, 237);
            this.grpBody.TabIndex = 16;
            this.grpBody.TabStop = false;
            // 
            // tbBody
            // 
            this.tbBody.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tbBody.Controls.Add(this.tbpGeneral);
            this.tbBody.Controls.Add(this.tbpAdvanced);
            this.tbBody.Location = new System.Drawing.Point(1, 9);
            this.tbBody.Name = "tbBody";
            this.tbBody.SelectedIndex = 0;
            this.tbBody.Size = new System.Drawing.Size(1133, 231);
            this.tbBody.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.txtBatchNo);
            this.tbpGeneral.Controls.Add(this.dtpExpiry);
            this.tbpGeneral.Controls.Add(this.cmbUnit);
            this.tbpGeneral.Controls.Add(this.txtProductAmount);
            this.tbpGeneral.Controls.Add(this.txtProductDiscountAmount);
            this.tbpGeneral.Controls.Add(this.txtProductDiscountPercentage);
            this.tbpGeneral.Controls.Add(this.txtRate);
            this.tbpGeneral.Controls.Add(this.txtFreeQty);
            this.tbpGeneral.Controls.Add(this.txtQty);
            this.tbpGeneral.Controls.Add(this.txtProductName);
            this.tbpGeneral.Controls.Add(this.chkAutoCompleationProduct);
            this.tbpGeneral.Controls.Add(this.dgvItemDetails);
            this.tbpGeneral.Controls.Add(this.txtProductCode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(1125, 202);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(488, 180);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(100, 21);
            this.txtBatchNo.TabIndex = 47;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(589, 180);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(91, 21);
            this.dtpExpiry.TabIndex = 46;
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(424, 180);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(63, 21);
            this.cmbUnit.TabIndex = 45;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Location = new System.Drawing.Point(992, 180);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.Size = new System.Drawing.Size(133, 21);
            this.txtProductAmount.TabIndex = 38;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtProductDiscountAmount
            // 
            this.txtProductDiscountAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountAmount.Location = new System.Drawing.Point(920, 180);
            this.txtProductDiscountAmount.Name = "txtProductDiscountAmount";
            this.txtProductDiscountAmount.Size = new System.Drawing.Size(71, 21);
            this.txtProductDiscountAmount.TabIndex = 39;
            this.txtProductDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountAmount_KeyDown);
            this.txtProductDiscountAmount.Leave += new System.EventHandler(this.txtProductDiscountAmount_Leave);
            // 
            // txtProductDiscountPercentage
            // 
            this.txtProductDiscountPercentage.Location = new System.Drawing.Point(879, 180);
            this.txtProductDiscountPercentage.Name = "txtProductDiscountPercentage";
            this.txtProductDiscountPercentage.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountPercentage.Size = new System.Drawing.Size(40, 21);
            this.txtProductDiscountPercentage.TabIndex = 40;
            this.txtProductDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountPercentage_KeyDown);
            this.txtProductDiscountPercentage.Leave += new System.EventHandler(this.txtProductDiscountPercentage_Leave);
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(783, 180);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(95, 21);
            this.txtRate.TabIndex = 41;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            // 
            // txtFreeQty
            // 
            this.txtFreeQty.Location = new System.Drawing.Point(738, 180);
            this.txtFreeQty.Name = "txtFreeQty";
            this.txtFreeQty.Size = new System.Drawing.Size(44, 21);
            this.txtFreeQty.TabIndex = 42;
            this.txtFreeQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFreeQty_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(681, 180);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(56, 21);
            this.txtQty.TabIndex = 43;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(177, 180);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(246, 21);
            this.txtProductName.TabIndex = 37;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 183);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 36;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.BatchNo,
            this.Expiry,
            this.Qty,
            this.FreeQty,
            this.Rate,
            this.DiscountPer,
            this.DiscountAmt,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 20;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1127, 176);
            this.dgvItemDetails.TabIndex = 34;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RowNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.Width = 33;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Width = 125;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 243;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.Width = 60;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.BatchNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.BatchNo.HeaderText = "Batch";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.Width = 110;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Expiry.DefaultCellStyle = dataGridViewCellStyle6;
            this.Expiry.HeaderText = "Expiry";
            this.Expiry.Name = "Expiry";
            this.Expiry.Width = 88;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle7;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 57;
            // 
            // FreeQty
            // 
            this.FreeQty.DataPropertyName = "FreeQty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FreeQty.DefaultCellStyle = dataGridViewCellStyle8;
            this.FreeQty.HeaderText = "Free";
            this.FreeQty.Name = "FreeQty";
            this.FreeQty.Width = 45;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle9;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 97;
            // 
            // DiscountPer
            // 
            this.DiscountPer.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPer.DefaultCellStyle = dataGridViewCellStyle10;
            this.DiscountPer.HeaderText = "Dis%";
            this.DiscountPer.Name = "DiscountPer";
            this.DiscountPer.Width = 40;
            // 
            // DiscountAmt
            // 
            this.DiscountAmt.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmt.DefaultCellStyle = dataGridViewCellStyle11;
            this.DiscountAmt.HeaderText = "Discount";
            this.DiscountAmt.Name = "DiscountAmt";
            this.DiscountAmt.Width = 71;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle12;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 113;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 180);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(154, 21);
            this.txtProductCode.TabIndex = 35;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // tbpAdvanced
            // 
            this.tbpAdvanced.Controls.Add(this.dgvAdvanced);
            this.tbpAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tbpAdvanced.Name = "tbpAdvanced";
            this.tbpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAdvanced.Size = new System.Drawing.Size(1125, 202);
            this.tbpAdvanced.TabIndex = 1;
            this.tbpAdvanced.Text = "Advanced";
            this.tbpAdvanced.UseVisualStyleBackColor = true;
            // 
            // dgvAdvanced
            // 
            this.dgvAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdvanced.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerCode,
            this.LedgerName,
            this.Value});
            this.dgvAdvanced.Location = new System.Drawing.Point(0, 0);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.RowHeadersWidth = 15;
            this.dgvAdvanced.Size = new System.Drawing.Size(1125, 201);
            this.dgvAdvanced.TabIndex = 36;
            // 
            // LedgerCode
            // 
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.Width = 150;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.Width = 350;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 120;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblPaidAmount);
            this.grpFooter.Controls.Add(this.txtPaidAmount);
            this.grpFooter.Controls.Add(this.lblPayingAmount);
            this.grpFooter.Controls.Add(this.txtPayingAmount);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.dtpChequeDate);
            this.grpFooter.Controls.Add(this.lblChequeDate);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.cmbBranchName);
            this.grpFooter.Controls.Add(this.cmbBankName);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.cmbBranchCode);
            this.grpFooter.Controls.Add(this.lblBranch);
            this.grpFooter.Controls.Add(this.cmbBankCode);
            this.grpFooter.Controls.Add(this.lblBank);
            this.grpFooter.Controls.Add(this.lblCardCheque);
            this.grpFooter.Controls.Add(this.txtCardChequeNo);
            this.grpFooter.Controls.Add(this.lblBalanceAmount);
            this.grpFooter.Controls.Add(this.cmbPaymentMethod);
            this.grpFooter.Controls.Add(this.txtBalanceAmount);
            this.grpFooter.Controls.Add(this.lblPaymentMethod);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.tbFooter);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(1, 330);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1135, 181);
            this.grpFooter.TabIndex = 17;
            this.grpFooter.TabStop = false;
            // 
            // lblPaidAmount
            // 
            this.lblPaidAmount.AutoSize = true;
            this.lblPaidAmount.Location = new System.Drawing.Point(496, 158);
            this.lblPaidAmount.Name = "lblPaidAmount";
            this.lblPaidAmount.Size = new System.Drawing.Size(79, 13);
            this.lblPaidAmount.TabIndex = 96;
            this.lblPaidAmount.Text = "Paid Amount";
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPaidAmount.Location = new System.Drawing.Point(607, 154);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.ReadOnly = true;
            this.txtPaidAmount.Size = new System.Drawing.Size(219, 21);
            this.txtPaidAmount.TabIndex = 95;
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayingAmount
            // 
            this.lblPayingAmount.AutoSize = true;
            this.lblPayingAmount.Location = new System.Drawing.Point(496, 133);
            this.lblPayingAmount.Name = "lblPayingAmount";
            this.lblPayingAmount.Size = new System.Drawing.Size(93, 13);
            this.lblPayingAmount.TabIndex = 94;
            this.lblPayingAmount.Text = "Paying Amount";
            // 
            // txtPayingAmount
            // 
            this.txtPayingAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPayingAmount.Location = new System.Drawing.Point(607, 130);
            this.txtPayingAmount.Name = "txtPayingAmount";
            this.txtPayingAmount.Size = new System.Drawing.Size(219, 21);
            this.txtPayingAmount.TabIndex = 93;
            this.txtPayingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayingAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayingAmount_KeyDown);
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(850, 83);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 91;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(978, 87);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 90;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChequeDate.Location = new System.Drawing.Point(607, 59);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(219, 21);
            this.dtpChequeDate.TabIndex = 89;
            this.dtpChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpChequeDate_KeyDown);
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Location = new System.Drawing.Point(496, 62);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(82, 13);
            this.lblChequeDate.TabIndex = 88;
            this.lblChequeDate.Text = "Cheque Date";
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(850, 111);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 59;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtOtherCharges.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharges.Location = new System.Drawing.Point(996, 108);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.ReadOnly = true;
            this.txtOtherCharges.Size = new System.Drawing.Size(134, 21);
            this.txtOtherCharges.TabIndex = 58;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbBranchName
            // 
            this.cmbBranchName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranchName.FormattingEnabled = true;
            this.cmbBranchName.Location = new System.Drawing.Point(668, 106);
            this.cmbBranchName.Name = "cmbBranchName";
            this.cmbBranchName.Size = new System.Drawing.Size(158, 21);
            this.cmbBranchName.TabIndex = 55;
            this.cmbBranchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBranchName_KeyDown);
            // 
            // cmbBankName
            // 
            this.cmbBankName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankName.FormattingEnabled = true;
            this.cmbBankName.Location = new System.Drawing.Point(668, 82);
            this.cmbBankName.Name = "cmbBankName";
            this.cmbBankName.Size = new System.Drawing.Size(158, 21);
            this.cmbBankName.TabIndex = 54;
            this.cmbBankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankName_KeyDown);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(957, 40);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // cmbBranchCode
            // 
            this.cmbBranchCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranchCode.FormattingEnabled = true;
            this.cmbBranchCode.Location = new System.Drawing.Point(607, 106);
            this.cmbBranchCode.Name = "cmbBranchCode";
            this.cmbBranchCode.Size = new System.Drawing.Size(60, 21);
            this.cmbBranchCode.TabIndex = 52;
            this.cmbBranchCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBranchCode_KeyDown);
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(496, 109);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(80, 13);
            this.lblBranch.TabIndex = 51;
            this.lblBranch.Text = "Bank Branch";
            // 
            // cmbBankCode
            // 
            this.cmbBankCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankCode.FormattingEnabled = true;
            this.cmbBankCode.Location = new System.Drawing.Point(607, 82);
            this.cmbBankCode.Name = "cmbBankCode";
            this.cmbBankCode.Size = new System.Drawing.Size(60, 21);
            this.cmbBankCode.TabIndex = 50;
            this.cmbBankCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankCode_KeyDown);
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(496, 85);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(36, 13);
            this.lblBank.TabIndex = 49;
            this.lblBank.Text = "Bank";
            // 
            // lblCardCheque
            // 
            this.lblCardCheque.AutoSize = true;
            this.lblCardCheque.Location = new System.Drawing.Point(496, 39);
            this.lblCardCheque.Name = "lblCardCheque";
            this.lblCardCheque.Size = new System.Drawing.Size(107, 13);
            this.lblCardCheque.TabIndex = 48;
            this.lblCardCheque.Text = "Card /Cheque No";
            // 
            // txtCardChequeNo
            // 
            this.txtCardChequeNo.Location = new System.Drawing.Point(607, 36);
            this.txtCardChequeNo.Name = "txtCardChequeNo";
            this.txtCardChequeNo.Size = new System.Drawing.Size(219, 21);
            this.txtCardChequeNo.TabIndex = 47;
            this.txtCardChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardChequeNo_KeyDown);
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.Location = new System.Drawing.Point(849, 159);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(100, 13);
            this.lblBalanceAmount.TabIndex = 45;
            this.lblBalanceAmount.Text = "Balance Amount";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(607, 12);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(219, 21);
            this.cmbPaymentMethod.TabIndex = 44;
            this.cmbPaymentMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentMethod_KeyDown);
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtBalanceAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalanceAmount.Location = new System.Drawing.Point(996, 156);
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.ReadOnly = true;
            this.txtBalanceAmount.Size = new System.Drawing.Size(134, 21);
            this.txtBalanceAmount.TabIndex = 43;
            this.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(496, 15);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(102, 13);
            this.lblPaymentMethod.TabIndex = 41;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(850, 135);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(869, 87);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 39;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(978, 40);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(996, 36);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.ReadOnly = true;
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(134, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 37;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(850, 39);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(850, 15);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpPayment);
            this.tbFooter.Controls.Add(this.tbpLoyalty);
            this.tbFooter.Controls.Add(this.tbpCreditCustomer);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Location = new System.Drawing.Point(1, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(476, 166);
            this.tbFooter.TabIndex = 34;
            // 
            // tbpPayment
            // 
            this.tbpPayment.Controls.Add(this.lblLedger);
            this.tbpPayment.Controls.Add(this.cmbLedgerName);
            this.tbpPayment.Controls.Add(this.cmbLedgerCode);
            this.tbpPayment.Controls.Add(this.dgvPaymentDetails);
            this.tbpPayment.Location = new System.Drawing.Point(4, 22);
            this.tbpPayment.Name = "tbpPayment";
            this.tbpPayment.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPayment.Size = new System.Drawing.Size(468, 140);
            this.tbpPayment.TabIndex = 0;
            this.tbpPayment.Text = "Payment Details";
            this.tbpPayment.UseVisualStyleBackColor = true;
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(4, 8);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(46, 13);
            this.lblLedger.TabIndex = 41;
            this.lblLedger.Text = "Ledger";
            // 
            // cmbLedgerName
            // 
            this.cmbLedgerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLedgerName.FormattingEnabled = true;
            this.cmbLedgerName.Location = new System.Drawing.Point(195, 5);
            this.cmbLedgerName.Name = "cmbLedgerName";
            this.cmbLedgerName.Size = new System.Drawing.Size(269, 21);
            this.cmbLedgerName.TabIndex = 40;
            // 
            // cmbLedgerCode
            // 
            this.cmbLedgerCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLedgerCode.FormattingEnabled = true;
            this.cmbLedgerCode.Location = new System.Drawing.Point(98, 5);
            this.cmbLedgerCode.Name = "cmbLedgerCode";
            this.cmbLedgerCode.Size = new System.Drawing.Size(93, 21);
            this.cmbLedgerCode.TabIndex = 39;
            // 
            // dgvPaymentDetails
            // 
            this.dgvPaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mode,
            this.No,
            this.Date,
            this.PayAmount,
            this.PaymentMethodID,
            this.PaymentTypeID});
            this.dgvPaymentDetails.Location = new System.Drawing.Point(2, 31);
            this.dgvPaymentDetails.Name = "dgvPaymentDetails";
            this.dgvPaymentDetails.RowHeadersWidth = 15;
            this.dgvPaymentDetails.Size = new System.Drawing.Size(462, 107);
            this.dgvPaymentDetails.TabIndex = 38;
            // 
            // Mode
            // 
            this.Mode.DataPropertyName = "PaymentMethod";
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            this.Mode.Width = 95;
            // 
            // No
            // 
            this.No.DataPropertyName = "CardChequeNo";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 140;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "ChequeDate";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 80;
            // 
            // PayAmount
            // 
            this.PayAmount.DataPropertyName = "PayAmount";
            this.PayAmount.HeaderText = "Amount";
            this.PayAmount.Name = "PayAmount";
            this.PayAmount.Width = 110;
            // 
            // PaymentMethodID
            // 
            this.PaymentMethodID.DataPropertyName = "PaymentMethodID";
            this.PaymentMethodID.HeaderText = "PaymentMethodID";
            this.PaymentMethodID.Name = "PaymentMethodID";
            this.PaymentMethodID.Visible = false;
            // 
            // PaymentTypeID
            // 
            this.PaymentTypeID.DataPropertyName = "PaymentTypeID";
            this.PaymentTypeID.HeaderText = "PaymentTypeID";
            this.PaymentTypeID.Name = "PaymentTypeID";
            this.PaymentTypeID.Visible = false;
            // 
            // tbpLoyalty
            // 
            this.tbpLoyalty.Controls.Add(this.lblLoyaltyCustomerName);
            this.tbpLoyalty.Controls.Add(this.lblCardType);
            this.tbpLoyalty.Controls.Add(this.txtCardType);
            this.tbpLoyalty.Controls.Add(this.txtLoyaltyCustomerName);
            this.tbpLoyalty.Controls.Add(this.lblLoyaltyCustomerCode);
            this.tbpLoyalty.Controls.Add(this.lblCardNo);
            this.tbpLoyalty.Controls.Add(this.txtLoyaltyCustomerCode);
            this.tbpLoyalty.Controls.Add(this.txtCardNo);
            this.tbpLoyalty.Location = new System.Drawing.Point(4, 22);
            this.tbpLoyalty.Name = "tbpLoyalty";
            this.tbpLoyalty.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLoyalty.Size = new System.Drawing.Size(468, 140);
            this.tbpLoyalty.TabIndex = 1;
            this.tbpLoyalty.Text = "Loyalty Customer";
            this.tbpLoyalty.UseVisualStyleBackColor = true;
            // 
            // lblLoyaltyCustomerName
            // 
            this.lblLoyaltyCustomerName.AutoSize = true;
            this.lblLoyaltyCustomerName.Location = new System.Drawing.Point(9, 57);
            this.lblLoyaltyCustomerName.Name = "lblLoyaltyCustomerName";
            this.lblLoyaltyCustomerName.Size = new System.Drawing.Size(145, 13);
            this.lblLoyaltyCustomerName.TabIndex = 48;
            this.lblLoyaltyCustomerName.Text = "Loyalty Customer Name";
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(274, 33);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(66, 13);
            this.lblCardType.TabIndex = 47;
            this.lblCardType.Text = "Card Type";
            // 
            // txtCardType
            // 
            this.txtCardType.Location = new System.Drawing.Point(346, 30);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(118, 21);
            this.txtCardType.TabIndex = 46;
            // 
            // txtLoyaltyCustomerName
            // 
            this.txtLoyaltyCustomerName.Location = new System.Drawing.Point(157, 54);
            this.txtLoyaltyCustomerName.Name = "txtLoyaltyCustomerName";
            this.txtLoyaltyCustomerName.Size = new System.Drawing.Size(307, 21);
            this.txtLoyaltyCustomerName.TabIndex = 45;
            // 
            // lblLoyaltyCustomerCode
            // 
            this.lblLoyaltyCustomerCode.AutoSize = true;
            this.lblLoyaltyCustomerCode.Location = new System.Drawing.Point(9, 33);
            this.lblLoyaltyCustomerCode.Name = "lblLoyaltyCustomerCode";
            this.lblLoyaltyCustomerCode.Size = new System.Drawing.Size(142, 13);
            this.lblLoyaltyCustomerCode.TabIndex = 44;
            this.lblLoyaltyCustomerCode.Text = "Loyalty Customer Code";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Location = new System.Drawing.Point(9, 8);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(54, 13);
            this.lblCardNo.TabIndex = 43;
            this.lblCardNo.Text = "Card No";
            // 
            // txtLoyaltyCustomerCode
            // 
            this.txtLoyaltyCustomerCode.Location = new System.Drawing.Point(157, 30);
            this.txtLoyaltyCustomerCode.Name = "txtLoyaltyCustomerCode";
            this.txtLoyaltyCustomerCode.Size = new System.Drawing.Size(111, 21);
            this.txtLoyaltyCustomerCode.TabIndex = 42;
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(157, 5);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(307, 21);
            this.txtCardNo.TabIndex = 41;
            // 
            // tbpCreditCustomer
            // 
            this.tbpCreditCustomer.Controls.Add(this.txtReturnCheques);
            this.tbpCreditCustomer.Controls.Add(this.lblReturnCheques);
            this.tbpCreditCustomer.Controls.Add(this.txtNoOfReturnCheques);
            this.tbpCreditCustomer.Controls.Add(this.txtPostDatedCheques);
            this.tbpCreditCustomer.Controls.Add(this.lblPostDatedCheques);
            this.tbpCreditCustomer.Controls.Add(this.txtNoOfPostDatedCheques);
            this.tbpCreditCustomer.Controls.Add(this.lblOutstanding);
            this.tbpCreditCustomer.Controls.Add(this.txtOutstanding);
            this.tbpCreditCustomer.Controls.Add(this.lblPaymentTerms);
            this.tbpCreditCustomer.Controls.Add(this.lblCreditLimit);
            this.tbpCreditCustomer.Controls.Add(this.txtPaymentTerms);
            this.tbpCreditCustomer.Controls.Add(this.txtCreditLimit);
            this.tbpCreditCustomer.Location = new System.Drawing.Point(4, 22);
            this.tbpCreditCustomer.Name = "tbpCreditCustomer";
            this.tbpCreditCustomer.Size = new System.Drawing.Size(468, 140);
            this.tbpCreditCustomer.TabIndex = 3;
            this.tbpCreditCustomer.Text = "Credit Customer";
            this.tbpCreditCustomer.UseVisualStyleBackColor = true;
            // 
            // txtReturnCheques
            // 
            this.txtReturnCheques.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtReturnCheques.Location = new System.Drawing.Point(135, 76);
            this.txtReturnCheques.Name = "txtReturnCheques";
            this.txtReturnCheques.Size = new System.Drawing.Size(123, 21);
            this.txtReturnCheques.TabIndex = 60;
            this.txtReturnCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblReturnCheques
            // 
            this.lblReturnCheques.AutoSize = true;
            this.lblReturnCheques.Location = new System.Drawing.Point(9, 79);
            this.lblReturnCheques.Name = "lblReturnCheques";
            this.lblReturnCheques.Size = new System.Drawing.Size(99, 13);
            this.lblReturnCheques.TabIndex = 59;
            this.lblReturnCheques.Text = "Return Cheques";
            // 
            // txtNoOfReturnCheques
            // 
            this.txtNoOfReturnCheques.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNoOfReturnCheques.Location = new System.Drawing.Point(261, 76);
            this.txtNoOfReturnCheques.Name = "txtNoOfReturnCheques";
            this.txtNoOfReturnCheques.Size = new System.Drawing.Size(43, 21);
            this.txtNoOfReturnCheques.TabIndex = 58;
            this.txtNoOfReturnCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPostDatedCheques
            // 
            this.txtPostDatedCheques.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPostDatedCheques.Location = new System.Drawing.Point(135, 52);
            this.txtPostDatedCheques.Name = "txtPostDatedCheques";
            this.txtPostDatedCheques.Size = new System.Drawing.Size(123, 21);
            this.txtPostDatedCheques.TabIndex = 57;
            this.txtPostDatedCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPostDatedCheques
            // 
            this.lblPostDatedCheques.AutoSize = true;
            this.lblPostDatedCheques.Location = new System.Drawing.Point(9, 55);
            this.lblPostDatedCheques.Name = "lblPostDatedCheques";
            this.lblPostDatedCheques.Size = new System.Drawing.Size(123, 13);
            this.lblPostDatedCheques.TabIndex = 56;
            this.lblPostDatedCheques.Text = "Post Dated Cheques";
            // 
            // txtNoOfPostDatedCheques
            // 
            this.txtNoOfPostDatedCheques.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNoOfPostDatedCheques.Location = new System.Drawing.Point(261, 52);
            this.txtNoOfPostDatedCheques.Name = "txtNoOfPostDatedCheques";
            this.txtNoOfPostDatedCheques.Size = new System.Drawing.Size(43, 21);
            this.txtNoOfPostDatedCheques.TabIndex = 55;
            this.txtNoOfPostDatedCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOutstanding
            // 
            this.lblOutstanding.AutoSize = true;
            this.lblOutstanding.Location = new System.Drawing.Point(259, 9);
            this.lblOutstanding.Name = "lblOutstanding";
            this.lblOutstanding.Size = new System.Drawing.Size(75, 13);
            this.lblOutstanding.TabIndex = 54;
            this.lblOutstanding.Text = "Outstanding";
            // 
            // txtOutstanding
            // 
            this.txtOutstanding.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtOutstanding.Location = new System.Drawing.Point(340, 6);
            this.txtOutstanding.Name = "txtOutstanding";
            this.txtOutstanding.Size = new System.Drawing.Size(123, 21);
            this.txtOutstanding.TabIndex = 53;
            this.txtOutstanding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(9, 32);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(96, 13);
            this.lblPaymentTerms.TabIndex = 52;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // lblCreditLimit
            // 
            this.lblCreditLimit.AutoSize = true;
            this.lblCreditLimit.Location = new System.Drawing.Point(9, 8);
            this.lblCreditLimit.Name = "lblCreditLimit";
            this.lblCreditLimit.Size = new System.Drawing.Size(73, 13);
            this.lblCreditLimit.TabIndex = 51;
            this.lblCreditLimit.Text = "Credit Limit";
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPaymentTerms.Location = new System.Drawing.Point(135, 28);
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.Size = new System.Drawing.Size(123, 21);
            this.txtPaymentTerms.TabIndex = 50;
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreditLimit.Location = new System.Drawing.Point(135, 4);
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(123, 21);
            this.txtCreditLimit.TabIndex = 49;
            this.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbpPageSetup
            // 
            this.tbpPageSetup.Controls.Add(this.rdoLandscape);
            this.tbpPageSetup.Controls.Add(this.rdoPortrait);
            this.tbpPageSetup.Controls.Add(this.cmbPaperSize);
            this.tbpPageSetup.Controls.Add(this.lblOrientation);
            this.tbpPageSetup.Controls.Add(this.cmbPrinter);
            this.tbpPageSetup.Controls.Add(this.lblPaperSize);
            this.tbpPageSetup.Controls.Add(this.lblPrinter);
            this.tbpPageSetup.Location = new System.Drawing.Point(4, 22);
            this.tbpPageSetup.Name = "tbpPageSetup";
            this.tbpPageSetup.Size = new System.Drawing.Size(468, 140);
            this.tbpPageSetup.TabIndex = 2;
            this.tbpPageSetup.Text = "Page Setup";
            this.tbpPageSetup.UseVisualStyleBackColor = true;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Location = new System.Drawing.Point(234, 57);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(85, 17);
            this.rdoLandscape.TabIndex = 51;
            this.rdoLandscape.TabStop = true;
            this.rdoLandscape.Text = "Landscape";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Location = new System.Drawing.Point(125, 57);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(67, 17);
            this.rdoPortrait.TabIndex = 50;
            this.rdoPortrait.TabStop = true;
            this.rdoPortrait.Text = "Portrait";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Location = new System.Drawing.Point(125, 30);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(207, 21);
            this.cmbPaperSize.TabIndex = 49;
            // 
            // lblOrientation
            // 
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new System.Drawing.Point(9, 59);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(70, 13);
            this.lblOrientation.TabIndex = 48;
            this.lblOrientation.Text = "Orientation";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 5);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(207, 21);
            this.cmbPrinter.TabIndex = 47;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(9, 33);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(68, 13);
            this.lblPaperSize.TabIndex = 46;
            this.lblPaperSize.Text = "Paper Size";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(9, 8);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 45;
            this.lblPrinter.Text = "Printer";
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
            this.txtNetAmount.Location = new System.Drawing.Point(996, 132);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(134, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalTaxAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(996, 84);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(134, 21);
            this.txtTotalTaxAmount.TabIndex = 32;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtSubTotalDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(996, 60);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(134, 21);
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
            this.txtGrossAmount.Location = new System.Drawing.Point(996, 12);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(134, 21);
            this.txtGrossAmount.TabIndex = 30;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmSalesReturnNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1138, 554);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmSalesReturnNote";
            this.Text = "Sales Return Note";
            this.Load += new System.EventHandler(this.FrmSalesReturnNote_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.tbBody.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.tbpAdvanced.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.tbFooter.ResumeLayout(false);
            this.tbpPayment.ResumeLayout(false);
            this.tbpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetails)).EndInit();
            this.tbpLoyalty.ResumeLayout(false);
            this.tbpLoyalty.PerformLayout();
            this.tbpCreditCustomer.ResumeLayout(false);
            this.tbpCreditCustomer.PerformLayout();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Button btnInvoiceDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblCrnDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationInvoiceNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.ComboBox cmbBranchName;
        private System.Windows.Forms.ComboBox cmbBankName;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.ComboBox cmbBranchCode;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBankCode;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblCardCheque;
        private System.Windows.Forms.TextBox txtCardChequeNo;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private CustomControls.TextBoxCurrency txtBalanceAmount;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tbpPayment;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.ComboBox cmbLedgerName;
        private System.Windows.Forms.ComboBox cmbLedgerCode;
        private System.Windows.Forms.DataGridView dgvPaymentDetails;
        private System.Windows.Forms.TabPage tbpLoyalty;
        private System.Windows.Forms.Label lblLoyaltyCustomerName;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.TextBox txtLoyaltyCustomerName;
        private System.Windows.Forms.Label lblLoyaltyCustomerCode;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.TextBox txtLoyaltyCustomerCode;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.TabPage tbpCreditCustomer;
        private System.Windows.Forms.TextBox txtReturnCheques;
        private System.Windows.Forms.Label lblReturnCheques;
        private System.Windows.Forms.TextBox txtNoOfReturnCheques;
        private System.Windows.Forms.TextBox txtPostDatedCheques;
        private System.Windows.Forms.Label lblPostDatedCheques;
        private System.Windows.Forms.TextBox txtNoOfPostDatedCheques;
        private System.Windows.Forms.Label lblOutstanding;
        private System.Windows.Forms.TextBox txtOutstanding;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.Label lblCreditLimit;
        private System.Windows.Forms.TextBox txtPaymentTerms;
        private System.Windows.Forms.TextBox txtCreditLimit;
        private System.Windows.Forms.TabPage tbpPageSetup;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblPrinter;
        private CustomControls.TextBoxCurrency txtNetAmount;
        private CustomControls.TextBoxCurrency txtTotalTaxAmount;
        private CustomControls.TextBoxCurrency txtSubTotalDiscount;
        private CustomControls.TextBoxCurrency txtGrossAmount;
        private System.Windows.Forms.ComboBox cmbReturnType;
        private System.Windows.Forms.Label lblReturnType;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TabControl tbBody;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.ComboBox cmbUnit;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private CustomControls.TextBoxCurrency txtProductDiscountAmount;
        private CustomControls.TextBoxPercentGen txtProductDiscountPercentage;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtFreeQty;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TabPage tbpAdvanced;
        private System.Windows.Forms.Label lblOtherCharges;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private System.Windows.Forms.DataGridViewComboBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.Label lblPaidAmount;
        private CustomControls.TextBoxCurrency txtPaidAmount;
        private System.Windows.Forms.Label lblPayingAmount;
        private CustomControls.TextBoxCurrency txtPayingAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethodID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentTypeID;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreeQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
