using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;
using Utility;

namespace Service
{
    public class SupplierPropertyService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new SupplierProperty
        /// </summary>
        /// <param name="supplierProperty"></param>
        public void AddSupplierProperty(SupplierProperty supplierProperty)
        {
            context.SupplierProperties.Add(supplierProperty);

            List<ValidationResult> errors = new List<ValidationResult>();
            ValidationContext contextVal = new ValidationContext(supplierProperty, null, null);

            if (!Validator.TryValidateObject(supplierProperty, contextVal, errors, true))
            {
                Console.WriteLine("Cannot save data:");
                foreach (ValidationResult e in errors)
                    Console.WriteLine(e.ErrorMessage);
            }
            else
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Update existing SupplierProperty
        /// </summary>
        /// <param name="supplierProperty"></param>
        public void UpdateSupplierProperty(SupplierProperty supplierProperty)
        {
            supplierProperty.ModifiedUser = Common.LoggedUser;
            supplierProperty.ModifiedDate = Common.GetSystemDateWithTime();
            supplierProperty.DataTransfer = 0;
            context.Entry(supplierProperty).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search SupplierProperty by code
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public SupplierProperty GetSupplierPropertyByCode(string supplierCode)
        {
            return context.SupplierProperties.Where(s => s.SupplierCode == supplierCode && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search SupplierProperty by ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public SupplierProperty GetSupplierPropertyByID(long supplierID)
        {
            return context.SupplierProperties.Where(s => s.SupplierID == supplierID && s.IsDelete == false).FirstOrDefault();
        }
        #endregion
    }
}
