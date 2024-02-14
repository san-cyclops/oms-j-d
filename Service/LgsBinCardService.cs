using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using Utility;
using EntityFramework.Extensions;
using System.Transactions;
using System.Data.Common;
using System.Data.Entity;
using MoreLinq;
using System.Collections;
using EntityFramework.Extensions;
using MoreLinq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Threading;
using System.Configuration;

namespace Service
{
    public class LgsBinCardService
    {
        SqlConnection mscn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
        ERPDbContext context = new ERPDbContext();

        private string sqlStatement;

        public string SqlStatement
        {
            get { return sqlStatement; }
            set { sqlStatement = value; }
        }

        private bool isExsists;

        public bool IsExsists
        {
            get { return isExsists; }
            set { isExsists = value; }
        }

        private DataSet dsGetBinCardDetails = new DataSet();

        public DataSet DsGetBinCardDetails
        {
            get { return dsGetBinCardDetails; }
            set { dsGetBinCardDetails = value; }
        }

        private DataSet dsGetDataTables = new DataSet();

        public DataSet DsGetDataTables
        {
            get { return dsGetDataTables; }
            set { dsGetDataTables = value; }
        }


        public bool View(int locationId, DateTime fromDate, DateTime toDate, string fromCode, string toCode)
        {

            {

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyId", Value=Common.LoggedCompanyID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SelectedLocationID", Value=locationId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromDate", Value=fromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToDate", Value=toDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromCode", Value=fromCode}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToCode", Value=toCode}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UserId",Value=Common.LoggedUserId}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CreatedUser", Value=Common.LoggedUser}
                    };


                if (CommonService.ExecuteStoredProcedure("spLogisticBinCard", parameter))
                {

                    return true;
                }
                else
                { return false; }

            }
        }

        public DataTable GetBinCardDetails(int locationId, string fromCode, string toCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.LgsTmpProductStockDetails
                            join lc in context.Locations on d.LocationID equals lc.LocationID
                            where d.UserID.Equals(Common.LoggedUserId) && d.LocationID.Equals(locationId) //&& d.ProductCode.CompareTo(fromCode) >= 0 && d.ProductCode.CompareTo(toCode) <= 0
                            select new
                            {
                                d.ProductID,
                                d.ProductCode,
                                d.ProductName,
                                d.TransactionDate,
                                d.TransactionNo,
                                d.TransactionType,
                                d.CostPrice,
                                d.SellingPrice,
                                d.StockQty,
                                d.LocationID,
                                d.BatchNo,
                                lc.LocationCode,
                                lc.LocationName,
                                d.ToLocationName,
                                d.Qty1,
                                d.Qty2,
                                d.Qty3,
                                d.Qty4,
                                d.Qty5,
                                d.GrossProfit,
                            };
                DataTable dt = query.ToDataTable();
                return query.ToDataTable();
                
            }

        }

    }
}
