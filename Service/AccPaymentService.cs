using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Data;
using Domain;
using Utility;

namespace Service
{
    //By Pravin
    public class AccPaymentService
    {
        //ERPDbContext context = new ERPDbContext();

        /// <summary>
        ///  PaymentTypes - PayTypeID in PaymentDet table
        /// </summary>
        public enum BillTypes
        {
            Sales = 1,
            LoyaltyPointRedeem = 2,
            PaidOut = 3,
            ArawanaRedeem = 4,
            Advance = 5,
            PaidIn = 6
        }

        public enum SaleTypes
        {
            Sales = 1,
            GiftVoucherSales = 2
        }

        public enum CustomerTypes
        {
            Normal = 1,
            Staff = 2, 
            Loyalty = 3
        }

        #region Method
        public AccPaymentHeader getAccPaymentHeader(long referenceDocumentID, int referenceDocumentDocumentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext()) 
            {return context.AccPaymentHeaders.Where(ph => ph.ReferenceDocumentDocumentID.Equals(referenceDocumentDocumentID) && ph.ReferenceDocumentID.Equals(referenceDocumentID) && ph.LocationID.Equals(locationID)).FirstOrDefault();}
        }

        public DataTable GetReceiptsRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.PaymentDets.Where("Status = @0", 1);

                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (string)))
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " +
                                reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                reportConditionsDataStruct.ConditionFrom.Trim(),
                                reportConditionsDataStruct.ConditionTo.Trim());
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (DateTime)))
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " +
                                reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1));
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (decimal)))
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " +
                                reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (long)))
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "PaymentID":
                                query = (from qr in query
                                            join jt in context.PayTypes.Where(
                                                "" +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " >= " + "@0 AND " +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " <= @1",
                                                (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                (reportConditionsDataStruct.ConditionTo.Trim())
                                                ) on qr.PayTypeID equals jt.PaymentID
                                            select qr
                                        );

                                break;
                            case "BankPosID":
                                query = (from qr in query
                                            join jt in context.Bankspos.Where(
                                                "" +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " >= " + "@0 AND " +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " <= @1",
                                                (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                (reportConditionsDataStruct.ConditionTo.Trim())
                                                ) on qr.BankId equals jt.BankID
                                            select qr
                                        );

                                break;
                            case "CashierID":
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
                            case "UpdatedBy":
                                query = (from qr in query
                                            join jt in context.Employees.Where(
                                                "" +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " >= " + "@0 AND " +
                                                reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                " <= @1",
                                                (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                (reportConditionsDataStruct.ConditionTo.Trim())
                                                ) on qr.UpdatedBy equals jt.EmployeeID
                                            select qr
                                        );

                                break;
                            default:
                                query =
                                        query.Where(
                                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " +
                                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                            long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                            long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                        
                        }
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (bool)))
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " +
                                reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1",
                                reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false,
                                reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false);
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
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
                            case "PaymentID":
                                query = (from qr in query
                                         join jt in context.PayTypes.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.PayTypeID equals jt.PaymentID
                                         select qr
                                        );

                                break;
                            case "BankPosID":
                                query = (from qr in query
                                         join jt in context.Bankspos.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.BankId equals jt.BankID
                                         select qr
                                        );

                                break;
                            case "SaleTypeID":
                                int selectedFromSaleTypeStatus = 0, selectedToSaleTypeStatus = 0;

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                                { selectedFromSaleTypeStatus = (int)SaleTypes.Sales; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.GiftVoucherSales.ToString()))
                                { selectedFromSaleTypeStatus = (int)SaleTypes.GiftVoucherSales; }

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.Sales.ToString()))
                                { selectedToSaleTypeStatus = (int)SaleTypes.Sales; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(SaleTypes.GiftVoucherSales.ToString()))
                                { selectedToSaleTypeStatus = (int)SaleTypes.GiftVoucherSales; }

                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromSaleTypeStatus), (selectedToSaleTypeStatus));

                                break;
                            case "BillTypeID":
                                int selectedFromBillTypeStatus = 0, selectedToBillTypeStatus = 0;

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(BillTypes.Sales.ToString()))
                                { selectedFromBillTypeStatus = (int)BillTypes.Sales; }

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(BillTypes.Sales.ToString()))
                                { selectedToBillTypeStatus = (int)BillTypes.Sales; }

                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromBillTypeStatus), (selectedToBillTypeStatus));

                                break;
                            case "CustomerType":
                                int selectedFromCustomerTypeStatus = 0, selectedToCustomerTypeStatus = 0;

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
                                { selectedFromCustomerTypeStatus = (int)CustomerTypes.Normal; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
                                { selectedFromCustomerTypeStatus = (int)CustomerTypes.Staff; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
                                { selectedFromCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Normal.ToString()))
                                { selectedToCustomerTypeStatus = (int)CustomerTypes.Normal; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Staff.ToString()))
                                { selectedToCustomerTypeStatus = (int)CustomerTypes.Staff; }
                                if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(CustomerTypes.Loyalty.ToString()))
                                { selectedToCustomerTypeStatus = (int)CustomerTypes.Loyalty; }

                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromCustomerTypeStatus), (selectedToCustomerTypeStatus));

                                break;
                            default:
                                break;
                        }

                    }
                }

                #region dynamic select

                //DataTable dt1 = new DataTable();
                //StringBuilder selectFields = new StringBuilder();
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{ 
                //    selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
                //    dt1.Columns.Add(reportDataStruct.DbColumnName.Trim());
                //}

                //// Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                //var selectedqry2 = query.Select("(new " + selectFields.ToString() + ")");

                //foreach (var item in selectedqry)
                //{
                //    string[] columnList = item.ToString().Split(',');
                //    dt1.Rows.Add(columnList);
                //}

                #endregion

                //var queryResult = query.AsEnumerable();
                //return queryResult.Select(ph =>
                //                   new
                //                   {
                //                       FieldString1 = reportDataStructList[0].IsSelectionField == true ? ph.LocationCode + " " + ph.LocationName : "",
                //                       //FieldString2 = reportDataStructList[1].IsSelectionField == true ? ph.LocationName : "",
                //                       FieldString2 = reportDataStructList[1].IsSelectionField == true ? ph.DepartmentCode + " " + ph.DepartmentName : "",
                //                       FieldString3 = reportDataStructList[2].IsSelectionField == true ? ph.CategoryCode + " " + ph.CategoryName : "",
                //                       FieldString4 = reportDataStructList[3].IsSelectionField == true ? ph.SubCategoryCode + " " + ph.SubCategoryName : "",
                //                       FieldString5 = reportDataStructList[4].IsSelectionField == true ? ph.SubCategory2Code + " " + ph.SubCategory2Name : "",
                //                       FieldString6 = reportDataStructList[5].IsSelectionField == true ? (ph.DocumentID == 1 ? "SL" : (ph.DocumentID == 2 ? "RT" : (ph.DocumentID == 3 ? "DSL" : (ph.DocumentID == 4 ? "DRT" : "")))) : "", 
                //                       // SL-Sales, RT-Sales Returns, DSL-Department Sales, DRT-Department Returns
                //                       FieldString7 = reportDataStructList[6].IsSelectionField == true ? ph.ProductCode : "",
                //                       FieldString8 = reportDataStructList[7].IsSelectionField == true ? ph.ProductName : "",
                //                       FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.DocumentNo : "",
                //                       FieldString10 = reportDataStructList[9].IsSelectionField == true ? ph.DocumentDate.ToShortDateString() : "",
                //                       //FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.TerminalNo : "",
                //                       FieldDecimal1 = reportDataStructList[10].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.Qty : (ph.DocumentID == 2 ? -(ph.Qty) : (ph.DocumentID == 3 ? ph.Qty : (ph.DocumentID == 4 ? -(ph.Qty) : 0)))) : 0,
                //                       FieldDecimal2 = reportDataStructList[11].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.GrossAmount : (ph.DocumentID == 2 ? -(ph.GrossAmount) : (ph.DocumentID == 3 ? ph.GrossAmount : (ph.DocumentID == 4 ? -(ph.GrossAmount) : 0)))) : 0,
                //                       FieldDecimal3 = reportDataStructList[12].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.DiscountAmount : (ph.DocumentID == 2 ? -(ph.DiscountAmount) : (ph.DocumentID == 3 ? ph.DiscountAmount : (ph.DocumentID == 4 ? -(ph.DiscountAmount) : 0)))) : 0,
                //                       FieldDecimal4 = reportDataStructList[13].IsSelectionField == true ? (ph.DocumentID == 1 ? ph.NetAmount : (ph.DocumentID == 2 ? -(ph.NetAmount) : (ph.DocumentID == 3 ? ph.NetAmount : (ph.DocumentID == 4 ? -(ph.NetAmount) : 0)))) : 0,
                //                       //FieldDecimal5 = reportDataStructList[14].IsSelectionField == true ? ph.SellingPrice : 0
                //                       //FieldDecimal6 = reportDataStructList[15].IsSelectionField == true ? ph.CostPrice : 0
                //                   }
                //                       ).ToArray().ToDataTable();

                var queryResult = (from qs in query.AsNoTracking()
                                   //join lc in context.LoyaltyCustomers on qs.CustomerId equals lc.LoyaltyCustomerID
                                   join lt in context.Locations on qs.LocationID equals lt.LocationID
                                   join pt in context.PayTypes on qs.PayTypeID equals pt.PaymentID
                                   join bt in context.Bankspos on new { BankId = qs.BankId } equals new { BankId = bt.BankID } into bt_join
                                   from bt in bt_join.DefaultIfEmpty()
                                   //join bt in context.Bankspos on qs.BankId equals bt.BankID
                                   join cs in context.Employees on qs.CashierID equals cs.EmployeeID
                                   join us in context.Employees on qs.UpdatedBy equals us.EmployeeID
                                   where qs.BillTypeID == 1
                                   select
                                       new
                                           {
                                               FieldString1 = lt.LocationCode + " " + lt.LocationName,
                                               FieldString2 = (qs.SaleTypeID == 1 ? "Sale" : (qs.SaleTypeID == 2 ? "GV Sale" : "")),
                                               FieldString3 = (qs.BillTypeID == 1 ? "Sale" : ""),
                                               FieldString4 = DbFunctions.TruncateTime(qs.SDate),
                                               FieldString5 = qs.Receipt,
                                               FieldString6 = pt.PrintDescrip,
                                               //FieldString7 = (qs.CustomerType == 1 ? CustomerTypes.Normal.ToString() : (qs.CustomerType == 2 ? CustomerTypes.Staff.ToString() : (qs.CustomerType == 3 ? CustomerTypes.Loyalty.ToString() : ""))),
                                               FieldString7 = (qs.CustomerType == 1 ? "Normal" : (qs.CustomerType == 2 ? "Staff" : (qs.CustomerType == 3 ? "Loyalty" : ""))),
                                               FieldString8 = // qs.CustomerCode + " " + qs.EnCodeName, // finalize if can view correct name
                                               (qs.CustomerType == 2 ? (from em in context.Employees
                                                                        where em.EmployeeID == qs.CustomerId
                                                                        select em.EmployeeCode + " " + em.EmployeeName).FirstOrDefault() : (qs.CustomerType == 3 ? (from lc in context.LoyaltyCustomers
                                                                                                                                                   where lc.LoyaltyCustomerID == qs.CustomerId
                                                                                                                                                   select lc.CustomerCode + " " + lc.CustomerName).FirstOrDefault() : "")),
                                                                                                                                            
                                               FieldString9 = bt.Bank,
                                               FieldString10 = DbFunctions.TruncateTime(qs.ChequeDate),
                                               FieldString11 = qs.RefNo,
                                               FieldString12 = us.EmployeeName, //"",
                                               FieldString13 = cs.EmployeeName,
                                               //FieldString14 = us.EmployeeName,
                                               FieldDecimal1 = (qs.PayTypeID != 1 ? qs.Amount : (qs.Balance < qs.Amount ? qs.Balance : qs.Amount))
                                           }); //.ToArray();

                DataTable dtQueryResult = new DataTable();

                if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy))
                {
                    dtQueryResult = queryResult.ToDataTable();

                    foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                    {
                        if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                        {
                            if (!reportDataStruct.IsSelectionField)
                            {
                                dtQueryResult.Columns.Remove(reportDataStruct.ReportField);
                            }
                        }
                    }
                }
                else
                {
                    StringBuilder sbGroupByColumns = new StringBuilder();
                    StringBuilder sbOrderByColumns = new StringBuilder();
                    StringBuilder sbSelectColumns = new StringBuilder();

                    sbGroupByColumns.Append("new(");
                    sbSelectColumns.Append("new(");

                    foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                    {
                        sbGroupByColumns.Append("it." + item.ReportField.ToString() + " , ");
                        sbSelectColumns.Append("Key." + item.ReportField.ToString() + " as " + item.ReportField + " , ");

                        sbOrderByColumns.Append("Key." + item.ReportField + " , ");
                    }

                    foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal))))
                    {
                        sbSelectColumns.Append("SUM(" + item.ReportField + ") as " + item.ReportField + " , ");
                    }

                    sbGroupByColumns.Remove(sbGroupByColumns.Length - 2, 2); // remove last ','
                    sbGroupByColumns.Append(")");

                    sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                    sbSelectColumns.Remove(sbSelectColumns.Length - 2, 2); // remove last ','
                    sbSelectColumns.Append(")");

                    var groupByResult = queryResult.GroupBy(sbGroupByColumns.ToString(), "it")
                                        .OrderBy(sbOrderByColumns.ToString())
                                        .Select(sbSelectColumns.ToString());

                    dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString());

                    dtQueryResult.Columns.Remove("C1"); // Remove the first 'C1' column
                    int colIndex = 0;
                    foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                    {
                        dtQueryResult.Columns[colIndex].ColumnName = item.ReportField;
                        colIndex++;
                    }

                    foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal))))
                    {
                        dtQueryResult.Columns[colIndex].ColumnName = item.ReportField;
                        colIndex++;
                    }

                    foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                    {
                        if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                        {
                            if (!reportDataStruct.IsSelectionField)
                            {
                                dtQueryResult.Columns.Remove(reportDataStruct.ReportField);
                            }
                        }
                    }
                }

                return dtQueryResult; // queryResult.ToDataTable();
            }
        }

        public ArrayList GetSelectionReceiptsDataSummary(Common.ReportDataStruct reportDataStruct)
        {
            //Note : include table for backoffice payment detail 
            using (ERPDbContext context = new ERPDbContext())
            {
                IQueryable qryResult = context.PaymentDets.Where("Status == @0", 1);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "LocationID":
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), 
                                                   reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "PaymentID":
                        qryResult = qryResult.Join(context.PayTypes, "PayTypeID", reportDataStruct.DbColumnName.Trim(), 
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");

                        break;
                    case "BankPosID":
                        qryResult = qryResult.Join(context.Bankspos, "BankID", "BankID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "CashierID":
                        qryResult = qryResult.Join(context.Employees, "CashierID", "EmployeeID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "UpdatedBy":
                        qryResult = qryResult.Join(context.Employees, "UpdatedBy", "EmployeeID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LoyaltyCustomerID":
                        qryResult = qryResult.Join(context.LoyaltyCustomers, "CustomerID", "LoyaltyCustomerID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "EnCodeName":
                        qryResult = qryResult.Where("CustomerType == @0 OR CustomerType == @1", 2, 3)
                                             .GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbColumnName.Trim() + "")
                                             .Select("key." + reportDataStruct.DbColumnName.Trim() + "");
                        break;
                    default:
                        qryResult =
                            qryResult.GroupBy("new(" + reportDataStruct.DbColumnName + ")",
                                              "new(" + reportDataStruct.DbColumnName + ")")
                                     .OrderBy("key." + reportDataStruct.DbColumnName + "")
                                     .Select("key." + reportDataStruct.DbColumnName + "");
                        break;
                }
                
                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();

                if (reportDataStruct.ValueDataType.Equals(typeof (bool)))
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
                    if (reportDataStruct.DbColumnName.Trim() == "SaleTypeID")
                    {
                        foreach (var item in qryResult)
                        {
                            if (item.Equals((int)SaleTypes.Sales))
                            { selectionDataList.Add(SaleTypes.Sales.ToString()); }
                            else if (item.Equals((int)SaleTypes.GiftVoucherSales))
                            { selectionDataList.Add(SaleTypes.GiftVoucherSales.ToString()); }
                        }
                    }
                    else if (reportDataStruct.DbColumnName.Trim() == "BillTypeID")
                    {
                        foreach (var item in qryResult)
                        {
                            if (item.Equals((int)BillTypes.Sales))
                            { selectionDataList.Add(BillTypes.Sales.ToString()); }
                        }
                    }
                    else if (reportDataStruct.DbColumnName.Trim() == "CustomerType")
                    {
                        foreach (var item in qryResult)
                        {
                            if (item.Equals((int)CustomerTypes.Normal))
                            { selectionDataList.Add(CustomerTypes.Normal.ToString()); }
                            else if (item.Equals((int)CustomerTypes.Staff))
                            { selectionDataList.Add(CustomerTypes.Staff.ToString()); }
                            else if (item.Equals((int)CustomerTypes.Loyalty))
                            { selectionDataList.Add(CustomerTypes.Loyalty.ToString()); }
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
        }
        #endregion
    }
}
