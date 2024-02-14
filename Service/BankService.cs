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
    public class BankService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new Bank
        /// </summary>
        /// <param name="bank"></param>
        public void AddBank(Bank bank)
        {
            context.Banks.Add(bank);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Bank
        /// </summary>
        /// <param name="bank"></param>
        public void UpdateBank(Bank bank)
        {
            bank.ModifiedUser = Common.LoggedUser;
            bank.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(bank).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Get Bank Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetBankCodes()
        {
            List<string> banksList = context.Banks.Where(l => l.IsDelete.Equals(false)).Select(l => l.BankCode).ToList();
            string[] bankCodes = banksList.ToArray();
            return bankCodes;
        }

        /// <summary>
        /// Get Bank Names
        /// </summary>
        /// <returns></returns>
        public string[] GetBankNames()
        {
            List<string> banksList = context.Banks.Where(l => l.IsDelete.Equals(false)).Select(l => l.BankName).ToList();
            string[] bankNames = banksList.ToArray();
            return bankNames;
        }

        /// <summary>
        /// Search Bank by code
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public Bank GetBankByCode(string bankCode)
        {
            return context.Banks.Where(l => l.BankCode == bankCode && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Bank by ID
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public Bank GetBankByID(long bankID)
        {
            return context.Banks.Where(l => l.BankID == bankID && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Bank by name
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public Bank GetBankByName(string bankName)
        {
            return context.Banks.Where(l => l.BankName == bankName && l.IsDelete == false).FirstOrDefault();
        }

        public List<Bank> GetAllBanks()
        {
            return context.Banks.Where(c => c.IsDelete == false).ToList();
            
        }

        #endregion
    }
}
