using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Service
{
    public class AccPettyCashPaymentUpdateValidator
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
    }
}
