namespace UI.Windows
{
    partial class FrmExchangeRate
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
            this.grpSize = new System.Windows.Forms.GroupBox();
            this.txtExchangeRate = new UI.Windows.CustomControls.TextBoxCurrency();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.lblSizeCode = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSize
            // 
            this.grpSize.Controls.Add(this.txtExchangeRate);
            this.grpSize.Controls.Add(this.chkAutoClear);
            this.grpSize.Controls.Add(this.lblSizeCode);
            this.grpSize.Location = new System.Drawing.Point(2, -5);
            this.grpSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSize.Name = "grpSize";
            this.grpSize.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSize.Size = new System.Drawing.Size(249, 86);
            this.grpSize.TabIndex = 12;
            this.grpSize.TabStop = false;
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.CurrencyValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExchangeRate.Location = new System.Drawing.Point(122, 31);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Size = new System.Drawing.Size(110, 20);
            this.txtExchangeRate.TabIndex = 31;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(362, 27);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(75, 17);
            this.chkAutoClear.TabIndex = 30;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // lblSizeCode
            // 
            this.lblSizeCode.AutoSize = true;
            this.lblSizeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSizeCode.Location = new System.Drawing.Point(24, 34);
            this.lblSizeCode.Name = "lblSizeCode";
            this.lblSizeCode.Size = new System.Drawing.Size(92, 13);
            this.lblSizeCode.TabIndex = 9;
            this.lblSizeCode.Text = "Exchange Rate";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::UI.Windows.Properties.Resources.web_pinterest1;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(159, 86);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmExchangeRate
            // 
            this.ClientSize = new System.Drawing.Size(255, 120);
            this.Controls.Add(this.grpSize);
            this.Controls.Add(this.btnSave);
            this.Name = "FrmExchangeRate";
            this.Text = "Size";
            this.Load += new System.EventHandler(this.FrmSize_Load);
            this.grpSize.ResumeLayout(false);
            this.grpSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSize;
        private System.Windows.Forms.Label lblSizeCode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxCurrency txtExchangeRate;
    }
}
