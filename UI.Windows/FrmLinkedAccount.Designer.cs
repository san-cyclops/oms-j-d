namespace UI.Windows
{
    partial class FrmLinkedAccount
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.cmbDefinition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTransaction = new System.Windows.Forms.ComboBox();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDrCrType = new System.Windows.Forms.ComboBox();
            this.txtLedgerPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationLedgerAccount = new System.Windows.Forms.CheckBox();
            this.txtLedgerName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtLedgerCode = new System.Windows.Forms.TextBox();
            this.dgvLinkedAccount = new System.Windows.Forms.DataGridView();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrCrType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 277);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(413, 277);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.cmbDefinition);
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.cmbTransaction);
            this.grpHeader.Controls.Add(this.lblTransaction);
            this.grpHeader.Location = new System.Drawing.Point(3, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(648, 68);
            this.grpHeader.TabIndex = 10;
            this.grpHeader.TabStop = false;
            // 
            // cmbDefinition
            // 
            this.cmbDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefinition.FormattingEnabled = true;
            this.cmbDefinition.Location = new System.Drawing.Point(107, 37);
            this.cmbDefinition.Name = "cmbDefinition";
            this.cmbDefinition.Size = new System.Drawing.Size(293, 21);
            this.cmbDefinition.TabIndex = 3;
            this.cmbDefinition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDefinition_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Definition";
            // 
            // cmbTransaction
            // 
            this.cmbTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransaction.FormattingEnabled = true;
            this.cmbTransaction.Location = new System.Drawing.Point(107, 14);
            this.cmbTransaction.Name = "cmbTransaction";
            this.cmbTransaction.Size = new System.Drawing.Size(293, 21);
            this.cmbTransaction.TabIndex = 1;
            this.cmbTransaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransaction_KeyDown);
            // 
            // lblTransaction
            // 
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.Location = new System.Drawing.Point(9, 17);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(72, 13);
            this.lblTransaction.TabIndex = 0;
            this.lblTransaction.Text = "Transaction";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbDrCrType);
            this.groupBox2.Controls.Add(this.txtLedgerPercentage);
            this.groupBox2.Controls.Add(this.chkAutoCompleationLedgerAccount);
            this.groupBox2.Controls.Add(this.txtLedgerName);
            this.groupBox2.Controls.Add(this.txtLedgerCode);
            this.groupBox2.Controls.Add(this.dgvLinkedAccount);
            this.groupBox2.Location = new System.Drawing.Point(3, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 222);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // cmbDrCrType
            // 
            this.cmbDrCrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrCrType.FormattingEnabled = true;
            this.cmbDrCrType.Items.AddRange(new object[] {
            "Dr",
            "Cr"});
            this.cmbDrCrType.Location = new System.Drawing.Point(505, 194);
            this.cmbDrCrType.Name = "cmbDrCrType";
            this.cmbDrCrType.Size = new System.Drawing.Size(55, 21);
            this.cmbDrCrType.TabIndex = 118;
            this.cmbDrCrType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDrCrType_KeyDown);
            // 
            // txtLedgerPercentage
            // 
            this.txtLedgerPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLedgerPercentage.Location = new System.Drawing.Point(562, 194);
            this.txtLedgerPercentage.Name = "txtLedgerPercentage";
            this.txtLedgerPercentage.Size = new System.Drawing.Size(83, 21);
            this.txtLedgerPercentage.TabIndex = 117;
            this.txtLedgerPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLedgerPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerPercentage_KeyDown);
            // 
            // chkAutoCompleationLedgerAccount
            // 
            this.chkAutoCompleationLedgerAccount.AutoSize = true;
            this.chkAutoCompleationLedgerAccount.Checked = true;
            this.chkAutoCompleationLedgerAccount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLedgerAccount.Location = new System.Drawing.Point(5, 197);
            this.chkAutoCompleationLedgerAccount.Name = "chkAutoCompleationLedgerAccount";
            this.chkAutoCompleationLedgerAccount.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLedgerAccount.TabIndex = 116;
            this.chkAutoCompleationLedgerAccount.Tag = "1";
            this.chkAutoCompleationLedgerAccount.UseVisualStyleBackColor = true;
            this.chkAutoCompleationLedgerAccount.CheckedChanged += new System.EventHandler(this.chkAutoCompleationLedgerAccount_CheckedChanged);
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(143, 194);
            this.txtLedgerName.MasterDescription = "";
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(361, 21);
            this.txtLedgerName.TabIndex = 115;
            this.txtLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerName_KeyDown);
            this.txtLedgerName.Leave += new System.EventHandler(this.txtLedgerName_Leave);
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(22, 194);
            this.txtLedgerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Size = new System.Drawing.Size(120, 21);
            this.txtLedgerCode.TabIndex = 114;
            this.txtLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerCode_KeyDown);
            this.txtLedgerCode.Leave += new System.EventHandler(this.txtLedgerCode_Leave);
            // 
            // dgvLinkedAccount
            // 
            this.dgvLinkedAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLinkedAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccLedgerAccountID,
            this.LedgerCode,
            this.LedgerName,
            this.DrCrType,
            this.Percentage});
            this.dgvLinkedAccount.Location = new System.Drawing.Point(8, 17);
            this.dgvLinkedAccount.Name = "dgvLinkedAccount";
            this.dgvLinkedAccount.Size = new System.Drawing.Size(636, 171);
            this.dgvLinkedAccount.TabIndex = 0;
            // 
            // AccLedgerAccountID
            // 
            this.AccLedgerAccountID.DataPropertyName = "AccLedgerAccountID";
            this.AccLedgerAccountID.HeaderText = "AccLedgerAccountID";
            this.AccLedgerAccountID.Name = "AccLedgerAccountID";
            this.AccLedgerAccountID.Visible = false;
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "LedgerCode";
            this.LedgerCode.Name = "LedgerCode";
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "LedgerName";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.Width = 355;
            // 
            // DrCrType
            // 
            this.DrCrType.DataPropertyName = "DrCrType";
            this.DrCrType.HeaderText = "DrCr";
            this.DrCrType.Name = "DrCrType";
            this.DrCrType.Width = 55;
            // 
            // Percentage
            // 
            this.Percentage.DataPropertyName = "LedgerPercentage";
            this.Percentage.HeaderText = "Percentage";
            this.Percentage.Name = "Percentage";
            this.Percentage.Width = 80;
            // 
            // FrmLinkedAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(654, 326);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmLinkedAccount";
            this.Text = "Linked Account";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLinkedAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbTransaction;
        private System.Windows.Forms.Label lblTransaction;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvLinkedAccount;
        private System.Windows.Forms.CheckBox chkAutoCompleationLedgerAccount;
        private CustomControls.TextBoxMasterDescription txtLedgerName;
        private System.Windows.Forms.TextBox txtLedgerCode;
        private CustomControls.TextBoxCurrency txtLedgerPercentage;
        private System.Windows.Forms.ComboBox cmbDrCrType;
        private System.Windows.Forms.ComboBox cmbDefinition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrCrType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percentage;
    }
}
