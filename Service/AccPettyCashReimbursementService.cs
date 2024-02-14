using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public class AccPettyCashReimbursementService
    {
        /// <summary>
        /// Save PettyCashReimbursement
        /// </summary>
        /// <param name="accPettyCashReimbursement"></param>
        public bool SavePettyCashReimbursement(AccPettyCashReimbursement accPettyCashReimbursement, out string newDocumentNo, string formName = "", bool isUpLoad = false)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (accPettyCashReimbursement.DocumentStatus.Equals(1))
                    {
                        AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                        LocationService locationService = new LocationService();
                        accPettyCashReimbursement.DocumentNo = accPettyCashBillService.GetDocumentNo(formName, accPettyCashReimbursement.LocationID, locationService.GetLocationsByID(accPettyCashReimbursement.LocationID).LocationCode, accPettyCashReimbursement.DocumentID, false).Trim();
                        //accPettyCashBillHeader.LedgerSerialNo = accPettyCashIOUService.GetLedgerSerialNo(formName, accPettyCashBillHeader.LocationID, locationService.GetLocationsByID(accPettyCashBillHeader.LocationID).LocationCode, accPettyCashBillHeader.DocumentID, false).Trim();
                    }

                    newDocumentNo = accPettyCashReimbursement.DocumentNo;

                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    context.Set<AccPettyCashBillDetail>().Delete(context.AccPettyCashBillDetails.Where(p => p.AccPettyCashBillHeaderID.Equals(accPettyCashReimbursement.AccPettyCashReimbursementID) && p.LocationID.Equals(accPettyCashReimbursement.LocationID) && p.DocumentID.Equals(accPettyCashReimbursement.DocumentID)).AsQueryable());
                    context.SaveChanges();

                    if (accPettyCashReimbursement.AccPettyCashReimbursementID.Equals(0))
                    { context.AccPettyCashReimbursements.Add(accPettyCashReimbursement); }
                    else
                    { context.Entry(accPettyCashReimbursement).State = EntityState.Modified; }

                    context.SaveChanges();
                    
                    if (accPettyCashReimbursement.DocumentStatus.Equals(1))
                    {
                        #region Update Imprest Details

                        //UpdatePettyCashImprest(accPettyCashReimbursement);


                        #endregion

                        #region Add Imprest Details

                        AddPettyCashImprest(accPettyCashReimbursement);
                        #endregion

                        #region Add Cheque Details
                        // as a payment record
                        PaymentMethodService paymentMethodService = new PaymentMethodService();
                        long paymentMethodID = paymentMethodService.GetPaymentMethodsByName("Cheque").PaymentMethodID;
                        if (accPettyCashReimbursement.PaymentMethodID == paymentMethodID)
                        {
                            AddAccChequeDetail(accPettyCashReimbursement);
                        }
                        #endregion

                        #region Account Entries

                        AddAccGlTransactionDetails(accPettyCashReimbursement, isUpLoad);

                        #endregion
                    }
                    DateTime t1 = DateTime.UtcNow;

                    transactionScope.Complete();
                    string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                    return true;
                }
            }
        }

        public void AddAccGlTransactionDetails(AccPettyCashReimbursement accPettyCashReimbursement, bool isUpLoad)
        {
            AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
            AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
            CompanyService companyService = new CompanyService();

            AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
            accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(accPettyCashReimbursement.AccPettyCashReimbursementID, accPettyCashReimbursement.DocumentID, accPettyCashReimbursement.LocationID);
            if (accGlTransactionHeader == null)
            { accGlTransactionHeader = new AccGlTransactionHeader(); }

            accGlTransactionHeader.Amount = accPettyCashReimbursement.ReimburseAmount;
            accGlTransactionHeader.CompanyID = accPettyCashReimbursement.CompanyID;
            accGlTransactionHeader.CostCentreID = companyService.GetCompaniesByID(accPettyCashReimbursement.CompanyID).CostCentreID;
            accGlTransactionHeader.CreatedDate = accPettyCashReimbursement.CreatedDate;
            accGlTransactionHeader.CreatedUser = accPettyCashReimbursement.CreatedUser;
            accGlTransactionHeader.DocumentDate = accPettyCashReimbursement.DocumentDate;
            accGlTransactionHeader.DocumentID = accPettyCashReimbursement.DocumentID;
            accGlTransactionHeader.DocumentStatus = accPettyCashReimbursement.DocumentStatus;
            accGlTransactionHeader.DocumentNo = accPettyCashReimbursement.DocumentNo;
            accGlTransactionHeader.GroupOfCompanyID = accPettyCashReimbursement.GroupOfCompanyID;
            accGlTransactionHeader.DataTransfer = accPettyCashReimbursement.DataTransfer;
            accGlTransactionHeader.IsUpLoad = false;
            accGlTransactionHeader.LocationID = accPettyCashReimbursement.LocationID;
            accGlTransactionHeader.ModifiedDate = accPettyCashReimbursement.ModifiedDate;
            accGlTransactionHeader.ModifiedUser = accPettyCashReimbursement.ModifiedUser;
            accGlTransactionHeader.ReferenceDocumentDocumentID = accPettyCashReimbursement.DocumentID;
            accGlTransactionHeader.ReferenceDocumentID = accPettyCashReimbursement.AccPettyCashReimbursementID;
            accGlTransactionHeader.ReferenceDocumentNo = accPettyCashReimbursement.DocumentNo;
            accGlTransactionHeader.ReferenceID = accPettyCashReimbursement.EmployeeID;

            using (ERPDbContext context = new ERPDbContext())
            {
                context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(accGlTransactionHeader.AccGlTransactionHeaderID) && p.LocationID.Equals(accGlTransactionHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(accGlTransactionHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());
            }
            AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

            //Have to read IOU A/C
            //accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID);

            List<AccGlTransactionDetail> accGlTransactionDetailTempList = new List<AccGlTransactionDetail>();
            
            var te = new AccGlTransactionDetail
            {
                AccGlTransactionDetailID = 0,
                AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                CompanyID = accGlTransactionHeader.CompanyID,
                LocationID = accGlTransactionHeader.LocationID,
                CostCentreID = accGlTransactionHeader.CostCentreID,
                DocumentNo = accGlTransactionHeader.DocumentNo,
                DocumentID = accGlTransactionHeader.DocumentID,
                LedgerID = accPettyCashReimbursement.PettyCashLedgerID,
                LedgerSerial = accPettyCashReimbursement.LedgerSerialNo,
                DocumentDate = accGlTransactionHeader.DocumentDate,
                PaymentDate = accPettyCashReimbursement.ChequeDate,
                ReferenceID = accGlTransactionHeader.ReferenceID,
                DrCr = 1,
                Amount = accGlTransactionHeader.Amount,
                //ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                //ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                //ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                ReferenceDocumentNo = string.Empty,
                TransactionTypeId = accGlTransactionHeader.DocumentID,
                TransactionDefinitionId = 1,
                ChequeNo = accPettyCashReimbursement.ChequeNo,
                Reference = accPettyCashReimbursement.Reference,
                Remark = accPettyCashReimbursement.Remark,
                DocumentStatus = accGlTransactionHeader.DocumentStatus,
                IsUpLoad = isUpLoad,
                GroupOfCompanyID = accGlTransactionHeader.GroupOfCompanyID,
                CreatedUser = accGlTransactionHeader.CreatedUser,
                CreatedDate = accGlTransactionHeader.CreatedDate,
                ModifiedUser = accGlTransactionHeader.ModifiedUser,
                ModifiedDate = accGlTransactionHeader.ModifiedDate,
                DataTransfer = accGlTransactionHeader.DataTransfer,
            };
            accGlTransactionDetailTempList.Add(te);
           
            var te1 = new AccGlTransactionDetail
            {
                AccGlTransactionDetailID = 0,
                AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                CompanyID = accGlTransactionHeader.CompanyID,
                LocationID = accGlTransactionHeader.LocationID,
                CostCentreID = accGlTransactionHeader.CostCentreID,
                DocumentNo = accGlTransactionHeader.DocumentNo,
                DocumentID = accGlTransactionHeader.DocumentID,
                LedgerID = accPettyCashReimbursement.LedgerID,
                LedgerSerial = accPettyCashReimbursement.LedgerSerialNo,
                DocumentDate = accGlTransactionHeader.DocumentDate,
                PaymentDate = accPettyCashReimbursement.ChequeDate,
                ReferenceID = accGlTransactionHeader.ReferenceID,
                DrCr = 2,
                Amount = accGlTransactionHeader.Amount,
                //ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                //ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                //ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                ReferenceDocumentNo = string.Empty,
                TransactionTypeId = accGlTransactionHeader.DocumentID,
                TransactionDefinitionId = 1,
                ChequeNo = accPettyCashReimbursement.ChequeNo,
                Reference = accPettyCashReimbursement.Reference,
                Remark = accPettyCashReimbursement.Remark,
                DocumentStatus = accPettyCashReimbursement.DocumentStatus,
                IsUpLoad = isUpLoad,
                GroupOfCompanyID = accPettyCashReimbursement.GroupOfCompanyID,
                CreatedUser = accPettyCashReimbursement.CreatedUser,
                CreatedDate = accPettyCashReimbursement.CreatedDate,
                ModifiedUser = accPettyCashReimbursement.ModifiedUser,
                ModifiedDate = accPettyCashReimbursement.ModifiedDate,
                DataTransfer = accPettyCashReimbursement.DataTransfer,
            };
            accGlTransactionDetailTempList.Add(te1);

            accGlTransactionHeader.AccGlTransactionDetails = accGlTransactionDetailTempList.ToList();

            bool isValid = accGlTransactionService.TallyTransactionEntry(accGlTransactionDetailTempList);
            if (isValid)
            {
                if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                    accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);
                else
                    accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);
            }
            else
            {
                return; // should rollback all transactions.
            }

            using (ERPDbContext context = new ERPDbContext())
            {
                context.SaveChanges();
            }
        }

        public void UpdatePettyCashImprest(AccPettyCashReimbursement accPettyCashReimbursement)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashReimbursement.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashReimbursement.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashReimbursement.PettyCashLedgerID;
            //accPettyCashImprestDetail.IssuedAmount = accPettyCashReimbursement.Amount;
            //accPettyCashImprestDetail.UsedAmount = accPettyCashReimbursement.Amount;

            accPettyCashImprestService.UpdatePettyCashImprest(accPettyCashImprestDetail);
        }

        public void AddPettyCashImprest(AccPettyCashReimbursement accPettyCashReimbursement)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashReimbursement.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashReimbursement.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashReimbursement.PettyCashLedgerID;
            accPettyCashImprestDetail.AvailabledAmount = accPettyCashReimbursement.ReimburseAmount;
            accPettyCashImprestDetail.BalanceAmount = accPettyCashReimbursement.ReimburseAmount;
            accPettyCashImprestDetail.ImprestAmount = accPettyCashReimbursement.ReimburseAmount;
            accPettyCashImprestDetail.ReferenceDocumentID = accPettyCashReimbursement.AccPettyCashReimbursementID;
            accPettyCashImprestDetail.ReferenceDocumentDocumentID = accPettyCashReimbursement.DocumentID;
            accPettyCashImprestDetail.ReferenceLocationID = accPettyCashReimbursement.LocationID;
            accPettyCashImprestDetail.DocumentStatus = 1;
            accPettyCashImprestDetail.CreatedUser = accPettyCashReimbursement.CreatedUser;
            accPettyCashImprestDetail.GroupOfCompanyID = accPettyCashReimbursement.GroupOfCompanyID;
            accPettyCashImprestDetail.CompanyID = accPettyCashReimbursement.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashReimbursement.LocationID;

            accPettyCashImprestService.AddPettyCashImprest(accPettyCashImprestDetail);
        }

        public void AddAccChequeDetail(AccPettyCashReimbursement accPettyCashReimbursement)
        {
            AccChequeDetailService accChequeDetailService = new AccChequeDetailService();
            AccChequeDetail accChequeDetail = new AccChequeDetail();
            CompanyService companyService = new CompanyService();

            accChequeDetail.CompanyID = accPettyCashReimbursement.CompanyID;
            accChequeDetail.LocationID = accPettyCashReimbursement.LocationID;
            accChequeDetail.CostCentreID = companyService.GetCompaniesByID(accPettyCashReimbursement.CompanyID).CostCentreID;
            accChequeDetail.DocumentNo = accPettyCashReimbursement.DocumentNo;
            accChequeDetail.DocumentID = accPettyCashReimbursement.DocumentID;
            accChequeDetail.DocumentDate = accPettyCashReimbursement.DocumentDate;
            accChequeDetail.ChequeDate = accPettyCashReimbursement.ChequeDate;
            accChequeDetail.BankID = accPettyCashReimbursement.LedgerID;
            accChequeDetail.DepositedBankID = accPettyCashReimbursement.LedgerID;
            accChequeDetail.BankBranchID = 0;
            accChequeDetail.CardNo = accPettyCashReimbursement.ChequeNo;
            accChequeDetail.Amount = accPettyCashReimbursement.ReimburseAmount;
            accChequeDetail.ChequeType = 1;
            accChequeDetail.ChequeStatus = 0;
            accChequeDetail.DocumentStatus = accPettyCashReimbursement.DocumentStatus;
            accChequeDetail.CreatedUser = accPettyCashReimbursement.CreatedUser;
            accChequeDetail.GroupOfCompanyID = accPettyCashReimbursement.GroupOfCompanyID;
            accChequeDetail.CompanyID = accPettyCashReimbursement.CompanyID;
            accChequeDetail.LocationID = accPettyCashReimbursement.LocationID;

            accChequeDetailService.AddAccChequeDetail(accChequeDetail);
        }

        /// <summary>
        /// Get Saved AccPettyCashReimbursement By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashReimbursement GetAccPettyCashReimbursementByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashReimbursements.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get Balanced AccPettyCashReimbursement By DocumentNo 
        /// </summary>
        /// <param name="pettyCashBookID"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashReimbursement GetAccPettyCashBalancedReimbursementHeader(long pettyCashBookID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashReimbursements.Where(ph => ph.PettyCashLedgerID.Equals(pettyCashBookID) && ph.LocationID.Equals(locationID) && ph.BalanceAmount > 0).FirstOrDefault<AccPettyCashReimbursement>();
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashReimbursements.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public DataTable GetPendingPettyCashReimbursementDocuments()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from poh in context.AccPettyCashReimbursements
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

        public DataTable GetPettyCashReimbursementTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (from ph in context.AccPettyCashReimbursements
                             join pld in context.AccLedgerAccounts on ph.PettyCashLedgerID equals pld.AccLedgerAccountID
                                join ld in context.AccLedgerAccounts on ph.LedgerID equals ld.AccLedgerAccountID
                                join e in context.Employees on ph.EmployeeID equals e.EmployeeID
                                join l in context.Locations on ph.LocationID equals l.LocationID
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
                                        FieldString14 = ph.ChequeDate,
                                        FieldString15 = ph.ChequeNo,
                                        FieldString16 = ph.ImprestAmount,
                                        FieldString17 = ph.IssuedAmount,
                                        FieldString18 = "",
                                        FieldDecimal1 = "",
                                        FieldDecimal2 = "",
                                        FieldDecimal3 = "",
                                        //FieldDecimal4 = pd.CostPrice,
                                        //FieldDecimal5 = pd.DiscountPercentage,
                                        //FieldDecimal6 = pd.DiscountAmount,
                                        //FieldDecimal7 = pd.NetAmount,
                                        FieldString23 = "",
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

        public DataTable GetPettyCashReimbursementsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                //StringBuilder selectFields = new StringBuilder();
                var query = context.AccPettyCashReimbursements.AsNoTracking().Where("DocumentStatus = @0", 1);

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
                                FieldDecimal1 = reportDataStructList[6].IsSelectionField == true ? h.ReimburseAmount : 0, 

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
                IQueryable qryResult = context.AccPettyCashReimbursements.Where("DocumentStatus == @0", 1);           

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
        #endregion
    }
}
