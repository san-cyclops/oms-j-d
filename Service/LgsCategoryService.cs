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
    public class LgsCategoryService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code  
        /// </summary>
        public string GetNewCode(string formName, long lgsDepartmentID, string lgsDepartmentCode, bool isDependLgsSubCategory)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
                if (isDependLgsSubCategory)
                    GetNewCode = context.LgsCategories.Max(c => c.CategoryCode);
                else
                    GetNewCode = context.LgsCategories.Where(c => c.LgsCategoryID != 1).Max(c => c.CategoryCode);
            }
            else
            {
                GetNewCode = context.LgsCategories.Where(c => c.LgsDepartmentID == lgsDepartmentID).Max(c => c.CategoryCode);
                preFix = lgsDepartmentCode + preFix;
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
        /// Get active categories
        /// </summary>
        /// <returns></returns>
        public List<LgsCategory> GetAllActiveLgsCategories(bool isDependLgsSubCategory)
        {
            if (isDependLgsSubCategory)
                return context.LgsCategories.Where(c => c.IsDelete.Equals(false)).ToList();
            else
                return context.LgsCategories.Where(c => c.IsDelete.Equals(false) && c.LgsCategoryID != 1).ToList();
        }

        /// <summary>
        /// Get all category details 
        /// </summary>
        /// <returns></returns>
        public List<LgsCategory> GetAllLgsCategories(bool isDependLgsSubCategory)
        {
            if (isDependLgsSubCategory)
                return context.LgsCategories.ToList();
            else
                return context.LgsCategories.Where(c => c.LgsCategoryID != 1).ToList();
        }

        /// <summary>
        /// Get category codes to auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsCategoryCodes(bool isDependLgsSubCategory)
        {
            List<string> lgsCategoryCodesList = new List<string>();
            if (isDependLgsSubCategory)
                lgsCategoryCodesList = context.LgsCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.CategoryCode).ToList();
            else
                lgsCategoryCodesList = context.LgsCategories.Where(v => v.IsDelete.Equals(false) && v.LgsCategoryID != 1).Select(v => v.CategoryCode).ToList();

            string[] lgsCategoryCodes = lgsCategoryCodesList.ToArray();
            return lgsCategoryCodes;
        }

        /// <summary>
        /// Get category names to auto complete
        /// </summary>
        /// <param name="isDependLgsSubCategory"></param>
        /// <returns></returns>
        public string[] GetAllLgsCategoryNames(bool isDependLgsSubCategory)
        {
            List<string> lgsCategoryNamesList = new List<string>();
            if (isDependLgsSubCategory)
                lgsCategoryNamesList = context.LgsCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.CategoryName).ToList();
            else
                lgsCategoryNamesList = context.LgsCategories.Where(v => v.IsDelete.Equals(false) && v.LgsCategoryID != 1).Select(v => v.CategoryName).ToList();

            string[] lgsCategoryNames = lgsCategoryNamesList.ToArray();
            return lgsCategoryNames;
        }

        /// <summary>
        /// Get category by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public LgsCategory GetLgsCategoryByCode(string lgsCategoryCode, bool isDependLgsSubCategory)
        {
            if (isDependLgsSubCategory)
                return context.LgsCategories.Where(c => c.CategoryCode == lgsCategoryCode && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsCategories.Where(c => c.CategoryCode == lgsCategoryCode && c.LgsCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public LgsCategory GetLgsCategoryByID(long lgsCategoryID, bool isDependLgsSubCategory)
        {
            if (isDependLgsSubCategory)
                return context.LgsCategories.Where(c => c.LgsCategoryID == lgsCategoryID && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsCategories.Where(c => c.LgsCategoryID == lgsCategoryID && c.LgsCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public DataTable GetAllLgsDepartmentWiseCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsCategories.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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

            var queryResult = (from lc in query
                               join cm in context.LgsDepartments on lc.LgsDepartmentID equals cm.LgsDepartmentID
                               select new
                               {
                                   FieldString1 = lc.CategoryCode,
                                   FieldString2 = lc.CategoryName,
                                   FieldString3 = cm.DepartmentCode + " - " + cm.DepartmentName,
                                   FieldString4 = lc.Remark
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

        public DataTable GetAllLgsDepartmentWiseCategoryDataTable()
        {
            //DataTable tblCategroy = new DataTable();

            var query = (from lc in context.LgsCategories
                      join cm in context.LgsDepartments on lc.LgsDepartmentID equals cm.LgsDepartmentID
                      select new
                      {
                          FieldString1 = lc.CategoryCode,
                          FieldString2 = lc.CategoryName,
                          FieldString3 = cm.DepartmentName,
                          FieldString4 = lc.Remark
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


            //return tblCategroy = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        public DataTable GetAllLgsCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsCategories.Where(c => c.IsDelete == false && c.LgsCategoryID != 1);
                //.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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

            var queryResult = (from lc in query
                               select new
                               {
                                   FieldString1 = lc.CategoryCode,
                                   FieldString2 = lc.CategoryName,
                                   FieldString3 = lc.Remark
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

        public DataTable GetAllLgsCategoryDataTable()
        {
            //DataTable tblCategroy = new DataTable();

            var query = (from lc in context.LgsCategories
                         where lc.IsDelete == false && lc.LgsCategoryID != 1
                      select new
                      {
                          FieldString1 = lc.CategoryCode,
                          FieldString2 = lc.CategoryName,
                          FieldString3 = lc.Remark
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


            //return tblCategroy = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsCategories.Where(c => c.IsDelete == false && c.LgsCategoryID != 1);
                                          //   .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsDepartmentID":
                    qryResult = qryResult.Join(context.LgsDepartments, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.DepartmentName)")
                                .GroupBy("new(DepartmentName)", "new(DepartmentName)")
                                .OrderBy("Key.DepartmentName")
                                .Select("Key.DepartmentName");
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
        /// Save new category
        /// </summary>
        /// <param name="lgsCategory"></param>
        public void AddLgsCategory(LgsCategory lgsCategory)
        {
            context.LgsCategories.Add(lgsCategory);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="existingLgsCategory"></param>
        public void UpdateLgsCategory(LgsCategory existingLgsCategory)
        {
            existingLgsCategory.ModifiedUser = Common.LoggedUser;
            existingLgsCategory.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingLgsCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete existing category
        /// </summary>
        /// <param name="existingLgsCategory"></param>
        public void DeleteLgsCategory(LgsCategory existingLgsCategory)
        {
            existingLgsCategory.IsDelete = true;
            this.context.Entry(existingLgsCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get category by Name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public LgsCategory GetLgsCategoryByName(string lgsCategoryName, bool isDependLgsSubCategory)
        {
            if (isDependLgsSubCategory)
                return context.LgsCategories.Where(c => c.CategoryName == lgsCategoryName && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.LgsCategories.Where(c => c.CategoryName == lgsCategoryName && c.LgsCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public DataTable GetAllActiveLgsCategoriesDataTable(bool isDependSubCategory) 
        {
            DataTable tblCategory = new DataTable();

            if (isDependSubCategory)
            {
                var query = from d in context.LgsCategories
                            join c in context.LgsDepartments on d.LgsDepartmentID equals c.LgsDepartmentID
                            where d.IsDelete.Equals(false)
                            select new
                            {
                                c.DepartmentCode,
                                d.CategoryCode,
                                d.CategoryName,
                                d.Remark,
                            };
                return tblCategory = Common.LINQToDataTable(query);
            }
            else
            {
                var query = from d in context.LgsCategories
                            where d.IsDelete.Equals(false) && d.LgsCategoryID != 1
                            select new
                            {
                                d.CategoryCode,
                                d.CategoryName,
                                d.Remark,
                            };
                return tblCategory = Common.LINQToDataTable(query);
            }
        }


        #endregion
    }
}
