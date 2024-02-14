namespace Report.GUI
{
    partial class FrmOrderSystemFilterReport2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderSystemFilterReport2));
            this.dgvFilter = new ADGV.AdvancedDataGridView();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.searchToolBar1 = new ADGV.SearchToolBar();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.textBox_sort = new System.Windows.Forms.TextBox();
            this.textBox_filter = new System.Windows.Forms.TextBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(824, 426);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 426);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dgvFilter
            // 
            this.dgvFilter.AllowUserToAddRows = false;
            this.dgvFilter.AllowUserToDeleteRows = false;
            this.dgvFilter.AutoGenerateContextFilters = true;
            this.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilter.DateWithTime = false;
            this.dgvFilter.Location = new System.Drawing.Point(2, 31);
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.ReadOnly = true;
            this.dgvFilter.Size = new System.Drawing.Size(1050, 389);
            this.dgvFilter.TabIndex = 12;
            this.dgvFilter.TimeFilter = false;
            this.dgvFilter.SortStringChanged += new System.EventHandler(this.dgvFilter_SortStringChanged);
            this.dgvFilter.FilterStringChanged += new System.EventHandler(this.dgvFilter_FilterStringChanged);
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
            // searchToolBar1
            // 
            this.searchToolBar1.AllowMerge = false;
            this.searchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.searchToolBar1.Location = new System.Drawing.Point(0, 0);
            this.searchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.searchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.searchToolBar1.Name = "searchToolBar1";
            this.searchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.searchToolBar1.Size = new System.Drawing.Size(1064, 27);
            this.searchToolBar1.TabIndex = 13;
            this.searchToolBar1.Text = "searchToolBar1";
            this.searchToolBar1.Search += new ADGV.SearchToolBarSearchEventHandler(this.searchToolBar1_Search);
            this.searchToolBar1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.searchToolBar1_ItemClicked);
            // 
            // textBox_sort
            // 
            this.textBox_sort.Location = new System.Drawing.Point(189, 30);
            this.textBox_sort.Multiline = true;
            this.textBox_sort.Name = "textBox_sort";
            this.textBox_sort.ReadOnly = true;
            this.textBox_sort.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_sort.Size = new System.Drawing.Size(180, 33);
            this.textBox_sort.TabIndex = 16;
            // 
            // textBox_filter
            // 
            this.textBox_filter.Location = new System.Drawing.Point(3, 32);
            this.textBox_filter.Multiline = true;
            this.textBox_filter.Name = "textBox_filter";
            this.textBox_filter.ReadOnly = true;
            this.textBox_filter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_filter.Size = new System.Drawing.Size(180, 33);
            this.textBox_filter.TabIndex = 15;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // FrmOrderSystemFilterReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 474);
            this.Controls.Add(this.searchToolBar1);
            this.Controls.Add(this.dgvFilter);
            this.Controls.Add(this.textBox_sort);
            this.Controls.Add(this.textBox_filter);
            this.Name = "FrmOrderSystemFilterReport";
            this.Text = "Filter Report";
            this.Load += new System.EventHandler(this.FrmOrderSystemFilterReport_Load);
            this.Controls.SetChildIndex(this.textBox_filter, 0);
            this.Controls.SetChildIndex(this.textBox_sort, 0);
            this.Controls.SetChildIndex(this.dgvFilter, 0);
            this.Controls.SetChildIndex(this.searchToolBar1, 0);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ADGV.AdvancedDataGridView dgvFilter;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private ADGV.SearchToolBar searchToolBar1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox textBox_sort;
        private System.Windows.Forms.TextBox textBox_filter;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}