namespace UI.Windows
{
    partial class FrmCashierGroup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCashier = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Access = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUpdateAllCashiers = new System.Windows.Forms.CheckBox();
            this.grpLocation = new System.Windows.Forms.GroupBox();
            this.txtFunction = new System.Windows.Forms.TextBox();
            this.chkUpdateFunction = new System.Windows.Forms.CheckBox();
            this.dgvLocationInfo = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashier)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 385);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(679, 385);
            this.grpButtonSet2.Size = new System.Drawing.Size(257, 46);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(91, 11);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 11);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(176, 11);
            // 
            // dgvCashier
            // 
            this.dgvCashier.AllowUserToDeleteRows = false;
            this.dgvCashier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.FunctionName,
            this.FunctionDescription,
            this.Access,
            this.Value,
            this.IsValue});
            this.dgvCashier.Location = new System.Drawing.Point(4, 31);
            this.dgvCashier.Name = "dgvCashier";
            this.dgvCashier.RowHeadersWidth = 10;
            this.dgvCashier.Size = new System.Drawing.Size(665, 316);
            this.dgvCashier.TabIndex = 46;
            this.dgvCashier.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCashier_CellContentClick);
            this.dgvCashier.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCashier_CellFormatting);
            this.dgvCashier.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCashier_CellMouseClick);
            this.dgvCashier.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvCashier_RowPrePaint);
            this.dgvCashier.DoubleClick += new System.EventHandler(this.dgvCashier_DoubleClick);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "Order";
            this.RowNo.HeaderText = "Row No";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 80;
            // 
            // FunctionName
            // 
            this.FunctionName.DataPropertyName = "FunctionName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionName.DefaultCellStyle = dataGridViewCellStyle4;
            this.FunctionName.HeaderText = "FunctionName";
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.ReadOnly = true;
            this.FunctionName.Visible = false;
            this.FunctionName.Width = 255;
            // 
            // FunctionDescription
            // 
            this.FunctionDescription.DataPropertyName = "FunctionDescription";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FunctionDescription.DefaultCellStyle = dataGridViewCellStyle5;
            this.FunctionDescription.HeaderText = "Function Description";
            this.FunctionDescription.Name = "FunctionDescription";
            this.FunctionDescription.ReadOnly = true;
            this.FunctionDescription.Width = 400;
            // 
            // Access
            // 
            this.Access.DataPropertyName = "IsAccess";
            this.Access.HeaderText = "Access";
            this.Access.Name = "Access";
            this.Access.Width = 50;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Value.DefaultCellStyle = dataGridViewCellStyle6;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // IsValue
            // 
            this.IsValue.DataPropertyName = "IsValue";
            this.IsValue.HeaderText = "IsValue";
            this.IsValue.Name = "IsValue";
            this.IsValue.ReadOnly = true;
            this.IsValue.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSelectAll);
            this.groupBox1.Controls.Add(this.dgvCashier);
            this.groupBox1.Location = new System.Drawing.Point(2, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 353);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(513, 11);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(79, 17);
            this.chkSelectAll.TabIndex = 47;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(102, 17);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(313, 21);
            this.cmbGroup.TabIndex = 52;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(6, 20);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(90, 13);
            this.lblGroup.TabIndex = 53;
            this.lblGroup.Text = "Cashier Group";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUpdateAllCashiers);
            this.groupBox2.Controls.Add(this.cmbGroup);
            this.groupBox2.Controls.Add(this.lblGroup);
            this.groupBox2.Location = new System.Drawing.Point(2, -6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 48);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            // 
            // chkUpdateAllCashiers
            // 
            this.chkUpdateAllCashiers.AutoSize = true;
            this.chkUpdateAllCashiers.Location = new System.Drawing.Point(421, 20);
            this.chkUpdateAllCashiers.Name = "chkUpdateAllCashiers";
            this.chkUpdateAllCashiers.Size = new System.Drawing.Size(247, 17);
            this.chkUpdateAllCashiers.TabIndex = 54;
            this.chkUpdateAllCashiers.Text = "Update All Cashiers In Relevent Group";
            this.chkUpdateAllCashiers.UseVisualStyleBackColor = true;
            this.chkUpdateAllCashiers.CheckedChanged += new System.EventHandler(this.chkUpdateAllCashiers_CheckedChanged);
            // 
            // grpLocation
            // 
            this.grpLocation.Controls.Add(this.txtFunction);
            this.grpLocation.Controls.Add(this.chkUpdateFunction);
            this.grpLocation.Controls.Add(this.dgvLocationInfo);
            this.grpLocation.Location = new System.Drawing.Point(679, -6);
            this.grpLocation.Name = "grpLocation";
            this.grpLocation.Size = new System.Drawing.Size(257, 396);
            this.grpLocation.TabIndex = 55;
            this.grpLocation.TabStop = false;
            // 
            // txtFunction
            // 
            this.txtFunction.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtFunction.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFunction.Location = new System.Drawing.Point(4, 11);
            this.txtFunction.Multiline = true;
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.ReadOnly = true;
            this.txtFunction.Size = new System.Drawing.Size(248, 27);
            this.txtFunction.TabIndex = 5;
            // 
            // chkUpdateFunction
            // 
            this.chkUpdateFunction.AutoSize = true;
            this.chkUpdateFunction.Location = new System.Drawing.Point(6, 40);
            this.chkUpdateFunction.Name = "chkUpdateFunction";
            this.chkUpdateFunction.Size = new System.Drawing.Size(48, 17);
            this.chkUpdateFunction.TabIndex = 4;
            this.chkUpdateFunction.Text = "text";
            this.chkUpdateFunction.UseVisualStyleBackColor = true;
            // 
            // dgvLocationInfo
            // 
            this.dgvLocationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.Location,
            this.LocationId});
            this.dgvLocationInfo.Location = new System.Drawing.Point(3, 74);
            this.dgvLocationInfo.Name = "dgvLocationInfo";
            this.dgvLocationInfo.RowHeadersWidth = 5;
            this.dgvLocationInfo.Size = new System.Drawing.Size(248, 315);
            this.dgvLocationInfo.TabIndex = 3;
            this.dgvLocationInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocationInfo_CellContentClick);
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
            // FrmCashierGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(938, 434);
            this.Controls.Add(this.grpLocation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCashierGroup";
            this.Text = "Cashier Group";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.grpLocation, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashier)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpLocation.ResumeLayout(false);
            this.grpLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCashier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUpdateAllCashiers;
        private System.Windows.Forms.GroupBox grpLocation;
        private System.Windows.Forms.CheckBox chkUpdateFunction;
        private System.Windows.Forms.DataGridView dgvLocationInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationId;
        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Access;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsValue;
    }
}
