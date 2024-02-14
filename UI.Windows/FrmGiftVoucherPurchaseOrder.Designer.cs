namespace UI.Windows
{
    partial class FrmGiftVoucherPurchaseOrder
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.btnPaymentTermLimits = new System.Windows.Forms.Button();
            this.cmbPaymentTerms = new System.Windows.Forms.ComboBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.txtValidityPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblValidityDays = new System.Windows.Forms.Label();
            this.lblValidityPeriod = new System.Windows.Forms.Label();
            this.dtpExpectedDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpectedDate = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblDocumentDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationPoNo = new System.Windows.Forms.CheckBox();
            this.txtPurchaseOrderNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtVoucherValue = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtVoucherSerial = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationVoucher = new System.Windows.Forms.CheckBox();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.dgvVoucherBookDetails = new System.Windows.Forms.DataGridView();
            this.lineNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherSerialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invGiftVoucherPurchaseOrderDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.chkRound = new System.Windows.Forms.CheckBox();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.txtTotalTaxAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new Glass.GlassButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationGroup = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationBook = new System.Windows.Forms.CheckBox();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cmbBasedOn = new System.Windows.Forms.ComboBox();
            this.lblSelectionCriteria = new System.Windows.Forms.Label();
            this.txtGroupCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.cmbSelectionCriteria = new System.Windows.Forms.ComboBox();
            this.txtGroupName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblGiftVoucherGroup = new System.Windows.Forms.Label();
            this.lblBook = new System.Windows.Forms.Label();
            this.txtBookCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.rdoCoupon = new System.Windows.Forms.RadioButton();
            this.rdoVoucher = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtNoOfVouchersOnBook = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblNoOfVouchersOnBook = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPercentageOfCoupon = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGiftVoucherValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblGiftVoucherValue = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbMore = new System.Windows.Forms.TabControl();
            this.tbpVoucherSerial = new System.Windows.Forms.TabPage();
            this.lblVoucherSerialQty = new System.Windows.Forms.Label();
            this.txtVoucherSerialQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtVoucherSerialTo = new System.Windows.Forms.TextBox();
            this.txtVoucherSerialFrom = new System.Windows.Forms.TextBox();
            this.lblVoucherSerialTo = new System.Windows.Forms.Label();
            this.lblVoucherSerialFrom = new System.Windows.Forms.Label();
            this.tbpVoucherNo = new System.Windows.Forms.TabPage();
            this.lblVoucherNoQty = new System.Windows.Forms.Label();
            this.txtVoucherNoQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtVoucherNoTo = new System.Windows.Forms.TextBox();
            this.txtVoucherNoFrom = new System.Windows.Forms.TextBox();
            this.lblVoucherNoTo = new System.Windows.Forms.Label();
            this.lblVoucherNoFrom = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblGiftVoucherQty = new System.Windows.Forms.Label();
            this.txtGiftVoucherQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherBookDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherPurchaseOrderDetailTempBindingSource)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbMore.SuspendLayout();
            this.tbpVoucherSerial.SuspendLayout();
            this.tbpVoucherNo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(720, 524);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 524);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.btnPaymentTermLimits);
            this.grpHeader.Controls.Add(this.cmbPaymentTerms);
            this.grpHeader.Controls.Add(this.lblPaymentTerms);
            this.grpHeader.Controls.Add(this.txtValidityPeriod);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.lblValidityDays);
            this.grpHeader.Controls.Add(this.lblValidityPeriod);
            this.grpHeader.Controls.Add(this.dtpExpectedDate);
            this.grpHeader.Controls.Add(this.lblExpectedDate);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.lblDocumentDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPoNo);
            this.grpHeader.Controls.Add(this.txtPurchaseOrderNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1036, 86);
            this.grpHeader.TabIndex = 18;
            this.grpHeader.TabStop = false;
            // 
            // btnPaymentTermLimits
            // 
            this.btnPaymentTermLimits.ForeColor = System.Drawing.Color.Black;
            this.btnPaymentTermLimits.Location = new System.Drawing.Point(1015, 62);
            this.btnPaymentTermLimits.Name = "btnPaymentTermLimits";
            this.btnPaymentTermLimits.Size = new System.Drawing.Size(17, 21);
            this.btnPaymentTermLimits.TabIndex = 89;
            this.btnPaymentTermLimits.Text = "?";
            this.btnPaymentTermLimits.UseVisualStyleBackColor = true;
            this.btnPaymentTermLimits.Click += new System.EventHandler(this.btnPaymentTermLimits_Click);
            // 
            // cmbPaymentTerms
            // 
            this.cmbPaymentTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTerms.FormattingEnabled = true;
            this.cmbPaymentTerms.Location = new System.Drawing.Point(902, 62);
            this.cmbPaymentTerms.Name = "cmbPaymentTerms";
            this.cmbPaymentTerms.Size = new System.Drawing.Size(107, 21);
            this.cmbPaymentTerms.TabIndex = 70;
            this.cmbPaymentTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentTerms_KeyDown);
            this.cmbPaymentTerms.Validated += new System.EventHandler(this.cmbPaymentTerms_Validated);
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(803, 62);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(96, 13);
            this.lblPaymentTerms.TabIndex = 71;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // txtValidityPeriod
            // 
            this.txtValidityPeriod.IntValue = 0;
            this.txtValidityPeriod.Location = new System.Drawing.Point(607, 36);
            this.txtValidityPeriod.Name = "txtValidityPeriod";
            this.txtValidityPeriod.Size = new System.Drawing.Size(84, 21);
            this.txtValidityPeriod.TabIndex = 6;
            this.txtValidityPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValidityPeriod_KeyDown);
            this.txtValidityPeriod.Validated += new System.EventHandler(this.txtValidityPeriod_Validated);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(607, 59);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(179, 21);
            this.cmbLocation.TabIndex = 8;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(513, 62);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Location";
            // 
            // lblValidityDays
            // 
            this.lblValidityDays.AutoSize = true;
            this.lblValidityDays.Location = new System.Drawing.Point(697, 39);
            this.lblValidityDays.Name = "lblValidityDays";
            this.lblValidityDays.Size = new System.Drawing.Size(36, 13);
            this.lblValidityDays.TabIndex = 43;
            this.lblValidityDays.Text = "Days";
            // 
            // lblValidityPeriod
            // 
            this.lblValidityPeriod.AutoSize = true;
            this.lblValidityPeriod.Location = new System.Drawing.Point(512, 39);
            this.lblValidityPeriod.Name = "lblValidityPeriod";
            this.lblValidityPeriod.Size = new System.Drawing.Size(88, 13);
            this.lblValidityPeriod.TabIndex = 42;
            this.lblValidityPeriod.Text = "Validity Period";
            // 
            // dtpExpectedDate
            // 
            this.dtpExpectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpectedDate.Location = new System.Drawing.Point(607, 13);
            this.dtpExpectedDate.Name = "dtpExpectedDate";
            this.dtpExpectedDate.Size = new System.Drawing.Size(126, 21);
            this.dtpExpectedDate.TabIndex = 4;
            this.dtpExpectedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpectedDate_KeyDown);
            // 
            // lblExpectedDate
            // 
            this.lblExpectedDate.AutoSize = true;
            this.lblExpectedDate.Location = new System.Drawing.Point(512, 17);
            this.lblExpectedDate.Name = "lblExpectedDate";
            this.lblExpectedDate.Size = new System.Drawing.Size(90, 13);
            this.lblExpectedDate.TabIndex = 38;
            this.lblExpectedDate.Text = "Expected Date";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(902, 36);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(130, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(803, 39);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(902, 12);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(129, 21);
            this.dtpDocumentDate.TabIndex = 5;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(247, 12);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 2;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            this.btnDocumentDetails.Click += new System.EventHandler(this.btnDocumentDetails_Click);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(7, 62);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 59);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(397, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(7, 39);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 13;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(247, 36);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(260, 21);
            this.txtSupplierName.TabIndex = 8;
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierName_KeyDown);
            this.txtSupplierName.Validated += new System.EventHandler(this.txtSupplierName_Validated);
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(93, 39);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 6;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSupplier_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(110, 36);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 2;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            this.txtSupplierCode.Validated += new System.EventHandler(this.txtSupplierCode_Validated);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(803, 17);
            this.lblDocumentDate.Name = "lblDocumentDate";
            this.lblDocumentDate.Size = new System.Drawing.Size(96, 13);
            this.lblDocumentDate.TabIndex = 9;
            this.lblDocumentDate.Text = "Document Date";
            // 
            // chkAutoCompleationPoNo
            // 
            this.chkAutoCompleationPoNo.AutoSize = true;
            this.chkAutoCompleationPoNo.Checked = true;
            this.chkAutoCompleationPoNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPoNo.Location = new System.Drawing.Point(93, 17);
            this.chkAutoCompleationPoNo.Name = "chkAutoCompleationPoNo";
            this.chkAutoCompleationPoNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPoNo.TabIndex = 0;
            this.chkAutoCompleationPoNo.Tag = "1";
            this.chkAutoCompleationPoNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPoNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPoNo_CheckedChanged);
            // 
            // txtPurchaseOrderNo
            // 
            this.txtPurchaseOrderNo.Location = new System.Drawing.Point(110, 13);
            this.txtPurchaseOrderNo.Name = "txtPurchaseOrderNo";
            this.txtPurchaseOrderNo.Size = new System.Drawing.Size(136, 21);
            this.txtPurchaseOrderNo.TabIndex = 1;
            this.txtPurchaseOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseOrderNo_KeyDown);
            // 
            // lblPurchaseOrderNo
            // 
            this.lblPurchaseOrderNo.AutoSize = true;
            this.lblPurchaseOrderNo.Location = new System.Drawing.Point(7, 17);
            this.lblPurchaseOrderNo.Name = "lblPurchaseOrderNo";
            this.lblPurchaseOrderNo.Size = new System.Drawing.Size(84, 13);
            this.lblPurchaseOrderNo.TabIndex = 1;
            this.lblPurchaseOrderNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtVoucherValue);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtVoucherSerial);
            this.grpBody.Controls.Add(this.chkAutoCompleationVoucher);
            this.grpBody.Controls.Add(this.txtVoucherNo);
            this.grpBody.Controls.Add(this.dgvVoucherBookDetails);
            this.grpBody.Location = new System.Drawing.Point(1, 75);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1036, 224);
            this.grpBody.TabIndex = 19;
            this.grpBody.TabStop = false;
            // 
            // txtVoucherValue
            // 
            this.txtVoucherValue.Location = new System.Drawing.Point(671, 224);
            this.txtVoucherValue.Name = "txtVoucherValue";
            this.txtVoucherValue.Size = new System.Drawing.Size(148, 21);
            this.txtVoucherValue.TabIndex = 28;
            this.txtVoucherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVoucherValue.Visible = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(821, 224);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(151, 21);
            this.txtQty.TabIndex = 27;
            this.txtQty.Visible = false;
            // 
            // txtVoucherSerial
            // 
            this.txtVoucherSerial.Location = new System.Drawing.Point(296, 224);
            this.txtVoucherSerial.Name = "txtVoucherSerial";
            this.txtVoucherSerial.Size = new System.Drawing.Size(373, 21);
            this.txtVoucherSerial.TabIndex = 26;
            this.txtVoucherSerial.Visible = false;
            // 
            // chkAutoCompleationVoucher
            // 
            this.chkAutoCompleationVoucher.AutoSize = true;
            this.chkAutoCompleationVoucher.Location = new System.Drawing.Point(10, 227);
            this.chkAutoCompleationVoucher.Name = "chkAutoCompleationVoucher";
            this.chkAutoCompleationVoucher.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationVoucher.TabIndex = 24;
            this.chkAutoCompleationVoucher.UseVisualStyleBackColor = true;
            this.chkAutoCompleationVoucher.Visible = false;
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.Location = new System.Drawing.Point(31, 224);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(263, 21);
            this.txtVoucherNo.TabIndex = 25;
            this.txtVoucherNo.Visible = false;
            // 
            // dgvVoucherBookDetails
            // 
            this.dgvVoucherBookDetails.AutoGenerateColumns = false;
            this.dgvVoucherBookDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucherBookDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNoDataGridViewTextBoxColumn,
            this.voucherNoDataGridViewTextBoxColumn,
            this.voucherSerialDataGridViewTextBoxColumn,
            this.voucherAmountDataGridViewTextBoxColumn});
            this.dgvVoucherBookDetails.DataSource = this.invGiftVoucherPurchaseOrderDetailTempBindingSource;
            this.dgvVoucherBookDetails.Location = new System.Drawing.Point(10, 11);
            this.dgvVoucherBookDetails.Name = "dgvVoucherBookDetails";
            this.dgvVoucherBookDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucherBookDetails.Size = new System.Drawing.Size(1021, 207);
            this.dgvVoucherBookDetails.TabIndex = 1;
            this.dgvVoucherBookDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvVoucherBookDetails_KeyDown);
            // 
            // lineNoDataGridViewTextBoxColumn
            // 
            this.lineNoDataGridViewTextBoxColumn.DataPropertyName = "LineNo";
            this.lineNoDataGridViewTextBoxColumn.HeaderText = "Raw";
            this.lineNoDataGridViewTextBoxColumn.Name = "lineNoDataGridViewTextBoxColumn";
            this.lineNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // voucherNoDataGridViewTextBoxColumn
            // 
            this.voucherNoDataGridViewTextBoxColumn.DataPropertyName = "VoucherNo";
            this.voucherNoDataGridViewTextBoxColumn.HeaderText = "Voucher No";
            this.voucherNoDataGridViewTextBoxColumn.Name = "voucherNoDataGridViewTextBoxColumn";
            this.voucherNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.voucherNoDataGridViewTextBoxColumn.Width = 255;
            // 
            // voucherSerialDataGridViewTextBoxColumn
            // 
            this.voucherSerialDataGridViewTextBoxColumn.DataPropertyName = "VoucherSerial";
            this.voucherSerialDataGridViewTextBoxColumn.HeaderText = "Voucher Serial";
            this.voucherSerialDataGridViewTextBoxColumn.Name = "voucherSerialDataGridViewTextBoxColumn";
            this.voucherSerialDataGridViewTextBoxColumn.ReadOnly = true;
            this.voucherSerialDataGridViewTextBoxColumn.Width = 255;
            // 
            // voucherAmountDataGridViewTextBoxColumn
            // 
            this.voucherAmountDataGridViewTextBoxColumn.DataPropertyName = "VoucherValue";
            this.voucherAmountDataGridViewTextBoxColumn.HeaderText = "Voucher Value";
            this.voucherAmountDataGridViewTextBoxColumn.Name = "voucherAmountDataGridViewTextBoxColumn";
            this.voucherAmountDataGridViewTextBoxColumn.ReadOnly = true;
            this.voucherAmountDataGridViewTextBoxColumn.Width = 250;
            // 
            // invGiftVoucherPurchaseOrderDetailTempBindingSource
            // 
            this.invGiftVoucherPurchaseOrderDetailTempBindingSource.DataSource = typeof(Domain.InvGiftVoucherPurchaseOrderDetailTemp);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.chkRound);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Location = new System.Drawing.Point(692, 294);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(344, 236);
            this.grpFooter.TabIndex = 20;
            this.grpFooter.TabStop = false;
            // 
            // chkRound
            // 
            this.chkRound.AutoSize = true;
            this.chkRound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRound.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRound.Location = new System.Drawing.Point(122, 211);
            this.chkRound.Name = "chkRound";
            this.chkRound.Size = new System.Drawing.Size(62, 17);
            this.chkRound.TabIndex = 101;
            this.chkRound.Text = "Round";
            this.chkRound.UseVisualStyleBackColor = true;
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(37, 187);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 100;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(37, 159);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 98;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(169, 163);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 97;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.Window;
            this.txtOtherCharges.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharges.Location = new System.Drawing.Point(186, 184);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(151, 21);
            this.txtOtherCharges.TabIndex = 99;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyDown);
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            this.txtOtherCharges.Validated += new System.EventHandler(this.txtOtherCharges_Validated);
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(54, 163);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 96;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(186, 160);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(151, 21);
            this.txtTotalTaxAmount.TabIndex = 95;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(186, 137);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(151, 21);
            this.txtSubTotalDiscount.TabIndex = 84;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(144, 117);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 83;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(37, 212);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 82;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(168, 117);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 81;
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
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(186, 113);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(151, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 17;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyDown);
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Validated += new System.EventHandler(this.txtSubTotalDiscountPercentage_Validated);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(37, 116);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 79;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(37, 93);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 78;
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
            this.txtNetAmount.Location = new System.Drawing.Point(186, 209);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(151, 21);
            this.txtNetAmount.TabIndex = 77;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtGrossAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGrossAmount.Location = new System.Drawing.Point(186, 89);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.Size = new System.Drawing.Size(151, 21);
            this.txtGrossAmount.TabIndex = 16;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGrossAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrossAmount_KeyDown);
            this.txtGrossAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGrossAmount_KeyUp);
            this.txtGrossAmount.Validated += new System.EventHandler(this.txtGrossAmount_Validated);
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(37, 21);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(58, 13);
            this.lblTotalQty.TabIndex = 63;
            this.lblTotalQty.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Location = new System.Drawing.Point(186, 18);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Size = new System.Drawing.Size(151, 21);
            this.txtTotalQty.TabIndex = 62;
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLoad);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.rdoCoupon);
            this.groupBox4.Controls.Add(this.rdoVoucher);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(1, 294);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(689, 236);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoad.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(607, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 27);
            this.btnLoad.TabIndex = 114;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoCompleationGroup);
            this.groupBox1.Controls.Add(this.chkAutoCompleationBook);
            this.groupBox1.Controls.Add(this.lblBasedOn);
            this.groupBox1.Controls.Add(this.cmbBasedOn);
            this.groupBox1.Controls.Add(this.lblSelectionCriteria);
            this.groupBox1.Controls.Add(this.txtGroupCode);
            this.groupBox1.Controls.Add(this.cmbSelectionCriteria);
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.lblGiftVoucherGroup);
            this.groupBox1.Controls.Add(this.lblBook);
            this.groupBox1.Controls.Add(this.txtBookCode);
            this.groupBox1.Controls.Add(this.txtBookName);
            this.groupBox1.Location = new System.Drawing.Point(5, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 83);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoCompleationGroup
            // 
            this.chkAutoCompleationGroup.AutoSize = true;
            this.chkAutoCompleationGroup.Checked = true;
            this.chkAutoCompleationGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGroup.Location = new System.Drawing.Point(125, 15);
            this.chkAutoCompleationGroup.Name = "chkAutoCompleationGroup";
            this.chkAutoCompleationGroup.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGroup.TabIndex = 131;
            this.chkAutoCompleationGroup.Tag = "1";
            this.chkAutoCompleationGroup.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGroup.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGroup_CheckedChanged);
            // 
            // chkAutoCompleationBook
            // 
            this.chkAutoCompleationBook.AutoSize = true;
            this.chkAutoCompleationBook.Checked = true;
            this.chkAutoCompleationBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBook.Location = new System.Drawing.Point(125, 38);
            this.chkAutoCompleationBook.Name = "chkAutoCompleationBook";
            this.chkAutoCompleationBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBook.TabIndex = 130;
            this.chkAutoCompleationBook.Tag = "1";
            this.chkAutoCompleationBook.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBook.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBook_CheckedChanged);
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(1, 65);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(62, 13);
            this.lblBasedOn.TabIndex = 129;
            this.lblBasedOn.Text = "Based On";
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Items.AddRange(new object[] {
            "Book Serial",
            "Voucher Serial"});
            this.cmbBasedOn.Location = new System.Drawing.Point(146, 59);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(164, 21);
            this.cmbBasedOn.TabIndex = 128;
            // 
            // lblSelectionCriteria
            // 
            this.lblSelectionCriteria.AutoSize = true;
            this.lblSelectionCriteria.Location = new System.Drawing.Point(314, 65);
            this.lblSelectionCriteria.Name = "lblSelectionCriteria";
            this.lblSelectionCriteria.Size = new System.Drawing.Size(106, 13);
            this.lblSelectionCriteria.TabIndex = 59;
            this.lblSelectionCriteria.Text = "Selection Criteria";
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.IsAutoComplete = false;
            this.txtGroupCode.ItemCollection = null;
            this.txtGroupCode.Location = new System.Drawing.Point(146, 13);
            this.txtGroupCode.MasterCode = "";
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(158, 21);
            this.txtGroupCode.TabIndex = 11;
            this.txtGroupCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupCode_KeyDown);
            this.txtGroupCode.Validated += new System.EventHandler(this.txtGroupCode_Validated);
            // 
            // cmbSelectionCriteria
            // 
            this.cmbSelectionCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionCriteria.FormattingEnabled = true;
            this.cmbSelectionCriteria.Items.AddRange(new object[] {
            "Base On Voucher Quantity",
            "Base On Voucher No Range",
            "Base On Voucher Serial Range"});
            this.cmbSelectionCriteria.Location = new System.Drawing.Point(428, 59);
            this.cmbSelectionCriteria.Name = "cmbSelectionCriteria";
            this.cmbSelectionCriteria.Size = new System.Drawing.Size(247, 21);
            this.cmbSelectionCriteria.TabIndex = 9;
            this.cmbSelectionCriteria.SelectedValueChanged += new System.EventHandler(this.cmbSelectionCriteria_SelectedValueChanged);
            this.cmbSelectionCriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelectionCriteria_KeyDown);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(306, 13);
            this.txtGroupName.MasterDescription = "";
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(369, 21);
            this.txtGroupName.TabIndex = 124;
            this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
            this.txtGroupName.Validated += new System.EventHandler(this.txtGroupName_Validated);
            // 
            // lblGiftVoucherGroup
            // 
            this.lblGiftVoucherGroup.AutoSize = true;
            this.lblGiftVoucherGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherGroup.Location = new System.Drawing.Point(1, 13);
            this.lblGiftVoucherGroup.Name = "lblGiftVoucherGroup";
            this.lblGiftVoucherGroup.Size = new System.Drawing.Size(116, 13);
            this.lblGiftVoucherGroup.TabIndex = 125;
            this.lblGiftVoucherGroup.Text = "Gift Voucher Group";
            // 
            // lblBook
            // 
            this.lblBook.AutoSize = true;
            this.lblBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBook.Location = new System.Drawing.Point(1, 36);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(36, 13);
            this.lblBook.TabIndex = 122;
            this.lblBook.Text = "Book";
            // 
            // txtBookCode
            // 
            this.txtBookCode.IsAutoComplete = false;
            this.txtBookCode.ItemCollection = null;
            this.txtBookCode.Location = new System.Drawing.Point(146, 36);
            this.txtBookCode.MasterCode = "";
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(158, 21);
            this.txtBookCode.TabIndex = 10;
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.Validated += new System.EventHandler(this.txtBookCode_Validated);
            // 
            // txtBookName
            // 
            this.txtBookName.Location = new System.Drawing.Point(306, 36);
            this.txtBookName.MasterDescription = "";
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(369, 21);
            this.txtBookName.TabIndex = 119;
            this.txtBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookName_KeyDown);
            this.txtBookName.Validated += new System.EventHandler(this.txtBookName_Validated);
            // 
            // rdoCoupon
            // 
            this.rdoCoupon.AutoSize = true;
            this.rdoCoupon.Location = new System.Drawing.Point(228, 15);
            this.rdoCoupon.Name = "rdoCoupon";
            this.rdoCoupon.Size = new System.Drawing.Size(69, 17);
            this.rdoCoupon.TabIndex = 113;
            this.rdoCoupon.Text = "Coupon";
            this.rdoCoupon.UseVisualStyleBackColor = true;
            this.rdoCoupon.CheckedChanged += new System.EventHandler(this.rdoCoupon_CheckedChanged);
            // 
            // rdoVoucher
            // 
            this.rdoVoucher.AutoSize = true;
            this.rdoVoucher.Checked = true;
            this.rdoVoucher.Location = new System.Drawing.Point(127, 15);
            this.rdoVoucher.Name = "rdoVoucher";
            this.rdoVoucher.Size = new System.Drawing.Size(71, 17);
            this.rdoVoucher.TabIndex = 112;
            this.rdoVoucher.TabStop = true;
            this.rdoVoucher.Text = "Voucher";
            this.rdoVoucher.UseVisualStyleBackColor = true;
            this.rdoVoucher.CheckedChanged += new System.EventHandler(this.rdoVoucher_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtNoOfVouchersOnBook);
            this.groupBox5.Controls.Add(this.lblNoOfVouchersOnBook);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtPercentageOfCoupon);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtGiftVoucherValue);
            this.groupBox5.Controls.Add(this.lblGiftVoucherValue);
            this.groupBox5.Location = new System.Drawing.Point(5, 109);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(442, 85);
            this.groupBox5.TabIndex = 111;
            this.groupBox5.TabStop = false;
            // 
            // txtNoOfVouchersOnBook
            // 
            this.txtNoOfVouchersOnBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNoOfVouchersOnBook.IntValue = 0;
            this.txtNoOfVouchersOnBook.Location = new System.Drawing.Point(165, 53);
            this.txtNoOfVouchersOnBook.MaxLength = 1;
            this.txtNoOfVouchersOnBook.Name = "txtNoOfVouchersOnBook";
            this.txtNoOfVouchersOnBook.ReadOnly = true;
            this.txtNoOfVouchersOnBook.Size = new System.Drawing.Size(43, 21);
            this.txtNoOfVouchersOnBook.TabIndex = 13;
            // 
            // lblNoOfVouchersOnBook
            // 
            this.lblNoOfVouchersOnBook.AutoSize = true;
            this.lblNoOfVouchersOnBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVouchersOnBook.Location = new System.Drawing.Point(1, 58);
            this.lblNoOfVouchersOnBook.Name = "lblNoOfVouchersOnBook";
            this.lblNoOfVouchersOnBook.Size = new System.Drawing.Size(144, 13);
            this.lblNoOfVouchersOnBook.TabIndex = 127;
            this.lblNoOfVouchersOnBook.Text = "No of Vouchers on Book";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 130;
            this.label7.Text = "%";
            // 
            // txtPercentageOfCoupon
            // 
            this.txtPercentageOfCoupon.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPercentageOfCoupon.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPercentageOfCoupon.Location = new System.Drawing.Point(147, 23);
            this.txtPercentageOfCoupon.Name = "txtPercentageOfCoupon";
            this.txtPercentageOfCoupon.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPercentageOfCoupon.Size = new System.Drawing.Size(61, 21);
            this.txtPercentageOfCoupon.TabIndex = 128;
            this.txtPercentageOfCoupon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 129;
            this.label4.Text = "Percentage of Coupon";
            // 
            // txtGiftVoucherValue
            // 
            this.txtGiftVoucherValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtGiftVoucherValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftVoucherValue.Location = new System.Drawing.Point(331, 55);
            this.txtGiftVoucherValue.Name = "txtGiftVoucherValue";
            this.txtGiftVoucherValue.ReadOnly = true;
            this.txtGiftVoucherValue.Size = new System.Drawing.Size(94, 21);
            this.txtGiftVoucherValue.TabIndex = 12;
            this.txtGiftVoucherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiftVoucherValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGiftVoucherValue_KeyDown);
            // 
            // lblGiftVoucherValue
            // 
            this.lblGiftVoucherValue.AutoSize = true;
            this.lblGiftVoucherValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherValue.Location = new System.Drawing.Point(218, 55);
            this.lblGiftVoucherValue.Name = "lblGiftVoucherValue";
            this.lblGiftVoucherValue.Size = new System.Drawing.Size(112, 13);
            this.lblGiftVoucherValue.TabIndex = 121;
            this.lblGiftVoucherValue.Text = "Gift Voucher Value";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbMore);
            this.groupBox3.Location = new System.Drawing.Point(453, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 122);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            // 
            // tbMore
            // 
            this.tbMore.Controls.Add(this.tbpVoucherSerial);
            this.tbMore.Controls.Add(this.tbpVoucherNo);
            this.tbMore.Location = new System.Drawing.Point(11, 12);
            this.tbMore.Name = "tbMore";
            this.tbMore.SelectedIndex = 0;
            this.tbMore.Size = new System.Drawing.Size(219, 107);
            this.tbMore.TabIndex = 60;
            // 
            // tbpVoucherSerial
            // 
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialQty);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialQty);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialTo);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialFrom);
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialTo);
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialFrom);
            this.tbpVoucherSerial.Location = new System.Drawing.Point(4, 22);
            this.tbpVoucherSerial.Name = "tbpVoucherSerial";
            this.tbpVoucherSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpVoucherSerial.Size = new System.Drawing.Size(211, 81);
            this.tbpVoucherSerial.TabIndex = 1;
            this.tbpVoucherSerial.Text = "Voucher Serial";
            this.tbpVoucherSerial.UseVisualStyleBackColor = true;
            // 
            // lblVoucherSerialQty
            // 
            this.lblVoucherSerialQty.AutoSize = true;
            this.lblVoucherSerialQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherSerialQty.Location = new System.Drawing.Point(19, 63);
            this.lblVoucherSerialQty.Name = "lblVoucherSerialQty";
            this.lblVoucherSerialQty.Size = new System.Drawing.Size(27, 13);
            this.lblVoucherSerialQty.TabIndex = 107;
            this.lblVoucherSerialQty.Text = "Qty";
            // 
            // txtVoucherSerialQty
            // 
            this.txtVoucherSerialQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtVoucherSerialQty.IntValue = 0;
            this.txtVoucherSerialQty.Location = new System.Drawing.Point(67, 56);
            this.txtVoucherSerialQty.Name = "txtVoucherSerialQty";
            this.txtVoucherSerialQty.Size = new System.Drawing.Size(99, 21);
            this.txtVoucherSerialQty.TabIndex = 106;
            this.txtVoucherSerialQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVoucherSerialTo
            // 
            this.txtVoucherSerialTo.Location = new System.Drawing.Point(67, 30);
            this.txtVoucherSerialTo.Name = "txtVoucherSerialTo";
            this.txtVoucherSerialTo.Size = new System.Drawing.Size(123, 21);
            this.txtVoucherSerialTo.TabIndex = 15;
            this.txtVoucherSerialTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherSerialTo_KeyDown);
            this.txtVoucherSerialTo.Leave += new System.EventHandler(this.txtVoucherSerialTo_Leave);
            // 
            // txtVoucherSerialFrom
            // 
            this.txtVoucherSerialFrom.Location = new System.Drawing.Point(67, 6);
            this.txtVoucherSerialFrom.Name = "txtVoucherSerialFrom";
            this.txtVoucherSerialFrom.Size = new System.Drawing.Size(123, 21);
            this.txtVoucherSerialFrom.TabIndex = 14;
            this.txtVoucherSerialFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherSerialFrom_KeyDown);
            this.txtVoucherSerialFrom.Leave += new System.EventHandler(this.txtVoucherSerialFrom_Leave);
            // 
            // lblVoucherSerialTo
            // 
            this.lblVoucherSerialTo.AutoSize = true;
            this.lblVoucherSerialTo.Location = new System.Drawing.Point(19, 33);
            this.lblVoucherSerialTo.Name = "lblVoucherSerialTo";
            this.lblVoucherSerialTo.Size = new System.Drawing.Size(20, 13);
            this.lblVoucherSerialTo.TabIndex = 30;
            this.lblVoucherSerialTo.Text = "To";
            // 
            // lblVoucherSerialFrom
            // 
            this.lblVoucherSerialFrom.AutoSize = true;
            this.lblVoucherSerialFrom.Location = new System.Drawing.Point(19, 9);
            this.lblVoucherSerialFrom.Name = "lblVoucherSerialFrom";
            this.lblVoucherSerialFrom.Size = new System.Drawing.Size(36, 13);
            this.lblVoucherSerialFrom.TabIndex = 29;
            this.lblVoucherSerialFrom.Text = "From";
            // 
            // tbpVoucherNo
            // 
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoQty);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoQty);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoTo);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoFrom);
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoTo);
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoFrom);
            this.tbpVoucherNo.Location = new System.Drawing.Point(4, 22);
            this.tbpVoucherNo.Name = "tbpVoucherNo";
            this.tbpVoucherNo.Padding = new System.Windows.Forms.Padding(3);
            this.tbpVoucherNo.Size = new System.Drawing.Size(211, 81);
            this.tbpVoucherNo.TabIndex = 0;
            this.tbpVoucherNo.Text = "Voucher No";
            this.tbpVoucherNo.UseVisualStyleBackColor = true;
            // 
            // lblVoucherNoQty
            // 
            this.lblVoucherNoQty.AutoSize = true;
            this.lblVoucherNoQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherNoQty.Location = new System.Drawing.Point(19, 63);
            this.lblVoucherNoQty.Name = "lblVoucherNoQty";
            this.lblVoucherNoQty.Size = new System.Drawing.Size(27, 13);
            this.lblVoucherNoQty.TabIndex = 109;
            this.lblVoucherNoQty.Text = "Qty";
            // 
            // txtVoucherNoQty
            // 
            this.txtVoucherNoQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtVoucherNoQty.IntValue = 0;
            this.txtVoucherNoQty.Location = new System.Drawing.Point(67, 56);
            this.txtVoucherNoQty.Name = "txtVoucherNoQty";
            this.txtVoucherNoQty.Size = new System.Drawing.Size(99, 21);
            this.txtVoucherNoQty.TabIndex = 108;
            this.txtVoucherNoQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVoucherNoTo
            // 
            this.txtVoucherNoTo.Location = new System.Drawing.Point(67, 29);
            this.txtVoucherNoTo.Name = "txtVoucherNoTo";
            this.txtVoucherNoTo.Size = new System.Drawing.Size(123, 21);
            this.txtVoucherNoTo.TabIndex = 15;
            this.txtVoucherNoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNoTo_KeyDown);
            this.txtVoucherNoTo.Leave += new System.EventHandler(this.txtVoucherNoTo_Leave);
            // 
            // txtVoucherNoFrom
            // 
            this.txtVoucherNoFrom.Location = new System.Drawing.Point(67, 4);
            this.txtVoucherNoFrom.Name = "txtVoucherNoFrom";
            this.txtVoucherNoFrom.Size = new System.Drawing.Size(123, 21);
            this.txtVoucherNoFrom.TabIndex = 14;
            this.txtVoucherNoFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNoFrom_KeyDown);
            this.txtVoucherNoFrom.Leave += new System.EventHandler(this.txtVoucherNoFrom_Leave);
            // 
            // lblVoucherNoTo
            // 
            this.lblVoucherNoTo.AutoSize = true;
            this.lblVoucherNoTo.Location = new System.Drawing.Point(19, 32);
            this.lblVoucherNoTo.Name = "lblVoucherNoTo";
            this.lblVoucherNoTo.Size = new System.Drawing.Size(20, 13);
            this.lblVoucherNoTo.TabIndex = 26;
            this.lblVoucherNoTo.Text = "To";
            // 
            // lblVoucherNoFrom
            // 
            this.lblVoucherNoFrom.AutoSize = true;
            this.lblVoucherNoFrom.Location = new System.Drawing.Point(19, 7);
            this.lblVoucherNoFrom.Name = "lblVoucherNoFrom";
            this.lblVoucherNoFrom.Size = new System.Drawing.Size(36, 13);
            this.lblVoucherNoFrom.TabIndex = 25;
            this.lblVoucherNoFrom.Text = "From";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblGiftVoucherQty);
            this.groupBox2.Controls.Add(this.txtGiftVoucherQty);
            this.groupBox2.Location = new System.Drawing.Point(5, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 41);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            // 
            // lblGiftVoucherQty
            // 
            this.lblGiftVoucherQty.AutoSize = true;
            this.lblGiftVoucherQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherQty.Location = new System.Drawing.Point(1, 17);
            this.lblGiftVoucherQty.Name = "lblGiftVoucherQty";
            this.lblGiftVoucherQty.Size = new System.Drawing.Size(101, 13);
            this.lblGiftVoucherQty.TabIndex = 105;
            this.lblGiftVoucherQty.Text = "Gift Voucher Qty";
            // 
            // txtGiftVoucherQty
            // 
            this.txtGiftVoucherQty.IntValue = 0;
            this.txtGiftVoucherQty.Location = new System.Drawing.Point(127, 14);
            this.txtGiftVoucherQty.Name = "txtGiftVoucherQty";
            this.txtGiftVoucherQty.Size = new System.Drawing.Size(122, 21);
            this.txtGiftVoucherQty.TabIndex = 14;
            this.txtGiftVoucherQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiftVoucherQty.Leave += new System.EventHandler(this.txtGiftVoucherQty_Leave);
            // 
            // FrmGiftVoucherPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1039, 572);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmGiftVoucherPurchaseOrder";
            this.Text = "Gift Voucher Purchase Order";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherBookDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherPurchaseOrderDetailTempBindingSource)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tbMore.ResumeLayout(false);
            this.tbpVoucherSerial.ResumeLayout(false);
            this.tbpVoucherSerial.PerformLayout();
            this.tbpVoucherNo.ResumeLayout(false);
            this.tbpVoucherNo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblValidityDays;
        private System.Windows.Forms.Label lblValidityPeriod;
        private System.Windows.Forms.DateTimePicker dtpExpectedDate;
        private System.Windows.Forms.Label lblExpectedDate;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblDocumentDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationPoNo;
        private System.Windows.Forms.TextBox txtPurchaseOrderNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvVoucherBookDetails;
        private System.Windows.Forms.TextBox txtVoucherValue;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtVoucherSerial;
        private System.Windows.Forms.CheckBox chkAutoCompleationVoucher;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TextBox txtTotalQty;
        private CustomControls.TextBoxInteger txtValidityPeriod;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblSelectionCriteria;
        private System.Windows.Forms.ComboBox cmbSelectionCriteria;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tbMore;
        private System.Windows.Forms.TabPage tbpVoucherNo;
        private System.Windows.Forms.TextBox txtVoucherNoTo;
        private System.Windows.Forms.TextBox txtVoucherNoFrom;
        private System.Windows.Forms.Label lblVoucherNoTo;
        private System.Windows.Forms.Label lblVoucherNoFrom;
        private System.Windows.Forms.TabPage tbpVoucherSerial;
        private System.Windows.Forms.TextBox txtVoucherSerialTo;
        private System.Windows.Forms.TextBox txtVoucherSerialFrom;
        private System.Windows.Forms.Label lblVoucherSerialTo;
        private System.Windows.Forms.Label lblVoucherSerialFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblGiftVoucherQty;
        private CustomControls.TextBoxInteger txtGiftVoucherQty;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxInteger txtNoOfVouchersOnBook;
        private System.Windows.Forms.Label lblNoOfVouchersOnBook;
        private CustomControls.TextBoxMasterCode txtGroupCode;
        private CustomControls.TextBoxMasterDescription txtGroupName;
        private System.Windows.Forms.Label lblGiftVoucherGroup;
        private System.Windows.Forms.Label lblBook;
        private CustomControls.TextBoxMasterCode txtBookCode;
        private CustomControls.TextBoxMasterDescription txtBookName;
        private System.Windows.Forms.Label lblGiftVoucherValue;
        private CustomControls.TextBoxCurrency txtGiftVoucherValue;
        private System.Windows.Forms.BindingSource invGiftVoucherPurchaseOrderDetailTempBindingSource;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private CustomControls.TextBoxCurrency txtNetAmount;
        private CustomControls.TextBoxCurrency txtGrossAmount;
        private CustomControls.TextBoxCurrency txtSubTotalDiscount;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private CustomControls.TextBoxPercentGen txtPercentageOfCoupon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.ComboBox cmbBasedOn;
        private System.Windows.Forms.RadioButton rdoCoupon;
        private System.Windows.Forms.RadioButton rdoVoucher;
        private System.Windows.Forms.Button btnPaymentTermLimits;
        private System.Windows.Forms.CheckBox chkRound;
        private System.Windows.Forms.Label lblOtherCharges;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private CustomControls.TextBoxCurrency txtTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationGroup;
        private System.Windows.Forms.CheckBox chkAutoCompleationBook;
        public Glass.GlassButton btnLoad;
        private System.Windows.Forms.Label lblVoucherSerialQty;
        private CustomControls.TextBoxInteger txtVoucherSerialQty;
        private System.Windows.Forms.Label lblVoucherNoQty;
        private CustomControls.TextBoxInteger txtVoucherNoQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherSerialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherAmountDataGridViewTextBoxColumn;
    }
}
