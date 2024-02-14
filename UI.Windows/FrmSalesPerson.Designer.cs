namespace UI.Windows
{
    partial class FrmSalesPerson
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
            this.tabSalesPerson = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoCompleationCommSchema = new System.Windows.Forms.CheckBox();
            this.txtCommtionName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCommtionCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.pbSalesPerson = new System.Windows.Forms.PictureBox();
            this.grpSalesPerson = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationSalesPerson = new System.Windows.Forms.CheckBox();
            this.txtSalesPersonName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSalesPersonCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblSalesPersonCode = new System.Windows.Forms.Label();
            this.lblSalesPersonDescription = new System.Windows.Forms.Label();
            this.oDBSalesman = new System.Windows.Forms.OpenFileDialog();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabSalesPerson.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.tbpContactDetails.SuspendLayout();
            this.tbpFinancial.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesPerson)).BeginInit();
            this.grpSalesPerson.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 281);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(560, 281);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabSalesPerson
            // 
            this.tabSalesPerson.Controls.Add(this.tbpGeneral);
            this.tabSalesPerson.Controls.Add(this.tbpContactDetails);
            this.tabSalesPerson.Controls.Add(this.tbpFinancial);
            this.tabSalesPerson.Location = new System.Drawing.Point(3, 62);
            this.tabSalesPerson.Name = "tabSalesPerson";
            this.tabSalesPerson.SelectedIndex = 0;
            this.tabSalesPerson.Size = new System.Drawing.Size(661, 173);
            this.tabSalesPerson.TabIndex = 27;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.cmbType);
            this.tbpGeneral.Controls.Add(this.lblType);
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
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Sales Person",
            "Rep"});
            this.cmbType.Location = new System.Drawing.Point(101, 64);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(240, 21);
            this.cmbType.TabIndex = 38;
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbType_KeyDown);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 67);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(41, 13);
            this.lblType.TabIndex = 37;
            this.lblType.Text = "Type*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(343, 41);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(267, 13);
            this.label34.TabIndex = 36;
            this.label34.Text = "(NIC/ Passport/ Driving License/ Business/...)";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(101, 13);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(101, 21);
            this.cmbGender.TabIndex = 35;
            this.cmbGender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGender_KeyDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(6, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(56, 13);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "Gender*";
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 41);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(91, 13);
            this.lblReferenceNo.TabIndex = 33;
            this.lblReferenceNo.Text = "Reference No*";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(101, 38);
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
            this.tbpFinancial.Controls.Add(this.label1);
            this.tbpFinancial.Controls.Add(this.chkAutoCompleationCommSchema);
            this.tbpFinancial.Controls.Add(this.txtCommtionName);
            this.tbpFinancial.Controls.Add(this.txtCommtionCode);
            this.tbpFinancial.Location = new System.Drawing.Point(4, 22);
            this.tbpFinancial.Name = "tbpFinancial";
            this.tbpFinancial.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFinancial.Size = new System.Drawing.Size(653, 147);
            this.tbpFinancial.TabIndex = 4;
            this.tbpFinancial.Text = "Financial Details";
            this.tbpFinancial.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Commission Schema";
            // 
            // chkAutoCompleationCommSchema
            // 
            this.chkAutoCompleationCommSchema.AutoSize = true;
            this.chkAutoCompleationCommSchema.Checked = true;
            this.chkAutoCompleationCommSchema.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCommSchema.Location = new System.Drawing.Point(149, 16);
            this.chkAutoCompleationCommSchema.Name = "chkAutoCompleationCommSchema";
            this.chkAutoCompleationCommSchema.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCommSchema.TabIndex = 32;
            this.chkAutoCompleationCommSchema.Tag = "1";
            this.chkAutoCompleationCommSchema.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCommSchema.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCommSchema_CheckedChanged);
            // 
            // txtCommtionName
            // 
            this.txtCommtionName.Location = new System.Drawing.Point(332, 13);
            this.txtCommtionName.MasterDescription = "";
            this.txtCommtionName.Name = "txtCommtionName";
            this.txtCommtionName.Size = new System.Drawing.Size(291, 21);
            this.txtCommtionName.TabIndex = 31;
            // 
            // txtCommtionCode
            // 
            this.txtCommtionCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommtionCode.IsAutoComplete = false;
            this.txtCommtionCode.ItemCollection = null;
            this.txtCommtionCode.Location = new System.Drawing.Point(168, 13);
            this.txtCommtionCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCommtionCode.MasterCode = "";
            this.txtCommtionCode.MaxLength = 25;
            this.txtCommtionCode.Name = "txtCommtionCode";
            this.txtCommtionCode.Size = new System.Drawing.Size(162, 21);
            this.txtCommtionCode.TabIndex = 30;
            this.txtCommtionCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommtionCode_KeyDown);
            this.txtCommtionCode.Leave += new System.EventHandler(this.txtCommtionCode_Leave);
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
            this.groupBox3.Controls.Add(this.chkAutoClear);
            this.groupBox3.Controls.Add(this.txtRemark);
            this.groupBox3.Controls.Add(this.lblRemark);
            this.groupBox3.Location = new System.Drawing.Point(3, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(796, 54);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
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
            // pbSalesPerson
            // 
            this.pbSalesPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSalesPerson.ErrorImage = null;
            this.pbSalesPerson.Image = global::UI.Windows.Properties.Resources.Default_Salesman;
            this.pbSalesPerson.Location = new System.Drawing.Point(665, 82);
            this.pbSalesPerson.Name = "pbSalesPerson";
            this.pbSalesPerson.Size = new System.Drawing.Size(131, 126);
            this.pbSalesPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSalesPerson.TabIndex = 28;
            this.pbSalesPerson.TabStop = false;
            // 
            // grpSalesPerson
            // 
            this.grpSalesPerson.Controls.Add(this.btnNew);
            this.grpSalesPerson.Controls.Add(this.chkAutoCompleationSalesPerson);
            this.grpSalesPerson.Controls.Add(this.txtSalesPersonName);
            this.grpSalesPerson.Controls.Add(this.txtSalesPersonCode);
            this.grpSalesPerson.Controls.Add(this.lblSalesPersonCode);
            this.grpSalesPerson.Controls.Add(this.lblSalesPersonDescription);
            this.grpSalesPerson.Location = new System.Drawing.Point(2, -5);
            this.grpSalesPerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSalesPerson.Name = "grpSalesPerson";
            this.grpSalesPerson.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSalesPerson.Size = new System.Drawing.Size(797, 63);
            this.grpSalesPerson.TabIndex = 26;
            this.grpSalesPerson.TabStop = false;
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
            // chkAutoCompleationSalesPerson
            // 
            this.chkAutoCompleationSalesPerson.AutoSize = true;
            this.chkAutoCompleationSalesPerson.Checked = true;
            this.chkAutoCompleationSalesPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(7, 39);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 29;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonName
            // 
            this.txtSalesPersonName.Location = new System.Drawing.Point(190, 36);
            this.txtSalesPersonName.MasterDescription = "";
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(556, 21);
            this.txtSalesPersonName.TabIndex = 12;
            this.txtSalesPersonName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonName_KeyDown);
            this.txtSalesPersonName.Leave += new System.EventHandler(this.txtSalesPersonName_Leave);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesPersonCode.IsAutoComplete = false;
            this.txtSalesPersonCode.ItemCollection = null;
            this.txtSalesPersonCode.Location = new System.Drawing.Point(26, 36);
            this.txtSalesPersonCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSalesPersonCode.MasterCode = "";
            this.txtSalesPersonCode.MaxLength = 25;
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(162, 21);
            this.txtSalesPersonCode.TabIndex = 11;
            this.txtSalesPersonCode.Tag = "1";
            this.txtSalesPersonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonCode_KeyDown);
            this.txtSalesPersonCode.Leave += new System.EventHandler(this.txtSalesPersonCode_Leave);
            // 
            // lblSalesPersonCode
            // 
            this.lblSalesPersonCode.AutoSize = true;
            this.lblSalesPersonCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesPersonCode.Location = new System.Drawing.Point(24, 21);
            this.lblSalesPersonCode.Name = "lblSalesPersonCode";
            this.lblSalesPersonCode.Size = new System.Drawing.Size(122, 13);
            this.lblSalesPersonCode.TabIndex = 9;
            this.lblSalesPersonCode.Text = "Sales Person Code*";
            // 
            // lblSalesPersonDescription
            // 
            this.lblSalesPersonDescription.AutoSize = true;
            this.lblSalesPersonDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesPersonDescription.Location = new System.Drawing.Point(188, 21);
            this.lblSalesPersonDescription.Name = "lblSalesPersonDescription";
            this.lblSalesPersonDescription.Size = new System.Drawing.Size(125, 13);
            this.lblSalesPersonDescription.TabIndex = 10;
            this.lblSalesPersonDescription.Text = "Sales Person Name*";
            // 
            // oDBSalesman
            // 
            this.oDBSalesman.FileName = "oDBSalesman";
            // 
            // FrmSalesPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(801, 330);
            this.Controls.Add(this.tabSalesPerson);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pbSalesPerson);
            this.Controls.Add(this.grpSalesPerson);
            this.Name = "FrmSalesPerson";
            this.Text = "Sales Person / Representative";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpSalesPerson, 0);
            this.Controls.SetChildIndex(this.pbSalesPerson, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.btnClearImage, 0);
            this.Controls.SetChildIndex(this.tabSalesPerson, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabSalesPerson.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpGeneral.PerformLayout();
            this.tbpContactDetails.ResumeLayout(false);
            this.tbpContactDetails.PerformLayout();
            this.tbpFinancial.ResumeLayout(false);
            this.tbpFinancial.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesPerson)).EndInit();
            this.grpSalesPerson.ResumeLayout(false);
            this.grpSalesPerson.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSalesPerson;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
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
        private System.Windows.Forms.PictureBox pbSalesPerson;
        private System.Windows.Forms.GroupBox grpSalesPerson;
        private CustomControls.TextBoxMasterDescription txtSalesPersonName;
        private CustomControls.TextBoxMasterCode txtSalesPersonCode;
        private System.Windows.Forms.Label lblSalesPersonCode;
        private System.Windows.Forms.Label lblSalesPersonDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAutoCompleationCommSchema;
        private CustomControls.TextBoxMasterDescription txtCommtionName;
        private CustomControls.TextBoxMasterCode txtCommtionCode;
        private System.Windows.Forms.OpenFileDialog oDBSalesman;
    }
}
