using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity.Validation;
using Utility;

namespace Service
{
    public class AccLedgerAccountService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Get Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetLedgerCodes()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false)).Select(l => l.LedgerCode).ToList();
            string[] ledgerCodes = ledgersList.ToArray();
            return ledgerCodes;
        }

        public string[] GetLedgerNames()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false)).Select(l => l.LedgerName).ToList();
            string[] ledgerNames = ledgersList.ToArray();
            return ledgerNames;
        }

        /// <summary>
        /// Get Expense Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetExpenceLedgerCodes()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.LedgerType.Equals("5")).Select(l => l.LedgerCode).ToList();
            string[] ledgerCodes = ledgersList.ToArray();
            return ledgerCodes;
        }

        /// <summary>
        /// Get Expense Ledger Names
        /// </summary>
        /// <returns></returns>
        public string[] GetExpenceLedgerNames()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.LedgerType.Equals("5")).Select(l => l.LedgerName).ToList();
            string[] ledgerNames = ledgersList.ToArray();
            return ledgerNames;
        }

        /// <summary>
        /// Get Petty Cash Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetPettyCashLedgerCodes()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(3)).Select(l => l.LedgerCode).ToList();
            string[] ledgerCodes = ledgersList.ToArray();
            return ledgerCodes;
        }

        /// <summary>
        /// Get Petty Cash Ledger Names
        /// </summary>
        /// <returns></returns>
        public string[] GetPettyCashLedgerNames()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(3)).Select(l => l.LedgerName).ToList();
            string[] ledgerNames = ledgersList.ToArray();
            return ledgerNames;
        }

        /// <summary>
        /// Get Cash Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetCashBookLedgerCodes()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(2)).Select(l => l.LedgerCode).ToList();
            string[] ledgerCodes = ledgersList.ToArray();
            return ledgerCodes;
        }

        /// <summary>
        /// Get Cash Ledger Names
        /// </summary>
        /// <returns></returns>
        public string[] GetCashBookLedgerNames()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(2)).Select(l => l.LedgerName).ToList();
            string[] ledgerNames = ledgersList.ToArray();
            return ledgerNames;
        }

        /// <summary>
        /// Get Bank Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetBankBookLedgerCodes()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(1)).Select(l => l.LedgerCode).ToList();
            string[] ledgerCodes = ledgersList.ToArray();
            return ledgerCodes;
        }

        /// <summary>
        /// Get Bank Ledger Names
        /// </summary>
        /// <returns></returns>
        public string[] GetBankBookLedgerNames()
        {
            List<string> ledgersList = context.AccLedgerAccounts.Where(l => l.IsDelete.Equals(false) && l.AccountStatus.Equals(1)).Select(l => l.LedgerName).ToList();
            string[] ledgerNames = ledgersList.ToArray();
            return ledgerNames;
        }

        /// <summary>
        /// Search AccLedgerAccount by code
        /// </summary>
        /// <param name="ledgerCode"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerAccountByCode(string ledgerCode)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerCode == ledgerCode && l.IsDelete == false).FirstOrDefault();
        }

        public List<AccLedgerAccount> GetBankList()
        {
            return context.AccLedgerAccounts.Where(l => l.AccountStatus.Equals(1) && l.IsDelete == false).ToList();
        }

        /// <summary>
        /// Search AccLedgerAccount by ID
        /// </summary>
        /// <param name="ledgerID"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerAccountByID(long ledgerID)
        {
            return context.AccLedgerAccounts.Where(l => l.AccLedgerAccountID == ledgerID && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search AccLedgerAccount by name
        /// </summary>
        /// <param name="ledgerName"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerAccountByName(string ledgerName)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerName == ledgerName && l.IsDelete == false).FirstOrDefault();
        }

        public AccLedgerAccount GetAccLedgerExpenseAccountByID(long ledgerID)
        {
            return context.AccLedgerAccounts.Where(l => l.AccLedgerAccountID == ledgerID && l.IsDelete == false && l.LedgerType.Equals("5")).FirstOrDefault();
        }

        /// <summary>
        /// Search AccLedgerAccount by name
        /// </summary>
        /// <param name="ledgerName"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerExpenseAccountByName(string ledgerName)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerName == ledgerName && l.IsDelete == false && l.LedgerType.Equals("5")).FirstOrDefault();
        }

        public AccLedgerAccount GetAccLedgerExpenseAccountByCode(string ledgerCode)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerCode == ledgerCode && l.IsDelete == false && l.LedgerType.Equals("5")).FirstOrDefault();
        }

        /// <summary>
        /// Get active PettyCashAccounts details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveExpenseAccountsDataTable()
        {
            DataTable tblExpenseAccounts = new DataTable();
            var query = from u in context.AccLedgerAccounts where u.IsDelete == false && u.LedgerType.Equals("5") select new { u.LedgerCode, u.LedgerName };
            return tblExpenseAccounts = Common.LINQToDataTable(query);
        }

        /// <summary>
        /// Search Petty Cash AccLedgerAccount by ID
        /// </summary>
        /// <param name="ledgerID"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerPettyCashAccountByID(long ledgerID)
        {
            return context.AccLedgerAccounts.Where(l => l.AccLedgerAccountID == ledgerID && l.IsDelete == false && l.AccountStatus.Equals(3)).FirstOrDefault();
        }

        /// <summary>
        /// Search AccLedgerAccount by name
        /// </summary>
        /// <param name="ledgerName"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerPettyCashAccountByName(string ledgerName)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerName == ledgerName && l.IsDelete == false && l.AccountStatus.Equals(3)).FirstOrDefault();
        }

        /// <summary>
        /// Search Petty Cash AccLedgerAccount by Code
        /// </summary>
        /// <param name="ledgerCode"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerPettyCashAccountByCode(string ledgerCode)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerCode == ledgerCode && l.IsDelete == false && l.AccountStatus.Equals(3)).FirstOrDefault();
        }


        /// <summary>
        /// Search BankBook AccLedgerAccount by ID
        /// </summary>
        /// <param name="ledgerID"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerBankBookAccountByID(long ledgerID)
        {
            return context.AccLedgerAccounts.Where(l => l.AccLedgerAccountID == ledgerID && l.IsDelete == false && l.AccountStatus.Equals(1)).FirstOrDefault();
        }

        /// <summary>
        /// Search AccLedgerAccount by name
        /// </summary>
        /// <param name="ledgerName"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerBankBookAccountByName(string ledgerName)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerName == ledgerName && l.IsDelete == false && l.AccountStatus.Equals(1)).FirstOrDefault();
        }

        /// <summary>
        /// Search BankBook AccLedgerAccount by Code
        /// </summary>
        /// <param name="ledgerCode"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerBankBookAccountByCode(string ledgerCode)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerCode == ledgerCode && l.IsDelete == false && l.AccountStatus.Equals(1)).FirstOrDefault();
        }

        public List<AccLedgerAccount> GetPettyCashBookList()
        {
            return context.AccLedgerAccounts.Where(l => l.AccountStatus.Equals(3) && l.IsDelete == false).ToList();
        }

        /// <summary>
        /// Get active PettyCashAccounts details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActivePettyCashAccountsDataTable(int locationID)
        {
            DataTable tblPettyCashAccounts = new DataTable();
            var query = from u in context.AccLedgerAccounts
                        join m in context.AccPettyCashMasters on u.AccLedgerAccountID equals m.LedgerID
                        where u.IsDelete == false && u.AccountStatus.Equals(3) && m.LocationID.Equals(locationID)
                        select new { u.LedgerCode, u.LedgerName };

            return tblPettyCashAccounts = Common.LINQToDataTable(query);
        }

        /////
        /// /// <summary>
        /// Search CashBook AccLedgerAccount by ID
        /// </summary>
        /// <param name="ledgerID"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerCashBookAccountByID(long ledgerID)
        {
            return context.AccLedgerAccounts.Where(l => l.AccLedgerAccountID == ledgerID && l.IsDelete == false && l.AccountStatus.Equals(2)).FirstOrDefault();
        }

        /// <summary>
        /// Search AccLedgerAccount by name
        /// </summary>
        /// <param name="ledgerName"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerCashBookAccountByName(string ledgerName)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerName == ledgerName && l.IsDelete == false && l.AccountStatus.Equals(2)).FirstOrDefault();
        }

        /// <summary>
        /// Search CashBook AccLedgerAccount by Code
        /// </summary>
        /// <param name="ledgerCode"></param>
        /// <returns></returns>
        public AccLedgerAccount GetAccLedgerCashBookAccountByCode(string ledgerCode)
        {
            return context.AccLedgerAccounts.Where(l => l.LedgerCode == ledgerCode && l.IsDelete == false && l.AccountStatus.Equals(2)).FirstOrDefault();
        }

        public List<AccLedgerAccount> GetCashBookBookList()
        {
            return context.AccLedgerAccounts.Where(l => l.AccountStatus.Equals(2) && l.IsDelete == false).ToList();
        }

        /// <summary>
        /// Get active CashBook Accounts details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveCashBookAccountsDataTable()
        {
            DataTable tblPettyCashAccounts = new DataTable();
            var query = from u in context.AccLedgerAccounts where u.IsDelete == false && u.AccountStatus.Equals(2) select new { u.LedgerCode, u.LedgerName };
            return tblPettyCashAccounts = Common.LINQToDataTable(query);
        }
        #endregion
    }
}
