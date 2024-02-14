namespace UI.Windows
{
    partial class FrmBillEntry
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
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSupplier = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.dtpReceivedDate = new System.Windows.Forms.DateTimePicker();
            this.lblReceivedDate = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.txtLedgerValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtLedgerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationOtherExpence = new System.Windows.Forms.CheckBox();
            this.txtLedgerCode = new System.Windows.Forms.TextBox();
            this.AccLedgerAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostCentre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtManualNo = new System.Windows.Forms.TextBox();
            this.lblManualNo = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(528, 345);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 345);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtManualNo);
            this.grpHeader.Controls.Add(this.lblManualNo);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpReceivedDate);
            this.grpHeader.Controls.Add(this.lblReceivedDate);
            this.grpHeader.Controls.Add(this.dtpDueDate);
            this.grpHeader.Controls.Add(this.lblDueDate);
            this.grpHeader.Controls.Add(this.dtpEntryDate);
            this.grpHeader.Controls.Add(this.lblEntryDate);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.dtpBillDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblBillDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(843, 111);
            this.grpHeader.TabIndex = 22;
            this.grpHeader.TabStop = false;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(6, 38);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 112;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(265, 37);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(352, 21);
            this.txtSupplierName.TabIndex = 111;
            // 
            // chkAutoCompleationSupplier
            // 
            this.chkAutoCompleationSupplier.AutoSize = true;
            this.chkAutoCompleationSupplier.Checked = true;
            this.chkAutoCompleationSupplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSupplier.Location = new System.Drawing.Point(96, 39);
            this.chkAutoCompleationSupplier.Name = "chkAutoCompleationSupplier";
            this.chkAutoCompleationSupplier.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSupplier.TabIndex = 110;
            this.chkAutoCompleationSupplier.Tag = "1";
            this.chkAutoCompleationSupplier.UseVisualStyleBackColor = true;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(114, 37);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(148, 21);
            this.txtSupplierCode.TabIndex = 109;
            // 
            // dtpReceivedDate
            // 
            this.dtpReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceivedDate.Location = new System.Drawing.Point(722, 85);
            this.dtpReceivedDate.Name = "dtpReceivedDate";
            this.dtpReceivedDate.Size = new System.Drawing.Size(116, 21);
            this.dtpReceivedDate.TabIndex = 108;
            // 
            // lblReceivedDate
            // 
            this.lblReceivedDate.AutoSize = true;
            this.lblReceivedDate.Location = new System.Drawing.Point(627, 88);
            this.lblReceivedDate.Name = "lblReceivedDate";
            this.lblReceivedDate.Size = new System.Drawing.Size(90, 13);
            this.lblReceivedDate.TabIndex = 107;
            this.lblReceivedDate.Text = "Received Date";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Location = new System.Drawing.Point(722, 61);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(116, 21);
            this.dtpDueDate.TabIndex = 106;
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(627, 63);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(61, 13);
            this.lblDueDate.TabIndex = 105;
            this.lblDueDate.Text = "Due Date";
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEntryDate.Location = new System.Drawing.Point(722, 13);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(116, 21);
            this.dtpEntryDate.TabIndex = 104;
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Location = new System.Drawing.Point(627, 16);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(68, 13);
            this.lblEntryDate.TabIndex = 103;
            this.lblEntryDate.Text = "Entry Date";
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(447, 223);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(229, 21);
            this.cmbCostCentre.TabIndex = 102;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(386, 12);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(231, 21);
            this.cmbLocation.TabIndex = 63;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(297, 15);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(386, 61);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(231, 21);
            this.txtReferenceNo.TabIndex = 24;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(297, 63);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBillDate.Location = new System.Drawing.Point(722, 37);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(116, 21);
            this.dtpBillDate.TabIndex = 22;
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(264, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 87);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(114, 85);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(503, 21);
            this.txtRemark.TabIndex = 18;
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Location = new System.Drawing.Point(627, 40);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(55, 13);
            this.lblBillDate.TabIndex = 9;
            this.lblBillDate.Text = "Bill Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(96, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(114, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(148, 21);
            this.txtDocumentNo.TabIndex = 2;
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 15);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.dgvBill);
            this.grpBody.Controls.Add(this.txtLedgerValue);
            this.grpBody.Controls.Add(this.txtLedgerName);
            this.grpBody.Controls.Add(this.chkAutoCompleationOtherExpence);
            this.grpBody.Controls.Add(this.txtLedgerCode);
            this.grpBody.Controls.Add(this.cmbCostCentre);
            this.grpBody.Location = new System.Drawing.Point(2, 101);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(843, 249);
            this.grpBody.TabIndex = 23;
            this.grpBody.TabStop = false;
            // 
            // dgvBill
            // 
            this.dgvBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccLedgerAccountID,
            this.LedgerCode,
            this.LedgerName,
            this.CostCentre,
            this.ExpenseAmount});
            this.dgvBill.Location = new System.Drawing.Point(6, 16);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.RowHeadersWidth = 40;
            this.dgvBill.Size = new System.Drawing.Size(832, 201);
            this.dgvBill.TabIndex = 53;
            // 
            // txtLedgerValue
            // 
            this.txtLedgerValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLedgerValue.Location = new System.Drawing.Point(678, 223);
            this.txtLedgerValue.Name = "txtLedgerValue";
            this.txtLedgerValue.Size = new System.Drawing.Size(161, 21);
            this.txtLedgerValue.TabIndex = 57;
            this.txtLedgerValue.Tag = "3";
            this.txtLedgerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(173, 223);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(273, 21);
            this.txtLedgerName.TabIndex = 56;
            // 
            // chkAutoCompleationOtherExpence
            // 
            this.chkAutoCompleationOtherExpence.AutoSize = true;
            this.chkAutoCompleationOtherExpence.Checked = true;
            this.chkAutoCompleationOtherExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationOtherExpence.Location = new System.Drawing.Point(6, 226);
            this.chkAutoCompleationOtherExpence.Name = "chkAutoCompleationOtherExpence";
            this.chkAutoCompleationOtherExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationOtherExpence.TabIndex = 55;
            this.chkAutoCompleationOtherExpence.Tag = "1";
            this.chkAutoCompleationOtherExpence.UseVisualStyleBackColor = true;
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(22, 223);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Size = new System.Drawing.Size(150, 21);
            this.txtLedgerCode.TabIndex = 54;
            // 
            // AccLedgerAccountID
            // 
            this.AccLedgerAccountID.DataPropertyName = "AccLedgerAccountID";
            this.AccLedgerAccountID.HeaderText = "AccLedgerAccountID";
            this.AccLedgerAccountID.Name = "AccLedgerAccountID";
            this.AccLedgerAccountID.ReadOnly = true;
            this.AccLedgerAccountID.Visible = false;
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 125;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerName.Width = 275;
            // 
            // CostCentre
            // 
            this.CostCentre.HeaderText = "Cost Centre";
            this.CostCentre.Name = "CostCentre";
            this.CostCentre.Width = 225;
            // 
            // ExpenseAmount
            // 
            this.ExpenseAmount.DataPropertyName = "ExpenseAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = null;
            this.ExpenseAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.ExpenseAmount.HeaderText = "Value";
            this.ExpenseAmount.Name = "ExpenseAmount";
            this.ExpenseAmount.ReadOnly = true;
            this.ExpenseAmount.Width = 140;
            // 
            // txtManualNo
            // 
            this.txtManualNo.Location = new System.Drawing.Point(114, 61);
            this.txtManualNo.Name = "txtManualNo";
            this.txtManualNo.Size = new System.Drawing.Size(178, 21);
            this.txtManualNo.TabIndex = 114;
            // 
            // lblManualNo
            // 
            this.lblManualNo.AutoSize = true;
            this.lblManualNo.Location = new System.Drawing.Point(6, 63);
            this.lblManualNo.Name = "lblManualNo";
            this.lblManualNo.Size = new System.Drawing.Size(66, 13);
            this.lblManualNo.TabIndex = 113;
            this.lblManualNo.Text = "Manual No";
            // 
            // FrmBillEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(847, 393);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Name = "FrmBillEntry";
            this.Text = "Bill Entry";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvBill;
        private CustomControls.TextBoxCurrency txtLedgerValue;
        private System.Windows.Forms.TextBox txtLedgerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationOtherExpence;
        private System.Windows.Forms.TextBox txtLedgerCode;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.Label lblEntryDate;
        private System.Windows.Forms.DateTimePicker dtpReceivedDate;
        private System.Windows.Forms.Label lblReceivedDate;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationSupplier;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.TextBox txtManualNo;
        private System.Windows.Forms.Label lblManualNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccLedgerAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCentre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAmount;
    }
}
