using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoreLinq;
namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class InvSubCategoryService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code 
        /// </summary>
        //public string GetNewCode(string formName, long invCategoryID, string invCategoryCode)
        //{

        //    AutoGenerateInfo autoGenerateInfo;
        //    autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
        //    string getNewCode = string.Empty;
        //    string preFix = autoGenerateInfo.Prefix;
        //    int codeLength = autoGenerateInfo.CodeLength;

        //    string GetNewCode = string.Empty;

        //    if (!autoGenerateInfo.IsDependCode)
        //    {
        //        GetNewCode = context.InvSubCategories.Max(s => s.SubCategoryCode);

        //    }
        //    else
        //    {
        //        GetNewCode = context.InvSubCategories.Where(s => s.InvCategoryID == invCategoryID).Max(s => s.SubCategoryCode);
        //        preFix = invCategoryCode + preFix;

        //    }


        //    if (GetNewCode != null)
        //    { GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length); }
        //    else
        //    { GetNewCode = "0"; }

        //    GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
        //    GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
        //    return GetNewCode;


        //}

        public string GetNewCode(string formName, long invCategoryID, string invCategoryCode, bool isDependSubCategory2)
        {

            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
                if (isDependSubCategory2)
                    GetNewCode = context.InvSubCategories.Max(c => c.SubCategoryCode);
                else
                    GetNewCode = context.InvSubCategories.Where(c => c.InvSubCategoryID != 1).Max(c => c.SubCategoryCode);


            }
            else
            {
                GetNewCode = context.InvSubCategories.Where(c => c.InvCategoryID == invCategoryID).Max(c => c.SubCategoryCode);
                preFix = invCategoryCode + preFix;

            }


            if (GetNewCode != null)
            { GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length); }
            else
            { GetNewCode = "0"; }

            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;


        }



        /// <summary>
        /// Get all Sub category details 
        /// </summary>
        /// <returns></returns>
        public List<InvSubCategory> GetAllInvSubCategories( bool isDependSubCategory2)
        {
            if (isDependSubCategory2)
                return context.InvSubCategories.ToList();
            else
                return context.InvSubCategories.Where(c=> c.InvSubCategoryID!=1).ToList();

        }


        //To data transfer
        public DataTable GetInvSubCategoriesDataTable()
        {
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvSubCategories.SqlQuery("select InvSubCategoryID,InvCategoryID,SubCategoryCode,SubCategoryName,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from InvSubCategory").ToDataTable();
            DataTable tblCategory = new DataTable();
            var query = from d in context.InvSubCategories
                        where d.DataTransfer.Equals(1)
                        select new
                        {
                            d.InvSubCategoryID,
                            d.InvCategoryID,
                            d.SubCategoryCode,
                            d.SubCategoryName,
                            d.Remark,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer
                        };
            return tblCategory = query.ToDataTable();
        }

        /// <summary>
        /// get all sub category to datatable for transfer
        /// </summary>
        /// <param name="isDependSubCategory2"></param>
        /// <returns></returns>
        public DataTable GetSubCategoriesDataTable(bool isDependSubCategory2) 
        {
            DataTable dt = new DataTable();
            if (isDependSubCategory2)
            {
                var query = from sc in context.InvSubCategories 
                            join c in context.InvCategories on sc.InvCategoryID equals c.InvCategoryID
                            where sc.IsDelete.Equals(false) 
                            select new 
                            {  
                                c.CategoryCode,
                                sc.SubCategoryCode, 
                                sc.SubCategoryName, 
                                sc.Remark
                            };
                return dt = Common.LINQToDataTable(query);
            }
            else
            {
                var query = from d in context.InvSubCategories 
                            where d.IsDelete.Equals(false) && d.InvSubCategoryID != 1 
                            select new 
                            { 
                                d.SubCategoryCode, 
                                d.SubCategoryName, 
                                d.Remark
                            };
                return dt = Common.LINQToDataTable(query);
            }
        }

        public InvSubCategory GetInvSubCategoryByCategory(long CategoryId)
        {
            return context.InvSubCategories.Where(c => c.IsDelete.Equals(false) && c.InvCategoryID == CategoryId).FirstOrDefault();
        }

        /// <summary>
        /// Get Sub Category Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvSubCategoryCodes(bool isDependSubCategory2)
        {
            List<string> invSubCategoryCodesList=new List<string>();
            if(isDependSubCategory2)
                invSubCategoryCodesList = context.InvSubCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.SubCategoryCode).ToList();
            else
                invSubCategoryCodesList = context.InvSubCategories.Where(v => v.IsDelete.Equals(false) && v.InvSubCategoryID != 1).Select(v => v.SubCategoryCode).ToList();

            string[] invSubCategoryCodes = invSubCategoryCodesList.ToArray();
            return invSubCategoryCodes;
        }

        public string[] GetInvSubCategoryNames(bool isDependSubCategory2)
        {
            List<string> invSubCategoryNamesList = new List<string>();
            if (isDependSubCategory2)
                invSubCategoryNamesList = context.InvSubCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.SubCategoryName).ToList();
            else
                invSubCategoryNamesList = context.InvSubCategories.Where(v => v.IsDelete.Equals(false) && v.InvSubCategoryID != 1).Select(v => v.SubCategoryName).ToList();

            string[] invSubCategoryNames = invSubCategoryNamesList.ToArray();
            return invSubCategoryNames;
        }

        /// <summary>
        /// Get Sub category by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvSubCategory GetInvSubCategoryByCode(string invSubCategoryCode, bool isDependSubCategory2)
        {
            if (isDependSubCategory2)
                return context.InvSubCategories.Where(c => c.SubCategoryCode == invSubCategoryCode && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvSubCategories.Where(c => c.SubCategoryCode == invSubCategoryCode && c.InvSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public InvSubCategory GetInvSubCategoryByID(long invSubCategoryID, bool isDependSubCategory2)
        {
            if (isDependSubCategory2)
                return context.InvSubCategories.Where(c => c.InvSubCategoryID == invSubCategoryID && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvSubCategories.Where(c => c.InvSubCategoryID == invSubCategoryID && c.InvSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        /// <summary>
        /// Save new Sub category
        /// </summary>
        /// <param name="invSubCategory"></param>
        public void AddInvSubCategory(InvSubCategory invSubCategory)
        {
            context.InvSubCategories.Add(invSubCategory);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Sub category
        /// </summary>
        /// <param name="existingInvSubCategory"></param>
        public void UpdateInvSubCategory(InvSubCategory existingInvSubCategory)
        {
            existingInvSubCategory.DataTransfer = 0;
            this.context.Entry(existingInvSubCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateInvSubCategory(int datatransfer)
        {
            context.InvSubCategories.Update(ps => ps.DataTransfer.Equals(1), ps => new InvSubCategory { DataTransfer = datatransfer });
        }

        public void UpdateInvSubCategoryDTSelect(int datatransfer)
        {
            context.InvSubCategories.Update(ps => ps.DataTransfer.Equals(0), ps => new InvSubCategory { DataTransfer = datatransfer });
        }
        /// <summary>
        /// Get Sub Category by Name
        /// </summary>
        /// <param name="invSubCategoryName"></param>
        /// <returns></returns>
        public InvSubCategory GetInvSubCategoryByName(string invSubCategoryName, bool isDependSubCategory2)
        {
            if (isDependSubCategory2)
                return context.InvSubCategories.Where(c => c.SubCategoryName == invSubCategoryName && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvSubCategories.Where(c => c.SubCategoryName == invSubCategoryName && c.InvSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public DataTable GetAllCategoryWiseSubCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvSubCategories.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query =  query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from sc in query
                               join cg in context.InvCategories on sc.InvCategoryID equals cg.InvCategoryID
                               select new
                               {
                                   FieldString1 = sc.SubCategoryCode,
                                   FieldString2 = sc.SubCategoryName,
                                   FieldString3 = cg.CategoryCode + " - " + cg.CategoryName,
                                   FieldString4 = sc.Remark
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetAllCategoryWiseSubCategoryDataTable()
        {
            //DataTable tblSubCategroy = new DataTable();

            var query = (from sc in context.InvSubCategories
                      join cg in context.InvCategories on sc.InvCategoryID equals cg.InvCategoryID
                      select new
                      {
                          FieldString1 = sc.SubCategoryCode,
                          FieldString2 = sc.SubCategoryName,
                          FieldString3 = cg.CategoryCode + " - " + cg.CategoryName,
                          FieldString4 = sc.Remark
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblSubCategroy = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        public DataTable GetAllSubCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvSubCategories.Where(c => c.IsDelete == false && c.InvSubCategoryID != 1);
                //.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from sc in query
                               select new
                               {
                                   FieldString1 = sc.SubCategoryCode,
                                   FieldString2 = sc.SubCategoryName,
                                   FieldString3 = sc.Remark
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetAllSubCategoryDataTable()
        {
            //DataTable tblSubCategroy = new DataTable();

            var query = (from sc in context.InvSubCategories
                         where sc.IsDelete == false && sc.InvSubCategoryID != 1
                      select new
                      {
                          FieldString1 = sc.SubCategoryCode,
                          FieldString2 = sc.SubCategoryName,
                          FieldString3 = sc.Remark
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblSubCategroy = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.InvSubCategories.Where(c => c.IsDelete == false && c.InvSubCategoryID != 1);
                                          //   .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "InvCategoryID":
                    qryResult = qryResult.Join(context.InvCategories, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CategoryName)")
                                .GroupBy("new(CategoryName)", "new(CategoryName)")
                                .OrderBy("Key.CategoryName")
                                .Select("Key.CategoryName");
                    break;
                default:
                    qryResult = qryResult.OrderBy(reportDataStruct.DbColumnName).Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResult)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }

        public string[] GetAllSubCategoryCodes()
        {
            List<string> SubCategoryCodeList = context.InvSubCategories.Where(c => c.IsDelete.Equals(false) && c.InvSubCategoryID != 1).Select(c => c.SubCategoryCode).ToList();
            return SubCategoryCodeList.ToArray();
        }

        public string[] GetAllSubCategoryNames()
        {
            List<string> SubCategoryNameList = context.InvSubCategories.Where(c => c.IsDelete.Equals(false) && c.InvSubCategoryID != 1).OrderBy(c => c.SubCategoryName).Select(c => c.SubCategoryName).ToList();
            return SubCategoryNameList.ToArray();
        }

        public long[] GetInvSubCategoryIdRangeBySubCategoryNames(string nameFrom, string nameTo)
        {
            return context.InvSubCategories.Where(d => d.IsDelete.Equals(false) && d.SubCategoryName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.SubCategoryName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.InvSubCategoryID).ToArray();
        }

        public List<InvSubCategory> GetSubCategorytList() 
        {
            return context.InvSubCategories.Where(l => l.IsDelete.Equals(false) && l.InvSubCategoryID != 1).OrderBy(l => l.InvSubCategoryID).ToList();
        }

        #endregion
    }
}
