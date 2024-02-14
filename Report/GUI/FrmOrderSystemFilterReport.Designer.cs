namespace Report.GUI
{
    partial class FrmOrderSystemFilterReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderSystemFilterReport));
            Telerik.WinControls.UI.RadPrintWatermark radPrintWatermark2 = new Telerik.WinControls.UI.RadPrintWatermark();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dgvFilter = new Telerik.WinControls.UI.RadGridView();
            this.radPrintDocument1 = new Telerik.WinControls.UI.RadPrintDocument();
            this.btnExport = new Glass.GlassButton();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(332, 464);
            this.grpButtonSet2.Size = new System.Drawing.Size(959, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(1006, 464);
            this.grpButtonSet.Size = new System.Drawing.Size(147, 46);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(97, 11);
            this.btnClear.Size = new System.Drawing.Size(87, 32);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(201, 11);
            this.btnClose.Size = new System.Drawing.Size(87, 32);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(8, 11);
            this.btnView.Size = new System.Drawing.Size(84, 32);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(15, 9);
            this.btnHelp.Size = new System.Drawing.Size(99, 32);
            this.btnHelp.Text = "&Preview";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 24);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // dgvFilter
            // 
            this.dgvFilter.Location = new System.Drawing.Point(13, 9);
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.Size = new System.Drawing.Size(1171, 458);
            this.dgvFilter.TabIndex = 12;
            this.dgvFilter.Text = "radGridView1";
            this.dgvFilter.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.dgvFilter_RowFormatting);
            this.dgvFilter.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvFilter_CellFormatting);
            this.dgvFilter.PrintCellFormatting += new Telerik.WinControls.UI.PrintCellFormattingEventHandler(this.dgvFilter_PrintCellFormatting);
            // 
            // radPrintDocument1
            // 
            this.radPrintDocument1.FooterFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrintDocument1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrintDocument1.Watermark = radPrintWatermark2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(13, 478);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(142, 32);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "&Export To Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FrmOrderSystemFilterReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 512);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvFilter);
            this.Name = "FrmOrderSystemFilterReport";
            this.Text = "                                       Active Pendign Filter Report";
            this.Load += new System.EventHandler(this.FrmOrderSystemFilterReport_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.dgvFilter, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Telerik.WinControls.UI.RadGridView dgvFilter;
        private Telerik.WinControls.UI.RadPrintDocument radPrintDocument1;
        public Glass.GlassButton btnExport;
    }
}