namespace UI.Windows
{
    partial class FrmProductExtendedProperty
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
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationExtendedProperty = new System.Windows.Forms.CheckBox();
            this.txtPropertyName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPropertyCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblPropertyCode = new System.Windows.Forms.Label();
            this.lblPropertyDescription = new System.Windows.Forms.Label();
            this.LblPropertyType = new System.Windows.Forms.Label();
            this.cmbProertyType = new System.Windows.Forms.ComboBox();
            this.lblParent = new System.Windows.Forms.Label();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.cmbParent = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 132);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(394, 132);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 7;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 6;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 11;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 8;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(312, 18);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationExtendedProperty
            // 
            this.chkAutoCompleationExtendedProperty.AutoSize = true;
            this.chkAutoCompleationExtendedProperty.Checked = true;
            this.chkAutoCompleationExtendedProperty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationExtendedProperty.Location = new System.Drawing.Point(142, 22);
            this.chkAutoCompleationExtendedProperty.Name = "chkAutoCompleationExtendedProperty";
            this.chkAutoCompleationExtendedProperty.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationExtendedProperty.TabIndex = 13;
            this.chkAutoCompleationExtendedProperty.Tag = "1";
            this.chkAutoCompleationExtendedProperty.UseVisualStyleBackColor = true;
            this.chkAutoCompleationExtendedProperty.CheckedChanged += new System.EventHandler(this.chkAutoCompleationExtendedProperty_CheckedChanged);
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(160, 44);
            this.txtPropertyName.MasterDescription = "";
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(464, 21);
            this.txtPropertyName.TabIndex = 2;
            this.txtPropertyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyName_KeyDown);
            this.txtPropertyName.Leave += new System.EventHandler(this.txtPropertyName_Leave);
            // 
            // txtPropertyCode
            // 
            this.txtPropertyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPropertyCode.IsAutoComplete = false;
            this.txtPropertyCode.ItemCollection = null;
            this.txtPropertyCode.Location = new System.Drawing.Point(160, 19);
            this.txtPropertyCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPropertyCode.MasterCode = "";
            this.txtPropertyCode.MaxLength = 25;
            this.txtPropertyCode.Name = "txtPropertyCode";
            this.txtPropertyCode.Size = new System.Drawing.Size(150, 21);
            this.txtPropertyCode.TabIndex = 0;
            this.txtPropertyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyCode_KeyDown);
            this.txtPropertyCode.Leave += new System.EventHandler(this.txtPropertyCode_Leave);
            // 
            // lblPropertyCode
            // 
            this.lblPropertyCode.AutoSize = true;
            this.lblPropertyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropertyCode.Location = new System.Drawing.Point(10, 22);
            this.lblPropertyCode.Name = "lblPropertyCode";
            this.lblPropertyCode.Size = new System.Drawing.Size(97, 13);
            this.lblPropertyCode.TabIndex = 33;
            this.lblPropertyCode.Text = "Property Code*";
            // 
            // lblPropertyDescription
            // 
            this.lblPropertyDescription.AutoSize = true;
            this.lblPropertyDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropertyDescription.Location = new System.Drawing.Point(10, 47);
            this.lblPropertyDescription.Name = "lblPropertyDescription";
            this.lblPropertyDescription.Size = new System.Drawing.Size(100, 13);
            this.lblPropertyDescription.TabIndex = 34;
            this.lblPropertyDescription.Text = "Property Name*";
            // 
            // LblPropertyType
            // 
            this.LblPropertyType.AutoSize = true;
            this.LblPropertyType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPropertyType.Location = new System.Drawing.Point(10, 73);
            this.LblPropertyType.Name = "LblPropertyType";
            this.LblPropertyType.Size = new System.Drawing.Size(90, 13);
            this.LblPropertyType.TabIndex = 34;
            this.LblPropertyType.Text = "PropertyType*";
            // 
            // cmbProertyType
            // 
            this.cmbProertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProertyType.FormattingEnabled = true;
            this.cmbProertyType.Location = new System.Drawing.Point(160, 69);
            this.cmbProertyType.Name = "cmbProertyType";
            this.cmbProertyType.Size = new System.Drawing.Size(150, 21);
            this.cmbProertyType.TabIndex = 3;
            this.cmbProertyType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbProertyType_KeyDown);
            this.cmbProertyType.Leave += new System.EventHandler(this.cmbProertyType_Leave);
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParent.Location = new System.Drawing.Point(10, 98);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(44, 13);
            this.lblParent.TabIndex = 40;
            this.lblParent.Text = "Parent";
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(540, 117);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 12;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // cmbParent
            // 
            this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParent.FormattingEnabled = true;
            this.cmbParent.Location = new System.Drawing.Point(160, 94);
            this.cmbParent.Name = "cmbParent";
            this.cmbParent.Size = new System.Drawing.Size(150, 21);
            this.cmbParent.TabIndex = 4;
            this.cmbParent.Leave += new System.EventHandler(this.cmbParent_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbParent);
            this.groupBox1.Controls.Add(this.lblPropertyDescription);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.LblPropertyType);
            this.groupBox1.Controls.Add(this.lblParent);
            this.groupBox1.Controls.Add(this.lblPropertyCode);
            this.groupBox1.Controls.Add(this.cmbProertyType);
            this.groupBox1.Controls.Add(this.txtPropertyCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.txtPropertyName);
            this.groupBox1.Controls.Add(this.chkAutoCompleationExtendedProperty);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 143);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // FrmProductExtendedProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 181);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmProductExtendedProperty";
            this.Text = "Product Extended Property";
            this.Load += new System.EventHandler(this.FrmProductExtendedProperty_Load);
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

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationExtendedProperty;
        private CustomControls.TextBoxMasterDescription txtPropertyName;
        private CustomControls.TextBoxMasterCode txtPropertyCode;
        private System.Windows.Forms.Label lblPropertyCode;
        private System.Windows.Forms.Label lblPropertyDescription;
        private System.Windows.Forms.Label LblPropertyType;
        private System.Windows.Forms.ComboBox cmbProertyType;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.ComboBox cmbParent;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}