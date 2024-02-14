namespace UI.Windows
{
    partial class FrmArea
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
            this.chkAutoCompleationAreaCode = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtAreaName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtAreaCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblAreaCode = new System.Windows.Forms.Label();
            this.lblAreaName = new System.Windows.Forms.Label();
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
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 5;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 4;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 6;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grpArea
            // 
            this.grpArea.Controls.Add(this.chkAutoClear);
            this.grpArea.Controls.Add(this.btnNew);
            this.grpArea.Controls.Add(this.chkAutoCompleationAreaCode);
            this.grpArea.Controls.Add(this.txtRemark);
            this.grpArea.Controls.Add(this.lblRemark);
            this.grpArea.Controls.Add(this.txtAreaName);
            this.grpArea.Controls.Add(this.txtAreaCode);
            this.grpArea.Controls.Add(this.lblAreaCode);
            this.grpArea.Controls.Add(this.lblAreaName);
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
            // chkAutoCompleationAreaCode
            // 
            this.chkAutoCompleationAreaCode.AutoSize = true;
            this.chkAutoCompleationAreaCode.Checked = true;
            this.chkAutoCompleationAreaCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationAreaCode.Location = new System.Drawing.Point(101, 20);
            this.chkAutoCompleationAreaCode.Name = "chkAutoCompleationAreaCode";
            this.chkAutoCompleationAreaCode.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationAreaCode.TabIndex = 11;
            this.chkAutoCompleationAreaCode.Tag = "1";
            this.chkAutoCompleationAreaCode.UseVisualStyleBackColor = true;
            this.chkAutoCompleationAreaCode.CheckedChanged += new System.EventHandler(this.chkAutoCompleationArea_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(138, 62);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(491, 37);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            this.txtRemark.Leave += new System.EventHandler(this.txtRemark_Leave);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(10, 65);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 15;
            this.lblRemark.Text = "Remark";
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(138, 39);
            this.txtAreaName.MasterDescription = "";
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(491, 21);
            this.txtAreaName.TabIndex = 2;
            this.txtAreaName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaName_KeyDown);
            this.txtAreaName.Leave += new System.EventHandler(this.txtAreaName_Leave);
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreaCode.IsAutoComplete = false;
            this.txtAreaCode.ItemCollection = null;
            this.txtAreaCode.Location = new System.Drawing.Point(138, 16);
            this.txtAreaCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAreaCode.MasterCode = "";
            this.txtAreaCode.MaxLength = 25;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(157, 21);
            this.txtAreaCode.TabIndex = 0;
            this.txtAreaCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaCode_KeyDown);
            this.txtAreaCode.Leave += new System.EventHandler(this.txtAreaCode_Leave);
            // 
            // lblAreaCode
            // 
            this.lblAreaCode.AutoSize = true;
            this.lblAreaCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaCode.Location = new System.Drawing.Point(10, 19);
            this.lblAreaCode.Name = "lblAreaCode";
            this.lblAreaCode.Size = new System.Drawing.Size(75, 13);
            this.lblAreaCode.TabIndex = 9;
            this.lblAreaCode.Text = "Area Code*";
            // 
            // lblAreaName
            // 
            this.lblAreaName.AutoSize = true;
            this.lblAreaName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaName.Location = new System.Drawing.Point(10, 42);
            this.lblAreaName.Name = "lblAreaName";
            this.lblAreaName.Size = new System.Drawing.Size(78, 13);
            this.lblAreaName.TabIndex = 10;
            this.lblAreaName.Text = "Area Name*";
            // 
            // FrmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 170);
            this.Controls.Add(this.grpArea);
            this.Name = "FrmArea";
            this.Text = "Area";
            this.Load += new System.EventHandler(this.FrmArea_Load);
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
        private CustomControls.TextBoxMasterCode txtAreaCode;
        private System.Windows.Forms.Label lblAreaCode;
        private System.Windows.Forms.Label lblAreaName;
        private CustomControls.TextBoxMasterDescription txtAreaName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.CheckBox chkAutoCompleationAreaCode;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
