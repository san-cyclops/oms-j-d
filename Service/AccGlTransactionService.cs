using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;
using Utility;

namespace Service
{
    //By Pravin
    public class AccGlTransactionService
    {
        public AccGlTransactionHeader getAccGlTransactionHeader(long referenceDocumentID, int referenceDocumentDocumentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext()) 
            {return context.AccGlTransactionHeaders.Where(ph => ph.ReferenceDocumentDocumentID.Equals(referenceDocumentDocumentID) && ph.ReferenceDocumentID.Equals(referenceDocumentID) && ph.LocationID.Equals(locationID)).FirstOrDefault();}
        }
        
        public void AddAccGlTransactionHeader(AccGlTransactionHeader accGlTransactionHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.AccGlTransactionHeaders.Add(accGlTransactionHeader);
                context.SaveChanges();
            }
        }

        public void UpdateAccGlTransactionHeader(AccGlTransactionHeader accGlTransactionHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                accGlTransactionHeader.ModifiedUser = Common.LoggedUser;
                accGlTransactionHeader.ModifiedDate = Common.GetSystemDateWithTime(); 
                context.Entry(accGlTransactionHeader).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public decimal GetAccLedgerAccountBalanceByAccountID(long accountID, DateTime toDate, bool uploadStatus)
        {
            decimal balanceTotal = 0;
            try
            {
                //note : change code when apply uploadStatus
                using (ERPDbContext context = new ERPDbContext())
                {
                    AccLedgerAccount accLedgerAccount = context.AccLedgerAccounts.Where(u => u.AccLedgerAccountID == accountID && u.IsDelete == false).FirstOrDefault();

                    if (accLedgerAccount != null)
                    {
                        int standardDrCr = accLedgerAccount.StandardDrCr;
                        var balanceDebitTotal = from p in context.AccGlTransactionDetails
                                                where (p.LedgerID == accountID && p.DrCr == 1 && p.DocumentStatus == 1 && p.IsUpLoad == uploadStatus && p.DocumentDate <= toDate)
                                                group p by p.LedgerID into g
                                                select new { TotalDebit = g.Sum(p => p.Amount) }.TotalDebit;

                        var balanceCreditTotal = from p in context.AccGlTransactionDetails
                                                 where (p.LedgerID == accountID && p.DrCr == 2 && p.DocumentStatus == 1 && p.IsUpLoad == uploadStatus && p.DocumentDate <= toDate)
                                                 group p by p.LedgerID into g
                                                 select new { TotalDebit = g.Sum(p => p.Amount) }.TotalDebit;

                        if (standardDrCr.Equals(1))
                        {
                            balanceTotal = balanceDebitTotal.FirstOrDefault() - balanceCreditTotal.FirstOrDefault();
                        }
                        else if (standardDrCr.Equals(2))
                        {
                            balanceTotal = balanceCreditTotal.FirstOrDefault() - balanceDebitTotal.FirstOrDefault();
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
            return balanceTotal;
        }

        public bool TallyTransactionEntry(List<AccGlTransactionDetail> accGlTransactionDetails)
        {
            bool isTally = true;

            var invalidLedgerCounts = from p in accGlTransactionDetails
                               where (p.LedgerID == null)
                               group p by new{p.DocumentNo, p.DocumentID, p.DrCr } into g
                               select new { Ledger = g.Key, LedgerCount = g.Count() }; 
            if (invalidLedgerCounts.Any())
            {
                isTally = false;
            }

            var debitTotal = from p in accGlTransactionDetails
                             where (p.DrCr == 1)
                             group p by p.LedgerID into g
                             select new { TotalDebit = g.Sum(p => p.Amount) }.TotalDebit; //new { debit = g.Key, TotalDebit = g.Sum(p => p.Amount) };

            var creditTotal = (from p in accGlTransactionDetails
                             where (p.DrCr == 2)
                             group p by p.LedgerID into g
                              select new { TotalCredit = g.Sum(p => p.Amount) }.TotalCredit); //new { credit = g.Key, TotalCredit = g.Sum(p => p.Amount) };

            decimal debitAmount = debitTotal.FirstOrDefault();
            decimal creditAmount = creditTotal.FirstOrDefault();
            if (debitAmount!=creditAmount)
            {
                isTally = false;
            }

            return isTally;
        }
    }
}
