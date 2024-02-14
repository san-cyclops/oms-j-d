using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using System.Data.Entity.Validation;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class CustomerService
    {

        ERPDbContext context = new ERPDbContext();
        Customer customer = new Customer();

        #region methods

        /// <summary>
        /// 
        /// Save new Customer
        /// </summary>
        /// <param name="Customer"></param>
        public void AddCustomer(Customer customer)
        {
           
                context.Customers.Add(customer);
                context.SaveChanges();
            
       }

        /// <summary>
        /// Update existing Customer
        /// </summary>
        /// <param name="Customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            customer.ModifiedUser = Common.LoggedUser;
            customer.AreaID = 1;
            customer.PaymentMethodID = 1;
            customer.CustomerGroupID = 1;
            customer.TerritoryID = 1;


            customer.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(customer).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Search Customer by code
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public Customer GetCustomersByCode(string customerCode)
        {
            return context.Customers.Where(c => c.CustomerCode == customerCode && c.IsDelete.Equals(false)).FirstOrDefault();
        }
        /// <summary>
        /// Get Customer by Name
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public Customer GetCustomersByName(string customerName)
        {
            return context.Customers.Where(c => c.CustomerName == customerName && c.IsDelete.Equals(false)).FirstOrDefault();
        }
        /// <summary>
        /// Get all Customer details 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            return context.Customers.Where(c => c.IsDelete == false).ToList();
        }
        public string[] GetAllCustomerCodes()
        {
            List<string> CustomerCodeList = context.Customers.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerCode).ToList();
            return CustomerCodeList.ToArray();
        }

        public string[] GetAllCustomerNames()
        {
            List<string> CustomerNameList = context.Customers.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerName).ToList();
            return CustomerNameList.ToArray();
        }

        public bool IsExistsCustomer(string customerCode) 
        {
            if (GetCustomersByCode(customerCode) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get active Customer details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveCustomersDataTable() 
        {
            DataTable tblCustomer = new DataTable();
            var query = from d in context.Customers 
                        where d.IsDelete == false 
                        select new 
                        { 
                            d.CustomerCode, 
                            d.CustomerName, 
                            d.BillingMobile,
                            d.BillingTelephone,
                            d.ContactPersonName, 
                            d.CreditLimit, 
                            d.CreditPeriod, 
                            d.ChequeLimit, 
                            d.ChequePeriod 
                        };
            return tblCustomer = Common.LINQToDataTable(query);
        }


        #region GetNewCode

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.Customers.Max(c => c.CustomerCode);
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }


        #endregion

        public Customer GetCustomersById(long customerId)
        {

            return context.Customers.Where(c => c.CustomerID == customerId && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public long[] GetCustomerIdRangeByCustomerNames(string nameFrom, string nameTo)
        {
            return context.Customers.Where(d => d.IsDelete.Equals(false) && d.CustomerName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.CustomerName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.CustomerID).ToArray();
        }


        #endregion
    }
}
