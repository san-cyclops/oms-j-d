using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using MoreLinq;

using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using System.Collections;

namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class LgsMaterialAllocationService
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
        public LgsMaterialAllocationHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsMaterialAllocationHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// /// Create LgsMaterialAllocationDetail list using LgsMaterialAllocationDetailTemp
        /// </summary>
        /// <param name="lgsMaterialAllocationHeader"></param>
        /// <returns></returns>
        public List<LgsMaterialAllocationDetailTemp> GetLgsMaterialAllocationDetails(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        {            
            // LgsMaterialAllocationDetail list for LgsMaterialAllocationtHeader 
            List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp = new List<LgsMaterialAllocationDetailTemp>();
            
            var tempProductList = (from d in context.LgsMaterialAllocationDetails
                                   join p in context.LgsProductMasters on d.ProductID equals p.LgsProductMasterID
                                   where d.DocumentStatus.Equals(0) && d.LgsMaterialAllocationHeaderID.Equals(lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID)
                                   select new
                                   {
                                       d.LgsMaterialAllocationDetailID,
                                       d.LineNo,
                                       d.ProductID,
                                       p.ProductCode,
                                       p.ProductName,
                                       d.BaseUnitID,
                                       d.UnitOfMeasureID,
                                       d.UnitOfMeasure,
                                       d.ConvertFactor,
                                       d.RequestNo,
                                       d.OrderQty,
                                       d.BalanceQty,
                                       d.Qty,
                                       p.CostPrice,
                                   }).ToArray();


            foreach (var tempProduct in tempProductList)
            {
                LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

                lgsMaterialAllocationDetailTemp.LineNo = tempProduct.LineNo;
                lgsMaterialAllocationDetailTemp.ProductID = tempProduct.ProductID;
                lgsMaterialAllocationDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsMaterialAllocationDetailTemp.ProductName = tempProduct.ProductName;
                lgsMaterialAllocationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsMaterialAllocationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasure;
                lgsMaterialAllocationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsMaterialAllocationDetailTemp.Qty = tempProduct.Qty;
                lgsMaterialAllocationDetailTemp.RequestLocationID = Common.LoggedLocationID;
                lgsMaterialAllocationDetailTemp.RequestLocationCode = Common.LoggedLocationCode;
                lgsMaterialAllocationDetailTemp.RequestNo = tempProduct.RequestNo == null ? "" : tempProduct.RequestNo;
                lgsMaterialAllocationDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsMaterialAllocationDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((tempProduct.CostPrice * tempProduct.BalanceQty).ToString());

                lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            }

            //List<LgsMaterialAllocationDetail> lgsMaterialAllocationDetails = new List<LgsMaterialAllocationDetail>();
            //lgsMaterialAllocationDetails = lgsMaterialAllocationHeader.LgsMaterialAllocationDetails.ToList();
            //foreach (LgsMaterialAllocationDetail lgsMaterialAllocationDetail in lgsMaterialAllocationDetails)
            //{
            //    LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

            //    //lgsMaterialAllocationDetailTemp.LgsMaterialRequestDetailTempID = lgsMaterialAllocationDetail.LgsMaterialRequestDetailID;
            //    lgsMaterialAllocationDetailTemp.LineNo = lgsMaterialAllocationDetail.LineNo;
            //    lgsMaterialAllocationDetailTemp.ProductID = lgsMaterialAllocationDetail.ProductID;
            //    lgsMaterialAllocationDetailTemp.ProductCode = lgsMaterialAllocationDetail.ProductCode;
            //    lgsMaterialAllocationDetailTemp.UnitOfMeasureID = lgsMaterialAllocationDetail.BaseUnitID;
            //    lgsMaterialAllocationDetailTemp.UnitOfMeasure = lgsMaterialAllocationDetail.UnitOfMeasure;
            //    lgsMaterialAllocationDetailTemp.ConvertFactor = lgsMaterialAllocationDetail.ConvertFactor;
            //    lgsMaterialAllocationDetailTemp.OrderQty = lgsMaterialAllocationDetail.OrderQty;
            //    lgsMaterialAllocationDetailTemp.BalanceQty = lgsMaterialAllocationDetail.Qty;
            //    lgsMaterialAllocationDetailTemp.RequestLocationID = lgsMaterialAllocationDetail.RequestLocationID;
            //    lgsMaterialAllocationDetailTemp.RequestLocationCode = lgsMaterialAllocationDetail.RequestLocationCode;
            //    lgsMaterialAllocationDetailTemp.RequestNo = lgsMaterialAllocationDetail.RequestNo == null ? "" : lgsMaterialAllocationDetail.RequestNo;

            //    lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            //}
            
            return lgsMaterialAllocationDetailsTemp;
        }

        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="lgsMaterialAllocationDetailTemp"></param>
        /// <returns></returns>
        public List<LgsMaterialAllocationDetailTemp> GetLgsMaterialAllocationDetailTempList(List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp, LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp)
        {
            if (lgsMaterialAllocationDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialAllocationDetailTemp.ProductCode && pr.UnitOfMeasure == lgsMaterialAllocationDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                // Update Existing Record
                lgsMaterialAllocationDetailTemp.LineNo = lgsMaterialAllocationDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialAllocationDetailTemp.ProductCode).FirstOrDefault().LineNo;
                lgsMaterialAllocationDetailsTemp.RemoveAll(pr => pr.ProductCode == lgsMaterialAllocationDetailTemp.ProductCode);
                lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsMaterialAllocationDetailsTemp.Count, 0))
                { lgsMaterialAllocationDetailTemp.LineNo = 1; }
                else
                { lgsMaterialAllocationDetailTemp.LineNo = lgsMaterialAllocationDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            }
            return lgsMaterialAllocationDetailsTemp;
        }

        /// <summary>
        /// Get LgsMaterialAllocationHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaterialAllocationHeader GetLgsMaterialAllocationHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaterialAllocationHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Save Material Allocation note
        /// </summary>
        /// <param name="lgsMaterialAllocationHeader"></param>
        /// <param name="lgsMaterialAllocationDetailsTemp"></param>
        /// <returns></returns>
        public bool SaveMaterialAllocationNote(LgsMaterialAllocationHeader lgsMaterialAllocationHeader, List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp)
        {            
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID.Equals(0))
                {
                    context.LgsMaterialAllocationHeaders.Add(AddLgsMaterialAllocationDetails(lgsMaterialAllocationHeader, lgsMaterialAllocationDetailsTemp));
                    this.context.SaveChanges();
                    lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID = lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID;
                    this.context.Entry(lgsMaterialAllocationHeader).State = EntityState.Modified;
                    
                    if (lgsMaterialAllocationHeader.DocumentStatus.Equals(1))
                    {
                        // Update Reference document product balances
                        UpdateReferenceDocumentProductBalances(lgsMaterialAllocationHeader);                        
                    }
                    this.context.SaveChanges();
                }
                else
                {
                    // Delete existing records
                    var deleteList = context.LgsMaterialAllocationDetails.Where(d => d.LgsMaterialAllocationHeaderID == lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID);
                    foreach (var lgsMaterialAllocationDetails in deleteList)
                    {
                        context.LgsMaterialAllocationDetails.Remove(lgsMaterialAllocationDetails);
                    }

                    lgsMaterialAllocationHeader.LgsMaterialAllocationDetails.Clear();
                    lgsMaterialAllocationHeader = AddLgsMaterialAllocationDetails(lgsMaterialAllocationHeader, lgsMaterialAllocationDetailsTemp);                    
                    this.context.Entry(lgsMaterialAllocationHeader).State = EntityState.Modified;
                    this.context.SaveChanges();
                }
                
                transactionScope.Complete();
                return true;
            }
        }

        /// <summary>
        /// Update Reference Document Product Balances of Material Allocation Note
        /// </summary>
        /// <param name="lgsMaterialAllocationHeader"></param>
        private void UpdateReferenceDocumentProductBalances(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        {

            if (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaintenanceJobAssignNote").DocumentID.Equals(lgsMaterialAllocationHeader.ReferenceDocumentDocumentID))
            {
                foreach (var lgsMaterialAllocationDetails in lgsMaterialAllocationHeader.LgsMaterialAllocationDetails)
                {
                    context.LgsMaintenanceJobAssignProductDetails.Update(sd => sd.LocationID.Equals(lgsMaterialAllocationDetails.LocationID) &&
                        sd.ProductID.Equals(lgsMaterialAllocationDetails.ProductID) && sd.LgsMaintenanceJobAssignHeaderID.Equals(lgsMaterialAllocationHeader.ReferenceDocumentID),
                        sd => new LgsMaintenanceJobAssignProductDetail { BalanceQty = sd.BalanceQty - lgsMaterialAllocationDetails.Qty });
                }
            }
            else if (AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialRequestNote").DocumentID.Equals(lgsMaterialAllocationHeader.ReferenceDocumentDocumentID))
            {
                foreach (var lgsMaterialAllocationDetails in lgsMaterialAllocationHeader.LgsMaterialAllocationDetails)
                {
                    context.LgsMaterialRequestDetails.Update(sd => sd.ProductID.Equals(lgsMaterialAllocationDetails.ProductID) && sd.LgsMaterialRequestHeaderID.Equals(lgsMaterialAllocationHeader.ReferenceDocumentID),
                        sd => new LgsMaterialRequestDetail { BalanceQty = sd.BalanceQty - lgsMaterialAllocationDetails.Qty });
                }
            }            
        }


        ///// <summary>
        ///// Update ReferenceDocumentDocumentID, ReferenceDocumentID fields in LgsMaintenanceJobAssignHeader 
        ///// </summary>
        ///// <param name="lgsMaterialAllocationHeader"></param>
        //public void UpdateLgsMaintenanceJobAssignHeader(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        //{
        //    //ERPDbContext context2 = new ERPDbContext();
        //    LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader = new LgsMaintenanceJobAssignHeader();
        //    LgsMaintenanceJobAssignService lgsMaintenanceJobAssignService = new LgsMaintenanceJobAssignService();
            
        //    lgsMaintenanceJobAssignHeader = lgsMaintenanceJobAssignService.GetLgsMaintenanceJobAssignHeaderByDocumentID(lgsMaterialAllocationHeader.LgsMaintenanceJobAssignHeaderID);
        //    lgsMaintenanceJobAssignHeader.ReferenceDocumentDocumentID = lgsMaterialAllocationHeader.DocumentID;
        //    lgsMaintenanceJobAssignHeader.ReferenceDocumentID = lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID;
        //    context.Entry(lgsMaintenanceJobAssignHeader).State = EntityState.Modified;
        //    context.SaveChanges();
        //}


        /// <summary>
        /// Update ReferenceDocumentDocumentID, ReferenceDocumentID fields in LgsMaintenanceJobRequisitionHeader 
        /// </summary>fgfgfgfgfgfgfgfgfgfgfgfgf
        /// <param name="lgsMaintefnanceJobAssignHeader"></param>
        public void UpdateLgsMaintenanceJobRequisitionHeader(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        {

            LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader();
            LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();

            lgsMaintenanceJobRequisitionHeader = lgsMaintenanceJobRequisitionService.GetLgsMaintenanceJobRequisitionHeaderByDocumentID(lgsMaintenanceJobAssignHeader.LgsMaintenanceJobRequisitionHeaderID);
            lgsMaintenanceJobRequisitionHeader.ReferenceDocumentDocumentID = lgsMaintenanceJobAssignHeader.DocumentID;
            lgsMaintenanceJobRequisitionHeader.ReferenceDocumentID = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID;
            this.context.Entry(lgsMaintenanceJobRequisitionHeader).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Create LgsMaterialRequestDetail list using LgsMaterialRequestDetailsTemp
        /// </summary>
        /// <param name="lgsMaterialAllocationHeader"></param>
        /// <param name="lgsMaterialAllocationDetailsTemp"></param>
        /// <returns></returns>
        private LgsMaterialAllocationHeader AddLgsMaterialAllocationDetails(LgsMaterialAllocationHeader lgsMaterialAllocationHeader, List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp)
        {
            // LgsMaterialAllocationDetail list for LgsMaterialAllocationHeader 
            lgsMaterialAllocationHeader.LgsMaterialAllocationDetails = new List<LgsMaterialAllocationDetail>();
            foreach (LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp in lgsMaterialAllocationDetailsTemp)
            {
                LgsMaterialAllocationDetail lgsMaterialAllocationDetail = new LgsMaterialAllocationDetail();

                lgsMaterialAllocationDetail.DocumentStatus = lgsMaterialAllocationHeader.DocumentStatus;
                lgsMaterialAllocationDetail.DocumentNo = lgsMaterialAllocationHeader.DocumentNo;
                lgsMaterialAllocationDetail.DocumentDate = lgsMaterialAllocationHeader.DocumentDate;
                lgsMaterialAllocationDetail.CompanyID = lgsMaterialAllocationHeader.CompanyID;
                lgsMaterialAllocationDetail.LocationID = lgsMaterialAllocationHeader.LocationID;
                lgsMaterialAllocationDetail.CostCentreID = lgsMaterialAllocationHeader.CostCentreID;
                lgsMaterialAllocationDetail.CreatedDate = lgsMaterialAllocationHeader.CreatedDate;
                lgsMaterialAllocationDetail.DocumentID = lgsMaterialAllocationHeader.DocumentID;
                lgsMaterialAllocationDetail.DataTransfer = lgsMaterialAllocationHeader.DataTransfer;
                lgsMaterialAllocationDetail.ModifiedDate = lgsMaterialAllocationHeader.ModifiedDate;

                lgsMaterialAllocationDetail.LgsMaterialAllocationDetailID = lgsMaterialAllocationDetailTemp.LgsMaterialAllocationDetailTempID;
                lgsMaterialAllocationDetail.LineNo = lgsMaterialAllocationDetailTemp.LineNo;
                lgsMaterialAllocationDetail.ProductID = lgsMaterialAllocationDetailTemp.ProductID;
                lgsMaterialAllocationDetail.ProductCode = lgsMaterialAllocationDetailTemp.ProductCode;
                lgsMaterialAllocationDetail.UnitOfMeasureID = lgsMaterialAllocationDetailTemp.UnitOfMeasureID;
                lgsMaterialAllocationDetail.BaseUnitID = lgsMaterialAllocationDetailTemp.BaseUnitID;
                lgsMaterialAllocationDetail.UnitOfMeasure = lgsMaterialAllocationDetailTemp.UnitOfMeasure;
                lgsMaterialAllocationDetail.ConvertFactor = lgsMaterialAllocationDetailTemp.ConvertFactor;
                lgsMaterialAllocationDetail.RequestLocationID = lgsMaterialAllocationDetailTemp.RequestLocationID;
                lgsMaterialAllocationDetail.RequestLocationCode = lgsMaterialAllocationDetailTemp.RequestLocationCode;
                lgsMaterialAllocationDetail.RequestNo = lgsMaterialAllocationDetailTemp.RequestNo == null ? "" : lgsMaterialAllocationDetailTemp.RequestNo;
                lgsMaterialAllocationDetail.Qty = lgsMaterialAllocationDetailTemp.Qty;
                lgsMaterialAllocationDetail.OrderQty = lgsMaterialAllocationDetailTemp.OrderQty;
                lgsMaterialAllocationDetail.BalanceQty = lgsMaterialAllocationDetailTemp.Qty;
                lgsMaterialAllocationDetail.CostPrice = lgsMaterialAllocationDetailTemp.CostPrice;
                lgsMaterialAllocationDetail.GrossAmount = lgsMaterialAllocationDetailTemp.GrossAmount;
                

                lgsMaterialAllocationHeader.LgsMaterialAllocationDetails.Add(lgsMaterialAllocationDetail);
            }

            return lgsMaterialAllocationHeader;
        }

        /// <summary>
        /// /// Create LgsMaterialAllocationDetailTemp using lgsMaintenanceJobAssignProductDetails 
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignHeader"></param>
        /// <returns></returns>
        public List<LgsMaterialAllocationDetailTemp> GetLgsMaintenanceJobAssignProductDetails(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        {
            // LgsMaterialAllocationDetail list for LgsMaterialAllocationtHeader  .
            List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp = new List<LgsMaterialAllocationDetailTemp>();
            List<LgsMaintenanceJobAssignProductDetail> lgsMaintenanceJobAssignProductDetails = new List<LgsMaintenanceJobAssignProductDetail>();
            lgsMaintenanceJobAssignProductDetails = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails.ToList();
            foreach (LgsMaintenanceJobAssignProductDetail lgsMaintenanceJobAssignProductDetail in lgsMaintenanceJobAssignProductDetails)
            {
                LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

                lgsMaterialAllocationDetailTemp.LineNo = lgsMaintenanceJobAssignProductDetail.LineNo;
                lgsMaterialAllocationDetailTemp.ProductID = lgsMaintenanceJobAssignProductDetail.ProductID;
                lgsMaterialAllocationDetailTemp.ProductCode = lgsMaintenanceJobAssignProductDetail.ProductCode;
                lgsMaterialAllocationDetailTemp.UnitOfMeasureID = lgsMaintenanceJobAssignProductDetail.UnitOfMeasureID;
                lgsMaterialAllocationDetailTemp.UnitOfMeasure = lgsMaintenanceJobAssignProductDetail.UnitOfMeasure;
                lgsMaterialAllocationDetailTemp.ConvertFactor = lgsMaintenanceJobAssignProductDetail.ConvertFactor;
                lgsMaterialAllocationDetailTemp.OrderQty = lgsMaintenanceJobAssignProductDetail.OrderQty;
                //lgsMaterialAllocationDetailTemp.Qty = lgsMaintenanceJobAssignProductDetail.Qty;
                lgsMaterialAllocationDetailTemp.RequestLocationID = Common.LoggedLocationID;
                lgsMaterialAllocationDetailTemp.RequestLocationCode = Common.LoggedLocationCode;

                lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            }

            return lgsMaterialAllocationDetailsTemp;
        }

        /// <summary>
        /// /// Create LgsMaterialAllocationDetailTemp using lgsMaterialRequestDetailList 
        /// </summary>
        /// <param name="lgsMaterialRequestHeader"></param>
        /// <returns></returns>
        public List<LgsMaterialAllocationDetailTemp> GetLgsMateriaRequsitionProductDetails(LgsMaterialRequestHeader lgsMaterialRequestHeader)
        {
            // LgsMaterialAllocationDetail list for LgsMaterialAllocationtHeader  .
            List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailsTemp = new List<LgsMaterialAllocationDetailTemp>();

            var tempProductList = (from d in context.LgsMaterialRequestDetails
                                   join h in context.LgsMaterialRequestHeaders on d.LgsMaterialRequestHeaderID equals h.LgsMaterialRequestHeaderID
                                   join p in context.LgsProductMasters on d.ProductID equals p.LgsProductMasterID
                                   join l in context.Locations on h.LocationID equals l.LocationID
                                   where d.DocumentStatus.Equals(1) && d.LgsMaterialRequestHeaderID.Equals(lgsMaterialRequestHeader.LgsMaterialRequestHeaderID)
                                   && d.BalanceQty != 0
                                   select new
                                   {
                                       d.LgsMaterialRequestDetailID,
                                       d.LineNo,
                                       d.ProductID,
                                       p.ProductCode,
                                       p.ProductName,
                                       d.BaseUnitID,
                                       d.UnitOfMeasureID,
                                       d.UnitOfMeasure,
                                       d.ConvertFactor,
                                       d.OrderQty,
                                       d.BalanceQty,
                                       p.CostPrice,
                                       l.LocationCode,
                                       l.LocationID
                                   }).ToArray();


            foreach (var tempProduct in tempProductList)
            {
                LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

                lgsMaterialAllocationDetailTemp.LineNo = tempProduct.LineNo;
                lgsMaterialAllocationDetailTemp.ProductID = tempProduct.ProductID;
                lgsMaterialAllocationDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsMaterialAllocationDetailTemp.ProductName = tempProduct.ProductName;
                lgsMaterialAllocationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsMaterialAllocationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasure;
                lgsMaterialAllocationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsMaterialAllocationDetailTemp.Qty = tempProduct.BalanceQty;
                lgsMaterialAllocationDetailTemp.RequestLocationID = Common.LoggedLocationID;
                lgsMaterialAllocationDetailTemp.RequestLocationCode = Common.LoggedLocationCode;
                lgsMaterialAllocationDetailTemp.CostPrice = tempProduct.CostPrice;
                lgsMaterialAllocationDetailTemp.GrossAmount = Common.ConvertStringToDecimalCurrency((tempProduct.CostPrice * tempProduct.BalanceQty).ToString());
                lgsMaterialAllocationDetailTemp.RequestLocationCode = tempProduct.LocationCode;
                lgsMaterialAllocationDetailTemp.RequestLocationID = tempProduct.LocationID;

                lgsMaterialAllocationDetailsTemp.Add(lgsMaterialAllocationDetailTemp);
            }

            return lgsMaterialAllocationDetailsTemp;
        }
        

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsMaterialAllocationHeaders.Where(jh => jh.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }


        /// <summary>
        /// Get Material Allocation details for report
        /// </summary>
        /// <returns></returns>
        public DataSet GetMaterialAllocationTransactionData(string documentNo, int documentStatus, int documentId)
        {
            AutoGenerateInfo autoGenerateInfoMRN = new AutoGenerateInfo();
            autoGenerateInfoMRN = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmMaterialRequestNote");

            var query = (
                        from ph in context.LgsMaterialAllocationHeaders
                        join pd in context.LgsMaterialAllocationDetails on ph.LgsMaterialAllocationHeaderID equals pd.LgsMaterialAllocationHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join lh in context.Locations on ph.LocationID equals lh.LocationID
                        join ld in context.Locations on pd.LocationID equals ld.LocationID
                        join mrn in context.LgsMaterialRequestHeaders on ph.ReferenceDocumentID equals mrn.LgsMaterialRequestHeaderID
                        join lmrn in context.Locations on mrn.LocationID equals lmrn.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId) 
                              && ph.ReferenceDocumentDocumentID.Equals(mrn.DocumentID) && ph.ReferenceDocumentDocumentID.Equals(autoGenerateInfoMRN.DocumentID)    
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = ph.DocumentDate,
                                FieldString3 = ph.ReferenceDocumentNo,
                                FieldString4 = ph.Remark,
                                FieldString5 = mrn.DocumentNo,
                                FieldString6 = mrn.CreatedUser,
                                FieldString7 = mrn.DocumentDate,
                                FieldString8 = "",
                                FieldString9 = lmrn.LocationCode + "  " + lmrn.LocationName,
                                FieldString10 = "",
                                FieldString11 = "",
                                FieldString12 = lh.LocationCode + "  " + lh.LocationName,
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = "",
                                FieldString17 = pd.Qty,
                                FieldString18 = ld.LocationCode,
                                FieldString19 = "",
                                FieldString20 = pd.RequestNo,
                                FieldString21 = pd.CostPrice,
                                FieldString22 = pd.GrossAmount,
                                FieldString23 = "",
                                FieldString24 = "",
                                FieldString25 = "",
                                FieldString26 = "",
                                FieldString27 = "",
                                FieldString28 = "",
                                FieldString29 = "",
                                FieldString30 = ph.CreatedUser
                            }).ToArray().ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(query);

            var materialRequestHeaderID = context.LgsMaterialAllocationHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus) 
                    && h.DocumentID.Equals(documentId)).FirstOrDefault().ReferenceDocumentID;

            if (context.LgsMaterialRequestDetails.Any(d => d.BalanceQty > 0 && d.LgsMaterialRequestHeaderID.Equals(materialRequestHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            {
                var querymrn = context.LgsMaterialRequestHeaders.Where("LgsMaterialRequestHeaderID==@0 AND DocumentStatus==@1", materialRequestHeaderID, documentStatus);

                var queryResult = querymrn.AsEnumerable();
                var mrnBalance = (from ph in queryResult
                            join pd in context.LgsMaterialRequestDetails on ph.LgsMaterialRequestHeaderID equals pd.LgsMaterialRequestHeaderID
                            join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                            join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                            join l in context.Locations on ph.LocationID equals l.LocationID
                            //where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                            select
                                new
                                {
                                    FieldString1 = ph.DocumentNo,
                                    FieldString2 = ph.DocumentDate.ToShortDateString(),
                                    FieldString3 = ph.DiliveryDate.ToShortDateString(),
                                    FieldString4 = ph.Remark,
                                    FieldString5 = ph.ReferenceDocumentNo,
                                    FieldString6 = "",
                                    FieldString7 = "",
                                    FieldString8 = "",
                                    FieldString9 = "",
                                    FieldString10 = "",
                                    FieldString11 = "",
                                    FieldString12 = l.LocationCode + "  " + l.LocationName,
                                    FieldString13 = pm.ProductCode,
                                    FieldString14 = pm.ProductName,
                                    FieldString15 = um.UnitOfMeasureName,
                                    FieldString16 = "",
                                    FieldString17 = "",
                                    FieldString18 = "",
                                    FieldString19 = "",
                                    FieldString20 = "",
                                    FieldString21 = pd.BalanceQty,
                                    FieldString22 = pd.OrderQty,
                                    FieldString23 = "",
                                    FieldString24 = "",
                                    FieldString25 = "",
                                    FieldString26 = "",
                                    FieldString27 = "",
                                    FieldString28 = "",
                                    FieldString29 = "",
                                    FieldString30 = ph.CreatedUser
                                }).ToArray().ToDataTable();

                 dsTransactionData.Tables.Add(mrnBalance);
            }

            
            //return query.AsEnumerable().ToDataTable();
            return dsTransactionData;
        }


        public LgsMaterialAllocationHeader GetPausedMaterialAllocationHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaterialAllocationHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public LgsMaterialAllocationHeader GetPausedMaterialAllocationHeaderByDocumentNo(string documentNo)
        {
            return context.LgsMaterialAllocationHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault(); 
        }

        /// <summary>
        /// Get pending Material Allocation Notes
        /// </summary>
        /// <returns></returns>
        public string[] GetPendingMaterialAllocationList()
        {            
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.LgsMaterialAllocationHeaders
                       join pd in context.LgsMaterialAllocationDetails on ph.LgsMaterialAllocationHeaderID equals pd.LgsMaterialAllocationHeaderID
                       where pd.BalanceQty != 0 && ph.DocumentStatus == 1
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

        public LgsMaterialAllocationHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsMaterialAllocationHeaders.Where(ph => ph.DocumentNo == documentNo.Trim() && ph.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.LgsMaterialAllocationHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

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

        public DataTable GetMaterialAllocationDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {

            var query = context.LgsMaterialAllocationHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
            }

            var queryResult = query.AsEnumerable();

            return (from h in queryResult
                    join l in context.Locations on h.LocationID equals l.LocationID
                    select
                        new
                        {
                            FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                            FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.CreatedUser : "",
                            FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.DocumentNo : "",
                            FieldString4 = reportDataStructList[3].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                            FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.ReferenceDocumentNo : "",
                            FieldDecimal1 = reportDataStructList[5].IsSelectionField == true ? (from t in context.LgsMaterialAllocationDetails
                                                                                                where
                                                                                                  t.LgsMaterialAllocationHeaderID == h.LgsMaterialAllocationHeaderID
                                                                                                select new
                                                                                                {
                                                                                                    Column1 = ((System.Decimal?)t.Qty ?? (System.Decimal?)0)
                                                                                                }).Sum(p => p.Column1) : 0, // Qty                            
                        }).ToArray().ToDataTable();
        }


        public LgsMaterialAllocationDetailTemp GetLgsMaterialAllocationDetailTemp(List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailTempPrm, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();
            decimal defaultConvertFactor = 1;
            var purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
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
                                               pm.PackSize,
                                               pm.SellingPrice,
                                               pm.AverageCost,
                                               ConvertFactor = defaultConvertFactor,
                                               pm.IsBatch
                                           }).ToArray();

            if (!unitOfMeasureID.Equals(0) && !unitOfMeasureID.Equals(lgsProductMaster.UnitOfMeasureID))
            {
                purchaseOrderDetailTemp = (dynamic)null;

                purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
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
                                               pm.PackSize,
                                               puc.SellingPrice,
                                               pm.AverageCost,
                                               puc.ConvertFactor,
                                               pm.IsBatch
                                           }).ToArray();
            }

            foreach (var tempProduct in purchaseOrderDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    lgsMaterialAllocationDetailTemp = lgsMaterialAllocationDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsMaterialAllocationDetailTemp = lgsMaterialAllocationDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    lgsMaterialAllocationDetailTemp = lgsMaterialAllocationDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsMaterialAllocationDetailTemp == null)
                {
                    lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();
                    lgsMaterialAllocationDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsMaterialAllocationDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsMaterialAllocationDetailTemp.ProductName = tempProduct.ProductName;
                    lgsMaterialAllocationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsMaterialAllocationDetailTemp.CostPrice = tempProduct.CostPrice;
                    lgsMaterialAllocationDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    lgsMaterialAllocationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsMaterialAllocationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                }

            }
            return lgsMaterialAllocationDetailTemp;
        }


        public List<LgsMaterialAllocationDetailTemp> GetUpdateLgsMaterialAllocationDetailTemp(List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailTempList, LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTempPrm, LgsProductMaster lgsProductMaster)
        {
            LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

            lgsMaterialAllocationDetailTemp = lgsMaterialAllocationDetailTempList.Where(p => p.ProductID == lgsMaterialAllocationDetailTempPrm.ProductID && p.UnitOfMeasureID == lgsMaterialAllocationDetailTempPrm.UnitOfMeasureID).FirstOrDefault();

            if (lgsMaterialAllocationDetailTemp == null || lgsMaterialAllocationDetailTemp.LineNo.Equals(0))
            {
                if (lgsMaterialAllocationDetailTempList.Count.Equals(0))
                    lgsMaterialAllocationDetailTempPrm.LineNo = 1;
                else
                    lgsMaterialAllocationDetailTempPrm.LineNo = lgsMaterialAllocationDetailTempList.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsMaterialAllocationDetailTempList.Remove(lgsMaterialAllocationDetailTemp);
                lgsMaterialAllocationDetailTempPrm.LineNo = lgsMaterialAllocationDetailTemp.LineNo;
            }

            lgsMaterialAllocationDetailTempList.Add(lgsMaterialAllocationDetailTempPrm);

            return lgsMaterialAllocationDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<LgsMaterialAllocationDetailTemp> GetDeleteLgsMaterialAllocationDetailTemp(List<LgsMaterialAllocationDetailTemp> lgsMaterialAllocationDetailTempList, LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTempPrm)
        {
            LgsMaterialAllocationDetailTemp lgsMaterialAllocationDetailTemp = new LgsMaterialAllocationDetailTemp();

            lgsMaterialAllocationDetailTemp = lgsMaterialAllocationDetailTempList.Where(p => p.ProductID == lgsMaterialAllocationDetailTempPrm.ProductID && p.UnitOfMeasureID == lgsMaterialAllocationDetailTempPrm.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (lgsMaterialAllocationDetailTemp != null)
            {
                lgsMaterialAllocationDetailTempList.Remove(lgsMaterialAllocationDetailTemp);
                removedLineNo = lgsMaterialAllocationDetailTemp.LineNo;

            }
            lgsMaterialAllocationDetailTempList.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);


            return lgsMaterialAllocationDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public string[] GetAllProductCodesAccordingToMAN(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        { 
            List<String> productCodes = new List<string>();

            var qry = (from ah in context.LgsMaterialAllocationHeaders
                       join ad in context.LgsMaterialAllocationDetails on ah.LgsMaterialAllocationHeaderID equals ad.LgsMaterialAllocationHeaderID
                       join pm in context.LgsProductMasters on ad.ProductID equals pm.LgsProductMasterID
                       where ah.LgsMaterialAllocationHeaderID == lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID
                       select new
                       {
                           pm.ProductCode
                       }).ToArray();
            foreach (var temp in qry)
            {
                productCodes.Add(temp.ProductCode);
            }
            return productCodes.ToArray();
        }

        public string[] GetAllProductNamesAccordingToMAN(LgsMaterialAllocationHeader lgsMaterialAllocationHeader)
        { 
            List<String> productNames = new List<string>();

            var qry = (from ah in context.LgsMaterialAllocationHeaders
                       join ad in context.LgsMaterialAllocationDetails on ah.LgsMaterialAllocationHeaderID equals ad.LgsMaterialAllocationHeaderID
                       join pm in context.LgsProductMasters on ad.ProductID equals pm.LgsProductMasterID
                       where ah.LgsMaterialAllocationHeaderID == lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID
                       select new
                       {
                           pm.ProductName
                       }).ToArray();
            foreach (var temp in qry)
            {
                productNames.Add(temp.ProductName);
            }
            return productNames.ToArray();
        }


        public bool CheckIsProductExisInMAN(LgsMaterialAllocationHeader lgsMaterialAllocationHeader, long productID)
        {
            bool isExists = false;
            var qry = (from ah in context.LgsMaterialAllocationHeaders
                       join ad in context.LgsMaterialAllocationDetails on ah.LgsMaterialAllocationHeaderID equals ad.LgsMaterialAllocationHeaderID
                       where ah.LgsMaterialAllocationHeaderID == lgsMaterialAllocationHeader.LgsMaterialAllocationHeaderID
                       select new
                       {
                           ad.ProductID
                       }).ToArray();
            foreach (var temp in qry)
            {
                if (temp.ProductID == productID) 
                { 
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }

        public bool IsValidNoOfQty(decimal qty, long productID, int unitOfMeasureID, long allocationHeaderID)
        {
            decimal poQty = 0;
            var getCurrentQty = (from a in context.LgsMaterialAllocationDetails
                                 where a.ProductID.Equals(productID) && a.LgsMaterialAllocationHeaderID.Equals(allocationHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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

        public DataTable GetPendingMaterialAllocationDocuments() 
        {
            LocationService locationService = new LocationService();
            var sql = (from mah in context.LgsMaterialAllocationHeaders
                       join mad in context.LgsMaterialAllocationDetails on mah.LgsMaterialAllocationHeaderID equals mad.LgsMaterialAllocationHeaderID
                       //join l in context.Locations on mah.LocationID equals l.LocationID
                       join pm in context.LgsProductMasters on mad.ProductID equals pm.LgsProductMasterID
                       where mad.BalanceQty != 0 && mah.DocumentStatus == 1

                       select new
                       {
                           mah.DocumentNo,
                           mah.DocumentDate,
                           mad.RequestLocationID,
                           pm.ProductName,
                           mad.Qty,
                           mad.BalanceQty
                       }).ToArray();

            var queryResult = (from qs in sql
                               let rqstLocation = (
                                       from md in context.LgsMaterialAllocationDetails
                                       join l in context.Locations on md.RequestLocationID equals l.LocationID
                                       where qs.RequestLocationID == l.LocationID
                                       select l.LocationName).FirstOrDefault()

                               select new
                               {
                                   qs.DocumentNo,
                                   qs.DocumentDate,
                                   Requestedlocation = rqstLocation,
                                   qs.ProductName,
                                   RequestedQty = qs.Qty,
                                   IssuedQty = qs.Qty - qs.BalanceQty,
                                   qs.BalanceQty
                               }).ToArray();

            return queryResult.ToDataTable();

        }


        #endregion
    }
}
