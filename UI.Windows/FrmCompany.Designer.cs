namespace UI.Windows
{
    partial class FrmCompany
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
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleteCompanyCode = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtCompanyName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCompanyCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblCompanyCode = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.tabCompany = new System.Windows.Forms.TabControl();
            this.tbpGeneralDetails = new System.Windows.Forms.TabPage();
            this.txtGroupOfCompanyName = new System.Windows.Forms.TextBox();
            this.lblGroupOfCompanyName = new System.Windows.Forms.Label();
            this.txtGroupOfCompanyCode = new System.Windows.Forms.TextBox();
            this.lblGroupOfCompanyCode = new System.Windows.Forms.Label();
            this.txtOtherBusinessName3 = new System.Windows.Forms.TextBox();
            this.txtOtherBusinessName2 = new System.Windows.Forms.TextBox();
            this.txtOtherBusinessName1 = new System.Windows.Forms.TextBox();
            this.lblOtherBusiness3 = new System.Windows.Forms.Label();
            this.lblOtherBusiness2 = new System.Windows.Forms.Label();
            this.lblOtherBusiness1 = new System.Windows.Forms.Label();
            this.tbpContactDetails = new System.Windows.Forms.TabPage();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.lblWeb = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress3 = new System.Windows.Forms.Label();
            this.lblAddress2 = new System.Windows.Forms.Label();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.tbpTaxDetails = new System.Windows.Forms.TabPage();
            this.txtTax5No = new System.Windows.Forms.TextBox();
            this.txtTax4No = new System.Windows.Forms.TextBox();
            this.txtTax3No = new System.Windows.Forms.TextBox();
            this.txtTax2No = new System.Windows.Forms.TextBox();
            this.txtTax1No = new System.Windows.Forms.TextBox();
            this.lblTax5 = new System.Windows.Forms.Label();
            this.lblTax4 = new System.Windows.Forms.Label();
            this.lblTax3 = new System.Windows.Forms.Label();
            this.lblTax2 = new System.Windows.Forms.Label();
            this.lblTax1 = new System.Windows.Forms.Label();
            this.chkTax5 = new System.Windows.Forms.CheckBox();
            this.chkTax4 = new System.Windows.Forms.CheckBox();
            this.chkTax3 = new System.Windows.Forms.CheckBox();
            this.chkTax2 = new System.Windows.Forms.CheckBox();
            this.chkTax1 = new System.Windows.Forms.CheckBox();
            this.tbpOtherDetails = new System.Windows.Forms.TabPage();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.lblFinancialYear = new System.Windows.Forms.Label();
            this.cmbCostingMethod = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.lblCostingMethod = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpCompany.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabCompany.SuspendLayout();
            this.tbpGeneralDetails.SuspendLayout();
            this.tbpContactDetails.SuspendLayout();
            this.tbpTaxDetails.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 303);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(428, 303);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.chkAutoCompleteCompanyCode);
            this.grpCompany.Controls.Add(this.btnNew);
            this.grpCompany.Controls.Add(this.txtCompanyName);
            this.grpCompany.Controls.Add(this.txtCompanyCode);
            this.grpCompany.Controls.Add(this.lblCompanyCode);
            this.grpCompany.Controls.Add(this.lblCompanyName);
            this.grpCompany.Location = new System.Drawing.Point(2, -5);
            this.grpCompany.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCompany.Size = new System.Drawing.Size(665, 70);
            this.grpCompany.TabIndex = 12;
            this.grpCompany.TabStop = false;
            // 
            // chkAutoCompleteCompanyCode
            // 
            this.chkAutoCompleteCompanyCode.AutoSize = true;
            this.chkAutoCompleteCompanyCode.Checked = true;
            this.chkAutoCompleteCompanyCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleteCompanyCode.Location = new System.Drawing.Point(6, 39);
            this.chkAutoCompleteCompanyCode.Name = "chkAutoCompleteCompanyCode";
            this.chkAutoCompleteCompanyCode.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleteCompanyCode.TabIndex = 31;
            this.chkAutoCompleteCompanyCode.Tag = "1";
            this.chkAutoCompleteCompanyCode.UseVisualStyleBackColor = true;
            this.chkAutoCompleteCompanyCode.CheckedChanged += new System.EventHandler(this.chkAutoCompleteCompanyCode_CheckedChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(613, 35);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(185, 36);
            this.txtCompanyName.MasterDescription = "";
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(426, 21);
            this.txtCompanyName.TabIndex = 2;
            this.txtCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyName_KeyDown);
            this.txtCompanyName.Leave += new System.EventHandler(this.txtCompanyName_Leave);
            // 
            // txtCompanyCode
            // 
            this.txtCompanyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyCode.IsAutoComplete = false;
            this.txtCompanyCode.ItemCollection = null;
            this.txtCompanyCode.Location = new System.Drawing.Point(24, 36);
            this.txtCompanyCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCompanyCode.MasterCode = "";
            this.txtCompanyCode.MaxLength = 25;
            this.txtCompanyCode.Name = "txtCompanyCode";
            this.txtCompanyCode.Size = new System.Drawing.Size(160, 21);
            this.txtCompanyCode.TabIndex = 0;
            this.txtCompanyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyCode_KeyDown);
            this.txtCompanyCode.Leave += new System.EventHandler(this.txtCompanyCode_Leave);
            // 
            // lblCompanyCode
            // 
            this.lblCompanyCode.AutoSize = true;
            this.lblCompanyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyCode.Location = new System.Drawing.Point(21, 19);
            this.lblCompanyCode.Name = "lblCompanyCode";
            this.lblCompanyCode.Size = new System.Drawing.Size(103, 13);
            this.lblCompanyCode.TabIndex = 9;
            this.lblCompanyCode.Text = "Company Code*";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(182, 19);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(106, 13);
            this.lblCompanyName.TabIndex = 10;
            this.lblCompanyName.Text = "Company Name*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.tabCompany);
            this.groupBox1.Location = new System.Drawing.Point(2, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 248);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(576, 225);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 31;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // tabCompany
            // 
            this.tabCompany.Controls.Add(this.tbpGeneralDetails);
            this.tabCompany.Controls.Add(this.tbpContactDetails);
            this.tabCompany.Controls.Add(this.tbpTaxDetails);
            this.tabCompany.Controls.Add(this.tbpOtherDetails);
            this.tabCompany.Location = new System.Drawing.Point(4, 11);
            this.tabCompany.Name = "tabCompany";
            this.tabCompany.SelectedIndex = 0;
            this.tabCompany.Size = new System.Drawing.Size(659, 208);
            this.tabCompany.TabIndex = 9;
            // 
            // tbpGeneralDetails
            // 
            this.tbpGeneralDetails.Controls.Add(this.txtGroupOfCompanyName);
            this.tbpGeneralDetails.Controls.Add(this.lblGroupOfCompanyName);
            this.tbpGeneralDetails.Controls.Add(this.txtGroupOfCompanyCode);
            this.tbpGeneralDetails.Controls.Add(this.lblGroupOfCompanyCode);
            this.tbpGeneralDetails.Controls.Add(this.txtOtherBusinessName3);
            this.tbpGeneralDetails.Controls.Add(this.txtOtherBusinessName2);
            this.tbpGeneralDetails.Controls.Add(this.txtOtherBusinessName1);
            this.tbpGeneralDetails.Controls.Add(this.lblOtherBusiness3);
            this.tbpGeneralDetails.Controls.Add(this.lblOtherBusiness2);
            this.tbpGeneralDetails.Controls.Add(this.lblOtherBusiness1);
            this.tbpGeneralDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneralDetails.Name = "tbpGeneralDetails";
            this.tbpGeneralDetails.Size = new System.Drawing.Size(651, 182);
            this.tbpGeneralDetails.TabIndex = 3;
            this.tbpGeneralDetails.Text = "Genaral Details";
            this.tbpGeneralDetails.UseVisualStyleBackColor = true;
            // 
            // txtGroupOfCompanyName
            // 
            this.txtGroupOfCompanyName.Enabled = false;
            this.txtGroupOfCompanyName.Location = new System.Drawing.Point(396, 13);
            this.txtGroupOfCompanyName.Name = "txtGroupOfCompanyName";
            this.txtGroupOfCompanyName.Size = new System.Drawing.Size(241, 21);
            this.txtGroupOfCompanyName.TabIndex = 5;
            this.txtGroupOfCompanyName.Tag = "1";
            // 
            // lblGroupOfCompanyName
            // 
            this.lblGroupOfCompanyName.AutoSize = true;
            this.lblGroupOfCompanyName.Location = new System.Drawing.Point(233, 16);
            this.lblGroupOfCompanyName.Name = "lblGroupOfCompanyName";
            this.lblGroupOfCompanyName.Size = new System.Drawing.Size(160, 13);
            this.lblGroupOfCompanyName.TabIndex = 56;
            this.lblGroupOfCompanyName.Text = "Group of Company Name*";
            // 
            // txtGroupOfCompanyCode
            // 
            this.txtGroupOfCompanyCode.Enabled = false;
            this.txtGroupOfCompanyCode.Location = new System.Drawing.Point(171, 13);
            this.txtGroupOfCompanyCode.Name = "txtGroupOfCompanyCode";
            this.txtGroupOfCompanyCode.Size = new System.Drawing.Size(56, 21);
            this.txtGroupOfCompanyCode.TabIndex = 4;
            this.txtGroupOfCompanyCode.Tag = "1";
            // 
            // lblGroupOfCompanyCode
            // 
            this.lblGroupOfCompanyCode.AutoSize = true;
            this.lblGroupOfCompanyCode.Location = new System.Drawing.Point(6, 16);
            this.lblGroupOfCompanyCode.Name = "lblGroupOfCompanyCode";
            this.lblGroupOfCompanyCode.Size = new System.Drawing.Size(157, 13);
            this.lblGroupOfCompanyCode.TabIndex = 54;
            this.lblGroupOfCompanyCode.Text = "Group of Company Code*";
            // 
            // txtOtherBusinessName3
            // 
            this.txtOtherBusinessName3.Location = new System.Drawing.Point(171, 94);
            this.txtOtherBusinessName3.Name = "txtOtherBusinessName3";
            this.txtOtherBusinessName3.Size = new System.Drawing.Size(466, 21);
            this.txtOtherBusinessName3.TabIndex = 8;
            this.txtOtherBusinessName3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherBusinessName3_KeyDown);
            this.txtOtherBusinessName3.Leave += new System.EventHandler(this.txtOtherBusinessName3_Leave);
            // 
            // txtOtherBusinessName2
            // 
            this.txtOtherBusinessName2.Location = new System.Drawing.Point(171, 67);
            this.txtOtherBusinessName2.Name = "txtOtherBusinessName2";
            this.txtOtherBusinessName2.Size = new System.Drawing.Size(466, 21);
            this.txtOtherBusinessName2.TabIndex = 7;
            this.txtOtherBusinessName2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherBusinessName2_KeyDown);
            this.txtOtherBusinessName2.Leave += new System.EventHandler(this.txtOtherBusinessName2_Leave);
            // 
            // txtOtherBusinessName1
            // 
            this.txtOtherBusinessName1.Location = new System.Drawing.Point(171, 40);
            this.txtOtherBusinessName1.Name = "txtOtherBusinessName1";
            this.txtOtherBusinessName1.Size = new System.Drawing.Size(466, 21);
            this.txtOtherBusinessName1.TabIndex = 6;
            this.txtOtherBusinessName1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherBusinessName1_KeyDown);
            this.txtOtherBusinessName1.Leave += new System.EventHandler(this.txtOtherBusinessName1_Leave);
            // 
            // lblOtherBusiness3
            // 
            this.lblOtherBusiness3.AutoSize = true;
            this.lblOtherBusiness3.Location = new System.Drawing.Point(6, 97);
            this.lblOtherBusiness3.Name = "lblOtherBusiness3";
            this.lblOtherBusiness3.Size = new System.Drawing.Size(141, 13);
            this.lblOtherBusiness3.TabIndex = 50;
            this.lblOtherBusiness3.Text = "Other Business Name 3";
            // 
            // lblOtherBusiness2
            // 
            this.lblOtherBusiness2.AutoSize = true;
            this.lblOtherBusiness2.Location = new System.Drawing.Point(6, 70);
            this.lblOtherBusiness2.Name = "lblOtherBusiness2";
            this.lblOtherBusiness2.Size = new System.Drawing.Size(141, 13);
            this.lblOtherBusiness2.TabIndex = 49;
            this.lblOtherBusiness2.Text = "Other Business Name 2";
            // 
            // lblOtherBusiness1
            // 
            this.lblOtherBusiness1.AutoSize = true;
            this.lblOtherBusiness1.Location = new System.Drawing.Point(6, 43);
            this.lblOtherBusiness1.Name = "lblOtherBusiness1";
            this.lblOtherBusiness1.Size = new System.Drawing.Size(141, 13);
            this.lblOtherBusiness1.TabIndex = 48;
            this.lblOtherBusiness1.Text = "Other Business Name 1";
            // 
            // tbpContactDetails
            // 
            this.tbpContactDetails.Controls.Add(this.txtWeb);
            this.tbpContactDetails.Controls.Add(this.lblWeb);
            this.tbpContactDetails.Controls.Add(this.txtContactPerson);
            this.tbpContactDetails.Controls.Add(this.lblContactPerson);
            this.tbpContactDetails.Controls.Add(this.lblMobile);
            this.tbpContactDetails.Controls.Add(this.txtEmail);
            this.tbpContactDetails.Controls.Add(this.txtMobile);
            this.tbpContactDetails.Controls.Add(this.txtFax);
            this.tbpContactDetails.Controls.Add(this.txtTelephone);
            this.tbpContactDetails.Controls.Add(this.txtAddress3);
            this.tbpContactDetails.Controls.Add(this.txtAddress2);
            this.tbpContactDetails.Controls.Add(this.txtAddress1);
            this.tbpContactDetails.Controls.Add(this.lblFax);
            this.tbpContactDetails.Controls.Add(this.lblTelephone);
            this.tbpContactDetails.Controls.Add(this.lblEmail);
            this.tbpContactDetails.Controls.Add(this.lblAddress3);
            this.tbpContactDetails.Controls.Add(this.lblAddress2);
            this.tbpContactDetails.Controls.Add(this.lblAddress1);
            this.tbpContactDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpContactDetails.Name = "tbpContactDetails";
            this.tbpContactDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpContactDetails.Size = new System.Drawing.Size(651, 182);
            this.tbpContactDetails.TabIndex = 0;
            this.tbpContactDetails.Text = "Contact Details";
            this.tbpContactDetails.UseVisualStyleBackColor = true;
            // 
            // txtWeb
            // 
            this.txtWeb.Location = new System.Drawing.Point(386, 92);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(251, 21);
            this.txtWeb.TabIndex = 39;
            this.txtWeb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWeb_KeyDown);
            this.txtWeb.Leave += new System.EventHandler(this.txtWeb_Leave);
            // 
            // lblWeb
            // 
            this.lblWeb.AutoSize = true;
            this.lblWeb.Location = new System.Drawing.Point(300, 95);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(31, 13);
            this.lblWeb.TabIndex = 38;
            this.lblWeb.Text = "Web";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(106, 131);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(531, 21);
            this.txtContactPerson.TabIndex = 37;
            this.txtContactPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactPerson_KeyDown);
            this.txtContactPerson.Leave += new System.EventHandler(this.txtContactPerson_Leave);
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(6, 134);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(94, 13);
            this.lblContactPerson.TabIndex = 36;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(300, 43);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(43, 13);
            this.lblMobile.TabIndex = 29;
            this.lblMobile.Text = "Mobile";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(386, 66);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(251, 21);
            this.txtEmail.TabIndex = 35;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(385, 40);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(107, 21);
            this.txtMobile.TabIndex = 14;
            this.txtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyDown);
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            this.txtMobile.Leave += new System.EventHandler(this.txtMobile_Leave);
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(530, 40);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(107, 21);
            this.txtFax.TabIndex = 15;
            this.txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFax_KeyDown);
            this.txtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFax_KeyPress);
            this.txtFax.Leave += new System.EventHandler(this.txtFax_Leave);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(386, 15);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(251, 21);
            this.txtTelephone.TabIndex = 13;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
            this.txtTelephone.Leave += new System.EventHandler(this.txtTelephone_Leave);
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(91, 66);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(204, 21);
            this.txtAddress3.TabIndex = 12;
            this.txtAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress3_KeyDown);
            this.txtAddress3.Leave += new System.EventHandler(this.txtAddress3_Leave);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(91, 40);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(204, 21);
            this.txtAddress2.TabIndex = 11;
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            this.txtAddress2.Leave += new System.EventHandler(this.txtAddress2_Leave);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(91, 15);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(204, 21);
            this.txtAddress1.TabIndex = 10;
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            this.txtAddress1.Leave += new System.EventHandler(this.txtAddress1_Leave);
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.Location = new System.Drawing.Point(498, 43);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(26, 13);
            this.lblFax.TabIndex = 28;
            this.lblFax.Text = "Fax";
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(300, 18);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblTelephone.TabIndex = 26;
            this.lblTelephone.Text = "Telephone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(300, 69);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 13);
            this.lblEmail.TabIndex = 25;
            this.lblEmail.Text = "E-mail";
            // 
            // lblAddress3
            // 
            this.lblAddress3.AutoSize = true;
            this.lblAddress3.Location = new System.Drawing.Point(6, 69);
            this.lblAddress3.Name = "lblAddress3";
            this.lblAddress3.Size = new System.Drawing.Size(64, 13);
            this.lblAddress3.TabIndex = 24;
            this.lblAddress3.Text = "Address 3";
            // 
            // lblAddress2
            // 
            this.lblAddress2.AutoSize = true;
            this.lblAddress2.Location = new System.Drawing.Point(6, 43);
            this.lblAddress2.Name = "lblAddress2";
            this.lblAddress2.Size = new System.Drawing.Size(64, 13);
            this.lblAddress2.TabIndex = 23;
            this.lblAddress2.Text = "Address 2";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(6, 18);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(64, 13);
            this.lblAddress1.TabIndex = 22;
            this.lblAddress1.Text = "Address 1";
            // 
            // tbpTaxDetails
            // 
            this.tbpTaxDetails.Controls.Add(this.txtTax5No);
            this.tbpTaxDetails.Controls.Add(this.txtTax4No);
            this.tbpTaxDetails.Controls.Add(this.txtTax3No);
            this.tbpTaxDetails.Controls.Add(this.txtTax2No);
            this.tbpTaxDetails.Controls.Add(this.txtTax1No);
            this.tbpTaxDetails.Controls.Add(this.lblTax5);
            this.tbpTaxDetails.Controls.Add(this.lblTax4);
            this.tbpTaxDetails.Controls.Add(this.lblTax3);
            this.tbpTaxDetails.Controls.Add(this.lblTax2);
            this.tbpTaxDetails.Controls.Add(this.lblTax1);
            this.tbpTaxDetails.Controls.Add(this.chkTax5);
            this.tbpTaxDetails.Controls.Add(this.chkTax4);
            this.tbpTaxDetails.Controls.Add(this.chkTax3);
            this.tbpTaxDetails.Controls.Add(this.chkTax2);
            this.tbpTaxDetails.Controls.Add(this.chkTax1);
            this.tbpTaxDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpTaxDetails.Name = "tbpTaxDetails";
            this.tbpTaxDetails.Size = new System.Drawing.Size(651, 182);
            this.tbpTaxDetails.TabIndex = 2;
            this.tbpTaxDetails.Text = "Tax Details";
            this.tbpTaxDetails.UseVisualStyleBackColor = true;
            // 
            // txtTax5No
            // 
            this.txtTax5No.Location = new System.Drawing.Point(143, 121);
            this.txtTax5No.Name = "txtTax5No";
            this.txtTax5No.Size = new System.Drawing.Size(264, 21);
            this.txtTax5No.TabIndex = 71;
            this.txtTax5No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax5No_KeyDown);
            this.txtTax5No.Leave += new System.EventHandler(this.txtTax5No_Leave);
            // 
            // txtTax4No
            // 
            this.txtTax4No.Location = new System.Drawing.Point(143, 94);
            this.txtTax4No.Name = "txtTax4No";
            this.txtTax4No.Size = new System.Drawing.Size(264, 21);
            this.txtTax4No.TabIndex = 70;
            this.txtTax4No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax4No_KeyDown);
            this.txtTax4No.Leave += new System.EventHandler(this.txtTax4No_Leave);
            // 
            // txtTax3No
            // 
            this.txtTax3No.Location = new System.Drawing.Point(143, 67);
            this.txtTax3No.Name = "txtTax3No";
            this.txtTax3No.Size = new System.Drawing.Size(264, 21);
            this.txtTax3No.TabIndex = 69;
            this.txtTax3No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax3No_KeyDown);
            this.txtTax3No.Leave += new System.EventHandler(this.txtTax3No_Leave);
            // 
            // txtTax2No
            // 
            this.txtTax2No.Location = new System.Drawing.Point(143, 40);
            this.txtTax2No.Name = "txtTax2No";
            this.txtTax2No.Size = new System.Drawing.Size(264, 21);
            this.txtTax2No.TabIndex = 68;
            this.txtTax2No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax2No_KeyDown);
            this.txtTax2No.Leave += new System.EventHandler(this.txtTax2No_Leave);
            // 
            // txtTax1No
            // 
            this.txtTax1No.Location = new System.Drawing.Point(143, 13);
            this.txtTax1No.Name = "txtTax1No";
            this.txtTax1No.Size = new System.Drawing.Size(264, 21);
            this.txtTax1No.TabIndex = 67;
            this.txtTax1No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax1No_KeyDown);
            this.txtTax1No.Leave += new System.EventHandler(this.txtTax1No_Leave);
            // 
            // lblTax5
            // 
            this.lblTax5.AutoSize = true;
            this.lblTax5.Location = new System.Drawing.Point(6, 124);
            this.lblTax5.Name = "lblTax5";
            this.lblTax5.Size = new System.Drawing.Size(38, 13);
            this.lblTax5.TabIndex = 66;
            this.lblTax5.Text = "Tax 5";
            // 
            // lblTax4
            // 
            this.lblTax4.AutoSize = true;
            this.lblTax4.Location = new System.Drawing.Point(6, 97);
            this.lblTax4.Name = "lblTax4";
            this.lblTax4.Size = new System.Drawing.Size(38, 13);
            this.lblTax4.TabIndex = 65;
            this.lblTax4.Text = "Tax 4";
            // 
            // lblTax3
            // 
            this.lblTax3.AutoSize = true;
            this.lblTax3.Location = new System.Drawing.Point(6, 70);
            this.lblTax3.Name = "lblTax3";
            this.lblTax3.Size = new System.Drawing.Size(38, 13);
            this.lblTax3.TabIndex = 64;
            this.lblTax3.Text = "Tax 3";
            // 
            // lblTax2
            // 
            this.lblTax2.AutoSize = true;
            this.lblTax2.Location = new System.Drawing.Point(6, 44);
            this.lblTax2.Name = "lblTax2";
            this.lblTax2.Size = new System.Drawing.Size(38, 13);
            this.lblTax2.TabIndex = 63;
            this.lblTax2.Text = "Tax 2";
            // 
            // lblTax1
            // 
            this.lblTax1.AutoSize = true;
            this.lblTax1.Location = new System.Drawing.Point(6, 16);
            this.lblTax1.Name = "lblTax1";
            this.lblTax1.Size = new System.Drawing.Size(38, 13);
            this.lblTax1.TabIndex = 62;
            this.lblTax1.Text = "Tax 1";
            // 
            // chkTax5
            // 
            this.chkTax5.AutoSize = true;
            this.chkTax5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax5.Location = new System.Drawing.Point(122, 124);
            this.chkTax5.Name = "chkTax5";
            this.chkTax5.Size = new System.Drawing.Size(15, 14);
            this.chkTax5.TabIndex = 61;
            this.chkTax5.UseVisualStyleBackColor = true;
            this.chkTax5.CheckedChanged += new System.EventHandler(this.chkTax5_CheckedChanged);
            // 
            // chkTax4
            // 
            this.chkTax4.AutoSize = true;
            this.chkTax4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax4.Location = new System.Drawing.Point(122, 97);
            this.chkTax4.Name = "chkTax4";
            this.chkTax4.Size = new System.Drawing.Size(15, 14);
            this.chkTax4.TabIndex = 60;
            this.chkTax4.UseVisualStyleBackColor = true;
            this.chkTax4.CheckedChanged += new System.EventHandler(this.chkTax4_CheckedChanged);
            // 
            // chkTax3
            // 
            this.chkTax3.AutoSize = true;
            this.chkTax3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax3.Location = new System.Drawing.Point(122, 70);
            this.chkTax3.Name = "chkTax3";
            this.chkTax3.Size = new System.Drawing.Size(15, 14);
            this.chkTax3.TabIndex = 59;
            this.chkTax3.UseVisualStyleBackColor = true;
            this.chkTax3.CheckedChanged += new System.EventHandler(this.chkTax3_CheckedChanged);
            // 
            // chkTax2
            // 
            this.chkTax2.AutoSize = true;
            this.chkTax2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax2.Location = new System.Drawing.Point(122, 43);
            this.chkTax2.Name = "chkTax2";
            this.chkTax2.Size = new System.Drawing.Size(15, 14);
            this.chkTax2.TabIndex = 58;
            this.chkTax2.UseVisualStyleBackColor = true;
            this.chkTax2.CheckedChanged += new System.EventHandler(this.chkTax2_CheckedChanged);
            // 
            // chkTax1
            // 
            this.chkTax1.AutoSize = true;
            this.chkTax1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax1.Location = new System.Drawing.Point(122, 16);
            this.chkTax1.Name = "chkTax1";
            this.chkTax1.Size = new System.Drawing.Size(15, 14);
            this.chkTax1.TabIndex = 57;
            this.chkTax1.UseVisualStyleBackColor = true;
            this.chkTax1.CheckedChanged += new System.EventHandler(this.chkTax1_CheckedChanged);
            // 
            // tbpOtherDetails
            // 
            this.tbpOtherDetails.Controls.Add(this.cmbCostCentre);
            this.tbpOtherDetails.Controls.Add(this.cmbFinancialYear);
            this.tbpOtherDetails.Controls.Add(this.lblFinancialYear);
            this.tbpOtherDetails.Controls.Add(this.cmbCostingMethod);
            this.tbpOtherDetails.Controls.Add(this.lblCostCentre);
            this.tbpOtherDetails.Controls.Add(this.lblCostingMethod);
            this.tbpOtherDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpOtherDetails.Name = "tbpOtherDetails";
            this.tbpOtherDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOtherDetails.Size = new System.Drawing.Size(651, 182);
            this.tbpOtherDetails.TabIndex = 1;
            this.tbpOtherDetails.Text = "Other Details";
            this.tbpOtherDetails.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(165, 13);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(247, 21);
            this.cmbCostCentre.TabIndex = 45;
            this.cmbCostCentre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostCentre_KeyDown);
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(165, 62);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(247, 21);
            this.cmbFinancialYear.TabIndex = 44;
            this.cmbFinancialYear.SelectedValueChanged += new System.EventHandler(this.cmbFinancialYear_SelectedValueChanged);
            this.cmbFinancialYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFinancialYear_KeyDown);
            this.cmbFinancialYear.Leave += new System.EventHandler(this.cmbFinancialYear_Leave);
            // 
            // lblFinancialYear
            // 
            this.lblFinancialYear.AutoSize = true;
            this.lblFinancialYear.Location = new System.Drawing.Point(7, 65);
            this.lblFinancialYear.Name = "lblFinancialYear";
            this.lblFinancialYear.Size = new System.Drawing.Size(139, 13);
            this.lblFinancialYear.TabIndex = 43;
            this.lblFinancialYear.Text = "Start of Financial Year*";
            // 
            // cmbCostingMethod
            // 
            this.cmbCostingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostingMethod.FormattingEnabled = true;
            this.cmbCostingMethod.Location = new System.Drawing.Point(165, 38);
            this.cmbCostingMethod.Name = "cmbCostingMethod";
            this.cmbCostingMethod.Size = new System.Drawing.Size(247, 21);
            this.cmbCostingMethod.TabIndex = 41;
            this.cmbCostingMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCostingMethod_KeyDown);
            this.cmbCostingMethod.Leave += new System.EventHandler(this.cmbCostingMethod_Leave);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(6, 16);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(83, 13);
            this.lblCostCentre.TabIndex = 40;
            this.lblCostCentre.Text = "Cost Centre*";
            // 
            // lblCostingMethod
            // 
            this.lblCostingMethod.AutoSize = true;
            this.lblCostingMethod.Location = new System.Drawing.Point(6, 41);
            this.lblCostingMethod.Name = "lblCostingMethod";
            this.lblCostingMethod.Size = new System.Drawing.Size(102, 13);
            this.lblCostingMethod.TabIndex = 39;
            this.lblCostingMethod.Text = "Costing Method*";
            // 
            // FrmCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(669, 351);
            this.Controls.Add(this.grpCompany);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCompany";
            this.Text = "Company";
            this.Load += new System.EventHandler(this.FrmCompany_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpCompany, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabCompany.ResumeLayout(false);
            this.tbpGeneralDetails.ResumeLayout(false);
            this.tbpGeneralDetails.PerformLayout();
            this.tbpContactDetails.ResumeLayout(false);
            this.tbpContactDetails.PerformLayout();
            this.tbpTaxDetails.ResumeLayout(false);
            this.tbpTaxDetails.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCompany;
        private CustomControls.TextBoxMasterCode txtCompanyCode;
        private System.Windows.Forms.Label lblCompanyCode;
        private System.Windows.Forms.Label lblCompanyName;
        private CustomControls.TextBoxMasterDescription txtCompanyName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabCompany;
        private System.Windows.Forms.TabPage tbpContactDetails;
        private System.Windows.Forms.TabPage tbpOtherDetails;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.Label lblWeb;
        private System.Windows.Forms.ComboBox cmbCostingMethod;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.Label lblCostingMethod;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label lblFinancialYear;
        private System.Windows.Forms.TabPage tbpTaxDetails;
        private System.Windows.Forms.TabPage tbpGeneralDetails;
        private System.Windows.Forms.Label lblGroupOfCompanyCode;
        private System.Windows.Forms.TextBox txtOtherBusinessName3;
        private System.Windows.Forms.TextBox txtOtherBusinessName2;
        private System.Windows.Forms.TextBox txtOtherBusinessName1;
        private System.Windows.Forms.Label lblOtherBusiness3;
        private System.Windows.Forms.Label lblOtherBusiness2;
        private System.Windows.Forms.Label lblOtherBusiness1;
        private System.Windows.Forms.TextBox txtTax5No;
        private System.Windows.Forms.TextBox txtTax4No;
        private System.Windows.Forms.TextBox txtTax3No;
        private System.Windows.Forms.TextBox txtTax2No;
        private System.Windows.Forms.TextBox txtTax1No;
        private System.Windows.Forms.Label lblTax5;
        private System.Windows.Forms.Label lblTax4;
        private System.Windows.Forms.Label lblTax3;
        private System.Windows.Forms.Label lblTax2;
        private System.Windows.Forms.Label lblTax1;
        private System.Windows.Forms.CheckBox chkTax5;
        private System.Windows.Forms.CheckBox chkTax4;
        private System.Windows.Forms.CheckBox chkTax3;
        private System.Windows.Forms.CheckBox chkTax2;
        private System.Windows.Forms.CheckBox chkTax1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.TextBox txtGroupOfCompanyCode;
        private System.Windows.Forms.CheckBox chkAutoCompleteCompanyCode;
        private System.Windows.Forms.TextBox txtGroupOfCompanyName;
        private System.Windows.Forms.Label lblGroupOfCompanyName;
        private System.Windows.Forms.ComboBox cmbCostCentre;
    }
}
