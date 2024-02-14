namespace UI.Windows
{
    partial class FrmPromotionMaster
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.cmbPromotionType = new System.Windows.Forms.ComboBox();
            this.lblPromotionType = new System.Windows.Forms.Label();
            this.chkAutoApply = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationPromotion = new System.Windows.Forms.CheckBox();
            this.txtPromotionCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtPromotionDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.tbPromotion = new System.Windows.Forms.TabControl();
            this.tbpBugXGetY = new System.Windows.Forms.TabPage();
            this.txtGetDiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.tbpBuyX = new System.Windows.Forms.TabControl();
            this.tbpBuyProduct = new System.Windows.Forms.TabPage();
            this.ChkAutoSelectGetProduct = new System.Windows.Forms.CheckBox();
            this.txtBuyRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.cmbBuyUnit = new System.Windows.Forms.ComboBox();
            this.chkAutoCompleationBuyProduct = new System.Windows.Forms.CheckBox();
            this.txtBuyQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBuyProductName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBuyProductCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvBuyDetails = new System.Windows.Forms.DataGridView();
            this.BuyProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpBuyDepartment = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationBuyDepartment = new System.Windows.Forms.CheckBox();
            this.txtBuyDepartmentQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBuyDepartmentName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBuyDepartmentCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvBuyDepartment = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpBuyCategory = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationBuyCaytegory = new System.Windows.Forms.CheckBox();
            this.txtBuyCategoryQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBuyCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBuyCategoryCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvBuyCategory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpBuySubCategory = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationBuySubCategory = new System.Windows.Forms.CheckBox();
            this.txtBuySubCategoryQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBuySubCategoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBuySubCategoryCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvBuySubCategory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpBuySubCategory2 = new System.Windows.Forms.TabPage();
            this.chkAutoCompleationBuySubCategory2 = new System.Windows.Forms.CheckBox();
            this.txtBuySubCategory2Qty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtBuySubCategory2Name = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtBuySubCategory2Code = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvBuySubCategory2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGetRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGetPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.cmbGetUnit = new System.Windows.Forms.ComboBox();
            this.txtGetDiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationGetProduct = new System.Windows.Forms.CheckBox();
            this.txtGetQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtGetProductName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtGetProductCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvGetDetails = new System.Windows.Forms.DataGridView();
            this.GetProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetDiscPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetDiscAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpProductDiscount = new System.Windows.Forms.TabPage();
            this.txtProductDiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtProductPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.cmbProductUnit = new System.Windows.Forms.ComboBox();
            this.txtProductDiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationPdisProduct = new System.Windows.Forms.CheckBox();
            this.txtProductQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtProductProductName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtProductProductCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvProductDiscount = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpDpartmentDiscount = new System.Windows.Forms.TabPage();
            this.txtDepartmentDiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.txtDepartmentPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtDepartmentDiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationDepartment = new System.Windows.Forms.CheckBox();
            this.txtDepartmentQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtDepartmentDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtDepartmentCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvDepartmentDiscount = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpCategoryDiscount = new System.Windows.Forms.TabPage();
            this.txtCategoryDiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.txtCategoryPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtCategoryDiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationCategory = new System.Windows.Forms.CheckBox();
            this.txtCategoryQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtCategoryCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvCategoryDiscount = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpSubCategoryDiscount = new System.Windows.Forms.TabPage();
            this.txtSubCategoryDiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.txtSubCategoryPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtSubCategoryDiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoCompleationSubCategory = new System.Windows.Forms.CheckBox();
            this.txtSubCategoryQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtSubCategoryDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategoryCode = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvSubCategory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubCategoryPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpSubCategory2Discount = new System.Windows.Forms.TabPage();
            this.txtSubCategory2DiscPercentage = new UI.Windows.CustomControls.TextBoxQty();
            this.txtSubCategory2Points = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtSubCategory2DiscAmount = new UI.Windows.CustomControls.TextBoxCurrency();
            this.ChkSubCategory2Dis = new System.Windows.Forms.CheckBox();
            this.txtSubCategory2Qty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtSubCategory2Description = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtSubCategory2Code = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dgvSubCategory2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpSubTotal = new System.Windows.Forms.TabPage();
            this.ChkCombined = new System.Windows.Forms.CheckBox();
            this.ChkGiftVoucher = new System.Windows.Forms.CheckBox();
            this.txtPoints = new UI.Windows.CustomControls.TextBoxNumeric();
            this.lblPoints = new System.Windows.Forms.Label();
            this.txtSubTotalDiscountValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiscountPercentage = new System.Windows.Forms.Label();
            this.txtSubTotalDiscountPercentage = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.tbpGetGift = new System.Windows.Forms.TabPage();
            this.txtGiftGetValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.txtGiftGetQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtGiftDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtGiftValueRange = new UI.Windows.CustomControls.TextBoxCurrency();
            this.ChkAutoCompleationSubCategory2 = new System.Windows.Forms.CheckBox();
            this.txtGiftBuyQty = new UI.Windows.CustomControls.TextBoxQty();
            this.txtGiftSubCategory2Name = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtGiftSubCategory2Code = new UI.Windows.CustomControls.TextBoxProductCode();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiftQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiftValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Increase = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Raffle = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpDays = new System.Windows.Forms.GroupBox();
            this.chkSundayTime = new System.Windows.Forms.CheckBox();
            this.chkSaturdayTime = new System.Windows.Forms.CheckBox();
            this.chkFridayTime = new System.Windows.Forms.CheckBox();
            this.chkThuresdayTime = new System.Windows.Forms.CheckBox();
            this.chkWednesdayTime = new System.Windows.Forms.CheckBox();
            this.chkTuesdayTime = new System.Windows.Forms.CheckBox();
            this.chkMondayTime = new System.Windows.Forms.CheckBox();
            this.dtpSundayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSundayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSaturdayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSaturdayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpFridayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFridayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpThuresdayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpThuresdayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpWednesdayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpWednesdayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTuesdayTo = new System.Windows.Forms.DateTimePicker();
            this.dtpTuesdayFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpMondayTo = new System.Windows.Forms.DateTimePicker();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkThuresday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.dtpMondayFrom = new System.Windows.Forms.DateTimePicker();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecipient = new System.Windows.Forms.Label();
            this.cmbRecipient = new System.Windows.Forms.ComboBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnSendSms = new System.Windows.Forms.Button();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAllLocations = new System.Windows.Forms.CheckBox();
            this.dgvLocationInfo = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.chkAllType = new System.Windows.Forms.CheckBox();
            this.chkConsiderProvider = new System.Windows.Forms.CheckBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoCard = new System.Windows.Forms.RadioButton();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.chkConsiderValue = new System.Windows.Forms.CheckBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtMaxValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblValueRange = new System.Windows.Forms.Label();
            this.txtMinValue = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblType = new System.Windows.Forms.Label();
            this.dgvType = new System.Windows.Forms.DataGridView();
            this.TypeSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblProvider = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCashierMsg = new System.Windows.Forms.Label();
            this.txtCashierMessage = new System.Windows.Forms.TextBox();
            this.lblBillDisplayMsg = new System.Windows.Forms.Label();
            this.txtBillDisplayMessage = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvCustomerGroup = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.tbPromotion.SuspendLayout();
            this.tbpBugXGetY.SuspendLayout();
            this.tbpBuyX.SuspendLayout();
            this.tbpBuyProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyDetails)).BeginInit();
            this.tbpBuyDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyDepartment)).BeginInit();
            this.tbpBuyCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyCategory)).BeginInit();
            this.tbpBuySubCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuySubCategory)).BeginInit();
            this.tbpBuySubCategory2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuySubCategory2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetDetails)).BeginInit();
            this.tbpProductDiscount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDiscount)).BeginInit();
            this.tbpDpartmentDiscount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentDiscount)).BeginInit();
            this.tbpCategoryDiscount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoryDiscount)).BeginInit();
            this.tbpSubCategoryDiscount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory)).BeginInit();
            this.tbpSubCategory2Discount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory2)).BeginInit();
            this.tbpSubTotal.SuspendLayout();
            this.tbpGetGift.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpDays.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).BeginInit();
            this.grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvType)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 513);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(852, 513);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.cmbPromotionType);
            this.grpHeader.Controls.Add(this.lblPromotionType);
            this.grpHeader.Controls.Add(this.chkAutoApply);
            this.grpHeader.Controls.Add(this.btnNew);
            this.grpHeader.Controls.Add(this.chkAutoCompleationPromotion);
            this.grpHeader.Controls.Add(this.txtPromotionCode);
            this.grpHeader.Controls.Add(this.txtPromotionDescription);
            this.grpHeader.Controls.Add(this.lblDescription);
            this.grpHeader.Controls.Add(this.lblCode);
            this.grpHeader.Location = new System.Drawing.Point(2, -5);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(762, 51);
            this.grpHeader.TabIndex = 10;
            this.grpHeader.TabStop = false;
            // 
            // cmbPromotionType
            // 
            this.cmbPromotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotionType.FormattingEnabled = true;
            this.cmbPromotionType.Location = new System.Drawing.Point(482, 26);
            this.cmbPromotionType.Name = "cmbPromotionType";
            this.cmbPromotionType.Size = new System.Drawing.Size(274, 21);
            this.cmbPromotionType.TabIndex = 64;
            this.cmbPromotionType.SelectedIndexChanged += new System.EventHandler(this.cmbPromotionType_SelectedIndexChanged);
            // 
            // lblPromotionType
            // 
            this.lblPromotionType.AutoSize = true;
            this.lblPromotionType.Location = new System.Drawing.Point(479, 9);
            this.lblPromotionType.Name = "lblPromotionType";
            this.lblPromotionType.Size = new System.Drawing.Size(103, 13);
            this.lblPromotionType.TabIndex = 37;
            this.lblPromotionType.Text = "Promotion Type*";
            // 
            // chkAutoApply
            // 
            this.chkAutoApply.AutoSize = true;
            this.chkAutoApply.Location = new System.Drawing.Point(673, 27);
            this.chkAutoApply.Name = "chkAutoApply";
            this.chkAutoApply.Size = new System.Drawing.Size(88, 17);
            this.chkAutoApply.TabIndex = 35;
            this.chkAutoApply.Text = "Auto Apply";
            this.chkAutoApply.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(433, 24);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 34;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationPromotion
            // 
            this.chkAutoCompleationPromotion.AutoSize = true;
            this.chkAutoCompleationPromotion.Checked = true;
            this.chkAutoCompleationPromotion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPromotion.Location = new System.Drawing.Point(3, 28);
            this.chkAutoCompleationPromotion.Name = "chkAutoCompleationPromotion";
            this.chkAutoCompleationPromotion.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPromotion.TabIndex = 33;
            this.chkAutoCompleationPromotion.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPromotion.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPromotion_CheckedChanged);
            // 
            // txtPromotionCode
            // 
            this.txtPromotionCode.IsAutoComplete = false;
            this.txtPromotionCode.ItemCollection = null;
            this.txtPromotionCode.Location = new System.Drawing.Point(20, 25);
            this.txtPromotionCode.MasterCode = "";
            this.txtPromotionCode.Name = "txtPromotionCode";
            this.txtPromotionCode.Size = new System.Drawing.Size(120, 21);
            this.txtPromotionCode.TabIndex = 3;
            this.txtPromotionCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPromotionCode_KeyDown);
            // 
            // txtPromotionDescription
            // 
            this.txtPromotionDescription.Location = new System.Drawing.Point(141, 25);
            this.txtPromotionDescription.MasterDescription = "";
            this.txtPromotionDescription.Name = "txtPromotionDescription";
            this.txtPromotionDescription.Size = new System.Drawing.Size(291, 21);
            this.txtPromotionDescription.TabIndex = 2;
            this.txtPromotionDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPromotionDescription_KeyDown);
            this.txtPromotionDescription.Leave += new System.EventHandler(this.txtPromotionDescription_Leave);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(140, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(140, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Promotion Description*";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(17, 9);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(106, 13);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Promotion Code*";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(122, 29);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(20, 13);
            this.lblToDate.TabIndex = 39;
            this.lblToDate.Text = "To";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(7, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(112, 13);
            this.lblPeriod.TabIndex = 35;
            this.lblPeriod.Text = "Promotion Period*";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(147, 25);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(109, 21);
            this.dtpEndDate.TabIndex = 38;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(8, 25);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(109, 21);
            this.dtpStartDate.TabIndex = 36;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.tbPromotion);
            this.grpBody.Location = new System.Drawing.Point(2, 209);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(830, 309);
            this.grpBody.TabIndex = 11;
            this.grpBody.TabStop = false;
            // 
            // tbPromotion
            // 
            this.tbPromotion.Controls.Add(this.tbpBugXGetY);
            this.tbPromotion.Controls.Add(this.tbpProductDiscount);
            this.tbPromotion.Controls.Add(this.tbpDpartmentDiscount);
            this.tbPromotion.Controls.Add(this.tbpCategoryDiscount);
            this.tbPromotion.Controls.Add(this.tbpSubCategoryDiscount);
            this.tbPromotion.Controls.Add(this.tbpSubCategory2Discount);
            this.tbPromotion.Controls.Add(this.tbpSubTotal);
            this.tbPromotion.Controls.Add(this.tbpGetGift);
            this.tbPromotion.Location = new System.Drawing.Point(4, 11);
            this.tbPromotion.Multiline = true;
            this.tbPromotion.Name = "tbPromotion";
            this.tbPromotion.SelectedIndex = 0;
            this.tbPromotion.Size = new System.Drawing.Size(824, 295);
            this.tbPromotion.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tbPromotion.TabIndex = 6;
            // 
            // tbpBugXGetY
            // 
            this.tbpBugXGetY.Controls.Add(this.txtGetDiscPercentage);
            this.tbpBugXGetY.Controls.Add(this.tbpBuyX);
            this.tbpBugXGetY.Controls.Add(this.txtGetRate);
            this.tbpBugXGetY.Controls.Add(this.txtGetPoints);
            this.tbpBugXGetY.Controls.Add(this.cmbGetUnit);
            this.tbpBugXGetY.Controls.Add(this.txtGetDiscAmount);
            this.tbpBugXGetY.Controls.Add(this.chkAutoCompleationGetProduct);
            this.tbpBugXGetY.Controls.Add(this.txtGetQty);
            this.tbpBugXGetY.Controls.Add(this.txtGetProductName);
            this.tbpBugXGetY.Controls.Add(this.txtGetProductCode);
            this.tbpBugXGetY.Controls.Add(this.dgvGetDetails);
            this.tbpBugXGetY.Location = new System.Drawing.Point(4, 22);
            this.tbpBugXGetY.Name = "tbpBugXGetY";
            this.tbpBugXGetY.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBugXGetY.Size = new System.Drawing.Size(816, 269);
            this.tbpBugXGetY.TabIndex = 0;
            this.tbpBugXGetY.Text = "Buy X Get Y";
            this.tbpBugXGetY.UseVisualStyleBackColor = true;
            // 
            // txtGetDiscPercentage
            // 
            this.txtGetDiscPercentage.Location = new System.Drawing.Point(667, 244);
            this.txtGetDiscPercentage.Name = "txtGetDiscPercentage";
            this.txtGetDiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGetDiscPercentage.Size = new System.Drawing.Size(53, 21);
            this.txtGetDiscPercentage.TabIndex = 20;
            this.txtGetDiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGetDiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetDiscPercentage_KeyDown);
            this.txtGetDiscPercentage.Validated += new System.EventHandler(this.txtGetDiscPercentage_Validated);
            // 
            // tbpBuyX
            // 
            this.tbpBuyX.Controls.Add(this.tbpBuyProduct);
            this.tbpBuyX.Controls.Add(this.tbpBuyDepartment);
            this.tbpBuyX.Controls.Add(this.tbpBuyCategory);
            this.tbpBuyX.Controls.Add(this.tbpBuySubCategory);
            this.tbpBuyX.Controls.Add(this.tbpBuySubCategory2);
            this.tbpBuyX.Location = new System.Drawing.Point(1, 1);
            this.tbpBuyX.Name = "tbpBuyX";
            this.tbpBuyX.SelectedIndex = 0;
            this.tbpBuyX.Size = new System.Drawing.Size(813, 146);
            this.tbpBuyX.TabIndex = 17;
            // 
            // tbpBuyProduct
            // 
            this.tbpBuyProduct.Controls.Add(this.ChkAutoSelectGetProduct);
            this.tbpBuyProduct.Controls.Add(this.txtBuyRate);
            this.tbpBuyProduct.Controls.Add(this.cmbBuyUnit);
            this.tbpBuyProduct.Controls.Add(this.chkAutoCompleationBuyProduct);
            this.tbpBuyProduct.Controls.Add(this.txtBuyQty);
            this.tbpBuyProduct.Controls.Add(this.txtBuyProductName);
            this.tbpBuyProduct.Controls.Add(this.txtBuyProductCode);
            this.tbpBuyProduct.Controls.Add(this.dgvBuyDetails);
            this.tbpBuyProduct.Location = new System.Drawing.Point(4, 22);
            this.tbpBuyProduct.Name = "tbpBuyProduct";
            this.tbpBuyProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBuyProduct.Size = new System.Drawing.Size(805, 120);
            this.tbpBuyProduct.TabIndex = 0;
            this.tbpBuyProduct.Text = "Product";
            this.tbpBuyProduct.UseVisualStyleBackColor = true;
            // 
            // ChkAutoSelectGetProduct
            // 
            this.ChkAutoSelectGetProduct.AutoSize = true;
            this.ChkAutoSelectGetProduct.Location = new System.Drawing.Point(646, 6);
            this.ChkAutoSelectGetProduct.Name = "ChkAutoSelectGetProduct";
            this.ChkAutoSelectGetProduct.Size = new System.Drawing.Size(162, 17);
            this.ChkAutoSelectGetProduct.TabIndex = 23;
            this.ChkAutoSelectGetProduct.Text = "Auto Select Get Product";
            this.ChkAutoSelectGetProduct.UseVisualStyleBackColor = true;
            // 
            // txtBuyRate
            // 
            this.txtBuyRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyRate.Location = new System.Drawing.Point(463, 96);
            this.txtBuyRate.Name = "txtBuyRate";
            this.txtBuyRate.Size = new System.Drawing.Size(98, 21);
            this.txtBuyRate.TabIndex = 22;
            this.txtBuyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbBuyUnit
            // 
            this.cmbBuyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuyUnit.FormattingEnabled = true;
            this.cmbBuyUnit.Location = new System.Drawing.Point(396, 96);
            this.cmbBuyUnit.Name = "cmbBuyUnit";
            this.cmbBuyUnit.Size = new System.Drawing.Size(66, 21);
            this.cmbBuyUnit.TabIndex = 21;
            // 
            // chkAutoCompleationBuyProduct
            // 
            this.chkAutoCompleationBuyProduct.AutoSize = true;
            this.chkAutoCompleationBuyProduct.Checked = true;
            this.chkAutoCompleationBuyProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBuyProduct.Location = new System.Drawing.Point(0, 99);
            this.chkAutoCompleationBuyProduct.Name = "chkAutoCompleationBuyProduct";
            this.chkAutoCompleationBuyProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBuyProduct.TabIndex = 20;
            this.chkAutoCompleationBuyProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBuyProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBuyProduct_CheckedChanged_1);
            // 
            // txtBuyQty
            // 
            this.txtBuyQty.Location = new System.Drawing.Point(562, 96);
            this.txtBuyQty.Name = "txtBuyQty";
            this.txtBuyQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyQty.Size = new System.Drawing.Size(82, 21);
            this.txtBuyQty.TabIndex = 19;
            this.txtBuyQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyQty_KeyDown);
            // 
            // txtBuyProductName
            // 
            this.txtBuyProductName.Location = new System.Drawing.Point(145, 96);
            this.txtBuyProductName.MasterDescription = "";
            this.txtBuyProductName.Name = "txtBuyProductName";
            this.txtBuyProductName.Size = new System.Drawing.Size(250, 21);
            this.txtBuyProductName.TabIndex = 18;
            this.txtBuyProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyProductName_KeyDown);
            this.txtBuyProductName.Leave += new System.EventHandler(this.txtBuyProductName_Leave);
            // 
            // txtBuyProductCode
            // 
            this.txtBuyProductCode.Location = new System.Drawing.Point(16, 96);
            this.txtBuyProductCode.Name = "txtBuyProductCode";
            this.txtBuyProductCode.ProductCode = "";
            this.txtBuyProductCode.Size = new System.Drawing.Size(128, 21);
            this.txtBuyProductCode.TabIndex = 17;
            this.txtBuyProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyProductCode_KeyDown);
            this.txtBuyProductCode.Validated += new System.EventHandler(this.txtBuyProductCode_Validated);
            // 
            // dgvBuyDetails
            // 
            this.dgvBuyDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuyDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BuyProductCode,
            this.BuyProductName,
            this.BuyUnit,
            this.BuyRate,
            this.BuyQty});
            this.dgvBuyDetails.Location = new System.Drawing.Point(0, 3);
            this.dgvBuyDetails.Name = "dgvBuyDetails";
            this.dgvBuyDetails.RowHeadersWidth = 5;
            this.dgvBuyDetails.Size = new System.Drawing.Size(644, 90);
            this.dgvBuyDetails.TabIndex = 16;
            // 
            // BuyProductCode
            // 
            this.BuyProductCode.DataPropertyName = "BuyProductCode";
            this.BuyProductCode.HeaderText = "Buy Product Code";
            this.BuyProductCode.Name = "BuyProductCode";
            this.BuyProductCode.Width = 140;
            // 
            // BuyProductName
            // 
            this.BuyProductName.DataPropertyName = "BuyProductName";
            this.BuyProductName.HeaderText = "Buy Product Name";
            this.BuyProductName.Name = "BuyProductName";
            this.BuyProductName.Width = 250;
            // 
            // BuyUnit
            // 
            this.BuyUnit.DataPropertyName = "UnitOfMeasure";
            this.BuyUnit.FillWeight = 70F;
            this.BuyUnit.HeaderText = "Unit";
            this.BuyUnit.Name = "BuyUnit";
            this.BuyUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BuyUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BuyUnit.Width = 65;
            // 
            // BuyRate
            // 
            this.BuyRate.DataPropertyName = "BuyRate";
            this.BuyRate.HeaderText = "Rate";
            this.BuyRate.Name = "BuyRate";
            // 
            // BuyQty
            // 
            this.BuyQty.DataPropertyName = "BuyQty";
            this.BuyQty.HeaderText = "Qty";
            this.BuyQty.Name = "BuyQty";
            this.BuyQty.Width = 60;
            // 
            // tbpBuyDepartment
            // 
            this.tbpBuyDepartment.Controls.Add(this.chkAutoCompleationBuyDepartment);
            this.tbpBuyDepartment.Controls.Add(this.txtBuyDepartmentQty);
            this.tbpBuyDepartment.Controls.Add(this.txtBuyDepartmentName);
            this.tbpBuyDepartment.Controls.Add(this.txtBuyDepartmentCode);
            this.tbpBuyDepartment.Controls.Add(this.dgvBuyDepartment);
            this.tbpBuyDepartment.Location = new System.Drawing.Point(4, 22);
            this.tbpBuyDepartment.Name = "tbpBuyDepartment";
            this.tbpBuyDepartment.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBuyDepartment.Size = new System.Drawing.Size(805, 120);
            this.tbpBuyDepartment.TabIndex = 1;
            this.tbpBuyDepartment.Text = "Department";
            this.tbpBuyDepartment.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationBuyDepartment
            // 
            this.chkAutoCompleationBuyDepartment.AutoSize = true;
            this.chkAutoCompleationBuyDepartment.Checked = true;
            this.chkAutoCompleationBuyDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBuyDepartment.Location = new System.Drawing.Point(0, 99);
            this.chkAutoCompleationBuyDepartment.Name = "chkAutoCompleationBuyDepartment";
            this.chkAutoCompleationBuyDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBuyDepartment.TabIndex = 27;
            this.chkAutoCompleationBuyDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBuyDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBuyDepartment_CheckedChanged);
            // 
            // txtBuyDepartmentQty
            // 
            this.txtBuyDepartmentQty.Location = new System.Drawing.Point(396, 96);
            this.txtBuyDepartmentQty.Name = "txtBuyDepartmentQty";
            this.txtBuyDepartmentQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyDepartmentQty.Size = new System.Drawing.Size(82, 21);
            this.txtBuyDepartmentQty.TabIndex = 26;
            this.txtBuyDepartmentQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyDepartmentQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyDepartmentQty_KeyDown);
            // 
            // txtBuyDepartmentName
            // 
            this.txtBuyDepartmentName.Location = new System.Drawing.Point(145, 96);
            this.txtBuyDepartmentName.MasterDescription = "";
            this.txtBuyDepartmentName.Name = "txtBuyDepartmentName";
            this.txtBuyDepartmentName.Size = new System.Drawing.Size(250, 21);
            this.txtBuyDepartmentName.TabIndex = 25;
            this.txtBuyDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyDepartmentName_KeyDown);
            this.txtBuyDepartmentName.Leave += new System.EventHandler(this.txtBuyDepartmentName_Leave);
            // 
            // txtBuyDepartmentCode
            // 
            this.txtBuyDepartmentCode.Location = new System.Drawing.Point(16, 96);
            this.txtBuyDepartmentCode.Name = "txtBuyDepartmentCode";
            this.txtBuyDepartmentCode.ProductCode = "";
            this.txtBuyDepartmentCode.Size = new System.Drawing.Size(128, 21);
            this.txtBuyDepartmentCode.TabIndex = 24;
            this.txtBuyDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyDepartmentCode_KeyDown);
            this.txtBuyDepartmentCode.Validated += new System.EventHandler(this.txtBuyDepartmentCode_Validated);
            // 
            // dgvBuyDepartment
            // 
            this.dgvBuyDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuyDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn27});
            this.dgvBuyDepartment.Location = new System.Drawing.Point(0, 3);
            this.dgvBuyDepartment.Name = "dgvBuyDepartment";
            this.dgvBuyDepartment.RowHeadersWidth = 5;
            this.dgvBuyDepartment.Size = new System.Drawing.Size(478, 90);
            this.dgvBuyDepartment.TabIndex = 23;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "DepartmentCode";
            this.dataGridViewTextBoxColumn23.HeaderText = "Code";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.Width = 140;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "DepartmentName";
            this.dataGridViewTextBoxColumn24.HeaderText = "Name";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.Width = 250;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "BuyQty";
            this.dataGridViewTextBoxColumn27.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.Width = 60;
            // 
            // tbpBuyCategory
            // 
            this.tbpBuyCategory.Controls.Add(this.chkAutoCompleationBuyCaytegory);
            this.tbpBuyCategory.Controls.Add(this.txtBuyCategoryQty);
            this.tbpBuyCategory.Controls.Add(this.txtBuyCategoryName);
            this.tbpBuyCategory.Controls.Add(this.txtBuyCategoryCode);
            this.tbpBuyCategory.Controls.Add(this.dgvBuyCategory);
            this.tbpBuyCategory.Location = new System.Drawing.Point(4, 22);
            this.tbpBuyCategory.Name = "tbpBuyCategory";
            this.tbpBuyCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBuyCategory.Size = new System.Drawing.Size(805, 120);
            this.tbpBuyCategory.TabIndex = 2;
            this.tbpBuyCategory.Text = "Category";
            this.tbpBuyCategory.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationBuyCaytegory
            // 
            this.chkAutoCompleationBuyCaytegory.AutoSize = true;
            this.chkAutoCompleationBuyCaytegory.Checked = true;
            this.chkAutoCompleationBuyCaytegory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBuyCaytegory.Location = new System.Drawing.Point(0, 99);
            this.chkAutoCompleationBuyCaytegory.Name = "chkAutoCompleationBuyCaytegory";
            this.chkAutoCompleationBuyCaytegory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBuyCaytegory.TabIndex = 27;
            this.chkAutoCompleationBuyCaytegory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBuyCaytegory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBuyCaytego_CheckedChanged);
            // 
            // txtBuyCategoryQty
            // 
            this.txtBuyCategoryQty.Location = new System.Drawing.Point(396, 96);
            this.txtBuyCategoryQty.Name = "txtBuyCategoryQty";
            this.txtBuyCategoryQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuyCategoryQty.Size = new System.Drawing.Size(82, 21);
            this.txtBuyCategoryQty.TabIndex = 26;
            this.txtBuyCategoryQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyCategoryQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyCategoryQty_KeyDown);
            // 
            // txtBuyCategoryName
            // 
            this.txtBuyCategoryName.Location = new System.Drawing.Point(145, 96);
            this.txtBuyCategoryName.MasterDescription = "";
            this.txtBuyCategoryName.Name = "txtBuyCategoryName";
            this.txtBuyCategoryName.Size = new System.Drawing.Size(250, 21);
            this.txtBuyCategoryName.TabIndex = 25;
            this.txtBuyCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyCategoryName_KeyDown);
            this.txtBuyCategoryName.Leave += new System.EventHandler(this.txtBuyCategoryName_Leave);
            // 
            // txtBuyCategoryCode
            // 
            this.txtBuyCategoryCode.Location = new System.Drawing.Point(16, 96);
            this.txtBuyCategoryCode.Name = "txtBuyCategoryCode";
            this.txtBuyCategoryCode.ProductCode = "";
            this.txtBuyCategoryCode.Size = new System.Drawing.Size(128, 21);
            this.txtBuyCategoryCode.TabIndex = 24;
            this.txtBuyCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuyCategoryCode_KeyDown);
            this.txtBuyCategoryCode.Validated += new System.EventHandler(this.txtBuyCategoryCode_Validated);
            // 
            // dgvBuyCategory
            // 
            this.dgvBuyCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuyCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn32});
            this.dgvBuyCategory.Location = new System.Drawing.Point(0, 3);
            this.dgvBuyCategory.Name = "dgvBuyCategory";
            this.dgvBuyCategory.RowHeadersWidth = 5;
            this.dgvBuyCategory.Size = new System.Drawing.Size(478, 90);
            this.dgvBuyCategory.TabIndex = 23;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "CategoryCode";
            this.dataGridViewTextBoxColumn28.HeaderText = "Code";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.Width = 140;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.DataPropertyName = "CategoryName";
            this.dataGridViewTextBoxColumn29.HeaderText = "Name";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.Width = 250;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.DataPropertyName = "BuyQty";
            this.dataGridViewTextBoxColumn32.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.Width = 60;
            // 
            // tbpBuySubCategory
            // 
            this.tbpBuySubCategory.Controls.Add(this.chkAutoCompleationBuySubCategory);
            this.tbpBuySubCategory.Controls.Add(this.txtBuySubCategoryQty);
            this.tbpBuySubCategory.Controls.Add(this.txtBuySubCategoryName);
            this.tbpBuySubCategory.Controls.Add(this.txtBuySubCategoryCode);
            this.tbpBuySubCategory.Controls.Add(this.dgvBuySubCategory);
            this.tbpBuySubCategory.Location = new System.Drawing.Point(4, 22);
            this.tbpBuySubCategory.Name = "tbpBuySubCategory";
            this.tbpBuySubCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBuySubCategory.Size = new System.Drawing.Size(805, 120);
            this.tbpBuySubCategory.TabIndex = 3;
            this.tbpBuySubCategory.Text = "Sub Category";
            this.tbpBuySubCategory.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationBuySubCategory
            // 
            this.chkAutoCompleationBuySubCategory.AutoSize = true;
            this.chkAutoCompleationBuySubCategory.Checked = true;
            this.chkAutoCompleationBuySubCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBuySubCategory.Location = new System.Drawing.Point(0, 99);
            this.chkAutoCompleationBuySubCategory.Name = "chkAutoCompleationBuySubCategory";
            this.chkAutoCompleationBuySubCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBuySubCategory.TabIndex = 27;
            this.chkAutoCompleationBuySubCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBuySubCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBuySubCategory_CheckedChanged);
            // 
            // txtBuySubCategoryQty
            // 
            this.txtBuySubCategoryQty.Location = new System.Drawing.Point(396, 96);
            this.txtBuySubCategoryQty.Name = "txtBuySubCategoryQty";
            this.txtBuySubCategoryQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuySubCategoryQty.Size = new System.Drawing.Size(82, 21);
            this.txtBuySubCategoryQty.TabIndex = 26;
            this.txtBuySubCategoryQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuySubCategoryQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategoryQty_KeyDown);
            // 
            // txtBuySubCategoryName
            // 
            this.txtBuySubCategoryName.Location = new System.Drawing.Point(145, 96);
            this.txtBuySubCategoryName.MasterDescription = "";
            this.txtBuySubCategoryName.Name = "txtBuySubCategoryName";
            this.txtBuySubCategoryName.Size = new System.Drawing.Size(250, 21);
            this.txtBuySubCategoryName.TabIndex = 25;
            this.txtBuySubCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategoryName_KeyDown);
            this.txtBuySubCategoryName.Leave += new System.EventHandler(this.txtBuySubCategoryName_Leave);
            // 
            // txtBuySubCategoryCode
            // 
            this.txtBuySubCategoryCode.Location = new System.Drawing.Point(16, 96);
            this.txtBuySubCategoryCode.Name = "txtBuySubCategoryCode";
            this.txtBuySubCategoryCode.ProductCode = "";
            this.txtBuySubCategoryCode.Size = new System.Drawing.Size(128, 21);
            this.txtBuySubCategoryCode.TabIndex = 24;
            this.txtBuySubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategoryCode_KeyDown);
            this.txtBuySubCategoryCode.Validated += new System.EventHandler(this.txtBuySubCategoryCode_Validated);
            // 
            // dgvBuySubCategory
            // 
            this.dgvBuySubCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuySubCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn37});
            this.dgvBuySubCategory.Location = new System.Drawing.Point(0, 3);
            this.dgvBuySubCategory.Name = "dgvBuySubCategory";
            this.dgvBuySubCategory.RowHeadersWidth = 5;
            this.dgvBuySubCategory.Size = new System.Drawing.Size(478, 90);
            this.dgvBuySubCategory.TabIndex = 23;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "SubCategoryCode";
            this.dataGridViewTextBoxColumn33.HeaderText = "Code";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.Width = 140;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "SubCategoryName";
            this.dataGridViewTextBoxColumn34.HeaderText = "Name";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.Width = 250;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.DataPropertyName = "BuyQty";
            this.dataGridViewTextBoxColumn37.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.Width = 60;
            // 
            // tbpBuySubCategory2
            // 
            this.tbpBuySubCategory2.Controls.Add(this.chkAutoCompleationBuySubCategory2);
            this.tbpBuySubCategory2.Controls.Add(this.txtBuySubCategory2Qty);
            this.tbpBuySubCategory2.Controls.Add(this.txtBuySubCategory2Name);
            this.tbpBuySubCategory2.Controls.Add(this.txtBuySubCategory2Code);
            this.tbpBuySubCategory2.Controls.Add(this.dgvBuySubCategory2);
            this.tbpBuySubCategory2.Location = new System.Drawing.Point(4, 22);
            this.tbpBuySubCategory2.Name = "tbpBuySubCategory2";
            this.tbpBuySubCategory2.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBuySubCategory2.Size = new System.Drawing.Size(805, 120);
            this.tbpBuySubCategory2.TabIndex = 4;
            this.tbpBuySubCategory2.Text = "Sub Category 2";
            this.tbpBuySubCategory2.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationBuySubCategory2
            // 
            this.chkAutoCompleationBuySubCategory2.AutoSize = true;
            this.chkAutoCompleationBuySubCategory2.Checked = true;
            this.chkAutoCompleationBuySubCategory2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationBuySubCategory2.Location = new System.Drawing.Point(0, 99);
            this.chkAutoCompleationBuySubCategory2.Name = "chkAutoCompleationBuySubCategory2";
            this.chkAutoCompleationBuySubCategory2.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationBuySubCategory2.TabIndex = 32;
            this.chkAutoCompleationBuySubCategory2.UseVisualStyleBackColor = true;
            this.chkAutoCompleationBuySubCategory2.CheckedChanged += new System.EventHandler(this.chkAutoCompleationBuySubCategory2_CheckedChanged);
            // 
            // txtBuySubCategory2Qty
            // 
            this.txtBuySubCategory2Qty.Location = new System.Drawing.Point(396, 96);
            this.txtBuySubCategory2Qty.Name = "txtBuySubCategory2Qty";
            this.txtBuySubCategory2Qty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuySubCategory2Qty.Size = new System.Drawing.Size(82, 21);
            this.txtBuySubCategory2Qty.TabIndex = 31;
            this.txtBuySubCategory2Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuySubCategory2Qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategory2Qty_KeyDown);
            // 
            // txtBuySubCategory2Name
            // 
            this.txtBuySubCategory2Name.Location = new System.Drawing.Point(145, 96);
            this.txtBuySubCategory2Name.MasterDescription = "";
            this.txtBuySubCategory2Name.Name = "txtBuySubCategory2Name";
            this.txtBuySubCategory2Name.Size = new System.Drawing.Size(250, 21);
            this.txtBuySubCategory2Name.TabIndex = 30;
            this.txtBuySubCategory2Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategory2Name_KeyDown);
            this.txtBuySubCategory2Name.Leave += new System.EventHandler(this.txtBuySubCategory2Name_Leave);
            // 
            // txtBuySubCategory2Code
            // 
            this.txtBuySubCategory2Code.Location = new System.Drawing.Point(16, 96);
            this.txtBuySubCategory2Code.Name = "txtBuySubCategory2Code";
            this.txtBuySubCategory2Code.ProductCode = "";
            this.txtBuySubCategory2Code.Size = new System.Drawing.Size(128, 21);
            this.txtBuySubCategory2Code.TabIndex = 29;
            this.txtBuySubCategory2Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuySubCategory2Code_KeyDown);
            this.txtBuySubCategory2Code.Validated += new System.EventHandler(this.txtBuySubCategory2Code_Validated);
            // 
            // dgvBuySubCategory2
            // 
            this.dgvBuySubCategory2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuySubCategory2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn30});
            this.dgvBuySubCategory2.Location = new System.Drawing.Point(0, 3);
            this.dgvBuySubCategory2.Name = "dgvBuySubCategory2";
            this.dgvBuySubCategory2.RowHeadersWidth = 5;
            this.dgvBuySubCategory2.Size = new System.Drawing.Size(478, 90);
            this.dgvBuySubCategory2.TabIndex = 28;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "SubCategory2Code";
            this.dataGridViewTextBoxColumn25.HeaderText = "Code";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 140;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "SubCategory2Name";
            this.dataGridViewTextBoxColumn26.HeaderText = "Name";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.Width = 250;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.DataPropertyName = "BuyQty";
            this.dataGridViewTextBoxColumn30.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.Width = 60;
            // 
            // txtGetRate
            // 
            this.txtGetRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGetRate.Location = new System.Drawing.Point(467, 244);
            this.txtGetRate.Name = "txtGetRate";
            this.txtGetRate.Size = new System.Drawing.Size(77, 21);
            this.txtGetRate.TabIndex = 16;
            this.txtGetRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGetPoints
            // 
            this.txtGetPoints.Location = new System.Drawing.Point(604, 244);
            this.txtGetPoints.Name = "txtGetPoints";
            this.txtGetPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGetPoints.Size = new System.Drawing.Size(62, 21);
            this.txtGetPoints.TabIndex = 14;
            this.txtGetPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGetPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetPoints_KeyDown);
            // 
            // cmbGetUnit
            // 
            this.cmbGetUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGetUnit.FormattingEnabled = true;
            this.cmbGetUnit.Location = new System.Drawing.Point(400, 244);
            this.cmbGetUnit.Name = "cmbGetUnit";
            this.cmbGetUnit.Size = new System.Drawing.Size(66, 21);
            this.cmbGetUnit.TabIndex = 13;
            // 
            // txtGetDiscAmount
            // 
            this.txtGetDiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGetDiscAmount.Location = new System.Drawing.Point(721, 244);
            this.txtGetDiscAmount.Name = "txtGetDiscAmount";
            this.txtGetDiscAmount.Size = new System.Drawing.Size(92, 21);
            this.txtGetDiscAmount.TabIndex = 11;
            this.txtGetDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGetDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetDiscAmount_KeyDown);
            // 
            // chkAutoCompleationGetProduct
            // 
            this.chkAutoCompleationGetProduct.AutoSize = true;
            this.chkAutoCompleationGetProduct.Checked = true;
            this.chkAutoCompleationGetProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGetProduct.Location = new System.Drawing.Point(4, 247);
            this.chkAutoCompleationGetProduct.Name = "chkAutoCompleationGetProduct";
            this.chkAutoCompleationGetProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGetProduct.TabIndex = 9;
            this.chkAutoCompleationGetProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGetProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGetProduct_CheckedChanged);
            // 
            // txtGetQty
            // 
            this.txtGetQty.Location = new System.Drawing.Point(545, 244);
            this.txtGetQty.Name = "txtGetQty";
            this.txtGetQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGetQty.Size = new System.Drawing.Size(58, 21);
            this.txtGetQty.TabIndex = 8;
            this.txtGetQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGetQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetQty_KeyDown);
            // 
            // txtGetProductName
            // 
            this.txtGetProductName.Location = new System.Drawing.Point(149, 244);
            this.txtGetProductName.MasterDescription = "";
            this.txtGetProductName.Name = "txtGetProductName";
            this.txtGetProductName.Size = new System.Drawing.Size(250, 21);
            this.txtGetProductName.TabIndex = 7;
            this.txtGetProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetProductName_KeyDown);
            this.txtGetProductName.Leave += new System.EventHandler(this.txtGetProductName_Leave);
            // 
            // txtGetProductCode
            // 
            this.txtGetProductCode.Location = new System.Drawing.Point(20, 244);
            this.txtGetProductCode.Name = "txtGetProductCode";
            this.txtGetProductCode.ProductCode = "";
            this.txtGetProductCode.Size = new System.Drawing.Size(128, 21);
            this.txtGetProductCode.TabIndex = 6;
            this.txtGetProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGetProductCode_KeyDown);
            this.txtGetProductCode.Validated += new System.EventHandler(this.txtGetProductCode_Validated);
            // 
            // dgvGetDetails
            // 
            this.dgvGetDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGetDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GetProductCode,
            this.GetProductName,
            this.GetUnit,
            this.GetRate,
            this.GetQty,
            this.GetPoints,
            this.GetDiscPer,
            this.GetDiscAmount});
            this.dgvGetDetails.Location = new System.Drawing.Point(4, 151);
            this.dgvGetDetails.Name = "dgvGetDetails";
            this.dgvGetDetails.RowHeadersWidth = 5;
            this.dgvGetDetails.Size = new System.Drawing.Size(810, 91);
            this.dgvGetDetails.TabIndex = 5;
            // 
            // GetProductCode
            // 
            this.GetProductCode.DataPropertyName = "ProductCode";
            this.GetProductCode.HeaderText = "Get Code";
            this.GetProductCode.Name = "GetProductCode";
            this.GetProductCode.Width = 140;
            // 
            // GetProductName
            // 
            this.GetProductName.DataPropertyName = "ProductName";
            this.GetProductName.HeaderText = "Get Name";
            this.GetProductName.Name = "GetProductName";
            this.GetProductName.Width = 250;
            // 
            // GetUnit
            // 
            this.GetUnit.DataPropertyName = "UnitOfMeasure";
            this.GetUnit.HeaderText = "Unit";
            this.GetUnit.Name = "GetUnit";
            this.GetUnit.Width = 65;
            // 
            // GetRate
            // 
            this.GetRate.DataPropertyName = "GetRate";
            this.GetRate.HeaderText = "Rate";
            this.GetRate.Name = "GetRate";
            this.GetRate.Width = 80;
            // 
            // GetQty
            // 
            this.GetQty.DataPropertyName = "GetQty";
            this.GetQty.HeaderText = "Qty";
            this.GetQty.Name = "GetQty";
            this.GetQty.Width = 57;
            // 
            // GetPoints
            // 
            this.GetPoints.DataPropertyName = "GetPoints";
            this.GetPoints.HeaderText = "Points";
            this.GetPoints.Name = "GetPoints";
            this.GetPoints.Width = 65;
            // 
            // GetDiscPer
            // 
            this.GetDiscPer.DataPropertyName = "GetDiscountPercentage";
            this.GetDiscPer.HeaderText = "Disc %";
            this.GetDiscPer.Name = "GetDiscPer";
            this.GetDiscPer.Width = 55;
            // 
            // GetDiscAmount
            // 
            this.GetDiscAmount.DataPropertyName = "GetDiscountAmount";
            this.GetDiscAmount.HeaderText = "Disc Amount";
            this.GetDiscAmount.Name = "GetDiscAmount";
            this.GetDiscAmount.Width = 70;
            // 
            // tbpProductDiscount
            // 
            this.tbpProductDiscount.Controls.Add(this.txtProductDiscPercentage);
            this.tbpProductDiscount.Controls.Add(this.txtProductRate);
            this.tbpProductDiscount.Controls.Add(this.txtProductPoints);
            this.tbpProductDiscount.Controls.Add(this.cmbProductUnit);
            this.tbpProductDiscount.Controls.Add(this.txtProductDiscAmount);
            this.tbpProductDiscount.Controls.Add(this.chkAutoCompleationPdisProduct);
            this.tbpProductDiscount.Controls.Add(this.txtProductQty);
            this.tbpProductDiscount.Controls.Add(this.txtProductProductName);
            this.tbpProductDiscount.Controls.Add(this.txtProductProductCode);
            this.tbpProductDiscount.Controls.Add(this.dgvProductDiscount);
            this.tbpProductDiscount.Location = new System.Drawing.Point(4, 22);
            this.tbpProductDiscount.Name = "tbpProductDiscount";
            this.tbpProductDiscount.Size = new System.Drawing.Size(816, 269);
            this.tbpProductDiscount.TabIndex = 1;
            this.tbpProductDiscount.Text = "Product Dis";
            this.tbpProductDiscount.UseVisualStyleBackColor = true;
            // 
            // txtProductDiscPercentage
            // 
            this.txtProductDiscPercentage.Location = new System.Drawing.Point(667, 244);
            this.txtProductDiscPercentage.Name = "txtProductDiscPercentage";
            this.txtProductDiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscPercentage.Size = new System.Drawing.Size(55, 21);
            this.txtProductDiscPercentage.TabIndex = 24;
            this.txtProductDiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscPercentage_KeyDown);
            this.txtProductDiscPercentage.Validated += new System.EventHandler(this.txtProductDiscPercentage_Validated);
            // 
            // txtProductRate
            // 
            this.txtProductRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductRate.Location = new System.Drawing.Point(463, 244);
            this.txtProductRate.Name = "txtProductRate";
            this.txtProductRate.Size = new System.Drawing.Size(74, 21);
            this.txtProductRate.TabIndex = 23;
            this.txtProductRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtProductPoints
            // 
            this.txtProductPoints.Location = new System.Drawing.Point(595, 244);
            this.txtProductPoints.Name = "txtProductPoints";
            this.txtProductPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductPoints.Size = new System.Drawing.Size(71, 21);
            this.txtProductPoints.TabIndex = 22;
            this.txtProductPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductPoints_KeyDown);
            // 
            // cmbProductUnit
            // 
            this.cmbProductUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductUnit.FormattingEnabled = true;
            this.cmbProductUnit.Location = new System.Drawing.Point(400, 244);
            this.cmbProductUnit.Name = "cmbProductUnit";
            this.cmbProductUnit.Size = new System.Drawing.Size(62, 21);
            this.cmbProductUnit.TabIndex = 21;
            // 
            // txtProductDiscAmount
            // 
            this.txtProductDiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductDiscAmount.Location = new System.Drawing.Point(723, 244);
            this.txtProductDiscAmount.Name = "txtProductDiscAmount";
            this.txtProductDiscAmount.Size = new System.Drawing.Size(91, 21);
            this.txtProductDiscAmount.TabIndex = 19;
            this.txtProductDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDiscAmount_KeyDown);
            // 
            // chkAutoCompleationPdisProduct
            // 
            this.chkAutoCompleationPdisProduct.AutoSize = true;
            this.chkAutoCompleationPdisProduct.Checked = true;
            this.chkAutoCompleationPdisProduct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPdisProduct.Location = new System.Drawing.Point(4, 247);
            this.chkAutoCompleationPdisProduct.Name = "chkAutoCompleationPdisProduct";
            this.chkAutoCompleationPdisProduct.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPdisProduct.TabIndex = 18;
            this.chkAutoCompleationPdisProduct.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPdisProduct.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPdisProduct_CheckedChanged);
            // 
            // txtProductQty
            // 
            this.txtProductQty.Location = new System.Drawing.Point(538, 244);
            this.txtProductQty.Name = "txtProductQty";
            this.txtProductQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductQty.Size = new System.Drawing.Size(56, 21);
            this.txtProductQty.TabIndex = 17;
            this.txtProductQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductQty_KeyDown);
            // 
            // txtProductProductName
            // 
            this.txtProductProductName.Location = new System.Drawing.Point(149, 244);
            this.txtProductProductName.MasterDescription = "";
            this.txtProductProductName.Name = "txtProductProductName";
            this.txtProductProductName.Size = new System.Drawing.Size(250, 21);
            this.txtProductProductName.TabIndex = 16;
            this.txtProductProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDisProductName_KeyDown);
            this.txtProductProductName.Leave += new System.EventHandler(this.txtProductDisProductName_Leave);
            // 
            // txtProductProductCode
            // 
            this.txtProductProductCode.Location = new System.Drawing.Point(20, 244);
            this.txtProductProductCode.Name = "txtProductProductCode";
            this.txtProductProductCode.ProductCode = "";
            this.txtProductProductCode.Size = new System.Drawing.Size(128, 21);
            this.txtProductProductCode.TabIndex = 15;
            this.txtProductProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductDisProductCode_KeyDown);
            this.txtProductProductCode.Validated += new System.EventHandler(this.txtProductDisProductCode_Validated);
            // 
            // dgvProductDiscount
            // 
            this.dgvProductDiscount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductDiscount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.ProductRate,
            this.dataGridViewTextBoxColumn4,
            this.ProductPoints,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn5});
            this.dgvProductDiscount.Location = new System.Drawing.Point(3, 6);
            this.dgvProductDiscount.Name = "dgvProductDiscount";
            this.dgvProductDiscount.RowHeadersWidth = 5;
            this.dgvProductDiscount.Size = new System.Drawing.Size(810, 235);
            this.dgvProductDiscount.TabIndex = 14;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProductCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "Product Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProductName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Product Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UnitOfMeasure";
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // ProductRate
            // 
            this.ProductRate.DataPropertyName = "Rate";
            this.ProductRate.HeaderText = "Rate";
            this.ProductRate.Name = "ProductRate";
            this.ProductRate.Width = 75;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn4.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // ProductPoints
            // 
            this.ProductPoints.DataPropertyName = "Points";
            this.ProductPoints.HeaderText = "Points";
            this.ProductPoints.Name = "ProductPoints";
            this.ProductPoints.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DiscountPercentage";
            this.dataGridViewTextBoxColumn6.HeaderText = "Disc %";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DiscountAmount";
            this.dataGridViewTextBoxColumn5.HeaderText = "Disc Amount";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // tbpDpartmentDiscount
            // 
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentDiscPercentage);
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentPoints);
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentDiscAmount);
            this.tbpDpartmentDiscount.Controls.Add(this.chkAutoCompleationDepartment);
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentQty);
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentDescription);
            this.tbpDpartmentDiscount.Controls.Add(this.txtDepartmentCode);
            this.tbpDpartmentDiscount.Controls.Add(this.dgvDepartmentDiscount);
            this.tbpDpartmentDiscount.Location = new System.Drawing.Point(4, 22);
            this.tbpDpartmentDiscount.Name = "tbpDpartmentDiscount";
            this.tbpDpartmentDiscount.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDpartmentDiscount.Size = new System.Drawing.Size(816, 269);
            this.tbpDpartmentDiscount.TabIndex = 2;
            this.tbpDpartmentDiscount.Text = "Department Dis";
            this.tbpDpartmentDiscount.UseVisualStyleBackColor = true;
            // 
            // txtDepartmentDiscPercentage
            // 
            this.txtDepartmentDiscPercentage.Location = new System.Drawing.Point(561, 244);
            this.txtDepartmentDiscPercentage.Name = "txtDepartmentDiscPercentage";
            this.txtDepartmentDiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDepartmentDiscPercentage.Size = new System.Drawing.Size(70, 21);
            this.txtDepartmentDiscPercentage.TabIndex = 30;
            this.txtDepartmentDiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDepartmentDiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentDiscPercentage_KeyDown);
            this.txtDepartmentDiscPercentage.Validated += new System.EventHandler(this.txtDepartmentDiscPercentage_Validated);
            // 
            // txtDepartmentPoints
            // 
            this.txtDepartmentPoints.Location = new System.Drawing.Point(475, 244);
            this.txtDepartmentPoints.Name = "txtDepartmentPoints";
            this.txtDepartmentPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDepartmentPoints.Size = new System.Drawing.Size(85, 21);
            this.txtDepartmentPoints.TabIndex = 29;
            this.txtDepartmentPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDepartmentPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentPoints_KeyDown);
            // 
            // txtDepartmentDiscAmount
            // 
            this.txtDepartmentDiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDepartmentDiscAmount.Location = new System.Drawing.Point(633, 244);
            this.txtDepartmentDiscAmount.Name = "txtDepartmentDiscAmount";
            this.txtDepartmentDiscAmount.Size = new System.Drawing.Size(100, 21);
            this.txtDepartmentDiscAmount.TabIndex = 27;
            this.txtDepartmentDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDepartmentDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentDiscAmount_KeyDown);
            // 
            // chkAutoCompleationDepartment
            // 
            this.chkAutoCompleationDepartment.AutoSize = true;
            this.chkAutoCompleationDepartment.Checked = true;
            this.chkAutoCompleationDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDepartment.Location = new System.Drawing.Point(3, 247);
            this.chkAutoCompleationDepartment.Name = "chkAutoCompleationDepartment";
            this.chkAutoCompleationDepartment.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDepartment.TabIndex = 26;
            this.chkAutoCompleationDepartment.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDepartment.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDepartment_CheckedChanged);
            // 
            // txtDepartmentQty
            // 
            this.txtDepartmentQty.Location = new System.Drawing.Point(399, 244);
            this.txtDepartmentQty.Name = "txtDepartmentQty";
            this.txtDepartmentQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDepartmentQty.Size = new System.Drawing.Size(75, 21);
            this.txtDepartmentQty.TabIndex = 25;
            this.txtDepartmentQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDepartmentQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentQty_KeyDown);
            // 
            // txtDepartmentDescription
            // 
            this.txtDepartmentDescription.Location = new System.Drawing.Point(148, 244);
            this.txtDepartmentDescription.MasterDescription = "";
            this.txtDepartmentDescription.Name = "txtDepartmentDescription";
            this.txtDepartmentDescription.Size = new System.Drawing.Size(250, 21);
            this.txtDepartmentDescription.TabIndex = 24;
            this.txtDepartmentDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentDescription_KeyDown);
            this.txtDepartmentDescription.Leave += new System.EventHandler(this.txtDepartmentDescription_Leave);
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.Location = new System.Drawing.Point(19, 244);
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.ProductCode = "";
            this.txtDepartmentCode.Size = new System.Drawing.Size(128, 21);
            this.txtDepartmentCode.TabIndex = 23;
            this.txtDepartmentCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDepartmentCode_KeyDown);
            this.txtDepartmentCode.Validated += new System.EventHandler(this.txtDepartmentCode_Validated);
            // 
            // dgvDepartmentDiscount
            // 
            this.dgvDepartmentDiscount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartmentDiscount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn10,
            this.DepartmentPoints,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn11});
            this.dgvDepartmentDiscount.Location = new System.Drawing.Point(3, 6);
            this.dgvDepartmentDiscount.Name = "dgvDepartmentDiscount";
            this.dgvDepartmentDiscount.RowHeadersWidth = 5;
            this.dgvDepartmentDiscount.Size = new System.Drawing.Size(730, 235);
            this.dgvDepartmentDiscount.TabIndex = 22;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "DepartmentCode";
            this.dataGridViewTextBoxColumn7.HeaderText = "Code";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 140;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "DepartmentName";
            this.dataGridViewTextBoxColumn8.HeaderText = "Name";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 250;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn10.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 75;
            // 
            // DepartmentPoints
            // 
            this.DepartmentPoints.DataPropertyName = "Points";
            this.DepartmentPoints.HeaderText = "Points";
            this.DepartmentPoints.Name = "DepartmentPoints";
            this.DepartmentPoints.Width = 85;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "DiscountPercentage";
            this.dataGridViewTextBoxColumn12.HeaderText = "Disc %";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 70;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "DiscountAmount";
            this.dataGridViewTextBoxColumn11.HeaderText = "Disc Amount";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 80;
            // 
            // tbpCategoryDiscount
            // 
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryDiscPercentage);
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryPoints);
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryDiscAmount);
            this.tbpCategoryDiscount.Controls.Add(this.chkAutoCompleationCategory);
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryQty);
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryDescription);
            this.tbpCategoryDiscount.Controls.Add(this.txtCategoryCode);
            this.tbpCategoryDiscount.Controls.Add(this.dgvCategoryDiscount);
            this.tbpCategoryDiscount.Location = new System.Drawing.Point(4, 22);
            this.tbpCategoryDiscount.Name = "tbpCategoryDiscount";
            this.tbpCategoryDiscount.Size = new System.Drawing.Size(816, 269);
            this.tbpCategoryDiscount.TabIndex = 3;
            this.tbpCategoryDiscount.Text = "Category Dis";
            this.tbpCategoryDiscount.UseVisualStyleBackColor = true;
            // 
            // txtCategoryDiscPercentage
            // 
            this.txtCategoryDiscPercentage.Location = new System.Drawing.Point(562, 244);
            this.txtCategoryDiscPercentage.Name = "txtCategoryDiscPercentage";
            this.txtCategoryDiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCategoryDiscPercentage.Size = new System.Drawing.Size(70, 21);
            this.txtCategoryDiscPercentage.TabIndex = 37;
            this.txtCategoryDiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCategoryDiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryDiscPercentage_KeyDown);
            this.txtCategoryDiscPercentage.Validated += new System.EventHandler(this.txtCategoryDiscPercentage_Validated);
            // 
            // txtCategoryPoints
            // 
            this.txtCategoryPoints.Location = new System.Drawing.Point(476, 244);
            this.txtCategoryPoints.Name = "txtCategoryPoints";
            this.txtCategoryPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCategoryPoints.Size = new System.Drawing.Size(85, 21);
            this.txtCategoryPoints.TabIndex = 36;
            this.txtCategoryPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCategoryPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryPoints_KeyDown);
            // 
            // txtCategoryDiscAmount
            // 
            this.txtCategoryDiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCategoryDiscAmount.Location = new System.Drawing.Point(633, 244);
            this.txtCategoryDiscAmount.Name = "txtCategoryDiscAmount";
            this.txtCategoryDiscAmount.Size = new System.Drawing.Size(99, 21);
            this.txtCategoryDiscAmount.TabIndex = 34;
            this.txtCategoryDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCategoryDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryDiscAmount_KeyDown);
            // 
            // chkAutoCompleationCategory
            // 
            this.chkAutoCompleationCategory.AutoSize = true;
            this.chkAutoCompleationCategory.Checked = true;
            this.chkAutoCompleationCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCategory.Location = new System.Drawing.Point(3, 247);
            this.chkAutoCompleationCategory.Name = "chkAutoCompleationCategory";
            this.chkAutoCompleationCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCategory.TabIndex = 33;
            this.chkAutoCompleationCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCategory_CheckedChanged);
            // 
            // txtCategoryQty
            // 
            this.txtCategoryQty.Location = new System.Drawing.Point(399, 244);
            this.txtCategoryQty.Name = "txtCategoryQty";
            this.txtCategoryQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCategoryQty.Size = new System.Drawing.Size(76, 21);
            this.txtCategoryQty.TabIndex = 32;
            this.txtCategoryQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCategoryQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryQty_KeyDown);
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Location = new System.Drawing.Point(148, 244);
            this.txtCategoryDescription.MasterDescription = "";
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(250, 21);
            this.txtCategoryDescription.TabIndex = 31;
            this.txtCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryDescription_KeyDown);
            this.txtCategoryDescription.Leave += new System.EventHandler(this.txtCategoryDescription_Leave);
            // 
            // txtCategoryCode
            // 
            this.txtCategoryCode.Location = new System.Drawing.Point(19, 244);
            this.txtCategoryCode.Name = "txtCategoryCode";
            this.txtCategoryCode.ProductCode = "";
            this.txtCategoryCode.Size = new System.Drawing.Size(128, 21);
            this.txtCategoryCode.TabIndex = 30;
            this.txtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryCode_KeyDown);
            this.txtCategoryCode.Validated += new System.EventHandler(this.txtCategoryCode_Validated);
            // 
            // dgvCategoryDiscount
            // 
            this.dgvCategoryDiscount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategoryDiscount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.CategoryPoints,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn15});
            this.dgvCategoryDiscount.Location = new System.Drawing.Point(3, 6);
            this.dgvCategoryDiscount.Name = "dgvCategoryDiscount";
            this.dgvCategoryDiscount.RowHeadersWidth = 5;
            this.dgvCategoryDiscount.Size = new System.Drawing.Size(730, 235);
            this.dgvCategoryDiscount.TabIndex = 29;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "CategoryCode";
            this.dataGridViewTextBoxColumn9.HeaderText = "Code";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 140;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "CategoryName";
            this.dataGridViewTextBoxColumn13.HeaderText = "Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 250;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn14.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 75;
            // 
            // CategoryPoints
            // 
            this.CategoryPoints.DataPropertyName = "Points";
            this.CategoryPoints.HeaderText = "Points";
            this.CategoryPoints.Name = "CategoryPoints";
            this.CategoryPoints.Width = 85;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "DiscountPercentage";
            this.dataGridViewTextBoxColumn16.HeaderText = "Disc %";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 70;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "DiscountAmount";
            this.dataGridViewTextBoxColumn15.HeaderText = "Disc Amount";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // tbpSubCategoryDiscount
            // 
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryDiscPercentage);
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryPoints);
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryDiscAmount);
            this.tbpSubCategoryDiscount.Controls.Add(this.chkAutoCompleationSubCategory);
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryQty);
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryDescription);
            this.tbpSubCategoryDiscount.Controls.Add(this.txtSubCategoryCode);
            this.tbpSubCategoryDiscount.Controls.Add(this.dgvSubCategory);
            this.tbpSubCategoryDiscount.Location = new System.Drawing.Point(4, 22);
            this.tbpSubCategoryDiscount.Name = "tbpSubCategoryDiscount";
            this.tbpSubCategoryDiscount.Size = new System.Drawing.Size(816, 269);
            this.tbpSubCategoryDiscount.TabIndex = 4;
            this.tbpSubCategoryDiscount.Text = "Sub Category Dis";
            this.tbpSubCategoryDiscount.UseVisualStyleBackColor = true;
            // 
            // txtSubCategoryDiscPercentage
            // 
            this.txtSubCategoryDiscPercentage.Location = new System.Drawing.Point(560, 244);
            this.txtSubCategoryDiscPercentage.Name = "txtSubCategoryDiscPercentage";
            this.txtSubCategoryDiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategoryDiscPercentage.Size = new System.Drawing.Size(69, 21);
            this.txtSubCategoryDiscPercentage.TabIndex = 37;
            this.txtSubCategoryDiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategoryDiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryDiscPercentage_KeyDown);
            this.txtSubCategoryDiscPercentage.Validated += new System.EventHandler(this.txtSubCategoryDiscPercentage_Validated);
            // 
            // txtSubCategoryPoints
            // 
            this.txtSubCategoryPoints.Location = new System.Drawing.Point(476, 244);
            this.txtSubCategoryPoints.Name = "txtSubCategoryPoints";
            this.txtSubCategoryPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategoryPoints.Size = new System.Drawing.Size(83, 21);
            this.txtSubCategoryPoints.TabIndex = 36;
            this.txtSubCategoryPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategoryPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryPoints_KeyDown);
            // 
            // txtSubCategoryDiscAmount
            // 
            this.txtSubCategoryDiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategoryDiscAmount.Location = new System.Drawing.Point(630, 244);
            this.txtSubCategoryDiscAmount.Name = "txtSubCategoryDiscAmount";
            this.txtSubCategoryDiscAmount.Size = new System.Drawing.Size(102, 21);
            this.txtSubCategoryDiscAmount.TabIndex = 34;
            this.txtSubCategoryDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategoryDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryDiscAmount_KeyDown);
            // 
            // chkAutoCompleationSubCategory
            // 
            this.chkAutoCompleationSubCategory.AutoSize = true;
            this.chkAutoCompleationSubCategory.Checked = true;
            this.chkAutoCompleationSubCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationSubCategory.Location = new System.Drawing.Point(3, 247);
            this.chkAutoCompleationSubCategory.Name = "chkAutoCompleationSubCategory";
            this.chkAutoCompleationSubCategory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationSubCategory.TabIndex = 33;
            this.chkAutoCompleationSubCategory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationSubCategory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationSubCategory_CheckedChanged);
            // 
            // txtSubCategoryQty
            // 
            this.txtSubCategoryQty.Location = new System.Drawing.Point(399, 244);
            this.txtSubCategoryQty.Name = "txtSubCategoryQty";
            this.txtSubCategoryQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategoryQty.Size = new System.Drawing.Size(75, 21);
            this.txtSubCategoryQty.TabIndex = 32;
            this.txtSubCategoryQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategoryQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryQty_KeyDown);
            // 
            // txtSubCategoryDescription
            // 
            this.txtSubCategoryDescription.Location = new System.Drawing.Point(148, 244);
            this.txtSubCategoryDescription.MasterDescription = "";
            this.txtSubCategoryDescription.Name = "txtSubCategoryDescription";
            this.txtSubCategoryDescription.Size = new System.Drawing.Size(250, 21);
            this.txtSubCategoryDescription.TabIndex = 31;
            this.txtSubCategoryDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryDescription_KeyDown);
            this.txtSubCategoryDescription.Leave += new System.EventHandler(this.txtSubCategoryDescription_Leave);
            // 
            // txtSubCategoryCode
            // 
            this.txtSubCategoryCode.Location = new System.Drawing.Point(19, 244);
            this.txtSubCategoryCode.Name = "txtSubCategoryCode";
            this.txtSubCategoryCode.ProductCode = "";
            this.txtSubCategoryCode.Size = new System.Drawing.Size(128, 21);
            this.txtSubCategoryCode.TabIndex = 30;
            this.txtSubCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategoryCode_KeyDown);
            this.txtSubCategoryCode.Validated += new System.EventHandler(this.txtSubCategoryCode_Validated);
            // 
            // dgvSubCategory
            // 
            this.dgvSubCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.SubCategoryPoints,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn20});
            this.dgvSubCategory.Location = new System.Drawing.Point(3, 6);
            this.dgvSubCategory.Name = "dgvSubCategory";
            this.dgvSubCategory.RowHeadersWidth = 5;
            this.dgvSubCategory.Size = new System.Drawing.Size(729, 235);
            this.dgvSubCategory.TabIndex = 29;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "SubCategoryCode";
            this.dataGridViewTextBoxColumn17.HeaderText = "Code";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 140;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "SubCategoryName";
            this.dataGridViewTextBoxColumn18.HeaderText = "Name";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Width = 250;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn19.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.Width = 75;
            // 
            // SubCategoryPoints
            // 
            this.SubCategoryPoints.DataPropertyName = "Points";
            this.SubCategoryPoints.HeaderText = "Points";
            this.SubCategoryPoints.Name = "SubCategoryPoints";
            this.SubCategoryPoints.Width = 85;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "DiscountPercentage";
            this.dataGridViewTextBoxColumn21.HeaderText = "Disc %";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.Width = 70;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "DiscountAmount";
            this.dataGridViewTextBoxColumn20.HeaderText = "Disc Amount";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.Width = 80;
            // 
            // tbpSubCategory2Discount
            // 
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2DiscPercentage);
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2Points);
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2DiscAmount);
            this.tbpSubCategory2Discount.Controls.Add(this.ChkSubCategory2Dis);
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2Qty);
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2Description);
            this.tbpSubCategory2Discount.Controls.Add(this.txtSubCategory2Code);
            this.tbpSubCategory2Discount.Controls.Add(this.dgvSubCategory2);
            this.tbpSubCategory2Discount.Location = new System.Drawing.Point(4, 22);
            this.tbpSubCategory2Discount.Name = "tbpSubCategory2Discount";
            this.tbpSubCategory2Discount.Size = new System.Drawing.Size(816, 269);
            this.tbpSubCategory2Discount.TabIndex = 7;
            this.tbpSubCategory2Discount.Text = "Sub Category 2 Dis";
            this.tbpSubCategory2Discount.UseVisualStyleBackColor = true;
            // 
            // txtSubCategory2DiscPercentage
            // 
            this.txtSubCategory2DiscPercentage.Location = new System.Drawing.Point(560, 241);
            this.txtSubCategory2DiscPercentage.Name = "txtSubCategory2DiscPercentage";
            this.txtSubCategory2DiscPercentage.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategory2DiscPercentage.Size = new System.Drawing.Size(68, 21);
            this.txtSubCategory2DiscPercentage.TabIndex = 45;
            this.txtSubCategory2DiscPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategory2DiscPercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2DiscPercentage_KeyDown);
            this.txtSubCategory2DiscPercentage.Validated += new System.EventHandler(this.txtSubCategory2DiscPercentage_Validated);
            // 
            // txtSubCategory2Points
            // 
            this.txtSubCategory2Points.Location = new System.Drawing.Point(476, 241);
            this.txtSubCategory2Points.Name = "txtSubCategory2Points";
            this.txtSubCategory2Points.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategory2Points.Size = new System.Drawing.Size(83, 21);
            this.txtSubCategory2Points.TabIndex = 44;
            this.txtSubCategory2Points.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategory2Points.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Points_KeyDown);
            // 
            // txtSubCategory2DiscAmount
            // 
            this.txtSubCategory2DiscAmount.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategory2DiscAmount.Location = new System.Drawing.Point(630, 241);
            this.txtSubCategory2DiscAmount.Name = "txtSubCategory2DiscAmount";
            this.txtSubCategory2DiscAmount.Size = new System.Drawing.Size(102, 21);
            this.txtSubCategory2DiscAmount.TabIndex = 42;
            this.txtSubCategory2DiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategory2DiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2DiscAmount_KeyDown);
            // 
            // ChkSubCategory2Dis
            // 
            this.ChkSubCategory2Dis.AutoSize = true;
            this.ChkSubCategory2Dis.Checked = true;
            this.ChkSubCategory2Dis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSubCategory2Dis.Location = new System.Drawing.Point(3, 244);
            this.ChkSubCategory2Dis.Name = "ChkSubCategory2Dis";
            this.ChkSubCategory2Dis.Size = new System.Drawing.Size(15, 14);
            this.ChkSubCategory2Dis.TabIndex = 41;
            this.ChkSubCategory2Dis.UseVisualStyleBackColor = true;
            this.ChkSubCategory2Dis.CheckedChanged += new System.EventHandler(this.ChkSubCategory2Dis_CheckedChanged);
            // 
            // txtSubCategory2Qty
            // 
            this.txtSubCategory2Qty.Location = new System.Drawing.Point(399, 241);
            this.txtSubCategory2Qty.Name = "txtSubCategory2Qty";
            this.txtSubCategory2Qty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubCategory2Qty.Size = new System.Drawing.Size(75, 21);
            this.txtSubCategory2Qty.TabIndex = 40;
            this.txtSubCategory2Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubCategory2Qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Qty_KeyDown);
            // 
            // txtSubCategory2Description
            // 
            this.txtSubCategory2Description.Location = new System.Drawing.Point(148, 241);
            this.txtSubCategory2Description.MasterDescription = "";
            this.txtSubCategory2Description.Name = "txtSubCategory2Description";
            this.txtSubCategory2Description.Size = new System.Drawing.Size(250, 21);
            this.txtSubCategory2Description.TabIndex = 39;
            this.txtSubCategory2Description.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Description_KeyDown);
            this.txtSubCategory2Description.Leave += new System.EventHandler(this.txtSubCategory2Description_Leave);
            // 
            // txtSubCategory2Code
            // 
            this.txtSubCategory2Code.Location = new System.Drawing.Point(19, 241);
            this.txtSubCategory2Code.Name = "txtSubCategory2Code";
            this.txtSubCategory2Code.ProductCode = "";
            this.txtSubCategory2Code.Size = new System.Drawing.Size(128, 21);
            this.txtSubCategory2Code.TabIndex = 38;
            this.txtSubCategory2Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCategory2Code_KeyDown);
            this.txtSubCategory2Code.Validated += new System.EventHandler(this.txtSubCategory2Code_Validated);
            // 
            // dgvSubCategory2
            // 
            this.dgvSubCategory2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubCategory2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43});
            this.dgvSubCategory2.Location = new System.Drawing.Point(3, 3);
            this.dgvSubCategory2.Name = "dgvSubCategory2";
            this.dgvSubCategory2.RowHeadersWidth = 5;
            this.dgvSubCategory2.Size = new System.Drawing.Size(729, 235);
            this.dgvSubCategory2.TabIndex = 37;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.DataPropertyName = "SubCategory2Code";
            this.dataGridViewTextBoxColumn38.HeaderText = "Code";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.Width = 140;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.DataPropertyName = "SubCategory2Name";
            this.dataGridViewTextBoxColumn39.HeaderText = "Name";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.Width = 250;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn40.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.Width = 75;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.DataPropertyName = "Points";
            this.dataGridViewTextBoxColumn41.HeaderText = "Points";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.Width = 85;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.DataPropertyName = "DiscountPercentage";
            this.dataGridViewTextBoxColumn42.HeaderText = "Disc %";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.Width = 70;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.DataPropertyName = "DiscountAmount";
            this.dataGridViewTextBoxColumn43.HeaderText = "Disc Amount";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.Width = 80;
            // 
            // tbpSubTotal
            // 
            this.tbpSubTotal.Controls.Add(this.ChkCombined);
            this.tbpSubTotal.Controls.Add(this.ChkGiftVoucher);
            this.tbpSubTotal.Controls.Add(this.txtPoints);
            this.tbpSubTotal.Controls.Add(this.lblPoints);
            this.tbpSubTotal.Controls.Add(this.txtSubTotalDiscountValue);
            this.tbpSubTotal.Controls.Add(this.lblDiscountValue);
            this.tbpSubTotal.Controls.Add(this.label6);
            this.tbpSubTotal.Controls.Add(this.lblDiscountPercentage);
            this.tbpSubTotal.Controls.Add(this.txtSubTotalDiscountPercentage);
            this.tbpSubTotal.Location = new System.Drawing.Point(4, 22);
            this.tbpSubTotal.Name = "tbpSubTotal";
            this.tbpSubTotal.Size = new System.Drawing.Size(816, 269);
            this.tbpSubTotal.TabIndex = 5;
            this.tbpSubTotal.Text = "Sub Total";
            this.tbpSubTotal.UseVisualStyleBackColor = true;
            // 
            // ChkCombined
            // 
            this.ChkCombined.AutoSize = true;
            this.ChkCombined.Location = new System.Drawing.Point(618, 217);
            this.ChkCombined.Name = "ChkCombined";
            this.ChkCombined.Size = new System.Drawing.Size(146, 17);
            this.ChkCombined.TabIndex = 75;
            this.ChkCombined.Text = "Combined Promotion";
            this.ChkCombined.UseVisualStyleBackColor = true;
            // 
            // ChkGiftVoucher
            // 
            this.ChkGiftVoucher.AutoSize = true;
            this.ChkGiftVoucher.Location = new System.Drawing.Point(618, 244);
            this.ChkGiftVoucher.Name = "ChkGiftVoucher";
            this.ChkGiftVoucher.Size = new System.Drawing.Size(182, 17);
            this.ChkGiftVoucher.TabIndex = 74;
            this.ChkGiftVoucher.Text = "Valid for Gift Voucher Sales";
            this.ChkGiftVoucher.UseVisualStyleBackColor = true;
            // 
            // txtPoints
            // 
            this.txtPoints.Location = new System.Drawing.Point(120, 60);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPoints.Size = new System.Drawing.Size(125, 21);
            this.txtPoints.TabIndex = 70;
            this.txtPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(10, 63);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(41, 13);
            this.lblPoints.TabIndex = 69;
            this.lblPoints.Text = "Points";
            // 
            // txtSubTotalDiscountValue
            // 
            this.txtSubTotalDiscountValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountValue.Location = new System.Drawing.Point(120, 35);
            this.txtSubTotalDiscountValue.Name = "txtSubTotalDiscountValue";
            this.txtSubTotalDiscountValue.Size = new System.Drawing.Size(125, 21);
            this.txtSubTotalDiscountValue.TabIndex = 68;
            this.txtSubTotalDiscountValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Location = new System.Drawing.Point(10, 38);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(91, 13);
            this.lblDiscountValue.TabIndex = 67;
            this.lblDiscountValue.Text = "Discount Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "%";
            // 
            // lblDiscountPercentage
            // 
            this.lblDiscountPercentage.AutoSize = true;
            this.lblDiscountPercentage.Location = new System.Drawing.Point(10, 13);
            this.lblDiscountPercentage.Name = "lblDiscountPercentage";
            this.lblDiscountPercentage.Size = new System.Drawing.Size(89, 13);
            this.lblDiscountPercentage.TabIndex = 65;
            this.lblDiscountPercentage.Text = "Discount Perc.";
            // 
            // txtSubTotalDiscountPercentage
            // 
            this.txtSubTotalDiscountPercentage.ForeColor = System.Drawing.Color.Black;
            this.txtSubTotalDiscountPercentage.Location = new System.Drawing.Point(121, 10);
            this.txtSubTotalDiscountPercentage.Name = "txtSubTotalDiscountPercentage";
            this.txtSubTotalDiscountPercentage.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubTotalDiscountPercentage.Size = new System.Drawing.Size(64, 21);
            this.txtSubTotalDiscountPercentage.TabIndex = 64;
            this.txtSubTotalDiscountPercentage.Text = "0.00";
            this.txtSubTotalDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbpGetGift
            // 
            this.tbpGetGift.Controls.Add(this.txtGiftGetValue);
            this.tbpGetGift.Controls.Add(this.txtGiftGetQty);
            this.tbpGetGift.Controls.Add(this.txtGiftDescription);
            this.tbpGetGift.Controls.Add(this.txtGiftValueRange);
            this.tbpGetGift.Controls.Add(this.ChkAutoCompleationSubCategory2);
            this.tbpGetGift.Controls.Add(this.txtGiftBuyQty);
            this.tbpGetGift.Controls.Add(this.txtGiftSubCategory2Name);
            this.tbpGetGift.Controls.Add(this.txtGiftSubCategory2Code);
            this.tbpGetGift.Controls.Add(this.dataGridView1);
            this.tbpGetGift.Location = new System.Drawing.Point(4, 22);
            this.tbpGetGift.Name = "tbpGetGift";
            this.tbpGetGift.Size = new System.Drawing.Size(816, 269);
            this.tbpGetGift.TabIndex = 6;
            this.tbpGetGift.Text = "Get Gift";
            this.tbpGetGift.UseVisualStyleBackColor = true;
            // 
            // txtGiftGetValue
            // 
            this.txtGiftGetValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftGetValue.Location = new System.Drawing.Point(621, 244);
            this.txtGiftGetValue.Name = "txtGiftGetValue";
            this.txtGiftGetValue.Size = new System.Drawing.Size(74, 21);
            this.txtGiftGetValue.TabIndex = 69;
            this.txtGiftGetValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGiftGetQty
            // 
            this.txtGiftGetQty.Location = new System.Drawing.Point(576, 244);
            this.txtGiftGetQty.Name = "txtGiftGetQty";
            this.txtGiftGetQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftGetQty.Size = new System.Drawing.Size(44, 21);
            this.txtGiftGetQty.TabIndex = 68;
            this.txtGiftGetQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGiftDescription
            // 
            this.txtGiftDescription.Location = new System.Drawing.Point(451, 244);
            this.txtGiftDescription.MasterDescription = "";
            this.txtGiftDescription.Name = "txtGiftDescription";
            this.txtGiftDescription.Size = new System.Drawing.Size(124, 21);
            this.txtGiftDescription.TabIndex = 67;
            // 
            // txtGiftValueRange
            // 
            this.txtGiftValueRange.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftValueRange.Location = new System.Drawing.Point(318, 244);
            this.txtGiftValueRange.Name = "txtGiftValueRange";
            this.txtGiftValueRange.Size = new System.Drawing.Size(132, 21);
            this.txtGiftValueRange.TabIndex = 66;
            this.txtGiftValueRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ChkAutoCompleationSubCategory2
            // 
            this.ChkAutoCompleationSubCategory2.AutoSize = true;
            this.ChkAutoCompleationSubCategory2.Checked = true;
            this.ChkAutoCompleationSubCategory2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAutoCompleationSubCategory2.Location = new System.Drawing.Point(2, 247);
            this.ChkAutoCompleationSubCategory2.Name = "ChkAutoCompleationSubCategory2";
            this.ChkAutoCompleationSubCategory2.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoCompleationSubCategory2.TabIndex = 37;
            this.ChkAutoCompleationSubCategory2.UseVisualStyleBackColor = true;
            // 
            // txtGiftBuyQty
            // 
            this.txtGiftBuyQty.Location = new System.Drawing.Point(273, 244);
            this.txtGiftBuyQty.Name = "txtGiftBuyQty";
            this.txtGiftBuyQty.QtyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtGiftBuyQty.Size = new System.Drawing.Size(44, 21);
            this.txtGiftBuyQty.TabIndex = 36;
            this.txtGiftBuyQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGiftSubCategory2Name
            // 
            this.txtGiftSubCategory2Name.Location = new System.Drawing.Point(118, 244);
            this.txtGiftSubCategory2Name.MasterDescription = "";
            this.txtGiftSubCategory2Name.Name = "txtGiftSubCategory2Name";
            this.txtGiftSubCategory2Name.Size = new System.Drawing.Size(154, 21);
            this.txtGiftSubCategory2Name.TabIndex = 35;
            // 
            // txtGiftSubCategory2Code
            // 
            this.txtGiftSubCategory2Code.Location = new System.Drawing.Point(18, 244);
            this.txtGiftSubCategory2Code.Name = "txtGiftSubCategory2Code";
            this.txtGiftSubCategory2Code.ProductCode = "";
            this.txtGiftSubCategory2Code.Size = new System.Drawing.Size(99, 21);
            this.txtGiftSubCategory2Code.TabIndex = 34;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.ValueRange,
            this.Description,
            this.GiftQty,
            this.GiftValue,
            this.Increase,
            this.Raffle});
            this.dataGridView1.Location = new System.Drawing.Point(1, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.Size = new System.Drawing.Size(813, 238);
            this.dataGridView1.TabIndex = 33;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.DataPropertyName = "SubCategory2Code";
            this.dataGridViewTextBoxColumn31.HeaderText = "Code";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.Width = 110;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.DataPropertyName = "SubCategory2Name";
            this.dataGridViewTextBoxColumn35.HeaderText = "Name";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.Width = 150;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.DataPropertyName = "BuyQty";
            this.dataGridViewTextBoxColumn36.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.Width = 50;
            // 
            // ValueRange
            // 
            this.ValueRange.HeaderText = "Value Range";
            this.ValueRange.Name = "ValueRange";
            this.ValueRange.Width = 130;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.Width = 130;
            // 
            // GiftQty
            // 
            this.GiftQty.HeaderText = "Gift Qty";
            this.GiftQty.Name = "GiftQty";
            this.GiftQty.Width = 45;
            // 
            // GiftValue
            // 
            this.GiftValue.HeaderText = "Gift Value";
            this.GiftValue.Name = "GiftValue";
            this.GiftValue.Width = 70;
            // 
            // Increase
            // 
            this.Increase.HeaderText = "Increase";
            this.Increase.Name = "Increase";
            this.Increase.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Increase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Increase.Width = 50;
            // 
            // Raffle
            // 
            this.Raffle.HeaderText = "Raffle";
            this.Raffle.Name = "Raffle";
            this.Raffle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Raffle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Raffle.Width = 50;
            // 
            // grpDays
            // 
            this.grpDays.Controls.Add(this.lblToDate);
            this.grpDays.Controls.Add(this.chkSundayTime);
            this.grpDays.Controls.Add(this.chkSaturdayTime);
            this.grpDays.Controls.Add(this.chkFridayTime);
            this.grpDays.Controls.Add(this.lblPeriod);
            this.grpDays.Controls.Add(this.chkThuresdayTime);
            this.grpDays.Controls.Add(this.chkWednesdayTime);
            this.grpDays.Controls.Add(this.dtpEndDate);
            this.grpDays.Controls.Add(this.chkTuesdayTime);
            this.grpDays.Controls.Add(this.chkMondayTime);
            this.grpDays.Controls.Add(this.dtpSundayTo);
            this.grpDays.Controls.Add(this.dtpStartDate);
            this.grpDays.Controls.Add(this.dtpSundayFrom);
            this.grpDays.Controls.Add(this.dtpSaturdayTo);
            this.grpDays.Controls.Add(this.dtpSaturdayFrom);
            this.grpDays.Controls.Add(this.dtpFridayTo);
            this.grpDays.Controls.Add(this.dtpFridayFrom);
            this.grpDays.Controls.Add(this.dtpThuresdayTo);
            this.grpDays.Controls.Add(this.dtpThuresdayFrom);
            this.grpDays.Controls.Add(this.dtpWednesdayTo);
            this.grpDays.Controls.Add(this.dtpWednesdayFrom);
            this.grpDays.Controls.Add(this.dtpTuesdayTo);
            this.grpDays.Controls.Add(this.dtpTuesdayFrom);
            this.grpDays.Controls.Add(this.dtpMondayTo);
            this.grpDays.Controls.Add(this.chkSunday);
            this.grpDays.Controls.Add(this.chkSaturday);
            this.grpDays.Controls.Add(this.chkFriday);
            this.grpDays.Controls.Add(this.chkThuresday);
            this.grpDays.Controls.Add(this.chkWednesday);
            this.grpDays.Controls.Add(this.chkTuesday);
            this.grpDays.Controls.Add(this.chkMonday);
            this.grpDays.Controls.Add(this.dtpMondayFrom);
            this.grpDays.Location = new System.Drawing.Point(766, -5);
            this.grpDays.Name = "grpDays";
            this.grpDays.Size = new System.Drawing.Size(325, 219);
            this.grpDays.TabIndex = 41;
            this.grpDays.TabStop = false;
            // 
            // chkSundayTime
            // 
            this.chkSundayTime.AutoSize = true;
            this.chkSundayTime.Enabled = false;
            this.chkSundayTime.Location = new System.Drawing.Point(103, 197);
            this.chkSundayTime.Name = "chkSundayTime";
            this.chkSundayTime.Size = new System.Drawing.Size(15, 14);
            this.chkSundayTime.TabIndex = 90;
            this.chkSundayTime.UseVisualStyleBackColor = true;
            this.chkSundayTime.CheckedChanged += new System.EventHandler(this.chkSundayTime_CheckedChanged);
            // 
            // chkSaturdayTime
            // 
            this.chkSaturdayTime.AutoSize = true;
            this.chkSaturdayTime.Enabled = false;
            this.chkSaturdayTime.Location = new System.Drawing.Point(102, 174);
            this.chkSaturdayTime.Name = "chkSaturdayTime";
            this.chkSaturdayTime.Size = new System.Drawing.Size(15, 14);
            this.chkSaturdayTime.TabIndex = 86;
            this.chkSaturdayTime.UseVisualStyleBackColor = true;
            this.chkSaturdayTime.CheckedChanged += new System.EventHandler(this.chkSaturdayTime_CheckedChanged);
            // 
            // chkFridayTime
            // 
            this.chkFridayTime.AutoSize = true;
            this.chkFridayTime.Enabled = false;
            this.chkFridayTime.Location = new System.Drawing.Point(102, 151);
            this.chkFridayTime.Name = "chkFridayTime";
            this.chkFridayTime.Size = new System.Drawing.Size(15, 14);
            this.chkFridayTime.TabIndex = 89;
            this.chkFridayTime.UseVisualStyleBackColor = true;
            this.chkFridayTime.CheckedChanged += new System.EventHandler(this.chkFridayTime_CheckedChanged);
            // 
            // chkThuresdayTime
            // 
            this.chkThuresdayTime.AutoSize = true;
            this.chkThuresdayTime.Enabled = false;
            this.chkThuresdayTime.Location = new System.Drawing.Point(102, 128);
            this.chkThuresdayTime.Name = "chkThuresdayTime";
            this.chkThuresdayTime.Size = new System.Drawing.Size(15, 14);
            this.chkThuresdayTime.TabIndex = 88;
            this.chkThuresdayTime.UseVisualStyleBackColor = true;
            this.chkThuresdayTime.CheckedChanged += new System.EventHandler(this.chkThuresdayTime_CheckedChanged);
            // 
            // chkWednesdayTime
            // 
            this.chkWednesdayTime.AutoSize = true;
            this.chkWednesdayTime.Enabled = false;
            this.chkWednesdayTime.Location = new System.Drawing.Point(102, 105);
            this.chkWednesdayTime.Name = "chkWednesdayTime";
            this.chkWednesdayTime.Size = new System.Drawing.Size(15, 14);
            this.chkWednesdayTime.TabIndex = 87;
            this.chkWednesdayTime.UseVisualStyleBackColor = true;
            this.chkWednesdayTime.CheckedChanged += new System.EventHandler(this.chkWednesdayTime_CheckedChanged);
            // 
            // chkTuesdayTime
            // 
            this.chkTuesdayTime.AutoSize = true;
            this.chkTuesdayTime.Enabled = false;
            this.chkTuesdayTime.Location = new System.Drawing.Point(102, 82);
            this.chkTuesdayTime.Name = "chkTuesdayTime";
            this.chkTuesdayTime.Size = new System.Drawing.Size(15, 14);
            this.chkTuesdayTime.TabIndex = 86;
            this.chkTuesdayTime.UseVisualStyleBackColor = true;
            this.chkTuesdayTime.CheckedChanged += new System.EventHandler(this.chkTuesdayTime_CheckedChanged);
            // 
            // chkMondayTime
            // 
            this.chkMondayTime.AutoSize = true;
            this.chkMondayTime.Enabled = false;
            this.chkMondayTime.Location = new System.Drawing.Point(102, 58);
            this.chkMondayTime.Name = "chkMondayTime";
            this.chkMondayTime.Size = new System.Drawing.Size(15, 14);
            this.chkMondayTime.TabIndex = 85;
            this.chkMondayTime.UseVisualStyleBackColor = true;
            this.chkMondayTime.CheckedChanged += new System.EventHandler(this.chkMondayTime_CheckedChanged);
            // 
            // dtpSundayTo
            // 
            this.dtpSundayTo.Enabled = false;
            this.dtpSundayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSundayTo.Location = new System.Drawing.Point(224, 194);
            this.dtpSundayTo.Name = "dtpSundayTo";
            this.dtpSundayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpSundayTo.TabIndex = 81;
            // 
            // dtpSundayFrom
            // 
            this.dtpSundayFrom.Enabled = false;
            this.dtpSundayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSundayFrom.Location = new System.Drawing.Point(121, 194);
            this.dtpSundayFrom.Name = "dtpSundayFrom";
            this.dtpSundayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpSundayFrom.TabIndex = 80;
            // 
            // dtpSaturdayTo
            // 
            this.dtpSaturdayTo.Enabled = false;
            this.dtpSaturdayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSaturdayTo.Location = new System.Drawing.Point(224, 171);
            this.dtpSaturdayTo.Name = "dtpSaturdayTo";
            this.dtpSaturdayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpSaturdayTo.TabIndex = 79;
            // 
            // dtpSaturdayFrom
            // 
            this.dtpSaturdayFrom.Enabled = false;
            this.dtpSaturdayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSaturdayFrom.Location = new System.Drawing.Point(120, 171);
            this.dtpSaturdayFrom.Name = "dtpSaturdayFrom";
            this.dtpSaturdayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpSaturdayFrom.TabIndex = 78;
            // 
            // dtpFridayTo
            // 
            this.dtpFridayTo.Enabled = false;
            this.dtpFridayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFridayTo.Location = new System.Drawing.Point(224, 148);
            this.dtpFridayTo.Name = "dtpFridayTo";
            this.dtpFridayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpFridayTo.TabIndex = 77;
            // 
            // dtpFridayFrom
            // 
            this.dtpFridayFrom.Enabled = false;
            this.dtpFridayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFridayFrom.Location = new System.Drawing.Point(120, 148);
            this.dtpFridayFrom.Name = "dtpFridayFrom";
            this.dtpFridayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpFridayFrom.TabIndex = 76;
            // 
            // dtpThuresdayTo
            // 
            this.dtpThuresdayTo.Enabled = false;
            this.dtpThuresdayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpThuresdayTo.Location = new System.Drawing.Point(224, 125);
            this.dtpThuresdayTo.Name = "dtpThuresdayTo";
            this.dtpThuresdayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpThuresdayTo.TabIndex = 75;
            // 
            // dtpThuresdayFrom
            // 
            this.dtpThuresdayFrom.Enabled = false;
            this.dtpThuresdayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpThuresdayFrom.Location = new System.Drawing.Point(120, 125);
            this.dtpThuresdayFrom.Name = "dtpThuresdayFrom";
            this.dtpThuresdayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpThuresdayFrom.TabIndex = 74;
            // 
            // dtpWednesdayTo
            // 
            this.dtpWednesdayTo.Enabled = false;
            this.dtpWednesdayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpWednesdayTo.Location = new System.Drawing.Point(224, 102);
            this.dtpWednesdayTo.Name = "dtpWednesdayTo";
            this.dtpWednesdayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpWednesdayTo.TabIndex = 73;
            // 
            // dtpWednesdayFrom
            // 
            this.dtpWednesdayFrom.Enabled = false;
            this.dtpWednesdayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpWednesdayFrom.Location = new System.Drawing.Point(120, 102);
            this.dtpWednesdayFrom.Name = "dtpWednesdayFrom";
            this.dtpWednesdayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpWednesdayFrom.TabIndex = 72;
            // 
            // dtpTuesdayTo
            // 
            this.dtpTuesdayTo.Enabled = false;
            this.dtpTuesdayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTuesdayTo.Location = new System.Drawing.Point(224, 79);
            this.dtpTuesdayTo.Name = "dtpTuesdayTo";
            this.dtpTuesdayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpTuesdayTo.TabIndex = 71;
            // 
            // dtpTuesdayFrom
            // 
            this.dtpTuesdayFrom.Enabled = false;
            this.dtpTuesdayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTuesdayFrom.Location = new System.Drawing.Point(120, 79);
            this.dtpTuesdayFrom.Name = "dtpTuesdayFrom";
            this.dtpTuesdayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpTuesdayFrom.TabIndex = 70;
            // 
            // dtpMondayTo
            // 
            this.dtpMondayTo.Enabled = false;
            this.dtpMondayTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpMondayTo.Location = new System.Drawing.Point(224, 56);
            this.dtpMondayTo.Name = "dtpMondayTo";
            this.dtpMondayTo.Size = new System.Drawing.Size(98, 21);
            this.dtpMondayTo.TabIndex = 69;
            // 
            // chkSunday
            // 
            this.chkSunday.AutoSize = true;
            this.chkSunday.Location = new System.Drawing.Point(10, 196);
            this.chkSunday.Name = "chkSunday";
            this.chkSunday.Size = new System.Drawing.Size(69, 17);
            this.chkSunday.TabIndex = 68;
            this.chkSunday.Text = "Sunday";
            this.chkSunday.UseVisualStyleBackColor = true;
            this.chkSunday.CheckedChanged += new System.EventHandler(this.chkSunday_CheckedChanged);
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Location = new System.Drawing.Point(10, 173);
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size(78, 17);
            this.chkSaturday.TabIndex = 67;
            this.chkSaturday.Text = "Saturday";
            this.chkSaturday.UseVisualStyleBackColor = true;
            this.chkSaturday.CheckedChanged += new System.EventHandler(this.chkSaturday_CheckedChanged);
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Location = new System.Drawing.Point(10, 150);
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size(61, 17);
            this.chkFriday.TabIndex = 66;
            this.chkFriday.Text = "Friday";
            this.chkFriday.UseVisualStyleBackColor = true;
            this.chkFriday.CheckedChanged += new System.EventHandler(this.chkFriday_CheckedChanged);
            // 
            // chkThuresday
            // 
            this.chkThuresday.AutoSize = true;
            this.chkThuresday.Location = new System.Drawing.Point(10, 127);
            this.chkThuresday.Name = "chkThuresday";
            this.chkThuresday.Size = new System.Drawing.Size(86, 17);
            this.chkThuresday.TabIndex = 65;
            this.chkThuresday.Text = "Thuresday";
            this.chkThuresday.UseVisualStyleBackColor = true;
            this.chkThuresday.CheckedChanged += new System.EventHandler(this.chkThuresday_CheckedChanged);
            // 
            // chkWednesday
            // 
            this.chkWednesday.AutoSize = true;
            this.chkWednesday.Location = new System.Drawing.Point(10, 104);
            this.chkWednesday.Name = "chkWednesday";
            this.chkWednesday.Size = new System.Drawing.Size(91, 17);
            this.chkWednesday.TabIndex = 64;
            this.chkWednesday.Text = "Wednesday";
            this.chkWednesday.UseVisualStyleBackColor = true;
            this.chkWednesday.CheckedChanged += new System.EventHandler(this.chkWednesday_CheckedChanged);
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Location = new System.Drawing.Point(10, 81);
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size(73, 17);
            this.chkTuesday.TabIndex = 63;
            this.chkTuesday.Text = "Tuesday";
            this.chkTuesday.UseVisualStyleBackColor = true;
            this.chkTuesday.CheckedChanged += new System.EventHandler(this.chkTuesday_CheckedChanged);
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Location = new System.Drawing.Point(10, 58);
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size(70, 17);
            this.chkMonday.TabIndex = 62;
            this.chkMonday.Text = "Monday";
            this.chkMonday.UseVisualStyleBackColor = true;
            this.chkMonday.CheckedChanged += new System.EventHandler(this.chkMonday_CheckedChanged);
            // 
            // dtpMondayFrom
            // 
            this.dtpMondayFrom.Enabled = false;
            this.dtpMondayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpMondayFrom.Location = new System.Drawing.Point(120, 56);
            this.dtpMondayFrom.Name = "dtpMondayFrom";
            this.dtpMondayFrom.Size = new System.Drawing.Size(98, 21);
            this.dtpMondayFrom.TabIndex = 61;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(64, 147);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(313, 22);
            this.txtRemark.TabIndex = 82;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 150);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 84;
            this.lblRemark.Text = "Remark";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecipient);
            this.groupBox3.Controls.Add(this.cmbRecipient);
            this.groupBox3.Controls.Add(this.lblCount);
            this.groupBox3.Controls.Add(this.btnSendEmail);
            this.groupBox3.Controls.Add(this.btnSendSms);
            this.groupBox3.Controls.Add(this.txtBody);
            this.groupBox3.Location = new System.Drawing.Point(835, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 113);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            // 
            // lblRecipient
            // 
            this.lblRecipient.AutoSize = true;
            this.lblRecipient.Location = new System.Drawing.Point(6, 13);
            this.lblRecipient.Name = "lblRecipient";
            this.lblRecipient.Size = new System.Drawing.Size(59, 13);
            this.lblRecipient.TabIndex = 5;
            this.lblRecipient.Text = "Recipient";
            // 
            // cmbRecipient
            // 
            this.cmbRecipient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipient.FormattingEnabled = true;
            this.cmbRecipient.Location = new System.Drawing.Point(69, 10);
            this.cmbRecipient.Name = "cmbRecipient";
            this.cmbRecipient.Size = new System.Drawing.Size(183, 21);
            this.cmbRecipient.TabIndex = 4;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCount.Location = new System.Drawing.Point(6, 92);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(41, 13);
            this.lblCount.TabIndex = 3;
            this.lblCount.Text = "Count";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(79, 87);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(94, 23);
            this.btnSendEmail.TabIndex = 2;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            // 
            // btnSendSms
            // 
            this.btnSendSms.Location = new System.Drawing.Point(177, 87);
            this.btnSendSms.Name = "btnSendSms";
            this.btnSendSms.Size = new System.Drawing.Size(75, 23);
            this.btnSendSms.TabIndex = 1;
            this.btnSendSms.Text = "Send SMS";
            this.btnSendSms.UseVisualStyleBackColor = true;
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(3, 34);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(249, 51);
            this.txtBody.TabIndex = 0;
            this.txtBody.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBody_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAllLocations);
            this.groupBox2.Controls.Add(this.dgvLocationInfo);
            this.groupBox2.Controls.Add(this.lblRemark);
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Location = new System.Drawing.Point(381, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 173);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            // 
            // chkAllLocations
            // 
            this.chkAllLocations.AutoSize = true;
            this.chkAllLocations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllLocations.Location = new System.Drawing.Point(6, 11);
            this.chkAllLocations.Name = "chkAllLocations";
            this.chkAllLocations.Size = new System.Drawing.Size(97, 17);
            this.chkAllLocations.TabIndex = 2;
            this.chkAllLocations.Text = "All Locations";
            this.chkAllLocations.UseVisualStyleBackColor = true;
            this.chkAllLocations.CheckedChanged += new System.EventHandler(this.chkAllLocations_CheckedChanged);
            // 
            // dgvLocationInfo
            // 
            this.dgvLocationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.Location,
            this.LocationId});
            this.dgvLocationInfo.Location = new System.Drawing.Point(4, 32);
            this.dgvLocationInfo.Name = "dgvLocationInfo";
            this.dgvLocationInfo.RowHeadersWidth = 5;
            this.dgvLocationInfo.Size = new System.Drawing.Size(374, 112);
            this.dgvLocationInfo.TabIndex = 2;
            this.dgvLocationInfo.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocationInfo_CellValidated);
            // 
            // Selection
            // 
            this.Selection.DataPropertyName = "IsSelect";
            this.Selection.FalseValue = "false";
            this.Selection.HeaderText = "Allow";
            this.Selection.Name = "Selection";
            this.Selection.TrueValue = "true";
            this.Selection.Width = 45;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "LocationName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 300;
            // 
            // LocationId
            // 
            this.LocationId.DataPropertyName = "LocationId";
            this.LocationId.HeaderText = "LocationId";
            this.LocationId.Name = "LocationId";
            this.LocationId.Visible = false;
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.chkAllType);
            this.grpPayment.Controls.Add(this.chkConsiderProvider);
            this.grpPayment.Controls.Add(this.lblPaymentMethod);
            this.grpPayment.Controls.Add(this.rdoAll);
            this.grpPayment.Controls.Add(this.rdoCard);
            this.grpPayment.Controls.Add(this.rdoCash);
            this.grpPayment.Controls.Add(this.chkConsiderValue);
            this.grpPayment.Controls.Add(this.lblTo);
            this.grpPayment.Controls.Add(this.txtMaxValue);
            this.grpPayment.Controls.Add(this.lblValueRange);
            this.grpPayment.Controls.Add(this.txtMinValue);
            this.grpPayment.Controls.Add(this.lblType);
            this.grpPayment.Controls.Add(this.dgvType);
            this.grpPayment.Controls.Add(this.cmbProvider);
            this.grpPayment.Controls.Add(this.lblProvider);
            this.grpPayment.Location = new System.Drawing.Point(2, 41);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(377, 173);
            this.grpPayment.TabIndex = 45;
            this.grpPayment.TabStop = false;
            // 
            // chkAllType
            // 
            this.chkAllType.AutoSize = true;
            this.chkAllType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllType.Enabled = false;
            this.chkAllType.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllType.Location = new System.Drawing.Point(20, 84);
            this.chkAllType.Name = "chkAllType";
            this.chkAllType.Size = new System.Drawing.Size(38, 16);
            this.chkAllType.TabIndex = 74;
            this.chkAllType.Text = "All";
            this.chkAllType.UseVisualStyleBackColor = true;
            this.chkAllType.CheckedChanged += new System.EventHandler(this.chkAllType_CheckedChanged);
            // 
            // chkConsiderProvider
            // 
            this.chkConsiderProvider.AutoSize = true;
            this.chkConsiderProvider.Enabled = false;
            this.chkConsiderProvider.Location = new System.Drawing.Point(92, 35);
            this.chkConsiderProvider.Name = "chkConsiderProvider";
            this.chkConsiderProvider.Size = new System.Drawing.Size(15, 14);
            this.chkConsiderProvider.TabIndex = 73;
            this.chkConsiderProvider.UseVisualStyleBackColor = true;
            this.chkConsiderProvider.CheckedChanged += new System.EventHandler(this.chkConsiderProvider_CheckedChanged);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(3, 13);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(109, 13);
            this.lblPaymentMethod.TabIndex = 72;
            this.lblPaymentMethod.Text = "Payment Method*";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(315, 11);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(39, 17);
            this.rdoAll.TabIndex = 71;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "All";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.Click += new System.EventHandler(this.rdoAll_Click);
            // 
            // rdoCard
            // 
            this.rdoCard.AutoSize = true;
            this.rdoCard.Location = new System.Drawing.Point(216, 11);
            this.rdoCard.Name = "rdoCard";
            this.rdoCard.Size = new System.Drawing.Size(80, 17);
            this.rdoCard.TabIndex = 70;
            this.rdoCard.TabStop = true;
            this.rdoCard.Text = "Non Cash";
            this.rdoCard.UseVisualStyleBackColor = true;
            this.rdoCard.Click += new System.EventHandler(this.rdoCard_Click);
            // 
            // rdoCash
            // 
            this.rdoCash.AutoSize = true;
            this.rdoCash.Location = new System.Drawing.Point(132, 11);
            this.rdoCash.Name = "rdoCash";
            this.rdoCash.Size = new System.Drawing.Size(54, 17);
            this.rdoCash.TabIndex = 69;
            this.rdoCash.TabStop = true;
            this.rdoCash.Text = "Cash";
            this.rdoCash.UseVisualStyleBackColor = true;
            this.rdoCash.Click += new System.EventHandler(this.rdoCash_Click);
            // 
            // chkConsiderValue
            // 
            this.chkConsiderValue.AutoSize = true;
            this.chkConsiderValue.Location = new System.Drawing.Point(92, 151);
            this.chkConsiderValue.Name = "chkConsiderValue";
            this.chkConsiderValue.Size = new System.Drawing.Size(15, 14);
            this.chkConsiderValue.TabIndex = 41;
            this.chkConsiderValue.UseVisualStyleBackColor = true;
            this.chkConsiderValue.CheckedChanged += new System.EventHandler(this.chkConsiderValue_CheckedChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Enabled = false;
            this.lblTo.Location = new System.Drawing.Point(230, 150);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 68;
            this.lblTo.Text = "To";
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMaxValue.Enabled = false;
            this.txtMaxValue.Location = new System.Drawing.Point(256, 147);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(115, 21);
            this.txtMaxValue.TabIndex = 67;
            this.txtMaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxValue_KeyDown);
            this.txtMaxValue.Leave += new System.EventHandler(this.txtMaxValue_Leave);
            // 
            // lblValueRange
            // 
            this.lblValueRange.AutoSize = true;
            this.lblValueRange.Location = new System.Drawing.Point(3, 150);
            this.lblValueRange.Name = "lblValueRange";
            this.lblValueRange.Size = new System.Drawing.Size(78, 13);
            this.lblValueRange.TabIndex = 66;
            this.lblValueRange.Text = "Value Range";
            // 
            // txtMinValue
            // 
            this.txtMinValue.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMinValue.Enabled = false;
            this.txtMinValue.Location = new System.Drawing.Point(109, 147);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(115, 21);
            this.txtMinValue.TabIndex = 65;
            this.txtMinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMinValue_KeyDown);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Enabled = false;
            this.lblType.Location = new System.Drawing.Point(3, 59);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(66, 13);
            this.lblType.TabIndex = 64;
            this.lblType.Text = "Card Type";
            // 
            // dgvType
            // 
            this.dgvType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeSelection,
            this.Type});
            this.dgvType.Enabled = false;
            this.dgvType.Location = new System.Drawing.Point(109, 54);
            this.dgvType.Name = "dgvType";
            this.dgvType.RowHeadersWidth = 5;
            this.dgvType.Size = new System.Drawing.Size(262, 90);
            this.dgvType.TabIndex = 63;
            this.dgvType.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvType_CellValidated);
            // 
            // TypeSelection
            // 
            this.TypeSelection.DataPropertyName = "IsSelect";
            this.TypeSelection.HeaderText = "Allow";
            this.TypeSelection.Name = "TypeSelection";
            this.TypeSelection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeSelection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TypeSelection.Width = 45;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "PaymentCardName";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.Width = 190;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Enabled = false;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(109, 31);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(262, 21);
            this.cmbProvider.TabIndex = 62;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Enabled = false;
            this.lblProvider.Location = new System.Drawing.Point(3, 35);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(55, 13);
            this.lblProvider.TabIndex = 60;
            this.lblProvider.Text = "Provider";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCashierMsg);
            this.groupBox1.Controls.Add(this.txtCashierMessage);
            this.groupBox1.Controls.Add(this.lblBillDisplayMsg);
            this.groupBox1.Controls.Add(this.txtBillDisplayMessage);
            this.groupBox1.Location = new System.Drawing.Point(835, 411);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 107);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            // 
            // lblCashierMsg
            // 
            this.lblCashierMsg.AutoSize = true;
            this.lblCashierMsg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashierMsg.Location = new System.Drawing.Point(8, 11);
            this.lblCashierMsg.Name = "lblCashierMsg";
            this.lblCashierMsg.Size = new System.Drawing.Size(104, 13);
            this.lblCashierMsg.TabIndex = 88;
            this.lblCashierMsg.Text = "Cashier Message";
            // 
            // txtCashierMessage
            // 
            this.txtCashierMessage.Location = new System.Drawing.Point(3, 26);
            this.txtCashierMessage.Multiline = true;
            this.txtCashierMessage.Name = "txtCashierMessage";
            this.txtCashierMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCashierMessage.Size = new System.Drawing.Size(248, 31);
            this.txtCashierMessage.TabIndex = 87;
            this.txtCashierMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCashierMessage_KeyDown);
            // 
            // lblBillDisplayMsg
            // 
            this.lblBillDisplayMsg.AutoSize = true;
            this.lblBillDisplayMsg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDisplayMsg.Location = new System.Drawing.Point(7, 60);
            this.lblBillDisplayMsg.Name = "lblBillDisplayMsg";
            this.lblBillDisplayMsg.Size = new System.Drawing.Size(123, 13);
            this.lblBillDisplayMsg.TabIndex = 86;
            this.lblBillDisplayMsg.Text = "Bill Display Message";
            // 
            // txtBillDisplayMessage
            // 
            this.txtBillDisplayMessage.Location = new System.Drawing.Point(3, 75);
            this.txtBillDisplayMessage.Multiline = true;
            this.txtBillDisplayMessage.Name = "txtBillDisplayMessage";
            this.txtBillDisplayMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBillDisplayMessage.Size = new System.Drawing.Size(248, 29);
            this.txtBillDisplayMessage.TabIndex = 85;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvCustomerGroup);
            this.groupBox4.Location = new System.Drawing.Point(834, 209);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 99);
            this.groupBox4.TabIndex = 86;
            this.groupBox4.TabStop = false;
            // 
            // dgvCustomerGroup
            // 
            this.dgvCustomerGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn22});
            this.dgvCustomerGroup.Location = new System.Drawing.Point(3, 11);
            this.dgvCustomerGroup.Name = "dgvCustomerGroup";
            this.dgvCustomerGroup.RowHeadersWidth = 5;
            this.dgvCustomerGroup.Size = new System.Drawing.Size(250, 84);
            this.dgvCustomerGroup.TabIndex = 3;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsSelect";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Allow";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 45;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "CustomerGroupName";
            this.dataGridViewTextBoxColumn22.HeaderText = "Customer Group";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 180;
            // 
            // FrmPromotionMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1093, 562);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.grpDays);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpPayment);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPromotionMaster";
            this.Text = "Promotions Master";
            this.Load += new System.EventHandler(this.FrmPromotionMaster_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.grpBody, 0);
            this.Controls.SetChildIndex(this.grpPayment, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grpDays, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.tbPromotion.ResumeLayout(false);
            this.tbpBugXGetY.ResumeLayout(false);
            this.tbpBugXGetY.PerformLayout();
            this.tbpBuyX.ResumeLayout(false);
            this.tbpBuyProduct.ResumeLayout(false);
            this.tbpBuyProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyDetails)).EndInit();
            this.tbpBuyDepartment.ResumeLayout(false);
            this.tbpBuyDepartment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyDepartment)).EndInit();
            this.tbpBuyCategory.ResumeLayout(false);
            this.tbpBuyCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyCategory)).EndInit();
            this.tbpBuySubCategory.ResumeLayout(false);
            this.tbpBuySubCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuySubCategory)).EndInit();
            this.tbpBuySubCategory2.ResumeLayout(false);
            this.tbpBuySubCategory2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuySubCategory2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetDetails)).EndInit();
            this.tbpProductDiscount.ResumeLayout(false);
            this.tbpProductDiscount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDiscount)).EndInit();
            this.tbpDpartmentDiscount.ResumeLayout(false);
            this.tbpDpartmentDiscount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentDiscount)).EndInit();
            this.tbpCategoryDiscount.ResumeLayout(false);
            this.tbpCategoryDiscount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoryDiscount)).EndInit();
            this.tbpSubCategoryDiscount.ResumeLayout(false);
            this.tbpSubCategoryDiscount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory)).EndInit();
            this.tbpSubCategory2Discount.ResumeLayout(false);
            this.tbpSubCategory2Discount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategory2)).EndInit();
            this.tbpSubTotal.ResumeLayout(false);
            this.tbpSubTotal.PerformLayout();
            this.tbpGetGift.ResumeLayout(false);
            this.tbpGetGift.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpDays.ResumeLayout(false);
            this.grpDays.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).EndInit();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvType)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private CustomControls.TextBoxMasterCode txtPromotionCode;
        private CustomControls.TextBoxMasterDescription txtPromotionDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationPromotion;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.TabControl tbPromotion;
        private System.Windows.Forms.TabPage tbpBugXGetY;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox grpDays;
        private System.Windows.Forms.DateTimePicker dtpSundayTo;
        private System.Windows.Forms.DateTimePicker dtpSundayFrom;
        private System.Windows.Forms.DateTimePicker dtpSaturdayTo;
        private System.Windows.Forms.DateTimePicker dtpSaturdayFrom;
        private System.Windows.Forms.DateTimePicker dtpFridayTo;
        private System.Windows.Forms.DateTimePicker dtpFridayFrom;
        private System.Windows.Forms.DateTimePicker dtpThuresdayTo;
        private System.Windows.Forms.DateTimePicker dtpThuresdayFrom;
        private System.Windows.Forms.DateTimePicker dtpWednesdayTo;
        private System.Windows.Forms.DateTimePicker dtpWednesdayFrom;
        private System.Windows.Forms.DateTimePicker dtpTuesdayTo;
        private System.Windows.Forms.DateTimePicker dtpTuesdayFrom;
        private System.Windows.Forms.DateTimePicker dtpMondayTo;
        private System.Windows.Forms.CheckBox chkSunday;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox chkThuresday;
        private System.Windows.Forms.CheckBox chkWednesday;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.DateTimePicker dtpMondayFrom;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.ComboBox cmbGetUnit;
        private CustomControls.TextBoxCurrency txtGetDiscAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationGetProduct;
        private CustomControls.TextBoxQty txtGetQty;
        private CustomControls.TextBoxMasterDescription txtGetProductName;
        private CustomControls.TextBoxProductCode txtGetProductCode;
        private System.Windows.Forms.DataGridView dgvGetDetails;
        private System.Windows.Forms.TabPage tbpProductDiscount;
        private System.Windows.Forms.ComboBox cmbProductUnit;
        private CustomControls.TextBoxCurrency txtProductDiscAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationPdisProduct;
        private CustomControls.TextBoxQty txtProductQty;
        private CustomControls.TextBoxMasterDescription txtProductProductName;
        private CustomControls.TextBoxProductCode txtProductProductCode;
        private System.Windows.Forms.DataGridView dgvProductDiscount;
        private System.Windows.Forms.TabPage tbpDpartmentDiscount;
        private CustomControls.TextBoxCurrency txtDepartmentDiscAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationDepartment;
        private CustomControls.TextBoxQty txtDepartmentQty;
        private CustomControls.TextBoxMasterDescription txtDepartmentDescription;
        private CustomControls.TextBoxProductCode txtDepartmentCode;
        private System.Windows.Forms.DataGridView dgvDepartmentDiscount;
        private System.Windows.Forms.TabPage tbpCategoryDiscount;
        private CustomControls.TextBoxCurrency txtCategoryDiscAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationCategory;
        private CustomControls.TextBoxQty txtCategoryQty;
        private CustomControls.TextBoxMasterDescription txtCategoryDescription;
        private CustomControls.TextBoxProductCode txtCategoryCode;
        private System.Windows.Forms.DataGridView dgvCategoryDiscount;
        private System.Windows.Forms.TabPage tbpSubCategoryDiscount;
        private CustomControls.TextBoxCurrency txtSubCategoryDiscAmount;
        private System.Windows.Forms.CheckBox chkAutoCompleationSubCategory;
        private CustomControls.TextBoxQty txtSubCategoryQty;
        private CustomControls.TextBoxMasterDescription txtSubCategoryDescription;
        private CustomControls.TextBoxProductCode txtSubCategoryCode;
        private System.Windows.Forms.DataGridView dgvSubCategory;
        private System.Windows.Forms.TabPage tbpSubTotal;
        private CustomControls.TextBoxCurrency txtSubTotalDiscountValue;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiscountPercentage;
        private CustomControls.TextBoxPercentGen txtSubTotalDiscountPercentage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnSendSms;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAllLocations;
        private System.Windows.Forms.DataGridView dgvLocationInfo;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.CheckBox chkConsiderValue;
        private System.Windows.Forms.Label lblTo;
        private CustomControls.TextBoxCurrency txtMaxValue;
        private System.Windows.Forms.Label lblValueRange;
        private CustomControls.TextBoxCurrency txtMinValue;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DataGridView dgvType;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoCard;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.ComboBox cmbRecipient;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkSundayTime;
        private System.Windows.Forms.CheckBox chkSaturdayTime;
        private System.Windows.Forms.CheckBox chkFridayTime;
        private System.Windows.Forms.CheckBox chkThuresdayTime;
        private System.Windows.Forms.CheckBox chkWednesdayTime;
        private System.Windows.Forms.CheckBox chkTuesdayTime;
        private System.Windows.Forms.CheckBox chkMondayTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxNumeric txtGetPoints;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvCustomerGroup;
        private CustomControls.TextBoxNumeric txtProductPoints;
        private CustomControls.TextBoxNumeric txtDepartmentPoints;
        private CustomControls.TextBoxNumeric txtCategoryPoints;
        private CustomControls.TextBoxNumeric txtSubCategoryPoints;
        private CustomControls.TextBoxCurrency txtGetRate;
        private CustomControls.TextBoxCurrency txtProductRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.CheckBox chkAllType;
        private System.Windows.Forms.CheckBox chkConsiderProvider;
        private System.Windows.Forms.TabControl tbpBuyX;
        private System.Windows.Forms.TabPage tbpBuyProduct;
        private CustomControls.TextBoxCurrency txtBuyRate;
        private System.Windows.Forms.ComboBox cmbBuyUnit;
        private System.Windows.Forms.CheckBox chkAutoCompleationBuyProduct;
        private CustomControls.TextBoxQty txtBuyQty;
        private CustomControls.TextBoxMasterDescription txtBuyProductName;
        private CustomControls.TextBoxProductCode txtBuyProductCode;
        private System.Windows.Forms.DataGridView dgvBuyDetails;
        private System.Windows.Forms.TabPage tbpBuyDepartment;
        private System.Windows.Forms.TabPage tbpBuyCategory;
        private System.Windows.Forms.TabPage tbpBuySubCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationBuyDepartment;
        private CustomControls.TextBoxQty txtBuyDepartmentQty;
        private CustomControls.TextBoxMasterDescription txtBuyDepartmentName;
        private CustomControls.TextBoxProductCode txtBuyDepartmentCode;
        private System.Windows.Forms.DataGridView dgvBuyDepartment;
        private System.Windows.Forms.CheckBox chkAutoCompleationBuyCaytegory;
        private CustomControls.TextBoxQty txtBuyCategoryQty;
        private CustomControls.TextBoxMasterDescription txtBuyCategoryName;
        private CustomControls.TextBoxProductCode txtBuyCategoryCode;
        private System.Windows.Forms.DataGridView dgvBuyCategory;
        private System.Windows.Forms.CheckBox chkAutoCompleationBuySubCategory;
        private CustomControls.TextBoxQty txtBuySubCategoryQty;
        private CustomControls.TextBoxMasterDescription txtBuySubCategoryName;
        private CustomControls.TextBoxProductCode txtBuySubCategoryCode;
        private System.Windows.Forms.DataGridView dgvBuySubCategory;
        private CustomControls.TextBoxNumeric txtPoints;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.CheckBox chkAutoApply;
        private System.Windows.Forms.Label lblCashierMsg;
        private System.Windows.Forms.TextBox txtCashierMessage;
        private System.Windows.Forms.Label lblBillDisplayMsg;
        private System.Windows.Forms.TextBox txtBillDisplayMessage;
        private System.Windows.Forms.Label lblPromotionType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TypeSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubCategoryPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationId;
        private System.Windows.Forms.ComboBox cmbPromotionType;
        private System.Windows.Forms.CheckBox ChkAutoSelectGetProduct;
        private System.Windows.Forms.CheckBox ChkCombined;
        private System.Windows.Forms.CheckBox ChkGiftVoucher;
        private System.Windows.Forms.TabPage tbpBuySubCategory2;
        private System.Windows.Forms.CheckBox chkAutoCompleationBuySubCategory2;
        private CustomControls.TextBoxQty txtBuySubCategory2Qty;
        private CustomControls.TextBoxMasterDescription txtBuySubCategory2Name;
        private CustomControls.TextBoxProductCode txtBuySubCategory2Code;
        private System.Windows.Forms.DataGridView dgvBuySubCategory2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetDiscPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn GetDiscAmount;
        private System.Windows.Forms.TabPage tbpGetGift;
        private System.Windows.Forms.CheckBox ChkAutoCompleationSubCategory2;
        private CustomControls.TextBoxQty txtGiftBuyQty;
        private CustomControls.TextBoxMasterDescription txtGiftSubCategory2Name;
        private CustomControls.TextBoxProductCode txtGiftSubCategory2Code;
        private System.Windows.Forms.DataGridView dataGridView1;
        private CustomControls.TextBoxCurrency txtGiftGetValue;
        private CustomControls.TextBoxQty txtGiftGetQty;
        private CustomControls.TextBoxMasterDescription txtGiftDescription;
        private CustomControls.TextBoxCurrency txtGiftValueRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiftQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiftValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Increase;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Raffle;
        private System.Windows.Forms.TabPage tbpSubCategory2Discount;
        private CustomControls.TextBoxNumeric txtSubCategory2Points;
        private CustomControls.TextBoxCurrency txtSubCategory2DiscAmount;
        private System.Windows.Forms.CheckBox ChkSubCategory2Dis;
        private CustomControls.TextBoxQty txtSubCategory2Qty;
        private CustomControls.TextBoxMasterDescription txtSubCategory2Description;
        private CustomControls.TextBoxProductCode txtSubCategory2Code;
        private System.Windows.Forms.DataGridView dgvSubCategory2;
        private CustomControls.TextBoxQty txtSubCategory2DiscPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private CustomControls.TextBoxQty txtGetDiscPercentage;
        private CustomControls.TextBoxQty txtProductDiscPercentage;
        private CustomControls.TextBoxQty txtDepartmentDiscPercentage;
        private CustomControls.TextBoxQty txtCategoryDiscPercentage;
        private CustomControls.TextBoxQty txtSubCategoryDiscPercentage;
    }
}
