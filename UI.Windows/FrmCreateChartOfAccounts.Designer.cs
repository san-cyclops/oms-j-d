namespace UI.Windows
{
    partial class FrmCreateChartOfAccounts
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
            this.chkInactive = new System.Windows.Forms.CheckBox();
            this.txtAccountName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtAccountCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.cmbSubAccountsOf = new System.Windows.Forms.ComboBox();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoDetailAccount = new System.Windows.Forms.RadioButton();
            this.rdoHeaderAccount = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.lblGroupOfCompany = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationBranch = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationBank = new System.Windows.Forms.CheckBox();
            this.txtBankName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBranchName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBranchCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtBankCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.textBoxMasterCode1 = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 262);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(637, 262);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkInactive);
            this.groupBox1.Controls.Add(this.txtAccountName);
            this.groupBox1.Controls.Add(this.txtAccountCode);
            this.groupBox1.Controls.Add(this.cmbSubAccountsOf);
            this.groupBox1.Controls.Add(this.cmbAccountType);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoDetailAccount);
            this.groupBox1.Controls.Add(this.rdoHeaderAccount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 178);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // chkInactive
            // 
            this.chkInactive.AutoSize = true;
            this.chkInactive.Location = new System.Drawing.Point(356, 124);
            this.chkInactive.Name = "chkInactive";
            this.chkInactive.Size = new System.Drawing.Size(72, 17);
            this.chkInactive.TabIndex = 70;
            this.chkInactive.Tag = "1";
            this.chkInactive.Text = "Inactive";
            this.chkInactive.UseVisualStyleBackColor = true;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(152, 149);
            this.txtAccountName.MasterDescription = "";
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(275, 21);
            this.txtAccountName.TabIndex = 11;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.IsAutoComplete = false;
            this.txtAccountCode.ItemCollection = null;
            this.txtAccountCode.Location = new System.Drawing.Point(152, 122);
            this.txtAccountCode.MasterCode = "";
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(158, 21);
            this.txtAccountCode.TabIndex = 10;
            // 
            // cmbSubAccountsOf
            // 
            this.cmbSubAccountsOf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubAccountsOf.FormattingEnabled = true;
            this.cmbSubAccountsOf.Location = new System.Drawing.Point(152, 95);
            this.cmbSubAccountsOf.Name = "cmbSubAccountsOf";
            this.cmbSubAccountsOf.Size = new System.Drawing.Size(275, 21);
            this.cmbSubAccountsOf.TabIndex = 9;
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Location = new System.Drawing.Point(152, 68);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(275, 21);
            this.cmbAccountType.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(152, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(275, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Account Name*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Account Code*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sub Accounts Of*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Account Type*";
            // 
            // rdoDetailAccount
            // 
            this.rdoDetailAccount.AutoSize = true;
            this.rdoDetailAccount.Location = new System.Drawing.Point(320, 15);
            this.rdoDetailAccount.Name = "rdoDetailAccount";
            this.rdoDetailAccount.Size = new System.Drawing.Size(107, 17);
            this.rdoDetailAccount.TabIndex = 2;
            this.rdoDetailAccount.TabStop = true;
            this.rdoDetailAccount.Text = "Detail Account";
            this.rdoDetailAccount.UseVisualStyleBackColor = true;
            // 
            // rdoHeaderAccount
            // 
            this.rdoHeaderAccount.AutoSize = true;
            this.rdoHeaderAccount.Location = new System.Drawing.Point(152, 15);
            this.rdoHeaderAccount.Name = "rdoHeaderAccount";
            this.rdoHeaderAccount.Size = new System.Drawing.Size(115, 17);
            this.rdoHeaderAccount.TabIndex = 1;
            this.rdoHeaderAccount.TabStop = true;
            this.rdoHeaderAccount.Text = "Header Account";
            this.rdoHeaderAccount.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Classification*";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbLocation);
            this.groupBox2.Controls.Add(this.cmbCostCentre);
            this.groupBox2.Controls.Add(this.cmbCompany);
            this.groupBox2.Controls.Add(this.lblGroupOfCompany);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(2, -6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(874, 100);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(153, 39);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(275, 21);
            this.cmbLocation.TabIndex = 69;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(153, 66);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(275, 21);
            this.cmbCostCentre.TabIndex = 68;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(153, 12);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(275, 21);
            this.cmbCompany.TabIndex = 51;
            // 
            // lblGroupOfCompany
            // 
            this.lblGroupOfCompany.AutoSize = true;
            this.lblGroupOfCompany.Location = new System.Drawing.Point(7, 15);
            this.lblGroupOfCompany.Name = "lblGroupOfCompany";
            this.lblGroupOfCompany.Size = new System.Drawing.Size(69, 13);
            this.lblGroupOfCompany.TabIndex = 50;
            this.lblGroupOfCompany.Text = "Company*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cost Center";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Location *";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAutoCompleationBranch);
            this.groupBox3.Controls.Add(this.chkAutoCompleationBank);
            this.groupBox3.Controls.Add(this.txtBankName);
            this.groupBox3.Controls.Add(this.txtBranchName);
            this.groupBox3.Controls.Add(this.txtBranchCode);
            this.groupBox3.Controls.Add(this.txtBankCode);
            this.groupBox3.Controls.Add(this.textBoxMasterCode1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(454, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 178);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // chkAutoCompleationBranch
            // 
            this.chkAutoCompleationBranch.AutoSize = true;
            this.chkAutoCompleationBranch.Location = new System.Drawing.Point(76, 71);
            this.chkAutoCompleationBranch.Name = "chkAutoCompleationBranch";
            this.chkAutoCompleationBranch.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBranch.TabIndex = 21;
            this.chkAutoCompleationBranch.Tag = "1";
            this.chkAutoCompleationBranch.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationBank
            // 
            this.chkAutoCompleationBank.AutoSize = true;
            this.chkAutoCompleationBank.Location = new System.Drawing.Point(76, 44);
            this.chkAutoCompleationBank.Name = "chkAutoCompleationBank";
            this.chkAutoCompleationBank.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBank.TabIndex = 20;
            this.chkAutoCompleationBank.Tag = "1";
            this.chkAutoCompleationBank.UseVisualStyleBackColor = true;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(171, 41);
            this.txtBankName.MasterDescription = "";
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(246, 21);
            this.txtBankName.TabIndex = 19;
            // 
            // txtBranchName
            // 
            this.txtBranchName.Location = new System.Drawing.Point(171, 68);
            this.txtBranchName.MasterDescription = "";
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(246, 21);
            this.txtBranchName.TabIndex = 18;
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.IsAutoComplete = false;
            this.txtBranchCode.ItemCollection = null;
            this.txtBranchCode.Location = new System.Drawing.Point(95, 68);
            this.txtBranchCode.MasterCode = "";
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.Size = new System.Drawing.Size(74, 21);
            this.txtBranchCode.TabIndex = 17;
            // 
            // txtBankCode
            // 
            this.txtBankCode.IsAutoComplete = false;
            this.txtBankCode.ItemCollection = null;
            this.txtBankCode.Location = new System.Drawing.Point(95, 41);
            this.txtBankCode.MasterCode = "";
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(74, 21);
            this.txtBankCode.TabIndex = 12;
            // 
            // textBoxMasterCode1
            // 
            this.textBoxMasterCode1.IsAutoComplete = false;
            this.textBoxMasterCode1.ItemCollection = null;
            this.textBoxMasterCode1.Location = new System.Drawing.Point(95, 95);
            this.textBoxMasterCode1.MasterCode = "";
            this.textBoxMasterCode1.Name = "textBoxMasterCode1";
            this.textBoxMasterCode1.Size = new System.Drawing.Size(322, 21);
            this.textBoxMasterCode1.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Account No";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Branch";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Bank";
            // 
            // FrmCreateChartOfAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(878, 311);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCreateChartOfAccounts";
            this.Load += new System.EventHandler(this.FrmCreateChartOfAccounts_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoDetailAccount;
        private System.Windows.Forms.RadioButton rdoHeaderAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSubAccountsOf;
        private System.Windows.Forms.ComboBox cmbAccountType;
        private System.Windows.Forms.TextBox textBox1;
        private CustomControls.TextBoxMasterDescription txtAccountName;
        private CustomControls.TextBoxMasterCode txtAccountCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label lblGroupOfCompany;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkAutoCompleationBranch;
        private System.Windows.Forms.CheckBox chkAutoCompleationBank;
        private CustomControls.TextBoxMasterDescription txtBankName;
        private CustomControls.TextBoxMasterDescription txtBranchName;
        private CustomControls.TextBoxMasterCode txtBranchCode;
        private CustomControls.TextBoxMasterCode txtBankCode;
        private CustomControls.TextBoxMasterCode textBoxMasterCode1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkInactive;
    }
}
