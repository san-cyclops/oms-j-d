using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;

using Domain;
using Data;
using System.Data;
using Utility;

namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class LgsMaterialConsumptionService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

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
                { preFix = "X"; }
                else
                { preFix = "T"; }
            }

            if (isLocationCode)
            { preFix = preFix + locationCode.Trim(); }

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

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        public LgsMaterialConsumptionHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsMaterialConsumptionHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// /// Create LgsMaterialConsumptionDetail list using LgsMaterialConsumptionDetailTemp
        /// </summary>
        /// <param name="lgsMaterialConsumptionHeader"></param>
        /// <returns></returns>
        public List<LgsMaterialConsumptionDetailTemp> GetLgsMaterialConsumptionDetails(LgsMaterialConsumptionHeader lgsMaterialConsumptionHeader)
        {
            // LgsMaterialConsumptionDetail list for LgsMaterialConsumptionHeader 
            List<LgsMaterialConsumptionDetailTemp> lgsMaterialConsumptionDetailsTemp = new List<LgsMaterialConsumptionDetailTemp>();
            List<LgsMaterialConsumptionDetail> lgsMaterialConsumptionDetails = new List<LgsMaterialConsumptionDetail>();
            lgsMaterialConsumptionDetails = lgsMaterialConsumptionHeader.LgsMaterialConsumptionDetails.ToList();
            foreach (LgsMaterialConsumptionDetail lgsMaterialConsumptionDetail in lgsMaterialConsumptionDetails)
            {
                LgsMaterialConsumptionDetailTemp lgsMaterialConsumptionDetailTemp = new LgsMaterialConsumptionDetailTemp();

                lgsMaterialConsumptionDetailTemp.LineNo = lgsMaterialConsumptionDetail.LineNo;
                lgsMaterialConsumptionDetailTemp.ProductID = lgsMaterialConsumptionDetail.ProductID;
                lgsMaterialConsumptionDetailTemp.ProductCode = lgsMaterialConsumptionDetail.ProductCode;
                lgsMaterialConsumptionDetailTemp.UnitOfMeasureID = lgsMaterialConsumptionDetail.BaseUnitID;
                lgsMaterialConsumptionDetailTemp.UnitOfMeasure = lgsMaterialConsumptionDetail.UnitOfMeasure;
                lgsMaterialConsumptionDetailTemp.ConvertFactor = lgsMaterialConsumptionDetail.ConvertFactor;
                lgsMaterialConsumptionDetailTemp.OrderQty = lgsMaterialConsumptionDetail.OrderQty;
                lgsMaterialConsumptionDetailTemp.Qty = lgsMaterialConsumptionDetail.Qty;
                
                lgsMaterialConsumptionDetailsTemp.Add(lgsMaterialConsumptionDetailTemp);
            }
            
            return lgsMaterialConsumptionDetailsTemp;
        }

        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="lgsMaterialConsumptionDetailTemp"></param>
        /// <returns></returns>
        public List<LgsMaterialConsumptionDetailTemp> GetLgsMaterialConsumptionDetailTempList(List<LgsMaterialConsumptionDetailTemp> lgsMaterialConsumptionDetailsTemp, LgsMaterialConsumptionDetailTemp lgsMaterialConsumptionDetailTemp)
        {
            if (lgsMaterialConsumptionDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialConsumptionDetailTemp.ProductCode && pr.UnitOfMeasure == lgsMaterialConsumptionDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                // Update Existing Record
                lgsMaterialConsumptionDetailTemp.LineNo = lgsMaterialConsumptionDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialConsumptionDetailTemp.ProductCode).FirstOrDefault().LineNo;
                lgsMaterialConsumptionDetailsTemp.RemoveAll(pr => pr.ProductCode == lgsMaterialConsumptionDetailTemp.ProductCode);
                lgsMaterialConsumptionDetailsTemp.Add(lgsMaterialConsumptionDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsMaterialConsumptionDetailsTemp.Count, 0))
                { lgsMaterialConsumptionDetailTemp.LineNo = 1; }
                else
                { lgsMaterialConsumptionDetailTemp.LineNo = lgsMaterialConsumptionDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsMaterialConsumptionDetailsTemp.Add(lgsMaterialConsumptionDetailTemp);
            }
            return lgsMaterialConsumptionDetailsTemp;
        }

        /// <summary>
        /// Get LgsMaterialConsumptionHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaterialConsumptionHeader GetLgsMaterialConsumptionHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaterialConsumptionHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Save Material Consumption note
        /// </summary>
        /// <param name="lgsMaterialConsumptionHeader"></param>
        /// <param name="lgsMaterialConsumptionDetailsTemp"></param>
        /// <returns></returns>
        public bool SaveMaterialConsumptionNote(LgsMaterialConsumptionHeader lgsMaterialConsumptionHeader, List<LgsMaterialConsumptionDetailTemp> lgsMaterialConsumptionDetailsTemp)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {
                context.LgsMaterialConsumptionHeaders.Add(AddLgsMaterialConsumptionDetails(lgsMaterialConsumptionHeader, lgsMaterialConsumptionDetailsTemp));
                this.context.SaveChanges();
                transactionScope.Complete();
                return true;
            }
        }

        /// <summary>
        /// Create LgsMaterialConsumptionDetail list using LgsMaterialConsumptionDetailTemp
        /// </summary>
        /// <param name="lgsMaterialConsumptionHeader"></param>
        /// <param name="lgsMaterialConsumptionDetailsTemp"></param>
        /// <returns></returns>
        private LgsMaterialConsumptionHeader AddLgsMaterialConsumptionDetails(LgsMaterialConsumptionHeader lgsMaterialConsumptionHeader, List<LgsMaterialConsumptionDetailTemp> lgsMaterialConsumptionDetailsTemp)
        {
            // LgsMaterialConsumptionDetail list for LgsMaterialConsumptionHeader 
            lgsMaterialConsumptionHeader.LgsMaterialConsumptionDetails = new List<LgsMaterialConsumptionDetail>();
            foreach (LgsMaterialConsumptionDetailTemp lgsMaterialConsumptionDetailTemp in lgsMaterialConsumptionDetailsTemp)
            {
                LgsMaterialConsumptionDetail lgsMaterialConsumptionDetail = new LgsMaterialConsumptionDetail();

                lgsMaterialConsumptionDetail.DocumentStatus = lgsMaterialConsumptionHeader.DocumentStatus;
                lgsMaterialConsumptionDetail.DocumentNo = lgsMaterialConsumptionHeader.DocumentNo;
                lgsMaterialConsumptionDetail.DocumentDate = lgsMaterialConsumptionHeader.DocumentDate;
                lgsMaterialConsumptionDetail.CompanyID = lgsMaterialConsumptionHeader.CompanyID;
                lgsMaterialConsumptionDetail.LocationID = lgsMaterialConsumptionHeader.LocationID;
                lgsMaterialConsumptionDetail.CostCentreID = lgsMaterialConsumptionHeader.CostCentreID;
                lgsMaterialConsumptionDetail.CreatedDate = lgsMaterialConsumptionHeader.CreatedDate;
                lgsMaterialConsumptionDetail.DocumentID = lgsMaterialConsumptionHeader.DocumentID;
                lgsMaterialConsumptionDetail.DataTransfer = lgsMaterialConsumptionHeader.DataTransfer;
                lgsMaterialConsumptionDetail.ModifiedDate = lgsMaterialConsumptionHeader.ModifiedDate;

                lgsMaterialConsumptionDetail.LgsMaterialConsumptionDetailID = lgsMaterialConsumptionDetailTemp.LgsMaterialConsumptionDetailTempID;
                lgsMaterialConsumptionDetail.LineNo = lgsMaterialConsumptionDetailTemp.LineNo;
                lgsMaterialConsumptionDetail.ProductID = lgsMaterialConsumptionDetailTemp.ProductID;
                lgsMaterialConsumptionDetail.ProductCode = lgsMaterialConsumptionDetailTemp.ProductCode;
                lgsMaterialConsumptionDetail.UnitOfMeasureID = lgsMaterialConsumptionDetailTemp.UnitOfMeasureID;
                lgsMaterialConsumptionDetail.BaseUnitID = lgsMaterialConsumptionDetailTemp.BaseUnitID;
                lgsMaterialConsumptionDetail.UnitOfMeasure = lgsMaterialConsumptionDetailTemp.UnitOfMeasure;
                lgsMaterialConsumptionDetail.ConvertFactor = lgsMaterialConsumptionDetailTemp.ConvertFactor;
                lgsMaterialConsumptionDetail.OrderQty = lgsMaterialConsumptionDetailTemp.OrderQty;
                lgsMaterialConsumptionDetail.Qty = lgsMaterialConsumptionDetailTemp.Qty;
                lgsMaterialConsumptionDetail.CostPrice = lgsMaterialConsumptionDetailTemp.CostPrice;
                lgsMaterialConsumptionDetail.GrossAmount = lgsMaterialConsumptionDetailTemp.GrossAmount;
                

                lgsMaterialConsumptionHeader.LgsMaterialConsumptionDetails.Add(lgsMaterialConsumptionDetail);
            }

            return lgsMaterialConsumptionHeader;
        }


        #endregion
    }
}
