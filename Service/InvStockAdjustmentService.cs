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
    /// Developed byu Nuwan
    /// </summary>
    public class InvStockAdjustmentService
    {
        ERPDbContext context = new ERPDbContext();
        string sql;

        public InvStockAdjustmentDetailTemp getStockAdjustmentDetailTemp(List<InvStockAdjustmentDetailTemp> InvStockAdjustmentDetailTemp, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID)
        {
            InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();
            decimal defaultConvertFactor = 1;
            var StockAdjustmentDetailTemp =   (from pm in context.InvProductMasters
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
                                                  ConvertFactor = defaultConvertFactor
                                              }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                StockAdjustmentDetailTemp = (dynamic)null;

                StockAdjustmentDetailTemp =   (from pm in context.InvProductMasters
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
                                                  puc.ConvertFactor
                                              }).ToArray();
            }

            foreach (var tempProduct in StockAdjustmentDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    invStockAdjustmentDetailTemp = InvStockAdjustmentDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    invStockAdjustmentDetailTemp = InvStockAdjustmentDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate).FirstOrDefault();
                else
                    invStockAdjustmentDetailTemp = InvStockAdjustmentDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                if (invStockAdjustmentDetailTemp == null)
                {
                    invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();
                    invStockAdjustmentDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invStockAdjustmentDetailTemp.ProductCode = tempProduct.ProductCode;
                    invStockAdjustmentDetailTemp.ProductName = tempProduct.ProductName;
                    invStockAdjustmentDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invStockAdjustmentDetailTemp.CostPrice = tempProduct.CostPrice;
                    invStockAdjustmentDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invStockAdjustmentDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invStockAdjustmentDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                }
            }

            return invStockAdjustmentDetailTemp;
        }

        public List<InvStockAdjustmentDetailTemp> getUpdateInvStockAdjustmentDetailTemp(List<InvStockAdjustmentDetailTemp> invStockAdjustmentDetail, InvStockAdjustmentDetailTemp invStockAdjDetail, InvProductMaster invProductMaster)
        {
            InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();

            if (invProductMaster.IsExpiry)
                invStockAdjustmentDetailTemp = invStockAdjustmentDetail.Where(p => p.ProductID == invStockAdjDetail.ProductID && p.UnitOfMeasureID == invStockAdjDetail.UnitOfMeasureID && p.ExpiryDate == invStockAdjDetail.ExpiryDate).FirstOrDefault();
            else
                invStockAdjustmentDetailTemp = invStockAdjustmentDetail.Where(p => p.ProductID == invStockAdjDetail.ProductID && p.UnitOfMeasureID == invStockAdjDetail.UnitOfMeasureID).FirstOrDefault();

            if (invStockAdjustmentDetailTemp == null || invStockAdjustmentDetailTemp.LineNo.Equals(0))
            {
                if (invStockAdjustmentDetail.Count.Equals(0))
                    invStockAdjDetail.LineNo = 1;
                else
                    invStockAdjDetail.LineNo = invStockAdjustmentDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                invStockAdjustmentDetail.Remove(invStockAdjustmentDetailTemp);
                invStockAdjDetail.LineNo = invStockAdjustmentDetailTemp.LineNo;

            }

            invStockAdjustmentDetail.Add(invStockAdjDetail);

            return invStockAdjustmentDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvStockAdjustmentDetailTemp> getDeleteInvStockAdjustmentDetailTemp(List<InvStockAdjustmentDetailTemp> invStockAdjustmentTempDetail, InvStockAdjustmentDetailTemp invStockAdjustmentDetail, List<InvProductSerialNoTemp> invProductSerialNoListToDelete, out List<InvProductSerialNoTemp> invProductSerialNoList)
        {
            InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            invStockAdjustmentDetailTemp = invStockAdjustmentTempDetail.Where(p => p.ProductID == invStockAdjustmentDetail.ProductID && p.UnitOfMeasureID == invStockAdjustmentDetail.UnitOfMeasureID && p.ExpiryDate == invStockAdjustmentDetail.ExpiryDate).FirstOrDefault();
            long removedLineNo = 0;
            if (invStockAdjustmentDetailTemp != null)
            {
                invStockAdjustmentTempDetail.Remove(invStockAdjustmentDetailTemp);
                removedLineNo = invStockAdjustmentDetailTemp.LineNo;

            }

            invStockAdjustmentTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            
            invProductSerialNoTempList = invProductSerialNoListToDelete.Where(p => p.ProductID == invStockAdjustmentDetail.ProductID && p.UnitOfMeasureID == invStockAdjustmentDetail.UnitOfMeasureID && p.ExpiryDate == invStockAdjustmentDetail.ExpiryDate).ToList();

            foreach (InvProductSerialNoTemp InvProductSerialNoTempDelete in invProductSerialNoTempList)
            {
                invProductSerialNoListToDelete.Remove(InvProductSerialNoTempDelete);
            }

            invProductSerialNoList = invProductSerialNoListToDelete;

            return invStockAdjustmentTempDetail.OrderBy(pd => pd.LineNo).ToList();
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

        public bool IsValidNoOfSerialNo(List<InvProductSerialNoTemp> invProductSerialNoList, InvProductSerialNoTemp invProductSerialNumber, decimal Qty)
        {
            if (Qty.Equals(invProductSerialNoList.Where(s => s.ProductID.Equals(invProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(invProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(invProductSerialNumber.ExpiryDate)).Count()))
                return true;
            else
                return false;
        }

        public List<InvStockAdjustmentDetailTemp> getPausedInvStockAdjustmentDetailTemp(InvStockAdjustmentHeader invStockAdjustmentHeader)
        {
            InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp;

            List<InvStockAdjustmentDetailTemp> invStockAdjustmentDetailTempList = new List<InvStockAdjustmentDetailTemp>();


            var stockAdjustmentDetailTemp =   (from pm in context.InvProductMasters
                                              join sa in context.InvStockAdjustmentDetails on pm.InvProductMasterID equals sa.ProductID
                                              join uc in context.UnitOfMeasures on sa.UnitOfMeasureID equals uc.UnitOfMeasureID
                                              where sa.LocationID.Equals(invStockAdjustmentHeader.LocationID) && sa.CompanyID.Equals(invStockAdjustmentHeader.CompanyID)
                                              && sa.DocumentID.Equals(invStockAdjustmentHeader.DocumentID) && sa.InvStockAdjustmentHeaderID.Equals(invStockAdjustmentHeader.InvStockAdjustmentHeaderID)
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
                invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();

                invStockAdjustmentDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invStockAdjustmentDetailTemp.CostPrice = tempProduct.CostPrice;
                invStockAdjustmentDetailTemp.CostValue = tempProduct.CostValue;
                invStockAdjustmentDetailTemp.SellingValue = tempProduct.SellingValue;
                invStockAdjustmentDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                invStockAdjustmentDetailTemp.LineNo = tempProduct.LineNo;
                invStockAdjustmentDetailTemp.LocationID = tempProduct.LocationID;
                invStockAdjustmentDetailTemp.OrderQty = tempProduct.OrderQty;
                invStockAdjustmentDetailTemp.BatchNo = tempProduct.BatchNo;
                invStockAdjustmentDetailTemp.ProductCode = tempProduct.ProductCode;
                invStockAdjustmentDetailTemp.ProductID = tempProduct.ProductID;
                invStockAdjustmentDetailTemp.ProductName = tempProduct.ProductName;
                invStockAdjustmentDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invStockAdjustmentDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invStockAdjustmentDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                invStockAdjustmentDetailTempList.Add(invStockAdjustmentDetailTemp);
            }
            return invStockAdjustmentDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvStockAdjustmentDetailTemp> GetSavedTogByDocumentNumber(string documentNo)
        {

            List<InvStockAdjustmentDetailTemp> invStockAdjustmentDetailTempList = new List<InvStockAdjustmentDetailTemp>();
            InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp;

 
            var stockAdjustmentDetailTemp = (
                           from tog in context.InvTransferNoteDetails
                           join th in context.InvTransferNoteHeaders on tog.TransferNoteHeaderID equals th.InvTransferNoteHeaderID
                           join uom in context.UnitOfMeasures on tog.UnitOfMeasureID equals uom.UnitOfMeasureID
                           join l in context.Locations on tog.LocationID equals l.LocationID
                           join pm in context.InvProductMasters on tog.ProductID equals pm.InvProductMasterID
                           where tog.DocumentNo == documentNo && th.IsDelete == false
                           select new
                           {

                               tog.ProductID,
                               pm.ProductCode,
                               pm.ProductName,
                               tog.BatchNo,
                               tog.BatchExpiryDate,
                               uom.UnitOfMeasureID,
                               uom.UnitOfMeasureName,
                               tog.Qty,
                               tog.CostPrice,
                               tog.SellingPrice,
                               tog.LocationID,
                               l.LocationName
                           }).ToArray().OrderBy(p => p.ProductID);

            int lineno = 0;
            foreach (var tempProduct in stockAdjustmentDetailTemp)
            {
                lineno++;
                invStockAdjustmentDetailTemp = new InvStockAdjustmentDetailTemp();

                invStockAdjustmentDetailTemp.ConvertFactor = 1;
                invStockAdjustmentDetailTemp.CostPrice = tempProduct.CostPrice;
                invStockAdjustmentDetailTemp.CostValue = tempProduct.CostPrice * tempProduct.Qty;
                invStockAdjustmentDetailTemp.SellingValue = tempProduct.SellingPrice * tempProduct.Qty;
                invStockAdjustmentDetailTemp.ExpiryDate = tempProduct.BatchExpiryDate;
                invStockAdjustmentDetailTemp.LocationID = tempProduct.LocationID;
                invStockAdjustmentDetailTemp.OrderQty = tempProduct.Qty;
                invStockAdjustmentDetailTemp.BatchNo = tempProduct.BatchNo;
                invStockAdjustmentDetailTemp.ProductCode = tempProduct.ProductCode;
                invStockAdjustmentDetailTemp.ProductID = tempProduct.ProductID;
                invStockAdjustmentDetailTemp.ProductName = tempProduct.ProductName;
                invStockAdjustmentDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invStockAdjustmentDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invStockAdjustmentDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                invStockAdjustmentDetailTemp.LineNo = lineno;
                invStockAdjustmentDetailTempList.Add(invStockAdjustmentDetailTemp);
            }
            return invStockAdjustmentDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvProductSerialNoTemp> getPausedPurchaseSerialNoDetail(InvPurchaseHeader invPurchaseHeader)
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>();

            invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(invPurchaseHeader.PurchaseHeaderID) && ps.ReferenceDocumentDocumentID.Equals(invPurchaseHeader.DocumentID)).ToList();
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

        public InvStockAdjustmentHeader getStockAdjustmentHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public InvStockAdjustmentHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.InvStockAdjustmentHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvStockAdjustmentHeader getPausedInvStockAdjustmentHeaderByDocumentID(int documentDocumentID, long documentID, int locationID) 
        {
            return context.InvStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.InvStockAdjustmentHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, int locationID)
        {
            InvStockAdjustmentHeader invStockAdjustmentHeader = new InvStockAdjustmentHeader();
            invStockAdjustmentHeader = getStockAdjustmentHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
            if (invStockAdjustmentHeader == null)
                invStockAdjustmentHeader = new InvStockAdjustmentHeader();

            InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
            InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(invProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(invStockAdjustmentHeader.InvStockAdjustmentHeaderID) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(invProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
            if (InvProductSerialNoDetailtmp != null)
            {
                invStockAdjustmentHeader = getPausedInvStockAdjustmentHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                if (invStockAdjustmentHeader != null)
                    return invStockAdjustmentHeader.DocumentNo;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }

        }

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp invProductSerialNoTemp)
        {
            if (context.InvProductSerialNos.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.ExpiryDate.Equals(invProductSerialNoTemp.ExpiryDate) && sd.DocumentStatus.Equals(1)).FirstOrDefault() == null)
                return false;
            else
                return true;
        }

        public bool Save(InvStockAdjustmentHeader invStockAdjustmentHeader, List<InvStockAdjustmentDetailTemp> invStockAdjustmentDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, bool isExcess, bool isShortage, bool isOverWrite)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (invStockAdjustmentHeader.InvStockAdjustmentHeaderID.Equals(0))
                    context.InvStockAdjustmentHeaders.Add(invStockAdjustmentHeader);
                else
                    context.Entry(invStockAdjustmentHeader).State = EntityState.Modified;

                context.SaveChanges();

                List<InvStockAdjustmentDetail> invStockAdjustmentDetailDelete = new List<InvStockAdjustmentDetail>();
                invStockAdjustmentDetailDelete = context.InvStockAdjustmentDetails.Where(p => p.InvStockAdjustmentHeaderID.Equals(invStockAdjustmentHeader.InvStockAdjustmentHeaderID) && p.LocationID.Equals(invStockAdjustmentHeader.LocationID) && p.DocumentID.Equals(invStockAdjustmentHeader.DocumentID)).ToList();

                foreach (InvStockAdjustmentDetail invStockAdjustmentDetailDeleteTemp in invStockAdjustmentDetailDelete)
                {
                    context.InvStockAdjustmentDetails.Remove(invStockAdjustmentDetailDeleteTemp);
                }

                List<InvProductSerialNoDetail> invProductSerialNoDetailDelete = new List<InvProductSerialNoDetail>();
                invProductSerialNoDetailDelete = context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(invStockAdjustmentHeader.InvStockAdjustmentHeaderID) && p.LocationID.Equals(invStockAdjustmentHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invStockAdjustmentHeader.DocumentID)).ToList();

                foreach (InvProductSerialNoDetail invProductSerialNoDetailDeleteTemp in invProductSerialNoDetailDelete)
                {
                    context.InvProductSerialNoDetails.Remove(invProductSerialNoDetailDeleteTemp);
                }
                context.SaveChanges();

                foreach (InvStockAdjustmentDetailTemp invStockAdjustmentDetail in invStockAdjustmentDetailTemp)
                {
                    InvStockAdjustmentDetail invStockAdjustmentDetailSave = new InvStockAdjustmentDetail();

                    invStockAdjustmentDetailSave.CompanyID = invStockAdjustmentHeader.CompanyID;
                    invStockAdjustmentDetailSave.ConvertFactor = invStockAdjustmentDetail.ConvertFactor;
                    invStockAdjustmentDetailSave.CostCentreID = invStockAdjustmentHeader.CostCentreID;
                    invStockAdjustmentDetailSave.BatchNo = invStockAdjustmentDetail.BatchNo;
                    invStockAdjustmentDetailSave.CostPrice = invStockAdjustmentDetail.CostPrice;
                    invStockAdjustmentDetailSave.DocumentID = invStockAdjustmentHeader.DocumentID;
                    invStockAdjustmentDetailSave.DocumentStatus = invStockAdjustmentHeader.DocumentStatus;
                    invStockAdjustmentDetailSave.DocumentDate = invStockAdjustmentHeader.DocumentDate;
                    invStockAdjustmentDetailSave.GroupOfCompanyID = invStockAdjustmentHeader.GroupOfCompanyID;
                    invStockAdjustmentDetailSave.LineNo = invStockAdjustmentDetail.LineNo;
                    invStockAdjustmentDetailSave.LocationID = invStockAdjustmentHeader.LocationID;
                    invStockAdjustmentDetailSave.SellingValue = invStockAdjustmentDetail.SellingValue;
                    invStockAdjustmentDetailSave.SellingPrice = invStockAdjustmentDetail.SellingPrice;
                    invStockAdjustmentDetailSave.ExpiryDate = invStockAdjustmentDetail.ExpiryDate;
                    invStockAdjustmentDetailSave.CostValue = invStockAdjustmentDetail.CostValue;
                    invStockAdjustmentDetailSave.OrderQty = invStockAdjustmentDetail.OrderQty;
                    invStockAdjustmentDetailSave.ProductID = invStockAdjustmentDetail.ProductID;
                    invStockAdjustmentDetailSave.InvStockAdjustmentHeaderID = invStockAdjustmentHeader.InvStockAdjustmentHeaderID;
                    invStockAdjustmentDetailSave.OrderQty = invStockAdjustmentDetail.OrderQty;
                    invStockAdjustmentDetailSave.UnitOfMeasureID = invStockAdjustmentDetail.UnitOfMeasureID;
                    invStockAdjustmentDetailSave.BaseUnitID = invStockAdjustmentDetail.BaseUnitID;
                    invStockAdjustmentDetailSave.StockAdjustmentMode = invStockAdjustmentHeader.StockAdjustmentMode;

                    if (invStockAdjustmentHeader.DocumentStatus.Equals(1))
                    {
                        if (isExcess)
                        {
                            decimal currentStock = context.InvProductStockMasters.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();
                            invStockAdjustmentDetailSave.CurrentQty = currentStock;
                            context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + invStockAdjustmentDetail.OrderQty });

                            decimal currentBatchStock = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();
                            invStockAdjustmentDetailSave.BatchQty = currentBatchStock;
                            context.InvProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty + invStockAdjustmentDetail.OrderQty });
                            
                            
                            invStockAdjustmentDetailSave.ExcessQty = invStockAdjustmentDetail.OrderQty;
                            invStockAdjustmentDetailSave.ShortageQty = 0;
                            invStockAdjustmentDetailSave.OverWriteQty = 0;
                        }
                        else if (isShortage)
                        {
                            decimal currentStock = context.InvProductStockMasters.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();
                            invStockAdjustmentDetailSave.CurrentQty = currentStock;
                            context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock - invStockAdjustmentDetail.OrderQty });

                            decimal currentBatchStock = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();
                            invStockAdjustmentDetailSave.BatchQty = currentBatchStock;
                            context.InvProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty - invStockAdjustmentDetail.OrderQty });


                            invStockAdjustmentDetailSave.ShortageQty = invStockAdjustmentDetail.OrderQty;
                            invStockAdjustmentDetailSave.ExcessQty = 0;
                            invStockAdjustmentDetailSave.OverWriteQty = 0;
                        }
                        else if (isOverWrite)
                        {
                            decimal qty;
                            decimal currentStock = context.InvProductStockMasters.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.Stock).FirstOrDefault();

                            decimal batchqty;
                            decimal currentBatchStock = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID)).Select(ps => ps.BalanceQty).FirstOrDefault();

                            invStockAdjustmentDetailSave.OverWriteQty = invStockAdjustmentDetail.OrderQty;
                            invStockAdjustmentDetailSave.CurrentQty = currentStock;
                            invStockAdjustmentDetailSave.BatchQty = currentBatchStock;

                            if (invStockAdjustmentDetail.OrderQty > currentBatchStock)
                            {
                                batchqty = invStockAdjustmentDetail.OrderQty - currentBatchStock;
                                invStockAdjustmentDetailSave.ExcessQty = batchqty;
                                invStockAdjustmentDetailSave.ShortageQty = 0;
                                context.InvProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.UnitOfMeasureID.Equals(1)  && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty + batchqty });
                            }
                            else if (invStockAdjustmentDetail.OrderQty < currentBatchStock)
                            {
                                batchqty   = currentBatchStock - invStockAdjustmentDetail.OrderQty;
                                invStockAdjustmentDetailSave.ExcessQty = 0;
                                invStockAdjustmentDetailSave.ShortageQty = batchqty;
                                context.InvProductBatchNoExpiaryDetails.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.BatchNo.Equals(invStockAdjustmentDetail.BatchNo) && ps.UnitOfMeasureID.Equals(1) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductBatchNoExpiaryDetail { BalanceQty = ps.BalanceQty - batchqty });
                            }
                            else if (invStockAdjustmentDetail.OrderQty == currentBatchStock)
                            {
                                invStockAdjustmentDetailSave.ExcessQty = 0;
                                invStockAdjustmentDetailSave.ShortageQty = 0;
                            }






                            if (invStockAdjustmentDetail.OrderQty > currentStock)
                            {
                                qty = invStockAdjustmentDetail.OrderQty - currentStock;
                                invStockAdjustmentDetailSave.ExcessQty = qty;
                                invStockAdjustmentDetailSave.ShortageQty = 0;
                                context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock + qty });
                            }
                            else if (invStockAdjustmentDetail.OrderQty < currentStock)
                            {
                                qty = currentStock - invStockAdjustmentDetail.OrderQty;
                                invStockAdjustmentDetailSave.ExcessQty = 0;
                                invStockAdjustmentDetailSave.ShortageQty = qty;
                                context.InvProductStockMasters.Update(ps => ps.LocationID.Equals(invStockAdjustmentHeader.LocationID) && ps.ProductID.Equals(invStockAdjustmentDetail.ProductID), ps => new InvProductStockMaster { Stock = ps.Stock - qty });
                            }
                            else if (invStockAdjustmentDetail.OrderQty == currentBatchStock)
                            {
                                invStockAdjustmentDetailSave.ExcessQty = 0;
                                invStockAdjustmentDetailSave.ShortageQty = 0;
                            }
                        } 
                    }
                    context.InvStockAdjustmentDetails.Add(invStockAdjustmentDetailSave);
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
            List<string> documentNoList = context.InvStockAdjustmentHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }
        
        public string[] GetSavedAddDocumentNumbers(int documentID, int locationID)
        {
            List<string> addStockList = context.InvStockAdjustmentHeaders.Where(c => c.DocumentID == documentID && c.DocumentStatus == 1 && c.LocationID == locationID && c.StockAdjustmentMode == 1).Select(c => c.DocumentNo).ToList();
            return addStockList.ToArray();
        }
        public List<InvBarcodeDetailTemp> getSavedDocumentDetailsForBarCodePrint(string documentNo, int locationID, int documentID)
        {
            List<InvBarcodeDetailTemp> invBarcodeDetailTempList = new List<InvBarcodeDetailTemp>();
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();


            var barcodeDetailTemp = (from pm in context.InvProductMasters
                                     join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                     join pd in context.InvStockAdjustmentDetails on psm.ProductID equals pd.ProductID
                                     join ph in context.InvStockAdjustmentHeaders on pd.InvStockAdjustmentHeaderID equals ph.InvStockAdjustmentHeaderID
                                     join um in context.UnitOfMeasures on psm.UnitOfMeasureID equals um.UnitOfMeasureID
                                     join sc in context.Suppliers on pm.SupplierID equals sc.SupplierID
                                     where pm.IsDelete == false
                                     && pd.LocationID == psm.LocationID
                                     && pd.UnitOfMeasureID == psm.UnitOfMeasureID
                                     && pd.LocationID == ph.LocationID
                                     && pd.BatchNo == psm.BatchNo
                                     && psm.LocationID == locationID
                                     //&& ph.InvStockAdjustmentHeaderID == psm.ReferenceDocumentID
                                     //&& ph.DocumentID == psm.ReferenceDocumentDocumentID
                                     && ph.DocumentNo == documentNo
                                     && ph.DocumentID == documentID
                                     && ph.StockAdjustmentMode == 1 //Add
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
                                         //ph.ExpiryDate
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

        public List<InvProductBatchNoTemp> GetBatchNoDetail(InvProductMaster invProductMaster, int locationID) 
        {
            List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

            List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

            //invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.LocationID.Equals(locationID) && ps.BalanceQty > 0).ToList();
            invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.LocationID.Equals(locationID)).ToList();

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

        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public List<InvProductBatchNoTemp> GetExpiryDetail(InvProductMaster invProductMaster) 
        {
            List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

            List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

            invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID)).ToList();
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

        public InvStockAdjustmentHeader GetPausedInvStockAdjustmentHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvStockAdjustmentHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public List<InvStockAdjustmentDetailTemp> GetDeleteStockAdjustmentDetailTemp(List<InvStockAdjustmentDetailTemp> invStockAdjustmentTempDetail, InvStockAdjustmentDetailTemp invStockAdjustmentDetailTemp)
        {
            InvStockAdjustmentDetailTemp invStockAdjustmentTempDetailTemp = new InvStockAdjustmentDetailTemp();

            invStockAdjustmentTempDetailTemp = invStockAdjustmentTempDetail.Where(p => p.ProductID == invStockAdjustmentDetailTemp.ProductID && p.UnitOfMeasureID == invStockAdjustmentDetailTemp.UnitOfMeasureID).FirstOrDefault();

            if (invStockAdjustmentTempDetailTemp != null)
            {
                invStockAdjustmentTempDetail.Remove(invStockAdjustmentTempDetailTemp);
            }

            return invStockAdjustmentTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetStockAdjustmentDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            int stockAdjustmentMode = 0;
            var query = context.InvStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);
            if (autoGenerateInfo.FormName == "RptExcessStockAdjustment")
            {
                stockAdjustmentMode = 1;
                // Create query 
                query = context.InvStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else if (autoGenerateInfo.FormName == "RptShortageStockAdjustment")
            {
                stockAdjustmentMode = 2;
                query = context.InvStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "DepartmentID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join dp in context.InvDepartments.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.DepartmentID equals dp.InvDepartmentID
                                         select qr
                                  );
                                break;
                            case "CategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join dp in context.InvCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.CategoryID equals dp.InvCategoryID
                                         select qr
                                  );
                                break;
                            case "SubCategoryID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join dp in context.InvSubCategories.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategoryID equals dp.InvSubCategoryID
                                         select qr
                                  );
                                break;
                            case "SubCategory2ID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters on qr.ProductID equals pm.InvProductMasterID
                                         join dp in context.InvSubCategories2.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on pm.SubCategory2ID equals dp.InvSubCategory2ID
                                         select qr
                                  );
                                break;
                            case "InvProductMasterID":
                                query = (from qr in query
                                         join pm in context.InvProductMasters.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ProductID equals pm.InvProductMasterID
                                         select qr
                                  );
                                break;
                            case "UnitOfMeasureID":
                                query = (from qr in query
                                         join uom in context.UnitOfMeasures.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.UnitOfMeasureID equals uom.UnitOfMeasureID
                                         select qr
                                  );
                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
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
                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }
                    else
                    {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}
                }
            }

            //var queryResultData = (dynamic)null;
            DataTable dtResult = new DataTable();
          
            if (stockAdjustmentMode == 1 || stockAdjustmentMode == 2)
            {
                var queryResult = (from ph in query
                                   join pd in context.InvStockAdjustmentHeaders on ph.InvStockAdjustmentHeaderID equals pd.InvStockAdjustmentHeaderID
                                   join pm in context.InvProductMasters on ph.ProductID equals pm.InvProductMasterID
                                   join um in context.UnitOfMeasures on ph.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                                   join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                                   join s in context.InvSubCategories on pm.SubCategoryID equals s.InvSubCategoryID
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
                                           FieldString2 = g.Key.LocationCode + " " + g.Key.LocationName,
                                           FieldString3 = g.Key.DepartmentCode + " " + g.Key.DepartmentName,
                                           FieldString4 = g.Key.CategoryCode + " " + g.Key.CategoryName,
                                           FieldString5 = g.Key.SubCategoryCode + " " + g.Key.SubCategoryName,
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
                                   join pd in context.InvStockAdjustmentHeaders on ph.InvStockAdjustmentHeaderID equals pd.InvStockAdjustmentHeaderID
                                   join pm in context.InvProductMasters on ph.ProductID equals pm.InvProductMasterID
                                   join um in context.UnitOfMeasures on ph.UnitOfMeasureID equals um.UnitOfMeasureID
                                   join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                                   join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                                   join s in context.InvSubCategories on pm.SubCategoryID equals s.InvSubCategoryID
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
                                           FieldString2 = g.Key.LocationCode + " " + g.Key.LocationName,
                                           FieldString3 = g.Key.DepartmentCode + " " + g.Key.DepartmentName,
                                           FieldString4 = g.Key.CategoryCode + " " + g.Key.CategoryName,
                                           FieldString5 = g.Key.SubCategoryCode + " " + g.Key.SubCategoryName,
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

        public DataTable GetStockAdjustmentDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;

            var query = context.InvStockAdjustmentHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);
            
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

            
            string refStockAdjustmentMode = ((int)LookUpReference.StockAdjustmentMode).ToString();

            var result = (from h in query
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

                    dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString()
                                                 .Replace("@p__linq__0", refStockAdjustmentMode.Trim()));

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

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
            autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
            int documentID = autoGeneratePurchaseInfo.DocumentID;
            int stockAdjustmentMode = 0;
            IQueryable qryResult;
            if (autoGenerateInfo.FormName == "FrmStockAdjustment")
            {
                qryResult = context.InvStockAdjustmentHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, autoGeneratePurchaseInfo.DocumentID);
            }
            else if(autoGenerateInfo.FormName=="RptExcessStockAdjustment")
            { 
                stockAdjustmentMode = 1;
                // Create query 
                qryResult = context.InvStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else if (autoGenerateInfo.FormName == "RptShortageStockAdjustment")
            { 
                stockAdjustmentMode = 2;
                qryResult = context.InvStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1 AND StockAdjustmentMode==@2", 1, documentID, stockAdjustmentMode);
            }
            else
            {
                qryResult = context.InvStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1", 1, documentID);
            }
            
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "InvProductMasterID":
                    //qryResult = null;
                    //qryResult = context.InvStockAdjustmentDetails.Where("IsDelete == @0", false);
                    qryResult = qryResult.Join(context.InvProductMasters, "ProductID", reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                case "DepartmentID":
                    //qryResult = null;
                    //qryResult = context.InvStockAdjustmentDetails.Where("IsDelete == @0", false);
                    qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID", "new(inner.InvProductMasterID, inner.DepartmentID)")
                                .OrderBy("DepartmentID")
                                .GroupBy("new(DepartmentID)", "new(DepartmentID)")
                                .Select("new(Key.DepartmentID)");
                    qryResult = qryResult.Join(context.InvDepartments, "DepartmentID", "InvDepartmentID", "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                case "CategoryID":
                    //qryResult = null;
                    //qryResult = context.InvStockAdjustmentDetails.Where("IsDelete == @0", false);
                    qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID", "new(inner.InvProductMasterID, inner.CategoryID)")
                                .OrderBy("CategoryID")
                                .GroupBy("new(CategoryID)", "new(CategoryID)")
                                .Select("new(Key.CategoryID)");
                    qryResult = qryResult.Join(context.InvCategories, "CategoryID", "InvCategoryID", "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                case "SubCategoryID":
                    //qryResult = null;
                    //qryResult = context.InvStockAdjustmentDetails.Where("IsDelete == @0", false);
                    qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID", "new(inner.InvProductMasterID, inner.SubCategoryID)")
                                .OrderBy("SubCategoryID")
                                .GroupBy("new(SubCategoryID)", "new(SubCategoryID)")
                                .Select("new(Key.SubCategoryID)");
                    qryResult = qryResult.Join(context.InvSubCategories, "SubCategoryID", "InvSubCategoryID", "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                case "UnitOfMeasureID":
                    //qryResult = null;
                    //qryResult = context.InvProductMasters.Where("IsDelete == @0", false);
                    qryResult = qryResult.Join(context.UnitOfMeasures, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                    break;
                    
                case "StockAdjustmentMode":                                        
                    qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", ((int)LookUpReference.StockAdjustmentMode).ToString()), "StockAdjustmentMode", "LookupKey", "new(inner.LookupValue)").
                                OrderBy("LookupValue").Select("LookupValue");
                    break;
                case "LocationID":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")", "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
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
                        from ph in context.InvStockAdjustmentHeaders
                        join pd in context.InvStockAdjustmentDetails on ph.InvStockAdjustmentHeaderID equals pd.InvStockAdjustmentHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
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
                                FieldString29 = "",
                                FieldString30 = ph.CreatedUser,
                            });

           return query.AsEnumerable().ToDataTable();

        }

        public string[] GetAllProductCodes()
        {
            //List<string> ProductCodeList = context.InvProductMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.ProductCode).ToList();

            //List<string> ProductCodeList = context.InvProductMasters.Join(context.InvProductBatchNoExpiaryDetails ,
            //                                s => new { s.InvProductMasterID, s.UnitOfMeasureID },
            //           h => new { h.ProductID, h.UnitOfMeasureID },
            //           (s, h) => s);

            //return ProductCodeList.ToArray();
            List<string> ProductCodeList = context.InvProductMasters.Where(b => b.IsDelete.Equals(false))
                                                            .DefaultIfEmpty()
                                                            .Join(context.InvProductBatchNoExpiaryDetails.Where(b => b.UnitOfMeasureID.Equals(1)),
                                                            b => b.InvProductMasterID,
                                                            c => c.ProductID,
                                                            (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode).ToList();

            return ProductCodeList.ToArray(); 
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

    }
}
