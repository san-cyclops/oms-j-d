namespace UI.Windows.CustomControls
{
    partial class TextBoxProductCode
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Product Code TextBox
            // 
            this.Click += new System.EventHandler(this.TextBoxProductCode_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxProductCode_KeyPress);
            this.Validated += new System.EventHandler(this.TextBoxProductCode_Validated);
            this.TextChanged += new System.EventHandler(this.TextBoxProductCode_TextChanged);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
