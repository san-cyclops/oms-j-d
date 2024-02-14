using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class AccPettyCashReimbursementValidator
    {
        public bool ValidateExistsPettyCashLedgerByLocationID(long pettyCashBookID, int locationID)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashMastersList = (from m in context.AccPettyCashMasters
                                                   where m.LedgerID == pettyCashBookID && m.LocationID == locationID && m.IsDelete == false
                                                   select m).ToList();
                    if (!accPettyCashMastersList.Contains(null) && accPettyCashMastersList.Count > 0)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return isValidated;
        }

        public bool ValidateExistsPettyCashLedgerByLocationID(long pettyCashBookID, int locationID, decimal reimburseAmount)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashMastersList = (from m in context.AccPettyCashMasters
                                                   where m.LedgerID == pettyCashBookID && m.LocationID == locationID && m.IsDelete == false
                                                   && m.Amount >= reimburseAmount
                                                   select m).ToList();
                    if (!accPettyCashMastersList.Contains(null) && accPettyCashMastersList.Count > 0)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return isValidated;
        }

        public bool ValidatePettyCashReimburseAmountByLedgerID(long pettyCashBookID, int locationID, decimal reimburseAmount)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    AccPettyCashMaster accPettyCashMaster = (from m in context.AccPettyCashMasters
                                                   where m.LedgerID == pettyCashBookID && m.LocationID == locationID && m.IsDelete == false
                                                   && m.Amount >= 0
                                                   select m).FirstOrDefault();

                    var balanceReimburseTotal = from m in context.AccPettyCashReimbursements
                                                where m.PettyCashLedgerID == pettyCashBookID && m.LocationID == locationID && m.DocumentStatus == 1
                                                && m.BalanceAmount >= 0
                                                group m by new { m.PettyCashLedgerID, m.LocationID } into g
                                                select new { TotalBalanceReimburse = g.Sum(p => p.BalanceAmount) }.TotalBalanceReimburse;

                    decimal reimburseLimitAmount = accPettyCashMaster.Amount;
                    decimal balanceReimburseAmount = balanceReimburseTotal.FirstOrDefault();

                    if ((reimburseLimitAmount> 0) && (reimburseLimitAmount-balanceReimburseAmount>=reimburseAmount))
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return isValidated;
        }

        public bool ValidateExistsPausedReimburseRecord(string documentNo, long pettyCashLedgerID, int locationID)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashReimbursementsList = (from m in context.AccPettyCashReimbursements
                                                          where m.PettyCashLedgerID == pettyCashLedgerID && m.LocationID == locationID
                                                          && m.BalanceAmount > 0 && m.DocumentNo != documentNo && m.DocumentStatus == 0 
                                                          select m).ToList();
                    if (!accPettyCashReimbursementsList.Contains(null) && accPettyCashReimbursementsList.Count == 0)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return isValidated;
        }
    }
}
