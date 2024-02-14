namespace UI.Windows
{
    partial class FrmPettyCashPayment
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.txtBookBalance = new UI.Windows.CustomControls.TextBoxCurrency();
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
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.dgvConfirmedVoucher = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AccPettyCashVoucherHeaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelVoucher = new System.Windows.Forms.Button();
            this.btnConfirmVoucher = new System.Windows.Forms.Button();
            this.dgvVoucher = new System.Windows.Forms.DataGridView();
            this.SelectedStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IOUAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accPettyCashVoucherHeaderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTotalSettlement = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalVoucherAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCashAvailable = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalPendingAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIssuedAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalIOUAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblCashAmount = new System.Windows.Forms.Label();
            this.lblUsedAmount = new System.Windows.Forms.Label();
            this.txtUsedAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPettyCashBalance = new System.Windows.Forms.Label();
            this.txtPettyCashBalance = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblImprestAmount = new System.Windows.Forms.Label();
            this.txtImprestAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPettyCashLimit = new System.Windows.Forms.Label();
            this.txtPettyCashLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfirmedVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accPettyCashVoucherHeaderBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(645, 506);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 506);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.lblNetAmount);
            this.grpHeader.Controls.Add(this.txtBookBalance);
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
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(3, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(959, 131);
            this.grpHeader.TabIndex = 20;
            this.grpHeader.TabStop = false;
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(658, 37);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(85, 13);
            this.lblNetAmount.TabIndex = 108;
            this.lblNetAmount.Text = "Book Balance";
            // 
            // txtBookBalance
            // 
            this.txtBookBalance.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtBookBalance.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtBookBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtBookBalance.Location = new System.Drawing.Point(763, 34);
            this.txtBookBalance.Name = "txtBookBalance";
            this.txtBookBalance.Size = new System.Drawing.Size(184, 21);
            this.txtBookBalance.TabIndex = 107;
            this.txtBookBalance.Text = "0.00";
            this.txtBookBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayee
            // 
            this.lblPayee.AutoSize = true;
            this.lblPayee.Location = new System.Drawing.Point(7, 84);
            this.lblPayee.Name = "lblPayee";
            this.lblPayee.Size = new System.Drawing.Size(86, 13);
            this.lblPayee.TabIndex = 106;
            this.lblPayee.Text = "Payee Name*";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.Location = new System.Drawing.Point(110, 81);
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.Size = new System.Drawing.Size(533, 21);
            this.txtPayeeName.TabIndex = 105;
            this.txtPayeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayeeName_KeyDown);
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(763, 81);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(184, 21);
            this.cmbCostCentre.TabIndex = 100;
            this.cmbCostCentre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostCentre_KeyDown);
            this.cmbCostCentre.Validated += new System.EventHandler(this.cmbCostCentre_Validated);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(658, 84);
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
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(93, 37);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 96;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPettyCash.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPettyCash_CheckedChanged);
            // 
            // txtPettyCashBookName
            // 
            this.txtPettyCashBookName.Location = new System.Drawing.Point(243, 34);
            this.txtPettyCashBookName.MasterDescription = "";
            this.txtPettyCashBookName.Name = "txtPettyCashBookName";
            this.txtPettyCashBookName.Size = new System.Drawing.Size(400, 21);
            this.txtPettyCashBookName.TabIndex = 94;
            this.txtPettyCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookName_KeyDown);
            this.txtPettyCashBookName.Leave += new System.EventHandler(this.txtPettyCashBookName_Leave);
            // 
            // txtPettyCashBookCode
            // 
            this.txtPettyCashBookCode.Location = new System.Drawing.Point(110, 34);
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
            this.lblPettyCashBook.Location = new System.Drawing.Point(7, 37);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblPettyCashBook.TabIndex = 95;
            this.lblPettyCashBook.Text = "Petty Cash*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(370, 10);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(273, 21);
            this.cmbLocation.TabIndex = 8;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(298, 15);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Location*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(763, 105);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(184, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(658, 108);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(832, 58);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(115, 21);
            this.dtpDocumentDate.TabIndex = 5;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(243, 10);
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
            this.lblRemark.Location = new System.Drawing.Point(7, 108);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 105);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(533, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(7, 61);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(70, 13);
            this.lblEmployee.TabIndex = 13;
            this.lblEmployee.Text = "Employee*";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(243, 58);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(400, 21);
            this.txtEmployeeName.TabIndex = 8;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(93, 61);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 6;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(110, 58);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(130, 21);
            this.txtEmployeeCode.TabIndex = 2;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(658, 61);
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
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(93, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 0;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(110, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(130, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Validated += new System.EventHandler(this.txtDocumentNo_Validated);
            // 
            // lblPurchaseOrderNo
            // 
            this.lblPurchaseOrderNo.AutoSize = true;
            this.lblPurchaseOrderNo.Location = new System.Drawing.Point(7, 15);
            this.lblPurchaseOrderNo.Name = "lblPurchaseOrderNo";
            this.lblPurchaseOrderNo.Size = new System.Drawing.Size(84, 13);
            this.lblPurchaseOrderNo.TabIndex = 1;
            this.lblPurchaseOrderNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.dgvConfirmedVoucher);
            this.grpBody.Controls.Add(this.btnCancelVoucher);
            this.grpBody.Controls.Add(this.btnConfirmVoucher);
            this.grpBody.Controls.Add(this.dgvVoucher);
            this.grpBody.Location = new System.Drawing.Point(4, 125);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(958, 267);
            this.grpBody.TabIndex = 102;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Payment Details";
            // 
            // dgvConfirmedVoucher
            // 
            this.dgvConfirmedVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfirmedVoucher.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.AccPettyCashVoucherHeaderID,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvConfirmedVoucher.Location = new System.Drawing.Point(9, 159);
            this.dgvConfirmedVoucher.Name = "dgvConfirmedVoucher";
            this.dgvConfirmedVoucher.RowHeadersWidth = 15;
            this.dgvConfirmedVoucher.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConfirmedVoucher.Size = new System.Drawing.Size(937, 102);
            this.dgvConfirmedVoucher.TabIndex = 64;
            this.dgvConfirmedVoucher.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConfirmedVoucher_CellContentClick);
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 48;
            // 
            // AccPettyCashVoucherHeaderID
            // 
            this.AccPettyCashVoucherHeaderID.DataPropertyName = "AccPettyCashVoucherHeaderID";
            this.AccPettyCashVoucherHeaderID.HeaderText = "AccPettyCashVoucherHeaderID";
            this.AccPettyCashVoucherHeaderID.Name = "AccPettyCashVoucherHeaderID";
            this.AccPettyCashVoucherHeaderID.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VoucherStatus";
            this.dataGridViewTextBoxColumn2.HeaderText = "VoucherStatus";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 105;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DocumentDate";
            this.dataGridViewTextBoxColumn3.HeaderText = "Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 105;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DocumentNo";
            this.dataGridViewTextBoxColumn4.HeaderText = "Voucher No";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 110;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PayeeName";
            this.dataGridViewTextBoxColumn5.HeaderText = "Payee Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 230;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn6.HeaderText = "Voucher Amount";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 140;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "IOUAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn7.HeaderText = "IOU Amount";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 140;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "BalanceAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn8.HeaderText = "Balance Amount";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 140;
            // 
            // btnCancelVoucher
            // 
            this.btnCancelVoucher.Location = new System.Drawing.Point(834, 131);
            this.btnCancelVoucher.Name = "btnCancelVoucher";
            this.btnCancelVoucher.Size = new System.Drawing.Size(105, 22);
            this.btnCancelVoucher.TabIndex = 63;
            this.btnCancelVoucher.Text = "Cancel Voucher";
            this.btnCancelVoucher.UseVisualStyleBackColor = true;
            this.btnCancelVoucher.Click += new System.EventHandler(this.btnCancelVoucher_Click);
            // 
            // btnConfirmVoucher
            // 
            this.btnConfirmVoucher.Location = new System.Drawing.Point(713, 131);
            this.btnConfirmVoucher.Name = "btnConfirmVoucher";
            this.btnConfirmVoucher.Size = new System.Drawing.Size(115, 22);
            this.btnConfirmVoucher.TabIndex = 62;
            this.btnConfirmVoucher.Text = "Confirm Voucher";
            this.btnConfirmVoucher.UseVisualStyleBackColor = true;
            this.btnConfirmVoucher.Click += new System.EventHandler(this.btnConfirmVoucher_Click);
            // 
            // dgvVoucher
            // 
            this.dgvVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedStatus,
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn,
            this.VoucherStatus,
            this.Date,
            this.VoucherNo,
            this.PayeeName,
            this.VoucherAmount,
            this.IOUAmount,
            this.BalanceAmount});
            this.dgvVoucher.Location = new System.Drawing.Point(9, 16);
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.RowHeadersWidth = 15;
            this.dgvVoucher.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucher.Size = new System.Drawing.Size(937, 109);
            this.dgvVoucher.TabIndex = 53;
            // 
            // SelectedStatus
            // 
            this.SelectedStatus.DataPropertyName = "Status";
            this.SelectedStatus.HeaderText = "Status";
            this.SelectedStatus.Name = "SelectedStatus";
            this.SelectedStatus.Width = 48;
            // 
            // accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn
            // 
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn.DataPropertyName = "AccPettyCashVoucherHeaderID";
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn.HeaderText = "AccPettyCashVoucherHeaderID";
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn.Name = "accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn";
            this.accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // VoucherStatus
            // 
            this.VoucherStatus.DataPropertyName = "VoucherStatus";
            this.VoucherStatus.HeaderText = "VoucherStatus";
            this.VoucherStatus.Name = "VoucherStatus";
            this.VoucherStatus.Visible = false;
            this.VoucherStatus.Width = 105;
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
            this.VoucherNo.HeaderText = "Voucher No";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.Width = 110;
            // 
            // PayeeName
            // 
            this.PayeeName.DataPropertyName = "PayeeName";
            this.PayeeName.HeaderText = "Payee Name";
            this.PayeeName.Name = "PayeeName";
            this.PayeeName.Width = 230;
            // 
            // VoucherAmount
            // 
            this.VoucherAmount.DataPropertyName = "Amount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VoucherAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.VoucherAmount.HeaderText = "Voucher Amount";
            this.VoucherAmount.Name = "VoucherAmount";
            this.VoucherAmount.Width = 140;
            // 
            // IOUAmount
            // 
            this.IOUAmount.DataPropertyName = "IOUAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IOUAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.IOUAmount.HeaderText = "IOU Amount";
            this.IOUAmount.Name = "IOUAmount";
            this.IOUAmount.Width = 140;
            // 
            // BalanceAmount
            // 
            this.BalanceAmount.DataPropertyName = "BalanceAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BalanceAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.BalanceAmount.HeaderText = "Balance Amount";
            this.BalanceAmount.Name = "BalanceAmount";
            this.BalanceAmount.Width = 140;
            // 
            // accPettyCashVoucherHeaderBindingSource
            // 
            this.accPettyCashVoucherHeaderBindingSource.DataSource = typeof(Domain.AccPettyCashVoucherHeader);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotalSettlement);
            this.groupBox3.Location = new System.Drawing.Point(4, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(958, 40);
            this.groupBox3.TabIndex = 106;
            this.groupBox3.TabStop = false;
            // 
            // txtTotalSettlement
            // 
            this.txtTotalSettlement.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalSettlement.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalSettlement.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSettlement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalSettlement.Location = new System.Drawing.Point(787, 13);
            this.txtTotalSettlement.Name = "txtTotalSettlement";
            this.txtTotalSettlement.Size = new System.Drawing.Size(152, 21);
            this.txtTotalSettlement.TabIndex = 113;
            this.txtTotalSettlement.Text = "0.00";
            this.txtTotalSettlement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalVoucherAmount
            // 
            this.txtTotalVoucherAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalVoucherAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalVoucherAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVoucherAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalVoucherAmount.Location = new System.Drawing.Point(158, 15);
            this.txtTotalVoucherAmount.Name = "txtTotalVoucherAmount";
            this.txtTotalVoucherAmount.Size = new System.Drawing.Size(156, 21);
            this.txtTotalVoucherAmount.TabIndex = 115;
            this.txtTotalVoucherAmount.Text = "0.00";
            this.txtTotalVoucherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label3);
            this.grpFooter.Controls.Add(this.txtCashAvailable);
            this.grpFooter.Controls.Add(this.label2);
            this.grpFooter.Controls.Add(this.txtTotalPendingAmount);
            this.grpFooter.Controls.Add(this.label1);
            this.grpFooter.Controls.Add(this.txtIssuedAmount);
            this.grpFooter.Controls.Add(this.txtTotalVoucherAmount);
            this.grpFooter.Controls.Add(this.label8);
            this.grpFooter.Controls.Add(this.txtTotalIOUAmount);
            this.grpFooter.Controls.Add(this.lblCashAmount);
            this.grpFooter.Controls.Add(this.lblUsedAmount);
            this.grpFooter.Controls.Add(this.txtUsedAmount);
            this.grpFooter.Controls.Add(this.lblPettyCashBalance);
            this.grpFooter.Controls.Add(this.txtPettyCashBalance);
            this.grpFooter.Controls.Add(this.lblImprestAmount);
            this.grpFooter.Controls.Add(this.txtImprestAmount);
            this.grpFooter.Controls.Add(this.lblPettyCashLimit);
            this.grpFooter.Controls.Add(this.txtPettyCashLimit);
            this.grpFooter.Location = new System.Drawing.Point(4, 421);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(958, 91);
            this.grpFooter.TabIndex = 107;
            this.grpFooter.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(674, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Cash Available";
            // 
            // txtCashAvailable
            // 
            this.txtCashAvailable.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtCashAvailable.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtCashAvailable.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtCashAvailable.Location = new System.Drawing.Point(787, 61);
            this.txtCashAvailable.Name = "txtCashAvailable";
            this.txtCashAvailable.Size = new System.Drawing.Size(156, 21);
            this.txtCashAvailable.TabIndex = 120;
            this.txtCashAvailable.Text = "0.00";
            this.txtCashAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 119;
            this.label2.Text = "Total Pending Amount";
            // 
            // txtTotalPendingAmount
            // 
            this.txtTotalPendingAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalPendingAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalPendingAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPendingAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalPendingAmount.Location = new System.Drawing.Point(158, 61);
            this.txtTotalPendingAmount.Name = "txtTotalPendingAmount";
            this.txtTotalPendingAmount.Size = new System.Drawing.Size(156, 21);
            this.txtTotalPendingAmount.TabIndex = 118;
            this.txtTotalPendingAmount.Text = "0.00";
            this.txtTotalPendingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(674, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Issued Amount";
            // 
            // txtIssuedAmount
            // 
            this.txtIssuedAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtIssuedAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtIssuedAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssuedAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtIssuedAmount.Location = new System.Drawing.Point(787, 15);
            this.txtIssuedAmount.Name = "txtIssuedAmount";
            this.txtIssuedAmount.Size = new System.Drawing.Size(156, 21);
            this.txtIssuedAmount.TabIndex = 116;
            this.txtIssuedAmount.Text = "0.00";
            this.txtIssuedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 114;
            this.label8.Text = "Total IOU Amount";
            // 
            // txtTotalIOUAmount
            // 
            this.txtTotalIOUAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalIOUAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalIOUAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalIOUAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalIOUAmount.Location = new System.Drawing.Point(158, 38);
            this.txtTotalIOUAmount.Name = "txtTotalIOUAmount";
            this.txtTotalIOUAmount.Size = new System.Drawing.Size(156, 21);
            this.txtTotalIOUAmount.TabIndex = 113;
            this.txtTotalIOUAmount.Text = "0.00";
            this.txtTotalIOUAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCashAmount
            // 
            this.lblCashAmount.AutoSize = true;
            this.lblCashAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashAmount.Location = new System.Drawing.Point(9, 18);
            this.lblCashAmount.Name = "lblCashAmount";
            this.lblCashAmount.Size = new System.Drawing.Size(132, 13);
            this.lblCashAmount.TabIndex = 112;
            this.lblCashAmount.Text = "Total Voucher Amount";
            // 
            // lblUsedAmount
            // 
            this.lblUsedAmount.AutoSize = true;
            this.lblUsedAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsedAmount.Location = new System.Drawing.Point(674, 41);
            this.lblUsedAmount.Name = "lblUsedAmount";
            this.lblUsedAmount.Size = new System.Drawing.Size(83, 13);
            this.lblUsedAmount.TabIndex = 110;
            this.lblUsedAmount.Text = "Used Amount";
            // 
            // txtUsedAmount
            // 
            this.txtUsedAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtUsedAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtUsedAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsedAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtUsedAmount.Location = new System.Drawing.Point(787, 38);
            this.txtUsedAmount.Name = "txtUsedAmount";
            this.txtUsedAmount.Size = new System.Drawing.Size(156, 21);
            this.txtUsedAmount.TabIndex = 109;
            this.txtUsedAmount.Text = "0.00";
            this.txtUsedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPettyCashBalance
            // 
            this.lblPettyCashBalance.AutoSize = true;
            this.lblPettyCashBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPettyCashBalance.Location = new System.Drawing.Point(352, 64);
            this.lblPettyCashBalance.Name = "lblPettyCashBalance";
            this.lblPettyCashBalance.Size = new System.Drawing.Size(85, 13);
            this.lblPettyCashBalance.TabIndex = 108;
            this.lblPettyCashBalance.Text = "Cash Balance";
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
            this.txtPettyCashBalance.Location = new System.Drawing.Point(465, 61);
            this.txtPettyCashBalance.Name = "txtPettyCashBalance";
            this.txtPettyCashBalance.Size = new System.Drawing.Size(156, 21);
            this.txtPettyCashBalance.TabIndex = 107;
            this.txtPettyCashBalance.Text = "0.00";
            this.txtPettyCashBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblImprestAmount
            // 
            this.lblImprestAmount.AutoSize = true;
            this.lblImprestAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImprestAmount.Location = new System.Drawing.Point(352, 41);
            this.lblImprestAmount.Name = "lblImprestAmount";
            this.lblImprestAmount.Size = new System.Drawing.Size(100, 13);
            this.lblImprestAmount.TabIndex = 106;
            this.lblImprestAmount.Text = "Imprest Amount";
            // 
            // txtImprestAmount
            // 
            this.txtImprestAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtImprestAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtImprestAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImprestAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtImprestAmount.Location = new System.Drawing.Point(465, 38);
            this.txtImprestAmount.Name = "txtImprestAmount";
            this.txtImprestAmount.Size = new System.Drawing.Size(156, 21);
            this.txtImprestAmount.TabIndex = 105;
            this.txtImprestAmount.Text = "0.00";
            this.txtImprestAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPettyCashLimit
            // 
            this.lblPettyCashLimit.AutoSize = true;
            this.lblPettyCashLimit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPettyCashLimit.Location = new System.Drawing.Point(352, 18);
            this.lblPettyCashLimit.Name = "lblPettyCashLimit";
            this.lblPettyCashLimit.Size = new System.Drawing.Size(100, 13);
            this.lblPettyCashLimit.TabIndex = 104;
            this.lblPettyCashLimit.Text = "Petty Cash Limit";
            // 
            // txtPettyCashLimit
            // 
            this.txtPettyCashLimit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtPettyCashLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtPettyCashLimit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPettyCashLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPettyCashLimit.Location = new System.Drawing.Point(465, 15);
            this.txtPettyCashLimit.Name = "txtPettyCashLimit";
            this.txtPettyCashLimit.Size = new System.Drawing.Size(156, 21);
            this.txtPettyCashLimit.TabIndex = 103;
            this.txtPettyCashLimit.Text = "0.00";
            this.txtPettyCashLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPettyCashPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(964, 554);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpHeader);
            this.Name = "FrmPettyCashPayment";
            this.Text = "Petty Cash Payment";
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfirmedVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accPettyCashVoucherHeaderBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.DataGridView dgvVoucher;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblPettyCashLimit;
        private CustomControls.TextBoxCurrency txtPettyCashLimit;
        private System.Windows.Forms.Label lblPayee;
        private System.Windows.Forms.TextBox txtPayeeName;
        private System.Windows.Forms.Label lblUsedAmount;
        private CustomControls.TextBoxCurrency txtUsedAmount;
        private System.Windows.Forms.Label lblPettyCashBalance;
        private CustomControls.TextBoxCurrency txtPettyCashBalance;
        private System.Windows.Forms.Label lblImprestAmount;
        private CustomControls.TextBoxCurrency txtImprestAmount;
        private CustomControls.TextBoxCurrency txtTotalSettlement;
        private System.Windows.Forms.Button btnCancelVoucher;
        private System.Windows.Forms.Button btnConfirmVoucher;
        private System.Windows.Forms.DataGridView dgvConfirmedVoucher;
        private System.Windows.Forms.BindingSource accPettyCashVoucherHeaderBindingSource;
        private CustomControls.TextBoxCurrency txtTotalVoucherAmount;
        private System.Windows.Forms.Label label8;
        private CustomControls.TextBoxCurrency txtTotalIOUAmount;
        private System.Windows.Forms.Label lblCashAmount;
        private System.Windows.Forms.Label lblNetAmount;
        private CustomControls.TextBoxCurrency txtBookBalance;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxCurrency txtIssuedAmount;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtTotalPendingAmount;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxCurrency txtCashAvailable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccPettyCashVoucherHeaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn accPettyCashVoucherHeaderIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IOUAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceAmount;
    }
}
