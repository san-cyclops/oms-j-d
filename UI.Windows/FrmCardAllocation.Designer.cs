namespace UI.Windows
{
    partial class FrmCardAllocation
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
            this.chkAutoCompleationSerialNo = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationCustomer = new System.Windows.Forms.CheckBox();
            this.dgvItemDetails = new System.Windows.Forms.DataGridView();
            this.CardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EncodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Assign = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.chkAutoCompleationSerialNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 338);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(884, 338);
            // 
            // chkAutoCompleationSerialNo
            // 
            this.chkAutoCompleationSerialNo.Controls.Add(this.checkBox1);
            this.chkAutoCompleationSerialNo.Controls.Add(this.chkAutoCompleationCustomer);
            this.chkAutoCompleationSerialNo.Controls.Add(this.dgvItemDetails);
            this.chkAutoCompleationSerialNo.Location = new System.Drawing.Point(2, -5);
            this.chkAutoCompleationSerialNo.Name = "chkAutoCompleationSerialNo";
            this.chkAutoCompleationSerialNo.Size = new System.Drawing.Size(1121, 348);
            this.chkAutoCompleationSerialNo.TabIndex = 10;
            this.chkAutoCompleationSerialNo.TabStop = false;
            this.chkAutoCompleationSerialNo.Tag = "1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(371, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Tag = "1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationCustomer
            // 
            this.chkAutoCompleationCustomer.AutoSize = true;
            this.chkAutoCompleationCustomer.Checked = true;
            this.chkAutoCompleationCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCustomer.Location = new System.Drawing.Point(181, 18);
            this.chkAutoCompleationCustomer.Name = "chkAutoCompleationCustomer";
            this.chkAutoCompleationCustomer.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCustomer.TabIndex = 30;
            this.chkAutoCompleationCustomer.Tag = "1";
            this.chkAutoCompleationCustomer.UseVisualStyleBackColor = true;
            // 
            // dgvItemDetails
            // 
            this.dgvItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardType,
            this.CardNo,
            this.SerialNo,
            this.EncodeNo,
            this.CustomerCode,
            this.CustomerName,
            this.Assign});
            this.dgvItemDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvItemDetails.Location = new System.Drawing.Point(3, 38);
            this.dgvItemDetails.Name = "dgvItemDetails";
            this.dgvItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemDetails.Size = new System.Drawing.Size(1112, 304);
            this.dgvItemDetails.TabIndex = 0;
            this.dgvItemDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItemDetails_EditingControlShowing);
            // 
            // CardType
            // 
            this.CardType.DataPropertyName = "CardType";
            this.CardType.HeaderText = "Card Type";
            this.CardType.Name = "CardType";
            this.CardType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CardType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CardType.Width = 150;
            // 
            // CardNo
            // 
            this.CardNo.DataPropertyName = "CardNo";
            this.CardNo.HeaderText = "Card No";
            this.CardNo.Name = "CardNo";
            this.CardNo.Width = 175;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "SerialNo";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.Width = 175;
            // 
            // EncodeNo
            // 
            this.EncodeNo.DataPropertyName = "EncodeNo";
            this.EncodeNo.HeaderText = "EncodeNo";
            this.EncodeNo.Name = "EncodeNo";
            this.EncodeNo.Width = 175;
            // 
            // CustomerCode
            // 
            this.CustomerCode.DataPropertyName = "CustomerCode";
            this.CustomerCode.HeaderText = "Customer Code";
            this.CustomerCode.Name = "CustomerCode";
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Width = 220;
            // 
            // Assign
            // 
            this.Assign.DataPropertyName = "Assign";
            this.Assign.HeaderText = "Assign";
            this.Assign.Name = "Assign";
            this.Assign.Width = 50;
            // 
            // FrmCardAllocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1125, 387);
            this.Controls.Add(this.chkAutoCompleationSerialNo);
            this.Name = "FrmCardAllocation";
            this.Text = "Card Allocation";
            this.Load += new System.EventHandler(this.FrmCardAllocation_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.chkAutoCompleationSerialNo, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.chkAutoCompleationSerialNo.ResumeLayout(false);
            this.chkAutoCompleationSerialNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox chkAutoCompleationSerialNo;
        private System.Windows.Forms.DataGridView dgvItemDetails;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkAutoCompleationCustomer;
        private System.Windows.Forms.DataGridViewComboBoxColumn CardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EncodeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Assign;
    }
}
