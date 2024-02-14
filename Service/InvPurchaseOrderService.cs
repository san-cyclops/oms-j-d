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
    /// Developed by - asanka
    /// </summary>
    public class InvPurchaseOrderService
    {
        ERPDbContext context = new ERPDbContext();
        public static bool poIsMandatory;
        int generateBatch; // 1-yes  2-no 
        
        //public enum PendingPoTypes
        //{
        //    Partial = 1,
        //    Opened = 2,
        //    Expired = 3,
        //}

        #region Methods

        /// <summary>
        /// Save purchase order
        /// </summary>
        /// <param name="invPurchaseOrderHeader"></param>
        public void SavePurchaseOrder(InvPurchaseOrderHeader invPurchaseOrderHeader)
        {
            context.InvPurchaseOrderHeaders.Add(invPurchaseOrderHeader);
            context.SaveChanges();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.InvPurchaseOrderHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        
        public InvPurchaseOrderHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.InvPurchaseOrderHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public InvPurchaseOrderHeader GetDocumentDetailsByDocumentNumber(string pausedDocumentNo) 
        {
            return context.InvPurchaseOrderHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim()).FirstOrDefault();
        }

        public InvPurchaseOrderHeader GetInvPurchaseOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvPurchaseOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvPurchaseOrderHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
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

            var sql = (from ph in context.InvPurchaseOrderHeaders
                       join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                       where ph.LocationID == locationID && pd.BalanceQty != 0 && ph.DocumentStatus == 1
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

        public string GetBatchNumberToGRN(int doucumentID, long referenceDocumentID)
        {
            string batchNo = "";
            var batch = (from bd in context.InvProductBatchNoExpiaryDetails
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
        /// <param name="invPurchaseOrderDetailTemp"></param>
        /// <returns></returns>
        public List<InvPurchaseOrderDetailTemp> GetInvPurchaseOrderDetailTempList(List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailsTemp, InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp)
        {
            if (invPurchaseOrderDetailsTemp.Where(pr => pr.ProductCode == invPurchaseOrderDetailTemp.ProductCode && pr.UnitOfMeasure == invPurchaseOrderDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                invPurchaseOrderDetailTemp.LineNo = invPurchaseOrderDetailsTemp.Where(pr => pr.ProductCode == invPurchaseOrderDetailTemp.ProductCode).FirstOrDefault().LineNo;
                invPurchaseOrderDetailsTemp.RemoveAll(pr => pr.ProductCode == invPurchaseOrderDetailTemp.ProductCode);
                invPurchaseOrderDetailsTemp.Add(invPurchaseOrderDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(invPurchaseOrderDetailsTemp.Count, 0))
                { invPurchaseOrderDetailTemp.LineNo = 1; }
                else
                { invPurchaseOrderDetailTemp.LineNo = invPurchaseOrderDetailsTemp.Max(ln => ln.LineNo) + 1; }
                invPurchaseOrderDetailsTemp.Add(invPurchaseOrderDetailTemp);
            }
            return invPurchaseOrderDetailsTemp;
        }

        public InvPurchaseDetailTemp GetPurchaseDetailTemp(List<InvPurchaseDetailTemp> invPurchaseDetailTemp, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            InvPurchaseDetailTemp invPurchaseTempDetailTemp = new InvPurchaseDetailTemp();
            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
                                      join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pm.IsDelete == false && pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID) && psm.LocationID.Equals(locationID)
                                      select new
                                      {
                                          pm.InvProductMasterID,
                                          pm.ProductCode,
                                          pm.ProductName,
                                          pm.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          pm.CostPrice,
                                          pm.SellingPrice,
                                          pm.AverageCost
                                      }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                purchaseDetailTemp = (dynamic)null;

                purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
                                      join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join puc in context.InvProductUnitConversions on pm.InvProductMasterID equals puc.ProductID
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
                                          pm.AverageCost
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    invPurchaseTempDetailTemp = invPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    invPurchaseTempDetailTemp = invPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    invPurchaseTempDetailTemp = invPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                if (invPurchaseTempDetailTemp == null)
                {
                    invPurchaseTempDetailTemp = new InvPurchaseDetailTemp();
                    invPurchaseTempDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invPurchaseTempDetailTemp.ProductCode = tempProduct.ProductCode;
                    invPurchaseTempDetailTemp.ProductName = tempProduct.ProductName;
                    invPurchaseTempDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invPurchaseTempDetailTemp.CostPrice = tempProduct.CostPrice;
                    invPurchaseTempDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invPurchaseTempDetailTemp.AverageCost = tempProduct.AverageCost;
                    invPurchaseTempDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                }
                else
                {

                }

            }

            return invPurchaseTempDetailTemp;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<InvPurchaseOrderDetailTemp> DeleteProductInvPurchaseOrderDetailTemp(List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailsTemp, string productCode)
        {
            invPurchaseOrderDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return invPurchaseOrderDetailsTemp;
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


        public InvPurchaseOrderDetailTemp GetPurchaseOrderDetailTemp(List<InvPurchaseOrderDetailTemp> InvPurchaseOrderDetailTemp, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID)
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();
            decimal defaultConvertFactor = 1;
            var purchaseOrderDetailTemp = (from pm in context.InvProductMasters.AsNoTracking()
                                           join psm in context.InvProductStockMasters.AsNoTracking() on pm.InvProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures.AsNoTracking() on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where pm.IsDelete == false && pm.InvProductMasterID.Equals(invProductMaster.InvProductMasterID) && psm.LocationID.Equals(locationID)
                                           select new
                                           {
                                               pm.InvProductMasterID,
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

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                purchaseOrderDetailTemp = (dynamic)null;

                purchaseOrderDetailTemp = (from pm in context.InvProductMasters
                                           join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           join puc in context.InvProductUnitConversions on pm.InvProductMasterID equals puc.ProductID
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
                                               pm.PackSize,
                                               puc.SellingPrice,
                                               pm.AverageCost,
                                               puc.ConvertFactor,
                                               pm.IsBatch
                                           }).ToArray();
            }

            foreach (var tempProduct in purchaseOrderDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    invPurchaseOrderDetailTemp = InvPurchaseOrderDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    invPurchaseOrderDetailTemp = InvPurchaseOrderDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    invPurchaseOrderDetailTemp = InvPurchaseOrderDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                if (invPurchaseOrderDetailTemp == null)
                {
                    invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();
                    invPurchaseOrderDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                    invPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                    invPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                    invPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                    invPurchaseOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invPurchaseOrderDetailTemp.AverageCost = tempProduct.AverageCost;
                    invPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invPurchaseOrderDetailTemp.IsBatch = tempProduct.IsBatch;
                }

            }
            return invPurchaseOrderDetailTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invPurchaseOrderDetailsTemp"></param>
        /// <returns></returns>  
        public List<InvPurchaseOrderDetailTemp> ResetLineNo(List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailsTemp)
        {
            var qry = invPurchaseOrderDetailsTemp.AsQueryable();
            return null;
        }

        public List<InvPurchaseOrderDetail> GetAllInvPurchaseOrderDetail()
        {
            return context.InvPurchaseOrderDetails.ToList();
        }

        public InvPurchaseOrderHeader GetPausedPurchaseOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvPurchaseOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public DataTable GetAllPurchaseOrderDataTable()
        {


            DataTable tblPurchaseOrder = new DataTable();

            var sq = (from pm in context.InvProductMasters
                      join pd in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pd.ProductID
                      join ph in context.InvPurchaseOrderHeaders on pd.InvPurchaseOrderHeaderID equals ph.InvPurchaseOrderHeaderID
                      join pr in context.InvProductMasters on pd.ProductID equals pr.InvProductMasterID 
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

        public DataTable GetAllPurchaseOrderHeader()
        {


            DataTable tblPurchaseOrder = new DataTable();

            var sq = (from pm in context.InvProductMasters where pm.DataTransfer.Equals(0) && pm.IsDelete.Equals(false) select pm);


            return sq.ToDataTable();

        }
        /// <summary>
        /// Get purchase order details for report
        /// </summary>
        /// <returns></returns>
        public DataSet GetPurchaseOrderTransactionDataSet(string documentNo, int documentStatus, int documentId)
        {
            DataSet dsReportData = new DataSet();
            DataTable dtPurchaseOrder = new DataTable();
            DataTable dtPurchaseOrderProdcutsDetails = new DataTable();
            DataTable dtProdcutsExtendedPropsColumns = new DataTable();
            dtProdcutsExtendedPropsColumns.Columns.Add("ColumnName", typeof(string));

            #region Purchase Order Data
            dtPurchaseOrder = (
                        from ph in context.InvPurchaseOrderHeaders
                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID                  
                        //join tx in context.Taxes on s.t
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                      select
                          new
                          {                                                           
                              FieldString1 = ph.DocumentNo,
                              FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                              FieldString3 = ph.Remark,
                              FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                              FieldString5 = "", // P Req. No
                              FieldString6 = ph.ValidityPeriod,
                              FieldString7 = s.TaxNo3, // Vat No
                              FieldString8 = DbFunctions.TruncateTime(ph.PaymentExpectedDate),                              
                              FieldString9 = ph.ReferenceNo,
                              FieldString10 = py.PaymentMethodName,
                              FieldString11 = ph.ExpectedDate,
                              FieldString12 = l.LocationCode + "  " + l.LocationName,
                              FieldString13 = pm.ProductCode,
                              FieldString14 = pm.ProductName,
                              FieldString15 = um.UnitOfMeasureName,
                              FieldDecimal1 = pd.PackSize,
                              FieldDecimal2 = pd.OrderQty,
                              FieldDecimal3 = pd.FreeQty,
                              FieldDecimal4 = pd.CostPrice,
                              FieldDecimal5 = pd.DiscountPercentage,
                              FieldDecimal6 = pd.DiscountAmount,
                              FieldDecimal7 = pd.NetAmount,
                              FieldString23 = ph.GrossAmount,
                              FieldString24 = ph.DiscountPercentage,
                              FieldString25 = ph.DiscountAmount,
                              FieldString26 = ph.TaxAmount3,
                              FieldString27 = ph.OtherCharges,
                              FieldString28 = ph.NetAmount,
                              FieldString29 = ph.CreatedUser,
                              FieldString30 = ph.TaxAmount1!=0? ph.TaxAmount1: ph.TaxAmount2,
                              FieldString31 = s.Remark
                          }).AsEnumerable().ToDataTable();

            dsReportData.Tables.Add(dtPurchaseOrder);
            #endregion

            #region Purchase Order Product Data
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
                //GetProductExtendedPropertyValuesByProductID
            var purchaseOrderProdcutsDetails = (
                        from ph in context.InvPurchaseOrderHeaders
                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                        join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                        join sc in context.InvSubCategories on pm.SubCategoryID equals sc.InvSubCategoryID
                        join sc2 in context.InvSubCategories2 on pm.SubCategory2ID equals sc2.InvSubCategory2ID
                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                FieldString4 = l.LocationCode + "  " + l.LocationName,
                                FieldString5 = pm.ProductCode,
                                FieldString6 = pm.CreatedUser,
                                FieldString7 = d.DepartmentName,
                                FieldString8 = c.CategoryName,
                                FieldString9 = sc.SubCategoryName,
                                FieldString10 = sc2.SubCategory2Name,
                                FieldString11 = pm.InvProductMasterID,//invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(pd.ProductID).ToList().ToString(),
                                FieldString12 = "",
                                FieldString13 = "",
                                FieldString14 = "",
                                FieldString15 = "",
                                FieldString16 = "",
                                FieldString17 = "",
                                FieldString18 = "",
                                FieldString19 = "",
                                FieldString20 = "",
                                FieldString21 = "",
                                FieldString22 = "",
                                FieldString23 = "",
                                FieldString24 = "",
                                FieldString25 = "",
                            }).AsEnumerable();

            dtPurchaseOrderProdcutsDetails = purchaseOrderProdcutsDetails.ToDataTable();

            int lastUpdatedColumnHeader = 11;
            foreach (DataRow drProduct in dtPurchaseOrderProdcutsDetails.Rows)
            {
                List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
                productExPropList = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(long.Parse(drProduct["FieldString11"].ToString()));
                
                // Set Column header
                for (int i = 0; i < productExPropList.Count; i++)
                {
                    if (!dtPurchaseOrderProdcutsDetails.Columns.Contains(productExPropList[i].ExtendedPropertyName.ToString().Trim()))
                    {
                        if (i < 14) // Because report has only 14 fields for product extended properties
                        {
                            dtPurchaseOrderProdcutsDetails.Columns[lastUpdatedColumnHeader].ColumnName = productExPropList[i].ExtendedPropertyName.ToString().Trim();
                            dtProdcutsExtendedPropsColumns.Rows.Add(productExPropList[i].ExtendedPropertyName.ToString().Trim());
                            lastUpdatedColumnHeader++;
                        }
                    }
                    if (i < 14) // Because report has only 14 fields for product extended properties
                    {
                        drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] = productExPropList[i].ValueData.ToString().Trim();
                    }
                }
            }

            // Reset Column Headers
            for (int i = 0; i < dtPurchaseOrderProdcutsDetails.Columns.Count; i++)
            {
                dtPurchaseOrderProdcutsDetails.Columns[i].ColumnName = "FieldString" + (i + 1);
            }

            dsReportData.Tables.Add(dtPurchaseOrderProdcutsDetails);
            dsReportData.Tables.Add(dtProdcutsExtendedPropsColumns);
            #endregion

            return dsReportData;
        }

        /// <summary>
        /// Get purchase order details for report
        /// </summary>
        /// <returns></returns>
        public DataSet GetPendingPurchaseOrderTransactionDataSet(string documentNo, int documentStatus, int documentId)
        {
            DataSet dsReportData = new DataSet();
            DataTable dtPurchaseOrder = new DataTable();
            DataTable dtPurchaseOrderProdcutsDetails = new DataTable();
            DataTable dtProdcutsExtendedPropsColumns = new DataTable();
            dtProdcutsExtendedPropsColumns.Columns.Add("ColumnName", typeof(string));

            #region Purchase Order Data
            dtPurchaseOrder = (
                        from ph in context.InvPurchaseOrderHeaders
                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        //join tx in context.Taxes on s.t
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = ph.Remark,
                                FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                                FieldString5 = "", // P Req. No
                                FieldString6 = ph.ValidityPeriod,
                                FieldString7 = s.TaxNo3, // Vat No
                                FieldString8 = DbFunctions.TruncateTime(ph.PaymentExpectedDate),
                                FieldString9 = ph.ReferenceNo,
                                FieldString10 = py.PaymentMethodName,
                                FieldString11 = ph.ExpectedDate,
                                FieldString12 = l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldDecimal1 = pd.PackSize,
                                FieldDecimal2 = pd.BalanceQty,
                                FieldDecimal3 = pd.FreeQty,
                                FieldDecimal4 = pd.CostPrice,
                                FieldDecimal5 = pd.DiscountPercentage,
                                FieldDecimal6 = pd.DiscountAmount,
                                FieldDecimal7 = pd.NetAmount,
                                FieldString23 = ph.GrossAmount,
                                FieldString24 = ph.DiscountPercentage,
                                FieldString25 = ph.DiscountAmount,
                                FieldString26 = ph.TaxAmount3,
                                FieldString27 = ph.OtherCharges,
                                FieldString28 = ph.NetAmount,
                                FieldString29 = ph.CreatedUser,
                                FieldString30 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2,
                                FieldString31 = s.Remark
                            }).AsEnumerable().ToDataTable();

            dsReportData.Tables.Add(dtPurchaseOrder);
            #endregion

            #region Purchase Order Product Data
            InvProductExtendedPropertyValueService invProductExtendedPropertyValueService = new InvProductExtendedPropertyValueService();
            //GetProductExtendedPropertyValuesByProductID
            var purchaseOrderProdcutsDetails = (
                        from ph in context.InvPurchaseOrderHeaders
                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                        join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                        join sc in context.InvSubCategories on pm.SubCategoryID equals sc.InvSubCategoryID
                        join sc2 in context.InvSubCategories2 on pm.SubCategory2ID equals sc2.InvSubCategory2ID
                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                FieldString4 = l.LocationCode + "  " + l.LocationName,
                                FieldString5 = pm.ProductCode,
                                FieldString6 = pm.CreatedUser,
                                FieldString7 = d.DepartmentName,
                                FieldString8 = c.CategoryName,
                                FieldString9 = sc.SubCategoryName,
                                FieldString10 = sc2.SubCategory2Name,
                                FieldString11 = pm.InvProductMasterID,//invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(pd.ProductID).ToList().ToString(),
                                FieldString12 = "",
                                FieldString13 = "",
                                FieldString14 = "",
                                FieldString15 = "",
                                FieldString16 = "",
                                FieldString17 = "",
                                FieldString18 = "",
                                FieldString19 = "",
                                FieldString20 = "",
                                FieldString21 = "",
                                FieldString22 = "",
                                FieldString23 = "",
                                FieldString24 = "",
                                FieldString25 = "",
                            }).AsEnumerable();

            dtPurchaseOrderProdcutsDetails = purchaseOrderProdcutsDetails.ToDataTable();

            int lastUpdatedColumnHeader = 11;
            foreach (DataRow drProduct in dtPurchaseOrderProdcutsDetails.Rows)
            {
                List<InvProductExtendedPropertyValue> productExPropList = new List<InvProductExtendedPropertyValue>();
                productExPropList = invProductExtendedPropertyValueService.GetProductExtendedPropertyValuesByProductID(long.Parse(drProduct["FieldString11"].ToString()));

                // Set Column header
                for (int i = 0; i < productExPropList.Count; i++)
                {
                    if (!dtPurchaseOrderProdcutsDetails.Columns.Contains(productExPropList[i].ExtendedPropertyName.ToString().Trim()))
                    {
                        if (i < 14) // Because report has only 14 fields for product extended properties
                        {
                            dtPurchaseOrderProdcutsDetails.Columns[lastUpdatedColumnHeader].ColumnName = productExPropList[i].ExtendedPropertyName.ToString().Trim();
                            dtProdcutsExtendedPropsColumns.Rows.Add(productExPropList[i].ExtendedPropertyName.ToString().Trim());
                            lastUpdatedColumnHeader++;
                        }
                    }
                    if (i < 14) // Because report has only 14 fields for product extended properties
                    {
                        drProduct[productExPropList[i].ExtendedPropertyName.ToString().Trim()] = productExPropList[i].ValueData.ToString().Trim();
                    }
                }
            }

            // Reset Column Headers
            for (int i = 0; i < dtPurchaseOrderProdcutsDetails.Columns.Count; i++)
            {
                dtPurchaseOrderProdcutsDetails.Columns[i].ColumnName = "FieldString" + (i + 1);
            }

            dsReportData.Tables.Add(dtPurchaseOrderProdcutsDetails);
            dsReportData.Tables.Add(dtProdcutsExtendedPropsColumns);
            #endregion

            return dsReportData;
        }

        /// <summary>
        /// Get purchase orders for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetPurchaseOrdersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            //StringBuilder selectFields = new StringBuilder();
            var query = context.InvPurchaseOrderHeaders.AsNoTracking().Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfo.DocumentID);
            
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
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
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
                            query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }


                    //query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
                }

            }
           
     
            //var queryResult = query.AsEnumerable();

            var result = (from h in query
                    join s in context.Suppliers on h.SupplierID equals s.SupplierID
                    join l in context.Locations on h.LocationID equals l.LocationID
                    //join po in context.InvPurchaseOrderHeaders on h.ReferenceDocumentID equals po.InvPurchaseOrderHeaderID
                    //where h.ReferenceDocumentDocumentID == autoGenerateInfo.DocumentID
                    select
                        new
                        {
                            FieldString1 = l.LocationCode + " " + l.LocationName,
                            FieldString2 = h.DocumentNo,
                            FieldString3 = h.DocumentDate,
                            FieldString4 = s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName,
                            FieldString5 = h.ReferenceNo,
                            FieldString6 = h.CreatedUser,
                            FieldDecimal1 = (from t in context.InvPurchaseOrderDetails
                                                    where
                                                        t.InvPurchaseOrderHeaderID == h.InvPurchaseOrderHeaderID
                                                    select new
                                                    {
                                                        Column1 = ((System.Decimal?)t.OrderQty ?? (System.Decimal?)0)
                                                    }).Sum(p => p.Column1), // OrderQty

                            FieldDecimal2 = h.GrossAmount,
                            FieldDecimal3 = h.DiscountAmount,
                            FieldDecimal4 = h.TaxAmount1, // NBT(2%)
                            FieldDecimal5 = h.TaxAmount2, // NBT(2.04%)
                            FieldDecimal6 = h.TaxAmount3, // VAT
                            FieldDecimal7 = h.NetAmount
                        });

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy)) // if no any groups
            {
                StringBuilder sbOrderByColumns = new StringBuilder();

                if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
                {
                    foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                    { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                    sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                    dtQueryResult = result.OrderBy(sbOrderByColumns.ToString())
                                               .ToDataTable();
                }
                else
                { dtQueryResult = result.ToDataTable(); }

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

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
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

                dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString().Trim());

                dtQueryResult.Columns.Remove("C1"); // Remove the first 'C1' column
                int colIndex = 0;
                foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                {
                    dtQueryResult.Columns[colIndex].ColumnName = item.ReportField;
                    colIndex++;
                }

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
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

        /// <summary>
        /// Get Pending purchase orders for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetPendingPurchaseOrdersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");

            var query = context.InvPurchaseOrderHeaders.AsNoTracking().Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfoPo.DocumentID);

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
                                     select qr );
                            break;
                        case "DocumentID":
                            var expiryDate = DateTime.Now.AddDays(1).AddSeconds(-1);
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals("Partial"))
                            {
                                query = (from qr in query
                                         where (from d in context.InvPurchaseOrderDetails
                                                where d.OrderQty != d.BalanceQty && d.BalanceQty > 0
                                                select d.InvPurchaseOrderHeaderID).Contains(qr.InvPurchaseOrderHeaderID)
                                                && qr.ExpiryDate > expiryDate
                                         select qr);
   
                               //query = (from qr in query
                               //         where qr.ExpiryDate > expiryDate &&
                               //          ((from p in context.InvPurchaseOrderDetails
                               //            where p.InvPurchaseOrderHeaderID == qr.InvPurchaseOrderHeaderID
                               //            group p by p.InvPurchaseOrderHeaderID into g
                               //            select new
                               //            {
                               //                PurchaseOrderHeaderID = g.Select(x => x.InvPurchaseOrderHeaderID),
                               //                sumBalanceQty = g.Sum(x => x.BalanceQty),
                               //                sumOrderQty = g.Sum(x => x.OrderQty)
                               //            })
                               //            .Where(p => p.sumBalanceQty > 0 && p.sumBalanceQty != p.sumOrderQty).Select(p => p.PurchaseOrderHeaderID).FirstOrDefault())
                               //            .Contains(qr.InvPurchaseOrderHeaderID)
                               //          select qr);
                             
                               //(from p in context.InvPurchaseOrderDetails
                               //                    where p.InvPurchaseOrderHeaderID == h.InvPurchaseOrderHeaderID
                               //                    group p by p.InvPurchaseOrderHeaderID into g
                               //                    select new
                               //                    {
                               //                        sumBalanceQty = g.Sum(x => x.BalanceQty),
                               //                        sumOrderQty = g.Sum(x => x.OrderQty),
                               //                    }).Any(p => p.sumBalanceQty > 0 && p.sumBalanceQty != p.sumOrderQty).Equals(true)
                            }
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals("Opened"))
                            {   
                                query = (from qr in query
                                         where (from d in context.InvPurchaseOrderDetails
                                                where d.OrderQty == d.BalanceQty && d.BalanceQty > 0
                                                select d.InvPurchaseOrderHeaderID).Contains(qr.InvPurchaseOrderHeaderID)
                                                && qr.ExpiryDate > expiryDate
                                         select qr);

                                //query = (from qr in query
                                //         where qr.ExpiryDate > expiryDate &&
                                //          ((from p in context.InvPurchaseOrderDetails
                                //            where p.InvPurchaseOrderHeaderID == qr.InvPurchaseOrderHeaderID
                                //            group p by p.InvPurchaseOrderHeaderID into g
                                //            select new
                                //            {
                                //                PurchaseOrderHeaderID = g.Select(x => x.InvPurchaseOrderHeaderID),
                                //                sumBalanceQty = g.Sum(x => x.BalanceQty),
                                //                sumOrderQty = g.Sum(x => x.OrderQty)
                                //            })
                                //            .Where(p => p.sumBalanceQty > 0 && p.sumBalanceQty == p.sumOrderQty).Select(p => p.PurchaseOrderHeaderID).FirstOrDefault())
                                //            .Contains(qr.InvPurchaseOrderHeaderID)
                                //         select qr);

                                //var xxx = ((from p in context.InvPurchaseOrderDetails
                                //            where p.InvPurchaseOrderHeaderID == 333
                                //            group p by p.InvPurchaseOrderHeaderID into g
                                //            select new
                                //            {
                                //                PurchaseOrderHeaderID = g.Select(x => x.InvPurchaseOrderHeaderID),
                                //                sumBalanceQty = g.Sum(x => x.BalanceQty),
                                //                sumOrderQty = g.Sum(x => x.OrderQty),
                                //            }).Where(p => p.sumBalanceQty > 0 && p.sumBalanceQty != p.sumOrderQty)
                                            
                                //);//.FirstOrDefault());

                                //string sttt = xxx.ToString();
                            }
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals("Expired"))
                            {
                                query = (from qr in query
                                         where qr.ExpiryDate < expiryDate
                                         select qr);
                            }
                            
                            break;
                        default:
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
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
                            query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }
            }

            var expiryDate2 = DateTime.Now.AddDays(1).AddSeconds(-1);
            string st = query.ToString();
            var result = (from h in query
                          join s in context.Suppliers on h.SupplierID equals s.SupplierID
                          join l in context.Locations on h.LocationID equals l.LocationID
                          //join po in context.InvPurchaseOrderHeaders on h.ReferenceDocumentID equals po.InvPurchaseOrderHeaderID
                          //where h.ReferenceDocumentDocumentID == autoGenerateInfo.DocumentID
                          select
                              new
                              {
                                  FieldString1 = l.LocationCode + " " + l.LocationName,
                                  FieldString2 = h.DocumentNo,
                                  FieldString3 = h.DocumentDate,
                                  FieldString4 = h.ExpiryDate,
                                  FieldString5 = s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName,
                                  FieldString6 = (((from p in context.InvPurchaseOrderDetails
                                                   where p.InvPurchaseOrderHeaderID == h.InvPurchaseOrderHeaderID
                                                   group p by p.InvPurchaseOrderHeaderID into g
                                                   select new
                                                   {
                                                       sumBalanceQty = g.Sum(x => x.BalanceQty),
                                                       sumOrderQty = g.Sum(x => x.OrderQty),
                                                   }).Any(p => p.sumBalanceQty > 0 && p.sumBalanceQty != p.sumOrderQty).Equals(true) && h.ExpiryDate > expiryDate2) ? "Partial"

                                                : (((from p in context.InvPurchaseOrderDetails
                                                    where p.InvPurchaseOrderHeaderID == h.InvPurchaseOrderHeaderID
                                                    group p by p.InvPurchaseOrderHeaderID into g
                                                    select new
                                                    {
                                                        sumBalanceQty = g.Sum(x => x.BalanceQty),
                                                        sumOrderQty = g.Sum(x => x.OrderQty),
                                                    }).Any(p => p.sumBalanceQty > 0 && p.sumBalanceQty == p.sumOrderQty).Equals(true) && h.ExpiryDate > expiryDate2) ? "Opened"

                                                : (h.ExpiryDate < expiryDate2 ? "Expired" : ""))),
                                  
                                  FieldString7 = h.ReferenceNo,
                                  FieldString8 = h.CreatedUser,
                                  FieldDecimal1 = (from t in context.InvPurchaseOrderDetails
                                                   where
                                                       t.InvPurchaseOrderHeaderID == h.InvPurchaseOrderHeaderID
                                                   select new
                                                   {
                                                       Column1 = ((System.Decimal?)t.BalanceQty ?? (System.Decimal?)0)
                                                   }).Sum(p => p.Column1), // BalanceQty
                                  FieldDecimal2 = h.GrossAmount,
                                  FieldDecimal3 = h.DiscountAmount,
                                  FieldDecimal4 = h.TaxAmount1, // NBT(2%)
                                  FieldDecimal5 = h.TaxAmount2, // NBT(2.04%)
                                  FieldDecimal6 = h.TaxAmount3, // VAT
                                  FieldDecimal7 = h.NetAmount
                              });

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy)) // if no any groups
            {
                StringBuilder sbOrderByColumns = new StringBuilder();

                if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
                {
                    foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                    { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                    sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                    dtQueryResult = result.OrderBy(sbOrderByColumns.ToString()).ToDataTable();
                }
                else
                { dtQueryResult = result.ToDataTable(); }

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

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
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

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal)) && d.IsColumnTotal.Equals(true)))
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


        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");
            
            // Create query 
            IQueryable qryResult = context.InvPurchaseOrderHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfoPo.DocumentID); //.GroupBy("" + reportDataStruct.DbColumnName.Trim() + "", "" + reportDataStruct.DbColumnName.Trim() + "");            

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "SupplierID":
                    qryResult = qryResult.Join(context.Suppliers, reportDataStruct.DbColumnName.Trim(),
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
                    selectionDataList.Add("Expired");
                    selectionDataList.Add("Opened");
                    selectionDataList.Add("Partial");
                }
                else
                {
                    foreach (var item in qryResult)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
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

        public bool Save(InvPurchaseOrderHeader invPurchaseOrderHeader, List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailTemp, out string newDocumentNo, string formName = "")
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                //if (invPurchaseOrderHeader.InvPurchaseOrderHeaderID.Equals(0))
                //    context.InvPurchaseOrderHeaders.Add(invPurchaseOrderHeader);
                //else
                //    context.Entry(invPurchaseOrderHeader).State = EntityState.Modified;

                //context.SaveChanges();


                if (invPurchaseOrderHeader.DocumentStatus.Equals(1))
                {
                    InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                    LocationService locationService = new LocationService();
                    invPurchaseOrderHeader.DocumentNo = invPurchaseOrderService.GetDocumentNo(formName, invPurchaseOrderHeader.LocationID, locationService.GetLocationsByID(invPurchaseOrderHeader.LocationID).LocationCode, invPurchaseOrderHeader.DocumentID, false).Trim();
                }

                newDocumentNo = invPurchaseOrderHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (invPurchaseOrderHeader.InvPurchaseOrderHeaderID.Equals(0))
                    context.InvPurchaseOrderHeaders.Add(invPurchaseOrderHeader);
                else
                    context.Entry(invPurchaseOrderHeader).State = EntityState.Modified;

                context.SaveChanges();

                context.Set<InvPurchaseOrderDetail>().Delete(context.InvPurchaseOrderDetails.Where(pod => pod.InvPurchaseOrderHeaderID.Equals(invPurchaseOrderHeader.InvPurchaseOrderHeaderID) && pod.LocationID.Equals(invPurchaseOrderHeader.LocationID) && pod.DocumentID.Equals(invPurchaseOrderHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var purchaseOrderDetailSaveQuery = (from pod in invPurchaseOrderDetailTemp
                                                    select new
                                                    {
                                                        InvPurchaseOrderDetailID = 0,
                                                        PurchaseOrderDetailID = 0,
                                                        InvPurchaseOrderHeaderID = invPurchaseOrderHeader.InvPurchaseOrderHeaderID,
                                                        CompanyID = invPurchaseOrderHeader.CompanyID,
                                                        LocationID = invPurchaseOrderHeader.LocationID,
                                                        CostCentreID = invPurchaseOrderHeader.CostCentreID,
                                                        ProductID = pod.ProductID,
                                                        DocumentID = invPurchaseOrderHeader.DocumentID,
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
                                                        DocumentStatus = invPurchaseOrderHeader.DocumentStatus,
                                                        IsBatch = pod.IsBatch,
                                                        GroupOfCompanyID = Common.GroupOfCompanyID,
                                                        CreatedUser = invPurchaseOrderHeader.CreatedUser,
                                                        CreatedDate = Common.ConvertStringToDateTime(invPurchaseOrderHeader.CreatedDate.ToString()),
                                                        ModifiedUser = invPurchaseOrderHeader.ModifiedUser,
                                                        ModifiedDate = Common.ConvertStringToDateTime(invPurchaseOrderHeader.ModifiedDate.ToString()),
                                                        DataTransfer = invPurchaseOrderHeader.DataTransfer
                                                    }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(purchaseOrderDetailSaveQuery), "InvPurchaseOrderDetail");
                context.SaveChanges();

                if (poIsMandatory) { generateBatch = 1; }
                else { generateBatch = 2; }

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@invPurchaseOrderHeaderID", Value=invPurchaseOrderHeader.InvPurchaseOrderHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseOrderHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseOrderHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invPurchaseOrderHeader.DocumentStatus},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@generateBatch", Value=generateBatch},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@supplierID", Value=invPurchaseOrderHeader.SupplierID}
                    };

                if (CommonService.ExecuteStoredProcedure("spInvPurchaseOrderSave", parameter))
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
            var getCurrentFreeQty = (from a in context.InvPurchaseOrderDetails
                                     where a.ProductID.Equals(productID) && a.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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
            var getCurrentQty = (from a in context.InvPurchaseOrderDetails
                                 where a.ProductID.Equals(productID) && a.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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


        public List<InvPurchaseOrderDetailTemp> GetPausedPurchaseOrderDetail(InvPurchaseOrderHeader invPurchaseOrderHeader)
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp;

            List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailTempList = new List<InvPurchaseOrderDetailTemp>();

            var purchaseOrderDetailTemp = (from pm in context.InvProductMasters
                                           join pd in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pd.ProductID
                                           join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where pd.LocationID.Equals(invPurchaseOrderHeader.LocationID) && pd.CompanyID.Equals(invPurchaseOrderHeader.CompanyID)
                                           && pd.DocumentID.Equals(invPurchaseOrderHeader.DocumentID) && pd.InvPurchaseOrderHeaderID.Equals(invPurchaseOrderHeader.InvPurchaseOrderHeaderID)
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

                invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

                invPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                invPurchaseOrderDetailTemp.CurrentQty = tempProduct.CurrentQty;
                invPurchaseOrderDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                invPurchaseOrderDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                invPurchaseOrderDetailTemp.GrossAmount = tempProduct.GrossAmount;
                invPurchaseOrderDetailTemp.LineNo = tempProduct.LineNo;
                invPurchaseOrderDetailTemp.LocationID = tempProduct.LocationID;
                invPurchaseOrderDetailTemp.NetAmount = tempProduct.NetAmount;
                invPurchaseOrderDetailTemp.OrderQty = tempProduct.OrderQty;
                invPurchaseOrderDetailTemp.FreeQty = tempProduct.FreeQty;
                invPurchaseOrderDetailTemp.BalanceQty = tempProduct.BalanceQty;
                invPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseOrderDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invPurchaseOrderDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invPurchaseOrderDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invPurchaseOrderDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invPurchaseOrderDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invPurchaseOrderDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseOrderDetailTemp.IsBatch = tempProduct.IsBatch;

                invPurchaseOrderDetailTempList.Add(invPurchaseOrderDetailTemp);
            }
            return invPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseOrderDetailTemp> GetUpdatePurchaseOrderDetailTemp(List<InvPurchaseOrderDetailTemp> invPurchaseOrderTempDetail, InvPurchaseOrderDetailTemp invPurchaseDetail, InvProductMaster invProductMaster)
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

            invPurchaseOrderDetailTemp = invPurchaseOrderTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();

            if (invPurchaseOrderDetailTemp == null || invPurchaseOrderDetailTemp.LineNo.Equals(0))
            {
                if (invPurchaseOrderTempDetail.Count.Equals(0))
                    invPurchaseDetail.LineNo = 1;
                else
                    invPurchaseDetail.LineNo = invPurchaseOrderTempDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invPurchaseOrderTempDetail.Remove(invPurchaseOrderDetailTemp);
                invPurchaseDetail.LineNo = invPurchaseOrderDetailTemp.LineNo;
            }

            invPurchaseOrderTempDetail.Add(invPurchaseDetail);

            return invPurchaseOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseOrderDetailTemp> GetDeletePurchaseOrderDetailTemp(List<InvPurchaseOrderDetailTemp> invPurchaseOrderTempDetail, InvPurchaseOrderDetailTemp invPurchaseOrderDetail)
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

            invPurchaseOrderDetailTemp = invPurchaseOrderTempDetail.Where(p => p.ProductID == invPurchaseOrderDetail.ProductID && p.UnitOfMeasureID == invPurchaseOrderDetail.UnitOfMeasureID).FirstOrDefault();
            long removedLineNo = 0;
            if (invPurchaseOrderDetailTemp != null)
            {
                invPurchaseOrderTempDetail.Remove(invPurchaseOrderDetailTemp);
                removedLineNo = invPurchaseOrderDetailTemp.LineNo;
            }

            invPurchaseOrderTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            return invPurchaseOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public InvPurchaseOrderHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.InvPurchaseOrderHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvPurchaseOrderHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.InvPurchaseOrderHeaders.Where(po => po.InvPurchaseOrderHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public string[] GetAllSavedDocumentNumbers(int documentID, int locationID)
        {
            List<string> GoodReceivedNoteList = context.InvPurchaseOrderHeaders.Where(c => c.DocumentID == documentID && c.DocumentStatus == 1 && c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
            return GoodReceivedNoteList.ToArray();
        }


        public List<InvBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo, int locationID, int documentID)
        {
            List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();


            var barcodeDetailTemp = (from pm in context.InvProductMasters
                                     join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                     join pd in context.InvPurchaseOrderDetails on psm.ProductID equals pd.ProductID
                                     join ph in context.InvPurchaseOrderHeaders on pd.InvPurchaseOrderHeaderID equals ph.InvPurchaseOrderHeaderID
                                     join um in context.UnitOfMeasures on psm.UnitOfMeasureID equals um.UnitOfMeasureID
                                     join sc in context.Suppliers on ph.SupplierID equals sc.SupplierID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     && pd.LocationID == ph.LocationID
                                     && psm.LocationID == locationID
                                     && ph.InvPurchaseOrderHeaderID == psm.ReferenceDocumentID
                                     && ph.DocumentID == psm.ReferenceDocumentDocumentID
                                     && ph.DocumentNo == documentNo
                                     && ph.DocumentID == documentID
                                     select new
                                     {
                                         pm.InvProductMasterID,
                                         pm.ProductCode,
                                         pm.ProductName,
                                         psm.BatchNo,
                                         psm.BarCode,
                                         Qty = pd.OrderQty,
                                         Stock = psm.BalanceQty,
                                         pd.CostPrice,
                                         pd.SellingPrice,
                                         UnitOfMeasureID = um.UnitOfMeasureID,
                                         UnitOfMeasureName = um.UnitOfMeasureName,
                                         pd.LineNo,
                                         ph.DocumentDate,
                                         sc.SupplierCode,
                                         ph.ExpiryDate
                                     }).ToArray();

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
                invBarcodeDetailTemp.LineNo = tempProduct.LineNo;
                invBarcodeDetailTemp.DocumentDate = tempProduct.DocumentDate;
                invBarcodeDetailTemp.SupplierCode = tempProduct.SupplierCode;
               // invBarcodeDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invBarcodeDetailTempList.Add(invBarcodeDetailTemp);
            }

            return invBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseOrderDetailTemp> GetSelectionCriteriaBySupplierAndCurrentLocation(DateTime fromDate, DateTime toDate, long supplierID, int locationID, int orderCircle) 
        { 
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp;
            List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailTempList = new List<InvPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from ph in context.InvPurchaseHeaders
                                      join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                                      join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
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
                invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

                invPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseOrderDetailTemp.PurchaseQty = tempProduct.Qty;
                invPurchaseOrderDetailTemp.SalesQty = 0;
                invPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseOrderDetailTemp.OrderQty = tempProduct.Qty;
                invPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                invPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseOrderDetailTemp.DiscountPercentage = 0;
                invPurchaseOrderDetailTemp.DiscountAmount = 0;
                invPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * tempProduct.Qty;
                invPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                lineNo = invPurchaseOrderDetailTemp.LineNo;
                invPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                invPurchaseOrderDetailTempList.Add(invPurchaseOrderDetailTemp);
            }
            return invPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseOrderDetailTemp> GetSelectionCriteriaBySupplierAndAllLocations(DateTime fromDate, DateTime toDate, long supplierID, int orderCircle)
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp;
            List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailTempList = new List<InvPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from ph in context.InvPurchaseHeaders
                                      join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                                      join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                      join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                      where ph.InvPurchaseHeaderID == pd.InvPurchaseHeaderID && ph.SupplierID == supplierID &&
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
                invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

                invPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseOrderDetailTemp.PurchaseQty = tempProduct.Qty;
                invPurchaseOrderDetailTemp.SalesQty = 0;
                invPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseOrderDetailTemp.OrderQty = tempProduct.Qty;
                invPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                invPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseOrderDetailTemp.DiscountPercentage = 0;
                invPurchaseOrderDetailTemp.DiscountAmount = 0;
                invPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * tempProduct.Qty;
                invPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                lineNo = invPurchaseOrderDetailTemp.LineNo;
                invPurchaseOrderDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                invPurchaseOrderDetailTempList.Add(invPurchaseOrderDetailTemp);
            }
            return invPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseOrderDetailTemp> GetSelectionCriteriaByReorderLevelAndCurrentLocation(int locationID, long supplierID)  
        {
            InvPurchaseOrderDetailTemp invPurchaseOrderDetailTemp;
            List<InvPurchaseOrderDetailTemp> invPurchaseOrderDetailTempList = new List<InvPurchaseOrderDetailTemp>();
            long lineNo = 0;

            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join psm in context.InvProductStockMasters on pm.InvProductMasterID equals psm.ProductID
                                      join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                                      join puc in context.InvProductUnitConversions on um.UnitOfMeasureID equals puc.UnitOfMeasureID
                                      where psm.LocationID == locationID && pm.SupplierID == supplierID
                                      select new
                                      {
                                          pm.ProductCode,
                                          pm.ProductName,
                                          pm.ReOrderLevel,
                                          pm.ReOrderQty,
                                          pm.PackSize,
                                          pm.CostPrice,
                                          um.UnitOfMeasureName,
                                          pm.InvProductMasterID,
                                          um.UnitOfMeasureID,
                                          puc.ConvertFactor,
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                invPurchaseOrderDetailTemp = new InvPurchaseOrderDetailTemp();

                decimal reOrderLevel = context.InvProductMasters.Where(pm => pm.InvProductMasterID.Equals(tempProduct.InvProductMasterID)).Select(pm => pm.ReOrderLevel).FirstOrDefault();
                decimal reOrderQty = context.InvProductMasters.Where(pm => pm.InvProductMasterID.Equals(tempProduct.InvProductMasterID)).Select(pm => pm.ReOrderQty).FirstOrDefault();
                decimal stock = context.InvProductStockMasters.Where(ps => ps.LocationID.Equals(locationID) && ps.ProductID.Equals(tempProduct.InvProductMasterID)).Select(ps => ps.Stock).FirstOrDefault();

                if (stock < reOrderLevel)
                {
                    invPurchaseOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                    invPurchaseOrderDetailTemp.ProductName = tempProduct.ProductName;
                    invPurchaseOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invPurchaseOrderDetailTemp.PurchaseQty = 0;
                    invPurchaseOrderDetailTemp.SalesQty = 0;
                    invPurchaseOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invPurchaseOrderDetailTemp.OrderQty = reOrderQty;
                    invPurchaseOrderDetailTemp.PackSize = tempProduct.PackSize;
                    invPurchaseOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                    invPurchaseOrderDetailTemp.DiscountPercentage = 0;
                    invPurchaseOrderDetailTemp.DiscountAmount = 0;
                    invPurchaseOrderDetailTemp.NetAmount = tempProduct.CostPrice * reOrderQty;
                    invPurchaseOrderDetailTemp.LineNo = lineNo + 1;
                    lineNo = invPurchaseOrderDetailTemp.LineNo;
                    invPurchaseOrderDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invPurchaseOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    invPurchaseOrderDetailTempList.Add(invPurchaseOrderDetailTemp);
                }
            }
            return invPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        public string[] GetProductCodesAccordingToPurchaseOrder(InvPurchaseOrderHeader invPurchaseOrderHeader)
        {
            List<String> rtnList = new List<string>();

            var productCodeList = (from pm in context.InvProductMasters
                                   join pod in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pod.ProductID
                                   join poh in context.InvPurchaseOrderHeaders on pod.InvPurchaseOrderHeaderID equals poh.InvPurchaseOrderHeaderID
                                   where poh.InvPurchaseOrderHeaderID == invPurchaseOrderHeader.InvPurchaseOrderHeaderID
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

        public string[] GetProductNamesAccordingToPurchaseOrder(InvPurchaseOrderHeader invPurchaseOrderHeader)
        { 
            List<String> rtnList = new List<string>();

            var productCodeList = (from pm in context.InvProductMasters
                                   join pod in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pod.ProductID
                                   join poh in context.InvPurchaseOrderHeaders on pod.InvPurchaseOrderHeaderID equals poh.InvPurchaseOrderHeaderID
                                   where poh.InvPurchaseOrderHeaderID == invPurchaseOrderHeader.InvPurchaseOrderHeaderID
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

        public void AuthorizePurchaseOrder(InvPurchaseOrderHeader invPurchaseOrderHeader) 
        {
            context.InvPurchaseOrderHeaders.Update(ph => ph.InvPurchaseOrderHeaderID.Equals(invPurchaseOrderHeader.InvPurchaseOrderHeaderID), ph => new InvPurchaseOrderHeader
            {
                IsAuthorized = true,
                LastAuthorizedBy = Common.LoggedUser,
                LastAuthorizedDate = Common.GetSystemDate(),
            });
        }


        #endregion
    }
}