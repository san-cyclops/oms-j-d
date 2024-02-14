using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class InvBatchNoExpiaryDetailService
    {
        ERPDbContext context = new ERPDbContext();
        InvProductMaster invProductMaster = new InvProductMaster();


        public InvProductBatchNoExpiaryDetail GetBatchNoExpiaryDetailByBarcode(long barcode)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(bd => bd.BarCode.Equals(barcode) && bd.BalanceQty > 0 && bd.IsDelete == false).FirstOrDefault();
        }

        public InvProductBatchNoExpiaryDetail GetBatchNoExpiaryDetailByBarcode(long barcode, InvProductMaster product, int locationID)  
        {
            InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetailRtn = new InvProductBatchNoExpiaryDetail();

            invProductBatchNoExpiaryDetailRtn = context.InvProductBatchNoExpiaryDetails.Where(bd => bd.BarCode.Equals(barcode) && bd.BalanceQty > 0 && bd.IsDelete == false).FirstOrDefault();

            if (invProductBatchNoExpiaryDetailRtn == null) 
            {
                //if (product != null)
                //{
                //    invProductBatchNoExpiaryDetailRtn = context.InvProductBatchNoExpiaryDetails.Where(bd => bd.ProductID.Equals(product.InvProductMasterID) && bd.LocationID.Equals(locationID) && bd.BalanceQty > 0 && bd.IsDelete == false).FirstOrDefault();
                //}
            }

            return invProductBatchNoExpiaryDetailRtn;
        }

        public InvProductBatchNoExpiaryDetail GetBatchNoExpiaryDetailByBarcodeForProductMaster(long barcode) 
        {
            return context.InvProductBatchNoExpiaryDetails.Where(bd => bd.BarCode.Equals(barcode) && bd.IsDelete == false).FirstOrDefault();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            //using (ERPDbContext context = new ERPDbContext())
            //{
                IQueryable qryResult = context.InvProductBatchNoExpiaryDetails.AsNoTracking().Where("IsDelete == @0", false);
                
                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "SupplierID":
                        qryResult = qryResult.Join(context.InvPurchaseHeaders, "BatchNo", "BatchNo",
                                                "new(inner.SupplierID)")
                                             .Join(context.Suppliers, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "InvProductMasterID":

                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(),
                                                    "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");

                        break;

                    case "InvDepartmentID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.DepartmentID)").
                                            Join(context.InvDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvCategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.CategoryID)")
                                            .Join(context.InvCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvSubCategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.SubCategoryID)").
                                            Join(context.InvSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvSubCategory2ID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.SubCategory2ID)").
                                            Join(context.InvSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                   
                    case "LocationID":
                        qryResult = context.Locations.Where("IsDelete == @0", false)
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    default:
                       
                        qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName + ")", "new(" + reportDataStruct.DbColumnName + ")").OrderBy("key." + reportDataStruct.DbColumnName + "").Select("key." + reportDataStruct.DbColumnName + "");
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
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
                }
                return selectionDataList;
            //}
        }

        public DataTable GetBatchStockBalancesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();

            var query = (from pb in context.InvProductBatchNoExpiaryDetails
                         where pb.IsDelete == false
                         select pb).AsNoTracking();

            query = query.AsNoTracking();
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); } // Add 1 day and reduce 1 second from 'To date' to get Date range results

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
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
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "SupplierID":
                                query = (from qr in query
                                         join ph in context.InvPurchaseHeaders on qr.BatchNo equals ph.BatchNo
                                         join s in context.Suppliers.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on ph.SupplierID equals s.SupplierID
                                         select qr
                                    );

                               break;

                            case "InvProductMasterID":
                                query = (from qr in query
                                         join jt in context.InvProductMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ProductID equals jt.InvProductMasterID
                                         select qr
                                        );
                                break;

                            case "InvDepartmentID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.DepartmentID equals jt.InvDepartmentID
                                         select qr
                                        );
                                break;

                            case "InvCategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.CategoryID equals jt.InvCategoryID
                                         select qr
                                        );
                                break;

                            case "InvSubCategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategoryID equals jt.InvSubCategoryID
                                         select qr
                                        );
                                break;

                            case "InvSubCategory2ID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategory2ID equals jt.InvSubCategory2ID
                                         select qr
                                        );
                                break;


                            default:
                                query = query
                                    .Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }
                }
            }

            var queryResult = query.AsEnumerable();

            return 
             (from b in queryResult
                    //join ph in context.InvPurchaseHeaders on b.BatchNo equals ph.BatchNo into t
                    //from ph2 in t.DefaultIfEmpty()
                    //join s in context.Suppliers on ph2.SupplierID equals s.SupplierID into t2
                    //from s2 in t2.DefaultIfEmpty()
                    join l in context.Locations on b.LocationID equals l.LocationID
                    join pm in context.InvProductMasters on b.ProductID equals pm.InvProductMasterID
                    join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                    join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                    join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                    join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
                    //where b.LocationID == ph.LocationID
                    select
                        new
                        {
                            //FieldString1 = reportDataStructList[0].IsSelectionField == true ? s.SupplierCode + " " + s.SupplierName : "",
                            FieldString1 = reportDataStructList[0].IsSelectionField == true ? b.BatchNo : "",
                            FieldString2 = reportDataStructList[1].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                            FieldString3 = reportDataStructList[2].IsSelectionField == true ? dp.DepartmentCode + " " + dp.DepartmentName : "",
                            FieldString4 = reportDataStructList[3].IsSelectionField == true ? ct.CategoryCode + " " + ct.CategoryName : "",
                            FieldString5 = reportDataStructList[4].IsSelectionField == true ? sct.SubCategoryCode + " " + sct.SubCategoryName : "",
                            FieldString6 = reportDataStructList[5].IsSelectionField == true ? sct2.SubCategory2Code + " " + sct2.SubCategory2Name : "",
                            FieldString7 = reportDataStructList[6].IsSelectionField == true ? pm.ProductCode : "",
                            FieldString8 = reportDataStructList[7].IsSelectionField == true ? pm.ProductName : "",
                            //FieldDecimal1 = reportDataStructList[9].IsSelectionField == true ? pd.Qty: 0,
                            FieldDecimal1 = reportDataStructList[8].IsSelectionField == true ? b.BalanceQty : 0
                            //FieldDecimal3 = reportDataStructList[8].IsSelectionField == true ? pd.SellingPrice : 0,
                            //FieldDecimal4 = reportDataStructList[8].IsSelectionField == true ? pd.CostPrice : 0,
                            //FieldDecimal5 = reportDataStructList[10].IsSelectionField == true ? (pd.Qty * pd.SellingPrice) : 0,
                            //FieldDecimal6 = reportDataStructList[11].IsSelectionField == true ? (pd.Stock * pm.CostPrice) : 0
                        }).ToDataTable();

            //DataTable dttt = x.ToDataTable();
            //DataTable dtt = Common.LINQToDataTable(x);//.ToDataTable();

            
            //return null;
        }
    }
}

