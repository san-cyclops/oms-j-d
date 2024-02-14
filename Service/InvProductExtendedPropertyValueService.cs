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
using MoreLinq;

namespace Service
{
    /// <summary>
    /// developed by asanka
    /// </summary>
    public class InvProductExtendedPropertyValueService
    {
        ERPDbContext context = new ERPDbContext();

        /// <summary>
        /// Add or Update products extended property values of grid view
        /// </summary>
        /// <param name="invProductExtendedPropertyValue"></param>
        /// <returns></returns>
        public List<InvProductExtendedPropertyValue> GetInvProductExtendedPropertyValueTempList(List<InvProductExtendedPropertyValue> invProductExtendedPropertyValueList, InvProductExtendedPropertyValue invProductExtendedPropertyValue)
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

            invProductExtendedPropertyValueList.RemoveAll(p => p.InvProductExtendedPropertyID == invProductExtendedPropertyValue.InvProductExtendedPropertyID);
            invProductExtendedPropertyValueList.Add(invProductExtendedPropertyValue);

            return invProductExtendedPropertyValueList;

        }

        /// <summary>
        /// Save new Product Extended Value
        /// </summary>
        /// <param name="invProductExtendedValue"></param>
        public void AddInvProductExtendedPropertyValues(InvProductExtendedPropertyValue invProductExtendedPropertyValues)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (invProductExtendedPropertyValues.InvProductExtendedPropertyValueID.Equals(0))
                {
                    context.InvProductExtendedPropertyValues.Add(invProductExtendedPropertyValues);
                }
                else
                {
                    invProductExtendedPropertyValues.DataTransfer = 0;
                    this.context.Entry(invProductExtendedPropertyValues).State = EntityState.Modified;
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

        public List<InvProductExtendedPropertyValue> GetProductExtendedPropertyValuesByProductID(long productID)
        {
            //by Nuwan 31/10/2013
            List<InvProductExtendedPropertyValue> rtnList = new List<InvProductExtendedPropertyValue>();

            var queryResult = (from pepv in context.InvProductExtendedPropertyValues
                               join pep in context.InvProductExtendedProperties on pepv.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               join pev in context.InvProductExtendedValues on pepv.InvProductExtendedValueID equals pev.InvProductExtendedValueID
                               where pepv.ProductID == productID && pepv.IsDelete == false
                               select new
                               {
                                   pepv.InvProductExtendedPropertyID,
                                   pepv.InvProductExtendedValueID,
                                   pep.ExtendedPropertyName,
                                   pev.ValueData
                               }).ToArray();

            foreach (var temp in queryResult)
            {
                InvProductExtendedPropertyValue tmpInvProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
                tmpInvProductExtendedPropertyValue.ExtendedPropertyName = temp.ExtendedPropertyName;
                tmpInvProductExtendedPropertyValue.ValueData = temp.ValueData;
                tmpInvProductExtendedPropertyValue.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
                tmpInvProductExtendedPropertyValue.InvProductExtendedValueID = temp.InvProductExtendedValueID;

                rtnList.Add(tmpInvProductExtendedPropertyValue);
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

        //public List<InvProductExtendedPropertyValue> GetProductExtendedPropertyValuesByProductCode(string productCode)
        //{
            
        //    List<InvProductExtendedPropertyValue> rtnList = new List<InvProductExtendedPropertyValue>();

        //    var queryResult = (from pepv in context.InvProductExtendedPropertyValues
        //                       join pep in context.InvProductExtendedProperties on pepv.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
        //                       join pev in context.InvProductExtendedValues on pepv.InvProductExtendedValueID equals pev.InvProductExtendedValueID
        //                       join pm in context.InvProductMasters on 
        //                       where pepv.ProductID == productID && pepv.IsDelete == false
        //                       select new
        //                       {
        //                           pepv.InvProductExtendedPropertyID,
        //                           pepv.InvProductExtendedValueID,
        //                           pep.ExtendedPropertyName,
        //                           pev.ValueData
        //                       }).ToArray();

        //    foreach (var temp in queryResult)
        //    {
        //        InvProductExtendedPropertyValue tmpInvProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
        //        tmpInvProductExtendedPropertyValue.ExtendedPropertyName = temp.ExtendedPropertyName;
        //        tmpInvProductExtendedPropertyValue.ValueData = temp.ValueData;
        //        tmpInvProductExtendedPropertyValue.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
        //        tmpInvProductExtendedPropertyValue.InvProductExtendedValueID = temp.InvProductExtendedValueID;

        //        rtnList.Add(tmpInvProductExtendedPropertyValue);
        //    }

        //    return rtnList;
        //}

        public void UpdateProductExtendedPropertyValueStatus(long productID)  
        {
            List<InvProductExtendedPropertyValue> invProductExtendedPropertyValue = new List<InvProductExtendedPropertyValue>();
            InvProductExtendedPropertyValue invProductExtendedPropertyValueSave = new InvProductExtendedPropertyValue();
            invProductExtendedPropertyValue = context.InvProductExtendedPropertyValues.Where(u => u.ProductID == productID).ToList();

            for (int i = 0; i < invProductExtendedPropertyValue.Count; i++)
            {
                invProductExtendedPropertyValueSave = invProductExtendedPropertyValue[i];
                invProductExtendedPropertyValueSave.IsDelete = true;
                UpdateProductExtendedPropertyValue(invProductExtendedPropertyValueSave);
            }
        }


        /// Update existing ProductUnitConversionService

        public void UpdateProductExtendedPropertyValue(InvProductExtendedPropertyValue invProductExtendedPropertyValue)
        {
            invProductExtendedPropertyValue.ModifiedUser = Common.LoggedUser;
            invProductExtendedPropertyValue.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(invProductExtendedPropertyValue).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public InvProductExtendedPropertyValue GetProductExtendedPropertyValueByProductID(long productID) 
        {
            return context.InvProductExtendedPropertyValues.Where(c => c.ProductID == productID && c.IsDelete == false).FirstOrDefault();
        }

        public string[] GetExtendedPropertyValuesAccordingToPropertyName(InvProductExtendedProperty invProductExtendedProperty) 
        {
            //List<String> rtnList = new List<string>();

            var qry= (from ep in context.InvProductExtendedProperties.AsNoTracking()
                    join epv in context.InvProductExtendedValues on ep.InvProductExtendedPropertyID equals epv.InvProductExtendedPropertyID
                    where ep.InvProductExtendedPropertyID == invProductExtendedProperty.InvProductExtendedPropertyID
                    select new
                    {
                        epv.ValueData
                    }).ToList();

            List<string> valueList = qry.Select(a => a.ValueData.Trim()).ToList();
            return valueList.ToArray();                                           
        }

        public DataTable GetAllProductExtendedPropertyValuesDataTable()
        {

            var queryResult = (from pepv in context.InvProductExtendedPropertyValues
                               join pep in context.InvProductExtendedProperties on pepv.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               join pev in context.InvProductExtendedValues on pepv.InvProductExtendedValueID equals pev.InvProductExtendedValueID
                               where pepv.IsDelete == false
                               select new
                               {
                                   pepv.InvProductExtendedPropertyID,
                                   pepv.InvProductExtendedValueID,
                                   pep.ExtendedPropertyName,
                                   pev.ValueData
                               }).ToDataTable();

            //List<InvProductExtendedPropertyValue> invProductExtendedPropertyValue = new List<InvProductExtendedPropertyValue>();
            return context.InvProductExtendedPropertyValues.Where(e => e.IsDelete.Equals(false)).ToDataTable();
            //return null;
        }

        public List<InvProductExtendedPropertyValue> GetAllProductExtendedPropertyValues()
        {
            List<InvProductExtendedPropertyValue> rtnList = new List<InvProductExtendedPropertyValue>();

            var queryResult = (from pepv in context.InvProductExtendedPropertyValues
                               join pep in context.InvProductExtendedProperties on pepv.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               join pev in context.InvProductExtendedValues on pepv.InvProductExtendedValueID equals pev.InvProductExtendedValueID
                               where pepv.IsDelete == false
                               select new
                               {
                                   pepv.ProductID, 
                                   pepv.InvProductExtendedPropertyID,
                                   pepv.InvProductExtendedValueID,
                                   pep.ExtendedPropertyName,
                                   pev.ValueData
                               }).ToArray();

            foreach (var temp in queryResult)
            {
                InvProductExtendedPropertyValue tmpInvProductExtendedPropertyValue = new InvProductExtendedPropertyValue();
                tmpInvProductExtendedPropertyValue.ProductID = temp.ProductID;
                tmpInvProductExtendedPropertyValue.ExtendedPropertyName = temp.ExtendedPropertyName;
                tmpInvProductExtendedPropertyValue.ValueData = temp.ValueData;
                tmpInvProductExtendedPropertyValue.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
                tmpInvProductExtendedPropertyValue.InvProductExtendedValueID = temp.InvProductExtendedValueID;

                rtnList.Add(tmpInvProductExtendedPropertyValue);
            }

            return rtnList;
        }

    }
}
