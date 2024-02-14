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
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using MoreLinq;
using System.Collections;
using System.Data.Entity;


namespace Service
{
    public class LgsTransferOfGoodsService
    {
        public static bool isRecallMAN;

        ERPDbContext context = new ERPDbContext();

        public LgsTransferDetailsTemp getTransferDetailTemp(List<LgsTransferDetailsTemp> LgsTransferDetailsTemp, LgsProductMaster lgsProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            LgsTransferDetailsTemp lgsTransferDetailTemp = new LgsTransferDetailsTemp();
            decimal defaultConvertFactor = 1;
            var transferDetailTemp = (from pm in context.LgsProductMasters
                                      join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
                                      join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID) && psm.LocationID.Equals(locationID)
                                      && psm.BalanceQty != 0
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
                                          pm.IsBatch,
                                          psm.BatchNo,
                                          psm.LgsProductBatchNoExpiaryDetailID
                                      }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(lgsProductMaster.UnitOfMeasureID))
            {
                transferDetailTemp = (dynamic)null;

                transferDetailTemp = (from pm in context.LgsProductMasters
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
                                          puc.ConvertFactor,
                                          pm.IsBatch,
                                          psm.BatchNo,
                                          psm.LgsProductBatchNoExpiaryDetailID

                                      }).ToArray();
            }

            foreach (var tempProduct in transferDetailTemp)
            {
                
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(false))
                    lgsTransferDetailTemp = LgsTransferDetailsTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    //invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsTransferDetailTemp = LgsTransferDetailsTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                    //invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    lgsTransferDetailTemp = LgsTransferDetailsTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsTransferDetailTemp == null)
                {
                    lgsTransferDetailTemp = new LgsTransferDetailsTemp();
                    lgsTransferDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
                    lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsTransferDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsTransferDetailTemp.UnitOfMeasure =  tempProduct.UnitOfMeasureName;
                    lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                    lgsTransferDetailTemp.BatchNo = tempProduct.BatchNo;
                    lgsTransferDetailTemp.LgsProductBatchNoExpiaryDetailID = tempProduct.LgsProductBatchNoExpiaryDetailID;
                    lgsTransferDetailTemp.BatchNoExpiaryDetailID = tempProduct.LgsProductBatchNoExpiaryDetailID;
                }
            }
            return lgsTransferDetailTemp;
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
        
        public List<LgsProductBatchNoTemp> getBatchNoDetail(LgsProductMaster lgsProductMaster, int locationID)
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

       

        public List<LgsProductBatchNoTemp> getExpiryDetail(LgsProductMaster lgsProductMaster, string batchNo)
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


        public LgsProductBatchNoExpiaryDetail GetLgsProductBatchNoByBatchNo(string batchNo)
        {
            return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public LgsTransferNoteHeader GetPausedTogHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsTransferNoteHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
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


        public LgsTransferNoteHeader GetPausedTOGHeaderByDocumentNo(int documentID, string documentNo)
        {
            return context.LgsTransferNoteHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        public bool Save(LgsTransferNoteHeader lgsTransferNoteHeader, List<LgsTransferDetailsTemp> lgsTransferDetailsTemp, List<InvProductSerialNoTemp> lgsProductSerialNoTemp, out string newDocumentNo, string formName = "")
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (lgsTransferNoteHeader.DocumentStatus.Equals(1))
                {
                    LgsTransferOfGoodsService LgsTransferOfGoodsService = new LgsTransferOfGoodsService();
                    LocationService locationService = new LocationService();
                    lgsTransferNoteHeader.DocumentNo = LgsTransferOfGoodsService.GetDocumentNo(formName, lgsTransferNoteHeader.LocationID, locationService.GetLocationsByID(lgsTransferNoteHeader.LocationID).LocationCode, lgsTransferNoteHeader.DocumentID, false).Trim();

                }

                newDocumentNo = lgsTransferNoteHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (lgsTransferNoteHeader.LgsTransferNoteHeaderID.Equals(0))
                    context.LgsTransferNoteHeaders.Add(lgsTransferNoteHeader);
                else
                    context.Entry(lgsTransferNoteHeader).State = EntityState.Modified;

                context.SaveChanges();


                if (isRecallMAN)
                {
                    if (lgsTransferNoteHeader.DocumentStatus.Equals(1))
                    {
                        foreach (LgsTransferDetailsTemp tmp in lgsTransferDetailsTemp)
                        {
                            context.LgsMaterialAllocationDetails.Update(ad => ad.ProductID.Equals(tmp.ProductID) && ad.LgsMaterialAllocationHeaderID.Equals(lgsTransferNoteHeader.ReferenceDocumentID),ad => new LgsMaterialAllocationDetail { BalanceQty = ad.BalanceQty - tmp.Qty });
                        }
                    }
                }


                context.Set<LgsTransferNoteDetail>().Delete(context.LgsTransferNoteDetails.Where(p => p.TransferNoteHeaderID.Equals(lgsTransferNoteHeader.LgsTransferNoteHeaderID) && p.LocationID.Equals(lgsTransferNoteHeader.LocationID) && p.DocumentID.Equals(lgsTransferNoteHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var lgsTransferNoteDetailSaveQuery = (from pd in lgsTransferDetailsTemp
                                                      select new
                                                      {
                                                          LgsTransferNoteDetailID = 0,
                                                          TransferNoteDetailID = 0,
                                                          TransferNoteHeaderID = lgsTransferNoteHeader.LgsTransferNoteHeaderID,
                                                          CompanyID = lgsTransferNoteHeader.CompanyID,
                                                          LocationID = lgsTransferNoteHeader.LocationID,
                                                          CostCentreID = lgsTransferNoteHeader.CostCentreID,
                                                          DocumentID = lgsTransferNoteHeader.DocumentID,
                                                          DocumentNo = lgsTransferNoteHeader.DocumentNo,
                                                          DocumentDate = Common.ConvertStringToDateTime(lgsTransferNoteHeader.DocumentDate.ToString()),
                                                          LineNo = pd.LineNo,
                                                          ProductID = pd.ProductID,
                                                          BatchNo = (pd.IsBatch == true ? pd.BatchNo : string.Empty),
                                                          BatchExpiryDate = Common.ConvertStringToDateTime(pd.ExpiryDate.ToString()),
                                                          AvgCost = pd.AverageCost,
                                                          CostPrice = pd.CostPrice,
                                                          SellingPrice = pd.SellingPrice,
                                                          //PackID Removed --> Replaces BatchNoExpiaryDetailID
                                                          Qty = pd.Qty,
                                                          OrderQty = pd.Qty,
                                                          CurrentQty = 0,
                                                          QtyConvertFactor = (pd.Qty * pd.ConvertFactor),
                                                          BaseUnitID = pd.BaseUnitID,
                                                          ConvertFactor = pd.ConvertFactor,
                                                          UnitOfMeasureID = pd.UnitOfMeasureID,
                                                          GrossAmount = pd.NetAmount,
                                                          NetAmount = pd.NetAmount,
                                                          ToLocationID = lgsTransferNoteHeader.ToLocationID,
                                                          DocumentStatus = lgsTransferNoteHeader.DocumentStatus,
                                                          IsBatch = pd.IsBatch,
                                                          GroupOfCompanyID = Common.GroupOfCompanyID,
                                                          CreatedUser = lgsTransferNoteHeader.CreatedUser,
                                                          CreatedDate = Common.ConvertStringToDateTime(lgsTransferNoteHeader.CreatedDate.ToString()),
                                                          ModifiedUser = lgsTransferNoteHeader.ModifiedUser,
                                                          ModifiedDate = Common.ConvertStringToDateTime(lgsTransferNoteHeader.ModifiedDate.ToString()),
                                                          DataTransfer = lgsTransferNoteHeader.DataTransfer,
                                                          BatchNoExpiaryDetailID = pd.BatchNoExpiaryDetailID
                                                      }).ToList();


               CommonService.BulkInsert(Common.LINQToDataTable(lgsTransferNoteDetailSaveQuery), "LgsTransferNoteDetail");                


                context.SaveChanges();


                var parameter = new DbParameter[] 
                    { 

                        new System.Data.SqlClient.SqlParameter { ParameterName ="@lgsTransferNoteHeaderID", Value=lgsTransferNoteHeader.LgsTransferNoteHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromLocationID", Value=lgsTransferNoteHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToLocationID", Value=lgsTransferNoteHeader.ToLocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=lgsTransferNoteHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=lgsTransferNoteHeader.DocumentStatus},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentDocumentID", Value=lgsTransferNoteHeader.ReferenceDocumentDocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentID", Value=lgsTransferNoteHeader.ReferenceDocumentID},
                        
                    };


                if (CommonService.ExecuteStoredProcedure("spLgsTransferNoteSave", parameter))
                {
                    transaction.Complete();

                    return true;
                }
                else
                    return false;
            }

        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.LgsTransferNoteHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }


        public LgsTransferNoteHeader GetTOGByDocumentNo(string documentNo)
        {
            return context.LgsTransferNoteHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<LgsTransferDetailsTemp> getPausedTOGDetail(LgsTransferNoteHeader lgsTransferNoteHeader)
        {
            LgsTransferDetailsTemp lgsTransferDetailsTemp;

            List<LgsTransferDetailsTemp> lgsTransferDetailsTempList = new List<LgsTransferDetailsTemp>();

           
            var transferTemp = (from pm in context.LgsProductMasters
                                    join qd in context.LgsTransferNoteDetails on pm.LgsProductMasterID equals qd.ProductID
                                    join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                    where qd.LocationID.Equals(lgsTransferNoteHeader.LocationID) && qd.CompanyID.Equals(lgsTransferNoteHeader.CompanyID)
                                    && qd.DocumentID.Equals(lgsTransferNoteHeader.DocumentID) && qd.TransferNoteHeaderID.Equals(lgsTransferNoteHeader.LgsTransferNoteHeaderID)
                                    select new
                                    {
                                        qd.SellingPrice,
                                        qd.CostPrice,
                                        qd.NetAmount,
                                        qd.ConvertFactor,
                                        qd.LineNo,
                                        qd.BatchExpiryDate,
                                        qd.BatchNo,
                                        pm.ProductCode,
                                        qd.ProductID,
                                        pm.ProductName,
                                        qd.Qty,
                                        qd.UnitOfMeasureID,
                                        uc.UnitOfMeasureName,
                                        qd.IsBatch

                                    }).ToArray();

            foreach (var tempProduct in transferTemp)
            {

                lgsTransferDetailsTemp = new LgsTransferDetailsTemp();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

                lgsTransferDetailsTemp.SellingPrice = tempProduct.SellingPrice;
                lgsTransferDetailsTemp.CostPrice = tempProduct.CostPrice;
                lgsTransferDetailsTemp.ConvertFactor = tempProduct.ConvertFactor;
               
                lgsTransferDetailsTemp.LineNo = tempProduct.LineNo;
                lgsTransferDetailsTemp.NetAmount = tempProduct.NetAmount;
                
                lgsTransferDetailsTemp.ProductCode = tempProduct.ProductCode;
                lgsTransferDetailsTemp.ProductID = tempProduct.ProductID;
                lgsTransferDetailsTemp.ProductName = tempProduct.ProductName;
                lgsTransferDetailsTemp.Qty = tempProduct.Qty;
                lgsTransferDetailsTemp.BatchNo = tempProduct.BatchNo;
                lgsTransferDetailsTemp.ExpiryDate = tempProduct.BatchExpiryDate;
                lgsTransferDetailsTemp.UnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(tempProduct.UnitOfMeasureID).UnitOfMeasureName;
                lgsTransferDetailsTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsTransferDetailsTemp.IsBatch = tempProduct.IsBatch;

                lgsTransferDetailsTempList.Add(lgsTransferDetailsTemp);
            }
            return lgsTransferDetailsTempList.OrderBy(pd => pd.LineNo).ToList();
            
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

        public List<LgsTransferDetailsTemp> getUpdateLgsTransferDetailTemp(List<LgsTransferDetailsTemp> lgsTransferDetail, LgsTransferDetailsTemp lgsTogDetails, LgsProductMaster lgsProductMaster)
        {
            LgsTransferDetailsTemp lgsTransferDetailsTemp = new LgsTransferDetailsTemp();

            if (lgsProductMaster.IsExpiry)
                lgsTransferDetailsTemp = lgsTransferDetail.Where(p => p.ProductID == lgsTogDetails.ProductID && p.UnitOfMeasureID == lgsTogDetails.UnitOfMeasureID && p.ExpiryDate == lgsTogDetails.ExpiryDate).FirstOrDefault();
            else
                lgsTransferDetailsTemp = lgsTransferDetail.Where(p => p.ProductID == lgsTogDetails.ProductID && p.UnitOfMeasureID == lgsTogDetails.UnitOfMeasureID).FirstOrDefault();

            if (lgsTransferDetailsTemp == null || lgsTransferDetailsTemp.LineNo.Equals(0))
            {
                if (lgsTransferDetail.Count.Equals(0))
                    lgsTogDetails.LineNo = 1;
                else
                    lgsTogDetails.LineNo = lgsTransferDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsTransferDetail.Remove(lgsTransferDetailsTemp);
                lgsTogDetails.LineNo = lgsTransferDetailsTemp.LineNo;

            }

            lgsTransferDetail.Add(lgsTogDetails);

            return lgsTransferDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsTransferDetailsTemp> GetUpdateLgsTransferDetailTempForBarcodeScan(List<LgsTransferDetailsTemp> lgsTransferDetail, LgsTransferDetailsTemp lgsTogDetails)
        {
            LgsTransferDetailsTemp lgsTransferDetailsTemp = new LgsTransferDetailsTemp();

            lgsTransferDetailsTemp = lgsTransferDetail.Where(p => p.ProductID == lgsTogDetails.ProductID && p.UnitOfMeasureID == lgsTogDetails.UnitOfMeasureID && p.BatchNo == lgsTogDetails.BatchNo).FirstOrDefault();

            if (lgsTransferDetailsTemp == null || lgsTransferDetailsTemp.LineNo.Equals(0))
            {
                if (lgsTransferDetail.Count.Equals(0))
                    lgsTogDetails.LineNo = 1;
                else
                    lgsTogDetails.LineNo = lgsTransferDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsTransferDetail.Remove(lgsTransferDetailsTemp);
                lgsTogDetails.LineNo = lgsTransferDetailsTemp.LineNo;
            }

            lgsTransferDetail.Add(lgsTogDetails);

            return lgsTransferDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsTransferDetailsTemp> GetDeleteLgsTransferDetailsTemp(List<LgsTransferDetailsTemp> lgsTransferTempDetail, LgsTransferDetailsTemp lgsTransferDetails, List<InvProductSerialNoTemp> lgsProductSerialNoListToDelete)
        {
            LgsTransferDetailsTemp lgsTransferDetailsTemp = new LgsTransferDetailsTemp();
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            //lgsTransferDetailsTemp = lgsTransferTempDetail.Where(p => p.ProductID == lgsTransferDetails.ProductID && p.UnitOfMeasureID == lgsTransferDetails.UnitOfMeasureID && p.ExpiryDate.Equals(lgsTransferDetails.ExpiryDate)).FirstOrDefault();
            lgsTransferDetailsTemp = lgsTransferTempDetail.Where(p => p.ProductID == lgsTransferDetails.ProductID && p.UnitOfMeasureID == lgsTransferDetails.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (lgsTransferDetailsTemp != null)
            {
                lgsTransferTempDetail.Remove(lgsTransferDetailsTemp);
                removedLineNo = lgsTransferDetailsTemp.LineNo;

            }

            lgsTransferTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            //invProductSerialNoTempList = lgsProductSerialNoListToDelete.Where(p => p.ProductID == lgsTransferDetails.ProductID && p.UnitOfMeasureID == lgsTransferDetails.UnitOfMeasureID && p.ExpiryDate.Equals(lgsTransferDetails.ExpiryDate)).ToList();

            foreach (InvProductSerialNoTemp LgsProductSerialNoTempDelete in lgsProductSerialNoTempList)
            {
                lgsProductSerialNoListToDelete.Remove(LgsProductSerialNoTempDelete);
            }

            //lgsProductSerialNoList = lgsProductSerialNoListToDelete;

            return lgsTransferTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }


        //public List<LgsTransferDetailsTemp> GetGRNDetails(LgsPurchaseHeader lgsPurchaseHeader, int documentID)
        //{
        //    LgsTransferDetailsTemp lgsTransferDetailTemp;

        //    List<LgsTransferDetailsTemp> lgsTransferDetailTempList = new List<LgsTransferDetailsTemp>();


        //    var transferDetailTemp = (from pm in context.LgsProductMasters
        //                              join pd in context.LgsPurchaseDetails on pm.LgsProductMasterID equals pd.ProductID
        //                              join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
        //                              join ph in context.LgsPurchaseHeaders on pd.LgsPurchaseHeaderID equals ph.LgsPurchaseHeaderID
        //                              where pd.LocationID.Equals(lgsPurchaseHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseHeader.CompanyID)
        //                              && pd.DocumentID.Equals(lgsPurchaseHeader.DocumentID) && pd.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ph.DocumentID.Equals(documentID)
        //                              select new
        //                              {
        //                                  pd.AvgCost,
        //                                  pd.ConvertFactor,
        //                                  pd.CostPrice,
        //                                  pd.CurrentQty,
        //                                  pd.DiscountAmount,
        //                                  pd.DiscountPercentage,
        //                                  ExpiryDate = pd.ExpiryDate,
        //                                  pd.FreeQty,
        //                                  pd.GrossAmount,
        //                                  pd.LineNo,
        //                                  pd.LocationID,
        //                                  pd.NetAmount,
        //                                  pd.OrderQty,
        //                                  pm.ProductCode,
        //                                  pd.ProductID,
        //                                  pm.ProductName,
        //                                  pd.BalanceQty,
        //                                  pd.SellingPrice,
        //                                  pd.SubTotalDiscount,
        //                                  pd.TaxAmount1,
        //                                  pd.TaxAmount2,
        //                                  pd.TaxAmount3,
        //                                  pd.TaxAmount4,
        //                                  pd.TaxAmount5,
        //                                  pd.UnitOfMeasureID,
        //                                  uc.UnitOfMeasureName,
        //                                  ph.BatchNo,
        //                                  pd.IsBatch
        //                              }).ToArray();

        //    foreach (var tempProduct in transferDetailTemp)
        //    {

        //        lgsTransferDetailTemp = new LgsTransferDetailsTemp();
        //        if (tempProduct.BalanceQty > 0)
        //        {
        //            lgsTransferDetailTemp.AverageCost = tempProduct.AvgCost;
        //            lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
        //            lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
        //            lgsTransferDetailTemp.CurrentQty = tempProduct.CurrentQty;
        //            lgsTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
        //            //invTransferDetailTemp.FreeQty = tempProduct.FreeQty;
        //            //invTransferDetailTemp.GrossAmount = tempProduct.GrossAmount;
        //            lgsTransferDetailTemp.LineNo = tempProduct.LineNo;
        //            lgsTransferDetailTemp.FromLocationID = tempProduct.LocationID;
        //            // invTransferDetailTemp.NetAmount = tempProduct.NetAmount;
        //            lgsTransferDetailTemp.NetAmount = tempProduct.BalanceQty * tempProduct.CostPrice;
        //            lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
        //            lgsTransferDetailTemp.ProductID = tempProduct.ProductID;
        //            lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
        //            lgsTransferDetailTemp.Qty = (tempProduct.BalanceQty + tempProduct.FreeQty);
        //            lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
        //            lgsTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
        //            lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
        //            lgsTransferDetailTemp.BatchNo = tempProduct.BatchNo;
        //            lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;

        //            lgsTransferDetailTempList.Add(lgsTransferDetailTemp);
        //        }

        //    }
        //    return lgsTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        //}


        public List<LgsTransferDetailsTemp> GetGRNDetails(LgsPurchaseHeader lgsPurchaseHeader, int documentID)
        {
            LgsTransferDetailsTemp lgsTransferDetailTemp;

            List<LgsTransferDetailsTemp> lgsTransferDetailTempList = new List<LgsTransferDetailsTemp>();


            var transferDetailTemp = (from pm in context.LgsProductMasters
                                      join pd in context.LgsPurchaseDetails on pm.LgsProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.LgsPurchaseHeaders on pd.LgsPurchaseHeaderID equals ph.LgsPurchaseHeaderID
                                      where pd.LocationID.Equals(lgsPurchaseHeader.LocationID) && pd.CompanyID.Equals(lgsPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(lgsPurchaseHeader.DocumentID) && pd.LgsPurchaseHeaderID.Equals(lgsPurchaseHeader.PurchaseHeaderID) && ph.DocumentID.Equals(documentID)
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
                                          ph.BatchNo,
                                          pd.IsBatch,
                                          pd.BaseUnitID,
                                      }).ToArray();

            foreach (var tempProduct in transferDetailTemp)
            {

                lgsTransferDetailTemp = new LgsTransferDetailsTemp();
                if (tempProduct.BalanceQty > 0)
                {
                    lgsTransferDetailTemp.AverageCost = tempProduct.AvgCost;
                    lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsTransferDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    lgsTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    //invTransferDetailTemp.FreeQty = tempProduct.FreeQty;
                    //invTransferDetailTemp.GrossAmount = tempProduct.GrossAmount;
                    lgsTransferDetailTemp.LineNo = tempProduct.LineNo;
                    lgsTransferDetailTemp.FromLocationID = tempProduct.LocationID;
                    // invTransferDetailTemp.NetAmount = tempProduct.NetAmount;
                    lgsTransferDetailTemp.NetAmount = tempProduct.BalanceQty * tempProduct.CostPrice;
                    lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsTransferDetailTemp.ProductID = tempProduct.ProductID;
                    lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
                    lgsTransferDetailTemp.Qty = (tempProduct.BalanceQty + tempProduct.FreeQty);
                    lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsTransferDetailTemp.BatchNo = tempProduct.BatchNo;
                    lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                    lgsTransferDetailTemp.BaseUnitID = tempProduct.BaseUnitID;

                    lgsTransferDetailTempList.Add(lgsTransferDetailTemp);
                }

            }
            return lgsTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        //public List<LgsTransferDetailsTemp> GetMANDetails(LgsMaterialAllocationHeader lgsMaterialAllocationHeader, int documentID)
        //{
        //    LgsTransferDetailsTemp lgsTransferDetailTemp;

        //    List<LgsTransferDetailsTemp> lgsTransferDetailTempList = new List<LgsTransferDetailsTemp>();


        //    var transferDetailTemp = (from pm in context.LgsProductMasters
        //                              join pd in context.LgsMaterialAllocationDetails on pm.LgsProductMasterID equals pd.ProductID
        //                              join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
        //                              join ph in context.LgsMaterialAllocationHeaders on pd.LgsMaterialAllocationHeaderID equals ph.LgsMaterialAllocationHeaderID
        //                              where pd.LocationID.Equals(lgsMaterialAllocationHeader.LocationID) && pd.CompanyID.Equals(lgsMaterialAllocationHeader.CompanyID)
        //                              && pd.DocumentID.Equals(lgsMaterialAllocationHeader.DocumentID) && pd.LgsMaterialAllocationHeaderID.Equals(lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID) && ph.DocumentID.Equals(documentID)
        //                              select new
        //                              {
        //                                  pd.ConvertFactor,
        //                                  pd.CostPrice,
        //                                  pd.CurrentQty,
        //                                  pd.GrossAmount,
        //                                  pd.LineNo,
        //                                  pd.LocationID,                                       
        //                                  pd.OrderQty,
        //                                  pm.ProductCode,
        //                                  pd.ProductID,
        //                                  pm.ProductName,
        //                                  pd.BalanceQty,
        //                                  pd.SellingPrice,
        //                                  pd.UnitOfMeasureID,
        //                                  uc.UnitOfMeasureName,
        //                                  pm.IsBatch,
        //                                  pd.RequestLocationID
        //                              }).ToArray();

        //    foreach (var tempProduct in transferDetailTemp)
        //    {

        //        lgsTransferDetailTemp = new LgsTransferDetailsTemp();
        //        if (tempProduct.BalanceQty > 0)
        //        {
        //            lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
        //            lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
        //            lgsTransferDetailTemp.CurrentQty = tempProduct.CurrentQty;
        //            lgsTransferDetailTemp.LineNo = tempProduct.LineNo;
        //            lgsTransferDetailTemp.FromLocationID = tempProduct.LocationID;
        //            lgsTransferDetailTemp.NetAmount = tempProduct.BalanceQty * tempProduct.CostPrice;
        //            lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
        //            lgsTransferDetailTemp.ProductID = tempProduct.ProductID;
        //            lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
        //            lgsTransferDetailTemp.Qty = (tempProduct.BalanceQty);
        //            lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
        //            lgsTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
        //            lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
        //            lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;
        //            lgsTransferDetailTemp.RequestLocationID = tempProduct.RequestLocationID;

        //            lgsTransferDetailTempList.Add(lgsTransferDetailTemp);
        //        }

        //    }
        //    return lgsTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        //}

        public List<LgsTransferDetailsTemp> GetMANDetails(LgsMaterialAllocationHeader lgsMaterialAllocationHeader, int documentID)
        {
            LgsTransferDetailsTemp lgsTransferDetailTemp;

            List<LgsTransferDetailsTemp> lgsTransferDetailTempList = new List<LgsTransferDetailsTemp>();


            var transferDetailTemp = (from pm in context.LgsProductMasters
                                      join pd in context.LgsMaterialAllocationDetails on pm.LgsProductMasterID equals pd.ProductID
                                      join uc in context.UnitOfMeasures on pd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                      join ph in context.LgsMaterialAllocationHeaders on pd.LgsMaterialAllocationHeaderID equals ph.LgsMaterialAllocationHeaderID
                                      where pd.LocationID.Equals(lgsMaterialAllocationHeader.LocationID) && pd.CompanyID.Equals(lgsMaterialAllocationHeader.CompanyID)
                                      && pd.DocumentID.Equals(lgsMaterialAllocationHeader.DocumentID) && pd.LgsMaterialAllocationHeaderID.Equals(lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID) && ph.DocumentID.Equals(documentID)
                                      select new
                                      {
                                          pd.ConvertFactor,
                                          pd.CostPrice,
                                          pd.CurrentQty,
                                          pd.GrossAmount,
                                          pd.LineNo,
                                          pd.LocationID,
                                          pd.OrderQty,
                                          pm.ProductCode,
                                          pd.ProductID,
                                          pm.ProductName,
                                          pd.BalanceQty,
                                          pd.SellingPrice,
                                          pd.UnitOfMeasureID,
                                          uc.UnitOfMeasureName,
                                          pm.IsBatch,
                                          pd.RequestLocationID,
                                      }).ToArray();

            foreach (var tempProduct in transferDetailTemp)
            {
                lgsTransferDetailTemp = new LgsTransferDetailsTemp();
                if (tempProduct.BalanceQty > 0)
                {
                    lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsTransferDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    lgsTransferDetailTemp.LineNo = tempProduct.LineNo;
                    lgsTransferDetailTemp.FromLocationID = tempProduct.LocationID;
                    lgsTransferDetailTemp.NetAmount = tempProduct.BalanceQty * tempProduct.CostPrice;
                    lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsTransferDetailTemp.ProductID = tempProduct.ProductID;
                    lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
                    lgsTransferDetailTemp.Qty = (tempProduct.BalanceQty);
                    lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                    lgsTransferDetailTemp.RequestLocationID = tempProduct.RequestLocationID;
                    
                    string batchNo = context.LgsProductBatchNoExpiaryDetails.Where(bd => bd.ProductID.Equals(tempProduct.ProductID) && bd.BalanceQty != 0).Select(bd => bd.BatchNo).FirstOrDefault();

                    lgsTransferDetailTemp.BatchNo = batchNo;

                    lgsTransferDetailTempList.Add(lgsTransferDetailTemp);
                }
            }
            return lgsTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetTransferofGoodsTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            var query = (
                           from ph in context.LgsTransferNoteHeaders
                           join pd in context.LgsTransferNoteDetails on ph.LgsTransferNoteHeaderID equals pd.TransferNoteHeaderID
                           join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                           join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                           join lFrom in context.Locations on ph.LocationID equals lFrom.LocationID
                           join lTo in context.Locations on ph.ToLocationID equals lTo.LocationID
                           join re in context.LgsTransferTypes on ph.TransferTypeID equals re.LgsTransferTypeID
                           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)
                           select
                               new
                               {
                                   FieldString1 = ph.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                   FieldString3 = "",
                                   FieldString4 = ph.Remark,
                                   FieldString5 = "", 
                                   FieldString6 = "",
                                   FieldString7 = "",
                                   FieldString8 = "",
                                   FieldString9 = ph.ReferenceNo,
                                   FieldString10 = "",
                                   FieldString11 = lFrom.LocationCode + "  " + lFrom.LocationName,
                                   FieldString12 = lTo.LocationCode + "  " + lTo.LocationName,
                                   FieldString13 = pm.ProductCode,
                                   FieldString14 = pm.ProductName,
                                   FieldString15 = um.UnitOfMeasureName,
                                   FieldString16 = "",
                                   FieldString17 = pd.OrderQty,
                                   FieldString18 = "",
                                   FieldString19 = pd.CostPrice,
                                   FieldString20 = "",
                                   FieldString21 = "",
                                   FieldString22 = pd.NetAmount,
                                   FieldString23 = ph.GrossAmount,
                                   FieldString24 = ph.NetAmount,
                                   FieldString25 = "",
                                   FieldString26 = "",
                                   FieldString27 = "",
                                   FieldString28 = "",
                                   FieldString29 = "",
                                   FieldString30 = ph.CreatedUser
                               });
            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticTransferOfGoodsNote");

            // Create query 
            IQueryable qryResult = null;
            if (string.Equals(autoGenerateInfo.FormName, "LgsRptTransferRegister"))
            {
                qryResult = context.LgsTransferNoteDetails.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "LocationID":
                        qryResult = null;
                        qryResult = context.LgsTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "ToLocationID":
                        qryResult = null;
                        qryResult = context.LgsTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                                  "LocationID",
                                                  "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                     "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "TransferTypeID":
                        qryResult = null;
                        qryResult = context.LgsTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.LgsTransferTypes, reportDataStruct.DbColumnName.Trim(),
                                                  "LgsTransferTypeID",
                                                  "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                     "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsProductMasterID":
                        qryResult = qryResult.Join(context.LgsProductMasters, "ProductID",
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                  "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                         .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsDepartmentID":
                        qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID",
                                            "new(inner.DepartmentID)")
                                            .Join(context.LgsDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "LgsCategoryID":
                        qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID",
                                            "new(inner.CategoryID)")
                                            .Join(context.LgsCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "LgsSubCategoryID":
                        qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID",
                                            "new(inner.SubCategoryID)")
                                            .Join(context.LgsSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "LgsSubCategory2ID":
                        qryResult = qryResult.Join(context.LgsProductMasters, "ProductID", "LgsProductMasterID",
                                            "new(inner.SubCategory2ID)")
                                            .Join(context.LgsSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(),
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

            }
            else
            {
                qryResult = context.LgsTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);

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

                    case "ToLocationID":
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                                  "LocationID",
                                                  "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                     "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "TransferTypeID":
                        qryResult = qryResult.Join(context.LgsTransferTypes, reportDataStruct.DbColumnName.Trim(),
                                                  "LgsTransferTypeID",
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

        /// <summary>
        /// Get purchase details for report generator
        /// </summary>
        /// <returns></returns>
        public DataTable GetTransferOfGoodsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.LgsTransferNoteHeaders.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                                     select qr);
                            break;

                        case "ToLocationID":
                            query = (from qr in query
                                     join jt in context.Locations.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.ToLocationID equals jt.LocationID
                                     select qr);
                            break;

                        case "TransferTypeID":
                            query = (from qr in query
                                     join jt in context.LgsTransferTypes.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.TransferTypeID equals jt.LgsTransferTypeID
                                     select qr);
                            break;
                        default:
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() +
                                    " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                    int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }
            }

            var queryResult = query.AsNoTracking().AsEnumerable();
            return (from h in queryResult
                    join lFrom in context.Locations on h.LocationID equals lFrom.LocationID
                    join lTo in context.Locations on h.ToLocationID equals lTo.LocationID
                    join t in context.LgsTransferTypes on h.TransferTypeID equals t.LgsTransferTypeID
                    select
                        new
                        {
                            FieldString1 = reportDataStructList[0].IsSelectionField == true ? t.TransferType.Trim() : "",
                            FieldString2 = reportDataStructList[1].IsSelectionField == true ? lFrom.LocationCode + " " + lFrom.LocationName : "",
                            FieldString3 = reportDataStructList[2].IsSelectionField == true ? lTo.LocationCode + " " + lTo.LocationName : "",
                            FieldString4 = reportDataStructList[3].IsSelectionField == true ? h.CreatedUser : "",
                            FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.DocumentNo : "",
                            FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                            FieldString7 = reportDataStructList[6].IsSelectionField == true ? h.ReferenceNo : "", //h.Remark : "",
                            FieldDecimal1 = reportDataStructList[7].IsSelectionField == true ? (from d in context.LgsTransferNoteDetails
                                                                                                where
                                                                                                  d.TransferNoteHeaderID == h.LgsTransferNoteHeaderID
                                                                                                select new
                                                                                                {
                                                                                                    Column1 = ((System.Decimal?)d.Qty ?? (System.Decimal?)0)
                                                                                                }).Sum(p => p.Column1) : 0, // Qty
                            FieldDecimal2 = reportDataStructList[8].IsSelectionField == true ? h.NetAmount : 0, // Salling Value

                        }).ToArray().ToDataTable();

        }

        public List<LgsMinusProductDetailsTemp> getMinusProductDetails(List<LgsTransferDetailsTemp> lgsTransferDetailsTemp, int locationID)
        {
            List<LgsMinusProductDetailsTemp> lgsMinusProductDetailsTempList = new List<LgsMinusProductDetailsTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsMinusProducts = new List<LgsProductBatchNoExpiaryDetail>();

            LgsProductMasterService lgsProductMasterService = new LgsProductMasterService();
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

            foreach (var tempProduct in lgsTransferDetailsTemp)
            {
                //lgsMinusProducts = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(tempProduct.ProductID) && ps.BatchNo.Equals(tempProduct.BatchNo) && ps.LocationID.Equals(locationID) && ps.BalanceQty < (tempProduct.Qty * tempProduct.ConvertFactor)).ToList();
                lgsMinusProducts = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(tempProduct.ProductID) && ps.LgsProductBatchNoExpiaryDetailID.Equals(tempProduct.LgsProductBatchNoExpiaryDetailID) && ps.BatchNo.Equals(tempProduct.BatchNo) && ps.LocationID.Equals(locationID) && ps.BalanceQty < (tempProduct.Qty * tempProduct.ConvertFactor)).ToList();

                if (lgsMinusProducts.Count> 0)
                {
                    LgsMinusProductDetailsTemp lgsMinusProductDetailsTemp = new LgsMinusProductDetailsTemp();

                    lgsMinusProductDetailsTemp.LineNo = tempProduct.LineNo;
                    lgsMinusProductDetailsTemp.ProductID = tempProduct.ProductID;
                    lgsMinusProductDetailsTemp.ProductCode = lgsProductMasterService.GetProductDetailsByID(tempProduct.ProductID).ProductCode;
                    lgsMinusProductDetailsTemp.ProductName = lgsProductMasterService.GetProductDetailsByID(tempProduct.ProductID).ProductName;
                    lgsMinusProductDetailsTemp.Qty = tempProduct.Qty;
                    lgsMinusProductDetailsTemp.UnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(tempProduct.UnitOfMeasureID).UnitOfMeasureName;

                    lgsMinusProductDetailsTempList.Add(lgsMinusProductDetailsTemp);
                }
            }
            return lgsMinusProductDetailsTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public string[] GetAllProductCodes()
        {
            //List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();
            
            //List<string> ProductCodeList = context.InvProductMasters.Join(context.InvProductBatchNoExpiaryDetails ,
            //                                s => new { s.InvProductMasterID, s.UnitOfMeasureID },
            //           h => new { h.ProductID, h.UnitOfMeasureID },
            //           (s, h) => s);
            
            //return ProductCodeList.ToArray();
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

            //List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            List<string> ProductNameList = context.LgsProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.LgsProductBatchNoExpiaryDetails.Where (b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.LgsProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();
            
            return ProductNameList.ToArray();

        }

        public LgsTransferDetailsTemp GetTransferDetailTempForBarcodeScan(long barcode) 
        {
            //List<InvTransferDetailsTemp> rtnList = new List<InvTransferDetailsTemp>();
            LgsTransferDetailsTemp lgsTransferDetailTemp = null;

            LgsBatchNoExpiaryDetailService lgsBatchNoExpiaryDetailService = new LgsBatchNoExpiaryDetailService();
            LgsProductBatchNoExpiaryDetail lgsProductBatchNoExpiaryDetail = new LgsProductBatchNoExpiaryDetail();

            lgsProductBatchNoExpiaryDetail = lgsBatchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcode(barcode);

            if (lgsProductBatchNoExpiaryDetail != null)
            {
                decimal defaultConvertFactor = 1;

                var transferDetailTemp = (from pm in context.LgsProductMasters
                                          join bed in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals bed.ProductID
                                          join um in context.UnitOfMeasures on bed.UnitOfMeasureID equals um.UnitOfMeasureID
                                          where bed.BarCode == barcode && bed.BalanceQty > 0
                                          select new
                                          {
                                              bed.ProductID,
                                              pm.ProductCode,
                                              pm.ProductName,
                                              bed.UnitOfMeasureID,
                                              um.UnitOfMeasureName,
                                              bed.CostPrice,
                                              bed.SellingPrice,
                                              pm.AverageCost,
                                              ConvertFactor = defaultConvertFactor,
                                              bed.ExpiryDate,
                                              bed.BatchNo,
                                              pm.IsBatch
                                          });

                foreach (var tempProduct in transferDetailTemp)
                {
                    lgsTransferDetailTemp = new LgsTransferDetailsTemp();

                    lgsTransferDetailTemp.ProductID = tempProduct.ProductID;
                    lgsTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsTransferDetailTemp.ProductName = tempProduct.ProductName;
                    lgsTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsTransferDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    lgsTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    lgsTransferDetailTemp.BatchNo = tempProduct.BatchNo;
                    lgsTransferDetailTemp.Qty = 1;
                    lgsTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }

            return lgsTransferDetailTemp;
        }

        public DataTable GetProductTransferRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticTransferOfGoodsNote");

            var query = context.LgsTransferNoteDetails.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, transAutoGenerateInfo.DocumentID);
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); } // Add 1 day and reduce 1 second from 'To date' to get Date range results

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                {
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        case "TransferTypeID":
                            query = (from qr in query
                                     join qh in context.LgsTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.LgsTransferNoteHeaderID
                                     join jt in context.LgsTransferTypes.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qh.TransferTypeID equals jt.LgsTransferTypeID
                                     select qr);
                            break;

                        case "LocationID":
                            query = (from qr in query
                                     join qh in context.LgsTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.LgsTransferNoteHeaderID
                                     join jt in context.Locations.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qh.LocationID equals jt.LocationID
                                     select qr
                                );
                            break;

                        case "ToLocationID":
                            query = (from qr in query
                                     join qh in context.LgsTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.LgsTransferNoteHeaderID
                                     join jt in context.Locations.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qh.ToLocationID equals jt.LocationID
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
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LgsProductMasterID":
                                query = (from qr in query
                                         join jt in context.LgsProductMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ProductID equals jt.LgsProductMasterID
                                         select qr
                                        );
                                break;

                            case "LgsDepartmentID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.DepartmentID equals jt.LgsDepartmentID
                                         select qr
                                        );
                                break;

                            case "LgsCategoryID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.CategoryID equals jt.LgsCategoryID
                                         select qr
                                        );
                                break;

                            case "LgsSubCategoryID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategoryID equals jt.LgsSubCategoryID
                                         select qr
                                        );
                                break;

                            case "LgsSubCategory2ID":
                                query = (from qr in query
                                         join pm in context.LgsProductMasters on qr.ProductID equals pm.LgsProductMasterID
                                         join jt in context.LgsSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategory2ID equals jt.LgsSubCategory2ID
                                         select qr
                                        );
                                break;


                            default:
                                query = query
                                 .Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {
                        query = query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }
                }
            }

            var result = (from pd in query
                          join ph in context.LgsTransferNoteHeaders on pd.TransferNoteHeaderID equals ph.LgsTransferNoteHeaderID
                          join t in context.LgsTransferTypes on ph.TransferTypeID equals t.LgsTransferTypeID
                          join fl in context.Locations on ph.LocationID equals fl.LocationID
                          join tl in context.Locations on ph.ToLocationID equals tl.LocationID
                          join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                          join dp in context.LgsDepartments on pm.DepartmentID equals dp.LgsDepartmentID
                          join ct in context.LgsCategories on pm.CategoryID equals ct.LgsCategoryID
                          join sct in context.LgsSubCategories on pm.SubCategoryID equals sct.LgsSubCategoryID
                          join sct2 in context.LgsSubCategories2 on pm.SubCategory2ID equals sct2.LgsSubCategory2ID
                          select
                                    new
                                    {
                                        FieldString1 = t.TransferType,
                                        FieldString2 = fl.LocationCode + " " + fl.LocationName,
                                        FieldString3 = tl.LocationCode + " " + tl.LocationName,
                                        FieldString4 = dp.DepartmentCode + " " + dp.DepartmentName,
                                        FieldString5 = ct.CategoryCode + " " + ct.CategoryName,
                                        FieldString6 = sct.SubCategoryCode + " " + sct.SubCategoryName,
                                        FieldString7 = sct2.SubCategory2Code + " " + sct2.SubCategory2Name,
                                        FieldString8 = pm.ProductCode,
                                        FieldString9 = pm.ProductName,
                                        FieldString10 = ph.DocumentNo,
                                        FieldString11 = ph.DocumentDate,
                                        FieldString12 = ph.CreatedUser,
                                        FieldDecimal1 = pd.Qty,
                                        FieldDecimal2 = pd.SellingPrice,
                                        FieldDecimal3 = pd.CostPrice,
                                        FieldDecimal4 = (pd.Qty * pd.SellingPrice),
                                        FieldDecimal5 = (pd.Qty * pd.CostPrice)
                                    });

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy))
            {
                dtQueryResult = result.ToDataTable();

                // Get only selected columns
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
                    sbGroupByColumns.Append("it." + item.ReportField + " , ");
                    sbSelectColumns.Append("Key." + item.ReportField + " as " + item.ReportField + " , ");

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

            return dtQueryResult;
        }

    }
}
