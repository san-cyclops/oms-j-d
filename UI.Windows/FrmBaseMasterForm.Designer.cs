namespace UI.Windows
{
    partial class FrmBaseMasterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseMasterForm));
            this.btnDelete = new Glass.GlassButton();
            this.grpButtonSet = new System.Windows.Forms.GroupBox();
            this.btnPrint = new Glass.GlassButton();
            this.btnView = new Glass.GlassButton();
            this.btnHelp = new Glass.GlassButton();
            this.grpButtonSet2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new Glass.GlassButton();
            this.btnSave = new Glass.GlassButton();
            this.btnClose = new Glass.GlassButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(238, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpButtonSet.Controls.Add(this.btnPrint);
            this.grpButtonSet.Controls.Add(this.btnView);
            this.grpButtonSet.Controls.Add(this.btnHelp);
            this.grpButtonSet.Controls.Add(this.btnDelete);
            this.grpButtonSet.Location = new System.Drawing.Point(2, 184);
            this.grpButtonSet.Name = "grpButtonSet";
            this.grpButtonSet.Size = new System.Drawing.Size(316, 46);
            this.grpButtonSet.TabIndex = 6;
            this.grpButtonSet.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(160, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "P&rint ";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnView.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(82, 11);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 32);
            this.btnView.TabIndex = 7;
            this.btnView.Text = "&View ";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnHelp.FadeOnFocus = true;
            this.btnHelp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHelp.Location = new System.Drawing.Point(4, 11);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 32);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "&Help ";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButtonSet2.Controls.Add(this.btnClear);
            this.grpButtonSet2.Controls.Add(this.btnSave);
            this.grpButtonSet2.Controls.Add(this.btnClose);
            this.grpButtonSet2.Location = new System.Drawing.Point(427, 184);
            this.grpButtonSet2.Name = "grpButtonSet2";
            this.grpButtonSet2.Size = new System.Drawing.Size(239, 46);
            this.grpButtonSet2.TabIndex = 9;
            this.grpButtonSet2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(82, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Cl&ear ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(4, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "&Save ";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(160, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "&Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FrmBaseMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(668, 233);
            this.ControlBox = false;
            this.Controls.Add(this.grpButtonSet2);
            this.Controls.Add(this.grpButtonSet);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmBaseMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBaseMasterForm";
            this.Load += new System.EventHandler(this.FrmBaseMasterForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBaseMasterForm_KeyDown);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox grpButtonSet;
        protected System.Windows.Forms.GroupBox grpButtonSet2;
        protected Glass.GlassButton btnPrint;
        protected Glass.GlassButton btnView;
        protected Glass.GlassButton btnHelp;
        protected Glass.GlassButton btnClear;
        protected Glass.GlassButton btnSave;
        protected Glass.GlassButton btnClose;
        protected Glass.GlassButton btnDelete;
        protected System.Windows.Forms.ErrorProvider errorProvider;



    }
}