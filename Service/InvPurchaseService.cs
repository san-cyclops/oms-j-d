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
    public class InvPurchaseService
    {
        //By Pravin
        ERPDbContext context = new ERPDbContext();
        string sql;
        public static bool poIsMandatory;
        int poMandatory; // 1-yes  2-no 

          
        ///// <summary>
        /////  PurchaseTypes - DocumentId in InvPurchase table (Purchase or Supplier Terurns)
        ///// </summary>
        //public enum PurchaseTypes
        //{
        //    Purchase = 1,
        //    SupplierReturns = 2,
        //}



        public InvPurchaseDetailTemp GetPurchaseDetailTemp(List<InvPurchaseDetailTemp> InvPurchaseDetailTemp, InvProductMaster invProductMaster,int locationID, DateTime expiryDate,long unitOfMeasureID)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp=new InvPurchaseDetailTemp();
            decimal defaultConvertFactor=1;
            var purchaseDetailTemp=(from pm in context.InvProductMasters
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
                                              pm.AverageCost,
                                              ConvertFactor=defaultConvertFactor,
                                              pm.IsBatch,
                                          }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                purchaseDetailTemp=(dynamic)null;

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
                                              pm.AverageCost,
                                              puc.ConvertFactor,
                                              pm.IsBatch
                                          }).ToArray();
            }
            
            foreach (var tempProduct in purchaseDetailTemp)
            {
                if(!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID==unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();
                
                if (invPurchaseDetailTemp==null)
                {
                    invPurchaseDetailTemp=new InvPurchaseDetailTemp();
                    invPurchaseDetailTemp.ProductID=tempProduct.InvProductMasterID;
                    invPurchaseDetailTemp.ProductCode=tempProduct.ProductCode;
                    invPurchaseDetailTemp.ProductName=tempProduct.ProductName;
                    invPurchaseDetailTemp.UnitOfMeasureID=tempProduct.UnitOfMeasureID;
                    invPurchaseDetailTemp.CostPrice=tempProduct.CostPrice;
                    invPurchaseDetailTemp.SellingPrice=tempProduct.SellingPrice;
                    invPurchaseDetailTemp.AverageCost=tempProduct.AverageCost;
                    invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }
            return invPurchaseDetailTemp;
        }

        public InvPurchaseDetailTemp GetPurchaseDetailTempPRNWithBatch(List<InvPurchaseDetailTemp> InvPurchaseDetailTemp, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID,string batch)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp();
            decimal defaultConvertFactor = 1;
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
                                          pm.AverageCost,
                                          ConvertFactor = defaultConvertFactor,
                                          pm.IsBatch,
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
                                          pm.AverageCost,
                                          puc.ConvertFactor,
                                          pm.IsBatch
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    //invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.BatchNo.Equals(batch)).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    //invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate && p.BatchNo.Equals(batch)).FirstOrDefault();
                else
                    //invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();
                    invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.BatchNo.Equals(batch)).FirstOrDefault();

                if (invPurchaseDetailTemp == null)
                {
                    invPurchaseDetailTemp = new InvPurchaseDetailTemp();
                    invPurchaseDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                    invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                    invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                    invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invPurchaseDetailTemp.AverageCost = tempProduct.AverageCost;
                    invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }
            return invPurchaseDetailTemp;
        }

        public InvPurchaseDetailTemp GetPurchaseDetailTempForPRN(InvPurchaseHeader invPurchaseHeader, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp(); 
            decimal defaultConvertFactor = 1;
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
                                          pm.AverageCost,
                                          ConvertFactor = defaultConvertFactor
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
                                          pm.AverageCost,
                                          puc.ConvertFactor
                                      }).ToArray();
            }

            foreach (var tempProduct in purchaseDetailTemp)
            {
                invPurchaseDetailTemp.ProductID = tempProduct.InvProductMasterID;
                invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseDetailTemp.ExpiryDate = expiryDate;
                invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseDetailTemp.AverageCost = tempProduct.AverageCost;
                invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseDetailTemp.BatchNo = invPurchaseHeader.BatchNo;
            }
            return invPurchaseDetailTemp;
        }


        public List<InvPurchaseDetailTemp> getUpdatePurchaseDetailTemp(List<InvPurchaseDetailTemp> invPurchaseTempDetail, InvPurchaseDetailTemp invPurchaseDetail, InvProductMaster invProductMaster, bool isRecallPo)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp();

            if (invProductMaster.IsExpiry)
            {
                invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && (p.ExpiryDate == invPurchaseDetail.ExpiryDate || p.ExpiryDate == null)).FirstOrDefault();

                if (isRecallPo)
                {
                    invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
                }
            }
            else
            {
                invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
            }

            if (invPurchaseDetailTemp == null || invPurchaseDetailTemp.LineNo.Equals(0))
            {
                if (invPurchaseTempDetail.Count.Equals(0))
                    invPurchaseDetail.LineNo = 1;
                else
                    invPurchaseDetail.LineNo = invPurchaseTempDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invPurchaseTempDetail.Remove(invPurchaseDetailTemp);
                invPurchaseDetail.LineNo = invPurchaseDetailTemp.LineNo;
            }

            invPurchaseTempDetail.Add(invPurchaseDetail);

            return invPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseDetailTemp> getUpdatePurchaseDetailTempPRN(List<InvPurchaseDetailTemp> invPurchaseTempDetail, InvPurchaseDetailTemp invPurchaseDetail, InvProductMaster invProductMaster, bool isRecallPo)
        { 
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp();

            if (invProductMaster.IsExpiry)
            {
                //invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && (p.ExpiryDate == invPurchaseDetail.ExpiryDate || p.ExpiryDate == null)).FirstOrDefault();
                invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && (p.ExpiryDate == invPurchaseDetail.ExpiryDate || p.ExpiryDate == null) && p.BatchNo.Equals(invPurchaseDetail.BatchNo)).FirstOrDefault();

                if (isRecallPo)
                {
                    //invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
                    invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && p.BatchNo.Equals(invPurchaseDetail.BatchNo)).FirstOrDefault();
                }
            }
            else
            {
                //invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
                invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && p.BatchNo.Equals(invPurchaseDetail.BatchNo)).FirstOrDefault();
            }

            if (invPurchaseDetailTemp == null || invPurchaseDetailTemp.LineNo.Equals(0))
            {
                if (invPurchaseTempDetail.Count.Equals(0))
                    invPurchaseDetail.LineNo = 1;
                else
                    invPurchaseDetail.LineNo = invPurchaseTempDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invPurchaseTempDetail.Remove(invPurchaseDetailTemp);
                invPurchaseDetail.LineNo = invPurchaseDetailTemp.LineNo;
            }

            invPurchaseTempDetail.Add(invPurchaseDetail);

            return invPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }




        public List<InvPurchaseDetailTemp> getDeletePurchaseDetailTemp(List<InvPurchaseDetailTemp> invPurchaseTempDetail, InvPurchaseDetailTemp invPurchaseDetail, List<InvProductSerialNoTemp> invProductSerialNoListToDelete, out List<InvProductSerialNoTemp> invProductSerialNoList)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp();
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            //invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && p.ExpiryDate == invPurchaseDetail.ExpiryDate || p.ExpiryDate == null).FirstOrDefault();

            invPurchaseDetailTemp = invPurchaseTempDetail.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID).FirstOrDefault();
            
            long removedLineNo = 0;
            if (invPurchaseDetailTemp != null )
            {
                invPurchaseTempDetail.Remove(invPurchaseDetailTemp);
                removedLineNo = invPurchaseDetailTemp.LineNo;
            }
            invPurchaseTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            //return invPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();

            if (invProductSerialNoListToDelete != null)
            {
                invProductSerialNoTempList = invProductSerialNoListToDelete.Where(p => p.ProductID == invPurchaseDetail.ProductID && p.UnitOfMeasureID == invPurchaseDetail.UnitOfMeasureID && p.ExpiryDate == invPurchaseDetail.ExpiryDate).ToList();
            }

            foreach (InvProductSerialNoTemp InvProductSerialNoTempDelete in invProductSerialNoTempList)
            {
                invProductSerialNoListToDelete.Remove(InvProductSerialNoTempDelete);
            }

            invProductSerialNoList = invProductSerialNoListToDelete;

            return invPurchaseTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvProductSerialNoTemp> getUpdateSerialNoTemp(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber)
        {
            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

            invProductSerialNoTemp = invProductSerialNoList.Where(s => s.SerialNo.Equals(invProductSerialNumber.SerialNo) && s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).FirstOrDefault();

            if (invProductSerialNoTemp == null || invProductSerialNoTemp.LineNo.Equals(0))
            {
                if (invProductSerialNoList.Count.Equals(0))
                    invProductSerialNumber.LineNo = 1;
                else
                {
                    if (!invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Count().Equals(0))
                        invProductSerialNumber.LineNo = invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Max(s => s.LineNo) + 1;
                    else
                        invProductSerialNumber.LineNo = 1;
                }
                invProductSerialNoList.Add(invProductSerialNumber);

            }

            return invProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfSerialNo(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber,decimal Qty)
        {
          
            if(Qty.Equals(invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Count()))
               return true;
            else
                return false;
           
        }

        public List<InvPurchaseDetailTemp> GetPausedPurchaseDetail(InvPurchaseHeader invPurchaseHeader)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp;
            List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join pd in context.InvPurchaseDetails on pm.InvProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.CompanyID.Equals(invPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseHeaderID.Equals(invPurchaseHeader.PurchaseHeaderID)
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
                invPurchaseDetailTemp = new InvPurchaseDetailTemp();

                invPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                invPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                invPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                invPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                invPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                invPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                invPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                invPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                invPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;

                if (tempProduct.BatchNo != null)
                {
                    invPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;
                }
                   
                else
                {
                    invPurchaseDetailTemp.BatchNo = string.Empty;
                }
                invPurchaseDetailTempList.Add(invPurchaseDetailTemp);
            }
            return invPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvPurchaseDetailTemp> GetPausedPurchaseDetailToPRN(InvPurchaseHeader invPurchaseHeader, int documentID) 
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp;
            List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join pd in context.InvPurchaseDetails on pm.InvProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.InvPurchaseHeaders on pd.InvPurchaseHeaderID equals ph.InvPurchaseHeaderID
                                      where pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.CompanyID.Equals(invPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseHeaderID.Equals(invPurchaseHeader.PurchaseHeaderID)
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
                invPurchaseDetailTemp = new InvPurchaseDetailTemp();

                invPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                invPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                invPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                invPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                invPurchaseDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((tempProduct.BalanceQty * tempProduct.CostPrice).ToString()); 
                invPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                invPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                invPurchaseDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(((tempProduct.BalanceQty * tempProduct.CostPrice) - ((tempProduct.BalanceQty * tempProduct.DiscountPercentage * tempProduct.CostPrice) / 100)).ToString());
                invPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;
                invPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;

                invPurchaseDetailTempList.Add(invPurchaseDetailTemp);
            }
            return invPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfQty(decimal qty, long productID, int unitOfMeasureID, long purchaseHeaderID)
        {
            decimal poQty = 0;
            var getCurrentQty = (from a in context.InvPurchaseDetails
                                 where a.ProductID.Equals(productID) && a.InvPurchaseHeaderID.Equals(purchaseHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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

        

        public InvPurchaseHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.InvPurchaseHeaders.Where(ph => ph.DocumentNo == documentNo.Trim() && ph.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvPurchaseHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.InvPurchaseHeaders.Where(po => po.InvPurchaseHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvPurchaseHeader GetInvPurchaseHeaderByDocumentNo(int documentID, string documentNo, int locationID) 
        {
            return context.InvPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public TransactionDet GetTransactionDetailsByDocumentNo(string documentNo, int locationID)
        {
            return context.TransactionDets.Where(ph => ph.Receipt.Equals(documentNo) && ph.LocationID.Equals(locationID) && ph.Status.Equals(1) && ph.DataTransfer.Equals(1)).FirstOrDefault();
        }


        public List<InvPurchaseDetailTemp> GetPurchaseOrderDetail(InvPurchaseOrderHeader invPurchaseOrderHeader)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp;

            List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();


            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join pod in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pod.ProductID
                                      join uc in context.UnitOfMeasures on pod.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pod.LocationID.Equals(invPurchaseOrderHeader.LocationID) && pod.CompanyID.Equals(invPurchaseOrderHeader.CompanyID) && pod.BalanceQty > 0
                                      && pod.DocumentID.Equals(invPurchaseOrderHeader.DocumentID) && pod.InvPurchaseOrderHeaderID.Equals(invPurchaseOrderHeader.InvPurchaseOrderHeaderID)
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
                                          pm.IsBatch,
                                          BatchNo = (pod.IsBatch == true ? invPurchaseOrderHeader.DocumentNo : string.Empty),

                                      }).ToArray();

            foreach (var tempProduct in purchaseDetailTemp)
            {

                invPurchaseDetailTemp = new InvPurchaseDetailTemp();

                invPurchaseDetailTemp.AverageCost = tempProduct.AverageCost;
                invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                //invPurchaseDetailTemp.DiscountAmount = Common.ConvertStringToDecimalCurrency(((tempProduct.BalanceQty * tempProduct.DiscountPercentage * tempProduct.CostPrice)/100).ToString());
                invPurchaseDetailTemp.DiscountAmount = Common.ConvertStringToDecimalCurrency((((tempProduct.DiscountAmount/tempProduct.OrderQty) * tempProduct.BalanceQty)).ToString());
                invPurchaseDetailTemp.DiscountPercentage =tempProduct.DiscountPercentage;
                invPurchaseDetailTemp.ExpiryDate = null;
                invPurchaseDetailTemp.FreeQty = tempProduct.BalanceFreeQty;
                invPurchaseDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((tempProduct.BalanceQty * tempProduct.CostPrice).ToString());
                invPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                invPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                //invPurchaseDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency(((tempProduct.BalanceQty * tempProduct.CostPrice) - ((tempProduct.BalanceQty * tempProduct.DiscountPercentage * tempProduct.CostPrice) / 100)).ToString());
                invPurchaseDetailTemp.NetAmount = (invPurchaseDetailTemp.GrossAmount) - (invPurchaseDetailTemp.DiscountAmount);
                 invPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseDetailTemp.BaseUnitID = tempProduct.BaseUnitID;
                invPurchaseDetailTemp.IsBatch = tempProduct.IsBatch;
                invPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;
                
                invPurchaseDetailTempList.Add(invPurchaseDetailTemp);

            }
            return invPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvProductSerialNoTemp> getPausedPurchaseSerialNoDetail(InvPurchaseHeader invPurchaseHeader)
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

            invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps=> ps.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();
            foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
            {
                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;

                invProductSerialNoTempList.Add(invProductSerialNoTemp);

            }

            return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public List<OtherExpenseTemp> getPausedExpence(InvPurchaseHeader invPurchaseHeader)
        {


            List<OtherExpenseTemp> otherExpenceTempList = new List<OtherExpenseTemp>();
            List<AccGlTransactionDetail> accGlTransactionDetails = new List<AccGlTransactionDetail>();
            AccLedgerAccountService AccLedgerAccountService = new AccLedgerAccountService();

            accGlTransactionDetails = context.AccGlTransactionDetails.Where(ps => ps.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).OrderBy(ps => ps.AccGlTransactionDetailID).ToList();
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

        public List<PaymentTemp> getPausedPayment(InvPurchaseHeader invPurchaseHeader)
        {


            List<PaymentTemp> paymentTempList = new List<PaymentTemp>();
            List<AccPaymentDetail> accPaymentDetail = new List<AccPaymentDetail>();
            PaymentMethodService paymentMethodService = new PaymentMethodService();

            accPaymentDetail = context.AccPaymentDetails.Where(ps => ps.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).OrderBy(ps => ps.AccPaymentDetailID).ToList();
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

        public InvPurchaseHeader getPurchaseHeaderByDocumentNo(int documentID,string documentNo,int locationID)
        {
            return context.InvPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public InvPurchaseHeader getPausedPurchaseHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.InvPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.PurchaseHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public List<InvProductBatchNoTemp> GetBatchNoDetail(InvProductMaster invProductMaster, int locationID) 
        {
            List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

            List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

            invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.LocationID.Equals(locationID) && ps.BalanceQty > 0).ToList();
            foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
            {
                InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
            }

            return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public List<InvProductBatchNoTemp> GetExpiryDetail(InvProductMaster invProductMaster, string batchNo)
        {
            List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

            List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

            invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.BatchNo.Equals(batchNo) && ps.BalanceQty > 0).ToList();
            foreach (InvProductBatchNoExpiaryDetail invProductBatchNoTemp in invProductBatchNoDetail)
            {
                InvProductBatchNoTemp invProductBatchNoDetailTemp = new InvProductBatchNoTemp();

                invProductBatchNoDetailTemp.LineNo = invProductBatchNoTemp.LineNo;
                invProductBatchNoDetailTemp.ProductID = invProductBatchNoTemp.ProductID;
                invProductBatchNoDetailTemp.BatchNo = invProductBatchNoTemp.BatchNo;
                invProductBatchNoDetailTemp.ExpiryDate = invProductBatchNoTemp.ExpiryDate;
                invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
            } 

            return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public List<InvProductSerialNoTemp> GetSerialNoDetail(InvProductMaster invProductMaster)
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            List<InvProductSerialNo> invProductSerialNo = new List<InvProductSerialNo>();

            invProductSerialNo = context.InvProductSerialNos.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID)).ToList();
            foreach (InvProductSerialNo invProductSerialNoRetrive in invProductSerialNo)
            {
                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                invProductSerialNoTemp.ExpiryDate = invProductSerialNoRetrive.ExpiryDate;
                invProductSerialNoTemp.ProductID = invProductSerialNoRetrive.ProductID;
                invProductSerialNoTemp.SerialNo = invProductSerialNoRetrive.SerialNo;
                invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoRetrive.UnitOfMeasureID;

                invProductSerialNoTempList.Add(invProductSerialNoTemp);
            }

            return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp,int locationID)
        {

            InvPurchaseHeader invPurchaseHeader = new InvPurchaseHeader();
            invPurchaseHeader = getPurchaseHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo,locationID);
            if (invPurchaseHeader == null)
                invPurchaseHeader = new InvPurchaseHeader();

            InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
            InvProductSerialNoDetailtmp = null;

            if (invProductSerialNoTemp.ExpiryDate != null)
            {
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                            && sd.ReferenceDocumentDocumentID == invProductSerialNoTemp.DocumentID
                                            && sd.ReferenceDocumentID != invPurchaseHeader.PurchaseHeaderID
                                            && sd.ProductID == invProductSerialNoTemp.ProductID
                                            && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID
                                            && sd.ExpiryDate == invProductSerialNoTemp.ExpiryDate
                                            && sd.DocumentStatus == 0).FirstOrDefault();
            }
            else
            {
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                           && sd.ReferenceDocumentDocumentID == invProductSerialNoTemp.DocumentID
                                           && sd.ReferenceDocumentID != invPurchaseHeader.PurchaseHeaderID
                                           && sd.ProductID == invProductSerialNoTemp.ProductID
                                           && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID
                                           && sd.DocumentStatus == 0).FirstOrDefault();
            }
            if (InvProductSerialNoDetailtmp != null)
            {
                invPurchaseHeader = getPausedPurchaseHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID,locationID);
                if (invPurchaseHeader!=null)
                    return invPurchaseHeader.DocumentNo;
                else
                    return string.Empty;

                 
            }
            else
            {
               
                return string.Empty;
            }
           
        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp invProductSerialNoTemp,int locationID)
        {

            InvProductSerialNo invProductSerialNo = new InvProductSerialNo();
            //invProductSerialNo = context.InvProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID && sd.ExpiryDate == invProductSerialNoTemp.ExpiryDate && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();
            if (invProductSerialNoTemp.ExpiryDate != null)
                invProductSerialNo = context.InvProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID && sd.ExpiryDate==invProductSerialNoTemp.ExpiryDate && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();
            else
                invProductSerialNo = context.InvProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID &&  sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();

            if (invProductSerialNo != null && invProductSerialNo.InvProductSerialNoID != 0)
                return true;
            else
                return false;
        }
        //public bool Save(InvPurchaseHeader invPurchaseHeader, List<InvPurchaseDetail> invPurchaseDetail)
        //{

        //        context.InvPurchaseHeaders.Add(invPurchaseHeader);
           

        //    context.SaveChanges();

        //    foreach (InvPurchaseDetail invPurchaseDetailtmp in invPurchaseDetail)
        //    {
              
        //        context.InvPurchaseDetails.Add(invPurchaseDetailtmp);
        //        context.SaveChanges();
        //    }

        //    return true;
        //}

        public bool Save(InvPurchaseHeader invPurchaseHeader, List<InvPurchaseDetailTemp> invPurchaseDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, List<OtherExpenseTemp> otherExpenceTempList, List<PaymentTemp> paymentTempList,decimal paidAmount, string formName = "",bool isTStatus=false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();

                #region PurchaseOrder Header Update (Authorized flag)

                context.InvPurchaseOrderHeaders.Update(ph => ph.InvPurchaseOrderHeaderID.Equals(invPurchaseHeader.ReferenceDocumentID), ph => new InvPurchaseOrderHeader
                {
                    IsAuthorized = false,
                });

                #endregion

                #region Purchase Header Save

                if (invPurchaseHeader.DocumentStatus.Equals(1))
                {
                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    LocationService locationService = new LocationService();
                    invPurchaseHeader.DocumentNo = invPurchaseServices.GetDocumentNo(formName, invPurchaseHeader.LocationID, locationService.GetLocationsByID(invPurchaseHeader.LocationID).LocationCode, invPurchaseHeader.DocumentID, false).Trim();
                    if (!poIsMandatory) { invPurchaseHeader.BatchNo = invPurchaseHeader.DocumentNo; }
                }
                newDocumentNo = invPurchaseHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (invPurchaseHeader.InvPurchaseHeaderID.Equals(0))
                    context.InvPurchaseHeaders.Add(invPurchaseHeader);
                else
                    context.Entry(invPurchaseHeader).State = EntityState.Modified;

                context.SaveChanges();

                //context.InvPurchaseHeaders.Update(ph => ph.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && ph.LocationID.Equals(invPurchaseHeader.LocationID) && ph.DocumentID.Equals(invPurchaseHeader.DocumentID), phNew => new InvPurchaseHeader { PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID });

                #endregion

                #region Deleted

                //List<InvPurchaseDetail> invPurchaseDetailDelete = new List<InvPurchaseDetail>();
                //invPurchaseDetailDelete = context.InvPurchaseDetails.Where(p => p.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.DocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();

                //foreach (InvPurchaseDetail invPurchaseDetailDeleteTemp in invPurchaseDetailDelete)
                //{
                //    context.InvPurchaseDetails.Remove(invPurchaseDetailDeleteTemp);
                //}

                #endregion

                #region Purchase Detail Save

                context.Set<InvPurchaseDetail>().Delete(context.InvPurchaseDetails.Where(p => p.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.DocumentID.Equals(invPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                #region UsingSQL

                //string sql = "DELETE FROM InvPurchaseDetail WHERE PurchaseHeaderID=@PurchaseHeaderID AND LocationID=@LocationID AND DocumentID=@DocumentID AND DocumentStatus=0";
                //var args = new DbParameter[] 
                //{ 
                //    new SqlParameter { ParameterName ="@PurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                //    new SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                //    new SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID}
                //};

                //CommonService.ExecuteSql(sql, args);

                #endregion

                #region Deleted

                //List<InvProductSerialNoDetail> invProductSerialNoDetailDelete = new List<InvProductSerialNoDetail>();
                //invProductSerialNoDetailDelete = context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();



                //foreach (InvProductSerialNoDetail invProductSerialNoDetailDeleteTemp in invProductSerialNoDetailDelete)
                //{
                //    context.InvProductSerialNoDetails.Remove(invProductSerialNoDetailDeleteTemp);
                //}

                //context.SaveChanges();

                #endregion

                var invPurchaseDetailSaveQuery = (from pd in invPurchaseDetailTemp
                                                  select new
                                                  {
                                                      InvPurchaseDetailID = 0,
                                                      PurchaseDetailID = 0,
                                                      PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID,
                                                      CompanyID = invPurchaseHeader.CompanyID,
                                                      LocationID = invPurchaseHeader.LocationID,
                                                      CostCentreID = invPurchaseHeader.CostCentreID,
                                                      DocumentID = invPurchaseHeader.DocumentID,
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
                                                      DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                      BatchNo = (pd.IsBatch == true ? invPurchaseHeader.BatchNo : string.Empty),
                                                      IsBatch = pd.IsBatch,
                                                      GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                      CreatedUser = invPurchaseHeader.CreatedUser,
                                                      CreatedDate = invPurchaseHeader.CreatedDate,
                                                      ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                      ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                      DataTransfer = invPurchaseHeader.DataTransfer
                                                  }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(invPurchaseDetailSaveQuery), "InvPurchaseDetail");

                //var entityConn = context.Database.Connection as System.Data.EntityClient.EntityConnection;
                //var dbConn = context.Database.Connection as SqlConnection;

                //SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                //bulkInsert.DestinationTableName = "InvPurchaseDetail";
                //dbConn.Open();
                //bulkInsert.WriteToServer(Common.LINQToDataTable(invPurchaseDetailSaveQuery));
                //dbConn.Close();


                // context.InvPurchaseDetails.Update(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID), pdNew => new InvPurchaseDetail { PurchaseDetailID = pdNew.InvPurchaseDetailID });

                // context.InvPurchaseDetails.Where(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID)).Update(pdNew => new InvPurchaseDetail() { PurchaseDetailID = pdNew.InvPurchaseDetailID+0 });


                //sql = "Update InvPurchaseDetail Set PurchaseDetailID = pdNew.InvPurchaseDetailID Where PurchaseHeaderID=@PurchaseHeaderID And LocationID=@LocationID And DocumentID=@DocumentID ";
                //var argsPurchaseDetail = new DbParameter[] 
                //{ 
                //    new System.Data.SqlClient.SqlParameter { ParameterName ="@PurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                //    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                //    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID}
                //};

                //CommonService.ExecuteSql(sql, argsPurchaseDetail);


                if (invPurchaseHeader.DocumentStatus.Equals(1))
                {
                    //context.InvProductStockMasters.Update(ps => context.InvPurchaseDetails.Any(pd=> ps.LocationID.Equals(invPurchaseHeader.LocationID) && ps.ProductID.Equals(pd.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (pd.Qty * ps.ConvertFactor) });


                    // context.InvProductStockMasters.Update(context.InvProductStockMasters.Join(context.InvPurchaseDetails, ps => ps.ProductID, pd => pd.ProductID,(ps,pd) => new (InvProductStockMaster=ps,InvPurchaseDetail=pd) {ps.Stock=(pd.Qty*pd.ConvertFactor)}));
                    //if (invPurchaseDetail.UnitOfMeasureID.Equals(invPurchaseDetail.BaseUnitID))
                    //    context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invPurchaseDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });
                    //else
                    //    context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invPurchaseDetail.ProductID) && uc.UnitOfMeasureID.Equals(invPurchaseDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });

                }

                #endregion

                #region Deleted

                //foreach (InvPurchaseDetailTemp invPurchaseDetail in invPurchaseDetailTemp)
                //{
                //    InvPurchaseDetail invPurchaseDetailSave = new InvPurchaseDetail();


                //    invPurchaseDetailSave.AvgCost = invPurchaseDetail.AverageCost;
                //    invPurchaseDetailSave.CompanyID = invPurchaseHeader.CompanyID;
                //    invPurchaseDetailSave.ConvertFactor = invPurchaseDetail.ConvertFactor;
                //    invPurchaseDetailSave.CostCentreID = invPurchaseHeader.CostCentreID;
                //    invPurchaseDetailSave.CostPrice = invPurchaseDetail.CostPrice;
                //    invPurchaseDetailSave.CurrentQty = invPurchaseDetail.CurrentQty;
                //    invPurchaseDetailSave.DiscountAmount = invPurchaseDetail.DiscountAmount;
                //    invPurchaseDetailSave.DiscountPercentage = invPurchaseDetail.DiscountPercentage;
                //    invPurchaseDetailSave.DocumentID = invPurchaseHeader.DocumentID;
                //    invPurchaseDetailSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                //    invPurchaseDetailSave.ExpiryDate = invPurchaseDetail.ExpiryDate;
                //    invPurchaseDetailSave.FreeQty = invPurchaseDetail.FreeQty;
                //    invPurchaseDetailSave.GrossAmount = invPurchaseDetail.GrossAmount;
                //    invPurchaseDetailSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                //    invPurchaseDetailSave.LineNo = invPurchaseDetail.LineNo;
                //    invPurchaseDetailSave.LocationID = invPurchaseHeader.LocationID;
                //    invPurchaseDetailSave.NetAmount = invPurchaseDetail.NetAmount;
                //    invPurchaseDetailSave.OrderQty = invPurchaseDetail.OrderQty;
                //    invPurchaseDetailSave.ProductID = invPurchaseDetail.ProductID;
                //    invPurchaseDetailSave.PurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID;
                //    invPurchaseDetailSave.Qty = invPurchaseDetail.Qty;
                //    invPurchaseDetailSave.BalanceQty = invPurchaseDetail.Qty;
                //    invPurchaseDetailSave.SellingPrice = invPurchaseDetail.SellingPrice;
                //    invPurchaseDetailSave.SubTotalDiscount = invPurchaseDetail.SubTotalDiscount;
                //    invPurchaseDetailSave.TaxAmount1 = invPurchaseDetail.TaxAmount1;
                //    invPurchaseDetailSave.TaxAmount2 = invPurchaseDetail.TaxAmount2;
                //    invPurchaseDetailSave.TaxAmount3 = invPurchaseDetail.TaxAmount3;
                //    invPurchaseDetailSave.TaxAmount4 = invPurchaseDetail.TaxAmount4;
                //    invPurchaseDetailSave.TaxAmount5 = invPurchaseDetail.TaxAmount5;
                //    invPurchaseDetailSave.UnitOfMeasureID = invPurchaseDetail.UnitOfMeasureID;
                //    invPurchaseDetailSave.BaseUnitID = invPurchaseDetail.BaseUnitID;


                //    //if (invPurchaseHeader.DocumentStatus.Equals(1))
                //    //{
                //    //    context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invPurchaseHeader.LocationID) && ps.ProductID.Equals(invPurchaseDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + (invPurchaseDetail.Qty * invPurchaseDetail.ConvertFactor) });

                //    //    if (invPurchaseDetail.UnitOfMeasureID.Equals(invPurchaseDetail.BaseUnitID))
                //    //        context.InvProductMasters.Update(pm => pm.InvProductMasterID.Equals(invPurchaseDetail.ProductID), pmNew => new InvProductMaster { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });
                //    //    else
                //    //        context.InvProductUnitConversions.Update(uc => uc.ProductID.Equals(invPurchaseDetail.ProductID) && uc.UnitOfMeasureID.Equals(invPurchaseDetail.UnitOfMeasureID), ucNew => new InvProductUnitConversion { CostPrice = invPurchaseDetail.CostPrice, SellingPrice = invPurchaseDetail.SellingPrice });

                //    //}
                //    context.InvPurchaseDetails.Add(invPurchaseDetailSave);

                //    //context.SaveChanges();
                //    //context.InvPurchaseDetails.Update(pd => pd.PurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseDetailID.Equals(invPurchaseDetailSave.InvPurchaseDetailID), pdNew => new InvPurchaseDetail { PurchaseDetailID = invPurchaseDetailSave.InvPurchaseDetailID });


                //}

                #endregion

                #region Serial No Detail Save

                context.Set<InvProductSerialNoDetail>().Delete(context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                if (invPurchaseHeader.DocumentStatus.Equals(1))
                {
                    if (poIsMandatory)
                    {
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(invPurchaseHeader.ReferenceDocumentID);

                        var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                                                           select new
                                                           {
                                                               InvProductSerialNoID = 0,
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                               CompanyID = invPurchaseHeader.CompanyID,
                                                               LocationID = invPurchaseHeader.LocationID,
                                                               CostCentreID = invPurchaseHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = invPurchaseOrderHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = invPurchaseHeader.DocumentID,
                                                               DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                               CreatedUser = invPurchaseHeader.CreatedUser,
                                                               CreatedDate = invPurchaseHeader.CreatedDate,
                                                               ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                               ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                               DataTransfer = invPurchaseHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");

                    }
                    else
                    {
                        var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                                                           select new
                                                           {
                                                               InvProductSerialNoID = 0,
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                               CompanyID = invPurchaseHeader.CompanyID,
                                                               LocationID = invPurchaseHeader.LocationID,
                                                               CostCentreID = invPurchaseHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = invPurchaseHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = invPurchaseHeader.DocumentID,
                                                               DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                               CreatedUser = invPurchaseHeader.CreatedUser,
                                                               CreatedDate = invPurchaseHeader.CreatedDate,
                                                               ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                               ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                               DataTransfer = invPurchaseHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");
                    }
                }
                else
                {
                    if (poIsMandatory)
                    {
                        InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
                        InvPurchaseOrderHeader invPurchaseOrderHeader = new InvPurchaseOrderHeader();
                        invPurchaseOrderHeader = invPurchaseOrderService.GetSavedDocumentDetailsByDocumentID(invPurchaseHeader.ReferenceDocumentID);

                        var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     InvProductSerialNoDetailID = 0,
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                                     CompanyID = invPurchaseHeader.CompanyID,
                                                                     LocationID = invPurchaseHeader.LocationID,
                                                                     CostCentreID = invPurchaseHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID,
                                                                     ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                                     BatchNo = invPurchaseOrderHeader.DocumentNo,
                                                                     CreatedUser = invPurchaseHeader.CreatedUser,
                                                                     CreatedDate = invPurchaseHeader.CreatedDate,
                                                                     ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                                     ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                                     DataTransfer = invPurchaseHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");

                    }
                    else
                    {
                        var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     InvProductSerialNoDetailID = 0,
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                                     CompanyID = invPurchaseHeader.CompanyID,
                                                                     LocationID = invPurchaseHeader.LocationID,
                                                                     CostCentreID = invPurchaseHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID,
                                                                     ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                                     BatchNo = invPurchaseHeader.DocumentNo,
                                                                     CreatedUser = invPurchaseHeader.CreatedUser,
                                                                     CreatedDate = invPurchaseHeader.CreatedDate,
                                                                     ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                                     ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                                     DataTransfer = invPurchaseHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");
                    }
                }

                #region Deleted

                // foreach (InvProductSerialNoTemp invPurchaseDetailSerialNo in invProductSerialNoTemp)
                // {
                //InvProductSerialNoDetail invProductSerialNoDetailSave = new InvProductSerialNoDetail();

                //invProductSerialNoDetailSave.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                //invProductSerialNoDetailSave.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;
                //if (invPurchaseHeader.DocumentStatus.Equals(1))
                //{
                //    InvProductSerialNo invProductSerialNoSave = new InvProductSerialNo();

                //    invProductSerialNoSave.CompanyID = invPurchaseHeader.CompanyID;
                //    invProductSerialNoSave.CostCentreID = invPurchaseHeader.CostCentreID;
                //    invProductSerialNoSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                //    invProductSerialNoSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                //    invProductSerialNoSave.LocationID = invPurchaseHeader.LocationID;
                //    invProductSerialNoSave.ProductID = invPurchaseDetailSerialNo.ProductID;
                //    invProductSerialNoSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
                //    invProductSerialNoSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
                //    invProductSerialNoSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
                //    invProductSerialNoSave.SerialNoStatus = 1;


                //    context.InvProductSerialNos.Add(invProductSerialNoSave);
                //}


                //invProductSerialNoDetailSave.CompanyID = invPurchaseHeader.CompanyID;
                //invProductSerialNoDetailSave.CostCentreID = invPurchaseHeader.CostCentreID;
                //invProductSerialNoDetailSave.DocumentStatus = invPurchaseHeader.DocumentStatus;
                //invProductSerialNoDetailSave.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                //invProductSerialNoDetailSave.LocationID = invPurchaseHeader.LocationID;
                //invProductSerialNoDetailSave.ProductID = invPurchaseDetailSerialNo.ProductID;
                //invProductSerialNoDetailSave.LineNo = invPurchaseDetailSerialNo.LineNo;
                //invProductSerialNoDetailSave.UnitOfMeasureID = invPurchaseDetailSerialNo.UnitOfMeasureID;
                //invProductSerialNoDetailSave.ExpiryDate = invPurchaseDetailSerialNo.ExpiryDate;
                //invProductSerialNoDetailSave.SerialNo = invPurchaseDetailSerialNo.SerialNo;
                //invProductSerialNoDetailSave.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                //invProductSerialNoDetailSave.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;

                //context.InvProductSerialNoDetails.Add(invProductSerialNoDetailSave);
                //context.SaveChanges();

                //context.InvProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvProductSerialNoDetailID.Equals(invProductSerialNoDetailSave.InvProductSerialNoDetailID), pdNew => new InvProductSerialNoDetail { ProductSerialNoDetailID = invProductSerialNoDetailSave.InvProductSerialNoDetailID });

                // }
                #endregion

                #endregion

                #region Account Entries

                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
                AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
                accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(invPurchaseHeader.InvPurchaseHeaderID, invPurchaseHeader.DocumentID, invPurchaseHeader.LocationID);
                if (accGlTransactionHeader == null)
                    accGlTransactionHeader = new AccGlTransactionHeader();

                accGlTransactionHeader.Amount = invPurchaseHeader.NetAmount;
                accGlTransactionHeader.CompanyID = invPurchaseHeader.CompanyID;
                accGlTransactionHeader.CostCentreID = invPurchaseHeader.CostCentreID;
                accGlTransactionHeader.CreatedDate = invPurchaseHeader.CreatedDate;
                accGlTransactionHeader.CreatedUser = invPurchaseHeader.CreatedUser;
                accGlTransactionHeader.DocumentDate = invPurchaseHeader.DocumentDate;
                accGlTransactionHeader.DocumentID = invPurchaseHeader.DocumentID;
                accGlTransactionHeader.DocumentStatus = invPurchaseHeader.DocumentStatus;
                accGlTransactionHeader.DocumentNo = string.Empty;
                accGlTransactionHeader.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                accGlTransactionHeader.DataTransfer = invPurchaseHeader.DataTransfer;
                accGlTransactionHeader.IsUpLoad = false;
                accGlTransactionHeader.LocationID = invPurchaseHeader.LocationID;
                accGlTransactionHeader.ModifiedDate = invPurchaseHeader.ModifiedDate;
                accGlTransactionHeader.ModifiedUser = invPurchaseHeader.ModifiedUser;
                accGlTransactionHeader.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;
                accGlTransactionHeader.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                accGlTransactionHeader.ReferenceDocumentNo = invPurchaseHeader.DocumentNo;
                accGlTransactionHeader.ReferenceID = invPurchaseHeader.SupplierID;

                if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                    accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);
                else
                    accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);
                context.SaveChanges();

                context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

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
                                                     TransactionTypeId = invPurchaseHeader.DocumentID,
                                                     ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                     ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                     ReferenceLocationID = 0,
                                                     ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                     TransactionDefinitionId = 1,
                                                     ChequeNo = string.Empty,
                                                     Reference = "OTHER EXPENCE",
                                                     Remark = "GOOD RECEIVED NOTE",
                                                     DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                     IsTStatus = isTStatus,
                                                     GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                     CreatedUser = invPurchaseHeader.CreatedUser,
                                                     CreatedDate = invPurchaseHeader.CreatedDate,
                                                     ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                     ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                     DataTransfer = invPurchaseHeader.DataTransfer
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
                                                        TransactionTypeId = invPurchaseHeader.DocumentID,
                                                        ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                        ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                        ReferenceLocationID = 0,
                                                        ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                        TransactionDefinitionId = 1,
                                                        ChequeNo = string.Empty,
                                                        Reference = "PAYMENTS",
                                                        Remark = "GOOD RECEIVED NOTE",
                                                        DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                        IsTStatus = isTStatus,
                                                        GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                        CreatedUser = invPurchaseHeader.CreatedUser,
                                                        CreatedDate = invPurchaseHeader.CreatedDate,
                                                        ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                        ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                        DataTransfer = invPurchaseHeader.DataTransfer
                                                    }).ToList();

                    CommonService.BulkInsert(Common.LINQToDataTable(paymentAccountsSaveQuery), "AccGlTransactionDetail");
                }

                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();
                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmSupplier").DocumentID);

                if (accTransactionTypeDetail != null)
                {
                    var supplierAccountEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                             Amount = invPurchaseHeader.NetAmount - paidAmount,
                                                             TransactionTypeId = invPurchaseHeader.DocumentID,
                                                             ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                             ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                             ReferenceLocationID = 0,
                                                             ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                             TransactionDefinitionId = 1,
                                                             ChequeNo = string.Empty,
                                                             Reference = "SUPPLIER",
                                                             Remark = "GOOD RECEIVED NOTE",
                                                             DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                             IsTStatus = isTStatus,
                                                             GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                             CreatedUser = invPurchaseHeader.CreatedUser,
                                                             CreatedDate = invPurchaseHeader.CreatedDate,
                                                             ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                             ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                             DataTransfer = invPurchaseHeader.DataTransfer
                                                         }).ToList();


                    CommonService.BulkInsert(Common.LINQToDataTable(supplierAccountEntrySaveQuery), "AccGlTransactionDetail");
                }

                if (invPurchaseHeader.DiscountAmount > 0)
                {
                    accTransactionTypeDetail = new AccTransactionTypeDetail();
                    accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID, 2);

                    if (accTransactionTypeDetail != null)
                    {
                        var discountAccountEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                                 Amount = invPurchaseHeader.DiscountAmount,
                                                                 TransactionTypeId = invPurchaseHeader.DocumentID,
                                                                 ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                                 ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                                 ReferenceLocationID = 0,
                                                                 ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                                 TransactionDefinitionId = 2,
                                                                 ChequeNo = string.Empty,
                                                                 Reference = "DISCOUNT",
                                                                 Remark = "GOOD RECEIVED NOTE",
                                                                 DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                                 IsTStatus = isTStatus,
                                                                 GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                                 CreatedUser = invPurchaseHeader.CreatedUser,
                                                                 CreatedDate = invPurchaseHeader.CreatedDate,
                                                                 ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                                 ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                                 DataTransfer = invPurchaseHeader.DataTransfer
                                                             }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(discountAccountEntrySaveQuery), "AccGlTransactionDetail");
                    }
                }

                #region Tax1

                DataTable dt = new DataTable();
                CommonService commonService = new CommonService();
                dt = commonService.GetAllTaxesRelatedToSupplier(invPurchaseHeader.SupplierID);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0) // Tax1
                    {

                        if (invPurchaseHeader.TaxAmount1 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                         Amount = invPurchaseHeader.TaxAmount1,
                                                         TransactionTypeId = invPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 1",
                                                         Remark = "GOOD RECEIVED NOTE",
                                                         DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = invPurchaseHeader.CreatedUser,
                                                         CreatedDate = invPurchaseHeader.CreatedDate,
                                                         ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                         DataTransfer = invPurchaseHeader.DataTransfer
                                                     }).ToList();


                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 1) // Tax2
                    {
                        if (invPurchaseHeader.TaxAmount2 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                         Amount = invPurchaseHeader.TaxAmount2,
                                                         TransactionTypeId = invPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 2",
                                                         Remark = "GOOD RECEIVED NOTE",
                                                         DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = invPurchaseHeader.CreatedUser,
                                                         CreatedDate = invPurchaseHeader.CreatedDate,
                                                         ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                         DataTransfer = invPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 2) // Tax3
                    {
                        if (invPurchaseHeader.TaxAmount3 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                         Amount = invPurchaseHeader.TaxAmount3,
                                                         TransactionTypeId = invPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 3",
                                                         Remark = "GOOD RECEIVED NOTE",
                                                         DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = invPurchaseHeader.CreatedUser,
                                                         CreatedDate = invPurchaseHeader.CreatedDate,
                                                         ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                         DataTransfer = invPurchaseHeader.DataTransfer
                                                     }).ToList();
                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 3) // Tax4
                    {
                        if (invPurchaseHeader.TaxAmount4 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                         Amount = invPurchaseHeader.TaxAmount4,
                                                         TransactionTypeId = invPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 4",
                                                         Remark = "GOOD RECEIVED NOTE",
                                                         DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = invPurchaseHeader.CreatedUser,
                                                         CreatedDate = invPurchaseHeader.CreatedDate,
                                                         ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                         DataTransfer = invPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else if (i == 4) // Tax5
                    {
                        if (invPurchaseHeader.TaxAmount5 > 0)
                        {
                            var taxEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                         Amount = invPurchaseHeader.TaxAmount5,
                                                         TransactionTypeId = invPurchaseHeader.DocumentID,
                                                         ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                         ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                         ReferenceLocationID = 0,
                                                         ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                         TransactionDefinitionId = 2,
                                                         ChequeNo = string.Empty,
                                                         Reference = "TAX 5",
                                                         Remark = "GOOD RECEIVED NOTE",
                                                         DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                         IsTStatus = isTStatus,
                                                         GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                         CreatedUser = invPurchaseHeader.CreatedUser,
                                                         CreatedDate = invPurchaseHeader.CreatedDate,
                                                         ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                         ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                         DataTransfer = invPurchaseHeader.DataTransfer
                                                     }).ToList();

                            CommonService.BulkInsert(Common.LINQToDataTable(taxEntrySaveQuery), "AccGlTransactionDetail");
                        }
                    }
                    else // More than 5
                    {

                    }
                }

                #endregion


                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote").DocumentID);

                if (accTransactionTypeDetail != null)
                {
                    var purchaseAccountEntrySaveQuery = (from oe in invPurchaseDetailTemp
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
                                                             Amount = invPurchaseHeader.GrossAmount,
                                                             TransactionTypeId = invPurchaseHeader.DocumentID,
                                                             ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                                                             ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                                                             ReferenceLocationID = 0,
                                                             ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                                             TransactionDefinitionId = 1,
                                                             ChequeNo = string.Empty,
                                                             Reference = "PURCHASE ACCOUNT",
                                                             Remark = "GOOD RECEIVED NOTE",
                                                             DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                             IsTStatus = isTStatus,
                                                             GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                             CreatedUser = invPurchaseHeader.CreatedUser,
                                                             CreatedDate = invPurchaseHeader.CreatedDate,
                                                             ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                             ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                             DataTransfer = invPurchaseHeader.DataTransfer
                                                         }).ToList();


                    CommonService.BulkInsert(Common.LINQToDataTable(purchaseAccountEntrySaveQuery), "AccGlTransactionDetail");
                }


                #endregion

                #region Payment Entries

                AccPaymentService accPaymentService = new Service.AccPaymentService();
                AccPaymentHeader accPaymentHeader = new AccPaymentHeader();
                accPaymentHeader = accPaymentService.getAccPaymentHeader(invPurchaseHeader.InvPurchaseHeaderID, invPurchaseHeader.DocumentID, invPurchaseHeader.LocationID);
                if (accPaymentHeader == null)
                    accPaymentHeader = new AccPaymentHeader();

                accPaymentHeader.Amount = invPurchaseHeader.NetAmount;
                accPaymentHeader.BalanceAmount = invPurchaseHeader.NetAmount - paidAmount;
                accPaymentHeader.CompanyID = invPurchaseHeader.CompanyID;
                accPaymentHeader.CostCentreID = invPurchaseHeader.CostCentreID;
                accPaymentHeader.CreatedDate = invPurchaseHeader.CreatedDate;
                accPaymentHeader.CreatedUser = invPurchaseHeader.CreatedUser;
                accPaymentHeader.DocumentDate = invPurchaseHeader.DocumentDate;
                accPaymentHeader.DocumentID = invPurchaseHeader.CompanyID;
                accPaymentHeader.DocumentStatus = invPurchaseHeader.DocumentStatus;
                accPaymentHeader.DocumentNo = invPurchaseHeader.DocumentNo;
                accPaymentHeader.GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID;
                accPaymentHeader.DataTransfer = invPurchaseHeader.DataTransfer;
                accPaymentHeader.IsUpLoad = false;
                accPaymentHeader.LocationID = invPurchaseHeader.LocationID;
                accPaymentHeader.ModifiedDate = invPurchaseHeader.ModifiedDate;
                accPaymentHeader.ModifiedUser = invPurchaseHeader.ModifiedUser;
                accPaymentHeader.PaymentDate = invPurchaseHeader.DocumentDate;
                accPaymentHeader.Reference = "GOOD RECEIVED NOTE PAYMENT";
                accPaymentHeader.ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID;
                accPaymentHeader.ReferenceID = invPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceLocationID = invPurchaseHeader.LocationID;
                accPaymentHeader.Remark = "GOOD RECEIVED NOTE PAYMENT";

                if (accPaymentHeader.AccPaymentHeaderID.Equals(0))
                    context.AccPaymentHeaders.Add(accPaymentHeader);
                else
                    context.Entry(accPaymentHeader).State = EntityState.Modified;
                context.SaveChanges();

                context.Set<AccPaymentDetail>().Delete(context.AccPaymentDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

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
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@InvPurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@InvPurchaseOrderHeaderID", Value=invPurchaseHeader.ReferenceDocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invPurchaseHeader.DocumentStatus},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@poMandatory", Value=poMandatory}
                };

                if (CommonService.ExecuteStoredProcedure("spInvGoodReceivedNoteSave", parameter))
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

        public bool SavePurchaseReturn(InvPurchaseHeader invPurchaseHeader, List<InvPurchaseDetailTemp> invPurchaseDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, string formName = "", bool isTStatus = false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                AccTransactionTypeService accTransactionTypeDetailService = new Service.AccTransactionTypeService();

                #region Purchase Header Save

                if (invPurchaseHeader.DocumentStatus.Equals(1))
                {
                    InvPurchaseService invPurchaseServices = new InvPurchaseService();
                    LocationService locationService = new LocationService();
                    invPurchaseHeader.DocumentNo = invPurchaseServices.GetDocumentNo(formName, invPurchaseHeader.LocationID, locationService.GetLocationsByID(invPurchaseHeader.LocationID).LocationCode, invPurchaseHeader.DocumentID, false).Trim();
                }

                newDocumentNo = invPurchaseHeader.DocumentNo;
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (invPurchaseHeader.InvPurchaseHeaderID.Equals(0)) { context.InvPurchaseHeaders.Add(invPurchaseHeader); }
                else { context.Entry(invPurchaseHeader).State = EntityState.Modified; }

                context.SaveChanges();

                #endregion

                #region Purchase Detail Save

                context.Set<InvPurchaseDetail>().Delete(context.InvPurchaseDetails.Where(p => p.InvPurchaseHeaderID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.DocumentID.Equals(invPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var invPurchaseDetailSaveQuery = (from pd in invPurchaseDetailTemp
                                                  select new
                                                  {
                                                      InvPurchaseDetailID = 0,
                                                      PurchaseDetailID = 0,
                                                      InvPurchaseHeaderID = invPurchaseHeader.InvPurchaseHeaderID,
                                                      CompanyID = invPurchaseHeader.CompanyID,
                                                      LocationID = invPurchaseHeader.LocationID,
                                                      CostCentreID = invPurchaseHeader.CostCentreID,
                                                      DocumentID = invPurchaseHeader.DocumentID,
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
                                                      DocumentStatus = invPurchaseHeader.DocumentStatus,
                                                      BatchNo = (pd.IsBatch == true ? pd.BatchNo : string.Empty),
                                                      IsBatch = pd.IsBatch,
                                                      GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                                                      CreatedUser = invPurchaseHeader.CreatedUser,
                                                      CreatedDate = invPurchaseHeader.CreatedDate,
                                                      ModifiedUser = invPurchaseHeader.ModifiedUser,
                                                      ModifiedDate = invPurchaseHeader.ModifiedDate,
                                                      DataTransfer = invPurchaseHeader.DataTransfer
                                                  }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(invPurchaseDetailSaveQuery), "InvPurchaseDetail");

                #endregion

                #region Serial No Detail Save

                //context.Set<InvProductSerialNoDetail>().Delete(context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invPurchaseHeader.InvPurchaseHeaderID) && p.LocationID.Equals(invPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).AsQueryable());
                //context.SaveChanges();

                //if (invPurchaseHeader.DocumentStatus.Equals(1))
                //{
                //    var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                //                                       select new
                //                                       {
                //                                           InvProductSerialNoID = 0,
                //                                           ProductSerialNoID = 0,
                //                                           GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                //                                           CompanyID = invPurchaseHeader.CompanyID,
                //                                           LocationID = invPurchaseHeader.LocationID,
                //                                           CostCentreID = invPurchaseHeader.CostCentreID,
                //                                           ProductID = pd.ProductID,
                //                                           BatchNo = invPurchaseHeader.DocumentNo,
                //                                           UnitOfMeasureID = pd.UnitOfMeasureID,
                //                                           ExpiryDate = pd.ExpiryDate,
                //                                           SerialNo = pd.SerialNo,
                //                                           SerialNoStatus = invPurchaseHeader.DocumentID,
                //                                           DocumentStatus = invPurchaseHeader.DocumentStatus,
                //                                           CreatedUser = invPurchaseHeader.CreatedUser,
                //                                           CreatedDate = invPurchaseHeader.CreatedDate,
                //                                           ModifiedUser = invPurchaseHeader.ModifiedUser,
                //                                           ModifiedDate = invPurchaseHeader.ModifiedDate,
                //                                           DataTransfer = invPurchaseHeader.DataTransfer
                //                                       }).ToList();

                //    CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");
                //}
                //else
                //{
                //    var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                //                                             select new
                //                                             {
                //                                                 InvProductSerialNoDetailID = 0,
                //                                                 ProductSerialNoDetailID = 0,
                //                                                 GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
                //                                                 CompanyID = invPurchaseHeader.CompanyID,
                //                                                 LocationID = invPurchaseHeader.LocationID,
                //                                                 CostCentreID = invPurchaseHeader.CostCentreID,
                //                                                 LineNo = pd.LineNo,
                //                                                 ProductID = pd.ProductID,
                //                                                 UnitOfMeasureID = pd.UnitOfMeasureID,
                //                                                 ExpiryDate = pd.ExpiryDate,
                //                                                 ReferenceDocumentDocumentID = invPurchaseHeader.DocumentID,
                //                                                 ReferenceDocumentID = invPurchaseHeader.InvPurchaseHeaderID,
                //                                                 SerialNo = pd.SerialNo,
                //                                                 DocumentStatus = invPurchaseHeader.DocumentStatus,
                //                                                 BatchNo = invPurchaseHeader.DocumentNo,
                //                                                 CreatedUser = invPurchaseHeader.CreatedUser,
                //                                                 CreatedDate = invPurchaseHeader.CreatedDate,
                //                                                 ModifiedUser = invPurchaseHeader.ModifiedUser,
                //                                                 ModifiedDate = invPurchaseHeader.ModifiedDate,
                //                                                 DataTransfer = invPurchaseHeader.DataTransfer
                //                                             }).ToList();

                //    CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");
                //}

                #endregion

                #region Process

                var parameter = new DbParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@InvPurchaseHeaderID", Value=invPurchaseHeader.InvPurchaseHeaderID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invPurchaseHeader.DocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invPurchaseHeader.LocationID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invPurchaseHeader.DocumentStatus},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentDocumentID", Value=invPurchaseHeader.ReferenceDocumentDocumentID},
                    new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentID", Value=invPurchaseHeader.ReferenceDocumentID},

                };

                if (CommonService.ExecuteStoredProcedure("spInvPurchaseReturnNoteSave", parameter))
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


        public List<InvProductSerialNoTemp> getSerialNoDetailForTOG(InvTransferNoteHeader transferNoteHeader)
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

            invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(transferNoteHeader.InvTransferNoteHeaderID) && ps.ReferenceDocumentDocumentID.Equals(transferNoteHeader.DocumentID)).ToList();
            foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
            {
                InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;

                invProductSerialNoTempList.Add(invProductSerialNoTemp);
            }

            return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public string[] GetAllSavedDocumentNumbers(int documentID,int locationID)
        {
            List<string> GoodReceivedNoteList = context.InvPurchaseHeaders.Where(c => c.DocumentID == documentID && c.DocumentStatus == 1 && c.LocationID == locationID).Select(c => c.DocumentNo).ToList();
            return GoodReceivedNoteList.ToArray();
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvPurchaseHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersAsReference(int documentID, int locationID)
        {
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.InvPurchaseHeaders
                       join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
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


        public List<InvBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo,int locationID,int documentID)
        {
            List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();

           
              var  barcodeDetailTemp = (from pm in context.InvProductMasters
                                     join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                     join pd in context.InvPurchaseDetails on psm.ProductID equals pd.ProductID
                                     join ph in context.InvPurchaseHeaders on pd.InvPurchaseHeaderID equals ph.PurchaseHeaderID
                                     join um in context.UnitOfMeasures on psm.UnitOfMeasureID equals um.UnitOfMeasureID
                                     join sc in context.Suppliers on ph.SupplierID equals sc.SupplierID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     && pd.LocationID == ph.LocationID
                                     && pd.BatchNo == psm.BatchNo
                                     && psm.LocationID == locationID
                                     //&& ph.PurchaseHeaderID == psm.ReferenceDocumentID
                                    // && ph.DocumentID == psm.ReferenceDocumentDocumentID
                                     && ph.DocumentNo == documentNo
                                     && ph.DocumentID == documentID
                                      
                                     select new
                                     {
                                         pm.InvProductMasterID,
                                         pm.ProductCode,
                                         pm.ProductName,
                                         psm.BatchNo,
                                         psm.BarCode,
                                         Qty = pd.Qty,
                                         Stock = psm.BalanceQty,
                                         psm.CostPrice,
                                         psm.SellingPrice,
                                         UnitOfMeasureID = um.UnitOfMeasureID,
                                         UnitOfMeasureName = um.UnitOfMeasureName,
                                         pd.LineNo,
                                         ph.DocumentDate,
                                         sc.SupplierCode,
                                         pd.ExpiryDate
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
                    //invBarcodeDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invBarcodeDetailTempList.Add(invBarcodeDetailTemp);               

            }


            return invBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        /// Get purchase details for report generator
        /// </summary>
        /// <returns></returns>
        public DataTable GetPurchaseDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.InvPurchaseHeaders.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +  
                                " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + 
                                " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }

                 }
            }

            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");

            var result = (from h in query
                               join s in context.Suppliers on h.SupplierID equals s.SupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               //join po in context.InvPurchaseOrderHeaders on h.ReferenceDocumentID equals po.InvPurchaseOrderHeaderID
                               where h.ReferenceDocumentDocumentID.Equals(autoGenerateInfoPo.DocumentID)
                               select
                                   new
                                   {
                                       FieldString1 = l.LocationCode + " " + l.LocationName,
                                       FieldString2 = h.DocumentNo,
                                       FieldString3 = h.DocumentDate,
                                       FieldString4 = s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName,
                                       FieldString5 = h.SupplierInvoiceNo,
                                       FieldString6 = h.ReferenceNo,
                                       FieldString7 = "", //po.DocumentNo, // Purchase Order No
                                       FieldString8 = s.TaxNo1 != "" ? "B" : s.TaxNo2 != "" ? "B" : s.TaxNo3 != "" ? "B" : "P",
                                       FieldString9 = h.CreatedUser,
                                       FieldDecimal1 = (from t in context.InvPurchaseDetails
                                                                where
                                                                    t.InvPurchaseHeaderID == h.InvPurchaseHeaderID
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

                dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString().Trim().Replace("@p__linq__0", autoGenerateInfoPo.DocumentID.ToString().Trim())); // replace value "@p__linq__0" in the SQL query. (Because when converting linq query to a sql query, it converts paramerters as '@p__linq__0'.) 

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

        public DataTable GetPurchaseRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
                autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
                int documentID = autoGeneratePurchaseInfo.DocumentID;

                AutoGenerateInfo autoGenerateInfoPch = new AutoGenerateInfo();
                autoGenerateInfoPch = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");
                AutoGenerateInfo autoGenerateInfoPchRet = new AutoGenerateInfo();
                autoGenerateInfoPchRet = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseReturnNote");

                var query = context.InvPurchase.AsNoTracking().Where("DocumentStatus= @0 ", 1);

                // Insert Conditions to query
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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

                            case "DocumentID":
                                int selectedFromStatus = 0, selectedToStatus = 0;

                                if (reportConditionsDataStruct.ConditionFrom == "Purchase")
                                {
                                    selectedFromStatus = autoGenerateInfoPch.DocumentID;
                                    selectedToStatus = autoGenerateInfoPch.DocumentID;
                                }
                                else
                                {
                                    selectedToStatus = autoGenerateInfoPchRet.DocumentID;
                                    selectedFromStatus = autoGenerateInfoPchRet.DocumentID;
                                }

                                //if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(autoGenerateInfoPch.DocumentID.ToString()))
                                //{ selectedFromStatus = autoGenerateInfoPch.DocumentID; }
                                //if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(autoGenerateInfoPchRet.DocumentID.ToString()))
                                //{ selectedFromStatus = autoGenerateInfoPchRet.DocumentID; }

                                //if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(autoGenerateInfoPch.DocumentID.ToString()))
                                //{ selectedToStatus = autoGenerateInfoPch.DocumentID; }
                                //if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(autoGenerateInfoPchRet.DocumentID.ToString()))
                                //{ selectedToStatus = autoGenerateInfoPchRet.DocumentID; }

                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));

                                break;
                            default:
                                break;
                        }
                    }
                }

                var result = (from ph in query
                         join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                         //join l in context.Locations on ph.LocationID equals l.LocationID
                         join po in context.InvPurchaseHeaders on ph.DocumentNo equals po.DocumentNo
                         //join st in context.InvProductStockMasters on ph.ProductID equals st.ProductID
                         //where st.LocationID == ph.LocationID
                         select new
                         {
                             FieldString1 = ph.ProductCode,
                             FieldString2 = ph.ProductName,
                             FieldString3 = ph.SupplierCode + " " + ph.SupplierName + " (" + s.Remark.Trim() + ")",
                             FieldString4 = ph.DepartmentCode + " " + ph.DepartmentName,
                             FieldString5 = ph.CategoryCode + " " + ph.CategoryName,
                             FieldString6 = ph.SubCategoryCode + " " + ph.SubCategoryName,
                             FieldString7 = ph.SubCategory2Code + " " + ph.SubCategory2Name,
                             FieldString8 = (ph.DocumentID == autoGenerateInfoPch.DocumentID ? "Purchase" : (ph.DocumentID == autoGenerateInfoPchRet.DocumentID ? "Sup. Return" : "")),
                             FieldString9 = ph.DocumentNo,
                             FieldString10 = ph.DocumentDate,
                             FieldString11 = ph.LocationCode + " " + ph.LocationName,
                             FieldDecimal1 = ph.CostPrice,
                             FieldDecimal2 = (ph.DocumentID == autoGenerateInfoPch.DocumentID ? ph.Qty : (ph.DocumentID == autoGenerateInfoPchRet.DocumentID ? -(ph.Qty) : 0)),
                             FieldDecimal3 = (ph.DocumentID == autoGenerateInfoPch.DocumentID ? ph.GrossAmount : (ph.DocumentID == autoGenerateInfoPchRet.DocumentID ? -(ph.GrossAmount) : 0)),
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

                    dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString().Trim()
                        .Replace("@p__linq__0", autoGenerateInfoPch.DocumentID.ToString().Trim())
                        .Replace("@p__linq__1", autoGenerateInfoPchRet.DocumentID.ToString().Trim())
                        .Replace("@p__linq__2", autoGenerateInfoPch.DocumentID.ToString().Trim())
                        .Replace("@p__linq__3", autoGenerateInfoPchRet.DocumentID.ToString().Trim())
                        .Replace("@p__linq__4", autoGenerateInfoPch.DocumentID.ToString().Trim())
                        .Replace("@p__linq__5", autoGenerateInfoPchRet.DocumentID.ToString().Trim())); // replace values "@p__linq__2, @p__linq__3, @p__linq__4, @p__linq__5" in the SQL query.

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
        }

        public DataTable GetPurchaseReturnRegistryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseReturnNote");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            var query = context.InvPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);

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
                               join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                               join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                               //join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                               join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                               //join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                               join s in context.Suppliers on ph.SupplierID equals s.SupplierID
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
        public DataTable GetPurchaseReturnDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.InvPurchaseHeaders.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }

                }
            }

            AutoGenerateInfo autoGenerateInfoGRn = new AutoGenerateInfo();
            autoGenerateInfoGRn = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");

            var result = (from h in query
                               join s in context.Suppliers on h.SupplierID equals s.SupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               join h2 in context.InvPurchaseHeaders on new { h.ReferenceDocumentID, DocumentID = autoGenerateInfoGRn.DocumentID }
                               equals new { ReferenceDocumentID = h2.InvPurchaseHeaderID, h2.DocumentID } into h2_join
                               from h2 in h2_join.DefaultIfEmpty()
                               where h.DocumentID == autoGenerateInfo.DocumentID
                               select
                                    new
                                    {
                                        FieldString1 = l.LocationCode + " " + l.LocationName,
                                        FieldString2 = h.DocumentNo,
                                        FieldString3 = h.DocumentDate,
                                        FieldString4 = s.SupplierCode + " (" + s.Remark.Trim() + ")" + " " + s.SupplierName,
                                        FieldString5 = h2.SupplierInvoiceNo,
                                        FieldString6 = h.ReferenceNo,
                                        FieldString7 = h2.DocumentNo, // GRN No
                                        FieldString8 = s.TaxNo1 != "" ? "B" : s.TaxNo2 != "" ? "B" : s.TaxNo3 != "" ? "B" : "P",
                                        FieldString9 = h.CreatedUser,
                                        FieldDecimal1 = (from t in context.InvPurchaseDetails
                                                            where
                                                                t.InvPurchaseHeaderID == h.InvPurchaseHeaderID
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

                dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString()
                                             .Replace("@p__linq__0", autoGenerateInfoGRn.DocumentID.ToString().Trim())
                                             .Replace("@p__linq__1", autoGenerateInfo.DocumentID.ToString().Trim())
                                             );

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

        public DataSet GetPurchaseTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");

            var query = (
                           from ph in context.InvPurchaseHeaders
                           join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                           join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                           join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                           join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           join po in context.InvPurchaseOrderHeaders on ph.ReferenceDocumentID equals po.InvPurchaseOrderHeaderID
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                                && po.DocumentID.Equals(autoGenerateInfo.DocumentID)
                           select
                               new
                               {                                   
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = ph.Remark,
                                   FieldString4 = s.SupplierCode + "  " + s.SupplierName,
                                   FieldString5 = po.DocumentNo, 
                                   FieldString6 = "",//ph.ValidityPeriod,
                                   FieldString7 = "",
                                   FieldString8 = "",
                                   FieldString9 = ph.ReferenceNo,
                                   FieldString10 = py.PaymentMethodName,
                                   FieldString11 = ph.SupplierInvoiceNo,
                                   FieldString12 = l.LocationCode + "  " + l.LocationName,
                                   FieldString13 = pm.ProductCode,
                                   FieldString14 = pm.ProductName,
                                   FieldString15 = um.UnitOfMeasureName,
                                   FieldString16 = pd.CurrentQty, //pd.PackSize,  xxxx
                                   FieldString17 = pd.Qty,
                                   FieldString18 = pd.FreeQty,  //xxxx
                                   FieldString19 = pd.CostPrice,
                                   FieldString20 = pd.Qty-pd.FreeQty, // pd.DiscountPercentage,
                                   FieldString21 = pd.DiscountAmount,
                                   FieldString22 = pd.NetAmount,
                                   FieldString23 = ph.GrossAmount,
                                   FieldString24 = ph.DiscountPercentage,
                                   FieldString25 = ph.DiscountAmount,
                                   FieldString26 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2,
                                   FieldString27 = ph.TaxAmount3,
                                   FieldString28 = ph.OtherChargers,
                                   FieldString29 = ph.NetAmount,
                                   FieldString30 = ph.CreatedUser,
                                   FieldString31 = s.Remark
                               }).ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(query);

            var purchaseOrderHeaderID = context.InvPurchaseHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus) 
                    && h.DocumentID.Equals(documentid)).FirstOrDefault().ReferenceDocumentID;

            if (context.InvPurchaseOrderDetails.Any(d => d.BalanceQty > 0 && d.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            { 
                var queryBal = (
                                        from ph in context.InvPurchaseOrderHeaders
                                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                                        join l in context.Locations on ph.LocationID equals l.LocationID
                                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                                        where ph.DocumentStatus.Equals(documentStatus) && ph.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID)
                                        select
                                            new
                                            {
                                                FieldString1 = ph.DocumentNo,
                                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                                FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                                FieldString4 = ph.Remark,
                                                FieldString5 = documentNo, // GRN No
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
                                                FieldDecimal1 = pd.OrderQty,
                                                FieldDecimal2 = pd.BalanceQty,
                                                FieldDecimal3 = pd.FreeQty,
                                                FieldDecimal4 = pd.CostPrice,
                                                FieldDecimal5 = pd.DiscountPercentage,
                                                FieldDecimal6 = (pd.DiscountAmount/ pd.OrderQty) * pd.BalanceQty, // Discount Amount
                                                FieldDecimal7 = (pd.BalanceQty * pd.CostPrice) - ((pd.DiscountAmount / pd.OrderQty) * pd.BalanceQty), //pd.NetAmount,
                                                FieldString23 = ph.GrossAmount,
                                                FieldString24 = ph.DiscountPercentage,
                                                FieldString25 = ph.DiscountAmount,
                                                FieldString26 = ph.TaxAmount3,
                                                FieldString27 = ph.OtherCharges,
                                                FieldString28 = ph.NetAmount,
                                                FieldString29 = ph.CreatedUser,
                                                FieldString30 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2,
                                                FieldString31 = s.Remark

                                            }).ToDataTable();

                dsTransactionData.Tables.Add(queryBal);
            }


            //return query.AsEnumerable().ToDataTable();

            return dsTransactionData;
        }

        public DataSet GetPurchaseTransactionDataTableGrnTemp(string documentNo, int documentStatus, int documentid, string supplier, string supplierRemark, string paymentMethod, string poDocumentNo)
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseOrder");

            var query = (
                           from ph in context.InvPurchaseHeaders
                           join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                           join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                           //join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           //join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           //join po in context.InvPurchaseOrderHeaders on ph.ReferenceDocumentID equals po.InvPurchaseOrderHeaderID
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                           select
                               new
                               {
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = ph.Remark,
                                   FieldString4 = supplier,
                                   FieldString5 = poDocumentNo,
                                   FieldString6 = "",//ph.ValidityPeriod,
                                   FieldString7 = "",
                                   FieldString8 = "",
                                   FieldString9 = ph.ReferenceNo,
                                   FieldString10 = paymentMethod,
                                   FieldString11 = ph.SupplierInvoiceNo,
                                   FieldString12 = l.LocationCode + "  " + l.LocationName,
                                   FieldString13 = pm.ProductCode,
                                   FieldString14 = pm.ProductName,
                                   FieldString15 = "Kg",
                                   FieldString16 = pd.Qty - (pd.FreeQty + ((pd.Qty * pd.DiscountPercentage) / 100)), //pd.PackSize, // thara
                                   FieldString17 = pd.Qty,
                                   FieldString18 = pd.FreeQty,  // wavg
                                   FieldString19 = pd.CostPrice,
                                   FieldString20 = pd.DiscountPercentage,
                                   FieldString21 = pd.DiscountAmount,
                                   FieldString22 = pd.NetAmount,
                                   FieldString23 = ph.GrossAmount,
                                   FieldString24 = ph.DiscountPercentage,
                                   FieldString25 = ph.DiscountAmount,
                                   FieldString26 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2,
                                   FieldString27 = ph.TaxAmount3,
                                   FieldString28 = ph.OtherChargers,
                                   FieldString29 = ph.NetAmount,
                                   FieldString30 = ph.CreatedUser,
                                   FieldString31 = supplierRemark
                               }).ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(query);

            var purchaseOrderHeaderID = context.InvPurchaseHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus)
                    && h.DocumentID.Equals(documentid)).FirstOrDefault().ReferenceDocumentID;

            if (context.InvPurchaseOrderDetails.Any(d => d.BalanceQty > 0 && d.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            {
                var queryBal = (
                                        from ph in context.InvPurchaseOrderHeaders
                                        join pd in context.InvPurchaseOrderDetails on ph.InvPurchaseOrderHeaderID equals pd.InvPurchaseOrderHeaderID
                                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                                        join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                                        join l in context.Locations on ph.LocationID equals l.LocationID
                                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                                        where ph.DocumentStatus.Equals(documentStatus) && ph.InvPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID)
                                        select
                                            new
                                            {
                                                FieldString1 = ph.DocumentNo,
                                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                                FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                                FieldString4 = ph.Remark,
                                                FieldString5 = documentNo, // GRN No
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
                                                FieldDecimal1 = pd.OrderQty,
                                                FieldDecimal2 = pd.BalanceQty,
                                                FieldDecimal3 = pd.FreeQty,
                                                FieldDecimal4 = pd.CostPrice,
                                                FieldDecimal5 = pd.DiscountPercentage,
                                                FieldDecimal6 = (pd.DiscountAmount / pd.OrderQty) * pd.BalanceQty, // Discount Amount
                                                FieldDecimal7 = (pd.BalanceQty * pd.CostPrice) - ((pd.DiscountAmount / pd.OrderQty) * pd.BalanceQty), //pd.NetAmount,
                                                FieldString23 = ph.GrossAmount,
                                                FieldString24 = ph.DiscountPercentage,
                                                FieldString25 = ph.DiscountAmount,
                                                FieldString26 = ph.TaxAmount3,
                                                FieldString27 = ph.OtherCharges,
                                                FieldString28 = ph.NetAmount,
                                                FieldString29 = ph.CreatedUser,
                                                FieldString30 = ph.TaxAmount1 != 0 ? ph.TaxAmount1 : ph.TaxAmount2,
                                                FieldString31 = s.Remark
                                            }).ToDataTable();

                dsTransactionData.Tables.Add(queryBal);
            }


            //return query.AsEnumerable().ToDataTable();

            return dsTransactionData;
        }

        public List<InvPurchaseDetailTemp> GetSavedGrnDetailToTOG(InvPurchaseHeader invPurchaseHeader, int documentID)
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp;
            List<InvPurchaseDetailTemp> invPurchaseDetailTempList = new List<InvPurchaseDetailTemp>();

            var purchaseDetailTemp = (from pm in context.InvProductMasters
                                      join pd in context.InvPurchaseDetails on pm.InvProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.InvPurchaseHeaders on pd.InvPurchaseHeaderID equals ph.InvPurchaseHeaderID
                                      where pd.LocationID.Equals(invPurchaseHeader.LocationID) && pd.CompanyID.Equals(invPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(invPurchaseHeader.DocumentID) && pd.InvPurchaseHeaderID.Equals(invPurchaseHeader.PurchaseHeaderID)
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
                invPurchaseDetailTemp = new InvPurchaseDetailTemp();

                invPurchaseDetailTemp.AverageCost = tempProduct.AvgCost;
                invPurchaseDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invPurchaseDetailTemp.CostPrice = tempProduct.CostPrice;
                invPurchaseDetailTemp.CurrentQty = tempProduct.CurrentQty;
                invPurchaseDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                invPurchaseDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                invPurchaseDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invPurchaseDetailTemp.FreeQty = tempProduct.FreeQty;
                invPurchaseDetailTemp.GrossAmount = tempProduct.GrossAmount;
                invPurchaseDetailTemp.LineNo = tempProduct.LineNo;
                invPurchaseDetailTemp.LocationID = tempProduct.LocationID;
                invPurchaseDetailTemp.NetAmount = tempProduct.NetAmount;
                invPurchaseDetailTemp.OrderQty = tempProduct.OrderQty;
                invPurchaseDetailTemp.ProductCode = tempProduct.ProductCode;
                invPurchaseDetailTemp.ProductID = tempProduct.ProductID;
                invPurchaseDetailTemp.ProductName = tempProduct.ProductName;
                invPurchaseDetailTemp.Qty = tempProduct.BalanceQty;
                invPurchaseDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invPurchaseDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invPurchaseDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invPurchaseDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invPurchaseDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invPurchaseDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invPurchaseDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invPurchaseDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invPurchaseDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invPurchaseDetailTemp.BatchNo = tempProduct.BatchNo;

                invPurchaseDetailTempList.Add(invPurchaseDetailTemp);
            }
            return invPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetPurchaseReturnTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            var query = (
                           from ph in context.InvPurchaseHeaders
                           join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                           join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                           join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           join s in context.Suppliers on ph.SupplierID equals s.SupplierID
                           join re in context.InvReturnTypes on ph.ReturnTypeID equals re.InvReturnTypeID
                           //join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                           select
                               new
                               {
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = s.SupplierCode + "  " + s.SupplierName,
                                   FieldString4 = ph.Remark,
                                   FieldString5 = re.ReturnType,
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
                                   FieldString26 = ph.TaxAmount,
                                   FieldString27 = ph.OtherChargers,
                                   FieldString28 = ph.NetAmount,
                                   FieldString29 = "",
                                   FieldString30 = ph.CreatedUser,
                                   FieldString31 = s.Remark
                               });            
            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            IQueryable qryResult = null;
            if (string.Equals(autoGenerateInfo.FormName, "RptPurchaseRegister"))
            {
                qryResult = context.InvPurchase.Where("DocumentStatus == @0", 1);
            }
            else
            {
                qryResult = context.InvPurchaseHeaders.Where("DocumentStatus == @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);
            }

            //DataTable dtt = qryResult.to
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "SupplierID":
                    qryResult = null;
                    qryResult = context.InvPurchaseHeaders.Where("DocumentStatus == @0", 1);
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
                    autoGenerateInfoPch = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGoodsReceivedNote");

                    AutoGenerateInfo autoGenerateInfoPchRet = new AutoGenerateInfo();
                    autoGenerateInfoPchRet = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPurchaseReturnNote");

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
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
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

        //public ArrayList GetSelectionPurchaseData(Common.ReportDataStruct reportDataStruct)
        //{
        //    IQueryable qryResult = context.InvPurchase.Where("DocumentStatus == @0", 1);

        //    switch (reportDataStruct.DbColumnName.Trim())
        //    {
        //        case "SupplierID":
        //            qryResult = null;
        //            qryResult = context.InvPurchaseHeaders.Where("DocumentStatus == @0", 1);
        //            qryResult = qryResult.Join(context.Suppliers, reportDataStruct.DbColumnName.Trim(),
        //                                       reportDataStruct.DbColumnName.Trim(),
        //                                       "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
        //                                 .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
        //                                          "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
        //                                 .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
        //                                 .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
        //            break;
        //        case "LocationID":
        //            qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
        //                                       reportDataStruct.DbColumnName.Trim(),
        //                                       "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
        //                                 .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
        //                                          "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
        //                                 .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
        //                                 .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
        //            break;
        //        default:
        //            qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")",
        //                                          "new(" + reportDataStruct.DbColumnName.Trim() + ")")
        //                                  .OrderBy("Key." + reportDataStruct.DbColumnName.Trim() + "")
        //                                  .Select("Key." + reportDataStruct.DbColumnName.Trim() + "");
        //            break;
        //    }

        //    // change this code to get rid of the foreach loop
        //    ArrayList selectionDataList = new ArrayList();
        //    if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
        //    {
        //        foreach (var item in qryResult)
        //        {
        //            if (item.Equals(true))
        //            { selectionDataList.Add("Yes"); }
        //            else
        //            { selectionDataList.Add("No"); }
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in qryResult)
        //        {
        //            if (!string.IsNullOrEmpty(item.ToString()))
        //            { selectionDataList.Add(item.ToString()); }
        //        }
        //    }
        //    return selectionDataList;
        //}        

        public DataTable GetProductPurchaseDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList)
        {

            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvPurchaseHeaders.Where("DocumentStatus=1");

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
                               join pd in context.InvPurchaseDetails on ph.InvPurchaseHeaderID equals pd.InvPurchaseHeaderID
                               join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                               join um in context.UnitOfMeasures on pm.UnitOfMeasureID equals um.UnitOfMeasureID
                               join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                               join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                               join s in context.Suppliers on ph.SupplierID equals s.SupplierID
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
            var query = context.InvPurchase.Where("DocumentStatus=1");

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
                               join pd in context.InvProductExtendedPropertyValues on ph.ProductID equals pd.ProductID
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
            IQueryable qryResult = context.InvPurchase.Where("DocumentStatus == @0", 1);
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
            var query = context.InvPurchase.Where("DocumentStatus=1");

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
                               join pd in context.InvProductExtendedPropertyValues on ph.ProductID equals pd.ProductID
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

        public InvPurchaseDetailTemp GetPurchaseDetailTempForRecallPO(List<InvPurchaseDetailTemp> InvPurchaseDetailTemp, InvProductMaster invProductMaster, long unitOfMeasureID) 
        {
            InvPurchaseDetailTemp invPurchaseDetailTemp = new InvPurchaseDetailTemp();

            invPurchaseDetailTemp = InvPurchaseDetailTemp.Where(pd => pd.ProductID.Equals(invProductMaster.InvProductMasterID) && pd.UnitOfMeasureID.Equals(unitOfMeasureID)).FirstOrDefault();

            //if (invPurchaseDetailTemp == null)
            //{
            //    invPurchaseDetailTemp = new InvPurchaseDetailTemp();
            //}
            
            return invPurchaseDetailTemp;
        }

        public bool ValidateExistingProduct(bool isCode, string strProduct, string purchaseOrderNo)
        { 
            bool result = false;
            InvPurchaseOrderHeader invPurchaseorderHeader = new InvPurchaseOrderHeader();
            InvPurchaseOrderService invPurchaseOrderService = new InvPurchaseOrderService();
            InvProductMaster invProductMaster = new InvProductMaster();
            InvProductMasterService invProductMasterService = new InvProductMasterService();

            invPurchaseorderHeader = invPurchaseOrderService.GetDocumentDetailsByDocumentNumber(purchaseOrderNo);

            if (invPurchaseorderHeader != null)
            {
                if (isCode)
                {
                    invProductMaster = invProductMasterService.GetProductsByCode(strProduct);
                }
                else
                {
                    invProductMaster = invProductMasterService.GetProductsByName(strProduct);
                }

                if (invProductMaster != null)
                {
                    var purchaseDetailTemp = (from pm in context.InvProductMasters
                                              join pod in context.InvPurchaseOrderDetails on pm.InvProductMasterID equals pod.ProductID
                                              join poh in context.InvPurchaseOrderHeaders on pod.InvPurchaseOrderHeaderID equals poh.InvPurchaseOrderHeaderID
                                              where pod.ProductID.Equals(invProductMaster.InvProductMasterID) && poh.InvPurchaseOrderHeaderID.Equals(invPurchaseorderHeader.InvPurchaseOrderHeaderID)
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


        public bool CheckExpiryDates(List<InvPurchaseDetailTemp> invPurchaseDetailTempList) 
        {
            bool rtnValue = true;
            InvProductMaster invProductMaster = new InvProductMaster();
            InvProductMasterService invProductMasterService = new InvProductMasterService();

            List<InvPurchaseDetailTemp> invProductSerialNoTemp = new List<InvPurchaseDetailTemp>();
            invProductSerialNoTemp = invPurchaseDetailTempList.Where(t => t.ExpiryDate == null).ToList();

            foreach (InvPurchaseDetailTemp temp in invProductSerialNoTemp)
            {
                long productID = temp.ProductID;
                invProductMaster = invProductMasterService.GetProductDetailsByID(productID);

                if (invProductMaster != null)
                {
                    if (invProductMaster.IsExpiry)
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

        public string[] GetAllProductCodes()
        {
            //List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();

            //List<string> ProductCodeList = context.InvProductMasters.Join(context.InvProductBatchNoExpiaryDetails ,
            //                                s => new { s.InvProductMasterID, s.UnitOfMeasureID },
            //           h => new { h.ProductID, h.UnitOfMeasureID },
            //           (s, h) => s);

            //return ProductCodeList.ToArray();
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllProductNames()
        {

            //List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();

        }

        public string[] GetAllProductCodesBySupplier(long supplierID)
        {
            //List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();

            //List<string> ProductCodeList = context.InvProductMasters.Join(context.InvProductBatchNoExpiaryDetails ,
            //                                s => new { s.InvProductMasterID, s.UnitOfMeasureID },
            //           h => new { h.ProductID, h.UnitOfMeasureID },
            //           (s, h) => s);

            //return ProductCodeList.ToArray();
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false) && b.SupplierID.Equals(supplierID))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1) ),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllProductNamesBySupplier(long supplierID)
        {

            //List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false) && b.SupplierID.Equals(supplierID))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();

        }
    }

}
