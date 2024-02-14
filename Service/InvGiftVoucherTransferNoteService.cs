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
    public class InvGiftVoucherTransferNoteService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Save Gift voucher purchase
        /// </summary>
        /// <param name="invGiftVoucherTransferNoteHeader"></param>
        /// <param name="invGiftVoucherTransferNoteDetailTemp"></param>
        public bool SaveGiftVoucherTransfer(InvGiftVoucherTransferNoteHeader invGiftVoucherTransferNoteHeader, List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailTemp, out string newDocumentNo, string formName = "", bool isTStatus = false)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (invGiftVoucherTransferNoteHeader.DocumentStatus.Equals(1))
                {
                    InvGiftVoucherTransferNoteService invGiftVoucherTransferNoteService = new InvGiftVoucherTransferNoteService();
                    LocationService locationService = new LocationService();
                    invGiftVoucherTransferNoteHeader.DocumentNo = invGiftVoucherTransferNoteService.GetDocumentNo(formName, invGiftVoucherTransferNoteHeader.LocationID, locationService.GetLocationsByID(invGiftVoucherTransferNoteHeader.LocationID).LocationCode, invGiftVoucherTransferNoteHeader.DocumentID, false).Trim();
                }

                newDocumentNo = invGiftVoucherTransferNoteHeader.DocumentNo;

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                #region Transaction Details

                if (invGiftVoucherTransferNoteHeader.InvGiftVoucherTransferNoteHeaderID.Equals(0))
                {context.InvGiftVoucherTransferNoteHeaders.Add(invGiftVoucherTransferNoteHeader);}
                else
                {context.Entry(invGiftVoucherTransferNoteHeader).State = EntityState.Modified;}

                context.SaveChanges();

                context.Set<InvGiftVoucherTransferNoteDetail>().Delete(context.InvGiftVoucherTransferNoteDetails.Where(p => p.InvGiftVoucherTransferNoteHeaderID.Equals(invGiftVoucherTransferNoteHeader.InvGiftVoucherTransferNoteHeaderID) && p.LocationID.Equals(invGiftVoucherTransferNoteHeader.LocationID) && p.DocumentID.Equals(invGiftVoucherTransferNoteHeader.DocumentID)).AsQueryable());
                context.SaveChanges();

                var invGiftVoucherTransferDetailSaveQuery = (from pd in invGiftVoucherTransferNoteDetailTemp
                                                             select new
                                                             {
                                                                 InvGiftVoucherTransferNoteDetailID = 0,
                                                                 GiftVoucherTransferNoteDetailID = 0,
                                                                 InvGiftVoucherTransferNoteHeaderID = invGiftVoucherTransferNoteHeader.InvGiftVoucherTransferNoteHeaderID,
                                                                 CompanyID = invGiftVoucherTransferNoteHeader.CompanyID,
                                                                 LocationID = invGiftVoucherTransferNoteHeader.LocationID,
                                                                 DocumentID = invGiftVoucherTransferNoteHeader.DocumentID,
                                                                 //DocumentNo = invGiftVoucherTransferNoteHeader.DocumentNo,
                                                                 DocumentDate = invGiftVoucherTransferNoteHeader.DocumentDate,
                                                                 LineNo = pd.LineNo,
                                                                 InvGiftVoucherMasterID = pd.InvGiftVoucherMasterID,
                                                                 NumberOfCount = pd.NumberOfCount,
                                                                 VoucherAmount = pd.VoucherValue,
                                                                 VoucherType = pd.VoucherType,
                                                                 ToLocationID = invGiftVoucherTransferNoteHeader.ToLocationID,
                                                                 DocumentStatus = invGiftVoucherTransferNoteHeader.DocumentStatus,
                                                                 GroupOfCompanyID = invGiftVoucherTransferNoteHeader.GroupOfCompanyID,
                                                                 CreatedUser = invGiftVoucherTransferNoteHeader.CreatedUser,
                                                                 CreatedDate = invGiftVoucherTransferNoteHeader.CreatedDate,
                                                                 ModifiedUser = invGiftVoucherTransferNoteHeader.ModifiedUser,
                                                                 ModifiedDate = invGiftVoucherTransferNoteHeader.ModifiedDate,
                                                                 DataTransfer = invGiftVoucherTransferNoteHeader.DataTransfer
                                                             }).ToList();

                //CommonService.BulkInsert(Common.LINQToDataTable(invGiftVoucherTransferDetailSaveQuery), "InvGiftVoucherTransferNoteDetail");

                DataTable DTITEM = new DataTable();
                DTITEM = invGiftVoucherTransferDetailSaveQuery.ToDataTable();

                string mergeSql = CommonService.MergeSqlToForTrans(DTITEM, "InvGiftVoucherTransferNoteDetail");
                CommonService.BulkInsert(DTITEM, "InvGiftVoucherTransferNoteDetail", mergeSql);

                #endregion

                DateTime t1 = DateTime.UtcNow;
                
                #region Update Master Vouchers
                //Update Voucher status on Voucher Master
                //if (invGiftVoucherTransferNoteHeader.DocumentStatus.Equals(1))
                //{
                //    UpdateInvGiftVoucherMaster(invGiftVoucherTransferNoteDetailTemp);
                //}
                #endregion

                //transactionScope.Complete();
                //string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                //return true;

                #region Process

                var parameter = new DbParameter[] 
                        { 
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvGiftVoucherTransferNoteHeaderID", Value=invGiftVoucherTransferNoteHeader.InvGiftVoucherTransferNoteHeaderID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invGiftVoucherTransferNoteHeader.LocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@ToLocationID", Value=invGiftVoucherTransferNoteHeader.ToLocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invGiftVoucherTransferNoteHeader.DocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentHeaderID", Value=invGiftVoucherTransferNoteHeader.ReferenceDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvReferenceDocumentDocumentID", Value=invGiftVoucherTransferNoteHeader.ReferenceDocumentDocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invGiftVoucherTransferNoteHeader.DocumentStatus},
                        };

                if (CommonService.ExecuteStoredProcedure("spInvGiftVoucherTransferNoteSave", parameter))
                {
                    transactionScope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }

                #endregion
            }
        }

        public void UpdateInvGiftVoucherMaster(List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailsTemp)
        {
            foreach (InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp in invGiftVoucherTransferNoteDetailsTemp)
            {
                context.InvGiftVoucherMasters.Update(pd => pd.InvGiftVoucherMasterID.Equals(invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherMasterID) && pd.VoucherNo.Equals(invGiftVoucherTransferNoteDetailTemp.VoucherNo) && pd.VoucherSerial.Equals(invGiftVoucherTransferNoteDetailTemp.VoucherSerial), pd => new InvGiftVoucherMaster { VoucherStatus = 3, ToLocationID = invGiftVoucherTransferNoteDetailTemp.ToLocationID});
                context.SaveChanges();
            }
        }

        public List<InvGiftVoucherTransferNoteDetailTemp> GetAllTransferDetail(InvGiftVoucherTransferNoteHeader invGiftVoucherTransferNoteHeader)
        {
            InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp;

            List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailTempList = new List<InvGiftVoucherTransferNoteDetailTemp>();

            var giftVoucherTransferDetailTemp = (from bm in context.InvGiftVoucherMasters
                                                 join pd in context.InvGiftVoucherTransferNoteDetails on bm.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                                 where pd.LocationID.Equals(invGiftVoucherTransferNoteHeader.LocationID) && pd.CompanyID.Equals(invGiftVoucherTransferNoteHeader.CompanyID)
                                                 && pd.DocumentID.Equals(invGiftVoucherTransferNoteHeader.DocumentID) && pd.InvGiftVoucherTransferNoteHeaderID.Equals(invGiftVoucherTransferNoteHeader.InvGiftVoucherTransferNoteHeaderID)
                                                 select new
                                                 {
                                                     invGiftVoucherTransferNoteHeader.DocumentNo,
                                                     pd.DocumentID,
                                                     pd.DocumentDate,
                                                     pd.DocumentStatus,
                                                     pd.ToLocationID,
                                                     pd.LineNo,
                                                     pd.LocationID,
                                                     bm.VoucherNo,
                                                     bm.InvGiftVoucherMasterID,
                                                     bm.InvGiftVoucherBookCodeID,
                                                     bm.InvGiftVoucherGroupID,
                                                     pd.VoucherAmount,
                                                     bm.VoucherSerial,
                                                     pd.NumberOfCount,
                                                     pd.VoucherType
                                                 }).ToArray();

            foreach (var tempGiftVoucher in giftVoucherTransferDetailTemp)
            {
                invGiftVoucherTransferNoteDetailTemp = new InvGiftVoucherTransferNoteDetailTemp();
                invGiftVoucherTransferNoteDetailTemp.DocumentNo = tempGiftVoucher.DocumentNo;
                invGiftVoucherTransferNoteDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                invGiftVoucherTransferNoteDetailTemp.DocumentDate = tempGiftVoucher.DocumentDate;
                invGiftVoucherTransferNoteDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                invGiftVoucherTransferNoteDetailTemp.LineNo = tempGiftVoucher.LineNo;
                invGiftVoucherTransferNoteDetailTemp.LocationID = tempGiftVoucher.LocationID;
                invGiftVoucherTransferNoteDetailTemp.VoucherNo = tempGiftVoucher.VoucherNo;
                invGiftVoucherTransferNoteDetailTemp.VoucherSerial = tempGiftVoucher.VoucherSerial;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherMasterID = tempGiftVoucher.InvGiftVoucherMasterID;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherBookCodeID = tempGiftVoucher.InvGiftVoucherBookCodeID;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherGroupID = tempGiftVoucher.InvGiftVoucherGroupID;
                invGiftVoucherTransferNoteDetailTemp.VoucherType = tempGiftVoucher.VoucherType;
                if (tempGiftVoucher.VoucherType == 1)
                {
                    invGiftVoucherTransferNoteDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherTransferNoteDetailTemp.VoucherAmount = tempGiftVoucher.VoucherAmount;
                }
                else
                {
                    invGiftVoucherTransferNoteDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherTransferNoteDetailTemp.GiftVoucherPercentage = tempGiftVoucher.VoucherAmount;
                }
                invGiftVoucherTransferNoteDetailTemp.NumberOfCount = tempGiftVoucher.NumberOfCount;
                invGiftVoucherTransferNoteDetailTempList.Add(invGiftVoucherTransferNoteDetailTemp);
            }

            return invGiftVoucherTransferNoteDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public List<InvGiftVoucherTransferNoteDetailTemp> GetGiftVoucherPurchaseDetail(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            InvGiftVoucherTransferNoteDetailTemp invGiftVoucherTransferNoteDetailTemp;

            List<InvGiftVoucherTransferNoteDetailTemp> invGiftVoucherTransferNoteDetailTempList = new List<InvGiftVoucherTransferNoteDetailTemp>();

            var giftVoucherPurchaseDetailTemp = (from bm in context.InvGiftVoucherMasters
                                                      join pd in context.InvGiftVoucherPurchaseDetails on bm.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                                      where pd.DocumentStatus == 1 && pd.LocationID.Equals(invGiftVoucherPurchaseHeader.LocationID) && pd.CompanyID.Equals(invGiftVoucherPurchaseHeader.CompanyID)
                                                      && pd.DocumentID.Equals(invGiftVoucherPurchaseHeader.DocumentID) && pd.InvGiftVoucherPurchaseHeaderID.Equals(invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID)
                                                      && pd.IsTransfer == false
                                                      select new
                                                      {
                                                          invGiftVoucherPurchaseHeader.DocumentNo, //pd.DocumentNo,
                                                          pd.DocumentID,
                                                          pd.DocumentDate,
                                                          pd.DocumentStatus,
                                                          invGiftVoucherPurchaseHeader.DiscountAmount,
                                                          invGiftVoucherPurchaseHeader.DiscountPercentage,
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
                invGiftVoucherTransferNoteDetailTemp = new InvGiftVoucherTransferNoteDetailTemp();
                invGiftVoucherTransferNoteDetailTemp.DocumentNo = tempGiftVoucher.DocumentNo;
                invGiftVoucherTransferNoteDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                invGiftVoucherTransferNoteDetailTemp.DocumentDate = tempGiftVoucher.DocumentDate;
                invGiftVoucherTransferNoteDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                invGiftVoucherTransferNoteDetailTemp.LineNo = tempGiftVoucher.LineNo;
                invGiftVoucherTransferNoteDetailTemp.LocationID = tempGiftVoucher.LocationID;
                invGiftVoucherTransferNoteDetailTemp.VoucherNo = tempGiftVoucher.VoucherNo;
                invGiftVoucherTransferNoteDetailTemp.VoucherSerial = tempGiftVoucher.VoucherSerial;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherMasterID = tempGiftVoucher.InvGiftVoucherMasterID;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherBookCodeID = tempGiftVoucher.InvGiftVoucherBookCodeID;
                invGiftVoucherTransferNoteDetailTemp.InvGiftVoucherGroupID = tempGiftVoucher.InvGiftVoucherGroupID;

                invGiftVoucherTransferNoteDetailTemp.VoucherType = tempGiftVoucher.VoucherType;
                if (tempGiftVoucher.VoucherType == 1)
                {
                    invGiftVoucherTransferNoteDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherTransferNoteDetailTemp.VoucherAmount = tempGiftVoucher.VoucherAmount;
                }
                else
                {
                    invGiftVoucherTransferNoteDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherTransferNoteDetailTemp.GiftVoucherPercentage = tempGiftVoucher.VoucherAmount;
                }
                invGiftVoucherTransferNoteDetailTemp.NumberOfCount = tempGiftVoucher.NumberOfCount;
                invGiftVoucherTransferNoteDetailTempList.Add(invGiftVoucherTransferNoteDetailTemp);
            }

            return invGiftVoucherTransferNoteDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public InvGiftVoucherTransferNoteHeader GetGiftVoucherTransferNoteHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvGiftVoucherTransferNoteHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvGiftVoucherTransferNoteHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        /// <summary>
        ///  Get all document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetAllDocumentNumbers()
        {
            List<string> transferedDocumentList = new List<string>();
            transferedDocumentList = context.InvGiftVoucherTransferNoteHeaders.Select(tn => tn.DocumentNo).ToList();
            string[] transferedDocumentCodes = transferedDocumentList.ToArray();
            return transferedDocumentCodes;
        }

        public DataTable GetPendingTransferNoteDocuments()
        {
            var sql = (from poh in context.InvGiftVoucherTransferNoteHeaders
                      // join pod in context.InvGiftVoucherTransferNoteDetails on poh.InvGiftVoucherTransferNoteHeaderID equals pod.InvGiftVoucherTransferNoteHeaderID
                       join fl in context.Locations on poh.LocationID equals fl.LocationID
                       join tl in context.Locations on poh.ToLocationID equals tl.LocationID
                       where poh.DocumentStatus == 0
                       select new
                       {
                           poh.DocumentNo,
                           poh.DocumentDate,
                           FromLocation = fl.LocationName,
                           ToLocation = tl.LocationName
                       }).ToArray();

            return sql.ToDataTable();
        }

        public string[] GetVoucherSerialByBookID(long bookID, int locationID, bool isHeadOffice)
        {
            List<string> availableVoucherCount = new List<string>();
            if (isHeadOffice)
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.ToLocationID == locationID && (s.VoucherStatus == 2) && s.IsDelete == false).Select(s => s.VoucherSerial).ToList();
            }
            else
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.ToLocationID == locationID && (s.VoucherStatus == 3) && s.IsDelete == false).Select(s => s.VoucherSerial).ToList();
            }

            string[] Vouchers = availableVoucherCount.ToArray();
            return Vouchers;
        }

        public string[] GetVoucherNoByBookID(long bookID, int locationID, bool isHeadOffice)
        {
            List<string> availableVoucherCount = new List<string>();
            if (isHeadOffice)
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.ToLocationID == locationID && (s.VoucherStatus == 2) && s.IsDelete == false).Select(s => s.VoucherNo).ToList();
            }
            else
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.ToLocationID == locationID && (s.VoucherStatus == 3) && s.IsDelete == false).Select(s => s.VoucherNo).ToList();
            }

            string[] Vouchers = availableVoucherCount.ToArray();
            return Vouchers;
        }

        public long GetVoucherQtyByBookID(int bookID, string fromNo, string toNo, int voucherStatus, bool isHeadOffice)
        {
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                    {
                        availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(fromNo) >= 0 && s.VoucherSerial.CompareTo(toNo) <= 0) && (s.VoucherStatus == 2) && s.IsDelete == false).Count();
                    }
                    else
                    {
                        bookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNo.CompareTo(fromNo) >= 0 && s.VoucherNo.CompareTo(toNo) <= 0) && (s.VoucherStatus == 2) && s.IsDelete == false).Count();

                        availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                    }
                }
                else
                {
                    if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                    {
                        availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(fromNo) >= 0 && s.VoucherSerial.CompareTo(toNo) <= 0) && (s.VoucherStatus == 3) && s.IsDelete == false).Count();
                    }
                    else
                    {
                        bookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNo.CompareTo(fromNo) >= 0 && s.VoucherNo.CompareTo(toNo) <= 0) && (s.VoucherStatus == 3) && s.IsDelete == false).Count();

                        availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                    }
                }
            }

            return availableBookCount;
        }

        public long GetGRNVoucherQtyByBookID(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader, int bookID, string fromNo, string toNo, int voucherStatus, bool isHeadOffice)
        {
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                    {
                        //availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(fromNo) >= 0 
                        //    && s.VoucherSerial.CompareTo(toNo) <= 0) && (s.VoucherStatus == 2 || s.VoucherStatus == 3) && s.IsDelete == false).Count();
                        availableBookCount = (from gv in context.InvGiftVoucherMasters
                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals
                             pd.InvGiftVoucherMasterID
                         where gv.InvGiftVoucherBookCodeID == bookID
                               && (gv.VoucherSerial.CompareTo(fromNo) >= 0 && gv.VoucherSerial.CompareTo(toNo) <= 0)
                               && (gv.VoucherStatus == 2) && gv.IsDelete == false
                         select gv.VoucherSerial).Count();
                    }
                    else
                    {
                        bookCount = (from gv in context.InvGiftVoucherMasters
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals
                                         pd.InvGiftVoucherMasterID
                                     where gv.InvGiftVoucherBookCodeID == bookID
                                           && (gv.VoucherNo.CompareTo(fromNo) >= 0 && gv.VoucherNo.CompareTo(toNo) <= 0)
                                           && (gv.VoucherStatus == 2) && gv.IsDelete == false
                                     select gv.VoucherSerial).Count();
                            
                            //context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNo.CompareTo(fromNo) >= 0 
                            //    && s.VoucherNo.CompareTo(toNo) <= 0) && (s.VoucherStatus == 2 || s.VoucherStatus == 3) && s.IsDelete == false).Count();

                        availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                    }
                }
                else
                {
                    if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                    {
                        availableBookCount = (from gv in context.InvGiftVoucherMasters
                                              join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals
                                                  pd.InvGiftVoucherMasterID
                                              where gv.InvGiftVoucherBookCodeID == bookID
                                                    && (gv.VoucherSerial.CompareTo(fromNo) >= 0 && gv.VoucherSerial.CompareTo(toNo) <= 0)
                                                    && (gv.VoucherStatus == 3) && gv.IsDelete == false
                                              select gv.VoucherSerial).Count();
                    }
                    else
                    {
                        bookCount = (from gv in context.InvGiftVoucherMasters
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals
                                         pd.InvGiftVoucherMasterID
                                     where gv.InvGiftVoucherBookCodeID == bookID
                                           && (gv.VoucherNo.CompareTo(fromNo) >= 0 && gv.VoucherNo.CompareTo(toNo) <= 0)
                                           && (gv.VoucherStatus == 3) && gv.IsDelete == false
                                     select gv.VoucherSerial).Count();

                        availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                    }
                }
                
            }

            return availableBookCount;
        }

        public DataTable GetAllActiveGiftVoucherSerialsDataTableByVoucherType(int voucherType, int voucherGroupID, int voucherBookID, int locationID, bool isHeadOffice)
        {
            DataTable tblGiftVoucherMasters = new DataTable();
            if (isHeadOffice)
            {
                var query = from u in context.InvGiftVoucherMasters
                            join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                            join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                            where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID &&
                            u.InvGiftVoucherBookCodeID == voucherBookID && (u.VoucherStatus == 2)) && u.ToLocationID == locationID
                            select new { u.VoucherSerial, u.VoucherNo, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue };
                tblGiftVoucherMasters = query.ToDataTable();
            }
            else
            {
                var query = from u in context.InvGiftVoucherMasters
                            join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                            join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                            where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID &&
                            u.InvGiftVoucherBookCodeID == voucherBookID && (u.VoucherStatus == 3)) && u.ToLocationID == locationID
                            select new { u.VoucherSerial, u.VoucherNo, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue };
                tblGiftVoucherMasters = query.ToDataTable();
            }
            return tblGiftVoucherMasters;
        }

        public string[] GetVoucherSerialByRecallGRN(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, long bookID)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType && gv.InvGiftVoucherBookCodeID == bookID
                                     && pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                     && pd.IsTransfer == false
                                     select gv.VoucherSerial).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public DataTable GetAllActiveGiftVoucherRecallGRNGroupsDataTable(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            DataTable tblGiftVoucherGroups = new DataTable();
            var query = (from gb in context.InvGiftVoucherGroups
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsTransfer == false
                         && pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
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

        public DataTable GetAllActiveGiftVoucherMasterRecallGRNBooksDataTable(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            DataTable tblGiftVoucherMasterBooks = new DataTable();
            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsTransfer == false 
                         && pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
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

        public string[] GetVoucherNoByRecallGRN(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader, long bookID)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType
                                     && gv.InvGiftVoucherBookCodeID == bookID
                                     && pd.InvGiftVoucherPurchaseHeaderID == existingInvGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                     && pd.IsTransfer == false
                                     select gv.VoucherNo).ToList();

            string[] vouchers = availableVoucherCount.ToArray();
            return vouchers;
        }

        public DataTable GetAllActiveGiftVoucherNosDataTableByVoucherType(int voucherType, int voucherGroupID, int voucherBookID, int locationID, bool isHeadOffice)
        {
            DataTable tblGiftVoucherMasters = new DataTable();

            if (isHeadOffice)
            {
                var query = from u in context.InvGiftVoucherMasters
                            join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                            join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                            where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID &&
                            u.InvGiftVoucherBookCodeID == voucherBookID && (u.VoucherStatus == 2)) && u.ToLocationID == locationID
                            select new { u.VoucherNo, u.VoucherSerial, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue, u.PageCount };
                tblGiftVoucherMasters = query.ToDataTable();
            }
            else
            {
                var query = from u in context.InvGiftVoucherMasters
                            join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                            join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                            where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID &&
                            u.InvGiftVoucherBookCodeID == voucherBookID && (u.VoucherStatus == 3)) && u.ToLocationID == locationID
                            select new { u.VoucherNo, u.VoucherSerial, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue, u.PageCount };
                tblGiftVoucherMasters = query.ToDataTable();
            }
            
            return tblGiftVoucherMasters;
        }

        public string[] GetRecallGRNInvGiftVoucherGroupCodes(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherGroups
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsTransfer == false && pd.InvGiftVoucherPurchaseHeaderID == existingInvGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
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

        public string[] GetRecallGRNInvGiftVoucherGroupNames(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherGroups
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.InvGiftVoucherPurchaseHeaderID == existingInvGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                      && pd.IsTransfer == false
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

        public string[] GetRecallGRNInvGiftVoucherBookCodesByVoucherType(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherBookCodes
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.InvGiftVoucherPurchaseHeaderID == existingInvGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                     && pd.IsTransfer == false
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

        public string[] GetRecallGRNInvGiftVoucherBookNamesByVoucherType(InvGiftVoucherPurchaseHeader existingInvGiftVoucherPurchaseHeader)
        {
            List<string> availableVoucherCount = new List<string>();

            availableVoucherCount = (from gb in context.InvGiftVoucherBookCodes
                                     join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherBookCodeID equals gv.InvGiftVoucherBookCodeID
                                     join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                     where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.InvGiftVoucherPurchaseHeaderID == existingInvGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                     && pd.IsTransfer == false
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

        public List<InvGiftVoucherMaster> GetGeneratedGRNGiftVouchersByBookIDWithQtyForTransfer(int bookID, long invGiftVoucherPurchaseHeaderID, int locationID, int voucherQty)
        {
           return (from gv in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID
                        equals pd.InvGiftVoucherMasterID
                    where
                        pd.VoucherType == gv.VoucherType && gv.IsDelete == false &&
                        gv.InvGiftVoucherBookCodeID == bookID && pd.IsTransfer == false && gv.ToLocationID == locationID && 
                        pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeaderID
                    orderby gv.VoucherSerial
                    select
                        gv
            ).Take(voucherQty).ToList();
        }


        public List<InvGiftVoucherMaster> GetGeneratedGRNGiftVouchersByBookIDWithVoucherNoRangeForTransfer(int bookID, long invGiftVoucherPurchaseHeaderID, int locationID, string voucherNoFrom, string voucherNoTo)
        {
            return (from gv in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID
                        equals pd.InvGiftVoucherMasterID
                    where
                        pd.VoucherType == gv.VoucherType && gv.IsDelete == false && (gv.VoucherNo.CompareTo(voucherNoFrom) >= 0
            && gv.VoucherNo.CompareTo(voucherNoTo) <= 0) &&
                        gv.InvGiftVoucherBookCodeID == bookID && pd.IsTransfer == false && gv.ToLocationID == locationID && 
                        pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeaderID
                    orderby gv.VoucherSerial
                    select
                        gv
            ).ToList();
        }

        public List<InvGiftVoucherMaster> GetGeneratedGRNGiftVouchersByBookIDWithVoucherSerialRangeForTransfer(int bookID, long invGiftVoucherPurchaseHeaderID, int locationID, string voucherSerialFrom, string voucherSerialTo)
        {
            return (from gv in context.InvGiftVoucherMasters
                    join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID
                        equals pd.InvGiftVoucherMasterID
                    where
                        pd.VoucherType == gv.VoucherType && gv.IsDelete == false &&
                        (gv.VoucherSerial.CompareTo(voucherSerialFrom) >= 0
                            && gv.VoucherSerial.CompareTo(voucherSerialTo) <= 0) &&
                        gv.InvGiftVoucherBookCodeID == bookID && pd.IsTransfer == false && gv.ToLocationID == locationID && 
                        pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeaderID
                    orderby gv.VoucherSerial
                    select
                        gv
            ).ToList();
        }

        public DataTable GetAllActiveGiftVoucherMasterRecallGRNVoucherNosDataTable(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            DataTable tblGiftVoucherMasters = new DataTable();

            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsTransfer == false &&
                         pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
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

        public DataTable GetAllActiveGiftVoucherMasterRecallGRNVoucherSerialsDataTable(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader)
        {
            DataTable tblGiftVoucherMasters = new DataTable();

            var query = (from gb in context.InvGiftVoucherBookCodes
                         join gv in context.InvGiftVoucherMasters on gb.InvGiftVoucherGroupID equals gv.InvGiftVoucherGroupID
                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                         where pd.VoucherType == gv.VoucherType && gb.IsDelete == false && pd.IsTransfer == false && pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
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
                         });
            return tblGiftVoucherMasters = query.ToDataTable();
        }

        public DataSet GetTransferofGoodsTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.InvGiftVoucherTransferNoteHeaders
                        join pd in context.InvGiftVoucherTransferNoteDetails on ph.InvGiftVoucherTransferNoteHeaderID equals pd.InvGiftVoucherTransferNoteHeaderID
                        join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                        join gg in context.InvGiftVoucherGroups on pm.InvGiftVoucherGroupID equals gg.InvGiftVoucherGroupID
                        join gb in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        join lTo in context.Locations on ph.ToLocationID equals lTo.LocationID
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        group new { pm, pd } by new
                        {
                            ph.DocumentNo,
                            ph.InvGiftVoucherTransferNoteHeaderID,
                            ph.DocumentDate,
                            ph.Remark,
                            pd.VoucherType,
                            ph.ReferenceNo,
                            //ph.ExpectedDate,
                            l.LocationCode,
                            l.LocationName,
                            lToCode = lTo.LocationCode,
                            lToName = lTo.LocationName,
                            pd.VoucherAmount,
                            pm.InvGiftVoucherGroupID,
                            gg.GiftVoucherGroupCode,
                            gg.GiftVoucherGroupName,
                            pm.InvGiftVoucherBookCodeID,
                            gb.BookCode,
                            gb.BookName,
                            Qty = pd.NumberOfCount,

                        }
                            into g
                            select new
                            {
                                g.Key.DocumentNo,
                                g.Key.InvGiftVoucherTransferNoteHeaderID,
                                g.Key.DocumentDate,
                                g.Key.Remark,
                                VoucherType = (g.Key.VoucherType == 1 ? "Voucher" : "Coupon"),
                                g.Key.ReferenceNo,
                                //g.Key.ExpectedDate,
                                g.Key.LocationCode,
                                g.Key.LocationName,
                                g.Key.lToCode,
                                g.Key.lToName,
                                VoucherValue = g.Key.VoucherAmount,
                                g.Key.InvGiftVoucherGroupID,
                                g.Key.GiftVoucherGroupCode,
                                g.Key.GiftVoucherGroupName,
                                g.Key.InvGiftVoucherBookCodeID,
                                g.Key.BookCode,
                                g.Key.BookName,
                            });


            var queryResult = (from qs in query
                               let firstV = (
                                         from pd in context.InvGiftVoucherTransferNoteDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         orderby pm.VoucherSerial
                                         select pm.VoucherSerial).FirstOrDefault()
                               let count = (
                                         from pd in context.InvGiftVoucherTransferNoteDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         select pm.VoucherSerial).Count()
                               let lastV = (
                                         from pd in context.InvGiftVoucherTransferNoteDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                         && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                         orderby pm.VoucherSerial descending
                                         select pm.VoucherSerial).FirstOrDefault()
                               let totalV = (
                                         from pd in context.InvGiftVoucherTransferNoteDetails
                                         join pm in context.InvGiftVoucherMasters
                                         on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                         where qs.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                         select pd.VoucherAmount).Sum()
                               select new
                               {
                                   FieldString1 = qs.DocumentNo,
                                   FieldString2 = DbFunctions.TruncateTime(qs.DocumentDate),
                                   FieldString3 = "",
                                   FieldString4 = qs.Remark,
                                   FieldString5 = "",//DbFunctions.TruncateTime(qs.ExpectedDate),
                                   FieldString6 = "",
                                   FieldString7 = "",
                                   FieldString8 = "",
                                   FieldString9 = qs.VoucherType,
                                   FieldString10 = qs.ReferenceNo,
                                   FieldString11 = qs.LocationCode + "  " + qs.LocationName,
                                   FieldString12 = qs.lToCode + "  " + qs.lToName,
                                   FieldString13 = qs.GiftVoucherGroupCode + " - " + qs.GiftVoucherGroupName,
                                   FieldString14 = qs.BookCode + " - " + qs.BookName,
                                   FieldString15 = firstV + " - " + lastV,
                                   FieldString16 = qs.VoucherValue,
                                   FieldString17 = count,
                                   FieldDecimal1 = "",
                                   FieldDecimal2 = "",
                                   FieldDecimal3 = "",
                                   //FieldDecimal4 = pd.CostPrice,
                                   //FieldDecimal5 = pd.DiscountPercentage,
                                   //FieldDecimal6 = pd.DiscountAmount,
                                   //FieldDecimal7 = pd.NetAmount,
                                   FieldString23 = totalV,
                                   FieldString24 = "",
                                   FieldString25 = "",
                                   FieldString26 = "",
                                   FieldString27 = "",
                                   FieldString28 = "",
                               }).ToDataTable();

            DataSet dsTransactionData = new DataSet();
            dsTransactionData.Tables.Add(queryResult);

            var purchaseHeaderID = context.InvGiftVoucherTransferNoteHeaders.Where(h => h.DocumentNo.Equals(documentNo) && h.DocumentStatus.Equals(documentStatus)
                    && h.DocumentID.Equals(documentId)).FirstOrDefault().ReferenceDocumentID;

            if (context.InvGiftVoucherPurchaseDetails.Any(d => d.IsTransfer == false && d.InvGiftVoucherPurchaseHeaderID.Equals(purchaseHeaderID) && d.DocumentStatus.Equals(documentStatus)))
            {
                var queryBal = (
                        from ph in context.InvGiftVoucherPurchaseHeaders
                        join pd in context.InvGiftVoucherPurchaseDetails on ph.InvGiftVoucherPurchaseHeaderID equals pd.InvGiftVoucherPurchaseHeaderID
                        join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                        join gg in context.InvGiftVoucherGroups on pm.InvGiftVoucherGroupID equals gg.InvGiftVoucherGroupID
                        join gb in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                        join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                        join py in context.PaymentMethods on ph.PaymentMethodID equals py.PaymentMethodID
                        join l in context.Locations on ph.LocationID equals l.LocationID
                        where ph.InvGiftVoucherPurchaseHeaderID.Equals(purchaseHeaderID) && ph.DocumentStatus.Equals(documentStatus)
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
                            l.LocationCode,
                            l.LocationName,
                            pd.VoucherAmount,
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
                                                from pd in context.InvGiftVoucherPurchaseDetails
                                                join pm in context.InvGiftVoucherMasters
                                                on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                                where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                                && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                                select pm.VoucherSerial).FirstOrDefault()
                                      let count = (
                                                from pd in context.InvGiftVoucherPurchaseDetails
                                                join pm in context.InvGiftVoucherMasters
                                                on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                                where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID
                                                && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                                select pm.VoucherSerial).Count()
                                      let balCount = (
                                                 from pd in context.InvGiftVoucherPurchaseDetails
                                                 join pm in context.InvGiftVoucherMasters
                                                 on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                                 where qs.InvGiftVoucherPurchaseHeaderID == pd.InvGiftVoucherPurchaseHeaderID && pd.IsTransfer == false
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
                                          FieldString5 = "",
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
                                          //FieldString19 = (qs.VoucherValue * count),
                                          FieldDecimal1 = "",
                                          FieldDecimal2 = "",
                                          FieldDecimal3 = "",
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


            return dsTransactionData;

        }

        public DataTable GetTransferofGiftVouchersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            var query = context.InvGiftVoucherTransferNoteHeaders.Where("DocumentStatus= @0 AND DocumentID = @1", 1, autoGenerateInfo.DocumentID);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        //switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        //{
                            //case "LgsSupplierID":
                            //    query = (from qr in query
                            //             join jt in context.LgsSuppliers.Where(
                            //                 "" +
                            //                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                            //                 " >= " + "@0 AND " +
                            //                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                            //                 " <= @1",
                            //                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                            //                 (reportConditionsDataStruct.ConditionTo.Trim())
                            //                 ) on qr.SupplierID equals jt.LgsSupplierID
                            //             select qr
                            //            );
                            //    break;
                            //default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                //break;
                        //}
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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "InvGiftVoucherBookCodeID":
                                //query = (from qr in query
                                //         join td in context.InvGiftVoucherTransferNoteDetails on qr.InvGiftVoucherTransferNoteHeaderID equals td.InvGiftVoucherTransferNoteHeaderID
                                //         join pm in context.InvGiftVoucherMasters on td.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                //         join jt in context.InvGiftVoucherBookCodes.Where(
                                //             "" +
                                //             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                //             " >= " + "@0 AND " +
                                //             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                //             " <= @1",
                                //             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                //             (reportConditionsDataStruct.ConditionTo.Trim())
                                //             ) on pm.InvGiftVoucherBookCodeID equals jt.InvGiftVoucherBookCodeID
                                //         select qr
                                //        );
                                break;
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
                            case "ToLocationID":
                                query = (from qr in query
                                         join jt in context.Locations.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.ToLocationID equals jt.LocationID
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
                        query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                    }
                }
            }

            var queryResult = (from h in query.AsNoTracking()
                               let bookName = (from pd in context.InvGiftVoucherTransferNoteDetails
                                               join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                               join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                               where h.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                               select bc.BookName).FirstOrDefault()
                               let bookValue = (from pd in context.InvGiftVoucherTransferNoteDetails
                                               join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                               join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                               where h.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                               && pm.VoucherType == pd.VoucherType && pd.VoucherType == 1
                                               select bc.GiftVoucherValue).FirstOrDefault()
                               let firstV = (from pd in context.InvGiftVoucherTransferNoteDetails
                                             join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                             where h.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                             orderby pm.VoucherSerial
                                             select pm.VoucherSerial).FirstOrDefault()
                               let lastV = (from pd in context.InvGiftVoucherTransferNoteDetails
                                             join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                             where h.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                             orderby pm.VoucherSerial descending
                                             select pm.VoucherSerial).FirstOrDefault()
                               let count = (from pd in context.InvGiftVoucherTransferNoteDetails
                                           join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                           join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                           where h.InvGiftVoucherTransferNoteHeaderID == pd.InvGiftVoucherTransferNoteHeaderID
                                           select pm.VoucherSerial).Count()
                               join l in context.Locations on h.LocationID equals l.LocationID
                               join tl in context.Locations on h.ToLocationID equals tl.LocationID
                               select
                                   new
                                   {
                                       FieldString1 = l.LocationName,
                                       FieldString2 = tl.LocationName,
                                       FieldString3 = DbFunctions.TruncateTime(h.DocumentDate),
                                       FieldString4 = h.DocumentNo,
                                       FieldString5 = bookName,
                                       FieldString6 = h.ReferenceNo,
                                       FieldString7 = h.Remark,
                                       FieldString8 = firstV + " - " + lastV,
                                       FieldDecimal1 = count,
                                       FieldDecimal2 = (bookValue > 0 ? (count * bookValue) : 0),
                                       
                                   }); //.ToArray();

            //DataTable mm = queryResult.ToDataTable();

            DataTable dtQueryResult = new DataTable();

            if (!reportGroupDataStructList.Any(g => g.IsResultGroupBy))
            {
                StringBuilder sbOrderByColumns = new StringBuilder();

                if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
                {
                    foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                    { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                    sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                    dtQueryResult = queryResult.AsNoTracking().OrderBy(sbOrderByColumns.ToString())
                                               .ToDataTable();
                }
                else
                { dtQueryResult = queryResult.AsNoTracking().ToDataTable(); }

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
            IQueryable qryResulth = context.InvGiftVoucherTransferNoteHeaders.Where("DocumentStatus == @0 AND DocumentId==@1", 1, autoGenerateInfo.DocumentID);
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
                case "LocationID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "LocationName")
                    {
                        qryResulth = qryResulth.Join(context.Locations, "LocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                                               .GroupBy("new(LocationName)", "new(LocationName)")
                                               .OrderBy("Key.LocationName").Select("Key.LocationName");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "LocationCode")
                    {
                        qryResulth = qryResulth.Join(context.Locations, "LocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)")
                                               .GroupBy("new(LocationCode)", "new(LocationCode)")
                                               .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                    }
                    break;
                case "ToLocationID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "LocationName")
                    {
                        qryResulth = qryResulth.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), "LocationID", "new(inner.LocationName)")
                                               .GroupBy("new(LocationName)", "new(LocationName)")
                                               .OrderBy("Key.LocationName").Select("Key.LocationName");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "LocationCode")
                    {
                        qryResulth = qryResulth.Join(context.Locations, reportDataStruct.DbColumnName.Trim(), "LocationID", "new(inner.LocationCode)")
                                               .GroupBy("new(LocationCode)", "new(LocationCode)")
                                               .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                    }
                    break;
                case "InvGiftVoucherBookCodeID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "BookCode")
                    {
                        qryResulth = qryResulth.Join(context.InvGiftVoucherBookCodes, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.BookCode)")
                               .GroupBy("new(BookCode)", "new(BookCode)")
                                           .OrderBy("Key.BookCode")
                                           .Select("Key.BookCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "BookName")
                    {
                        qryResulth = qryResulth
                                .Join(context.InvGiftVoucherTransferNoteDetails, "InvGiftVoucherTransferNoteHeaderID", "InvGiftVoucherTransferNoteHeaderID", "new(inner.InvGiftVoucherMasterID)")
                                .Join(context.InvGiftVoucherMasters, "InvGiftVoucherMasterID", "InvGiftVoucherMasterID", "new(inner.InvGiftVoucherBookCodeID)")
                                .Join(context.InvGiftVoucherBookCodes, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.BookName)")
                                .GroupBy("new(BookName)", "new(BookName)")
                                           .OrderBy("Key.BookName")
                                           .Select("Key.BookName");
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
