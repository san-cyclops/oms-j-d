namespace UI.Windows
{
    partial class FrmVehicle
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
            this.txtWeight = new UI.Windows.CustomControls.TextBoxNumeric();
            this.txtRegistrationNo = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.chkAutoClear = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleationRegistrationNo = new System.Windows.Forms.CheckBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.txtChassesNo = new System.Windows.Forms.TextBox();
            this.lblChassisNo = new System.Windows.Forms.Label();
            this.txtEngineNo = new System.Windows.Forms.TextBox();
            this.lblEngineNo = new System.Windows.Forms.Label();
            this.cmbSeatingCapacity = new System.Windows.Forms.ComboBox();
            this.lblSeatingCapacity = new System.Windows.Forms.Label();
            this.cmbEngineCapacity = new System.Windows.Forms.ComboBox();
            this.lblEngineCapacity = new System.Windows.Forms.Label();
            this.cmbFuelType = new System.Windows.Forms.ComboBox();
            this.lblFuelType = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblMake = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtVehicleName = new UI.Windows.CustomControls.TextBoxMasterDescription();
            this.lblRegistrationNo = new System.Windows.Forms.Label();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 245);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(402, 245);
            // 
            // btnSave
            // 
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtWeight);
            this.groupBox1.Controls.Add(this.txtRegistrationNo);
            this.groupBox1.Controls.Add(this.txtMake);
            this.groupBox1.Controls.Add(this.chkAutoClear);
            this.groupBox1.Controls.Add(this.chkAutoCompleationRegistrationNo);
            this.groupBox1.Controls.Add(this.lblWeight);
            this.groupBox1.Controls.Add(this.txtChassesNo);
            this.groupBox1.Controls.Add(this.lblChassisNo);
            this.groupBox1.Controls.Add(this.txtEngineNo);
            this.groupBox1.Controls.Add(this.lblEngineNo);
            this.groupBox1.Controls.Add(this.cmbSeatingCapacity);
            this.groupBox1.Controls.Add(this.lblSeatingCapacity);
            this.groupBox1.Controls.Add(this.cmbEngineCapacity);
            this.groupBox1.Controls.Add(this.lblEngineCapacity);
            this.groupBox1.Controls.Add(this.cmbFuelType);
            this.groupBox1.Controls.Add(this.lblFuelType);
            this.groupBox1.Controls.Add(this.txtModel);
            this.groupBox1.Controls.Add(this.cmbVehicleType);
            this.groupBox1.Controls.Add(this.lblModel);
            this.groupBox1.Controls.Add(this.lblMake);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.txtVehicleName);
            this.groupBox1.Controls.Add(this.lblRegistrationNo);
            this.groupBox1.Controls.Add(this.lblVehicleName);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(639, 256);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(563, 152);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWeight.Size = new System.Drawing.Size(67, 21);
            this.txtWeight.TabIndex = 39;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWeight_KeyDown);
            this.txtWeight.Leave += new System.EventHandler(this.txtWeight_Leave);
            // 
            // txtRegistrationNo
            // 
            this.txtRegistrationNo.Location = new System.Drawing.Point(144, 17);
            this.txtRegistrationNo.MasterDescription = "";
            this.txtRegistrationNo.Name = "txtRegistrationNo";
            this.txtRegistrationNo.Size = new System.Drawing.Size(486, 21);
            this.txtRegistrationNo.TabIndex = 38;
            this.txtRegistrationNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRegistrationNo_KeyDown);
            this.txtRegistrationNo.Leave += new System.EventHandler(this.txtRegistrationNo_Leave);
            // 
            // txtMake
            // 
            this.txtMake.Location = new System.Drawing.Point(144, 125);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(169, 21);
            this.txtMake.TabIndex = 37;
            this.txtMake.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMake_KeyDown);
            this.txtMake.Leave += new System.EventHandler(this.txtMake_Leave);
            // 
            // chkAutoClear
            // 
            this.chkAutoClear.AutoSize = true;
            this.chkAutoClear.Location = new System.Drawing.Point(543, 228);
            this.chkAutoClear.Name = "chkAutoClear";
            this.chkAutoClear.Size = new System.Drawing.Size(87, 17);
            this.chkAutoClear.TabIndex = 34;
            this.chkAutoClear.Tag = "1";
            this.chkAutoClear.Text = "Auto Clear";
            this.chkAutoClear.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleationRegistrationNo
            // 
            this.chkAutoCompleationRegistrationNo.AutoSize = true;
            this.chkAutoCompleationRegistrationNo.Checked = true;
            this.chkAutoCompleationRegistrationNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCompleationRegistrationNo.Location = new System.Drawing.Point(108, 21);
            this.chkAutoCompleationRegistrationNo.Name = "chkAutoCompleationRegistrationNo";
            this.chkAutoCompleationRegistrationNo.Size = new System.Drawing.Size(15, 14);
            this.chkAutoCompleationRegistrationNo.TabIndex = 33;
            this.chkAutoCompleationRegistrationNo.Tag = "1";
            this.chkAutoCompleationRegistrationNo.UseVisualStyleBackColor = true;
            this.chkAutoCompleationRegistrationNo.CheckedChanged += new System.EventHandler(this.chkAutoCompleationRegistrationNo_CheckedChanged);
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.Location = new System.Drawing.Point(483, 155);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(74, 13);
            this.lblWeight.TabIndex = 31;
            this.lblWeight.Text = "Weight (Kg)";
            // 
            // txtChassesNo
            // 
            this.txtChassesNo.Location = new System.Drawing.Point(427, 71);
            this.txtChassesNo.Name = "txtChassesNo";
            this.txtChassesNo.Size = new System.Drawing.Size(203, 21);
            this.txtChassesNo.TabIndex = 30;
            this.txtChassesNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChassesNo_KeyDown);
            this.txtChassesNo.Leave += new System.EventHandler(this.txtChassesNo_Leave);
            // 
            // lblChassisNo
            // 
            this.lblChassisNo.AutoSize = true;
            this.lblChassisNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChassisNo.Location = new System.Drawing.Point(320, 74);
            this.lblChassisNo.Name = "lblChassisNo";
            this.lblChassisNo.Size = new System.Drawing.Size(74, 13);
            this.lblChassisNo.TabIndex = 29;
            this.lblChassisNo.Text = "Chasses No";
            // 
            // txtEngineNo
            // 
            this.txtEngineNo.Location = new System.Drawing.Point(144, 71);
            this.txtEngineNo.Name = "txtEngineNo";
            this.txtEngineNo.Size = new System.Drawing.Size(169, 21);
            this.txtEngineNo.TabIndex = 28;
            this.txtEngineNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEngineNo_KeyDown);
            this.txtEngineNo.Leave += new System.EventHandler(this.txtEngineNo_Leave);
            // 
            // lblEngineNo
            // 
            this.lblEngineNo.AutoSize = true;
            this.lblEngineNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEngineNo.Location = new System.Drawing.Point(6, 74);
            this.lblEngineNo.Name = "lblEngineNo";
            this.lblEngineNo.Size = new System.Drawing.Size(64, 13);
            this.lblEngineNo.TabIndex = 27;
            this.lblEngineNo.Text = "Engine No";
            // 
            // cmbSeatingCapacity
            // 
            this.cmbSeatingCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeatingCapacity.FormattingEnabled = true;
            this.cmbSeatingCapacity.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            ">15"});
            this.cmbSeatingCapacity.Location = new System.Drawing.Point(427, 152);
            this.cmbSeatingCapacity.Name = "cmbSeatingCapacity";
            this.cmbSeatingCapacity.Size = new System.Drawing.Size(52, 21);
            this.cmbSeatingCapacity.TabIndex = 26;
            this.cmbSeatingCapacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSeatingCapacity_KeyDown);
            this.cmbSeatingCapacity.Leave += new System.EventHandler(this.cmbSeatingCapacity_Leave);
            // 
            // lblSeatingCapacity
            // 
            this.lblSeatingCapacity.AutoSize = true;
            this.lblSeatingCapacity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeatingCapacity.Location = new System.Drawing.Point(320, 155);
            this.lblSeatingCapacity.Name = "lblSeatingCapacity";
            this.lblSeatingCapacity.Size = new System.Drawing.Size(104, 13);
            this.lblSeatingCapacity.TabIndex = 25;
            this.lblSeatingCapacity.Text = "Seating Capacity";
            // 
            // cmbEngineCapacity
            // 
            this.cmbEngineCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEngineCapacity.FormattingEnabled = true;
            this.cmbEngineCapacity.Items.AddRange(new object[] {
            "0 -100 CC",
            "100 - 250 CC",
            "250 - 600 CC",
            "600 - 1000 CC",
            "1000 - 1500 CC",
            "1500 - 1800 CC",
            "1800 - 2200 CC",
            "2200 - 3000 CC",
            "3000 - 4500 CC",
            "More Than 4500 CC",
            ""});
            this.cmbEngineCapacity.Location = new System.Drawing.Point(144, 152);
            this.cmbEngineCapacity.Name = "cmbEngineCapacity";
            this.cmbEngineCapacity.Size = new System.Drawing.Size(169, 21);
            this.cmbEngineCapacity.TabIndex = 24;
            this.cmbEngineCapacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbEngineCapacity_KeyDown);
            this.cmbEngineCapacity.Leave += new System.EventHandler(this.cmbEngineCapacity_Leave);
            // 
            // lblEngineCapacity
            // 
            this.lblEngineCapacity.AutoSize = true;
            this.lblEngineCapacity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEngineCapacity.Location = new System.Drawing.Point(6, 155);
            this.lblEngineCapacity.Name = "lblEngineCapacity";
            this.lblEngineCapacity.Size = new System.Drawing.Size(99, 13);
            this.lblEngineCapacity.TabIndex = 23;
            this.lblEngineCapacity.Text = "Engine Capacity";
            // 
            // cmbFuelType
            // 
            this.cmbFuelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuelType.FormattingEnabled = true;
            this.cmbFuelType.Items.AddRange(new object[] {
            "Petrol",
            "Diesel",
            "Gas"});
            this.cmbFuelType.Location = new System.Drawing.Point(427, 98);
            this.cmbFuelType.Name = "cmbFuelType";
            this.cmbFuelType.Size = new System.Drawing.Size(203, 21);
            this.cmbFuelType.TabIndex = 22;
            this.cmbFuelType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFuelType_KeyDown);
            this.cmbFuelType.Leave += new System.EventHandler(this.cmbFuelType_Leave);
            // 
            // lblFuelType
            // 
            this.lblFuelType.AutoSize = true;
            this.lblFuelType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuelType.Location = new System.Drawing.Point(320, 101);
            this.lblFuelType.Name = "lblFuelType";
            this.lblFuelType.Size = new System.Drawing.Size(68, 13);
            this.lblFuelType.TabIndex = 21;
            this.lblFuelType.Text = "Fuel Type*";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(427, 125);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(203, 21);
            this.txtModel.TabIndex = 20;
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModel_KeyDown);
            this.txtModel.Leave += new System.EventHandler(this.txtModel_Leave);
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] {
            "Car",
            "Van",
            "Jeep",
            "Lorry",
            "Bus",
            "Three Weel",
            "Motor Bike"});
            this.cmbVehicleType.Location = new System.Drawing.Point(144, 98);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(169, 21);
            this.cmbVehicleType.TabIndex = 18;
            this.cmbVehicleType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbVehicleType_KeyDown);
            this.cmbVehicleType.Leave += new System.EventHandler(this.cmbVehicleType_Leave);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(320, 128);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(40, 13);
            this.lblModel.TabIndex = 17;
            this.lblModel.Text = "Model";
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMake.Location = new System.Drawing.Point(6, 128);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(37, 13);
            this.lblMake.TabIndex = 16;
            this.lblMake.Text = "Make";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(6, 101);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "Type";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(144, 179);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(486, 37);
            this.txtRemark.TabIndex = 14;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.Location = new System.Drawing.Point(6, 182);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(52, 13);
            this.lblRemark.TabIndex = 13;
            this.lblRemark.Text = "Remark";
            // 
            // txtVehicleName
            // 
            this.txtVehicleName.Location = new System.Drawing.Point(144, 44);
            this.txtVehicleName.MasterDescription = "";
            this.txtVehicleName.Name = "txtVehicleName";
            this.txtVehicleName.Size = new System.Drawing.Size(486, 21);
            this.txtVehicleName.TabIndex = 12;
            this.txtVehicleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVehicleName_KeyDown);
            this.txtVehicleName.Leave += new System.EventHandler(this.txtVehicleName_Leave);
            // 
            // lblRegistrationNo
            // 
            this.lblRegistrationNo.AutoSize = true;
            this.lblRegistrationNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrationNo.Location = new System.Drawing.Point(6, 21);
            this.lblRegistrationNo.Name = "lblRegistrationNo";
            this.lblRegistrationNo.Size = new System.Drawing.Size(101, 13);
            this.lblRegistrationNo.TabIndex = 9;
            this.lblRegistrationNo.Text = "Registration No*";
            // 
            // lblVehicleName
            // 
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicleName.Location = new System.Drawing.Point(6, 47);
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.Size = new System.Drawing.Size(47, 13);
            this.lblVehicleName.TabIndex = 10;
            this.lblVehicleName.Text = "Name*";
            // 
            // FrmVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(643, 294);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmVehicle";
            this.Text = "Vehicle";
            this.Load += new System.EventHandler(this.FrmVehicle_Load);
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
        private CustomControls.TextBoxMasterDescription txtVehicleName;
        private System.Windows.Forms.Label lblRegistrationNo;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbSeatingCapacity;
        private System.Windows.Forms.Label lblSeatingCapacity;
        private System.Windows.Forms.ComboBox cmbEngineCapacity;
        private System.Windows.Forms.Label lblEngineCapacity;
        private System.Windows.Forms.ComboBox cmbFuelType;
        private System.Windows.Forms.Label lblFuelType;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtChassesNo;
        private System.Windows.Forms.Label lblChassisNo;
        private System.Windows.Forms.TextBox txtEngineNo;
        private System.Windows.Forms.Label lblEngineNo;
        private System.Windows.Forms.CheckBox chkAutoCompleationRegistrationNo;
        private System.Windows.Forms.CheckBox chkAutoClear;
        private System.Windows.Forms.TextBox txtMake;
        private CustomControls.TextBoxMasterDescription txtRegistrationNo;
        private CustomControls.TextBoxNumeric txtWeight;
    }
}
