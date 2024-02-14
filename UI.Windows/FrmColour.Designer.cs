namespace UI.Windows
{
    partial class FrmColour
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
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStandardColourName = new System.Windows.Forms.TextBox();
            this.txtStandardColourCode = new System.Windows.Forms.TextBox();
            this.lblClickMe = new System.Windows.Forms.Label();
            this.lblStandardColour = new System.Windows.Forms.Label();
            this.txtColourName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtColourCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblColourCode = new System.Windows.Forms.Label();
            this.lblColourName = new System.Windows.Forms.Label();
            this.picSampleColour = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSampleColour)).BeginInit();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 160);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(425, 160);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtStandardColourName);
            this.groupBox1.Controls.Add(this.txtStandardColourCode);
            this.groupBox1.Controls.Add(this.lblClickMe);
            this.groupBox1.Controls.Add(this.lblStandardColour);
            this.groupBox1.Controls.Add(this.txtColourName);
            this.groupBox1.Controls.Add(this.txtColourCode);
            this.groupBox1.Controls.Add(this.lblColourCode);
            this.groupBox1.Controls.Add(this.lblColourName);
            this.groupBox1.Controls.Add(this.picSampleColour);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(662, 170);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(280, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 31;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(570, 145);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 30;
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Sample Colour Picker";
            // 
            // txtStandardColourName
            // 
            this.txtStandardColourName.Location = new System.Drawing.Point(280, 75);
            this.txtStandardColourName.Name = "txtStandardColourName";
            this.txtStandardColourName.Size = new System.Drawing.Size(378, 21);
            this.txtStandardColourName.TabIndex = 27;
            // 
            // txtStandardColourCode
            // 
            this.txtStandardColourCode.Location = new System.Drawing.Point(156, 75);
            this.txtStandardColourCode.Name = "txtStandardColourCode";
            this.txtStandardColourCode.Size = new System.Drawing.Size(123, 21);
            this.txtStandardColourCode.TabIndex = 26;
            // 
            // lblClickMe
            // 
            this.lblClickMe.AutoSize = true;
            this.lblClickMe.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickMe.Location = new System.Drawing.Point(166, 127);
            this.lblClickMe.Name = "lblClickMe";
            this.lblClickMe.Size = new System.Drawing.Size(46, 13);
            this.lblClickMe.TabIndex = 25;
            this.lblClickMe.Text = "Palette";
            // 
            // lblStandardColour
            // 
            this.lblStandardColour.AutoSize = true;
            this.lblStandardColour.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStandardColour.Location = new System.Drawing.Point(10, 78);
            this.lblStandardColour.Name = "lblStandardColour";
            this.lblStandardColour.Size = new System.Drawing.Size(108, 13);
            this.lblStandardColour.TabIndex = 21;
            this.lblStandardColour.Text = "Standard Colour*";
            // 
            // txtColourName
            // 
            this.txtColourName.Location = new System.Drawing.Point(156, 49);
            this.txtColourName.MasterDescription = "";
            this.txtColourName.Name = "txtColourName";
            this.txtColourName.Size = new System.Drawing.Size(502, 21);
            this.txtColourName.TabIndex = 16;
            // 
            // txtColourCode
            // 
            this.txtColourCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColourCode.IsAutoComplete = false;
            this.txtColourCode.ItemCollection = null;
            this.txtColourCode.Location = new System.Drawing.Point(156, 23);
            this.txtColourCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtColourCode.MasterCode = "";
            this.txtColourCode.MaxLength = 25;
            this.txtColourCode.Name = "txtColourCode";
            this.txtColourCode.Size = new System.Drawing.Size(123, 21);
            this.txtColourCode.TabIndex = 15;
            // 
            // lblColourCode
            // 
            this.lblColourCode.AutoSize = true;
            this.lblColourCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColourCode.Location = new System.Drawing.Point(10, 26);
            this.lblColourCode.Name = "lblColourCode";
            this.lblColourCode.Size = new System.Drawing.Size(86, 13);
            this.lblColourCode.TabIndex = 13;
            this.lblColourCode.Text = "Colour Code*";
            // 
            // lblColourName
            // 
            this.lblColourName.AutoSize = true;
            this.lblColourName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColourName.Location = new System.Drawing.Point(10, 52);
            this.lblColourName.Name = "lblColourName";
            this.lblColourName.Size = new System.Drawing.Size(89, 13);
            this.lblColourName.TabIndex = 14;
            this.lblColourName.Text = "Colour Name*";
            // 
            // picSampleColour
            // 
            this.picSampleColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSampleColour.Location = new System.Drawing.Point(156, 104);
            this.picSampleColour.Name = "picSampleColour";
            this.picSampleColour.Size = new System.Drawing.Size(66, 58);
            this.picSampleColour.TabIndex = 19;
            this.picSampleColour.TabStop = false;
            this.picSampleColour.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            // 
            // FrmColour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(666, 209);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmColour";
            this.Text = "Colour";
            this.Load += new System.EventHandler(this.FrmColour_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSampleColour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.TextBoxMasterDescription txtColourName;
        private CustomControls.TextBoxMasterCode txtColourCode;
        private System.Windows.Forms.Label lblColourCode;
        private System.Windows.Forms.Label lblColourName;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.PictureBox picSampleColour;
        private System.Windows.Forms.Label lblStandardColour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStandardColourName;
        private System.Windows.Forms.TextBox txtStandardColourCode;
        private System.Windows.Forms.Label lblClickMe;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Button btnNew;
    }
}
