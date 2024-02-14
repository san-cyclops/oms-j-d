using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using MoreLinq;
using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class LgsSupplierService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new Logistic Supplier
        /// </summary>
        /// <param name="lgsSupplier"></param>
        public void AddSupplier(LgsSupplier lgsSupplier)
        {
            context.LgsSuppliers.Add(lgsSupplier);

            List<ValidationResult> errors = new List<ValidationResult>();
            ValidationContext contextVal = new ValidationContext(lgsSupplier, null, null);

            if (!Validator.TryValidateObject(lgsSupplier, contextVal, errors, true))
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
        /// Search Supplier by ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public LgsSupplier GetSupplierByID(long supplierID)
        {
            return context.LgsSuppliers.Where(s => s.LgsSupplierID == supplierID && s.IsDelete == false).FirstOrDefault();
        }


        public bool IsExistsSupplier(string supplierCode)
        {

            if (GetLgsSupplierByCode(supplierCode) != null)
            {
                return true;
            }
            else
            {
                return false;

            }

        }




        /// <summary>
        /// Update existing Logistic Supplier
        /// </summary>
        /// <param name="lgsSupplier"></param>
        public void UpdateSupplier(LgsSupplier lgsSupplier)
        {
            lgsSupplier.ModifiedUser = Common.LoggedUser;
            lgsSupplier.ModifiedDate = Common.GetSystemDateWithTime();
            context.Entry(lgsSupplier).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Search Logistic Supplier by code
        /// </summary>
        /// <param name="lgsSupplierCode"></param>
        /// <returns></returns>
        public LgsSupplier GetLgsSupplierByCode(string lgsSupplierCode)
        {
            return context.LgsSuppliers.Where(s => s.SupplierCode == lgsSupplierCode && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Logistic Supplier by name
        /// </summary>
        /// <param name="lgsSupplierName"></param>
        /// <returns></returns>
        public LgsSupplier GetLgsSupplierByName(string lgsSupplierName)
        {
            return context.LgsSuppliers.Where(s => s.SupplierName == lgsSupplierName && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Logistic Supplier by ID
        /// </summary>
        /// <param name="lgsSsupplierID"></param>
        /// <returns></returns>
        public LgsSupplier GetLgsSupplierByID(long lgsSsupplierID)
        {
            return context.LgsSuppliers.Where(s => s.LgsSupplierID == lgsSsupplierID && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get All logistic Supplier Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSupplierCodes()
        {
            List<string> lgsSuppliersList = context.LgsSuppliers.Where(s => s.IsDelete.Equals(false)).Select(s => s.SupplierCode).ToList();
            string[] lgsSupplierCodes = lgsSuppliersList.ToArray();
            return lgsSupplierCodes;
        }

        /// <summary>
        /// Get all logistic Supplier Names
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSupplierNames()
        {
            List<string> lgsSuppliersList = context.LgsSuppliers.Where(s => s.IsDelete.Equals(false)).Select(s => s.SupplierName).ToList();
            string[] lgsSupplierNames = lgsSuppliersList.ToArray();
            return lgsSupplierNames;
        }

        /// <summary>
        /// Get All logistic Supplier Codes by supplier Type
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSupplierCodesBySupplierType(int supplierType)
        {
            List<string> lgsSuppliersList = context.LgsSuppliers.Where(s => s.SupplierType == supplierType && s.IsDelete.Equals(false)).Select(s => s.SupplierCode).ToList();
            string[] lgsSupplierCodes = lgsSuppliersList.ToArray();
            return lgsSupplierCodes;
        }

        /// <summary>
        /// Get all logistic Supplier Names by supplier Type
        /// </summary>
        /// <returns></returns>
        public string[] GetAllLgsSupplierNamesBySupplierType(int supplierType)
        {
            List<string> lgsSuppliersList = context.LgsSuppliers.Where(s => s.SupplierType == supplierType && s.IsDelete.Equals(false)).Select(s => s.SupplierName).ToList();
            string[] lgsSupplierNames = lgsSuppliersList.ToArray();
            return lgsSupplierNames;
        }

        /// <summary>
        /// Get all logistic Suppliers details 
        /// </summary>
        /// <returns>Supplier</returns>
        public List<LgsSupplier> GetAllSuppliers()
        {
            return context.LgsSuppliers.Where(s => s.IsDelete == false).ToList();
        }

        /// <summary>
        /// Get active logistic Supplier details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveLgsSuppliersDataTable()
        {
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.LgsSuppliers
                        where d.IsDelete == false
                        select new
                        {
                            d.SupplierCode,
                            d.SupplierName,
                            d.BillingTelephone,
                            d.BillingMobile,
                            d.ContactPersonName,
                            d.CreditLimit,
                            d.CreditPeriod,
                            d.ChequeLimit,
                            d.ChequePeriod
                        };
            return tblSuppliers = Common.LINQToDataTable(query);
        }

        /// <summary>
        /// Get active logistic Supplier details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveLgsSuppliersDataTableBySupplierType(int supplierType)
        {
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.LgsSuppliers where d.SupplierType == supplierType && d.IsDelete == false select new { d.SupplierCode, d.SupplierName, d.SupplierGroup.SupplierGroupName, d.RepresentativeName };
            return tblSuppliers = Common.LINQToDataTable(query);
        }


        public LgsSupplier GetTaxByLgsSupplierCode(string supplierCode)
        {
            return context.LgsSuppliers.Where(s => s.SupplierCode == supplierCode && s.IsDelete == false).FirstOrDefault();
        }

        public DataTable GetAllLgsSupplierDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.LgsSuppliers.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "PaymentMethodID":
                                query = (from qr in query
                                         join jt in context.PaymentMethods.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.PaymentMethod equals jt.PaymentMethodID
                                         select qr
                                        );

                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));

                                break;
                        }
                    }
                    else
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            { querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            string titleType = ((int)LookUpReference.TitleType).ToString();
            var queryResult = (from lc in query
                               //join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID
                               join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID into ld_join
                               from ld in ld_join.DefaultIfEmpty()
                               join rt in context.ReferenceTypes on lc.SupplierTitle equals rt.LookupKey
                               where rt.LookupType.Equals(titleType)
                               select new
                               {
                                   FieldString1 = lc.SupplierCode,
                                   FieldString2 = (rt.LookupValue + " " + lc.SupplierName),
                                   FieldString3 = (lc.BillingAddress1 + " " + lc.BillingAddress2 + " " + lc.BillingAddress3 + " " + lc.PostalCode),
                                   FieldString4 = lc.BillingTelephone,
                                   FieldString5 = lc.ReferenceNo,
                                   FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString7 = ld.PaymentMethodName,
                                   FieldString8 = lc.ContactPersonName,
                                   FieldString9 = lc.Remark //,
                                   //FieldString10 = lc.DeliveryAddress1 + " " + lc.DeliveryAddress2 + " " + lc.DeliveryAddress3
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

        public DataTable GetAllLgsSupplierDataTable()
        {
            //DataTable tblSupplier = new DataTable();

            string titleType = ((int)LookUpReference.TitleType).ToString();

            var query = (from lc in context.LgsSuppliers
                      join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID
                      join rt in context.ReferenceTypes on lc.SupplierTitle equals rt.LookupKey
                      where rt.LookupType.Equals(titleType)
                      select new
                      {
                          FieldString1 = lc.SupplierCode,
                          FieldString2 = (rt.LookupValue + " " + lc.SupplierName),
                          FieldString3 = (lc.BillingAddress1 + ", " + lc.BillingAddress2 + ", " + lc.BillingAddress3 + ". " + lc.PostalCode),
                          FieldString4 = lc.BillingTelephone,
                          FieldString5 = lc.ReferenceNo,
                          FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                          FieldString7 = ld.PaymentMethodName,
                          FieldString8 = lc.ContactPersonName,
                          FieldString9 = lc.Remark,
                          FieldString10 = lc.DeliveryAddress1 + " " + lc.DeliveryAddress2 + " " + lc.DeliveryAddress3
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToShortDateString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblSupplier = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        /// <summary>
        /// Get selection data for Report Generator Combo boxes
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.LgsSuppliers
                            .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());


            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LookupKey":
                    qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0", "2"), "SupplierTitle", reportDataStruct.DbColumnName.Trim(), "new(outer.SupplierName, inner.LookupValue)").
                                OrderBy("LookupValue").Select("LookupValue, SupplierName")//.Select()
                                ; //.Concat(qryResult.GetType());//.Where(""); //GroupBy("SupplierCode", "SupplierCode"); //.Select("SupplierCode");
                    break;
                case "PaymentMethodID":
                    qryResult = qryResult.Join(context.PaymentMethods, "PaymentMethod", reportDataStruct.DbColumnName.Trim(), "new(inner.PaymentMethodName)")
                                .GroupBy("new(PaymentMethodName)", "new(PaymentMethodName)")
                                .OrderBy("Key.PaymentMethodName")
                                .Select("Key.PaymentMethodName");
                    break;
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

        /// <summary>
        /// Search Supplier by code
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public LgsSupplier GetSupplierByCode(string supplierCode)
        {
            return context.LgsSuppliers.Where(s => s.SupplierCode == supplierCode && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Supplier by name
        /// </summary>
        /// <param name="supplierName"></param>
        /// <returns></returns>
        public LgsSupplier GetSupplierByName(string supplierName)
        {
            return context.LgsSuppliers.Where(s => s.SupplierName == supplierName && s.IsDelete == false).FirstOrDefault();
        }

        public bool IsExistsLgsSupplier(string supplierCode)
        {

            if (GetSupplierByCode(supplierCode) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #region GetNewCode
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.LgsSuppliers.Max(a => a.SupplierCode);
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

        #endregion
    }
}
