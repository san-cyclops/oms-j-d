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

namespace Service
{
    public class InvBarCodeService
    {
        //By Pravin
        ERPDbContext context = new ERPDbContext();

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


        public InvBarcodeDetailTemp getBarCodeDetailTemp(List<InvBarcodeDetailTemp> InvBarcodeDetailTemp, InvProductMaster invProductMaster, int locationID, DateTime expiryDate, long unitOfMeasureID, string batchNo)
        {
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();
            decimal defaultConvertFactor = 1;
            var barcodeDetailTemp = (from pm in context.InvProductMasters
                                     join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                     join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                     join sc in context.Suppliers on pm.SupplierID equals sc.SupplierID
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
                                         psm.BalanceQty,
                                         sc.SupplierCode
                                     }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(invProductMaster.UnitOfMeasureID))
            {
                barcodeDetailTemp = (dynamic)null;

                barcodeDetailTemp = (from pm in context.InvProductMasters
                                        join psm in context.InvProductBatchNoExpiaryDetails on pm.InvProductMasterID equals psm.ProductID
                                        join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                        join sc in context.Suppliers on pm.SupplierID equals sc.SupplierID
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
                                            psm.BalanceQty,
                                            sc.SupplierCode
                                        }).ToArray();
            }

           
            foreach (var tempProduct in barcodeDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                { invBarcodeDetailTemp = InvBarcodeDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.BatchNo.Equals(batchNo)).FirstOrDefault(); }
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                { invBarcodeDetailTemp = InvBarcodeDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID && p.ExpiryDate == expiryDate && p.BatchNo.Equals(batchNo)).FirstOrDefault(); }
                else
                { invBarcodeDetailTemp = InvBarcodeDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.BatchNo.Equals(batchNo)).FirstOrDefault(); }

                if (invBarcodeDetailTemp == null)
                {
                    invBarcodeDetailTemp = new InvBarcodeDetailTemp();
                    invBarcodeDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invBarcodeDetailTemp.ProductCode = tempProduct.ProductCode;
                    invBarcodeDetailTemp.ProductName = tempProduct.ProductName;
                    invBarcodeDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invBarcodeDetailTemp.CostPrice = tempProduct.CostPrice;
                    invBarcodeDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invBarcodeDetailTemp.UnitOfMeasureName = tempProduct.UnitOfMeasureName;
                    invBarcodeDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    invBarcodeDetailTemp.Stock = tempProduct.BalanceQty;
                    invBarcodeDetailTemp.SupplierCode = tempProduct.SupplierCode;
                }
            }

            return invBarcodeDetailTemp;
        }


        public List<InvBarcodeDetailTemp> getUpdateBarCodeDetailTemp(List<InvBarcodeDetailTemp> invBarcodeDetailTempList, InvBarcodeDetailTemp invBarcodeDetail)
        {
            InvBarcodeDetailTemp invBarcodeDetailTemp = new InvBarcodeDetailTemp();


            //invBarcodeDetailTemp = invBarcodeDetailTempList.Where(p => p.ProductID == invBarcodeDetail.ProductID && p.BatchNo == invBarcodeDetail.BatchNo).FirstOrDefault();
            invBarcodeDetailTemp = invBarcodeDetailTempList.Where(p => p.ProductID == invBarcodeDetail.ProductID && p.UnitOfMeasureID == invBarcodeDetail.UnitOfMeasureID && p.BatchNo == invBarcodeDetail.BatchNo).FirstOrDefault();
            
            if (invBarcodeDetailTemp == null || invBarcodeDetailTemp.LineNo.Equals(0))
            {
                invBarcodeDetailTemp = invBarcodeDetail;
                if (invBarcodeDetailTempList.Count.Equals(0))
                {invBarcodeDetailTemp.LineNo = 1;}
                else
                { invBarcodeDetailTemp.LineNo = invBarcodeDetailTempList.Max(s => s.LineNo) + 1;}
            }
            else
            {
                invBarcodeDetailTempList.Remove(invBarcodeDetailTemp);
                invBarcodeDetail.LineNo = invBarcodeDetailTemp.LineNo;
                invBarcodeDetailTemp = invBarcodeDetail;
            }

            invBarcodeDetailTempList.Add(invBarcodeDetailTemp);

            return invBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvBarcodeDetailTemp> getDeleteBarCodeDetailTemp(List<InvBarcodeDetailTemp> invBarcodeDetailTempList, InvBarcodeDetailTemp invBarcodeDetail)
        {
            InvBarcodeDetailTemp invBarcodeDetailTemp = new Domain.InvBarcodeDetailTemp();

            invBarcodeDetailTemp = invBarcodeDetailTempList.Where(p => p.ProductCode == invBarcodeDetail.ProductCode && p.BatchNo == invBarcodeDetail.BatchNo).FirstOrDefault();
            long removedLineNo = 0;
            if (invBarcodeDetailTemp != null)
            {
                invBarcodeDetailTempList.Remove(invBarcodeDetailTemp);
                removedLineNo = invBarcodeDetailTemp.LineNo;
            }


           // invBarcodeDetailTempList.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            return invBarcodeDetailTempList.OrderBy(pd => pd.LineNo).ToList();
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

        public List<InvProductBatchNoTemp> GetExpiryDetail(InvProductMaster invProductMaster, string batchNo)
        {
            List<InvProductBatchNoTemp> invProductBatchNoTempList = new List<InvProductBatchNoTemp>();

            List<InvProductBatchNoExpiaryDetail> invProductBatchNoDetail = new List<InvProductBatchNoExpiaryDetail>();

            invProductBatchNoDetail = context.InvProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(invProductMaster.InvProductMasterID) && ps.BatchNo.Equals(batchNo)).ToList();
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
    }
}
