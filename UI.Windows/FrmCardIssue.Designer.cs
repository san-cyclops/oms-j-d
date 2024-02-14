namespace UI.Windows
{
    partial class FrmCardIssue
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
            this.txtEmployeeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.txtEmployeeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpIssueDate = new System.Windows.Forms.DateTimePicker();
            this.btnPoDetails = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationDocNo = new System.Windows.Forms.CheckBox();
            this.txtDocNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoCompleationCardRange = new System.Windows.Forms.CheckBox();
            this.txtCardNoTo = new System.Windows.Forms.TextBox();
            this.txtCardNoFrom = new System.Windows.Forms.TextBox();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.encodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loyaltyCardGenerationDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblCardNoTo = new System.Windows.Forms.Label();
            this.lblCardNoFrom = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loyaltyCardGenerationDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(371, 439);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 439);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtEmployeeCode);
            this.grpHeader.Controls.Add(this.txtEmployeeName);
            this.grpHeader.Controls.Add(this.label2);
            this.grpHeader.Controls.Add(this.chkAutoCompleationEmployee);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpIssueDate);
            this.grpHeader.Controls.Add(this.btnPoDetails);
            this.grpHeader.Controls.Add(this.lblRemark);
            this.grpHeader.Controls.Add(this.txtRemark);
            this.grpHeader.Controls.Add(this.lblIssueDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationDocNo);
            this.grpHeader.Controls.Add(this.txtDocNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(686, 111);
            this.grpHeader.TabIndex = 18;
            this.grpHeader.TabStop = false;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.IsAutoComplete = false;
            this.txtEmployeeCode.ItemCollection = null;
            this.txtEmployeeCode.Location = new System.Drawing.Point(124, 37);
            this.txtEmployeeCode.MasterCode = "";
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(136, 21);
            this.txtEmployeeCode.TabIndex = 61;
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Validated += new System.EventHandler(this.txtEmployeeCode_Validated);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(262, 37);
            this.txtEmployeeName.MasterDescription = "";
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(418, 21);
            this.txtEmployeeName.TabIndex = 62;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            this.txtEmployeeName.Validated += new System.EventHandler(this.txtEmployeeName_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Employee";
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(103, 40);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 63;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(327, 84);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(353, 21);
            this.cmbLocation.TabIndex = 16;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(261, 87);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(55, 13);
            this.lblLocation.TabIndex = 60;
            this.lblLocation.Text = "Issue To";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(124, 84);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(136, 21);
            this.txtReferenceNo.TabIndex = 15;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 87);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpIssueDate
            // 
            this.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssueDate.Location = new System.Drawing.Point(573, 14);
            this.dtpIssueDate.Name = "dtpIssueDate";
            this.dtpIssueDate.Size = new System.Drawing.Size(107, 21);
            this.dtpIssueDate.TabIndex = 11;
            this.dtpIssueDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpIssueDate_KeyDown);
            // 
            // btnPoDetails
            // 
            this.btnPoDetails.Location = new System.Drawing.Point(261, 13);
            this.btnPoDetails.Name = "btnPoDetails";
            this.btnPoDetails.Size = new System.Drawing.Size(28, 23);
            this.btnPoDetails.TabIndex = 2;
            this.btnPoDetails.Text = "...";
            this.btnPoDetails.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(7, 63);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 19;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(124, 60);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(556, 21);
            this.txtRemark.TabIndex = 9;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Location = new System.Drawing.Point(505, 18);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(69, 13);
            this.lblIssueDate.TabIndex = 9;
            this.lblIssueDate.Text = "Issue Date";
            // 
            // chkAutoCompleationDocNo
            // 
            this.chkAutoCompleationDocNo.AutoSize = true;
            this.chkAutoCompleationDocNo.Location = new System.Drawing.Point(103, 18);
            this.chkAutoCompleationDocNo.Name = "chkAutoCompleationDocNo";
            this.chkAutoCompleationDocNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDocNo.TabIndex = 0;
            this.chkAutoCompleationDocNo.Tag = "1";
            this.chkAutoCompleationDocNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDocNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDocNo_CheckedChanged);
            // 
            // txtDocNo
            // 
            this.txtDocNo.Location = new System.Drawing.Point(124, 14);
            this.txtDocNo.Name = "txtDocNo";
            this.txtDocNo.Size = new System.Drawing.Size(136, 21);
            this.txtDocNo.TabIndex = 1;
            this.txtDocNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocNo_KeyDown);
            this.txtDocNo.Leave += new System.EventHandler(this.txtDocNo_Leave);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 18);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 1;
            this.lblDocumentNo.Text = "Document No";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoCompleationCardRange);
            this.groupBox1.Controls.Add(this.txtCardNoTo);
            this.groupBox1.Controls.Add(this.txtCardNoFrom);
            this.groupBox1.Controls.Add(this.dgvDisplay);
            this.groupBox1.Controls.Add(this.lblCardNoTo);
            this.groupBox1.Controls.Add(this.lblCardNoFrom);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Location = new System.Drawing.Point(2, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 344);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // chkAutoCompleationCardRange
            // 
            this.chkAutoCompleationCardRange.AutoSize = true;
            this.chkAutoCompleationCardRange.Location = new System.Drawing.Point(102, 15);
            this.chkAutoCompleationCardRange.Name = "chkAutoCompleationCardRange";
            this.chkAutoCompleationCardRange.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCardRange.TabIndex = 49;
            this.chkAutoCompleationCardRange.Tag = "1";
            this.chkAutoCompleationCardRange.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCardRange.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCardRange_CheckedChanged);
            // 
            // txtCardNoTo
            // 
            this.txtCardNoTo.Location = new System.Drawing.Point(326, 12);
            this.txtCardNoTo.Name = "txtCardNoTo";
            this.txtCardNoTo.Size = new System.Drawing.Size(174, 21);
            this.txtCardNoTo.TabIndex = 48;
            this.txtCardNoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNoTo_KeyDown);
            this.txtCardNoTo.Leave += new System.EventHandler(this.txtCardNoTo_Leave);
            // 
            // txtCardNoFrom
            // 
            this.txtCardNoFrom.Location = new System.Drawing.Point(124, 12);
            this.txtCardNoFrom.Name = "txtCardNoFrom";
            this.txtCardNoFrom.Size = new System.Drawing.Size(164, 21);
            this.txtCardNoFrom.TabIndex = 48;
            this.txtCardNoFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNoFrom_KeyDown);
            this.txtCardNoFrom.Leave += new System.EventHandler(this.txtCardNoFrom_Leave);
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.AllowUserToAddRows = false;
            this.dgvDisplay.AutoGenerateColumns = false;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cardNo,
            this.serialNo,
            this.encodeNo});
            this.dgvDisplay.DataSource = this.loyaltyCardGenerationDetailBindingSource;
            this.dgvDisplay.Location = new System.Drawing.Point(6, 40);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.RowHeadersWidth = 20;
            this.dgvDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDisplay.Size = new System.Drawing.Size(672, 298);
            this.dgvDisplay.TabIndex = 46;
            // 
            // cardNo
            // 
            this.cardNo.DataPropertyName = "CardNo";
            this.cardNo.HeaderText = "Card No";
            this.cardNo.Name = "cardNo";
            this.cardNo.Width = 200;
            // 
            // serialNo
            // 
            this.serialNo.DataPropertyName = "SerialNo";
            this.serialNo.HeaderText = "Serial No";
            this.serialNo.Name = "serialNo";
            this.serialNo.Width = 200;
            // 
            // encodeNo
            // 
            this.encodeNo.DataPropertyName = "EncodeNo";
            this.encodeNo.HeaderText = "Encode No";
            this.encodeNo.Name = "encodeNo";
            this.encodeNo.Width = 200;
            // 
            // loyaltyCardGenerationDetailBindingSource
            // 
            this.loyaltyCardGenerationDetailBindingSource.DataSource = typeof(Domain.LoyaltyCardGenerationDetail);
            // 
            // lblCardNoTo
            // 
            this.lblCardNoTo.AutoSize = true;
            this.lblCardNoTo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNoTo.Location = new System.Drawing.Point(295, 16);
            this.lblCardNoTo.Name = "lblCardNoTo";
            this.lblCardNoTo.Size = new System.Drawing.Size(20, 13);
            this.lblCardNoTo.TabIndex = 37;
            this.lblCardNoTo.Text = "To";
            // 
            // lblCardNoFrom
            // 
            this.lblCardNoFrom.AutoSize = true;
            this.lblCardNoFrom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNoFrom.Location = new System.Drawing.Point(6, 16);
            this.lblCardNoFrom.Name = "lblCardNoFrom";
            this.lblCardNoFrom.Size = new System.Drawing.Size(87, 13);
            this.lblCardNoFrom.TabIndex = 37;
            this.lblCardNoFrom.Text = "Card No From";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(502, 11);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(74, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(844, 443);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 39;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // FrmCardIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(690, 487);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkAutoClear);
            this.Name = "FrmCardIssue";
            this.Text = "Card Issue";
            this.Load += new System.EventHandler(this.FrmCardIssue_Load);
            this.Controls.SetChildIndex(this.chkAutoClear, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loyaltyCardGenerationDetailBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpIssueDate;
        private System.Windows.Forms.Button btnPoDetails;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationDocNo;
        private System.Windows.Forms.TextBox txtDocNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoad;
        private CustomControls.TextBoxMasterCode txtEmployeeCode;
        private CustomControls.TextBoxMasterDescription txtEmployeeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.CheckBox chkAutoCompleationCardRange;
        private System.Windows.Forms.TextBox txtCardNoTo;
        private System.Windows.Forms.TextBox txtCardNoFrom;
        private System.Windows.Forms.Label lblCardNoTo;
        private System.Windows.Forms.Label lblCardNoFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn encodeNo;
        private System.Windows.Forms.BindingSource loyaltyCardGenerationDetailBindingSource;
    }
}
