namespace UI.Windows
{
    partial class FrmLogisticTransactionSearch
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblViewedRequestions = new System.Windows.Forms.Label();
            this.lblPendingPO = new System.Windows.Forms.Label();
            this.lblExpired = new System.Windows.Forms.Label();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblOutbox = new System.Windows.Forms.Label();
            this.lblInbox = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabSummery = new System.Windows.Forms.TabControl();
            this.tbpMain = new System.Windows.Forms.TabPage();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.DocumentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestedLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpPendingPO = new System.Windows.Forms.TabPage();
            this.dgvPendingPO = new System.Windows.Forms.DataGridView();
            this.PONo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PODate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POExpiaryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpViewedRequestions = new System.Windows.Forms.TabPage();
            this.dgvViewedRequestions = new System.Windows.Forms.DataGridView();
            this.DocumentNoViewed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new Glass.GlassButton();
            this.invProductExtendedPropertyValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new UI.Windows.CustomControls.uc_GButton();
            this.btnViewedRequestions = new UI.Windows.CustomControls.uc_GButton();
            this.btnPendingPurchaseOrder = new UI.Windows.CustomControls.uc_GButton();
            this.btnExpiredRequestion = new UI.Windows.CustomControls.uc_GButton();
            this.btnPendingRequestion = new UI.Windows.CustomControls.uc_GButton();
            this.btnRequestionOutbox = new UI.Windows.CustomControls.uc_GButton();
            this.btnRequestionInbox = new UI.Windows.CustomControls.uc_GButton();
            this.btnSupplier = new UI.Windows.CustomControls.uc_GButton();
            this.btnProductMaster = new UI.Windows.CustomControls.uc_GButton();
            this.btnSubCategory2 = new UI.Windows.CustomControls.uc_GButton();
            this.btnSubCategory = new UI.Windows.CustomControls.uc_GButton();
            this.btnCategory = new UI.Windows.CustomControls.uc_GButton();
            this.btnDepartment = new UI.Windows.CustomControls.uc_GButton();
            this.btnPurchaseReturnNote = new UI.Windows.CustomControls.uc_GButton();
            this.btnMaterialAllocationNote = new UI.Windows.CustomControls.uc_GButton();
            this.btnMaterialRequestNote = new UI.Windows.CustomControls.uc_GButton();
            this.btnTransferOfGoodsNote = new UI.Windows.CustomControls.uc_GButton();
            this.btnGoodsReceivedNote = new UI.Windows.CustomControls.uc_GButton();
            this.btnPurchaseOrder = new UI.Windows.CustomControls.uc_GButton();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabSummery.SuspendLayout();
            this.tbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tbpPendingPO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingPO)).BeginInit();
            this.tbpViewedRequestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewedRequestions)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedPropertyValueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblViewedRequestions);
            this.groupBox2.Controls.Add(this.btnViewedRequestions);
            this.groupBox2.Controls.Add(this.lblPendingPO);
            this.groupBox2.Controls.Add(this.lblExpired);
            this.groupBox2.Controls.Add(this.lblPending);
            this.groupBox2.Controls.Add(this.lblOutbox);
            this.groupBox2.Controls.Add(this.lblInbox);
            this.groupBox2.Controls.Add(this.btnPendingPurchaseOrder);
            this.groupBox2.Controls.Add(this.btnExpiredRequestion);
            this.groupBox2.Controls.Add(this.btnPendingRequestion);
            this.groupBox2.Controls.Add(this.btnRequestionOutbox);
            this.groupBox2.Controls.Add(this.btnRequestionInbox);
            this.groupBox2.Location = new System.Drawing.Point(2, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 194);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            // 
            // lblViewedRequestions
            // 
            this.lblViewedRequestions.AutoSize = true;
            this.lblViewedRequestions.Location = new System.Drawing.Point(160, 50);
            this.lblViewedRequestions.Name = "lblViewedRequestions";
            this.lblViewedRequestions.Size = new System.Drawing.Size(24, 13);
            this.lblViewedRequestions.TabIndex = 70;
            this.lblViewedRequestions.Text = "(0)";
            // 
            // lblPendingPO
            // 
            this.lblPendingPO.AutoSize = true;
            this.lblPendingPO.Location = new System.Drawing.Point(160, 168);
            this.lblPendingPO.Name = "lblPendingPO";
            this.lblPendingPO.Size = new System.Drawing.Size(24, 13);
            this.lblPendingPO.TabIndex = 68;
            this.lblPendingPO.Text = "(0)";
            // 
            // lblExpired
            // 
            this.lblExpired.AutoSize = true;
            this.lblExpired.Location = new System.Drawing.Point(160, 137);
            this.lblExpired.Name = "lblExpired";
            this.lblExpired.Size = new System.Drawing.Size(24, 13);
            this.lblExpired.TabIndex = 67;
            this.lblExpired.Text = "(0)";
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Location = new System.Drawing.Point(160, 108);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(24, 13);
            this.lblPending.TabIndex = 66;
            this.lblPending.Text = "(0)";
            // 
            // lblOutbox
            // 
            this.lblOutbox.AutoSize = true;
            this.lblOutbox.Location = new System.Drawing.Point(160, 78);
            this.lblOutbox.Name = "lblOutbox";
            this.lblOutbox.Size = new System.Drawing.Size(24, 13);
            this.lblOutbox.TabIndex = 65;
            this.lblOutbox.Text = "(0)";
            // 
            // lblInbox
            // 
            this.lblInbox.AutoSize = true;
            this.lblInbox.Location = new System.Drawing.Point(160, 21);
            this.lblInbox.Name = "lblInbox";
            this.lblInbox.Size = new System.Drawing.Size(24, 13);
            this.lblInbox.TabIndex = 0;
            this.lblInbox.Text = "(0)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPurchaseReturnNote);
            this.groupBox3.Controls.Add(this.btnMaterialAllocationNote);
            this.groupBox3.Controls.Add(this.btnMaterialRequestNote);
            this.groupBox3.Controls.Add(this.btnTransferOfGoodsNote);
            this.groupBox3.Controls.Add(this.btnGoodsReceivedNote);
            this.groupBox3.Controls.Add(this.btnPurchaseOrder);
            this.groupBox3.Location = new System.Drawing.Point(2, 318);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 140);
            this.groupBox3.TabIndex = 59;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tabSummery);
            this.groupBox4.Location = new System.Drawing.Point(219, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(668, 436);
            this.groupBox4.TabIndex = 60;
            this.groupBox4.TabStop = false;
            // 
            // tabSummery
            // 
            this.tabSummery.Controls.Add(this.tbpMain);
            this.tabSummery.Controls.Add(this.tbpPendingPO);
            this.tabSummery.Controls.Add(this.tbpViewedRequestions);
            this.tabSummery.Location = new System.Drawing.Point(3, 10);
            this.tabSummery.Name = "tabSummery";
            this.tabSummery.SelectedIndex = 0;
            this.tabSummery.Size = new System.Drawing.Size(659, 423);
            this.tabSummery.TabIndex = 0;
            // 
            // tbpMain
            // 
            this.tbpMain.Controls.Add(this.dgvMain);
            this.tbpMain.Location = new System.Drawing.Point(4, 22);
            this.tbpMain.Name = "tbpMain";
            this.tbpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMain.Size = new System.Drawing.Size(651, 397);
            this.tbpMain.TabIndex = 0;
            this.tbpMain.Text = "Main";
            this.tbpMain.UseVisualStyleBackColor = true;
            // 
            // dgvMain
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocumentNo,
            this.RequestedLocation,
            this.RequestedDate,
            this.RequestedBy});
            this.dgvMain.Location = new System.Drawing.Point(3, 3);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 4;
            this.dgvMain.Size = new System.Drawing.Size(645, 388);
            this.dgvMain.TabIndex = 0;
            this.dgvMain.DoubleClick += new System.EventHandler(this.dgvMain_DoubleClick);
            // 
            // DocumentNo
            // 
            this.DocumentNo.DataPropertyName = "DocumentNo";
            this.DocumentNo.HeaderText = "Document No";
            this.DocumentNo.Name = "DocumentNo";
            this.DocumentNo.ReadOnly = true;
            this.DocumentNo.Width = 140;
            // 
            // RequestedLocation
            // 
            this.RequestedLocation.DataPropertyName = "LocationName";
            this.RequestedLocation.HeaderText = "Requested Location";
            this.RequestedLocation.Name = "RequestedLocation";
            this.RequestedLocation.ReadOnly = true;
            this.RequestedLocation.Width = 200;
            // 
            // RequestedDate
            // 
            this.RequestedDate.DataPropertyName = "DocumentDateShort";
            this.RequestedDate.HeaderText = "Requested Date";
            this.RequestedDate.Name = "RequestedDate";
            this.RequestedDate.ReadOnly = true;
            this.RequestedDate.Width = 140;
            // 
            // RequestedBy
            // 
            this.RequestedBy.DataPropertyName = "RequestedBy";
            this.RequestedBy.HeaderText = "Requested By";
            this.RequestedBy.Name = "RequestedBy";
            this.RequestedBy.ReadOnly = true;
            this.RequestedBy.Width = 150;
            // 
            // tbpPendingPO
            // 
            this.tbpPendingPO.Controls.Add(this.dgvPendingPO);
            this.tbpPendingPO.Location = new System.Drawing.Point(4, 22);
            this.tbpPendingPO.Name = "tbpPendingPO";
            this.tbpPendingPO.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPendingPO.Size = new System.Drawing.Size(651, 397);
            this.tbpPendingPO.TabIndex = 1;
            this.tbpPendingPO.Text = "Pending Po";
            this.tbpPendingPO.UseVisualStyleBackColor = true;
            // 
            // dgvPendingPO
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvPendingPO.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPendingPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingPO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PONo,
            this.PODate,
            this.POExpiaryDate});
            this.dgvPendingPO.Location = new System.Drawing.Point(3, 3);
            this.dgvPendingPO.Name = "dgvPendingPO";
            this.dgvPendingPO.RowHeadersWidth = 4;
            this.dgvPendingPO.Size = new System.Drawing.Size(645, 391);
            this.dgvPendingPO.TabIndex = 1;
            // 
            // PONo
            // 
            this.PONo.DataPropertyName = "DocumentNo";
            this.PONo.HeaderText = "PO No";
            this.PONo.Name = "PONo";
            this.PONo.ReadOnly = true;
            this.PONo.Width = 150;
            // 
            // PODate
            // 
            this.PODate.DataPropertyName = "DocumentDateShort";
            this.PODate.HeaderText = "PO Date";
            this.PODate.Name = "PODate";
            this.PODate.ReadOnly = true;
            this.PODate.Width = 150;
            // 
            // POExpiaryDate
            // 
            this.POExpiaryDate.DataPropertyName = "ExpiryDateShort";
            this.POExpiaryDate.HeaderText = "Expiary Date";
            this.POExpiaryDate.Name = "POExpiaryDate";
            this.POExpiaryDate.ReadOnly = true;
            this.POExpiaryDate.Width = 150;
            // 
            // tbpViewedRequestions
            // 
            this.tbpViewedRequestions.Controls.Add(this.dgvViewedRequestions);
            this.tbpViewedRequestions.Location = new System.Drawing.Point(4, 22);
            this.tbpViewedRequestions.Name = "tbpViewedRequestions";
            this.tbpViewedRequestions.Size = new System.Drawing.Size(651, 397);
            this.tbpViewedRequestions.TabIndex = 2;
            this.tbpViewedRequestions.Text = "Viewed Requestions";
            this.tbpViewedRequestions.UseVisualStyleBackColor = true;
            // 
            // dgvViewedRequestions
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvViewedRequestions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvViewedRequestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewedRequestions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocumentNoViewed,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvViewedRequestions.Location = new System.Drawing.Point(3, 3);
            this.dgvViewedRequestions.Name = "dgvViewedRequestions";
            this.dgvViewedRequestions.RowHeadersWidth = 4;
            this.dgvViewedRequestions.Size = new System.Drawing.Size(645, 391);
            this.dgvViewedRequestions.TabIndex = 1;
            this.dgvViewedRequestions.DoubleClick += new System.EventHandler(this.dgvViewedRequestions_DoubleClick);
            // 
            // DocumentNoViewed
            // 
            this.DocumentNoViewed.DataPropertyName = "DocumentNo";
            this.DocumentNoViewed.HeaderText = "Document No";
            this.DocumentNoViewed.Name = "DocumentNoViewed";
            this.DocumentNoViewed.ReadOnly = true;
            this.DocumentNoViewed.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "LocationName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Requested Location";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DocumentDateShort";
            this.dataGridViewTextBoxColumn3.HeaderText = "Requested Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 140;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "RequestedBy";
            this.dataGridViewTextBoxColumn4.HeaderText = "Requested By";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSupplier);
            this.groupBox5.Controls.Add(this.btnProductMaster);
            this.groupBox5.Controls.Add(this.btnSubCategory2);
            this.groupBox5.Controls.Add(this.btnSubCategory);
            this.groupBox5.Controls.Add(this.btnCategory);
            this.groupBox5.Controls.Add(this.btnDepartment);
            this.groupBox5.Location = new System.Drawing.Point(2, 184);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(214, 138);
            this.groupBox5.TabIndex = 71;
            this.groupBox5.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Image = global::UI.Windows.Properties.Resources.Mag;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(316, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 55;
            this.btnSearch.Text = "&Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // invProductExtendedPropertyValueBindingSource
            // 
            this.invProductExtendedPropertyValueBindingSource.DataSource = typeof(Domain.InvProductExtendedPropertyValue);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(754, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(34, 15);
            this.lblTime.TabIndex = 72;
            this.lblTime.Text = "Time";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(226, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 15);
            this.lblDate.TabIndex = 73;
            this.lblDate.Text = "Date";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(495, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 15);
            this.lblUser.TabIndex = 74;
            this.lblUser.Text = "User";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::UI.Windows.Properties.Resources.close1;
            this.btnExit.Location = new System.Drawing.Point(855, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 24);
            this.btnExit.TabIndex = 70;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnViewedRequestions
            // 
            this.btnViewedRequestions.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewedRequestions.Location = new System.Drawing.Point(6, 46);
            this.btnViewedRequestions.Name = "btnViewedRequestions";
            this.btnViewedRequestions.Size = new System.Drawing.Size(152, 23);
            this.btnViewedRequestions.TabIndex = 69;
            this.btnViewedRequestions.Text = "Viewed Requestions";
            this.btnViewedRequestions.Click += new System.EventHandler(this.btnViewedRequestions_Click);
            // 
            // btnPendingPurchaseOrder
            // 
            this.btnPendingPurchaseOrder.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPendingPurchaseOrder.Location = new System.Drawing.Point(6, 162);
            this.btnPendingPurchaseOrder.Name = "btnPendingPurchaseOrder";
            this.btnPendingPurchaseOrder.Size = new System.Drawing.Size(152, 23);
            this.btnPendingPurchaseOrder.TabIndex = 64;
            this.btnPendingPurchaseOrder.Text = "Pending Purchase Order";
            this.btnPendingPurchaseOrder.Click += new System.EventHandler(this.btnPendingPurchaseOrder_Click);
            // 
            // btnExpiredRequestion
            // 
            this.btnExpiredRequestion.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpiredRequestion.Location = new System.Drawing.Point(6, 133);
            this.btnExpiredRequestion.Name = "btnExpiredRequestion";
            this.btnExpiredRequestion.Size = new System.Drawing.Size(152, 23);
            this.btnExpiredRequestion.TabIndex = 63;
            this.btnExpiredRequestion.Text = "Expired Requestion";
            this.btnExpiredRequestion.Click += new System.EventHandler(this.btnExpiredRequestion_Click);
            // 
            // btnPendingRequestion
            // 
            this.btnPendingRequestion.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPendingRequestion.Location = new System.Drawing.Point(6, 104);
            this.btnPendingRequestion.Name = "btnPendingRequestion";
            this.btnPendingRequestion.Size = new System.Drawing.Size(152, 23);
            this.btnPendingRequestion.TabIndex = 62;
            this.btnPendingRequestion.Text = "Pending Requestion";
            this.btnPendingRequestion.Click += new System.EventHandler(this.btnPendingRequestion_Click);
            // 
            // btnRequestionOutbox
            // 
            this.btnRequestionOutbox.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestionOutbox.Location = new System.Drawing.Point(6, 75);
            this.btnRequestionOutbox.Name = "btnRequestionOutbox";
            this.btnRequestionOutbox.Size = new System.Drawing.Size(152, 23);
            this.btnRequestionOutbox.TabIndex = 61;
            this.btnRequestionOutbox.Text = "Requestion Outbox";
            this.btnRequestionOutbox.Click += new System.EventHandler(this.btnRequestionOutbox_Click);
            // 
            // btnRequestionInbox
            // 
            this.btnRequestionInbox.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestionInbox.Location = new System.Drawing.Point(6, 17);
            this.btnRequestionInbox.Name = "btnRequestionInbox";
            this.btnRequestionInbox.Size = new System.Drawing.Size(152, 23);
            this.btnRequestionInbox.TabIndex = 60;
            this.btnRequestionInbox.Text = "Requestion Inbox";
            this.btnRequestionInbox.Click += new System.EventHandler(this.btnRequestionInbox_Click);
            // 
            // btnSupplier
            // 
            this.btnSupplier.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplier.Location = new System.Drawing.Point(109, 94);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(98, 39);
            this.btnSupplier.TabIndex = 72;
            this.btnSupplier.Text = "Supplier";
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // btnProductMaster
            // 
            this.btnProductMaster.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductMaster.Location = new System.Drawing.Point(6, 94);
            this.btnProductMaster.Name = "btnProductMaster";
            this.btnProductMaster.Size = new System.Drawing.Size(98, 39);
            this.btnProductMaster.TabIndex = 71;
            this.btnProductMaster.Text = "Product Master";
            this.btnProductMaster.Click += new System.EventHandler(this.btnProductMaster_Click);
            // 
            // btnSubCategory2
            // 
            this.btnSubCategory2.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubCategory2.Location = new System.Drawing.Point(109, 53);
            this.btnSubCategory2.Name = "btnSubCategory2";
            this.btnSubCategory2.Size = new System.Drawing.Size(98, 39);
            this.btnSubCategory2.TabIndex = 70;
            this.btnSubCategory2.Text = "Sub Category 2";
            this.btnSubCategory2.Click += new System.EventHandler(this.btnSubCategory2_Click);
            // 
            // btnSubCategory
            // 
            this.btnSubCategory.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubCategory.Location = new System.Drawing.Point(6, 53);
            this.btnSubCategory.Name = "btnSubCategory";
            this.btnSubCategory.Size = new System.Drawing.Size(98, 39);
            this.btnSubCategory.TabIndex = 67;
            this.btnSubCategory.Text = "Sub Category";
            this.btnSubCategory.Click += new System.EventHandler(this.btnSubCategory_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategory.Location = new System.Drawing.Point(109, 13);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(98, 39);
            this.btnCategory.TabIndex = 66;
            this.btnCategory.Text = "Category";
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnDepartment
            // 
            this.btnDepartment.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartment.Location = new System.Drawing.Point(6, 13);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(98, 39);
            this.btnDepartment.TabIndex = 65;
            this.btnDepartment.Text = "Department";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // btnPurchaseReturnNote
            // 
            this.btnPurchaseReturnNote.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseReturnNote.Location = new System.Drawing.Point(109, 53);
            this.btnPurchaseReturnNote.Name = "btnPurchaseReturnNote";
            this.btnPurchaseReturnNote.Size = new System.Drawing.Size(98, 39);
            this.btnPurchaseReturnNote.TabIndex = 70;
            this.btnPurchaseReturnNote.Text = "Purchase Return Note";
            this.btnPurchaseReturnNote.Click += new System.EventHandler(this.btnPurchaseReturnNote_Click);
            // 
            // btnMaterialAllocationNote
            // 
            this.btnMaterialAllocationNote.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialAllocationNote.Location = new System.Drawing.Point(109, 96);
            this.btnMaterialAllocationNote.Name = "btnMaterialAllocationNote";
            this.btnMaterialAllocationNote.Size = new System.Drawing.Size(98, 39);
            this.btnMaterialAllocationNote.TabIndex = 69;
            this.btnMaterialAllocationNote.Text = "Material Allocation Note";
            this.btnMaterialAllocationNote.Click += new System.EventHandler(this.btnMaterialAllocationNote_Click);
            // 
            // btnMaterialRequestNote
            // 
            this.btnMaterialRequestNote.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialRequestNote.Location = new System.Drawing.Point(6, 96);
            this.btnMaterialRequestNote.Name = "btnMaterialRequestNote";
            this.btnMaterialRequestNote.Size = new System.Drawing.Size(98, 39);
            this.btnMaterialRequestNote.TabIndex = 68;
            this.btnMaterialRequestNote.Text = "Material Request Note";
            this.btnMaterialRequestNote.Click += new System.EventHandler(this.btnMaterialRequestNote_Click);
            // 
            // btnTransferOfGoodsNote
            // 
            this.btnTransferOfGoodsNote.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferOfGoodsNote.Location = new System.Drawing.Point(6, 53);
            this.btnTransferOfGoodsNote.Name = "btnTransferOfGoodsNote";
            this.btnTransferOfGoodsNote.Size = new System.Drawing.Size(98, 39);
            this.btnTransferOfGoodsNote.TabIndex = 67;
            this.btnTransferOfGoodsNote.Text = "Transfer of Goods Note";
            this.btnTransferOfGoodsNote.Click += new System.EventHandler(this.btnTransferOfGoodsNote_Click);
            // 
            // btnGoodsReceivedNote
            // 
            this.btnGoodsReceivedNote.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoodsReceivedNote.Location = new System.Drawing.Point(109, 11);
            this.btnGoodsReceivedNote.Name = "btnGoodsReceivedNote";
            this.btnGoodsReceivedNote.Size = new System.Drawing.Size(98, 39);
            this.btnGoodsReceivedNote.TabIndex = 66;
            this.btnGoodsReceivedNote.Text = "Goods Received Note";
            this.btnGoodsReceivedNote.Click += new System.EventHandler(this.btnGoodsReceivedNote_Click);
            // 
            // btnPurchaseOrder
            // 
            this.btnPurchaseOrder.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseOrder.Location = new System.Drawing.Point(6, 11);
            this.btnPurchaseOrder.Name = "btnPurchaseOrder";
            this.btnPurchaseOrder.Size = new System.Drawing.Size(98, 39);
            this.btnPurchaseOrder.TabIndex = 65;
            this.btnPurchaseOrder.Text = "Purchase Order";
            this.btnPurchaseOrder.Click += new System.EventHandler(this.btnPurchaseOrder_Click);
            // 
            // FrmLogisticTransactionSearch
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(891, 462);
            this.ControlBox = false;
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmLogisticTransactionSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logistic Transaction Summery";
            this.Load += new System.EventHandler(this.FrmLogisticTransactionSearch_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabSummery.ResumeLayout(false);
            this.tbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.tbpPendingPO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingPO)).EndInit();
            this.tbpViewedRequestions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewedRequestions)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedPropertyValueBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource invProductExtendedPropertyValueBindingSource;
        protected Glass.GlassButton btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private CustomControls.uc_GButton btnPendingPurchaseOrder;
        private CustomControls.uc_GButton btnExpiredRequestion;
        private CustomControls.uc_GButton btnPendingRequestion;
        private CustomControls.uc_GButton btnRequestionOutbox;
        private CustomControls.uc_GButton btnRequestionInbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private CustomControls.uc_GButton btnPurchaseOrder;
        private CustomControls.uc_GButton btnGoodsReceivedNote;
        private CustomControls.uc_GButton btnMaterialAllocationNote;
        private CustomControls.uc_GButton btnMaterialRequestNote;
        private CustomControls.uc_GButton btnTransferOfGoodsNote;
        private System.Windows.Forms.Label lblPendingPO;
        private System.Windows.Forms.Label lblExpired;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label lblOutbox;
        private System.Windows.Forms.Label lblInbox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabSummery;
        private System.Windows.Forms.TabPage tbpMain;
        private System.Windows.Forms.TabPage tbpPendingPO;
        private CustomControls.uc_GButton btnExit;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridView dgvPendingPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestedLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestedBy;
        private System.Windows.Forms.TabPage tbpViewedRequestions;
        private System.Windows.Forms.DataGridView dgvViewedRequestions;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentNoViewed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label lblViewedRequestions;
        private CustomControls.uc_GButton btnViewedRequestions;
        private System.Windows.Forms.DataGridViewTextBoxColumn PONo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PODate;
        private System.Windows.Forms.DataGridViewTextBoxColumn POExpiaryDate;
        private CustomControls.uc_GButton btnPurchaseReturnNote;
        private System.Windows.Forms.GroupBox groupBox5;
        private CustomControls.uc_GButton btnSupplier;
        private CustomControls.uc_GButton btnProductMaster;
        private CustomControls.uc_GButton btnSubCategory2;
        private CustomControls.uc_GButton btnSubCategory;
        private CustomControls.uc_GButton btnCategory;
        private CustomControls.uc_GButton btnDepartment;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Timer timer1;
    }
}
