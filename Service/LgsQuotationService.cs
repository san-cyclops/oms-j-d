using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using Utility;
using EntityFramework.Extensions;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class LgsQuotationService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods



        public LgsQuotationDetailTemp getQuotationDetailTemp(List<LgsQuotationDetailTemp> LgsQuotationDetailTemp, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            LgsQuotationDetailTemp lgsQuotationDetailTemp = new LgsQuotationDetailTemp();
            decimal defaultConvertFactor = 1;
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
                                          pm.AverageCost,
                                          ConvertFactor = defaultConvertFactor
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
                                          pm.AverageCost,
                                          puc.ConvertFactor
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsQuotationDetailTemp = LgsQuotationDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsQuotationDetailTemp = LgsQuotationDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    lgsQuotationDetailTemp = LgsQuotationDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsQuotationDetailTemp == null)
                {
                    lgsQuotationDetailTemp = new LgsQuotationDetailTemp();
                    lgsQuotationDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsQuotationDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsQuotationDetailTemp.ProductName = tempProduct.ProductName;
                    lgsQuotationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsQuotationDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsQuotationDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsQuotationDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsQuotationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsQuotationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;

                }
            }

            return lgsQuotationDetailTemp;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.LgsQuotationHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersToPurchaseOrder(int locationID) 
        {
            List<string> documentNoList = context.LgsQuotationHeaders.Where(q => q.LocationID.Equals(locationID) && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<LgsQuotationDetailTemp> getUpdateQuotationDetailTemp(List<LgsQuotationDetailTemp> lgsQuotationTempDetail, LgsQuotationDetailTemp lgsQuotationDetail, LgsProductMaster lgsProductMaster)
        {
            LgsQuotationDetailTemp lgsQuotationDetailTemp = new LgsQuotationDetailTemp();

            lgsQuotationDetailTemp = lgsQuotationTempDetail.Where(p => p.ProductID == lgsQuotationDetail.ProductID && p.UnitOfMeasureID == lgsQuotationDetail.UnitOfMeasureID).FirstOrDefault();

            if (lgsQuotationDetailTemp == null || lgsQuotationDetailTemp.LineNo.Equals(0))
            {
                lgsQuotationDetail.LineNo = lgsQuotationTempDetail.Count + 1;
            }
            else
            {
                lgsQuotationTempDetail.Remove(lgsQuotationDetailTemp);
                lgsQuotationDetail.LineNo = lgsQuotationDetailTemp.LineNo;

            }

            lgsQuotationTempDetail.Add(lgsQuotationDetail);

            return lgsQuotationTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsQuotationHeaders.Where(q => q.DocumentStatus.Equals(0)).Select(q => q.DocumentNo).ToList(); 
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="InvDepartmentCode"></param>
        /// <returns></returns>
        public LgsQuotationHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsQuotationHeaders.Where(q => q.DocumentNo == pausedDocumentNo.Trim() && q.DocumentStatus.Equals(0)).FirstOrDefault();
        }


        public LgsQuotationHeader GetPausedQuotationHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsQuotationHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        ///// <summary>
        /////  Return InvPurchaseDetail list for grid view
        ///// </summary>
        ///// <returns></returns>
        //public List<NtrQuotationDetail> GetTempNtrQuotationDetails(NtrQuotationDetail ntrQuotationDetail)
        //{
        //    // List for grid view
        //    List<NtrQuotationDetail> tempNtrQuotationDetails = new List<NtrQuotationDetail>();
        //    NtrQuotationDetail tempNtrQuotationDetail = new NtrQuotationDetail();
        //    tempNtrQuotationDetail = tempNtrQuotationDetails.Where(q => q.ProductCode == ntrQuotationDetail.ProductCode).FirstOrDefault();
        //    if (tempNtrQuotationDetail != null)
        //    {
        //        tempNtrQuotationDetails.RemoveAll(q => q.ProductCode == ntrQuotationDetail.ProductCode);
        //        tempNtrQuotationDetails.Remove(tempNtrQuotationDetail);
        //        tempNtrQuotationDetails.Add(ntrQuotationDetail);
        //    }
        //    else
        //    {
        //        tempNtrQuotationDetails.Add(ntrQuotationDetail);
        //    }

        //    return tempNtrQuotationDetails;
        //}

        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="lgsQuotationDetailTemp"></param>
        /// <returns></returns>
        public List<LgsQuotationDetailTemp> GetLgsQuotationDetailTempList(List<LgsQuotationDetailTemp> lgsQuotationDetailsTemp, LgsQuotationDetailTemp lgsQuotationDetailTemp)
        {
            if (lgsQuotationDetailsTemp.Where(pr => pr.ProductCode == lgsQuotationDetailTemp.ProductCode && pr.UnitOfMeasure == lgsQuotationDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                lgsQuotationDetailTemp.LineNo = lgsQuotationDetailsTemp.Where(pr => pr.ProductCode == lgsQuotationDetailTemp.ProductCode).FirstOrDefault().LineNo;
                lgsQuotationDetailsTemp.RemoveAll(pr => pr.ProductCode == lgsQuotationDetailTemp.ProductCode);
                lgsQuotationDetailsTemp.Add(lgsQuotationDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsQuotationDetailsTemp.Count, 0))
                { lgsQuotationDetailTemp.LineNo = 1; }
                else
                { lgsQuotationDetailTemp.LineNo = lgsQuotationDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsQuotationDetailsTemp.Add(lgsQuotationDetailTemp);
            }
            return lgsQuotationDetailsTemp;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<LgsQuotationDetailTemp> DeleteProductLgsQuotationDetailTemp(List<LgsQuotationDetailTemp> lgsQuotationDetailsTemp, string productCode)
        {
            lgsQuotationDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return lgsQuotationDetailsTemp;
        }

        public List<LgsQuotationDetail> GetAllLgsQuotationDetail()
        {
            //return context.InvPurchaseOrderDetails.Where(u => u. == false).ToList();
            return context.LgsQuotationDetails.ToList();
        }


        public bool Save(LgsQuotationHeader lgsQuotationHeader, List<LgsQuotationDetailTemp> lgsQuotationDetailTemp)
        {

            if (lgsQuotationHeader.LgsQuotationHeaderID.Equals(0))
                context.LgsQuotationHeaders.Add(lgsQuotationHeader);
            else
                context.Entry(lgsQuotationHeader).State = EntityState.Modified;

            context.SaveChanges();

            List<LgsQuotationDetail> lgsQuotationDetailDelete = new List<LgsQuotationDetail>();
            lgsQuotationDetailDelete = context.LgsQuotationDetails.Where(p => p.LgsQuotationHeaderID == lgsQuotationHeader.LgsQuotationHeaderID).ToList();



            foreach (LgsQuotationDetail lgsQuotationDetailDeleteTemp in lgsQuotationDetailDelete)
            {
                context.LgsQuotationDetails.Remove(lgsQuotationDetailDeleteTemp);
            }
            context.SaveChanges();


            foreach (LgsQuotationDetailTemp lgsQuotationDetail in lgsQuotationDetailTemp)
            {
                LgsQuotationDetail lgsQuotationDetailSave = new LgsQuotationDetail();

                lgsQuotationDetailSave.LgsQuotationHeaderID = lgsQuotationHeader.LgsQuotationHeaderID;
                lgsQuotationDetailSave.CompanyID = lgsQuotationHeader.CompanyID;
                lgsQuotationDetailSave.LocationID = lgsQuotationHeader.LocationID;
                lgsQuotationDetailSave.CostCentreID = lgsQuotationHeader.CostCentreID;
                lgsQuotationDetailSave.ProductID = lgsQuotationDetail.ProductID;
                lgsQuotationDetailSave.DocumentID = lgsQuotationHeader.DocumentID;
                

                lgsQuotationDetailSave.AverageCost = lgsQuotationDetail.AverageCost;
                
                lgsQuotationDetailSave.ConvertFactor = lgsQuotationDetail.ConvertFactor;
                
                lgsQuotationDetailSave.CostPrice = lgsQuotationDetail.CostPrice;
                lgsQuotationDetailSave.CurrentQty = lgsQuotationDetail.CurrentQty;
                lgsQuotationDetailSave.DiscountAmount = lgsQuotationDetail.DiscountAmount;
                lgsQuotationDetailSave.DiscountPercentage = lgsQuotationDetail.DiscountPercentage;
                
                lgsQuotationDetailSave.DocumentStatus = lgsQuotationHeader.DocumentStatus;
                lgsQuotationDetailSave.GrossAmount = lgsQuotationDetail.GrossAmount;
                lgsQuotationDetailSave.GroupOfCompanyID = lgsQuotationHeader.GroupOfCompanyID;
                lgsQuotationDetailSave.LineNo = lgsQuotationDetail.LineNo;
                
                lgsQuotationDetailSave.NetAmount = lgsQuotationDetail.NetAmount;
                lgsQuotationDetailSave.OrderQty = lgsQuotationDetail.OrderQty;
                
                
                lgsQuotationDetailSave.Qty = lgsQuotationDetail.Qty;
                lgsQuotationDetailSave.BalanceQty = lgsQuotationDetail.Qty;
                lgsQuotationDetailSave.SellingPrice = lgsQuotationDetail.SellingPrice;
                lgsQuotationDetailSave.SubTotalDiscount = lgsQuotationDetail.SubTotalDiscount;
                lgsQuotationDetailSave.TaxAmount1 = lgsQuotationDetail.TaxAmount1;
                lgsQuotationDetailSave.TaxAmount2 = lgsQuotationDetail.TaxAmount2;
                lgsQuotationDetailSave.TaxAmount3 = lgsQuotationDetail.TaxAmount3;
                lgsQuotationDetailSave.TaxAmount4 = lgsQuotationDetail.TaxAmount4;
                lgsQuotationDetailSave.TaxAmount5 = lgsQuotationDetail.TaxAmount5;
                lgsQuotationDetailSave.UnitOfMeasureID = lgsQuotationDetail.UnitOfMeasureID;
                lgsQuotationDetailSave.BaseUnitID = lgsQuotationDetail.BaseUnitID;

                context.LgsQuotationDetails.Add(lgsQuotationDetailSave);

                context.SaveChanges();

            }


            return true;
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


        public LgsQuotationHeader GetQuotationByDocumentNo(string documentNo)
        {
            return context.LgsQuotationHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LgsQuotationHeader GetQuotationByID(long quotationID) 
        {
            return context.LgsQuotationHeaders.Where(q => q.LgsQuotationHeaderID.Equals(quotationID) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<LgsQuotationDetailTemp> getPausedQuotationDetail(LgsQuotationHeader lgsQuotationHeader)
        {
            LgsQuotationDetailTemp lgsQuotationDetailTemp;

            List<LgsQuotationDetailTemp> lgsQuotationDetailTempList = new List<LgsQuotationDetailTemp>();


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

                lgsQuotationDetailTemp = new LgsQuotationDetailTemp();

                lgsQuotationDetailTemp.AverageCost = tempProduct.AverageCost;
                lgsQuotationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsQuotationDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsQuotationDetailTemp.PackSize = tempProduct.PackSize;
                lgsQuotationDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsQuotationDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsQuotationDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsQuotationDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsQuotationDetailTemp.LineNo = tempProduct.LineNo;
                lgsQuotationDetailTemp.LocationID = tempProduct.LocationID;
                lgsQuotationDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsQuotationDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsQuotationDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsQuotationDetailTemp.ProductID = tempProduct.ProductID;
                lgsQuotationDetailTemp.ProductName = tempProduct.ProductName;
                lgsQuotationDetailTemp.Qty = tempProduct.Qty;
                lgsQuotationDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsQuotationDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsQuotationDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsQuotationDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsQuotationDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsQuotationDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsQuotationDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsQuotationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsQuotationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                lgsQuotationDetailTempList.Add(lgsQuotationDetailTemp);
            }
            return lgsQuotationDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsQuotationDetailTemp> GetDeleteQuotationDetailTemp(List<LgsQuotationDetailTemp> lgsQuotationTempDetail, LgsQuotationDetailTemp lgsQuotationDetailTemp)
        {
            LgsQuotationDetailTemp lgsQuotationTempDetailTemp = new LgsQuotationDetailTemp();

            lgsQuotationTempDetailTemp = lgsQuotationTempDetail.Where(p => p.ProductID == lgsQuotationDetailTemp.ProductID && p.UnitOfMeasureID == lgsQuotationDetailTemp.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;

            if (lgsQuotationTempDetailTemp != null)
            {
                lgsQuotationTempDetail.Remove(lgsQuotationTempDetailTemp);
                removedLineNo = lgsQuotationTempDetailTemp.LineNo;

            }
            //lgsQuotationTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            return lgsQuotationTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        #endregion
    }
}
