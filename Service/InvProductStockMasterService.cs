using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity.Validation;
using Utility;
using System.Data.Entity;
using MoreLinq;
using System.Collections;
 

namespace Service
{
    /// <summary>
    /// By - Pravin
    /// Product Stock Details
    /// </summary>
    public class InvProductStockMasterService
    {
        ERPDbContext context = new ERPDbContext();
       // InvProductStockMasterService invProductStockMasterService = new InvProductStockMasterService();

        

        #region methods


        /// Save new ProductUnitConversionService

        public void AddProductStockMaster(InvProductStockMaster invProductStockMaster)
        {

            context.InvProductStockMasters.Add(invProductStockMaster);
            context.SaveChanges();
        }

       

        /// Update existing ProductUnitConversionService

        public void UpdateProductStockMaster(InvProductStockMaster invProductStockMaster)
        {
            this.context.Entry(invProductStockMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductUnitConversionService by code

        public InvProductStockMaster GetProductStockMasterByProductID(long productID)
        {

            return context.InvProductStockMasters.Where(p => p.ProductID == productID && p.IsDelete == false ).FirstOrDefault();

        }

        public InvProductStockMaster GetProductStockMasterWithDeletedByProductID(long productID, long locationID)
        {

            return context.InvProductStockMasters.Where(p => p.ProductID == productID && p.LocationID==locationID ).FirstOrDefault();

        }

        public List<InvProductStockMaster> GetAllProductStockMasterByProductID(long productID)
        {
            
            return context.InvProductStockMasters.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();


        }


        //////public void GetAllProductUnitConversionByProductCode(long productID)
        //////{


        //////    var b = (from u in context.InvProductUnitConversions
        //////   from um in context.UnitOfMeasures
        //////              where u.InvProductUnitConversionID == um.UnitOfMeasureID && u.ProductID == productID
        //////    select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

        //////   // return b;

        //////}

        public DataTable GetLocationInfo(long productID)
        {

            DataTable dt = new DataTable();

            //var query = (from l in context.Locations
            //             join ps in context.InvProductStockMasters on
            //             l.LocationID equals ps.LocationID into psm
            //             from psn in psm.DefaultIfEmpty()
            //             where psn.ProductID == productID
            //             select new { l.LocationName, psn.ReOrderLevel, psn.ReOrderQuantity, psn.ReOrderPeriod, psn.CostPrice, psn.SellingPrice, l.LocationID });


            var query = (from l in context.Locations
                         join ps in context.InvProductStockMasters on
                         l.LocationID equals ps.LocationID into psm
                         from psn in psm.DefaultIfEmpty()
                         where psn.ProductID == productID || psn.ProductID == null
                         select new
                         {
                             l.LocationName,
                             ReOrderLevel = (psn.ReOrderLevel == null ? 0 : psn.ReOrderLevel),
                             ReOrderQuantity = (psn.ReOrderQuantity == null ? 0 : psn.ReOrderQuantity),
                             ReOrderPeriod = (psn.ReOrderPeriod == null ? 0 : psn.ReOrderPeriod),
                             CostPrice = (psn.CostPrice == null ? 0 : psn.CostPrice),
                             MinimumPrice = (psn.MinimumPrice == null ? 0 : psn.MinimumPrice),
                             SellingPrice = (psn.SellingPrice == null ? 0 : psn.SellingPrice),
                             IsDelete = (psn.IsDelete == false ? true :false),
                             l.LocationID
                         }).OrderBy(p=>p.LocationID);
            //return dt = Common.LINQToDataTable(query);
            return query.ToDataTable();

           ///// return dt;
           
        }

        //public DataTable GetInfo(long productID)
        //{
        //    return (from pm in context.InvProductMasters
        //            join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
        //            join l in context.Locations on psm.LocationID equals l.LocationID
        //            where pm.InvProductMasterID == productID
        //            select new
        //            {
        //                l.LocationName,
        //                pm.CostPrice,
        //                pm.SellingPrice,
        //                pm.WholesalePrice,
        //                pm.MinimumPrice,
        //                psm.Stock,
        //                CurrentGp = ((pm.SellingPrice - pm.CostPrice) / (pm.CostPrice == 0 ? 1 : pm.CostPrice)) * 100
        //            }).ToDataTable();

        //}

        public List<ProductLocationInfoTemp> GetInfo(long productID) 
        {
            List<ProductLocationInfoTemp> productLocationInfoTempList = new List<ProductLocationInfoTemp>();

            var info = (from pm in context.InvProductMasters
                        join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
                        join l in context.Locations on psm.LocationID equals l.LocationID
                        where pm.InvProductMasterID == productID
                        select new
                        {
                            l.LocationID,
                            l.LocationName,
                            pm.CostPrice,
                            pm.SellingPrice,
                            pm.WholesalePrice,
                            pm.MinimumPrice,
                            psm.Stock,
                            CurrentGp = ((pm.SellingPrice - pm.CostPrice) / (pm.CostPrice == 0 ? 1 : pm.CostPrice)) * 100
                        }).ToArray().OrderBy(l => l.LocationID);

            foreach (var temp in info)
            {
                ProductLocationInfoTemp productLocationInfoTemp = new ProductLocationInfoTemp();

                productLocationInfoTemp.LocationName = temp.LocationName;
                productLocationInfoTemp.CostPrice = temp.CostPrice;
                productLocationInfoTemp.SellingPrice = temp.SellingPrice;
                productLocationInfoTemp.WholesalePrice = temp.WholesalePrice;
                productLocationInfoTemp.MinimumPrice = temp.MinimumPrice;
                productLocationInfoTemp.Stock = temp.Stock;
                productLocationInfoTemp.CurrentGp = temp.CurrentGp;

                productLocationInfoTempList.Add(productLocationInfoTemp);
            }

            return productLocationInfoTempList;
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                //AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
                //autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
                //int documentID = autoGeneratePurchaseInfo.DocumentID;
                IQueryable qryResult = context.InvProductMasters.Where("IsDelete == @0", false);

                switch (reportDataStruct.DbColumnName.Trim())
                {

                    //case "InvProductMasterID":

                    //    qryResult = qryResult.OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                    //                     .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                    //                     .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    //    break;
                        
                    case "InvDepartmentID":
                        qryResult = qryResult.Join(context.InvDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvCategoryID":
                        qryResult = qryResult.Join(context.InvCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvSubCategoryID":
                        qryResult = qryResult.Join(context.InvSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvSubCategory2ID":
                        qryResult = qryResult.Join(context.InvSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    //case "UnitOfMeasureID":
                    //    qryResult = qryResult.Join(context.UnitOfMeasures, reportDataStruct.DbColumnName.Trim(),
                    //                               reportDataStruct.DbColumnName.Trim(), "new(inner.UnitOfMeasureName)")
                    //                         .OrderBy("UnitOfMeasureName")
                    //                         .GroupBy("new(UnitOfMeasureName)", "new(UnitOfMeasureName)")
                    //                         .Select("Key.UnitOfMeasureName");
                    //    break;
                    case "LocationID":
                        qryResult = context.Locations.Where("IsDelete == @0", false)
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "SupplierID":
                        qryResult = context.Suppliers.Where("IsDelete == @0", false)
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    default:
                        qryResult = qryResult.OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             //.GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select(reportDataStruct.DbJoinColumnName.Trim());
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
            }
        }

        public new DataTable GetStockBalancesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();

            var query = (from pm in context.InvProductMasters
                         join ps in context.InvProductStockMasters on pm.InvProductMasterID equals ps.ProductID
                         where pm.IsDelete == false
                         select new {ps.CostPrice, ps.LocationID,pm.SupplierID,ps.ProductID,ps.MinimumPrice,ps.SellingPrice,ps.Stock}).AsNoTracking();

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
                        case "SupplierID":
                            query = (from qr in query
                                     join jt in context.Suppliers.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.SupplierID equals jt.SupplierID
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

            //var queryResult = query.AsEnumerable();
            
            var result = (from pd in query
                        join l in context.Locations on pd.LocationID equals l.LocationID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                        join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                        join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                        join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
                        join sup in context.Suppliers on pm.SupplierID equals sup.SupplierID
                        select
                            new
                            {
                                //FieldString1 = reportDataStructList[0].IsSelectionField == true ? sup.SupplierCode + " " + sup.SupplierName : "", 
                                //FieldString2 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                                //FieldString3 = reportDataStructList[1].IsSelectionField == true ? dp.DepartmentCode + " " + dp.DepartmentName : "",
                                //FieldString4 = reportDataStructList[2].IsSelectionField == true ? ct.CategoryCode + " " + ct.CategoryName : "",
                                //FieldString5 = reportDataStructList[3].IsSelectionField == true ? sct.SubCategoryCode + " " + sct.SubCategoryName : "",
                                //FieldString6 = reportDataStructList[4].IsSelectionField == true ? sct2.SubCategory2Code + " " + sct2.SubCategory2Name : "",
                                //FieldString7 = reportDataStructList[5].IsSelectionField == true ? pm.ProductCode : "",
                                //FieldString8 = reportDataStructList[6].IsSelectionField == true ? pm.ProductName : "",
                                //FieldDecimal1 = reportDataStructList[7].IsSelectionField == true ? pd.Stock : 0,
                                //FieldDecimal2 = reportDataStructList[8].IsSelectionField == true ? pm.SellingPrice : 0,
                                //FieldDecimal3 = reportDataStructList[9].IsSelectionField == true ? pd.CostPrice : 0,
                                //FieldDecimal4 = reportDataStructList[10].IsSelectionField == true ? (pd.Stock * pm.SellingPrice) : 0,
                                //FieldDecimal5 = reportDataStructList[11].IsSelectionField == true ? (pd.Stock * pm.CostPrice) : 0

                                FieldString1 = sup.SupplierCode + " " + sup.SupplierName, 
                                FieldString2 = l.LocationCode + " " + l.LocationName,
                                FieldString3 = dp.DepartmentCode + " " + dp.DepartmentName,
                                FieldString4 = ct.CategoryCode + " " + ct.CategoryName,
                                FieldString5 = sct.SubCategoryCode + " " + sct.SubCategoryName,
                                FieldString6 = sct2.SubCategory2Code + " " + sct2.SubCategory2Name,
                                FieldString7 = pm.ProductCode,
                                FieldString8 = pm.ProductName,
                                FieldDecimal1 = pd.Stock,
                                FieldDecimal2 = pm.SellingPrice,
                                FieldDecimal3 = pd.CostPrice,
                                FieldDecimal4 = (pd.Stock * pm.SellingPrice),
                                FieldDecimal5 = (pd.Stock * pm.CostPrice)

                            });

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy)) // if no any grops
            {
                dtQueryResult = result.ToDataTable(); // Convert query to data table with all columns

                foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                {
                    if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                    {
                        if (!reportDataStruct.IsSelectionField)
                        {
                            dtQueryResult.Columns.Remove(reportDataStruct.ReportField); // remove not selected columns 
                        }
                    }
                }
            }
            else
            {

                StringBuilder sbGroupByColumns = new StringBuilder(); // Group by columns
                StringBuilder sbOrderByColumns = new StringBuilder(); // Order by Columns
                StringBuilder sbSelectColumns = new StringBuilder(); // Selected Columns

                sbGroupByColumns.Append("new(");
                sbSelectColumns.Append("new(");

                foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                {
                    sbGroupByColumns.Append("it." + item.ReportField + " , ");
                    sbSelectColumns.Append("Key." + item.ReportField + " as " + item.ReportField + " , ");

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

                var groupByResult = result.GroupBy(sbGroupByColumns.ToString(), "it")
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

            return dtQueryResult;
        }
        
        #endregion
    }
}
