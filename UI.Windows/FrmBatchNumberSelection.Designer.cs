namespace UI.Windows
{
    partial class FrmBatchNumberSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchNumberSelection));
            this.dgvSearchDetails = new System.Windows.Forms.DataGridView();
            this.lblSerial = new System.Windows.Forms.Label();
            this.btnClose = new Glass.GlassButton();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.SelectSerial = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProductIdBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIDBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchDetails
            // 
            this.dgvSearchDetails.AllowUserToAddRows = false;
            this.dgvSearchDetails.AllowUserToDeleteRows = false;
            this.dgvSearchDetails.AllowUserToResizeColumns = false;
            this.dgvSearchDetails.AllowUserToResizeRows = false;
            this.dgvSearchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectSerial,
            this.ProductIdBatch,
            this.UOM,
            this.LocationIDBatch,
            this.BatchNO,
            this.Qty});
            this.dgvSearchDetails.Location = new System.Drawing.Point(8, 53);
            this.dgvSearchDetails.Name = "dgvSearchDetails";
            this.dgvSearchDetails.RowHeadersWidth = 5;
            this.dgvSearchDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchDetails.Size = new System.Drawing.Size(410, 282);
            this.dgvSearchDetails.TabIndex = 69;
            this.dgvSearchDetails.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchDetails_CellLeave);
            this.dgvSearchDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearchDetails_KeyDown);
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(12, 9);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(75, 16);
            this.lblSerial.TabIndex = 73;
            this.lblSerial.Text = "Batch List";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(331, 340);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            this.btnClose.TabIndex = 72;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(12, 60);
            this.txtSerialNo.MaxLength = 50;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(227, 21);
            this.txtSerialNo.TabIndex = 71;
            this.txtSerialNo.Visible = false;
            this.txtSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNo_KeyDown);
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotal.Location = new System.Drawing.Point(240, 60);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(50, 21);
            this.txtTotal.TabIndex = 70;
            this.txtTotal.Text = "1";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(10, 33);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(79, 17);
            this.chkSelectAll.TabIndex = 74;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // SelectSerial
            // 
            this.SelectSerial.DataPropertyName = "Selection";
            this.SelectSerial.HeaderText = "Select";
            this.SelectSerial.Name = "SelectSerial";
            this.SelectSerial.Width = 50;
            // 
            // ProductIdBatch
            // 
            this.ProductIdBatch.DataPropertyName = "ProductId";
            this.ProductIdBatch.HeaderText = "ProductId";
            this.ProductIdBatch.Name = "ProductIdBatch";
            this.ProductIdBatch.ReadOnly = true;
            this.ProductIdBatch.Visible = false;
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UnitOfMeasureID";
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 40;
            // 
            // LocationIDBatch
            // 
            this.LocationIDBatch.DataPropertyName = "LocationCode";
            this.LocationIDBatch.HeaderText = "Loca.";
            this.LocationIDBatch.Name = "LocationIDBatch";
            this.LocationIDBatch.ReadOnly = true;
            this.LocationIDBatch.Width = 60;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "Batch No";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.ReadOnly = true;
            this.BatchNO.Width = 180;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 50;
            // 
            // FrmBatchNumberSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(427, 377);
            this.ControlBox = false;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.dgvSearchDetails);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.txtTotal);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBatchNumberSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBatchNumberSelection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBatchNumberSelection_FormClosing);
            this.Load += new System.EventHandler(this.FrmBatchNumberSelection_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBatchNumberSelection_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchDetails;
        private System.Windows.Forms.Label lblSerial;
        public Glass.GlassButton btnClose;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductIdBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationIDBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;

    }
}