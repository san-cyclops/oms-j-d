namespace UI.Windows
{
    partial class FrmCashier
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.grpSalesPerson = new System.Windows.Forms.GroupBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.tabEmployee = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtEnCode = new System.Windows.Forms.TextBox();
            this.lblEnCode = new System.Windows.Forms.Label();
            this.txtJournalName = new System.Windows.Forms.TextBox();
            this.lblJournalName = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.tbpContactDetails = new System.Windows.Forms.TabPage();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress3 = new System.Windows.Forms.Label();
            this.lblAddress2 = new System.Windows.Forms.Label();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.tbpPasswordGenerate = new System.Windows.Forms.TabPage();
            this.btnGenerate = new Glass.GlassButton();
            this.txtGeneratedPassword = new System.Windows.Forms.TextBox();
            this.tbpCashiers = new System.Windows.Forms.TabPage();
            this.btnExportToExcel = new Glass.GlassButton();
            this.btnCopy = new Glass.GlassButton();
            this.dgvCashierLocation = new System.Windows.Forms.DataGridView();
            this.EmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEmployeeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblEmployeeCode = new System.Windows.Forms.Label();
            this.txtEmployeeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAllLocations = new System.Windows.Forms.CheckBox();
            this.dgvLocationInfo = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCashier = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Access = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.txtFunctionName = new System.Windows.Forms.TextBox();
            this.cmbAccess = new System.Windows.Forms.ComboBox();
            this.txtFunctionDescription = new System.Windows.Forms.TextBox();
            this.txtValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpSalesPerson.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.tbpContactDetails.SuspendLayout();
            this.tbpPasswordGenerate.SuspendLayout();
            this.tbpCashiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashierLocation)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashier)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 479);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(691, 479);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(95, 129);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(419, 36);
            this.txtRemark.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 138);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "Remark";
            // 
            // grpSalesPerson
            // 
            this.grpSalesPerson.Controls.Add(this.cmbGroup);
            this.grpSalesPerson.Controls.Add(this.lblGroup);
            this.grpSalesPerson.Controls.Add(this.chkAutoCompleationEmployee);
            this.grpSalesPerson.Controls.Add(this.tabEmployee);
            this.grpSalesPerson.Controls.Add(this.txtEmployeeCode);
            this.grpSalesPerson.Controls.Add(this.lblEmployeeCode);
            this.grpSalesPerson.Controls.Add(this.txtEmployeeName);
            this.grpSalesPerson.Location = new System.Drawing.Point(389, -5);
            this.grpSalesPerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSalesPerson.Name = "grpSalesPerson";
            this.grpSalesPerson.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSalesPerson.Size = new System.Drawing.Size(541, 271);
            this.grpSalesPerson.TabIndex = 26;
            this.grpSalesPerson.TabStop = false;
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(104, 246);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(429, 21);
            this.cmbGroup.TabIndex = 56;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(4, 249);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(90, 13);
            this.lblGroup.TabIndex = 57;
            this.lblGroup.Text = "Cashier Group";
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(8, 15);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 29;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // tabEmployee
            // 
            this.tabEmployee.Controls.Add(this.tbpGeneral);
            this.tabEmployee.Controls.Add(this.tbpContactDetails);
            this.tabEmployee.Controls.Add(this.tbpPasswordGenerate);
            this.tabEmployee.Controls.Add(this.tbpCashiers);
            this.tabEmployee.Location = new System.Drawing.Point(6, 38);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.SelectedIndex = 0;
            this.tabEmployee.Size = new System.Drawing.Size(529, 205);
            this.tabEmployee.TabIndex = 27;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.lblDesignation);
            this.tbpGeneral.Controls.Add(this.txtRemark);
            this.tbpGeneral.Controls.Add(this.txtDesignation);
            this.tbpGeneral.Controls.Add(this.lblRemark);
            this.tbpGeneral.Controls.Add(this.txtType);
            this.tbpGeneral.Controls.Add(this.lblType);
            this.tbpGeneral.Controls.Add(this.txtEnCode);
            this.tbpGeneral.Controls.Add(this.lblEnCode);
            this.tbpGeneral.Controls.Add(this.txtJournalName);
            this.tbpGeneral.Controls.Add(this.lblJournalName);
            this.tbpGeneral.Controls.Add(this.txtConfirmPassword);
            this.tbpGeneral.Controls.Add(this.txtPassword);
            this.tbpGeneral.Controls.Add(this.lblConfirmPassword);
            this.tbpGeneral.Controls.Add(this.lblPassword);
            this.tbpGeneral.Controls.Add(this.chkIsActive);
            this.tbpGeneral.Controls.Add(this.label34);
            this.tbpGeneral.Controls.Add(this.lblReferenceNo);
            this.tbpGeneral.Controls.Add(this.txtReferenceNo);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Size = new System.Drawing.Size(521, 179);
            this.tbpGeneral.TabIndex = 3;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoSize = true;
            this.lblDesignation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblDesignation.Location = new System.Drawing.Point(363, 30);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(83, 13);
            this.lblDesignation.TabIndex = 49;
            this.lblDesignation.Text = "(Designation)";
            // 
            // txtDesignation
            // 
            this.txtDesignation.Enabled = false;
            this.txtDesignation.Location = new System.Drawing.Point(299, 6);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(215, 21);
            this.txtDesignation.TabIndex = 48;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(95, 102);
            this.txtType.MaxLength = 10;
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(153, 21);
            this.txtType.TabIndex = 47;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 105);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 46;
            this.lblType.Text = "Type";
            // 
            // txtEnCode
            // 
            this.txtEnCode.Location = new System.Drawing.Point(373, 77);
            this.txtEnCode.MaxLength = 10;
            this.txtEnCode.Name = "txtEnCode";
            this.txtEnCode.Size = new System.Drawing.Size(141, 21);
            this.txtEnCode.TabIndex = 45;
            this.txtEnCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnCode_KeyDown);
            // 
            // lblEnCode
            // 
            this.lblEnCode.AutoSize = true;
            this.lblEnCode.Location = new System.Drawing.Point(260, 81);
            this.lblEnCode.Name = "lblEnCode";
            this.lblEnCode.Size = new System.Drawing.Size(51, 13);
            this.lblEnCode.TabIndex = 44;
            this.lblEnCode.Text = "EnCode";
            // 
            // txtJournalName
            // 
            this.txtJournalName.Location = new System.Drawing.Point(95, 78);
            this.txtJournalName.MaxLength = 10;
            this.txtJournalName.Name = "txtJournalName";
            this.txtJournalName.Size = new System.Drawing.Size(153, 21);
            this.txtJournalName.TabIndex = 43;
            this.txtJournalName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJournalName_KeyDown);
            // 
            // lblJournalName
            // 
            this.lblJournalName.AutoSize = true;
            this.lblJournalName.Location = new System.Drawing.Point(6, 81);
            this.lblJournalName.Name = "lblJournalName";
            this.lblJournalName.Size = new System.Drawing.Size(85, 13);
            this.lblJournalName.TabIndex = 42;
            this.lblJournalName.Text = "Journal Name";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(373, 54);
            this.txtConfirmPassword.MaxLength = 6;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(141, 21);
            this.txtConfirmPassword.TabIndex = 41;
            this.txtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPassword_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(95, 54);
            this.txtPassword.MaxLength = 6;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(153, 21);
            this.txtPassword.TabIndex = 40;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(255, 57);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(118, 13);
            this.lblConfirmPassword.TabIndex = 39;
            this.lblConfirmPassword.Text = "Confirm Password*";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 57);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 13);
            this.lblPassword.TabIndex = 38;
            this.lblPassword.Text = "Password*";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Location = new System.Drawing.Point(453, 105);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(61, 17);
            this.chkIsActive.TabIndex = 37;
            this.chkIsActive.Tag = "1";
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(94, 30);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(199, 13);
            this.label34.TabIndex = 36;
            this.label34.Text = "(NIC/ Passport/ Driving License..)";
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 9);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 33;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Enabled = false;
            this.txtReferenceNo.Location = new System.Drawing.Point(95, 6);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(198, 21);
            this.txtReferenceNo.TabIndex = 32;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // tbpContactDetails
            // 
            this.tbpContactDetails.Controls.Add(this.lblMobile);
            this.tbpContactDetails.Controls.Add(this.txtEmail);
            this.tbpContactDetails.Controls.Add(this.txtMobile);
            this.tbpContactDetails.Controls.Add(this.txtTelephone);
            this.tbpContactDetails.Controls.Add(this.txtAddress3);
            this.tbpContactDetails.Controls.Add(this.txtAddress2);
            this.tbpContactDetails.Controls.Add(this.txtAddress1);
            this.tbpContactDetails.Controls.Add(this.lblTelephone);
            this.tbpContactDetails.Controls.Add(this.lblEmail);
            this.tbpContactDetails.Controls.Add(this.lblAddress3);
            this.tbpContactDetails.Controls.Add(this.lblAddress2);
            this.tbpContactDetails.Controls.Add(this.lblAddress1);
            this.tbpContactDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpContactDetails.Name = "tbpContactDetails";
            this.tbpContactDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpContactDetails.Size = new System.Drawing.Size(521, 179);
            this.tbpContactDetails.TabIndex = 0;
            this.tbpContactDetails.Text = "Contact Details";
            this.tbpContactDetails.UseVisualStyleBackColor = true;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(291, 67);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(43, 13);
            this.lblMobile.TabIndex = 14;
            this.lblMobile.Text = "Mobile";
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(362, 13);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(151, 21);
            this.txtEmail.TabIndex = 19;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // txtMobile
            // 
            this.txtMobile.Enabled = false;
            this.txtMobile.Location = new System.Drawing.Point(362, 64);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(151, 21);
            this.txtMobile.TabIndex = 13;
            this.txtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyDown);
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Enabled = false;
            this.txtTelephone.Location = new System.Drawing.Point(362, 38);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(151, 21);
            this.txtTelephone.TabIndex = 17;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
            // 
            // txtAddress3
            // 
            this.txtAddress3.Enabled = false;
            this.txtAddress3.Location = new System.Drawing.Point(84, 64);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(201, 21);
            this.txtAddress3.TabIndex = 16;
            this.txtAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress3_KeyDown);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Enabled = false;
            this.txtAddress2.Location = new System.Drawing.Point(84, 38);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(201, 21);
            this.txtAddress2.TabIndex = 15;
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Enabled = false;
            this.txtAddress1.Location = new System.Drawing.Point(83, 13);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(202, 21);
            this.txtAddress1.TabIndex = 14;
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(291, 41);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblTelephone.TabIndex = 12;
            this.lblTelephone.Text = "Telephone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(291, 16);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 13);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "E-mail";
            // 
            // lblAddress3
            // 
            this.lblAddress3.AutoSize = true;
            this.lblAddress3.Location = new System.Drawing.Point(6, 67);
            this.lblAddress3.Name = "lblAddress3";
            this.lblAddress3.Size = new System.Drawing.Size(64, 13);
            this.lblAddress3.TabIndex = 9;
            this.lblAddress3.Text = "Address 3";
            // 
            // lblAddress2
            // 
            this.lblAddress2.AutoSize = true;
            this.lblAddress2.Location = new System.Drawing.Point(6, 41);
            this.lblAddress2.Name = "lblAddress2";
            this.lblAddress2.Size = new System.Drawing.Size(64, 13);
            this.lblAddress2.TabIndex = 8;
            this.lblAddress2.Text = "Address 2";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(6, 16);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(64, 13);
            this.lblAddress1.TabIndex = 7;
            this.lblAddress1.Text = "Address 1";
            // 
            // tbpPasswordGenerate
            // 
            this.tbpPasswordGenerate.Controls.Add(this.btnGenerate);
            this.tbpPasswordGenerate.Controls.Add(this.txtGeneratedPassword);
            this.tbpPasswordGenerate.Location = new System.Drawing.Point(4, 22);
            this.tbpPasswordGenerate.Name = "tbpPasswordGenerate";
            this.tbpPasswordGenerate.Size = new System.Drawing.Size(521, 179);
            this.tbpPasswordGenerate.TabIndex = 4;
            this.tbpPasswordGenerate.Text = "Password Generate";
            this.tbpPasswordGenerate.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnGenerate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.Black;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(168, 8);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 24);
            this.btnGenerate.TabIndex = 17;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtGeneratedPassword
            // 
            this.txtGeneratedPassword.Location = new System.Drawing.Point(9, 10);
            this.txtGeneratedPassword.Name = "txtGeneratedPassword";
            this.txtGeneratedPassword.Size = new System.Drawing.Size(153, 21);
            this.txtGeneratedPassword.TabIndex = 0;
            // 
            // tbpCashiers
            // 
            this.tbpCashiers.Controls.Add(this.btnExportToExcel);
            this.tbpCashiers.Controls.Add(this.btnCopy);
            this.tbpCashiers.Controls.Add(this.dgvCashierLocation);
            this.tbpCashiers.Location = new System.Drawing.Point(4, 22);
            this.tbpCashiers.Name = "tbpCashiers";
            this.tbpCashiers.Size = new System.Drawing.Size(521, 179);
            this.tbpCashiers.TabIndex = 5;
            this.tbpCashiers.Text = "Cashiers ";
            this.tbpCashiers.UseVisualStyleBackColor = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnExportToExcel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportToExcel.Location = new System.Drawing.Point(406, 151);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(110, 24);
            this.btnExportToExcel.TabIndex = 19;
            this.btnExportToExcel.Text = "&Export To Excel";
            this.btnExportToExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnCopy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.Black;
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(292, 151);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(110, 24);
            this.btnCopy.TabIndex = 18;
            this.btnCopy.Text = "&Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // dgvCashierLocation
            // 
            this.dgvCashierLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashierLocation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeCode,
            this.EmployeeName,
            this.Password,
            this.Designation});
            this.dgvCashierLocation.Location = new System.Drawing.Point(8, 5);
            this.dgvCashierLocation.Name = "dgvCashierLocation";
            this.dgvCashierLocation.RowHeadersWidth = 20;
            this.dgvCashierLocation.Size = new System.Drawing.Size(508, 143);
            this.dgvCashierLocation.TabIndex = 0;
            this.dgvCashierLocation.DoubleClick += new System.EventHandler(this.dgvCashierLocation_DoubleClick);
            // 
            // EmployeeCode
            // 
            this.EmployeeCode.DataPropertyName = "EmployeeCode";
            this.EmployeeCode.HeaderText = "Employee Code";
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.ReadOnly = true;
            this.EmployeeCode.Width = 90;
            // 
            // EmployeeName
            // 
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Width = 130;
            // 
            // Password
            // 
            this.Password.DataPropertyName = "Password";
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            this.Password.Width = 80;
            // 
            // Designation
            // 
            this.Designation.DataPropertyName = "Designation";
            this.Designation.HeaderText = "Group";
            this.Designation.Name = "Designation";
            this.Designation.ReadOnly = true;
            this.Designation.Width = 165;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeCode.IsAutoComplete = false;
            this.txtEmployeeCode.ItemCollection = null;
            this.txtEmployeeCode.Location = new System.Drawing.Point(136, 13);
            this.txtEmployeeCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmployeeCode.MasterCode = "";
            this.txtEmployeeCode.MaxLength = 25;
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(88, 21);
            this.txtEmployeeCode.TabIndex = 11;
            this.txtEmployeeCode.Tag = "1";
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Leave += new System.EventHandler(this.txtEmployeeCode_Leave);
            // 
            // lblEmployeeCode
            // 
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeCode.Location = new System.Drawing.Point(29, 15);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new System.Drawing.Size(97, 13);
            this.lblEmployeeCode.TabIndex = 9;
            this.lblEmployeeCode.Text = "Employee Code";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(226, 13);
            this.txtEmployeeName.MasterDescription = "";
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(307, 21);
            this.txtEmployeeName.TabIndex = 12;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAllLocations);
            this.groupBox2.Controls.Add(this.dgvLocationInfo);
            this.groupBox2.Location = new System.Drawing.Point(2, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 270);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // chkAllLocations
            // 
            this.chkAllLocations.AutoSize = true;
            this.chkAllLocations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllLocations.Location = new System.Drawing.Point(6, 17);
            this.chkAllLocations.Name = "chkAllLocations";
            this.chkAllLocations.Size = new System.Drawing.Size(97, 17);
            this.chkAllLocations.TabIndex = 2;
            this.chkAllLocations.Text = "All Locations";
            this.chkAllLocations.UseVisualStyleBackColor = true;
            this.chkAllLocations.CheckedChanged += new System.EventHandler(this.chkAllLocations_CheckedChanged);
            // 
            // dgvLocationInfo
            // 
            this.dgvLocationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.Location,
            this.LocationId});
            this.dgvLocationInfo.Location = new System.Drawing.Point(4, 39);
            this.dgvLocationInfo.Name = "dgvLocationInfo";
            this.dgvLocationInfo.RowHeadersWidth = 5;
            this.dgvLocationInfo.Size = new System.Drawing.Size(374, 224);
            this.dgvLocationInfo.TabIndex = 2;
            this.dgvLocationInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocationInfo_CellContentClick);
            // 
            // Selection
            // 
            this.Selection.DataPropertyName = "IsSelect";
            this.Selection.FalseValue = "false";
            this.Selection.HeaderText = "Allow";
            this.Selection.Name = "Selection";
            this.Selection.TrueValue = "true";
            this.Selection.Width = 45;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "LocationName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 300;
            // 
            // LocationId
            // 
            this.LocationId.DataPropertyName = "LocationId";
            this.LocationId.HeaderText = "LocationId";
            this.LocationId.Name = "LocationId";
            this.LocationId.Visible = false;
            // 
            // dgvCashier
            // 
            this.dgvCashier.AllowUserToDeleteRows = false;
            this.dgvCashier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.FunctionName,
            this.FunctionDescription,
            this.Access,
            this.Value,
            this.IsValue});
            this.dgvCashier.Location = new System.Drawing.Point(4, 31);
            this.dgvCashier.Name = "dgvCashier";
            this.dgvCashier.Size = new System.Drawing.Size(920, 162);
            this.dgvCashier.TabIndex = 46;
            this.dgvCashier.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCashier_CellContentClick);
            this.dgvCashier.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCashier_CellFormatting);
            this.dgvCashier.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCashier_CellMouseClick);
            this.dgvCashier.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvCashier_RowPrePaint);
            this.dgvCashier.DoubleClick += new System.EventHandler(this.dgvCashier_DoubleClick);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "Order";
            this.RowNo.HeaderText = "Row No";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 50;
            // 
            // FunctionName
            // 
            this.FunctionName.DataPropertyName = "FunctionName";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionName.DefaultCellStyle = dataGridViewCellStyle1;
            this.FunctionName.HeaderText = "FunctionName";
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.ReadOnly = true;
            this.FunctionName.Visible = false;
            this.FunctionName.Width = 255;
            // 
            // FunctionDescription
            // 
            this.FunctionDescription.DataPropertyName = "FunctionDescription";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionDescription.DefaultCellStyle = dataGridViewCellStyle2;
            this.FunctionDescription.HeaderText = "Function Description";
            this.FunctionDescription.Name = "FunctionDescription";
            this.FunctionDescription.ReadOnly = true;
            this.FunctionDescription.Width = 555;
            // 
            // Access
            // 
            this.Access.DataPropertyName = "IsAccess";
            this.Access.HeaderText = "Access";
            this.Access.Name = "Access";
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Value.DefaultCellStyle = dataGridViewCellStyle3;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 150;
            // 
            // IsValue
            // 
            this.IsValue.DataPropertyName = "IsValue";
            this.IsValue.HeaderText = "IsValue";
            this.IsValue.Name = "IsValue";
            this.IsValue.ReadOnly = true;
            this.IsValue.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSelectAll);
            this.groupBox1.Controls.Add(this.dgvCashier);
            this.groupBox1.Controls.Add(this.txtOrderNo);
            this.groupBox1.Controls.Add(this.txtFunctionName);
            this.groupBox1.Controls.Add(this.cmbAccess);
            this.groupBox1.Controls.Add(this.txtFunctionDescription);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Location = new System.Drawing.Point(2, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(930, 222);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(657, 11);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(79, 17);
            this.chkSelectAll.TabIndex = 47;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(4, 196);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(96, 21);
            this.txtOrderNo.TabIndex = 31;
            // 
            // txtFunctionName
            // 
            this.txtFunctionName.Location = new System.Drawing.Point(101, 196);
            this.txtFunctionName.Name = "txtFunctionName";
            this.txtFunctionName.Size = new System.Drawing.Size(249, 21);
            this.txtFunctionName.TabIndex = 48;
            // 
            // cmbAccess
            // 
            this.cmbAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccess.FormattingEnabled = true;
            this.cmbAccess.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbAccess.Location = new System.Drawing.Point(652, 196);
            this.cmbAccess.Name = "cmbAccess";
            this.cmbAccess.Size = new System.Drawing.Size(99, 21);
            this.cmbAccess.TabIndex = 51;
            this.cmbAccess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAccess_KeyDown);
            // 
            // txtFunctionDescription
            // 
            this.txtFunctionDescription.Location = new System.Drawing.Point(351, 196);
            this.txtFunctionDescription.Name = "txtFunctionDescription";
            this.txtFunctionDescription.Size = new System.Drawing.Size(300, 21);
            this.txtFunctionDescription.TabIndex = 49;
            // 
            // txtValue
            // 
            this.txtValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtValue.Location = new System.Drawing.Point(752, 196);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(172, 21);
            this.txtValue.TabIndex = 50;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // FrmCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(933, 528);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpSalesPerson);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCashier";
            this.Text = "Cashier";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpSalesPerson, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpSalesPerson.ResumeLayout(false);
            this.grpSalesPerson.PerformLayout();
            this.tabEmployee.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            this.tbpContactDetails.ResumeLayout(false);
            this.tbpContactDetails.PerformLayout();
            this.tbpPasswordGenerate.ResumeLayout(false);
            this.tbpPasswordGenerate.PerformLayout();
            this.tbpCashiers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashierLocation)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashier)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpSalesPerson;
        private CustomControls.TextBoxMasterDescription txtEmployeeName;
        private CustomControls.TextBoxMasterCode txtEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeCode;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAllLocations;
        private System.Windows.Forms.DataGridView dgvLocationInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationId;
        private System.Windows.Forms.TabControl tabEmployee;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.TabPage tbpContactDetails;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.DataGridView dgvCashier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtFunctionName;
        private System.Windows.Forms.TextBox txtFunctionDescription;
        private CustomControls.TextBoxCurrency txtValue;
        private System.Windows.Forms.ComboBox cmbAccess;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtEnCode;
        private System.Windows.Forms.Label lblEnCode;
        private System.Windows.Forms.TextBox txtJournalName;
        private System.Windows.Forms.Label lblJournalName;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.TextBox txtDesignation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Access;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsValue;
        private System.Windows.Forms.TabPage tbpPasswordGenerate;
        private System.Windows.Forms.TextBox txtGeneratedPassword;
        protected Glass.GlassButton btnGenerate;
        private System.Windows.Forms.TabPage tbpCashiers;
        private System.Windows.Forms.DataGridView dgvCashierLocation;
        protected Glass.GlassButton btnExportToExcel;
        protected Glass.GlassButton btnCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn Designation;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblGroup;
    }
}
