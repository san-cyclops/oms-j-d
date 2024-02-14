namespace UI.Windows
{
    partial class FrmGiftVoucherBookCodeGeneration
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cmbBasedOn = new System.Windows.Forms.ComboBox();
            this.txtNoOfVouchers = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblNoOfVouchers = new System.Windows.Forms.Label();
            this.txtBookPrefix = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationBook = new System.Windows.Forms.CheckBox();
            this.txtBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBookCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblBookCode = new System.Windows.Forms.Label();
            this.lblBookName = new System.Windows.Forms.Label();
            this.txtLength = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblSerialLength = new System.Windows.Forms.Label();
            this.lblStartingNo = new System.Windows.Forms.Label();
            this.lblVoucherPrefix = new System.Windows.Forms.Label();
            this.txtStartingNo = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtGroupCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoCompleationGroup = new System.Windows.Forms.CheckBox();
            this.txtGroupName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblGiftVoucherGroup = new System.Windows.Forms.Label();
            this.lblGiftVoucherValue = new System.Windows.Forms.Label();
            this.txtGiftVoucherValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoCoupon = new System.Windows.Forms.RadioButton();
            this.rdoVoucher = new System.Windows.Forms.RadioButton();
            this.grpVoucher = new System.Windows.Forms.GroupBox();
            this.txtValidityPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblValidityPeriod = new System.Windows.Forms.Label();
            this.grpCoupon = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPercentageOfCoupon = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.lblPercentageOfCoupon = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpVoucher.SuspendLayout();
            this.grpCoupon.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 251);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(421, 251);
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
            this.groupBox1.Controls.Add(this.lblBasedOn);
            this.groupBox1.Controls.Add(this.cmbBasedOn);
            this.groupBox1.Controls.Add(this.txtNoOfVouchers);
            this.groupBox1.Controls.Add(this.lblNoOfVouchers);
            this.groupBox1.Controls.Add(this.txtBookPrefix);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationBook);
            this.groupBox1.Controls.Add(this.txtBookName);
            this.groupBox1.Controls.Add(this.txtBookCode);
            this.groupBox1.Controls.Add(this.lblBookCode);
            this.groupBox1.Controls.Add(this.lblBookName);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.lblSerialLength);
            this.groupBox1.Controls.Add(this.lblStartingNo);
            this.groupBox1.Controls.Add(this.lblVoucherPrefix);
            this.groupBox1.Controls.Add(this.txtStartingNo);
            this.groupBox1.Controls.Add(this.txtGroupCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationGroup);
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.lblGiftVoucherGroup);
            this.groupBox1.Location = new System.Drawing.Point(2, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(658, 154);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(6, 37);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(62, 13);
            this.lblBasedOn.TabIndex = 61;
            this.lblBasedOn.Text = "Based On";
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Items.AddRange(new object[] {
            "Book Serial",
            "Voucher Serial"});
            this.cmbBasedOn.Location = new System.Drawing.Point(158, 34);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(153, 21);
            this.cmbBasedOn.TabIndex = 60;
            this.cmbBasedOn.SelectedValueChanged += new System.EventHandler(this.cmbBasedOn_SelectedValueChanged);
            this.cmbBasedOn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBasedOn_KeyDown);
            this.cmbBasedOn.Leave += new System.EventHandler(this.cmbBasedOn_Leave);
            // 
            // txtNoOfVouchers
            // 
            this.txtNoOfVouchers.IntValue = 0;
            this.txtNoOfVouchers.Location = new System.Drawing.Point(158, 129);
            this.txtNoOfVouchers.MaxLength = 1;
            this.txtNoOfVouchers.Name = "txtNoOfVouchers";
            this.txtNoOfVouchers.Size = new System.Drawing.Size(43, 21);
            this.txtNoOfVouchers.TabIndex = 10;
            this.txtNoOfVouchers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfVouchers_KeyDown);
            // 
            // lblNoOfVouchers
            // 
            this.lblNoOfVouchers.AutoSize = true;
            this.lblNoOfVouchers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVouchers.Location = new System.Drawing.Point(6, 132);
            this.lblNoOfVouchers.Name = "lblNoOfVouchers";
            this.lblNoOfVouchers.Size = new System.Drawing.Size(151, 13);
            this.lblNoOfVouchers.TabIndex = 59;
            this.lblNoOfVouchers.Text = "No of Vouchers on Book*";
            // 
            // txtBookPrefix
            // 
            this.txtBookPrefix.IsAutoComplete = false;
            this.txtBookPrefix.ItemCollection = null;
            this.txtBookPrefix.Location = new System.Drawing.Point(504, 129);
            this.txtBookPrefix.MasterCode = "";
            this.txtBookPrefix.MaxLength = 2;
            this.txtBookPrefix.Name = "txtBookPrefix";
            this.txtBookPrefix.Size = new System.Drawing.Size(38, 21);
            this.txtBookPrefix.TabIndex = 8;
            this.txtBookPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookPrefix_KeyDown);
            this.txtBookPrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBookPrefix_KeyPress);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(567, 130);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 20;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(312, 56);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationBook
            // 
            this.chkAutoCompleationBook.AutoSize = true;
            this.chkAutoCompleationBook.Checked = true;
            this.chkAutoCompleationBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBook.Location = new System.Drawing.Point(141, 60);
            this.chkAutoCompleationBook.Name = "chkAutoCompleationBook";
            this.chkAutoCompleationBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBook.TabIndex = 19;
            this.chkAutoCompleationBook.Tag = "1";
            this.chkAutoCompleationBook.UseVisualStyleBackColor = true;
            // 
            // txtBookName
            // 
            this.txtBookName.Location = new System.Drawing.Point(158, 81);
            this.txtBookName.MasterDescription = "";
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(488, 21);
            this.txtBookName.TabIndex = 5;
            this.txtBookName.Tag = "";
            this.txtBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookName_KeyDown);
            this.txtBookName.Leave += new System.EventHandler(this.txtBookName_Leave);
            // 
            // txtBookCode
            // 
            this.txtBookCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCode.IsAutoComplete = false;
            this.txtBookCode.ItemCollection = null;
            this.txtBookCode.Location = new System.Drawing.Point(158, 57);
            this.txtBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBookCode.MasterCode = "";
            this.txtBookCode.MaxLength = 25;
            this.txtBookCode.Name = "txtBookCode";
            this.txtBookCode.Size = new System.Drawing.Size(153, 21);
            this.txtBookCode.TabIndex = 3;
            this.txtBookCode.Tag = "";
            this.txtBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookCode_KeyDown);
            this.txtBookCode.Leave += new System.EventHandler(this.txtBookCode_Leave);
            this.txtBookCode.Validated += new System.EventHandler(this.txtBookCode_Validated);
            // 
            // lblBookCode
            // 
            this.lblBookCode.AutoSize = true;
            this.lblBookCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookCode.Location = new System.Drawing.Point(6, 59);
            this.lblBookCode.Name = "lblBookCode";
            this.lblBookCode.Size = new System.Drawing.Size(77, 13);
            this.lblBookCode.TabIndex = 53;
            this.lblBookCode.Text = "Book Code*";
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookName.Location = new System.Drawing.Point(6, 84);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(80, 13);
            this.lblBookName.TabIndex = 54;
            this.lblBookName.Text = "Book Name*";
            // 
            // txtLength
            // 
            this.txtLength.IntValue = 0;
            this.txtLength.Location = new System.Drawing.Point(504, 106);
            this.txtLength.MaxLength = 2;
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(38, 21);
            this.txtLength.TabIndex = 9;
            this.txtLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLength_KeyDown);
            this.txtLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            // 
            // lblSerialLength
            // 
            this.lblSerialLength.AutoSize = true;
            this.lblSerialLength.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialLength.Location = new System.Drawing.Point(351, 109);
            this.lblSerialLength.Name = "lblSerialLength";
            this.lblSerialLength.Size = new System.Drawing.Size(89, 13);
            this.lblSerialLength.TabIndex = 51;
            this.lblSerialLength.Text = "Serial Length*";
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AutoSize = true;
            this.lblStartingNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartingNo.Location = new System.Drawing.Point(6, 108);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(78, 13);
            this.lblStartingNo.TabIndex = 49;
            this.lblStartingNo.Text = "Starting No*";
            // 
            // lblVoucherPrefix
            // 
            this.lblVoucherPrefix.AutoSize = true;
            this.lblVoucherPrefix.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherPrefix.Location = new System.Drawing.Point(351, 131);
            this.lblVoucherPrefix.Name = "lblVoucherPrefix";
            this.lblVoucherPrefix.Size = new System.Drawing.Size(73, 13);
            this.lblVoucherPrefix.TabIndex = 47;
            this.lblVoucherPrefix.Text = "Book Prefix";
            // 
            // txtStartingNo
            // 
            this.txtStartingNo.Location = new System.Drawing.Point(158, 105);
            this.txtStartingNo.Name = "txtStartingNo";
            this.txtStartingNo.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtStartingNo.Size = new System.Drawing.Size(100, 21);
            this.txtStartingNo.TabIndex = 7;
            this.txtStartingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartingNo_KeyDown);
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.IsAutoComplete = false;
            this.txtGroupCode.ItemCollection = null;
            this.txtGroupCode.Location = new System.Drawing.Point(158, 11);
            this.txtGroupCode.MasterCode = "";
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(153, 21);
            this.txtGroupCode.TabIndex = 1;
            this.txtGroupCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupCode_KeyDown);
            this.txtGroupCode.Leave += new System.EventHandler(this.txtGroupCode_Leave);
            // 
            // chkAutoCompleationGroup
            // 
            this.chkAutoCompleationGroup.AutoSize = true;
            this.chkAutoCompleationGroup.Checked = true;
            this.chkAutoCompleationGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGroup.Location = new System.Drawing.Point(141, 14);
            this.chkAutoCompleationGroup.Name = "chkAutoCompleationGroup";
            this.chkAutoCompleationGroup.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGroup.TabIndex = 18;
            this.chkAutoCompleationGroup.Tag = "1";
            this.chkAutoCompleationGroup.UseVisualStyleBackColor = true;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(313, 11);
            this.txtGroupName.MasterDescription = "";
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(334, 21);
            this.txtGroupName.TabIndex = 2;
            this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // lblGiftVoucherGroup
            // 
            this.lblGiftVoucherGroup.AutoSize = true;
            this.lblGiftVoucherGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherGroup.Location = new System.Drawing.Point(6, 14);
            this.lblGiftVoucherGroup.Name = "lblGiftVoucherGroup";
            this.lblGiftVoucherGroup.Size = new System.Drawing.Size(123, 13);
            this.lblGiftVoucherGroup.TabIndex = 9;
            this.lblGiftVoucherGroup.Text = "Gift Voucher Group*";
            // 
            // lblGiftVoucherValue
            // 
            this.lblGiftVoucherValue.AutoSize = true;
            this.lblGiftVoucherValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiftVoucherValue.Location = new System.Drawing.Point(8, 20);
            this.lblGiftVoucherValue.Name = "lblGiftVoucherValue";
            this.lblGiftVoucherValue.Size = new System.Drawing.Size(119, 13);
            this.lblGiftVoucherValue.TabIndex = 48;
            this.lblGiftVoucherValue.Text = "Gift Voucher Value*";
            // 
            // txtGiftVoucherValue
            // 
            this.txtGiftVoucherValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftVoucherValue.Location = new System.Drawing.Point(158, 17);
            this.txtGiftVoucherValue.Name = "txtGiftVoucherValue";
            this.txtGiftVoucherValue.Size = new System.Drawing.Size(130, 21);
            this.txtGiftVoucherValue.TabIndex = 6;
            this.txtGiftVoucherValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGiftVoucherValue_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoCoupon);
            this.groupBox2.Controls.Add(this.rdoVoucher);
            this.groupBox2.Location = new System.Drawing.Point(2, -4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 37);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
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
            // grpVoucher
            // 
            this.grpVoucher.Controls.Add(this.txtGiftVoucherValue);
            this.grpVoucher.Controls.Add(this.lblGiftVoucherValue);
            this.grpVoucher.Location = new System.Drawing.Point(2, 177);
            this.grpVoucher.Name = "grpVoucher";
            this.grpVoucher.Size = new System.Drawing.Size(658, 44);
            this.grpVoucher.TabIndex = 15;
            this.grpVoucher.TabStop = false;
            // 
            // txtValidityPeriod
            // 
            this.txtValidityPeriod.IntValue = 0;
            this.txtValidityPeriod.Location = new System.Drawing.Point(158, 13);
            this.txtValidityPeriod.Name = "txtValidityPeriod";
            this.txtValidityPeriod.Size = new System.Drawing.Size(130, 21);
            this.txtValidityPeriod.TabIndex = 101;
            this.txtValidityPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValidityPeriod_KeyDown);
            // 
            // lblValidityPeriod
            // 
            this.lblValidityPeriod.AutoSize = true;
            this.lblValidityPeriod.Location = new System.Drawing.Point(8, 16);
            this.lblValidityPeriod.Name = "lblValidityPeriod";
            this.lblValidityPeriod.Size = new System.Drawing.Size(131, 13);
            this.lblValidityPeriod.TabIndex = 100;
            this.lblValidityPeriod.Text = "Validity Period (Days)";
            // 
            // grpCoupon
            // 
            this.grpCoupon.Controls.Add(this.label7);
            this.grpCoupon.Controls.Add(this.txtPercentageOfCoupon);
            this.grpCoupon.Controls.Add(this.lblPercentageOfCoupon);
            this.grpCoupon.Controls.Add(this.txtValidityPeriod);
            this.grpCoupon.Controls.Add(this.lblValidityPeriod);
            this.grpCoupon.Location = new System.Drawing.Point(2, 216);
            this.grpCoupon.Name = "grpCoupon";
            this.grpCoupon.Size = new System.Drawing.Size(658, 40);
            this.grpCoupon.TabIndex = 16;
            this.grpCoupon.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(589, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 104;
            this.label7.Text = "%";
            // 
            // txtPercentageOfCoupon
            // 
            this.txtPercentageOfCoupon.ForeColor = System.Drawing.Color.Red;
            this.txtPercentageOfCoupon.Location = new System.Drawing.Point(504, 13);
            this.txtPercentageOfCoupon.Name = "txtPercentageOfCoupon";
            this.txtPercentageOfCoupon.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPercentageOfCoupon.Size = new System.Drawing.Size(79, 21);
            this.txtPercentageOfCoupon.TabIndex = 102;
            // 
            // lblPercentageOfCoupon
            // 
            this.lblPercentageOfCoupon.AutoSize = true;
            this.lblPercentageOfCoupon.Location = new System.Drawing.Point(351, 16);
            this.lblPercentageOfCoupon.Name = "lblPercentageOfCoupon";
            this.lblPercentageOfCoupon.Size = new System.Drawing.Size(143, 13);
            this.lblPercentageOfCoupon.TabIndex = 103;
            this.lblPercentageOfCoupon.Text = "Percentage Of Coupon*";
            // 
            // FrmGiftVoucherBookCodeGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(662, 300);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpVoucher);
            this.Controls.Add(this.grpCoupon);
            this.Name = "FrmGiftVoucherBookCodeGeneration";
            this.Text = "Gift Voucher Book Code Generation";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpCoupon, 0);
            this.Controls.SetChildIndex(this.grpVoucher, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpVoucher.ResumeLayout(false);
            this.grpVoucher.PerformLayout();
            this.grpCoupon.ResumeLayout(false);
            this.grpCoupon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxMasterCode txtGroupCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationGroup;
        private CustomControls.TextBoxMasterDescription txtGroupName;
        private System.Windows.Forms.Label lblGiftVoucherGroup;
        private CustomControls.TextBoxCurrency txtGiftVoucherValue;
        private CustomControls.TextBoxNumeric txtStartingNo;
        private System.Windows.Forms.Label lblGiftVoucherValue;
        private CustomControls.TextBoxMasterCode txtBookPrefix;
        private System.Windows.Forms.Label lblVoucherPrefix;
        private System.Windows.Forms.Label lblStartingNo;
        private CustomControls.TextBoxInteger txtLength;
        private System.Windows.Forms.Label lblSerialLength;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationBook;
        private CustomControls.TextBoxMasterDescription txtBookName;
        private CustomControls.TextBoxMasterCode txtBookCode;
        private System.Windows.Forms.Label lblBookCode;
        private System.Windows.Forms.Label lblBookName;
        private CustomControls.TextBoxInteger txtNoOfVouchers;
        private System.Windows.Forms.Label lblNoOfVouchers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoCoupon;
        private System.Windows.Forms.RadioButton rdoVoucher;
        private System.Windows.Forms.GroupBox grpVoucher;
        private CustomControls.TextBoxInteger txtValidityPeriod;
        private System.Windows.Forms.Label lblValidityPeriod;
        private System.Windows.Forms.GroupBox grpCoupon;
        private System.Windows.Forms.Label label7;
        private CustomControls.TextBoxPercentGen txtPercentageOfCoupon;
        private System.Windows.Forms.Label lblPercentageOfCoupon;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.ComboBox cmbBasedOn;
    }
}
