namespace UI.Windows
{
    partial class FrmMaintenanceJobRequisitionNote
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
            this.dtpExpectedDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDocumentDetails = new System.Windows.Forms.Button();
            this.lblJobDescription = new System.Windows.Forms.Label();
            this.txtJobDescription = new System.Windows.Forms.TextBox();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.chkAutoCompleationJbrNo = new System.Windows.Forms.CheckBox();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.lblDocumentNo = new System.Windows.Forms.Label();
            this.txtValidityPeriod = new UI.Windows.CustomControls.TextBoxInteger();
            this.lblValidityPeriod = new System.Windows.Forms.Label();
            this.grpButtonSet2.SuspendLayout();
            this.grpButtonSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(604, 117);
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 117);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.txtValidityPeriod);
            this.grpHeader.Controls.Add(this.lblValidityPeriod);
            this.grpHeader.Controls.Add(this.dtpExpectedDate);
            this.grpHeader.Controls.Add(this.label1);
            this.grpHeader.Controls.Add(this.cmbLocation);
            this.grpHeader.Controls.Add(this.lblLocation);
            this.grpHeader.Controls.Add(this.txtReferenceNo);
            this.grpHeader.Controls.Add(this.lblReferenceNo);
            this.grpHeader.Controls.Add(this.dtpDocumentDate);
            this.grpHeader.Controls.Add(this.btnDocumentDetails);
            this.grpHeader.Controls.Add(this.lblJobDescription);
            this.grpHeader.Controls.Add(this.txtJobDescription);
            this.grpHeader.Controls.Add(this.lblRequestDate);
            this.grpHeader.Controls.Add(this.chkAutoCompleationJbrNo);
            this.grpHeader.Controls.Add(this.txtDocumentNo);
            this.grpHeader.Controls.Add(this.lblDocumentNo);
            this.grpHeader.Location = new System.Drawing.Point(1, -6);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(921, 128);
            this.grpHeader.TabIndex = 20;
            this.grpHeader.TabStop = false;
            // 
            // dtpExpectedDate
            // 
            this.dtpExpectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpectedDate.Location = new System.Drawing.Point(602, 12);
            this.dtpExpectedDate.Name = "dtpExpectedDate";
            this.dtpExpectedDate.Size = new System.Drawing.Size(111, 21);
            this.dtpExpectedDate.TabIndex = 65;
            this.dtpExpectedDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpectedDate_KeyDown);
            this.dtpExpectedDate.Validated += new System.EventHandler(this.dtpExpectedDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(510, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Expected Date";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(602, 36);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(313, 21);
            this.cmbLocation.TabIndex = 63;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            this.cmbLocation.Validated += new System.EventHandler(this.cmbLocation_Validated);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(510, 39);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 62;
            this.lblLocation.Text = "Location";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(112, 36);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(392, 21);
            this.txtReferenceNo.TabIndex = 24;
            this.txtReferenceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferenceNo_KeyDown);
            this.txtReferenceNo.Validated += new System.EventHandler(this.txtReferenceNo_Validated);
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Location = new System.Drawing.Point(6, 39);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(84, 13);
            this.lblReferenceNo.TabIndex = 23;
            this.lblReferenceNo.Text = "Reference No";
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocumentDate.Location = new System.Drawing.Point(804, 12);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(111, 21);
            this.dtpDocumentDate.TabIndex = 22;
            this.dtpDocumentDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDocumentDate_KeyDown);
            this.dtpDocumentDate.Validated += new System.EventHandler(this.dtpDocumentDate_Validated);
            // 
            // btnDocumentDetails
            // 
            this.btnDocumentDetails.Location = new System.Drawing.Point(254, 11);
            this.btnDocumentDetails.Name = "btnDocumentDetails";
            this.btnDocumentDetails.Size = new System.Drawing.Size(28, 23);
            this.btnDocumentDetails.TabIndex = 21;
            this.btnDocumentDetails.Text = "...";
            this.btnDocumentDetails.UseVisualStyleBackColor = true;
            // 
            // lblJobDescription
            // 
            this.lblJobDescription.AutoSize = true;
            this.lblJobDescription.Location = new System.Drawing.Point(6, 63);
            this.lblJobDescription.Name = "lblJobDescription";
            this.lblJobDescription.Size = new System.Drawing.Size(92, 13);
            this.lblJobDescription.TabIndex = 19;
            this.lblJobDescription.Text = "Job description";
            // 
            // txtJobDescription
            // 
            this.txtJobDescription.Location = new System.Drawing.Point(112, 60);
            this.txtJobDescription.MaxLength = 500;
            this.txtJobDescription.Multiline = true;
            this.txtJobDescription.Name = "txtJobDescription";
            this.txtJobDescription.Size = new System.Drawing.Size(803, 61);
            this.txtJobDescription.TabIndex = 18;
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.AutoSize = true;
            this.lblRequestDate.Location = new System.Drawing.Point(714, 16);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(60, 13);
            this.lblRequestDate.TabIndex = 9;
            this.lblRequestDate.Text = "MJR Date";
            // 
            // chkAutoCompleationJbrNo
            // 
            this.chkAutoCompleationJbrNo.AutoSize = true;
            this.chkAutoCompleationJbrNo.Location = new System.Drawing.Point(95, 15);
            this.chkAutoCompleationJbrNo.Name = "chkAutoCompleationJbrNo";
            this.chkAutoCompleationJbrNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationJbrNo.TabIndex = 4;
            this.chkAutoCompleationJbrNo.Tag = "1";
            this.chkAutoCompleationJbrNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationJbrNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationJbrNo_CheckedChanged);
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.Location = new System.Drawing.Point(112, 12);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(141, 21);
            this.txtDocumentNo.TabIndex = 2;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
            this.txtDocumentNo.Validated += new System.EventHandler(this.txtDocumentNo_Validated);
            // 
            // lblDocumentNo
            // 
            this.lblDocumentNo.AutoSize = true;
            this.lblDocumentNo.Location = new System.Drawing.Point(6, 16);
            this.lblDocumentNo.Name = "lblDocumentNo";
            this.lblDocumentNo.Size = new System.Drawing.Size(84, 13);
            this.lblDocumentNo.TabIndex = 0;
            this.lblDocumentNo.Text = "Document No";
            // 
            // txtValidityPeriod
            // 
            this.txtValidityPeriod.IntValue = 7;
            this.txtValidityPeriod.Location = new System.Drawing.Point(394, 13);
            this.txtValidityPeriod.Name = "txtValidityPeriod";
            this.txtValidityPeriod.Size = new System.Drawing.Size(110, 21);
            this.txtValidityPeriod.TabIndex = 100;
            this.txtValidityPeriod.Text = "7";
            this.txtValidityPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValidityPeriod
            // 
            this.lblValidityPeriod.AutoSize = true;
            this.lblValidityPeriod.Location = new System.Drawing.Point(285, 18);
            this.lblValidityPeriod.Name = "lblValidityPeriod";
            this.lblValidityPeriod.Size = new System.Drawing.Size(111, 13);
            this.lblValidityPeriod.TabIndex = 99;
            this.lblValidityPeriod.Text = "Validity Period (D)";
            // 
            // FrmMaintenanceJobRequisitionNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 165);
            this.Controls.Add(this.grpHeader);
            this.Name = "FrmMaintenanceJobRequisitionNote";
            this.Text = "FrmMaintenanceJobRequisitionNote";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.grpHeader, 0);
            this.grpButtonSet2.ResumeLayout(false);
            this.grpButtonSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.DateTimePicker dtpExpectedDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Button btnDocumentDetails;
        private System.Windows.Forms.Label lblJobDescription;
        private System.Windows.Forms.TextBox txtJobDescription;
        private System.Windows.Forms.Label lblRequestDate;
        private System.Windows.Forms.CheckBox chkAutoCompleationJbrNo;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label lblDocumentNo;
        private CustomControls.TextBoxInteger txtValidityPeriod;
        private System.Windows.Forms.Label lblValidityPeriod;
    }
}