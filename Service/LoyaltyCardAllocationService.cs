using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;

namespace Service
{
    public class LoyaltyCardAllocationService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods


        public void AddLoyaltyCardAllocation(LoyaltyCardAllocation loyaltyCardAllocation)
        {
            context.LoyaltyCardAllocations.Add(loyaltyCardAllocation);
            context.SaveChanges();

            LoyaltyCardAllocationLog loyaltyCardAllocationLog = new LoyaltyCardAllocationLog();
            loyaltyCardAllocationLog.CardNo = loyaltyCardAllocation.CardNo;
            loyaltyCardAllocationLog.CardTypeId = loyaltyCardAllocation.CardTypeId;
            loyaltyCardAllocationLog.CustomerId = loyaltyCardAllocation.CustomerId;
            loyaltyCardAllocationLog.EncodeNo = loyaltyCardAllocation.EncodeNo;
            loyaltyCardAllocationLog.LoyaltyCardAllocationID = loyaltyCardAllocation.LoyaltyCardAllocationID;
            loyaltyCardAllocationLog.SerialNo = loyaltyCardAllocation.SerialNo;
            loyaltyCardAllocationLog.IsDelete = loyaltyCardAllocation.IsDelete;

            context.CardAllocationLogs.Add(loyaltyCardAllocationLog);
            context.SaveChanges();

            //LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();
            //LoyaltyCustomerervice loyaltyCustomerervice = new LoyaltyCustomerervice();
            //loyaltyCustomer =  loyaltyCustomerervice.GetLgsCustomerById(loyaltyCardAllocation.CustomerId);
            //if(loyaltyCustomer != null)
            //    loyaltyCustomer.CardNo = global::

        }

        public void UpdateLoyaltyCardAllocation(LoyaltyCardAllocation loyaltyCardAllocations)
        {
            context.Entry(loyaltyCardAllocations).State = EntityState.Modified;
            context.SaveChanges();
            LoyaltyCardAllocationLog loyaltyCardAllocationLog = new LoyaltyCardAllocationLog();
            loyaltyCardAllocationLog.CardNo = loyaltyCardAllocations.CardNo;
            loyaltyCardAllocationLog.CardTypeId = loyaltyCardAllocations.CardTypeId;
            loyaltyCardAllocationLog.CustomerId = loyaltyCardAllocations.CustomerId;
            loyaltyCardAllocationLog.EncodeNo = loyaltyCardAllocations.EncodeNo;
            loyaltyCardAllocationLog.LoyaltyCardAllocationID = loyaltyCardAllocations.LoyaltyCardAllocationID;
            loyaltyCardAllocationLog.SerialNo = loyaltyCardAllocations.SerialNo;

            context.CardAllocationLogs.Add(loyaltyCardAllocationLog);
            context.SaveChanges();

        }

        public LoyaltyCardAllocation GetLoyaltyCardAllocationBySerial(string  SerialNo)
        {
            return context.LoyaltyCardAllocations.Where(l => l.SerialNo == SerialNo && l.IsDelete == false).FirstOrDefault();
        }

        public List<LoyaltyCardAllocation> GetAllLoyaltyCardAllocations()
        {
            return context.LoyaltyCardAllocations.Where(u => u.IsDelete == false).ToList();
        }

        #endregion
    }
}
