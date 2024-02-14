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
    public class TerritoryService
    {
        ERPDbContext context = new ERPDbContext();

        #region GetNewCode....

        /// <summary>
        /// Get new territory code
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="areaID"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public string GetNewCode(string formName, long areaID, string areaCode)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            if (autoGenerateInfo.IsDependCode)
            {
                preFix = areaCode + preFix;
                getNewCode = context.Territories.Where(t => t.AreaID == areaID).Max(t => t.TerritoryCode);
            }
            else
            {
                getNewCode = context.Territories.Max(t => t.TerritoryCode); 
            }
            if (getNewCode == null)
            {
                getNewCode = "0";
            }
            else
            {
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }

        #endregion

        #region Methods.....

        /// <summary>
        /// Save Territory
        /// </summary>
        /// <param name="Territory"></param>
        public void AddTerritory(Territory Territory) 
        {
            context.Territories.Add(Territory);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active  Territory details by area code
        /// </summary>
        /// <param name="Territory"></param>
        /// <returns></returns>
        public Territory GetTerritoryByCode(string TerritoryCode)
        {
            return context.Territories.Where(t => t.TerritoryCode == TerritoryCode && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active  Territory details by territory name
        /// </summary>
        /// <param name="TerritoryName"></param>
        /// <returns></returns>
        public Territory GetTerritoryByName(string territoryName)
        {
            return context.Territories.Where(t => t.TerritoryName == territoryName && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active  terrritory details
        /// </summary>
        /// <returns></returns>
        public List<Territory> GetAllTerritories()
        {
            return context.Territories.Where(t => t.IsDelete.Equals(false)).ToList(); 
        }

        /// <summary>
        /// Update Territory
        /// </summary>
        /// <param name="existingTerritory"></param>
        public void UpdateTerritory(Territory existingTerritory)
        {
            existingTerritory.ModifiedUser = Common.LoggedUser;
            existingTerritory.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingTerritory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        
        /// <summary>
        /// Delete territoty
        /// </summary>
        /// <param name="existingTerritory"></param>
        public void DeleteTerritory(Territory existingTerritory)
        {
            existingTerritory.IsDelete = true;
            this.context.Entry(existingTerritory).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get All Territory Codes for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllTerritoryCodes()
        {
            List<string> TerritoryCodeList = context.Territories.Where(t => t.IsDelete.Equals(false)).Select(t => t.TerritoryCode).ToList();
            return TerritoryCodeList.ToArray();
        }

        /// <summary>
        /// Get All Territory Names for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllTerritoryNames()
        {
            List<string> TerritoryNameList = context.Territories.Where(t => t.IsDelete.Equals(false)).Select(t => t.TerritoryName).ToList();
            return TerritoryNameList.ToArray();
        }

        public DataTable GetAllActiveTerritoriesDataTable() 
        {
            DataTable tblTerritories = new DataTable();
            var query = from t in context.Territories join a in context.Areas on t.AreaID equals a.AreaID select new { a.AreaCode, a.AreaName, t.TerritoryCode, t.TerritoryName };
            return tblTerritories = Common.LINQToDataTable(query);
            
        }

        #endregion

    }
}
