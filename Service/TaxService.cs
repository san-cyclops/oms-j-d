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
    public class TaxService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods.....

        /// <summary>
        /// Save Tax details
        /// </summary>
        /// <param name="Tax"></param>
        public void AddTax(Tax Tax)
        {
            context.Taxes.Add(Tax);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active Tax details by Tax code
        /// </summary>
        /// <param name="taxCode"></param>
        /// <returns></returns>
        public Tax GetTaxByCode(string taxCode) 
        {
            return context.Taxes.Where(t => t.TaxCode.Equals(taxCode) && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Tax details by Tax name
        /// </summary>
        /// <param name="taxName"></param>
        /// <returns></returns>
        public Tax GetTaxByName(string taxName)
        {
            return context.Taxes.Where(t => t.TaxName.Equals(taxName) && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Tax details by Tax ID
        /// </summary>
        /// <param name="taxID"></param>
        /// <returns></returns>
        public Tax GetTaxByID(int taxID) 
        {
            return context.Taxes.Where(t => t.TaxID.Equals(taxID) && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active Tax details
        /// </summary>
        /// <returns></returns>
        public List<Tax> GetAllTaxes()
        {
            return context.Taxes.Where(t => t.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Update Cost Tax details
        /// </summary>
        /// <param name="existingTax"></param>
        public void UpdateTax(Tax existingTax)
        {
            this.context.Entry(existingTax).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete Tax details
        /// </summary>
        /// <param name="existingTax"></param>
        public void DeleteTax(CostCentre existingTax) 
        {
            existingTax.IsDelete = true;
            this.context.Entry(existingTax).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        #endregion
    }
}
