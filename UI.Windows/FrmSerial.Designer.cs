namespace UI.Windows
{
    partial class FrmSerial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSerial));
            this.dgvSerialNo = new System.Windows.Forms.DataGridView();
            this.RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.btnClose = new Glass.GlassButton();
            this.lblSerial = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSerialNo
            // 
            this.dgvSerialNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerialNo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNo,
            this.SerialNo});
            this.dgvSerialNo.Location = new System.Drawing.Point(14, 85);
            this.dgvSerialNo.Name = "dgvSerialNo";
            this.dgvSerialNo.RowHeadersWidth = 15;
            this.dgvSerialNo.Size = new System.Drawing.Size(303, 326);
            this.dgvSerialNo.TabIndex = 34;
            this.dgvSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSerialNo_KeyDown);
            // 
            // RowNo
            // 
            this.RowNo.DataPropertyName = "LineNo";
            this.RowNo.HeaderText = "Row";
            this.RowNo.Name = "RowNo";
            this.RowNo.ReadOnly = true;
            this.RowNo.Width = 35;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "Serial No";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            this.SerialNo.Width = 200;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTotal.Location = new System.Drawing.Point(240, 59);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(76, 21);
            this.txtTotal.TabIndex = 35;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(14, 59);
            this.txtSerialNo.MaxLength = 50;
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(224, 21);
            this.txtSerialNo.TabIndex = 66;
            this.txtSerialNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNo_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(230, 417);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            this.btnClose.TabIndex = 67;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(14, 9);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(140, 16);
            this.lblSerial.TabIndex = 68;
            this.lblSerial.Text = "Purchase Order No";
            // 
            // FrmSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 457);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.dgvSerialNo);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmSerial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSerial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSerial_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSerial_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSerialNo;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        public Glass.GlassButton btnClose;
        private System.Windows.Forms.Label lblSerial;
    }
}