using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;
using Utility;

namespace Service
{
    public class BankBranchService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new BankBranch
        /// </summary>
        /// <param name="bankBranch"></param>
        public void AddBankBranch(BankBranch bankBranch)
        {
            context.BankBranches.Add(bankBranch);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing BankBranch
        /// </summary>
        /// <param name="bankBranch"></param>
        public void UpdateBankBranch(BankBranch bankBranch)
        {
            bankBranch.ModifiedUser = Common.LoggedUser;
            bankBranch.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(bankBranch).State = EntityState.Modified;
            context.SaveChanges();
        }

        ///// <summary>
        ///// Get BankBranch Codes
        ///// </summary>
        ///// <param name="bankID"></param>
        ///// <returns></returns>
        //public string[] GetBankBranchCodes(int bankID)
        //{
        //    List<string> bankBranchesList = context.BankBranches.Where(l => l.IsDelete.Equals(false) && l.BankID.Equals(bankID)).Select(l => l.BankBranchCode).ToList();
        //    string[] bankBranchCodes = bankBranchesList.ToArray();
        //    return bankBranchCodes;
        //}

        ///// <summary>
        ///// Get BankBranch Names
        ///// </summary>
        ///// <param name="bankID"></param>
        ///// <returns></returns>
        //public string[] GetBankBranchNames(int bankID)
        //{
        //    List<string> bankBranchesList = context.Banks.Where(l => l.IsDelete.Equals(false) && l.BankID.Equals(bankID)).Select(l => l.BankName).ToList();
        //    string[] bankBranchNames = bankBranchesList.ToArray();
        //    return bankBranchNames;
        //}

        /// <summary>
        /// Get BankBranch Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetBranchCodes()
        {
            List<string> bankBranchesList = context.BankBranches.Where(l => l.IsDelete.Equals(false)).Select(l => l.BankBranchCode).ToList();
            string[] bankBranchCodes = bankBranchesList.ToArray();
            return bankBranchCodes;
        }

        /// <summary>
        /// Get BankBranch Names
        /// </summary>
        /// <returns></returns>
        public string[] GetBranchNames()
        {
            List<string> bankBranchesList = context.BankBranches.Where(l => l.IsDelete.Equals(false)).Select(l => l.BranchName).ToList();
            string[] bankBranchNames = bankBranchesList.ToArray();
            return bankBranchNames;
        }

        /// <summary>
        /// Search BankBranch by code
        /// </summary>
        /// <param name="bankBranchCode"></param>
        /// <returns></returns>
        public BankBranch GetBankBranchByCode(string bankBranchCode)
        {
            return context.BankBranches.Where(l => l.BankBranchCode == bankBranchCode && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search BankBranch by ID
        /// </summary>
        /// <param name="bankBranchID"></param>
        /// <returns></returns>
        public BankBranch GetBankBranchByID(long bankBranchID)
        {
            return context.BankBranches.Where(l => l.BankBranchID == bankBranchID && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search BankBranch by name
        /// </summary>
        /// <param name="bankBranchName"></param>
        /// <returns></returns>
        public BankBranch GetBankBranchByName(string bankBranchName)
        {
            return context.BankBranches.Where(l => l.BranchName == bankBranchName && l.IsDelete == false).FirstOrDefault();
        }
        #endregion
    }
}
