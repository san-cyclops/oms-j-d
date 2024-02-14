namespace UI.Windows
{
    partial class FrmReceipt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.lblReceivedFrom = new System.Windows.Forms.Label();
            this.txtReceivedFrom = new System.Windows.Forms.TextBox();
            this.rdoAdvance = new System.Windows.Forms.RadioButton();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblReceiptDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCardChequeNo = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBoxCurrency5 = new UI.Windows.CustomControls.TextBoxCurrency();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBoxMasterDescription1 = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.PaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetCreditDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(647, 376);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 376);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.lblReceivedFrom);
            this.grpHeader.Controls.Add(this.txtReceivedFrom);
            this.grpHeader.Controls.Add(this.rdoAdvance);
            this.grpHeader.Controls.Add(this.lblCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationCustomer);
            this.grpHeader.Controls.Add(this.txtCustomerCode);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
            this.grpHeader.Controls.Add(this.chkTStatus);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpReceiptDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblReceiptDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(961, 110);
            this.grpHeader.TabIndex = 24;
            this.grpHeader.TabStop = false;
            // 
            // lblReceivedFrom
            // 
            this.lblReceivedFrom.AutoSize = true;
            this.lblReceivedFrom.Location = new System.Drawing.Point(6, 61);
            this.lblReceivedFrom.Name = "lblReceivedFrom";
            this.lblReceivedFrom.Size = new System.Drawing.Size(92, 13);
            this.lblReceivedFrom.TabIndex = 109;
            this.lblReceivedFrom.Text = "Received From";
            // 
            // txtReceivedFrom
            // 
            this.txtReceivedFrom.Location = new System.Drawing.Point(107, 58);
            this.txtReceivedFrom.Name = "txtReceivedFrom";
            this.txtReceivedFrom.Size = new System.Drawing.Size(474, 21);
            this.txtReceivedFrom.TabIndex = 108;
            // 
            // rdoAdvance
            // 
            this.rdoAdvance.AutoSize = true;
            this.rdoAdvance.Location = new System.Drawing.Point(744, 90);
            this.rdoAdvance.Name = "rdoAdvance";
            this.rdoAdvance.Size = new System.Drawing.Size(74, 17);
            this.rdoAdvance.TabIndex = 107;
            this.rdoAdvance.TabStop = true;
            this.rdoAdvance.Text = "Advance";
            this.rdoAdvance.UseVisualStyleBackColor = true;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(6, 38);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(63, 13);
            this.lblCustomer.TabIndex = 106;
            this.lblCustomer.Text = "Customer";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(244, 35);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(337, 21);
            this.txtCustomerName.TabIndex = 105;
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(90, 38);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 104;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(107, 35);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(136, 21);
            this.txtCustomerCode.TabIndex = 103;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(743, 62);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(207, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(648, 65);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 101;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTStatus.Location = new System.Drawing.Point(937, 39);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(15, 14);
            this.chkTStatus.TabIndex = 87;
            this.chkTStatus.UseVisualStyleBackColor = true;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(374, 13);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(207, 21);
            this.cmbLocation.TabIndex = 63;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(292, 15);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(743, 38);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(197, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(648, 41);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceiptDate.Location = new System.Drawing.Point(744, 15);
            this.dtpReceiptDate.Name = "dtpReceiptDate";
            this.dtpReceiptDate.Size = new System.Drawing.Size(207, 21);
            this.dtpReceiptDate.TabIndex = 22;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(244, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 85);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 82);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(474, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSize = true;
            this.lblReceiptDate.Location = new System.Drawing.Point(648, 17);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(80, 13);
            this.lblReceiptDate.TabIndex = 9;
            this.lblReceiptDate.Text = "Receipt Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCardChequeNo);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.textBoxCurrency5);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.textBoxMasterDescription1);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.cmbPaymentMethod);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(2, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 256);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // txtCardChequeNo
            // 
            this.txtCardChequeNo.Location = new System.Drawing.Point(592, 227);
            this.txtCardChequeNo.Name = "txtCardChequeNo";
            this.txtCardChequeNo.Size = new System.Drawing.Size(80, 21);
            this.txtCardChequeNo.TabIndex = 121;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(726, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Set Credits";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(674, 227);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(52, 23);
            this.button6.TabIndex = 120;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBoxCurrency5
            // 
            this.textBoxCurrency5.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textBoxCurrency5.Location = new System.Drawing.Point(809, 227);
            this.textBoxCurrency5.Name = "textBoxCurrency5";
            this.textBoxCurrency5.Size = new System.Drawing.Size(144, 21);
            this.textBoxCurrency5.TabIndex = 118;
            this.textBoxCurrency5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(148, 230);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 117;
            this.checkBox1.Tag = "1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBoxMasterDescription1
            // 
            this.textBoxMasterDescription1.Location = new System.Drawing.Point(279, 227);
            this.textBoxMasterDescription1.MasterDescription = "";
            this.textBoxMasterDescription1.Name = "textBoxMasterDescription1";
            this.textBoxMasterDescription1.Size = new System.Drawing.Size(218, 21);
            this.textBoxMasterDescription1.TabIndex = 115;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(164, 227);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(114, 21);
            this.textBox2.TabIndex = 114;
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(8, 227);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(139, 21);
            this.cmbPaymentMethod.TabIndex = 46;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(499, 227);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 21);
            this.dateTimePicker1.TabIndex = 24;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentMethod,
            this.AccountCode,
            this.AccountName,
            this.Date,
            this.CardNo,
            this.SetCreditDocument,
            this.Amount});
            this.dataGridView2.Location = new System.Drawing.Point(6, 11);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(950, 210);
            this.dataGridView2.TabIndex = 0;
            // 
            // PaymentMethod
            // 
            this.PaymentMethod.HeaderText = "Payment Method";
            this.PaymentMethod.Name = "PaymentMethod";
            // 
            // AccountCode
            // 
            this.AccountCode.DataPropertyName = "AccountCode";
            this.AccountCode.HeaderText = "Account Code";
            this.AccountCode.Name = "AccountCode";
            this.AccountCode.Width = 110;
            // 
            // AccountName
            // 
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.Width = 220;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // CardNo
            // 
            this.CardNo.HeaderText = "Card/Cheque No";
            this.CardNo.Name = "CardNo";
            // 
            // SetCreditDocument
            // 
            this.SetCreditDocument.HeaderText = "Set Credit Document";
            this.SetCreditDocument.Name = "SetCreditDocument";
            // 
            // Amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 175;
            // 
            // FrmReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(966, 424);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmReceipt";
            this.Text = "Receipt";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblReceiptDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.RadioButton rdoAdvance;
        private System.Windows.Forms.Label lblReceivedFrom;
        private System.Windows.Forms.TextBox txtReceivedFrom;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCardChequeNo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private CustomControls.TextBoxCurrency textBoxCurrency5;
        private System.Windows.Forms.CheckBox checkBox1;
        private CustomControls.TextBoxMasterDescription textBoxMasterDescription1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetCreditDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
