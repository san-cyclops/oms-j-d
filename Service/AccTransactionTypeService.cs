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
    public class AccTransactionTypeService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Save new AccTransactionTypeDetail
        /// </summary>
        /// <param name="accTransactionTypeDetail"></param>
        public void AddAccCardCommission(AccTransactionTypeDetail accTransactionTypeDetail)
        {
            context.AccTransactionTypeDetails.Add(accTransactionTypeDetail);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing AccTransactionTypeDetail
        /// </summary>
        /// <param name="accTransactionTypeDetail"></param>
        public void UpdateAccCardCommission(AccTransactionTypeDetail accTransactionTypeDetail)
        {
            accTransactionTypeDetail.ModifiedUser = Common.LoggedUser;
            accTransactionTypeDetail.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(accTransactionTypeDetail).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search AccTransactionTypeDetail by ID
        /// </summary>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        public AccTransactionTypeDetail GetAccTransactionTypeDetailByID(long transactionID)
        {
            return context.AccTransactionTypeDetails.Where(l => l.AccTransactionTypeHeaderID == transactionID && l.IsDelete == false).FirstOrDefault();
        }

        public AccTransactionTypeDetail GetAccTransactionTypeDetailByTypeHeaderID(long accTransactionTypeHeaderID)
        {
            return context.AccTransactionTypeDetails.Where(l => l.AccTransactionTypeHeaderID == accTransactionTypeHeaderID && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get all AccTransactionTypeDetail details 
        /// </summary>
        /// <returns>AccTransactionTypeDetail</returns>
        public List<AccTransactionTypeDetail> GetAllAccTransactionTypeDetails()
        {
            return context.AccTransactionTypeDetails.Where(u => u.IsDelete == false).ToList();
        }

        /// <summary>
        /// Get all AccTransactionTypeDetail details 
        /// </summary>
        /// <returns>AccTransactionTypeDetail</returns>
        public AccTransactionTypeDetail GetAllAccTransactionTypeDetailsByTransactionID(int transactionID)
        {
            return context.AccTransactionTypeDetails.Where(u => u.IsDelete == false && u.AccTransactionTypeHeaderID.Equals(transactionID)).FirstOrDefault();
        }

        /// <summary>
        /// Get all AccTransactionTypeDetail details 
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="definition"></param>
        /// <returns>AccTransactionTypeDetail</returns>
        public AccTransactionTypeDetail GetAllAccTransactionTypeDetailsByTransactionIDDefinition(int transactionID, int definition)
        {
            return context.AccTransactionTypeDetails.Where(u => u.IsDelete == false && u.AccTransactionTypeHeaderID.Equals(transactionID) && u.TransactionDefinition.Equals(definition)).FirstOrDefault();
        }
        #endregion
    }
}
