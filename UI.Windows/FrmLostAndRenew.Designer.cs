namespace UI.Windows
{
    partial class FrmLostAndRenew
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
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNicNo = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.txtNewCardNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoCompleationCardNo = new System.Windows.Forms.CheckBox();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewCustomerCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.txtCustomerCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.chkAutoCompleationNic = new System.Windows.Forms.CheckBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRPoints = new System.Windows.Forms.Label();
            this.lblEPoints = new System.Windows.Forms.Label();
            this.lblCPoints = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpIssuedDate = new System.Windows.Forms.DateTimePicker();
            this.lblIssuedOn = new System.Windows.Forms.Label();
            this.txtNameOnCard = new System.Windows.Forms.TextBox();
            this.lblNameOnCard = new System.Windows.Forms.Label();
            this.dtpRenewedDate = new System.Windows.Forms.DateTimePicker();
            this.lblRenewedOn = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(3, 265);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(392, 265);
            this.grpButtonSet2.Size = new System.Drawing.Size(243, 46);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(84, 11);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 11);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(162, 11);
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(111, 16);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 63;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Nic No";
            // 
            // txtNicNo
            // 
            this.txtNicNo.IsAutoComplete = false;
            this.txtNicNo.ItemCollection = null;
            this.txtNicNo.Location = new System.Drawing.Point(129, 35);
            this.txtNicNo.MasterCode = "";
            this.txtNicNo.Name = "txtNicNo";
            this.txtNicNo.Size = new System.Drawing.Size(177, 21);
            this.txtNicNo.TabIndex = 61;
            this.txtNicNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNicNo_KeyDown);
            this.txtNicNo.Validated += new System.EventHandler(this.txtNicNo_Validated);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtNewCardNo);
            this.grpHeader.Controls.Add(this.label5);
            this.grpHeader.Controls.Add(this.chkAutoCompleationCardNo);
            this.grpHeader.Controls.Add(this.txtCardNo);
            this.grpHeader.Controls.Add(this.label4);
            this.grpHeader.Controls.Add(this.label3);
            this.grpHeader.Controls.Add(this.txtNewCustomerCode);
            this.grpHeader.Controls.Add(this.lblCustomerCode);
            this.grpHeader.Controls.Add(this.txtCustomerCode);
            this.grpHeader.Controls.Add(this.chkAutoCompleationCustomer);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(633, 70);
            this.grpHeader.TabIndex = 19;
            this.grpHeader.TabStop = false;
            // 
            // txtNewCardNo
            // 
            this.txtNewCardNo.Location = new System.Drawing.Point(450, 36);
            this.txtNewCardNo.Name = "txtNewCardNo";
            this.txtNewCardNo.Size = new System.Drawing.Size(177, 21);
            this.txtNewCardNo.TabIndex = 78;
            this.txtNewCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewCardNo_KeyDown);
            this.txtNewCardNo.Validated += new System.EventHandler(this.txtNewCardNo_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "New Card No";
            // 
            // chkAutoCompleationCardNo
            // 
            this.chkAutoCompleationCardNo.AutoSize = true;
            this.chkAutoCompleationCardNo.Location = new System.Drawing.Point(111, 39);
            this.chkAutoCompleationCardNo.Name = "chkAutoCompleationCardNo";
            this.chkAutoCompleationCardNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCardNo.TabIndex = 74;
            this.chkAutoCompleationCardNo.Tag = "1";
            this.chkAutoCompleationCardNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCardNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCardNo_CheckedChanged);
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(129, 36);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(177, 21);
            this.txtCardNo.TabIndex = 75;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtCardNo.Validated += new System.EventHandler(this.txtCardNo_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Card No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 72;
            this.label3.Text = "New Customer Code*";
            // 
            // txtNewCustomerCode
            // 
            this.txtNewCustomerCode.IsAutoComplete = false;
            this.txtNewCustomerCode.ItemCollection = null;
            this.txtNewCustomerCode.Location = new System.Drawing.Point(450, 12);
            this.txtNewCustomerCode.MasterCode = "";
            this.txtNewCustomerCode.MaxLength = 7;
            this.txtNewCustomerCode.Name = "txtNewCustomerCode";
            this.txtNewCustomerCode.Size = new System.Drawing.Size(177, 21);
            this.txtNewCustomerCode.TabIndex = 71;
            this.txtNewCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewCustomerCode_KeyDown);
            this.txtNewCustomerCode.Validated += new System.EventHandler(this.txtNewCustomerCode_Validated);
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Location = new System.Drawing.Point(3, 15);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(104, 13);
            this.lblCustomerCode.TabIndex = 69;
            this.lblCustomerCode.Text = "Customer Code*";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.IsAutoComplete = false;
            this.txtCustomerCode.ItemCollection = null;
            this.txtCustomerCode.Location = new System.Drawing.Point(129, 12);
            this.txtCustomerCode.MasterCode = "";
            this.txtCustomerCode.MaxLength = 7;
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(177, 21);
            this.txtCustomerCode.TabIndex = 68;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Validated += new System.EventHandler(this.txtCustomerCode_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(129, 35);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(498, 59);
            this.txtRemark.TabIndex = 77;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(3, 14);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(100, 13);
            this.lblCustomerName.TabIndex = 70;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // chkAutoCompleationNic
            // 
            this.chkAutoCompleationNic.AutoSize = true;
            this.chkAutoCompleationNic.Location = new System.Drawing.Point(111, 38);
            this.chkAutoCompleationNic.Name = "chkAutoCompleationNic";
            this.chkAutoCompleationNic.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationNic.TabIndex = 67;
            this.chkAutoCompleationNic.Tag = "1";
            this.chkAutoCompleationNic.UseVisualStyleBackColor = true;
            this.chkAutoCompleationNic.CheckedChanged += new System.EventHandler(this.chkAutoCompleationNic_CheckedChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(129, 11);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(498, 21);
            this.txtCustomerName.TabIndex = 66;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRPoints);
            this.groupBox1.Controls.Add(this.lblEPoints);
            this.groupBox1.Controls.Add(this.lblCPoints);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbl1);
            this.groupBox1.Controls.Add(this.txtCardType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpIssuedDate);
            this.groupBox1.Controls.Add(this.lblIssuedOn);
            this.groupBox1.Controls.Add(this.txtNameOnCard);
            this.groupBox1.Controls.Add(this.lblNameOnCard);
            this.groupBox1.Controls.Add(this.lblCustomerName);
            this.groupBox1.Controls.Add(this.txtNicNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkAutoCompleationNic);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Location = new System.Drawing.Point(2, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 115);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            // 
            // lblRPoints
            // 
            this.lblRPoints.AutoSize = true;
            this.lblRPoints.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblRPoints.Location = new System.Drawing.Point(536, 93);
            this.lblRPoints.Name = "lblRPoints";
            this.lblRPoints.Size = new System.Drawing.Size(32, 13);
            this.lblRPoints.TabIndex = 105;
            this.lblRPoints.Text = "0.00";
            // 
            // lblEPoints
            // 
            this.lblEPoints.AutoSize = true;
            this.lblEPoints.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblEPoints.Location = new System.Drawing.Point(316, 93);
            this.lblEPoints.Name = "lblEPoints";
            this.lblEPoints.Size = new System.Drawing.Size(32, 13);
            this.lblEPoints.TabIndex = 104;
            this.lblEPoints.Text = "0.00";
            // 
            // lblCPoints
            // 
            this.lblCPoints.AutoSize = true;
            this.lblCPoints.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCPoints.Location = new System.Drawing.Point(127, 93);
            this.lblCPoints.Name = "lblCPoints";
            this.lblCPoints.Size = new System.Drawing.Size(32, 13);
            this.lblCPoints.TabIndex = 103;
            this.lblCPoints.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 101;
            this.label7.Text = "Redeemed Points : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(222, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Earn Points : ";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(3, 93);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(102, 13);
            this.lbl1.TabIndex = 100;
            this.lbl1.Text = "Current Points : ";
            // 
            // txtCardType
            // 
            this.txtCardType.Location = new System.Drawing.Point(417, 59);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.ReadOnly = true;
            this.txtCardType.Size = new System.Drawing.Size(210, 21);
            this.txtCardType.TabIndex = 97;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(312, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 96;
            this.label6.Text = "Card Type ";
            // 
            // dtpIssuedDate
            // 
            this.dtpIssuedDate.Enabled = false;
            this.dtpIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssuedDate.Location = new System.Drawing.Point(129, 59);
            this.dtpIssuedDate.Name = "dtpIssuedDate";
            this.dtpIssuedDate.Size = new System.Drawing.Size(177, 21);
            this.dtpIssuedDate.TabIndex = 74;
            // 
            // lblIssuedOn
            // 
            this.lblIssuedOn.AutoSize = true;
            this.lblIssuedOn.Location = new System.Drawing.Point(3, 65);
            this.lblIssuedOn.Name = "lblIssuedOn";
            this.lblIssuedOn.Size = new System.Drawing.Size(76, 13);
            this.lblIssuedOn.TabIndex = 73;
            this.lblIssuedOn.Text = "Issued Date";
            // 
            // txtNameOnCard
            // 
            this.txtNameOnCard.Location = new System.Drawing.Point(417, 35);
            this.txtNameOnCard.Name = "txtNameOnCard";
            this.txtNameOnCard.ReadOnly = true;
            this.txtNameOnCard.Size = new System.Drawing.Size(210, 21);
            this.txtNameOnCard.TabIndex = 72;
            // 
            // lblNameOnCard
            // 
            this.lblNameOnCard.AutoSize = true;
            this.lblNameOnCard.Location = new System.Drawing.Point(312, 38);
            this.lblNameOnCard.Name = "lblNameOnCard";
            this.lblNameOnCard.Size = new System.Drawing.Size(92, 13);
            this.lblNameOnCard.TabIndex = 71;
            this.lblNameOnCard.Text = "Name On Card";
            // 
            // dtpRenewedDate
            // 
            this.dtpRenewedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRenewedDate.Location = new System.Drawing.Point(130, 11);
            this.dtpRenewedDate.Name = "dtpRenewedDate";
            this.dtpRenewedDate.Size = new System.Drawing.Size(177, 21);
            this.dtpRenewedDate.TabIndex = 99;
            this.dtpRenewedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpRenewedDate_KeyDown);
            // 
            // lblRenewedOn
            // 
            this.lblRenewedOn.AutoSize = true;
            this.lblRenewedOn.Location = new System.Drawing.Point(4, 17);
            this.lblRenewedOn.Name = "lblRenewedOn";
            this.lblRenewedOn.Size = new System.Drawing.Size(90, 13);
            this.lblRenewedOn.TabIndex = 98;
            this.lblRenewedOn.Text = "Renewed Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpRenewedDate);
            this.groupBox2.Controls.Add(this.lblRenewedOn);
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(2, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(633, 100);
            this.groupBox2.TabIndex = 83;
            this.groupBox2.TabStop = false;
            // 
            // FrmLostAndRenew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 313);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmLostAndRenew";
            this.Text = "Lost & Renew";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxMasterCode txtNicNo;
        private System.Windows.Forms.GroupBox grpHeader;
        private CustomControls.TextBoxMasterCode txtCustomerCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationNic;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxMasterCode txtNewCustomerCode;
        private System.Windows.Forms.TextBox txtNewCardNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoCompleationCardNo;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameOnCard;
        private System.Windows.Forms.Label lblNameOnCard;
        private System.Windows.Forms.DateTimePicker dtpIssuedDate;
        private System.Windows.Forms.Label lblIssuedOn;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpRenewedDate;
        private System.Windows.Forms.Label lblRenewedOn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRPoints;
        private System.Windows.Forms.Label lblEPoints;
        private System.Windows.Forms.Label lblCPoints;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl1;
    }
}