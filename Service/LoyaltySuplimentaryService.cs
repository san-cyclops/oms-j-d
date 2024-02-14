using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;

namespace Service
{
    public class LoyaltySuplimentaryService
    {
        ERPDbContext context = new ERPDbContext();

        public void AddLoyaltySuplimentarys(LoyaltySuplimentary loyaltySuplimentary)
        {
            context.LoyaltySuplimentaries.Add(loyaltySuplimentary);
            context.SaveChanges();
        }

        public void UpdateLoyaltySuplimentarys(LoyaltySuplimentary loyaltySuplimentary)
        {
            var lsID = context.LoyaltySuplimentaries.FirstOrDefault(x => x.LoyaltySuplimentaryID == loyaltySuplimentary.LoyaltySuplimentaryID);
            var entry = context.Entry(lsID);
            entry.CurrentValues.SetValues(lsID);
            this.context.SaveChanges();
        }

        public DataTable GetLoyaltySuplimentarys(long loyalityId)
        {

            DataTable dt = new DataTable();


            var query = from ls in context.LoyaltySuplimentaries
                        join lc in context.LoyaltyCustomers on ls.LoyaltyCustomerID equals lc.LoyaltyCustomerID
                        join cm in context.CardMasters on ls.CardTypeId equals cm.CardMasterID
                        join rt in context.ReferenceTypes on ls.RelationShipId equals rt.LookupKey
                        join rts in context.ReferenceTypes on ls.Status equals  rts.LookupKey
                        where ls.IsDelete == false && rt.LookupType.Equals("13") && rts.LookupType.Equals("14") 
                        orderby ls.LoyaltyCustomerID
                        select new { lc.CardNo, Relationship = rt.LookupValue, CardType = cm.CardMasterID, sname = ls.Name, Status=rts.LookupValue,LcId=ls.LoyaltySuplimentaryID }; 
            return dt = Common.LINQToDataTable(query);
             return dt;

        }

        public LoyaltySuplimentary GetLoyaltySuplimentaryById(long LgsCustomerId, long SuplimentaryId)
        {

            return context.LoyaltySuplimentaries.Where(c => c.LoyaltyCustomerID == LgsCustomerId && c.LoyaltySuplimentaryID == SuplimentaryId && c.IsDelete.Equals(false)).FirstOrDefault();
        }
    }
}
