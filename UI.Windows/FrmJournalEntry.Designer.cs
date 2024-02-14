namespace UI.Windows
{
    partial class FrmJournalEntry
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
            this.rdoPayable = new System.Windows.Forms.RadioButton();
            this.rdoReceivables = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtManualNo = new System.Windows.Forms.TextBox();
            this.lblManualNo = new System.Windows.Forms.Label();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpJournalDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblJournalDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.cmbDrCrType = new System.Windows.Forms.ComboBox();
            this.dgvJournal = new System.Windows.Forms.DataGridView();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostCentre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DebitAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherExpenceTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLedgerValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtLedgerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationLedger = new System.Windows.Forms.CheckBox();
            this.txtLedgerCode = new System.Windows.Forms.TextBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.txtTotalDebitValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotalCreditValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(779, 356);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 356);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.rdoPayable);
            this.grpHeader.Controls.Add(this.rdoReceivables);
            this.grpHeader.Controls.Add(this.checkBox1);
            this.grpHeader.Controls.Add(this.txtManualNo);
            this.grpHeader.Controls.Add(this.lblManualNo);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpJournalDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblJournalDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1093, 87);
            this.grpHeader.TabIndex = 20;
            this.grpHeader.TabStop = false;
            // 
            // rdoPayable
            // 
            this.rdoPayable.AutoSize = true;
            this.rdoPayable.Location = new System.Drawing.Point(173, 61);
            this.rdoPayable.Name = "rdoPayable";
            this.rdoPayable.Size = new System.Drawing.Size(125, 17);
            this.rdoPayable.TabIndex = 107;
            this.rdoPayable.TabStop = true;
            this.rdoPayable.Text = "Accounts Payable";
            this.rdoPayable.UseVisualStyleBackColor = true;
            // 
            // rdoReceivables
            // 
            this.rdoReceivables.AutoSize = true;
            this.rdoReceivables.Location = new System.Drawing.Point(32, 61);
            this.rdoReceivables.Name = "rdoReceivables";
            this.rdoReceivables.Size = new System.Drawing.Size(142, 17);
            this.rdoReceivables.TabIndex = 106;
            this.rdoReceivables.TabStop = true;
            this.rdoReceivables.Text = "Accounts Receivable";
            this.rdoReceivables.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 64);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 105;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtManualNo
            // 
            this.txtManualNo.Location = new System.Drawing.Point(878, 58);
            this.txtManualNo.Name = "txtManualNo";
            this.txtManualNo.Size = new System.Drawing.Size(177, 21);
            this.txtManualNo.TabIndex = 104;
            // 
            // lblManualNo
            // 
            this.lblManualNo.AutoSize = true;
            this.lblManualNo.Location = new System.Drawing.Point(786, 61);
            this.lblManualNo.Name = "lblManualNo";
            this.lblManualNo.Size = new System.Drawing.Size(66, 13);
            this.lblManualNo.TabIndex = 103;
            this.lblManualNo.Text = "Manual No";
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTStatus.Location = new System.Drawing.Point(1070, 38);
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
            this.lblLocation.Location = new System.Drawing.Point(314, 16);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(878, 35);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(177, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(786, 38);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpJournalDate
            // 
            this.dtpJournalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpJournalDate.Location = new System.Drawing.Point(878, 11);
            this.dtpJournalDate.Name = "dtpJournalDate";
            this.dtpJournalDate.Size = new System.Drawing.Size(207, 21);
            this.dtpJournalDate.TabIndex = 22;
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
            this.lblRemark.Location = new System.Drawing.Point(6, 38);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 35);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblJournalDate
            // 
            this.lblJournalDate.AutoSize = true;
            this.lblJournalDate.Location = new System.Drawing.Point(786, 14);
            this.lblJournalDate.Name = "lblJournalDate";
            this.lblJournalDate.Size = new System.Drawing.Size(79, 13);
            this.lblJournalDate.TabIndex = 9;
            this.lblJournalDate.Text = "Journal Date";
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
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(369, 222);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(129, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtSupplierName);
            this.grpBody.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpBody.Controls.Add(this.txtSupplierCode);
            this.grpBody.Controls.Add(this.cmbDrCrType);
            this.grpBody.Controls.Add(this.dgvJournal);
            this.grpBody.Controls.Add(this.cmbCostCentre);
            this.grpBody.Controls.Add(this.txtLedgerValue);
            this.grpBody.Controls.Add(this.txtLedgerName);
            this.grpBody.Controls.Add(this.chkAutoCompleationLedger);
            this.grpBody.Controls.Add(this.txtLedgerCode);
            this.grpBody.Location = new System.Drawing.Point(2, 77);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1093, 249);
            this.grpBody.TabIndex = 21;
            this.grpBody.TabStop = false;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(635, 222);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(242, 21);
            this.txtSupplierName.TabIndex = 108;
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(500, 225);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 107;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(517, 222);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(117, 21);
            this.txtSupplierCode.TabIndex = 106;
            // 
            // cmbDrCrType
            // 
            this.cmbDrCrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrCrType.FormattingEnabled = true;
            this.cmbDrCrType.Items.AddRange(new object[] {
            "Dr",
            "Cr"});
            this.cmbDrCrType.Location = new System.Drawing.Point(878, 222);
            this.cmbDrCrType.Name = "cmbDrCrType";
            this.cmbDrCrType.Size = new System.Drawing.Size(76, 21);
            this.cmbDrCrType.TabIndex = 58;
            // 
            // dgvJournal
            // 
            this.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJournal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccLedgerAccountID,
            this.LedgerCode,
            this.LedgerName,
            this.CostCentre,
            this.AccountCode,
            this.AccountName,
            this.DebitAmount,
            this.CreditAmount,
            this.OtherExpenceTempID});
            this.dgvJournal.Location = new System.Drawing.Point(6, 20);
            this.dgvJournal.Name = "dgvJournal";
            this.dgvJournal.RowHeadersWidth = 15;
            this.dgvJournal.Size = new System.Drawing.Size(1077, 201);
            this.dgvJournal.TabIndex = 53;
            // 
            // AccLedgerAccountID
            // 
            this.AccLedgerAccountID.DataPropertyName = "AccLedgerAccountID";
            this.AccLedgerAccountID.HeaderText = "AccLedgerAccountID";
            this.AccLedgerAccountID.Name = "AccLedgerAccountID";
            this.AccLedgerAccountID.ReadOnly = true;
            this.AccLedgerAccountID.Visible = false;
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 120;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerName.Width = 250;
            // 
            // CostCentre
            // 
            this.CostCentre.HeaderText = "CostCentre";
            this.CostCentre.Name = "CostCentre";
            // 
            // AccountCode
            // 
            this.AccountCode.HeaderText = "Account Code";
            this.AccountCode.Name = "AccountCode";
            // 
            // AccountName
            // 
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.Width = 250;
            // 
            // DebitAmount
            // 
            this.DebitAmount.DataPropertyName = "DebitAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DebitAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.DebitAmount.HeaderText = "Debit Amount";
            this.DebitAmount.Name = "DebitAmount";
            this.DebitAmount.ReadOnly = true;
            this.DebitAmount.Width = 120;
            // 
            // CreditAmount
            // 
            this.CreditAmount.DataPropertyName = "CreditAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CreditAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.CreditAmount.HeaderText = "Credit Amount";
            this.CreditAmount.Name = "CreditAmount";
            this.CreditAmount.Width = 120;
            // 
            // OtherExpenseTempID
            // 
            this.OtherExpenceTempID.DataPropertyName = "OtherExpenseTempID";
            this.OtherExpenceTempID.HeaderText = "OtherExpenseTempID";
            this.OtherExpenceTempID.Name = "OtherExpenseTempID";
            this.OtherExpenceTempID.ReadOnly = true;
            this.OtherExpenceTempID.Visible = false;
            // 
            // txtLedgerValue
            // 
            this.txtLedgerValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLedgerValue.Location = new System.Drawing.Point(957, 222);
            this.txtLedgerValue.Name = "txtLedgerValue";
            this.txtLedgerValue.Size = new System.Drawing.Size(126, 21);
            this.txtLedgerValue.TabIndex = 57;
            this.txtLedgerValue.Tag = "3";
            this.txtLedgerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(140, 222);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(228, 21);
            this.txtLedgerName.TabIndex = 56;
            // 
            // chkAutoCompleationLedger
            // 
            this.chkAutoCompleationLedger.AutoSize = true;
            this.chkAutoCompleationLedger.Checked = true;
            this.chkAutoCompleationLedger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLedger.Location = new System.Drawing.Point(6, 225);
            this.chkAutoCompleationLedger.Name = "chkAutoCompleationLedger";
            this.chkAutoCompleationLedger.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLedger.TabIndex = 55;
            this.chkAutoCompleationLedger.Tag = "1";
            this.chkAutoCompleationLedger.UseVisualStyleBackColor = true;
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(21, 222);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Size = new System.Drawing.Size(118, 21);
            this.txtLedgerCode.TabIndex = 54;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtTotalDebitValue);
            this.grpFooter.Controls.Add(this.lblTotal);
            this.grpFooter.Controls.Add(this.txtTotalCreditValue);
            this.grpFooter.Location = new System.Drawing.Point(2, 320);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1093, 42);
            this.grpFooter.TabIndex = 31;
            this.grpFooter.TabStop = false;
            // 
            // txtTotalDebitValue
            // 
            this.txtTotalDebitValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalDebitValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalDebitValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalDebitValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalDebitValue.Location = new System.Drawing.Point(832, 12);
            this.txtTotalDebitValue.Name = "txtTotalDebitValue";
            this.txtTotalDebitValue.ReadOnly = true;
            this.txtTotalDebitValue.Size = new System.Drawing.Size(123, 21);
            this.txtTotalDebitValue.TabIndex = 59;
            this.txtTotalDebitValue.Text = "0.00";
            this.txtTotalDebitValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(785, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 36;
            this.lblTotal.Text = "Total";
            // 
            // txtTotalCreditValue
            // 
            this.txtTotalCreditValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalCreditValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalCreditValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalCreditValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalCreditValue.Location = new System.Drawing.Point(958, 12);
            this.txtTotalCreditValue.Name = "txtTotalCreditValue";
            this.txtTotalCreditValue.ReadOnly = true;
            this.txtTotalCreditValue.Size = new System.Drawing.Size(123, 21);
            this.txtTotalCreditValue.TabIndex = 0;
            this.txtTotalCreditValue.Text = "0.00";
            this.txtTotalCreditValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmJournalEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1098, 404);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmJournalEntry";
            this.Text = "Journal Entry";
            this.Load += new System.EventHandler(this.FrmJournalEntry_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpJournalDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblJournalDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvJournal;
        private CustomControls.TextBoxCurrency txtLedgerValue;
        private System.Windows.Forms.TextBox txtLedgerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationLedger;
        private System.Windows.Forms.TextBox txtLedgerCode;
        private System.Windows.Forms.ComboBox cmbDrCrType;
        private System.Windows.Forms.TextBox txtManualNo;
        private System.Windows.Forms.Label lblManualNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private CustomControls.TextBoxCurrency txtTotalDebitValue;
        private System.Windows.Forms.Label lblTotal;
        private CustomControls.TextBoxCurrency txtTotalCreditValue;
        private System.Windows.Forms.RadioButton rdoPayable;
        private System.Windows.Forms.RadioButton rdoReceivables;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCentre;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DebitAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenceTempID;

    }
}
