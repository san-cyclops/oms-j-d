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
    public class InvGiftVoucherGroupService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Save new InvGiftVoucherGroup
        /// </summary>
        /// <param name="invGiftVoucherMasterGroup"></param>
        public void AddInvGiftVoucherGroup(InvGiftVoucherGroup invGiftVoucherMasterGroup)
        {
            context.InvGiftVoucherGroups.Add(invGiftVoucherMasterGroup);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing InvGiftVoucherGroup
        /// </summary>
        /// <param name="invGiftVoucherMasterGroup"></param>
        public void UpdateInvGiftVoucherGroup(InvGiftVoucherGroup invGiftVoucherMasterGroup)
        {
            invGiftVoucherMasterGroup.ModifiedUser = Common.LoggedUser;
            invGiftVoucherMasterGroup.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(invGiftVoucherMasterGroup).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search InvGiftVoucherGroup by code
        /// </summary>
        /// <param name="giftVoucherMasterGroupCode"></param>
        /// <returns></returns>
        public InvGiftVoucherGroup GetInvGiftVoucherGroupByCode(string giftVoucherMasterGroupCode)
        {
            return context.InvGiftVoucherGroups.Where(u => u.GiftVoucherGroupCode == giftVoucherMasterGroupCode && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search InvGiftVoucherGroup by name
        /// </summary>
        /// <param name="giftVoucherMasterGroupName"></param>
        /// <returns></returns>
        public InvGiftVoucherGroup GetInvGiftVoucherGroupByName(string giftVoucherMasterGroupName)
        {
            return context.InvGiftVoucherGroups.Where(u => u.GiftVoucherGroupName == giftVoucherMasterGroupName && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search InvGiftVoucherGroup by ID
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public InvGiftVoucherGroup GetInvGiftVoucherGroupByID(long groupID)
        {
            return context.InvGiftVoucherGroups.Where(u => u.InvGiftVoucherGroupID == groupID).FirstOrDefault();
        }

        /// <summary>
        /// Get InvGiftVoucherGroup Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvGiftVoucherGroupCodes()
        {
            List<string> invGiftVoucherMasterGroupCodesList = context.InvGiftVoucherGroups.Where(u => u.IsDelete.Equals(false)).Select(u => u.GiftVoucherGroupCode).ToList();
            string[] invGiftVoucherMasterGroupCodes = invGiftVoucherMasterGroupCodesList.ToArray();
            return invGiftVoucherMasterGroupCodes;
        }

        /// <summary>
        /// Get InvGiftVoucherGroup Names
        /// </summary>
        /// <returns></returns>
        public string[] GetInvGiftVoucherGroupNames()
        {
            List<string> invGiftVoucherMasterGroupNamesList = context.InvGiftVoucherGroups.Where(u => u.IsDelete.Equals(false)).Select(u => u.GiftVoucherGroupName).ToList();
            string[] invGiftVoucherMasterGroupNames = invGiftVoucherMasterGroupNamesList.ToArray();
            return invGiftVoucherMasterGroupNames;
        }

        /// <summary>
        /// Get active Gift Voucher Master Group details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveGiftVoucherGroupsDataTable()
        {
            DataTable tblGiftVoucherGroups = new DataTable();
            var query = from u in context.InvGiftVoucherGroups where u.IsDelete == false select new { u.GiftVoucherGroupCode, u.GiftVoucherGroupName };
            return tblGiftVoucherGroups = Common.LINQToDataTable(query);
        }

        #region GetNewCode
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.InvGiftVoucherGroups.Max(a => a.GiftVoucherGroupCode);
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
