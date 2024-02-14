namespace UI.Windows
{
    partial class FrmSubCategory
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
            this.grpSubCategory = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationSubCategory = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSubCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblSubCategoryName = new System.Windows.Forms.Label();
            this.lblSubCategoryCode = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpSubCategory.SuspendLayout();
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
            this.grpButtonSet2.Location = new System.Drawing.Point(397, 150);
            this.grpButtonSet2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpButtonSet2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 10;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 9;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 14;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 11;
            // 
            // grpSubCategory
            // 
            this.grpSubCategory.Controls.Add(this.chkAutoClear);
            this.grpSubCategory.Controls.Add(this.btnNew);
            this.grpSubCategory.Controls.Add(this.chkAutoCompleationSubCategory);
            this.grpSubCategory.Controls.Add(this.chkAutoCompleationCategory);
            this.grpSubCategory.Controls.Add(this.txtRemark);
            this.grpSubCategory.Controls.Add(this.lblRemark);
            this.grpSubCategory.Controls.Add(this.txtCategoryName);
            this.grpSubCategory.Controls.Add(this.txtSubCategoryName);
            this.grpSubCategory.Controls.Add(this.txtCategoryCode);
            this.grpSubCategory.Controls.Add(this.txtSubCategoryCode);
            this.grpSubCategory.Controls.Add(this.lblSubCategoryName);
            this.grpSubCategory.Controls.Add(this.lblSubCategoryCode);
            this.grpSubCategory.Controls.Add(this.lblCategory);
            this.grpSubCategory.Location = new System.Drawing.Point(2, -5);
            this.grpSubCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSubCategory.Name = "grpSubCategory";
            this.grpSubCategory.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSubCategory.Size = new System.Drawing.Size(635, 160);
            this.grpSubCategory.TabIndex = 7;
            this.grpSubCategory.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(544, 139);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(318, 40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationSubCategory
            // 
            this.chkAutoCompleationSubCategory.AutoSize = true;
            this.chkAutoCompleationSubCategory.Checked = true;
            this.chkAutoCompleationSubCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory.Location = new System.Drawing.Point(166, 44);
            this.chkAutoCompleationSubCategory.Name = "chkAutoCompleationSubCategory";
            this.chkAutoCompleationSubCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory.TabIndex = 18;
            this.chkAutoCompleationSubCategory.Tag = "1";
            this.chkAutoCompleationSubCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSubCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSubCategory_CheckedChanged);
            // 
            // chkAutoCompleationCategory
            // 
            this.chkAutoCompleationCategory.AutoSize = true;
            this.chkAutoCompleationCategory.Checked = true;
            this.chkAutoCompleationCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCategory.Location = new System.Drawing.Point(166, 18);
            this.chkAutoCompleationCategory.Name = "chkAutoCompleationCategory";
            this.chkAutoCompleationCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCategory.TabIndex = 17;
            this.chkAutoCompleationCategory.Tag = "1";
            this.chkAutoCompleationCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCategory_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(187, 93);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(444, 37);
            this.txtRemark.TabIndex = 7;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 96);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(318, 15);
            this.txtCategoryName.MasterDescription = "";
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(313, 21);
            this.txtCategoryName.TabIndex = 3;
            this.txtCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryName_KeyDown);
            this.txtCategoryName.Leave += new System.EventHandler(this.txtCategoryName_Leave);
            // 
            // txtSubCategoryName
            // 
            this.txtSubCategoryName.Location = new System.Drawing.Point(187, 66);
            this.txtSubCategoryName.MasterDescription = "";
            this.txtSubCategoryName.Name = "txtSubCategoryName";
            this.txtSubCategoryName.Size = new System.Drawing.Size(444, 21);
            this.txtSubCategoryName.TabIndex = 6;
            this.txtSubCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryName_KeyDown);
            this.txtSubCategoryName.Leave += new System.EventHandler(this.txtSubCategoryName_Leave);
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.IsAutoComplete = false;
            this.txtCategoryCode.ItemCollection = null;
            this.txtCategoryCode.Location = new System.Drawing.Point(187, 15);
            this.txtCategoryCode.MasterCode = "";
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.Size = new System.Drawing.Size(129, 21);
            this.txtCategoryCode.TabIndex = 2;
            this.txtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryCode_KeyDown);
            this.txtCategoryCode.Leave += new System.EventHandler(this.txtCategoryCode_Leave);
            // 
            // txtSubCategoryCode
            // 
            this.txtSubCategoryCode.IsAutoComplete = false;
            this.txtSubCategoryCode.ItemCollection = null;
            this.txtSubCategoryCode.Location = new System.Drawing.Point(187, 41);
            this.txtSubCategoryCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSubCategoryCode.MasterCode = "";
            this.txtSubCategoryCode.MaxLength = 25;
            this.txtSubCategoryCode.Name = "txtSubCategoryCode";
            this.txtSubCategoryCode.Size = new System.Drawing.Size(129, 21);
            this.txtSubCategoryCode.TabIndex = 4;
            this.txtSubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryCode_KeyDown);
            this.txtSubCategoryCode.Leave += new System.EventHandler(this.txtSubCategoryCode_Leave);
            // 
            // lblSubCategoryName
            // 
            this.lblSubCategoryName.AutoSize = true;
            this.lblSubCategoryName.Location = new System.Drawing.Point(10, 69);
            this.lblSubCategoryName.Name = "lblSubCategoryName";
            this.lblSubCategoryName.Size = new System.Drawing.Size(47, 13);
            this.lblSubCategoryName.TabIndex = 9;
            this.lblSubCategoryName.Text = "Name*";
            // 
            // lblSubCategoryCode
            // 
            this.lblSubCategoryCode.AutoSize = true;
            this.lblSubCategoryCode.Location = new System.Drawing.Point(10, 44);
            this.lblSubCategoryCode.Name = "lblSubCategoryCode";
            this.lblSubCategoryCode.Size = new System.Drawing.Size(44, 13);
            this.lblSubCategoryCode.TabIndex = 8;
            this.lblSubCategoryCode.Text = "Code*";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(10, 18);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(67, 13);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "Category*";
            // 
            // FrmSubCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(639, 198);
            this.Controls.Add(this.grpSubCategory);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmSubCategory";
            this.Text = "Sub Category";
            this.Load += new System.EventHandler(this.FrmSubCategory_Load);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpSubCategory, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpSubCategory.ResumeLayout(false);
            this.grpSubCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSubCategory;
        private System.Windows.Forms.Label lblSubCategoryName;
        private System.Windows.Forms.Label lblSubCategoryCode;
        private System.Windows.Forms.Label lblCategory;
        private CustomControls.TextBoxMasterCode txtSubCategoryCode;
        private CustomControls.TextBoxMasterCode txtCategoryCode;
        private CustomControls.TextBoxMasterDescription txtCategoryName;
        private CustomControls.TextBoxMasterDescription txtSubCategoryName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
