using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ErrorMessage
    {       

        public enum enumMessageType
        {
            Error,
            Warning,
            Information,
            Question,
        }

        private void selectMessage(enumMessageType MessageType)
        {
            switch (MessageType)
            {
                case enumMessageType.Error:
                    const string cannectionFailed = "Cannection Failed.";
                    break;
                case enumMessageType.Information:                    
                    const string savedSuccessfully = "Record saved successfully.";
                    const string modifiedSuccessfully = "Record modified successfully.";
                    const string deletedSuccessfully = "Record deleted successfully.";
                    const string printedSuccessfully = "Report printed successfully.";
                    const string processedSuccessfully = "Record processed successfully.";
                    const string alreadySuccessfully = "Record already exists.";
                    const string cancelledSuccessfully = "Cancelled by user.";               
                    break;
                case enumMessageType.Question:
                    const string confirmsaverecord ="Confirm to save record.";
                    const string confirmmodifyrecord = "Confirm to modify record";
                    const string confirmprintreport   = "Confirm to print report.";
                    const string confirmdeleterecord = "Confirm to delete record.";
                    const string confirmprocessrecord = "Confirm to process record.";
                    break;
                case enumMessageType.Warning:
                    const string warningCode = "Invalid Code Length.Code description must be.";
                    const string warningCannobenull = "Cannot be null.";
                    const string warningCannotbeempty = "Cannot be empty.";
                    const string warningCannotbezero = "Cannot be zero.";
                    const string warningGreaterthan = "Should be greater than .";                    
                    const string warningLesserthan = "Should be lesser than .";
                    const string warningCannotbe = "Cannot be."; 
                    break;                
            }
        }

    }


}
