namespace UI.Windows
{
    partial class FrmPurchaseReturnNote
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbReturnType = new System.Windows.Forms.ComboBox();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnGrnDetails = new System.Windows.Forms.Button();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblSrnDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationGrnNo = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtGrnNo = new System.Windows.Forms.TextBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblGrnNo = new System.Windows.Forms.Label();
            this.lblSrnNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tbDetails = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscountPercentage = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtFreeQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
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
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtSubTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.TextBox();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.chkViewStokDetails = new System.Windows.Forms.CheckBox();
            this.grpStock = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvBatchStock = new System.Windows.Forms.DataGridView();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpPaymentDetails = new System.Windows.Forms.TabPage();
            this.cmbCurrencyType = new System.Windows.Forms.ComboBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.lblCurrencyType = new System.Windows.Forms.Label();
            this.cmbLedgerName = new System.Windows.Forms.ComboBox();
            this.lblCurrencyRate = new System.Windows.Forms.Label();
            this.txtCurrencyRate = new System.Windows.Forms.TextBox();
            this.cmbLedgerCode = new System.Windows.Forms.ComboBox();
            this.tbpSupplier = new System.Windows.Forms.TabPage();
            this.lblTotalDueAmount = new System.Windows.Forms.Label();
            this.txtTotalDueAmount = new System.Windows.Forms.TextBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.txtPaymentTerms = new System.Windows.Forms.TextBox();
            this.tbpPageSetup = new System.Windows.Forms.TabPage();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.tbDetails.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.tbpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.grpStock.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).BeginInit();
            this.tbpPaymentDetails.SuspendLayout();
            this.tbpSupplier.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(814, 485);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 485);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.chkViewStokDetails);
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.cmbReturnType);
            this.grpHeader.Controls.Add(this.lblReturnType);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnGrnDetails);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.lblSrnDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationGrnNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtGrnNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblGrnNo);
            this.grpHeader.Controls.Add(this.lblSrnNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1129, 86);
            this.grpHeader.TabIndex = 18;
            this.grpHeader.TabStop = false;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(584, 61);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 46;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.Location = new System.Drawing.Point(796, 61);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(88, 17);
            this.chkTStatus.TabIndex = 89;
            this.chkTStatus.Text = "checkBox1";
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(667, 36);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(179, 21);
            this.cmbCostCentre.TabIndex = 67;
            this.cmbCostCentre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostCentre_KeyDown);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(582, 39);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 66;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(965, 59);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(160, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(883, 63);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // cmbReturnType
            // 
            this.cmbReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnType.FormattingEnabled = true;
            this.cmbReturnType.Location = new System.Drawing.Point(965, 35);
            this.cmbReturnType.Name = "cmbReturnType";
            this.cmbReturnType.Size = new System.Drawing.Size(160, 21);
            this.cmbReturnType.TabIndex = 29;
            this.cmbReturnType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReturnType_KeyDown);
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.Location = new System.Drawing.Point(883, 39);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(76, 13);
            this.lblReturnType.TabIndex = 28;
            this.lblReturnType.Text = "Return Type";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(667, 12);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(179, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(582, 15);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(965, 11);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(159, 21);
            this.dtpDocumentDate.TabIndex = 22;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // btnGrnDetails
            // 
            this.btnGrnDetails.Location = new System.Drawing.Point(548, 12);
            this.btnGrnDetails.Name = "btnGrnDetails";
            this.btnGrnDetails.Size = new System.Drawing.Size(28, 23);
            this.btnGrnDetails.TabIndex = 21;
            this.btnGrnDetails.Text = "...";
            this.btnGrnDetails.UseVisualStyleBackColor = true;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(279, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(142, 60);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(433, 21);
            this.txtRemark.TabIndex = 18;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(6, 39);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 13;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(279, 36);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(296, 21);
            this.txtSupplierName.TabIndex = 12;
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierName_KeyDown);
            this.txtSupplierName.Leave += new System.EventHandler(this.txtSupplierName_Leave);
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(125, 39);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 11;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSupplier_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(142, 36);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 10;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            this.txtSupplierCode.Leave += new System.EventHandler(this.txtSupplierCode_Leave);
            // 
            // lblSrnDate
            // 
            this.lblSrnDate.AutoSize = true;
            this.lblSrnDate.Location = new System.Drawing.Point(883, 17);
            this.lblSrnDate.Name = "lblSrnDate";
            this.lblSrnDate.Size = new System.Drawing.Size(61, 13);
            this.lblSrnDate.TabIndex = 9;
            this.lblSrnDate.Text = "PRN Date";
            // 
            // chkAutoCompleationGrnNo
            // 
            this.chkAutoCompleationGrnNo.AutoSize = true;
            this.chkAutoCompleationGrnNo.Checked = true;
            this.chkAutoCompleationGrnNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGrnNo.Location = new System.Drawing.Point(392, 16);
            this.chkAutoCompleationGrnNo.Name = "chkAutoCompleationGrnNo";
            this.chkAutoCompleationGrnNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGrnNo.TabIndex = 5;
            this.chkAutoCompleationGrnNo.Tag = "1";
            this.chkAutoCompleationGrnNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGrnNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGrnNo_CheckedChanged);
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(125, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtGrnNo
            // 
            this.txtGrnNo.Location = new System.Drawing.Point(410, 13);
            this.txtGrnNo.Name = "txtGrnNo";
            this.txtGrnNo.Size = new System.Drawing.Size(136, 21);
            this.txtGrnNo.TabIndex = 3;
            this.txtGrnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrnNo_KeyDown);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(142, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            // 
            // lblGrnNo
            // 
            this.lblGrnNo.AutoSize = true;
            this.lblGrnNo.Location = new System.Drawing.Point(334, 16);
            this.lblGrnNo.Name = "lblGrnNo";
            this.lblGrnNo.Size = new System.Drawing.Size(51, 13);
            this.lblGrnNo.TabIndex = 1;
            this.lblGrnNo.Text = "GRN No";
            // 
            // lblSrnNo
            // 
            this.lblSrnNo.AutoSize = true;
            this.lblSrnNo.Location = new System.Drawing.Point(6, 16);
            this.lblSrnNo.Name = "lblSrnNo";
            this.lblSrnNo.Size = new System.Drawing.Size(84, 13);
            this.lblSrnNo.TabIndex = 0;
            this.lblSrnNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tbDetails);
            this.grpBody.Location = new System.Drawing.Point(2, 75);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1129, 241);
            this.grpBody.TabIndex = 19;
            this.grpBody.TabStop = false;
            // 
            // tbDetails
            // 
            this.tbDetails.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tbDetails.Controls.Add(this.tbpGeneral);
            this.tbDetails.Controls.Add(this.tbpAdvanced);
            this.tbDetails.Location = new System.Drawing.Point(2, 9);
            this.tbDetails.Name = "tbDetails";
            this.tbDetails.SelectedIndex = 0;
            this.tbDetails.Size = new System.Drawing.Size(1126, 231);
            this.tbDetails.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.txtProductAmount);
            this.tbpGeneral.Controls.Add(this.txtProductDiscount);
            this.tbpGeneral.Controls.Add(this.txtProductDiscountPercentage);
            this.tbpGeneral.Controls.Add(this.txtCostPrice);
            this.tbpGeneral.Controls.Add(this.txtFreeQty);
            this.tbpGeneral.Controls.Add(this.txtQty);
            this.tbpGeneral.Controls.Add(this.txtBatchNo);
            this.tbpGeneral.Controls.Add(this.dtpExpiry);
            this.tbpGeneral.Controls.Add(this.cmbUnit);
            this.tbpGeneral.Controls.Add(this.txtProductName);
            this.tbpGeneral.Controls.Add(this.chkAutoCompleationProduct);
            this.tbpGeneral.Controls.Add(this.dgvItemDetails);
            this.tbpGeneral.Controls.Add(this.txtProductCode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(1118, 202);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Location = new System.Drawing.Point(976, 178);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(138, 21);
            this.txtProductAmount.TabIndex = 69;
            this.txtProductAmount.Tag = "3";
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtProductDiscount
            // 
            this.txtProductDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscount.Location = new System.Drawing.Point(910, 178);
            this.txtProductDiscount.Name = "txtProductDiscount";
            this.txtProductDiscount.Size = new System.Drawing.Size(65, 21);
            this.txtProductDiscount.TabIndex = 68;
            this.txtProductDiscount.Tag = "3";
            this.txtProductDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscount_KeyDown);
            this.txtProductDiscount.Leave += new System.EventHandler(this.txtProductDiscount_Leave);
            // 
            // txtProductDiscountPercentage
            // 
            this.txtProductDiscountPercentage.ForeColor = System.Drawing.Color.Black;
            this.txtProductDiscountPercentage.Location = new System.Drawing.Point(866, 178);
            this.txtProductDiscountPercentage.Name = "txtProductDiscountPercentage";
            this.txtProductDiscountPercentage.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountPercentage.Size = new System.Drawing.Size(43, 21);
            this.txtProductDiscountPercentage.TabIndex = 67;
            this.txtProductDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountPercentage_KeyDown);
            this.txtProductDiscountPercentage.Leave += new System.EventHandler(this.txtProductDiscountPercentage_Leave);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(767, 178);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(98, 21);
            this.txtCostPrice.TabIndex = 55;
            this.txtCostPrice.Tag = "3";
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostPrice.Leave += new System.EventHandler(this.txtCostPrice_Leave);
            // 
            // txtFreeQty
            // 
            this.txtFreeQty.Location = new System.Drawing.Point(717, 178);
            this.txtFreeQty.Name = "txtFreeQty";
            this.txtFreeQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFreeQty.Size = new System.Drawing.Size(49, 21);
            this.txtFreeQty.TabIndex = 54;
            this.txtFreeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFreeQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFreeQty_KeyDown);
            this.txtFreeQty.Leave += new System.EventHandler(this.txtFreeQty_Leave);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(658, 178);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(58, 21);
            this.txtQty.TabIndex = 53;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(477, 178);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(90, 21);
            this.txtBatchNo.TabIndex = 46;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(568, 178);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(89, 21);
            this.dtpExpiry.TabIndex = 45;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(409, 178);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbUnit.TabIndex = 44;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(185, 178);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(223, 21);
            this.txtProductName.TabIndex = 37;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(2, 181);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 36;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
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
            this.CostPrice,
            this.DiscountPer,
            this.DiscountAmt,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(-1, 3);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 15;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1117, 170);
            this.dgvItemDetails.TabIndex = 34;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 135;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 220;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 65;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            this.Expiry.HeaderText = "Expiry";
            this.Expiry.Name = "Expiry";
            this.Expiry.ReadOnly = true;
            this.Expiry.Width = 88;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle13;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 55;
            // 
            // FreeQty
            // 
            this.FreeQty.DataPropertyName = "FreeQty";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FreeQty.DefaultCellStyle = dataGridViewCellStyle14;
            this.FreeQty.HeaderText = "Free";
            this.FreeQty.Name = "FreeQty";
            this.FreeQty.ReadOnly = true;
            this.FreeQty.Width = 45;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle15;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            // 
            // DiscountPer
            // 
            this.DiscountPer.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPer.DefaultCellStyle = dataGridViewCellStyle16;
            this.DiscountPer.HeaderText = "Dis%";
            this.DiscountPer.Name = "DiscountPer";
            this.DiscountPer.ReadOnly = true;
            this.DiscountPer.Width = 48;
            // 
            // DiscountAmt
            // 
            this.DiscountAmt.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmt.DefaultCellStyle = dataGridViewCellStyle17;
            this.DiscountAmt.HeaderText = "Discount";
            this.DiscountAmt.Name = "DiscountAmt";
            this.DiscountAmt.ReadOnly = true;
            this.DiscountAmt.Width = 70;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle18;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 120;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(18, 178);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(166, 21);
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
            this.tbpAdvanced.Size = new System.Drawing.Size(1118, 202);
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
            this.dgvAdvanced.Location = new System.Drawing.Point(-1, 3);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.RowHeadersWidth = 15;
            this.dgvAdvanced.Size = new System.Drawing.Size(1115, 196);
            this.dgvAdvanced.TabIndex = 35;
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
            this.grpFooter.Controls.Add(this.label3);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Location = new System.Drawing.Point(543, 310);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(588, 179);
            this.grpFooter.TabIndex = 20;
            this.grpFooter.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Red;
            this.txtTotalQty.Location = new System.Drawing.Point(436, 153);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(145, 21);
            this.txtTotalQty.TabIndex = 96;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtOtherCharges.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharges.Location = new System.Drawing.Point(436, 106);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(145, 21);
            this.txtOtherCharges.TabIndex = 92;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(436, 82);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(145, 21);
            this.txtTotalTaxAmount.TabIndex = 70;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalTaxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalTaxAmount_KeyUp);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(436, 58);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(145, 21);
            this.txtSubTotalDiscount.TabIndex = 91;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.Location = new System.Drawing.Point(436, 10);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(145, 21);
            this.txtGrossAmount.TabIndex = 70;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(436, 34);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(145, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 90;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(289, 81);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 83;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(418, 85);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 82;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(288, 109);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 55;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(397, 38);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(287, 133);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(309, 85);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 39;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(419, 38);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            this.chkSubTotalDiscountPercentage.CheckedChanged += new System.EventHandler(this.chkSubTotalDiscountPercentage_CheckedChanged);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(288, 37);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(288, 13);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount.Location = new System.Drawing.Point(436, 130);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(145, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkViewStokDetails
            // 
            this.chkViewStokDetails.AutoSize = true;
            this.chkViewStokDetails.Location = new System.Drawing.Point(701, 62);
            this.chkViewStokDetails.Name = "chkViewStokDetails";
            this.chkViewStokDetails.Size = new System.Drawing.Size(93, 17);
            this.chkViewStokDetails.TabIndex = 90;
            this.chkViewStokDetails.Text = "View Stock ";
            this.chkViewStokDetails.UseVisualStyleBackColor = true;
            this.chkViewStokDetails.CheckedChanged += new System.EventHandler(this.chkViewStokDetails_CheckedChanged);
            // 
            // grpStock
            // 
            this.grpStock.Controls.Add(this.tbFooter);
            this.grpStock.Location = new System.Drawing.Point(2, 310);
            this.grpStock.Name = "grpStock";
            this.grpStock.Size = new System.Drawing.Size(537, 180);
            this.grpStock.TabIndex = 21;
            this.grpStock.TabStop = false;
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tabPage1);
            this.tbFooter.Controls.Add(this.tbpPaymentDetails);
            this.tbFooter.Controls.Add(this.tbpSupplier);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Location = new System.Drawing.Point(4, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(531, 164);
            this.tbFooter.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvBatchStock);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(523, 138);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Batch Wise Stock Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvBatchStock
            // 
            this.dgvBatchStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch,
            this.dataGridViewTextBoxColumn4});
            this.dgvBatchStock.Location = new System.Drawing.Point(6, 3);
            this.dgvBatchStock.Name = "dgvBatchStock";
            this.dgvBatchStock.Size = new System.Drawing.Size(513, 132);
            this.dgvBatchStock.TabIndex = 3;
            this.dgvBatchStock.DoubleClick += new System.EventHandler(this.dgvBatchStock_DoubleClick);
            // 
            // Batch
            // 
            this.Batch.DataPropertyName = "BatchNo";
            this.Batch.HeaderText = "Batch No";
            this.Batch.Name = "Batch";
            this.Batch.ReadOnly = true;
            this.Batch.Width = 192;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "BalanceQty";
            this.dataGridViewTextBoxColumn4.HeaderText = "Current Stock";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 75;
            // 
            // tbpPaymentDetails
            // 
            this.tbpPaymentDetails.Controls.Add(this.cmbCurrencyType);
            this.tbpPaymentDetails.Controls.Add(this.lblLedger);
            this.tbpPaymentDetails.Controls.Add(this.lblCurrencyType);
            this.tbpPaymentDetails.Controls.Add(this.cmbLedgerName);
            this.tbpPaymentDetails.Controls.Add(this.lblCurrencyRate);
            this.tbpPaymentDetails.Controls.Add(this.txtCurrencyRate);
            this.tbpPaymentDetails.Controls.Add(this.cmbLedgerCode);
            this.tbpPaymentDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpPaymentDetails.Name = "tbpPaymentDetails";
            this.tbpPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPaymentDetails.Size = new System.Drawing.Size(522, 138);
            this.tbpPaymentDetails.TabIndex = 4;
            this.tbpPaymentDetails.Text = "Payment Details";
            this.tbpPaymentDetails.UseVisualStyleBackColor = true;
            // 
            // cmbCurrencyType
            // 
            this.cmbCurrencyType.FormattingEnabled = true;
            this.cmbCurrencyType.Location = new System.Drawing.Point(102, 34);
            this.cmbCurrencyType.Name = "cmbCurrencyType";
            this.cmbCurrencyType.Size = new System.Drawing.Size(92, 21);
            this.cmbCurrencyType.TabIndex = 84;
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(4, 12);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(46, 13);
            this.lblLedger.TabIndex = 44;
            this.lblLedger.Text = "Ledger";
            // 
            // lblCurrencyType
            // 
            this.lblCurrencyType.AutoSize = true;
            this.lblCurrencyType.Location = new System.Drawing.Point(4, 37);
            this.lblCurrencyType.Name = "lblCurrencyType";
            this.lblCurrencyType.Size = new System.Drawing.Size(91, 13);
            this.lblCurrencyType.TabIndex = 83;
            this.lblCurrencyType.Text = "Currency Type";
            // 
            // cmbLedgerName
            // 
            this.cmbLedgerName.FormattingEnabled = true;
            this.cmbLedgerName.Location = new System.Drawing.Point(197, 9);
            this.cmbLedgerName.Name = "cmbLedgerName";
            this.cmbLedgerName.Size = new System.Drawing.Size(203, 21);
            this.cmbLedgerName.TabIndex = 43;
            // 
            // lblCurrencyRate
            // 
            this.lblCurrencyRate.AutoSize = true;
            this.lblCurrencyRate.Location = new System.Drawing.Point(195, 37);
            this.lblCurrencyRate.Name = "lblCurrencyRate";
            this.lblCurrencyRate.Size = new System.Drawing.Size(90, 13);
            this.lblCurrencyRate.TabIndex = 82;
            this.lblCurrencyRate.Text = "Currency Rate";
            // 
            // txtCurrencyRate
            // 
            this.txtCurrencyRate.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCurrencyRate.Location = new System.Drawing.Point(290, 34);
            this.txtCurrencyRate.Name = "txtCurrencyRate";
            this.txtCurrencyRate.Size = new System.Drawing.Size(110, 21);
            this.txtCurrencyRate.TabIndex = 81;
            this.txtCurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLedgerCode
            // 
            this.cmbLedgerCode.FormattingEnabled = true;
            this.cmbLedgerCode.Location = new System.Drawing.Point(102, 9);
            this.cmbLedgerCode.Name = "cmbLedgerCode";
            this.cmbLedgerCode.Size = new System.Drawing.Size(92, 21);
            this.cmbLedgerCode.TabIndex = 42;
            // 
            // tbpSupplier
            // 
            this.tbpSupplier.Controls.Add(this.lblTotalDueAmount);
            this.tbpSupplier.Controls.Add(this.txtTotalDueAmount);
            this.tbpSupplier.Controls.Add(this.lblPaymentTerms);
            this.tbpSupplier.Controls.Add(this.txtPaymentTerms);
            this.tbpSupplier.Location = new System.Drawing.Point(4, 22);
            this.tbpSupplier.Name = "tbpSupplier";
            this.tbpSupplier.Size = new System.Drawing.Size(522, 138);
            this.tbpSupplier.TabIndex = 3;
            this.tbpSupplier.Text = "Supplier";
            this.tbpSupplier.UseVisualStyleBackColor = true;
            // 
            // lblTotalDueAmount
            // 
            this.lblTotalDueAmount.AutoSize = true;
            this.lblTotalDueAmount.Location = new System.Drawing.Point(9, 31);
            this.lblTotalDueAmount.Name = "lblTotalDueAmount";
            this.lblTotalDueAmount.Size = new System.Drawing.Size(109, 13);
            this.lblTotalDueAmount.TabIndex = 54;
            this.lblTotalDueAmount.Text = "Total Due Amount";
            // 
            // txtTotalDueAmount
            // 
            this.txtTotalDueAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalDueAmount.Location = new System.Drawing.Point(135, 28);
            this.txtTotalDueAmount.Name = "txtTotalDueAmount";
            this.txtTotalDueAmount.Size = new System.Drawing.Size(143, 21);
            this.txtTotalDueAmount.TabIndex = 53;
            this.txtTotalDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(9, 8);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(96, 13);
            this.lblPaymentTerms.TabIndex = 52;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPaymentTerms.Location = new System.Drawing.Point(135, 4);
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.Size = new System.Drawing.Size(143, 21);
            this.txtPaymentTerms.TabIndex = 50;
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
            this.tbpPageSetup.Size = new System.Drawing.Size(522, 138);
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
            // FrmPurchaseReturnNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1133, 533);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpStock);
            this.Name = "FrmPurchaseReturnNote";
            this.Text = "Purchase Return Note";
            this.Load += new System.EventHandler(this.FrmPurchaseReturnNote_Load);
            this.Controls.SetChildIndex(this.grpStock, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.tbDetails.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.tbpAdvanced.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.grpStock.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).EndInit();
            this.tbpPaymentDetails.ResumeLayout(false);
            this.tbpPaymentDetails.PerformLayout();
            this.tbpSupplier.ResumeLayout(false);
            this.tbpSupplier.PerformLayout();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnGrnDetails;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblSrnDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationGrnNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtGrnNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblGrnNo;
        private System.Windows.Forms.Label lblSrnNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.ComboBox cmbReturnType;
        private System.Windows.Forms.Label lblReturnType;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TabControl tbDetails;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TabPage tbpAdvanced;
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private System.Windows.Forms.DataGridViewComboBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Label lblOtherCharges;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private CustomControls.TextBoxQty txtQty;
        private CustomControls.TextBoxQty txtFreeQty;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private CustomControls.TextBoxPercentGen txtProductDiscountPercentage;
        private CustomControls.TextBoxCurrency txtProductDiscount;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private System.Windows.Forms.TextBox txtGrossAmount;
        private System.Windows.Forms.TextBox txtSubTotalDiscount;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreeQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.CheckBox chkViewStokDetails;
        private System.Windows.Forms.GroupBox grpStock;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvBatchStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TabPage tbpPaymentDetails;
        private System.Windows.Forms.ComboBox cmbCurrencyType;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.Label lblCurrencyType;
        private System.Windows.Forms.ComboBox cmbLedgerName;
        private System.Windows.Forms.Label lblCurrencyRate;
        private System.Windows.Forms.TextBox txtCurrencyRate;
        private System.Windows.Forms.ComboBox cmbLedgerCode;
        private System.Windows.Forms.TabPage tbpSupplier;
        private System.Windows.Forms.Label lblTotalDueAmount;
        private System.Windows.Forms.TextBox txtTotalDueAmount;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.TextBox txtPaymentTerms;
        private System.Windows.Forms.TabPage tbpPageSetup;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblPrinter;
    }
}
