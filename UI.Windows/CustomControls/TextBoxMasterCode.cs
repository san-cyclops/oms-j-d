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
    /// Extended Textbox Control used to display Master code 
    /// </summary>
    /// 
    public partial class TextBoxMasterCode : TextBox
    {
        // member variable used to keep Master code
        private string mMasterCode;

        /// <summary>
        /// property to maintain value of control
        /// </summary>
        public string MasterCode
        {
            get
            {
                return mMasterCode;
            }
            set
            {
                mMasterCode = value;
            }
        }


        // constructor
        public TextBoxMasterCode()
        {
            InitializeComponent();
            MasterCode = string.Empty;
        }

        public bool IsAutoComplete { get; set; }

        public AutoCompleteStringCollection ItemCollection { get; set; }

        private bool autoModeSet = false;

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
        private void TextBoxMasterCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SetMaxLength();

                // allows only numbers, letters and selected characters
                if (!Char.IsLetter(e.KeyChar) && !Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Update display to show master code
        /// whenver it is validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterCode_Validated(object sender, EventArgs e)
        {
            try
            {
                // Convert letters to upper case
                this.Text = this.Text.ToUpper();
            }
            catch { }
        }


        /// <summary>
        /// Update the master code each time the value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterCode_TextChanged(object sender, EventArgs e)
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
        private void TextBoxMasterCode_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Bind auto complete collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterCode_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Remove auto complete collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxMasterCode_Leave(object sender, EventArgs e)
        {

        }
    }
}


