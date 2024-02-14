namespace UI.Windows
{
    partial class FrmChequeBookRegister
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
            this.lblStartingNo = new System.Windows.Forms.Label();
            this.lblNoOfPages = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtBankBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBankBookCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBranchName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBankName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBranchCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBankCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 293);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(413, 293);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.lblStartingNo);
            this.grpHeader.Controls.Add(this.lblNoOfPages);
            this.grpHeader.Controls.Add(this.dtpDate);
            this.grpHeader.Controls.Add(this.lblDate);
            this.grpHeader.Controls.Add(this.txtBankBookName);
            this.grpHeader.Controls.Add(this.txtBankBookCode);
            this.grpHeader.Controls.Add(this.txtBranchName);
            this.grpHeader.Controls.Add(this.txtBankName);
            this.grpHeader.Controls.Add(this.txtBranchCode);
            this.grpHeader.Controls.Add(this.txtBankCode);
            this.grpHeader.Controls.Add(this.lblBranch);
            this.grpHeader.Controls.Add(this.lblBank);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(650, 163);
            this.grpHeader.TabIndex = 10;
            this.grpHeader.TabStop = false;
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AutoSize = true;
            this.lblStartingNo.Location = new System.Drawing.Point(12, 141);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(71, 13);
            this.lblStartingNo.TabIndex = 68;
            this.lblStartingNo.Text = "Starting No";
            // 
            // lblNoOfPages
            // 
            this.lblNoOfPages.AutoSize = true;
            this.lblNoOfPages.Location = new System.Drawing.Point(12, 117);
            this.lblNoOfPages.Name = "lblNoOfPages";
            this.lblNoOfPages.Size = new System.Drawing.Size(75, 13);
            this.lblNoOfPages.TabIndex = 67;
            this.lblNoOfPages.Text = "No of Pages";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(114, 83);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(122, 21);
            this.dtpDate.TabIndex = 66;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 89);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 13);
            this.lblDate.TabIndex = 65;
            this.lblDate.Text = "Date";
            // 
            // txtBankBookName
            // 
            this.txtBankBookName.Location = new System.Drawing.Point(216, 10);
            this.txtBankBookName.MasterDescription = "";
            this.txtBankBookName.Name = "txtBankBookName";
            this.txtBankBookName.Size = new System.Drawing.Size(352, 21);
            this.txtBankBookName.TabIndex = 64;
            // 
            // txtBankBookCode
            // 
            this.txtBankBookCode.IsAutoComplete = false;
            this.txtBankBookCode.ItemCollection = null;
            this.txtBankBookCode.Location = new System.Drawing.Point(114, 10);
            this.txtBankBookCode.MasterCode = "";
            this.txtBankBookCode.Name = "txtBankBookCode";
            this.txtBankBookCode.Size = new System.Drawing.Size(100, 21);
            this.txtBankBookCode.TabIndex = 63;
            // 
            // txtBranchName
            // 
            this.txtBranchName.Enabled = false;
            this.txtBranchName.Location = new System.Drawing.Point(177, 58);
            this.txtBranchName.MasterDescription = "";
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(391, 21);
            this.txtBranchName.TabIndex = 62;
            // 
            // txtBankName
            // 
            this.txtBankName.Enabled = false;
            this.txtBankName.Location = new System.Drawing.Point(177, 34);
            this.txtBankName.MasterDescription = "";
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(391, 21);
            this.txtBankName.TabIndex = 61;
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.Enabled = false;
            this.txtBranchCode.IsAutoComplete = false;
            this.txtBranchCode.ItemCollection = null;
            this.txtBranchCode.Location = new System.Drawing.Point(114, 58);
            this.txtBranchCode.MasterCode = "";
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.Size = new System.Drawing.Size(61, 21);
            this.txtBranchCode.TabIndex = 60;
            // 
            // txtBankCode
            // 
            this.txtBankCode.Enabled = false;
            this.txtBankCode.IsAutoComplete = false;
            this.txtBankCode.ItemCollection = null;
            this.txtBankCode.Location = new System.Drawing.Point(114, 34);
            this.txtBankCode.MasterCode = "";
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(61, 21);
            this.txtBankCode.TabIndex = 59;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(10, 66);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(80, 13);
            this.lblBranch.TabIndex = 58;
            this.lblBranch.Text = "Bank Branch";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(10, 14);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(36, 13);
            this.lblBank.TabIndex = 56;
            this.lblBank.Text = "Bank";
            // 
            // grpBody
            // 
            this.grpBody.Location = new System.Drawing.Point(2, 152);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(650, 146);
            this.grpBody.TabIndex = 11;
            this.grpBody.TabStop = false;
            // 
            // FrmChequeBookRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(654, 342);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmChequeBookRegister";
            this.Text = "Cheque Book Register";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.Label lblBank;
        private CustomControls.TextBoxMasterDescription txtBankBookName;
        private CustomControls.TextBoxMasterCode txtBankBookCode;
        private CustomControls.TextBoxMasterDescription txtBranchName;
        private CustomControls.TextBoxMasterDescription txtBankName;
        private CustomControls.TextBoxMasterCode txtBranchCode;
        private CustomControls.TextBoxMasterCode txtBankCode;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblStartingNo;
        private System.Windows.Forms.Label lblNoOfPages;
        private System.Windows.Forms.GroupBox grpBody;
    }
}
