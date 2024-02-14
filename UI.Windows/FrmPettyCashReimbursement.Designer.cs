namespace UI.Windows
{
    partial class FrmPettyCashReimbursement
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtImprestAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblPayee = new System.Windows.Forms.Label();
            this.txtPayeeName = new System.Windows.Forms.TextBox();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.txtBookBalance = new UI.Windows.CustomControls.TextBoxCurrency();
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
            this.chkAutoCompleationReimbursementNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPurchaseOrderNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.rdoCheque = new System.Windows.Forms.RadioButton();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBankBookCode = new System.Windows.Forms.ComboBox();
            this.cmbBankBookName = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationBankBook = new System.Windows.Forms.CheckBox();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBankBook = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbCashBookCode = new System.Windows.Forms.ComboBox();
            this.cmbCashBookName = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationCashBook = new System.Windows.Forms.CheckBox();
            this.lblCashBook = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.lblReimbursementAmount = new System.Windows.Forms.Label();
            this.txtReimbursementAmount = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(613, 321);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 321);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.label3);
            this.grpHeader.Controls.Add(this.txtImprestAmount);
            this.grpHeader.Controls.Add(this.lblPayee);
            this.grpHeader.Controls.Add(this.txtPayeeName);
            this.grpHeader.Controls.Add(this.lblNetAmount);
            this.grpHeader.Controls.Add(this.txtBookBalance);
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
            this.grpHeader.Controls.Add(this.chkAutoCompleationReimbursementNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblPurchaseOrderNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -4);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(928, 155);
            this.grpHeader.TabIndex = 19;
            this.grpHeader.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(602, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 110;
            this.label3.Text = "Imprest Amount";
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
            this.txtImprestAmount.Location = new System.Drawing.Point(710, 107);
            this.txtImprestAmount.Name = "txtImprestAmount";
            this.txtImprestAmount.Size = new System.Drawing.Size(209, 21);
            this.txtImprestAmount.TabIndex = 109;
            this.txtImprestAmount.Text = "0.00";
            this.txtImprestAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayee
            // 
            this.lblPayee.AutoSize = true;
            this.lblPayee.Location = new System.Drawing.Point(7, 110);
            this.lblPayee.Name = "lblPayee";
            this.lblPayee.Size = new System.Drawing.Size(79, 13);
            this.lblPayee.TabIndex = 108;
            this.lblPayee.Text = "Payee Name";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.Location = new System.Drawing.Point(110, 107);
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.Size = new System.Drawing.Size(467, 21);
            this.txtPayeeName.TabIndex = 107;
            this.txtPayeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayeeName_KeyDown);
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(602, 133);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(85, 13);
            this.lblNetAmount.TabIndex = 98;
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
            this.txtBookBalance.Location = new System.Drawing.Point(710, 130);
            this.txtBookBalance.Name = "txtBookBalance";
            this.txtBookBalance.Size = new System.Drawing.Size(209, 21);
            this.txtBookBalance.TabIndex = 97;
            this.txtBookBalance.Text = "0.00";
            this.txtBookBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkAutoCompleationPettyCash
            // 
            this.chkAutoCompleationPettyCash.AutoSize = true;
            this.chkAutoCompleationPettyCash.Checked = true;
            this.chkAutoCompleationPettyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPettyCash.Location = new System.Drawing.Point(93, 63);
            this.chkAutoCompleationPettyCash.Name = "chkAutoCompleationPettyCash";
            this.chkAutoCompleationPettyCash.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPettyCash.TabIndex = 96;
            this.chkAutoCompleationPettyCash.Tag = "1";
            this.chkAutoCompleationPettyCash.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPettyCash.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPettyCash_CheckedChanged);
            // 
            // txtPettyCashBookName
            // 
            this.txtPettyCashBookName.Location = new System.Drawing.Point(243, 60);
            this.txtPettyCashBookName.MasterDescription = "";
            this.txtPettyCashBookName.Name = "txtPettyCashBookName";
            this.txtPettyCashBookName.Size = new System.Drawing.Size(334, 21);
            this.txtPettyCashBookName.TabIndex = 94;
            this.txtPettyCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookName_KeyDown);
            this.txtPettyCashBookName.Leave += new System.EventHandler(this.txtPettyCashBookName_Leave);
            // 
            // txtPettyCashBookCode
            // 
            this.txtPettyCashBookCode.Location = new System.Drawing.Point(110, 60);
            this.txtPettyCashBookCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPettyCashBookCode.Name = "txtPettyCashBookCode";
            this.txtPettyCashBookCode.Size = new System.Drawing.Size(130, 21);
            this.txtPettyCashBookCode.TabIndex = 93;
            this.txtPettyCashBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPettyCashBookCode_KeyDown);
            this.txtPettyCashBookCode.Leave += new System.EventHandler(this.txtPettyCashBookCode_Leave);
            // 
            // lblPettyCashBook
            // 
            this.lblPettyCashBook.AutoSize = true;
            this.lblPettyCashBook.Location = new System.Drawing.Point(7, 63);
            this.lblPettyCashBook.Name = "lblPettyCashBook";
            this.lblPettyCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblPettyCashBook.TabIndex = 95;
            this.lblPettyCashBook.Text = "Petty Cash*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(110, 36);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(263, 21);
            this.cmbLocation.TabIndex = 8;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Leave += new System.EventHandler(this.cmbLocation_Leave);
            this.cmbLocation.Move += new System.EventHandler(this.cmbLocation_Move);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(7, 40);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Location*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(710, 84);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(209, 21);
            this.txtReferenceNo.TabIndex = 7;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(602, 86);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(811, 32);
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
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(7, 133);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 130);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(467, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(7, 87);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(63, 13);
            this.lblEmployee.TabIndex = 13;
            this.lblEmployee.Text = "Employee";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(243, 84);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(334, 21);
            this.txtEmployeeName.TabIndex = 8;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(93, 87);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 6;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(110, 84);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(130, 21);
            this.txtEmployeeCode.TabIndex = 2;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(601, 39);
            this.lblDocumentDate.Name = "lblDocumentDate";
            this.lblDocumentDate.Size = new System.Drawing.Size(96, 13);
            this.lblDocumentDate.TabIndex = 9;
            this.lblDocumentDate.Text = "Document Date";
            // 
            // chkAutoCompleationReimbursementNo
            // 
            this.chkAutoCompleationReimbursementNo.AutoSize = true;
            this.chkAutoCompleationReimbursementNo.Checked = true;
            this.chkAutoCompleationReimbursementNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationReimbursementNo.Location = new System.Drawing.Point(93, 17);
            this.chkAutoCompleationReimbursementNo.Name = "chkAutoCompleationReimbursementNo";
            this.chkAutoCompleationReimbursementNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationReimbursementNo.TabIndex = 0;
            this.chkAutoCompleationReimbursementNo.Tag = "1";
            this.chkAutoCompleationReimbursementNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationReimbursementNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationReimbursementNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(110, 13);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(130, 21);
            this.txtDocumentNo.TabIndex = 1;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
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
            this.grpBody.Controls.Add(this.rdoCheque);
            this.grpBody.Controls.Add(this.rdoCash);
            this.grpBody.Controls.Add(this.lblPaymentMethod);
            this.grpBody.Controls.Add(this.groupBox1);
            this.grpBody.Controls.Add(this.groupBox2);
            this.grpBody.Location = new System.Drawing.Point(2, 146);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(928, 131);
            this.grpBody.TabIndex = 20;
            this.grpBody.TabStop = false;
            // 
            // rdoCheque
            // 
            this.rdoCheque.AutoSize = true;
            this.rdoCheque.Location = new System.Drawing.Point(393, 15);
            this.rdoCheque.Name = "rdoCheque";
            this.rdoCheque.Size = new System.Drawing.Size(69, 17);
            this.rdoCheque.TabIndex = 54;
            this.rdoCheque.TabStop = true;
            this.rdoCheque.Text = "Cheque";
            this.rdoCheque.UseVisualStyleBackColor = true;
            this.rdoCheque.CheckedChanged += new System.EventHandler(this.rdoCheque_CheckedChanged);
            this.rdoCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdoCheque_KeyDown);
            // 
            // rdoCash
            // 
            this.rdoCash.AutoSize = true;
            this.rdoCash.Location = new System.Drawing.Point(479, 15);
            this.rdoCash.Name = "rdoCash";
            this.rdoCash.Size = new System.Drawing.Size(54, 17);
            this.rdoCash.TabIndex = 53;
            this.rdoCash.TabStop = true;
            this.rdoCash.Text = "Cash";
            this.rdoCash.UseVisualStyleBackColor = true;
            this.rdoCash.CheckedChanged += new System.EventHandler(this.rdoCash_CheckedChanged);
            this.rdoCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdoCash_KeyDown);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(325, 15);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(59, 13);
            this.lblPaymentMethod.TabIndex = 52;
            this.lblPaymentMethod.Text = "Method *";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBankBookCode);
            this.groupBox1.Controls.Add(this.cmbBankBookName);
            this.groupBox1.Controls.Add(this.chkAutoCompleationBankBook);
            this.groupBox1.Controls.Add(this.dtpChequeDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblBankBook);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtChequeNo);
            this.groupBox1.Location = new System.Drawing.Point(7, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Details";
            // 
            // cmbBankBookCode
            // 
            this.cmbBankBookCode.FormattingEnabled = true;
            this.cmbBankBookCode.Location = new System.Drawing.Point(120, 16);
            this.cmbBankBookCode.Name = "cmbBankBookCode";
            this.cmbBankBookCode.Size = new System.Drawing.Size(113, 21);
            this.cmbBankBookCode.TabIndex = 104;
            this.cmbBankBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankBookCode_KeyDown);
            this.cmbBankBookCode.Validated += new System.EventHandler(this.cmbBankBookCode_Validated);
            // 
            // cmbBankBookName
            // 
            this.cmbBankBookName.FormattingEnabled = true;
            this.cmbBankBookName.Location = new System.Drawing.Point(236, 16);
            this.cmbBankBookName.Name = "cmbBankBookName";
            this.cmbBankBookName.Size = new System.Drawing.Size(213, 21);
            this.cmbBankBookName.TabIndex = 105;
            this.cmbBankBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBankBookName_KeyDown);
            this.cmbBankBookName.Leave += new System.EventHandler(this.cmbBankBookName_Leave);
            // 
            // chkAutoCompleationBankBook
            // 
            this.chkAutoCompleationBankBook.AutoSize = true;
            this.chkAutoCompleationBankBook.Checked = true;
            this.chkAutoCompleationBankBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBankBook.Location = new System.Drawing.Point(103, 19);
            this.chkAutoCompleationBankBook.Name = "chkAutoCompleationBankBook";
            this.chkAutoCompleationBankBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBankBook.TabIndex = 103;
            this.chkAutoCompleationBankBook.Tag = "1";
            this.chkAutoCompleationBankBook.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBankBook.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBankBook_CheckedChanged);
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChequeDate.Location = new System.Drawing.Point(120, 39);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(98, 21);
            this.dtpChequeDate.TabIndex = 94;
            this.dtpChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpChequeDate_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "Cheque Date";
            // 
            // lblBankBook
            // 
            this.lblBankBook.AutoSize = true;
            this.lblBankBook.Location = new System.Drawing.Point(14, 19);
            this.lblBankBook.Name = "lblBankBook";
            this.lblBankBook.Size = new System.Drawing.Size(43, 13);
            this.lblBankBook.TabIndex = 90;
            this.lblBankBook.Text = "Bank*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 89;
            this.label7.Text = "Cheque No*";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(120, 63);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(98, 21);
            this.txtChequeNo.TabIndex = 88;
            this.txtChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNo_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbCashBookCode);
            this.groupBox2.Controls.Add(this.cmbCashBookName);
            this.groupBox2.Controls.Add(this.chkAutoCompleationCashBook);
            this.groupBox2.Controls.Add(this.lblCashBook);
            this.groupBox2.Location = new System.Drawing.Point(468, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cash Details";
            // 
            // cmbCashBookCode
            // 
            this.cmbCashBookCode.FormattingEnabled = true;
            this.cmbCashBookCode.Location = new System.Drawing.Point(109, 16);
            this.cmbCashBookCode.Name = "cmbCashBookCode";
            this.cmbCashBookCode.Size = new System.Drawing.Size(113, 21);
            this.cmbCashBookCode.TabIndex = 106;
            this.cmbCashBookCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCashBookCode_KeyDown);
            this.cmbCashBookCode.Validated += new System.EventHandler(this.cmbCashBookCode_Validated);
            // 
            // cmbCashBookName
            // 
            this.cmbCashBookName.FormattingEnabled = true;
            this.cmbCashBookName.Location = new System.Drawing.Point(225, 16);
            this.cmbCashBookName.Name = "cmbCashBookName";
            this.cmbCashBookName.Size = new System.Drawing.Size(213, 21);
            this.cmbCashBookName.TabIndex = 107;
            this.cmbCashBookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCashBookName_KeyDown);
            this.cmbCashBookName.Leave += new System.EventHandler(this.cmbCashBookName_Leave);
            // 
            // chkAutoCompleationCashBook
            // 
            this.chkAutoCompleationCashBook.AutoSize = true;
            this.chkAutoCompleationCashBook.Checked = true;
            this.chkAutoCompleationCashBook.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCashBook.Location = new System.Drawing.Point(94, 22);
            this.chkAutoCompleationCashBook.Name = "chkAutoCompleationCashBook";
            this.chkAutoCompleationCashBook.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCashBook.TabIndex = 100;
            this.chkAutoCompleationCashBook.Tag = "1";
            this.chkAutoCompleationCashBook.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCashBook.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCashBook_CheckedChanged);
            // 
            // lblCashBook
            // 
            this.lblCashBook.AutoSize = true;
            this.lblCashBook.Location = new System.Drawing.Point(8, 22);
            this.lblCashBook.Name = "lblCashBook";
            this.lblCashBook.Size = new System.Drawing.Size(76, 13);
            this.lblCashBook.TabIndex = 99;
            this.lblCashBook.Text = "Cash Book*";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblReimbursementAmount);
            this.grpFooter.Controls.Add(this.txtReimbursementAmount);
            this.grpFooter.Location = new System.Drawing.Point(2, 272);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(928, 54);
            this.grpFooter.TabIndex = 90;
            this.grpFooter.TabStop = false;
            // 
            // lblReimbursementAmount
            // 
            this.lblReimbursementAmount.AutoSize = true;
            this.lblReimbursementAmount.Location = new System.Drawing.Point(21, 23);
            this.lblReimbursementAmount.Name = "lblReimbursementAmount";
            this.lblReimbursementAmount.Size = new System.Drawing.Size(58, 13);
            this.lblReimbursementAmount.TabIndex = 89;
            this.lblReimbursementAmount.Text = "Amount*";
            // 
            // txtReimbursementAmount
            // 
            this.txtReimbursementAmount.Location = new System.Drawing.Point(127, 20);
            this.txtReimbursementAmount.Name = "txtReimbursementAmount";
            this.txtReimbursementAmount.Size = new System.Drawing.Size(174, 21);
            this.txtReimbursementAmount.TabIndex = 88;
            this.txtReimbursementAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPettyCashReimbursement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(933, 369);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmPettyCashReimbursement";
            this.Text = "Petty Cash Reimbursement";
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
            this.grpBody.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
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
        private System.Windows.Forms.CheckBox chkAutoCompleationReimbursementNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPurchaseOrderNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationPettyCash;
        private CustomControls.TextBoxMasterDescription txtPettyCashBookName;
        private System.Windows.Forms.TextBox txtPettyCashBookCode;
        private System.Windows.Forms.Label lblPettyCashBook;
        private System.Windows.Forms.Label lblNetAmount;
        private CustomControls.TextBoxCurrency txtBookBalance;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBankBook;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationCashBook;
        private System.Windows.Forms.Label lblCashBook;
        private System.Windows.Forms.CheckBox chkAutoCompleationBankBook;
        private System.Windows.Forms.RadioButton rdoCheque;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblReimbursementAmount;
        private System.Windows.Forms.TextBox txtReimbursementAmount;
        private System.Windows.Forms.Label lblPayee;
        private System.Windows.Forms.TextBox txtPayeeName;
        private System.Windows.Forms.ComboBox cmbBankBookCode;
        private System.Windows.Forms.ComboBox cmbBankBookName;
        private System.Windows.Forms.ComboBox cmbCashBookCode;
        private System.Windows.Forms.ComboBox cmbCashBookName;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextBoxCurrency txtImprestAmount;
    }
}
