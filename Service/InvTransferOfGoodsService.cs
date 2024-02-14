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
    public class InvTransferOfGoodsService
    {
        ERPDbContext context = new ERPDbContext();

        public InvTransferDetailsTemp getTransferDetailTemp(List<InvTransferDetailsTemp> InvTransferDetailsTemp, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID, string batchNo)
        {
            InvTransferDetailsTemp invTransferDetailTemp = new InvTransferDetailsTemp();
            decimal defaultConvertFactor = 1;
            var transferDetailTemp = (from pm in context.InvProductMasters
                                      join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
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
                                          pm.IsBatch
                                      }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                transferDetailTemp = (dynamic)null;

                transferDetailTemp = (from pm in context.InvProductMasters
                                      join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
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

            foreach (var tempProduct in transferDetailTemp)
            {
                
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(false))
                    //invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.BatchNo.Equals(batchNo)).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    //invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                    invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate && p.BatchNo.Equals(batchNo)).FirstOrDefault();
                else
                    //invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();
                    invTransferDetailTemp = InvTransferDetailsTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.BatchNo.Equals(batchNo)).FirstOrDefault();

                if (invTransferDetailTemp == null)
                {
                    invTransferDetailTemp = new InvTransferDetailsTemp();
                    invTransferDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    invTransferDetailTemp.ProductName = tempProduct.ProductName;
                    invTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    invTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invTransferDetailTemp.AverageCost = tempProduct.AverageCost;
                    invTransferDetailTemp.UnitOfMeasure =  tempProduct.UnitOfMeasureName;
                    invTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                }

            }

            return invTransferDetailTemp;
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
        
        public List<InvProductBatchNoTemp> getBatchNoDetail(InvProductMaster invProductMaster, int locationID)
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

       

        public List<InvProductBatchNoTemp> getExpiryDetail(InvProductMaster invProductMaster, string batchNo)
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


        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public InvTransferNoteHeader GetPausedTogHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvTransferNoteHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public InvTransferNoteHeader GetPausedTogHeaderByDocumentNo(long headerID, int locationID)
        {
            return context.InvTransferNoteHeaders.Where(ph => ph.InvTransferNoteHeaderID.Equals(headerID) && ph.LocationID.Equals(locationID)).FirstOrDefault();
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


        public InvTransferNoteHeader GetPausedTOGHeaderByDocumentNo(int documentID, string documentNo)
        {
            return context.InvTransferNoteHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        public bool Save(InvTransferNoteHeader invTransferNoteHeader, List<InvTransferDetailsTemp> invTransferDetailsTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, string formName = "")
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (invTransferNoteHeader.DocumentStatus.Equals(1))
                {
                    InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
                    LocationService locationService = new LocationService();
                    invTransferNoteHeader.DocumentNo = invTransferOfGoodsService.GetDocumentNo(formName, invTransferNoteHeader.LocationID, locationService.GetLocationsByID(invTransferNoteHeader.LocationID).LocationCode, invTransferNoteHeader.DocumentID, false).Trim();

                }

                newDocumentNo = invTransferNoteHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (invTransferNoteHeader.InvTransferNoteHeaderID.Equals(0))
                    context.InvTransferNoteHeaders.Add(invTransferNoteHeader);
                else
                    context.Entry(invTransferNoteHeader).State = EntityState.Modified;

                context.SaveChanges();


                context.Set<InvTransferNoteDetail>().Delete(context.InvTransferNoteDetails.Where(p => p.TransferNoteHeaderID.Equals(invTransferNoteHeader.InvTransferNoteHeaderID) && p.LocationID.Equals(invTransferNoteHeader.LocationID) && p.DocumentID.Equals(invTransferNoteHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var invTransferNoteDetailSaveQuery = (from pd in invTransferDetailsTemp
                                                      select new
                                                      {
                                                          InvTransferNoteDetailID = 0,
                                                          TransferNoteDetailID = 0,
                                                          TransferNoteHeaderID = invTransferNoteHeader.InvTransferNoteHeaderID,
                                                          CompanyID = invTransferNoteHeader.CompanyID,
                                                          LocationID = invTransferNoteHeader.LocationID,
                                                          CostCentreID = invTransferNoteHeader.CostCentreID,
                                                          DocumentID = invTransferNoteHeader.DocumentID,
                                                          DocumentNo = invTransferNoteHeader.DocumentNo,
                                                          DocumentDate = Common.ConvertStringToDateTime(invTransferNoteHeader.DocumentDate.ToString()),
                                                          LineNo = pd.LineNo,
                                                          ProductID = pd.ProductID,
                                                          //BatchNo = (pd.IsBatch == true ? pd.BatchNo : string.Empty),
                                                          BatchNo = pd.BatchNo,
                                                          BatchExpiryDate = Common.ConvertStringToDateTime(pd.ExpiryDate.ToString()),
                                                          AvgCost = pd.AverageCost,
                                                          CostPrice = pd.CostPrice,
                                                          SellingPrice = pd.SellingPrice,
                                                          PackId = 0,
                                                          Qty = pd.Qty,
                                                          OrderQty = pd.Qty,
                                                          CurrentQty = 0,
                                                          QtyConvertFactor = (pd.Qty * pd.ConvertFactor),
                                                          BaseUnitID = pd.BaseUnitID,
                                                          ConvertFactor = pd.ConvertFactor,
                                                          UnitOfMeasureID = pd.UnitOfMeasureID,
                                                          GrossAmount = pd.NetAmount,
                                                          NetAmount = pd.NetAmount,
                                                          ToLocationID = invTransferNoteHeader.ToLocationID,
                                                          DocumentStatus = invTransferNoteHeader.DocumentStatus,
                                                          IsBatch = pd.IsBatch,
                                                          GroupOfCompanyID = Common.GroupOfCompanyID,
                                                          CreatedUser = invTransferNoteHeader.CreatedUser,
                                                          CreatedDate = Common.ConvertStringToDateTime(invTransferNoteHeader.CreatedDate.ToString()),
                                                          ModifiedUser = invTransferNoteHeader.ModifiedUser,
                                                          ModifiedDate = Common.ConvertStringToDateTime(invTransferNoteHeader.ModifiedDate.ToString()),
                                                          DataTransfer = invTransferNoteHeader.DataTransfer
                                                      }).ToList();


               CommonService.BulkInsert(Common.LINQToDataTable(invTransferNoteDetailSaveQuery), "InvTransferNoteDetail");                


                context.SaveChanges();


                var parameter = new DbParameter[] 
                    { 

                        new System.Data.SqlClient.SqlParameter { ParameterName ="@invTransferNoteHeaderID", Value=invTransferNoteHeader.InvTransferNoteHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromLocationID", Value=invTransferNoteHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToLocationID", Value=invTransferNoteHeader.ToLocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invTransferNoteHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invTransferNoteHeader.DocumentStatus},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentDocumentID", Value=invTransferNoteHeader.ReferenceDocumentDocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReferenceDocumentID", Value=invTransferNoteHeader.ReferenceDocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@PosDocument", Value=invTransferNoteHeader.PosReferenceNo},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@TransStatus", Value=invTransferNoteHeader.TransStatusId},
                        
                    };

               
                if (CommonService.ExecuteStoredProcedure("spInvTransferNoteSave", parameter))
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
            List<string> documentNoList = context.InvTransferNoteHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }


        public InvTransferNoteHeader GetTOGByDocumentNo(string documentNo)
        {
            return context.InvTransferNoteHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<InvTransferDetailsTemp> getPausedTOGDetail(InvTransferNoteHeader invTransferNoteHeader)
        {
            InvTransferDetailsTemp invTransferDetailsTemp;

            List<InvTransferDetailsTemp> invTransferDetailsTempList = new List<InvTransferDetailsTemp>();

           
            var transferTemp = (from pm in context.InvProductMasters
                                    join qd in context.InvTransferNoteDetails on pm.InvProductMasterID equals qd.ProductID
                                    join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                    where qd.LocationID.Equals(invTransferNoteHeader.LocationID) && qd.CompanyID.Equals(invTransferNoteHeader.CompanyID)
                                    && qd.DocumentID.Equals(invTransferNoteHeader.DocumentID) && qd.TransferNoteHeaderID.Equals(invTransferNoteHeader.InvTransferNoteHeaderID)
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

                invTransferDetailsTemp = new InvTransferDetailsTemp();
                UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

                invTransferDetailsTemp.SellingPrice = tempProduct.SellingPrice;
                invTransferDetailsTemp.CostPrice = tempProduct.CostPrice;
                invTransferDetailsTemp.ConvertFactor = tempProduct.ConvertFactor;
               
                invTransferDetailsTemp.LineNo = tempProduct.LineNo;
                invTransferDetailsTemp.NetAmount = tempProduct.NetAmount;
                
                invTransferDetailsTemp.ProductCode = tempProduct.ProductCode;
                invTransferDetailsTemp.ProductID = tempProduct.ProductID;
                invTransferDetailsTemp.ProductName = tempProduct.ProductName;
                invTransferDetailsTemp.Qty = tempProduct.Qty;
                invTransferDetailsTemp.BatchNo = tempProduct.BatchNo;
                invTransferDetailsTemp.ExpiryDate = tempProduct.BatchExpiryDate;
                invTransferDetailsTemp.UnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(tempProduct.UnitOfMeasureID).UnitOfMeasureName;
                invTransferDetailsTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invTransferDetailsTemp.IsBatch = tempProduct.IsBatch;

                invTransferDetailsTempList.Add(invTransferDetailsTemp);
            }
            return invTransferDetailsTempList.OrderBy(pd => pd.LineNo).ToList();
            
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

        public List<InvTransferDetailsTemp> getUpdateInvTransferDetailTemp(List<InvTransferDetailsTemp> invTransferDetail, InvTransferDetailsTemp invTogDetails, InvProductMaster invProductMaster)
        {
            InvTransferDetailsTemp invTransferDetailsTemp = new InvTransferDetailsTemp();

            if (invProductMaster.IsExpiry)
                //invTransferDetailsTemp = invTransferDetail.Where(p => p.ProductID == invTogDetails.ProductID && p.UnitOfMeasureID == invTogDetails.UnitOfMeasureID && p.ExpiryDate == invTogDetails.ExpiryDate).FirstOrDefault();
                invTransferDetailsTemp = invTransferDetail.Where(p => p.ProductID == invTogDetails.ProductID && p.UnitOfMeasureID == invTogDetails.UnitOfMeasureID && p.ExpiryDate == invTogDetails.ExpiryDate && p.BatchNo == invTogDetails.BatchNo).FirstOrDefault();
            else
                //invTransferDetailsTemp = invTransferDetail.Where(p => p.ProductID == invTogDetails.ProductID && p.UnitOfMeasureID == invTogDetails.UnitOfMeasureID).FirstOrDefault();
                invTransferDetailsTemp = invTransferDetail.Where(p => p.ProductID == invTogDetails.ProductID && p.UnitOfMeasureID == invTogDetails.UnitOfMeasureID && p.BatchNo == invTogDetails.BatchNo).FirstOrDefault();

            if (invTransferDetailsTemp == null || invTransferDetailsTemp.LineNo.Equals(0))
            {
                if (invTransferDetail.Count.Equals(0))
                    invTogDetails.LineNo = 1;
                else
                    invTogDetails.LineNo = invTransferDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invTransferDetail.Remove(invTransferDetailsTemp);
                invTogDetails.LineNo = invTransferDetailsTemp.LineNo;

            }

            invTransferDetail.Add(invTogDetails);

            return invTransferDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvTransferDetailsTemp> GetUpdateInvTransferDetailTempForBarcodeScan(List<InvTransferDetailsTemp> invTransferDetail, InvTransferDetailsTemp invTogDetails)
        {
            InvTransferDetailsTemp invTransferDetailsTemp = new InvTransferDetailsTemp();

            invTransferDetailsTemp = invTransferDetail.Where(p => p.ProductID == invTogDetails.ProductID && p.UnitOfMeasureID == invTogDetails.UnitOfMeasureID && p.BatchNo == invTogDetails.BatchNo).FirstOrDefault();

            if (invTransferDetailsTemp == null || invTransferDetailsTemp.LineNo.Equals(0))
            {
                if (invTransferDetail.Count.Equals(0))
                    invTogDetails.LineNo = 1;
                else
                    invTogDetails.LineNo = invTransferDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invTransferDetail.Remove(invTransferDetailsTemp);
                invTogDetails.LineNo = invTransferDetailsTemp.LineNo;
            }

            invTransferDetail.Add(invTogDetails);

            return invTransferDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvTransferDetailsTemp> GetDeleteInvTransferDetailsTemp(List<InvTransferDetailsTemp> invTransferTempDetail, InvTransferDetailsTemp invTransferDetails, List<InvProductSerialNoTemp> invProductSerialNoListToDelete)
        {
            InvTransferDetailsTemp invTransferDetailsTemp = new InvTransferDetailsTemp();
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();


            //invTransferDetailsTemp = invTransferTempDetail.Where(p => p.ProductID == invTransferDetails.ProductID && p.UnitOfMeasureID == invTransferDetails.UnitOfMeasureID && (p.ExpiryDate.Equals(invTransferDetails.ExpiryDate))).FirstOrDefault();
            invTransferDetailsTemp = invTransferTempDetail.Where(p => p.ProductID == invTransferDetails.ProductID && p.UnitOfMeasureID == invTransferDetails.UnitOfMeasureID).FirstOrDefault();
            
            long removedLineNo = 0;
            if (invTransferDetailsTemp != null)
            {
                invTransferTempDetail.Remove(invTransferDetailsTemp);
                removedLineNo = invTransferDetailsTemp.LineNo;
            }
            invTransferTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            //invProductSerialNoTempList = lgsProductSerialNoListToDelete.Where(p => p.ProductID == lgsTransferDetails.ProductID && p.UnitOfMeasureID == lgsTransferDetails.UnitOfMeasureID && p.ExpiryDate.Equals(lgsTransferDetails.ExpiryDate)).ToList();

            foreach (InvProductSerialNoTemp InvProductSerialNoTempDelete in invProductSerialNoTempList)
            {
                invProductSerialNoListToDelete.Remove(InvProductSerialNoTempDelete);
            }

            //lgsProductSerialNoList = lgsProductSerialNoListToDelete;

            return invTransferTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<InvTransferDetailsTemp> GetGRNDetails(InvPurchaseHeader invPurchaseHeader, int documentID)
        {
            InvTransferDetailsTemp invTransferDetailTemp;

            List<InvTransferDetailsTemp> invTransferDetailTempList = new List<InvTransferDetailsTemp>();


            var transferDetailTemp = (from pm in context.InvProductMasters
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
                                          pd.BatchNo,
                                          pd.IsBatch,
                                          pd.BaseUnitID

                                      }).ToArray();

            foreach (var tempProduct in transferDetailTemp)
            {

                invTransferDetailTemp = new InvTransferDetailsTemp();
                if (tempProduct.BalanceQty > 0)
                {
                    invTransferDetailTemp.AverageCost = tempProduct.AvgCost;
                    invTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    invTransferDetailTemp.CurrentQty = tempProduct.CurrentQty;
                    invTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invTransferDetailTemp.LineNo = tempProduct.LineNo;
                    invTransferDetailTemp.FromLocationID = tempProduct.LocationID;
                    invTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency((tempProduct.BalanceQty * tempProduct.SellingPrice).ToString());
                    invTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    invTransferDetailTemp.ProductID = tempProduct.ProductID;
                    invTransferDetailTemp.ProductName = tempProduct.ProductName;
                    invTransferDetailTemp.Qty = (tempProduct.BalanceQty + tempProduct.FreeQty);
                    invTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invTransferDetailTemp.BatchNo = tempProduct.BatchNo;
                    invTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                    invTransferDetailTemp.BaseUnitID = tempProduct.BaseUnitID;

                    invTransferDetailTempList.Add(invTransferDetailTemp);
                }

            }
            return invTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<InvTransferDetailsTemp> GetReturnDocumentDetails(string documentNo, int locationId, int transStatus)
        {
           
            InvTransferDetailsTemp invTransferDetailTemp;

            List<InvTransferDetailsTemp> invTransferDetailTempList = new List<InvTransferDetailsTemp>();


            var transferDetailTemp = (from td in context.TransactionDets
                                      join um in context.UnitOfMeasures on td.UnitOfMeasureID equals um.UnitOfMeasureID
                                      where
                                        td.TransStatus == transStatus &&
                                        td.LocationID == locationId &&
                                        td.Status == transStatus &&
                                        td.Receipt == documentNo &&
                                        td.BalanceQty > 0
                                      group new { td, um } by new
                                      {
                                          td.ExpiryDate,
                                          td.ProductCode,
                                          td.ProductID,
                                          td.Descrip,
                                          um.UnitOfMeasureID,
                                          um.UnitOfMeasureName,
                                          td.BatchNo,
                                          td.Cost,
                                          td.AvgCost,
                                          td.Price,
                                          td.Amount,
                                          td.ConvertFactor,
                                          td.RowNo,
                                          td.LocationID,
                                          td.DocumentID
                                      } into g
                                      select new
                                      {
                                          ExpiryDate = (System.DateTime?)g.Key.ExpiryDate,
                                          g.Key.ProductCode,
                                          ProductID = (System.Int64?)g.Key.ProductID,
                                          g.Key.Descrip,
                                          UnitOfMeasureID = (System.Int64?)g.Key.UnitOfMeasureID,
                                          g.Key.UnitOfMeasureName,
                                          g.Key.BatchNo,
                                          Cost = (System.Decimal?)g.Key.Cost,
                                          AvgCost = (System.Decimal?)g.Key.AvgCost,
                                          Price = (System.Decimal?)g.Key.Price,
                                          Amount = (System.Decimal?)g.Key.Amount,
                                          ConvertFactor = (System.Decimal?)g.Key.ConvertFactor,
                                          Qty =
                                          g.Key.DocumentID == 1 ? g.Sum(p => p.td.BalanceQty) :
                                          g.Key.DocumentID == 3 ? g.Sum(p => p.td.BalanceQty) :
                                          g.Key.DocumentID == 2 ? g.Sum(p => -p.td.BalanceQty) :
                                          g.Key.DocumentID == 4 ? g.Sum(p => -p.td.BalanceQty) : 0,
                                          RowNo = (System.Int32?)g.Key.RowNo,
                                          LocationID = (System.Int32?)g.Key.LocationID
                                      }).ToArray();

            foreach (var tempProduct in transferDetailTemp)
            {

                invTransferDetailTemp = new InvTransferDetailsTemp();

                invTransferDetailTemp.AverageCost = (decimal)tempProduct.AvgCost;
                invTransferDetailTemp.ConvertFactor = (decimal)tempProduct.ConvertFactor;
                invTransferDetailTemp.CostPrice = (decimal)tempProduct.Cost;
                invTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invTransferDetailTemp.LineNo = (long)tempProduct.RowNo;
                invTransferDetailTemp.FromLocationID = (int)tempProduct.LocationID;
                invTransferDetailTemp.NetAmount = Common.ConvertStringToDecimalCurrency((tempProduct.Qty * tempProduct.Price).ToString());
                invTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                invTransferDetailTemp.ProductID = (long)tempProduct.ProductID;
                invTransferDetailTemp.ProductName = tempProduct.Descrip;
                invTransferDetailTemp.Qty = (decimal)tempProduct.Qty;
                invTransferDetailTemp.SellingPrice = (decimal)tempProduct.Price;
                invTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invTransferDetailTemp.UnitOfMeasureID = (long)tempProduct.UnitOfMeasureID;
                invTransferDetailTemp.BatchNo = tempProduct.BatchNo;

                invTransferDetailTempList.Add(invTransferDetailTemp);


            }
            return invTransferDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }


        public DataTable GetTransferofGoodsTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            //var query = context.InvTransferNoteHeaders.AsNoTracking().Where("DocumentNo==@0 AND DocumentStatus == @1 AND DocumentId == @2", documentNo, documentStatus, documentid);
            //var queryResult = query.AsEnumerable();
            return (
                        from ph in context.InvTransferNoteHeaders
                        join pd in context.InvTransferNoteDetails on ph.InvTransferNoteHeaderID equals pd.TransferNoteHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join sc2 in context.InvSubCategories2 on pm.SubCategory2ID equals sc2.InvSubCategory2ID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join lFrom in context.Locations on ph.LocationID equals lFrom.LocationID
                        join lTo in context.Locations on ph.ToLocationID equals lTo.LocationID
                        join re in context.InvTransferType on ph.TransferTypeID equals re.InvTransferTypeID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentid)

                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),//.ToShortDateString(),
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
                                FieldString16 = sc2.SubCategory2Name,
                                FieldString17 = "",
                                FieldString18 = pd.OrderQty,
                                FieldString19 = "",
                                FieldString20 = "",
                                FieldString21 = pd.SellingPrice,
                                FieldString22 = pd.NetAmount,
                                FieldString23 = ph.GrossAmount,
                                FieldString24 = "",
                                FieldString25 = "",
                                FieldString26 = "",
                                FieldString27 = "",
                                FieldString28 = ph.NetAmount,
                                FieldString29 = "",
                                FieldString30 = ph.CreatedUser
                            }).ToArray().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmTransferOfGoodsNote");
           
            // Create query 
            IQueryable qryResult = null;
            if (string.Equals(autoGenerateInfo.FormName, "InvRptTransferRegister"))
            {
                qryResult = context.InvTransferNoteDetails.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "LocationID":
                        qryResult = null;
                        qryResult = context.InvTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
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
                        qryResult = context.InvTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
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
                        qryResult = context.InvTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.InvTransferType, reportDataStruct.DbColumnName.Trim(),
                                                  "InvTransferTypeID",
                                                  "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                     "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvProductMasterID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID",
                                               reportDataStruct.DbColumnName.Trim(),
                                               "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                  "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                         .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                         .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvDepartmentID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.DepartmentID)")
                                            .Join(context.InvDepartments, "DepartmentID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;
                    case "InvCategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.CategoryID)")
                                            .Join(context.InvCategories, "CategoryID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "InvSubCategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.SubCategoryID)")
                                            .Join(context.InvSubCategories, "SubCategoryID", reportDataStruct.DbColumnName.Trim(),
                                            "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "InvSubCategory2ID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                            "new(inner.SubCategory2ID)")
                                            .Join(context.InvSubCategories2, "SubCategory2ID", reportDataStruct.DbColumnName.Trim(),
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
                qryResult = context.InvTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, transAutoGenerateInfo.DocumentID);

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
                        qryResult = qryResult.Join(context.InvTransferType, reportDataStruct.DbColumnName.Trim(),
                                                  "InvTransferTypeID",
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
        public DataTable GetTransferOfGoodsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.InvTransferNoteHeaders.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                                     select qr );
                            break;

                        case "TransferTypeID":
                            query = (from qr in query
                                     join jt in context.InvTransferType.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.TransferTypeID equals jt.InvTransferTypeID
                                     select qr );
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

            var queryResult = query.AsNoTracking();//.AsEnumerable();
            var result = (from h in queryResult
                               join lFrom in context.Locations on h.LocationID equals lFrom.LocationID
                               join lTo in context.Locations on h.ToLocationID equals lTo.LocationID
                               join t in context.InvTransferType on h.TransferTypeID equals t.InvTransferTypeID
                               select
                                   new
                                   {
                                       FieldString1 = lFrom.LocationCode + " " + lFrom.LocationName,
                                       FieldString2 = lTo.LocationCode + " " + lTo.LocationName,
                                       FieldString3 = h.DocumentNo,
                                       FieldString4 = h.DocumentDate, //.ToShortDateString(),
                                       FieldString5 = t.TransferType.Trim(),
                                       FieldString6 = h.CreatedUser,
                                       FieldString7 = h.ReferenceNo, //h.Remark : "",
                                       FieldDecimal1 = (from d in context.InvTransferNoteDetails
                                                            where
                                                                d.TransferNoteHeaderID == h.InvTransferNoteHeaderID
                                                            select new
                                                            {
                                                                Column1 = ((System.Decimal?)d.Qty ?? (System.Decimal?)0)
                                                            }).Sum(p => p.Column1), // Qty
                                       FieldDecimal2 = h.NetAmount, // Salling Value
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

        public List<InvMinusProductDetailsTemp> getMinusProductDetails(List<InvTransferDetailsTemp> invTransferDetailsTemp, int locationID)
        {
            List<InvMinusProductDetailsTemp> invMinusProductDetailsTempList = new List<InvMinusProductDetailsTemp>();

            List<InvProductBatchNoExpiaryDetail> invMinusProducts = new List<InvProductBatchNoExpiaryDetail>();

            InvProductMasterService invProductMasterService = new InvProductMasterService();
            UnitOfMeasureService unitOfMeasureService = new UnitOfMeasureService();

            foreach (var tempProduct in invTransferDetailsTemp)
            {
                invMinusProducts = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(tempProduct.ProductID) && ps.BatchNo.Equals(tempProduct.BatchNo) && ps.LocationID.Equals(locationID) && ps.BalanceQty < (tempProduct.Qty * tempProduct.ConvertFactor)).ToList();

                if (invMinusProducts.Count> 0)
                {
                    InvMinusProductDetailsTemp invMinusProductDetailsTemp = new InvMinusProductDetailsTemp();

                    invMinusProductDetailsTemp.LineNo = tempProduct.LineNo;
                    invMinusProductDetailsTemp.ProductID = tempProduct.ProductID;
                    invMinusProductDetailsTemp.ProductCode = invProductMasterService.GetProductDetailsByID(tempProduct.ProductID).ProductCode;
                    invMinusProductDetailsTemp.ProductName = invProductMasterService.GetProductDetailsByID(tempProduct.ProductID).ProductName;
                    invMinusProductDetailsTemp.Qty = tempProduct.Qty;
                    invMinusProductDetailsTemp.UnitOfMeasure = unitOfMeasureService.GetUnitOfMeasureById(tempProduct.UnitOfMeasureID).UnitOfMeasureName;

                    invMinusProductDetailsTempList.Add(invMinusProductDetailsTemp);
                }
            }
            return invMinusProductDetailsTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public string[] GetAllProductCodes()
        {
            List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();
            
            //List<string> ProductCodeList = context.InvProductMasters.Join(context.InvProductBatchNoExpiaryDetails ,
            //                                s => new { s.InvProductMasterID, s.UnitOfMeasureID },
            //           h => new { h.ProductID, h.UnitOfMeasureID },
            //           (s, h) => s);
            
            //return ProductCodeList.ToArray();
            //List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
            //                                                .DefaultIfEmpty()
            //                                                .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
            //                                                b => b.InvProductMasterID,
            //                                                c => c.ProductID,
            //                                                (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductCodeList.ToArray();
        }

        public string[] GetAllProductNames()
        {

            List<string> ProductNameList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductName).ToList();
            //List<string> ProductNameList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
            //                                                .DefaultIfEmpty()
            //                                                .Join(context.InvProductBatchNoExpiaryDetails.Where (b => b.UnitOfMeasureID.Equals(1)),
            //                                                b => b.InvProductMasterID,
            //                                                c => c.ProductID,
            //                                                (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductName).ToList();
            
            return ProductNameList.ToArray();

        }

        public InvTransferDetailsTemp GetTransferDetailTempForBarcodeScan(long barcode, int qty) 
        {
            //List<InvTransferDetailsTemp> rtnList = new List<InvTransferDetailsTemp>();
            InvTransferDetailsTemp invTransferDetailTemp=null;

            InvBatchNoExpiaryDetailService batchNoExpiaryDetailService = new InvBatchNoExpiaryDetailService();
            InvProductBatchNoExpiaryDetail invProductBatchNoExpiaryDetail = new InvProductBatchNoExpiaryDetail();

            invProductBatchNoExpiaryDetail = batchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcode(barcode);

            if (invProductBatchNoExpiaryDetail != null)
            {
                decimal defaultConvertFactor = 1;

                var transferDetailTemp = (from pm in context.InvProductMasters
                                          join bed in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals bed.ProductID
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
                    invTransferDetailTemp = new InvTransferDetailsTemp();

                    invTransferDetailTemp.ProductID = tempProduct.ProductID;
                    invTransferDetailTemp.ProductCode = tempProduct.ProductCode;
                    invTransferDetailTemp.ProductName = tempProduct.ProductName;
                    invTransferDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invTransferDetailTemp.CostPrice = tempProduct.CostPrice;
                    invTransferDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invTransferDetailTemp.AverageCost = tempProduct.AverageCost;
                    invTransferDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invTransferDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invTransferDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    invTransferDetailTemp.BatchNo = tempProduct.BatchNo;
                    invTransferDetailTemp.Qty = qty;
                    invTransferDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }

            return invTransferDetailTemp;
        }

        public string GetPreviousDocumentNo(int locationID, int documentID, string createdUser)
        {
            long PreDocNo =0;
            string rtnDocNo = "";

            //string aa = context.InvTransferNoteHeaders.Where(d => d.LocationID.Equals(locationID) && d.CreatedUser.Equals(createdUser)).Max(c => c.InvTransferNoteHeaderID).ToString();

            var a = from t in
                        (from t in context.InvTransferNoteHeaders
                         where
                         t.LocationID == 1 &&
                         t.DocumentID == documentID &&
                         t.CreatedUser == createdUser
                         select new
                         {
                             t.InvTransferNoteHeaderID,
                             Dummy = "x"
                         })
                    group t by new { t.Dummy } into g
                    select new
                    {
                        Column1 = (System.Int64)g.Max(p => p.InvTransferNoteHeaderID)
                    };

            foreach(var tmp in a)
            {
                PreDocNo = tmp.Column1;
                   // string.IsNullOrEmpty(existingLoyaltyCustomer.Address1) ? string.Empty : existingLoyaltyCustomer.Address1;
            }


            InvTransferNoteHeader invTransferNoteHeader = new InvTransferNoteHeader();
            InvTransferOfGoodsService invTransferOfGoodsService = new InvTransferOfGoodsService();
            invTransferNoteHeader = invTransferOfGoodsService.GetPausedTogHeaderByDocumentNo(PreDocNo, locationID);

            if (invTransferNoteHeader != null)
            {
                rtnDocNo = invTransferNoteHeader.DocumentNo.ToString();
            }

            return rtnDocNo;
        }
        
        public List<InvTransferNoteHeader> GetTOGDocuments(int locationID)
        {

            List<InvTransferNoteHeader> rtnList = new List<InvTransferNoteHeader>();
           
            var qry = (from td in context.InvTransferNoteHeaders
                       where td.ToLocationID == locationID && td.IsDelete == false  
                       select new
                       {
                           td.DocumentNo
                       }).Distinct().OrderBy(dt => dt.DocumentNo).ToArray();
            foreach (var temp in qry)
            {
                InvTransferNoteHeader invTransferNoteHeader = new InvTransferNoteHeader();
                invTransferNoteHeader.DocumentNo = temp.DocumentNo;

                rtnList.Add(invTransferNoteHeader);
            }

            return rtnList;
        }

        public List<InvTransferNoteHeader> GetTOGDocumentsForStAdj(int locationID)
        {

            List<InvTransferNoteHeader> rtnList = new List<InvTransferNoteHeader>();

            var qry = (from td in context.InvTransferNoteHeaders
                       where td.ToLocationID == locationID && td.PosReferenceNo !="" 
                       select new
                       {
                           td.DocumentNo
                       }).Distinct().OrderBy(dt => dt.DocumentNo).ToArray();
            foreach (var temp in qry)
            {
                InvTransferNoteHeader invTransferNoteHeader = new InvTransferNoteHeader();
                invTransferNoteHeader.DocumentNo = temp.DocumentNo;

                rtnList.Add(invTransferNoteHeader);
            }

            return rtnList;
        }

        public DataTable GetProductTransferRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmTransferOfGoodsNote");

            var query = context.InvTransferNoteDetails.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, transAutoGenerateInfo.DocumentID);
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
                                     join qh in context.InvTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.InvTransferNoteHeaderID
                                     join jt in context.InvTransferType.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " >= " + "@0 AND " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " <= @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qh.TransferTypeID equals jt.InvTransferTypeID
                                     select qr);
                            break;
                    
                        case "LocationID":
                            query = (from qr in query
                                     join qh in context.InvTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.InvTransferNoteHeaderID
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
                                     join qh in context.InvTransferNoteHeaders on qr.TransferNoteHeaderID equals qh.InvTransferNoteHeaderID
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
                            case "InvProductMasterID":
                                query = (from qr in query
                                         join jt in context.InvProductMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ProductID equals jt.InvProductMasterID
                                         select qr
                                        );
                                break;

                            case "InvDepartmentID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.DepartmentID equals jt.InvDepartmentID
                                         select qr
                                        );
                                break;

                            case "InvCategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.CategoryID equals jt.InvCategoryID
                                         select qr
                                        );
                                break;

                            case "InvSubCategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategoryID equals jt.InvSubCategoryID
                                         select qr
                                        );
                                break;

                            case "InvSubCategory2ID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join jt in context.InvSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategory2ID equals jt.InvSubCategory2ID
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
                          join ph in context.InvTransferNoteHeaders on pd.TransferNoteHeaderID equals ph.InvTransferNoteHeaderID
                          join t in context.InvTransferType on ph.TransferTypeID equals t.InvTransferTypeID
                          join fl in context.Locations on ph.LocationID equals fl.LocationID
                          join tl in context.Locations on ph.ToLocationID equals tl.LocationID
                          join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                          join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                          join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                          join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                          join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
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

        
    }
}
