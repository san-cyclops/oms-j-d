using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class LgsSubCategoryService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public string GetNewCode(string formName, long lgsCategoryID, string lgsCategoryCode, bool isDependLgsSubCategory2)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
                if (isDependLgsSubCategory2)
                    GetNewCode = context.LgsSubCategories.Max(c => c.SubCategoryCode);
                else
                    GetNewCode = context.LgsSubCategories.Where(c => c.LgsSubCategoryID != 1).Max(c => c.SubCategoryCode);
            }
            else
            {
                GetNewCode = context.LgsSubCategories.Where(c => c.LgsCategoryID == lgsCategoryID).Max(c => c.SubCategoryCode);
                preFix = lgsCategoryCode + preFix;
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
        public List<LgsSubCategory> GetAllLgsSubCategories(bool isDependLgsSubCategory2)
        {
            if (isDependLgsSubCategory2)
                return context.LgsSubCategories.ToList();
            else
                return context.LgsSubCategories.Where(c => c.LgsSubCategoryID != 1).ToList();
        }

        /// <summary>
        /// Get Sub Category Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSubCategoryCodes(bool isDependLgsSubCategory2)
        {
            List<string> lgsSubCategoryCodesList = new List<string>();
            if (isDependLgsSubCategory2)
                lgsSubCategoryCodesList = context.LgsSubCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.SubCategoryCode).ToList();
            else
                lgsSubCategoryCodesList = context.LgsSubCategories.Where(v => v.IsDelete.Equals(false) && v.LgsSubCategoryID != 1).Select(v => v.SubCategoryCode).ToList();

            string[] lgsSubCategoryCodes = lgsSubCategoryCodesList.ToArray();
            return lgsSubCategoryCodes;
        }

        public string[] GetAllLgsSubCategoryNames(bool isDependLgsSubCategory2)
        {
            List<string> lgsSubCategoryNamesList = new List<string>();
            if (isDependLgsSubCategory2)
                lgsSubCategoryNamesList = context.LgsSubCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.SubCategoryName).ToList();
            else
                lgsSubCategoryNamesList = context.LgsSubCategories.Where(v => v.IsDelete.Equals(false) && v.LgsSubCategoryID != 1).Select(v => v.SubCategoryName).ToList();

            string[] lgsSubCategoryNames = lgsSubCategoryNamesList.ToArray();
            return lgsSubCategoryNames;
        }

        public DataTable GetAllLgsCategoryWiseSubCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsSubCategories.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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
                               join cg in context.LgsCategories on sc.LgsCategoryID equals cg.LgsCategoryID
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

        public DataTable GetAllLgsCategoryWiseSubCategoryDataTable()
        {
            //DataTable tblSubCategroy = new DataTable();

            var query = (from sc in context.LgsSubCategories
                      join cg in context.LgsCategories on sc.LgsCategoryID equals cg.LgsCategoryID
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
                var query = from sc in context.LgsSubCategories
                            join c in context.LgsCategories on sc.LgsCategoryID equals c.LgsCategoryID
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
                var query = from d in context.LgsSubCategories
                            where d.IsDelete.Equals(false) && d.LgsSubCategoryID != 1
                            select new
                            {
                                d.SubCategoryCode,
                                d.SubCategoryName,
                                d.Remark
                            };
                return dt = Common.LINQToDataTable(query);
            }
        }

        public DataTable GetAllLgsSubCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsSubCategories.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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
                                   FieldString3 = sc.Remark,
                                  
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

        public DataTable GetAllLgsSubCategoryDataTable()
        {
            //DataTable tblSubCategroy = new DataTable();

            var query = (from sc in context.LgsSubCategories
                      select new
                      {
                          FieldString1 = sc.SubCategoryCode,
                          FieldString2 = sc.SubCategoryName,
                          FieldString3 = "",
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

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsSubCategories.Where(c => c.IsDelete == false && c.LgsSubCategoryID != 1);
                                           //  .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsCategoryID":
                    qryResult = qryResult.Join(context.LgsCategories, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CategoryName)")
                                .GroupBy("new(CategoryName)", "new(CategoryName)")
                                .OrderBy("CategoryName")
                                .Select("CategoryName");
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

        /// <summary>
        /// Get Sub category by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public LgsSubCategory GetLgsSubCategoryByCode(string lgsSubCategoryCode, bool isDependLgsSubCategory2)
        {
            if (isDependLgsSubCategory2)
                return context.LgsSubCategories.Where(c => c.SubCategoryCode == lgsSubCategoryCode && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsSubCategories.Where(c => c.SubCategoryCode == lgsSubCategoryCode && c.LgsSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        /// <summary>
        /// Get sub category by ID
        /// </summary>
        /// <param name="lgsSubCategoryID"></param>
        /// <param name="isDependLgsSubCategory2"></param>
        /// <returns></returns>
        public LgsSubCategory GetLgsSubCategoryByID(long lgsSubCategoryID, bool isDependLgsSubCategory2)
        {
            if (isDependLgsSubCategory2)
                return context.LgsSubCategories.Where(c => c.LgsSubCategoryID == lgsSubCategoryID && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsSubCategories.Where(c => c.LgsSubCategoryID == lgsSubCategoryID && c.LgsSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Save new Sub category
        /// </summary>
        /// <param name="lgsSubCategory"></param>
        public void AddLgsSubCategory(LgsSubCategory lgsSubCategory)
        {
            context.LgsSubCategories.Add(lgsSubCategory);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Sub category
        /// </summary>
        /// <param name="existingLgsSubCategory"></param>
        public void UpdateLgsSubCategory(LgsSubCategory existingLgsSubCategory)
        {
            existingLgsSubCategory.ModifiedUser = Common.LoggedUser;
            existingLgsSubCategory.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingLgsSubCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get Sub Category by Name
        /// </summary>
        /// <param name="lgsSubCategoryName"></param>
        /// <returns></returns>
        public LgsSubCategory GetLgsSubCategoryByName(string lgsSubCategoryName, bool isDependLgsSubCategory2)
        {
            if (isDependLgsSubCategory2)
                return context.LgsSubCategories.Where(c => c.SubCategoryName == lgsSubCategoryName && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsSubCategories.Where(c => c.SubCategoryName == lgsSubCategoryName && c.LgsSubCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Delete Sub category
        /// </summary>
        /// <param name="existingLgsSubCategory"></param>
        public void DeleteLgsSubCategory(LgsSubCategory existingLgsSubCategory)
        {
            existingLgsSubCategory.IsDelete = true;
            this.context.Entry(existingLgsSubCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        #endregion
    }
}
