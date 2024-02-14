namespace UI.Windows
{
    partial class FrmLoanEntry
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
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtpGrnDate = new System.Windows.Forms.DateTimePicker();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationPurpose = new System.Windows.Forms.CheckBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationLoanType = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProvider = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblManualNo = new System.Windows.Forms.Label();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.lblLoanType = new System.Windows.Forms.Label();
            this.lblFinanceInstitut = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtEndMonth = new UI.Windows.CustomControls.TextBoxInteger();
            this.txtBeginMonth = new UI.Windows.CustomControls.TextBoxInteger();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbLoan = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMonthlyGracePeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblMonthlyGracePeriod = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblGraceRate = new System.Windows.Forms.Label();
            this.txtGraceRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtInterestRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtGracePeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblPlus = new System.Windows.Forms.Label();
            this.txtDownPayment = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtLoanAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtAssetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.dtpFirstDueDate = new System.Windows.Forms.DateTimePicker();
            this.txtLoanPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.cmbLoanTerm = new System.Windows.Forms.ComboBox();
            this.lblGracePeriod = new System.Windows.Forms.Label();
            this.lblDownPayment = new System.Windows.Forms.Label();
            this.lblInterestRate = new System.Windows.Forms.Label();
            this.lblNoOfDownPayment = new System.Windows.Forms.Label();
            this.lblLoanAmount = new System.Windows.Forms.Label();
            this.lblAssetAmount = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblLoanPeriod = new System.Windows.Forms.Label();
            this.lblLoanTerm = new System.Windows.Forms.Label();
            this.tbpSchedule = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpCharges = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtOtherCharge = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtServiceCharge = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblOtherCharge = new System.Windows.Forms.Label();
            this.lblServiceCharge = new System.Windows.Forms.Label();
            this.tbpNotifications = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtBeforeNoOfDays = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblBeforeNoOfDays = new System.Windows.Forms.Label();
            this.chkUserPopups = new System.Windows.Forms.CheckBox();
            this.chkSms = new System.Windows.Forms.CheckBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.tbpMortgage = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtMortgageNo = new System.Windows.Forms.TextBox();
            this.lblMortgageNo = new System.Windows.Forms.Label();
            this.txtMortgageInterestRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.lblMortgageInterest = new System.Windows.Forms.Label();
            this.dtpMortgageDate = new System.Windows.Forms.DateTimePicker();
            this.lblMortgageDate = new System.Windows.Forms.Label();
            this.chkMortgage = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDisplayGrossInstallment = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtDisplayNetInstallment = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtDisplayMaturityDate = new System.Windows.Forms.TextBox();
            this.txtDisplayDownPayment = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtDisplayTaxCharges = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtDisplayEffectiveRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtDisplayInterestRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.txtDisplayGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtDisplayNetAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblDisplayEffectiveRate = new System.Windows.Forms.Label();
            this.lblDisplayInterestRate = new System.Windows.Forms.Label();
            this.lblDisplayTaxRate = new System.Windows.Forms.Label();
            this.lblDisplayGrossAmount = new System.Windows.Forms.Label();
            this.lblDisplayNetAmount = new System.Windows.Forms.Label();
            this.lblDisplayMaturityDate = new System.Windows.Forms.Label();
            this.lblDisplayDownPayment = new System.Windows.Forms.Label();
            this.lblDisplayGrossInstallment = new System.Windows.Forms.Label();
            this.lblDisplayNetInstallment = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tbLoan.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbpSchedule.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.tbpCharges.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tbpNotifications.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tbpMortgage.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(665, 372);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 372);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCostCentre);
            this.groupBox1.Controls.Add(this.lblCostCentre);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.dtpGrnDate);
            this.groupBox1.Controls.Add(this.lblEntryDate);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.txtReferenceNo);
            this.groupBox1.Controls.Add(this.lblReferenceNo);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.chkAutoCompleationPurpose);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.chkAutoCompleationLoanType);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btnDocumentDetails);
            this.groupBox1.Controls.Add(this.txtSupplierName);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProvider);
            this.groupBox1.Controls.Add(this.txtSupplierCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.groupBox1.Controls.Add(this.txtDocumentNo);
            this.groupBox1.Controls.Add(this.lblDocumentNo);
            this.groupBox1.Controls.Add(this.lblPaymentMethod);
            this.groupBox1.Controls.Add(this.lblManualNo);
            this.groupBox1.Controls.Add(this.lblPurpose);
            this.groupBox1.Controls.Add(this.lblLoanType);
            this.groupBox1.Controls.Add(this.lblFinanceInstitut);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 138);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(802, 111);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(171, 21);
            this.cmbCostCentre.TabIndex = 69;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(683, 115);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 68;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(802, 87);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(171, 21);
            this.cmbLocation.TabIndex = 65;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(683, 90);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 64;
            this.lblLocation.Text = "Location";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(802, 63);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(171, 21);
            this.comboBox1.TabIndex = 45;
            // 
            // dtpGrnDate
            // 
            this.dtpGrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGrnDate.Location = new System.Drawing.Point(802, 15);
            this.dtpGrnDate.Name = "dtpGrnDate";
            this.dtpGrnDate.Size = new System.Drawing.Size(171, 21);
            this.dtpGrnDate.TabIndex = 44;
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Location = new System.Drawing.Point(683, 19);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(68, 13);
            this.lblEntryDate.TabIndex = 43;
            this.lblEntryDate.Text = "Entry Date";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(444, 15);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(222, 21);
            this.textBox5.TabIndex = 41;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(802, 39);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(171, 21);
            this.txtReferenceNo.TabIndex = 40;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(683, 44);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 39;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 114);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 38;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(140, 111);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(526, 21);
            this.txtRemark.TabIndex = 37;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(278, 86);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(388, 21);
            this.textBox3.TabIndex = 36;
            // 
            // chkAutoCompleationPurpose
            // 
            this.chkAutoCompleationPurpose.AutoSize = true;
            this.chkAutoCompleationPurpose.Checked = true;
            this.chkAutoCompleationPurpose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPurpose.Location = new System.Drawing.Point(123, 89);
            this.chkAutoCompleationPurpose.Name = "chkAutoCompleationPurpose";
            this.chkAutoCompleationPurpose.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPurpose.TabIndex = 35;
            this.chkAutoCompleationPurpose.Tag = "1";
            this.chkAutoCompleationPurpose.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(140, 86);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(136, 21);
            this.textBox4.TabIndex = 34;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(278, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(388, 21);
            this.textBox1.TabIndex = 32;
            // 
            // chkAutoCompleationLoanType
            // 
            this.chkAutoCompleationLoanType.AutoSize = true;
            this.chkAutoCompleationLoanType.Checked = true;
            this.chkAutoCompleationLoanType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLoanType.Location = new System.Drawing.Point(123, 65);
            this.chkAutoCompleationLoanType.Name = "chkAutoCompleationLoanType";
            this.chkAutoCompleationLoanType.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLoanType.TabIndex = 31;
            this.chkAutoCompleationLoanType.Tag = "1";
            this.chkAutoCompleationLoanType.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(140, 62);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(136, 21);
            this.textBox2.TabIndex = 30;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(277, 13);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 29;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(278, 38);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(388, 21);
            this.txtSupplierName.TabIndex = 27;
            // 
            // chkAutoCompleationProvider
            // 
            this.chkAutoCompleationProvider.AutoSize = true;
            this.chkAutoCompleationProvider.Checked = true;
            this.chkAutoCompleationProvider.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProvider.Location = new System.Drawing.Point(123, 41);
            this.chkAutoCompleationProvider.Name = "chkAutoCompleationProvider";
            this.chkAutoCompleationProvider.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProvider.TabIndex = 26;
            this.chkAutoCompleationProvider.Tag = "1";
            this.chkAutoCompleationProvider.UseVisualStyleBackColor = true;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(140, 38);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 25;
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(123, 17);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 24;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(140, 14);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 23;
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 18);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 22;
            this.lblDocumentNo.Text = "Document No";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(683, 66);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(102, 13);
            this.lblPaymentMethod.TabIndex = 6;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // lblManualNo
            // 
            this.lblManualNo.AutoSize = true;
            this.lblManualNo.Location = new System.Drawing.Point(369, 18);
            this.lblManualNo.Name = "lblManualNo";
            this.lblManualNo.Size = new System.Drawing.Size(66, 13);
            this.lblManualNo.TabIndex = 4;
            this.lblManualNo.Text = "Manual No";
            // 
            // lblPurpose
            // 
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(6, 89);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(53, 13);
            this.lblPurpose.TabIndex = 2;
            this.lblPurpose.Text = "Purpose";
            // 
            // lblLoanType
            // 
            this.lblLoanType.AutoSize = true;
            this.lblLoanType.Location = new System.Drawing.Point(6, 65);
            this.lblLoanType.Name = "lblLoanType";
            this.lblLoanType.Size = new System.Drawing.Size(65, 13);
            this.lblLoanType.TabIndex = 1;
            this.lblLoanType.Text = "Loan Type";
            // 
            // lblFinanceInstitut
            // 
            this.lblFinanceInstitut.AutoSize = true;
            this.lblFinanceInstitut.Location = new System.Drawing.Point(6, 41);
            this.lblFinanceInstitut.Name = "lblFinanceInstitut";
            this.lblFinanceInstitut.Size = new System.Drawing.Size(101, 13);
            this.lblFinanceInstitut.TabIndex = 0;
            this.lblFinanceInstitut.Text = "Finance Institute";
            // 
            // txtEndMonth
            // 
            this.txtEndMonth.IntValue = 0;
            this.txtEndMonth.Location = new System.Drawing.Point(607, 69);
            this.txtEndMonth.Name = "txtEndMonth";
            this.txtEndMonth.Size = new System.Drawing.Size(37, 21);
            this.txtEndMonth.TabIndex = 96;
            this.toolTip1.SetToolTip(this.txtEndMonth, "End Of Month");
            // 
            // txtBeginMonth
            // 
            this.txtBeginMonth.IntValue = 0;
            this.txtBeginMonth.Location = new System.Drawing.Point(542, 69);
            this.txtBeginMonth.Name = "txtBeginMonth";
            this.txtBeginMonth.Size = new System.Drawing.Size(37, 21);
            this.txtBeginMonth.TabIndex = 94;
            this.toolTip1.SetToolTip(this.txtBeginMonth, "Begin Of Month");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbLoan);
            this.groupBox2.Location = new System.Drawing.Point(2, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 249);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // tbLoan
            // 
            this.tbLoan.Controls.Add(this.tbpGeneral);
            this.tbLoan.Controls.Add(this.tbpSchedule);
            this.tbLoan.Controls.Add(this.tbpCharges);
            this.tbLoan.Controls.Add(this.tbpNotifications);
            this.tbLoan.Controls.Add(this.tbpMortgage);
            this.tbLoan.Location = new System.Drawing.Point(3, 11);
            this.tbLoan.Name = "tbLoan";
            this.tbLoan.SelectedIndex = 0;
            this.tbLoan.Size = new System.Drawing.Size(669, 234);
            this.tbLoan.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.groupBox3);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(661, 208);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMonthlyGracePeriod);
            this.groupBox3.Controls.Add(this.lblMonthlyGracePeriod);
            this.groupBox3.Controls.Add(this.btnCalculate);
            this.groupBox3.Controls.Add(this.lblGraceRate);
            this.groupBox3.Controls.Add(this.txtGraceRate);
            this.groupBox3.Controls.Add(this.txtInterestRate);
            this.groupBox3.Controls.Add(this.txtGracePeriod);
            this.groupBox3.Controls.Add(this.lblPlus);
            this.groupBox3.Controls.Add(this.txtEndMonth);
            this.groupBox3.Controls.Add(this.txtDownPayment);
            this.groupBox3.Controls.Add(this.txtBeginMonth);
            this.groupBox3.Controls.Add(this.txtLoanAmount);
            this.groupBox3.Controls.Add(this.txtAssetAmount);
            this.groupBox3.Controls.Add(this.dtpFirstDueDate);
            this.groupBox3.Controls.Add(this.txtLoanPeriod);
            this.groupBox3.Controls.Add(this.cmbLoanTerm);
            this.groupBox3.Controls.Add(this.lblGracePeriod);
            this.groupBox3.Controls.Add(this.lblDownPayment);
            this.groupBox3.Controls.Add(this.lblInterestRate);
            this.groupBox3.Controls.Add(this.lblNoOfDownPayment);
            this.groupBox3.Controls.Add(this.lblLoanAmount);
            this.groupBox3.Controls.Add(this.lblAssetAmount);
            this.groupBox3.Controls.Add(this.lblDueDate);
            this.groupBox3.Controls.Add(this.lblLoanPeriod);
            this.groupBox3.Controls.Add(this.lblLoanTerm);
            this.groupBox3.Location = new System.Drawing.Point(4, -4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(653, 208);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // txtMonthlyGracePeriod
            // 
            this.txtMonthlyGracePeriod.IntValue = 0;
            this.txtMonthlyGracePeriod.Location = new System.Drawing.Point(478, 150);
            this.txtMonthlyGracePeriod.Name = "txtMonthlyGracePeriod";
            this.txtMonthlyGracePeriod.Size = new System.Drawing.Size(57, 21);
            this.txtMonthlyGracePeriod.TabIndex = 104;
            // 
            // lblMonthlyGracePeriod
            // 
            this.lblMonthlyGracePeriod.AutoSize = true;
            this.lblMonthlyGracePeriod.Location = new System.Drawing.Point(347, 153);
            this.lblMonthlyGracePeriod.Name = "lblMonthlyGracePeriod";
            this.lblMonthlyGracePeriod.Size = new System.Drawing.Size(129, 13);
            this.lblMonthlyGracePeriod.TabIndex = 103;
            this.lblMonthlyGracePeriod.Text = "Monthly Grace Period";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(542, 175);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(102, 27);
            this.btnCalculate.TabIndex = 102;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lblGraceRate
            // 
            this.lblGraceRate.AutoSize = true;
            this.lblGraceRate.Location = new System.Drawing.Point(538, 126);
            this.lblGraceRate.Name = "lblGraceRate";
            this.lblGraceRate.Size = new System.Drawing.Size(49, 13);
            this.lblGraceRate.TabIndex = 101;
            this.lblGraceRate.Text = "Rate %";
            // 
            // txtGraceRate
            // 
            this.txtGraceRate.ForeColor = System.Drawing.Color.Black;
            this.txtGraceRate.Location = new System.Drawing.Point(589, 123);
            this.txtGraceRate.Name = "txtGraceRate";
            this.txtGraceRate.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGraceRate.Size = new System.Drawing.Size(55, 21);
            this.txtGraceRate.TabIndex = 100;
            // 
            // txtInterestRate
            // 
            this.txtInterestRate.ForeColor = System.Drawing.Color.Black;
            this.txtInterestRate.Location = new System.Drawing.Point(542, 96);
            this.txtInterestRate.Name = "txtInterestRate";
            this.txtInterestRate.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtInterestRate.Size = new System.Drawing.Size(102, 21);
            this.txtInterestRate.TabIndex = 99;
            // 
            // txtGracePeriod
            // 
            this.txtGracePeriod.IntValue = 0;
            this.txtGracePeriod.Location = new System.Drawing.Point(478, 123);
            this.txtGracePeriod.Name = "txtGracePeriod";
            this.txtGracePeriod.Size = new System.Drawing.Size(57, 21);
            this.txtGracePeriod.TabIndex = 98;
            // 
            // lblPlus
            // 
            this.lblPlus.AutoSize = true;
            this.lblPlus.Location = new System.Drawing.Point(586, 72);
            this.lblPlus.Name = "lblPlus";
            this.lblPlus.Size = new System.Drawing.Size(16, 13);
            this.lblPlus.TabIndex = 97;
            this.lblPlus.Text = "+";
            // 
            // txtDownPayment
            // 
            this.txtDownPayment.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDownPayment.Location = new System.Drawing.Point(478, 42);
            this.txtDownPayment.Name = "txtDownPayment";
            this.txtDownPayment.Size = new System.Drawing.Size(166, 21);
            this.txtDownPayment.TabIndex = 95;
            // 
            // txtLoanAmount
            // 
            this.txtLoanAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLoanAmount.Location = new System.Drawing.Point(478, 15);
            this.txtLoanAmount.Name = "txtLoanAmount";
            this.txtLoanAmount.Size = new System.Drawing.Size(166, 21);
            this.txtLoanAmount.TabIndex = 93;
            // 
            // txtAssetAmount
            // 
            this.txtAssetAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAssetAmount.Location = new System.Drawing.Point(113, 96);
            this.txtAssetAmount.Name = "txtAssetAmount";
            this.txtAssetAmount.Size = new System.Drawing.Size(199, 21);
            this.txtAssetAmount.TabIndex = 92;
            // 
            // dtpFirstDueDate
            // 
            this.dtpFirstDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFirstDueDate.Location = new System.Drawing.Point(113, 69);
            this.dtpFirstDueDate.Name = "dtpFirstDueDate";
            this.dtpFirstDueDate.Size = new System.Drawing.Size(94, 21);
            this.dtpFirstDueDate.TabIndex = 91;
            // 
            // txtLoanPeriod
            // 
            this.txtLoanPeriod.IntValue = 0;
            this.txtLoanPeriod.Location = new System.Drawing.Point(113, 42);
            this.txtLoanPeriod.Name = "txtLoanPeriod";
            this.txtLoanPeriod.Size = new System.Drawing.Size(94, 21);
            this.txtLoanPeriod.TabIndex = 90;
            // 
            // cmbLoanTerm
            // 
            this.cmbLoanTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoanTerm.FormattingEnabled = true;
            this.cmbLoanTerm.Location = new System.Drawing.Point(113, 16);
            this.cmbLoanTerm.Name = "cmbLoanTerm";
            this.cmbLoanTerm.Size = new System.Drawing.Size(199, 21);
            this.cmbLoanTerm.TabIndex = 89;
            // 
            // lblGracePeriod
            // 
            this.lblGracePeriod.AutoSize = true;
            this.lblGracePeriod.Location = new System.Drawing.Point(347, 126);
            this.lblGracePeriod.Name = "lblGracePeriod";
            this.lblGracePeriod.Size = new System.Drawing.Size(81, 13);
            this.lblGracePeriod.TabIndex = 88;
            this.lblGracePeriod.Text = "Grace Period";
            // 
            // lblDownPayment
            // 
            this.lblDownPayment.AutoSize = true;
            this.lblDownPayment.Location = new System.Drawing.Point(347, 46);
            this.lblDownPayment.Name = "lblDownPayment";
            this.lblDownPayment.Size = new System.Drawing.Size(93, 13);
            this.lblDownPayment.TabIndex = 87;
            this.lblDownPayment.Text = "Down Payment";
            // 
            // lblInterestRate
            // 
            this.lblInterestRate.AutoSize = true;
            this.lblInterestRate.Location = new System.Drawing.Point(347, 99);
            this.lblInterestRate.Name = "lblInterestRate";
            this.lblInterestRate.Size = new System.Drawing.Size(98, 13);
            this.lblInterestRate.TabIndex = 86;
            this.lblInterestRate.Text = "Interest Rate %";
            // 
            // lblNoOfDownPayment
            // 
            this.lblNoOfDownPayment.AutoSize = true;
            this.lblNoOfDownPayment.Location = new System.Drawing.Point(347, 75);
            this.lblNoOfDownPayment.Name = "lblNoOfDownPayment";
            this.lblNoOfDownPayment.Size = new System.Drawing.Size(129, 13);
            this.lblNoOfDownPayment.TabIndex = 85;
            this.lblNoOfDownPayment.Text = "No Of Down Payment";
            // 
            // lblLoanAmount
            // 
            this.lblLoanAmount.AutoSize = true;
            this.lblLoanAmount.Location = new System.Drawing.Point(347, 19);
            this.lblLoanAmount.Name = "lblLoanAmount";
            this.lblLoanAmount.Size = new System.Drawing.Size(82, 13);
            this.lblLoanAmount.TabIndex = 84;
            this.lblLoanAmount.Text = "Loan Amount";
            // 
            // lblAssetAmount
            // 
            this.lblAssetAmount.AutoSize = true;
            this.lblAssetAmount.Location = new System.Drawing.Point(6, 99);
            this.lblAssetAmount.Name = "lblAssetAmount";
            this.lblAssetAmount.Size = new System.Drawing.Size(86, 13);
            this.lblAssetAmount.TabIndex = 83;
            this.lblAssetAmount.Text = "Asset Amount";
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(6, 72);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(89, 13);
            this.lblDueDate.TabIndex = 82;
            this.lblDueDate.Text = "First Due Date";
            // 
            // lblLoanPeriod
            // 
            this.lblLoanPeriod.AutoSize = true;
            this.lblLoanPeriod.Location = new System.Drawing.Point(6, 45);
            this.lblLoanPeriod.Name = "lblLoanPeriod";
            this.lblLoanPeriod.Size = new System.Drawing.Size(74, 13);
            this.lblLoanPeriod.TabIndex = 81;
            this.lblLoanPeriod.Text = "Loan Period";
            // 
            // lblLoanTerm
            // 
            this.lblLoanTerm.AutoSize = true;
            this.lblLoanTerm.Location = new System.Drawing.Point(6, 19);
            this.lblLoanTerm.Name = "lblLoanTerm";
            this.lblLoanTerm.Size = new System.Drawing.Size(67, 13);
            this.lblLoanTerm.TabIndex = 80;
            this.lblLoanTerm.Text = "Loan Term";
            // 
            // tbpSchedule
            // 
            this.tbpSchedule.Controls.Add(this.groupBox5);
            this.tbpSchedule.Location = new System.Drawing.Point(4, 22);
            this.tbpSchedule.Name = "tbpSchedule";
            this.tbpSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSchedule.Size = new System.Drawing.Size(661, 208);
            this.tbpSchedule.TabIndex = 1;
            this.tbpSchedule.Text = "Schedule";
            this.tbpSchedule.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvSchedule);
            this.groupBox5.Location = new System.Drawing.Point(4, -4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(653, 208);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvSchedule.Location = new System.Drawing.Point(6, 19);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowHeadersWidth = 15;
            this.dgvSchedule.Size = new System.Drawing.Size(643, 183);
            this.dgvSchedule.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Rnt No";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Date";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Scheduled Payment";
            this.Column3.Name = "Column3";
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Capital";
            this.Column4.Name = "Column4";
            this.Column4.Width = 107;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Interest";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Tax";
            this.Column6.Name = "Column6";
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Balance";
            this.Column7.Name = "Column7";
            this.Column7.Width = 110;
            // 
            // tbpCharges
            // 
            this.tbpCharges.Controls.Add(this.groupBox6);
            this.tbpCharges.Location = new System.Drawing.Point(4, 22);
            this.tbpCharges.Name = "tbpCharges";
            this.tbpCharges.Size = new System.Drawing.Size(661, 208);
            this.tbpCharges.TabIndex = 2;
            this.tbpCharges.Text = "Charges";
            this.tbpCharges.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtOtherCharge);
            this.groupBox6.Controls.Add(this.txtServiceCharge);
            this.groupBox6.Controls.Add(this.lblOtherCharge);
            this.groupBox6.Controls.Add(this.lblServiceCharge);
            this.groupBox6.Location = new System.Drawing.Point(4, -4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(653, 208);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            // 
            // txtOtherCharge
            // 
            this.txtOtherCharge.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherCharge.Location = new System.Drawing.Point(129, 42);
            this.txtOtherCharge.Name = "txtOtherCharge";
            this.txtOtherCharge.Size = new System.Drawing.Size(165, 21);
            this.txtOtherCharge.TabIndex = 42;
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtServiceCharge.Location = new System.Drawing.Point(129, 16);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.Size = new System.Drawing.Size(165, 21);
            this.txtServiceCharge.TabIndex = 41;
            // 
            // lblOtherCharge
            // 
            this.lblOtherCharge.AutoSize = true;
            this.lblOtherCharge.Location = new System.Drawing.Point(6, 45);
            this.lblOtherCharge.Name = "lblOtherCharge";
            this.lblOtherCharge.Size = new System.Drawing.Size(85, 13);
            this.lblOtherCharge.TabIndex = 40;
            this.lblOtherCharge.Text = "Other Charge";
            // 
            // lblServiceCharge
            // 
            this.lblServiceCharge.AutoSize = true;
            this.lblServiceCharge.Location = new System.Drawing.Point(6, 19);
            this.lblServiceCharge.Name = "lblServiceCharge";
            this.lblServiceCharge.Size = new System.Drawing.Size(96, 13);
            this.lblServiceCharge.TabIndex = 39;
            this.lblServiceCharge.Text = "Service Charge";
            // 
            // tbpNotifications
            // 
            this.tbpNotifications.Controls.Add(this.groupBox7);
            this.tbpNotifications.Location = new System.Drawing.Point(4, 22);
            this.tbpNotifications.Name = "tbpNotifications";
            this.tbpNotifications.Size = new System.Drawing.Size(661, 208);
            this.tbpNotifications.TabIndex = 3;
            this.tbpNotifications.Text = "Notifications";
            this.tbpNotifications.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtTelephone);
            this.groupBox7.Controls.Add(this.txtEmail);
            this.groupBox7.Controls.Add(this.txtBeforeNoOfDays);
            this.groupBox7.Controls.Add(this.lblBeforeNoOfDays);
            this.groupBox7.Controls.Add(this.chkUserPopups);
            this.groupBox7.Controls.Add(this.chkSms);
            this.groupBox7.Controls.Add(this.chkEmail);
            this.groupBox7.Location = new System.Drawing.Point(4, -4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(653, 208);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(139, 42);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(155, 21);
            this.txtTelephone.TabIndex = 102;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(139, 18);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(285, 21);
            this.txtEmail.TabIndex = 101;
            // 
            // txtBeforeNoOfDays
            // 
            this.txtBeforeNoOfDays.IntValue = 0;
            this.txtBeforeNoOfDays.Location = new System.Drawing.Point(139, 94);
            this.txtBeforeNoOfDays.Name = "txtBeforeNoOfDays";
            this.txtBeforeNoOfDays.Size = new System.Drawing.Size(57, 21);
            this.txtBeforeNoOfDays.TabIndex = 100;
            // 
            // lblBeforeNoOfDays
            // 
            this.lblBeforeNoOfDays.AutoSize = true;
            this.lblBeforeNoOfDays.Location = new System.Drawing.Point(16, 98);
            this.lblBeforeNoOfDays.Name = "lblBeforeNoOfDays";
            this.lblBeforeNoOfDays.Size = new System.Drawing.Size(114, 13);
            this.lblBeforeNoOfDays.TabIndex = 99;
            this.lblBeforeNoOfDays.Text = "Before No Of Days";
            // 
            // chkUserPopups
            // 
            this.chkUserPopups.AutoSize = true;
            this.chkUserPopups.Location = new System.Drawing.Point(17, 66);
            this.chkUserPopups.Name = "chkUserPopups";
            this.chkUserPopups.Size = new System.Drawing.Size(97, 17);
            this.chkUserPopups.TabIndex = 2;
            this.chkUserPopups.Text = "User Popups";
            this.chkUserPopups.UseVisualStyleBackColor = true;
            // 
            // chkSms
            // 
            this.chkSms.AutoSize = true;
            this.chkSms.Location = new System.Drawing.Point(17, 43);
            this.chkSms.Name = "chkSms";
            this.chkSms.Size = new System.Drawing.Size(51, 17);
            this.chkSms.TabIndex = 1;
            this.chkSms.Text = "SMS";
            this.chkSms.UseVisualStyleBackColor = true;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(17, 20);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(62, 17);
            this.chkEmail.TabIndex = 0;
            this.chkEmail.Text = "E-mail";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // tbpMortgage
            // 
            this.tbpMortgage.Controls.Add(this.groupBox8);
            this.tbpMortgage.Location = new System.Drawing.Point(4, 22);
            this.tbpMortgage.Name = "tbpMortgage";
            this.tbpMortgage.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMortgage.Size = new System.Drawing.Size(661, 208);
            this.tbpMortgage.TabIndex = 4;
            this.tbpMortgage.Text = "Mortgage";
            this.tbpMortgage.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtMortgageNo);
            this.groupBox8.Controls.Add(this.lblMortgageNo);
            this.groupBox8.Controls.Add(this.txtMortgageInterestRate);
            this.groupBox8.Controls.Add(this.lblMortgageInterest);
            this.groupBox8.Controls.Add(this.dtpMortgageDate);
            this.groupBox8.Controls.Add(this.lblMortgageDate);
            this.groupBox8.Controls.Add(this.chkMortgage);
            this.groupBox8.Location = new System.Drawing.Point(4, -4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(653, 208);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            // 
            // txtMortgageNo
            // 
            this.txtMortgageNo.Location = new System.Drawing.Point(129, 92);
            this.txtMortgageNo.Name = "txtMortgageNo";
            this.txtMortgageNo.Size = new System.Drawing.Size(237, 21);
            this.txtMortgageNo.TabIndex = 103;
            // 
            // lblMortgageNo
            // 
            this.lblMortgageNo.AutoSize = true;
            this.lblMortgageNo.Location = new System.Drawing.Point(7, 97);
            this.lblMortgageNo.Name = "lblMortgageNo";
            this.lblMortgageNo.Size = new System.Drawing.Size(79, 13);
            this.lblMortgageNo.TabIndex = 102;
            this.lblMortgageNo.Text = "Mortgage No";
            // 
            // txtMortgageInterestRate
            // 
            this.txtMortgageInterestRate.ForeColor = System.Drawing.Color.Black;
            this.txtMortgageInterestRate.Location = new System.Drawing.Point(129, 65);
            this.txtMortgageInterestRate.Name = "txtMortgageInterestRate";
            this.txtMortgageInterestRate.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMortgageInterestRate.Size = new System.Drawing.Size(136, 21);
            this.txtMortgageInterestRate.TabIndex = 101;
            // 
            // lblMortgageInterest
            // 
            this.lblMortgageInterest.AutoSize = true;
            this.lblMortgageInterest.Location = new System.Drawing.Point(7, 70);
            this.lblMortgageInterest.Name = "lblMortgageInterest";
            this.lblMortgageInterest.Size = new System.Drawing.Size(98, 13);
            this.lblMortgageInterest.TabIndex = 100;
            this.lblMortgageInterest.Text = "Interest Rate %";
            // 
            // dtpMortgageDate
            // 
            this.dtpMortgageDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMortgageDate.Location = new System.Drawing.Point(129, 39);
            this.dtpMortgageDate.Name = "dtpMortgageDate";
            this.dtpMortgageDate.Size = new System.Drawing.Size(136, 21);
            this.dtpMortgageDate.TabIndex = 46;
            // 
            // lblMortgageDate
            // 
            this.lblMortgageDate.AutoSize = true;
            this.lblMortgageDate.Location = new System.Drawing.Point(7, 45);
            this.lblMortgageDate.Name = "lblMortgageDate";
            this.lblMortgageDate.Size = new System.Drawing.Size(91, 13);
            this.lblMortgageDate.TabIndex = 45;
            this.lblMortgageDate.Text = "Mortgage Date";
            // 
            // chkMortgage
            // 
            this.chkMortgage.AutoSize = true;
            this.chkMortgage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMortgage.Location = new System.Drawing.Point(6, 19);
            this.chkMortgage.Name = "chkMortgage";
            this.chkMortgage.Size = new System.Drawing.Size(138, 17);
            this.chkMortgage.TabIndex = 0;
            this.chkMortgage.Text = "Mortgage Loan       ";
            this.chkMortgage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Silver;
            this.groupBox4.Controls.Add(this.txtDisplayGrossInstallment);
            this.groupBox4.Controls.Add(this.txtDisplayNetInstallment);
            this.groupBox4.Controls.Add(this.txtDisplayMaturityDate);
            this.groupBox4.Controls.Add(this.txtDisplayDownPayment);
            this.groupBox4.Controls.Add(this.txtDisplayTaxCharges);
            this.groupBox4.Controls.Add(this.txtDisplayEffectiveRate);
            this.groupBox4.Controls.Add(this.txtDisplayInterestRate);
            this.groupBox4.Controls.Add(this.txtDisplayGrossAmount);
            this.groupBox4.Controls.Add(this.txtDisplayNetAmount);
            this.groupBox4.Controls.Add(this.lblDisplayEffectiveRate);
            this.groupBox4.Controls.Add(this.lblDisplayInterestRate);
            this.groupBox4.Controls.Add(this.lblDisplayTaxRate);
            this.groupBox4.Controls.Add(this.lblDisplayGrossAmount);
            this.groupBox4.Controls.Add(this.lblDisplayNetAmount);
            this.groupBox4.Controls.Add(this.lblDisplayMaturityDate);
            this.groupBox4.Controls.Add(this.lblDisplayDownPayment);
            this.groupBox4.Controls.Add(this.lblDisplayGrossInstallment);
            this.groupBox4.Controls.Add(this.lblDisplayNetInstallment);
            this.groupBox4.Location = new System.Drawing.Point(679, 128);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 249);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // txtDisplayGrossInstallment
            // 
            this.txtDisplayGrossInstallment.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayGrossInstallment.Location = new System.Drawing.Point(125, 216);
            this.txtDisplayGrossInstallment.Name = "txtDisplayGrossInstallment";
            this.txtDisplayGrossInstallment.ReadOnly = true;
            this.txtDisplayGrossInstallment.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayGrossInstallment.TabIndex = 87;
            this.txtDisplayGrossInstallment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayNetInstallment
            // 
            this.txtDisplayNetInstallment.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayNetInstallment.Location = new System.Drawing.Point(125, 191);
            this.txtDisplayNetInstallment.Name = "txtDisplayNetInstallment";
            this.txtDisplayNetInstallment.ReadOnly = true;
            this.txtDisplayNetInstallment.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayNetInstallment.TabIndex = 86;
            this.txtDisplayNetInstallment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayMaturityDate
            // 
            this.txtDisplayMaturityDate.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayMaturityDate.Location = new System.Drawing.Point(125, 166);
            this.txtDisplayMaturityDate.Name = "txtDisplayMaturityDate";
            this.txtDisplayMaturityDate.ReadOnly = true;
            this.txtDisplayMaturityDate.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayMaturityDate.TabIndex = 85;
            this.txtDisplayMaturityDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayDownPayment
            // 
            this.txtDisplayDownPayment.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayDownPayment.Location = new System.Drawing.Point(125, 141);
            this.txtDisplayDownPayment.Name = "txtDisplayDownPayment";
            this.txtDisplayDownPayment.ReadOnly = true;
            this.txtDisplayDownPayment.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayDownPayment.TabIndex = 84;
            this.txtDisplayDownPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayTaxCharges
            // 
            this.txtDisplayTaxCharges.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayTaxCharges.Location = new System.Drawing.Point(125, 116);
            this.txtDisplayTaxCharges.Name = "txtDisplayTaxCharges";
            this.txtDisplayTaxCharges.ReadOnly = true;
            this.txtDisplayTaxCharges.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayTaxCharges.TabIndex = 83;
            this.txtDisplayTaxCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayEffectiveRate
            // 
            this.txtDisplayEffectiveRate.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayEffectiveRate.Location = new System.Drawing.Point(125, 91);
            this.txtDisplayEffectiveRate.Name = "txtDisplayEffectiveRate";
            this.txtDisplayEffectiveRate.ReadOnly = true;
            this.txtDisplayEffectiveRate.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayEffectiveRate.TabIndex = 82;
            this.txtDisplayEffectiveRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayInterestRate
            // 
            this.txtDisplayInterestRate.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayInterestRate.Location = new System.Drawing.Point(125, 66);
            this.txtDisplayInterestRate.Name = "txtDisplayInterestRate";
            this.txtDisplayInterestRate.ReadOnly = true;
            this.txtDisplayInterestRate.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayInterestRate.TabIndex = 81;
            this.txtDisplayInterestRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayGrossAmount
            // 
            this.txtDisplayGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayGrossAmount.Location = new System.Drawing.Point(125, 41);
            this.txtDisplayGrossAmount.Name = "txtDisplayGrossAmount";
            this.txtDisplayGrossAmount.ReadOnly = true;
            this.txtDisplayGrossAmount.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayGrossAmount.TabIndex = 80;
            this.txtDisplayGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayNetAmount
            // 
            this.txtDisplayNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayNetAmount.Location = new System.Drawing.Point(125, 16);
            this.txtDisplayNetAmount.Name = "txtDisplayNetAmount";
            this.txtDisplayNetAmount.ReadOnly = true;
            this.txtDisplayNetAmount.Size = new System.Drawing.Size(173, 21);
            this.txtDisplayNetAmount.TabIndex = 79;
            this.txtDisplayNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDisplayEffectiveRate
            // 
            this.lblDisplayEffectiveRate.AutoSize = true;
            this.lblDisplayEffectiveRate.Location = new System.Drawing.Point(7, 94);
            this.lblDisplayEffectiveRate.Name = "lblDisplayEffectiveRate";
            this.lblDisplayEffectiveRate.Size = new System.Drawing.Size(102, 13);
            this.lblDisplayEffectiveRate.TabIndex = 78;
            this.lblDisplayEffectiveRate.Text = "Effective Rate %";
            // 
            // lblDisplayInterestRate
            // 
            this.lblDisplayInterestRate.AutoSize = true;
            this.lblDisplayInterestRate.Location = new System.Drawing.Point(6, 69);
            this.lblDisplayInterestRate.Name = "lblDisplayInterestRate";
            this.lblDisplayInterestRate.Size = new System.Drawing.Size(98, 13);
            this.lblDisplayInterestRate.TabIndex = 77;
            this.lblDisplayInterestRate.Text = "Interest Rate %";
            // 
            // lblDisplayTaxRate
            // 
            this.lblDisplayTaxRate.AutoSize = true;
            this.lblDisplayTaxRate.Location = new System.Drawing.Point(6, 119);
            this.lblDisplayTaxRate.Name = "lblDisplayTaxRate";
            this.lblDisplayTaxRate.Size = new System.Drawing.Size(79, 13);
            this.lblDisplayTaxRate.TabIndex = 76;
            this.lblDisplayTaxRate.Text = "Tax Charges";
            // 
            // lblDisplayGrossAmount
            // 
            this.lblDisplayGrossAmount.AutoSize = true;
            this.lblDisplayGrossAmount.Location = new System.Drawing.Point(6, 44);
            this.lblDisplayGrossAmount.Name = "lblDisplayGrossAmount";
            this.lblDisplayGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblDisplayGrossAmount.TabIndex = 75;
            this.lblDisplayGrossAmount.Text = "Gross Amount";
            // 
            // lblDisplayNetAmount
            // 
            this.lblDisplayNetAmount.AutoSize = true;
            this.lblDisplayNetAmount.Location = new System.Drawing.Point(6, 19);
            this.lblDisplayNetAmount.Name = "lblDisplayNetAmount";
            this.lblDisplayNetAmount.Size = new System.Drawing.Size(74, 13);
            this.lblDisplayNetAmount.TabIndex = 74;
            this.lblDisplayNetAmount.Text = "Net Amount";
            // 
            // lblDisplayMaturityDate
            // 
            this.lblDisplayMaturityDate.AutoSize = true;
            this.lblDisplayMaturityDate.Location = new System.Drawing.Point(7, 169);
            this.lblDisplayMaturityDate.Name = "lblDisplayMaturityDate";
            this.lblDisplayMaturityDate.Size = new System.Drawing.Size(84, 13);
            this.lblDisplayMaturityDate.TabIndex = 73;
            this.lblDisplayMaturityDate.Text = "Maturity Date";
            // 
            // lblDisplayDownPayment
            // 
            this.lblDisplayDownPayment.AutoSize = true;
            this.lblDisplayDownPayment.Location = new System.Drawing.Point(7, 144);
            this.lblDisplayDownPayment.Name = "lblDisplayDownPayment";
            this.lblDisplayDownPayment.Size = new System.Drawing.Size(93, 13);
            this.lblDisplayDownPayment.TabIndex = 72;
            this.lblDisplayDownPayment.Text = "Down Payment";
            // 
            // lblDisplayGrossInstallment
            // 
            this.lblDisplayGrossInstallment.AutoSize = true;
            this.lblDisplayGrossInstallment.Location = new System.Drawing.Point(6, 219);
            this.lblDisplayGrossInstallment.Name = "lblDisplayGrossInstallment";
            this.lblDisplayGrossInstallment.Size = new System.Drawing.Size(108, 13);
            this.lblDisplayGrossInstallment.TabIndex = 70;
            this.lblDisplayGrossInstallment.Text = "Gross Installment";
            // 
            // lblDisplayNetInstallment
            // 
            this.lblDisplayNetInstallment.AutoSize = true;
            this.lblDisplayNetInstallment.Location = new System.Drawing.Point(7, 194);
            this.lblDisplayNetInstallment.Name = "lblDisplayNetInstallment";
            this.lblDisplayNetInstallment.Size = new System.Drawing.Size(94, 13);
            this.lblDisplayNetInstallment.TabIndex = 69;
            this.lblDisplayNetInstallment.Text = "Net Installment";
            // 
            // FrmLoanEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(984, 420);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmLoanEntry";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tbLoan.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tbpSchedule.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.tbpCharges.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tbpNotifications.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tbpMortgage.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblManualNo;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.Label lblLoanType;
        private System.Windows.Forms.Label lblFinanceInstitut;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox chkAutoCompleationPurpose;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkAutoCompleationLoanType;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProvider;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DateTimePicker dtpGrnDate;
        private System.Windows.Forms.Label lblEntryDate;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tbLoan;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblGraceRate;
        private CustomControls.TextBoxPercentGen txtGraceRate;
        private CustomControls.TextBoxPercentGen txtInterestRate;
        private CustomControls.TextBoxInteger txtGracePeriod;
        private System.Windows.Forms.Label lblPlus;
        private CustomControls.TextBoxInteger txtEndMonth;
        private CustomControls.TextBoxCurrency txtDownPayment;
        private CustomControls.TextBoxInteger txtBeginMonth;
        private CustomControls.TextBoxCurrency txtLoanAmount;
        private CustomControls.TextBoxCurrency txtAssetAmount;
        private System.Windows.Forms.DateTimePicker dtpFirstDueDate;
        private CustomControls.TextBoxInteger txtLoanPeriod;
        private System.Windows.Forms.ComboBox cmbLoanTerm;
        private System.Windows.Forms.Label lblGracePeriod;
        private System.Windows.Forms.Label lblDownPayment;
        private System.Windows.Forms.Label lblInterestRate;
        private System.Windows.Forms.Label lblNoOfDownPayment;
        private System.Windows.Forms.Label lblLoanAmount;
        private System.Windows.Forms.Label lblAssetAmount;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblLoanPeriod;
        private System.Windows.Forms.Label lblLoanTerm;
        private System.Windows.Forms.TabPage tbpSchedule;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblDisplayMaturityDate;
        private System.Windows.Forms.Label lblDisplayDownPayment;
        private System.Windows.Forms.Label lblDisplayGrossInstallment;
        private System.Windows.Forms.Label lblDisplayNetInstallment;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblDisplayEffectiveRate;
        private System.Windows.Forms.Label lblDisplayInterestRate;
        private System.Windows.Forms.Label lblDisplayTaxRate;
        private System.Windows.Forms.Label lblDisplayGrossAmount;
        private System.Windows.Forms.Label lblDisplayNetAmount;
        private CustomControls.TextBoxCurrency txtDisplayGrossInstallment;
        private CustomControls.TextBoxCurrency txtDisplayNetInstallment;
        private CustomControls.TextBoxCurrency txtDisplayDownPayment;
        private CustomControls.TextBoxCurrency txtDisplayTaxCharges;
        private CustomControls.TextBoxPercentGen txtDisplayEffectiveRate;
        private CustomControls.TextBoxPercentGen txtDisplayInterestRate;
        private CustomControls.TextBoxCurrency txtDisplayGrossAmount;
        private CustomControls.TextBoxCurrency txtDisplayNetAmount;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.TabPage tbpCharges;
        private System.Windows.Forms.TabPage tbpNotifications;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.GroupBox groupBox6;
        private CustomControls.TextBoxCurrency txtOtherCharge;
        private CustomControls.TextBoxCurrency txtServiceCharge;
        private System.Windows.Forms.Label lblOtherCharge;
        private System.Windows.Forms.Label lblServiceCharge;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkUserPopups;
        private System.Windows.Forms.CheckBox chkSms;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.TabPage tbpMortgage;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtMortgageNo;
        private System.Windows.Forms.Label lblMortgageNo;
        private CustomControls.TextBoxPercentGen txtMortgageInterestRate;
        private System.Windows.Forms.Label lblMortgageInterest;
        private System.Windows.Forms.DateTimePicker dtpMortgageDate;
        private System.Windows.Forms.Label lblMortgageDate;
        private System.Windows.Forms.CheckBox chkMortgage;
        private System.Windows.Forms.TextBox txtDisplayMaturityDate;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtEmail;
        private CustomControls.TextBoxInteger txtBeforeNoOfDays;
        private System.Windows.Forms.Label lblBeforeNoOfDays;
        private CustomControls.TextBoxInteger txtMonthlyGracePeriod;
        private System.Windows.Forms.Label lblMonthlyGracePeriod;
    }
}
