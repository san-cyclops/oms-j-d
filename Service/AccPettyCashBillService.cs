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
    public class AccPettyCashBillService
    {
        #region Methods
        /// <summary>
        /// Save PettyCashBill
        /// </summary>
        /// <param name="accPettyCashBillHeader"></param>
        /// <param name="accPettyCashBillDetails"></param>
        public bool SavePettyCashBill(AccPettyCashBillHeader accPettyCashBillHeader, List<AccPettyCashBillDetail> accPettyCashBillDetails, out string newDocumentNo, string formName = "", bool isUpload = false)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                try
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (accPettyCashBillHeader.DocumentStatus.Equals(1))
                        {
                            AccPettyCashBillService accPettyCashBillService = new AccPettyCashBillService();
                            LocationService locationService = new LocationService();
                            accPettyCashBillHeader.DocumentNo = accPettyCashBillService.GetDocumentNo(formName, accPettyCashBillHeader.LocationID, locationService.GetLocationsByID(accPettyCashBillHeader.LocationID).LocationCode, accPettyCashBillHeader.DocumentID, false).Trim();
                            //accPettyCashBillHeader.LedgerSerialNo = accPettyCashIOUService.GetLedgerSerialNo(formName, accPettyCashBillHeader.LocationID, locationService.GetLocationsByID(accPettyCashBillHeader.LocationID).LocationCode, accPettyCashBillHeader.DocumentID, false).Trim();
                        }

                        newDocumentNo = accPettyCashBillHeader.DocumentNo;

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        //context.Set<AccPettyCashBillDetail>().Delete(context.AccPettyCashBillDetails.Where(p => p.AccPettyCashBillHeaderID.Equals(accPettyCashBillHeader.AccPettyCashBillHeaderID) && p.LocationID.Equals(accPettyCashBillHeader.LocationID) && p.DocumentID.Equals(accPettyCashBillHeader.DocumentID)).AsQueryable());
                        //context.SaveChanges();

                        AccPettyCashBillDetail accPettyCashBillDetail;
                        List<AccPettyCashBillDetail> tempAccPettyCashBillDetailList = new List<AccPettyCashBillDetail>();
                        foreach (AccPettyCashBillDetail tempAccPettyCashBillDetail in accPettyCashBillDetails)
                        {
                            accPettyCashBillDetail = new AccPettyCashBillDetail();
                            accPettyCashBillDetail.GroupOfCompanyID = accPettyCashBillHeader.GroupOfCompanyID;
                            accPettyCashBillDetail.CompanyID = accPettyCashBillHeader.CompanyID;
                            accPettyCashBillDetail.LocationID = accPettyCashBillHeader.LocationID;
                            accPettyCashBillDetail.CostCentreID = accPettyCashBillHeader.CostCentreID;
                            accPettyCashBillDetail.DocumentID = accPettyCashBillHeader.DocumentID;
                            accPettyCashBillDetail.DocumentStatus = accPettyCashBillHeader.DocumentStatus;
                            accPettyCashBillDetail.AccPettyCashBillHeaderID = accPettyCashBillHeader.AccPettyCashBillHeaderID;
                            accPettyCashBillDetail.AccPettyCashBillDetailID = tempAccPettyCashBillDetail.AccPettyCashBillDetailID;
                            accPettyCashBillDetail.CreatedUser = accPettyCashBillHeader.CreatedUser;
                            accPettyCashBillDetail.CreatedDate = accPettyCashBillHeader.CreatedDate;
                            accPettyCashBillDetail.ModifiedUser = accPettyCashBillHeader.ModifiedUser;
                            accPettyCashBillDetail.ModifiedDate = accPettyCashBillHeader.ModifiedDate;
                            accPettyCashBillDetail.LedgerID = tempAccPettyCashBillDetail.LedgerID;
                            accPettyCashBillDetail.Amount = tempAccPettyCashBillDetail.Amount;
                            accPettyCashBillDetail.LedgerCode = tempAccPettyCashBillDetail.LedgerCode;
                            accPettyCashBillDetail.LedgerName = tempAccPettyCashBillDetail.LedgerName;
                            accPettyCashBillDetail.AccPettyCashBillHeader = tempAccPettyCashBillDetail.AccPettyCashBillHeader;

                            tempAccPettyCashBillDetailList.Add(accPettyCashBillDetail);
                        }
                        accPettyCashBillDetails = null;
                        accPettyCashBillDetails = tempAccPettyCashBillDetailList.ToList();

                        accPettyCashBillHeader.AccPettyCashBillDetails = accPettyCashBillDetails.ToList();

                        if (accPettyCashBillHeader.AccPettyCashBillHeaderID.Equals(0))
                        {
                            context.AccPettyCashBillHeaders.Add(accPettyCashBillHeader);
                            context.SaveChanges();
                        }
                        else
                        {
                            //context.Entry(accPettyCashBillHeader).State = EntityState.Modified;
                            //accPettyCashBillHeader.AccPettyCashBillDetails.ToList().ForEach(r => context.Entry(r).State = EntityState.Modified);
                            //context.SaveChanges();
                            var existingAccPettyCashBillHeader = context.AccPettyCashBillHeaders.AsNoTracking().Include(s => s.AccPettyCashBillDetails)
                                                                       .Where(s => s.AccPettyCashBillHeaderID == accPettyCashBillHeader.AccPettyCashBillHeaderID)
                                                                       .FirstOrDefault<AccPettyCashBillHeader>();

                            if (existingAccPettyCashBillHeader != null)
                            {
                                var existingAccPettyCashBillDetails = existingAccPettyCashBillHeader.AccPettyCashBillDetails.ToList<AccPettyCashBillDetail>();

                                var updatedAccPettyCashBillDetails = accPettyCashBillHeader.AccPettyCashBillDetails.ToList<AccPettyCashBillDetail>();

                                var addedAccPettyCashBillDetails = CommonService.Except(updatedAccPettyCashBillDetails, existingAccPettyCashBillDetails, billDeta => billDeta.AccPettyCashBillDetailID);

                                var deletedAccPettyCashBillDetails = CommonService.Except(existingAccPettyCashBillDetails, updatedAccPettyCashBillDetails, billDeta => billDeta.AccPettyCashBillDetailID);

                                var modifiedAccPettyCashBillDetails = CommonService.Except(updatedAccPettyCashBillDetails, addedAccPettyCashBillDetails, billDeta => billDeta.AccPettyCashBillDetailID);

                                addedAccPettyCashBillDetails.ToList<AccPettyCashBillDetail>().ForEach(billDeta => context.Entry(billDeta).State = EntityState.Added);

                                deletedAccPettyCashBillDetails.ToList<AccPettyCashBillDetail>().ForEach(billDeta => context.Entry(billDeta).State = EntityState.Deleted);

                                foreach (AccPettyCashBillDetail tempAccPettyCashBillDetail in modifiedAccPettyCashBillDetails)
                                {
                                    var existingAccPettyCashBillDetail = context.AccPettyCashBillDetails.Find(tempAccPettyCashBillDetail.AccPettyCashBillDetailID);

                                    if (existingAccPettyCashBillDetail != null)
                                    {
                                        var accPettyCashBillDetailEntry = context.Entry(existingAccPettyCashBillDetail);
                                        accPettyCashBillDetailEntry.CurrentValues.SetValues(tempAccPettyCashBillDetail);
                                    }
                                }

                                var existingAccPettyCashBillHeaders = context.AccPettyCashBillHeaders.Find(accPettyCashBillHeader.AccPettyCashBillHeaderID);

                                if (existingAccPettyCashBillHeaders != null)
                                {
                                    var accPettyCashBillHeaderEntry = context.Entry(existingAccPettyCashBillHeaders);
                                    accPettyCashBillHeaderEntry.CurrentValues.SetValues(accPettyCashBillHeader);
                                }
                                context.SaveChanges();
                            }
                        }  

                        if (accPettyCashBillHeader.DocumentStatus.Equals(1))
                        {
                            decimal balanceAmount = 0;

                            #region Update Status of IOU Details 
                            AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();

                            //Status - ok, return amount
                            accPettyCashIOUService.UpdateBillSettledPettyCashIOU(accPettyCashBillHeader);

                            #endregion

                            #region Add Voucher Details

                            AddPettyCashVoucher(accPettyCashBillHeader, accPettyCashBillDetails, out balanceAmount);

                            #endregion

                            #region Update Imprest Details

                            UpdatePettyCashImprest(accPettyCashBillHeader, balanceAmount);

                            #endregion

                            #region Insert GL Entries

                            //check about updatations
                            #region Account Entries
                            AddAccGlTransactionDetails(accPettyCashBillHeader, accPettyCashBillDetails, isUpload);
                            #endregion

                            #endregion
                        }

                        
                        DateTime t1 = DateTime.UtcNow;

                        transactionScope.Complete();
                        string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                        return true;
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

        public void AddPettyCashVoucher(AccPettyCashBillHeader accPettyCashBillHeader, List<AccPettyCashBillDetail> accPettyCashBillDetails, out decimal availableBalanceAmount)
        {
            AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
            CompanyService companyService = new CompanyService();
            AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
            AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
            AccPettyCashVoucherHeader accPettyCashVoucherHeader = new AccPettyCashVoucherHeader();

            decimal balanceAmount = 0;
            using (ERPDbContext context = new ERPDbContext())
            {
                accPettyCashVoucherHeader = accPettyCashVoucherService.GetAccPettyCashVoucherHeader(accPettyCashBillHeader.DocumentNo, accPettyCashBillHeader.DocumentID, accPettyCashBillHeader.LocationID);
                if (accPettyCashVoucherHeader == null)
                { accPettyCashVoucherHeader = new AccPettyCashVoucherHeader(); }

                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(accPettyCashBillHeader.ReferenceDocumentDocumentID, accPettyCashBillHeader.ReferenceDocumentID, accPettyCashBillHeader.ReferenceLocationID);

                availableBalanceAmount = 0;
                if (accPettyCashIOUHeader != null)
                {
                    if (accPettyCashIOUHeader.Amount == accPettyCashBillHeader.Amount)
                    {
                        balanceAmount = 0;
                        availableBalanceAmount = 0;
                    }
                    else if (accPettyCashIOUHeader.Amount > accPettyCashBillHeader.Amount)
                    {
                        //return 
                        balanceAmount = 0;
                        availableBalanceAmount = (accPettyCashIOUHeader.Amount - accPettyCashBillHeader.Amount);
                    }
                    else if (accPettyCashIOUHeader.Amount < accPettyCashBillHeader.Amount)
                    {
                        //generate voucher to cash out
                        balanceAmount = (accPettyCashBillHeader.Amount - accPettyCashIOUHeader.Amount);
                        availableBalanceAmount = (accPettyCashBillHeader.Amount - accPettyCashIOUHeader.Amount);
                    }
                    accPettyCashVoucherHeader.IOUAmount = accPettyCashIOUHeader.Amount;
                }
                else
                {
                    balanceAmount = accPettyCashBillHeader.Amount;
                    availableBalanceAmount = accPettyCashBillHeader.Amount;
                    accPettyCashVoucherHeader.IOUAmount = 0;
                }

                accPettyCashVoucherHeader.ReferenceDocumentID = accPettyCashBillHeader.AccPettyCashBillHeaderID;
                accPettyCashVoucherHeader.ReferenceDocumentDocumentID = accPettyCashBillHeader.DocumentID;
                accPettyCashVoucherHeader.ReferenceLocationID = accPettyCashBillHeader.LocationID;
                accPettyCashVoucherHeader.CompanyID = accPettyCashBillHeader.CompanyID;
                accPettyCashVoucherHeader.LocationID = accPettyCashBillHeader.LocationID;
                accPettyCashVoucherHeader.CostCentreID = accPettyCashBillHeader.CostCentreID; //companyService.GetCompaniesByID(accPettyCashBillHeader.CompanyID).CostCentreID;
                accPettyCashVoucherHeader.DocumentID = accPettyCashBillHeader.DocumentID;
                accPettyCashVoucherHeader.DocumentNo = accPettyCashBillHeader.DocumentNo;
                accPettyCashVoucherHeader.LedgerSerialNo = accPettyCashBillHeader.LedgerSerialNo;
                accPettyCashVoucherHeader.PettyCashLedgerID = accPettyCashBillHeader.PettyCashLedgerID;
                accPettyCashVoucherHeader.EmployeeID = accPettyCashBillHeader.EmployeeID;
                accPettyCashVoucherHeader.PayeeName = accPettyCashBillHeader.PayeeName;
                accPettyCashVoucherHeader.DocumentDate = accPettyCashBillHeader.DocumentDate;
                accPettyCashVoucherHeader.PaymentDate = accPettyCashBillHeader.DocumentDate;
                accPettyCashVoucherHeader.Amount = accPettyCashBillHeader.Amount;
                accPettyCashVoucherHeader.BalanceAmount = balanceAmount;
                accPettyCashVoucherHeader.Reference = accPettyCashBillHeader.Reference;
                accPettyCashVoucherHeader.Remark = accPettyCashBillHeader.Remark;
                accPettyCashVoucherHeader.DocumentStatus = accPettyCashBillHeader.DocumentStatus;
                accPettyCashVoucherHeader.IsUpLoad = accPettyCashBillHeader.IsUpLoad;
                accPettyCashVoucherHeader.CreatedUser = accPettyCashBillHeader.CreatedUser;
                accPettyCashVoucherHeader.VoucherStatus = 0;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                //context.Set<AccPettyCashVoucherDetail>().Delete(context.AccPettyCashVoucherDetails.Where(p => p.AccPettyCashVoucherDetailID.Equals(accPettyCashBillHeader.AccPettyCashBillHeaderID) && p.LocationID.Equals(accPettyCashBillHeader.LocationID) && p.DocumentID.Equals(accPettyCashBillHeader.DocumentID)).AsQueryable());
                //context.SaveChanges();

                List<AccPettyCashVoucherDetail> accPettyCashVoucherDetailList = new List<AccPettyCashVoucherDetail>();

                AccPettyCashVoucherDetail accPettyCashVoucherDetailTemp;
                accPettyCashVoucherDetailList.Clear();
                foreach (var tempPettyCashVoucher in accPettyCashBillDetails)
                {
                    accPettyCashVoucherDetailTemp = new AccPettyCashVoucherDetail();

                    accPettyCashVoucherDetailTemp.CompanyID = accPettyCashVoucherHeader.CompanyID;
                    accPettyCashVoucherDetailTemp.LocationID = accPettyCashVoucherHeader.LocationID;
                    accPettyCashVoucherDetailTemp.CostCentreID = accPettyCashVoucherHeader.CostCentreID;
                    accPettyCashVoucherDetailTemp.DocumentNo = accPettyCashVoucherHeader.DocumentNo;
                    accPettyCashVoucherDetailTemp.DocumentID = tempPettyCashVoucher.DocumentID;
                    accPettyCashVoucherDetailTemp.DocumentDate = accPettyCashVoucherHeader.DocumentDate;
                    accPettyCashVoucherDetailTemp.PaymentDate = accPettyCashVoucherHeader.DocumentDate;
                    accPettyCashVoucherDetailTemp.LedgerID = tempPettyCashVoucher.LedgerID;
                    accPettyCashVoucherDetailTemp.Amount = tempPettyCashVoucher.Amount;
                    accPettyCashVoucherDetailTemp.ChequeDate = accPettyCashVoucherHeader.DocumentDate;
                    accPettyCashVoucherDetailTemp.DocumentStatus = tempPettyCashVoucher.DocumentStatus;
                    accPettyCashVoucherDetailTemp.IsUpLoad = tempPettyCashVoucher.IsUpLoad;
                    accPettyCashVoucherDetailTemp.AccPettyCashVoucherHeaderID = accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID;
                    accPettyCashVoucherDetailTemp.CreatedUser = accPettyCashVoucherHeader.CreatedUser;
                    accPettyCashVoucherDetailList.Add(accPettyCashVoucherDetailTemp);
                }

                accPettyCashVoucherHeader.AccPettyCashVoucherDetails = accPettyCashVoucherDetailList.ToList();

                if (accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID.Equals(0))
                {
                    accPettyCashVoucherService.AddAccPettyCashVoucherHeader(accPettyCashVoucherHeader);
                }
                else
                {
                    accPettyCashVoucherService.UpdateAccPettyCashVoucherHeader(accPettyCashVoucherHeader);
                }
            }
        }

        public void AddAccGlTransactionDetails(AccPettyCashBillHeader accPettyCashBillHeader, List<AccPettyCashBillDetail> accPettyCashBillDetails, bool isTStatus)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
                AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
                CompanyService companyService = new CompanyService();
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
                List<AccGlTransactionDetail> accGlTransactionDetailTempList = new List<AccGlTransactionDetail>();

                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(accPettyCashBillHeader.ReferenceDocumentDocumentID, accPettyCashBillHeader.ReferenceDocumentID, accPettyCashBillHeader.ReferenceLocationID);

                if (accPettyCashIOUHeader != null)
                {
                    //Have to read IOU A/C
                    accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID);

                    int DrCr = 0;

                    if (accTransactionTypeDetail != null)
                    {
                        if (accPettyCashIOUHeader.Amount == accPettyCashBillHeader.Amount || accPettyCashIOUHeader.Amount > accPettyCashBillHeader.Amount)
                        {
                            if (accTransactionTypeDetail.DrCr != null)
                            { DrCr = ((accTransactionTypeDetail.DrCr == 1) ? 2 : 1); }

                            accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(accPettyCashBillHeader.AccPettyCashBillHeaderID, accPettyCashBillHeader.DocumentID, accPettyCashBillHeader.LocationID);
                            if (accGlTransactionHeader == null)
                            { accGlTransactionHeader = new AccGlTransactionHeader(); }

                            accGlTransactionHeader.CompanyID = accPettyCashBillHeader.CompanyID;
                            accGlTransactionHeader.CostCentreID = accPettyCashBillHeader.CostCentreID; //companyService.GetCompaniesByID(accPettyCashBillHeader.CompanyID).CostCentreID;
                            accGlTransactionHeader.CreatedDate = accPettyCashBillHeader.CreatedDate;
                            accGlTransactionHeader.CreatedUser = accPettyCashBillHeader.CreatedUser;
                            accGlTransactionHeader.DocumentDate = accPettyCashBillHeader.DocumentDate;
                            accGlTransactionHeader.DocumentID = accPettyCashBillHeader.DocumentID;
                            accGlTransactionHeader.DocumentStatus = accPettyCashBillHeader.DocumentStatus;
                            accGlTransactionHeader.DocumentNo = accPettyCashBillHeader.DocumentNo;
                            accGlTransactionHeader.GroupOfCompanyID = accPettyCashBillHeader.GroupOfCompanyID;
                            accGlTransactionHeader.DataTransfer = accPettyCashBillHeader.DataTransfer;
                            accGlTransactionHeader.IsUpLoad = false;
                            accGlTransactionHeader.LocationID = accPettyCashBillHeader.LocationID;
                            accGlTransactionHeader.ModifiedDate = accPettyCashBillHeader.ModifiedDate;
                            accGlTransactionHeader.ModifiedUser = accPettyCashBillHeader.ModifiedUser;
                            accGlTransactionHeader.ReferenceDocumentDocumentID = accPettyCashBillHeader.DocumentID;
                            accGlTransactionHeader.ReferenceDocumentID = accPettyCashBillHeader.AccPettyCashBillHeaderID;
                            accGlTransactionHeader.ReferenceDocumentNo = accPettyCashBillHeader.DocumentNo;
                            accGlTransactionHeader.ReferenceID = accPettyCashBillHeader.EmployeeID;
                            accGlTransactionHeader.Amount = accPettyCashBillHeader.Amount;

                            context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(accGlTransactionHeader.AccGlTransactionHeaderID) && p.LocationID.Equals(accGlTransactionHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(accGlTransactionHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());

                            var te = new AccGlTransactionDetail
                            {
                                AccGlTransactionDetailID = 0,
                                AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                CompanyID = accGlTransactionHeader.CompanyID,
                                LocationID = accGlTransactionHeader.LocationID,
                                CostCentreID = accGlTransactionHeader.CostCentreID,
                                DocumentNo = accGlTransactionHeader.DocumentNo,
                                DocumentID = accGlTransactionHeader.DocumentID,
                                LedgerSerial = accPettyCashBillHeader.LedgerSerialNo,
                                DocumentDate = accGlTransactionHeader.DocumentDate,
                                PaymentDate = accPettyCashBillHeader.DocumentDate,
                                ReferenceID = accGlTransactionHeader.ReferenceID,
                                LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                DrCr = DrCr,
                                Amount = accGlTransactionHeader.Amount,
                                TransactionTypeId = accGlTransactionHeader.DocumentID,
                                ReferenceDocumentID = accPettyCashBillHeader.ReferenceDocumentID,
                                ReferenceDocumentDocumentID = accPettyCashBillHeader.ReferenceDocumentDocumentID,
                                ReferenceLocationID = accPettyCashBillHeader.ReferenceLocationID,
                                ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                TransactionDefinitionId = 1,
                                ChequeNo = string.Empty,
                                Reference = accPettyCashBillHeader.Reference,
                                Remark = accPettyCashBillHeader.Remark,
                                DocumentStatus = accGlTransactionHeader.DocumentStatus,
                                IsUpLoad = isTStatus,
                                GroupOfCompanyID = accGlTransactionHeader.GroupOfCompanyID,
                                CreatedUser = accGlTransactionHeader.CreatedUser,
                                CreatedDate = accGlTransactionHeader.CreatedDate,
                                ModifiedUser = accGlTransactionHeader.ModifiedUser,
                                ModifiedDate = accGlTransactionHeader.ModifiedDate,
                                DataTransfer = accGlTransactionHeader.DataTransfer,
                            };
                            accGlTransactionDetailTempList.Add(te);

                            foreach (AccPettyCashBillDetail accPettyCashBillDetail in accPettyCashBillDetails)
                            {
                                var te1 = new AccGlTransactionDetail
                                {
                                    AccGlTransactionDetailID = 0,
                                    AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                    CompanyID = accGlTransactionHeader.CompanyID,
                                    LocationID = accGlTransactionHeader.LocationID,
                                    CostCentreID = accGlTransactionHeader.CostCentreID,
                                    DocumentNo = accGlTransactionHeader.DocumentNo,
                                    DocumentID = accGlTransactionHeader.DocumentID,
                                    LedgerID = accPettyCashBillDetail.LedgerID,
                                    LedgerSerial = accPettyCashBillHeader.LedgerSerialNo,
                                    DocumentDate = accGlTransactionHeader.DocumentDate,
                                    PaymentDate = accGlTransactionHeader.DocumentDate,
                                    ReferenceID = accGlTransactionHeader.ReferenceID,
                                    DrCr = accTransactionTypeDetail.DrCr,
                                    Amount = accPettyCashBillDetail.Amount,
                                    TransactionTypeId = accGlTransactionHeader.DocumentID,
                                    ReferenceDocumentID = accPettyCashBillHeader.ReferenceDocumentID,
                                    ReferenceDocumentDocumentID = accPettyCashBillHeader.ReferenceDocumentDocumentID,
                                    ReferenceLocationID = accPettyCashBillHeader.ReferenceLocationID,
                                    ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                    TransactionDefinitionId = 1,
                                    ChequeNo = string.Empty,
                                    Reference = accPettyCashBillHeader.Reference,
                                    Remark = accPettyCashBillHeader.Remark,
                                    DocumentStatus = accPettyCashBillHeader.DocumentStatus,
                                    IsUpLoad = isTStatus,
                                    GroupOfCompanyID = accPettyCashBillHeader.GroupOfCompanyID,
                                    CreatedUser = accPettyCashBillHeader.CreatedUser,
                                    CreatedDate = accPettyCashBillHeader.CreatedDate,
                                    ModifiedUser = accPettyCashBillHeader.ModifiedUser,
                                    ModifiedDate = accPettyCashBillHeader.ModifiedDate,
                                    DataTransfer = accPettyCashBillHeader.DataTransfer,
                                };
                                accGlTransactionDetailTempList.Add(te1);
                            }

                            if (accPettyCashIOUHeader.Amount > accPettyCashBillHeader.Amount)
                            {
                                if (accPettyCashBillHeader.ReturnedAmount > 0)
                                {
                                    var te1 = new AccGlTransactionDetail
                                    {
                                        AccGlTransactionDetailID = 0,
                                        AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                        CompanyID = accGlTransactionHeader.CompanyID,
                                        LocationID = accGlTransactionHeader.LocationID,
                                        CostCentreID = accGlTransactionHeader.CostCentreID,
                                        DocumentNo = accGlTransactionHeader.DocumentNo,
                                        DocumentID = accGlTransactionHeader.DocumentID,
                                        LedgerID = accPettyCashBillHeader.PettyCashLedgerID,
                                        LedgerSerial = accPettyCashBillHeader.LedgerSerialNo,
                                        DocumentDate = accGlTransactionHeader.DocumentDate,
                                        PaymentDate = accGlTransactionHeader.DocumentDate,
                                        ReferenceID = accGlTransactionHeader.ReferenceID,
                                        DrCr = accTransactionTypeDetail.DrCr,
                                        Amount = accPettyCashBillHeader.ReturnedAmount,
                                        TransactionTypeId = accGlTransactionHeader.DocumentID,
                                        ReferenceDocumentID = accPettyCashBillHeader.ReferenceDocumentID,
                                        ReferenceDocumentDocumentID = accPettyCashBillHeader.ReferenceDocumentDocumentID,
                                        ReferenceLocationID = accPettyCashBillHeader.ReferenceLocationID,
                                        ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                        TransactionDefinitionId = 1,
                                        ChequeNo = string.Empty,
                                        Reference = accPettyCashBillHeader.Reference,
                                        Remark = accPettyCashBillHeader.Remark,
                                        DocumentStatus = accPettyCashBillHeader.DocumentStatus,
                                        IsUpLoad = isTStatus,
                                        GroupOfCompanyID = accPettyCashBillHeader.GroupOfCompanyID,
                                        CreatedUser = accPettyCashBillHeader.CreatedUser,
                                        CreatedDate = accPettyCashBillHeader.CreatedDate,
                                        ModifiedUser = accPettyCashBillHeader.ModifiedUser,
                                        ModifiedDate = accPettyCashBillHeader.ModifiedDate,
                                        DataTransfer = accPettyCashBillHeader.DataTransfer,
                                    };
                                    accGlTransactionDetailTempList.Add(te1);
                                }
                            }

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
                            context.SaveChanges();
                        }
                    }
                //    else if (accPettyCashIOUHeader.Amount < accPettyCashBillHeader.Amount)
                //    {
                //        //return 
                //        availableBalanceAmount = (accPettyCashBillHeader.Amount - accPettyCashIOUHeader.Amount);
                //    }
                //    accPettyCashVoucherHeader.IOUAmount = accPettyCashIOUHeader.Amount;
                }
                //else
                //{
                //    balanceAmount = accPettyCashBillHeader.Amount;
                //    availableBalanceAmount = accPettyCashBillHeader.Amount;
                //    accPettyCashVoucherHeader.IOUAmount = 0;
                //}

                
                
            }
        }

        public void UpdatePettyCashImprest(AccPettyCashBillHeader accPettyCashBillHeader, decimal balanceAmount)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashBillHeader.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashBillHeader.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashBillHeader.PettyCashLedgerID;
            accPettyCashImprestDetail.IssuedAmount = (accPettyCashBillHeader.Amount - (accPettyCashBillHeader.ToSettleAmount + accPettyCashBillHeader.IOUAmount) + accPettyCashBillHeader.ReturnedAmount);
            accPettyCashImprestDetail.UsedAmount = ((accPettyCashBillHeader.Amount - accPettyCashBillHeader.IOUAmount) + accPettyCashBillHeader.ReturnedAmount);

            accPettyCashImprestService.UpdatePettyCashImprest(accPettyCashImprestDetail);
        }
        
        /// <summary>
        /// Get Saved AccPettyCashBillHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashBillHeader GetAccPettyCashBillHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashBillHeaders.Include("AccPettyCashBillDetails").Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashBillHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public List<AccPettyCashBillDetail> GetAllPettyCashBillDetail(AccPettyCashBillHeader accPettyCashBillHeader)
        {
            List<AccPettyCashBillDetail> accPettyCashBillDetailList = new List<AccPettyCashBillDetail>();

            AccPettyCashBillDetail accPettyCashBillDetailTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashBillDetails = (from id in context.AccPettyCashBillDetails
                                              join ih in context.AccPettyCashBillHeaders on id.AccPettyCashBillHeaderID equals ih.AccPettyCashBillHeaderID
                                              join ld in context.AccLedgerAccounts on id.LedgerID equals ld.AccLedgerAccountID
                                               where ih.LocationID.Equals(accPettyCashBillHeader.LocationID) && ih.CompanyID.Equals(accPettyCashBillHeader.CompanyID) &&
                                              ih.DocumentID.Equals(accPettyCashBillHeader.DocumentID) && ih.AccPettyCashBillHeaderID.Equals(accPettyCashBillHeader.AccPettyCashBillHeaderID)
                                               select new // AccPettyCashBillDetail
                                              {
                                                  AccPettyCashBillDetailID = id.AccPettyCashBillDetailID,
                                                  AccPettyCashBillHeaderID = id.AccPettyCashBillHeaderID,
                                                  CompanyID = id.CompanyID,
                                                  LocationID = id.LocationID,
                                                  DocumentID = id.DocumentID,
                                                  LedgerID = id.LedgerID,
                                                  LedgerCode = ld.LedgerCode,
                                                  LedgerName = ld.LedgerName,
                                                  Amount = id.Amount,
                                                  DocumentStatus = id.DocumentStatus,
                                                  IsTStatus = id.IsUpLoad,
                                                  AccPettyCashBillHeader = id.AccPettyCashBillHeader,
                                                  CreatedDate = id.CreatedDate,
                                                  CreatedUser = id.CreatedUser,
                                                  ModifiedDate = id.ModifiedDate,
                                                  ModifiedUser = id.ModifiedUser,
                                                  GroupOfCompanyID = id.GroupOfCompanyID,
                                                  DataTransfer = id.DataTransfer
                                              }).ToList();

                foreach (var tempGiftVoucher in accPettyCashBillDetails)
                {
                    accPettyCashBillDetailTemp = new AccPettyCashBillDetail();
                    accPettyCashBillDetailTemp.AccPettyCashBillDetailID = tempGiftVoucher.AccPettyCashBillDetailID;
                    accPettyCashBillDetailTemp.AccPettyCashBillHeaderID = tempGiftVoucher.AccPettyCashBillHeaderID;
                    accPettyCashBillDetailTemp.CompanyID = tempGiftVoucher.CompanyID;
                    accPettyCashBillDetailTemp.LocationID = tempGiftVoucher.LocationID;
                    accPettyCashBillDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                    accPettyCashBillDetailTemp.LedgerID = tempGiftVoucher.LedgerID;
                    accPettyCashBillDetailTemp.LedgerCode = tempGiftVoucher.LedgerCode;
                    accPettyCashBillDetailTemp.LedgerName = tempGiftVoucher.LedgerName;
                    accPettyCashBillDetailTemp.Amount = tempGiftVoucher.Amount;
                    accPettyCashBillDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                    accPettyCashBillDetailTemp.IsUpLoad = tempGiftVoucher.IsTStatus;
                    accPettyCashBillDetailTemp.AccPettyCashBillHeader = tempGiftVoucher.AccPettyCashBillHeader;
                    accPettyCashBillDetailTemp.CreatedDate = tempGiftVoucher.CreatedDate;
                    accPettyCashBillDetailTemp.CreatedUser = tempGiftVoucher.CreatedUser;
                    accPettyCashBillDetailTemp.ModifiedDate = tempGiftVoucher.ModifiedDate;
                    accPettyCashBillDetailTemp.ModifiedUser = tempGiftVoucher.ModifiedUser;
                    accPettyCashBillDetailTemp.GroupOfCompanyID = tempGiftVoucher.GroupOfCompanyID;
                    accPettyCashBillDetailTemp.DataTransfer = tempGiftVoucher.DataTransfer;
                    accPettyCashBillDetailList.Add(accPettyCashBillDetailTemp);
                }
            }

            return accPettyCashBillDetailList;
        }

        public DataTable GetPendingPettyCashBillDocuments()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from poh in context.AccPettyCashBillHeaders
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

        public DataTable GetPettyCashBillTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var query = (from ph in context.AccPettyCashBillHeaders
                             join pd in context.AccPettyCashBillDetails on ph.AccPettyCashBillHeaderID equals pd.AccPettyCashBillHeaderID
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

        public DataTable GetPettyCashBillsDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                //StringBuilder selectFields = new StringBuilder();
                var query = context.AccPettyCashBillHeaders.AsNoTracking().Where("DocumentStatus = @0", 1);

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
                IQueryable qryResult = context.AccPettyCashBillHeaders.Where("DocumentStatus == @0", 1);

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
        #endregion
    }
}
