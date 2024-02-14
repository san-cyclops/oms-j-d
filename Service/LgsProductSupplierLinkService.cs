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

namespace Service
{
    /// <summary>
    /// By Pravin - 25/07/2013
    /// Supplier Link
    /// </summary>
    public class LgsProductSupplierLinkService
    {
        ERPDbContext context = new ERPDbContext();
        LgsProductSupplierLink lgsProductSupplierLink = new LgsProductSupplierLink();

        #region methods


        /// Save new LgsProductSupplierLinkService

        public void AddLgsProductSupplierLink(LgsProductSupplierLink lgsProductSupplierLink)
        {
            context.LgsProductSupplierLinks.Add(lgsProductSupplierLink);
            context.SaveChanges();
        }

        public void UpdateLgsProductSupplierLinkStatus(long productID)
        {
            List<LgsProductSupplierLink> LgsProductSupplierLinks = new List<LgsProductSupplierLink>();
            LgsProductSupplierLink LgsProductSupplierLink = new LgsProductSupplierLink();
            LgsProductSupplierLinks = context.LgsProductSupplierLinks.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < LgsProductSupplierLinks.Count; i++)
            {
                LgsProductSupplierLink = LgsProductSupplierLinks[i];
                LgsProductSupplierLink.IsDelete = true;
                UpdateLgsProductSupplierLink(LgsProductSupplierLink);
            }
        }


        /// Update existing LgsProductSupplierLinkService

        public void UpdateLgsProductSupplierLink(LgsProductSupplierLink LgsProductSupplierLink)
        { 
            this.context.Entry(LgsProductSupplierLink).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search LgsProductSupplierLinkService by code

        public LgsProductSupplierLink GetLgsProductSupplierLinkByProductCode(long productID)
        {

            return context.LgsProductSupplierLinks.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();

        }

        public LgsProductSupplierLink GetLgsProductSupplierLinkByProductCode(long productID, long supplierID)
        {

            return context.LgsProductSupplierLinks.Where(p => p.ProductID == productID && p.SupplierID == supplierID && p.IsDelete == false).FirstOrDefault();

        }

        public LgsProductSupplierLink GetLgsProductSupplierLinkWithDeletedByProductCode(long productID, long supplierID)
        {
            return context.LgsProductSupplierLinks.Where(p => p.ProductID == productID && p.SupplierID == supplierID).FirstOrDefault();
        }


        /// Get all LgsProductSupplierLinkService details 

        public List<LgsProductSupplierLink> GetAllLgsProductSupplierLinkByProductCode(long productID)
        {
            // return (from u in context.InvProductSupplierLinks
            // from um in context.UnitOfMeasures
            //           where u.InvProductSupplierLinkID == um.UnitOfMeasureID && u.ProductID == productID
            //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            return context.LgsProductSupplierLinks.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();


        }


        public List<LgsProductSupplierLink> GetSupplierLink(long productID)
        {
            List<LgsProductSupplierLink> rtnList = new List<LgsProductSupplierLink>();

            var query = (from s in context.Suppliers
                         join sl in context.LgsProductSupplierLinks on s.SupplierID equals sl.SupplierID
                         where sl.IsDelete == false && sl.ProductID.Equals(productID)
                         orderby s.SupplierID
                         select new
                         {
                             s.SupplierCode,
                             s.SupplierName,
                             sl.SupplierID,
                             sl.CostPrice,
                             sl.FixedGP,
                             sl.ProductID,
                             sl.ReferenceDocumentID,
                             sl.ReferenceDocumentNo
                         }).ToArray();

            foreach (var temp in query)
            {
                LgsProductSupplierLink lgsProductSupplierLink = new LgsProductSupplierLink();

                lgsProductSupplierLink.SupplierCode = temp.SupplierCode;
                lgsProductSupplierLink.SupplierName = temp.SupplierName;
                lgsProductSupplierLink.CostPrice = temp.CostPrice;
                lgsProductSupplierLink.FixedGP = temp.FixedGP;
                lgsProductSupplierLink.ProductID = temp.ProductID;
                lgsProductSupplierLink.SupplierID = temp.SupplierID;

                rtnList.Add(lgsProductSupplierLink);
            }
            return rtnList;
        }




        #endregion
    }
}
