namespace UI.Windows
{
    partial class FrmMinusProducts
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
            this.dgvSearchDetails = new System.Windows.Forms.DataGridView();
            this.LineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchDetails
            // 
            this.dgvSearchDetails.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvSearchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNo,
            this.ProductCode,
            this.ProductName,
            this.UnitOfMeasure,
            this.Qty});
            this.dgvSearchDetails.Location = new System.Drawing.Point(4, 30);
            this.dgvSearchDetails.Name = "dgvSearchDetails";
            this.dgvSearchDetails.RowHeadersWidth = 5;
            this.dgvSearchDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchDetails.Size = new System.Drawing.Size(599, 211);
            this.dgvSearchDetails.TabIndex = 78;
            // 
            // LineNo
            // 
            this.LineNo.DataPropertyName = "LineNo";
            this.LineNo.HeaderText = "Row";
            this.LineNo.Name = "LineNo";
            this.LineNo.Width = 35;
            // 
            // ProductCode
            // 
            this.ProductCode.DataPropertyName = "ProductCode";
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 130;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 275;
            // 
            // UnitOfMeasure
            // 
            this.UnitOfMeasure.DataPropertyName = "UnitOfMeasure";
            this.UnitOfMeasure.HeaderText = "Unit";
            this.UnitOfMeasure.Name = "UnitOfMeasure";
            this.UnitOfMeasure.Width = 80;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Stock";
            this.Qty.Name = "Qty";
            this.Qty.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 14);
            this.label1.TabIndex = 79;
            this.label1.Text = "Stock Minus Products";
            // 
            // FrmMinusProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(607, 246);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSearchDetails);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMinusProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmMinusProducts";
            this.Click += new System.EventHandler(this.FrmMinusProducts_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMinusProducts_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
    }
}