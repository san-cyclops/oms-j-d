using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity.Validation;

namespace Service
{
    public class BrokerService
    {
        ERPDbContext context = new ERPDbContext();

        public List<Broker> GetAllBrokers()
        {
            return context.Brokers.Where(c => c.IsDelete == false).ToList();
        }


        public string[] GetAllBrokerCodes()
        {
            List<string> BrokerCodeList = context.Brokers.Where(c => c.IsDelete.Equals(false)).Select(c => c.BrokerCode).ToList();
            return BrokerCodeList.ToArray();
        }

        public string[] GetAllBrokerNames()
        {
            List<string> BrokerNameList = context.Brokers.Where(c => c.IsDelete.Equals(false)).Select(c => c.BrokerName).ToList();
            return BrokerNameList.ToArray();
        }

        public Broker GetBrokersByCode(string BrokerCode)
        {
            return context.Brokers.Where(b => b.BrokerCode.Equals(BrokerCode) && b.IsDelete.Equals(false)).FirstOrDefault();
        }

        public Broker GetBrokersByName(string BrokerName)
        {
            return context.Brokers.Where(b => b.BrokerName.Equals(BrokerName) && b.IsDelete.Equals(false)).FirstOrDefault();
        }

        public Broker GetBrokersByID(long BrokerID)
        {
            return context.Brokers.Where(b => b.BrokerID.Equals(BrokerID) && b.IsDelete.Equals(false)).FirstOrDefault();
        }
    }
}
