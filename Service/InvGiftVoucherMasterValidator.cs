using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class InvGiftVoucherMasterValidator
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


        public bool ValidateNoOfVouchers(int voucherCount, int noOfPagesOnBook)
        {
            bool isValidated = false;

            if (voucherCount > 0 && noOfPagesOnBook > 0)
            {
                if ((voucherCount % noOfPagesOnBook) != 0)
                { isValidated = false; }
                else
                { isValidated = true; }
            }
            else
            { isValidated = false; }

            return isValidated;
        }

        public bool ValidateBook(int groupID, decimal voucherValue)
        {
            bool isValidated = false;
           // return context.InvGiftVoucherGroups.Where(u => u.InvGiftVoucherGroupID == groupID).FirstOrDefault();

            return isValidated;
        }

        public bool ValidateExistsVoucherSerial(int groupID, decimal voucherValue, int startingNo, int noOfVouchers)
        {
            bool isValidated = false;
            // return context.InvGiftVoucherGroups.Where(u => u.InvGiftVoucherGroupID == groupID).FirstOrDefault();

            InvGiftVoucherMaster invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == voucherValue).FirstOrDefault();

            if (invGiftVoucherMaster != null)
            {
                int endSerialNo = startingNo + noOfVouchers;

                //int voucherExistsCount = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == voucherValue 
                //    && (u.VoucherSerialNo >= startingNo || u.VoucherSerialNo <= endSerialNo)).Count();

                for (int s = startingNo; s <= endSerialNo; s++)
                {
                    invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == voucherValue && u.VoucherSerialNo == s).FirstOrDefault();

                    if (invGiftVoucherMaster != null)
                    {
                        isValidated = false;
                        return isValidated;
                    }
                    else
                    {
                        isValidated = true;
                    }
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
