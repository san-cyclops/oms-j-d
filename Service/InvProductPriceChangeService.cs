using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data.Entity;
using System.Transactions;
using Utility;
using EntityFramework.Extensions;
using System.Collections;
using MoreLinq;

namespace Service
{
    public class InvProductPriceChangeService
    {
        //private ERPDbContext context = new ERPDbContext();

        public List<InvProductBatchNoExpiaryDetail> GetAllBatchNos()
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvProductBatchNoExpiaryDetails.GroupBy(b => b.BatchNo).Select(b => b.FirstOrDefault()).ToList(); }
        }

        public void UpdatePriceDtl(InvProductPriceChangeDetail productPriceChange)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.InvProductBatchNoExpiaryDetails.Update(ps => ps.ProductID.Equals(productPriceChange.ProductID) && ps.LocationID.Equals(productPriceChange.LocationID), ps => new InvProductBatchNoExpiaryDetail { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewCostPrice });
                context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(productPriceChange.ProductID), pm => new InvProductMaster { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewSellingPrice });
                context.InvProductStockMasters.Update(ps => ps.ProductID.Equals(productPriceChange.ProductID), ps => new InvProductStockMaster { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewSellingPrice });
            }
        }

        //public void UpdatePriceChange(InvProductPriceChangeDetail productPriceChange)
        //{
        //    productPriceChange.ModifiedUser = Common.LoggedUser;
        //    productPriceChange.ModifiedDate = Common.GetSystemDateWithTime(); 
        //    context.Entry(productPriceChange).State = EntityState.Modified;
        //    context.SaveChanges();
        //}

        public string[] GetAllSavedDocumentNumbers(int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> PriceChangeList = context.InvProductPriceChangeDetails.Where(c => c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
                return PriceChangeList.ToArray();
            }
        }

        public string[] GetPausedDocumentNumbers()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> pausedDocumentList = new List<string>();
                pausedDocumentList = context.InvProductPriceChangeHeaders.Select(po => po.DocumentNo).ToList();
                string[] pausedDocumentCodes = pausedDocumentList.ToArray();
                return pausedDocumentCodes;
            }
        }

        public string[] GetPausedDocumentNumbers(DateTime EffDate)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> pausedDocumentList = new List<string>();
                pausedDocumentList = context.InvProductPriceChangeHeaders.Where(po => po.DocumentStatus.Equals(0) && po.DocumentDate.Equals(EffDate)).Select(po => po.DocumentNo).ToList();
                string[] pausedDocumentCodes = pausedDocumentList.ToArray();
                return pausedDocumentCodes;
            }
        }

        public InvProductPriceChangeDetail GetSavedDocumentDetailsByDocumentNumber4BarCode(string documentNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvProductPriceChangeDetails.Where(po => po.DocumentNo == documentNo.Trim()).FirstOrDefault(); }
        }

        public InvProductPriceChangeDetail GetSavedDocumentDetailsByDocumentNumber(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvProductPriceChangeDetails.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).OrderBy(a => a.LineNo).FirstOrDefault(); }

        }

        public InvProductPriceChangeHeader GetPausedInvProductPriceChangeHeaderByDocumentNo(int documentID, string documentNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvProductPriceChangeHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault(); }
        }

        public List<PriceChangeTemp> GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            //return context.InvProductPriceChanges.Where(po => po.DocumentNo == documentNo.Trim()).FirstOrDefault();
            using (ERPDbContext context = new ERPDbContext())
            {
                List<PriceChangeTemp> PriceChangeTempList = new List<PriceChangeTemp>();
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();

                var pclst = (from pc in context.InvProductPriceChangeDetails
                             join uom in context.UnitOfMeasures on pc.UnitOfMeasureID equals uom.UnitOfMeasureID
                             join lc in context.Locations on pc.LocationID equals lc.LocationID
                             where pc.DocumentNo == documentNo
                             select new
                             {
                                 pc.ProductID,
                                 pc.ProductCode,
                                 pc.ProductName,
                                 pc.UnitOfMeasureID,
                                 uom.UnitOfMeasureName,
                                 pc.BatchNo,
                                 pc.CurrentStock,
                                 pc.MRP,
                                 pc.OldCostPrice,
                                 pc.NewCostPrice,
                                 pc.OldSellingPrice,
                                 pc.NewSellingPrice,
                                 pc.LocationID,
                                 lc.LocationName
                             }
                             );

                int lineno = 0;

                foreach (var pcGrdlist in pclst)
                {
                    priceChangeTemp = new PriceChangeTemp();
                    priceChangeTemp.ProductID = pcGrdlist.ProductID;
                    priceChangeTemp.ProductName = pcGrdlist.ProductName;
                    priceChangeTemp.ProductCode = pcGrdlist.ProductCode;
                    priceChangeTemp.UnitOfMeasureID = pcGrdlist.UnitOfMeasureID;
                    priceChangeTemp.UnitOfMeasureName = pcGrdlist.UnitOfMeasureName;
                    priceChangeTemp.BatchNo = pcGrdlist.BatchNo;
                    priceChangeTemp.Qty = pcGrdlist.CurrentStock;
                    priceChangeTemp.MRP = pcGrdlist.MRP;
                    priceChangeTemp.CostPrice = pcGrdlist.OldCostPrice;
                    priceChangeTemp.NewCostPrice = pcGrdlist.NewCostPrice;
                    priceChangeTemp.SellingPrice = pcGrdlist.OldSellingPrice;
                    priceChangeTemp.NewSellingPrice = pcGrdlist.NewSellingPrice;
                    priceChangeTemp.LocationId = pcGrdlist.LocationID;
                    priceChangeTemp.LocationName = pcGrdlist.LocationName;
                    priceChangeTemp.LineNo = lineno;
                    lineno++;
                    PriceChangeTempList.Add(priceChangeTemp);
                }

                return PriceChangeTempList.OrderBy(pd => pd.LineNo).ToList();

            }
        }

        public InvProductBatchNoExpiaryDetail GetProductBatchNo(long productId)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.InvProductBatchNoExpiaryDetails.Where(c => c.ProductID == productId && c.IsDelete == false).FirstOrDefault(); }
        }

        public List<PriceChangeTemp> GetDeletePriceChangeTemp(List<PriceChangeTemp> PriceChangeTempList, PriceChangeTemp priceChangeTemp)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTempx = new PriceChangeTemp();

                priceChangeTempx = PriceChangeTempList.Where(p => p.LineNo == priceChangeTemp.LineNo).FirstOrDefault();
                long removedLineNo = 0;
                if (priceChangeTempx != null)
                {
                    PriceChangeTempList.Remove(priceChangeTempx);
                    removedLineNo = priceChangeTemp.LineNo;
                }

                PriceChangeTempList.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

                return PriceChangeTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<InvProductBatchNoTemp> getBatchNoDetail(InvProductMaster invProductMaster)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

                invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.BalanceQty > 0).ToList();
                foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
                {
                    InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                    invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                    invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                    invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                    invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;
                    invProductBatchNoDetailTemp.LocationID = invProductBatchNoTemp.LocationID;
                    invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
                }

                return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public DataSet GetPriceChangeTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                DataSet dsReportData = new DataSet();
                DataTable dtPriceChange = new DataTable();
                DataTable dtPriceChangeProdcutsDetails = new DataTable();
                DataTable dtProdcutsExtendedPropsColumns = new DataTable();
                dtProdcutsExtendedPropsColumns.Columns.Add("ColumnName", typeof (string));

                #region Price Change Data

                dtPriceChange = (
                                    from ph in context.InvProductPriceChangeHeaders
                                    join pd in context.InvProductPriceChangeDetails on ph.InvProductPriceChangeHeaderID
                                        equals pd.InvProductPriceChangeDetailID
                                    join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                    join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                    join l in context.Locations on ph.LocationID equals l.LocationID
                                    //join tx in context.Taxes on s.t
                                    where
                                        ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) &&
                                        ph.DocumentID.Equals(documentId)
                                    select
                                        new
                                            {
                                                FieldString1 = ph.DocumentNo,
                                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                                FieldString3 = DbFunctions.TruncateTime(ph.EffectFromDate),
                                                FieldString4 = ph.ReferenceNo,
                                                FieldString5 = pd.ProductCode,
                                                FieldString6 = pd.ProductName,
                                                FieldString7 = pm.CreatedUser,
                                            }).AsEnumerable().ToDataTable();

                dsReportData.Tables.Add(dtPriceChange);

                #endregion

                #region Purchase Order Product Data

                InvProductExtendedPropertyValueService invProductExtendedPropertyValueService =
                    new InvProductExtendedPropertyValueService();
                //GetProductExtendedPropertyValuesByProductID
                var PriceChangeProdcutsDetails = (
                                                     from ph in context.InvProductPriceChangeHeaders
                                                     join pd in context.InvProductPriceChangeDetails on
                                                         ph.InvProductPriceChangeHeaderID equals
                                                         pd.InvProductPriceChangeHeaderID
                                                     join pm in context.InvProductMasters on pd.ProductID equals
                                                         pm.InvProductMasterID
                                                     join d in context.InvDepartments on pm.DepartmentID equals
                                                         d.InvDepartmentID
                                                     join c in context.InvCategories on pm.CategoryID equals
                                                         c.InvCategoryID
                                                     join sc in context.InvSubCategories on pm.SubCategoryID equals
                                                         sc.InvSubCategoryID
                                                     join sc2 in context.InvSubCategories2 on pm.SubCategory2ID equals
                                                         sc2.InvSubCategory2ID
                                                     join l in context.Locations on pd.LocationID equals l.LocationID
                                                     where
                                                         ph.DocumentNo.Equals(documentNo) &&
                                                         ph.DocumentStatus.Equals(documentStatus) &&
                                                         ph.DocumentID.Equals(documentId)
                                                     select
                                                         new
                                                             {
                                                                 FieldString1 = ph.DocumentNo,
                                                                 FieldString2 =
                                                         DbFunctions.TruncateTime(ph.DocumentDate),
                                                                 FieldString3 =
                                                         DbFunctions.TruncateTime(ph.EffectFromDate),
                                                                 FieldString4 = ph.ReferenceNo,
                                                                 FieldString5 = pd.ProductCode,
                                                                 FieldString6 = pd.ProductName,
                                                                 FieldString7 = pm.CreatedUser,
                                                                 FieldString8 = pd.BatchNo,
                                                                 FieldString9 = c.CategoryName,
                                                                 FieldString10 = sc.SubCategoryName,
                                                                 FieldString11 = sc2.SubCategory2Name,
                                                                 FieldString12 = pm.InvProductMasterID,
                                                                 FieldString13 = "",
                                                                 FieldString14 = "",
                                                                 FieldString15 = "",
                                                                 FieldString16 = "",
                                                                 FieldString17 = "",
                                                                 FieldString18 = "",
                                                                 FieldString19 = "",
                                                                 FieldString20 = "",
                                                                 FieldString21 = pd.OldSellingPrice,
                                                                 FieldString22 = pd.NewSellingPrice,
                                                                 FieldString23 = pd.CurrentStock,
                                                                 FieldString24 = l.LocationName,
                                                                 FieldString25 =
                                                         (pd.OldSellingPrice > pd.NewSellingPrice
                                                              ? "PRICE DECREASE ADVICE"
                                                              : "PRICE INCREASE ADVICE"),
                                                                 FieldString26 = "",
                                                             }).AsEnumerable();

                dtPriceChangeProdcutsDetails = PriceChangeProdcutsDetails.ToDataTable();

                int lastUpdatedColumnHeader = 12;
                foreach (DataRow drProduct in dtPriceChangeProdcutsDetails.Rows)
                {
                    List<InvProductExtendedPropertyValue> productExPropList =
                        new List<InvProductExtendedPropertyValue>();
                    productExPropList =
                        invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(
                            long.Parse(drProduct["FieldString12"].ToString()));

                    // Set Column header
                    for (int i = 0; i < productExPropList.Count; i++)
                    {
                        if (
                            !dtPriceChangeProdcutsDetails.Columns.Contains(
                                productExPropList[i].ExtendedPropertyName.ToString().Trim()))
                        {
                            if (i < 8) // Because report has ony 14 fields for product extended properties
                            {
                                dtPriceChangeProdcutsDetails.Columns[lastUpdatedColumnHeader].ColumnName =
                                    productExPropList[i].ExtendedPropertyName.ToString().Trim();
                                dtProdcutsExtendedPropsColumns.Rows.Add(
                                    productExPropList[i].ExtendedPropertyName.ToString().Trim());
                                lastUpdatedColumnHeader++;
                            }
                        }
                        if (i < 8) // Because report has ony 14 fields for product extended properties
                        {
                            drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] =
                                productExPropList[i].ValueData.ToString().Trim();
                        }
                    }
                }

                // Reset Column Headers
                for (int i = 0; i < dtPriceChangeProdcutsDetails.Columns.Count; i++)
                {
                    dtPriceChangeProdcutsDetails.Columns[i].ColumnName = "FieldString" + (i + 1);
                }

                dsReportData.Tables.Add(dtPriceChangeProdcutsDetails);
                dsReportData.Tables.Add(dtProdcutsExtendedPropsColumns);

                #endregion

                return dsReportData;
            }
        }
        
        public List<InvBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo, int locationID, int documentID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
                InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();

                var barcodeDetailTemp = (from pm in context.InvProductMasters
                                         join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                         join pd in context.InvProductPriceChangeDetails on psm.ProductID equals pd.ProductID
                                         join ph in context.InvProductPriceChangeHeaders on pd.InvProductPriceChangeHeaderID equals ph.InvProductPriceChangeHeaderID
                                         join um in context.UnitOfMeasures on psm.UnitOfMeasureID equals um.UnitOfMeasureID
                                         join sc in context.Suppliers on pm.SupplierID equals sc.SupplierID
                                         where pm.IsDelete == false
                                         && pd.LocationID == psm.LocationID
                                         && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                         && psm.LocationID == locationID
                                         && ph.DocumentNo == documentNo
                                         && ph.DocumentID == documentID
                                         && pd.BatchNo == psm.BatchNo

                                         select new
                                         {
                                             pm.InvProductMasterID,
                                             pm.ProductCode,
                                             pm.ProductName,
                                             pd.BatchNo,
                                             psm.BarCode,
                                             Qty = pd.CurrentStock,
                                             Stock = psm.Qty,
                                             CostPrice = pd.NewCostPrice,
                                             SellingPrice = pd.NewSellingPrice,
                                             OldCostPrice = pd.OldCostPrice,
                                             OldSellingPrice = pd.OldSellingPrice,
                                             UnitOfMeasureID = um.UnitOfMeasureID,
                                             UnitOfMeasureName = um.UnitOfMeasureName,
                                             LineNo = 0,
                                             pd.DocumentDate,
                                             sc.SupplierCode
                                         }).ToArray();
                int lineno = 0;

                foreach (var tempProduct in barcodeDetailTemp)
                {
                    invBarcodeDetailTemp = new InvBarcodeDetailTemp();
                    invBarcodeDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invBarcodeDetailTemp.ProductCode = tempProduct.ProductCode;
                    invBarcodeDetailTemp.ProductName = tempProduct.ProductName;
                    invBarcodeDetailTemp.BatchNo = tempProduct.BatchNo;
                    invBarcodeDetailTemp.BarCode = tempProduct.BarCode;
                    invBarcodeDetailTemp.Qty = tempProduct.Qty;
                    invBarcodeDetailTemp.Stock = tempProduct.Stock;
                    invBarcodeDetailTemp.CostPrice = tempProduct.CostPrice;
                    invBarcodeDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invBarcodeDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invBarcodeDetailTemp.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                    invBarcodeDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invBarcodeDetailTemp.LineNo = lineno;
                    lineno++;
                    invBarcodeDetailTemp.DocumentDate = tempProduct.DocumentDate;
                    invBarcodeDetailTemp.SupplierCode = tempProduct.SupplierCode;
                    invBarcodeDetailTemp.OldCostPrice = tempProduct.OldCostPrice;
                    invBarcodeDetailTemp.OldSellingPrice = tempProduct.OldSellingPrice;
                    invBarcodeDetailTempList.Add(invBarcodeDetailTemp);
                }

                return invBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }
      
        public List<InvProductBatchNoTemp> getBatchNoDetailPriceChange(InvProductMaster invProductMaster,long unitOfMeasure)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

                List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

                // invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails .Join ( Locations, t => t.LocationID, t0 => t0.LocationID, (t, t0) => new { t = t, t0 = t0 } ) .Where (temp0 => ((temp0.t.ProductID == invProductMaster.InvProductMasterID) && (temp0.t.UnitOfMeasureID == unitOfMeasure))) .Select ( temp0 => new { InvProductBatchNoExpiaryDetailID = temp0.t.InvProductBatchNoExpiaryDetailID, ProductBatchNoExpiaryDetailID = temp0.t.ProductBatchNoExpiaryDetailID, CompanyID = temp0.t.CompanyID, LocationID = temp0.t.LocationID, CostCentreID = temp0.t.CostCentreID, ReferenceDocumentDocumentID = temp0.t.ReferenceDocumentDocumentID, ReferenceDocumentID = temp0.t.ReferenceDocumentID, ProductID = temp0.t.ProductID, BarCode = temp0.t.BarCode, BatchNo = temp0.t.BatchNo, ExpiryDate = temp0.t.ExpiryDate, LineNo = temp0.t.LineNo, Qty = temp0.t.Qty, UnitOfMeasureID = temp0.t.UnitOfMeasureID, BalanceQty = temp0.t.BalanceQty, CostPrice = temp0.t.CostPrice, SellingPrice = temp0.t.SellingPrice, SupplierID = temp0.t.SupplierID, IsDelete = temp0.t.IsDelete, GroupOfCompanyID = temp0.t.GroupOfCompanyID, CreatedUser = temp0.t.CreatedUser, CreatedDate = temp0.t.CreatedDate, ModifiedUser = temp0.t.ModifiedUser, ModifiedDate = temp0.t.ModifiedDate, DataTransfer = temp0.t.DataTransfer, LocationCode = temp0.t0.LocationCode } )  
                invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.UnitOfMeasureID.Equals(unitOfMeasure)).ToList();  // && ps.BalanceQty > 0
                foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
                {
                    InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();
                    Location location = new Location();
                    LocationService locationService = new LocationService();
                    location = locationService.GetLocationsByID(invProductBatchNoTemp.LocationID);
                    invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                    invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                    invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                    invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;
                    invProductBatchNoDetailTemp.LocationID = invProductBatchNoTemp.LocationID;
                    invProductBatchNoDetailTemp.LocationCode = location.LocationCode;
                    invProductBatchNoDetailTemp.Qty = invProductBatchNoTemp.BalanceQty;
                    invProductBatchNoDetailTemp.LocationName = location.LocationName;
                    invProductBatchNoDetailTemp.Selection = false;
                    invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
                }

                return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList();
            }
        }

        //public List<InvProductBatchNoTemp> getBatchNoDetailPriceChangeSum(InvProductMaster invProductMaster, long unitOfMeasure,long locationId)
        //{
        //    List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

        //    List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

        //    // invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails .Join ( Locations, t => t.LocationID, t0 => t0.LocationID, (t, t0) => new { t = t, t0 = t0 } ) .Where (temp0 => ((temp0.t.ProductID == invProductMaster.InvProductMasterID) && (temp0.t.UnitOfMeasureID == unitOfMeasure))) .Select ( temp0 => new { InvProductBatchNoExpiaryDetailID = temp0.t.InvProductBatchNoExpiaryDetailID, ProductBatchNoExpiaryDetailID = temp0.t.ProductBatchNoExpiaryDetailID, CompanyID = temp0.t.CompanyID, LocationID = temp0.t.LocationID, CostCentreID = temp0.t.CostCentreID, ReferenceDocumentDocumentID = temp0.t.ReferenceDocumentDocumentID, ReferenceDocumentID = temp0.t.ReferenceDocumentID, ProductID = temp0.t.ProductID, BarCode = temp0.t.BarCode, BatchNo = temp0.t.BatchNo, ExpiryDate = temp0.t.ExpiryDate, LineNo = temp0.t.LineNo, Qty = temp0.t.Qty, UnitOfMeasureID = temp0.t.UnitOfMeasureID, BalanceQty = temp0.t.BalanceQty, CostPrice = temp0.t.CostPrice, SellingPrice = temp0.t.SellingPrice, SupplierID = temp0.t.SupplierID, IsDelete = temp0.t.IsDelete, GroupOfCompanyID = temp0.t.GroupOfCompanyID, CreatedUser = temp0.t.CreatedUser, CreatedDate = temp0.t.CreatedDate, ModifiedUser = temp0.t.ModifiedUser, ModifiedDate = temp0.t.ModifiedDate, DataTransfer = temp0.t.DataTransfer, LocationCode = temp0.t0.LocationCode } )  
        //    invProductBatchNoDetail =  InvProductBatchNoExpiaryDetails .Where (t => ((t.LocationID == locationId) && (t.UnitOfMeasureID == unitOfMeasure))) .GroupBy ( t => new { ProductID = t.ProductID } ) .Select ( g => new { ProductID = (Int64?)(g.Key.ProductID), qty = (Decimal?)(g.Sum (p => p.Qty)) } )  
        //    //invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.UnitOfMeasureID.Equals(unitOfMeasure) && ps.LocationID.Equals(locationId)).ToList();  // && ps.BalanceQty > 0
        //    foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
        //    {
        //        InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();
        //        Location location = new Location();
        //        LocationService locationService = new LocationService();
        //        location = locationService.GetLocationsByID(invProductBatchNoTemp.LocationID);
        //        invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
        //        invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
        //        invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
        //        invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;
        //        invProductBatchNoDetailTemp.LocationID = invProductBatchNoTemp.LocationID;
        //        invProductBatchNoDetailTemp.LocationCode = location.LocationCode;
        //        invProductBatchNoDetailTemp.Qty = invProductBatchNoTemp.Qty;
        //        invProductBatchNoDetailTemp.Selection = false;
        //        invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
        //    }

        //    return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        //}

        public DataTable GetPriceChangeDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                StringBuilder selectFields = new StringBuilder();
                var query = context.InvProductMasters.AsQueryable();


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

                    //if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(Int64)))
                    // { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                }

                var queryResult = (from pm in query
                                   join qs in context.InvProductPriceChangeDetails on pm.InvProductMasterID equals qs.ProductID
                                   join de in context.InvDepartments on pm.DepartmentID equals de.InvDepartmentID
                                   join ca in context.InvCategories on pm.CategoryID equals ca.InvCategoryID
                                   select new
                                   {
                                       FieldString1 = qs.CreatedDate,
                                       FieldString2 = de.DepartmentName,
                                       FieldString3 = ca.CategoryName,
                                       FieldString4 = pm.ProductCode,
                                       FieldString5 = pm.ProductName,
                                       FieldDecimal7 = qs.NewCostPrice,
                                       FieldDecimal6 = qs.NewSellingPrice
                                   }).ToArray();


                return queryResult.ToDataTable();
            }
        }

        public List<PriceChangeTemp> getUpdatePriceChangeTemp(  List<PriceChangeTemp> priceChangeGridList,List<PriceChangeTemp> priceChangeTempxList, decimal newCostPrice, decimal newSellingPrice)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                PriceChangeTemp priceChangeTempLine = new PriceChangeTemp();
                InvProductMaster invProductMaster = new InvProductMaster();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                Location location = new Location();
                LocationService locationService = new LocationService();
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();

                var lstitem = priceChangeTempxList;

                foreach (var item in lstitem)
                {
                    priceChangeTemp = new PriceChangeTemp();
                    priceChangeTemp.ProductID = item.ProductID;
                    invProductMaster = invProductMasterService.GetProductDetailsByID(item.ProductID);
                    unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(invProductMaster.UnitOfMeasureID);
                    priceChangeTemp.ProductName = invProductMaster.ProductName;
                    priceChangeTemp.ProductCode = invProductMaster.ProductCode;
                    priceChangeTemp.UnitOfMeasureID = unitOfMeasure.UnitOfMeasureID;
                    priceChangeTemp.UnitOfMeasureName = unitOfMeasure.UnitOfMeasureName;
                    priceChangeTemp.SellingPrice = invProductMaster.SellingPrice;
                    priceChangeTemp.MRP = invProductMaster.MinimumPrice;
                    priceChangeTemp.LocationId = item.LocationId;
                    priceChangeTemp.LocationName = item.LocationName;
                    priceChangeTemp.CostPrice = invProductMaster.CostPrice;
                    priceChangeTemp.BatchNo = item.BatchNo;
                    priceChangeTemp.Qty = item.Qty;
                    priceChangeTemp.NewCostPrice = newCostPrice;
                    priceChangeTemp.NewSellingPrice = newSellingPrice;
                    priceChangeTemp.LineNo = item.LineNo;

                    if (priceChangeGridList != null)
                        priceChangeTempLine = priceChangeGridList.Where(p => p.ProductID == priceChangeTemp.ProductID && p.LocationId == priceChangeTemp.LocationId && p.BatchNo == priceChangeTemp.BatchNo && p.UnitOfMeasureID == priceChangeTemp.UnitOfMeasureID).FirstOrDefault();
                    else
                        priceChangeTempLine = null;
                    if (priceChangeTempLine != null)
                    {
                        priceChangeGridList.Remove(priceChangeTempLine);
                        priceChangeTemp.LineNo = priceChangeTempLine.LineNo;
                        priceChangeGridList.Add(priceChangeTemp);
                    }
                    else
                    {
                        if (priceChangeGridList != null)
                        {
                            if (priceChangeGridList.Count != 0)
                            {
                                priceChangeTemp.LineNo = priceChangeGridList.Max(s => s.LineNo) + 1;
                            }
                        }

                        priceChangeGridList.Add(priceChangeTemp);
                    }



                }
                //}
                //else
                //{
                //    invPurchaseTempDetail.Remove(invPurchaseDetailTemp);
                //     invPurchaseDetail.LineNo = invPurchaseDetailTemp.LineNo;

                // }


                return priceChangeGridList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public List<PriceChangeTemp> getUpdatePriceDiscount(List<PriceChangeTemp> priceChangeGridList, decimal DiscountCostPrice, decimal DiscountSellingPrice, bool isDisPer, bool isIncrement)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                PriceChangeTemp priceChangeTempLine = new PriceChangeTemp();
                InvProductMaster invProductMaster = new InvProductMaster();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
                UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                Location location = new Location();
                LocationService locationService = new LocationService();
                InvProductMasterService invProductMasterService = new InvProductMasterService();
                InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();
                decimal discountCostAmount = 0;
                decimal discountSellingAmount = 0;
                List<PriceChangeTemp> priceChangnewGridList = new List<PriceChangeTemp>();
                var lstitem = priceChangeGridList;

                foreach (var item in lstitem)
                {
                    priceChangeTemp = new PriceChangeTemp();
                    priceChangeTemp.ProductID = item.ProductID;
                    invProductMaster = invProductMasterService.GetProductDetailsByID(item.ProductID);
                    unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(invProductMaster.UnitOfMeasureID);
                    priceChangeTemp.ProductName = invProductMaster.ProductName;
                    priceChangeTemp.ProductCode = invProductMaster.ProductCode;
                    priceChangeTemp.UnitOfMeasureID = unitOfMeasure.UnitOfMeasureID;
                    priceChangeTemp.UnitOfMeasureName = unitOfMeasure.UnitOfMeasureName;
                    priceChangeTemp.SellingPrice = invProductMaster.SellingPrice;
                    priceChangeTemp.MRP = invProductMaster.MinimumPrice;
                    location = locationService.GetLocationsByID(Common.ConvertStringToInt(item.LocationId.ToString()));
                    priceChangeTemp.LocationId = location.LocationID;
                    priceChangeTemp.LocationName = location.LocationName;
                    priceChangeTemp.CostPrice = invProductMaster.CostPrice;
                    priceChangeTemp.BatchNo = item.BatchNo;
                    priceChangeTemp.Qty = item.Qty;
                    discountCostAmount = Common.GetDiscountAmount(isDisPer, item.CostPrice, DiscountCostPrice);
                    discountSellingAmount = Common.GetDiscountAmount(isDisPer, item.SellingPrice, DiscountSellingPrice);
                    if (isIncrement == true)
                    {
                        priceChangeTemp.NewCostPrice = item.CostPrice + discountCostAmount;
                        priceChangeTemp.NewSellingPrice = item.SellingPrice + discountSellingAmount;
                    }
                    else
                    {
                        priceChangeTemp.NewCostPrice = item.CostPrice - discountCostAmount;
                        priceChangeTemp.NewSellingPrice = item.SellingPrice - discountSellingAmount;
                    }
                    priceChangeTemp.LineNo = item.LineNo;

                    priceChangnewGridList.Add(priceChangeTemp);

                    //if (priceChangeGridList != null)
                    //    priceChangeTempLine = priceChangeGridList.Where(p => p.ProductID == priceChangeTemp.ProductID && p.LineNo == item.LineNo && p.LocationId == priceChangeTemp.LocationId && p.BatchNo == priceChangeTemp.BatchNo && p.UnitOfMeasureID == priceChangeTemp.UnitOfMeasureID).FirstOrDefault();
                    //else
                    //    priceChangeTempLine = null;
                    //if (priceChangeTempLine != null)
                    //{
                    //    //priceChangeGridList.Remove(priceChangeTempLine);
                    //    priceChangeTemp.LineNo = priceChangeTempLine.LineNo;
                    //    //priceChangeGridList.Add(priceChangeTemp);
                    //    priceChangnewGridList
                    //}
                    //else
                    //{
                    //    if (priceChangeGridList != null)
                    //    {
                    //        if (priceChangeGridList.Count != 0)
                    //        {
                    //            priceChangeTemp.LineNo = priceChangeGridList.Max(s => s.LineNo) + 1;
                    //        }
                    //    }

                    //    priceChangeGridList.Add(priceChangeTemp);
                    //}



                }
                //}
                //else
                //{
                //    invPurchaseTempDetail.Remove(invPurchaseDetailTemp);
                //     invPurchaseDetail.LineNo = invPurchaseDetailTemp.LineNo;

                // }


                return priceChangnewGridList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        public bool Save(InvProductPriceChangeHeader invProductPriceChangeHeader, List<PriceChangeTemp> priceChangeTemplst, out string newDocumentNo, string formName = "")
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                using (TransactionScope transaction = new TransactionScope())
                {

                    if (invProductPriceChangeHeader.DocumentStatus.Equals(1))
                    {
                        InvProductPriceChangeService invProductPriceChangeService = new InvProductPriceChangeService();
                        LocationService locationService = new LocationService();
                        invProductPriceChangeHeader.DocumentNo =
                            invProductPriceChangeService.GetDocumentNo(formName, invProductPriceChangeHeader.LocationID,
                                                                       locationService.GetLocationsByID(
                                                                           invProductPriceChangeHeader.LocationID)
                                                                                      .LocationCode,
                                                                       invProductPriceChangeHeader.DocumentID, false)
                                                        .Trim();
                    }

                    newDocumentNo = invProductPriceChangeHeader.DocumentNo;

                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    if (invProductPriceChangeHeader.InvProductPriceChangeHeaderID.Equals(0))
                        context.InvProductPriceChangeHeaders.Add(invProductPriceChangeHeader);
                    else
                        context.Entry(invProductPriceChangeHeader).State = EntityState.Modified;
                    context.SaveChanges();

                    context.Set<InvProductPriceChangeDetail>()
                           .Delete(
                               context.InvProductPriceChangeDetails.Where(
                                   pod =>
                                   pod.InvProductPriceChangeDetailID.Equals(
                                       invProductPriceChangeHeader.InvProductPriceChangeHeaderID) &&
                                   pod.LocationID.Equals(invProductPriceChangeHeader.LocationID) &&
                                   pod.DocumentID.Equals(invProductPriceChangeHeader.DocumentID)).AsQueryable());
                    context.SaveChanges();

                    var priceChangeSaveQuery = (from pd in priceChangeTemplst
                                                select new
                                                    {
                                                        LocationID = pd.LocationId,
                                                        pd.ProductID,
                                                        pd.ProductCode,
                                                        pd.ProductName,
                                                        DocumentNo = invProductPriceChangeHeader.DocumentNo,
                                                        DocumentDate = Common.ConvertDateTimeToDate(DateTime.Now),
                                                        OldCostPrice = pd.CostPrice,
                                                        pd.NewCostPrice,
                                                        OldSellingPrice = pd.SellingPrice,
                                                        pd.NewSellingPrice,
                                                        CurrentStock = pd.Qty,
                                                        pd.UnitOfMeasureID,
                                                        pd.BatchNo,
                                                        EffectFromDate = invProductPriceChangeHeader.EffectFromDate,
                                                        Percentage = 0,
                                                        pd.MRP,
                                                        IsDelete = false,
                                                        DocumentID = invProductPriceChangeHeader.DocumentID,
                                                        GroupOfCompanyID = Common.LoggedCompanyID,
                                                        CreatedUser = Common.LoggedUser,
                                                        CreatedDate = invProductPriceChangeHeader.DocumentDate,
                                                        ModifiedUser = Common.LoggedUser,
                                                        ModifiedDate = invProductPriceChangeHeader.DocumentDate,
                                                        DataTransfer = 0,
                                                        pd.LineNo,
                                                        invProductPriceChangeHeader.InvProductPriceChangeHeaderID,


                                                    }).ToList();
                    //CommonService.MergeSqlTo(Common.LINQToDataTable(priceChangeSaveQuery), "InvProductPriceChangeDetail");
                    DataTable dtx = new DataTable();
                    dtx = priceChangeSaveQuery.ToDataTable();
                    dtx.TableName = "InvProductPriceChangeDetail";
                    //CommonService.BulkInsert(dtx, "InvProductPriceChangeDetail");

                    string MergeSql = @" MERGE INTO InvProductPriceChangeDetail AS Target
                                        USING #TEMP AS Source
                                        ON Target.[LocationID] = Source.[LocationID] AND Target.[ProductID] = Source.[ProductID]
		                                    AND Target.[DocumentID] = Source.[DocumentID] AND Target.[BatchNo] = Source.[BatchNo]
                                        WHEN MATCHED 
                                            THEN UPDATE
                                              SET       Target.[ProductID] = Source.[ProductID],
                                                        Target.[ProductCode] = Source.[ProductCode],
                                                        Target.[ProductName] = Source.[ProductName],
                                                        Target.[DocumentNo] = Source.[DocumentNo],
                                                        Target.[DocumentDate] = Source.[DocumentDate],
                                                        Target.[OldCostPrice] = Source.[OldCostPrice],
                                                        Target.[NewCostPrice] = Source.[NewCostPrice],
                                                        Target.[OldSellingPrice] = Source.[OldSellingPrice],
                                                        Target.[NewSellingPrice] = Source.[NewSellingPrice],
                                                        Target.[CurrentStock] = Source.[CurrentStock],
                                                        Target.[UnitOfMeasureID] = Source.[UnitOfMeasureID],
                                                        Target.[BatchNo] = Source.[BatchNo],
                                                        Target.[EffectFromDate] = Source.[EffectFromDate],
                                                        Target.[Percentage] = Source.[Percentage],
                                                        Target.[MRP] = Source.[MRP],
                                                        Target.[IsDelete] = Source.[IsDelete],
                                                        Target.[DocumentID] = Source.[DocumentID],
                                                        Target.[GroupOfCompanyID] = Source.[GroupOfCompanyID],
                                                        Target.[CreatedUser] = Source.[CreatedUser],
                                                        Target.[CreatedDate] = Source.[CreatedDate],
                                                        Target.[ModifiedUser] = Source.[ModifiedUser],
                                                        Target.[ModifiedDate] = Source.[ModifiedDate],
                                                        Target.[DataTransfer] = Source.[DataTransfer],
                                                        Target.[LineNo] = Source.[LineNo],
                                                        Target.[InvProductPriceChangeHeaderID] = Source.[InvProductPriceChangeHeaderID]
                                        WHEN NOT MATCHED 
                                            THEN INSERT ( [LocationID], [ProductID], [ProductCode], [ProductName],
                                                          [DocumentNo], [DocumentDate], [OldCostPrice],
                                                          [NewCostPrice], [OldSellingPrice], [NewSellingPrice],
                                                          [CurrentStock], [UnitOfMeasureID], [BatchNo],
                                                          [EffectFromDate], [Percentage], [MRP], [IsDelete],
                                                          [DocumentID], [GroupOfCompanyID], [CreatedUser],
                                                          [CreatedDate], [ModifiedUser], [ModifiedDate],
                                                          [DataTransfer], [LineNo],[InvProductPriceChangeHeaderID] )
                                              VALUES    ( Source.[LocationID], Source.[ProductID],
                                                          Source.[ProductCode], Source.[ProductName],
                                                          Source.[DocumentNo], Source.[DocumentDate],
                                                          Source.[OldCostPrice], Source.[NewCostPrice],
                                                          Source.[OldSellingPrice], Source.[NewSellingPrice],
                                                          Source.[CurrentStock], Source.[UnitOfMeasureID],
                                                          Source.[BatchNo], Source.[EffectFromDate],
                                                          Source.[Percentage], Source.[MRP], Source.[IsDelete],
                                                          Source.[DocumentID], Source.[GroupOfCompanyID],
                                                          Source.[CreatedUser], Source.[CreatedDate],
                                                          Source.[ModifiedUser], Source.[ModifiedDate],
                                                          Source.[DataTransfer], Source.[LineNo],[InvProductPriceChangeHeaderID] );";


                    CommonService.BulkInsert(priceChangeSaveQuery.ToDataTable(), "InvProductPriceChangeDetail", MergeSql);
                    context.SaveChanges();



                    //if (invProductPriceChangeHeader.DocumentStatus.Equals(1))
                    //{
                    //    UpdatePriceDtl(GetSavedDocumentDetailsByDocumentNumber(invProductPriceChangeHeader.DocumentID, invProductPriceChangeHeader.DocumentNo, invProductPriceChangeHeader.LocationID));
                    //}

                    transaction.Complete();
                    return true;

                }

            }
        }

        public PriceChangeTemp getPriceChangeTemp(List<PriceChangeTemp> priceChangeTemplst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                decimal defaultConvertFactor = 1;
                var PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals
                                  psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where
                                  pm.IsDelete == false &&
                                  pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID) &&
                                  psm.LocationID.Equals(locationID)
                              select new
                                  {
                                      pm.InvProductMasterID,
                                      pm.ProductCode,
                                      pm.ProductName,
                                      uc.UnitOfMeasureID,
                                      uc.UnitOfMeasureName,
                                      psm.CostPrice,
                                      psm.SellingPrice,
                                      pm.MaximumPrice,
                                      lc.LocationID,
                                      lc.LocationName,
                                      psm.Qty,
                                      ConvertFactor = defaultConvertFactor,
                                  }).ToArray();

                if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
                {
                    PCTemp = (dynamic) null;

                    PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals
                                  psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join puc in context.InvProductUnitConversions on pm.InvProductMasterID equals
                                  puc.ProductID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where
                                  pm.IsDelete == false &&
                                  pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID)
                                  && psm.LocationID.Equals(locationID) && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
                              select new
                                  {
                                      pm.InvProductMasterID,
                                      pm.ProductCode,
                                      pm.ProductName,
                                      puc.UnitOfMeasureID,
                                      uc.UnitOfMeasureName,
                                      puc.CostPrice,
                                      puc.SellingPrice,
                                      pm.MaximumPrice,
                                      lc.LocationID,
                                      lc.LocationName,
                                      psm.Qty,
                                      ConvertFactor = puc.ConvertFactor,
                                  }).ToArray();
                }

                foreach (var tempProduct in PCTemp)
                {

                    if (!unitOfMeasureID.Equals(0))
                        priceChangeTemp =
                            priceChangeTemplst.Where(
                                p =>
                                p.ProductID == invProductMaster.InvProductMasterID &&
                                p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        priceChangeTemp =
                            priceChangeTemplst.Where(p => p.ProductID == invProductMaster.InvProductMasterID)
                                              .FirstOrDefault();

                    if (priceChangeTemp == null)
                    {
                        priceChangeTemp = new PriceChangeTemp();
                        priceChangeTemp.ProductID = tempProduct.InvProductMasterID;
                        priceChangeTemp.ProductCode = tempProduct.ProductCode;
                        priceChangeTemp.ProductName = tempProduct.ProductName;
                        priceChangeTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        priceChangeTemp.CostPrice = tempProduct.CostPrice;
                        priceChangeTemp.SellingPrice = tempProduct.SellingPrice;
                        priceChangeTemp.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                        priceChangeTemp.MRP = tempProduct.MaximumPrice;
                        priceChangeTemp.LocationId = tempProduct.LocationID;
                        priceChangeTemp.LocationName = tempProduct.LocationName;

                        priceChangeTemp.Qty = tempProduct.Qty;


                    }

                }

                return priceChangeTemp;
            }
        }

        public List<PriceChangeTemp> getPriceChangeTempList(List<PriceChangeTemp> priceChangeTemplst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                List<PriceChangeTemp> PriceChangeTemplst = new List<PriceChangeTemp>();
                decimal defaultConvertFactor = 1;
                var PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals
                                  psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where
                                  pm.IsDelete == false &&
                                  pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID) &&
                                  psm.LocationID.Equals(locationID)
                              select new
                                  {
                                      pm.InvProductMasterID,
                                      pm.ProductCode,
                                      pm.ProductName,
                                      uc.UnitOfMeasureID,
                                      uc.UnitOfMeasureName,
                                      psm.CostPrice,
                                      psm.SellingPrice,
                                      pm.MaximumPrice,
                                      lc.LocationID,
                                      lc.LocationName,
                                      psm.Qty,
                                      ConvertFactor = defaultConvertFactor,
                                  }).ToArray();

                if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
                {
                    PCTemp = (dynamic) null;

                    PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals
                                  psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join puc in context.InvProductUnitConversions on pm.InvProductMasterID equals
                                  puc.ProductID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where
                                  pm.IsDelete == false &&
                                  pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID)
                                  && psm.LocationID.Equals(locationID) && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
                              select new
                                  {
                                      pm.InvProductMasterID,
                                      pm.ProductCode,
                                      pm.ProductName,
                                      puc.UnitOfMeasureID,
                                      uc.UnitOfMeasureName,
                                      puc.CostPrice,
                                      puc.SellingPrice,
                                      pm.MaximumPrice,
                                      lc.LocationID,
                                      lc.LocationName,
                                      psm.Qty,
                                      ConvertFactor = puc.ConvertFactor,
                                  }).ToArray();
                }

                foreach (var tempProduct in PCTemp)
                {

                    if (!unitOfMeasureID.Equals(0))
                        priceChangeTemp =
                            priceChangeTemplst.Where(
                                p =>
                                p.ProductID == invProductMaster.InvProductMasterID &&
                                p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        priceChangeTemp =
                            priceChangeTemplst.Where(p => p.ProductID == invProductMaster.InvProductMasterID)
                                              .FirstOrDefault();

                    if (priceChangeTemp == null)
                    {
                        priceChangeTemp = new PriceChangeTemp();
                        priceChangeTemp.ProductID = tempProduct.InvProductMasterID;
                        priceChangeTemp.ProductCode = tempProduct.ProductCode;
                        priceChangeTemp.ProductName = tempProduct.ProductName;
                        priceChangeTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        priceChangeTemp.CostPrice = tempProduct.CostPrice;
                        priceChangeTemp.SellingPrice = tempProduct.SellingPrice;
                        priceChangeTemp.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                        priceChangeTemp.MRP = tempProduct.MaximumPrice;
                        priceChangeTemp.LocationId = tempProduct.LocationID;
                        priceChangeTemp.LocationName = tempProduct.LocationName;

                        priceChangeTemp.Qty = tempProduct.Qty;
                        PriceChangeTemplst.Add(priceChangeTemp);
                    }

                }

                return PriceChangeTemplst.OrderBy(x => x.LineNo).ToList();
            }
        }

        public List<PriceChangeTemp> getPriceChangeTempList(List<PriceChangeTemp> priceChangeTemplst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID,int lineNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                PriceChangeTemp priceChangeTemp = new PriceChangeTemp();
                List<PriceChangeTemp> PriceChangeTemplst = new List<PriceChangeTemp>();
                decimal defaultConvertFactor = 1;
                var PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where pm.IsDelete == false && pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID) && psm.LocationID.Equals(locationID)
                              select new
                              {
                                  pm.InvProductMasterID,
                                  pm.ProductCode,
                                  pm.ProductName,
                                  uc.UnitOfMeasureID,
                                  uc.UnitOfMeasureName,
                                  psm.CostPrice,
                                  psm.SellingPrice,
                                  pm.MaximumPrice,
                                  lc.LocationID,
                                  lc.LocationName,
                                  psm.Qty,
                                  ConvertFactor = defaultConvertFactor,
                              }).ToArray();

                if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
                {
                    PCTemp = (dynamic)null;

                    PCTemp = (from pm in context.InvProductMasters
                              join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                              join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                              join puc in context.InvProductUnitConversions on pm.InvProductMasterID equals puc.ProductID
                              join lc in context.Locations on psm.LocationID equals lc.LocationID
                              where pm.IsDelete == false && pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID)
                              && psm.LocationID.Equals(locationID) && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
                              select new
                              {
                                  pm.InvProductMasterID,
                                  pm.ProductCode,
                                  pm.ProductName,
                                  puc.UnitOfMeasureID,
                                  uc.UnitOfMeasureName,
                                  puc.CostPrice,
                                  puc.SellingPrice,
                                  pm.MaximumPrice,
                                  lc.LocationID,
                                  lc.LocationName,
                                  psm.Qty,
                                  ConvertFactor = puc.ConvertFactor,
                              }).ToArray();
                }

                if (!unitOfMeasureID.Equals(0))
                    priceChangeTemp = priceChangeTemplst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.LineNo.Equals(lineNo) && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    priceChangeTemp = priceChangeTemplst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.LineNo == lineNo).FirstOrDefault();


                foreach (var tempProduct in PCTemp)
                {

                    if (priceChangeTemp == null)
                    {
                        priceChangeTemp = new PriceChangeTemp();
                        priceChangeTemp.ProductID = tempProduct.InvProductMasterID;
                        priceChangeTemp.ProductCode = tempProduct.ProductCode;
                        priceChangeTemp.ProductName = tempProduct.ProductName;
                        priceChangeTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        priceChangeTemp.CostPrice = tempProduct.CostPrice;
                        priceChangeTemp.SellingPrice = tempProduct.SellingPrice;
                        priceChangeTemp.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                        priceChangeTemp.MRP = tempProduct.MaximumPrice;
                        priceChangeTemp.LocationId = tempProduct.LocationID;
                        priceChangeTemp.LocationName = tempProduct.LocationName;
                        priceChangeTemp.LineNo = lineNo;
                        priceChangeTemp.Qty = tempProduct.Qty;
                        PriceChangeTemplst.Add(priceChangeTemp);
                    }
                    //else
                    //{
                    //    PriceChangeTemplst.Add(priceChangeTemp);
                    //}

                }

                if (PriceChangeTemplst.Count > 0) { }
                else
                { PriceChangeTemplst.Add(priceChangeTemp); }


                return PriceChangeTemplst.OrderBy(x => x.LineNo).ToList();
            }
        }

        public DataTable GetPriceChangeDetailDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.InvProductPriceChangeHeaders.Where("DocumentStatus = @0", 1);

                // Insert Conditions to query
                #region Conditions
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                        {
                            switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                            {
                                case "PriceBatchNo":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             select qr
                                      );

                                    break;
                                default:
                                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                    break;
                            }
                        }
                        else
                        {
                            if (reportConditionsDataStruct.ReportDataStruct.IsManualRecordFilter)
                            { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @0 ", reportConditionsDataStruct.ConditionFrom.Trim()); }
                            else
                            { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }
                        }

                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                        {
                            switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                            {
                                case "DepartmentID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join pm in context.InvProductMasters on jt.ProductID equals pm.InvProductMasterID
                                             join dp in context.InvDepartments.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on pm.DepartmentID equals dp.InvDepartmentID
                                             select qr
                                      );
                                    break;
                                case "CategoryID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join pm in context.InvProductMasters on jt.ProductID equals pm.InvProductMasterID
                                             join dp in context.InvCategories.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on pm.CategoryID equals dp.InvCategoryID
                                             select qr
                                      );
                                    break;
                                case "SubCategoryID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join pm in context.InvProductMasters on jt.ProductID equals pm.InvProductMasterID
                                             join dp in context.InvSubCategories.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on pm.SubCategoryID equals dp.InvSubCategoryID
                                             select qr
                                      );
                                    break;
                                case "SubCategory2ID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join pm in context.InvProductMasters on jt.ProductID equals pm.InvProductMasterID
                                             join dp in context.InvSubCategories2.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on pm.SubCategory2ID equals dp.InvSubCategory2ID
                                             select qr
                                      );
                                    break;
                                case "InvProductMasterID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join pm in context.InvProductMasters.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on jt.ProductID equals pm.InvProductMasterID
                                             select qr
                                      );
                                    break;
                                case "UnitOfMeasureID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetails on qr.InvProductPriceChangeHeaderID equals jt.InvProductPriceChangeHeaderID
                                             join uom in context.UnitOfMeasures.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on jt.UnitOfMeasureID equals uom.UnitOfMeasureID
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
                                default:
                                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                    break;
                            }
                        }
                        else
                        {
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                        }
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
                }
                #endregion

                #region dynamic select
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{ selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                #endregion

                //int aa = query.ToDataTable().Rows.Count;
                //DataTable nn2 = query.ToDataTable();

                var queryResult = (from qs in query.AsNoTracking()
                                   join pd in context.InvProductPriceChangeDetails on qs.InvProductPriceChangeHeaderID equals pd.InvProductPriceChangeHeaderID
                                   join pm in context.InvProductMasters on pd.ProductID equals  pm.InvProductMasterID 
                                   join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                                   join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                                   join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                                   join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
                                   join uom in context.UnitOfMeasures on pd.UnitOfMeasureID equals uom.UnitOfMeasureID
                                   join lt in context.Locations on qs.LocationID equals lt.LocationID
                                   select
                                       new
                                       {
                                           FieldString1 = lt.LocationCode + " " + lt.LocationName,
                                           FieldString2 = qs.DocumentNo,
                                           FieldString3 = DbFunctions.TruncateTime(qs.DocumentDate),
                                           FieldString4 = pd.BatchNo,
                                           FieldString5 = dp.DepartmentCode + " " + dp.DepartmentName,
                                           FieldString6 = ct.CategoryCode + " " + ct.CategoryName,
                                           FieldString7 = sct.SubCategoryCode + " " + sct.SubCategoryName,
                                           FieldString8 = sct2.SubCategory2Code + " " + sct2.SubCategory2Name,
                                           FieldString9 = pm.ProductCode,
                                           FieldString10 = pm.ProductName,
                                           FieldString11 = uom.UnitOfMeasureCode + " " + uom.UnitOfMeasureName,
                                           FieldString12 = qs.CreatedUser,
                                           FieldString13 = DbFunctions.TruncateTime(qs.EffectFromDate),
                                           FieldDecimal1 = pd.CurrentStock,
                                           FieldDecimal2 = pd.OldSellingPrice,
                                           FieldDecimal3 = pd.NewSellingPrice,
                                           FieldDecimal4 = pd.OldCostPrice,
                                           FieldDecimal5 = (pd.CurrentStock * pd.OldSellingPrice),
                                           FieldDecimal6 = (pd.CurrentStock * pd.NewSellingPrice),
                                           FieldDecimal7 = (pd.CurrentStock * pd.NewSellingPrice) - (pd.CurrentStock * pd.OldSellingPrice),
                                           FieldDecimal8 = (pd.NewSellingPrice - pd.OldSellingPrice)
                                       }); //.ToArray();

                //var yy = CompiledQuery.Compile<ERPDbContext, string, IQueryable>
                //    ((ctx, conditions) =>
                //     from cs in ctx.InvProductPriceChangeHeaders where cs.DocumentStatus == 1
                //         select cs) ;


                //DataTable nn = queryResult.ToDataTable();
                //int val = queryResult.ToDataTable().Rows.Count;

                DataTable dtQueryResult = new DataTable();

                if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy))
                {
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

                return dtQueryResult; //return queryResult.ToDataTable();
            }
        }

        public ArrayList GetSelectionDataLC(Common.ReportDataStruct reportDataStruct)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                IQueryable qryResulData = context.InvProductMasters
                                .Where("IsDelete = @0 ", false)
                                .OrderBy("" + reportDataStruct.DbColumnName.Trim() + "") //.Select("new(DocumentNo)")
                                .Select(reportDataStruct.DbColumnName.Trim());

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "DepartmentID":
                        qryResulData = qryResulData.Join(context.InvDepartments, "InvDepartmentID", "InvDepartmentID", "new(inner.DepartmentID)").
                                      OrderBy("DepartmentID").Select("DepartmentName");
                        break;
                    case "CategoryID":
                        qryResulData = qryResulData.Join(context.InvProductPriceChangeDetails, "CategoryID", "CategoryID", "new(inner.InvCategoryID)").
                                      OrderBy("InvCategoryID").Select("CategoryName");
                        break;
                    case "LocationID":
                        qryResulData = qryResulData.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationID)").
                                      OrderBy("LocationID").Select("LocationCode");
                        break;
                    case "ProductID":
                        qryResulData = qryResulData.Join(context.InvProductPriceChangeDetails, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.InvLoyaltyTransactionID)").
                                      OrderBy("InvLoyaltyTransactionID").Select("InvLoyaltyTransactionID");
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
        }

        public ArrayList GetSelectionDataSummary(Common.ReportDataStruct reportDataStruct)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                IQueryable qryResult = context.InvProductPriceChangeHeaders.Where("DocumentStatus == @0", 1);

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
                    case "InvProductMasterID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");

                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "DepartmentID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID", "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");

                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID", "new(inner.InvProductMasterID, inner.DepartmentID)")
                                             .OrderBy("DepartmentID")
                                             .GroupBy("new(DepartmentID)", "new(DepartmentID)")
                                             .Select("new(Key.DepartmentID)");
                        qryResult = qryResult.Join(context.InvDepartments, "DepartmentID", "InvDepartmentID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "CategoryID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.CategoryID)")
                                             .OrderBy("CategoryID")
                                             .GroupBy("new(CategoryID)", "new(CategoryID)")
                                             .Select("new(Key.CategoryID)");
                        qryResult = qryResult.Join(context.InvCategories, "CategoryID", "InvCategoryID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "SubCategoryID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.SubCategoryID)")
                                             .OrderBy("SubCategoryID")
                                             .GroupBy("new(SubCategoryID)", "new(SubCategoryID)")
                                             .Select("new(Key.SubCategoryID)");
                        qryResult = qryResult.Join(context.InvSubCategories, "SubCategoryID", "InvSubCategoryID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "SubCategory2ID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.SubCategory2ID)")
                                             .OrderBy("SubCategory2ID")
                                             .GroupBy("new(SubCategory2ID)", "new(SubCategory2ID)")
                                             .Select("new(Key.SubCategory2ID)");
                        qryResult = qryResult.Join(context.InvSubCategories2, "SubCategory2ID", "InvSubCategory2ID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "UnitOfMeasureID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.UnitOfMeasureID)")
                                             .OrderBy("UnitOfMeasureID")
                                             .GroupBy("new(UnitOfMeasureID)", "new(UnitOfMeasureID)")
                                             .Select("new(Key.UnitOfMeasureID)");
                        qryResult = qryResult.Join(context.UnitOfMeasures, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "PriceBatchNo":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetails, "InvProductPriceChangeHeaderID", "InvProductPriceChangeHeaderID",
                                                   "new(inner.BatchNo)")
                                             .OrderBy("BatchNo")
                                             .GroupBy("new(BatchNo)", "new(BatchNo)")
                                             .Select("(Key.BatchNo)");
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
                        if (!string.IsNullOrEmpty(item.ToString().Trim()))
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
                }
                return selectionDataList;
            }
        }

        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AutoGenerateInfo autoGenerateInfo;
                autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
                string getNewCode = string.Empty;
                long tempCode = 0;

                string preFix = string.Empty;

                bool isLocationCode = autoGenerateInfo.IsLocationCode;
                int codeLength = autoGenerateInfo.CodeLength;
                preFix = autoGenerateInfo.Prefix.Trim();

                if (isTemporytNo)
                {
                    if ((preFix.StartsWith("T")))
                    { preFix = "X"; }
                    else
                    { preFix = "T"; }
                }

                if (isLocationCode)
                { preFix = preFix + locationCode.Trim(); }

                if (isTemporytNo)
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });

                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
                else
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });

                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
                }

                getNewCode = (tempCode).ToString();
                getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
                return getNewCode.ToUpper();
            }
        }
        #endregion

    }
}
