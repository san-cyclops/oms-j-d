namespace UI.Windows
{
    partial class FrmExpiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpiry));
            this.dgvSearchDetails = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClose = new Glass.GlassButton();
            this.dtpExpiary = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchDetails
            // 
            this.dgvSearchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.Expiary});
            this.dgvSearchDetails.Location = new System.Drawing.Point(10, 89);
            this.dgvSearchDetails.Name = "dgvSearchDetails";
            this.dgvSearchDetails.RowHeadersWidth = 5;
            this.dgvSearchDetails.Size = new System.Drawing.Size(277, 256);
            this.dgvSearchDetails.TabIndex = 85;
            this.dgvSearchDetails.DoubleClick += new System.EventHandler(this.dgvSearchDetails_DoubleClick);
            this.dgvSearchDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearchDetails_KeyDown);
            // 
            // Row
            // 
            this.Row.DataPropertyName = "LineNo";
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.Width = 50;
            // 
            // Expiary
            // 
            this.Expiary.DataPropertyName = "ExpiryDate";
            this.Expiary.HeaderText = "Expiry";
            this.Expiary.Name = "Expiary";
            this.Expiary.ReadOnly = true;
            this.Expiary.Width = 200;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(10, 10);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(135, 19);
            this.lblSerial.TabIndex = 83;
            this.lblSerial.Text = "Purchase Order No";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(238, 60);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(50, 23);
            this.txtTotal.TabIndex = 80;
            this.txtTotal.Text = "1";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(200, 349);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            this.btnClose.TabIndex = 82;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpExpiary
            // 
            this.dtpExpiary.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpiary.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiary.Location = new System.Drawing.Point(10, 60);
            this.dtpExpiary.Name = "dtpExpiary";
            this.dtpExpiary.Size = new System.Drawing.Size(222, 23);
            this.dtpExpiary.TabIndex = 84;
            this.dtpExpiary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiary_KeyDown);
            // 
            // FrmExpiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 391);
            this.ControlBox = false;
            this.Controls.Add(this.dtpExpiary);
            this.Controls.Add(this.dgvSearchDetails);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmExpiry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmExpiary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmExpiary_FormClosing);
            this.Load += new System.EventHandler(this.FrmExpiary_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExpiary_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchDetails;
        private System.Windows.Forms.Label lblSerial;
        public Glass.GlassButton btnClose;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DateTimePicker dtpExpiary;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiary;
    }
}