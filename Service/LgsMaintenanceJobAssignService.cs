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
    public class LgsMaintenanceJobAssignService
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
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignProductDetailTempPrm"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignProductDetailTemp> GetLgsMaintenanceJobAssignProductDetailTempList(List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailTempList, LgsMaintenanceJobAssignProductDetailTemp lgsMaintenanceJobAssignProductDetailTempPrm)
        {
            //LgsMaintenanceJobAssignProductDetailTemp lgsMaintenanceJobAssignProductDetailTemp = new LgsMaintenanceJobAssignProductDetailTemp();

            //if (lgsMaintenanceJobAssignProductDetailTemp == null || lgsMaintenanceJobAssignProductDetailTemp.LineNo.Equals(0))
            //{
            //    if (lgsMaintenanceJobAssignProductDetailTempList.Count.Equals(0))
            //        lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = 1;
            //    else
            //        lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = lgsMaintenanceJobAssignProductDetailTempList.Max(s => s.LineNo) + 1;
            //}
            //else
            //{
            //    lgsMaintenanceJobAssignProductDetailTempList.Remove(lgsMaintenanceJobAssignProductDetailTemp);
            //    lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = lgsMaintenanceJobAssignProductDetailTemp.LineNo;
            //}

            //lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);

            //if (lgsMaintenanceJobAssignProductDetailTempList != null)
            //{
            //    if (lgsMaintenanceJobAssignProductDetailTempList.Count == 0)
            //    {
            //        lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = 1;
            //    }
            //    else
            //    {
            //        lgsMaintenanceJobAssignProductDetailTemp = lgsMaintenanceJobAssignProductDetailTempList.Where(pr => pr.ProductCode == lgsMaintenanceJobAssignProductDetailTempPrm.ProductCode && pr.UnitOfMeasure == lgsMaintenanceJobAssignProductDetailTempPrm.UnitOfMeasure).FirstOrDefault();

            //        if (lgsMaintenanceJobAssignProductDetailTemp != null)
            //        {
            //            lgsMaintenanceJobAssignProductDetailTempList.Remove(lgsMaintenanceJobAssignProductDetailTemp);
            //            lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);
            //        }
            //        else
            //        {
            //            lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = lgsMaintenanceJobAssignProductDetailTempList.Max(ln => ln.LineNo) + 1;
            //        }

            //    }
            //    lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);
            //}
            //else
            //{
            //    lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = 1;
            //    lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);
            //}

            if (lgsMaintenanceJobAssignProductDetailTempList.Where(pr => pr.ProductCode == lgsMaintenanceJobAssignProductDetailTempPrm.ProductCode && pr.UnitOfMeasure == lgsMaintenanceJobAssignProductDetailTempPrm.UnitOfMeasure).FirstOrDefault() != null)
            {
                // Update Existing Record
                lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = lgsMaintenanceJobAssignProductDetailTempList.Where(pr => pr.ProductCode == lgsMaintenanceJobAssignProductDetailTempPrm.ProductCode).FirstOrDefault().LineNo;
                lgsMaintenanceJobAssignProductDetailTempList.RemoveAll(pr => pr.ProductCode == lgsMaintenanceJobAssignProductDetailTempPrm.ProductCode);
                lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsMaintenanceJobAssignProductDetailTempList.Count, 0))
                { lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = 1; }
                else
                { lgsMaintenanceJobAssignProductDetailTempPrm.LineNo = lgsMaintenanceJobAssignProductDetailTempList.Max(ln => ln.LineNo) + 1; }
                lgsMaintenanceJobAssignProductDetailTempList.Add(lgsMaintenanceJobAssignProductDetailTempPrm);
            }

            return lgsMaintenanceJobAssignProductDetailTempList;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignProductDetailTemp> DeleteProductLgsMaintenanceJobAssignProductDetailTemp(List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailsTemp, string productCode)
        {
            // List for grid view.
            lgsMaintenanceJobAssignProductDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return lgsMaintenanceJobAssignProductDetailsTemp;
        }

        /// <summary>
        /// Get LgsMaintenanceJobAssignHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobAssignHeader GetLgsMaintenanceJobAssignHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaintenanceJobAssignHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Save Maintenance Job Assign note
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignHeader"></param>
        /// <param name="lgsMaintenanceJobAssignProductDetailsTemp"></param>
        /// <returns></returns>
        public bool SaveMaintenanceJobAssignNote(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader, List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailsTemp, List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp)
        { 
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID.Equals(0))
                {
                    context.LgsMaintenanceJobAssignHeaders.Add(AddLgsMaintenanceJobAssignDetails(lgsMaintenanceJobAssignHeader, lgsMaintenanceJobAssignProductDetailsTemp, lgsMaintenanceJobAssignEmployeeDetailsTemp));
                    context.SaveChanges();
                    lgsMaintenanceJobAssignHeader.MaintenanceJobAssignHeaderID = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID;
                    context.Entry(lgsMaintenanceJobAssignHeader).State = EntityState.Modified;

                    foreach (var lgsMaintenanceJobAssignProductDetail in lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails)
                    {
                        lgsMaintenanceJobAssignProductDetail.MaintenanceJobAssignProductDetailID = lgsMaintenanceJobAssignProductDetail.LgsMaintenanceJobAssignProductDetailID;
                    }
                    foreach (var lgsMaintenanceJobAssignEmployeeDetail in lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails)
                    {
                        lgsMaintenanceJobAssignEmployeeDetail.MaintenanceJobAssignEmployeeDetailID = lgsMaintenanceJobAssignEmployeeDetail.LgsMaintenanceJobAssignEmployeeDetailID;
                    }
                    this.context.Entry(lgsMaintenanceJobAssignHeader).State = EntityState.Modified;
                    this.context.SaveChanges();

                    context.LgsMaintenanceJobRequisitionHeaders.Update(d => d.LgsMaintenanceJobRequisitionHeaderID.Equals(lgsMaintenanceJobAssignHeader.LgsMaintenanceJobRequisitionHeaderID), d => new LgsMaintenanceJobRequisitionHeader { IsAssigned = true });
                }
                else
                {
                    // Delete existing records
                    var deleteProductList = context.LgsMaintenanceJobAssignProductDetails.Where(d => d.LgsMaintenanceJobAssignHeaderID == lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID);
                    foreach (var lgsMaintenanceJobAssignProductDetail in deleteProductList)
                    {
                        context.LgsMaintenanceJobAssignProductDetails.Remove(lgsMaintenanceJobAssignProductDetail);
                    }

                    var deleteEmployeeList = context.LgsMaintenanceJobAssignEmployeeDetails.Where(d => d.LgsMaintenanceJobAssignHeaderID == lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID);
                    foreach (var lgsMaintenanceJobAssignEmployeeDetail in deleteEmployeeList)
                    {
                        context.LgsMaintenanceJobAssignEmployeeDetails.Remove(lgsMaintenanceJobAssignEmployeeDetail);
                    }

                    lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails.Clear();
                    lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails.Clear();
                    lgsMaintenanceJobAssignHeader = AddLgsMaintenanceJobAssignDetails(lgsMaintenanceJobAssignHeader, lgsMaintenanceJobAssignProductDetailsTemp, lgsMaintenanceJobAssignEmployeeDetailsTemp);                    
                    this.context.Entry(lgsMaintenanceJobAssignHeader).State = EntityState.Modified;
                    this.context.SaveChanges();

                    foreach (var lgsMaintenanceJobAssignProductDetail in lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails)
                    {
                        lgsMaintenanceJobAssignProductDetail.MaintenanceJobAssignProductDetailID = lgsMaintenanceJobAssignProductDetail.LgsMaintenanceJobAssignProductDetailID;
                    }
                    foreach (var lgsMaintenanceJobAssignEmployeeDetail in lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails)
                    {
                        lgsMaintenanceJobAssignEmployeeDetail.MaintenanceJobAssignEmployeeDetailID = lgsMaintenanceJobAssignEmployeeDetail.LgsMaintenanceJobAssignEmployeeDetailID;
                    }
                    this.context.Entry(lgsMaintenanceJobAssignHeader).State = EntityState.Modified;
                    this.context.SaveChanges();

                    context.LgsMaintenanceJobRequisitionHeaders.Update(d => d.LgsMaintenanceJobRequisitionHeaderID.Equals(lgsMaintenanceJobAssignHeader.LgsMaintenanceJobRequisitionHeaderID), d => new LgsMaintenanceJobRequisitionHeader { IsAssigned = true });
                }
                
                
                transactionScope.Complete();
                return true;
            }
        }

        ///// <summary>
        ///// Update ReferenceDocumentDocumentID, ReferenceDocumentID fields in LgsMaintenanceJobRequisitionHeader 
        ///// </summary>
        ///// <param name="lgsMaintenanceJobAssignHeader"></param>
        //public void UpdateLgsMaintenanceJobRequisitionHeader(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        //{

        //    LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader = new LgsMaintenanceJobRequisitionHeader();
        //    LgsMaintenanceJobRequisitionService lgsMaintenanceJobRequisitionService = new LgsMaintenanceJobRequisitionService();
            
        //    lgsMaintenanceJobRequisitionHeader = lgsMaintenanceJobRequisitionService.GetLgsMaintenanceJobRequisitionHeaderByDocumentID(lgsMaintenanceJobAssignHeader.LgsMaintenanceJobRequisitionHeaderID);
        //    lgsMaintenanceJobRequisitionHeader.ReferenceDocumentDocumentID = lgsMaintenanceJobAssignHeader.DocumentID;
        //    lgsMaintenanceJobRequisitionHeader.ReferenceDocumentID = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignHeaderID;
        //    this.context.Entry(lgsMaintenanceJobRequisitionHeader).State = EntityState.Modified;
        //    this.context.SaveChanges();
        //}

        /// <summary>
        /// Create LgsMaintenanceJobAssignProductDetail list using LgsMaintenanceJobAssignProductDetailTemp
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignHeader"></param>
        /// <param name="lgsMaintenanceJobAssignProductDetailsTemp"></param>
        /// <returns></returns>
        private LgsMaintenanceJobAssignHeader AddLgsMaintenanceJobAssignDetails(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader, List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailsTemp, List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp)
        {
                    
            // LgsMaintenanceJobAssignProductDetail list for LgsMaintenanceJobAssignHeader 
            lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails = new List<LgsMaintenanceJobAssignProductDetail>();
            foreach (LgsMaintenanceJobAssignProductDetailTemp lgsMaintenanceJobAssignProductDetailTemp in lgsMaintenanceJobAssignProductDetailsTemp)
            {
                LgsMaintenanceJobAssignProductDetail lgsMaintenanceJobAssignProductDetail = new LgsMaintenanceJobAssignProductDetail();

                lgsMaintenanceJobAssignProductDetail.DocumentStatus = lgsMaintenanceJobAssignHeader.DocumentStatus;
                lgsMaintenanceJobAssignProductDetail.DocumentNo = lgsMaintenanceJobAssignHeader.DocumentNo;
                lgsMaintenanceJobAssignProductDetail.DocumentDate = lgsMaintenanceJobAssignHeader.DocumentDate;
                lgsMaintenanceJobAssignProductDetail.CompanyID = lgsMaintenanceJobAssignHeader.CompanyID;
                lgsMaintenanceJobAssignProductDetail.LocationID = lgsMaintenanceJobAssignHeader.LocationID;
                lgsMaintenanceJobAssignProductDetail.CostCentreID = lgsMaintenanceJobAssignHeader.CostCentreID;
                lgsMaintenanceJobAssignProductDetail.CreatedDate = lgsMaintenanceJobAssignHeader.CreatedDate;
                lgsMaintenanceJobAssignProductDetail.DocumentID = lgsMaintenanceJobAssignHeader.DocumentID;
                lgsMaintenanceJobAssignProductDetail.DataTransfer = lgsMaintenanceJobAssignHeader.DataTransfer;
                lgsMaintenanceJobAssignProductDetail.ModifiedDate = lgsMaintenanceJobAssignHeader.ModifiedDate;

                lgsMaintenanceJobAssignProductDetail.LgsMaintenanceJobAssignProductDetailID = lgsMaintenanceJobAssignProductDetailTemp.LgsMaintenanceJobAssignProductDetailTempID;
                lgsMaintenanceJobAssignProductDetail.LineNo = lgsMaintenanceJobAssignProductDetailTemp.LineNo;
                lgsMaintenanceJobAssignProductDetail.ProductID = lgsMaintenanceJobAssignProductDetailTemp.ProductID;
                lgsMaintenanceJobAssignProductDetail.ProductCode = lgsMaintenanceJobAssignProductDetailTemp.ProductCode;
                lgsMaintenanceJobAssignProductDetail.UnitOfMeasureID = lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasureID;
                lgsMaintenanceJobAssignProductDetail.BaseUnitID = lgsMaintenanceJobAssignProductDetailTemp.BaseUnitID;
                lgsMaintenanceJobAssignProductDetail.UnitOfMeasure = lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasure;
                lgsMaintenanceJobAssignProductDetail.ConvertFactor = lgsMaintenanceJobAssignProductDetailTemp.ConvertFactor;
                lgsMaintenanceJobAssignProductDetail.OrderQty = lgsMaintenanceJobAssignProductDetailTemp.OrderQty;
                lgsMaintenanceJobAssignProductDetail.BalanceQty = lgsMaintenanceJobAssignProductDetailTemp.BalanceQty;

                lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails.Add(lgsMaintenanceJobAssignProductDetail);
            }

            // LgsMaintenanceJobAssignEmployeeDetail list for LgsMaintenanceJobAssignHeader 
            lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails = new List<LgsMaintenanceJobAssignEmployeeDetail>();
            foreach (LgsMaintenanceJobAssignEmployeeDetailTemp lgsMaintenanceJobAssignEmployeeDetailTemp in lgsMaintenanceJobAssignEmployeeDetailsTemp)
            {
                LgsMaintenanceJobAssignEmployeeDetail lgsMaintenanceJobAssignEmployeeDetail = new LgsMaintenanceJobAssignEmployeeDetail();

                lgsMaintenanceJobAssignEmployeeDetail.DocumentStatus = lgsMaintenanceJobAssignHeader.DocumentStatus;
                lgsMaintenanceJobAssignEmployeeDetail.DocumentNo = lgsMaintenanceJobAssignHeader.DocumentNo;
                lgsMaintenanceJobAssignEmployeeDetail.DocumentDate = lgsMaintenanceJobAssignHeader.DocumentDate;
                lgsMaintenanceJobAssignEmployeeDetail.CompanyID = lgsMaintenanceJobAssignHeader.CompanyID;
                lgsMaintenanceJobAssignEmployeeDetail.LocationID = lgsMaintenanceJobAssignHeader.LocationID;
                lgsMaintenanceJobAssignEmployeeDetail.CostCentreID = lgsMaintenanceJobAssignHeader.CostCentreID;
                lgsMaintenanceJobAssignEmployeeDetail.CreatedDate = lgsMaintenanceJobAssignHeader.CreatedDate;
                lgsMaintenanceJobAssignEmployeeDetail.DocumentID = lgsMaintenanceJobAssignHeader.DocumentID;
                lgsMaintenanceJobAssignEmployeeDetail.DataTransfer = lgsMaintenanceJobAssignHeader.DataTransfer;
                lgsMaintenanceJobAssignEmployeeDetail.ModifiedDate = lgsMaintenanceJobAssignHeader.ModifiedDate;

                lgsMaintenanceJobAssignEmployeeDetail.EmployeeID = lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeID;
                lgsMaintenanceJobAssignEmployeeDetail.EmployeeCode = lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode;
                
                lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails.Add(lgsMaintenanceJobAssignEmployeeDetail);
            }

            return lgsMaintenanceJobAssignHeader;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        public LgsMaintenanceJobAssignHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsMaintenanceJobAssignHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// /// Create LgsMaintenanceJobAssignProductDetail list using LgsMaintenanceJobAssignProductDetailTemp
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignHeader"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignProductDetailTemp> GetLgsMaintenanceJobAssignProductDetails(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        {

            // LgsMaintenanceJobAssignProductDetail list for LgsMaintenanceJobAssignHeader 
            List<LgsMaintenanceJobAssignProductDetailTemp> lgsMaintenanceJobAssignProductDetailsTemp = new List<LgsMaintenanceJobAssignProductDetailTemp>();
            List<LgsMaintenanceJobAssignProductDetail> lgsMaintenanceJobAssignProductDetails = new List<LgsMaintenanceJobAssignProductDetail>();
            lgsMaintenanceJobAssignProductDetails = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignProductDetails.ToList();
            foreach (LgsMaintenanceJobAssignProductDetail lgsMaintenanceJobAssignProductDetail in lgsMaintenanceJobAssignProductDetails)
            {
                LgsMaintenanceJobAssignProductDetailTemp lgsMaintenanceJobAssignProductDetailTemp = new LgsMaintenanceJobAssignProductDetailTemp();

                lgsMaintenanceJobAssignProductDetailTemp.LgsMaintenanceJobAssignProductDetailTempID = lgsMaintenanceJobAssignProductDetail.LgsMaintenanceJobAssignProductDetailID;
                lgsMaintenanceJobAssignProductDetailTemp.LineNo = lgsMaintenanceJobAssignProductDetail.LineNo;
                lgsMaintenanceJobAssignProductDetailTemp.ProductID = lgsMaintenanceJobAssignProductDetail.ProductID;
                lgsMaintenanceJobAssignProductDetailTemp.ProductCode = lgsMaintenanceJobAssignProductDetail.ProductCode;
                lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasureID = lgsMaintenanceJobAssignProductDetail.BaseUnitID;
                lgsMaintenanceJobAssignProductDetailTemp.UnitOfMeasure = lgsMaintenanceJobAssignProductDetail.UnitOfMeasure;
                lgsMaintenanceJobAssignProductDetailTemp.ConvertFactor = lgsMaintenanceJobAssignProductDetail.ConvertFactor;
                lgsMaintenanceJobAssignProductDetailTemp.OrderQty = lgsMaintenanceJobAssignProductDetail.OrderQty;
                //lgsMaintenanceJobAssignProductDetailTemp.Qty = lgsMaintenanceJobAssignProductDetail.Qty;
               
                lgsMaintenanceJobAssignProductDetailsTemp.Add(lgsMaintenanceJobAssignProductDetailTemp);
            }
            
            return lgsMaintenanceJobAssignProductDetailsTemp;
        }

        /// <summary>
        /// /// Create LgsMaintenanceJobAssignEmployeeDetail list using LgsMaintenanceJobAssignEmployeeDetailTemp
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignHeader"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignEmployeeDetailTemp> GetLgsMaintenanceJobAssignEmployeeDetails(LgsMaintenanceJobAssignHeader lgsMaintenanceJobAssignHeader)
        {

            // LgsMaintenanceJobAssignEmployeeDetail list for LgsMaintenanceJobAssignHeader
            List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp = new List<LgsMaintenanceJobAssignEmployeeDetailTemp>();
            List<LgsMaintenanceJobAssignEmployeeDetail> lgsMaintenanceJobAssignEmployeeDetails = new List<LgsMaintenanceJobAssignEmployeeDetail>();
            lgsMaintenanceJobAssignEmployeeDetails = lgsMaintenanceJobAssignHeader.LgsMaintenanceJobAssignEmployeeDetails.ToList();
            foreach (LgsMaintenanceJobAssignEmployeeDetail lgsMaintenanceJobAssignEmployeeDetail in lgsMaintenanceJobAssignEmployeeDetails)
            {
                LgsMaintenanceJobAssignEmployeeDetailTemp lgsMaintenanceJobAssignEmployeeDetailTemp = new LgsMaintenanceJobAssignEmployeeDetailTemp();

                lgsMaintenanceJobAssignEmployeeDetailTemp.LgsMaintenanceJobAssignEmployeeDetailTempID = lgsMaintenanceJobAssignEmployeeDetail.LgsMaintenanceJobAssignEmployeeDetailID;
                lgsMaintenanceJobAssignEmployeeDetailTemp.LineNo = lgsMaintenanceJobAssignEmployeeDetail.LineNo;
                lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeID = lgsMaintenanceJobAssignEmployeeDetail.EmployeeID;
                lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode = lgsMaintenanceJobAssignEmployeeDetail.EmployeeCode;
                
                lgsMaintenanceJobAssignEmployeeDetailsTemp.Add(lgsMaintenanceJobAssignEmployeeDetailTemp);
            }

            return lgsMaintenanceJobAssignEmployeeDetailsTemp;
        }

        /// <summary>
        /// Add or Update employees of grid view
        /// </summary>
        /// <param name="lgsMaintenanceJobAssignEmployeeDetailTemp"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignEmployeeDetailTemp> GetLgsMaintenanceJobAssignEmployeeDetailTempList(List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp, LgsMaintenanceJobAssignEmployeeDetailTemp lgsMaintenanceJobAssignEmployeeDetailTemp)
        {
           if (lgsMaintenanceJobAssignEmployeeDetailsTemp.Where(ep => ep.EmployeeCode== lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode).FirstOrDefault() != null)
            {
                // Update Existing Record
                lgsMaintenanceJobAssignEmployeeDetailTemp.LineNo = lgsMaintenanceJobAssignEmployeeDetailsTemp.Where(pr => pr.EmployeeCode == lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode).FirstOrDefault().LineNo;
                lgsMaintenanceJobAssignEmployeeDetailsTemp.RemoveAll(pr => pr.EmployeeCode == lgsMaintenanceJobAssignEmployeeDetailTemp.EmployeeCode);
                lgsMaintenanceJobAssignEmployeeDetailsTemp.Add(lgsMaintenanceJobAssignEmployeeDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsMaintenanceJobAssignEmployeeDetailsTemp.Count, 0))
                { lgsMaintenanceJobAssignEmployeeDetailTemp.LineNo = 1; }
                else
                { lgsMaintenanceJobAssignEmployeeDetailTemp.LineNo = lgsMaintenanceJobAssignEmployeeDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsMaintenanceJobAssignEmployeeDetailsTemp.Add(lgsMaintenanceJobAssignEmployeeDetailTemp);
            }
           return lgsMaintenanceJobAssignEmployeeDetailsTemp;
        }

        /// <summary>
        /// Delete a employee from grid view.
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public List<LgsMaintenanceJobAssignEmployeeDetailTemp> DeleteEmployeeLgsMaintenanceJobAssignEmployeeDetailTemp(List<LgsMaintenanceJobAssignEmployeeDetailTemp> lgsMaintenanceJobAssignEmployeeDetailsTemp, string employeeCode)
        {
            // List for grid view.
            lgsMaintenanceJobAssignEmployeeDetailsTemp.RemoveAll(ep => ep.EmployeeCode == employeeCode);
            return lgsMaintenanceJobAssignEmployeeDetailsTemp;
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsMaintenanceJobAssignHeaders.Where(jh => jh.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }
        

        /// <summary>
        /// Get Saved Document Details for Material Alllcation Note
        /// </summary>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        public LgsMaintenanceJobAssignHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsMaintenanceJobAssignHeaders.Where(jh => jh.DocumentNo == documentNo.Trim() && jh.DocumentStatus.Equals(1) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// Get LgsMaintenanceJobAssignHeader for Material Allocation Note
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobAssignHeader GetLgsMaintenanceJobAssignHeaderByDocumentID(long documentID)
        {
            return context.LgsMaintenanceJobAssignHeaders.Where(jh => jh.LgsMaintenanceJobAssignHeaderID == documentID && jh.DocumentStatus.Equals(1) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault();
        }


        /// <summary>
        /// Get pending Assigned jobs list 
        /// </summary>
        /// <returns></returns>
        public string[] GetPendingAssignedobList(int locationID)
        {
            //List<string> pendingAssignedJobList = context.LgsMaintenanceJobAssignHeaders.Where(j => j.DocumentStatus.Equals(1) && j.ReferenceDocumentDocumentID.Equals(0)).Select(j => j.DocumentNo).ToList();
            //return pendingAssignedJobList.ToArray();

            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.LgsMaintenanceJobAssignHeaders
                       join pd in context.LgsMaintenanceJobAssignProductDetails on ph.LgsMaintenanceJobAssignHeaderID equals pd.LgsMaintenanceJobAssignHeaderID
                       where ph.LocationID == locationID && pd.BalanceQty != 0
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobAssignHeader GetPausedLgsMaintenanceJobAssignHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaintenanceJobAssignHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.LgsMaintenanceJobAssignHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

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

        public DataTable GetMaintenanceJobAssignDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {

            var query = context.LgsMaintenanceJobAssignHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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

            return (from h in query
                    select
                        new
                        {
                            FieldString1 = h.DocumentNo,
                            FieldString2 = DbFunctions.TruncateTime(h.DocumentDate),
                            FieldString3 = DbFunctions.TruncateTime(h.DiliveryDate),
                            FieldString4 = h.ReferenceDocumentNo,
                            FieldString5 = h.Remark

                        }).ToArray().ToDataTable();
        }


        /// <summary>
        /// Get Maintenance job assign details for report
        /// </summary>
        /// <returns></returns>
        public DataSet GetMaintenanceJobAssignTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            DataSet reportData = new DataSet();
            DataTable dtHeader = (
                        from ph in context.LgsMaintenanceJobAssignHeaders
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = DbFunctions.TruncateTime(ph.DiliveryDate),
                                FieldString4 = ph.Remark,
                                FieldString5 = ph.ReferenceDocumentNo,
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = "",
                                FieldString9 = "",
                                FieldString10 = "",
                                FieldString11 = "",
                                FieldString12 = l.LocationCode + "  " + l.LocationName
                                
                            }).ToDataTable();
            reportData.Tables.Add(dtHeader);

            DataTable dtProductDetails = (
                       from ph in context.LgsMaintenanceJobAssignHeaders
                       join pd in context.LgsMaintenanceJobAssignProductDetails on ph.LgsMaintenanceJobAssignHeaderID equals pd.LgsMaintenanceJobAssignHeaderID
                       join pr in context.LgsProductMasters on pd.ProductID equals pr.LgsProductMasterID
                       join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                       where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                       select
                           new
                           {
                               FieldString13 = pr.ProductCode,
                               FieldString14 = pr.ProductName,
                               FieldString15 = um.UnitOfMeasureName,
                               FieldString16 = pd.PackSize,
                               FieldString17 = pd.OrderQty,
                               FieldString18 = "", // FreeQty
                               FieldString19 = "", // CostPrice
                               FieldString20 = "", // DiscountPercentage
                               FieldString21 = "", // DiscountAmount
                               FieldString22 = "" // NetAmount                               
                           }).ToDataTable();
            reportData.Tables.Add(dtProductDetails);

            //DataTable dtEmployeeDetails = (
            //           from ph in context.LgsMaintenanceJobAssignHeaders
            //           join pe in context.LgsMaintenanceJobAssignEmployeeDetails on ph.LgsMaintenanceJobAssignHeaderID equals pe.LgsMaintenanceJobAssignHeaderID
            //           where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
            //           select
            //               new
            //               {
            //                   FieldString13 = pr.ProductCode,
            //                   FieldString14 = pr.ProductName,
            //                   FieldString15 = um.UnitOfMeasureName,
            //                   FieldString16 = pe.PackSize,
            //                   FieldString17 = pe.OrderQty,
            //                   FieldString18 = "", // FreeQty
            //                   FieldString19 = "", // CostPrice
            //                   FieldString20 = "", // DiscountPercentage
            //                   FieldString21 = "", // DiscountAmount
            //                   FieldString22 = "" // NetAmount                               
            //               }).ToDataTable();
            //return query.AsEnumerable().ToDataTable();

            return reportData;
        }


        #endregion

        
    }
}
