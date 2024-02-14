namespace UI.Windows
{
    partial class FrmMaterialAllocationNote
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.btnMRNDocuments = new System.Windows.Forms.Button();
            this.btnRequestDetails = new System.Windows.Forms.Button();
            this.chkAutoCompleationMRNNo = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationMJANo = new System.Windows.Forms.CheckBox();
            this.txtMRNNo = new System.Windows.Forms.TextBox();
            this.LblMrnNo = new System.Windows.Forms.Label();
            this.txtJobAssignNoteNo = new System.Windows.Forms.TextBox();
            this.lblJobAssignNo = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpAllocationDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblAllocationDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtRequestNo = new System.Windows.Forms.TextBox();
            this.cmbRequestLocationCode = new System.Windows.Forms.ComboBox();
            this.txtProductAmount = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.lineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitOfMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestLocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requestNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrossAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lgsMaterialAllocationDetailTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
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
            this.tbpStockDetails = new System.Windows.Forms.TabPage();
            this.dgvBatchStock = new System.Windows.Forms.DataGridView();
            this.Batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkViewStokDetails = new System.Windows.Forms.CheckBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialAllocationDetailTempBindingSource)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tbpPageSetup.SuspendLayout();
            this.tbpOtherDetails.SuspendLayout();
            this.tbpStockDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(658, 449);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 449);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.chkViewStokDetails);
            this.grpHeader.Controls.Add(this.btnMRNDocuments);
            this.grpHeader.Controls.Add(this.btnRequestDetails);
            this.grpHeader.Controls.Add(this.chkAutoCompleationMRNNo);
            this.grpHeader.Controls.Add(this.chkAutoCompleationMJANo);
            this.grpHeader.Controls.Add(this.txtMRNNo);
            this.grpHeader.Controls.Add(this.LblMrnNo);
            this.grpHeader.Controls.Add(this.txtJobAssignNoteNo);
            this.grpHeader.Controls.Add(this.lblJobAssignNo);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpAllocationDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblAllocationDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(974, 84);
            this.grpHeader.TabIndex = 22;
            this.grpHeader.TabStop = false;
            // 
            // btnMRNDocuments
            // 
            this.btnMRNDocuments.Location = new System.Drawing.Point(723, 11);
            this.btnMRNDocuments.Name = "btnMRNDocuments";
            this.btnMRNDocuments.Size = new System.Drawing.Size(28, 23);
            this.btnMRNDocuments.TabIndex = 67;
            this.btnMRNDocuments.Text = "...";
            this.btnMRNDocuments.UseVisualStyleBackColor = true;
            this.btnMRNDocuments.Click += new System.EventHandler(this.btnMRNDocuments_Click);
            // 
            // btnRequestDetails
            // 
            this.btnRequestDetails.Location = new System.Drawing.Point(485, 11);
            this.btnRequestDetails.Name = "btnRequestDetails";
            this.btnRequestDetails.Size = new System.Drawing.Size(28, 23);
            this.btnRequestDetails.TabIndex = 67;
            this.btnRequestDetails.Text = "...";
            this.btnRequestDetails.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationMRNNo
            // 
            this.chkAutoCompleationMRNNo.AutoSize = true;
            this.chkAutoCompleationMRNNo.Checked = true;
            this.chkAutoCompleationMRNNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationMRNNo.Location = new System.Drawing.Point(573, 15);
            this.chkAutoCompleationMRNNo.Name = "chkAutoCompleationMRNNo";
            this.chkAutoCompleationMRNNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationMRNNo.TabIndex = 66;
            this.chkAutoCompleationMRNNo.Tag = "1";
            this.chkAutoCompleationMRNNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationMRNNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationMRNNo_CheckedChanged);
            // 
            // chkAutoCompleationMJANo
            // 
            this.chkAutoCompleationMJANo.AutoSize = true;
            this.chkAutoCompleationMJANo.Checked = true;
            this.chkAutoCompleationMJANo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationMJANo.Location = new System.Drawing.Point(334, 15);
            this.chkAutoCompleationMJANo.Name = "chkAutoCompleationMJANo";
            this.chkAutoCompleationMJANo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationMJANo.TabIndex = 66;
            this.chkAutoCompleationMJANo.Tag = "1";
            this.chkAutoCompleationMJANo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationMJANo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationMJANo_CheckedChanged);
            // 
            // txtMRNNo
            // 
            this.txtMRNNo.Location = new System.Drawing.Point(591, 12);
            this.txtMRNNo.Name = "txtMRNNo";
            this.txtMRNNo.Size = new System.Drawing.Size(131, 21);
            this.txtMRNNo.TabIndex = 65;
            this.txtMRNNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMRNNo_KeyDown);
            this.txtMRNNo.Validated += new System.EventHandler(this.txtMRNNo_Validated);
            // 
            // LblMrnNo
            // 
            this.LblMrnNo.AutoSize = true;
            this.LblMrnNo.Location = new System.Drawing.Point(517, 16);
            this.LblMrnNo.Name = "LblMrnNo";
            this.LblMrnNo.Size = new System.Drawing.Size(51, 13);
            this.LblMrnNo.TabIndex = 64;
            this.LblMrnNo.Text = "MRN No";
            // 
            // txtJobAssignNoteNo
            // 
            this.txtJobAssignNoteNo.Location = new System.Drawing.Point(352, 12);
            this.txtJobAssignNoteNo.Name = "txtJobAssignNoteNo";
            this.txtJobAssignNoteNo.Size = new System.Drawing.Size(131, 21);
            this.txtJobAssignNoteNo.TabIndex = 65;
            this.txtJobAssignNoteNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJobAssignNoteNo_KeyDown);
            this.txtJobAssignNoteNo.Validated += new System.EventHandler(this.txtJobAssignNoteNo_Validated);
            // 
            // lblJobAssignNo
            // 
            this.lblJobAssignNo.AutoSize = true;
            this.lblJobAssignNo.Location = new System.Drawing.Point(284, 16);
            this.lblJobAssignNo.Name = "lblJobAssignNo";
            this.lblJobAssignNo.Size = new System.Drawing.Size(48, 13);
            this.lblJobAssignNo.TabIndex = 64;
            this.lblJobAssignNo.Text = "MJA No";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(591, 56);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(379, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(517, 60);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(112, 34);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(136, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            this.txtReferenceNo.Validated += new System.EventHandler(this.txtReferenceNo_Validated);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 36);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpAllocationDate
            // 
            this.dtpAllocationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAllocationDate.Location = new System.Drawing.Point(859, 12);
            this.dtpAllocationDate.Name = "dtpAllocationDate";
            this.dtpAllocationDate.Size = new System.Drawing.Size(111, 21);
            this.dtpAllocationDate.TabIndex = 22;
            this.dtpAllocationDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpAllocationDate_KeyDown);
            this.dtpAllocationDate.Validated += new System.EventHandler(this.dtpAllocationDate_Validated);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(249, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(6, 59);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 56);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(401, 21);
            this.txtRemark.TabIndex = 18;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            this.txtRemark.Validated += new System.EventHandler(this.txtRemark_Validated);
            // 
            // lblAllocationDate
            // 
            this.lblAllocationDate.AutoSize = true;
            this.lblAllocationDate.Location = new System.Drawing.Point(767, 16);
            this.lblAllocationDate.Name = "lblAllocationDate";
            this.lblAllocationDate.Size = new System.Drawing.Size(63, 13);
            this.lblAllocationDate.TabIndex = 9;
            this.lblAllocationDate.Text = "MAN Date";
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(95, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 4;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocumentNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocumentNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
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
            // grpBody
            // 
            this.grpBody.Controls.Add(this.txtQty);
            this.grpBody.Controls.Add(this.txtRequestNo);
            this.grpBody.Controls.Add(this.cmbRequestLocationCode);
            this.grpBody.Controls.Add(this.txtProductAmount);
            this.grpBody.Controls.Add(this.txtRate);
            this.grpBody.Controls.Add(this.cmbUnit);
            this.grpBody.Controls.Add(this.txtProductName);
            this.grpBody.Controls.Add(this.chkAutoCompleationProduct);
            this.grpBody.Controls.Add(this.dgvItemDetails);
            this.grpBody.Controls.Add(this.txtProductCode);
            this.grpBody.Location = new System.Drawing.Point(1, 72);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(974, 241);
            this.grpBody.TabIndex = 24;
            this.grpBody.TabStop = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(707, 214);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(59, 21);
            this.txtQty.TabIndex = 60;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtRequestNo
            // 
            this.txtRequestNo.Location = new System.Drawing.Point(607, 214);
            this.txtRequestNo.Name = "txtRequestNo";
            this.txtRequestNo.Size = new System.Drawing.Size(99, 21);
            this.txtRequestNo.TabIndex = 59;
            this.txtRequestNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRequestNo_KeyDown);
            this.txtRequestNo.Validated += new System.EventHandler(this.txtRequestNo_Validated);
            // 
            // cmbRequestLocationCode
            // 
            this.cmbRequestLocationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestLocationCode.FormattingEnabled = true;
            this.cmbRequestLocationCode.Location = new System.Drawing.Point(528, 214);
            this.cmbRequestLocationCode.Name = "cmbRequestLocationCode";
            this.cmbRequestLocationCode.Size = new System.Drawing.Size(78, 21);
            this.cmbRequestLocationCode.TabIndex = 58;
            this.cmbRequestLocationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRequestLocationCode_KeyDown);
            this.cmbRequestLocationCode.Validated += new System.EventHandler(this.cmbRequestLocationCode_Validated);
            // 
            // txtProductAmount
            // 
            this.txtProductAmount.Location = new System.Drawing.Point(855, 214);
            this.txtProductAmount.Name = "txtProductAmount";
            this.txtProductAmount.ReadOnly = true;
            this.txtProductAmount.Size = new System.Drawing.Size(115, 21);
            this.txtProductAmount.TabIndex = 56;
            this.txtProductAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductAmount_KeyDown);
            this.txtProductAmount.Validated += new System.EventHandler(this.txtProductAmount_Validated);
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(767, 214);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(87, 21);
            this.txtRate.TabIndex = 57;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            this.txtRate.Validated += new System.EventHandler(this.txtRate_Validated);
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(457, 214);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(70, 21);
            this.cmbUnit.TabIndex = 55;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Validated += new System.EventHandler(this.cmbUnit_Validated);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(207, 214);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(249, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Validated += new System.EventHandler(this.txtProductName_Validated);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 217);
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
            this.unitOfMeasure,
            this.RequestLocationCode,
            this.requestNo,
            this.qty,
            this.costPrice,
            this.GrossAmount});
            this.dgvItemDetails.DataSource = this.lgsMaterialAllocationDetailTempBindingSource;
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 11);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.ReadOnly = true;
            this.dgvItemDetails.RowHeadersWidth = 20;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(965, 200);
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
            this.lineNo.Width = 35;
            // 
            // productCode
            // 
            this.productCode.DataPropertyName = "ProductCode";
            this.productCode.HeaderText = "Product Code";
            this.productCode.Name = "productCode";
            this.productCode.ReadOnly = true;
            this.productCode.Width = 145;
            // 
            // productName
            // 
            this.productName.DataPropertyName = "ProductName";
            this.productName.HeaderText = "Product Name";
            this.productName.Name = "productName";
            this.productName.ReadOnly = true;
            this.productName.Width = 250;
            // 
            // unitOfMeasure
            // 
            this.unitOfMeasure.DataPropertyName = "UnitOfMeasure";
            this.unitOfMeasure.HeaderText = "Unit";
            this.unitOfMeasure.Name = "unitOfMeasure";
            this.unitOfMeasure.ReadOnly = true;
            this.unitOfMeasure.Width = 70;
            // 
            // RequestLocationCode
            // 
            this.RequestLocationCode.DataPropertyName = "RequestLocationCode";
            this.RequestLocationCode.HeaderText = "Location";
            this.RequestLocationCode.Name = "RequestLocationCode";
            this.RequestLocationCode.ReadOnly = true;
            this.RequestLocationCode.Width = 80;
            // 
            // requestNo
            // 
            this.requestNo.DataPropertyName = "RequestNo";
            this.requestNo.HeaderText = "Request#";
            this.requestNo.Name = "requestNo";
            this.requestNo.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "Qty";
            this.qty.HeaderText = "Qty";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 60;
            // 
            // costPrice
            // 
            this.costPrice.DataPropertyName = "CostPrice";
            this.costPrice.HeaderText = "Rate";
            this.costPrice.Name = "costPrice";
            this.costPrice.ReadOnly = true;
            this.costPrice.Width = 85;
            // 
            // GrossAmount
            // 
            this.GrossAmount.DataPropertyName = "GrossAmount";
            this.GrossAmount.HeaderText = "Amount";
            this.GrossAmount.Name = "GrossAmount";
            this.GrossAmount.ReadOnly = true;
            this.GrossAmount.Width = 95;
            // 
            // lgsMaterialAllocationDetailTempBindingSource
            // 
            this.lgsMaterialAllocationDetailTempBindingSource.DataSource = typeof(Domain.LgsMaterialAllocationDetailTemp);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 214);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(184, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Validated += new System.EventHandler(this.txtProductCode_Validated);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.lblTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.lblTotalAmount);
            this.grpFooter.Controls.Add(this.txtTotalAmount);
            this.grpFooter.Controls.Add(this.tbFooter);
            this.grpFooter.Location = new System.Drawing.Point(1, 307);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(974, 147);
            this.grpFooter.TabIndex = 23;
            this.grpFooter.TabStop = false;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(756, 58);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(58, 13);
            this.lblTotalQty.TabIndex = 39;
            this.lblTotalQty.Text = "Total Qty";
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Firebrick;
            this.txtTotalQty.Location = new System.Drawing.Point(844, 55);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(126, 21);
            this.txtTotalQty.TabIndex = 38;
            this.txtTotalQty.Text = "0.00";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(756, 33);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(82, 13);
            this.lblTotalAmount.TabIndex = 37;
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotalAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.ForeColor = System.Drawing.Color.Firebrick;
            this.txtTotalAmount.Location = new System.Drawing.Point(844, 30);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(126, 21);
            this.txtTotalAmount.TabIndex = 36;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbFooter
            // 
            this.tbFooter.Controls.Add(this.tbpStockDetails);
            this.tbFooter.Controls.Add(this.tbpPageSetup);
            this.tbFooter.Controls.Add(this.tbpOtherDetails);
            this.tbFooter.Location = new System.Drawing.Point(5, 10);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.SelectedIndex = 0;
            this.tbFooter.Size = new System.Drawing.Size(455, 131);
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
            this.tbpPageSetup.Size = new System.Drawing.Size(447, 86);
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
            this.tbpOtherDetails.Size = new System.Drawing.Size(447, 86);
            this.tbpOtherDetails.TabIndex = 4;
            this.tbpOtherDetails.Text = "Other Details";
            this.tbpOtherDetails.UseVisualStyleBackColor = true;
            // 
            // cmbCostCentre
            // 
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
            // tbpStockDetails
            // 
            this.tbpStockDetails.Controls.Add(this.dgvBatchStock);
            this.tbpStockDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpStockDetails.Name = "tbpStockDetails";
            this.tbpStockDetails.Size = new System.Drawing.Size(447, 105);
            this.tbpStockDetails.TabIndex = 5;
            this.tbpStockDetails.Text = "Stock Details";
            this.tbpStockDetails.UseVisualStyleBackColor = true;
            // 
            // dgvBatchStock
            // 
            this.dgvBatchStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch,
            this.Stock});
            this.dgvBatchStock.Location = new System.Drawing.Point(3, 3);
            this.dgvBatchStock.Name = "dgvBatchStock";
            this.dgvBatchStock.Size = new System.Drawing.Size(440, 98);
            this.dgvBatchStock.TabIndex = 4;
            // 
            // Batch
            // 
            this.Batch.DataPropertyName = "BatchNo";
            this.Batch.HeaderText = "Batch No";
            this.Batch.Name = "Batch";
            this.Batch.ReadOnly = true;
            this.Batch.Width = 192;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "BalanceQty";
            this.Stock.HeaderText = "Current Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 75;
            // 
            // chkViewStokDetails
            // 
            this.chkViewStokDetails.AutoSize = true;
            this.chkViewStokDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewStokDetails.Checked = true;
            this.chkViewStokDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewStokDetails.Location = new System.Drawing.Point(881, 37);
            this.chkViewStokDetails.Name = "chkViewStokDetails";
            this.chkViewStokDetails.Size = new System.Drawing.Size(89, 17);
            this.chkViewStokDetails.TabIndex = 68;
            this.chkViewStokDetails.Tag = "1";
            this.chkViewStokDetails.Text = "View Stock";
            this.chkViewStokDetails.UseVisualStyleBackColor = true;
            this.chkViewStokDetails.CheckedChanged += new System.EventHandler(this.chkViewStokDetails_CheckedChanged);
            // 
            // FrmMaterialAllocationNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(977, 497);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmMaterialAllocationNote";
            this.Text = "Material Allocation Note";
            this.Activated += new System.EventHandler(this.FrmMaterialAllocationNote_Activated);
            this.Deactivate += new System.EventHandler(this.FrmMaterialAllocationNote_Deactivate);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgsMaterialAllocationDetailTempBindingSource)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.tbFooter.ResumeLayout(false);
            this.tbpPageSetup.ResumeLayout(false);
            this.tbpPageSetup.PerformLayout();
            this.tbpOtherDetails.ResumeLayout(false);
            this.tbpOtherDetails.PerformLayout();
            this.tbpStockDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Button btnRequestDetails;
        private System.Windows.Forms.CheckBox chkAutoCompleationMJANo;
        private System.Windows.Forms.TextBox txtJobAssignNoteNo;
        private System.Windows.Forms.Label lblJobAssignNo;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpAllocationDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblAllocationDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
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
        private System.Windows.Forms.TextBox txtProductAmount;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtRequestNo;
        private System.Windows.Forms.ComboBox cmbRequestLocationCode;
        private System.Windows.Forms.BindingSource lgsMaterialAllocationDetailTempBindingSource;
        private System.Windows.Forms.Button btnMRNDocuments;
        private System.Windows.Forms.CheckBox chkAutoCompleationMRNNo;
        private System.Windows.Forms.TextBox txtMRNNo;
        private System.Windows.Forms.Label LblMrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn productCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitOfMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestLocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn costPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrossAmount;
        private CustomControls.TextBoxQty txtQty;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.TabPage tbpStockDetails;
        private System.Windows.Forms.DataGridView dgvBatchStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.CheckBox chkViewStokDetails;
    }
}
