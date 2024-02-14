using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using Utility;

namespace UI.Windows.CustomControls
{

    /// <summary>
    /// Extended Textbox Control used to display Currency
    /// </summary>
    /// 
    public partial class TextBoxCurrency : TextBox
    {
        // member variable used to keep Currency value
        private decimal mCurrencyValue;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public decimal CurrencyValue
        {
            get
            {
                return mCurrencyValue;
            }
            set
            {
                mCurrencyValue = value;
            }
        }

        // constructor
        public TextBoxCurrency()
        {
            InitializeComponent();
            CurrencyValue = 0;
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
        private void TextBoxCurrency_KeyPress(object sender, KeyPressEventArgs e)
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
        /// Update display to show decimal as currency
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxCurrency_Validated(object sender, EventArgs e)
        {
            try
            {
                // format the value as currency
                Decimal dTmp = Convert.ToDecimal(SetTextValue(this.Text));

                /// <summery>
                /// Change the 'N' value(number of decimal points) depending on system requirenment.
                /// Thousands separator(",") is using to display currency values.
                /// If entered value is 450000.02622 Then formated value is 
                /// N  --> 450,000.03
                /// N2 --> 450,000.03
                /// N3 --> 450,000.026
                /// N4 --> 450,000.0262
                /// </summary>
                //this.Text = dTmp.ToString("N", CultureInfo.InvariantCulture);
                this.Text = Common.ConvertDecimalToStringCurrency(dTmp);
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
        private void TextBoxCurrency_Click(object sender, EventArgs e)
        {
            this.Text = mCurrencyValue.ToString();

            if (this.Text == "0")
                this.Clear();

        }

        /// <summary>
        /// Update the currency value each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    CurrencyValue = Convert.ToDecimal(this.Text);
                }
                else
                {
                    CurrencyValue = 0;
                }
            }
            catch { }
        }

        /// <summary>
        /// Revert back to the display of numbers only
        /// whenever the user enters in to text box for
        /// editing purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxCurrency_Enter(object sender, EventArgs e)
        {
            try
            {
                this.Text = mCurrencyValue.ToString();

                if (this.Text == "0")
                    this.Clear();

                this.SelectionStart = this.Text.Length;
            }
            catch (Exception ex)
            { }
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
