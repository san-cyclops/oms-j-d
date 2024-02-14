namespace UI.Windows
{
    partial class FrmManualPointsAdd
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblLoyltyTyp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.cmbCustomerType = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationCardNo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPoints = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.TxtReceptNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpAddedDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnitNo = new UI.Windows.CustomControls.TextBoxInteger();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.txtEmployeeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblEmployeeCode = new System.Windows.Forms.Label();
            this.txtEmployeeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 189);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(436, 189);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblLoyltyTyp);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblCardType);
            this.groupBox2.Controls.Add(this.cmbCustomerType);
            this.groupBox2.Controls.Add(this.chkAutoCompleationCardNo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtCardNo);
            this.groupBox2.Controls.Add(this.lblCardNo);
            this.groupBox2.Controls.Add(this.chkAutoCompleationCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerName);
            this.groupBox2.Controls.Add(this.txtCustomerCode);
            this.groupBox2.Controls.Add(this.lblCustomerName);
            this.groupBox2.Controls.Add(this.lblCustomerCode);
            this.groupBox2.Location = new System.Drawing.Point(2, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 90);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // lblLoyltyTyp
            // 
            this.lblLoyltyTyp.AutoSize = true;
            this.lblLoyltyTyp.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLoyltyTyp.Location = new System.Drawing.Point(120, 64);
            this.lblLoyltyTyp.Name = "lblLoyltyTyp";
            this.lblLoyltyTyp.Size = new System.Drawing.Size(83, 13);
            this.lblLoyltyTyp.TabIndex = 39;
            this.lblLoyltyTyp.Text = "Loyalty Type ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Loyalty Type : ";
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCardType.Location = new System.Drawing.Point(360, 64);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(70, 13);
            this.lblCardType.TabIndex = 35;
            this.lblCardType.Text = "Card Type ";
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Location = new System.Drawing.Point(122, 38);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new System.Drawing.Size(137, 21);
            this.cmbCustomerType.TabIndex = 51;
            this.cmbCustomerType.SelectedValueChanged += new System.EventHandler(this.cmbCustomerType_SelectedValueChanged);
            // 
            // chkAutoCompleationCardNo
            // 
            this.chkAutoCompleationCardNo.AutoSize = true;
            this.chkAutoCompleationCardNo.Checked = true;
            this.chkAutoCompleationCardNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCardNo.Location = new System.Drawing.Point(338, 41);
            this.chkAutoCompleationCardNo.Name = "chkAutoCompleationCardNo";
            this.chkAutoCompleationCardNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCardNo.TabIndex = 33;
            this.chkAutoCompleationCardNo.Tag = "1";
            this.chkAutoCompleationCardNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCardNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCardNo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Card Type : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Customer Type";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(363, 38);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(306, 21);
            this.txtCardNo.TabIndex = 53;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtCardNo.Leave += new System.EventHandler(this.txtCardNo_Leave);
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Location = new System.Drawing.Point(260, 41);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(54, 13);
            this.lblCardNo.TabIndex = 31;
            this.lblCardNo.Text = "Card No";
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(103, 17);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 29;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(363, 14);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(306, 21);
            this.txtCustomerName.TabIndex = 50;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(122, 14);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(137, 21);
            this.txtCustomerCode.TabIndex = 1;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(260, 18);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(100, 13);
            this.lblCustomerName.TabIndex = 4;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Location = new System.Drawing.Point(6, 17);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(97, 13);
            this.lblCustomerCode.TabIndex = 0;
            this.lblCustomerCode.Text = "Customer Code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPoints);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dtpBillDate);
            this.groupBox1.Controls.Add(this.lblBillDate);
            this.groupBox1.Controls.Add(this.TxtReceptNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpAddedDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtUnitNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkAutoCompleationEmployee);
            this.groupBox1.Controls.Add(this.txtEmployeeCode);
            this.groupBox1.Controls.Add(this.lblEmployeeCode);
            this.groupBox1.Controls.Add(this.txtEmployeeName);
            this.groupBox1.Location = new System.Drawing.Point(2, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 115);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtPoints
            // 
            this.txtPoints.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPoints.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPoints.Location = new System.Drawing.Point(363, 88);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(141, 21);
            this.txtPoints.TabIndex = 10;
            this.txtPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(263, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 77;
            this.label9.Text = "Points";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Location = new System.Drawing.Point(122, 89);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(137, 21);
            this.txtAmount.TabIndex = 9;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "Amount";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBillDate.Location = new System.Drawing.Point(122, 64);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(137, 21);
            this.dtpBillDate.TabIndex = 7;
            this.dtpBillDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpBillDate_KeyDown);
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Location = new System.Drawing.Point(7, 67);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(55, 13);
            this.lblBillDate.TabIndex = 65;
            this.lblBillDate.Text = "Bill Date";
            // 
            // TxtReceptNo
            // 
            this.TxtReceptNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxtReceptNo.Location = new System.Drawing.Point(544, 40);
            this.TxtReceptNo.Name = "TxtReceptNo";
            this.TxtReceptNo.Size = new System.Drawing.Size(126, 21);
            this.TxtReceptNo.TabIndex = 6;
            this.TxtReceptNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtReceptNo_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(478, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Recept No";
            // 
            // dtpAddedDate
            // 
            this.dtpAddedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAddedDate.Location = new System.Drawing.Point(363, 64);
            this.dtpAddedDate.Name = "dtpAddedDate";
            this.dtpAddedDate.Size = new System.Drawing.Size(141, 21);
            this.dtpAddedDate.TabIndex = 8;
            this.dtpAddedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpAddedDate_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(263, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "Added Date";
            // 
            // txtUnitNo
            // 
            this.txtUnitNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtUnitNo.IntValue = 0;
            this.txtUnitNo.Location = new System.Drawing.Point(425, 40);
            this.txtUnitNo.Name = "txtUnitNo";
            this.txtUnitNo.Size = new System.Drawing.Size(41, 21);
            this.txtUnitNo.TabIndex = 5;
            this.txtUnitNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitNo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Unit No";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(122, 39);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(242, 21);
            this.cmbLocation.TabIndex = 4;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 43);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(262, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Employee Name";
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(103, 17);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 33;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeCode.IsAutoComplete = false;
            this.txtEmployeeCode.ItemCollection = null;
            this.txtEmployeeCode.Location = new System.Drawing.Point(122, 14);
            this.txtEmployeeCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmployeeCode.MasterCode = "";
            this.txtEmployeeCode.MaxLength = 25;
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(137, 21);
            this.txtEmployeeCode.TabIndex = 2;
            this.txtEmployeeCode.Tag = "1";
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Leave += new System.EventHandler(this.txtEmployeeCode_Leave);
            // 
            // lblEmployeeCode
            // 
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeCode.Location = new System.Drawing.Point(7, 17);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new System.Drawing.Size(97, 13);
            this.lblEmployeeCode.TabIndex = 30;
            this.lblEmployeeCode.Text = "Employee Code";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(363, 14);
            this.txtEmployeeName.MasterDescription = "";
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(307, 21);
            this.txtEmployeeName.TabIndex = 3;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // FrmManualPointsAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 238);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmManualPointsAdd";
            this.Text = "Manual Points Add";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAutoCompleationCardNo;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLoyltyTyp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCustomerType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private CustomControls.TextBoxMasterCode txtEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeCode;
        private CustomControls.TextBoxMasterDescription txtEmployeeName;
        private CustomControls.TextBoxCurrency txtPoints;
        private System.Windows.Forms.Label label9;
        private CustomControls.TextBoxCurrency txtAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.TextBox TxtReceptNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpAddedDate;
        private System.Windows.Forms.Label label6;
        private CustomControls.TextBoxInteger txtUnitNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;

    }
}