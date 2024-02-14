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
using System.Data.Entity;
using MoreLinq;

namespace Service
{
    public class ServiceInService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public ServiceInDetailTemp getServiceInDetailTemp(List<ServiceInDetailTemp> ServiceInDetailTemp, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            ServiceInDetailTemp serviceInDetailTemp = new ServiceInDetailTemp();
            decimal defaultConvertFactor = 1;
            var serviceDetailTemp = (from pm in context.LgsProductMasters
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
                serviceDetailTemp = (dynamic)null;

                serviceDetailTemp = (from pm in context.LgsProductMasters
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

            foreach (var tempProduct in serviceDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !lgsProductMaster.IsExpiry.Equals(true))
                    serviceInDetailTemp = ServiceInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    serviceInDetailTemp = ServiceInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    serviceInDetailTemp = ServiceInDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (serviceInDetailTemp == null)
                {
                    serviceInDetailTemp = new ServiceInDetailTemp();
                    serviceInDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    serviceInDetailTemp.ProductCode = tempProduct.ProductCode;
                    serviceInDetailTemp.ProductName = tempProduct.ProductName;
                    serviceInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    serviceInDetailTemp.CostPrice = tempProduct.CostPrice;
                    serviceInDetailTemp.AverageCost = tempProduct.AverageCost;
                    serviceInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    serviceInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                }
            }
            return serviceInDetailTemp;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.ServiceInHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<ServiceInDetailTemp> getUpdateServiceInDetailTemp(List<ServiceInDetailTemp> serviceInTempDetail, ServiceInDetailTemp serviceInDetail)//Product Master Removed from parameters
        {
            ServiceInDetailTemp serviceInDetailTemp = new ServiceInDetailTemp();

            serviceInDetailTemp = serviceInTempDetail.Where(p => p.ProductID == serviceInDetail.ProductID && p.UnitOfMeasureID == serviceInDetail.UnitOfMeasureID).FirstOrDefault();

            if (serviceInDetailTemp == null || serviceInDetailTemp.LineNo.Equals(0))
            {
                serviceInDetail.LineNo = serviceInTempDetail.Count + 1;
            }
            else
            {
                serviceInTempDetail.Remove(serviceInDetailTemp);
                serviceInDetail.LineNo = serviceInDetailTemp.LineNo;
            }

            serviceInTempDetail.Add(serviceInDetail);

            return serviceInTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public ServiceInHeader GetPausedServiceInHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.ServiceInHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<ServiceInDetailTemp> DeleteProductServiceInDetailTemp(List<ServiceInDetailTemp> serviceInDetailsTemp, string productCode)
        {
            serviceInDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return serviceInDetailsTemp;
        }

        public List<ServiceInDetail> GetAllServiceInDetail()
        {
            return context.ServiceInDetails.ToList();
        }


        public bool Save(ServiceInHeader serviceInHeader, List<ServiceInDetailTemp> serviceInDetailTemp)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (serviceInHeader.ServiceInHeaderID.Equals(0))
                    context.ServiceInHeaders.Add(serviceInHeader);
                else
                    context.Entry(serviceInHeader).State = EntityState.Modified;

                context.SaveChanges();

                List<ServiceInDetail> serviceInDetailDelete = new List<ServiceInDetail>();
                serviceInDetailDelete = context.ServiceInDetails.Where(p => p.ServiceInHeaderID.Equals(serviceInHeader.ServiceInHeaderID) && p.LocationID.Equals(serviceInHeader.LocationID) && p.DocumentID.Equals(serviceInHeader.DocumentID)).ToList();

                foreach (ServiceInDetail serviceInDetailDeleteTemp in serviceInDetailDelete)
                {
                    context.ServiceInDetails.Remove(serviceInDetailDeleteTemp);
                }

                foreach (ServiceInDetailTemp serviceInDetail in serviceInDetailTemp)
                {
                    ServiceInDetail serviceInDetailSave = new ServiceInDetail();

                    serviceInDetailSave.CompanyID = serviceInHeader.CompanyID;
                    serviceInDetailSave.ConvertFactor = serviceInDetail.ConvertFactor;
                    serviceInDetailSave.CostCentreID = serviceInHeader.CostCentreID;
                    serviceInDetailSave.CostPrice = serviceInDetail.CostPrice;
                    serviceInDetailSave.DocumentID = serviceInHeader.DocumentID;
                    serviceInDetailSave.DocumentStatus = serviceInHeader.DocumentStatus;
                    serviceInDetailSave.GrossAmount = serviceInDetail.GrossAmount;
                    serviceInDetailSave.GroupOfCompanyID = serviceInHeader.GroupOfCompanyID;
                    serviceInDetailSave.LineNo = serviceInDetail.LineNo;
                    serviceInDetailSave.BatchNo = serviceInDetail.BatchNo;
                    serviceInDetailSave.LocationID = serviceInHeader.LocationID;
                    serviceInDetailSave.NetAmount = serviceInDetail.NetAmount;
                    serviceInDetailSave.OrderQty = serviceInDetail.OrderQty;
                    serviceInDetailSave.ProductID = serviceInDetail.ProductID;
                    serviceInDetailSave.ServiceInHeaderID = serviceInHeader.ServiceInHeaderID;
                    serviceInDetailSave.OrderQty = serviceInDetail.OrderQty;
                    serviceInDetailSave.NetAmount = serviceInDetail.NetAmount;
                    serviceInDetailSave.UnitOfMeasureID = serviceInDetail.UnitOfMeasureID;
                    serviceInDetailSave.BaseUnitID = serviceInDetail.BaseUnitID;

                    if (serviceInHeader.DocumentStatus.Equals(1))
                    {
                        context.LgsProductStockMasters.Update(ps => ps.LocationID.Equals(serviceInHeader.LocationID) && ps.ProductID.Equals(serviceInDetail.ProductID), ps => new LgsProductStockMaster { Stock = ps.Stock + serviceInDetail.OrderQty });
                        context.ServiceOutDetails.Update(sd => sd.LocationID.Equals(serviceInDetailSave.LocationID) && sd.ProductID.Equals(serviceInDetailSave.ProductID) && sd.ServiceOutHeaderID.Equals(serviceInHeader.ServiceOutHeaderID), sd => new ServiceOutDetail { BalancedQty = sd.BalancedQty - serviceInDetailSave.OrderQty });
                        context.LgsProductBatchNoExpiaryDetails.Update(bd => bd.LocationID.Equals(serviceInHeader.LocationID) && bd.ProductID.Equals(serviceInDetail.ProductID) && bd.BatchNo.Equals(serviceInDetail.BatchNo), bd => new LgsProductBatchNoExpiaryDetail { Qty = bd.Qty + serviceInDetailSave.OrderQty });
                    }
                    context.ServiceInDetails.Add(serviceInDetailSave);
                    context.SaveChanges();
                }

                if (serviceInHeader.DocumentStatus.Equals(1))
                {
                    context.ServiceOutHeaders.Update(sh => sh.LocationID.Equals(serviceInHeader.LocationID) && sh.ServiceOutHeaderID.Equals(serviceInHeader.ServiceOutHeaderID), sh => new ServiceOutHeader { TotalBalancedQty = sh.TotalBalancedQty - serviceInHeader.TotalQty });

                    decimal sohQty = context.ServiceOutHeaders.Where(sh => sh.LocationID.Equals(serviceInHeader.LocationID) && sh.ServiceOutHeaderID.Equals(serviceInHeader.ServiceOutHeaderID)).Select(sh => sh.TotalBalancedQty).FirstOrDefault();

                    if (sohQty.Equals(0))
                    {
                        context.ServiceOutHeaders.Update(sh => sh.LocationID.Equals(serviceInHeader.LocationID) && sh.ServiceOutHeaderID.Equals(serviceInHeader.ServiceOutHeaderID), sh => new ServiceOutHeader { Balanced = 1 });
                    }
                }

                transaction.Complete();

                return true;
            }
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


        public ServiceInHeader GetServiceInByDocumentNo(string documentNo)
        {
            return context.ServiceInHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<ServiceInDetailTemp> GetPausedServiceInDetail(ServiceInHeader serviceInHeader)
        {
            ServiceInDetailTemp serviceInDetailTemp;

            List<ServiceInDetailTemp> serviceInDetailTempList = new List<ServiceInDetailTemp>();

            var serviceInTemp = (from pm in context.LgsProductMasters
                                 join sd in context.ServiceInDetails on pm.LgsProductMasterID equals sd.ProductID
                                 join uc in context.UnitOfMeasures on sd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                 where sd.LocationID.Equals(serviceInHeader.LocationID) && sd.CompanyID.Equals(serviceInHeader.CompanyID)
                                 && sd.DocumentID.Equals(serviceInHeader.DocumentID) && sd.ServiceInHeaderID.Equals(serviceInHeader.ServiceInHeaderID)
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
                                     sd.BatchNo,
                                     sd.UnitOfMeasureID,
                                     uc.UnitOfMeasureName

                                 }).ToArray();

            foreach (var tempProduct in serviceInTemp)
            {

                serviceInDetailTemp = new ServiceInDetailTemp();

                serviceInDetailTemp.AverageCost = tempProduct.AverageCost;
                serviceInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                serviceInDetailTemp.CostPrice = tempProduct.CostPrice;
                serviceInDetailTemp.BatchNo = tempProduct.BatchNo; 
                serviceInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                serviceInDetailTemp.LineNo = tempProduct.LineNo;
                serviceInDetailTemp.LocationID = tempProduct.LocationID;
                serviceInDetailTemp.NetAmount = tempProduct.NetAmount;
                serviceInDetailTemp.OrderQty = tempProduct.OrderQty;
                serviceInDetailTemp.ProductCode = tempProduct.ProductCode;
                serviceInDetailTemp.ProductID = tempProduct.ProductID;
                serviceInDetailTemp.ProductName = tempProduct.ProductName;
                serviceInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                serviceInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                serviceInDetailTempList.Add(serviceInDetailTemp);
            }
            return serviceInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

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

        public ServiceInHeader getServiceInHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.ServiceInHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.ServiceInHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public ServiceInHeader getServiceInHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.ServiceInHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, LgsProductSerialNoTemp lgsProductSerialNoTemp, int locationID)
        {
            ServiceInHeader serviceInHeader = new ServiceInHeader();
            serviceInHeader = getServiceInHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
            if (serviceInHeader == null)
                serviceInHeader = new ServiceInHeader();

            LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
            LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(lgsProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(serviceInHeader.ServiceInHeaderID) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
            if (LgsProductSerialNoDetailtmp != null)
            {
                serviceInHeader = getServiceInHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                if (serviceInHeader != null)
                    return serviceInHeader.DocumentNo;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
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

        public List<ServiceInDetailTemp> getServiceOutDetailForServiceIn(ServiceOutHeader serviceOutHeader) 
        {
            ServiceInDetailTemp serviceInDetailTemp;

            List<ServiceInDetailTemp> serviceInDetailTempList = new List<ServiceInDetailTemp>();


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

                serviceInDetailTemp = new ServiceInDetailTemp();

                serviceInDetailTemp.AverageCost = tempProduct.AverageCost;
                serviceInDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                serviceInDetailTemp.CostPrice = tempProduct.CostPrice;
                serviceInDetailTemp.GrossAmount = tempProduct.GrossAmount;
                serviceInDetailTemp.LineNo = tempProduct.LineNo;
                serviceInDetailTemp.LocationID = tempProduct.LocationID;
                serviceInDetailTemp.NetAmount = tempProduct.NetAmount;
                serviceInDetailTemp.OrderQty = tempProduct.OrderQty;
                serviceInDetailTemp.BalancedQty = tempProduct.BalancedQty;
                serviceInDetailTemp.ProductCode = tempProduct.ProductCode;
                serviceInDetailTemp.ProductID = tempProduct.ProductID;
                serviceInDetailTemp.ProductName = tempProduct.ProductName;
                serviceInDetailTemp.BatchNo = tempProduct.BatchNo;
                serviceInDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                serviceInDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                serviceInDetailTempList.Add(serviceInDetailTemp);
            }
            return serviceInDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        /// Get Service in details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetServiceInTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.ServiceInHeaders
                        join pd in context.ServiceInDetails on ph.ServiceInHeaderID equals pd.ServiceInHeaderID
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
                                FieldString17 = pd.BatchNo,
                                FieldString18 = "",
                                FieldString19 = pd.OrderQty,
                                FieldString20 = "",//pd.BalancedQty,
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

        #endregion
    }
}
