using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using Utility;
using System.Data;
using System.Data.Entity;
using EntityFramework.Extensions;
namespace Service
{

    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class InvCategoryService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code  
        /// </summary>
        public string GetNewCode(string formName, long invDepartmentID, string invDepartmentCode, bool isDependSubCategory)
        {

            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
               if(isDependSubCategory)
                    GetNewCode = context.InvCategories.Max(c => c.CategoryCode);
               else
                   GetNewCode = context.InvCategories.Where(c => c.InvCategoryID!=1).Max(c => c.CategoryCode);


            }
            else
            {
                GetNewCode = context.InvCategories.Where(c => c.InvDepartmentID==invDepartmentID).Max(c => c.CategoryCode);
                preFix = invDepartmentCode + preFix;
               
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
        public List<InvCategory> GetAllActiveInvCategories(bool isDependSubCategory)
        {
            if (isDependSubCategory)
                return context.InvCategories.Where(c => c.IsDelete.Equals(false)).ToList();
            else
                return context.InvCategories.Where(c => c.IsDelete.Equals(false) && c.InvCategoryID!=1).ToList();

        }

        //TO data transfe
        public DataTable GetActiveInvCategoriesDataTable()
        {
            DataTable tblCategory = new DataTable();
            var query = from d in context.InvCategories where d.DataTransfer.Equals(1) select new
            { d.InvCategoryID, d.InvDepartmentID, d.CategoryCode, d.CategoryName, d.Remark, d.IsDelete, 
                d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
            return tblCategory =query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvCategories.SqlQuery("select InvCategoryID,InvDepartmentID,CategoryCode,CategoryName,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from InvCategory").ToDataTable();

        }

        /// <summary>
        /// Get active categories
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveInvCategoriesDataTable(bool isDependSubCategory) 
        {
            DataTable tblCategory = new DataTable();

            if (isDependSubCategory)
            {
                var query = from d in context.InvCategories 
                            join c in context.InvDepartments on d.InvDepartmentID equals c.InvDepartmentID
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
                var query = from d in context.InvCategories 
                            where d.IsDelete.Equals(false) && d.InvCategoryID != 1 
                            select new 
                            {  
                                d.CategoryCode, 
                                d.CategoryName, 
                                d.Remark, 
                            };
                return tblCategory = Common.LINQToDataTable(query);
            }
        }

        /// <summary>
        /// Get all category details 
        /// </summary>
        /// <returns></returns>
        public List<InvCategory> GetAllInvCategories(bool isDependSubCategory)
        {
            if (isDependSubCategory)
                return context.InvCategories.ToList();
            else
                return context.InvCategories.Where(c => c.InvCategoryID!=1).ToList();

        }

        public DataTable GetAllDepartmentWiseCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvCategories.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

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

            var queryResult = (from lc in query
                               join cm in context.InvDepartments on lc.InvDepartmentID equals cm.InvDepartmentID
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

        public DataTable GetAllDepartmentWiseCategoryDataTable()
        {
            //DataTable tblCategroy = new DataTable();

            var query = (from lc in context.InvCategories
                      join cm in context.InvDepartments on lc.InvDepartmentID equals cm.InvDepartmentID
                      select new
                      {
                          FieldString1 = lc.CategoryCode,
                          FieldString2 = lc.CategoryName,
                          FieldString3 = cm.DepartmentCode + " - " + cm.DepartmentName,
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

        public DataTable GetAllCategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvCategories.Where(c => c.IsDelete == false && c.InvCategoryID != 1);
                //"IsDelete=false &&   " 
                //);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

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

        public DataTable GetAllCategoryDataTable()
        {
            //DataTable tblCategroy = new DataTable();

            var query = (from lc in context.InvCategories where lc.IsDelete == false && lc.InvCategoryID != 1
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
            IQueryable qryResult = context.InvCategories.Where(c => c.IsDelete == false && c.InvCategoryID != 1);
                           // .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "InvDepartmentID":
                    qryResult = qryResult.Join(context.InvDepartments, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.DepartmentName)")
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
        /// Get Category Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvCategoryCodes(bool isDependSubCategory)
        {
            List<string> invCategoryCodesList =new List<string>();
            if(isDependSubCategory)
                invCategoryCodesList=context.InvCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.CategoryCode).ToList();
            else
                invCategoryCodesList=context.InvCategories.Where(v => v.IsDelete.Equals(false) && v.InvCategoryID!=1).Select(v => v.CategoryCode).ToList();

            string[] invCategoryCodes = invCategoryCodesList.ToArray();
            return invCategoryCodes;
        }

        public string[] GetInvCategoryNames(bool isDependSubCategory)
        {
            List<string> invCategoryNamesList = new List<string>();
            if (isDependSubCategory)
                invCategoryNamesList = context.InvCategories.Where(v => v.IsDelete.Equals(false)).Select(v => v.CategoryName).ToList();
            else
                invCategoryNamesList = context.InvCategories.Where(v => v.IsDelete.Equals(false) && v.InvCategoryID != 1).Select(v => v.CategoryName).ToList();

            string[] invCategoryNames = invCategoryNamesList.ToArray();
            return invCategoryNames;
        }

        /// <summary>
        /// Get category by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvCategory GetInvCategoryByCode(string invCategoryCode, bool isDependSubCategory)
        {
            if (isDependSubCategory)
                return context.InvCategories.Where(c => c.CategoryCode == invCategoryCode && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvCategories.Where(c => c.CategoryCode == invCategoryCode && c.InvCategoryID!=1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public InvCategory GetInvCategoryByID(long invCategoryID, bool isDependSubCategory)
        {
            if (isDependSubCategory)
                return context.InvCategories.Where(c => c.InvCategoryID == invCategoryID && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvCategories.Where(c => c.InvCategoryID == invCategoryID && c.InvCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        /// <summary>
        /// Save new category
        /// </summary>
        /// <param name="invCategory"></param>
        public void AddInvCategory(InvCategory invCategory)
        {
            context.InvCategories.Add(invCategory);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="existingInvCategory"></param>
        public void UpdateInvCategory(InvCategory existingInvCategory)
        {
            existingInvCategory.ModifiedUser = Common.LoggedUser;
            existingInvCategory.ModifiedDate = Common.GetSystemDateWithTime();
            existingInvCategory.DataTransfer = 0;
            this.context.Entry(existingInvCategory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateInvCategory(int datatransfer)
        {
            context.InvCategories.Update(ps => ps.DataTransfer.Equals(1), ps => new InvCategory { DataTransfer = datatransfer });
        }

        public void UpdateInvCategoryDTSelect(int datatransfer)
        {
            context.InvCategories.Update(ps => ps.DataTransfer.Equals(0), ps => new InvCategory { DataTransfer = datatransfer });

        }

        /// <summary>
        /// Get category by Name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvCategory GetInvCategoryByName(string invCategoryName, bool isDependSubCategory)
        {
            if (isDependSubCategory)
                return context.InvCategories.Where(c => c.CategoryName == invCategoryName && c.IsDelete.Equals(false)).FirstOrDefault();
            else
                return context.InvCategories.Where(c => c.CategoryName == invCategoryName && c.InvCategoryID != 1 && c.IsDelete.Equals(false)).FirstOrDefault();

        }


        public InvCategory GetInvCategoryByDepartment(long invDepartmentId)
        {
           return context.InvCategories.Where(c => c.InvDepartmentID == invDepartmentId && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public string[] GetAllCategoryCodes()
        {
            List<string> CategoryCodeList = context.InvCategories.Where(c => c.IsDelete.Equals(false) && c.InvCategoryID != 1).Select(c => c.CategoryCode).ToList();
            return CategoryCodeList.ToArray();
        }

        public string[] GetAllCategoryNames()
        {
            List<string> CategoryNameList = context.InvCategories.Where(c => c.IsDelete.Equals(false) && c.InvCategoryID != 1).OrderBy(c=> c.CategoryName).Select(c => c.CategoryName).ToList();
            return CategoryNameList.ToArray();
        }

        public long[] GetInvCategoryIdRangeByCategoryNames(string nameFrom, string nameTo)
        {
            return context.InvCategories.Where(d => d.IsDelete.Equals(false) && d.CategoryName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.CategoryName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.InvCategoryID).ToArray();
        }

        public List<InvCategory> GetCategorytList() 
        {
            return context.InvCategories.Where(l => l.IsDelete.Equals(false) && l.InvCategoryID != 1).OrderBy(l => l.InvCategoryID).ToList();
        }

        #endregion
    }
}
