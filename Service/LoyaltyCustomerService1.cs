using System;
using System.Collections.Generic;
//using System.Data.Objects.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using System.Reflection;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Configuration;

namespace Service
{
    public class LoyaltyCustomerService
    {
        SqlConnection mscn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString);
        ERPDbContext context = new ERPDbContext();
        LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();

        private string dataSetName;

        private string sqlStatement;
        public string SqlStatement
        {
            get { return sqlStatement; }
            set { sqlStatement = value; }
        }

        private DataSet dsReport = new DataSet();
        public DataSet DsReport
        {
            get { return dsReport; }
        }

        public void AddLoyaltyCustomer(LoyaltyCustomer loyaltyCustomer)
        {
            try
            {
                //context.LoyaltyCustomers.Add(loyaltyCustomer);
                //context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void UpdateLoyaltyCustomer(LoyaltyCustomer loyaltyCustomer)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    loyaltyCustomer.ModifiedUser = Common.LoggedUser;
                    loyaltyCustomer.ModifiedDate = Common.GetSystemDateWithTime();
                    this.context.Entry(loyaltyCustomer).State = EntityState.Modified;
                    this.context.SaveChanges();
                    transaction.Complete();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public LoyaltyCustomer GetLoyaltyCustomerByCode(string loyaltyCustomerCode)
        {
            return context.LoyaltyCustomers.Where(c => c.CustomerCode == loyaltyCustomerCode && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LoyaltyCustomer GetLoyaltyCustomerByCustomerCodeOrCardNo(string customerCode, string cardNo)
        {
            ///return context.LoyaltyCustomers.Where(c => (c.CustomerCode == customerCode || c.CardNo == cardNo) && c.IsDelete.Equals(false)).FirstOrDefault();
            return context.LoyaltyCustomers.Where(c => (c.CustomerCode == customerCode) && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public LoyaltyCustomer GetLoyaltyCustomerByNicNo(string NicNo)
        {

            return context.LoyaltyCustomers.Where(c => c.NicNo == NicNo && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LoyaltyCustomer GetLoyaltyCustomerById(long loyaltyCustomerId)
        {

            return context.LoyaltyCustomers.Where(c => c.LoyaltyCustomerID == loyaltyCustomerId && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LoyaltyCustomer GetLoyaltyCustomerByName(string loyaltyCustomerName)
        {
            return context.LoyaltyCustomers.Where(c => c.CustomerCode == loyaltyCustomerName && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LoyaltyCustomer GetLoyaltyCustomerByCardNo(string CardNo)
        {
            return context.LoyaltyCustomers.Where(c => c.CardNo == CardNo && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<LoyaltyCustomer> GetAllLoyaltyCustomer()
        {
            return context.LoyaltyCustomers.Where(c => c.IsDelete == false).ToList();
        }

        public string[] GetAllLoyaltyCustomerCodes()
        {
            //return context.LoyaltyCustomers.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerCode).ToList().ToArray();
            var qry = (from lc in context.LoyaltyCustomers
                       where lc.IsDelete == false
                       select new
                       {
                           lc.LoyaltyCustomerID,
                           lc.CustomerCode
                       }).ToArray().OrderBy(lc => lc.LoyaltyCustomerID);
            List<String> rtnList = qry.Select(q => q.CustomerCode).ToList();
            return rtnList.ToArray();
        }

        public string[] GetAllNicNos()
        {
            return context.LoyaltyCustomers.Where(c => c.IsDelete.Equals(false)).Select(c => c.NicNo).ToList().ToArray();
            
        }

        public string[] GetAllCustomersCardNos()
        {
            return context.LoyaltyCustomers.Where(c => c.IsDelete.Equals(false)).Select(c => c.CardNo).ToList().ToArray();            
        }

        public string[] GetAllLoyaltyCustomerNames()
        {
            //return context.LoyaltyCustomers.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerName).ToList().ToArray();
            var qry = (from lc in context.LoyaltyCustomers
                       where lc.IsDelete == false
                       select new
                       {
                           lc.LoyaltyCustomerID,
                           lc.CustomerName
                       }).ToArray().OrderBy(lc => lc.LoyaltyCustomerID);
            List<String> rtnList = qry.Select(q => q.CustomerName).ToList();
            return rtnList.ToArray();
        }

        public InvLoyaltyTransaction GetLoyaltyTransactionByDate(DateTime DocDate)
        {

            return context.InvLoyaltyTransactions.Where(c => c.DocumentDate == DocDate).FirstOrDefault();
        }

        public DataTable GetBranchWiseCustomerDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1", false, true);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LocationID":
                                query = (from qr in query
                                         join jt in context.Locations.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.LocationID equals jt.LocationID
                                         select qr
                                        );

                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                }
            }
            var queryResult = (from ph in query.AsNoTracking()
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               where ph.Active == true
                               select new
                               {
                                   FieldString1 = ph.CustomerCode,
                                   FieldString2 = ph.CardNo,
                                   FieldString3 = ph.CustomerName,
                                   FieldString4 = l.LocationCode,
                                   FieldString5 = l.LocationName,
                                   FieldString6 = ph.Gender,
                                   FieldString7 = ph.Age,
                                   FieldString8 = ph.NicNo,
                                   FieldString9 = ph.Telephone,
                                   FieldString10 = ph.Mobile,
                                   FieldString11 = ph.WorkTelephone,
                                   FieldString12 = ph.CPoints,
                                   FieldString13 = ph.CustomerSince,
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetCustomerHistoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            //StringBuilder selectFields = new StringBuilder();
            //var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1", false, true);

            //// Insert Conditions to query
            //foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            //{
            //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
            //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

            //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

            //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
            //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

            //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
            //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

            //    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
            //    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            //}



            //var queryResult = (from ph in query
            //                   join l in context.Locations on ph.LocationID equals l.LocationID
            //                   join ci in context.LoyaltyCardIssueDetails on ph.CardNo equals ci.SerialNo
            //                   join ch in context.LoyaltyCardIssueHeaders on ci.LoyaltyCardIssueHeaderID equals ch.LoyaltyCardIssueHeaderID
            //                   join e in context.Employees on ch.EmployeeId equals e.EmployeeID
            //                   join c in context.CardMasters on ch.CardTypeId equals c.CardMasterID
            //                   where ph.Active == true
            //                   select new
            //                   {
            //                       FieldString1 = ph.CreatedDate,
            //                       FieldString2 = ph.CustomerCode,
            //                       FieldString3 = ph.CardNo,
            //                       FieldString4 = ph.CustomerName,
            //                       FieldString5 = ph.NicNo,
            //                       FieldString6 = ph.CardIssued,
            //                       FieldString7 = ch.Remark,
            //                       FieldString8 = l.LocationName,
            //                       FieldString9 = ch.IssueDate,
            //                       FieldString10 = e.EmployeeName,
            //                       FieldString11 = e.EmployeeTitle,
            //                       FieldString12 = c.CardName,
          
            //                   }).ToArray();




            //#region sanjeewa's code

            //#endregion

            //return queryResult.ToDataTable();

            return null;
        }

        public ArrayList GetSelectionDataLT(Common.ReportDataStruct reportDataStruct)
        {

            IQueryable qryResult = context.InvLoyaltyTransactions.AsQueryable(); 
                //context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1", false, true);             
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "CustomerID":
                    qryResult = qryResult.Join(context.LoyaltyCustomers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerCode)").
                                  OrderBy("CustomerCode").Select("CustomerCode");
                      break;
                case "NicNo":
                      qryResult = qryResult.Join(context.LoyaltyCustomers, "CustomerID", "CustomerID", "new(inner.NicNo)").
                                    OrderBy("NicNo").Select("NicNo");
                      break;
                case "LocationID":
                      qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)").
                                    OrderBy("LocationCode").Select("LocationCode");
                      break;

               
                //case "InvLoyaltyTransactionID":
                //      qryResult = qryResult.Join(context.InvLoyaltyTransactions, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.InvLoyaltyTransactionID)").
                //                    OrderBy("InvLoyaltyTransactionID").Select("InvLoyaltyTransactionID");
                      break;
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
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
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString()); }
                }
            }
            return selectionDataList;
           
        }

        public ArrayList GetSelectionDataLC(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResulData = context.LoyaltyCustomers.AsNoTracking()
                            .Where("IsDelete = @0 ", false) //AND Active =@1 , true
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "") //.Select("new(DocumentNo)")
                            ; //.Select(reportDataStruct.DbColumnName.Trim())

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LookupKey":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0 AND IsDelete == @1", "false"), "SupplierTitle", reportDataStruct.DbColumnName.Trim(), "new(outer.SupplierName, inner.LookupValue)")
                                    .OrderBy("LookupValue").Select("new( LookupValue, SupplierName )"); 
                    break;
                case "Gender":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0", "1"), "Gender", "LookupKey", "new(inner.LookupValue)")
                                .GroupBy("new(LookupValue)", "new(LookupValue)")
                                .OrderBy("Key.LookupValue").Select("(Key.LookupValue)");
                    break;
                case "CivilStatus":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0", "10"), "CivilStatus", "LookupKey", "new(inner.LookupValue)")
                                .GroupBy("new(LookupValue)", "new(LookupValue)")
                                .OrderBy("Key.LookupValue").Select("(Key.LookupValue)");
                    break;
                case "CardMasterID":
                    qryResulData = qryResulData.Join(context.CardMasters, "CardMasterID", reportDataStruct.DbColumnName.Trim(), "new(inner.CardName)")
                                .GroupBy("new(CardName)", "new(CardName)")
                                .OrderBy("Key.CardName").Select("(Key.CardName)");
                    break;
                case "Nationality":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0", "6"), "Nationality", "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("Key.LookupValue").Select("(Key.LookupValue)");
                    break;
                case "Religion":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0", "7"), "Religion", "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("Key.LookupValue").Select("(Key.LookupValue)");
                    break;
                case "Race":
                    qryResulData = qryResulData.Join(context.ReferenceTypes.Where("LookupType == @0", "29"), "Race", "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("Key.LookupValue").Select("(Key.LookupValue)");
                    break;
                case "LoyaltyCustomerID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "CustomerCode")
                    {
                        qryResulData = qryResulData.Join(context.LoyaltyCustomers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerCode)")
                            .GroupBy("new(CustomerCode)", "new(CustomerCode)")
                            .OrderBy("Key.CustomerCode").Select("Key.CustomerCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "CustomerName")
                    {
                        qryResulData = qryResulData.Join(context.LoyaltyCustomers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerName)")
                            .GroupBy("new(CustomerName)", "new(CustomerName)")
                            .OrderBy("Key.CustomerName").Select("Key.CustomerName");
                    }
                    break;
                case "LocationID":
                    qryResulData = qryResulData.Join(context.Locations,     reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                                    .GroupBy("new(LocationName)", "new(LocationName)")
                                    .OrderBy("Key.LocationName").Select("Key.LocationName");
                    break;

                case "NameOnCard":
                    qryResulData = qryResulData
                                    .Where(reportDataStruct.DbColumnName.Trim() + " != null ")
                                    .OrderBy("" + reportDataStruct.DbColumnName.Trim())
                                    .Select(reportDataStruct.DbColumnName.Trim())
                                    ;
                    
                    break;
                case "IsSuspended":
                case "IsBlackListed":
                case "IsCreditAllowed":
                case "SendUpdatesViaEmail":
                case "SendUpdatesViaSms":
                case "CardIssued":
                case "Active":
                    qryResulData = qryResulData.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")", "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                               .OrderBy("Key." + reportDataStruct.DbColumnName.Trim())
                                               .Select("Key." + reportDataStruct.DbColumnName.Trim() + "");
                    break;
                default:
                    qryResulData = qryResulData
                                    //.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")", "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                    .Where(reportDataStruct.DbColumnName.Trim() + " != null  "  )
                                    .OrderBy("" + reportDataStruct.DbColumnName.Trim())
                                    .Select("" + reportDataStruct.DbColumnName.Trim());
                    break;
            }

            //var x = qryResulData.AsQueryable().ToListAsync();

            qryResulData = qryResulData.AsQueryable();

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResulData)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else
            {
                foreach (var item in qryResulData)  
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
                    catch (Exception)
                    {

                        //selectionDataList.Add(string.Empty);
                    }
                    
                }
            }
            return selectionDataList;
        }

        public ArrayList GetSelectionDataCardMaster(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResulData = context.CardMasters
                            .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + ""); //.Select("new(DocumentNo)")
                            //.Select(reportDataStruct.DbColumnName.Trim());
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LoyaltyCustomerID":
                    qryResulData = qryResulData.Join(context.LoyaltyCustomers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerCode)").
                                  OrderBy("CustomerCode").Select("CustomerCode");
                    break;
                case "NicNo":
                    qryResulData = qryResulData.Join(context.LoyaltyCustomers, "CustomerID", "CustomerID", "new(inner.NicNo)").
                                  OrderBy("NicNo").Select("NicNo");
                    break;
                case "LocationID":
                    qryResulData = qryResulData.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationID)").
                                  OrderBy("LocationID").Select("LocationCode");
                    break;
                    //case "InvLoyaltyTransactionID":
                    //      qryResult = qryResult.Join(context.InvLoyaltyTransactions, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.InvLoyaltyTransactionID)").
                    //                    OrderBy("InvLoyaltyTransactionID").Select("InvLoyaltyTransactionID");
                    break;
                default:
                    // qryResulData = qryResulData.Select("" + reportDataStruct.DbColumnName.Trim() + "");
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResulData)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else
            {
                foreach (var item in qryResulData)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString()); }
                }
            }
            return selectionDataList;
        }

        public DataTable GetInActiveCustomerDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1", false, false);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from ph in query
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               select new
                               {
                                   FieldString1 = ph.CustomerCode,
                                   FieldString2 = ph.CustomerName,
                                   FieldString3 = ph.CardNo,
                                   FieldString4 = ph.CreatedDate,
                                   FieldString5 = ph.NicNo,
                                   FieldString6 = ph.Address1,
                                   FieldString7 = ph.Mobile,
                                   FieldString8 = l.LocationName
                               }).ToArray();

            return queryResult.ToDataTable();
        }

        public DataTable GetLoyaltyCustomerDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            //var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1",false, true);
            var query = context.LoyaltyCustomers.Where("IsDelete = @0", false);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "CardMasterID":
                                query = (from qr in query
                                         join jt in context.CardMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CardMasterID equals jt.CardMasterID
                                         select qr
                                        );
                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {
                        query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }                    
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LocationID":
                                query = (from qr in query
                                         join jt in context.Locations.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.LocationID equals jt.LocationID
                                         select qr
                                        );

                                break;
                            case "Gender":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Gender equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "CivilStatus":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CivilStatus equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "Nationality":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Nationality equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "Religion":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Religion equals jt.LookupKey
                                         select qr
                                        );
                                break;
                            case "Race":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Race equals jt.LookupKey
                                         select qr
                                        );

                                break;                            
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                }
            }


            //var queryResult = (from ph in query
            //                   join l in context.Locations on ph.LocationID equals l.LocationID
            //                   where ph.Active == true
            //                   select new
            //                   {
            //                       FieldString1 = ph.CustomerCode,
            //                       FieldString2 = ph.CustomerName,
            //                       FieldString3 = ph.CardNo,
            //                       FieldString4 = ph.Address1 + ph.Address2 + ph.Address3,
            //                       FieldString5 = ph.NicNo,
            //                       FieldString6 = ph.Mobile,
            //                       FieldString7 = l.LocationName

            //                   }).ToArray();


            string titleType = ((int)LookUpReference.TitleType).ToString();
            string genderType = ((int)LookUpReference.GenderType).ToString();
            string civilStatus = ((int)LookUpReference.CivilStatus).ToString();
            string district = ((int)LookUpReference.District).ToString();
            string nationality = ((int)LookUpReference.Nationality).ToString();
            string religion = ((int)LookUpReference.Religion).ToString();
            string race = ((int)LookUpReference.Race).ToString();

            var queryResult = (from lc in query.AsNoTracking()
                               join l in context.Locations on lc.LocationID equals l.LocationID
                               join rt in context.ReferenceTypes on lc.CustomerTitle equals rt.LookupKey
                               join ct in context.CardMasters on lc.CardMasterID equals ct.CardMasterID
                               join gt in context.ReferenceTypes on lc.Gender equals gt.LookupKey
                               join cs in context.ReferenceTypes on lc.CivilStatus equals cs.LookupKey
                               join nl in context.ReferenceTypes on new { Nationality = lc.Nationality, LookupType = nationality } equals new { Nationality = nl.LookupKey, nl.LookupType } into nl_join
                               from nl in nl_join.DefaultIfEmpty()
                               join rl in context.ReferenceTypes on new { Religion = lc.Religion, LookupType = religion } equals new { Religion = rl.LookupKey, rl.LookupType } into rl_join
                               from rl in rl_join.DefaultIfEmpty()
                               join rc in context.ReferenceTypes on new { Race = lc.Race, LookupType = race } equals new { Race = rc.LookupKey, rc.LookupType } into rc_join
                               from rc in rc_join.DefaultIfEmpty()
                               where rt.LookupType.Equals(titleType) && gt.LookupType.Equals(genderType)
                               && cs.LookupType.Equals(civilStatus) && lc.IsDelete.Equals(false) && lc.Active.Equals(true)
                               select new
                               {
                                   FieldString1 = lc.CustomerCode,
                                   FieldString2 = (rt.LookupValue + " " + lc.CustomerName),
                                   FieldString3 = lc.NameOnCard,
                                   FieldString4 = lc.CardNo,
                                   FieldString5 = lc.NicNo,
                                   FieldString6 = (lc.Address1 + " " + lc.Address2 + " " + lc.Address3 + " "),
                                   FieldString7 = lc.Mobile,
                                   FieldString8 = lc.Telephone,
                                   FieldString9 = lc.ReferenceNo,
                                   FieldString10 = cs.LookupValue,
                                   FieldString11 = gt.LookupValue,
                                   FieldString12 = lc.Email,
                                   FieldString13 = lc.Fax,
                                   FieldString14 = EntityFunctions.TruncateTime(lc.DateOfBirth),
                                   FieldString15 = (lc.CardIssued ? "Yes" : "No"),
                                   FieldString16 = EntityFunctions.TruncateTime(lc.IssuedOn),
                                   FieldString17 = EntityFunctions.TruncateTime(lc.ExpiryDate),
                                   FieldString18 = EntityFunctions.TruncateTime(lc.RenewedOn),
                                   FieldString19 = EntityFunctions.TruncateTime(lc.CustomerSince),
                                   FieldString20 = ct.CardName,
                                   FieldString21 = lc.Occupation,
                                   FieldString22 = lc.WorkAddres1 + " " + lc.WorkAddres2 + " " + lc.WorkAddres3,
                                   FieldString23 = lc.WorkTelephone,
                                   FieldString24 = lc.WorkMobile,
                                   FieldString25 = lc.WorkEmail,
                                   FieldString26 = lc.WorkFax,
                                   FieldString27 = nl.LookupValue,
                                   FieldString28 = rl.LookupValue,
                                   FieldString29 = rc.LookupValue,
                                   FieldString30 = (lc.Active ? "Yes": "No"),
                                   FieldString31 = EntityFunctions.TruncateTime(lc.AcitiveDate),
                                   FieldString32 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString33 = l.LocationName,
                                   FieldString34 = lc.CPoints,
                                   FieldString35 = lc.EPoints,
                                   FieldString36 = lc.RPoints,
                                   FieldString37 = (lc.IsSuspended ? "Yes" : "No"),
                                   FieldString38 = (lc.IsBlackListed ? "Yes" : "No"),
                                   FieldString39 = (lc.IsCreditAllowed ? "Yes" : "No"),
                                   FieldString40 = (lc.SendUpdatesViaEmail ? "Yes" : "No"),
                                   FieldString41 = (lc.SendUpdatesViaSms ? "Yes" : "No"),
                                   FieldString42 = lc.Remark
                               }); //.ToArray();


            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetLoyaltyCustomerRegiserDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            //var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1",false, true);
            var query = context.LoyaltyCustomers.Where("IsDelete = @0", false);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }
               
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }


                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == false)
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    }
                    else
                    {
                        DateTime FromDate = Convert.ToDateTime(reportConditionsDataStruct.ConditionFrom.Trim());
                        DateTime ToDate = Convert.ToDateTime(reportConditionsDataStruct.ConditionTo.Trim());

                        FromDate = FromDate.AddSeconds(1);
                        ToDate = ToDate.AddDays(1).AddSeconds(-1);

                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "AbeyanceDate":
                                query = (from qr in query
                                         where
                                             !context.InvLoyaltyTransactions.Any(p => p.CustomerID.Equals(qr.LoyaltyCustomerID)
                                             && p.DocumentDate >= FromDate && p.DocumentDate <= ToDate
                                             )
                                         select qr
                                        );
                                query = (from qr in query
                                         where qr.IssuedOn <FromDate
                                         select qr
                                        );
 
                                break;

                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
 
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "CardMasterID":
                                query = (from qr in query
                                         join jt in context.CardMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CardMasterID equals jt.CardMasterID
                                         select qr
                                        );
                                break;
                                
                            //case "UsingStatus":
                            //    query = (from qr in query
                            //             where
                            //                 !context.InvLoyaltyTransactions.Any(p => p.CustomerID.Equals(qr.LoyaltyCustomerID)
                            //                 && p.DocumentDate >= FromDate && p.DocumentDate <= ToDate
                            //                 )
                            //             select qr
                            //            );
                            //    query = (from qr in query
                            //             where qr.IssuedOn <FromDate
                            //             select qr
                            //            );
 
                            //    break;

                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {
                        query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }

                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LocationID":
                                query = (from qr in query
                                         join jt in context.Locations.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.LocationID equals jt.LocationID
                                         select qr
                                        );

                                break;
                            case "Gender":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Gender equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "CivilStatus":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CivilStatus equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "Nationality":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Nationality equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "Religion":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Religion equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            case "Race":
                                query = (from qr in query
                                         join jt in context.ReferenceTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.Religion equals jt.LookupKey
                                         select qr
                                        );

                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                }
            }

  
            string titleType = ((int)LookUpReference.TitleType).ToString();
            string genderType = ((int)LookUpReference.GenderType).ToString();
            string civilStatus = ((int)LookUpReference.CivilStatus).ToString();
            string district = ((int)LookUpReference.District).ToString();
            string nationality = ((int)LookUpReference.Nationality).ToString();
            string religion = ((int)LookUpReference.Religion).ToString();
            string race = ((int)LookUpReference.Race).ToString();
            var queryResult = (from lc in query.AsNoTracking()
                               join l in context.Locations on lc.LocationID equals l.LocationID
                               join rt in context.ReferenceTypes on lc.CustomerTitle equals rt.LookupKey
                               join ct in context.CardMasters on lc.CardMasterID equals ct.CardMasterID
                               join gt in context.ReferenceTypes on lc.Gender equals gt.LookupKey
                               join cs in context.ReferenceTypes on lc.CivilStatus equals cs.LookupKey
                               join nl in context.ReferenceTypes on new { Nationality = lc.Nationality, LookupType = nationality } equals new { Nationality = nl.LookupKey, nl.LookupType } into nl_join
                               from nl in nl_join.DefaultIfEmpty()
                               join rl in context.ReferenceTypes on new { Religion = lc.Religion, LookupType = religion } equals new { Religion = rl.LookupKey, rl.LookupType } into rl_join
                               from rl in rl_join.DefaultIfEmpty()
                               join rc in context.ReferenceTypes on new { Race = lc.Race, LookupType = race } equals new { Race = rc.LookupKey, rc.LookupType } into rc_join
                               from rc in rc_join.DefaultIfEmpty()
                               where rt.LookupType.Equals(titleType) && gt.LookupType.Equals(genderType)
                               && cs.LookupType.Equals(civilStatus) && lc.IsDelete.Equals(false) && lc.Active.Equals(true)
                               select new
                               {
                                   FieldString1 = lc.CustomerCode,
                                   FieldString2 = (rt.LookupValue + " " + lc.CustomerName),
                                   FieldString3 = lc.NameOnCard,
                                   FieldString4 = lc.CardNo,
                                   FieldString5 = lc.NicNo,
                                   FieldString6 = (lc.Address1 + " " + lc.Address2 + " " + lc.Address3 + " "),
                                   FieldString7 = lc.Mobile,
                                   FieldString8 = lc.Telephone,
                                   FieldString9 = lc.ReferenceNo,
                                   FieldString10 = cs.LookupValue,
                                   FieldString11 = gt.LookupValue,
                                   FieldString12 = lc.Email,
                                   FieldString13 = lc.Fax,
                                   FieldString14 = EntityFunctions.TruncateTime(lc.DateOfBirth),
                                   FieldString15 = (lc.CardIssued ? "Yes" : "No"),
                                   FieldString16 = EntityFunctions.TruncateTime(lc.IssuedOn),
                                   FieldString17 = EntityFunctions.TruncateTime(lc.ExpiryDate),
                                   FieldString18 = EntityFunctions.TruncateTime(lc.RenewedOn),
                                   FieldString19 = EntityFunctions.TruncateTime(lc.CustomerSince),
                                   FieldString20 = ct.CardName,
                                   FieldString21 = lc.Occupation,
                                   FieldString22 = lc.WorkAddres1 + " " + lc.WorkAddres2 + " " + lc.WorkAddres3,
                                   FieldString23 = lc.WorkTelephone,
                                   FieldString24 = lc.WorkMobile,
                                   FieldString25 = lc.WorkEmail,
                                   FieldString26 = lc.WorkFax,
                                   FieldString27 = nl.LookupValue,
                                   FieldString28 = rl.LookupValue,
                                   FieldString29 = rc.LookupValue,
                                   FieldString30 = (lc.Active ? "Yes" : "No"),
                                   FieldString31 = EntityFunctions.TruncateTime(lc.AcitiveDate),
                                   FieldString32 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString33 = l.LocationName,
                                   FieldString34 = lc.CPoints,
                                   FieldString35 = lc.EPoints,
                                   FieldString36 = lc.RPoints,
                                   FieldString37 = (lc.IsSuspended ? "Yes" : "No"),
                                   FieldString38 = (lc.IsBlackListed ? "Yes" : "No"),
                                   FieldString39 = (lc.IsCreditAllowed ? "Yes" : "No"),
                                   FieldString40 = (lc.SendUpdatesViaEmail ? "Yes" : "No"),
                                   FieldString41 = (lc.SendUpdatesViaSms ? "Yes" : "No"),
                                   FieldString42 = lc.Remark
                               }); //.ToArray();


            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

           // dtQueryResult = queryResult.ToDataTable();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }



        public DataTable GetLoyaltyCustomerDataTable(string customerCode)
        {
            DataTable dsReportData = new DataTable();

            string titleType = ((int)LookUpReference.TitleType).ToString();
            string genderType = ((int)LookUpReference.GenderType).ToString();
            string civilStatus = ((int)LookUpReference.CivilStatus).ToString();
            string district = ((int)LookUpReference.District).ToString();
            string nationality = ((int)LookUpReference.Nationality).ToString();
            string religion = ((int)LookUpReference.Religion).ToString();
            string race = ((int)LookUpReference.Race).ToString();
            var queryResult = (from lc in context.LoyaltyCustomers
                               join rt in context.ReferenceTypes on lc.CustomerTitle equals rt.LookupKey
                               join ct in context.CardMasters on lc.CardMasterID equals ct.CardMasterID
                               join gt in context.ReferenceTypes on lc.Gender equals gt.LookupKey
                               join cs in context.ReferenceTypes on lc.CivilStatus equals cs.LookupKey
                               join nl in context.ReferenceTypes on new { Nationality = lc.Nationality, LookupType = nationality } equals new { Nationality = nl.LookupKey, nl.LookupType } into nl_join
                               from nl in nl_join.DefaultIfEmpty()
                               join rl in context.ReferenceTypes on new { Religion = lc.Religion, LookupType = religion } equals new { Religion = rl.LookupKey, rl.LookupType } into rl_join
                               from rl in rl_join.DefaultIfEmpty()
                               join rc in context.ReferenceTypes on new { Race = lc.Race, LookupType = race } equals new { Race = rc.LookupKey, rc.LookupType } into rc_join
                               from rc in rc_join.DefaultIfEmpty()
                               where rt.LookupType.Equals(titleType) && gt.LookupType.Equals(genderType) && cs.LookupType.Equals(civilStatus) 
                                && lc.IsDelete.Equals(false) //&& lc.Active.Equals(true)
                                && lc.CustomerCode == customerCode
                               select new
                               {
                                   FieldString1 = ct.CardName,
                                   FieldString2 = lc.CardNo,
                                   FieldString3 = lc.CustomerCode,
                                   FieldString4 = EntityFunctions.TruncateTime(lc.CustomerSince),
                                   FieldString5 = (rt.LookupValue + " " + lc.CustomerName),
                                   FieldString6 = lc.NameOnCard,
                                   FieldString7 = EntityFunctions.TruncateTime(lc.DateOfBirth),
                                   FieldString8 = gt.LookupValue,
                                   FieldString9 = lc.NicNo,
                                   FieldString10 = lc.ReferenceNo,
                                   FieldString11 = lc.Age,
                                   FieldString12 = cs.LookupValue,
                                   FieldString13 = nl.LookupValue,
                                   FieldString14 = rl.LookupValue,
                                   FieldString15 = (lc.Address1 + " \n" + lc.Address2 + " \n" + lc.Address3 + " "),
                                   FieldString16 = lc.Mobile,
                                   FieldString17 = lc.Telephone,
                                   FieldString18 = lc.Fax,
                                   FieldString19 = lc.Email,
                                   FieldString20 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString21 = (lc.SendUpdatesViaSms ? "Yes" : "No"),
                                   FieldString22 = (lc.SendUpdatesViaEmail ? "Yes" : "No"),
                                   FieldString23 = lc.Organization,
                                   FieldString24 = lc.Occupation,
                                   FieldString25 = (lc.WorkAddres1 + " \n " + lc.WorkAddres2 + " \n " + lc.WorkAddres3),
                                   FieldString26 = lc.WorkTelephone,
                                   FieldString27 = lc.WorkEmail,
                                   FieldString28 = lc.WorkMobile,
                                   FieldString29 = lc.WorkFax,
                                   FieldString30 = "",
                                   FieldString31 = (lc.CardIssued ? "Yes" : "No"),
                                   FieldString32 = EntityFunctions.TruncateTime(lc.IssuedOn),
                                   FieldString33 = EntityFunctions.TruncateTime(lc.ExpiryDate),
                                   FieldString34 = EntityFunctions.TruncateTime(lc.RenewedOn),
                                   FieldString35 = (lc.IsCreditAllowed ? "Yes" : "No"),
                                   FieldString36 = lc.CPoints,
                                   FieldString37 = lc.EPoints,
                                   FieldString38 = lc.RPoints,
                                   FieldString39 = (lc.IsSuspended ? "Yes" : "No"),
                                   FieldString40 = (lc.IsBlackListed ? "Yes" : "No"),
                                   FieldString41 = (lc.Active ? "Yes" : "No"),
                                   FieldString42 = EntityFunctions.TruncateTime(lc.AcitiveDate),
                                   FieldString43 = lc.Remark,
                                   FieldString44 = "",
                                   FieldString45 = lc.CreatedUser,
                                   FieldString46 = lc.CustomerImage,
                                   FieldString47 = (lc.SinhalaHinduNewYear ? "Yes" : "No"),
                                   FieldString48 = (lc.Wesak ? "Yes" : "No"),
                                   FieldString49 = (lc.Ramazan ? "Yes" : "No"),
                                   FieldString50 = (lc.ThaiPongal ? "Yes" : "No"),
                                   FieldString51 = (lc.HajFestival ? "Yes" : "No"),
                                   FieldString52 = (lc.Xmas ? "Yes" : "No"),
                                   FieldString53 = rc.LookupValue,
                               }).ToArray();

            dsReportData = queryResult.ToDataTable();

            return dsReportData;
        }
        public DataTable GetCustomervisitsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.LoyaltyCustomers.Where("IsDelete = @0 AND Active =@1",false, true);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from ph in query
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               join lt in context.InvLoyaltyTransactions on ph.LoyaltyCustomerID equals lt.CustomerID
                               where ph.Active == true
                               select new
                               {
                                   ph.CustomerCode,
                                   ph.CustomerName,
                                   lt.Receipt,
                                   lt.Amount,
                                   lt.Points,
                                   l.LocationName,
                                   type = (lt.TransID == 1 ? "Earn" :
                                                     lt.TransID == 2 ? "Redeem" :
                                                      "Err"),
                                   lt.CustomerID
                                   
                                                     
                                  });


            var custcount = (from qs in queryResult
                             let Ccount = (
                             from c in context.InvLoyaltyTransactions where 
                             qs.CustomerID ==  c.CustomerID
                             select c).Count()
                             select new {

                                 FieldString1=qs.CustomerCode,
                                 FieldString2=qs.CustomerName,
                                 FieldString3=qs.Receipt,
                                 FieldDecimal7 = qs.Amount,
                                 FieldDecimal6 = qs.Points,
                                 FieldString4=qs.LocationName,
                                 FieldString5=qs.type,
                                 FieldDecimal5  = Ccount  
                                 
                             }

                            ).ToArray();

            return custcount.ToDataTable();
        }

        public DataTable GetNumberOfVisitsCustomerWiseDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsQueryable();

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from lt in query
                               join ph in context.LoyaltyCustomers on lt.CustomerID equals ph.LoyaltyCustomerID
                               join l in context.Locations on ph.LocationID equals l.LocationID

                               where ph.Active == true
                               group new { ph, lt } by new
                               {
                                   lt.CustomerID,
                                   ph.CustomerCode,
                                   ph.CustomerName,
                                   lt.CardNo,
                                   ph.NicNo,
                                   ph.Mobile,
                                   lt.DocumentDate
                               }
                                   into g

                                   let Ecount = (
                                    from c in context.InvLoyaltyTransactions
                                    where
                                        g.Key.CustomerID == c.CustomerID && c.TransID == 1
                                    select c).Count()

                                   select new
                                   {
                                       CustomerCode = g.Key.CustomerCode,
                                       CustomerName = g.Key.CustomerName,
                                       CardNo = g.Key.CardNo,
                                       NicNo = g.Key.NicNo,
                                       Mobile = g.Key.Mobile,
                                       Ecount = Ecount,
                                       DocumentDate= g.Key.DocumentDate,

                                   }


                                );

            var queryResult2 = (from x in queryResult 
                      group new  {x} by new { x.DocumentDate,x.CustomerName,x.CustomerCode,x.CardNo,x.Mobile }
                      into gr

                      select new 
                      {
                          FieldString1=gr.Key.CustomerCode,
                          FieldString2=gr.Key.CustomerName,
                          FieldString3=gr.Key.CardNo,
                          FieldString4 = gr.Key.Mobile,
                          FieldDecimal6=gr.Count(),
                          FieldDecimal7= gr.Sum(a=>a.x.Ecount)
                      }
                    ).ToArray();



            return queryResult2.ToDataTable();
        }

        public DataTable GetCustomerStatementDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
 
            StringBuilder selectFields = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsQueryable();
            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }

            var queryResult = (from lt in query
                               join c in context.LoyaltyCustomers on lt.CustomerID  equals c.LoyaltyCustomerID
                               join l in context.Locations on lt.LocationID equals l.LocationID
                               group new { lt, l } by new
                               {
                                   lt.TransID,
                                   lt.CardType,
                                   lt.CustomerID,
                                   c.CustomerCode,
                                   c.CustomerName,
                                   c.Mobile,
                                   c.NicNo,
                                   c.CardNo,
                                   lt.DocumentDate,
                                   l.LocationID,
                                   l.LocationCode,
                                   l.LocationName,
                                   lt.Receipt
                               } 
                               into g
                               select new
                               {
                                  CustomerID= g.Key.CustomerID,
                                  CustomerCode= g.Key.CustomerCode,
                                  CustomerName= g.Key.CustomerName,
                                  CardNo= g.Key.CardNo,
                                  DocumentDate= g.Key.DocumentDate,
                                  Receipt= g.Key.Receipt,
                                  NicNo= g.Key.NicNo,
                                  Mobile= g.Key.Mobile,
                                  LocationCode= g.Key.LocationCode,
                                  LocationName= g.Key.LocationName,
                                  amount = (g.Sum(x=>x.lt.Amount)),
                                  ep = (g.Key.TransID == 1 ? g.Sum(t => t.lt.Points) : 0),
                                  rp = (g.Key.TransID == 2 ? g.Sum(t => t.lt.Points) : 0),
                               });


            var FResult = (from qs in queryResult
                           group new { qs } by
                            new
                            {
                                qs.CustomerCode,
                                qs.CustomerName,
                                qs.CardNo,
                                qs.DocumentDate,
                                qs.Receipt,
                                qs.NicNo,
                                qs.Mobile,
                                qs.LocationCode,
                                qs.LocationName
                            }
                               into g
                               select new
                               {

                                   FieldString1 = g.Key.CustomerCode,
                                   //FieldString2 = g.Key.CustomerName,
                                   FieldString2 = g.Key.CardNo,
                                   FieldString3 = EntityFunctions.TruncateTime(g.Key.DocumentDate),
                                   FieldString4 = g.Key.Receipt,
                                   FieldString5 = g.Key.NicNo,
                                   FieldString6 = g.Key.Mobile,
                                   FieldString7 = g.Key.LocationName,
                                   FieldDecimal7 = g.Sum(x => x.qs.amount),
                                   FieldDecimal6 = g.Sum(x => x.qs.ep),
                                   FieldDecimal5 = g.Sum(x => x.qs.rp),
                                   //FieldDecimal4 = qs.ep - qs.rp,

                               }
                            ).ToArray();





            return FResult.ToDataTable();
        }

        public DataTable GetBestCustomersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsQueryable();

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from lt in query
                               join ph in context.LoyaltyCustomers on lt.CustomerID equals ph.LoyaltyCustomerID
                               join l in context.Locations on ph.LocationID equals l.LocationID

                               where ph.Active == true
                               group new { ph, lt } by new
                               {
                                   lt.CustomerID,
                                   ph.CustomerCode,
                                   ph.CustomerName,
                                   lt.CardNo,
                                   ph.NicNo,
                                   ph.Mobile
                               }
                                   into g

                                   let Ecount = (
                                    from c in context.InvLoyaltyTransactions
                                    where
                                        g.Key.CustomerID == c.CustomerID && c.TransID == 1
                                    select c).Count()

                                   select new
                                   {
                                       FieldString1 = g.Key.CustomerCode,
                                       FieldString2 = g.Key.CustomerName,
                                       FieldString3 = g.Key.CardNo,
                                       FieldString4 = g.Key.NicNo,
                                       FieldString5 = g.Key.Mobile,
                                       FieldDecimal7 = g.Sum(x => x.lt.Amount),
                                       FieldDecimal6 = g.Sum(x => x.lt.Points),
                                       FieldDecimal5 = Ecount
                                   }
 

                               ).ToArray();
 

            return queryResult.ToDataTable();
        }

        public DataTable GetRptMembershipUpgradesAnalysisDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder selectFields = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsQueryable();

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from lt in query
								join ph in context.LoyaltyCustomers on lt.CustomerID  equals ph.LoyaltyCustomerID
								join l in context.Locations on lt.LocationID equals l.LocationID
							    join cm in context.CardMasters on lt.CardType equals cm.CardMasterID
							   where ph.Active == true
							   group new { ph,lt } by new {
							   lt.CustomerID,
							   ph.CustomerCode , 
							   ph.CustomerName,
							   lt.CardNo,
							   ph.NicNo,
							   ph.Mobile,
							   ph.CustomerSince,
							   lt.CardType,
							   cm.CardName
							   }
							   into g
							   
							   let visit = (
								from c in context.InvLoyaltyTransactions where 
								g.Key.CustomerID ==  c.CustomerID && g.Key.CardType == c.CardType && c.TransID==1
								select c).Count()
							 
							   select new
							   {
								   FieldString1 = g.Key.CustomerCode,
								   FieldString2 = g.Key.CustomerName,
								   FieldString3 = g.Key.CardNo,
								   FieldString4 = g.Key.NicNo,
								   FieldString5 = g.Key.CustomerSince,
								   FieldString6 = g.Key.CardName,
								   FieldDecimal7 = g.Sum(x => x.lt.Amount),
								   FieldDecimal6 = visit
								   
							   }

                               ).ToArray();


            return queryResult.ToDataTable();
        }

        public DataTable GetCustomerBehaviorDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {

            StringBuilder selectFields = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsQueryable();
 

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }

            var queryResult = (from lt in query
                               join ph in context.CardMasters on lt.CardType equals ph.CardMasterID
                               join l in context.Locations on lt.LocationID equals l.LocationID
                               where lt.TransID == 1
                               group new { ph, lt, l } by new { l.LocationID, l.LocationCode, l.LocationName, ph.CardCode, ph.CardName }
                                   into g
                                   select new
                                   {
                                       FieldString1 = g.Key.LocationCode,
                                       FieldString2 = g.Key.LocationName,
                                       FieldString4 = g.Key.CardCode,
                                       FieldString5 = g.Key.CardName,
                                       FieldDecimal7 = g.Sum(x => x.lt.Points),
                                       FieldDecimal6 = g.Count()
                                   }).ToArray();


    
            return queryResult.ToDataTable();
        }

        public DataTable GetLostAndRenewalDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
 
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LoyaltyCustomers.Where("Active=True");

             // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (
							from ph in query 
							join l in context.Locations on ph.LocationID equals l.LocationID
                            join lr in context.LostAndRenews on ph.LoyaltyCustomerID equals lr.LoyaltyCustomerID 
							where ph.Active == true
							  
							   select new
							   {
								   FieldString1 = ph.CustomerCode,
								   FieldString2 = ph.CustomerName,
								   FieldString3 = ph.CardNo,
								   FieldString4 = ph.NicNo,
								   FieldString5 = lr.Remark,
								   FieldString6 = ph.RenewedOn,
								   FieldString7 = l.LocationName,
 
								   
							   }).ToArray();
 

            return queryResult.ToDataTable();
        }
 
        // Get selection data for Report Generator Combo boxes
 
        public ArrayList GetSelectionDataLCInActive(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResulData = context.LoyaltyCustomers
                            .Where("Active == @0 ", false)
                            .OrderBy("" + reportDataStruct.DbColumnName.Trim() + "") //.Select("new(DocumentNo)")
                            .Select(reportDataStruct.DbColumnName.Trim());

             
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResulData)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResulData)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }

        #region GetNewCode

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.LoyaltyCustomers.Max(c => c.CustomerCode.Trim().Equals(string.Empty) ? null : c.CustomerCode.Trim());
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }


        #endregion

        public DataTable GetAllActiveLoyalityCustomerssDataTable()  
        {
            DataTable tblLoyalityCustomers = new DataTable();
            var query = from d in context.LoyaltyCustomers.AsNoTracking().AsEnumerable()
                        where d.IsDelete == false
                        select new
                        {
                            d.CustomerCode,
                            d.CustomerName,
                            d.CPoints,
                            d.CreditPeriod,
                            d.CreditLimit,
                            d.Mobile,
                            d.Telephone,
                            d.NicNo
                        };
            return query.AsEnumerable().ToDataTable();
        }

        public List<LoyaltyCustomer> GetLoyalityCustomerlistByCustomerCode(string customerCode)
        {
            List<LoyaltyCustomer> rtnList = new List<LoyaltyCustomer>();
            LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();

            var qry = (from lc in context.LoyaltyCustomers
                       where lc.CustomerCode == customerCode
                       select new
                       {
                           lc.CustomerCode,
                           lc.CustomerName,
                           lc.CardNo,
                           lc.NicNo,
                           lc.Active
                       }).ToArray();

            foreach (var temp in qry)
            {
                loyaltyCustomer = new LoyaltyCustomer();

                loyaltyCustomer.CustomerCode = temp.CustomerCode;
                loyaltyCustomer.CustomerName = temp.CustomerName;
                loyaltyCustomer.NicNo = temp.NicNo;
                loyaltyCustomer.Active = temp.Active;

                rtnList.Add(loyaltyCustomer);
            }

            return rtnList;
        }

        public List<LoyaltyCustomer> GetLoyalityCustomerlistByNicNo(string NicNo) 
        {
            List<LoyaltyCustomer> rtnList = new List<LoyaltyCustomer>();
            LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();

            var qry = (from lc in context.LoyaltyCustomers
                       where lc.NicNo == NicNo
                       select new
                       {
                           lc.CustomerCode,
                           lc.CustomerName,
                           lc.CardNo,
                           lc.NicNo,
                           lc.Active
                       }).ToArray();

            foreach (var temp in qry)
            {
                loyaltyCustomer = new LoyaltyCustomer();

                loyaltyCustomer.CustomerCode = temp.CustomerCode;
                loyaltyCustomer.CustomerName = temp.CustomerName;
                loyaltyCustomer.NicNo = temp.NicNo;
                loyaltyCustomer.Active = temp.Active;

                rtnList.Add(loyaltyCustomer);
            }

            return rtnList;
        }

        public List<LoyaltyCustomer> GetLoyalityCustomerlistByCardNo(string CardNo) 
        {
            List<LoyaltyCustomer> rtnList = new List<LoyaltyCustomer>();
            LoyaltyCustomer loyaltyCustomer = new LoyaltyCustomer();

            var qry = (from lc in context.LoyaltyCustomers
                       where lc.CardNo == CardNo
                       select new
                       {
                           lc.CustomerCode,
                           lc.CustomerName,
                           lc.CardNo,
                           lc.NicNo,
                           lc.Active
                       }).ToArray();

            foreach (var temp in qry)
            {
                loyaltyCustomer = new LoyaltyCustomer();

                loyaltyCustomer.CustomerCode = temp.CustomerCode;
                loyaltyCustomer.CustomerName = temp.CustomerName;
                loyaltyCustomer.NicNo = temp.NicNo;
                loyaltyCustomer.Active = temp.Active;

                rtnList.Add(loyaltyCustomer);
            }

            return rtnList;
        }


        public bool CheckExistingNic(string nic)
        {
            bool recordFound = false;
            LoyaltyCustomer exsistLoyaltyCustomer = new LoyaltyCustomer();

            exsistLoyaltyCustomer = context.LoyaltyCustomers.Where(lc => lc.NicNo.Equals(nic)).FirstOrDefault();

            if (exsistLoyaltyCustomer == null) { recordFound = false; }
            else { recordFound = true; }

            return recordFound;
        }

        public bool CheckExistingCardNo(string cardNo)  
        {
            bool recordFound = false;
            LoyaltyCustomer exsistLoyaltyCustomer = new LoyaltyCustomer();

            exsistLoyaltyCustomer = context.LoyaltyCustomers.Where(lc => lc.CardNo.Equals(cardNo)).FirstOrDefault();

            if (exsistLoyaltyCustomer == null) { recordFound = false; }
            else { recordFound = true; }

            return recordFound;
        }

        //public bool CheckExistingCardNoModify(string cardNo, LoyaltyCustomer loyaltyCustomer)  
        //{
        //    //bool recordFound = false;
        //    //LoyaltyCustomer exsistLoyaltyCustomer = new LoyaltyCustomer();

        //    //var qry=from lc in context.LoyaltyCustomers
        //    //        where lc

        //    //exsistLoyaltyCustomer = context.LoyaltyCustomers.Where(lc => lc.CardNo.Equals(cardNo)).FirstOrDefault();

        //    //if (exsistLoyaltyCustomer == null) { recordFound = false; }
        //    //else { recordFound = true; }

        //    //return recordFound;
        //}

        public void GetManuallyAddedPointsAllCustomersAllType(DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "SELECT lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate,l.LocationName, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END) AS EPoints, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Amount ELSE 0 END) AS Amount " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lt.LocationID " +
                                "WHERE lt.Receipt LIKE 'M%' " +
                                "AND CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate,l.LocationName";

            this.dataSetName = "ManuallyAddedPoints";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetManuallyAddedPointsAllLocationsSelectedType(DateTime fromDate, DateTime toDate, int cardType)  
        {
            this.sqlStatement = "SELECT lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate,l.LocationName, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END) AS EPoints, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Amount ELSE 0 END) AS Amount " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lt.LocationID " +
                                "WHERE lt.Receipt LIKE 'M%' " +
                                "AND lt.CardType=" + cardType + " " +
                                "AND CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate,l.LocationName";

            this.dataSetName = "ManuallyAddedPoints";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetManuallyAddedPointsAllTypesSelectedlocations(DateTime fromDate, DateTime toDate, int locationID)
        {
            this.sqlStatement = "SELECT lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END) AS EPoints, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Amount ELSE 0 END) AS Amount " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "WHERE lt.Receipt LIKE 'M%' " +
                                "AND lt.LocationID=" + locationID + " " +
                                "AND CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate";

            this.dataSetName = "ManuallyAddedPoints";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetManuallyAddedPointsSelectedTypesSelectedlocations(DateTime fromDate, DateTime toDate, int locationID, int cardType) 
        {
            this.sqlStatement = "SELECT lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END) AS EPoints, " +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Amount ELSE 0 END) AS Amount " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "WHERE lt.Receipt LIKE 'M%' " +
                                "AND lt.CardType=" + cardType + " " +
                                "AND lt.LocationID=" + locationID + " " +
                                "AND CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lc.CardNo,lt.Receipt,lt.UnitNo,lt.DocumentDate";

            this.dataSetName = "ManuallyAddedPoints";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public DataTable GetCustomerTransactionsAllCustomersAlllocations(DateTime fromDate, DateTime toDate, decimal amount, int cardType)    
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataTable rtnTable = new DataTable();
                if (amount == 0)
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                else
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.Amount >= amount
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                return rtnTable;
            }
        }


        public DataTable GetCustomerTransactionsAllCustomersWithlocations(DateTime fromDate, DateTime toDate, decimal amount, int locationID, int cardType)  
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataTable rtnTable = new DataTable();
                if (amount == 0)
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.LocationID == locationID
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                else
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.LocationID == locationID
                               && lt.Amount >= amount
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                return rtnTable;
            }
        }


        public DataTable GetCustomerTransactionsSelectedCustomersAlllocations(DateTime fromDate, DateTime toDate, long customerIDFrom, long customerIDTo, decimal amount, int cardType) 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataTable rtnTable = new DataTable();
                if (amount == 0)
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                else
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                               && lt.Amount >= amount
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                return rtnTable;
            }
        }


        public DataTable GetCustomerTransactionsSelectedCustomersSelectedlocations(DateTime fromDate, DateTime toDate, long customerIDFrom, long customerIDTo, decimal amount, int locationID, int cardType)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataTable rtnTable = new DataTable();
                if (amount == 0)
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                               && lt.LocationID == locationID
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                else
                {
                    var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                               join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                               join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                               join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                               //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                               where
                               lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                               && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                               && lt.Amount >= amount
                               && lt.LocationID == locationID
                               && lc.CardMasterID == cardType
                               select new
                               {
                                   lc.CardNo,
                                   CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                                   cm.CardName,
                                   lc.CPoints,
                                   lt.Receipt,
                                   lt.Amount,
                                   l.LocationName,
                                   lt.DocumentDate,
                                   lt.UnitNo,
                                   //em.EmployeeName,
                                   lt.TransID,
                                   lc.NicNo,
                                   lt.Points
                               });
                    rtnTable = qry.ToDataTable();
                }
                return rtnTable;
            }
        }


        public DataTable GetCustomerStatement(string customerCode)  
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataTable rtnTable = new DataTable();
                var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                           join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                           join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                           join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                           join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                           where
                           lc.CustomerCode == customerCode
                           select new
                           {
                               lc.CardNo,
                               lc.CustomerName,
                               lc.CustomerCode,
                               cm.CardName,
                               lc.CPoints,
                               lt.Receipt,
                               lt.Amount,
                               l.LocationName,
                               lt.DocumentDate,
                               lt.UnitNo,
                               em.EmployeeName,
                               lt.TransID,
                               lc.NicNo,
                               lt.Points
                           }).OrderBy(a => a.DocumentDate);
                return qry.ToDataTable();
            }
        }

        public List<CardMaster> GetAllCardTypes()
        {
            return context.CardMasters.Where(l => l.IsDelete.Equals(false)).OrderBy(l => l.CardMasterID).ToList();
        }

        public DataTable GetCustomerStatementAllCustomersAlllocations(DateTime fromDate, DateTime toDate) 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                           join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                           join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                           join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                           //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                           where
                           lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                           select new
                           {
                               lc.CardNo,
                               CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                               cm.CardName,
                               lc.CPoints,
                               lt.Receipt,
                               lt.Amount,
                               l.LocationName,
                               lt.DocumentDate,
                               lt.UnitNo,
                               //em.EmployeeName,
                               lt.TransID,
                               lc.NicNo,
                               lt.Points
                           });
                return qry.ToDataTable();
            }
        }


        public DataTable GetCustomerStatementAllCustomersWithlocations(DateTime fromDate, DateTime toDate, int locationID) 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                           join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                           join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                           join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                           //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                           where
                           lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                           && lt.LocationID == locationID
                           select new
                           {
                               lc.CardNo,
                               CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                               cm.CardName,
                               lc.CPoints,
                               lt.Receipt,
                               lt.Amount,
                               l.LocationName,
                               lt.DocumentDate,
                               lt.UnitNo,
                               //em.EmployeeName,
                               lt.TransID,
                               lc.NicNo,
                               lt.Points
                           });
                return qry.ToDataTable();
            }
        }


        public DataTable GetCustomerStatementSelectedCustomersAlllocations(DateTime fromDate, DateTime toDate, long customerIDFrom, long customerIDTo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                           join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                           join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                           join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                           //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                           where
                           lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                           && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                           select new
                           {
                               lc.CardNo,
                               CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                               cm.CardName,
                               lc.CPoints,
                               lt.Receipt,
                               lt.Amount,
                               l.LocationName,
                               lt.DocumentDate,
                               lt.UnitNo,
                               //em.EmployeeName,
                               lt.TransID,
                               lc.NicNo,
                               lt.Points
                           });
                return qry.ToDataTable();
            }
        }


        public DataTable GetCustomerStatementSelectedCustomersSelectedlocations(DateTime fromDate, DateTime toDate, long customerIDFrom, long customerIDTo, int locationID) 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lc in context.LoyaltyCustomers.AsNoTracking()
                           join lt in context.InvLoyaltyTransactions.AsNoTracking() on lc.LoyaltyCustomerID equals lt.CustomerID
                           join cm in context.CardMasters.AsNoTracking() on lc.CardMasterID equals cm.CardMasterID
                           join l in context.Locations.AsNoTracking() on lt.LocationID equals l.LocationID
                           //join em in context.Employees.AsNoTracking() on lt.CashierID equals em.EmployeeID
                           where
                           lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                           && lt.CustomerID >= customerIDFrom && lt.CustomerID <= customerIDTo
                           && lt.LocationID == locationID
                           select new
                           {
                               lc.CardNo,
                               CustomerName = (lc.CustomerName.Equals(string.Empty) ? "New Customer, NIC : " + lc.NicNo : lc.CustomerName),
                               cm.CardName,
                               lc.CPoints,
                               lt.Receipt,
                               lt.Amount,
                               l.LocationName,
                               lt.DocumentDate,
                               lt.UnitNo,
                               //em.EmployeeName,
                               lt.TransID,
                               lc.NicNo,
                               lt.Points
                           });
                return qry.ToDataTable();
            }
        }


        //public DataTable GetLocationWiseTypeWiseSummeryAllLocations(int cardType, DateTime fromDate, DateTime toDate)
        //{
        //    using (ERPDbContext context = new ERPDbContext())
        //    {
        //        var qr = (from lt in context.InvLoyaltyTransactions
        //                  join lc in context.LoyaltyCustomers on lt.CustomerID equals lc.LoyaltyCustomerID
        //                  join l in context.Locations on lt.LocationID equals l.LocationID
        //                  where lc.CardMasterID == cardType &&
        //                  lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
        //                  group new { lt, l } by new
        //                  {
        //                      lt.TransID,
        //                      l.LocationID,
        //                      l.LocationCode,
        //                      l.LocationName,
        //                      lt.Receipt
        //                  } into g
        //                  select new
        //                  {
        //                      g.Key.LocationID,
        //                      g.Key.LocationCode,
        //                      g.Key.LocationName,
        //                      ep = (g.Key.TransID == 1 ? g.Sum(t => t.lt.Points) : 0),
        //                      rp = (g.Key.TransID == 2 ? g.Sum(t => t.lt.Points) : 0),
        //                      g.Key.Receipt
        //                  });

        //        var queryResult = (from a in qr
        //                           group a by new { a.LocationID, a.LocationCode, a.LocationName } into g
        //                           select new
        //                           {
        //                               LocationID = g.Key.LocationID,
        //                               LocationCode = g.Key.LocationCode,
        //                               LocationName = g.Key.LocationName,
        //                               TotalEarnpoints = g.Sum(a => a.ep),
        //                               TotalRedeempoints = g.Sum(a => a.rp),
        //                               //Pointsdiff = g.Sum(a => a.ep) - g.Sum(a => a.rp),
        //                               Pointsdiff = g.Sum(a => a.rp) - g.Sum(a => a.ep),
        //                               PersentageRedeem = (((g.Sum(a => a.rp)) / (g.Sum(a => a.ep))) * 100),
        //                               AvgRedeemPerBill = 0
        //                           }).ToArray();

        //        var finel = (from fnl in queryResult
        //                     let NoOfBillsEarned = (
        //                            from c in context.InvLoyaltyTransactions
        //                            where
        //                                c.TransID == 1 && fnl.LocationID == c.LocationID
        //                            select c.Receipt).Count()

        //                     let NoOfBillsRedeemed = (
        //                            from c in context.InvLoyaltyTransactions
        //                            where
        //                            c.TransID == 2 && fnl.LocationID == c.LocationID
        //                            select c.Receipt).Count()

        //                     select new
        //                     {
        //                         LocationCode = fnl.LocationCode,
        //                         LocationName = fnl.LocationName,
        //                         TotalEarnpoints = fnl.TotalEarnpoints,
        //                         TotalRedeempoints = fnl.TotalRedeempoints,
        //                         Pointsdiff = fnl.Pointsdiff,
        //                         PersentageRedeem = fnl.PersentageRedeem,
        //                         NoOfBillsEarned = NoOfBillsEarned,
        //                         NoOfBillsRedeemed = NoOfBillsRedeemed,
        //                         AvgRedeemPerBill = ((NoOfBillsRedeemed > 0 ? fnl.TotalRedeempoints / NoOfBillsRedeemed : 0))
        //                     }).ToArray();

        //        return finel.ToDataTable();
        //    }
        //}

        public void GetDataSource(int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "select l.LocationName, l.LocationPrefixCode, sum(case lt.transid when 1 then lt.Points  else 0 end)as TotalEarnpoints, sum(case lt.transid when 2 then lt.points else 0 end)as TotalRedeempoints, " +
                                "sum(case lt.transid when 1 then 1 else 0 end) as NoOfBillsEarned, sum(case lt.transid when 2 then 1 else 0 end) as NoOfBillsRedeemed " +
                                "from InvLoyaltyTransaction lt INNER JOIN dbo.Location l ON lt.LocationID = l.LocationID " +
                                "where cast(DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) and CardType='" + cardType + "'   " +
                                "AND lt.LocationID!=1 " +
                                "GROUP BY l.LocationName, l.LocationPrefixCode ORDER BY l.LocationName";

            this.dataSetName = "LocationWiseSummery";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSourceArapaima(DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "select l.LocationName, l.LocationPrefixCode, sum(case lt.transid when 1 then lt.Points  else 0 end)as TotalEarnpoints, sum(case lt.transid when 2 then lt.points else 0 end)as TotalRedeempoints, " +
                                "sum(case lt.transid when 1 then 1 else 0 end) as NoOfBillsEarned, sum(case lt.transid when 2 then 1 else 0 end) as NoOfBillsRedeemed " +
                                "from InvLoyaltyTransaction lt INNER JOIN dbo.Location l ON lt.LocationID = l.LocationID " +
                                "where cast(DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) AND CardType IN (1,2) " +
                                "AND lt.LocationID!=1 " +
                                "GROUP BY l.LocationName, l.LocationPrefixCode ORDER BY l.LocationName";

            this.dataSetName = "LocationWiseSummery";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSource(int locationID, int cardType, DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "select (SELECT LocationName FROM dbo.Location WHERE LocationID = " + locationID + ")AS LocationName, " +
                                "(SELECT LocationPrefixCode FROM dbo.Location WHERE LocationID = " + locationID + ")AS LocationPrefixCode, " +
                                "sum(case lt.transid when 1 then lt.Points  else 0 end)as TotalEarnpoints, sum(case lt.transid when 2 then lt.points else 0 end)as TotalRedeempoints, " +
                                "sum(case lt.transid when 1 then 1 else 0 end) as NoOfBillsEarned, sum(case lt.transid when 2 then 1 else 0 end) as NoOfBillsRedeemed " +
                                "from InvLoyaltyTransaction lt " +
                                "where cast(DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) " +
                                "and CardType=" + cardType + " AND lt.LocationID = " + locationID + " ";

            this.dataSetName = "LocationWiseSummery";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSourceArapaimaSelectedLocation(int locationID, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "select (SELECT LocationName FROM dbo.Location WHERE LocationID = " + locationID + ")AS LocationName, " +
                                "(SELECT LocationPrefixCode FROM dbo.Location WHERE LocationID = " + locationID + ")AS LocationPrefixCode, " +
                                "sum(case lt.transid when 1 then lt.Points  else 0 end)as TotalEarnpoints, sum(case lt.transid when 2 then lt.points else 0 end)as TotalRedeempoints, " +
                                "sum(case lt.transid when 1 then 1 else 0 end) as NoOfBillsEarned, sum(case lt.transid when 2 then 1 else 0 end) as NoOfBillsRedeemed " +
                                "from InvLoyaltyTransaction lt " +
                                "where cast(DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) " +
                                "and CardType IN (1,2) AND lt.LocationID = " + locationID + " ";

            this.dataSetName = "LocationWiseSummery";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        #region active pending reprots

        public DataTable GetPendingOrders(string company, DateTime fromDate, DateTime toDate) 
        {
            DataTable dt = new DataTable();
            if (company != "")
            {

                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and sd.DocumentStatus !=4 and sd.BalanceQty>0 and sh.Company ='" + company + "' and  " +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) order by sh.DocumentNo";
                //" cast(sh.DocumentDate as date) BETWEEN cast('" + fromDate + "' as date)   AND cast('" + toDate + "' as date) ";

                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }
            else
            {
                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and  sd.DocumentStatus !=4 and sd.BalanceQty>0 and  " +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) order by sh.DocumentNo";
                //" cast(sh.DocumentDate as date) BETWEEN cast('" + fromDate + "' as date)   AND cast('" + toDate + "' as date) ";

                dt = CommonService.ExecuteSqlQuery(sqlStatement);            
            }


            return dt;
        }
        public DataTable GetActiveOrders(string company, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();

            if (company != "")
            {
                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and sd.BalanceQty>0 and sd.DocumentStatus !=4  and sh.Company ='" + company + "' and  " +
                                    " sh.SalesOrderHeaderId not in (SELECT ReferenceDocumentID FROM dbo.InvSalesHeader)  and" +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) order by sh.DocumentNo";
                //" CONVERT(DATE,sh.DocumentDate,103) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) ";


                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }
            else
            {
                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                        " where sd.IsDelete=1 and sd.BalanceQty>0 and sd.DocumentStatus !=4 and  " +
                                        " sh.SalesOrderHeaderId not in (SELECT ReferenceDocumentID FROM dbo.InvSalesHeader)  and" +
                                        " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) order by sh.DocumentNo";
                //" CONVERT(DATE,sh.DocumentDate,103) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) ";


                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }

            return dt;
        }


        public DataTable GetPendingOrdersFilter(string company, DateTime fromDate, DateTime toDate,int type,string paraName)
        {
            DataTable dt = new DataTable();
            if (company != "")
            {

                sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and sd.DocumentStatus !=4 and sd.BalanceQty>0 and sh.Company ='" + company + "' and  " +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) ";
                if (type == 1)
                {
                    sqlStatement = sqlStatement + " and c.Customercode = '"+ paraName +"'";
                }
                else if (type == 2)
                {
                    sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
                }

                sqlStatement = sqlStatement + " order by sh.DocumentNo";

                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }
            else
            {
                sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and sd.DocumentStatus !=4 and sd.BalanceQty>0 and  " +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103)  ";

                if (type == 1)
                {
                    sqlStatement = sqlStatement + " and c.Customercode = '" + paraName + "'";
                }
                else if (type == 2)
                {
                    sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
                }
                sqlStatement = sqlStatement + " order by sh.DocumentNo";
                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }
            return dt;
        }


        public DataTable GetActiveOrdersFilter(string company, DateTime fromDate, DateTime toDate, int type, string paraName)
        {
            DataTable dt = new DataTable();

            if (company != "")
            {
                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                    " where sd.IsDelete=1 and sd.BalanceQty>0 and sd.DocumentStatus !=4  and sh.Company ='" + company + "' and  " +
                                    " sh.SalesOrderHeaderId not in (SELECT ReferenceDocumentID FROM dbo.InvSalesHeader)  and" +
                                    " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103)  ";
                if (type == 1)
                {
                    sqlStatement = sqlStatement + " and c.Customercode = '" + paraName + "'";
                }
                else if (type == 2)
                {
                    sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
                }
                sqlStatement = sqlStatement + " order by sh.DocumentNo";
                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }
            else
            {
                this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo , " +
                                    " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,sh.Unit,sh.Company,sh.DocumentDate,sh.DeliverDate " +
                                    " ,sh.Colour,  sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge,d.DepartmentName,ca.CategoryName,sc.SubCategoryName " +
                                    " ,sc2.SubCategory2Name, sd.OrderQty, (sd.OrderQty-sd.BalanceQty)Qty,(CASE WHEN sd.BalanceQty=0 THEN 'Sold' ELSE 'Pending' END) Pending, " +
                                    " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel " +
                                    " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                    " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                    " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                    " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                    " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                    " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                    " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID " +
                                        " where sd.IsDelete=1 and sd.BalanceQty>0 and sd.DocumentStatus !=4 and  " +
                                        " sh.SalesOrderHeaderId not in (SELECT ReferenceDocumentID FROM dbo.InvSalesHeader)  and" +
                                        " cast(sh.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103) ";

                if (type == 1)
                {
                    sqlStatement = sqlStatement + " and c.Customercode = '" + paraName + "'";
                }
                else if (type == 2)
                {
                    sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
                }
                sqlStatement = sqlStatement + " order by sh.DocumentNo";                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                
                dt = CommonService.ExecuteSqlQuery(sqlStatement);
            }

            return dt;
        }


        public DataTable GetActiveOrdersFilterGrid()
        {
            DataTable dt = new DataTable();

            this.sqlStatement = " SELECT sh.DocumentNo OrderNo,sh.ReferenceNo ,CAST(sh.DocumentDate AS DATE) DocumentDate,CAST(sh.DeliverDate AS DATE)DeliverDate " +
                                " ,sh.Company , (CASE WHEN sh.salesorderheaderid in (select ih.ReferenceDocumentID from InvSalesHeader ih where ih.DocumentStatus=1)  THEN 'Active' ELSE 'Pending' END ) ActivePending , " +
                                  " (CASE WHEN sd.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel ," +
                                  " c.CustomerName,sp.SalesPersonName,sh.Remark DeliverAddress,cast(sh.DocumentDate as date),cast(sh.DeliverDate as date) " +
                                  " ,sd.ProductCode,sd.ProductName,sd.Size,sd.Gauge  " +
                                  " , sd.OrderQty, (sd.BalanceQty)Qty " +
                                  " FROM dbo.SalesOrderHeader sh INNER JOIN dbo.SalesOrderDetail sd ON sd.SalesOrderHeaderID=sh.SalesOrderHeaderID  " +
                                  " INNER JOIN dbo.Customer c ON sh.CustomerID = c.CustomerID  " +
                                  " INNER JOIN dbo.InvSalesPerson sp ON sh.InvSalesPersonID = sp.InvSalesPersonID  " +
                                  " INNER JOIN dbo.InvDepartment d ON sh.DepartmentID= d.InvDepartmentID  " +
                                  " INNER JOIN dbo.InvCategory ca ON sh.CategoryID= ca.InvCategoryID  " +
                                  " INNER JOIN dbo.InvSubCategory sc ON sh.SubCategoryID = sc.InvSubCategoryID " +
                                  " INNER JOIN dbo.InvSubCategory2 sc2 ON sh.SubCategory2ID = sc2.InvSubCategory2ID ";
                                   
                 
                sqlStatement = sqlStatement + " order by sh.DocumentNo";
                dt = CommonService.ExecuteSqlQuery(sqlStatement);
             

            return dt;
        }


        public DataTable GetActiveOrdersSalesFilterGrid()
        {
            DataTable dt = new DataTable();

            this.sqlStatement = "SELECT 'I'+ SUBSTRING(ih.DocumentNo,2,7) InvoiceNo,ih.ReferenceNo , c.CustomerName,sp.SalesPersonName,ih.Remark DeliverAddress, " +
                                "oh.Unit,oh.Company,ih.DocumentDate,oh.DeliverDate, " +
                                "oh.Colour,id.ProductCode,id.ProductName,id.Qty,id.NetAmount ," +
                                "  (CASE WHEN id.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel,id.Waight,id.Height,id.IsDeliver " +
                                " from InvSalesHeader ih INNER JOIN dbo.InvSalesDetail id ON ih.InvSalesHeaderID = id.InvSalesHeaderID " +
                                " inner join SalesOrderHeader oh on ih.ReferenceDocumentID = oh.SalesOrderHeaderID " +
                                "  INNER JOIN dbo.Customer c ON  ih.CustomerID = c.CustomerID  INNER JOIN dbo.InvSalesPerson sp ON " +
                                "  ih.SalesPersonID = sp.InvSalesPersonID ";
                               


            sqlStatement = sqlStatement + " order by 'I'+ SUBSTRING(ih.DocumentNo,2,7)";
            dt = CommonService.ExecuteSqlQuery(sqlStatement);


            return dt;
        }

        #endregion





        public DataTable GetSalesDtl(string company, DateTime fromDate, DateTime toDate,int type, string paraName)
        {
            this.sqlStatement = "SELECT 'I'+ SUBSTRING(ih.DocumentNo,2,7) InvoiceNo,ih.ReferenceNo , c.CustomerName,sp.SalesPersonName,ih.Remark DeliverAddress, " +
                                "oh.Unit,oh.Company,ih.DocumentDate,oh.DeliverDate, " +
                                "oh.Colour,id.ProductCode,id.ProductName,id.Qty,id.NetAmount ," +
                                "  (CASE WHEN id.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel,id.Waight,id.Height,id.IsDeliver " +
                                " from InvSalesHeader ih INNER JOIN dbo.InvSalesDetail id ON ih.InvSalesHeaderID = id.InvSalesHeaderID " +
                                " inner join SalesOrderHeader oh on ih.ReferenceDocumentID = oh.SalesOrderHeaderID " +
                                "  INNER JOIN dbo.Customer c ON  ih.CustomerID = c.CustomerID  INNER JOIN dbo.InvSalesPerson sp ON " +
                                "  ih.SalesPersonID = sp.InvSalesPersonID " +
                                " where id.IsDeliver =1 AND  ih.DocumentStatus !=4  and oh.Company ='" + company + "' and  " +
                                " cast(ih.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103)  ";

            if (type == 1)
            {
                sqlStatement = sqlStatement + " and c.Customercode = '" + paraName + "'";
            }
            else if (type == 2)
            {
                sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
            }
            sqlStatement = sqlStatement + " order by ih.DocumentNo";      
            DataTable dt = new DataTable();
            dt = CommonService.ExecuteSqlQuery(sqlStatement);


            return dt;
        }

        public DataTable GetSalesDtl(DateTime fromDate, DateTime toDate, int type, string paraName)
        {
            this.sqlStatement = "SELECT 'I'+ SUBSTRING(ih.DocumentNo,2,7) InvoiceNo,ih.ReferenceNo , c.CustomerName,sp.SalesPersonName,ih.Remark DeliverAddress, " +
                                "oh.Unit,oh.Company,ih.DocumentDate,oh.DeliverDate, " +
                                "oh.Colour,id.ProductCode,id.ProductName,id.Qty,id.NetAmount ," +
                                "  (CASE WHEN id.DocumentStatus = 4 THEN 'Cancel' ELSE 'Not Cancel' END) Cancel,id.Waight,id.Height,id.IsDeliver " +
                                " from InvSalesHeader ih INNER JOIN dbo.InvSalesDetail id ON ih.InvSalesHeaderID = id.InvSalesHeaderID " +
                                " inner join SalesOrderHeader oh on ih.ReferenceDocumentID = oh.SalesOrderHeaderID " +
                                "  INNER JOIN dbo.Customer c ON  ih.CustomerID = c.CustomerID  INNER JOIN dbo.InvSalesPerson sp ON " +
                                "  ih.SalesPersonID = sp.InvSalesPersonID " +
                                " where id.IsDeliver =1 AND  ih.DocumentStatus !=4  and  " +
                                " cast(ih.DocumentDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103)  AND CONVERT(DATE,'" + toDate + "',103)  ";
            if (type == 1)
            {
                sqlStatement = sqlStatement + " and c.Customercode = '" + paraName + "'";
            }
            else if (type == 2)
            {
                sqlStatement = sqlStatement + " and sP.SalesPersonCode = '" + paraName + "'";
            }
            sqlStatement = sqlStatement + " order by ih.DocumentNo";
            DataTable dt = new DataTable();
            dt = CommonService.ExecuteSqlQuery(sqlStatement);


            return dt;
        }



        public void GetDataSourceCustomerVisitSelectedLocations(int locationID, int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "select lc.CustomerCode,(CASE lc.CustomerName WHEN '' THEN 'NEW CUSTOMER : '+lc.CustomerCode ELSE lc.CustomerName END) AS CustomerName," +
                                "lt.CustomerID,count( lc.CustomerCode)as TotalVisit ," +
                                "SUM(CASE TransID WHEN 1 THEN Amount ELSE 0 END)AS TotalPurchases," +
                                "(SELECT LocationName FROM dbo.Location WHERE LocationID=" + locationID + "), " +
                                "(SELECT SUM(Points) FROM dbo.InvLoyaltyTransaction WHERE TransID=1 AND LocationID=" + locationID + " AND CustomerId=lt.CustomerID) AS TotalEarns," +
                                "(ISNULL((SELECT SUM(Points) FROM dbo.InvLoyaltyTransaction WHERE TransID=2 AND LocationID=" + locationID + " AND CustomerId=lt.CustomerID),0.00)) AS TotalRedeems " +
                                "from InvLoyaltyTransaction lt  inner join LoyaltyCustomer lc on lc.LoyaltyCustomerID  = lt.CustomerID " +
                                "where cast(DocumentDate as date) between CONVERT(DATE,'" + fromDate + "',103) and CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lt.TransID=1 and lt.LocationID !=1 AND lt.CardType = " + cardType + " " +
                                "AND lt.LocationID=" + locationID + " " +
                                "group by lc.CustomerCode,lc.CustomerName,lt.CustomerID";

            this.dataSetName = "CustomerVisitDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }


        public void GetDataSourceSummeryVisitAllLocations(int cardType, DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "SELECT l.LocationName,l.LocationPrefixCode, " +
                                "COUNT(DISTINCT(lc.customerCode)) AS TotalCustomers, " +
                                "SUM(CASE lt.transid WHEN 1 THEN 1 ELSE 0 END) AS TotalEarnBills, " +
                                "SUM(CASE lt.transid WHEN 2 THEN 1 ELSE 0 END) AS TotalRedeemBIlls, " +
                                "SUM(CASE lt.transid WHEN 1 THEN 1 ELSE 0 END) AS TotalVisit, " +
                                "SUM(CASE lt.transid WHEN 1 THEN lt.Points  ELSE 0 END)AS TotalEarns, " +
                                "SUM(CASE lt.transid WHEN 2 THEN lt.points ELSE 0 END)AS TotalRedeems, " +
                                "SUM(CASE TransID WHEN 1 THEN Amount ELSE 0 END)AS TotalPurchases " +
                                "FROM InvLoyaltyTransaction lt " +
                                "INNER JOIN LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN Location l ON lt.LocationID=l.LocationID " +
                                "WHERE cast(lt.documentdate AS DATE) BETWEEN CONVERT(DATETIME,'" + fromDate + "',103) AND CONVERT(DATETIME,'" + toDate + "',103) " +
                                "AND lt.LocationID!=1" +
                                "AND lt.CardType=" + cardType + " GROUP BY l.LocationName, l.LocationPrefixCode";
            
            this.dataSetName = "CustomerVisitDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }


        public void GetDataSourceSummeryVisitSelectedLocations(int locationID, int cardType, DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "SELECT l.LocationName, " +
                                "COUNT(DISTINCT(lc.customerCode)) AS TotalCustomers, " +
                                "SUM(CASE lt.transid WHEN 1 THEN 1 ELSE 0 END) AS TotalEarnBills, " +
                                "SUM(CASE lt.transid WHEN 2 THEN 1 ELSE 0 END) AS TotalRedeemBIlls, " +
                                "SUM(CASE lt.transid WHEN 1 THEN 1 ELSE 0 END) AS TotalVisit, " +
                                "SUM(CASE lt.transid WHEN 1 THEN lt.Points  ELSE 0 END)AS TotalEarns, " +
                                "SUM(CASE lt.transid WHEN 2 THEN lt.points ELSE 0 END)AS TotalRedeems, " +
                                "SUM(CASE TransID WHEN 1 THEN Amount ELSE 0 END)AS TotalPurchases " +
                                "FROM InvLoyaltyTransaction lt " +
                                "INNER JOIN LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN Location l ON lt.LocationID=l.LocationID " +
                                "WHERE cast(lt.documentdate AS DATE) BETWEEN CONVERT(DATETIME,'" + fromDate + "',103) AND CONVERT(DATETIME,'" + toDate + "',103) " +
                                "AND lt.LocationID=" + locationID + " " +
                                "AND lt.CardType=" + cardType + " GROUP BY l.LocationName";

            this.dataSetName = "CustomerVisitDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public DataTable GetLocationWiseTypeWiseSummeryAllLocations()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qr = (from lt in context.TempLocationsWiseLoyaltyAnalysis
                          select new
                          {
                              LocationCode=lt.LocationCode,
                              LocationName = lt.LocationName,
                              CardIssu=lt.CardIssu,
                              UsedCard=lt.UsedCard,
                              UsedPoints=lt.UsedPoints,
                              NonUsedCard=lt.NonUsedCard,
                              NonUsedPoints=lt.NonUsedPoints,
                              CARDINABEYANCE=lt.CardInAbeyance,
                              POINTSINABEYANCE =lt.PointsInAbeyance,
                              ACTIVECARD=lt.ActiveCard,
                              ACTIVEPOINTS=lt.ActivePoints,
                              INACTCARD=lt.InactiveCard,
                              INACTPOINTS=lt.InactivePoints,
                              USEDCARDPer=lt.UsedCardPer,

                          });

                return qr.ToDataTable();
            }
        }


        public bool SpLocationWiseLoyaltyAnalysisCardType(int type, int Custtype, int locationId, long cardType, DateTime fromDate, DateTime toDate, long fromCust, long toCust, long uniqueId)
        {

            {

                var parameter = new DbParameter[] 
                    { 
                       
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@TYPE", Value=type},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CUSTTYPE", Value=Custtype},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LOCATIONID", Value=locationId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CARDTYPE", Value=cardType},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DATEFROM", Value=fromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DATETO", Value=toDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CUSTFROM", Value=fromCust},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CUSTTO", Value=toCust}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyId", Value=Common.LoggedCompanyID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UniqueId", Value=uniqueId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@USER", Value=Common.LoggedUser}
                    };


                if (CommonService.ExecuteStoredProcedure("spLocationWiseLoyaltyAnalysis", parameter))
                {

                    return true;
                }
                else
                { return false; }

            }
        }
        public DataTable GetLocationWiseLoyaltyAnalysisCardType(int cardType, DateTime fromDate, DateTime toDate)
        {
            DateTime Abeyancedate,InactiveDate;

            Abeyancedate = toDate.AddYears(-1);
            InactiveDate = toDate.AddYears(-3);

            var query = context.InvLoyaltyTransactions.AsNoTracking();

            var qrx = (from l in context.Locations
                       join lt in query on l.LocationID equals lt.LocationID
                       where lt.CardType == cardType &&
                          lt.DocumentDate >= fromDate && lt.DocumentDate <= toDate
                       group new { lt, l } by new
                       {
                           lt.TransID,
                           l.LocationID,
                           l.LocationCode,
                           l.LocationName,
                           lt.Receipt
                       } into g
                       select new
                       {
                           g.Key.LocationID,
                           g.Key.LocationCode,
                           g.Key.LocationName,
                           ep = (g.Key.TransID == 1 ? g.Sum(t => t.lt.Points) : 0),
                           rp = (g.Key.TransID == 2 ? g.Sum(t => t.lt.Points) : 0),
                           g.Key.Receipt
                       }).AsNoTracking();

            var x = (from a in qrx
                     let CardIssu = (
                               from c in context.LoyaltyCustomers
                               where
                                    a.LocationID == c.LocationID
                               group new { c } by new
                               {
                                   c.CustomerCode
                               }
                                   into g
                                   select g).Count()

                     let UsedCard = (
                              from c in context.InvLoyaltyTransactions
                              where
                                   a.LocationID == c.LocationID
                              group new { c } by new
                              {
                                  c.CardNo
                              }
                                  into g

                                  select g).Count()

                     let Ecount = (
                              from c in context.InvLoyaltyTransactions
                              where
                                   c.TransID == 1 && a.LocationID == c.LocationID
                              select c).Count()
                     let Rcount = (
                              from c in context.InvLoyaltyTransactions
                              where
                                   c.TransID == 2 && a.LocationID == c.LocationID
                              select c).Count()

                     let NonUsedCard = (
                                 from c in context.LoyaltyCustomers
                                 where !(from o in context.InvLoyaltyTransactions where o.LocationID == c.LocationID 
                                         select o.CustomerID)
                                     .Contains(c.LoyaltyCustomerID) && a.LocationID == c.LocationID
                                 select c).Count()
                     let CARDINABEYANCE = (
                                 from c in context.InvLoyaltyTransactions
                                 where
                                     a.LocationID == c.LocationID && c.DocumentDate < Abeyancedate
                                 group new { c } by new
                                 {
                                     c.CardNo
                                 }
                                     into g
                                     select g).Count()

                     let POINTSINABEYANCE = (
                                 from t in
                                     (from t in context.InvLoyaltyTransactions
                                      where
                                      a.LocationID == t.LocationID && t.DocumentDate < Abeyancedate
                                      select new
                                      {
                                          t.Points,
                                          Dummy = "x"
                                      })
                                 group t by new { t.Dummy } into g
                                 select new
                                 {
                                     Column1 = (System.Decimal?)g.Sum(p => p.Points)
                                 })
                     let ACTIVECARDS = (
                                 from c in context.InvLoyaltyTransactions
                                 where
                                     a.LocationID == c.LocationID && c.DocumentDate > InactiveDate
                                 group new { c } by new
                                 {
                                     c.CardNo
                                 }
                                     into g
                                     select g).Count()
                     let ACTIVEPOINTS = (
                                 from t in
                                     (from t in context.InvLoyaltyTransactions
                                      where
                                      a.LocationID == t.LocationID && t.DocumentDate > InactiveDate
                                      select new
                                      {
                                          t.Points,
                                          Dummy = "x"
                                      })
                                 group t by new { t.Dummy } into g
                                 select new
                                 {
                                     Column1 = (System.Decimal?)g.Sum(p => p.Points)
                                 })
                     let INACTCARD = (
                                from c in context.InvLoyaltyTransactions
                                where
                                    a.LocationID == c.LocationID && c.DocumentDate < InactiveDate
                                group new { c } by new
                                {
                                    c.CardNo
                                }
                                    into g
                                    select g).Count()
                     let INACTPOINTS = (
                               from t in
                                   (from t in context.InvLoyaltyTransactions
                                    where
                                    a.LocationID == t.LocationID && t.DocumentDate < InactiveDate
                                    select new
                                    {
                                        t.Points,
                                        Dummy = "x"
                                    })
                               group t by new { t.Dummy } into g
                               select new
                               {
                                   Column1 = (System.Decimal?)g.Sum(p => p.Points)
                               })

                     select new
                     {
                         a.LocationCode,
                         a.LocationName,
                         CardIssu,
                         UsedCard,
                         UsedPoints = a.rp,
                         NonUsedCard,
                         NonUsedPoints = 0,
                         CARDINABEYANCE,
                         POINTSINABEYANCE,
                         ACTIVECARDS,
                         ACTIVEPOINTS,
                         INACTCARD,
                         INACTPOINTS,
                         USEDCARDPer = UsedCard / ACTIVECARDS * 100

                     }

                           ).AsNoTracking().ToArray();

            DataTable dtx = new DataTable();
            dtx = x.ToDataTable();

            return x.ToDataTable();
        }

        public void GetTypeWiseSelectedCustomerDetails(long customerIDFrom, long customerIDTo, int cardType) 
        {
            this.sqlStatement = "SELECT lc.CustomerCode,(CASE lc.CustomerName WHEN '' THEN 'NEW CUSTOMER : '+lc.CustomerCode ELSE lc.CustomerName END)AS CustomerName," +
                                "lt.CustomerID,(SELECT MAX(DocumentDate)FROM dbo.InvLoyaltyTransaction WHERE CustomerID=lt.CustomerID) AS DocumentDate," +
                                "(SELECT Location.LocationName FROM dbo.InvLoyaltyTransaction " +
                                "INNER JOIN dbo.Location ON dbo.InvLoyaltyTransaction.LocationID = dbo.Location.LocationID " +
                                "WHERE CustomerID=lt.CustomerID " +
                                "AND DocumentDate=(SELECT MAX(DocumentDate)FROM dbo.InvLoyaltyTransaction WHERE CustomerID=lt.CustomerID)) AS LastVisitedLocation, " +
                                "lc.CardNo,cm.CardName,lc.NicNo FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.CardMaster cm ON lt.CardType=cm.CardMasterID " +
                                "WHERE lt.CardType= " + cardType + " AND lt.LocationID!=1 " +
                                "AND lc.LoyaltyCustomerID BETWEEN " + customerIDFrom + " AND " + customerIDTo + "" +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lt.CustomerID, " +
                                "lc.CardNo,cm.CardName,lc.NicNo ";

            this.dataSetName = "TypeWiseCustomerDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }


        public void GetTypeWiseAllCustomerDetails(int cardType) 
        {
            this.sqlStatement = "SELECT lc.CustomerCode,(CASE lc.CustomerName WHEN '' THEN 'NEW CUSTOMER : '+lc.CustomerCode ELSE lc.CustomerName END)AS CustomerName," +
                                "lt.CustomerID,(SELECT MAX(DocumentDate)FROM dbo.InvLoyaltyTransaction WHERE CustomerID=lt.CustomerID) AS DocumentDate," +
                                "(SELECT Location.LocationName FROM dbo.InvLoyaltyTransaction " +
                                "INNER JOIN dbo.Location ON dbo.InvLoyaltyTransaction.LocationID = dbo.Location.LocationID " +
                                "WHERE CustomerID=lt.CustomerID " +
                                "AND DocumentDate=(SELECT MAX(DocumentDate)FROM dbo.InvLoyaltyTransaction WHERE CustomerID=lt.CustomerID) GROUP BY Location.LocationName) AS LastVisitedLocation, " +
                                "lc.CardNo,cm.CardName,lc.NicNo FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON lt.CustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.CardMaster cm ON lt.CardType=cm.CardMasterID " +
                                "WHERE lt.CardType= " + cardType + " AND lt.LocationID!=1 " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,lt.CustomerID, " +
                                "lc.CardNo,cm.CardName,lc.NicNo ";

            this.dataSetName = "TypeWiseCustomerDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public bool GetLoyaltyTransactions(DateTime fromDate, DateTime toDate)
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateFrom", Value=fromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateTo", Value=toDate},
                    };
            if (CommonService.ExecuteStoredProcedure("spGoldCardSelection", parameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetLoyaltyTransactions(DateTime fromDate, DateTime toDate, int cardType, int select)
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateFrom", Value=fromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateTo", Value=toDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Select", Value=select},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CardType", Value=cardType},
                    };
            if (CommonService.ExecuteStoredProcedure("spTopLoyaltyCustomerPerformance", parameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetGoldCardSelectionAllCustomers(decimal amount, int totalVisits)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lt in context.TempLoyaltyTransactionSummerys
                           where lt.TotalPurchaseAmount >= (amount != 0 ? amount : 0)
                           && lt.TotalVisits >= (totalVisits != 0 ? totalVisits : 0)
                           select new
                           {
                               lt.CustomerCode,
                               CustomerName = (lt.CustomerName == string.Empty ? "NEW CUSTOMER : " + lt.CustomerCode : lt.CustomerName),
                               lt.CustomerID,
                               lt.EPoints,
                               lt.RPoints,
                               lt.TotalPurchaseAmount,
                               lt.TotalVisits,
                               lt.CardNo,
                               lt.CardName,
                               lt.NicNo
                           }).ToArray().OrderByDescending(a => a.TotalPurchaseAmount);

                return qry.ToDataTable();
            }
        }


        public DataTable GetGoldCardSelectionSelectedCustomers(decimal amount, long customerID, int totalVisits)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lt in context.TempLoyaltyTransactionSummerys
                           where lt.CustomerID == customerID
                           && lt.TotalPurchaseAmount >= (amount != 0 ? amount : 0)
                           && lt.TotalVisits >= (totalVisits != 0 ? totalVisits : 0)
                           select new
                           {
                               lt.CustomerCode,
                               CustomerName = (lt.CustomerName == string.Empty ? "NEW CUSTOMER : " + lt.CustomerCode : lt.CustomerName),
                               lt.CustomerID,
                               lt.EPoints,
                               lt.RPoints,
                               lt.TotalPurchaseAmount,
                               lt.TotalVisits,
                               lt.CardNo,
                               lt.CardName,
                               lt.NicNo
                           }).ToArray().OrderByDescending(a => a.TotalPurchaseAmount);

                return qry.ToDataTable();
            }
        }


        public DataTable GetTopCustomers() 
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from lt in context.TempLoyaltyTransactionSummerys
                           select new
                           {
                               lt.CustomerCode,
                               CustomerName = (lt.CustomerName == string.Empty ? "NEW CUSTOMER : " + lt.CustomerCode : lt.CustomerName),
                               lt.CustomerID,
                               lt.EPoints,
                               lt.RPoints,
                               lt.TotalPurchaseAmount,
                               lt.TotalVisits,
                               lt.CardNo,
                               lt.CardName,
                               lt.NicNo
                           }).ToArray().OrderByDescending(a => a.TotalPurchaseAmount);

                return qry.ToDataTable();
            }
        }

        public void GetDataSourceCashierPerformanceSelectedLocations(int locationID, int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID," +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END)  AS TotalEarns," +
                                "SUM(CASE lt.TransID WHEN 2 THEN lt.Points ELSE 0 END)  AS TotalRedeemes," +
                                "SUM(CASE lt.TransID WHEN 1 THEN 1 ELSE 0 END) AS BilledCustomers," +
                                "SUM(CASE lt.TransID WHEN 2 THEN 1 ELSE 0 END) AS RedeemedCustomers " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.Employee em ON lt.CashierID=em.EmployeeID " +
                                "INNER JOIN dbo.Location l ON lt.LocationID=l.LocationID " +
                                "WHERE CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lt.LocationID!=1 AND lt.LocationID=" + locationID + " AND lt.CardType=" + cardType + "  " +
                                "GROUP BY em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID " +
                                "ORDER BY lt.LocationID";

            this.dataSetName = "CashierPerformances";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSourceCashierPerformanceSelectedLocationsArapaima(int locationID, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID," +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END)  AS TotalEarns," +
                                "SUM(CASE lt.TransID WHEN 2 THEN lt.Points ELSE 0 END)  AS TotalRedeemes," +
                                "SUM(CASE lt.TransID WHEN 1 THEN 1 ELSE 0 END) AS BilledCustomers," +
                                "SUM(CASE lt.TransID WHEN 2 THEN 1 ELSE 0 END) AS RedeemedCustomers " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.Employee em ON lt.CashierID=em.EmployeeID " +
                                "INNER JOIN dbo.Location l ON lt.LocationID=l.LocationID " +
                                "WHERE CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lt.LocationID!=1 AND lt.LocationID=" + locationID + " AND lt.CardType IN (1,2)  " +
                                "GROUP BY em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID " +
                                "ORDER BY lt.LocationID";

            this.dataSetName = "CashierPerformances";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSourceCashierPerformanceAllLocations(int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID," +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END)  AS TotalEarns," +
                                "SUM(CASE lt.TransID WHEN 2 THEN lt.Points ELSE 0 END)  AS TotalRedeemes," +
                                "SUM(CASE lt.TransID WHEN 1 THEN 1 ELSE 0 END) AS BilledCustomers," +
                                "SUM(CASE lt.TransID WHEN 2 THEN 1 ELSE 0 END) AS RedeemedCustomers " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.Employee em ON lt.CashierID=em.EmployeeID " +
                                "INNER JOIN dbo.Location l ON lt.LocationID=l.LocationID " +
                                "WHERE CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lt.LocationID!=1 AND lt.CardType=" + cardType + "  " +
                                "GROUP BY em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID " +
                                "ORDER BY lt.LocationID";

            this.dataSetName = "CashierPerformances";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetDataSourceCashierPerformanceAllLocationsArapaima(DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "SELECT em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID," +
                                "SUM(CASE lt.TransID WHEN 1 THEN lt.Points ELSE 0 END)  AS TotalEarns," +
                                "SUM(CASE lt.TransID WHEN 2 THEN lt.Points ELSE 0 END)  AS TotalRedeemes," +
                                "SUM(CASE lt.TransID WHEN 1 THEN 1 ELSE 0 END) AS BilledCustomers," +
                                "SUM(CASE lt.TransID WHEN 2 THEN 1 ELSE 0 END) AS RedeemedCustomers " +
                                "FROM dbo.InvLoyaltyTransaction lt " +
                                "INNER JOIN dbo.Employee em ON lt.CashierID=em.EmployeeID " +
                                "INNER JOIN dbo.Location l ON lt.LocationID=l.LocationID " +
                                "WHERE CAST(lt.DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lt.LocationID!=1 AND lt.CardType IN (1,2)  " +
                                "GROUP BY em.EmployeeName, l.LocationName,lt.CashierID,lt.LocationID " +
                                "ORDER BY lt.LocationID";

            this.dataSetName = "CashierPerformances";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void ViewReportCardIssueSelectedLocation(int locationID, int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT lc.CustomerCode, " +
                                "(CASE lc.CustomerName WHEN '' THEN 'NEW CUSTOMER : '+lc.CustomerCode ELSE lc.NameOnCard END)AS CustomerName, " +
                                "lc.NicNo,lc.CardNo,l.LocationName AS IssuedLocation, " +
                                "lc.IssuedOn AS IssuedDate " +
                                "FROM dbo.LoyaltyCustomer lc " +
                                "INNER JOIN dbo.Location l ON lc.LocationID=l.LocationID " +
                                "WHERE  CAST(lc.CreatedDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.LocationID=" + locationID + "   AND lc.CardMasterID=" + cardType + " " +
                                "AND lc.IsRegByPOS = 1 " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,l.LocationName,lc.NicNo,lc.IssuedOn,lc.NameOnCard,lc.CardNo ";

            this.dataSetName = "CardIssuedDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void ViewReportCardIssueSelectedLocationArapaima(int locationID, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT lc.CustomerCode, " +
                                "(CASE lc.CustomerName WHEN '' THEN 'NEW CUSTOMER : '+lc.CustomerCode ELSE lc.NameOnCard END)AS CustomerName, " +
                                "lc.NicNo,lc.CardNo,l.LocationName AS IssuedLocation, " +
                                "lc.IssuedOn AS IssuedDate " +
                                "FROM dbo.LoyaltyCustomer lc " +
                                "INNER JOIN dbo.Location l ON lc.LocationID=l.LocationID " +
                                "WHERE  CAST(lc.CreatedDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.LocationID=" + locationID + "   AND lc.CardMasterID IN (1,2) " +
                                "AND lc.IsRegByPOS = 1 " +
                                "GROUP BY lc.CustomerCode,lc.CustomerName,l.LocationName,lc.NicNo,lc.IssuedOn,lc.NameOnCard,lc.CardNo ";

            this.dataSetName = "CardIssuedDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void ViewReportCardIssueAllLocation(int cardType, DateTime fromDate, DateTime toDate)  
        {
            this.sqlStatement = "SELECT l.LocationCode,l.LocationName,l.LocationPrefixCode AS LocationPrefixCode, " +
                                "COUNT(lc.CustomerCode) AS CardCount " +
                                "FROM dbo.LoyaltyCustomer lc " +
                                "INNER JOIN dbo.Location l ON lc.LocationID=l.LocationID " +
                                "WHERE  CAST(lc.IssuedOn AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID = " + cardType + " " +
                                "AND lc.LocationID != 1 " +
                                "AND lc.IsRegByPOS = 1 " +
                                "GROUP BY l.LocationCode,l.LocationName,l.LocationPrefixCode ";

            this.dataSetName = "CardIssuedDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void ViewReportCardIssueAllLocationArapaima(DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT l.LocationCode,l.LocationName,l.LocationPrefixCode AS LocationPrefixCode, " +
                                "COUNT(lc.CustomerCode) AS CardCount " +
                                "FROM dbo.LoyaltyCustomer lc " +
                                "INNER JOIN dbo.Location l ON lc.LocationID=l.LocationID " +
                                "WHERE  CAST(lc.IssuedOn AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID IN (1,2) " +
                                "AND lc.LocationID != 1 " +
                                "AND lc.IsRegByPOS = 1 " +
                                "GROUP BY l.LocationCode,l.LocationName,l.LocationPrefixCode ";

            this.dataSetName = "CardIssuedDetails";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetCardUsageAllType(DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT * FROM dbo.CrmCardUsageSummery " +
                                "WHERE CAST(DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103)";

            this.dataSetName = "CardUsage";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetCardUsageSelectedType(DateTime fromDate, DateTime toDate, int cardType) 
        {
            this.sqlStatement = "SELECT * FROM dbo.CrmCardUsageSummery " +
                                "WHERE CAST(DocumentDate AS DATE) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND CardType=" + cardType + "  ";
                

            this.dataSetName = "CardUsage";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dsReport, dataSetName);
        }

        public void GetLostAndReNewDataSourceArapaima(DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT l.LocationName,lc.CustomerName,lc.NameOnCard,cm.CardName,lr.OldCustomerCode,lr.NewCustomerCode, " +
                                "lr.OldCardNo,lr.NewCardNo,lr.CreatedUser AS RenewedBy,lr.RenewedDate,lr.Remark,lc.IssuedOn " +
                                "FROM dbo.LostAndRenew lr " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON  lr.LoyaltyCustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lc.LocationID " +
                                "INNER JOIN dbo.CardMaster cm ON lc.CardMasterID=cm.CardMasterID " +
                                "WHERE cast(lr.RenewedDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID IN (1,2) ";


            this.dataSetName = "CrmLostAndReNew";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        public void GetLostAndReNewDataSourceArapaima(int cardType, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT l.LocationName,lc.CustomerName,lc.NameOnCard,cm.CardName,lr.OldCustomerCode,lr.NewCustomerCode, " +
                                "lr.OldCardNo,lr.NewCardNo,lr.CreatedUser AS RenewedBy,lr.RenewedDate,lr.Remark,lc.IssuedOn " +
                                "FROM dbo.LostAndRenew lr " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON  lr.LoyaltyCustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lc.LocationID " +
                                "INNER JOIN dbo.CardMaster cm ON lc.CardMasterID=cm.CardMasterID " +
                                "WHERE cast(lr.RenewedDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID = " + cardType + " ";


            this.dataSetName = "CrmLostAndReNew";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }


        public void GetLostAndRenewDataSourceArapaimaSelectedLocation(int locationID, DateTime fromDate, DateTime toDate) 
        {
            this.sqlStatement = "SELECT l.LocationName,lc.CustomerName,lc.NameOnCard,cm.CardName,lr.OldCustomerCode,lr.NewCustomerCode, " +
                                "lr.OldCardNo,lr.NewCardNo,lr.CreatedUser AS RenewedBy,lr.RenewedDate,lr.Remark,lc.IssuedOn " +
                                "FROM dbo.LostAndRenew lr " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON  lr.LoyaltyCustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lc.LocationID " +
                                "INNER JOIN dbo.CardMaster cm ON lc.CardMasterID=cm.CardMasterID " +
                                "WHERE cast(lr.RenewedDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID IN (1,2) " +
                                "AND lc.LocationID=" + locationID + "";

            this.dataSetName = "CrmLostAndReNew";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }


        public void GetLostAndRenewDataSourceArapaimaSelectedLocation(int locationID, int cardType, DateTime fromDate, DateTime toDate)
        {
            this.sqlStatement = "SELECT l.LocationName,lc.CustomerName,lc.NameOnCard,cm.CardName,lr.OldCustomerCode,lr.NewCustomerCode, " +
                                "lr.OldCardNo,lr.NewCardNo,lr.CreatedUser AS RenewedBy,lr.RenewedDate,lr.Remark,lc.IssuedOn " +
                                "FROM dbo.LostAndRenew lr " +
                                "INNER JOIN dbo.LoyaltyCustomer lc ON  lr.LoyaltyCustomerID=lc.LoyaltyCustomerID " +
                                "INNER JOIN dbo.Location l ON l.LocationID=lc.LocationID " +
                                "INNER JOIN dbo.CardMaster cm ON lc.CardMasterID=cm.CardMasterID " +
                                "WHERE cast(lr.RenewedDate as date) BETWEEN CONVERT(DATE,'" + fromDate + "',103) AND CONVERT(DATE,'" + toDate + "',103) " +
                                "AND lc.CardMasterID = " + cardType + " " +
                                "AND lc.LocationID=" + locationID + "";

            this.dataSetName = "CrmLostAndReNew";
            dsReport.Clear();
            SqlDataAdapter da = new SqlDataAdapter(sqlStatement, mscn);
            da.Fill(dsReport, dataSetName);
        }

        public List<PointsBreakdown> GetInitialBreakdown()
        {
            List<PointsBreakdown> rtnList = new List<PointsBreakdown>();
            var qry = from bd in context.PointsBreakdowns select bd;
            foreach (var item in qry)
            {
                PointsBreakdown pointsBreakdown = new PointsBreakdown();
                pointsBreakdown.RangeFrom = item.RangeFrom;
                pointsBreakdown.RangeTo = item.RangeTo;
                pointsBreakdown.PointsBreakdownID = item.PointsBreakdownID;

                rtnList.Add(pointsBreakdown);
            }
            return rtnList;
        }

        public bool UpdatePointsBreakdownList(List<PointsBreakdown> pointsBreakdownList, DateTime dateFrom, DateTime dateTo)  
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [TempPointsBreakdown]");

                foreach (PointsBreakdown temp in pointsBreakdownList)
                {
                    TempPointsBreakdown tempPointsBreakdownsave = new TempPointsBreakdown();
                    tempPointsBreakdownsave.PointsBreakdownID = temp.PointsBreakdownID;
                    tempPointsBreakdownsave.RangeFrom = temp.RangeFrom;
                    tempPointsBreakdownsave.RangeTo = temp.RangeTo;
                    tempPointsBreakdownsave.Range = "";

                    context.TempPointsBreakdowns.Add(tempPointsBreakdownsave);
                    context.SaveChanges();
                }

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateFrom", Value=dateFrom},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateTo", Value=dateTo},
                    };

                if (CommonService.ExecuteStoredProcedure("spPointsBreakdown", parameter))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataTable GetPointsBreakDownDetails()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var qry = (from pb in context.TempPointsBreakdowns
                           select new
                           {
                               pb.PointsBreakdownID,
                               pb.Range,
                               pb.RangeFrom,
                               pb.RangeTo,
                               pb.CustomerCount,
                               pb.PointsTotal
                           }).OrderByDescending(a => a.PointsBreakdownID);

                return qry.ToDataTable();  
            }
        }


        public List<PointsBreakdown> GetDeletePointsRange(List<PointsBreakdown> existingList, PointsBreakdown pointsBreakdownPrm)
        {
            PointsBreakdown pointsBreakdownRemove=new PointsBreakdown();
            pointsBreakdownRemove = existingList.Where(p => p.PointsBreakdownID.Equals(pointsBreakdownPrm.PointsBreakdownID)).FirstOrDefault();

            if (pointsBreakdownRemove != null)
            {
                existingList.Remove(pointsBreakdownRemove);
            }

            return existingList;
        }

    }
}
