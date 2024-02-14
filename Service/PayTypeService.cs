using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class PayTypeService
    {
        #region Methods
        /// <summary>
        /// Get PayTypes Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetPayTypesCodes()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> payTypeCodesList = context.PayTypes.Where(l => l.IsDelete.Equals(false)).Select(l => l.PaymentID.ToString()).ToList();
                string[] payTypesCodes = payTypeCodesList.ToArray();
                return payTypesCodes;
            }
        }

        /// <summary>
        /// Get PayType Names
        /// </summary>
        /// <returns></returns>
        public string[] GetPayTypesNames()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> payTypeNamesList = context.PayTypes.Where(l => l.IsDelete.Equals(false)).Select(l => l.Descrip).ToList();
                string[] payTypeNames = payTypeNamesList.ToArray();
                return payTypeNames;
            }
        }

        /// <summary>
        /// Search PayType by code
        /// </summary>
        /// <param name="paymentTypeCode"></param>
        /// <returns></returns>
        public PayType GetPayTypeByCode(int paymentTypeCode)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.PayTypes.Where(l => l.PaymentID == paymentTypeCode).FirstOrDefault();
            }
        }

        /// <summary>
        /// Search PayType by ID
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns></returns>
        public PayType GetPayTypeByID(long paymentTypeID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.PayTypes.Where(l => l.PayTypeID == paymentTypeID && l.IsDelete == false).FirstOrDefault();
            }
        }

        /// <summary>
        /// Search PayType by name
        /// </summary>
        /// <param name="paymentTypeName"></param>
        /// <returns></returns>
        public PayType GetPayTypeByName(string paymentTypeName)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.PayTypes.Where(l => l.Descrip == paymentTypeName && l.IsDelete == false).FirstOrDefault();
            }
        }
        #endregion
    }
}
