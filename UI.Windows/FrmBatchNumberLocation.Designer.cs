namespace Sarasa.ERP.UI.Windows
{
    partial class FrmBatchNumberLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchNumberLocation));
            this.dgvSearchDetails = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClose = new Glass.GlassButton();
            this.txtLocatin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchDetails
            // 
            this.dgvSearchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.LocationIdx,
            this.BatchNo});
            this.dgvSearchDetails.Location = new System.Drawing.Point(10, 84);
            this.dgvSearchDetails.Name = "dgvSearchDetails";
            this.dgvSearchDetails.RowHeadersWidth = 5;
            this.dgvSearchDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchDetails.Size = new System.Drawing.Size(297, 261);
            this.dgvSearchDetails.TabIndex = 77;
            this.dgvSearchDetails.DoubleClick += new System.EventHandler(this.dgvSearchDetails_DoubleClick);
            this.dgvSearchDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearchDetails_KeyDown);
            // 
            // Row
            // 
            this.Row.DataPropertyName = "LineNo";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.Row.DefaultCellStyle = dataGridViewCellStyle1;
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 50;
            // 
            // LocationIdx
            // 
            this.LocationIdx.DataPropertyName = "LocationID";
            this.LocationIdx.HeaderText = "Loca";
            this.LocationIdx.Name = "LocationIdx";
            this.LocationIdx.Width = 40;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 200;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(10, 10);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(140, 16);
            this.lblSerial.TabIndex = 78;
            this.lblSerial.Text = "Purchase Order No";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(10, 60);
            this.txtBatchNo.MaxLength = 50;
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(277, 20);
            this.txtBatchNo.TabIndex = 76;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotal.Location = new System.Drawing.Point(237, 36);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(50, 20);
            this.txtTotal.TabIndex = 75;
            this.txtTotal.Text = "1";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(221, 349);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 32);
            this.btnClose.TabIndex = 78;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtLocatin
            // 
            this.txtLocatin.Location = new System.Drawing.Point(30, 36);
            this.txtLocatin.MaxLength = 50;
            this.txtLocatin.Name = "txtLocatin";
            this.txtLocatin.Size = new System.Drawing.Size(133, 20);
            this.txtLocatin.TabIndex = 79;
            this.txtLocatin.Visible = false;
            // 
            // FrmBatchNumberLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 391);
            this.ControlBox = false;
            this.Controls.Add(this.txtLocatin);
            this.Controls.Add(this.dgvSearchDetails);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.txtTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBatchNumberLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBatchNumberLocation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBatchNumberLocation_FormClosing);
            this.Load += new System.EventHandler(this.FrmBatchNumberLocation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBatchNumberLocation_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchDetails;
        private System.Windows.Forms.Label lblSerial;
        public Glass.GlassButton btnClose;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.TextBox txtLocatin;
    }
}