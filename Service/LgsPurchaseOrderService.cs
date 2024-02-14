using System;
using System.Collections.Generic;
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
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by - Nuwan
    /// </summary>
    public class LgsPurchaseOrderService
    {
        ERPDbContext context = new ERPDbContext();
        public static bool poIsMandatory;
        int generateBatch; // 1-yes  2-no 

        #region Methods

        /// <summary>
        /// Save purchase order
        /// </summary>
        /// <param name="lgsPurchaseOrderHeader"></param>
        public void SavePurchaseOrder(LgsPurchaseOrderHeader lgsPurchaseOrderHeader)
        {
            context.LgsPurchaseOrderHeaders.Add(lgsPurchaseOrderHeader);
            context.SaveChanges();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsPurchaseOrderHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        
        public LgsPurchaseOrderHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsPurchaseOrderHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public LgsPurchaseOrderHeader GetDocumentDetailsByDocumentNumber(string pausedDocumentNo) 
        {
            return context.LgsPurchaseOrderHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim()).FirstOrDefault();
        }

        public LgsPurchaseOrderHeader GetLgsPurchaseOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsPurchaseOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.LgsPurchaseOrderHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        //public string[] GetAllDocumentNumbersToGRN(int locationID)
        //{
        //    List<string> documentNoList = context.InvPurchaseOrderHeaders.Where(q => q.LocationID == locationID && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
        //    return documentNoList.ToArray();
        //}

        public string[] GetAllDocumentNumbersToGRN(int locationID)
        {
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.LgsPurchaseOrderHeaders
                       join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                       where ph.LocationID == locationID && pd.BalanceQty != 0
                       && ph.ExpiryDate > DateTime.Now
                       group pd by new
                       {
                           ph.DocumentNo,
                           pd.BalanceQty,
                       } into poGroup

                       select new
                       {
                           DocumentNo = poGroup.Key.DocumentNo,
                           BalanceQty = poGroup.Sum(x => x.BalanceQty),
                       }).ToArray();

            foreach (var temp in sql)
            {
                documentNoList = sql.Where(a => a.BalanceQty > 0).Select(a => a.DocumentNo).ToList();
            }

            return documentNoList.ToArray();
        }

        public DataTable GetAllPODocumentNumbersToGRN(int locationID) 
        {
            var sql = (from ph in context.LgsPurchaseOrderHeaders
                       join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                       join s in context.LgsSuppliers on ph.LgsSupplierID equals s.LgsSupplierID
                       where ph.LocationID == locationID && pd.BalanceQty != 0
                       && ph.DocumentStatus == 1
                       && ph.ExpiryDate > DateTime.Now
                       group pd by new
                       {
                           ph.DocumentNo,
                           s.SupplierCode,
                           s.SupplierName
                       } into poGroup

                       select new
                       {
                           DocumentNo = poGroup.Key.DocumentNo,
                           SupplierCode = poGroup.Key.SupplierCode,
                           SupplierName = poGroup.Key.SupplierName
                       }).ToArray();

            return sql.ToDataTable();
        }


        public string GetBatchNumberToGRN(int doucumentID, long referenceDocumentID)
        {
            string batchNo = "";
            var batch = (from bd in context.LgsProductBatchNoExpiaryDetails
                        where bd.ReferenceDocumentDocumentID == doucumentID && bd.ReferenceDocumentID == referenceDocumentID
                        select new
                        {
                            BatchNo = bd.BatchNo,
                        }).ToArray();

            foreach (var temp in batch)
            {
                batchNo = temp.BatchNo;
            }
            return batchNo;
        }


        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="lgsPurchaseOrderDetailTemp"></param>
        /// <returns></returns>
        public List<LgsPurchaseOrderDetailTemp> GetLgsPurchaseOrderDetailTempList(List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailsTemp, LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp)
        {
            if (lgsPurchaseOrderDetailsTemp.Where(pr => pr.ProductCode == lgsPurchaseOrderDetailTemp.ProductCode && pr.UnitOfMeasure == lgsPurchaseOrderDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                lgsPurchaseOrderDetailTemp.LineNo = lgsPurchaseOrderDetailsTemp.Where(pr => pr.ProductCode == lgsPurchaseOrderDetailTemp.ProductCode).FirstOrDefault().LineNo;
                lgsPurchaseOrderDetailsTemp.RemoveAll(pr => pr.ProductCode == lgsPurchaseOrderDetailTemp.ProductCode);
                lgsPurchaseOrderDetailsTemp.Add(lgsPurchaseOrderDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsPurchaseOrderDetailsTemp.Count, 0))
                { lgsPurchaseOrderDetailTemp.LineNo = 1; }
                else
                { lgsPurchaseOrderDetailTemp.LineNo = lgsPurchaseOrderDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsPurchaseOrderDetailsTemp.Add(lgsPurchaseOrderDetailTemp);
            }
            return lgsPurchaseOrderDetailsTemp;
        }

        public LgsPurchaseDetailTemp GetPurchaseDetailTemp(List<LgsPurchaseDetailTemp> lgsPurchaseDetailTemp, LgsProductMaster lgsProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            LgsPurchaseDetailTemp lgsPurchaseTempDetailTemp = new LgsPurchaseDetailTemp();
            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                      join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID) && psm.LocationID.Equals(locationID)
                                      select new
                                      {
                                          pm.LgsProductMasterID,
                                          pm.ProductCode,
                                          pm.ProductName,
                                          pm.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          pm.CostPrice,
                                          pm.SellingPrice,
                                          pm.AverageCost
                                      }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(lgsProductMaster.UnitOfMeasureID))
            {
                purchaseDetailTemp = (dynamic)null;

                purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                      join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join puc in context.LgsProductUnitConversions on pm.LgsProductMasterID equals puc.ProductID
                                      where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID)
                                      && psm.LocationID.Equals(locationID) && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
                                      select new
                                      {
                                          pm.LgsProductMasterID,
                                          pm.ProductCode,
                                          pm.ProductName,
                                          puc.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          puc.CostPrice,
                                          puc.SellingPrice,
                                          pm.AverageCost
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseTempDetailTemp = lgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseTempDetailTemp = lgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    lgsPurchaseTempDetailTemp = lgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsPurchaseTempDetailTemp == null)
                {
                    lgsPurchaseTempDetailTemp = new LgsPurchaseDetailTemp();
                    lgsPurchaseTempDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsPurchaseTempDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsPurchaseTempDetailTemp.ProductName = tempProduct.ProductName;
                    lgsPurchaseTempDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsPurchaseTempDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsPurchaseTempDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsPurchaseTempDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsPurchaseTempDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                }
                else
                {

                }

            }

            return lgsPurchaseTempDetailTemp;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<LgsPurchaseOrderDetailTemp> DeleteProductLgsPurchaseOrderDetailTemp(List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailsTemp, string productCode)
        {
            lgsPurchaseOrderDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return lgsPurchaseOrderDetailsTemp;
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
                    preFix = "X";
                else
                    preFix = "T";

            }

            if (isLocationCode)
                preFix = preFix + locationCode.Trim();


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


        public LgsPurchaseOrderDetailTemp GetPurchaseOrderDetailTemp(List<LgsPurchaseOrderDetailTemp> LgsPurchaseOrderDetailTemp, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();
            decimal defaultConvertFactor = 1;
            var purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
                                           join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID) && psm.LocationID.Equals(locationID)
                                           select new
                                           {
                                               pm.LgsProductMasterID,
                                               pm.ProductCode,
                                               pm.ProductName,
                                               pm.UnitOfMeasureID,
                                               uc.UnitOfMeasureName,
                                               pm.CostPrice,
                                               pm.PackSize,
                                               pm.SellingPrice,
                                               pm.AverageCost,
                                               ConvertFactor = defaultConvertFactor,
                                               pm.IsBatch
                                           }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(lgsProductMaster.UnitOfMeasureID))
            {
                purchaseOrderDetailTemp = (dynamic)null;

                purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
                                           join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           join puc in context.LgsProductUnitConversions on pm.LgsProductMasterID equals puc.ProductID
                                           where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID)
                                           && psm.LocationID.Equals(locationID) && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
                                           select new
                                           {
                                               pm.LgsProductMasterID,
                                               pm.ProductCode,
                                               pm.ProductName,
                                               puc.UnitOfMeasureID,
                                               uc.UnitOfMeasureName,
                                               puc.CostPrice,
                                               pm.PackSize,
                                               puc.SellingPrice,
                                               pm.AverageCost,
                                               puc.ConvertFactor,
                                               pm.IsBatch
                                           }).ToArray();
            }

            foreach (var tempProduct in purchaseOrderDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseOrderDetailTemp = LgsPurchaseOrderDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseOrderDetailTemp = LgsPurchaseOrderDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    lgsPurchaseOrderDetailTemp = LgsPurchaseOrderDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsPurchaseOrderDetailTemp == null)
                {
                    lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();
                    lgsPurchaseOrderDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                    lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                    lgsPurchaseOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsPurchaseOrderDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsPurchaseOrderDetailTemp.IsBatch = tempProduct.IsBatch;
                }

            }
            return lgsPurchaseOrderDetailTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lgsPurchaseOrderDetailsTemp"></param>
        /// <returns></returns>  
        public List<LgsPurchaseOrderDetailTemp> ResetLineNo(List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailsTemp)
        {
            var qry = lgsPurchaseOrderDetailsTemp.AsQueryable();
            return null;
        }

        public List<LgsPurchaseOrderDetail> GetAllLgsPurchaseOrderDetail()
        {
            return context.LgsPurchaseOrderDetails.ToList();
        }

        public LgsPurchaseOrderHeader GetPausedPurchaseOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsPurchaseOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public DataTable GetAllPurchaseOrderDataTable()
        {
            DataTable tblPurchaseOrder = new DataTable();

            var sq = (from pm in context.LgsProductMasters
                      join pd in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pd.ProductID
                      join ph in context.LgsPurchaseOrderHeaders on pd.LgsPurchaseOrderHeaderID equals ph.LgsPurchaseOrderHeaderID
                      join pr in context.LgsProductMasters on pd.ProductID equals pr.LgsProductMasterID 
                      select
                          new
                              {
                                  FieldDecimal1 = pd.NetAmount,
                                  FieldDecimal2 = pd.PackSize,
                                  FieldDecimal3 = pd.FreeQty,
                                  FieldDecimal4 = pd.CostPrice,
                                  FieldDecimal5 = pd.OrderQty,
                                  FieldDecimal6 = ph.GrossAmount,
                                  FieldDecimal7 = ph.DiscountPercentage,
                                  FieldDecimal8 = ph.DiscountAmount,
                                  FieldDecimal9 = ph.TaxAmount,
                                  FieldDecimal10 = ph.OtherCharges,
                                  FieldString1 = ph.DocumentNo,
                                  FieldString2 = ph.DocumentDate,
                                  FieldString3 = pr.ProductCode,
                                  FieldString4 = pr.ProductName,
                                  FieldString5 = "",
                                  FieldString6 = "",
                                  FieldString7 = ""

                              }).ToArray();

            var data2 =(from c in sq
                            select new
                            {
                                FieldDecimal1 = c.FieldDecimal1,
                                FieldDecimal2 = c.FieldDecimal1,
                                FieldDecimal3 = c.FieldDecimal1,
                                FieldDecimal4 = c.FieldDecimal1,
                                FieldDecimal5 = c.FieldDecimal1,
                                FieldDecimal6 = c.FieldDecimal1,
                                FieldDecimal7 = c.FieldDecimal1,
                                FieldDecimal8 = c.FieldDecimal1,
                                FieldDecimal9 = c.FieldDecimal1,
                                FieldDecimal10 = c.FieldDecimal1,
                                FieldString1=c.FieldString1.ToString(),
                                FieldString2= c.FieldString2.ToShortDateString(),
                                FieldString3=c.FieldString3.ToString(),
                                FieldString4=c.FieldString4.ToString(),
                                FieldString5 = "",
                                FieldString6 = "",
                                FieldString7 = ""
                               
                            });


            return tblPurchaseOrder = Common.LINQToDataTable(data2);

        }

        /// <summary>
        /// Get purchase order details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetPurchaseOrderTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {            
            var query = (
                        from ph in context.LgsPurchaseOrderHeaders
                        join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join s in context.LgsSuppliers on ph.LgsSupplierID equals s.LgsSupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID                  
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                      select
                          new
                          {                                                           
                              FieldString1 = ph.DocumentNo,
                              FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                              FieldString3 = ph.Remark,
                              FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                              FieldString5 = ph.ValidityPeriod,
                              FieldString6 = ph.ReferenceNo,
                              FieldString7 = s.TaxNo3,
                              FieldString8 = s.BillingAddress1,                              
                              FieldString9 = DbFunctions.TruncateTime(ph.PaymentExpectedDate),
                              FieldString10 = py.PaymentMethodName,
                              FieldString11 = ph.ExpectedDate,
                              FieldString12 = l.LocationCode + "  " + l.LocationName,
                              FieldString13 = pm.ProductCode,
                              FieldString14 = pm.ProductName,
                              FieldString15 = um.UnitOfMeasureName,
                              FieldString16 = pd.PackSize,
                              FieldString17 = pd.OrderQty,
                              FieldString18 = pd.FreeQty,
                              FieldString19 = pd.CostPrice,
                              FieldString20 = pd.DiscountPercentage,
                              FieldString21 = pd.DiscountAmount,
                              FieldString22 = pd.NetAmount,
                              FieldString23 = ph.GrossAmount,
                              FieldString24 = ph.DiscountPercentage,
                              FieldString25 = ph.DiscountAmount,
                              FieldString26 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2, // NBT
                              FieldString27 = ph.TaxAmount3, // VAT
                              FieldString28 = ph.OtherCharges,
                              FieldString29 = ph.NetAmount,
                              FieldString30 = ph.CreatedUser
                          });

            return query.AsEnumerable().ToDataTable();

        }

        /// <summary>
        /// Get purchase orders for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetPurchaseOrdersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            #region Dynamic linq sample
            //var qry1 = context.InvPurchaseOrderHeaders //.Where("Customer.Country == @0 and OrderDate < @1", "Switzerland", DateTime.Parse("2013-10-16"))
            //                .Where("InvPurchaseOrderHeaders.DocumentDate >= @0 AND InvPurchaseOrderHeaders.DocumentDate <= @1", DateTime.Parse("2013-10-16"), DateTime.Parse("2013-10-18"))
            //                .OrderBy("InvPurchaseOrderHeaders.DocumentDate")
            //                .Select("new(InvPurchaseOrderHeaders.DocumentDate, InvPurchaseOrderHeaders.DocumentNo");
            #endregion

            //StringBuilder selectFields = new StringBuilder();
            var query = context.LgsPurchaseOrderHeaders.Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfo.DocumentID);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        case "LgsSupplierID":
                            query = (from qr in query
                                     join jt in context.LgsSuppliers.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.LgsSupplierID equals jt.LgsSupplierID
                                     select qr
                                );
                            break;
                        default:
                            query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

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
            }

                #region dynamic select
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{ selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                #endregion

            var queryResult = query.AsEnumerable();
            return (from h in queryResult
                                   join s in context.LgsSuppliers on h.LgsSupplierID equals s.LgsSupplierID
                                   join l in context.Locations on h.LocationID equals l.LocationID
                                   select
                                       new
                                       {
                                           FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode : "",
                                           FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.CreatedUser : "",
                                           FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                                           FieldString4 = reportDataStructList[3].IsSelectionField == true ? s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName : "",
                                           FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.DocumentNo : "",
                                           FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.ReferenceNo : "",
                                           FieldDecimal1 = reportDataStructList[6].IsSelectionField == true ? (from t in context.LgsPurchaseOrderDetails
                                                                                                               where
                                                                                                                 t.LgsPurchaseOrderHeaderID == h.LgsPurchaseOrderHeaderID
                                                                                                               select new
                                                                                                               {
                                                                                                                   Column1 = ((System.Decimal?)t.OrderQty ?? (System.Decimal?)0)
                                                                                                               }).Sum(p => p.Column1) : 0, // OrderQty

                                           FieldDecimal2 = reportDataStructList[7].IsSelectionField == true ? h.GrossAmount : 0,
                                           FieldDecimal3 = reportDataStructList[8].IsSelectionField == true ? h.DiscountAmount : 0,
                                           FieldDecimal4 = reportDataStructList[9].IsSelectionField == true ? h.TaxAmount1 : 0, // NBT(2%)
                                           FieldDecimal5 = reportDataStructList[10].IsSelectionField == true ? h.TaxAmount2 : 0, // NBT(2.04%)
                                           FieldDecimal6 = reportDataStructList[11].IsSelectionField == true ? h.TaxAmount3 : 0, // VAT
                                           FieldDecimal7 = reportDataStructList[12].IsSelectionField == true ? h.NetAmount : 0

                                       }).ToArray().ToDataTable();            
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.LgsPurchaseOrderHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID); //.GroupBy("" + reportDataStruct.DbColumnName.Trim() + "", "" + reportDataStruct.DbColumnName.Trim() + "");            

             switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSupplierID":
                    qryResult = qryResult.Join(context.LgsSuppliers, reportDataStruct.DbColumnName.Trim(),
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                  "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                         .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;

                case "LocationID":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                  "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                         .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;

                default:
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
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString().Trim()); }
                }
            }
            return selectionDataList;
        }

        public bool Save(LgsPurchaseOrderHeader lgsPurchaseOrderHeader, List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTemp, out string newDocumentNo, string formName = "")
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                //if (invPurchaseOrderHeader.InvPurchaseOrderHeaderID.Equals(0))
                //    context.InvPurchaseOrderHeaders.Add(invPurchaseOrderHeader);
                //else
                //    context.Entry(invPurchaseOrderHeader).State = EntityState.Modified;

                //context.SaveChanges();


                if (lgsPurchaseOrderHeader.DocumentStatus.Equals(1))
                {
                    LgsPurchaseOrderService LgsPurchaseOrderService = new LgsPurchaseOrderService();
                    LocationService locationService = new LocationService();
                    lgsPurchaseOrderHeader.DocumentNo = LgsPurchaseOrderService.GetDocumentNo(formName, lgsPurchaseOrderHeader.LocationID, locationService.GetLocationsByID(lgsPurchaseOrderHeader.LocationID).LocationCode, lgsPurchaseOrderHeader.DocumentID, false).Trim();
                }

                newDocumentNo = lgsPurchaseOrderHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID.Equals(0))
                    context.LgsPurchaseOrderHeaders.Add(lgsPurchaseOrderHeader);
                else
                    context.Entry(lgsPurchaseOrderHeader).State = EntityState.Modified;

                context.SaveChanges();

                context.Set<LgsPurchaseOrderDetail>().Delete(context.LgsPurchaseOrderDetails.Where(pod => pod.LgsPurchaseOrderHeaderID.Equals(lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID) && pod.LocationID.Equals(lgsPurchaseOrderHeader.LocationID) && pod.DocumentID.Equals(lgsPurchaseOrderHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var purchaseOrderDetailSaveQuery = (from pod in lgsPurchaseOrderDetailTemp
                                                    select new
                                                    {
                                                        LgsPurchaseOrderDetailID = 0,
                                                        PurchaseOrderDetailID = 0,
                                                        LgsPurchaseOrderHeaderID = lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID,
                                                        CompanyID = lgsPurchaseOrderHeader.CompanyID,
                                                        LocationID = lgsPurchaseOrderHeader.LocationID,
                                                        CostCentreID = lgsPurchaseOrderHeader.CostCentreID,
                                                        ProductID = pod.ProductID,
                                                        DocumentID = lgsPurchaseOrderHeader.DocumentID,
                                                        OrderQty = pod.OrderQty,
                                                        FreeQty = pod.FreeQty,
                                                        CurrentQty = pod.CurrentQty,
                                                        BalanceQty = pod.OrderQty,
                                                        BalanceFreeQty = pod.FreeQty,
                                                        CostPrice = pod.CostPrice,
                                                        SellingPrice = pod.SellingPrice,
                                                        PackSize = pod.PackSize,
                                                        ConvertFactor = pod.ConvertFactor,
                                                        PackID = pod.PackID,
                                                        UnitOfMeasureID = pod.UnitOfMeasureID,
                                                        GrossAmount = pod.GrossAmount,
                                                        DiscountPercentage = pod.DiscountPercentage,
                                                        DiscountAmount = pod.DiscountAmount,
                                                        SubTotalDiscount = pod.SubTotalDiscount,
                                                        TaxAmount1 = pod.TaxAmount1,
                                                        TaxAmount2 = pod.TaxAmount2,
                                                        TaxAmount3 = pod.TaxAmount3,
                                                        TaxAmount4 = pod.TaxAmount4,
                                                        TaxAmount5 = pod.TaxAmount5,
                                                        NetAmount = pod.NetAmount,
                                                        CurrencyID = pod.CurrencyID,
                                                        CurrencyRate = pod.CurrencyRate,
                                                        LineNo = pod.LineNo,
                                                        DocumentStatus = lgsPurchaseOrderHeader.DocumentStatus,
                                                        IsBatch = pod.IsBatch,
                                                        GroupOfCompanyID = Common.GroupOfCompanyID,
                                                        CreatedUser = lgsPurchaseOrderHeader.CreatedUser,
                                                        CreatedDate = Common.ConvertStringToDateTime(lgsPurchaseOrderHeader.CreatedDate.ToString()),
                                                        ModifiedUser = lgsPurchaseOrderHeader.ModifiedUser,
                                                        ModifiedDate = Common.ConvertStringToDateTime(lgsPurchaseOrderHeader.ModifiedDate.ToString()),
                                                        DataTransfer = lgsPurchaseOrderHeader.DataTransfer
                                                    }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(purchaseOrderDetailSaveQuery), "LgsPurchaseOrderDetail");
                context.SaveChanges();

                if (poIsMandatory) { generateBatch = 1; }
                else { generateBatch = 2; }

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@lgsPurchaseOrderHeaderID", Value=lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=lgsPurchaseOrderHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=lgsPurchaseOrderHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=lgsPurchaseOrderHeader.DocumentStatus},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@generateBatch", Value=generateBatch},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@supplierID", Value=lgsPurchaseOrderHeader.LgsSupplierID}
                    };

                if (CommonService.ExecuteStoredProcedure("spLgsPurchaseOrderSave", parameter))
                {
                    transaction.Complete();
                    return true;
                }
                else
                { return false; }
            }
        }

        public bool IsValidNoOfFreeQty(decimal freeQty, long productID, int unitOfMeasureID, long purchaseOrderHeaderID) 
        {
            decimal poFreeQty = 0;
            var getCurrentFreeQty = (from a in context.LgsPurchaseOrderDetails
                                     where a.ProductID.Equals(productID) && a.LgsPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
                                     select new
                                     {
                                         Qty = a.BalanceFreeQty,
                                     }).ToArray();

            foreach (var temp in getCurrentFreeQty)
            {
                poFreeQty = temp.Qty;
            }

            if (poFreeQty >= freeQty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidNoOfQty(decimal qty, long productID, int unitOfMeasureID, long purchaseOrderHeaderID)
        {
            decimal poQty = 0;
            var getCurrentQty = (from a in context.LgsPurchaseOrderDetails
                                 where a.ProductID.Equals(productID) && a.LgsPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
                                 select new
                                 {
                                     Qty = a.BalanceQty,
                                 }).ToArray();

            foreach (var temp in getCurrentQty)
            {
                poQty = temp.Qty;
            }

            if (poQty >= qty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool IsValidNoOfQty(decimal qty, decimal listQty, long productID, int unitOfMeasureID, long purchaseOrderHeaderID)
        //{
        //    decimal poQty = 0;
        //    decimal remainPoQty = 0;
        //    var getCurrentQty = (from a in context.InvPurchaseOrderDetails
        //                         where a.ProductID.Equals(productID) && a.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
        //                         select new
        //                         {
        //                             Qty = a.BalanceQty * a.ConvertFactor,
        //                         }).ToArray();

        //    foreach (var temp in getCurrentQty)
        //    {
        //        poQty = temp.Qty;
        //    }

        //    remainPoQty = poQty - listQty;

        //    if (remainPoQty == 0)
        //    {
        //        if (listQty == poQty) { return true; }
        //        else { return false; }
        //    }
        //    else if (remainPoQty >= qty)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        public List<LgsPurchaseOrderDetailTemp> GetPausedPurchaseOrderDetail(LgsPurchaseOrderHeader lgsPurchaseOrderHeader)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp;

            List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTempList = new List<LgsPurchaseOrderDetailTemp>();

            var purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
                                           join pd in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pd.ProductID
                                           join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where pd.LocationID.Equals(lgsPurchaseOrderHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseOrderHeader.CompanyID)
                                           && pd.DocumentID.Equals(lgsPurchaseOrderHeader.DocumentID) && pd.LgsPurchaseOrderHeaderID.Equals(lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID)
                                           select new
                                           {
                                               pd.CostPrice,
                                               pd.PackSize,
                                               pd.CurrentQty,
                                               pd.DiscountAmount,
                                               pd.DiscountPercentage,
                                               pd.GrossAmount,
                                               pd.LineNo,
                                               pd.LocationID,
                                               pd.NetAmount,
                                               pd.OrderQty,
                                               pd.FreeQty,
                                               pd.BalanceQty,
                                               pd.ConvertFactor,
                                               pm.ProductCode,
                                               pd.ProductID,
                                               pm.ProductName,
                                               pd.SellingPrice,
                                               pd.SubTotalDiscount,
                                               pd.TaxAmount1,
                                               pd.TaxAmount2,
                                               pd.TaxAmount3,
                                               pd.TaxAmount4,
                                               pd.TaxAmount5,
                                               pd.UnitOfMeasureID,
                                               uc.UnitOfMeasureName,
                                               pd.IsBatch

                                           }).ToArray();

            foreach (var tempProduct in purchaseOrderDetailTemp)
            {

                lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

                lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                lgsPurchaseOrderDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseOrderDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsPurchaseOrderDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsPurchaseOrderDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsPurchaseOrderDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseOrderDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseOrderDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsPurchaseOrderDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseOrderDetailTemp.FreeQty = tempProduct.FreeQty;
                lgsPurchaseOrderDetailTemp.BalanceQty = tempProduct.BalanceQty;
                lgsPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseOrderDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseOrderDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseOrderDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseOrderDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseOrderDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseOrderDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseOrderDetailTemp.IsBatch = tempProduct.IsBatch;

                lgsPurchaseOrderDetailTempList.Add(lgsPurchaseOrderDetailTemp);
            }
            return lgsPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseOrderDetailTemp> GetUpdatePurchaseOrderDetailTemp(List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderTempDetail, LgsPurchaseOrderDetailTemp lgsPurchaseDetail, LgsProductMaster lgsProductMaster)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

            lgsPurchaseOrderDetailTemp = lgsPurchaseOrderTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID).FirstOrDefault();

            if (lgsPurchaseOrderDetailTemp == null || lgsPurchaseOrderDetailTemp.LineNo.Equals(0))
            {
                if (lgsPurchaseOrderTempDetail.Count.Equals(0))
                    lgsPurchaseDetail.LineNo = 1;
                else
                    lgsPurchaseDetail.LineNo = lgsPurchaseOrderTempDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsPurchaseOrderTempDetail.Remove(lgsPurchaseOrderDetailTemp);
                lgsPurchaseDetail.LineNo = lgsPurchaseOrderDetailTemp.LineNo;
            }

            lgsPurchaseOrderTempDetail.Add(lgsPurchaseDetail);

            return lgsPurchaseOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseOrderDetailTemp> GetDeletePurchaseOrderDetailTemp(List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderTempDetail, LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetail)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

            lgsPurchaseOrderDetailTemp = lgsPurchaseOrderTempDetail.Where(p => p.ProductID == lgsPurchaseOrderDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseOrderDetail.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (lgsPurchaseOrderDetailTemp != null)
            {
                lgsPurchaseOrderTempDetail.Remove(lgsPurchaseOrderDetailTemp);
                removedLineNo = lgsPurchaseOrderDetailTemp.LineNo;

            }
            lgsPurchaseOrderTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);


            return lgsPurchaseOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public LgsPurchaseOrderHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsPurchaseOrderHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public LgsPurchaseOrderHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.LgsPurchaseOrderHeaders.Where(po => po.LgsPurchaseOrderHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public string[] GetAllSavedDocumentNumbers(int documentID, int locationID)
        {
            List<string> GoodReceivedNoteList = context.LgsPurchaseOrderHeaders.Where(c => c.DocumentID == documentID && c.DocumentStatus == 1 && c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
            return GoodReceivedNoteList.ToArray();
        }


        public List<LgsBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo, int locationID, int documentID)
        {
            List<LgsBarcodeDetailTemp> lgsBarcodeDetailTempList = new List<LgsBarcodeDetailTemp>();
            LgsBarcodeDetailTemp lgsBarcodeDetailTemp = new LgsBarcodeDetailTemp();


            var barcodeDetailTemp = (from pm in context.LgsProductMasters
                                     join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
                                     join pd in context.LgsPurchaseOrderDetails on psm.ProductID equals pd.ProductID
                                     join ph in context.LgsPurchaseOrderHeaders on pd.LgsPurchaseOrderHeaderID equals ph.LgsPurchaseOrderHeaderID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     && pd.LocationID == ph.LocationID
                                     && psm.LocationID == locationID
                                     && ph.LgsPurchaseOrderHeaderID == psm.ReferenceDocumentID
                                     && ph.DocumentID == psm.ReferenceDocumentDocumentID
                                     && ph.DocumentNo == documentNo
                                     && ph.DocumentID == documentID
                                     select new
                                     {
                                         pm.LgsProductMasterID,
                                         pm.ProductCode,
                                         pm.ProductName,
                                         psm.BatchNo,
                                         psm.BarCode,
                                         Qty = pd.BalanceQty,
                                         Stock = pd.BalanceQty,
                                         pd.CostPrice,
                                         pd.SellingPrice,
                                         pd.LineNo
                                     }).ToArray();

            foreach (var tempProduct in barcodeDetailTemp)
            {


                lgsBarcodeDetailTemp = new LgsBarcodeDetailTemp();
                lgsBarcodeDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                lgsBarcodeDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsBarcodeDetailTemp.ProductName = tempProduct.ProductName;
                lgsBarcodeDetailTemp.BatchNo = tempProduct.BatchNo;
                lgsBarcodeDetailTemp.BarCode = tempProduct.BarCode;
                lgsBarcodeDetailTemp.Qty = tempProduct.Stock;
                lgsBarcodeDetailTemp.Stock = tempProduct.Stock;
                lgsBarcodeDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsBarcodeDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsBarcodeDetailTemp.LineNo = tempProduct.LineNo;

                lgsBarcodeDetailTempList.Add(lgsBarcodeDetailTemp);



            }

            return lgsBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseOrderDetailTemp> GetSelectionCriteriaBySupplierAndCurrentLocation(DateTime fromDate, DateTime toDate, long supplierID, int locationID, int orderCircle) 
        { 
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp;
            List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTempList = new List<LgsPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from ph in context.LgsPurchaseHeaders
                                      join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                                      join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                                      join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                      where ph.SupplierID == supplierID && pd.LocationID == locationID && 
                                      ph.DocumentDate >= fromDate && ph.DocumentDate <= toDate
                                      group new { ph, pd, pm } by new
                                      {
                                          pd.ProductID,
                                          pm.ProductCode,
                                          pm.ProductName,
                                          um.UnitOfMeasureName,
                                          um.UnitOfMeasureID,
                                          pm.PackSize,
                                          pm.CostPrice,
                                          pd.ConvertFactor
                                      } into g

                                      select new
                                      {
                                          ProductCode = g.Key.ProductCode,
                                          ProductName = g.Key.ProductName,
                                          UnitOfMeasureName = g.Key.UnitOfMeasureName,
                                          Qty = g.Sum(x => x.pd.Qty),
                                          PackSize = g.Key.PackSize,
                                          CostPrice = g.Key.CostPrice,
                                          ProductID = g.Key.ProductID,
                                          UnitOfMeasureID = g.Key.UnitOfMeasureID,
                                          ConvertFactor = g.Key.ConvertFactor,
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

                lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseOrderDetailTemp.PurchaseQty = tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.TransferQty = 0;
                lgsPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseOrderDetailTemp.OrderQty = tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseOrderDetailTemp.DiscountPercentage = 0;
                lgsPurchaseOrderDetailTemp.DiscountAmount = 0;
                lgsPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                lineNo = lgsPurchaseOrderDetailTemp.LineNo;
                lgsPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                lgsPurchaseOrderDetailTempList.Add(lgsPurchaseOrderDetailTemp);
            }
            return lgsPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseOrderDetailTemp> GetSelectionCriteriaBySupplierAndAllLocations(DateTime fromDate, DateTime toDate, long supplierID, int orderCircle)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp;
            List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTempList = new List<LgsPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from ph in context.LgsPurchaseHeaders
                                      join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                                      join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                                      join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                      where ph.LgsPurchaseHeaderID == pd.LgsPurchaseHeaderID && ph.SupplierID == supplierID &&
                                      ph.DocumentDate >= fromDate && ph.DocumentDate <= toDate
                                      group pd by new
                                      {
                                          pd.ProductID,
                                          pm.ProductCode,
                                          pm.ProductName,
                                          um.UnitOfMeasureName,
                                          um.UnitOfMeasureID,
                                          pm.PackSize,
                                          pm.CostPrice,
                                          pd.ConvertFactor,
                                      } into pdgroup

                                      select new
                                      {
                                          ProductCode = pdgroup.Key.ProductCode,
                                          ProductName = pdgroup.Key.ProductName,
                                          UnitOfMeasureName = pdgroup.Key.UnitOfMeasureName,
                                          Qty = pdgroup.Sum(x => x.Qty),
                                          PackSize = pdgroup.Key.PackSize,
                                          CostPrice = pdgroup.Key.CostPrice,
                                          ProductID = pdgroup.Key.ProductID,
                                          UnitOfMeasureID = pdgroup.Key.UnitOfMeasureID,
                                          ConvertFactor = pdgroup.Key.ConvertFactor,
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

                lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseOrderDetailTemp.PurchaseQty = tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.TransferQty = 0;
                lgsPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseOrderDetailTemp.OrderQty = tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseOrderDetailTemp.DiscountPercentage = 0;
                lgsPurchaseOrderDetailTemp.DiscountAmount = 0;
                lgsPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                lineNo = lgsPurchaseOrderDetailTemp.LineNo;
                lgsPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                lgsPurchaseOrderDetailTempList.Add(lgsPurchaseOrderDetailTemp);
            }
            return lgsPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseOrderDetailTemp> GetSelectionCriteriaByReorderLevelAndCurrentLocation(int locationID, long supplierID)  
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp;
            List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTempList = new List<LgsPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                      join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                                      join puc in context.LgsProductUnitConversions on um.UnitOfMeasureID equals puc.UnitOfMeasureID
                                      where psm.LocationID == locationID && pm.LgsSupplierID == supplierID
                                      select new
                                      {
                                          pm.ProductCode,
                                          pm.ProductName,
                                          pm.ReOrderLevel,
                                          pm.ReOrderQty,
                                          pm.PackSize,
                                          pm.CostPrice,
                                          um.UnitOfMeasureName,
                                          pm.LgsProductMasterID,
                                          um.UnitOfMeasureID,
                                          puc.ConvertFactor,
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

                decimal reOrderLevel = context.LgsProductMasters.Where(pm => pm.LgsProductMasterID.Equals(tempProduct.LgsProductMasterID)).Select(pm => pm.ReOrderLevel).FirstOrDefault();
                decimal reOrderQty = context.LgsProductMasters.Where(pm => pm.LgsProductMasterID.Equals(tempProduct.LgsProductMasterID)).Select(pm => pm.ReOrderQty).FirstOrDefault();
                //decimal stock = context.LgsProductStockMasters.Where(ps => ps.LocationID.Equals(locationID) && ps.ProductID.Equals(tempProduct.LgsroductMasterID)).Select(ps => ps.Stock).FirstOrDefault();
                decimal stock = context.LgsProductStockMasters.Where(ps => ps.LocationID == locationID && ps.ProductID == tempProduct.LgsProductMasterID).Select(ps => ps.Stock).FirstOrDefault();

                if (stock < reOrderLevel)
                {
                    lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                    lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsPurchaseOrderDetailTemp.PurchaseQty = 0;
                    lgsPurchaseOrderDetailTemp.TransferQty = 0;
                    lgsPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsPurchaseOrderDetailTemp.OrderQty = reOrderQty;
                    lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                    lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsPurchaseOrderDetailTemp.DiscountPercentage = 0;
                    lgsPurchaseOrderDetailTemp.DiscountAmount = 0;
                    lgsPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * reOrderQty;
                    lgsPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                    lineNo = lgsPurchaseOrderDetailTemp.LineNo;
                    lgsPurchaseOrderDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    lgsPurchaseOrderDetailTempList.Add(lgsPurchaseOrderDetailTemp);
                }
            }
            return lgsPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        public string[] GetProductCodesAccordingToPurchaseOrder(LgsPurchaseOrderHeader lgsPurchaseOrderHeader)
        {
            List<String> rtnList = new List<string>();

            var productCodeList = (from pm in context.LgsProductMasters
                                   join pod in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pod.ProductID
                                   join poh in context.LgsPurchaseOrderHeaders on pod.LgsPurchaseOrderHeaderID equals poh.LgsPurchaseOrderHeaderID
                                   where poh.LgsPurchaseOrderHeaderID == lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID
                                   select new
                                   {
                                       pm.ProductCode,
                                   });

            foreach (var temp in productCodeList)
            {
                rtnList.Add(temp.ProductCode);
            }

            return rtnList.ToArray();
        }

        public string[] GetProductNamesAccordingToPurchaseOrder(LgsPurchaseOrderHeader lgsPurchaseOrderHeader)
        { 
            List<String> rtnList = new List<string>();

            var productCodeList = (from pm in context.LgsProductMasters
                                   join pod in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pod.ProductID
                                   join poh in context.LgsPurchaseOrderHeaders on pod.LgsPurchaseOrderHeaderID equals poh.LgsPurchaseOrderHeaderID
                                   where poh.LgsPurchaseOrderHeaderID == lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID
                                   select new
                                   {
                                       pm.ProductName,
                                   });

            foreach (var temp in productCodeList)
            {
                rtnList.Add(temp.ProductName);
            }

            return rtnList.ToArray();
        }

        //public List<String> GetProductCodesAccordingToPurchaseOrder(InvPurchaseOrderHeader invPurchaseOrderHeader)
        //{
        //    List<String> rtnList = new List<String>();

        //    var productCodeList = (from pm in context.InvProductMasters
        //                           join pod in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pod.ProductID
        //                           join poh in context.InvPurchaseOrderHeaders on pod.InvPurchaseOrderHeaderID equals poh.InvPurchaseOrderHeaderID
        //                           where poh.InvPurchaseOrderHeaderID == invPurchaseOrderHeader.InvPurchaseOrderHeaderID
        //                           select new
        //                           {
        //                               pm.ProductCode,
        //                           });

        //    foreach (var temp in productCodeList)
        //    {
        //        rtnList.Add(temp.ProductCode);
        //    }

        //    return rtnList;
        //}


        public bool IsValidNoOfQty(decimal qty, decimal listQty, long productID, int unitOfMeasureID, long purchaseOrderHeaderID)
        {
            decimal poQty = 0;
            decimal remainPoQty = 0;
            var getCurrentQty = (from a in context.LgsPurchaseOrderDetails
                                 where a.ProductID.Equals(productID) && a.LgsPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
                                 select new
                                 {
                                     Qty = a.BalanceQty * a.ConvertFactor,
                                 }).ToArray();

            foreach (var temp in getCurrentQty)
            {
                poQty = temp.Qty;
            }

            remainPoQty = poQty - listQty;

            if (remainPoQty == 0)
            {
                if (listQty == poQty) { return true; }
                else { return false; }
            }
            else if (remainPoQty >= qty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public LgsQuotationHeader GetQuotationHeaderToPurchaseOrder(string documentNo)
        {
            return context.LgsQuotationHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        public List<LgsPurchaseOrderDetailTemp> GetPausedQuotationDetail(LgsQuotationHeader lgsQuotationHeader)
        {
            LgsPurchaseOrderDetailTemp lgsPurchaseOrderDetailTemp;

            List<LgsPurchaseOrderDetailTemp> lgsPurchaseOrderDetailTempList = new List<LgsPurchaseOrderDetailTemp>();


            var quotationDetailTemp = (from pm in context.LgsProductMasters
                                       join qd in context.LgsQuotationDetails on pm.LgsProductMasterID equals qd.ProductID
                                       join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       where qd.LocationID.Equals(lgsQuotationHeader.LocationID) && qd.CompanyID.Equals(lgsQuotationHeader.CompanyID)
                                       && qd.DocumentID.Equals(lgsQuotationHeader.DocumentID) && qd.LgsQuotationHeaderID.Equals(lgsQuotationHeader.LgsQuotationHeaderID)
                                       select new
                                       {
                                           qd.AverageCost,
                                           qd.ConvertFactor,
                                           qd.CostPrice,
                                           pm.PackSize,
                                           qd.CurrentQty,
                                           qd.DiscountAmount,
                                           qd.DiscountPercentage,
                                           qd.GrossAmount,
                                           qd.LineNo,
                                           qd.LocationID,
                                           qd.NetAmount,
                                           qd.OrderQty,
                                           pm.ProductCode,
                                           qd.ProductID,
                                           pm.ProductName,
                                           qd.Qty,
                                           qd.SellingPrice,
                                           qd.SubTotalDiscount,
                                           qd.TaxAmount1,
                                           qd.TaxAmount2,
                                           qd.TaxAmount3,
                                           qd.TaxAmount4,
                                           qd.TaxAmount5,
                                           qd.UnitOfMeasureID,
                                           uc.UnitOfMeasureName

                                       }).ToArray();

            foreach (var tempProduct in quotationDetailTemp)
            {

                lgsPurchaseOrderDetailTemp = new LgsPurchaseOrderDetailTemp();

                lgsPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                lgsPurchaseOrderDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseOrderDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsPurchaseOrderDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsPurchaseOrderDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsPurchaseOrderDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseOrderDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseOrderDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsPurchaseOrderDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseOrderDetailTemp.BalanceQty = tempProduct.Qty;
                lgsPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseOrderDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseOrderDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseOrderDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseOrderDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseOrderDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseOrderDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                lgsPurchaseOrderDetailTempList.Add(lgsPurchaseOrderDetailTemp);
            }
            return lgsPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        #endregion
    }
}