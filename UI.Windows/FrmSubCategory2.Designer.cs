namespace UI.Windows
{
    partial class FrmSubCategory2
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
            this.chkAutoCompleationSubCategory2 = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtSubCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategory2Name = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtSubCategory2Code = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblSubCategory2Name = new System.Windows.Forms.Label();
            this.lblSubCategory2Code = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpSubCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 149);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 149);
            // 
            // grpSubCategory
            // 
            this.grpSubCategory.Controls.Add(this.chkAutoClear);
            this.grpSubCategory.Controls.Add(this.btnNew);
            this.grpSubCategory.Controls.Add(this.chkAutoCompleationSubCategory2);
            this.grpSubCategory.Controls.Add(this.chkAutoCompleationCategory);
            this.grpSubCategory.Controls.Add(this.txtRemark);
            this.grpSubCategory.Controls.Add(this.lblRemark);
            this.grpSubCategory.Controls.Add(this.txtSubCategoryName);
            this.grpSubCategory.Controls.Add(this.txtSubCategory2Name);
            this.grpSubCategory.Controls.Add(this.txtSubCategoryCode);
            this.grpSubCategory.Controls.Add(this.txtSubCategory2Code);
            this.grpSubCategory.Controls.Add(this.lblSubCategory2Name);
            this.grpSubCategory.Controls.Add(this.lblSubCategory2Code);
            this.grpSubCategory.Controls.Add(this.lblCategory);
            this.grpSubCategory.Location = new System.Drawing.Point(2, -6);
            this.grpSubCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSubCategory.Name = "grpSubCategory";
            this.grpSubCategory.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSubCategory.Size = new System.Drawing.Size(635, 160);
            this.grpSubCategory.TabIndex = 10;
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
            this.btnNew.Location = new System.Drawing.Point(369, 40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationSubCategory2
            // 
            this.chkAutoCompleationSubCategory2.AutoSize = true;
            this.chkAutoCompleationSubCategory2.Checked = true;
            this.chkAutoCompleationSubCategory2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory2.Location = new System.Drawing.Point(166, 44);
            this.chkAutoCompleationSubCategory2.Name = "chkAutoCompleationSubCategory2";
            this.chkAutoCompleationSubCategory2.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory2.TabIndex = 18;
            this.chkAutoCompleationSubCategory2.Tag = "1";
            this.chkAutoCompleationSubCategory2.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSubCategory2.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSubCategory2_CheckedChanged);
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
            // txtSubCategoryName
            // 
            this.txtSubCategoryName.Location = new System.Drawing.Point(370, 15);
            this.txtSubCategoryName.MasterDescription = "";
            this.txtSubCategoryName.Name = "txtSubCategoryName";
            this.txtSubCategoryName.Size = new System.Drawing.Size(261, 21);
            this.txtSubCategoryName.TabIndex = 3;
            this.txtSubCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryName_KeyDown);
            this.txtSubCategoryName.Leave += new System.EventHandler(this.txtSubCategoryName_Leave);
            // 
            // txtSubCategory2Name
            // 
            this.txtSubCategory2Name.Location = new System.Drawing.Point(187, 66);
            this.txtSubCategory2Name.MasterDescription = "";
            this.txtSubCategory2Name.Name = "txtSubCategory2Name";
            this.txtSubCategory2Name.Size = new System.Drawing.Size(444, 21);
            this.txtSubCategory2Name.TabIndex = 6;
            this.txtSubCategory2Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Name_KeyDown);
            this.txtSubCategory2Name.Leave += new System.EventHandler(this.txtSubCategory2Name_Leave);
            // 
            // txtSubCategoryCode
            // 
            this.txtSubCategoryCode.IsAutoComplete = false;
            this.txtSubCategoryCode.ItemCollection = null;
            this.txtSubCategoryCode.Location = new System.Drawing.Point(187, 15);
            this.txtSubCategoryCode.MasterCode = "";
            this.txtSubCategoryCode.Name = "txtSubCategoryCode";
            this.txtSubCategoryCode.Size = new System.Drawing.Size(178, 21);
            this.txtSubCategoryCode.TabIndex = 2;
            this.txtSubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryCode_KeyDown);
            this.txtSubCategoryCode.Leave += new System.EventHandler(this.txtSubCategoryCode_Leave);
            // 
            // txtSubCategory2Code
            // 
            this.txtSubCategory2Code.IsAutoComplete = false;
            this.txtSubCategory2Code.ItemCollection = null;
            this.txtSubCategory2Code.Location = new System.Drawing.Point(187, 41);
            this.txtSubCategory2Code.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSubCategory2Code.MasterCode = "";
            this.txtSubCategory2Code.MaxLength = 25;
            this.txtSubCategory2Code.Name = "txtSubCategory2Code";
            this.txtSubCategory2Code.Size = new System.Drawing.Size(178, 21);
            this.txtSubCategory2Code.TabIndex = 4;
            this.txtSubCategory2Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Code_KeyDown);
            this.txtSubCategory2Code.Leave += new System.EventHandler(this.txtSubCategory2Code_Leave);
            // 
            // lblSubCategory2Name
            // 
            this.lblSubCategory2Name.AutoSize = true;
            this.lblSubCategory2Name.Location = new System.Drawing.Point(10, 69);
            this.lblSubCategory2Name.Name = "lblSubCategory2Name";
            this.lblSubCategory2Name.Size = new System.Drawing.Size(47, 13);
            this.lblSubCategory2Name.TabIndex = 9;
            this.lblSubCategory2Name.Text = "Name*";
            // 
            // lblSubCategory2Code
            // 
            this.lblSubCategory2Code.AutoSize = true;
            this.lblSubCategory2Code.Location = new System.Drawing.Point(10, 44);
            this.lblSubCategory2Code.Name = "lblSubCategory2Code";
            this.lblSubCategory2Code.Size = new System.Drawing.Size(44, 13);
            this.lblSubCategory2Code.TabIndex = 8;
            this.lblSubCategory2Code.Text = "Code*";
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
            // FrmSubCategory2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(639, 198);
            this.Controls.Add(this.grpSubCategory);
            this.Name = "FrmSubCategory2";
            this.Text = "Sub Category 2";
            this.Load += new System.EventHandler(this.FrmSubCategory2_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
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
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory2;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtSubCategoryName;
        private CustomControls.TextBoxMasterDescription txtSubCategory2Name;
        private CustomControls.TextBoxMasterCode txtSubCategoryCode;
        private CustomControls.TextBoxMasterCode txtSubCategory2Code;
        private System.Windows.Forms.Label lblSubCategory2Name;
        private System.Windows.Forms.Label lblSubCategory2Code;
        private System.Windows.Forms.Label lblCategory;
    }
}
