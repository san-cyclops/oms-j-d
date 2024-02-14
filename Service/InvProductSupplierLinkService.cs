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
    public class InvProductSupplierLinkService
    {
        ERPDbContext context = new ERPDbContext();
        InvProductSupplierLink invProductSupplierLink = new InvProductSupplierLink();

        #region methods


        /// Save new InvProductSupplierLinkService

        public void AddInvProductSupplierLink(InvProductSupplierLink invProductSupplierLink)
        {
            context.InvProductSupplierLinks.Add(invProductSupplierLink);
            context.SaveChanges();
        }

        public void UpdateInvProductSupplierLinkStatus(long productID)
        {
            List<InvProductSupplierLink> InvProductSupplierLinks = new List<InvProductSupplierLink>();
            InvProductSupplierLink InvProductSupplierLink = new InvProductSupplierLink();
            InvProductSupplierLinks = context.InvProductSupplierLinks.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < InvProductSupplierLinks.Count; i++)
            {
                InvProductSupplierLink = InvProductSupplierLinks[i];
                InvProductSupplierLink.IsDelete = true;
                UpdateInvProductSupplierLink(InvProductSupplierLink);
            }
        }


        /// Update existing InvProductSupplierLinkService

        public void UpdateInvProductSupplierLink(InvProductSupplierLink InvProductSupplierLink)
        {
            InvProductSupplierLink.DataTransfer = 0;
            this.context.Entry(InvProductSupplierLink).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search InvProductSupplierLinkService by code

        public InvProductSupplierLink GetInvProductSupplierLinkByProductCode(long productID)
        {

            return context.InvProductSupplierLinks.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();

        }

        public InvProductSupplierLink GetInvProductSupplierLinkByProductCode(long productID, long supplierID)
        {

            return context.InvProductSupplierLinks.Where(p => p.ProductID == productID && p.SupplierID == supplierID && p.IsDelete == false).FirstOrDefault();

        }

        public InvProductSupplierLink GetInvProductSupplierLinkWithDeletedByProductCode(long productID, long supplierID)
        {
            return context.InvProductSupplierLinks.Where(p => p.ProductID == productID && p.SupplierID == supplierID).FirstOrDefault();
        }


        /// Get all InvProductSupplierLinkService details 

        public List<InvProductSupplierLink> GetAllInvProductSupplierLinkByProductCode(long productID)
        {
            // return (from u in context.InvProductSupplierLinks
            // from um in context.UnitOfMeasures
            //           where u.InvProductSupplierLinkID == um.UnitOfMeasureID && u.ProductID == productID
            //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            return context.InvProductSupplierLinks.Where(c => c.ProductID == productID && c.IsDelete == false).ToList();


        }


        public List<InvProductSupplierLink> GetSupplierLink(long productID)
        {
            List<InvProductSupplierLink> rtnList = new List<InvProductSupplierLink>();

            var query = (from s in context.Suppliers
                         join sl in context.InvProductSupplierLinks on s.SupplierID equals sl.SupplierID
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
                InvProductSupplierLink invProductSupplierLink = new InvProductSupplierLink();

                invProductSupplierLink.SupplierCode = temp.SupplierCode;
                invProductSupplierLink.SupplierName = temp.SupplierName;
                invProductSupplierLink.CostPrice = temp.CostPrice;
                invProductSupplierLink.FixedGP = temp.FixedGP;
                invProductSupplierLink.ProductID = temp.ProductID;
                invProductSupplierLink.SupplierID = temp.SupplierID;

                rtnList.Add(invProductSupplierLink);
            }
            return rtnList;
        }




        #endregion
    }
}
