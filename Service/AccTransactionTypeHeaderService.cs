using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class AccTransactionTypeHeaderService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Get AccTransactionTypeHeader Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetAccTransactionTypeHeaderCodes()
        {
            List<string> transactionsList = context.AccTransactionTypeHeaders.Where(l => l.IsDelete.Equals(false)).Select(l => l.TransactionCode).ToList();
            string[] transactionCodes = transactionsList.ToArray();
            return transactionCodes;
        }

        /// <summary>
        /// Get AccTransactionTypeHeader Names
        /// </summary>
        /// <returns></returns>
        public string[] GetAccTransactionTypeHeaderNames()
        {
            List<string> transactionsList = context.AccTransactionTypeHeaders.Where(l => l.IsDelete.Equals(false)).Select(l => l.TransactionName).ToList();
            string[] transactionNames = transactionsList.ToArray();
            return transactionNames;
        }

        /// <summary>
        /// Search AccTransactionTypeHeader by code
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public AccTransactionTypeHeader GetAccTransactionTypeHeaderByCode(string transactionCode)
        {
            return context.AccTransactionTypeHeaders.Where(l => l.TransactionCode == transactionCode && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search AccTransactionTypeHeader by ID
        /// </summary>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        public AccTransactionTypeHeader GetAccTransactionTypeHeaderByID(long transactionID)
        {
            return context.AccTransactionTypeHeaders.Where(l => l.AccTransactionTypeHeaderID == transactionID && l.IsDelete == false).FirstOrDefault();
        }

        
        /// <summary>
        /// Search AccTransactionTypeHeader by name
        /// </summary>
        /// <param name="transactionName"></param>
        /// <returns></returns>
        public AccTransactionTypeHeader GetAccTransactionTypeHeaderByName(string transactionName)
        {
            return context.AccTransactionTypeHeaders.Where(l => l.TransactionName == transactionName && l.IsDelete == false).FirstOrDefault();
        }
        #endregion
    }
}
