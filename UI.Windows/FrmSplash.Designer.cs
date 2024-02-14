namespace UI.Windows
{
    partial class FrmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picPreloader = new System.Windows.Forms.PictureBox();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblComponent = new System.Windows.Forms.Label();
            this.lblDatabaseStatus = new System.Windows.Forms.Label();
            this.lblLicensedTo = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPreloader)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picPreloader
            // 
            this.picPreloader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(41)))), ((int)(((byte)(39)))));
            this.picPreloader.Image = ((System.Drawing.Image)(resources.GetObject("picPreloader.Image")));
            this.picPreloader.Location = new System.Drawing.Point(314, 239);
            this.picPreloader.Name = "picPreloader";
            this.picPreloader.Size = new System.Drawing.Size(131, 4);
            this.picPreloader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPreloader.TabIndex = 0;
            this.picPreloader.TabStop = false;
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlProgressBar.Location = new System.Drawing.Point(1, 295);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(1, 15);
            this.pnlProgressBar.TabIndex = 3;
            this.pnlProgressBar.Tag = "";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblVersion.Location = new System.Drawing.Point(54, 141);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(84, 14);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version 1.0.0.0";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.BackColor = System.Drawing.Color.Transparent;
            this.lblComponent.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComponent.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblComponent.Location = new System.Drawing.Point(12, 262);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(144, 13);
            this.lblComponent.TabIndex = 6;
            this.lblComponent.Text = "Checking Components...........";
            // 
            // lblDatabaseStatus
            // 
            this.lblDatabaseStatus.AutoSize = true;
            this.lblDatabaseStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabaseStatus.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseStatus.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblDatabaseStatus.Location = new System.Drawing.Point(12, 275);
            this.lblDatabaseStatus.Name = "lblDatabaseStatus";
            this.lblDatabaseStatus.Size = new System.Drawing.Size(177, 13);
            this.lblDatabaseStatus.TabIndex = 5;
            this.lblDatabaseStatus.Text = "Checking Database Connectivity.......";
            // 
            // lblLicensedTo
            // 
            this.lblLicensedTo.BackColor = System.Drawing.Color.Transparent;
            this.lblLicensedTo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicensedTo.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblLicensedTo.Location = new System.Drawing.Point(326, 210);
            this.lblLicensedTo.Name = "lblLicensedTo";
            this.lblLicensedTo.Size = new System.Drawing.Size(119, 14);
            this.lblLicensedTo.TabIndex = 7;
            this.lblLicensedTo.Text = "Licensed To J and D";
            this.lblLicensedTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblProductName.Location = new System.Drawing.Point(52, 115);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(247, 26);
            this.lblProductName.TabIndex = 8;
            this.lblProductName.Text = "Order Management System";
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(451, 297);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblLicensedTo);
            this.Controls.Add(this.lblComponent);
            this.Controls.Add(this.lblDatabaseStatus);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pnlProgressBar);
            this.Controls.Add(this.picPreloader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSplash";
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPreloader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox picPreloader;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.Label lblDatabaseStatus;
        private System.Windows.Forms.Label lblLicensedTo;
        private System.Windows.Forms.Label lblProductName;

    }
}