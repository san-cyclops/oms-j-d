namespace UI.Windows
{
    partial class FrmLogisticQuotation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.cmbPaymentTerms = new System.Windows.Forms.ComboBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpQuotationDate = new System.Windows.Forms.DateTimePicker();
            this.btnInvoiceDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblSalesPerson = new System.Windows.Forms.Label();
            this.txtSalesPersonName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationSalesPerson = new System.Windows.Forms.CheckBox();
            this.txtSalesPersonCode = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationVendor = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblQuotationDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationQuotationNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblQuotationNo = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.btnTaxBreakdown = new System.Windows.Forms.Button();
            this.chkTaxEnable = new System.Windows.Forms.CheckBox();
            this.lblOtherCharges = new System.Windows.Forms.Label();
            this.txtOtherCharges = new System.Windows.Forms.TextBox();
            this.lblSubTotalDiscountPecentage = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.chkSubTotalDiscountPercentage = new System.Windows.Forms.CheckBox();
            this.txtSubTotalDiscountPercentage = new System.Windows.Forms.TextBox();
            this.lblSubTotalDiscount = new System.Windows.Forms.Label();
            this.lblGrossAmount = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtSubTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtGrossAmount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductAmount = new System.Windows.Forms.TextBox();
            this.txtProductDiscountAmount = new System.Windows.Forms.TextBox();
            this.txtProductDiscountPercentage = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPersentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(702, 471);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 471);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.cmbPaymentTerms);
            this.grpHeader.Controls.Add(this.lblPaymentTerms);
            this.grpHeader.Controls.Add(this.dtpDeliveryDate);
            this.grpHeader.Controls.Add(this.lblDeliveryDate);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpQuotationDate);
            this.grpHeader.Controls.Add(this.btnInvoiceDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationSalesPerson);
            this.grpHeader.Controls.Add(this.txtSalesPersonCode);
            this.grpHeader.Controls.Add(this.lblSupplier);
            this.grpHeader.Controls.Add(this.txtSupplierName);
            this.grpHeader.Controls.Add(this.chkAutoCompleationVendor);
            this.grpHeader.Controls.Add(this.txtSupplierCode);
            this.grpHeader.Controls.Add(this.lblQuotationDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationQuotationNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblQuotationNo);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1017, 110);
            this.grpHeader.TabIndex = 16;
            this.grpHeader.TabStop = false;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(907, 61);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 76;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // cmbPaymentTerms
            // 
            this.cmbPaymentTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTerms.FormattingEnabled = true;
            this.cmbPaymentTerms.Location = new System.Drawing.Point(771, 60);
            this.cmbPaymentTerms.Name = "cmbPaymentTerms";
            this.cmbPaymentTerms.Size = new System.Drawing.Size(124, 21);
            this.cmbPaymentTerms.TabIndex = 74;
            this.cmbPaymentTerms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentTerms_KeyDown);
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Location = new System.Drawing.Point(659, 63);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(96, 13);
            this.lblPaymentTerms.TabIndex = 75;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(771, 36);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(242, 21);
            this.dtpDeliveryDate.TabIndex = 73;
            this.dtpDeliveryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDeliveryDate_KeyDown);
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Location = new System.Drawing.Point(659, 42);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(86, 13);
            this.lblDeliveryDate.TabIndex = 72;
            this.lblDeliveryDate.Text = "Delivery Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(771, 84);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(242, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(659, 87);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 64;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(378, 13);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(215, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(289, 15);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpQuotationDate
            // 
            this.dtpQuotationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpQuotationDate.Location = new System.Drawing.Point(771, 12);
            this.dtpQuotationDate.Name = "dtpQuotationDate";
            this.dtpQuotationDate.Size = new System.Drawing.Size(242, 21);
            this.dtpQuotationDate.TabIndex = 22;
            this.dtpQuotationDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpQuotationDate_KeyDown);
            // 
            // btnInvoiceDetails
            // 
            this.btnInvoiceDetails.Location = new System.Drawing.Point(244, 11);
            this.btnInvoiceDetails.Name = "btnInvoiceDetails";
            this.btnInvoiceDetails.Size = new System.Drawing.Size(28, 23);
            this.btnInvoiceDetails.TabIndex = 21;
            this.btnInvoiceDetails.Text = "...";
            this.btnInvoiceDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 87);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(107, 84);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(486, 21);
            this.txtRemark.TabIndex = 18;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblSalesPerson
            // 
            this.lblSalesPerson.AutoSize = true;
            this.lblSalesPerson.Location = new System.Drawing.Point(6, 63);
            this.lblSalesPerson.Name = "lblSalesPerson";
            this.lblSalesPerson.Size = new System.Drawing.Size(81, 13);
            this.lblSalesPerson.TabIndex = 17;
            this.lblSalesPerson.Text = "Sales Person";
            // 
            // txtSalesPersonName
            // 
            this.txtSalesPersonName.Location = new System.Drawing.Point(244, 60);
            this.txtSalesPersonName.Name = "txtSalesPersonName";
            this.txtSalesPersonName.Size = new System.Drawing.Size(349, 21);
            this.txtSalesPersonName.TabIndex = 16;
            this.txtSalesPersonName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonName_KeyDown);
            this.txtSalesPersonName.Leave += new System.EventHandler(this.txtSalesPersonName_Leave);
            // 
            // chkAutoCompleationSalesPerson
            // 
            this.chkAutoCompleationSalesPerson.AutoSize = true;
            this.chkAutoCompleationSalesPerson.Checked = true;
            this.chkAutoCompleationSalesPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSalesPerson.Location = new System.Drawing.Point(90, 63);
            this.chkAutoCompleationSalesPerson.Name = "chkAutoCompleationSalesPerson";
            this.chkAutoCompleationSalesPerson.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSalesPerson.TabIndex = 15;
            this.chkAutoCompleationSalesPerson.Tag = "1";
            this.chkAutoCompleationSalesPerson.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSalesPerson.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSalesPerson_CheckedChanged);
            // 
            // txtSalesPersonCode
            // 
            this.txtSalesPersonCode.Location = new System.Drawing.Point(107, 60);
            this.txtSalesPersonCode.Name = "txtSalesPersonCode";
            this.txtSalesPersonCode.Size = new System.Drawing.Size(136, 21);
            this.txtSalesPersonCode.TabIndex = 14;
            this.txtSalesPersonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesPersonCode_KeyDown);
            this.txtSalesPersonCode.Leave += new System.EventHandler(this.txtSalesPersonCode_Leave);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(6, 39);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(54, 13);
            this.lblSupplier.TabIndex = 13;
            this.lblSupplier.Text = "Supplier";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(244, 36);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(349, 21);
            this.txtSupplierName.TabIndex = 12;
            this.txtSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorName_KeyDown);
            this.txtSupplierName.Leave += new System.EventHandler(this.txtVendorName_Leave);
            // 
            // chkAutoCompleationVendor
            // 
            this.chkAutoCompleationVendor.AutoSize = true;
            this.chkAutoCompleationVendor.Checked = true;
            this.chkAutoCompleationVendor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationVendor.Location = new System.Drawing.Point(90, 39);
            this.chkAutoCompleationVendor.Name = "chkAutoCompleationVendor";
            this.chkAutoCompleationVendor.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationVendor.TabIndex = 11;
            this.chkAutoCompleationVendor.Tag = "1";
            this.chkAutoCompleationVendor.UseVisualStyleBackColor = true;
            this.chkAutoCompleationVendor.CheckedChanged += new System.EventHandler(this.chkAutoCompleationVendor_CheckedChanged);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(107, 36);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(136, 21);
            this.txtSupplierCode.TabIndex = 10;
            this.txtSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierCode_KeyDown);
            this.txtSupplierCode.Leave += new System.EventHandler(this.txtSupplierCode_Leave);
            // 
            // lblQuotationDate
            // 
            this.lblQuotationDate.AutoSize = true;
            this.lblQuotationDate.Location = new System.Drawing.Point(659, 18);
            this.lblQuotationDate.Name = "lblQuotationDate";
            this.lblQuotationDate.Size = new System.Drawing.Size(93, 13);
            this.lblQuotationDate.TabIndex = 9;
            this.lblQuotationDate.Text = "Quotation Date";
            // 
            // chkAutoCompleationQuotationNo
            // 
            this.chkAutoCompleationQuotationNo.AutoSize = true;
            this.chkAutoCompleationQuotationNo.Checked = true;
            this.chkAutoCompleationQuotationNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationQuotationNo.Location = new System.Drawing.Point(90, 15);
            this.chkAutoCompleationQuotationNo.Name = "chkAutoCompleationQuotationNo";
            this.chkAutoCompleationQuotationNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationQuotationNo.TabIndex = 4;
            this.chkAutoCompleationQuotationNo.Tag = "1";
            this.chkAutoCompleationQuotationNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationQuotationNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationQuotationNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(107, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // lblQuotationNo
            // 
            this.lblQuotationNo.AutoSize = true;
            this.lblQuotationNo.Location = new System.Drawing.Point(6, 16);
            this.lblQuotationNo.Name = "lblQuotationNo";
            this.lblQuotationNo.Size = new System.Drawing.Size(81, 13);
            this.lblQuotationNo.TabIndex = 0;
            this.lblQuotationNo.Text = "Quotation No";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.btnTaxBreakdown);
            this.grpFooter.Controls.Add(this.chkTaxEnable);
            this.grpFooter.Controls.Add(this.lblOtherCharges);
            this.grpFooter.Controls.Add(this.txtOtherCharges);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscountPecentage);
            this.grpFooter.Controls.Add(this.lblNetAmount);
            this.grpFooter.Controls.Add(this.lblTotalTaxAmount);
            this.grpFooter.Controls.Add(this.chkSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.grpFooter.Controls.Add(this.lblSubTotalDiscount);
            this.grpFooter.Controls.Add(this.lblGrossAmount);
            this.grpFooter.Controls.Add(this.txtNetAmount);
            this.grpFooter.Controls.Add(this.txtTotalTaxAmount);
            this.grpFooter.Controls.Add(this.txtSubTotalDiscount);
            this.grpFooter.Controls.Add(this.txtGrossAmount);
            this.grpFooter.Location = new System.Drawing.Point(702, 319);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(317, 157);
            this.grpFooter.TabIndex = 17;
            this.grpFooter.TabStop = false;
            // 
            // btnTaxBreakdown
            // 
            this.btnTaxBreakdown.ForeColor = System.Drawing.Color.Black;
            this.btnTaxBreakdown.Location = new System.Drawing.Point(8, 82);
            this.btnTaxBreakdown.Name = "btnTaxBreakdown";
            this.btnTaxBreakdown.Size = new System.Drawing.Size(17, 21);
            this.btnTaxBreakdown.TabIndex = 89;
            this.btnTaxBreakdown.Text = "?";
            this.btnTaxBreakdown.UseVisualStyleBackColor = true;
            this.btnTaxBreakdown.Click += new System.EventHandler(this.btnTaxBreakdown_Click);
            // 
            // chkTaxEnable
            // 
            this.chkTaxEnable.AutoSize = true;
            this.chkTaxEnable.Location = new System.Drawing.Point(146, 86);
            this.chkTaxEnable.Name = "chkTaxEnable";
            this.chkTaxEnable.Size = new System.Drawing.Size(15, 14);
            this.chkTaxEnable.TabIndex = 88;
            this.chkTaxEnable.UseVisualStyleBackColor = true;
            this.chkTaxEnable.CheckedChanged += new System.EventHandler(this.chkTaxEnable_CheckedChanged);
            // 
            // lblOtherCharges
            // 
            this.lblOtherCharges.AutoSize = true;
            this.lblOtherCharges.Location = new System.Drawing.Point(31, 110);
            this.lblOtherCharges.Name = "lblOtherCharges";
            this.lblOtherCharges.Size = new System.Drawing.Size(91, 13);
            this.lblOtherCharges.TabIndex = 57;
            this.lblOtherCharges.Text = "Other Charges";
            // 
            // txtOtherCharges
            // 
            this.txtOtherCharges.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtOtherCharges.Location = new System.Drawing.Point(170, 107);
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Size = new System.Drawing.Size(140, 21);
            this.txtOtherCharges.TabIndex = 56;
            this.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherCharges.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyUp);
            // 
            // lblSubTotalDiscountPecentage
            // 
            this.lblSubTotalDiscountPecentage.AutoSize = true;
            this.lblSubTotalDiscountPecentage.Location = new System.Drawing.Point(124, 38);
            this.lblSubTotalDiscountPecentage.Name = "lblSubTotalDiscountPecentage";
            this.lblSubTotalDiscountPecentage.Size = new System.Drawing.Size(19, 13);
            this.lblSubTotalDiscountPecentage.TabIndex = 53;
            this.lblSubTotalDiscountPecentage.Text = "%";
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmount.Location = new System.Drawing.Point(30, 134);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(83, 13);
            this.lblNetAmount.TabIndex = 40;
            this.lblNetAmount.Text = "Net Amount";
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(30, 86);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(106, 13);
            this.lblTotalTaxAmount.TabIndex = 39;
            this.lblTotalTaxAmount.Text = "Total Tax Amount";
            // 
            // chkSubTotalDiscountPercentage
            // 
            this.chkSubTotalDiscountPercentage.AutoSize = true;
            this.chkSubTotalDiscountPercentage.Location = new System.Drawing.Point(146, 38);
            this.chkSubTotalDiscountPercentage.Name = "chkSubTotalDiscountPercentage";
            this.chkSubTotalDiscountPercentage.Size = new System.Drawing.Size(15, 14);
            this.chkSubTotalDiscountPercentage.TabIndex = 38;
            this.chkSubTotalDiscountPercentage.UseVisualStyleBackColor = true;
            this.chkSubTotalDiscountPercentage.CheckedChanged += new System.EventHandler(this.chkSubTotalDiscountPercentage_CheckedChanged);
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(170, 35);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(140, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 37;
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotalDiscountPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSubTotalDiscountPercentage_KeyUp);
            this.txtSubTotalDiscountPercentage.Leave += new System.EventHandler(this.txtSubTotalDiscountPercentage_Leave);
            // 
            // lblSubTotalDiscount
            // 
            this.lblSubTotalDiscount.AutoSize = true;
            this.lblSubTotalDiscount.Location = new System.Drawing.Point(31, 38);
            this.lblSubTotalDiscount.Name = "lblSubTotalDiscount";
            this.lblSubTotalDiscount.Size = new System.Drawing.Size(56, 13);
            this.lblSubTotalDiscount.TabIndex = 36;
            this.lblSubTotalDiscount.Text = "Discount";
            // 
            // lblGrossAmount
            // 
            this.lblGrossAmount.AutoSize = true;
            this.lblGrossAmount.Location = new System.Drawing.Point(31, 14);
            this.lblGrossAmount.Name = "lblGrossAmount";
            this.lblGrossAmount.Size = new System.Drawing.Size(88, 13);
            this.lblGrossAmount.TabIndex = 35;
            this.lblGrossAmount.Text = "Gross Amount";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtNetAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetAmount.Location = new System.Drawing.Point(170, 131);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(140, 21);
            this.txtNetAmount.TabIndex = 33;
            this.txtNetAmount.Text = "0.00";
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(170, 83);
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(140, 21);
            this.txtTotalTaxAmount.TabIndex = 32;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalTaxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalTaxAmount_KeyUp);
            // 
            // txtSubTotalDiscount
            // 
            this.txtSubTotalDiscount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSubTotalDiscount.Location = new System.Drawing.Point(170, 59);
            this.txtSubTotalDiscount.Name = "txtSubTotalDiscount";
            this.txtSubTotalDiscount.ReadOnly = true;
            this.txtSubTotalDiscount.Size = new System.Drawing.Size(140, 21);
            this.txtSubTotalDiscount.TabIndex = 31;
            this.txtSubTotalDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAmount
            // 
            this.txtGrossAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtGrossAmount.Location = new System.Drawing.Point(170, 11);
            this.txtGrossAmount.Name = "txtGrossAmount";
            this.txtGrossAmount.ReadOnly = true;
            this.txtGrossAmount.Size = new System.Drawing.Size(140, 21);
            this.txtGrossAmount.TabIndex = 30;
            this.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.txtProductAmount);
            this.groupBox1.Controls.Add(this.txtProductDiscountAmount);
            this.groupBox1.Controls.Add(this.txtProductDiscountPercentage);
            this.groupBox1.Controls.Add(this.txtRate);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProduct);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Location = new System.Drawing.Point(2, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1017, 224);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(513, 199);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(71, 21);
            this.cmbUnit.TabIndex = 59;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtProductAmount.Location = new System.Drawing.Point(870, 199);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(141, 21);
            this.txtProductAmount.TabIndex = 52;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            this.txtProductAmount.Leave += new System.EventHandler(this.txtProductAmount_Leave);
            // 
            // txtProductDiscountAmount
            // 
            this.txtProductDiscountAmount.Location = new System.Drawing.Point(800, 199);
            this.txtProductDiscountAmount.Name = "txtProductDiscountAmount";
            this.txtProductDiscountAmount.Size = new System.Drawing.Size(69, 21);
            this.txtProductDiscountAmount.TabIndex = 53;
            this.txtProductDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscountAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountAmount_KeyDown);
            this.txtProductDiscountAmount.Leave += new System.EventHandler(this.txtProductDiscountAmount_Leave);
            // 
            // txtProductDiscountPercentage
            // 
            this.txtProductDiscountPercentage.Location = new System.Drawing.Point(745, 199);
            this.txtProductDiscountPercentage.Name = "txtProductDiscountPercentage";
            this.txtProductDiscountPercentage.Size = new System.Drawing.Size(54, 21);
            this.txtProductDiscountPercentage.TabIndex = 54;
            this.txtProductDiscountPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscountPercentage_KeyDown);
            this.txtProductDiscountPercentage.Leave += new System.EventHandler(this.txtProductDiscountPercentage_Leave);
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtRate.Enabled = false;
            this.txtRate.Location = new System.Drawing.Point(652, 199);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(92, 21);
            this.txtRate.TabIndex = 55;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(585, 199);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(66, 21);
            this.txtQty.TabIndex = 57;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(233, 199);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(279, 21);
            this.txtProductName.TabIndex = 51;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(7, 202);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 50;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.Qty,
            this.Rate,
            this.DiscountPersentage,
            this.Discount,
            this.Amount});
            this.dgvItemDetails.Location = new System.Drawing.Point(3, 10);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 20;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1010, 186);
            this.dgvItemDetails.TabIndex = 48;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // Row
            // 
            this.Row.DataPropertyName = "LineNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Row.DefaultCellStyle = dataGridViewCellStyle1;
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 170;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 281;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 71;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 66;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "CostPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle6;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 92;
            // 
            // DiscountPersentage
            // 
            this.DiscountPersentage.DataPropertyName = "DiscountPercentage";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountPersentage.DefaultCellStyle = dataGridViewCellStyle7;
            this.DiscountPersentage.HeaderText = "Dis%";
            this.DiscountPersentage.Name = "DiscountPersentage";
            this.DiscountPersentage.ReadOnly = true;
            this.DiscountPersentage.Width = 52;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Discount.DefaultCellStyle = dataGridViewCellStyle8;
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 70;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle9;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 130;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(23, 199);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(209, 21);
            this.txtProductCode.TabIndex = 49;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // FrmLogisticQuotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1021, 519);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmLogisticQuotation";
            this.Text = "Supplier Quotation Entry";
            this.Load += new System.EventHandler(this.FrmLogisticQuotation_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.Button btnInvoiceDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblSalesPerson;
        private System.Windows.Forms.CheckBox chkAutoCompleationSalesPerson;
        private System.Windows.Forms.TextBox txtSalesPersonCode;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.CheckBox chkAutoCompleationVendor;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label lblQuotationDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationQuotationNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblQuotationNo;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Button btnTaxBreakdown;
        private System.Windows.Forms.CheckBox chkTaxEnable;
        private System.Windows.Forms.Label lblOtherCharges;
        private System.Windows.Forms.TextBox txtOtherCharges;
        private System.Windows.Forms.Label lblSubTotalDiscountPecentage;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.CheckBox chkSubTotalDiscountPercentage;
        private System.Windows.Forms.TextBox txtSubTotalDiscountPercentage;
        private System.Windows.Forms.Label lblSubTotalDiscount;
        private System.Windows.Forms.Label lblGrossAmount;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private System.Windows.Forms.TextBox txtSubTotalDiscount;
        private System.Windows.Forms.TextBox txtGrossAmount;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductAmount;
        private System.Windows.Forms.TextBox txtProductDiscountAmount;
        private System.Windows.Forms.TextBox txtProductDiscountPercentage;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.TextBox txtSalesPersonName;
        private System.Windows.Forms.DateTimePicker dtpQuotationDate;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPersentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;

    }
}
