namespace UI.Windows
{
    partial class FrmGiftVoucherMaster
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPercentageOfCoupon = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cmbBasedOn = new System.Windows.Forms.ComboBox();
            this.txtNoOfVouchers = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblNoOfVouchers = new System.Windows.Forms.Label();
            this.txtGroupCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoCompleationGroup = new System.Windows.Forms.CheckBox();
            this.txtGroupName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblGiftVoucherGroup = new System.Windows.Forms.Label();
            this.txtPrefix = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtStartingNo = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtLength = new UI.Windows.CustomControls.TextBoxInteger();
            this.chkAutoCompleationBook = new System.Windows.Forms.CheckBox();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.lblVoucherPrefix = new System.Windows.Forms.Label();
            this.lblStartingNo = new System.Windows.Forms.Label();
            this.lblSerialLength = new System.Windows.Forms.Label();
            this.txtNoOfVouchersOnBook = new UI.Windows.CustomControls.TextBoxInteger();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblSerialFormat = new System.Windows.Forms.Label();
            this.lblNoOfVouchersOnBook = new System.Windows.Forms.Label();
            this.lblBook = new System.Windows.Forms.Label();
            this.txtBookCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblVoucherValue = new System.Windows.Forms.Label();
            this.txtVoucherValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.voucherNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherSerialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invGiftVoucherMasterTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoCoupon = new System.Windows.Forms.RadioButton();
            this.rdoVoucher = new System.Windows.Forms.RadioButton();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherMasterTempBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 389);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(466, 389);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 16;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 15;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 14;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 13;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPercentageOfCoupon);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblBasedOn);
            this.groupBox1.Controls.Add(this.cmbBasedOn);
            this.groupBox1.Controls.Add(this.txtNoOfVouchers);
            this.groupBox1.Controls.Add(this.lblNoOfVouchers);
            this.groupBox1.Controls.Add(this.txtGroupCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationGroup);
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.lblGiftVoucherGroup);
            this.groupBox1.Controls.Add(this.txtPrefix);
            this.groupBox1.Controls.Add(this.txtStartingNo);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.chkAutoCompleationBook);
            this.groupBox1.Controls.Add(this.txtFormat);
            this.groupBox1.Controls.Add(this.lblVoucherPrefix);
            this.groupBox1.Controls.Add(this.lblStartingNo);
            this.groupBox1.Controls.Add(this.lblSerialLength);
            this.groupBox1.Controls.Add(this.txtNoOfVouchersOnBook);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.lblSerialFormat);
            this.groupBox1.Controls.Add(this.lblNoOfVouchersOnBook);
            this.groupBox1.Controls.Add(this.lblBook);
            this.groupBox1.Controls.Add(this.txtBookCode);
            this.groupBox1.Controls.Add(this.txtBookName);
            this.groupBox1.Controls.Add(this.lblVoucherValue);
            this.groupBox1.Controls.Add(this.txtVoucherValue);
            this.groupBox1.Location = new System.Drawing.Point(2, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(703, 140);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 107;
            this.label7.Text = "%";
            // 
            // txtPercentageOfCoupon
            // 
            this.txtPercentageOfCoupon.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPercentageOfCoupon.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPercentageOfCoupon.Location = new System.Drawing.Point(160, 111);
            this.txtPercentageOfCoupon.Name = "txtPercentageOfCoupon";
            this.txtPercentageOfCoupon.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPercentageOfCoupon.Size = new System.Drawing.Size(61, 21);
            this.txtPercentageOfCoupon.TabIndex = 105;
            this.txtPercentageOfCoupon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Percentage Of Coupon*";
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(247, 117);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(62, 13);
            this.lblBasedOn.TabIndex = 100;
            this.lblBasedOn.Text = "Based On";
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Items.AddRange(new object[] {
            "Book Serial",
            "Voucher Serial"});
            this.cmbBasedOn.Location = new System.Drawing.Point(345, 111);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(218, 21);
            this.cmbBasedOn.TabIndex = 99;
            // 
            // txtNoOfVouchers
            // 
            this.txtNoOfVouchers.IntValue = 0;
            this.txtNoOfVouchers.Location = new System.Drawing.Point(345, 65);
            this.txtNoOfVouchers.MaxLength = 1;
            this.txtNoOfVouchers.Name = "txtNoOfVouchers";
            this.txtNoOfVouchers.Size = new System.Drawing.Size(74, 21);
            this.txtNoOfVouchers.TabIndex = 97;
            this.txtNoOfVouchers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfVouchers_KeyDown);
            this.txtNoOfVouchers.Validated += new System.EventHandler(this.txtNoOfVouchers_Validated);
            // 
            // lblNoOfVouchers
            // 
            this.lblNoOfVouchers.AutoSize = true;
            this.lblNoOfVouchers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVouchers.Location = new System.Drawing.Point(247, 68);
            this.lblNoOfVouchers.Name = "lblNoOfVouchers";
            this.lblNoOfVouchers.Size = new System.Drawing.Size(100, 13);
            this.lblNoOfVouchers.TabIndex = 98;
            this.lblNoOfVouchers.Text = "No of Vouchers*";
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.IsAutoComplete = false;
            this.txtGroupCode.ItemCollection = null;
            this.txtGroupCode.Location = new System.Drawing.Point(160, 17);
            this.txtGroupCode.MasterCode = "";
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(183, 21);
            this.txtGroupCode.TabIndex = 93;
            this.txtGroupCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupCode_KeyDown);
            this.txtGroupCode.Leave += new System.EventHandler(this.txtGroupCode_Leave);
            // 
            // chkAutoCompleationGroup
            // 
            this.chkAutoCompleationGroup.AutoSize = true;
            this.chkAutoCompleationGroup.Checked = true;
            this.chkAutoCompleationGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGroup.Location = new System.Drawing.Point(142, 17);
            this.chkAutoCompleationGroup.Name = "chkAutoCompleationGroup";
            this.chkAutoCompleationGroup.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGroup.TabIndex = 96;
            this.chkAutoCompleationGroup.Tag = "1";
            this.chkAutoCompleationGroup.UseVisualStyleBackColor = true;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(345, 17);
            this.txtGroupName.MasterDescription = "";
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(352, 21);
            this.txtGroupName.TabIndex = 94;
            this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // lblGiftVoucherGroup
            // 
            this.lblGiftVoucherGroup.AutoSize = true;
            this.lblGiftVoucherGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherGroup.Location = new System.Drawing.Point(7, 20);
            this.lblGiftVoucherGroup.Name = "lblGiftVoucherGroup";
            this.lblGiftVoucherGroup.Size = new System.Drawing.Size(123, 13);
            this.lblGiftVoucherGroup.TabIndex = 95;
            this.lblGiftVoucherGroup.Text = "Gift Voucher Group*";
            // 
            // txtPrefix
            // 
            this.txtPrefix.IsAutoComplete = false;
            this.txtPrefix.ItemCollection = null;
            this.txtPrefix.Location = new System.Drawing.Point(655, 88);
            this.txtPrefix.MasterCode = "";
            this.txtPrefix.MaxLength = 2;
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(42, 21);
            this.txtPrefix.TabIndex = 5;
            this.txtPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrefix_KeyDown);
            this.txtPrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrefix_KeyPress);
            this.txtPrefix.Validated += new System.EventHandler(this.txtPrefix_Validated);
            // 
            // txtStartingNo
            // 
            this.txtStartingNo.IntValue = 0;
            this.txtStartingNo.Location = new System.Drawing.Point(468, 88);
            this.txtStartingNo.Name = "txtStartingNo";
            this.txtStartingNo.Size = new System.Drawing.Size(95, 21);
            this.txtStartingNo.TabIndex = 8;
            this.txtStartingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartingNo_KeyDown);
            this.txtStartingNo.Validated += new System.EventHandler(this.txtStartingNo_Validated);
            // 
            // txtLength
            // 
            this.txtLength.IntValue = 0;
            this.txtLength.Location = new System.Drawing.Point(345, 88);
            this.txtLength.MaxLength = 2;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(38, 21);
            this.txtLength.TabIndex = 6;
            this.txtLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLength_KeyDown);
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            this.txtLength.Validated += new System.EventHandler(this.txtLength_Validated);
            // 
            // chkAutoCompleationBook
            // 
            this.chkAutoCompleationBook.AutoSize = true;
            this.chkAutoCompleationBook.Checked = true;
            this.chkAutoCompleationBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBook.Location = new System.Drawing.Point(142, 43);
            this.chkAutoCompleationBook.Name = "chkAutoCompleationBook";
            this.chkAutoCompleationBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBook.TabIndex = 92;
            this.chkAutoCompleationBook.Tag = "1";
            this.chkAutoCompleationBook.UseVisualStyleBackColor = true;
            // 
            // txtFormat
            // 
            this.txtFormat.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtFormat.Location = new System.Drawing.Point(521, 65);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.ReadOnly = true;
            this.txtFormat.Size = new System.Drawing.Size(176, 21);
            this.txtFormat.TabIndex = 23;
            // 
            // lblVoucherPrefix
            // 
            this.lblVoucherPrefix.AutoSize = true;
            this.lblVoucherPrefix.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherPrefix.Location = new System.Drawing.Point(564, 91);
            this.lblVoucherPrefix.Name = "lblVoucherPrefix";
            this.lblVoucherPrefix.Size = new System.Drawing.Size(90, 13);
            this.lblVoucherPrefix.TabIndex = 9;
            this.lblVoucherPrefix.Text = "Voucher Prefix";
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AutoSize = true;
            this.lblStartingNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartingNo.Location = new System.Drawing.Point(391, 91);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(78, 13);
            this.lblStartingNo.TabIndex = 34;
            this.lblStartingNo.Text = "Starting No*";
            // 
            // lblSerialLength
            // 
            this.lblSerialLength.AutoSize = true;
            this.lblSerialLength.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialLength.Location = new System.Drawing.Point(247, 91);
            this.lblSerialLength.Name = "lblSerialLength";
            this.lblSerialLength.Size = new System.Drawing.Size(89, 13);
            this.lblSerialLength.TabIndex = 20;
            this.lblSerialLength.Text = "Serial Length*";
            // 
            // txtNoOfVouchersOnBook
            // 
            this.txtNoOfVouchersOnBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNoOfVouchersOnBook.IntValue = 0;
            this.txtNoOfVouchersOnBook.Location = new System.Drawing.Point(160, 88);
            this.txtNoOfVouchersOnBook.MaxLength = 1;
            this.txtNoOfVouchersOnBook.Name = "txtNoOfVouchersOnBook";
            this.txtNoOfVouchersOnBook.ReadOnly = true;
            this.txtNoOfVouchersOnBook.Size = new System.Drawing.Size(61, 21);
            this.txtNoOfVouchersOnBook.TabIndex = 9;
            this.txtNoOfVouchersOnBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfVouchersOnBook_KeyDown);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(628, 112);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(69, 23);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblSerialFormat
            // 
            this.lblSerialFormat.AutoSize = true;
            this.lblSerialFormat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialFormat.Location = new System.Drawing.Point(432, 68);
            this.lblSerialFormat.Name = "lblSerialFormat";
            this.lblSerialFormat.Size = new System.Drawing.Size(84, 13);
            this.lblSerialFormat.TabIndex = 24;
            this.lblSerialFormat.Text = "Serial Format";
            // 
            // lblNoOfVouchersOnBook
            // 
            this.lblNoOfVouchersOnBook.AutoSize = true;
            this.lblNoOfVouchersOnBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVouchersOnBook.Location = new System.Drawing.Point(8, 91);
            this.lblNoOfVouchersOnBook.Name = "lblNoOfVouchersOnBook";
            this.lblNoOfVouchersOnBook.Size = new System.Drawing.Size(151, 13);
            this.lblNoOfVouchersOnBook.TabIndex = 32;
            this.lblNoOfVouchersOnBook.Text = "No of Vouchers on Book*";
            // 
            // lblBook
            // 
            this.lblBook.AutoSize = true;
            this.lblBook.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBook.Location = new System.Drawing.Point(8, 43);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(43, 13);
            this.lblBook.TabIndex = 30;
            this.lblBook.Text = "Book*";
            // 
            // txtBookCode
            // 
            this.txtBookCode.IsAutoComplete = false;
            this.txtBookCode.ItemCollection = null;
            this.txtBookCode.Location = new System.Drawing.Point(160, 41);
            this.txtBookCode.MasterCode = "";
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(183, 21);
            this.txtBookCode.TabIndex = 1;
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.Leave += new System.EventHandler(this.txtBookCode_Leave);
            // 
            // txtBookName
            // 
            this.txtBookName.Location = new System.Drawing.Point(345, 41);
            this.txtBookName.MasterDescription = "";
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(352, 21);
            this.txtBookName.TabIndex = 4;
            this.txtBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookName_KeyDown);
            this.txtBookName.Leave += new System.EventHandler(this.txtBookName_Leave);
            // 
            // lblVoucherValue
            // 
            this.lblVoucherValue.AutoSize = true;
            this.lblVoucherValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherValue.Location = new System.Drawing.Point(7, 68);
            this.lblVoucherValue.Name = "lblVoucherValue";
            this.lblVoucherValue.Size = new System.Drawing.Size(119, 13);
            this.lblVoucherValue.TabIndex = 26;
            this.lblVoucherValue.Text = "Gift Voucher Value*";
            // 
            // txtVoucherValue
            // 
            this.txtVoucherValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtVoucherValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVoucherValue.Location = new System.Drawing.Point(160, 65);
            this.txtVoucherValue.Name = "txtVoucherValue";
            this.txtVoucherValue.ReadOnly = true;
            this.txtVoucherValue.Size = new System.Drawing.Size(83, 21);
            this.txtVoucherValue.TabIndex = 7;
            this.txtVoucherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVoucherValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherValue_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDisplay);
            this.groupBox2.Location = new System.Drawing.Point(2, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(703, 232);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.AutoGenerateColumns = false;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.voucherNoDataGridViewTextBoxColumn,
            this.voucherSerialDataGridViewTextBoxColumn});
            this.dgvDisplay.DataSource = this.invGiftVoucherMasterTempBindingSource;
            this.dgvDisplay.Location = new System.Drawing.Point(10, 14);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.RowHeadersWidth = 20;
            this.dgvDisplay.Size = new System.Drawing.Size(686, 212);
            this.dgvDisplay.TabIndex = 21;
            // 
            // voucherNoDataGridViewTextBoxColumn
            // 
            this.voucherNoDataGridViewTextBoxColumn.DataPropertyName = "VoucherNo";
            this.voucherNoDataGridViewTextBoxColumn.HeaderText = "Voucher No";
            this.voucherNoDataGridViewTextBoxColumn.Name = "voucherNoDataGridViewTextBoxColumn";
            this.voucherNoDataGridViewTextBoxColumn.Width = 220;
            // 
            // voucherSerialDataGridViewTextBoxColumn
            // 
            this.voucherSerialDataGridViewTextBoxColumn.DataPropertyName = "VoucherSerial";
            this.voucherSerialDataGridViewTextBoxColumn.HeaderText = "Voucher Serial";
            this.voucherSerialDataGridViewTextBoxColumn.Name = "voucherSerialDataGridViewTextBoxColumn";
            this.voucherSerialDataGridViewTextBoxColumn.Width = 220;
            // 
            // invGiftVoucherMasterTempBindingSource
            // 
            this.invGiftVoucherMasterTempBindingSource.DataSource = typeof(Domain.InvGiftVoucherMasterTemp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoCoupon);
            this.groupBox3.Controls.Add(this.rdoVoucher);
            this.groupBox3.Location = new System.Drawing.Point(2, -5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(703, 37);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // rdoCoupon
            // 
            this.rdoCoupon.AutoSize = true;
            this.rdoCoupon.Location = new System.Drawing.Point(371, 14);
            this.rdoCoupon.Name = "rdoCoupon";
            this.rdoCoupon.Size = new System.Drawing.Size(69, 17);
            this.rdoCoupon.TabIndex = 1;
            this.rdoCoupon.Text = "Coupon";
            this.rdoCoupon.UseVisualStyleBackColor = true;
            this.rdoCoupon.CheckedChanged += new System.EventHandler(this.rdoCoupon_CheckedChanged);
            // 
            // rdoVoucher
            // 
            this.rdoVoucher.AutoSize = true;
            this.rdoVoucher.Checked = true;
            this.rdoVoucher.Location = new System.Drawing.Point(244, 14);
            this.rdoVoucher.Name = "rdoVoucher";
            this.rdoVoucher.Size = new System.Drawing.Size(71, 17);
            this.rdoVoucher.TabIndex = 0;
            this.rdoVoucher.TabStop = true;
            this.rdoVoucher.Text = "Voucher";
            this.rdoVoucher.UseVisualStyleBackColor = true;
            this.rdoVoucher.CheckedChanged += new System.EventHandler(this.rdoVoucher_CheckedChanged);
            // 
            // FrmGiftVoucherMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(707, 438);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmGiftVoucherMaster";
            this.Text = "Gift Voucher Master";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invGiftVoucherMasterTempBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSerialFormat;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.Label lblSerialLength;
        private System.Windows.Forms.Label lblVoucherPrefix;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblVoucherValue;
        private CustomControls.TextBoxCurrency txtVoucherValue;
        private System.Windows.Forms.Label lblStartingNo;
        private CustomControls.TextBoxInteger txtNoOfVouchersOnBook;
        private System.Windows.Forms.Label lblNoOfVouchersOnBook;
        private CustomControls.TextBoxInteger txtLength;
        private CustomControls.TextBoxInteger txtStartingNo;
        private CustomControls.TextBoxMasterCode txtPrefix;
        private System.Windows.Forms.CheckBox chkAutoCompleationBook;
        private System.Windows.Forms.Label lblBook;
        private CustomControls.TextBoxMasterCode txtBookCode;
        private CustomControls.TextBoxMasterDescription txtBookName;
        private CustomControls.TextBoxMasterCode txtGroupCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationGroup;
        private CustomControls.TextBoxMasterDescription txtGroupName;
        private System.Windows.Forms.Label lblGiftVoucherGroup;
        private CustomControls.TextBoxInteger txtNoOfVouchers;
        private System.Windows.Forms.Label lblNoOfVouchers;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource invGiftVoucherMasterTempBindingSource;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoCoupon;
        private System.Windows.Forms.RadioButton rdoVoucher;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.ComboBox cmbBasedOn;
        private System.Windows.Forms.Label label7;
        private CustomControls.TextBoxPercentGen txtPercentageOfCoupon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherSerialDataGridViewTextBoxColumn;
    }
}
