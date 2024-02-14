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
using EntityFramework.Extensions;
using System.Data.Entity.Core.Objects;

namespace Service
{
    public class LgsProductMasterService
    {
        ERPDbContext context = new ERPDbContext();
        LgsProductMaster lgsProductMaster = new LgsProductMaster();


        #region methods


        /// Save new Product

        public void AddProduct(LgsProductMaster lgsProductMaster)
        {
            context.LgsProductMasters.Add(lgsProductMaster);
            context.SaveChanges();
        }

        /// Update existing Product

        public void UpdateProduct(LgsProductMaster lgsProductMaster)
        {
            lgsProductMaster.ModifiedUser = Common.LoggedUser;
            lgsProductMaster.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(lgsProductMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteProduct(LgsProductMaster lgsProductMaster)
        {
            lgsProductMaster.IsDelete = true;
            this.context.Entry(lgsProductMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateProduct(int datatransfer)
        {
            context.LgsProductMasters.Update(ps => ps.DataTransfer.Equals(0) && ps.IsDelete.Equals(false), ps => new LgsProductMaster { DataTransfer = datatransfer });
        }

        /// Search Product by code

        public LgsProductMaster GetProductsByRefCodes(string RefCode)
        {
            return context.LgsProductMasters.Where(c => c.ProductCode == RefCode || c.BarCode == RefCode || c.ReferenceCode1 == RefCode || c.ReferenceCode2 == RefCode && c.IsDelete == false).FirstOrDefault();
        }

        public LgsProductMaster GetProductsByCode(string ProductCode)
        {
            return context.LgsProductMasters.Where(c => c.ProductCode == ProductCode  && c.IsDelete == false).FirstOrDefault();
        }

        public LgsProductMaster GetProductsByBarCode(string ProductCode)
        {
            //return context.InvProductMasters.Where(c => c.ProductCode == ProductCode && c.IsDelete == false).FirstOrDefault();

            return context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails,
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b)
                                                            .FirstOrDefault();
        }

        //public InvProductMaster GetProductsByID(int productID)
        //{
        //    return context.InvProductMasters.Where(c => c.InvProductMasterID == productID && c.IsDelete == false).FirstOrDefault();
        //}

        public LgsProductMaster GetProductDetailsByID(long productID)
        {
            return context.LgsProductMasters.Where(c => c.LgsProductMasterID == productID && c.IsDelete == false).FirstOrDefault();
        }

        public LgsProductMaster GetProductsByCodeBySupplier(string ProductCode, long supplierID)
        {
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            return context.LgsProductMasters.Where(c => c.ProductCode == ProductCode && c.LgsSupplierID.Equals(supplierID) && c.IsDelete == false).FirstOrDefault();
        }
        
        ///  Search Product by name

        public LgsProductMaster GetProductsByName(string productName)
        {
            return context.LgsProductMasters.Where(c => c.ProductName == productName && c.IsDelete == false).FirstOrDefault();
        }

        public LgsProductMaster GetProductsByNameBySupplier(string productName, long supplierID)
        {
            return context.LgsProductMasters.Where(c => c.ProductName == productName && c.LgsSupplierID.Equals(supplierID) && c.IsDelete == false).FirstOrDefault();
        }

        /// Get all Product details 

        public List<LgsProductMaster> GetAllProducts()
        {
            return context.LgsProductMasters.Where(c => c.IsDelete == false).ToList();
        }

        public DataTable GetAllProductsDataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.LgsProductMasters where d.IsDelete.Equals(false) && d.DataTransfer.Equals(0) select new { d.LgsProductMasterID, d.ProductCode, d.BarCode, d.ReferenceCode1, d.ReferenceCode2, d.ProductName, d.NameOnInvoice, d.DepartmentID, d.CategoryID, d.SubCategoryID, d.SubCategory2ID, d.LgsSupplierID, d.UnitOfMeasureID, d.PackSize, d.ProductImage, ValuationMethod = d.CostingMethod, d.CostPrice, d.AverageCost, d.SellingPrice, WholeSalePrice = d.WholesalePrice, d.MinimumPrice, d.FixedDiscount, d.MaximumDiscount, d.MaximumPrice, d.FixedDiscountPercentage, d.MaximumDiscountPercentage, d.ReOrderLevel, d.ReOrderQty, d.ReOrderPeriod, d.IsActive, d.IsBatch, d.IsPromotion, d.IsBundle, d.IsFreeIssue, IsDryage = d.IsDrayage, Dryage = d.DrayagePercentage, d.IsExpiry, d.IsConsignment, d.IsCountable, d.IsDCS, d.DcsID, d.IsTax, d.IsSerial, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
            return dt = Common.LINQToDataTable(query);
        }

        public DataTable GetProductsDataTable()
        {
            DataTable dt = new DataTable();
            var query = from pm in context.LgsProductMasters
                        join s in context.LgsSuppliers on pm.LgsSupplierID equals s.LgsSupplierID
                        join dep in context.LgsDepartments on pm.DepartmentID equals dep.LgsDepartmentID
                        join cat in context.LgsCategories on pm.CategoryID equals cat.LgsCategoryID
                        join scat in context.LgsSubCategories on pm.SubCategoryID equals scat.LgsSubCategoryID
                        join scat2 in context.LgsSubCategories2 on pm.SubCategory2ID equals scat2.LgsSubCategory2ID
                        where pm.IsDelete.Equals(false)
                        select new
                        {
                            pm.ProductCode,
                            pm.ProductName,
                            pm.NameOnInvoice,
                            dep.DepartmentCode,
                            cat.CategoryCode,
                            scat.SubCategoryCode,
                            scat2.SubCategory2Code,
                            pm.BarCode,
                            pm.ReferenceCode1,
                            pm.ReferenceCode2,
                            s.SupplierCode,
                            pm.CostPrice,
                            pm.AverageCost,
                            pm.SellingPrice,
                            WholeSalePrice = pm.WholesalePrice,
                            pm.ReOrderLevel,
                            pm.ReOrderQty,
                            pm.ReOrderPeriod,
                        };
            return dt = Common.LINQToDataTable(query);
        }

        public DataTable GetProductsDataTableForTransactions() 
        {
            DataTable dt = new DataTable();
            var query = from pm in context.LgsProductMasters
                        join s in context.LgsSuppliers on pm.LgsSupplierID equals s.LgsSupplierID
                        join dep in context.LgsDepartments on pm.DepartmentID equals dep.LgsDepartmentID
                        join cat in context.LgsCategories on pm.CategoryID equals cat.LgsCategoryID
                        join scat in context.LgsSubCategories on pm.SubCategoryID equals scat.LgsSubCategoryID
                        join scat2 in context.LgsSubCategories2 on pm.SubCategory2ID equals scat2.LgsSubCategory2ID
                        where pm.IsDelete.Equals(false) //&&
                        //s.LgsSupplierID == supplierID
                        select new
                        {
                            pm.ProductCode,
                            pm.ProductName,
                            pm.NameOnInvoice,
                            dep.DepartmentCode,
                            cat.CategoryCode,
                            scat.SubCategoryCode,
                            scat2.SubCategory2Code,
                            s.SupplierCode,
                            pm.CostPrice,
                            pm.AverageCost,
                            pm.SellingPrice,
                            WholeSalePrice = pm.WholesalePrice,
                            pm.ReOrderLevel,
                            pm.ReOrderQty,
                            pm.ReOrderPeriod,
                        };
            return dt = Common.LINQToDataTable(query);
        }

        public DataTable GetAllProductRelatedToBatchNo(long[] arrDept,long[] arrCategory, long[] arrSubcategory,long[] arrSubCategory2,string[] arrBatchNo,long[] arrLocation)
        {
            DataTable dt = new DataTable();

            var query = (from p in context.LgsProductMasters  
                         join pb in context.LgsProductBatchNoExpiaryDetails on p.LgsProductMasterID equals pb.ProductID
                         join lc in context.Locations on pb.LocationID equals lc.LocationID
                         join um in context.UnitOfMeasures on pb.UnitOfMeasureID equals um.UnitOfMeasureID
                         where (arrDept).Contains(p.DepartmentID)
                         || (arrCategory).Contains(p.CategoryID)
                         || (arrSubcategory).Contains(p.SubCategoryID)
                         || (arrSubCategory2).Contains(p.SubCategory2ID)
                         || (arrBatchNo).Contains(pb.BatchNo)
                         || (arrLocation).Contains(lc.LocationID)
                         // where arrDept.Any(dept => dept.Equals(p.DepartmentID)) 
                         //&& arrCategory.Any(category => category.Equals(p.CategoryID))
                         //&& arrSubcategory.Any(subcategory =>subcategory.Equals(p.SubCategoryID)) && arrSubCategory2.Any(subcategory2 => subcategory2.Equals(p.SubCategory2ID))
                         //&& arrBatchNo.Any(batchno => batchno.Equals(pb.BatchNo))
                         //&& (arrCategory).Contains(p.CategoryID) && (arrSubcategory).Contains(p.SubCategoryID) && (arrSubCategory2).Contains(p.SubCategory2ID) && (arrBatchNo).Contains(pb.BatchNo)
                         select new
                         {
                             pb.LocationID,
                             lc.LocationName,
                             pb.BatchNo,
                             pb.ProductID,
                             p.ProductCode,
                             p.ProductName,
                             pb.Qty,
                             pb.UnitOfMeasureID
                             ,um.UnitOfMeasureName,
                             pb.CostPrice,
                             pb.SellingPrice,
                             NewSellingPrice=0,
                             NewCostPrice=0
                         });
            return dt = Common.LINQToDataTable(query);
 
        }

        public List<PriceChangeTemp> GetAllProductRelatedToBatchNoList(long[] arrDept, long[] arrCategory, long[] arrSubcategory, long[] arrSubCategory2, string[] arrBatchNo, long[] arrLocation, long[] arrSupplier)
        {
            PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
            List<PriceChangeTemp> priceChangeLst = new List<PriceChangeTemp>();
            long lineNo = 0;
            var query = (from p in context.LgsProductMasters
                         join pb in context.LgsProductBatchNoExpiaryDetails on p.LgsProductMasterID equals pb.ProductID
                         join lc in context.Locations on pb.LocationID equals lc.LocationID
                         join um in context.UnitOfMeasures on pb.UnitOfMeasureID equals um.UnitOfMeasureID
                         where (arrDept).Contains(p.DepartmentID)
                         || (arrCategory).Contains(p.CategoryID)
                         || (arrSubcategory).Contains(p.SubCategoryID)
                         || (arrSubCategory2).Contains(p.SubCategory2ID)
                         || (arrBatchNo).Contains(pb.BatchNo)
                         || (arrSupplier).Contains(p.LgsSupplierID)
                         && (arrLocation).Contains(lc.LocationID)
                         orderby p.LgsProductMasterID
                         // where arrDept.Any(dept => dept.Equals(p.DepartmentID)) 
                         //&& arrCategory.Any(category => category.Equals(p.CategoryID))
                         //&& arrSubcategory.Any(subcategory =>subcategory.Equals(p.SubCategoryID)) && arrSubCategory2.Any(subcategory2 => subcategory2.Equals(p.SubCategory2ID))
                         //&& arrBatchNo.Any(batchno => batchno.Equals(pb.BatchNo))
                         //&& (arrCategory).Contains(p.CategoryID) && (arrSubcategory).Contains(p.SubCategoryID) && (arrSubCategory2).Contains(p.SubCategory2ID) && (arrBatchNo).Contains(pb.BatchNo)
                         select new
                         {
                             pb.LocationID,
                             lc.LocationName,
                             pb.BatchNo,
                             pb.ProductID,
                             p.ProductCode,
                             p.ProductName,
                             pb.Qty,
                             pb.UnitOfMeasureID,
                             p.MaximumPrice,
                             um.UnitOfMeasureName,
                             pb.CostPrice,
                             pb.SellingPrice,
                             NewSellingPrice = 0,
                             NewCostPrice = 0
                         }).ToArray();

            foreach (var tmpPriceChange in query)
            {
                priceChangeTemp = new PriceChangeTemp();
                priceChangeTemp.ProductCode = tmpPriceChange.ProductCode;
                priceChangeTemp.ProductID = tmpPriceChange.ProductID;
                priceChangeTemp.ProductName = tmpPriceChange.ProductName;
                priceChangeTemp.Qty = tmpPriceChange.Qty;
                priceChangeTemp.SellingPrice = tmpPriceChange.SellingPrice;
                priceChangeTemp.UnitOfMeasureID = tmpPriceChange.UnitOfMeasureID;
                priceChangeTemp.UnitOfMeasureName = tmpPriceChange.UnitOfMeasureName;
                priceChangeTemp.NewSellingPrice = tmpPriceChange.NewSellingPrice;
                priceChangeTemp.NewCostPrice = tmpPriceChange.NewCostPrice;
                priceChangeTemp.MRP = tmpPriceChange.MaximumPrice;
                priceChangeTemp.LocationName = tmpPriceChange.LocationName;
                priceChangeTemp.LocationId = tmpPriceChange.LocationID;
                priceChangeTemp.BatchNo = tmpPriceChange.BatchNo;
                priceChangeTemp.CostPrice = tmpPriceChange.CostPrice;
               
                lineNo=   lineNo + 1;
                priceChangeTemp.LineNo = lineNo;
                
                priceChangeLst.Add(priceChangeTemp);
            }


            return priceChangeLst.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<string> GetAllProductsArray()
        {
            List<string> list = context.LgsProductMasters.Where(c => c.IsDelete == false).Select(c => c.ProductCode).ToList();
            return list;


        }

        public DataTable GetAllProductDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsProductMasters.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsManualRecordFilter)
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @0 ", reportConditionsDataStruct.ConditionFrom.Trim()); }
                    else
                    {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim());}
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LgsDepartmentID":
                                query = (from qr in query
                                         join jt in context.LgsDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.DepartmentID equals jt.LgsDepartmentID
                                         select qr
                                        );
                                break;
                            case "LgsCategoryID":
                                query = (from qr in query
                                         join jt in context.LgsCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CategoryID equals jt.LgsCategoryID
                                         select qr
                                       );
                                break;
                            case "LgsSubCategoryID":
                                query = (from qr in query
                                         join jt in context.LgsSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SubCategoryID equals jt.LgsSubCategoryID
                                         select qr
                                        );
                                break;
                            case "LgsSubCategory2ID":
                                query = (from qr in query
                                         join jt in context.LgsSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SubCategory2ID equals jt.LgsSubCategory2ID
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
                    {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() +
                                    ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from pm in query
                               join dm in context.LgsDepartments on pm.DepartmentID equals dm.LgsDepartmentID
                               join cm in context.LgsCategories on pm.CategoryID equals cm.LgsCategoryID
                               join sm in context.LgsSubCategories on pm.SubCategoryID equals sm.LgsSubCategoryID
                               join sc in context.LgsSubCategories2 on pm.SubCategory2ID equals sc.LgsSubCategory2ID
                               select new
                               {
                                   FieldString1 = pm.ProductCode,
                                   FieldString2 = pm.ProductName,
                                   FieldString3 = pm.NameOnInvoice,
                                   FieldString4 = dm.DepartmentCode + dm.DepartmentName,
                                   FieldString5 = cm.CategoryCode + cm.CategoryName,
                                   FieldString6 = sm.SubCategoryCode + sm.SubCategoryName,
                                   FieldString7 = sc.SubCategory2Code + sc.SubCategory2Name,
                                   FieldString8 = pm.BarCode,
                                   FieldString9 = pm.ReOrderQty,
                                   FieldString10 = pm.CostPrice,
                                   FieldString11 = pm.SellingPrice,
                                   FieldString12 = pm.FixedDiscount,
                                   FieldString13 = pm.IsBatch,
                                   FieldString14 = pm.IsPromotion
                               }).ToArray();

            return queryResult.ToDataTable();
        }

        public DataTable GetAllProductDataTable()
        {
            //DataTable tblProductMasters = new DataTable();

            var query = (from pm in context.LgsProductMasters
                      join dm in context.LgsDepartments on pm.DepartmentID equals dm.LgsDepartmentID
                      join cm in context.LgsCategories on pm.CategoryID equals cm.LgsCategoryID
                      join sm in context.LgsSubCategories on pm.SubCategoryID equals sm.LgsSubCategoryID
                      join sc in context.LgsSubCategories2 on pm.SubCategory2ID equals sc.LgsSubCategory2ID 
                      select new
                      {
                          FieldString1 = pm.ProductCode,
                          FieldString2 = pm.ProductName,
                          FieldString3 = pm.NameOnInvoice,
                          FieldString4 = dm.DepartmentCode + dm.DepartmentName,
                          FieldString5 = cm.CategoryCode + cm.CategoryName,
                          FieldString6 = sm.SubCategoryCode + sm.SubCategoryName,
                          FieldString7 = sc.SubCategory2Code + sc.SubCategory2Name,
                          FieldString8 = pm.BarCode,
                          FieldString9 = pm.ReOrderQty,
                          FieldString10 = pm.CostPrice,
                          FieldString11 = pm.SellingPrice,
                          FieldString12 = pm.FixedDiscount,
                          FieldString13 = pm.IsBatch,
                          FieldString14 = pm.IsPromotion
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblProductMasters = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsProductMasters
                                             .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsDepartmentID":
                    qryResult = qryResult.Join(context.LgsDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(), "new(inner.DepartmentName)")
                                .GroupBy("new(DepartmentName)", "new(DepartmentName)")
                                .OrderBy("Key.DepartmentName")
                                .Select("Key.DepartmentName");
                    break;
                case "LgsCategoryID":
                    qryResult = qryResult.Join(context.LgsCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(), "new(inner.CategoryName)")
                                .GroupBy("new(CategoryName)", "new(CategoryName)")
                                .OrderBy("Key.CategoryName")
                                .Select("Key.CategoryName");
                    break;
                case "LgsSubCategoryID":
                    qryResult = qryResult.Join(context.LgsSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategoryName)")
                                .GroupBy("new(SubCategoryName)", "new(SubCategoryName)")
                                .OrderBy("Key.SubCategoryName")
                                .Select("Key.SubCategoryName");
                    break;
                case "LgsSubCategory2ID":
                    qryResult = qryResult.Join(context.LgsSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategory2Name)")
                                .GroupBy("new(SubCategory2Name)", "new(SubCategory2Name)")
                                .OrderBy("Key.SubCategory2Name")
                                .Select("Key.SubCategory2Name");
                    break;
                default:
                    qryResult = qryResult.OrderBy(reportDataStruct.DbColumnName).Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResult)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }

       #region GetNewCode

        public string GetDirectNewCode(string formName) 
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.LgsProductMasters.Max(c => c.ProductCode);
            
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }

        #endregion

        public string GetNewCode(string formName, string productCode, long departmentID, long categoryID, long subCategoryID) 
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = productCode;
            int codeLength = autoGenerateInfo.CodeLength;

            int prefixLength = Convert.ToInt32(productCode.Length);
            int length = codeLength - prefixLength;

            //getNewCode = context.LgsProductMasters.Max(c => EntityFunctions.Right(c.ProductCode, length));
            getNewCode = context.LgsProductMasters.Where(pm => pm.DepartmentID.Equals(departmentID) && pm.CategoryID.Equals(categoryID) && pm.SubCategoryID.Equals(subCategoryID)).Max(c => EntityFunctions.Right(c.ProductCode, length));

            if (getNewCode == null)
            {
                getNewCode = "0";
            }

            if (getNewCode == null) { getNewCode = Convert.ToString("1"); }
            else { getNewCode = (int.Parse(getNewCode) + 1).ToString(); }

            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }


        public string[] GetAllProductCodes()
        {
            List<string> ProductCodeList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();

        }

        public string[] GetAllProductNames()
        {
            List<string> ProductNameList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.ProductName).ToList();
            return ProductNameList.ToArray();

        }

        public string[] GetAllProductCodesBySupplier(long supplierID)
        {
            List<string> ProductCodeList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true) && c.LgsSupplierID.Equals(supplierID)).Select(c => c.ProductCode).ToList();
            return ProductCodeList.ToArray();

        }

        public string[] GetAllProductNamesBySupplier(long supplierID)
        {
            List<string> ProductNameList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true) && c.LgsSupplierID.Equals(supplierID)).Select(c => c.ProductName).ToList();
            return ProductNameList.ToArray();
        }

        public List<LgsProductUnitConversion> GetUpdateProductUnitConversionTemp(List<LgsProductUnitConversion> lgsProductUnitConversionList, LgsProductUnitConversion lgsProductUnitConversionPrm, LgsProductMaster lgsProductMaster)
        {
            LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();

            lgsProductUnitConversion = lgsProductUnitConversionList.Where(p => p.ProductID == lgsProductUnitConversionPrm.ProductID && p.UnitOfMeasureID == lgsProductUnitConversionPrm.UnitOfMeasureID).FirstOrDefault();

            if (lgsProductUnitConversion == null)
            {
                if (lgsProductUnitConversionList.Count.Equals(0))
                    lgsProductUnitConversionPrm.LineNo = 1;
                else
                    lgsProductUnitConversionPrm.LineNo = lgsProductUnitConversionList.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsProductUnitConversionList.Remove(lgsProductUnitConversion);
                lgsProductUnitConversionPrm.LineNo = lgsProductUnitConversion.LineNo;
            }

            lgsProductUnitConversionList.Add(lgsProductUnitConversionPrm);

            return lgsProductUnitConversionList.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<LgsProductLink> GetUpdateProductLinkTemp(List<LgsProductLink> lgsProductLinkList, LgsProductLink lgsProductLinkPrm, LgsProductMaster lgsProductMaster)
        {
            LgsProductLink lgsProductLink = new LgsProductLink();

            lgsProductLink = lgsProductLinkList.Where(p => p.ProductID == lgsProductLinkPrm.ProductID && p.ProductLinkCode == lgsProductLinkPrm.ProductLinkCode).FirstOrDefault();

            if (lgsProductLink == null)
            {
                //if (invProductLinkList.Count.Equals(0))
                //    invProductLinkPrm.LineNo = 1;
                //else
                //    invProductLinkPrm.LineNo = invProductLinkList.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsProductLinkList.Remove(lgsProductLink);
                //invProductLinkPrm.LineNo = invProductLink.LineNo;
            }

            lgsProductLinkList.Add(lgsProductLinkPrm);

            return lgsProductLinkList.ToList();
        }

        //public List<InvProductUnitConversion> GetUpdateProductUnitConversionTemp(List<InvProductUnitConversion> invProductUnitConversionList, InvProductUnitConversion invProductUnitConversionPrm, InvProductMaster invProductMaster)
        //{
        //    InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

        //    invProductUnitConversion = invProductUnitConversionList.Where(p => p.ProductID == invProductUnitConversionPrm.ProductID && p.UnitOfMeasureID == invProductUnitConversionPrm.UnitOfMeasureID).FirstOrDefault();

        //    if (invProductUnitConversion == null || invProductUnitConversion.LineNo.Equals(0))
        //    {
        //        if (invProductUnitConversionList.Count.Equals(0))
        //            invProductUnitConversionPrm.LineNo = 1;
        //        else
        //            invProductUnitConversionPrm.LineNo = invProductUnitConversionList.Max(s => s.LineNo) + 1;
        //    }
        //    else
        //    {
        //        invProductUnitConversionList.Remove(invProductUnitConversion);
        //        invProductUnitConversionPrm.LineNo = invProductUnitConversion.LineNo;
        //    }

        //    invProductUnitConversionList.Add(invProductUnitConversionPrm);

        //    return invProductUnitConversionList.OrderBy(pd => pd.LineNo).ToList();
        //}

        public List<LgsProductSupplierLink> GetUpdateProductSupplierLinkTemp(List<LgsProductSupplierLink> lgsproductSupplierLinkList, LgsProductSupplierLink lgsProductSupplierLinkPrn, LgsProductMaster lgsProductMaster)
        {
            LgsProductSupplierLink lgsProductSupplierLink = new LgsProductSupplierLink();

            lgsProductSupplierLink = lgsproductSupplierLinkList.Where(p => p.ProductID == lgsProductSupplierLinkPrn.ProductID && p.SupplierCode == lgsProductSupplierLinkPrn.SupplierCode).FirstOrDefault();

            if (lgsProductSupplierLink != null)
            {
                lgsproductSupplierLinkList.Remove(lgsProductSupplierLink);
            }

            lgsproductSupplierLinkList.Add(lgsProductSupplierLinkPrn);

            return lgsproductSupplierLinkList.ToList();
        }

        public DataTable GetAllLgsProductDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsProductMasters.Where("IsDelete=false");

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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)) && reportConditionsDataStruct.ReportDataStruct.IsJoinField == false)
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from pm in query
                               join dm in context.LgsDepartments on pm.DepartmentID equals dm.LgsDepartmentID
                               join cm in context.LgsCategories on pm.CategoryID equals cm.LgsCategoryID
                               join sm in context.LgsSubCategories on pm.SubCategoryID equals sm.LgsSubCategoryID
                               join sc in context.LgsSubCategories2 on pm.SubCategory2ID equals sc.LgsSubCategory2ID
                               select new
                               {
                                   FieldString1 = dm.DepartmentCode + " " + dm.DepartmentName,
                                   FieldString2 = cm.CategoryCode + " " + cm.CategoryName,
                                   FieldString3 = sm.SubCategoryCode + " " + sm.SubCategoryName,
                                   FieldString4 = sc.SubCategory2Code + " " + sc.SubCategory2Name,
                                   FieldString5 = pm.ProductCode,
                                   FieldString6 = pm.ProductName,
                                   FieldString7 = pm.NameOnInvoice,
                                   FieldString8 = pm.BarCode,
                                   FieldString9 = pm.ReOrderQty,
                                   FieldString10 = pm.CostPrice,
                                   FieldString11 = pm.SellingPrice,
                                   FieldString12 = pm.FixedDiscount,
                                   FieldString13 = pm.IsBatch,
                                   FieldString14 = pm.IsPromotion
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


        #endregion

        public string [] GetAllProductCodesAccordingToBatchNumber() 
        {
            List<String> rtnProducts = new List<string>();
            var products = (from bn in context.LgsProductBatchNoExpiaryDetails
                            join pm in context.LgsProductMasters on bn.ProductID equals pm.LgsProductMasterID
                            select new
                            {
                                pm.ProductCode,
                            }).ToArray();

            foreach (var temp in products)
            {
                rtnProducts.Add(temp.ProductCode);
            }
            return rtnProducts.ToArray();          
        }

        public string [] GetAllProductNamesAccordingToBatchNumber() 
        {
            List<String> rtnProducts = new List<string>();
            var products = (from bn in context.LgsProductBatchNoExpiaryDetails
                            join pm in context.LgsProductMasters on bn.ProductID equals pm.LgsProductMasterID
                            select new
                            {
                                pm.ProductName
                            }).ToArray();

            foreach (var temp in products)
            {
                rtnProducts.Add(temp.ProductName);
            }
            return rtnProducts.ToArray();
        }

        //public string[] GetAllProductCodes()
        //{
        //    List<string> ProductCodeList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();
        //    return ProductCodeList.ToArray();

        //}

        public DataTable GetProductsSearchAccordingToCombination(long departmentID, long CategoryID, long SubCategoryID, long subCategory2ID, long SupplierID, decimal costPrice, decimal sellingPrise)
        {
            var qry = (from pm in context.LgsProductMasters
                       join dep in context.LgsDepartments on pm.DepartmentID equals dep.LgsDepartmentID
                       join cat in context.LgsCategories on pm.CategoryID equals cat.LgsCategoryID
                       join scat in context.LgsSubCategories on pm.SubCategoryID equals scat.LgsSubCategoryID
                       join scat2 in context.LgsSubCategories2 on pm.SubCategory2ID equals scat2.LgsSubCategory2ID
                       join s in context.LgsSuppliers on pm.LgsSupplierID equals s.LgsSupplierID
                       select new
                       {
                           pm.LgsProductMasterID,
                           pm.ProductCode,
                           pm.ProductName,
                           dep.DepartmentName,
                           pm.DepartmentID,
                           cat.CategoryName,
                           pm.CategoryID,
                           scat.SubCategoryName,
                           pm.SubCategoryID,
                           scat2.SubCategory2Name,
                           pm.SubCategory2ID,
                           s.SupplierName,
                           pm.LgsSupplierID,
                           pm.CostPrice,
                           pm.SellingPrice
                       }).ToArray();

            if (departmentID != 0)
            {
                qry = (from q in qry
                       where q.DepartmentID == departmentID
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();

            }
            if (CategoryID != 0)
            {
                qry = (from q in qry
                       where q.CategoryID == CategoryID
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (SubCategoryID != 0)
            {
                qry = (from q in qry
                       where q.SubCategoryID == SubCategoryID
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (subCategory2ID != 0)
            {
                qry = (from q in qry
                       where q.SubCategory2ID == subCategory2ID
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (SupplierID != 0)
            {
                qry = (from q in qry
                       where q.LgsSupplierID == SupplierID
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (costPrice != 0)
            {
                qry = (from q in qry
                       where q.CostPrice == costPrice
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }
            if (sellingPrise != 0)
            {
                qry = (from q in qry
                       where q.SellingPrice == sellingPrise
                       select new
                       {
                           q.LgsProductMasterID,
                           q.ProductCode,
                           q.ProductName,
                           q.DepartmentName,
                           q.DepartmentID,
                           q.CategoryName,
                           q.CategoryID,
                           q.SubCategoryName,
                           q.SubCategoryID,
                           q.SubCategory2Name,
                           q.SubCategory2ID,
                           q.SupplierName,
                           q.LgsSupplierID,
                           q.CostPrice,
                           q.SellingPrice
                       }).ToArray();
            }

            var rtnqry = (from q in qry
                          select new
                          {
                              q.ProductCode,
                              q.ProductName,
                              Department = q.DepartmentName,
                              Category = q.CategoryName,
                              SubCategory = q.SubCategoryName,
                              SubCategory2 = q.SubCategory2Name, 
                              q.SupplierName,
                              q.CostPrice,
                              q.SellingPrice
                          }).ToArray();

            return rtnqry.ToDataTable();

        }

    }
}
