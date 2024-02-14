using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoreLinq;
namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class SupplierGroupService
     {
        ERPDbContext context = new ERPDbContext();

        #region Get new code....
        /// <summary>
        /// Generate new supplier group code
        /// </summary>
        /// <param name="preFix"></param>
        /// <param name="suffix"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.SupplierGroups.Max(s => s.SupplierGroupCode);
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

        /// <summary>
        /// Save new supplier group
        /// </summary>
        /// <param name="supplierGroup"></param>
        public void AddSupplierGroup(SupplierGroup supplierGroup)
        {
          context.SupplierGroups.Add(supplierGroup);
          context.SaveChanges();
        }

        /// <summary>
        /// Get all supplier groups by suplier group codes
        /// </summary>
        /// <param name="supplierGroupCode"></param>
        /// <returns></returns>
        public SupplierGroup GetSupplierGroupsByCode(string supplierGroupCode)
        {
            return context.SupplierGroups.Where(c => c.SupplierGroupCode == supplierGroupCode && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Get all supplier groups by suplier group names
        /// </summary>
        /// <param name="supplierGroupName"></param>
        /// <returns></returns>
        public SupplierGroup GetSupplierGroupsByName(string supplierGroupName)
        {
            return context.SupplierGroups.Where(c => c.SupplierGroupName == supplierGroupName && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Get all supplier groups by suplier group ID
        /// </summary>
        /// <param name="supplierGroupID"></param>
        /// <returns></returns>
        public SupplierGroup GetSupplierGroupsByID(int supplierGroupID)
        {
            return context.SupplierGroups.Where(s => s.SupplierGroupID.Equals(supplierGroupID) && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Modify existing customer group
        /// </summary>
        /// <param name="supplierGroup"></param>
        public void UpdateSupplierGroup(SupplierGroup supplierGroup)
        {
            supplierGroup.ModifiedUser = Common.LoggedUser;
            supplierGroup.ModifiedDate = Common.GetSystemDateWithTime();
            supplierGroup.DataTransfer = 0;
            this.context.Entry(supplierGroup).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void UpdateSupplierGroup(int datatransfer)
        {
            context.SupplierGroups.Update(ps => ps.DataTransfer.Equals(1) , ps => new SupplierGroup { DataTransfer = datatransfer });
        }

        public void UpdateSupplierGroupDTSelect(int datatransfer)
        {
            context.SupplierGroups.Update(ps => ps.DataTransfer.Equals(0), ps => new SupplierGroup { DataTransfer = datatransfer });
        }

        public void UpdateSupplierProperty(int datatransfer)
        {
            context.SupplierProperties.Update(ps => ps.DataTransfer.Equals(1), ps => new SupplierProperty { DataTransfer = datatransfer });
        }

        /// <summary>
        /// Get all avtive supplier groups
        /// </summary>
        /// <returns></returns>
        public List<SupplierGroup> GetAllSupplierGroups()
        {
            return context.SupplierGroups.Where(c => c.IsDelete==false).ToList();
        }


        public DataTable GetAllSupplierGroupsDataTable()
        {
            DataTable tblSupplierGroups = new DataTable();
            var query = from d in context.SupplierGroups where d.IsDelete == false select new { d.SupplierGroupID,d.SupplierGroupCode,d.SupplierGroupName,d.Remark, d.IsDelete, d.GroupOfCompanyID, d.CreatedUser, d.CreatedDate, d.ModifiedUser, d.ModifiedDate, DataTransfer = d.DataTransfer };
            return tblSupplierGroups = Common.LINQToDataTable(query);
        }

        public DataTable GetAllSupGroupsDataTable()
        {

            //context.Configuration.LazyLoadingEnabled = false;
            //return context.SupplierGroups.SqlQuery("select SupplierGroupID,SupplierGroupCode,SupplierGroupName,Remark,IsDelete,GroupOfCompanyID,CreatedUser,CreatedDate,ModifiedUser,ModifiedDate,DataTransfer from SupplierGroup").ToDataTable();
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.SupplierGroups
                        where d.DataTransfer == 1
                        select new
                        {
                            d.SupplierGroupID,
                            d.SupplierGroupCode,
                            d.SupplierGroupName,
                            d.Remark,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer
                        };

            return tblSuppliers = query.ToDataTable();
        }

        public DataTable GetSupplierGroupDataTable() 
        {
            DataTable tblAreas = new DataTable();
            var query = from a in context.SupplierGroups
                        where a.IsDelete == false
                        select new
                        {
                            a.SupplierGroupCode,
                            a.SupplierGroupName,
                            a.Remark
                        };
            return tblAreas = Common.LINQToDataTable(query);
        }


        /// <summary>
        /// Get all supplier group codes to auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllSupplierGroupCodes()
        {
            List<string> supplierGroupCodeList = context.SupplierGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.SupplierGroupCode).ToList();
            return supplierGroupCodeList.ToArray();
            
        }

        /// <summary>
        /// Get all supplier group names to auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllSupplierGroupNames()
        {
            List<string> supplierGroupNameList = context.SupplierGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.SupplierGroupName).ToList();
            return supplierGroupNameList.ToArray();
        }

        /// <summary>
        /// Delete Supplier group details
        /// </summary>
        /// <param name="existingSupplierGroup"></param>
        public void DeleteSupplierGroup(SupplierGroup existingSupplierGroup)
        {
            existingSupplierGroup.IsDelete = true;
            this.context.Entry(existingSupplierGroup).State = EntityState.Modified;
            this.context.SaveChanges();
        }

    }
}



