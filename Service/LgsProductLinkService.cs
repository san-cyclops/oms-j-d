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
    /// By Nuwan - 22/11/2013
    /// Product PLU Link
    /// </summary>
    public class LgsProductLinkService
    {
        ERPDbContext context = new ERPDbContext();
        LgsProductLink lgsProductLink = new LgsProductLink();

        #region methods


        /// Save new ProductLinkService

        public void AddProductLink(LgsProductLink lgsProductLink)
        {
            context.LgsProductLinks.Add(lgsProductLink);
            context.SaveChanges();
        }

        public void UpdateProductLinkStatus(long productID)
        {
            List<LgsProductLink> lgsProductLinks = new List<LgsProductLink>();
            LgsProductLink lgsProductLink = new LgsProductLink();
            lgsProductLinks = context.LgsProductLinks.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < lgsProductLinks.Count; i++)
            {
                lgsProductLink = lgsProductLinks[i];
                lgsProductLink.IsDelete = true;
                UpdateProductLink(lgsProductLink);
            }
        }


        /// Update existing ProductLinkService

        public void UpdateProductLink(LgsProductLink lgsProductLink)
        {
            this.context.Entry(lgsProductLink).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductLinkService by code

        public LgsProductLink GetProductLinkByProductCode(long productID)
        {
            return context.LgsProductLinks.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();
        }

        public LgsProductLink GetProductLinkByProductCode(long productID, string productLinkCode)
        {
            return context.LgsProductLinks.Where(p => p.ProductID == productID && p.ProductLinkCode == productLinkCode && p.IsDelete == false).FirstOrDefault();
        }

        public LgsProductLink GetProductLinkWithDeletedByProductCode(long productID, string productLinkCode)
        {
            return context.LgsProductLinks.Where(p => p.ProductID == productID && p.ProductLinkCode == productLinkCode).FirstOrDefault();
        }


        /// Get all ProductLinkService details 

        public List<LgsProductLink> GetAllProductLinkByProductCode(long productID)
        {
            // return (from u in context.InvProductLinks
            // from um in context.UnitOfMeasures
            //           where u.InvProductLinkID == um.UnitOfMeasureID && u.ProductID == productID
            //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            return context.LgsProductLinks.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();
        }


        //public string[] GetAllCustomerGroupNames()
        // {
        //    List<string> CustomerGroupNameList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupName).ToList();
        //     return CustomerGroupNameList.ToArray();

        // }

        #endregion
    }
}
