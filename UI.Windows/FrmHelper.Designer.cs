namespace UI.Windows
{
    partial class FrmHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelper));
            this.tabHelper = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress3 = new System.Windows.Forms.Label();
            this.lblAddress2 = new System.Windows.Forms.Label();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.picHelper = new System.Windows.Forms.PictureBox();
            this.grpHelper = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkAutoCompleationHelper = new System.Windows.Forms.CheckBox();
            this.txtHelperDescription = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtHelperCode = new UI.Windows.CustomControls.TextBoxMasterCode();
            this.lblHelperCode = new System.Windows.Forms.Label();
            this.lblHelperDescription = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabHelper.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelper)).BeginInit();
            this.grpHelper.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 274);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(561, 274);
            // 
            // tabHelper
            // 
            this.tabHelper.Controls.Add(this.tabPage4);
            this.tabHelper.Controls.Add(this.tabPage1);
            this.tabHelper.Controls.Add(this.tabPage5);
            this.tabHelper.Location = new System.Drawing.Point(3, 55);
            this.tabHelper.Name = "tabHelper";
            this.tabHelper.SelectedIndex = 0;
            this.tabHelper.Size = new System.Drawing.Size(661, 173);
            this.tabHelper.TabIndex = 27;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.cmbGender);
            this.tabPage4.Controls.Add(this.lblGender);
            this.tabPage4.Controls.Add(this.lblReferenceNo);
            this.tabPage4.Controls.Add(this.txtReferenceNo);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(653, 147);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(343, 41);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(267, 13);
            this.label34.TabIndex = 36;
            this.label34.Text = "(NIC/ Passport/ Driving License/ Business/...)";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(101, 13);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(101, 21);
            this.cmbGender.TabIndex = 35;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(6, 16);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(56, 13);
            this.lblGender.TabIndex = 34;
            this.lblGender.Text = "Gender*";
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 41);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 33;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(101, 38);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(240, 21);
            this.txtReferenceNo.TabIndex = 32;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblMobile);
            this.tabPage1.Controls.Add(this.txtEmail);
            this.tabPage1.Controls.Add(this.txtMobile);
            this.tabPage1.Controls.Add(this.txtTelephone);
            this.tabPage1.Controls.Add(this.txtAddress3);
            this.tabPage1.Controls.Add(this.txtAddress2);
            this.tabPage1.Controls.Add(this.txtAddress1);
            this.tabPage1.Controls.Add(this.lblTelephone);
            this.tabPage1.Controls.Add(this.lblEmail);
            this.tabPage1.Controls.Add(this.lblAddress3);
            this.tabPage1.Controls.Add(this.lblAddress2);
            this.tabPage1.Controls.Add(this.lblAddress1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(653, 147);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Contact Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(323, 67);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(43, 13);
            this.lblMobile.TabIndex = 14;
            this.lblMobile.Text = "Mobile";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(394, 13);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(253, 21);
            this.txtEmail.TabIndex = 19;
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(394, 64);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(107, 21);
            this.txtMobile.TabIndex = 13;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(394, 38);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(253, 21);
            this.txtTelephone.TabIndex = 17;
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(113, 64);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(203, 21);
            this.txtAddress3.TabIndex = 16;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(113, 38);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(203, 21);
            this.txtAddress2.TabIndex = 15;
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(113, 13);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(204, 21);
            this.txtAddress1.TabIndex = 14;
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(323, 41);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(65, 13);
            this.lblTelephone.TabIndex = 12;
            this.lblTelephone.Text = "Telephone";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(323, 16);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 13);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "E-mail";
            // 
            // lblAddress3
            // 
            this.lblAddress3.AutoSize = true;
            this.lblAddress3.Location = new System.Drawing.Point(6, 67);
            this.lblAddress3.Name = "lblAddress3";
            this.lblAddress3.Size = new System.Drawing.Size(64, 13);
            this.lblAddress3.TabIndex = 9;
            this.lblAddress3.Text = "Address 3";
            // 
            // lblAddress2
            // 
            this.lblAddress2.AutoSize = true;
            this.lblAddress2.Location = new System.Drawing.Point(6, 41);
            this.lblAddress2.Name = "lblAddress2";
            this.lblAddress2.Size = new System.Drawing.Size(64, 13);
            this.lblAddress2.TabIndex = 8;
            this.lblAddress2.Text = "Address 2";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(6, 16);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(64, 13);
            this.lblAddress1.TabIndex = 7;
            this.lblAddress1.Text = "Address 1";
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(653, 147);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Financial Details";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnClearImage
            // 
            this.btnClearImage.Location = new System.Drawing.Point(735, 205);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(61, 23);
            this.btnClearImage.TabIndex = 31;
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(666, 205);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 23);
            this.btnBrowse.TabIndex = 30;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAutoClear);
            this.groupBox3.Controls.Add(this.txtRemark);
            this.groupBox3.Controls.Add(this.lblRemark);
            this.groupBox3.Location = new System.Drawing.Point(3, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(796, 54);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(704, 31);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 33;
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(105, 12);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(552, 36);
            this.txtRemark.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(10, 13);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "Remark";
            // 
            // picHelper
            // 
            this.picHelper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHelper.ErrorImage = null;
            this.picHelper.Image = ((System.Drawing.Image)(resources.GetObject("picHelper.Image")));
            this.picHelper.Location = new System.Drawing.Point(666, 75);
            this.picHelper.Name = "picHelper";
            this.picHelper.Size = new System.Drawing.Size(130, 127);
            this.picHelper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHelper.TabIndex = 28;
            this.picHelper.TabStop = false;
            // 
            // grpHelper
            // 
            this.grpHelper.Controls.Add(this.btnNew);
            this.grpHelper.Controls.Add(this.chkAutoCompleationHelper);
            this.grpHelper.Controls.Add(this.txtHelperDescription);
            this.grpHelper.Controls.Add(this.txtHelperCode);
            this.grpHelper.Controls.Add(this.lblHelperCode);
            this.grpHelper.Controls.Add(this.lblHelperDescription);
            this.grpHelper.Location = new System.Drawing.Point(2, -5);
            this.grpHelper.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHelper.Name = "grpHelper";
            this.grpHelper.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHelper.Size = new System.Drawing.Size(797, 57);
            this.grpHelper.TabIndex = 26;
            this.grpHelper.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(748, 29);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 23);
            this.btnNew.TabIndex = 32;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationHelper
            // 
            this.chkAutoCompleationHelper.AutoSize = true;
            this.chkAutoCompleationHelper.Location = new System.Drawing.Point(8, 33);
            this.chkAutoCompleationHelper.Name = "chkAutoCompleationHelper";
            this.chkAutoCompleationHelper.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationHelper.TabIndex = 14;
            this.chkAutoCompleationHelper.UseVisualStyleBackColor = true;
            // 
            // txtHelperDescription
            // 
            this.txtHelperDescription.Location = new System.Drawing.Point(188, 30);
            this.txtHelperDescription.MasterDescription = "";
            this.txtHelperDescription.Name = "txtHelperDescription";
            this.txtHelperDescription.Size = new System.Drawing.Size(558, 21);
            this.txtHelperDescription.TabIndex = 12;
            // 
            // txtHelperCode
            // 
            this.txtHelperCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHelperCode.IsAutoComplete = false;
            this.txtHelperCode.ItemCollection = null;
            this.txtHelperCode.Location = new System.Drawing.Point(28, 30);
            this.txtHelperCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHelperCode.MasterCode = "";
            this.txtHelperCode.MaxLength = 25;
            this.txtHelperCode.Name = "txtHelperCode";
            this.txtHelperCode.Size = new System.Drawing.Size(157, 21);
            this.txtHelperCode.TabIndex = 11;
            // 
            // lblHelperCode
            // 
            this.lblHelperCode.AutoSize = true;
            this.lblHelperCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelperCode.Location = new System.Drawing.Point(27, 15);
            this.lblHelperCode.Name = "lblHelperCode";
            this.lblHelperCode.Size = new System.Drawing.Size(85, 13);
            this.lblHelperCode.TabIndex = 9;
            this.lblHelperCode.Text = "Helper Code*";
            // 
            // lblHelperDescription
            // 
            this.lblHelperDescription.AutoSize = true;
            this.lblHelperDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelperDescription.Location = new System.Drawing.Point(185, 15);
            this.lblHelperDescription.Name = "lblHelperDescription";
            this.lblHelperDescription.Size = new System.Drawing.Size(119, 13);
            this.lblHelperDescription.TabIndex = 10;
            this.lblHelperDescription.Text = "Helper Description*";
            // 
            // FrmHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(802, 323);
            this.Controls.Add(this.tabHelper);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.picHelper);
            this.Controls.Add(this.grpHelper);
            this.Name = "FrmHelper";
            this.Text = "Helper";
            this.Load += new System.EventHandler(this.FrmHelper_Load);
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpHelper, 0);
            this.Controls.SetChildIndex(this.picHelper, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.btnClearImage, 0);
            this.Controls.SetChildIndex(this.tabHelper, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabHelper.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelper)).EndInit();
            this.grpHelper.ResumeLayout(false);
            this.grpHelper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabHelper;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.PictureBox picHelper;
        private System.Windows.Forms.GroupBox grpHelper;
        private CustomControls.TextBoxMasterDescription txtHelperDescription;
        private CustomControls.TextBoxMasterCode txtHelperCode;
        private System.Windows.Forms.Label lblHelperCode;
        private System.Windows.Forms.Label lblHelperDescription;
        private System.Windows.Forms.CheckBox chkAutoCompleationHelper;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.Button btnNew;
    }
}
