namespace UI.Windows
{
    partial class FrmPayDocumentList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tbpAdvanced = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationOtherExpence = new System.Windows.Forms.CheckBox();
            this.txtOtherExpenceCode = new System.Windows.Forms.TextBox();
            this.txtOtherExpenceName = new System.Windows.Forms.TextBox();
            this.txtOtherExpenceValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.dgvAdvanced = new System.Windows.Forms.DataGridView();
            this.OtherExpenceTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AmountToPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditsUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtDisplayNetAmount = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.textBoxCurrency1 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.textBoxCurrency2 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtPayingAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.tabGRN = new System.Windows.Forms.TabControl();
            this.grpBody.SuspendLayout();
            this.tbpAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.tbpGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabGRN.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tabGRN);
            this.grpBody.Location = new System.Drawing.Point(3, -2);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(1014, 265);
            this.grpBody.TabIndex = 17;
            this.grpBody.TabStop = false;
            // 
            // tbpAdvanced
            // 
            this.tbpAdvanced.Controls.Add(this.dgvAdvanced);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceValue);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceName);
            this.tbpAdvanced.Controls.Add(this.txtOtherExpenceCode);
            this.tbpAdvanced.Controls.Add(this.chkAutoCompleationOtherExpence);
            this.tbpAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tbpAdvanced.Name = "tbpAdvanced";
            this.tbpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAdvanced.Size = new System.Drawing.Size(1004, 224);
            this.tbpAdvanced.TabIndex = 1;
            this.tbpAdvanced.Text = "Advanced";
            this.tbpAdvanced.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationOtherExpence
            // 
            this.chkAutoCompleationOtherExpence.AutoSize = true;
            this.chkAutoCompleationOtherExpence.Checked = true;
            this.chkAutoCompleationOtherExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationOtherExpence.Location = new System.Drawing.Point(3, 205);
            this.chkAutoCompleationOtherExpence.Name = "chkAutoCompleationOtherExpence";
            this.chkAutoCompleationOtherExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationOtherExpence.TabIndex = 50;
            this.chkAutoCompleationOtherExpence.Tag = "1";
            this.chkAutoCompleationOtherExpence.UseVisualStyleBackColor = true;
            // 
            // txtOtherExpenceCode
            // 
            this.txtOtherExpenceCode.Location = new System.Drawing.Point(18, 202);
            this.txtOtherExpenceCode.Name = "txtOtherExpenceCode";
            this.txtOtherExpenceCode.Size = new System.Drawing.Size(151, 20);
            this.txtOtherExpenceCode.TabIndex = 49;
            // 
            // txtOtherExpenceName
            // 
            this.txtOtherExpenceName.Location = new System.Drawing.Point(170, 202);
            this.txtOtherExpenceName.Name = "txtOtherExpenceName";
            this.txtOtherExpenceName.Size = new System.Drawing.Size(348, 20);
            this.txtOtherExpenceName.TabIndex = 51;
            // 
            // txtOtherExpenceValue
            // 
            this.txtOtherExpenceValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherExpenceValue.Location = new System.Drawing.Point(519, 202);
            this.txtOtherExpenceValue.Name = "txtOtherExpenceValue";
            this.txtOtherExpenceValue.Size = new System.Drawing.Size(126, 20);
            this.txtOtherExpenceValue.TabIndex = 52;
            this.txtOtherExpenceValue.Tag = "3";
            this.txtOtherExpenceValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.dgvAdvanced.Location = new System.Drawing.Point(3, 0);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.RowHeadersWidth = 15;
            this.dgvAdvanced.Size = new System.Drawing.Size(1057, 201);
            this.dgvAdvanced.TabIndex = 34;
            // 
            // OtherExpenseTempID
            // 
            this.OtherExpenceTempID.DataPropertyName = "OtherExpenseTempID";
            this.OtherExpenceTempID.HeaderText = "OtherExpenseTempID";
            this.OtherExpenceTempID.Name = "OtherExpenseTempID";
            this.OtherExpenceTempID.ReadOnly = true;
            this.OtherExpenceTempID.Visible = false;
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
            this.LedgerCode.Width = 150;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.groupBox1);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(1004, 224);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPayingAmount);
            this.groupBox1.Controls.Add(this.textBoxCurrency2);
            this.groupBox1.Controls.Add(this.textBoxCurrency1);
            this.groupBox1.Controls.Add(this.txtGrossAmount);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.txtDisplayNetAmount);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(991, 212);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
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
            this.dataGridView1.Size = new System.Drawing.Size(985, 169);
            this.dataGridView1.TabIndex = 0;
            // 
            // AmountToPay
            // 
            this.AmountToPay.HeaderText = "Amount To Pay";
            this.AmountToPay.Name = "AmountToPay";
            this.AmountToPay.Width = 150;
            // 
            // CreditsUsed
            // 
            this.CreditsUsed.HeaderText = "Credits Used";
            this.CreditsUsed.Name = "CreditsUsed";
            this.CreditsUsed.Width = 125;
            // 
            // BalanceAmount
            // 
            this.BalanceAmount.HeaderText = "Balance Amount";
            this.BalanceAmount.Name = "BalanceAmount";
            this.BalanceAmount.Width = 150;
            // 
            // DueAmount
            // 
            this.DueAmount.HeaderText = "Due Amount";
            this.DueAmount.Name = "DueAmount";
            this.DueAmount.Width = 150;
            // 
            // DueDate
            // 
            this.DueDate.HeaderText = "Due Date";
            this.DueDate.Name = "DueDate";
            // 
            // ReferenceNo
            // 
            this.ReferenceNo.HeaderText = "Reference No";
            this.ReferenceNo.Name = "ReferenceNo";
            // 
            // DocumentNo
            // 
            this.DocumentNo.HeaderText = "Document No";
            this.DocumentNo.Name = "DocumentNo";
            this.DocumentNo.Width = 120;
            // 
            // SupplierName
            // 
            this.SupplierName.HeaderText = "Supplier Name";
            this.SupplierName.Name = "SupplierName";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 50;
            // 
            // txtDisplayNetAmount
            // 
            this.txtDisplayNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtDisplayNetAmount.Location = new System.Drawing.Point(47, 186);
            this.txtDisplayNetAmount.Name = "txtDisplayNetAmount";
            this.txtDisplayNetAmount.ReadOnly = true;
            this.txtDisplayNetAmount.Size = new System.Drawing.Size(125, 20);
            this.txtDisplayNetAmount.TabIndex = 80;
            this.txtDisplayNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBox1.Location = new System.Drawing.Point(178, 186);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(92, 20);
            this.textBox1.TabIndex = 81;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGrossAmount.Location = new System.Drawing.Point(276, 186);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(134, 20);
            this.txtGrossAmount.TabIndex = 82;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCurrency1
            // 
            this.textBoxCurrency1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency1.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency1.Location = new System.Drawing.Point(416, 187);
            this.textBoxCurrency1.Name = "textBoxCurrency1";
            this.textBoxCurrency1.ReadOnly = true;
            this.textBoxCurrency1.Size = new System.Drawing.Size(134, 20);
            this.textBoxCurrency1.TabIndex = 31;
            this.textBoxCurrency1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCurrency2
            // 
            this.textBoxCurrency2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxCurrency2.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency2.Location = new System.Drawing.Point(570, 187);
            this.textBoxCurrency2.Name = "textBoxCurrency2";
            this.textBoxCurrency2.ReadOnly = true;
            this.textBoxCurrency2.Size = new System.Drawing.Size(134, 20);
            this.textBoxCurrency2.TabIndex = 83;
            this.textBoxCurrency2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPayingAmount
            // 
            this.txtPayingAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPayingAmount.Location = new System.Drawing.Point(710, 186);
            this.txtPayingAmount.Name = "txtPayingAmount";
            this.txtPayingAmount.Size = new System.Drawing.Size(163, 20);
            this.txtPayingAmount.TabIndex = 84;
            this.txtPayingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabGRN
            // 
            this.tabGRN.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabGRN.Controls.Add(this.tbpGeneral);
            this.tabGRN.Controls.Add(this.tbpAdvanced);
            this.tabGRN.Location = new System.Drawing.Point(9, 131);
            this.tabGRN.Name = "tabGRN";
            this.tabGRN.SelectedIndex = 0;
            this.tabGRN.Size = new System.Drawing.Size(1012, 253);
            this.tabGRN.TabIndex = 1;
            // 
            // FrmPayDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 475);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmPayDocumentList";
            this.Text = "FrmPaymentOutDocument";
            this.grpBody.ResumeLayout(false);
            this.tbpAdvanced.ResumeLayout(false);
            this.tbpAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.tbpGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabGRN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TabControl tabGRN;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenceTempID;
        private CustomControls.TextBoxCurrency txtOtherExpenceValue;
        private System.Windows.Forms.TextBox txtOtherExpenceName;
        private System.Windows.Forms.TextBox txtOtherExpenceCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationOtherExpence;
    }
}