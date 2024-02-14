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
using System.Data.Common;


namespace Service
{
    public class InvProductPriceChangeDamageService
    {
        private ERPDbContext context = new ERPDbContext();

        public List<InvProductBatchNoExpiaryDetail> GetAllBatchNos()
        {
            return context.InvProductBatchNoExpiaryDetails.GroupBy(b => b.BatchNo).Select(b => b.FirstOrDefault()).ToList();
        }

        public List<InvDamageType> GetAllDamageTypesList()
        {
            return context.InvDamageTypes .Where(l => l.IsDelete.Equals(false)).OrderBy(l => l.InvDamageTypeID).ToList();
        }

        public void UpdatePriceDtl(InvProductPriceChangeDetail productPriceChange)
        {
            context.InvProductBatchNoExpiaryDetails.Update(ps => ps.ProductID.Equals(productPriceChange.ProductID) && ps.LocationID.Equals(productPriceChange.LocationID), ps => new InvProductBatchNoExpiaryDetail { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewCostPrice });
            context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(productPriceChange.ProductID), pm => new InvProductMaster { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewSellingPrice });
            context.InvProductStockMasters.Update(ps => ps.ProductID.Equals(productPriceChange.ProductID), ps => new InvProductStockMaster { SellingPrice = productPriceChange.NewSellingPrice, CostPrice = productPriceChange.NewSellingPrice });
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
            List<string> PriceChangeList = context.InvProductPriceChangeDetailDamages.Where(c => c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
            return PriceChangeList.ToArray();
        }

        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
             pausedDocumentList = context.InvProductPriceChangeHeaderDamages.Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        public string[] GetPausedDocumentNumbers(DateTime EffDate)
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.InvProductPriceChangeHeaders.Where(po => po.DocumentStatus.Equals(0) && po.DocumentDate.Equals(EffDate)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        public InvProductPriceChangeDetail GetSavedDocumentDetailsByDocumentNumber4BarCode(string documentNo)
        {
            return context.InvProductPriceChangeDetails.Where(po => po.DocumentNo == documentNo.Trim()).FirstOrDefault();
        }

        public InvProductPriceChangeDetailDamage GetSavedDocumentDetailsByDocumentNumber(int documentID, string documentNo, int locationID)
        {
            return context.InvProductPriceChangeDetailDamages.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).OrderBy(a=>a.LineNo).FirstOrDefault();
        }

        public InvProductPriceChangeHeaderDamage GetPausedInvProductPriceChangeHeaderDamageByDocumentNo(int documentID, string documentNo)
        {
            return context.InvProductPriceChangeHeaderDamages.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        public List<PriceChangeTempDamage> GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            //return context.InvProductPriceChanges.Where(po => po.DocumentNo == documentNo.Trim()).FirstOrDefault();

            List<PriceChangeTempDamage> PriceChangeTempList = new List<PriceChangeTempDamage>();
            PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
            
            var pclst = (from pc in context.InvProductPriceChangeDetailDamages
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
                             pc.NewProductCode,
                             pc.NewProductID,
                             pc.NewProductName,
                             pc.CurrentStock,
                             pc.MRP,
                             pc.OldCostPrice,
                             pc.NewCostPrice,
                             pc.OldSellingPrice,
                             pc.NewSellingPrice,
                             pc.LocationID,
                             lc.LocationName,
                             pc.DamageType,
                         }
                         );

            int lineno = 1;

            foreach (var pcGrdlist in pclst)
            {
                priceChangeTempDamage = new PriceChangeTempDamage();
                priceChangeTempDamage. ProductID = pcGrdlist.ProductID;
                priceChangeTempDamage.ProductName = pcGrdlist.ProductName;
                priceChangeTempDamage.ProductCode =pcGrdlist.ProductCode;
                priceChangeTempDamage.UnitOfMeasureID =pcGrdlist.UnitOfMeasureID;
                priceChangeTempDamage.UnitOfMeasureName = pcGrdlist.UnitOfMeasureName;
                priceChangeTempDamage.NewProductID=pcGrdlist.NewProductID;
                priceChangeTempDamage.NewProductCode = pcGrdlist.NewProductCode;
                priceChangeTempDamage.NewProductName = pcGrdlist.NewProductName;
                priceChangeTempDamage.Qty=pcGrdlist.CurrentStock;
                priceChangeTempDamage.MRP=pcGrdlist.MRP;
                priceChangeTempDamage.CostPrice=pcGrdlist.OldCostPrice;
                priceChangeTempDamage.NewCostPrice=pcGrdlist.NewCostPrice;
                priceChangeTempDamage.SellingPrice=pcGrdlist.OldSellingPrice;
                priceChangeTempDamage.NewSellingPrice = pcGrdlist.NewSellingPrice;
                priceChangeTempDamage.LocationId=pcGrdlist.LocationID;
                priceChangeTempDamage.LocationName=pcGrdlist.LocationName;
                priceChangeTempDamage.DamageType = pcGrdlist.DamageType;
                priceChangeTempDamage.LineNo=lineno;
                lineno++;
                PriceChangeTempList.Add(priceChangeTempDamage);
            }

            return PriceChangeTempList.OrderBy(pd => pd.LineNo).ToList();


        }

        public InvProductBatchNoExpiaryDetail GetProductBatchNo(long productId)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(c => c.ProductID == productId && c.IsDelete == false).FirstOrDefault();
        }

        public List<InvProductBatchNoTemp> getBatchNoDetail(InvProductMaster invProductMaster)
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

            return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public DataSet GetPriceChangeTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            DataSet dsReportData = new DataSet();
            DataTable dtPriceChange = new DataTable();
            DataTable dtPriceChangeProdcutsDetails = new DataTable();
            DataTable dtProdcutsExtendedPropsColumns = new DataTable();
            dtProdcutsExtendedPropsColumns.Columns.Add("ColumnName", typeof(string));

            #region Price Change Data
            dtPriceChange = (
                        from ph in context.InvProductPriceChangeHeaderDamages
                        join pd in context.InvProductPriceChangeDetailDamages on ph.InvProductPriceChangeHeaderDamageID equals pd.InvProductPriceChangeDetailDamageID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        //join tx in context.Taxes on s.t
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
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

            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
            //GetProductExtendedPropertyValuesByProductID
            var PriceChangeProdcutsDetails = (
                        from ph in context.InvProductPriceChangeHeaderDamages
                        join pd in context.InvProductPriceChangeDetailDamages on ph.InvProductPriceChangeHeaderDamageID equals pd.InvProductPriceChangeHeaderDamageID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                        join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                        join sc in context.InvSubCategories on pm.SubCategoryID equals sc.InvSubCategoryID
                        join sc2 in context.InvSubCategories2 on pm.SubCategory2ID equals sc2.InvSubCategory2ID
                        join l in context.Locations on pd.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
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
                                FieldString8 = pd.NewProductCode,
                                FieldString9 = pd.NewProductName,
                                FieldString10 = ph.TogNO,
                                FieldString11 = sc2.SubCategory2Name,
                                FieldString12 = "",
                                FieldString13 = "",
                                FieldString14 = "",
                                FieldString15 = pd.OldSellingPrice,
                                FieldString16 = pd.NewSellingPrice,
                                FieldString17 = pd.CurrentStock,
                                FieldString18 = "",
                                FieldString19 = "",
                                FieldString20 = "",
                                FieldString21 = "",
                                FieldString22 ="",
                                FieldString23 = "",
                                FieldString24 = l.LocationName,
                                FieldString25 =  "PRICE DECREAS NON BATCH",
                                FieldString26 = "",
                            }).AsEnumerable();

            dtPriceChangeProdcutsDetails = PriceChangeProdcutsDetails.ToDataTable();

            //int lastUpdatedColumnHeader = 12;
            //foreach (DataRow drProduct in dtPriceChangeProdcutsDetails.Rows)
            //{
            //    List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
            //    productExPropList = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(long.Parse(drProduct["FieldString12"].ToString()));

            //    // Set Column header
            //    for (int i = 0; i < productExPropList.Count; i++)
            //    {
            //        if (!dtPriceChangeProdcutsDetails.Columns.Contains(productExPropList[i].ExtendedPropertyName.ToString().Trim()))
            //        {
            //            if (i < 8) // Because report has ony 14 fields for product extended properties
            //            {
            //                dtPriceChangeProdcutsDetails.Columns[lastUpdatedColumnHeader].ColumnName = productExPropList[i].ExtendedPropertyName.ToString().Trim();
            //                dtProdcutsExtendedPropsColumns.Rows.Add(productExPropList[i].ExtendedPropertyName.ToString().Trim());
            //                lastUpdatedColumnHeader++;
            //            }
            //        }
            //        if (i < 8) // Because report has ony 14 fields for product extended properties
            //        {
            //            drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] = productExPropList[i].ValueData.ToString().Trim();
            //        }
            //    }
            //}

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

        public List<InvBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo, int locationID, int documentID)
        {
            List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();

            var barcodeDetailTemp = (from pm in context.InvProductMasters
                                     join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                     join pd in context.InvProductPriceChangeDetailDamages on psm.ProductID equals pd.NewProductID
                                     join um in context.UnitOfMeasures on psm.UnitOfMeasureID equals um.UnitOfMeasureID
                                     join sc in context.Suppliers on pm.SupplierID equals sc.SupplierID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     //&& pd.LocationID == pd.LocationID
                                     && psm.LocationID == locationID
                                     && pd.DocumentNo == documentNo

                                     //??????
                                     && pd.DocumentNo == psm.BatchNo

                                     select new
                                     {
                                         pm.InvProductMasterID,
                                         pm.ProductCode,
                                         pm.ProductName,
                                         psm.BatchNo,
                                         psm.BarCode,
                                         Qty = pd.CurrentStock,
                                         Stock = psm.Qty,
                                         CostPrice=pd.NewCostPrice,
                                         SellingPrice=pd.NewSellingPrice,
                                         OldCostPrice = pd.OldCostPrice,
                                         OldSellingPrice = pd.OldSellingPrice,
                                         UnitOfMeasureID=um.UnitOfMeasureID,
                                         UnitOfMeasureName=um.UnitOfMeasureName,
                                         LineNo =0,
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

        public List<PriceChangeTempDamage>GetSavedTogByDocumentNumber(string documentNo)
        {

            List<PriceChangeTempDamage> PriceChangeTempDamageList = new List<PriceChangeTempDamage>();
            PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();

               var qry = (from tog in context.InvTransferNoteDetails
                          join th in context.InvTransferNoteHeaders on tog.TransferNoteHeaderID equals th.InvTransferNoteHeaderID
                          join uom in context.UnitOfMeasures on tog.UnitOfMeasureID equals uom.UnitOfMeasureID
                          join l in context.Locations on tog.LocationID equals l.LocationID
                          join pm in context.InvProductMasters on tog.ProductID equals pm.InvProductMasterID
                          where tog.DocumentNo == documentNo && th.IsDelete == false
					       select new
					       {
						       tog.ProductID,
						       pm.ProductCode,
						       pm.ProductName,
						       uom.UnitOfMeasureID,
						       uom.UnitOfMeasureName,
						       tog.Qty,
						       tog.CostPrice,
						       tog.SellingPrice,
						       tog.LocationID,
						       l.LocationName
					       }).ToArray().OrderBy(p=>p.ProductID);

            int lineno =0;
            foreach (var pcGrdlist in qry)
            {
                lineno++;
                priceChangeTempDamage = new PriceChangeTempDamage();
                priceChangeTempDamage.ProductID = pcGrdlist.ProductID;
                priceChangeTempDamage.ProductName = pcGrdlist.ProductName;
                priceChangeTempDamage.ProductCode = pcGrdlist.ProductCode;
                priceChangeTempDamage.UnitOfMeasureID = pcGrdlist.UnitOfMeasureID;
                priceChangeTempDamage.UnitOfMeasureName = pcGrdlist.UnitOfMeasureName;
                priceChangeTempDamage.NewProductID = 0;
                priceChangeTempDamage.NewProductCode = "";
                priceChangeTempDamage.NewProductName = "";
                priceChangeTempDamage.Qty = pcGrdlist.Qty;
                priceChangeTempDamage.MRP = 0;
                priceChangeTempDamage.CostPrice = pcGrdlist.CostPrice;
                priceChangeTempDamage.NewCostPrice = pcGrdlist.CostPrice;
                priceChangeTempDamage.SellingPrice = pcGrdlist.SellingPrice;
                priceChangeTempDamage.NewSellingPrice = 0;
                priceChangeTempDamage.LocationId = pcGrdlist.LocationID;
                priceChangeTempDamage.LocationName = pcGrdlist.LocationName;
                priceChangeTempDamage.LineNo = lineno;
                PriceChangeTempDamageList.Add(priceChangeTempDamage);
            }
            return PriceChangeTempDamageList.OrderBy(pd => pd.LineNo).ToList();

 

           
        }

        public List<PriceChangeTempDamage> GetDeletePriceChangeTemp(List<PriceChangeTempDamage> PriceChangeTempDamageList, PriceChangeTempDamage priceChangeTempDamage)
        {
            PriceChangeTempDamage priceChangeTempDamagex = new PriceChangeTempDamage();

            priceChangeTempDamagex = PriceChangeTempDamageList.Where(p => p.LineNo == priceChangeTempDamage.LineNo).FirstOrDefault();
            long removedLineNo = 0;
            if (priceChangeTempDamagex != null)
            {
                PriceChangeTempDamageList.Remove(priceChangeTempDamagex);
                removedLineNo = priceChangeTempDamage.LineNo;
            }

            PriceChangeTempDamageList.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            return PriceChangeTempDamageList.OrderBy(pd => pd.LineNo).ToList();
        }

        //public List<PriceChangeTempDamage> getLineDtPriceChange(InvProductMaster invProductMaster, long unitOfMeasure)
        //{
        //    List<PriceChangeTempDamage> invProductBatchNoTempList = new List<PriceChangeTempDamage>();

        //    List<PriceChangeTempDamage> invProductBatchNoDetail = new List<PriceChangeTempDamage>();

        //    invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.UnitOfMeasureID.Equals(unitOfMeasure)).ToList();  // && ps.BalanceQty > 0
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
                                  FieldString1= qs.CreatedDate,
                                  FieldString2= de.DepartmentName,
                                  FieldString3= ca.CategoryName,
                                  FieldString4= pm.ProductCode,
                                  FieldString5= pm.ProductName,
                                  FieldDecimal7= qs.NewCostPrice,
                                  FieldDecimal6= qs.NewSellingPrice
                               }).ToArray();
 

            return queryResult.ToDataTable();
        }

        public List<PriceChangeTempDamage> getUpdatePriceChangeTemp(List<PriceChangeTempDamage> priceChangeGridList, PriceChangeTempDamage priceChangeTempDamagex,long ProductId, decimal newSellingPrice)
        {
           
            PriceChangeTempDamage priceChangeTempDamageDelete = new PriceChangeTempDamage();

            priceChangeTempDamageDelete = priceChangeGridList.Where(p => p.LineNo == priceChangeTempDamagex.LineNo).FirstOrDefault();
            long removedLineNo = 0;
            if (priceChangeTempDamageDelete != null)
            {
                //if (priceChangeGridList.Count == 0)
                //{
                //    priceChangeTempDamagex.LineNo = 0;
                //}
                //else
                //{
                //    priceChangeTempDamagex.LineNo = priceChangeGridList.Max(s => s.LineNo) + 1;
                //}
                priceChangeGridList.Remove(priceChangeTempDamageDelete);
                priceChangeTempDamagex.LineNo = priceChangeTempDamageDelete.LineNo;

            }
            else
            {
                priceChangeGridList.Remove(priceChangeTempDamageDelete);
                if (priceChangeTempDamagex.LineNo == 0)
                {
                    priceChangeTempDamagex.LineNo = priceChangeGridList.Max(s => s.LineNo) + 1;
                }
            }

            priceChangeGridList.Add(priceChangeTempDamagex);

            return priceChangeGridList.OrderBy(pd => pd.LineNo).ToList();
  
             
        }


        public List<PriceChangeTempDamage> getUpdatePriceDiscount(List<PriceChangeTempDamage> priceChangeGridList, decimal DiscountSellingPrice, string ProductCode)
        {
            PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
            PriceChangeTempDamage priceChangeTempLine = new PriceChangeTempDamage();
            InvProductMaster invProductMaster = new InvProductMaster();
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();
            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
            Location location = new Location();
            LocationService locationService = new LocationService();
            InvProductMasterService invProductMasterService = new InvProductMasterService();
            InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();
            decimal discountCostAmount=0;
            decimal discountSellingAmount=0;
            List<PriceChangeTempDamage> priceChangnewGridList = new List<PriceChangeTempDamage>();
            var lstitem = priceChangeGridList;

            foreach (var item in lstitem)
            {
                priceChangeTempDamage = new PriceChangeTempDamage();
                priceChangeTempDamage.ProductID = item.ProductID;
                invProductMaster = invProductMasterService.GetProductDetailsByID(item.ProductID);
                unitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(invProductMaster.UnitOfMeasureID);
                priceChangeTempDamage.ProductName = invProductMaster.ProductName;
                priceChangeTempDamage.ProductCode = invProductMaster.ProductCode;
                priceChangeTempDamage.UnitOfMeasureID = unitOfMeasure.UnitOfMeasureID;
                priceChangeTempDamage.UnitOfMeasureName = unitOfMeasure.UnitOfMeasureName;
                priceChangeTempDamage.SellingPrice = invProductMaster.SellingPrice;
                priceChangeTempDamage.MRP = invProductMaster.MinimumPrice;
                location = locationService.GetLocationsByID(Common.ConvertStringToInt(item.LocationId.ToString()));
                priceChangeTempDamage.LocationId = location.LocationID;
                priceChangeTempDamage.LocationName = location.LocationName;
                priceChangeTempDamage.CostPrice = invProductMaster.CostPrice;
                invProductMaster = invProductMasterService.GetProductsByCode(ProductCode);
                priceChangeTempDamage.NewProductID = invProductMaster.InvProductMasterID;
                priceChangeTempDamage.NewProductCode = invProductMaster.ProductCode;
                priceChangeTempDamage.NewProductName = invProductMaster.ProductName;
                priceChangeTempDamage.Qty = item.Qty;
                discountCostAmount =   0;
                //discountSellingAmount =  item.SellingPrice - DiscountSellingPrice ;
                
                    priceChangeTempDamage.NewCostPrice = item.CostPrice;
                    priceChangeTempDamage.NewSellingPrice = DiscountSellingPrice;
                
                priceChangeTempDamage.LineNo = item.LineNo;

                priceChangnewGridList.Add(priceChangeTempDamage);

                //if (priceChangeGridList != null)
                //    priceChangeTempLine = priceChangeGridList.Where(p => p.ProductID == priceChangeTempDamage.ProductID && p.LineNo == item.LineNo && p.LocationId == priceChangeTempDamage.LocationId && p.BatchNo == priceChangeTempDamage.BatchNo && p.UnitOfMeasureID == priceChangeTempDamage.UnitOfMeasureID).FirstOrDefault();
                //else
                //    priceChangeTempLine = null;
                //if (priceChangeTempLine != null)
                //{
                //    //priceChangeGridList.Remove(priceChangeTempLine);
                //    priceChangeTempDamage.LineNo = priceChangeTempLine.LineNo;
                //    //priceChangeGridList.Add(priceChangeTempDamage);
                //    priceChangnewGridList
                //}
                //else
                //{
                //    if (priceChangeGridList != null)
                //    {
                //        if (priceChangeGridList.Count != 0)
                //        {
                //            priceChangeTempDamage.LineNo = priceChangeGridList.Max(s => s.LineNo) + 1;
                //        }
                //    }

                //    priceChangeGridList.Add(priceChangeTempDamage);
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
        
        public bool Save(InvProductPriceChangeHeaderDamage invProductPriceChangeHeaderDamage, List<PriceChangeTempDamage> priceChangeTempDamagelst, out string newDocumentNo, string formName = "")
        {

            using (TransactionScope transaction = new TransactionScope())
            {

                if (invProductPriceChangeHeaderDamage.DocumentStatus.Equals(1))
                {
                    InvProductPriceChangeDamageService invProductPriceChangeDamageService = new InvProductPriceChangeDamageService();
                    LocationService locationService = new LocationService();
                    invProductPriceChangeHeaderDamage.DocumentNo = invProductPriceChangeDamageService.GetDocumentNo(formName, invProductPriceChangeHeaderDamage.LocationID, locationService.GetLocationsByID(invProductPriceChangeHeaderDamage.LocationID).LocationCode, invProductPriceChangeHeaderDamage.DocumentID, false).Trim();
                }

                newDocumentNo = invProductPriceChangeHeaderDamage.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (invProductPriceChangeHeaderDamage.InvProductPriceChangeHeaderDamageID.Equals(0))
                    context.InvProductPriceChangeHeaderDamages.Add(invProductPriceChangeHeaderDamage);
                else
                    context.Entry(invProductPriceChangeHeaderDamage).State = EntityState.Modified;
                context.SaveChanges();

                context.Set<InvProductPriceChangeDetailDamage>().Delete(context.InvProductPriceChangeDetailDamages.Where(pod => pod.InvProductPriceChangeHeaderDamageID.Equals(invProductPriceChangeHeaderDamage.InvProductPriceChangeHeaderDamageID) && pod.LocationID.Equals(invProductPriceChangeHeaderDamage.LocationID) && pod.DocumentID.Equals(invProductPriceChangeHeaderDamage.DocumentID)).AsQueryable());
                context.SaveChanges();

                var priceChangeSaveQuery = (from pd in priceChangeTempDamagelst
                                            select new
                                            {
                                                LocationID = invProductPriceChangeHeaderDamage.LocationID,
                                                pd.ProductID,
                                                pd.ProductCode,
                                                pd.ProductName,
                                                DocumentNo=invProductPriceChangeHeaderDamage.DocumentNo,
                                                DocumentDate=Common.ConvertDateTimeToDate(DateTime.Now),
                                                OldCostPrice=pd.CostPrice,
                                                pd.NewCostPrice,
                                                OldSellingPrice=pd.SellingPrice,
                                                pd.NewSellingPrice,
                                                CurrentStock=pd.Qty,
                                                pd.UnitOfMeasureID,
                                                pd.NewProductID,
                                                pd.NewProductCode,
                                                pd.NewProductName,
                                                EffectFromDate = invProductPriceChangeHeaderDamage.EffectFromDate,
                                                Percentage= 0,
                                                pd.MRP,
                                                IsDelete=false,
                                                DocumentID=invProductPriceChangeHeaderDamage.DocumentID,
                                                GroupOfCompanyID=Common.LoggedCompanyID,
                                                CreatedUser=Common.LoggedUser,
                                                CreatedDate = invProductPriceChangeHeaderDamage.DocumentDate,
                                                ModifiedUser=Common.LoggedUser,
                                                ModifiedDate = invProductPriceChangeHeaderDamage.DocumentDate,
                                                DataTransfer=0,
                                                pd.LineNo,
                                                invProductPriceChangeHeaderDamage.InvProductPriceChangeHeaderDamageID,
                                                pd.DamageType,
     

                                            }).ToList();
                //CommonService.MergeSqlTo(Common.LINQToDataTable(priceChangeSaveQuery), "InvProductPriceChangeDetailDamage");
                DataTable dtx = new DataTable();
                dtx = priceChangeSaveQuery.ToDataTable();
                dtx.TableName = "InvProductPriceChangeDetailDamage";
                //CommonService.BulkInsert(dtx, "InvProductPriceChangeDetail");

                string MergeSql = @"  
                         MERGE INTO InvProductPriceChangeDetailDamage AS Target
                            USING #TEMP AS Source
                            ON Target.[LocationID] = Source.[LocationID]
		                          AND Target.[DocumentID] = Source.[DocumentID] AND Target.[DocumentNo] = Source.[DocumentNo] and
		                          Target.[LineNo] = Source.[LineNo]
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
                                            Target.[NewProductID] = Source.[NewProductID],
                                            Target.[NewProductCode] = Source.[NewProductCode],
                                            Target.[NewProductName] = Source.[NewProductName],
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
                                            Target.[InvProductPriceChangeHeaderDamageID] = Source.[InvProductPriceChangeHeaderDamageID],
                                            Target.[DamageType] = Source.[DamageType]
                            WHEN NOT MATCHED 
                                THEN INSERT ( [LocationID], [ProductID], [ProductCode], [ProductName],
                                              [DocumentNo], [DocumentDate], [OldCostPrice],
                                              [NewCostPrice], [OldSellingPrice], [NewSellingPrice],
                                              [CurrentStock], [UnitOfMeasureID], [NewProductID],
                                              [NewProductCode], [NewProductName], [EffectFromDate],
                                              [Percentage], [MRP], [IsDelete], [DocumentID],
                                              [GroupOfCompanyID], [CreatedUser], [CreatedDate],
                                              [ModifiedUser], [ModifiedDate], [DataTransfer], [LineNo],
                                              [InvProductPriceChangeHeaderDamageID],[DamageType])
                                  VALUES    ( Source.[LocationID], Source.[ProductID],
                                              Source.[ProductCode], Source.[ProductName],
                                              Source.[DocumentNo], Source.[DocumentDate],
                                              Source.[OldCostPrice], Source.[NewCostPrice],
                                              Source.[OldSellingPrice], Source.[NewSellingPrice],
                                              Source.[CurrentStock], Source.[UnitOfMeasureID],
                                              Source.[NewProductID], Source.[NewProductCode],
                                              Source.[NewProductName], Source.[EffectFromDate],
                                              Source.[Percentage], Source.[MRP], Source.[IsDelete],
                                              Source.[DocumentID], Source.[GroupOfCompanyID],
                                              Source.[CreatedUser], Source.[CreatedDate],
                                              Source.[ModifiedUser], Source.[ModifiedDate],
                                              Source.[DataTransfer], Source.[LineNo],
                                              Source.[InvProductPriceChangeHeaderDamageID],Source.[DamageType] );";


                CommonService.BulkInsert(priceChangeSaveQuery.ToDataTable(), "InvProductPriceChangeDetailDamage", MergeSql);
                context.SaveChanges();

                if (invProductPriceChangeHeaderDamage.DocumentStatus.Equals(1))
                {

                    var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@InvProductPriceChangeHeaderDamageID", Value=invProductPriceChangeHeaderDamage.InvProductPriceChangeHeaderDamageID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invProductPriceChangeHeaderDamage.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invProductPriceChangeHeaderDamage.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invProductPriceChangeHeaderDamage.DocumentStatus},
                    };

                    if (CommonService.ExecuteStoredProcedure("spInvPriceChangeDamageSave", parameter))
                    {
                        transaction.Complete();
                        return true;
                    }
                    else
                    { return false; }
                }
                else
                {
                    transaction.Complete();
                    return true;
                }
                 
            }
 

        }

        public PriceChangeTempDamage getPriceChangeTemp(List<PriceChangeTempDamage> priceChangeTempDamagelst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID,long lineno)
        {
            PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
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

            foreach (var tempProduct in PCTemp)
            {

                //if (!unitOfMeasureID.Equals(0))
                //    priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                //else
                    priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.LineNo == lineno).FirstOrDefault();

                if (priceChangeTempDamage == null)
                {
                    priceChangeTempDamage = new PriceChangeTempDamage();
                    priceChangeTempDamage.ProductID = tempProduct.InvProductMasterID;
                    priceChangeTempDamage.ProductCode = tempProduct.ProductCode;
                    priceChangeTempDamage.ProductName = tempProduct.ProductName;
                    priceChangeTempDamage.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    priceChangeTempDamage.CostPrice = tempProduct.CostPrice;
                    priceChangeTempDamage.SellingPrice = tempProduct.SellingPrice;
                    priceChangeTempDamage.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                    priceChangeTempDamage.MRP = tempProduct.MaximumPrice;
                    priceChangeTempDamage.LocationId = tempProduct.LocationID;
                    priceChangeTempDamage.LocationName = tempProduct.LocationName;
                    priceChangeTempDamage.Qty = tempProduct.Qty;
                }
                

            }
            priceChangeTempDamage.LineNo = lineno;
            return priceChangeTempDamage;
        }

        public List<PriceChangeTempDamage> getPriceChangeTempList(List<PriceChangeTempDamage> priceChangeTempDamagelst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID)
        {
            PriceChangeTempDamage priceChangeTempDamage = new PriceChangeTempDamage();
            //List<PriceChangeTempDamage> priceChangeTempDamagelst = new List<PriceChangeTempDamage>();
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

            foreach (var tempProduct in PCTemp)
            {

                if (!unitOfMeasureID.Equals(0))
                    priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                if (priceChangeTempDamage == null)
                {
                    priceChangeTempDamage = new PriceChangeTempDamage();
                    priceChangeTempDamage.ProductID = tempProduct.InvProductMasterID;
                    priceChangeTempDamage.ProductCode = tempProduct.ProductCode;
                    priceChangeTempDamage.ProductName = tempProduct.ProductName;
                    priceChangeTempDamage.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    priceChangeTempDamage.CostPrice = tempProduct.CostPrice;
                    priceChangeTempDamage.SellingPrice = tempProduct.SellingPrice;
                    priceChangeTempDamage.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                    priceChangeTempDamage.MRP = tempProduct.MaximumPrice;
                    priceChangeTempDamage.LocationId = tempProduct.LocationID;
                    priceChangeTempDamage.LocationName = tempProduct.LocationName;

                    priceChangeTempDamage.Qty = tempProduct.Qty;
                    priceChangeTempDamagelst.Add(priceChangeTempDamage);
                }

            }

            return priceChangeTempDamagelst.OrderBy(x => x.LineNo).ToList();
        }

        public List<PriceChangeTemp> getPriceChangeTempList(List<PriceChangeTemp> priceChangeTempDamagelst, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID,int lineNo)
        {
            PriceChangeTemp priceChangeTempDamage = new PriceChangeTemp();
            List<PriceChangeTemp> priceChangeTempDamageList = new List<PriceChangeTemp>();
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
                priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.LineNo.Equals(lineNo) && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
            else
                priceChangeTempDamage = priceChangeTempDamagelst.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.LineNo == lineNo).FirstOrDefault();


            foreach (var tempProduct in PCTemp)
            {

                if (priceChangeTempDamage == null)
                {
                    priceChangeTempDamage = new PriceChangeTemp();
                    priceChangeTempDamage.ProductID = tempProduct.InvProductMasterID;
                    priceChangeTempDamage.ProductCode = tempProduct.ProductCode;
                    priceChangeTempDamage.ProductName = tempProduct.ProductName;
                    priceChangeTempDamage.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    priceChangeTempDamage.CostPrice = tempProduct.CostPrice;
                    priceChangeTempDamage.SellingPrice = tempProduct.SellingPrice;
                    priceChangeTempDamage.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                    priceChangeTempDamage.MRP = tempProduct.MaximumPrice;
                    priceChangeTempDamage.LocationId = tempProduct.LocationID;
                    priceChangeTempDamage.LocationName = tempProduct.LocationName;
                    priceChangeTempDamage.LineNo = lineNo;
                    priceChangeTempDamage.Qty = tempProduct.Qty;
                    priceChangeTempDamageList.Add(priceChangeTempDamage);
                }
                //else
                //{
                //    priceChangeTempDamagelst.Add(priceChangeTempDamage);
                //}

            }

            if (priceChangeTempDamageList.Count > 0) { }
            else
            { priceChangeTempDamageList.Add(priceChangeTempDamage); }
         

            return priceChangeTempDamageList.OrderBy(x => x.LineNo).ToList();
        }

         

        public DataTable GetPriceChangeDamageDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = context.InvProductPriceChangeHeaderDamages.Where("DocumentStatus = @0", 1);

                // Insert Conditions to query
                #region Conditions
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                  
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (reportConditionsDataStruct.ConditionFrom.Trim()), (reportConditionsDataStruct.ConditionTo.Trim())); }

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
                                case "LocationID":
                                    query = (from qr in query
                                             join l in context.Locations.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.LocationID equals l.LocationID
                                                 select qr
                                      );
                                    break;

                                case "DepartmentID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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
                                case "SupplierID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
                                             join pm in context.InvProductMasters on jt.ProductID equals pm.InvProductMasterID
                                             join dp in context.Suppliers.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on pm.SupplierID equals dp.SupplierID
                                             select qr
                                      );
                                    break;
     
                                case "DamageType":
                                case "CreatedUser":
                                case "NewProductName":
                                case "NewProductID":
                                    query = (from qr in query
                                             join dp in context.InvProductPriceChangeDetailDamages.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.InvProductPriceChangeHeaderDamageID equals dp.InvProductPriceChangeHeaderDamageID
                                             select qr
                                      );
                                    break;
                                 
                                case "InvProductMasterID":
                                    query = (from qr in query
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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
                                             join jt in context.InvProductPriceChangeDetailDamages on qr.InvProductPriceChangeHeaderDamageID equals jt.InvProductPriceChangeHeaderDamageID
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

                var queryResult = (from qs in query
                                   join pd in context.InvProductPriceChangeDetailDamages on qs.InvProductPriceChangeHeaderDamageID equals pd.InvProductPriceChangeHeaderDamageID
                                   join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                   join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                                   join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                                   join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                                   join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
                                   join lt in context.Locations on qs.LocationID equals lt.LocationID
                                   join sup in context.Suppliers on pm.SupplierID equals sup.SupplierID
                                   join tog in context.InvTransferNoteHeaders on qs.TogNO equals tog.DocumentNo
                                   join l in context.Locations on tog.LocationID equals l.LocationID
                                   select
                                       new
                                       {
                                           FieldString1 = lt.LocationCode + " " + lt.LocationName,
                                           FieldString2 = qs.DocumentNo,
                                           FieldString3 = qs.DocumentDate,
                                           FieldString4 = dp.DepartmentCode + " " + dp.DepartmentName,
                                           FieldString5 = ct.CategoryCode + " " + ct.CategoryName,
                                           FieldString6 = sct.SubCategoryCode + " " + sct.SubCategoryName,
                                           FieldString7 = sct2.SubCategory2Code + " " + sct2.SubCategory2Name,
                                           FieldString8 = pm.ProductCode,
                                           FieldString9 = pm.ProductName,
                                           FieldString10 = pd.NewProductCode,
                                           FieldString11 = pd.NewProductName,
                                           FieldString12 = qs.CreatedUser,
                                           FieldDecima13 = pd.DamageType,
                                           FieldDecima14 = l.LocationName,
                                           FieldDecima15 = tog.DocumentNo,
                                           FieldDecima16 = sup.SupplierName,
                                           FieldDecimal7 = pd.CurrentStock,
                                           FieldDecimal8 = pd.OldSellingPrice,
                                           FieldDecimal9 = pd.NewSellingPrice,
                                           FieldDecima20 = pd.OldCostPrice,
                                           FieldDecima21 = (pd.CurrentStock * pd.OldSellingPrice),
                                           FieldDecima22 = (pd.CurrentStock * pd.NewSellingPrice),
                                           FieldDecima23 = (pd.CurrentStock * pd.NewSellingPrice) - (pd.CurrentStock * pd.OldSellingPrice),
                                           FieldDecima24 = (pd.NewSellingPrice - pd.OldSellingPrice)
                                          
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
                IQueryable qryResult = context.InvProductPriceChangeHeaderDamages.Where("DocumentStatus == @0", 1);

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
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");

                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
 

                    case "NewProductID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.NewProductCode)")
                                             .OrderBy("NewProductCode")
                                             .GroupBy("new(NewProductCode)", "new(NewProductCode)")
                                             .Select("Key.NewProductCode");

                        //qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                        //                     .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "NewProductName":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.NewProductName)")
                                             .OrderBy("NewProductName")
                                             .GroupBy("new(NewProductName)", "new(NewProductName)")
                                             .Select("Key.NewProductName");

                        //qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                        //                     .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "DepartmentID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID", "new(inner.ProductID)")
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
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
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
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
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
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
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
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
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
                    case "SupplierID":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.ProductID)")
                                             .OrderBy("ProductID")
                                             .GroupBy("new(ProductID)", "new(ProductID)")
                                             .Select("new(Key.ProductID)");
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.SupplierID)")
                                             .OrderBy("SupplierID")
                                             .GroupBy("new(SupplierID)", "new(SupplierID)")
                                             .Select("new(Key.SupplierID)");
                        qryResult = qryResult.Join(context.Suppliers, "SupplierID", "SupplierID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("" + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;


                    case "PriceBatchNo":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.BatchNo)")
                                             .OrderBy("BatchNo")
                                             .GroupBy("new(BatchNo)", "new(BatchNo)")
                                             .Select("(Key.BatchNo)");
                        break;
                    case "DamageType":
                        qryResult = qryResult.Join(context.InvProductPriceChangeDetailDamages, "InvProductPriceChangeHeaderDamageID", "InvProductPriceChangeHeaderDamageID",
                                                   "new(inner.DamageType)")
                                             .OrderBy("DamageType")
                                             .GroupBy("new(DamageType)", "new(DamageType)")
                                             .Select("Key.DamageType");
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
                        if (!object.Equals(item,null))
                        {
                            if (!string.IsNullOrEmpty(item.ToString().Trim()))
                            { selectionDataList.Add(item.ToString().Trim()); }
                        }
                    }
                }
                return selectionDataList;
            }
        }



        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
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
        #endregion

    }
}
