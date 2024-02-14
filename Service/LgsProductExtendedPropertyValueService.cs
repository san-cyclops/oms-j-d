using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Domain;
using Data;
using System.Data;
using System.Transactions;
using System.Data.Entity;
using Utility;

namespace Service
{
    public class LgsProductExtendedPropertyValueService
    {
        ERPDbContext context = new ERPDbContext();

        /// <summary>
        /// Add or Update products extended property values of grid view
        /// </summary>
        /// <param name="lgsProductExtendedPropertyValue"></param>
        /// <returns></returns>
        public List<LgsProductExtendedPropertyValue> GetLgsProductExtendedPropertyValueTempList(List<LgsProductExtendedPropertyValue> lgsProductExtendedPropertyValueList, LgsProductExtendedPropertyValue lgsProductExtendedPropertyValue)
        {
            //if (invProductExtendedPropertyValues.Where(p => p.InvProductExtendedPropertyID == invProductExtendedPropertyValue.InvProductExtendedPropertyID).FirstOrDefault() != null)
            //{
            //    // Update Existing Record
            //    //invProductExtendedPropertyValue.LineNo = invPurchaseOrderDetailsTemp.Where(pr => pr.ProductCode == invProductExtendedPropertyValue.ProductCode).FirstOrDefault().LineNo;
            //    invProductExtendedPropertyValues.RemoveAll(p => p.InvProductExtendedPropertyID == invProductExtendedPropertyValue.InvProductExtendedPropertyID);
            //    invProductExtendedPropertyValues.Add(invProductExtendedPropertyValue);
            //}
            //else
            //{
            //    // Insert New Record
            //    //if (int.Equals(invProductExtendedPropertyValues.Count, 0))
            //    ////{ invProductExtendedPropertyValue.LineNo = 1; }
            //    //else
            //    //{ invProductExtendedPropertyValue.LineNo = invPurchaseOrderDetailsTemp.Max(ln => ln.LineNo) + 1; }
            //    invProductExtendedPropertyValues.Add(invProductExtendedPropertyValue);
            //}
            //return invProductExtendedPropertyValues;

            // By Nuwan

            lgsProductExtendedPropertyValueList.RemoveAll(p => p.LgsProductExtendedPropertyID == lgsProductExtendedPropertyValue.LgsProductExtendedPropertyID);
            lgsProductExtendedPropertyValueList.Add(lgsProductExtendedPropertyValue);

            return lgsProductExtendedPropertyValueList;

        }

        /// <summary>
        /// Save new Product Extended Value
        /// </summary>
        /// <param name="invProductExtendedValue"></param>
        public void AddLgsProductExtendedPropertyValues(LgsProductExtendedPropertyValue lgsProductExtendedPropertyValues)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (lgsProductExtendedPropertyValues.LgsProductExtendedPropertyValueID.Equals(0))
                {
                    context.LgsProductExtendedPropertyValues.Add(lgsProductExtendedPropertyValues);
                }
                else
                {
                    this.context.Entry(lgsProductExtendedPropertyValues).State = EntityState.Modified;
                }

                context.SaveChanges();
                transactionScope.Complete();
            }
        }

        ///// <summary>
        ///// Save new Product Extended Value
        ///// </summary>
        ///// <param name="invProductExtendedValue"></param>
        //public void AddInvProductExtendedPropertyValues(List<InvProductExtendedPropertyValue> invProductExtendedPropertyValues)
        //{
        //    using (TransactionScope transactionScope = new TransactionScope())
        //    {
        //        foreach (InvProductExtendedPropertyValue invProductExtendedPropertyValue in invProductExtendedPropertyValues)
        //        {
        //            if (invProductExtendedPropertyValue.InvProductExtendedPropertyValueID.Equals(0))
        //            {
        //                foreach (InvProductExtendedPropertyValue temp in invProductExtendedPropertyValues)
        //                {
        //                    long ectendedID = invProductExtendedPropertyValue.InvProductExtendedPropertyID;
        //                    long productID = invProductExtendedPropertyValue.ProductID;
        //                    context.Set<InvProductExtendedPropertyValue>().Delete(context.InvProductExtendedPropertyValues.Where(pepv => pepv.InvProductExtendedPropertyID.Equals(ectendedID) && pepv.ProductID.Equals(productID)).AsQueryable());
        //                }
        //                context.InvProductExtendedPropertyValues.Add(invProductExtendedPropertyValue);
        //            }
        //            else
        //            {
        //                this.context.Entry(invProductExtendedPropertyValue).State = EntityState.Modified;
        //            }
        //        }
        //        context.SaveChanges();
        //        transactionScope.Complete();
        //    }
        //}

        public List<LgsProductExtendedPropertyValue> GetProductExtendedPropertyValuesByProductID(long productID)
        {
            //by Nuwan 31/10/2013
            List<LgsProductExtendedPropertyValue> rtnList = new List<LgsProductExtendedPropertyValue>();

            var queryResult = (from pepv in context.LgsProductExtendedPropertyValues
                               join pep in context.InvProductExtendedProperties on pepv.LgsProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               join pev in context.InvProductExtendedValues on pepv.LgsProductExtendedValueID equals pev.InvProductExtendedValueID
                               where pepv.ProductID == productID && pepv.IsDelete == false
                               select new
                               {
                                   pepv.LgsProductExtendedPropertyID,
                                   pepv.LgsProductExtendedValueID,
                                   pep.ExtendedPropertyName,
                                   pev.ValueData
                               }).ToArray();

            foreach (var temp in queryResult)
            {
                LgsProductExtendedPropertyValue tmpLgsProductExtendedPropertyValue = new LgsProductExtendedPropertyValue();
                tmpLgsProductExtendedPropertyValue.ExtendedPropertyName = temp.ExtendedPropertyName;
                tmpLgsProductExtendedPropertyValue.ValueData = temp.ValueData;
                tmpLgsProductExtendedPropertyValue.LgsProductExtendedPropertyID = temp.LgsProductExtendedPropertyID;
                tmpLgsProductExtendedPropertyValue.LgsProductExtendedValueID = temp.LgsProductExtendedValueID;

                rtnList.Add(tmpLgsProductExtendedPropertyValue);
            }

            return rtnList;

            //var x = context.InvProductExtendedPropertyValues // Source table
            //    .Join(context.InvProductMasters// Target table
            //            , v => v.ProductID // EmployeeID of Employees table
            //            , p => p.InvProductMasterID // EmployeeID of Orders table
            //            , (v, p) => new { InvProductExtendedPropertyValues = v, InvProductMasters = p })
            //    .Select(s => new { s.InvProductExtendedPropertyValues, s.InvProductMasters });

            //return context.InvProductExtendedPropertyValues.Where(p => p.ProductID == productID && p.IsDelete == false).ToList();
        }


        public void UpdateProductExtendedPropertyValueStatus(long productID)
        {
            List<LgsProductExtendedPropertyValue> lgsProductExtendedPropertyValue = new List<LgsProductExtendedPropertyValue>();
            LgsProductExtendedPropertyValue lgsProductExtendedPropertyValueSave = new LgsProductExtendedPropertyValue();
            lgsProductExtendedPropertyValue = context.LgsProductExtendedPropertyValues.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < lgsProductExtendedPropertyValue.Count; i++)
            {
                lgsProductExtendedPropertyValueSave = lgsProductExtendedPropertyValue[i];
                lgsProductExtendedPropertyValueSave.IsDelete = true;
                UpdateProductExtendedPropertyValue(lgsProductExtendedPropertyValueSave);
            }
        }


        /// Update existing ProductUnitConversionService

        public void UpdateProductExtendedPropertyValue(LgsProductExtendedPropertyValue lgsProductExtendedPropertyValue)
        {
            lgsProductExtendedPropertyValue.ModifiedUser = Common.LoggedUser;
            lgsProductExtendedPropertyValue.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(lgsProductExtendedPropertyValue).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public LgsProductExtendedPropertyValue GetProductExtendedPropertyValueByProductID(long productID)
        {
            return context.LgsProductExtendedPropertyValues.Where(c => c.ProductID == productID && c.IsDelete == false).FirstOrDefault();
        }
    }
}
