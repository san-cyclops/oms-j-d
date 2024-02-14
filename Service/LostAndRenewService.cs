using Data;
using Domain;
using Utility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service
{
    public class LostAndRenewService
    {
        ERPDbContext context = new ERPDbContext();
        LostAndRenew lostAndRenew = new LostAndRenew();

        public void AddlostAndRenew(LostAndRenew lostAndRenew)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                context.LostAndRenews.Add(lostAndRenew);
                context.SaveChanges();
                transaction.Complete();
            }
        }


        public bool UpdateLoyaltyCustomerDetails(string oldCustomerCode, string newCustomerCode, string newCardNo, string remarks) 
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@oldEncode", Value=oldCustomerCode},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@newEncode", Value=newCustomerCode},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@userName", Value=Common.LoggedUser},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@newCardNo", Value=newCardNo},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@remarks", Value=remarks}
                    };
            if (CommonService.ExecuteStoredProcedure("spLostAndReNew", parameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckNewCodeExists(string newCode)
        {
            bool found = false;

            LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            loyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCode(newCode);

            if (loyaltyCustomer == null) { found = false; }
            else { found = true; }

            return found;
        }

        public bool CheckNewCardNoExists(string newCardNo)  
        {
            bool found = false;

            LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
            LoyaltyCustomerService loyaltyCustomerService = new LoyaltyCustomerService();

            loyaltyCustomer = loyaltyCustomerService.GetLoyaltyCustomerByCardNo(newCardNo);

            if (loyaltyCustomer == null) { found = false; }
            else { found = true; }

            return found;
        }


    }
}
