namespace UI.Windows
{
    partial class FrmCardNoGeneration
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
            this.gbEncodeSetting = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEncodePrefix = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtEncodeLength = new UI.Windows.CustomControls.TextBoxInteger();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEncodeFormat = new System.Windows.Forms.TextBox();
            this.txtEncodeStartingNo = new UI.Windows.CustomControls.TextBoxInteger();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.gbSerial = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSerialPrefix = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSerialLength = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtSerialFormat = new System.Windows.Forms.TextBox();
            this.txtSerialStartingNo = new UI.Windows.CustomControls.TextBoxInteger();
            this.label1 = new System.Windows.Forms.Label();
            this.gbCardNo = new System.Windows.Forms.GroupBox();
            this.txtCardPrefix = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.txtCardLength = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtCardFormat = new System.Windows.Forms.TextBox();
            this.lblStartingNo = new System.Windows.Forms.Label();
            this.txtCardStartingNo = new UI.Windows.CustomControls.TextBoxInteger();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chkAutoCompleationCard = new System.Windows.Forms.CheckBox();
            this.txtName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.serialNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EncodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSerialExists = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.loyaltyCardGenerationDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblQty = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbEncodeSetting.SuspendLayout();
            this.gbSerial.SuspendLayout();
            this.gbCardNo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loyaltyCardGenerationDetailTempBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 484);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(352, 484);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbEncodeSetting);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.gbSerial);
            this.groupBox1.Controls.Add(this.gbCardNo);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(589, 192);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // gbEncodeSetting
            // 
            this.gbEncodeSetting.Controls.Add(this.label5);
            this.gbEncodeSetting.Controls.Add(this.label6);
            this.gbEncodeSetting.Controls.Add(this.txtEncodePrefix);
            this.gbEncodeSetting.Controls.Add(this.txtEncodeLength);
            this.gbEncodeSetting.Controls.Add(this.label7);
            this.gbEncodeSetting.Controls.Add(this.txtEncodeFormat);
            this.gbEncodeSetting.Controls.Add(this.txtEncodeStartingNo);
            this.gbEncodeSetting.Controls.Add(this.label8);
            this.gbEncodeSetting.Enabled = false;
            this.gbEncodeSetting.Location = new System.Drawing.Point(4, 139);
            this.gbEncodeSetting.Name = "gbEncodeSetting";
            this.gbEncodeSetting.Size = new System.Drawing.Size(581, 49);
            this.gbEncodeSetting.TabIndex = 73;
            this.gbEncodeSetting.TabStop = false;
            this.gbEncodeSetting.Text = "Encode No Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(120, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Length";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(389, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Format";
            // 
            // txtEncodePrefix
            // 
            this.txtEncodePrefix.IsAutoComplete = false;
            this.txtEncodePrefix.ItemCollection = null;
            this.txtEncodePrefix.Location = new System.Drawing.Point(55, 21);
            this.txtEncodePrefix.MasterCode = "";
            this.txtEncodePrefix.MaxLength = 3;
            this.txtEncodePrefix.Name = "txtEncodePrefix";
            this.txtEncodePrefix.ReadOnly = true;
            this.txtEncodePrefix.Size = new System.Drawing.Size(49, 21);
            this.txtEncodePrefix.TabIndex = 70;
            this.txtEncodePrefix.TextChanged += new System.EventHandler(this.txtEncodePrefix_TextChanged);
            this.txtEncodePrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEncodePrefix_KeyDown);
            // 
            // txtEncodeLength
            // 
            this.txtEncodeLength.IntValue = 0;
            this.txtEncodeLength.Location = new System.Drawing.Point(171, 21);
            this.txtEncodeLength.Name = "txtEncodeLength";
            this.txtEncodeLength.ReadOnly = true;
            this.txtEncodeLength.Size = new System.Drawing.Size(52, 21);
            this.txtEncodeLength.TabIndex = 65;
            this.txtEncodeLength.TextChanged += new System.EventHandler(this.txtEncodeLength_TextChanged);
            this.txtEncodeLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEncodeLength_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Prefix";
            // 
            // txtEncodeFormat
            // 
            this.txtEncodeFormat.Location = new System.Drawing.Point(451, 21);
            this.txtEncodeFormat.Name = "txtEncodeFormat";
            this.txtEncodeFormat.ReadOnly = true;
            this.txtEncodeFormat.Size = new System.Drawing.Size(125, 21);
            this.txtEncodeFormat.TabIndex = 66;
            // 
            // txtEncodeStartingNo
            // 
            this.txtEncodeStartingNo.IntValue = 0;
            this.txtEncodeStartingNo.Location = new System.Drawing.Point(317, 21);
            this.txtEncodeStartingNo.Name = "txtEncodeStartingNo";
            this.txtEncodeStartingNo.ReadOnly = true;
            this.txtEncodeStartingNo.Size = new System.Drawing.Size(55, 21);
            this.txtEncodeStartingNo.TabIndex = 67;
            this.txtEncodeStartingNo.TextChanged += new System.EventHandler(this.txtEncodeStartingNo_TextChanged);
            this.txtEncodeStartingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEncodeStartingNo_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(236, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "Starting No*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(301, 12);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(282, 21);
            this.cmbLocation.TabIndex = 61;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            // 
            // gbSerial
            // 
            this.gbSerial.Controls.Add(this.label3);
            this.gbSerial.Controls.Add(this.label2);
            this.gbSerial.Controls.Add(this.txtSerialPrefix);
            this.gbSerial.Controls.Add(this.txtSerialLength);
            this.gbSerial.Controls.Add(this.lblPrefix);
            this.gbSerial.Controls.Add(this.txtSerialFormat);
            this.gbSerial.Controls.Add(this.txtSerialStartingNo);
            this.gbSerial.Controls.Add(this.label1);
            this.gbSerial.Location = new System.Drawing.Point(4, 90);
            this.gbSerial.Name = "gbSerial";
            this.gbSerial.Size = new System.Drawing.Size(581, 47);
            this.gbSerial.TabIndex = 72;
            this.gbSerial.TabStop = false;
            this.gbSerial.Text = "Serial No Setting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(120, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Length";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(389, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Format";
            // 
            // txtSerialPrefix
            // 
            this.txtSerialPrefix.IsAutoComplete = false;
            this.txtSerialPrefix.ItemCollection = null;
            this.txtSerialPrefix.Location = new System.Drawing.Point(55, 19);
            this.txtSerialPrefix.MasterCode = "";
            this.txtSerialPrefix.MaxLength = 3;
            this.txtSerialPrefix.Name = "txtSerialPrefix";
            this.txtSerialPrefix.ReadOnly = true;
            this.txtSerialPrefix.Size = new System.Drawing.Size(49, 21);
            this.txtSerialPrefix.TabIndex = 70;
            this.txtSerialPrefix.TextChanged += new System.EventHandler(this.txtSerialPrefix_TextChanged);
            this.txtSerialPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialPrefix_KeyDown);
            // 
            // txtSerialLength
            // 
            this.txtSerialLength.IntValue = 0;
            this.txtSerialLength.Location = new System.Drawing.Point(171, 19);
            this.txtSerialLength.Name = "txtSerialLength";
            this.txtSerialLength.ReadOnly = true;
            this.txtSerialLength.Size = new System.Drawing.Size(52, 21);
            this.txtSerialLength.TabIndex = 65;
            this.txtSerialLength.TextChanged += new System.EventHandler(this.txtSerialLength_TextChanged);
            this.txtSerialLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialLength_KeyDown);
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrefix.Location = new System.Drawing.Point(11, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(40, 13);
            this.lblPrefix.TabIndex = 69;
            this.lblPrefix.Text = "Prefix";
            // 
            // txtSerialFormat
            // 
            this.txtSerialFormat.Location = new System.Drawing.Point(451, 19);
            this.txtSerialFormat.Name = "txtSerialFormat";
            this.txtSerialFormat.ReadOnly = true;
            this.txtSerialFormat.Size = new System.Drawing.Size(126, 21);
            this.txtSerialFormat.TabIndex = 66;
            // 
            // txtSerialStartingNo
            // 
            this.txtSerialStartingNo.IntValue = 0;
            this.txtSerialStartingNo.Location = new System.Drawing.Point(317, 19);
            this.txtSerialStartingNo.Name = "txtSerialStartingNo";
            this.txtSerialStartingNo.ReadOnly = true;
            this.txtSerialStartingNo.Size = new System.Drawing.Size(55, 21);
            this.txtSerialStartingNo.TabIndex = 67;
            this.txtSerialStartingNo.TextChanged += new System.EventHandler(this.txtSerialStartingNo_TextChanged);
            this.txtSerialStartingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialStartingNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(236, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Starting No*";
            // 
            // gbCardNo
            // 
            this.gbCardNo.Controls.Add(this.txtCardPrefix);
            this.gbCardNo.Controls.Add(this.label4);
            this.gbCardNo.Controls.Add(this.lblLength);
            this.gbCardNo.Controls.Add(this.lblFormat);
            this.gbCardNo.Controls.Add(this.txtCardLength);
            this.gbCardNo.Controls.Add(this.txtCardFormat);
            this.gbCardNo.Controls.Add(this.lblStartingNo);
            this.gbCardNo.Controls.Add(this.txtCardStartingNo);
            this.gbCardNo.Location = new System.Drawing.Point(4, 40);
            this.gbCardNo.Name = "gbCardNo";
            this.gbCardNo.Size = new System.Drawing.Size(581, 47);
            this.gbCardNo.TabIndex = 71;
            this.gbCardNo.TabStop = false;
            this.gbCardNo.Text = "Card No Setting";
            // 
            // txtCardPrefix
            // 
            this.txtCardPrefix.IsAutoComplete = false;
            this.txtCardPrefix.ItemCollection = null;
            this.txtCardPrefix.Location = new System.Drawing.Point(55, 18);
            this.txtCardPrefix.MasterCode = "";
            this.txtCardPrefix.MaxLength = 3;
            this.txtCardPrefix.Name = "txtCardPrefix";
            this.txtCardPrefix.ReadOnly = true;
            this.txtCardPrefix.Size = new System.Drawing.Size(49, 21);
            this.txtCardPrefix.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Prefix";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLength.Location = new System.Drawing.Point(120, 22);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(45, 13);
            this.lblLength.TabIndex = 20;
            this.lblLength.Text = "Length";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormat.Location = new System.Drawing.Point(389, 22);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(47, 13);
            this.lblFormat.TabIndex = 24;
            this.lblFormat.Text = "Format";
            // 
            // txtCardLength
            // 
            this.txtCardLength.IntValue = 0;
            this.txtCardLength.Location = new System.Drawing.Point(171, 18);
            this.txtCardLength.Name = "txtCardLength";
            this.txtCardLength.ReadOnly = true;
            this.txtCardLength.Size = new System.Drawing.Size(52, 21);
            this.txtCardLength.TabIndex = 25;
            // 
            // txtCardFormat
            // 
            this.txtCardFormat.Location = new System.Drawing.Point(451, 18);
            this.txtCardFormat.Name = "txtCardFormat";
            this.txtCardFormat.ReadOnly = true;
            this.txtCardFormat.Size = new System.Drawing.Size(126, 21);
            this.txtCardFormat.TabIndex = 27;
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AutoSize = true;
            this.lblStartingNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartingNo.Location = new System.Drawing.Point(236, 22);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(78, 13);
            this.lblStartingNo.TabIndex = 36;
            this.lblStartingNo.Text = "Starting No*";
            // 
            // txtCardStartingNo
            // 
            this.txtCardStartingNo.IntValue = 0;
            this.txtCardStartingNo.Location = new System.Drawing.Point(317, 18);
            this.txtCardStartingNo.Name = "txtCardStartingNo";
            this.txtCardStartingNo.ReadOnly = true;
            this.txtCardStartingNo.Size = new System.Drawing.Size(55, 21);
            this.txtCardStartingNo.TabIndex = 35;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(123, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 21);
            this.dtpDate.TabIndex = 22;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(6, 16);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(98, 13);
            this.lblDate.TabIndex = 21;
            this.lblDate.Text = "Generated Date";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(241, 16);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(30, 11);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(66, 13);
            this.lblCode.TabIndex = 17;
            this.lblCode.Text = "Card Type";
            // 
            // txtCode
            // 
            this.txtCode.IsAutoComplete = false;
            this.txtCode.ItemCollection = null;
            this.txtCode.Location = new System.Drawing.Point(30, 27);
            this.txtCode.MasterCode = "";
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(109, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(509, 26);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(73, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // chkAutoCompleationCard
            // 
            this.chkAutoCompleationCard.AutoSize = true;
            this.chkAutoCompleationCard.Checked = true;
            this.chkAutoCompleationCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCard.Location = new System.Drawing.Point(10, 30);
            this.chkAutoCompleationCard.Name = "chkAutoCompleationCard";
            this.chkAutoCompleationCard.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCard.TabIndex = 14;
            this.chkAutoCompleationCard.Tag = "1";
            this.chkAutoCompleationCard.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(141, 27);
            this.txtName.MasterDescription = "";
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 21);
            this.txtName.TabIndex = 2;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtQty);
            this.groupBox2.Controls.Add(this.dgvDisplay);
            this.groupBox2.Controls.Add(this.lblQty);
            this.groupBox2.Controls.Add(this.txtCode);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.lblCode);
            this.groupBox2.Controls.Add(this.chkAutoCompleationCard);
            this.groupBox2.Controls.Add(this.btnGenerate);
            this.groupBox2.Location = new System.Drawing.Point(2, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 308);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(425, 27);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(83, 21);
            this.txtQty.TabIndex = 22;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.AllowUserToAddRows = false;
            this.dgvDisplay.AutoGenerateColumns = false;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialNoDataGridViewTextBoxColumn,
            this.SerialNo,
            this.EncodeNo,
            this.IsSerialExists});
            this.dgvDisplay.DataSource = this.loyaltyCardGenerationDetailTempBindingSource;
            this.dgvDisplay.Location = new System.Drawing.Point(9, 54);
            this.dgvDisplay.MultiSelect = false;
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.ReadOnly = true;
            this.dgvDisplay.RowHeadersWidth = 20;
            this.dgvDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisplay.Size = new System.Drawing.Size(573, 248);
            this.dgvDisplay.TabIndex = 21;
            this.dgvDisplay.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDisplay_CellFormatting);
            // 
            // serialNoDataGridViewTextBoxColumn
            // 
            this.serialNoDataGridViewTextBoxColumn.DataPropertyName = "CardNo";
            this.serialNoDataGridViewTextBoxColumn.HeaderText = "CardNo";
            this.serialNoDataGridViewTextBoxColumn.Name = "serialNoDataGridViewTextBoxColumn";
            this.serialNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.serialNoDataGridViewTextBoxColumn.Width = 150;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "SerialNo";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            this.SerialNo.Width = 150;
            // 
            // EncodeNo
            // 
            this.EncodeNo.DataPropertyName = "EncodeNo";
            this.EncodeNo.HeaderText = "Encode No";
            this.EncodeNo.Name = "EncodeNo";
            this.EncodeNo.ReadOnly = true;
            this.EncodeNo.Width = 150;
            // 
            // IsSerialExists
            // 
            this.IsSerialExists.DataPropertyName = "IsSerialExists";
            this.IsSerialExists.HeaderText = "IsSerialExists";
            this.IsSerialExists.Name = "IsSerialExists";
            this.IsSerialExists.ReadOnly = true;
            this.IsSerialExists.Visible = false;
            // 
            // loyaltyCardGenerationDetailTempBindingSource
            // 
            this.loyaltyCardGenerationDetailTempBindingSource.DataSource = typeof(Domain.LoyaltyCardGenerationDetailTemp);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(429, 11);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(27, 13);
            this.lblQty.TabIndex = 20;
            this.lblQty.Text = "Qty";
            // 
            // FrmCardNoGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(593, 533);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmCardNoGeneration";
            this.Text = "Card Number Generation";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEncodeSetting.ResumeLayout(false);
            this.gbEncodeSetting.PerformLayout();
            this.gbSerial.ResumeLayout(false);
            this.gbSerial.PerformLayout();
            this.gbCardNo.ResumeLayout(false);
            this.gbCardNo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loyaltyCardGenerationDetailTempBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblCode;
        private CustomControls.TextBoxMasterCode txtCode;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckBox chkAutoCompleationCard;
        private CustomControls.TextBoxMasterDescription txtName;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.Label lblQty;
        private CustomControls.TextBoxQty txtQty;
        private CustomControls.TextBoxInteger txtCardLength;
        private System.Windows.Forms.TextBox txtCardFormat;
        private CustomControls.TextBoxInteger txtCardStartingNo;
        private System.Windows.Forms.Label lblStartingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardTypeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDeleteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource loyaltyCardGenerationDetailTempBindingSource;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private CustomControls.TextBoxInteger txtSerialStartingNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerialFormat;
        private CustomControls.TextBoxInteger txtSerialLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxMasterCode txtSerialPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.GroupBox gbSerial;
        private System.Windows.Forms.GroupBox gbCardNo;
        private CustomControls.TextBoxMasterCode txtCardPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbEncodeSetting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CustomControls.TextBoxMasterCode txtEncodePrefix;
        private CustomControls.TextBoxInteger txtEncodeLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEncodeFormat;
        private CustomControls.TextBoxInteger txtEncodeStartingNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EncodeNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSerialExists;
    }
}
