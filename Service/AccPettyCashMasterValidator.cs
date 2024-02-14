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
    public class AccPettyCashMasterValidator
    {
        public bool ValidateExistsLedgerTransactions(AccPettyCashMaster accPettyCashMaster)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashMastersList = (from m in context.AccPettyCashMasters
                                                  join pd in context.AccPettyCashReimbursements on m.LedgerID equals pd.LedgerID
                                                  group pd by new { pd.LedgerID } into refGroup
                                                  select new { keyVal = refGroup.Key, details = refGroup }).ToList();
                    if (!accPettyCashMastersList.Contains(null) && accPettyCashMastersList.Count > 0)
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

        public bool ValidateExistsPettyCashLedgerLocation(AccPettyCashMaster accPettyCashMaster)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashMastersList = (from m in context.AccPettyCashMasters
                                                   where m.LedgerID == accPettyCashMaster.LedgerID && m.LocationID != accPettyCashMaster.LocationID
                                                   && m.IsDelete == false
                                                   select m).ToList();
                    if (!accPettyCashMastersList.Contains(null) && accPettyCashMastersList.Count > 0)
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

        public bool ValidateExistsPettyCashLedgerByLocationID(long pettyCashBookID, int locationID)
        {
            bool isValidated = false;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var accPettyCashMastersList = (from m in context.AccPettyCashMasters
                                                   where m.LedgerID == pettyCashBookID && m.LocationID != locationID && m.IsDelete == false
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
    }
}
