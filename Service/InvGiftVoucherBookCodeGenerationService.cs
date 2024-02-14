using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class InvGiftVoucherBookCodeGenerationService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Save new InvGiftVoucherMasterBook
        /// </summary>
        /// <param name="invGiftVoucherMasterBook"></param>
        public void AddInvGiftVoucherMasterBook(InvGiftVoucherBookCode invGiftVoucherMasterBook)
        {
            context.InvGiftVoucherBookCodes.Add(invGiftVoucherMasterBook);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing InvGiftVoucherMasterBook
        /// </summary>
        /// <param name="invGiftVoucherMasterBook"></param>
        public void UpdateInvGiftVoucherMasterBook(InvGiftVoucherBookCode invGiftVoucherMasterBook)
        {
            invGiftVoucherMasterBook.ModifiedUser = Common.LoggedUser;
            invGiftVoucherMasterBook.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(invGiftVoucherMasterBook).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search InvGiftVoucherMasterBook by code
        /// </summary>
        /// <param name="bookCode"></param>
        /// <returns></returns>
        public InvGiftVoucherBookCode GetInvGiftVoucherMasterBookByCode(string bookCode)
        {
            return context.InvGiftVoucherBookCodes.Where(u => u.BookCode == bookCode && u.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherBookCode GetInvGiftVoucherMasterBookByID(int bookID)
        {
            return context.InvGiftVoucherBookCodes.Where(u => u.InvGiftVoucherBookCodeID == bookID && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search InvGiftVoucherMasterBook by name
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public InvGiftVoucherBookCode GetInvGiftVoucherMasterBookByName(string bookName)
        {
            return context.InvGiftVoucherBookCodes.Where(u => u.BookName == bookName && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search GiftVoucherMasterBook by code
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="giftVoucherValue"></param>
        /// <returns></returns>
        public InvGiftVoucherBookCode GetInvGiftVoucherMasterBookByCodeValue(int groupID, decimal giftVoucherValue)
        {
            return context.InvGiftVoucherBookCodes.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == giftVoucherValue && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get InvGiftVoucherMasterBook Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvGiftVoucherBookGenerationCodesByVoucherType(int voucherType)
        {
            List<string> invGiftVoucherMasterBookCodesList = context.InvGiftVoucherBookCodes.Where(u => u.VoucherType.Equals(voucherType) && u.IsDelete.Equals(false)).Select(u => u.BookCode).ToList();
            string[] invGiftVoucherMasterBookCodes = invGiftVoucherMasterBookCodesList.ToArray();
            return invGiftVoucherMasterBookCodes;
        }

        public string[] GetInvGiftVoucherBookGenerationCodesByVoucherType(int voucherType,int bookCode)
        {
            List<string> invGiftVoucherMasterBookCodesList = context.InvGiftVoucherBookCodes.Where(u => u.VoucherType.Equals(voucherType) && u.IsDelete.Equals(false) && u.InvGiftVoucherBookCodeID.Equals(bookCode)).Select(u => u.BookCode).ToList();
            string[] invGiftVoucherMasterBookCodes = invGiftVoucherMasterBookCodesList.ToArray();
            return invGiftVoucherMasterBookCodes;
        }

        public string[] GetInvGiftVoucherBookGenerationCodesByVoucherType(int voucherType, string groupCode)
        {
            List<string> invGiftVoucherMasterBookCodesList =
                (from u in context.InvGiftVoucherBookCodes 
                join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                where u.IsDelete == false && u.VoucherType == voucherType && g.GiftVoucherGroupCode == groupCode
            select u.BookCode).ToList();

            string[] invGiftVoucherMasterBookCodes = invGiftVoucherMasterBookCodesList.ToArray();
            return invGiftVoucherMasterBookCodes;
        }

        /// <summary>
        /// Get InvGiftVoucherMasterBook Names
        /// </summary>
        /// <returns></returns>
        public string[] GetInvGiftVoucherBookGenerationNamesByVoucherType(int voucherType)
        {
            List<string> giftVoucherMasterBookNamesList = context.InvGiftVoucherBookCodes.Where(u => u.IsDelete.Equals(false) && u.VoucherType.Equals(voucherType)).Select(u => u.BookName).ToList();
            string[] invGiftVoucherMasterBookNames = giftVoucherMasterBookNamesList.ToArray();
            return invGiftVoucherMasterBookNames;
        }

        public string[] GetInvGiftVoucherBookGenerationNamesByVoucherType(int voucherType,int bookCode)
        {
            List<string> giftVoucherMasterBookNamesList = context.InvGiftVoucherBookCodes.Where(u => u.IsDelete.Equals(false) && u.VoucherType.Equals(voucherType) && u.InvGiftVoucherBookCodeID.Equals(bookCode)).Select(u => u.BookName).ToList();
            string[] invGiftVoucherMasterBookNames = giftVoucherMasterBookNamesList.ToArray();
            return invGiftVoucherMasterBookNames;
        }

        public string[] GetInvGiftVoucherBookGenerationNamesByVoucherType(int voucherType, string groupCode)
        {
            List<string> giftVoucherMasterBookNamesList =
                (from u in context.InvGiftVoucherBookCodes
                 join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                 where u.IsDelete == false && u.VoucherType == voucherType && g.GiftVoucherGroupCode == groupCode
                 select u.BookCode).ToList();

            string[] invGiftVoucherMasterBookNames = giftVoucherMasterBookNamesList.ToArray();
            return invGiftVoucherMasterBookNames;
        }

        /// <summary>
        /// Get active Gift Voucher Master Group Value details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveGiftVoucherMasterBooksDataTableByVoucherType(int voucherType)
        {
            DataTable tblGiftVoucherMasterBooks = new DataTable();
            var query = from u in context.InvGiftVoucherBookCodes where (u.IsDelete == false && u.VoucherType == voucherType)  select new { u.BookCode, u.BookName, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.GiftVoucherValue, u.PageCount };
            return tblGiftVoucherMasterBooks = Common.LINQToDataTable(query);
        }

        #region GetNewCode
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.InvGiftVoucherBookCodes.Max(a => a.BookCode);
            if (GetNewCode != null)
            {
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            else
            {
                GetNewCode = "0";
            }
            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }
        #endregion
        #endregion
    }
}
