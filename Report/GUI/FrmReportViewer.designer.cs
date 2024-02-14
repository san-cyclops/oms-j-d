namespace Report
{
    partial class FrmReportViewer
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
            this.crRptViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crRptViewer
            // 
            this.crRptViewer.ActiveViewIndex = -1;
            this.crRptViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crRptViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crRptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crRptViewer.Location = new System.Drawing.Point(0, 0);
            this.crRptViewer.Name = "crRptViewer";
            this.crRptViewer.Size = new System.Drawing.Size(649, 420);
            this.crRptViewer.TabIndex = 0;
            this.crRptViewer.Load += new System.EventHandler(this.crRptViewer_Load);
            this.crRptViewer.Click += new System.EventHandler(this.crRptViewer_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // FrmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.crRptViewer);
            this.Name = "FrmReportViewer";
            this.Text = "Report Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmReportViewer_FormClosed);
            this.Load += new System.EventHandler(this.FrmReportViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReportViewer_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crRptViewer;
        private System.Windows.Forms.Button button1;

    }
}