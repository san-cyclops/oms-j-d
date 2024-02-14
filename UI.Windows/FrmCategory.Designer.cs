namespace UI.Windows
{
    partial class FrmCategory
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
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtDepartmentName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblCategoryDescription = new System.Windows.Forms.Label();
            this.lblCategoryCode = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 150);
            this.grpButtonSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(396, 150);
            this.grpButtonSet2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 12;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 11;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 10;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 9;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoCompleationCategory);
            this.groupBox1.Controls.Add(this.txtDepartmentCode);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationDepartment);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtDepartmentName);
            this.groupBox1.Controls.Add(this.txtCategoryName);
            this.groupBox1.Controls.Add(this.txtCategoryCode);
            this.groupBox1.Controls.Add(this.lblCategoryDescription);
            this.groupBox1.Controls.Add(this.lblCategoryCode);
            this.groupBox1.Controls.Add(this.lblDepartment);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(634, 160);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoCompleationCategory
            // 
            this.chkAutoCompleationCategory.AutoSize = true;
            this.chkAutoCompleationCategory.Checked = true;
            this.chkAutoCompleationCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCategory.Location = new System.Drawing.Point(123, 48);
            this.chkAutoCompleationCategory.Name = "chkAutoCompleationCategory";
            this.chkAutoCompleationCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCategory.TabIndex = 31;
            this.chkAutoCompleationCategory.Tag = "1";
            this.chkAutoCompleationCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCategory_CheckedChanged);
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.IsAutoComplete = false;
            this.txtDepartmentCode.ItemCollection = null;
            this.txtDepartmentCode.Location = new System.Drawing.Point(139, 21);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(149, 21);
            this.txtDepartmentCode.TabIndex = 1;
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Leave += new System.EventHandler(this.txtDepartmentCode_Leave);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(544, 137);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(289, 44);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(123, 24);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 14;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDepartment_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(139, 93);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(491, 37);
            this.txtRemark.TabIndex = 6;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 96);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 15;
            this.lblRemark.Text = "Remark";
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Location = new System.Drawing.Point(290, 21);
            this.txtDepartmentName.MasterDescription = "";
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(340, 21);
            this.txtDepartmentName.TabIndex = 2;
            this.txtDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentName_KeyDown);
            this.txtDepartmentName.Leave += new System.EventHandler(this.txtDepartmentName_Leave);
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(139, 69);
            this.txtCategoryName.MasterDescription = "";
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(491, 21);
            this.txtCategoryName.TabIndex = 5;
            this.txtCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryName_KeyDown);
            this.txtCategoryName.Leave += new System.EventHandler(this.txtCategoryName_Leave);
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.IsAutoComplete = false;
            this.txtCategoryCode.ItemCollection = null;
            this.txtCategoryCode.Location = new System.Drawing.Point(139, 45);
            this.txtCategoryCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCategoryCode.MasterCode = "";
            this.txtCategoryCode.MaxLength = 25;
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.Size = new System.Drawing.Size(149, 21);
            this.txtCategoryCode.TabIndex = 3;
            this.txtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryCode_KeyDown);
            this.txtCategoryCode.Leave += new System.EventHandler(this.txtCategoryCode_Leave);
            // 
            // lblCategoryDescription
            // 
            this.lblCategoryDescription.AutoSize = true;
            this.lblCategoryDescription.Location = new System.Drawing.Point(10, 72);
            this.lblCategoryDescription.Name = "lblCategoryDescription";
            this.lblCategoryDescription.Size = new System.Drawing.Size(47, 13);
            this.lblCategoryDescription.TabIndex = 2;
            this.lblCategoryDescription.Text = "Name*";
            // 
            // lblCategoryCode
            // 
            this.lblCategoryCode.AutoSize = true;
            this.lblCategoryCode.Location = new System.Drawing.Point(10, 49);
            this.lblCategoryCode.Name = "lblCategoryCode";
            this.lblCategoryCode.Size = new System.Drawing.Size(44, 13);
            this.lblCategoryCode.TabIndex = 1;
            this.lblCategoryCode.Text = "Code*";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(10, 25);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(82, 13);
            this.lblDepartment.TabIndex = 0;
            this.lblDepartment.Text = "Department*";
            // 
            // FrmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(638, 199);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmCategory";
            this.Text = "Category";
            this.Load += new System.EventHandler(this.frmCategory_Load);
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

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCategoryDescription;
        private System.Windows.Forms.Label lblCategoryCode;
        private System.Windows.Forms.Label lblDepartment;
        private CustomControls.TextBoxMasterCode txtCategoryCode;
        private CustomControls.TextBoxMasterDescription txtCategoryName;
        private CustomControls.TextBoxMasterDescription txtDepartmentName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Button btnNew;
        private CustomControls.TextBoxMasterCode txtDepartmentCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
    }
}
