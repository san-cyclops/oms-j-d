namespace UI.Windows
{
    partial class FrmProductPriceChangeDamage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabCritaria = new System.Windows.Forms.TabControl();
            this.tpCritaria = new System.Windows.Forms.TabPage();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDamageType = new System.Windows.Forms.ComboBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTogDetails = new System.Windows.Forms.Button();
            this.cmbTogDocument = new System.Windows.Forms.ComboBox();
            this.lblPOSDocuments = new System.Windows.Forms.Label();
            this.cmbFromLocation = new System.Windows.Forms.ComboBox();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.txtNewProductName = new System.Windows.Forms.TextBox();
            this.chkTStatus = new System.Windows.Forms.CheckBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.chkAutoCompleationPoNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblPPChange = new System.Windows.Forms.Label();
            this.txtNewSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtMrp = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxNumeric();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.txtNewCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.pnlUpdateCostPrice = new System.Windows.Forms.Panel();
            this.chkIsIncrementCost = new System.Windows.Forms.CheckBox();
            this.txtCostDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.btnUpdateCostPrice = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.chkCostPercentage = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.LineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasureName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewSellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewCostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationNamex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlUpdateSellingPrice = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewUProName = new System.Windows.Forms.TextBox();
            this.txtNewUProduct = new System.Windows.Forms.TextBox();
            this.txtSellingDiscount = new UI.Windows.CustomControls.TextBoxNumeric();
            this.btnUpdateSellingPrice = new System.Windows.Forms.Button();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtNewProductCode = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.dtpEffectivDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.chkUpdateCostPrice = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabCritaria.SuspendLayout();
            this.tpCritaria.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.pnlUpdateCostPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.pnlUpdateSellingPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(856, 523);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(3, 523);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabCritaria);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Location = new System.Drawing.Point(2, -7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1206, 521);
            this.panel1.TabIndex = 12;
            // 
            // tabCritaria
            // 
            this.tabCritaria.Controls.Add(this.tpCritaria);
            this.tabCritaria.Location = new System.Drawing.Point(5, 9);
            this.tabCritaria.Name = "tabCritaria";
            this.tabCritaria.SelectedIndex = 0;
            this.tabCritaria.Size = new System.Drawing.Size(1193, 561);
            this.tabCritaria.TabIndex = 65;
            // 
            // tpCritaria
            // 
            this.tpCritaria.Controls.Add(this.pnlProduct);
            this.tpCritaria.Controls.Add(this.chkUpdateCostPrice);
            this.tpCritaria.Location = new System.Drawing.Point(4, 22);
            this.tpCritaria.Name = "tpCritaria";
            this.tpCritaria.Padding = new System.Windows.Forms.Padding(3);
            this.tpCritaria.Size = new System.Drawing.Size(1185, 535);
            this.tpCritaria.TabIndex = 1;
            this.tpCritaria.Text = "Selection Criteria";
            this.tpCritaria.UseVisualStyleBackColor = true;
            // 
            // pnlProduct
            // 
            this.pnlProduct.Controls.Add(this.label2);
            this.pnlProduct.Controls.Add(this.cmbDamageType);
            this.pnlProduct.Controls.Add(this.cmbDepartment);
            this.pnlProduct.Controls.Add(this.label1);
            this.pnlProduct.Controls.Add(this.btnTogDetails);
            this.pnlProduct.Controls.Add(this.cmbTogDocument);
            this.pnlProduct.Controls.Add(this.lblPOSDocuments);
            this.pnlProduct.Controls.Add(this.cmbFromLocation);
            this.pnlProduct.Controls.Add(this.lblFromLocation);
            this.pnlProduct.Controls.Add(this.txtNewProductName);
            this.pnlProduct.Controls.Add(this.chkTStatus);
            this.pnlProduct.Controls.Add(this.txtReferenceNo);
            this.pnlProduct.Controls.Add(this.lblReferenceNo);
            this.pnlProduct.Controls.Add(this.chkAutoCompleationPoNo);
            this.pnlProduct.Controls.Add(this.txtDocumentNo);
            this.pnlProduct.Controls.Add(this.lblPPChange);
            this.pnlProduct.Controls.Add(this.txtNewSellingPrice);
            this.pnlProduct.Controls.Add(this.txtMrp);
            this.pnlProduct.Controls.Add(this.txtTotalQty);
            this.pnlProduct.Controls.Add(this.cmbUnit);
            this.pnlProduct.Controls.Add(this.chkAutoCompleationProduct);
            this.pnlProduct.Controls.Add(this.txtNewCostPrice);
            this.pnlProduct.Controls.Add(this.pnlUpdateCostPrice);
            this.pnlProduct.Controls.Add(this.dgvItemDetails);
            this.pnlProduct.Controls.Add(this.pnlUpdateSellingPrice);
            this.pnlProduct.Controls.Add(this.txtSellingPrice);
            this.pnlProduct.Controls.Add(this.txtNewProductCode);
            this.pnlProduct.Controls.Add(this.txtQty);
            this.pnlProduct.Controls.Add(this.txtProductName);
            this.pnlProduct.Controls.Add(this.txtCostPrice);
            this.pnlProduct.Controls.Add(this.txtProductCode);
            this.pnlProduct.Controls.Add(this.dtpEffectivDate);
            this.pnlProduct.Controls.Add(this.label8);
            this.pnlProduct.Location = new System.Drawing.Point(3, 3);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(1160, 528);
            this.pnlProduct.TabIndex = 75;
            this.pnlProduct.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProduct_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1009, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 115;
            this.label2.Text = "Total QTY";
            // 
            // cmbDamageType
            // 
            this.cmbDamageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDamageType.FormattingEnabled = true;
            this.cmbDamageType.Location = new System.Drawing.Point(1063, 379);
            this.cmbDamageType.Name = "cmbDamageType";
            this.cmbDamageType.Size = new System.Drawing.Size(98, 21);
            this.cmbDamageType.TabIndex = 114;
            this.cmbDamageType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDamageType_KeyDown);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(423, 42);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(152, 21);
            this.cmbDepartment.TabIndex = 112;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(319, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Department";
            // 
            // btnTogDetails
            // 
            this.btnTogDetails.Location = new System.Drawing.Point(581, 15);
            this.btnTogDetails.Name = "btnTogDetails";
            this.btnTogDetails.Size = new System.Drawing.Size(55, 23);
            this.btnTogDetails.TabIndex = 111;
            this.btnTogDetails.Text = "Load";
            this.btnTogDetails.UseVisualStyleBackColor = true;
            this.btnTogDetails.Click += new System.EventHandler(this.btnTogDetails_Click);
            // 
            // cmbTogDocument
            // 
            this.cmbTogDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTogDocument.Enabled = false;
            this.cmbTogDocument.FormattingEnabled = true;
            this.cmbTogDocument.Location = new System.Drawing.Point(423, 16);
            this.cmbTogDocument.Name = "cmbTogDocument";
            this.cmbTogDocument.Size = new System.Drawing.Size(152, 21);
            this.cmbTogDocument.TabIndex = 109;
            // 
            // lblPOSDocuments
            // 
            this.lblPOSDocuments.AutoSize = true;
            this.lblPOSDocuments.Enabled = false;
            this.lblPOSDocuments.Location = new System.Drawing.Point(319, 20);
            this.lblPOSDocuments.Name = "lblPOSDocuments";
            this.lblPOSDocuments.Size = new System.Drawing.Size(100, 13);
            this.lblPOSDocuments.TabIndex = 110;
            this.lblPOSDocuments.Text = "TOG Documents";
            // 
            // cmbFromLocation
            // 
            this.cmbFromLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromLocation.FormattingEnabled = true;
            this.cmbFromLocation.Location = new System.Drawing.Point(76, 42);
            this.cmbFromLocation.Name = "cmbFromLocation";
            this.cmbFromLocation.Size = new System.Drawing.Size(210, 21);
            this.cmbFromLocation.TabIndex = 108;
            this.cmbFromLocation.SelectedIndexChanged += new System.EventHandler(this.cmbFromLocation_SelectedIndexChanged);
            this.cmbFromLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromLocation_KeyDown);
            this.cmbFromLocation.Leave += new System.EventHandler(this.cmbFromLocation_Leave);
            this.cmbFromLocation.Validated += new System.EventHandler(this.cmbFromLocation_Validated);
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Location = new System.Drawing.Point(10, 44);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(54, 13);
            this.lblFromLocation.TabIndex = 107;
            this.lblFromLocation.Text = "Location";
            // 
            // txtNewProductName
            // 
            this.txtNewProductName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNewProductName.Location = new System.Drawing.Point(687, 379);
            this.txtNewProductName.Name = "txtNewProductName";
            this.txtNewProductName.Size = new System.Drawing.Size(146, 21);
            this.txtNewProductName.TabIndex = 106;
            // 
            // chkTStatus
            // 
            this.chkTStatus.AutoSize = true;
            this.chkTStatus.Location = new System.Drawing.Point(1152, 20);
            this.chkTStatus.Name = "chkTStatus";
            this.chkTStatus.Size = new System.Drawing.Size(15, 14);
            this.chkTStatus.TabIndex = 102;
            this.chkTStatus.UseVisualStyleBackColor = true;
            this.chkTStatus.Visible = false;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(945, 17);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(201, 21);
            this.txtReferenceNo.TabIndex = 100;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(855, 20);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 101;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // chkAutoCompleationPoNo
            // 
            this.chkAutoCompleationPoNo.AutoSize = true;
            this.chkAutoCompleationPoNo.Checked = true;
            this.chkAutoCompleationPoNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPoNo.Location = new System.Drawing.Point(58, 19);
            this.chkAutoCompleationPoNo.Name = "chkAutoCompleationPoNo";
            this.chkAutoCompleationPoNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPoNo.TabIndex = 97;
            this.chkAutoCompleationPoNo.Tag = "1";
            this.chkAutoCompleationPoNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPoNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPoNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(78, 15);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(133, 21);
            this.txtDocumentNo.TabIndex = 98;
            this.txtDocumentNo.TextChanged += new System.EventHandler(this.txtDocumentNo_TextChanged);
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblPPChange
            // 
            this.lblPPChange.AutoSize = true;
            this.lblPPChange.Location = new System.Drawing.Point(10, 20);
            this.lblPPChange.Name = "lblPPChange";
            this.lblPPChange.Size = new System.Drawing.Size(42, 13);
            this.lblPPChange.TabIndex = 99;
            this.lblPPChange.Text = "PC No";
            // 
            // txtNewSellingPrice
            // 
            this.txtNewSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNewSellingPrice.Location = new System.Drawing.Point(442, 380);
            this.txtNewSellingPrice.Name = "txtNewSellingPrice";
            this.txtNewSellingPrice.Size = new System.Drawing.Size(94, 21);
            this.txtNewSellingPrice.TabIndex = 96;
            this.txtNewSellingPrice.Tag = "3";
            this.txtNewSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNewSellingPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewSellingPrice_KeyDown);
            // 
            // txtMrp
            // 
            this.txtMrp.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMrp.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMrp.Location = new System.Drawing.Point(910, 425);
            this.txtMrp.Name = "txtMrp";
            this.txtMrp.Size = new System.Drawing.Size(10, 21);
            this.txtMrp.TabIndex = 95;
            this.txtMrp.Tag = "3";
            this.txtMrp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMrp.Visible = false;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Location = new System.Drawing.Point(1078, 422);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(82, 21);
            this.txtTotalQty.TabIndex = 94;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(385, 379);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbUnit.TabIndex = 89;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 382);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 88;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // txtNewCostPrice
            // 
            this.txtNewCostPrice.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtNewCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNewCostPrice.Location = new System.Drawing.Point(1075, 379);
            this.txtNewCostPrice.Name = "txtNewCostPrice";
            this.txtNewCostPrice.Size = new System.Drawing.Size(10, 21);
            this.txtNewCostPrice.TabIndex = 86;
            this.txtNewCostPrice.Tag = "3";
            this.txtNewCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNewCostPrice.Visible = false;
            // 
            // pnlUpdateCostPrice
            // 
            this.pnlUpdateCostPrice.Controls.Add(this.chkIsIncrementCost);
            this.pnlUpdateCostPrice.Controls.Add(this.txtCostDiscount);
            this.pnlUpdateCostPrice.Controls.Add(this.btnUpdateCostPrice);
            this.pnlUpdateCostPrice.Controls.Add(this.label11);
            this.pnlUpdateCostPrice.Controls.Add(this.chkCostPercentage);
            this.pnlUpdateCostPrice.Controls.Add(this.label12);
            this.pnlUpdateCostPrice.Location = new System.Drawing.Point(538, 521);
            this.pnlUpdateCostPrice.Name = "pnlUpdateCostPrice";
            this.pnlUpdateCostPrice.Size = new System.Drawing.Size(396, 56);
            this.pnlUpdateCostPrice.TabIndex = 83;
            this.pnlUpdateCostPrice.Visible = false;
            // 
            // chkIsIncrementCost
            // 
            this.chkIsIncrementCost.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsIncrementCost.AutoSize = true;
            this.chkIsIncrementCost.BackColor = System.Drawing.Color.NavajoWhite;
            this.chkIsIncrementCost.Location = new System.Drawing.Point(5, 11);
            this.chkIsIncrementCost.Margin = new System.Windows.Forms.Padding(6);
            this.chkIsIncrementCost.Name = "chkIsIncrementCost";
            this.chkIsIncrementCost.Size = new System.Drawing.Size(80, 23);
            this.chkIsIncrementCost.TabIndex = 98;
            this.chkIsIncrementCost.Text = "Decrement";
            this.chkIsIncrementCost.UseVisualStyleBackColor = false;
            this.chkIsIncrementCost.CheckedChanged += new System.EventHandler(this.chkIsIncrementCost_CheckedChanged);
            // 
            // txtCostDiscount
            // 
            this.txtCostDiscount.Location = new System.Drawing.Point(130, 13);
            this.txtCostDiscount.Name = "txtCostDiscount";
            this.txtCostDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostDiscount.Size = new System.Drawing.Size(141, 21);
            this.txtCostDiscount.TabIndex = 97;
            // 
            // btnUpdateCostPrice
            // 
            this.btnUpdateCostPrice.Location = new System.Drawing.Point(280, 12);
            this.btnUpdateCostPrice.Name = "btnUpdateCostPrice";
            this.btnUpdateCostPrice.Size = new System.Drawing.Size(110, 23);
            this.btnUpdateCostPrice.TabIndex = 76;
            this.btnUpdateCostPrice.Text = "Update All Items";
            this.btnUpdateCostPrice.UseVisualStyleBackColor = true;
            this.btnUpdateCostPrice.Click += new System.EventHandler(this.btnUpdateCostPrice_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 75;
            this.label11.Text = "%";
            // 
            // chkCostPercentage
            // 
            this.chkCostPercentage.AutoSize = true;
            this.chkCostPercentage.Location = new System.Drawing.Point(109, 16);
            this.chkCostPercentage.Name = "chkCostPercentage";
            this.chkCostPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkCostPercentage.TabIndex = 74;
            this.chkCostPercentage.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Discount";
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AllowUserToDeleteRows = false;
            this.dgvItemDetails.AllowUserToOrderColumns = true;
            this.dgvItemDetails.AllowUserToResizeColumns = false;
            this.dgvItemDetails.AllowUserToResizeRows = false;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNo,
            this.ProductId,
            this.ProductCode,
            this.ProductName,
            this.UOMID,
            this.UnitOfMeasureName,
            this.NewSellingPrice,
            this.NewProductId,
            this.NewProductCode,
            this.NewProductName,
            this.Qty,
            this.MRP,
            this.SellingPrice,
            this.CostPrice,
            this.NewCostPrice,
            this.LocationIdx,
            this.LocationNamex,
            this.Type});
            this.dgvItemDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItemDetails.Location = new System.Drawing.Point(4, 79);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 15;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1159, 293);
            this.dgvItemDetails.TabIndex = 78;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // LineNo
            // 
            this.LineNo.DataPropertyName = "LineNo";
            this.LineNo.HeaderText = "Row";
            this.LineNo.Name = "LineNo";
            this.LineNo.Width = 40;
            // 
            // ProductId
            // 
            this.ProductId.DataPropertyName = "ProductID";
            this.ProductId.HeaderText = "Product Id";
            this.ProductId.Name = "ProductId";
            this.ProductId.Visible = false;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 120;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 200;
            // 
            // UOMID
            // 
            this.UOMID.DataPropertyName = "UnitOfMeasureID";
            this.UOMID.HeaderText = "UOMID";
            this.UOMID.Name = "UOMID";
            this.UOMID.Visible = false;
            // 
            // UnitOfMeasureName
            // 
            this.UnitOfMeasureName.DataPropertyName = "UnitOfMeasureName";
            this.UnitOfMeasureName.HeaderText = "Unit";
            this.UnitOfMeasureName.Name = "UnitOfMeasureName";
            this.UnitOfMeasureName.ReadOnly = true;
            this.UnitOfMeasureName.Width = 55;
            // 
            // NewSellingPrice
            // 
            this.NewSellingPrice.DataPropertyName = "NewSellingPrice";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NewSellingPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.NewSellingPrice.HeaderText = "New Selling";
            this.NewSellingPrice.Name = "NewSellingPrice";
            this.NewSellingPrice.Width = 90;
            // 
            // NewProductId
            // 
            this.NewProductId.DataPropertyName = "NewProductId";
            this.NewProductId.HeaderText = "NewProductId";
            this.NewProductId.Name = "NewProductId";
            this.NewProductId.ReadOnly = true;
            this.NewProductId.Visible = false;
            this.NewProductId.Width = 120;
            // 
            // NewProductCode
            // 
            this.NewProductCode.DataPropertyName = "NewProductCode";
            this.NewProductCode.HeaderText = "New Product Code";
            this.NewProductCode.Name = "NewProductCode";
            this.NewProductCode.Width = 150;
            // 
            // NewProductName
            // 
            this.NewProductName.DataPropertyName = "NewProductName";
            this.NewProductName.HeaderText = "New Product Name";
            this.NewProductName.Name = "NewProductName";
            this.NewProductName.Width = 150;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle8;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 50;
            // 
            // MRP
            // 
            this.MRP.DataPropertyName = "MRP";
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.Visible = false;
            this.MRP.Width = 90;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Width = 90;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 90;
            // 
            // NewCostPrice
            // 
            this.NewCostPrice.DataPropertyName = "NewCostPrice";
            this.NewCostPrice.HeaderText = "New Cost";
            this.NewCostPrice.Name = "NewCostPrice";
            this.NewCostPrice.Visible = false;
            this.NewCostPrice.Width = 90;
            // 
            // LocationIdx
            // 
            this.LocationIdx.DataPropertyName = "LocationId";
            this.LocationIdx.HeaderText = "Location Id";
            this.LocationIdx.Name = "LocationIdx";
            this.LocationIdx.Visible = false;
            // 
            // LocationNamex
            // 
            this.LocationNamex.DataPropertyName = "LocationName";
            this.LocationNamex.HeaderText = "Location";
            this.LocationNamex.Name = "LocationNamex";
            this.LocationNamex.Visible = false;
            this.LocationNamex.Width = 80;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "DamageType";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // pnlUpdateSellingPrice
            // 
            this.pnlUpdateSellingPrice.Controls.Add(this.label4);
            this.pnlUpdateSellingPrice.Controls.Add(this.label3);
            this.pnlUpdateSellingPrice.Controls.Add(this.txtNewUProName);
            this.pnlUpdateSellingPrice.Controls.Add(this.txtNewUProduct);
            this.pnlUpdateSellingPrice.Controls.Add(this.txtSellingDiscount);
            this.pnlUpdateSellingPrice.Controls.Add(this.btnUpdateSellingPrice);
            this.pnlUpdateSellingPrice.Location = new System.Drawing.Point(15, 414);
            this.pnlUpdateSellingPrice.Name = "pnlUpdateSellingPrice";
            this.pnlUpdateSellingPrice.Size = new System.Drawing.Size(587, 71);
            this.pnlUpdateSellingPrice.TabIndex = 66;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 115;
            this.label4.Text = "Product";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 114;
            this.label3.Text = "New Selling";
            // 
            // txtNewUProName
            // 
            this.txtNewUProName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNewUProName.Location = new System.Drawing.Point(311, 36);
            this.txtNewUProName.Name = "txtNewUProName";
            this.txtNewUProName.Size = new System.Drawing.Size(146, 21);
            this.txtNewUProName.TabIndex = 108;
            // 
            // txtNewUProduct
            // 
            this.txtNewUProduct.Location = new System.Drawing.Point(162, 36);
            this.txtNewUProduct.Name = "txtNewUProduct";
            this.txtNewUProduct.Size = new System.Drawing.Size(147, 21);
            this.txtNewUProduct.TabIndex = 107;
            // 
            // txtSellingDiscount
            // 
            this.txtSellingDiscount.Location = new System.Drawing.Point(15, 36);
            this.txtSellingDiscount.Name = "txtSellingDiscount";
            this.txtSellingDiscount.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingDiscount.Size = new System.Drawing.Size(141, 21);
            this.txtSellingDiscount.TabIndex = 96;
            this.txtSellingDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSellingDiscount_KeyDown);
            // 
            // btnUpdateSellingPrice
            // 
            this.btnUpdateSellingPrice.Location = new System.Drawing.Point(463, 34);
            this.btnUpdateSellingPrice.Name = "btnUpdateSellingPrice";
            this.btnUpdateSellingPrice.Size = new System.Drawing.Size(110, 23);
            this.btnUpdateSellingPrice.TabIndex = 76;
            this.btnUpdateSellingPrice.Text = "Update All Items";
            this.btnUpdateSellingPrice.UseVisualStyleBackColor = true;
            this.btnUpdateSellingPrice.Click += new System.EventHandler(this.btnUpdateItem_Click);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(885, 379);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(88, 21);
            this.txtSellingPrice.TabIndex = 80;
            this.txtSellingPrice.Tag = "3";
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNewProductCode
            // 
            this.txtNewProductCode.Location = new System.Drawing.Point(538, 379);
            this.txtNewProductCode.Name = "txtNewProductCode";
            this.txtNewProductCode.Size = new System.Drawing.Size(147, 21);
            this.txtNewProductCode.TabIndex = 74;
            this.txtNewProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewProductCode_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(835, 379);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(48, 21);
            this.txtQty.TabIndex = 70;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // txtProductName
            // 
            this.txtProductName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtProductName.Location = new System.Drawing.Point(174, 379);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(208, 21);
            this.txtProductName.TabIndex = 68;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(975, 379);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(86, 21);
            this.txtCostPrice.TabIndex = 72;
            this.txtCostPrice.Tag = "3";
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(21, 379);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(151, 21);
            this.txtProductCode.TabIndex = 67;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // dtpEffectivDate
            // 
            this.dtpEffectivDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffectivDate.Location = new System.Drawing.Point(316, 79);
            this.dtpEffectivDate.Name = "dtpEffectivDate";
            this.dtpEffectivDate.Size = new System.Drawing.Size(109, 21);
            this.dtpEffectivDate.TabIndex = 73;
            this.dtpEffectivDate.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 72;
            this.label8.Text = "Effective Date";
            this.label8.Visible = false;
            // 
            // chkUpdateCostPrice
            // 
            this.chkUpdateCostPrice.AutoSize = true;
            this.chkUpdateCostPrice.Location = new System.Drawing.Point(445, 470);
            this.chkUpdateCostPrice.Name = "chkUpdateCostPrice";
            this.chkUpdateCostPrice.Size = new System.Drawing.Size(128, 17);
            this.chkUpdateCostPrice.TabIndex = 84;
            this.chkUpdateCostPrice.Text = "Update Cost Price";
            this.chkUpdateCostPrice.UseVisualStyleBackColor = true;
            this.chkUpdateCostPrice.Visible = false;
            this.chkUpdateCostPrice.CheckedChanged += new System.EventHandler(this.chkUpdateCostPrice_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(210, 453);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(73, 23);
            this.btnNext.TabIndex = 43;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(131, 453);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(73, 23);
            this.btnBack.TabIndex = 42;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            // 
            // FrmProductPriceChangeDamage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 572);
            this.Controls.Add(this.panel1);
            this.Name = "FrmProductPriceChangeDamage";
            this.Text = "Temp Location - Reduce";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabCritaria.ResumeLayout(false);
            this.tpCritaria.ResumeLayout(false);
            this.tpCritaria.PerformLayout();
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            this.pnlUpdateCostPrice.ResumeLayout(false);
            this.pnlUpdateCostPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.pnlUpdateSellingPrice.ResumeLayout(false);
            this.pnlUpdateSellingPrice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCritaria;
        private System.Windows.Forms.TabPage tpCritaria;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlProduct;
        private System.Windows.Forms.CheckBox chkTStatus;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpEffectivDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAutoCompleationPoNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblPPChange;
        private CustomControls.TextBoxCurrency txtNewSellingPrice;
        private CustomControls.TextBoxCurrency txtMrp;
        private CustomControls.TextBoxNumeric txtTotalQty;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private CustomControls.TextBoxCurrency txtNewCostPrice;
        private System.Windows.Forms.CheckBox chkUpdateCostPrice;
        private System.Windows.Forms.Panel pnlUpdateCostPrice;
        private System.Windows.Forms.CheckBox chkIsIncrementCost;
        private CustomControls.TextBoxNumeric txtCostDiscount;
        private System.Windows.Forms.Button btnUpdateCostPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkCostPercentage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.Panel pnlUpdateSellingPrice;
        private CustomControls.TextBoxNumeric txtSellingDiscount;
        private System.Windows.Forms.Button btnUpdateSellingPrice;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.TextBox txtNewProductCode;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.TextBox txtNewProductName;
        private System.Windows.Forms.ComboBox cmbFromLocation;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.Button btnTogDetails;
        private System.Windows.Forms.ComboBox cmbTogDocument;
        private System.Windows.Forms.Label lblPOSDocuments;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDamageType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasureName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewSellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationNamex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewUProName;
        private System.Windows.Forms.TextBox txtNewUProduct;
    }
}