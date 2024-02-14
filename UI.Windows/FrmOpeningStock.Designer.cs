namespace UI.Windows
{
    partial class FrmOpeningStock
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
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.ChkBarCodeScan = new System.Windows.Forms.CheckBox();
            this.btnPosDetails = new System.Windows.Forms.Button();
            this.btnLoadPosDocuments = new System.Windows.Forms.Button();
            this.cmbPosDocument = new System.Windows.Forms.ComboBox();
            this.lblPosDocument = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.dtpOpeningStockDate = new System.Windows.Forms.DateTimePicker();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblOpeningDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.LblTime = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHideCostPrice = new System.Windows.Forms.Label();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSellingValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.lblScannedCode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalSellingValue = new System.Windows.Forms.Label();
            this.lblTotalCostValue = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtTotalCostValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtTotalSellingValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(773, 453);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 453);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.ChkBarCodeScan);
            this.GrpHeader.Controls.Add(this.btnPosDetails);
            this.GrpHeader.Controls.Add(this.btnLoadPosDocuments);
            this.GrpHeader.Controls.Add(this.cmbPosDocument);
            this.GrpHeader.Controls.Add(this.lblPosDocument);
            this.GrpHeader.Controls.Add(this.cmbType);
            this.GrpHeader.Controls.Add(this.lblType);
            this.GrpHeader.Controls.Add(this.label2);
            this.GrpHeader.Controls.Add(this.txtNarration);
            this.GrpHeader.Controls.Add(this.chkOverwrite);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.txtReference);
            this.GrpHeader.Controls.Add(this.dtpOpeningStockDate);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.lblOpeningDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.cmbLocation);
            this.GrpHeader.Controls.Add(this.LblTime);
            this.GrpHeader.Controls.Add(this.lblLocation);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -5);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1088, 109);
            this.GrpHeader.TabIndex = 24;
            this.GrpHeader.TabStop = false;
            // 
            // ChkBarCodeScan
            // 
            this.ChkBarCodeScan.AutoSize = true;
            this.ChkBarCodeScan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBarCodeScan.Checked = true;
            this.ChkBarCodeScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBarCodeScan.Location = new System.Drawing.Point(914, 82);
            this.ChkBarCodeScan.Name = "ChkBarCodeScan";
            this.ChkBarCodeScan.Size = new System.Drawing.Size(76, 17);
            this.ChkBarCodeScan.TabIndex = 82;
            this.ChkBarCodeScan.Tag = "0";
            this.ChkBarCodeScan.Text = "BarCode";
            this.ChkBarCodeScan.UseVisualStyleBackColor = true;
            this.ChkBarCodeScan.Visible = false;
            this.ChkBarCodeScan.CheckedChanged += new System.EventHandler(this.ChkBarCodeScan_CheckedChanged);
            // 
            // btnPosDetails
            // 
            this.btnPosDetails.Enabled = false;
            this.btnPosDetails.Location = new System.Drawing.Point(694, 84);
            this.btnPosDetails.Name = "btnPosDetails";
            this.btnPosDetails.Size = new System.Drawing.Size(55, 23);
            this.btnPosDetails.TabIndex = 81;
            this.btnPosDetails.Text = "Load";
            this.btnPosDetails.UseVisualStyleBackColor = true;
            this.btnPosDetails.Visible = false;
            this.btnPosDetails.Click += new System.EventHandler(this.btnPosDetails_Click);
            // 
            // btnLoadPosDocuments
            // 
            this.btnLoadPosDocuments.Location = new System.Drawing.Point(394, 84);
            this.btnLoadPosDocuments.Name = "btnLoadPosDocuments";
            this.btnLoadPosDocuments.Size = new System.Drawing.Size(28, 23);
            this.btnLoadPosDocuments.TabIndex = 80;
            this.btnLoadPosDocuments.Text = "...";
            this.btnLoadPosDocuments.UseVisualStyleBackColor = true;
            this.btnLoadPosDocuments.Click += new System.EventHandler(this.btnLoadPosDocuments_Click);
            // 
            // cmbPosDocument
            // 
            this.cmbPosDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosDocument.Enabled = false;
            this.cmbPosDocument.FormattingEnabled = true;
            this.cmbPosDocument.Location = new System.Drawing.Point(534, 85);
            this.cmbPosDocument.Name = "cmbPosDocument";
            this.cmbPosDocument.Size = new System.Drawing.Size(159, 21);
            this.cmbPosDocument.TabIndex = 79;
            this.cmbPosDocument.Visible = false;
            // 
            // lblPosDocument
            // 
            this.lblPosDocument.AutoSize = true;
            this.lblPosDocument.Location = new System.Drawing.Point(435, 88);
            this.lblPosDocument.Name = "lblPosDocument";
            this.lblPosDocument.Size = new System.Drawing.Size(93, 13);
            this.lblPosDocument.TabIndex = 78;
            this.lblPosDocument.Text = "POS Document";
            this.lblPosDocument.Visible = false;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(128, 35);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(182, 21);
            this.cmbType.TabIndex = 77;
            this.cmbType.SelectedValueChanged += new System.EventHandler(this.cmbType_SelectedValueChanged);
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbType_KeyDown);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(4, 38);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(121, 13);
            this.lblType.TabIndex = 76;
            this.lblType.Text = "Opening Stock Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(749, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Narration";
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(818, 35);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Multiline = true;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNarration.Size = new System.Drawing.Size(260, 37);
            this.txtNarration.TabIndex = 74;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(996, 82);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(82, 17);
            this.chkOverwrite.TabIndex = 73;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(94, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(534, 11);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(159, 21);
            this.txtReference.TabIndex = 10;
            this.txtReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReference_KeyDown);
            // 
            // dtpOpeningStockDate
            // 
            this.dtpOpeningStockDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOpeningStockDate.Location = new System.Drawing.Point(926, 11);
            this.dtpOpeningStockDate.Name = "dtpOpeningStockDate";
            this.dtpOpeningStockDate.Size = new System.Drawing.Size(152, 21);
            this.dtpOpeningStockDate.TabIndex = 2;
            this.dtpOpeningStockDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOpeningStockDate_KeyDown);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(435, 16);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 13);
            this.lblReference.TabIndex = 18;
            this.lblReference.Text = "Reference";
            // 
            // lblOpeningDate
            // 
            this.lblOpeningDate.AutoSize = true;
            this.lblOpeningDate.Location = new System.Drawing.Point(749, 16);
            this.lblOpeningDate.Name = "lblOpeningDate";
            this.lblOpeningDate.Size = new System.Drawing.Size(121, 13);
            this.lblOpeningDate.TabIndex = 38;
            this.lblOpeningDate.Text = "Opening Stock Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(128, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(153, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(282, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(128, 85);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(265, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // LblTime
            // 
            this.LblTime.Location = new System.Drawing.Point(805, 88);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(87, 16);
            this.LblTime.TabIndex = 45;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(6, 86);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 43;
            this.lblLocation.Text = "Location";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(128, 60);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(565, 21);
            this.txtRemark.TabIndex = 11;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(4, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.label3);
            this.grpBody.Controls.Add(this.lblHideCostPrice);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Controls.Add(this.dtpExpiry);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtCostPrice);
            this.grpBody.Controls.Add(this.txtSellingPrice);
            this.grpBody.Controls.Add(this.txtSellingValue);
            this.grpBody.Controls.Add(this.txtCostValue);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Location = new System.Drawing.Point(2, 100);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1088, 278);
            this.grpBody.TabIndex = 25;
            this.grpBody.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Location = new System.Drawing.Point(888, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "XXXXXX.XX";
            // 
            // lblHideCostPrice
            // 
            this.lblHideCostPrice.AutoSize = true;
            this.lblHideCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHideCostPrice.Location = new System.Drawing.Point(736, 257);
            this.lblHideCostPrice.Name = "lblHideCostPrice";
            this.lblHideCostPrice.Size = new System.Drawing.Size(67, 13);
            this.lblHideCostPrice.TabIndex = 63;
            this.lblHideCostPrice.Text = "XXXXX.XX";
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
            this.CostPrice,
            this.SellingPrice,
            this.CostValue,
            this.SellingValue});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 11);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 45;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1079, 239);
            this.dgvItemDetails.TabIndex = 62;
            this.dgvItemDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemDetails_CellContentClick);
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RowNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 130;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 300;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 62;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Expiry.DefaultCellStyle = dataGridViewCellStyle5;
            this.Expiry.HeaderText = "Expiry Date";
            this.Expiry.Name = "Expiry";
            this.Expiry.ReadOnly = true;
            this.Expiry.Width = 90;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle6;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 65;
            // 
            // CostPrice
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 75;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Width = 75;
            // 
            // CostValue
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostValue.DefaultCellStyle = dataGridViewCellStyle9;
            this.CostValue.HeaderText = "Cost Value";
            this.CostValue.Name = "CostValue";
            this.CostValue.ReadOnly = true;
            this.CostValue.Width = 80;
            // 
            // SellingValue
            // 
            this.SellingValue.DataPropertyName = "SellingValue";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingValue.DefaultCellStyle = dataGridViewCellStyle10;
            this.SellingValue.HeaderText = "Selling Value";
            this.SellingValue.Name = "SellingValue";
            this.SellingValue.ReadOnly = true;
            this.SellingValue.Width = 95;
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(583, 253);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(88, 21);
            this.dtpExpiry.TabIndex = 61;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(672, 253);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(58, 21);
            this.txtQty.TabIndex = 59;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(732, 253);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.ReadOnly = true;
            this.txtCostPrice.Size = new System.Drawing.Size(77, 21);
            this.txtCostPrice.TabIndex = 56;
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(810, 253);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.ReadOnly = true;
            this.txtSellingPrice.Size = new System.Drawing.Size(74, 21);
            this.txtSellingPrice.TabIndex = 55;
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSellingValue
            // 
            this.txtSellingValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingValue.Location = new System.Drawing.Point(965, 253);
            this.txtSellingValue.Name = "txtSellingValue";
            this.txtSellingValue.ReadOnly = true;
            this.txtSellingValue.Size = new System.Drawing.Size(118, 21);
            this.txtSellingValue.TabIndex = 58;
            this.txtSellingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellingValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSellingValue_KeyDown);
            // 
            // txtCostValue
            // 
            this.txtCostValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostValue.Location = new System.Drawing.Point(885, 253);
            this.txtCostValue.Name = "txtCostValue";
            this.txtCostValue.ReadOnly = true;
            this.txtCostValue.Size = new System.Drawing.Size(79, 21);
            this.txtCostValue.TabIndex = 57;
            this.txtCostValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(516, 253);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(66, 21);
            this.cmbUnit.TabIndex = 54;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(223, 253);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(291, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 256);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 47;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 253);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(200, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblScannedCode);
            this.grpFooter.Controls.Add(this.label4);
            this.grpFooter.Controls.Add(this.lblTotalSellingValue);
            this.grpFooter.Controls.Add(this.lblTotalCostValue);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalCostValue);
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalSellingValue);
            this.grpFooter.Location = new System.Drawing.Point(2, 373);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1088, 85);
            this.grpFooter.TabIndex = 26;
            this.grpFooter.TabStop = false;
            // 
            // lblScannedCode
            // 
            this.lblScannedCode.AutoSize = true;
            this.lblScannedCode.Location = new System.Drawing.Point(19, 17);
            this.lblScannedCode.Name = "lblScannedCode";
            this.lblScannedCode.Size = new System.Drawing.Size(0, 13);
            this.lblScannedCode.TabIndex = 66;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(966, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "XXXXXXXXXXX.XX";
            // 
            // lblTotalSellingValue
            // 
            this.lblTotalSellingValue.AutoSize = true;
            this.lblTotalSellingValue.Location = new System.Drawing.Point(849, 62);
            this.lblTotalSellingValue.Name = "lblTotalSellingValue";
            this.lblTotalSellingValue.Size = new System.Drawing.Size(111, 13);
            this.lblTotalSellingValue.TabIndex = 62;
            this.lblTotalSellingValue.Text = "Total Selling Value";
            // 
            // lblTotalCostValue
            // 
            this.lblTotalCostValue.AutoSize = true;
            this.lblTotalCostValue.Location = new System.Drawing.Point(849, 38);
            this.lblTotalCostValue.Name = "lblTotalCostValue";
            this.lblTotalCostValue.Size = new System.Drawing.Size(99, 13);
            this.lblTotalCostValue.TabIndex = 61;
            this.lblTotalCostValue.Text = "Total Cost Value";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalQty.Location = new System.Drawing.Point(965, 11);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(118, 21);
            this.txtTotalQty.TabIndex = 60;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalCostValue
            // 
            this.txtTotalCostValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalCostValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalCostValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalCostValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalCostValue.Location = new System.Drawing.Point(965, 35);
            this.txtTotalCostValue.Name = "txtTotalCostValue";
            this.txtTotalCostValue.ReadOnly = true;
            this.txtTotalCostValue.Size = new System.Drawing.Size(118, 21);
            this.txtTotalCostValue.TabIndex = 59;
            this.txtTotalCostValue.Text = "0.00";
            this.txtTotalCostValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(849, 14);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(86, 13);
            this.lblTotalQty.TabIndex = 36;
            this.lblTotalQty.Text = "Total Quantity";
            // 
            // txtTotalSellingValue
            // 
            this.txtTotalSellingValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalSellingValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalSellingValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalSellingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalSellingValue.Location = new System.Drawing.Point(965, 59);
            this.txtTotalSellingValue.Name = "txtTotalSellingValue";
            this.txtTotalSellingValue.ReadOnly = true;
            this.txtTotalSellingValue.Size = new System.Drawing.Size(118, 21);
            this.txtTotalSellingValue.TabIndex = 0;
            this.txtTotalSellingValue.Text = "0.00";
            this.txtTotalSellingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmOpeningStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1092, 501);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmOpeningStock";
            this.Text = "Opening Stock Balance";
            this.Load += new System.EventHandler(this.FrmOpeningStock_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.GrpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpHeader.ResumeLayout(false);
            this.GrpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.DateTimePicker dtpOpeningStockDate;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label lblOpeningDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label LblTime;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBody;
        private CustomControls.TextBoxCurrency txtCostValue;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalQty;
        private CustomControls.TextBoxCurrency txtTotalSellingValue;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private CustomControls.TextBoxCurrency txtSellingValue;
        private CustomControls.TextBoxCurrency txtTotalCostValue;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblTotalSellingValue;
        private System.Windows.Forms.Label lblTotalCostValue;
        private System.Windows.Forms.ComboBox cmbPosDocument;
        private System.Windows.Forms.Label lblPosDocument;
        private System.Windows.Forms.Button btnLoadPosDocuments;
        private System.Windows.Forms.Button btnPosDetails;
        private System.Windows.Forms.CheckBox ChkBarCodeScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHideCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblScannedCode;
    }
}
