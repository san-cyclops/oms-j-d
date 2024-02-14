namespace UI.Windows
{
    partial class FrmLocation
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
            this.grpLocation = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleteLocationCode = new System.Windows.Forms.CheckBox();
            this.txtLocationName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtLocationCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblLocationCode = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.lblGroupOfCompany = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.tabLocation = new System.Windows.Forms.TabControl();
            this.tbpContactDetails = new System.Windows.Forms.TabPage();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress3 = new System.Windows.Forms.Label();
            this.lblAddress2 = new System.Windows.Forms.Label();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.tbpOtherDetails = new System.Windows.Forms.TabPage();
            this.cmbExistingLocations = new System.Windows.Forms.ComboBox();
            this.chkAddProducts = new System.Windows.Forms.CheckBox();
            this.txtTypeOfBusiness = new System.Windows.Forms.TextBox();
            this.txtLocationPrefix = new System.Windows.Forms.TextBox();
            this.txtOtherBusinessName = new System.Windows.Forms.TextBox();
            this.chkStockFacilitated = new System.Windows.Forms.CheckBox();
            this.chkTaxFacilitated = new System.Windows.Forms.CheckBox();
            this.cmbCostingMethod = new System.Windows.Forms.ComboBox();
            this.lblCostingMethod = new System.Windows.Forms.Label();
            this.lblTypeOfBusiness = new System.Windows.Forms.Label();
            this.lblLocationPrefix = new System.Windows.Forms.Label();
            this.lblOtherBusiness = new System.Windows.Forms.Label();
            this.tbpCostCentres = new System.Windows.Forms.TabPage();
            this.dgvCostCentres = new System.Windows.Forms.DataGridView();
            this.CostCentreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostCentreName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CostCentreID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpLocation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabLocation.SuspendLayout();
            this.tbpContactDetails.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.tbpCostCentres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostCentres)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 320);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 320);
            // 
            // grpLocation
            // 
            this.grpLocation.Controls.Add(this.chkAutoCompleteLocationCode);
            this.grpLocation.Controls.Add(this.txtLocationName);
            this.grpLocation.Controls.Add(this.btnNew);
            this.grpLocation.Controls.Add(this.txtLocationCode);
            this.grpLocation.Controls.Add(this.lblLocationCode);
            this.grpLocation.Controls.Add(this.cmbCompany);
            this.grpLocation.Controls.Add(this.lblLocationName);
            this.grpLocation.Controls.Add(this.lblGroupOfCompany);
            this.grpLocation.Location = new System.Drawing.Point(2, -5);
            this.grpLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLocation.Name = "grpLocation";
            this.grpLocation.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLocation.Size = new System.Drawing.Size(635, 94);
            this.grpLocation.TabIndex = 12;
            this.grpLocation.TabStop = false;
            this.grpLocation.Enter += new System.EventHandler(this.grpLocation_Enter);
            // 
            // chkAutoCompleteLocationCode
            // 
            this.chkAutoCompleteLocationCode.AutoSize = true;
            this.chkAutoCompleteLocationCode.Checked = true;
            this.chkAutoCompleteLocationCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleteLocationCode.Location = new System.Drawing.Point(9, 70);
            this.chkAutoCompleteLocationCode.Name = "chkAutoCompleteLocationCode";
            this.chkAutoCompleteLocationCode.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleteLocationCode.TabIndex = 33;
            this.chkAutoCompleteLocationCode.Tag = "1";
            this.chkAutoCompleteLocationCode.UseVisualStyleBackColor = true;
            this.chkAutoCompleteLocationCode.CheckedChanged += new System.EventHandler(this.chkAutoCompleteLocationCode_CheckedChanged);
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new System.Drawing.Point(183, 67);
            this.txtLocationName.MasterDescription = "";
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Size = new System.Drawing.Size(396, 21);
            this.txtLocationName.TabIndex = 12;
            this.txtLocationName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocationName_KeyDown);
            this.txtLocationName.Leave += new System.EventHandler(this.txtLocationName_Leave);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(580, 66);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 32;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocationCode.IsAutoComplete = false;
            this.txtLocationCode.ItemCollection = null;
            this.txtLocationCode.Location = new System.Drawing.Point(27, 67);
            this.txtLocationCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLocationCode.MasterCode = "";
            this.txtLocationCode.MaxLength = 25;
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.Size = new System.Drawing.Size(154, 21);
            this.txtLocationCode.TabIndex = 11;
            this.txtLocationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocationCode_KeyDown);
            this.txtLocationCode.Leave += new System.EventHandler(this.txtLocationCode_Leave);
            // 
            // lblLocationCode
            // 
            this.lblLocationCode.AutoSize = true;
            this.lblLocationCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationCode.Location = new System.Drawing.Point(24, 47);
            this.lblLocationCode.Name = "lblLocationCode";
            this.lblLocationCode.Size = new System.Drawing.Size(95, 13);
            this.lblLocationCode.TabIndex = 9;
            this.lblLocationCode.Text = "Location Code*";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(183, 17);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(444, 21);
            this.cmbCompany.TabIndex = 49;
            this.cmbCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCompany_KeyDown);
            this.cmbCompany.Leave += new System.EventHandler(this.cmbCompany_Leave);
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationName.Location = new System.Drawing.Point(181, 47);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(98, 13);
            this.lblLocationName.TabIndex = 10;
            this.lblLocationName.Text = "Location Name*";
            // 
            // lblGroupOfCompany
            // 
            this.lblGroupOfCompany.AutoSize = true;
            this.lblGroupOfCompany.Location = new System.Drawing.Point(24, 20);
            this.lblGroupOfCompany.Name = "lblGroupOfCompany";
            this.lblGroupOfCompany.Size = new System.Drawing.Size(125, 13);
            this.lblGroupOfCompany.TabIndex = 46;
            this.lblGroupOfCompany.Text = "Allocated Company*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.tabLocation);
            this.groupBox1.Location = new System.Drawing.Point(2, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 241);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(540, 217);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 33;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // tabLocation
            // 
            this.tabLocation.Controls.Add(this.tbpContactDetails);
            this.tabLocation.Controls.Add(this.tbpOtherDetails);
            this.tabLocation.Controls.Add(this.tbpCostCentres);
            this.tabLocation.Location = new System.Drawing.Point(5, 12);
            this.tabLocation.Name = "tabLocation";
            this.tabLocation.SelectedIndex = 0;
            this.tabLocation.Size = new System.Drawing.Size(627, 198);
            this.tabLocation.TabIndex = 14;
            // 
            // tbpContactDetails
            // 
            this.tbpContactDetails.Controls.Add(this.txtContactPerson);
            this.tbpContactDetails.Controls.Add(this.txtEmail);
            this.tbpContactDetails.Controls.Add(this.txtMobile);
            this.tbpContactDetails.Controls.Add(this.txtFax);
            this.tbpContactDetails.Controls.Add(this.txtTelephone);
            this.tbpContactDetails.Controls.Add(this.txtAddress3);
            this.tbpContactDetails.Controls.Add(this.txtAddress2);
            this.tbpContactDetails.Controls.Add(this.txtAddress1);
            this.tbpContactDetails.Controls.Add(this.lblContactPerson);
            this.tbpContactDetails.Controls.Add(this.lblMobile);
            this.tbpContactDetails.Controls.Add(this.lblFax);
            this.tbpContactDetails.Controls.Add(this.lblTelephone);
            this.tbpContactDetails.Controls.Add(this.lblEmail);
            this.tbpContactDetails.Controls.Add(this.lblAddress3);
            this.tbpContactDetails.Controls.Add(this.lblAddress2);
            this.tbpContactDetails.Controls.Add(this.lblAddress1);
            this.tbpContactDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpContactDetails.Name = "tbpContactDetails";
            this.tbpContactDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpContactDetails.Size = new System.Drawing.Size(619, 172);
            this.tbpContactDetails.TabIndex = 0;
            this.tbpContactDetails.Text = "Contact Details";
            this.tbpContactDetails.UseVisualStyleBackColor = true;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(106, 110);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(504, 21);
            this.txtContactPerson.TabIndex = 37;
            this.txtContactPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactPerson_KeyDown);
            this.txtContactPerson.Leave += new System.EventHandler(this.txtContactPerson_Leave);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(359, 65);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(251, 21);
            this.txtEmail.TabIndex = 35;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(359, 40);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(107, 21);
            this.txtMobile.TabIndex = 27;
            this.txtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyDown);
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            this.txtMobile.Leave += new System.EventHandler(this.txtMobile_Leave);
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(503, 40);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(107, 21);
            this.txtFax.TabIndex = 34;
            this.txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFax_KeyDown);
            this.txtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFax_KeyPress);
            this.txtFax.Leave += new System.EventHandler(this.txtFax_Leave);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(359, 15);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(251, 21);
            this.txtTelephone.TabIndex = 33;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelephone_KeyDown);
            this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
            this.txtTelephone.Leave += new System.EventHandler(this.txtTelephone_Leave);
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(91, 65);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(193, 21);
            this.txtAddress3.TabIndex = 32;
            this.txtAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress3_KeyDown);
            this.txtAddress3.Leave += new System.EventHandler(this.txtAddress3_Leave);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(91, 40);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(193, 21);
            this.txtAddress2.TabIndex = 31;
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            this.txtAddress2.Leave += new System.EventHandler(this.txtAddress2_Leave);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(91, 15);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(193, 21);
            this.txtAddress1.TabIndex = 30;
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            this.txtAddress1.Leave += new System.EventHandler(this.txtAddress1_Leave);
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(6, 113);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(94, 13);
            this.lblContactPerson.TabIndex = 36;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(294, 43);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(43, 13);
            this.lblMobile.TabIndex = 29;
            this.lblMobile.Text = "Mobile";
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.Location = new System.Drawing.Point(472, 43);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(26, 13);
            this.lblFax.TabIndex = 28;
            this.lblFax.Text = "Fax";
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(294, 18);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblTelephone.TabIndex = 26;
            this.lblTelephone.Text = "Telephone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(294, 68);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 13);
            this.lblEmail.TabIndex = 25;
            this.lblEmail.Text = "E-mail";
            // 
            // lblAddress3
            // 
            this.lblAddress3.AutoSize = true;
            this.lblAddress3.Location = new System.Drawing.Point(6, 68);
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
            this.lblAddress2.Size = new System.Drawing.Size(71, 13);
            this.lblAddress2.TabIndex = 23;
            this.lblAddress2.Text = "Address 2*";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(6, 18);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(71, 13);
            this.lblAddress1.TabIndex = 22;
            this.lblAddress1.Text = "Address 1*";
            // 
            // tbpOtherDetails
            // 
            this.tbpOtherDetails.Controls.Add(this.cmbExistingLocations);
            this.tbpOtherDetails.Controls.Add(this.chkAddProducts);
            this.tbpOtherDetails.Controls.Add(this.txtTypeOfBusiness);
            this.tbpOtherDetails.Controls.Add(this.txtLocationPrefix);
            this.tbpOtherDetails.Controls.Add(this.txtOtherBusinessName);
            this.tbpOtherDetails.Controls.Add(this.chkStockFacilitated);
            this.tbpOtherDetails.Controls.Add(this.chkTaxFacilitated);
            this.tbpOtherDetails.Controls.Add(this.cmbCostingMethod);
            this.tbpOtherDetails.Controls.Add(this.lblCostingMethod);
            this.tbpOtherDetails.Controls.Add(this.lblTypeOfBusiness);
            this.tbpOtherDetails.Controls.Add(this.lblLocationPrefix);
            this.tbpOtherDetails.Controls.Add(this.lblOtherBusiness);
            this.tbpOtherDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpOtherDetails.Name = "tbpOtherDetails";
            this.tbpOtherDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOtherDetails.Size = new System.Drawing.Size(619, 172);
            this.tbpOtherDetails.TabIndex = 1;
            this.tbpOtherDetails.Text = "Other Details";
            this.tbpOtherDetails.UseVisualStyleBackColor = true;
            // 
            // cmbExistingLocations
            // 
            this.cmbExistingLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExistingLocations.FormattingEnabled = true;
            this.cmbExistingLocations.Location = new System.Drawing.Point(227, 89);
            this.cmbExistingLocations.Name = "cmbExistingLocations";
            this.cmbExistingLocations.Size = new System.Drawing.Size(220, 21);
            this.cmbExistingLocations.TabIndex = 55;
            // 
            // chkAddProducts
            // 
            this.chkAddProducts.AutoSize = true;
            this.chkAddProducts.Location = new System.Drawing.Point(8, 91);
            this.chkAddProducts.Name = "chkAddProducts";
            this.chkAddProducts.Size = new System.Drawing.Size(216, 17);
            this.chkAddProducts.TabIndex = 54;
            this.chkAddProducts.Text = "Add Products Similler To Location";
            this.chkAddProducts.UseVisualStyleBackColor = true;
            this.chkAddProducts.CheckedChanged += new System.EventHandler(this.chkAddProducts_CheckedChanged);
            // 
            // txtTypeOfBusiness
            // 
            this.txtTypeOfBusiness.Location = new System.Drawing.Point(403, 35);
            this.txtTypeOfBusiness.Name = "txtTypeOfBusiness";
            this.txtTypeOfBusiness.Size = new System.Drawing.Size(181, 21);
            this.txtTypeOfBusiness.TabIndex = 38;
            this.txtTypeOfBusiness.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTypeOfBusiness_KeyDown);
            this.txtTypeOfBusiness.Leave += new System.EventHandler(this.txtTypeOfBusiness_Leave);
            // 
            // txtLocationPrefix
            // 
            this.txtLocationPrefix.Location = new System.Drawing.Point(166, 35);
            this.txtLocationPrefix.Name = "txtLocationPrefix";
            this.txtLocationPrefix.Size = new System.Drawing.Size(122, 21);
            this.txtLocationPrefix.TabIndex = 37;
            this.txtLocationPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocationPrefix_KeyDown);
            this.txtLocationPrefix.Leave += new System.EventHandler(this.txtLocationPrefix_Leave);
            // 
            // txtOtherBusinessName
            // 
            this.txtOtherBusinessName.Location = new System.Drawing.Point(166, 11);
            this.txtOtherBusinessName.Name = "txtOtherBusinessName";
            this.txtOtherBusinessName.Size = new System.Drawing.Size(122, 21);
            this.txtOtherBusinessName.TabIndex = 36;
            this.txtOtherBusinessName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherBusinessName_KeyDown);
            this.txtOtherBusinessName.Leave += new System.EventHandler(this.txtOtherBusinessName_Leave);
            // 
            // chkStockFacilitated
            // 
            this.chkStockFacilitated.AutoSize = true;
            this.chkStockFacilitated.Location = new System.Drawing.Point(488, 136);
            this.chkStockFacilitated.Name = "chkStockFacilitated";
            this.chkStockFacilitated.Size = new System.Drawing.Size(122, 17);
            this.chkStockFacilitated.TabIndex = 48;
            this.chkStockFacilitated.Text = "Stock Facilitated ";
            this.chkStockFacilitated.UseVisualStyleBackColor = true;
            this.chkStockFacilitated.CheckedChanged += new System.EventHandler(this.chkStockFacilitated_CheckedChanged);
            // 
            // chkTaxFacilitated
            // 
            this.chkTaxFacilitated.AutoSize = true;
            this.chkTaxFacilitated.Location = new System.Drawing.Point(488, 118);
            this.chkTaxFacilitated.Name = "chkTaxFacilitated";
            this.chkTaxFacilitated.Size = new System.Drawing.Size(110, 17);
            this.chkTaxFacilitated.TabIndex = 45;
            this.chkTaxFacilitated.Text = "Tax Facilitated ";
            this.chkTaxFacilitated.UseVisualStyleBackColor = true;
            this.chkTaxFacilitated.CheckedChanged += new System.EventHandler(this.chkTaxFacilitated_CheckedChanged);
            // 
            // cmbCostingMethod
            // 
            this.cmbCostingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostingMethod.FormattingEnabled = true;
            this.cmbCostingMethod.Location = new System.Drawing.Point(403, 11);
            this.cmbCostingMethod.Name = "cmbCostingMethod";
            this.cmbCostingMethod.Size = new System.Drawing.Size(181, 21);
            this.cmbCostingMethod.TabIndex = 41;
            // 
            // lblCostingMethod
            // 
            this.lblCostingMethod.AutoSize = true;
            this.lblCostingMethod.Location = new System.Drawing.Point(294, 15);
            this.lblCostingMethod.Name = "lblCostingMethod";
            this.lblCostingMethod.Size = new System.Drawing.Size(95, 13);
            this.lblCostingMethod.TabIndex = 39;
            this.lblCostingMethod.Text = "Costing Method";
            // 
            // lblTypeOfBusiness
            // 
            this.lblTypeOfBusiness.AutoSize = true;
            this.lblTypeOfBusiness.Location = new System.Drawing.Point(294, 40);
            this.lblTypeOfBusiness.Name = "lblTypeOfBusiness";
            this.lblTypeOfBusiness.Size = new System.Drawing.Size(103, 13);
            this.lblTypeOfBusiness.TabIndex = 35;
            this.lblTypeOfBusiness.Text = "Type of Business";
            // 
            // lblLocationPrefix
            // 
            this.lblLocationPrefix.AutoSize = true;
            this.lblLocationPrefix.Location = new System.Drawing.Point(6, 40);
            this.lblLocationPrefix.Name = "lblLocationPrefix";
            this.lblLocationPrefix.Size = new System.Drawing.Size(91, 13);
            this.lblLocationPrefix.TabIndex = 34;
            this.lblLocationPrefix.Text = "Location Prefix";
            // 
            // lblOtherBusiness
            // 
            this.lblOtherBusiness.AutoSize = true;
            this.lblOtherBusiness.Location = new System.Drawing.Point(6, 15);
            this.lblOtherBusiness.Name = "lblOtherBusiness";
            this.lblOtherBusiness.Size = new System.Drawing.Size(130, 13);
            this.lblOtherBusiness.TabIndex = 33;
            this.lblOtherBusiness.Text = "Other Business Name";
            // 
            // tbpCostCentres
            // 
            this.tbpCostCentres.Controls.Add(this.dgvCostCentres);
            this.tbpCostCentres.Location = new System.Drawing.Point(4, 22);
            this.tbpCostCentres.Name = "tbpCostCentres";
            this.tbpCostCentres.Size = new System.Drawing.Size(619, 172);
            this.tbpCostCentres.TabIndex = 2;
            this.tbpCostCentres.Text = "Cost Centres";
            this.tbpCostCentres.UseVisualStyleBackColor = true;
            // 
            // dgvCostCentres
            // 
            this.dgvCostCentres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCostCentres.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CostCentreCode,
            this.CostCentreName,
            this.Select,
            this.CostCentreID});
            this.dgvCostCentres.Location = new System.Drawing.Point(3, 4);
            this.dgvCostCentres.Name = "dgvCostCentres";
            this.dgvCostCentres.Size = new System.Drawing.Size(565, 164);
            this.dgvCostCentres.TabIndex = 0;
            // 
            // CostCentreCode
            // 
            this.CostCentreCode.DataPropertyName = "CostCentreCode";
            this.CostCentreCode.HeaderText = "Code";
            this.CostCentreCode.Name = "CostCentreCode";
            this.CostCentreCode.ReadOnly = true;
            // 
            // CostCentreName
            // 
            this.CostCentreName.DataPropertyName = "CostCentreName";
            this.CostCentreName.HeaderText = "Name";
            this.CostCentreName.Name = "CostCentreName";
            this.CostCentreName.ReadOnly = true;
            this.CostCentreName.Width = 300;
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Select";
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            // 
            // CostCentreID
            // 
            this.CostCentreID.DataPropertyName = "CostCentreID";
            this.CostCentreID.HeaderText = "Id";
            this.CostCentreID.Name = "CostCentreID";
            this.CostCentreID.ReadOnly = true;
            this.CostCentreID.Visible = false;
            // 
            // FrmLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(639, 369);
            this.Controls.Add(this.grpLocation);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLocation";
            this.Text = "Location";
            this.Load += new System.EventHandler(this.FrmLocation_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpLocation, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpLocation.ResumeLayout(false);
            this.grpLocation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabLocation.ResumeLayout(false);
            this.tbpContactDetails.ResumeLayout(false);
            this.tbpContactDetails.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.tbpCostCentres.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostCentres)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLocation;
        private CustomControls.TextBoxMasterCode txtLocationCode;
        private System.Windows.Forms.Label lblLocationCode;
        private System.Windows.Forms.Label lblLocationName;
        private CustomControls.TextBoxMasterDescription txtLocationName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.CheckBox chkAutoCompleteLocationCode;
        private System.Windows.Forms.TabControl tabLocation;
        private System.Windows.Forms.TabPage tbpContactDetails;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.TabPage tbpOtherDetails;
        private System.Windows.Forms.TextBox txtTypeOfBusiness;
        private System.Windows.Forms.TextBox txtLocationPrefix;
        private System.Windows.Forms.TextBox txtOtherBusinessName;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.CheckBox chkStockFacilitated;
        private System.Windows.Forms.Label lblGroupOfCompany;
        private System.Windows.Forms.CheckBox chkTaxFacilitated;
        private System.Windows.Forms.ComboBox cmbCostingMethod;
        private System.Windows.Forms.Label lblCostingMethod;
        private System.Windows.Forms.Label lblTypeOfBusiness;
        private System.Windows.Forms.Label lblLocationPrefix;
        private System.Windows.Forms.Label lblOtherBusiness;
        private System.Windows.Forms.ComboBox cmbExistingLocations;
        private System.Windows.Forms.CheckBox chkAddProducts;
        private System.Windows.Forms.TabPage tbpCostCentres;
        private System.Windows.Forms.DataGridView dgvCostCentres;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCentreCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCentreName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCentreID;
    }
}
