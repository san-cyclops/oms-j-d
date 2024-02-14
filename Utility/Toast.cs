using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;



namespace Utility
{
    public static class Toast
    {
        public enum messageType
        {
            Error,
            Warning,
            Information,
            Question
        }

        public enum messageAction
        {
            Save,
            SaveTransaction,
            PauseTransaction,
            Modify,
            Delete,
            ExistRecord,
            ExistsForSelected,
            NotExists,
            NotExistsForSelected,
            NotExistsSave,
            NotSelected,
            General,
            Length,
            ValidationFailed,
            Exceed,
            Generate,
            Invalid,
            Print,
            OverwriteQty,
            ConfirmTransaction,
            SubTotalDiscountPercentageExceed,
            SubTotalDiscountAmountExceed,
            ProductDiscountAmountExceedMinimum,
            ProductDiscountPercentageExceed, 
            ProductDiscountAmountExceed,            
            QtyExceed,
            BatchQtyExceed,
            InvalidDate,
            InvalidDateRange,
            AccessDenied,
            NotFound,
            ZeroAmount,
            ZeroQty,
            InvalidTelephoneNumber,
            InvalidEmailAddress,
            GreaterThan,
            ConfirmPassword,
            Saved,
            Empty,
            ExitSystem,
            AlreadyExists,
            Permission,
        }

        public static DialogResult Show(string displayMessage, messageType MessageType, messageAction MessageAction, string optionalDisplayMessage="")
        {
            string message=string.Empty;
            DialogResult dialogResult=DialogResult.No;
            
            if (MessageAction==messageAction.Save)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to save?\n" + displayMessage;
                else if(MessageType==messageType.Information)
                    message = displayMessage + "\nSuccessfully saved.\n" + "Do you want to create new Record?";

            }
            else if (MessageAction == messageAction.SaveTransaction)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to save?\n" + displayMessage;
                else if (MessageType == messageType.Information)
                    message = displayMessage + "\nSuccessfully saved.";
                else if (MessageType == messageType.Error)
                    message = displayMessage + "\nNot saved.";

            }
            else if (MessageAction == messageAction.Print)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to Print?\n" + displayMessage;
                else if (MessageType == messageType.Information)
                    message = displayMessage + "\nSuccessfully Printed.";
                else if (MessageType == messageType.Error)
                    message = displayMessage + "\nNot Printed.";

            }
            else if (MessageAction == messageAction.PauseTransaction)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to Pause?\n" + displayMessage;
                else if (MessageType == messageType.Information)
                    message = displayMessage + "\nSuccessfully Paused.";
                else if (MessageType == messageType.Error)
                    message = displayMessage + "\nNot Paused.";

            }
            else if (MessageAction == messageAction.Modify)
            {
                if(MessageType==messageType.Question)
                    message = "Do you want to modify?\n" + displayMessage;
                else
                    message = displayMessage + "\nSuccessfully modified.";

            }
            else if (MessageAction == messageAction.ConfirmTransaction)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to confirm?\n" + displayMessage;
                else if (MessageType == messageType.Information)
                    message = displayMessage + "\nSuccessfully confirmed.";
                else if (MessageType == messageType.Error)
                    message = displayMessage + "\nNot Confirmed.";

            }
            else if (MessageAction == messageAction.ExistRecord)
            {
                message = displayMessage + " already exists.\nDo you want to modify?";
            }
            else if (MessageAction == messageAction.ExistsForSelected)
            {
                message = displayMessage + " already exists.\nFor " + optionalDisplayMessage;
            }
            else if (MessageAction == messageAction.Delete)
            {
                if (MessageType == messageType.Question)
                     message = "Do you want to delete?\n" + displayMessage;
                else
                    message = displayMessage + "\nSuccessfully deleted.";

            }
            else if (MessageAction == messageAction.NotExists)
            {
                message = displayMessage + " not exists.";

            }
            else if (MessageAction == messageAction.NotExistsForSelected)
            {
                message = displayMessage + " not exists.\nFor " + optionalDisplayMessage;

            }
            else if (MessageAction == messageAction.NotExistsSave)
            {
                message = displayMessage + " not exists.\nDo you want to create New?";

            }
            else if (MessageAction == messageAction.NotSelected)
            {
                message = displayMessage + " not selected.";

            }
            else if (MessageAction == messageAction.General)
            {
                message =  displayMessage;
            }
            else if (MessageAction == messageAction.Length)
            {
                message = "Invalid Length for " + displayMessage + ".";
            }
            else if (MessageAction == messageAction.ValidationFailed)
            {
                message = "Validation failed\nPlease check blinking area values.";
            }
            else if (MessageAction == messageAction.Exceed)
            {
                message = "You are already exceeded\n" + displayMessage + ".";
            }
            else if (MessageAction == messageAction.Generate)
            {
                if (MessageType == messageType.Question)
                    message = "Do you want to generate?\n" + displayMessage;
                else if (MessageType == messageType.Information)
                    message = displayMessage + "\nSuccessfully generated.";
            }
            else if (MessageAction == messageAction.Invalid)
            {
                message = displayMessage + " is invalid.";
            }
            else if (MessageAction == messageAction.OverwriteQty)
            {
                message = "Do you want to overwrite the qty ?\n if not untick the overwrite qty check box and continue";
            }
            else if (MessageAction == messageAction.SubTotalDiscountPercentageExceed)
            {
                message = "Sub total discount percentage cannot exceed 100";
            }
            else if (MessageAction == messageAction.SubTotalDiscountAmountExceed)
            {
                message = "Sub total discount amount cannot exceed gross amount";
            }
            else if (MessageAction == messageAction.ProductDiscountAmountExceedMinimum)
            {
                message = "Product discount amount cannot exceed minimum price";
            }
            else if (MessageAction == messageAction.ProductDiscountPercentageExceed)
            {
                message = "Product discount percentage cannot exceed 100";
            }
            else if (MessageAction == messageAction.ProductDiscountAmountExceed)
            {
                message = "Product discount amount cannot exceed product amount";
            }
            else if (MessageAction == messageAction.QtyExceed)
            {
                message = displayMessage + " qty cannot exceed current stock";
            }
            else if (MessageAction == messageAction.BatchQtyExceed)
            {
                message = displayMessage + " qty cannot exceed batch stock";
            }
            else if (MessageAction == messageAction.InvalidDate)
            {
                message = "Please select valid date";
            }
            else if (MessageAction == messageAction.InvalidDateRange)
            {
                message = "Please select valid date range";
            }
            else if (MessageAction == messageAction.AccessDenied)
            {
                if (MessageType == messageType.Information)
                {message = "Access denied";}
            }
            else if (MessageAction == messageAction.NotFound)
            {
                message = displayMessage + " not found.";

            }
            else if (MessageAction == messageAction.ZeroAmount)
            {
                message = displayMessage + " cant be zero.";

            }
            else if (MessageAction == messageAction.ZeroQty)
            {
                message = displayMessage + " cant be zero.";
            }
            else if (MessageAction == messageAction.InvalidTelephoneNumber)
            {
                message = "Invalid telephone number.";
            }
            else if (MessageAction == messageAction.InvalidEmailAddress)
            {
                message = "Invalid email address.";
            }
            else if (MessageAction == messageAction.GreaterThan)
            {
                message = string.Format("{0} cannot be greater than {1}.", displayMessage, optionalDisplayMessage );
            }
            else if (MessageAction == messageAction.ConfirmPassword)
            {
                message = "Password & comfirm password miss match";
            }
            else if (MessageAction == messageAction.Saved)
            {
                message = "Successfully saved"; 
            }
            else if (MessageAction == messageAction.Empty)
            {
                message = displayMessage + " cannot be empty";
            }
            else if (MessageAction == messageAction.ExitSystem)
            {
                message = "Are you sure you want to exit ?";
            }
            else if (MessageAction == messageAction.AlreadyExists)
            {
                message = displayMessage + " already exists";
            }
            else if (MessageAction == messageAction.Permission)
            {
                message = "You do not have Privileges to modify this \n" + displayMessage;
            }

            switch (MessageType)
            {
                case messageType.Error:
                    if (message.Equals(string.Empty))
                        message = "Error Found.";
                    //return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return MessageBox.Show(message, Common.Version, MessageBoxButtons.OK, MessageBoxIcon.Error);

                case messageType.Warning:
                    //return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return MessageBox.Show(message, Common.Version, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                case messageType.Information:
                    if (MessageAction == messageAction.Save)
                        //return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        return MessageBox.Show(message, Common.Version, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    else
                        //return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return MessageBox.Show(message, Common.Version, MessageBoxButtons.OK, MessageBoxIcon.Information);

                case messageType.Question:
                    //return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return MessageBox.Show(message, Common.Version, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                default:
                    return dialogResult;
            }
        }


        public static void ShowToast(string x)
        {
            Form frmToast = new Form();
        }

    }
}
