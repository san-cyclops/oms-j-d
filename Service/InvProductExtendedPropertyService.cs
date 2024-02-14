using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Data;
using System.Data;
using System.Data.Entity;
using Utility;

namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class InvProductExtendedPropertyService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Generate new code 
        /// </summary>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.InvProductExtendedProperties.Max(c => c.ExtendedPropertyCode);
            if (GetNewCode != null)
            { GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length); }
            else
            { GetNewCode = "0"; }

            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }



        /// <summary>
        /// Get active Product Extended Properties
        /// </summary>
        /// <returns></returns>
        public List<InvProductExtendedProperty> GetAllActiveInvProductExtendedProperties()
        {
            return context.InvProductExtendedProperties.Where(c => c.IsDelete.Equals(false)).OrderBy(c => c.InvProductExtendedPropertyID).ToList();
        }

        /// <summary>
        /// Get all Product Extended Property details 
        /// </summary>
        /// <returns></returns>
        public List<InvProductExtendedProperty> GetAllInvProductExtendedProperties()
        {
            return context.InvProductExtendedProperties.ToList();
        }


        /// <summary>
        /// Get Product Extended Property by code
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvProductExtendedProperty GetInvProductExtendedPropertyByCode(string invProductExtendedPropertyCode)
        {
            return context.InvProductExtendedProperties.Where(c => c.ExtendedPropertyCode == invProductExtendedPropertyCode).FirstOrDefault();
        }

        public InvProductExtendedProperty GetInvProductExtendedPropertyById(long invProductExtendedPropertyId)
        {
            return context.InvProductExtendedProperties.Where(c => c.InvProductExtendedPropertyID == invProductExtendedPropertyId).FirstOrDefault();
        }

        /// <summary>
        /// Save new Product Extended Property
        /// </summary>
        /// <param name="invProductExtendedProperty"></param>
        public void AddInvProductExtendedProperty(InvProductExtendedProperty invProductExtendedProperty)
        {
            context.InvProductExtendedProperties.Add(invProductExtendedProperty);
            context.SaveChanges();
        }

        /// <summary>
        /// Update existing Product Extended Property
        /// </summary>
        /// <param name="existingInvProductExtendedProperty"></param>
        public void UpdateInvProductExtendedProperty(InvProductExtendedProperty existingInvProductExtendedProperty)
        {
            existingInvProductExtendedProperty.ModifiedUser = Common.LoggedUser;
            existingInvProductExtendedProperty.ModifiedDate = Common.GetSystemDateWithTime();
            existingInvProductExtendedProperty.DataTransfer  =0; 
            this.context.Entry(existingInvProductExtendedProperty).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get Product Extended Property by Name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvProductExtendedProperty GetInvProductExtendedPropertyByName(string invProductExtendedPropertyName)
        {
            return context.InvProductExtendedProperties.Where(c => c.ExtendedPropertyName == invProductExtendedPropertyName).FirstOrDefault();
        }

        /// <summary>
        /// Get Product Extended PropertyCodes
        /// </summary>
        /// <returns></returns>
        //public string[] GetInvProductExtendedPropertyCodes()
        //{
        //    List<string> extendedPropertyCodesList = context.InvProductExtendedProperties.Where(v => v.IsDelete.Equals(false)).Select(v => v.ExtendedPropertyName).ToList();
        //    string[] extendedPropertyCodes = extendedPropertyCodesList.ToArray();
        //    return extendedPropertyCodes;
        //}

        /// <summary>
        /// Get Product Extended PropertyCodes
        /// </summary>
        /// <returns></returns>
        //public string[] GetInvProductExtendedPropertyNames() 
        //{
        //    List<string> extendedPropertyCodesList = context.InvProductExtendedProperties.Where(v => v.IsDelete.Equals(false)).Select(v => v.ExtendedPropertyCode).ToList();
        //    string[] extendedPropertyCodes = extendedPropertyCodesList.ToArray();
        //    return extendedPropertyCodes;
        //}


        public string[] GetInvProductExtendedPropertyCodes()  
        {
            List<string> ProductList = context.InvProductExtendedProperties.Where(c => c.IsDelete.Equals(false)).Select(c => c.ExtendedPropertyCode).ToList();
            return ProductList.ToArray();
        }

       
        public string[] GetInvProductExtendedPropertyNames()
        {
            List<string> ProductList = context.InvProductExtendedProperties.Where(c => c.IsDelete.Equals(false)).OrderBy(c=> c.ExtendedPropertyName).Select(c => c.ExtendedPropertyName).ToList();
            return ProductList.ToArray();
        }

        /// <summary>
        /// Get Property data types
        /// </summary>
        /// <returns></returns>
        public List<string> GetPropertyTypes()
        {
            List<string> propertyTypes = new List<string>();
            propertyTypes.Add("DATE");
            propertyTypes.Add("DECIMAL");
            propertyTypes.Add("INTEGER");
            propertyTypes.Add("STRING");

            return propertyTypes;
        }

        public DataTable GetExtendedPropertyDataTable() 
        {
            DataTable tblAreas = new DataTable();
            var query = from a in context.InvProductExtendedProperties
                        where a.IsDelete == false
                        select new
                        {
                            a.ExtendedPropertyCode,
                            a.ExtendedPropertyName,
                            a.DataType,
                            a.Parent
                        };
            return tblAreas = Common.LINQToDataTable(query);
        }

        /// <summary>
        /// Validate Product Extende Property parent
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="childCode"></param>
        /// <returns></returns>
        public bool ValidateParent(string parentCode, string childCode)
        {
            bool isParentValidated = false;
            
            InvProductExtendedProperty invProductExtendedProperty = new InvProductExtendedProperty();
            invProductExtendedProperty = GetInvProductExtendedPropertyByCode(parentCode.Trim());

            if (GetInvProductExtendedPropertyByCode(childCode.Trim()) != null)
            {
                if (string.Equals(GetInvProductExtendedPropertyByCode(parentCode.Trim()).ExtendedPropertyCode, childCode.Trim()))
                { isParentValidated = false; }
                else 
                { isParentValidated = true; }
            }
            else
            { isParentValidated = true; }
            
            return isParentValidated;
        }

        #endregion
    }
}
