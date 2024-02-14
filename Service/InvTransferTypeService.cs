using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

using Domain;
using Data;
using System.Data;
using Utility;

namespace Service
{
    /// <summary>
    /// TransferType Service
    /// Design By - C.S.Malluwawadu
    /// Developed By - C.S.Malluwawadu
    /// Date - 126/08/2013
    /// </summary>
    /// 

    public class InvTransferTypeService
    {
        ERPDbContext context = new ERPDbContext();

        public InvTransferType GetTransferTypesByName(string InvTransferType)
        {
            return context.InvTransferType.Where(d => d.TransferType == InvTransferType && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public InvTransferType GetTransferTypesByID(long InvTransferTypeID)
        {
            return context.InvTransferType.Where(d => d.InvTransferTypeID == InvTransferTypeID && d.IsDelete.Equals(false)).FirstOrDefault(); 
        }

        public List<InvTransferType> GetAllTransferTypes()
        {
            return context.InvTransferType.Where(l => l.IsDelete.Equals(false)).OrderBy(l => l.InvTransferTypeID).ToList();
        }

        public List<TransactionDet> GetPOSDocuments(int locationID, int transStatus)
        {
            
            List<TransactionDet> rtnList=new List<TransactionDet>();
            //var qry = context.TransactionDets.Where(ph => ph.LocationID.Equals(locationID) && ph.TransStatus.Equals(transStatus) && ph.Status.Equals(1) && ph.DataTransfer.Equals(1)).Select(ph => ph.Receipt).ToArray();
            var qry = (from td in context.TransactionDets
                       where td.LocationID == locationID && td.TransStatus == transStatus &&
                       td.Status == transStatus && td.BalanceQty > 0
                       select new
                       {
                           td.Receipt
                       }).Distinct().OrderBy(dt => dt.Receipt).ToArray();
            foreach (var temp in qry)
            {
                TransactionDet transactionDetAdd = new TransactionDet();
                transactionDetAdd.Receipt = temp.Receipt;

                rtnList.Add(transactionDetAdd);
            }

            return rtnList;
        }
    }
}
