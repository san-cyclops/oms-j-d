using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Data;
using Domain;
using Utility;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
namespace Service
{
    public class InvReportService
    {
        public DataTable GetBinCardDetailsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
                autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
                int documentID = autoGeneratePurchaseInfo.DocumentID;

                int stockAdjustmentMode = 0;
                var query = context.InvStockAdjustmentDetails.Where("DocumentStatus= @0 AND DocumentId = @1", 1, documentID);
                
                // Insert Conditions to query
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (DateTime)))
                    { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (decimal)))
                    { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (bool)))
                    { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                    { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }

                //var queryResultData = (dynamic)null;
                DataTable dtResult = new DataTable();
                
                var queryResult = (from ph in query
                                    join pd in context.InvStockAdjustmentHeaders on ph.InvStockAdjustmentHeaderID
                                        equals pd.InvStockAdjustmentHeaderID
                                    join pm in context.InvProductMasters on ph.ProductID equals pm.InvProductMasterID
                                    join um in context.UnitOfMeasures on ph.UnitOfMeasureID equals um.UnitOfMeasureID
                                    join d in context.InvDepartments on pm.DepartmentID equals d.InvDepartmentID
                                    join c in context.InvCategories on pm.CategoryID equals c.InvCategoryID
                                    join s in context.InvSubCategories on pm.SubCategoryID equals s.InvSubCategoryID
                                    join l in context.Locations on ph.LocationID equals l.LocationID
                                    where ph.DocumentStatus == 1 && ph.DocumentID == pd.DocumentID
                                    group new {ph, pd, pm, um, d, c, s, l} by new
                                        {
                                            ph.DocumentDate,
                                            pd.DocumentNo,
                                            pd.Remark,
                                            pm.ProductCode,
                                            pm.BarCode,
                                            pm.ProductName,
                                            um.UnitOfMeasureName,
                                            d.DepartmentCode,
                                            d.DepartmentName,
                                            c.CategoryCode,
                                            c.CategoryName,
                                            s.SubCategoryCode,
                                            s.SubCategoryName,
                                            ph.CostPrice,
                                            ph.CurrentQty,
                                            ph.OrderQty,
                                            l.LocationCode,
                                            l.LocationName
                                        }
                                    into g
                                    select new
                                        {
                                            FieldString1 = EntityFunctions.TruncateTime(g.Key.DocumentDate),
                                            FieldString2 = g.Key.LocationCode + " - " + g.Key.LocationName,
                                            FieldString3 = g.Key.DepartmentCode + " - " + g.Key.DepartmentName,
                                            FieldString4 = g.Key.CategoryCode + " - " + g.Key.CategoryName,
                                            FieldString5 = g.Key.SubCategoryCode + " - " + g.Key.SubCategoryName,
                                            FieldString6 = g.Key.ProductCode,
                                            FieldString7 = g.Key.ProductName,
                                            FieldString8 = g.Key.UnitOfMeasureName,
                                            FieldString9 = g.Key.Remark,
                                            FieldDecimal1 =
                                        (stockAdjustmentMode == 2
                                            ? g.Sum(x => x.ph.ShortageQty)
                                            : g.Sum(x => x.ph.ExcessQty)),
                                            FieldDecimal2 =
                                        (g.Key.CurrentQty != 0
                                            ? ((g.Sum(x => x.ph.OrderQty)/g.Key.CurrentQty)*100)
                                            : 0)
                                        }).ToArray();

                return queryResult.ToDataTable();
            }
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AutoGenerateInfo autoGeneratePurchaseInfo = new AutoGenerateInfo();
                autoGeneratePurchaseInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmStockAdjustment");
                int documentID = autoGeneratePurchaseInfo.DocumentID;
                IQueryable qryResult;
                
                qryResult = context.InvStockAdjustmentDetails.Where("DocumentStatus == @0 AND DocumentId==@1", 1, documentID);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "ProductID":
                        qryResult = qryResult.Join(context.InvProductMasters, reportDataStruct.DbColumnName.Trim(),
                                                   "InvProductMasterID", "new(inner.ProductCode)")
                                             .OrderBy("ProductCode")
                                             .GroupBy("new(ProductCode)", "new(ProductCode)")
                                             .Select("Key.ProductCode");
                        break;
                    case "DepartmentID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.DepartmentID)")
                                             .OrderBy("DepartmentID")
                                             .GroupBy("new(DepartmentID)", "new(DepartmentID)")
                                             .Select("new(Key.DepartmentID)");
                        qryResult = qryResult.Join(context.InvDepartments, "DepartmentID", "InvDepartmentID",
                                                   "new(inner.DepartmentCode)")
                                             .OrderBy("DepartmentCode")
                                             .GroupBy("new(DepartmentCode)", "new(DepartmentCode)")
                                             .Select("Key.DepartmentCode");
                        break;
                    case "CategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.CategoryID)")
                                             .OrderBy("CategoryID")
                                             .GroupBy("new(CategoryID)", "new(CategoryID)")
                                             .Select("new(Key.CategoryID)");
                        qryResult = qryResult.Join(context.InvCategories, "CategoryID", "InvCategoryID",
                                                   "new(inner.CategoryCode)")
                                             .OrderBy("CategoryCode")
                                             .GroupBy("new(CategoryCode)", "new(CategoryCode)")
                                             .Select("Key.CategoryCode");
                        break;
                    case "SubCategoryID":
                        qryResult = qryResult.Join(context.InvProductMasters, "ProductID", "InvProductMasterID",
                                                   "new(inner.InvProductMasterID, inner.SubCategoryID)")
                                             .OrderBy("SubCategoryID")
                                             .GroupBy("new(SubCategoryID)", "new(SubCategoryID)")
                                             .Select("new(Key.SubCategoryID)");
                        qryResult = qryResult.Join(context.InvSubCategories, "SubCategoryID", "InvSubCategoryID",
                                                   "new(inner.SubCategoryCode)")
                                             .OrderBy("SubCategoryCode")
                                             .GroupBy("new(SubCategoryCode)", "new(SubCategoryCode)")
                                             .Select("Key.SubCategoryCode");
                        break;
                    case "UnitOfMeasureID":
                        qryResult = qryResult.Join(context.UnitOfMeasures, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(), "new(inner.UnitOfMeasureName)")
                                             .OrderBy("UnitOfMeasureName")
                                             .GroupBy("new(UnitOfMeasureName)", "new(UnitOfMeasureName)")
                                             .Select("Key.UnitOfMeasureName");
                        break;
                    case "LocationID":
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)").
                                              OrderBy("LocationCode")
                                             .GroupBy("new(LocationCode)", "new(LocationCode)")
                                             .Select("Key.LocationCode");
                        break;
                    default:
                        qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                        break;
                }

                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();
                if (reportDataStruct.ValueDataType.Equals(typeof (bool)))
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
        }


        #region Basket Analysis Report
        
        public DataTable GetBasketAnalysisValueRange(int RangeType=1)
       
        {
            DataTable dt = new DataTable();
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (from d in context.InvBasketAnalysisValueRanges
                             where d.RangeType==RangeType
                             orderby d.RangeFrom
                             select new
                                 {
                                     d.RangeFrom,
                                     d.RangeTo
                                 });
                dt = query.ToDataTable();
            }
            return dt;
        }

        public bool ClearBasketAnalysisTempFiles()
        {
            try
            {
                CommonService.ExecuteSqlstatement("DELETE FROM InvBasketAnalysisValueRangeTemp WHERE UserName='" + Common.LoggedUser + "' AND PcName='" + Common.LoggedPcName + "'");
                CommonService.ExecuteSqlstatement("DELETE FROM InvBasketAnalysisSelectedLocationsTemp WHERE UserName='" + Common.LoggedUser + "' AND PcName='" + Common.LoggedPcName + "'");
                
                return true;
            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "InvReportService", Logger.logtype.ErrorLog, Common.LoggedLocationID);

                return false;
            }
        }

        public void AddBasketAnalysisTempValueRange(InvBasketAnalysisValueRangeTemp invBasketAnalysisValueRange)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.InvBasketAnalysisValueRangeTemps.Add(invBasketAnalysisValueRange);
                context.SaveChanges();
            }
        }

        public void AddBasketAnalysisTempLocation(InvBasketAnalysisSelectedLocationsTemp invBasketAnalysisSelectedLocationsTemp)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                context.InvBasketAnalysisSelectedLocationsTemps.Add(invBasketAnalysisSelectedLocationsTemp);
                context.SaveChanges();
            }
        }

        public DataTable GetBasketAnalysisReport(bool IsLocationWise, bool IsExcludeExchange, bool IsExcludeGVSales, bool IsDayWise, int CustomerType, DateTime DateFrom, DateTime DateTo, bool IsDay1, bool IsDay2, bool IsDay3, bool IsDay4, bool IsDay5, bool IsDay6, bool IsDay7, int RangeType)
        {
            try
            {

                DataTable dt = new DataTable();

                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SysConn"].ConnectionString))
                {
                    
                        try
                        {
                            sqlConn.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = sqlConn;
                            cmd.CommandText = "spRBasketAnalysisReport";
                            cmd.CommandType = CommandType.StoredProcedure;

                            
                           

                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsLocationWise",IsLocationWise));                   
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsExcludeExchange",IsExcludeExchange));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsExcludeGVSales",IsExcludeGVSales));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDayWise",IsDayWise));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@CustomerType",CustomerType));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@UserName",Common.LoggedUser));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@PCName", Common.LoggedPcName));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@DateFrom",DateFrom));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@DateTo",DateTo));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay1",IsDay1));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay2",IsDay2));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay3",IsDay3));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay4",IsDay4));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay5",IsDay5));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay6",IsDay6));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@IsDay7", IsDay7));
                            cmd.Parameters.Add(CommonService.AddSqlParameter("@RangeType", RangeType));
                            

                            cmd.CommandTimeout = 300;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);

                            da.Fill(dt);
                            dt.TableName = "BasketAnalysis";
                            return dt;
                        }

                        catch (Exception ex)
                        {
                            return null;
                        }
                        finally
                        {
                            if(sqlConn.State==ConnectionState.Open)
                                sqlConn.Close();
                        }
                    


                }


                return dt;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex, MethodInfo.GetCurrentMethod().Name.ToString(), "InvReportService", Logger.logtype.ErrorLog, Common.LoggedLocationID);

                return null;
            }
        }

        #endregion


    }
}
