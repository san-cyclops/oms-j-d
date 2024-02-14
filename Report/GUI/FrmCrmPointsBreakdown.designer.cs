namespace Report.GUI
{
    partial class FrmCrmPointsBreakdown
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvBreakdown = new System.Windows.Forms.DataGridView();
            this.RangeFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangeTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PointsBreakdownID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBreakdown)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(263, 303);
            this.grpButtonSet2.Size = new System.Drawing.Size(236, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 303);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblDateRange);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 312);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(8, 297);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(478, 10);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 145;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 128;
            this.label1.Text = "-";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(242, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 21);
            this.dtpToDate.TabIndex = 127;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Enabled = false;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(93, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 116;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(8, 23);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(74, 13);
            this.lblDateRange.TabIndex = 115;
            this.lblDateRange.Text = "Date Range";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvBreakdown);
            this.groupBox2.Location = new System.Drawing.Point(8, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 250);
            this.groupBox2.TabIndex = 147;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Value Range";
            // 
            // dgvBreakdown
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvBreakdown.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBreakdown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBreakdown.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RangeFrom,
            this.RangeTo,
            this.PointsBreakdownID});
            this.dgvBreakdown.Location = new System.Drawing.Point(5, 22);
            this.dgvBreakdown.Name = "dgvBreakdown";
            this.dgvBreakdown.Size = new System.Drawing.Size(465, 222);
            this.dgvBreakdown.TabIndex = 146;
            this.dgvBreakdown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBreakdown_KeyDown);
            // 
            // RangeFrom
            // 
            this.RangeFrom.DataPropertyName = "RangeFrom";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RangeFrom.DefaultCellStyle = dataGridViewCellStyle2;
            this.RangeFrom.HeaderText = "Range From";
            this.RangeFrom.Name = "RangeFrom";
            this.RangeFrom.Width = 200;
            // 
            // RangeTo
            // 
            this.RangeTo.DataPropertyName = "RangeTo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RangeTo.DefaultCellStyle = dataGridViewCellStyle3;
            this.RangeTo.HeaderText = "Range To";
            this.RangeTo.Name = "RangeTo";
            this.RangeTo.Width = 200;
            // 
            // PointsBreakdownID
            // 
            this.PointsBreakdownID.DataPropertyName = "PointsBreakdownID";
            this.PointsBreakdownID.HeaderText = "PointsBreakdownID";
            this.PointsBreakdownID.Name = "PointsBreakdownID";
            this.PointsBreakdownID.Visible = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // FrmCrmPointsBreakdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 351);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCrmPointsBreakdown";
            this.Text = "FrmCrmPointsBreakdown";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBreakdown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvBreakdown;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangeTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PointsBreakdownID;
    }
}