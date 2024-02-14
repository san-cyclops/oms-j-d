namespace UI.Windows
{
    partial class FrmPettyCashMasterCreation
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
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.chkAutoCompleationLedger = new System.Windows.Forms.CheckBox();
            this.txtLedgerName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtLedgerCode = new System.Windows.Forms.TextBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 96);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(412, 96);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.chkAutoCompleationLedger);
            this.groupBox1.Controls.Add(this.txtLedgerName);
            this.groupBox1.Controls.Add(this.txtLedgerCode);
            this.groupBox1.Controls.Add(this.lblLedger);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Location = new System.Drawing.Point(3, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(648, 105);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(8, 74);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 13);
            this.lblAmount.TabIndex = 100;
            this.lblAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Location = new System.Drawing.Point(138, 71);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(150, 21);
            this.txtAmount.TabIndex = 99;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(138, 17);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(330, 21);
            this.cmbLocation.TabIndex = 97;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(8, 20);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 98;
            this.lblLocation.Text = "Location";
            // 
            // chkAutoCompleationLedger
            // 
            this.chkAutoCompleationLedger.AutoSize = true;
            this.chkAutoCompleationLedger.Checked = true;
            this.chkAutoCompleationLedger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLedger.Location = new System.Drawing.Point(116, 47);
            this.chkAutoCompleationLedger.Name = "chkAutoCompleationLedger";
            this.chkAutoCompleationLedger.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLedger.TabIndex = 96;
            this.chkAutoCompleationLedger.Tag = "1";
            this.chkAutoCompleationLedger.UseVisualStyleBackColor = true;
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(256, 44);
            this.txtLedgerName.MasterDescription = "";
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(374, 21);
            this.txtLedgerName.TabIndex = 94;
            this.txtLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerName_KeyDown);
            this.txtLedgerName.Leave += new System.EventHandler(this.txtLedgerName_Leave);
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(138, 44);
            this.txtLedgerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Size = new System.Drawing.Size(116, 21);
            this.txtLedgerCode.TabIndex = 93;
            this.txtLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerCode_KeyDown);
            this.txtLedgerCode.Leave += new System.EventHandler(this.txtLedgerCode_Leave);
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(8, 47);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(102, 13);
            this.lblLedger.TabIndex = 95;
            this.lblLedger.Text = "Petty Cash Book";
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(547, 75);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // FrmPettyCashMasterCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(653, 145);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPettyCashMasterCreation";
            this.Text = "Petty Cash Master Creation";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.CheckBox chkAutoCompleationLedger;
        private CustomControls.TextBoxMasterDescription txtLedgerName;
        private System.Windows.Forms.TextBox txtLedgerCode;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblAmount;
        private CustomControls.TextBoxCurrency txtAmount;
    }
}
