namespace UI.Windows
{
    partial class FrmUserPrivileges
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
            this.chkAutoCompleationEmployee = new System.Windows.Forms.CheckBox();
            this.txtEmployeeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblEmployeeCode = new System.Windows.Forms.Label();
            this.txtEmployeeName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.cmbUserGroupName = new System.Windows.Forms.ComboBox();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnViewUserList = new UI.Windows.CustomControls.uc_GButton();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.ChkCantChgPwd = new System.Windows.Forms.CheckBox();
            this.ChkChangePassword = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvGroupPrivileges = new System.Windows.Forms.DataGridView();
            this.FormId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkPause = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkView = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvLocationInfo = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAllLocations = new System.Windows.Forms.CheckBox();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupPrivileges)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 464);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(810, 464);
            // 
            // btnPrint
            // 
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoCompleationEmployee);
            this.groupBox1.Controls.Add(this.txtEmployeeCode);
            this.groupBox1.Controls.Add(this.lblEmployeeCode);
            this.groupBox1.Controls.Add(this.txtEmployeeName);
            this.groupBox1.Controls.Add(this.lblUserGroup);
            this.groupBox1.Controls.Add(this.cmbUserGroupName);
            this.groupBox1.Controls.Add(this.cmbUserName);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.txtConfirmPassword);
            this.groupBox1.Controls.Add(this.lblConfirm);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 186);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoCompleationEmployee
            // 
            this.chkAutoCompleationEmployee.AutoSize = true;
            this.chkAutoCompleationEmployee.Checked = true;
            this.chkAutoCompleationEmployee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationEmployee.Location = new System.Drawing.Point(105, 17);
            this.chkAutoCompleationEmployee.Name = "chkAutoCompleationEmployee";
            this.chkAutoCompleationEmployee.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationEmployee.TabIndex = 54;
            this.chkAutoCompleationEmployee.Tag = "1";
            this.chkAutoCompleationEmployee.UseVisualStyleBackColor = true;
            this.chkAutoCompleationEmployee.CheckedChanged += new System.EventHandler(this.chkAutoCompleationEmployee_CheckedChanged);
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeCode.IsAutoComplete = false;
            this.txtEmployeeCode.ItemCollection = null;
            this.txtEmployeeCode.Location = new System.Drawing.Point(125, 13);
            this.txtEmployeeCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmployeeCode.MasterCode = "";
            this.txtEmployeeCode.MaxLength = 25;
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(88, 21);
            this.txtEmployeeCode.TabIndex = 52;
            this.txtEmployeeCode.Tag = "1";
            this.txtEmployeeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeCode_KeyDown);
            this.txtEmployeeCode.Leave += new System.EventHandler(this.txtEmployeeCode_Leave);
            // 
            // lblEmployeeCode
            // 
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeCode.Location = new System.Drawing.Point(11, 17);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new System.Drawing.Size(63, 13);
            this.lblEmployeeCode.TabIndex = 51;
            this.lblEmployeeCode.Text = "Employee";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(215, 13);
            this.txtEmployeeName.MasterDescription = "";
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(181, 21);
            this.txtEmployeeName.TabIndex = 53;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeName_KeyDown);
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Location = new System.Drawing.Point(11, 151);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(109, 13);
            this.lblUserGroup.TabIndex = 50;
            this.lblUserGroup.Text = "User Group Name";
            // 
            // cmbUserGroupName
            // 
            this.cmbUserGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbUserGroupName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUserGroupName.DisplayMember = "UserGroupName";
            this.cmbUserGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserGroupName.FormattingEnabled = true;
            this.cmbUserGroupName.Location = new System.Drawing.Point(125, 148);
            this.cmbUserGroupName.MaxLength = 50;
            this.cmbUserGroupName.Name = "cmbUserGroupName";
            this.cmbUserGroupName.Size = new System.Drawing.Size(271, 21);
            this.cmbUserGroupName.TabIndex = 44;
            this.cmbUserGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUserGroupName_KeyDown);
            this.cmbUserGroupName.Leave += new System.EventHandler(this.cmbUserGroupName_Leave);
            // 
            // cmbUserName
            // 
            this.cmbUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUserName.DisplayMember = "User_Name";
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(125, 41);
            this.cmbUserName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbUserName.MaxLength = 50;
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(271, 21);
            this.cmbUserName.TabIndex = 38;
            this.cmbUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUserName_KeyDown);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(125, 67);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(271, 21);
            this.txtDescription.TabIndex = 40;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(11, 70);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 18);
            this.lblDescription.TabIndex = 48;
            this.lblDescription.Text = "Description";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Enabled = false;
            this.txtConfirmPassword.Location = new System.Drawing.Point(125, 120);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfirmPassword.MaxLength = 15;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(271, 21);
            this.txtConfirmPassword.TabIndex = 43;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            this.txtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPassword_KeyDown);
            // 
            // lblConfirm
            // 
            this.lblConfirm.Location = new System.Drawing.Point(11, 123);
            this.lblConfirm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(106, 18);
            this.lblConfirm.TabIndex = 45;
            this.lblConfirm.Text = "Confirm Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(125, 94);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.MaxLength = 15;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(271, 21);
            this.txtPassword.TabIndex = 41;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(11, 97);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(85, 18);
            this.lblPassword.TabIndex = 42;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(11, 44);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(85, 18);
            this.lblUserName.TabIndex = 39;
            this.lblUserName.Text = "User Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnViewUserList);
            this.groupBox2.Controls.Add(this.chkActive);
            this.groupBox2.Controls.Add(this.ChkCantChgPwd);
            this.groupBox2.Controls.Add(this.ChkChangePassword);
            this.groupBox2.Location = new System.Drawing.Point(405, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 186);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // btnViewUserList
            // 
            this.btnViewUserList.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnViewUserList.Font = new System.Drawing.Font("Cambria", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewUserList.Location = new System.Drawing.Point(7, 146);
            this.btnViewUserList.Name = "btnViewUserList";
            this.btnViewUserList.Size = new System.Drawing.Size(98, 25);
            this.btnViewUserList.TabIndex = 53;
            this.btnViewUserList.Text = "View User List";
            this.btnViewUserList.Click += new System.EventHandler(this.btnViewUserList_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(7, 71);
            this.chkActive.Margin = new System.Windows.Forms.Padding(4);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(61, 17);
            this.chkActive.TabIndex = 52;
            this.chkActive.Tag = "1";
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // ChkCantChgPwd
            // 
            this.ChkCantChgPwd.AutoSize = true;
            this.ChkCantChgPwd.Location = new System.Drawing.Point(7, 43);
            this.ChkCantChgPwd.Margin = new System.Windows.Forms.Padding(4);
            this.ChkCantChgPwd.Name = "ChkCantChgPwd";
            this.ChkCantChgPwd.Size = new System.Drawing.Size(203, 17);
            this.ChkCantChgPwd.TabIndex = 51;
            this.ChkCantChgPwd.Text = "User Cannot Change Password";
            this.ChkCantChgPwd.UseVisualStyleBackColor = true;
            // 
            // ChkChangePassword
            // 
            this.ChkChangePassword.AutoSize = true;
            this.ChkChangePassword.Location = new System.Drawing.Point(7, 17);
            this.ChkChangePassword.Margin = new System.Windows.Forms.Padding(4);
            this.ChkChangePassword.Name = "ChkChangePassword";
            this.ChkChangePassword.Size = new System.Drawing.Size(271, 17);
            this.ChkChangePassword.TabIndex = 50;
            this.ChkChangePassword.Text = "User Must Change Password at Next Logon";
            this.ChkChangePassword.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvGroupPrivileges);
            this.groupBox3.Location = new System.Drawing.Point(2, 176);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(725, 293);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
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
            this.ChkView});
            this.dgvGroupPrivileges.Location = new System.Drawing.Point(4, 11);
            this.dgvGroupPrivileges.Name = "dgvGroupPrivileges";
            this.dgvGroupPrivileges.Size = new System.Drawing.Size(716, 277);
            this.dgvGroupPrivileges.TabIndex = 1;
            this.dgvGroupPrivileges.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroupPrivileges_CellContentClick);
            // 
            // FormId
            // 
            this.FormId.DataPropertyName = "TransactionRightsID";
            this.FormId.HeaderText = "TransactionId";
            this.FormId.Name = "FormId";
            this.FormId.Visible = false;
            // 
            // FormText
            // 
            this.FormText.DataPropertyName = "FormText";
            this.FormText.HeaderText = "Transaction/ Report Description";
            this.FormText.Name = "FormText";
            this.FormText.ReadOnly = true;
            this.FormText.Width = 283;
            // 
            // ChkAccess
            // 
            this.ChkAccess.DataPropertyName = "IsAccess";
            this.ChkAccess.HeaderText = "   Access";
            this.ChkAccess.Name = "ChkAccess";
            this.ChkAccess.Width = 73;
            // 
            // ChkPause
            // 
            this.ChkPause.DataPropertyName = "IsPause";
            this.ChkPause.HeaderText = "Pause/Edit";
            this.ChkPause.Name = "ChkPause";
            this.ChkPause.Width = 73;
            // 
            // ChkSave
            // 
            this.ChkSave.DataPropertyName = "IsSave";
            this.ChkSave.HeaderText = "    Save";
            this.ChkSave.Name = "ChkSave";
            this.ChkSave.Width = 73;
            // 
            // ChkDelete
            // 
            this.ChkDelete.DataPropertyName = "IsModify";
            this.ChkDelete.HeaderText = "  Delete";
            this.ChkDelete.Name = "ChkDelete";
            this.ChkDelete.Width = 73;
            // 
            // ChkView
            // 
            this.ChkView.DataPropertyName = "IsView";
            this.ChkView.HeaderText = "   View";
            this.ChkView.Name = "ChkView";
            this.ChkView.Width = 73;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvLocationInfo);
            this.groupBox4.Controls.Add(this.chkAllLocations);
            this.groupBox4.Location = new System.Drawing.Point(729, -5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 474);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // dgvLocationInfo
            // 
            this.dgvLocationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.Location,
            this.LocationId,
            this.TransactionId});
            this.dgvLocationInfo.Location = new System.Drawing.Point(4, 42);
            this.dgvLocationInfo.Name = "dgvLocationInfo";
            this.dgvLocationInfo.RowHeadersWidth = 5;
            this.dgvLocationInfo.Size = new System.Drawing.Size(311, 426);
            this.dgvLocationInfo.TabIndex = 2;
            this.dgvLocationInfo.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocationInfo_CellValidated);
            // 
            // Selection
            // 
            this.Selection.DataPropertyName = "IsSelect";
            this.Selection.FalseValue = "false";
            this.Selection.HeaderText = "Allow";
            this.Selection.Name = "Selection";
            this.Selection.TrueValue = "true";
            this.Selection.Width = 45;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "LocationName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 240;
            // 
            // LocationId
            // 
            this.LocationId.DataPropertyName = "LocationId";
            this.LocationId.HeaderText = "LocationId";
            this.LocationId.Name = "LocationId";
            this.LocationId.Visible = false;
            // 
            // TransactionId
            // 
            this.TransactionId.HeaderText = "TransactionId";
            this.TransactionId.Name = "TransactionId";
            this.TransactionId.Visible = false;
            // 
            // chkAllLocations
            // 
            this.chkAllLocations.AutoSize = true;
            this.chkAllLocations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllLocations.Location = new System.Drawing.Point(6, 18);
            this.chkAllLocations.Name = "chkAllLocations";
            this.chkAllLocations.Size = new System.Drawing.Size(97, 17);
            this.chkAllLocations.TabIndex = 2;
            this.chkAllLocations.Text = "All Locations";
            this.chkAllLocations.UseVisualStyleBackColor = true;
            this.chkAllLocations.CheckedChanged += new System.EventHandler(this.chkAllLocations_CheckedChanged);
            // 
            // FrmUserPrivileges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(1051, 513);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmUserPrivileges";
            this.Text = "User Privileges";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupPrivileges)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblUserGroup;
        private System.Windows.Forms.ComboBox cmbUserGroupName;
        private System.Windows.Forms.ComboBox cmbUserName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ChkCantChgPwd;
        private System.Windows.Forms.CheckBox ChkChangePassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvGroupPrivileges;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkAllLocations;
        private System.Windows.Forms.DataGridView dgvLocationInfo;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckBox chkAutoCompleationEmployee;
        private CustomControls.TextBoxMasterCode txtEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeCode;
        private CustomControls.TextBoxMasterDescription txtEmployeeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkPause;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkDelete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkView;
        private CustomControls.uc_GButton btnViewUserList;
    }
}
