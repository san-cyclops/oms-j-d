namespace UI.Windows
{
    partial class FrmPaymentMethod
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
            this.grpArea = new System.Windows.Forms.GroupBox();
            this.txtCommisionRate = new UI.Windows.CustomControls.TextBoxPercentGen();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationPaymentMethodCode = new System.Windows.Forms.CheckBox();
            this.lblCommissionRate = new System.Windows.Forms.Label();
            this.txtPaymentMethodName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtPaymentMethodCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblPaymentMethodCode = new System.Windows.Forms.Label();
            this.lblPaymentMethodName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 101);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 101);
            // 
            // grpArea
            // 
            this.grpArea.Controls.Add(this.txtCommisionRate);
            this.grpArea.Controls.Add(this.chkAutoClear);
            this.grpArea.Controls.Add(this.btnNew);
            this.grpArea.Controls.Add(this.chkAutoCompleationPaymentMethodCode);
            this.grpArea.Controls.Add(this.lblCommissionRate);
            this.grpArea.Controls.Add(this.txtPaymentMethodName);
            this.grpArea.Controls.Add(this.txtPaymentMethodCode);
            this.grpArea.Controls.Add(this.lblPaymentMethodCode);
            this.grpArea.Controls.Add(this.lblPaymentMethodName);
            this.grpArea.Location = new System.Drawing.Point(2, -5);
            this.grpArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpArea.Name = "grpArea";
            this.grpArea.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpArea.Size = new System.Drawing.Size(635, 111);
            this.grpArea.TabIndex = 13;
            this.grpArea.TabStop = false;
            // 
            // txtCommisionRate
            // 
            this.txtCommisionRate.ForeColor = System.Drawing.Color.Red;
            this.txtCommisionRate.Location = new System.Drawing.Point(186, 64);
            this.txtCommisionRate.Name = "txtCommisionRate";
            this.txtCommisionRate.PercentageValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCommisionRate.Size = new System.Drawing.Size(443, 21);
            this.txtCommisionRate.TabIndex = 30;
            this.txtCommisionRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommisionRate_KeyDown);
            this.txtCommisionRate.Leave += new System.EventHandler(this.txtCommisionRate_Leave);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(544, 91);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 29;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(317, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 28;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationPaymentMethodCode
            // 
            this.chkAutoCompleationPaymentMethodCode.AutoSize = true;
            this.chkAutoCompleationPaymentMethodCode.Checked = true;
            this.chkAutoCompleationPaymentMethodCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationPaymentMethodCode.Location = new System.Drawing.Point(169, 19);
            this.chkAutoCompleationPaymentMethodCode.Name = "chkAutoCompleationPaymentMethodCode";
            this.chkAutoCompleationPaymentMethodCode.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationPaymentMethodCode.TabIndex = 27;
            this.chkAutoCompleationPaymentMethodCode.Tag = "1";
            this.chkAutoCompleationPaymentMethodCode.UseVisualStyleBackColor = true;
            this.chkAutoCompleationPaymentMethodCode.CheckedChanged += new System.EventHandler(this.chkAutoCompleationPaymentMethod_CheckedChanged);
            // 
            // lblCommissionRate
            // 
            this.lblCommissionRate.AutoSize = true;
            this.lblCommissionRate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommissionRate.Location = new System.Drawing.Point(10, 70);
            this.lblCommissionRate.Name = "lblCommissionRate";
            this.lblCommissionRate.Size = new System.Drawing.Size(123, 13);
            this.lblCommissionRate.TabIndex = 15;
            this.lblCommissionRate.Text = "Commission Rate %";
            // 
            // txtPaymentMethodName
            // 
            this.txtPaymentMethodName.Location = new System.Drawing.Point(186, 40);
            this.txtPaymentMethodName.MasterDescription = "";
            this.txtPaymentMethodName.Name = "txtPaymentMethodName";
            this.txtPaymentMethodName.Size = new System.Drawing.Size(443, 21);
            this.txtPaymentMethodName.TabIndex = 12;
            this.txtPaymentMethodName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaymentMethodName_KeyDown);
            this.txtPaymentMethodName.Leave += new System.EventHandler(this.txtPaymentMethodName_Leave);
            // 
            // txtPaymentMethodCode
            // 
            this.txtPaymentMethodCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentMethodCode.IsAutoComplete = false;
            this.txtPaymentMethodCode.ItemCollection = null;
            this.txtPaymentMethodCode.Location = new System.Drawing.Point(186, 16);
            this.txtPaymentMethodCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPaymentMethodCode.MasterCode = "";
            this.txtPaymentMethodCode.MaxLength = 25;
            this.txtPaymentMethodCode.Name = "txtPaymentMethodCode";
            this.txtPaymentMethodCode.Size = new System.Drawing.Size(130, 21);
            this.txtPaymentMethodCode.TabIndex = 11;
            this.txtPaymentMethodCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaymentMethodCode_KeyDown);
            this.txtPaymentMethodCode.Leave += new System.EventHandler(this.txtPaymentMethodCode_Leave);
            // 
            // lblPaymentMethodCode
            // 
            this.lblPaymentMethodCode.AutoSize = true;
            this.lblPaymentMethodCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentMethodCode.Location = new System.Drawing.Point(10, 19);
            this.lblPaymentMethodCode.Name = "lblPaymentMethodCode";
            this.lblPaymentMethodCode.Size = new System.Drawing.Size(150, 13);
            this.lblPaymentMethodCode.TabIndex = 9;
            this.lblPaymentMethodCode.Text = "Payment Methode Code*";
            // 
            // lblPaymentMethodName
            // 
            this.lblPaymentMethodName.AutoSize = true;
            this.lblPaymentMethodName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentMethodName.Location = new System.Drawing.Point(10, 44);
            this.lblPaymentMethodName.Name = "lblPaymentMethodName";
            this.lblPaymentMethodName.Size = new System.Drawing.Size(146, 13);
            this.lblPaymentMethodName.TabIndex = 10;
            this.lblPaymentMethodName.Text = "Payment Method Name*";
            // 
            // FrmPaymentMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(639, 150);
            this.Controls.Add(this.grpArea);
            this.Name = "FrmPaymentMethod";
            this.Text = "Payment Method";
            this.Load += new System.EventHandler(this.FrmPaymentMethod_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpArea, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpArea.ResumeLayout(false);
            this.grpArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpArea;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationPaymentMethodCode;
        private System.Windows.Forms.Label lblCommissionRate;
        private CustomControls.TextBoxMasterDescription txtPaymentMethodName;
        private CustomControls.TextBoxMasterCode txtPaymentMethodCode;
        private System.Windows.Forms.Label lblPaymentMethodCode;
        private System.Windows.Forms.Label lblPaymentMethodName;
        private CustomControls.TextBoxPercentGen txtCommisionRate;
    }
}
