namespace UI.Windows
{
    partial class FrmMaintenanceJobAssignNote
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJobDescription = new System.Windows.Forms.TextBox();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.BtnMaintenanceJobDetails = new System.Windows.Forms.Button();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblMaintenanceJobNo = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationJobNo = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationMjaNo = new System.Windows.Forms.CheckBox();
            this.txtMaintenanceJobRequestNo = new System.Windows.Forms.TextBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.lgsMaterialRequestDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tabAssignedDetails = new System.Windows.Forms.TabControl();
            this.tbpTechnician = new System.Windows.Forms.TabPage();
            this.txtEmployeeTitle = new System.Windows.Forms.TextBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.txtEmployeeCode = new System.Windows.Forms.TextBox();
            this.dgvEmployeeDetails = new System.Windows.Forms.DataGridView();
            this.lineNoDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgsMaintenanceJobAssignEmployeeDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbpProduct = new System.Windows.Forms.TabPage();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.lineNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.grpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialRequestDetailTempBindingSource)).BeginInit();
            this.grpBody.SuspendLayout();
            this.tabAssignedDetails.SuspendLayout();
            this.tbpTechnician.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaintenanceJobAssignEmployeeDetailTempBindingSource)).BeginInit();
            this.tbpProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(637, 494);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 494);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.tbFooter);
            this.grpFooter.Location = new System.Drawing.Point(1, 374);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(953, 125);
            this.grpFooter.TabIndex = 20;
            this.grpFooter.TabStop = false;
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
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.txtJobDescription);
            this.grpHeader.Controls.Add(this.dtpDeliveryDate);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.BtnMaintenanceJobDetails);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblMaintenanceJobNo);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblRequestDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationJobNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationMjaNo);
            this.grpHeader.Controls.Add(this.txtMaintenanceJobRequestNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(953, 106);
            this.grpHeader.TabIndex = 19;
            this.grpHeader.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(531, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Job Description";
            // 
            // txtJobDescription
            // 
            this.txtJobDescription.Location = new System.Drawing.Point(625, 57);
            this.txtJobDescription.Multiline = true;
            this.txtJobDescription.Name = "txtJobDescription";
            this.txtJobDescription.Size = new System.Drawing.Size(322, 44);
            this.txtJobDescription.TabIndex = 66;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(625, 12);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(111, 21);
            this.dtpDeliveryDate.TabIndex = 65;
            this.dtpDeliveryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDeliveryDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(531, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Delivery Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(625, 34);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(322, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(531, 38);
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
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 38);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(836, 11);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(111, 21);
            this.dtpDocumentDate.TabIndex = 22;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            // 
            // BtnMaintenanceJobDetails
            // 
            this.BtnMaintenanceJobDetails.Location = new System.Drawing.Point(504, 10);
            this.BtnMaintenanceJobDetails.Name = "BtnMaintenanceJobDetails";
            this.BtnMaintenanceJobDetails.Size = new System.Drawing.Size(28, 23);
            this.BtnMaintenanceJobDetails.TabIndex = 21;
            this.BtnMaintenanceJobDetails.Text = "...";
            this.BtnMaintenanceJobDetails.UseVisualStyleBackColor = true;
            this.BtnMaintenanceJobDetails.Click += new System.EventHandler(this.BtnMaintenanceJobDetails_Click);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(254, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblMaintenanceJobNo
            // 
            this.lblMaintenanceJobNo.AutoSize = true;
            this.lblMaintenanceJobNo.Location = new System.Drawing.Point(289, 15);
            this.lblMaintenanceJobNo.Name = "lblMaintenanceJobNo";
            this.lblMaintenanceJobNo.Size = new System.Drawing.Size(48, 13);
            this.lblMaintenanceJobNo.TabIndex = 19;
            this.lblMaintenanceJobNo.Text = "MJR No";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 60);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 56);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(392, 45);
            this.txtRemark.TabIndex = 18;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSize = true;
            this.lblRequestDate.Location = new System.Drawing.Point(767, 16);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(60, 13);
            this.lblRequestDate.TabIndex = 9;
            this.lblRequestDate.Text = "MJA Date";
            // 
            // chkAutoCompleationJobNo
            // 
            this.chkAutoCompleationJobNo.AutoSize = true;
            this.chkAutoCompleationJobNo.Checked = true;
            this.chkAutoCompleationJobNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationJobNo.Location = new System.Drawing.Point(341, 14);
            this.chkAutoCompleationJobNo.Name = "chkAutoCompleationJobNo";
            this.chkAutoCompleationJobNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationJobNo.TabIndex = 4;
            this.chkAutoCompleationJobNo.Tag = "1";
            this.chkAutoCompleationJobNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationJobNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationJobNo_CheckedChanged);
            // 
            // chkAutoCompleationMjaNo
            // 
            this.chkAutoCompleationMjaNo.AutoSize = true;
            this.chkAutoCompleationMjaNo.Checked = true;
            this.chkAutoCompleationMjaNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationMjaNo.Location = new System.Drawing.Point(95, 15);
            this.chkAutoCompleationMjaNo.Name = "chkAutoCompleationMjaNo";
            this.chkAutoCompleationMjaNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationMjaNo.TabIndex = 4;
            this.chkAutoCompleationMjaNo.Tag = "1";
            this.chkAutoCompleationMjaNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationMjaNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationMjaNo_CheckedChanged);
            // 
            // txtMaintenanceJobRequestNo
            // 
            this.txtMaintenanceJobRequestNo.Location = new System.Drawing.Point(358, 11);
            this.txtMaintenanceJobRequestNo.Name = "txtMaintenanceJobRequestNo";
            this.txtMaintenanceJobRequestNo.Size = new System.Drawing.Size(146, 21);
            this.txtMaintenanceJobRequestNo.TabIndex = 2;
            this.txtMaintenanceJobRequestNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaintenanceJobRequestNo_KeyDown);
            this.txtMaintenanceJobRequestNo.Validated += new System.EventHandler(this.txtMaintenanceJobRequestNo_Validated);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(141, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Validated += new System.EventHandler(this.txtDocumentNo_Validated);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // lgsMaterialRequestDetailTempBindingSource
            // 
            this.lgsMaterialRequestDetailTempBindingSource.DataSource = typeof(Domain.LgsMaintenanceJobAssignProductDetailTemp);
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tabAssignedDetails);
            this.grpBody.Location = new System.Drawing.Point(1, 94);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(953, 286);
            this.grpBody.TabIndex = 21;
            this.grpBody.TabStop = false;
            // 
            // tabAssignedDetails
            // 
            this.tabAssignedDetails.Controls.Add(this.tbpTechnician);
            this.tabAssignedDetails.Controls.Add(this.tbpProduct);
            this.tabAssignedDetails.Location = new System.Drawing.Point(5, 11);
            this.tabAssignedDetails.Name = "tabAssignedDetails";
            this.tabAssignedDetails.SelectedIndex = 0;
            this.tabAssignedDetails.Size = new System.Drawing.Size(944, 271);
            this.tabAssignedDetails.TabIndex = 0;
            this.tabAssignedDetails.SelectedIndexChanged += new System.EventHandler(this.tabAssignedDetails_SelectedIndexChanged);
            // 
            // tbpTechnician
            // 
            this.tbpTechnician.Controls.Add(this.txtEmployeeTitle);
            this.tbpTechnician.Controls.Add(this.txtEmployeeName);
            this.tbpTechnician.Controls.Add(this.chkAutoCompleationEmployee);
            this.tbpTechnician.Controls.Add(this.txtEmployeeCode);
            this.tbpTechnician.Controls.Add(this.dgvEmployeeDetails);
            this.tbpTechnician.Location = new System.Drawing.Point(4, 22);
            this.tbpTechnician.Name = "tbpTechnician";
            this.tbpTechnician.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTechnician.Size = new System.Drawing.Size(936, 245);
            this.tbpTechnician.TabIndex = 0;
            this.tbpTechnician.Text = "Technician";
            this.tbpTechnician.UseVisualStyleBackColor = true;
            // 
            // txtEmployeeTitle
            // 
            this.txtEmployeeTitle.Location = new System.Drawing.Point(577, 224);
            this.txtEmployeeTitle.Name = "txtEmployeeTitle";
            this.txtEmployeeTitle.ReadOnly = true;
            this.txtEmployeeTitle.Size = new System.Drawing.Size(353, 21);
            this.txtEmployeeTitle.TabIndex = 68;
            this.txtEmployeeTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeTitle_KeyDown);
            this.txtEmployeeTitle.Validated += new System.EventHandler(this.txtEmployeeTitle_Validated);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(217, 224);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(359, 21);
            this.txtEmployeeName.TabIndex = 68;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Validated += new System.EventHandler(this.txtEmployeeName_Validated);
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(4, 227);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 67;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(21, 224);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(195, 21);
            this.txtEmployeeCode.TabIndex = 66;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Leave += new System.EventHandler(this.txtEmployeeCode_Leave);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // dgvEmployeeDetails
            // 
            this.dgvEmployeeDetails.AllowUserToAddRows = false;
            this.dgvEmployeeDetails.AutoGenerateColumns = false;
            this.dgvEmployeeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNoDataGridViewTextBoxColumn1,
            this.employeeCodeDataGridViewTextBoxColumn,
            this.employeeNameDataGridViewTextBoxColumn,
            this.EmployeeTitle});
            this.dgvEmployeeDetails.DataSource = this.lgsMaintenanceJobAssignEmployeeDetailTempBindingSource;
            this.dgvEmployeeDetails.Location = new System.Drawing.Point(4, 6);
            this.dgvEmployeeDetails.Name = "dgvEmployeeDetails";
            this.dgvEmployeeDetails.RowHeadersWidth = 40;
            this.dgvEmployeeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployeeDetails.Size = new System.Drawing.Size(926, 215);
            this.dgvEmployeeDetails.TabIndex = 63;
            this.dgvEmployeeDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvEmployeeDetails_KeyDown);
            // 
            // lineNoDataGridViewTextBoxColumn1
            // 
            this.lineNoDataGridViewTextBoxColumn1.DataPropertyName = "LineNo";
            this.lineNoDataGridViewTextBoxColumn1.HeaderText = "Row";
            this.lineNoDataGridViewTextBoxColumn1.Name = "lineNoDataGridViewTextBoxColumn1";
            this.lineNoDataGridViewTextBoxColumn1.Width = 35;
            // 
            // employeeCodeDataGridViewTextBoxColumn
            // 
            this.employeeCodeDataGridViewTextBoxColumn.DataPropertyName = "EmployeeCode";
            this.employeeCodeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.employeeCodeDataGridViewTextBoxColumn.Name = "employeeCodeDataGridViewTextBoxColumn";
            this.employeeCodeDataGridViewTextBoxColumn.Width = 150;
            // 
            // employeeNameDataGridViewTextBoxColumn
            // 
            this.employeeNameDataGridViewTextBoxColumn.DataPropertyName = "EmployeeName";
            this.employeeNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.employeeNameDataGridViewTextBoxColumn.Name = "employeeNameDataGridViewTextBoxColumn";
            this.employeeNameDataGridViewTextBoxColumn.Width = 350;
            // 
            // EmployeeTitle
            // 
            this.EmployeeTitle.DataPropertyName = "EmployeeTitle";
            this.EmployeeTitle.HeaderText = "Title";
            this.EmployeeTitle.Name = "EmployeeTitle";
            this.EmployeeTitle.ReadOnly = true;
            this.EmployeeTitle.Width = 200;
            // 
            // lgsMaintenanceJobAssignEmployeeDetailTempBindingSource
            // 
            this.lgsMaintenanceJobAssignEmployeeDetailTempBindingSource.DataSource = typeof(Domain.LgsMaintenanceJobAssignEmployeeDetailTemp);
            // 
            // tbpProduct
            // 
            this.tbpProduct.Controls.Add(this.txtQty);
            this.tbpProduct.Controls.Add(this.cmbUnit);
            this.tbpProduct.Controls.Add(this.txtProductName);
            this.tbpProduct.Controls.Add(this.chkAutoCompleationProduct);
            this.tbpProduct.Controls.Add(this.dgvItemDetails);
            this.tbpProduct.Controls.Add(this.txtProductCode);
            this.tbpProduct.Location = new System.Drawing.Point(4, 22);
            this.tbpProduct.Name = "tbpProduct";
            this.tbpProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tbpProduct.Size = new System.Drawing.Size(936, 245);
            this.tbpProduct.TabIndex = 1;
            this.tbpProduct.Text = "Products";
            this.tbpProduct.UseVisualStyleBackColor = true;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(582, 218);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(111, 21);
            this.txtQty.TabIndex = 68;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(491, 218);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(89, 21);
            this.cmbUnit.TabIndex = 67;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Validated += new System.EventHandler(this.cmbUnit_Validated);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(192, 218);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(298, 21);
            this.txtProductName.TabIndex = 65;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Validated += new System.EventHandler(this.txtProductName_Validated);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(4, 221);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 64;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProduct_CheckedChanged);
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.AllowUserToAddRows = false;
            this.dgvItemDetails.AutoGenerateColumns = false;
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNoDataGridViewTextBoxColumn,
            this.productCodeDataGridViewTextBoxColumn,
            this.productNameDataGridViewTextBoxColumn,
            this.UnitOfMeasure,
            this.orderQtyDataGridViewTextBoxColumn});
            this.dgvItemDetails.DataSource = this.lgsMaterialRequestDetailTempBindingSource;
            this.dgvItemDetails.Location = new System.Drawing.Point(4, 6);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 40;
            this.dgvItemDetails.Size = new System.Drawing.Size(689, 208);
            this.dgvItemDetails.TabIndex = 62;
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // lineNoDataGridViewTextBoxColumn
            // 
            this.lineNoDataGridViewTextBoxColumn.DataPropertyName = "LineNo";
            this.lineNoDataGridViewTextBoxColumn.HeaderText = "Row";
            this.lineNoDataGridViewTextBoxColumn.Name = "lineNoDataGridViewTextBoxColumn";
            this.lineNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.lineNoDataGridViewTextBoxColumn.Width = 40;
            // 
            // productCodeDataGridViewTextBoxColumn
            // 
            this.productCodeDataGridViewTextBoxColumn.DataPropertyName = "ProductCode";
            this.productCodeDataGridViewTextBoxColumn.HeaderText = "Product Code";
            this.productCodeDataGridViewTextBoxColumn.Name = "productCodeDataGridViewTextBoxColumn";
            this.productCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.productCodeDataGridViewTextBoxColumn.Width = 125;
            // 
            // productNameDataGridViewTextBoxColumn
            // 
            this.productNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName";
            this.productNameDataGridViewTextBoxColumn.HeaderText = "Product Name";
            this.productNameDataGridViewTextBoxColumn.Name = "productNameDataGridViewTextBoxColumn";
            this.productNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.productNameDataGridViewTextBoxColumn.Width = 270;
            // 
            // UnitOfMeasure
            // 
            this.UnitOfMeasure.DataPropertyName = "UnitOfMeasure";
            this.UnitOfMeasure.HeaderText = "Unit";
            this.UnitOfMeasure.Name = "UnitOfMeasure";
            this.UnitOfMeasure.ReadOnly = true;
            // 
            // orderQtyDataGridViewTextBoxColumn
            // 
            this.orderQtyDataGridViewTextBoxColumn.DataPropertyName = "OrderQty";
            this.orderQtyDataGridViewTextBoxColumn.HeaderText = "Order Qty";
            this.orderQtyDataGridViewTextBoxColumn.Name = "orderQtyDataGridViewTextBoxColumn";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(21, 218);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(170, 21);
            this.txtProductCode.TabIndex = 63;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            this.txtProductCode.Validated += new System.EventHandler(this.txtProductCode_Validated);
            // 
            // FrmMaintenanceJobAssignNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(956, 542);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmMaintenanceJobAssignNote";
            this.Text = "Maintenance Job Assign Note";
            this.Activated += new System.EventHandler(this.FrmMaintenanceJobAssignNote_Activated);
            this.Deactivate += new System.EventHandler(this.FrmMaintenanceJobAssignNote_Deactivate);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.tbFooter.ResumeLayout(false);
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialRequestDetailTempBindingSource)).EndInit();
            this.grpBody.ResumeLayout(false);
            this.tabAssignedDetails.ResumeLayout(false);
            this.tbpTechnician.ResumeLayout(false);
            this.tbpTechnician.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaintenanceJobAssignEmployeeDetailTempBindingSource)).EndInit();
            this.tbpProduct.ResumeLayout(false);
            this.tbpProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFooter;
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
        private System.Windows.Forms.CheckBox chkAutoCompleationMjaNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource lgsMaterialRequestDetailTempBindingSource;
        private System.Windows.Forms.Button BtnMaintenanceJobDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationJobNo;
        private System.Windows.Forms.TextBox txtMaintenanceJobRequestNo;
        private System.Windows.Forms.Label lblMaintenanceJobNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TabControl tabAssignedDetails;
        private System.Windows.Forms.TabPage tbpTechnician;
        private System.Windows.Forms.TabPage tbpProduct;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.DataGridView dgvEmployeeDetails;
        private System.Windows.Forms.BindingSource lgsMaintenanceJobAssignEmployeeDetailTempBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeLastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtEmployeeTitle;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNoDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeTitle;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJobDescription;
    }
}
