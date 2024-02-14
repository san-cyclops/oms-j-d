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
    public class InvProductUnitConversionService
    {

        /// <summary>
        /// By Pravin 
        /// Product PLU Link
        /// </summary>
        /// 
        ERPDbContext context = new ERPDbContext();
        InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();

        #region methods


        /// Save new ProductUnitConversionService

        public void AddProductUnitConversion(InvProductUnitConversion invProductUnitConversion)
        {

            context.InvProductUnitConversions.Add(invProductUnitConversion);
            context.SaveChanges();
        }

        public void UpdateProductUnitConversionStatus(long productID)
        {
            List<InvProductUnitConversion> invProductUnitConversions = new List<InvProductUnitConversion>();
            InvProductUnitConversion invProductUnitConversion = new InvProductUnitConversion();
            invProductUnitConversions=context.InvProductUnitConversions.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < invProductUnitConversions.Count; i++)
            {
                invProductUnitConversion = invProductUnitConversions[i];
                invProductUnitConversion.IsDelete = true;
                UpdateProductUnitConversion(invProductUnitConversion);
            }
        }


        /// Update existing ProductUnitConversionService

        public void UpdateProductUnitConversion(InvProductUnitConversion invProductUnitConversion)
        {
            this.context.Entry(invProductUnitConversion).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductUnitConversionService by code

        public InvProductUnitConversion GetProductUnitConversionByProductCode(long productID)
        {

            return context.InvProductUnitConversions.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();

        }

        public InvProductUnitConversion GetProductUnitByProductCode(long productID, long unitOfMeasureID)
        {
            return context.InvProductUnitConversions.Where(c => c.ProductID == productID && c.UnitOfMeasureID == unitOfMeasureID && c.IsDelete == false).FirstOrDefault();
        }

        public InvProductUnitConversion GetProductUnitWithDeletedByProductCode(long productID, long unitOfMeasureID)
        {
            return context.InvProductUnitConversions.Where(c => c.ProductID == productID && c.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
        }


        /// Get all ProductUnitConversionService details 

        public List<InvProductUnitConversion> GetAllProductUnitConversionByProductCode(long productID)
        {
            // return (from u in context.InvProductUnitConversions
            // from um in context.UnitOfMeasures
           //           where u.InvProductUnitConversionID == um.UnitOfMeasureID && u.ProductID == productID
           //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            //return context.InvProductUnitConversions.Where(c => c.ProductID == productID && c.IsDelete==false).ToList();

            List<InvProductUnitConversion> retnList = new List<InvProductUnitConversion>();

            var qry = (from puc in context.InvProductUnitConversions
                       join um in context.UnitOfMeasures on puc.UnitOfMeasureID equals um.UnitOfMeasureID
                       where puc.ProductID == productID && puc.IsDelete == false
                       select new
                       {
                           puc.ProductID,
                           puc.Description,
                           puc.UnitOfMeasureID,
                           puc.ConvertFactor,
                           puc.CostPrice,
                           puc.SellingPrice,
                           puc.MinimumPrice,
                           puc.LineNo,
                           um.UnitOfMeasureName
                       }).ToArray();

            foreach (var temp in qry)
            {
                InvProductUnitConversion tmpInvProductUnitConversion = new InvProductUnitConversion();

                tmpInvProductUnitConversion.ProductID = temp.ProductID;
                tmpInvProductUnitConversion.Description = temp.Description;
                tmpInvProductUnitConversion.UnitOfMeasureID = temp.UnitOfMeasureID;
                tmpInvProductUnitConversion.ConvertFactor = temp.ConvertFactor;
                tmpInvProductUnitConversion.CostPrice = temp.CostPrice;
                tmpInvProductUnitConversion.SellingPrice = temp.SellingPrice;
                tmpInvProductUnitConversion.MinimumPrice = temp.MinimumPrice;
                tmpInvProductUnitConversion.LineNo = temp.LineNo;
                tmpInvProductUnitConversion.UnitOfMeasureName = temp.UnitOfMeasureName;

                retnList.Add(tmpInvProductUnitConversion);
            }

            return retnList;
        }

        public InvProductUnitConversion GetProductUnitConversionByProductCodeByUnit(long productID, long unitOfMeasureID)
        {

            return context.InvProductUnitConversions.Where(p => p.ProductID == productID && p.UnitOfMeasureID == unitOfMeasureID && p.IsDelete == false).FirstOrDefault();

        }


        //public string[] GetAllCustomerGroupNames()
       // {
        //    List<string> CustomerGroupNameList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupName).ToList();
       //     return CustomerGroupNameList.ToArray();

       // }


      

        #endregion
    }
}
