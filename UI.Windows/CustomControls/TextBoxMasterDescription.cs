using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Windows.CustomControls
{
    /// <summary>
    /// Extended Textbox Control used to display Master description
    /// </summary>
    /// 
    public partial class TextBoxMasterDescription : TextBox
    {
        // member variable used to keep master description
        private string mMasterDescription;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public string MasterDescription
        {
            get
            {
                return mMasterDescription;
            }
            set
            {
                mMasterDescription = value;
            }
        }


        // constructor
        public TextBoxMasterDescription()
        {
            InitializeComponent();
            MasterDescription = string.Empty;
        }

        // default OnPaint
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        /// <summary>
        /// Keypress handler used to restrict unnecessary user inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SetMaxLength();
            }
            catch (Exception ex)
            {

            }
        }



        /// <summary>
        /// Update display to show master description
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterDescription_Validated(object sender, EventArgs e)
        {
            try
            {
                // Convert letters to upper case
                this.Text = this.Text.ToUpper();
            }
            catch { }
        }

        public virtual void SetDescrtiptionFormat()
        {
            // Convert letters to upper case
            this.Text = this.Text.ToUpper();
        }

        /// <summary>
        /// Update the master description each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterDescription_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Returns MaxLength of Text Box
        /// </summary>
        /// <returns></returns>
        private void SetMaxLength()
        {
            this.MaxLength = 100;
        }

    }
}
