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
    public class SampleInService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public SampleInDetailTemp getSampleInDetailTemp(List<SampleInDetailTemp> SampleInDetailTemp, LgsProductMaster lgsProductMaster, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID, bool isInv)
        {
            if (isInv)
            {
                SampleInDetailTemp sampleInDetailTemp = new SampleInDetailTemp();
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
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                    if (sampleInDetailTemp == null)
                    {
                        sampleInDetailTemp = new SampleInDetailTemp();
                        sampleInDetailTemp.ProductID = tempProduct.InvProductMasterID;
                        sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                        sampleInDetailTemp.ProductName = tempProduct.ProductName;
                        sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                        sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                        sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    }
                }
                return sampleInDetailTemp;
            }
            else
            {
                SampleInDetailTemp sampleInDetailTemp = new SampleInDetailTemp();
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
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        sampleInDetailTemp = SampleInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                    if (sampleInDetailTemp == null)
                    {
                        sampleInDetailTemp = new SampleInDetailTemp();
                        sampleInDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                        sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                        sampleInDetailTemp.ProductName = tempProduct.ProductName;
                        sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                        sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                        sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    }
                }
                return sampleInDetailTemp;
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.SampleInHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<SampleInDetailTemp> getUpdateSampleInDetailTemp(List<SampleInDetailTemp> sampleInTempDetail, SampleInDetailTemp sampleInDetail)//Product Master Removed from parameters
        {
            SampleInDetailTemp sampleInDetailTemp = new SampleInDetailTemp();

            sampleInDetailTemp = sampleInTempDetail.Where(p => p.ProductID == sampleInDetail.ProductID && p.UnitOfMeasureID == sampleInDetail.UnitOfMeasureID).FirstOrDefault();

            if (sampleInDetailTemp == null || sampleInDetailTemp.LineNo.Equals(0))
            {
                sampleInDetail.LineNo = sampleInTempDetail.Count + 1;
            }
            else
            {
                sampleInTempDetail.Remove(sampleInDetailTemp);
                sampleInDetail.LineNo = sampleInDetailTemp.LineNo;
            }

            sampleInTempDetail.Add(sampleInDetail);

            return sampleInTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public SampleInHeader GetPausedSampleInHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.SampleInHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }


        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<SampleInDetailTemp> DeleteProductSampleInDetailTemp(List<SampleInDetailTemp> sampleInDetailsTemp, string productCode)
        {
            sampleInDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return sampleInDetailsTemp;
        }

        public List<SampleInDetail> GetAllSampleInDetail()
        {
            return context.SampleInDetails.ToList();
        }


        public bool Save(SampleInHeader sampleInHeader, List<SampleInDetailTemp> sampleInDetailTemp, bool isInv) 
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (sampleInHeader.SampleInHeaderID.Equals(0))
                    context.SampleInHeaders.Add(sampleInHeader);
                else
                    context.Entry(sampleInHeader).State = EntityState.Modified;

                context.SaveChanges();

                List<SampleInDetail> sampleInDetailDelete = new List<SampleInDetail>();
                sampleInDetailDelete = context.SampleInDetails.Where(p => p.SampleInHeaderID.Equals(sampleInHeader.SampleInHeaderID) && p.LocationID.Equals(sampleInHeader.LocationID) && p.DocumentID.Equals(sampleInHeader.DocumentID)).ToList();

                foreach (SampleInDetail sampleInDetailDeleteTemp in sampleInDetailDelete)
                {
                    context.SampleInDetails.Remove(sampleInDetailDeleteTemp);
                }

                if (isInv)
                {
                    foreach (SampleInDetailTemp sampleOutDetail in sampleInDetailTemp)
                    {
                        SampleInDetail sampleInDetailSave = new SampleInDetail();

                        sampleInDetailSave.CompanyID = sampleInHeader.CompanyID;
                        sampleInDetailSave.ConvertFactor = sampleOutDetail.ConvertFactor;
                        sampleInDetailSave.CostCentreID = sampleInHeader.CostCentreID;
                        sampleInDetailSave.CostPrice = sampleOutDetail.CostPrice;
                        sampleInDetailSave.DocumentID = sampleInHeader.DocumentID;
                        sampleInDetailSave.DocumentStatus = sampleInHeader.DocumentStatus;
                        sampleInDetailSave.GrossAmount = sampleOutDetail.GrossAmount;
                        sampleInDetailSave.GroupOfCompanyID = sampleInHeader.GroupOfCompanyID;
                        sampleInDetailSave.BatchNo = sampleOutDetail.BatchNo;
                        sampleInDetailSave.LineNo = sampleOutDetail.LineNo;
                        sampleInDetailSave.LocationID = sampleInHeader.LocationID;
                        sampleInDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleInDetailSave.OrderQty = sampleOutDetail.OrderQty;
                        sampleInDetailSave.ProductID = sampleOutDetail.ProductID;
                        sampleInDetailSave.SampleInHeaderID = sampleInHeader.SampleInHeaderID;
                        sampleInDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleInDetailSave.UnitOfMeasureID = sampleOutDetail.UnitOfMeasureID;
                        sampleInDetailSave.BaseUnitID = sampleOutDetail.BaseUnitID;

                        if (sampleInHeader.DocumentStatus.Equals(1))
                        {
                            context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(sampleInHeader.LocationID) && ps.ProductID.Equals(sampleOutDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + sampleOutDetail.OrderQty });
                            context.SampleOutDetails.Update(sd => sd.LocationID.Equals(sampleInDetailSave.LocationID) && sd.ProductID.Equals(sampleInDetailSave.ProductID) && sd.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sd => new SampleOutDetail { BalancedQty = sd.BalancedQty - sampleInDetailSave.OrderQty });
                            context.InvProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleInHeader.LocationID) && bd.ProductID.Equals(sampleInDetailSave.ProductID) && bd.BatchNo.Equals(sampleInDetailSave.BatchNo) && bd.UnitOfMeasureID.Equals(sampleInDetailSave.UnitOfMeasureID), bd => new InvProductBatchNoExpiaryDetail { BalanceQty = bd.BalanceQty + sampleInDetailSave.OrderQty });
                        }
                        context.SampleInDetails.Add(sampleInDetailSave);
                        context.SaveChanges();  
                    }

                    if (sampleInHeader.DocumentStatus.Equals(1))
                    {
                        context.SampleOutHeaders.Update(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sh => new SampleOutHeader { TotalBalancedQty = sh.TotalBalancedQty - sampleInHeader.TotalQty });

                        decimal sampleOutQty = context.SampleOutHeaders.Where(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID)).Select(sh => sh.TotalBalancedQty).FirstOrDefault();

                        if (sampleOutQty.Equals(0))
                        {
                            context.SampleOutHeaders.Update(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sh => new SampleOutHeader { Balanced = 1 });
                        }
                    }

                    transaction.Complete();

                    return true;
                }
                else
                {
                    foreach (SampleInDetailTemp sampleOutDetail in sampleInDetailTemp)
                    {
                        SampleInDetail sampleInDetailSave = new SampleInDetail();

                        sampleInDetailSave.CompanyID = sampleInHeader.CompanyID;
                        sampleInDetailSave.ConvertFactor = sampleOutDetail.ConvertFactor;
                        sampleInDetailSave.CostCentreID = sampleInHeader.CostCentreID;
                        sampleInDetailSave.CostPrice = sampleOutDetail.CostPrice;
                        sampleInDetailSave.DocumentID = sampleInHeader.DocumentID;
                        sampleInDetailSave.DocumentStatus = sampleInHeader.DocumentStatus;
                        sampleInDetailSave.GrossAmount = sampleOutDetail.GrossAmount;
                        sampleInDetailSave.GroupOfCompanyID = sampleInHeader.GroupOfCompanyID;
                        sampleInDetailSave.LineNo = sampleOutDetail.LineNo;
                        sampleInDetailSave.BatchNo = sampleOutDetail.BatchNo;
                        sampleInDetailSave.LocationID = sampleInHeader.LocationID;
                        sampleInDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleInDetailSave.OrderQty = sampleOutDetail.OrderQty;
                        sampleInDetailSave.ProductID = sampleOutDetail.ProductID;
                        sampleInDetailSave.SampleInHeaderID = sampleInHeader.SampleInHeaderID;
                        //sampleInDetailSave.Qty = sampleOutDetail.IssuedBackQty;
                        sampleInDetailSave.OrderQty = sampleOutDetail.OrderQty;
                        sampleInDetailSave.NetAmount = sampleOutDetail.NetAmount;
                        sampleInDetailSave.UnitOfMeasureID = sampleOutDetail.UnitOfMeasureID;
                        sampleInDetailSave.BaseUnitID = sampleOutDetail.BaseUnitID;

                        if (sampleInHeader.DocumentStatus.Equals(1))
                        {
                            context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(sampleInHeader.LocationID) && ps.ProductID.Equals(sampleOutDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock + sampleOutDetail.OrderQty });
                            context.SampleOutDetails.Update(sd => sd.LocationID.Equals(sampleInDetailSave.LocationID) && sd.ProductID.Equals(sampleInDetailSave.ProductID) && sd.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sd => new SampleOutDetail { BalancedQty = sd.BalancedQty - sampleInDetailSave.OrderQty });
                            context.LgsProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(sampleInHeader.LocationID) && bd.ProductID.Equals(sampleInDetailSave.ProductID) && bd.BatchNo.Equals(sampleInDetailSave.BatchNo) && bd.UnitOfMeasureID.Equals(sampleInDetailSave.UnitOfMeasureID), bd => new LgsProductBatchNoExpiaryDetail { BalanceQty = bd.BalanceQty + sampleInDetailSave.OrderQty });
                        }
                        context.SampleInDetails.Add(sampleInDetailSave);
                        context.SaveChanges();
                    }

                    if (sampleInHeader.DocumentStatus.Equals(1))
                    {
                        context.SampleOutHeaders.Update(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sh => new SampleOutHeader { TotalBalancedQty = sh.TotalBalancedQty - sampleInHeader.TotalQty });

                        decimal sampleOutQty = context.SampleOutHeaders.Where(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID)).Select(sh => sh.TotalBalancedQty).FirstOrDefault();

                        if (sampleOutQty.Equals(0))
                        {
                            context.SampleOutHeaders.Update(sh => sh.LocationID.Equals(sampleInHeader.LocationID) && sh.SampleOutHeaderID.Equals(sampleInHeader.SampleOutHeaderID), sh => new SampleOutHeader { Balanced = 1 });
                        }
                    }

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


        public SampleInHeader GetSampleInByDocumentNo(string documentNo)
        {
            return context.SampleInHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<SampleInDetailTemp> getPausedSampleInDetail(SampleInHeader sampleInHeader, bool isInv)
        {
            SampleInDetailTemp sampleInDetailTemp;

            List<SampleInDetailTemp> sampleInDetailTempList = new List<SampleInDetailTemp>();

            if (isInv)
            {
                var sampleOutTemp = (from pm in context.InvProductMasters
                                     join sd in context.SampleInDetails on pm.InvProductMasterID equals sd.ProductID
                                     join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                     where sd.LocationID.Equals(sampleInHeader.LocationID) && sd.CompanyID.Equals(sampleInHeader.CompanyID)
                                     && sd.DocumentID.Equals(sampleInHeader.DocumentID) && sd.SampleInHeaderID.Equals(sampleInHeader.SampleInHeaderID)
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
                                         Qty = sd.Qty,
                                         sd.UnitOfMeasureID,
                                         sd.BatchNo,
                                         uc.UnitOfMeasureName

                                     }).ToArray();

                foreach (var tempProduct in sampleOutTemp)
                {

                    sampleInDetailTemp = new SampleInDetailTemp();

                    sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleInDetailTemp.LineNo = tempProduct.LineNo;
                    sampleInDetailTemp.LocationID = tempProduct.LocationID;
                    sampleInDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleInDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleInDetailTemp.BatchNo = tempProduct.BatchNo;
                    sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleInDetailTemp.ProductID = tempProduct.ProductID;
                    sampleInDetailTemp.ProductName = tempProduct.ProductName;
                    sampleInDetailTemp.Qty = tempProduct.Qty;
                    sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    sampleInDetailTempList.Add(sampleInDetailTemp);
                }
                return sampleInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
            else
            {
                var sampleOutTemp = (from pm in context.LgsProductMasters
                                     join sd in context.SampleInDetails on pm.LgsProductMasterID equals sd.ProductID
                                     join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                     where sd.LocationID.Equals(sampleInHeader.LocationID) && sd.CompanyID.Equals(sampleInHeader.CompanyID)
                                     && sd.DocumentID.Equals(sampleInHeader.DocumentID) && sd.SampleInHeaderID.Equals(sampleInHeader.SampleInHeaderID)
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
                                         Qty = sd.Qty,
                                         sd.UnitOfMeasureID,
                                         sd.BatchNo,
                                         uc.UnitOfMeasureName

                                     }).ToArray();

                foreach (var tempProduct in sampleOutTemp)
                {

                    sampleInDetailTemp = new SampleInDetailTemp();

                    sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleInDetailTemp.LineNo = tempProduct.LineNo;
                    sampleInDetailTemp.LocationID = tempProduct.LocationID;
                    sampleInDetailTemp.BatchNo = tempProduct.BatchNo;
                    sampleInDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleInDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleInDetailTemp.ProductID = tempProduct.ProductID;
                    sampleInDetailTemp.ProductName = tempProduct.ProductName;
                    sampleInDetailTemp.Qty = tempProduct.Qty;
                    sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    sampleInDetailTempList.Add(sampleInDetailTemp);
                }
                return sampleInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
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
            List<string> IssuedToList = context.SampleInHeaders.Where(a => a.IsDelete.Equals(false)).Select(a => a.IssuedTo).ToList();
            return IssuedToList.ToArray();
        }

        public string[] GetAllDeliveryPersons()
        {
            List<string> DeliveryList = context.SampleInHeaders.Where(a => a.IsDelete.Equals(false)).Select(a => a.DeliveryPerson).ToList();
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

        public SampleInHeader getSampleInHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.SampleInHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.SampleInHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public SampleInHeader getSampleInHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.SampleInHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, LgsProductSerialNoTemp lgsProductSerialNoTemp, int locationID, bool isInv)
        {
            if (isInv)
            {
                SampleInHeader sampleInHeader = new SampleInHeader();
                sampleInHeader = getSampleInHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (sampleInHeader == null)
                    sampleInHeader = new SampleInHeader();

                InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(invProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(sampleInHeader.SampleInHeaderID) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (InvProductSerialNoDetailtmp != null)
                {
                    sampleInHeader = getSampleInHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (sampleInHeader != null)
                        return sampleInHeader.DocumentNo;
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
                SampleInHeader sampleInHeader = new SampleInHeader();
                sampleInHeader = getSampleInHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (sampleInHeader == null)
                    sampleInHeader = new SampleInHeader();

                LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
                LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(lgsProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(sampleInHeader.SampleInHeaderID) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (LgsProductSerialNoDetailtmp != null)
                {
                    sampleInHeader = getSampleInHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (sampleInHeader != null)
                        return sampleInHeader.DocumentNo;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
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

        public List<SampleInDetailTemp> GetSampleOutDetailForSampleIn(SampleOutHeader sampleOutHeader, bool isInv) 
        {
            SampleInDetailTemp sampleInDetailTemp;

            List<SampleInDetailTemp> sampleInDetailTempList = new List<SampleInDetailTemp>();

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

                    sampleInDetailTemp = new SampleInDetailTemp();

                    sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleInDetailTemp.LineNo = tempProduct.LineNo;
                    sampleInDetailTemp.LocationID = tempProduct.LocationID;
                    sampleInDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleInDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleInDetailTemp.ProductID = tempProduct.ProductID;
                    sampleInDetailTemp.ProductName = tempProduct.ProductName;
                    sampleInDetailTemp.BalancedQty = tempProduct.BalancedQty;
                    sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    sampleInDetailTemp.BatchNo = tempProduct.BatchNo;

                    sampleInDetailTempList.Add(sampleInDetailTemp);
                }
                return sampleInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
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

                    sampleInDetailTemp = new SampleInDetailTemp();

                    sampleInDetailTemp.AverageCost = tempProduct.AverageCost;
                    sampleInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    sampleInDetailTemp.CostPrice = tempProduct.CostPrice;
                    sampleInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    sampleInDetailTemp.LineNo = tempProduct.LineNo;
                    sampleInDetailTemp.LocationID = tempProduct.LocationID;
                    sampleInDetailTemp.NetAmount = tempProduct.NetAmount;
                    sampleInDetailTemp.OrderQty = tempProduct.OrderQty;
                    sampleInDetailTemp.ProductCode = tempProduct.ProductCode;
                    sampleInDetailTemp.ProductID = tempProduct.ProductID;
                    sampleInDetailTemp.ProductName = tempProduct.ProductName;
                    sampleInDetailTemp.BalancedQty = tempProduct.BalancedQty;
                    sampleInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    sampleInDetailTemp.BatchNo = tempProduct.BatchNo;
                    sampleInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    sampleInDetailTempList.Add(sampleInDetailTemp);
                }
                return sampleInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
        }


        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo, int sampleOutType)
        {
            // Create query 
            IQueryable qryResult = context.SampleInHeaders.Where("DocumentStatus == @0 AND DocumentId == @1 AND SampleOutType == @2", 1, autoGenerateInfo.DocumentID, sampleOutType);

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
        /// Get sample in destails for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetSamleInDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo, int sampleOutType)
        {

            StringBuilder selectFields = new StringBuilder();
            var query = context.SampleInHeaders.Where("DocumentStatus == @0 AND DocumentId ==@1 AND SampleOutType == @2", 1, autoGenerateInfo.DocumentID, sampleOutType);

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
                                       FieldString5 = h.Remark,                                       
                                       FieldString6 = l.LocationCode + " " + l.LocationName
                                   }).ToArray();

            return queryResult.ToDataTable();

        }


        /// <summary>
        /// Get Sample In details for report
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        /// <param name="documentId"></param>
        /// <param name="transactionType" (1-->Inventry, 2-->Logistic)></param>
        /// <returns></returns>
        public DataTable GetSampleInTransactionDataTable(string documentNo, int documentStatus, int documentId, int transactionType)
        {
            string lookupType = ((int)LookUpReference.ModuleType).ToString();

            var query = transactionType.Equals(1) ?
                        (
                        from ph in context.SampleInHeaders
                        join pd in context.SampleInDetails on ph.SampleInHeaderID equals pd.SampleInHeaderID
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
                                FieldString8 = "",
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
                        (from ph in context.SampleInHeaders
                        join pd in context.SampleInDetails on ph.SampleInHeaderID equals pd.SampleInHeaderID
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
                                FieldString8 = "",
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
                            });
            return query.AsEnumerable().ToDataTable();

        }

        #endregion
    }
}
