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
using System.Data.Entity;
using MoreLinq;

namespace Service
{
    public class ServiceOutService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public ServiceOutDetailTemp getServiceOutDetailTemp(List<ServiceOutDetailTemp> ServiceOutDetailTemp, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            ServiceOutDetailTemp serviceOutDetailTemp = new ServiceOutDetailTemp();
            decimal defaultConvertFactor = 1;
            var serviceDetailTemp = (from pm in context.LgsProductMasters
                                      join psm in context.LgsProductBatchNoExpiaryDetails on pm.LgsProductMasterID equals psm.ProductID
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
                serviceDetailTemp = (dynamic)null;

                serviceDetailTemp = (from pm in context.LgsProductMasters
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
                                          puc.ConvertFactor
                                      }).ToArray();
            }

            foreach (var tempProduct in serviceDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    serviceOutDetailTemp = ServiceOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    serviceOutDetailTemp = ServiceOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    serviceOutDetailTemp = ServiceOutDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (serviceOutDetailTemp == null)
                {
                    serviceOutDetailTemp = new ServiceOutDetailTemp();
                    serviceOutDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    serviceOutDetailTemp.ProductCode = tempProduct.ProductCode;
                    serviceOutDetailTemp.ProductName = tempProduct.ProductName;
                    serviceOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    serviceOutDetailTemp.CostPrice = tempProduct.CostPrice;
                    serviceOutDetailTemp.AverageCost = tempProduct.AverageCost;
                    serviceOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    serviceOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;

                }
            }

            return serviceOutDetailTemp;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.ServiceOutHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersToServiceIn(int locationID)
        {
            List<string> documentNoList = context.ServiceOutHeaders.Where(q => q.LocationID == locationID && q.DocumentStatus.Equals(1) && q.Balanced.Equals(0)).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<ServiceOutDetailTemp> getUpdateServiceOutDetailTemp(List<ServiceOutDetailTemp> serviceOutTempDetail, ServiceOutDetailTemp serviceOutDetail)
        {
            ServiceOutDetailTemp serviceOutDetailTemp = new ServiceOutDetailTemp();

            serviceOutDetailTemp = serviceOutTempDetail.Where(p => p.ProductID == serviceOutDetail.ProductID && p.UnitOfMeasureID == serviceOutDetail.UnitOfMeasureID).FirstOrDefault();

            if (serviceOutDetailTemp == null || serviceOutDetailTemp.LineNo.Equals(0))
            {
                serviceOutDetail.LineNo = serviceOutTempDetail.Count + 1;
            }
            else
            {
                serviceOutTempDetail.Remove(serviceOutDetailTemp);
                serviceOutDetail.LineNo = serviceOutDetailTemp.LineNo;

            }

            serviceOutTempDetail.Add(serviceOutDetail);

            return serviceOutTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.ServiceOutHeaders.Where(q => q.DocumentStatus.Equals(0)).Select(q => q.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="InvDepartmentCode"></param>
        /// <returns></returns>
        public ServiceOutHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.ServiceOutHeaders.Where(q => q.DocumentNo == pausedDocumentNo.Trim() && q.DocumentStatus.Equals(0)).FirstOrDefault();
        }


        public ServiceOutHeader GetPausedServiceOutHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.ServiceOutHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }


        ///// <summary>
        /////  Return InvPurchaseDetail list for grid view
        ///// </summary>
        ///// <returns></returns>
        //public List<NtrQuotationDetail> GetTempNtrQuotationDetails(NtrQuotationDetail ntrQuotationDetail)
        //{
        //    // List for grid view
        //    List<NtrQuotationDetail> tempNtrQuotationDetails = new List<NtrQuotationDetail>();
        //    NtrQuotationDetail tempNtrQuotationDetail = new NtrQuotationDetail();
        //    tempNtrQuotationDetail = tempNtrQuotationDetails.Where(q => q.ProductCode == ntrQuotationDetail.ProductCode).FirstOrDefault();
        //    if (tempNtrQuotationDetail != null)
        //    {
        //        tempNtrQuotationDetails.RemoveAll(q => q.ProductCode == ntrQuotationDetail.ProductCode);
        //        tempNtrQuotationDetails.Remove(tempNtrQuotationDetail);
        //        tempNtrQuotationDetails.Add(ntrQuotationDetail);
        //    }
        //    else
        //    {
        //        tempNtrQuotationDetails.Add(ntrQuotationDetail);
        //    }

        //    return tempNtrQuotationDetails;
        //}

        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="serviceOutDetailTemp"></param>
        /// <returns></returns>
        public List<ServiceOutDetailTemp> GetServiceOutDetailTempList(List<ServiceOutDetailTemp> serviceOutDetailTempList, ServiceOutDetailTemp serviceOutDetailTemp)
        {
            if (serviceOutDetailTempList.Where(pr => pr.ProductCode == serviceOutDetailTemp.ProductCode && pr.UnitOfMeasure == serviceOutDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                serviceOutDetailTemp.LineNo = serviceOutDetailTempList.Where(pr => pr.ProductCode == serviceOutDetailTemp.ProductCode).FirstOrDefault().LineNo;
                serviceOutDetailTempList.RemoveAll(pr => pr.ProductCode == serviceOutDetailTemp.ProductCode);
                serviceOutDetailTempList.Add(serviceOutDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(serviceOutDetailTempList.Count, 0))
                { serviceOutDetailTemp.LineNo = 1; }
                else
                { serviceOutDetailTemp.LineNo = serviceOutDetailTempList.Max(ln => ln.LineNo) + 1; }
                serviceOutDetailTempList.Add(serviceOutDetailTemp);
            }
            return serviceOutDetailTempList;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<ServiceOutDetailTemp> DeleteProductServiceOutDetailTemp(List<ServiceOutDetailTemp> ServiceOutDetailTemp, string productCode)
        {
            ServiceOutDetailTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return ServiceOutDetailTemp;
        }

        public List<ServiceOutDetail> GetAllLgsServiceOutDetail()
        {
            //return context.InvPurchaseOrderDetails.Where(u => u. == false).ToList();
            return context.ServiceOutDetails.ToList();
        }


        public bool Save(ServiceOutHeader serviceOutHeader, List<ServiceOutDetailTemp> serviceOutDetailTemp, List<InvProductSerialNoTemp> lgsProductSerialNoTemp)
        {

            if (serviceOutHeader.ServiceOutHeaderID.Equals(0))
                context.ServiceOutHeaders.Add(serviceOutHeader);
            else
                context.Entry(serviceOutHeader).State = EntityState.Modified;

            context.SaveChanges();

            List<LgsProductSerialNoDetail> lgsProductSerialNoDetailDelete = new List<LgsProductSerialNoDetail>();
            lgsProductSerialNoDetailDelete = context.LgsProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(serviceOutHeader.ServiceOutHeaderID) && p.LocationID.Equals(serviceOutHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(serviceOutHeader.DocumentID)).ToList();

            foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailDeleteTemp in lgsProductSerialNoDetailDelete)
            {
                context.LgsProductSerialNoDetails.Remove(lgsProductSerialNoDetailDeleteTemp);
            }
            context.SaveChanges();

            List<ServiceOutDetail> serviceOutDetailDelete = new List<ServiceOutDetail>();
            serviceOutDetailDelete = context.ServiceOutDetails.Where(p => p.ServiceOutHeaderID == serviceOutHeader.ServiceOutHeaderID).ToList();

            foreach (ServiceOutDetail serviceOutDetailDeleteTemp in serviceOutDetailDelete)
            {
                context.ServiceOutDetails.Remove(serviceOutDetailDeleteTemp);
            }
            context.SaveChanges();


            foreach (ServiceOutDetailTemp serviceOutDetail in serviceOutDetailTemp)
            {
                ServiceOutDetail serviceOutDetailSave = new ServiceOutDetail();

                serviceOutDetailSave.ServiceOutHeaderID = serviceOutHeader.ServiceOutHeaderID;
                serviceOutDetailSave.CompanyID = serviceOutHeader.CompanyID;
                serviceOutDetailSave.LocationID = serviceOutHeader.LocationID;
                serviceOutDetailSave.CostCentreID = serviceOutHeader.CostCentreID;
                serviceOutDetailSave.ProductID = serviceOutDetail.ProductID;
                serviceOutDetailSave.DocumentID = serviceOutHeader.DocumentID;
                serviceOutDetailSave.AverageCost = serviceOutDetail.AverageCost;
                serviceOutDetailSave.ConvertFactor = serviceOutDetail.ConvertFactor;
                serviceOutDetailSave.CostPrice = serviceOutDetail.CostPrice;
                serviceOutDetailSave.DocumentStatus = serviceOutHeader.DocumentStatus;
                serviceOutDetailSave.GrossAmount = serviceOutDetail.GrossAmount;
                serviceOutDetailSave.GroupOfCompanyID = serviceOutHeader.GroupOfCompanyID;
                serviceOutDetailSave.LineNo = serviceOutDetail.LineNo;
                serviceOutDetailSave.NetAmount = serviceOutDetail.NetAmount;
                serviceOutDetailSave.OrderQty = serviceOutDetail.OrderQty;
                serviceOutDetailSave.BalancedQty = serviceOutDetail.OrderQty;
                serviceOutDetailSave.UnitOfMeasureID = serviceOutDetail.UnitOfMeasureID;
                serviceOutDetailSave.BatchNo = serviceOutDetail.BatchNo;
                serviceOutDetailSave.BaseUnitID = serviceOutDetail.BaseUnitID;

                if (serviceOutHeader.DocumentStatus.Equals(1))
                {
                    context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(serviceOutHeader.LocationID) && ps.ProductID.Equals(serviceOutDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock - serviceOutDetail.OrderQty });
                    context.LgsProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(serviceOutHeader.LocationID) && bd.ProductID.Equals(serviceOutDetail.ProductID) && bd.BatchNo.Equals(serviceOutDetail.BatchNo), bd => new LgsProductBatchNoExpiaryDetail { Qty = bd.Qty - serviceOutDetail.OrderQty });
                }

                context.ServiceOutDetails.Add(serviceOutDetailSave);
                context.SaveChanges();
            }

            foreach (InvProductSerialNoTemp lgsSampleDetailSerialNo in lgsProductSerialNoTemp)
            {
                LgsProductSerialNoDetail lgsProductSerialNoDetailSave = new LgsProductSerialNoDetail();

                if (serviceOutHeader.DocumentStatus.Equals(1))
                {
                    LgsProductSerialNo lgsProductSerialNoSave = new LgsProductSerialNo();

                    lgsProductSerialNoSave.CompanyID = serviceOutHeader.CompanyID;
                    lgsProductSerialNoSave.CostCentreID = serviceOutHeader.CostCentreID;
                    lgsProductSerialNoSave.DocumentStatus = serviceOutHeader.DocumentStatus;
                    lgsProductSerialNoSave.GroupOfCompanyID = serviceOutHeader.GroupOfCompanyID;
                    lgsProductSerialNoSave.LocationID = serviceOutHeader.LocationID;
                    lgsProductSerialNoSave.ProductID = lgsSampleDetailSerialNo.ProductID;
                    lgsProductSerialNoSave.UnitOfMeasureID = lgsSampleDetailSerialNo.UnitOfMeasureID;
                    lgsProductSerialNoSave.SerialNo = lgsSampleDetailSerialNo.SerialNo;
                    lgsProductSerialNoSave.SerialNoStatus = 1;

                    context.LgsProductSerialNos.Add(lgsProductSerialNoSave);
                }

                lgsProductSerialNoDetailSave.CompanyID = serviceOutHeader.CompanyID;
                lgsProductSerialNoDetailSave.CostCentreID = serviceOutHeader.CostCentreID;
                lgsProductSerialNoDetailSave.DocumentStatus = serviceOutHeader.DocumentStatus;
                lgsProductSerialNoDetailSave.GroupOfCompanyID = serviceOutHeader.GroupOfCompanyID;
                lgsProductSerialNoDetailSave.LocationID = serviceOutHeader.LocationID;
                lgsProductSerialNoDetailSave.ProductID = lgsSampleDetailSerialNo.ProductID;
                lgsProductSerialNoDetailSave.LineNo = lgsSampleDetailSerialNo.LineNo;
                lgsProductSerialNoDetailSave.UnitOfMeasureID = lgsSampleDetailSerialNo.UnitOfMeasureID;
                lgsProductSerialNoDetailSave.SerialNo = lgsSampleDetailSerialNo.SerialNo;
                lgsProductSerialNoDetailSave.ReferenceDocumentID = serviceOutHeader.ServiceOutHeaderID;
                lgsProductSerialNoDetailSave.ReferenceDocumentDocumentID = serviceOutHeader.DocumentID;

                context.LgsProductSerialNoDetails.Add(lgsProductSerialNoDetailSave);
                context.SaveChanges();

                context.LgsProductSerialNoDetails.Update(pd => pd.ReferenceDocumentID.Equals(serviceOutHeader.ServiceOutHeaderID) && pd.LocationID.Equals(serviceOutHeader.LocationID) && pd.ReferenceDocumentDocumentID.Equals(serviceOutHeader.DocumentID) && pd.LgsProductSerialNoDetailID.Equals(lgsProductSerialNoDetailSave.LgsProductSerialNoDetailID), pdNew => new LgsProductSerialNoDetail { ProductSerialNoDetailID = lgsProductSerialNoDetailSave.LgsProductSerialNoDetailID });
            }

            return true;
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


        public ServiceOutHeader GetServiceOutHeaderByDocumentNo(string documentNo)
        {
            return context.ServiceOutHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }


        public List<ServiceOutDetailTemp> getPausedServiceOutDetail(ServiceOutHeader serviceOutHeader)
        {
            ServiceOutDetailTemp serviceOutDetailTemp;

            List<ServiceOutDetailTemp> serviceOutDetailTempList = new List<ServiceOutDetailTemp>();


            var serviceDetailTemp = (from pm in context.LgsProductMasters
                                       join sd in context.ServiceOutDetails on pm.LgsProductMasterID equals sd.ProductID
                                       join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       where sd.LocationID.Equals(serviceOutHeader.LocationID) && sd.CompanyID.Equals(serviceOutHeader.CompanyID)
                                       && sd.DocumentID.Equals(serviceOutHeader.DocumentID) && sd.ServiceOutHeaderID.Equals(serviceOutHeader.ServiceOutHeaderID)
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
                                           sd.UnitOfMeasureID,
                                           sd.BatchNo,
                                           sd.BalancedQty,
                                           uc.UnitOfMeasureName

                                       }).ToArray();

            foreach (var tempProduct in serviceDetailTemp)
            {

                serviceOutDetailTemp = new ServiceOutDetailTemp();

                serviceOutDetailTemp.AverageCost = tempProduct.AverageCost;
                serviceOutDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                serviceOutDetailTemp.CostPrice = tempProduct.CostPrice;
                serviceOutDetailTemp.GrossAmount = tempProduct.GrossAmount;
                serviceOutDetailTemp.LineNo = tempProduct.LineNo;
                serviceOutDetailTemp.LocationID = tempProduct.LocationID;
                serviceOutDetailTemp.NetAmount = tempProduct.NetAmount;
                serviceOutDetailTemp.OrderQty = tempProduct.OrderQty;
                serviceOutDetailTemp.BalancedQty = tempProduct.BalancedQty;
                serviceOutDetailTemp.ProductCode = tempProduct.ProductCode;
                serviceOutDetailTemp.ProductID = tempProduct.ProductID;
                serviceOutDetailTemp.ProductName = tempProduct.ProductName;
                serviceOutDetailTemp.BatchNo = tempProduct.BatchNo;
                serviceOutDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                serviceOutDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                serviceOutDetailTempList.Add(serviceOutDetailTemp);
            }
            return serviceOutDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public ServiceOutHeader GetServiceOutByDocumentNo(string documentNo)
        {
            return context.ServiceOutHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public ServiceOutHeader GetServiceOutHeaderToServiceIn(string documentNo)
        {
            return context.ServiceOutHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
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

        
        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public bool IsValidNoOfQty(int qty, ServiceOutDetailTemp serviceOutDetailTemp)
        {
            decimal currentQty = 0;
            var getCurrentQty = (from a in context.ServiceOutDetails
                                 where a.ProductID.Equals(serviceOutDetailTemp.ProductID) && a.ServiceOutHeaderID.Equals(serviceOutDetailTemp.ServiceOutHeaderID) && a.UnitOfMeasureID.Equals(serviceOutDetailTemp.UnitOfMeasureID)
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


        public List<ServiceOutDetailTemp> GetDeleteServiceOutDetailTemp(List<ServiceOutDetailTemp> serviceOutTempDetail, ServiceOutDetailTemp serviceOutDetailTemp)
        {
            ServiceOutDetailTemp ServiceOutTempDetailTemp = new ServiceOutDetailTemp();
            ServiceOutTempDetailTemp = serviceOutTempDetail.Where(p => p.ProductID == serviceOutDetailTemp.ProductID && p.UnitOfMeasureID == serviceOutDetailTemp.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (ServiceOutTempDetailTemp != null)
            {
                serviceOutTempDetail.Remove(ServiceOutTempDetailTemp);
                removedLineNo = ServiceOutTempDetailTemp.LineNo;
            }

            serviceOutTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);
            return serviceOutTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }


        public ServiceOutHeader RecallServiceOutHeaderToServiceIn(long serviceOutHeaderID)  
        {
            return context.ServiceOutHeaders.Where(ph => ph.ServiceOutHeaderID.Equals(serviceOutHeaderID)).FirstOrDefault();
        }


        /// <summary>
        /// Get Service out details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetServiceOutTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.ServiceOutHeaders
                        join pd in context.ServiceOutDetails on ph.ServiceOutHeaderID equals pd.ServiceOutHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                        join e in context.Employees on ph.EmployeeID equals e.EmployeeID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = s.SupplierCode + " " + s.SupplierName,
                                FieldString4 = e.EmployeeCode + " " + e.EmployeeName, 
                                FieldString5 = ph.Remark,
                                FieldString6 = ph.ReferenceNo,
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
                                FieldString17 = pd.BatchNo,
                                FieldString18 = "",
                                FieldString19 = pd.OrderQty,
                                FieldString20 = pd.BalancedQty,
                                FieldString21 = pd.CostPrice,
                                FieldString22 = pd.GrossAmount,
                                FieldString23 = "",
                                FieldString24 = "",
                                FieldString25 = "",
                                FieldString26 = "",
                                FieldString27 = "",
                                FieldString28 = ""
                            });
            return query.AsEnumerable().ToDataTable();

        }

        public List<InvProductSerialNoTemp> getInvSerialNoDetailForServiceOut(ServiceOutHeader serviceOutHeader) 
        {
            List<InvProductSerialNoTemp> lgsProductSerialNoTempList = new List<InvProductSerialNoTemp>();
            List<InvProductSerialNoDetail> lgsProductSerialNoDetail = new List<InvProductSerialNoDetail>();

            lgsProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(serviceOutHeader.ServiceOutHeaderID) && ps.ReferenceDocumentDocumentID.Equals(serviceOutHeader.DocumentID)).ToList();
            foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in lgsProductSerialNoDetail)
            {
                InvProductSerialNoTemp lgsProductSerialNoTemp = new InvProductSerialNoTemp();

                lgsProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                lgsProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                lgsProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                lgsProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }

            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }


        #endregion
    }
}
