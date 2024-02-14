namespace UI.Windows
{
    partial class FrmTransferOfGoodsNote
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
            this.btnGetReturnDocuments = new System.Windows.Forms.Button();
            this.btnReturnDetails = new System.Windows.Forms.Button();
            this.ChkBarCodeScan = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.cmbReturnDocument = new System.Windows.Forms.ComboBox();
            this.lblPOSDocuments = new System.Windows.Forms.Label();
            this.chkViewStokDetails = new System.Windows.Forms.CheckBox();
            this.txtGrnNo = new System.Windows.Forms.TextBox();
            this.btnGrnDetails = new System.Windows.Forms.Button();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.dtpTogDate = new System.Windows.Forms.DateTimePicker();
            this.lblReference = new System.Windows.Forms.Label();
            this.chkAutoCompleationGrnNo = new System.Windows.Forms.CheckBox();
            this.lblTogDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblGrnNo = new System.Windows.Forms.Label();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.cmbFromLocation = new System.Windows.Forms.ComboBox();
            this.cmbTransferType = new System.Windows.Forms.ComboBox();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.lblTransferType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.cmbToLocation = new System.Windows.Forms.ComboBox();
            this.lblToLocation = new System.Windows.Forms.Label();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.LblTime = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
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
            this.lblHideCostPrice = new System.Windows.Forms.Label();
            this.PnlRemoteData = new System.Windows.Forms.Panel();
            this.grpDocumentDetails = new System.Windows.Forms.GroupBox();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.dgvDocument = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScannerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScannedPcs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScannedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpScanner = new System.Windows.Forms.GroupBox();
            this.btnLoadDocuments = new System.Windows.Forms.Button();
            this.dgvScanner = new System.Windows.Forms.DataGridView();
            this.ScScannerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScScanerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkScSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpStockDetails = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tabBatchStock = new System.Windows.Forms.TabPage();
            this.dgvBatchStock = new System.Windows.Forms.DataGridView();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tbpRemote = new System.Windows.Forms.TabPage();
            this.btnFromScanner = new System.Windows.Forms.Button();
            this.btnPrevious = new Glass.GlassButton();
            this.txtQty = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.PnlRemoteData.SuspendLayout();
            this.grpDocumentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocument)).BeginInit();
            this.grpScanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanner)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.grpStockDetails.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tabBatchStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).BeginInit();
            this.tbpStockDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).BeginInit();
            this.tbpPageSetup.SuspendLayout();
            this.tbpRemote.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(772, 517);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Controls.Add(this.btnPrevious);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 517);
            this.grpButtonSet.Size = new System.Drawing.Size(238, 46);
            this.grpButtonSet.Controls.SetChildIndex(this.btnHelp, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnView, 0);
            this.grpButtonSet.Controls.SetChildIndex(this.btnPrevious, 0);
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
            this.GrpHeader.Controls.Add(this.btnGetReturnDocuments);
            this.GrpHeader.Controls.Add(this.btnReturnDetails);
            this.GrpHeader.Controls.Add(this.ChkBarCodeScan);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.cmbReturnDocument);
            this.GrpHeader.Controls.Add(this.lblPOSDocuments);
            this.GrpHeader.Controls.Add(this.chkViewStokDetails);
            this.GrpHeader.Controls.Add(this.txtGrnNo);
            this.GrpHeader.Controls.Add(this.btnGrnDetails);
            this.GrpHeader.Controls.Add(this.chkOverwrite);
            this.GrpHeader.Controls.Add(this.txtReference);
            this.GrpHeader.Controls.Add(this.dtpTogDate);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationGrnNo);
            this.GrpHeader.Controls.Add(this.lblTogDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.lblGrnNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.cmbFromLocation);
            this.GrpHeader.Controls.Add(this.cmbTransferType);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.lblTransferType);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.cmbToLocation);
            this.GrpHeader.Controls.Add(this.lblToLocation);
            this.GrpHeader.Controls.Add(this.lblDocumentNo);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1087, 86);
            this.GrpHeader.TabIndex = 12;
            this.GrpHeader.TabStop = false;
            // 
            // btnGetReturnDocuments
            // 
            this.btnGetReturnDocuments.Location = new System.Drawing.Point(616, 34);
            this.btnGetReturnDocuments.Name = "btnGetReturnDocuments";
            this.btnGetReturnDocuments.Size = new System.Drawing.Size(28, 23);
            this.btnGetReturnDocuments.TabIndex = 78;
            this.btnGetReturnDocuments.Text = "...";
            this.btnGetReturnDocuments.UseVisualStyleBackColor = true;
            this.btnGetReturnDocuments.Click += new System.EventHandler(this.btnGetReturnDocuments_Click);
            // 
            // btnReturnDetails
            // 
            this.btnReturnDetails.Enabled = false;
            this.btnReturnDetails.Location = new System.Drawing.Point(616, 58);
            this.btnReturnDetails.Name = "btnReturnDetails";
            this.btnReturnDetails.Size = new System.Drawing.Size(55, 23);
            this.btnReturnDetails.TabIndex = 77;
            this.btnReturnDetails.Text = "Load";
            this.btnReturnDetails.UseVisualStyleBackColor = true;
            this.btnReturnDetails.Visible = false;
            this.btnReturnDetails.Click += new System.EventHandler(this.btnReturnDetails_Click);
            // 
            // ChkBarCodeScan
            // 
            this.ChkBarCodeScan.AutoSize = true;
            this.ChkBarCodeScan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBarCodeScan.Location = new System.Drawing.Point(1007, 62);
            this.ChkBarCodeScan.Name = "ChkBarCodeScan";
            this.ChkBarCodeScan.Size = new System.Drawing.Size(76, 17);
            this.ChkBarCodeScan.TabIndex = 74;
            this.ChkBarCodeScan.Tag = "1";
            this.ChkBarCodeScan.Text = "BarCode";
            this.ChkBarCodeScan.UseVisualStyleBackColor = true;
            this.ChkBarCodeScan.Visible = false;
            this.ChkBarCodeScan.CheckedChanged += new System.EventHandler(this.ChkBarCodeScan_CheckedChanged);
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // cmbReturnDocument
            // 
            this.cmbReturnDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnDocument.Enabled = false;
            this.cmbReturnDocument.FormattingEnabled = true;
            this.cmbReturnDocument.Location = new System.Drawing.Point(463, 59);
            this.cmbReturnDocument.Name = "cmbReturnDocument";
            this.cmbReturnDocument.Size = new System.Drawing.Size(152, 21);
            this.cmbReturnDocument.TabIndex = 75;
            this.cmbReturnDocument.Visible = false;
            // 
            // lblPOSDocuments
            // 
            this.lblPOSDocuments.AutoSize = true;
            this.lblPOSDocuments.Enabled = false;
            this.lblPOSDocuments.Location = new System.Drawing.Point(361, 62);
            this.lblPOSDocuments.Name = "lblPOSDocuments";
            this.lblPOSDocuments.Size = new System.Drawing.Size(99, 13);
            this.lblPOSDocuments.TabIndex = 76;
            this.lblPOSDocuments.Text = "POS Documents";
            this.lblPOSDocuments.Visible = false;
            // 
            // chkViewStokDetails
            // 
            this.chkViewStokDetails.AutoSize = true;
            this.chkViewStokDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewStokDetails.Location = new System.Drawing.Point(737, 62);
            this.chkViewStokDetails.Name = "chkViewStokDetails";
            this.chkViewStokDetails.Size = new System.Drawing.Size(93, 17);
            this.chkViewStokDetails.TabIndex = 43;
            this.chkViewStokDetails.Text = "View Stock ";
            this.chkViewStokDetails.UseVisualStyleBackColor = true;
            this.chkViewStokDetails.Visible = false;
            this.chkViewStokDetails.CheckedChanged += new System.EventHandler(this.chkViewStokDetails_CheckedChanged);
            // 
            // txtGrnNo
            // 
            this.txtGrnNo.Location = new System.Drawing.Point(463, 11);
            this.txtGrnNo.Name = "txtGrnNo";
            this.txtGrnNo.Size = new System.Drawing.Size(152, 21);
            this.txtGrnNo.TabIndex = 71;
            this.txtGrnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrnNo_KeyDown);
            // 
            // btnGrnDetails
            // 
            this.btnGrnDetails.Location = new System.Drawing.Point(616, 10);
            this.btnGrnDetails.Name = "btnGrnDetails";
            this.btnGrnDetails.Size = new System.Drawing.Size(28, 23);
            this.btnGrnDetails.TabIndex = 70;
            this.btnGrnDetails.Text = "...";
            this.btnGrnDetails.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Location = new System.Drawing.Point(883, 62);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(82, 17);
            this.chkOverwrite.TabIndex = 73;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.Visible = false;
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(739, 11);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(180, 21);
            this.txtReference.TabIndex = 10;
            this.txtReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReference_KeyDown);
            // 
            // dtpTogDate
            // 
            this.dtpTogDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTogDate.Location = new System.Drawing.Point(968, 10);
            this.dtpTogDate.Name = "dtpTogDate";
            this.dtpTogDate.Size = new System.Drawing.Size(114, 21);
            this.dtpTogDate.TabIndex = 2;
            this.dtpTogDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpTogDate_KeyDown);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(656, 16);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 13);
            this.lblReference.TabIndex = 18;
            this.lblReference.Text = "Reference";
            // 
            // chkAutoCompleationGrnNo
            // 
            this.chkAutoCompleationGrnNo.AutoSize = true;
            this.chkAutoCompleationGrnNo.Location = new System.Drawing.Point(445, 15);
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
            this.lblTogDate.Location = new System.Drawing.Point(928, 14);
            this.lblTogDate.Name = "lblTogDate";
            this.lblTogDate.Size = new System.Drawing.Size(34, 13);
            this.lblTogDate.TabIndex = 38;
            this.lblTogDate.Text = "Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(108, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(148, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblGrnNo
            // 
            this.lblGrnNo.AutoSize = true;
            this.lblGrnNo.Location = new System.Drawing.Point(361, 15);
            this.lblGrnNo.Name = "lblGrnNo";
            this.lblGrnNo.Size = new System.Drawing.Size(51, 13);
            this.lblGrnNo.TabIndex = 67;
            this.lblGrnNo.Text = "GRN No";
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(257, 10);
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
            this.cmbFromLocation.Location = new System.Drawing.Point(108, 35);
            this.cmbFromLocation.Name = "cmbFromLocation";
            this.cmbFromLocation.Size = new System.Drawing.Size(241, 21);
            this.cmbFromLocation.TabIndex = 65;
            this.cmbFromLocation.SelectedValueChanged += new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);
            this.cmbFromLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromLocation_KeyDown);
            this.cmbFromLocation.Validated += new System.EventHandler(this.cmbFromLocation_Validated);
            // 
            // cmbTransferType
            // 
            this.cmbTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransferType.FormattingEnabled = true;
            this.cmbTransferType.Location = new System.Drawing.Point(463, 35);
            this.cmbTransferType.Name = "cmbTransferType";
            this.cmbTransferType.Size = new System.Drawing.Size(152, 21);
            this.cmbTransferType.TabIndex = 2;
            this.cmbTransferType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransferType_KeyDown);
            this.cmbTransferType.Validated += new System.EventHandler(this.cmbTransferType_Validated);
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(4, 38);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 43;
            this.lblFromLocation.Text = "Location";
            // 
            // lblTransferType
            // 
            this.lblTransferType.AutoSize = true;
            this.lblTransferType.Location = new System.Drawing.Point(361, 38);
            this.lblTransferType.Name = "lblTransferType";
            this.lblTransferType.Size = new System.Drawing.Size(85, 13);
            this.lblTransferType.TabIndex = 40;
            this.lblTransferType.Text = "Transfer Type";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(739, 35);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(344, 21);
            this.txtRemark.TabIndex = 11;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(656, 39);
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
            this.cmbToLocation.Location = new System.Drawing.Point(108, 59);
            this.cmbToLocation.Name = "cmbToLocation";
            this.cmbToLocation.Size = new System.Drawing.Size(241, 21);
            this.cmbToLocation.TabIndex = 7;
            this.cmbToLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToLocation_KeyDown);
            // 
            // lblToLocation
            // 
            this.lblToLocation.AutoSize = true;
            this.lblToLocation.Location = new System.Drawing.Point(4, 62);
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
            // LblTime
            // 
            this.LblTime.Location = new System.Drawing.Point(721, 24);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(87, 16);
            this.LblTime.TabIndex = 45;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.dtpExpiry);
            this.grpBody.Controls.Add(this.txtBatchNo);
            this.grpBody.Controls.Add(this.txtAmount);
            this.grpBody.Controls.Add(this.txtCostPrice);
            this.grpBody.Controls.Add(this.txtSellingPrice);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Location = new System.Drawing.Point(2, 75);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1087, 293);
            this.grpBody.TabIndex = 13;
            this.grpBody.TabStop = false;
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(627, 268);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(90, 21);
            this.dtpExpiry.TabIndex = 60;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(532, 268);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(94, 21);
            this.txtBatchNo.TabIndex = 59;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Location = new System.Drawing.Point(960, 268);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(122, 21);
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
            this.txtCostPrice.Location = new System.Drawing.Point(718, 268);
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
            this.txtSellingPrice.Location = new System.Drawing.Point(805, 268);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(96, 21);
            this.txtSellingPrice.TabIndex = 55;
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(466, 268);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(65, 21);
            this.cmbUnit.TabIndex = 54;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(219, 268);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(246, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
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
            this.Expiry,
            this.CostPrice,
            this.SellingPrice,
            this.Qty,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 12);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 45;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1077, 253);
            this.dgvItemDetails.TabIndex = 45;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            this.RowNo.Frozen = true;
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 40;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.Frozen = true;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 135;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.Frozen = true;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 240;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            this.Unit.Frozen = true;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 60;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.Frozen = true;
            this.BatchNo.HeaderText = "Batch";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.Visible = false;
            this.BatchNo.Width = 5;
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
            this.txtProductCode.Location = new System.Drawing.Point(24, 268);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(194, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // lblHideCostPrice
            // 
            this.lblHideCostPrice.AutoSize = true;
            this.lblHideCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHideCostPrice.Location = new System.Drawing.Point(223, 17);
            this.lblHideCostPrice.Name = "lblHideCostPrice";
            this.lblHideCostPrice.Size = new System.Drawing.Size(75, 13);
            this.lblHideCostPrice.TabIndex = 61;
            this.lblHideCostPrice.Text = "XXXXXX.XX";
            this.lblHideCostPrice.Visible = false;
            // 
            // PnlRemoteData
            // 
            this.PnlRemoteData.BackColor = System.Drawing.SystemColors.GrayText;
            this.PnlRemoteData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlRemoteData.Controls.Add(this.grpDocumentDetails);
            this.PnlRemoteData.Controls.Add(this.grpScanner);
            this.PnlRemoteData.Controls.Add(this.LblTime);
            this.PnlRemoteData.Location = new System.Drawing.Point(42, 106);
            this.PnlRemoteData.Name = "PnlRemoteData";
            this.PnlRemoteData.Size = new System.Drawing.Size(903, 260);
            this.PnlRemoteData.TabIndex = 79;
            this.PnlRemoteData.Visible = false;
            // 
            // grpDocumentDetails
            // 
            this.grpDocumentDetails.Controls.Add(this.btnDiscard);
            this.grpDocumentDetails.Controls.Add(this.btnApply);
            this.grpDocumentDetails.Controls.Add(this.dgvDocument);
            this.grpDocumentDetails.Location = new System.Drawing.Point(302, 2);
            this.grpDocumentDetails.Name = "grpDocumentDetails";
            this.grpDocumentDetails.Size = new System.Drawing.Size(593, 250);
            this.grpDocumentDetails.TabIndex = 47;
            this.grpDocumentDetails.TabStop = false;
            this.grpDocumentDetails.Text = "Document Details";
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(431, 221);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(76, 24);
            this.btnDiscard.TabIndex = 4;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(513, 221);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(76, 24);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgvDocument
            // 
            this.dgvDocument.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocument.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ScannerName,
            this.DocumentNo,
            this.ScannedPcs,
            this.ScannedQty,
            this.dataGridViewCheckBoxColumn1});
            this.dgvDocument.Location = new System.Drawing.Point(12, 19);
            this.dgvDocument.Name = "dgvDocument";
            this.dgvDocument.RowHeadersWidth = 5;
            this.dgvDocument.Size = new System.Drawing.Size(577, 196);
            this.dgvDocument.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ScannerID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // ScannerName
            // 
            this.ScannerName.DataPropertyName = "ScannerName";
            this.ScannerName.HeaderText = "Scanner Description";
            this.ScannerName.Name = "ScannerName";
            this.ScannerName.Width = 150;
            // 
            // DocumentNo
            // 
            this.DocumentNo.DataPropertyName = "FormText";
            this.DocumentNo.HeaderText = "Document No";
            this.DocumentNo.Name = "DocumentNo";
            this.DocumentNo.ReadOnly = true;
            this.DocumentNo.Width = 200;
            // 
            // ScannedPcs
            // 
            this.ScannedPcs.HeaderText = "Pcs";
            this.ScannedPcs.Name = "ScannedPcs";
            this.ScannedPcs.Width = 75;
            // 
            // ScannedQty
            // 
            this.ScannedQty.HeaderText = "Qty";
            this.ScannedQty.Name = "ScannedQty";
            this.ScannedQty.Width = 75;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsSelect";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // grpScanner
            // 
            this.grpScanner.Controls.Add(this.btnLoadDocuments);
            this.grpScanner.Controls.Add(this.dgvScanner);
            this.grpScanner.Location = new System.Drawing.Point(3, 8);
            this.grpScanner.Name = "grpScanner";
            this.grpScanner.Size = new System.Drawing.Size(296, 250);
            this.grpScanner.TabIndex = 46;
            this.grpScanner.TabStop = false;
            this.grpScanner.Text = "Scanner Details";
            // 
            // btnLoadDocuments
            // 
            this.btnLoadDocuments.Location = new System.Drawing.Point(214, 221);
            this.btnLoadDocuments.Name = "btnLoadDocuments";
            this.btnLoadDocuments.Size = new System.Drawing.Size(76, 24);
            this.btnLoadDocuments.TabIndex = 2;
            this.btnLoadDocuments.Text = "Load >>";
            this.btnLoadDocuments.UseVisualStyleBackColor = true;
            this.btnLoadDocuments.Click += new System.EventHandler(this.btnLoadDocuments_Click);
            // 
            // dgvScanner
            // 
            this.dgvScanner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScanner.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ScScannerID,
            this.ScScanerName,
            this.ChkScSelect});
            this.dgvScanner.Location = new System.Drawing.Point(12, 19);
            this.dgvScanner.Name = "dgvScanner";
            this.dgvScanner.RowHeadersWidth = 5;
            this.dgvScanner.Size = new System.Drawing.Size(277, 196);
            this.dgvScanner.TabIndex = 1;
            // 
            // ScScannerID
            // 
            this.ScScannerID.HeaderText = "ScannerID";
            this.ScScannerID.Name = "ScScannerID";
            this.ScScannerID.Visible = false;
            // 
            // ScScanerName
            // 
            this.ScScanerName.DataPropertyName = "ScannerName";
            this.ScScanerName.HeaderText = "Scanner Description";
            this.ScScanerName.Name = "ScScanerName";
            this.ScScanerName.ReadOnly = true;
            this.ScScanerName.Width = 200;
            // 
            // ChkScSelect
            // 
            this.ChkScSelect.DataPropertyName = "IsSelect";
            this.ChkScSelect.HeaderText = "Select";
            this.ChkScSelect.Name = "ChkScSelect";
            this.ChkScSelect.Width = 50;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblHideCostPrice);
            this.grpFooter.Controls.Add(this.PnlRemoteData);
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
            this.label1.Location = new System.Drawing.Point(366, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 40;
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
            this.txtTotalQty.Location = new System.Drawing.Point(454, 11);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(123, 21);
            this.txtTotalQty.TabIndex = 39;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(366, 37);
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
            this.txtTotalAmount.Location = new System.Drawing.Point(454, 34);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(123, 21);
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
            this.tbFooter.Controls.Add(this.tabBatchStock);
            this.tbFooter.Controls.Add(this.tbpStockDetails);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Controls.Add(this.tbpRemote);
            this.tbFooter.Location = new System.Drawing.Point(4, 11);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(495, 144);
            this.tbFooter.TabIndex = 41;
            // 
            // tabBatchStock
            // 
            this.tabBatchStock.Controls.Add(this.dgvBatchStock);
            this.tabBatchStock.Location = new System.Drawing.Point(4, 22);
            this.tabBatchStock.Name = "tabBatchStock";
            this.tabBatchStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabBatchStock.Size = new System.Drawing.Size(487, 118);
            this.tabBatchStock.TabIndex = 5;
            this.tabBatchStock.Text = "Batch Wise Stock";
            this.tabBatchStock.UseVisualStyleBackColor = true;
            // 
            // dgvBatchStock
            // 
            this.dgvBatchStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch,
            this.dataGridViewTextBoxColumn4});
            this.dgvBatchStock.Location = new System.Drawing.Point(2, 2);
            this.dgvBatchStock.Name = "dgvBatchStock";
            this.dgvBatchStock.Size = new System.Drawing.Size(482, 113);
            this.dgvBatchStock.TabIndex = 2;
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
            // tbpStockDetails
            // 
            this.tbpStockDetails.Controls.Add(this.dgvStockDetails);
            this.tbpStockDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpStockDetails.Name = "tbpStockDetails";
            this.tbpStockDetails.Size = new System.Drawing.Size(487, 118);
            this.tbpStockDetails.TabIndex = 4;
            this.tbpStockDetails.Text = "Location Wise Stock";
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
            this.dgvStockDetails.Size = new System.Drawing.Size(482, 113);
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
            // tbpRemote
            // 
            this.tbpRemote.Controls.Add(this.btnFromScanner);
            this.tbpRemote.Location = new System.Drawing.Point(4, 22);
            this.tbpRemote.Name = "tbpRemote";
            this.tbpRemote.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRemote.Size = new System.Drawing.Size(487, 118);
            this.tbpRemote.TabIndex = 6;
            this.tbpRemote.Text = "Remote Console";
            this.tbpRemote.UseVisualStyleBackColor = true;
            // 
            // btnFromScanner
            // 
            this.btnFromScanner.Enabled = false;
            this.btnFromScanner.Location = new System.Drawing.Point(307, 33);
            this.btnFromScanner.Name = "btnFromScanner";
            this.btnFromScanner.Size = new System.Drawing.Size(131, 43);
            this.btnFromScanner.TabIndex = 0;
            this.btnFromScanner.Text = "Load Scanner Details";
            this.btnFromScanner.UseVisualStyleBackColor = true;
            this.btnFromScanner.Click += new System.EventHandler(this.btnFromScanner_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnPrevious.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.Black;
            this.btnPrevious.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevious.Location = new System.Drawing.Point(159, 11);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 32);
            this.btnPrevious.TabIndex = 8;
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // txtQty
            // 
            this.txtQty.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Location = new System.Drawing.Point(903, 268);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(55, 21);
            this.txtQty.TabIndex = 80;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // FrmTransferOfGoodsNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1091, 565);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpStockDetails);
            this.Name = "FrmTransferOfGoodsNote";
            this.Text = "Transfer Of Goods Note";
            this.Load += new System.EventHandler(this.FrmTransferOfGoodsNote_Load);
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
            this.PnlRemoteData.ResumeLayout(false);
            this.grpDocumentDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocument)).EndInit();
            this.grpScanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScanner)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.grpStockDetails.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tabBatchStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).EndInit();
            this.tbpStockDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).EndInit();
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.tbpRemote.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnReturnDetails;
        private System.Windows.Forms.ComboBox cmbReturnDocument;
        private System.Windows.Forms.Label lblPOSDocuments;
        private System.Windows.Forms.Label lblHideCostPrice;
        private System.Windows.Forms.Button btnGetReturnDocuments;
        private System.Windows.Forms.TabPage tabBatchStock;
        private System.Windows.Forms.DataGridView dgvBatchStock;
        protected Glass.GlassButton btnPrevious;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxCurrency txtTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Panel PnlRemoteData;
        private System.Windows.Forms.GroupBox grpScanner;
        private System.Windows.Forms.DataGridView dgvScanner;
        private System.Windows.Forms.GroupBox grpDocumentDetails;
        private System.Windows.Forms.DataGridView dgvDocument;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnLoadDocuments;
        private System.Windows.Forms.TabPage tbpRemote;
        private System.Windows.Forms.Button btnFromScanner;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScannerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScannedPcs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScannedQty;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScScannerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScScanerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkScSelect;
        private System.Windows.Forms.Button btnDiscard;
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
        private CustomControls.TextBoxCurrency txtQty;
    }
}
