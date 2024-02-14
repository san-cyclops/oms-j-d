using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoreLinq;

namespace Service
{
    public class UnitOfMeasureService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new UnitOfMeasure
        /// </summary>
        /// <param name="unitOfMeasure"></param>
        public void AddUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            context.UnitOfMeasures.Add(unitOfMeasure);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing UnitOfMeasure
        /// </summary>
        /// <param name="unitOfMeasure"></param>
        public void UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            unitOfMeasure.ModifiedUser = Common.LoggedUser;
            unitOfMeasure.ModifiedDate = Common.GetSystemDateWithTime();
            unitOfMeasure.DataTransfer = 0;
            context.Entry(unitOfMeasure).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// Data Transfer ------
        public void UpdateUnitOfMeasure(int datatransfer)
        {
            context.UnitOfMeasures.Update(ps => ps.DataTransfer.Equals(1) , ps => new UnitOfMeasure { DataTransfer = datatransfer });
        }

        public void UpdateUnitOfMeasureDTSelect(int datatransfer)
        {
            context.UnitOfMeasures.Update(ps => ps.DataTransfer.Equals(0), ps => new UnitOfMeasure { DataTransfer = datatransfer });
        }
        //----------------------

        /// <summary>
        /// Search UnitOfMeasure by ID
        /// </summary>
        /// <param name="unitOfMeasureBeforeUpdateId"></param>
        /// <returns></returns>
        public UnitOfMeasure GetUnitOfMeasureById(long unitOfMeasureBeforeUpdateId)
        {
            return context.UnitOfMeasures.Where(u => u.UnitOfMeasureID == unitOfMeasureBeforeUpdateId).FirstOrDefault();
        }

        /// <summary>
        /// Search UnitOfMeasure by code
        /// </summary>
        /// <param name="unitOfMeasureCode"></param>
        /// <returns></returns>
        public UnitOfMeasure GetUnitOfMeasureByCode(string unitOfMeasureCode)
        {
            return context.UnitOfMeasures.Where(u => u.UnitOfMeasureCode == unitOfMeasureCode && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search UnitOfMeasure by name
        /// </summary>
        /// <param name="unitOfMeasureName"></param>
        /// <returns></returns>
        public UnitOfMeasure GetUnitOfMeasureByName(string unitOfMeasureName)
        {
            return context.UnitOfMeasures.Where(u => u.UnitOfMeasureName == unitOfMeasureName && u.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get all UnitOfMeasure details 
        /// </summary>
        /// <returns>UnitOfMeasure</returns>
        public List<UnitOfMeasure> GetAllUnitOfMeasures()
        {
            return context.UnitOfMeasures.Where(u => u.IsDelete == false).ToList();
        }

        public DataTable GetAllUnitOfMeasuresDataTable()
        {
            DataTable tblUom = new DataTable();
            var query = from d in context.UnitOfMeasures 
                        where d.IsDelete.Equals(false)  
                        select new 
                        { 
                            d.UnitOfMeasureCode,
                            d.UnitOfMeasureName,
                            d.Remark,
                        };
            return tblUom = Common.LINQToDataTable(query);
        }


        public DataTable GetAllDTUnitOfMeasuresDataTable()
        {
            DataTable tblUom = new DataTable();
            var query = (from d in context.UnitOfMeasures
                         where d.DataTransfer.Equals(1)
                         select new 
                         {
                             d.UnitOfMeasureID,
                             d.UnitOfMeasureCode,
                             d.UnitOfMeasureName,
                             d.Remark,
                             d.IsDelete,
                             d.GroupOfCompanyID,
                             d.CreatedUser,
                             d.CreatedDate,
                             d.ModifiedUser,
                             d.ModifiedDate,
                             DataTransfer = d.DataTransfer
                         });
            return tblUom = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.UnitOfMeasures.SqlQuery("select UnitOfMeasureID,UnitOfMeasureCode,UnitOfMeasureName,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from UnitOfMeasure").ToDataTable();
        }


        /// <summary>
        /// Get UnitOfMeasure Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetUnitOfMeasureCodes()
        {
            List<string> unitOfMeasureCodesList = context.UnitOfMeasures.Where(u => u.IsDelete.Equals(false)).Select(u => u.UnitOfMeasureCode).ToList();
            string[] unitOfMeasureCodes = unitOfMeasureCodesList.ToArray();
            return unitOfMeasureCodes;
        }

        /// <summary>
        /// Get UnitOfMeasure Names
        /// </summary>
        /// <returns></returns>
        public string[] GetUnitOfMeasureNames()
        {
            List<string> unitOfMeasureNamesList = context.UnitOfMeasures.Where(u => u.IsDelete.Equals(false)).Select(u => u.UnitOfMeasureName).ToList();
            string[] unitOfMeasureNames = unitOfMeasureNamesList.ToArray();
            return unitOfMeasureNames;
        }

        public string[] GetUnitOfMeasureIDNames()
        {
            List<string> unitOfMeasureNamesList = context.UnitOfMeasures.Where(u => u.IsDelete.Equals(false)).Select(u => u.UnitOfMeasureName ).ToList();
            string[] unitOfMeasureNames = unitOfMeasureNamesList.ToArray();
            return unitOfMeasureNames;
        }
      

        #region GetNewCode
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.UnitOfMeasures.Max(a => a.UnitOfMeasureCode);
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

        #endregion
    }
}
