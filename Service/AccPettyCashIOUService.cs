using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using MoreLinq;
using Data;
using Domain;
using Utility;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Service
{
    public class AccPettyCashIOUService
    {
        #region Methods
        
        /// <summary>
        /// Save PettyCashIOU
        /// </summary>
        /// <param name="accPettyCashIOUHeader"></param>
        /// <param name="accPettyCashIOUDetails"></param>
        public bool SavePettyCashIOU(AccPettyCashIOUHeader accPettyCashIOUHeader, List<AccPettyCashIOUDetail> accPettyCashIOUDetails, out string newDocumentNo, string formName = "", bool isUpload = false)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (accPettyCashIOUHeader.DocumentStatus.Equals(1))
                    {
                        AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                        LocationService locationService = new LocationService();
                        accPettyCashIOUHeader.DocumentNo = accPettyCashIOUService.GetDocumentNo(formName, accPettyCashIOUHeader.LocationID, locationService.GetLocationsByID(accPettyCashIOUHeader.LocationID).LocationCode, accPettyCashIOUHeader.DocumentID, false).Trim();
                        //accPettyCashIOUHeader.LedgerSerialNo = accPettyCashIOUService.GetLedgerSerialNo(formName, accPettyCashIOUHeader.LocationID, locationService.GetLocationsByID(accPettyCashIOUHeader.LocationID).LocationCode, accPettyCashIOUHeader.DocumentID, false).Trim();
                        
                        //Note : set with follow necessary requirement
                        //accPettyCashIOUHeader.LedgerSerialNo = accPettyCashIOUService.GetLedgerSerialNo(formName, accPettyCashIOUHeader.PettyCashLedgerID, accPettyCashIOUHeader.LocationID, locationService.GetLocationsByID(accPettyCashIOUHeader.LocationID).LocationCode, accPettyCashIOUHeader.DocumentID, false).Trim();
                        accPettyCashIOUHeader.LedgerSerialNo = "";
                    }

                    newDocumentNo = accPettyCashIOUHeader.DocumentNo;

                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    AccPettyCashIOUDetail accPettyCashIOUDetail;
                    List<AccPettyCashIOUDetail> tempAccPettyCashIOUDetailList = new List<AccPettyCashIOUDetail>();
                    foreach (AccPettyCashIOUDetail tempAccPettyCashIOUDetail in accPettyCashIOUDetails)
                    {
                        accPettyCashIOUDetail = new AccPettyCashIOUDetail();
                        accPettyCashIOUDetail.GroupOfCompanyID = accPettyCashIOUHeader.GroupOfCompanyID;
                        accPettyCashIOUDetail.CompanyID = accPettyCashIOUHeader.CompanyID;
                        accPettyCashIOUDetail.LocationID = accPettyCashIOUHeader.LocationID;
                        accPettyCashIOUDetail.DocumentID = accPettyCashIOUHeader.DocumentID;
                        accPettyCashIOUDetail.DocumentStatus = accPettyCashIOUHeader.DocumentStatus;
                        accPettyCashIOUDetail.AccPettyCashIOUHeaderID = accPettyCashIOUHeader.AccPettyCashIOUHeaderID;
                        accPettyCashIOUDetail.AccPettyCashIOUDetailID = tempAccPettyCashIOUDetail.AccPettyCashIOUDetailID;
                        accPettyCashIOUDetail.CreatedUser = accPettyCashIOUHeader.CreatedUser;
                        accPettyCashIOUDetail.CreatedDate = accPettyCashIOUHeader.CreatedDate;
                        accPettyCashIOUDetail.ModifiedUser = accPettyCashIOUHeader.ModifiedUser;
                        accPettyCashIOUDetail.ModifiedDate = accPettyCashIOUHeader.ModifiedDate;
                        accPettyCashIOUDetail.LedgerID = tempAccPettyCashIOUDetail.LedgerID;
                        accPettyCashIOUDetail.Amount = tempAccPettyCashIOUDetail.Amount;
                        accPettyCashIOUDetail.LedgerCode = tempAccPettyCashIOUDetail.LedgerCode;
                        accPettyCashIOUDetail.LedgerName = tempAccPettyCashIOUDetail.LedgerName;
                        accPettyCashIOUDetail.AccPettyCashIOUHeader = tempAccPettyCashIOUDetail.AccPettyCashIOUHeader;

                        tempAccPettyCashIOUDetailList.Add(accPettyCashIOUDetail);
                    }
                    accPettyCashIOUDetails = null;
                    accPettyCashIOUDetails = tempAccPettyCashIOUDetailList.ToList();
                    
                    if (accPettyCashIOUHeader.AccPettyCashIOUHeaderID.Equals(0))
                    {
                        accPettyCashIOUHeader.AccPettyCashIOUDetails = accPettyCashIOUDetails.ToList();
                        context.AccPettyCashIOUHeaders.Add(accPettyCashIOUHeader);
                        context.SaveChanges();
                    }
                    else
                    {
                        accPettyCashIOUHeader.AccPettyCashIOUDetails = accPettyCashIOUDetails.ToList();
                        
                        var existingAccPettyCashIOUHeader = context.AccPettyCashIOUHeaders.AsNoTracking().Include(s => s.AccPettyCashIOUDetails)
                                                                   .Where(s => s.AccPettyCashIOUHeaderID == accPettyCashIOUHeader.AccPettyCashIOUHeaderID)
                                                                   .FirstOrDefault<AccPettyCashIOUHeader>();

                        if (existingAccPettyCashIOUHeader != null)
                        {
                            var existingAccPettyCashIOUDetails = existingAccPettyCashIOUHeader.AccPettyCashIOUDetails.ToList<AccPettyCashIOUDetail>();

                            var updatedAccPettyCashIOUDetails = accPettyCashIOUHeader.AccPettyCashIOUDetails.ToList<AccPettyCashIOUDetail>();

                            var addedAccPettyCashIOUDetails = CommonService.Except(updatedAccPettyCashIOUDetails, existingAccPettyCashIOUDetails, iouDeta => iouDeta.AccPettyCashIOUDetailID);

                            var deletedAccPettyCashIOUDetails = CommonService.Except(existingAccPettyCashIOUDetails, updatedAccPettyCashIOUDetails, iouDeta => iouDeta.AccPettyCashIOUDetailID);

                            var modifiedAccPettyCashIOUDetails = CommonService.Except(updatedAccPettyCashIOUDetails, addedAccPettyCashIOUDetails, iouDeta => iouDeta.AccPettyCashIOUDetailID);

                            addedAccPettyCashIOUDetails.ToList<AccPettyCashIOUDetail>().ForEach(iouDeta => context.Entry(iouDeta).State = EntityState.Added);

                            deletedAccPettyCashIOUDetails.ToList<AccPettyCashIOUDetail>().ForEach(iouDeta => context.Entry(iouDeta).State = EntityState.Deleted);

                            foreach (AccPettyCashIOUDetail tempAccPettyCashIOUDetail in modifiedAccPettyCashIOUDetails)
                            {
                                var existingAccPettyCashIOUDetail = context.AccPettyCashIOUDetails.Find(tempAccPettyCashIOUDetail.AccPettyCashIOUDetailID);

                                if (existingAccPettyCashIOUDetail != null)
                                {
                                    var accPettyCashIOUDetailEntry = context.Entry(existingAccPettyCashIOUDetail);
                                    accPettyCashIOUDetailEntry.CurrentValues.SetValues(tempAccPettyCashIOUDetail);
                                }
                            }
                            
                            var existingAccPettyCashIOUHeaders = context.AccPettyCashIOUHeaders.Find(accPettyCashIOUHeader.AccPettyCashIOUHeaderID);

                            if (existingAccPettyCashIOUHeaders != null)
                            {
                                var accPettyCashIOUHeaderEntry = context.Entry(existingAccPettyCashIOUHeaders);
                                accPettyCashIOUHeaderEntry.CurrentValues.SetValues(accPettyCashIOUHeader);
                            }
                            context.SaveChanges();
                        }
                    }

                    if (accPettyCashIOUHeader.DocumentStatus.Equals(1))
                    {
                        #region Update Imprest Details

                        UpdatePettyCashImprest(accPettyCashIOUHeader);

                        #endregion

                        #region Update Reimbursement

                        UpdatePettyCashReimbursement(accPettyCashIOUHeader);
                        #endregion

                        #region Account Entries

                        AddAccGlTransactionDetails(accPettyCashIOUHeader, isUpload);

                        #endregion
                    }

                    DateTime t1 = DateTime.UtcNow;

                    transactionScope.Complete();
                    string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                    return true;
                }
            }
        }

        public void UpdatePettyCashImprest(AccPettyCashIOUHeader accPettyCashIOUHeader)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashIOUHeader.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashIOUHeader.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashIOUHeader.PettyCashLedgerID;
            accPettyCashImprestDetail.IssuedAmount = accPettyCashIOUHeader.Amount;
            accPettyCashImprestDetail.UsedAmount = accPettyCashIOUHeader.Amount;

            accPettyCashImprestService.UpdatePettyCashImprest(accPettyCashImprestDetail);
        }

        public void AddAccGlTransactionDetails(AccPettyCashIOUHeader accPettyCashIOUHeader, bool isUpload)
        {
            AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
            AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
            CompanyService companyService = new CompanyService();

            AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();

            using (ERPDbContext context = new ERPDbContext())
            {
                accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(accPettyCashIOUHeader.AccPettyCashIOUHeaderID, accPettyCashIOUHeader.DocumentID, accPettyCashIOUHeader.LocationID);
                if (accGlTransactionHeader == null)
                { accGlTransactionHeader = new AccGlTransactionHeader(); }

                #region Fill GL Header
                accGlTransactionHeader.Amount = accPettyCashIOUHeader.Amount;
                accGlTransactionHeader.CompanyID = accPettyCashIOUHeader.CompanyID;
                accGlTransactionHeader.CostCentreID = companyService.GetCompaniesByID(accPettyCashIOUHeader.CompanyID).CostCentreID;
                accGlTransactionHeader.CreatedDate = accPettyCashIOUHeader.CreatedDate;
                accGlTransactionHeader.CreatedUser = accPettyCashIOUHeader.CreatedUser;
                accGlTransactionHeader.DocumentDate = accPettyCashIOUHeader.DocumentDate;
                accGlTransactionHeader.DocumentID = accPettyCashIOUHeader.DocumentID;
                accGlTransactionHeader.DocumentStatus = accPettyCashIOUHeader.DocumentStatus;
                accGlTransactionHeader.DocumentNo = accPettyCashIOUHeader.DocumentNo;
                accGlTransactionHeader.GroupOfCompanyID = accPettyCashIOUHeader.GroupOfCompanyID;
                accGlTransactionHeader.DataTransfer = accPettyCashIOUHeader.DataTransfer;
                accGlTransactionHeader.IsUpLoad = false;
                accGlTransactionHeader.LocationID = accPettyCashIOUHeader.LocationID;
                accGlTransactionHeader.ModifiedDate = accPettyCashIOUHeader.ModifiedDate;
                accGlTransactionHeader.ModifiedUser = accPettyCashIOUHeader.ModifiedUser;
                accGlTransactionHeader.ReferenceDocumentDocumentID = accPettyCashIOUHeader.DocumentID;
                accGlTransactionHeader.ReferenceDocumentID = accPettyCashIOUHeader.AccPettyCashIOUHeaderID;
                accGlTransactionHeader.ReferenceDocumentNo = accPettyCashIOUHeader.DocumentNo;
                accGlTransactionHeader.ReferenceID = accPettyCashIOUHeader.EmployeeID;
                #endregion

                context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(accGlTransactionHeader.AccGlTransactionHeaderID) && p.LocationID.Equals(accGlTransactionHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(accGlTransactionHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());
            
                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

                //Have to read IOU A/C
                accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID);

                List<AccGlTransactionDetail> accGlTransactionDetailTempList = new List<AccGlTransactionDetail>();

                #region Fill GL Detail
                if (accTransactionTypeDetail != null)
                {
                    var te = new AccGlTransactionDetail
                    {
                        AccGlTransactionDetailID = 0,
                        AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                        CompanyID = accGlTransactionHeader.CompanyID,
                        LocationID = accGlTransactionHeader.LocationID,
                        CostCentreID = accGlTransactionHeader.CostCentreID,
                        DocumentNo = accGlTransactionHeader.DocumentNo,
                        DocumentID = accGlTransactionHeader.DocumentID,
                        LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                        LedgerSerial = accPettyCashIOUHeader.LedgerSerialNo,
                        DocumentDate = accGlTransactionHeader.DocumentDate,
                        PaymentDate = accGlTransactionHeader.DocumentDate,
                        ReferenceID = accGlTransactionHeader.ReferenceID,
                        DrCr = accTransactionTypeDetail.DrCr,
                        Amount = accGlTransactionHeader.Amount,
                        //ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                        //ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                        //ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                        ReferenceDocumentNo = string.Empty,
                        TransactionTypeId = accGlTransactionHeader.DocumentID,
                        TransactionDefinitionId = 1,
                        ChequeNo = string.Empty,
                        Reference = accPettyCashIOUHeader.Reference,
                        Remark = accPettyCashIOUHeader.Remark,
                        DocumentStatus = accGlTransactionHeader.DocumentStatus,
                        IsUpLoad = isUpload,
                        GroupOfCompanyID = accGlTransactionHeader.GroupOfCompanyID,
                        CreatedUser = accGlTransactionHeader.CreatedUser,
                        CreatedDate = accGlTransactionHeader.CreatedDate,
                        ModifiedUser = accGlTransactionHeader.ModifiedUser,
                        ModifiedDate = accGlTransactionHeader.ModifiedDate,
                        DataTransfer = accGlTransactionHeader.DataTransfer,
                    };
                    accGlTransactionDetailTempList.Add(te);
                }
                int DrCr = 0;

                if (accTransactionTypeDetail!=null)
                {
                    if (accTransactionTypeDetail.DrCr != null)
                    { DrCr = ((accTransactionTypeDetail.DrCr == 1) ? 2 : 1); }

                    var te1 = new AccGlTransactionDetail
                    {
                        AccGlTransactionDetailID = 0,
                        AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                        CompanyID = accGlTransactionHeader.CompanyID,
                        LocationID = accGlTransactionHeader.LocationID,
                        CostCentreID = accGlTransactionHeader.CostCentreID,
                        DocumentNo = accGlTransactionHeader.DocumentNo,
                        DocumentID = accGlTransactionHeader.DocumentID,
                        LedgerID = accPettyCashIOUHeader.PettyCashLedgerID,
                        LedgerSerial = accPettyCashIOUHeader.LedgerSerialNo,
                        DocumentDate = accGlTransactionHeader.DocumentDate,
                        PaymentDate = accGlTransactionHeader.DocumentDate,
                        ReferenceID = accGlTransactionHeader.ReferenceID,
                        DrCr = DrCr,
                        Amount = accPettyCashIOUHeader.Amount,
                        //ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                        //ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                        //ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                        ReferenceDocumentNo = string.Empty,
                        TransactionTypeId = accGlTransactionHeader.DocumentID,
                        TransactionDefinitionId = accTransactionTypeDetail.TransactionDefinition,
                        ChequeNo = string.Empty,
                        Reference = accPettyCashIOUHeader.Reference,
                        Remark = accPettyCashIOUHeader.Remark,
                        DocumentStatus = accPettyCashIOUHeader.DocumentStatus,
                        IsUpLoad = isUpload,
                        GroupOfCompanyID = accPettyCashIOUHeader.GroupOfCompanyID,
                        CreatedUser = accPettyCashIOUHeader.CreatedUser,
                        CreatedDate = accPettyCashIOUHeader.CreatedDate,
                        ModifiedUser = accPettyCashIOUHeader.ModifiedUser,
                        ModifiedDate = accPettyCashIOUHeader.ModifiedDate,
                        DataTransfer = accPettyCashIOUHeader.DataTransfer,
                    };
                    accGlTransactionDetailTempList.Add(te1);
                }
                #endregion

                accGlTransactionHeader.AccGlTransactionDetails = accGlTransactionDetailTempList.ToList();
            
                bool isValid = accGlTransactionService.TallyTransactionEntry(accGlTransactionDetailTempList);
                if (isValid)
                {
                    if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                    {accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);}
                    else
                    {accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);}
                }
                else
                {
                    return; // should rollback all transactions.
                }
            
                context.SaveChanges();
            }
        }

        public void UpdateBillSettledPettyCashIOU(AccPettyCashBillHeader accPettyCashBillHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var existingAccPettyCashIOUHeader = context.AccPettyCashIOUHeaders.AsNoTracking().Include(s => s.AccPettyCashIOUDetails)
                                                                       .Where(s => s.AccPettyCashIOUHeaderID == accPettyCashBillHeader.ReferenceDocumentID && s.DocumentID == accPettyCashBillHeader.ReferenceDocumentDocumentID && s.LocationID == accPettyCashBillHeader.ReferenceLocationID)
                                                                       .FirstOrDefault<AccPettyCashIOUHeader>();

                if (existingAccPettyCashIOUHeader != null)
                {
                    existingAccPettyCashIOUHeader.IOUStatus = 2;

                    var existingAccPettyCashIOUHeaders = context.AccPettyCashIOUHeaders.Find(existingAccPettyCashIOUHeader.AccPettyCashIOUHeaderID);

                    if (existingAccPettyCashIOUHeaders != null)
                    {
                        var accPettyCashIOUHeaderEntry = context.Entry(existingAccPettyCashIOUHeaders);
                        accPettyCashIOUHeaderEntry.CurrentValues.SetValues(existingAccPettyCashIOUHeader);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void UpdatePettyCashReimbursement(AccPettyCashIOUHeader accPettyCashIOUHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var existingAccPettyCashReimbursement = context.AccPettyCashReimbursements.AsNoTracking()
                                                                       .Where(s => s.AccPettyCashReimbursementID == accPettyCashIOUHeader.ReferenceDocumentID && s.DocumentID == accPettyCashIOUHeader.ReferenceDocumentDocumentID && s.LocationID == accPettyCashIOUHeader.ReferenceLocationID)
                                                                       .FirstOrDefault<AccPettyCashReimbursement>();

                if (existingAccPettyCashReimbursement != null && existingAccPettyCashReimbursement.BalanceAmount > accPettyCashIOUHeader.Amount)
                {
                    existingAccPettyCashReimbursement.BalanceAmount = existingAccPettyCashReimbursement.BalanceAmount - accPettyCashIOUHeader.Amount;

                    var existingAccPettyCashReimbursements = context.AccPettyCashReimbursements.Find(existingAccPettyCashReimbursement.AccPettyCashReimbursementID);

                    if (existingAccPettyCashReimbursements != null)
                    {
                        var accPettyCashReimbursementEntry = context.Entry(existingAccPettyCashReimbursements);
                        accPettyCashReimbursementEntry.CurrentValues.SetValues(existingAccPettyCashReimbursement);
                    }
                    context.SaveChanges();
                }
                else
                {
                    return;
                }
            }
        }

        public List<AccPettyCashIOUDetail> GetAllPettyCashIOUDetail(AccPettyCashIOUHeader accPettyCashIOUHeader)
        {
            List<AccPettyCashIOUDetail> accPettyCashIOUDetailList = new List<AccPettyCashIOUDetail>();

            AccPettyCashIOUDetail accPettyCashIOUDetailTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashIOUDetails = (from id in context.AccPettyCashIOUDetails
                                             join ih in context.AccPettyCashIOUHeaders on id.AccPettyCashIOUHeaderID equals ih.AccPettyCashIOUHeaderID
                                             join ld in context.AccLedgerAccounts on id.LedgerID equals ld.AccLedgerAccountID
                                             where ih.LocationID.Equals(accPettyCashIOUHeader.LocationID) && ih.CompanyID.Equals(accPettyCashIOUHeader.CompanyID) &&
                                             ih.DocumentID.Equals(accPettyCashIOUHeader.DocumentID) && ih.AccPettyCashIOUHeaderID.Equals(accPettyCashIOUHeader.AccPettyCashIOUHeaderID)
                                             select new 
                                             {
                                                 AccPettyCashIOUDetailID = id.AccPettyCashIOUDetailID,
                                                 AccPettyCashIOUHeaderID = id.AccPettyCashIOUHeaderID,
                                                 CompanyID = id.CompanyID,
                                                 LocationID = id.LocationID,
                                                 DocumentID = id.DocumentID,
                                                 LedgerID = id.LedgerID,
                                                 LedgerCode = ld.LedgerCode,
                                                 LedgerName = ld.LedgerName,
                                                 Amount = id.Amount,
                                                 DocumentStatus = id.DocumentStatus,
                                                 IsUpLoad = id.IsUpLoad,
                                                 AccPettyCashIOUHeader = id.AccPettyCashIOUHeader,
                                                 CreatedDate = id.CreatedDate,
                                                 CreatedUser = id.CreatedUser,
                                                 ModifiedDate = id.ModifiedDate,
                                                 ModifiedUser = id.ModifiedUser,
                                                 GroupOfCompanyID = id.GroupOfCompanyID,
                                                 DataTransfer = id.DataTransfer
                                             }).ToList();

                foreach (var tempGiftVoucher in accPettyCashIOUDetails)
                {
                    accPettyCashIOUDetailTemp = new AccPettyCashIOUDetail();
                    accPettyCashIOUDetailTemp.AccPettyCashIOUDetailID = tempGiftVoucher.AccPettyCashIOUDetailID;
                    accPettyCashIOUDetailTemp.AccPettyCashIOUHeaderID = tempGiftVoucher.AccPettyCashIOUHeaderID;
                    accPettyCashIOUDetailTemp.CompanyID = tempGiftVoucher.CompanyID;
                    accPettyCashIOUDetailTemp.LocationID = tempGiftVoucher.LocationID;
                    accPettyCashIOUDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                    accPettyCashIOUDetailTemp.LedgerID = tempGiftVoucher.LedgerID;
                    accPettyCashIOUDetailTemp.LedgerCode = tempGiftVoucher.LedgerCode;
                    accPettyCashIOUDetailTemp.LedgerName = tempGiftVoucher.LedgerName;
                    accPettyCashIOUDetailTemp.Amount = tempGiftVoucher.Amount;
                    accPettyCashIOUDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                    accPettyCashIOUDetailTemp.IsUpLoad = tempGiftVoucher.IsUpLoad;
                    accPettyCashIOUDetailTemp.AccPettyCashIOUHeader = tempGiftVoucher.AccPettyCashIOUHeader;
                    accPettyCashIOUDetailTemp.CreatedDate = tempGiftVoucher.CreatedDate;
                    accPettyCashIOUDetailTemp.CreatedUser = tempGiftVoucher.CreatedUser;
                    accPettyCashIOUDetailTemp.ModifiedDate = tempGiftVoucher.ModifiedDate;
                    accPettyCashIOUDetailTemp.ModifiedUser = tempGiftVoucher.ModifiedUser;
                    accPettyCashIOUDetailTemp.GroupOfCompanyID = tempGiftVoucher.GroupOfCompanyID;
                    accPettyCashIOUDetailTemp.DataTransfer = tempGiftVoucher.DataTransfer;
                    accPettyCashIOUDetailList.Add(accPettyCashIOUDetailTemp);
                }
            }
            
            return accPettyCashIOUDetailList;
        }

        public decimal GetAllNotSettelledIOUDocuments(long employeeID)
        {
            decimal iouBalanceOutstanding = 0;
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    var balanceTotalIOU = from m in context.AccPettyCashIOUHeaders
                                                where m.EmployeeID == employeeID && m.DocumentStatus == 1
                                                && m.IOUStatus == 1
                                                group m by new { m.EmployeeID, m.LocationID } into g
                                                select new { TotalBalanceIOU = g.Sum(p => p.Amount) }.TotalBalanceIOU;

                    iouBalanceOutstanding = balanceTotalIOU.FirstOrDefault();
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
            return iouBalanceOutstanding;
        }
        
        /// <summary>
        /// Get Saved AccPettyCashIOUHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashIOUHeader GetAccPettyCashIOUHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashIOUHeaders.Include("AccPettyCashIOUDetails").Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault<AccPettyCashIOUHeader>();
            }
        }

        /// <summary>
        /// Get Saved AccPettyCashIOUHeader By DocumentNo 
        /// </summary>
        /// <param name="documentDocumentID"></param>
        /// <param name="documentID"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashIOUHeader GetAccPettyCashIOUHeaderByDocumentNo(int documentDocumentID, long documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashIOUHeaders.Include("AccPettyCashIOUDetails").Where(ph => ph.DocumentID.Equals(documentDocumentID) && ph.AccPettyCashIOUHeaderID.Equals(documentID) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get Saved AccPettyCashIOUHeader By IOU No 
        /// </summary>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashIOUHeader GetAccPettyCashSettlementsPendingIOUHeaderByDocumentNo(string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashIOUHeaders.Where(ph => ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID) && ph.IOUStatus == 1).FirstOrDefault();
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashIOUHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public string[] GetAllDocumentNumbersToBill(int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashIOUHeaders.Where(q => q.LocationID == locationID && q.DocumentStatus.Equals(1) && q.IOUStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public DataTable GetPendingPettyCashIOUDocumentsToBill()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from ph in context.AccPettyCashIOUHeaders
                           join pd in context.AccPettyCashIOUDetails on ph.AccPettyCashIOUHeaderID equals pd.AccPettyCashIOUHeaderID
                           join l in context.Locations on ph.LocationID equals l.LocationID
                           join s in context.AccLedgerAccounts on ph.PettyCashLedgerID equals s.AccLedgerAccountID
                           where ph.DocumentStatus == 1 && ph.IOUStatus == 1
                           group pd by new
                           {
                               ph.DocumentNo,
                               ph.DocumentDate,
                               DocumentLocation = l.LocationName,
                               PettyCashLedgerName = s.LedgerName
                           } into poGroup
                           orderby poGroup.Key.DocumentNo descending
                           select new
                           {
                               DocumentNo = poGroup.Key.DocumentNo,
                               DocumentDate = poGroup.Key.DocumentDate,
                               DocumentLocation = poGroup.Key.DocumentLocation,
                               PettyCashLedgerName = poGroup.Key.PettyCashLedgerName
                           }).ToArray();

                return sql.ToDataTable();
            }
        }

        public DataTable GetPendingPettyCashIOUDocuments()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from poh in context.AccPettyCashIOUHeaders
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

        public DataTable GetPettyCashIOUTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (from ph in context.AccPettyCashIOUHeaders
                             join pd in context.AccPettyCashIOUDetails on ph.AccPettyCashIOUHeaderID equals pd.AccPettyCashIOUHeaderID
                             join pld in context.AccLedgerAccounts on ph.PettyCashLedgerID equals pld.AccLedgerAccountID
                             join ld in context.AccLedgerAccounts on pd.LedgerID equals ld.AccLedgerAccountID
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

        public DataTable GetPettyCashIOUsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                //StringBuilder selectFields = new StringBuilder();
                var query = context.AccPettyCashIOUHeaders.AsNoTracking().Where("DocumentStatus = @0", 1);

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
                IQueryable qryResult = context.AccPettyCashIOUHeaders.Where("DocumentStatus == @0", 1);

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

            using (ERPDbContext context = new ERPDbContext())
            {
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
                    context.DocumentNumbers.Update( d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber {TempDocumentNo = d.TempDocumentNo + 1});
                    tempCode = context.DocumentNumbers.Where( d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
                else
                {
                    context.DocumentNumbers.Update( d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber {DocumentNo = d.DocumentNo + 1});
                    tempCode = context.DocumentNumbers.Where( d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
                }

                getNewCode = (tempCode).ToString();
                getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
                return getNewCode.ToUpper();
            }
        }

        public string GetLedgerSerialNo(string formName, long ledgerID, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            long tempCode = 0;

            string preFix = string.Empty;

            using (ERPDbContext context = new ERPDbContext())
            {
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
                {preFix = preFix + locationCode.Trim();}

                if (isTemporytNo)
                {
                    context.AccLedgerSerialNumbers.Update(d => d.LedgerID.Equals(ledgerID) && d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new AccLedgerSerialNumber { TempDocumentNo = d.TempDocumentNo + 1 });
                    tempCode = context.AccLedgerSerialNumbers.Where(d => d.LedgerID.Equals(ledgerID) && d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
                }
                else
                {
                    context.AccLedgerSerialNumbers.Update(d => d.LedgerID.Equals(ledgerID) && d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new AccLedgerSerialNumber { DocumentNo = d.DocumentNo + 1 });
                    tempCode = context.AccLedgerSerialNumbers.Where(d => d.LedgerID.Equals(ledgerID) && d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
                }

                getNewCode = (tempCode).ToString();
                getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
                return getNewCode.ToUpper();
            }
        }

        #endregion
        #endregion
    }
}
