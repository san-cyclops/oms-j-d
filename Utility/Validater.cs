using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public static class Validater
    {


        public enum ValidateType
        {
            Empty,
            Date,
            Zero,
            Length,
            CompareDate,
            Telephone
        }

        #region ValidateForm

        public static bool ValidateTextBox(ErrorProvider errorProvider, ValidateType validateType, params TextBox[] textBox)
        {
            errorProvider.Clear();
            bool isValid=true;

            foreach (TextBox T in textBox)
            {
                switch (validateType)
                {
                    case ValidateType.Empty:
                        {
                            if (string.IsNullOrEmpty(T.Text.Trim()))
                            {
                                errorProvider.SetError(T, "This field cannot be empty!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                //T.Text = "0.00";
                                isValid = false;
                            }
                        }
                        break;

                    case ValidateType.Zero:
                        {
                            //decimal value;
                            //if (string.IsNullOrEmpty(T.Text.Trim()) || !decimal.TryParse(T.Text, out value) || decimal.Parse(T.Text.Trim()) == 0)
                            //{
                            //    errorProvider.SetError(T, "This field cannot be Zero!");
                            //    errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                            //    //T.Text = "1.00";
                            //    isValid = false;
                            //}

                            decimal value;
                            if (string.IsNullOrEmpty(T.Text.Trim()) || decimal.Parse(T.Text.Trim()) == 0)
                            {
                                errorProvider.SetError(T, "This field cannot be Zero!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                //T.Text = "1.00";
                                isValid = false;
                            }
                        }
                        break;     
                }
                
            }
            if (!isValid)
                Toast.Show("",Toast.messageType.Error,Toast.messageAction.ValidationFailed);
            return isValid;
        }

        public static bool ValidateComboBox(ErrorProvider errorProvider, ValidateType validateType, params ComboBox[] comboBox)
        {
            errorProvider.Clear();
            bool isValid = true;

            foreach (ComboBox c in comboBox)
            {
                switch (validateType)
                {
                    case ValidateType.Empty:
                        {
                            if (string.IsNullOrEmpty(c.Text.Trim()))
                            {
                                errorProvider.SetError(c, "This field cannot be empty!");
                                errorProvider.SetIconAlignment(c, ErrorIconAlignment.MiddleLeft);
                                isValid = false;
                            }
                        }
                        break;

                    case ValidateType.Zero:
                        {
                            decimal value;
                            if (string.IsNullOrEmpty(c.Text.Trim()) || !decimal.TryParse(c.Text, out value) || decimal.Parse(c.Text.Trim()) == 0)
                            {
                                errorProvider.SetError(c, "This field cannot be Zero!");
                                errorProvider.SetIconAlignment(c, ErrorIconAlignment.MiddleLeft);

                                isValid = false;
                            }
                        }
                        break;
                }
            }
            if (!isValid)
                Toast.Show("", Toast.messageType.Error, Toast.messageAction.ValidationFailed);

            return isValid;
        }

        public static bool ValidateDateTimePicker(ErrorProvider errorProvider, ValidateType validateType, params DateTimePicker[] dateTimePicker)
        {
            errorProvider.Clear();
            bool isValid = true;

            foreach (DateTimePicker dtp in dateTimePicker)
            {

                switch (validateType)
                {

                   
                    case ValidateType.Date:
                        {

                            DateTime value;
                            if (string.IsNullOrEmpty(dtp.Text.Trim()) || !DateTime.TryParse(dtp.Text, out value))
                            
                                {
                                    errorProvider.SetError(dtp, "Invalid Date!");
                                    errorProvider.SetIconAlignment(dtp, ErrorIconAlignment.MiddleLeft);

                                    isValid = false;
                                }

                        }
                        break;


                }

            }

            if (!isValid)
                Toast.Show("", Toast.messageType.Error, Toast.messageAction.ValidationFailed);

            return isValid;
        }

        public static bool ValidateControls(ErrorProvider errorProvider, ValidateType validateType, params Control[] controlBox)
        {
            errorProvider.Clear();
            bool isValid = true;

            foreach (Control T in controlBox)
            {
                switch (validateType)
                {
                    case ValidateType.Empty:
                        {
                            if (string.IsNullOrEmpty(T.Text.Trim()))
                            {
                                errorProvider.SetError(T, "This field cannot be empty!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                isValid = false;
                            }
                        }
                        break;

                    case ValidateType.Zero:
                        {
                            if (string.IsNullOrEmpty(T.Text.Trim()) && decimal.Parse(T.Text.Trim()) != 0)
                            {
                                errorProvider.SetError(T, "This field cannot be Zero!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);

                                isValid = false;
                            }
                        }
                        break;

                    case ValidateType.Date:
                        {
                            DateTime value;
                            if (string.IsNullOrEmpty(T.Text.Trim()) || !DateTime.TryParse(T.Text, out value))
                            {
                                errorProvider.SetError(T, "Invalid Date!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                
                                isValid = false;
                            }
                        }
                        break;
                }
            }
            if (!isValid)
                Toast.Show("", Toast.messageType.Error, Toast.messageAction.ValidationFailed);
            return isValid;
        }

        public static bool ValidateTextBoxWithCustomerMessage(string message, ErrorProvider errorProvider, ValidateType validateType, params TextBox[] textBox)
        {
            errorProvider.Clear();
            bool isValid = true;

            foreach (TextBox T in textBox)
            {
                switch (validateType)
                {
                    case ValidateType.Empty:
                        {
                            if (string.IsNullOrEmpty(T.Text.Trim()))
                            {
                                errorProvider.SetError(T, "This field cannot be empty!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                //T.Text = "0.00";
                                isValid = false;
                            }
                        }
                        break;

                    case ValidateType.Zero:
                        {
                            decimal value;

                            if (string.IsNullOrEmpty(T.Text.Trim()) || !decimal.TryParse(T.Text, out value) || decimal.Parse(T.Text.Trim()) == 0)
                            {
                                errorProvider.SetError(T, "This field cannot be Zero!");
                                errorProvider.SetIconAlignment(T, ErrorIconAlignment.MiddleLeft);
                                //T.Text = "1.00";
                                isValid = false;
                            }
                        }
                        break;
                }
            }

            if (!isValid)
            {
                Toast.Show(message, Toast.messageType.Warning, Toast.messageAction.General);
            }
            return isValid;
        }

        public static bool ValidateCharacterLength(ErrorProvider errorProvider, ValidateType validateType, TextBox textBox, int textLength)
        {
            errorProvider.Clear();
            bool isValid = true;
            
            switch (validateType)
            {
                case ValidateType.Length:
                {
                    if (!string.IsNullOrEmpty(textBox.Text.Trim()) && textBox.TextLength != textLength)
                    {
                        errorProvider.SetError(textBox, " invalid Length.");
                        errorProvider.SetIconAlignment(textBox, ErrorIconAlignment.MiddleLeft);
                        isValid = false;
                    }
                }
                break;
            }
            
            if (!isValid)
                Toast.Show("", Toast.messageType.Error, Toast.messageAction.ValidationFailed);
            return isValid;
        }

        public static bool ValidateDate(ErrorProvider errorProvider, ValidateType validateType, DateTimePicker compareDate, DateTime fromDate, DateTime toDate)
        {
            errorProvider.Clear();
            bool isValid = true;

            switch (validateType)
            {
                case ValidateType.CompareDate:
                    {
                        if (Common.FormatDate(toDate) < Common.FormatDate(fromDate))
                        {
                            errorProvider.SetError(compareDate, " invalid Date.");
                            errorProvider.SetIconAlignment(compareDate, ErrorIconAlignment.MiddleLeft);
                            isValid = false;
                        }
                    }
                    break;
            }

            if (!isValid)
                Toast.Show("", Toast.messageType.Error, Toast.messageAction.ValidationFailed);
            return isValid;
        }

        public static bool ValidateTelephoneNumber(object sender, KeyPressEventArgs e) 
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '#')
            {
                e.Handled = true;
            }
            return e.Handled;
        }

        public static bool ValidateAmount(object sender, KeyPressEventArgs e) 
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            return e.Handled;
        }

        public static bool ValidateFigure(object sender, KeyPressEventArgs e) 
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            return e.Handled;
        }

        public static bool ValidateEmailAddress(object sender, KeyPressEventArgs e)  
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '@' && e.KeyChar != '.' && e.KeyChar != '_')
            {
                e.Handled = true;
            }
            return e.Handled;
        }

        #endregion
    }
}
