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
    public class AreaService
    {
        ERPDbContext context = new ERPDbContext(); 
 

        #region GetNewCode....

        /// <summary>
        /// Generate new area code
        /// </summary>
        /// <param name="preFix"></param>
        /// <param name="suffix"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.Areas.Max(a => a.AreaCode);
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
        /// Save Area details
        /// </summary>
        /// <param name="Area"></param>
        public void AddArea(Area Area)
        {
            context.Areas.Add(Area);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active areas details by area code
        /// </summary>
        /// <param name="AreaCode"></param>
        /// <returns></returns>
        public Area GetAreasByCode(string AreaCode)
        {
            return context.Areas.Where(a => a.AreaCode.Equals(AreaCode) && a.IsDelete.Equals(false)).FirstOrDefault(); 
        }

        /// <summary>
        /// Retrive all active areas details by area name
        /// </summary>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        public Area GetAreasByName(string AreaName)
        {
            return context.Areas.Where(a => a.AreaName.Equals(AreaName) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active areas details by area ID
        /// </summary>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        public Area GetAreasByID(long AreaID)
        {
            return context.Areas.Where(a => a.AreaID.Equals(AreaID) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active area details
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAllAreas() 
        {
            return context.Areas.Where(a => a.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Update area details
        /// </summary>
        /// <param name="existingArea"></param>
        public void UpdateArea(Area existingArea)
        {
            existingArea.ModifiedUser = Common.LoggedUser;
            existingArea.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(existingArea).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete area details
        /// </summary>
        /// <param name="existingArea"></param>
        public void DeleteArea(Area existingArea)  
        {
            existingArea.IsDelete = true;
            this.context.Entry(existingArea).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// <summary>
        /// Get active Areas details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveAreasDataTable() 
        {
            DataTable tblAreas = new DataTable();
            var query = from a in context.Areas where a.IsDelete == false select new { a.AreaCode, a.AreaName, a.Remark };
            return tblAreas = Common.LINQToDataTable(query);
        }

        /// <summary>
        /// Get All Area Names for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllAreaNames()
        {
            List<string> AreaNameList = context.Areas.Where(a => a.IsDelete.Equals(false)).Select(a => a.AreaName).ToList();
            return AreaNameList.ToArray();
        }

        /// <summary>
        /// Get All Area Codes for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllAreaCodes()
        {
            List<string> AreaCodeList = context.Areas.Where(a => a.IsDelete.Equals(false)).Select(a => a.AreaCode).ToList();
            return AreaCodeList.ToArray();
        }


        #endregion
    }
}
