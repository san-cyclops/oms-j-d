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
    public class AccCardCommissionService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new AccCardCommission
        /// </summary>
        /// <param name="accCardCommission"></param>
        public void AddAccCardCommission(AccCardCommission accCardCommission)
        {
            context.AccCardCommissions.Add(accCardCommission);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing AccCardCommission
        /// </summary>
        /// <param name="accCardCommission"></param>
        public void UpdateAccCardCommission(AccCardCommission accCardCommission)
        {
            accCardCommission.ModifiedUser = Common.LoggedUser;
            accCardCommission.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(accCardCommission).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search AccCardCommission by ID
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public AccCardCommission GetAccCardCommissionByID(long bankID)
        {
            return context.AccCardCommissions.Where(l => l.BankLedgerID == bankID && l.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get all AccCardCommission details 
        /// </summary>
        /// <returns>AccCardCommission</returns>
        public List<AccCardCommission> GetAllAccTransactionTypeDetails()
        {
            return context.AccCardCommissions.Where(u => u.IsDelete == false).ToList();
        }
        #endregion
    }
}
