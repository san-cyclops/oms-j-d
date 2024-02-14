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
using System.Transactions;
using System.Collections;
using System.Data.Entity.Core.Objects;
using MoreLinq;
using System.Data.Entity;

namespace Service
{
    public class SampleOutService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public SampleOutDetailTemp getSampleOutDetailTemp(List<SampleOutDetailTemp> SampleOutDetailTemp, LgsProductMaster lgsProductMaster, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID, bool isInv) 
        {
            if (isInv)
            {
                SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();
                decimal defaultConvertFactor = 1;
                var sampleDetailTemp = (from pm in context.InvProductMasters
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
                    sampleDetailTemp = (dynamic)null;

                    sampleDetailTemp = (from pm in context.InvProductMasters
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

                foreach (var tempProduct in sampleDetailTemp)
                {
                    if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                    if (sampleOutDetailTemp == null)
                    {
                        sampleOutDetailTemp = new SampleOutDetailTemp();
                        sampleOutDetailTemp.ProductID = tempProduct.InvProductMasterID;
                        sampleOutDetailTemp.ProductCode = tempProduct.ProductCode;
                        sampleOutDetailTemp.ProductName = tempProduct.ProductName;
                        sampleOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        sampleOutDetailTemp.CostPrice = tempProduct.CostPrice;
                        sampleOutDetailTemp.AverageCost = tempProduct.AverageCost;
                        sampleOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        sampleOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;

                    }
                }

                return sampleOutDetailTemp;
            }
            else
            {
                SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();
                decimal defaultConvertFactor = 1;
                var sampleDetailTemp = (from pm in context.LgsProductMasters
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
                    sampleDetailTemp = (dynamic)null;

                    sampleDetailTemp = (from pm in context.LgsProductMasters
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

                foreach (var tempProduct in sampleDetailTemp)
                {
                    if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        sampleOutDetailTemp = SampleOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                    if (sampleOutDetailTemp == null)
                    {
                        sampleOutDetailTemp = new SampleOutDetailTemp();
                        sampleOutDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                        sampleOutDetailTemp.ProductCode = tempProduct.ProductCode;
                        sampleOutDetailTemp.ProductName = tempProduct.ProductName;
                        sampleOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        sampleOutDetailTemp.CostPrice = tempProduct.CostPrice;
                        sampleOutDetailTemp.AverageCost = tempProduct.AverageCost;
                        sampleOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        sampleOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;

                    }
                }

                return sampleOutDetailTemp;
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.SampleOutHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersToSampleIn(int locationID) 
        {
            List<string> documentNoList = context.SampleOutHeaders.Where(q => q.LocationID == locationID && q.Balanced.Equals(0)).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<SampleOutDetailTemp> getUpdateSampleOutDetailTemp(List<SampleOutDetailTemp> sampleOutTempDetail, SampleOutDetailTemp sampleOutDetail)//Product Master Removed from parameters
        {
            SampleOutDetailTemp sampleOutDetailTemp = new SampleOutDetailTemp();

            sampleOutDetailTemp = sampleOutTempDetail.Where(p => p.ProductID == sampleOutDetail.ProductID && p.UnitOfMeasureID == sampleOutDetail.UnitOfMeasureID).FirstOrDefault();

            if (sampleOutDetailTemp == null || sampleOutDetailTemp.LineNo.Equals(0))
            {
                sampleOutDetail.LineNo = sampleOutTempDetail.Count + 1;
            }
            else
            {
                sampleOutTempDetail.Remove(sampleOutDetailTemp);
                sampleOutDetail.LineNo = sampleOutDetailTemp.LineNo;
            }

            sampleOutTempDetail.Add(sampleOutDetail);

            return sampleOutTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public SampleOutHeader GetPausedSampleOutHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.SampleOutHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public SampleOutHeader GetSampleOutHeaderToSampleIn(string documentNo) 
        {
            return context.SampleOutHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<SampleOutDetailTemp> DeleteProductSampleOutDetailTemp(List<SampleOutDetailTemp> sampleOutDetailsTemp, string productCode) 
        {
            sampleOutDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return sampleOutDetailsTemp;
        }

        public List<SampleOutDetail> GetAllSampleOutDetail()
        {
            return context.SampleOutDetails.ToList();
        }

        public List<InvProductBatchNoTemp> GetInvBatchNoDetail(InvProductMaster invProductMaster, int locationID)
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


        public List<LgsProductBatchNoTemp> GetLgsBatchNoDetail(LgsProductMaster lgsProductMaster, int locationID)
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


        public bool Save(SampleOutHeader sampleOutHeader, List<SampleOutDetailTemp> sampleOutDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, bool isInv) 
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (sampleOutHeader.SampleOutHeaderID.Equals(0))
                    context.SampleOutHeaders.Add(sampleOutHeader);
                else
                    context.Entry(sampleOutHeader).State = EntityState.Modified;

                context.SaveChanges();

                List<SampleOutDetail> sampleOutDetailDelete = new List<SampleOutDetail>();
                sampleOutDetailDelete = context.SampleOutDetails.Where(p => p.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID) && p.LocationID.Equals(sampleOutHeader.LocationID) && p.DocumentID.Equals(sampleOutHeader.DocumentID)).ToList();

                foreach (SampleOutDetail sampleOutDetailDeleteTemp in sampleOutDetailDelete)
                {
                    context.SampleOutDetails.Remove(sampleOutDetailDeleteTemp);
                }

                if (isInv)
                {
                    List<InvProductSerialNoDetail> invProductSerialNoDetailDelete = new List<InvProductSerialNoDetail>();
                    invProductSerialNoDetailDelete = context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && p.LocationID.Equals(sampleOutHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID)).ToList();

                    foreach (InvProductSerialNoDetail invProductSerialNoDetailDeleteTemp in invProductSerialNoDetailDelete)
                    {
                        context.InvProductSerialNoDetails.Remove(invProductSerialNoDetailDeleteTemp);
                    }
                    context.SaveChanges();

                    foreach (SampleOutDetailTemp sampleOutDetail in sampleOutDetailTemp)
                    {
                        SampleOutDetail sampleOutDetailSave = new SampleOutDetail();

                        sampleOutDetailSave.CompanyID = sampleOutHeader.CompanyID;
                        sampleOutDetailSave.ConvertFactor = sampleOutDetail.ConvertFactor;
                        sampleOutDetailSave.CostCentreID = sampleOutHeader.CostCentreID;
                        sampleOutDetailSave.CostPrice = sampleOutDetail.CostPrice;
                        sampleOutDetailSave.DocumentID = sampleOutHeader.DocumentID;
                        sampleOutDetailSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                        sampleOutDetailSave.GrossAmount = sampleOutDetail.GrossAmount;
                        sampleOutDetailSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                        sampleOutDetailSave.LineNo = sampleOutDetail.LineNo;
                        sampleOutDetailSave.LocationID = sampleOutHeader.LocationID;
                        sampleOutDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleOutDetailSave.BatchNo = sampleOutDetail.BatchNo;
                        sampleOutDetailSave.ProductID = sampleOutDetail.ProductID;
                        sampleOutDetailSave.SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID;
                        sampleOutDetailSave.OrderQty = sampleOutDetail.OrderQty;
                        sampleOutDetailSave.BalancedQty = sampleOutDetail.OrderQty;
                        sampleOutDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleOutDetailSave.UnitOfMeasureID = sampleOutDetail.UnitOfMeasureID;
                        sampleOutDetailSave.BaseUnitID = sampleOutDetail.BaseUnitID;

                        if (sampleOutHeader.DocumentStatus.Equals(1))
                        {
                            context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(sampleOutHeader.LocationID) && ps.ProductID.Equals(sampleOutDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock - sampleOutDetail.OrderQty });
                            //context.InvProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleOutHeader.LocationID) && bd.ProductID.Equals(sampleOutDetail.ProductID) && bd.BatchNo.Equals(sampleOutDetail.BatchNo), bd => new InvProductBatchNoExpiaryDetail { Qty = bd.Qty - sampleOutDetail.OrderQty });
                            context.InvProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleOutHeader.LocationID) && bd.ProductID.Equals(sampleOutDetail.ProductID) && bd.BatchNo.Equals(sampleOutDetail.BatchNo) && bd.UnitOfMeasureID.Equals(sampleOutDetail.UnitOfMeasureID), bd => new InvProductBatchNoExpiaryDetail { BalanceQty = bd.BalanceQty - sampleOutDetail.OrderQty });
                        }

                        context.SampleOutDetails.Add(sampleOutDetailSave);
                        context.SaveChanges();

                        //context.SampleOutDetails.Update(pd => pd.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID) && pd.LocationID.Equals(sampleOutHeader.LocationID) && pd.DocumentID.Equals(sampleOutHeader.DocumentID) && pd.SampleOutHeaderID.Equals(sampleOutDetailSave.SampleOutDetailID), pdNew => new SampleOutDetail { SampleOutDetailID = sampleOutDetailSave.SampleOutDetailID });
                    }

                    //foreach (InvProductSerialNoTemp invSampleDetailSerialNo in invProductSerialNoTemp)
                    //{
                    //    InvProductSerialNoDetail invProductSerialNoDetailSave = new InvProductSerialNoDetail();

                    //    if (sampleOutHeader.DocumentStatus.Equals(1))
                    //    {
                    //        InvProductSerialNo invProductSerialNoSave = new InvProductSerialNo();

                    //        invProductSerialNoSave.CompanyID = sampleOutHeader.CompanyID;
                    //        invProductSerialNoSave.CostCentreID = sampleOutHeader.CostCentreID;
                    //        invProductSerialNoSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                    //        invProductSerialNoSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                    //        invProductSerialNoSave.LocationID = sampleOutHeader.LocationID;
                    //        invProductSerialNoSave.ProductID = invSampleDetailSerialNo.ProductID;
                    //        invProductSerialNoSave.UnitOfMeasureID = invSampleDetailSerialNo.UnitOfMeasureID;
                    //        invProductSerialNoSave.ExpiryDate = invSampleDetailSerialNo.ExpiryDate;
                    //        invProductSerialNoSave.SerialNo = invSampleDetailSerialNo.SerialNo;
                    //        invProductSerialNoSave.SerialNoStatus = 1;

                    //        context.InvProductSerialNos.Add(invProductSerialNoSave);
                    //    }

                    //    invProductSerialNoDetailSave.CompanyID = sampleOutHeader.CompanyID;
                    //    invProductSerialNoDetailSave.CostCentreID = sampleOutHeader.CostCentreID;
                    //    invProductSerialNoDetailSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                    //    invProductSerialNoDetailSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                    //    invProductSerialNoDetailSave.LocationID = sampleOutHeader.LocationID;
                    //    invProductSerialNoDetailSave.ProductID = invSampleDetailSerialNo.ProductID;
                    //    invProductSerialNoDetailSave.LineNo = invSampleDetailSerialNo.LineNo;
                    //    invProductSerialNoDetailSave.UnitOfMeasureID = invSampleDetailSerialNo.UnitOfMeasureID;
                    //    invProductSerialNoDetailSave.ExpiryDate = invSampleDetailSerialNo.ExpiryDate;
                    //    invProductSerialNoDetailSave.SerialNo = invSampleDetailSerialNo.SerialNo;
                    //    invProductSerialNoDetailSave.ReferenceDocumentID = sampleOutHeader.SampleOutHeaderID;
                    //    invProductSerialNoDetailSave.ReferenceDocumentDocumentID = sampleOutHeader.DocumentID;

                    //    context.InvProductSerialNoDetails.Add(invProductSerialNoDetailSave);
                    //    context.SaveChanges();

                    //    context.InvProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && pd.LocationID.Equals(sampleOutHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID) && pd.InvProductSerialNoDetailID.Equals(invProductSerialNoDetailSave.InvProductSerialNoDetailID), pdNew => new InvProductSerialNoDetail { ProductSerialNoDetailID = invProductSerialNoDetailSave.InvProductSerialNoDetailID });
                    //}

                    //context.SampleOutHeaders.Update(ph => ph.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID) && ph.LocationID.Equals(sampleOutHeader.LocationID) && ph.DocumentID.Equals(sampleOutHeader.DocumentID), phNew => new SampleOutHeader { SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID });
                    context.SaveChanges();

                    transaction.Complete();

                    return true;
                }
                else
                {
                    List<LgsProductSerialNoDetail> lgsProductSerialNoDetailDelete = new List<LgsProductSerialNoDetail>();
                    lgsProductSerialNoDetailDelete = context.LgsProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && p.LocationID.Equals(sampleOutHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID)).ToList();

                    foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailDeleteTemp in lgsProductSerialNoDetailDelete)
                    {
                        context.LgsProductSerialNoDetails.Remove(lgsProductSerialNoDetailDeleteTemp);
                    }
                    context.SaveChanges();

                    foreach (SampleOutDetailTemp sampleOutDetail in sampleOutDetailTemp)
                    {
                        SampleOutDetail sampleOutDetailSave = new SampleOutDetail();

                        sampleOutDetailSave.CompanyID = sampleOutHeader.CompanyID;
                        sampleOutDetailSave.ConvertFactor = sampleOutDetail.ConvertFactor;
                        sampleOutDetailSave.CostCentreID = sampleOutHeader.CostCentreID;
                        sampleOutDetailSave.CostPrice = sampleOutDetail.CostPrice;
                        sampleOutDetailSave.BatchNo = sampleOutDetail.BatchNo;
                        sampleOutDetailSave.DocumentID = sampleOutHeader.DocumentID;
                        sampleOutDetailSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                        sampleOutDetailSave.GrossAmount = sampleOutDetail.GrossAmount;
                        sampleOutDetailSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                        sampleOutDetailSave.LineNo = sampleOutDetail.LineNo;
                        sampleOutDetailSave.LocationID = sampleOutHeader.LocationID;
                        sampleOutDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleOutDetailSave.OrderQty = sampleOutDetail.OrderQty;
                        sampleOutDetailSave.ProductID = sampleOutDetail.ProductID;
                        sampleOutDetailSave.SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID;
                        sampleOutDetailSave.BalancedQty = sampleOutDetail.OrderQty;
                        sampleOutDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleOutDetailSave.UnitOfMeasureID = sampleOutDetail.UnitOfMeasureID;
                        sampleOutDetailSave.BaseUnitID = sampleOutDetail.BaseUnitID;

                        if (sampleOutHeader.DocumentStatus.Equals(1))
                        {
                            context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(sampleOutHeader.LocationID) && ps.ProductID.Equals(sampleOutDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock - sampleOutDetail.OrderQty });
                            //context.LgsProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleOutHeader.LocationID) && bd.ProductID.Equals(sampleOutDetail.ProductID) && bd.BatchNo.Equals(sampleOutDetail.BatchNo), bd => new LgsProductBatchNoExpiaryDetail { Qty = bd.Qty - sampleOutDetail.OrderQty });
                            context.LgsProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleOutHeader.LocationID) && bd.ProductID.Equals(sampleOutDetail.ProductID) && bd.BatchNo.Equals(sampleOutDetail.BatchNo) && bd.UnitOfMeasureID.Equals(sampleOutDetail.UnitOfMeasureID), bd => new LgsProductBatchNoExpiaryDetail { BalanceQty = bd.BalanceQty - sampleOutDetail.OrderQty });
                        }
                        context.SampleOutDetails.Add(sampleOutDetailSave);
                        context.SaveChanges();

                        //context.SampleOutDetails.Update(pd => pd.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID) && pd.LocationID.Equals(sampleOutHeader.LocationID) && pd.DocumentID.Equals(sampleOutHeader.DocumentID) && pd.SampleOutHeaderID.Equals(sampleOutDetailSave.SampleOutDetailID), pdNew => new SampleOutDetail { SampleOutDetailID = sampleOutDetailSave.SampleOutDetailID });
                    }

                    //foreach (LgsProductSerialNoTemp lgsSampleDetailSerialNo in lgsProductSerialNoTemp)
                    //{
                    //    LgsProductSerialNoDetail lgsProductSerialNoDetailSave = new LgsProductSerialNoDetail();

                    //    if (sampleOutHeader.DocumentStatus.Equals(1))
                    //    {
                    //        LgsProductSerialNo lgsProductSerialNoSave = new LgsProductSerialNo();

                    //        lgsProductSerialNoSave.CompanyID = sampleOutHeader.CompanyID;
                    //        lgsProductSerialNoSave.CostCentreID = sampleOutHeader.CostCentreID;
                    //        lgsProductSerialNoSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                    //        lgsProductSerialNoSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                    //        lgsProductSerialNoSave.LocationID = sampleOutHeader.LocationID;
                    //        lgsProductSerialNoSave.ProductID = lgsSampleDetailSerialNo.ProductID;
                    //        lgsProductSerialNoSave.UnitOfMeasureID = lgsSampleDetailSerialNo.UnitOfMeasureID;
                    //        lgsProductSerialNoSave.SerialNo = lgsSampleDetailSerialNo.SerialNo;
                    //        lgsProductSerialNoSave.SerialNoStatus = 1;

                    //        context.LgsProductSerialNos.Add(lgsProductSerialNoSave);
                    //    }

                    //    lgsProductSerialNoDetailSave.CompanyID = sampleOutHeader.CompanyID;
                    //    lgsProductSerialNoDetailSave.CostCentreID = sampleOutHeader.CostCentreID;
                    //    lgsProductSerialNoDetailSave.DocumentStatus = sampleOutHeader.DocumentStatus;
                    //    lgsProductSerialNoDetailSave.GroupOfCompanyID = sampleOutHeader.GroupOfCompanyID;
                    //    lgsProductSerialNoDetailSave.LocationID = sampleOutHeader.LocationID;
                    //    lgsProductSerialNoDetailSave.ProductID = lgsSampleDetailSerialNo.ProductID;
                    //    lgsProductSerialNoDetailSave.LineNo = lgsSampleDetailSerialNo.LineNo;
                    //    lgsProductSerialNoDetailSave.UnitOfMeasureID = lgsSampleDetailSerialNo.UnitOfMeasureID;
                    //    lgsProductSerialNoDetailSave.SerialNo = lgsSampleDetailSerialNo.SerialNo;
                    //    lgsProductSerialNoDetailSave.ReferenceDocumentID = sampleOutHeader.SampleOutHeaderID;
                    //    lgsProductSerialNoDetailSave.ReferenceDocumentDocumentID = sampleOutHeader.DocumentID;

                    //    context.LgsProductSerialNoDetails.Add(lgsProductSerialNoDetailSave);
                    //    context.SaveChanges();

                    //    context.LgsProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && pd.LocationID.Equals(sampleOutHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID) && pd.LgsProductSerialNoDetailID.Equals(lgsProductSerialNoDetailSave.LgsProductSerialNoDetailID), pdNew => new LgsProductSerialNoDetail { ProductSerialNoDetailID = lgsProductSerialNoDetailSave.LgsProductSerialNoDetailID });
                    //}

                    //context.SampleOutHeaders.Update(ph => ph.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID) && ph.LocationID.Equals(sampleOutHeader.LocationID) && ph.DocumentID.Equals(sampleOutHeader.DocumentID), phNew => new SampleOutHeader { SampleOutHeaderID = sampleOutHeader.SampleOutHeaderID });
                    context.SaveChanges();

                    transaction.Complete();

                    return true;
                }

            }

        }


        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo, bool isInv)
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
                if (isInv)
                {
                    preFix = preFix + "IN";
                }
                else
                {
                    preFix = preFix + "LG";
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


        public SampleOutHeader GetSampleOutByDocumentNo(string documentNo)
        {
            return context.SampleOutHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<SampleOutDetailTemp> GetPausedSampleOutDetail(SampleOutHeader sampleOutHeader, bool isInv)
        {
            SampleOutDetailTemp sampleOutDetailTemp;

            List<SampleOutDetailTemp> sampleOutDetailTempList = new List<SampleOutDetailTemp>();

            if (isInv)
            {
                var sampleOutTemp = (from pm in context.InvProductMasters
                                     join sd in context.SampleOutDetails on pm.InvProductMasterID equals sd.ProductID
                                     join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                     where sd.LocationID.Equals(sampleOutHeader.LocationID) && sd.CompanyID.Equals(sampleOutHeader.CompanyID)
                                     && sd.DocumentID.Equals(sampleOutHeader.DocumentID) && sd.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID)
                                     select new
                                     {
                                         sd.AverageCost,
                                         sd.CostPrice,
                                         sd.GrossAmount,
                                         sd.ConvertFactor,
                                         sd.LineNo,
                                         sd.LocationID,
                                         sd.NetAmount,
                                         sd.OrderQty,
                                         pm.ProductCode,
                                         sd.ProductID,
                                         pm.ProductName,
                                         sd.BalancedQty,
                                         sd.UnitOfMeasureID,
                                         sd.BatchNo,
                                         uc.UnitOfMeasureName

                                     }).ToArray();

                foreach (var tempProduct in sampleOutTemp)
                {

                    sampleOutDetailTemp = new SampleOutDetailTemp();

                    sampleOutDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleOutDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleOutDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleOutDetailTemp.LineNo = tempProduct.LineNo;
                    sampleOutDetailTemp.LocationID = tempProduct.LocationID;
                    sampleOutDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleOutDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleOutDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleOutDetailTemp.ProductID = tempProduct.ProductID;
                    sampleOutDetailTemp.ProductName = tempProduct.ProductName;
                    sampleOutDetailTemp.BalancedQty = tempProduct.BalancedQty;
                    sampleOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    sampleOutDetailTemp.BatchNo = tempProduct.BatchNo;

                    sampleOutDetailTempList.Add(sampleOutDetailTemp);
                }
                return sampleOutDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
            else
            {
                var sampleOutTemp = (from pm in context.LgsProductMasters
                                     join sd in context.SampleOutDetails on pm.LgsProductMasterID equals sd.ProductID
                                     join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                     where sd.LocationID.Equals(sampleOutHeader.LocationID) && sd.CompanyID.Equals(sampleOutHeader.CompanyID)
                                     && sd.DocumentID.Equals(sampleOutHeader.DocumentID) && sd.SampleOutHeaderID.Equals(sampleOutHeader.SampleOutHeaderID)
                                     select new
                                     {
                                         sd.AverageCost,
                                         sd.ConvertFactor,
                                         sd.CostPrice,
                                         sd.GrossAmount,
                                         sd.LineNo,
                                         sd.LocationID,
                                         sd.NetAmount,
                                         sd.OrderQty,
                                         pm.ProductCode,
                                         sd.ProductID,
                                         pm.ProductName,
                                         sd.BalancedQty,
                                         sd.UnitOfMeasureID,
                                         sd.BatchNo,
                                         uc.UnitOfMeasureName

                                     }).ToArray();

                foreach (var tempProduct in sampleOutTemp)
                {

                    sampleOutDetailTemp = new SampleOutDetailTemp();

                    sampleOutDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleOutDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleOutDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleOutDetailTemp.LineNo = tempProduct.LineNo;
                    sampleOutDetailTemp.LocationID = tempProduct.LocationID;
                    sampleOutDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleOutDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleOutDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleOutDetailTemp.ProductID = tempProduct.ProductID;
                    sampleOutDetailTemp.ProductName = tempProduct.ProductName;
                    sampleOutDetailTemp.BalancedQty = tempProduct.BalancedQty;
                    sampleOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleOutDetailTemp.BatchNo = tempProduct.BatchNo;
                    sampleOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    sampleOutDetailTempList.Add(sampleOutDetailTemp);
                }
                return sampleOutDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }

        //// For InvProduct
        public List<InvProductSerialNoTemp> getUpdateInvSerialNoTemp(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber)
        {
            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

            invProductSerialNoTemp = invProductSerialNoList.Where(s => s.SerialNo.Equals(invProductSerialNumber.SerialNo) && s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID)).FirstOrDefault();

            if (invProductSerialNoTemp == null || invProductSerialNoTemp.LineNo.Equals(0))
            {
                if (invProductSerialNoList.Count.Equals(0))
                    invProductSerialNumber.LineNo = 1;
                else
                    invProductSerialNumber.LineNo = invProductSerialNoList.Max(s => s.LineNo) + 1;

                invProductSerialNoList.Add(invProductSerialNumber);
            }
            return invProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfInvSerialNo(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, decimal Qty)
        {

            if (Qty.Equals(invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID)).Count()))
                return true;
            else
                return false;

        }
        //////

        ////For LgsProduct
        public List<LgsProductSerialNoTemp> getUpdateLgsSerialNoTemp(List<LgsProductSerialNoTemp> lgsProductSerialNoList, LgsProductSerialNoTemp lgsProductSerialNumber)
        {
            LgsProductSerialNoTemp lgsProductSerialNoTemp = new LgsProductSerialNoTemp();

            lgsProductSerialNoTemp = lgsProductSerialNoList.Where(s => s.SerialNo.Equals(lgsProductSerialNumber.SerialNo) && s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID)).FirstOrDefault();

            if (lgsProductSerialNoTemp == null || lgsProductSerialNoTemp.LineNo.Equals(0))
            {
                if (lgsProductSerialNoList.Count.Equals(0))
                    lgsProductSerialNumber.LineNo = 1;
                else
                    lgsProductSerialNumber.LineNo = lgsProductSerialNoList.Max(s => s.LineNo) + 1;

                lgsProductSerialNoList.Add(lgsProductSerialNumber);
            }
            return lgsProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool IsValidNoOfLgsSerialNo(List<LgsProductSerialNoTemp> lgsProductSerialNoList, LgsProductSerialNoTemp lgsProductSerialNumber, decimal Qty)
        {

            if (Qty.Equals(lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID)).Count()))
                return true;
            else
                return false;

        }
        ////

        public string[] GetAllIssuedToNames() 
        {
            List<string> IssuedToList = context.SampleOutHeaders.Where(a => a.IsDelete.Equals(false)).Select(a => a.IssuedTo).ToList();
            return IssuedToList.ToArray();
        }

        public string[] GetAllDeliveryPersons()
        {
            List<string> DeliveryList = context.SampleOutHeaders.Where(a => a.IsDelete.Equals(false)).Select(a => a.DeliveryPerson).ToList();
            return DeliveryList.ToArray();
        }

        public bool IsValidNoOfSerialNoInv(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, decimal Qty)
        {

            if (Qty.Equals(invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID)).Count()))
                return true;
            else
                return false;
        }

        public bool IsValidNoOfSerialNoLgs(List<LgsProductSerialNoTemp> lgsProductSerialNoList, LgsProductSerialNoTemp lgsProductSerialNumber, decimal Qty)
        {

            if (Qty.Equals(lgsProductSerialNoList.Where(s => s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID)).Count()))
                return true;
            else
                return false;
        }

        public SampleOutHeader getSampleOutHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.SampleOutHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.SampleOutHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public SampleOutHeader getSampleOutHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.SampleOutHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, LgsProductSerialNoTemp lgsProductSerialNoTemp, int locationID, bool isInv)
        {
            if (isInv)
            {
                SampleOutHeader sampleOutHeader = new SampleOutHeader();
                sampleOutHeader = getSampleOutHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (sampleOutHeader == null)
                    sampleOutHeader = new SampleOutHeader();

                InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(invProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (InvProductSerialNoDetailtmp != null)
                {
                    sampleOutHeader = getSampleOutHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (sampleOutHeader != null)
                        return sampleOutHeader.DocumentNo;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                SampleOutHeader sampleOutHeader = new SampleOutHeader();
                sampleOutHeader = getSampleOutHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (sampleOutHeader == null)
                    sampleOutHeader = new SampleOutHeader();

                LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
                LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(lgsProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (LgsProductSerialNoDetailtmp != null)
                {
                    sampleOutHeader = getSampleOutHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (sampleOutHeader != null)
                        return sampleOutHeader.DocumentNo;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public List<InvProductSerialNoTemp> GetInvSerialNoDetail(InvProductMaster invProductMaster)
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

        public List<InvProductSerialNoTemp> GetLgsSerialNoDetail(LgsProductMaster lgsProductMaster)
        {
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            List<LgsProductSerialNo> lgsProductSerialNo = new List<LgsProductSerialNo>();

            lgsProductSerialNo = context.LgsProductSerialNos.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
            foreach (LgsProductSerialNo lgsProductSerialNoRetrive in lgsProductSerialNo)
            {
                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();

                lgsProductSerialNoTemp.ProductID = lgsProductSerialNoRetrive.ProductID;
                lgsProductSerialNoTemp.SerialNo = lgsProductSerialNoRetrive.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoRetrive.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }

            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp invProductSerialNoTemp, LgsProductSerialNoTemp lgsProductSerialNoTemp, bool isInv)
        {
            if (isInv)
            {
                if (context.InvProductSerialNos.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(1)).FirstOrDefault() == null)
                    return false;
                else
                    return true;
            }
            else
            {
                if (context.LgsProductSerialNos.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(1)).FirstOrDefault() == null)
                    return false;
                else
                    return true;
            }
        }

        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public bool IsValidNoOfQty(int qty, SampleOutDetailTemp sampleOutDetailTemp)
        {
            decimal currentQty = 0;
            var getCurrentQty = (from a in context.SampleOutDetails
                                 where a.ProductID.Equals(sampleOutDetailTemp.ProductID) && a.SampleOutHeaderID.Equals(sampleOutDetailTemp.SampleOutHeaderID) && a.UnitOfMeasureID.Equals(sampleOutDetailTemp.UnitOfMeasureID)
                                 select new
                                 {
                                     a.BalancedQty
                                 }).ToArray();

            foreach (var temp in getCurrentQty)
            {
                currentQty = temp.BalancedQty;
            }

            if (currentQty >= qty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public DataTable GetSampleOutDocumentNumberDataTable(int locationID)
        //{
        //    DataTable tblSampleOut = new DataTable();
        //    var query = from sa in context.SampleOutHeaders where sa.LocationID == locationID select new { sa.DocumentNo, sa.DocumentDate, sa.SampleOutMethod, sa.TotalQty, sa.TotalBalancedQty, sa.NetAmount };
        //    return tblSampleOut = Common.LINQToDataTable(query);
        //}


        public List<SampleOutDetailTemp> GetDeleteSampleOutDetailTemp(List<SampleOutDetailTemp> sampleOutTempDetail, SampleOutDetailTemp sampleOutDetailTemp)
        {
            SampleOutDetailTemp SampleOutTempDetailTemp = new SampleOutDetailTemp();

            SampleOutTempDetailTemp = sampleOutTempDetail.Where(p => p.ProductID == sampleOutDetailTemp.ProductID && p.UnitOfMeasureID == sampleOutDetailTemp.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (SampleOutTempDetailTemp != null)
            {
                sampleOutTempDetail.Remove(SampleOutTempDetailTemp);
                removedLineNo = SampleOutTempDetailTemp.LineNo;

            }
            sampleOutTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            return sampleOutTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public SampleOutHeader RecallSampleOutHeaderToServiceIn(long sampleOutHeaderID)
        {
            return context.SampleOutHeaders.Where(ph => ph.SampleOutHeaderID.Equals(sampleOutHeaderID)).FirstOrDefault();
        }


        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo, int sampleOutType)
        {
            // Create query 
            IQueryable qryResult = context.SampleOutHeaders.Where("DocumentStatus == @0 AND DocumentId == @1 AND SampleOutType == @2", 1, autoGenerateInfo.DocumentID, sampleOutType);

            switch (reportDataStruct.DbColumnName.Trim())
            {                
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
        /// Get sample out destails for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetSamleOutDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo, int sampleOutType)
        {

            StringBuilder selectFields = new StringBuilder();
            var query = context.SampleOutHeaders.Where("DocumentStatus == @0 AND DocumentId ==@1 AND SampleOutType == @2", 1, autoGenerateInfo.DocumentID, sampleOutType);

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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }


            var queryResult = (from h in query                               
                               join l in context.Locations on h.LocationID equals l.LocationID                               
                               select
                                   new
                                   {
                                       FieldString1 = h.DocumentNo,
                                       FieldString2 = EntityFunctions.TruncateTime(h.DocumentDate),
                                       FieldString3 = h.IssuedTo,
                                       FieldString4 = h.DeliveryPerson,
                                       FieldString5 = h.ReferenceNo,
                                       FieldString6 = h.Remark,
                                       FieldString7 = l.LocationCode + " " + l.LocationName,
                                   }).ToArray();

            return queryResult.ToDataTable();
           
        }

        /// <summary>
        /// Get Sample Out details for report
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        /// <param name="documentId"></param>
        /// <param name="TransactionType" (1-->Inventry, 2-->Logistic) ></param>
        /// <returns></returns>
        public DataTable GetSampleOutTransactionDataTable(string documentNo, int documentStatus, int documentId, int transactionType)
        {
            string lookupType = ((int)LookUpReference.ModuleType).ToString();

            var query = transactionType.Equals(1) ? 
                        (from ph in context.SampleOutHeaders
                        join pd in context.SampleOutDetails on ph.SampleOutHeaderID equals pd.SampleOutHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join re in context.ReferenceTypes on ph.SampleOutType equals re.LookupKey
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        && re.LookupType.Equals(lookupType)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = ph.IssuedTo,
                                FieldString4 = ph.DeliveryPerson,
                                FieldString5 = ph.Remark,
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = ph.ReferenceNo,
                                FieldString9 = "", //py.PaymentMethodName,
                                FieldString10 = re.LookupValue,
                                FieldString11 = l.LocationCode + "  " + l.LocationName,
                                FieldString12 = "",
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = "",
                                FieldString17 = pd.OrderQty,
                                FieldString18 = "",
                                FieldString19 = pd.CostPrice,
                                FieldString20 = "", //pd.DiscountPercentage,
                                FieldString21 = "", //pd.DiscountAmount,
                                FieldString22 = pd.NetAmount,
                                FieldString23 = ph.NetAmount,
                                FieldString24 = "", //ph.DiscountPercentage,
                                FieldString25 = "", //ph.DiscountAmount,
                                FieldString26 = "", //ph.TaxAmount,
                                FieldString27 = "",
                                FieldString28 = "",
                            }) :
                        (from ph in context.SampleOutHeaders
                        join pd in context.SampleOutDetails on ph.SampleOutHeaderID equals pd.SampleOutHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join re in context.ReferenceTypes on ph.SampleOutType equals re.LookupKey
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        && re.LookupType.Equals(lookupType)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = ph.IssuedTo,
                                FieldString4 = ph.DeliveryPerson,
                                FieldString5 = ph.Remark,
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = ph.ReferenceNo,
                                FieldString9 = "", //py.PaymentMethodName,
                                FieldString10 = re.LookupValue,
                                FieldString11 = l.LocationCode + "  " + l.LocationName,
                                FieldString12 = "",
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = "",
                                FieldString17 = pd.OrderQty,
                                FieldString18 = "",
                                FieldString19 = pd.CostPrice,
                                FieldString20 = "", //pd.DiscountPercentage,
                                FieldString21 = "", //pd.DiscountAmount,
                                FieldString22 = pd.NetAmount,
                                FieldString23 = ph.NetAmount,
                                FieldString24 = "", //ph.DiscountPercentage,
                                FieldString25 = "", //ph.DiscountAmount,
                                FieldString26 = "", //ph.TaxAmount,
                                FieldString27 = "",
                                FieldString28 = "",
                            })
                            ;
            return query.AsEnumerable().ToDataTable();

        }

        public List<InvProductSerialNoTemp> getLgsSerialNoDetailForSampleOut(SampleOutHeader sampleOutHeader) 
        {
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<LgsProductSerialNoDetail> lgsProductSerialNoDetail = new List<LgsProductSerialNoDetail>();

            lgsProductSerialNoDetail = context.LgsProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && ps.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID)).ToList();
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

        public List<InvProductSerialNoTemp> getInvSerialNoDetailForSampleOut(SampleOutHeader sampleOutHeader)  
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

            invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(sampleOutHeader.SampleOutHeaderID) && ps.ReferenceDocumentDocumentID.Equals(sampleOutHeader.DocumentID)).ToList();
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

        public string[] GetAllInvProductCodes() 
        {
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllInvProductNames() 
        {
            List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllLgsProductCodes() 
        {
            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllLgsProductNames() 
        {
            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();

            return ProductNameList.ToArray();
        }

        public string[] GetAllProductCodesAccordingToSampleOut(bool isInv, string sampleOutDocNo)
        {
            List<String> productCodeList = new List<string>();
            if (isInv)
            {
               var qry = (from so in context.SampleOutDetails
                           join sh in context.SampleOutHeaders on so.SampleOutHeaderID equals sh.SampleOutHeaderID
                           join pm in context.InvProductMasters on so.ProductID equals pm.InvProductMasterID
                           where sh.DocumentNo == sampleOutDocNo
                           select new
                           {
                               pm.ProductCode,
                               pm.ProductName
                           }).ToArray();
               foreach (var temp in qry)
               {
                   productCodeList.Add(temp.ProductCode);
               }
            }
            else
            {
                var qry = (from so in context.SampleOutDetails
                           join sh in context.SampleOutHeaders on so.SampleOutHeaderID equals sh.SampleOutHeaderID
                           join pm in context.InvProductMasters on so.ProductID equals pm.InvProductMasterID
                           where sh.DocumentNo == sampleOutDocNo
                           select new
                           {
                               pm.ProductCode,
                               pm.ProductName
                           }).ToArray();
                foreach (var temp in qry)
                {
                    productCodeList.Add(temp.ProductCode);
                }
            }
            return productCodeList.ToArray();
        }

        public string[] GetAllProductNamesAccordingToSampleOut(bool isInv, string sampleOutDocNo) 
        {
            List<String> productNameList = new List<string>();
            if (isInv)
            {
                var qry = (from so in context.SampleOutDetails
                           join sh in context.SampleOutHeaders on so.SampleOutHeaderID equals sh.SampleOutHeaderID
                           join pm in context.InvProductMasters on so.ProductID equals pm.InvProductMasterID
                           where sh.DocumentNo == sampleOutDocNo
                           select new
                           {
                               pm.ProductCode,
                               pm.ProductName
                           }).ToArray();
                foreach (var temp in qry)
                {
                    productNameList.Add(temp.ProductName);
                }
            }
            else
            {
                var qry = (from so in context.SampleOutDetails
                           join sh in context.SampleOutHeaders on so.SampleOutHeaderID equals sh.SampleOutHeaderID
                           join pm in context.InvProductMasters on so.ProductID equals pm.InvProductMasterID
                           where sh.DocumentNo == sampleOutDocNo
                           select new
                           {
                               pm.ProductCode,
                               pm.ProductName
                           }).ToArray();
                foreach (var temp in qry)
                {
                    productNameList.Add(temp.ProductName);
                }
            }
            return productNameList.ToArray();
        }


        public bool checkIsSampleOutProduct(string sampleOutHeaderDocNo, long productID)
        {
            bool isTrue = false;
            var qry = (from sh in context.SampleOutHeaders
                       join sd in context.SampleOutDetails on sh.SampleOutHeaderID equals sd.SampleOutHeaderID
                       where sh.DocumentNo == sampleOutHeaderDocNo
                       select new
                       {
                           sd.ProductID,
                       });
            foreach (var temp in qry)
            {
                if (temp.ProductID == productID) 
                { 
                    isTrue = true;
                    break;
                }
                else 
                { 
                    isTrue = false; 
                }
            }
            return isTrue;
        }

        #endregion
    }
}
