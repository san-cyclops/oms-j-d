namespace UI.Windows
{
    partial class FrmGoodsReceivedNote
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
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tabGRN = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.txtWavgDis = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtProductDiscountPercentage = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductDiscount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FreeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.tbpAdvanced = new System.Windows.Forms.TabPage();
            this.dgvAdvanced = new System.Windows.Forms.DataGridView();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherExpenseTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtOtherExpenceValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtOtherExpenceName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationOtherExpence = new System.Windows.Forms.CheckBox();
            this.txtOtherExpenceCode = new System.Windows.Forms.TextBox();
            this.txtOtherDis = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtFreeQty = new UI.Windows.CustomControls.TextBoxQty();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtPayingAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbBankCode = new System.Windows.Forms.ComboBox();
            this.lblPaidAmount = new System.Windows.Forms.Label();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.chkRound = new System.Windows.Forms.CheckBox();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.cmbBankName = new System.Windows.Forms.ComboBox();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblCardCheque = new System.Windows.Forms.Label();
            this.txtCardChequeNo = new System.Windows.Forms.TextBox();
            this.lblPayingAmount = new System.Windows.Forms.Label();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.txtBalanceAmount = new System.Windows.Forms.TextBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpPaymentDetails = new System.Windows.Forms.TabPage();
            this.lblLedger = new System.Windows.Forms.Label();
            this.cmbLedgerName = new System.Windows.Forms.ComboBox();
            this.cmbLedgerCode = new System.Windows.Forms.ComboBox();
            this.dgvPaymentDetails = new System.Windows.Forms.DataGridView();
            this.PaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCheqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentMethodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tbpOtherDetails = new System.Windows.Forms.TabPage();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtSubTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.TextBox();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.cmbPaymentTerms = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.txtSupplierInvoiceNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.chkConsignmentBasis = new System.Windows.Forms.CheckBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpGrnDate = new System.Windows.Forms.DateTimePicker();
            this.btnPoDetails = new System.Windows.Forms.Button();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblGrnDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationPoNo = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtPurchaseOrderNo = new System.Windows.Forms.TextBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpBody.SuspendLayout();
            this.tabGRN.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.tbpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpPaymentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetails)).BeginInit();
            this.tbpSupplier.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(848, 530);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 530);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tabGRN);
            this.grpBody.Location = new System.Drawing.Point(2, 78);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1163, 265);
            this.grpBody.TabIndex = 16;
            this.grpBody.TabStop = false;
            // 
            // tabGRN
            // 
            this.tabGRN.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabGRN.Controls.Add(this.tbpGeneral);
            this.tabGRN.Controls.Add(this.tbpAdvanced);
            this.tabGRN.Location = new System.Drawing.Point(0, 11);
            this.tabGRN.Name = "tabGRN";
            this.tabGRN.SelectedIndex = 0;
            this.tabGRN.Size = new System.Drawing.Size(1162, 253);
            this.tabGRN.TabIndex = 1;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.txtWavgDis);
            this.tbpGeneral.Controls.Add(this.txtProductDiscountPercentage);
            this.tbpGeneral.Controls.Add(this.txtQty);
            this.tbpGeneral.Controls.Add(this.txtProductAmount);
            this.tbpGeneral.Controls.Add(this.txtProductDiscount);
            this.tbpGeneral.Controls.Add(this.txtCostPrice);
            this.tbpGeneral.Controls.Add(this.cmbUnit);
            this.tbpGeneral.Controls.Add(this.txtProductName);
            this.tbpGeneral.Controls.Add(this.chkAutoCompleationProduct);
            this.tbpGeneral.Controls.Add(this.dgvItemDetails);
            this.tbpGeneral.Controls.Add(this.txtProductCode);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(1154, 224);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // txtWavgDis
            // 
            this.txtWavgDis.ForeColor = System.Drawing.Color.Black;
            this.txtWavgDis.Location = new System.Drawing.Point(654, 202);
            this.txtWavgDis.Name = "txtWavgDis";
            this.txtWavgDis.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWavgDis.Size = new System.Drawing.Size(60, 21);
            this.txtWavgDis.TabIndex = 67;
            this.txtWavgDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWavgDis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWavgDis_KeyDown);
            this.txtWavgDis.Leave += new System.EventHandler(this.txtWavgDis_Leave);
            // 
            // txtProductDiscountPercentage
            // 
            this.txtProductDiscountPercentage.ForeColor = System.Drawing.Color.Black;
            this.txtProductDiscountPercentage.Location = new System.Drawing.Point(717, 202);
            this.txtProductDiscountPercentage.Name = "txtProductDiscountPercentage";
            this.txtProductDiscountPercentage.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountPercentage.Size = new System.Drawing.Size(60, 21);
            this.txtProductDiscountPercentage.TabIndex = 66;
            this.txtProductDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountPercentage_KeyDown);
            this.txtProductDiscountPercentage.Leave += new System.EventHandler(this.txtProductDiscountPercentage_Leave);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(489, 202);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(62, 21);
            this.txtQty.TabIndex = 52;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Location = new System.Drawing.Point(869, 202);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(151, 21);
            this.txtProductAmount.TabIndex = 51;
            this.txtProductAmount.Tag = "3";
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            this.txtProductAmount.Leave += new System.EventHandler(this.txtProductAmount_Leave);
            // 
            // txtProductDiscount
            // 
            this.txtProductDiscount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscount.Location = new System.Drawing.Point(779, 202);
            this.txtProductDiscount.Name = "txtProductDiscount";
            this.txtProductDiscount.Size = new System.Drawing.Size(86, 21);
            this.txtProductDiscount.TabIndex = 50;
            this.txtProductDiscount.Tag = "3";
            this.txtProductDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscount_KeyDown);
            this.txtProductDiscount.Leave += new System.EventHandler(this.txtProductDiscount_Leave);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(553, 202);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(99, 21);
            this.txtCostPrice.TabIndex = 48;
            this.txtCostPrice.Tag = "3";
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostPrice_KeyDown);
            this.txtCostPrice.Leave += new System.EventHandler(this.txtCostPrice_Leave);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(420, 202);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbUnit.TabIndex = 43;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(186, 202);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(233, 21);
            this.txtProductName.TabIndex = 36;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(3, 205);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 35;
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
            this.Expiry,
            this.Qty,
            this.FreeQty,
            this.CostPrice,
            this.SellingPrice,
            this.WAvg,
            this.DiscountPer,
            this.DiscountAmt,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(3, 0);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 45;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1150, 199);
            this.dgvItemDetails.TabIndex = 33;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
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
            this.ProductCode.Width = 120;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 225;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 58;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            this.Expiry.HeaderText = "Expiry";
            this.Expiry.Name = "Expiry";
            this.Expiry.ReadOnly = true;
            this.Expiry.Visible = false;
            this.Expiry.Width = 90;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 62;
            // 
            // FreeQty
            // 
            this.FreeQty.DataPropertyName = "FreeQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FreeQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.FreeQty.HeaderText = "Free";
            this.FreeQty.Name = "FreeQty";
            this.FreeQty.ReadOnly = true;
            this.FreeQty.Visible = false;
            this.FreeQty.Width = 50;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Visible = false;
            // 
            // WAvg
            // 
            this.WAvg.DataPropertyName = "FreeQty";
            this.WAvg.HeaderText = "Wtg";
            this.WAvg.Name = "WAvg";
            this.WAvg.Width = 60;
            // 
            // DiscountPer
            // 
            this.DiscountPer.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPer.DefaultCellStyle = dataGridViewCellStyle5;
            this.DiscountPer.HeaderText = "Other%";
            this.DiscountPer.Name = "DiscountPer";
            this.DiscountPer.ReadOnly = true;
            this.DiscountPer.Width = 60;
            // 
            // DiscountAmt
            // 
            this.DiscountAmt.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmt.DefaultCellStyle = dataGridViewCellStyle6;
            this.DiscountAmt.HeaderText = "Ded.Amt.";
            this.DiscountAmt.Name = "DiscountAmt";
            this.DiscountAmt.ReadOnly = true;
            this.DiscountAmt.Width = 72;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 120;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(20, 202);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(165, 21);
            this.txtProductCode.TabIndex = 34;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductCode_KeyPress);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // tbpAdvanced
            // 
            this.tbpAdvanced.Controls.Add(this.dgvAdvanced);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceValue);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceName);
            this.tbpAdvanced.Controls.Add(this.chkAutoCompleationOtherExpence);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceCode);
            this.tbpAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tbpAdvanced.Name = "tbpAdvanced";
            this.tbpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAdvanced.Size = new System.Drawing.Size(1154, 224);
            this.tbpAdvanced.TabIndex = 1;
            this.tbpAdvanced.Text = "Advanced";
            this.tbpAdvanced.UseVisualStyleBackColor = true;
            // 
            // dgvAdvanced
            // 
            this.dgvAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdvanced.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerCode,
            this.AccLedgerAccountID,
            this.LedgerName,
            this.ExpenseAmount,
            this.OtherExpenseTempID});
            this.dgvAdvanced.Location = new System.Drawing.Point(3, 0);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.RowHeadersWidth = 15;
            this.dgvAdvanced.Size = new System.Drawing.Size(1057, 201);
            this.dgvAdvanced.TabIndex = 34;
            this.dgvAdvanced.DoubleClick += new System.EventHandler(this.dgvAdvanced_DoubleClick);
            this.dgvAdvanced.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAdvanced_KeyDown);
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 150;
            // 
            // AccLedgerAccountID
            // 
            this.AccLedgerAccountID.DataPropertyName = "AccLedgerAccountID";
            this.AccLedgerAccountID.HeaderText = "AccLedgerAccountID";
            this.AccLedgerAccountID.Name = "AccLedgerAccountID";
            this.AccLedgerAccountID.ReadOnly = true;
            this.AccLedgerAccountID.Visible = false;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerName.Width = 350;
            // 
            // ExpenseAmount
            // 
            this.ExpenseAmount.DataPropertyName = "ExpenseAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ExpenseAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.ExpenseAmount.HeaderText = "Value";
            this.ExpenseAmount.Name = "ExpenseAmount";
            this.ExpenseAmount.ReadOnly = true;
            this.ExpenseAmount.Width = 120;
            // 
            // OtherExpenseTempID
            // 
            this.OtherExpenseTempID.DataPropertyName = "OtherExpenseTempID";
            this.OtherExpenseTempID.HeaderText = "OtherExpenseTempID";
            this.OtherExpenseTempID.Name = "OtherExpenseTempID";
            this.OtherExpenseTempID.ReadOnly = true;
            this.OtherExpenseTempID.Visible = false;
            // 
            // txtOtherExpenceValue
            // 
            this.txtOtherExpenceValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherExpenceValue.Location = new System.Drawing.Point(519, 202);
            this.txtOtherExpenceValue.Name = "txtOtherExpenceValue";
            this.txtOtherExpenceValue.Size = new System.Drawing.Size(126, 21);
            this.txtOtherExpenceValue.TabIndex = 52;
            this.txtOtherExpenceValue.Tag = "3";
            this.txtOtherExpenceValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherExpenceValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherExpenceValue_KeyDown);
            // 
            // txtOtherExpenceName
            // 
            this.txtOtherExpenceName.Location = new System.Drawing.Point(170, 202);
            this.txtOtherExpenceName.Name = "txtOtherExpenceName";
            this.txtOtherExpenceName.Size = new System.Drawing.Size(348, 21);
            this.txtOtherExpenceName.TabIndex = 51;
            this.txtOtherExpenceName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherExpenceName_KeyDown);
            this.txtOtherExpenceName.Leave += new System.EventHandler(this.txtOtherExpenceName_Leave);
            // 
            // chkAutoCompleationOtherExpence
            // 
            this.chkAutoCompleationOtherExpence.AutoSize = true;
            this.chkAutoCompleationOtherExpence.Checked = true;
            this.chkAutoCompleationOtherExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationOtherExpence.Location = new System.Drawing.Point(3, 205);
            this.chkAutoCompleationOtherExpence.Name = "chkAutoCompleationOtherExpence";
            this.chkAutoCompleationOtherExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationOtherExpence.TabIndex = 50;
            this.chkAutoCompleationOtherExpence.Tag = "1";
            this.chkAutoCompleationOtherExpence.UseVisualStyleBackColor = true;
            this.chkAutoCompleationOtherExpence.CheckedChanged += new System.EventHandler(this.chkAutoCompleationOtherExpence_CheckedChanged);
            // 
            // txtOtherExpenceCode
            // 
            this.txtOtherExpenceCode.Location = new System.Drawing.Point(18, 202);
            this.txtOtherExpenceCode.Name = "txtOtherExpenceCode";
            this.txtOtherExpenceCode.Size = new System.Drawing.Size(151, 21);
            this.txtOtherExpenceCode.TabIndex = 49;
            this.txtOtherExpenceCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherExpenceCode_KeyDown);
            this.txtOtherExpenceCode.Leave += new System.EventHandler(this.txtOtherExpenceCode_Leave);
            // 
            // txtOtherDis
            // 
            this.txtOtherDis.ForeColor = System.Drawing.Color.Black;
            this.txtOtherDis.Location = new System.Drawing.Point(815, 63);
            this.txtOtherDis.Name = "txtOtherDis";
            this.txtOtherDis.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherDis.Size = new System.Drawing.Size(60, 21);
            this.txtOtherDis.TabIndex = 68;
            this.txtOtherDis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherDis.Visible = false;
            this.txtOtherDis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherDis_KeyDown);
            // 
            // txtFreeQty
            // 
            this.txtFreeQty.Location = new System.Drawing.Point(639, 9);
            this.txtFreeQty.Name = "txtFreeQty";
            this.txtFreeQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFreeQty.Size = new System.Drawing.Size(49, 21);
            this.txtFreeQty.TabIndex = 53;
            this.txtFreeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFreeQty.Visible = false;
            this.txtFreeQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFreeQty_KeyDown);
            this.txtFreeQty.Leave += new System.EventHandler(this.txtFreeQty_Leave);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(492, 3);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(89, 21);
            this.dtpExpiry.TabIndex = 44;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.Visible = false;
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            this.dtpExpiry.Leave += new System.EventHandler(this.dtpExpiry_Leave);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(739, 6);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(99, 21);
            this.txtSellingPrice.TabIndex = 49;
            this.txtSellingPrice.Tag = "3";
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellingPrice.Visible = false;
            this.txtSellingPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSellingPrice_KeyDown);
            this.txtSellingPrice.Leave += new System.EventHandler(this.txtSellingPrice_Leave);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtOtherDis);
            this.grpFooter.Controls.Add(this.label3);
            this.grpFooter.Controls.Add(this.txtFreeQty);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.txtPayingAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSellingPrice);
            this.grpFooter.Controls.Add(this.dtpExpiry);
            this.grpFooter.Controls.Add(this.cmbBankCode);
            this.grpFooter.Controls.Add(this.lblPaidAmount);
            this.grpFooter.Controls.Add(this.txtPaidAmount);
            this.grpFooter.Controls.Add(this.chkRound);
            this.grpFooter.Controls.Add(this.dtpChequeDate);
            this.grpFooter.Controls.Add(this.lblChequeDate);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.cmbBankName);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblBank);
            this.grpFooter.Controls.Add(this.lblCardCheque);
            this.grpFooter.Controls.Add(this.txtCardChequeNo);
            this.grpFooter.Controls.Add(this.lblPayingAmount);
            this.grpFooter.Controls.Add(this.lblBalanceAmount);
            this.grpFooter.Controls.Add(this.cmbPaymentMethod);
            this.grpFooter.Controls.Add(this.txtBalanceAmount);
            this.grpFooter.Controls.Add(this.lblPaymentMethod);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.tabControl1);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(2, 337);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1163, 199);
            this.grpFooter.TabIndex = 17;
            this.grpFooter.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(857, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 93;
            this.label3.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Red;
            this.txtTotalQty.Location = new System.Drawing.Point(1007, 177);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(149, 21);
            this.txtTotalQty.TabIndex = 92;
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
            this.txtOtherCharges.Location = new System.Drawing.Point(710, 171);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(148, 21);
            this.txtOtherCharges.TabIndex = 91;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.Visible = false;
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            // 
            // txtPayingAmount
            // 
            this.txtPayingAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPayingAmount.Location = new System.Drawing.Point(635, 133);
            this.txtPayingAmount.Name = "txtPayingAmount";
            this.txtPayingAmount.Size = new System.Drawing.Size(174, 21);
            this.txtPayingAmount.TabIndex = 90;
            this.txtPayingAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayingAmount_KeyDown);
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(1008, 34);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(148, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 89;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.Visible = false;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // cmbBankCode
            // 
            this.cmbBankCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankCode.FormattingEnabled = true;
            this.cmbBankCode.Location = new System.Drawing.Point(635, 109);
            this.cmbBankCode.Name = "cmbBankCode";
            this.cmbBankCode.Size = new System.Drawing.Size(53, 21);
            this.cmbBankCode.TabIndex = 50;
            this.cmbBankCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankCode_KeyDown);
            // 
            // lblPaidAmount
            // 
            this.lblPaidAmount.AutoSize = true;
            this.lblPaidAmount.Location = new System.Drawing.Point(528, 159);
            this.lblPaidAmount.Name = "lblPaidAmount";
            this.lblPaidAmount.Size = new System.Drawing.Size(79, 13);
            this.lblPaidAmount.TabIndex = 88;
            this.lblPaidAmount.Text = "Paid Amount";
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(635, 157);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.ReadOnly = true;
            this.txtPaidAmount.Size = new System.Drawing.Size(174, 21);
            this.txtPaidAmount.TabIndex = 87;
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkRound
            // 
            this.chkRound.AutoSize = true;
            this.chkRound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRound.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRound.Location = new System.Drawing.Point(941, 134);
            this.chkRound.Name = "chkRound";
            this.chkRound.Size = new System.Drawing.Size(62, 17);
            this.chkRound.TabIndex = 86;
            this.chkRound.Text = "Round";
            this.chkRound.UseVisualStyleBackColor = true;
            this.chkRound.Visible = false;
            this.chkRound.CheckedChanged += new System.EventHandler(this.chkRound_CheckedChanged);
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChequeDate.Location = new System.Drawing.Point(635, 85);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(174, 21);
            this.dtpChequeDate.TabIndex = 85;
            this.dtpChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpChequeDate_KeyDown);
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Location = new System.Drawing.Point(528, 88);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(82, 13);
            this.lblChequeDate.TabIndex = 84;
            this.lblChequeDate.Text = "Cheque Date";
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(559, 175);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 83;
            this.lblOtherCharges.Text = "Other Charges";
            this.lblOtherCharges.Visible = false;
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(249, 175);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 81;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Visible = false;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(380, 179);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 80;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.Visible = false;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // cmbBankName
            // 
            this.cmbBankName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankName.FormattingEnabled = true;
            this.cmbBankName.Location = new System.Drawing.Point(690, 109);
            this.cmbBankName.Name = "cmbBankName";
            this.cmbBankName.Size = new System.Drawing.Size(119, 21);
            this.cmbBankName.TabIndex = 54;
            this.cmbBankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankName_KeyDown);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(967, 39);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            this.lblSubTotalDiscountPecentage.Visible = false;
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(529, 112);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(36, 13);
            this.lblBank.TabIndex = 49;
            this.lblBank.Text = "Bank";
            // 
            // lblCardCheque
            // 
            this.lblCardCheque.AutoSize = true;
            this.lblCardCheque.Location = new System.Drawing.Point(528, 63);
            this.lblCardCheque.Name = "lblCardCheque";
            this.lblCardCheque.Size = new System.Drawing.Size(107, 13);
            this.lblCardCheque.TabIndex = 48;
            this.lblCardCheque.Text = "Card /Cheque No";
            // 
            // txtCardChequeNo
            // 
            this.txtCardChequeNo.Location = new System.Drawing.Point(635, 60);
            this.txtCardChequeNo.Name = "txtCardChequeNo";
            this.txtCardChequeNo.Size = new System.Drawing.Size(174, 21);
            this.txtCardChequeNo.TabIndex = 47;
            this.txtCardChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardChequeNo_KeyDown);
            // 
            // lblPayingAmount
            // 
            this.lblPayingAmount.AutoSize = true;
            this.lblPayingAmount.Location = new System.Drawing.Point(529, 136);
            this.lblPayingAmount.Name = "lblPayingAmount";
            this.lblPayingAmount.Size = new System.Drawing.Size(93, 13);
            this.lblPayingAmount.TabIndex = 46;
            this.lblPayingAmount.Text = "Paying Amount";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.Location = new System.Drawing.Point(857, 158);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(100, 13);
            this.lblBalanceAmount.TabIndex = 45;
            this.lblBalanceAmount.Text = "Balance Amount";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(635, 35);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(174, 21);
            this.cmbPaymentMethod.TabIndex = 44;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            this.cmbPaymentMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentMethod_KeyDown);
            this.cmbPaymentMethod.Leave += new System.EventHandler(this.cmbPaymentMethod_Leave);
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.Location = new System.Drawing.Point(1008, 154);
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.ReadOnly = true;
            this.txtBalanceAmount.Size = new System.Drawing.Size(148, 21);
            this.txtBalanceAmount.TabIndex = 43;
            this.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(528, 39);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(102, 13);
            this.lblPaymentMethod.TabIndex = 41;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(857, 134);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(267, 179);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(108, 13);
            this.lblTotalTaxAmount.TabIndex = 39;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            this.lblTotalTaxAmount.Visible = false;
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(989, 39);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            this.chkSubTotalDiscountPercentage.Visible = false;
            this.chkSubTotalDiscountPercentage.CheckedChanged += new System.EventHandler(this.chkSubTotalDiscountPercentage_CheckedChanged);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(857, 38);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "WAvg %";
            this.lblSubTotalDiscount.Visible = false;
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(857, 14);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpPaymentDetails);
            this.tabControl1.Controls.Add(this.tbpSupplier);
            this.tabControl1.Controls.Add(this.tbpPageSetup);
            this.tabControl1.Controls.Add(this.tbpOtherDetails);
            this.tabControl1.Location = new System.Drawing.Point(4, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(476, 170);
            this.tabControl1.TabIndex = 34;
            // 
            // tbpPaymentDetails
            // 
            this.tbpPaymentDetails.Controls.Add(this.lblLedger);
            this.tbpPaymentDetails.Controls.Add(this.cmbLedgerName);
            this.tbpPaymentDetails.Controls.Add(this.cmbLedgerCode);
            this.tbpPaymentDetails.Controls.Add(this.dgvPaymentDetails);
            this.tbpPaymentDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpPaymentDetails.Name = "tbpPaymentDetails";
            this.tbpPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPaymentDetails.Size = new System.Drawing.Size(468, 144);
            this.tbpPaymentDetails.TabIndex = 0;
            this.tbpPaymentDetails.Text = "Payment Details";
            this.tbpPaymentDetails.UseVisualStyleBackColor = true;
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(10, 8);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(46, 13);
            this.lblLedger.TabIndex = 41;
            this.lblLedger.Text = "Ledger";
            // 
            // cmbLedgerName
            // 
            this.cmbLedgerName.FormattingEnabled = true;
            this.cmbLedgerName.Location = new System.Drawing.Point(217, 5);
            this.cmbLedgerName.Name = "cmbLedgerName";
            this.cmbLedgerName.Size = new System.Drawing.Size(247, 21);
            this.cmbLedgerName.TabIndex = 40;
            // 
            // cmbLedgerCode
            // 
            this.cmbLedgerCode.FormattingEnabled = true;
            this.cmbLedgerCode.Location = new System.Drawing.Point(106, 5);
            this.cmbLedgerCode.Name = "cmbLedgerCode";
            this.cmbLedgerCode.Size = new System.Drawing.Size(109, 21);
            this.cmbLedgerCode.TabIndex = 39;
            // 
            // dgvPaymentDetails
            // 
            this.dgvPaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentMethod,
            this.CardCheqNo,
            this.ChequeDate,
            this.PayAmount,
            this.PaymentMethodID,
            this.PaymentTempID});
            this.dgvPaymentDetails.Location = new System.Drawing.Point(5, 30);
            this.dgvPaymentDetails.Name = "dgvPaymentDetails";
            this.dgvPaymentDetails.RowHeadersWidth = 15;
            this.dgvPaymentDetails.Size = new System.Drawing.Size(458, 111);
            this.dgvPaymentDetails.TabIndex = 38;
            this.dgvPaymentDetails.DoubleClick += new System.EventHandler(this.dgvPaymentDetails_DoubleClick);
            this.dgvPaymentDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPaymentDetails_KeyDown);
            // 
            // PaymentMethod
            // 
            this.PaymentMethod.DataPropertyName = "PaymentMethod";
            this.PaymentMethod.HeaderText = "Mode";
            this.PaymentMethod.Name = "PaymentMethod";
            this.PaymentMethod.ReadOnly = true;
            this.PaymentMethod.Width = 90;
            // 
            // CardCheqNo
            // 
            this.CardCheqNo.DataPropertyName = "CardCheqNo";
            this.CardCheqNo.HeaderText = "No";
            this.CardCheqNo.Name = "CardCheqNo";
            this.CardCheqNo.ReadOnly = true;
            this.CardCheqNo.Width = 140;
            // 
            // ChequeDate
            // 
            this.ChequeDate.DataPropertyName = "ChequeDate";
            this.ChequeDate.HeaderText = "Date";
            this.ChequeDate.Name = "ChequeDate";
            this.ChequeDate.ReadOnly = true;
            this.ChequeDate.Width = 80;
            // 
            // PayAmount
            // 
            this.PayAmount.DataPropertyName = "PayAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PayAmount.DefaultCellStyle = dataGridViewCellStyle9;
            this.PayAmount.HeaderText = "Amount";
            this.PayAmount.Name = "PayAmount";
            this.PayAmount.ReadOnly = true;
            this.PayAmount.Width = 110;
            // 
            // PaymentMethodID
            // 
            this.PaymentMethodID.DataPropertyName = "PaymentMethodID";
            this.PaymentMethodID.HeaderText = "PaymentMethodID";
            this.PaymentMethodID.Name = "PaymentMethodID";
            this.PaymentMethodID.ReadOnly = true;
            this.PaymentMethodID.Visible = false;
            // 
            // PaymentTempID
            // 
            this.PaymentTempID.DataPropertyName = "PaymentTempID";
            this.PaymentTempID.HeaderText = "PaymentTempID";
            this.PaymentTempID.Name = "PaymentTempID";
            this.PaymentTempID.ReadOnly = true;
            this.PaymentTempID.Visible = false;
            // 
            // tbpSupplier
            // 
            this.tbpSupplier.Controls.Add(this.lblTotalDueAmount);
            this.tbpSupplier.Controls.Add(this.txtTotalDueAmount);
            this.tbpSupplier.Controls.Add(this.lblPaymentTerms);
            this.tbpSupplier.Controls.Add(this.txtPaymentTerms);
            this.tbpSupplier.Location = new System.Drawing.Point(4, 22);
            this.tbpSupplier.Name = "tbpSupplier";
            this.tbpSupplier.Size = new System.Drawing.Size(468, 144);
            this.tbpSupplier.TabIndex = 3;
            this.tbpSupplier.Text = "Supplier";
            this.tbpSupplier.UseVisualStyleBackColor = true;
            // 
            // lblTotalDueAmount
            // 
            this.lblTotalDueAmount.AutoSize = true;
            this.lblTotalDueAmount.Location = new System.Drawing.Point(9, 33);
            this.lblTotalDueAmount.Name = "lblTotalDueAmount";
            this.lblTotalDueAmount.Size = new System.Drawing.Size(110, 13);
            this.lblTotalDueAmount.TabIndex = 54;
            this.lblTotalDueAmount.Text = "Total Due Amount";
            // 
            // txtTotalDueAmount
            // 
            this.txtTotalDueAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalDueAmount.Location = new System.Drawing.Point(135, 30);
            this.txtTotalDueAmount.Name = "txtTotalDueAmount";
            this.txtTotalDueAmount.Size = new System.Drawing.Size(142, 21);
            this.txtTotalDueAmount.TabIndex = 53;
            this.txtTotalDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(9, 10);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(97, 13);
            this.lblPaymentTerms.TabIndex = 52;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPaymentTerms.Location = new System.Drawing.Point(135, 6);
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.Size = new System.Drawing.Size(142, 21);
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
            this.tbpPageSetup.Size = new System.Drawing.Size(468, 144);
            this.tbpPageSetup.TabIndex = 2;
            this.tbpPageSetup.Text = "Page Setup";
            this.tbpPageSetup.UseVisualStyleBackColor = true;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Location = new System.Drawing.Point(234, 59);
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
            this.rdoPortrait.Location = new System.Drawing.Point(125, 59);
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
            this.cmbPaperSize.Location = new System.Drawing.Point(125, 32);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(207, 21);
            this.cmbPaperSize.TabIndex = 49;
            // 
            // lblOrientation
            // 
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new System.Drawing.Point(9, 61);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(70, 13);
            this.lblOrientation.TabIndex = 48;
            this.lblOrientation.Text = "Orientation";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 7);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(207, 21);
            this.cmbPrinter.TabIndex = 47;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(9, 35);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(68, 13);
            this.lblPaperSize.TabIndex = 46;
            this.lblPaperSize.Text = "Paper Size";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(9, 10);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 45;
            this.lblPrinter.Text = "Printer";
            // 
            // tbpOtherDetails
            // 
            this.tbpOtherDetails.Controls.Add(this.cmbCostCentre);
            this.tbpOtherDetails.Controls.Add(this.lblCostCentre);
            this.tbpOtherDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpOtherDetails.Name = "tbpOtherDetails";
            this.tbpOtherDetails.Size = new System.Drawing.Size(468, 144);
            this.tbpOtherDetails.TabIndex = 4;
            this.tbpOtherDetails.Text = "Other Details";
            this.tbpOtherDetails.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(102, 5);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(225, 21);
            this.cmbCostCentre.TabIndex = 67;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(10, 8);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 66;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount.Location = new System.Drawing.Point(1008, 130);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(148, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(399, 175);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(148, 21);
            this.txtTotalTaxAmount.TabIndex = 32;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalTaxAmount.Visible = false;
            this.txtTotalTaxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalTaxAmount_KeyUp);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(1008, 58);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(148, 21);
            this.txtSubTotalDiscount.TabIndex = 31;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscount.Visible = false;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.Location = new System.Drawing.Point(1008, 11);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(148, 21);
            this.txtGrossAmount.TabIndex = 30;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.cmbPaymentTerms);
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.txtSupplierInvoiceNo);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.chkConsignmentBasis);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpGrnDate);
            this.grpHeader.Controls.Add(this.btnPoDetails);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.lblGrnDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPoNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtPurchaseOrderNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1162, 87);
            this.grpHeader.TabIndex = 15;
            this.grpHeader.TabStop = false;
            // 
            // cmbPaymentTerms
            // 
            this.cmbPaymentTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTerms.FormattingEnabled = true;
            this.cmbPaymentTerms.Location = new System.Drawing.Point(710, 60);
            this.cmbPaymentTerms.Name = "cmbPaymentTerms";
            this.cmbPaymentTerms.Size = new System.Drawing.Size(177, 21);
            this.cmbPaymentTerms.TabIndex = 88;
            this.cmbPaymentTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentTerms_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Payment Terms";
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTStatus.Location = new System.Drawing.Point(1144, 38);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(15, 14);
            this.chkTStatus.TabIndex = 87;
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Location = new System.Drawing.Point(1007, 37);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 45;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.CheckedChanged += new System.EventHandler(this.chkOverwrite_CheckedChanged);
            this.chkOverwrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkOverwrite_KeyDown);
            // 
            // txtSupplierInvoiceNo
            // 
            this.txtSupplierInvoiceNo.Location = new System.Drawing.Point(710, 36);
            this.txtSupplierInvoiceNo.Name = "txtSupplierInvoiceNo";
            this.txtSupplierInvoiceNo.Size = new System.Drawing.Size(177, 21);
            this.txtSupplierInvoiceNo.TabIndex = 65;
            this.txtSupplierInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierInvoiceNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(589, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Supplier Invoice No";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(951, 60);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(207, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(891, 63);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // chkConsignmentBasis
            // 
            this.chkConsignmentBasis.AutoSize = true;
            this.chkConsignmentBasis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConsignmentBasis.Location = new System.Drawing.Point(888, 37);
            this.chkConsignmentBasis.Name = "chkConsignmentBasis";
            this.chkConsignmentBasis.Size = new System.Drawing.Size(101, 17);
            this.chkConsignmentBasis.TabIndex = 25;
            this.chkConsignmentBasis.Text = "Consignment";
            this.chkConsignmentBasis.UseVisualStyleBackColor = true;
            this.chkConsignmentBasis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkConsignmentBasis_KeyDown);
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(710, 12);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(177, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(589, 15);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpGrnDate
            // 
            this.dtpGrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGrnDate.Location = new System.Drawing.Point(951, 10);
            this.dtpGrnDate.Name = "dtpGrnDate";
            this.dtpGrnDate.Size = new System.Drawing.Size(207, 21);
            this.dtpGrnDate.TabIndex = 22;
            this.dtpGrnDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpGrnDate_KeyDown);
            this.dtpGrnDate.Leave += new System.EventHandler(this.dtpGrnDate_Leave);
            // 
            // btnPoDetails
            // 
            this.btnPoDetails.Location = new System.Drawing.Point(553, 11);
            this.btnPoDetails.Name = "btnPoDetails";
            this.btnPoDetails.Size = new System.Drawing.Size(28, 23);
            this.btnPoDetails.TabIndex = 21;
            this.btnPoDetails.Text = "...";
            this.btnPoDetails.UseVisualStyleBackColor = true;
            this.btnPoDetails.Click += new System.EventHandler(this.btnPoDetails_Click);
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
            this.lblRemark.Location = new System.Drawing.Point(6, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 60);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
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
            this.txtSupplierName.Location = new System.Drawing.Point(244, 36);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(337, 21);
            this.txtSupplierName.TabIndex = 12;
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierName_KeyDown);
            this.txtSupplierName.Leave += new System.EventHandler(this.txtSupplierName_Leave);
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(90, 39);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 11;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSupplier_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(107, 36);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 10;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            this.txtSupplierCode.Leave += new System.EventHandler(this.txtSupplierCode_Leave);
            // 
            // lblGrnDate
            // 
            this.lblGrnDate.AutoSize = true;
            this.lblGrnDate.Location = new System.Drawing.Point(889, 16);
            this.lblGrnDate.Name = "lblGrnDate";
            this.lblGrnDate.Size = new System.Drawing.Size(63, 13);
            this.lblGrnDate.TabIndex = 9;
            this.lblGrnDate.Text = "GRN Date";
            // 
            // chkAutoCompleationPoNo
            // 
            this.chkAutoCompleationPoNo.AutoSize = true;
            this.chkAutoCompleationPoNo.Checked = true;
            this.chkAutoCompleationPoNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPoNo.Location = new System.Drawing.Point(399, 16);
            this.chkAutoCompleationPoNo.Name = "chkAutoCompleationPoNo";
            this.chkAutoCompleationPoNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPoNo.TabIndex = 5;
            this.chkAutoCompleationPoNo.Tag = "1";
            this.chkAutoCompleationPoNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPoNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPoNo_CheckedChanged);
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtPurchaseOrderNo
            // 
            this.txtPurchaseOrderNo.Location = new System.Drawing.Point(416, 12);
            this.txtPurchaseOrderNo.Name = "txtPurchaseOrderNo";
            this.txtPurchaseOrderNo.Size = new System.Drawing.Size(136, 21);
            this.txtPurchaseOrderNo.TabIndex = 3;
            this.txtPurchaseOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseOrderNo_KeyDown);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblPurchaseOrderNo
            // 
            this.lblPurchaseOrderNo.AutoSize = true;
            this.lblPurchaseOrderNo.Location = new System.Drawing.Point(281, 16);
            this.lblPurchaseOrderNo.Name = "lblPurchaseOrderNo";
            this.lblPurchaseOrderNo.Size = new System.Drawing.Size(115, 13);
            this.lblPurchaseOrderNo.TabIndex = 1;
            this.lblPurchaseOrderNo.Text = "Purchase Order No";
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
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // FrmGoodsReceivedNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1167, 578);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmGoodsReceivedNote";
            this.Text = "Goods Received Note";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpBody.ResumeLayout(false);
            this.tabGRN.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.tbpAdvanced.ResumeLayout(false);
            this.tbpAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbpPaymentDetails.ResumeLayout(false);
            this.tbpPaymentDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetails)).EndInit();
            this.tbpSupplier.ResumeLayout(false);
            this.tbpSupplier.PerformLayout();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.ComboBox cmbBankName;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.ComboBox cmbBankCode;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblCardCheque;
        private System.Windows.Forms.TextBox txtCardChequeNo;
        private System.Windows.Forms.Label lblPayingAmount;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.TextBox txtBalanceAmount;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpPaymentDetails;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.ComboBox cmbLedgerName;
        private System.Windows.Forms.ComboBox cmbLedgerCode;
        private System.Windows.Forms.DataGridView dgvPaymentDetails;
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
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private System.Windows.Forms.TextBox txtSubTotalDiscount;
        private System.Windows.Forms.TextBox txtGrossAmount;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.CheckBox chkConsignmentBasis;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpGrnDate;
        private System.Windows.Forms.Button btnPoDetails;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblGrnDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationPoNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtPurchaseOrderNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.CheckBox chkRound;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.Label lblOtherCharges;
        private System.Windows.Forms.TextBox txtSupplierInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpOtherDetails;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.TabControl tabGRN;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TabPage tbpAdvanced;
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private CustomControls.TextBoxCurrency txtProductDiscount;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private CustomControls.TextBoxQty txtQty;
        private CustomControls.TextBoxQty txtFreeQty;
        private CustomControls.TextBoxPercentGen txtProductDiscountPercentage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblPaidAmount;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCheqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethodID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentTempID;
        private CustomControls.TextBoxCurrency txtOtherExpenceValue;
        private System.Windows.Forms.TextBox txtOtherExpenceName;
        private System.Windows.Forms.CheckBox chkAutoCompleationOtherExpence;
        private System.Windows.Forms.TextBox txtOtherExpenceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenceTempID;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private CustomControls.TextBoxCurrency txtPayingAmount;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenseTempID;
        private CustomControls.TextBoxPercentGen txtOtherDis;
        private CustomControls.TextBoxPercentGen txtWavgDis;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn FreeQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
