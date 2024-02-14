namespace UI.Windows
{
    partial class FrmChartOfAccounts
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node16");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node17");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Asset", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node18");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node19");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Liability", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node20");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node21");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node22");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Equity", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node23");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node24");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node25");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node26");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Income", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node27");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node28");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Expense", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChartOfAccounts));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bB1 = new Glass.GlassButton();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.glassButton1 = new Glass.GlassButton();
            this.grpButtonSet.SuspendLayout();
            this.grpButtonSet2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtonSet
            // 
            this.grpButtonSet.Location = new System.Drawing.Point(2, 422);
            // 
            // grpButtonSet2
            // 
            this.grpButtonSet2.Location = new System.Drawing.Point(644, 422);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(635, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 307);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(11, 18);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node16";
            treeNode1.Text = "Node16";
            treeNode2.Name = "Node17";
            treeNode2.Text = "Node17";
            treeNode3.Name = "Node11";
            treeNode3.Text = "Asset";
            treeNode4.Name = "Node18";
            treeNode4.Text = "Node18";
            treeNode5.Name = "Node19";
            treeNode5.Text = "Node19";
            treeNode6.Name = "Node12";
            treeNode6.Text = "Liability";
            treeNode7.Name = "Node20";
            treeNode7.Text = "Node20";
            treeNode8.Name = "Node21";
            treeNode8.Text = "Node21";
            treeNode9.Name = "Node22";
            treeNode9.Text = "Node22";
            treeNode10.Name = "Node13";
            treeNode10.Text = "Equity";
            treeNode11.Name = "Node23";
            treeNode11.Text = "Node23";
            treeNode12.Name = "Node24";
            treeNode12.Text = "Node24";
            treeNode13.Name = "Node25";
            treeNode13.Text = "Node25";
            treeNode14.Name = "Node26";
            treeNode14.Text = "Node26";
            treeNode15.Name = "Node14";
            treeNode15.Text = "Income";
            treeNode16.Name = "Node27";
            treeNode16.Text = "Node27";
            treeNode17.Name = "Node28";
            treeNode17.Text = "Node28";
            treeNode18.Name = "Node15";
            treeNode18.Text = "Expense";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode10,
            treeNode15,
            treeNode18});
            this.treeView1.Size = new System.Drawing.Size(380, 283);
            this.treeView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 274);
            this.panel1.TabIndex = 11;
            // 
            // bB1
            // 
            this.bB1.BackColor = System.Drawing.Color.Purple;
            this.bB1.GlowColor = System.Drawing.Color.Blue;
            this.bB1.Location = new System.Drawing.Point(530, 59);
            this.bB1.Name = "bB1";
            this.bB1.Size = new System.Drawing.Size(74, 30);
            this.bB1.TabIndex = 12;
            this.bB1.Text = ">>";
            // 
            // pnlItems
            // 
            this.pnlItems.BackColor = System.Drawing.Color.Transparent;
            this.pnlItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlItems.BackgroundImage")));
            this.pnlItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlItems.Location = new System.Drawing.Point(480, 123);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(85, 76);
            this.pnlItems.TabIndex = 14;
            this.pnlItems.Visible = false;
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.LightSlateGray;
            this.glassButton1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton1.ForeColor = System.Drawing.Color.Black;
            this.glassButton1.Image = ((System.Drawing.Image)(resources.GetObject("glassButton1.Image")));
            this.glassButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.glassButton1.Location = new System.Drawing.Point(490, 236);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(75, 32);
            this.glassButton1.TabIndex = 13;
            this.glassButton1.Text = "&View ";
            this.glassButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmChartOfAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(885, 471);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.bB1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmChartOfAccounts";
            this.Controls.SetChildIndex(this.grpButtonSet, 0);
            this.Controls.SetChildIndex(this.grpButtonSet2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.bB1, 0);
            this.Controls.SetChildIndex(this.pnlItems, 0);
            this.Controls.SetChildIndex(this.glassButton1, 0);
            this.grpButtonSet.ResumeLayout(false);
            this.grpButtonSet2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private Glass.GlassButton bB1;
        private System.Windows.Forms.Panel pnlItems;
        public Glass.GlassButton glassButton1;
    }
}
