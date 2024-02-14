using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;

namespace Service
{
    public class AccChequeDetailService
    {
        public void AddAccChequeDetail(AccChequeDetail accChequeDetail)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.AccChequeDetails.Add(accChequeDetail);
                context.SaveChanges();
            }
        }

        public void UpdateAccChequeDetail(AccChequeDetail accChequeDetail)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                accChequeDetail.ModifiedUser = Common.LoggedUser;
                accChequeDetail.ModifiedDate = Common.GetSystemDateWithTime();
                context.Entry(accChequeDetail).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
