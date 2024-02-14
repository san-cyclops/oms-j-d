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
using EntityFramework.Extensions;

namespace Service
{

    public class InvSubCategory2Service
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code 
        /// </summary>
        public string GetNewCode(string formName, long invSubCategoryID, string invSubCategoryCode)
        {

            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            string GetNewCode = string.Empty;

            if (!autoGenerateInfo.IsDependCode)
            {
                GetNewCode = context.InvSubCategories2.Max(s => s.SubCategory2Code);

            }
            else
            {
                GetNewCode = context.InvSubCategories2.Where(s => s.InvSubCategoryID == invSubCategoryID).Max(s => s.SubCategory2Code);
                preFix = invSubCategoryCode + preFix;

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
        public List<InvSubCategory2> GetAllInvSubCategories2()
        {
            return context.InvSubCategories2.ToList();
        }

        //To data transfer
        public DataTable GetAllInvSubCategories2DataTable()
        {
            DataTable dt = new DataTable();
            var query = from d in context.InvSubCategories2 where d.DataTransfer.Equals(1) select new { d.InvSubCategory2ID, d.InvSubCategoryID, d.SubCategory2Code, d.SubCategory2Name, d.Remark, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
            return dt = query.ToDataTable();

            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvSubCategories2.SqlQuery("select InvSubCategory2ID,InvSubCategoryID,SubCategory2Code,SubCategory2Name,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from InvSubCategory2").ToDataTable();
        }

        /// <summary>
        /// Get All sub category2 data table
        /// </summary>
        /// <returns></returns>
        public DataTable GetInvSubCategories2DataTable() 
        {
            DataTable dt = new DataTable();
            var query = from d in context.InvSubCategories2
                        where d.IsDelete.Equals(false) && d.InvSubCategory2ID != 1
                        select new 
                        { 
                            d.SubCategory2Code, 
                            d.SubCategory2Name, 
                            d.Remark
                        };
            return dt = Common.LINQToDataTable(query);
        }



        public InvSubCategory2 GetInvSubCategory2ByCategory(long SubCategory2Id)
        {
            return context.InvSubCategories2.Where(c => c.IsDelete.Equals(false) && c.InvSubCategoryID == SubCategory2Id).FirstOrDefault();
        }

        /// <summary>
        /// Get Sub Category Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvSubCategory2Codes()
        {
            List<string> invSubCategory2CodesList = context.InvSubCategories2.Where(v => v.IsDelete.Equals(false) && v.InvSubCategory2ID != 1).Select(v => v.SubCategory2Code).ToList();
            string[] invSubCategory2Codes = invSubCategory2CodesList.ToArray();
            return invSubCategory2Codes;
        }

        public string[] GetInvSubCategory2Names()
        {
            List<string> invSubCategory2NamesList = context.InvSubCategories2.Where(v => v.IsDelete.Equals(false) && v.InvSubCategory2ID != 1).OrderBy(v => v.SubCategory2Name).Select(v => v.SubCategory2Name).ToList();
            string[] invSubCategory2Names = invSubCategory2NamesList.ToArray();
            return invSubCategory2Names;
        }

        /// <summary>
        /// Get Sub category by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvSubCategory2 GetInvSubCategory2ByCode(string invSubCategory2Code)
        {
            //return context.InvSubCategories2.Where(c => c.SubCategory2Code == invSubCategory2Code && c.IsDelete.Equals(false)).FirstOrDefault();
            return context.InvSubCategories2.Where(c => c.SubCategory2Code == invSubCategory2Code && c.IsDelete.Equals(false) && c.InvSubCategory2ID != 1).FirstOrDefault();
        }

        public InvSubCategory2 GetInvSubCategory2ByID(long invSubCategory2ID)
        {
            return context.InvSubCategories2.Where(c => c.InvSubCategory2ID == invSubCategory2ID && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public InvSubCategory2 GetInvSubCategory2ByName(string invSubCategory2Name)
        {
            return context.InvSubCategories2.Where(c => c.SubCategory2Name == invSubCategory2Name && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public DataTable GetAllSubCategoryWiseSub2CategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvSubCategories2.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where( "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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
                               join cg in context.InvSubCategories on sc.InvSubCategoryID equals cg.InvSubCategoryID
                               select new
                               {
                                   FieldString1 = sc.SubCategory2Code,
                                   FieldString2 = sc.SubCategory2Name,
                                   FieldString3 = cg.SubCategoryCode + " " + cg.SubCategoryName,
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

        public DataTable GetAllSubCategoryWiseSub2CategoryDataTable()
        {
            //DataTable tblSubCategroy2 = new DataTable();

            var query = (from sc in context.InvSubCategories2
                      join cg in context.InvSubCategories on sc.InvSubCategoryID equals cg.InvSubCategoryID
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

        public DataTable GetAllSub2CategoryDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvSubCategories2.Where(c => c.IsDelete == false && c.InvSubCategory2ID != 1);
               // .Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim());}

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
            {querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");}

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

        public DataTable GetAllSub2CategoryDataTable()
        {
            //DataTable tblSubCategroy2 = new DataTable();

            var query = (from sc in context.InvSubCategories2 where sc.IsDelete == false && sc.InvSubCategory2ID != 1
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
            IQueryable qryResult = context.InvSubCategories2.Where(c => c.IsDelete == false && c.InvSubCategory2ID != 1);
                                            // .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "InvSubCategoryID":
                    qryResult = qryResult.Join(context.InvSubCategories, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.SubCategoryName)")
                                .GroupBy("new(SubCategoryName)", "new(SubCategoryName)")
                                .OrderBy("Key.SubCategoryName")
                                .Select("Key.SubCategoryName");
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
        /// Save new Sub category
        /// </summary>
        /// <param name="invSubCategory"></param>
        public void AddInvSubCategory2(InvSubCategory2 invSubCategory2)
        {
            context.InvSubCategories2.Add(invSubCategory2);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Sub category
        /// </summary>
        /// <param name="existingInvSubCategory"></param>
        public void UpdateInvSubCategory2(InvSubCategory2 existingInvSubCategory2)
        {
            existingInvSubCategory2.DataTransfer = 0;
            this.context.Entry(existingInvSubCategory2).State = EntityState.Modified;
            this.context.SaveChanges();
        }
        public void UpdateInvSubCategory2(int datatransfer)
        {
            context.InvSubCategories2.Update(ps => ps.DataTransfer.Equals(1), ps => new InvSubCategory2 { DataTransfer = datatransfer });
        }

        public void UpdateInvSubCategory2DTSelect(int datatransfer)
        {
            context.InvSubCategories2.Update(ps => ps.DataTransfer.Equals(0), ps => new InvSubCategory2 { DataTransfer = datatransfer });
        }

        public long[] GetInvSubCategory2IdRangeBySubCategory2Names(string nameFrom, string nameTo)
        {
            return context.InvSubCategories2.Where(d => d.IsDelete.Equals(false) && d.SubCategory2Name.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.SubCategory2Name.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.InvSubCategory2ID).ToArray();
        }


        public List<InvSubCategory2> GetSubCategory2List()  
        {
            return context.InvSubCategories2.Where(l => l.IsDelete.Equals(false) && l.InvSubCategory2ID != 1).OrderBy(l => l.InvSubCategory2ID).ToList();
        }

        #endregion
    }
}
