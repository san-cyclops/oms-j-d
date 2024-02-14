namespace UI.Windows
{
    partial class FrmStockAdjustment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockAdjustment));
            this.GrpHeader = new System.Windows.Forms.GroupBox();
            this.btnTogDetails = new System.Windows.Forms.Button();
            this.cmbTogDocument = new System.Windows.Forms.ComboBox();
            this.lblPOSDocuments = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationDocumentNo = new System.Windows.Forms.CheckBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.dtpAdjustmentDate = new System.Windows.Forms.DateTimePicker();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblAdjustmentDate = new System.Windows.Forms.Label();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.LblTime = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.btnEnterData = new UI.Windows.CustomControls.uc_GButton();
            this.uc_GButton2 = new UI.Windows.CustomControls.uc_GButton();
            this.uc_GButton1 = new UI.Windows.CustomControls.uc_GButton();
            this.rdoNone = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoSelected = new System.Windows.Forms.RadioButton();
            this.lstLayer = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLayer = new System.Windows.Forms.ComboBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtSellingValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtCostPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtSellingPrice = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.chkAutoCompleationProduct = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.txtTotalQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtTotalCostValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotalSellingValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GrpHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.grpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(771, 417);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 417);
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.Add(this.btnTogDetails);
            this.GrpHeader.Controls.Add(this.cmbTogDocument);
            this.GrpHeader.Controls.Add(this.lblPOSDocuments);
            this.GrpHeader.Controls.Add(this.cmbMode);
            this.GrpHeader.Controls.Add(this.label3);
            this.GrpHeader.Controls.Add(this.label2);
            this.GrpHeader.Controls.Add(this.txtNarration);
            this.GrpHeader.Controls.Add(this.chkOverwrite);
            this.GrpHeader.Controls.Add(this.chkAutoCompleationDocumentNo);
            this.GrpHeader.Controls.Add(this.txtReference);
            this.GrpHeader.Controls.Add(this.dtpAdjustmentDate);
            this.GrpHeader.Controls.Add(this.lblReference);
            this.GrpHeader.Controls.Add(this.lblAdjustmentDate);
            this.GrpHeader.Controls.Add(this.txtDocumentNo);
            this.GrpHeader.Controls.Add(this.btnDocumentDetails);
            this.GrpHeader.Controls.Add(this.cmbLocation);
            this.GrpHeader.Controls.Add(this.LblTime);
            this.GrpHeader.Controls.Add(this.lblLocation);
            this.GrpHeader.Controls.Add(this.txtRemark);
            this.GrpHeader.Controls.Add(this.lblRemark);
            this.GrpHeader.Controls.Add(this.label1);
            this.GrpHeader.Location = new System.Drawing.Point(2, -6);
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Size = new System.Drawing.Size(1086, 114);
            this.GrpHeader.TabIndex = 27;
            this.GrpHeader.TabStop = false;
            // 
            // btnTogDetails
            // 
            this.btnTogDetails.Location = new System.Drawing.Point(908, 85);
            this.btnTogDetails.Name = "btnTogDetails";
            this.btnTogDetails.Size = new System.Drawing.Size(55, 23);
            this.btnTogDetails.TabIndex = 114;
            this.btnTogDetails.Text = "Load";
            this.btnTogDetails.UseVisualStyleBackColor = true;
            this.btnTogDetails.Click += new System.EventHandler(this.btnTogDetails_Click);
            // 
            // cmbTogDocument
            // 
            this.cmbTogDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTogDocument.Enabled = false;
            this.cmbTogDocument.FormattingEnabled = true;
            this.cmbTogDocument.Location = new System.Drawing.Point(715, 86);
            this.cmbTogDocument.Name = "cmbTogDocument";
            this.cmbTogDocument.Size = new System.Drawing.Size(187, 21);
            this.cmbTogDocument.TabIndex = 112;
            // 
            // lblPOSDocuments
            // 
            this.lblPOSDocuments.AutoSize = true;
            this.lblPOSDocuments.Enabled = false;
            this.lblPOSDocuments.Location = new System.Drawing.Point(604, 89);
            this.lblPOSDocuments.Name = "lblPOSDocuments";
            this.lblPOSDocuments.Size = new System.Drawing.Size(100, 13);
            this.lblPOSDocuments.TabIndex = 113;
            this.lblPOSDocuments.Text = "TOG Documents";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(112, 12);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(171, 21);
            this.cmbMode.TabIndex = 77;
            this.cmbMode.SelectedValueChanged += new System.EventHandler(this.cmbMode_SelectedValueChanged);
            this.cmbMode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMode_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Adjustment Mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Narration";
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(112, 59);
            this.txtNarration.MaxLength = 50;
            this.txtNarration.Multiline = true;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNarration.Size = new System.Drawing.Size(448, 45);
            this.txtNarration.TabIndex = 74;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(603, 12);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(82, 17);
            this.chkOverwrite.TabIndex = 73;
            this.chkOverwrite.Tag = "1";
            this.chkOverwrite.Text = "Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationDocumentNo
            // 
            this.chkAutoCompleationDocumentNo.AutoSize = true;
            this.chkAutoCompleationDocumentNo.Checked = true;
            this.chkAutoCompleationDocumentNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDocumentNo.Location = new System.Drawing.Point(377, 15);
            this.chkAutoCompleationDocumentNo.Name = "chkAutoCompleationDocumentNo";
            this.chkAutoCompleationDocumentNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocumentNo.TabIndex = 72;
            this.chkAutoCompleationDocumentNo.Tag = "1";
            this.chkAutoCompleationDocumentNo.UseVisualStyleBackColor = true;
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(715, 35);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(366, 21);
            this.txtReference.TabIndex = 10;
            this.txtReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReference_KeyDown);
            // 
            // dtpAdjustmentDate
            // 
            this.dtpAdjustmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAdjustmentDate.Location = new System.Drawing.Point(944, 11);
            this.dtpAdjustmentDate.Name = "dtpAdjustmentDate";
            this.dtpAdjustmentDate.Size = new System.Drawing.Size(136, 21);
            this.dtpAdjustmentDate.TabIndex = 2;
            this.dtpAdjustmentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpAdjustmentDate_KeyDown);
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(604, 38);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 13);
            this.lblReference.TabIndex = 18;
            this.lblReference.Text = "Reference";
            // 
            // lblAdjustmentDate
            // 
            this.lblAdjustmentDate.AutoSize = true;
            this.lblAdjustmentDate.Location = new System.Drawing.Point(801, 14);
            this.lblAdjustmentDate.Name = "lblAdjustmentDate";
            this.lblAdjustmentDate.Size = new System.Drawing.Size(139, 13);
            this.lblAdjustmentDate.TabIndex = 38;
            this.lblAdjustmentDate.Text = "Stock Adjustment Date";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(395, 11);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocumentNo.TabIndex = 68;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Leave += new System.EventHandler(this.txtDocumentNo_Leave);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(532, 10);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 66;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(715, 59);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(365, 21);
            this.cmbLocation.TabIndex = 65;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // LblTime
            // 
            this.LblTime.Location = new System.Drawing.Point(989, 85);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(87, 16);
            this.LblTime.TabIndex = 45;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(604, 62);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 43;
            this.lblLocation.Text = "Location";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(112, 35);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(448, 21);
            this.txtRemark.TabIndex = 11;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(4, 38);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 20;
            this.lblRemark.Text = "Remark";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document No";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlInfo);
            this.groupBox1.Controls.Add(this.dtpExpiry);
            this.groupBox1.Controls.Add(this.txtBatchNo);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtSellingValue);
            this.groupBox1.Controls.Add(this.txtCostValue);
            this.groupBox1.Controls.Add(this.txtCostPrice);
            this.groupBox1.Controls.Add(this.txtSellingPrice);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProduct);
            this.groupBox1.Controls.Add(this.dgvItemDetails);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Location = new System.Drawing.Point(2, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1086, 293);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.btnEnterData);
            this.pnlInfo.Controls.Add(this.uc_GButton2);
            this.pnlInfo.Controls.Add(this.uc_GButton1);
            this.pnlInfo.Controls.Add(this.rdoNone);
            this.pnlInfo.Controls.Add(this.rdoAll);
            this.pnlInfo.Controls.Add(this.rdoSelected);
            this.pnlInfo.Controls.Add(this.lstLayer);
            this.pnlInfo.Controls.Add(this.label4);
            this.pnlInfo.Controls.Add(this.cmbLayer);
            this.pnlInfo.Location = new System.Drawing.Point(5, 10);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(1077, 281);
            this.pnlInfo.TabIndex = 63;
            // 
            // btnEnterData
            // 
            this.btnEnterData.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterData.Location = new System.Drawing.Point(11, 201);
            this.btnEnterData.Name = "btnEnterData";
            this.btnEnterData.Size = new System.Drawing.Size(141, 23);
            this.btnEnterData.TabIndex = 91;
            this.btnEnterData.Text = "&Enter Data";
            this.btnEnterData.Click += new System.EventHandler(this.btnEnterData_Click);
            // 
            // uc_GButton2
            // 
            this.uc_GButton2.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_GButton2.Location = new System.Drawing.Point(11, 172);
            this.uc_GButton2.Name = "uc_GButton2";
            this.uc_GButton2.Size = new System.Drawing.Size(141, 23);
            this.uc_GButton2.TabIndex = 90;
            this.uc_GButton2.Text = "I&mport Data";
            // 
            // uc_GButton1
            // 
            this.uc_GButton1.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_GButton1.Location = new System.Drawing.Point(11, 143);
            this.uc_GButton1.Name = "uc_GButton1";
            this.uc_GButton1.Size = new System.Drawing.Size(141, 23);
            this.uc_GButton1.TabIndex = 89;
            this.uc_GButton1.Text = "&Initialize Stock ";
            // 
            // rdoNone
            // 
            this.rdoNone.AutoSize = true;
            this.rdoNone.Checked = true;
            this.rdoNone.Location = new System.Drawing.Point(11, 46);
            this.rdoNone.Name = "rdoNone";
            this.rdoNone.Size = new System.Drawing.Size(163, 17);
            this.rdoNone.TabIndex = 87;
            this.rdoNone.TabStop = true;
            this.rdoNone.Text = "Adjust Enterd Item Only";
            this.rdoNone.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(11, 110);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(107, 17);
            this.rdoAll.TabIndex = 85;
            this.rdoAll.Text = "Zero All Items";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdoSelected
            // 
            this.rdoSelected.AutoSize = true;
            this.rdoSelected.Location = new System.Drawing.Point(11, 77);
            this.rdoSelected.Name = "rdoSelected";
            this.rdoSelected.Size = new System.Drawing.Size(178, 17);
            this.rdoSelected.TabIndex = 84;
            this.rdoSelected.Text = "Zero Selected Layer Items";
            this.rdoSelected.UseVisualStyleBackColor = true;
            // 
            // lstLayer
            // 
            this.lstLayer.BackgroundImageTiled = true;
            this.lstLayer.CheckBoxes = true;
            this.lstLayer.Enabled = false;
            this.lstLayer.HideSelection = false;
            this.lstLayer.LargeImageList = this.imgList;
            this.lstLayer.Location = new System.Drawing.Point(340, 6);
            this.lstLayer.Name = "lstLayer";
            this.lstLayer.ShowItemToolTips = true;
            this.lstLayer.Size = new System.Drawing.Size(733, 269);
            this.lstLayer.SmallImageList = this.imgList;
            this.lstLayer.TabIndex = 82;
            this.lstLayer.UseCompatibleStateImageBehavior = false;
            this.lstLayer.View = System.Windows.Forms.View.List;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 78;
            this.label4.Text = "Stock Taking Layer";
            // 
            // cmbLayer
            // 
            this.cmbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayer.FormattingEnabled = true;
            this.cmbLayer.Items.AddRange(new object[] {
            "Department",
            "Lifestyle",
            "Product",
            "Brand"});
            this.cmbLayer.Location = new System.Drawing.Point(147, 6);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Size = new System.Drawing.Size(173, 21);
            this.cmbLayer.TabIndex = 77;
            this.cmbLayer.SelectedIndexChanged += new System.EventHandler(this.cmbLayer_SelectedIndexChanged);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(589, 268);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(88, 21);
            this.dtpExpiry.TabIndex = 61;
            this.dtpExpiry.Value = new System.DateTime(2013, 12, 11, 9, 32, 0, 0);
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(445, 268);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(142, 21);
            this.txtBatchNo.TabIndex = 60;
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            this.txtBatchNo.Leave += new System.EventHandler(this.txtBatchNo_Leave);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(678, 268);
            this.txtQty.Name = "txtQty";
            this.txtQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.Size = new System.Drawing.Size(55, 21);
            this.txtQty.TabIndex = 59;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtSellingValue
            // 
            this.txtSellingValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingValue.Location = new System.Drawing.Point(967, 268);
            this.txtSellingValue.Name = "txtSellingValue";
            this.txtSellingValue.ReadOnly = true;
            this.txtSellingValue.Size = new System.Drawing.Size(115, 21);
            this.txtSellingValue.TabIndex = 58;
            this.txtSellingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellingValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSellingValue_KeyDown);
            // 
            // txtCostValue
            // 
            this.txtCostValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostValue.Location = new System.Drawing.Point(887, 268);
            this.txtCostValue.Name = "txtCostValue";
            this.txtCostValue.ReadOnly = true;
            this.txtCostValue.Size = new System.Drawing.Size(79, 21);
            this.txtCostValue.TabIndex = 57;
            this.txtCostValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCostPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCostPrice.Location = new System.Drawing.Point(734, 268);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.ReadOnly = true;
            this.txtCostPrice.Size = new System.Drawing.Size(77, 21);
            this.txtCostPrice.TabIndex = 56;
            this.txtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostPrice_KeyDown);
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSellingPrice.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Location = new System.Drawing.Point(812, 268);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.ReadOnly = true;
            this.txtSellingPrice.Size = new System.Drawing.Size(74, 21);
            this.txtSellingPrice.TabIndex = 55;
            this.txtSellingPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(378, 268);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(66, 21);
            this.cmbUnit.TabIndex = 54;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(188, 268);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(189, 21);
            this.txtProductName.TabIndex = 48;
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            this.txtProductName.Leave += new System.EventHandler(this.txtProductName_Leave);
            // 
            // chkAutoCompleationProduct
            // 
            this.chkAutoCompleationProduct.AutoSize = true;
            this.chkAutoCompleationProduct.Checked = true;
            this.chkAutoCompleationProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProduct.Location = new System.Drawing.Point(5, 271);
            this.chkAutoCompleationProduct.Name = "chkAutoCompleationProduct";
            this.chkAutoCompleationProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProduct.TabIndex = 47;
            this.chkAutoCompleationProduct.Tag = "1";
            this.chkAutoCompleationProduct.UseVisualStyleBackColor = true;
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.ProductCode,
            this.ProductName,
            this.Unit,
            this.BatchNo,
            this.Expiry,
            this.Qty,
            this.CostPrice,
            this.SellingPrice,
            this.CostValue,
            this.SellingValue});
            this.dgvItemDetails.Location = new System.Drawing.Point(5, 12);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.RowHeadersWidth = 45;
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1077, 253);
            this.dgvItemDetails.TabIndex = 45;
            this.dgvItemDetails.DoubleClick += new System.EventHandler(this.dgvItemDetails_DoubleClick);
            this.dgvItemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemDetails_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RowNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 115;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 185;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "UnitOfMeasure";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 58;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BatchNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 145;
            // 
            // Expiry
            // 
            this.Expiry.DataPropertyName = "ExpiryDate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Expiry.DefaultCellStyle = dataGridViewCellStyle6;
            this.Expiry.HeaderText = "Expiry Date";
            this.Expiry.Name = "Expiry";
            this.Expiry.ReadOnly = true;
            this.Expiry.Width = 83;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle7;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 55;
            // 
            // CostPrice
            // 
            this.CostPrice.DataPropertyName = "CostPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.CostPrice.HeaderText = "Cost Price";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.ReadOnly = true;
            this.CostPrice.Width = 78;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Width = 78;
            // 
            // CostValue
            // 
            this.CostValue.DataPropertyName = "CostValue";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CostValue.DefaultCellStyle = dataGridViewCellStyle10;
            this.CostValue.HeaderText = "Cost Value";
            this.CostValue.Name = "CostValue";
            this.CostValue.ReadOnly = true;
            this.CostValue.Width = 80;
            // 
            // SellingValue
            // 
            this.SellingValue.DataPropertyName = "SellingValue";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SellingValue.DefaultCellStyle = dataGridViewCellStyle11;
            this.SellingValue.HeaderText = "Selling Value";
            this.SellingValue.Name = "SellingValue";
            this.SellingValue.ReadOnly = true;
            this.SellingValue.Width = 95;
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(22, 268);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(165, 21);
            this.txtProductCode.TabIndex = 46;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);
            this.txtProductCode.Leave += new System.EventHandler(this.txtProductCode_Leave);
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.prgBar);
            this.grpFooter.Controls.Add(this.txtTotalQty);
            this.grpFooter.Controls.Add(this.txtTotalCostValue);
            this.grpFooter.Controls.Add(this.lblTotal);
            this.grpFooter.Controls.Add(this.txtTotalSellingValue);
            this.grpFooter.Location = new System.Drawing.Point(2, 381);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(1086, 42);
            this.grpFooter.TabIndex = 29;
            this.grpFooter.TabStop = false;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(4, 22);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(776, 10);
            this.prgBar.TabIndex = 30;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalQty.Location = new System.Drawing.Point(832, 17);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(55, 21);
            this.txtTotalQty.TabIndex = 60;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalCostValue
            // 
            this.txtTotalCostValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalCostValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalCostValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalCostValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalCostValue.Location = new System.Drawing.Point(888, 17);
            this.txtTotalCostValue.Name = "txtTotalCostValue";
            this.txtTotalCostValue.ReadOnly = true;
            this.txtTotalCostValue.Size = new System.Drawing.Size(79, 21);
            this.txtTotalCostValue.TabIndex = 59;
            this.txtTotalCostValue.Text = "0.00";
            this.txtTotalCostValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(786, 20);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 36;
            this.lblTotal.Text = "Total";
            // 
            // txtTotalSellingValue
            // 
            this.txtTotalSellingValue.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalSellingValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotalSellingValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalSellingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTotalSellingValue.Location = new System.Drawing.Point(968, 17);
            this.txtTotalSellingValue.Name = "txtTotalSellingValue";
            this.txtTotalSellingValue.ReadOnly = true;
            this.txtTotalSellingValue.Size = new System.Drawing.Size(113, 21);
            this.txtTotalSellingValue.TabIndex = 0;
            this.txtTotalSellingValue.Text = "0.00";
            this.txtTotalSellingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "box.png");
            this.imgList.Images.SetKeyName(1, "home.gif");
            this.imgList.Images.SetKeyName(2, "kgpg.png");
            this.imgList.Images.SetKeyName(3, "LogOff.png");
            this.imgList.Images.SetKeyName(4, "xchat.png");
            // 
            // FrmStockAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1090, 465);
            this.Controls.Add(this.GrpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFooter);
            this.Name = "FrmStockAdjustment";
            this.Text = "Stock Adjustment";
            this.Load += new System.EventHandler(this.FrmStockAdjustment_Load);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.GrpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GrpHeader.ResumeLayout(false);
            this.GrpHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.grpFooter.ResumeLayout(false);
            this.grpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpHeader;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocumentNo;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.DateTimePicker dtpAdjustmentDate;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label lblAdjustmentDate;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label LblTime;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.TextBox txtBatchNo;
        private CustomControls.TextBoxQty txtQty;
        private CustomControls.TextBoxCurrency txtSellingValue;
        private CustomControls.TextBoxCurrency txtCostValue;
        private CustomControls.TextBoxCurrency txtCostPrice;
        private CustomControls.TextBoxCurrency txtSellingPrice;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.CheckBox chkAutoCompleationProduct;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.GroupBox grpFooter;
        private CustomControls.TextBoxQty txtTotalQty;
        private CustomControls.TextBoxCurrency txtTotalCostValue;
        private System.Windows.Forms.Label lblTotal;
        private CustomControls.TextBoxCurrency txtTotalSellingValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingValue;
        private System.Windows.Forms.Button btnTogDetails;
        private System.Windows.Forms.ComboBox cmbTogDocument;
        private System.Windows.Forms.Label lblPOSDocuments;
        private System.Windows.Forms.Panel pnlInfo;
        private CustomControls.uc_GButton btnEnterData;
        private CustomControls.uc_GButton uc_GButton2;
        private CustomControls.uc_GButton uc_GButton1;
        private System.Windows.Forms.RadioButton rdoNone;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoSelected;
        internal System.Windows.Forms.ListView lstLayer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLayer;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.ImageList imgList;
    }
}
