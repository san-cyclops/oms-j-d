namespace UI.Windows
{
    partial class FrmSampleIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationSampleOut = new System.Windows.Forms.CheckBox();
            this.txtSampleOut = new System.Windows.Forms.TextBox();
            this.btnSampleOutDetails = new System.Windows.Forms.Button();
            this.lblSampleOut = new System.Windows.Forms.Label();
            this.lblDeliveryPerson = new System.Windows.Forms.Label();
            this.txtIssuedTo = new System.Windows.Forms.TextBox();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.txtDeliveryPerson = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.dtpInDate = new System.Windows.Forms.DateTimePicker();
            this.lblInDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblIssuedTo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductAmount = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalancedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(809, 410);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 410);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.cmbType);
            this.GrpHeader.Controls.Add(this.label2);
            this.GrpHeader.Controls.Add(this.cmbLocation);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationSampleOut);
            this.GrpHeader.Controls.Add(this.txtSampleOut);
            this.GrpHeader.Controls.Add(this.btnSampleOutDetails);
            this.GrpHeader.Controls.Add(this.lblSampleOut);
            this.GrpHeader.Controls.Add(this.lblDeliveryPerson);
            this.GrpHeader.Controls.Add(this.txtIssuedTo);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.txtDeliveryPerson);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.dtpInDate);
            this.GrpHeader.Controls.Add(this.lblInDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.lblIssuedTo);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1123, 103);
            this.GrpHeader.TabIndex = 25;
            this.GrpHeader.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(128, 80);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(164, 21);
            this.cmbType.TabIndex = 83;
            this.cmbType.SelectedValueChanged += new System.EventHandler(this.cmbSampleOutMethod_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 82;
            this.label2.Text = "Sample In Type";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(773, 58);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(344, 21);
            this.cmbLocation.TabIndex = 81;
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // chkAutoCompleationSampleOut
            // 
            this.chkAutoCompleationSampleOut.AutoSize = true;
            this.chkAutoCompleationSampleOut.Checked = true;
            this.chkAutoCompleationSampleOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSampleOut.Location = new System.Drawing.Point(388, 14);
            this.chkAutoCompleationSampleOut.Name = "chkAutoCompleationSampleOut";
            this.chkAutoCompleationSampleOut.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSampleOut.TabIndex = 80;
            this.chkAutoCompleationSampleOut.Tag = "1";
            this.chkAutoCompleationSampleOut.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSampleOut.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSampleOut_CheckedChanged);
            // 
            // txtSampleOut
            // 
            this.txtSampleOut.Location = new System.Drawing.Point(406, 11);
            this.txtSampleOut.Name = "txtSampleOut";
            this.txtSampleOut.Size = new System.Drawing.Size(192, 21);
            this.txtSampleOut.TabIndex = 79;
            this.txtSampleOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSampleOut_KeyDown);
            this.txtSampleOut.Leave += new System.EventHandler(this.txtSampleOut_Leave);
            // 
            // btnSampleOutDetails
            // 
            this.btnSampleOutDetails.Location = new System.Drawing.Point(600, 10);
            this.btnSampleOutDetails.Name = "btnSampleOutDetails";
            this.btnSampleOutDetails.Size = new System.Drawing.Size(28, 23);
            this.btnSampleOutDetails.TabIndex = 78;
            this.btnSampleOutDetails.Text = "...";
            this.btnSampleOutDetails.UseVisualStyleBackColor = true;
            // 
            // lblSampleOut
            // 
            this.lblSampleOut.AutoSize = true;
            this.lblSampleOut.Location = new System.Drawing.Point(292, 15);
            this.lblSampleOut.Name = "lblSampleOut";
            this.lblSampleOut.Size = new System.Drawing.Size(93, 13);
            this.lblSampleOut.TabIndex = 77;
            this.lblSampleOut.Text = "Sample Out No";
            // 
            // lblDeliveryPerson
            // 
            this.lblDeliveryPerson.AutoSize = true;
            this.lblDeliveryPerson.Location = new System.Drawing.Point(669, 37);
            this.lblDeliveryPerson.Name = "lblDeliveryPerson";
            this.lblDeliveryPerson.Size = new System.Drawing.Size(98, 13);
            this.lblDeliveryPerson.TabIndex = 75;
            this.lblDeliveryPerson.Text = "Delivery Person";
            // 
            // txtIssuedTo
            // 
            this.txtIssuedTo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtIssuedTo.Location = new System.Drawing.Point(128, 34);
            this.txtIssuedTo.MaxLength = 50;
            this.txtIssuedTo.Name = "txtIssuedTo";
            this.txtIssuedTo.Size = new System.Drawing.Size(470, 21);
            this.txtIssuedTo.TabIndex = 74;
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(669, 61);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 43;
            this.lblFromLocation.Text = "Location";
            // 
            // txtDeliveryPerson
            // 
            this.txtDeliveryPerson.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDeliveryPerson.Location = new System.Drawing.Point(773, 34);
            this.txtDeliveryPerson.MaxLength = 50;
            this.txtDeliveryPerson.Name = "txtDeliveryPerson";
            this.txtDeliveryPerson.Size = new System.Drawing.Size(344, 21);
            this.txtDeliveryPerson.TabIndex = 73;
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(111, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // dtpInDate
            // 
            this.dtpInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInDate.Location = new System.Drawing.Point(773, 10);
            this.dtpInDate.Name = "dtpInDate";
            this.dtpInDate.Size = new System.Drawing.Size(343, 21);
            this.dtpInDate.TabIndex = 2;
            this.dtpInDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpInDate_KeyDown);
            // 
            // lblInDate
            // 
            this.lblInDate.AutoSize = true;
            this.lblInDate.Location = new System.Drawing.Point(669, 15);
            this.lblInDate.Name = "lblInDate";
            this.lblInDate.Size = new System.Drawing.Size(50, 13);
            this.lblInDate.TabIndex = 38;
            this.lblInDate.Text = "In Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(128, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(134, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(265, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(128, 57);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(470, 21);
            this.txtRemark.TabIndex = 11;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(4, 60);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
            // 
            // lblIssuedTo
            // 
            this.lblIssuedTo.AutoSize = true;
            this.lblIssuedTo.Location = new System.Drawing.Point(4, 38);
            this.lblIssuedTo.Name = "lblIssuedTo";
            this.lblIssuedTo.Size = new System.Drawing.Size(62, 13);
            this.lblIssuedTo.TabIndex = 13;
            this.lblIssuedTo.Text = "Issued To";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBalQty);
            this.groupBox1.Controls.Add(this.txtProductAmount);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtBatchNo);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProduct);
            this.groupBox1.Location = new System.Drawing.Point(2, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1123, 293);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // txtBalQty
            // 
            this.txtBalQty.Enabled = false;
            this.txtBalQty.Location = new System.Drawing.Point(793, 267);
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalQty.Size = new System.Drawing.Size(66, 21);
            this.txtBalQty.TabIndex = 67;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.Location = new System.Drawing.Point(964, 267);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(156, 21);
            this.txtProductAmount.TabIndex = 66;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(719, 267);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(73, 21);
            this.txtQty.TabIndex = 65;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Enabled = false;
            this.txtBatchNo.Location = new System.Drawing.Point(567, 267);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(151, 21);
            this.txtBatchNo.TabIndex = 63;
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRate.Enabled = false;
            this.txtRate.Location = new System.Drawing.Point(860, 267);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(103, 21);
            this.txtRate.TabIndex = 64;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 267);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(213, 21);
            this.txtProductCode.TabIndex = 60;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(236, 267);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(239, 21);
            this.txtProductName.TabIndex = 61;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(476, 267);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(90, 21);
            this.cmbUnit.TabIndex = 62;
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
            this.Qty,
            this.BalancedQty,
            this.Rate,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 10);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1113, 253);
            this.dgvItemDetails.TabIndex = 59;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RowNo.DefaultCellStyle = dataGridViewCellStyle10;
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle11;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 150;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle12;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 250;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle13;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 80;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BatchNo.DefaultCellStyle = dataGridViewCellStyle14;
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.Width = 150;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle15;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 68;
            // 
            // BalancedQty
            // 
            this.BalancedQty.DataPropertyName = "BalancedQty";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BalancedQty.DefaultCellStyle = dataGridViewCellStyle16;
            this.BalancedQty.HeaderText = "Bal.Qty";
            this.BalancedQty.Name = "BalancedQty";
            this.BalancedQty.ReadOnly = true;
            this.BalancedQty.Width = 68;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "CostPrice";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle17;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 110;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle18;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 137;
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 272);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 47;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Location = new System.Drawing.Point(2, 380);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1123, 36);
            this.grpFooter.TabIndex = 27;
            this.grpFooter.TabStop = false;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalAmount.Location = new System.Drawing.Point(983, 11);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(137, 21);
            this.txtTotalAmount.TabIndex = 65;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalQty.Location = new System.Drawing.Point(909, 11);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(73, 21);
            this.txtTotalQty.TabIndex = 64;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(874, 14);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(34, 13);
            this.lblTotalQty.TabIndex = 63;
            this.lblTotalQty.Text = "Total";
            // 
            // FrmSampleIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1128, 458);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmSampleIn";
            this.Text = "Sample In";
            this.Load += new System.EventHandler(this.FrmSampleIn_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.GrpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpHeader.ResumeLayout(false);
            this.GrpHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationSampleOut;
        private System.Windows.Forms.TextBox txtSampleOut;
        private System.Windows.Forms.Button btnSampleOutDetails;
        private System.Windows.Forms.Label lblSampleOut;
        private System.Windows.Forms.Label lblDeliveryPerson;
        private System.Windows.Forms.TextBox txtIssuedTo;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.TextBox txtDeliveryPerson;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.DateTimePicker dtpInDate;
        private System.Windows.Forms.Label lblInDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblIssuedTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductAmount;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtBatchNo;
        private CustomControls.TextBoxCurrency txtRate;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.ComboBox cmbLocation;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxQty txtBalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalancedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
