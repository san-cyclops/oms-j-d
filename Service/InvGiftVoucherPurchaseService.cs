using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using MoreLinq;
using Data;
using Domain;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class InvGiftVoucherPurchaseService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save Gift voucher purchase
        /// </summary>
        /// <param name="invGiftVoucherPurchaseHeader"></param>
        /// <param name="invGiftVoucherPurchaseDetailsTemp"></param>
        public bool SaveGiftVoucherPurchase(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp, out string newDocumentNo, string formName = "", bool isTStatus=false)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (invGiftVoucherPurchaseHeader.DocumentStatus.Equals(1))
                {
                    InvGiftVoucherPurchaseService invGiftVoucherPurchaseService = new InvGiftVoucherPurchaseService();
                    LocationService locationService = new LocationService();
                    invGiftVoucherPurchaseHeader.DocumentNo = invGiftVoucherPurchaseService.GetDocumentNo(formName, invGiftVoucherPurchaseHeader.LocationID, locationService.GetLocationsByID(invGiftVoucherPurchaseHeader.LocationID).LocationCode, invGiftVoucherPurchaseHeader.DocumentID, false).Trim();
                }

                newDocumentNo = invGiftVoucherPurchaseHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                #region Transaction Details

                if (invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID.Equals(0))
                {context.InvGiftVoucherPurchaseHeaders.Add(invGiftVoucherPurchaseHeader);}
                else
                {context.Entry(invGiftVoucherPurchaseHeader).State = EntityState.Modified;}

                context.SaveChanges();

                context.Set<InvGiftVoucherPurchaseDetail>().Delete(context.InvGiftVoucherPurchaseDetails.Where(p => p.InvGiftVoucherPurchaseHeaderID.Equals(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID) && p.LocationID.Equals(invGiftVoucherPurchaseHeader.LocationID) && p.DocumentID.Equals(invGiftVoucherPurchaseHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var invGiftVoucherPurchaseDetailSaveQuery = (from pd in invGiftVoucherPurchaseDetailsTemp
                                                             select new 
                                                  {
                                                      InvGiftVoucherPurchaseDetailID = 0,
                                                      GiftVoucherPurchaseDetailID = 0,
                                                      InvGiftVoucherPurchaseHeaderID = invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID,
                                                      CompanyID = invGiftVoucherPurchaseHeader.CompanyID,
                                                      LocationID = invGiftVoucherPurchaseHeader.LocationID,
                                                      DocumentID = invGiftVoucherPurchaseHeader.DocumentID,
                                                      //DocumentNo = invGiftVoucherPurchaseHeader.DocumentNo,
                                                      DocumentDate = invGiftVoucherPurchaseHeader.DocumentDate,
                                                      LineNo = pd.LineNo,
                                                      InvGiftVoucherMasterID = pd.InvGiftVoucherMasterID,
                                                      NumberOfCount = pd.NumberOfCount,
                                                      VoucherAmount = pd.VoucherValue,
                                                      VoucherType = invGiftVoucherPurchaseHeader.VoucherType,
                                                      IsTransfer = false,
                                                      DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus,
                                                      GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID,
                                                      CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser,
                                                      CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate,
                                                      ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser,
                                                      ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate,
                                                      DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer
                                                  }).ToList();


                //CommonService.BulkInsert(Common.LINQToDataTable(invGiftVoucherPurchaseDetailSaveQuery), "InvGiftVoucherPurchaseDetail");

                DataTable DTITEM = new DataTable();
                DTITEM = invGiftVoucherPurchaseDetailSaveQuery.ToDataTable();

                string mergeSql = CommonService.MergeSqlToForTrans(DTITEM, "InvGiftVoucherPurchaseDetail");
                CommonService.BulkInsert(DTITEM, "InvGiftVoucherPurchaseDetail", mergeSql);

                #endregion

                #region Payment Details

                AccPaymentService accPaymentService = new AccPaymentService();

                AccPaymentHeader accPaymentHeader = new AccPaymentHeader();
                accPaymentHeader = accPaymentService.getAccPaymentHeader(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID, invGiftVoucherPurchaseHeader.DocumentID, invGiftVoucherPurchaseHeader.LocationID);
                if (accPaymentHeader == null)
                {accPaymentHeader = new AccPaymentHeader();}

                accPaymentHeader.Amount = invGiftVoucherPurchaseHeader.NetAmount;
                accPaymentHeader.BalanceAmount = invGiftVoucherPurchaseHeader.NetAmount;
                accPaymentHeader.CompanyID = invGiftVoucherPurchaseHeader.CompanyID;
                accPaymentHeader.CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate;
                accPaymentHeader.CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser;
                accPaymentHeader.DocumentDate = invGiftVoucherPurchaseHeader.DocumentDate;
                accPaymentHeader.DocumentID = invGiftVoucherPurchaseHeader.CompanyID;
                accPaymentHeader.DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus;
                accPaymentHeader.DocumentNo = invGiftVoucherPurchaseHeader.DocumentNo;
                accPaymentHeader.GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID;
                accPaymentHeader.DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer;
                accPaymentHeader.IsUpLoad = false;
                accPaymentHeader.LocationID = invGiftVoucherPurchaseHeader.LocationID;
                accPaymentHeader.ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate;
                accPaymentHeader.ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser;
                accPaymentHeader.PaymentDate = invGiftVoucherPurchaseHeader.DocumentDate;
                accPaymentHeader.Reference = invGiftVoucherPurchaseHeader.ReferenceNo;
                accPaymentHeader.ReferenceDocumentDocumentID = invGiftVoucherPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceDocumentID = invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID;
                accPaymentHeader.ReferenceID = invGiftVoucherPurchaseHeader.DocumentID;
                accPaymentHeader.ReferenceLocationID = invGiftVoucherPurchaseHeader.LocationID;
                accPaymentHeader.Remark = invGiftVoucherPurchaseHeader.Remark;

                if (accPaymentHeader.AccPaymentHeaderID.Equals(0))
                    context.AccPaymentHeaders.Add(accPaymentHeader);
                else
                    context.Entry(accPaymentHeader).State = EntityState.Modified;

                context.SaveChanges();
                #endregion

                #region Account Entries
                AddAccGlTransactionDetails(invGiftVoucherPurchaseHeader, isTStatus);
                #endregion

                //context.InvGiftVoucherPurchaseHeaders.Add(AddInvGiftVoucherPurchaseDetails(invGiftVoucherPurchaseHeader, invGiftVoucherPurchaseDetailsTemp));
                DateTime t1 = DateTime.UtcNow;
                //context.SaveChanges();

                #region Update Master Vouchers
                //Update Voucher status on Voucher Master
                //if (invGiftVoucherPurchaseHeader.DocumentStatus.Equals(1))
                //{
                //    //update po
                //    UpdateInvGiftVoucherPurchaseOrderDetail(invGiftVoucherPurchaseHeader, invGiftVoucherPurchaseDetailsTemp);
                //    UpdateInvGiftVoucherMaster(invGiftVoucherPurchaseDetailsTemp);
                //}
                #endregion

                #region Process

                var parameter = new DbParameter[] 
                        { 
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvGiftVoucherPurchaseHeaderID", Value=invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invGiftVoucherPurchaseHeader.LocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invGiftVoucherPurchaseHeader.DocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentHeaderID", Value=invGiftVoucherPurchaseHeader.ReferenceDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentDocumentID", Value=invGiftVoucherPurchaseHeader.ReferenceDocumentDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invGiftVoucherPurchaseHeader.DocumentStatus},
                        };

                if (CommonService.ExecuteStoredProcedure("spInvGiftVoucherPurchaseNoteSave", parameter))
                {
                    transactionScope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }

                #endregion

                //transactionScope.Complete();
                //string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                //return true;
            }
        }

        public void UpdateInvGiftVoucherMaster(List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp)
        {
            foreach (InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp in invGiftVoucherPurchaseDetailsTemp)
            {
                context.InvGiftVoucherMasters.Update(pd => pd.InvGiftVoucherMasterID.Equals(invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID) && pd.VoucherNo.Equals(invGiftVoucherPurchaseDetailTemp.VoucherNo) && pd.VoucherSerial.Equals(invGiftVoucherPurchaseDetailTemp.VoucherSerial), pd => new InvGiftVoucherMaster { VoucherStatus = 2 });
                context.SaveChanges();
            }
        }

        public void UpdateInvGiftVoucherPurchaseOrderDetail(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp)
        {
            ////foreach (InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp in invGiftVoucherPurchaseDetailsTemp)
            ////{
            ////    context.InvPurchaseOrderDetails.Join(context.InvPurchaseOrderHeaders, )
            ////        .Update(pd => pd.InvGiftVoucherMasterID.Equals(invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID) && pd.VoucherNo.Equals(invGiftVoucherPurchaseDetailTemp.VoucherNo) && pd.VoucherSerial.Equals(invGiftVoucherPurchaseDetailTemp.VoucherSerial), pd => new InvGiftVoucherMaster { VoucherStatus = 2 });



            ////    employees.Join(customers, (employee) => employee.Name, (customer) => customer.Name, (employee, customer) =>
            ////    {
            ////        customer.ContactNo = employee.ContactNo;
            ////        return customer;
            ////    }).ToList();


            ////   context.InvGiftVoucherPurchaseOrderDetails
            ////        .Join(context.InvGiftVoucherPurchaseOrderHeaders.Where(b => b.UnitOfMeasureID.Equals(1)),
            ////                                                b => b.InvProductMasterID,
            ////                                                c => c.ProductID,
            ////                                                (b, c) => b).Where(c => c.IsDelete.Equals(false)).Select(b => b.ProductCode)
            ////    .Where(book => book.PageCount > 150)
            ////    .ForEach(book => {
            ////    book.Title += " (long)";
            ////    Console.WriteLine(book.Title);
            ////    });

            ////    context.SaveChanges();
            ////}
        }

        public void AddAccGlTransactionDetails(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, bool isTStatus)
        {
            AccGlTransactionService accGlTransactionService = new AccGlTransactionService();
            AccTransactionTypeService accTransactionTypeDetailService = new AccTransactionTypeService();

            AccGlTransactionHeader accGlTransactionHeader = new AccGlTransactionHeader();
            accGlTransactionHeader = accGlTransactionService.getAccGlTransactionHeader(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID, invGiftVoucherPurchaseHeader.DocumentID, invGiftVoucherPurchaseHeader.LocationID);
            if (accGlTransactionHeader == null)
            {accGlTransactionHeader = new AccGlTransactionHeader();}

            accGlTransactionHeader.Amount = invGiftVoucherPurchaseHeader.NetAmount;
            accGlTransactionHeader.CompanyID = invGiftVoucherPurchaseHeader.CompanyID;
            accGlTransactionHeader.CostCentreID = invGiftVoucherPurchaseHeader.CostCentreID;
            accGlTransactionHeader.CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate;
            accGlTransactionHeader.CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser;
            accGlTransactionHeader.DocumentDate = invGiftVoucherPurchaseHeader.DocumentDate;
            accGlTransactionHeader.DocumentID = invGiftVoucherPurchaseHeader.DocumentID;
            accGlTransactionHeader.DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus;
            accGlTransactionHeader.DocumentNo = invGiftVoucherPurchaseHeader.DocumentNo; //string.Empty;
            accGlTransactionHeader.GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID;
            accGlTransactionHeader.DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer;
            accGlTransactionHeader.IsUpLoad = false;
            accGlTransactionHeader.LocationID = invGiftVoucherPurchaseHeader.LocationID;
            accGlTransactionHeader.ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate;
            accGlTransactionHeader.ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser;
            accGlTransactionHeader.ReferenceDocumentDocumentID = invGiftVoucherPurchaseHeader.DocumentID;
            accGlTransactionHeader.ReferenceDocumentID = invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID;
            accGlTransactionHeader.ReferenceDocumentNo = invGiftVoucherPurchaseHeader.DocumentNo;
            accGlTransactionHeader.ReferenceID = invGiftVoucherPurchaseHeader.SupplierID;

            if (accGlTransactionHeader.AccGlTransactionHeaderID.Equals(0))
                accGlTransactionService.AddAccGlTransactionHeader(accGlTransactionHeader);
            else
                accGlTransactionService.UpdateAccGlTransactionHeader(accGlTransactionHeader);

            context.SaveChanges();

            context.Set<AccGlTransactionDetail>().Delete(context.AccGlTransactionDetails.Where(p => p.ReferenceDocumentID.Equals(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID) && p.LocationID.Equals(invGiftVoucherPurchaseHeader.LocationID) && p.ReferenceDocumentDocumentID.Equals(invGiftVoucherPurchaseHeader.DocumentID) && p.DocumentStatus.Equals(0)).AsQueryable());
            AccTransactionTypeDetail accTransactionTypeDetail = new AccTransactionTypeDetail();

            //Have to read Non Trade Control A/C
            accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionID(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmLogisticSupplier").DocumentID);

            List<AccGlTransactionDetail> accGlTransactionDetailTempList = new List<AccGlTransactionDetail>();

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
                    LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                    LedgerSerial = accGlTransactionHeader.DocumentNo,
                    DocumentDate = accGlTransactionHeader.DocumentDate,
                    PaymentDate = accGlTransactionHeader.DocumentDate,
                    ReferenceID = accGlTransactionHeader.ReferenceID,
                    DrCr = accTransactionTypeDetail.DrCr,
                    Amount = invGiftVoucherPurchaseHeader.NetAmount,
                    ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                    ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                    ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                    TransactionDefinitionId = 1,
                    ChequeNo = string.Empty,
                    Reference = invGiftVoucherPurchaseHeader.ReferenceNo,
                    Remark = invGiftVoucherPurchaseHeader.Remark,
                    DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus,
                    IsUpLoad = isTStatus,
                    GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID,
                    CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser,
                    CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate,
                    ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser,
                    ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate,
                    DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer,
                };
                accGlTransactionDetailTempList.Add(te);
            }

            int definitionType = 0;
            LookUpReferenceService lookUpReferenceService = new LookUpReferenceService();
            ReferenceType referenceType;
            referenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TransactionDefinition).ToString(), 1);
            if (referenceType != null)
            {
                definitionType = referenceType.LookupKey;
            }

            accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, definitionType);

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
                    LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                    LedgerSerial = accGlTransactionHeader.DocumentNo,
                    DocumentDate = accGlTransactionHeader.DocumentDate,
                    PaymentDate = accGlTransactionHeader.DocumentDate,
                    ReferenceID = accGlTransactionHeader.ReferenceID,
                    DrCr = accTransactionTypeDetail.DrCr,
                    Amount = (invGiftVoucherPurchaseHeader.NetAmount + invGiftVoucherPurchaseHeader.DiscountAmount),
                    ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                    ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                    ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                    TransactionDefinitionId = accTransactionTypeDetail.TransactionDefinition,
                    ChequeNo = string.Empty,
                    Reference = invGiftVoucherPurchaseHeader.ReferenceNo,
                    Remark = invGiftVoucherPurchaseHeader.Remark,
                    DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus,
                    IsUpLoad = isTStatus,
                    GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID,
                    CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser,
                    CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate,
                    ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser,
                    ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate,
                    DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer,
                };
                accGlTransactionDetailTempList.Add(te);
            }

            referenceType = lookUpReferenceService.GetLookUpReferenceByKey(((int)LookUpReference.TransactionDefinition).ToString(), 2);
            if (referenceType != null)
            {
                definitionType = referenceType.LookupKey;
            }

            accTransactionTypeDetail = accTransactionTypeDetailService.GetAllAccTransactionTypeDetailsByTransactionIDDefinition(AutoGenerateInfoService.GetAutoGenerateInfoByForm("FrmGiftVoucherGoodsReceivedNote").DocumentID, definitionType);

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
                    LedgerID = accTransactionTypeDetail.AccLedgerAccountID,
                    LedgerSerial = accGlTransactionHeader.DocumentNo,
                    DocumentDate = accGlTransactionHeader.DocumentDate,
                    PaymentDate = accGlTransactionHeader.DocumentDate,
                    ReferenceID = accGlTransactionHeader.ReferenceID,
                    DrCr = accTransactionTypeDetail.DrCr,
                    Amount = invGiftVoucherPurchaseHeader.DiscountAmount,
                    ReferenceDocumentDocumentID = accGlTransactionHeader.ReferenceDocumentDocumentID,
                    ReferenceDocumentID = accGlTransactionHeader.ReferenceDocumentID,
                    ReferenceDocumentNo = accGlTransactionHeader.ReferenceDocumentNo,
                    TransactionDefinitionId = accTransactionTypeDetail.TransactionDefinition,
                    ChequeNo = string.Empty,
                    Reference = invGiftVoucherPurchaseHeader.ReferenceNo,
                    Remark = invGiftVoucherPurchaseHeader.Remark,
                    DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus,
                    IsUpLoad = isTStatus,
                    GroupOfCompanyID = invGiftVoucherPurchaseHeader.GroupOfCompanyID,
                    CreatedUser = invGiftVoucherPurchaseHeader.CreatedUser,
                    CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate,
                    ModifiedUser = invGiftVoucherPurchaseHeader.ModifiedUser,
                    ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate,
                    DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer,
                };
                accGlTransactionDetailTempList.Add(te);
            }

            var glEntriesSaveQuery = (from ae in accGlTransactionDetailTempList
                                         select new
                                         {
                                             AccGlTransactionDetailID = 0,
                                             AccGlTransactionHeaderID = ae.AccGlTransactionHeaderID,
                                             CompanyID = ae.CompanyID,
                                             LocationID = ae.LocationID,
                                             CostCentreID = ae.CostCentreID,
                                             DocumentNo = ae.DocumentNo,
                                             LedgerID = ae.LedgerID,
                                             LedgerSerial = ae.LedgerSerial,
                                             DocumentID = ae.DocumentID,
                                             DocumentDate = ae.DocumentDate,
                                             PaymentDate = ae.PaymentDate,
                                             ReferenceID = ae.ReferenceID,
                                             DrCr = ae.DrCr,
                                             Amount = ae.Amount,
                                             TransactionTypeId = ae.TransactionTypeId,
                                             ReferenceDocumentID = ae.ReferenceDocumentID,
                                             ReferenceDocumentDocumentID = ae.ReferenceDocumentDocumentID,                                             
                                             ReferenceDocumentNo = ae.ReferenceDocumentNo,
                                             TransactionDefinitionId = ae.TransactionDefinitionId,
                                             ChequeNo = ae.ChequeNo,
                                             Reference = ae.Reference,
                                             Remark = ae.Remark,
                                             DocumentStatus = ae.DocumentStatus,
                                             IsUpLoad = ae.IsUpLoad,
                                             GroupOfCompanyID = ae.GroupOfCompanyID,
                                             CreatedUser = ae.CreatedUser,
                                             CreatedDate = ae.CreatedDate,
                                             ModifiedUser = ae.ModifiedUser,
                                             ModifiedDate = ae.ModifiedDate,
                                             DataTransfer = ae.DataTransfer
                                         }).ToList();

            CommonService.BulkInsert(Common.LINQToDataTable(glEntriesSaveQuery), "AccGlTransactionDetail");
        }

        /// <summary>
        /// Create InvGiftVoucherPurchaseDetail list using InvGiftVoucherPurchaseDetailTemp
        /// </summary>
        /// <param name="invGiftVoucherPurchaseHeader"></param>
        /// <param name="invGiftVoucherPurchaseDetailsTemp"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseHeader AddInvGiftVoucherPurchaseDetails(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp)
        {
            DateTime t1 = DateTime.UtcNow;
            // InvGiftVoucherPurchaseDetail list for InvGiftVoucherPurchaseHeader 
            invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseDetails = new List<InvGiftVoucherPurchaseDetail>();
            foreach (InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp in invGiftVoucherPurchaseDetailsTemp)
            {
                InvGiftVoucherPurchaseDetail invGiftVoucherPurchaseDetail = new InvGiftVoucherPurchaseDetail();

                invGiftVoucherPurchaseDetail.DocumentStatus = invGiftVoucherPurchaseHeader.DocumentStatus;
                invGiftVoucherPurchaseDetail.DocumentNo = invGiftVoucherPurchaseHeader.DocumentNo;
                invGiftVoucherPurchaseDetail.DocumentDate = invGiftVoucherPurchaseHeader.DocumentDate;
                invGiftVoucherPurchaseDetail.CompanyID = invGiftVoucherPurchaseHeader.CompanyID;
                invGiftVoucherPurchaseDetail.LocationID = invGiftVoucherPurchaseHeader.LocationID;
                invGiftVoucherPurchaseDetail.CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate;
                invGiftVoucherPurchaseDetail.DocumentID = invGiftVoucherPurchaseHeader.DocumentID;
                invGiftVoucherPurchaseDetail.DataTransfer = invGiftVoucherPurchaseHeader.DataTransfer;
                invGiftVoucherPurchaseDetail.CreatedDate = invGiftVoucherPurchaseHeader.CreatedDate;
                invGiftVoucherPurchaseDetail.ModifiedDate = invGiftVoucherPurchaseHeader.ModifiedDate;

                invGiftVoucherPurchaseDetail.LineNo = invGiftVoucherPurchaseDetailTemp.LineNo;
                invGiftVoucherPurchaseDetail.InvGiftVoucherMasterID = invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID;
                //invGiftVoucherPurchaseDetail.ProductID = invGiftVoucherPurchaseDetailTemp.ProductID;
                //invGiftVoucherPurchaseDetail.ProductCode = invGiftVoucherPurchaseDetailTemp.ProductCode;
                //invGiftVoucherPurchaseDetail.UnitOfMeasureID = invGiftVoucherPurchaseDetailTemp.UnitOfMeasureID;
                //invGiftVoucherPurchaseDetail.BaseUnitID = invGiftVoucherPurchaseDetailTemp.BaseUnitID;
                //invGiftVoucherPurchaseDetail.UnitOfMeasure = invGiftVoucherPurchaseDetailTemp.UnitOfMeasure;
                //invGiftVoucherPurchaseDetail.ConvertFactor = invGiftVoucherPurchaseDetailTemp.ConvertFactor;
                //invGiftVoucherPurchaseDetail.OrderQty = invGiftVoucherPurchaseDetailTemp.OrderQty;
                //invGiftVoucherPurchaseDetail.Qty = invGiftVoucherPurchaseDetailTemp.Qty;
                //invGiftVoucherPurchaseDetail.FreeQty = invGiftVoucherPurchaseDetailTemp.FreeQty;
                //invGiftVoucherPurchaseDetail.CurrentQty = invGiftVoucherPurchaseDetailTemp.CurrentQty;
                //invGiftVoucherPurchaseDetail.CostPrice = invGiftVoucherPurchaseDetailTemp.CostPrice;
                //invGiftVoucherPurchaseDetail.SellingPrice = invGiftVoucherPurchaseDetailTemp.SellingPrice;
                //invGiftVoucherPurchaseDetail.GrossAmount = invGiftVoucherPurchaseDetailTemp.GrossAmount;
                //invGiftVoucherPurchaseDetail.DiscountPercentage = invGiftVoucherPurchaseDetailTemp.DiscountPercentage;
                //invGiftVoucherPurchaseDetail.DiscountAmount = invGiftVoucherPurchaseDetailTemp.DiscountAmount;
                //invGiftVoucherPurchaseDetail.SubTotalDiscount = invGiftVoucherPurchaseDetailTemp.SubTotalDiscount;
                //invGiftVoucherPurchaseDetail.TaxAmount1 = invGiftVoucherPurchaseDetailTemp.TaxAmount1;
                //invGiftVoucherPurchaseDetail.TaxAmount2 = invGiftVoucherPurchaseDetailTemp.TaxAmount2;
                //invGiftVoucherPurchaseDetail.TaxAmount3 = invGiftVoucherPurchaseDetailTemp.TaxAmount3;
                //invGiftVoucherPurchaseDetail.TaxAmount4 = invGiftVoucherPurchaseDetailTemp.TaxAmount4;
                //invGiftVoucherPurchaseDetail.TaxAmount5 = invGiftVoucherPurchaseDetailTemp.TaxAmount5;
                //invGiftVoucherPurchaseDetail.NetAmount = invGiftVoucherPurchaseDetailTemp.NetAmount;


                invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseDetails.Add(invGiftVoucherPurchaseDetail);
            }
            string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
            return invGiftVoucherPurchaseHeader;
        }

        public List<InvGiftVoucherPurchaseDetailTemp> GetAllPurchaseDetail(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp;

            List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailTempList = new List<InvGiftVoucherPurchaseDetailTemp>();

            var giftVoucherPurchaseDetailTemp = (from bm in context.InvGiftVoucherMasters
                                      join pd in context.InvGiftVoucherPurchaseDetails on bm.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                      where pd.LocationID.Equals(invGiftVoucherPurchaseHeader.LocationID) && pd.CompanyID.Equals(invGiftVoucherPurchaseHeader.CompanyID)
                                      && pd.DocumentID.Equals(invGiftVoucherPurchaseHeader.DocumentID) && pd.InvGiftVoucherPurchaseHeaderID.Equals(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID)
                                      select new
                                      {
                                          invGiftVoucherPurchaseHeader.DocumentNo,
                                          pd.DocumentID,
                                          pd.DocumentDate,
                                          pd.DocumentStatus,
                                          invGiftVoucherPurchaseHeader.DiscountAmount,
                                          invGiftVoucherPurchaseHeader.DiscountPercentage,
                                          invGiftVoucherPurchaseHeader.PartyInvoiceNo,
                                          invGiftVoucherPurchaseHeader.PartyInvoiceDate,
                                          invGiftVoucherPurchaseHeader.DispatchNo,
                                          invGiftVoucherPurchaseHeader.DispatchDate,
                                          invGiftVoucherPurchaseHeader.GrossAmount,
                                          pd.VoucherAmount,
                                          pd.LineNo,
                                          pd.LocationID,
                                          invGiftVoucherPurchaseHeader.NetAmount,
                                          bm.VoucherNo,
                                          bm.InvGiftVoucherMasterID,
                                          bm.InvGiftVoucherBookCodeID,
                                          bm.InvGiftVoucherGroupID,
                                          bm.VoucherSerial,
                                          pd.NumberOfCount,
                                          pd.VoucherType
                                      }).ToArray();

            foreach (var tempGiftVoucher in giftVoucherPurchaseDetailTemp)
            {
                invGiftVoucherPurchaseDetailTemp = new InvGiftVoucherPurchaseDetailTemp();
                invGiftVoucherPurchaseDetailTemp.DocumentNo = tempGiftVoucher.DocumentNo;
                invGiftVoucherPurchaseDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                invGiftVoucherPurchaseDetailTemp.DocumentDate = tempGiftVoucher.DocumentDate;
                invGiftVoucherPurchaseDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                invGiftVoucherPurchaseDetailTemp.DiscountAmount = tempGiftVoucher.DiscountAmount;
                invGiftVoucherPurchaseDetailTemp.DiscountPercentage = tempGiftVoucher.DiscountPercentage;
                invGiftVoucherPurchaseDetailTemp.PartyInvoiceDate = tempGiftVoucher.PartyInvoiceDate;
                invGiftVoucherPurchaseDetailTemp.PartyInvoiceNo = tempGiftVoucher.PartyInvoiceNo;
                invGiftVoucherPurchaseDetailTemp.DispatchNo = tempGiftVoucher.DispatchNo;
                invGiftVoucherPurchaseDetailTemp.DispatchDate = tempGiftVoucher.DispatchDate;
                invGiftVoucherPurchaseDetailTemp.GrossAmount = tempGiftVoucher.GrossAmount;
                invGiftVoucherPurchaseDetailTemp.LineNo = tempGiftVoucher.LineNo;
                invGiftVoucherPurchaseDetailTemp.LocationID = tempGiftVoucher.LocationID;
                invGiftVoucherPurchaseDetailTemp.NetAmount = tempGiftVoucher.NetAmount;
                invGiftVoucherPurchaseDetailTemp.VoucherNo = tempGiftVoucher.VoucherNo;
                invGiftVoucherPurchaseDetailTemp.VoucherSerial = tempGiftVoucher.VoucherSerial;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID = tempGiftVoucher.InvGiftVoucherMasterID;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherBookCodeID = tempGiftVoucher.InvGiftVoucherBookCodeID;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherGroupID = tempGiftVoucher.InvGiftVoucherGroupID;
                invGiftVoucherPurchaseDetailTemp.NumberOfCount = tempGiftVoucher.NumberOfCount;
                invGiftVoucherPurchaseDetailTemp.VoucherType = tempGiftVoucher.VoucherType;
                if (tempGiftVoucher.VoucherType == 1)
                {
                    invGiftVoucherPurchaseDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseDetailTemp.VoucherAmount = tempGiftVoucher.VoucherAmount;
                }
                else
                {
                    invGiftVoucherPurchaseDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseDetailTemp.GiftVoucherPercentage = tempGiftVoucher.VoucherAmount;
                }
                invGiftVoucherPurchaseDetailTempList.Add(invGiftVoucherPurchaseDetailTemp);
            }

            return invGiftVoucherPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvGiftVoucherPurchaseDetailTemp> GetGiftVoucherPurchaseOrderDetail(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader, string documentNo)
        {
            InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp;

            List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailTempList = new List<InvGiftVoucherPurchaseDetailTemp>();

            var giftVoucherPurchaseOrderDetailTemp = (from bm in context.InvGiftVoucherMasters
                                                 join pd in context.InvGiftVoucherPurchaseOrderDetails on bm.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                                      where pd.IsPurchase == false && pd.DocumentStatus == 1 && pd.LocationID.Equals(invGiftVoucherPurchaseOrderHeader.LocationID) && pd.CompanyID.Equals(invGiftVoucherPurchaseOrderHeader.CompanyID)
                                                 && pd.DocumentID.Equals(invGiftVoucherPurchaseOrderHeader.DocumentID) && pd.InvGiftVoucherPurchaseOrderHeaderID.Equals(invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID)
                                                 
                                                 select new
                                                 {
                                                     pd.InvGiftVoucherPurchaseOrderDetailID,
                                                     pd.GiftVoucherPurchaseOrderDetailID,
                                                     invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID,
                                                     pd.CompanyID,
                                                     pd.LocationID,
                                                     invGiftVoucherPurchaseOrderHeader.DocumentNo,
                                                     pd.DocumentID,
                                                     invGiftVoucherPurchaseOrderHeader.DocumentDate,
                                                     invGiftVoucherPurchaseOrderHeader.DiscountAmount,
                                                     invGiftVoucherPurchaseOrderHeader.DiscountPercentage,
                                                     invGiftVoucherPurchaseOrderHeader.GrossAmount,
                                                     pd.VoucherAmount,
                                                     pd.LineNo,
                                                     invGiftVoucherPurchaseOrderHeader.NetAmount,
                                                     bm.VoucherNo,
                                                     bm.InvGiftVoucherMasterID,
                                                     bm.InvGiftVoucherBookCodeID,
                                                     bm.InvGiftVoucherGroupID,
                                                     bm.VoucherSerial,
                                                     pd.NumberOfCount,
                                                     pd.VoucherType
                                                 }).ToArray();

            foreach (var tempGiftVoucher in giftVoucherPurchaseOrderDetailTemp)
            {
                invGiftVoucherPurchaseDetailTemp = new InvGiftVoucherPurchaseDetailTemp();
                invGiftVoucherPurchaseDetailTemp.DocumentNo = documentNo;
                invGiftVoucherPurchaseDetailTemp.DiscountAmount = tempGiftVoucher.DiscountAmount;
                invGiftVoucherPurchaseDetailTemp.DiscountPercentage = tempGiftVoucher.DiscountPercentage;
                invGiftVoucherPurchaseDetailTemp.GrossAmount = tempGiftVoucher.GrossAmount;
                invGiftVoucherPurchaseDetailTemp.LineNo = tempGiftVoucher.LineNo;
                invGiftVoucherPurchaseDetailTemp.LocationID = tempGiftVoucher.LocationID;
                invGiftVoucherPurchaseDetailTemp.NetAmount = tempGiftVoucher.NetAmount;
                invGiftVoucherPurchaseDetailTemp.VoucherNo = tempGiftVoucher.VoucherNo;
                invGiftVoucherPurchaseDetailTemp.VoucherSerial = tempGiftVoucher.VoucherSerial;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID = tempGiftVoucher.InvGiftVoucherMasterID;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherBookCodeID = tempGiftVoucher.InvGiftVoucherBookCodeID;
                invGiftVoucherPurchaseDetailTemp.InvGiftVoucherGroupID = tempGiftVoucher.InvGiftVoucherGroupID;
                invGiftVoucherPurchaseDetailTemp.NumberOfCount = tempGiftVoucher.NumberOfCount;
                invGiftVoucherPurchaseDetailTemp.VoucherType = tempGiftVoucher.VoucherType;
                if (tempGiftVoucher.VoucherType == 1)
                {
                    invGiftVoucherPurchaseDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseDetailTemp.VoucherAmount = tempGiftVoucher.VoucherAmount;
                }
                else
                {
                    invGiftVoucherPurchaseDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseDetailTemp.GiftVoucherPercentage = tempGiftVoucher.VoucherAmount;
                }
                invGiftVoucherPurchaseDetailTempList.Add(invGiftVoucherPurchaseDetailTemp);
            }

            return invGiftVoucherPurchaseDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public InvGiftVoucherPurchaseHeader GetGiftVoucherPurchaseHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvGiftVoucherPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Get Saved InvPurchaserHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseHeader GetInvGiftVoucherPurchaseHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvGiftVoucherPurchaseHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.InvGiftVoucherPurchaseHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public InvGiftVoucherPurchaseHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.InvGiftVoucherPurchaseHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvGiftVoucherPurchaseHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.InvGiftVoucherPurchaseHeaders.Where(po => po.InvGiftVoucherPurchaseHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }
        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.InvGiftVoucherPurchaseHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        public DataTable GetPendingPurchaseDocuments()
        {
            var sql = (from poh in context.InvGiftVoucherPurchaseHeaders
                       //join pod in context.InvGiftVoucherPurchaseDetails on poh.InvGiftVoucherPurchaseHeaderID equals pod.InvGiftVoucherPurchaseHeaderID
                       join l in context.Locations on poh.LocationID equals l.LocationID
                       join s in context.LgsSuppliers on poh.SupplierID equals s.LgsSupplierID
                       where poh.DocumentStatus == 0
                       select new
                       {
                           poh.DocumentNo,
                           poh.DocumentDate,
                           DocumentLocation = l.LocationName,
                           SupplierName = s.SupplierName
                       }).ToArray();

            return sql.ToDataTable();
        }

        /// <summary>
        ///  Get all document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetAllDocumentNumbers()
        {
            List<string> purchaseDocumentList = new List<string>();
            purchaseDocumentList = context.InvGiftVoucherPurchaseHeaders.Select(ph => ph.DocumentNo).ToList();
            string[] purchaseDocumentCodes = purchaseDocumentList.ToArray();
            return purchaseDocumentCodes;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvGiftVoucherPurchaseHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public string[] GetAllDocumentNumbersToTransfer(int locationID)
        {
           // List<string> documentNoList = context.InvGiftVoucherPurchaseHeaders.Where(q => q.LocationID == locationID && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();


            var query = (from ph in context.InvGiftVoucherPurchaseHeaders
                         join pd in context.InvGiftVoucherPurchaseDetails on ph.InvGiftVoucherPurchaseHeaderID equals pd.InvGiftVoucherPurchaseHeaderID
                         where ph.LocationID == locationID && pd.IsTransfer == false && ph.DocumentStatus.Equals(1)
                         group pd by new
                         {
                             ph.DocumentNo,
                         } into poGroup
                         orderby poGroup.Key.DocumentNo descending 
                         select new
                         {
                             DocumentNo = poGroup.Key.DocumentNo,
                         }).ToList();

            List<string> documentNoList = query.Select(a => a.DocumentNo).ToList();

            return documentNoList.ToArray();
        }

        public DataTable GetPendingPurchaseDocumentsToTransfer()
        {
            var sql = (from ph in context.InvGiftVoucherPurchaseHeaders
                       join pd in context.InvGiftVoucherPurchaseDetails on ph.InvGiftVoucherPurchaseHeaderID equals pd.InvGiftVoucherPurchaseHeaderID
                       join l in context.Locations on ph.LocationID equals l.LocationID
                       join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                       where ph.DocumentStatus == 1 && pd.IsTransfer == false
                       group pd by new
                         {
                            ph.DocumentNo,
                            ph.DocumentDate,
                            DocumentLocation = l.LocationName,
                            SupplierName = s.SupplierName
                         } into poGroup
                         orderby poGroup.Key.DocumentNo descending 
                         select new
                         {
                            DocumentNo = poGroup.Key.DocumentNo,
                            DocumentDate= poGroup.Key.DocumentDate,
                            DocumentLocation = poGroup.Key.DocumentLocation,
                            SupplierName = poGroup.Key.SupplierName
                         }).ToArray();

            return sql.ToDataTable();
        }

        public string[] GetVoucherSerialByBookID(long bookID)
        {
            List<string> availableVoucherCount = new List<string>();
            availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherSerial).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public string[] GetVoucherSerialByRecallPO(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader, long bookID)
        {
           List<string> availableVoucherCount = new List<string>();
           
           availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                    join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                    join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                    where pd.VoucherType == gv.VoucherType && gv.InvGiftVoucherBookCodeID == bookID 
                                    && ph.InvGiftVoucherPurchaseOrderHeaderID == invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                    && pd.IsPurchase == false
                                    select  gv.VoucherSerial).ToList();

           string[] vouchers = availableVoucherCount.ToArray();
           return vouchers;
        }

        public string[] GetVoucherNoByBookID(long bookID)
        {
            List<string> availableVoucherCount = new List<string>();
            availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNo).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public string[] GetVoucherNoByRecallPO(InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader, long bookID)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                     join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                     where pd.VoucherType == gv.VoucherType
                                     && gv.InvGiftVoucherBookCodeID == bookID 
                                     && ph.InvGiftVoucherPurchaseOrderHeaderID == existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                     && pd.IsPurchase == false
                                     select gv.VoucherNo).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public string[] GetRecallPOInvGiftVoucherBookCodesByVoucherType(InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherBookCodes
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                                     join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && ph.InvGiftVoucherPurchaseOrderHeaderID == existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                     && pd.IsPurchase == false
                                     group gv by new
                                       {
                                           gv.InvGiftVoucherGroupID,
                                           gb.BookCode
                                       } into poGroup
                                       select 
                                           poGroup.Key.BookCode
                                       ).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public string[] GetRecallPOInvGiftVoucherBookNamesByVoucherType(InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherBookCodes
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                                     join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && ph.InvGiftVoucherPurchaseOrderHeaderID == existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                     && pd.IsPurchase == false
                                     group gv by new
                                     {
                                         gv.InvGiftVoucherGroupID,
                                         gb.BookName
                                     } into poGroup
                                     select
                                         poGroup.Key.BookName
                                     ).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public string[] GetRecallPOInvGiftVoucherGroupCodes(InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherGroups
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                                     join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && ph.InvGiftVoucherPurchaseOrderHeaderID == existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                     && pd.IsPurchase == false
                                     group gv by new
                                     {
                                         gv.InvGiftVoucherGroupID,
                                         gb.GiftVoucherGroupCode
                                     } into poGroup
                                     select
                                         poGroup.Key.GiftVoucherGroupCode
                                     ).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToGRNByPOVoucherSerial(long referenceDocumentID, int locationID, string voucherSerial)
        {
            //return context.InvGiftVoucherPurchaseOrderDetails.Where(s => s.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID && s.LocationID == locationID)
                //s => s.VoucherSerial.CompareTo(voucherSerial) == 0 && s.VoucherStatus == 1 && s.IsDelete == false)
           //     .Join(context.InvGiftVoucherMasters, m=>m.InvGiftVoucherMasterID, m.VoucherSerial,
           //     )
         //      .FirstOrDefault();

            return (from pm in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseOrderDetails on pm.InvGiftVoucherMasterID equals
                        pd.InvGiftVoucherMasterID
                    join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID
                        equals ph.InvGiftVoucherPurchaseOrderHeaderID
                    where
                        ph.LocationID.Equals(locationID) && ph.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID && 
                        pm.VoucherSerial.CompareTo(voucherSerial) == 0
                        
                    select pm).AsEnumerable().FirstOrDefault();

        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToGRNByPOVoucherNo(long referenceDocumentID, int locationID, string voucherNo)
        {
            //return context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).FirstOrDefault();

            return (from pm in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseOrderDetails on pm.InvGiftVoucherMasterID equals
                        pd.InvGiftVoucherMasterID
                    join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID
                        equals ph.InvGiftVoucherPurchaseOrderHeaderID
                    where
                        ph.LocationID.Equals(locationID) && ph.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID &&
                        pm.VoucherNo.CompareTo(voucherNo) == 0
                    select pm).AsEnumerable().FirstOrDefault();
        }

        public string[] GetRecallPOInvGiftVoucherGroupNames(InvGiftVoucherPurchaseOrderHeader existingInvGiftVoucherPurchaseOrderHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherGroups
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                                     join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && ph.InvGiftVoucherPurchaseOrderHeaderID == existingInvGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                                      && pd.IsPurchase == false
                                     group gv by new
                                     {
                                         gv.InvGiftVoucherGroupID,
                                         gb.GiftVoucherGroupName
                                     } into poGroup
                                     select
                                         poGroup.Key.GiftVoucherGroupName
                                       ).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public DataTable GetAllActiveGiftVoucherRecallPOGroupsDataTable(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader)
        {
            DataTable tblGiftVoucherGroups = new DataTable();
            var query = (from gb in context.InvGiftVoucherGroups
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsPurchase == false
                         && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                         group gv by new
                         {
                             gv.InvGiftVoucherGroupID,
                             gb.GiftVoucherGroupCode,
                             gb.GiftVoucherGroupName
                         } into poGroup
                         select new
                             {
                                 poGroup.Key.GiftVoucherGroupCode,
                                 poGroup.Key.GiftVoucherGroupName
                             }
                     
                         );
            return tblGiftVoucherGroups = Common.LINQToDataTable(query);
        }

        public DataTable GetAllActiveGiftVoucherMasterRecallPOBooksDataTable(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader)
        {
            DataTable tblGiftVoucherMasterBooks = new DataTable();
            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                         join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsPurchase == false && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                         group gv by new
                         {
                             gb.BookCode,
                             gb.BookName,
                             gb.InvGiftVoucherGroup.GiftVoucherGroupCode,
                             gb.InvGiftVoucherGroup.GiftVoucherGroupName,
                             gb.GiftVoucherValue,
                             gb.PageCount
                         } into poGroup
                         select new
                         {
                             poGroup.Key.BookCode,
                             poGroup.Key.BookName,
                             poGroup.Key.GiftVoucherGroupCode,
                             poGroup.Key.GiftVoucherGroupName,
                             poGroup.Key.GiftVoucherValue,
                             poGroup.Key.PageCount
                         });
            return tblGiftVoucherMasterBooks = Common.LINQToDataTable(query);
        }

        public long GetVoucherQtyByBookID(int bookID, string fromNo, string toNo, int voucherStatus)
        {
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(fromNo) >= 0 && s.VoucherSerial.CompareTo(toNo) <= 0) && s.VoucherStatus == 1 && s.IsDelete == false).Count();
                }
                else
                {
                    bookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNo.CompareTo(fromNo) >= 0 && s.VoucherNo.CompareTo(toNo) <= 0) && s.VoucherStatus == 1 && s.IsDelete == false).Count();

                    availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                }
            }

            return availableBookCount;
        }

        public List<InvGiftVoucherMaster> GetGeneratedPOGiftVouchersByBookIDWithQtyForPurchase(int bookID, long invGiftVOucherPurchaseOrderHeaderID, int voucherQty)
        {
            // return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).Take(voucherQty).ToList();

            return (from gv in context.InvGiftVoucherMasters
             join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID
                 equals pd.InvGiftVoucherMasterID
             where
                 pd.VoucherType == gv.VoucherType && gv.IsDelete == false &&
                 gv.InvGiftVoucherBookCodeID == bookID && pd.IsPurchase == false && 
                 pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVOucherPurchaseOrderHeaderID
                    orderby gv.VoucherSerial
             select
                 gv
            ).Take(voucherQty).ToList();
        }


        public List<InvGiftVoucherMaster> GetGeneratedPOGiftVouchersByBookIDWithVoucherNoRangeForPurchase(int bookID, long invGiftVOucherPurchaseOrderHeaderID, string voucherNoFrom, string voucherNoTo)
        {
           // return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNo.CompareTo(voucherNoFrom) >= 0
           // && u.VoucherNo.CompareTo(voucherNoTo) <= 0) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).ToList();

            return (from gv in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID
                        equals pd.InvGiftVoucherMasterID
                    where
                        pd.VoucherType == gv.VoucherType && gv.IsDelete == false && (gv.VoucherNo.CompareTo(voucherNoFrom) >= 0
            && gv.VoucherNo.CompareTo(voucherNoTo) <= 0) &&
                        gv.InvGiftVoucherBookCodeID == bookID && pd.IsPurchase == false && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVOucherPurchaseOrderHeaderID
                    orderby gv.VoucherSerial
                    select
                        gv
            ).ToList();
        }

        public List<InvGiftVoucherMaster> GetGeneratedPOGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(int bookID, long invGiftVOucherPurchaseOrderHeaderID, string voucherSerialFrom, string voucherSerialTo)
        {
           // return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && 
            //(u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).ToList();

            return (from gv in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID
                        equals pd.InvGiftVoucherMasterID
                    where
                        pd.VoucherType == gv.VoucherType && gv.IsDelete == false && 
                        (gv.VoucherSerial.CompareTo(voucherSerialFrom) >= 0
                            && gv.VoucherSerial.CompareTo(voucherSerialTo) <= 0) &&
                        gv.InvGiftVoucherBookCodeID == bookID && pd.IsPurchase == false && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVOucherPurchaseOrderHeaderID
                    orderby gv.VoucherSerial
                    select
                        gv
            ).ToList();
        }

        public DataTable GetAllActiveGiftVoucherSerialsDataTableByVoucherType(int voucherType, int voucherGroupID, int voucherBookID)
        {
            DataTable tblGiftVoucherMasters = new DataTable();
            var query = from u in context.InvGiftVoucherMasters
                        join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                        join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                        where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID && u.InvGiftVoucherBookCodeID == voucherBookID
                        && u.VoucherStatus == 1)
                        select new { u.VoucherSerial, u.VoucherNo, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue };
            return tblGiftVoucherMasters = query.ToDataTable();
        }

        

        public DataTable GetAllActiveGiftVoucherNosDataTableByVoucherType(int voucherType, int voucherGroupID, int voucherBookID)
        {
            DataTable tblGiftVoucherMasters = new DataTable();
            var query = from u in context.InvGiftVoucherMasters
                        join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                        join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                        where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID && u.InvGiftVoucherBookCodeID == voucherBookID
                        && u.VoucherStatus == 1)
                        select new { u.VoucherNo, u.VoucherSerial, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue, u.PageCount };
            return tblGiftVoucherMasters = query.ToDataTable();
        }

        public DataTable GetAllActiveGiftVoucherMasterRecallPOVoucherSerialsDataTable(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader)
        {
            DataTable tblGiftVoucherMasters = new DataTable();
           
            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                         group gv by new
                         {
                             gv.VoucherSerial,
                             gv.VoucherNo,
                             gv.InvGiftVoucherGroup.GiftVoucherGroupCode,
                             gv.InvGiftVoucherGroup.GiftVoucherGroupName,
                             gv.InvGiftVoucherBookCode.BookCode,
                             gv.InvGiftVoucherBookCode.BookName,
                             gv.GiftVoucherValue
                         } into poGroup
                         select new
                         {
                             poGroup.Key.VoucherSerial,
                             poGroup.Key.VoucherNo,
                             poGroup.Key.GiftVoucherGroupCode,
                             poGroup.Key.GiftVoucherGroupName,
                             poGroup.Key.BookCode,
                             poGroup.Key.BookName,
                             poGroup.Key.GiftVoucherValue
                         }).AsQueryable();
            return tblGiftVoucherMasters = query.ToDataTable();
        }

        public DataTable GetAllActiveGiftVoucherMasterRecallPOVoucherNosDataTable(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader)
        {
            DataTable tblGiftVoucherMasters = new DataTable();

            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseOrderDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.InvGiftVoucherPurchaseOrderHeaderID == invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID
                         group gv by new
                         {
                             gv.VoucherNo,
                             gv.VoucherSerial,
                             gv.InvGiftVoucherGroup.GiftVoucherGroupCode,
                             gv.InvGiftVoucherGroup.GiftVoucherGroupName,
                             gv.InvGiftVoucherBookCode.BookCode,
                             gv.InvGiftVoucherBookCode.BookName,
                             gv.GiftVoucherValue
                         } into poGroup
                         select new
                         {
                             poGroup.Key.VoucherNo,
                             poGroup.Key.VoucherSerial,
                             poGroup.Key.GiftVoucherGroupCode,
                             poGroup.Key.GiftVoucherGroupName,
                             poGroup.Key.BookCode,
                             poGroup.Key.BookName,
                             poGroup.Key.GiftVoucherValue
                         });
            return tblGiftVoucherMasters = query.ToDataTable();
        }

        public DataSet GetPurchaseTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.InvGiftVoucherPurchaseHeaders
                        join pd in context.InvGiftVoucherPurchaseDetails on ph.InvGiftVoucherPurchaseHeaderID equals pd.InvGiftVoucherPurchaseHeaderID
                        join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                        join gg in context.InvGiftVoucherGroups on pm.InvGiftVoucherGroupID equals gg.InvGiftVoucherGroupID
                        join gb in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                        join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        group new { pm, pd, py } by new
                        {
                            ph.DocumentNo,
                            ph.InvGiftVoucherPurchaseHeaderID,
                            ph.DocumentDate,
                            s.SupplierCode,
                            s.SupplierName,
                            ph.Remark,
                            pd.VoucherType,
                            ph.ReferenceNo,
                            py.PaymentMethodName,
                            //ph.ExpectedDate,
                            l.LocationCode,
                            l.LocationName,
                            pd.VoucherAmount,
                            // pm.VoucherNo,
                            // pm.VoucherSerial,
                            // pd.InvGiftVoucherMasterID,
                            pm.InvGiftVoucherGroupID,
                            gg.GiftVoucherGroupCode,
                            gg.GiftVoucherGroupName,
                            pm.InvGiftVoucherBookCodeID,
                            gb.BookCode,
                            gb.BookName,
                            pd.NumberOfCount,
                            ph.GrossAmount,
                            ph.DiscountPercentage,
                            ph.DiscountAmount,
                            ph.TaxAmount,
                            ph.OtherCharges,
                            ph.NetAmount,

                        }
                            into g
                            select new
                            {
                                g.Key.DocumentNo,
                                g.Key.InvGiftVoucherPurchaseHeaderID,
                                g.Key.DocumentDate,
                                g.Key.SupplierCode,
                                g.Key.SupplierName,
                                g.Key.Remark,
                                // g.Key.InvGiftVoucherMasterID,
                                VoucherType = (g.Key.VoucherType == 1 ? "Voucher" : "Coupon"),
                                g.Key.ReferenceNo,
                                g.Key.PaymentMethodName,
                                //g.Key.ExpectedDate,
                                g.Key.LocationCode,
                                g.Key.LocationName,
                                VoucherValue = g.Key.VoucherAmount,
                                //ff = g.Sum(x => x.VoucherAmount), //.VoucherAmount,
                                g.Key.InvGiftVoucherGroupID,
                                g.Key.GiftVoucherGroupCode,
                                g.Key.GiftVoucherGroupName,
                                g.Key.InvGiftVoucherBookCodeID,
                                g.Key.BookCode,
                                g.Key.BookName,
                                g.Key.GrossAmount,
                                g.Key.DiscountPercentage,
                                g.Key.DiscountAmount,
                                g.Key.TaxAmount,
                                g.Key.OtherCharges,
                                g.Key.NetAmount,
                            });


            var queryResult = (from qs in query
                               let firstV = (
                                         from pd in context.InvGiftVoucherPurchaseDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         orderby pm.VoucherSerial
                                         select pm.VoucherSerial).FirstOrDefault()
                               let count = (
                                         from pd in context.InvGiftVoucherPurchaseDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         select pm.VoucherSerial).Count()
                               let lastV = (
                                         from pd in context.InvGiftVoucherPurchaseDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         orderby pm.VoucherSerial descending
                                         select pm.VoucherSerial).FirstOrDefault()
                               let totalV = (
                                         from pd in context.InvGiftVoucherPurchaseDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                         select pd.VoucherAmount).Sum()
                               select new
                               {
                                   FieldString1 = qs.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(qs.DocumentDate),
                                   FieldString3 = qs.SupplierCode + "  " + qs.SupplierName,
                                   FieldString4 = qs.Remark,
                                   FieldString5 = "",//DbFunctions.TruncateTime(qs.ExpectedDate),
                                   FieldString6 = qs.PaymentMethodName,
                                   FieldString7 = "",
                                   FieldString8 = "",
                                   FieldString9 = qs.VoucherType,
                                   FieldString10 = qs.ReferenceNo,
                                   FieldString11 = "",
                                   FieldString12 = qs.LocationCode + "  " + qs.LocationName,
                                   FieldString13 = qs.GiftVoucherGroupCode + " - " + qs.GiftVoucherGroupName,
                                   FieldString14 = qs.BookCode + " - " + qs.BookName,
                                   FieldString15 = firstV + " - " + lastV,
                                   FieldString16 = qs.VoucherValue,
                                   FieldString17 = count,
                                   FieldString18 = (qs.VoucherValue * count),
                                   FieldDecimal1 = "",
                                   FieldDecimal2 = "",
                                   FieldDecimal3 = "",
                                   //FieldDecimal4 = pd.CostPrice,
                                   //FieldDecimal5 = pd.DiscountPercentage,
                                   //FieldDecimal6 = pd.DiscountAmount,
                                   //FieldDecimal7 = pd.NetAmount,
                                   FieldString23 = totalV,
                                   FieldString24 = qs.GrossAmount,
                                   FieldString25 = qs.DiscountPercentage,
                                   FieldString26 = qs.DiscountAmount,
                                   FieldString27 = qs.TaxAmount,
                                   FieldString28 = qs.OtherCharges,
                                   FieldString29 = qs.NetAmount,
                               }).ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(queryResult);

            var purchaseOrderHeaderID = context.InvGiftVoucherPurchaseHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus)
                    && h.DocumentID.Equals(documentId)).FirstOrDefault().ReferenceDocumentID;

            if (context.InvGiftVoucherPurchaseOrderDetails.Any(d => d.IsPurchase == false && d.InvGiftVoucherPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            {
                var queryBal = (
                        from ph in context.InvGiftVoucherPurchaseOrderHeaders
                        join pd in context.InvGiftVoucherPurchaseOrderDetails on ph.InvGiftVoucherPurchaseOrderHeaderID equals pd.InvGiftVoucherPurchaseOrderHeaderID
                        join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                        join gg in context.InvGiftVoucherGroups on pm.InvGiftVoucherGroupID equals gg.InvGiftVoucherGroupID
                        join gb in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                        join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.InvGiftVoucherPurchaseOrderHeaderID.Equals(purchaseOrderHeaderID) && ph.DocumentStatus.Equals(documentStatus)
                        group new { pm, pd, py } by new
                        {
                            ph.DocumentNo,
                            ph.InvGiftVoucherPurchaseOrderHeaderID,
                            ph.DocumentDate,
                            s.SupplierCode,
                            s.SupplierName,
                            ph.Remark,
                            pd.VoucherType,
                            ph.ReferenceNo,
                            py.PaymentMethodName,
                            ph.ExpectedDate,
                            l.LocationCode,
                            l.LocationName,
                            pd.VoucherAmount,
                            // pm.VoucherNo,
                            // pm.VoucherSerial,
                            // pd.InvGiftVoucherMasterID,
                            pm.InvGiftVoucherGroupID,
                            gg.GiftVoucherGroupCode,
                            gg.GiftVoucherGroupName,
                            pm.InvGiftVoucherBookCodeID,
                            gb.BookCode,
                            gb.BookName,
                            pd.NumberOfCount,
                            ph.GrossAmount,
                            ph.DiscountPercentage,
                            ph.DiscountAmount,
                            ph.TaxAmount,
                            ph.OtherCharges,
                            ph.NetAmount,

                        }
                            into g
                            select new
                            {
                                g.Key.DocumentNo,
                                g.Key.InvGiftVoucherPurchaseOrderHeaderID,
                                g.Key.DocumentDate,
                                g.Key.SupplierCode,
                                g.Key.SupplierName,
                                g.Key.Remark,
                                // g.Key.InvGiftVoucherMasterID,
                                VoucherType = (g.Key.VoucherType == 1 ? "Voucher" : "Coupon"),
                                g.Key.ReferenceNo,
                                g.Key.PaymentMethodName,
                                g.Key.ExpectedDate,
                                g.Key.LocationCode,
                                g.Key.LocationName,
                                VoucherValue = g.Key.VoucherAmount,
                                g.Key.InvGiftVoucherGroupID,
                                g.Key.GiftVoucherGroupCode,
                                g.Key.GiftVoucherGroupName,
                                g.Key.InvGiftVoucherBookCodeID,
                                g.Key.BookCode,
                                g.Key.BookName,
                                g.Key.GrossAmount,
                                g.Key.DiscountPercentage,
                                g.Key.DiscountAmount,
                                g.Key.TaxAmount,
                                g.Key.OtherCharges,
                                g.Key.NetAmount,
                            });


                var queryBalResult = (from qs in queryBal
                                   let firstV = (
                                             from pd in context.InvGiftVoucherPurchaseOrderDetails
                                             join pm in context.InvGiftVoucherMasters
                                             on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                             && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                             select pm.VoucherSerial).FirstOrDefault()
                                   let count = (
                                             from pd in context.InvGiftVoucherPurchaseOrderDetails
                                             join pm in context.InvGiftVoucherMasters
                                             on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                             && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                             select pm.VoucherSerial).Count()
                                   let balCount = (
                                              from pd in context.InvGiftVoucherPurchaseOrderDetails
                                              join pm in context.InvGiftVoucherMasters
                                              on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                              where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID && pd.IsPurchase == false
                                              && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                              select pm.VoucherSerial).Count()
                                   let lastV = (
                                             from pd in context.InvGiftVoucherPurchaseOrderDetails
                                             join pm in context.InvGiftVoucherMasters
                                             on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                             && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                             orderby pm.VoucherSerial descending
                                             select pm.VoucherSerial).FirstOrDefault()
                                   let totalV = (
                                            from pd in context.InvGiftVoucherPurchaseDetails
                                            join pm in context.InvGiftVoucherMasters
                                            on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                            where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                            select pd.VoucherAmount).Sum()
                                   select new
                                   {
                                       FieldString1 = qs.DocumentNo,
                                       FieldString2 = DbFunctions.TruncateTime(qs.DocumentDate),
                                       FieldString3 = qs.SupplierCode + "  " + qs.SupplierName,
                                       FieldString4 = qs.Remark,
                                       FieldString5 = DbFunctions.TruncateTime(qs.ExpectedDate),
                                       FieldString6 = qs.PaymentMethodName,
                                       FieldString7 = "",
                                       FieldString8 = "",
                                       FieldString9 = qs.VoucherType,
                                       FieldString10 = qs.ReferenceNo,
                                       FieldString11 = "",
                                       FieldString12 = qs.LocationCode + "  " + qs.LocationName,
                                       FieldString13 = qs.GiftVoucherGroupCode + " - " + qs.GiftVoucherGroupName,
                                       FieldString14 = qs.BookCode + " - " + qs.BookName,
                                       FieldString15 = firstV + " - " + lastV,
                                       FieldString16 = qs.VoucherValue,
                                       FieldString17 = count,
                                       FieldString18 = balCount,
                                       FieldDecimal1 = "",
                                       FieldDecimal2 = "",
                                       FieldDecimal3 = "",
                                       //FieldDecimal4 = pd.CostPrice,
                                       //FieldDecimal5 = pd.DiscountPercentage,
                                       //FieldDecimal6 = pd.DiscountAmount,
                                       //FieldDecimal7 = pd.NetAmount,
                                       //FieldString23 = qs.GrossAmount,
                                       //FieldString24 = qs.DiscountPercentage,
                                       //FieldString25 = qs.DiscountAmount,
                                       //FieldString26 = qs.TaxAmount,
                                       //FieldString27 = qs.OtherCharges,
                                       //FieldString28 = qs.NetAmount,
                                       FieldString23 = totalV,
                                       FieldString24 = qs.GrossAmount,
                                       FieldString25 = qs.DiscountPercentage,
                                       FieldString26 = qs.DiscountAmount,
                                       FieldString27 = qs.TaxAmount,
                                       FieldString28 = qs.OtherCharges,
                                       FieldString29 = qs.NetAmount,
                                   }).ToDataTable();

                dsTransactionData.Tables.Add(queryBalResult);
            }

            //return query.AsEnumerable().ToDataTable();

            return dsTransactionData;
        }

        public DataTable GetPurchaseDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.InvGiftVoucherPurchaseHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "LgsSupplierID":
                                query = (from qr in query
                                         join jt in context.LgsSuppliers.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.SupplierID equals jt.LgsSupplierID
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
                    {
                        query =
                            query.Where(
                                "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " +
                                reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                                long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
            }

            var queryResult = (from h in query
                               let bookName = (from pd in context.InvGiftVoucherPurchaseDetails
                                               join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                               join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                               where h.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                               select bc.BookName).FirstOrDefault()
                               let bookValue = (from pd in context.InvGiftVoucherPurchaseDetails
                                                join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                                join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                                where h.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                                && pm.VoucherType == pd.VoucherType && pd.VoucherType == 1
                                                select bc.GiftVoucherValue).FirstOrDefault()
                               let firstV = (from pd in context.InvGiftVoucherPurchaseDetails
                                             join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                             where h.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                             orderby pm.VoucherSerial
                                             select pm.VoucherSerial).FirstOrDefault()
                               let lastV = (from pd in context.InvGiftVoucherPurchaseDetails
                                            join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                            join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                            where h.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                            orderby pm.VoucherSerial descending
                                            select pm.VoucherSerial).FirstOrDefault()
                               let count = (from pd in context.InvGiftVoucherPurchaseDetails
                                            join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                            join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                            where h.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                            select pm.VoucherSerial).Count()
                               join s in context.LgsSuppliers on h.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               select
                                   new
                                   {
                                       FieldString1 = EntityFunctions.TruncateTime(h.DocumentDate),
                                       FieldString2 = h.DocumentNo,
                                       FieldString3 = bookName,
                                       FieldString4 = s.SupplierName,
                                       FieldString5 = h.ReferenceNo,
                                       FieldString6 = h.Remark,
                                       FieldString7 = firstV + " - " + lastV,
                                       FieldDecimal1 = count,
                                       FieldDecimal2 = h.NetAmount,
                                       FieldDecimal3 = h.GrossAmount,
                                       FieldDecimal4 = h.DiscountPercentage,
                                       FieldDecimal5 = h.DiscountAmount,
                                       FieldDecimal6 = h.TaxAmount,
                                       FieldDecimal7 = (bookValue > 0 ? (count * bookValue) : 0)
                                   }); //.ToArray();

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy))
            {
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
                        {
                            dtQueryResult.Columns.Remove(reportDataStruct.ReportField);
                        }
                    }
                }
            }
            else
            {
                StringBuilder sbGroupByColumns = new StringBuilder();
                StringBuilder sbOrderByColumns = new StringBuilder();
                StringBuilder sbSelectColumns = new StringBuilder();

                sbGroupByColumns.Append("new(");
                sbSelectColumns.Append("new(");

                foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                {
                    sbGroupByColumns.Append("it." + item.ReportField.ToString() + " , ");
                    sbSelectColumns.Append("Key." + item.ReportField.ToString() + " as " + item.ReportField + " , ");

                    sbOrderByColumns.Append("Key." + item.ReportField + " , ");
                }

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal))))
                {
                    sbSelectColumns.Append("SUM(" + item.ReportField + ") as " + item.ReportField + " , ");
                }

                sbGroupByColumns.Remove(sbGroupByColumns.Length - 2, 2); // remove last ','
                sbGroupByColumns.Append(")");

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                sbSelectColumns.Remove(sbSelectColumns.Length - 2, 2); // remove last ','
                sbSelectColumns.Append(")");

                var groupByResult = queryResult.GroupBy(sbGroupByColumns.ToString(), "it")
                                    .OrderBy(sbOrderByColumns.ToString())
                                    .Select(sbSelectColumns.ToString());

                dtQueryResult = CommonService.ExecuteSqlQuery(groupByResult.ToString());

                dtQueryResult.Columns.Remove("C1"); // Remove the first 'C1' column
                int colIndex = 0;
                foreach (var item in reportGroupDataStructList.Where(g => g.IsResultGroupBy.Equals(true)))
                {
                    dtQueryResult.Columns[colIndex].ColumnName = item.ReportField;
                    colIndex++;
                }

                foreach (var item in reportDataStructList.Where(d => d.ReportDataType.Equals(typeof(decimal))))
                {
                    dtQueryResult.Columns[colIndex].ColumnName = item.ReportField;
                    colIndex++;
                }

                foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                {
                    if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                    {
                        if (!reportDataStruct.IsSelectionField)
                        {
                            dtQueryResult.Columns.Remove(reportDataStruct.ReportField);
                        }
                    }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();

        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {            
            // Create query 
            IQueryable qryResulth = context.InvGiftVoucherPurchaseHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, autoGenerateInfo.DocumentID);
            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSupplierID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "SupplierCode")
                    {
                        qryResulth = qryResulth.Join(context.LgsSuppliers, "SupplierID", reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierCode)")
                            .GroupBy("new(SupplierCode)", "new(SupplierCode)")
                            .OrderBy("Key.SupplierCode").Select("Key.SupplierCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "SupplierName")
                    {
                        qryResulth = qryResulth.Join(context.LgsSuppliers, "SupplierID", reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierName)")
                            .GroupBy("new(SupplierName)", "new(SupplierName)")
                            .OrderBy("Key.SupplierName").Select("Key.SupplierName");
                    }
                    break;

                default:
                    qryResulth = qryResulth.Select(reportDataStruct.DbColumnName.Trim());
            
                    break;
            }
           
            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResulth)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else
            {
                foreach (var item in qryResulth)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString()); }
                }
            }
            return selectionDataList;
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
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });

                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
            }
            else
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });
                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }

        #endregion
        #endregion
    }
}
