namespace UI.Windows
{
    partial class FrmBarcode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbTransaction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoCompleationReferenceDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtReferenceDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.LineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpReferenceDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.txtTotQty = new System.Windows.Forms.TextBox();
            this.grpLeftFooter = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tbpTagSetup = new System.Windows.Forms.TabPage();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.cmbTag = new System.Windows.Forms.ComboBox();
            this.lblTag = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.grpLeftFooter.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpTagSetup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(805, 485);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 485);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(167, 40);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(235, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(14, 43);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 64;
            this.lblLocation.Text = "Location";
            // 
            // cmbTransaction
            // 
            this.cmbTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransaction.FormattingEnabled = true;
            this.cmbTransaction.Items.AddRange(new object[] {
            "Purchase Order",
            "Good Received Note",
            "Price Change",
            "Temp Location - Reduce",
            "Stock Adjustment - Add",
            "Manualy Enter Item"});
            this.cmbTransaction.Location = new System.Drawing.Point(167, 67);
            this.cmbTransaction.Name = "cmbTransaction";
            this.cmbTransaction.Size = new System.Drawing.Size(235, 21);
            this.cmbTransaction.TabIndex = 67;
            this.cmbTransaction.SelectedIndexChanged += new System.EventHandler(this.cmbTransaction_SelectedIndexChanged);
            this.cmbTransaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransaction_KeyDown);
            this.cmbTransaction.Validated += new System.EventHandler(this.cmbTransaction_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Transaction";
            // 
            // chkAutoCompleationReferenceDocumentNo
            // 
            this.chkAutoCompleationReferenceDocumentNo.AutoSize = true;
            this.chkAutoCompleationReferenceDocumentNo.Checked = true;
            this.chkAutoCompleationReferenceDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationReferenceDocumentNo.Location = new System.Drawing.Point(147, 97);
            this.chkAutoCompleationReferenceDocumentNo.Name = "chkAutoCompleationReferenceDocumentNo";
            this.chkAutoCompleationReferenceDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationReferenceDocumentNo.TabIndex = 70;
            this.chkAutoCompleationReferenceDocumentNo.Tag = "1";
            this.chkAutoCompleationReferenceDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtReferenceDocumentNo
            // 
            this.txtReferenceDocumentNo.Location = new System.Drawing.Point(167, 94);
            this.txtReferenceDocumentNo.Name = "txtReferenceDocumentNo";
            this.txtReferenceDocumentNo.Size = new System.Drawing.Size(235, 21);
            this.txtReferenceDocumentNo.TabIndex = 69;
            this.txtReferenceDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceDocumentNo_KeyDown);
            this.txtReferenceDocumentNo.Validated += new System.EventHandler(this.txtReferenceDocumentNo_Validated);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(14, 97);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(114, 13);
            this.lblDocumentNo.TabIndex = 68;
            this.lblDocumentNo.Text = "Print Document No";
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNo,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.BatchNo,
            this.Expiry,
            this.Stock,
            this.Qty,
            this.CostPrice,
            this.SellingPrice});
            this.dgvItemDetails.Location = new System.Drawing.Point(6, 14);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 15;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1110, 250);
            this.dgvItemDetails.TabIndex = 71;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // LineNo
            // 
            this.LineNo.DataPropertyName = "LineNo";
            this.LineNo.HeaderText = "Row";
            this.LineNo.Name = "LineNo";
            this.LineNo.ReadOnly = true;
            this.LineNo.Width = 35;
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
            this.ProductName.Width = 245;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.Width = 58;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 140;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            this.Expiry.HeaderText = "Expiry Date";
            this.Expiry.Name = "Expiry";
            this.Expiry.Width = 88;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "Stock";
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 65;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 65;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 120;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Width = 120;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.label3);
            this.grpHeader.Controls.Add(this.dtpReferenceDocumentDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.txtReferenceDocumentNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationReferenceDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.cmbTransaction);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1120, 123);
            this.grpHeader.TabIndex = 72;
            this.grpHeader.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(951, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Date";
            // 
            // dtpReferenceDocumentDate
            // 
            this.dtpReferenceDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReferenceDocumentDate.Location = new System.Drawing.Point(996, 94);
            this.dtpReferenceDocumentDate.Name = "dtpReferenceDocumentDate";
            this.dtpReferenceDocumentDate.Size = new System.Drawing.Size(119, 21);
            this.dtpReferenceDocumentDate.TabIndex = 82;
            this.dtpReferenceDocumentDate.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpReferenceDocumentDate.Leave += new System.EventHandler(this.dtpReferenceDocumentDate_Leave);
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(147, 16);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 7;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(167, 13);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(235, 21);
            this.txtDocumentNo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Document No";
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(798, 59);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.dtpExpiry);
            this.grpBody.Controls.Add(this.txtSellingPrice);
            this.grpBody.Controls.Add(this.txtCostPrice);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtStock);
            this.grpBody.Controls.Add(this.txtBatchNo);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Location = new System.Drawing.Point(2, 113);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1120, 300);
            this.grpBody.TabIndex = 73;
            this.grpBody.TabStop = false;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(437, 270);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(73, 21);
            this.cmbUnit.TabIndex = 76;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(646, 270);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(88, 21);
            this.dtpExpiry.TabIndex = 81;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtSellingPrice.Location = new System.Drawing.Point(981, 270);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.ReadOnly = true;
            this.txtSellingPrice.Size = new System.Drawing.Size(135, 21);
            this.txtSellingPrice.TabIndex = 79;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtCostPrice.Location = new System.Drawing.Point(866, 270);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.ReadOnly = true;
            this.txtCostPrice.Size = new System.Drawing.Size(114, 21);
            this.txtCostPrice.TabIndex = 78;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(793, 270);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(72, 21);
            this.txtQty.TabIndex = 77;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtStock.Location = new System.Drawing.Point(735, 270);
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(56, 21);
            this.txtStock.TabIndex = 76;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(512, 270);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(133, 21);
            this.txtBatchNo.TabIndex = 75;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            this.txtBatchNo.Leave += new System.EventHandler(this.txtBatchNo_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(7, 272);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 74;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(187, 270);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(249, 21);
            this.txtProductName.TabIndex = 73;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(26, 270);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(160, 21);
            this.txtProductCode.TabIndex = 72;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(412, 23);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(66, 13);
            this.lblNetAmount.TabIndex = 75;
            this.lblNetAmount.Text = "Total Qty";
            // 
            // txtTotQty
            // 
            this.txtTotQty.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotQty.Location = new System.Drawing.Point(530, 20);
            this.txtTotQty.Name = "txtTotQty";
            this.txtTotQty.ReadOnly = true;
            this.txtTotQty.Size = new System.Drawing.Size(135, 21);
            this.txtTotQty.TabIndex = 74;
            this.txtTotQty.Text = "0.00";
            this.txtTotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpLeftFooter
            // 
            this.grpLeftFooter.Controls.Add(this.tbFooter);
            this.grpLeftFooter.Location = new System.Drawing.Point(2, 408);
            this.grpLeftFooter.Name = "grpLeftFooter";
            this.grpLeftFooter.Size = new System.Drawing.Size(449, 82);
            this.grpLeftFooter.TabIndex = 76;
            this.grpLeftFooter.TabStop = false;
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpTagSetup);
            this.tbFooter.Location = new System.Drawing.Point(6, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(440, 67);
            this.tbFooter.TabIndex = 40;
            // 
            // tbpTagSetup
            // 
            this.tbpTagSetup.Controls.Add(this.cmbPrinter);
            this.tbpTagSetup.Controls.Add(this.lblPrinter);
            this.tbpTagSetup.Controls.Add(this.cmbTag);
            this.tbpTagSetup.Controls.Add(this.lblTag);
            this.tbpTagSetup.Location = new System.Drawing.Point(4, 22);
            this.tbpTagSetup.Name = "tbpTagSetup";
            this.tbpTagSetup.Size = new System.Drawing.Size(432, 41);
            this.tbpTagSetup.TabIndex = 2;
            this.tbpTagSetup.Text = "Tag Setup";
            this.tbpTagSetup.UseVisualStyleBackColor = true;
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 44);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(266, 21);
            this.cmbPrinter.TabIndex = 49;
            this.cmbPrinter.SelectedValueChanged += new System.EventHandler(this.cmbPrinter_SelectedValueChanged);
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(10, 47);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 48;
            this.lblPrinter.Text = "Printer";
            // 
            // cmbTag
            // 
            this.cmbTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTag.FormattingEnabled = true;
            this.cmbTag.Location = new System.Drawing.Point(68, 12);
            this.cmbTag.Name = "cmbTag";
            this.cmbTag.Size = new System.Drawing.Size(358, 21);
            this.cmbTag.TabIndex = 47;
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(9, 15);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(27, 13);
            this.lblTag.TabIndex = 45;
            this.lblTag.Text = "Tag";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTotQty);
            this.groupBox2.Controls.Add(this.lblNetAmount);
            this.groupBox2.Location = new System.Drawing.Point(453, 408);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 82);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            // 
            // FrmBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 533);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpLeftFooter);
            this.Controls.Add(this.btnDocumentDetails);
            this.Name = "FrmBarcode";
            this.Text = "Barcode Printing";
            this.Controls.SetChildIndex(this.btnDocumentDetails, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpLeftFooter, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            this.grpLeftFooter.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tbpTagSetup.ResumeLayout(false);
            this.tbpTagSetup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbTransaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAutoCompleationReferenceDocumentNo;
        private System.Windows.Forms.TextBox txtReferenceDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.TextBox txtTotQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpReferenceDocumentDate;
        private System.Windows.Forms.GroupBox grpLeftFooter;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tbpTagSetup;
        private System.Windows.Forms.ComboBox cmbTag;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPrinter;
    }
}