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
    public class PosSalesSummeryService
    {
        SqlConnection mscn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
         ERPDbContext context = new ERPDbContext();

        //public string GetSqlConnectionString()
        //{
        //    ERPDbContext context = new ERPDbContext();
        //    var entityConn = context.Database.Connection as System.Data.Entity.Core.EntityClient.EntityConnection;
        //    var dbConn = context.Database.Connection as SqlConnection;


            

        //    return dbConn.ToString();
        //}


 

       

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

        private DataSet dsGetCurrentsales = new DataSet();

        public DataSet DsGetCurrentsales
        {
            get { return dsGetCurrentsales; }
            set { dsGetCurrentsales = value; }
        }

        private DataSet dsGetDataTables = new DataSet();

        public DataSet DsGetDataTables
        {
            get { return dsGetDataTables; }
            set { dsGetDataTables = value; }
        }

        public bool View(int locationID, int terminalID, DateTime fromDate, DateTime toDate, bool locationWise)
        {
           // using (TransactionScope transaction = new TransactionScope())
            {

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=locationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UnitNo", Value=terminalID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LogedUserId", Value=Common.LoggedUserId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromDate", Value=fromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToDate", Value=toDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationWise", Value=locationWise}
                    };

                if (CommonService.ExecuteStoredProcedure("spPosSalesSummery", parameter))
                {
                    //transaction.Complete();
                    return true;
                }
                else
                { return false; }

            }
        }

        public DataTable GetSalesSum()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = from d in context.RSalesSummarys
                            //join lc in context.Locations on d.LocationID equals lc.LocationID
                            where d.LogedUser.Equals(Common.LoggedUserId) 
                            select new
                            {
                                d.LocationID,
                                d.UnitNo,
                                d.CashierID,
                                d.vouchersalegross,
                                d.nvouchersale,
                                d.voudiscount,
                                d.nvoudiscount,
                                d.vouRedeem,
                                d.nvouRedeem,
                                d.loyaltyRedeem,
                                d.nloyaltyRedeem,
                                d.vouchernet,
                                d.vouchercash,
                                d.vouchercredit,
                                d.grosssale,
                                d.refunds,
                                d.nrefunds,
                                d.Idiscount,
                                d.nidiscount,
                                d.sdiscount,
                                d.nsdiscount,
                                d.voids,
                                d.nvoids,
                                d.error,
                                d.nerror,
                                d.cancel,
                                d.ncancel,
                                d.loyalty,
                                d.nloyalty,
                                d.nett,
                                d.credpay,
                                d.ncredpay,
                                d.paidout,
                                d.npaidout,
                                d.cashsale,
                                d.staffcashsale,
                                d.nstaffcashsale,
                                d.cashrefund,
                                d.ncashrefund,
                                d.advancereceive,
                                d.nadvancereceive,
                                d.advancerefund,
                                d.nadvancerefund,
                                d.advancereceivecrd,
                                d.nadvancereceivecrd,
                                d.advancerefundcrd,
                                d.nadvancerefundcrd,
                                d.advancesettlement,
                                d.nadvancesettlement,
                                d.noofbills,
                                d.voucher,
                                d.nvoucher,
                                d.staffcred,
                                d.nstaffcred,
                                d.cheque,
                                d.ncheque,
                                d.starpoint,
                                d.starpointErn,
                                d.starpointErnVal,
                                d.nstarpoint,
                                d.mcredit,
                                d.nmcredit,
                                d.credit,
                                d.ncredit,
                                d.crdnote,
                                d.ncrdnote,
                                d.crdnotestle,
                                d.ncrdnotestle,
                                d.CashInHand,
                                d.DeclaredAmount,
                                d.ReceivedOnAccount,
                                d.ReportNo,
                                d.Vstaffcashsale,
                                d.VstaffCreditSale,
                                d.MasterCard,
                                d.VisaCard,
                                d.AmexCard,
                                d.DebitCard,
                                d.NMasterCard,
                                d.NVisaCard,
                                d.NAmexCard,
                                d.NDebitCard,
                                d.SMSVoucher,
                                d.MCash,
                                d.eZCash,
                                d.LogedUser,
                                d.LocationCode,
                                d.LocationName
                            };
                return query.ToDataTable();
            }

        }

        public void GetCurrentSales(int terminalId, bool IsUnit, int LocationId, out string OfflineTerminals)
        
        {
            OfflineTerminals = string.Empty;
            try
            {


                DataTable dtLocConnections = new DataTable();

                string strSql;

                if (IsUnit == true)

                    strSql = "SELECT * FROM InvPosTerminalDetails WHERE LocationID='" + LocationId + "' and TerminalId='" + terminalId + "'";
                else
                    strSql = "SELECT * FROM InvPosTerminalDetails WHERE LocationID='" + LocationId + "'";

                
                    SqlDataAdapter Da = new SqlDataAdapter(strSql, mscn);
                    DsGetDataTables.Clear();
                    dsGetCurrentsales.Clear();
                    Da.Fill(DsGetDataTables, "dtLocConnections");

                    if (DsGetDataTables.Tables["dtLocConnections"].Rows.Count > 0)
                    {
                        isExsists = true;
                    }
                    else
                    {
                        isExsists = false;
                    }


                    if (isExsists == true)
                    {
                        dtLocConnections = dsGetDataTables.Tables["dtLocConnections"];


                        if (dtLocConnections.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLocConnections.Rows.Count; i++)
                            {

                                Thread.Sleep(50);
                                if (this.CheckTargetServer1Status(dtLocConnections.Rows[i]["IP"].ToString().Trim()) == true)
                                {

                                    string strconnstr;
                                    strconnstr = "";
                                    strconnstr = "data source=" + dtLocConnections.Rows[i]["IP"].ToString().Trim() + ";initial catalog=" + dtLocConnections.Rows[i]["DBNAME"].ToString().Trim() + ";user id= sa; password= " + dtLocConnections.Rows[i]["PWD"].ToString().Trim() + ";packet size=4096;Min pool size=10";


                                    SqlDataAdapter sqlDa = new SqlDataAdapter(SqlStatement, strconnstr);
                                    sqlDa.Fill(dsGetCurrentsales, "inv_productsalesdetails");
                                    //OfflineTerminals = "";

                                }
                                else
                                {
                                    if (OfflineTerminals == "")
                                    {
                                        OfflineTerminals = "Terminal # " + dtLocConnections.Rows[i]["TerminalId"].ToString().Trim();
                                    }
                                    else
                                    {
                                        OfflineTerminals = OfflineTerminals + ", Terminal # " + dtLocConnections.Rows[i]["TerminalId"].ToString().Trim();
                                    }
                                }

                            }
                        }
                        
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void GetCurrentSalesCategory(string codeFrom, string codeTo)
        {
            
            try
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT [InvCategoryID],[CategoryCode],[CategoryName] FROM [InvCategory] WHERE InvCategoryId != 1 AND CategoryCode BETWEEN '" + codeFrom + "' AND '" + codeTo + "' ", CommonService.GetConnString());
                sqlDa.Fill(dsGetCurrentsales, "InvCategory");                                
                   
            }
            catch (Exception ex)
            {

            }
        }

        public void GetCurrentSalesSubCategory(string codeFrom, string codeTo)
        {

            try
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT [InvSubCategoryID],[SubCategoryCode],[SubCategoryName] FROM [InvSubCategory] WHERE InvSubCategoryId != 1 AND SubCategoryCode BETWEEN '" + codeFrom + "' AND '" + codeTo + "' ", CommonService.GetConnString());
                sqlDa.Fill(dsGetCurrentsales, "InvSubCategory");

            }
            catch (Exception ex)
            {

            }
        }

        public bool CheckTargetServer1Status(String LocIP)
        {
            try
            {

                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                // Create a buffer of 32 bytes of data to be transmitted.
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                //check slave server 1
                if (LocIP.Trim() != string.Empty)
                {
                    PingReply reply = pingSender.Send(LocIP, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
               
            }
        }

        //public DataTable GetStaffPurchaseCreditSalesDataTable()
        //{

        //      //1-pur  iseffsta adddis 1 - without dis 0
        //      //2-cre
        //      //3-adva
  
        //    using (ERPDbContext context = new ERPDbContext())
        //    {
        //        var query = from d in context.InvEmployeeTransactions
        //                    join e in context.Employees on d.EmployeeID equals e.EmployeeID
        //                    join l in context.Locations on d.LocationID equals l.LocationID
        //                    select new
        //                    {
        //                        l.LocationCode,
        //                        l.LocationName,
        //                        d.Date,
        //                        d.Receipt,
        //                        e.EmployeeCode,
        //                        e.EmployeeName,
        //                        d.Amount,
        //                        d.DiscAmt

                                 
        //                    };
        //        return query.ToDataTable();
        //    }

        //}

        public ArrayList GetSelectionDataStaffSalesCredit(Common.ReportDataStruct reportDataStruct,int TranId)
        {

            IQueryable qryResult = context.InvEmployeeTransactions.Where("TransID==@0", TranId);
            //context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1", false, true);   
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LocationId":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                        .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");;
                    break;
                case "EmployeeId":
                    qryResult = qryResult.Join(context.Employees, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");          
                        //OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "").Select("" + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                //case "LocationID":
                //      qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)").
                //                    OrderBy("LocationCode").Select("LocationCode");
                //     break;
                //case "InvLoyaltyTransactionID":
                //      qryResult = qryResult.Join(context.InvLoyaltyTransactions, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.InvLoyaltyTransactionID)").
                //                    OrderBy("InvLoyaltyTransactionID").Select("InvLoyaltyTransactionID");
                //      break;
                default:
                    //qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim())
                    qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")",
                                        "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                        .OrderBy("Key." + reportDataStruct.DbColumnName.Trim() + "")
                                        .Select("Key." + reportDataStruct.DbColumnName.Trim() + "");
                    break;
            }
            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResult)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else
            {
                foreach (var item in qryResult)
                {
                    if (!string.IsNullOrEmpty(item.ToString().Trim()))
                    { selectionDataList.Add(item.ToString().Trim()); }
                }
            }
            return selectionDataList;
        }
        public DataTable GetStaffPurchaseCreditSalesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo,int TranId)
        {
            var query = context.InvEmployeeTransactions.Where("TransID==@0", TranId);


            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }

            var queryResult = query.AsEnumerable();
            
            return (from d in queryResult
                        join e in context.Employees on d.EmployeeID equals e.EmployeeID
                        join l in context.Locations on d.LocationID equals l.LocationID
                        where d.Amount>0
                        select new
                        {
                           FieldString1 = l.LocationCode,
                           FieldString2= l.LocationName,
                           FieldString3= d.Date,
                           FieldString4= d.Receipt,
                           FieldString5= e.EmployeeCode,
                           FieldString6= e.EmployeeName,
                           FieldString = d.UnitNo,
                           FieldDecimal1= d.Amount,
                           FieldDecimal2= d.DiscAmt
 
                        }).ToArray().ToDataTable(); 
      
                      
        }

    }
}
