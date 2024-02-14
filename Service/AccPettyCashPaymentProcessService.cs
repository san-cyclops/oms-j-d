using System;
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

namespace Service
{
    public class AccPettyCashPaymentProcessService
    {
        #region Methods

        /// <summary>
        /// Save PettyCashPayment
        /// </summary>
        /// <param name="accPettyCashPaymentProcessHeader"></param>
        /// <param name="accPettyCashPaymentProcessDetails"></param>
        public bool SavePettyCashPaymentProcess(AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader, List<AccPettyCashPaymentProcessDetail> accPettyCashPaymentProcessDetails, out string newDocumentNo, string formName = "", bool isUpload = false)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    using (ERPDbContext context = new ERPDbContext())
                    {
                        if (accPettyCashPaymentProcessHeader.DocumentStatus.Equals(1))
                        {
                            AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                            LocationService locationService = new LocationService();
                            AccLedgerAccountService accLedgerAccountService = new AccLedgerAccountService();
                            accPettyCashPaymentProcessHeader.DocumentNo = accPettyCashPaymentProcessService.GetDocumentNo(formName, accPettyCashPaymentProcessHeader.LocationID, locationService.GetLocationsByID(accPettyCashPaymentProcessHeader.LocationID).LocationCode, accPettyCashPaymentProcessHeader.DocumentID, false).Trim();
                            //accPettyCashPaymentProcessHeader.LedgerSerialNo = accPettyCashPaymentProcessService.GetLedgerSerialNo(formName, accPettyCashPaymentProcessHeader.LocationID, locationService.GetLocationsByID(accPettyCashPaymentProcessHeader.LocationID).LocationCode, accPettyCashPaymentProcessHeader.PettyCashLedgerID, accLedgerAccountService.GetAccLedgerAccountByID(accPettyCashPaymentProcessHeader.PettyCashLedgerID).LedgerCode, accPettyCashPaymentProcessHeader.DocumentID, false).Trim();
                        }

                        newDocumentNo = accPettyCashPaymentProcessHeader.DocumentNo;

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        context.Set<AccPettyCashPaymentProcessDetail>().Delete(context.AccPettyCashPaymentProcessDetails.Where(p => p.AccPettyCashPaymentProcessHeaderID.Equals(accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID) && p.LocationID.Equals(accPettyCashPaymentProcessHeader.LocationID) && p.DocumentID.Equals(accPettyCashPaymentProcessHeader.DocumentID)).AsQueryable());
                        context.SaveChanges();

                        accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessDetails = accPettyCashPaymentProcessDetails.ToList();

                        if (accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID.Equals(0))
                        {context.AccPettyCashPaymentProcessHeaders.Add(accPettyCashPaymentProcessHeader);}
                        else
                        {
                            accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessDetails.ToList().ForEach(r => context.Entry(r).State = EntityState.Modified);
                            //context.Entry(accPettyCashPaymentProcessHeader).State = EntityState.Modified;
                        }
                        context.SaveChanges();
                        if (accPettyCashPaymentProcessHeader.DocumentStatus.Equals(1))
                        {
                            #region Update Payment Status
                            AccPettyCashPaymentProcessService accPettyCashPaymentProcessService = new AccPettyCashPaymentProcessService();
                            accPettyCashPaymentProcessService.UpdateProcessedAccPettyCashPaymentHeader(accPettyCashPaymentProcessDetails);
                            #endregion

                            //#region Update Imprest Details
                            //UpdatePettyCashImprest(accPettyCashPaymentProcessHeader);
                            //#endregion

                            #region Account Entries
                            AddAccGlTransactionDetails(accPettyCashPaymentProcessHeader, accPettyCashPaymentProcessDetails, isUpload);
                            #endregion
                        }
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

        public void AddAccGlTransactionDetails(AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader, List<AccPettyCashPaymentProcessDetail> accPettyCashPaymentProcessDetails, bool isUpload)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
                AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();
                AccPettyCashIOUService accPettyCashIOUService = new AccPettyCashIOUService();
                AccPettyCashIOUHeader accPettyCashIOUHeader = new AccPettyCashIOUHeader();
                AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
                List<AccGlTransactionDetail> accGlTransactionDetailTempList = new List<AccGlTransactionDetail>();
                List<AccPettyCashBillDetail> accPettyCashBillDetailList = new List<AccPettyCashBillDetail>();
                List<AccPettyCashBillHeader> accPettyCashBillHeaderList = new List<AccPettyCashBillHeader>();

                AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

                accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID, accPettyCashPaymentProcessHeader.DocumentID, accPettyCashPaymentProcessHeader.LocationID);
                if (accGlTransactionHeader == null)
                { accGlTransactionHeader = new AccGlTransactionHeader(); }

                #region Fill Header
                accGlTransactionHeader.CompanyID = accPettyCashPaymentProcessHeader.CompanyID;
                accGlTransactionHeader.LocationID = accPettyCashPaymentProcessHeader.LocationID;
                accGlTransactionHeader.CostCentreID = accPettyCashPaymentProcessHeader.CostCentreID;
                accGlTransactionHeader.DocumentDate = accPettyCashPaymentProcessHeader.DocumentDate;
                accGlTransactionHeader.DocumentID = accPettyCashPaymentProcessHeader.DocumentID;
                accGlTransactionHeader.DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus;
                accGlTransactionHeader.DocumentNo = accPettyCashPaymentProcessHeader.DocumentNo;
                accGlTransactionHeader.Amount = accPettyCashPaymentProcessHeader.Amount;
                //accGlTransactionHeader.ReferenceDocumentDocumentID = accPettyCashPaymentProcessHeader.DocumentID;
                //accGlTransactionHeader.ReferenceDocumentID = accPettyCashPaymentProcessHeader.AccPettyCashPaymentProcessHeaderID;
                //accGlTransactionHeader.ReferenceDocumentNo = accPettyCashPaymentProcessHeader.DocumentNo;
                accGlTransactionHeader.GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID;
                accGlTransactionHeader.DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer;
                accGlTransactionHeader.IsUpLoad = false;
                accGlTransactionHeader.CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate;
                accGlTransactionHeader.CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser;
                accGlTransactionHeader.ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate;
                accGlTransactionHeader.ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser;

                //accGlTransactionHeader.ReferenceID = string.Empty; //accPettyCashPaymentProcessHeader.EmployeeID;

                #endregion

                accPettyCashIOUHeader = accPettyCashIOUService.GetAccPettyCashIOUHeaderByDocumentNo(accPettyCashPaymentProcessHeader.ReferenceDocumentDocumentID, accPettyCashPaymentProcessHeader.ReferenceDocumentID, accPettyCashPaymentProcessHeader.ReferenceLocationID);

                if (accPettyCashIOUHeader != null)
                {
                    //Have to read IOU A/C
                    accTransactionTypeDetail =
                        accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(
                            AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmPettyCashIOU").DocumentID);

                    int DrCr = 0;

                    if (accTransactionTypeDetail != null)
                    {
                        if (accTransactionTypeDetail.DrCr != null)
                        {
                            if (accTransactionTypeDetail.DrCr == 1) DrCr = 2;
                            else DrCr = 1;
                        }

                        //context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(accGlTransactionHeader.AccGlTransactionHeaderID) && p.LocationID.Equals(accGlTransactionHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(accGlTransactionHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());


                        foreach (
                            AccPettyCashPaymentProcessDetail accPettyCashPaymentProcessDetail in
                                accPettyCashPaymentProcessDetails)
                        {
                            #region IOU Bill Entry

                            accPettyCashBillDetailList = (from bd in context.AccPettyCashBillDetails
                                                          join bh in context.AccPettyCashBillHeaders on
                                                              bd.AccPettyCashBillHeaderID equals
                                                              bh.AccPettyCashBillHeaderID
                                                          join ih in context.AccPettyCashVoucherHeaders on
                                                              bh.AccPettyCashBillHeaderID equals ih.ReferenceDocumentID
                                                          join ph in context.AccPettyCashPaymentDetails on
                                                              ih.AccPettyCashVoucherHeaderID equals
                                                              ph.ReferenceDocumentID
                                                          where
                                                              ph.LocationID.Equals(
                                                                  accPettyCashPaymentProcessDetail.ReferenceLocationID) &&
                                                              ph.DocumentID.Equals(
                                                                  accPettyCashPaymentProcessDetail
                                                                      .ReferenceDocumentDocumentID) &&
                                                              ph.AccPettyCashPaymentHeaderID.Equals(
                                                                  accPettyCashPaymentProcessDetail.ReferenceDocumentID)
                                                          select bd).ToList();

                            if (accPettyCashBillDetailList.Count() > 0)
                            {
                                foreach (var pettyBillDetail in accPettyCashBillDetailList)
                                {
                                    var te3 = new AccGlTransactionDetail
                                        {
                                            AccGlTransactionDetailID = 0,
                                            AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                            CompanyID = accGlTransactionHeader.CompanyID,
                                            LocationID = accGlTransactionHeader.LocationID,
                                            CostCentreID = accGlTransactionHeader.CostCentreID, //? payment cost centre
                                            DocumentNo = accGlTransactionHeader.DocumentNo,
                                            DocumentID = accGlTransactionHeader.DocumentID,
                                            LedgerID = pettyBillDetail.LedgerID,
                                            LedgerSerial = accPettyCashPaymentProcessHeader.LedgerSerialNo,
                                            DocumentDate = accGlTransactionHeader.DocumentDate,
                                            PaymentDate = accGlTransactionHeader.DocumentDate,
                                            ReferenceID = accGlTransactionHeader.ReferenceID,
                                            DrCr = accTransactionTypeDetail.DrCr,
                                            Amount = pettyBillDetail.Amount,
                                            TransactionTypeId = accGlTransactionHeader.DocumentID,
                                            ReferenceDocumentID = accPettyCashPaymentProcessDetail.ReferenceDocumentID,
                                            ReferenceDocumentDocumentID =
                                                accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID,
                                            ReferenceLocationID = accPettyCashPaymentProcessDetail.ReferenceLocationID,
                                            ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                            TransactionDefinitionId = 1,
                                            ChequeNo = string.Empty,
                                            //Reference = accPettyCashBillHeader.Reference,
                                            //Remark = accPettyCashBillHeader.Remark,
                                            DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus,
                                            IsUpLoad = isUpload,
                                            GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID,
                                            CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser,
                                            CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate,
                                            ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser,
                                            ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate,
                                            DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer,
                                        };
                                    accGlTransactionDetailTempList.Add(te3);
                                }
                            }

                            #endregion

                            accPettyCashBillHeaderList = (from bh in context.AccPettyCashBillHeaders
                                                          join ih in context.AccPettyCashVoucherHeaders on
                                                              bh.AccPettyCashBillHeaderID equals ih.ReferenceDocumentID
                                                          join ph in context.AccPettyCashPaymentDetails on
                                                              ih.AccPettyCashVoucherHeaderID equals
                                                              ph.ReferenceDocumentID
                                                          where
                                                              ph.LocationID.Equals(
                                                                  accPettyCashPaymentProcessDetail.ReferenceLocationID) &&
                                                              ph.DocumentID.Equals(
                                                                  accPettyCashPaymentProcessDetail
                                                                      .ReferenceDocumentDocumentID) &&
                                                              ph.AccPettyCashPaymentHeaderID.Equals(
                                                                  accPettyCashPaymentProcessDetail.ReferenceDocumentID) &&
                                                              ih.IOUAmount > 0 && bh.ToSettleAmount > 0 &&
                                                              bh.ReferenceDocumentID != null
                                                          select bh).ToList();

                            if (accPettyCashBillHeaderList.Count() > 0)
                            {
                                foreach (var pettyBillHeader in accPettyCashBillHeaderList)
                                {
                                    #region IOU Entry

                                    //#region Credit Entry
                                    var teIOU = new AccGlTransactionDetail
                                        {
                                            AccGlTransactionDetailID = 0,
                                            AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                            CompanyID = accGlTransactionHeader.CompanyID,
                                            LocationID = accGlTransactionHeader.LocationID,
                                            CostCentreID = accGlTransactionHeader.CostCentreID,
                                            DocumentNo = accGlTransactionHeader.DocumentNo,
                                            DocumentID = accGlTransactionHeader.DocumentID,
                                            LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                                            LedgerSerial = accPettyCashPaymentProcessHeader.LedgerSerialNo,
                                            DocumentDate = accGlTransactionHeader.DocumentDate,
                                            PaymentDate = accGlTransactionHeader.DocumentDate,
                                            ReferenceID = accGlTransactionHeader.ReferenceID,
                                            DrCr = DrCr,
                                            Amount = pettyBillHeader.IOUAmount,
                                            TransactionTypeId = accGlTransactionHeader.DocumentID,
                                            ReferenceDocumentID = accPettyCashPaymentProcessDetail.ReferenceDocumentID,
                                            ReferenceDocumentDocumentID =
                                                accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID,
                                            ReferenceLocationID = accPettyCashPaymentProcessDetail.ReferenceLocationID,
                                            ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                            TransactionDefinitionId = 1,
                                            ChequeNo = string.Empty,
                                            //Reference = accPettyCashBillHeader.Reference,
                                            //Remark = accPettyCashBillHeader.Remark,
                                            DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus,
                                            IsUpLoad = isUpload,
                                            GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID,
                                            CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser,
                                            CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate,
                                            ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser,
                                            ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate,
                                            DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer,
                                        };
                                    accGlTransactionDetailTempList.Add(teIOU);

                                    //#endregion

                                    #endregion

                                    #region Payment Entry

                                    var te1 = new AccGlTransactionDetail
                                        {
                                            AccGlTransactionDetailID = 0,
                                            AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                            CompanyID = accGlTransactionHeader.CompanyID,
                                            LocationID = accGlTransactionHeader.LocationID,
                                            CostCentreID = accGlTransactionHeader.CostCentreID,
                                            DocumentNo = accGlTransactionHeader.DocumentNo,
                                            DocumentID = accGlTransactionHeader.DocumentID,
                                            LedgerID = pettyBillHeader.PettyCashLedgerID,
                                            LedgerSerial = accPettyCashPaymentProcessHeader.LedgerSerialNo,
                                            DocumentDate = accGlTransactionHeader.DocumentDate,
                                            PaymentDate = accGlTransactionHeader.DocumentDate,
                                            ReferenceID = accGlTransactionHeader.ReferenceID,
                                            DrCr = DrCr,
                                            Amount = pettyBillHeader.ToSettleAmount,
                                            TransactionTypeId = accGlTransactionHeader.DocumentID,
                                            ReferenceDocumentID = accPettyCashPaymentProcessDetail.ReferenceDocumentID,
                                            ReferenceDocumentDocumentID =
                                                accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID,
                                            ReferenceLocationID = accPettyCashPaymentProcessDetail.ReferenceLocationID,
                                            ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                            TransactionDefinitionId = 1,
                                            ChequeNo = string.Empty,
                                            //Reference = accPettyCashBillHeader.Reference,
                                            //Remark = accPettyCashBillHeader.Remark,
                                            DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus,
                                            IsUpLoad = isUpload,
                                            GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID,
                                            CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser,
                                            CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate,
                                            ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser,
                                            ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate,
                                            DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer,
                                        };
                                    accGlTransactionDetailTempList.Add(te1);

                                    #endregion
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (AccPettyCashPaymentProcessDetail accPettyCashPaymentProcessDetail in accPettyCashPaymentProcessDetails)
                    {
                        #region Bill Entry

                        accPettyCashBillDetailList = (from bd in context.AccPettyCashBillDetails
                                                      join bh in context.AccPettyCashBillHeaders on
                                                          bd.AccPettyCashBillHeaderID equals bh.AccPettyCashBillHeaderID
                                                      join ih in context.AccPettyCashVoucherHeaders on
                                                          bh.AccPettyCashBillHeaderID equals ih.ReferenceDocumentID
                                                      join ph in context.AccPettyCashPaymentDetails on
                                                          ih.AccPettyCashVoucherHeaderID equals ph.ReferenceDocumentID
                                                      where
                                                          ph.LocationID.Equals(
                                                              accPettyCashPaymentProcessDetail.ReferenceLocationID) &&
                                                          ph.DocumentID.Equals(
                                                              accPettyCashPaymentProcessDetail
                                                                  .ReferenceDocumentDocumentID) &&
                                                          ph.AccPettyCashPaymentHeaderID.Equals(
                                                              accPettyCashPaymentProcessDetail.ReferenceDocumentID)
                                                      select bd).ToList();

                        if (accPettyCashBillDetailList.Count() > 0)
                        {
                            foreach (var pettyBillDetail in accPettyCashBillDetailList)
                            {
                                var te3 = new AccGlTransactionDetail
                                    {
                                        AccGlTransactionDetailID = 0,
                                        AccGlTransactionHeaderID = accGlTransactionHeader.AccGlTransactionHeaderID,
                                        CompanyID = accGlTransactionHeader.CompanyID,
                                        LocationID = accGlTransactionHeader.LocationID,
                                        CostCentreID = accGlTransactionHeader.CostCentreID, //? payment cost centre
                                        DocumentNo = accGlTransactionHeader.DocumentNo,
                                        DocumentID = accGlTransactionHeader.DocumentID,
                                        LedgerID = pettyBillDetail.LedgerID,
                                        LedgerSerial = accPettyCashPaymentProcessHeader.LedgerSerialNo,
                                        DocumentDate = accGlTransactionHeader.DocumentDate,
                                        PaymentDate = accGlTransactionHeader.DocumentDate,
                                        ReferenceID = accGlTransactionHeader.ReferenceID,
                                        DrCr = 2,
                                        Amount = pettyBillDetail.Amount,
                                        TransactionTypeId = accGlTransactionHeader.DocumentID,
                                        ReferenceDocumentID = accPettyCashPaymentProcessDetail.ReferenceDocumentID,
                                        ReferenceDocumentDocumentID =
                                            accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID,
                                        ReferenceLocationID = accPettyCashPaymentProcessDetail.ReferenceLocationID,
                                        ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                        TransactionDefinitionId = 1,
                                        ChequeNo = string.Empty,
                                        //Reference = accPettyCashBillHeader.Reference,
                                        //Remark = accPettyCashBillHeader.Remark,
                                        DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus,
                                        IsUpLoad = isUpload,
                                        GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID,
                                        CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser,
                                        CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate,
                                        ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser,
                                        ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate,
                                        DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer,
                                    };
                                accGlTransactionDetailTempList.Add(te3);
                            }
                        }

                        #endregion

                        #region Payment Entry

                        accPettyCashBillHeaderList = (from bh in context.AccPettyCashBillHeaders
                                                      join ih in context.AccPettyCashVoucherHeaders on
                                                          bh.AccPettyCashBillHeaderID equals ih.ReferenceDocumentID
                                                      join ph in context.AccPettyCashPaymentDetails on
                                                          ih.AccPettyCashVoucherHeaderID equals
                                                          ph.ReferenceDocumentID
                                                      where
                                                          ph.LocationID.Equals(accPettyCashPaymentProcessDetail.ReferenceLocationID) &&
                                                          ph.DocumentID.Equals(accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID) &&
                                                          ph.AccPettyCashPaymentHeaderID.Equals(accPettyCashPaymentProcessDetail.ReferenceDocumentID) &&
                                                          ih.IOUAmount > 0 && bh.ToSettleAmount > 0 &&
                                                          bh.ReferenceDocumentID != null
                                                      select bh).ToList();

                        if (accPettyCashBillHeaderList.Count() > 0)
                        {
                            foreach (var pettyBillHeader in accPettyCashBillHeaderList)
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
                                        LedgerID = pettyBillHeader.PettyCashLedgerID,
                                        LedgerSerial = accPettyCashPaymentProcessHeader.LedgerSerialNo,
                                        DocumentDate = accGlTransactionHeader.DocumentDate,
                                        PaymentDate = accGlTransactionHeader.DocumentDate,
                                        ReferenceID = accGlTransactionHeader.ReferenceID,
                                        DrCr = 2,
                                        Amount = pettyBillHeader.ToSettleAmount,
                                        TransactionTypeId = accGlTransactionHeader.DocumentID,
                                        ReferenceDocumentID = accPettyCashPaymentProcessDetail.ReferenceDocumentID,
                                        ReferenceDocumentDocumentID =
                                            accPettyCashPaymentProcessDetail.ReferenceDocumentDocumentID,
                                        ReferenceLocationID = accPettyCashPaymentProcessDetail.ReferenceLocationID,
                                        ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                                        TransactionDefinitionId = 1,
                                        ChequeNo = string.Empty,
                                        //Reference = accPettyCashBillHeader.Reference,
                                        //Remark = accPettyCashBillHeader.Remark,
                                        DocumentStatus = accPettyCashPaymentProcessHeader.DocumentStatus,
                                        IsUpLoad = isUpload,
                                        GroupOfCompanyID = accPettyCashPaymentProcessHeader.GroupOfCompanyID,
                                        CreatedUser = accPettyCashPaymentProcessHeader.CreatedUser,
                                        CreatedDate = accPettyCashPaymentProcessHeader.CreatedDate,
                                        ModifiedUser = accPettyCashPaymentProcessHeader.ModifiedUser,
                                        ModifiedDate = accPettyCashPaymentProcessHeader.ModifiedDate,
                                        DataTransfer = accPettyCashPaymentProcessHeader.DataTransfer,
                                    };
                                accGlTransactionDetailTempList.Add(te1);


                            }
                        }

                        #endregion
                    }
                }

                accGlTransactionHeader.AccGlTransactionDetails = accGlTransactionDetailTempList.ToList();

                bool isValid = accGlTransactionService.TallyTransactionEntry(accGlTransactionDetailTempList);
                if (isValid)
                {
                    if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                    { accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader); }
                    else
                    { accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader); }
                }
                else
                {
                    return; // should rollback all transactions.
                }
                context.SaveChanges();

            }
        }

        public void UpdatePettyCashImprest(AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader)
        {
            AccPettyCashImprestService accPettyCashImprestService = new AccPettyCashImprestService();
            AccPettyCashImprestDetail accPettyCashImprestDetail = new AccPettyCashImprestDetail();

            accPettyCashImprestDetail.CompanyID = accPettyCashPaymentProcessHeader.CompanyID;
            accPettyCashImprestDetail.LocationID = accPettyCashPaymentProcessHeader.LocationID;
            accPettyCashImprestDetail.PettyCashLedgerID = accPettyCashPaymentProcessHeader.PettyCashLedgerID;
            accPettyCashImprestDetail.IssuedAmount = accPettyCashPaymentProcessHeader.Amount;
            accPettyCashImprestDetail.UsedAmount = accPettyCashPaymentProcessHeader.Amount;

            accPettyCashImprestService.UpdatePettyCashImprest(accPettyCashImprestDetail);
        }

        public void UpdateProcessedAccPettyCashPaymentHeader(List<AccPettyCashPaymentProcessDetail> accPettyCashPaymentProcessDetailList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                foreach (var pettyPaymentDetail in accPettyCashPaymentProcessDetailList)
                {
                    AccPettyCashPaymentService accPettyCashPaymentService = new AccPettyCashPaymentService();
                    AccPettyCashPaymentHeader accPettyCashPaymentHeader = new AccPettyCashPaymentHeader();
                    List<AccPettyCashPaymentDetail> accPettyCashPaymentDetailList = new List<AccPettyCashPaymentDetail>();
                    List<AccPettyCashPaymentHeader> accPettyCashPaymentHeaderList = new List<AccPettyCashPaymentHeader>();

                    accPettyCashPaymentHeaderList = (from ih in context.AccPettyCashPaymentHeaders
                                                     where
                                                         ih.LocationID.Equals(pettyPaymentDetail.ReferenceLocationID) &&
                                                         ih.DocumentID.Equals(pettyPaymentDetail.ReferenceDocumentDocumentID) &&
                                                         ih.AccPettyCashPaymentHeaderID.Equals(pettyPaymentDetail.ReferenceDocumentID)
                                                     select ih).ToList();

                    if (accPettyCashPaymentHeaderList.Count() > 0)
                    {
                        foreach (var pettyPaymentHeader in accPettyCashPaymentHeaderList)
                        {
                            accPettyCashPaymentHeader = pettyPaymentHeader;
                            accPettyCashPaymentHeader.PaymentStatus = 3;

                            var accPettyCashPaymentDetails = (from id in context.AccPettyCashPaymentDetails
                                                              join ih in context.AccPettyCashPaymentHeaders on id.AccPettyCashPaymentHeaderID equals ih.AccPettyCashPaymentHeaderID
                                                              where
                                                                  ih.LocationID.Equals(pettyPaymentHeader.LocationID) &&
                                                                  ih.DocumentID.Equals(pettyPaymentHeader.DocumentID) &&
                                                                  ih.DocumentNo.Equals(pettyPaymentHeader.DocumentNo)
                                                              select id).ToList();

                            foreach (AccPettyCashPaymentDetail accPettyCashPaymentDetail in accPettyCashPaymentDetails)
                            {
                                accPettyCashPaymentDetail.PaymentStatus = 3;

                                accPettyCashPaymentDetailList.Add(accPettyCashPaymentDetail);
                            }

                            accPettyCashPaymentHeader.AccPettyCashPaymentDetails = accPettyCashPaymentDetailList;
                        }
                    }

                    try
                    {
                        accPettyCashPaymentService.UpdateAccPettyCashPaymentHeader(accPettyCashPaymentHeader);
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
        }

        /// <summary>
        /// Get Saved AccPettyCashPaymentProcessHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public AccPettyCashPaymentProcessHeader GetAccPettyCashPaymentProcessHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                return context.AccPettyCashPaymentProcessHeaders.Include("AccPettyCashPaymentProcessDetails").Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
            }
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                List<string> documentNoList = context.AccPettyCashPaymentProcessHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
                return documentNoList.ToArray();
            }
        }

        public List<AccPettyCashPaymentHeader> GetAllPettyCashPaymentProcessDetail(AccPettyCashPaymentProcessHeader accPettyCashPaymentProcessHeader)
        {
            List<AccPettyCashPaymentHeader> accPettyCashPaymentHeaderList = new List<AccPettyCashPaymentHeader>();

            AccPettyCashPaymentHeader accPettyCashPaymentHeaderTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashPaymentHeaders = (from id in context.AccPettyCashPaymentHeaders
                                                  join pd in context.AccPettyCashPaymentProcessDetails on id.AccPettyCashPaymentHeaderID equals pd.ReferenceDocumentID
                                                  join ph in context.AccPettyCashPaymentProcessHeaders on pd.AccPettyCashPaymentProcessHeaderID equals ph.AccPettyCashPaymentProcessHeaderID
                                                  join ld in context.AccLedgerAccounts on id.PettyCashLedgerID equals ld.AccLedgerAccountID
                                                  where id.LocationID.Equals(accPettyCashPaymentProcessHeader.LocationID) && id.PettyCashLedgerID.Equals(accPettyCashPaymentProcessHeader.PettyCashLedgerID)
                                                  && id.PaymentStatus == 2 
                                                  select new // AccPettyCashVoucherHeader
                                                  {
                                                      AccPettyCashPaymentHeaderID = id.AccPettyCashPaymentHeaderID,
                                                      CompanyID = id.CompanyID,
                                                      LocationID = id.LocationID,
                                                      CostCentreID = id.CostCentreID,
                                                      DocumentID = id.DocumentID,
                                                      DocumentNo = id.DocumentNo,
                                                      DocumentDate = id.DocumentDate,
                                                      PettyCashLedgerID = id.PettyCashLedgerID,
                                                      Amount = id.Amount,
                                                      BalanceAmount = id.BalanceAmount,
                                                      EmployeeID = id.EmployeeID,
                                                      PayeeName = id.PayeeName,
                                                      Reference = id.Reference,
                                                      Remark = id.Remark,
                                                      PaymentDate = id.PaymentDate,
                                                      DocumentStatus = id.DocumentStatus,
                                                      PaymentStatus = id.PaymentStatus,
                                                      CreatedDate = id.CreatedDate,
                                                      CreatedUser = id.CreatedUser,
                                                      ModifiedDate = id.ModifiedDate,
                                                      ModifiedUser = id.ModifiedUser,
                                                      GroupOfCompanyID = id.GroupOfCompanyID,
                                                      DataTransfer = id.DataTransfer,
                                                      IsUpLoad = id.IsUpLoad,
                                                      AccPettyCashPaymentDetails = id.AccPettyCashPaymentDetails,
                                                      Status = true
                                                  }).ToList();

                foreach (var tempPettyCashPayment in accPettyCashPaymentHeaders)
                {
                    accPettyCashPaymentHeaderTemp = new AccPettyCashPaymentHeader();
                    accPettyCashPaymentHeaderTemp.AccPettyCashPaymentHeaderID = tempPettyCashPayment.AccPettyCashPaymentHeaderID;
                    accPettyCashPaymentHeaderTemp.CompanyID = tempPettyCashPayment.CompanyID;
                    accPettyCashPaymentHeaderTemp.LocationID = tempPettyCashPayment.LocationID;
                    accPettyCashPaymentHeaderTemp.CostCentreID = tempPettyCashPayment.CostCentreID;
                    accPettyCashPaymentHeaderTemp.DocumentID = tempPettyCashPayment.DocumentID;
                    accPettyCashPaymentHeaderTemp.DocumentNo = tempPettyCashPayment.DocumentNo;
                    accPettyCashPaymentHeaderTemp.DocumentDate = tempPettyCashPayment.DocumentDate;
                    accPettyCashPaymentHeaderTemp.PettyCashLedgerID = tempPettyCashPayment.PettyCashLedgerID;
                    accPettyCashPaymentHeaderTemp.PayeeName = tempPettyCashPayment.PayeeName;
                    accPettyCashPaymentHeaderTemp.PaymentDate = tempPettyCashPayment.PaymentDate;
                    accPettyCashPaymentHeaderTemp.Amount = tempPettyCashPayment.Amount;
                    accPettyCashPaymentHeaderTemp.BalanceAmount = tempPettyCashPayment.BalanceAmount;
                    accPettyCashPaymentHeaderTemp.DocumentStatus = tempPettyCashPayment.DocumentStatus;
                    accPettyCashPaymentHeaderTemp.PaymentStatus = tempPettyCashPayment.PaymentStatus;
                    accPettyCashPaymentHeaderTemp.IsUpLoad = tempPettyCashPayment.IsUpLoad;
                    accPettyCashPaymentHeaderTemp.CreatedDate = tempPettyCashPayment.CreatedDate;
                    accPettyCashPaymentHeaderTemp.CreatedUser = tempPettyCashPayment.CreatedUser;
                    accPettyCashPaymentHeaderTemp.ModifiedDate = tempPettyCashPayment.ModifiedDate;
                    accPettyCashPaymentHeaderTemp.ModifiedUser = tempPettyCashPayment.ModifiedUser;
                    accPettyCashPaymentHeaderTemp.GroupOfCompanyID = tempPettyCashPayment.GroupOfCompanyID;
                    accPettyCashPaymentHeaderTemp.DataTransfer = tempPettyCashPayment.DataTransfer;
                    accPettyCashPaymentHeaderTemp.AccPettyCashPaymentDetails = tempPettyCashPayment.AccPettyCashPaymentDetails;
                    accPettyCashPaymentHeaderTemp.Status = tempPettyCashPayment.Status;
                    accPettyCashPaymentHeaderList.Add(accPettyCashPaymentHeaderTemp);
                }
            }
            return accPettyCashPaymentHeaderList;
        }

        public List<AccPettyCashPaymentHeader> GetAllPettyCashPaymentHeader(long pettyCashLedgerID, int locationID)
        {
            List<AccPettyCashPaymentHeader> accPettyCashPaymentHeaderList = new List<AccPettyCashPaymentHeader>();

            AccPettyCashPaymentHeader accPettyCashPaymentHeaderTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashPaymentHeaders = (from id in context.AccPettyCashPaymentHeaders
                                                  join ld in context.AccLedgerAccounts on id.PettyCashLedgerID equals ld.AccLedgerAccountID
                                                  where (locationID != 0 ? id.LocationID.Equals(locationID) : true) && (pettyCashLedgerID != 0 ? id.PettyCashLedgerID.Equals(pettyCashLedgerID) : true) 
                                                  && id.PaymentStatus == 2 && id.DocumentStatus == 1
                                                  select new // AccPettyCashVoucherHeader
                                                  {
                                                      AccPettyCashPaymentHeaderID = id.AccPettyCashPaymentHeaderID,
                                                      CompanyID = id.CompanyID,
                                                      LocationID = id.LocationID,
                                                      CostCentreID = id.CostCentreID,
                                                      DocumentID = id.DocumentID,
                                                      DocumentNo = id.DocumentNo,
                                                      DocumentDate = id.DocumentDate,
                                                      PettyCashLedgerID = id.PettyCashLedgerID,
                                                      Amount = id.Amount,
                                                      BalanceAmount = id.BalanceAmount,
                                                      EmployeeID = id.EmployeeID,
                                                      PayeeName = id.PayeeName,
                                                      Reference = id.Reference,
                                                      Remark = id.Remark,
                                                      PaymentDate = id.PaymentDate,
                                                      DocumentStatus = id.DocumentStatus,
                                                      PaymentStatus = id.PaymentStatus,
                                                      CreatedDate = id.CreatedDate,
                                                      CreatedUser = id.CreatedUser,
                                                      ModifiedDate = id.ModifiedDate,
                                                      ModifiedUser = id.ModifiedUser,
                                                      GroupOfCompanyID = id.GroupOfCompanyID,
                                                      DataTransfer = id.DataTransfer,
                                                      IsUpLoad = id.IsUpLoad,
                                                      AccPettyCashPaymentDetails = id.AccPettyCashPaymentDetails,
                                                      Status = true
                                                  }).ToList();

                foreach (var tempPettyCashPayment in accPettyCashPaymentHeaders)
                {
                    accPettyCashPaymentHeaderTemp = new AccPettyCashPaymentHeader();
                    accPettyCashPaymentHeaderTemp.AccPettyCashPaymentHeaderID = tempPettyCashPayment.AccPettyCashPaymentHeaderID;
                    accPettyCashPaymentHeaderTemp.CompanyID = tempPettyCashPayment.CompanyID;
                    accPettyCashPaymentHeaderTemp.LocationID = tempPettyCashPayment.LocationID;
                    accPettyCashPaymentHeaderTemp.CostCentreID = tempPettyCashPayment.CostCentreID;
                    accPettyCashPaymentHeaderTemp.DocumentID = tempPettyCashPayment.DocumentID;
                    accPettyCashPaymentHeaderTemp.DocumentNo = tempPettyCashPayment.DocumentNo;
                    accPettyCashPaymentHeaderTemp.DocumentDate = tempPettyCashPayment.DocumentDate;
                    accPettyCashPaymentHeaderTemp.PettyCashLedgerID = tempPettyCashPayment.PettyCashLedgerID;
                    accPettyCashPaymentHeaderTemp.PayeeName = tempPettyCashPayment.PayeeName;
                    accPettyCashPaymentHeaderTemp.PaymentDate = tempPettyCashPayment.PaymentDate;
                    accPettyCashPaymentHeaderTemp.Amount = tempPettyCashPayment.Amount;
                    accPettyCashPaymentHeaderTemp.BalanceAmount = tempPettyCashPayment.BalanceAmount;
                    accPettyCashPaymentHeaderTemp.DocumentStatus = tempPettyCashPayment.DocumentStatus;
                    accPettyCashPaymentHeaderTemp.PaymentStatus = tempPettyCashPayment.PaymentStatus;
                    accPettyCashPaymentHeaderTemp.IsUpLoad = tempPettyCashPayment.IsUpLoad;
                    accPettyCashPaymentHeaderTemp.CreatedDate = tempPettyCashPayment.CreatedDate;
                    accPettyCashPaymentHeaderTemp.CreatedUser = tempPettyCashPayment.CreatedUser;
                    accPettyCashPaymentHeaderTemp.ModifiedDate = tempPettyCashPayment.ModifiedDate;
                    accPettyCashPaymentHeaderTemp.ModifiedUser = tempPettyCashPayment.ModifiedUser;
                    accPettyCashPaymentHeaderTemp.GroupOfCompanyID = tempPettyCashPayment.GroupOfCompanyID;
                    accPettyCashPaymentHeaderTemp.DataTransfer = tempPettyCashPayment.DataTransfer;
                    accPettyCashPaymentHeaderTemp.AccPettyCashPaymentDetails = tempPettyCashPayment.AccPettyCashPaymentDetails;
                    //accPettyCashPaymentHeaderTemp.Status = tempPettyCashVoucher.Status;
                    accPettyCashPaymentHeaderList.Add(accPettyCashPaymentHeaderTemp);
                }
            }
            return accPettyCashPaymentHeaderList;
        }

        public DataTable GetPendingPettyCashPaymentUpdateDocuments()
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                var sql = (from poh in context.AccPettyCashPaymentProcessHeaders
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

            using (ERPDbContext context = new ERPDbContext())
            {
                if (isTemporytNo)
                {
                    context.AccLedgerSerialNumbers.Update(
                        d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId),
                        d => new AccLedgerSerialNumber {TempDocumentNo = d.TempDocumentNo + 1});
                    tempCode =
                        context.AccLedgerSerialNumbers.Where(
                            d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId))
                               .Max(d => d.TempDocumentNo);
                }
                else
                {
                    
                    context.AccLedgerSerialNumbers.Update(
                        d => d.LocationID.Equals(locationID) && d.LedgerID.Equals(ledgerID),
                        d => new AccLedgerSerialNumber {DocumentNo = d.DocumentNo + 1});
                    tempCode =
                        context.AccLedgerSerialNumbers.Where(
                            d => d.LocationID.Equals(locationID) && d.LedgerID.Equals(ledgerID))
                                .Max(d => d.DocumentNo);
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
