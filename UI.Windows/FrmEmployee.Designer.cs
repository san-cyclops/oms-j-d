namespace UI.Windows
{
    partial class FrmEmployee
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
            this.tabEmployee = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.lblDesig = new System.Windows.Forms.Label();
            this.lblDivisionAndDept = new System.Windows.Forms.Label();
            this.lblDepartmentAndDivision = new System.Windows.Forms.Label();
            this.cmbDesignation = new System.Windows.Forms.ComboBox();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTitle = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
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
            this.tbpFinancial = new System.Windows.Forms.TabPage();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.pbEmployee = new System.Windows.Forms.PictureBox();
            this.grpEmployee = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.txtEmployeeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtEmployeeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblEmployeeCode = new System.Windows.Forms.Label();
            this.lblEmployeeDescription = new System.Windows.Forms.Label();
            this.oDBSalesman = new System.Windows.Forms.OpenFileDialog();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabEmployee.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.tbpContactDetails.SuspendLayout();
            this.tbpFinancial.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).BeginInit();
            this.grpEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 278);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(560, 278);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabEmployee
            // 
            this.tabEmployee.Controls.Add(this.tbpGeneral);
            this.tabEmployee.Controls.Add(this.tbpContactDetails);
            this.tabEmployee.Controls.Add(this.tbpFinancial);
            this.tabEmployee.Location = new System.Drawing.Point(3, 61);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.SelectedIndex = 0;
            this.tabEmployee.Size = new System.Drawing.Size(661, 173);
            this.tabEmployee.TabIndex = 27;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.lblDesig);
            this.tbpGeneral.Controls.Add(this.lblDivisionAndDept);
            this.tbpGeneral.Controls.Add(this.lblDepartmentAndDivision);
            this.tbpGeneral.Controls.Add(this.cmbDesignation);
            this.tbpGeneral.Controls.Add(this.lblDesignation);
            this.tbpGeneral.Controls.Add(this.label1);
            this.tbpGeneral.Controls.Add(this.cmbTitle);
            this.tbpGeneral.Controls.Add(this.label34);
            this.tbpGeneral.Controls.Add(this.cmbGender);
            this.tbpGeneral.Controls.Add(this.lblTitle);
            this.tbpGeneral.Controls.Add(this.lblReferenceNo);
            this.tbpGeneral.Controls.Add(this.txtReferenceNo);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Size = new System.Drawing.Size(653, 147);
            this.tbpGeneral.TabIndex = 3;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // lblDesig
            // 
            this.lblDesig.AutoSize = true;
            this.lblDesig.Location = new System.Drawing.Point(386, 91);
            this.lblDesig.Name = "lblDesig";
            this.lblDesig.Size = new System.Drawing.Size(0, 13);
            this.lblDesig.TabIndex = 45;
            // 
            // lblDivisionAndDept
            // 
            this.lblDivisionAndDept.AutoSize = true;
            this.lblDivisionAndDept.Location = new System.Drawing.Point(137, 113);
            this.lblDivisionAndDept.Name = "lblDivisionAndDept";
            this.lblDivisionAndDept.Size = new System.Drawing.Size(0, 13);
            this.lblDivisionAndDept.TabIndex = 44;
            // 
            // lblDepartmentAndDivision
            // 
            this.lblDepartmentAndDivision.AutoSize = true;
            this.lblDepartmentAndDivision.Location = new System.Drawing.Point(6, 113);
            this.lblDepartmentAndDivision.Name = "lblDepartmentAndDivision";
            this.lblDepartmentAndDivision.Size = new System.Drawing.Size(125, 13);
            this.lblDepartmentAndDivision.TabIndex = 43;
            this.lblDepartmentAndDivision.Text = "Department/Division";
            // 
            // cmbDesignation
            // 
            this.cmbDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesignation.FormattingEnabled = true;
            this.cmbDesignation.Location = new System.Drawing.Point(140, 87);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.Size = new System.Drawing.Size(240, 21);
            this.cmbDesignation.TabIndex = 42;
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoSize = true;
            this.lblDesignation.Location = new System.Drawing.Point(6, 89);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(74, 13);
            this.lblDesignation.TabIndex = 41;
            this.lblDesignation.Text = "Designation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Title*";
            // 
            // cmbTitle
            // 
            this.cmbTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTitle.FormattingEnabled = true;
            this.cmbTitle.Location = new System.Drawing.Point(140, 12);
            this.cmbTitle.Name = "cmbTitle";
            this.cmbTitle.Size = new System.Drawing.Size(100, 21);
            this.cmbTitle.TabIndex = 39;
            this.cmbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTitle_KeyDown);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(386, 65);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(267, 13);
            this.label34.TabIndex = 36;
            this.label34.Text = "(NIC/ Passport/ Driving License/ Business/...)";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(140, 37);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(101, 21);
            this.cmbGender.TabIndex = 35;
            this.cmbGender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGender_KeyDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(5, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(56, 13);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "Gender*";
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(5, 65);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(91, 13);
            this.lblReferenceNo.TabIndex = 33;
            this.lblReferenceNo.Text = "Reference No*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(140, 62);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(240, 21);
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
            this.tbpContactDetails.Size = new System.Drawing.Size(653, 147);
            this.tbpContactDetails.TabIndex = 0;
            this.tbpContactDetails.Text = "Contact Details";
            this.tbpContactDetails.UseVisualStyleBackColor = true;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(323, 67);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(50, 13);
            this.lblMobile.TabIndex = 14;
            this.lblMobile.Text = "Mobile*";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(394, 13);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(253, 21);
            this.txtEmail.TabIndex = 19;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(394, 64);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(107, 21);
            this.txtMobile.TabIndex = 13;
            this.txtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyDown);
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(394, 38);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(253, 21);
            this.txtTelephone.TabIndex = 17;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(113, 64);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(203, 21);
            this.txtAddress3.TabIndex = 16;
            this.txtAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress3_KeyDown);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(113, 38);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(203, 21);
            this.txtAddress2.TabIndex = 15;
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(113, 13);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(204, 21);
            this.txtAddress1.TabIndex = 14;
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(323, 41);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblTelephone.TabIndex = 12;
            this.lblTelephone.Text = "Telephone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(323, 16);
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
            this.lblAddress2.Size = new System.Drawing.Size(71, 13);
            this.lblAddress2.TabIndex = 8;
            this.lblAddress2.Text = "Address 2*";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(6, 16);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(71, 13);
            this.lblAddress1.TabIndex = 7;
            this.lblAddress1.Text = "Address 1*";
            // 
            // tbpFinancial
            // 
            this.tbpFinancial.Controls.Add(this.cmbCostCentre);
            this.tbpFinancial.Controls.Add(this.lblCostCentre);
            this.tbpFinancial.Controls.Add(this.cmbLocation);
            this.tbpFinancial.Controls.Add(this.lblLocation);
            this.tbpFinancial.Location = new System.Drawing.Point(4, 22);
            this.tbpFinancial.Name = "tbpFinancial";
            this.tbpFinancial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFinancial.Size = new System.Drawing.Size(653, 147);
            this.tbpFinancial.TabIndex = 4;
            this.tbpFinancial.Text = "Financial Details";
            this.tbpFinancial.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(101, 40);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(334, 21);
            this.cmbCostCentre.TabIndex = 69;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(9, 45);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(83, 13);
            this.lblCostCentre.TabIndex = 68;
            this.lblCostCentre.Text = "Cost Centre*";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(101, 15);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(334, 21);
            this.cmbLocation.TabIndex = 61;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(9, 18);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location*";
            // 
            // btnClearImage
            // 
            this.btnClearImage.Location = new System.Drawing.Point(735, 211);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(61, 23);
            this.btnClearImage.TabIndex = 31;
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.UseVisualStyleBackColor = true;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(665, 211);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 23);
            this.btnBrowse.TabIndex = 30;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkIsActive);
            this.groupBox3.Controls.Add(this.chkAutoClear);
            this.groupBox3.Controls.Add(this.txtRemark);
            this.groupBox3.Controls.Add(this.lblRemark);
            this.groupBox3.Location = new System.Drawing.Point(3, 229);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(796, 54);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Location = new System.Drawing.Point(706, 14);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(61, 17);
            this.chkIsActive.TabIndex = 41;
            this.chkIsActive.Tag = "1";
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(706, 31);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 30;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(105, 12);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(552, 36);
            this.txtRemark.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(10, 13);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "Remark";
            // 
            // pbEmployee
            // 
            this.pbEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbEmployee.ErrorImage = null;
            this.pbEmployee.Image = global::UI.Windows.Properties.Resources.Default_Salesman;
            this.pbEmployee.Location = new System.Drawing.Point(665, 82);
            this.pbEmployee.Name = "pbEmployee";
            this.pbEmployee.Size = new System.Drawing.Size(131, 126);
            this.pbEmployee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEmployee.TabIndex = 28;
            this.pbEmployee.TabStop = false;
            // 
            // grpEmployee
            // 
            this.grpEmployee.Controls.Add(this.btnNew);
            this.grpEmployee.Controls.Add(this.chkAutoCompleationEmployee);
            this.grpEmployee.Controls.Add(this.txtEmployeeName);
            this.grpEmployee.Controls.Add(this.txtEmployeeCode);
            this.grpEmployee.Controls.Add(this.lblEmployeeCode);
            this.grpEmployee.Controls.Add(this.lblEmployeeDescription);
            this.grpEmployee.Location = new System.Drawing.Point(2, -5);
            this.grpEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpEmployee.Name = "grpEmployee";
            this.grpEmployee.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpEmployee.Size = new System.Drawing.Size(797, 63);
            this.grpEmployee.TabIndex = 26;
            this.grpEmployee.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(748, 35);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 30;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(7, 39);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 29;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(190, 36);
            this.txtEmployeeName.MasterDescription = "";
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(556, 21);
            this.txtEmployeeName.TabIndex = 12;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Leave += new System.EventHandler(this.txtEmployeeName_Leave);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeCode.IsAutoComplete = false;
            this.txtEmployeeCode.ItemCollection = null;
            this.txtEmployeeCode.Location = new System.Drawing.Point(26, 36);
            this.txtEmployeeCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmployeeCode.MasterCode = "";
            this.txtEmployeeCode.MaxLength = 25;
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(162, 21);
            this.txtEmployeeCode.TabIndex = 11;
            this.txtEmployeeCode.Tag = "1";
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Leave += new System.EventHandler(this.txtEmployeeCode_Leave);
            // 
            // lblEmployeeCode
            // 
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeCode.Location = new System.Drawing.Point(24, 21);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new System.Drawing.Size(104, 13);
            this.lblEmployeeCode.TabIndex = 9;
            this.lblEmployeeCode.Text = "Employee Code*";
            // 
            // lblEmployeeDescription
            // 
            this.lblEmployeeDescription.AutoSize = true;
            this.lblEmployeeDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeDescription.Location = new System.Drawing.Point(188, 21);
            this.lblEmployeeDescription.Name = "lblEmployeeDescription";
            this.lblEmployeeDescription.Size = new System.Drawing.Size(107, 13);
            this.lblEmployeeDescription.TabIndex = 10;
            this.lblEmployeeDescription.Text = "Employee Name*";
            // 
            // oDBSalesman
            // 
            this.oDBSalesman.FileName = "oDBSalesman";
            // 
            // FrmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(801, 327);
            this.Controls.Add(this.tabEmployee);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pbEmployee);
            this.Controls.Add(this.grpEmployee);
            this.Name = "FrmEmployee";
            this.Text = "Employee";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpEmployee, 0);
            this.Controls.SetChildIndex(this.pbEmployee, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.btnClearImage, 0);
            this.Controls.SetChildIndex(this.tabEmployee, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabEmployee.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            this.tbpContactDetails.ResumeLayout(false);
            this.tbpContactDetails.PerformLayout();
            this.tbpFinancial.ResumeLayout(false);
            this.tbpFinancial.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).EndInit();
            this.grpEmployee.ResumeLayout(false);
            this.grpEmployee.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabEmployee;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblTitle;
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
        private System.Windows.Forms.TabPage tbpFinancial;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.PictureBox pbEmployee;
        private System.Windows.Forms.GroupBox grpEmployee;
        private CustomControls.TextBoxMasterDescription txtEmployeeName;
        private CustomControls.TextBoxMasterCode txtEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.OpenFileDialog oDBSalesman;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTitle;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.ComboBox cmbDesignation;
        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.Label lblDesig;
        private System.Windows.Forms.Label lblDivisionAndDept;
        private System.Windows.Forms.Label lblDepartmentAndDivision;
    }
}
