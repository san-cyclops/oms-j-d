namespace Report.GUI
{
    partial class FrmCrmTypeWiseCustomerDetails
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.chkAllCostomers = new System.Windows.Forms.CheckBox();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.TxtSearchCodeFrom = new System.Windows.Forms.TextBox();
            this.LblSearchFrom = new System.Windows.Forms.Label();
            this.ChkAutoComplteFrom = new System.Windows.Forms.CheckBox();
            this.LblSerachTo = new System.Windows.Forms.Label();
            this.ChkAutoComplteTo = new System.Windows.Forms.CheckBox();
            this.TxtSearchCodeTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameTo = new System.Windows.Forms.TextBox();
            this.TxtSearchNameFrom = new System.Windows.Forms.TextBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(370, 134);
            this.grpButtonSet2.Size = new System.Drawing.Size(236, 46);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 134);
            // 
            // btnClear
            // 
            this.btnClear.Size = new System.Drawing.Size(73, 32);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(157, 11);
            // 
            // btnView
            // 
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCardType);
            this.groupBox1.Controls.Add(this.chkAllCostomers);
            this.groupBox1.Controls.Add(this.pnlSelection);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 143);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 139;
            this.label3.Text = "Card Type";
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(126, 100);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(231, 21);
            this.cmbCardType.TabIndex = 138;
            // 
            // chkAllCostomers
            // 
            this.chkAllCostomers.AutoSize = true;
            this.chkAllCostomers.Location = new System.Drawing.Point(105, 80);
            this.chkAllCostomers.Name = "chkAllCostomers";
            this.chkAllCostomers.Size = new System.Drawing.Size(106, 17);
            this.chkAllCostomers.TabIndex = 129;
            this.chkAllCostomers.Text = "All Customers";
            this.chkAllCostomers.UseVisualStyleBackColor = true;
            this.chkAllCostomers.CheckedChanged += new System.EventHandler(this.chkAllCostomers_CheckedChanged);
            // 
            // pnlSelection
            // 
            this.pnlSelection.Controls.Add(this.TxtSearchCodeFrom);
            this.pnlSelection.Controls.Add(this.LblSearchFrom);
            this.pnlSelection.Controls.Add(this.ChkAutoComplteFrom);
            this.pnlSelection.Controls.Add(this.LblSerachTo);
            this.pnlSelection.Controls.Add(this.ChkAutoComplteTo);
            this.pnlSelection.Controls.Add(this.TxtSearchCodeTo);
            this.pnlSelection.Controls.Add(this.TxtSearchNameTo);
            this.pnlSelection.Controls.Add(this.TxtSearchNameFrom);
            this.pnlSelection.Location = new System.Drawing.Point(6, 10);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(590, 64);
            this.pnlSelection.TabIndex = 131;
            // 
            // TxtSearchCodeFrom
            // 
            this.TxtSearchCodeFrom.Location = new System.Drawing.Point(120, 8);
            this.TxtSearchCodeFrom.Name = "TxtSearchCodeFrom";
            this.TxtSearchCodeFrom.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeFrom.TabIndex = 119;
            this.TxtSearchCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeFrom_KeyDown);
            this.TxtSearchCodeFrom.Leave += new System.EventHandler(this.TxtSearchCodeFrom_Leave);
            // 
            // LblSearchFrom
            // 
            this.LblSearchFrom.AutoSize = true;
            this.LblSearchFrom.Location = new System.Drawing.Point(3, 11);
            this.LblSearchFrom.Name = "LblSearchFrom";
            this.LblSearchFrom.Size = new System.Drawing.Size(96, 13);
            this.LblSearchFrom.TabIndex = 117;
            this.LblSearchFrom.Text = "Customer From";
            // 
            // ChkAutoComplteFrom
            // 
            this.ChkAutoComplteFrom.AutoSize = true;
            this.ChkAutoComplteFrom.Location = new System.Drawing.Point(99, 11);
            this.ChkAutoComplteFrom.Name = "ChkAutoComplteFrom";
            this.ChkAutoComplteFrom.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteFrom.TabIndex = 123;
            this.ChkAutoComplteFrom.Tag = "1";
            this.ChkAutoComplteFrom.UseVisualStyleBackColor = true;
            this.ChkAutoComplteFrom.CheckedChanged += new System.EventHandler(this.ChkAutoComplteFrom_CheckedChanged);
            // 
            // LblSerachTo
            // 
            this.LblSerachTo.AutoSize = true;
            this.LblSerachTo.Location = new System.Drawing.Point(3, 38);
            this.LblSerachTo.Name = "LblSerachTo";
            this.LblSerachTo.Size = new System.Drawing.Size(80, 13);
            this.LblSerachTo.TabIndex = 118;
            this.LblSerachTo.Text = "Customer To";
            // 
            // ChkAutoComplteTo
            // 
            this.ChkAutoComplteTo.AutoSize = true;
            this.ChkAutoComplteTo.Location = new System.Drawing.Point(99, 38);
            this.ChkAutoComplteTo.Name = "ChkAutoComplteTo";
            this.ChkAutoComplteTo.Size = new System.Drawing.Size(15, 14);
            this.ChkAutoComplteTo.TabIndex = 124;
            this.ChkAutoComplteTo.Tag = "1";
            this.ChkAutoComplteTo.UseVisualStyleBackColor = true;
            this.ChkAutoComplteTo.CheckedChanged += new System.EventHandler(this.ChkAutoComplteTo_CheckedChanged);
            // 
            // TxtSearchCodeTo
            // 
            this.TxtSearchCodeTo.Location = new System.Drawing.Point(120, 35);
            this.TxtSearchCodeTo.Name = "TxtSearchCodeTo";
            this.TxtSearchCodeTo.Size = new System.Drawing.Size(133, 21);
            this.TxtSearchCodeTo.TabIndex = 120;
            this.TxtSearchCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearchCodeTo_KeyDown);
            this.TxtSearchCodeTo.Leave += new System.EventHandler(this.TxtSearchCodeTo_Leave);
            // 
            // TxtSearchNameTo
            // 
            this.TxtSearchNameTo.Location = new System.Drawing.Point(258, 35);
            this.TxtSearchNameTo.Name = "TxtSearchNameTo";
            this.TxtSearchNameTo.Size = new System.Drawing.Size(329, 21);
            this.TxtSearchNameTo.TabIndex = 122;
            // 
            // TxtSearchNameFrom
            // 
            this.TxtSearchNameFrom.Location = new System.Drawing.Point(258, 8);
            this.TxtSearchNameFrom.Name = "TxtSearchNameFrom";
            this.TxtSearchNameFrom.Size = new System.Drawing.Size(329, 21);
            this.TxtSearchNameFrom.TabIndex = 121;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(5, 129);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(593, 10);
            this.prgBar.Step = 1;
            this.prgBar.TabIndex = 143;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // FrmCrmTypeWiseCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 182);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCrmTypeWiseCustomerDetails";
            this.Text = "FrmCrmTypeWiseCustomerDetails";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlSelection.ResumeLayout(false);
            this.pnlSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkAutoComplteTo;
        private System.Windows.Forms.CheckBox ChkAutoComplteFrom;
        private System.Windows.Forms.TextBox TxtSearchNameTo;
        private System.Windows.Forms.TextBox TxtSearchNameFrom;
        private System.Windows.Forms.TextBox TxtSearchCodeTo;
        private System.Windows.Forms.TextBox TxtSearchCodeFrom;
        private System.Windows.Forms.Label LblSerachTo;
        private System.Windows.Forms.Label LblSearchFrom;
        private System.Windows.Forms.CheckBox chkAllCostomers;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}