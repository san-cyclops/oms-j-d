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
    public class AccPettyCashMasterService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Save new AccPettyCashMaster
        /// </summary>
        /// <param name="accPettyCashMaster"></param>
        public void AddAccPettyCashMaster(AccPettyCashMaster accPettyCashMaster)
        {
            context.AccPettyCashMasters.Add(accPettyCashMaster);
            context.SaveChanges();

            //AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            //AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            //accPettyCashImprestDetail.CompanyID = accPettyCashMaster.CompanyID;
            //accPettyCashImprestDetail.LocationID = accPettyCashMaster.LocationID;
            //accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashMaster.AccLedgerAccountID;
            //accPettyCashImprestDetail.AvailabledAmount = accPettyCashMaster.Amount;
            //accPettyCashImprestDetail.BalanceAmount = accPettyCashMaster.Amount;
            //accPettyCashImprestDetail.DocumentStatus = 1;
            //accPettyCashImprestDetail.CreatedUser = accPettyCashMaster.CreatedUser;
            //accPettyCashImprestDetail.GroupOfCompanyID = accPettyCashMaster.GroupOfCompanyID;
            //accPettyCashImprestDetail.CompanyID = accPettyCashMaster.CompanyID;
            //accPettyCashImprestDetail.LocationID = accPettyCashMaster.LocationID;
            
            //accPettyCashImprestService.AddPettyCashImprest(accPettyCashImprestDetail);
        }

        /// <summary>
        /// Update existing AccPettyCashMaster
        /// </summary>
        /// <param name="accPettyCashMaster"></param>
        public void UpdateAccPettyCashMaster(AccPettyCashMaster accPettyCashMaster)
        {
            accPettyCashMaster.ModifiedUser = Common.LoggedUser;
            accPettyCashMaster.ModifiedDate = Common.GetSystemDateWithTime(); 
            context.Entry(accPettyCashMaster).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search AccPettyCashMaster by Ledger ID
        /// </summary>
        /// <param name="ledgerID"></param>
        /// <returns></returns>
        public AccPettyCashMaster GetAccPettyCashMasterByLedgerID(long ledgerID, int locationID)
        {
            return context.AccPettyCashMasters.Where(u => u.LedgerID == ledgerID && u.LocationID == locationID && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get Petty Cash Ledger Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetPettyCashLedgerCodesByLocationID(int locationID)
        {
            string[] ledgersList = (from pm in context.AccPettyCashMasters
                                            join ld in context.AccLedgerAccounts on pm.LedgerID equals ld.AccLedgerAccountID
                                            where pm.LocationID.Equals(locationID) && pm.IsDelete == false
                                            select ld.LedgerCode).ToArray();
            return ledgersList;
        }

        /// <summary>
        /// Get Petty Cash Ledger Names
        /// </summary>
        /// <returns></returns>
        public string[] GetPettyCashLedgerNamesByLocationID(int locationID)
        {
            string[] ledgersList = (from pm in context.AccPettyCashMasters
                                    join ld in context.AccLedgerAccounts on pm.LedgerID equals ld.AccLedgerAccountID
                                    where pm.LocationID.Equals(locationID) && pm.IsDelete == false
                                    select ld.LedgerName).ToArray();
            return ledgersList;
        }
        #endregion
    }
}
