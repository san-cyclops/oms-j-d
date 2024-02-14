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
    public class LgsProductStockMasterService
    {
        ERPDbContext context = new ERPDbContext();
       // LgsProductStockMasterService LgsProductStockMasterService = new LgsProductStockMasterService();

        #region methods


        /// Save new ProductUnitConversionService

        public void AddProductStockMaster(LgsProductStockMaster lgsProductStockMaster)
        {
            context.LgsProductStockMasters.Add(lgsProductStockMaster);
            context.SaveChanges();
        }

       

        /// Update existing ProductUnitConversionService

        public void UpdateProductStockMaster(LgsProductStockMaster lgsProductStockMaster)
        {
            this.context.Entry(lgsProductStockMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductUnitConversionService by code

        public LgsProductStockMaster GetProductStockMasterByProductID(long productID)
        {
            return context.LgsProductStockMasters.Where(p => p.ProductID == productID && p.IsDelete == false ).FirstOrDefault();
        }

        public LgsProductStockMaster GetProductStockMasterWithDeletedByProductID(long productID, long locationID)
        {
            return context.LgsProductStockMasters.Where(p => p.ProductID == productID && p.LocationID==locationID ).FirstOrDefault();
        }

        public List<LgsProductStockMaster> GetAllProductStockMasterByProductID(long productID)
        {
            return context.LgsProductStockMasters.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();
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
                         join ps in context.LgsProductStockMasters on
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
                             IsDelete = (psn.IsDelete == false ? true : false),
                             l.LocationID
                         }).OrderBy(p => p.LocationID);
            
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

            var info = (from pm in context.LgsProductMasters
                        join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                        join l in context.Locations on psm.LocationID equals l.LocationID
                        where pm.LgsProductMasterID == productID
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
                IQueryable qryResult = context.LgsProductMasters.Where("IsDelete == @0", false);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "LgsDepartmentID":
                        qryResult = qryResult.Join(context.LgsDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsCategoryID":
                        qryResult = qryResult.Join(context.LgsCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsSubCategoryID":
                        qryResult = qryResult.Join(context.LgsSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy(reportDataStruct.DbJoinColumnName.Trim())
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsSubCategory2ID":
                        qryResult = qryResult.Join(context.LgsSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(),
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
                    default:
                        qryResult = qryResult.OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
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


        public DataTable GetStockBalancesDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();

            var query = (from pm in context.LgsProductMasters
                         join ps in context.LgsProductStockMasters on pm.LgsProductMasterID equals ps.ProductID
                         where pm.IsDelete == false
                         select ps).AsNoTracking();

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
                            case "LgsProductMasterID":
                                query = (from qr in query
                                         join jt in context.LgsProductMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ProductID equals jt.LgsProductMasterID
                                         select qr
                                        );
                                break;

                            case "LgsDepartmentID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.DepartmentID equals jt.LgsDepartmentID
                                         select qr
                                        );
                                break;

                            case "LgsCategoryID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.CategoryID equals jt.LgsCategoryID
                                         select qr
                                        );
                                break;

                            case "LgsSubCategoryID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategoryID equals jt.LgsSubCategoryID
                                         select qr
                                        );
                                break;

                            case "LgsSubCategory2ID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategory2ID equals jt.LgsSubCategory2ID
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

            return (from pd in queryResult
                        join l in context.Locations on pd.LocationID equals l.LocationID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join dp in context.LgsDepartments on pm.DepartmentID equals dp.LgsDepartmentID
                        join ct in context.LgsCategories on pm.CategoryID equals ct.LgsCategoryID
                        join sct in context.LgsSubCategories on pm.SubCategoryID equals sct.LgsSubCategoryID
                        join sct2 in context.LgsSubCategories2 on pm.SubCategory2ID equals sct2.LgsSubCategory2ID
                        select
                            new
                            {
                                FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                                FieldString2 = reportDataStructList[1].IsSelectionField == true ? dp.DepartmentCode + " " + dp.DepartmentName : "",
                                FieldString3 = reportDataStructList[2].IsSelectionField == true ? ct.CategoryCode + " " + ct.CategoryName : "",
                                FieldString4 = reportDataStructList[3].IsSelectionField == true ? sct.SubCategoryCode + " " + sct.SubCategoryName : "",
                                FieldString5 = reportDataStructList[4].IsSelectionField == true ? sct2.SubCategory2Code + " " + sct2.SubCategory2Name : "",
                                FieldString6 = reportDataStructList[5].IsSelectionField == true ? pm.ProductCode : "",
                                FieldString7 = reportDataStructList[6].IsSelectionField == true ? pm.ProductName : "",
                                FieldDecimal1 = reportDataStructList[7].IsSelectionField == true ? pd.Stock : 0,
                                FieldDecimal2 = reportDataStructList[8].IsSelectionField == true ? pm.SellingPrice : 0,
                                FieldDecimal3 = reportDataStructList[9].IsSelectionField == true ? pd.CostPrice : 0,
                                FieldDecimal4 = reportDataStructList[10].IsSelectionField == true ? (pd.Stock * pm.SellingPrice) : 0,
                                FieldDecimal5 = reportDataStructList[11].IsSelectionField == true ? (pd.Stock * pm.CostPrice) : 0
                            }).ToArray().ToDataTable();
          
        }


        #endregion
    }
}
