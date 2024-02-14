namespace UI.Windows
{
    partial class FrmPettyCashBillEntry
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
            this.btnIOUDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationIOUNo = new System.Windows.Forms.CheckBox();
            this.lblIOUAmount = new System.Windows.Forms.Label();
            this.txtIOUAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblIOUNo = new System.Windows.Forms.Label();
            this.txtIOUNo = new System.Windows.Forms.TextBox();
            this.lblPettyCashBalance = new System.Windows.Forms.Label();
            this.txtPettyCashBalance = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPayee = new System.Windows.Forms.Label();
            this.txtPayeeName = new System.Windows.Forms.TextBox();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
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
            this.chkAutoCompleationBillNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationLedgerCategory = new System.Windows.Forms.CheckBox();
            this.txtLedgerCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtLegderCategoryCode = new System.Windows.Forms.TextBox();
            this.lblLedgerCategory = new System.Windows.Forms.Label();
            this.dgvBillDetail = new System.Windows.Forms.DataGridView();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtExpenseAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtExpenseLedgerName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationExpence = new System.Windows.Forms.CheckBox();
            this.txtExpenseLedgerCode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.txtReturnAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblReturnAmount = new System.Windows.Forms.Label();
            this.txtToSettleAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalBillAmount = new System.Windows.Forms.Label();
            this.txtTotalBillAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblToSettleAmount = new System.Windows.Forms.Label();
            this.lblCashReturnAmount = new System.Windows.Forms.Label();
            this.txtCashReturnAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetail)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(586, 365);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 365);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.btnIOUDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationIOUNo);
            this.grpHeader.Controls.Add(this.lblIOUAmount);
            this.grpHeader.Controls.Add(this.txtIOUAmount);
            this.grpHeader.Controls.Add(this.lblIOUNo);
            this.grpHeader.Controls.Add(this.txtIOUNo);
            this.grpHeader.Controls.Add(this.lblPettyCashBalance);
            this.grpHeader.Controls.Add(this.txtPettyCashBalance);
            this.grpHeader.Controls.Add(this.lblPayee);
            this.grpHeader.Controls.Add(this.txtPayeeName);
            this.grpHeader.Controls.Add(this.cmbCostCentre);
            this.grpHeader.Controls.Add(this.lblCostCentre);
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
            this.grpHeader.Controls.Add(this.chkAutoCompleationBillNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(3, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(899, 159);
            this.grpHeader.TabIndex = 21;
            this.grpHeader.TabStop = false;
            // 
            // btnIOUDetails
            // 
            this.btnIOUDetails.Location = new System.Drawing.Point(541, 14);
            this.btnIOUDetails.Name = "btnIOUDetails";
            this.btnIOUDetails.Size = new System.Drawing.Size(28, 23);
            this.btnIOUDetails.TabIndex = 114;
            this.btnIOUDetails.Text = "...";
            this.btnIOUDetails.UseVisualStyleBackColor = true;
            this.btnIOUDetails.Click += new System.EventHandler(this.btnIOUDetails_Click);
            // 
            // chkAutoCompleationIOUNo
            // 
            this.chkAutoCompleationIOUNo.AutoSize = true;
            this.chkAutoCompleationIOUNo.Checked = true;
            this.chkAutoCompleationIOUNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationIOUNo.Location = new System.Drawing.Point(391, 16);
            this.chkAutoCompleationIOUNo.Name = "chkAutoCompleationIOUNo";
            this.chkAutoCompleationIOUNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationIOUNo.TabIndex = 113;
            this.chkAutoCompleationIOUNo.Tag = "1";
            this.chkAutoCompleationIOUNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationIOUNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationIOUNo_CheckedChanged);
            // 
            // lblIOUAmount
            // 
            this.lblIOUAmount.AutoSize = true;
            this.lblIOUAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIOUAmount.Location = new System.Drawing.Point(583, 17);
            this.lblIOUAmount.Name = "lblIOUAmount";
            this.lblIOUAmount.Size = new System.Drawing.Size(77, 13);
            this.lblIOUAmount.TabIndex = 112;
            this.lblIOUAmount.Text = "IOU Amount";
            // 
            // txtIOUAmount
            // 
            this.txtIOUAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtIOUAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtIOUAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIOUAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtIOUAmount.Location = new System.Drawing.Point(711, 14);
            this.txtIOUAmount.Name = "txtIOUAmount";
            this.txtIOUAmount.Size = new System.Drawing.Size(182, 21);
            this.txtIOUAmount.TabIndex = 111;
            this.txtIOUAmount.Text = "0.00";
            this.txtIOUAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIOUNo
            // 
            this.lblIOUNo.AutoSize = true;
            this.lblIOUNo.Location = new System.Drawing.Point(325, 17);
            this.lblIOUNo.Name = "lblIOUNo";
            this.lblIOUNo.Size = new System.Drawing.Size(48, 13);
            this.lblIOUNo.TabIndex = 110;
            this.lblIOUNo.Text = "IOU No";
            // 
            // txtIOUNo
            // 
            this.txtIOUNo.Location = new System.Drawing.Point(407, 14);
            this.txtIOUNo.Name = "txtIOUNo";
            this.txtIOUNo.Size = new System.Drawing.Size(131, 21);
            this.txtIOUNo.TabIndex = 109;
            this.txtIOUNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIOUNo_KeyDown);
            this.txtIOUNo.Leave += new System.EventHandler(this.txtIOUNo_Leave);
            // 
            // lblPettyCashBalance
            // 
            this.lblPettyCashBalance.AutoSize = true;
            this.lblPettyCashBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPettyCashBalance.Location = new System.Drawing.Point(583, 42);
            this.lblPettyCashBalance.Name = "lblPettyCashBalance";
            this.lblPettyCashBalance.Size = new System.Drawing.Size(118, 13);
            this.lblPettyCashBalance.TabIndex = 108;
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
            this.txtPettyCashBalance.Location = new System.Drawing.Point(711, 39);
            this.txtPettyCashBalance.Name = "txtPettyCashBalance";
            this.txtPettyCashBalance.Size = new System.Drawing.Size(182, 21);
            this.txtPettyCashBalance.TabIndex = 107;
            this.txtPettyCashBalance.Text = "0.00";
            this.txtPettyCashBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayee
            // 
            this.lblPayee.AutoSize = true;
            this.lblPayee.Location = new System.Drawing.Point(7, 112);
            this.lblPayee.Name = "lblPayee";
            this.lblPayee.Size = new System.Drawing.Size(86, 13);
            this.lblPayee.TabIndex = 106;
            this.lblPayee.Text = "Payee Name*";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.Location = new System.Drawing.Point(110, 109);
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.Size = new System.Drawing.Size(458, 21);
            this.txtPayeeName.TabIndex = 105;
            this.txtPayeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayeeName_KeyDown);
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(711, 109);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(182, 21);
            this.cmbCostCentre.TabIndex = 100;
            this.cmbCostCentre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostCentre_KeyDown);
            this.cmbCostCentre.Validated += new System.EventHandler(this.cmbCostCentre_Validated);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(583, 112);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(83, 13);
            this.lblCostCentre.TabIndex = 99;
            this.lblCostCentre.Text = "Cost Centre*";
            // 
            // chkAutoCompleationPettyCash
            // 
            this.chkAutoCompleationPettyCash.AutoSize = true;
            this.chkAutoCompleationPettyCash.Checked = true;
            this.chkAutoCompleationPettyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(93, 65);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 96;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPettyCash.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPettyCash_CheckedChanged);
            // 
            // txtPettyCashBookName
            // 
            this.txtPettyCashBookName.Location = new System.Drawing.Point(243, 62);
            this.txtPettyCashBookName.MasterDescription = "";
            this.txtPettyCashBookName.Name = "txtPettyCashBookName";
            this.txtPettyCashBookName.Size = new System.Drawing.Size(325, 21);
            this.txtPettyCashBookName.TabIndex = 94;
            this.txtPettyCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookName_KeyDown);
            this.txtPettyCashBookName.Leave += new System.EventHandler(this.txtPettyCashBookName_Leave);
            // 
            // txtPettyCashBookCode
            // 
            this.txtPettyCashBookCode.Location = new System.Drawing.Point(110, 62);
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
            this.lblPettyCashBook.Location = new System.Drawing.Point(7, 65);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblPettyCashBook.TabIndex = 95;
            this.lblPettyCashBook.Text = "Petty Cash*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(110, 38);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(209, 21);
            this.cmbLocation.TabIndex = 8;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 41);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Location*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(711, 133);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(182, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(583, 136);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(455, 39);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(113, 21);
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
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(7, 136);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 133);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(458, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(7, 89);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(63, 13);
            this.lblEmployee.TabIndex = 13;
            this.lblEmployee.Text = "Employee";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(243, 86);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(325, 21);
            this.txtEmployeeName.TabIndex = 8;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(93, 89);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 6;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(110, 86);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(130, 21);
            this.txtEmployeeCode.TabIndex = 2;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(325, 41);
            this.lblDocumentDate.Name = "lblDocumentDate";
            this.lblDocumentDate.Size = new System.Drawing.Size(96, 13);
            this.lblDocumentDate.TabIndex = 9;
            this.lblDocumentDate.Text = "Document Date";
            // 
            // chkAutoCompleationBillNo
            // 
            this.chkAutoCompleationBillNo.AutoSize = true;
            this.chkAutoCompleationBillNo.Checked = true;
            this.chkAutoCompleationBillNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBillNo.Location = new System.Drawing.Point(93, 17);
            this.chkAutoCompleationBillNo.Name = "chkAutoCompleationBillNo";
            this.chkAutoCompleationBillNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBillNo.TabIndex = 0;
            this.chkAutoCompleationBillNo.Tag = "1";
            this.chkAutoCompleationBillNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBillNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBillNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDocumentNo.Location = new System.Drawing.Point(110, 13);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(130, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
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
            this.grpBody.Controls.Add(this.chkAutoCompleationLedgerCategory);
            this.grpBody.Controls.Add(this.txtLedgerCategoryName);
            this.grpBody.Controls.Add(this.txtLegderCategoryCode);
            this.grpBody.Controls.Add(this.lblLedgerCategory);
            this.grpBody.Controls.Add(this.dgvBillDetail);
            this.grpBody.Controls.Add(this.txtExpenseAmount);
            this.grpBody.Controls.Add(this.txtExpenseLedgerName);
            this.grpBody.Controls.Add(this.chkAutoCompleationExpence);
            this.grpBody.Controls.Add(this.txtExpenseLedgerCode);
            this.grpBody.Location = new System.Drawing.Point(3, 153);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(577, 216);
            this.grpBody.TabIndex = 103;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Bill Details";
            // 
            // chkAutoCompleationLedgerCategory
            // 
            this.chkAutoCompleationLedgerCategory.AutoSize = true;
            this.chkAutoCompleationLedgerCategory.Checked = true;
            this.chkAutoCompleationLedgerCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLedgerCategory.Location = new System.Drawing.Point(94, 22);
            this.chkAutoCompleationLedgerCategory.Name = "chkAutoCompleationLedgerCategory";
            this.chkAutoCompleationLedgerCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLedgerCategory.TabIndex = 96;
            this.chkAutoCompleationLedgerCategory.Tag = "1";
            this.chkAutoCompleationLedgerCategory.UseVisualStyleBackColor = true;
            // 
            // txtLedgerCategoryName
            // 
            this.txtLedgerCategoryName.Location = new System.Drawing.Point(244, 19);
            this.txtLedgerCategoryName.MasterDescription = "";
            this.txtLedgerCategoryName.Name = "txtLedgerCategoryName";
            this.txtLedgerCategoryName.Size = new System.Drawing.Size(321, 21);
            this.txtLedgerCategoryName.TabIndex = 94;
            // 
            // txtLegderCategoryCode
            // 
            this.txtLegderCategoryCode.Location = new System.Drawing.Point(111, 19);
            this.txtLegderCategoryCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLegderCategoryCode.Name = "txtLegderCategoryCode";
            this.txtLegderCategoryCode.Size = new System.Drawing.Size(130, 21);
            this.txtLegderCategoryCode.TabIndex = 93;
            // 
            // lblLedgerCategory
            // 
            this.lblLedgerCategory.AutoSize = true;
            this.lblLedgerCategory.Location = new System.Drawing.Point(8, 22);
            this.lblLedgerCategory.Name = "lblLedgerCategory";
            this.lblLedgerCategory.Size = new System.Drawing.Size(60, 13);
            this.lblLedgerCategory.TabIndex = 95;
            this.lblLedgerCategory.Text = "Category";
            // 
            // dgvBillDetail
            // 
            this.dgvBillDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerCode,
            this.LedgerID,
            this.LedgerName,
            this.Amount});
            this.dgvBillDetail.Location = new System.Drawing.Point(6, 53);
            this.dgvBillDetail.Name = "dgvBillDetail";
            this.dgvBillDetail.RowHeadersWidth = 15;
            this.dgvBillDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillDetail.Size = new System.Drawing.Size(559, 131);
            this.dgvBillDetail.TabIndex = 53;
            this.dgvBillDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBillDetail_KeyDown);
            // 
            // LedgerCode
            // 
            this.LedgerCode.DataPropertyName = "LedgerCode";
            this.LedgerCode.HeaderText = "Ledger Code";
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerCode.Width = 130;
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
            this.LedgerName.Width = 280;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 125;
            // 
            // txtExpenseAmount
            // 
            this.txtExpenseAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExpenseAmount.Location = new System.Drawing.Point(433, 190);
            this.txtExpenseAmount.Name = "txtExpenseAmount";
            this.txtExpenseAmount.Size = new System.Drawing.Size(126, 21);
            this.txtExpenseAmount.TabIndex = 57;
            this.txtExpenseAmount.Tag = "3";
            this.txtExpenseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExpenseAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseAmount_KeyDown);
            // 
            // txtExpenseLedgerName
            // 
            this.txtExpenseLedgerName.Location = new System.Drawing.Point(141, 190);
            this.txtExpenseLedgerName.Name = "txtExpenseLedgerName";
            this.txtExpenseLedgerName.Size = new System.Drawing.Size(291, 21);
            this.txtExpenseLedgerName.TabIndex = 56;
            this.txtExpenseLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseLedgerName_KeyDown);
            this.txtExpenseLedgerName.Leave += new System.EventHandler(this.txtExpenseLedgerName_Leave);
            // 
            // chkAutoCompleationExpence
            // 
            this.chkAutoCompleationExpence.AutoSize = true;
            this.chkAutoCompleationExpence.Checked = true;
            this.chkAutoCompleationExpence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationExpence.Location = new System.Drawing.Point(1, 193);
            this.chkAutoCompleationExpence.Name = "chkAutoCompleationExpence";
            this.chkAutoCompleationExpence.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationExpence.TabIndex = 55;
            this.chkAutoCompleationExpence.Tag = "1";
            this.chkAutoCompleationExpence.UseVisualStyleBackColor = true;
            // 
            // txtExpenseLedgerCode
            // 
            this.txtExpenseLedgerCode.Location = new System.Drawing.Point(22, 190);
            this.txtExpenseLedgerCode.Name = "txtExpenseLedgerCode";
            this.txtExpenseLedgerCode.Size = new System.Drawing.Size(118, 21);
            this.txtExpenseLedgerCode.TabIndex = 54;
            this.txtExpenseLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpenseLedgerCode_KeyDown);
            this.txtExpenseLedgerCode.Leave += new System.EventHandler(this.txtExpenseLedgerCode_Leave);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(443, 378);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 59;
            this.button2.Text = "Delete Bill";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.txtReturnAmount);
            this.grpFooter.Controls.Add(this.lblReturnAmount);
            this.grpFooter.Controls.Add(this.txtToSettleAmount);
            this.grpFooter.Controls.Add(this.lblTotalBillAmount);
            this.grpFooter.Controls.Add(this.txtTotalBillAmount);
            this.grpFooter.Controls.Add(this.lblToSettleAmount);
            this.grpFooter.Controls.Add(this.lblCashReturnAmount);
            this.grpFooter.Controls.Add(this.txtCashReturnAmount);
            this.grpFooter.Location = new System.Drawing.Point(581, 153);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(321, 216);
            this.grpFooter.TabIndex = 107;
            this.grpFooter.TabStop = false;
            // 
            // txtReturnAmount
            // 
            this.txtReturnAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtReturnAmount.Location = new System.Drawing.Point(133, 153);
            this.txtReturnAmount.Name = "txtReturnAmount";
            this.txtReturnAmount.Size = new System.Drawing.Size(182, 21);
            this.txtReturnAmount.TabIndex = 118;
            this.txtReturnAmount.Tag = "3";
            this.txtReturnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblReturnAmount
            // 
            this.lblReturnAmount.AutoSize = true;
            this.lblReturnAmount.Location = new System.Drawing.Point(5, 156);
            this.lblReturnAmount.Name = "lblReturnAmount";
            this.lblReturnAmount.Size = new System.Drawing.Size(93, 13);
            this.lblReturnAmount.TabIndex = 117;
            this.lblReturnAmount.Text = "Return Amount";
            // 
            // txtToSettleAmount
            // 
            this.txtToSettleAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtToSettleAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtToSettleAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToSettleAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtToSettleAmount.Location = new System.Drawing.Point(133, 99);
            this.txtToSettleAmount.Name = "txtToSettleAmount";
            this.txtToSettleAmount.Size = new System.Drawing.Size(182, 21);
            this.txtToSettleAmount.TabIndex = 115;
            this.txtToSettleAmount.Text = "0.00";
            this.txtToSettleAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalBillAmount
            // 
            this.lblTotalBillAmount.AutoSize = true;
            this.lblTotalBillAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBillAmount.Location = new System.Drawing.Point(5, 183);
            this.lblTotalBillAmount.Name = "lblTotalBillAmount";
            this.lblTotalBillAmount.Size = new System.Drawing.Size(110, 13);
            this.lblTotalBillAmount.TabIndex = 114;
            this.lblTotalBillAmount.Text = "Total Bill Amount*";
            // 
            // txtTotalBillAmount
            // 
            this.txtTotalBillAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalBillAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalBillAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBillAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalBillAmount.Location = new System.Drawing.Point(133, 180);
            this.txtTotalBillAmount.Name = "txtTotalBillAmount";
            this.txtTotalBillAmount.Size = new System.Drawing.Size(182, 21);
            this.txtTotalBillAmount.TabIndex = 113;
            this.txtTotalBillAmount.Text = "0.00";
            this.txtTotalBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblToSettleAmount
            // 
            this.lblToSettleAmount.AutoSize = true;
            this.lblToSettleAmount.Location = new System.Drawing.Point(5, 102);
            this.lblToSettleAmount.Name = "lblToSettleAmount";
            this.lblToSettleAmount.Size = new System.Drawing.Size(105, 13);
            this.lblToSettleAmount.TabIndex = 107;
            this.lblToSettleAmount.Text = "To Settle Amount";
            // 
            // lblCashReturnAmount
            // 
            this.lblCashReturnAmount.AutoSize = true;
            this.lblCashReturnAmount.Location = new System.Drawing.Point(5, 134);
            this.lblCashReturnAmount.Name = "lblCashReturnAmount";
            this.lblCashReturnAmount.Size = new System.Drawing.Size(126, 13);
            this.lblCashReturnAmount.TabIndex = 84;
            this.lblCashReturnAmount.Text = "Cash Return Amount";
            // 
            // txtCashReturnAmount
            // 
            this.txtCashReturnAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtCashReturnAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtCashReturnAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCashReturnAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtCashReturnAmount.Location = new System.Drawing.Point(133, 126);
            this.txtCashReturnAmount.Name = "txtCashReturnAmount";
            this.txtCashReturnAmount.Size = new System.Drawing.Size(182, 21);
            this.txtCashReturnAmount.TabIndex = 83;
            this.txtCashReturnAmount.Text = "0.00";
            this.txtCashReturnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPettyCashBillEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(905, 413);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.button2);
            this.Name = "FrmPettyCashBillEntry";
            this.Text = "Petty Cash Bill Entry";
            this.Load += new System.EventHandler(this.FrmPettyCashBillEntry_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetail)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label lblPayee;
        private System.Windows.Forms.TextBox txtPayeeName;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
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
        private System.Windows.Forms.CheckBox chkAutoCompleationBillNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.GroupBox grpBody;
        private CustomControls.TextBoxCurrency txtExpenseAmount;
        private System.Windows.Forms.TextBox txtExpenseLedgerName;
        private System.Windows.Forms.CheckBox chkAutoCompleationExpence;
        private System.Windows.Forms.TextBox txtExpenseLedgerCode;
        private System.Windows.Forms.DataGridView dgvBillDetail;
        private System.Windows.Forms.CheckBox chkAutoCompleationLedgerCategory;
        private CustomControls.TextBoxMasterDescription txtLedgerCategoryName;
        private System.Windows.Forms.TextBox txtLegderCategoryCode;
        private System.Windows.Forms.Label lblLedgerCategory;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblPettyCashBalance;
        private CustomControls.TextBoxCurrency txtPettyCashBalance;
        private System.Windows.Forms.Label lblIOUNo;
        private System.Windows.Forms.TextBox txtIOUNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalBillAmount;
        private CustomControls.TextBoxCurrency txtTotalBillAmount;
        private System.Windows.Forms.Label lblToSettleAmount;
        private System.Windows.Forms.Label lblCashReturnAmount;
        private CustomControls.TextBoxCurrency txtCashReturnAmount;
        private System.Windows.Forms.Label lblIOUAmount;
        private CustomControls.TextBoxCurrency txtIOUAmount;
        private CustomControls.TextBoxCurrency txtToSettleAmount;
        private System.Windows.Forms.Label lblReturnAmount;
        private CustomControls.TextBoxCurrency txtReturnAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationIOUNo;
        private System.Windows.Forms.Button btnIOUDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}
