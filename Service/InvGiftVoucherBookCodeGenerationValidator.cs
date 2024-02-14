using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class InvGiftVoucherBookCodeGenerationValidator
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Validate Length Of Starting No
        /// </summary>
        /// <param name="length"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ValidateLength(int length, string code)
        {
            bool isValidated = false;

            if (code.Length > 0)
            {
                if (length < code.Length)
                { isValidated = false; }
                else
                { isValidated = true; }
            }
            else
            { isValidated = false; }

            return isValidated;
        }

        public bool ValidateDelete()
        {
            return true;
        }

        public bool ValidateExistsGiftVoucherMaster(int voucherType, int bookID, decimal voucherValue)
        {
            bool isValidated = false;

            InvGiftVoucherMaster invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID).FirstOrDefault();

            if (invGiftVoucherMaster != null)
            {
                if (voucherType == 1 && invGiftVoucherMaster.GiftVoucherValue == voucherValue)
                {
                    isValidated = true;
                }
                else if (voucherType == 2 && invGiftVoucherMaster.GiftVoucherPercentage == voucherValue)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    return isValidated;
                }
            }
            else
            {
                isValidated = true;
            }

            return isValidated;
        }

        #endregion
    }
}
