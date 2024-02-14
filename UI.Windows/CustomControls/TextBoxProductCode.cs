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
    /// Extended Textbox Control used to display Product code
    /// </summary>
    /// 
    public partial class TextBoxProductCode : TextBox
    {
        // member variable used to keep Product code
        private string mProductCode;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public string ProductCode
        {
            get
            {
                return mProductCode;
            }
            set
            {
                mProductCode = value;
            }
        }


        // constructor
        public TextBoxProductCode()
        {
            InitializeComponent();
            ProductCode = string.Empty;
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
        private void TextBoxProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SetMaxLength();

                // allows only numbers, letters and selected characters('#', '-', '/', '\')
                if (!Char.IsLetter(e.KeyChar) && !Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == '#' || e.KeyChar == '-' || e.KeyChar == '/' || e.KeyChar == '\\')
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        /// <summary>
        /// Update display to show Product code
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxProductCode_Validated(object sender, EventArgs e)
        {
            try
            {
                // Convert letters to upper case
                this.Text = this.Text.ToUpper();
            }
            catch { }
        }


        /// <summary>
        /// Update the Product code each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxProductCode_TextChanged(object sender, EventArgs e)
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
        private void TextBoxProductCode_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Returns MaxLength of Text Box
        /// </summary>
        /// <returns></returns>
        private void SetMaxLength()
        {
            this.MaxLength = 25;
        }

    }
}
