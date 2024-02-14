using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity;
using Utility;
using MoreLinq;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class CostCentreService
    {
        ERPDbContext context = new ERPDbContext();


        #region Methods.....

        /// <summary>
        /// Save Cost Centre details
        /// </summary>
        /// <param name="CostCentre"></param>
        public void AddCostCentre(CostCentre CostCentre) 
        {
            context.CostCentres.Add(CostCentre);
            context.SaveChanges(); 
        }

        /// <summary>
        /// Retrive all active Cost Centre details by Cost Centre code
        /// </summary>
        /// <param name="CostCentreCode"></param>
        /// <returns></returns>
        public CostCentre GetCostCentresByCode(string CostCentreCode) 
        {
            return context.CostCentres.Where(c => c.CostCentreCode.Equals(CostCentreCode) && c.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Cost Centre details by Cost Centre Name
        /// </summary>
        /// <param name="CostCentreName"></param>
        /// <returns></returns>
        public CostCentre GetCostCentresByName(string CostCentreName)
        {
            return context.CostCentres.Where(c => c.CostCentreName.Equals(CostCentreName) && c.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Cost Centre details by Cost Centre ID
        /// </summary>
        /// <param name="CostCentreID"></param>
        /// <returns></returns>
        public CostCentre GetCostCentresByID(int CostCentreID)
        {
            return context.CostCentres.Where(c => c.CostCentreID.Equals(CostCentreID) && c.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Cost Centre details
        /// </summary>
        /// <returns></returns>
        public List<CostCentre> GetAllCostCentres()
        {
            return context.CostCentres.Where(c => c.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Update Cost Centre details
        /// </summary>
        /// <param name="existingCostCentre"></param>
        public void UpdateCostCentre(CostCentre existingCostCentre)
        {
            existingCostCentre.ModifiedUser = Common.LoggedUser;
            existingCostCentre.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingCostCentre).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public List<string> GetAllCostCentreArray()
        {
            List<string> list = context.CostCentres.Where(c => c.IsDelete == false).Select(c => c.CostCentreCode).ToList();
            return list;

        }

        public List<string> GetAllCostCentreNameArray()
        {
            List<string> list = context.CostCentres.Where(c => c.IsDelete == false).Select(c => c.CostCentreName).ToList();
            return list;

        }

        #region GetNewCode

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.CostCentres.Max(c => c.CostCentreCode);
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }

        #endregion

        public string[] GetAllCostCentreCodes()
        {
            List<string> CostCentreCodeList = context.CostCentres.Where(c => c.IsDelete.Equals(false)).Select(c => c.CostCentreCode).ToList();
            return CostCentreCodeList.ToArray();

        }

        public string[] GetAllCostCentreNames()
        {
            List<string> CostCentreNameList = context.CostCentres.Where(c => c.IsDelete.Equals(false)).Select(c => c.CostCentreName).ToList();
            return CostCentreNameList.ToArray();

        }

        public DataTable GetAllCostCentreDataTable()
        {
            var qry = (from cs in context.CostCentres
                       where cs.IsDelete == false
                       select new
                       {
                           cs.CostCentreCode,
                           cs.CostCentreName
                       });
            return qry.ToDataTable();
        }

        #endregion
    }
}
