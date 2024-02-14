namespace UI.Windows
{
    partial class FrmCardMaster
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
            this.lblNationality = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtPointValue = new UI.Windows.CustomControls.TextBoxNumeric();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtMinimumPoints = new UI.Windows.CustomControls.TextBoxInteger();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblDescription = new System.Windows.Forms.Label();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationCard = new System.Windows.Forms.CheckBox();
            this.txtName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 169);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(355, 169);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNationality);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.txtPointValue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.txtMinimumPoints);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationCard);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblCode);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(592, 180);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Location = new System.Drawing.Point(10, 21);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(34, 13);
            this.lblNationality.TabIndex = 48;
            this.lblNationality.Text = "Type";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(119, 18);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(193, 21);
            this.cmbType.TabIndex = 47;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbType_KeyDown);
            // 
            // txtPointValue
            // 
            this.txtPointValue.Location = new System.Drawing.Point(502, 118);
            this.txtPointValue.Name = "txtPointValue";
            this.txtPointValue.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPointValue.Size = new System.Drawing.Size(82, 21);
            this.txtPointValue.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(426, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Point Value";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(314, 118);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDiscount.Size = new System.Drawing.Size(73, 21);
            this.txtDiscount.TabIndex = 23;
            this.txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscount_KeyDown);
            // 
            // txtMinimumPoints
            // 
            this.txtMinimumPoints.IntValue = 0;
            this.txtMinimumPoints.Location = new System.Drawing.Point(119, 118);
            this.txtMinimumPoints.Name = "txtMinimumPoints";
            this.txtMinimumPoints.Size = new System.Drawing.Size(80, 21);
            this.txtMinimumPoints.TabIndex = 22;
            this.txtMinimumPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMinimumPoints_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Discount %";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Minimum Points";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(119, 93);
            this.txtDescription.MasterDescription = "";
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(465, 21);
            this.txtDescription.TabIndex = 16;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(10, 96);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 13);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "Description";
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(501, 155);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.IsAutoComplete = false;
            this.txtCode.ItemCollection = null;
            this.txtCode.Location = new System.Drawing.Point(119, 43);
            this.txtCode.MasterCode = "";
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(193, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(314, 42);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationCard
            // 
            this.chkAutoCompleationCard.AutoSize = true;
            this.chkAutoCompleationCard.Checked = true;
            this.chkAutoCompleationCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCard.Location = new System.Drawing.Point(96, 48);
            this.chkAutoCompleationCard.Name = "chkAutoCompleationCard";
            this.chkAutoCompleationCard.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCard.TabIndex = 14;
            this.chkAutoCompleationCard.Tag = "1";
            this.chkAutoCompleationCard.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCard.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCard_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 68);
            this.txtName.MasterDescription = "";
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(465, 21);
            this.txtName.TabIndex = 2;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(10, 47);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(44, 13);
            this.lblCode.TabIndex = 9;
            this.lblCode.Text = "Code*";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(10, 71);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name*";
            // 
            // FrmCardMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(596, 218);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCardMaster";
            this.Text = "Card Type Master";
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
        private CustomControls.TextBoxMasterDescription txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxMasterCode txtCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationCard;
        private CustomControls.TextBoxMasterDescription txtName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxInteger txtMinimumPoints;
        private CustomControls.TextBoxNumeric txtPointValue;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxNumeric txtDiscount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.ComboBox cmbType;
    }
}
