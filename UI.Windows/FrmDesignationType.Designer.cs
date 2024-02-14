namespace UI.Windows
{
    partial class FrmDesignationType
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
            this.grpArea = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationDesignationType = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtDesignation = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtDesignationCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblDesignationCode = new System.Windows.Forms.Label();
            this.lblDesignationName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 121);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 121);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 9;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 8;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 6;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 10;
            // 
            // grpArea
            // 
            this.grpArea.Controls.Add(this.chkAutoClear);
            this.grpArea.Controls.Add(this.btnNew);
            this.grpArea.Controls.Add(this.chkAutoCompleationDesignationType);
            this.grpArea.Controls.Add(this.txtRemark);
            this.grpArea.Controls.Add(this.lblRemark);
            this.grpArea.Controls.Add(this.txtDesignation);
            this.grpArea.Controls.Add(this.txtDesignationCode);
            this.grpArea.Controls.Add(this.lblDesignationCode);
            this.grpArea.Controls.Add(this.lblDesignationName);
            this.grpArea.Location = new System.Drawing.Point(2, -5);
            this.grpArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpArea.Name = "grpArea";
            this.grpArea.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpArea.Size = new System.Drawing.Size(635, 131);
            this.grpArea.TabIndex = 12;
            this.grpArea.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(542, 108);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 10;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(296, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationDesignationType
            // 
            this.chkAutoCompleationDesignationType.AutoSize = true;
            this.chkAutoCompleationDesignationType.Checked = true;
            this.chkAutoCompleationDesignationType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationDesignationType.Location = new System.Drawing.Point(101, 20);
            this.chkAutoCompleationDesignationType.Name = "chkAutoCompleationDesignationType";
            this.chkAutoCompleationDesignationType.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationDesignationType.TabIndex = 11;
            this.chkAutoCompleationDesignationType.Tag = "1";
            this.chkAutoCompleationDesignationType.UseVisualStyleBackColor = true;
            this.chkAutoCompleationDesignationType.CheckedChanged += new System.EventHandler(this.chkAutoCompleationDesignationType_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(138, 62);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(493, 37);
            this.txtRemark.TabIndex = 3;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 70);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 15;
            this.lblRemark.Text = "Remark";
            // 
            // txtDesignation
            // 
            this.txtDesignation.Location = new System.Drawing.Point(138, 39);
            this.txtDesignation.MasterDescription = "";
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(493, 21);
            this.txtDesignation.TabIndex = 2;
            this.txtDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDesignation_KeyDown);
            this.txtDesignation.Leave += new System.EventHandler(this.txtDesignation_Leave);
            // 
            // txtDesignationCode
            // 
            this.txtDesignationCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignationCode.IsAutoComplete = false;
            this.txtDesignationCode.ItemCollection = null;
            this.txtDesignationCode.Location = new System.Drawing.Point(138, 16);
            this.txtDesignationCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDesignationCode.MasterCode = "";
            this.txtDesignationCode.MaxLength = 25;
            this.txtDesignationCode.Name = "txtDesignationCode";
            this.txtDesignationCode.Size = new System.Drawing.Size(157, 21);
            this.txtDesignationCode.TabIndex = 0;
            this.txtDesignationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDesignationCode_KeyDown);
            this.txtDesignationCode.Leave += new System.EventHandler(this.txtDesignationCode_Leave);
            // 
            // lblDesignationCode
            // 
            this.lblDesignationCode.AutoSize = true;
            this.lblDesignationCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesignationCode.Location = new System.Drawing.Point(10, 19);
            this.lblDesignationCode.Name = "lblDesignationCode";
            this.lblDesignationCode.Size = new System.Drawing.Size(44, 13);
            this.lblDesignationCode.TabIndex = 9;
            this.lblDesignationCode.Text = "Code*";
            // 
            // lblDesignationName
            // 
            this.lblDesignationName.AutoSize = true;
            this.lblDesignationName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesignationName.Location = new System.Drawing.Point(10, 44);
            this.lblDesignationName.Name = "lblDesignationName";
            this.lblDesignationName.Size = new System.Drawing.Size(81, 13);
            this.lblDesignationName.TabIndex = 10;
            this.lblDesignationName.Text = "Designation*";
            // 
            // FrmDesignationType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 170);
            this.Controls.Add(this.grpArea);
            this.Name = "FrmDesignationType";
            this.Text = "Designation Types";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpArea, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpArea.ResumeLayout(false);
            this.grpArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpArea;
        private CustomControls.TextBoxMasterCode txtDesignationCode;
        private System.Windows.Forms.Label lblDesignationCode;
        private System.Windows.Forms.Label lblDesignationName;
        private CustomControls.TextBoxMasterDescription txtDesignation;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkAutoCompleationDesignationType;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
