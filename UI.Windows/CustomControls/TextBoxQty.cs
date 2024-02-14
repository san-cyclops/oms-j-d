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
    /// Extended Textbox Control used to display Qty Values
    /// </summary>
    
    public partial class TextBoxQty : TextBox
    {
         // member variable used to keep Qty value
        private Decimal mQtyValue;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public decimal QtyValue 
        {
            get
            {
                return mQtyValue;
            }
            set
            {
                mQtyValue = value;
            }
        }

        // constructor
        public TextBoxQty()
        {
            InitializeComponent();
            QtyValue = 0;
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
        private void TextBoxQty_KeyPress(object sender, KeyPressEventArgs e)
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
        /// Update display to show decimal as Qty
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxQty_Validated(object sender, EventArgs e)
        {
            try
            {
                // format the value as Qty
                Decimal dTmp = Convert.ToDecimal(SetTextValue(this.Text));

                /// <summery>
                /// Change the 'F' value(number of decimal points) depending on system requirenment
                /// If entered value is 450000.02622 Then formated value is 
                /// F  --> 450000.03
                /// F2 --> 450000.03
                /// F3 --> 450000.026
                /// F4 --> 450000.0262
                /// </summary>
                //this.Text = dTmp.ToString("F2");
                this.Text = Math.Round(dTmp, Common.decimalPointsQty).ToString();
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
        private void TextBoxQty_Click(object sender, EventArgs e)
        {
            this.Text = mQtyValue.ToString();

            if (this.Text == "0")
                this.Clear();

            this.SelectionStart = this.Text.Length;
        }

        /// <summary>
        /// Update the Qty value each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                QtyValue = Convert.ToDecimal(this.Text);
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
