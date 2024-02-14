using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using Data;
using System.Data.Entity;
using Domain;

namespace Service
{
    public class AccPettyCashImprestService
    {
        public bool AddPettyCashImprest(AccPettyCashImprestDetail accPettyCashImprestDetail)
        {
            bool isSuccess = false;
            using (ERPDbContext context = new ERPDbContext())
            {
                context.AccPettyCashImprestDetails.Add(accPettyCashImprestDetail);
                context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        /// <summary>
        /// Update PettyCashImprest
        /// </summary>
        /// <param name="updatedAccPettyCashImprestDetail"></param>
        public bool UpdatePettyCashImprest(AccPettyCashImprestDetail updatedAccPettyCashImprestDetail)
        {
            AccPettyCashImprestDetail accPettyCashImprestDetail;
            using (ERPDbContext context = new ERPDbContext())
            {
                accPettyCashImprestDetail = (from s in context.AccPettyCashImprestDetails
                                             where s.PettyCashLedgerID == updatedAccPettyCashImprestDetail.PettyCashLedgerID &&
                                                   s.LocationID == updatedAccPettyCashImprestDetail.LocationID
                                             select s).FirstOrDefault<AccPettyCashImprestDetail>();

                if (accPettyCashImprestDetail != null)
                {
                    if (updatedAccPettyCashImprestDetail.UsedAmount > accPettyCashImprestDetail.BalanceAmount)
                    {
                        accPettyCashImprestDetail.UsedAmount = accPettyCashImprestDetail.UsedAmount + updatedAccPettyCashImprestDetail.UsedAmount;
                        accPettyCashImprestDetail.BalanceAmount = accPettyCashImprestDetail.BalanceAmount - updatedAccPettyCashImprestDetail.IssuedAmount;
                    }
                    else
                    {
                        accPettyCashImprestDetail.UsedAmount = accPettyCashImprestDetail.UsedAmount + updatedAccPettyCashImprestDetail.IssuedAmount;
                        accPettyCashImprestDetail.BalanceAmount = accPettyCashImprestDetail.BalanceAmount - updatedAccPettyCashImprestDetail.IssuedAmount;
                    }

                    if (updatedAccPettyCashImprestDetail.UsedAmount > accPettyCashImprestDetail.AvailabledAmount)
                    {
                        //accPettyCashImprestDetail.UsedAmount = accPettyCashImprestDetail.UsedAmount + updatedAccPettyCashImprestDetail.IssuedAmount;
                        accPettyCashImprestDetail.IssuedAmount = accPettyCashImprestDetail.IssuedAmount + updatedAccPettyCashImprestDetail.IssuedAmount;
                        accPettyCashImprestDetail.AvailabledAmount = accPettyCashImprestDetail.AvailabledAmount - updatedAccPettyCashImprestDetail.IssuedAmount;
                    }
                    else
                    {
                        //accPettyCashImprestDetail.UsedAmount = accPettyCashImprestDetail.UsedAmount + updatedAccPettyCashImprestDetail.IssuedAmount;
                        accPettyCashImprestDetail.IssuedAmount = accPettyCashImprestDetail.IssuedAmount + updatedAccPettyCashImprestDetail.IssuedAmount;
                        accPettyCashImprestDetail.AvailabledAmount = accPettyCashImprestDetail.AvailabledAmount - updatedAccPettyCashImprestDetail.IssuedAmount;
                    }
                }

                context.Entry(accPettyCashImprestDetail).State = EntityState.Modified;
                context.SaveChanges();
            }
            return true;
        }
        
        /// <summary>
        /// Get Saved AccPettyCashImprestDetail By PettyCashBookID 
        /// </summary>
        /// <param name="pettyCashBookID"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashImprestDetail GetAccPettyCashImprestByPettyCashBookID(long pettyCashBookID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashImprestDetails.Where(ph => ph.PettyCashLedgerID.Equals(pettyCashBookID) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }
    }
}
