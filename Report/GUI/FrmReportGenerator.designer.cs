namespace Report
{
    partial class FrmReprotGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReprotGenerator));
            this.gbFieldSelection = new System.Windows.Forms.GroupBox();
            this.chkLstFieldSelectionStr = new System.Windows.Forms.CheckedListBox();
            this.chkLstFieldSelectionDes = new System.Windows.Forms.CheckedListBox();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtValueFrom = new Report.GUI.CustomControls.TextBoxNumericMinus();
            this.txtValueTo = new UI.Windows.CustomControls.TextBoxNumeric();
            this.dgvValueRange = new System.Windows.Forms.DataGridView();
            this.ValueType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblValue = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.cmbValueTo = new System.Windows.Forms.ComboBox();
            this.cmbValueFrom = new System.Windows.Forms.ComboBox();
            this.btnValueAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbValueType = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gbRowTotal = new System.Windows.Forms.GroupBox();
            this.chkRowTotal = new System.Windows.Forms.CheckedListBox();
            this.gbColumnTotal = new System.Windows.Forms.GroupBox();
            this.chklstColumnTotal = new System.Windows.Forms.CheckedListBox();
            this.gbGroupBy = new System.Windows.Forms.GroupBox();
            this.btnGroupByDown = new System.Windows.Forms.Button();
            this.btnGroupByUp = new System.Windows.Forms.Button();
            this.chkViewGroupDetails = new System.Windows.Forms.CheckBox();
            this.chkLstGroupBy = new System.Windows.Forms.CheckedListBox();
            this.gbOrderBy = new System.Windows.Forms.GroupBox();
            this.chkLstOrderBy = new System.Windows.Forms.CheckedListBox();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gbFieldSelection.SuspendLayout();
            this.gbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValueRange)).BeginInit();
            this.gbRowTotal.SuspendLayout();
            this.gbColumnTotal.SuspendLayout();
            this.gbGroupBy.SuspendLayout();
            this.gbOrderBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(872, 510);
            this.grpButtonSet2.Size = new System.Drawing.Size(237, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(3, 510);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // gbFieldSelection
            // 
            this.gbFieldSelection.Controls.Add(this.chkLstFieldSelectionStr);
            this.gbFieldSelection.Controls.Add(this.chkLstFieldSelectionDes);
            this.gbFieldSelection.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFieldSelection.Location = new System.Drawing.Point(2, 175);
            this.gbFieldSelection.Name = "gbFieldSelection";
            this.gbFieldSelection.Size = new System.Drawing.Size(636, 158);
            this.gbFieldSelection.TabIndex = 19;
            this.gbFieldSelection.TabStop = false;
            this.gbFieldSelection.Text = "Field Selection";
            // 
            // chkLstFieldSelectionStr
            // 
            this.chkLstFieldSelectionStr.CheckOnClick = true;
            this.chkLstFieldSelectionStr.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstFieldSelectionStr.FormattingEnabled = true;
            this.chkLstFieldSelectionStr.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07"});
            this.chkLstFieldSelectionStr.Location = new System.Drawing.Point(17, 21);
            this.chkLstFieldSelectionStr.MultiColumn = true;
            this.chkLstFieldSelectionStr.Name = "chkLstFieldSelectionStr";
            this.chkLstFieldSelectionStr.Size = new System.Drawing.Size(449, 132);
            this.chkLstFieldSelectionStr.TabIndex = 13;
            this.chkLstFieldSelectionStr.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionStr_ItemCheck);
            // 
            // chkLstFieldSelectionDes
            // 
            this.chkLstFieldSelectionDes.CheckOnClick = true;
            this.chkLstFieldSelectionDes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstFieldSelectionDes.FormattingEnabled = true;
            this.chkLstFieldSelectionDes.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07"});
            this.chkLstFieldSelectionDes.Location = new System.Drawing.Point(470, 21);
            this.chkLstFieldSelectionDes.MultiColumn = true;
            this.chkLstFieldSelectionDes.Name = "chkLstFieldSelectionDes";
            this.chkLstFieldSelectionDes.Size = new System.Drawing.Size(159, 132);
            this.chkLstFieldSelectionDes.TabIndex = 14;
            this.chkLstFieldSelectionDes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstFieldSelectionDes_ItemCheck);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.dgvResult);
            this.gbResult.Location = new System.Drawing.Point(3, 328);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(1105, 187);
            this.gbResult.TabIndex = 25;
            this.gbResult.TabStop = false;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(4, 12);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(1097, 169);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.DoubleClick += new System.EventHandler(this.dgvResult_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtValueFrom);
            this.groupBox1.Controls.Add(this.txtValueTo);
            this.groupBox1.Controls.Add(this.dgvValueRange);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.lblValue);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.cmbValueTo);
            this.groupBox1.Controls.Add(this.cmbValueFrom);
            this.groupBox1.Controls.Add(this.btnValueAdd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbValueType);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 170);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Value Range";
            // 
            // txtValueFrom
            // 
            this.txtValueFrom.Location = new System.Drawing.Point(110, 46);
            this.txtValueFrom.Name = "txtValueFrom";
            this.txtValueFrom.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtValueFrom.Size = new System.Drawing.Size(240, 21);
            this.txtValueFrom.TabIndex = 34;
            // 
            // txtValueTo
            // 
            this.txtValueTo.Location = new System.Drawing.Point(353, 46);
            this.txtValueTo.Name = "txtValueTo";
            this.txtValueTo.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtValueTo.Size = new System.Drawing.Size(240, 21);
            this.txtValueTo.TabIndex = 29;
            this.txtValueTo.Visible = false;
            // 
            // dgvValueRange
            // 
            this.dgvValueRange.AllowDrop = true;
            this.dgvValueRange.AllowUserToAddRows = false;
            this.dgvValueRange.AllowUserToDeleteRows = false;
            this.dgvValueRange.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvValueRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValueRange.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValueType,
            this.ValueFrom,
            this.ValueTo,
            this.ValueTypeId});
            this.dgvValueRange.Location = new System.Drawing.Point(14, 73);
            this.dgvValueRange.Name = "dgvValueRange";
            this.dgvValueRange.ReadOnly = true;
            this.dgvValueRange.RowHeadersVisible = false;
            this.dgvValueRange.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvValueRange.Size = new System.Drawing.Size(615, 92);
            this.dgvValueRange.TabIndex = 21;
            this.dgvValueRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvValueRange_KeyDown);
            // 
            // ValueType
            // 
            this.ValueType.HeaderText = "Value Type";
            this.ValueType.Name = "ValueType";
            this.ValueType.ReadOnly = true;
            this.ValueType.Width = 140;
            // 
            // ValueFrom
            // 
            this.ValueFrom.HeaderText = "Value From";
            this.ValueFrom.Name = "ValueFrom";
            this.ValueFrom.ReadOnly = true;
            this.ValueFrom.Width = 225;
            // 
            // ValueTo
            // 
            this.ValueTo.HeaderText = "Value To";
            this.ValueTo.Name = "ValueTo";
            this.ValueTo.ReadOnly = true;
            this.ValueTo.Width = 225;
            // 
            // ValueTypeId
            // 
            this.ValueTypeId.HeaderText = "ValueTypeId";
            this.ValueTypeId.Name = "ValueTypeId";
            this.ValueTypeId.ReadOnly = true;
            this.ValueTypeId.Visible = false;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(110, 46);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(240, 21);
            this.dtpDateFrom.TabIndex = 31;
            this.dtpDateFrom.Visible = false;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(15, 49);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(91, 13);
            this.lblValue.TabIndex = 28;
            this.lblValue.Text = "Value Between";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(353, 46);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(240, 21);
            this.dtpDateTo.TabIndex = 32;
            this.dtpDateTo.Visible = false;
            // 
            // cmbValueTo
            // 
            this.cmbValueTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbValueTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbValueTo.FormattingEnabled = true;
            this.cmbValueTo.Location = new System.Drawing.Point(353, 46);
            this.cmbValueTo.Name = "cmbValueTo";
            this.cmbValueTo.Size = new System.Drawing.Size(240, 21);
            this.cmbValueTo.TabIndex = 27;
            this.cmbValueTo.Leave += new System.EventHandler(this.cmbValueTo_Leave);
            // 
            // cmbValueFrom
            // 
            this.cmbValueFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbValueFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbValueFrom.FormattingEnabled = true;
            this.cmbValueFrom.Location = new System.Drawing.Point(110, 46);
            this.cmbValueFrom.Name = "cmbValueFrom";
            this.cmbValueFrom.Size = new System.Drawing.Size(240, 21);
            this.cmbValueFrom.TabIndex = 26;
            this.cmbValueFrom.Leave += new System.EventHandler(this.cmbValueFrom_Leave);
            // 
            // btnValueAdd
            // 
            this.btnValueAdd.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValueAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnValueAdd.Image")));
            this.btnValueAdd.Location = new System.Drawing.Point(595, 45);
            this.btnValueAdd.Name = "btnValueAdd";
            this.btnValueAdd.Size = new System.Drawing.Size(35, 23);
            this.btnValueAdd.TabIndex = 9;
            this.btnValueAdd.UseVisualStyleBackColor = true;
            this.btnValueAdd.Click += new System.EventHandler(this.btnValueAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Value Type";
            // 
            // cmbValueType
            // 
            this.cmbValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValueType.FormattingEnabled = true;
            this.cmbValueType.Location = new System.Drawing.Point(110, 21);
            this.cmbValueType.Name = "cmbValueType";
            this.cmbValueType.Size = new System.Drawing.Size(240, 21);
            this.cmbValueType.TabIndex = 4;
            this.cmbValueType.SelectedIndexChanged += new System.EventHandler(this.cmbValueType_SelectedIndexChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(110, 46);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(240, 21);
            this.txtValue.TabIndex = 33;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(14, 123);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(450, 32);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "L o a d";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // gbRowTotal
            // 
            this.gbRowTotal.Controls.Add(this.chkRowTotal);
            this.gbRowTotal.Location = new System.Drawing.Point(1166, 254);
            this.gbRowTotal.Name = "gbRowTotal";
            this.gbRowTotal.Size = new System.Drawing.Size(81, 207);
            this.gbRowTotal.TabIndex = 34;
            this.gbRowTotal.TabStop = false;
            this.gbRowTotal.Text = "Row Total";
            this.gbRowTotal.Visible = false;
            // 
            // chkRowTotal
            // 
            this.chkRowTotal.CheckOnClick = true;
            this.chkRowTotal.FormattingEnabled = true;
            this.chkRowTotal.HorizontalScrollbar = true;
            this.chkRowTotal.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.chkRowTotal.Location = new System.Drawing.Point(12, 17);
            this.chkRowTotal.MultiColumn = true;
            this.chkRowTotal.Name = "chkRowTotal";
            this.chkRowTotal.Size = new System.Drawing.Size(125, 180);
            this.chkRowTotal.TabIndex = 13;
            this.chkRowTotal.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkRowTotal_ItemCheck);
            // 
            // gbColumnTotal
            // 
            this.gbColumnTotal.Controls.Add(this.chklstColumnTotal);
            this.gbColumnTotal.Location = new System.Drawing.Point(1166, 22);
            this.gbColumnTotal.Name = "gbColumnTotal";
            this.gbColumnTotal.Size = new System.Drawing.Size(139, 207);
            this.gbColumnTotal.TabIndex = 33;
            this.gbColumnTotal.TabStop = false;
            this.gbColumnTotal.Text = "Column Total";
            this.gbColumnTotal.Visible = false;
            // 
            // chklstColumnTotal
            // 
            this.chklstColumnTotal.CheckOnClick = true;
            this.chklstColumnTotal.FormattingEnabled = true;
            this.chklstColumnTotal.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.chklstColumnTotal.Location = new System.Drawing.Point(8, 16);
            this.chklstColumnTotal.MultiColumn = true;
            this.chklstColumnTotal.Name = "chklstColumnTotal";
            this.chklstColumnTotal.Size = new System.Drawing.Size(125, 180);
            this.chklstColumnTotal.TabIndex = 13;
            this.chklstColumnTotal.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklstColumnTotal_ItemCheck);
            // 
            // gbGroupBy
            // 
            this.gbGroupBy.Controls.Add(this.btnGroupByDown);
            this.gbGroupBy.Controls.Add(this.btnGroupByUp);
            this.gbGroupBy.Controls.Add(this.chkViewGroupDetails);
            this.gbGroupBy.Controls.Add(this.chkLstGroupBy);
            this.gbGroupBy.Location = new System.Drawing.Point(641, 2);
            this.gbGroupBy.Name = "gbGroupBy";
            this.gbGroupBy.Size = new System.Drawing.Size(467, 170);
            this.gbGroupBy.TabIndex = 31;
            this.gbGroupBy.TabStop = false;
            this.gbGroupBy.Text = "Group By ";
            // 
            // btnGroupByDown
            // 
            this.btnGroupByDown.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupByDown.Location = new System.Drawing.Point(440, 53);
            this.btnGroupByDown.Name = "btnGroupByDown";
            this.btnGroupByDown.Size = new System.Drawing.Size(23, 35);
            this.btnGroupByDown.TabIndex = 14;
            this.btnGroupByDown.Text = "˅";
            this.btnGroupByDown.UseVisualStyleBackColor = true;
            this.btnGroupByDown.Click += new System.EventHandler(this.btnGroupByDown_Click);
            // 
            // btnGroupByUp
            // 
            this.btnGroupByUp.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupByUp.Location = new System.Drawing.Point(440, 16);
            this.btnGroupByUp.Name = "btnGroupByUp";
            this.btnGroupByUp.Size = new System.Drawing.Size(23, 35);
            this.btnGroupByUp.TabIndex = 14;
            this.btnGroupByUp.Text = "˄";
            this.btnGroupByUp.UseVisualStyleBackColor = true;
            this.btnGroupByUp.Click += new System.EventHandler(this.btnGroupByUp_Click);
            // 
            // chkViewGroupDetails
            // 
            this.chkViewGroupDetails.AutoSize = true;
            this.chkViewGroupDetails.Location = new System.Drawing.Point(82, 0);
            this.chkViewGroupDetails.Name = "chkViewGroupDetails";
            this.chkViewGroupDetails.Size = new System.Drawing.Size(135, 17);
            this.chkViewGroupDetails.TabIndex = 13;
            this.chkViewGroupDetails.Text = "View Group Details";
            this.chkViewGroupDetails.UseVisualStyleBackColor = true;
            this.chkViewGroupDetails.Visible = false;
            this.chkViewGroupDetails.CheckedChanged += new System.EventHandler(this.chkViewGroupDetails_CheckedChanged);
            // 
            // chkLstGroupBy
            // 
            this.chkLstGroupBy.CheckOnClick = true;
            this.chkLstGroupBy.FormattingEnabled = true;
            this.chkLstGroupBy.Location = new System.Drawing.Point(15, 17);
            this.chkLstGroupBy.MultiColumn = true;
            this.chkLstGroupBy.Name = "chkLstGroupBy";
            this.chkLstGroupBy.Size = new System.Drawing.Size(419, 148);
            this.chkLstGroupBy.TabIndex = 12;
            this.chkLstGroupBy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstGroupBy_ItemCheck);
            this.chkLstGroupBy.Leave += new System.EventHandler(this.chkLstGroupBy_Leave);
            // 
            // gbOrderBy
            // 
            this.gbOrderBy.Controls.Add(this.btnLoad);
            this.gbOrderBy.Controls.Add(this.chkLstOrderBy);
            this.gbOrderBy.Location = new System.Drawing.Point(641, 175);
            this.gbOrderBy.Name = "gbOrderBy";
            this.gbOrderBy.Size = new System.Drawing.Size(468, 158);
            this.gbOrderBy.TabIndex = 32;
            this.gbOrderBy.TabStop = false;
            this.gbOrderBy.Text = "Order By";
            // 
            // chkLstOrderBy
            // 
            this.chkLstOrderBy.CheckOnClick = true;
            this.chkLstOrderBy.FormattingEnabled = true;
            this.chkLstOrderBy.Location = new System.Drawing.Point(15, 21);
            this.chkLstOrderBy.MultiColumn = true;
            this.chkLstOrderBy.Name = "chkLstOrderBy";
            this.chkLstOrderBy.Size = new System.Drawing.Size(448, 100);
            this.chkLstOrderBy.TabIndex = 13;
            this.chkLstOrderBy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstOrderBy_ItemCheck);
            // 
            // FrmReprotGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 557);
            this.Controls.Add(this.gbOrderBy);
            this.Controls.Add(this.gbFieldSelection);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbGroupBy);
            this.Controls.Add(this.gbColumnTotal);
            this.Controls.Add(this.gbRowTotal);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmReprotGenerator";
            this.Text = "Report Generator";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.gbRowTotal, 0);
            this.Controls.SetChildIndex(this.gbColumnTotal, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.gbGroupBy, 0);
            this.Controls.SetChildIndex(this.gbResult, 0);
            this.Controls.SetChildIndex(this.gbFieldSelection, 0);
            this.Controls.SetChildIndex(this.gbOrderBy, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gbFieldSelection.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValueRange)).EndInit();
            this.gbRowTotal.ResumeLayout(false);
            this.gbColumnTotal.ResumeLayout(false);
            this.gbGroupBy.ResumeLayout(false);
            this.gbGroupBy.PerformLayout();
            this.gbOrderBy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.Button btnValueAdd;
        //private System.Windows.Forms.ComboBox cmbValueType;
        private System.Windows.Forms.GroupBox gbFieldSelection;
        private System.Windows.Forms.CheckedListBox chkLstFieldSelectionDes;
        private System.Windows.Forms.CheckedListBox chkLstFieldSelectionStr;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoad;
        private UI.Windows.CustomControls.TextBoxNumeric txtValueTo;
        private System.Windows.Forms.DataGridView dgvValueRange;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.ComboBox cmbValueTo;
        private System.Windows.Forms.ComboBox cmbValueFrom;
        private System.Windows.Forms.Button btnValueAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbValueType;
        private System.Windows.Forms.GroupBox gbRowTotal;
        private System.Windows.Forms.CheckedListBox chkRowTotal;
        private System.Windows.Forms.GroupBox gbColumnTotal;
        private System.Windows.Forms.CheckedListBox chklstColumnTotal;
        private System.Windows.Forms.GroupBox gbGroupBy;
        private System.Windows.Forms.CheckedListBox chkLstGroupBy;
        private System.Windows.Forms.GroupBox gbOrderBy;
        private System.Windows.Forms.CheckedListBox chkLstOrderBy;
        private System.Windows.Forms.CheckBox chkViewGroupDetails;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGroupByDown;
        private System.Windows.Forms.Button btnGroupByUp;
        private GUI.CustomControls.TextBoxNumericMinus txtValueFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueTypeId;
        //private UI.Windows.CustomControls.TextBoxNumeric txtValueTo;
        //private UI.Windows.CustomControls.TextBoxNumeric txtValueFrom;
        //private System.Windows.Forms.DateTimePicker dtpDateTo;
        //private System.Windows.Forms.DateTimePicker dtpDateFrom;
        //private System.Windows.Forms.ComboBox cmbValueTo;
        //private System.Windows.Forms.ComboBox cmbValueFrom;

    }
}