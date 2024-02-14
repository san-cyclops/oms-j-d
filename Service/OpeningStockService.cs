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
using System.Data.Common;
using System.Data.Entity;
using MoreLinq;
using System.Collections;

namespace Service
{
    public class OpeningStockService
    {
        ERPDbContext context = new ERPDbContext();
        public static bool isInv;

        #region Methods

        public OpeningStockDetailTemp getOpeningStockDetailTemp(List<OpeningStockDetailTemp> OpeningStockDetailTemp, LgsProductMaster lgsProductMaster, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID, bool isInv)
        {
            if (isInv)
            {
                OpeningStockDetailTemp openingStockDetailTemp = new OpeningStockDetailTemp();
                decimal defaultConvertFactor = 1;
                var openingStockTemp = (from pm in context.InvProductMasters
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
                    openingStockTemp = (dynamic)null;

                    openingStockTemp = (from pm in context.InvProductMasters
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

                foreach (var tempProduct in openingStockTemp)
                {
                    if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                    if (openingStockDetailTemp == null)
                    {
                        openingStockDetailTemp = new OpeningStockDetailTemp();
                        openingStockDetailTemp.ProductID = tempProduct.InvProductMasterID;
                        openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                        openingStockDetailTemp.ProductName = tempProduct.ProductName;
                        openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                        openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                        openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    }
                }

                return openingStockDetailTemp;
            }
            else
            {
                OpeningStockDetailTemp openingStockDetailTemp = new OpeningStockDetailTemp();
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
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                    else
                        openingStockDetailTemp = OpeningStockDetailTemp.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                    if (openingStockDetailTemp == null)
                    {
                        openingStockDetailTemp = new OpeningStockDetailTemp();
                        openingStockDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                        openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                        openingStockDetailTemp.ProductName = tempProduct.ProductName;
                        openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                        openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                        openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                        openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                        openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;

                    }
                }

                return openingStockDetailTemp;
            }
        }


        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.OpeningStockHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public List<OpeningStockDetailTemp> GetUpdateOpeningStockDetailTemp(List<OpeningStockDetailTemp> openingStockTempDetail, OpeningStockDetailTemp openingStockDetail)//Product Master Removed from parameters
        {
            OpeningStockDetailTemp openingStockDetailTemp = new OpeningStockDetailTemp();

            openingStockDetailTemp = openingStockTempDetail.Where(p => p.ProductID == openingStockDetail.ProductID && p.UnitOfMeasureID == openingStockDetail.UnitOfMeasureID).FirstOrDefault();

            if (openingStockDetailTemp == null || openingStockDetailTemp.LineNo.Equals(0))
            {
                openingStockDetail.LineNo = openingStockTempDetail.Count + 1;
            }
            else
            {
                openingStockTempDetail.Remove(openingStockDetailTemp);
                openingStockDetail.LineNo = openingStockDetailTemp.LineNo;
            }

            openingStockTempDetail.Add(openingStockDetail);

            return openingStockTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public OpeningStockHeader GetPausedOpeningStockHeaderByDocumentNo(int documentID, string documentNo, int locationID) 
        {
            return context.OpeningStockHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<OpeningStockDetailTemp> DeleteProductOpeningStockDetailTemp(List<OpeningStockDetailTemp> openingStockDetailTemp, string productCode)
        {
            openingStockDetailTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return openingStockDetailTemp;
        }

        public List<OpeningStockDetail> GetAllOpeningStockDetail() 
        {
            return context.OpeningStockDetails.ToList();
        }


        public bool Save(OpeningStockHeader openingStockHeader, List<OpeningStockDetailTemp> openingStockDetailTemp, List<InvProductSerialNoTemp> invProductSerialNoTemp, out string newDocumentNo, string formName = "")
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //if (openingStockHeader.OpeningStockHeaderID.Equals(0))
                //    context.OpeningStockHeaders.Add(openingStockHeader);
                //else
                //    context.Entry(openingStockHeader).State = EntityState.Modified;

                //context.SaveChanges();

                if (openingStockHeader.DocumentStatus.Equals(1))
                {
                    OpeningStockService openingStockService = new OpeningStockService();
                    LocationService locationService = new LocationService();
                    openingStockHeader.DocumentNo = openingStockService.GetDocumentNo(formName, openingStockHeader.LocationID, locationService.GetLocationsByID(openingStockHeader.LocationID).LocationCode, openingStockHeader.DocumentID, false, isInv).Trim();
                }

                newDocumentNo = openingStockHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                if (openingStockHeader.OpeningStockHeaderID.Equals(0))
                    context.OpeningStockHeaders.Add(openingStockHeader);
                else
                    context.Entry(openingStockHeader).State = EntityState.Modified;

                context.SaveChanges();


                context.Set<OpeningStockDetail>().Delete(context.OpeningStockDetails.Where(os => os.OpeningStockHeaderID.Equals(openingStockHeader.OpeningStockHeaderID) && os.LocationID.Equals(openingStockHeader.LocationID) && os.DocumentID.Equals(openingStockHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var openingStockDetailSaveQuery = (from os in openingStockDetailTemp
                                                   select new
                                                   {
                                                       OpeningStockDetailID = 0,
                                                       StockDetailID = 0,
                                                       OpeningStockHeaderID = openingStockHeader.OpeningStockHeaderID,
                                                       CompanyID = openingStockHeader.CompanyID,
                                                       LocationID = openingStockHeader.LocationID,
                                                       CostCentreID = openingStockHeader.CostCentreID,
                                                       ProductID = os.ProductID,
                                                       DocumentID = openingStockHeader.DocumentID,
                                                       DocumentDate = Common.ConvertStringToDateTime(openingStockHeader.DocumentDate.ToString()),
                                                       //OpeningStockType = openingStockHeader.OpeningStockType,
                                                       OrderQty = os.OrderQty,
                                                       BatchNo = (openingStockHeader.OpeningStockType == 1 ? "OSIN00000000001" : "OSLG00000000001"),
                                                       BaseUnitID = os.BaseUnitID,
                                                       ConvertFactor = os.ConvertFactor,
                                                       ExpiryDate = Common.ConvertStringToDateTime(os.ExpiryDate.ToString()),
                                                       CostPrice = os.CostPrice,
                                                       SellingPrice = os.SellingPrice,
                                                       UnitOfMeasureID = os.UnitOfMeasureID,
                                                       CostValue = os.CostValue,
                                                       SellingValue = os.SellingValue,
                                                       LineNo = os.LineNo,
                                                       DocumentStatus = openingStockHeader.DocumentStatus,
                                                       GroupOfCompanyID = Common.GroupOfCompanyID,
                                                       CreatedUser = openingStockHeader.CreatedUser,
                                                       CreatedDate = Common.ConvertStringToDateTime(openingStockHeader.CreatedDate.ToString()),
                                                       ModifiedUser = openingStockHeader.ModifiedUser,
                                                       ModifiedDate = Common.ConvertStringToDateTime(openingStockHeader.ModifiedDate.ToString()),
                                                       DataTransfer = openingStockHeader.DataTransfer
                                                   }).ToList();

                CommonService.BulkInsert(Common.LINQToDataTable(openingStockDetailSaveQuery), "OpeningStockDetail");

                if (isInv)
                {
                    context.Set<InvProductSerialNoDetail>().Delete(context.InvProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && p.LocationID.Equals(openingStockHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(openingStockHeader.DocumentID)).AsQueryable());
                    context.SaveChanges();
                }
                else
                {
                    context.Set<LgsProductSerialNoDetail>().Delete(context.LgsProductSerialNoDetails.Where(p => p.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && p.LocationID.Equals(openingStockHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(openingStockHeader.DocumentID)).AsQueryable());
                    context.SaveChanges();
                }

                if (isInv)
                {
                    if (openingStockHeader.DocumentStatus.Equals(1))
                    {
                        var invProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                                                           select new
                                                           {
                                                               InvProductSerialNoID = 0,
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = openingStockHeader.GroupOfCompanyID,
                                                               CompanyID = openingStockHeader.CompanyID,
                                                               LocationID = openingStockHeader.LocationID,
                                                               CostCentreID = openingStockHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = openingStockHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = openingStockHeader.DocumentID,
                                                               DocumentStatus = openingStockHeader.DocumentStatus,
                                                               CreatedUser = openingStockHeader.CreatedUser,
                                                               CreatedDate = openingStockHeader.CreatedDate,
                                                               ModifiedUser = openingStockHeader.ModifiedUser,
                                                               ModifiedDate = openingStockHeader.ModifiedDate,
                                                               DataTransfer = openingStockHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoSaveQuery), "InvProductSerialNo");

                    }
                    else
                    {

                        var invProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     InvProductSerialNoDetailID = 0,
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = openingStockHeader.GroupOfCompanyID,
                                                                     CompanyID = openingStockHeader.CompanyID,
                                                                     LocationID = openingStockHeader.LocationID,
                                                                     CostCentreID = openingStockHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = openingStockHeader.DocumentID,
                                                                     ReferenceDocumentID = openingStockHeader.OpeningStockHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = openingStockHeader.DocumentStatus,
                                                                     BatchNo = openingStockHeader.DocumentNo,
                                                                     CreatedUser = openingStockHeader.CreatedUser,
                                                                     CreatedDate = openingStockHeader.CreatedDate,
                                                                     ModifiedUser = openingStockHeader.ModifiedUser,
                                                                     ModifiedDate = openingStockHeader.ModifiedDate,
                                                                     DataTransfer = openingStockHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(invProductSerialNoDetailSaveQuery), "InvProductSerialNoDetail");
                    }

                    context.SaveChanges();
                }
                else
                {
                    if (openingStockHeader.DocumentStatus.Equals(1))
                    {
                        var lgsProductSerialNoSaveQuery = (from pd in invProductSerialNoTemp
                                                           select new
                                                           {
                                                               LgsProductSerialNoID = 0,
                                                               ProductSerialNoID = 0,
                                                               GroupOfCompanyID = openingStockHeader.GroupOfCompanyID,
                                                               CompanyID = openingStockHeader.CompanyID,
                                                               LocationID = openingStockHeader.LocationID,
                                                               CostCentreID = openingStockHeader.CostCentreID,
                                                               ProductID = pd.ProductID,
                                                               BatchNo = openingStockHeader.DocumentNo,
                                                               UnitOfMeasureID = pd.UnitOfMeasureID,
                                                               ExpiryDate = pd.ExpiryDate,
                                                               SerialNo = pd.SerialNo,
                                                               SerialNoStatus = openingStockHeader.DocumentID,
                                                               DocumentStatus = openingStockHeader.DocumentStatus,
                                                               CreatedUser = openingStockHeader.CreatedUser,
                                                               CreatedDate = openingStockHeader.CreatedDate,
                                                               ModifiedUser = openingStockHeader.ModifiedUser,
                                                               ModifiedDate = openingStockHeader.ModifiedDate,
                                                               DataTransfer = openingStockHeader.DataTransfer
                                                           }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoSaveQuery), "LgsProductSerialNo");

                    }
                    else
                    {
                        var lgsProductSerialNoDetailSaveQuery = (from pd in invProductSerialNoTemp
                                                                 select new
                                                                 {
                                                                     LgsProductSerialNoDetailID = 0,
                                                                     ProductSerialNoDetailID = 0,
                                                                     GroupOfCompanyID = openingStockHeader.GroupOfCompanyID,
                                                                     CompanyID = openingStockHeader.CompanyID,
                                                                     LocationID = openingStockHeader.LocationID,
                                                                     CostCentreID = openingStockHeader.CostCentreID,
                                                                     LineNo = pd.LineNo,
                                                                     ProductID = pd.ProductID,
                                                                     UnitOfMeasureID = pd.UnitOfMeasureID,
                                                                     ExpiryDate = pd.ExpiryDate,
                                                                     ReferenceDocumentDocumentID = openingStockHeader.DocumentID,
                                                                     ReferenceDocumentID = openingStockHeader.OpeningStockHeaderID,
                                                                     SerialNo = pd.SerialNo,
                                                                     DocumentStatus = openingStockHeader.DocumentStatus,
                                                                     BatchNo = openingStockHeader.DocumentNo,
                                                                     CreatedUser = openingStockHeader.CreatedUser,
                                                                     CreatedDate = openingStockHeader.CreatedDate,
                                                                     ModifiedUser = openingStockHeader.ModifiedUser,
                                                                     ModifiedDate = openingStockHeader.ModifiedDate,
                                                                     DataTransfer = openingStockHeader.DataTransfer
                                                                 }).ToList();

                        CommonService.BulkInsert(Common.LINQToDataTable(lgsProductSerialNoDetailSaveQuery), "LgsProductSerialNoDetail");
                    }

                    context.SaveChanges();
                }

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@openingStockHeaderID", Value=openingStockHeader.OpeningStockHeaderID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=openingStockHeader.LocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=openingStockHeader.DocumentID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=openingStockHeader.DocumentStatus},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@OpeningStockType", Value=openingStockHeader.OpeningStockType}                      
                    };

                if (CommonService.ExecuteStoredProcedure("spOpeningStockSave", parameter))
                {
                    transaction.Complete();
                    return true;
                }
                else
                { return false; }

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


        public OpeningStockHeader GetOpeningStockByDocumentNo(string documentNo)
        {
            return context.OpeningStockHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<OpeningStockDetailTemp> getPausedOpeningStockDetail(OpeningStockHeader openingStockHeader, bool isInv)
        {
            OpeningStockDetailTemp openingStockDetailTemp;

            List<OpeningStockDetailTemp> openingStockDetailTempList = new List<OpeningStockDetailTemp>();

            if (isInv)
            {
                var openingStockTemp = (from pm in context.InvProductMasters
                                        join os in context.OpeningStockDetails on pm.InvProductMasterID equals os.ProductID
                                        join uc in context.UnitOfMeasures on os.UnitOfMeasureID equals uc.UnitOfMeasureID
                                        where os.LocationID.Equals(openingStockHeader.LocationID) && os.CompanyID.Equals(openingStockHeader.CompanyID)
                                        && os.DocumentID.Equals(openingStockHeader.DocumentID) && os.OpeningStockHeaderID.Equals(openingStockHeader.OpeningStockHeaderID)
                                        select new
                                        {
                                            os.ConvertFactor,
                                            os.CostPrice,
                                            os.SellingPrice,
                                            os.ExpiryDate,
                                            os.CostValue,
                                            os.SellingValue,
                                            os.LineNo,
                                            os.LocationID,
                                            os.OrderQty,
                                            os.BatchNo,
                                            pm.ProductCode,
                                            os.ProductID,
                                            pm.ProductName,
                                            os.UnitOfMeasureID,
                                            uc.UnitOfMeasureName
                                        }).ToArray();

                foreach (var tempProduct in openingStockTemp)
                {
                    openingStockDetailTemp = new OpeningStockDetailTemp();

                    openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                    openingStockDetailTemp.CostValue = tempProduct.CostValue;
                    openingStockDetailTemp.SellingValue = tempProduct.SellingValue;
                    openingStockDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    openingStockDetailTemp.LineNo = tempProduct.LineNo;
                    openingStockDetailTemp.LocationID = tempProduct.LocationID;
                    openingStockDetailTemp.OrderQty = tempProduct.OrderQty;
                    openingStockDetailTemp.BatchNo = tempProduct.BatchNo;
                    openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                    openingStockDetailTemp.ProductID = tempProduct.ProductID;
                    openingStockDetailTemp.ProductName = tempProduct.ProductName;
                    openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    openingStockDetailTempList.Add(openingStockDetailTemp);
                }
                return openingStockDetailTempList.OrderBy(pd => pd.LineNo).ToList();
            }
            else
            {
                var openingStockTemp = (from pm in context.LgsProductMasters
                                        join os in context.OpeningStockDetails on pm.LgsProductMasterID equals os.ProductID
                                        join uc in context.UnitOfMeasures on os.UnitOfMeasureID equals uc.UnitOfMeasureID
                                        where os.LocationID.Equals(openingStockHeader.LocationID) && os.CompanyID.Equals(openingStockHeader.CompanyID)
                                        && os.DocumentID.Equals(openingStockHeader.DocumentID) && os.OpeningStockHeaderID.Equals(openingStockHeader.OpeningStockHeaderID)
                                        select new
                                        {
                                            os.ConvertFactor,
                                            os.CostPrice,
                                            os.SellingPrice,
                                            os.ExpiryDate,
                                            os.CostValue,
                                            os.SellingValue,
                                            os.LineNo,
                                            os.LocationID,
                                            os.OrderQty,
                                            os.BatchNo,
                                            pm.ProductCode,
                                            os.ProductID,
                                            pm.ProductName,
                                            os.UnitOfMeasureID,
                                            uc.UnitOfMeasureName

                                        }).ToArray();

                foreach (var tempProduct in openingStockTemp)
                {
                    openingStockDetailTemp = new OpeningStockDetailTemp();

                    openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                    openingStockDetailTemp.CostValue = tempProduct.CostValue;
                    openingStockDetailTemp.SellingValue = tempProduct.SellingValue;
                    openingStockDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    openingStockDetailTemp.LineNo = tempProduct.LineNo;
                    openingStockDetailTemp.LocationID = tempProduct.LocationID;
                    openingStockDetailTemp.OrderQty = tempProduct.OrderQty;
                    openingStockDetailTemp.BatchNo = tempProduct.BatchNo;
                    openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                    openingStockDetailTemp.ProductID = tempProduct.ProductID;
                    openingStockDetailTemp.ProductName = tempProduct.ProductName;
                    openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                    openingStockDetailTempList.Add(openingStockDetailTemp);
                }
                return openingStockDetailTempList.OrderBy(pd => pd.LineNo).ToList();
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

        public OpeningStockHeader getOpeningStockHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.OpeningStockHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.OpeningStockHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public OpeningStockHeader getOpeningStockHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.OpeningStockHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string getPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, LgsProductSerialNoTemp lgsProductSerialNoTemp, int locationID, bool isInv)
        {
            if (isInv)
            {
                OpeningStockHeader openingStockHeader = new OpeningStockHeader();
                openingStockHeader = getOpeningStockHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (openingStockHeader == null)
                    openingStockHeader = new OpeningStockHeader();

                InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(invProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && sd.ProductID.Equals(invProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(invProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (InvProductSerialNoDetailtmp != null)
                {
                    openingStockHeader = getOpeningStockHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (openingStockHeader != null)
                        return openingStockHeader.DocumentNo;
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
                OpeningStockHeader openingStockHeader = new OpeningStockHeader();
                openingStockHeader = getOpeningStockHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
                if (openingStockHeader == null)
                    openingStockHeader = new OpeningStockHeader();

                LgsProductSerialNoDetail LgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
                LgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo.Equals(serialNo) && sd.ReferenceDocumentDocumentID.Equals(lgsProductSerialNoTemp.DocumentID) && !sd.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && sd.ProductID.Equals(lgsProductSerialNoTemp.ProductID) && sd.UnitOfMeasureID.Equals(lgsProductSerialNoTemp.UnitOfMeasureID) && sd.DocumentStatus.Equals(0)).FirstOrDefault();
                if (LgsProductSerialNoDetailtmp != null)
                {
                    openingStockHeader = getOpeningStockHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, LgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                    if (openingStockHeader != null)
                        return openingStockHeader.DocumentNo;
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

        public List<InvProductBatchNoTemp> GetInvBatchNoDetail(InvProductMaster invProductMaster) 
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
                invProductBatchNoDetailTemp.UnitOfMeasureID = invProductBatchNoTemp.UnitOfMeasureID;

                invProductBatchNoTempList.Add(invProductBatchNoDetailTemp);
            }

            return invProductBatchNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }


        public List<LgsProductBatchNoTemp> GetLgsBatchNoDetail(LgsProductMaster lgsProductMaster)
        {
            List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoDetail = new List<LgsProductBatchNoExpiaryDetail>();

            lgsProductBatchNoDetail = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
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

        public List<InvProductBatchNoTemp> GetInvExpiryDetail(InvProductMaster invProductMaster) 
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

        public List<LgsProductBatchNoTemp> GetLgsExpiryDetail(LgsProductMaster lgsProductMaster)
        {
            List<LgsProductBatchNoTemp> lgsProductBatchNoTempList = new List<LgsProductBatchNoTemp>();

            List<LgsProductBatchNoExpiaryDetail> lgsProductBatchNoDetail = new List<LgsProductBatchNoExpiaryDetail>();

            lgsProductBatchNoDetail = context.LgsProductBatchNoExpiaryDetails.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
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

        public List<InvProductSerialNoTemp> GetInvSerialNoDetail(InvProductMaster invProductMaster) 
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

        public List<LgsProductSerialNoTemp> GetLgsSerialNoDetail(LgsProductMaster lgsProductMaster) 
        {
            List<LgsProductSerialNoTemp> lgsProductSerialNoTempList = new List<LgsProductSerialNoTemp>();

            List<LgsProductSerialNo> lgsProductSerialNo = new List<LgsProductSerialNo>();

            lgsProductSerialNo = context.LgsProductSerialNos.Where(ps => ps.ProductID.Equals(lgsProductMaster.LgsProductMasterID)).ToList();
            foreach (LgsProductSerialNo lgsProductSerialNoRetrive in lgsProductSerialNo)
            {
                LgsProductSerialNoTemp lgsProductSerialNoTemp = new LgsProductSerialNoTemp();

                lgsProductSerialNoTemp.ProductID = lgsProductSerialNoRetrive.ProductID;
                lgsProductSerialNoTemp.SerialNo = lgsProductSerialNoRetrive.SerialNo;
                lgsProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoRetrive.UnitOfMeasureID;

                lgsProductSerialNoTempList.Add(lgsProductSerialNoTemp);
            }

            return lgsProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
        }

        public InvProductBatchNoExpiaryDetail GetInvProductBatchNoByBatchNo(string batchNo)
        {
            return context.InvProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public LgsProductBatchNoExpiaryDetail GetLgsProductBatchNoByBatchNo(string batchNo)
        {
            return context.LgsProductBatchNoExpiaryDetails.Where(a => a.BatchNo.Equals(batchNo)).FirstOrDefault();
        }

        public List<OpeningStockDetailTemp> GetDeleteOpeningStockDetailTemp(List<OpeningStockDetailTemp> openingStockTempDetail, OpeningStockDetailTemp openingStockDetailTemp)
        {
            OpeningStockDetailTemp openingStockTempDetailTemp = new OpeningStockDetailTemp();
            openingStockTempDetailTemp = openingStockTempDetail.Where(p => p.ProductID == openingStockDetailTemp.ProductID && p.UnitOfMeasureID == openingStockDetailTemp.UnitOfMeasureID).FirstOrDefault();
            long removedLineNo = 0;

            if (openingStockTempDetailTemp != null)
            {
                openingStockTempDetail.Remove(openingStockTempDetailTemp);
                removedLineNo = openingStockTempDetailTemp.LineNo;
            }

            openingStockTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            return openingStockTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        //public List<OpeningStockDetailTemp> GetDeleteInvOpeningStockDetailTemp(List<OpeningStockDetailTemp> openingStockTempDetail, OpeningStockDetailTemp openingStockDetailTemp)
        //{
        //    OpeningStockDetailTemp openingStockTempDetailTemp = new OpeningStockDetailTemp();

        //    openingStockTempDetailTemp = openingStockTempDetail.Where(p => p.ProductID == openingStockDetailTemp.ProductID && p.UnitOfMeasureID == openingStockDetailTemp.UnitOfMeasureID).FirstOrDefault();

        //    if (openingStockTempDetailTemp != null)
        //    {
        //        openingStockTempDetail.Remove(openingStockTempDetailTemp);
        //    }

        //    return openingStockTempDetail.OrderBy(pd => pd.LineNo).ToList();
        //}

        public OpeningStockHeader GetPausedOpeningStockHeaderByDocumentID(int documentDocumentID, long documentID, int locationID)
        {
            return context.OpeningStockHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.OpeningStockHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID) && ph.DocumentStatus.Equals(0)).FirstOrDefault();
        }


        public string GetPausedSerialNoDetailByDocumentNo(string serialNo, string documentNo, InvProductSerialNoTemp invProductSerialNoTemp, int locationID)
        {
            OpeningStockHeader openingStockHeader = new OpeningStockHeader();
            openingStockHeader = getOpeningStockHeaderByDocumentNo(invProductSerialNoTemp.DocumentID, documentNo, locationID);
            if (openingStockHeader == null)
                openingStockHeader = new OpeningStockHeader();

            InvProductSerialNoDetail InvProductSerialNoDetailtmp = new InvProductSerialNoDetail();
            InvProductSerialNoDetailtmp = null;

            if (invProductSerialNoTemp.ExpiryDate != null)
            {
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                            && sd.ReferenceDocumentDocumentID == invProductSerialNoTemp.DocumentID
                                            && sd.ReferenceDocumentID != openingStockHeader.OpeningStockHeaderID
                                            && sd.ProductID == invProductSerialNoTemp.ProductID
                                            && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID
                                            && sd.ExpiryDate == invProductSerialNoTemp.ExpiryDate
                                            && sd.DocumentStatus == 0).FirstOrDefault();
            }
            else
            {
                InvProductSerialNoDetailtmp = context.InvProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
                                           && sd.ReferenceDocumentDocumentID == invProductSerialNoTemp.DocumentID
                                           && sd.ReferenceDocumentID != openingStockHeader.OpeningStockHeaderID
                                           && sd.ProductID == invProductSerialNoTemp.ProductID
                                           && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID
                                           && sd.DocumentStatus == 0).FirstOrDefault();
            }


            if (InvProductSerialNoDetailtmp != null)
            {
                openingStockHeader = GetPausedOpeningStockHeaderByDocumentID(invProductSerialNoTemp.DocumentID, InvProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
                if (openingStockHeader != null)
                    return openingStockHeader.DocumentNo;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }

        }

        //public string GetPausedLgsSerialNoDetailByDocumentNo(string serialNo, string documentNo, LgsProductSerialNoTemp lgsProductSerialNoTemp, int locationID) 
        //{
        //    OpeningStockHeader openingStockHeader = new OpeningStockHeader();
        //    openingStockHeader = getOpeningStockHeaderByDocumentNo(lgsProductSerialNoTemp.DocumentID, documentNo, locationID);
        //    if (openingStockHeader == null)
        //        openingStockHeader = new OpeningStockHeader();

        //    LgsProductSerialNoDetail lgsProductSerialNoDetailtmp = new LgsProductSerialNoDetail();
        //    lgsProductSerialNoDetailtmp = null;

        //    if (lgsProductSerialNoTemp.ExpiryDate != null)
        //    {
        //        lgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
        //                                    && sd.ReferenceDocumentDocumentID == lgsProductSerialNoTemp.DocumentID
        //                                    && sd.ReferenceDocumentID != openingStockHeader.OpeningStockHeaderID
        //                                    && sd.ProductID == lgsProductSerialNoTemp.ProductID
        //                                    && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID
        //                                    && sd.ExpiryDate == lgsProductSerialNoTemp.ExpiryDate
        //                                    && sd.DocumentStatus == 0).FirstOrDefault();
        //    }
        //    else
        //    {
        //        lgsProductSerialNoDetailtmp = context.LgsProductSerialNoDetails.Where(sd => sd.SerialNo == serialNo
        //                                   && sd.ReferenceDocumentDocumentID == lgsProductSerialNoTemp.DocumentID
        //                                   && sd.ReferenceDocumentID != openingStockHeader.OpeningStockHeaderID
        //                                   && sd.ProductID == lgsProductSerialNoTemp.ProductID
        //                                   && sd.UnitOfMeasureID == lgsProductSerialNoTemp.UnitOfMeasureID
        //                                   && sd.DocumentStatus == 0).FirstOrDefault();
        //    }


        //    if (lgsProductSerialNoDetailtmp != null)
        //    {
        //        openingStockHeader = GetPausedOpeningStockHeaderByDocumentID(lgsProductSerialNoTemp.DocumentID, lgsProductSerialNoDetailtmp.ReferenceDocumentID, locationID);
        //        if (openingStockHeader != null)
        //            return openingStockHeader.DocumentNo;
        //        else
        //            return string.Empty;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }

        //}

        public bool IsExistsSavedSerialNoDetailByDocumentNo(string serialNo, InvProductSerialNoTemp invProductSerialNoTemp, int locationID)
        {

            LgsProductSerialNo lgsProductSerialNo = new LgsProductSerialNo();
            //invProductSerialNo = context.InvProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID && sd.ExpiryDate == invProductSerialNoTemp.ExpiryDate && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();
            if (invProductSerialNoTemp.ExpiryDate != null)
                lgsProductSerialNo = context.LgsProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID && sd.ExpiryDate == invProductSerialNoTemp.ExpiryDate && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();
            else
                lgsProductSerialNo = context.LgsProductSerialNos.Where(sd => sd.SerialNo == serialNo && sd.ProductID == invProductSerialNoTemp.ProductID && sd.UnitOfMeasureID == invProductSerialNoTemp.UnitOfMeasureID && sd.DocumentStatus == 1 && sd.LocationID == locationID).FirstOrDefault();

            if (lgsProductSerialNo != null && lgsProductSerialNo.LgsProductSerialNoID != 0)
                return true;
            else
                return false;
        }

        public List<InvProductSerialNoTemp> GetUpdateSerialNoTemp(List<InvProductSerialNoTemp> lgsProductSerialNoList, InvProductSerialNoTemp lgsProductSerialNumber)
        {
            InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

            invProductSerialNoTemp = lgsProductSerialNoList.Where(s => s.SerialNo.Equals(lgsProductSerialNumber.SerialNo) && s.ProductID.Equals(lgsProductSerialNumber.ProductID) && s.UnitOfMeasureID.Equals(lgsProductSerialNumber.UnitOfMeasureID) && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).FirstOrDefault();

            if (invProductSerialNoTemp == null || invProductSerialNoTemp.LineNo.Equals(0))
            {
                if (lgsProductSerialNoList.Count.Equals(0))
                    lgsProductSerialNumber.LineNo = 1;
                else
                {
                    if (!lgsProductSerialNoList.Where(s => s.ProductID == lgsProductSerialNumber.ProductID && s.UnitOfMeasureID == lgsProductSerialNumber.UnitOfMeasureID && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Count().Equals(0))
                        lgsProductSerialNumber.LineNo = lgsProductSerialNoList.Where(s => s.ProductID == lgsProductSerialNumber.ProductID && s.UnitOfMeasureID == lgsProductSerialNumber.UnitOfMeasureID && s.ExpiryDate.Equals(lgsProductSerialNumber.ExpiryDate)).Max(s => s.LineNo) + 1;
                    else
                        lgsProductSerialNumber.LineNo = 1;
                }
                lgsProductSerialNoList.Add(lgsProductSerialNumber);

            }
            return lgsProductSerialNoList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvProductSerialNoTemp> GetPausedPurchaseSerialNoDetail(OpeningStockHeader openingStockHeader, bool isInv) 
        {
            List<InvProductSerialNoTemp> invProductSerialNoTempList = new List<InvProductSerialNoTemp>();

            List<LgsProductSerialNoDetail> lgsProductSerialNoDetail = new List<LgsProductSerialNoDetail>();
            List<InvProductSerialNoDetail> invProductSerialNoDetail = new List<InvProductSerialNoDetail>(); 

            if (isInv)
            {
                invProductSerialNoDetail = context.InvProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && ps.ReferenceDocumentDocumentID.Equals(openingStockHeader.DocumentID)).ToList();
                foreach (InvProductSerialNoDetail invProductSerialNoDetailTemp in invProductSerialNoDetail)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                    invProductSerialNoTemp.ExpiryDate = invProductSerialNoDetailTemp.ExpiryDate;
                    invProductSerialNoTemp.LineNo = invProductSerialNoDetailTemp.LineNo;
                    invProductSerialNoTemp.ProductID = invProductSerialNoDetailTemp.ProductID;
                    invProductSerialNoTemp.SerialNo = invProductSerialNoDetailTemp.SerialNo;
                    invProductSerialNoTemp.UnitOfMeasureID = invProductSerialNoDetailTemp.UnitOfMeasureID;
                    invProductSerialNoTemp.BatchNo = invProductSerialNoDetailTemp.BatchNo;

                    invProductSerialNoTempList.Add(invProductSerialNoTemp);

                }
                return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
            }
            else
            {
                lgsProductSerialNoDetail = context.LgsProductSerialNoDetails.Where(ps => ps.ReferenceDocumentID.Equals(openingStockHeader.OpeningStockHeaderID) && ps.ReferenceDocumentDocumentID.Equals(openingStockHeader.DocumentID)).ToList();
                foreach (LgsProductSerialNoDetail lgsProductSerialNoDetailTemp in lgsProductSerialNoDetail)
                {
                    InvProductSerialNoTemp invProductSerialNoTemp = new InvProductSerialNoTemp();

                    invProductSerialNoTemp.ExpiryDate = lgsProductSerialNoDetailTemp.ExpiryDate;
                    invProductSerialNoTemp.LineNo = lgsProductSerialNoDetailTemp.LineNo;
                    invProductSerialNoTemp.ProductID = lgsProductSerialNoDetailTemp.ProductID;
                    invProductSerialNoTemp.SerialNo = lgsProductSerialNoDetailTemp.SerialNo;
                    invProductSerialNoTemp.UnitOfMeasureID = lgsProductSerialNoDetailTemp.UnitOfMeasureID;
                    invProductSerialNoTemp.BatchNo = lgsProductSerialNoDetailTemp.BatchNo;

                    invProductSerialNoTempList.Add(invProductSerialNoTemp);

                }
                return invProductSerialNoTempList.OrderBy(pd => pd.ProductID).OrderBy(pd => pd.LineNo).ToList(); ;
            }
        }

        /// <summary>
        /// Get opening stock details for report
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        /// <param name="documentId"></param>
        /// <param name="transactionType" (1-->Inventry, 2-->Logistic)></param>
        /// <returns></returns>
        public DataTable GetOpeningStockTransactionDataTable(string documentNo, int documentStatus, int documentId, int transactionType)
        {
            string lookupType = ((int)LookUpReference.ModuleType).ToString();
            var query = transactionType.Equals(1) ?
                        (
                        from ph in context.OpeningStockHeaders
                        join pd in context.OpeningStockDetails on ph.OpeningStockHeaderID equals pd.OpeningStockHeaderID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join r in context.ReferenceTypes on ph.OpeningStockType equals r.LookupKey
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                            && r.LookupType.Equals(lookupType)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = ph.DocumentDate, // DbFunctions.TruncateTime(ph.DocumentDate.Date),
                                FieldString3 = ph.Narration,
                                FieldString4 = ph.Remark,
                                FieldString5 = "", // P Req. No
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = "",
                                FieldString9 = ph.ReferenceDocumentNo,
                                FieldString10 = r.LookupValue,
                                FieldString11 = ph.CreatedUser,
                                FieldString12 = l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = pd.BatchNo,
                                FieldString17 = pd.ExpiryDate, 
                                FieldString18 = pd.OrderQty,
                                FieldString19 = pd.SellingPrice,
                                FieldString20 = pd.CostPrice,
                                FieldString21 = pd.SellingValue,
                                FieldString22 = pd.CostValue,
                                FieldString23 = ph.TotalSellingtValue,
                                FieldString24 = ph.TotalCostValue,
                                FieldString25 = ph.TotalQty,//ph.DiscountAmount,
                                FieldString26 = "",//ph.TaxAmount,
                                FieldString27 = "",//ph.OtherCharges,
                                FieldString28 = "",//ph.NetAmount,

                            }) :
                            (
                        from ph in context.OpeningStockHeaders
                        join pd in context.OpeningStockDetails on ph.OpeningStockHeaderID equals pd.OpeningStockHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join r in context.ReferenceTypes on ph.OpeningStockType equals r.LookupKey
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                            && r.LookupType.Equals(lookupType)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = ph.DocumentDate, // DbFunctions.TruncateTime(ph.DocumentDate.Date),
                                FieldString3 = ph.Narration,
                                FieldString4 = ph.Remark,
                                FieldString5 = "", // P Req. No
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = "",
                                FieldString9 = ph.ReferenceDocumentNo,
                                FieldString10 = r.LookupValue,
                                FieldString11 = ph.CreatedUser,
                                FieldString12 = l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pm.ProductCode,
                                FieldString14 = pm.ProductName,
                                FieldString15 = um.UnitOfMeasureName,
                                FieldString16 = pd.BatchNo,
                                FieldString17 = pd.ExpiryDate,
                                FieldString18 = pd.OrderQty,
                                FieldString19 = pd.SellingPrice,
                                FieldString20 = pd.CostPrice,
                                FieldString21 = pd.SellingValue,
                                FieldString22 = pd.CostValue,
                                FieldString23 = ph.TotalSellingtValue,
                                FieldString24 = ph.TotalCostValue,
                                FieldString25 = ph.TotalQty,//ph.DiscountAmount,
                                FieldString26 = "",//ph.TaxAmount,
                                FieldString27 = "",//ph.OtherCharges,
                                FieldString28 = "",//ph.NetAmount,

                            });

            return query.AsEnumerable().ToDataTable();

        }

        /// <summary>
        /// Get opening stock type (1-->Inventry, 2-->Logistic)
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public int GetOpeningStockTransactionType(string documentNo, int documentStatus, int documentId)
        {
            return context.OpeningStockHeaders.Where(o => o.DocumentNo == documentNo && o.DocumentStatus == documentStatus && o.DocumentID == documentId).Select(o => o.OpeningStockType).FirstOrDefault();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmOpeningStock");
            
            IQueryable qryResult = null;

            if (string.Equals(autoGenerateInfo.FormName, "FrmOpeningStock"))
            {
                qryResult = context.OpeningStockHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, transAutoGenerateInfo.DocumentID);
                //var x = qryResult.Count();
                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "OpeningStockType":
                        qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", ((int)LookUpReference.ModuleType).ToString()), "OpeningStockType", "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("key.LookupValue").Select("key.LookupValue");

                        //qryResult = qryResult.Join(context.Employees, "SoldCashierID",
                        //                           reportDataStruct.DbColumnName.Trim(), "new(inner.EmployeeName)")
                        //                     .GroupBy("new(EmployeeName)", "new(EmployeeName)")
                        //                     .OrderBy("Key.EmployeeName").Select("Key.EmployeeName");
                        break;
                    default:
                        qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                        break;
                }
            }
            else if (string.Equals(autoGenerateInfo.FormName, "InvRptOpeningBalanceRegister"))
            {
                qryResult = null;
                qryResult = context.OpeningStockDetails.Where("DocumentStatus == @0 AND DocumentId==@1", 1, transAutoGenerateInfo.DocumentID);
                //var x = qryResult.Count();
                
                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "OpeningStockType":
                        qryResult = null;
                        qryResult = context.OpeningStockHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", ((int)LookUpReference.ModuleType).ToString()), reportDataStruct.DbColumnName.Trim(), "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("key.LookupValue").Select("key.LookupValue");
                        break;

                    case "InvProductMasterID":
                        //if (string.Equals(reportDataStruct.DbJoinColumnName.Trim(), "ProductCode"))
                        //{
                            qryResult = qryResult.Join(context.InvProductMasters, "ProductID",
                                                   reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        //}
                        //if (string.Equals(reportDataStruct.DbJoinColumnName.Trim(), "ProductName"))
                        //{
                        //    qryResult = qryResult.Join(context.InvProductMasters, "ProductID",
                        //                           reportDataStruct.DbColumnName.Trim(),
                        //                           "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                        //                              "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                        //                     .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                        //                     .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        //}
                        
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
            }
            else if (string.Equals(autoGenerateInfo.FormName, "LgsRptOpeningBalanceRegister"))
            {
                qryResult = null;
                qryResult = context.OpeningStockDetails.Where("DocumentStatus == @0 AND DocumentId==@1", 1, transAutoGenerateInfo.DocumentID);
                //var x = qryResult.Count();

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "OpeningStockType":
                        qryResult = null;
                        qryResult = context.OpeningStockHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, transAutoGenerateInfo.DocumentID);
                        qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", ((int)LookUpReference.ModuleType).ToString()), reportDataStruct.DbColumnName.Trim(), "LookupKey", "new(inner.LookupValue)")
                                    .GroupBy("new(LookupValue)", "new(LookupValue)")
                                    .OrderBy("key.LookupValue").Select("key.LookupValue");
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
            }


            //var y = qryResult.Count();
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

        public DataTable GetOpeningStockDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            
            var query = context.OpeningStockHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                        case "OpeningStockType":
                            query = (from qr in query
                                     join jt in context.ReferenceTypes.Where(
                                         "" +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " = " + "@0 OR " +
                                         reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                         " = @1",
                                         (reportConditionsDataStruct.ConditionFrom.Trim()),
                                         (reportConditionsDataStruct.ConditionTo.Trim())
                                         ) on qr.OpeningStockType equals jt.LookupKey
                                     select qr
                                        );
                            break;
                        default:
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
                            break;
                    }

                    
                }
            }


            //var queryResultData = (dynamic)null;
            DataTable dtResult = new DataTable();
            string refOpeningStockMode = ((int)LookUpReference.ModuleType).ToString();

            var queryResult = (from h in query
                               join r in context.ReferenceTypes on h.OpeningStockType equals r.LookupKey
                               where r.LookupType == refOpeningStockMode
                               select
                                   new
                                   {
                                       FieldString1 = h.DocumentNo,
                                       FieldString2 = DbFunctions.TruncateTime(h.DocumentDate),
                                       FieldString3 = r.LookupValue,
                                       FieldString4 = h.ReferenceDocumentNo,
                                       FieldString5 = h.Remark
                                   }).ToArray();

            dtResult = queryResult.ToDataTable();

            return dtResult;
        }

        public List<TransactionDet> GetPOSDocuments(int locationID, int transStatus)
        {

            List<TransactionDet> rtnList = new List<TransactionDet>();
            //var qry = context.TransactionDets.Where(ph => ph.LocationID.Equals(locationID) && ph.TransStatus.Equals(transStatus) && ph.Status.Equals(1) && ph.DataTransfer.Equals(1)).Select(ph => ph.Receipt).ToArray();
            var qry = (from td in context.TransactionDets
                       where td.LocationID == locationID && td.TransStatus == transStatus &&
                       td.Status == 1 && td.DataTransfer == 1
                       select new
                       {
                           td.Receipt,td.LocationID,td.UnitNo,td.ZNo
                       }).Distinct().OrderBy(dt => dt.Receipt).ToArray();
            foreach (var temp in qry)
            {
                TransactionDet transactionDetAdd = new TransactionDet();
                transactionDetAdd.Receipt = (temp.LocationID.ToString().Trim().Length == 1 ? "0" : string.Empty) + temp.LocationID.ToString().Trim() + (temp.UnitNo.ToString().Trim().Length == 1 ? "0" : string.Empty) + temp.UnitNo.ToString().Trim() + temp.ZNo.ToString().Trim();

                rtnList.Add(transactionDetAdd);
            }

            return rtnList;
        }

        public List<OpeningStockDetailTemp> GetPosDocumentDetails(string documentNo, int locationId)
        {

            OpeningStockDetailTemp openingStockDetailTemp;

            List<OpeningStockDetailTemp> openingStockDetailTempList = new List<OpeningStockDetailTemp>();

            // TransStatus = 3 = StockCount

            var oStockDetailTemp = (from td in context.TransactionDets
                                    join um in context.UnitOfMeasures on td.UnitOfMeasureID equals um.UnitOfMeasureID
                                    where
                                      td.TransStatus == 1 &&
                                      td.LocationID == locationId &&
                                      td.Status == 1 &&
                                      td.DataTransfer == 1 &&
                                      td.UnitNo == Common.ConvertStringToInt(documentNo.Trim().Substring(2,2)) &&
                                      td.ZNo == Common.ConvertStringToLong(documentNo.Trim().Substring(4, documentNo.Trim().Length-4))
                                    group new { td, um } by new
                                    {
                                        td.ExpiryDate,
                                        td.ProductCode,
                                        td.ProductID,
                                        td.Descrip,
                                        td.UnitOfMeasureID,
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
                                        g.Key.DocumentID == 1 ? g.Sum(p => p.td.Qty) :
                                        g.Key.DocumentID == 3 ? g.Sum(p => p.td.Qty) :
                                        g.Key.DocumentID == 2 ? g.Sum(p => -p.td.Qty) :
                                        g.Key.DocumentID == 4 ? g.Sum(p => -p.td.Qty) : 0,
                                        RowNo = (System.Int32?)g.Key.RowNo,
                                        LocationID = (System.Int32?)g.Key.LocationID
                                    }).ToArray();

            foreach (var tempProduct in oStockDetailTemp)
            {

                openingStockDetailTemp = new OpeningStockDetailTemp();

                //openingStockDetailTemp.AverageCost = tempProduct.AvgCost;
                openingStockDetailTemp.ConvertFactor = (decimal)tempProduct.ConvertFactor;
                openingStockDetailTemp.CostPrice = (decimal)tempProduct.Cost;
                openingStockDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                openingStockDetailTemp.LineNo = (long)tempProduct.RowNo;
                openingStockDetailTemp.LocationID = (int)tempProduct.LocationID;
                openingStockDetailTemp.CostValue = Common.ConvertStringToDecimalCurrency((tempProduct.Qty * tempProduct.Cost).ToString());
                openingStockDetailTemp.SellingValue = Common.ConvertStringToDecimalCurrency((tempProduct.Qty * tempProduct.Price).ToString());
                openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                openingStockDetailTemp.ProductID = (long)tempProduct.ProductID;
                openingStockDetailTemp.ProductName = tempProduct.Descrip;
                openingStockDetailTemp.OrderQty = (decimal)tempProduct.Qty;
                openingStockDetailTemp.SellingPrice = (decimal)tempProduct.Price;
                openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                openingStockDetailTemp.UnitOfMeasureID = (long)tempProduct.UnitOfMeasureID;
                openingStockDetailTemp.BatchNo = tempProduct.BatchNo;

                openingStockDetailTempList.Add(openingStockDetailTemp);


            }
            return openingStockDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public OpeningStockDetailTemp GetInvOpeningStockDetailTempForBarcodeScan(long barcode, int qty)  
        {
            OpeningStockDetailTemp openingStockDetailTemp = null;

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
                                              //bed.BatchNo,
                                              pm.IsBatch
                                          });

                foreach (var tempProduct in transferDetailTemp)
                {
                    openingStockDetailTemp = new OpeningStockDetailTemp();

                    openingStockDetailTemp.ProductID = tempProduct.ProductID;
                    openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                    openingStockDetailTemp.ProductName = tempProduct.ProductName;
                    openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                    openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    openingStockDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    //openingStockDetailTemp.BatchNo = tempProduct.BatchNo;
                    openingStockDetailTemp.OrderQty = qty;
                    //openingStockDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }

            return openingStockDetailTemp;
        }

        public OpeningStockDetailTemp GetLgsOpeningStockDetailTempForBarcodeScan(long barcode, int qty) 
        {
            OpeningStockDetailTemp openingStockDetailTemp = null;

            LgsBatchNoExpiaryDetailService batchNoExpiaryDetailService = new LgsBatchNoExpiaryDetailService();
            LgsProductBatchNoExpiaryDetail lgsProductBatchNoExpiaryDetail = new LgsProductBatchNoExpiaryDetail();

            lgsProductBatchNoExpiaryDetail = batchNoExpiaryDetailService.GetBatchNoExpiaryDetailByBarcode(barcode);

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
                                              //bed.BatchNo,
                                              pm.IsBatch
                                          });

                foreach (var tempProduct in transferDetailTemp)
                {
                    openingStockDetailTemp = new OpeningStockDetailTemp();

                    openingStockDetailTemp.ProductID = tempProduct.ProductID;
                    openingStockDetailTemp.ProductCode = tempProduct.ProductCode;
                    openingStockDetailTemp.ProductName = tempProduct.ProductName;
                    openingStockDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    openingStockDetailTemp.CostPrice = tempProduct.CostPrice;
                    openingStockDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    openingStockDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    openingStockDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    openingStockDetailTemp.ExpiryDate = tempProduct.ExpiryDate;
                    //openingStockDetailTemp.BatchNo = tempProduct.BatchNo;
                    openingStockDetailTemp.OrderQty = qty;
                    //openingStockDetailTemp.IsBatch = tempProduct.IsBatch;
                }
            }

            return openingStockDetailTemp;
        }

        public List<OpeningStockDetailTemp> GetUpdateInvOpeningStockDetailTempForBarcodeScan(List<OpeningStockDetailTemp> openingStockDetail, OpeningStockDetailTemp OpStockDetail)
        {
            OpeningStockDetailTemp openingStockDetailTemp = new OpeningStockDetailTemp();

            //openingStockDetailTemp = openingStockDetail.Where(p => p.ProductID == OpStockDetail.ProductID && p.UnitOfMeasureID == OpStockDetail.UnitOfMeasureID && p.BatchNo == OpStockDetail.BatchNo).FirstOrDefault();
            openingStockDetailTemp = openingStockDetail.Where(p => p.ProductID == OpStockDetail.ProductID && p.UnitOfMeasureID == OpStockDetail.UnitOfMeasureID).FirstOrDefault();

            if (openingStockDetailTemp == null || openingStockDetailTemp.LineNo.Equals(0))
            {
                if (openingStockDetail.Count.Equals(0))
                    OpStockDetail.LineNo = 1;
                else
                    OpStockDetail.LineNo = openingStockDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                openingStockDetail.Remove(openingStockDetailTemp);
                OpStockDetail.LineNo = openingStockDetailTemp.LineNo;
            }

            openingStockDetail.Add(OpStockDetail);

            return openingStockDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<OpeningStockDetailTemp> GetUpdateLgsOpeningStockDetailTempForBarcodeScan(List<OpeningStockDetailTemp> openingStockDetail, OpeningStockDetailTemp OpStockDetail)
        {
            OpeningStockDetailTemp openingStockDetailTemp = new OpeningStockDetailTemp(); 

            //openingStockDetailTemp = openingStockDetail.Where(p => p.ProductID == OpStockDetail.ProductID && p.UnitOfMeasureID == OpStockDetail.UnitOfMeasureID && p.BatchNo == OpStockDetail.BatchNo).FirstOrDefault();
            openingStockDetailTemp = openingStockDetail.Where(p => p.ProductID == OpStockDetail.ProductID && p.UnitOfMeasureID == OpStockDetail.UnitOfMeasureID).FirstOrDefault();

            if (openingStockDetailTemp == null || openingStockDetailTemp.LineNo.Equals(0))
            {
                if (openingStockDetail.Count.Equals(0))
                    OpStockDetail.LineNo = 1;
                else
                    OpStockDetail.LineNo = openingStockDetail.Max(s => s.LineNo) + 1;
            }
            else
            {
                openingStockDetail.Remove(openingStockDetailTemp);
                OpStockDetail.LineNo = openingStockDetailTemp.LineNo;
            }

            openingStockDetail.Add(OpStockDetail);

            return openingStockDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public DataTable GetOpeningBalancesRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo transAutoGenerateInfo = new AutoGenerateInfo();
            transAutoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmOpeningStock");

            var query = context.OpeningStockDetails.AsNoTracking().Where("DocumentStatus= @0 AND DocumentId = @1", 1, transAutoGenerateInfo.DocumentID);

            LookUpReferenceService lookUpReferenceTitleTypeService = new LookUpReferenceService();
            string moduleType = ((int)LookUpReference.ModuleType).ToString();
            int openingStockType; 

            // OpeningStockType - Inventory
            if (string.Equals(autoGenerateInfo.FormName.Trim(), "InvRptOpeningBalanceRegister"))
            {
                openingStockType = lookUpReferenceTitleTypeService.GetLookUpReferenceByValue(moduleType, "Inventory").LookupKey;
                query = (from qr in query
                         join h in context.OpeningStockHeaders on qr.OpeningStockHeaderID equals h.OpeningStockHeaderID
                         where h.OpeningStockType == openingStockType
                         select qr
                            );
            }

            // OpeningStockType - Logistic
            if (string.Equals(autoGenerateInfo.FormName.Trim(), "LgsRptOpeningBalanceRegister"))
            {
                openingStockType = lookUpReferenceTitleTypeService.GetLookUpReferenceByValue(moduleType, "Logistic").LookupKey;
                query = (from qr in query
                         join h in context.OpeningStockHeaders on qr.OpeningStockHeaderID equals h.OpeningStockHeaderID
                         where h.OpeningStockType == openingStockType
                         select qr
                            );
            }

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
                    //query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));
                    switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                    {
                        //case "OpeningStockType":
                        //    string moduleType2 = ((int)LookUpReference.ModuleType).ToString();

                        //    if (string.Equals(autoGenerateInfo.FormName.Trim(), "InvRptOpeningBalanceRegister"))
                        //    {
                        //        int openingStockType = lookUpReferenceTitleTypeService.GetLookUpReferenceByValue(moduleType, "Inventory").LookupKey;
                        //        query = (from qr in query
                        //                 join h in context.OpeningStockHeaders on qr.OpeningStockHeaderID equals h.OpeningStockHeaderID
                        //                 //join jt in context.ReferenceTypes.Where(
                        //                 //    "" +
                        //                 //    reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                        //                 //    " = " + "@0 OR " +
                        //                 //    reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                        //                 //    " = @1",
                        //                 //    (reportConditionsDataStruct.ConditionFrom.Trim()),
                        //                 //    (reportConditionsDataStruct.ConditionTo.Trim())
                        //                 //    ) on h.OpeningStockType equals jt.LookupKey
                        //                 where h.OpeningStockType == openingStockType
                        //                 select qr
                        //                    );
                        //    }
                        //    if (string.Equals(autoGenerateInfo.FormName.Trim(), "LgsRptOpeningBalanceRegister"))
                        //    {
                        //        int openingStockType = lookUpReferenceTitleTypeService.GetLookUpReferenceByValue(moduleType, "Logistic").LookupKey;
                        //        query = (from qr in query
                        //                 join h in context.OpeningStockHeaders on qr.OpeningStockHeaderID equals h.OpeningStockHeaderID
                        //                 where h.OpeningStockType == openingStockType
                        //                 select qr
                        //                    );
                        //    }
                        //    break;
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
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                            break;
                    }
                }

                //var r = query.Count();
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { 
                    //query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        if (string.Equals(autoGenerateInfo.FormName, "InvRptOpeningBalanceRegister"))
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

                        if (string.Equals(autoGenerateInfo.FormName, "LgsRptOpeningBalanceRegister"))
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

            #region dynamic select

            //DataTable dt1 = new DataTable();
            //StringBuilder selectFields = new StringBuilder();
            ////// Set fields to be selected
            //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            //{ 
            //    selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            //    dt1.Columns.Add(reportDataStruct.DbColumnName.Trim());
            //}

            //// Remove last two charators (", ")
            //selectFields.Remove(selectFields.Length - 2, 2);
            //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
            //var selectedqry2 = query.Select("(new " + selectFields.ToString() + ")");

            //foreach (var item in selectedqry)
            //{
            //    string[] columnList = item.ToString().Split(',');
            //    dt1.Rows.Add(columnList);
            //}
            #endregion

            //var cc = query.Count();
            var queryResult = query.AsEnumerable();
            DataTable dtResult = new DataTable();
            string refOpeningStockMode = ((int)LookUpReference.ModuleType).ToString();
            if (string.Equals(autoGenerateInfo.FormName.Trim(), "InvRptOpeningBalanceRegister"))
            {

                dtResult = (from pd in queryResult
                        join ph in context.OpeningStockHeaders on pd.OpeningStockHeaderID equals ph.OpeningStockHeaderID
                        join r in context.ReferenceTypes on ph.OpeningStockType equals r.LookupKey
                        join l in context.Locations on pd.LocationID equals l.LocationID
                        join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                        join dp in context.InvDepartments on pm.DepartmentID equals dp.InvDepartmentID
                        join ct in context.InvCategories on pm.CategoryID equals ct.InvCategoryID
                        join sct in context.InvSubCategories on pm.SubCategoryID equals sct.InvSubCategoryID
                        join sct2 in context.InvSubCategories2 on pm.SubCategory2ID equals sct2.InvSubCategory2ID
                        where r.LookupType == refOpeningStockMode
                        select
                            new
                            {
                                FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                                FieldString2 = reportDataStructList[1].IsSelectionField == true ? dp.DepartmentCode + " " + dp.DepartmentName : "",
                                FieldString3 = reportDataStructList[2].IsSelectionField == true ? ct.CategoryCode + " " + ct.CategoryName : "",
                                FieldString4 = reportDataStructList[3].IsSelectionField == true ? sct.SubCategoryCode + " " + sct.SubCategoryName : "",
                                FieldString5 = reportDataStructList[4].IsSelectionField == true ? sct2.SubCategory2Code + " " + sct2.SubCategory2Name : "",
                                FieldString6 = reportDataStructList[5].IsSelectionField == true ? pm.ProductCode : "",
                                FieldString7 = reportDataStructList[6].IsSelectionField == true ? pm.ProductName : "",
                                FieldString8 = reportDataStructList[7].IsSelectionField == true ? r.LookupValue : "",
                                FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.CreatedUser : "",
                                FieldString10 = reportDataStructList[9].IsSelectionField == true ? ph.DocumentNo : "",
                                FieldString11 = reportDataStructList[10].IsSelectionField == true ? ph.DocumentDate.ToShortDateString() : "",
                                FieldDecimal1 = reportDataStructList[11].IsSelectionField == true ? pd.OrderQty : 0,
                                FieldDecimal2 = reportDataStructList[12].IsSelectionField == true ? pd.SellingValue : 0
                            }).ToArray().ToDataTable();

            }
            else if (string.Equals(autoGenerateInfo.FormName.Trim(), "LgsRptOpeningBalanceRegister"))
            {

                dtResult = (from pd in queryResult
                        join ph in context.OpeningStockHeaders on pd.OpeningStockHeaderID equals ph.OpeningStockHeaderID
                        join r in context.ReferenceTypes on ph.OpeningStockType equals r.LookupKey
                        join l in context.Locations on pd.LocationID equals l.LocationID
                            join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                            join dp in context.LgsDepartments on pm.DepartmentID equals dp.LgsDepartmentID
                            join ct in context.LgsCategories on pm.CategoryID equals ct.LgsCategoryID
                            join sct in context.LgsSubCategories on pm.SubCategoryID equals sct.LgsSubCategoryID
                            join sct2 in context.LgsSubCategories2 on pm.SubCategory2ID equals sct2.LgsSubCategory2ID
                        where r.LookupType == refOpeningStockMode
                        select
                            new
                            {
                                FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode + " " + l.LocationName : "",
                                FieldString2 = reportDataStructList[1].IsSelectionField == true ? dp.DepartmentCode + " " + dp.DepartmentName : "",
                                FieldString3 = reportDataStructList[2].IsSelectionField == true ? ct.CategoryCode + " " + ct.CategoryName : "",
                                FieldString4 = reportDataStructList[3].IsSelectionField == true ? sct.SubCategoryCode + " " + sct.SubCategoryName : "",
                                FieldString5 = reportDataStructList[4].IsSelectionField == true ? sct2.SubCategory2Code + " " + sct2.SubCategory2Name : "",
                                FieldString6 = reportDataStructList[5].IsSelectionField == true ? pm.ProductCode : "",
                                FieldString7 = reportDataStructList[6].IsSelectionField == true ? pm.ProductName : "",
                                FieldString8 = reportDataStructList[7].IsSelectionField == true ? r.LookupValue : "",
                                FieldString9 = reportDataStructList[8].IsSelectionField == true ? ph.CreatedUser : "",
                                FieldString10 = reportDataStructList[9].IsSelectionField == true ? ph.DocumentNo : "",
                                FieldString11 = reportDataStructList[10].IsSelectionField == true ? ph.DocumentDate.ToShortDateString() : "",
                                FieldDecimal1 = reportDataStructList[11].IsSelectionField == true ? pd.OrderQty : 0,
                                FieldDecimal2 = reportDataStructList[12].IsSelectionField == true ? pd.SellingValue : 0
                            }).ToArray().ToDataTable();

            }

            return dtResult;
        }
        
        #endregion
    }
}
