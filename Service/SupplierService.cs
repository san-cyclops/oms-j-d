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
using EntityFramework.Extensions;
using System.Data.Common;

namespace Service
{
    public class SupplierService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save new Supplier
        /// </summary>
        /// <param name="supplier"></param>
        public void AddSupplier(Supplier supplier)
        {
            context.Suppliers.Add(supplier);

            List<ValidationResult> errors = new List<ValidationResult>();
            ValidationContext contextVal = new ValidationContext(supplier, null, null);
 
            if (!Validator.TryValidateObject(supplier, contextVal, errors, true))
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
        /// Update existing Supplier
        /// </summary>
        /// <param name="supplier"></param>
        public void UpdateSupplier(Supplier supplier)
        {
            supplier.ModifiedUser = Common.LoggedUser;
            supplier.ModifiedDate = Common.GetSystemDateWithTime();
            supplier.DataTransfer = 0;
            context.Entry(supplier).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void UpdateSupplier(int datatransfer)
        {
            context.Suppliers.Update(ps => ps.DataTransfer.Equals(1) , ps => new Supplier { DataTransfer = datatransfer });
        }

        public void UpdateSupplierDTSelect(int datatransfer)
        {
            context.Suppliers.Update(ps => ps.DataTransfer.Equals(0), ps => new Supplier { DataTransfer = datatransfer });
        }

        /// <summary>
        /// Search Supplier by code
        /// </summary>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public Supplier GetSupplierByCode(string supplierCode)
        {
            return context.Suppliers.Where(s => s.SupplierCode == supplierCode && s.IsDelete == false).FirstOrDefault();
        }

        public bool IsExistsSupplier(string supplierCode)
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

        /// <summary>
        /// Search Supplier by name
        /// </summary>
        /// <param name="supplierName"></param>
        /// <returns></returns>
        public Supplier GetSupplierByName(string supplierName)
        {
            return context.Suppliers.Where(s => s.SupplierName == supplierName && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Search Supplier by ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier GetSupplierByID(long supplierID)
        {
            return context.Suppliers.Where(s => s.SupplierID == supplierID && s.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get Supplier Codes
        /// </summary>
        /// <returns></returns>
        public string[] GetSupplierCodes()
        {
            List<string> suppliersList = context.Suppliers.Where(s => s.IsDelete.Equals(false)).Select(s => s.SupplierCode).ToList();
            string[] supplierCodes = suppliersList.ToArray();
            return supplierCodes;
        }

        /// <summary>
        /// Get Supplier Names
        /// </summary>
        /// <returns></returns>
        public string[] GetSupplierNames()
        {
            List<string> suppliersList = context.Suppliers.Where(s => s.IsDelete.Equals(false)).OrderBy(s=> s.SupplierName).Select(s => s.SupplierName).ToList();
            string[] supplierNames = suppliersList.ToArray();
            return supplierNames;
        }

        /// <summary>
        /// Get all Suppliers details 
        /// </summary>
        /// <returns>Supplier</returns>
        public List<Supplier> GetAllSuppliers()
        {
            return context.Suppliers.Where(u => u.IsDelete == false).ToList();
        }

        public DataTable GetAllSuppliersList()
        {
            DataTable tblSuppliers = new DataTable();
            var sq = (from s in context.Suppliers
                      select
                          new
                              {
                                  FieldDecimal1 = 0,
                                  FieldDecimal2 = 0,
                                  FieldDecimal3 = 0,
                                  FieldDecimal4 = 0,
                                  FieldDecimal5 = 0,
                                  FieldDecimal6 = 0,
                                  FieldDecimal7 = 0,
                                  FieldString1 = s.SupplierCode,
                                  FieldString2 = s.SupplierName,
                                  FieldString3 = s.SuppliedProducts,
                                  FieldString4 = "",
                                  FieldString5 = "",
                                  FieldString6 = "",
                                  FieldString7 = ""

                              }).ToList();

            return tblSuppliers = Common.LINQToDataTable(sq);

        }

        public DataTable GetAllSupplierDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Suppliers.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));}

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
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
                            case "SupplierGroupID":
                                query = (from qr in query
                                         join jt in context.SupplierGroups.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SupplierGroupID equals jt.SupplierGroupID
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

            string titleType = ((int) LookUpReference.TitleType).ToString();
            var queryResult = (from lc in query
                               //join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID
                               join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID into ld_join
                               from ld in ld_join.DefaultIfEmpty()
                               join rt in context.ReferenceTypes on lc.SupplierTitle equals rt.LookupKey
                               join sg in context.SupplierGroups on lc.SupplierGroupID equals sg.SupplierGroupID
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
                                       FieldString9 = lc.DeliveryAddress1 + " " + lc.DeliveryAddress2 + " " + lc.DeliveryAddress3,
                                       FieldString10 = lc.ReferenceNo,
                                       FieldString11 = sg.SupplierGroupName,
                                       FieldString12 = lc.Remark,
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

        public DataTable GetAllSupplierDataTable()
        {
            //DataTable tblSupplier = new DataTable();

            string titleType = ((int)LookUpReference.TitleType).ToString();

            var query = (from lc in context.Suppliers
                      join ld in context.PaymentMethods on lc.PaymentMethod equals ld.PaymentMethodID
                      join rt in context.ReferenceTypes on lc.SupplierTitle equals rt.LookupKey
                      where rt.LookupType.Equals(titleType)
                      select new
                      {
                          FieldString1 = lc.SupplierCode,
                          FieldString2 = (rt.LookupValue + " " + lc.SupplierName),
                          FieldString3 = (lc.BillingAddress1 + ", " + lc.BillingAddress2 + ", " + lc.BillingAddress3 + ". " + lc.PostalCode ),
                          FieldString4 = lc.BillingTelephone,
                          FieldString5 = lc.ReferenceNo,
                          FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                          FieldString7 = ld.PaymentMethodName,
                          FieldString8 = lc.ContactPersonName,
                          FieldString9 = lc.DeliveryAddress1 + " " + lc.DeliveryAddress2 + " " + lc.DeliveryAddress3,
                          FieldString10 = lc.Remark
                      });

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
            IQueryable qryResult = context.Suppliers
                            .Where("IsDelete == @0 ", false);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LookupKey":
                    qryResult = qryResult.Join(context.ReferenceTypes.Where("LookupType == @0 AND IsDelete == @1", "2", false), "SupplierTitle", reportDataStruct.DbColumnName.Trim(), "new(outer.SupplierName, inner.LookupValue)").
                                OrderBy("LookupValue").Select("new( LookupValue, SupplierName )")
                                ; //.Concat(qryResult.GetType());//.Where(""); //GroupBy("SupplierCode", "SupplierCode"); //.Select("SupplierCode");
                    break; 
                case "PaymentMethodID":
                    qryResult = qryResult.Join(context.PaymentMethods.Where("IsDelete == @0", false), "PaymentMethod", reportDataStruct.DbColumnName.Trim(), "new(inner.PaymentMethodName)")
                                .OrderBy("PaymentMethodName")
                                .GroupBy("new(PaymentMethodName)", "new(PaymentMethodName)")
                                .Select("Key.PaymentMethodName");
                    break;
                case "SupplierGroupID":
                    qryResult = qryResult.Join(context.SupplierGroups.Where("IsDelete == @0", false), reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierGroupName)")
                                .GroupBy("new(SupplierGroupName)", "new(SupplierGroupName)")
                                .OrderBy("Key.SupplierGroupName")
                                .Select("Key.SupplierGroupName");
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
        /// Get active Supplier details DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllActiveSuppliersDataTable()
        {
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.Suppliers 
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

        public DataTable GetAllSuppliersDataTable()
        {
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.Suppliers
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

        public DataTable GetAllDTSuppliersDataTable()
        {
            DataTable tblSuppliers = new DataTable();
            var query = from d in context.Suppliers
                        where d.DataTransfer == 1
                        select new
                            {
                                d.SupplierID,
                                d.SupplierCode,
                                d.SupplierTitle,
                                d.SupplierName,
                                d.SupplierType,
                                d.ContactPersonName,
                                d.Gender,
                                d.BillingAddress1,
                                d.BillingAddress2,
                                d.BillingAddress3,
                                d.BillingTelephone,
                                d.BillingMobile,
                                d.BillingFax,
                                d.Email,
                                d.RepresentativeName,
                                d.RepresentativeNICNo,
                                d.DeliveryAddress1,
                                d.DeliveryAddress2,
                                d.DeliveryAddress3,
                                d.DeliveryTelephone,
                                d.DeliveryMobile,
                                d.DeliveryFax,
                                d.SupplierImage,
                                d.ReferenceNo,
                                d.ReferenceSerial,
                                d.PostalCode,
                                d.TaxID1,
                                d.TaxNo1,
                                d.TaxID2,
                                d.TaxNo2,
                                d.TaxID3,
                                d.TaxNo3,
                                d.TaxID4,
                                d.TaxNo4,
                                d.TaxID5,
                                d.TaxNo5,
                                d.TaxRegistrationNo,
                                d.TaxRegistrationName,
                                d.PaymentMethod,
                                d.CreditLimit,
                                d.CreditPeriod,
                                d.ChequeLimit,
                                d.ChequePeriod,
                                d.Remark,
                                d.ProductBusinessType,
                                d.SuppliedProducts,
                                d.OrderCircle,
                                d.SupplierGroupID,
                                d.LedgerID,
                                d.OtherLedgerID,
                                d.TaxIdNo,
                                d.IsUpload,
                                d.IsBlocked,
                                d.IsSuspended,
                                d.IsDelete,
                                d.GroupOfCompanyID,
                                d.CreatedUser,
                                d.CreatedDate,
                                d.ModifiedUser,
                                d.ModifiedDate,
                                DataTransfer = d.DataTransfer
                            };

            return tblSuppliers = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.Suppliers.SqlQuery("select SupplierID ,SupplierCode ,SupplierTitle ,SupplierName ,SupplierType ,ContactPersonName ,Gender ,BillingAddress1 ,BillingAddress2 ,BillingAddress3 ,BillingTelephone ,BillingMobile ,BillingFax ,Email ,RepresentativeName ,RepresentativeNICNo ,DeliveryAddress1 ,DeliveryAddress2 ,DeliveryAddress3 ,DeliveryTelephone ,DeliveryMobile ,DeliveryFax ,SupplierImage ,ReferenceNo ,ReferenceSerial ,PostalCode ,TaxID1 ,TaxNo1 ,TaxID2 ,TaxNo2 ,TaxID3 ,TaxNo3 ,TaxID4 ,TaxNo4 ,TaxID5 ,TaxNo5 ,TaxRegistrationNo ,TaxRegistrationName ,PaymentMethod ,CreditLimit ,CreditPeriod ,ChequeLimit ,ChequePeriod ,Remark ,ProductBusinessType ,SuppliedProducts ,OrderCircle ,SupplierGroupID ,AccLedgerAccountID ,OtherLedgerID ,TaxIdNo ,IsUpload ,IsBlocked ,IsSuspended ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from Supplier").ToDataTable();
        }

        public DataTable GetAllDTSupplierPropertyDataTable()
        {
            DataTable tblSupplierProperties = new DataTable();
            var query = from d in context.SupplierProperties
                        where d.DataTransfer == 1
                        select new
                            {
                                d.SupplierID, d.SupplierCode, d.PaymentCollector1Name, d.PaymentCollector1Designation, d.PaymentCollector1ReferenceNo, d.PaymentCollector1TelephoneNo,d. PaymentCollector1Image, 
                      d.PaymentCollector2Name, d.PaymentCollector2Designation, d.PaymentCollector2ReferenceNo, d.PaymentCollector2TelephoneNo, d.PaymentCollector2Image, d.IntroducedPerson1Name, 
                      d.IntroducedPerson1Address1, d.IntroducedPerson1Address2, d.IntroducedPerson1Address3, d.IntroducedPerson1TelephoneNo, d.IntroducedPerson2Name, d.IntroducedPerson2Address1, 
                      d.IntroducedPerson2Address2, d.IntroducedPerson2Address3, d.IntroducedPerson2TelephoneNo, d.DealingPeriod, d.YearOfBusinessCommencement, d.NoOfEmployees, d.NoOfMachines, d.PercentageOfWHT, 
                      d.IncomeTaxFileNo, d.RemarkByPurchasingDepartment, d.RemarkByAccountDepartment, d.IsVatRegistrationCopy, d.VatRegistrationCopy, d.IsPassportPhotos, d.PassportPhotos, d.IsCDDCertification, 
                      d.CDDCertificate, d.IsBrandCertification, d.BrandCertificate, d.IsRefeneceDocumentCopy, d.RefeneceDocumentCopy, d.IsLicenceToImport, d.LicenceToImport, d.IsLabCertification, d.LabCertificate, d.IsBRCCopy, 
                      d.BRCCopy, d.IsDistributorOwnerCopy, d.DistributorOwnerCopy, d.IsQCCertification, d.QCCertificate, d.IsSLSISOCertiification, d.SLSISOCertiificate,
                                d.IsDelete,
                                d.GroupOfCompanyID,
                                d.CreatedUser,
                                d.CreatedDate,
                                d.ModifiedUser,
                                d.ModifiedDate,
                                DataTransfer = d.DataTransfer
                            };

            return tblSupplierProperties = query.ToDataTable();
           // context.Configuration.LazyLoadingEnabled = false;
           // return context.Suppliers.SqlQuery("select SupplierID ,SupplierCode ,SupplierTitle ,SupplierName ,SupplierType ,ContactPersonName ,Gender ,BillingAddress1 ,BillingAddress2 ,BillingAddress3 ,BillingTelephone ,BillingMobile ,BillingFax ,Email ,RepresentativeName ,RepresentativeNICNo ,DeliveryAddress1 ,DeliveryAddress2 ,DeliveryAddress3 ,DeliveryTelephone ,DeliveryMobile ,DeliveryFax ,SupplierImage ,ReferenceNo ,ReferenceSerial ,PostalCode ,TaxID1 ,TaxNo1 ,TaxID2 ,TaxNo2 ,TaxID3 ,TaxNo3 ,TaxID4 ,TaxNo4 ,TaxID5 ,TaxNo5 ,TaxRegistrationNo ,TaxRegistrationName ,PaymentMethod ,CreditLimit ,CreditPeriod ,ChequeLimit ,ChequePeriod ,Remark ,ProductBusinessType ,SuppliedProducts ,OrderCircle ,SupplierGroupID ,AccLedgerAccountID ,OtherLedgerID ,TaxIdNo ,IsUpload ,IsBlocked ,IsSuspended ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from Supplier").ToDataTable();
        }

        public Supplier GetTaxBySupplierCode(string supplierCode)
        {
            return context.Suppliers.Where(s => s.SupplierCode == supplierCode && s.IsDelete == false).FirstOrDefault();
        }

        #region GetNewCode
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.Suppliers.Max(a => a.SupplierCode);
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


        public string[] GetAllSupplierCodes()
        {
            List<string> SupplierCodeList = context.Suppliers.Where(c => c.IsDelete.Equals(false)).Select(c => c.SupplierCode).ToList();
            return SupplierCodeList.ToArray();
        }

        public string[] GetAllSupplierNames()
        {
            List<string> SupplierNameList = context.Suppliers.Where(c => c.IsDelete.Equals(false)).Select(c => c.SupplierName).ToList();
            return SupplierNameList.ToArray();
        }

        /// <summary>
        /// Get All Supplier Codes by supplier Type
        /// </summary>
        /// <returns></returns>
        public string[] GetAllSupplierCodesBySupplierType(int supplierType)
        {
            List<string> suppliersList = context.Suppliers.Where(s => s.SupplierType == supplierType && s.IsDelete.Equals(false)).Select(s => s.SupplierCode).ToList();
            string[] supplierCodes = suppliersList.ToArray();
            return supplierCodes;
        }

        public long[] GetSupplierIdRangeBySupplierNames(string nameFrom, string nameTo)
        {
            return context.Suppliers.Where(d => d.IsDelete.Equals(false) && d.SupplierName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && d.SupplierName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(d => d.SupplierID).ToArray();
        }

        /// <summary>
        /// Get all Supplier Names by supplier Type
        /// </summary>
        /// <returns></returns>
        public string[] GetAllSupplierNamesBySupplierType(int supplierType)
        {
            List<string> suppliersList = context.Suppliers.Where(s => s.SupplierType == supplierType && s.IsDelete.Equals(false)).Select(s => s.SupplierName).ToList();
            string[]supplierNames = suppliersList.ToArray();
            return supplierNames;
        }

        public DataTable GetSupplierDetails(DateTime DateFrom ,DateTime DateTo,string compnay)
        {
            using (ERPDbContext context = new ERPDbContext())
            {

                var query = (
                        from ph in context.SalesOrderHeaders
                        join pd in context.SalesOrderDetails on ph.SalesOrderHeaderID equals pd.SalesOrderHeaderID
                        join c in context.Customers on ph.CustomerID equals c.CustomerID
                        join sp in context.InvSalesPersons on ph.InvSalesPersonID equals sp.InvSalesPersonID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join dep in context.InvDepartments on ph.DepartmentID equals dep.InvDepartmentID
                        join ct in context.InvCategories on ph.CategoryID equals ct.InvCategoryID
                        join sct in context.InvSubCategories on ph.SubCategoryID equals sct.InvSubCategoryID
                        join sct2 in context.InvSubCategories2 on ph.SubCategory2ID equals sct2.InvSubCategory2ID
                        join dh in context.InvSalesHeaders on ph.SalesOrderHeaderID equals dh.ReferenceDocumentID
                        join dd in context.InvSalesDetails on dh.InvSalesHeaderID equals dd.InvSalesHeaderID
                        where pd.SalesOrderDetailID.Equals(dd.ProductID) && ph.DocumentDate >= DateFrom && ph.DocumentDate<= DateTo && ph.Company.Equals(compnay)
                        select
                            new
                            {
                                ph.DocumentNo,
                                ph.ReferenceNo,
                                c.CustomerName,
                                sp.SalesPersonName,
                                ph.Remark,
                                ph.Unit,
                                ph.Company,
                                ph.DocumentDate,
                                ph.DeliverDate,
                                ph.Colour,
                                pd.ProductCode,
                                pd.ProductName,
                                pd.Size,
                                pd.Gauge,
                                dep.DepartmentName,
                                ct.CategoryName,
                                sct.SubCategoryName,
                                sct2.SubCategory2Name,
                                dd.OrderQty,
                                dd.Qty,
                                InvNo= dh.DocumentNo


                            });
                return query.AsEnumerable().ToDataTable();

            }

        }

        public bool View(string LocaCodeFrom, string LocaCodeTo, string SupCodeFrom, string SupCodeTo, string ProCodeFrom, string ProCodeTo, DateTime FromDate, DateTime ToDate)
        {

            {

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocaCodeFrom", Value=LocaCodeFrom}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocaCodeTo", Value=LocaCodeTo}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SupCodeFrom", Value=SupCodeFrom}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SupCodeTo", Value=SupCodeTo}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ProCodeFrom", Value=ProCodeFrom}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ProCodeTo", Value=ProCodeTo}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@FromDate", Value=FromDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ToDate", Value=ToDate},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UserId",Value=Common.LoggedUserId}, 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CreatedUser", Value=Common.LoggedUser},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyId", Value=Common.LoggedCompanyID}
                        
                    };


                if (CommonService.ExecuteStoredProcedure("spRptSupplierWisePerformanceAnalysis", parameter))
                {

                    return true;
                }
                else
                { return false; }

            }
        }

        

        #endregion
    }
}
