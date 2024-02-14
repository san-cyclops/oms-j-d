namespace UI.Windows
{
    partial class FrmPettyCashPaymentUpdate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.chkAutoCompleationPettyCash = new System.Windows.Forms.CheckBox();
            this.txtPettyCashBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPettyCashBookCode = new System.Windows.Forms.TextBox();
            this.lblPettyCashBook = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblDocumentDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.dgvPaymentDetail = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AccPettyCashPaymentHeaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetail)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.grpButtonSet2.Location = new System.Drawing.Point(448, 419);
            this.grpButtonSet2.Size = new System.Drawing.Size(240, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 419);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(162, 12);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(84, 11);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 11);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(162, 11);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPettyCash);
            this.grpHeader.Controls.Add(this.txtPettyCashBookName);
            this.grpHeader.Controls.Add(this.txtPettyCashBookCode);
            this.grpHeader.Controls.Add(this.lblPettyCashBook);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblDocumentDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(6, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(682, 87);
            this.grpHeader.TabIndex = 21;
            this.grpHeader.TabStop = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(109, 37);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(563, 21);
            this.cmbLocation.TabIndex = 97;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 40);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(65, 13);
            this.lblLocation.TabIndex = 98;
            this.lblLocation.Text = "Location *";
            // 
            // chkAutoCompleationPettyCash
            // 
            this.chkAutoCompleationPettyCash.AutoSize = true;
            this.chkAutoCompleationPettyCash.Checked = true;
            this.chkAutoCompleationPettyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(92, 63);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 96;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            // 
            // txtPettyCashBookName
            // 
            this.txtPettyCashBookName.Location = new System.Drawing.Point(242, 60);
            this.txtPettyCashBookName.MasterDescription = "";
            this.txtPettyCashBookName.Name = "txtPettyCashBookName";
            this.txtPettyCashBookName.Size = new System.Drawing.Size(430, 21);
            this.txtPettyCashBookName.TabIndex = 94;
            this.txtPettyCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookName_KeyDown);
            this.txtPettyCashBookName.Leave += new System.EventHandler(this.txtPettyCashBookName_Leave);
            // 
            // txtPettyCashBookCode
            // 
            this.txtPettyCashBookCode.Location = new System.Drawing.Point(109, 60);
            this.txtPettyCashBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPettyCashBookCode.Name = "txtPettyCashBookCode";
            this.txtPettyCashBookCode.Size = new System.Drawing.Size(130, 21);
            this.txtPettyCashBookCode.TabIndex = 93;
            this.txtPettyCashBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookCode_KeyDown);
            this.txtPettyCashBookCode.Validated += new System.EventHandler(this.txtPettyCashBookCode_Validated);
            // 
            // lblPettyCashBook
            // 
            this.lblPettyCashBook.AutoSize = true;
            this.lblPettyCashBook.Location = new System.Drawing.Point(6, 63);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(80, 13);
            this.lblPettyCashBook.TabIndex = 95;
            this.lblPettyCashBook.Text = "Petty Cash *";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(566, 11);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(107, 21);
            this.dtpDocumentDate.TabIndex = 5;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(243, 12);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 2;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            this.btnDocumentDetails.Click += new System.EventHandler(this.btnDocumentDetails_Click);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(356, 18);
            this.lblDocumentDate.Name = "lblDocumentDate";
            this.lblDocumentDate.Size = new System.Drawing.Size(96, 13);
            this.lblDocumentDate.TabIndex = 9;
            this.lblDocumentDate.Text = "Document Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(93, 17);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 0;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(110, 13);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(130, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Validated += new System.EventHandler(this.txtDocumentNo_Validated);
            // 
            // lblPurchaseOrderNo
            // 
            this.lblPurchaseOrderNo.AutoSize = true;
            this.lblPurchaseOrderNo.Location = new System.Drawing.Point(7, 17);
            this.lblPurchaseOrderNo.Name = "lblPurchaseOrderNo";
            this.lblPurchaseOrderNo.Size = new System.Drawing.Size(84, 13);
            this.lblPurchaseOrderNo.TabIndex = 1;
            this.lblPurchaseOrderNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.dgvPaymentDetail);
            this.grpBody.Location = new System.Drawing.Point(6, 80);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(682, 278);
            this.grpBody.TabIndex = 103;
            this.grpBody.TabStop = false;
            // 
            // dgvPaymentDetail
            // 
            this.dgvPaymentDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.AccPettyCashPaymentHeaderID,
            this.Date,
            this.VoucherNo,
            this.PayeeName,
            this.Amount});
            this.dgvPaymentDetail.Location = new System.Drawing.Point(9, 20);
            this.dgvPaymentDetail.Name = "dgvPaymentDetail";
            this.dgvPaymentDetail.RowHeadersWidth = 15;
            this.dgvPaymentDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaymentDetail.Size = new System.Drawing.Size(664, 252);
            this.dgvPaymentDetail.TabIndex = 53;
            this.dgvPaymentDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaymentDetail_CellContentClick);
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 48;
            // 
            // AccPettyCashPaymentHeaderID
            // 
            this.AccPettyCashPaymentHeaderID.DataPropertyName = "AccPettyCashPaymentHeaderID";
            this.AccPettyCashPaymentHeaderID.HeaderText = "AccPettyCashPaymentHeaderID";
            this.AccPettyCashPaymentHeaderID.Name = "AccPettyCashPaymentHeaderID";
            this.AccPettyCashPaymentHeaderID.Visible = false;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "DocumentDate";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 105;
            // 
            // VoucherNo
            // 
            this.VoucherNo.DataPropertyName = "DocumentNo";
            this.VoucherNo.HeaderText = "Payment No";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.Width = 120;
            // 
            // PayeeName
            // 
            this.PayeeName.DataPropertyName = "PayeeName";
            this.PayeeName.HeaderText = "Payee Name";
            this.PayeeName.Name = "PayeeName";
            this.PayeeName.Width = 240;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Width = 130;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label2);
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Location = new System.Drawing.Point(6, 355);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(682, 67);
            this.grpFooter.TabIndex = 107;
            this.grpFooter.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(384, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 114;
            this.label2.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalAmount.Location = new System.Drawing.Point(522, 20);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(151, 21);
            this.txtTotalAmount.TabIndex = 113;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPettyCashPaymentUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(690, 467);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmPettyCashPaymentUpdate";
            this.Text = "Petty Cash Payment Update";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentDetail)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationPettyCash;
        private CustomControls.TextBoxMasterDescription txtPettyCashBookName;
        private System.Windows.Forms.TextBox txtPettyCashBookCode;
        private System.Windows.Forms.Label lblPettyCashBook;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblDocumentDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvPaymentDetail;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccPettyCashPaymentHeaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
