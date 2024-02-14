namespace UI.Windows
{
    partial class FrmMaterialRequestNote
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
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.tbFooter = new System.Windows.Forms.TabControl();
            this.tbpPageSetup = new System.Windows.Forms.TabPage();
            this.chkLandscape = new System.Windows.Forms.RadioButton();
            this.chkPortrait = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.tbpOtherDetails = new System.Windows.Forms.TabPage();
            this.cmbCostCentre = new System.Windows.Forms.ComboBox();
            this.lblCostCentre = new System.Windows.Forms.Label();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.txtValidityPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblValidityPeriod = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationMrnNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtProductAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.lineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgsMaterialRequestDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialRequestDetailTempBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(605, 452);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 452);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.label2);
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Controls.Add(this.tbFooter);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Location = new System.Drawing.Point(1, 331);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(921, 125);
            this.grpFooter.TabIndex = 20;
            this.grpFooter.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(709, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.ForeColor = System.Drawing.Color.Firebrick;
            this.txtTotalAmount.Location = new System.Drawing.Point(791, 21);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(126, 21);
            this.txtTotalAmount.TabIndex = 59;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(709, 47);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(58, 13);
            this.lblTotalQty.TabIndex = 35;
            this.lblTotalQty.Text = "Total Qty";
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Controls.Add(this.tbpOtherDetails);
            this.tbFooter.Location = new System.Drawing.Point(5, 10);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(476, 112);
            this.tbFooter.TabIndex = 34;
            // 
            // tbpPageSetup
            // 
            this.tbpPageSetup.Controls.Add(this.chkLandscape);
            this.tbpPageSetup.Controls.Add(this.chkPortrait);
            this.tbpPageSetup.Controls.Add(this.cmbPaperSize);
            this.tbpPageSetup.Controls.Add(this.lblOrientation);
            this.tbpPageSetup.Controls.Add(this.cmbPrinter);
            this.tbpPageSetup.Controls.Add(this.lblPaperSize);
            this.tbpPageSetup.Controls.Add(this.lblPrinter);
            this.tbpPageSetup.Location = new System.Drawing.Point(4, 22);
            this.tbpPageSetup.Name = "tbpPageSetup";
            this.tbpPageSetup.Size = new System.Drawing.Size(468, 86);
            this.tbpPageSetup.TabIndex = 2;
            this.tbpPageSetup.Text = "Page Setup";
            this.tbpPageSetup.UseVisualStyleBackColor = true;
            // 
            // chkLandscape
            // 
            this.chkLandscape.AutoSize = true;
            this.chkLandscape.Location = new System.Drawing.Point(234, 59);
            this.chkLandscape.Name = "chkLandscape";
            this.chkLandscape.Size = new System.Drawing.Size(85, 17);
            this.chkLandscape.TabIndex = 51;
            this.chkLandscape.TabStop = true;
            this.chkLandscape.Text = "Landscape";
            this.chkLandscape.UseVisualStyleBackColor = true;
            // 
            // chkPortrait
            // 
            this.chkPortrait.AutoSize = true;
            this.chkPortrait.Location = new System.Drawing.Point(125, 59);
            this.chkPortrait.Name = "chkPortrait";
            this.chkPortrait.Size = new System.Drawing.Size(67, 17);
            this.chkPortrait.TabIndex = 50;
            this.chkPortrait.TabStop = true;
            this.chkPortrait.Text = "Portrait";
            this.chkPortrait.UseVisualStyleBackColor = true;
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Location = new System.Drawing.Point(125, 32);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(207, 21);
            this.cmbPaperSize.TabIndex = 49;
            // 
            // lblOrientation
            // 
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new System.Drawing.Point(9, 61);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(70, 13);
            this.lblOrientation.TabIndex = 48;
            this.lblOrientation.Text = "Orientation";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(125, 7);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(207, 21);
            this.cmbPrinter.TabIndex = 47;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(9, 35);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(68, 13);
            this.lblPaperSize.TabIndex = 46;
            this.lblPaperSize.Text = "Paper Size";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(9, 10);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(45, 13);
            this.lblPrinter.TabIndex = 45;
            this.lblPrinter.Text = "Printer";
            // 
            // tbpOtherDetails
            // 
            this.tbpOtherDetails.Controls.Add(this.cmbCostCentre);
            this.tbpOtherDetails.Controls.Add(this.lblCostCentre);
            this.tbpOtherDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpOtherDetails.Name = "tbpOtherDetails";
            this.tbpOtherDetails.Size = new System.Drawing.Size(468, 86);
            this.tbpOtherDetails.TabIndex = 4;
            this.tbpOtherDetails.Text = "Other Details";
            this.tbpOtherDetails.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
            this.cmbCostCentre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCostCentre.FormattingEnabled = true;
            this.cmbCostCentre.Location = new System.Drawing.Point(102, 5);
            this.cmbCostCentre.Name = "cmbCostCentre";
            this.cmbCostCentre.Size = new System.Drawing.Size(225, 21);
            this.cmbCostCentre.TabIndex = 67;
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.AutoSize = true;
            this.lblCostCentre.Location = new System.Drawing.Point(10, 8);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(76, 13);
            this.lblCostCentre.TabIndex = 66;
            this.lblCostCentre.Text = "Cost Centre";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Firebrick;
            this.txtTotalQty.Location = new System.Drawing.Point(791, 44);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(126, 21);
            this.txtTotalQty.TabIndex = 30;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(809, 61);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(106, 17);
            this.chkOverwrite.TabIndex = 101;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite Qty";
            this.chkOverwrite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtValidityPeriod);
            this.grpHeader.Controls.Add(this.chkOverwrite);
            this.grpHeader.Controls.Add(this.lblValidityPeriod);
            this.grpHeader.Controls.Add(this.dtpDeliveryDate);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblRequestDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationMrnNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(921, 81);
            this.grpHeader.TabIndex = 19;
            this.grpHeader.TabStop = false;
            // 
            // txtValidityPeriod
            // 
            this.txtValidityPeriod.IntValue = 7;
            this.txtValidityPeriod.Location = new System.Drawing.Point(404, 11);
            this.txtValidityPeriod.Name = "txtValidityPeriod";
            this.txtValidityPeriod.Size = new System.Drawing.Size(100, 21);
            this.txtValidityPeriod.TabIndex = 100;
            this.txtValidityPeriod.Text = "7";
            this.txtValidityPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValidityPeriod
            // 
            this.lblValidityPeriod.AutoSize = true;
            this.lblValidityPeriod.Location = new System.Drawing.Point(287, 15);
            this.lblValidityPeriod.Name = "lblValidityPeriod";
            this.lblValidityPeriod.Size = new System.Drawing.Size(111, 13);
            this.lblValidityPeriod.TabIndex = 99;
            this.lblValidityPeriod.Text = "Validity Period (D)";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(602, 12);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(111, 21);
            this.dtpDeliveryDate.TabIndex = 65;
            this.dtpDeliveryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDeliveryDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(510, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Delivery Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(602, 36);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(313, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(510, 39);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(112, 34);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(392, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 37);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(804, 11);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(111, 21);
            this.dtpDocumentDate.TabIndex = 22;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(254, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 58);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 57);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(392, 21);
            this.txtRemark.TabIndex = 18;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSize = true;
            this.lblRequestDate.Location = new System.Drawing.Point(714, 16);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(63, 13);
            this.lblRequestDate.TabIndex = 9;
            this.lblRequestDate.Text = "MRN Date";
            // 
            // chkAutoCompleationMrnNo
            // 
            this.chkAutoCompleationMrnNo.AutoSize = true;
            this.chkAutoCompleationMrnNo.Checked = true;
            this.chkAutoCompleationMrnNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationMrnNo.Location = new System.Drawing.Point(95, 14);
            this.chkAutoCompleationMrnNo.Name = "chkAutoCompleationMrnNo";
            this.chkAutoCompleationMrnNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationMrnNo.TabIndex = 4;
            this.chkAutoCompleationMrnNo.Tag = "1";
            this.chkAutoCompleationMrnNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationMrnNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationMrnNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(141, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Validated += new System.EventHandler(this.txtDocumentNo_Validated);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 15);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtProductAmount);
            this.grpBody.Controls.Add(this.txtCostPrice);
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Location = new System.Drawing.Point(1, 69);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(921, 266);
            this.grpBody.TabIndex = 21;
            this.grpBody.TabStop = false;
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtProductAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductAmount.Location = new System.Drawing.Point(791, 241);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(126, 21);
            this.txtProductAmount.TabIndex = 58;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(678, 241);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.ReadOnly = true;
            this.txtCostPrice.Size = new System.Drawing.Size(112, 21);
            this.txtCostPrice.TabIndex = 57;
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostPrice_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(611, 241);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(66, 21);
            this.txtQty.TabIndex = 56;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(556, 241);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(54, 21);
            this.cmbUnit.TabIndex = 55;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(214, 241);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(341, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 244);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 47;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AutoGenerateColumns = false;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNo,
            this.productCode,
            this.productName,
            this.UnitOfMeasure,
            this.orderQty,
            this.CostPrice,
            this.NetAmount});
            this.dgvItemDetails.DataSource = this.lgsMaterialRequestDetailTempBindingSource;
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 12);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.ReadOnly = true;
            this.dgvItemDetails.RowHeadersWidth = 10;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(912, 225);
            this.dgvItemDetails.TabIndex = 45;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // lineNo
            // 
            this.lineNo.DataPropertyName = "LineNo";
            this.lineNo.HeaderText = "Row";
            this.lineNo.Name = "lineNo";
            this.lineNo.ReadOnly = true;
            this.lineNo.Width = 40;
            // 
            // productCode
            // 
            this.productCode.DataPropertyName = "ProductCode";
            this.productCode.HeaderText = "Product Code";
            this.productCode.Name = "productCode";
            this.productCode.ReadOnly = true;
            this.productCode.Width = 150;
            // 
            // productName
            // 
            this.productName.DataPropertyName = "ProductName";
            this.productName.HeaderText = "Product Name";
            this.productName.Name = "productName";
            this.productName.ReadOnly = true;
            this.productName.Width = 350;
            // 
            // UnitOfMeasure
            // 
            this.UnitOfMeasure.DataPropertyName = "UnitOfMeasure";
            this.UnitOfMeasure.HeaderText = "Unit";
            this.UnitOfMeasure.Name = "UnitOfMeasure";
            this.UnitOfMeasure.ReadOnly = true;
            this.UnitOfMeasure.Width = 50;
            // 
            // orderQty
            // 
            this.orderQty.DataPropertyName = "OrderQty";
            this.orderQty.HeaderText = "Qty";
            this.orderQty.Name = "orderQty";
            this.orderQty.ReadOnly = true;
            this.orderQty.Width = 70;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 110;
            // 
            // NetAmount
            // 
            this.NetAmount.DataPropertyName = "NetAmount";
            this.NetAmount.HeaderText = "Amount";
            this.NetAmount.Name = "NetAmount";
            this.NetAmount.ReadOnly = true;
            this.NetAmount.Width = 110;
            // 
            // lgsMaterialRequestDetailTempBindingSource
            // 
            this.lgsMaterialRequestDetailTempBindingSource.DataSource = typeof(Domain.LgsMaterialRequestDetailTemp);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 241);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(191, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // FrmMaterialRequestNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(924, 500);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmMaterialRequestNote";
            this.Text = "Material Request Note";
            this.Activated += new System.EventHandler(this.FrmMaterialRequestNote_Activated);
            this.Deactivate += new System.EventHandler(this.FrmMaterialRequestNote_Deactivate);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.tbFooter.ResumeLayout(false);
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialRequestDetailTempBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TabControl tbFooter;
        private System.Windows.Forms.TabPage tbpPageSetup;
        private System.Windows.Forms.RadioButton chkLandscape;
        private System.Windows.Forms.RadioButton chkPortrait;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblOrientation;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.TabPage tbpOtherDetails;
        private System.Windows.Forms.ComboBox cmbCostCentre;
        private System.Windows.Forms.Label lblCostCentre;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRequestDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationMrnNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource lgsMaterialRequestDetailTempBindingSource;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn productCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private CustomControls.TextBoxCurrency txtProductAmount;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxCurrency txtTotalAmount;
        private CustomControls.TextBoxInteger txtValidityPeriod;
        private System.Windows.Forms.Label lblValidityPeriod;
    }
}
