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
    public class AccPettyCashPaymentValidator
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

        public bool ValidateExistsValidReimburseAmount(long pettyCashLedgerID, int locationID, decimal totalExpenseAmount)
        {
            bool isValidated = false;
            decimal totalBalanceAmount = 0;
            try
            {
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

        public bool ValidateExistsPausedPaymentRecord(string documentNo, long pettyCashLedgerID, int locationID, List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    foreach (var accPettyCashVoucherHeader in accPettyCashVoucherHeaderList)
                    {
                        var accPettyCashPaymentsList = (from m in context.AccPettyCashPaymentDetails
                                                        where m.ReferenceDocumentID == accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID 
                                                        && m.ReferenceDocumentDocumentID == accPettyCashVoucherHeader.DocumentID 
                                                        && m.ReferenceLocationID == accPettyCashVoucherHeader.LocationID
                                                        && m.DocumentNo != documentNo && m.DocumentStatus == 0 
                                                        && m.LedgerID == pettyCashLedgerID && m.LocationID == locationID
                                                        select m).ToList();

                        if (!accPettyCashPaymentsList.Contains(null) && accPettyCashPaymentsList.Count == 0)
                        {
                            isValidated = true;
                            break;
                        }
                        else
                        {
                            isValidated = false;
                            break;
                        }
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
