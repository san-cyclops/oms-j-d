namespace UI.Windows
{
    partial class FrmTypes
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
            this.btnNew = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtTypeDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtTypeCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblTypeCode = new System.Windows.Forms.Label();
            this.lblTypeDescription = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 160);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(400, 160);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtTypeDescription);
            this.groupBox1.Controls.Add(this.txtTypeCode);
            this.groupBox1.Controls.Add(this.lblTypeCode);
            this.groupBox1.Controls.Add(this.lblTypeDescription);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(636, 170);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(547, 147);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 30;
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(266, 49);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 29;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(106, 24);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(157, 21);
            this.cmbType.TabIndex = 16;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(6, 27);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(41, 13);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "Type*";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(106, 101);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(527, 37);
            this.txtRemark.TabIndex = 14;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 104);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtTypeDescription
            // 
            this.txtTypeDescription.Location = new System.Drawing.Point(106, 75);
            this.txtTypeDescription.MasterDescription = "";
            this.txtTypeDescription.Name = "txtTypeDescription";
            this.txtTypeDescription.Size = new System.Drawing.Size(527, 21);
            this.txtTypeDescription.TabIndex = 12;
            // 
            // txtTypeCode
            // 
            this.txtTypeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTypeCode.IsAutoComplete = false;
            this.txtTypeCode.ItemCollection = null;
            this.txtTypeCode.Location = new System.Drawing.Point(106, 50);
            this.txtTypeCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTypeCode.MasterCode = "";
            this.txtTypeCode.MaxLength = 25;
            this.txtTypeCode.Name = "txtTypeCode";
            this.txtTypeCode.Size = new System.Drawing.Size(157, 21);
            this.txtTypeCode.TabIndex = 11;
            // 
            // lblTypeCode
            // 
            this.lblTypeCode.AutoSize = true;
            this.lblTypeCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeCode.Location = new System.Drawing.Point(6, 53);
            this.lblTypeCode.Name = "lblTypeCode";
            this.lblTypeCode.Size = new System.Drawing.Size(75, 13);
            this.lblTypeCode.TabIndex = 9;
            this.lblTypeCode.Text = "Type Code*";
            // 
            // lblTypeDescription
            // 
            this.lblTypeDescription.AutoSize = true;
            this.lblTypeDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeDescription.Location = new System.Drawing.Point(6, 78);
            this.lblTypeDescription.Name = "lblTypeDescription";
            this.lblTypeDescription.Size = new System.Drawing.Size(78, 13);
            this.lblTypeDescription.TabIndex = 10;
            this.lblTypeDescription.Text = "Type Name*";
            // 
            // FrmTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(641, 209);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTypes";
            this.Text = "Types";
            this.Load += new System.EventHandler(this.FrmTypes_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtTypeDescription;
        private CustomControls.TextBoxMasterCode txtTypeCode;
        private System.Windows.Forms.Label lblTypeCode;
        private System.Windows.Forms.Label lblTypeDescription;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
