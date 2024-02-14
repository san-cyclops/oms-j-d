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
using EntityFramework.Extensions;


namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class InvDepartmentService
    {
        ERPDbContext context = new ERPDbContext();

        #region methods
        
        /// <summary>
        /// Generate new code 
        /// </summary>
        public string GetNewCode(string preFix, int suffix, int codeLength,bool isDependCategory)
        {
            string GetNewCode = string.Empty;
            if (isDependCategory)
            { GetNewCode = context.InvDepartments.Max(c => c.DepartmentCode); }
            else
            { GetNewCode = context.InvDepartments.Where(c => c.InvDepartmentID != 1).Max(c => c.DepartmentCode); }

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
        /// <param name="invDepartment"></param>
        public void AddInvDepartment(InvDepartment invDepartment)
        {
            context.InvDepartments.Add(invDepartment);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing department
        /// </summary>
        /// <param name="existingInvDepartment"></param>
        public void UpdateInvDepartment(InvDepartment existingInvDepartment)
        {
            existingInvDepartment.ModifiedUser = Common.LoggedUser;
            existingInvDepartment.ModifiedDate = Common.GetSystemDateWithTime();
            existingInvDepartment.DataTransfer = 0;
            this.context.Entry(existingInvDepartment).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateInvDepartmentDataTransfer(int datatransfer)
        {
            context.InvDepartments.Update(ps => ps.DataTransfer.Equals(1), ps => new InvDepartment { DataTransfer = datatransfer});
        }

        public void UpdateInvDepartmentDTSelect(int datatransfer)
        {
            var qry = (from d in context.InvDepartments where d.DataTransfer.Equals(0) select d).ToArray();

            foreach (var temp in qry)
            {
                context.InvDepartments.Update(d => d.InvDepartmentID.Equals(temp.InvDepartmentID), d => new InvDepartment { DataTransfer = 1 });
            }
        }

        /// <summary>
        /// Get Department by code
        /// </summary>
        /// <param name="InvDepartmentCode"></param>
        /// <returns></returns>
        public InvDepartment GetInvDepartmentsByCode(string InvDepartmentCode, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.InvDepartments.Where(d => d.DepartmentCode == InvDepartmentCode && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.InvDepartments.Where(d => d.DepartmentCode == InvDepartmentCode && d.InvDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get Department by ID
        /// </summary>
        /// <param name="InvDepartmentID"></param>
        /// <param name="isDependCategory"></param>
        /// <returns></returns>
        public InvDepartment GetInvDepartmentsByID(long InvDepartmentID, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.InvDepartments.Where(d => d.InvDepartmentID == InvDepartmentID && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.InvDepartments.Where(d => d.InvDepartmentID == InvDepartmentID && d.InvDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get Departments by name
        /// </summary>
        /// <param name="InvDepartmentName"></param>
        /// <param name="isDependCategory"></param>
        /// <returns></returns>
        public InvDepartment GetInvDepartmentsByName(string InvDepartmentName, bool isDependCategory)
        {
            if (isDependCategory)
            { return context.InvDepartments.Where(d => d.DepartmentName == InvDepartmentName && d.IsDelete.Equals(false)).FirstOrDefault(); }
            else
            { return context.InvDepartments.Where(d => d.DepartmentName == InvDepartmentName && d.InvDepartmentID != 1 && d.IsDelete.Equals(false)).FirstOrDefault(); }
        }

        /// <summary>
        /// Get all Department details 
        /// </summary>
        /// <returns></returns>
        public List<InvDepartment> GetAllInvDepartments(bool isDependCategory)
        {
            if (isDependCategory)
            { return context.InvDepartments.ToList(); }
            else
            { return context.InvDepartments.ToList(); }
        }


        public InvDepartment GetAllInvDepartmentAll()
        {
            return context.InvDepartments.FirstOrDefault();

        }

     
        /// <summary>
        /// Get active Department details 
        /// </summary>
        /// <returns></returns>
        public List<InvDepartment> GetAllActiveInvDepartments(bool isDependCategory)
        {
            if (isDependCategory)
            { return context.InvDepartments.Where(d => d.IsDelete.Equals(false)).ToList(); }
            else
            { return context.InvDepartments.Where(d => d.IsDelete.Equals(false) && d.InvDepartmentID != 1).ToList(); }
        }

        /// <summary>
        /// Get active Department details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveInvDepartmentsDataTable(bool isDependCategory)
        {

            DataTable tblDepartments = new DataTable();
            if (isDependCategory)
            {
                var query = from d in context.InvDepartments 
                            where d.IsDelete == false 
                            select new 
                            {
                                d.DepartmentCode,
                                d.DepartmentName,
                                d.Remark,
                            };
                return tblDepartments = Common.LINQToDataTable(query);
            }
            else
            {
                var query = from d in context.InvDepartments 
                            where d.IsDelete == false && d.InvDepartmentID != 1 
                            select new 
                            { 
                                d.DepartmentCode, 
                                d.DepartmentName, 
                                d.Remark, 
                            };
                return tblDepartments = Common.LINQToDataTable(query);
            }            
        }

        //To data transfer
        public DataTable GetActiveInvDepartmentsDataTable(bool isDependCategory) 
        {

            DataTable tblDepartments = new DataTable();
            if (isDependCategory)
            {
                var query = from d in context.InvDepartments
                            where d.IsDelete == false && d.DataTransfer.Equals(0)
                            select new { d.InvDepartmentID, d.DepartmentCode, d.DepartmentName, d.Remark, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
                return tblDepartments = Common.LINQToDataTable(query);
            }
            else
            {
                var query = from d in context.InvDepartments where d.IsDelete == false && d.InvDepartmentID != 1 && d.DataTransfer.Equals(0) select new { d.InvDepartmentID, d.DepartmentCode, d.DepartmentName, d.Remark, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
                return tblDepartments = Common.LINQToDataTable(query);
            }
        }

        //To data transfer
        public DataTable GetActiveInvDepartmentsDataTable()
        {
            DataTable dtx = new DataTable();
            var query = from d in context.InvDepartments where d.DataTransfer.Equals(1) select new 
            { 
                d.InvDepartmentID, d.DepartmentCode, d.DepartmentName, d.Remark, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer 
            };
            return dtx = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.InvDepartments.SqlQuery("select InvDepartmentID,DepartmentCode,DepartmentName,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from InvDepartment").ToDataTable();



        }

        /// <summary>
        /// Get Department codes
        /// </summary>
        /// <returns></returns>
        public string[] GetInvDepartmentCodes(bool isDependCategory)
        {
            List<string> InvDepartmentCodesList = new List<string>();
            if (isDependCategory)
            { InvDepartmentCodesList = context.InvDepartments.Where(v => v.IsDelete.Equals(false)).Select(v => v.DepartmentCode).ToList(); }
            else
            {  InvDepartmentCodesList = context.InvDepartments.Where(v => v.IsDelete.Equals(false) && v.InvDepartmentID != 1).Select(v => v.DepartmentCode).ToList(); }

            string[] invDepartmentCodes = InvDepartmentCodesList.ToArray();
            return invDepartmentCodes;
        }

        /// <summary>
        /// Get Department names
        /// </summary>
        /// <returns></returns>
        public string[] GetInvDepartmentNames(bool isDependCategory)
        {
            List<string> InvDepartmentNameList = new List<string>();
            if (isDependCategory)
            { InvDepartmentNameList = context.InvDepartments.Where(v => v.IsDelete.Equals(false)).Select(v => v.DepartmentName).ToList(); }
            else
            { InvDepartmentNameList = context.InvDepartments.Where(v => v.IsDelete.Equals(false) && v.InvDepartmentID != 1).Select(v => v.DepartmentName).ToList(); }

            string[] invDepartmentNames = InvDepartmentNameList.ToArray();
            return invDepartmentNames;
        }

        public DataTable GetAllInvDepartmentDataTable(bool isDependCategory, List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.InvDepartments.Where(d => d.IsDelete == false);

            if (!isDependCategory)
            {
                query = null;
                query = context.InvDepartments.Where(d => d.InvDepartmentID != 1 && d.IsDelete == false);
            }

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

        public DataTable GetAllInvDepartmentDataTable(bool isDependCategory)
        {
            //DataTable tblCategroy = new DataTable();

            var queryTable = context.InvDepartments.Where(d => d.IsDelete == false);

            if (!isDependCategory)
            {
                queryTable = context.InvDepartments.Where(d => d.InvDepartmentID != 1 && d.IsDelete == false);
            }

            var query = (from lc in queryTable
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
            //IQueryable qryResult = context.InvDepartments
            //                                 .Where("IsDelete == @0 ", false);

            IQueryable qryResult;

            if (!isDependCategory)
            {
                qryResult = context.InvDepartments.Where("InvDepartmentID != @0 AND IsDelete =@1", 1, false).OrderBy(reportDataStruct.DbColumnName); 
            }
            else
            {
                qryResult = context.InvDepartments.Where("IsDelete = @0", false).OrderBy(reportDataStruct.DbColumnName);       

            }
            qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
            //.Select(reportDataStruct.DbColumnName.Trim());


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


        public string[] GetAllDepartmentCodes() 
        {
            List<string> DepartmentCodeList = context.InvDepartments.Where(c => c.IsDelete.Equals(false) && c.InvDepartmentID != 1).Select(c => c.DepartmentCode).ToList();
            return DepartmentCodeList.ToArray();
        }

        public string[] GetAllDepartmentNames() 
        {
            List<string> DepartmentNameList = context.InvDepartments.Where(c => c.IsDelete.Equals(false) && c.InvDepartmentID != 1).OrderBy(c=> c.DepartmentName).Select(c => c.DepartmentName).ToList();
            return DepartmentNameList.ToArray();
        }

        public List<InvDepartment> GetAllDepartmentNamesList()
        {
            return context.InvDepartments.Where(l => l.IsDelete.Equals(false) && l.InvDepartmentID != 1 ).OrderBy(l => l.InvDepartmentID).ToList();
        }

        public List<InvDepartment> GetDepartmentList()  
        {
            return context.InvDepartments.Where(l => l.IsDelete.Equals(false) && l.InvDepartmentID != 1).OrderBy(l => l.InvDepartmentID).ToList();
        }


        public long[] GetInvDepartmentIdRangeByDepartmentNames(string nameFrom, string nameTo)
        {
            return context.InvDepartments.Where(d => d.IsDelete.Equals(false) && d.DepartmentName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.DepartmentName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.InvDepartmentID).ToArray();
        }
        #endregion


    }
}
