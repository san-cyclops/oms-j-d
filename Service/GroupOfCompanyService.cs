using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class GroupOfCompanyService
    {
        ERPDbContext context = new ERPDbContext();

        
        #region Methods.....

        /// <summary>
        /// Save Group Of CompanyCompany details
        /// </summary>
        /// <param name="GroupOfCompany"></param>
        public void AddGroupOfCompany(GroupOfCompany GroupOfCompany) 
        {
            context.GroupOfCompanies.Add(GroupOfCompany);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active Group Of Company details by Group Of Company code
        /// </summary>
        /// <param name="GroupOfCompanyCode"></param>
        /// <returns></returns>
        public GroupOfCompany GetGroupOfCompaniesByCode(string GroupOfCompanyCode) 
        {
            return context.GroupOfCompanies.Where(g => g.GroupOfCompanyCode.Equals(GroupOfCompanyCode) && g.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Group Of Company details by Group Of Company ID
        /// </summary>
        /// <param name="GroupOfCompanyID"></param>
        /// <returns></returns>
        public GroupOfCompany GetGroupOfCompaniesByID(int GroupOfCompanyID)
        {
            return context.GroupOfCompanies.Where(g => g.GroupOfCompanyID.Equals(GroupOfCompanyID) && g.IsDelete.Equals(false)).FirstOrDefault(); 
        }

        /// <summary>
        /// Retrive active Group Of Company details
        /// </summary>
        /// <returns></returns>
        public GroupOfCompany GetGroupOfCompany()
        {
            return context.GroupOfCompanies.Where(g => g.IsActive.Equals(true) && g.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Group Of Company details
        /// do not apply below for client visibility
        /// </summary>
        /// <returns></returns>
        //public List<GroupOfCompany> GetAllGroupOfCompanies()
        //{
        //    return context.GroupOfCompanies.Where(g => g.IsDelete.Equals(false) && g.IsDelete.Equals(false)).ToList();
        //}

        /// <summary>
        /// Update Group Of Company details
        /// </summary>
        /// <param name="existingGroupOfCompany"></param>
        public void UpdateGroupOfCompany(GroupOfCompany existingGroupOfCompany)
        {
            this.context.Entry(existingGroupOfCompany).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete Group Of Company details
        /// </summary>
        /// <param name="existingGroupOfCompany"></param>
        public void DeleteGroupOfCompany(Company existingGroupOfCompany) 
        {
            existingGroupOfCompany.IsDelete = true;
            this.context.Entry(existingGroupOfCompany).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        #endregion
    }
}
