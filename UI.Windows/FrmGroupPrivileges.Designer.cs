namespace UI.Windows
{
    partial class FrmGroupPrivileges
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.chkUpdateAllUser = new System.Windows.Forms.CheckBox();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.chkSelAllView = new System.Windows.Forms.CheckBox();
            this.dgvGroupPrivileges = new System.Windows.Forms.DataGridView();
            this.FormId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkPause = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkView = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ModuleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelAllDelete = new System.Windows.Forms.CheckBox();
            this.chkSelAllSave = new System.Windows.Forms.CheckBox();
            this.chkSelAllPause = new System.Windows.Forms.CheckBox();
            this.chkSelAllAccess = new System.Windows.Forms.CheckBox();
            this.chkCommon = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkHR = new System.Windows.Forms.CheckBox();
            this.chkCRM = new System.Windows.Forms.CheckBox();
            this.chkLogistic = new System.Windows.Forms.CheckBox();
            this.chkFinance = new System.Windows.Forms.CheckBox();
            this.chkPOS = new System.Windows.Forms.CheckBox();
            this.chkGV = new System.Windows.Forms.CheckBox();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.cmbUserGroupName = new System.Windows.Forms.ComboBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupPrivileges)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 413);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(475, 413);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.chkUpdateAllUser);
            this.grpHeader.Controls.Add(this.grpGrid);
            this.grpHeader.Controls.Add(this.chkCommon);
            this.grpHeader.Controls.Add(this.btnNew);
            this.grpHeader.Controls.Add(this.chkHR);
            this.grpHeader.Controls.Add(this.chkCRM);
            this.grpHeader.Controls.Add(this.chkLogistic);
            this.grpHeader.Controls.Add(this.chkFinance);
            this.grpHeader.Controls.Add(this.chkPOS);
            this.grpHeader.Controls.Add(this.chkGV);
            this.grpHeader.Controls.Add(this.chkInventory);
            this.grpHeader.Controls.Add(this.lblUserGroup);
            this.grpHeader.Controls.Add(this.cmbUserGroupName);
            this.grpHeader.Location = new System.Drawing.Point(2, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(712, 424);
            this.grpHeader.TabIndex = 10;
            this.grpHeader.TabStop = false;
            this.grpHeader.Enter += new System.EventHandler(this.grpHeader_Enter);
            // 
            // chkUpdateAllUser
            // 
            this.chkUpdateAllUser.AutoSize = true;
            this.chkUpdateAllUser.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdateAllUser.Location = new System.Drawing.Point(473, 400);
            this.chkUpdateAllUser.Name = "chkUpdateAllUser";
            this.chkUpdateAllUser.Size = new System.Drawing.Size(229, 17);
            this.chkUpdateAllUser.TabIndex = 55;
            this.chkUpdateAllUser.Text = "Update All Users In Relevent Group";
            this.chkUpdateAllUser.UseVisualStyleBackColor = true;
            this.chkUpdateAllUser.CheckedChanged += new System.EventHandler(this.chkUpdateAllUser_CheckedChanged);
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.chkSelAllView);
            this.grpGrid.Controls.Add(this.dgvGroupPrivileges);
            this.grpGrid.Controls.Add(this.chkSelAllDelete);
            this.grpGrid.Controls.Add(this.chkSelAllSave);
            this.grpGrid.Controls.Add(this.chkSelAllPause);
            this.grpGrid.Controls.Add(this.chkSelAllAccess);
            this.grpGrid.Location = new System.Drawing.Point(5, 79);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(703, 314);
            this.grpGrid.TabIndex = 24;
            this.grpGrid.TabStop = false;
            // 
            // chkSelAllView
            // 
            this.chkSelAllView.AutoSize = true;
            this.chkSelAllView.Checked = true;
            this.chkSelAllView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelAllView.Location = new System.Drawing.Point(635, 10);
            this.chkSelAllView.Name = "chkSelAllView";
            this.chkSelAllView.Size = new System.Drawing.Size(15, 14);
            this.chkSelAllView.TabIndex = 24;
            this.chkSelAllView.UseVisualStyleBackColor = true;
            this.chkSelAllView.CheckedChanged += new System.EventHandler(this.chkSelAllView_CheckedChanged);
            // 
            // dgvGroupPrivileges
            // 
            this.dgvGroupPrivileges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupPrivileges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FormId,
            this.FormText,
            this.ChkAccess,
            this.ChkPause,
            this.ChkSave,
            this.ChkDelete,
            this.ChkView,
            this.ModuleType});
            this.dgvGroupPrivileges.Location = new System.Drawing.Point(6, 26);
            this.dgvGroupPrivileges.Name = "dgvGroupPrivileges";
            this.dgvGroupPrivileges.Size = new System.Drawing.Size(691, 284);
            this.dgvGroupPrivileges.TabIndex = 0;
            this.dgvGroupPrivileges.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvGroupPrivileges_RowPrePaint);
            // 
            // FormId
            // 
            this.FormId.DataPropertyName = "FormID";
            this.FormId.HeaderText = "FormId";
            this.FormId.Name = "FormId";
            this.FormId.Visible = false;
            // 
            // FormText
            // 
            this.FormText.DataPropertyName = "FormText";
            this.FormText.HeaderText = "Transaction/ Report Group Description";
            this.FormText.Name = "FormText";
            this.FormText.ReadOnly = true;
            this.FormText.Width = 325;
            // 
            // ChkAccess
            // 
            this.ChkAccess.DataPropertyName = "IsAccess";
            this.ChkAccess.HeaderText = "Access";
            this.ChkAccess.Name = "ChkAccess";
            this.ChkAccess.Width = 60;
            // 
            // ChkPause
            // 
            this.ChkPause.DataPropertyName = "IsPause";
            this.ChkPause.HeaderText = "Pause";
            this.ChkPause.Name = "ChkPause";
            this.ChkPause.Width = 60;
            // 
            // ChkSave
            // 
            this.ChkSave.DataPropertyName = "IsSave";
            this.ChkSave.HeaderText = "Save";
            this.ChkSave.Name = "ChkSave";
            this.ChkSave.Width = 60;
            // 
            // ChkDelete
            // 
            this.ChkDelete.DataPropertyName = "IsModify";
            this.ChkDelete.HeaderText = "Delete";
            this.ChkDelete.Name = "ChkDelete";
            this.ChkDelete.Width = 60;
            // 
            // ChkView
            // 
            this.ChkView.DataPropertyName = "IsView";
            this.ChkView.HeaderText = "  View";
            this.ChkView.Name = "ChkView";
            this.ChkView.Width = 60;
            // 
            // ModuleType
            // 
            this.ModuleType.DataPropertyName = "ModuleType";
            this.ModuleType.HeaderText = "TransactionTypeId";
            this.ModuleType.Name = "ModuleType";
            this.ModuleType.Visible = false;
            // 
            // chkSelAllDelete
            // 
            this.chkSelAllDelete.AutoSize = true;
            this.chkSelAllDelete.Checked = true;
            this.chkSelAllDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelAllDelete.Location = new System.Drawing.Point(575, 10);
            this.chkSelAllDelete.Name = "chkSelAllDelete";
            this.chkSelAllDelete.Size = new System.Drawing.Size(15, 14);
            this.chkSelAllDelete.TabIndex = 23;
            this.chkSelAllDelete.UseVisualStyleBackColor = true;
            this.chkSelAllDelete.CheckedChanged += new System.EventHandler(this.chkSelAllDelete_CheckedChanged);
            // 
            // chkSelAllSave
            // 
            this.chkSelAllSave.AutoSize = true;
            this.chkSelAllSave.Checked = true;
            this.chkSelAllSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelAllSave.Location = new System.Drawing.Point(516, 10);
            this.chkSelAllSave.Name = "chkSelAllSave";
            this.chkSelAllSave.Size = new System.Drawing.Size(15, 14);
            this.chkSelAllSave.TabIndex = 23;
            this.chkSelAllSave.UseVisualStyleBackColor = true;
            this.chkSelAllSave.CheckedChanged += new System.EventHandler(this.chkSelAllSave_CheckedChanged);
            // 
            // chkSelAllPause
            // 
            this.chkSelAllPause.AutoSize = true;
            this.chkSelAllPause.Checked = true;
            this.chkSelAllPause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelAllPause.Location = new System.Drawing.Point(456, 10);
            this.chkSelAllPause.Name = "chkSelAllPause";
            this.chkSelAllPause.Size = new System.Drawing.Size(15, 14);
            this.chkSelAllPause.TabIndex = 23;
            this.chkSelAllPause.UseVisualStyleBackColor = true;
            this.chkSelAllPause.CheckedChanged += new System.EventHandler(this.chkSelAllPause_CheckedChanged);
            // 
            // chkSelAllAccess
            // 
            this.chkSelAllAccess.AutoSize = true;
            this.chkSelAllAccess.Checked = true;
            this.chkSelAllAccess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelAllAccess.Location = new System.Drawing.Point(395, 10);
            this.chkSelAllAccess.Name = "chkSelAllAccess";
            this.chkSelAllAccess.Size = new System.Drawing.Size(15, 14);
            this.chkSelAllAccess.TabIndex = 23;
            this.chkSelAllAccess.UseVisualStyleBackColor = true;
            this.chkSelAllAccess.CheckedChanged += new System.EventHandler(this.chkSelAllAccess_CheckedChanged);
            // 
            // chkCommon
            // 
            this.chkCommon.AutoSize = true;
            this.chkCommon.Enabled = false;
            this.chkCommon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkCommon.Location = new System.Drawing.Point(120, 39);
            this.chkCommon.Name = "chkCommon";
            this.chkCommon.Size = new System.Drawing.Size(78, 17);
            this.chkCommon.TabIndex = 22;
            this.chkCommon.Text = "Common";
            this.chkCommon.UseVisualStyleBackColor = true;
            this.chkCommon.CheckedChanged += new System.EventHandler(this.chkCommon_CheckedChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(464, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(67, 23);
            this.btnNew.TabIndex = 21;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkHR
            // 
            this.chkHR.AutoSize = true;
            this.chkHR.Enabled = false;
            this.chkHR.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkHR.Location = new System.Drawing.Point(451, 62);
            this.chkHR.Name = "chkHR";
            this.chkHR.Size = new System.Drawing.Size(97, 17);
            this.chkHR.TabIndex = 20;
            this.chkHR.Text = "HR && Payroll";
            this.chkHR.UseVisualStyleBackColor = true;
            this.chkHR.Visible = false;
            this.chkHR.CheckedChanged += new System.EventHandler(this.chkHR_CheckedChanged);
            // 
            // chkCRM
            // 
            this.chkCRM.AutoSize = true;
            this.chkCRM.Enabled = false;
            this.chkCRM.ForeColor = System.Drawing.Color.Olive;
            this.chkCRM.Location = new System.Drawing.Point(451, 39);
            this.chkCRM.Name = "chkCRM";
            this.chkCRM.Size = new System.Drawing.Size(52, 17);
            this.chkCRM.TabIndex = 19;
            this.chkCRM.Text = "CRM";
            this.chkCRM.UseVisualStyleBackColor = true;
            this.chkCRM.CheckedChanged += new System.EventHandler(this.chkCRM_CheckedChanged);
            // 
            // chkLogistic
            // 
            this.chkLogistic.AutoSize = true;
            this.chkLogistic.Enabled = false;
            this.chkLogistic.ForeColor = System.Drawing.Color.Teal;
            this.chkLogistic.Location = new System.Drawing.Point(355, 39);
            this.chkLogistic.Name = "chkLogistic";
            this.chkLogistic.Size = new System.Drawing.Size(68, 17);
            this.chkLogistic.TabIndex = 18;
            this.chkLogistic.Text = "Logistic";
            this.chkLogistic.UseVisualStyleBackColor = true;
            this.chkLogistic.CheckedChanged += new System.EventHandler(this.chkLogistic_CheckedChanged);
            // 
            // chkFinance
            // 
            this.chkFinance.AutoSize = true;
            this.chkFinance.Enabled = false;
            this.chkFinance.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkFinance.Location = new System.Drawing.Point(120, 62);
            this.chkFinance.Name = "chkFinance";
            this.chkFinance.Size = new System.Drawing.Size(77, 17);
            this.chkFinance.TabIndex = 17;
            this.chkFinance.Text = "Accounts";
            this.chkFinance.UseVisualStyleBackColor = true;
            this.chkFinance.CheckedChanged += new System.EventHandler(this.chkFinance_CheckedChanged);
            // 
            // chkPOS
            // 
            this.chkPOS.AutoSize = true;
            this.chkPOS.Enabled = false;
            this.chkPOS.ForeColor = System.Drawing.Color.Purple;
            this.chkPOS.Location = new System.Drawing.Point(355, 62);
            this.chkPOS.Name = "chkPOS";
            this.chkPOS.Size = new System.Drawing.Size(50, 17);
            this.chkPOS.TabIndex = 16;
            this.chkPOS.Text = "POS";
            this.chkPOS.UseVisualStyleBackColor = true;
            this.chkPOS.CheckedChanged += new System.EventHandler(this.chkPOS_CheckedChanged);
            // 
            // chkGV
            // 
            this.chkGV.AutoSize = true;
            this.chkGV.Enabled = false;
            this.chkGV.ForeColor = System.Drawing.Color.SaddleBrown;
            this.chkGV.Location = new System.Drawing.Point(209, 62);
            this.chkGV.Name = "chkGV";
            this.chkGV.Size = new System.Drawing.Size(96, 17);
            this.chkGV.TabIndex = 15;
            this.chkGV.Text = "Gift Voucher";
            this.chkGV.UseVisualStyleBackColor = true;
            this.chkGV.CheckedChanged += new System.EventHandler(this.chkGV_CheckedChanged);
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Enabled = false;
            this.chkInventory.ForeColor = System.Drawing.Color.Blue;
            this.chkInventory.Location = new System.Drawing.Point(209, 39);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(129, 17);
            this.chkInventory.TabIndex = 14;
            this.chkInventory.Text = "Inventory && Sales";
            this.chkInventory.UseVisualStyleBackColor = true;
            this.chkInventory.CheckedChanged += new System.EventHandler(this.chkInventory_CheckedChanged);
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Location = new System.Drawing.Point(6, 17);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(109, 13);
            this.lblUserGroup.TabIndex = 13;
            this.lblUserGroup.Text = "User Group Name";
            // 
            // cmbUserGroupName
            // 
            this.cmbUserGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbUserGroupName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUserGroupName.DisplayMember = "UserGroupName";
            this.cmbUserGroupName.FormattingEnabled = true;
            this.cmbUserGroupName.Location = new System.Drawing.Point(120, 13);
            this.cmbUserGroupName.MaxLength = 50;
            this.cmbUserGroupName.Name = "cmbUserGroupName";
            this.cmbUserGroupName.Size = new System.Drawing.Size(342, 21);
            this.cmbUserGroupName.TabIndex = 12;
            this.cmbUserGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUserGroupName_KeyDown);
            // 
            // FrmGroupPrivileges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 462);
            this.Controls.Add(this.grpHeader);
            this.Name = "FrmGroupPrivileges";
            this.Text = "Group Privileges";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            this.grpGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupPrivileges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.DataGridView dgvGroupPrivileges;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.Label lblUserGroup;
        private System.Windows.Forms.ComboBox cmbUserGroupName;
        private System.Windows.Forms.CheckBox chkGV;
        private System.Windows.Forms.CheckBox chkFinance;
        private System.Windows.Forms.CheckBox chkPOS;
        private System.Windows.Forms.CheckBox chkCRM;
        private System.Windows.Forms.CheckBox chkLogistic;
        private System.Windows.Forms.CheckBox chkHR;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkCommon;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.CheckBox chkSelAllAccess;
        private System.Windows.Forms.CheckBox chkSelAllDelete;
        private System.Windows.Forms.CheckBox chkSelAllSave;
        private System.Windows.Forms.CheckBox chkSelAllPause;
        private System.Windows.Forms.CheckBox chkSelAllView;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkPause;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkDelete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleType;
        private System.Windows.Forms.CheckBox chkUpdateAllUser;
    }
}
