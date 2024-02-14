namespace UI.Windows
{
    partial class FrmSerialCommon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSerialCommon));
            this.dgvSearchDetails = new System.Windows.Forms.DataGridView();
            this.SelectSerial = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSerial = new System.Windows.Forms.Label();
            this.btnClose = new Glass.GlassButton();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchDetails
            // 
            this.dgvSearchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectSerial,
            this.SerialNo});
            this.dgvSearchDetails.Location = new System.Drawing.Point(12, 84);
            this.dgvSearchDetails.Name = "dgvSearchDetails";
            this.dgvSearchDetails.RowHeadersWidth = 5;
            this.dgvSearchDetails.Size = new System.Drawing.Size(277, 261);
            this.dgvSearchDetails.TabIndex = 69;
            // 
            // SelectSerial
            // 
            this.SelectSerial.HeaderText = "Select";
            this.SelectSerial.Name = "SelectSerial";
            this.SelectSerial.Width = 50;
            // 
            // SerialNo
            // 
            this.SerialNo.DataPropertyName = "SerialNo";
            this.SerialNo.HeaderText = "Serial No";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            this.SerialNo.Width = 200;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(12, 10);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(140, 16);
            this.lblSerial.TabIndex = 73;
            this.lblSerial.Text = "Purchase Order No";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(202, 349);
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
            // 
            // FrmSerialCommon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(299, 389);
            this.ControlBox = false;
            this.Controls.Add(this.dgvSearchDetails);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.txtTotal);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSerialCommon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSerialCommon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSerialCommon_FormClosing);
            this.Load += new System.EventHandler(this.FrmSerialCommon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSerialCommon_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchDetails;
        private System.Windows.Forms.Label lblSerial;
        public Glass.GlassButton btnClose;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.TextBox txtTotal;

    }
}