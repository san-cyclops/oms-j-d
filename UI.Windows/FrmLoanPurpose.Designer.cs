namespace UI.Windows
{
    partial class FrmLoanPurpose
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
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtPurposeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtPurposeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblLoanPurpose = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 129);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(348, 129);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.txtPurposeCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationDepartment);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtPurposeName);
            this.groupBox1.Controls.Add(this.lblCode);
            this.groupBox1.Controls.Add(this.lblLoanPurpose);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(585, 139);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoClear.Location = new System.Drawing.Point(489, 114);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtPurposeCode
            // 
            this.txtPurposeCode.IsAutoComplete = false;
            this.txtPurposeCode.ItemCollection = null;
            this.txtPurposeCode.Location = new System.Drawing.Point(118, 23);
            this.txtPurposeCode.MasterCode = "";
            this.txtPurposeCode.Name = "txtPurposeCode";
            this.txtPurposeCode.Size = new System.Drawing.Size(124, 21);
            this.txtPurposeCode.TabIndex = 0;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(244, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(95, 26);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 14;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(118, 71);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(458, 37);
            this.txtRemark.TabIndex = 3;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 79);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtPurposeName
            // 
            this.txtPurposeName.Location = new System.Drawing.Point(118, 47);
            this.txtPurposeName.MasterDescription = "";
            this.txtPurposeName.Name = "txtPurposeName";
            this.txtPurposeName.Size = new System.Drawing.Size(458, 21);
            this.txtPurposeName.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(10, 26);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(44, 13);
            this.lblCode.TabIndex = 9;
            this.lblCode.Text = "Code*";
            // 
            // lblLoanPurpose
            // 
            this.lblLoanPurpose.AutoSize = true;
            this.lblLoanPurpose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoanPurpose.Location = new System.Drawing.Point(10, 52);
            this.lblLoanPurpose.Name = "lblLoanPurpose";
            this.lblLoanPurpose.Size = new System.Drawing.Size(91, 13);
            this.lblLoanPurpose.TabIndex = 10;
            this.lblLoanPurpose.Text = "Loan Purpose*";
            // 
            // FrmLoanPurpose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(589, 178);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLoanPurpose";
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
        private CustomControls.TextBoxMasterCode txtPurposeCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtPurposeName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblLoanPurpose;
    }
}
