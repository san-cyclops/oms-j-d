namespace UI.Windows
{
    partial class FrmTax
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
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.textBoxCurrency1 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoCompleationCollectedAccount = new System.Windows.Forms.CheckBox();
            this.txtCollectedAccountName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCollectedAccountCode = new System.Windows.Forms.TextBox();
            this.lblPettyCashBook = new System.Windows.Forms.Label();
            this.chkAutoCompleationPaidAccount = new System.Windows.Forms.CheckBox();
            this.txtPaidAccountName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPaidAccountCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEffectiveFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblEffectiveFromDate = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffectiveRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 329);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(413, 329);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox6);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.dtpEffectiveFromDate);
            this.groupBox1.Controls.Add(this.lblEffectiveFromDate);
            this.groupBox1.Controls.Add(this.chkAutoCompleationPaidAccount);
            this.groupBox1.Controls.Add(this.txtPaidAccountName);
            this.groupBox1.Controls.Add(this.txtPaidAccountCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkAutoCompleationCollectedAccount);
            this.groupBox1.Controls.Add(this.txtCollectedAccountName);
            this.groupBox1.Controls.Add(this.txtCollectedAccountCode);
            this.groupBox1.Controls.Add(this.lblPettyCashBook);
            this.groupBox1.Controls.Add(this.textBoxCurrency1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.groupBox1.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.groupBox1.Controls.Add(this.lblSubTotalDiscount);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.txtDepartmentCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationDepartment);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtDepartmentName);
            this.groupBox1.Controls.Add(this.lblDepartmentCode);
            this.groupBox1.Controls.Add(this.lblDepartmentName);
            this.groupBox1.Location = new System.Drawing.Point(2, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 338);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoClear.Location = new System.Drawing.Point(544, 317);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 24;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.IsAutoComplete = false;
            this.txtDepartmentCode.ItemCollection = null;
            this.txtDepartmentCode.Location = new System.Drawing.Point(138, 13);
            this.txtDepartmentCode.MasterCode = "";
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(57, 21);
            this.txtDepartmentCode.TabIndex = 16;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(195, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 17;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(121, 16);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 23;
            this.chkAutoCompleationDepartment.Tag = "1";
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(138, 112);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(458, 37);
            this.txtRemark.TabIndex = 19;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 123);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 22;
            this.lblRemark.Text = "Remark";
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Location = new System.Drawing.Point(138, 37);
            this.txtDepartmentName.MasterDescription = "";
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(458, 21);
            this.txtDepartmentName.TabIndex = 18;
            // 
            // lblDepartmentCode
            // 
            this.lblDepartmentCode.AutoSize = true;
            this.lblDepartmentCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentCode.Location = new System.Drawing.Point(10, 19);
            this.lblDepartmentCode.Name = "lblDepartmentCode";
            this.lblDepartmentCode.Size = new System.Drawing.Size(44, 13);
            this.lblDepartmentCode.TabIndex = 20;
            this.lblDepartmentCode.Text = "Code*";
            // 
            // lblDepartmentName
            // 
            this.lblDepartmentName.AutoSize = true;
            this.lblDepartmentName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentName.Location = new System.Drawing.Point(10, 45);
            this.lblDepartmentName.Name = "lblDepartmentName";
            this.lblDepartmentName.Size = new System.Drawing.Size(47, 13);
            this.lblDepartmentName.TabIndex = 21;
            this.lblDepartmentName.Text = "Name*";
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(138, 61);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(148, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 92;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(117, 66);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 91;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(10, 69);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(33, 13);
            this.lblSubTotalDiscount.TabIndex = 90;
            this.lblSubTotalDiscount.Text = "Rate";
            // 
            // textBoxCurrency1
            // 
            this.textBoxCurrency1.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency1.Location = new System.Drawing.Point(448, 61);
            this.textBoxCurrency1.Name = "textBoxCurrency1";
            this.textBoxCurrency1.Size = new System.Drawing.Size(148, 21);
            this.textBoxCurrency1.TabIndex = 95;
            this.textBoxCurrency1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "Effective Rate";
            // 
            // chkAutoCompleationCollectedAccount
            // 
            this.chkAutoCompleationCollectedAccount.AutoSize = true;
            this.chkAutoCompleationCollectedAccount.Checked = true;
            this.chkAutoCompleationCollectedAccount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCollectedAccount.Location = new System.Drawing.Point(121, 274);
            this.chkAutoCompleationCollectedAccount.Name = "chkAutoCompleationCollectedAccount";
            this.chkAutoCompleationCollectedAccount.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCollectedAccount.TabIndex = 117;
            this.chkAutoCompleationCollectedAccount.Tag = "1";
            this.chkAutoCompleationCollectedAccount.UseVisualStyleBackColor = true;
            // 
            // txtCollectedAccountName
            // 
            this.txtCollectedAccountName.Location = new System.Drawing.Point(275, 271);
            this.txtCollectedAccountName.MasterDescription = "";
            this.txtCollectedAccountName.Name = "txtCollectedAccountName";
            this.txtCollectedAccountName.Size = new System.Drawing.Size(337, 21);
            this.txtCollectedAccountName.TabIndex = 115;
            // 
            // txtCollectedAccountCode
            // 
            this.txtCollectedAccountCode.Location = new System.Drawing.Point(138, 271);
            this.txtCollectedAccountCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCollectedAccountCode.Name = "txtCollectedAccountCode";
            this.txtCollectedAccountCode.Size = new System.Drawing.Size(136, 21);
            this.txtCollectedAccountCode.TabIndex = 114;
            // 
            // lblPettyCashBook
            // 
            this.lblPettyCashBook.AutoSize = true;
            this.lblPettyCashBook.Location = new System.Drawing.Point(10, 275);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(103, 13);
            this.lblPettyCashBook.TabIndex = 116;
            this.lblPettyCashBook.Text = "Collected Ledger";
            // 
            // chkAutoCompleationPaidAccount
            // 
            this.chkAutoCompleationPaidAccount.AutoSize = true;
            this.chkAutoCompleationPaidAccount.Checked = true;
            this.chkAutoCompleationPaidAccount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPaidAccount.Location = new System.Drawing.Point(121, 297);
            this.chkAutoCompleationPaidAccount.Name = "chkAutoCompleationPaidAccount";
            this.chkAutoCompleationPaidAccount.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPaidAccount.TabIndex = 121;
            this.chkAutoCompleationPaidAccount.Tag = "1";
            this.chkAutoCompleationPaidAccount.UseVisualStyleBackColor = true;
            // 
            // txtPaidAccountName
            // 
            this.txtPaidAccountName.Location = new System.Drawing.Point(275, 294);
            this.txtPaidAccountName.MasterDescription = "";
            this.txtPaidAccountName.Name = "txtPaidAccountName";
            this.txtPaidAccountName.Size = new System.Drawing.Size(337, 21);
            this.txtPaidAccountName.TabIndex = 119;
            // 
            // txtPaidAccountCode
            // 
            this.txtPaidAccountCode.Location = new System.Drawing.Point(138, 294);
            this.txtPaidAccountCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPaidAccountCode.Name = "txtPaidAccountCode";
            this.txtPaidAccountCode.Size = new System.Drawing.Size(136, 21);
            this.txtPaidAccountCode.TabIndex = 118;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Paid Ledger";
            // 
            // dtpEffectiveFromDate
            // 
            this.dtpEffectiveFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectiveFromDate.Location = new System.Drawing.Point(138, 88);
            this.dtpEffectiveFromDate.Name = "dtpEffectiveFromDate";
            this.dtpEffectiveFromDate.Size = new System.Drawing.Size(148, 21);
            this.dtpEffectiveFromDate.TabIndex = 123;
            // 
            // lblEffectiveFromDate
            // 
            this.lblEffectiveFromDate.AutoSize = true;
            this.lblEffectiveFromDate.Location = new System.Drawing.Point(10, 94);
            this.lblEffectiveFromDate.Name = "lblEffectiveFromDate";
            this.lblEffectiveFromDate.Size = new System.Drawing.Size(89, 13);
            this.lblEffectiveFromDate.TabIndex = 122;
            this.lblEffectiveFromDate.Text = "Effective From";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Order,
            this.TaxCode,
            this.Rate,
            this.EffectiveRate});
            this.dataGridView1.Location = new System.Drawing.Point(13, 181);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(598, 77);
            this.dataGridView1.TabIndex = 124;
            // 
            // Order
            // 
            this.Order.HeaderText = "Order";
            this.Order.Name = "Order";
            // 
            // TaxCode
            // 
            this.TaxCode.HeaderText = "TaxCode";
            this.TaxCode.Name = "TaxCode";
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            // 
            // EffectiveRate
            // 
            this.EffectiveRate.HeaderText = "EffectiveRate";
            this.EffectiveRate.Name = "EffectiveRate";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(448, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 21);
            this.comboBox1.TabIndex = 125;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(390, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Type";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(13, 158);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(88, 17);
            this.checkBox2.TabIndex = 127;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(107, 158);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(88, 17);
            this.checkBox3.TabIndex = 128;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(201, 158);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(88, 17);
            this.checkBox4.TabIndex = 129;
            this.checkBox4.Text = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(295, 158);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(88, 17);
            this.checkBox5.TabIndex = 130;
            this.checkBox5.Text = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(389, 158);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(88, 17);
            this.checkBox6.TabIndex = 131;
            this.checkBox6.Text = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // FrmTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(654, 378);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTax";
            this.Text = "Tax";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private CustomControls.TextBoxCurrency textBoxCurrency1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.CheckBox chkAutoCompleationPaidAccount;
        private CustomControls.TextBoxMasterDescription txtPaidAccountName;
        private System.Windows.Forms.TextBox txtPaidAccountCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoCompleationCollectedAccount;
        private CustomControls.TextBoxMasterDescription txtCollectedAccountName;
        private System.Windows.Forms.TextBox txtCollectedAccountCode;
        private System.Windows.Forms.Label lblPettyCashBook;
        private System.Windows.Forms.DateTimePicker dtpEffectiveFromDate;
        private System.Windows.Forms.Label lblEffectiveFromDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffectiveRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
