namespace UI.Windows
{
    partial class FrmSampleOut
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
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.lblDeliveryPerson = new System.Windows.Forms.Label();
            this.txtIssuedTo = new System.Windows.Forms.TextBox();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.txtDeliveryPerson = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.dtpIssuedDate = new System.Windows.Forms.DateTimePicker();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblIssuedDate = new System.Windows.Forms.Label();
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
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
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
            this.txtRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
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
            this.grpButtonSet2.Location = new System.Drawing.Point(811, 410);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 410);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.cmbType);
            this.GrpHeader.Controls.Add(this.label2);
            this.GrpHeader.Controls.Add(this.chkOverwrite);
            this.GrpHeader.Controls.Add(this.lblDeliveryPerson);
            this.GrpHeader.Controls.Add(this.txtIssuedTo);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.cmbLocation);
            this.GrpHeader.Controls.Add(this.txtDeliveryPerson);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.txtReference);
            this.GrpHeader.Controls.Add(this.dtpIssuedDate);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.lblIssuedDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.lblIssuedTo);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1125, 105);
            this.GrpHeader.TabIndex = 13;
            this.GrpHeader.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(131, 79);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(164, 21);
            this.cmbType.TabIndex = 78;
            this.cmbType.SelectedValueChanged += new System.EventHandler(this.cmbSampleOutMethod_SelectedValueChanged);
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSampleOutMethod_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Sample Out Type";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(538, 83);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(114, 17);
            this.chkOverwrite.TabIndex = 76;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty  ";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkOverwrite_KeyDown);
            // 
            // lblDeliveryPerson
            // 
            this.lblDeliveryPerson.AutoSize = true;
            this.lblDeliveryPerson.Location = new System.Drawing.Point(726, 38);
            this.lblDeliveryPerson.Name = "lblDeliveryPerson";
            this.lblDeliveryPerson.Size = new System.Drawing.Size(98, 13);
            this.lblDeliveryPerson.TabIndex = 75;
            this.lblDeliveryPerson.Text = "Delivery Person";
            // 
            // txtIssuedTo
            // 
            this.txtIssuedTo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtIssuedTo.Location = new System.Drawing.Point(131, 34);
            this.txtIssuedTo.MaxLength = 50;
            this.txtIssuedTo.Name = "txtIssuedTo";
            this.txtIssuedTo.Size = new System.Drawing.Size(523, 21);
            this.txtIssuedTo.TabIndex = 74;
            this.txtIssuedTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIssuedTo_KeyDown);
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(726, 62);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 43;
            this.lblFromLocation.Text = "Location";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(834, 58);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(285, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // txtDeliveryPerson
            // 
            this.txtDeliveryPerson.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDeliveryPerson.Location = new System.Drawing.Point(834, 35);
            this.txtDeliveryPerson.MaxLength = 50;
            this.txtDeliveryPerson.Name = "txtDeliveryPerson";
            this.txtDeliveryPerson.Size = new System.Drawing.Size(285, 21);
            this.txtDeliveryPerson.TabIndex = 73;
            this.txtDeliveryPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryPerson_KeyDown);
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(110, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(354, 11);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(300, 21);
            this.txtReference.TabIndex = 10;
            this.txtReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReference_KeyDown);
            // 
            // dtpIssuedDate
            // 
            this.dtpIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssuedDate.Location = new System.Drawing.Point(834, 10);
            this.dtpIssuedDate.Name = "dtpIssuedDate";
            this.dtpIssuedDate.Size = new System.Drawing.Size(285, 21);
            this.dtpIssuedDate.TabIndex = 2;
            this.dtpIssuedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpIssuedDate_KeyDown);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(287, 14);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 13);
            this.lblReference.TabIndex = 18;
            this.lblReference.Text = "Reference";
            // 
            // lblIssuedDate
            // 
            this.lblIssuedDate.AutoSize = true;
            this.lblIssuedDate.Location = new System.Drawing.Point(726, 16);
            this.lblIssuedDate.Name = "lblIssuedDate";
            this.lblIssuedDate.Size = new System.Drawing.Size(76, 13);
            this.lblIssuedDate.TabIndex = 38;
            this.lblIssuedDate.Text = "Issued Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(131, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(121, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(253, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            this.btnDocumentDetails.Click += new System.EventHandler(this.btnDocumentDetails_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(131, 57);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(523, 21);
            this.txtRemark.TabIndex = 11;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
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
            this.groupBox1.Controls.Add(this.chkAutoCompleationProduct);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Location = new System.Drawing.Point(2, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1125, 293);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // txtBalQty
            // 
            this.txtBalQty.Enabled = false;
            this.txtBalQty.Location = new System.Drawing.Point(793, 268);
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(68, 21);
            this.txtBalQty.TabIndex = 60;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.Location = new System.Drawing.Point(966, 268);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(154, 21);
            this.txtProductAmount.TabIndex = 59;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(719, 268);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(73, 21);
            this.txtQty.TabIndex = 58;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(567, 268);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(151, 21);
            this.txtBatchNo.TabIndex = 55;
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            this.txtBatchNo.Leave += new System.EventHandler(this.txtBatchNo_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 271);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 47;
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
            this.Qty,
            this.BalancedQty,
            this.Rate,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 12);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1115, 253);
            this.dgvItemDetails.TabIndex = 45;
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
            this.ProductCode.Width = 150;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 250;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 80;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BatchNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 150;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle6;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 68;
            // 
            // BalancedQty
            // 
            this.BalancedQty.DataPropertyName = "BalancedQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BalancedQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.BalancedQty.HeaderText = "Bal.Qty";
            this.BalancedQty.Name = "BalancedQty";
            this.BalancedQty.ReadOnly = true;
            this.BalancedQty.Width = 68;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "CostPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle8;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 110;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle9;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 137;
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRate.Location = new System.Drawing.Point(862, 268);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(103, 21);
            this.txtRate.TabIndex = 56;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 268);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(213, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(236, 268);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(239, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(476, 268);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(90, 21);
            this.cmbUnit.TabIndex = 54;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Location = new System.Drawing.Point(2, 380);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1125, 36);
            this.grpFooter.TabIndex = 24;
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
            this.txtTotalAmount.Location = new System.Drawing.Point(982, 11);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(137, 21);
            this.txtTotalAmount.TabIndex = 62;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalQty.Location = new System.Drawing.Point(908, 11);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(73, 21);
            this.txtTotalQty.TabIndex = 61;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(873, 14);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(34, 13);
            this.lblTotalQty.TabIndex = 36;
            this.lblTotalQty.Text = "Total";
            // 
            // FrmSampleOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1130, 458);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmSampleOut";
            this.Text = "Sample Out";
            this.Load += new System.EventHandler(this.FrmSampleOut_Load);
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
        private System.Windows.Forms.Label lblDeliveryPerson;
        private System.Windows.Forms.TextBox txtIssuedTo;
        private System.Windows.Forms.TextBox txtDeliveryPerson;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.DateTimePicker dtpIssuedDate;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label lblIssuedDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblIssuedTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private CustomControls.TextBoxCurrency txtRate;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.TextBox txtProductAmount;
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
        private System.Windows.Forms.TextBox txtBatchNo;
    }
}
