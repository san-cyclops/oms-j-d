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
    public class LgsProductUnitConversionService
    {

        /// <summary>
        /// By Pravin 
        /// Product PLU Link
        /// </summary>
        /// 
        ERPDbContext context = new ERPDbContext();
        LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();

        #region methods


        /// Save new ProductUnitConversionService

        public void AddProductUnitConversion(LgsProductUnitConversion lgsProductUnitConversion)
        {
            context.LgsProductUnitConversions.Add(lgsProductUnitConversion);
            context.SaveChanges();
        }

        public void UpdateProductUnitConversionStatus(long productID)
        {
            List<LgsProductUnitConversion> lgsProductUnitConversions = new List<LgsProductUnitConversion>();
            LgsProductUnitConversion lgsProductUnitConversion = new LgsProductUnitConversion();
            lgsProductUnitConversions=context.LgsProductUnitConversions.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < lgsProductUnitConversions.Count; i++)
            {
                lgsProductUnitConversion = lgsProductUnitConversions[i];
                lgsProductUnitConversion.IsDelete = true;
                UpdateProductUnitConversion(lgsProductUnitConversion);
            }
        }


        /// Update existing ProductUnitConversionService

        public void UpdateProductUnitConversion(LgsProductUnitConversion lgsProductUnitConversion)
        {
            this.context.Entry(lgsProductUnitConversion).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        /// Search ProductUnitConversionService by code

        public LgsProductUnitConversion GetProductUnitConversionByProductCode(long productID)
        {

            return context.LgsProductUnitConversions.Where(p => p.ProductID == productID && p.IsDelete == false).FirstOrDefault();

        }

        public LgsProductUnitConversion GetProductUnitByProductCode(long productID, long unitOfMeasureID)
        {
            return context.LgsProductUnitConversions.Where(c => c.ProductID == productID && c.UnitOfMeasureID == unitOfMeasureID && c.IsDelete == false).FirstOrDefault();
        }

        public LgsProductUnitConversion GetProductUnitWithDeletedByProductCode(long productID, long unitOfMeasureID)
        {
            return context.LgsProductUnitConversions.Where(c => c.ProductID == productID && c.UnitOfMeasureID == unitOfMeasureID).FirstOrDefault();
        }


        /// Get all ProductUnitConversionService details 

        public List<LgsProductUnitConversion> GetAllProductUnitConversionByProductCode(long productID)
        {
            // return (from u in context.InvProductUnitConversions
            // from um in context.UnitOfMeasures
           //           where u.InvProductUnitConversionID == um.UnitOfMeasureID && u.ProductID == productID
           //  select new { u.ProductName,um.UnitOfMeasureName,u.ConvertFactor,u.SellingPrice,u.MinimumPrice }).ToList();

            //return context.InvProductUnitConversions.Where(c => c.ProductID == productID && c.IsDelete==false).ToList();

            List<LgsProductUnitConversion> retnList = new List<LgsProductUnitConversion>();

            var qry = (from puc in context.LgsProductUnitConversions
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
                LgsProductUnitConversion tmpLgsProductUnitConversion = new LgsProductUnitConversion();

                tmpLgsProductUnitConversion.ProductID = temp.ProductID;
                tmpLgsProductUnitConversion.Description = temp.Description;
                tmpLgsProductUnitConversion.UnitOfMeasureID = temp.UnitOfMeasureID;
                tmpLgsProductUnitConversion.ConvertFactor = temp.ConvertFactor;
                tmpLgsProductUnitConversion.CostPrice = temp.CostPrice;
                tmpLgsProductUnitConversion.SellingPrice = temp.SellingPrice;
                tmpLgsProductUnitConversion.MinimumPrice = temp.MinimumPrice;
                tmpLgsProductUnitConversion.LineNo = temp.LineNo;
                tmpLgsProductUnitConversion.UnitOfMeasureName = temp.UnitOfMeasureName;

                retnList.Add(tmpLgsProductUnitConversion);
            }

            return retnList;
        }

        public LgsProductUnitConversion GetProductUnitConversionByProductCodeByUnit(long productID, long unitOfMeasureID)
        {

            return context.LgsProductUnitConversions.Where(p => p.ProductID == productID && p.UnitOfMeasureID == unitOfMeasureID && p.IsDelete == false).FirstOrDefault();

        }


        //public string[] GetAllCustomerGroupNames()
       // {
        //    List<string> CustomerGroupNameList = context.CustomerGroups.Where(c => c.IsDelete.Equals(false)).Select(c => c.CustomerGroupName).ToList();
       //     return CustomerGroupNameList.ToArray();

       // }


      

        #endregion
    }
}
