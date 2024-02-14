namespace UI.Windows
{
    partial class FrmLoginUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginUser));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.chkRememberPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new Glass.GlassButton();
            this.btnOk = new Glass.GlassButton();
            this.btnHint = new Glass.GlassButton();
            this.btnCancel = new Glass.GlassButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.chkRememberPassword);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.picLogin);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(139, 68);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Location";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(139, 42);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(61, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(139, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(70, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name";
            // 
            // chkRememberPassword
            // 
            this.chkRememberPassword.AutoSize = true;
            this.chkRememberPassword.Location = new System.Drawing.Point(308, 102);
            this.chkRememberPassword.Name = "chkRememberPassword";
            this.chkRememberPassword.Size = new System.Drawing.Size(147, 17);
            this.chkRememberPassword.TabIndex = 0;
            this.chkRememberPassword.Text = "Remember Password";
            this.chkRememberPassword.UseVisualStyleBackColor = true;
            this.chkRememberPassword.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(215, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(241, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(215, 13);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(241, 21);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // cmbLocation
            // 
            this.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocation.Enabled = false;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(215, 65);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(241, 21);
            this.cmbLocation.TabIndex = 2;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // picLogin
            // 
            this.picLogin.Image = global::UI.Windows.Properties.Resources.images1;
            this.picLogin.Location = new System.Drawing.Point(2, 9);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(131, 131);
            this.picLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogin.TabIndex = 13;
            this.picLogin.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.btnHint);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Location = new System.Drawing.Point(2, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 47);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(280, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 32);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Cl&ear   ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnOk.Enabled = false;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(190, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 32);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok    ";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnHint
            // 
            this.btnHint.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnHint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHint.ForeColor = System.Drawing.Color.Black;
            this.btnHint.Image = ((System.Drawing.Image)(resources.GetObject("btnHint.Image")));
            this.btnHint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHint.Location = new System.Drawing.Point(8, 11);
            this.btnHint.Name = "btnHint";
            this.btnHint.Size = new System.Drawing.Size(87, 32);
            this.btnHint.TabIndex = 0;
            this.btnHint.Text = "&Hint   ";
            this.btnHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHint.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(370, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel ";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmLoginUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 177);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmLoginUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login User";
            this.Load += new System.EventHandler(this.FrmLoginUser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRememberPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.GroupBox groupBox2;
        private Glass.GlassButton btnClear;
        private Glass.GlassButton btnOk;
        private Glass.GlassButton btnHint;
        private Glass.GlassButton btnCancel;
        private System.Windows.Forms.PictureBox picLogin;
    }
}