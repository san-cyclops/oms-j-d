using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccPettyCashBillEntryValidator
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

        public bool ValidateExistsPausedBillRecordByIOUDocumentID(string documentNo, long documentID, int documentDocumentID, int locationID)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashBillsList = (from m in context.AccPettyCashBillHeaders
                                                 where m.ReferenceDocumentID == documentID && m.ReferenceDocumentDocumentID == documentDocumentID
                                                 && m.ReferenceLocationID == locationID && m.DocumentNo != documentNo && m.DocumentStatus == 0
                                                 select m).ToList();
                    if (!accPettyCashBillsList.Contains(null) && accPettyCashBillsList.Count == 0)
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

        public bool ValidateExistsValidReimburseRecord(long pettyCashLedgerID, int locationID)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashReimbursementsList = (from m in context.AccPettyCashReimbursements
                                                          where m.PettyCashLedgerID == pettyCashLedgerID && m.LocationID == locationID
                                                          && m.BalanceAmount > 0
                                                          select m).ToList();
                    if (!accPettyCashReimbursementsList.Contains(null) && accPettyCashReimbursementsList.Count == 0)
                    {
                        isValidated = false;
                    }
                    else
                    {
                        isValidated = true;
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

        public bool ValidateExistsValidReimburseAmount(long pettyCashLedgerID, int locationID, decimal totalExpenseAmount)
        {
            bool isValidated = false;
            decimal totalBalanceAmount = 0;
            try
            { //?????????????
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashReimbursementsList = (from m in context.AccPettyCashReimbursements
                                                          where m.PettyCashLedgerID == pettyCashLedgerID && m.LocationID == locationID
                                                          && m.BalanceAmount > 0
                                                          select m).ToList();
                    if (!accPettyCashReimbursementsList.Contains(null) && accPettyCashReimbursementsList.Count > 0)
                    {
                        totalBalanceAmount = accPettyCashReimbursementsList.Sum(r => r.BalanceAmount);
                        if (totalBalanceAmount < totalExpenseAmount)
                        {
                            isValidated = false;
                        }
                        else
                        {
                            isValidated = true;
                        }
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
