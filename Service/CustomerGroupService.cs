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
    public class CustomerGroupService
    {

        ERPDbContext context = new ERPDbContext();
        CustomerGroup customerGroup = new CustomerGroup();


        #region methods

        
        /// Save new CustomerGroup
       
        public void AddCustomerGroup(CustomerGroup customerGroup)
        {

            context.CustomerGroups.Add(customerGroup);
            context.SaveChanges();
        }

        
        /// Update existing CustomerGroup
       
        public void UpdateCustomerGroup(CustomerGroup customerGroup)
        {
            customerGroup.ModifiedUser = Common.LoggedUser;
            customerGroup.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(customerGroup).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        
        /// Search CustomerGroup by code
       
        public CustomerGroup GetCustomerGroupsByCode(string customerGroupCode)
        {

            return context.CustomerGroups.Where(c => c.CustomerGroupCode == customerGroupCode && c.IsDelete==false).FirstOrDefault();

        }

        public CustomerGroup GetCustomerGroupByName(string customerGroupName)
        {
            return context.CustomerGroups.Where(c => c.CustomerGroupName.Equals(customerGroupName) && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public CustomerGroup GetCustomerGroupByID(int customerGroupID)
        {
            return context.CustomerGroups.Where(c => c.CustomerGroupID.Equals(customerGroupID) && c.IsDelete.Equals(false)).FirstOrDefault();
        }
       
        /// Get all CustomerGroup details 
        
        public List<CustomerGroup> GetAllCustomerGroups()
        {
            return context.CustomerGroups.Where(c => c.IsDelete==false).ToList();
            

        }


        public DataTable GetAllVustomerGroupDataTable() 
        {
            DataTable tblAreas = new DataTable();
            var query = from a in context.CustomerGroups 
                        where a.IsDelete == false 
                        select new 
                        { 
                            a.CustomerGroupCode, 
                            a.CustomerGroupName, 
                            a.Remark 
                        };
            return tblAreas = Common.LINQToDataTable(query);
        }
     

        public  List<string>  GetAllCustomerGroupsArray()
        {
            List<string> list=context.CustomerGroups.Where(c => c.IsDelete==false).Select(c=> c.CustomerGroupCode).ToList();
            return list;


        }

        #region GetNewCode

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode=string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;
            
            getNewCode=  context.CustomerGroups.Max(c => c.CustomerGroupCode);
            if (getNewCode!=null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length+getNewCode.Length))) + getNewCode;
            return getNewCode;
        }

        #endregion


        public string[] GetAllCustomerGroupCodes()
        {
            List<string> CustomerGroupCodeList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupCode).ToList();
            return CustomerGroupCodeList.ToArray();
            
        }

       

        public string[] GetAllCustomerGroupNames()
        {
            List<string> CustomerGroupNameList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupName).ToList();
            return CustomerGroupNameList.ToArray();

        }

        #endregion

    }
}
