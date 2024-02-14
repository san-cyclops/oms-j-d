namespace UI.Windows
{
    partial class FrmPettyCashIOU
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
            this.lblPettyCashBalance = new System.Windows.Forms.Label();
            this.txtPettyCashBalance = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPayee = new System.Windows.Forms.Label();
            this.txtPayeeName = new System.Windows.Forms.TextBox();
            this.lblTotalIOUAmount = new System.Windows.Forms.Label();
            this.txtBalanceIOUAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationPettyCash = new System.Windows.Forms.CheckBox();
            this.txtPettyCashBookName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPettyCashBookCode = new System.Windows.Forms.TextBox();
            this.lblPettyCashBook = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.txtEmployeeCode = new System.Windows.Forms.TextBox();
            this.lblDocumentDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpenseTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.dgvIOUDetail = new System.Windows.Forms.DataGridView();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherExpenseTempID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtExpenseAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtExpenseLedgerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationExpence = new System.Windows.Forms.CheckBox();
            this.txtExpenseLedgerCode = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIOUDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(346, 468);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 468);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.lblPettyCashBalance);
            this.grpHeader.Controls.Add(this.txtPettyCashBalance);
            this.grpHeader.Controls.Add(this.lblPayee);
            this.grpHeader.Controls.Add(this.txtPayeeName);
            this.grpHeader.Controls.Add(this.lblTotalIOUAmount);
            this.grpHeader.Controls.Add(this.txtBalanceIOUAmount);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPettyCash);
            this.grpHeader.Controls.Add(this.txtPettyCashBookName);
            this.grpHeader.Controls.Add(this.txtPettyCashBookCode);
            this.grpHeader.Controls.Add(this.lblPettyCashBook);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblEmployee);
            this.grpHeader.Controls.Add(this.txtEmployeeName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationEmployee);
            this.grpHeader.Controls.Add(this.txtEmployeeCode);
            this.grpHeader.Controls.Add(this.lblDocumentDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -3);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(660, 181);
            this.grpHeader.TabIndex = 21;
            this.grpHeader.TabStop = false;
            // 
            // lblPettyCashBalance
            // 
            this.lblPettyCashBalance.AutoSize = true;
            this.lblPettyCashBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPettyCashBalance.Location = new System.Drawing.Point(372, 134);
            this.lblPettyCashBalance.Name = "lblPettyCashBalance";
            this.lblPettyCashBalance.Size = new System.Drawing.Size(118, 13);
            this.lblPettyCashBalance.TabIndex = 112;
            this.lblPettyCashBalance.Text = "Petty Cash Balance";
            // 
            // txtPettyCashBalance
            // 
            this.txtPettyCashBalance.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtPettyCashBalance.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtPettyCashBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPettyCashBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPettyCashBalance.Location = new System.Drawing.Point(493, 131);
            this.txtPettyCashBalance.Name = "txtPettyCashBalance";
            this.txtPettyCashBalance.Size = new System.Drawing.Size(156, 21);
            this.txtPettyCashBalance.TabIndex = 111;
            this.txtPettyCashBalance.Text = "0.00";
            this.txtPettyCashBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayee
            // 
            this.lblPayee.AutoSize = true;
            this.lblPayee.Location = new System.Drawing.Point(7, 63);
            this.lblPayee.Name = "lblPayee";
            this.lblPayee.Size = new System.Drawing.Size(79, 13);
            this.lblPayee.TabIndex = 104;
            this.lblPayee.Text = "Payee Name";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.Location = new System.Drawing.Point(110, 60);
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.Size = new System.Drawing.Size(539, 21);
            this.txtPayeeName.TabIndex = 103;
            this.txtPayeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayeeName_KeyDown);
            // 
            // lblTotalIOUAmount
            // 
            this.lblTotalIOUAmount.AutoSize = true;
            this.lblTotalIOUAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalIOUAmount.Location = new System.Drawing.Point(372, 88);
            this.lblTotalIOUAmount.Name = "lblTotalIOUAmount";
            this.lblTotalIOUAmount.Size = new System.Drawing.Size(104, 13);
            this.lblTotalIOUAmount.TabIndex = 102;
            this.lblTotalIOUAmount.Text = "IOU Balance O/S";
            // 
            // txtBalanceIOUAmount
            // 
            this.txtBalanceIOUAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtBalanceIOUAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtBalanceIOUAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceIOUAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtBalanceIOUAmount.Location = new System.Drawing.Point(489, 84);
            this.txtBalanceIOUAmount.Name = "txtBalanceIOUAmount";
            this.txtBalanceIOUAmount.Size = new System.Drawing.Size(160, 21);
            this.txtBalanceIOUAmount.TabIndex = 101;
            this.txtBalanceIOUAmount.Text = "0.00";
            this.txtBalanceIOUAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkAutoCompleationPettyCash
            // 
            this.chkAutoCompleationPettyCash.AutoSize = true;
            this.chkAutoCompleationPettyCash.Checked = true;
            this.chkAutoCompleationPettyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(93, 110);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 96;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPettyCash.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPettyCash_CheckedChanged);
            // 
            // txtPettyCashBookName
            // 
            this.txtPettyCashBookName.Location = new System.Drawing.Point(243, 107);
            this.txtPettyCashBookName.MasterDescription = "";
            this.txtPettyCashBookName.Name = "txtPettyCashBookName";
            this.txtPettyCashBookName.Size = new System.Drawing.Size(406, 21);
            this.txtPettyCashBookName.TabIndex = 94;
            this.txtPettyCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookName_KeyDown);
            this.txtPettyCashBookName.Leave += new System.EventHandler(this.txtPettyCashBookName_Leave);
            // 
            // txtPettyCashBookCode
            // 
            this.txtPettyCashBookCode.Location = new System.Drawing.Point(110, 107);
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
            this.lblPettyCashBook.Location = new System.Drawing.Point(7, 110);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblPettyCashBook.TabIndex = 95;
            this.lblPettyCashBook.Text = "Petty Cash*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(110, 84);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(223, 21);
            this.cmbLocation.TabIndex = 8;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 87);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Location*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(110, 131);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(176, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(7, 134);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(542, 14);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(107, 21);
            this.dtpDocumentDate.TabIndex = 5;
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
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(7, 156);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 156);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(539, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(7, 40);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(70, 13);
            this.lblEmployee.TabIndex = 13;
            this.lblEmployee.Text = "Employee*";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(243, 37);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(406, 21);
            this.txtEmployeeName.TabIndex = 8;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(93, 40);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 6;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(110, 37);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(130, 21);
            this.txtEmployeeCode.TabIndex = 2;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(372, 17);
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
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationIOUNo_CheckedChanged);
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
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label1);
            this.grpFooter.Controls.Add(this.txtExpenseTotalAmount);
            this.grpFooter.Location = new System.Drawing.Point(3, 415);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(659, 57);
            this.grpFooter.TabIndex = 22;
            this.grpFooter.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Total Amount";
            // 
            // txtExpenseTotalAmount
            // 
            this.txtExpenseTotalAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtExpenseTotalAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtExpenseTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpenseTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtExpenseTotalAmount.Location = new System.Drawing.Point(416, 22);
            this.txtExpenseTotalAmount.Name = "txtExpenseTotalAmount";
            this.txtExpenseTotalAmount.Size = new System.Drawing.Size(209, 21);
            this.txtExpenseTotalAmount.TabIndex = 105;
            this.txtExpenseTotalAmount.Text = "0.00";
            this.txtExpenseTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.dgvIOUDetail);
            this.grpBody.Controls.Add(this.txtExpenseAmount);
            this.grpBody.Controls.Add(this.txtExpenseLedgerName);
            this.grpBody.Controls.Add(this.chkAutoCompleationExpence);
            this.grpBody.Controls.Add(this.txtExpenseLedgerCode);
            this.grpBody.Location = new System.Drawing.Point(3, 173);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(659, 247);
            this.grpBody.TabIndex = 106;
            this.grpBody.TabStop = false;
            // 
            // dgvIOUDetail
            // 
            this.dgvIOUDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIOUDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerCode,
            this.LedgerID,
            this.LedgerName,
            this.Amount,
            this.OtherExpenseTempID});
            this.dgvIOUDetail.Location = new System.Drawing.Point(9, 11);
            this.dgvIOUDetail.Name = "dgvIOUDetail";
            this.dgvIOUDetail.RowHeadersWidth = 15;
            this.dgvIOUDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIOUDetail.Size = new System.Drawing.Size(639, 201);
            this.dgvIOUDetail.TabIndex = 58;
            this.dgvIOUDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvIOUDetail_KeyDown);
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 150;
            // 
            // AccLedgerAccountID
            // 
            this.LedgerID.DataPropertyName = "AccLedgerAccountID";
            this.LedgerID.HeaderText = "AccLedgerAccountID";
            this.LedgerID.Name = "AccLedgerAccountID";
            this.LedgerID.ReadOnly = true;
            this.LedgerID.Visible = false;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerName.Width = 300;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 150;
            // 
            // OtherExpenseTempID
            // 
            this.OtherExpenseTempID.DataPropertyName = "OtherExpenseTempID";
            this.OtherExpenseTempID.HeaderText = "OtherExpenseTempID";
            this.OtherExpenseTempID.Name = "OtherExpenseTempID";
            this.OtherExpenseTempID.ReadOnly = true;
            this.OtherExpenseTempID.Visible = false;
            // 
            // txtExpenseAmount
            // 
            this.txtExpenseAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExpenseAmount.Location = new System.Drawing.Point(481, 218);
            this.txtExpenseAmount.Name = "txtExpenseAmount";
            this.txtExpenseAmount.Size = new System.Drawing.Size(144, 21);
            this.txtExpenseAmount.TabIndex = 62;
            this.txtExpenseAmount.Tag = "3";
            this.txtExpenseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExpenseAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseAmount_KeyDown);
            // 
            // txtExpenseLedgerName
            // 
            this.txtExpenseLedgerName.Location = new System.Drawing.Point(163, 218);
            this.txtExpenseLedgerName.Name = "txtExpenseLedgerName";
            this.txtExpenseLedgerName.Size = new System.Drawing.Size(317, 21);
            this.txtExpenseLedgerName.TabIndex = 61;
            this.txtExpenseLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseLedgerName_KeyDown);
            this.txtExpenseLedgerName.Leave += new System.EventHandler(this.txtExpenseLedgerName_Leave);
            // 
            // chkAutoCompleationExpence
            // 
            this.chkAutoCompleationExpence.AutoSize = true;
            this.chkAutoCompleationExpence.Checked = true;
            this.chkAutoCompleationExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationExpence.Location = new System.Drawing.Point(12, 221);
            this.chkAutoCompleationExpence.Name = "chkAutoCompleationExpence";
            this.chkAutoCompleationExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationExpence.TabIndex = 60;
            this.chkAutoCompleationExpence.Tag = "1";
            this.chkAutoCompleationExpence.UseVisualStyleBackColor = true;
            // 
            // txtExpenseLedgerCode
            // 
            this.txtExpenseLedgerCode.Location = new System.Drawing.Point(33, 218);
            this.txtExpenseLedgerCode.Name = "txtExpenseLedgerCode";
            this.txtExpenseLedgerCode.Size = new System.Drawing.Size(128, 21);
            this.txtExpenseLedgerCode.TabIndex = 59;
            this.txtExpenseLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseLedgerCode_KeyDown);
            this.txtExpenseLedgerCode.Leave += new System.EventHandler(this.txtExpenseLedgerCode_Leave);
            // 
            // FrmPettyCashIOU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(665, 516);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmPettyCashIOU";
            this.Text = "Petty Cash IOU";
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
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIOUDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.CheckBox chkAutoCompleationPettyCash;
        private CustomControls.TextBoxMasterDescription txtPettyCashBookName;
        private System.Windows.Forms.TextBox txtPettyCashBookCode;
        private System.Windows.Forms.Label lblPettyCashBook;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.Label lblDocumentDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalIOUAmount;
        private CustomControls.TextBoxCurrency txtBalanceIOUAmount;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvIOUDetail;
        private CustomControls.TextBoxCurrency txtExpenseAmount;
        private System.Windows.Forms.TextBox txtExpenseLedgerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationExpence;
        private System.Windows.Forms.TextBox txtExpenseLedgerCode;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxCurrency txtExpenseTotalAmount;
        private System.Windows.Forms.Label lblPayee;
        private System.Windows.Forms.TextBox txtPayeeName;
        private System.Windows.Forms.Label lblPettyCashBalance;
        private CustomControls.TextBoxCurrency txtPettyCashBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherExpenseTempID;
    }
}
