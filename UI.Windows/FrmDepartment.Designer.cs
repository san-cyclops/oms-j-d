namespace UI.Windows
{
    partial class FrmDepartment
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
            this.lblDepartmentCode = new System.Windows.Forms.Label();
            this.lblDepartmentName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtDepartmentName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 129);
            this.grpButtonSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet.TabIndex = 12;
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(399, 129);
            this.grpButtonSet2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet2.TabIndex = 13;
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 9;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 8;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 5;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 6;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblDepartmentCode
            // 
            this.lblDepartmentCode.AutoSize = true;
            this.lblDepartmentCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentCode.Location = new System.Drawing.Point(10, 26);
            this.lblDepartmentCode.Name = "lblDepartmentCode";
            this.lblDepartmentCode.Size = new System.Drawing.Size(44, 13);
            this.lblDepartmentCode.TabIndex = 9;
            this.lblDepartmentCode.Text = "Code*";
            // 
            // lblDepartmentName
            // 
            this.lblDepartmentName.AutoSize = true;
            this.lblDepartmentName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentName.Location = new System.Drawing.Point(10, 52);
            this.lblDepartmentName.Name = "lblDepartmentName";
            this.lblDepartmentName.Size = new System.Drawing.Size(47, 13);
            this.lblDepartmentName.TabIndex = 10;
            this.lblDepartmentName.Text = "Name*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.txtDepartmentCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationDepartment);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtDepartmentName);
            this.groupBox1.Controls.Add(this.lblDepartmentCode);
            this.groupBox1.Controls.Add(this.lblDepartmentName);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(636, 139);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoClear.Location = new System.Drawing.Point(543, 114);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.IsAutoComplete = false;
            this.txtDepartmentCode.ItemCollection = null;
            this.txtDepartmentCode.Location = new System.Drawing.Point(172, 23);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(124, 21);
            this.txtDepartmentCode.TabIndex = 0;
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Leave += new System.EventHandler(this.txtDepartmentCode_Leave);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(298, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(149, 26);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 14;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDepartment_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(172, 71);
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
            // txtDepartmentName
            // 
            this.txtDepartmentName.Location = new System.Drawing.Point(172, 47);
            this.txtDepartmentName.MasterDescription = "";
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(458, 21);
            this.txtDepartmentName.TabIndex = 2;
            this.txtDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentName_KeyDown);
            this.txtDepartmentName.Leave += new System.EventHandler(this.txtDepartmentName_Leave);
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 177);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmDepartment";
            this.Text = "Department";
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDepartmentCode;
        private System.Windows.Forms.Label lblDepartmentName;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxMasterDescription txtDepartmentName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.Button btnNew;
        private CustomControls.TextBoxMasterCode txtDepartmentCode;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
