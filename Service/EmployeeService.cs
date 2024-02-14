using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;
using Utility;
using System.Data;
using MoreLinq;
using EntityFramework.Extensions;
using System.Collections;

namespace Service
{
    public class EmployeeService
    {
        ERPDbContext context = new ERPDbContext();
        Employee Employee = new Employee();

        #region methods


        /// <summary>
        /// Search Employee by code
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public Employee GetEmployeesByCode(string employeeCode)
        {
            return context.Employees.Where(c => c.EmployeeCode == employeeCode && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Get Employee by Name
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public Employee GetEmployeesByName(string employeeName)
        {
            return context.Employees.Where(c => c.EmployeeName == employeeName && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Search Employee by ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee GetEmployeesByID(long employeeID)
        {
            return context.Employees.Where(s => s.EmployeeID == employeeID && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get all Employee details 
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllEmployees()
        {
            return context.Employees.Where(c => c.IsDelete == false).ToList();
        }


        public string[] GetAllEmployeeCodes()
        {
            List<string> EmployeeCodeList = context.Employees.Where(c => c.IsDelete.Equals(false)).Select(c => c.EmployeeCode).ToList();
            return EmployeeCodeList.ToArray();
        }

        public string[] GetAllEmployeeNames()
        {
            List<string> EmployeeNameList = context.Employees.Where(c => c.IsDelete.Equals(false)).Select(c => c.EmployeeName).ToList();
            return EmployeeNameList.ToArray();
        }

        public string[] GetAllemployeeDesignations()
        {
            List<String> designationList = context.EmployeeDesignationTypes.Select(dt => dt.Designation).ToList();
            return designationList.ToArray();
        }

        public string[] GetAllActiveEmployeeCodesForCashier()
        {
            List<string> EmployeeCodeList = context.Employees.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.EmployeeCode).ToList();
            return EmployeeCodeList.ToArray();
        }

        public string[] GetAllActiveEmployeeNamesForCashier() 
        {
            List<string> EmployeeNameList = context.Employees.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.EmployeeName).ToList();
            return EmployeeNameList.ToArray();
        }


        #region GetNewCode

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.Employees.Max(c => c.EmployeeCode);
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }


        #endregion

        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.Employees.Max(s => s.EmployeeCode);
            if (GetNewCode != null)
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                GetNewCode = "0";

            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }


        public void AddEmployee(Employee employee) 
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee) 
        {
            employee.ModifiedUser = Common.LoggedUser;
            employee.ModifiedDate = Common.GetSystemDateWithTime();
            employee.DataTransfer = 0;
            this.context.Entry(employee).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateEmployee(int Edatatransfer, int datatransfer)
        {
            context.Employees.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new Employee { DataTransfer = datatransfer });
        }

        public void UpdateEmployeeDesignationType(int Edatatransfer, int datatransfer)
        {
            context.EmployeeDesignationTypes.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new EmployeeDesignationType { DataTransfer = datatransfer });
        }

        public Employee GetActiveEmployeesByCode(string employeeCode) 
        {
            return context.Employees.Where(c => c.EmployeeCode == employeeCode && c.IsDelete.Equals(false) && c.IsActive.Equals(true)).FirstOrDefault();
        }

        public Employee GetActiveEmployeesByName(string employeeName) 
        {
            return context.Employees.Where(c => c.EmployeeName == employeeName && c.IsDelete.Equals(false) && c.IsActive.Equals(true)).FirstOrDefault();
        }

        //public DataTable GetAllActiveAreasDataTable(bool isDependTerritory)
        //{
        //    DataTable tblAreas = new DataTable();
        //    var query = from a in context.Areas where a.IsDelete == false select new { a.AreaCode, a.AreaName, a.Remark };
        //    return tblAreas = Common.LINQToDataTable(query);
        //}

        public DataTable GetAllEmployeeDataTable()  
        {
            DataTable dtEmployee = new DataTable();
            var empQry = from e in context.Employees where e.IsDelete.Equals(false) select new { e.EmployeeCode, e.EmployeeName, e.Designation, e.Mobile, e.Address1, e.Address2 };
            return dtEmployee = empQry.ToDataTable();
        }

        public DataTable GetAllEmployeeDataTableTransfer()
        {
            DataTable dtEmployee = new DataTable();
            var empQry = from e in context.Employees
                         where e.DataTransfer.Equals(1)
                         select new
                         {
                             e.EmployeeID,
                             e.EmployeeCode,
                             e.EmployeeTitle,
                             e.EmployeeName,
                             e.Gender,
                             e.ReferenceNo,
                             e.Remark,
                             e.Address1,
                             e.Address2,
                             e.Address3,
                             e.Email,
                             e.Telephone,
                             e.Mobile,
                             e.Image,
                             e.IsActive,
                             e.MinimumDiscount,
                             e.MaximumDiscount,
                             e.CrLimit,
                             e.PchLimit,
                             e.AccCredit,
                             e.AccPch,
                             e.IsDelete,
                             e.GroupOfCompanyID,
                             e.CreatedUser,
                             e.CreatedDate,
                             e.ModifiedUser,
                             e.ModifiedDate,
                             e.DataTransfer,
                             e.Designation
                         };
            return dtEmployee = empQry.ToDataTable();
        }

        public DataTable GetAllEmployeeDesignationTypeDataTableTransfer()
        {
            DataTable dtEmployee = new DataTable();
            var empQry = from e in context.EmployeeDesignationTypes
                         where e.DataTransfer.Equals(1)
                         select new
                         {
                             e.EmployeeDesignationTypeID ,
                             e.Designation,
                             e.Remarks,
                             e.GroupOfCompanyID,
                             e.CreatedUser,
                             e.CreatedDate,
                             e.ModifiedUser,
                             e.ModifiedDate,
                             e.DataTransfer
                             
                         };
            return dtEmployee = empQry.ToDataTable();
        }


        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            AutoGenerateInfo autoGenerateInfoPo = new AutoGenerateInfo();
            autoGenerateInfoPo = AutoGenerateInfoService.GetAutoGenerateInfoByForm("RptLocationWiseSummary");


            IQueryable qryResult = context.Employees;


            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LocationId":
                    qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                        .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                        .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + ""); ;
                    break;
                case "EmployeeID":
                    qryResult = qryResult.Join(context.Employees, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                            .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");

                    break;

                default:
                    //qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());    
                    qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")",
                                                    "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                            .OrderBy("Key." + reportDataStruct.DbColumnName.Trim() + "")
                                            .Select("Key." + reportDataStruct.DbColumnName.Trim() + "");
                    break;

            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResult)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else if (reportDataStruct.ValueDataType.Equals(typeof(int)))
            {
                if (reportDataStruct.ReportFieldName.Trim() == "Document Status")
                {
                    selectionDataList.Add("Partial");
                    selectionDataList.Add("Opened");
                    selectionDataList.Add("Expired");
                }
            }
            else
            {
                foreach (var item in qryResult)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString().Trim()); }
                }
            }
            return selectionDataList;
        }

        public DataTable GetAllEmployeeDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Employees.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            //querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from lc in query
                               where lc.IsDelete == false
                               select new
                               {
                                   FieldString1 = lc.EmployeeCode,
                                   FieldString2 = lc.EmployeeName,
                                   FieldString3 = lc.Designation,
                                   FieldString4 = lc.Mobile, 
                                   FieldString5 = lc.Telephone,
                                   FieldString6 = lc.ReferenceNo,
                                   FieldString7 = (lc.Address1 + " " + lc.Address2 + " " + lc.Address3 + " "),
                                   FieldString8 = lc.Remark,
                                   FieldString9 = "",
                                   FieldString10 = "",
                                   FieldString11 = "",
                                   FieldString12 = "",
                                   FieldString13 = "",
                                   FieldString14 = ""
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


        #endregion
    }
}
