using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;

namespace Service
{
    public class AccPettyCashVoucherService
    {
        public void AddAccPettyCashVoucherHeader(AccPettyCashVoucherHeader accPettyCashVoucherHeader)
        {
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    context.AccPettyCashVoucherHeaders.Add(accPettyCashVoucherHeader);
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

        public void UpdateAccPettyCashVoucherHeader(AccPettyCashVoucherHeader accPettyCashVoucherHeader)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                try
                {
                    var existingAccPettyCashVoucherHeader = context.AccPettyCashVoucherHeaders.AsNoTracking().Include(s => s.AccPettyCashVoucherDetails)
                                                               .Where(s => s.AccPettyCashVoucherHeaderID == accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID)
                                                               .FirstOrDefault<AccPettyCashVoucherHeader>();

                    if (existingAccPettyCashVoucherHeader != null)
                    {
                        var existingAccPettyCashVoucherDetails = existingAccPettyCashVoucherHeader.AccPettyCashVoucherDetails.ToList<AccPettyCashVoucherDetail>();

                        var updatedAccPettyCashVoucherDetails = accPettyCashVoucherHeader.AccPettyCashVoucherDetails.ToList<AccPettyCashVoucherDetail>();

                        var addedAccPettyCashVoucherDetails = CommonService.Except(updatedAccPettyCashVoucherDetails, existingAccPettyCashVoucherDetails, voucherDeta => voucherDeta.AccPettyCashVoucherDetailID);

                        var deletedAccPettyCashVoucherDetails = CommonService.Except(existingAccPettyCashVoucherDetails, updatedAccPettyCashVoucherDetails, voucherDeta => voucherDeta.AccPettyCashVoucherDetailID);

                        var modifiedAccPettyCashVoucherDetails = CommonService.Except(updatedAccPettyCashVoucherDetails, addedAccPettyCashVoucherDetails, voucherDeta => voucherDeta.AccPettyCashVoucherDetailID);

                        addedAccPettyCashVoucherDetails.ToList<AccPettyCashVoucherDetail>().ForEach(voucherDeta => context.Entry(voucherDeta).State = EntityState.Added);

                        deletedAccPettyCashVoucherDetails.ToList<AccPettyCashVoucherDetail>().ForEach(voucherDeta => context.Entry(voucherDeta).State = EntityState.Deleted);

                        foreach (AccPettyCashVoucherDetail tempAccPettyCashVoucherDetail in modifiedAccPettyCashVoucherDetails)
                        {
                            var existingAccPettyCashVoucherDetail = context.AccPettyCashVoucherDetails.Find(tempAccPettyCashVoucherDetail.AccPettyCashVoucherDetailID);

                            if (existingAccPettyCashVoucherDetail != null)
                            {
                                var accPettyCashVoucherDetailEntry = context.Entry(existingAccPettyCashVoucherDetail);
                                accPettyCashVoucherDetailEntry.CurrentValues.SetValues(tempAccPettyCashVoucherDetail);
                            }
                        }

                        var existingAccPettyCashVoucherHeaders = context.AccPettyCashVoucherHeaders.Find(accPettyCashVoucherHeader.AccPettyCashVoucherHeaderID);

                        if (existingAccPettyCashVoucherHeaders != null)
                        {
                            var accPettyCashVoucherHeaderEntry = context.Entry(existingAccPettyCashVoucherHeaders);
                            accPettyCashVoucherHeaderEntry.CurrentValues.SetValues(accPettyCashVoucherHeader);
                        }
                        context.SaveChanges();
                    }


                    //accPettyCashVoucherHeader.AccPettyCashVoucherDetails.ToList().ForEach(r => context.Entry(r).State = EntityState.Modified);
                    //context.SaveChanges();
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

        public void ConfirmAccPettyCashVoucherHeader(List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                foreach (AccPettyCashVoucherHeader pettyVoucherHeader in accPettyCashVoucherHeaderList.Where(v => v.Status == true))
                {
                    AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                    AccPettyCashVoucherHeader accPettyCashVoucherHeader = pettyVoucherHeader;
                    List<AccPettyCashVoucherDetail> accPettyCashVoucherDetailList = new List<AccPettyCashVoucherDetail>();

                    accPettyCashVoucherHeader.VoucherStatus = 1;

                    var accPettyCashVoucherDetails = (from id in context.AccPettyCashVoucherDetails
                                                      join ih in context.AccPettyCashVoucherHeaders on id.AccPettyCashVoucherHeaderID equals ih.AccPettyCashVoucherHeaderID 
                                                      where
                                                          ih.LocationID.Equals(pettyVoucherHeader.LocationID) &&
                                                          ih.DocumentID.Equals(pettyVoucherHeader.DocumentID) &&
                                                          ih.DocumentNo.Equals(pettyVoucherHeader.DocumentNo)
                                                      select id);

                    foreach (AccPettyCashVoucherDetail accPettyCashVoucherDetail in accPettyCashVoucherDetails)
                    {
                        accPettyCashVoucherDetail.VoucherStatus = 1;
                        accPettyCashVoucherDetailList.Add(accPettyCashVoucherDetail);
                    }

                    accPettyCashVoucherHeader.AccPettyCashVoucherDetails = accPettyCashVoucherDetailList;
                        
                    try
                    {
                        accPettyCashVoucherService.UpdateAccPettyCashVoucherHeader(accPettyCashVoucherHeader);
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

        public void CancelAccPettyCashVoucherHeader(List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                foreach (AccPettyCashVoucherHeader pettyVoucherHeader in accPettyCashVoucherHeaderList.Where(v => v.Status == true))
                {
                    AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                    AccPettyCashVoucherHeader accPettyCashVoucherHeader = pettyVoucherHeader;
                    List<AccPettyCashVoucherDetail> accPettyCashVoucherDetailList = new List<AccPettyCashVoucherDetail>();

                    accPettyCashVoucherHeader.VoucherStatus = 3;

                    var accPettyCashVoucherDetails = (from id in context.AccPettyCashVoucherDetails
                                                      join ih in context.AccPettyCashVoucherHeaders on id.AccPettyCashVoucherHeaderID equals ih.AccPettyCashVoucherHeaderID 
                                                      where
                                                          ih.LocationID.Equals(pettyVoucherHeader.LocationID) &&
                                                          ih.DocumentID.Equals(pettyVoucherHeader.DocumentID) &&
                                                          ih.DocumentNo.Equals(pettyVoucherHeader.DocumentNo)
                                                      select id);

                    foreach (AccPettyCashVoucherDetail accPettyCashVoucherDetail in accPettyCashVoucherDetails)
                    {
                        accPettyCashVoucherDetail.VoucherStatus = 3;

                        accPettyCashVoucherDetailList.Add(accPettyCashVoucherDetail);
                    }

                    accPettyCashVoucherHeader.AccPettyCashVoucherDetails = accPettyCashVoucherDetailList;
                        
                    try
                    {
                        accPettyCashVoucherService.UpdateAccPettyCashVoucherHeader(accPettyCashVoucherHeader);
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

        public void IssueAccPettyCashVoucherHeader(List<AccPettyCashPaymentDetail> accPettyCashPaymentDetailList)
        {
            using (ERPDbContext context = new ERPDbContext())
            {
                foreach (var pettyVoucherDetail in accPettyCashPaymentDetailList)
                {
                    AccPettyCashVoucherService accPettyCashVoucherService = new AccPettyCashVoucherService();
                    AccPettyCashVoucherHeader accPettyCashVoucherHeader = new AccPettyCashVoucherHeader();
                    List<AccPettyCashVoucherDetail> accPettyCashVoucherDetailList = new List<AccPettyCashVoucherDetail>();
                    List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList = new List<AccPettyCashVoucherHeader>();

                    accPettyCashVoucherHeaderList = (from ih in context.AccPettyCashVoucherHeaders
                                                      where
                                                          ih.LocationID.Equals(pettyVoucherDetail.ReferenceLocationID) &&
                                                          ih.DocumentID.Equals(pettyVoucherDetail.ReferenceDocumentDocumentID) &&
                                                          ih.AccPettyCashVoucherHeaderID.Equals(pettyVoucherDetail.ReferenceDocumentID)
                                                      select ih).ToList();

                    if (accPettyCashVoucherHeaderList.Count() > 0)
                    {
                        foreach (var pettyVoucherHeader in accPettyCashVoucherHeaderList)
                        {
                            accPettyCashVoucherHeader = pettyVoucherHeader;
                            accPettyCashVoucherHeader.VoucherStatus = 2;

                            var accPettyCashVoucherDetails = (from id in context.AccPettyCashVoucherDetails
                                                              join ih in context.AccPettyCashVoucherHeaders on id.AccPettyCashVoucherHeaderID equals ih.AccPettyCashVoucherHeaderID
                                                              where
                                                                  ih.LocationID.Equals(pettyVoucherHeader.LocationID) &&
                                                                  ih.DocumentID.Equals(pettyVoucherHeader.DocumentID) &&
                                                                  ih.DocumentNo.Equals(pettyVoucherHeader.DocumentNo)
                                                              select id).ToList();

                            foreach (AccPettyCashVoucherDetail accPettyCashVoucherDetail in accPettyCashVoucherDetails)
                            {
                                accPettyCashVoucherDetail.VoucherStatus = 2;

                                accPettyCashVoucherDetailList.Add(accPettyCashVoucherDetail);
                            }

                            accPettyCashVoucherHeader.AccPettyCashVoucherDetails = accPettyCashVoucherDetailList;
                        }
                    }

                    try
                    {
                        accPettyCashVoucherService.UpdateAccPettyCashVoucherHeader(accPettyCashVoucherHeader);
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

        public AccPettyCashVoucherHeader GetAccPettyCashVoucherHeader(string documentNo, int documentID, int locationID)
        {
            using (ERPDbContext context = new ERPDbContext())
            { return context.AccPettyCashVoucherHeaders.Where(ph => ph.DocumentNo.Equals(documentNo) && ph.DocumentID.Equals(documentID) && ph.LocationID.Equals(locationID)).FirstOrDefault(); }
        }

        public List<AccPettyCashVoucherHeader> GetAllPettyCashVoucherHeader(long pettyCashLedgerID, long employeeID, int locationID)
        {
            List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList = new List<AccPettyCashVoucherHeader>();

            AccPettyCashVoucherHeader accPettyCashVoucherHeaderTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashVoucherDetails = (from id in context.AccPettyCashVoucherHeaders
                                                    join ld in context.AccLedgerAccounts on id.PettyCashLedgerID equals ld.AccLedgerAccountID
                                                  where (locationID != 0 ? id.LocationID.Equals(locationID) : true) && (pettyCashLedgerID != 0 ? id.PettyCashLedgerID.Equals(pettyCashLedgerID) : true) && (employeeID != 0 ? (id.EmployeeID.Equals(employeeID) || id.EmployeeID == 0) : true)
                                                  && id.BalanceAmount > 0 && id.VoucherStatus == 0
                                              select new // AccPettyCashVoucherHeader
                                             {
                                                 AccPettyCashVoucherHeaderID = id.AccPettyCashVoucherHeaderID,
                                                 PettyCashVoucherHeaderID = id.PettyCashVoucherHeaderID,
                                                 CompanyID = id.CompanyID,
                                                 LocationID = id.LocationID,
                                                 CostCentreID = id.CostCentreID,
                                                 DocumentID = id.DocumentID,
                                                 DocumentNo = id.DocumentNo,
                                                 DocumentDate = id.DocumentDate,
                                                 LedgerSerialNo = id.LedgerSerialNo,
                                                 PettyCashLedgerID = id.PettyCashLedgerID,
                                                 Amount = id.Amount,
                                                 IOUAmount = id.IOUAmount,
                                                 BalanceAmount = id.BalanceAmount,
                                                 EmployeeID = id.EmployeeID,
                                                 ReferenceID = id.ReferenceID,
                                                 PayeeName = id.PayeeName,
                                                 Reference = id.Reference,
                                                 Remark = id.Remark,
                                                 PaymentDate = id.PaymentDate,
                                                 ReferenceDocumentID = id.ReferenceDocumentID,
                                                 ReferenceDocumentDocumentID = id.ReferenceDocumentDocumentID,
                                                 ReferenceLocationID = id.ReferenceLocationID,
                                                 DocumentStatus = id.DocumentStatus,
                                                 VoucherStatus = id.VoucherStatus,
                                                 CreatedDate = id.CreatedDate,
                                                 CreatedUser = id.CreatedUser,
                                                 ModifiedDate = id.ModifiedDate,
                                                 ModifiedUser = id.ModifiedUser,
                                                 GroupOfCompanyID = id.GroupOfCompanyID,
                                                 DataTransfer = id.DataTransfer,
                                                 IsUpLoad = id.IsUpLoad,
                                                 AccPettyCashVoucherDetails = id.AccPettyCashVoucherDetails,
                                                 Status = false
                                             }).ToList();

                foreach (var tempPettyCashVoucher in accPettyCashVoucherDetails)
                {
                    accPettyCashVoucherHeaderTemp = new AccPettyCashVoucherHeader();
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherHeaderID = tempPettyCashVoucher.AccPettyCashVoucherHeaderID;
                    accPettyCashVoucherHeaderTemp.PettyCashVoucherHeaderID = tempPettyCashVoucher.PettyCashVoucherHeaderID;
                    accPettyCashVoucherHeaderTemp.CompanyID = tempPettyCashVoucher.CompanyID;
                    accPettyCashVoucherHeaderTemp.LocationID = tempPettyCashVoucher.LocationID;
                    accPettyCashVoucherHeaderTemp.CostCentreID = tempPettyCashVoucher.CostCentreID;
                    accPettyCashVoucherHeaderTemp.DocumentID = tempPettyCashVoucher.DocumentID;
                    accPettyCashVoucherHeaderTemp.DocumentNo = tempPettyCashVoucher.DocumentNo;
                    accPettyCashVoucherHeaderTemp.DocumentDate = tempPettyCashVoucher.DocumentDate;
                    accPettyCashVoucherHeaderTemp.LedgerSerialNo = tempPettyCashVoucher.LedgerSerialNo;
                    accPettyCashVoucherHeaderTemp.PettyCashLedgerID = tempPettyCashVoucher.PettyCashLedgerID;
                    accPettyCashVoucherHeaderTemp.EmployeeID = tempPettyCashVoucher.EmployeeID;
                    accPettyCashVoucherHeaderTemp.ReferenceID = tempPettyCashVoucher.ReferenceID;
                    accPettyCashVoucherHeaderTemp.PayeeName = tempPettyCashVoucher.PayeeName;
                    accPettyCashVoucherHeaderTemp.PaymentDate = tempPettyCashVoucher.PaymentDate;
                    accPettyCashVoucherHeaderTemp.Amount = tempPettyCashVoucher.Amount;
                    accPettyCashVoucherHeaderTemp.IOUAmount = tempPettyCashVoucher.IOUAmount;
                    accPettyCashVoucherHeaderTemp.BalanceAmount = tempPettyCashVoucher.BalanceAmount;
                    accPettyCashVoucherHeaderTemp.Reference = tempPettyCashVoucher.Reference;
                    accPettyCashVoucherHeaderTemp.Remark = tempPettyCashVoucher.Remark;
                    accPettyCashVoucherHeaderTemp.ReferenceDocumentID = tempPettyCashVoucher.ReferenceDocumentID;
                    accPettyCashVoucherHeaderTemp.ReferenceDocumentDocumentID = tempPettyCashVoucher.ReferenceDocumentDocumentID;
                    accPettyCashVoucherHeaderTemp.ReferenceLocationID = tempPettyCashVoucher.ReferenceLocationID;
                    accPettyCashVoucherHeaderTemp.DocumentStatus = tempPettyCashVoucher.DocumentStatus;
                    accPettyCashVoucherHeaderTemp.VoucherStatus = tempPettyCashVoucher.VoucherStatus;
                    accPettyCashVoucherHeaderTemp.IsUpLoad = tempPettyCashVoucher.IsUpLoad;
                    accPettyCashVoucherHeaderTemp.CreatedDate = tempPettyCashVoucher.CreatedDate;
                    accPettyCashVoucherHeaderTemp.CreatedUser = tempPettyCashVoucher.CreatedUser;
                    accPettyCashVoucherHeaderTemp.ModifiedDate = tempPettyCashVoucher.ModifiedDate;
                    accPettyCashVoucherHeaderTemp.ModifiedUser = tempPettyCashVoucher.ModifiedUser;
                    accPettyCashVoucherHeaderTemp.GroupOfCompanyID = tempPettyCashVoucher.GroupOfCompanyID;
                    accPettyCashVoucherHeaderTemp.DataTransfer = tempPettyCashVoucher.DataTransfer;
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherDetails = tempPettyCashVoucher.AccPettyCashVoucherDetails;
                    accPettyCashVoucherHeaderTemp.Status = tempPettyCashVoucher.Status;
                    accPettyCashVoucherHeaderList.Add(accPettyCashVoucherHeaderTemp);
                }
            }
            
            return accPettyCashVoucherHeaderList;
        }


        public List<AccPettyCashVoucherHeader> GetAllPettyCashApprovedVoucherHeader(long pettyCashLedgerID, long employeeID, int locationID)
        {
            List<AccPettyCashVoucherHeader> accPettyCashVoucherHeaderList = new List<AccPettyCashVoucherHeader>();

            AccPettyCashVoucherHeader accPettyCashVoucherHeaderTemp;

            using (ERPDbContext context = new ERPDbContext())
            {
                var accPettyCashVoucherDetails = (from id in context.AccPettyCashVoucherHeaders
                                                    join ld in context.AccLedgerAccounts on id.PettyCashLedgerID equals ld.AccLedgerAccountID
                                                  where (locationID != 0 ? id.LocationID.Equals(locationID) : true) && (pettyCashLedgerID != 0 ? id.PettyCashLedgerID.Equals(pettyCashLedgerID) : true) && (employeeID != 0 ? (id.EmployeeID.Equals(employeeID) || id.EmployeeID == 0) : true)
                                                  && id.BalanceAmount > 0 && id.VoucherStatus == 1
                                              select new // AccPettyCashVoucherHeader
                                             {
                                                 AccPettyCashVoucherHeaderID = id.AccPettyCashVoucherHeaderID,
                                                 PettyCashVoucherHeaderID = id.PettyCashVoucherHeaderID,
                                                 CompanyID = id.CompanyID,
                                                 LocationID = id.LocationID,
                                                 CostCentreID = id.CostCentreID,
                                                 DocumentID = id.DocumentID,
                                                 DocumentNo = id.DocumentNo,
                                                 DocumentDate = id.DocumentDate,
                                                 LedgerSerialNo = id.LedgerSerialNo,
                                                 PettyCashLedgerID = id.PettyCashLedgerID,
                                                 Amount = id.Amount,
                                                 IOUAmount = id.IOUAmount,
                                                 BalanceAmount = id.BalanceAmount,
                                                 EmployeeID = id.EmployeeID,
                                                 ReferenceID = id.ReferenceID,
                                                 PayeeName = id.PayeeName,
                                                 Reference = id.Reference,
                                                 Remark = id.Remark,
                                                 PaymentDate = id.PaymentDate,
                                                 ReferenceDocumentID = id.ReferenceDocumentID,
                                                 ReferenceDocumentDocumentID = id.ReferenceDocumentDocumentID,
                                                 ReferenceLocationID = id.ReferenceLocationID,
                                                 DocumentStatus = id.DocumentStatus,
                                                 VoucherStatus = id.VoucherStatus,
                                                 CreatedDate = id.CreatedDate,
                                                 CreatedUser = id.CreatedUser,
                                                 ModifiedDate = id.ModifiedDate,
                                                 ModifiedUser = id.ModifiedUser,
                                                 GroupOfCompanyID = id.GroupOfCompanyID,
                                                 DataTransfer = id.DataTransfer,
                                                 IsUpLoad = id.IsUpLoad,
                                                 AccPettyCashVoucherDetails = id.AccPettyCashVoucherDetails,
                                                 Status = false
                                             }).ToList();

                foreach (var tempPettyCashVoucher in accPettyCashVoucherDetails)
                {
                    accPettyCashVoucherHeaderTemp = new AccPettyCashVoucherHeader();
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherHeaderID = tempPettyCashVoucher.AccPettyCashVoucherHeaderID;
                    accPettyCashVoucherHeaderTemp.PettyCashVoucherHeaderID = tempPettyCashVoucher.PettyCashVoucherHeaderID;
                    accPettyCashVoucherHeaderTemp.CompanyID = tempPettyCashVoucher.CompanyID;
                    accPettyCashVoucherHeaderTemp.LocationID = tempPettyCashVoucher.LocationID;
                    accPettyCashVoucherHeaderTemp.CostCentreID = tempPettyCashVoucher.CostCentreID;
                    accPettyCashVoucherHeaderTemp.DocumentID = tempPettyCashVoucher.DocumentID;
                    accPettyCashVoucherHeaderTemp.DocumentNo = tempPettyCashVoucher.DocumentNo;
                    accPettyCashVoucherHeaderTemp.DocumentDate = tempPettyCashVoucher.DocumentDate;
                    accPettyCashVoucherHeaderTemp.LedgerSerialNo = tempPettyCashVoucher.LedgerSerialNo;
                    accPettyCashVoucherHeaderTemp.PettyCashLedgerID = tempPettyCashVoucher.PettyCashLedgerID;
                    accPettyCashVoucherHeaderTemp.EmployeeID = tempPettyCashVoucher.EmployeeID;
                    accPettyCashVoucherHeaderTemp.ReferenceID = tempPettyCashVoucher.ReferenceID;
                    accPettyCashVoucherHeaderTemp.PayeeName = tempPettyCashVoucher.PayeeName;
                    accPettyCashVoucherHeaderTemp.PaymentDate = tempPettyCashVoucher.PaymentDate;
                    accPettyCashVoucherHeaderTemp.Amount = tempPettyCashVoucher.Amount;
                    accPettyCashVoucherHeaderTemp.IOUAmount = tempPettyCashVoucher.IOUAmount;
                    accPettyCashVoucherHeaderTemp.BalanceAmount = tempPettyCashVoucher.BalanceAmount;
                    accPettyCashVoucherHeaderTemp.Reference = tempPettyCashVoucher.Reference;
                    accPettyCashVoucherHeaderTemp.Remark = tempPettyCashVoucher.Remark;
                    accPettyCashVoucherHeaderTemp.ReferenceDocumentID = tempPettyCashVoucher.ReferenceDocumentID;
                    accPettyCashVoucherHeaderTemp.ReferenceDocumentDocumentID = tempPettyCashVoucher.ReferenceDocumentDocumentID;
                    accPettyCashVoucherHeaderTemp.ReferenceLocationID = tempPettyCashVoucher.ReferenceLocationID;
                    accPettyCashVoucherHeaderTemp.DocumentStatus = tempPettyCashVoucher.DocumentStatus;
                    accPettyCashVoucherHeaderTemp.VoucherStatus = tempPettyCashVoucher.VoucherStatus;
                    accPettyCashVoucherHeaderTemp.IsUpLoad = tempPettyCashVoucher.IsUpLoad;
                    accPettyCashVoucherHeaderTemp.CreatedDate = tempPettyCashVoucher.CreatedDate;
                    accPettyCashVoucherHeaderTemp.CreatedUser = tempPettyCashVoucher.CreatedUser;
                    accPettyCashVoucherHeaderTemp.ModifiedDate = tempPettyCashVoucher.ModifiedDate;
                    accPettyCashVoucherHeaderTemp.ModifiedUser = tempPettyCashVoucher.ModifiedUser;
                    accPettyCashVoucherHeaderTemp.GroupOfCompanyID = tempPettyCashVoucher.GroupOfCompanyID;
                    accPettyCashVoucherHeaderTemp.DataTransfer = tempPettyCashVoucher.DataTransfer;
                    accPettyCashVoucherHeaderTemp.AccPettyCashVoucherDetails = tempPettyCashVoucher.AccPettyCashVoucherDetails;
                    accPettyCashVoucherHeaderTemp.Status = tempPettyCashVoucher.Status;
                    accPettyCashVoucherHeaderList.Add(accPettyCashVoucherHeaderTemp);
                }
            }
            
            return accPettyCashVoucherHeaderList;
        }
    }
}
