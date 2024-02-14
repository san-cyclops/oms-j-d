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
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class CompanyService
    {
        ERPDbContext context = new ERPDbContext();

        #region GetNewCode....

        /// <summary>
        /// Generate new company code
        /// </summary>
        /// <param name="preFix"></param>
        /// <param name="suffix"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.Companies.Max(c => c.CompanyCode);
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

        /// <summary>
        /// Save Company details
        /// </summary>
        /// <param name="Company"></param>
        public void AddCompany(Company Company)
        {
            context.Companies.Add(Company);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active company details by company code
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <returns></returns>
        public Company GetCompaniesByCode(string CompanyCode)
        {
            return context.Companies.Where(c => c.CompanyCode.Equals(CompanyCode) && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active company details by company name
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        public Company GetCompaniesByName(string CompanyName)
        {
            return context.Companies.Where(c => c.CompanyName.Equals(CompanyName) && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active company details by company ID
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public Company GetCompaniesByID(int CompanyID)
        {
            return context.Companies.Where(c => c.CompanyID.Equals(CompanyID) && c.IsDelete.Equals(false)).FirstOrDefault();
        }

       
        /// <summary>
        /// Retrive all active company details
        /// </summary>
        /// <returns></returns>
        public List<Company> GetAllCompanies() 
        {
            return context.Companies.Where(c => c.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Update company details
        /// </summary>
        /// <param name="existingCompany"></param>
        public void UpdateCompany(Company existingCompany) 
        {
            existingCompany.ModifiedUser = Common.LoggedUser;
            existingCompany.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingCompany).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete company details
        /// </summary>
        /// <param name="existingCompany"></param>
        public void DeleteCompany(Company existingCompany)
        {
            existingCompany.IsDelete = true;
            this.context.Entry(existingCompany).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get company codes for auto complete
        /// </summary>
        /// <returns></returns>
        public string [] GetAllCompanyCodes()
        {
            List<string> CompanyCodeList = context.Companies.Where(c => c.IsDelete.Equals(false)).Select(c => c.CompanyCode).ToList();
            return CompanyCodeList.ToArray();
        }

        /// <summary>
        /// Get company names for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllCompanyNames()
        {
            List<string> CompanyNameList = context.Companies.Where(c => c.IsDelete.Equals(false)).Select(c => c.CompanyName).ToList();
            return CompanyNameList.ToArray();
        }

        public DataTable GetAllActiveCompaniesDataTable()
        {
            DataTable tblCompany = new DataTable();
            var query = from c in context.Companies where c.IsDelete.Equals(false) select new { c.CompanyCode, c.CompanyName, c.GroupOfCompany.GroupOfCompanyName, c.CostCentre.CostCentreName,c.CostingMethod };
            return tblCompany = Common.LINQToDataTable(query);
        }


        #endregion
    }
}
