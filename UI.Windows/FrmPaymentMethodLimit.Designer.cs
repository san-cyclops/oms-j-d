namespace UI.Windows
{
    partial class FrmPaymentMethodLimit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPaymentMethodLimit));
            this.btnClose = new Glass.GlassButton();
            this.txtChequePeriod = new UI.Windows.CustomControls.TextBoxNumeric();
            this.lblChequePeriod = new System.Windows.Forms.Label();
            this.txtChequeLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblChequeLimit = new System.Windows.Forms.Label();
            this.txtCreditPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblCreditPeriod = new System.Windows.Forms.Label();
            this.txtCreditLimit = new UI.Windows.CustomControls.TextBoxCurrency();
            this.lblCreditLimit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(190, 184);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            this.btnClose.TabIndex = 78;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtChequePeriod
            // 
            this.txtChequePeriod.Location = new System.Drawing.Point(147, 147);
            this.txtChequePeriod.Name = "txtChequePeriod";
            this.txtChequePeriod.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChequePeriod.Size = new System.Drawing.Size(130, 20);
            this.txtChequePeriod.TabIndex = 111;
            // 
            // lblChequePeriod
            // 
            this.lblChequePeriod.AutoSize = true;
            this.lblChequePeriod.Location = new System.Drawing.Point(12, 150);
            this.lblChequePeriod.Name = "lblChequePeriod";
            this.lblChequePeriod.Size = new System.Drawing.Size(110, 13);
            this.lblChequePeriod.TabIndex = 110;
            this.lblChequePeriod.Text = "Cheque Period (Days)";
            // 
            // txtChequeLimit
            // 
            this.txtChequeLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChequeLimit.Location = new System.Drawing.Point(147, 124);
            this.txtChequeLimit.Name = "txtChequeLimit";
            this.txtChequeLimit.Size = new System.Drawing.Size(130, 20);
            this.txtChequeLimit.TabIndex = 109;
            // 
            // lblChequeLimit
            // 
            this.lblChequeLimit.AutoSize = true;
            this.lblChequeLimit.Location = new System.Drawing.Point(12, 127);
            this.lblChequeLimit.Name = "lblChequeLimit";
            this.lblChequeLimit.Size = new System.Drawing.Size(68, 13);
            this.lblChequeLimit.TabIndex = 108;
            this.lblChequeLimit.Text = "Cheque Limit";
            // 
            // txtCreditPeriod
            // 
            this.txtCreditPeriod.IntValue = 0;
            this.txtCreditPeriod.Location = new System.Drawing.Point(147, 101);
            this.txtCreditPeriod.Name = "txtCreditPeriod";
            this.txtCreditPeriod.Size = new System.Drawing.Size(130, 20);
            this.txtCreditPeriod.TabIndex = 107;
            // 
            // lblCreditPeriod
            // 
            this.lblCreditPeriod.AutoSize = true;
            this.lblCreditPeriod.Location = new System.Drawing.Point(12, 104);
            this.lblCreditPeriod.Name = "lblCreditPeriod";
            this.lblCreditPeriod.Size = new System.Drawing.Size(100, 13);
            this.lblCreditPeriod.TabIndex = 106;
            this.lblCreditPeriod.Text = "Credit Period (Days)";
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCreditLimit.Location = new System.Drawing.Point(147, 78);
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(130, 20);
            this.txtCreditLimit.TabIndex = 105;
            // 
            // lblCreditLimit
            // 
            this.lblCreditLimit.AutoSize = true;
            this.lblCreditLimit.Location = new System.Drawing.Point(12, 81);
            this.lblCreditLimit.Name = "lblCreditLimit";
            this.lblCreditLimit.Size = new System.Drawing.Size(58, 13);
            this.lblCreditLimit.TabIndex = 104;
            this.lblCreditLimit.Text = "Credit Limit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 112;
            this.label1.Text = "Payment Methods Limit";
            // 
            // FrmPaymentMethodLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 245);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChequePeriod);
            this.Controls.Add(this.lblChequePeriod);
            this.Controls.Add(this.txtChequeLimit);
            this.Controls.Add(this.lblChequeLimit);
            this.Controls.Add(this.txtCreditPeriod);
            this.Controls.Add(this.lblCreditPeriod);
            this.Controls.Add(this.txtCreditLimit);
            this.Controls.Add(this.lblCreditLimit);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPaymentMethodLimit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPaymentMethodLimit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPaymentMethodLimit_FormClosing);
            this.Load += new System.EventHandler(this.FrmPaymentMethodLimit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Glass.GlassButton btnClose;
        private CustomControls.TextBoxNumeric txtChequePeriod;
        private System.Windows.Forms.Label lblChequePeriod;
        private CustomControls.TextBoxCurrency txtChequeLimit;
        private System.Windows.Forms.Label lblChequeLimit;
        private CustomControls.TextBoxInteger txtCreditPeriod;
        private System.Windows.Forms.Label lblCreditPeriod;
        private CustomControls.TextBoxCurrency txtCreditLimit;
        private System.Windows.Forms.Label lblCreditLimit;
        private System.Windows.Forms.Label label1;
    }
}