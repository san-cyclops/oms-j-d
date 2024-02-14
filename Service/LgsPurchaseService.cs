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
using System.Data.Entity.Validation;
using MoreLinq;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Service
{
    public class LgsPurchaseService
    {
        //By Pravin
        ERPDbContext context = new ERPDbContext();
        string sql;
        public static bool poIsMandatory;
        int poMandatory; // 1-yes  2-no 


        public LgsPurchaseDetailTemp GetPurchaseDetailTemp(List<LgsPurchaseDetailTemp> LgsPurchaseDetailTemp, LgsProductMaster lgsProductMaster,int locationID, DateTime expiryDate,long unitOfMeasureID)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();
            decimal defaultConvertFactor=1;
            var purchaseDetailTemp=(from pm in context.LgsProductMasters
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
                                              ConvertFactor=defaultConvertFactor,
                                              pm.IsBatch
                                          }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(lgsProductMaster.UnitOfMeasureID))
            {
                purchaseDetailTemp=(dynamic)null;

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
                                              puc.ConvertFactor,
                                              pm.IsBatch
                                          }).ToArray();
            }
            
            foreach (var tempProduct in purchaseDetailTemp)
            {
                if(!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseDetailTemp = LgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID==unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsPurchaseDetailTemp = LgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    lgsPurchaseDetailTemp = LgsPurchaseDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();
                
                if (lgsPurchaseDetailTemp==null)
                {
                    lgsPurchaseDetailTemp=new LgsPurchaseDetailTemp();
                    lgsPurchaseDetailTemp.ProductID=tempProduct.LgsProductMasterID;
                    lgsPurchaseDetailTemp.ProductCode=tempProduct.ProductCode;
                    lgsPurchaseDetailTemp.ProductName=tempProduct.ProductName;
                    lgsPurchaseDetailTemp.UnitOfMeasureID=tempProduct.UnitOfMeasureID;
                    lgsPurchaseDetailTemp.CostPrice=tempProduct.CostPrice;
                    lgsPurchaseDetailTemp.SellingPrice=tempProduct.SellingPrice;
                    lgsPurchaseDetailTemp.AverageCost=tempProduct.AverageCost;
                    lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }
            return lgsPurchaseDetailTemp;
        }

        public LgsPurchaseDetailTemp GetPurchaseDetailTempForPRN(LgsPurchaseHeader lgsPurchaseHeader, LgsProductMaster lgsProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp(); 
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
                                          ConvertFactor = defaultConvertFactor,
                                          pm.IsBatch
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
                                          puc.ConvertFactor,
                                          pm.IsBatch
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                lgsPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseDetailTemp.ExpiryDate = expiryDate;
                lgsPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseDetailTemp.AverageCost = tempProduct.AverageCost;
                lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                //lgsPurchaseDetailTemp.BatchNo = lgsPurchaseHeader.BatchNo;  //(pd.IsBatch == true ? pd.BatchNo : string.Empty),
                lgsPurchaseDetailTemp.BatchNo = (tempProduct.IsBatch == true ? lgsPurchaseHeader.BatchNo : string.Empty);
                lgsPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
            }
            return lgsPurchaseDetailTemp;
        }


        public List<LgsPurchaseDetailTemp> getUpdatePurchaseDetailTemp(List<LgsPurchaseDetailTemp> lgsPurchaseTempDetail, LgsPurchaseDetailTemp lgsPurchaseDetail, LgsProductMaster lgsProductMaster, bool isRecallPo)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

            if (lgsProductMaster.IsExpiry)
            {
                lgsPurchaseDetailTemp = lgsPurchaseTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID && (p.ExpiryDate == lgsPurchaseDetail.ExpiryDate || p.ExpiryDate == null)).FirstOrDefault();
                
                if (isRecallPo)
                {
                    lgsPurchaseDetailTemp = lgsPurchaseTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
                }
            }
            else
            {
                lgsPurchaseDetailTemp = lgsPurchaseTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
            }

            if (lgsPurchaseDetailTemp == null || lgsPurchaseDetailTemp.LineNo.Equals(0))
            {
                if (lgsPurchaseTempDetail.Count.Equals(0))
                    lgsPurchaseDetail.LineNo = 1;
                else
                    lgsPurchaseDetail.LineNo = lgsPurchaseTempDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsPurchaseTempDetail.Remove(lgsPurchaseDetailTemp);
                lgsPurchaseDetail.LineNo = lgsPurchaseDetailTemp.LineNo;
            }

            lgsPurchaseTempDetail.Add(lgsPurchaseDetail);

            return lgsPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfQty(decimal qty, long productID, int unitOfMeasureID, long purchaseHeaderID)
        {
            decimal poQty = 0;
            var getCurrentQty = (from a in context.LgsPurchaseDetails
                                 where a.ProductID.Equals(productID) && a.LgsPurchaseHeaderID.Equals(purchaseHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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

        public List<LgsPurchaseDetailTemp> getDeletePurchaseDetailTemp(List<LgsPurchaseDetailTemp> lgsPurchaseTempDetail, LgsPurchaseDetailTemp lgsPurchaseDetail, List<InvProductSerialNoTemp> lgsProductSerialNoListToDelete, out List<InvProductSerialNoTemp> lgsProductSerialNoList)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();


            //lgsPurchaseDetailTemp = lgsPurchaseTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID && p.ExpiryDate == lgsPurchaseDetail.ExpiryDate || p.ExpiryDate == null).FirstOrDefault();
            lgsPurchaseDetailTemp = lgsPurchaseTempDetail.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
           
            long removedLineNo = 0;
            if (lgsPurchaseDetailTemp != null)
            {
                lgsPurchaseTempDetail.Remove(lgsPurchaseDetailTemp);
                removedLineNo = lgsPurchaseDetailTemp.LineNo;

            }

            lgsPurchaseTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);


            if (lgsProductSerialNoListToDelete != null)
            {
                lgsProductSerialNoTempList = lgsProductSerialNoListToDelete.Where(p => p.ProductID == lgsPurchaseDetail.ProductID && p.UnitOfMeasureID == lgsPurchaseDetail.UnitOfMeasureID && p.ExpiryDate == lgsPurchaseDetail.ExpiryDate).ToList();
            }

            foreach (InvProductSerialNoTemp LgsProductSerialNoTempDelete in lgsProductSerialNoTempList)
            {
                lgsProductSerialNoListToDelete.Remove(LgsProductSerialNoTempDelete);
            }

            lgsProductSerialNoList = lgsProductSerialNoListToDelete;

            return lgsPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<InvProductSerialNoTemp> getUpdateSerialNoTemp(List<InvProductSerialNoTemp> lgsProductSerialNoList,  InvProductSerialNoTemp lgsProductSerialNumber)
        {
            InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();

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

        public bool IsValidNoOfSerialNo(List<InvProductSerialNoTemp> lgsProductSerialNoList, InvProductSerialNoTemp lgsProductSerialNumber, decimal Qty)
        {
            if(Qty.Equals(lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Count()))
               return true;
            else
                return false;
        }

        public List<LgsPurchaseDetailTemp> GetPausedPurchaseDetail(LgsPurchaseHeader lgsPurchaseHeader)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp;
            List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join pd in context.LgsPurchaseDetails on pm.LgsProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pd.LocationID.Equals(lgsPurchaseHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(lgsPurchaseHeader.DocumentID) && pd.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.PurchaseHeaderID)
                                      select new
                                      {
                                          pd.AvgCost,
                                          pd.ConvertFactor,
                                          pd.CostPrice,
                                          pd.CurrentQty,
                                          pd.DiscountAmount,
                                          pd.DiscountPercentage,
                                          ExpiryDate = pd.ExpiryDate,
                                          pd.FreeQty,
                                          pd.GrossAmount,
                                          pd.LineNo,
                                          pd.LocationID,
                                          pd.NetAmount,
                                          pd.OrderQty,
                                          pm.ProductCode,
                                          pd.ProductID,
                                          pm.ProductName,
                                          pd.BalanceQty,
                                          pd.SellingPrice,
                                          pd.SubTotalDiscount,
                                          pd.TaxAmount1,
                                          pd.TaxAmount2,
                                          pd.TaxAmount3,
                                          pd.TaxAmount4,
                                          pd.TaxAmount5,
                                          pd.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          pm.IsBatch,
                                          pd.BatchNo
                                          
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

                lgsPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                lgsPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                lgsPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                lgsPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                lgsPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;
                   
                lgsPurchaseDetailTempList.Add(lgsPurchaseDetailTemp);
            }
            return lgsPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsPurchaseDetailTemp> GetPausedPurchaseDetailToPRN(LgsPurchaseHeader lgsPurchaseHeader, int documentID) 
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp;
            List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join pd in context.LgsPurchaseDetails on pm.LgsProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.LgsPurchaseHeaders on pd.LgsPurchaseHeaderID equals ph.LgsPurchaseHeaderID
                                      where pd.LocationID.Equals(lgsPurchaseHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(lgsPurchaseHeader.DocumentID) && pd.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.PurchaseHeaderID)
                                      select new
                                      {
                                          pd.AvgCost,
                                          pd.ConvertFactor,
                                          pd.CostPrice,
                                          pd.CurrentQty,
                                          pd.DiscountAmount,
                                          pd.DiscountPercentage,
                                          pd.ExpiryDate,
                                          pd.FreeQty,
                                          pd.GrossAmount,
                                          pd.LineNo,
                                          pd.LocationID,
                                          pd.NetAmount,
                                          pd.OrderQty,
                                          pm.ProductCode,
                                          pd.ProductID,
                                          pm.ProductName,
                                          pd.BalanceQty,
                                          pd.SellingPrice,
                                          pd.SubTotalDiscount,
                                          pd.TaxAmount1,
                                          pd.TaxAmount2,
                                          pd.TaxAmount3,
                                          pd.TaxAmount4,
                                          pd.TaxAmount5,
                                          pd.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          pd.BatchNo,
                                          pd.IsBatch
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

                lgsPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                lgsPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                lgsPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                lgsPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;
                lgsPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;

                lgsPurchaseDetailTempList.Add(lgsPurchaseDetailTemp);
            }
            return lgsPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public LgsPurchaseHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsPurchaseHeaders.Where(ph => ph.DocumentNo == documentNo.Trim() && ph.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public LgsPurchaseHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.LgsPurchaseHeaders.Where(po => po.LgsPurchaseHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public LgsPurchaseHeader GetLgsPurchaseHeaderByDocumentNo(int documentID, string documentNo, int locationID)  
        {
            return context.LgsPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }


        public List<LgsPurchaseDetailTemp> GetPurchaseOrderDetail(LgsPurchaseOrderHeader lgsPurchaseOrderHeader)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp;

            List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();


            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join pod in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pod.ProductID
                                      join uc in context.UnitOfMeasures on pod.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pod.LocationID.Equals(lgsPurchaseOrderHeader.LocationID) && pod.CompanyID.Equals(lgsPurchaseOrderHeader.CompanyID) && pod.BalanceQty > 0
                                      && pod.DocumentID.Equals(lgsPurchaseOrderHeader.DocumentID) && pod.LgsPurchaseOrderHeaderID.Equals(lgsPurchaseOrderHeader.LgsPurchaseOrderHeaderID)
                                      select new
                                      {
                                          pm.AverageCost,
                                          pod.ConvertFactor,
                                          pod.CostPrice,
                                          pod.CurrentQty,
                                          pod.DiscountAmount,
                                          pod.DiscountPercentage,
                                          pod.GrossAmount,
                                          pod.LineNo,
                                          pod.LocationID,
                                          pod.NetAmount,
                                          pod.OrderQty,
                                          pm.ProductCode,
                                          pod.ProductID,
                                          pm.ProductName,
                                          pod.BalanceQty,
                                          pod.BalanceFreeQty,
                                          pm.SellingPrice,
                                          pod.SubTotalDiscount,
                                          pod.TaxAmount1,
                                          pod.TaxAmount2,
                                          pod.TaxAmount3,
                                          pod.TaxAmount4,
                                          pod.TaxAmount5,
                                          pod.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          BaseUnitID = pm.UnitOfMeasureID,
                                          pod.IsBatch

                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {

                lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

                lgsPurchaseDetailTemp.AverageCost = tempProduct.AverageCost;
                lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseDetailTemp.DiscountAmount = ((tempProduct.BalanceQty * tempProduct.DiscountPercentage * tempProduct.CostPrice)/100);
                lgsPurchaseDetailTemp.DiscountPercentage =tempProduct.DiscountPercentage;
                lgsPurchaseDetailTemp.ExpiryDate = null;
                lgsPurchaseDetailTemp.FreeQty = tempProduct.BalanceFreeQty;
                lgsPurchaseDetailTemp.GrossAmount =Common.ConvertStringToDecimalCurrency((tempProduct.BalanceQty * tempProduct.CostPrice).ToString());
                lgsPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseDetailTemp.NetAmount =Common.ConvertStringToDecimalCurrency(((tempProduct.BalanceQty * tempProduct.CostPrice) - ((tempProduct.BalanceQty * tempProduct.DiscountPercentage * tempProduct.CostPrice) / 100)).ToString());
                lgsPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseDetailTemp.Qty = Common.ConvertDecimalToDecimalQty(tempProduct.BalanceQty);
                lgsPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseDetailTemp.BaseUnitID = tempProduct.BaseUnitID;
                lgsPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                
                lgsPurchaseDetailTempList.Add(lgsPurchaseDetailTemp);

            }
            return lgsPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvProductSerialNoTemp> getPausedPurchaseSerialNoDetail(LgsPurchaseHeader lgsPurchaseHeader)
        {
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<LgsProductSerialNoDetail> lgsProductSerialNoDetail = new List<LgsProductSerialNoDetail>();

            lgsProductSerialNoDetail = context.LgsProductSerialNoDetails.Where(ps=> ps.ReferenceDocumentID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID)).ToList();
            foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailTemp in lgsProductSerialNoDetail)
            {
                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();

                lgsProductSerialNoTemp.ExpiryDate = lgsProductSerialNoDetailTemp.ExpiryDate;
                lgsProductSerialNoTemp.LineNo = lgsProductSerialNoDetailTemp.LineNo;
                lgsProductSerialNoTemp.ProductID = lgsProductSerialNoDetailTemp.ProductID;
                lgsProductSerialNoTemp.SerialNo = lgsProductSerialNoDetailTemp.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoDetailTemp.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }
            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public List<OtherExpenseTemp> getPausedExpence(LgsPurchaseHeader lgsPurchaseHeader)
        {
            List<OtherExpenseTemp> otherExpenceTempList = new List<OtherExpenseTemp>();
            List<AccGlTransactionDetail> accGlTransactionDetails = new List<AccGlTransactionDetail>();
            AccLedgerAccountService AccLedgerAccountService = new AccLedgerAccountService();

            accGlTransactionDetails = context.AccGlTransactionDetails.Where(ps => ps.ReferenceDocumentID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID)).OrderBy(ps => ps.AccGlTransactionDetailID).ToList();
            foreach (AccGlTransactionDetail accGlTransactionDetailTemp in accGlTransactionDetails)
            {
                OtherExpenseTemp otherExpenceTemp = new OtherExpenseTemp();
                AccLedgerAccount accLedgerAccount = new Domain.AccLedgerAccount();

                accLedgerAccount = AccLedgerAccountService.GetAccLedgerExpenseAccountByID(accGlTransactionDetailTemp.LedgerID);

                if (accLedgerAccount != null)
                {
                    otherExpenceTemp.AccLedgerAccountID = accGlTransactionDetailTemp.LedgerID;
                    otherExpenceTemp.ExpenseAmount = accGlTransactionDetailTemp.Amount;
                    otherExpenceTemp.LedgerCode = accLedgerAccount.LedgerCode;
                    otherExpenceTemp.LedgerName = accLedgerAccount.LedgerName;

                    otherExpenceTempList.Add(otherExpenceTemp);
                }

            }

            return otherExpenceTempList.OrderBy(pd => pd.OtherExpenseTempID).ToList(); ;

        }

        public List<PaymentTemp> getPausedPayment(LgsPurchaseHeader lgsPurchaseHeader)
        {
            List<PaymentTemp> paymentTempList = new List<PaymentTemp>();
            List<AccPaymentDetail> accPaymentDetail = new List<AccPaymentDetail>();
            PaymentMethodService paymentMethodService = new PaymentMethodService();

            accPaymentDetail = context.AccPaymentDetails.Where(ps => ps.ReferenceDocumentID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID)).OrderBy(ps => ps.AccPaymentDetailID).ToList();
            foreach (AccPaymentDetail accPaymentDetailTemp in accPaymentDetail)
            {
                PaymentTemp paymentTemp = new PaymentTemp();
                AccLedgerAccount accLedgerAccount = new Domain.AccLedgerAccount();
                BankService bankService=new BankService();

                Bank bank = new Bank();
                bank = bankService.GetBankByID(accPaymentDetailTemp.BankID);

                paymentTemp.AccLedgerAccountID = 0;
                paymentTemp.BankID = accPaymentDetailTemp.BankID;

                if (bank != null)
                {
                    paymentTemp.BankCode = bank.BankCode;
                    paymentTemp.BankName = bank.BankName;
                }
                
                paymentTemp.CardCheqNo = accPaymentDetailTemp.CardNo;
                paymentTemp.ChequeDate = accPaymentDetailTemp.ChequeDate;
                paymentTemp.PayAmount = accPaymentDetailTemp.Amount;
                paymentTemp.PaymentMethod = paymentMethodService.GetPaymentMethodsByTypeID(accPaymentDetailTemp.paymentModeID).PaymentMethodName;
                paymentTemp.PaymentMethodID = accPaymentDetailTemp.paymentModeID;
                paymentTempList.Add(paymentTemp);

            }

            return paymentTempList.OrderBy(pd => pd.AccLedgerAccountID).ToList(); ;
        }

        public LgsPurchaseHeader getPurchaseHeaderByDocumentNo(int documentID,string documentNo,int locationID)
        {
            return context.LgsPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public LgsPurchaseHeader getPausedPurchaseHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.LgsPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.PurchaseHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
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

        public List<LgsProductBatchNoTemp> GetExpiryDetail(LgsProductMaster lgsProductMaster, string batchNo)
        {
            List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoDetail = new List<LgsProductBatchNoExpiaryDetail>();

            lgsProductBatchNoDetail = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && ps.BatchNo.Equals(batchNo) && ps.BalanceQty > 0).ToList();
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

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp lgsProductSerialNoTemp, int locationID)
        {
            LgsPurchaseHeader lgsPurchaseHeader = new LgsPurchaseHeader();
            lgsPurchaseHeader = getPurchaseHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo,locationID);
            if (lgsPurchaseHeader == null)
                lgsPurchaseHeader = new LgsPurchaseHeader();

            LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
            LgsProductSerialNoDetailtmp = null;

            if (lgsProductSerialNoTemp.ExpiryDate != null)
            {
                LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                            && sd.ReferenceDocumentDocumentID == lgsProductSerialNoTemp.DocumentID
                                            && sd.ReferenceDocumentID != lgsPurchaseHeader.PurchaseHeaderID
                                            && sd.ProductID == lgsProductSerialNoTemp.ProductID
                                            && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID
                                            && sd.ExpiryDate == lgsProductSerialNoTemp.ExpiryDate
                                            && sd.DocumentStatus == 0).FirstOrDefault();
            }
            else
            {
                LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                           && sd.ReferenceDocumentDocumentID == lgsProductSerialNoTemp.DocumentID
                                           && sd.ReferenceDocumentID != lgsPurchaseHeader.PurchaseHeaderID
                                           && sd.ProductID == lgsProductSerialNoTemp.ProductID
                                           && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID
                                           && sd.DocumentStatus == 0).FirstOrDefault();
            }
            if (LgsProductSerialNoDetailtmp != null)
            {
                lgsPurchaseHeader = getPausedPurchaseHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID,locationID);
                if (lgsPurchaseHeader!=null)
                    return lgsPurchaseHeader.DocumentNo;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
           
        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp lgsProductSerialNoTemp, int locationID)
        {

            LgsProductSerialNo lgsProductSerialNo = new LgsProductSerialNo();
            if (lgsProductSerialNoTemp.ExpiryDate != null)
                lgsProductSerialNo = context.LgsProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == lgsProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID && sd.ExpiryDate==lgsProductSerialNoTemp.ExpiryDate && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();
            else
                lgsProductSerialNo = context.LgsProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == lgsProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID &&  sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();

            if (lgsProductSerialNo != null && lgsProductSerialNo.LgsProductSerialNoID != 0)
                return true;
            else
                return false;
        }


        public bool Save(LgsPurchaseHeader lgsPurchaseHeader, List<LgsPurchaseDetailTemp> lgsPurchaseDetailTemp, List<InvProductSerialNoTemp> LgsProductSerialNoTemp, out string newDocumentNo, List<OtherExpenseTemp> otherExpenceTempList, List<PaymentTemp> paymentTempList, decimal paidAmount, string formName = "", bool isTStatus = false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();

                #region Purchase Header Save

                if (lgsPurchaseHeader.DocumentStatus.Equals(1))
                {
                    LgsPurchaseService LgsPurchaseServices = new LgsPurchaseService();
                    LocationService locationService = new LocationService();
                    lgsPurchaseHeader.DocumentNo = LgsPurchaseServices.GetDocumentNo(formName, lgsPurchaseHeader.LocationID, locationService.GetLocationsByID(lgsPurchaseHeader.LocationID).LocationCode, lgsPurchaseHeader.DocumentID, false).Trim();
                    if (!poIsMandatory) { lgsPurchaseHeader.BatchNo = lgsPurchaseHeader.DocumentNo; }
                }
                newDocumentNo = lgsPurchaseHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (lgsPurchaseHeader.LgsPurchaseHeaderID.Equals(0))
                    context.LgsPurchaseHeaders.Add(lgsPurchaseHeader);
                else
                    context.Entry(lgsPurchaseHeader).State = EntityState.Modified;

                context.SaveChanges();


                #endregion

                #region Deleted



                #endregion

                #region Purchase Detail Save

                context.Set<LgsPurchaseDetail>().Delete(context.LgsPurchaseDetails.Where(p => p.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.LgsPurchaseHeaderID) && p.LocationID.Equals(lgsPurchaseHeader.LocationID) && p.DocumentID.Equals(lgsPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                #region UsingSQL



                #endregion

                #region Deleted



                #endregion

                var lgsPurchaseDetailSaveQuery = (from pd in lgsPurchaseDetailTemp
                                                  select new
                                                  {
                                                      LgsPurchaseDetailID = 0,
                                                      PurchaseDetailID = 0,
                                                      PurchaseHeaderID = lgsPurchaseHeader.LgsPurchaseHeaderID,
                                                      CompanyID = lgsPurchaseHeader.CompanyID,
                                                      LocationID = lgsPurchaseHeader.LocationID,
                                                      CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                      DocumentID = lgsPurchaseHeader.DocumentID,
                                                      LineNo = pd.LineNo,
                                                      ProductID = pd.ProductID,
                                                      UnitOfMeasureID = pd.UnitOfMeasureID,
                                                      BaseUnitID = pd.BaseUnitID,
                                                      ConvertFactor = pd.ConvertFactor,
                                                      ExpiryDate = pd.ExpiryDate,
                                                      OrderQty = pd.OrderQty,
                                                      Qty = pd.Qty,
                                                      FreeQty = pd.FreeQty,
                                                      CurrentQty = pd.CurrentQty,
                                                      QtyConvertFactor = (pd.Qty * pd.ConvertFactor),
                                                      FreeQtyConvertFactor = (pd.FreeQty * pd.ConvertFactor),
                                                      BalanceQty = pd.Qty,
                                                      CostPrice = pd.CostPrice,
                                                      SellingPrice = pd.SellingPrice,
                                                      AvgCost = pd.AverageCost,
                                                      GrossAmount = pd.GrossAmount,
                                                      DiscountPercentage = pd.DiscountPercentage,
                                                      DiscountAmount = pd.DiscountAmount,
                                                      SubTotalDiscount = pd.SubTotalDiscount,
                                                      TaxAmount1 = pd.TaxAmount1,
                                                      TaxAmount2 = pd.TaxAmount2,
                                                      TaxAmount3 = pd.TaxAmount3,
                                                      TaxAmount4 = pd.TaxAmount4,
                                                      TaxAmount5 = pd.TaxAmount5,
                                                      NetAmount = pd.NetAmount,
                                                      DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                      BatchNo = (pd.IsBatch == true ? lgsPurchaseHeader.BatchNo : string.Empty),
                                                      IsBatch = pd.IsBatch,
                                                      GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                      CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                      CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                      ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                      ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                      DataTransfer = lgsPurchaseHeader.DataTransfer
                                                  }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(lgsPurchaseDetailSaveQuery), "LgsPurchaseDetail");




                if (lgsPurchaseHeader.DocumentStatus.Equals(1))
                {
                    
                }

                #endregion

                #region Deleted



                #endregion

                #region Serial No Detail Save

                context.Set<LgsProductSerialNoDetail>().Delete(context.LgsProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(lgsPurchaseHeader.LgsPurchaseHeaderID) && p.LocationID.Equals(lgsPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                if (lgsPurchaseHeader.DocumentStatus.Equals(1))
                {
                    if (poIsMandatory)
                    {
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        LgsPurchaseOrderHeader ilgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                        ilgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(lgsPurchaseHeader.ReferenceDocumentID);

                        var lgsProductSerialNoSaveQuery = (from pd in LgsProductSerialNoTemp
                                                           select new
                                                           {
                                                               LgsProductSerialNoID = 0, 
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                               CompanyID = lgsPurchaseHeader.CompanyID,
                                                               LocationID = lgsPurchaseHeader.LocationID,
                                                               CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = ilgsPurchaseOrderHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = lgsPurchaseHeader.DocumentID,
                                                               DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                               CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                               CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                               ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                               ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                               DataTransfer = lgsPurchaseHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoSaveQuery), "LgsProductSerialNo");

                    }
                    else
                    {
                        var lgsProductSerialNoSaveQuery = (from pd in LgsProductSerialNoTemp
                                                           select new
                                                           {
                                                               LgsProductSerialNoID = 0,
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                               CompanyID = lgsPurchaseHeader.CompanyID,
                                                               LocationID = lgsPurchaseHeader.LocationID,
                                                               CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = lgsPurchaseHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = lgsPurchaseHeader.DocumentID,
                                                               DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                               CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                               CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                               ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                               ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                               DataTransfer = lgsPurchaseHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoSaveQuery), "LgsProductSerialNo");
                    }
                }
                else
                {
                    if (poIsMandatory)
                    {
                        LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
                        LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                        lgsPurchaseOrderHeader = lgsPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(lgsPurchaseHeader.ReferenceDocumentID);

                        var lgsProductSerialNoDetailSaveQuery = (from pd in LgsProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     LgsProductSerialNoDetailID = 0, 
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                                     CompanyID = lgsPurchaseHeader.CompanyID,
                                                                     LocationID = lgsPurchaseHeader.LocationID,
                                                                     CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = lgsPurchaseHeader.DocumentID,
                                                                     ReferenceDocumentID = lgsPurchaseHeader.LgsPurchaseHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                                     BatchNo = lgsPurchaseOrderHeader.DocumentNo,
                                                                     CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                                     CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                                     ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                                     ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                                     DataTransfer = lgsPurchaseHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoDetailSaveQuery), "LgsProductSerialNoDetail");

                    }
                    else
                    {
                        var lgsProductSerialNoDetailSaveQuery = (from pd in LgsProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     LgsProductSerialNoDetailID = 0, 
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                                     CompanyID = lgsPurchaseHeader.CompanyID,
                                                                     LocationID = lgsPurchaseHeader.LocationID,
                                                                     CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = lgsPurchaseHeader.DocumentID,
                                                                     ReferenceDocumentID = lgsPurchaseHeader.LgsPurchaseHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                                     BatchNo = lgsPurchaseHeader.DocumentNo,
                                                                     CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                                     CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                                     ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                                     ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                                     DataTransfer = lgsPurchaseHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoDetailSaveQuery), "LgsProductSerialNoDetail"); 
                    }
                }

                #region Deleted

               
                #endregion

                #endregion

                #region Account Entries

                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
                AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
                accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(lgsPurchaseHeader.LgsPurchaseHeaderID, lgsPurchaseHeader.DocumentID, lgsPurchaseHeader.LocationID);
                if (accGlTransactionHeader == null)
                    accGlTransactionHeader = new AccGlTransactionHeader();

                accGlTransactionHeader.Amount = lgsPurchaseHeader.NetAmount;
                accGlTransactionHeader.CompanyID = lgsPurchaseHeader.CompanyID;
                accGlTransactionHeader.CostCentreID = lgsPurchaseHeader.CostCentreID;
                accGlTransactionHeader.CreatedDate = lgsPurchaseHeader.CreatedDate;
                accGlTransactionHeader.CreatedUser = lgsPurchaseHeader.CreatedUser;
                accGlTransactionHeader.DocumentDate = lgsPurchaseHeader.DocumentDate;
                accGlTransactionHeader.DocumentID = lgsPurchaseHeader.DocumentID;
                accGlTransactionHeader.DocumentStatus = lgsPurchaseHeader.DocumentStatus;
                accGlTransactionHeader.DocumentNo = string.Empty;
                accGlTransactionHeader.GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID;
                accGlTransactionHeader.DataTransfer = lgsPurchaseHeader.DataTransfer;
                accGlTransactionHeader.IsUpLoad = false;
                accGlTransactionHeader.LocationID = lgsPurchaseHeader.LocationID;
                accGlTransactionHeader.ModifiedDate = lgsPurchaseHeader.ModifiedDate;
                accGlTransactionHeader.ModifiedUser = lgsPurchaseHeader.ModifiedUser;
                accGlTransactionHeader.ReferenceDocumentDocumentID = lgsPurchaseHeader.DocumentID;
                accGlTransactionHeader.ReferenceDocumentID = lgsPurchaseHeader.LgsPurchaseHeaderID;
                accGlTransactionHeader.ReferenceDocumentNo = lgsPurchaseHeader.DocumentNo;
                accGlTransactionHeader.ReferenceID = lgsPurchaseHeader.SupplierID;

                if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                    accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);
                else
                    accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);
                context.SaveChanges();

                context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(lgsPurchaseHeader.LgsPurchaseHeaderID) && p.LocationID.Equals(lgsPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

                if (otherExpenceTempList != null)
                {
                    var otherExpenceSaveQuery = (from oe in otherExpenceTempList
                                                 select new
                                                 {
                                                     AccGlTransactionDetailID = 0,
                                                     AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                     CompanyID = accGlTransactionHeader.CompanyID,
                                                     LocationID = accGlTransactionHeader.LocationID,
                                                     CostCentreID = accGlTransactionHeader.CostCentreID,
                                                     DocumentNo = accGlTransactionHeader.DocumentNo,
                                                     LedgerID = oe.AccLedgerAccountID,
                                                     LedgerSerial = 0,
                                                     DocumentID = accGlTransactionHeader.DocumentID,
                                                     DocumentDate = accGlTransactionHeader.DocumentDate,
                                                     PaymentDate = accGlTransactionHeader.DocumentDate,
                                                     ReferenceID = accGlTransactionHeader.ReferenceID,
                                                     DrCr = 1,
                                                     Amount = oe.ExpenseAmount,
                                                     TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                     ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                     ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                     ReferenceLocacationID = 0,
                                                     ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                     TransactionDefinitionId = 1,
                                                     ChequeNo = string.Empty,
                                                     Reference = "OTHER EXPENCE",
                                                     Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                     DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                     IsTStatus = isTStatus,
                                                     GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                     CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                     CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                     ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                     ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                     DataTransfer = lgsPurchaseHeader.DataTransfer
                                                 }).ToList();

                    CommonService.BulkInsert(Common.LINQToDataTable(otherExpenceSaveQuery), "AccGlTransactionDetail");
                }


                if (paymentTempList != null)
                {
                    var paymentAccountsSaveQuery = (from oe in paymentTempList
                                                    select new
                                                    {
                                                        AccGlTransactionDetailID = 0,
                                                        AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                        CompanyID = accGlTransactionHeader.CompanyID,
                                                        LocationID = accGlTransactionHeader.LocationID,
                                                        CostCentreID = accGlTransactionHeader.CostCentreID,
                                                        DocumentNo = accGlTransactionHeader.DocumentNo,
                                                        LedgerID = oe.AccLedgerAccountID,
                                                        LedgerSerial = 0,
                                                        DocumentID = accGlTransactionHeader.DocumentID,
                                                        DocumentDate = accGlTransactionHeader.DocumentDate,
                                                        PaymentDate = accGlTransactionHeader.DocumentDate,
                                                        ReferenceID = accGlTransactionHeader.ReferenceID,
                                                        DrCr = 2,
                                                        Amount = oe.PayAmount,
                                                        TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                        ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                        ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                        ReferenceLocacationID = 0,
                                                        ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                        TransactionDefinitionId = 1,
                                                        ChequeNo = string.Empty,
                                                        Reference = "PAYMENTS",
                                                        Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                        DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                        IsTStatus = isTStatus,
                                                        GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                        CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                        CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                        ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                        ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                        DataTransfer = lgsPurchaseHeader.DataTransfer
                                                    }).ToList();

                    CommonService.BulkInsert(Common.LINQToDataTable(paymentAccountsSaveQuery), "AccGlTransactionDetail");
                }

                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();
                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSupplier").DocumentID);

                if (accTransactionTypeDetail != null)
                {
                    var supplierAccountEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                         select new
                                                         {
                                                             AccGlTransactionDetailID = 0,
                                                             AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                             CompanyID = accGlTransactionHeader.CompanyID,
                                                             LocationID = accGlTransactionHeader.LocationID,
                                                             CostCentreID = accGlTransactionHeader.CostCentreID,
                                                             DocumentNo = accGlTransactionHeader.DocumentNo,
                                                             LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                             LedgerSerial = "",
                                                             DocumentID = accGlTransactionHeader.DocumentID,
                                                             DocumentDate = accGlTransactionHeader.DocumentDate,
                                                             PaymentDate = accGlTransactionHeader.DocumentDate,
                                                             ReferenceID = accGlTransactionHeader.ReferenceID,
                                                             DrCr = 2,
                                                             Amount = lgsPurchaseHeader.NetAmount - paidAmount,
                                                             TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                             ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                             ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                             ReferenceLocacationID = 0,
                                                             ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                             TransactionDefinitionId = 1,
                                                             ChequeNo = string.Empty,
                                                             Reference = "SUPPLIER",
                                                             Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                             DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                             IsTStatus = isTStatus,
                                                             GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                             CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                             CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                             ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                             ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                             DataTransfer = lgsPurchaseHeader.DataTransfer
                                                         }).ToList();


                    CommonService.BulkInsert(Common.LINQToDataTable(supplierAccountEntrySaveQuery), "AccGlTransactionDetail");
                }

                if (lgsPurchaseHeader.DiscountAmount > 0)
                {
                    accTransactionTypeDetail = new AccTransactionTypeDetail();
                    accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID, 2);

                    if (accTransactionTypeDetail != null)
                    {
                        var discountAccountEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                             select new
                                                             {
                                                                 AccGlTransactionDetailID = 0,
                                                                 AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                                 CompanyID = accGlTransactionHeader.CompanyID,
                                                                 LocationID = accGlTransactionHeader.LocationID,
                                                                 CostCentreID = accGlTransactionHeader.CostCentreID,
                                                                 DocumentNo = accGlTransactionHeader.DocumentNo,
                                                                 LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                                 LedgerSerial = 0,
                                                                 DocumentID = accGlTransactionHeader.DocumentID,
                                                                 DocumentDate = accGlTransactionHeader.DocumentDate,
                                                                 PaymentDate = accGlTransactionHeader.DocumentDate,
                                                                 ReferenceID = accGlTransactionHeader.ReferenceID,
                                                                 DrCr = 2,
                                                                 Amount = lgsPurchaseHeader.DiscountAmount,
                                                                 TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                                 ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                 ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                 ReferenceLocacationID = 0,
                                                                 ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                 TransactionDefinitionId = 2,
                                                                 ChequeNo = string.Empty,
                                                                 Reference = "DISCOUNT",
                                                                 Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                                 DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                                 IsTStatus = isTStatus,
                                                                 GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                                 CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                                 CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                                 ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                                 ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                                 DataTransfer = lgsPurchaseHeader.DataTransfer
                                                             }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(discountAccountEntrySaveQuery), "AccGlTransactionDetail");
                    }
                }

                #region Tax1

                DataTable dt = new DataTable();
                CommonService commonService = new CommonService();
                dt = commonService.GetAllTaxesRelatedToSupplier(lgsPurchaseHeader.SupplierID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0) // Tax1
                    {

                        if (lgsPurchaseHeader.TaxAmount1 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                     select new
                                                     {
                                                         AccGlTransactionDetailID = 0,
                                                         AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                         CompanyID = accGlTransactionHeader.CompanyID,
                                                         LocationID = accGlTransactionHeader.LocationID,
                                                         CostCentreID = accGlTransactionHeader.CostCentreID,
                                                         DocumentNo = accGlTransactionHeader.DocumentNo,
                                                         LedgerID = Common.ConvertStringToLong(dt.Rows[i]["AccLedgerAccountID"].ToString()),
                                                         LedgerSerial = 0,
                                                         DocumentID = accGlTransactionHeader.DocumentID,
                                                         DocumentDate = accGlTransactionHeader.DocumentDate,
                                                         PaymentDate = accGlTransactionHeader.DocumentDate,
                                                         ReferenceID = accGlTransactionHeader.ReferenceID,
                                                         DrCr = 1,
                                                         Amount = lgsPurchaseHeader.TaxAmount1,
                                                         TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocacationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 1",
                                                         Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                         DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                         CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                         ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                         DataTransfer = lgsPurchaseHeader.DataTransfer
                                                     }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 1) // Tax2
                    {
                        if (lgsPurchaseHeader.TaxAmount2 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                     select new
                                                     {
                                                         AccGlTransactionDetailID = 0,
                                                         AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                         CompanyID = accGlTransactionHeader.CompanyID,
                                                         LocationID = accGlTransactionHeader.LocationID,
                                                         CostCentreID = accGlTransactionHeader.CostCentreID,
                                                         DocumentNo = accGlTransactionHeader.DocumentNo,
                                                         LedgerID = Common.ConvertStringToLong(dt.Rows[i]["AccLedgerAccountID"].ToString()),
                                                         LedgerSerial = 0,
                                                         DocumentID = accGlTransactionHeader.DocumentID,
                                                         DocumentDate = accGlTransactionHeader.DocumentDate,
                                                         PaymentDate = accGlTransactionHeader.DocumentDate,
                                                         ReferenceID = accGlTransactionHeader.ReferenceID,
                                                         DrCr = 1,
                                                         Amount = lgsPurchaseHeader.TaxAmount2,
                                                         TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocacationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 2",
                                                         Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                         DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                         CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                         ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                         DataTransfer = lgsPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 2) // Tax3
                    {
                        if (lgsPurchaseHeader.TaxAmount3 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                     select new
                                                     {
                                                         AccGlTransactionDetailID = 0,
                                                         AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                         CompanyID = accGlTransactionHeader.CompanyID,
                                                         LocationID = accGlTransactionHeader.LocationID,
                                                         CostCentreID = accGlTransactionHeader.CostCentreID,
                                                         DocumentNo = accGlTransactionHeader.DocumentNo,
                                                         LedgerID = Common.ConvertStringToLong(dt.Rows[i]["AccLedgerAccountID"].ToString()),
                                                         LedgerSerial = 0,
                                                         DocumentID = accGlTransactionHeader.DocumentID,
                                                         DocumentDate = accGlTransactionHeader.DocumentDate,
                                                         PaymentDate = accGlTransactionHeader.DocumentDate,
                                                         ReferenceID = accGlTransactionHeader.ReferenceID,
                                                         DrCr = 1,
                                                         Amount = lgsPurchaseHeader.TaxAmount3,
                                                         TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocacationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 3",
                                                         Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                         DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                         CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                         ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                         DataTransfer = lgsPurchaseHeader.DataTransfer
                                                     }).ToList();
                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 3) // Tax4
                    {
                        if (lgsPurchaseHeader.TaxAmount4 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                     select new
                                                     {
                                                         AccGlTransactionDetailID = 0,
                                                         AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                         CompanyID = accGlTransactionHeader.CompanyID,
                                                         LocationID = accGlTransactionHeader.LocationID,
                                                         CostCentreID = accGlTransactionHeader.CostCentreID,
                                                         DocumentNo = accGlTransactionHeader.DocumentNo,
                                                         LedgerID = Common.ConvertStringToLong(dt.Rows[i]["AccLedgerAccountID"].ToString()),
                                                         LedgerSerial = 0,
                                                         DocumentID = accGlTransactionHeader.DocumentID,
                                                         DocumentDate = accGlTransactionHeader.DocumentDate,
                                                         PaymentDate = accGlTransactionHeader.DocumentDate,
                                                         ReferenceID = accGlTransactionHeader.ReferenceID,
                                                         DrCr = 1,
                                                         Amount = lgsPurchaseHeader.TaxAmount4,
                                                         TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocacationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 4",
                                                         Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                         DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                         CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                         ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                         DataTransfer = lgsPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 4) // Tax5
                    {
                        if (lgsPurchaseHeader.TaxAmount5 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                     select new
                                                     {
                                                         AccGlTransactionDetailID = 0,
                                                         AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                         CompanyID = accGlTransactionHeader.CompanyID,
                                                         LocationID = accGlTransactionHeader.LocationID,
                                                         CostCentreID = accGlTransactionHeader.CostCentreID,
                                                         DocumentNo = accGlTransactionHeader.DocumentNo,
                                                         LedgerID = Common.ConvertStringToLong(dt.Rows[i]["AccLedgerAccountID"].ToString()),
                                                         LedgerSerial = 0,
                                                         DocumentID = accGlTransactionHeader.DocumentID,
                                                         DocumentDate = accGlTransactionHeader.DocumentDate,
                                                         PaymentDate = accGlTransactionHeader.DocumentDate,
                                                         ReferenceID = accGlTransactionHeader.ReferenceID,
                                                         DrCr = 1,
                                                         Amount = lgsPurchaseHeader.TaxAmount5,
                                                         TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocacationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 5",
                                                         Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                         DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                         CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                         ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                         DataTransfer = lgsPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else // More than 5
                    {

                    }
                }

                #endregion


                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote").DocumentID);

                if (accTransactionTypeDetail != null)
                {
                    var purchaseAccountEntrySaveQuery = (from oe in lgsPurchaseDetailTemp
                                                         select new
                                                         {
                                                             AccGlTransactionDetailID = 0,
                                                             AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                                             CompanyID = accGlTransactionHeader.CompanyID,
                                                             LocationID = accGlTransactionHeader.LocationID,
                                                             CostCentreID = accGlTransactionHeader.CostCentreID,
                                                             DocumentNo = accGlTransactionHeader.DocumentNo,
                                                             LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                                             LedgerSerial = 0,
                                                             DocumentID = accGlTransactionHeader.DocumentID,
                                                             DocumentDate = accGlTransactionHeader.DocumentDate,
                                                             PaymentDate = accGlTransactionHeader.DocumentDate,
                                                             ReferenceID = accGlTransactionHeader.ReferenceID,
                                                             DrCr = 1,
                                                             Amount = lgsPurchaseHeader.GrossAmount,
                                                             TransactionTypeId = lgsPurchaseHeader.DocumentID,
                                                             ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                             ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                             ReferenceLocacationID = 0,
                                                             ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                             TransactionDefinitionId = 1,
                                                             ChequeNo = string.Empty,
                                                             Reference = "PURCHASE ACCOUNT",
                                                             Remark = "LOGISTIC GOOD RECEIVED NOTE",
                                                             DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                             IsTStatus = isTStatus,
                                                             GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                             CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                             CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                             ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                             ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                             DataTransfer = lgsPurchaseHeader.DataTransfer
                                                         }).ToList();


                    CommonService.BulkInsert(Common.LINQToDataTable(purchaseAccountEntrySaveQuery), "AccGlTransactionDetail");
                }


                #endregion

                #region Payment Entries

                AccPaymentService accPaymentService = new Service.AccPaymentService();
                AccPaymentHeader accPaymentHeader = new AccPaymentHeader();
                accPaymentHeader = accPaymentService.getAccPaymentHeader(lgsPurchaseHeader.LgsPurchaseHeaderID, lgsPurchaseHeader.DocumentID, lgsPurchaseHeader.LocationID);
                if (accPaymentHeader == null)
                    accPaymentHeader = new AccPaymentHeader();

                accPaymentHeader.Amount = lgsPurchaseHeader.NetAmount;
                accPaymentHeader.BalanceAmount = lgsPurchaseHeader.NetAmount - paidAmount;
                accPaymentHeader.CompanyID = lgsPurchaseHeader.CompanyID;
                accPaymentHeader.CostCentreID = lgsPurchaseHeader.CostCentreID;
                accPaymentHeader.CreatedDate = lgsPurchaseHeader.CreatedDate;
                accPaymentHeader.CreatedUser = lgsPurchaseHeader.CreatedUser;
                accPaymentHeader.DocumentDate = lgsPurchaseHeader.DocumentDate;
                accPaymentHeader.DocumentID = lgsPurchaseHeader.CompanyID;
                accPaymentHeader.DocumentStatus = lgsPurchaseHeader.DocumentStatus;
                accPaymentHeader.DocumentNo = lgsPurchaseHeader.DocumentNo;
                accPaymentHeader.GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID;
                accPaymentHeader.DataTransfer = lgsPurchaseHeader.DataTransfer;
                accPaymentHeader.IsUpLoad = false;
                accPaymentHeader.LocationID = lgsPurchaseHeader.LocationID;
                accPaymentHeader.ModifiedDate = lgsPurchaseHeader.ModifiedDate;
                accPaymentHeader.ModifiedUser = lgsPurchaseHeader.ModifiedUser;
                accPaymentHeader.PaymentDate = lgsPurchaseHeader.DocumentDate;
                accPaymentHeader.Reference = "LOGISTIC GOOD RECEIVED NOTE PAYMENT";
                accPaymentHeader.ReferenceDocumentDocumentID = lgsPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceDocumentID = lgsPurchaseHeader.LgsPurchaseHeaderID;
                accPaymentHeader.ReferenceID = lgsPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceLocationID = lgsPurchaseHeader.LocationID;
                accPaymentHeader.Remark = "LOGISTIC GOOD RECEIVED NOTE PAYMENT";

                if (accPaymentHeader.AccPaymentHeaderID.Equals(0))
                    context.AccPaymentHeaders.Add(accPaymentHeader);
                else
                    context.Entry(accPaymentHeader).State = EntityState.Modified;
                context.SaveChanges();

                context.Set<AccPaymentDetail>().Delete(context.AccPaymentDetails.Where(p => p.ReferenceDocumentID.Equals(lgsPurchaseHeader.LgsPurchaseHeaderID) && p.LocationID.Equals(lgsPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(lgsPurchaseHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

                if (paymentTempList != null)
                {
                    var paymentDetailsSaveQuery = (from pt in paymentTempList
                                                   select new
                                                   {
                                                       AccPaymentDetailID = 0
                                                           ,
                                                       PaymentHeaderID = accPaymentHeader.AccPaymentHeaderID
                                                           ,
                                                       CompanyID = accPaymentHeader.CompanyID
                                                           ,
                                                       LocationID = accPaymentHeader.LocationID
                                                           ,
                                                       CostCentreID = accPaymentHeader.CostCentreID
                                                           ,
                                                       DocumentNo = accPaymentHeader.DocumentNo
                                                           ,
                                                       DocumentID = accPaymentHeader.DocumentID
                                                           ,
                                                       DocumentDate = accPaymentHeader.DocumentDate
                                                           ,
                                                       PaymentDate = accPaymentHeader.PaymentDate
                                                           ,
                                                       ReferenceDocumentID = accPaymentHeader.ReferenceDocumentID
                                                           ,
                                                       ReferenceDocumentDocumentID = accPaymentHeader.ReferenceDocumentDocumentID
                                                           ,
                                                       ReferenceLocationID = accPaymentHeader.ReferenceLocationID
                                                           ,
                                                       SetoffDocumentID = 0
                                                           ,
                                                       SetoffDocumentDocumentID = 0
                                                           ,
                                                       SetoffLocationID = 0
                                                           ,
                                                       ReferenceID = accPaymentHeader.ReferenceID
                                                           ,
                                                       Amount = pt.PayAmount
                                                           ,
                                                       paymentModeID = pt.PaymentMethodID
                                                           ,
                                                       BankID = pt.BankID
                                                           ,
                                                       BankBranchID = 0
                                                           ,
                                                       CardNo = pt.CardCheqNo
                                                           ,
                                                       ChequeDate = pt.ChequeDate
                                                           ,
                                                       DocumentStatus = accPaymentHeader.DocumentStatus
                                                           ,
                                                       IsTStatus = accPaymentHeader.IsUpLoad
                                                           ,
                                                       GroupOfCompanyID = accPaymentHeader.GroupOfCompanyID
                                                           ,
                                                       CreatedUser = accPaymentHeader.CreatedUser
                                                           ,
                                                       CreatedDate = accPaymentHeader.CreatedDate
                                                           ,
                                                       ModifiedUser = accPaymentHeader.ModifiedUser
                                                           ,
                                                       ModifiedDate = accPaymentHeader.ModifiedDate
                                                           ,
                                                       DataTransfer = accPaymentHeader.DataTransfer
                                                   }).ToList();

                    CommonService.BulkInsert(Common.LINQToDataTable(paymentDetailsSaveQuery), "AccPaymentDetail");
                }

                #endregion

                #region Process

                if (poIsMandatory)
                {
                    poMandatory = 1;
                }
                else
                {
                    poMandatory = 2;
                }

                var parameter = new DbParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LgsPurchaseHeaderID", Value=lgsPurchaseHeader.LgsPurchaseHeaderID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LgsPurchaseOrderHeaderID", Value=lgsPurchaseHeader.ReferenceDocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=lgsPurchaseHeader.LocationID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=lgsPurchaseHeader.DocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=lgsPurchaseHeader.DocumentStatus},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@poMandatory", Value=poMandatory}
                };

                if (CommonService.ExecuteStoredProcedure("spLgsGoodReceivedNoteSave", parameter))
                {
                    transaction.Complete();
                    return true;
                }
                else
                {
                    return false;
                }

                #endregion
            }
        }

        public bool SavePurchaseReturn(LgsPurchaseHeader lgsPurchaseHeader, List<LgsPurchaseDetailTemp> lgsPurchaseDetailTemp, List<InvProductSerialNoTemp> lgsProductSerialNoTemp, out string newDocumentNo, string formName = "", bool isTStatus = false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();

                #region Purchase Header Save

                if (lgsPurchaseHeader.DocumentStatus.Equals(1))
                {
                    LgsPurchaseService LgsPurchaseServices = new LgsPurchaseService();
                    LocationService locationService = new LocationService();
                    lgsPurchaseHeader.DocumentNo = LgsPurchaseServices.GetDocumentNo(formName, lgsPurchaseHeader.LocationID, locationService.GetLocationsByID(lgsPurchaseHeader.LocationID).LocationCode, lgsPurchaseHeader.DocumentID, false).Trim();
                }

                newDocumentNo = lgsPurchaseHeader.DocumentNo;
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (lgsPurchaseHeader.LgsPurchaseHeaderID.Equals(0)) { context.LgsPurchaseHeaders.Add(lgsPurchaseHeader); }
                else { context.Entry(lgsPurchaseHeader).State = EntityState.Modified; }

                context.SaveChanges();

                #endregion

                #region Purchase Detail Save

                context.Set<LgsPurchaseDetail>().Delete(context.LgsPurchaseDetails.Where(p => p.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.LgsPurchaseHeaderID) && p.LocationID.Equals(lgsPurchaseHeader.LocationID) && p.DocumentID.Equals(lgsPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var lgsPurchaseDetailSaveQuery = (from pd in lgsPurchaseDetailTemp
                                                  select new
                                                  {
                                                      LgsPurchaseDetailID = 0,
                                                      PurchaseDetailID = 0,
                                                      LgsPurchaseHeaderID = lgsPurchaseHeader.LgsPurchaseHeaderID,
                                                      CompanyID = lgsPurchaseHeader.CompanyID,
                                                      LocationID = lgsPurchaseHeader.LocationID,
                                                      CostCentreID = lgsPurchaseHeader.CostCentreID,
                                                      DocumentID = lgsPurchaseHeader.DocumentID,
                                                      LineNo = pd.LineNo,
                                                      ProductID = pd.ProductID,
                                                      UnitOfMeasureID = pd.UnitOfMeasureID,
                                                      BaseUnitID = pd.BaseUnitID,
                                                      ConvertFactor = pd.ConvertFactor,
                                                      ExpiryDate = pd.ExpiryDate,
                                                      OrderQty = pd.OrderQty,
                                                      Qty = pd.Qty,
                                                      FreeQty = pd.FreeQty,
                                                      CurrentQty = pd.CurrentQty,
                                                      QtyConvertFactor = (pd.Qty * pd.ConvertFactor),
                                                      FreeQtyConvertFactor = (pd.FreeQty * pd.ConvertFactor),
                                                      BalanceQty = pd.Qty,
                                                      CostPrice = pd.CostPrice,
                                                      SellingPrice = pd.SellingPrice,
                                                      AvgCost = pd.AverageCost,
                                                      GrossAmount = pd.GrossAmount,
                                                      DiscountPercentage = pd.DiscountPercentage,
                                                      DiscountAmount = pd.DiscountAmount,
                                                      SubTotalDiscount = pd.SubTotalDiscount,
                                                      TaxAmount1 = pd.TaxAmount1,
                                                      TaxAmount2 = pd.TaxAmount2,
                                                      TaxAmount3 = pd.TaxAmount3,
                                                      TaxAmount4 = pd.TaxAmount4,
                                                      TaxAmount5 = pd.TaxAmount5,
                                                      NetAmount = pd.NetAmount,
                                                      DocumentStatus = lgsPurchaseHeader.DocumentStatus,
                                                      BatchNo = (pd.IsBatch == true ? pd.BatchNo : string.Empty),
                                                      //BatchNo = pd.BatchNo,
                                                      IsBatch = pd.IsBatch,
                                                      GroupOfCompanyID = lgsPurchaseHeader.GroupOfCompanyID,
                                                      CreatedUser = lgsPurchaseHeader.CreatedUser,
                                                      CreatedDate = lgsPurchaseHeader.CreatedDate,
                                                      ModifiedUser = lgsPurchaseHeader.ModifiedUser,
                                                      ModifiedDate = lgsPurchaseHeader.ModifiedDate,
                                                      DataTransfer = lgsPurchaseHeader.DataTransfer
                                                  }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(lgsPurchaseDetailSaveQuery), "LgsPurchaseDetail");

                #endregion

                #region Serial No Detail Save

                

                #endregion

                #region Process

                var parameter = new DbParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LgsPurchaseHeaderID", Value=lgsPurchaseHeader.LgsPurchaseHeaderID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=lgsPurchaseHeader.DocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=lgsPurchaseHeader.LocationID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=lgsPurchaseHeader.DocumentStatus},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentDocumentID", Value=lgsPurchaseHeader.ReferenceDocumentDocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentID", Value=lgsPurchaseHeader.ReferenceDocumentID},
                };

                if (CommonService.ExecuteStoredProcedure("spLgsPurchaseReturnNoteSave", parameter))
                {
                    transaction.Complete();
                    return true;
                }
                else
                {
                    return false;
                }

                #endregion
            }
        }


        #region GetNewCode

        public string GetDocumentNo(string formName,int locationID,string locationCode,int documentId,bool isTemporytNo)
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
                     preFix =preFix+locationCode.Trim();


            if (isTemporytNo)
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo+1});

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


        public List<LgsProductSerialNoTemp> getSerialNoDetailForTOG(LgsTransferNoteHeader transferNoteHeader)
        {
            List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();
            List<LgsProductSerialNoDetail> lgsProductSerialNoDetail = new List<LgsProductSerialNoDetail>();

            lgsProductSerialNoDetail = context.LgsProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(transferNoteHeader.LgsTransferNoteHeaderID) && ps.ReferenceDocumentDocumentID.Equals(transferNoteHeader.DocumentID)).ToList();
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

        public string[] GetAllSavedDocumentNumbers(int documentID,int locationID)
        {
            List<string> GoodReceivedNoteList = context.LgsPurchaseHeaders.Where(c => c.DocumentID == documentID && c.DocumentStatus == 1 && c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
            return GoodReceivedNoteList.ToArray();
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.LgsPurchaseHeaders.Where(q => q.DocumentID==documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersAsReference(int documentID, int locationID)
        {
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.LgsPurchaseHeaders
                       join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                       where ph.LocationID == locationID && ph.DocumentID == documentID && pd.BalanceQty != 0
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

        public List<LgsBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo,int locationID,int documentID)
        {
            List<LgsBarcodeDetailTemp> lgsBarcodeDetailTempList = new List<LgsBarcodeDetailTemp>();
            LgsBarcodeDetailTemp lgsBarcodeDetailTemp = new LgsBarcodeDetailTemp();


            var barcodeDetailTemp = (from pm in context.LgsProductMasters
                                     join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
                                     join pd in context.LgsPurchaseDetails on psm.ProductID equals pd.ProductID
                                     join ph in context.LgsPurchaseHeaders on pd.LgsPurchaseHeaderID equals ph.PurchaseHeaderID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     && pd.LocationID == ph.LocationID
                                     && ph.DocumentNo == psm.BatchNo
                                     && psm.LocationID == locationID
                                     && ph.PurchaseHeaderID == psm.ReferenceDocumentID
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
                                         Qty = psm.BalanceQty,
                                         Stock = psm.BalanceQty,
                                         psm.CostPrice,
                                         psm.SellingPrice,
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

        /// <summary>
        /// Get purchase details for report generator
        /// </summary>
        /// <returns></returns>
        public DataTable GetPurchaseDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
           
            var query = context.LgsPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                    int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        case "LgsSupplierID":
                        case "SupplierID":
                            query = (from qr in query
                                     join jt in context.LgsSuppliers.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.SupplierID equals jt.LgsSupplierID
                                     select qr
                                );
                            break;

                        default:
                            query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }

                }
            }

            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder");

            var queryResult = query.AsEnumerable();
            
            return (from h in queryResult 
                               join s in context.LgsSuppliers on h.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               join po in context.LgsPurchaseOrderHeaders on h.ReferenceDocumentID equals po.LgsPurchaseOrderHeaderID
                               where h.ReferenceDocumentDocumentID == autoGenerateInfoPo.DocumentID
                               select
                                   new
                                   {
                                       FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode : "",
                                       FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.CreatedUser : "",
                                       FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                                       FieldString4 = reportDataStructList[3].IsSelectionField == true ? s.SupplierCode + " " + s.SupplierName : "",
                                       FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.DocumentNo : "",
                                       FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.SupplierInvoiceNo : "",
                                       FieldString7 = reportDataStructList[6].IsSelectionField == true ? h.ReferenceNo : "",
                                       FieldString8 = reportDataStructList[7].IsSelectionField == true ? po.DocumentNo : "", // Purchase Order No
                                       FieldString9 = reportDataStructList[8].IsSelectionField == true ? s.TaxNo1 != "" ? "B" : s.TaxNo2 != "" ? "B" : s.TaxNo3 != "" ? "B" : "P" : "",
                                       FieldDecimal1 = reportDataStructList[9].IsSelectionField == true ? (from t in context.LgsPurchaseDetails
                                                                                                           where
                                                                                                             t.LgsPurchaseHeaderID == h.LgsPurchaseHeaderID
                                                                                                           select new
                                                                                                           {
                                                                                                               Column1 = ((System.Decimal?)t.Qty ?? (System.Decimal?)0)
                                                                                                           }).Sum(p => p.Column1) : 0, // Qty

                                       FieldDecimal2 = reportDataStructList[10].IsSelectionField == true ? h.GrossAmount : 0,
                                       FieldDecimal3 = reportDataStructList[11].IsSelectionField == true ? h.DiscountAmount : 0,
                                       FieldDecimal4 = reportDataStructList[12].IsSelectionField == true ? h.TaxAmount1 : 0, // NBT(2%)
                                       FieldDecimal5 = reportDataStructList[13].IsSelectionField == true ? h.TaxAmount2 : 0, // NBT(2.04%)
                                       FieldDecimal6 = reportDataStructList[14].IsSelectionField == true ? h.TaxAmount3 : 0, // VAT
                                       FieldDecimal7 = reportDataStructList[15].IsSelectionField == true ? h.NetAmount : 0
                                   }).ToArray().ToDataTable();

        }

        public DataTable GetPurchaseRegistryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            var query = context.LgsPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);

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
            }



            var queryResult = (from ph in query
                               join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                               join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                               join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                               //group new { ph, pd, pm, um, s } by new
                               group new { ph, pd, pm, s, l } by new
                               {
                                   ph.DocumentDate,
                                   ph.DocumentNo,
                                   pm.ProductCode,
                                   pm.BarCode,
                                   pm.ProductName,
                                   s.SupplierCode,
                                   s.SupplierName,
                                   pd.CostPrice,
                                   l.LocationCode
                               }
                                   into g
                                   select new
                                   {
                                       FieldString1 = DbFunctions.TruncateTime(g.Key.DocumentDate),
                                       FieldString2 = g.Key.DocumentNo,
                                       FieldString3 = g.Key.ProductCode,
                                       FieldString4 = g.Key.BarCode,
                                       FieldString5 = g.Key.ProductName,
                                       //FieldString5 = g.Key.UnitOfMeasureName,
                                       FieldString6 = g.Key.SupplierCode,
                                       FieldString7 = g.Key.SupplierName,
                                       FieldDecimal1 = g.Sum(x => x.pd.Qty),
                                       FieldDecimal2 = g.Key.CostPrice,
                                       //FieldDecimal3 = g.Sum(x => x.pd.DiscountPercentage),
                                       FieldDecimal3 = g.Sum(x => x.pd.DiscountAmount),
                                       FieldDecimal4 = g.Sum(x => x.pd.NetAmount),
                                       FieldDecimal5 = g.Sum(x => x.pd.GrossAmount)
                                   }).ToArray();

            return queryResult.ToDataTable();

        }

        public DataTable GetPurchaseReturnRegistryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseReturnNote");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            var query = context.LgsPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);

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
            }

            var queryResult = (from ph in query
                               join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                               join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                               //join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                               join d in context.LgsDepartments on pm.DepartmentID equals d.LgsDepartmentID
                               join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                               //group new { ph, pd, pm, um, s } by new
                               group new { ph, pd, pm, d, s, l } by new
                               {
                                   ph.DocumentDate,
                                   ph.DocumentNo,
                                   pm.ProductCode,
                                   pm.BarCode,
                                   pm.ProductName,
                                   d.DepartmentCode,
                                   d.DepartmentName,
                                   s.SupplierCode,
                                   s.SupplierName,
                                   pd.CostPrice,
                                   l.LocationCode,
                                   l.LocationName
                               }
                                   into g
                                   select new
                                   {
                                       FieldString1 = DbFunctions.TruncateTime(g.Key.DocumentDate),
                                       FieldString2 = g.Key.DocumentNo,
                                       FieldString3 = g.Key.ProductCode,
                                       FieldString4 = g.Key.BarCode,
                                       FieldString5 = g.Key.ProductName,
                                       //FieldString5 = g.Key.UnitOfMeasureName,
                                       FieldString6 = g.Key.SupplierCode,
                                       FieldString7 = g.Key.SupplierName,
                                       FieldDecimal1 = g.Sum(x => x.pd.Qty),
                                       FieldDecimal2 = g.Key.CostPrice,
                                       //FieldDecimal3 = g.Sum(x => x.pd.DiscountPercentage),
                                       FieldDecimal3 = g.Sum(x => x.pd.DiscountAmount),
                                       FieldDecimal4 = g.Sum(x => x.pd.NetAmount),
                                       FieldDecimal5 = g.Sum(x => x.pd.GrossAmount),
                                       FieldDecimal6 = 90
                                   }).ToArray();

            return queryResult.ToDataTable();

        }

        /// <summary>
        /// Get purchase details for report generator
        /// </summary>
        /// <returns></returns>
        public DataTable GetPurchaseReturnDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {         
            var query = context.LgsPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                    int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        case "SupplierID":
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
                                         ) on qr.SupplierID equals jt.LgsSupplierID
                                     select qr
                                );
                            break;
                        default:
                            query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }

                }
            }

            AutoGenerateInfo autoGenerateInfoGRn = new AutoGenerateInfo();
            autoGenerateInfoGRn = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");

            DataTable dtt = query.AsEnumerable().ToArray().ToDataTable();
            var queryResult = (from h in query
                               join s in context.LgsSuppliers on h.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               join h2 in context.LgsPurchaseHeaders on new { h.ReferenceDocumentID, DocumentID = autoGenerateInfoGRn.DocumentID }
                               equals new { ReferenceDocumentID = h2.LgsPurchaseHeaderID, h2.DocumentID } into h2_join
                               from h2 in h2_join.DefaultIfEmpty()
                               where h.DocumentID == autoGenerateInfo.DocumentID
                               select
                                    new
                                    {
                                        FieldString1 = l.LocationCode,
                                        FieldString2 = h.CreatedUser,
                                        FieldString3 = h.DocumentDate,
                                        FieldString4 = s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName,
                                        FieldString5 = h.DocumentNo,
                                        FieldString6 = h2.SupplierInvoiceNo,
                                        FieldString7 = h.ReferenceNo,
                                        FieldString8 = h2.DocumentNo, // GRN No
                                        FieldString9 = s.TaxNo1 != "" ? "B" : s.TaxNo2 != "" ? "B" : s.TaxNo3 != "" ? "B" : "P",
                                        FieldDecimal1 = (from t in context.LgsPurchaseDetails
                                                         where
                                                             t.LgsPurchaseHeaderID == h.LgsPurchaseHeaderID
                                                         select new
                                                         {
                                                             Column1 = ((System.Decimal?)t.Qty ?? (System.Decimal?)0)
                                                         }).Sum(p => p.Column1), // Qty

                                        FieldDecimal2 = h.GrossAmount,
                                        FieldDecimal3 = h.DiscountAmount,
                                        FieldDecimal4 = h.TaxAmount1, // NBT(2%)
                                        FieldDecimal5 = h.TaxAmount2, // NBT(2.04%)
                                        FieldDecimal6 = h.TaxAmount3, // VAT
                                        FieldDecimal7 = h.NetAmount

                                    }).ToArray();

            return (from h in queryResult
                    select new
                    {
                        FieldString1 = reportDataStructList[0].IsSelectionField == true ? h.FieldString1 : "",
                        FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.FieldString2 : "",
                        FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.FieldString3.ToShortDateString() : "",
                        FieldString4 = reportDataStructList[3].IsSelectionField == true ? h.FieldString4 : "",
                        FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.FieldString5 : "",
                        FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.FieldString6 : "",
                        FieldString7 = reportDataStructList[6].IsSelectionField == true ? h.FieldString7 : "",
                        FieldString8 = reportDataStructList[7].IsSelectionField == true ? h.FieldString8 : "", // GRN No
                        FieldString9 = reportDataStructList[8].IsSelectionField == true ? h.FieldString9 : "",
                        FieldDecimal1 = reportDataStructList[9].IsSelectionField == true ? h.FieldDecimal1 : 0, // Qty
                        FieldDecimal2 = reportDataStructList[10].IsSelectionField == true ? h.FieldDecimal2 : 0,
                        FieldDecimal3 = reportDataStructList[11].IsSelectionField == true ? h.FieldDecimal3 : 0,
                        FieldDecimal4 = reportDataStructList[12].IsSelectionField == true ? h.FieldDecimal4 : 0, // NBT(2%)
                        FieldDecimal5 = reportDataStructList[13].IsSelectionField == true ? h.FieldDecimal5 : 0, // NBT(2.04%)
                        FieldDecimal6 = reportDataStructList[14].IsSelectionField == true ? h.FieldDecimal6 : 0, // VAT
                        FieldDecimal7 = reportDataStructList[15].IsSelectionField == true ? h.FieldDecimal7 : 0
                    }).AsEnumerable().ToDataTable();

        }

        public DataSet GetPurchaseTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseOrder");

            var query = (
                           from ph in context.LgsPurchaseHeaders
                           join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                           join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                           join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                           join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           join po in context.LgsPurchaseOrderHeaders on ph.ReferenceDocumentID equals po.LgsPurchaseOrderHeaderID
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                           && po.DocumentID.Equals(autoGenerateInfoPo.DocumentID)
                           select
                               new
                               {
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = ph.Remark,
                                   FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                                   FieldString5 = po.DocumentNo, // PO
                                   FieldString6 = ph.ReferenceNo,
                                   FieldString7 = s.TaxNo3,
                                   FieldString8 = s.BillingAddress1,
                                   FieldString9 = ph.SupplierInvoiceNo,
                                   FieldString10 = py.PaymentMethodName,
                                   FieldString11 = "",
                                   FieldString12 = l.LocationCode + "  " + l.LocationName,
                                   FieldString13 = pm.ProductCode,
                                   FieldString14 = pm.ProductName,
                                   FieldString15 = um.UnitOfMeasureName,
                                   FieldString16 = pd.OrderQty,
                                   FieldString17 = pd.Qty,
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
                                   FieldString28 = ph.OtherChargers,
                                   FieldString29 = ph.NetAmount,
                                   FieldString30 = ph.CreatedUser            
                               }).ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(query);

            var purchaseOrderHeaderID = context.LgsPurchaseHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus) 
                    && h.DocumentID.Equals(documentid)).FirstOrDefault().ReferenceDocumentID;

            if (context.LgsPurchaseOrderDetails.Any(d => d.BalanceQty > 0 && d.LgsPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            { 
                var queryBal = (
                                        from ph in context.LgsPurchaseOrderHeaders
                                        join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                        join s in context.LgsSuppliers on ph.LgsSupplierID equals s.LgsSupplierID
                                        join l in context.Locations on ph.LocationID equals l.LocationID
                                        where ph.DocumentStatus.Equals(documentStatus) && ph.LgsPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID)
                                        select
                                            new
                                            {
                                                FieldString1 = ph.DocumentNo,
                                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                                FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                                FieldString4 = ph.Remark,
                                                FieldString5 = "", // P Req. No
                                                FieldString6 = "",//ph.ValidityPeriod,
                                                FieldString7 = "",
                                                FieldString8 = "",
                                                FieldString9 = ph.ReferenceNo,
                                                FieldString10 = "",//py.PaymentMethodName,
                                                FieldString11 = "",//ph.ExpectedDate,
                                                FieldString12 = l.LocationCode + "  " + l.LocationName,
                                                FieldString13 = pm.ProductCode,
                                                FieldString14 = pm.ProductName,
                                                FieldString15 = um.UnitOfMeasureName,
                                                FieldString16 = pd.BalanceQty, //pd.PackSize,
                                                FieldString17 = pd.OrderQty,
                                                FieldString18 = pd.FreeQty,
                                                FieldString19 = pd.CostPrice,
                                                FieldString20 = pd.DiscountPercentage,
                                                FieldString21 = pd.DiscountAmount,
                                                FieldString22 = pd.NetAmount,
                                                FieldString23 = ph.GrossAmount,
                                                FieldString24 = ph.DiscountPercentage,
                                                FieldString25 = ph.DiscountAmount,
                                                FieldString26 = ph.TaxAmount,
                                                FieldString27 = "",//ph.OtherCharges,
                                                FieldString28 = ph.NetAmount,
                                            }).ToDataTable();

                dsTransactionData.Tables.Add(queryBal);
            }


            //return query.AsEnumerable().ToDataTable();

            return dsTransactionData;
        }

        public List<LgsPurchaseDetailTemp> GetSavedGrnDetailToTOG(LgsPurchaseHeader lgsPurchaseHeader, int documentID)
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp;
            List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList = new List<LgsPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                      join pd in context.LgsPurchaseDetails on pm.LgsProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.LgsPurchaseHeaders on pd.LgsPurchaseHeaderID equals ph.LgsPurchaseHeaderID
                                      where pd.LocationID.Equals(lgsPurchaseHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(lgsPurchaseHeader.DocumentID) && pd.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.PurchaseHeaderID)
                                      select new
                                      {
                                          pd.AvgCost,
                                          pd.ConvertFactor,
                                          pd.CostPrice,
                                          pd.CurrentQty,
                                          pd.DiscountAmount,
                                          pd.DiscountPercentage,
                                          ExpiryDate = pd.ExpiryDate,
                                          pd.FreeQty,
                                          pd.GrossAmount,
                                          pd.LineNo,
                                          pd.LocationID,
                                          pd.NetAmount,
                                          pd.OrderQty,
                                          pm.ProductCode,
                                          pd.ProductID,
                                          pm.ProductName,
                                          pd.BalanceQty,
                                          pd.SellingPrice,
                                          pd.SubTotalDiscount,
                                          pd.TaxAmount1,
                                          pd.TaxAmount2,
                                          pd.TaxAmount3,
                                          pd.TaxAmount4,
                                          pd.TaxAmount5,
                                          pd.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          ph.BatchNo
                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {
                lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

                lgsPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                lgsPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                lgsPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                lgsPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                lgsPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                lgsPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                lgsPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                lgsPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                lgsPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                lgsPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                lgsPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                lgsPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                lgsPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                lgsPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                lgsPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                lgsPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                lgsPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                lgsPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                lgsPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                lgsPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                lgsPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                lgsPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                lgsPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;

                lgsPurchaseDetailTempList.Add(lgsPurchaseDetailTemp);
            }
            return lgsPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetPurchaseReturnTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            AutoGenerateInfo autoGenerateInfoGRn = new AutoGenerateInfo();
            autoGenerateInfoGRn = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");
            
            var query = (
                           from ph in context.LgsPurchaseHeaders
                           join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                           join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                           join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                           join re in context.LgsReturnTypes on ph.ReturnTypeID equals re.LgsReturnTypeID
                           //join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           join h2 in context.LgsPurchaseHeaders on new { ph.ReferenceDocumentID, DocumentID = autoGenerateInfoGRn.DocumentID }
                               equals new { ReferenceDocumentID = h2.LgsPurchaseHeaderID, h2.DocumentID } into h2_join
                           from h2 in h2_join.DefaultIfEmpty()
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                           select
                               new
                               {
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = ph.Remark,
                                   FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                                   FieldString5 = h2.DocumentNo,//GRN No
                                   FieldString6 = ph.ReferenceNo,//ph.ValidityPeriod,
                                   FieldString7 = re.ReturnType,
                                   FieldString8 = s.BillingAddress1,
                                   FieldString9 = h2.SupplierInvoiceNo, // Supplier Invoice
                                   FieldString10 = "",
                                   FieldString11 = "",
                                   FieldString12 = l.LocationCode + "  " + l.LocationName,
                                   FieldString13 = pm.ProductCode,
                                   FieldString14 = pm.ProductName,
                                   FieldString15 = um.UnitOfMeasureName,
                                   FieldString16 = "", //pd.PackSize,
                                   FieldString17 = pd.Qty, 
                                   FieldString18 = pd.FreeQty,
                                   FieldString19 = pd.CostPrice,
                                   FieldString20 = pd.DiscountPercentage,
                                   FieldString21 = pd.DiscountAmount,
                                   FieldString22 = pd.NetAmount,
                                   FieldString23 = ph.GrossAmount,
                                   FieldString24 = ph.DiscountPercentage,
                                   FieldString25 = ph.DiscountAmount,
                                   FieldString26 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2, // NBT,
                                   FieldString27 = ph.TaxAmount3, // VAT,
                                   FieldString28 = ph.OtherChargers,
                                   FieldString29 = ph.NetAmount,
                                   FieldString30 = ph.CreatedUser
                               }).ToArray();

            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {            
            // Create query 
            IQueryable qryResult = context.LgsPurchaseHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, autoGenerateInfo.DocumentID);

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSupplierID":
                    qryResult = null;
                    qryResult = context.LgsPurchaseHeaders.Where("DocumentStatus == @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);
                    qryResult = qryResult.Join(context.LgsSuppliers, reportDataStruct.DbColumnName.Trim(),
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                  "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                         .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;

                case "SupplierID":
                    qryResult = null;
                    qryResult = context.LgsPurchaseHeaders.Where("DocumentStatus == @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);
                    qryResult = qryResult.Join(context.LgsSuppliers, reportDataStruct.DbColumnName.Trim(),
                                               "LgsSupplierID",
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
            else if (reportDataStruct.ValueDataType.Equals(typeof(int)))
            {
                if (reportDataStruct.DbColumnName.Trim() == "DocumentID")
                {
                    AutoGenerateInfo autoGenerateInfoPch = new AutoGenerateInfo();
                    autoGenerateInfoPch = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");

                    AutoGenerateInfo autoGenerateInfoPchRet = new AutoGenerateInfo();
                    autoGenerateInfoPchRet = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseReturnNote");

                    foreach (var item in qryResult)
                    {
                        if (item.Equals((int)autoGenerateInfoPch.DocumentID))
                        { selectionDataList.Add("Purchase"); }
                        if (item.Equals((int)autoGenerateInfoPchRet.DocumentID))
                        { selectionDataList.Add("Supplier Return"); }
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

        public ArrayList GetSelectionPurchaseData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsPurchaseDetails.Where("DocumentStatus == @0", 1);


            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "ProductID":
                    qryResult = qryResult.Join(context.LgsProductMasters, reportDataStruct.DbColumnName.Trim(), "LgsProductMasterID", "new(inner.ProductCode)").
                                OrderBy("ProductCode").Select("ProductCode");
                    break;

                case "LgsPurchaseHeaderID":
                    qryResult = qryResult.Join(context.LgsPurchaseHeaders, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.DocumentNo)").
                                OrderBy("DocumentNo").Select("DocumentNo");
                    break;

                case "SupplierID":
                    qryResult = null;
                    qryResult = context.LgsPurchaseHeaders.Where("DocumentStatus == @0", 1);
                    qryResult = qryResult.Join(context.LgsSuppliers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierCode)").
                                   OrderBy("SupplierCode").Select("SupplierCode");
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

        public DataTable GetProductPurchaseDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {

            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsPurchaseHeaders.Where("DocumentStatus=1");

            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            //(this IQueryable outer, IEnumerable inner, string outerSelector, string innerSelector, string resultsSelector, params object[] values)
            //var newq = query.Join("c", querrSupplier  ,"g","c.key.grp","g.key","new(g.key as grp,c.key.cat,new(c.count()*100/g.count() as percent) as agg)");

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);
            //string st = querrySelect.ToString();
            //var v = query.AsQueryable().Select("new(" + querrySelect.ToString() + ")");



            var queryResult = (from ph in query
                               join pd in context.LgsPurchaseDetails on ph.LgsPurchaseHeaderID equals pd.LgsPurchaseHeaderID
                               join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                               join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                               join d in context.LgsDepartments on pm.DepartmentID equals d.LgsDepartmentID
                               join c in context.LgsCategories on pm.CategoryID equals c.LgsCategoryID
                               join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on ph.LocationID equals l.LocationID
                               where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                               group new { ph, pd, pm, um, s } by new
                               {
                                   ph.DocumentNo,
                                   pm.ProductCode,
                                   pm.BarCode,
                                   pm.ProductName,
                                   um.UnitOfMeasureName,
                                   s.SupplierCode,
                                   s.SupplierName,
                                   pd.CostPrice
                               }
                                   into g
                                   select new
                                   {
                                       FieldString1 = g.Key.DocumentNo,
                                       FieldString2 = g.Key.ProductCode,
                                       FieldString3 = g.Key.BarCode,
                                       FieldString4 = g.Key.ProductName,
                                       FieldString5 = g.Key.UnitOfMeasureName,
                                       FieldString6 = g.Key.SupplierCode,
                                       FieldString7 = g.Key.SupplierName,
                                       FieldDecimal1 = g.Sum(x => x.pd.Qty),
                                       FieldDecimal2 = g.Key.CostPrice,
                                       //FieldDecimal3 = g.Sum(x => x.pd.DiscountPercentage),
                                       FieldDecimal3 = g.Sum(x => x.pd.DiscountAmount),
                                       FieldDecimal4 = g.Sum(x => x.pd.NetAmount),

                                   }).ToArray();

            return queryResult.ToDataTable();
        }

        public DataTable GetSupplierPurchaseDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {


            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsPurchase.Where("DocumentStatus=1");

            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }


            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
            var purchaseDocumentID = autoGenerateInfo.DocumentID;

            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseReturnNote");
            var purchaseReturnDocumentID = autoGenerateInfo.DocumentID;

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from ph in query
                               join pd in context.LgsProductExtendedPropertyValues on ph.ProductID equals pd.ProductID
                               where ph.DocumentStatus == 1 && ph.IsDelete == false
                               group new { ph, pd } by new
                               {
                                   ph.DocumentDate,
                                   ph.ProductCode,
                                   ph.BarCode,
                                   ph.ProductName,
                                   ph.UnitOfMeasureName,
                                   ph.CostPrice,
                                   ph.DocumentID,
                                   ph.DepartmentCode,
                                   ph.DepartmentName,
                                   ph.CategoryCode,
                                   ph.CategoryName,
                                   ph.SubCategoryCode,
                                   ph.SubCategoryName,
                                   ph.SubCategory2Code,
                                   ph.SubCategory2Name,
                                   ph.LocationCode,
                                   ph.LocationName,
                                   ph.DocumentNo,
                                   ph.SupplierCode,
                                   ph.SupplierName
                               }
                                   into g
                                   select new
                                   {
                                       //FieldString1 = g.Key.DocumentNo,
                                       FieldString1 = g.Key.DocumentDate,
                                       FieldString2 = g.Key.ProductCode,
                                       FieldString3 = g.Key.BarCode,
                                       FieldString4 = g.Key.ProductName,
                                       FieldString5 = g.Key.UnitOfMeasureName,
                                       FieldString6= g.Key.DepartmentCode,
                                       FieldString7 = g.Key.DepartmentName,
                                       FieldString8 = g.Key.CategoryCode,
                                       FieldString9 = g.Key.CategoryName,
                                       FieldString10 = g.Key.SubCategoryCode,
                                       FieldString11 = g.Key.SubCategoryName,
                                       FieldString12 = g.Key.LocationCode,
                                       FieldString13 = g.Key.LocationName,
                                       FieldString14 = g.Key.SupplierCode,
                                       FieldString15 = g.Key.SupplierName,
                                       //FieldDecimal1 = g.Sum(x => x.ph.Qty),
                                       //FieldDecimal2 = g.Sum(x => x.ph.Qty),
                                       FieldDecimal1 = (g.Key.DocumentID == purchaseDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                       FieldDecimal2 = (g.Key.DocumentID == purchaseReturnDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                       FieldDecimal3 = g.Key.CostPrice,
                                       FieldDecimal4 = g.Sum(x => x.ph.DiscountAmount),
                                       FieldDecimal5 = g.Sum(x => x.ph.NetAmount),

                                   }).ToArray();

            return queryResult.ToDataTable();
        }

        public ArrayList GetSelectionPurchaseDataSummary(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsPurchase.Where("DocumentStatus == @0", 1);
            qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
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

        public DataTable GetPurchaseDetailsSummaryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsPurchase.Where("DocumentStatus=1");

            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }


            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();

            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticGoodsReceivedNote");
            var purchaseDocumentID = autoGenerateInfo.DocumentID;

            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticPurchaseReturnNote");
            var purchaseReturnDocumentID = autoGenerateInfo.DocumentID;

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from ph in query
                               join pd in context.LgsProductExtendedPropertyValues on ph.ProductID equals pd.ProductID
                               where ph.DocumentStatus == 1 && ph.IsDelete == false
                               group new { ph, pd } by new
                               {
                                   ph.DocumentDate,
                                   ph.ProductCode,
                                   ph.BarCode,
                                   ph.ProductName,
                                   ph.UnitOfMeasureName,
                                   ph.CostPrice,
                                   ph.DocumentID,
                                   ph.DepartmentCode,
                                   ph.DepartmentName,
                                   ph.CategoryCode,
                                   ph.CategoryName,
                                   ph.SubCategoryCode,
                                   ph.SubCategoryName,
                                   ph.SubCategory2Code,
                                   ph.SubCategory2Name,
                                   ph.LocationCode,
                                   ph.LocationName,
                                   ph.DocumentNo,
                                   ph.SupplierCode,
                                   ph.SupplierName,
                               }
                                   into g
                                   select new
                                   {
                                       FieldString1 = g.Key.DocumentNo,
                                       FieldString2 = g.Key.DocumentDate,
                                       FieldString3= g.Key.ProductCode,
                                       FieldString4 = g.Key.BarCode,
                                       FieldString5 = g.Key.ProductName,
                                       FieldString6 = g.Key.UnitOfMeasureName,
                                       FieldString7 = g.Key.DepartmentCode,
                                       FieldString8 = g.Key.DepartmentName,
                                       FieldString9 = g.Key.CategoryCode,
                                       FieldString10 = g.Key.CategoryName,
                                       FieldString11 = g.Key.SubCategoryCode,
                                       FieldString12 = g.Key.SubCategoryName,
                                       FieldString13 = g.Key.LocationCode,
                                       FieldString14 = g.Key.LocationName,
                                       FieldString15 = g.Key.SupplierCode,
                                       FieldString16 = g.Key.SupplierName,
                                       //FieldDecimal1 = g.Sum(x => x.ph.Qty),
                                       //FieldDecimal2 = g.Sum(x => x.ph.Qty),
                                       FieldDecimal1 = (g.Key.DocumentID == purchaseDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                       FieldDecimal2 = (g.Key.DocumentID == purchaseReturnDocumentID ? g.Sum(x => x.ph.Qty) : 0),
                                       FieldDecimal3 = g.Key.CostPrice,
                                       FieldDecimal4 = g.Sum(x => x.ph.DiscountAmount),
                                       FieldDecimal5 = g.Sum(x => x.ph.NetAmount),

                                   }).ToArray();

            return queryResult.ToDataTable();
        }

        public LgsPurchaseDetailTemp GetPurchaseDetailTempForRecallPO(List<LgsPurchaseDetailTemp> LgsPurchaseDetailTemp, LgsProductMaster lgsProductMaster, long unitOfMeasureID) 
        {
            LgsPurchaseDetailTemp lgsPurchaseDetailTemp = new LgsPurchaseDetailTemp();

            lgsPurchaseDetailTemp = LgsPurchaseDetailTemp.Where(pd => pd.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && pd.UnitOfMeasureID.Equals(unitOfMeasureID)).FirstOrDefault();

           
            
            return lgsPurchaseDetailTemp;
        }

        public bool ValidateExistingProduct(bool isCode, string strProduct, string purchaseOrderNo)
        { 
            bool result = false;
            LgsPurchaseOrderHeader lgsPurchaseorderHeader = new LgsPurchaseOrderHeader();
            LgsPurchaseOrderService lgsPurchaseOrderService = new LgsPurchaseOrderService();
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

            lgsPurchaseorderHeader = lgsPurchaseOrderService.GetDocumentDetailsByDocumentNumber(purchaseOrderNo);

            if (lgsPurchaseorderHeader != null)
            {
                if (isCode)
                {
                    lgsProductMaster = lgsProductMasterService.GetProductsByCode(strProduct);
                }
                else
                {
                    lgsProductMaster = lgsProductMasterService.GetProductsByName(strProduct);
                }

                if (lgsProductMaster != null)
                {
                    var purchaseDetailTemp = (from pm in context.LgsProductMasters
                                              join pod in context.LgsPurchaseOrderDetails on pm.LgsProductMasterID equals pod.ProductID
                                              join poh in context.LgsPurchaseOrderHeaders on pod.LgsPurchaseOrderHeaderID equals poh.LgsPurchaseOrderHeaderID
                                              where pod.ProductID.Equals(lgsProductMaster.LgsProductMasterID) && poh.LgsPurchaseOrderHeaderID.Equals(lgsPurchaseorderHeader.LgsPurchaseOrderHeaderID)
                                              select new
                                              {
                                                  pm.ProductCode
                                              });

                    if (purchaseDetailTemp == null || purchaseDetailTemp.Count() == 0)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            else { }

            return result;
        }


        public bool CheckExpiryDates(List<LgsPurchaseDetailTemp> lgsPurchaseDetailTempList) 
        {
            bool rtnValue = true;
            LgsProductMaster lgsProductMaster = new LgsProductMaster();
            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();

            List<LgsPurchaseDetailTemp> lgsProductSerialNoTemp = new List<LgsPurchaseDetailTemp>();
            lgsProductSerialNoTemp = lgsPurchaseDetailTempList.Where(t => t.ExpiryDate == null).ToList();

            foreach (LgsPurchaseDetailTemp temp in lgsProductSerialNoTemp)
            {
                long productID = temp.ProductID;
                lgsProductMaster = lgsProductMasterService.GetProductDetailsByID(productID);

                if (lgsProductMaster != null)
                {
                    if (lgsProductMaster.IsExpiry)
                    {
                        rtnValue = false;
                        break;
                    }
                    else
                    {
                        rtnValue = true;
                    }
                } 
            }

            return rtnValue;
        }

        public string[] GetAllProductCodesBySupplier(long supplierID)
        {

            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false) && b.LgsSupplierID.Equals(supplierID))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllProductNamesBySupplier(long supplierID)
        {

            
            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false) && b.LgsSupplierID.Equals(supplierID))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();

        }

        public string[] GetAllProductCodes()
        {

            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllProductNames()
        {


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
