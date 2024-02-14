namespace UI.Windows
{
    partial class FrmCostCentre
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
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtCostCentreCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationCostCentre = new System.Windows.Forms.CheckBox();
            this.txtCostCentreName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblCostCentreCode = new System.Windows.Forms.Label();
            this.lblCostCentreName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 86);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 86);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.txtCostCentreCode);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationCostCentre);
            this.groupBox1.Controls.Add(this.txtCostCentreName);
            this.groupBox1.Controls.Add(this.lblCostCentreCode);
            this.groupBox1.Controls.Add(this.lblCostCentreName);
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(636, 97);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(546, 76);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 15;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtCostCentreCode
            // 
            this.txtCostCentreCode.IsAutoComplete = false;
            this.txtCostCentreCode.ItemCollection = null;
            this.txtCostCentreCode.Location = new System.Drawing.Point(172, 23);
            this.txtCostCentreCode.MasterCode = "";
            this.txtCostCentreCode.Name = "txtCostCentreCode";
            this.txtCostCentreCode.Size = new System.Drawing.Size(124, 21);
            this.txtCostCentreCode.TabIndex = 0;
            this.txtCostCentreCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostCentreCode_KeyDown);
            this.txtCostCentreCode.Leave += new System.EventHandler(this.txtCostCentreCode_Leave);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(298, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationCostCentre
            // 
            this.chkAutoCompleationCostCentre.AutoSize = true;
            this.chkAutoCompleationCostCentre.Checked = true;
            this.chkAutoCompleationCostCentre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationCostCentre.Location = new System.Drawing.Point(149, 26);
            this.chkAutoCompleationCostCentre.Name = "chkAutoCompleationCostCentre";
            this.chkAutoCompleationCostCentre.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationCostCentre.TabIndex = 14;
            this.chkAutoCompleationCostCentre.Tag = "1";
            this.chkAutoCompleationCostCentre.UseVisualStyleBackColor = true;
            this.chkAutoCompleationCostCentre.CheckedChanged += new System.EventHandler(this.chkAutoCompleationCostCentre_CheckedChanged);
            // 
            // txtCostCentreName
            // 
            this.txtCostCentreName.Location = new System.Drawing.Point(172, 49);
            this.txtCostCentreName.MasterDescription = "";
            this.txtCostCentreName.Name = "txtCostCentreName";
            this.txtCostCentreName.Size = new System.Drawing.Size(458, 21);
            this.txtCostCentreName.TabIndex = 2;
            this.txtCostCentreName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostCentreName_KeyDown);
            // 
            // lblCostCentreCode
            // 
            this.lblCostCentreCode.AutoSize = true;
            this.lblCostCentreCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostCentreCode.Location = new System.Drawing.Point(10, 26);
            this.lblCostCentreCode.Name = "lblCostCentreCode";
            this.lblCostCentreCode.Size = new System.Drawing.Size(117, 13);
            this.lblCostCentreCode.TabIndex = 9;
            this.lblCostCentreCode.Text = "Cost Centre Code*";
            // 
            // lblCostCentreName
            // 
            this.lblCostCentreName.AutoSize = true;
            this.lblCostCentreName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostCentreName.Location = new System.Drawing.Point(10, 52);
            this.lblCostCentreName.Name = "lblCostCentreName";
            this.lblCostCentreName.Size = new System.Drawing.Size(120, 13);
            this.lblCostCentreName.TabIndex = 10;
            this.lblCostCentreName.Text = "Cost Centre Name*";
            // 
            // FrmCostCentre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(639, 135);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCostCentre";
            this.Text = "Cost Centre";
            this.Load += new System.EventHandler(this.FrmCostCentre_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxMasterCode txtCostCentreCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationCostCentre;
        private CustomControls.TextBoxMasterDescription txtCostCentreName;
        private System.Windows.Forms.Label lblCostCentreCode;
        private System.Windows.Forms.Label lblCostCentreName;
    }
}
