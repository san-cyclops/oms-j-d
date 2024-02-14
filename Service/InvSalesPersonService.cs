using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MoreLinq;
using Domain;
using Data;
using System.Data.Entity.Validation;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class InvSalesPersonService
    {
        ERPDbContext context = new ERPDbContext();


        #region public methods

        public void AddInvSalesPerson(InvSalesPerson invSalesPerson)
        {
            try
            {
                context.InvSalesPersons.Add(invSalesPerson);
                context.SaveChanges();
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

        public void UpdateInvSalesPerson(InvSalesPerson invSalesPerson)
        {
            this.context.Entry(invSalesPerson).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        public InvSalesPerson GetInvSalesPersonByCode(string invSalesPersonCode)
        {
            return context.InvSalesPersons.Where(s => s.SalesPersonCode == invSalesPersonCode && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        public InvSalesPerson GetInvSalesPersonByName(string invSalesPersonName)
        {
            return context.InvSalesPersons.Where(s => s.SalesPersonName == invSalesPersonName && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Search Sales Person by ID
        /// </summary>
        /// <param name="invSalesPersonID"></param>
        /// <returns></returns>
        public InvSalesPerson GetInvSalesPersonByID(long invSalesPersonID)
        {
            return context.InvSalesPersons.Where(s => s.InvSalesPersonID == invSalesPersonID && s.IsDelete == false).FirstOrDefault();
        }

        public List<InvSalesPerson> GetAllInvSalesPerson()
        {
            return context.InvSalesPersons.Where(s => s.IsDelete == false).ToList();
        }

        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.InvSalesPersons.Max(s => s.SalesPersonCode);
            if (GetNewCode != null)
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                GetNewCode = "0";

            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }



        public string[] GetAllInvSalesPersonCodes()
        {
            List<string> SalesmanList = context.InvSalesPersons.Where(s => s.IsDelete.Equals(false)).Select(s => s.SalesPersonCode).ToList();
            return SalesmanList.ToArray();

        }

        public string[] GetAllInvSalesPersonNames()
        {
            List<string> SalesmanList = context.InvSalesPersons.Where(s => s.IsDelete.Equals(false)).Select(s => s.SalesPersonName).ToList();
            return SalesmanList.ToArray();

        }

        public DataTable GetAllInvSalesPersonsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvSalesPersons.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {query =query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",reportConditionsDataStruct.ConditionFrom.Trim(),reportConditionsDataStruct.ConditionTo.Trim());}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {query =query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {query =query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");}

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from lc in query
                               where lc.IsDelete == false
                               select new
                               {
                                   FieldString1 = lc.SalesPersonCode,
                                   FieldString2 = lc.SalesPersonName,
                                   FieldString3 = (lc.Address1 + " " + lc.Address2 + " " + lc.Address3 + " "),
                                   FieldString4 = lc.Telephone,
                                   FieldString5 = lc.ReferenceNo,
                                   FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString7 = lc.SalesPersonType,
                                   FieldString8 = lc.Remark,
                                   FieldString9 = "",
                                   FieldString10 = "",
                                   FieldString11 = "",
                                   FieldString12 = "",
                                   FieldString13 = "",
                                   FieldString14 = ""
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

        public DataTable GetCashierLoyaltysDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Employees.Where("IsDelete=false");

            #region xx
            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            reportConditionsDataStruct.ConditionFrom.Trim(),
                            reportConditionsDataStruct.ConditionTo.Trim());
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() +
                                    ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);


            #endregion 

            //var qr = (from sp in query
            //          join lt in context.InvLoyaltyTransactions on sp.EmployeeID equals lt.CashierID
            //          group new { sp, lt, c } by new
            //          {
            //              sp.EmployeeID,
            //              sp.EmployeeCode,
            //              sp.EmployeeName,
            //              lt.TransID,
            //          } into g
            //          select new
            //          {
            //              g.Key.CustomerID,
            //              g.Key.EmployeeCode,
            //              g.Key.EmployeeName,
            //              ep = (g.Key.TransID == 1 ? g.Sum(t => t.lt.Points) : 0),
            //              rp = (g.Key.TransID == 2 ? g.Sum(t => t.lt.Points) : 0)

            //          });
            //var queryResult = (from a in qr		
            //                   group a by new {a.,a.SalesPersonCode,a.SalesPersonName }
            //                    into g
			
            //                    select new 
            //                    {
            //                        FieldString1=g.Key.SalesPersonCode,
            //                        FieldString2=g.Key.SalesPersonName,
            //                        FieldDecimal6 = g.Sum(a => a.ep),
            //                        FieldDecimal7 = g.Sum(a => a.rp),
            //                    }
			
            //                    );



            return query.ToDataTable();
        }


        public DataTable GetLocationWiseSummaryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvLoyaltyTransactions.AsNoTracking().Where("DiscPer==@0", 0);
 
            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
          
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        case "LocationId":
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
                        case "EmployeeID":
                            query = (from qr in query
                                     join jt in context.Employees.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.CashierID equals jt.EmployeeID
                                     select qr
                                );
                            break;
                        default:
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;

                    }
                }
 
            }




            var qrx = (from lt in query
                      join sp in context.Employees on lt.CashierID equals sp.EmployeeID
                      join l in context.Locations on lt.LocationID equals l.LocationID
                      group new { sp, lt, l } by new
                      {
                          sp.EmployeeID,
                          sp.EmployeeCode,
                          sp.EmployeeName,
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
                          g.Key.EmployeeID,
                          g.Key.EmployeeCode,
                          g.Key.EmployeeName,
                          ep = (g.Key.TransID == 1 ? g.Sum(t => t.lt.Points) : 0),
                          rp = (g.Key.TransID == 2 ? g.Sum(t => t.lt.Points) : 0),
                          g.Key.Receipt
                      });


            var queryResult = (from a in qrx

                               group a by new { a.EmployeeID, a.LocationID, a.EmployeeCode, a.EmployeeName, a.LocationCode, a.LocationName}
                                   into g

                                   select new
                                   {
                                       InvSalesPersonID = g.Key.EmployeeID ,
                                       SalesPersonName = g.Key.EmployeeName,
                                       SalesPersonCode = g.Key.EmployeeCode,
                                       LocationId = g.Key.LocationID,
                                       LocationCode = g.Key.LocationCode,
                                       LocationName = g.Key.LocationName,
                                       ofPointsRedeem = (((g.Sum(a => a.rp)) / (g.Sum(a => a.ep))) * 100),
                                       Pointsdiff = g.Sum(a => a.ep) - g.Sum(a => a.rp),
                                       ep = g.Sum(a => a.ep),
                                       rp = g.Sum(a => a.rp),
                                   }

                                ); //.ToArray();


            var x = (from a in queryResult
                     let Ecount = (
                              from c in context.InvLoyaltyTransactions
                              where
                                  a.InvSalesPersonID == c.CashierID && c.TransID == 1 && a.LocationId ==c.LocationID
                              select c).Count()
                     let Rcount = (
                              from c in context.InvLoyaltyTransactions
                              where
                                  a.InvSalesPersonID == c.CashierID && c.TransID == 2 && a.LocationId == c.LocationID
                              select c).Count()
                     let Totcount = (
                              from c in context.InvLoyaltyTransactions
                              where
                                  a.InvSalesPersonID == c.CashierID && a.LocationId == c.LocationID
                              select c).Count()

                     select new
                     {
                         FieldString1 = a.SalesPersonCode,
                         FieldString2 = a.SalesPersonName,
                         FieldString3 = a.LocationCode,
                         FieldString4 = a.LocationName,
                         FieldDecimal2 = a.ep,
                         FieldDecimal3 = a.rp,
                         FieldDecimal4 = a.Pointsdiff,
                         FieldDecimal5 = Ecount,
                         FieldDecimal6 = Rcount,
                         FieldDecima17 = Totcount,
                        // FieldDecimal8 = a.ofPointsRedeem,
                     }
                     
                 ).ToArray();


            return x.ToDataTable();
        }

        public DataTable GetAllInvSalesPersonsDataTable()
        {
            //DataTable tblSupplier = new DataTable();

            var query = (from lc in context.InvSalesPersons
                         where lc.IsDelete == false
                         select new
                         {
                             FieldString1 = lc.SalesPersonCode,
                             FieldString2 = lc.SalesPersonName,
                             FieldString3 = (lc.Address1 + ", " + lc.Address2 + ", " + lc.Address3 + ". "),
                             FieldString4 = lc.Telephone,
                             FieldString5 = lc.ReferenceNo,
                             FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                             FieldString7 = lc.SalesPersonType,
                             FieldString8 = lc.Remark,
                             FieldString9 = "",
                             FieldString10 = "",
                             FieldString11 = "",
                             FieldString12 = "",
                             FieldString13 = "",
                             FieldString14 = ""
                         });

            return query.AsEnumerable().ToDataTable();
        }

        public DataTable GetAllActiveSalesPersonsDataTable() 
        {
            DataTable tblSalesPersons = new DataTable();
            var query = from d in context.InvSalesPersons
                        where d.IsDelete == false
                        select new
                        {
                            d.SalesPersonCode,
                            d.SalesPersonName,
                            d.SalesPersonType,
                            d.ReferenceNo,
                            d.Telephone,
                            d.Mobile
                        };
            return query.ToDataTable();
        }

 
        // Get selection data for Report Generator Combo boxes
  
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseSummary");


            IQueryable qryResult = context.Employees;
                         

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LocationId":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                        .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + ""); ;
                    break;
                case "EmployeeID":
                    qryResult = qryResult.Join(context.Employees, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    
                    break;
  
                default:
                    //qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());    
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
            else if (reportDataStruct.ValueDataType.Equals(typeof(int)))
            {
                if (reportDataStruct.ReportFieldName.Trim() == "Document Status")
                {
                    selectionDataList.Add("Partial");
                    selectionDataList.Add("Opened");
                    selectionDataList.Add("Expired");
                }
            }
            else
            {
                foreach (var item in qryResult)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString().Trim()); }
                }
            }
            return selectionDataList;
        }


        public bool IsExistsSalesPerson(string salesPersonCode) 
        {
            if (GetInvSalesPersonByCode(salesPersonCode) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
        #endregion

    }
}
