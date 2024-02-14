namespace UI.Windows
{
    partial class FrmLogisticDepartment
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
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtDepartmentName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblDepartmentCode = new System.Windows.Forms.Label();
            this.lblDepartmentName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(3, 129);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(401, 128);
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
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(636, 137);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
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
            this.txtDepartmentCode.Location = new System.Drawing.Point(195, 23);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(159, 21);
            this.txtDepartmentCode.TabIndex = 0;
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Leave += new System.EventHandler(this.txtDepartmentCode_Leave);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(357, 21);
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
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(174, 26);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 14;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDepartment_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(195, 71);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(435, 37);
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
            this.txtDepartmentName.Location = new System.Drawing.Point(195, 47);
            this.txtDepartmentName.MasterDescription = "";
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(435, 21);
            this.txtDepartmentName.TabIndex = 2;
            this.txtDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentName_KeyDown);
            this.txtDepartmentName.Leave += new System.EventHandler(this.txtDepartmentName_Leave);
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
            // FrmLogisticDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 177);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmLogisticDepartment";
            this.Text = "Logistic Department";
            this.Load += new System.EventHandler(this.FrmLogisticDepartment_Load);
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
        private CustomControls.TextBoxMasterCode txtDepartmentCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtDepartmentName;
        private System.Windows.Forms.Label lblDepartmentCode;
        private System.Windows.Forms.Label lblDepartmentName;
    }
}