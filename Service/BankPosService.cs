using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class BankPosService
    {
        #region Methods
        /// <summary>
        /// Get Bankspos Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetBanksposCodes()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> bankbankBankPosList = context.Bankspos.Where(l => l.IsDelete.Equals(false)).Select(l => l.BankID.ToString()).ToList();
                string[] bankbankBankPosCodes = bankbankBankPosList.ToArray();
                return bankbankBankPosCodes;
            }
        }

        /// <summary>
        /// Get Bankspos Names
        /// </summary>
        /// <returns></returns>
        public string[] GetBankPosNames()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> bankBankPosList = context.Bankspos.Where(l => l.IsDelete.Equals(false)).Select(l => l.Bank).ToList();
                string[] bankbankBankPosNames = bankBankPosList.ToArray();
                return bankbankBankPosNames;
            }
        }

        /// <summary>
        /// Search BankPos by code
        /// </summary>
        /// <param name="bankPosCode"></param>
        /// <returns></returns>
        public BankPos GetBankPosByCode(int bankPosCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.Bankspos.Where(l => l.BankID == bankPosCode).FirstOrDefault();
            }
        }

        /// <summary>
        /// Search BankPos by ID
        /// </summary>
        /// <param name="bankPosID"></param>
        /// <returns></returns>
        public BankPos GetBankPosByID(long bankPosID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.Bankspos.Where(l => l.BankPosID == bankPosID && l.IsDelete == false).FirstOrDefault();
            }
        }

        /// <summary>
        /// Search BankPos by name
        /// </summary>
        /// <param name="bankPosName"></param>
        /// <returns></returns>
        public BankPos GetBankPosByName(string bankPosName)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.Bankspos.Where(l => l.Bank == bankPosName && l.IsDelete == false).FirstOrDefault();
            }
        }
        #endregion
    }
}
