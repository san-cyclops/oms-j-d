using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class DesignationTypeService
    {
        ERPDbContext context = new ERPDbContext();


        #region GetNewCode....

        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.EmployeeDesignationTypes.Max(a => a.DesignationCode);
            if (GetNewCode != null)
            {
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            else
            {
                GetNewCode = "0";
            }
            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }

        #endregion

        #region Methods.....

        public void AddDesignations(EmployeeDesignationType employeeDesignationType) 
        {
            context.EmployeeDesignationTypes.Add(employeeDesignationType);
            context.SaveChanges();
        }

        public EmployeeDesignationType GetesignationTypeByCode(string Code)
        {
            return context.EmployeeDesignationTypes.Where(a => a.DesignationCode.Equals(Code) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        public EmployeeDesignationType GetDesignationTypeByName(string designation) 
        {
            return context.EmployeeDesignationTypes.Where(a => a.Designation.Equals(designation) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        public EmployeeDesignationType GetDesignationTypeByID(long DesignationTypeID)  
        {
            return context.EmployeeDesignationTypes.Where(a => a.EmployeeDesignationTypeID.Equals(DesignationTypeID) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<EmployeeDesignationType> GetAllDesignationTypes() 
        {
            return context.EmployeeDesignationTypes.Where(a => a.IsDelete.Equals(false)).ToList();
        }

        public void UpdateDesignationType(EmployeeDesignationType existingEmployeeDesignationType)
        {
            existingEmployeeDesignationType.ModifiedUser = Common.LoggedUser;
            existingEmployeeDesignationType.ModifiedDate = Common.GetSystemDateWithTime();
            existingEmployeeDesignationType.DataTransfer = 0;
            this.context.Entry(existingEmployeeDesignationType).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void DeleteDesignationType(EmployeeDesignationType existingEmployeeDesignationType) 
        {
            existingEmployeeDesignationType.IsDelete = true;
            this.context.Entry(existingEmployeeDesignationType).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public DataTable GetAllActiveDesignationTypeDataTable() 
        {
            DataTable tblDesignations = new DataTable();
            var query = from a in context.EmployeeDesignationTypes where a.IsDelete == false select new { a.DesignationCode, a.Designation, a.Remarks };
            return tblDesignations = Common.LINQToDataTable(query);
        }

        public string[] GetAllDesignations() 
        {
            List<string> DesignationList = context.EmployeeDesignationTypes.Where(a => a.IsDelete.Equals(false)).Select(a => a.Designation).ToList();
            return DesignationList.ToArray();
        }

        public string[] GetAllDesignationCodes() 
        {
            List<string> DesignationCodeList = context.EmployeeDesignationTypes.Where(a => a.IsDelete.Equals(false)).Select(a => a.DesignationCode).ToList();
            return DesignationCodeList.ToArray();
        }


        #endregion
    }
}
