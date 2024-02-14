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
    public class LgsSubCategory2Service
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code 
        /// </summary>
        public string GetNewCode(string formName, long lgsSubCategoryID, string lgsSubCategoryCode)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
                GetNewCode = context.LgsSubCategories2.Max(s => s.SubCategory2Code);
            }
            else
            {
                GetNewCode = context.LgsSubCategories2.Where(s => s.LgsSubCategoryID == lgsSubCategoryID).Max(s => s.SubCategory2Code);
                preFix = lgsSubCategoryCode + preFix;
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
        public List<LgsSubCategory2> GetAllLgsSubCategories2()
        {
            return context.LgsSubCategories2.ToList();
        }

        /// <summary>
        /// Get Sub Category Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSubCategory2Codes()
        {
            List<string> lgsSubCategory2CodesList = context.LgsSubCategories2.Where(v => v.IsDelete.Equals(false) && v.LgsSubCategory2ID != 1).Select(v => v.SubCategory2Code).ToList();
            string[] lgsSubCategory2Codes = lgsSubCategory2CodesList.ToArray();
            return lgsSubCategory2Codes;
        }

        public string[] GetAllLgsSubCategory2Names()
        {
            List<string> lgsSubCategory2NamesList = context.LgsSubCategories2.Where(v => v.IsDelete.Equals(false) && v.LgsSubCategory2ID != 1).Select(v => v.SubCategory2Name).ToList();
            string[] lgsSubCategory2Names = lgsSubCategory2NamesList.ToArray();
            return lgsSubCategory2Names;
        }

        /// <summary>
        /// Get All sub category2 data table
        /// </summary>
        /// <returns></returns>
        public DataTable GetInvSubCategories2DataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.LgsSubCategories2
                        where d.IsDelete.Equals(false) && d.LgsSubCategory2ID != 1
                        select new
                        {
                            d.SubCategory2Code,
                            d.SubCategory2Name,
                            d.Remark
                        };
            return dt = Common.LINQToDataTable(query);
        }

        public DataTable GetAllLgsSubCategoryWiseSub2CategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsSubCategories2.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

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

            var queryResult = (from sc in query
                               join cg in context.LgsSubCategories on sc.LgsSubCategoryID equals cg.LgsSubCategoryID
                               select new
                               {
                                   FieldString1 = sc.SubCategory2Code,
                                   FieldString2 = sc.SubCategory2Name,
                                   FieldString3 = cg.SubCategoryCode + " - " + cg.SubCategoryName,
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

        public DataTable GetAllLgsSubCategoryWiseSub2CategoryDataTable()
        {
            //DataTable tblSubCategroy2 = new DataTable();

            var query = (from sc in context.LgsSubCategories2
                      join cg in context.LgsSubCategories on sc.LgsSubCategoryID equals cg.LgsSubCategoryID
                      select new
                      {
                          FieldString1 = sc.SubCategory2Code,
                          FieldString2 = sc.SubCategory2Name,
                          FieldString3 = cg.SubCategoryCode + " - " + cg.SubCategoryName,
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


            //return tblSubCategroy2 = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        public DataTable GetAllLgsSub2CategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsSubCategories2.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

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

            var queryResult = (from sc in query
                               select new
                               {
                                   FieldString1 = sc.SubCategory2Code,
                                   FieldString2 = sc.SubCategory2Name,
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

        public DataTable GetAllLgsSub2CategoryDataTable()
        {
            //DataTable tblSubCategroy2 = new DataTable();

            var query = (from sc in context.LgsSubCategories2
                      select new
                      {
                          FieldString1 = sc.SubCategory2Code,
                          FieldString2 = sc.SubCategory2Name,
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


            //return tblSubCategroy2 = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsSubCategories2.Where(c => c.IsDelete == false && c.LgsSubCategory2ID != 1).OrderBy(reportDataStruct.DbColumnName);
                                           //  .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSubCategoryID":
                    qryResult = qryResult.Join(context.LgsSubCategories, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategoryName)")
                                .GroupBy("new(SubCategoryName)", "new(SubCategoryName)")
                                .OrderBy("Key.SubCategoryName")
                                .Select("Key.SubCategoryName");
                    break;
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
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
        public LgsSubCategory2 GetLgsSubCategory2ByCode(string lgsSubCategory2Code)
        {
            return context.LgsSubCategories2.Where(c => c.SubCategory2Code == lgsSubCategory2Code && c.IsDelete.Equals(false) && c.LgsSubCategory2ID != 1).FirstOrDefault();
        }

        public LgsSubCategory2 GetLgsSubCategory2ByID(long lgsSubCategory2ID)
        {
            return context.LgsSubCategories2.Where(c => c.LgsSubCategory2ID == lgsSubCategory2ID && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LgsSubCategory2 GetLgsSubCategory2ByName(string lgsSubCategory2Name)
        {
            return context.LgsSubCategories2.Where(c => c.SubCategory2Name == lgsSubCategory2Name && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Save new Sub category
        /// </summary>
        /// <param name="lgsSubCategory2"></param>
        public void AddLgsSubCategory2(LgsSubCategory2 lgsSubCategory2)
        {
            context.LgsSubCategories2.Add(lgsSubCategory2);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Sub category
        /// </summary>
        /// <param name="existingLgsSubCategory2"></param>
        public void UpdateLgsSubCategory2(LgsSubCategory2 existingLgsSubCategory2)
        {
            existingLgsSubCategory2.ModifiedUser = Common.LoggedUser;
            existingLgsSubCategory2.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingLgsSubCategory2).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete existing Sub category
        /// </summary>
        /// <param name="existingLgsSubCategory2"></param>
        public void DeleteLgsSubCategory2(LgsSubCategory2 existingLgsSubCategory2)
        {
            existingLgsSubCategory2.IsDelete = true;
            this.context.Entry(existingLgsSubCategory2).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        #endregion
    }
}
