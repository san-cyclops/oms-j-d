using Data;
using Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace Service
{
    public class PaymentDatService
    {
        ERPDbContext context = new ERPDbContext();

        public DataTable GetAllPaymentDetDataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.PaymentDets
                        where d.Datatransfer.Equals(1)
                        select new
                            {
                                d.PaymentDetID,
                                d.RowNo,
                                d.PayTypeID,
                                d.Amount,
                                d.Balance,
                                d.SDate,
                                d.Receipt,
                                d.LocationID,
                                d.CashierID,
                                d.UnitNo,
                                d.BillTypeID,
                                d.SaleTypeID,
                                d.RefNo,
                                d.BankId,
                                d.ChequeDate,
                                d.IsRecallAdv,
                                d.RecallNo,
                                d.Descrip,
                                d.EnCodeName,
                                d.UpdatedBy,
                                d.Status,
                                d.ZNo,
                                d.CustomerId,
                                d.CustomerType,
                                d.CustomerCode,
                                d.GroupOfCompanyID,
                                d.Datatransfer,
                                d.ZDate

                            };
            return dt = Common.LINQToDataTable(query);
            //context.Configuration.LazyLoadingEnabled = false;
           // return context.PaymentDets.SqlQuery("select * from PaymentDet").ToDataTable();


        }
        
        public void UpdatePaymentDat(int Edatatransfer, int datatransfer)
        {
            context.PaymentDets.Update(x => x.Datatransfer.Equals(Edatatransfer), x => new PaymentDet { Datatransfer = datatransfer });
        }

        public void UpdatePaymentDatSelect()
        {
            var qry = (from d in context.PaymentDets where d.Datatransfer.Equals(0) select d).Take(100).ToArray();

            foreach (var temp in qry)
            {
                context.PaymentDets.Update(d => d.PaymentDetID.Equals(temp.PaymentDetID), d => new PaymentDet { Datatransfer = 1 });
            }
        }

    }
}
