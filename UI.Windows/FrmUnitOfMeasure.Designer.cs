namespace UI.Windows
{
    partial class FrmUnitOfMeasure
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
            this.chkAutoCompleationUnit = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtUnitOfMeasureName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtUnitOfMeasureCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblUnitCode = new System.Windows.Forms.Label();
            this.lblUnitDescription = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 131);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(398, 131);
            // 
            // btnPrint
            // 
            this.btnPrint.TabIndex = 10;
            // 
            // btnView
            // 
            this.btnView.TabIndex = 9;
            // 
            // btnHelp
            // 
            this.btnHelp.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.TabIndex = 7;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoCompleationUnit);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtUnitOfMeasureName);
            this.groupBox1.Controls.Add(this.txtUnitOfMeasureCode);
            this.groupBox1.Controls.Add(this.lblUnitCode);
            this.groupBox1.Controls.Add(this.lblUnitDescription);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(635, 141);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(544, 118);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 13;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(283, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkAutoCompleationUnit
            // 
            this.chkAutoCompleationUnit.AutoSize = true;
            this.chkAutoCompleationUnit.Checked = true;
            this.chkAutoCompleationUnit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationUnit.Location = new System.Drawing.Point(105, 26);
            this.chkAutoCompleationUnit.Name = "chkAutoCompleationUnit";
            this.chkAutoCompleationUnit.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationUnit.TabIndex = 12;
            this.chkAutoCompleationUnit.Tag = "1";
            this.chkAutoCompleationUnit.UseVisualStyleBackColor = true;
            this.chkAutoCompleationUnit.CheckedChanged += new System.EventHandler(this.chkAutoCompleationUnit_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(123, 74);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(507, 37);
            this.txtRemark.TabIndex = 4;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 77);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtUnitOfMeasureName
            // 
            this.txtUnitOfMeasureName.Location = new System.Drawing.Point(123, 48);
            this.txtUnitOfMeasureName.MasterDescription = "";
            this.txtUnitOfMeasureName.Name = "txtUnitOfMeasureName";
            this.txtUnitOfMeasureName.Size = new System.Drawing.Size(507, 21);
            this.txtUnitOfMeasureName.TabIndex = 3;
            this.txtUnitOfMeasureName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitOfMeasureName_KeyDown);
            this.txtUnitOfMeasureName.Leave += new System.EventHandler(this.txtUnitOfMeasureName_Leave);
            this.txtUnitOfMeasureName.Validated += new System.EventHandler(this.txtUnitOfMeasureName_Validated);
            // 
            // txtUnitOfMeasureCode
            // 
            this.txtUnitOfMeasureCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnitOfMeasureCode.IsAutoComplete = false;
            this.txtUnitOfMeasureCode.ItemCollection = null;
            this.txtUnitOfMeasureCode.Location = new System.Drawing.Point(123, 23);
            this.txtUnitOfMeasureCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUnitOfMeasureCode.MasterCode = "";
            this.txtUnitOfMeasureCode.MaxLength = 25;
            this.txtUnitOfMeasureCode.Name = "txtUnitOfMeasureCode";
            this.txtUnitOfMeasureCode.Size = new System.Drawing.Size(157, 21);
            this.txtUnitOfMeasureCode.TabIndex = 1;
            this.txtUnitOfMeasureCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitOfMeasureCode_KeyDown);
            this.txtUnitOfMeasureCode.Leave += new System.EventHandler(this.txtUnitOfMeasureCode_Leave);
            // 
            // lblUnitCode
            // 
            this.lblUnitCode.AutoSize = true;
            this.lblUnitCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitCode.Location = new System.Drawing.Point(6, 26);
            this.lblUnitCode.Name = "lblUnitCode";
            this.lblUnitCode.Size = new System.Drawing.Size(70, 13);
            this.lblUnitCode.TabIndex = 9;
            this.lblUnitCode.Text = "Unit Code*";
            // 
            // lblUnitDescription
            // 
            this.lblUnitDescription.AutoSize = true;
            this.lblUnitDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitDescription.Location = new System.Drawing.Point(6, 51);
            this.lblUnitDescription.Name = "lblUnitDescription";
            this.lblUnitDescription.Size = new System.Drawing.Size(73, 13);
            this.lblUnitDescription.TabIndex = 10;
            this.lblUnitDescription.Text = "Unit Name*";
            // 
            // FrmUnitOfMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 180);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmUnitOfMeasure";
            this.Text = "Unit Of Measure";
            this.Load += new System.EventHandler(this.FrmUnitOfMeasure_Load);
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
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private CustomControls.TextBoxMasterDescription txtUnitOfMeasureName;
        private CustomControls.TextBoxMasterCode txtUnitOfMeasureCode;
        private System.Windows.Forms.Label lblUnitCode;
        private System.Windows.Forms.Label lblUnitDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationUnit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkAutoClear;
    }
}
