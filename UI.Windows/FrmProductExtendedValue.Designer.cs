namespace UI.Windows
{
    partial class FrmProductExtendedValue
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtExtendedValue = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblDataType = new System.Windows.Forms.Label();
            this.txtDataType = new System.Windows.Forms.TextBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.lblPropertyValue = new System.Windows.Forms.Label();
            this.dgvPropertyValue = new System.Windows.Forms.DataGridView();
            this.ValueData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invProductExtendedValueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chkAutoCompleationProperty = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtPropertyName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPropertyCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblPropertyCode = new System.Windows.Forms.Label();
            this.invProductExtendedValueID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedValueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 297);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(372, 297);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtExtendedValue);
            this.groupBox1.Controls.Add(this.lblDataType);
            this.groupBox1.Controls.Add(this.txtDataType);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.lblPropertyValue);
            this.groupBox1.Controls.Add(this.dgvPropertyValue);
            this.groupBox1.Controls.Add(this.chkAutoCompleationProperty);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtPropertyName);
            this.groupBox1.Controls.Add(this.txtPropertyCode);
            this.groupBox1.Controls.Add(this.lblPropertyCode);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(608, 308);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtExtendedValue
            // 
            this.txtExtendedValue.Location = new System.Drawing.Point(127, 225);
            this.txtExtendedValue.MasterDescription = "";
            this.txtExtendedValue.Name = "txtExtendedValue";
            this.txtExtendedValue.Size = new System.Drawing.Size(476, 21);
            this.txtExtendedValue.TabIndex = 37;
            this.txtExtendedValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExtendedValue_KeyDown);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataType.Location = new System.Drawing.Point(6, 49);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(65, 13);
            this.lblDataType.TabIndex = 36;
            this.lblDataType.Text = "Data Type";
            // 
            // txtDataType
            // 
            this.txtDataType.Location = new System.Drawing.Point(127, 45);
            this.txtDataType.Name = "txtDataType";
            this.txtDataType.ReadOnly = true;
            this.txtDataType.Size = new System.Drawing.Size(175, 21);
            this.txtDataType.TabIndex = 35;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(516, 290);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 33;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // lblPropertyValue
            // 
            this.lblPropertyValue.AutoSize = true;
            this.lblPropertyValue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropertyValue.Location = new System.Drawing.Point(6, 73);
            this.lblPropertyValue.Name = "lblPropertyValue";
            this.lblPropertyValue.Size = new System.Drawing.Size(98, 13);
            this.lblPropertyValue.TabIndex = 31;
            this.lblPropertyValue.Text = "Property Value*";
            // 
            // dgvPropertyValue
            // 
            this.dgvPropertyValue.AllowUserToAddRows = false;
            this.dgvPropertyValue.AutoGenerateColumns = false;
            this.dgvPropertyValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertyValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValueData});
            this.dgvPropertyValue.DataSource = this.invProductExtendedValueBindingSource;
            this.dgvPropertyValue.Location = new System.Drawing.Point(127, 73);
            this.dgvPropertyValue.Name = "dgvPropertyValue";
            this.dgvPropertyValue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPropertyValue.Size = new System.Drawing.Size(476, 149);
            this.dgvPropertyValue.TabIndex = 3;
            this.dgvPropertyValue.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertyValue_RowValidated);
            this.dgvPropertyValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPropertyValue_KeyDown);
            // 
            // ValueData
            // 
            this.ValueData.DataPropertyName = "ValueData";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ValueData.DefaultCellStyle = dataGridViewCellStyle1;
            this.ValueData.HeaderText = "Property Value";
            this.ValueData.Name = "ValueData";
            this.ValueData.ReadOnly = true;
            this.ValueData.Width = 250;
            // 
            // invProductExtendedValueBindingSource
            // 
            this.invProductExtendedValueBindingSource.DataSource = typeof(Domain.InvProductExtendedValue);
            // 
            // chkAutoCompleationProperty
            // 
            this.chkAutoCompleationProperty.AutoSize = true;
            this.chkAutoCompleationProperty.Checked = true;
            this.chkAutoCompleationProperty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationProperty.Location = new System.Drawing.Point(109, 26);
            this.chkAutoCompleationProperty.Name = "chkAutoCompleationProperty";
            this.chkAutoCompleationProperty.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationProperty.TabIndex = 29;
            this.chkAutoCompleationProperty.Tag = "1";
            this.chkAutoCompleationProperty.UseVisualStyleBackColor = true;
            this.chkAutoCompleationProperty.CheckedChanged += new System.EventHandler(this.chkAutoCompleationProperty_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(127, 248);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(476, 37);
            this.txtRemark.TabIndex = 14;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 263);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(304, 23);
            this.txtPropertyName.MasterDescription = "";
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(299, 21);
            this.txtPropertyName.TabIndex = 2;
            this.txtPropertyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyName_KeyDown);
            this.txtPropertyName.Leave += new System.EventHandler(this.txtPropertyName_Leave);
            // 
            // txtPropertyCode
            // 
            this.txtPropertyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPropertyCode.IsAutoComplete = false;
            this.txtPropertyCode.ItemCollection = null;
            this.txtPropertyCode.Location = new System.Drawing.Point(127, 23);
            this.txtPropertyCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPropertyCode.MasterCode = "";
            this.txtPropertyCode.MaxLength = 25;
            this.txtPropertyCode.Name = "txtPropertyCode";
            this.txtPropertyCode.Size = new System.Drawing.Size(175, 21);
            this.txtPropertyCode.TabIndex = 0;
            this.txtPropertyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPropertyCode_KeyDown);
            this.txtPropertyCode.Leave += new System.EventHandler(this.txtPropertyCode_Leave);
            // 
            // lblPropertyCode
            // 
            this.lblPropertyCode.AutoSize = true;
            this.lblPropertyCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropertyCode.Location = new System.Drawing.Point(6, 26);
            this.lblPropertyCode.Name = "lblPropertyCode";
            this.lblPropertyCode.Size = new System.Drawing.Size(97, 13);
            this.lblPropertyCode.TabIndex = 9;
            this.lblPropertyCode.Text = "Property Code*";
            // 
            // invProductExtendedValueID
            // 
            this.invProductExtendedValueID.DataPropertyName = "InvProductExtendedValueID";
            this.invProductExtendedValueID.HeaderText = "InvProductExtendedValueID";
            this.invProductExtendedValueID.Name = "invProductExtendedValueID";
            // 
            // FrmProductExtendedValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(613, 346);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmProductExtendedValue";
            this.Text = "Extended Values";
            this.Load += new System.EventHandler(this.FrmProductExtendedValues_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invProductExtendedValueBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoCompleationProperty;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtPropertyName;
        private CustomControls.TextBoxMasterCode txtPropertyCode;
        private System.Windows.Forms.Label lblPropertyCode;
        private System.Windows.Forms.DataGridView dgvPropertyValue;
        private System.Windows.Forms.Label lblPropertyValue;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentValueData;
        private System.Windows.Forms.DataGridViewTextBoxColumn invProductExtendedValueID;
        private System.Windows.Forms.DataGridViewTextBoxColumn productExtendedValueCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invExtendedPropertyIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invProductExtendedPropertyDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource invProductExtendedValueBindingSource;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.TextBox txtDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueData;
        private CustomControls.TextBoxMasterDescription txtExtendedValue;
    }
}
