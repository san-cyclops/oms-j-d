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
using System.Data.Entity.Core.Objects;


namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class LgsMaterialRequestService
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

            Location location = new Location();
            LocationService locationService = new LocationService();
            location = locationService.GetLocationsByID(locationID);

            if (location != null)
            {
                if (!string.IsNullOrEmpty(location.LocationPrefixCode))
                {
                    preFix = preFix + location.LocationPrefixCode;
                }
            }

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
        /// <param name="lgsMaterialRequestDetailTemp"></param>
        /// <returns></returns>
        public List<LgsMaterialRequestDetailTemp> GetLgsMaterialRequestDetailTempList(List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTemp, LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp)
        {
           if (lgsMaterialRequestDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialRequestDetailTemp.ProductCode && pr.UnitOfMeasure == lgsMaterialRequestDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                // Update Existing Record
                lgsMaterialRequestDetailTemp.LineNo = lgsMaterialRequestDetailsTemp.Where(pr => pr.ProductCode == lgsMaterialRequestDetailTemp.ProductCode).FirstOrDefault().LineNo;
                lgsMaterialRequestDetailsTemp.RemoveAll(pr => pr.ProductCode == lgsMaterialRequestDetailTemp.ProductCode);
                lgsMaterialRequestDetailsTemp.Add(lgsMaterialRequestDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(lgsMaterialRequestDetailsTemp.Count, 0))
                { lgsMaterialRequestDetailTemp.LineNo = 1; }
                else
                { lgsMaterialRequestDetailTemp.LineNo = lgsMaterialRequestDetailsTemp.Max(ln => ln.LineNo) + 1; }
                lgsMaterialRequestDetailsTemp.Add(lgsMaterialRequestDetailTemp);
            }
            return lgsMaterialRequestDetailsTemp;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<LgsMaterialRequestDetailTemp> DeleteProductLgsMaterialRequestDetailTemp(List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTemp, string productCode, long unitOfMeasureID)
        {
            // List for grid view.
            lgsMaterialRequestDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode && pr.UnitOfMeasureID == unitOfMeasureID);
            return lgsMaterialRequestDetailsTemp;
        }

        /// <summary>
        /// Get LgsMaterialRequestHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaterialRequestHeader GetLgsMaterialRequestHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            //return context.LgsMaterialRequestHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            return context.LgsMaterialRequestHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }

        public LgsMaterialRequestHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsMaterialRequestHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public bool IsValidNoOfQty(decimal qty, long productID, int unitOfMeasureID, long requtationHeaderID)
        {
            decimal poQty = 0;
            var getCurrentQty = (from a in context.LgsMaterialRequestDetails
                                 where a.ProductID.Equals(productID) && a.LgsMaterialRequestHeaderID.Equals(requtationHeaderID) && a.UnitOfMeasureID.Equals(unitOfMeasureID)
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

        /// <summary>
        /// Save Material Request note
        /// </summary>
        /// <param name="lgsMaterialRequestHeader"></param>
        /// <param name="lgsMaterialRequestDetailsTemp"></param>
        /// <returns></returns>
        public bool SaveMaterialRequestNote(LgsMaterialRequestHeader lgsMaterialRequestHeader, List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTemp)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (lgsMaterialRequestHeader.LgsMaterialRequestHeaderID.Equals(0))
                {
                    context.LgsMaterialRequestHeaders.Add(AddLgsMaterialRequestDetails(lgsMaterialRequestHeader, lgsMaterialRequestDetailsTemp));
                    this.context.SaveChanges();                    
                    lgsMaterialRequestHeader.MaterialRequestHeaderID = lgsMaterialRequestHeader.LgsMaterialRequestHeaderID;
                    this.context.Entry(lgsMaterialRequestHeader).State = EntityState.Modified;
                    this.context.SaveChanges();

                    foreach (var lgsMaterialRequestDetail in lgsMaterialRequestHeader.LgsMaterialRequestDetails)
                    {
                        lgsMaterialRequestDetail.MaterialRequestDetailID = lgsMaterialRequestDetail.LgsMaterialRequestDetailID;
                    }
                    this.context.Entry(lgsMaterialRequestHeader).State = EntityState.Modified;
                    this.context.SaveChanges();
                }
                else
                {
                    // Delete existing records
                    var deleteList = context.LgsMaterialRequestDetails.Where(d => d.LgsMaterialRequestHeaderID == lgsMaterialRequestHeader.LgsMaterialRequestHeaderID);
                    foreach (var lgsMaterialRequestDetail in deleteList)
                    {
                        context.LgsMaterialRequestDetails.Remove(lgsMaterialRequestDetail);
                    }

                    lgsMaterialRequestHeader.LgsMaterialRequestDetails.Clear();
                    lgsMaterialRequestHeader = AddLgsMaterialRequestDetails(lgsMaterialRequestHeader, lgsMaterialRequestDetailsTemp);
                    this.context.Entry(lgsMaterialRequestHeader).State = EntityState.Modified;
                    this.context.SaveChanges();
                }
                
                
                transactionScope.Complete();
                return true;
            }
        }

        



        /// <summary>
        /// Create LgsMaterialRequestDetail list using LgsMaterialRequestDetailsTemp
        /// </summary>
        /// <param name="lgsMaterialRequestHeader"></param>
        /// <param name="lgsMaterialRequestDetailsTemp"></param>
        /// <returns></returns>
        private LgsMaterialRequestHeader AddLgsMaterialRequestDetails(LgsMaterialRequestHeader lgsMaterialRequestHeader, List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTemp)
        {
            
            // LgsMaterialRequestDetail list for LgsMaterialRequestHeader 
            lgsMaterialRequestHeader.LgsMaterialRequestDetails = new List<LgsMaterialRequestDetail>();
            foreach (LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp in lgsMaterialRequestDetailsTemp)
            {
                LgsMaterialRequestDetail lgsMaterialRequestDetail = new LgsMaterialRequestDetail();

                lgsMaterialRequestDetail.DocumentStatus = lgsMaterialRequestHeader.DocumentStatus;
                lgsMaterialRequestDetail.DocumentNo = lgsMaterialRequestHeader.DocumentNo;
                lgsMaterialRequestDetail.DocumentDate = lgsMaterialRequestHeader.DocumentDate;
                lgsMaterialRequestDetail.CompanyID = lgsMaterialRequestHeader.CompanyID;
                lgsMaterialRequestDetail.LocationID = lgsMaterialRequestHeader.LocationID;
                lgsMaterialRequestDetail.CostCentreID = lgsMaterialRequestHeader.CostCentreID;
                lgsMaterialRequestDetail.CreatedDate = lgsMaterialRequestHeader.CreatedDate;
                lgsMaterialRequestDetail.DocumentID = lgsMaterialRequestHeader.DocumentID;
                lgsMaterialRequestDetail.DataTransfer = lgsMaterialRequestHeader.DataTransfer;
                lgsMaterialRequestDetail.ModifiedDate = lgsMaterialRequestHeader.ModifiedDate;

                //lgsMaterialRequestDetail.LgsMaterialRequestDetailID = lgsMaterialRequestDetailTemp.LgsMaterialRequestDetailTempID;
                lgsMaterialRequestDetail.LineNo = lgsMaterialRequestDetailTemp.LineNo;
                lgsMaterialRequestDetail.ProductID = lgsMaterialRequestDetailTemp.ProductID;
                lgsMaterialRequestDetail.ProductCode = lgsMaterialRequestDetailTemp.ProductCode;
                lgsMaterialRequestDetail.UnitOfMeasureID = lgsMaterialRequestDetailTemp.UnitOfMeasureID;
                lgsMaterialRequestDetail.BaseUnitID = lgsMaterialRequestDetailTemp.BaseUnitID;
                lgsMaterialRequestDetail.UnitOfMeasure = lgsMaterialRequestDetailTemp.UnitOfMeasure;
                lgsMaterialRequestDetail.ConvertFactor = lgsMaterialRequestDetailTemp.ConvertFactor;
                lgsMaterialRequestDetail.OrderQty = lgsMaterialRequestDetailTemp.OrderQty;
                lgsMaterialRequestDetail.BalanceQty = lgsMaterialRequestDetailTemp.OrderQty;

                lgsMaterialRequestHeader.LgsMaterialRequestDetails.Add(lgsMaterialRequestDetail);
            }

            return lgsMaterialRequestHeader;
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        public LgsMaterialRequestHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsMaterialRequestHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }
        

        /// <summary>
        /// Create LgsMaterialRequestDetail list using LgsMaterialRequestDetailTemp
        /// </summary>
        /// <param name="lgsMaterialRequestHeader"></param>
        /// <returns></returns>
        public List<LgsMaterialRequestDetailTemp> GetLgsMaterialRequestDetails(LgsMaterialRequestHeader lgsMaterialRequestHeader)
        {
            // LgsMaterialRequestDetail list for LgsMaterialRequestHeader 
            List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailsTemp = new List<LgsMaterialRequestDetailTemp>();
          
            var tempProductList = (from d in context.LgsMaterialRequestDetails
                                  join p in context.LgsProductMasters on d.ProductID equals p.LgsProductMasterID
                                  where d.DocumentStatus.Equals(0) && d.LgsMaterialRequestHeaderID.Equals(lgsMaterialRequestHeader.LgsMaterialRequestHeaderID)
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
                                        d.OrderQty                                    
                                    }).ToArray();

            foreach (var tempProduct in tempProductList)
            {
                LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();

                lgsMaterialRequestDetailTemp.LgsMaterialRequestDetailTempID = tempProduct.LgsMaterialRequestDetailID;
                lgsMaterialRequestDetailTemp.LineNo = tempProduct.LineNo;
                lgsMaterialRequestDetailTemp.ProductID = tempProduct.ProductID;
                lgsMaterialRequestDetailTemp.ProductCode = tempProduct.ProductCode;
                lgsMaterialRequestDetailTemp.ProductName = tempProduct.ProductName;
                lgsMaterialRequestDetailTemp.BaseUnitID = tempProduct.BaseUnitID;
                lgsMaterialRequestDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                lgsMaterialRequestDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasure;
                lgsMaterialRequestDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                lgsMaterialRequestDetailTemp.OrderQty = tempProduct.OrderQty;                

                lgsMaterialRequestDetailsTemp.Add(lgsMaterialRequestDetailTemp);
            }            

            return lgsMaterialRequestDetailsTemp;
        }


        /// <summary>
        /// Get pending Material requsition list 
        /// </summary>
        /// <returns></returns>
        public string[] GetPendingMaterialRequsitionList(int locationID)
        {
            //List<string> pendingMaterialRequsitionList = context.LgsMaterialRequestHeaders.Where(j => j.DocumentStatus.Equals(1) && j.ReferenceDocumentDocumentID.Equals(0)).Select(j => j.DocumentNo).ToList();
            //return pendingMaterialRequsitionList.ToArray();        
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.LgsMaterialRequestHeaders
                       join pd in context.LgsMaterialRequestDetails on ph.LgsMaterialRequestHeaderID equals pd.LgsMaterialRequestHeaderID
                       //where ph.LocationID == locationID && 
                       where pd.BalanceQty != 0
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
        /// Get Material Request details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialRequestTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = context.LgsMaterialRequestHeaders.Where("DocumentNo==@0 AND DocumentStatus == @1 AND DocumentId == @2", documentNo, documentStatus, documentId);    
            var queryResult = query.AsEnumerable();
 
            return (
                        //from ph in context.LgsMaterialRequestHeaders
                        from ph in queryResult
                        join pd in context.LgsMaterialRequestDetails on ph.LgsMaterialRequestHeaderID equals pd.LgsMaterialRequestHeaderID
                        join pm in context.LgsProductMasters on pd.ProductID equals pm.LgsProductMasterID
                        join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
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
                                FieldString21 = "",
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

            //return query.AsEnumerable().ToDataTable();

        }


        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsMaterialRequestHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        public LgsMaterialRequestHeader GetPausedLgsMaterialRequestHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaterialRequestHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }


        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.LgsMaterialRequestHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

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


        public DataTable GetMaterialRequestDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {

            var query = context.LgsMaterialRequestHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

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
                            FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationName : "",
                            FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.CreatedUser : "",
                            FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                            FieldString4 = reportDataStructList[3].IsSelectionField == true ? h.DocumentNo : "", 
                            FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.DiliveryDate.ToShortDateString() : "",
                            FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.ReferenceDocumentNo : "",
                            FieldDecimal1 = reportDataStructList[6].IsSelectionField == true ? (from t in context.LgsMaterialRequestDetails
                                                                                                where
                                                                                                  t.LgsMaterialRequestHeaderID == h.LgsMaterialRequestHeaderID
                                                                                                select new
                                                                                                {
                                                                                                    Column1 = ((System.Decimal?)t.OrderQty ?? (System.Decimal?)0)
                                                                                                }).Sum(p => p.Column1) : 0, // OrderQty
                        }).ToArray().ToDataTable();
        }


        public LgsMaterialRequestDetailTemp GetLgsMaterialRequestDetailTemp(List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailTempPrm, LgsProductMaster lgsProductMaster, int locationID, long unitOfMeasureID)
        {
            LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();
            decimal defaultConvertFactor = 1;
            var purchaseOrderDetailTemp = (from pm in context.LgsProductMasters
                                           //join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID)
                                           //&& psm.LocationID.Equals(locationID)
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
                                           //join psm in context.LgsProductStockMasters on pm.LgsProductMasterID equals psm.ProductID
                                           join uc in context.UnitOfMeasures on pm.UnitOfMeasureID equals uc.UnitOfMeasureID
                                           join puc in context.LgsProductUnitConversions on pm.LgsProductMasterID equals puc.ProductID
                                           where pm.IsDelete == false && pm.LgsProductMasterID.Equals(lgsProductMaster.LgsProductMasterID)
                                           //&& psm.LocationID.Equals(locationID) 
                                           && puc.UnitOfMeasureID.Equals(unitOfMeasureID)
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
                    lgsMaterialRequestDetailTemp = lgsMaterialRequestDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && lgsProductMaster.IsExpiry.Equals(true))
                    lgsMaterialRequestDetailTemp = lgsMaterialRequestDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    lgsMaterialRequestDetailTemp = lgsMaterialRequestDetailTempPrm.Where(p => p.ProductID == lgsProductMaster.LgsProductMasterID).FirstOrDefault();

                if (lgsMaterialRequestDetailTemp == null)
                {
                    lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();
                    lgsMaterialRequestDetailTemp.ProductID = tempProduct.LgsProductMasterID;
                    lgsMaterialRequestDetailTemp.ProductCode = tempProduct.ProductCode;
                    lgsMaterialRequestDetailTemp.ProductName = tempProduct.ProductName;
                    lgsMaterialRequestDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    lgsMaterialRequestDetailTemp.CostPrice = tempProduct.CostPrice;
                    //lgsMaterialRequestDetailTemp.PackSize = tempProduct.PackSize;
                    //lgsMaterialRequestDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    //lgsMaterialRequestDetailTemp.AverageCost = tempProduct.AverageCost;
                    lgsMaterialRequestDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    lgsMaterialRequestDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                    //lgsMaterialRequestDetailTemp.IsBatch = tempProduct.IsBatch;
                }

            }
            return lgsMaterialRequestDetailTemp;
        }


        public List<LgsMaterialRequestDetailTemp> GetUpdateLgsMaterialRequestDetailTemp(List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailTempListPrm, LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTempPrm, LgsProductMaster lgsProductMaster)
        {
            LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();

            lgsMaterialRequestDetailTemp = lgsMaterialRequestDetailTempListPrm.Where(p => p.ProductID == lgsMaterialRequestDetailTempPrm.ProductID && p.UnitOfMeasureID == lgsMaterialRequestDetailTempPrm.UnitOfMeasureID).FirstOrDefault();

            if (lgsMaterialRequestDetailTemp == null || lgsMaterialRequestDetailTemp.LineNo.Equals(0))
            {
                if (lgsMaterialRequestDetailTempListPrm.Count.Equals(0))
                    lgsMaterialRequestDetailTempPrm.LineNo = 1;
                else
                    lgsMaterialRequestDetailTempPrm.LineNo = lgsMaterialRequestDetailTempListPrm.Max(s => s.LineNo) + 1;
            }
            else
            {
                lgsMaterialRequestDetailTempListPrm.Remove(lgsMaterialRequestDetailTemp);
                lgsMaterialRequestDetailTempPrm.LineNo = lgsMaterialRequestDetailTemp.LineNo;
            }

            lgsMaterialRequestDetailTempListPrm.Add(lgsMaterialRequestDetailTempPrm);

            return lgsMaterialRequestDetailTempListPrm.OrderBy(pd => pd.LineNo).ToList();
        }


        public List<LgsMaterialRequestDetailTemp> GetDeleteLgsMaterialRequestDetailTemp(List<LgsMaterialRequestDetailTemp> lgsMaterialRequestDetailTempListprm, LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTempPrm)
        {
            LgsMaterialRequestDetailTemp lgsMaterialRequestDetailTemp = new LgsMaterialRequestDetailTemp();

            lgsMaterialRequestDetailTemp = lgsMaterialRequestDetailTempListprm.Where(p => p.ProductID == lgsMaterialRequestDetailTempPrm.ProductID && p.UnitOfMeasureID == lgsMaterialRequestDetailTempPrm.UnitOfMeasureID).FirstOrDefault();

            long removedLineNo = 0;
            if (lgsMaterialRequestDetailTemp != null)
            {
                lgsMaterialRequestDetailTempListprm.Remove(lgsMaterialRequestDetailTemp);
                removedLineNo = lgsMaterialRequestDetailTemp.LineNo;

            }
            lgsMaterialRequestDetailTempListprm.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);


            return lgsMaterialRequestDetailTempListprm.OrderBy(pd => pd.LineNo).ToList();
        }


        public DataTable GetPendingMaterialRequsitionDocuments() 
        {
            var sql = (from mrh in context.LgsMaterialRequestHeaders
                       join mrd in context.LgsMaterialRequestDetails on mrh.LgsMaterialRequestHeaderID equals mrd.LgsMaterialRequestHeaderID
                       join l in context.Locations on mrh.LocationID equals l.LocationID
                       join pm in context.LgsProductMasters on mrd.ProductID equals pm.LgsProductMasterID
                       where mrd.BalanceQty != 0
                       select new
                       {
                           mrh.DocumentNo,
                           mrh.DocumentDate,
                           RequestedLocation = l.LocationName,
                           pm.ProductName,
                           RequestedQty = mrd.OrderQty,
                           AllocatedQty = mrd.OrderQty - mrd.BalanceQty, 
                           mrd.BalanceQty
                       }).ToArray();

            return sql.ToDataTable();
        }


        public List<LgsMaterialRequestHeader> GetMaterialRequestionsInbox()
        {
            List<LgsMaterialRequestHeader> rtnList = new List<LgsMaterialRequestHeader>();

            return rtnList = (from mh in context.LgsMaterialRequestHeaders.AsEnumerable()
                              join l in context.Locations on mh.LocationID equals l.LocationID
                              where mh.Viewed == false
                              orderby mh.DocumentDate descending
                              select new LgsMaterialRequestHeader
                              {
                                  DocumentNo = mh.DocumentNo,
                                  LocationName = l.LocationName,
                                  DocumentDate = mh.DocumentDate,
                                  RequestedBy = mh.RequestedBy,
                                  DocumentDateShort = mh.DocumentDate.ToShortDateString(),
                              }).ToList();
        }

        public int RequestInboxCount()
        {
            int count;
            return count = context.LgsMaterialRequestHeaders.Where(mh => mh.Viewed == false).Count();
        }

        public List<LgsMaterialRequestHeader> GetViewedMaterialRequestions()
        {
            List<LgsMaterialRequestHeader> rtnList = new List<LgsMaterialRequestHeader>();

            var qry = (from md in context.LgsMaterialRequestDetails
                       join l in context.Locations on md.LocationID equals l.LocationID
                       where
                         md.LgsMaterialRequestHeader.Viewed == true &&
                         md.OrderQty == md.BalanceQty
                       group new { md.LgsMaterialRequestHeader, l } by new
                       {
                           md.LgsMaterialRequestHeader.DocumentNo,
                           l.LocationName,
                           DocumentDate = md.LgsMaterialRequestHeader.DocumentDate,
                           md.LgsMaterialRequestHeader.RequestedBy
                       } into g
                       select new
                       {
                           g.Key.DocumentNo,
                           g.Key.LocationName,
                           DocumentDateShort = g.Key.DocumentDate,
                           g.Key.RequestedBy
                       }).ToArray().OrderByDescending(p => p.DocumentDateShort);

            foreach (var temp in qry)
            {
                LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
                lgsMaterialRequestHeader.DocumentNo = temp.DocumentNo;
                lgsMaterialRequestHeader.LocationName = temp.LocationName;
                lgsMaterialRequestHeader.DocumentDateShort = temp.DocumentDateShort.ToShortDateString();
                lgsMaterialRequestHeader.RequestedBy = temp.RequestedBy;

                rtnList.Add(lgsMaterialRequestHeader);
            }

            return rtnList;
        }

        public int ViewedRequestCount() 
        {
            int count = 0;

            return count = (from md in context.LgsMaterialRequestDetails
                            where
                              md.LgsMaterialRequestHeader.Viewed == true &&
                              md.OrderQty == md.BalanceQty
                            group md.LgsMaterialRequestHeader by new
                            {
                                md.LgsMaterialRequestHeader.DocumentNo
                            } into g
                            select new
                            {
                                g.Key.DocumentNo
                            }).ToArray().Count();
        }

        public void UpdateViewedMrnDocuments(string documentNo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.LgsMaterialRequestHeaders.Update(mr => mr.DocumentNo == documentNo, mr => new LgsMaterialRequestHeader { Viewed = true });
            }
        }

        public List<LgsMaterialRequestHeader> GetMaterialRequestionsOutbox() 
        {
            List<LgsMaterialRequestHeader> rtnList = new List<LgsMaterialRequestHeader>();

            var qry = (from md in context.LgsMaterialRequestDetails
                       join l in context.Locations on md.LgsMaterialRequestHeader.LocationID equals l.LocationID
                       where md.BalanceQty == 0
                       group new { md.LgsMaterialRequestHeader, l } by new
                       {
                           md.LgsMaterialRequestHeader.DocumentNo,
                           l.LocationName,
                           DocumentDate = md.LgsMaterialRequestHeader.DocumentDate,
                           md.LgsMaterialRequestHeader.RequestedBy
                       } into g
                       select new
                       {
                           g.Key.DocumentNo,
                           g.Key.LocationName,
                           DocumentDate = g.Key.DocumentDate,
                           g.Key.RequestedBy
                       }).ToArray().OrderByDescending(md => md.DocumentDate);

            foreach (var item in qry)
            {
                LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
                lgsMaterialRequestHeader.DocumentNo = item.DocumentNo;
                lgsMaterialRequestHeader.LocationName = item.LocationName;
                lgsMaterialRequestHeader.DocumentDateShort = item.DocumentDate.ToShortDateString();
                lgsMaterialRequestHeader.RequestedBy = item.RequestedBy;

                rtnList.Add(lgsMaterialRequestHeader);
            }
            return rtnList;
        }

        public int RequestOutboxCount() 
        {
            int count;
            var qry = (from md in context.LgsMaterialRequestDetails
                       join l in context.Locations on md.LgsMaterialRequestHeader.LocationID equals l.LocationID
                       where md.BalanceQty == 0
                       group new { md.LgsMaterialRequestHeader, l } by new
                       {
                           md.LgsMaterialRequestHeader.DocumentNo,
                           l.LocationName,
                           DocumentDate = md.LgsMaterialRequestHeader.DocumentDate,
                           md.LgsMaterialRequestHeader.RequestedBy
                       } into g
                       select new
                       {
                           g.Key.DocumentNo,
                           g.Key.LocationName,
                           DocumentDate = g.Key.DocumentDate,
                           g.Key.RequestedBy
                       }).ToList().Count();

            count = qry;
            return count;
        }

        public List<LgsMaterialRequestHeader> GetPendingRequestions() 
        {
            List<LgsMaterialRequestHeader> rtnList = new List<LgsMaterialRequestHeader>();

            var qry = (from md in context.LgsMaterialRequestDetails
                       join l in context.Locations on md.LgsMaterialRequestHeader.LocationID equals l.LocationID
                       where md.BalanceQty != 0
                       && md.BalanceQty != md.OrderQty
                       group new { md.LgsMaterialRequestHeader, l } by new
                       {
                           md.LgsMaterialRequestHeader.DocumentNo,
                           l.LocationName,
                           DocumentDate = md.LgsMaterialRequestHeader.DocumentDate,
                           md.LgsMaterialRequestHeader.RequestedBy
                       } into g
                       select new
                       {
                           g.Key.DocumentNo,
                           g.Key.LocationName,
                           DocumentDate = g.Key.DocumentDate,
                           g.Key.RequestedBy
                       }).ToArray().OrderByDescending(md => md.DocumentDate);

            foreach (var item in qry)
            {
                LgsMaterialRequestHeader lgsMaterialRequestHeader = new LgsMaterialRequestHeader();
                lgsMaterialRequestHeader.DocumentNo = item.DocumentNo;
                lgsMaterialRequestHeader.LocationName = item.LocationName;
                lgsMaterialRequestHeader.DocumentDateShort = item.DocumentDate.ToShortDateString();
                lgsMaterialRequestHeader.RequestedBy = item.RequestedBy;

                rtnList.Add(lgsMaterialRequestHeader);
            }
            return rtnList;
        }

        public int PendingRequestCount() 
        {
            int count;
            var qry = (from md in context.LgsMaterialRequestDetails
                       join l in context.Locations on md.LgsMaterialRequestHeader.LocationID equals l.LocationID
                       where md.BalanceQty != 0
                       && md.BalanceQty != md.OrderQty
                       group new { md.LgsMaterialRequestHeader, l } by new
                       {
                           md.LgsMaterialRequestHeader.DocumentNo,
                           l.LocationName,
                           DocumentDate = md.LgsMaterialRequestHeader.DocumentDate,
                           md.LgsMaterialRequestHeader.RequestedBy
                       } into g
                       select new
                       {
                           g.Key.DocumentNo,
                           g.Key.LocationName,
                           DocumentDate = g.Key.DocumentDate,
                           g.Key.RequestedBy
                       }).ToList().Count();

            count = qry;
            return count;
        }

        public List<LgsPurchaseOrderHeader> GetPendingPurchaseOrders() 
        {
            List<LgsPurchaseOrderHeader> rtnList = new List<LgsPurchaseOrderHeader>();

            var qry = (from ph in context.LgsPurchaseOrderHeaders
                       join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                       where pd.OrderQty != pd.BalanceQty

                       group new { ph } by new
                       {
                           ph.DocumentNo,
                           ph.DocumentDate,
                           ph.ExpiryDate
                       } into g
                       select new
                       {
                           DocumentNo = g.Key.DocumentNo,
                           DocumentDateShort = g.Key.DocumentDate,
                           ExpiryDate = g.Key.ExpiryDate
                       }).ToArray().OrderByDescending(p => p.DocumentDateShort);

            foreach (var item in qry)
            {
                LgsPurchaseOrderHeader lgsPurchaseOrderHeader = new LgsPurchaseOrderHeader();
                lgsPurchaseOrderHeader.DocumentNo = item.DocumentNo;
                lgsPurchaseOrderHeader.DocumentDateShort = item.DocumentDateShort.ToShortDateString();
                lgsPurchaseOrderHeader.ExpiryDateShort = item.ExpiryDate.ToShortDateString();

                rtnList.Add(lgsPurchaseOrderHeader);
            }

            return rtnList;
        }

        public int PendingPurchaseOrderCount()
        {
            int count;
            return count = (from ph in context.LgsPurchaseOrderHeaders
                            join pd in context.LgsPurchaseOrderDetails on ph.LgsPurchaseOrderHeaderID equals pd.LgsPurchaseOrderHeaderID
                            where pd.OrderQty != pd.BalanceQty

                            group new { ph } by new
                            {
                                ph.DocumentNo,
                                ph.DocumentDate,
                                ph.ExpiryDate
                            } into g
                            select new
                            {
                                DocumentNo = g.Key.DocumentNo,
                                DocumentDateShort = g.Key.DocumentDate,
                                ExpiryDate = g.Key.ExpiryDate
                            }).ToArray().Count();
        }

        #endregion

    }
}
