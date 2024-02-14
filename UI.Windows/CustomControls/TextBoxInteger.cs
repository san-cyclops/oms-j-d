using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Utility;

namespace UI.Windows.CustomControls
{
    /// <summary>
    /// Extended Textbox Control used to display Integer Values
    /// </summary>
    /// 
    public partial class TextBoxInteger : TextBox
    {// member variable used to keep Numeric value
        private int mIntValue;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public int IntValue
        {
            get
            {
                return mIntValue;
            }
            set
            {
                mIntValue = value;
            }
        }

        // constructor
        public TextBoxInteger()
        {
            InitializeComponent();
            IntValue = 0;
        }

        // default OnPaint
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        /// <summary>
        /// Keypress handler used to restrict user input
        /// to numbers and control characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInteger_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetMaxLength();

            // allows only numbers, decimals and control characters
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && this.Text.Contains("."))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && this.Text.Length < 1)
            {
                e.Handled = true;
            }

        }

        /// <summary>
        /// Update display to show interger value
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInteger_Validated(object sender, EventArgs e)
        {
            try
            {
                // format the value as integer
                int dTmp = Convert.ToInt32(SetTextValue(this.Text));
                this.Text = dTmp.ToString();
            }
            catch { }
        }

        /// <summary>
        /// Revert back to the display of numbers only
        /// whenever the user clicks in the box for
        /// editing purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInteger_Click(object sender, EventArgs e)
        {
            this.Text = mIntValue.ToString();

            if (this.Text == "0")
                this.Clear();

            this.SelectionStart = this.Text.Length;
        }

        /// <summary>
        /// Update the integer value each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxInteger_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IntValue = Convert.ToInt32(this.Text);
            }
            catch { }
        }

        /// <summary>
        /// Returns MaxLength of Text Box
        /// </summary>
        /// <returns></returns>
        private void SetMaxLength()
        {
            this.MaxLength = 23;
        }

        /// <summary>
        /// Set text value to '0' if input text is empty
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string SetTextValue(string text)
        {
            return text.Equals(string.Empty) ? text = "0" : text;
        }
    }
}

