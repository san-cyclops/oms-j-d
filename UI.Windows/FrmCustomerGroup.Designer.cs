namespace UI.Windows
{
    partial class FrmCustomerGroup
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
            this.grpGroup = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationGroup = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtGroupName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtGroupCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblGroupCode = new System.Windows.Forms.Label();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 121);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(399, 121);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpGroup
            // 
            this.grpGroup.Controls.Add(this.chkAutoClear);
            this.grpGroup.Controls.Add(this.btnNew);
            this.grpGroup.Controls.Add(this.chkAutoCompleationGroup);
            this.grpGroup.Controls.Add(this.txtRemark);
            this.grpGroup.Controls.Add(this.lblRemark);
            this.grpGroup.Controls.Add(this.txtGroupName);
            this.grpGroup.Controls.Add(this.txtGroupCode);
            this.grpGroup.Controls.Add(this.lblGroupCode);
            this.grpGroup.Controls.Add(this.lblGroupName);
            this.grpGroup.Location = new System.Drawing.Point(2, -5);
            this.grpGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpGroup.Name = "grpGroup";
            this.grpGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpGroup.Size = new System.Drawing.Size(636, 131);
            this.grpGroup.TabIndex = 12;
            this.grpGroup.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(543, 110);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 32;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(291, 17);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 30;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationGroup
            // 
            this.chkAutoCompleationGroup.AutoSize = true;
            this.chkAutoCompleationGroup.Checked = true;
            this.chkAutoCompleationGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationGroup.Location = new System.Drawing.Point(115, 21);
            this.chkAutoCompleationGroup.Name = "chkAutoCompleationGroup";
            this.chkAutoCompleationGroup.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationGroup.TabIndex = 29;
            this.chkAutoCompleationGroup.Tag = "1";
            this.chkAutoCompleationGroup.UseVisualStyleBackColor = true;
            this.chkAutoCompleationGroup.CheckedChanged += new System.EventHandler(this.chkAutoCompleationGroup_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(132, 66);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(500, 37);
            this.txtRemark.TabIndex = 14;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 72);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(132, 42);
            this.txtGroupName.MasterDescription = "";
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(500, 21);
            this.txtGroupName.TabIndex = 12;
            this.txtGroupName.Tag = "";
            this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroupCode.IsAutoComplete = false;
            this.txtGroupCode.ItemCollection = null;
            this.txtGroupCode.Location = new System.Drawing.Point(132, 18);
            this.txtGroupCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGroupCode.MasterCode = "";
            this.txtGroupCode.MaxLength = 25;
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(157, 21);
            this.txtGroupCode.TabIndex = 0;
            this.txtGroupCode.Tag = "";
            this.txtGroupCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupCode_KeyDown);
            this.txtGroupCode.Leave += new System.EventHandler(this.txtGroupCode_Leave);
            // 
            // lblGroupCode
            // 
            this.lblGroupCode.AutoSize = true;
            this.lblGroupCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupCode.Location = new System.Drawing.Point(6, 21);
            this.lblGroupCode.Name = "lblGroupCode";
            this.lblGroupCode.Size = new System.Drawing.Size(83, 13);
            this.lblGroupCode.TabIndex = 9;
            this.lblGroupCode.Text = "Group Code*";
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupName.Location = new System.Drawing.Point(6, 46);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(86, 13);
            this.lblGroupName.TabIndex = 10;
            this.lblGroupName.Text = "Group Name*";
            // 
            // FrmCustomerGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 170);
            this.Controls.Add(this.grpGroup);
            this.Name = "FrmCustomerGroup";
            this.Text = "Customer Group";
            this.Load += new System.EventHandler(this.FrmCustomerGroup_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpGroup, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpGroup.ResumeLayout(false);
            this.grpGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGroup;
        private System.Windows.Forms.CheckBox chkAutoCompleationGroup;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtGroupName;
        private CustomControls.TextBoxMasterCode txtGroupCode;
        private System.Windows.Forms.Label lblGroupCode;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
