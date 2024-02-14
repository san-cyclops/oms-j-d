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
using System.Data.Entity.Validation;
using System.Transactions;

namespace Service
{
    public class SalesOrderService
    {
        ERPDbContext context = new ERPDbContext();

        public enum OrderTypes
        {
            Pending = 1,
            Cancel = 2,
        }

        #region Methods

        public void DeleteSalesOrder(string DocumentNo)
        {
            CommonService.ExecuteSqlQuery("update sd set sd.DocumentStatus = 4 from SalesOrderDetail sd inner join SalesOrderHeader sh on sh.SalesOrderHeaderID=sd.SalesOrderHeaderID   where sh.DocumentNo = '" + DocumentNo + "'");
            CommonService.ExecuteSqlQuery("update SalesOrderHeader set DocumentStatus = 4 where DocumentNo = '" + DocumentNo + "'");
        }

        public SalesOrderDetailTemp GetSalesOrderDetailTemp(List<SalesOrderDetailTemp> SalesOrderDetailTemp,string productCode, int locationID, long unitOfMeasureID)
        {
            SalesOrderDetailTemp salesOrderDetailTemp1 = new SalesOrderDetailTemp();

            if (SalesOrderDetailTemp == null)
                SalesOrderDetailTemp = new List<SalesOrderDetailTemp>();

            salesOrderDetailTemp1 = SalesOrderDetailTemp.Where(p => p.ProductCode == productCode).FirstOrDefault();

            foreach (var tempProduct in SalesOrderDetailTemp)
            {
                if (salesOrderDetailTemp1 == null)
                {
                    salesOrderDetailTemp1 = new SalesOrderDetailTemp();
                    salesOrderDetailTemp1.ProductID = tempProduct.ProductID;
                    salesOrderDetailTemp1.ProductCode = tempProduct.ProductCode;
                    salesOrderDetailTemp1.ProductName = tempProduct.ProductName;
                    salesOrderDetailTemp1.Size = tempProduct.Size;
                    salesOrderDetailTemp1.Gauge = tempProduct.Gauge;
                    salesOrderDetailTemp1.NetAmount = tempProduct.NetAmount;
                    salesOrderDetailTemp1.CostPrice = tempProduct.CostPrice;
                    salesOrderDetailTemp1.SellingPrice = tempProduct.SellingPrice;
                    salesOrderDetailTemp1.UnitOfMeasure = tempProduct.UnitOfMeasure;
                    salesOrderDetailTemp1.ConvertFactor = tempProduct.ConvertFactor;

                }
            }

            return salesOrderDetailTemp1;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.SalesOrderHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID && q.DocumentStatus == 1).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetDocumentNumbersToDispatch(int locationID)
        {
            List<string> documentNoList = context.SalesOrderHeaders.Where(q => q.LocationID.Equals(locationID) && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }


        public string[] GetAllDocumentNumbersToDispatch(int documentID, int locationID)
        {
            List<string> documentNoList = new List<string>();

            var sql = (from ph in context.SalesOrderHeaders
                       join pd in context.SalesOrderDetails on ph.SalesOrderHeaderID equals pd.SalesOrderHeaderID
                       where ph.LocationID == locationID && pd.BalanceQty != 0 && ph.DocumentStatus==1
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

        public List<SalesOrderDetailTemp> GetUpdateSalesOrderDetailTemp(List<SalesOrderDetailTemp> salesOrderTempDetail, SalesOrderDetailTemp salesOrderDetail, InvProductMaster invProductMaster)
        {
            SalesOrderDetailTemp salesOrderDetailTemp = new SalesOrderDetailTemp();

            if (salesOrderTempDetail == null)
                salesOrderTempDetail = new List<SalesOrderDetailTemp>();
            salesOrderDetailTemp = salesOrderTempDetail.Where(p => p.ProductCode == salesOrderDetail.ProductCode).FirstOrDefault();

            if (salesOrderDetailTemp == null || salesOrderDetailTemp.LineNo.Equals(0))
            {
                salesOrderDetail.LineNo = salesOrderTempDetail.Count + 1;
            }
            else
            {
                salesOrderTempDetail.Remove(salesOrderDetailTemp);
                salesOrderDetail.LineNo = salesOrderDetailTemp.LineNo;

            }
            salesOrderTempDetail.Add(salesOrderDetail);

            return salesOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="InvDepartmentCode"></param>
        /// <returns></returns>
        public SalesOrderHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.SalesOrderHeaders.Where(q => q.DocumentNo == pausedDocumentNo.Trim() && q.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public SalesOrderHeader GetPausedSalesOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.SalesOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public SalesOrderHeader GetSavedSalesOrderHeaderByDocumentID(int documentDocumentID, long documentID)
        {
            return context.SalesOrderHeaders.Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.SalesOrderHeaderID.Equals(documentID) && ph.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public SalesOrderHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.SalesOrderHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        /// <summary>
        /// Delete a product from grid view.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<SalesOrderDetailTemp> DeleteProductSalesOrderDetailTemp(List<SalesOrderDetailTemp> salesOrderDetailTemp, string productCode)
        {
            salesOrderDetailTemp.RemoveAll(pr => pr.ProductCode == productCode);
            return salesOrderDetailTemp;
        }

        public List<SalesOrderDetail> GetAllSalesOrderDetail() 
        {
            return context.SalesOrderDetails.ToList();
        }
        
        public bool Save(SalesOrderHeader salesOrderHeader, List<SalesOrderDetailTemp> salesOrderDetailTemp,decimal exchangeRate)
        {

            using (TransactionScope transaction = new TransactionScope())
            {
                //context.AutoGenerateInfos.Update(phnew => new AutoGenerateInfo { ExchangeRate = exchangeRate });

                if (salesOrderHeader.SalesOrderHeaderID.Equals(0))
                { context.SalesOrderHeaders.Add(salesOrderHeader); }
                else
                { context.Entry(salesOrderHeader).State = EntityState.Modified; }

                context.SaveChanges();

                List<SalesOrderDetail> salesOrderDetailDelete = new List<SalesOrderDetail>();
                salesOrderDetailDelete = context.SalesOrderDetails.Where(p => p.SalesOrderHeaderID == salesOrderHeader.SalesOrderHeaderID).ToList();

                foreach (SalesOrderDetail SalesOrderDetailDeleteTemp in salesOrderDetailDelete)
                {
                    context.SalesOrderDetails.Remove(SalesOrderDetailDeleteTemp);
                }
                context.SaveChanges();

                foreach (SalesOrderDetailTemp salesOrderDetail in salesOrderDetailTemp)
                {
                    SalesOrderDetail salesOrderDetailSave = new SalesOrderDetail();

                    salesOrderDetailSave.SalesOrderHeaderID = salesOrderHeader.SalesOrderHeaderID;
                    salesOrderDetailSave.CompanyID = salesOrderHeader.CompanyID;
                    salesOrderDetailSave.LocationID = salesOrderHeader.LocationID;
                    salesOrderDetailSave.CostCentreID = salesOrderHeader.CostCentreID;
                    salesOrderDetailSave.ProductID = salesOrderDetail.ProductID;
                    salesOrderDetailSave.DocumentID = salesOrderHeader.DocumentID;
                    salesOrderDetailSave.ConvertFactor = salesOrderDetail.ConvertFactor;
                    salesOrderDetailSave.CostPrice = salesOrderDetail.CostPrice;
                    salesOrderDetailSave.DiscountAmount = salesOrderDetail.DiscountAmount;
                    salesOrderDetailSave.DiscountPercentage = salesOrderDetail.DiscountPercentage;
                    salesOrderDetailSave.DocumentStatus = salesOrderHeader.DocumentStatus;
                    salesOrderDetailSave.GroupOfCompanyID = salesOrderHeader.GroupOfCompanyID;
                    salesOrderDetailSave.LineNo = salesOrderDetail.LineNo;
                    salesOrderDetailSave.NetAmount = salesOrderDetail.NetAmount;
                    salesOrderDetailSave.GrossAmount = salesOrderDetail.GrossAmount;
                    salesOrderDetailSave.OrderQty = salesOrderDetail.OrderQty;
                    salesOrderDetailSave.FreeQty = salesOrderDetail.FreeQty;
                    salesOrderDetailSave.CurrentQty = salesOrderDetail.CurrentQty;
                    salesOrderDetailSave.BalanceQty = salesOrderDetail.OrderQty;
                    salesOrderDetailSave.SellingPrice = salesOrderDetail.SellingPrice;
                    salesOrderDetailSave.SubTotalDiscount = salesOrderDetail.SubTotalDiscount;
                    salesOrderDetailSave.TaxAmount1 = salesOrderDetail.TaxAmount1;
                    salesOrderDetailSave.TaxAmount2 = salesOrderDetail.TaxAmount2;
                    salesOrderDetailSave.TaxAmount3 = salesOrderDetail.TaxAmount3;
                    salesOrderDetailSave.TaxAmount4 = salesOrderDetail.TaxAmount4;
                    salesOrderDetailSave.TaxAmount5 = salesOrderDetail.TaxAmount5;
                    salesOrderDetailSave.UnitOfMeasureID = salesOrderDetail.UnitOfMeasureID;
                    salesOrderDetailSave.BaseUnitID = salesOrderDetail.BaseUnitID;
                    salesOrderDetailSave.Size = salesOrderDetail.Size;
                    salesOrderDetailSave.Gauge = salesOrderDetail.Gauge;
                    salesOrderDetailSave.ProductCode = salesOrderDetail.ProductCode;
                    salesOrderDetailSave.ProductName = salesOrderDetail.ProductName;



                    context.SalesOrderDetails.Add(salesOrderDetailSave);
                    context.SaveChanges();

                }
                transaction.Complete();
                return true;
            }

        }
        
        public SalesOrderHeader GetSalesOrderByDocumentNo(string documentNo)
        {
            return context.SalesOrderHeaders.Where(q => q.DocumentNo.Equals(documentNo) && q.DocumentStatus != 3).FirstOrDefault();
        }

        public List<SalesOrderDetailTemp> GetPausedSalesOrderDetail(SalesOrderHeader salesOrderHeader)
        {
            SalesOrderDetailTemp salesOrderDetailTemp;

            List<SalesOrderDetailTemp> salesOrderDetailTempList = new List<SalesOrderDetailTemp>();
            
            var salesOrder = (from qd in context.SalesOrderDetails
                              where qd.LocationID.Equals(salesOrderHeader.LocationID) && qd.CompanyID.Equals(salesOrderHeader.CompanyID)
                              && qd.DocumentID.Equals(salesOrderHeader.DocumentID) && qd.SalesOrderHeaderID.Equals(salesOrderHeader.SalesOrderHeaderID)
                              select new
                              {
                                  qd.ConvertFactor,
                                  qd.CostPrice,
                                  qd.DiscountAmount,
                                  qd.DiscountPercentage,
                                  qd.LineNo,
                                  qd.LocationID,
                                  qd.NetAmount,
                                  qd.OrderQty,
                                  qd.ProductCode,
                                  qd.ProductID,
                                  qd.ProductName,
                                  qd.BaseUnitID,
                                  qd.SellingPrice,
                                  qd.SubTotalDiscount,
                                  qd.TaxAmount1,
                                  qd.TaxAmount2,
                                  qd.TaxAmount3,
                                  qd.TaxAmount4,
                                  qd.TaxAmount5,
                                  qd.UnitOfMeasureID,
                                  UnitOfMeasureName ="",
                                  qd.Size,
                                  qd.Gauge,
                                  qd.IsDelete,

                              }).ToArray();

            foreach (var tempProduct in salesOrder)
            {
                salesOrderDetailTemp = new SalesOrderDetailTemp();

                salesOrderDetailTemp.ConvertFactor = tempProduct.ConvertFactor;
                salesOrderDetailTemp.CostPrice = tempProduct.CostPrice;
                salesOrderDetailTemp.DiscountAmount = tempProduct.DiscountAmount;
                salesOrderDetailTemp.DiscountPercentage = tempProduct.DiscountPercentage;
                salesOrderDetailTemp.LineNo = tempProduct.LineNo;
                salesOrderDetailTemp.LocationID = tempProduct.LocationID;
                salesOrderDetailTemp.NetAmount = tempProduct.NetAmount;
                salesOrderDetailTemp.OrderQty = tempProduct.OrderQty;
                salesOrderDetailTemp.ProductCode = tempProduct.ProductCode;
                salesOrderDetailTemp.ProductID = tempProduct.ProductID;
                salesOrderDetailTemp.ProductName = tempProduct.ProductName;
                salesOrderDetailTemp.BaseUnitID = tempProduct.BaseUnitID;
                salesOrderDetailTemp.SellingPrice = tempProduct.SellingPrice;
                salesOrderDetailTemp.SubTotalDiscount = tempProduct.SubTotalDiscount;
                salesOrderDetailTemp.TaxAmount1 = tempProduct.TaxAmount1;
                salesOrderDetailTemp.TaxAmount2 = tempProduct.TaxAmount2;
                salesOrderDetailTemp.TaxAmount3 = tempProduct.TaxAmount3;
                salesOrderDetailTemp.TaxAmount4 = tempProduct.TaxAmount4;
                salesOrderDetailTemp.TaxAmount5 = tempProduct.TaxAmount5;
                salesOrderDetailTemp.UnitOfMeasure = tempProduct.UnitOfMeasureName;
                salesOrderDetailTemp.UnitOfMeasureID = tempProduct.UnitOfMeasureID;
                salesOrderDetailTemp.Size = tempProduct.Size;
                salesOrderDetailTemp.Gauge = tempProduct.Gauge;
                salesOrderDetailTemp.IsDelete = tempProduct.IsDelete;
                salesOrderDetailTempList.Add(salesOrderDetailTemp);
            }
            return salesOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public bool ValidateCurrentStock(int orderQty, InvProductMaster invProductMaster, int locationId)
        {
            decimal currentStock = 0;
            var getCurrentStock = (from psm in context.InvProductStockMasters
                                   where psm.ProductID.Equals(invProductMaster.InvProductMasterID) && psm.LocationID.Equals(locationId)
                                   select new
                                   {
                                       psm.Stock
                                   }).ToArray();
            
            
            foreach (var temp in getCurrentStock)
            {
                currentStock = temp.Stock;
            }

            if (orderQty > currentStock)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<SalesOrderDetailTemp> GetDeleteSalesOrderDetailTemp(List<SalesOrderDetailTemp> salesOrderTempDetail, string ProductCode)
        {
            SalesOrderDetailTemp salesOrderTempDetailTemp = new SalesOrderDetailTemp();

            salesOrderTempDetailTemp = salesOrderTempDetail.Where(p => p.ProductCode == ProductCode).FirstOrDefault();
            long removedLineNo = 0;
            if (salesOrderTempDetailTemp != null)
            {
                salesOrderTempDetail.Remove(salesOrderTempDetailTemp);
                removedLineNo = salesOrderTempDetailTemp.LineNo;
            }

            salesOrderTempDetail.ToList().Where(x => x.LineNo > removedLineNo).ForEach(x => x.LineNo = x.LineNo - 1);

            return salesOrderTempDetail.OrderBy(pd => pd.LineNo).ToList();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.SalesOrderHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

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
                case "Pending":
                    break;
                case "Cancel":
                    break;

                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
           // if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
           // {
                //foreach (var item in qryResult)
                //{
                //    if (item.Equals(true))
                //    { selectionDataList.Add("Yes"); }
                //    else
                //    { selectionDataList.Add("No"); }
                //}


            //}
           // else
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                if (reportDataStruct.DbColumnName.Trim() == "Pending")  
                {
                     
                        selectionDataList.Add("True");
                        selectionDataList.Add("False");
                    
                }
                if (reportDataStruct.DbColumnName.Trim() == "Cancel")
                {

                    selectionDataList.Add("True");
                    selectionDataList.Add("False");

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


        public DataTable GetSalesOrderDtlDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            try
            {


                using (ERPDbContext context = new ERPDbContext())
                {
                    var query = context.SalesOrderHeaders.AsNoTracking().Where("DocumentStatus=@0 OR DocumentStatus=@1", 1, 4);// AND DocumentID=@1 AND DocumentID=@1", 1,1,3);

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
                        {

                            switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                            {
                                case "Pending":
                                    if (reportConditionsDataStruct.ConditionFrom.Trim() == "True")
                                    {
                                        query = (from qr in query
                                                 join jt in context.SalesOrderDetails on qr.SalesOrderHeaderID equals jt.SalesOrderHeaderID
                                                 where jt.BalanceQty != 0
                                                 select qr
                                                );
                                    }
                                    else
                                    {
                                        query = (from qr in query
                                                 join jt in context.SalesOrderDetails on qr.SalesOrderHeaderID equals jt.SalesOrderHeaderID
                                                 where jt.BalanceQty == 0
                                                 select qr
                                                );
                                    }
                                    break;
                                case "Cancel":
                                    if (reportConditionsDataStruct.ConditionFrom.Trim() == "True")
                                    {
                                        query = (from qr in query
                                                 where qr.DocumentStatus.Equals(4)
                                                 select qr
                                                );
                                    }
                                    else
                                    {
                                        query = (from qr in query
                                                 where qr.DocumentStatus.Equals(1)
                                                 select qr
                                               );
 
                                    }
                                    break;
                                default:
                                    break;

                            }

                            //query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); 
                        }

                        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    }
                    /*
                    group pd by new
                       {
                           ph.DocumentNo,
                           pd.BalanceQty,
                       } into poGroup

                       select new
                       {
                           DocumentNo = poGroup.Key.DocumentNo,
                           BalanceQty = poGroup.Sum(x => x.BalanceQty),
                       }).ToArray();*/

                    var result =    from ph in query 
                                    join pd in context.SalesOrderDetails on ph.SalesOrderHeaderID equals pd.SalesOrderHeaderID
                                    join c in context.Customers on ph.CustomerID equals c.CustomerID
                                    join sp in context.InvSalesPersons on ph.InvSalesPersonID equals sp.InvSalesPersonID
                                    join dep in context.InvDepartments on ph.DepartmentID equals dep.InvDepartmentID
                                    join ct in context.InvCategories on ph.CategoryID equals ct.InvCategoryID
                                    join sct in context.InvSubCategories on ph.SubCategoryID equals sct.InvSubCategoryID
                                    join sct2 in context.InvSubCategories2 on ph.SubCategory2ID equals sct2.InvSubCategory2ID
                                  select new 
                                  {
                                               FieldString1 = ph.DocumentNo,
                                               FieldString2 = pd.ProductCode,
                                               FieldString3 = pd.ProductName,
                                               FieldString4 = ph.Company,
                                               FieldString5 = (ph.DocumentID == 4 ? true: false),  
                                               FieldString6 = (pd.BalanceQty != 0 ? true:false),
                                               FieldString7 = ph.DocumentDate,
                                               FieldString8 = ph.DeliverDate,//.ToShortDateString(),
                                               FieldString9 = c.CustomerName,
                                               FieldString10 = sp.SalesPersonName,
                                               FieldString11 = dep.DepartmentName,
                                               FieldString12 = ct.CategoryName,
                                               FieldString13 = sct.SubCategoryName,
                                               FieldString14 = sct2.SubCategory2Name,
                                               FieldString15 = pd.Size,
                                               FieldString16 = pd.Gauge,

                                               FieldDecimal1 = pd.OrderQty, //(ph.DocumentID == 1 ? ph.Qty : (ph.DocumentID == 2 ? -(ph.Qty) : (ph.DocumentID == 3 ? ph.Qty : (ph.DocumentID == 4 ? -(ph.Qty) : 0)))),
                                               FieldDecimal2 = pd.BalanceQty, //(ph.DocumentID == 1 ? ph.GrossAmount : (ph.DocumentID == 2 ? -(ph.GrossAmount) : (ph.DocumentID == 3 ? ph.GrossAmount : (ph.DocumentID == 4 ? -(ph.GrossAmount) : 0)))),
                                               FieldDecimal3 = pd.SellingPrice,
                                               FieldDecimal4 = pd.NetAmount,//(ph.DocumentID == 1 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 2 ? -(ph.SubTotalDiscountAmount) : (ph.DocumentID == 3 ? ph.SubTotalDiscountAmount : (ph.DocumentID == 4 ? -(ph.SubTotalDiscountAmount) : 0)))),
                                               
                                  };

                    DataTable dtQueryResult = new DataTable();

                    if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy)) // if no any groups
                    {
                        StringBuilder sbOrderByColumns = new StringBuilder();

                        if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
                        {
                            foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                            { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                            sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                            dtQueryResult = result.OrderBy(sbOrderByColumns.ToString())
                                                       .ToDataTable();
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

                        dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString());

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
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        } 



        /// <summary>
        /// Get Sales order destails for report generator
        /// </summary>
        /// <returns></returns>  
        /// 
        //public DataTable GetSalesOrderDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        //{
        //    StringBuilder selectFields = new StringBuilder();
        //    var query = context.SalesOrderHeaders.Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfo.DocumentID);

        //    // Insert Conditions to query
        //    foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
        //    {
        //        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
        //        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

        //        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
        //        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

        //        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
        //        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

        //        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
        //        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

        //        if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
        //        { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
        //    }

        //    var queryResult = (from h in query
        //                       join c in context.Customers on h.CustomerID equals c.CustomerID
        //                       join l in context.Locations on h.LocationID equals l.LocationID
        //                       join sp in context.InvSalesPersons on h.InvSalesPersonID equals sp.InvSalesPersonID
        //                       select
        //                           new
        //                           {
        //                               FieldString1 = h.DocumentNo,
        //                               FieldString2 = EntityFunctions.TruncateTime(h.DocumentDate),
        //                               FieldString3 = c.CustomerName,
        //                               FieldString4 = sp.SalesPersonName,
        //                               FieldString5 = h.ReferenceNo,
        //                               FieldString6 = h.Remark,
        //                               //FieldString6 = l.LocationName,
        //                               FieldDecimal1 = h.NetAmount,
        //                               FieldDecimal2 = h.GrossAmount,
        //                               FieldDecimal3 = h.DiscountPercentage,
        //                               FieldDecimal4 = h.DiscountAmount,
        //                               FieldDecimal5 = h.TaxAmount
        //                           }).ToArray();

        //    return queryResult.ToDataTable();
        //}

        /// <summary>
        /// Get Sales order details for report
        /// </summary>
        /// <returns></returns>
        /// 
     


        public DataTable GetSalesOrderTransactionInvoiceDataTable (string documentNo, int documentStatus, int documentId)
        {

            InvSalesHeader obj = new InvSalesHeader();
            InvSalesServices objser = new InvSalesServices();
            obj = objser.GetSavedSalesHeaderByDocumentID(documentNo);

            if (obj.OtherChargers == 1)
            {
                var query = (from ph in context.InvSalesHeaders
                             join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                             join c in context.Customers on ph.CustomerID equals c.CustomerID
                             join dh in context.SalesOrderHeaders on ph.ReferenceDocumentID equals dh.SalesOrderHeaderID
                             join dd in context.SalesOrderDetails on dh.SalesOrderHeaderID equals dd.SalesOrderHeaderID
                             where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && pd.ProductID.Equals(dd.SalesOrderDetailID) && pd.Qty > 0 //&& ph.DocumentID.Equals(documentId)
                             select
                                 new
                                 {
                                     FieldString1 = "I" + ph.DocumentNo.Substring(1, 6),
                                     FieldString2 = dh.DocumentNo,
                                     FieldString3 = ph.ReferenceNo,
                                     FieldString4 = c.CustomerName,
                                     FieldString5 = c.TaxNo1,
                                     FieldString6 = c.TaxNo3,
                                     FieldString7 = dh.Currency,
                                     FieldString8 = ph.Remark,
                                     FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                     FieldString10 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, // EntityFunctions.TruncateTime(ph.DeliverDate),
                                     FieldDecimal11 = ph.ExchangeRate,
                                     FieldString12 = ph.OtherChargers, //l.LocationCode + "  " + l.LocationName,
                                     FieldString13 = pd.ProductCode,
                                     FieldString14 = pd.ProductName,
                                     FieldDecimal15 = (pd.SellingPrice * dh.DiscountAmount) / ph.ExchangeRate,
                                     FieldDecimal16 = pd.Qty,
                                     FieldDecimal17 = ph.OtherChargers == 1 ? ((ph.NetAmount * dh.DiscountAmount) / ph.ExchangeRate) * ph.ExchangeRate : 0,
                                     FieldDecimal18 = ((pd.SellingPrice * dh.DiscountAmount) / ph.ExchangeRate * pd.Qty),
                                     FieldDecimal19 = ((ph.NetAmount * dh.DiscountAmount) / ph.ExchangeRate),
                                     FieldDecimal20 = (((ph.NetAmount * dh.DiscountAmount) / ph.ExchangeRate) * 11) / 100,
                                     FieldDecimal1 = ((ph.NetAmount * dh.DiscountAmount) / ph.ExchangeRate),//((ph.NetAmount * 11) / 100) * Common.ExchangeRate,
                                     FieldString22 = ph.DocumentNo,
                                     FieldString23 = dd.Size,
                                     FieldString24 = dd.Gauge,
                                     FieldString25 = dh.Unit,


                                 });
                DataTable dtxc = query.ToDataTable();

                return query.AsEnumerable().ToDataTable();
            }
            else
            {
                var query = (from ph in context.InvSalesHeaders
                             join pd in context.InvSalesDetails on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                             join c in context.Customers on ph.CustomerID equals c.CustomerID
                             join dh in context.SalesOrderHeaders on ph.ReferenceDocumentID equals dh.SalesOrderHeaderID
                             join dd in context.SalesOrderDetails on dh.SalesOrderHeaderID equals dd.SalesOrderHeaderID
                             where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && pd.ProductID.Equals(dd.SalesOrderDetailID) && pd.Qty > 0 //&& ph.DocumentID.Equals(documentId)
                             select
                                 new
                                 {
                                     FieldString1 = "I" + ph.DocumentNo.Substring(1, 6),
                                     FieldString2 = dh.DocumentNo,
                                     FieldString3 = ph.ReferenceNo,
                                     FieldString4 = c.CustomerName,
                                     FieldString5 = c.TaxNo1,
                                     FieldString6 = c.TaxNo3,
                                     FieldString7 = dh.Currency,
                                     FieldString8 = ph.Remark,
                                     FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                     FieldString10 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, // EntityFunctions.TruncateTime(ph.DeliverDate),
                                     FieldDecimal11 = ph.ExchangeRate,
                                     FieldString12 = ph.OtherChargers, //l.LocationCode + "  " + l.LocationName,
                                     FieldString13 = pd.ProductCode,
                                     FieldString14 = pd.ProductName,
                                     FieldDecimal15 = pd.SellingPrice,
                                     FieldDecimal16 = pd.Qty,
                                     FieldDecimal17 = ph.OtherChargers == 1 ? ph.NetAmount * ph.ExchangeRate : 0,
                                     FieldDecimal18 = pd.NetAmount,
                                     FieldDecimal19 = ph.NetAmount,
                                     FieldDecimal20 = (ph.NetAmount * 11) / 100,
                                     FieldDecimal1 = ph.NetAmount,//((ph.NetAmount * 11) / 100) * Common.ExchangeRate,
                                     FieldString22 = ph.DocumentNo,
                                     FieldString23 = dd.Size,
                                     FieldString24 = dd.Gauge,
                                     FieldString25 = dh.Unit,
                                 });

                                 DataTable dtxc = query.ToDataTable();

                                return query.AsEnumerable().ToDataTable();
            }

           

        }


        public DataTable GetSalesOrderTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.InvSalesHeaders  
                        join pd in context.InvSalesDetails  on ph.InvSalesHeaderID equals pd.InvSalesHeaderID
                        join c in context.Customers on ph.CustomerID equals c.CustomerID
                        join dh in context.SalesOrderHeaders on ph.ReferenceDocumentID equals dh.SalesOrderHeaderID
                        join dd in context.SalesOrderDetails on dh.SalesOrderHeaderID equals dd.SalesOrderHeaderID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && pd.Qty>0 && dd.SalesOrderDetailID.Equals(pd.ProductID) //&& ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo ,
                                FieldString2 = dh.DocumentNo,
                                FieldString3 = ph.ReferenceNo,
                                FieldString4 = c.CustomerName,
                                FieldString5 = c.TaxNo1,
                                FieldString6 = c.TaxNo3,
                                FieldString7 = dh.Currency,                                
                                FieldString8 = ph.Remark,
                                FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                FieldString10 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, // EntityFunctions.TruncateTime(ph.DeliverDate),
                                FieldString11 = ph.ExchangeRate,
                                FieldString12 = ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pd.ProductCode,
                                FieldString14 = pd.ProductName,
                                FieldString15 = "", //pd.SellingPrice ,
                                FieldDecimal16 = pd.Qty,
                                FieldString17 = dd.Size,
                                FieldString18 = dd.Gauge, //pd.NetAmount,
                                FieldString19 = "",//ph.NetAmount,
                                FieldString20 = "" ,//(ph.NetAmount*12)/100,
                                FieldString21 = "",// ((ph.NetAmount * 12) / 100) * Common.ExchangeRate,
                                FieldString22 = "I" + ph.DocumentNo.Substring(1,6) ,// ph.OtherCharges==1 ? ph.NetAmount * Common.ExchangeRate:0
                                FieldString25 = dh.Unit,
                                FieldString16 = "",
                                //FieldString1 = i.DocumentNo,
                                //FieldString2 = ph.DocumentNo,
                                //FieldString3 = "I"+ i.DocumentNo,
                                //FieldString4 =  c.CustomerCode + "  " + c.CustomerName, //sp.SalesPersonCode + " " + sp.SalesPersonName,
                                //FieldString5 = ph.Remark,
                                //FieldString6 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, //ph.Unit,
                                //FieldString7 = ph.Currency,
                                //FieldString8 = ph.Company,
                                //FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                //FieldString10 = c.TaxNo1,
                                //FieldString11 = c.TaxNo3,
                                //FieldString12 = ""//ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                //FieldString13 = pd.ProductCode,
                                //FieldString14 = pd.ProductName,
                                //FieldString15 = pd.Size,
                                //FieldString16 = pd.Gauge,
                                //FieldString17 = "",
                                //FieldString18 = pd.OrderQty,
                                //FieldString19 = dep.DepartmentName,
                                //FieldString20 = ct.CategoryName,
                                //FieldString21 = sct.SubCategoryName,
                                //FieldString22 = sct2.SubCategory2Name,



                            });
 
            return query.AsEnumerable().ToDataTable();

        }

        public DataTable GetReorderTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.InvSalesHeader2s
                        join pd in context.InvSalesDetail2s on ph.InvSalesHeader2ID equals pd.InvSalesHeader2ID
                        join c in context.Customers on ph.CustomerID equals c.CustomerID
                        join dh in context.SalesOrderHeaders on ph.ReferenceDocumentID equals dh.SalesOrderHeaderID
                        join dd in context.SalesOrderDetails on dh.SalesOrderHeaderID equals dd.SalesOrderHeaderID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && pd.IssuedQty > 0 && dd.SalesOrderDetailID.Equals(pd.ProductID) //&& ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = dh.DocumentNo,
                                FieldString3 = ph.ReferenceNo,
                                FieldString4 = c.CustomerName,
                                FieldString5 = c.TaxNo1,
                                FieldString6 = c.TaxNo3,
                                FieldString7 = dh.Currency,
                                FieldString8 = ph.Remark,
                                FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                FieldString10 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, // EntityFunctions.TruncateTime(ph.DeliverDate),
                                FieldString11 = Common.ExchangeRate,
                                FieldString12 = ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pd.ProductCode,
                                FieldString14 = pd.ProductName,
                                FieldString15 = "", //pd.SellingPrice ,
                                FieldDecimal16 = pd.IssuedQty,
                                FieldString17 = dd.Size,
                                FieldString18 = dd.Gauge, //pd.NetAmount,
                                FieldString19 = "",//ph.NetAmount,
                                FieldString20 = "",//(ph.NetAmount*12)/100,
                                FieldString21 = "",// ((ph.NetAmount * 12) / 100) * Common.ExchangeRate,
                                FieldString22 = "I" + ph.DocumentNo.Substring(1, 6),// ph.OtherCharges==1 ? ph.NetAmount * Common.ExchangeRate:0
                                FieldString25 = dh.Unit,
                                FieldString16 = "",
                                //FieldString1 = i.DocumentNo,
                                //FieldString2 = ph.DocumentNo,
                                //FieldString3 = "I"+ i.DocumentNo,
                                //FieldString4 =  c.CustomerCode + "  " + c.CustomerName, //sp.SalesPersonCode + " " + sp.SalesPersonName,
                                //FieldString5 = ph.Remark,
                                //FieldString6 = c.BillingAddress1 + " " + c.BillingAddress2 + " " + c.BillingAddress3, //ph.Unit,
                                //FieldString7 = ph.Currency,
                                //FieldString8 = ph.Company,
                                //FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                //FieldString10 = c.TaxNo1,
                                //FieldString11 = c.TaxNo3,
                                //FieldString12 = ""//ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                //FieldString13 = pd.ProductCode,
                                //FieldString14 = pd.ProductName,
                                //FieldString15 = pd.Size,
                                //FieldString16 = pd.Gauge,
                                //FieldString17 = "",
                                //FieldString18 = pd.OrderQty,
                                //FieldString19 = dep.DepartmentName,
                                //FieldString20 = ct.CategoryName,
                                //FieldString21 = sct.SubCategoryName,
                                //FieldString22 = sct2.SubCategory2Name,



                            });

            return query.AsEnumerable().ToDataTable();

        }
        public DataTable GetSalesOrderTransactionOldDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.SalesOrderHeaders
                        join pd in context.SalesOrderDetails on ph.SalesOrderHeaderID equals pd.SalesOrderHeaderID
                        join c in context.Customers on ph.CustomerID equals c.CustomerID
                        join sp in context.InvSalesPersons on ph.InvSalesPersonID equals sp.InvSalesPersonID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join dep in context.InvDepartments on ph.DepartmentID equals dep.InvDepartmentID
                        join ct in context.InvCategories on ph.CategoryID equals ct.InvCategoryID
                        join sct in context.InvSubCategories on ph.SubCategoryID equals sct.InvSubCategoryID
                        join sct2 in context.InvSubCategories2 on ph.SubCategory2ID equals sct2.InvSubCategory2ID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = ph.ReferenceNo,
                                FieldString3 = c.CustomerName,
                                FieldString4 = sp.SalesPersonName,
                                FieldString5 = ph.Remark,
                                FieldString6 = ph.Unit,
                                FieldString7 = "",
                                FieldString8 = ph.Company,
                                FieldString9 = EntityFunctions.TruncateTime(ph.DocumentDate),
                                FieldString10 =EntityFunctions.TruncateTime(ph.DeliverDate),
                                FieldString11 = ph.Colour,
                                FieldString12 = "",//ph.CreatedUser, //l.LocationCode + "  " + l.LocationName,
                                FieldString13 = pd.ProductCode,
                                FieldString14 = pd.ProductName,
                                FieldString15 = pd.Size,
                                FieldString16 = pd.Gauge,
                                FieldString17 = "",
                                FieldString18 = pd.OrderQty,
                                FieldString19 = dep.DepartmentName,
                                FieldString20 = ct.CategoryName,
                                FieldString21 = sct.SubCategoryName,
                                FieldString22 = sct2.SubCategory2Name,


                            });
            return query.AsEnumerable().ToDataTable();

        }
        #endregion


        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            documentId = autoGenerateInfo.DocumentID;
 
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
                preFix = preFix + locationCode.Trim();


            if (isTemporytNo)
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });

                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
            }
            else
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });

                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentName.Equals(autoGenerateInfo.FormText) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }

        #endregion

        
    }
}
