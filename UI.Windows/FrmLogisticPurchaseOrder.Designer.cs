﻿namespace UI.Windows
{
    partial class FrmLogisticPurchaseOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtFreeQty = new System.Windows.Forms.TextBox();
            this.txtProductDiscountPer = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductAmount = new System.Windows.Forms.TextBox();
            this.txtProductDiscountAmount = new System.Windows.Forms.TextBox();
            this.txtPackSize = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Free = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.dtpPaymentExpectedDate = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentExpectedDate = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblExpectedDate = new System.Windows.Forms.Label();
            this.dtpExpectedDate = new System.Windows.Forms.DateTimePicker();
            this.cmbPaymentTerms = new System.Windows.Forms.ComboBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.dtpPODate = new System.Windows.Forms.DateTimePicker();
            this.lblPoDate = new System.Windows.Forms.Label();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkConsignmentBasis = new System.Windows.Forms.CheckBox();
            this.txtValidityPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.btnQuotationDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationQuotation = new System.Windows.Forms.CheckBox();
            this.txtQuotationNo = new System.Windows.Forms.TextBox();
            this.lblQuotationNo = new System.Windows.Forms.Label();
            this.lblValidityPeriod = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.btnPurchaseRequestDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationPurchaseRequestNo = new System.Windows.Forms.CheckBox();
            this.txtPurchaseRequestNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseRequestNo = new System.Windows.Forms.Label();
            this.btnPoDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationPoNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.txtOtherCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtSubTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.TextBox();
            this.grpFooter1 = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tbpStockDetails = new System.Windows.Forms.TabPage();
            this.chkViewStokDetails = new System.Windows.Forms.CheckBox();
            this.dgvStockDetails = new System.Windows.Forms.DataGridView();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReOrderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpSupplierInfo = new System.Windows.Forms.TabPage();
            this.lblTotalDueAmount = new System.Windows.Forms.Label();
            this.txtTotalDueAmount = new System.Windows.Forms.TextBox();
            this.tbpPageSetup = new System.Windows.Forms.TabPage();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.grpFooter2 = new System.Windows.Forms.GroupBox();
            this.txtOrderCircle = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblLocationStatus = new System.Windows.Forms.Label();
            this.cmbLocationStatus = new System.Windows.Forms.ComboBox();
            this.lblOrderCircle = new System.Windows.Forms.Label();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.lblSelectionCriteria = new System.Windows.Forms.Label();
            this.cmbSelectionCriteria = new System.Windows.Forms.ComboBox();
            this.tbMore = new System.Windows.Forms.TabControl();
            this.tbpTransfer = new System.Windows.Forms.TabPage();
            this.lblTransferToDate = new System.Windows.Forms.Label();
            this.lblTransferFromDate = new System.Windows.Forms.Label();
            this.dtpTransferToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTransferFromDate = new System.Windows.Forms.DateTimePicker();
            this.tbpPurchase = new System.Windows.Forms.TabPage();
            this.dtpPurchaseToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPurchaseFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblPurToDate = new System.Windows.Forms.Label();
            this.lblPurFromDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.grpFooter1.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpStockDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).BeginInit();
            this.tbpSupplierInfo.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.grpFooter2.SuspendLayout();
            this.tbMore.SuspendLayout();
            this.tbpTransfer.SuspendLayout();
            this.tbpPurchase.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(822, 484);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 484);
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtRate);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.txtFreeQty);
            this.grpBody.Controls.Add(this.txtProductDiscountPer);
            this.grpBody.Controls.Add(this.textBox2);
            this.grpBody.Controls.Add(this.textBox1);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductAmount);
            this.grpBody.Controls.Add(this.txtProductDiscountAmount);
            this.grpBody.Controls.Add(this.txtPackSize);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Location = new System.Drawing.Point(1, 101);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1139, 214);
            this.grpBody.TabIndex = 37;
            this.grpBody.TabStop = false;
            // 
            // txtRate
            // 
            this.txtRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRate.Location = new System.Drawing.Point(753, 190);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(102, 21);
            this.txtRate.TabIndex = 53;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(6, 194);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 52;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // txtFreeQty
            // 
            this.txtFreeQty.Location = new System.Drawing.Point(660, 190);
            this.txtFreeQty.Name = "txtFreeQty";
            this.txtFreeQty.Size = new System.Drawing.Size(46, 21);
            this.txtFreeQty.TabIndex = 51;
            this.txtFreeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFreeQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFreeQty_KeyDown);
            this.txtFreeQty.Leave += new System.EventHandler(this.txtFreeQty_Leave);
            // 
            // txtProductDiscountPer
            // 
            this.txtProductDiscountPer.ForeColor = System.Drawing.Color.Red;
            this.txtProductDiscountPer.Location = new System.Drawing.Point(856, 190);
            this.txtProductDiscountPer.Name = "txtProductDiscountPer";
            this.txtProductDiscountPer.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscountPer.Size = new System.Drawing.Size(45, 21);
            this.txtProductDiscountPer.TabIndex = 46;
            this.txtProductDiscountPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountPer_KeyDown);
            this.txtProductDiscountPer.Leave += new System.EventHandler(this.txtProductDiscountPer_Leave);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(563, 190);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(50, 21);
            this.textBox2.TabIndex = 50;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(514, 190);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(49, 21);
            this.textBox1.TabIndex = 49;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(446, 190);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(68, 21);
            this.cmbUnit.TabIndex = 42;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.Location = new System.Drawing.Point(984, 190);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(147, 21);
            this.txtProductAmount.TabIndex = 48;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            this.txtProductAmount.Leave += new System.EventHandler(this.txtProductAmount_Leave);
            // 
            // txtProductDiscountAmount
            // 
            this.txtProductDiscountAmount.Location = new System.Drawing.Point(902, 190);
            this.txtProductDiscountAmount.Name = "txtProductDiscountAmount";
            this.txtProductDiscountAmount.Size = new System.Drawing.Size(81, 21);
            this.txtProductDiscountAmount.TabIndex = 47;
            this.txtProductDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountAmount_KeyDown);
            this.txtProductDiscountAmount.Leave += new System.EventHandler(this.txtProductDiscountAmount_Leave);
            // 
            // txtPackSize
            // 
            this.txtPackSize.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtPackSize.Enabled = false;
            this.txtPackSize.Location = new System.Drawing.Point(706, 190);
            this.txtPackSize.Name = "txtPackSize";
            this.txtPackSize.ReadOnly = true;
            this.txtPackSize.Size = new System.Drawing.Size(46, 21);
            this.txtPackSize.TabIndex = 44;
            this.txtPackSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(613, 190);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(46, 21);
            this.txtQty.TabIndex = 43;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(203, 190);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(242, 21);
            this.txtProductName.TabIndex = 41;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 190);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(180, 21);
            this.txtProductCode.TabIndex = 40;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.PQty,
            this.SQty,
            this.OrderQty,
            this.Free,
            this.PackSize,
            this.CostPrice,
            this.DiscountPercentage,
            this.DiscountAmount,
            this.NetAmount});
            this.dgvItemDetails.Location = new System.Drawing.Point(6, 11);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 20;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1128, 176);
            this.dgvItemDetails.TabIndex = 39;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // Row
            // 
            this.Row.DataPropertyName = "LineNo";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Row.DefaultCellStyle = dataGridViewCellStyle14;
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle15;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 145;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle16;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 242;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle17;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.Width = 60;
            // 
            // PQty
            // 
            this.PQty.DataPropertyName = "PurchaseQty";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PQty.DefaultCellStyle = dataGridViewCellStyle18;
            this.PQty.HeaderText = "P Qty";
            this.PQty.Name = "PQty";
            this.PQty.ReadOnly = true;
            this.PQty.Width = 50;
            // 
            // SQty
            // 
            this.SQty.DataPropertyName = "TransferQty";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SQty.DefaultCellStyle = dataGridViewCellStyle19;
            this.SQty.HeaderText = "T Qty";
            this.SQty.Name = "SQty";
            this.SQty.ReadOnly = true;
            this.SQty.Width = 50;
            // 
            // OrderQty
            // 
            this.OrderQty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OrderQty.DefaultCellStyle = dataGridViewCellStyle20;
            this.OrderQty.HeaderText = "Qty";
            this.OrderQty.Name = "OrderQty";
            this.OrderQty.ReadOnly = true;
            this.OrderQty.Width = 50;
            // 
            // Free
            // 
            this.Free.DataPropertyName = "FreeQty";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Free.DefaultCellStyle = dataGridViewCellStyle21;
            this.Free.HeaderText = "Free";
            this.Free.Name = "Free";
            this.Free.ReadOnly = true;
            this.Free.Width = 50;
            // 
            // PackSize
            // 
            this.PackSize.DataPropertyName = "PackSize";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PackSize.DefaultCellStyle = dataGridViewCellStyle22;
            this.PackSize.HeaderText = "P Size";
            this.PackSize.Name = "PackSize";
            this.PackSize.ReadOnly = true;
            this.PackSize.Width = 48;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle23;
            this.CostPrice.HeaderText = "Rate";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            // 
            // DiscountPercentage
            // 
            this.DiscountPercentage.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPercentage.DefaultCellStyle = dataGridViewCellStyle24;
            this.DiscountPercentage.HeaderText = "Dis%";
            this.DiscountPercentage.Name = "DiscountPercentage";
            this.DiscountPercentage.ReadOnly = true;
            this.DiscountPercentage.Width = 45;
            // 
            // DiscountAmount
            // 
            this.DiscountAmount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmount.DefaultCellStyle = dataGridViewCellStyle25;
            this.DiscountAmount.HeaderText = "Discount";
            this.DiscountAmount.Name = "DiscountAmount";
            this.DiscountAmount.ReadOnly = true;
            this.DiscountAmount.Width = 81;
            // 
            // NetAmount
            // 
            this.NetAmount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NetAmount.DefaultCellStyle = dataGridViewCellStyle26;
            this.NetAmount.HeaderText = "Amount";
            this.NetAmount.Name = "NetAmount";
            this.NetAmount.ReadOnly = true;
            this.NetAmount.Width = 130;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.dtpPaymentExpectedDate);
            this.grpHeader.Controls.Add(this.lblPaymentExpectedDate);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.lblExpectedDate);
            this.grpHeader.Controls.Add(this.dtpExpectedDate);
            this.grpHeader.Controls.Add(this.cmbPaymentTerms);
            this.grpHeader.Controls.Add(this.lblPaymentTerms);
            this.grpHeader.Controls.Add(this.dtpPODate);
            this.grpHeader.Controls.Add(this.lblPoDate);
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.chkConsignmentBasis);
            this.grpHeader.Controls.Add(this.txtValidityPeriod);
            this.grpHeader.Controls.Add(this.btnQuotationDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationQuotation);
            this.grpHeader.Controls.Add(this.txtQuotationNo);
            this.grpHeader.Controls.Add(this.lblQuotationNo);
            this.grpHeader.Controls.Add(this.lblValidityPeriod);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.btnPurchaseRequestDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPurchaseRequestNo);
            this.grpHeader.Controls.Add(this.txtPurchaseRequestNo);
            this.grpHeader.Controls.Add(this.lblPurchaseRequestNo);
            this.grpHeader.Controls.Add(this.btnPoDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPoNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1139, 112);
            this.grpHeader.TabIndex = 36;
            this.grpHeader.TabStop = false;
            // 
            // dtpPaymentExpectedDate
            // 
            this.dtpPaymentExpectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentExpectedDate.Location = new System.Drawing.Point(592, 60);
            this.dtpPaymentExpectedDate.Name = "dtpPaymentExpectedDate";
            this.dtpPaymentExpectedDate.Size = new System.Drawing.Size(142, 21);
            this.dtpPaymentExpectedDate.TabIndex = 110;
            this.dtpPaymentExpectedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpPaymentExpectedDate_KeyDown);
            // 
            // lblPaymentExpectedDate
            // 
            this.lblPaymentExpectedDate.AutoSize = true;
            this.lblPaymentExpectedDate.Location = new System.Drawing.Point(483, 65);
            this.lblPaymentExpectedDate.Name = "lblPaymentExpectedDate";
            this.lblPaymentExpectedDate.Size = new System.Drawing.Size(58, 13);
            this.lblPaymentExpectedDate.TabIndex = 109;
            this.lblPaymentExpectedDate.Text = "P.E. Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(975, 81);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(157, 21);
            this.cmbLocation.TabIndex = 105;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(873, 84);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 108;
            this.lblLocation.Text = "Location";
            // 
            // lblExpectedDate
            // 
            this.lblExpectedDate.AutoSize = true;
            this.lblExpectedDate.Location = new System.Drawing.Point(873, 37);
            this.lblExpectedDate.Name = "lblExpectedDate";
            this.lblExpectedDate.Size = new System.Drawing.Size(90, 13);
            this.lblExpectedDate.TabIndex = 106;
            this.lblExpectedDate.Text = "Expected Date";
            // 
            // dtpExpectedDate
            // 
            this.dtpExpectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpectedDate.Location = new System.Drawing.Point(975, 33);
            this.dtpExpectedDate.Name = "dtpExpectedDate";
            this.dtpExpectedDate.Size = new System.Drawing.Size(157, 21);
            this.dtpExpectedDate.TabIndex = 102;
            this.dtpExpectedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpectedDate_KeyDown);
            this.dtpExpectedDate.Leave += new System.EventHandler(this.dtpExpectedDate_Leave);
            // 
            // cmbPaymentTerms
            // 
            this.cmbPaymentTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTerms.FormattingEnabled = true;
            this.cmbPaymentTerms.Location = new System.Drawing.Point(975, 57);
            this.cmbPaymentTerms.Name = "cmbPaymentTerms";
            this.cmbPaymentTerms.Size = new System.Drawing.Size(157, 21);
            this.cmbPaymentTerms.TabIndex = 104;
            this.cmbPaymentTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentTerms_KeyDown);
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(873, 60);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(96, 13);
            this.lblPaymentTerms.TabIndex = 107;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // dtpPODate
            // 
            this.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPODate.Location = new System.Drawing.Point(975, 10);
            this.dtpPODate.Name = "dtpPODate";
            this.dtpPODate.Size = new System.Drawing.Size(157, 21);
            this.dtpPODate.TabIndex = 103;
            this.dtpPODate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpPODate_KeyDown);
            this.dtpPODate.Leave += new System.EventHandler(this.dtpPODate_Leave);
            // 
            // lblPoDate
            // 
            this.lblPoDate.AutoSize = true;
            this.lblPoDate.Location = new System.Drawing.Point(873, 16);
            this.lblPoDate.Name = "lblPoDate";
            this.lblPoDate.Size = new System.Drawing.Size(54, 13);
            this.lblPoDate.TabIndex = 101;
            this.lblPoDate.Text = "PO Date";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(764, 36);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 100;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkConsignmentBasis
            // 
            this.chkConsignmentBasis.AutoSize = true;
            this.chkConsignmentBasis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConsignmentBasis.Location = new System.Drawing.Point(735, 15);
            this.chkConsignmentBasis.Name = "chkConsignmentBasis";
            this.chkConsignmentBasis.Size = new System.Drawing.Size(135, 17);
            this.chkConsignmentBasis.TabIndex = 99;
            this.chkConsignmentBasis.Text = "Consignment Basis";
            this.chkConsignmentBasis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConsignmentBasis.UseVisualStyleBackColor = true;
            // 
            // txtValidityPeriod
            // 
            this.txtValidityPeriod.IntValue = 0;
            this.txtValidityPeriod.Location = new System.Drawing.Point(592, 36);
            this.txtValidityPeriod.Name = "txtValidityPeriod";
            this.txtValidityPeriod.Size = new System.Drawing.Size(142, 21);
            this.txtValidityPeriod.TabIndex = 98;
            this.txtValidityPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValidityPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValidityPeriod_KeyDown);
            // 
            // btnQuotationDetails
            // 
            this.btnQuotationDetails.Location = new System.Drawing.Point(706, 12);
            this.btnQuotationDetails.Name = "btnQuotationDetails";
            this.btnQuotationDetails.Size = new System.Drawing.Size(28, 23);
            this.btnQuotationDetails.TabIndex = 96;
            this.btnQuotationDetails.Text = "...";
            this.btnQuotationDetails.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationQuotation
            // 
            this.chkAutoCompleationQuotation.AutoSize = true;
            this.chkAutoCompleationQuotation.Checked = true;
            this.chkAutoCompleationQuotation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationQuotation.Location = new System.Drawing.Point(568, 18);
            this.chkAutoCompleationQuotation.Name = "chkAutoCompleationQuotation";
            this.chkAutoCompleationQuotation.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationQuotation.TabIndex = 94;
            this.chkAutoCompleationQuotation.Tag = "1";
            this.chkAutoCompleationQuotation.UseVisualStyleBackColor = true;
            this.chkAutoCompleationQuotation.CheckedChanged += new System.EventHandler(this.chkAutoCompleationQuotation_CheckedChanged);
            // 
            // txtQuotationNo
            // 
            this.txtQuotationNo.Location = new System.Drawing.Point(592, 13);
            this.txtQuotationNo.Name = "txtQuotationNo";
            this.txtQuotationNo.Size = new System.Drawing.Size(114, 21);
            this.txtQuotationNo.TabIndex = 95;
            this.txtQuotationNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuotationNo_KeyDown);
            this.txtQuotationNo.Leave += new System.EventHandler(this.txtQuotationNo_Leave);
            // 
            // lblQuotationNo
            // 
            this.lblQuotationNo.AutoSize = true;
            this.lblQuotationNo.Location = new System.Drawing.Point(482, 17);
            this.lblQuotationNo.Name = "lblQuotationNo";
            this.lblQuotationNo.Size = new System.Drawing.Size(81, 13);
            this.lblQuotationNo.TabIndex = 97;
            this.lblQuotationNo.Text = "Quotation No";
            // 
            // lblValidityPeriod
            // 
            this.lblValidityPeriod.AutoSize = true;
            this.lblValidityPeriod.Location = new System.Drawing.Point(483, 41);
            this.lblValidityPeriod.Name = "lblValidityPeriod";
            this.lblValidityPeriod.Size = new System.Drawing.Size(111, 13);
            this.lblValidityPeriod.TabIndex = 92;
            this.lblValidityPeriod.Text = "Validity Period (D)";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(592, 84);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(143, 21);
            this.txtReferenceNo.TabIndex = 90;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(483, 87);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 91;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(73, 62);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(407, 45);
            this.txtRemark.TabIndex = 89;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.Location = new System.Drawing.Point(741, 87);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(91, 17);
            this.chkTStatus.TabIndex = 86;
            this.chkTStatus.Text = "CheckBox1";
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // btnPurchaseRequestDetails
            // 
            this.btnPurchaseRequestDetails.Location = new System.Drawing.Point(452, 12);
            this.btnPurchaseRequestDetails.Name = "btnPurchaseRequestDetails";
            this.btnPurchaseRequestDetails.Size = new System.Drawing.Size(28, 23);
            this.btnPurchaseRequestDetails.TabIndex = 5;
            this.btnPurchaseRequestDetails.Text = "...";
            this.btnPurchaseRequestDetails.UseVisualStyleBackColor = true;
            this.btnPurchaseRequestDetails.Visible = false;
            // 
            // chkAutoCompleationPurchaseRequestNo
            // 
            this.chkAutoCompleationPurchaseRequestNo.AutoSize = true;
            this.chkAutoCompleationPurchaseRequestNo.Checked = true;
            this.chkAutoCompleationPurchaseRequestNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPurchaseRequestNo.Location = new System.Drawing.Point(301, 17);
            this.chkAutoCompleationPurchaseRequestNo.Name = "chkAutoCompleationPurchaseRequestNo";
            this.chkAutoCompleationPurchaseRequestNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPurchaseRequestNo.TabIndex = 3;
            this.chkAutoCompleationPurchaseRequestNo.Tag = "1";
            this.chkAutoCompleationPurchaseRequestNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPurchaseRequestNo.Visible = false;
            this.chkAutoCompleationPurchaseRequestNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPurchaseRequestNo_CheckedChanged);
            // 
            // txtPurchaseRequestNo
            // 
            this.txtPurchaseRequestNo.Location = new System.Drawing.Point(318, 13);
            this.txtPurchaseRequestNo.Name = "txtPurchaseRequestNo";
            this.txtPurchaseRequestNo.Size = new System.Drawing.Size(133, 21);
            this.txtPurchaseRequestNo.TabIndex = 4;
            this.txtPurchaseRequestNo.Visible = false;
            // 
            // lblPurchaseRequestNo
            // 
            this.lblPurchaseRequestNo.AutoSize = true;
            this.lblPurchaseRequestNo.Location = new System.Drawing.Point(237, 17);
            this.lblPurchaseRequestNo.Name = "lblPurchaseRequestNo";
            this.lblPurchaseRequestNo.Size = new System.Drawing.Size(63, 13);
            this.lblPurchaseRequestNo.TabIndex = 29;
            this.lblPurchaseRequestNo.Text = "P Req. No";
            this.lblPurchaseRequestNo.Visible = false;
            // 
            // btnPoDetails
            // 
            this.btnPoDetails.Location = new System.Drawing.Point(207, 12);
            this.btnPoDetails.Name = "btnPoDetails";
            this.btnPoDetails.Size = new System.Drawing.Size(28, 23);
            this.btnPoDetails.TabIndex = 2;
            this.btnPoDetails.Text = "...";
            this.btnPoDetails.UseVisualStyleBackColor = true;
            this.btnPoDetails.Click += new System.EventHandler(this.btnPoDetails_Click);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(3, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(3, 39);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 13;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(208, 38);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(272, 21);
            this.txtSupplierName.TabIndex = 8;
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierName_KeyDown);
            this.txtSupplierName.Leave += new System.EventHandler(this.txtSupplierName_Leave);
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(57, 39);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 6;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSupplier_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(73, 38);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(133, 21);
            this.txtSupplierCode.TabIndex = 7;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            this.txtSupplierCode.Leave += new System.EventHandler(this.txtSupplierCode_Leave);
            // 
            // chkAutoCompleationPoNo
            // 
            this.chkAutoCompleationPoNo.AutoSize = true;
            this.chkAutoCompleationPoNo.Checked = true;
            this.chkAutoCompleationPoNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPoNo.Location = new System.Drawing.Point(57, 17);
            this.chkAutoCompleationPoNo.Name = "chkAutoCompleationPoNo";
            this.chkAutoCompleationPoNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPoNo.TabIndex = 0;
            this.chkAutoCompleationPoNo.Tag = "1";
            this.chkAutoCompleationPoNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPoNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPoNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(73, 13);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(133, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblPurchaseOrderNo
            // 
            this.lblPurchaseOrderNo.AutoSize = true;
            this.lblPurchaseOrderNo.Location = new System.Drawing.Point(3, 17);
            this.lblPurchaseOrderNo.Name = "lblPurchaseOrderNo";
            this.lblPurchaseOrderNo.Size = new System.Drawing.Size(42, 13);
            this.lblPurchaseOrderNo.TabIndex = 1;
            this.lblPurchaseOrderNo.Text = "PO No";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label3);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(840, 310);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(300, 180);
            this.grpFooter.TabIndex = 40;
            this.grpFooter.TabStop = false;
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtOtherCharges.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharges.Location = new System.Drawing.Point(159, 108);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(135, 21);
            this.txtOtherCharges.TabIndex = 83;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(158, 36);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(136, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 82;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(23, 111);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 81;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(2, 84);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(18, 21);
            this.btnTaxBreakdown.TabIndex = 79;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(136, 87);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 78;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(109, 40);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 75;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(22, 135);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 74;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(22, 87);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 73;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(137, 40);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 72;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            this.chkSubTotalDiscountPercentage.CheckedChanged += new System.EventHandler(this.chkSubTotalDiscountPercentage_CheckedChanged);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(22, 40);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 70;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(22, 15);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 69;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount.Location = new System.Drawing.Point(159, 132);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(135, 21);
            this.txtNetAmount.TabIndex = 68;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(159, 84);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(135, 21);
            this.txtTotalTaxAmount.TabIndex = 67;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalTaxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalTaxAmount_KeyUp);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(159, 60);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(135, 21);
            this.txtSubTotalDiscount.TabIndex = 66;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.Location = new System.Drawing.Point(159, 12);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(135, 21);
            this.txtGrossAmount.TabIndex = 65;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpFooter1
            // 
            this.grpFooter1.Controls.Add(this.tbFooter);
            this.grpFooter1.Location = new System.Drawing.Point(1, 310);
            this.grpFooter1.Name = "grpFooter1";
            this.grpFooter1.Size = new System.Drawing.Size(493, 181);
            this.grpFooter1.TabIndex = 41;
            this.grpFooter1.TabStop = false;
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpStockDetails);
            this.tbFooter.Controls.Add(this.tbpSupplierInfo);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Location = new System.Drawing.Point(8, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(482, 147);
            this.tbFooter.TabIndex = 40;
            // 
            // tbpStockDetails
            // 
            this.tbpStockDetails.Controls.Add(this.chkViewStokDetails);
            this.tbpStockDetails.Controls.Add(this.dgvStockDetails);
            this.tbpStockDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpStockDetails.Name = "tbpStockDetails";
            this.tbpStockDetails.Size = new System.Drawing.Size(474, 121);
            this.tbpStockDetails.TabIndex = 4;
            this.tbpStockDetails.Text = "Stock Details";
            this.tbpStockDetails.UseVisualStyleBackColor = true;
            // 
            // chkViewStokDetails
            // 
            this.chkViewStokDetails.AutoSize = true;
            this.chkViewStokDetails.Location = new System.Drawing.Point(3, 2);
            this.chkViewStokDetails.Name = "chkViewStokDetails";
            this.chkViewStokDetails.Size = new System.Drawing.Size(89, 17);
            this.chkViewStokDetails.TabIndex = 88;
            this.chkViewStokDetails.Text = "View Stock";
            this.chkViewStokDetails.UseVisualStyleBackColor = true;
            this.chkViewStokDetails.CheckedChanged += new System.EventHandler(this.chkViewStokDetails_CheckedChanged);
            // 
            // dgvStockDetails
            // 
            this.dgvStockDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Location,
            this.ReOrderQty,
            this.ReOrderLevel,
            this.CurrentStock});
            this.dgvStockDetails.Location = new System.Drawing.Point(2, 20);
            this.dgvStockDetails.Name = "dgvStockDetails";
            this.dgvStockDetails.Size = new System.Drawing.Size(470, 103);
            this.dgvStockDetails.TabIndex = 1;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "LocationName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 192;
            // 
            // ReOrderQty
            // 
            this.ReOrderQty.DataPropertyName = "ReOrderQuantity";
            this.ReOrderQty.HeaderText = "Reorder Qty";
            this.ReOrderQty.Name = "ReOrderQty";
            this.ReOrderQty.ReadOnly = true;
            this.ReOrderQty.Width = 70;
            // 
            // ReOrderLevel
            // 
            this.ReOrderLevel.DataPropertyName = "ReOrderLevel";
            this.ReOrderLevel.HeaderText = "Reorder Level";
            this.ReOrderLevel.Name = "ReOrderLevel";
            this.ReOrderLevel.ReadOnly = true;
            this.ReOrderLevel.Width = 70;
            // 
            // CurrentStock
            // 
            this.CurrentStock.DataPropertyName = "Stock";
            this.CurrentStock.HeaderText = "Current Stock";
            this.CurrentStock.Name = "CurrentStock";
            this.CurrentStock.ReadOnly = true;
            this.CurrentStock.Width = 70;
            // 
            // tbpSupplierInfo
            // 
            this.tbpSupplierInfo.Controls.Add(this.lblTotalDueAmount);
            this.tbpSupplierInfo.Controls.Add(this.txtTotalDueAmount);
            this.tbpSupplierInfo.Location = new System.Drawing.Point(4, 22);
            this.tbpSupplierInfo.Name = "tbpSupplierInfo";
            this.tbpSupplierInfo.Size = new System.Drawing.Size(474, 121);
            this.tbpSupplierInfo.TabIndex = 3;
            this.tbpSupplierInfo.Text = "Supplier";
            this.tbpSupplierInfo.UseVisualStyleBackColor = true;
            // 
            // lblTotalDueAmount
            // 
            this.lblTotalDueAmount.AutoSize = true;
            this.lblTotalDueAmount.Location = new System.Drawing.Point(14, 12);
            this.lblTotalDueAmount.Name = "lblTotalDueAmount";
            this.lblTotalDueAmount.Size = new System.Drawing.Size(109, 13);
            this.lblTotalDueAmount.TabIndex = 54;
            this.lblTotalDueAmount.Text = "Total Due Amount";
            // 
            // txtTotalDueAmount
            // 
            this.txtTotalDueAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalDueAmount.Location = new System.Drawing.Point(140, 9);
            this.txtTotalDueAmount.Name = "txtTotalDueAmount";
            this.txtTotalDueAmount.Size = new System.Drawing.Size(123, 21);
            this.txtTotalDueAmount.TabIndex = 53;
            this.txtTotalDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.tbpPageSetup.Size = new System.Drawing.Size(474, 121);
            this.tbpPageSetup.TabIndex = 2;
            this.tbpPageSetup.Text = "Page Setup";
            this.tbpPageSetup.UseVisualStyleBackColor = true;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Location = new System.Drawing.Point(245, 61);
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
            this.rdoPortrait.Location = new System.Drawing.Point(125, 61);
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
            this.cmbPaperSize.Location = new System.Drawing.Point(125, 34);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(266, 21);
            this.cmbPaperSize.TabIndex = 49;
            // 
            // lblOrientation
            // 
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new System.Drawing.Point(9, 63);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(70, 13);
            this.lblOrientation.TabIndex = 48;
            this.lblOrientation.Text = "Orientation";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 9);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(266, 21);
            this.cmbPrinter.TabIndex = 47;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(9, 37);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(68, 13);
            this.lblPaperSize.TabIndex = 46;
            this.lblPaperSize.Text = "Paper Size";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(9, 12);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 45;
            this.lblPrinter.Text = "Printer";
            // 
            // grpFooter2
            // 
            this.grpFooter2.Controls.Add(this.txtOrderCircle);
            this.grpFooter2.Controls.Add(this.lblGroup);
            this.grpFooter2.Controls.Add(this.cmbGroup);
            this.grpFooter2.Controls.Add(this.lblLocationStatus);
            this.grpFooter2.Controls.Add(this.cmbLocationStatus);
            this.grpFooter2.Controls.Add(this.lblOrderCircle);
            this.grpFooter2.Controls.Add(this.btnLoadData);
            this.grpFooter2.Controls.Add(this.lblSelectionCriteria);
            this.grpFooter2.Controls.Add(this.cmbSelectionCriteria);
            this.grpFooter2.Controls.Add(this.tbMore);
            this.grpFooter2.Location = new System.Drawing.Point(497, 310);
            this.grpFooter2.Name = "grpFooter2";
            this.grpFooter2.Size = new System.Drawing.Size(340, 180);
            this.grpFooter2.TabIndex = 42;
            this.grpFooter2.TabStop = false;
            // 
            // txtOrderCircle
            // 
            this.txtOrderCircle.IntValue = 0;
            this.txtOrderCircle.Location = new System.Drawing.Point(116, 34);
            this.txtOrderCircle.Name = "txtOrderCircle";
            this.txtOrderCircle.Size = new System.Drawing.Size(35, 21);
            this.txtOrderCircle.TabIndex = 68;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(153, 63);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(42, 13);
            this.lblGroup.TabIndex = 67;
            this.lblGroup.Text = "Group";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(207, 59);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(129, 21);
            this.cmbGroup.TabIndex = 66;
            // 
            // lblLocationStatus
            // 
            this.lblLocationStatus.AutoSize = true;
            this.lblLocationStatus.Location = new System.Drawing.Point(153, 38);
            this.lblLocationStatus.Name = "lblLocationStatus";
            this.lblLocationStatus.Size = new System.Drawing.Size(54, 13);
            this.lblLocationStatus.TabIndex = 65;
            this.lblLocationStatus.Text = "Location";
            // 
            // cmbLocationStatus
            // 
            this.cmbLocationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationStatus.FormattingEnabled = true;
            this.cmbLocationStatus.Items.AddRange(new object[] {
            "Current Location",
            "All Locations",
            "Location Group"});
            this.cmbLocationStatus.Location = new System.Drawing.Point(207, 35);
            this.cmbLocationStatus.Name = "cmbLocationStatus";
            this.cmbLocationStatus.Size = new System.Drawing.Size(129, 21);
            this.cmbLocationStatus.TabIndex = 64;
            // 
            // lblOrderCircle
            // 
            this.lblOrderCircle.AutoSize = true;
            this.lblOrderCircle.Location = new System.Drawing.Point(6, 38);
            this.lblOrderCircle.Name = "lblOrderCircle";
            this.lblOrderCircle.Size = new System.Drawing.Size(77, 13);
            this.lblOrderCircle.TabIndex = 62;
            this.lblOrderCircle.Text = "Order Circle";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(234, 133);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(102, 22);
            this.btnLoadData.TabIndex = 61;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // lblSelectionCriteria
            // 
            this.lblSelectionCriteria.AutoSize = true;
            this.lblSelectionCriteria.Location = new System.Drawing.Point(6, 16);
            this.lblSelectionCriteria.Name = "lblSelectionCriteria";
            this.lblSelectionCriteria.Size = new System.Drawing.Size(106, 13);
            this.lblSelectionCriteria.TabIndex = 59;
            this.lblSelectionCriteria.Text = "Selection Criteria";
            // 
            // cmbSelectionCriteria
            // 
            this.cmbSelectionCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionCriteria.FormattingEnabled = true;
            this.cmbSelectionCriteria.Items.AddRange(new object[] {
            "Manual",
            "From Supplier",
            "From Reorder Level",
            "Base On Average Sales"});
            this.cmbSelectionCriteria.Location = new System.Drawing.Point(116, 11);
            this.cmbSelectionCriteria.Name = "cmbSelectionCriteria";
            this.cmbSelectionCriteria.Size = new System.Drawing.Size(220, 21);
            this.cmbSelectionCriteria.TabIndex = 58;
            this.cmbSelectionCriteria.SelectedIndexChanged += new System.EventHandler(this.cmbSelectionCriteria_SelectedIndexChanged);
            // 
            // tbMore
            // 
            this.tbMore.Controls.Add(this.tbpTransfer);
            this.tbMore.Controls.Add(this.tbpPurchase);
            this.tbMore.Location = new System.Drawing.Point(8, 64);
            this.tbMore.Name = "tbMore";
            this.tbMore.SelectedIndex = 0;
            this.tbMore.Size = new System.Drawing.Size(220, 89);
            this.tbMore.TabIndex = 69;
            // 
            // tbpTransfer
            // 
            this.tbpTransfer.Controls.Add(this.lblTransferToDate);
            this.tbpTransfer.Controls.Add(this.lblTransferFromDate);
            this.tbpTransfer.Controls.Add(this.dtpTransferToDate);
            this.tbpTransfer.Controls.Add(this.dtpTransferFromDate);
            this.tbpTransfer.Location = new System.Drawing.Point(4, 22);
            this.tbpTransfer.Name = "tbpTransfer";
            this.tbpTransfer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTransfer.Size = new System.Drawing.Size(212, 63);
            this.tbpTransfer.TabIndex = 0;
            this.tbpTransfer.Text = "Transfer";
            this.tbpTransfer.UseVisualStyleBackColor = true;
            // 
            // lblTransferToDate
            // 
            this.lblTransferToDate.AutoSize = true;
            this.lblTransferToDate.Location = new System.Drawing.Point(9, 31);
            this.lblTransferToDate.Name = "lblTransferToDate";
            this.lblTransferToDate.Size = new System.Drawing.Size(51, 13);
            this.lblTransferToDate.TabIndex = 26;
            this.lblTransferToDate.Text = "To Date";
            // 
            // lblTransferFromDate
            // 
            this.lblTransferFromDate.AutoSize = true;
            this.lblTransferFromDate.Location = new System.Drawing.Point(9, 8);
            this.lblTransferFromDate.Name = "lblTransferFromDate";
            this.lblTransferFromDate.Size = new System.Drawing.Size(67, 13);
            this.lblTransferFromDate.TabIndex = 25;
            this.lblTransferFromDate.Text = "From Date";
            // 
            // dtpTransferToDate
            // 
            this.dtpTransferToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransferToDate.Location = new System.Drawing.Point(81, 29);
            this.dtpTransferToDate.Name = "dtpTransferToDate";
            this.dtpTransferToDate.Size = new System.Drawing.Size(123, 21);
            this.dtpTransferToDate.TabIndex = 24;
            // 
            // dtpTransferFromDate
            // 
            this.dtpTransferFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransferFromDate.Location = new System.Drawing.Point(81, 6);
            this.dtpTransferFromDate.Name = "dtpTransferFromDate";
            this.dtpTransferFromDate.Size = new System.Drawing.Size(123, 21);
            this.dtpTransferFromDate.TabIndex = 23;
            // 
            // tbpPurchase
            // 
            this.tbpPurchase.Controls.Add(this.dtpPurchaseToDate);
            this.tbpPurchase.Controls.Add(this.dtpPurchaseFromDate);
            this.tbpPurchase.Controls.Add(this.lblPurToDate);
            this.tbpPurchase.Controls.Add(this.lblPurFromDate);
            this.tbpPurchase.Location = new System.Drawing.Point(4, 22);
            this.tbpPurchase.Name = "tbpPurchase";
            this.tbpPurchase.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPurchase.Size = new System.Drawing.Size(212, 63);
            this.tbpPurchase.TabIndex = 1;
            this.tbpPurchase.Text = "Purchase";
            this.tbpPurchase.UseVisualStyleBackColor = true;
            // 
            // dtpPurchaseToDate
            // 
            this.dtpPurchaseToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseToDate.Location = new System.Drawing.Point(81, 29);
            this.dtpPurchaseToDate.Name = "dtpPurchaseToDate";
            this.dtpPurchaseToDate.Size = new System.Drawing.Size(123, 21);
            this.dtpPurchaseToDate.TabIndex = 32;
            // 
            // dtpPurchaseFromDate
            // 
            this.dtpPurchaseFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseFromDate.Location = new System.Drawing.Point(81, 6);
            this.dtpPurchaseFromDate.Name = "dtpPurchaseFromDate";
            this.dtpPurchaseFromDate.Size = new System.Drawing.Size(123, 21);
            this.dtpPurchaseFromDate.TabIndex = 31;
            // 
            // lblPurToDate
            // 
            this.lblPurToDate.AutoSize = true;
            this.lblPurToDate.Location = new System.Drawing.Point(9, 31);
            this.lblPurToDate.Name = "lblPurToDate";
            this.lblPurToDate.Size = new System.Drawing.Size(51, 13);
            this.lblPurToDate.TabIndex = 30;
            this.lblPurToDate.Text = "To Date";
            // 
            // lblPurFromDate
            // 
            this.lblPurFromDate.AutoSize = true;
            this.lblPurFromDate.Location = new System.Drawing.Point(9, 8);
            this.lblPurFromDate.Name = "lblPurFromDate";
            this.lblPurFromDate.Size = new System.Drawing.Size(67, 13);
            this.lblPurFromDate.TabIndex = 29;
            this.lblPurFromDate.Text = "From Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 158);
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
            this.txtTotalQty.Location = new System.Drawing.Point(159, 155);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(135, 21);
            this.txtTotalQty.TabIndex = 96;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmLogisticPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1143, 532);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter1);
            this.Controls.Add(this.grpFooter2);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmLogisticPurchaseOrder";
            this.Text = "Logistic Purchase Order";
            this.Activated += new System.EventHandler(this.FrmLogisticPurchaseOrder_Activated);
            this.Deactivate += new System.EventHandler(this.FrmLogisticPurchaseOrder_Deactivate);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpFooter2, 0);
            this.Controls.SetChildIndex(this.grpFooter1, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.grpFooter1.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tbpStockDetails.ResumeLayout(false);
            this.tbpStockDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).EndInit();
            this.tbpSupplierInfo.ResumeLayout(false);
            this.tbpSupplierInfo.PerformLayout();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.grpFooter2.ResumeLayout(false);
            this.grpFooter2.PerformLayout();
            this.tbMore.ResumeLayout(false);
            this.tbpTransfer.ResumeLayout(false);
            this.tbpTransfer.PerformLayout();
            this.tbpPurchase.ResumeLayout(false);
            this.tbpPurchase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Button btnPurchaseRequestDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationPurchaseRequestNo;
        private System.Windows.Forms.TextBox txtPurchaseRequestNo;
        private System.Windows.Forms.Label lblPurchaseRequestNo;
        private System.Windows.Forms.Button btnPoDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationPoNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private System.Windows.Forms.TextBox txtSubTotalDiscount;
        private System.Windows.Forms.TextBox txtGrossAmount;
        private System.Windows.Forms.GroupBox grpFooter1;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tbpStockDetails;
        private System.Windows.Forms.DataGridView dgvStockDetails;
        private System.Windows.Forms.TabPage tbpSupplierInfo;
        private System.Windows.Forms.Label lblTotalDueAmount;
        private System.Windows.Forms.TextBox txtTotalDueAmount;
        private System.Windows.Forms.TabPage tbpPageSetup;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label lblOtherCharges;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasure;
        private System.Windows.Forms.GroupBox grpFooter2;
        private CustomControls.TextBoxInteger txtOrderCircle;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblLocationStatus;
        private System.Windows.Forms.ComboBox cmbLocationStatus;
        private System.Windows.Forms.Label lblOrderCircle;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label lblSelectionCriteria;
        private System.Windows.Forms.ComboBox cmbSelectionCriteria;
        private System.Windows.Forms.CheckBox chkTStatus;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private CustomControls.TextBoxCurrency txtOtherCharges;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtFreeQty;
        private CustomControls.TextBoxPercentGen txtProductDiscountPer;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductAmount;
        private System.Windows.Forms.TextBox txtProductDiscountAmount;
        private System.Windows.Forms.TextBox txtPackSize;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblExpectedDate;
        private System.Windows.Forms.DateTimePicker dtpExpectedDate;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.DateTimePicker dtpPODate;
        private System.Windows.Forms.Label lblPoDate;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.CheckBox chkConsignmentBasis;
        private CustomControls.TextBoxInteger txtValidityPeriod;
        private System.Windows.Forms.Button btnQuotationDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationQuotation;
        private System.Windows.Forms.TextBox txtQuotationNo;
        private System.Windows.Forms.Label lblQuotationNo;
        private System.Windows.Forms.Label lblValidityPeriod;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReOrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReOrderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentStock;
        private System.Windows.Forms.TabControl tbMore;
        private System.Windows.Forms.TabPage tbpTransfer;
        private System.Windows.Forms.Label lblTransferToDate;
        private System.Windows.Forms.Label lblTransferFromDate;
        private System.Windows.Forms.DateTimePicker dtpTransferToDate;
        private System.Windows.Forms.DateTimePicker dtpTransferFromDate;
        private System.Windows.Forms.TabPage tbpPurchase;
        private System.Windows.Forms.DateTimePicker dtpPurchaseToDate;
        private System.Windows.Forms.DateTimePicker dtpPurchaseFromDate;
        private System.Windows.Forms.Label lblPurToDate;
        private System.Windows.Forms.Label lblPurFromDate;
        private CustomControls.TextBoxCurrency txtRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Free;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private System.Windows.Forms.CheckBox chkViewStokDetails;
        private System.Windows.Forms.DateTimePicker dtpPaymentExpectedDate;
        private System.Windows.Forms.Label lblPaymentExpectedDate;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxQty txtTotalQty;
    }
}