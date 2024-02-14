namespace UI.Windows
{
    partial class FrmLogisticTransferOfGoodsNote
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
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.ChkBarCodeScan = new System.Windows.Forms.CheckBox();
            this.chkViewStokDetails = new System.Windows.Forms.CheckBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtManNo = new System.Windows.Forms.TextBox();
            this.btnManDetails = new System.Windows.Forms.Button();
            this.txtGrnNo = new System.Windows.Forms.TextBox();
            this.btnGrnDetails = new System.Windows.Forms.Button();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.dtpTogDate = new System.Windows.Forms.DateTimePicker();
            this.chkAutoCompleationManNo = new System.Windows.Forms.CheckBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.chkAutoCompleationGrnNo = new System.Windows.Forms.CheckBox();
            this.lblTogDate = new System.Windows.Forms.Label();
            this.lblManNo = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblGrnNo = new System.Windows.Forms.Label();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.cmbFromLocation = new System.Windows.Forms.ComboBox();
            this.cmbTransferType = new System.Windows.Forms.ComboBox();
            this.LblTime = new System.Windows.Forms.Label();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.lblTransferType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.cmbToLocation = new System.Windows.Forms.ComboBox();
            this.lblToLocation = new System.Windows.Forms.Label();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.lblHideCost = new System.Windows.Forms.Label();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
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
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpStockDetails = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tbpBatchStock = new System.Windows.Forms.TabPage();
            this.dgvBatchStock = new System.Windows.Forms.DataGridView();
            this.tbpStockDetails = new System.Windows.Forms.TabPage();
            this.dgvStockDetails = new System.Windows.Forms.DataGridView();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReOrderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpPageSetup = new System.Windows.Forms.TabPage();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LgsProductBatchNoExpiaryDetailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.grpStockDetails.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpBatchStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).BeginInit();
            this.tbpStockDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).BeginInit();
            this.tbpPageSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(772, 518);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 518);
            // 
            // btnPause
            // 
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.ChkBarCodeScan);
            this.GrpHeader.Controls.Add(this.chkViewStokDetails);
            this.GrpHeader.Controls.Add(this.chkOverwrite);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.txtManNo);
            this.GrpHeader.Controls.Add(this.btnManDetails);
            this.GrpHeader.Controls.Add(this.txtGrnNo);
            this.GrpHeader.Controls.Add(this.btnGrnDetails);
            this.GrpHeader.Controls.Add(this.txtReference);
            this.GrpHeader.Controls.Add(this.dtpTogDate);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationManNo);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationGrnNo);
            this.GrpHeader.Controls.Add(this.lblTogDate);
            this.GrpHeader.Controls.Add(this.lblManNo);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.lblGrnNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.cmbFromLocation);
            this.GrpHeader.Controls.Add(this.cmbTransferType);
            this.GrpHeader.Controls.Add(this.LblTime);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.lblTransferType);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.cmbToLocation);
            this.GrpHeader.Controls.Add(this.lblToLocation);
            this.GrpHeader.Controls.Add(this.lblDocumentNo);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1087, 108);
            this.GrpHeader.TabIndex = 12;
            this.GrpHeader.TabStop = false;
            // 
            // ChkBarCodeScan
            // 
            this.ChkBarCodeScan.AutoSize = true;
            this.ChkBarCodeScan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBarCodeScan.Enabled = false;
            this.ChkBarCodeScan.Location = new System.Drawing.Point(1003, 37);
            this.ChkBarCodeScan.Name = "ChkBarCodeScan";
            this.ChkBarCodeScan.Size = new System.Drawing.Size(76, 17);
            this.ChkBarCodeScan.TabIndex = 74;
            this.ChkBarCodeScan.Tag = "1";
            this.ChkBarCodeScan.Text = "BarCode";
            this.ChkBarCodeScan.UseVisualStyleBackColor = true;
            this.ChkBarCodeScan.CheckedChanged += new System.EventHandler(this.ChkBarCodeScan_CheckedChanged);
            // 
            // chkViewStokDetails
            // 
            this.chkViewStokDetails.AutoSize = true;
            this.chkViewStokDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewStokDetails.Checked = true;
            this.chkViewStokDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewStokDetails.Location = new System.Drawing.Point(813, 37);
            this.chkViewStokDetails.Name = "chkViewStokDetails";
            this.chkViewStokDetails.Size = new System.Drawing.Size(89, 17);
            this.chkViewStokDetails.TabIndex = 43;
            this.chkViewStokDetails.Tag = "1";
            this.chkViewStokDetails.Text = "View Stock";
            this.chkViewStokDetails.UseVisualStyleBackColor = true;
            this.chkViewStokDetails.CheckedChanged += new System.EventHandler(this.chkViewStokDetails_CheckedChanged);
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Location = new System.Drawing.Point(911, 37);
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
            // txtManNo
            // 
            this.txtManNo.Location = new System.Drawing.Point(374, 35);
            this.txtManNo.Name = "txtManNo";
            this.txtManNo.Size = new System.Drawing.Size(143, 21);
            this.txtManNo.TabIndex = 71;
            this.txtManNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManNo_KeyDown);
            this.txtManNo.Validated += new System.EventHandler(this.txtManNo_Validated);
            // 
            // btnManDetails
            // 
            this.btnManDetails.Location = new System.Drawing.Point(518, 34);
            this.btnManDetails.Name = "btnManDetails";
            this.btnManDetails.Size = new System.Drawing.Size(28, 23);
            this.btnManDetails.TabIndex = 70;
            this.btnManDetails.Text = "...";
            this.btnManDetails.UseVisualStyleBackColor = true;
            this.btnManDetails.Click += new System.EventHandler(this.btnManDetails_Click);
            // 
            // txtGrnNo
            // 
            this.txtGrnNo.Location = new System.Drawing.Point(374, 12);
            this.txtGrnNo.Name = "txtGrnNo";
            this.txtGrnNo.Size = new System.Drawing.Size(143, 21);
            this.txtGrnNo.TabIndex = 71;
            this.txtGrnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrnNo_KeyDown);
            // 
            // btnGrnDetails
            // 
            this.btnGrnDetails.Location = new System.Drawing.Point(518, 11);
            this.btnGrnDetails.Name = "btnGrnDetails";
            this.btnGrnDetails.Size = new System.Drawing.Size(28, 23);
            this.btnGrnDetails.TabIndex = 70;
            this.btnGrnDetails.Text = "...";
            this.btnGrnDetails.UseVisualStyleBackColor = true;
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(634, 35);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(172, 21);
            this.txtReference.TabIndex = 10;
            this.txtReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReference_KeyDown);
            // 
            // dtpTogDate
            // 
            this.dtpTogDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTogDate.Location = new System.Drawing.Point(634, 11);
            this.dtpTogDate.Name = "dtpTogDate";
            this.dtpTogDate.Size = new System.Drawing.Size(172, 21);
            this.dtpTogDate.TabIndex = 2;
            this.dtpTogDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpTogDate_KeyDown);
            // 
            // chkAutoCompleationManNo
            // 
            this.chkAutoCompleationManNo.AutoSize = true;
            this.chkAutoCompleationManNo.Checked = true;
            this.chkAutoCompleationManNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationManNo.Location = new System.Drawing.Point(353, 38);
            this.chkAutoCompleationManNo.Name = "chkAutoCompleationManNo";
            this.chkAutoCompleationManNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationManNo.TabIndex = 69;
            this.chkAutoCompleationManNo.Tag = "1";
            this.chkAutoCompleationManNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationManNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationManNo_CheckedChanged);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(563, 39);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 13);
            this.lblReference.TabIndex = 18;
            this.lblReference.Text = "Reference";
            // 
            // chkAutoCompleationGrnNo
            // 
            this.chkAutoCompleationGrnNo.AutoSize = true;
            this.chkAutoCompleationGrnNo.Checked = true;
            this.chkAutoCompleationGrnNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGrnNo.Location = new System.Drawing.Point(353, 16);
            this.chkAutoCompleationGrnNo.Name = "chkAutoCompleationGrnNo";
            this.chkAutoCompleationGrnNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGrnNo.TabIndex = 69;
            this.chkAutoCompleationGrnNo.Tag = "1";
            this.chkAutoCompleationGrnNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGrnNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGrnNo_CheckedChanged);
            // 
            // lblTogDate
            // 
            this.lblTogDate.AutoSize = true;
            this.lblTogDate.Location = new System.Drawing.Point(563, 15);
            this.lblTogDate.Name = "lblTogDate";
            this.lblTogDate.Size = new System.Drawing.Size(63, 13);
            this.lblTogDate.TabIndex = 38;
            this.lblTogDate.Text = "TOG Date";
            // 
            // lblManNo
            // 
            this.lblManNo.AutoSize = true;
            this.lblManNo.Location = new System.Drawing.Point(300, 39);
            this.lblManNo.Name = "lblManNo";
            this.lblManNo.Size = new System.Drawing.Size(51, 13);
            this.lblManNo.TabIndex = 67;
            this.lblManNo.Text = "MAN No";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(143, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblGrnNo
            // 
            this.lblGrnNo.AutoSize = true;
            this.lblGrnNo.Location = new System.Drawing.Point(300, 16);
            this.lblGrnNo.Name = "lblGrnNo";
            this.lblGrnNo.Size = new System.Drawing.Size(51, 13);
            this.lblGrnNo.TabIndex = 67;
            this.lblGrnNo.Text = "GRN No";
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(256, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // cmbFromLocation
            // 
            this.cmbFromLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromLocation.FormattingEnabled = true;
            this.cmbFromLocation.Location = new System.Drawing.Point(112, 59);
            this.cmbFromLocation.Name = "cmbFromLocation";
            this.cmbFromLocation.Size = new System.Drawing.Size(434, 21);
            this.cmbFromLocation.TabIndex = 65;
            this.cmbFromLocation.SelectedValueChanged += new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);
            this.cmbFromLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromLocation_KeyDown);
            this.cmbFromLocation.Validated += new System.EventHandler(this.cmbFromLocation_Validated);
            // 
            // cmbTransferType
            // 
            this.cmbTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransferType.FormattingEnabled = true;
            this.cmbTransferType.Location = new System.Drawing.Point(905, 10);
            this.cmbTransferType.Name = "cmbTransferType";
            this.cmbTransferType.Size = new System.Drawing.Size(175, 21);
            this.cmbTransferType.TabIndex = 2;
            this.cmbTransferType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransferType_KeyDown);
            this.cmbTransferType.Validated += new System.EventHandler(this.cmbTransferType_Validated);
            // 
            // LblTime
            // 
            this.LblTime.Location = new System.Drawing.Point(805, 88);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(87, 16);
            this.LblTime.TabIndex = 45;
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(4, 63);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 43;
            this.lblFromLocation.Text = "Location";
            // 
            // lblTransferType
            // 
            this.lblTransferType.AutoSize = true;
            this.lblTransferType.Location = new System.Drawing.Point(812, 13);
            this.lblTransferType.Name = "lblTransferType";
            this.lblTransferType.Size = new System.Drawing.Size(85, 13);
            this.lblTransferType.TabIndex = 40;
            this.lblTransferType.Text = "Transfer Type";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(634, 59);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(446, 21);
            this.txtRemark.TabIndex = 11;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(563, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
            // 
            // cmbToLocation
            // 
            this.cmbToLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbToLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbToLocation.DisplayMember = "LocationDescription";
            this.cmbToLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToLocation.FormattingEnabled = true;
            this.cmbToLocation.Location = new System.Drawing.Point(112, 83);
            this.cmbToLocation.Name = "cmbToLocation";
            this.cmbToLocation.Size = new System.Drawing.Size(434, 21);
            this.cmbToLocation.TabIndex = 7;
            this.cmbToLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToLocation_KeyDown);
            // 
            // lblToLocation
            // 
            this.lblToLocation.AutoSize = true;
            this.lblToLocation.Location = new System.Drawing.Point(4, 86);
            this.lblToLocation.Name = "lblToLocation";
            this.lblToLocation.Size = new System.Drawing.Size(71, 13);
            this.lblToLocation.TabIndex = 13;
            this.lblToLocation.Text = "To Location";
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(4, 15);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.lblHideCost);
            this.grpBody.Controls.Add(this.dtpExpiry);
            this.grpBody.Controls.Add(this.txtBatchNo);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtAmount);
            this.grpBody.Controls.Add(this.txtCostPrice);
            this.grpBody.Controls.Add(this.txtSellingPrice);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Location = new System.Drawing.Point(2, 96);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1087, 274);
            this.grpBody.TabIndex = 13;
            this.grpBody.TabStop = false;
            // 
            // lblHideCost
            // 
            this.lblHideCost.AutoSize = true;
            this.lblHideCost.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHideCost.Location = new System.Drawing.Point(722, 253);
            this.lblHideCost.Name = "lblHideCost";
            this.lblHideCost.Size = new System.Drawing.Size(75, 13);
            this.lblHideCost.TabIndex = 61;
            this.lblHideCost.Text = "XXXXXX.XX";
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(627, 249);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(90, 21);
            this.dtpExpiry.TabIndex = 60;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(532, 249);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(94, 21);
            this.txtBatchNo.TabIndex = 59;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(905, 249);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(59, 21);
            this.txtQty.TabIndex = 58;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Location = new System.Drawing.Point(965, 249);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(117, 21);
            this.txtAmount.TabIndex = 57;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(718, 249);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(86, 21);
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
            this.txtSellingPrice.Location = new System.Drawing.Point(805, 249);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(99, 21);
            this.txtSellingPrice.TabIndex = 55;
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(466, 249);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(65, 21);
            this.cmbUnit.TabIndex = 54;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(219, 249);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(246, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 252);
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
            this.Expiry,
            this.CostPrice,
            this.SellingPrice,
            this.Qty,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 12);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 45;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1077, 234);
            this.dgvItemDetails.TabIndex = 45;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 40;
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
            this.ProductName.Width = 240;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 60;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "Batch";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 95;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            this.Expiry.HeaderText = "Expiry";
            this.Expiry.Name = "Expiry";
            this.Expiry.ReadOnly = true;
            this.Expiry.Width = 90;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle1;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 90;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 58;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle4;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(24, 249);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(194, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label1);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.lblTotalAmount);
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Location = new System.Drawing.Point(508, 364);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(581, 158);
            this.grpFooter.TabIndex = 23;
            this.grpFooter.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Red;
            this.txtTotalQty.Location = new System.Drawing.Point(460, 12);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(117, 21);
            this.txtTotalQty.TabIndex = 37;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(372, 38);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(82, 13);
            this.lblTotalAmount.TabIndex = 36;
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.txtTotalAmount.Location = new System.Drawing.Point(460, 35);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(117, 21);
            this.txtTotalAmount.TabIndex = 0;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpStockDetails
            // 
            this.grpStockDetails.Controls.Add(this.tbFooter);
            this.grpStockDetails.Location = new System.Drawing.Point(2, 364);
            this.grpStockDetails.Name = "grpStockDetails";
            this.grpStockDetails.Size = new System.Drawing.Size(503, 158);
            this.grpStockDetails.TabIndex = 42;
            this.grpStockDetails.TabStop = false;
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpBatchStock);
            this.tbFooter.Controls.Add(this.tbpStockDetails);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Location = new System.Drawing.Point(4, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(495, 144);
            this.tbFooter.TabIndex = 41;
            // 
            // tbpBatchStock
            // 
            this.tbpBatchStock.Controls.Add(this.dgvBatchStock);
            this.tbpBatchStock.Location = new System.Drawing.Point(4, 22);
            this.tbpBatchStock.Name = "tbpBatchStock";
            this.tbpBatchStock.Size = new System.Drawing.Size(487, 118);
            this.tbpBatchStock.TabIndex = 5;
            this.tbpBatchStock.Text = "Batch Wise Stock Details";
            this.tbpBatchStock.UseVisualStyleBackColor = true;
            // 
            // dgvBatchStock
            // 
            this.dgvBatchStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch,
            this.Stock,
            this.LgsProductBatchNoExpiaryDetailID});
            this.dgvBatchStock.Location = new System.Drawing.Point(2, 3);
            this.dgvBatchStock.Name = "dgvBatchStock";
            this.dgvBatchStock.Size = new System.Drawing.Size(482, 112);
            this.dgvBatchStock.TabIndex = 3;
            this.dgvBatchStock.DoubleClick += new System.EventHandler(this.dgvBatchStock_DoubleClick);
            // 
            // tbpStockDetails
            // 
            this.tbpStockDetails.Controls.Add(this.dgvStockDetails);
            this.tbpStockDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpStockDetails.Name = "tbpStockDetails";
            this.tbpStockDetails.Size = new System.Drawing.Size(487, 118);
            this.tbpStockDetails.TabIndex = 4;
            this.tbpStockDetails.Text = "Stock Details";
            this.tbpStockDetails.UseVisualStyleBackColor = true;
            // 
            // dgvStockDetails
            // 
            this.dgvStockDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Location,
            this.ReOrderQty,
            this.ReOrderLevel,
            this.CurrentStock});
            this.dgvStockDetails.Location = new System.Drawing.Point(2, 2);
            this.dgvStockDetails.Name = "dgvStockDetails";
            this.dgvStockDetails.Size = new System.Drawing.Size(482, 121);
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
            this.ReOrderQty.Width = 75;
            // 
            // ReOrderLevel
            // 
            this.ReOrderLevel.DataPropertyName = "ReOrderLevel";
            this.ReOrderLevel.HeaderText = "Reorder Level";
            this.ReOrderLevel.Name = "ReOrderLevel";
            this.ReOrderLevel.ReadOnly = true;
            this.ReOrderLevel.Width = 75;
            // 
            // CurrentStock
            // 
            this.CurrentStock.DataPropertyName = "Stock";
            this.CurrentStock.HeaderText = "Current Stock";
            this.CurrentStock.Name = "CurrentStock";
            this.CurrentStock.ReadOnly = true;
            this.CurrentStock.Width = 75;
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
            this.tbpPageSetup.Size = new System.Drawing.Size(487, 118);
            this.tbpPageSetup.TabIndex = 2;
            this.tbpPageSetup.Text = "Page Setup";
            this.tbpPageSetup.UseVisualStyleBackColor = true;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Location = new System.Drawing.Point(242, 62);
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
            this.rdoPortrait.Location = new System.Drawing.Point(125, 62);
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
            this.cmbPaperSize.Location = new System.Drawing.Point(125, 35);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(266, 21);
            this.cmbPaperSize.TabIndex = 49;
            // 
            // lblOrientation
            // 
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new System.Drawing.Point(9, 64);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(70, 13);
            this.lblOrientation.TabIndex = 48;
            this.lblOrientation.Text = "Orientation";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 10);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(266, 21);
            this.cmbPrinter.TabIndex = 47;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(9, 38);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(68, 13);
            this.lblPaperSize.TabIndex = 46;
            this.lblPaperSize.Text = "Paper Size";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(9, 13);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 45;
            this.lblPrinter.Text = "Printer";
            // 
            // Batch
            // 
            this.Batch.DataPropertyName = "BatchNo";
            this.Batch.HeaderText = "Batch No";
            this.Batch.Name = "Batch";
            this.Batch.ReadOnly = true;
            this.Batch.Width = 192;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "BalanceQty";
            this.Stock.HeaderText = "Current Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 75;
            // 
            // LgsProductBatchNoExpiaryDetailID
            // 
            this.LgsProductBatchNoExpiaryDetailID.DataPropertyName = "LgsProductBatchNoExpiaryDetailID";
            this.LgsProductBatchNoExpiaryDetailID.HeaderText = "LgsProductBatchNoExpiaryDetailID";
            this.LgsProductBatchNoExpiaryDetailID.Name = "LgsProductBatchNoExpiaryDetailID";
            this.LgsProductBatchNoExpiaryDetailID.Visible = false;
            // 
            // FrmLogisticTransferOfGoodsNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1091, 566);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpStockDetails);
            this.Name = "FrmLogisticTransferOfGoodsNote";
            this.Text = "Logistic Transfer Of Goods Note";
            this.Load += new System.EventHandler(this.FrmLogisticTransferOfGoodsNote_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpStockDetails, 0);
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
            this.grpStockDetails.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tbpBatchStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).EndInit();
            this.tbpStockDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).EndInit();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.Label LblTime;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.Label lblTransferType;
        private System.Windows.Forms.ComboBox cmbTransferType;
        private System.Windows.Forms.DateTimePicker dtpTogDate;
        private System.Windows.Forms.Label lblTogDate;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.ComboBox cmbToLocation;
        private System.Windows.Forms.Label lblToLocation;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.ComboBox cmbFromLocation;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtGrnNo;
        private System.Windows.Forms.Button btnGrnDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationGrnNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblGrnNo;
        private System.Windows.Forms.GroupBox grpBody;
        private CustomControls.TextBoxCurrency txtAmount;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.GroupBox grpFooter;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.GroupBox grpStockDetails;
        private System.Windows.Forms.CheckBox chkViewStokDetails;
        private System.Windows.Forms.CheckBox ChkBarCodeScan;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tbpStockDetails;
        private System.Windows.Forms.DataGridView dgvStockDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReOrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReOrderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentStock;
        private System.Windows.Forms.TabPage tbpPageSetup;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.TextBox txtManNo;
        private System.Windows.Forms.Button btnManDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationManNo;
        private System.Windows.Forms.Label lblManNo;
        private System.Windows.Forms.Label lblHideCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.TabPage tbpBatchStock;
        private System.Windows.Forms.DataGridView dgvBatchStock;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxCurrency txtTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn LgsProductBatchNoExpiaryDetailID;
    }
}
