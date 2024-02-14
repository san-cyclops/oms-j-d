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
using System.Collections;
using System.Data.Entity.Core.Objects;
using MoreLinq;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class InvQuotationService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public InvQuotationDetailTemp GetQuotationDetailTemp(List<InvQuotationDetailTemp> InvQuotationDetailTemp, InvProductMaster invProductMaster, int locationID, long unitOfMeasureID)
        {
            InvQuotationDetailTemp invQuotationDetailTemp = new InvQuotationDetailTemp();
            decimal defaultConvertFactor = 1;
            var purchaseDetailTemp = (from pm in context.InvProductMasters
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
                purchaseDetailTemp = (dynamic)null;

                purchaseDetailTemp = (from pm in context.InvProductMasters
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

            foreach (var tempProduct in purchaseDetailTemp)
            {
                if (!unitOfMeasureID.Equals(0) && !invProductMaster.IsExpiry.Equals(true))
                    invQuotationDetailTemp = InvQuotationDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else if (!unitOfMeasureID.Equals(0) && invProductMaster.IsExpiry.Equals(true))
                    invQuotationDetailTemp = InvQuotationDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID && p.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
                else
                    invQuotationDetailTemp = InvQuotationDetailTemp.Where(p => p.ProductID == invProductMaster.InvProductMasterID).FirstOrDefault();

                if (invQuotationDetailTemp == null)
                {
                    invQuotationDetailTemp = new InvQuotationDetailTemp();
                    invQuotationDetailTemp.ProductID = tempProduct.InvProductMasterID;
                    invQuotationDetailTemp.ProductCode = tempProduct.ProductCode;
                    invQuotationDetailTemp.ProductName = tempProduct.ProductName;
                    invQuotationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                    invQuotationDetailTemp.CostPrice = tempProduct.CostPrice;
                    invQuotationDetailTemp.SellingPrice = tempProduct.SellingPrice;
                    invQuotationDetailTemp.AverageCost = tempProduct.AverageCost;
                    invQuotationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                    invQuotationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                }
            }

            return invQuotationDetailTemp;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvQuotationHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetDocumentNumbersToInvoice(int locationID)
        {
            List<string> documentNoList = context.InvQuotationHeaders.Where(q => q.LocationID.Equals(locationID) && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersToInvoice(int documentID, int locationID)
        {
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.InvQuotationHeaders
                       join pd in context.InvQuotationDetails on ph.InvQuotationHeaderID equals pd.InvQuotationHeaderID
                       where ph.DocumentID == documentID && ph.LocationID == locationID && pd.BalanceQty != 0
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

        public List<InvQuotationDetailTemp> GetUpdateQuotationDetailTemp(List<InvQuotationDetailTemp> invQuotationTempDetail, InvQuotationDetailTemp invQuotationDetail, InvProductMaster invProductMaster)
        {
            InvQuotationDetailTemp invQuotationDetailTemp = new InvQuotationDetailTemp();

            invQuotationDetailTemp = invQuotationTempDetail.Where(p => p.ProductID == invQuotationDetail.ProductID && p.UnitOfMeasureID == invQuotationDetail.UnitOfMeasureID).FirstOrDefault();

            if (invQuotationDetailTemp == null || invQuotationDetailTemp.LineNo.Equals(0))
            {
                invQuotationDetail.LineNo = invQuotationTempDetail.Count + 1;
            }
            else
            {
                invQuotationTempDetail.Remove(invQuotationDetailTemp);
                invQuotationDetail.LineNo = invQuotationDetailTemp.LineNo;
            }

            invQuotationTempDetail.Add(invQuotationDetail);

            return invQuotationTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        public InvQuotationHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.InvQuotationHeaders.Where(q => q.DocumentNo == pausedDocumentNo.Trim() && q.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public InvQuotationHeader GetPausedQuotationHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvQuotationHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Add or Update products of grid view
        /// </summary>
        /// <param name="invQuotationDetailTemp"></param>
        /// <returns></returns>
        public List<InvQuotationDetailTemp> GetInvQuotationDetailTempList(List<InvQuotationDetailTemp> invQuotationDetailsTemp, InvQuotationDetailTemp invQuotationDetailTemp)
        {
            if (invQuotationDetailsTemp.Where(pr => pr.ProductCode == invQuotationDetailTemp.ProductCode && pr.UnitOfMeasure == invQuotationDetailTemp.UnitOfMeasure).FirstOrDefault() != null)
            {
                invQuotationDetailTemp.LineNo = invQuotationDetailsTemp.Where(pr => pr.ProductCode == invQuotationDetailTemp.ProductCode).FirstOrDefault().LineNo;
                invQuotationDetailsTemp.RemoveAll(pr => pr.ProductCode == invQuotationDetailTemp.ProductCode);
                invQuotationDetailsTemp.Add(invQuotationDetailTemp);
            }
            else
            {
                // Insert New Record
                if (int.Equals(invQuotationDetailsTemp.Count, 0))
                { invQuotationDetailTemp.LineNo = 1; }
                else
                { invQuotationDetailTemp.LineNo = invQuotationDetailsTemp.Max(ln => ln.LineNo) + 1; }
                invQuotationDetailsTemp.Add(invQuotationDetailTemp);
            }
            return invQuotationDetailsTemp;
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<InvQuotationDetailTemp> DeleteProductinvQuotationDetailTemp(List<InvQuotationDetailTemp> invQuotationDetailsTemp, string productCode)
        {
            invQuotationDetailsTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return invQuotationDetailsTemp;
        }

        public List<InvQuotationDetail> GetAllInvQuotationDetail()
        {
            return context.InvQuotationDetails.ToList();
        }

        public bool Save(InvQuotationHeader invQuotationHeader, List<InvQuotationDetailTemp> invQuotationDetailTemp)
        {
            if (invQuotationHeader.InvQuotationHeaderID.Equals(0))
            {context.InvQuotationHeaders.Add(invQuotationHeader);}
            else
            {context.Entry(invQuotationHeader).State = EntityState.Modified;}

            context.SaveChanges();

            List<InvQuotationDetail> invQuotationDetailDelete = new List<InvQuotationDetail>();
            invQuotationDetailDelete = context.InvQuotationDetails.Where(p => p.InvQuotationHeaderID == invQuotationHeader.InvQuotationHeaderID).ToList();

            foreach (InvQuotationDetail invQuotationDetailDeleteTemp in invQuotationDetailDelete)
            {
                context.InvQuotationDetails.Remove(invQuotationDetailDeleteTemp);
            }
            context.SaveChanges();

            foreach (InvQuotationDetailTemp invQuotationDetail in invQuotationDetailTemp)
            {
                InvQuotationDetail invQuotationDetailSave = new InvQuotationDetail();

                invQuotationDetailSave.InvQuotationHeaderID = invQuotationHeader.InvQuotationHeaderID;
                invQuotationDetailSave.CompanyID = invQuotationHeader.CompanyID;
                invQuotationDetailSave.LocationID = invQuotationHeader.LocationID;
                invQuotationDetailSave.CostCentreID = invQuotationHeader.CostCentreID;
                invQuotationDetailSave.ProductID = invQuotationDetail.ProductID;
                invQuotationDetailSave.DocumentID = invQuotationHeader.DocumentID;
                invQuotationDetailSave.ConvertFactor = invQuotationDetail.ConvertFactor;
                invQuotationDetailSave.CostPrice = invQuotationDetail.CostPrice;
                invQuotationDetailSave.DiscountAmount = invQuotationDetail.DiscountAmount;
                invQuotationDetailSave.DiscountPercentage = invQuotationDetail.DiscountPercentage;
                invQuotationDetailSave.DocumentStatus = invQuotationHeader.DocumentStatus;
                invQuotationDetailSave.GrossAmount = invQuotationDetail.GrossAmount;
                invQuotationDetailSave.GroupOfCompanyID = invQuotationHeader.GroupOfCompanyID;
                invQuotationDetailSave.LineNo = invQuotationDetail.LineNo;
                invQuotationDetailSave.NetAmount = invQuotationDetail.NetAmount;
                invQuotationDetailSave.OrderQty = invQuotationDetail.OrderQty;
                invQuotationDetailSave.BalanceQty = invQuotationDetail.OrderQty;
                invQuotationDetailSave.SellingPrice = invQuotationDetail.SellingPrice;
                invQuotationDetailSave.SubTotalDiscount = invQuotationDetail.SubTotalDiscount;
                invQuotationDetailSave.TaxAmount1 = invQuotationDetail.TaxAmount1;
                invQuotationDetailSave.TaxAmount2 = invQuotationDetail.TaxAmount2;
                invQuotationDetailSave.TaxAmount3 = invQuotationDetail.TaxAmount3;
                invQuotationDetailSave.TaxAmount4 = invQuotationDetail.TaxAmount4;
                invQuotationDetailSave.TaxAmount5 = invQuotationDetail.TaxAmount5;
                invQuotationDetailSave.UnitOfMeasureID = invQuotationDetail.UnitOfMeasureID;
                invQuotationDetailSave.BaseUnitID = invQuotationDetail.BaseUnitID;

                context.InvQuotationDetails.Add(invQuotationDetailSave);

                context.SaveChanges();

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


        public InvQuotationHeader GetQuotationByDocumentNo(string documentNo)
        {
            return context.InvQuotationHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.DocumentStatus != 2).FirstOrDefault();
        }

        public InvQuotationHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.InvQuotationHeaders.Where(po => po.InvQuotationHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public List<InvQuotationDetailTemp> GetPausedQuotationDetail(InvQuotationHeader invQuotationHeader)
        {
            InvQuotationDetailTemp invQuotationDetailTemp;

            List<InvQuotationDetailTemp> invQuotationDetailTempList = new List<InvQuotationDetailTemp>();


            var quotationDetailTemp = (from pm in context.InvProductMasters
                                       join qd in context.InvQuotationDetails on pm.InvProductMasterID equals qd.ProductID
                                       join uc in context.UnitOfMeasures on qd.UnitOfMeasureID equals uc.UnitOfMeasureID
                                       where qd.LocationID.Equals(invQuotationHeader.LocationID) && qd.CompanyID.Equals(invQuotationHeader.CompanyID)
                                       && qd.DocumentID.Equals(invQuotationHeader.DocumentID) && qd.InvQuotationHeaderID.Equals(invQuotationHeader.InvQuotationHeaderID)
                                       select new
                                       {
                                           qd.ConvertFactor,
                                           qd.CostPrice,
                                           qd.DiscountAmount,
                                           qd.DiscountPercentage,
                                           qd.GrossAmount,
                                           qd.LineNo,
                                           qd.LocationID,
                                           qd.NetAmount,
                                           qd.OrderQty,
                                           pm.ProductCode,
                                           qd.ProductID,
                                           pm.ProductName,
                                           qd.BaseUnitID,
                                           qd.SellingPrice,
                                           qd.SubTotalDiscount,
                                           qd.TaxAmount1,
                                           qd.TaxAmount2,
                                           qd.TaxAmount3,
                                           qd.TaxAmount4,
                                           qd.TaxAmount5,
                                           qd.UnitOfMeasureID,
                                           uc.UnitOfMeasureName

                                       }).ToArray();

            foreach (var tempProduct in quotationDetailTemp)
            {

                invQuotationDetailTemp = new InvQuotationDetailTemp();

                invQuotationDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                invQuotationDetailTemp.CostPrice = tempProduct.CostPrice;
                invQuotationDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                invQuotationDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                invQuotationDetailTemp.GrossAmount = tempProduct.GrossAmount;
                invQuotationDetailTemp.LineNo = tempProduct.LineNo;
                invQuotationDetailTemp.LocationID = tempProduct.LocationID;
                invQuotationDetailTemp.NetAmount = tempProduct.NetAmount;
                invQuotationDetailTemp.OrderQty = tempProduct.OrderQty;
                invQuotationDetailTemp.ProductCode = tempProduct.ProductCode;
                invQuotationDetailTemp.ProductID = tempProduct.ProductID;
                invQuotationDetailTemp.ProductName = tempProduct.ProductName;
                invQuotationDetailTemp.BaseUnitID = tempProduct.BaseUnitID;
                invQuotationDetailTemp.SellingPrice = tempProduct.SellingPrice;
                invQuotationDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                invQuotationDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                invQuotationDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                invQuotationDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                invQuotationDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                invQuotationDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                invQuotationDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                invQuotationDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;

                invQuotationDetailTempList.Add(invQuotationDetailTemp);
            }
            return invQuotationDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvQuotationDetailTemp> GetDeleteInvQuotationDetailTemp(List<InvQuotationDetailTemp> invQuotationTempDetail, InvQuotationDetailTemp lgsQuotationDetailTemp)
        {
            InvQuotationDetailTemp invQuotationTempDetailTemp = new InvQuotationDetailTemp();

            invQuotationTempDetailTemp = invQuotationTempDetail.Where(p => p.ProductID == lgsQuotationDetailTemp.ProductID && p.UnitOfMeasureID == lgsQuotationDetailTemp.UnitOfMeasureID).FirstOrDefault();
            long removedLineNo = 0;
            if (invQuotationTempDetailTemp != null)
            {
                invQuotationTempDetail.Remove(invQuotationTempDetailTemp);
                removedLineNo = invQuotationTempDetailTemp.LineNo;

            }
            invQuotationTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            return invQuotationTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.InvQuotationHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "CustomerID":
                    qryResult = qryResult.Join(context.Customers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CustomerCode)").
                                OrderBy("CustomerCode").Select("CustomerCode");
                    break;
                case "InvSalesPersonID":
                    qryResult = qryResult.Join(context.InvSalesPersons, reportDataStruct.DbColumnName.Trim(), "InvSalesPersonID", "new(inner.SalesPersonCode)").
                                    OrderBy("SalesPersonCode").Select("SalesPersonCode");
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
        /// Get Quotations destails for report generator
        /// </summary>
        /// <returns></returns>       
        public DataTable GetQuotationsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {            
            StringBuilder selectFields = new StringBuilder();
            var query = context.InvQuotationHeaders.Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfo.DocumentID);

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
            }

           
            var queryResult = (from h in query
                               join c in context.Customers on h.CustomerID equals c.CustomerID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               join sp in context.InvSalesPersons on h.InvSalesPersonID equals sp.InvSalesPersonID
                               select
                                   new
                                   {
                                       FieldString1 = h.DocumentNo,
                                       FieldString2 = EntityFunctions.TruncateTime(h.DocumentDate),
                                       FieldString3 = c.CustomerName,
                                       FieldString4 = sp.SalesPersonName,
                                       FieldString5 = h.ReferenceNo,
                                       FieldString6 = h.Remark,
                                       //FieldString6 = l.LocationName,
                                       FieldDecimal1 = h.NetAmount,
                                       FieldDecimal2 = h.GrossAmount,
                                       FieldDecimal3 = h.DiscountPercentage,
                                       FieldDecimal4 = h.DiscountAmount,
                                       FieldDecimal5 = h.TaxAmount
                                   }).ToArray();

            return queryResult.ToDataTable();
        }

        public DataTable GetQuotationTransactionDataTable(string documentNo, int documentStatus, int documentid)
        {
            var query = (
                            from ph in context.InvQuotationHeaders
                            join pd in context.InvQuotationDetails on ph.InvQuotationHeaderID equals
                                pd.InvQuotationHeaderID
                            join pm in context.InvProductMasters on pd.ProductID equals pm.InvProductMasterID
                            join um in context.UnitOfMeasures on pd.UnitOfMeasureID equals um.UnitOfMeasureID
                            join s in context.Customers on ph.CustomerID equals s.CustomerID
                            join l in context.Locations on ph.LocationID equals l.LocationID
                            where
                                ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) &&
                                ph.DocumentID.Equals(documentid)
                            select
                                new
                                    {
                                        FieldString1 = ph.DocumentNo,
                                        FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                        FieldString3 = s.CustomerCode + "  " + s.CustomerName,
                                        FieldString4 = ph.Remark,
                                        FieldString5 = "",
                                        FieldString6 = "",
                                        FieldString7 = "",
                                        FieldString8 = "",
                                        FieldString9 = ph.ReferenceNo,
                                        FieldString10 = "",
                                        FieldString11 = "",
                                        FieldString12 = l.LocationCode + "  " + l.LocationName,
                                        FieldString13 = pm.ProductCode,
                                        FieldString14 = pm.ProductName,
                                        FieldString15 = um.UnitOfMeasureName,
                                        FieldString16 = "",
                                        FieldString17 = pd.OrderQty,
                                        FieldString18 = "0",
                                        FieldString19 = pd.CostPrice,
                                        FieldString20 = pd.DiscountPercentage,
                                        FieldString21 = pd.DiscountAmount,
                                        FieldString22 = pd.NetAmount,
                                        FieldString23 = ph.GrossAmount,
                                        FieldString24 = ph.DiscountPercentage,
                                        FieldString25 = ph.DiscountAmount,
                                        FieldString26 = ph.TaxAmount,
                                        FieldString27 = ph.OtherCharges,
                                        FieldString28 = ph.NetAmount,
                                        FieldString29 = "",
                                        FieldString30 = ph.CreatedUser
                                    });
            return query.AsEnumerable().ToDataTable();

        }

        #endregion
    }
}
