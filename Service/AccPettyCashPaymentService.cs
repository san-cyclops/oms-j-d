using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using MoreLinq;
using Data;
using Domain;
using System.Data.Entity;
using Utility;

namespace Service
{
    public class AccPettyCashPaymentService
    {
        /// <summary>
        /// Save PettyCashPayment
        /// </summary>
        /// <param name="accPettyCashPaymentHeader"></param>
        /// <param name="accPettyCashPaymentDetails"></param>
        public bool SavePettyCashPayment(AccPettyCashPaymentHeader accPettyCashPaymentHeader, List<AccPettyCashPaymentDetail> accPettyCashPaymentDetails, out string newDocumentNo, string formName = "", bool isTStatus = false)
        {
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {                    
                        if (accPettyCashPaymentHeader.DocumentStatus.Equals(1))
                        {
                            AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                            LocationService locationService = new LocationService();
                            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                            accPettyCashPaymentHeader.DocumentNo = accPettyCashPaymentService.GetDocumentNo(formName, accPettyCashPaymentHeader.LocationID, locationService.GetLocationsByID(accPettyCashPaymentHeader.LocationID).LocationCode, accPettyCashPaymentHeader.DocumentID, false).Trim();
                            //accPettyCashPaymentHeader.LedgerSerialNo = accPettyCashPaymentService.GetLedgerSerialNo(formName, accPettyCashPaymentHeader.LocationID, locationService.GetLocationsByID(accPettyCashPaymentHeader.LocationID).LocationCode, accPettyCashPaymentHeader.PettyCashLedgerID, accLedgerAccountService.GetAccLedgerAccountByID(accPettyCashPaymentHeader.PettyCashLedgerID).LedgerCode, accPettyCashPaymentHeader.DocumentID, false).Trim();
                        }

                        newDocumentNo = accPettyCashPaymentHeader.DocumentNo;

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        //context.Set<AccPettyCashPaymentDetail>().Delete(context.AccPettyCashPaymentDetails.Where(p => p.AccPettyCashPaymentHeaderID.Equals(accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID) && p.LocationID.Equals(accPettyCashPaymentHeader.LocationID) && p.DocumentID.Equals(accPettyCashPaymentHeader.DocumentID)).AsQueryable());
                        //context.SaveChanges();

                        accPettyCashPaymentHeader.AccPettyCashPaymentDetails = accPettyCashPaymentDetails.ToList();

                        if (accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID.Equals(0))
                        {
                            context.AccPettyCashPaymentHeaders.Add(accPettyCashPaymentHeader);
                            context.SaveChanges();
                        }
                        else
                        {
                            var existingAccPettyCashPaymentHeader = context.AccPettyCashPaymentHeaders.AsNoTracking().Include(s => s.AccPettyCashPaymentDetails)
                                                                   .Where(s => s.AccPettyCashPaymentHeaderID == accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID)
                                                                   .FirstOrDefault<AccPettyCashPaymentHeader>();

                            if (existingAccPettyCashPaymentHeader != null)
                            {
                                var existingAccPettyCashPaymentDetails = existingAccPettyCashPaymentHeader.AccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>();

                                var updatedAccPettyCashPaymentDetails = accPettyCashPaymentHeader.AccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>();

                                var addedAccPettyCashPaymentDetails = CommonService.Except(updatedAccPettyCashPaymentDetails, existingAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                                var deletedAccPettyCashPaymentDetails = CommonService.Except(existingAccPettyCashPaymentDetails, updatedAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                                var modifiedAccPettyCashPaymentDetails = CommonService.Except(updatedAccPettyCashPaymentDetails, addedAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                                addedAccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>().ForEach(paymentDeta => context.Entry(paymentDeta).State = EntityState.Added);

                                deletedAccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>().ForEach(paymentDeta => context.Entry(paymentDeta).State = EntityState.Deleted);

                                foreach (AccPettyCashPaymentDetail tempAccPettyCashPaymentDetail in modifiedAccPettyCashPaymentDetails)
                                {
                                    var existingAccPettyCashPaymentDetail = context.AccPettyCashPaymentDetails.Find(tempAccPettyCashPaymentDetail.AccPettyCashPaymentDetailID);

                                    if (existingAccPettyCashPaymentDetail != null)
                                    {
                                        var accPettyCashPaymentDetailEntry = context.Entry(existingAccPettyCashPaymentDetail);
                                        accPettyCashPaymentDetailEntry.CurrentValues.SetValues(tempAccPettyCashPaymentDetail);
                                    }
                                }

                                var existingAccPettyCashPaymentHeaders = context.AccPettyCashPaymentHeaders.Find(accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID);

                                if (existingAccPettyCashPaymentHeaders != null)
                                {
                                    var accPettyCashPaymentHeaderEntry = context.Entry(existingAccPettyCashPaymentHeaders);
                                    accPettyCashPaymentHeaderEntry.CurrentValues.SetValues(accPettyCashPaymentHeader);
                                }
                                context.SaveChanges();
                            }
                        }
                       
                        if (accPettyCashPaymentHeader.DocumentStatus.Equals(1))
                        {
                            #region Update Voucher Details
                            AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                            accPettyCashVoucherService.IssueAccPettyCashVoucherHeader(accPettyCashPaymentDetails);
                            #endregion

                            #region Update Imprest Details
                            UpdatePettyCashImprest(accPettyCashPaymentHeader);
                            #endregion
                        }

                        //#region Account Entries
                        //AddAccGlTransactionDetails(accPettyCashIOUHeader, isTStatus);
                        //#endregion
                        DateTime t1 = DateTime.UtcNow;

                        transactionScope.Complete();
                        string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                        return true;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void UpdatePettyCashImprest(AccPettyCashPaymentHeader accPettyCashPaymentHeader)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashPaymentHeader.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashPaymentHeader.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashPaymentHeader.PettyCashLedgerID;
            accPettyCashImprestDetail.IssuedAmount = accPettyCashPaymentHeader.Amount;
            accPettyCashImprestDetail.UsedAmount = accPettyCashPaymentHeader.Amount;

            accPettyCashImprestService.UpdatePettyCashImprest(accPettyCashImprestDetail);
        }

        public void UpdateAccPettyCashPaymentHeader(AccPettyCashPaymentHeader accPettyCashPaymentHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                try
                {
                    var existingAccPettyCashPaymentHeader = context.AccPettyCashPaymentHeaders.AsNoTracking().Include(s => s.AccPettyCashPaymentDetails)
                                                               .Where(s => s.AccPettyCashPaymentHeaderID == accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID)
                                                               .FirstOrDefault<AccPettyCashPaymentHeader>();

                    if (existingAccPettyCashPaymentHeader != null)
                    {
                        var existingAccPettyCashPaymentDetails = existingAccPettyCashPaymentHeader.AccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>();

                        var updatedAccPettyCashPaymentDetails = accPettyCashPaymentHeader.AccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>();

                        var addedAccPettyCashPaymentDetails = CommonService.Except(updatedAccPettyCashPaymentDetails, existingAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                        var deletedAccPettyCashPaymentDetails = CommonService.Except(existingAccPettyCashPaymentDetails, updatedAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                        var modifiedAccPettyCashPaymentDetails = CommonService.Except(updatedAccPettyCashPaymentDetails, addedAccPettyCashPaymentDetails, paymentDeta => paymentDeta.AccPettyCashPaymentDetailID);

                        addedAccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>().ForEach(paymentDeta => context.Entry(paymentDeta).State = EntityState.Added);

                        deletedAccPettyCashPaymentDetails.ToList<AccPettyCashPaymentDetail>().ForEach(paymentDeta => context.Entry(paymentDeta).State = EntityState.Deleted);

                        foreach (AccPettyCashPaymentDetail tempAccPettyCashPaymentDetail in modifiedAccPettyCashPaymentDetails)
                        {
                            var existingAccPettyCashPaymentDetail = context.AccPettyCashVoucherDetails.Find(tempAccPettyCashPaymentDetail.AccPettyCashPaymentDetailID);

                            if (existingAccPettyCashPaymentDetail != null)
                            {
                                var accPettyCashPaymentDetailEntry = context.Entry(existingAccPettyCashPaymentDetail);
                                accPettyCashPaymentDetailEntry.CurrentValues.SetValues(tempAccPettyCashPaymentDetail);
                            }
                        }

                        var existingAccPettyCashPaymentHeaders = context.AccPettyCashPaymentHeaders.Find(accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID);

                        if (existingAccPettyCashPaymentHeaders != null)
                        {
                            var accPettyCashPaymentHeaderEntry = context.Entry(existingAccPettyCashPaymentHeaders);
                            accPettyCashPaymentHeaderEntry.CurrentValues.SetValues(accPettyCashPaymentHeader);
                        }
                        context.SaveChanges();
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public List<AccPettyCashVoucherHeader> GetAllPettyCashPaymentDetail(AccPettyCashPaymentHeader accPettyCashPaymentHeader)
        {
            List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList = new List<AccPettyCashVoucherHeader>();

            AccPettyCashVoucherHeader accPettyCashVoucherHeaderTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashPaymentDetails = (from vh in context.AccPettyCashVoucherHeaders
                                                  join id in context.AccPettyCashPaymentDetails on vh.AccPettyCashVoucherHeaderID equals id.ReferenceDocumentID
                                              join ih in context.AccPettyCashPaymentHeaders on id.AccPettyCashPaymentHeaderID equals ih.AccPettyCashPaymentHeaderID
                                              join ld in context.AccLedgerAccounts on id.LedgerID equals ld.AccLedgerAccountID
                                              where ih.LocationID.Equals(accPettyCashPaymentHeader.LocationID) && ih.CompanyID.Equals(accPettyCashPaymentHeader.CompanyID) &&
                                              ih.DocumentID.Equals(accPettyCashPaymentHeader.DocumentID) && ih.AccPettyCashPaymentHeaderID.Equals(accPettyCashPaymentHeader.AccPettyCashPaymentHeaderID)
                                              select new //AccPettyCashPaymentDetail
                                              {
                                                  AccPettyCashVoucherHeaderID = vh.AccPettyCashVoucherHeaderID,
                                                  CompanyID = vh.CompanyID,
                                                  LocationID = vh.LocationID,
                                                  CostCentreID = vh.CostCentreID,
                                                  DocumentID = vh.DocumentID,
                                                  DocumentNo = vh.DocumentNo,
                                                  DocumentDate = id.DocumentDate,
                                                  PettyCashLedgerID = vh.PettyCashLedgerID,
                                                  Amount = vh.Amount,
                                                  IOUAmount = vh.IOUAmount,
                                                  BalanceAmount = vh.BalanceAmount,
                                                  EmployeeID = vh.EmployeeID,
                                                  PayeeName = vh.PayeeName,
                                                  Reference = vh.Reference,
                                                  Remark = vh.Remark,
                                                  PaymentDate = id.PaymentDate,
                                                  DocumentStatus = id.DocumentStatus,
                                                  VoucherStatus = vh.VoucherStatus,
                                                  CreatedDate = id.CreatedDate,
                                                  CreatedUser = id.CreatedUser,
                                                  ModifiedDate = id.ModifiedDate,
                                                  ModifiedUser = id.ModifiedUser,
                                                  GroupOfCompanyID = id.GroupOfCompanyID,
                                                  DataTransfer = id.DataTransfer,
                                                  IsUpLoad = id.IsUpLoad,
                                                  AccPettyCashVoucherDetails = vh.AccPettyCashVoucherDetails,
                                                  Status = true
                                              }).ToList();

                foreach (var tempPettyCashPayment in accPettyCashPaymentDetails)
                {
                    accPettyCashVoucherHeaderTemp = new AccPettyCashVoucherHeader();
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherHeaderID = tempPettyCashPayment.AccPettyCashVoucherHeaderID;
                    accPettyCashVoucherHeaderTemp.CompanyID = tempPettyCashPayment.CompanyID;
                    accPettyCashVoucherHeaderTemp.LocationID = tempPettyCashPayment.LocationID;
                    accPettyCashVoucherHeaderTemp.CostCentreID = tempPettyCashPayment.CostCentreID;
                    accPettyCashVoucherHeaderTemp.DocumentID = tempPettyCashPayment.DocumentID;
                    accPettyCashVoucherHeaderTemp.DocumentNo = tempPettyCashPayment.DocumentNo;
                    accPettyCashVoucherHeaderTemp.DocumentDate = tempPettyCashPayment.DocumentDate;
                    accPettyCashVoucherHeaderTemp.PettyCashLedgerID = tempPettyCashPayment.PettyCashLedgerID;
                    accPettyCashVoucherHeaderTemp.EmployeeID = tempPettyCashPayment.EmployeeID;
                    accPettyCashVoucherHeaderTemp.Reference = tempPettyCashPayment.Reference;
                    accPettyCashVoucherHeaderTemp.Remark = tempPettyCashPayment.Remark;
                    accPettyCashVoucherHeaderTemp.PayeeName = tempPettyCashPayment.PayeeName;
                    accPettyCashVoucherHeaderTemp.PaymentDate = tempPettyCashPayment.PaymentDate;
                    accPettyCashVoucherHeaderTemp.Amount = tempPettyCashPayment.Amount;
                    accPettyCashVoucherHeaderTemp.IOUAmount = tempPettyCashPayment.IOUAmount;
                    accPettyCashVoucherHeaderTemp.BalanceAmount = tempPettyCashPayment.BalanceAmount;
                    accPettyCashVoucherHeaderTemp.DocumentStatus = tempPettyCashPayment.DocumentStatus;
                    accPettyCashVoucherHeaderTemp.IsUpLoad = tempPettyCashPayment.IsUpLoad;
                    accPettyCashVoucherHeaderTemp.CreatedDate = tempPettyCashPayment.CreatedDate;
                    accPettyCashVoucherHeaderTemp.CreatedUser = tempPettyCashPayment.CreatedUser;
                    accPettyCashVoucherHeaderTemp.ModifiedDate = tempPettyCashPayment.ModifiedDate;
                    accPettyCashVoucherHeaderTemp.ModifiedUser = tempPettyCashPayment.ModifiedUser;
                    accPettyCashVoucherHeaderTemp.GroupOfCompanyID = tempPettyCashPayment.GroupOfCompanyID;
                    accPettyCashVoucherHeaderTemp.DataTransfer = tempPettyCashPayment.DataTransfer;
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherDetails = tempPettyCashPayment.AccPettyCashVoucherDetails;
                    accPettyCashVoucherHeaderTemp.Status = tempPettyCashPayment.Status;
                    if(tempPettyCashPayment.DocumentStatus==0)
                    { accPettyCashVoucherHeaderTemp.VoucherStatus = 1; }
                    else if(tempPettyCashPayment.DocumentStatus==1)
                    { accPettyCashVoucherHeaderTemp.VoucherStatus = 2; }
                    accPettyCashVoucherHeaderList.Add(accPettyCashVoucherHeaderTemp);
                }
            }

            return accPettyCashVoucherHeaderList;
        }

        /// <summary>
        /// Get Saved AccPettyCashPaymentHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashPaymentHeader GetAccPettyCashPaymentHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashPaymentHeaders.Include("AccPettyCashPaymentDetails").Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashPaymentHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public DataTable GetPendingPettyCashPaymentDocuments()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from poh in context.AccPettyCashPaymentHeaders
                           join l in context.Locations on poh.LocationID equals l.LocationID
                           join s in context.AccLedgerAccounts on poh.PettyCashLedgerID equals s.AccLedgerAccountID
                           where poh.DocumentStatus == 0
                           select new
                           {
                               poh.DocumentNo,
                               poh.DocumentDate,
                               DocumentLocation = l.LocationName,
                               LedgerName = s.LedgerName
                           }).ToArray();

                return sql.ToDataTable();
            }
        }

        public DataTable GetPettyCashPaymentTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (from ph in context.AccPettyCashPaymentHeaders
                             join pd in context.AccPettyCashPaymentDetails on ph.AccPettyCashPaymentHeaderID equals pd.AccPettyCashPaymentHeaderID
                             join pld in context.AccLedgerAccounts on ph.PettyCashLedgerID equals pld.AccLedgerAccountID
                             join ld in context.AccLedgerAccounts on pd.LedgerID equals ld.AccLedgerAccountID
                             join e in context.Employees on ph.EmployeeID equals e.EmployeeID
                             join l in context.Locations on ph.LocationID equals l.LocationID
                             join cs in context.CostCentres on ph.CostCentreID equals cs.CostCentreID
                             where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                             select new
                             {
                                 FieldString1 = ph.DocumentNo,
                                 FieldString2 = ph.DocumentDate,
                                 FieldString3 = pld.LedgerCode + "  " + pld.LedgerName,
                                 FieldString4 = ph.Remark,
                                 FieldString5 = e.EmployeeCode + "  " + e.EmployeeName,
                                 FieldString6 = ph.PayeeName,
                                 FieldString7 = "",
                                 FieldString8 = "",
                                 FieldString9 = ph.Reference,
                                 FieldString10 = "",
                                 FieldString11 = "",
                                 FieldString12 = l.LocationCode + "  " + l.LocationName,
                                 FieldString13 = ld.LedgerCode + "  " + ld.LedgerName,
                                 FieldString14 = "",
                                 FieldString15 = "",
                                 FieldString16 = "",
                                 FieldString17 = "",
                                 FieldString18 = "",
                                 FieldString19 = pd.Amount,
                                 FieldDecimal1 = "",
                                 FieldDecimal2 = "",
                                 FieldDecimal3 = "",
                                 //FieldDecimal4 = pd.CostPrice,
                                 //FieldDecimal5 = pd.DiscountPercentage,
                                 //FieldDecimal6 = pd.DiscountAmount,
                                 //FieldDecimal7 = pd.NetAmount,
                                 FieldString23 = ph.Amount,
                                 FieldString24 = "",
                                 FieldString25 = "",
                                 FieldString26 = "",
                                 FieldString27 = "",
                                 FieldString28 = "",
                                 FieldString29 = "",

                             }).ToArray();

                return query.ToDataTable();
            }
        }

        public DataTable GetPettyCashPaymentsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                //StringBuilder selectFields = new StringBuilder();
                var query = context.AccPettyCashPaymentHeaders.AsNoTracking().Where("DocumentStatus = @0", 1);

                // Insert Conditions to query
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LocationID":
                                query = (from qr in query
                                         join jt in context.Locations.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.LocationID equals jt.LocationID
                                         select qr
                                    );
                                break;
                            default:
                                query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "PettyCashLedgerID":
                                query = (from qr in query
                                         join jt in context.AccLedgerAccounts.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.PettyCashLedgerID equals jt.AccLedgerAccountID
                                         select qr
                                    );
                                break;
                            default:
                                query = query.Where(
                                    "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " +
                                    "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                    long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                    long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                break;
                        }

                    }

                }

                #region dynamic select
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{ selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                #endregion

                var queryResult = query.AsEnumerable();

                return (from h in queryResult
                        join s in context.AccLedgerAccounts on h.PettyCashLedgerID equals s.AccLedgerAccountID
                        join l in context.Locations on h.LocationID equals l.LocationID
                        select
                            new
                            {
                                FieldString1 = reportDataStructList[0].IsSelectionField == true ? l.LocationCode : "",
                                FieldString2 = reportDataStructList[1].IsSelectionField == true ? h.CreatedUser : "",
                                FieldString3 = reportDataStructList[2].IsSelectionField == true ? h.DocumentDate.ToShortDateString() : "",
                                FieldString4 = reportDataStructList[3].IsSelectionField == true ? s.LedgerCode + " " + s.LedgerName : "",
                                FieldString5 = reportDataStructList[4].IsSelectionField == true ? h.DocumentNo : "",
                                FieldString6 = reportDataStructList[5].IsSelectionField == true ? h.Reference : "",
                                FieldDecimal1 = reportDataStructList[6].IsSelectionField == true ? h.Amount : 0,

                                //FieldDecimal2 = reportDataStructList[7].IsSelectionField == true ? h.GrossAmount : 0,
                                //FieldDecimal3 = reportDataStructList[8].IsSelectionField == true ? h.DiscountAmount : 0,
                                //FieldDecimal4 = reportDataStructList[9].IsSelectionField == true ? h.TaxAmount1 : 0, // NBT(2%)
                                //FieldDecimal5 = reportDataStructList[10].IsSelectionField == true ? h.TaxAmount2 : 0, // NBT(2.04%)
                                //FieldDecimal6 = reportDataStructList[11].IsSelectionField == true ? h.TaxAmount3 : 0, // VAT
                                //FieldDecimal7 = reportDataStructList[12].IsSelectionField == true ? h.NetAmount : 0

                            }).ToArray().ToDataTable();

                //return null;
            }
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                // Create query 
                IQueryable qryResult = context.AccPettyCashPaymentHeaders.Where("DocumentStatus == @0", 1);

                switch (reportDataStruct.DbColumnName.Trim())
                {
                    case "PettyCashLedgerID":
                        qryResult = qryResult.Join(context.AccLedgerAccounts, reportDataStruct.DbColumnName.Trim(), "AccLedgerAccountID",
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    case "LocationID":
                        qryResult = qryResult.Join(context.Locations, reportDataStruct.DbColumnName.Trim(),
                                                   reportDataStruct.DbColumnName.Trim(),
                                                   "new(inner." + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .GroupBy("new(" + reportDataStruct.DbJoinColumnName.Trim() + ")",
                                                      "new(" + reportDataStruct.DbJoinColumnName.Trim() + ")")
                                             .OrderBy("Key." + reportDataStruct.DbJoinColumnName.Trim() + "")
                                             .Select("Key." + reportDataStruct.DbJoinColumnName.Trim() + "");
                        break;

                    default:
                        qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName.Trim() + ")",
                                                        "new(" + reportDataStruct.DbColumnName.Trim() + ")")
                                                .OrderBy("Key." + reportDataStruct.DbColumnName.Trim() + "")
                                                .Select("Key." + reportDataStruct.DbColumnName.Trim() + "");
                        break;

                }

                // change this code to get rid of the foreach loop
                ArrayList selectionDataList = new ArrayList();
                if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
                {
                    foreach (var item in qryResult)
                    {
                        if (item.Equals(true))
                        { selectionDataList.Add("Yes"); }
                        else
                        { selectionDataList.Add("No"); }
                    }
                }
                //else if (reportDataStruct.ValueDataType.Equals(typeof(int)))
                //{
                //    if (reportDataStruct.ReportFieldName.Trim() == "Document Status")
                //    {
                //        selectionDataList.Add("Partial");
                //        selectionDataList.Add("Opened");
                //        selectionDataList.Add("Expired");
                //    }
                //}
                else
                {
                    foreach (var item in qryResult)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        { selectionDataList.Add(item.ToString().Trim()); }
                    }
                }
                return selectionDataList;
            }
        }

        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            long tempCode = 0;

            string preFix = string.Empty;

            bool isLocationCode = autoGenerateInfo.IsLocationCode;
            int codeLength = autoGenerateInfo.CodeLength;
            preFix = autoGenerateInfo.Prefix.Trim();

            if (isTemporytNo)
            {
                if ((preFix.StartsWith("T")))
                { preFix = "X"; }
                else
                { preFix = "T"; }
            }

            if (isLocationCode)
            { preFix = preFix + locationCode.Trim(); }

            if (isTemporytNo)
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });
                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
            }
            else
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });
                    tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
                }
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }

        public string GetLedgerSerialNo(string formName, int locationID, string locationCode, long ledgerID, string ledgerCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            long tempCode = 0;

            string preFix = string.Empty;

            bool isLocationCode = autoGenerateInfo.IsLocationCode;
            int codeLength = autoGenerateInfo.CodeLength;
            preFix = autoGenerateInfo.Prefix.Trim();

            if (isTemporytNo)
            {
                if ((preFix.StartsWith("T")))
                { preFix = "X"; }
                else
                { preFix = "T"; }
            }

            if (isLocationCode)
            { preFix = preFix + locationCode.Trim(); }

            if (isTemporytNo)
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    context.AccLedgerSerialNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new AccLedgerSerialNumber { TempDocumentNo = d.TempDocumentNo + 1 });
                    tempCode = context.AccLedgerSerialNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
            }
            else
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    context.AccLedgerSerialNumbers.Update(d => d.LocationID.Equals(locationID) && d.LedgerID.Equals(ledgerID), d => new AccLedgerSerialNumber { DocumentNo = d.DocumentNo + 1 });
                    tempCode = context.AccLedgerSerialNumbers.Where(d => d.LocationID.Equals(locationID) && d.LedgerID.Equals(ledgerID)).Max(d => d.DocumentNo);
                }
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }

        #endregion
    }
}
