using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data;

using Domain;
using Data;
using Utility;
using System.Data.Entity;


using EntityFramework.Extensions;
using MoreLinq;


namespace Service
{

    /// <summary>
    /// Developed by - asanka
    /// </summary>
    public class InvProductExtendedValueService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Get active Product Extended Values
        /// </summary>
        /// <returns></returns>
        public List<InvProductExtendedValue> GetAllActiveInvProductExtendedValues()
        {
            return context.InvProductExtendedValues.Where(c => c.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Get active Product Extended Values by Extended Prperty code
        /// </summary>
        /// <returns></returns>
        public List<InvProductExtendedValue> GetAllActiveInvProductExtendedValuesByProductExtendedPrpertyCode(string productExtendedPrpertyCode)
        {
            //return context.InvProductExtendedValues.Join(context.InvProductExtendedProperties, v => v.InvProductExtendedPropertyID, vt => vt.InvProductExtendedPropertyID,
            //                                            (v, vt) => new { v = v, vt = vt })
            //                                        .Where(temp0 => (temp0.vt.ExtendedPropertyCode == productExtendedPrpertyCode))
            //                                        .Select(temp0 => temp0.v).ToList();

            List<InvProductExtendedValue> rtnList = new List<InvProductExtendedValue>();

            var qry = (from a in context.InvProductExtendedValues
                       join e in context.InvProductExtendedProperties on a.InvProductExtendedPropertyID equals e.InvProductExtendedPropertyID
                       where e.ExtendedPropertyCode == productExtendedPrpertyCode && a.IsDelete == false
                       select new
                       {

                           a.InvProductExtendedValueID,
                           a.InvProductExtendedPropertyID,
                           a.ValueData,
                           a.ParentValueData
                       }).AsNoTracking().ToArray();
            
            foreach (var temp in qry)
            {
                InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();

                invProductExtendedValue.InvProductExtendedPropertyID = temp.InvProductExtendedPropertyID;
                invProductExtendedValue.ValueData = temp.ValueData;
                invProductExtendedValue.ParentValueData = temp.ParentValueData;
                invProductExtendedValue.InvProductExtendedValueID = temp.InvProductExtendedValueID;

                rtnList.Add(invProductExtendedValue);
            }
            return rtnList;
        }

        public void UpdateExtendedValues(List<InvProductExtendedValue> invProductExtendedValues) 
        {
            //List<InvProductExtendedValue> invProductExtendedValueList = new List<InvProductExtendedValue>();
            //InvProductExtendedValue invProductExtendedValue = new InvProductExtendedValue();
            //invProductExtendedValueList = context.InvProductExtendedValues.Where(u => u.InvProductExtendedPropertyID == extendedPropertyID).ToList();

            //for (int i = 0; i < invProductExtendedValueList.Count; i++)
            //{
            //    invProductExtendedValue = invProductExtendedValueList[i];
            //    invProductExtendedValue.IsDelete = true;
              // UpdateInvProductExtendedValue(invProductExtendedValue);
            //}

            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            var invProductExtendedValueSaveQuery =
                           (from pd in invProductExtendedValues
                            select new InvProductExtendedValue
                            {
                                InvProductExtendedPropertyID = pd.InvProductExtendedPropertyID,
                                InvProductExtendedValueID = pd.InvProductExtendedValueID,
                                //InvProductExtendedPropertyID = 0,
                                ValueData = pd.ValueData,
                                ParentValueData = pd.ParentValueData,
                                IsDelete = pd.IsDelete,
                                ModifiedDate = pd.ModifiedDate,
                                ModifiedUser = pd.ModifiedUser,
                                GroupOfCompanyID = pd.GroupOfCompanyID,
                                DataTransfer = pd.DataTransfer,
                                CreatedUser = pd.CreatedUser,
                                CreatedDate = pd.CreatedDate

                            }).ToList();
            DataTable DTITEM = new DataTable();
            DTITEM = invProductExtendedValueSaveQuery.ToDataTable();

            string mergeSql = CommonService.MergeSqlToForTrans(DTITEM, "InvProductExtendedValue");
            CommonService.BulkInsert(DTITEM, "InvProductExtendedValue", mergeSql);
        }

        /// <summary>
        /// Get active Product Extended Values by Extended Prperty Id
        /// </summary>
        /// <returns></returns>
        public List<InvProductExtendedValue> GetAllActiveInvProductExtendedValuesByProductExtendedPropertyID(long productExtendedPrpertyID)
        {
            //By Nuwan
            List<InvProductExtendedValue> rtnList = new List<InvProductExtendedValue>();
            
            var queryResult = (from pev in context.InvProductExtendedValues.AsNoTracking()
                               join pep in context.InvProductExtendedProperties on pev.InvProductExtendedPropertyID equals pep.InvProductExtendedPropertyID
                               where pev.InvProductExtendedPropertyID.Equals(productExtendedPrpertyID)
                               select new
                               {
                                   pev.ValueData
                               }).ToArray();

            foreach (var temp in queryResult) 
            {
                InvProductExtendedValue tmpInvProductExtendedValue = new InvProductExtendedValue();

                tmpInvProductExtendedValue.ValueData = temp.ValueData;

                rtnList.Add(tmpInvProductExtendedValue);
            }

            return rtnList;

            //return context.InvProductExtendedValues.Join(context.InvProductExtendedProperties, v => v.InvProductExtendedPropertyID, vt => vt.InvProductExtendedPropertyID,
            //                                            (v, vt) => new { v = v, vt = vt })
            //                                        .Where(temp0 => (temp0.vt.InvProductExtendedPropertyID == productExtendedPrpertyID))
            //                                        .Select(temp0 => temp0.v).ToList();
        }

        /// <summary>
        /// Get Product Extended Value by Value Data
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvProductExtendedValue GetInvProductExtendedValueByValueData(string invProductExtendedValueData)
        {
            return context.InvProductExtendedValues.Where(c => c.ValueData == invProductExtendedValueData).FirstOrDefault();
        }

        /// <summary>
        /// Get Product Extended Value by Value Data and extended property ID
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public InvProductExtendedValue GetInvProductExtendedValueByValueDataByExtendedPropertyID(string invProductExtendedValueData, long ExtendedPropertyID)
        {
            return context.InvProductExtendedValues.Where(c => c.ValueData == invProductExtendedValueData && c.InvProductExtendedPropertyID == ExtendedPropertyID).FirstOrDefault();
        }

        /// <summary>
        /// Save new Product Extended Value
        /// </summary>
        /// <param name="invProductExtendedValue"></param>
        public void AddInvProductExtendedValues(List<InvProductExtendedValue> invProductExtendedValues)
        {            
            using (TransactionScope transactionScope = new TransactionScope())
            {
                foreach (InvProductExtendedValue invProductExtendedValue in invProductExtendedValues)
                {
                    if (invProductExtendedValue.InvProductExtendedValueID.Equals(0))
                    {
                        context.InvProductExtendedValues.Add(invProductExtendedValue);
                    }
                    else
                    {
                        this.context.Entry(invProductExtendedValue).State = EntityState.Modified;
                    }
                }
                context.SaveChanges();
                transactionScope.Complete();
            }
        }

        public void saveQuery(List<InvProductExtendedValue> invProductExtendedValues)
        {
            
        }

        public void SaveValues(List<InvProductExtendedValue> invProductExtendedValues)
        {
            foreach (InvProductExtendedValue invProductExtendedValue in invProductExtendedValues)
            {
                context.InvProductExtendedValues.Add(invProductExtendedValue);
            }
        }

        /// <summary>
        /// Update existing Product Extended Value
        /// </summary>
        /// <param name="existingInvProductExtendedValue"></param>
        public void UpdateInvProductExtendedValue(InvProductExtendedValue existingInvProductExtendedValue)
        {
            this.context.Entry(existingInvProductExtendedValue).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Add or Update values of grid view
        /// </summary>
        /// <param name="invProductExtendedValue"></param>
        /// <returns></returns>
        public List<InvProductExtendedValue> GetInvProductExtendedValuesList(List<InvProductExtendedValue> invProductExtendedValues, InvProductExtendedValue invProductExtendedValue)
        {
            if (invProductExtendedValues.Where(v => v.ValueData == invProductExtendedValue.ValueData).FirstOrDefault() != null)
            {
                // Update Existing Record
                invProductExtendedValues.RemoveAll(v => v.ValueData == invProductExtendedValue.ValueData);
                invProductExtendedValues.Add(invProductExtendedValue);
            }
            else
            {
                // Insert New Record
                invProductExtendedValues.Add(invProductExtendedValue);
            }
            return invProductExtendedValues;
        }


        /// <summary>
        /// Remove values of grid view
        /// </summary>
        /// <param name="invProductExtendedValue"></param>
        /// <returns></returns>
        public List<InvProductExtendedValue> GetDeleteInvProductExtendedValuesList(List<InvProductExtendedValue> invProductExtendedValues, InvProductExtendedValue invProductExtendedValue)
        { 
            if (invProductExtendedValues.Where(v => v.ValueData == invProductExtendedValue.ValueData).FirstOrDefault() != null)
            {
                invProductExtendedValues.RemoveAll(v => v.ValueData == invProductExtendedValue.ValueData);
            }
            else
            {
            }
            return invProductExtendedValues;
        }

        /// <summary>
        /// Validate input data type according to selected extended property
        /// </summary>
        /// <returns></returns>
        public bool IsValidPropertyValueDataType(InvProductExtendedProperty invProductExtendedProperty, string valueData)
        {
            bool validated = false;
            switch (invProductExtendedProperty.DataType)
            {
                case "DATE":                    
                    DateTime valueDateTime;
                    validated = DateTime.TryParse(valueData, out valueDateTime);
                    break;
                case "DECIMAL":
                    Decimal valueDecimal;
                    validated = Decimal.TryParse(valueData, out valueDecimal);
                    break;
                case "INTEGER":
                    Int32 valueInt32;
                    validated = Int32.TryParse(valueData, out valueInt32);
                    break;
                case "STRING":
                    if (string.IsNullOrEmpty(valueData.Trim()))
                    { validated = false; }
                    else
                    { validated = true; }
                    break;
                default:
                    break;
            }
            return validated;
        }

        #endregion
    }
}
