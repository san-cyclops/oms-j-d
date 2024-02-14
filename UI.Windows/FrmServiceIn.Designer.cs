namespace UI.Windows
{
    partial class FrmServiceIn
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
            this.chkAutoCompleteEmployee = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleteSupplier = new System.Windows.Forms.CheckBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.txtEmployeeCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblSupplierCode = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationServiceOut = new System.Windows.Forms.CheckBox();
            this.txtServiceOut = new System.Windows.Forms.TextBox();
            this.btnServiceOutDetails = new System.Windows.Forms.Button();
            this.lblServiceOut = new System.Windows.Forms.Label();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.dtpServiceInDate = new System.Windows.Forms.DateTimePicker();
            this.lblInDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.txtRate = new UI.Windows.CustomControls.TextBoxCurrency();
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
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(753, 418);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 418);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.chkAutoCompleteEmployee);
            this.GrpHeader.Controls.Add(this.chkAutoCompleteSupplier);
            this.GrpHeader.Controls.Add(this.txtEmployeeName);
            this.GrpHeader.Controls.Add(this.txtEmployeeCode);
            this.GrpHeader.Controls.Add(this.label3);
            this.GrpHeader.Controls.Add(this.txtSupplierName);
            this.GrpHeader.Controls.Add(this.txtSupplierCode);
            this.GrpHeader.Controls.Add(this.lblSupplierCode);
            this.GrpHeader.Controls.Add(this.cmbLocation);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationServiceOut);
            this.GrpHeader.Controls.Add(this.txtServiceOut);
            this.GrpHeader.Controls.Add(this.btnServiceOutDetails);
            this.GrpHeader.Controls.Add(this.lblServiceOut);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.dtpServiceInDate);
            this.GrpHeader.Controls.Add(this.lblInDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -5);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1067, 106);
            this.GrpHeader.TabIndex = 28;
            this.GrpHeader.TabStop = false;
            // 
            // chkAutoCompleteEmployee
            // 
            this.chkAutoCompleteEmployee.AutoSize = true;
            this.chkAutoCompleteEmployee.Checked = true;
            this.chkAutoCompleteEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleteEmployee.Location = new System.Drawing.Point(113, 60);
            this.chkAutoCompleteEmployee.Name = "chkAutoCompleteEmployee";
            this.chkAutoCompleteEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleteEmployee.TabIndex = 96;
            this.chkAutoCompleteEmployee.Tag = "1";
            this.chkAutoCompleteEmployee.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleteSupplier
            // 
            this.chkAutoCompleteSupplier.AutoSize = true;
            this.chkAutoCompleteSupplier.Checked = true;
            this.chkAutoCompleteSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleteSupplier.Location = new System.Drawing.Point(113, 37);
            this.chkAutoCompleteSupplier.Name = "chkAutoCompleteSupplier";
            this.chkAutoCompleteSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleteSupplier.TabIndex = 97;
            this.chkAutoCompleteSupplier.Tag = "1";
            this.chkAutoCompleteSupplier.UseVisualStyleBackColor = true;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(299, 57);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(352, 21);
            this.txtEmployeeName.TabIndex = 94;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(130, 57);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(168, 21);
            this.txtEmployeeCode.TabIndex = 93;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Employee";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(299, 34);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(352, 21);
            this.txtSupplierName.TabIndex = 90;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(130, 34);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(168, 21);
            this.txtSupplierCode.TabIndex = 89;
            // 
            // lblSupplierCode
            // 
            this.lblSupplierCode.AutoSize = true;
            this.lblSupplierCode.Location = new System.Drawing.Point(10, 37);
            this.lblSupplierCode.Name = "lblSupplierCode";
            this.lblSupplierCode.Size = new System.Drawing.Size(54, 13);
            this.lblSupplierCode.TabIndex = 88;
            this.lblSupplierCode.Text = "Supplier";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(854, 34);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(203, 21);
            this.cmbLocation.TabIndex = 81;
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // chkAutoCompleationServiceOut
            // 
            this.chkAutoCompleationServiceOut.AutoSize = true;
            this.chkAutoCompleationServiceOut.Checked = true;
            this.chkAutoCompleationServiceOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationServiceOut.Location = new System.Drawing.Point(432, 15);
            this.chkAutoCompleationServiceOut.Name = "chkAutoCompleationServiceOut";
            this.chkAutoCompleationServiceOut.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationServiceOut.TabIndex = 80;
            this.chkAutoCompleationServiceOut.Tag = "1";
            this.chkAutoCompleationServiceOut.UseVisualStyleBackColor = true;
            // 
            // txtServiceOut
            // 
            this.txtServiceOut.Location = new System.Drawing.Point(453, 11);
            this.txtServiceOut.Name = "txtServiceOut";
            this.txtServiceOut.Size = new System.Drawing.Size(169, 21);
            this.txtServiceOut.TabIndex = 79;
            this.txtServiceOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtServiceOut_KeyDown);
            this.txtServiceOut.Leave += new System.EventHandler(this.txtServiceOut_Leave);
            // 
            // btnServiceOutDetails
            // 
            this.btnServiceOutDetails.Location = new System.Drawing.Point(623, 10);
            this.btnServiceOutDetails.Name = "btnServiceOutDetails";
            this.btnServiceOutDetails.Size = new System.Drawing.Size(28, 23);
            this.btnServiceOutDetails.TabIndex = 78;
            this.btnServiceOutDetails.Text = "...";
            this.btnServiceOutDetails.UseVisualStyleBackColor = true;
            // 
            // lblServiceOut
            // 
            this.lblServiceOut.AutoSize = true;
            this.lblServiceOut.Location = new System.Drawing.Point(338, 15);
            this.lblServiceOut.Name = "lblServiceOut";
            this.lblServiceOut.Size = new System.Drawing.Size(93, 13);
            this.lblServiceOut.TabIndex = 77;
            this.lblServiceOut.Text = "Service Out No";
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(741, 37);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 43;
            this.lblFromLocation.Text = "Location";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(113, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // dtpServiceInDate
            // 
            this.dtpServiceInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpServiceInDate.Location = new System.Drawing.Point(854, 10);
            this.dtpServiceInDate.Name = "dtpServiceInDate";
            this.dtpServiceInDate.Size = new System.Drawing.Size(204, 21);
            this.dtpServiceInDate.TabIndex = 2;
            // 
            // lblInDate
            // 
            this.lblInDate.AutoSize = true;
            this.lblInDate.Location = new System.Drawing.Point(741, 14);
            this.lblInDate.Name = "lblInDate";
            this.lblInDate.Size = new System.Drawing.Size(97, 13);
            this.lblInDate.TabIndex = 38;
            this.lblInDate.Text = "Service In Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(130, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(168, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(298, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(130, 80);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(521, 21);
            this.txtRemark.TabIndex = 11;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(10, 83);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document No";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.lblTotal);
            this.grpFooter.Location = new System.Drawing.Point(2, 382);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1068, 36);
            this.grpFooter.TabIndex = 30;
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
            this.txtTotalAmount.Location = new System.Drawing.Point(917, 10);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(142, 21);
            this.txtTotalAmount.TabIndex = 62;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalQty.Location = new System.Drawing.Point(854, 10);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(62, 21);
            this.txtTotalQty.TabIndex = 61;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(816, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 36;
            this.lblTotal.Text = "Total";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBalQty);
            this.groupBox1.Controls.Add(this.txtProductAmount);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtBatchNo);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProduct);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Location = new System.Drawing.Point(2, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1068, 293);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // txtBalQty
            // 
            this.txtBalQty.Enabled = false;
            this.txtBalQty.Location = new System.Drawing.Point(749, 267);
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(69, 21);
            this.txtBalQty.TabIndex = 69;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Location = new System.Drawing.Point(917, 267);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(142, 21);
            this.txtProductAmount.TabIndex = 68;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(679, 267);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(69, 21);
            this.txtQty.TabIndex = 67;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(535, 267);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(143, 21);
            this.txtBatchNo.TabIndex = 65;
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(4, 270);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 62;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(21, 267);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(213, 21);
            this.txtProductCode.TabIndex = 61;
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRate.Location = new System.Drawing.Point(819, 267);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(97, 21);
            this.txtRate.TabIndex = 66;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(235, 267);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(236, 21);
            this.txtProductName.TabIndex = 63;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(472, 267);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(62, 21);
            this.cmbUnit.TabIndex = 64;
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
            this.dgvItemDetails.Location = new System.Drawing.Point(4, 11);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1055, 253);
            this.dgvItemDetails.TabIndex = 60;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
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
            this.ProductName.Width = 240;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 65;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BatchNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 140;
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
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle9;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 125;
            // 
            // FrmServiceIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1072, 466);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmServiceIn";
            this.Text = "Service In";
            this.Load += new System.EventHandler(this.FrmServiceIn_Load);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.GrpHeader, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpHeader.ResumeLayout(false);
            this.GrpHeader.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationServiceOut;
        private System.Windows.Forms.TextBox txtServiceOut;
        private System.Windows.Forms.Button btnServiceOutDetails;
        private System.Windows.Forms.Label lblServiceOut;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.DateTimePicker dtpServiceInDate;
        private System.Windows.Forms.Label lblInDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.TextBox txtProductCode;
        private CustomControls.TextBoxCurrency txtRate;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.ComboBox cmbUnit;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private CustomControls.TextBoxQty txtTotalQty;
        private System.Windows.Forms.ComboBox cmbLocation;
        private CustomControls.TextBoxQty txtBalQty;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblSupplierCode;
        private System.Windows.Forms.CheckBox chkAutoCompleteEmployee;
        private System.Windows.Forms.CheckBox chkAutoCompleteSupplier;
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
