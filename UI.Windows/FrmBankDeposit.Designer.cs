namespace UI.Windows
{
    partial class FrmBankDeposit
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoCompleationPettyCash = new System.Windows.Forms.CheckBox();
            this.txtBankBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBankBookCode = new System.Windows.Forms.TextBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDepositDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblDepositDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCashBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCashBookCode = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationCashBook = new System.Windows.Forms.CheckBox();
            this.lblCashBook = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(602, 313);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 313);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPettyCash);
            this.grpHeader.Controls.Add(this.txtBankBookName);
            this.grpHeader.Controls.Add(this.txtBankBookCode);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDepositDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblDepositDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(917, 110);
            this.grpHeader.TabIndex = 23;
            this.grpHeader.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Bank";
            // 
            // chkAutoCompleationPettyCash
            // 
            this.chkAutoCompleationPettyCash.AutoSize = true;
            this.chkAutoCompleationPettyCash.Checked = true;
            this.chkAutoCompleationPettyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(90, 62);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 105;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            // 
            // txtBankBookName
            // 
            this.txtBankBookName.Location = new System.Drawing.Point(240, 59);
            this.txtBankBookName.MasterDescription = "";
            this.txtBankBookName.Name = "txtBankBookName";
            this.txtBankBookName.Size = new System.Drawing.Size(341, 21);
            this.txtBankBookName.TabIndex = 104;
            // 
            // txtBankBookCode
            // 
            this.txtBankBookCode.Location = new System.Drawing.Point(107, 59);
            this.txtBankBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBankBookCode.Name = "txtBankBookCode";
            this.txtBankBookCode.Size = new System.Drawing.Size(130, 21);
            this.txtBankBookCode.TabIndex = 103;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(374, 35);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(207, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(292, 38);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 101;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTStatus.Location = new System.Drawing.Point(896, 35);
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
            this.txtReferenceNo.Location = new System.Drawing.Point(107, 35);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(177, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 38);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDepositDate
            // 
            this.dtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDepositDate.Location = new System.Drawing.Point(703, 11);
            this.dtpDepositDate.Name = "dtpDepositDate";
            this.dtpDepositDate.Size = new System.Drawing.Size(207, 21);
            this.dtpDepositDate.TabIndex = 22;
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
            this.lblRemark.Location = new System.Drawing.Point(6, 88);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 85);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblDepositDate
            // 
            this.lblDepositDate.AutoSize = true;
            this.lblDepositDate.Location = new System.Drawing.Point(610, 14);
            this.lblDepositDate.Name = "lblDepositDate";
            this.lblDepositDate.Size = new System.Drawing.Size(81, 13);
            this.lblDepositDate.TabIndex = 9;
            this.lblDepositDate.Text = "Deposit Date";
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
            // grpBody
            // 
            this.grpBody.Location = new System.Drawing.Point(2, 150);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(917, 168);
            this.grpBody.TabIndex = 24;
            this.grpBody.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCashBookName);
            this.groupBox1.Controls.Add(this.txtCashBookCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationCashBook);
            this.groupBox1.Controls.Add(this.lblCashBook);
            this.groupBox1.Location = new System.Drawing.Point(2, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(917, 54);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // txtCashBookName
            // 
            this.txtCashBookName.Location = new System.Drawing.Point(242, 14);
            this.txtCashBookName.MasterDescription = "";
            this.txtCashBookName.Name = "txtCashBookName";
            this.txtCashBookName.Size = new System.Drawing.Size(341, 21);
            this.txtCashBookName.TabIndex = 111;
            // 
            // txtCashBookCode
            // 
            this.txtCashBookCode.Location = new System.Drawing.Point(109, 14);
            this.txtCashBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCashBookCode.Name = "txtCashBookCode";
            this.txtCashBookCode.Size = new System.Drawing.Size(130, 21);
            this.txtCashBookCode.TabIndex = 110;
            // 
            // chkAutoCompleationCashBook
            // 
            this.chkAutoCompleationCashBook.AutoSize = true;
            this.chkAutoCompleationCashBook.Checked = true;
            this.chkAutoCompleationCashBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCashBook.Location = new System.Drawing.Point(94, 17);
            this.chkAutoCompleationCashBook.Name = "chkAutoCompleationCashBook";
            this.chkAutoCompleationCashBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCashBook.TabIndex = 109;
            this.chkAutoCompleationCashBook.Tag = "1";
            this.chkAutoCompleationCashBook.UseVisualStyleBackColor = true;
            // 
            // lblCashBook
            // 
            this.lblCashBook.AutoSize = true;
            this.lblCashBook.Location = new System.Drawing.Point(8, 17);
            this.lblCashBook.Name = "lblCashBook";
            this.lblCashBook.Size = new System.Drawing.Size(69, 13);
            this.lblCashBook.TabIndex = 108;
            this.lblCashBook.Text = "Cash Book";
            // 
            // FrmBankDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(921, 361);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmBankDeposit";
            this.Text = "Bank Deposit";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDepositDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblDepositDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAutoCompleationPettyCash;
        private CustomControls.TextBoxMasterDescription txtBankBookName;
        private System.Windows.Forms.TextBox txtBankBookCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoCompleationCashBook;
        private System.Windows.Forms.Label lblCashBook;
        private CustomControls.TextBoxMasterDescription txtCashBookName;
        private System.Windows.Forms.TextBox txtCashBookCode;
    }
}
