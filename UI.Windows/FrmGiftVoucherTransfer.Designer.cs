namespace UI.Windows
{
    partial class FrmGiftVoucherTransfer
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
            this.components = new System.ComponentModel.Container();
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.cmbFromLocation = new System.Windows.Forms.ComboBox();
            this.cmbToLocation = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtGrnNo = new System.Windows.Forms.TextBox();
            this.btnGrnDetails = new System.Windows.Forms.Button();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.dtpTransferDate = new System.Windows.Forms.DateTimePicker();
            this.lblReference = new System.Windows.Forms.Label();
            this.chkAutoCompleationGrnNo = new System.Windows.Forms.CheckBox();
            this.lblTransferDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblGrnNo = new System.Windows.Forms.Label();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblToLocation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtVoucherValue = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtVoucherSerial = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationBookCode = new System.Windows.Forms.CheckBox();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.dgvVoucherBookDetails = new System.Windows.Forms.DataGridView();
            this.lineNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherSerialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invGiftVoucherTransferNoteDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new Glass.GlassButton();
            this.rdoCoupon = new System.Windows.Forms.RadioButton();
            this.rdoVoucher = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationGroup = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationBook = new System.Windows.Forms.CheckBox();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cmbBasedOn = new System.Windows.Forms.ComboBox();
            this.txtGroupCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblSelectionCriteria = new System.Windows.Forms.Label();
            this.txtGroupName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.cmbSelectionCriteria = new System.Windows.Forms.ComboBox();
            this.lblGiftVoucherGroup = new System.Windows.Forms.Label();
            this.lblBook = new System.Windows.Forms.Label();
            this.txtBookCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPercentageOfCoupon = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoOfVouchersOnBook = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtGiftVoucherValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblNoOfVouchersOnBook = new System.Windows.Forms.Label();
            this.lblGiftVoucherValue = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbMore = new System.Windows.Forms.TabControl();
            this.tbpVoucherSerial = new System.Windows.Forms.TabPage();
            this.lblVoucherSerialQty = new System.Windows.Forms.Label();
            this.txtVoucherSerialQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtVoucherSerialTo = new System.Windows.Forms.TextBox();
            this.txtVoucherSerialFrom = new System.Windows.Forms.TextBox();
            this.lblVoucherSerialTo = new System.Windows.Forms.Label();
            this.lblVoucherSerialFrom = new System.Windows.Forms.Label();
            this.tbpVoucherNo = new System.Windows.Forms.TabPage();
            this.lblVoucherNoQty = new System.Windows.Forms.Label();
            this.txtVoucherNoQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtVoucherNoTo = new System.Windows.Forms.TextBox();
            this.txtVoucherNoFrom = new System.Windows.Forms.TextBox();
            this.lblVoucherNoTo = new System.Windows.Forms.Label();
            this.lblVoucherNoFrom = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblGiftVoucherQty = new System.Windows.Forms.Label();
            this.txtGiftVoucherQty = new UI.Windows.CustomControls.TextBoxInteger();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherBookDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherTransferNoteDetailTempBindingSource)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbMore.SuspendLayout();
            this.tbpVoucherSerial.SuspendLayout();
            this.tbpVoucherNo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(552, 524);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 524);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.cmbFromLocation);
            this.GrpHeader.Controls.Add(this.cmbToLocation);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.txtGrnNo);
            this.GrpHeader.Controls.Add(this.btnGrnDetails);
            this.GrpHeader.Controls.Add(this.txtReferenceNo);
            this.GrpHeader.Controls.Add(this.dtpTransferDate);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationGrnNo);
            this.GrpHeader.Controls.Add(this.lblTransferDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.lblGrnNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.lblFromLocation);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.lblToLocation);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(867, 108);
            this.GrpHeader.TabIndex = 24;
            this.GrpHeader.TabStop = false;
            // 
            // cmbFromLocation
            // 
            this.cmbFromLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromLocation.FormattingEnabled = true;
            this.cmbFromLocation.Location = new System.Drawing.Point(112, 36);
            this.cmbFromLocation.Name = "cmbFromLocation";
            this.cmbFromLocation.Size = new System.Drawing.Size(427, 21);
            this.cmbFromLocation.TabIndex = 2;
            this.cmbFromLocation.SelectedValueChanged += new System.EventHandler(this.cmbFromLocation_SelectedValueChanged);
            this.cmbFromLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromLocation_KeyDown);
            this.cmbFromLocation.Validated += new System.EventHandler(this.cmbFromLocation_Validated);
            // 
            // cmbToLocation
            // 
            this.cmbToLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbToLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbToLocation.DisplayMember = "LocationDescription";
            this.cmbToLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToLocation.FormattingEnabled = true;
            this.cmbToLocation.Location = new System.Drawing.Point(112, 60);
            this.cmbToLocation.Name = "cmbToLocation";
            this.cmbToLocation.Size = new System.Drawing.Size(427, 21);
            this.cmbToLocation.TabIndex = 3;
            this.cmbToLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToLocation_KeyDown);
            this.cmbToLocation.Validated += new System.EventHandler(this.cmbToLocation_Validated);
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
            // txtGrnNo
            // 
            this.txtGrnNo.Location = new System.Drawing.Point(374, 12);
            this.txtGrnNo.Name = "txtGrnNo";
            this.txtGrnNo.Size = new System.Drawing.Size(136, 21);
            this.txtGrnNo.TabIndex = 71;
            this.txtGrnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGrnNo_KeyDown);
            // 
            // btnGrnDetails
            // 
            this.btnGrnDetails.Location = new System.Drawing.Point(511, 11);
            this.btnGrnDetails.Name = "btnGrnDetails";
            this.btnGrnDetails.Size = new System.Drawing.Size(28, 23);
            this.btnGrnDetails.TabIndex = 70;
            this.btnGrnDetails.Text = "...";
            this.btnGrnDetails.UseVisualStyleBackColor = true;
            this.btnGrnDetails.Click += new System.EventHandler(this.btnGrnDetails_Click);
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(698, 35);
            this.txtReferenceNo.MaxLength = 20;
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(159, 21);
            this.txtReferenceNo.TabIndex = 6;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            this.txtReferenceNo.Validated += new System.EventHandler(this.txtReferenceNo_Validated);
            // 
            // dtpTransferDate
            // 
            this.dtpTransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTransferDate.Location = new System.Drawing.Point(698, 12);
            this.dtpTransferDate.Name = "dtpTransferDate";
            this.dtpTransferDate.Size = new System.Drawing.Size(159, 21);
            this.dtpTransferDate.TabIndex = 5;
            this.dtpTransferDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpTransferDate_KeyDown);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(600, 38);
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
            // 
            // lblTransferDate
            // 
            this.lblTransferDate.AutoSize = true;
            this.lblTransferDate.Location = new System.Drawing.Point(600, 14);
            this.lblTransferDate.Name = "lblTransferDate";
            this.lblTransferDate.Size = new System.Drawing.Size(85, 13);
            this.lblTransferDate.TabIndex = 38;
            this.lblTransferDate.Text = "Transfer Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            // 
            // lblGrnNo
            // 
            this.lblGrnNo.AutoSize = true;
            this.lblGrnNo.Location = new System.Drawing.Point(278, 16);
            this.lblGrnNo.Name = "lblGrnNo";
            this.lblGrnNo.Size = new System.Drawing.Size(74, 13);
            this.lblGrnNo.TabIndex = 67;
            this.lblGrnNo.Text = "G.V. Grn No";
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(249, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            this.btnDocumentDetails.Click += new System.EventHandler(this.btnDocumentDetails_Click);
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
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 83);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(427, 21);
            this.txtRemark.TabIndex = 4;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(4, 86);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document No";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Location = new System.Drawing.Point(632, 295);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(236, 234);
            this.grpFooter.TabIndex = 26;
            this.grpFooter.TabStop = false;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(9, 16);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(58, 13);
            this.lblTotalQty.TabIndex = 65;
            this.lblTotalQty.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.IntValue = 0;
            this.txtTotalQty.Location = new System.Drawing.Point(76, 13);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Size = new System.Drawing.Size(151, 21);
            this.txtTotalQty.TabIndex = 64;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtVoucherValue);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtVoucherSerial);
            this.grpBody.Controls.Add(this.chkAutoCompleationBookCode);
            this.grpBody.Controls.Add(this.txtVoucherNo);
            this.grpBody.Controls.Add(this.dgvVoucherBookDetails);
            this.grpBody.Location = new System.Drawing.Point(2, 98);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(867, 203);
            this.grpBody.TabIndex = 27;
            this.grpBody.TabStop = false;
            // 
            // txtVoucherValue
            // 
            this.txtVoucherValue.Location = new System.Drawing.Point(591, 224);
            this.txtVoucherValue.Name = "txtVoucherValue";
            this.txtVoucherValue.Size = new System.Drawing.Size(105, 21);
            this.txtVoucherValue.TabIndex = 28;
            this.txtVoucherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVoucherValue.Visible = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(697, 224);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(108, 21);
            this.txtQty.TabIndex = 27;
            this.txtQty.Visible = false;
            // 
            // txtVoucherSerial
            // 
            this.txtVoucherSerial.Location = new System.Drawing.Point(256, 224);
            this.txtVoucherSerial.Name = "txtVoucherSerial";
            this.txtVoucherSerial.Size = new System.Drawing.Size(334, 21);
            this.txtVoucherSerial.TabIndex = 26;
            this.txtVoucherSerial.Visible = false;
            // 
            // chkAutoCompleationBookCode
            // 
            this.chkAutoCompleationBookCode.AutoSize = true;
            this.chkAutoCompleationBookCode.Location = new System.Drawing.Point(10, 227);
            this.chkAutoCompleationBookCode.Name = "chkAutoCompleationBookCode";
            this.chkAutoCompleationBookCode.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBookCode.TabIndex = 24;
            this.chkAutoCompleationBookCode.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBookCode.Visible = false;
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.Location = new System.Drawing.Point(31, 224);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(224, 21);
            this.txtVoucherNo.TabIndex = 25;
            this.txtVoucherNo.Visible = false;
            // 
            // dgvVoucherBookDetails
            // 
            this.dgvVoucherBookDetails.AutoGenerateColumns = false;
            this.dgvVoucherBookDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucherBookDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNoDataGridViewTextBoxColumn,
            this.voucherNoDataGridViewTextBoxColumn,
            this.voucherSerialDataGridViewTextBoxColumn,
            this.VoucherAmount});
            this.dgvVoucherBookDetails.DataSource = this.invGiftVoucherTransferNoteDetailTempBindingSource;
            this.dgvVoucherBookDetails.Location = new System.Drawing.Point(10, 11);
            this.dgvVoucherBookDetails.Name = "dgvVoucherBookDetails";
            this.dgvVoucherBookDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucherBookDetails.Size = new System.Drawing.Size(851, 187);
            this.dgvVoucherBookDetails.TabIndex = 1;
            // 
            // lineNoDataGridViewTextBoxColumn
            // 
            this.lineNoDataGridViewTextBoxColumn.DataPropertyName = "LineNo";
            this.lineNoDataGridViewTextBoxColumn.HeaderText = "Raw";
            this.lineNoDataGridViewTextBoxColumn.Name = "lineNoDataGridViewTextBoxColumn";
            this.lineNoDataGridViewTextBoxColumn.Width = 90;
            // 
            // voucherNoDataGridViewTextBoxColumn
            // 
            this.voucherNoDataGridViewTextBoxColumn.DataPropertyName = "VoucherNo";
            this.voucherNoDataGridViewTextBoxColumn.HeaderText = "Voucher No";
            this.voucherNoDataGridViewTextBoxColumn.Name = "voucherNoDataGridViewTextBoxColumn";
            this.voucherNoDataGridViewTextBoxColumn.Width = 255;
            // 
            // voucherSerialDataGridViewTextBoxColumn
            // 
            this.voucherSerialDataGridViewTextBoxColumn.DataPropertyName = "VoucherSerial";
            this.voucherSerialDataGridViewTextBoxColumn.HeaderText = "Voucher Serial";
            this.voucherSerialDataGridViewTextBoxColumn.Name = "voucherSerialDataGridViewTextBoxColumn";
            this.voucherSerialDataGridViewTextBoxColumn.Width = 255;
            // 
            // VoucherAmount
            // 
            this.VoucherAmount.DataPropertyName = "VoucherValue";
            this.VoucherAmount.HeaderText = "Voucher Value";
            this.VoucherAmount.Name = "VoucherAmount";
            this.VoucherAmount.Width = 150;
            // 
            // invGiftVoucherTransferNoteDetailTempBindingSource
            // 
            this.invGiftVoucherTransferNoteDetailTempBindingSource.DataSource = typeof(Domain.InvGiftVoucherTransferNoteDetailTemp);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLoad);
            this.groupBox4.Controls.Add(this.rdoCoupon);
            this.groupBox4.Controls.Add(this.rdoVoucher);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(2, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(628, 234);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoad.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(543, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 27);
            this.btnLoad.TabIndex = 116;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // rdoCoupon
            // 
            this.rdoCoupon.AutoSize = true;
            this.rdoCoupon.Location = new System.Drawing.Point(256, 14);
            this.rdoCoupon.Name = "rdoCoupon";
            this.rdoCoupon.Size = new System.Drawing.Size(69, 17);
            this.rdoCoupon.TabIndex = 113;
            this.rdoCoupon.Text = "Coupon";
            this.rdoCoupon.UseVisualStyleBackColor = true;
            this.rdoCoupon.CheckedChanged += new System.EventHandler(this.rdoCoupon_CheckedChanged);
            // 
            // rdoVoucher
            // 
            this.rdoVoucher.AutoSize = true;
            this.rdoVoucher.Checked = true;
            this.rdoVoucher.Location = new System.Drawing.Point(155, 14);
            this.rdoVoucher.Name = "rdoVoucher";
            this.rdoVoucher.Size = new System.Drawing.Size(71, 17);
            this.rdoVoucher.TabIndex = 112;
            this.rdoVoucher.TabStop = true;
            this.rdoVoucher.Text = "Voucher";
            this.rdoVoucher.UseVisualStyleBackColor = true;
            this.rdoVoucher.CheckedChanged += new System.EventHandler(this.rdoVoucher_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoCompleationGroup);
            this.groupBox1.Controls.Add(this.chkAutoCompleationBook);
            this.groupBox1.Controls.Add(this.lblBasedOn);
            this.groupBox1.Controls.Add(this.cmbBasedOn);
            this.groupBox1.Controls.Add(this.txtGroupCode);
            this.groupBox1.Controls.Add(this.lblSelectionCriteria);
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.cmbSelectionCriteria);
            this.groupBox1.Controls.Add(this.lblGiftVoucherGroup);
            this.groupBox1.Controls.Add(this.lblBook);
            this.groupBox1.Controls.Add(this.txtBookCode);
            this.groupBox1.Controls.Add(this.txtBookName);
            this.groupBox1.Location = new System.Drawing.Point(3, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 83);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoCompleationGroup
            // 
            this.chkAutoCompleationGroup.AutoSize = true;
            this.chkAutoCompleationGroup.Checked = true;
            this.chkAutoCompleationGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGroup.Location = new System.Drawing.Point(119, 14);
            this.chkAutoCompleationGroup.Name = "chkAutoCompleationGroup";
            this.chkAutoCompleationGroup.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGroup.TabIndex = 131;
            this.chkAutoCompleationGroup.Tag = "1";
            this.chkAutoCompleationGroup.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGroup.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGroup_CheckedChanged);
            // 
            // chkAutoCompleationBook
            // 
            this.chkAutoCompleationBook.AutoSize = true;
            this.chkAutoCompleationBook.Checked = true;
            this.chkAutoCompleationBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBook.Location = new System.Drawing.Point(119, 35);
            this.chkAutoCompleationBook.Name = "chkAutoCompleationBook";
            this.chkAutoCompleationBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBook.TabIndex = 130;
            this.chkAutoCompleationBook.Tag = "1";
            this.chkAutoCompleationBook.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBook.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBook_CheckedChanged);
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(1, 60);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(62, 13);
            this.lblBasedOn.TabIndex = 129;
            this.lblBasedOn.Text = "Based On";
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Items.AddRange(new object[] {
            "Book Serial",
            "Voucher Serial"});
            this.cmbBasedOn.Location = new System.Drawing.Point(139, 58);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(159, 21);
            this.cmbBasedOn.TabIndex = 128;
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.IsAutoComplete = false;
            this.txtGroupCode.ItemCollection = null;
            this.txtGroupCode.Location = new System.Drawing.Point(140, 11);
            this.txtGroupCode.MasterCode = "";
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(125, 21);
            this.txtGroupCode.TabIndex = 9;
            this.txtGroupCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupCode_KeyDown);
            this.txtGroupCode.Validated += new System.EventHandler(this.txtGroupCode_Validated);
            // 
            // lblSelectionCriteria
            // 
            this.lblSelectionCriteria.AutoSize = true;
            this.lblSelectionCriteria.Location = new System.Drawing.Point(300, 63);
            this.lblSelectionCriteria.Name = "lblSelectionCriteria";
            this.lblSelectionCriteria.Size = new System.Drawing.Size(106, 13);
            this.lblSelectionCriteria.TabIndex = 59;
            this.lblSelectionCriteria.Text = "Selection Criteria";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(266, 11);
            this.txtGroupName.MasterDescription = "";
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(353, 21);
            this.txtGroupName.TabIndex = 124;
            this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // cmbSelectionCriteria
            // 
            this.cmbSelectionCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionCriteria.FormattingEnabled = true;
            this.cmbSelectionCriteria.Items.AddRange(new object[] {
            "Base On Voucher Quantity",
            "Base On Voucher No Range",
            "Base On Voucher Serial Range"});
            this.cmbSelectionCriteria.Location = new System.Drawing.Point(408, 58);
            this.cmbSelectionCriteria.Name = "cmbSelectionCriteria";
            this.cmbSelectionCriteria.Size = new System.Drawing.Size(211, 21);
            this.cmbSelectionCriteria.TabIndex = 7;
            this.cmbSelectionCriteria.SelectedValueChanged += new System.EventHandler(this.cmbSelectionCriteria_SelectedValueChanged);
            this.cmbSelectionCriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelectionCriteria_KeyDown);
            // 
            // lblGiftVoucherGroup
            // 
            this.lblGiftVoucherGroup.AutoSize = true;
            this.lblGiftVoucherGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherGroup.Location = new System.Drawing.Point(1, 11);
            this.lblGiftVoucherGroup.Name = "lblGiftVoucherGroup";
            this.lblGiftVoucherGroup.Size = new System.Drawing.Size(116, 13);
            this.lblGiftVoucherGroup.TabIndex = 125;
            this.lblGiftVoucherGroup.Text = "Gift Voucher Group";
            // 
            // lblBook
            // 
            this.lblBook.AutoSize = true;
            this.lblBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBook.Location = new System.Drawing.Point(1, 34);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(36, 13);
            this.lblBook.TabIndex = 122;
            this.lblBook.Text = "Book";
            // 
            // txtBookCode
            // 
            this.txtBookCode.IsAutoComplete = false;
            this.txtBookCode.ItemCollection = null;
            this.txtBookCode.Location = new System.Drawing.Point(140, 34);
            this.txtBookCode.MasterCode = "";
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(125, 21);
            this.txtBookCode.TabIndex = 8;
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.Validated += new System.EventHandler(this.txtBookCode_Validated);
            // 
            // txtBookName
            // 
            this.txtBookName.Location = new System.Drawing.Point(266, 34);
            this.txtBookName.MasterDescription = "";
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(353, 21);
            this.txtBookName.TabIndex = 119;
            this.txtBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookName_KeyDown);
            this.txtBookName.Leave += new System.EventHandler(this.txtBookName_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtPercentageOfCoupon);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtNoOfVouchersOnBook);
            this.groupBox5.Controls.Add(this.txtGiftVoucherValue);
            this.groupBox5.Controls.Add(this.lblNoOfVouchersOnBook);
            this.groupBox5.Controls.Add(this.lblGiftVoucherValue);
            this.groupBox5.Location = new System.Drawing.Point(3, 109);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(407, 85);
            this.groupBox5.TabIndex = 111;
            this.groupBox5.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 130;
            this.label7.Text = "%";
            // 
            // txtPercentageOfCoupon
            // 
            this.txtPercentageOfCoupon.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPercentageOfCoupon.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPercentageOfCoupon.Location = new System.Drawing.Point(139, 20);
            this.txtPercentageOfCoupon.Name = "txtPercentageOfCoupon";
            this.txtPercentageOfCoupon.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPercentageOfCoupon.Size = new System.Drawing.Size(50, 21);
            this.txtPercentageOfCoupon.TabIndex = 128;
            this.txtPercentageOfCoupon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 129;
            this.label4.Text = "Percentage of Coupon";
            // 
            // txtNoOfVouchersOnBook
            // 
            this.txtNoOfVouchersOnBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNoOfVouchersOnBook.IntValue = 0;
            this.txtNoOfVouchersOnBook.Location = new System.Drawing.Point(146, 47);
            this.txtNoOfVouchersOnBook.MaxLength = 1;
            this.txtNoOfVouchersOnBook.Name = "txtNoOfVouchersOnBook";
            this.txtNoOfVouchersOnBook.ReadOnly = true;
            this.txtNoOfVouchersOnBook.Size = new System.Drawing.Size(43, 21);
            this.txtNoOfVouchersOnBook.TabIndex = 126;
            // 
            // txtGiftVoucherValue
            // 
            this.txtGiftVoucherValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtGiftVoucherValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftVoucherValue.Location = new System.Drawing.Point(300, 47);
            this.txtGiftVoucherValue.Name = "txtGiftVoucherValue";
            this.txtGiftVoucherValue.ReadOnly = true;
            this.txtGiftVoucherValue.Size = new System.Drawing.Size(104, 21);
            this.txtGiftVoucherValue.TabIndex = 120;
            this.txtGiftVoucherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoOfVouchersOnBook
            // 
            this.lblNoOfVouchersOnBook.AutoSize = true;
            this.lblNoOfVouchersOnBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVouchersOnBook.Location = new System.Drawing.Point(1, 52);
            this.lblNoOfVouchersOnBook.Name = "lblNoOfVouchersOnBook";
            this.lblNoOfVouchersOnBook.Size = new System.Drawing.Size(144, 13);
            this.lblNoOfVouchersOnBook.TabIndex = 127;
            this.lblNoOfVouchersOnBook.Text = "No of Vouchers on Book";
            // 
            // lblGiftVoucherValue
            // 
            this.lblGiftVoucherValue.AutoSize = true;
            this.lblGiftVoucherValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherValue.Location = new System.Drawing.Point(188, 52);
            this.lblGiftVoucherValue.Name = "lblGiftVoucherValue";
            this.lblGiftVoucherValue.Size = new System.Drawing.Size(112, 13);
            this.lblGiftVoucherValue.TabIndex = 121;
            this.lblGiftVoucherValue.Text = "Gift Voucher Value";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbMore);
            this.groupBox3.Location = new System.Drawing.Point(413, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 121);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            // 
            // tbMore
            // 
            this.tbMore.Controls.Add(this.tbpVoucherSerial);
            this.tbMore.Controls.Add(this.tbpVoucherNo);
            this.tbMore.Location = new System.Drawing.Point(7, 13);
            this.tbMore.Name = "tbMore";
            this.tbMore.SelectedIndex = 0;
            this.tbMore.Size = new System.Drawing.Size(202, 106);
            this.tbMore.TabIndex = 60;
            // 
            // tbpVoucherSerial
            // 
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialQty);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialQty);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialTo);
            this.tbpVoucherSerial.Controls.Add(this.txtVoucherSerialFrom);
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialTo);
            this.tbpVoucherSerial.Controls.Add(this.lblVoucherSerialFrom);
            this.tbpVoucherSerial.Location = new System.Drawing.Point(4, 22);
            this.tbpVoucherSerial.Name = "tbpVoucherSerial";
            this.tbpVoucherSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpVoucherSerial.Size = new System.Drawing.Size(194, 80);
            this.tbpVoucherSerial.TabIndex = 1;
            this.tbpVoucherSerial.Text = "Voucher Serial";
            this.tbpVoucherSerial.UseVisualStyleBackColor = true;
            // 
            // lblVoucherSerialQty
            // 
            this.lblVoucherSerialQty.AutoSize = true;
            this.lblVoucherSerialQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherSerialQty.Location = new System.Drawing.Point(16, 64);
            this.lblVoucherSerialQty.Name = "lblVoucherSerialQty";
            this.lblVoucherSerialQty.Size = new System.Drawing.Size(27, 13);
            this.lblVoucherSerialQty.TabIndex = 109;
            this.lblVoucherSerialQty.Text = "Qty";
            // 
            // txtVoucherSerialQty
            // 
            this.txtVoucherSerialQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtVoucherSerialQty.IntValue = 0;
            this.txtVoucherSerialQty.Location = new System.Drawing.Point(56, 56);
            this.txtVoucherSerialQty.Name = "txtVoucherSerialQty";
            this.txtVoucherSerialQty.Size = new System.Drawing.Size(99, 21);
            this.txtVoucherSerialQty.TabIndex = 108;
            this.txtVoucherSerialQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVoucherSerialTo
            // 
            this.txtVoucherSerialTo.Location = new System.Drawing.Point(56, 28);
            this.txtVoucherSerialTo.Name = "txtVoucherSerialTo";
            this.txtVoucherSerialTo.Size = new System.Drawing.Size(110, 21);
            this.txtVoucherSerialTo.TabIndex = 13;
            this.txtVoucherSerialTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherSerialTo_KeyDown);
            this.txtVoucherSerialTo.Leave += new System.EventHandler(this.txtVoucherSerialTo_Leave);
            // 
            // txtVoucherSerialFrom
            // 
            this.txtVoucherSerialFrom.Location = new System.Drawing.Point(56, 4);
            this.txtVoucherSerialFrom.Name = "txtVoucherSerialFrom";
            this.txtVoucherSerialFrom.Size = new System.Drawing.Size(110, 21);
            this.txtVoucherSerialFrom.TabIndex = 12;
            this.txtVoucherSerialFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherSerialFrom_KeyDown);
            this.txtVoucherSerialFrom.Leave += new System.EventHandler(this.txtVoucherSerialFrom_Leave);
            // 
            // lblVoucherSerialTo
            // 
            this.lblVoucherSerialTo.AutoSize = true;
            this.lblVoucherSerialTo.Location = new System.Drawing.Point(16, 31);
            this.lblVoucherSerialTo.Name = "lblVoucherSerialTo";
            this.lblVoucherSerialTo.Size = new System.Drawing.Size(20, 13);
            this.lblVoucherSerialTo.TabIndex = 30;
            this.lblVoucherSerialTo.Text = "To";
            // 
            // lblVoucherSerialFrom
            // 
            this.lblVoucherSerialFrom.AutoSize = true;
            this.lblVoucherSerialFrom.Location = new System.Drawing.Point(16, 7);
            this.lblVoucherSerialFrom.Name = "lblVoucherSerialFrom";
            this.lblVoucherSerialFrom.Size = new System.Drawing.Size(36, 13);
            this.lblVoucherSerialFrom.TabIndex = 29;
            this.lblVoucherSerialFrom.Text = "From";
            // 
            // tbpVoucherNo
            // 
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoQty);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoQty);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoTo);
            this.tbpVoucherNo.Controls.Add(this.txtVoucherNoFrom);
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoTo);
            this.tbpVoucherNo.Controls.Add(this.lblVoucherNoFrom);
            this.tbpVoucherNo.Location = new System.Drawing.Point(4, 22);
            this.tbpVoucherNo.Name = "tbpVoucherNo";
            this.tbpVoucherNo.Padding = new System.Windows.Forms.Padding(3);
            this.tbpVoucherNo.Size = new System.Drawing.Size(194, 80);
            this.tbpVoucherNo.TabIndex = 0;
            this.tbpVoucherNo.Text = "Voucher No";
            this.tbpVoucherNo.UseVisualStyleBackColor = true;
            // 
            // lblVoucherNoQty
            // 
            this.lblVoucherNoQty.AutoSize = true;
            this.lblVoucherNoQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherNoQty.Location = new System.Drawing.Point(17, 61);
            this.lblVoucherNoQty.Name = "lblVoucherNoQty";
            this.lblVoucherNoQty.Size = new System.Drawing.Size(27, 13);
            this.lblVoucherNoQty.TabIndex = 113;
            this.lblVoucherNoQty.Text = "Qty";
            // 
            // txtVoucherNoQty
            // 
            this.txtVoucherNoQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtVoucherNoQty.IntValue = 0;
            this.txtVoucherNoQty.Location = new System.Drawing.Point(55, 55);
            this.txtVoucherNoQty.Name = "txtVoucherNoQty";
            this.txtVoucherNoQty.Size = new System.Drawing.Size(99, 21);
            this.txtVoucherNoQty.TabIndex = 112;
            this.txtVoucherNoQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVoucherNoTo
            // 
            this.txtVoucherNoTo.Location = new System.Drawing.Point(55, 28);
            this.txtVoucherNoTo.Name = "txtVoucherNoTo";
            this.txtVoucherNoTo.Size = new System.Drawing.Size(118, 21);
            this.txtVoucherNoTo.TabIndex = 13;
            this.txtVoucherNoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNoTo_KeyDown);
            this.txtVoucherNoTo.Leave += new System.EventHandler(this.txtVoucherNoTo_Leave);
            // 
            // txtVoucherNoFrom
            // 
            this.txtVoucherNoFrom.Location = new System.Drawing.Point(55, 4);
            this.txtVoucherNoFrom.Name = "txtVoucherNoFrom";
            this.txtVoucherNoFrom.Size = new System.Drawing.Size(118, 21);
            this.txtVoucherNoFrom.TabIndex = 12;
            this.txtVoucherNoFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNoFrom_KeyDown);
            this.txtVoucherNoFrom.Leave += new System.EventHandler(this.txtVoucherNoFrom_Leave);
            // 
            // lblVoucherNoTo
            // 
            this.lblVoucherNoTo.AutoSize = true;
            this.lblVoucherNoTo.Location = new System.Drawing.Point(17, 31);
            this.lblVoucherNoTo.Name = "lblVoucherNoTo";
            this.lblVoucherNoTo.Size = new System.Drawing.Size(20, 13);
            this.lblVoucherNoTo.TabIndex = 26;
            this.lblVoucherNoTo.Text = "To";
            // 
            // lblVoucherNoFrom
            // 
            this.lblVoucherNoFrom.AutoSize = true;
            this.lblVoucherNoFrom.Location = new System.Drawing.Point(17, 8);
            this.lblVoucherNoFrom.Name = "lblVoucherNoFrom";
            this.lblVoucherNoFrom.Size = new System.Drawing.Size(36, 13);
            this.lblVoucherNoFrom.TabIndex = 25;
            this.lblVoucherNoFrom.Text = "From";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblGiftVoucherQty);
            this.groupBox2.Controls.Add(this.txtGiftVoucherQty);
            this.groupBox2.Location = new System.Drawing.Point(3, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 41);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            // 
            // lblGiftVoucherQty
            // 
            this.lblGiftVoucherQty.AutoSize = true;
            this.lblGiftVoucherQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherQty.Location = new System.Drawing.Point(2, 16);
            this.lblGiftVoucherQty.Name = "lblGiftVoucherQty";
            this.lblGiftVoucherQty.Size = new System.Drawing.Size(101, 13);
            this.lblGiftVoucherQty.TabIndex = 105;
            this.lblGiftVoucherQty.Text = "Gift Voucher Qty";
            // 
            // txtGiftVoucherQty
            // 
            this.txtGiftVoucherQty.IntValue = 0;
            this.txtGiftVoucherQty.Location = new System.Drawing.Point(127, 13);
            this.txtGiftVoucherQty.Name = "txtGiftVoucherQty";
            this.txtGiftVoucherQty.Size = new System.Drawing.Size(151, 21);
            this.txtGiftVoucherQty.TabIndex = 12;
            this.txtGiftVoucherQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiftVoucherQty.Leave += new System.EventHandler(this.txtGiftVoucherQty_Leave);
            // 
            // FrmGiftVoucherTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(871, 572);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.groupBox4);
            this.Name = "FrmGiftVoucherTransfer";
            this.Text = "Gift Voucher Transfer";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.GrpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpHeader.ResumeLayout(false);
            this.GrpHeader.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherBookDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherTransferNoteDetailTempBindingSource)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tbMore.ResumeLayout(false);
            this.tbpVoucherSerial.ResumeLayout(false);
            this.tbpVoucherSerial.PerformLayout();
            this.tbpVoucherNo.ResumeLayout(false);
            this.tbpVoucherNo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtGrnNo;
        private System.Windows.Forms.Button btnGrnDetails;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpTransferDate;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.CheckBox chkAutoCompleationGrnNo;
        private System.Windows.Forms.Label lblTransferDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblGrnNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label lblToLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TextBox txtVoucherValue;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtVoucherSerial;
        private System.Windows.Forms.CheckBox chkAutoCompleationBookCode;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.DataGridView dgvVoucherBookDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblSelectionCriteria;
        private System.Windows.Forms.ComboBox cmbSelectionCriteria;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxMasterCode txtGroupCode;
        private CustomControls.TextBoxMasterDescription txtGroupName;
        private System.Windows.Forms.Label lblGiftVoucherGroup;
        private System.Windows.Forms.Label lblBook;
        private CustomControls.TextBoxMasterCode txtBookCode;
        private CustomControls.TextBoxMasterDescription txtBookName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tbMore;
        private System.Windows.Forms.TabPage tbpVoucherNo;
        private System.Windows.Forms.TextBox txtVoucherNoTo;
        private System.Windows.Forms.TextBox txtVoucherNoFrom;
        private System.Windows.Forms.Label lblVoucherNoTo;
        private System.Windows.Forms.Label lblVoucherNoFrom;
        private System.Windows.Forms.TabPage tbpVoucherSerial;
        private System.Windows.Forms.TextBox txtVoucherSerialTo;
        private System.Windows.Forms.TextBox txtVoucherSerialFrom;
        private System.Windows.Forms.Label lblVoucherSerialTo;
        private System.Windows.Forms.Label lblVoucherSerialFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblGiftVoucherQty;
        private CustomControls.TextBoxInteger txtGiftVoucherQty;
        private System.Windows.Forms.Label lblTotalQty;
        private CustomControls.TextBoxInteger txtTotalQty;
        private System.Windows.Forms.ComboBox cmbFromLocation;
        private System.Windows.Forms.ComboBox cmbToLocation;
        private System.Windows.Forms.BindingSource invGiftVoucherTransferNoteDetailTempBindingSource;
        private System.Windows.Forms.CheckBox chkAutoCompleationGroup;
        private System.Windows.Forms.CheckBox chkAutoCompleationBook;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.ComboBox cmbBasedOn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private CustomControls.TextBoxPercentGen txtPercentageOfCoupon;
        private System.Windows.Forms.Label label4;
        private CustomControls.TextBoxInteger txtNoOfVouchersOnBook;
        private CustomControls.TextBoxCurrency txtGiftVoucherValue;
        private System.Windows.Forms.Label lblNoOfVouchersOnBook;
        private System.Windows.Forms.Label lblGiftVoucherValue;
        private System.Windows.Forms.RadioButton rdoCoupon;
        private System.Windows.Forms.RadioButton rdoVoucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherSerialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherAmount;
        public Glass.GlassButton btnLoad;
        private System.Windows.Forms.Label lblVoucherSerialQty;
        private CustomControls.TextBoxInteger txtVoucherSerialQty;
        private System.Windows.Forms.Label lblVoucherNoQty;
        private CustomControls.TextBoxInteger txtVoucherNoQty;
    }
}
