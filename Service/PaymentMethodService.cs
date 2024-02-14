using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MoreLinq;
using Domain;
using Data;
using Utility;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class PaymentMethodService
    {
        ERPDbContext context = new ERPDbContext();

        #region GetNewCode....

        /// <summary>
        /// Generate new Payment Method code
        /// </summary>
        /// <param name="preFix"></param>
        /// <param name="suffix"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.PaymentMethods.Max(p => p.PaymentMethodCode);
            if (GetNewCode != null)
            {
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            else
            {
                GetNewCode = "0";
            }
            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }

        #endregion

        #region Methods.....

        /// <summary>
        /// Save Payment Method details
        /// </summary>
        /// <param name="paymentMethod"></param>
        public void AddPaymentMethod(PaymentMethod paymentMethod)
        {
            context.PaymentMethods.Add(paymentMethod);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all payment methods details by payment method ID
        /// </summary>
        /// <param name="PaymentMethodCode"></param>
        /// <returns></returns>
        public PaymentMethod GetPaymentMethodsByID(int paymentMethodID)
        {
            return context.PaymentMethods.Where(p => p.PaymentMethodID.Equals(paymentMethodID) && p.IsDelete.Equals(false)).FirstOrDefault();
        }

        public PaymentMethod GetPaymentMethodsByTypeID(int paymentTypeID)
        {
            return context.PaymentMethods.Where(p => p.PaymentType.Equals(paymentTypeID) && p.IsDelete.Equals(false)).FirstOrDefault();
        }

        public int GetPaymentTypeByID(int paymentMethodID)
        {
            return context.PaymentMethods.Where(p => p.PaymentMethodID.Equals(paymentMethodID) && p.IsDelete.Equals(false)).FirstOrDefault().PaymentType;
        }

        /// <summary>
        /// Retrive all payment methods details by payment method code
        /// </summary>
        /// <param name="PaymentMethodCode"></param>
        /// <returns></returns>
        public PaymentMethod GetPaymentMethodsByCode(string PaymentMethodCode) 
        {
            return context.PaymentMethods.Where(p => p.PaymentMethodCode.Equals(PaymentMethodCode) && p.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all payment methods details by payment method name
        /// </summary>
        /// <param name="PaymentMethodName"></param>
        /// <returns></returns>
        public PaymentMethod GetPaymentMethodsByName(string PaymentMethodName) 
        {
            return context.PaymentMethods.Where(p => p.PaymentMethodName.Equals(PaymentMethodName) && p.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active payment methods details
        /// </summary>
        /// <returns></returns>
        public List<PaymentMethod> GetAllPaymentMethods() 
        {
            return context.PaymentMethods.Where(p => p.IsDelete.Equals(false)).ToList();
        }

        public List<PaymentMethod> GetPaymentMethodsWithoutCredit()
        {
            return context.PaymentMethods.Where(p => p.IsDelete.Equals(false) && p.PaymentMethodID!=2).ToList();
        }

        /// <summary>
        /// Update payment methods details
        /// </summary>
        /// <param name="existingPaymentMethod"></param>
        public void UpdatePaymentMethod(PaymentMethod existingPaymentMethod) 
        {
            existingPaymentMethod.ModifiedUser = Common.LoggedUser;
            existingPaymentMethod.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingPaymentMethod).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete Payment Method details
        /// </summary>
        /// <param name="existingPaymentMethod"></param>
        public void DeletePaymentMethod(PaymentMethod existingPaymentMethod)
        {
            existingPaymentMethod.IsDelete = true;
            this.context.Entry(existingPaymentMethod).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get All Payment method Codes for auto complete
        /// </summary>
        /// <returns></returns>
        public string[] GetAllPaymentMethodCodes()
        {
            List<string> PaymentMethodCodeList = context.PaymentMethods.Where(p => p.IsDelete.Equals(false)).Select(p => p.PaymentMethodCode).ToList();
            return PaymentMethodCodeList.ToArray(); 
        }

        public string[] GetAllPaymentMethodNames()
        {
            List<string> PaymentMethodNameList = context.PaymentMethods.Where(p => p.IsDelete.Equals(false)).Select(p => p.PaymentMethodName).ToList();
            return PaymentMethodNameList.ToArray();
        }

        public DataTable GetAllActivePaymentMethodsDataTable()
        {
            DataTable tblPaymentMethods = new DataTable();
            var query = from p in context.PaymentMethods 
                        where p.IsDelete == false 
                        select new 
                        { 
                            p.PaymentMethodCode, 
                            p.PaymentMethodName, 
                            CommisionRate = p.CommissionRate ,
                        };
            return tblPaymentMethods = Common.LINQToDataTable(query);

        }

        public DataTable GetAllPaymentMethodDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.PaymentMethods.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            reportConditionsDataStruct.ConditionFrom.Trim(),
                            reportConditionsDataStruct.ConditionTo.Trim());
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from lc in query
                               select new
                               {
                                   FieldString1 = lc.PaymentMethodCode,
                                   FieldString2 = lc.PaymentMethodName,
                                   FieldString3 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString4 = lc.CommissionRate
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetAllPaymentMethodDataTable()
        {
            var query = (from lc in context.PaymentMethods
                         select new
                         {
                             FieldString1 = lc.PaymentMethodCode,
                             FieldString2 = lc.PaymentMethodName,
                             FieldString3 = EntityFunctions.TruncateTime(lc.CreatedDate),
                             FieldString4 = lc.CommissionRate
                         }).ToArray();

            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.PaymentMethods
                                             .Where("IsDelete == @0 ", false);
            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
            //.Select(reportDataStruct.DbColumnName.Trim());


            switch (reportDataStruct.DbColumnName.Trim())
            {
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResult)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }
        #endregion
    }
}
