namespace UI.Windows
{
    partial class FrmBaseReportsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseReportsForm));
            this.grpButtonSet2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new Glass.GlassButton();
            this.btnView = new Glass.GlassButton();
            this.btnClose = new Glass.GlassButton();
            this.grpButtonSet = new System.Windows.Forms.GroupBox();
            this.btnHelp = new Glass.GlassButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButtonSet2.Controls.Add(this.btnClear);
            this.grpButtonSet2.Controls.Add(this.btnView);
            this.grpButtonSet2.Controls.Add(this.btnClose);
            this.grpButtonSet2.Location = new System.Drawing.Point(589, 236);
            this.grpButtonSet2.Name = "grpButtonSet2";
            this.grpButtonSet2.Size = new System.Drawing.Size(238, 46);
            this.grpButtonSet2.TabIndex = 11;
            this.grpButtonSet2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(81, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Cl&ear ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnView.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(3, 11);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 32);
            this.btnView.TabIndex = 7;
            this.btnView.Text = "&View ";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(159, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpButtonSet.Controls.Add(this.btnHelp);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 236);
            this.grpButtonSet.Name = "grpButtonSet";
            this.grpButtonSet.Size = new System.Drawing.Size(83, 46);
            this.grpButtonSet.TabIndex = 10;
            this.grpButtonSet.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnHelp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHelp.Location = new System.Drawing.Point(3, 11);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 32);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "&Help ";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FrmBaseReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 284);
            this.ControlBox = false;
            this.Controls.Add(this.grpButtonSet2);
            this.Controls.Add(this.grpButtonSet);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmBaseReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBaseTransactionForm";
            this.Load += new System.EventHandler(this.FrmBaseTransactionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBaseTransactionForm_KeyDown);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox grpButtonSet2;
        public System.Windows.Forms.GroupBox grpButtonSet;
        protected System.Windows.Forms.ErrorProvider errorProvider;
        public Glass.GlassButton btnClear;
        public Glass.GlassButton btnClose;
        public Glass.GlassButton btnView;
        public Glass.GlassButton btnHelp;
    }
}