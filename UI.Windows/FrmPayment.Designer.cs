namespace UI.Windows
{
    partial class FrmPayment
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.lblManualNo = new System.Windows.Forms.Label();
            this.chkAutoCompleationAPAccount = new System.Windows.Forms.CheckBox();
            this.txtAPAccountName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtAPAccountCode = new System.Windows.Forms.TextBox();
            this.lblPettyCashBook = new System.Windows.Forms.Label();
            this.lblPayeeName = new System.Windows.Forms.Label();
            this.txtPayeeName = new System.Windows.Forms.TextBox();
            this.rdoAdvance = new System.Windows.Forms.RadioButton();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblReceiptDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCardChequeNo = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBoxCurrency5 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtAccountName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.PaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetCreditDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCurrency4 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCurrency3 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tabGRN = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtPayingAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.textBoxCurrency2 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.textBoxCurrency1 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtDisplayNetAmount = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditsUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountToPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpAdvanced = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtOtherExpenceValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtOtherExpenceName = new System.Windows.Forms.TextBox();
            this.txtOtherExpenceCode = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationOtherExpence = new System.Windows.Forms.CheckBox();
            this.dgvAdvanced = new System.Windows.Forms.DataGridView();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherExpenceTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.tabGRN.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tbpAdvanced.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(826, 530);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 530);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.textBox5);
            this.grpHeader.Controls.Add(this.lblManualNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationAPAccount);
            this.grpHeader.Controls.Add(this.txtAPAccountName);
            this.grpHeader.Controls.Add(this.txtAPAccountCode);
            this.grpHeader.Controls.Add(this.lblPettyCashBook);
            this.grpHeader.Controls.Add(this.lblPayeeName);
            this.grpHeader.Controls.Add(this.txtPayeeName);
            this.grpHeader.Controls.Add(this.rdoAdvance);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpPaymentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblReceiptDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(3, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(961, 133);
            this.grpHeader.TabIndex = 25;
            this.grpHeader.TabStop = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(743, 61);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(209, 21);
            this.textBox5.TabIndex = 115;
            // 
            // lblManualNo
            // 
            this.lblManualNo.AutoSize = true;
            this.lblManualNo.Location = new System.Drawing.Point(648, 64);
            this.lblManualNo.Name = "lblManualNo";
            this.lblManualNo.Size = new System.Drawing.Size(66, 13);
            this.lblManualNo.TabIndex = 114;
            this.lblManualNo.Text = "Manual No";
            // 
            // chkAutoCompleationAPAccount
            // 
            this.chkAutoCompleationAPAccount.AutoSize = true;
            this.chkAutoCompleationAPAccount.Checked = true;
            this.chkAutoCompleationAPAccount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationAPAccount.Location = new System.Drawing.Point(90, 40);
            this.chkAutoCompleationAPAccount.Name = "chkAutoCompleationAPAccount";
            this.chkAutoCompleationAPAccount.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationAPAccount.TabIndex = 113;
            this.chkAutoCompleationAPAccount.Tag = "1";
            this.chkAutoCompleationAPAccount.UseVisualStyleBackColor = true;
            // 
            // txtAPAccountName
            // 
            this.txtAPAccountName.Location = new System.Drawing.Point(244, 37);
            this.txtAPAccountName.MasterDescription = "";
            this.txtAPAccountName.Name = "txtAPAccountName";
            this.txtAPAccountName.Size = new System.Drawing.Size(337, 21);
            this.txtAPAccountName.TabIndex = 111;
            // 
            // txtAPAccountCode
            // 
            this.txtAPAccountCode.Location = new System.Drawing.Point(107, 37);
            this.txtAPAccountCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAPAccountCode.Name = "txtAPAccountCode";
            this.txtAPAccountCode.Size = new System.Drawing.Size(136, 21);
            this.txtAPAccountCode.TabIndex = 110;
            // 
            // lblPettyCashBook
            // 
            this.lblPettyCashBook.AutoSize = true;
            this.lblPettyCashBook.Location = new System.Drawing.Point(6, 41);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblPettyCashBook.TabIndex = 112;
            this.lblPettyCashBook.Text = "A/P Account";
            // 
            // lblPayeeName
            // 
            this.lblPayeeName.AutoSize = true;
            this.lblPayeeName.Location = new System.Drawing.Point(6, 86);
            this.lblPayeeName.Name = "lblPayeeName";
            this.lblPayeeName.Size = new System.Drawing.Size(79, 13);
            this.lblPayeeName.TabIndex = 109;
            this.lblPayeeName.Text = "Payee Name";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.Location = new System.Drawing.Point(107, 83);
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.Size = new System.Drawing.Size(474, 21);
            this.txtPayeeName.TabIndex = 108;
            // 
            // rdoAdvance
            // 
            this.rdoAdvance.AutoSize = true;
            this.rdoAdvance.Location = new System.Drawing.Point(746, 111);
            this.rdoAdvance.Name = "rdoAdvance";
            this.rdoAdvance.Size = new System.Drawing.Size(74, 17);
            this.rdoAdvance.TabIndex = 107;
            this.rdoAdvance.TabStop = true;
            this.rdoAdvance.Text = "Advance";
            this.rdoAdvance.UseVisualStyleBackColor = true;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(6, 63);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 106;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(244, 60);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(337, 21);
            this.txtSupplierName.TabIndex = 105;
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(90, 63);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 104;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSupplier.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSupplier_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(107, 60);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 103;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(743, 83);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(207, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(648, 86);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 101;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTStatus.Location = new System.Drawing.Point(937, 40);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(15, 14);
            this.chkTStatus.TabIndex = 87;
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(374, 13);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(207, 21);
            this.cmbLocation.TabIndex = 63;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(292, 15);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(743, 38);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(197, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(648, 41);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentDate.Location = new System.Drawing.Point(744, 15);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(207, 21);
            this.dtpPaymentDate.TabIndex = 22;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(244, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 109);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 106);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSize = true;
            this.lblReceiptDate.Location = new System.Drawing.Point(648, 17);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(88, 13);
            this.lblReceiptDate.TabIndex = 9;
            this.lblReceiptDate.Text = "Payment Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCardChequeNo);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.textBoxCurrency5);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.txtAccountName);
            this.groupBox3.Controls.Add(this.txtAccountCode);
            this.groupBox3.Controls.Add(this.cmbPaymentMethod);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(3, 345);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 139);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // txtCardChequeNo
            // 
            this.txtCardChequeNo.Location = new System.Drawing.Point(588, 118);
            this.txtCardChequeNo.Name = "txtCardChequeNo";
            this.txtCardChequeNo.Size = new System.Drawing.Size(80, 21);
            this.txtCardChequeNo.TabIndex = 121;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(722, 118);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Set Credits";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(670, 118);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(52, 23);
            this.button6.TabIndex = 120;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBoxCurrency5
            // 
            this.textBoxCurrency5.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency5.Location = new System.Drawing.Point(805, 118);
            this.textBoxCurrency5.Name = "textBoxCurrency5";
            this.textBoxCurrency5.Size = new System.Drawing.Size(144, 21);
            this.textBoxCurrency5.TabIndex = 118;
            this.textBoxCurrency5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxCurrency5.TextChanged += new System.EventHandler(this.textBoxCurrency5_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(143, 121);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 117;
            this.checkBox1.Tag = "1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(275, 118);
            this.txtAccountName.MasterDescription = "";
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(218, 21);
            this.txtAccountName.TabIndex = 115;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new System.Drawing.Point(160, 118);
            this.txtAccountCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(114, 21);
            this.txtAccountCode.TabIndex = 114;
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(4, 118);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(139, 21);
            this.cmbPaymentMethod.TabIndex = 46;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(495, 118);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 21);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentMethod,
            this.AccountCode,
            this.AccountName,
            this.Date,
            this.CardNo,
            this.SetCreditDocument,
            this.Amount});
            this.dataGridView2.Location = new System.Drawing.Point(6, 11);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(950, 104);
            this.dataGridView2.TabIndex = 0;
            // 
            // PaymentMethod
            // 
            this.PaymentMethod.HeaderText = "Payment Method";
            this.PaymentMethod.Name = "PaymentMethod";
            // 
            // AccountCode
            // 
            this.AccountCode.DataPropertyName = "AccountCode";
            this.AccountCode.HeaderText = "Account Code";
            this.AccountCode.Name = "AccountCode";
            this.AccountCode.Width = 110;
            // 
            // AccountName
            // 
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.Width = 220;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // CardNo
            // 
            this.CardNo.HeaderText = "Card/Cheque No";
            this.CardNo.Name = "CardNo";
            // 
            // SetCreditDocument
            // 
            this.SetCreditDocument.HeaderText = "Set Credit Document";
            this.SetCreditDocument.Name = "SetCreditDocument";
            // 
            // Amount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 175;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(191, 530);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 46);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 27);
            this.button2.TabIndex = 1;
            this.button2.Text = "Use Recurring";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save As Recurring";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBoxCurrency4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxCurrency3);
            this.groupBox4.Location = new System.Drawing.Point(3, 487);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(961, 48);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 31);
            this.button5.TabIndex = 37;
            this.button5.Text = "PD Cheques";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 17);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 31);
            this.button4.TabIndex = 36;
            this.button4.Text = "Statement";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(637, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Total Credits Available";
            // 
            // textBoxCurrency4
            // 
            this.textBoxCurrency4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency4.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency4.Location = new System.Drawing.Point(789, 37);
            this.textBoxCurrency4.Name = "textBoxCurrency4";
            this.textBoxCurrency4.ReadOnly = true;
            this.textBoxCurrency4.Size = new System.Drawing.Size(163, 21);
            this.textBoxCurrency4.TabIndex = 34;
            this.textBoxCurrency4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(637, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Total Amount";
            // 
            // textBoxCurrency3
            // 
            this.textBoxCurrency3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency3.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency3.Location = new System.Drawing.Point(789, 14);
            this.textBoxCurrency3.Name = "textBoxCurrency3";
            this.textBoxCurrency3.ReadOnly = true;
            this.textBoxCurrency3.Size = new System.Drawing.Size(163, 21);
            this.textBoxCurrency3.TabIndex = 32;
            this.textBoxCurrency3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(973, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 515);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tabGRN);
            this.grpBody.Location = new System.Drawing.Point(3, 123);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(961, 228);
            this.grpBody.TabIndex = 32;
            this.grpBody.TabStop = false;
            // 
            // tabGRN
            // 
            this.tabGRN.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabGRN.Controls.Add(this.tbpGeneral);
            this.tabGRN.Controls.Add(this.tbpAdvanced);
            this.tabGRN.Location = new System.Drawing.Point(3, 12);
            this.tabGRN.Name = "tabGRN";
            this.tabGRN.SelectedIndex = 0;
            this.tabGRN.Size = new System.Drawing.Size(937, 213);
            this.tabGRN.TabIndex = 1;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.groupBox5);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(929, 184);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtPayingAmount);
            this.groupBox5.Controls.Add(this.textBoxCurrency2);
            this.groupBox5.Controls.Add(this.textBoxCurrency1);
            this.groupBox5.Controls.Add(this.txtGrossAmount);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.txtDisplayNetAmount);
            this.groupBox5.Controls.Add(this.dataGridView1);
            this.groupBox5.Location = new System.Drawing.Point(4, -4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(916, 183);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            // 
            // txtPayingAmount
            // 
            this.txtPayingAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPayingAmount.Location = new System.Drawing.Point(685, 159);
            this.txtPayingAmount.Name = "txtPayingAmount";
            this.txtPayingAmount.Size = new System.Drawing.Size(163, 21);
            this.txtPayingAmount.TabIndex = 84;
            this.txtPayingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCurrency2
            // 
            this.textBoxCurrency2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency2.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency2.Location = new System.Drawing.Point(545, 160);
            this.textBoxCurrency2.Name = "textBoxCurrency2";
            this.textBoxCurrency2.ReadOnly = true;
            this.textBoxCurrency2.Size = new System.Drawing.Size(134, 21);
            this.textBoxCurrency2.TabIndex = 83;
            this.textBoxCurrency2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCurrency1
            // 
            this.textBoxCurrency1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency1.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency1.Location = new System.Drawing.Point(391, 160);
            this.textBoxCurrency1.Name = "textBoxCurrency1";
            this.textBoxCurrency1.ReadOnly = true;
            this.textBoxCurrency1.Size = new System.Drawing.Size(134, 21);
            this.textBoxCurrency1.TabIndex = 31;
            this.textBoxCurrency1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGrossAmount.Location = new System.Drawing.Point(251, 159);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(134, 21);
            this.txtGrossAmount.TabIndex = 82;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBox1.Location = new System.Drawing.Point(153, 159);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(92, 21);
            this.textBox1.TabIndex = 81;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDisplayNetAmount
            // 
            this.txtDisplayNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayNetAmount.Location = new System.Drawing.Point(22, 159);
            this.txtDisplayNetAmount.Name = "txtDisplayNetAmount";
            this.txtDisplayNetAmount.ReadOnly = true;
            this.txtDisplayNetAmount.Size = new System.Drawing.Size(125, 21);
            this.txtDisplayNetAmount.TabIndex = 80;
            this.txtDisplayNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.SupplierName,
            this.DocumentNo,
            this.ReferenceNo,
            this.DueDate,
            this.DueAmount,
            this.BalanceAmount,
            this.CreditsUsed,
            this.AmountToPay});
            this.dataGridView1.Location = new System.Drawing.Point(6, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(904, 147);
            this.dataGridView1.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 50;
            // 
            // SupplierName
            // 
            this.SupplierName.HeaderText = "Supplier Name";
            this.SupplierName.Name = "SupplierName";
            // 
            // DocumentNo
            // 
            this.DocumentNo.HeaderText = "Document No";
            this.DocumentNo.Name = "DocumentNo";
            this.DocumentNo.Width = 120;
            // 
            // ReferenceNo
            // 
            this.ReferenceNo.HeaderText = "Reference No";
            this.ReferenceNo.Name = "ReferenceNo";
            // 
            // DueDate
            // 
            this.DueDate.HeaderText = "Due Date";
            this.DueDate.Name = "DueDate";
            // 
            // DueAmount
            // 
            this.DueAmount.HeaderText = "Due Amount";
            this.DueAmount.Name = "DueAmount";
            this.DueAmount.Width = 150;
            // 
            // BalanceAmount
            // 
            this.BalanceAmount.HeaderText = "Balance Amount";
            this.BalanceAmount.Name = "BalanceAmount";
            this.BalanceAmount.Width = 150;
            // 
            // CreditsUsed
            // 
            this.CreditsUsed.HeaderText = "Credits Used";
            this.CreditsUsed.Name = "CreditsUsed";
            this.CreditsUsed.Width = 125;
            // 
            // AmountToPay
            // 
            this.AmountToPay.HeaderText = "Amount To Pay";
            this.AmountToPay.Name = "AmountToPay";
            this.AmountToPay.Width = 150;
            // 
            // tbpAdvanced
            // 
            this.tbpAdvanced.Controls.Add(this.groupBox6);
            this.tbpAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tbpAdvanced.Name = "tbpAdvanced";
            this.tbpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAdvanced.Size = new System.Drawing.Size(929, 184);
            this.tbpAdvanced.TabIndex = 1;
            this.tbpAdvanced.Text = "Advanced";
            this.tbpAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtOtherExpenceValue);
            this.groupBox6.Controls.Add(this.txtOtherExpenceName);
            this.groupBox6.Controls.Add(this.txtOtherExpenceCode);
            this.groupBox6.Controls.Add(this.chkAutoCompleationOtherExpence);
            this.groupBox6.Controls.Add(this.dgvAdvanced);
            this.groupBox6.Location = new System.Drawing.Point(4, -4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(833, 192);
            this.groupBox6.TabIndex = 53;
            this.groupBox6.TabStop = false;
            // 
            // txtOtherExpenceValue
            // 
            this.txtOtherExpenceValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherExpenceValue.Location = new System.Drawing.Point(519, 162);
            this.txtOtherExpenceValue.Name = "txtOtherExpenceValue";
            this.txtOtherExpenceValue.Size = new System.Drawing.Size(126, 21);
            this.txtOtherExpenceValue.TabIndex = 56;
            this.txtOtherExpenceValue.Tag = "3";
            this.txtOtherExpenceValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOtherExpenceName
            // 
            this.txtOtherExpenceName.Location = new System.Drawing.Point(170, 162);
            this.txtOtherExpenceName.Name = "txtOtherExpenceName";
            this.txtOtherExpenceName.Size = new System.Drawing.Size(348, 21);
            this.txtOtherExpenceName.TabIndex = 55;
            // 
            // txtOtherExpenceCode
            // 
            this.txtOtherExpenceCode.Location = new System.Drawing.Point(18, 162);
            this.txtOtherExpenceCode.Name = "txtOtherExpenceCode";
            this.txtOtherExpenceCode.Size = new System.Drawing.Size(151, 21);
            this.txtOtherExpenceCode.TabIndex = 53;
            // 
            // chkAutoCompleationOtherExpence
            // 
            this.chkAutoCompleationOtherExpence.AutoSize = true;
            this.chkAutoCompleationOtherExpence.Checked = true;
            this.chkAutoCompleationOtherExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationOtherExpence.Location = new System.Drawing.Point(3, 165);
            this.chkAutoCompleationOtherExpence.Name = "chkAutoCompleationOtherExpence";
            this.chkAutoCompleationOtherExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationOtherExpence.TabIndex = 54;
            this.chkAutoCompleationOtherExpence.Tag = "1";
            this.chkAutoCompleationOtherExpence.UseVisualStyleBackColor = true;
            // 
            // dgvAdvanced
            // 
            this.dgvAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdvanced.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerCode,
            this.AccLedgerAccountID,
            this.LedgerName,
            this.ExpenceAmount,
            this.OtherExpenceTempID});
            this.dgvAdvanced.Location = new System.Drawing.Point(6, 12);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.RowHeadersWidth = 15;
            this.dgvAdvanced.Size = new System.Drawing.Size(823, 144);
            this.dgvAdvanced.TabIndex = 35;
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 150;
            // 
            // AccLedgerAccountID
            // 
            this.AccLedgerAccountID.DataPropertyName = "AccLedgerAccountID";
            this.AccLedgerAccountID.HeaderText = "AccLedgerAccountID";
            this.AccLedgerAccountID.Name = "AccLedgerAccountID";
            this.AccLedgerAccountID.ReadOnly = true;
            this.AccLedgerAccountID.Visible = false;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerName.Width = 350;
            // 
            // ExpenseAmount
            // 
            this.ExpenceAmount.DataPropertyName = "ExpenseAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ExpenceAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.ExpenceAmount.HeaderText = "Value";
            this.ExpenceAmount.Name = "ExpenseAmount";
            this.ExpenceAmount.ReadOnly = true;
            this.ExpenceAmount.Width = 120;
            // 
            // OtherExpenseTempID
            // 
            this.OtherExpenceTempID.DataPropertyName = "OtherExpenseTempID";
            this.OtherExpenceTempID.HeaderText = "OtherExpenseTempID";
            this.OtherExpenceTempID.Name = "OtherExpenseTempID";
            this.OtherExpenceTempID.ReadOnly = true;
            this.OtherExpenceTempID.Visible = false;
            // 
            // FrmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1145, 578);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmPayment";
            this.Text = "Payment";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.tabGRN.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tbpAdvanced.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label lblPayeeName;
        private System.Windows.Forms.TextBox txtPayeeName;
        private System.Windows.Forms.RadioButton rdoAdvance;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblReceiptDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationAPAccount;
        private CustomControls.TextBoxMasterDescription txtAPAccountName;
        private System.Windows.Forms.TextBox txtAPAccountCode;
        private System.Windows.Forms.Label lblPettyCashBook;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.CheckBox checkBox1;
        private CustomControls.TextBoxMasterDescription txtAccountName;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.GroupBox groupBox4;
        private CustomControls.TextBoxCurrency textBoxCurrency3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CustomControls.TextBoxCurrency textBoxCurrency4;
        private CustomControls.TextBoxCurrency textBoxCurrency5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label lblManualNo;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetCreditDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.TextBox txtCardChequeNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TabControl tabGRN;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.GroupBox groupBox5;
        private CustomControls.TextBoxCurrency txtPayingAmount;
        private CustomControls.TextBoxCurrency textBoxCurrency2;
        private CustomControls.TextBoxCurrency textBoxCurrency1;
        private CustomControls.TextBoxCurrency txtGrossAmount;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtDisplayNetAmount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditsUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountToPay;
        private System.Windows.Forms.TabPage tbpAdvanced;
        private System.Windows.Forms.GroupBox groupBox6;
        private CustomControls.TextBoxCurrency txtOtherExpenceValue;
        private System.Windows.Forms.TextBox txtOtherExpenceName;
        private System.Windows.Forms.TextBox txtOtherExpenceCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationOtherExpence;
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenceTempID;
    }
}
