using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using Utility;
using MoreLinq;
using EntityFramework.Extensions;

namespace Service
{
    public class TransactionDetService
    {
        ERPDbContext context = new ERPDbContext();

        public DataTable GetAllTransactionDetDataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.TransactionDets
                        where d.DataTransfer.Equals(1)
                        select new
                            {
                                d.TransactionDetID,
                                d.ProductID,
                                d.ProductCode,
                                d.RefCode,
                                d.BarCodeFull,
                                d.Descrip,
                                d.BatchNo,
                                d.SerialNo,
                                d.ExpiryDate,
                                d.Cost,
                                d.AvgCost,
                                d.Price,
                                d.Qty,
                                d.Amount,
                                d.UnitOfMeasureID,
                                d.UnitOfMeasureName,
                                d.ConvertFactor,
                                d.IDI1,
                                d.IDis1,
                                d.IDiscount1,
                                d.IDI1CashierID,
                                d.IDI2,
                                d.IDis2,
                                d.IDiscount2,
                                d.IDI2CashierID,
                                d.IDI3,
                                d.IDis3,
                                d.IDiscount3,
                                d.IDI3CashierID,
                                d.IDI4,
                                d.IDis4,
                                d.IDiscount4,
                                d.IDI4CashierID,
                                d.IDI5,
                                d.IDis5,
                                d.IDiscount5,
                                d.IDI5CashierID,
                                d.Rate,
                                d.IsSDis,
                                d.SDNo,
                                d.SDID,
                                d.SDIs,
                                d.SDiscount,
                                d.DDisCashierID,
                                d.Nett,
                                d.LocationID,
                                d.DocumentID,
                                d.BillTypeID,
                                d.SaleTypeID,
                                d.Receipt,
                                d.SalesmanID,
                                d.Salesman,
                                d.CustomerID,
                                d.Customer,
                                d.CashierID,
                                d.Cashier,
                                d.StartTime,
                                d.EndTime,
                                d.RecDate,
                                d.BaseUnitID,
                                d.UnitNo,
                                d.RowNo,
                                d.IsRecall,
                                d.RecallNO,
                                d.RecallAdv,
                                d.TaxAmount,
                                d.IsTax,
                                d.TaxPercentage,
                                d.IsStock,
                                d.UpdateBy,
                                d.Status,
                                d.ZNo,
                                d.GroupOfCompanyID,
                                d.DataTransfer,
                                d.CustomerType,
                                d.TransStatus,
                                d.ZDate

                            };
            return dt = Common.LINQToDataTable(query);
           // context.Configuration.LazyLoadingEnabled = false;
           // return context.TransactionDets.SqlQuery("select * from TransactionDet").ToDataTable();
        }

        public void UpdateTransactionDat(int Edatatransfer, int datatransfer)
        {
            context.TransactionDets.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new TransactionDet { DataTransfer = datatransfer });
        }

        public void UpdateTransactionDatSelect()
        {
            var qry = (from d in context.TransactionDets where d.DataTransfer.Equals(0) select d).Take(100).ToArray();

            foreach (var temp in qry)
            {
                context.TransactionDets.Update(d => d.TransactionDetID.Equals(temp.TransactionDetID), d => new TransactionDet { DataTransfer = 1 });
            }
        }



    }
}
