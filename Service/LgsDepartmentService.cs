using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
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
    public class LgsDepartmentService
    {
        ERPDbContext context = new ERPDbContext();

        #region methods

        /// <summary>
        /// Generate new code 
        /// </summary>
        public string GetNewCode(string preFix, int suffix, int codeLength, bool isDependCategory)
        {
            string GetNewCode = string.Empty;
            if (isDependCategory)
            { GetNewCode = context.LgsDepartments.Max(d => d.DepartmentCode); }
            else
            { GetNewCode = context.LgsDepartments.Where(d => d.LgsDepartmentID != 1).Max(d => d.DepartmentCode); }

            if (!string.IsNullOrEmpty(GetNewCode))
            { GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length); }
            else
            { GetNewCode = "0"; }

            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }

        /// <summary>
        /// Save new department
        /// </summary>
        /// <param name="lgsDepartment"></param>
        public void AddLgsDepartment(LgsDepartment lgsDepartment)
        {
            context.LgsDepartments.Add(lgsDepartment);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing department
        /// </summary>
        /// <param name="existingLgsDepartment"></param>
        public void UpdateLgsDepartment(LgsDepartment existingLgsDepartment)
        {
            existingLgsDepartment.ModifiedUser = Common.LoggedUser;
            existingLgsDepartment.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingLgsDepartment).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete existing department
        /// </summary>
        /// <param name="existingLgsDepartment"></param>
        public void DeleteLgsDepartment(LgsDepartment existingLgsDepartment)
        {
            existingLgsDepartment.IsDelete = true;
            this.context.Entry(existingLgsDepartment).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get Department by code
        /// </summary>
        /// <param name="LgsDepartmentCode"></param>
        /// <param name="isDependCategory"></param>
        /// <returns></returns>
        public LgsDepartment GetLgsDepartmentsByCode(string LgsDepartmentCode, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.LgsDepartments.Where(d => d.DepartmentCode == LgsDepartmentCode && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.LgsDepartments.Where(d => d.DepartmentCode == LgsDepartmentCode && d.LgsDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get Department by ID
        /// </summary>
        /// <param name="LgsDepartmentID"></param>
        /// <param name="isDependCategory"></param>
        /// <returns></returns>
        public LgsDepartment GetLgsDepartmentsByID(long LgsDepartmentID, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.LgsDepartments.Where(d => d.LgsDepartmentID == LgsDepartmentID && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.LgsDepartments.Where(d => d.LgsDepartmentID == LgsDepartmentID && d.LgsDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get Departments by name
        /// </summary>
        /// <param name="LgsDepartmentName"></param>
        /// <param name="isDependCategory"></param>
        /// <returns></returns>
        public LgsDepartment GetLgsDepartmentsByName(string LgsDepartmentName, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.LgsDepartments.Where(d => d.DepartmentName == LgsDepartmentName && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.LgsDepartments.Where(d => d.DepartmentName == LgsDepartmentName && d.LgsDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get all Department details 
        /// </summary>
        /// <returns></returns>
        public List<LgsDepartment> GetAllLgsDepartments(bool isDependCategory)
        {
            if (isDependCategory)
            { return context.LgsDepartments.ToList(); }
            else
            { return context.LgsDepartments.ToList(); }
        }

        /// <summary>
        /// Get active Department details 
        /// </summary>
        /// <returns></returns>
        public List<LgsDepartment> GetAllActiveLgsDepartments(bool isDependCategory)
        {
            if (isDependCategory)
            { return context.LgsDepartments.Where(d => d.IsDelete.Equals(false)).ToList(); }
            else
            { return context.LgsDepartments.Where(d => d.IsDelete.Equals(false) && d.LgsDepartmentID != 1).ToList(); }
        }

        /// <summary>
        /// Get active Department details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveLgsDepartmentsDataTable(bool isDependCategory)
        {
            DataTable tblDepartments = new DataTable();
            if (isDependCategory)
            {
                var query = from d in context.LgsDepartments where d.IsDelete == false select new { d.DepartmentCode, d.DepartmentName, d.Remark };
                return tblDepartments = Common.LINQToDataTable(query);
            }
            else
            {
                var query = from d in context.LgsDepartments where d.IsDelete == false && d.LgsDepartmentID != 1 select new { d.DepartmentCode, d.DepartmentName, d.Remark };
                return tblDepartments = Common.LINQToDataTable(query);
            }
        }

        /// <summary>
        /// Get Department codes
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsDepartmentCodes(bool isDependCategory) 
        {
            List<string> LgsDepartmentCodesList = new List<string>();
            if (isDependCategory)
            { LgsDepartmentCodesList = context.LgsDepartments.Where(v => v.IsDelete.Equals(false)).Select(v => v.DepartmentCode).ToList(); }
            else
            { LgsDepartmentCodesList = context.LgsDepartments.Where(v => v.IsDelete.Equals(false) && v.LgsDepartmentID != 1).Select(v => v.DepartmentCode).ToList(); }

            string[] lgsDepartmentCodes = LgsDepartmentCodesList.ToArray();
            return lgsDepartmentCodes;
        }

        /// <summary>
        /// Get Department names
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsDepartmentNames(bool isDependCategory)
        {
            List<string> LgsDepartmentNameList = new List<string>();
            if (isDependCategory)
            { LgsDepartmentNameList = context.LgsDepartments.Where(v => v.IsDelete.Equals(false)).Select(v => v.DepartmentName).ToList(); }
            else
            { LgsDepartmentNameList = context.LgsDepartments.Where(v => v.IsDelete.Equals(false) && v.LgsDepartmentID != 1).Select(v => v.DepartmentName).ToList(); }

            string[] lgsDepartmentNames = LgsDepartmentNameList.ToArray();
            return lgsDepartmentNames;
        }

        public DataTable GetAllLgsDepartmentDataTable(bool isDependCategory, List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            //var query = context.LgsDepartments.Where("IsDelete=false");

            var query = context.LgsDepartments.Where(d => d.IsDelete == false);

            if (!isDependCategory)
            {
                query = null;
                query = context.LgsDepartments.Where(d => d.LgsDepartmentID != 1 && d.IsDelete == false);
            }

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +  reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

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
                                   FieldString1 = lc.DepartmentCode,
                                   FieldString2 = lc.DepartmentName,
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

        public DataTable GetAllLgsDepartmentDataTable()
        {
            //DataTable tblCategroy = new DataTable();

            var query = (from lc in context.LgsDepartments
                         select new
                         {
                             FieldString1 = lc.DepartmentCode,
                             FieldString2 = lc.DepartmentName,
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
        public ArrayList GetSelectionData(bool isDependCategory, Common.ReportDataStruct reportDataStruct)
        {
            ////IQueryable qryResult = context.LgsDepartments
            ////                                 .Where("IsDelete == @0 ", false);
            
            IQueryable qryResult;

            if (!isDependCategory)
            {
                qryResult = context.LgsDepartments.Where("LgsDepartmentID != @0 AND IsDelete =@1", 1, false).OrderBy(reportDataStruct.DbColumnName);
            }
            else
            {
                qryResult = context.LgsDepartments.Where("IsDelete = @0", false).OrderBy(reportDataStruct.DbColumnName);

            }
            qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());

           
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
        #endregion
    }
}
