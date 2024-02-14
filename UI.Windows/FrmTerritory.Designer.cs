namespace UI.Windows
{
    partial class FrmTerritory
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
            this.txtAreaCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.chkAutoCompleationTerritory = new System.Windows.Forms.CheckBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationArea = new System.Windows.Forms.CheckBox();
            this.txtAreaName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTerritoryName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTerritoryCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblTerritoryName = new System.Windows.Forms.Label();
            this.lblTerritoryCode = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 112);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(399, 112);
            // 
            // btnClear
            // 
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSave_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAreaCode);
            this.groupBox1.Controls.Add(this.chkAutoCompleationTerritory);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationArea);
            this.groupBox1.Controls.Add(this.txtAreaName);
            this.groupBox1.Controls.Add(this.txtTerritoryName);
            this.groupBox1.Controls.Add(this.txtTerritoryCode);
            this.groupBox1.Controls.Add(this.lblTerritoryName);
            this.groupBox1.Controls.Add(this.lblTerritoryCode);
            this.groupBox1.Controls.Add(this.lblArea);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(635, 122);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.IsAutoComplete = false;
            this.txtAreaCode.ItemCollection = null;
            this.txtAreaCode.Location = new System.Drawing.Point(136, 19);
            this.txtAreaCode.MasterCode = "";
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(153, 21);
            this.txtAreaCode.TabIndex = 34;
            this.txtAreaCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaCode_KeyDown);
            this.txtAreaCode.Leave += new System.EventHandler(this.txtAreaCode_Leave);
            // 
            // chkAutoCompleationTerritory
            // 
            this.chkAutoCompleationTerritory.AutoSize = true;
            this.chkAutoCompleationTerritory.Checked = true;
            this.chkAutoCompleationTerritory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationTerritory.Location = new System.Drawing.Point(119, 48);
            this.chkAutoCompleationTerritory.Name = "chkAutoCompleationTerritory";
            this.chkAutoCompleationTerritory.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationTerritory.TabIndex = 33;
            this.chkAutoCompleationTerritory.Tag = "1";
            this.chkAutoCompleationTerritory.UseVisualStyleBackColor = true;
            this.chkAutoCompleationTerritory.CheckedChanged += new System.EventHandler(this.chkAutoCompleationTerritory_CheckedChanged);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(544, 98);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 32;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(292, 43);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 31;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationArea
            // 
            this.chkAutoCompleationArea.AutoSize = true;
            this.chkAutoCompleationArea.Checked = true;
            this.chkAutoCompleationArea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationArea.Location = new System.Drawing.Point(119, 22);
            this.chkAutoCompleationArea.Name = "chkAutoCompleationArea";
            this.chkAutoCompleationArea.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationArea.TabIndex = 29;
            this.chkAutoCompleationArea.Tag = "1";
            this.chkAutoCompleationArea.UseVisualStyleBackColor = true;
            this.chkAutoCompleationArea.CheckedChanged += new System.EventHandler(this.chkAutoCompleationArea_CheckedChanged);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(292, 19);
            this.txtAreaName.MasterDescription = "";
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(336, 21);
            this.txtAreaName.TabIndex = 8;
            this.txtAreaName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAreaName_KeyDown);
            this.txtAreaName.Leave += new System.EventHandler(this.txtAreaName_Leave);
            // 
            // txtTerritoryName
            // 
            this.txtTerritoryName.Location = new System.Drawing.Point(136, 69);
            this.txtTerritoryName.MasterDescription = "";
            this.txtTerritoryName.Name = "txtTerritoryName";
            this.txtTerritoryName.Size = new System.Drawing.Size(492, 21);
            this.txtTerritoryName.TabIndex = 7;
            this.txtTerritoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerritoryName_KeyDown);
            this.txtTerritoryName.Leave += new System.EventHandler(this.txtTerritoryName_Leave);
            // 
            // txtTerritoryCode
            // 
            this.txtTerritoryCode.IsAutoComplete = false;
            this.txtTerritoryCode.ItemCollection = null;
            this.txtTerritoryCode.Location = new System.Drawing.Point(136, 44);
            this.txtTerritoryCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTerritoryCode.MasterCode = "";
            this.txtTerritoryCode.MaxLength = 25;
            this.txtTerritoryCode.Name = "txtTerritoryCode";
            this.txtTerritoryCode.Size = new System.Drawing.Size(153, 21);
            this.txtTerritoryCode.TabIndex = 4;
            this.txtTerritoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerritoryCode_KeyDown);
            this.txtTerritoryCode.Leave += new System.EventHandler(this.txtTerritoryCode_Leave);
            // 
            // lblTerritoryName
            // 
            this.lblTerritoryName.AutoSize = true;
            this.lblTerritoryName.Location = new System.Drawing.Point(10, 72);
            this.lblTerritoryName.Name = "lblTerritoryName";
            this.lblTerritoryName.Size = new System.Drawing.Size(100, 13);
            this.lblTerritoryName.TabIndex = 2;
            this.lblTerritoryName.Text = "Territory Name*";
            // 
            // lblTerritoryCode
            // 
            this.lblTerritoryCode.AutoSize = true;
            this.lblTerritoryCode.Location = new System.Drawing.Point(10, 47);
            this.lblTerritoryCode.Name = "lblTerritoryCode";
            this.lblTerritoryCode.Size = new System.Drawing.Size(97, 13);
            this.lblTerritoryCode.TabIndex = 1;
            this.lblTerritoryCode.Text = "Territory Code*";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(10, 22);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(41, 13);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "Area*";
            // 
            // FrmTerritory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 161);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTerritory";
            this.Text = "Territory";
            this.Load += new System.EventHandler(this.FrmTerritory_Load);
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
        private CustomControls.TextBoxMasterDescription txtTerritoryName;
        private CustomControls.TextBoxMasterCode txtTerritoryCode;
        private System.Windows.Forms.Label lblTerritoryName;
        private System.Windows.Forms.Label lblTerritoryCode;
        private System.Windows.Forms.Label lblArea;
        private CustomControls.TextBoxMasterDescription txtAreaName;
        private System.Windows.Forms.CheckBox chkAutoCompleationArea;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoCompleationTerritory;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private CustomControls.TextBoxMasterCode txtAreaCode;
    }
}
