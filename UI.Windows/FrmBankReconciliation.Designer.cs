namespace UI.Windows
{
    partial class FrmBankReconciliation
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
            this.dtpReconcileDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblDepositDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(601, 308);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 308);
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
            this.grpHeader.Controls.Add(this.dtpReconcileDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblDepositDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(915, 110);
            this.grpHeader.TabIndex = 24;
            this.grpHeader.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
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
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(90, 38);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 105;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            // 
            // txtBankBookName
            // 
            this.txtBankBookName.Location = new System.Drawing.Point(240, 35);
            this.txtBankBookName.MasterDescription = "";
            this.txtBankBookName.Name = "txtBankBookName";
            this.txtBankBookName.Size = new System.Drawing.Size(341, 21);
            this.txtBankBookName.TabIndex = 104;
            // 
            // txtBankBookCode
            // 
            this.txtBankBookCode.Location = new System.Drawing.Point(107, 35);
            this.txtBankBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBankBookCode.Name = "txtBankBookCode";
            this.txtBankBookCode.Size = new System.Drawing.Size(130, 21);
            this.txtBankBookCode.TabIndex = 103;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(702, 77);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(207, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(610, 81);
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
            this.cmbLocation.Location = new System.Drawing.Point(702, 55);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(207, 21);
            this.cmbLocation.TabIndex = 63;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(610, 58);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // dtpReconcileDate
            // 
            this.dtpReconcileDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReconcileDate.Location = new System.Drawing.Point(703, 11);
            this.dtpReconcileDate.Name = "dtpReconcileDate";
            this.dtpReconcileDate.Size = new System.Drawing.Size(207, 21);
            this.dtpReconcileDate.TabIndex = 22;
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
            this.lblRemark.Location = new System.Drawing.Point(6, 64);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 61);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblDepositDate
            // 
            this.lblDepositDate.AutoSize = true;
            this.lblDepositDate.Location = new System.Drawing.Point(610, 14);
            this.lblDepositDate.Name = "lblDepositDate";
            this.lblDepositDate.Size = new System.Drawing.Size(92, 13);
            this.lblDepositDate.TabIndex = 9;
            this.lblDepositDate.Text = "Reconcile Date";
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
            this.grpBody.Location = new System.Drawing.Point(2, 100);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(915, 208);
            this.grpBody.TabIndex = 25;
            this.grpBody.TabStop = false;
            // 
            // FrmBankReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(920, 356);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmBankReconciliation";
            this.Text = "Bank Reconciliation";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAutoCompleationPettyCash;
        private CustomControls.TextBoxMasterDescription txtBankBookName;
        private System.Windows.Forms.TextBox txtBankBookCode;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.DateTimePicker dtpReconcileDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblDepositDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
    }
}
