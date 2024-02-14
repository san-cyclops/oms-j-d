using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using EntityFramework.Extensions;
using System.Transactions;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class LgsStockAdjustmentService
    {
        ERPDbContext context = new ERPDbContext();
        string sql;

        public LgsStockAdjustmentDetailTemp getStockAdjustmentDetailTemp(List<LgsStockAdjustmentDetailTemp> LgsStockAdjustmentDetailTemp, LgsProductMaster lgsProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();
            decimal defaultConvertFactor = 1;
            var StockAdjustmentDetailTemp =   (from pm in context.LgsProductMasters
                                              join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
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
                StockAdjustmentDetailTemp = (dynamic)null;

                StockAdjustmentDetailTemp =   (from pm in context.LgsProductMasters
                                               join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
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

            foreach (var tempProduct in StockAdjustmentDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsStockAdjustmentDetailTemp = LgsStockAdjustmentDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsStockAdjustmentDetailTemp = LgsStockAdjustmentDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    lgsStockAdjustmentDetailTemp = LgsStockAdjustmentDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsStockAdjustmentDetailTemp == null)
                {
                    lgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();
                    lgsStockAdjustmentDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsStockAdjustmentDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsStockAdjustmentDetailTemp.ProductName = tempProduct.ProductName;
                    lgsStockAdjustmentDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsStockAdjustmentDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsStockAdjustmentDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsStockAdjustmentDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsStockAdjustmentDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                }
            }

            return lgsStockAdjustmentDetailTemp;
        }

        public List<LgsStockAdjustmentDetailTemp> getUpdateLgsStockAdjustmentDetailTemp(List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentDetail, LgsStockAdjustmentDetailTemp lgsStockAdjDetail, LgsProductMaster lgsProductMaster)
        {
            LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();

            if (lgsProductMaster.IsExpiry)
                lgsStockAdjustmentDetailTemp = lgsStockAdjustmentDetail.Where(p => p.ProductID == lgsStockAdjDetail.ProductID && p.UnitOfMeasureID == lgsStockAdjDetail.UnitOfMeasureID && p.ExpiryDate == lgsStockAdjDetail.ExpiryDate).FirstOrDefault();
            else
                lgsStockAdjustmentDetailTemp = lgsStockAdjustmentDetail.Where(p => p.ProductID == lgsStockAdjDetail.ProductID && p.UnitOfMeasureID == lgsStockAdjDetail.UnitOfMeasureID).FirstOrDefault();

            if (lgsStockAdjustmentDetailTemp == null || lgsStockAdjustmentDetailTemp.LineNo.Equals(0))
            {
                if (lgsStockAdjustmentDetail.Count.Equals(0))
                    lgsStockAdjDetail.LineNo = 1;
                else
                    lgsStockAdjDetail.LineNo = lgsStockAdjustmentDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsStockAdjustmentDetail.Remove(lgsStockAdjustmentDetailTemp);
                lgsStockAdjDetail.LineNo = lgsStockAdjustmentDetailTemp.LineNo;

            }

            lgsStockAdjustmentDetail.Add(lgsStockAdjDetail);

            return lgsStockAdjustmentDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsStockAdjustmentDetailTemp> getDeleteLgsStockAdjustmentDetailTemp(List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentTempDetail, LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetail, List<LgsProductSerialNoTemp> lgsProductSerialNoListToDelete, out List<LgsProductSerialNoTemp> lgsProductSerialNoList)
        {
            LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();
            List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();

            lgsStockAdjustmentDetailTemp = lgsStockAdjustmentTempDetail.Where(p => p.ProductID == lgsStockAdjustmentDetail.ProductID && p.UnitOfMeasureID == lgsStockAdjustmentDetail.UnitOfMeasureID && p.ExpiryDate == lgsStockAdjustmentDetail.ExpiryDate).FirstOrDefault();

            if (lgsStockAdjustmentDetailTemp != null)
            {
                lgsStockAdjustmentTempDetail.Remove(lgsStockAdjustmentDetailTemp);
            }

            lgsProductSerialNoTempList = lgsProductSerialNoListToDelete.Where(p => p.ProductID == lgsStockAdjustmentDetail.ProductID && p.UnitOfMeasureID == lgsStockAdjustmentDetail.UnitOfMeasureID && p.ExpiryDate == lgsStockAdjustmentDetail.ExpiryDate).ToList();

            foreach (LgsProductSerialNoTemp LgsProductSerialNoTempDelete in lgsProductSerialNoTempList)
            {
                lgsProductSerialNoListToDelete.Remove(LgsProductSerialNoTempDelete);
            }

            lgsProductSerialNoList = lgsProductSerialNoListToDelete;

            return lgsStockAdjustmentTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsProductSerialNoTemp> getUpdateSerialNoTemp(List<LgsProductSerialNoTemp> lgsProductSerialNoList, LgsProductSerialNoTemp lgsProductSerialNumber)
        {
            LgsProductSerialNoTemp lgsProductSerialNoTemp = new LgsProductSerialNoTemp();

            lgsProductSerialNoTemp = lgsProductSerialNoList.Where(s => s.SerialNo.Equals(lgsProductSerialNumber.SerialNo) && s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).FirstOrDefault();

            if (lgsProductSerialNoTemp == null || lgsProductSerialNoTemp.LineNo.Equals(0))
            {
                if (lgsProductSerialNoList.Count.Equals(0))
                    lgsProductSerialNumber.LineNo = 1;
                else
                {
                    if (!lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Count().Equals(0))
                        lgsProductSerialNumber.LineNo = lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Max(s => s.LineNo) + 1;
                    else
                        lgsProductSerialNumber.LineNo = 1;
                }
                lgsProductSerialNoList.Add(lgsProductSerialNumber);
            }
            return lgsProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfSerialNo(List<LgsProductSerialNoTemp> lgsProductSerialNoList, LgsProductSerialNoTemp lgsProductSerialNumber, decimal Qty)
        {
            if (Qty.Equals(lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Count()))
                return true;
            else
                return false;
        }

        public List<LgsStockAdjustmentDetailTemp> getPausedLgsStockAdjustmentDetailTemp(LgsStockAdjustmentHeader StockAdjustmentHeader)
        {
            LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp;

            List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentDetailTempList = new List<LgsStockAdjustmentDetailTemp>();


            var stockAdjustmentDetailTemp =   (from pm in context.LgsProductMasters
                                              join sa in context.LgsStockAdjustmentDetails on pm.LgsProductMasterID equals sa.ProductID
                                              join uc in context.UnitOfMeasures on sa.UnitOfMeasureID equals uc.UnitOfMeasureID
                                              where sa.LocationID.Equals(StockAdjustmentHeader.LocationID) && sa.CompanyID.Equals(StockAdjustmentHeader.CompanyID)
                                              && sa.DocumentID.Equals(StockAdjustmentHeader.DocumentID) && sa.LgsStockAdjustmentHeaderID.Equals(StockAdjustmentHeader.LgsStockAdjustmentHeaderID)
                                              select new
                                              {
                                                  sa.ConvertFactor,
                                                  sa.CostPrice,
                                                  sa.SellingPrice,
                                                  sa.ExpiryDate,
                                                  sa.CostValue,
                                                  sa.SellingValue,
                                                  sa.LineNo,
                                                  sa.LocationID,
                                                  sa.OrderQty,
                                                  sa.BatchNo,
                                                  pm.ProductCode,
                                                  sa.ProductID,
                                                  pm.ProductName,
                                                  sa.UnitOfMeasureID,
                                                  uc.UnitOfMeasureName
                                              }).ToArray();

            foreach (var tempProduct in stockAdjustmentDetailTemp)
            {
                lgsStockAdjustmentDetailTemp = new LgsStockAdjustmentDetailTemp();

                lgsStockAdjustmentDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsStockAdjustmentDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsStockAdjustmentDetailTemp.CostValue = tempProduct.CostValue;
                lgsStockAdjustmentDetailTemp.SellingValue = tempProduct.SellingValue;
                lgsStockAdjustmentDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                lgsStockAdjustmentDetailTemp.LineNo = tempProduct.LineNo;
                lgsStockAdjustmentDetailTemp.LocationID = tempProduct.LocationID;
                lgsStockAdjustmentDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsStockAdjustmentDetailTemp.BatchNo = tempProduct.BatchNo;
                lgsStockAdjustmentDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsStockAdjustmentDetailTemp.ProductID = tempProduct.ProductID;
                lgsStockAdjustmentDetailTemp.ProductName = tempProduct.ProductName;
                lgsStockAdjustmentDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsStockAdjustmentDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsStockAdjustmentDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                lgsStockAdjustmentDetailTempList.Add(lgsStockAdjustmentDetailTemp);
            }
            return lgsStockAdjustmentDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<LgsProductSerialNoTemp> getPausedPurchaseSerialNoDetail(LgsPurchaseHeader lgsPurchaseHeader)
        {
            List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();
            List<LgsProductSerialNoDetail> lgsProductSerialNoDetail = new List<LgsProductSerialNoDetail>();

            lgsProductSerialNoDetail = context.LgsProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID)).ToList();
            foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailTemp in lgsProductSerialNoDetail)
            {
                LgsProductSerialNoTemp lgsProductSerialNoTemp = new LgsProductSerialNoTemp();

                lgsProductSerialNoTemp.ExpiryDate = lgsProductSerialNoDetailTemp.ExpiryDate;
                lgsProductSerialNoTemp.LineNo = lgsProductSerialNoDetailTemp.LineNo;
                lgsProductSerialNoTemp.ProductID = lgsProductSerialNoDetailTemp.ProductID;
                lgsProductSerialNoTemp.SerialNo = lgsProductSerialNoDetailTemp.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoDetailTemp.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }
            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public LgsStockAdjustmentHeader getStockAdjustmentHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public LgsStockAdjustmentHeader getPausedLgsStockAdjustmentHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)  
        {
            return context.LgsStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.LgsStockAdjustmentHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp lgsProductSerialNoTemp, int locationID)
        {
            LgsStockAdjustmentHeader lgsStockAdjustmentHeader = new LgsStockAdjustmentHeader();
            lgsStockAdjustmentHeader = getStockAdjustmentHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
            if (lgsStockAdjustmentHeader == null)
                lgsStockAdjustmentHeader = new LgsStockAdjustmentHeader();

            LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
            LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(lgsProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(lgsStockAdjustmentHeader.LgsStockAdjustmentHeaderID) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(lgsProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
            if (LgsProductSerialNoDetailtmp != null)
            {
                lgsStockAdjustmentHeader = getPausedLgsStockAdjustmentHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                if (lgsStockAdjustmentHeader != null)
                    return lgsStockAdjustmentHeader.DocumentNo;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }

        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp lgsProductSerialNoTemp)
        {
            if (context.LgsProductSerialNos.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(lgsProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(1)).FirstOrDefault() == null)
                return false;
            else
                return true;
        }

        public bool Save(LgsStockAdjustmentHeader lgsStockAdjustmentHeader, List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentDetailTemp, List<InvProductSerialNoTemp> lgsProductSerialNoTemp, bool isExcess, bool isShortage, bool isOverWrite)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (lgsStockAdjustmentHeader.LgsStockAdjustmentHeaderID.Equals(0))
                    context.LgsStockAdjustmentHeaders.Add(lgsStockAdjustmentHeader);
                else
                    context.Entry(lgsStockAdjustmentHeader).State = EntityState.Modified;

                context.SaveChanges();

                List<LgsStockAdjustmentDetail> lgsStockAdjustmentDetailDelete = new List<LgsStockAdjustmentDetail>();
                lgsStockAdjustmentDetailDelete = context.LgsStockAdjustmentDetails.Where(p => p.LgsStockAdjustmentHeaderID.Equals(lgsStockAdjustmentHeader.LgsStockAdjustmentHeaderID) && p.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && p.DocumentID.Equals(lgsStockAdjustmentHeader.DocumentID)).ToList();

                foreach (LgsStockAdjustmentDetail lgsStockAdjustmentDetailDeleteTemp in lgsStockAdjustmentDetailDelete)
                {
                    context.LgsStockAdjustmentDetails.Remove(lgsStockAdjustmentDetailDeleteTemp);
                }

                List<LgsProductSerialNoDetail> lgsProductSerialNoDetailDelete = new List<LgsProductSerialNoDetail>();
                lgsProductSerialNoDetailDelete = context.LgsProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(lgsStockAdjustmentHeader.LgsStockAdjustmentHeaderID) && p.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(lgsStockAdjustmentHeader.DocumentID)).ToList();

                foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailDeleteTemp in lgsProductSerialNoDetailDelete)
                {
                    context.LgsProductSerialNoDetails.Remove(lgsProductSerialNoDetailDeleteTemp);
                }
                context.SaveChanges();

                foreach (LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetail in lgsStockAdjustmentDetailTemp)
                {
                    LgsStockAdjustmentDetail lgsStockAdjustmentDetailSave = new LgsStockAdjustmentDetail();

                    lgsStockAdjustmentDetailSave.CompanyID = lgsStockAdjustmentHeader.CompanyID;
                    lgsStockAdjustmentDetailSave.ConvertFactor = lgsStockAdjustmentDetail.ConvertFactor;
                    lgsStockAdjustmentDetailSave.CostCentreID = lgsStockAdjustmentHeader.CostCentreID;
                    lgsStockAdjustmentDetailSave.BatchNo = lgsStockAdjustmentDetail.BatchNo;
                    lgsStockAdjustmentDetailSave.CostPrice = lgsStockAdjustmentDetail.CostPrice;
                    lgsStockAdjustmentDetailSave.DocumentID = lgsStockAdjustmentHeader.DocumentID;
                    lgsStockAdjustmentDetailSave.DocumentStatus = lgsStockAdjustmentHeader.DocumentStatus;
                    lgsStockAdjustmentDetailSave.DocumentDate = lgsStockAdjustmentHeader.DocumentDate;
                    lgsStockAdjustmentDetailSave.GroupOfCompanyID = lgsStockAdjustmentHeader.GroupOfCompanyID;
                    lgsStockAdjustmentDetailSave.LineNo = lgsStockAdjustmentDetail.LineNo;
                    lgsStockAdjustmentDetailSave.LocationID = lgsStockAdjustmentHeader.LocationID;
                    lgsStockAdjustmentDetailSave.SellingValue = lgsStockAdjustmentDetail.SellingValue;
                    lgsStockAdjustmentDetailSave.SellingPrice = lgsStockAdjustmentDetail.SellingPrice;
                    lgsStockAdjustmentDetailSave.ExpiryDate = lgsStockAdjustmentDetail.ExpiryDate;
                    lgsStockAdjustmentDetailSave.CostValue = lgsStockAdjustmentDetail.CostValue;
                    lgsStockAdjustmentDetailSave.OrderQty = lgsStockAdjustmentDetail.OrderQty;
                    lgsStockAdjustmentDetailSave.ProductID = lgsStockAdjustmentDetail.ProductID;
                    lgsStockAdjustmentDetailSave.LgsStockAdjustmentHeaderID = lgsStockAdjustmentHeader.LgsStockAdjustmentHeaderID;
                    lgsStockAdjustmentDetailSave.OrderQty = lgsStockAdjustmentDetail.OrderQty;
                    lgsStockAdjustmentDetailSave.UnitOfMeasureID = lgsStockAdjustmentDetail.UnitOfMeasureID;
                    lgsStockAdjustmentDetailSave.BaseUnitID = lgsStockAdjustmentDetail.BaseUnitID;
                    lgsStockAdjustmentDetailSave.StockAdjustmentMode = lgsStockAdjustmentHeader.StockAdjustmentMode;

                    if (lgsStockAdjustmentHeader.DocumentStatus.Equals(1))
                    {
                        if (isExcess)
                        {
                            decimal currentStock = context.LgsProductStockMasters.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();
                            lgsStockAdjustmentDetailSave.CurrentQty = currentStock;
                            context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock + lgsStockAdjustmentDetail.OrderQty });

                            decimal currentBatchStock = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();
                            lgsStockAdjustmentDetailSave.BatchQty = currentBatchStock;
                            context.LgsProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty + lgsStockAdjustmentDetail.OrderQty });
                            
                            
                            lgsStockAdjustmentDetailSave.ExcessQty = lgsStockAdjustmentDetail.OrderQty;
                            lgsStockAdjustmentDetailSave.ShortageQty = 0;
                            lgsStockAdjustmentDetailSave.OverWriteQty = 0;
                        }
                        else if (isShortage)
                        {
                            decimal currentStock = context.LgsProductStockMasters.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();
                            lgsStockAdjustmentDetailSave.CurrentQty = currentStock;
                            context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock - lgsStockAdjustmentDetail.OrderQty });

                            decimal currentBatchStock = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();
                            lgsStockAdjustmentDetailSave.BatchQty = currentBatchStock;
                            context.LgsProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty - lgsStockAdjustmentDetail.OrderQty });


                            lgsStockAdjustmentDetailSave.ShortageQty = lgsStockAdjustmentDetail.OrderQty;
                            lgsStockAdjustmentDetailSave.ExcessQty = 0;
                            lgsStockAdjustmentDetailSave.OverWriteQty = 0;
                        }
                        else if (isOverWrite)
                        {
                            decimal qty;
                            decimal currentStock = context.LgsProductStockMasters.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();

                            decimal batchqty;
                            decimal currentBatchStock = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();

                            lgsStockAdjustmentDetailSave.OverWriteQty = lgsStockAdjustmentDetail.OrderQty;
                            lgsStockAdjustmentDetailSave.CurrentQty = currentStock;
                            lgsStockAdjustmentDetailSave.BatchQty = currentBatchStock;

                            if (lgsStockAdjustmentDetail.OrderQty > currentBatchStock)
                            {
                                batchqty = lgsStockAdjustmentDetail.OrderQty - currentBatchStock;
                                lgsStockAdjustmentDetailSave.ExcessQty = batchqty;
                                lgsStockAdjustmentDetailSave.ShortageQty = 0;
                                context.LgsProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.UnitOfMeasureID.Equals(1)  && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty + batchqty });
                            }
                            else if (lgsStockAdjustmentDetail.OrderQty < currentBatchStock)
                            {
                                batchqty   = currentBatchStock - lgsStockAdjustmentDetail.OrderQty;
                                lgsStockAdjustmentDetailSave.ExcessQty = 0;
                                lgsStockAdjustmentDetailSave.ShortageQty = batchqty;
                                context.LgsProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(lgsStockAdjustmentDetail.BatchNo) && ps.UnitOfMeasureID.Equals(1) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty - batchqty });
                            }
                            else if (lgsStockAdjustmentDetail.OrderQty == currentBatchStock)
                            {
                                lgsStockAdjustmentDetailSave.ExcessQty = 0;
                                lgsStockAdjustmentDetailSave.ShortageQty = 0;
                            }

                            if (lgsStockAdjustmentDetail.OrderQty > currentStock)
                            {
                                qty = lgsStockAdjustmentDetail.OrderQty - currentStock;
                                lgsStockAdjustmentDetailSave.ExcessQty = qty;
                                lgsStockAdjustmentDetailSave.ShortageQty = 0;
                                context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock + qty });
                            }
                            else if (lgsStockAdjustmentDetail.OrderQty < currentStock)
                            {
                                qty = currentStock - lgsStockAdjustmentDetail.OrderQty;
                                lgsStockAdjustmentDetailSave.ExcessQty = 0;
                                lgsStockAdjustmentDetailSave.ShortageQty = qty;
                                context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(lgsStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(lgsStockAdjustmentDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock - qty });
                            }
                            else if (lgsStockAdjustmentDetail.OrderQty == currentBatchStock)
                            {
                                lgsStockAdjustmentDetailSave.ExcessQty = 0;
                                lgsStockAdjustmentDetailSave.ShortageQty = 0;
                            }
                        } 
                    }
                    context.LgsStockAdjustmentDetails.Add(lgsStockAdjustmentDetailSave);
                    context.SaveChanges();
                }

               
                transaction.Complete();

                return true;
            }

        }


        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo, bool isExcess, bool isShortage, bool isOverwrite) 
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
            else
            {
                if (isExcess)
                {
                    preFix = preFix + "AS";
                }
                else if (isShortage)
                {
                    preFix = preFix + "RS";
                }
                else if(isOverwrite)
                {
                    preFix = preFix + "OW";
                }
            }

            if (isLocationCode)
            {
                preFix = preFix + locationCode.Trim();
            }
            else
            {

            }

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


        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.LgsStockAdjustmentHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }


        public List<InvProductSerialNoTemp> GetSerialNoDetail(LgsProductMaster lgsProductMaster) 
        {
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            List<LgsProductSerialNo> lgsProductSerialNo = new List<LgsProductSerialNo>();

            lgsProductSerialNo = context.LgsProductSerialNos.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
            foreach (LgsProductSerialNo lgsProductSerialNoRetrive in lgsProductSerialNo)
            {
                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();

                lgsProductSerialNoTemp.ExpiryDate = lgsProductSerialNoRetrive.ExpiryDate;
                lgsProductSerialNoTemp.ProductID = lgsProductSerialNoRetrive.ProductID;
                lgsProductSerialNoTemp.SerialNo = lgsProductSerialNoRetrive.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoRetrive.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }

            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public List<LgsProductBatchNoTemp> GetBatchNoDetail(LgsProductMaster lgsProductMaster, int locationID) 
        {
            List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoDetail = new List<LgsProductBatchNoExpiaryDetail>();

            lgsProductBatchNoDetail = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && ps.LocationID.Equals(locationID) && ps.BalanceQty > 0).ToList();
            foreach (LgsProductBatchNoExpiaryDetail lgsProductBatchNoTemp in lgsProductBatchNoDetail)
            {
                LgsProductBatchNoTemp lgsProductBatchNoDetailTemp = new LgsProductBatchNoTemp();

                lgsProductBatchNoDetailTemp.LineNo = lgsProductBatchNoTemp.LineNo;
                lgsProductBatchNoDetailTemp.ProductID = lgsProductBatchNoTemp.ProductID;
                lgsProductBatchNoDetailTemp.BatchNo = lgsProductBatchNoTemp.BatchNo;
                lgsProductBatchNoDetailTemp.UnitOfMeasureID = lgsProductBatchNoTemp.UnitOfMeasureID;

                lgsProductBatchNoTempList.Add(lgsProductBatchNoDetailTemp);
            }

            return lgsProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public LgsProductBatchNoExpiaryDetail GetLgsProductBatchNoByBatchNo(string batchNo)
        {
            return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public List<LgsProductBatchNoTemp> GetExpiryDetail(LgsProductMaster lgsProductMaster) 
        {
            List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoDetail = new List<LgsProductBatchNoExpiaryDetail>();

            lgsProductBatchNoDetail = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
            foreach (LgsProductBatchNoExpiaryDetail lgsProductBatchNoTemp in lgsProductBatchNoDetail)
            {
                LgsProductBatchNoTemp lgsProductBatchNoDetailTemp = new LgsProductBatchNoTemp();

                lgsProductBatchNoDetailTemp.LineNo = lgsProductBatchNoTemp.LineNo;
                lgsProductBatchNoDetailTemp.ProductID = lgsProductBatchNoTemp.ProductID;
                lgsProductBatchNoDetailTemp.BatchNo = lgsProductBatchNoTemp.BatchNo;
                lgsProductBatchNoDetailTemp.ExpiryDate = lgsProductBatchNoTemp.ExpiryDate;
                lgsProductBatchNoDetailTemp.UnitOfMeasureID = lgsProductBatchNoTemp.UnitOfMeasureID;

                lgsProductBatchNoTempList.Add(lgsProductBatchNoDetailTemp);
            }

            return lgsProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public LgsStockAdjustmentHeader GetPausedLgsStockAdjustmentHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public List<LgsStockAdjustmentDetailTemp> GetDeleteStockAdjustmentDetailTemp(List<LgsStockAdjustmentDetailTemp> lgsStockAdjustmentTempDetail, LgsStockAdjustmentDetailTemp lgsStockAdjustmentDetailTemp)
        {
            LgsStockAdjustmentDetailTemp lgsStockAdjustmentTempDetailTemp = new LgsStockAdjustmentDetailTemp();

            lgsStockAdjustmentTempDetailTemp = lgsStockAdjustmentTempDetail.Where(p => p.ProductID == lgsStockAdjustmentDetailTemp.ProductID && p.UnitOfMeasureID == lgsStockAdjustmentDetailTemp.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (lgsStockAdjustmentTempDetailTemp != null)
            {
                lgsStockAdjustmentTempDetail.Remove(lgsStockAdjustmentTempDetailTemp);
                removedLineNo = lgsStockAdjustmentTempDetailTemp.LineNo;

            }
            lgsStockAdjustmentTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            return lgsStockAdjustmentTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetStockAdjustmentDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            int stockAdjustmentMode = 0;
            var query = context.LgsStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);
            if (autoGenerateInfo.FormName == "RptExcessStockAdjustment")
            {
                stockAdjustmentMode = 1;
                // Create query 
                query = context.LgsStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else if (autoGenerateInfo.FormName == "RptShortageStockAdjustment")
            {
                stockAdjustmentMode = 2;
                query = context.LgsStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                //if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                //{ query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            //var queryResultData = (dynamic)null;
            DataTable dtResult = new DataTable();
          
            if (stockAdjustmentMode == 1 || stockAdjustmentMode == 2)
            {
                var queryResult = (from ph in query
                                   join pd in context.LgsStockAdjustmentHeaders on ph.LgsStockAdjustmentHeaderID equals pd.LgsStockAdjustmentHeaderID
                                   join pm in context.LgsProductMasters on ph.ProductID equals pm.LgsProductMasterID
                                   join um in context.UnitOfMeasures on ph.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.LgsDepartments on pm.DepartmentID equals d.LgsDepartmentID
                                   join c in context.LgsCategories on pm.CategoryID equals c.LgsCategoryID
                                   join s in context.LgsSubCategories on pm.SubCategoryID equals s.LgsSubCategoryID
                                   join l in context.Locations on ph.LocationID equals l.LocationID
                                   where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                                   group new {ph, pd, pm, um, d, c, s, l} by new
                                       {
                                           ph.DocumentDate,
                                           pd.DocumentNo,
                                           pd.Remark,
                                           pm.ProductCode,
                                           pm.BarCode,
                                           pm.ProductName,
                                           um.UnitOfMeasureName,
                                           d.DepartmentCode,
                                           d.DepartmentName,
                                           c.CategoryCode,
                                           c.CategoryName,
                                           s.SubCategoryCode,
                                           s.SubCategoryName,
                                           ph.CostPrice,
                                           ph.CurrentQty,
                                           ph.OrderQty,
                                           l.LocationCode,
                                           l.LocationName
                                       }
                                   into g
                                   select new
                                       {
                                           FieldString1 = EntityFunctions.TruncateTime(g.Key.DocumentDate),
                                           FieldString2 = g.Key.LocationCode + " - " + g.Key.LocationName,
                                           FieldString3 = g.Key.DepartmentCode + " - " + g.Key.DepartmentName,
                                           FieldString4 = g.Key.CategoryCode + " - " + g.Key.CategoryName,
                                           FieldString5 = g.Key.SubCategoryCode + " - " + g.Key.SubCategoryName,
                                           FieldString6 = g.Key.ProductCode,
                                           FieldString7 = g.Key.ProductName,
                                           FieldString8 = g.Key.UnitOfMeasureName,
                                           FieldString9 = g.Key.Remark,
                                           FieldDecimal1 = (stockAdjustmentMode == 2 ? g.Sum(x => x.ph.ShortageQty) : g.Sum(x => x.ph.ExcessQty)),
                                           FieldDecimal2 = (g.Key.CurrentQty != 0 ? ((g.Sum(x => x.ph.OrderQty) / g.Key.CurrentQty) * 100) : 0)
                                       }).ToArray();

                return queryResult.ToDataTable();
            }
            else
            {
                var queryResult = (from ph in query
                                   join pd in context.LgsStockAdjustmentHeaders on ph.LgsStockAdjustmentHeaderID equals pd.LgsStockAdjustmentHeaderID
                                   join pm in context.LgsProductMasters on ph.ProductID equals pm.LgsProductMasterID
                                   join um in context.UnitOfMeasures on ph.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.LgsDepartments on pm.DepartmentID equals d.LgsDepartmentID
                                   join c in context.LgsCategories on pm.CategoryID equals c.LgsCategoryID
                                   join s in context.LgsSubCategories on pm.SubCategoryID equals s.LgsSubCategoryID
                                   join l in context.Locations on ph.LocationID equals l.LocationID
                                   where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                                   group new { ph, pd, pm, um, d, c, s, l } by new
                                   {
                                       ph.DocumentDate,
                                       pd.DocumentNo,
                                       pd.Remark,
                                       pm.ProductCode,
                                       pm.BarCode,
                                       pm.ProductName,
                                       um.UnitOfMeasureName,
                                       d.DepartmentCode,
                                       d.DepartmentName,
                                       c.CategoryCode,
                                       c.CategoryName,
                                       s.SubCategoryCode,
                                       s.SubCategoryName,
                                       ph.CostPrice,
                                       ph.CostValue,
                                       ph.SellingPrice,
                                       ph.SellingValue,
                                       ph.CurrentQty,
                                       ph.OrderQty,
                                       l.LocationCode,
                                       l.LocationName
                                   }
                                       into g
                                       select new
                                       {
                                           FieldString1 = EntityFunctions.TruncateTime(g.Key.DocumentDate),
                                           FieldString2 = g.Key.LocationCode + " - " + g.Key.LocationName,
                                           FieldString3 = g.Key.DepartmentCode + " - " + g.Key.DepartmentName,
                                           FieldString4 = g.Key.CategoryCode + " - " + g.Key.CategoryName,
                                           FieldString5 = g.Key.SubCategoryCode + " - " + g.Key.SubCategoryName,
                                           FieldString6 = g.Key.ProductCode,
                                           FieldString7 = g.Key.ProductName,
                                           FieldString8 = g.Key.UnitOfMeasureName,
                                           FieldString9 = g.Key.Remark,
                                           FieldDecimal1 = g.Sum(x => x.ph.ExcessQty),
                                           FieldDecimal2 = g.Sum(x => x.ph.ShortageQty),
                                           FieldDecimal3 = g.Sum(x => x.ph.OverWriteQty),
                                           FieldDecimal4 = (g.Key.CurrentQty != 0 ? ((g.Sum(x => x.ph.OrderQty) / g.Key.CurrentQty) * 100) : 0),
                                           FieldDecimal5 = g.Key.CostValue,
                                           FieldDecimal6 = g.Key.SellingValue,
                                       }).ToArray();

                dtResult = queryResult.ToDataTable();
            }
            

            return dtResult;
        }

        public DataTable GetStockAdjustmentDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            var query = context.LgsStockAdjustmentHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);
            
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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            //var queryResultData = (dynamic)null;
            DataTable dtResult = new DataTable();
            string refStockAdjustmentMode = ((int)LookUpReference.StockAdjustmentMode).ToString();
            
            var queryResult = (from h in query
                                join r in context.ReferenceTypes on h.StockAdjustmentMode equals r.LookupKey
                                where r.LookupType == refStockAdjustmentMode
                                select
                                    new
                                    {
                                        FieldString1 = h.DocumentNo,
                                        FieldString2 = DbFunctions.TruncateTime(h.DocumentDate),
                                        FieldString3 = r.LookupValue,
                                        FieldString4 = h.ReferenceDocumentNo,
                                        FieldString5 = h.Remark
                                    }).ToArray();

                dtResult = queryResult.ToDataTable();          

            return dtResult;
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;
            int stockAdjustmentMode = 0;
            IQueryable qryResult;
            if (autoGenerateInfo.FormName == "FrmLogisticStockAdjustment")
            {
                qryResult = context.LgsStockAdjustmentHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, autoGeneratePurchaseInfo.DocumentID);
            }
            else if(autoGenerateInfo.FormName=="RptExcessStockAdjustment")
            { 
                stockAdjustmentMode = 1;
                // Create query 
                qryResult = context.LgsStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else if (autoGenerateInfo.FormName == "RptShortageStockAdjustment")
            { 
                stockAdjustmentMode = 2;
                qryResult = context.LgsStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else
            {
                qryResult = context.LgsStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1", 1, documentID);
            }
            
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "ProductID":

                    qryResult = qryResult.Join(context.LgsProductMasters, reportDataStruct.DbColumnName.Trim(), "LgsProductMasterID", "new(inner.ProductCode)")
                                .OrderBy("ProductCode")
                                .GroupBy("new(ProductCode)", "new(ProductCode)")
                                .Select("Key.ProductCode");
                    break;
                case "DepartmentID":

                    qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID", "new(inner.LgsProductMasterID, inner.DepartmentID)")
                                .OrderBy("DepartmentID")
                                .GroupBy("new(DepartmentID)", "new(DepartmentID)")
                                .Select("new(Key.DepartmentID)");
                    qryResult = qryResult.Join(context.LgsDepartments, "DepartmentID", "LgsDepartmentID", "new(inner.DepartmentCode)")
                                .OrderBy("DepartmentCode")
                                .GroupBy("new(DepartmentCode)", "new(DepartmentCode)")
                                .Select("Key.DepartmentCode");
                    break;
                case "CategoryID":

                    qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID", "new(inner.LgsProductMasterID, inner.CategoryID)")
                                .OrderBy("CategoryID")
                                .GroupBy("new(CategoryID)", "new(CategoryID)")
                                .Select("new(Key.CategoryID)");
                    qryResult = qryResult.Join(context.LgsCategories, "CategoryID", "LgsCategoryID", "new(inner.CategoryCode)")
                                .OrderBy("CategoryCode")
                                .GroupBy("new(CategoryCode)", "new(CategoryCode)")
                                .Select("Key.CategoryCode");
                    break;
                case "SubCategoryID":

                    qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID", "new(inner.LgsProductMasterID, inner.SubCategoryID)")
                                .OrderBy("SubCategoryID")
                                .GroupBy("new(SubCategoryID)", "new(SubCategoryID)")
                                .Select("new(Key.SubCategoryID)");
                    qryResult = qryResult.Join(context.LgsSubCategories, "SubCategoryID", "LgsSubCategoryID", "new(inner.SubCategoryCode)")
                                .OrderBy("SubCategoryCode")
                                .GroupBy("new(SubCategoryCode)", "new(SubCategoryCode)")
                                .Select("Key.SubCategoryCode");
                    break;
                case "UnitOfMeasureID":

                    qryResult = qryResult.Join(context.UnitOfMeasures, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.UnitOfMeasureName)")
                                .OrderBy("UnitOfMeasureName")
                                .GroupBy("new(UnitOfMeasureName)", "new(UnitOfMeasureName)")
                                .Select("Key.UnitOfMeasureName");
                    break;
                    
                case "StockAdjustmentMode":                                        
                    qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", ((int)LookUpReference.StockAdjustmentMode).ToString()), "StockAdjustmentMode", "LookupKey", "new(inner.LookupValue)").
                                OrderBy("LookupValue").Select("LookupValue");
                    break;
                case "LocationID":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)").
                                OrderBy("LocationCode")
                                .GroupBy("new(LocationCode)", "new(LocationCode)")
                                .Select("Key.LocationCode");
                    break;
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
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
                    { selectionDataList.Add(item.ToString()); }
                }
            }
            return selectionDataList;
        }

        /// <summary>
        /// Get stock adjustment details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockAdjustmentTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            string lookupType = ((int)LookUpReference.StockAdjustmentMode).ToString();
            var query = (
                        from ph in context.LgsStockAdjustmentHeaders
                        join pd in context.LgsStockAdjustmentDetails on ph.LgsStockAdjustmentHeaderID equals pd.LgsStockAdjustmentHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join r in context.ReferenceTypes on ph.StockAdjustmentMode equals r.LookupKey
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                            && r.LookupType.Equals(lookupType)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = ph.Narration,
                                FieldString4 = ph.Remark,
                                FieldString5 = "", // P Req. No
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = "",
                                FieldString9 = ph.ReferenceDocumentNo,
                                FieldString10 = r.LookupValue,
                                FieldString11 = "",
                                FieldString12 = l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = "",//pd.PackSize,
                                FieldString17 = pd.OrderQty,
                                FieldString18 = pd.SellingPrice,
                                FieldString19 = pd.CostPrice,
                                FieldString20 = "",//pd.DiscountPercentage,
                                FieldString21 = pd.SellingValue,
                                FieldString22 = pd.CostValue,
                                FieldString23 = ph.TotalSellingtValue,
                                FieldString24 = ph.TotalCostValue,
                                FieldString25 = "",//ph.DiscountAmount,
                                FieldString26 = "",//ph.TaxAmount,
                                FieldString27 = "",//ph.OtherCharges,
                                FieldString28 = "",//ph.NetAmount,
                            });

           return query.AsEnumerable().ToDataTable();

        }

        public string[] GetAllProductCodes()
        {
            //List<string> ProductCodeList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();


            List<string> ProductCodeList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductCodeList.ToArray(); 
        }

        public string[] GetAllProductNames()
        {

            //List<string> ProductNameList = context.LgsProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();

        }
    }
}
