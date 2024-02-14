namespace UI.Windows
{
    partial class FrmLoyaltyCustomerGrid
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
            this.dgvLoyalityCustomerGrid = new System.Windows.Forms.DataGridView();
            this.CustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NicNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoyalityCustomerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLoyalityCustomerGrid
            // 
            this.dgvLoyalityCustomerGrid.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvLoyalityCustomerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoyalityCustomerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerCode,
            this.CustomerName,
            this.CardNo,
            this.NicNo,
            this.Active});
            this.dgvLoyalityCustomerGrid.Location = new System.Drawing.Point(5, 24);
            this.dgvLoyalityCustomerGrid.Name = "dgvLoyalityCustomerGrid";
            this.dgvLoyalityCustomerGrid.RowHeadersWidth = 20;
            this.dgvLoyalityCustomerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoyalityCustomerGrid.Size = new System.Drawing.Size(929, 226);
            this.dgvLoyalityCustomerGrid.TabIndex = 0;
            this.dgvLoyalityCustomerGrid.DoubleClick += new System.EventHandler(this.dgvLoyalityCustomerGrid_DoubleClick);
            this.dgvLoyalityCustomerGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvLoyalityCustomerGrid_KeyDown);
            // 
            // CustomerCode
            // 
            this.CustomerCode.DataPropertyName = "CustomerCode";
            this.CustomerCode.HeaderText = "Customer Code";
            this.CustomerCode.Name = "CustomerCode";
            this.CustomerCode.ReadOnly = true;
            this.CustomerCode.Width = 150;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 300;
            // 
            // CardNo
            // 
            this.CardNo.HeaderText = "Card No";
            this.CardNo.Name = "CardNo";
            this.CardNo.ReadOnly = true;
            this.CardNo.Width = 150;
            // 
            // NicNo
            // 
            this.NicNo.DataPropertyName = "NicNo";
            this.NicNo.HeaderText = "Nic No";
            this.NicNo.Name = "NicNo";
            this.NicNo.ReadOnly = true;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Status";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Width = 50;
            // 
            // FrmLoyaltyCustomerGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(938, 255);
            this.Controls.Add(this.dgvLoyalityCustomerGrid);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLoyaltyCustomerGrid";
            this.Text = "FrmLoyaltyCustomerGrid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLoyaltyCustomerGrid_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoyalityCustomerGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLoyalityCustomerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NicNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
    }
}