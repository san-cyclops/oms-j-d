using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using System.Data.Entity;


namespace Service

{

    /// <summary>
    /// By Pravin - 24/07/2013
    /// Product PLU Link
    /// </summary>
    public class InvProductLinkService
    {
        ERPDbContext context = new ERPDbContext();
        InvProductLink invProductLink = new InvProductLink();

        #region methods


        /// Save new ProductLinkService

        public void AddProductLink(InvProductLink invProductLink)
        {

            context.InvProductLinks.Add(invProductLink);
            context.SaveChanges();
        }

        public void UpdateProductLinkStatus(long productID)
        {
            List<InvProductLink> invProductLinks = new List<InvProductLink>();
            InvProductLink invProductLink = new InvProductLink();
            invProductLinks = context.InvProductLinks.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < invProductLinks.Count; i++)
            {
                invProductLink = invProductLinks[i];
                invProductLink.IsDelete = true;
                UpdateProductLink(invProductLink);
            }
        }


        /// Update existing ProductLinkService

        public void UpdateProductLink(InvProductLink invProductLink)
        {
            invProductLink.DataTransfer = 0;
            this.context.Entry(invProductLink).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductLinkService by code

        public InvProductLink GetProductLinkByProductCode(long productID)
        {

            return context.InvProductLinks.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();

        }

        public InvProductLink GetProductLinkByProductCode(long productID, string productLinkCode)
        {
            return context.InvProductLinks.Where(p => p.ProductID == productID && p.ProductLinkCode == productLinkCode && p.IsDelete == false).FirstOrDefault();
        }

        public InvProductLink GetProductLinkWithDeletedByProductCode(long productID, string productLinkCode)
        {

            return context.InvProductLinks.Where(p => p.ProductID == productID && p.ProductLinkCode == productLinkCode).FirstOrDefault();

        }


        /// Get all ProductLinkService details 

        public List<InvProductLink> GetAllProductLinkByProductCode(long productID)
        {
            // return (from u in context.InvProductLinks
            // from um in context.UnitOfMeasures
            //           where u.InvProductLinkID == um.UnitOfMeasureID && u.ProductID == productID
            //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            return context.InvProductLinks.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();


        }


        //public string[] GetAllCustomerGroupNames()
        // {
        //    List<string> CustomerGroupNameList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupName).ToList();
        //     return CustomerGroupNameList.ToArray();

        // }




        #endregion
    }
}
