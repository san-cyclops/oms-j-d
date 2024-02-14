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
    public class LgsSupplierPropertyService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new logistic SupplierProperty
        /// </summary>
        /// <param name="lgsSupplierProperty"></param>
        public void AddLgsSupplierProperty(LgsSupplierProperty lgsSupplierProperty)
        {
            context.LgsSupplierProperties.Add(lgsSupplierProperty);

            List<ValidationResult> errors = new List<ValidationResult>();
            ValidationContext contextVal = new ValidationContext(lgsSupplierProperty, null, null);

            if (!Validator.TryValidateObject(lgsSupplierProperty, contextVal, errors, true))
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
        /// Update existing logistic SupplierProperty
        /// </summary>
        /// <param name="lgsSupplierProperty"></param>
        public void UpdateLgsSupplierProperty(LgsSupplierProperty lgsSupplierProperty)
        {
            lgsSupplierProperty.ModifiedUser = Common.LoggedUser;
            lgsSupplierProperty.ModifiedDate = Common.GetSystemDateWithTime();
            context.Entry(lgsSupplierProperty).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search logistic SupplierProperty by code
        /// </summary>
        /// <param name="lgsSupplierCode"></param>
        /// <returns></returns>
        public LgsSupplierProperty GetLgsSupplierPropertyByCode(string lgsSupplierCode)
        {
            return context.LgsSupplierProperties.Where(s => s.SupplierCode == lgsSupplierCode && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search logistic SupplierProperty by ID
        /// </summary>
        /// <param name="lgsSupplierID"></param>
        /// <returns></returns>
        public LgsSupplierProperty GetLgsSupplierPropertyByID(long lgsSupplierID)
        {
            return context.LgsSupplierProperties.Where(s => s.LgsSupplierID == lgsSupplierID && s.IsDelete == false).FirstOrDefault();
        }
        #endregion
    }
}
