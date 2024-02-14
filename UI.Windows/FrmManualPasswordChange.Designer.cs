namespace UI.Windows
{
    partial class FrmManualPasswordChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManualPasswordChange));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new Glass.GlassButton();
            this.btnSave = new Glass.GlassButton();
            this.btnCancel = new Glass.GlassButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserSescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Location = new System.Drawing.Point(1, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 47);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(188, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 32);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Cl&ear   ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(6, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save  ";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbUserName);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.txtUserSescription);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 60);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // cmbUserName
            // 
            this.cmbUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUserName.DisplayMember = "User_Name";
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(125, 10);
            this.cmbUserName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbUserName.MaxLength = 50;
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(332, 21);
            this.cmbUserName.TabIndex = 40;
            this.cmbUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUserName_KeyDown);
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(11, 13);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(85, 18);
            this.lblUserName.TabIndex = 41;
            this.lblUserName.Text = "User Name";
            // 
            // txtUserSescription
            // 
            this.txtUserSescription.Enabled = false;
            this.txtUserSescription.Location = new System.Drawing.Point(125, 33);
            this.txtUserSescription.Name = "txtUserSescription";
            this.txtUserSescription.Size = new System.Drawing.Size(332, 21);
            this.txtUserSescription.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "User Description";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(11, 38);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(89, 13);
            this.lblNewPassword.TabIndex = 15;
            this.lblNewPassword.Text = "New Password";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(11, 60);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(111, 13);
            this.lblConfirmPassword.TabIndex = 16;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picLogin);
            this.groupBox3.Controls.Add(this.txtOldPassword);
            this.groupBox3.Controls.Add(this.lblOldPassword);
            this.groupBox3.Controls.Add(this.txtConfirmPassword);
            this.groupBox3.Controls.Add(this.txtNewPassword);
            this.groupBox3.Controls.Add(this.lblNewPassword);
            this.groupBox3.Controls.Add(this.lblConfirmPassword);
            this.groupBox3.Location = new System.Drawing.Point(1, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 83);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            // 
            // picLogin
            // 
            this.picLogin.Image = ((System.Drawing.Image)(resources.GetObject("picLogin.Image")));
            this.picLogin.Location = new System.Drawing.Point(355, 11);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(101, 67);
            this.picLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogin.TabIndex = 18;
            this.picLogin.TabStop = false;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Enabled = false;
            this.txtOldPassword.Location = new System.Drawing.Point(125, 11);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(224, 21);
            this.txtOldPassword.TabIndex = 27;
            this.txtOldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOldPassword_KeyDown);
            this.txtOldPassword.Leave += new System.EventHandler(this.txtOldPassword_Leave);
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(11, 16);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(84, 13);
            this.lblOldPassword.TabIndex = 26;
            this.lblOldPassword.Text = "Old Password";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Enabled = false;
            this.txtConfirmPassword.Location = new System.Drawing.Point(125, 57);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(224, 21);
            this.txtConfirmPassword.TabIndex = 23;
            this.txtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPassword_KeyDown);
            this.txtConfirmPassword.Leave += new System.EventHandler(this.txtConfirmPassword_Leave);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Enabled = false;
            this.txtNewPassword.Location = new System.Drawing.Point(125, 34);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(224, 21);
            this.txtNewPassword.TabIndex = 22;
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            // 
            // FrmManualPasswordChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 178);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmManualPasswordChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPasswordChange_FormClosing);
            this.Load += new System.EventHandler(this.FrmManualPasswordChange_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private Glass.GlassButton btnClear;
        private Glass.GlassButton btnSave;
        private Glass.GlassButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUserSescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.ComboBox cmbUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.PictureBox picLogin;
    }
}