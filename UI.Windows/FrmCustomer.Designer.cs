namespace UI.Windows
{
    partial class FrmCustomer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCustomerName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCustomerCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbTitle = new System.Windows.Forms.ComboBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.lblBillingMobile = new System.Windows.Forms.Label();
            this.txtBillingMobile = new System.Windows.Forms.TextBox();
            this.tabCustomer = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRNic = new System.Windows.Forms.TextBox();
            this.txtRepresentativeMoblie = new System.Windows.Forms.TextBox();
            this.lblRepresentativeMoblie = new System.Windows.Forms.Label();
            this.cmbCustomerGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblNic = new System.Windows.Forms.Label();
            this.txtNic = new System.Windows.Forms.TextBox();
            this.txtRepresentativeName = new System.Windows.Forms.TextBox();
            this.lblRepresentativeName = new System.Windows.Forms.Label();
            this.tpContact = new System.Windows.Forms.TabPage();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtBillingFax = new System.Windows.Forms.TextBox();
            this.txtBillingTelephone = new System.Windows.Forms.TextBox();
            this.txtBillingAddress3 = new System.Windows.Forms.TextBox();
            this.txtBillingAddress2 = new System.Windows.Forms.TextBox();
            this.txtBillingAddress1 = new System.Windows.Forms.TextBox();
            this.lblBillingFax = new System.Windows.Forms.Label();
            this.lblBillingTelephone = new System.Windows.Forms.Label();
            this.lblBillingEmail = new System.Windows.Forms.Label();
            this.lblBillingAddress3 = new System.Windows.Forms.Label();
            this.lblBillingAddress2 = new System.Windows.Forms.Label();
            this.lblBillingAddress1 = new System.Windows.Forms.Label();
            this.tpDelivery = new System.Windows.Forms.TabPage();
            this.txtDeliveryMobile = new System.Windows.Forms.TextBox();
            this.txtDeliveryFax = new System.Windows.Forms.TextBox();
            this.txtDeliveryTelephone = new System.Windows.Forms.TextBox();
            this.txtDeliveryAddress3 = new System.Windows.Forms.TextBox();
            this.txtDeliveryAddress2 = new System.Windows.Forms.TextBox();
            this.txtDeliveryAddress1 = new System.Windows.Forms.TextBox();
            this.lblDeliveryFax = new System.Windows.Forms.Label();
            this.lblDeliveryTelephone = new System.Windows.Forms.Label();
            this.lblDeliveryMobile = new System.Windows.Forms.Label();
            this.lblDeliveryAddress3 = new System.Windows.Forms.Label();
            this.lblDeliveryAddress2 = new System.Windows.Forms.Label();
            this.lblDeliveryAddress1 = new System.Windows.Forms.Label();
            this.tpFinancial = new System.Windows.Forms.TabPage();
            this.txtCreditPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.chkAutoCompleationOtherLedger = new System.Windows.Forms.CheckBox();
            this.txtOtherLedgerDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtOtherLedgerCode = new System.Windows.Forms.TextBox();
            this.lblOtherLedger = new System.Windows.Forms.Label();
            this.txtMaxCreditDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtMaxCashDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtTemporallyLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTemporaryLimit = new System.Windows.Forms.Label();
            this.txtChequePeriod = new UI.Windows.CustomControls.TextBoxNumeric();
            this.lblChequePeriod = new System.Windows.Forms.Label();
            this.lblCreditPeriod = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblCustomerType = new System.Windows.Forms.Label();
            this.cmbCustomerType = new System.Windows.Forms.ComboBox();
            this.lblMaximumCreditDiscountPercentage = new System.Windows.Forms.Label();
            this.txtBankDraft = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtChequeLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCreditLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationLedger = new System.Windows.Forms.CheckBox();
            this.txtLedgerDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtLedgerCode = new System.Windows.Forms.TextBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.lblOutstandingValue = new System.Windows.Forms.Label();
            this.lblOutstanding = new System.Windows.Forms.Label();
            this.lblMaximumCashDiscountPercentage = new System.Windows.Forms.Label();
            this.lblBankDraft = new System.Windows.Forms.Label();
            this.lblChequeLimit = new System.Windows.Forms.Label();
            this.lblCreditLimit = new System.Windows.Forms.Label();
            this.tpTaxDetails = new System.Windows.Forms.TabPage();
            this.txtTax5No = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTax4No = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTax3No = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTax2No = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTax1No = new UI.Windows.CustomControls.TextBoxMasterDescription();
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
            this.tpOther = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationArea = new System.Windows.Forms.CheckBox();
            this.txtAreaName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.chkAutoCompleationBroker = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationTerritory = new System.Windows.Forms.CheckBox();
            this.txtBrokerName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTerritoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBrokerCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtTerritoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblBroker = new System.Windows.Forms.Label();
            this.lblTerritory = new System.Windows.Forms.Label();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.chkSuspended = new System.Windows.Forms.CheckBox();
            this.chkBlackListed = new System.Windows.Forms.CheckBox();
            this.chkLoyalty = new System.Windows.Forms.CheckBox();
            this.chkCreditAllowed = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabCustomer.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpContact.SuspendLayout();
            this.tpDelivery.SuspendLayout();
            this.tpFinancial.SuspendLayout();
            this.tpTaxDetails.SuspendLayout();
            this.tpOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 302);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(584, 302);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Controls.Add(this.txtCustomerCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationCustomer);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.cmbTitle);
            this.groupBox1.Controls.Add(this.lblCustomerName);
            this.groupBox1.Controls.Add(this.lblCustomerCode);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(821, 53);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(277, 27);
            this.txtCustomerName.MasterDescription = "";
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(494, 21);
            this.txtCustomerName.TabIndex = 32;
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.IsAutoComplete = false;
            this.txtCustomerCode.ItemCollection = null;
            this.txtCustomerCode.Location = new System.Drawing.Point(20, 27);
            this.txtCustomerCode.MasterCode = "";
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(152, 21);
            this.txtCustomerCode.TabIndex = 31;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerCode_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(4, 30);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 29;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCustomer.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCustomer_CheckedChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(772, 26);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 30;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(171, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(38, 13);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "Title*";
            // 
            // cmbTitle
            // 
            this.cmbTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTitle.FormattingEnabled = true;
            this.cmbTitle.Location = new System.Drawing.Point(174, 27);
            this.cmbTitle.Name = "cmbTitle";
            this.cmbTitle.Size = new System.Drawing.Size(100, 21);
            this.cmbTitle.TabIndex = 4;
            this.cmbTitle.Tag = "1";
            this.cmbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTitle_KeyDown);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(273, 11);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(107, 13);
            this.lblCustomerName.TabIndex = 1;
            this.lblCustomerName.Text = "Customer Name*";
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Location = new System.Drawing.Point(17, 11);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(104, 13);
            this.lblCustomerCode.TabIndex = 0;
            this.lblCustomerCode.Text = "Customer Code*";
            // 
            // lblBillingMobile
            // 
            this.lblBillingMobile.AutoSize = true;
            this.lblBillingMobile.Location = new System.Drawing.Point(332, 67);
            this.lblBillingMobile.Name = "lblBillingMobile";
            this.lblBillingMobile.Size = new System.Drawing.Size(43, 13);
            this.lblBillingMobile.TabIndex = 14;
            this.lblBillingMobile.Text = "Mobile";
            // 
            // txtBillingMobile
            // 
            this.txtBillingMobile.Location = new System.Drawing.Point(403, 64);
            this.txtBillingMobile.Name = "txtBillingMobile";
            this.txtBillingMobile.Size = new System.Drawing.Size(120, 21);
            this.txtBillingMobile.TabIndex = 13;
            this.txtBillingMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingMobile_KeyDown);
            this.txtBillingMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillingMobile_KeyPress);
            // 
            // tabCustomer
            // 
            this.tabCustomer.Controls.Add(this.tpGeneral);
            this.tabCustomer.Controls.Add(this.tpContact);
            this.tabCustomer.Controls.Add(this.tpDelivery);
            this.tabCustomer.Controls.Add(this.tpFinancial);
            this.tabCustomer.Controls.Add(this.tpTaxDetails);
            this.tabCustomer.Controls.Add(this.tpOther);
            this.tabCustomer.Location = new System.Drawing.Point(2, 50);
            this.tabCustomer.Name = "tabCustomer";
            this.tabCustomer.SelectedIndex = 0;
            this.tabCustomer.Size = new System.Drawing.Size(688, 208);
            this.tabCustomer.TabIndex = 11;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.label2);
            this.tpGeneral.Controls.Add(this.txtRNic);
            this.tpGeneral.Controls.Add(this.txtRepresentativeMoblie);
            this.tpGeneral.Controls.Add(this.lblRepresentativeMoblie);
            this.tpGeneral.Controls.Add(this.cmbCustomerGroup);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.label34);
            this.tpGeneral.Controls.Add(this.cmbGender);
            this.tpGeneral.Controls.Add(this.lblGender);
            this.tpGeneral.Controls.Add(this.lblReferenceNo);
            this.tpGeneral.Controls.Add(this.txtReferenceNo);
            this.tpGeneral.Controls.Add(this.lblNic);
            this.tpGeneral.Controls.Add(this.txtNic);
            this.tpGeneral.Controls.Add(this.txtRepresentativeName);
            this.tpGeneral.Controls.Add(this.lblRepresentativeName);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(680, 182);
            this.tpGeneral.TabIndex = 3;
            this.tpGeneral.Text = "General Details";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(347, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 42;
            this.label2.Text = "R NIC/ Passport";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label2.UseCompatibleTextRendering = true;
            // 
            // txtRNic
            // 
            this.txtRNic.Location = new System.Drawing.Point(452, 120);
            this.txtRNic.Name = "txtRNic";
            this.txtRNic.Size = new System.Drawing.Size(215, 21);
            this.txtRNic.TabIndex = 41;
            this.txtRNic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRNic_KeyDown);
            // 
            // txtRepresentativeMoblie
            // 
            this.txtRepresentativeMoblie.Location = new System.Drawing.Point(144, 120);
            this.txtRepresentativeMoblie.Name = "txtRepresentativeMoblie";
            this.txtRepresentativeMoblie.Size = new System.Drawing.Size(197, 21);
            this.txtRepresentativeMoblie.TabIndex = 40;
            this.txtRepresentativeMoblie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRepresentativeMoblie_KeyDown);
            // 
            // lblRepresentativeMoblie
            // 
            this.lblRepresentativeMoblie.AutoSize = true;
            this.lblRepresentativeMoblie.Location = new System.Drawing.Point(6, 123);
            this.lblRepresentativeMoblie.Name = "lblRepresentativeMoblie";
            this.lblRepresentativeMoblie.Size = new System.Drawing.Size(133, 13);
            this.lblRepresentativeMoblie.TabIndex = 39;
            this.lblRepresentativeMoblie.Text = "Representative Mobile";
            // 
            // cmbCustomerGroup
            // 
            this.cmbCustomerGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerGroup.FormattingEnabled = true;
            this.cmbCustomerGroup.Location = new System.Drawing.Point(144, 40);
            this.cmbCustomerGroup.Name = "cmbCustomerGroup";
            this.cmbCustomerGroup.Size = new System.Drawing.Size(197, 21);
            this.cmbCustomerGroup.TabIndex = 38;
            this.cmbCustomerGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomerGroup_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Customer Group*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(347, 70);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(267, 13);
            this.label34.TabIndex = 36;
            this.label34.Text = "(NIC/ Passport/ Driving License/ Business/...)";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(144, 13);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(101, 21);
            this.cmbGender.TabIndex = 35;
            this.cmbGender.Tag = "";
            this.cmbGender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGender_KeyDown);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(6, 16);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(56, 13);
            this.lblGender.TabIndex = 34;
            this.lblGender.Text = "Gender*";
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 70);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 33;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(144, 67);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(197, 21);
            this.txtReferenceNo.TabIndex = 32;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblNic
            // 
            this.lblNic.AutoSize = true;
            this.lblNic.Location = new System.Drawing.Point(347, 96);
            this.lblNic.Name = "lblNic";
            this.lblNic.Size = new System.Drawing.Size(87, 13);
            this.lblNic.TabIndex = 31;
            this.lblNic.Text = "NIC/ Passport";
            // 
            // txtNic
            // 
            this.txtNic.Location = new System.Drawing.Point(452, 93);
            this.txtNic.Name = "txtNic";
            this.txtNic.Size = new System.Drawing.Size(215, 21);
            this.txtNic.TabIndex = 30;
            this.txtNic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNic_KeyDown);
            // 
            // txtRepresentativeName
            // 
            this.txtRepresentativeName.Location = new System.Drawing.Point(144, 93);
            this.txtRepresentativeName.Name = "txtRepresentativeName";
            this.txtRepresentativeName.Size = new System.Drawing.Size(197, 21);
            this.txtRepresentativeName.TabIndex = 29;
            this.txtRepresentativeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRepresentativeName_KeyDown);
            // 
            // lblRepresentativeName
            // 
            this.lblRepresentativeName.AutoSize = true;
            this.lblRepresentativeName.Location = new System.Drawing.Point(6, 96);
            this.lblRepresentativeName.Name = "lblRepresentativeName";
            this.lblRepresentativeName.Size = new System.Drawing.Size(130, 13);
            this.lblRepresentativeName.TabIndex = 28;
            this.lblRepresentativeName.Text = "Representative Name";
            // 
            // tpContact
            // 
            this.tpContact.Controls.Add(this.txtContactPerson);
            this.tpContact.Controls.Add(this.lblContactPerson);
            this.tpContact.Controls.Add(this.lblBillingMobile);
            this.tpContact.Controls.Add(this.txtEmail);
            this.tpContact.Controls.Add(this.txtBillingMobile);
            this.tpContact.Controls.Add(this.txtBillingFax);
            this.tpContact.Controls.Add(this.txtBillingTelephone);
            this.tpContact.Controls.Add(this.txtBillingAddress3);
            this.tpContact.Controls.Add(this.txtBillingAddress2);
            this.tpContact.Controls.Add(this.txtBillingAddress1);
            this.tpContact.Controls.Add(this.lblBillingFax);
            this.tpContact.Controls.Add(this.lblBillingTelephone);
            this.tpContact.Controls.Add(this.lblBillingEmail);
            this.tpContact.Controls.Add(this.lblBillingAddress3);
            this.tpContact.Controls.Add(this.lblBillingAddress2);
            this.tpContact.Controls.Add(this.lblBillingAddress1);
            this.tpContact.Location = new System.Drawing.Point(4, 22);
            this.tpContact.Name = "tpContact";
            this.tpContact.Padding = new System.Windows.Forms.Padding(3);
            this.tpContact.Size = new System.Drawing.Size(680, 182);
            this.tpContact.TabIndex = 0;
            this.tpContact.Text = "Contact Details";
            this.tpContact.UseVisualStyleBackColor = true;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(113, 91);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(203, 21);
            this.txtContactPerson.TabIndex = 21;
            this.txtContactPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactPerson_KeyDown);
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(6, 94);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(94, 13);
            this.lblContactPerson.TabIndex = 20;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(403, 13);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(271, 21);
            this.txtEmail.TabIndex = 19;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // txtBillingFax
            // 
            this.txtBillingFax.Location = new System.Drawing.Point(561, 64);
            this.txtBillingFax.Name = "txtBillingFax";
            this.txtBillingFax.Size = new System.Drawing.Size(113, 21);
            this.txtBillingFax.TabIndex = 18;
            this.txtBillingFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingFax_KeyDown);
            this.txtBillingFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillingFax_KeyPress);
            // 
            // txtBillingTelephone
            // 
            this.txtBillingTelephone.Location = new System.Drawing.Point(403, 38);
            this.txtBillingTelephone.Name = "txtBillingTelephone";
            this.txtBillingTelephone.Size = new System.Drawing.Size(271, 21);
            this.txtBillingTelephone.TabIndex = 17;
            this.txtBillingTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingTelephone_KeyDown);
            this.txtBillingTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillingTelephone_KeyPress);
            // 
            // txtBillingAddress3
            // 
            this.txtBillingAddress3.Location = new System.Drawing.Point(113, 64);
            this.txtBillingAddress3.Name = "txtBillingAddress3";
            this.txtBillingAddress3.Size = new System.Drawing.Size(203, 21);
            this.txtBillingAddress3.TabIndex = 16;
            this.txtBillingAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingAddress3_KeyDown);
            // 
            // txtBillingAddress2
            // 
            this.txtBillingAddress2.Location = new System.Drawing.Point(113, 38);
            this.txtBillingAddress2.Name = "txtBillingAddress2";
            this.txtBillingAddress2.Size = new System.Drawing.Size(203, 21);
            this.txtBillingAddress2.TabIndex = 15;
            this.txtBillingAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingAddress2_KeyDown);
            // 
            // txtBillingAddress1
            // 
            this.txtBillingAddress1.Location = new System.Drawing.Point(113, 13);
            this.txtBillingAddress1.Name = "txtBillingAddress1";
            this.txtBillingAddress1.Size = new System.Drawing.Size(204, 21);
            this.txtBillingAddress1.TabIndex = 14;
            this.txtBillingAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillingAddress1_KeyDown);
            // 
            // lblBillingFax
            // 
            this.lblBillingFax.AutoSize = true;
            this.lblBillingFax.Location = new System.Drawing.Point(529, 67);
            this.lblBillingFax.Name = "lblBillingFax";
            this.lblBillingFax.Size = new System.Drawing.Size(26, 13);
            this.lblBillingFax.TabIndex = 13;
            this.lblBillingFax.Text = "Fax";
            // 
            // lblBillingTelephone
            // 
            this.lblBillingTelephone.AutoSize = true;
            this.lblBillingTelephone.Location = new System.Drawing.Point(332, 41);
            this.lblBillingTelephone.Name = "lblBillingTelephone";
            this.lblBillingTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblBillingTelephone.TabIndex = 12;
            this.lblBillingTelephone.Text = "Telephone";
            // 
            // lblBillingEmail
            // 
            this.lblBillingEmail.AutoSize = true;
            this.lblBillingEmail.Location = new System.Drawing.Point(332, 16);
            this.lblBillingEmail.Name = "lblBillingEmail";
            this.lblBillingEmail.Size = new System.Drawing.Size(43, 13);
            this.lblBillingEmail.TabIndex = 10;
            this.lblBillingEmail.Text = "E-mail";
            // 
            // lblBillingAddress3
            // 
            this.lblBillingAddress3.AutoSize = true;
            this.lblBillingAddress3.Location = new System.Drawing.Point(6, 67);
            this.lblBillingAddress3.Name = "lblBillingAddress3";
            this.lblBillingAddress3.Size = new System.Drawing.Size(102, 13);
            this.lblBillingAddress3.TabIndex = 9;
            this.lblBillingAddress3.Text = "Billing Address 3";
            // 
            // lblBillingAddress2
            // 
            this.lblBillingAddress2.AutoSize = true;
            this.lblBillingAddress2.Location = new System.Drawing.Point(6, 41);
            this.lblBillingAddress2.Name = "lblBillingAddress2";
            this.lblBillingAddress2.Size = new System.Drawing.Size(102, 13);
            this.lblBillingAddress2.TabIndex = 8;
            this.lblBillingAddress2.Text = "Billing Address 2";
            // 
            // lblBillingAddress1
            // 
            this.lblBillingAddress1.AutoSize = true;
            this.lblBillingAddress1.Location = new System.Drawing.Point(6, 16);
            this.lblBillingAddress1.Name = "lblBillingAddress1";
            this.lblBillingAddress1.Size = new System.Drawing.Size(102, 13);
            this.lblBillingAddress1.TabIndex = 7;
            this.lblBillingAddress1.Text = "Billing Address 1";
            // 
            // tpDelivery
            // 
            this.tpDelivery.Controls.Add(this.txtDeliveryMobile);
            this.tpDelivery.Controls.Add(this.txtDeliveryFax);
            this.tpDelivery.Controls.Add(this.txtDeliveryTelephone);
            this.tpDelivery.Controls.Add(this.txtDeliveryAddress3);
            this.tpDelivery.Controls.Add(this.txtDeliveryAddress2);
            this.tpDelivery.Controls.Add(this.txtDeliveryAddress1);
            this.tpDelivery.Controls.Add(this.lblDeliveryFax);
            this.tpDelivery.Controls.Add(this.lblDeliveryTelephone);
            this.tpDelivery.Controls.Add(this.lblDeliveryMobile);
            this.tpDelivery.Controls.Add(this.lblDeliveryAddress3);
            this.tpDelivery.Controls.Add(this.lblDeliveryAddress2);
            this.tpDelivery.Controls.Add(this.lblDeliveryAddress1);
            this.tpDelivery.Location = new System.Drawing.Point(4, 22);
            this.tpDelivery.Name = "tpDelivery";
            this.tpDelivery.Padding = new System.Windows.Forms.Padding(3);
            this.tpDelivery.Size = new System.Drawing.Size(680, 182);
            this.tpDelivery.TabIndex = 1;
            this.tpDelivery.Text = "Delivery Details";
            this.tpDelivery.UseVisualStyleBackColor = true;
            // 
            // txtDeliveryMobile
            // 
            this.txtDeliveryMobile.Location = new System.Drawing.Point(399, 38);
            this.txtDeliveryMobile.Name = "txtDeliveryMobile";
            this.txtDeliveryMobile.Size = new System.Drawing.Size(113, 21);
            this.txtDeliveryMobile.TabIndex = 31;
            this.txtDeliveryMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryMobile_KeyDown);
            this.txtDeliveryMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeliveryMobile_KeyPress);
            // 
            // txtDeliveryFax
            // 
            this.txtDeliveryFax.Location = new System.Drawing.Point(556, 13);
            this.txtDeliveryFax.Name = "txtDeliveryFax";
            this.txtDeliveryFax.Size = new System.Drawing.Size(118, 21);
            this.txtDeliveryFax.TabIndex = 30;
            this.txtDeliveryFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryFax_KeyDown);
            this.txtDeliveryFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeliveryFax_KeyPress);
            // 
            // txtDeliveryTelephone
            // 
            this.txtDeliveryTelephone.Location = new System.Drawing.Point(399, 13);
            this.txtDeliveryTelephone.Name = "txtDeliveryTelephone";
            this.txtDeliveryTelephone.Size = new System.Drawing.Size(113, 21);
            this.txtDeliveryTelephone.TabIndex = 29;
            this.txtDeliveryTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryTelephone_KeyDown);
            this.txtDeliveryTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeliveryTelephone_KeyPress);
            // 
            // txtDeliveryAddress3
            // 
            this.txtDeliveryAddress3.Location = new System.Drawing.Point(125, 64);
            this.txtDeliveryAddress3.Name = "txtDeliveryAddress3";
            this.txtDeliveryAddress3.Size = new System.Drawing.Size(192, 21);
            this.txtDeliveryAddress3.TabIndex = 28;
            this.txtDeliveryAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryAddress3_KeyDown);
            // 
            // txtDeliveryAddress2
            // 
            this.txtDeliveryAddress2.Location = new System.Drawing.Point(125, 38);
            this.txtDeliveryAddress2.Name = "txtDeliveryAddress2";
            this.txtDeliveryAddress2.Size = new System.Drawing.Size(192, 21);
            this.txtDeliveryAddress2.TabIndex = 27;
            this.txtDeliveryAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryAddress2_KeyDown);
            // 
            // txtDeliveryAddress1
            // 
            this.txtDeliveryAddress1.Location = new System.Drawing.Point(125, 13);
            this.txtDeliveryAddress1.Name = "txtDeliveryAddress1";
            this.txtDeliveryAddress1.Size = new System.Drawing.Size(192, 21);
            this.txtDeliveryAddress1.TabIndex = 26;
            this.txtDeliveryAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeliveryAddress1_KeyDown);
            // 
            // lblDeliveryFax
            // 
            this.lblDeliveryFax.AutoSize = true;
            this.lblDeliveryFax.Location = new System.Drawing.Point(524, 16);
            this.lblDeliveryFax.Name = "lblDeliveryFax";
            this.lblDeliveryFax.Size = new System.Drawing.Size(26, 13);
            this.lblDeliveryFax.TabIndex = 25;
            this.lblDeliveryFax.Text = "Fax";
            // 
            // lblDeliveryTelephone
            // 
            this.lblDeliveryTelephone.AutoSize = true;
            this.lblDeliveryTelephone.Location = new System.Drawing.Point(328, 16);
            this.lblDeliveryTelephone.Name = "lblDeliveryTelephone";
            this.lblDeliveryTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblDeliveryTelephone.TabIndex = 24;
            this.lblDeliveryTelephone.Text = "Telephone";
            // 
            // lblDeliveryMobile
            // 
            this.lblDeliveryMobile.AutoSize = true;
            this.lblDeliveryMobile.Location = new System.Drawing.Point(328, 41);
            this.lblDeliveryMobile.Name = "lblDeliveryMobile";
            this.lblDeliveryMobile.Size = new System.Drawing.Size(43, 13);
            this.lblDeliveryMobile.TabIndex = 23;
            this.lblDeliveryMobile.Text = "Mobile";
            // 
            // lblDeliveryAddress3
            // 
            this.lblDeliveryAddress3.AutoSize = true;
            this.lblDeliveryAddress3.Location = new System.Drawing.Point(6, 67);
            this.lblDeliveryAddress3.Name = "lblDeliveryAddress3";
            this.lblDeliveryAddress3.Size = new System.Drawing.Size(116, 13);
            this.lblDeliveryAddress3.TabIndex = 22;
            this.lblDeliveryAddress3.Text = "Delivery Address 3";
            // 
            // lblDeliveryAddress2
            // 
            this.lblDeliveryAddress2.AutoSize = true;
            this.lblDeliveryAddress2.Location = new System.Drawing.Point(6, 41);
            this.lblDeliveryAddress2.Name = "lblDeliveryAddress2";
            this.lblDeliveryAddress2.Size = new System.Drawing.Size(116, 13);
            this.lblDeliveryAddress2.TabIndex = 21;
            this.lblDeliveryAddress2.Text = "Delivery Address 2";
            // 
            // lblDeliveryAddress1
            // 
            this.lblDeliveryAddress1.AutoSize = true;
            this.lblDeliveryAddress1.Location = new System.Drawing.Point(6, 16);
            this.lblDeliveryAddress1.Name = "lblDeliveryAddress1";
            this.lblDeliveryAddress1.Size = new System.Drawing.Size(116, 13);
            this.lblDeliveryAddress1.TabIndex = 20;
            this.lblDeliveryAddress1.Text = "Delivery Address 1";
            // 
            // tpFinancial
            // 
            this.tpFinancial.Controls.Add(this.txtCreditPeriod);
            this.tpFinancial.Controls.Add(this.chkAutoCompleationOtherLedger);
            this.tpFinancial.Controls.Add(this.txtOtherLedgerDescription);
            this.tpFinancial.Controls.Add(this.txtOtherLedgerCode);
            this.tpFinancial.Controls.Add(this.lblOtherLedger);
            this.tpFinancial.Controls.Add(this.txtMaxCreditDiscount);
            this.tpFinancial.Controls.Add(this.txtMaxCashDiscount);
            this.tpFinancial.Controls.Add(this.txtTemporallyLimit);
            this.tpFinancial.Controls.Add(this.lblTemporaryLimit);
            this.tpFinancial.Controls.Add(this.txtChequePeriod);
            this.tpFinancial.Controls.Add(this.lblChequePeriod);
            this.tpFinancial.Controls.Add(this.lblCreditPeriod);
            this.tpFinancial.Controls.Add(this.lblPaymentMethod);
            this.tpFinancial.Controls.Add(this.cmbPaymentMethod);
            this.tpFinancial.Controls.Add(this.lblCustomerType);
            this.tpFinancial.Controls.Add(this.cmbCustomerType);
            this.tpFinancial.Controls.Add(this.lblMaximumCreditDiscountPercentage);
            this.tpFinancial.Controls.Add(this.txtBankDraft);
            this.tpFinancial.Controls.Add(this.txtChequeLimit);
            this.tpFinancial.Controls.Add(this.txtCreditLimit);
            this.tpFinancial.Controls.Add(this.chkAutoCompleationLedger);
            this.tpFinancial.Controls.Add(this.txtLedgerDescription);
            this.tpFinancial.Controls.Add(this.txtLedgerCode);
            this.tpFinancial.Controls.Add(this.lblLedger);
            this.tpFinancial.Controls.Add(this.lblOutstandingValue);
            this.tpFinancial.Controls.Add(this.lblOutstanding);
            this.tpFinancial.Controls.Add(this.lblMaximumCashDiscountPercentage);
            this.tpFinancial.Controls.Add(this.lblBankDraft);
            this.tpFinancial.Controls.Add(this.lblChequeLimit);
            this.tpFinancial.Controls.Add(this.lblCreditLimit);
            this.tpFinancial.Location = new System.Drawing.Point(4, 22);
            this.tpFinancial.Name = "tpFinancial";
            this.tpFinancial.Padding = new System.Windows.Forms.Padding(3);
            this.tpFinancial.Size = new System.Drawing.Size(680, 182);
            this.tpFinancial.TabIndex = 4;
            this.tpFinancial.Text = "Financial Details";
            this.tpFinancial.UseVisualStyleBackColor = true;
            // 
            // txtCreditPeriod
            // 
            this.txtCreditPeriod.IntValue = 0;
            this.txtCreditPeriod.Location = new System.Drawing.Point(362, 31);
            this.txtCreditPeriod.Name = "txtCreditPeriod";
            this.txtCreditPeriod.Size = new System.Drawing.Size(86, 21);
            this.txtCreditPeriod.TabIndex = 71;
            this.txtCreditPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCreditPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCreditPeriod_KeyDown);
            // 
            // chkAutoCompleationOtherLedger
            // 
            this.chkAutoCompleationOtherLedger.AutoSize = true;
            this.chkAutoCompleationOtherLedger.Checked = true;
            this.chkAutoCompleationOtherLedger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationOtherLedger.Location = new System.Drawing.Point(61, 159);
            this.chkAutoCompleationOtherLedger.Name = "chkAutoCompleationOtherLedger";
            this.chkAutoCompleationOtherLedger.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationOtherLedger.TabIndex = 70;
            this.chkAutoCompleationOtherLedger.Tag = "1";
            this.chkAutoCompleationOtherLedger.UseVisualStyleBackColor = true;
            this.chkAutoCompleationOtherLedger.Visible = false;
            this.chkAutoCompleationOtherLedger.CheckedChanged += new System.EventHandler(this.chkAutoCompleationOtherLedger_CheckedChanged);
            // 
            // txtOtherLedgerDescription
            // 
            this.txtOtherLedgerDescription.Location = new System.Drawing.Point(211, 156);
            this.txtOtherLedgerDescription.MasterDescription = "";
            this.txtOtherLedgerDescription.Name = "txtOtherLedgerDescription";
            this.txtOtherLedgerDescription.Size = new System.Drawing.Size(439, 21);
            this.txtOtherLedgerDescription.TabIndex = 69;
            this.txtOtherLedgerDescription.Visible = false;
            this.txtOtherLedgerDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherLedgerDescription_KeyDown);
            this.txtOtherLedgerDescription.Leave += new System.EventHandler(this.txtOtherLedgerDescription_Leave);
            // 
            // txtOtherLedgerCode
            // 
            this.txtOtherLedgerCode.Location = new System.Drawing.Point(78, 156);
            this.txtOtherLedgerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOtherLedgerCode.Name = "txtOtherLedgerCode";
            this.txtOtherLedgerCode.Size = new System.Drawing.Size(130, 21);
            this.txtOtherLedgerCode.TabIndex = 68;
            this.txtOtherLedgerCode.Visible = false;
            this.txtOtherLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherLedgerCode_KeyDown);
            this.txtOtherLedgerCode.Leave += new System.EventHandler(this.txtOtherLedgerCode_Leave);
            // 
            // lblOtherLedger
            // 
            this.lblOtherLedger.AutoSize = true;
            this.lblOtherLedger.Location = new System.Drawing.Point(257, 181);
            this.lblOtherLedger.Name = "lblOtherLedger";
            this.lblOtherLedger.Size = new System.Drawing.Size(46, 13);
            this.lblOtherLedger.TabIndex = 67;
            this.lblOtherLedger.Text = "Ledger";
            // 
            // txtMaxCreditDiscount
            // 
            this.txtMaxCreditDiscount.Location = new System.Drawing.Point(409, 79);
            this.txtMaxCreditDiscount.MaxLength = 2;
            this.txtMaxCreditDiscount.Name = "txtMaxCreditDiscount";
            this.txtMaxCreditDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMaxCreditDiscount.Size = new System.Drawing.Size(39, 21);
            this.txtMaxCreditDiscount.TabIndex = 66;
            this.txtMaxCreditDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxCreditDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxCreditDiscount_KeyDown);
            // 
            // txtMaxCashDiscount
            // 
            this.txtMaxCashDiscount.Location = new System.Drawing.Point(192, 79);
            this.txtMaxCashDiscount.Name = "txtMaxCashDiscount";
            this.txtMaxCashDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMaxCashDiscount.Size = new System.Drawing.Size(39, 21);
            this.txtMaxCashDiscount.TabIndex = 65;
            this.txtMaxCashDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxCashDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxCashDiscount_KeyDown);
            // 
            // txtTemporallyLimit
            // 
            this.txtTemporallyLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTemporallyLimit.Location = new System.Drawing.Point(549, 30);
            this.txtTemporallyLimit.Name = "txtTemporallyLimit";
            this.txtTemporallyLimit.Size = new System.Drawing.Size(124, 21);
            this.txtTemporallyLimit.TabIndex = 64;
            this.txtTemporallyLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTemporallyLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTemporallyLimit_KeyDown);
            // 
            // lblTemporaryLimit
            // 
            this.lblTemporaryLimit.AutoSize = true;
            this.lblTemporaryLimit.Location = new System.Drawing.Point(449, 34);
            this.lblTemporaryLimit.Name = "lblTemporaryLimit";
            this.lblTemporaryLimit.Size = new System.Drawing.Size(100, 13);
            this.lblTemporaryLimit.TabIndex = 63;
            this.lblTemporaryLimit.Text = "Temporary Limit";
            // 
            // txtChequePeriod
            // 
            this.txtChequePeriod.Location = new System.Drawing.Point(380, 55);
            this.txtChequePeriod.Name = "txtChequePeriod";
            this.txtChequePeriod.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChequePeriod.Size = new System.Drawing.Size(68, 21);
            this.txtChequePeriod.TabIndex = 62;
            this.txtChequePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChequePeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequePeriod_KeyDown);
            // 
            // lblChequePeriod
            // 
            this.lblChequePeriod.AutoSize = true;
            this.lblChequePeriod.Location = new System.Drawing.Point(231, 58);
            this.lblChequePeriod.Name = "lblChequePeriod";
            this.lblChequePeriod.Size = new System.Drawing.Size(134, 13);
            this.lblChequePeriod.TabIndex = 60;
            this.lblChequePeriod.Text = "Cheque Period (Days)";
            // 
            // lblCreditPeriod
            // 
            this.lblCreditPeriod.AutoSize = true;
            this.lblCreditPeriod.Location = new System.Drawing.Point(231, 34);
            this.lblCreditPeriod.Name = "lblCreditPeriod";
            this.lblCreditPeriod.Size = new System.Drawing.Size(125, 13);
            this.lblCreditPeriod.TabIndex = 59;
            this.lblCreditPeriod.Text = "Credit Period (Days)";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(231, 10);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(109, 13);
            this.lblPaymentMethod.TabIndex = 58;
            this.lblPaymentMethod.Text = "Payment Method*";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(346, 7);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(102, 21);
            this.cmbPaymentMethod.TabIndex = 57;
            this.cmbPaymentMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentMethod_KeyDown);
            // 
            // lblCustomerType
            // 
            this.lblCustomerType.AutoSize = true;
            this.lblCustomerType.Location = new System.Drawing.Point(5, 10);
            this.lblCustomerType.Name = "lblCustomerType";
            this.lblCustomerType.Size = new System.Drawing.Size(101, 13);
            this.lblCustomerType.TabIndex = 46;
            this.lblCustomerType.Text = "Customer Type*";
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Location = new System.Drawing.Point(112, 7);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new System.Drawing.Size(119, 21);
            this.cmbCustomerType.TabIndex = 45;
            this.cmbCustomerType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomerType_KeyDown);
            // 
            // lblMaximumCreditDiscountPercentage
            // 
            this.lblMaximumCreditDiscountPercentage.AutoSize = true;
            this.lblMaximumCreditDiscountPercentage.Location = new System.Drawing.Point(231, 82);
            this.lblMaximumCreditDiscountPercentage.Name = "lblMaximumCreditDiscountPercentage";
            this.lblMaximumCreditDiscountPercentage.Size = new System.Drawing.Size(180, 13);
            this.lblMaximumCreditDiscountPercentage.TabIndex = 43;
            this.lblMaximumCreditDiscountPercentage.Text = "Maximum Credit Discount (%)";
            // 
            // txtBankDraft
            // 
            this.txtBankDraft.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBankDraft.Location = new System.Drawing.Point(549, 7);
            this.txtBankDraft.Name = "txtBankDraft";
            this.txtBankDraft.Size = new System.Drawing.Size(124, 21);
            this.txtBankDraft.TabIndex = 42;
            this.txtBankDraft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBankDraft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankDraft_KeyDown);
            // 
            // txtChequeLimit
            // 
            this.txtChequeLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChequeLimit.Location = new System.Drawing.Point(101, 55);
            this.txtChequeLimit.Name = "txtChequeLimit";
            this.txtChequeLimit.Size = new System.Drawing.Size(130, 21);
            this.txtChequeLimit.TabIndex = 41;
            this.txtChequeLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChequeLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeLimit_KeyDown);
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCreditLimit.Location = new System.Drawing.Point(101, 31);
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(130, 21);
            this.txtCreditLimit.TabIndex = 40;
            this.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCreditLimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCreditLimit_KeyDown);
            // 
            // chkAutoCompleationLedger
            // 
            this.chkAutoCompleationLedger.AutoSize = true;
            this.chkAutoCompleationLedger.Checked = true;
            this.chkAutoCompleationLedger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationLedger.Location = new System.Drawing.Point(84, 131);
            this.chkAutoCompleationLedger.Name = "chkAutoCompleationLedger";
            this.chkAutoCompleationLedger.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationLedger.TabIndex = 39;
            this.chkAutoCompleationLedger.Tag = "1";
            this.chkAutoCompleationLedger.UseVisualStyleBackColor = true;
            this.chkAutoCompleationLedger.Visible = false;
            this.chkAutoCompleationLedger.CheckedChanged += new System.EventHandler(this.chkAutoCompleationLedger_CheckedChanged);
            // 
            // txtLedgerDescription
            // 
            this.txtLedgerDescription.Location = new System.Drawing.Point(234, 128);
            this.txtLedgerDescription.MasterDescription = "";
            this.txtLedgerDescription.Name = "txtLedgerDescription";
            this.txtLedgerDescription.Size = new System.Drawing.Size(371, 21);
            this.txtLedgerDescription.TabIndex = 38;
            this.txtLedgerDescription.Visible = false;
            this.txtLedgerDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerDescription_KeyDown);
            this.txtLedgerDescription.Leave += new System.EventHandler(this.txtLedgerDescription_Leave);
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(101, 128);
            this.txtLedgerCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Size = new System.Drawing.Size(130, 21);
            this.txtLedgerCode.TabIndex = 37;
            this.txtLedgerCode.Visible = false;
            this.txtLedgerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerCode_KeyDown);
            this.txtLedgerCode.Leave += new System.EventHandler(this.txtLedgerCode_Leave);
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(6, 131);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(46, 13);
            this.lblLedger.TabIndex = 36;
            this.lblLedger.Text = "Ledger";
            this.lblLedger.Visible = false;
            // 
            // lblOutstandingValue
            // 
            this.lblOutstandingValue.AutoSize = true;
            this.lblOutstandingValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutstandingValue.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOutstandingValue.Location = new System.Drawing.Point(546, 87);
            this.lblOutstandingValue.Name = "lblOutstandingValue";
            this.lblOutstandingValue.Size = new System.Drawing.Size(35, 13);
            this.lblOutstandingValue.TabIndex = 25;
            this.lblOutstandingValue.Text = "0.00";
            // 
            // lblOutstanding
            // 
            this.lblOutstanding.AutoSize = true;
            this.lblOutstanding.Location = new System.Drawing.Point(451, 82);
            this.lblOutstanding.Name = "lblOutstanding";
            this.lblOutstanding.Size = new System.Drawing.Size(75, 13);
            this.lblOutstanding.TabIndex = 24;
            this.lblOutstanding.Text = "Outstanding";
            // 
            // lblMaximumCashDiscountPercentage
            // 
            this.lblMaximumCashDiscountPercentage.AutoSize = true;
            this.lblMaximumCashDiscountPercentage.Location = new System.Drawing.Point(5, 82);
            this.lblMaximumCashDiscountPercentage.Name = "lblMaximumCashDiscountPercentage";
            this.lblMaximumCashDiscountPercentage.Size = new System.Drawing.Size(174, 13);
            this.lblMaximumCashDiscountPercentage.TabIndex = 22;
            this.lblMaximumCashDiscountPercentage.Text = "Maximum Cash Discount (%)";
            // 
            // lblBankDraft
            // 
            this.lblBankDraft.AutoSize = true;
            this.lblBankDraft.Location = new System.Drawing.Point(450, 10);
            this.lblBankDraft.Name = "lblBankDraft";
            this.lblBankDraft.Size = new System.Drawing.Size(69, 13);
            this.lblBankDraft.TabIndex = 18;
            this.lblBankDraft.Text = "Bank Draft";
            // 
            // lblChequeLimit
            // 
            this.lblChequeLimit.AutoSize = true;
            this.lblChequeLimit.Location = new System.Drawing.Point(6, 58);
            this.lblChequeLimit.Name = "lblChequeLimit";
            this.lblChequeLimit.Size = new System.Drawing.Size(82, 13);
            this.lblChequeLimit.TabIndex = 17;
            this.lblChequeLimit.Text = "Cheque Limit";
            // 
            // lblCreditLimit
            // 
            this.lblCreditLimit.AutoSize = true;
            this.lblCreditLimit.Location = new System.Drawing.Point(6, 34);
            this.lblCreditLimit.Name = "lblCreditLimit";
            this.lblCreditLimit.Size = new System.Drawing.Size(73, 13);
            this.lblCreditLimit.TabIndex = 16;
            this.lblCreditLimit.Text = "Credit Limit";
            // 
            // tpTaxDetails
            // 
            this.tpTaxDetails.Controls.Add(this.txtTax5No);
            this.tpTaxDetails.Controls.Add(this.txtTax4No);
            this.tpTaxDetails.Controls.Add(this.txtTax3No);
            this.tpTaxDetails.Controls.Add(this.txtTax2No);
            this.tpTaxDetails.Controls.Add(this.txtTax1No);
            this.tpTaxDetails.Controls.Add(this.lblTax5);
            this.tpTaxDetails.Controls.Add(this.lblTax4);
            this.tpTaxDetails.Controls.Add(this.lblTax3);
            this.tpTaxDetails.Controls.Add(this.lblTax2);
            this.tpTaxDetails.Controls.Add(this.lblTax1);
            this.tpTaxDetails.Controls.Add(this.chkTax5);
            this.tpTaxDetails.Controls.Add(this.chkTax4);
            this.tpTaxDetails.Controls.Add(this.chkTax3);
            this.tpTaxDetails.Controls.Add(this.chkTax2);
            this.tpTaxDetails.Controls.Add(this.chkTax1);
            this.tpTaxDetails.Location = new System.Drawing.Point(4, 22);
            this.tpTaxDetails.Name = "tpTaxDetails";
            this.tpTaxDetails.Size = new System.Drawing.Size(680, 182);
            this.tpTaxDetails.TabIndex = 5;
            this.tpTaxDetails.Text = "Tax Details";
            this.tpTaxDetails.UseVisualStyleBackColor = true;
            // 
            // txtTax5No
            // 
            this.txtTax5No.Location = new System.Drawing.Point(173, 121);
            this.txtTax5No.MasterDescription = "";
            this.txtTax5No.Name = "txtTax5No";
            this.txtTax5No.Size = new System.Drawing.Size(224, 21);
            this.txtTax5No.TabIndex = 86;
            this.txtTax5No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax5No_KeyDown);
            // 
            // txtTax4No
            // 
            this.txtTax4No.Location = new System.Drawing.Point(173, 94);
            this.txtTax4No.MasterDescription = "";
            this.txtTax4No.Name = "txtTax4No";
            this.txtTax4No.Size = new System.Drawing.Size(224, 21);
            this.txtTax4No.TabIndex = 85;
            this.txtTax4No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax4No_KeyDown);
            // 
            // txtTax3No
            // 
            this.txtTax3No.Location = new System.Drawing.Point(173, 67);
            this.txtTax3No.MasterDescription = "";
            this.txtTax3No.Name = "txtTax3No";
            this.txtTax3No.Size = new System.Drawing.Size(224, 21);
            this.txtTax3No.TabIndex = 84;
            this.txtTax3No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax3No_KeyDown);
            // 
            // txtTax2No
            // 
            this.txtTax2No.Location = new System.Drawing.Point(173, 40);
            this.txtTax2No.MasterDescription = "";
            this.txtTax2No.Name = "txtTax2No";
            this.txtTax2No.Size = new System.Drawing.Size(224, 21);
            this.txtTax2No.TabIndex = 83;
            this.txtTax2No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax2No_KeyDown);
            // 
            // txtTax1No
            // 
            this.txtTax1No.Location = new System.Drawing.Point(173, 13);
            this.txtTax1No.MasterDescription = "";
            this.txtTax1No.Name = "txtTax1No";
            this.txtTax1No.Size = new System.Drawing.Size(224, 21);
            this.txtTax1No.TabIndex = 82;
            this.txtTax1No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax1No_KeyDown);
            // 
            // lblTax5
            // 
            this.lblTax5.AutoSize = true;
            this.lblTax5.Location = new System.Drawing.Point(9, 124);
            this.lblTax5.Name = "lblTax5";
            this.lblTax5.Size = new System.Drawing.Size(38, 13);
            this.lblTax5.TabIndex = 81;
            this.lblTax5.Text = "Tax 5";
            // 
            // lblTax4
            // 
            this.lblTax4.AutoSize = true;
            this.lblTax4.Location = new System.Drawing.Point(9, 97);
            this.lblTax4.Name = "lblTax4";
            this.lblTax4.Size = new System.Drawing.Size(38, 13);
            this.lblTax4.TabIndex = 80;
            this.lblTax4.Text = "Tax 4";
            // 
            // lblTax3
            // 
            this.lblTax3.AutoSize = true;
            this.lblTax3.Location = new System.Drawing.Point(9, 70);
            this.lblTax3.Name = "lblTax3";
            this.lblTax3.Size = new System.Drawing.Size(38, 13);
            this.lblTax3.TabIndex = 79;
            this.lblTax3.Text = "Tax 3";
            // 
            // lblTax2
            // 
            this.lblTax2.AutoSize = true;
            this.lblTax2.Location = new System.Drawing.Point(9, 44);
            this.lblTax2.Name = "lblTax2";
            this.lblTax2.Size = new System.Drawing.Size(38, 13);
            this.lblTax2.TabIndex = 78;
            this.lblTax2.Text = "Tax 2";
            // 
            // lblTax1
            // 
            this.lblTax1.AutoSize = true;
            this.lblTax1.Location = new System.Drawing.Point(9, 16);
            this.lblTax1.Name = "lblTax1";
            this.lblTax1.Size = new System.Drawing.Size(38, 13);
            this.lblTax1.TabIndex = 77;
            this.lblTax1.Text = "Tax 1";
            // 
            // chkTax5
            // 
            this.chkTax5.AutoSize = true;
            this.chkTax5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax5.Location = new System.Drawing.Point(147, 124);
            this.chkTax5.Name = "chkTax5";
            this.chkTax5.Size = new System.Drawing.Size(15, 14);
            this.chkTax5.TabIndex = 76;
            this.chkTax5.UseVisualStyleBackColor = true;
            this.chkTax5.CheckedChanged += new System.EventHandler(this.chkTax5_CheckedChanged);
            // 
            // chkTax4
            // 
            this.chkTax4.AutoSize = true;
            this.chkTax4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax4.Location = new System.Drawing.Point(147, 97);
            this.chkTax4.Name = "chkTax4";
            this.chkTax4.Size = new System.Drawing.Size(15, 14);
            this.chkTax4.TabIndex = 75;
            this.chkTax4.UseVisualStyleBackColor = true;
            this.chkTax4.CheckedChanged += new System.EventHandler(this.chkTax4_CheckedChanged);
            // 
            // chkTax3
            // 
            this.chkTax3.AutoSize = true;
            this.chkTax3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax3.Location = new System.Drawing.Point(147, 70);
            this.chkTax3.Name = "chkTax3";
            this.chkTax3.Size = new System.Drawing.Size(15, 14);
            this.chkTax3.TabIndex = 74;
            this.chkTax3.UseVisualStyleBackColor = true;
            this.chkTax3.CheckedChanged += new System.EventHandler(this.chkTax3_CheckedChanged);
            // 
            // chkTax2
            // 
            this.chkTax2.AutoSize = true;
            this.chkTax2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax2.Location = new System.Drawing.Point(147, 43);
            this.chkTax2.Name = "chkTax2";
            this.chkTax2.Size = new System.Drawing.Size(15, 14);
            this.chkTax2.TabIndex = 73;
            this.chkTax2.UseVisualStyleBackColor = true;
            this.chkTax2.CheckedChanged += new System.EventHandler(this.chkTax2_CheckedChanged);
            // 
            // chkTax1
            // 
            this.chkTax1.AutoSize = true;
            this.chkTax1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTax1.Location = new System.Drawing.Point(147, 16);
            this.chkTax1.Name = "chkTax1";
            this.chkTax1.Size = new System.Drawing.Size(15, 14);
            this.chkTax1.TabIndex = 72;
            this.chkTax1.UseVisualStyleBackColor = true;
            this.chkTax1.CheckedChanged += new System.EventHandler(this.chkTax1_CheckedChanged);
            // 
            // tpOther
            // 
            this.tpOther.Controls.Add(this.chkAutoCompleationArea);
            this.tpOther.Controls.Add(this.txtAreaName);
            this.tpOther.Controls.Add(this.txtAreaCode);
            this.tpOther.Controls.Add(this.lblArea);
            this.tpOther.Controls.Add(this.chkAutoCompleationBroker);
            this.tpOther.Controls.Add(this.chkAutoCompleationTerritory);
            this.tpOther.Controls.Add(this.txtBrokerName);
            this.tpOther.Controls.Add(this.txtTerritoryName);
            this.tpOther.Controls.Add(this.txtBrokerCode);
            this.tpOther.Controls.Add(this.txtTerritoryCode);
            this.tpOther.Controls.Add(this.lblBroker);
            this.tpOther.Controls.Add(this.lblTerritory);
            this.tpOther.Location = new System.Drawing.Point(4, 22);
            this.tpOther.Name = "tpOther";
            this.tpOther.Size = new System.Drawing.Size(680, 182);
            this.tpOther.TabIndex = 2;
            this.tpOther.Text = "Other Details";
            this.tpOther.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationArea
            // 
            this.chkAutoCompleationArea.AutoSize = true;
            this.chkAutoCompleationArea.Checked = true;
            this.chkAutoCompleationArea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationArea.Location = new System.Drawing.Point(78, 16);
            this.chkAutoCompleationArea.Name = "chkAutoCompleationArea";
            this.chkAutoCompleationArea.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationArea.TabIndex = 35;
            this.chkAutoCompleationArea.Tag = "1";
            this.chkAutoCompleationArea.UseVisualStyleBackColor = true;
            this.chkAutoCompleationArea.CheckedChanged += new System.EventHandler(this.chkAutoCompleationArea_CheckedChanged);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(234, 12);
            this.txtAreaName.MasterDescription = "";
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(436, 21);
            this.txtAreaName.TabIndex = 34;
            this.txtAreaName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaName_KeyDown);
            this.txtAreaName.Leave += new System.EventHandler(this.txtAreaName_Leave);
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Location = new System.Drawing.Point(95, 12);
            this.txtAreaCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(136, 21);
            this.txtAreaCode.TabIndex = 33;
            this.txtAreaCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaCode_KeyDown);
            this.txtAreaCode.Leave += new System.EventHandler(this.txtAreaCode_Leave);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(6, 16);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(41, 13);
            this.lblArea.TabIndex = 32;
            this.lblArea.Text = "Area*";
            // 
            // chkAutoCompleationBroker
            // 
            this.chkAutoCompleationBroker.AutoSize = true;
            this.chkAutoCompleationBroker.Checked = true;
            this.chkAutoCompleationBroker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBroker.Location = new System.Drawing.Point(78, 68);
            this.chkAutoCompleationBroker.Name = "chkAutoCompleationBroker";
            this.chkAutoCompleationBroker.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBroker.TabIndex = 31;
            this.chkAutoCompleationBroker.Tag = "1";
            this.chkAutoCompleationBroker.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBroker.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBroker_CheckedChanged);
            // 
            // chkAutoCompleationTerritory
            // 
            this.chkAutoCompleationTerritory.AutoSize = true;
            this.chkAutoCompleationTerritory.Checked = true;
            this.chkAutoCompleationTerritory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationTerritory.Location = new System.Drawing.Point(78, 41);
            this.chkAutoCompleationTerritory.Name = "chkAutoCompleationTerritory";
            this.chkAutoCompleationTerritory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationTerritory.TabIndex = 30;
            this.chkAutoCompleationTerritory.Tag = "1";
            this.chkAutoCompleationTerritory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationTerritory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationTerritory_CheckedChanged);
            // 
            // txtBrokerName
            // 
            this.txtBrokerName.Location = new System.Drawing.Point(234, 65);
            this.txtBrokerName.MasterDescription = "";
            this.txtBrokerName.Name = "txtBrokerName";
            this.txtBrokerName.Size = new System.Drawing.Size(436, 21);
            this.txtBrokerName.TabIndex = 19;
            this.txtBrokerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrokerName_KeyDown);
            this.txtBrokerName.Leave += new System.EventHandler(this.txtBrokerName_Leave);
            // 
            // txtTerritoryName
            // 
            this.txtTerritoryName.Location = new System.Drawing.Point(234, 38);
            this.txtTerritoryName.MasterDescription = "";
            this.txtTerritoryName.Name = "txtTerritoryName";
            this.txtTerritoryName.Size = new System.Drawing.Size(436, 21);
            this.txtTerritoryName.TabIndex = 18;
            this.txtTerritoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerritoryName_KeyDown);
            this.txtTerritoryName.Leave += new System.EventHandler(this.txtTerritoryName_Leave);
            // 
            // txtBrokerCode
            // 
            this.txtBrokerCode.IsAutoComplete = false;
            this.txtBrokerCode.ItemCollection = null;
            this.txtBrokerCode.Location = new System.Drawing.Point(95, 65);
            this.txtBrokerCode.MasterCode = "";
            this.txtBrokerCode.Name = "txtBrokerCode";
            this.txtBrokerCode.Size = new System.Drawing.Size(136, 21);
            this.txtBrokerCode.TabIndex = 17;
            this.txtBrokerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrokerCode_KeyDown);
            this.txtBrokerCode.Leave += new System.EventHandler(this.txtBrokerCode_Leave);
            // 
            // txtTerritoryCode
            // 
            this.txtTerritoryCode.IsAutoComplete = false;
            this.txtTerritoryCode.ItemCollection = null;
            this.txtTerritoryCode.Location = new System.Drawing.Point(95, 38);
            this.txtTerritoryCode.MasterCode = "";
            this.txtTerritoryCode.Name = "txtTerritoryCode";
            this.txtTerritoryCode.Size = new System.Drawing.Size(136, 21);
            this.txtTerritoryCode.TabIndex = 16;
            this.txtTerritoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerritoryCode_KeyDown);
            this.txtTerritoryCode.Leave += new System.EventHandler(this.txtTerritoryCode_Leave);
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Location = new System.Drawing.Point(6, 68);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(46, 13);
            this.lblBroker.TabIndex = 6;
            this.lblBroker.Text = "Broker";
            // 
            // lblTerritory
            // 
            this.lblTerritory.AutoSize = true;
            this.lblTerritory.Location = new System.Drawing.Point(5, 41);
            this.lblTerritory.Name = "lblTerritory";
            this.lblTerritory.Size = new System.Drawing.Size(63, 13);
            this.lblTerritory.TabIndex = 2;
            this.lblTerritory.Text = "Territory*";
            // 
            // pbCustomer
            // 
            this.pbCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCustomer.ErrorImage = null;
            this.pbCustomer.Image = global::UI.Windows.Properties.Resources.Default_Customer;
            this.pbCustomer.Location = new System.Drawing.Point(692, 70);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(131, 133);
            this.pbCustomer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCustomer.TabIndex = 12;
            this.pbCustomer.TabStop = false;
            // 
            // chkSuspended
            // 
            this.chkSuspended.AutoSize = true;
            this.chkSuspended.Location = new System.Drawing.Point(134, 12);
            this.chkSuspended.Name = "chkSuspended";
            this.chkSuspended.Size = new System.Drawing.Size(89, 17);
            this.chkSuspended.TabIndex = 13;
            this.chkSuspended.Text = "Suspended";
            this.chkSuspended.UseVisualStyleBackColor = true;
            this.chkSuspended.Visible = false;
            // 
            // chkBlackListed
            // 
            this.chkBlackListed.AutoSize = true;
            this.chkBlackListed.Location = new System.Drawing.Point(134, 33);
            this.chkBlackListed.Name = "chkBlackListed";
            this.chkBlackListed.Size = new System.Drawing.Size(94, 17);
            this.chkBlackListed.TabIndex = 14;
            this.chkBlackListed.Text = "Black Listed";
            this.chkBlackListed.UseVisualStyleBackColor = true;
            // 
            // chkLoyalty
            // 
            this.chkLoyalty.AutoSize = true;
            this.chkLoyalty.Location = new System.Drawing.Point(7, 12);
            this.chkLoyalty.Name = "chkLoyalty";
            this.chkLoyalty.Size = new System.Drawing.Size(67, 17);
            this.chkLoyalty.TabIndex = 15;
            this.chkLoyalty.Text = "Loyalty";
            this.chkLoyalty.UseVisualStyleBackColor = true;
            this.chkLoyalty.Visible = false;
            // 
            // chkCreditAllowed
            // 
            this.chkCreditAllowed.AutoSize = true;
            this.chkCreditAllowed.Location = new System.Drawing.Point(7, 33);
            this.chkCreditAllowed.Name = "chkCreditAllowed";
            this.chkCreditAllowed.Size = new System.Drawing.Size(109, 17);
            this.chkCreditAllowed.TabIndex = 16;
            this.chkCreditAllowed.Text = "Credit Allowed";
            this.chkCreditAllowed.UseVisualStyleBackColor = true;
            this.chkCreditAllowed.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCreditAllowed);
            this.groupBox2.Controls.Add(this.chkLoyalty);
            this.groupBox2.Controls.Add(this.chkBlackListed);
            this.groupBox2.Controls.Add(this.chkSuspended);
            this.groupBox2.Location = new System.Drawing.Point(584, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 54);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAutoClear);
            this.groupBox3.Controls.Add(this.txtRemark);
            this.groupBox3.Controls.Add(this.lblRemark);
            this.groupBox3.Location = new System.Drawing.Point(2, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(580, 54);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(489, 31);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 31;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(81, 12);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(398, 36);
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
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(692, 213);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 25);
            this.btnBrowse.TabIndex = 19;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClearImage
            // 
            this.btnClearImage.Location = new System.Drawing.Point(762, 213);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(61, 25);
            this.btnClearImage.TabIndex = 20;
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.UseVisualStyleBackColor = true;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(825, 351);
            this.Controls.Add(this.tabCustomer);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pbCustomer);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCustomer";
            this.Text = "Customer";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.pbCustomer, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.btnClearImage, 0);
            this.Controls.SetChildIndex(this.tabCustomer, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabCustomer.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpContact.ResumeLayout(false);
            this.tpContact.PerformLayout();
            this.tpDelivery.ResumeLayout(false);
            this.tpDelivery.PerformLayout();
            this.tpFinancial.ResumeLayout(false);
            this.tpFinancial.PerformLayout();
            this.tpTaxDetails.ResumeLayout(false);
            this.tpTaxDetails.PerformLayout();
            this.tpOther.ResumeLayout(false);
            this.tpOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabCustomer;
        private System.Windows.Forms.TabPage tpContact;
        private System.Windows.Forms.TabPage tpDelivery;
        private System.Windows.Forms.TextBox txtBillingAddress3;
        private System.Windows.Forms.TextBox txtBillingAddress2;
        private System.Windows.Forms.TextBox txtBillingAddress1;
        private System.Windows.Forms.Label lblBillingFax;
        private System.Windows.Forms.Label lblBillingTelephone;
        private System.Windows.Forms.Label lblBillingEmail;
        private System.Windows.Forms.Label lblBillingAddress3;
        private System.Windows.Forms.Label lblBillingAddress2;
        private System.Windows.Forms.Label lblBillingAddress1;
        private System.Windows.Forms.TextBox txtBillingMobile;
        private System.Windows.Forms.Label lblBillingMobile;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtBillingFax;
        private System.Windows.Forms.TextBox txtBillingTelephone;
        private System.Windows.Forms.TextBox txtDeliveryMobile;
        private System.Windows.Forms.TextBox txtDeliveryFax;
        private System.Windows.Forms.TextBox txtDeliveryTelephone;
        private System.Windows.Forms.TextBox txtDeliveryAddress3;
        private System.Windows.Forms.TextBox txtDeliveryAddress2;
        private System.Windows.Forms.TextBox txtDeliveryAddress1;
        private System.Windows.Forms.Label lblDeliveryFax;
        private System.Windows.Forms.Label lblDeliveryTelephone;
        private System.Windows.Forms.Label lblDeliveryMobile;
        private System.Windows.Forms.Label lblDeliveryAddress3;
        private System.Windows.Forms.Label lblDeliveryAddress2;
        private System.Windows.Forms.Label lblDeliveryAddress1;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.CheckBox chkSuspended;
        private System.Windows.Forms.CheckBox chkBlackListed;
        private System.Windows.Forms.CheckBox chkLoyalty;
        private System.Windows.Forms.CheckBox chkCreditAllowed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tpOther;
        private System.Windows.Forms.Label lblTerritory;
        private System.Windows.Forms.Label lblBroker;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblNic;
        private System.Windows.Forms.TextBox txtNic;
        private System.Windows.Forms.TextBox txtRepresentativeName;
        private System.Windows.Forms.Label lblRepresentativeName;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TabPage tpFinancial;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.CheckBox chkAutoCompleationBroker;
        private System.Windows.Forms.CheckBox chkAutoCompleationTerritory;
        private CustomControls.TextBoxMasterDescription txtBrokerName;
        private CustomControls.TextBoxMasterDescription txtTerritoryName;
        private CustomControls.TextBoxMasterCode txtBrokerCode;
        private CustomControls.TextBoxMasterCode txtTerritoryCode;
        private System.Windows.Forms.Label lblOutstandingValue;
        private System.Windows.Forms.Label lblOutstanding;
        private System.Windows.Forms.Label lblBankDraft;
        private System.Windows.Forms.Label lblChequeLimit;
        private System.Windows.Forms.Label lblCreditLimit;
        private CustomControls.TextBoxCurrency txtBankDraft;
        private CustomControls.TextBoxCurrency txtChequeLimit;
        private CustomControls.TextBoxCurrency txtCreditLimit;
        private System.Windows.Forms.CheckBox chkAutoCompleationLedger;
        private CustomControls.TextBoxMasterDescription txtLedgerDescription;
        private System.Windows.Forms.TextBox txtLedgerCode;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.Label lblMaximumCashDiscountPercentage;
        private System.Windows.Forms.CheckBox chkAutoCompleationArea;
        private CustomControls.TextBoxMasterDescription txtAreaName;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblMaximumCreditDiscountPercentage;
        private System.Windows.Forms.Label lblCustomerType;
        private System.Windows.Forms.ComboBox cmbCustomerType;
        private System.Windows.Forms.ComboBox cmbCustomerGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private CustomControls.TextBoxNumeric txtChequePeriod;
        private System.Windows.Forms.Label lblChequePeriod;
        private System.Windows.Forms.Label lblCreditPeriod;
        private CustomControls.TextBoxCurrency txtTemporallyLimit;
        private System.Windows.Forms.Label lblTemporaryLimit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxMasterCode txtCustomerCode;
        private CustomControls.TextBoxNumeric txtMaxCreditDiscount;
        private CustomControls.TextBoxNumeric txtMaxCashDiscount;
        private System.Windows.Forms.TextBox txtRepresentativeMoblie;
        private System.Windows.Forms.Label lblRepresentativeMoblie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRNic;
        private System.Windows.Forms.CheckBox chkAutoCompleationOtherLedger;
        private CustomControls.TextBoxMasterDescription txtOtherLedgerDescription;
        private System.Windows.Forms.TextBox txtOtherLedgerCode;
        private System.Windows.Forms.Label lblOtherLedger;
        private System.Windows.Forms.TabPage tpTaxDetails;
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
        private CustomControls.TextBoxMasterDescription txtTax5No;
        private CustomControls.TextBoxMasterDescription txtTax4No;
        private CustomControls.TextBoxMasterDescription txtTax3No;
        private CustomControls.TextBoxMasterDescription txtTax2No;
        private CustomControls.TextBoxMasterDescription txtTax1No;
        private CustomControls.TextBoxInteger txtCreditPeriod;
        private CustomControls.TextBoxMasterDescription txtCustomerName;

    }
}
