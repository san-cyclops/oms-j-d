using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Validation;
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
    public class InvGiftVoucherPurchaseOrderService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Save Gift voucher purchase order
        /// </summary>
        /// <param name="invGiftVoucherPurchaseOrderHeader"></param>
        /// <param name="invGiftVoucherPurchaseOrderDetailsTemp"></param>
        public bool SaveGiftVoucherPurchaseOrder(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader, List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailsTemp, out string newDocumentNo, string formName = "", bool isTStatus = false)
        {
            try
            {
                using (ERPDbContext context = new ERPDbContext())
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (invGiftVoucherPurchaseOrderHeader.DocumentStatus.Equals(1))
                        {
                            InvGiftVoucherPurchaseOrderService invGiftVoucherPurchaseOrderService = new InvGiftVoucherPurchaseOrderService();
                            LocationService locationService = new LocationService();
                            invGiftVoucherPurchaseOrderHeader.DocumentNo = invGiftVoucherPurchaseOrderService.GetDocumentNo(formName, invGiftVoucherPurchaseOrderHeader.LocationID, locationService.GetLocationsByID(invGiftVoucherPurchaseOrderHeader.LocationID).LocationCode,invGiftVoucherPurchaseOrderHeader.DocumentID, false).Trim();
                        }

                        newDocumentNo = invGiftVoucherPurchaseOrderHeader.DocumentNo;

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        if (invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID.Equals(0))
                            context.InvGiftVoucherPurchaseOrderHeaders.Add(invGiftVoucherPurchaseOrderHeader);
                        else
                            context.Entry(invGiftVoucherPurchaseOrderHeader).State = EntityState.Modified;

                        context.SaveChanges();

                        #region Transaction Details

                        var invGiftVoucherPurchaseOrderDetailSaveQuery =
                            (from pd in invGiftVoucherPurchaseOrderDetailsTemp
                             select new  //InvGiftVoucherPurchaseOrderDetail
                                 {
                                     InvGiftVoucherPurchaseOrderDetailID = 0, //pd.InvGiftVoucherPurchaseOrderDetailID,
                                     GiftVoucherPurchaseOrderDetailID = 0,
                                     InvGiftVoucherPurchaseOrderHeaderID = invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID,
                                     CompanyID = invGiftVoucherPurchaseOrderHeader.CompanyID,
                                     LocationID = invGiftVoucherPurchaseOrderHeader.LocationID,
                                     DocumentID = invGiftVoucherPurchaseOrderHeader.DocumentID,
                                     DocumentDate = invGiftVoucherPurchaseOrderHeader.DocumentDate,
                                     LineNo = pd.LineNo,
                                     InvGiftVoucherMasterID = pd.InvGiftVoucherMasterID,
                                     //VoucherNo = pd.VoucherNo,
                                     //VoucherSerial = pd.VoucherSerial,
                                     NumberOfCount = pd.NumberOfCount,
                                     VoucherAmount = pd.VoucherValue,
                                     VoucherType = invGiftVoucherPurchaseOrderHeader.VoucherType,
                                     IsPurchase = false,
                                     DocumentStatus = invGiftVoucherPurchaseOrderHeader.DocumentStatus,
                                     GroupOfCompanyID = invGiftVoucherPurchaseOrderHeader.GroupOfCompanyID,
                                     CreatedUser = invGiftVoucherPurchaseOrderHeader.CreatedUser,
                                     CreatedDate = invGiftVoucherPurchaseOrderHeader.CreatedDate,
                                     ModifiedUser = invGiftVoucherPurchaseOrderHeader.ModifiedUser,
                                     ModifiedDate = invGiftVoucherPurchaseOrderHeader.ModifiedDate,
                                     DataTransfer = invGiftVoucherPurchaseOrderHeader.DataTransfer
                                 }).ToList();
                        

                        DataTable DTITEM = new DataTable();
                        DTITEM = invGiftVoucherPurchaseOrderDetailSaveQuery.ToDataTable();

                        context.Set<InvGiftVoucherPurchaseOrderDetail>().Delete(context.InvGiftVoucherPurchaseOrderDetails.Where(p => p.InvGiftVoucherPurchaseOrderHeaderID.Equals(invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID) && p.LocationID.Equals(invGiftVoucherPurchaseOrderHeader.LocationID) && p.DocumentID.Equals(invGiftVoucherPurchaseOrderHeader.DocumentID)).AsQueryable());
                        context.SaveChanges();

                        //CommonService.BulkInsert(DTITEM, "InvGiftVoucherPurchaseOrderDetail");

                        string mergeSql = CommonService.MergeSqlToForTrans(DTITEM, "InvGiftVoucherPurchaseOrderDetail");
                        CommonService.BulkInsert(DTITEM, "InvGiftVoucherPurchaseOrderDetail", mergeSql);
                        
                        #region Process

                        var parameter = new DbParameter[] 
                        { 
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@InvGiftVoucherPurchaseOrderHeaderID", Value=invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderHeaderID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=invGiftVoucherPurchaseOrderHeader.LocationID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentID", Value=invGiftVoucherPurchaseOrderHeader.DocumentID},
                            new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentStatus", Value=invGiftVoucherPurchaseOrderHeader.DocumentStatus},
                        };

                        if (CommonService.ExecuteStoredProcedure("spInvGiftVoucherPurchaseOrderNoteSave", parameter))
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        #endregion

                        
                        #endregion



                        //context.InvGiftVoucherPurchaseOrderHeaders.Add(AddInvGiftVoucherPurchaseOrderDetails(invGiftVoucherPurchaseOrderHeader, invGiftVoucherPurchaseOrderDetailsTemp));
                        //DateTime t1 = DateTime.UtcNow;
                        //context.SaveChanges();

                        //Update Voucher status on Voucher Master
                        //if (invGiftVoucherPurchaseOrderHeader.DocumentStatus.Equals(1))
                        //{
                        //    // UpdateInvGiftVoucherMaster(invGiftVoucherPurchaseOrderDetailsTemp);
                        //   // UpdateInvGiftVoucherMaster(invGiftVoucherPurchaseOrderDetailSaveQuery.ToList());
                        //}
                        //transactionScope.Complete();
                        //string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
                        //return true;
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

        public void UpdateInvGiftVoucherMaster(List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailsTemp)
        {
            try
            {
                foreach (InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetailTemp in invGiftVoucherPurchaseOrderDetailsTemp)
                {
                    context.InvGiftVoucherMasters.Update(pd => pd.InvGiftVoucherMasterID == invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherMasterID && pd.VoucherNo == invGiftVoucherPurchaseOrderDetailTemp.VoucherNo && pd.VoucherSerial == invGiftVoucherPurchaseOrderDetailTemp.VoucherSerial, pd => new InvGiftVoucherMaster { VoucherStatus = 1 });
                    //context.SaveChanges();
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

        /// <summary>
        /// Create InvGiftVoucherPurchaseOrderDetail list using InvGiftVoucherPurchaseOrderDetailTemp
        /// </summary>
        /// <param name="invGiftVoucherPurchaseOrderHeader"></param>
        /// <param name="invGiftVoucherPurchaseOrderDetailsTemp"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseOrderHeader AddInvGiftVoucherPurchaseOrderDetails(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader, List<InvGiftVoucherPurchaseOrderDetailTemp> invGiftVoucherPurchaseOrderDetailsTemp)
        {
            DateTime t1 = DateTime.UtcNow;
            // InvGiftVoucherPurchaseOrderDetail list for InvGiftVoucherPurchaseOrderHeader 
            invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderDetails = new List<InvGiftVoucherPurchaseOrderDetail>();
            foreach (InvGiftVoucherPurchaseOrderDetailTemp invGiftVoucherPurchaseOrderDetailTemp in invGiftVoucherPurchaseOrderDetailsTemp)
            {
                InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetail = new InvGiftVoucherPurchaseOrderDetail();

                invGiftVoucherPurchaseOrderDetail.DocumentStatus = invGiftVoucherPurchaseOrderHeader.DocumentStatus;
                invGiftVoucherPurchaseOrderDetail.DocumentDate = invGiftVoucherPurchaseOrderHeader.DocumentDate;
                invGiftVoucherPurchaseOrderDetail.CompanyID = invGiftVoucherPurchaseOrderHeader.CompanyID;
                invGiftVoucherPurchaseOrderDetail.LocationID = invGiftVoucherPurchaseOrderHeader.LocationID;
                invGiftVoucherPurchaseOrderDetail.CreatedDate = invGiftVoucherPurchaseOrderHeader.CreatedDate;
                invGiftVoucherPurchaseOrderDetail.DocumentID = invGiftVoucherPurchaseOrderHeader.DocumentID;
                invGiftVoucherPurchaseOrderDetail.DataTransfer = invGiftVoucherPurchaseOrderHeader.DataTransfer;
                invGiftVoucherPurchaseOrderDetail.CreatedDate = invGiftVoucherPurchaseOrderHeader.CreatedDate;
                invGiftVoucherPurchaseOrderDetail.ModifiedDate = invGiftVoucherPurchaseOrderHeader.ModifiedDate;

                invGiftVoucherPurchaseOrderDetail.LineNo = invGiftVoucherPurchaseOrderDetailTemp.LineNo;
                invGiftVoucherPurchaseOrderDetail.InvGiftVoucherMasterID = invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherMasterID;
                //invGiftVoucherPurchaseOrderDetail.ProductID = invGiftVoucherPurchaseOrderDetailTemp.ProductID;
                //invGiftVoucherPurchaseOrderDetail.ProductCode = invGiftVoucherPurchaseOrderDetailTemp.ProductCode;
                //invGiftVoucherPurchaseOrderDetail.UnitOfMeasureID = invGiftVoucherPurchaseOrderDetailTemp.UnitOfMeasureID;
                //invGiftVoucherPurchaseOrderDetail.BaseUnitID = invGiftVoucherPurchaseOrderDetailTemp.BaseUnitID;
                //invGiftVoucherPurchaseOrderDetail.UnitOfMeasure = invGiftVoucherPurchaseOrderDetailTemp.UnitOfMeasure;
                //invGiftVoucherPurchaseOrderDetail.ConvertFactor = invGiftVoucherPurchaseOrderDetailTemp.ConvertFactor;
                //invGiftVoucherPurchaseOrderDetail.OrderQty = invGiftVoucherPurchaseOrderDetailTemp.OrderQty;
                //invGiftVoucherPurchaseOrderDetail.Qty = invGiftVoucherPurchaseOrderDetailTemp.Qty;
                //invGiftVoucherPurchaseOrderDetail.FreeQty = invGiftVoucherPurchaseOrderDetailTemp.FreeQty;
                //invGiftVoucherPurchaseOrderDetail.CurrentQty = invGiftVoucherPurchaseOrderDetailTemp.CurrentQty;
                //invGiftVoucherPurchaseOrderDetail.CostPrice = invGiftVoucherPurchaseOrderDetailTemp.CostPrice;
                //invGiftVoucherPurchaseOrderDetail.SellingPrice = invGiftVoucherPurchaseOrderDetailTemp.SellingPrice;
                //invGiftVoucherPurchaseOrderDetail.GrossAmount = invGiftVoucherPurchaseOrderDetailTemp.GrossAmount;
                //invGiftVoucherPurchaseOrderDetail.DiscountPercentage = invGiftVoucherPurchaseOrderDetailTemp.DiscountPercentage;
                //invGiftVoucherPurchaseOrderDetail.DiscountAmount = invGiftVoucherPurchaseOrderDetailTemp.DiscountAmount;
                //invGiftVoucherPurchaseOrderDetail.SubTotalDiscount = invGiftVoucherPurchaseOrderDetailTemp.SubTotalDiscount;
                //invGiftVoucherPurchaseOrderDetail.TaxAmount1 = invGiftVoucherPurchaseOrderDetailTemp.TaxAmount1;
                //invGiftVoucherPurchaseOrderDetail.TaxAmount2 = invGiftVoucherPurchaseOrderDetailTemp.TaxAmount2;
                //invGiftVoucherPurchaseOrderDetail.TaxAmount3 = invGiftVoucherPurchaseOrderDetailTemp.TaxAmount3;
                //invGiftVoucherPurchaseOrderDetail.TaxAmount4 = invGiftVoucherPurchaseOrderDetailTemp.TaxAmount4;
                //invGiftVoucherPurchaseOrderDetail.TaxAmount5 = invGiftVoucherPurchaseOrderDetailTemp.TaxAmount5;
                //invGiftVoucherPurchaseOrderDetail.NetAmount = invGiftVoucherPurchaseOrderDetailTemp.NetAmount;


                invGiftVoucherPurchaseOrderHeader.InvGiftVoucherPurchaseOrderDetails.Add(invGiftVoucherPurchaseOrderDetail);
            }
            string mins = (DateTime.UtcNow - t1).TotalMinutes.ToString();
            return invGiftVoucherPurchaseOrderHeader;
        }

        /// <summary>
        /// Get Saved InvPurchaseOrderHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseOrderHeader GetInvGiftVoucherPurchaseOrderHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.InvGiftVoucherPurchaseOrderHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="InvDepartmentCode"></param>
        /// <returns></returns>
        public InvGiftVoucherPurchaseOrderHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.InvGiftVoucherPurchaseOrderHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        public InvGiftVoucherPurchaseOrderHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.InvGiftVoucherPurchaseOrderHeaders.Where(po => po.DocumentNo == documentNo.Trim() && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        public InvGiftVoucherPurchaseOrderHeader GetSavedDocumentDetailsByDocumentID(long documentID)
        {
            return context.InvGiftVoucherPurchaseOrderHeaders.Where(po => po.InvGiftVoucherPurchaseOrderHeaderID == documentID && po.DocumentStatus.Equals(1)).FirstOrDefault();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>(); 
            pausedDocumentList = context.InvGiftVoucherPurchaseOrderHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        public string[] GetVoucherSerial()
        {
            List<string> availableVoucherCount = new List<string>();
            availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.VoucherStatus == 0 && s.IsDelete == false).Select(s=>s.VoucherSerial).ToList();

            string[] Vouchers = availableVoucherCount.ToArray();
            return Vouchers;
        }

        public string[] GetVoucherSerialByBookID(long bookID)
        {
            List<string> availableVoucherCount = new List<string>();
            availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherSerial).ToList();

            string[] Vouchers = availableVoucherCount.ToArray();
            return Vouchers;
        }

        public string[] GetVoucherNoByBookID(long bookID)
        {
            List<string> availableVoucherCount = new List<string>();
            availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherNo).ToList();

            string[] Vouchers = availableVoucherCount.ToArray();
            return Vouchers;
        }

        public string[] GetAllDocumentNumbers(int documentID, int locationID)
        {
            List<string> documentNoList = context.InvGiftVoucherPurchaseOrderHeaders.Where(q => q.DocumentID == documentID && q.LocationID == locationID).Select(q => q.DocumentNo).ToList();
            return documentNoList.ToArray();
        }

        public DataTable GetPendingPurchaseOrderDocuments()
        {
            var sql = (from poh in context.InvGiftVoucherPurchaseOrderHeaders
                       //join pod in context.InvGiftVoucherPurchaseOrderDetails on poh.InvGiftVoucherPurchaseOrderHeaderID equals pod.InvGiftVoucherPurchaseOrderHeaderID
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

        public string[] GetAllDocumentNumbersToGRN(int locationID)
        {
            //List<string> documentNoList = context.InvGiftVoucherPurchaseOrderHeaders.Where(q => q.LocationID == locationID 
            //    && q.DocumentStatus.Equals(1)).Select(q => q.DocumentNo).ToList();
            //
            
            var query = (from ph in context.InvGiftVoucherPurchaseOrderHeaders
                       join pd in context.InvGiftVoucherPurchaseOrderDetails on ph.InvGiftVoucherPurchaseOrderHeaderID equals pd.InvGiftVoucherPurchaseOrderHeaderID
                       where ph.LocationID == locationID && pd.IsPurchase == false && ph.DocumentStatus.Equals(1)
                       group pd by new
                       {
                           ph.DocumentNo,
                       } into poGroup
                       orderby poGroup.Key.DocumentNo descending 
                       select new
                       {
                           DocumentNo = poGroup.Key.DocumentNo,
                       }
                       ).ToList();

            List<string> documentNoList = query.Select(a => a.DocumentNo).ToList();

            return documentNoList.ToArray();
        }

        public DataTable GetPendingPurchaseOrderDocumentsToGRN()
        {
            var sql = (from ph in context.InvGiftVoucherPurchaseOrderHeaders
                       join pd in context.InvGiftVoucherPurchaseOrderDetails on ph.InvGiftVoucherPurchaseOrderHeaderID equals pd.InvGiftVoucherPurchaseOrderHeaderID
                       join l in context.Locations on ph.LocationID equals l.LocationID
                       join s in context.LgsSuppliers on ph.SupplierID equals s.LgsSupplierID
                       where ph.DocumentStatus == 1 && pd.IsPurchase == false
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
                           DocumentDate = poGroup.Key.DocumentDate,
                           DocumentLocation = poGroup.Key.DocumentLocation,
                           SupplierName = poGroup.Key.SupplierName
                       }).ToArray();

            return sql.ToDataTable();
        }

        public DataTable GetAllActiveGiftVoucherSerialsDataTableByVoucherType(int voucherType, int voucherGroupID, int voucherBookID)
        {
            DataTable tblGiftVoucherMasters = new DataTable();
            var query = from u in context.InvGiftVoucherMasters
                        join b in context.InvGiftVoucherBookCodes on u.InvGiftVoucherBookCodeID equals b.InvGiftVoucherBookCodeID
                        join g in context.InvGiftVoucherGroups on u.InvGiftVoucherGroupID equals g.InvGiftVoucherGroupID
                        where (u.IsDelete == false && u.VoucherType == voucherType && u.InvGiftVoucherGroupID == voucherGroupID && u.InvGiftVoucherBookCodeID == voucherBookID
                        && u.VoucherStatus == 0)
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
                        && u.VoucherStatus == 0)
                        select new { u.VoucherNo, u.VoucherSerial, u.InvGiftVoucherGroup.GiftVoucherGroupCode, u.InvGiftVoucherGroup.GiftVoucherGroupName, u.InvGiftVoucherBookCode.BookCode, u.InvGiftVoucherBookCode.BookName, u.GiftVoucherValue, u.PageCount };
            return tblGiftVoucherMasters = query.ToDataTable();
        }
        
        public List<InvGiftVoucherPurchaseOrderDetail> GetAllPurchaseOrderDetail(InvGiftVoucherPurchaseOrderHeader invGiftVoucherPurchaseOrderHeader)
        {
            InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetailTemp;

            List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailTempList = new List<InvGiftVoucherPurchaseOrderDetail>();

            var giftVoucherPurchaseOrderDetailTemp = (from bm in context.InvGiftVoucherMasters
                                      join pd in context.InvGiftVoucherPurchaseOrderDetails on bm.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                      where pd.LocationID.Equals(invGiftVoucherPurchaseOrderHeader.LocationID) && pd.CompanyID.Equals(invGiftVoucherPurchaseOrderHeader.CompanyID)
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
                                          pd.LineNo,
                                          bm.InvGiftVoucherMasterID,
                                          bm.InvGiftVoucherBookCodeID,
                                          bm.InvGiftVoucherGroupID,
                                          bm.VoucherNo,
                                          bm.VoucherSerial,
                                          pd.NumberOfCount,
                                          pd.VoucherAmount,
                                          invGiftVoucherPurchaseOrderHeader.DiscountPercentage,
                                          invGiftVoucherPurchaseOrderHeader.DiscountAmount,
                                          invGiftVoucherPurchaseOrderHeader.GrossAmount,
                                          invGiftVoucherPurchaseOrderHeader.NetAmount,
                                          pd.VoucherType,
                                          pd.DocumentStatus
                                      }).ToArray();

            foreach (var tempGiftVoucher in giftVoucherPurchaseOrderDetailTemp)
            {
                invGiftVoucherPurchaseOrderDetailTemp = new InvGiftVoucherPurchaseOrderDetail();
                invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherPurchaseOrderDetailID = tempGiftVoucher.InvGiftVoucherPurchaseOrderDetailID;
                invGiftVoucherPurchaseOrderDetailTemp.GiftVoucherPurchaseOrderDetailID = tempGiftVoucher.GiftVoucherPurchaseOrderDetailID;
                invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherPurchaseOrderHeaderID = tempGiftVoucher.InvGiftVoucherPurchaseOrderHeaderID;
                invGiftVoucherPurchaseOrderDetailTemp.CompanyID = tempGiftVoucher.CompanyID;
                invGiftVoucherPurchaseOrderDetailTemp.LocationID = tempGiftVoucher.LocationID;
                invGiftVoucherPurchaseOrderDetailTemp.DocumentNo = tempGiftVoucher.DocumentNo;
                invGiftVoucherPurchaseOrderDetailTemp.DocumentID = tempGiftVoucher.DocumentID;
                invGiftVoucherPurchaseOrderDetailTemp.DocumentDate = tempGiftVoucher.DocumentDate;
                invGiftVoucherPurchaseOrderDetailTemp.LineNo = tempGiftVoucher.LineNo;
                invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherMasterID = tempGiftVoucher.InvGiftVoucherMasterID;
                invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherBookCodeID = tempGiftVoucher.InvGiftVoucherBookCodeID;
                invGiftVoucherPurchaseOrderDetailTemp.InvGiftVoucherGroupID = tempGiftVoucher.InvGiftVoucherGroupID;
                invGiftVoucherPurchaseOrderDetailTemp.VoucherNo = tempGiftVoucher.VoucherNo;
                invGiftVoucherPurchaseOrderDetailTemp.VoucherSerial = tempGiftVoucher.VoucherSerial;
                invGiftVoucherPurchaseOrderDetailTemp.NumberOfCount = tempGiftVoucher.NumberOfCount;
                invGiftVoucherPurchaseOrderDetailTemp.DiscountPercentage = tempGiftVoucher.DiscountPercentage;
                invGiftVoucherPurchaseOrderDetailTemp.DiscountAmount = tempGiftVoucher.DiscountAmount;
                invGiftVoucherPurchaseOrderDetailTemp.GrossAmount = tempGiftVoucher.GrossAmount;
                invGiftVoucherPurchaseOrderDetailTemp.NetAmount = tempGiftVoucher.NetAmount;
                if (tempGiftVoucher.VoucherType == 1)
                {
                    invGiftVoucherPurchaseOrderDetailTemp.VoucherAmount = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseOrderDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                }
                else
                {
                    invGiftVoucherPurchaseOrderDetailTemp.VoucherValue = tempGiftVoucher.VoucherAmount;
                    invGiftVoucherPurchaseOrderDetailTemp.GiftVoucherPercentage = tempGiftVoucher.VoucherAmount;
                }
                invGiftVoucherPurchaseOrderDetailTemp.VoucherType = tempGiftVoucher.VoucherType;
                invGiftVoucherPurchaseOrderDetailTemp.DocumentStatus = tempGiftVoucher.DocumentStatus;
                invGiftVoucherPurchaseOrderDetailTempList.Add(invGiftVoucherPurchaseOrderDetailTemp);
            }

            return invGiftVoucherPurchaseOrderDetailTempList.OrderBy(pd => pd.LineNo).ToList();
        }

        public long GetVoucherQtyByBookID(int bookID, string fromNo, string toNo, int voucherStatus)
        {
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (voucherStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(fromNo) >= 0 && s.VoucherSerial.CompareTo(toNo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).Count();
                }
                else
                {
                    bookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNo.CompareTo(fromNo) >= 0 && s.VoucherNo.CompareTo(toNo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).Count();

                    availableBookCount = availableBookCount % invGiftVoucherBookCode.PageCount;
                }
            }

            return availableBookCount;
        }

        public DataTable GetPurchaseOrderTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.InvGiftVoucherPurchaseOrderHeaders
                        join pd in context.InvGiftVoucherPurchaseOrderDetails on ph.InvGiftVoucherPurchaseOrderHeaderID equals pd.InvGiftVoucherPurchaseOrderHeaderID
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


            var queryResult = (from qs in query
                      let firstV = (
                                from pd in context.InvGiftVoucherPurchaseOrderDetails
                                join pm in context.InvGiftVoucherMasters 
                                on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                && qs.InvGiftVoucherBookCodeID == pm.InvGiftVoucherBookCodeID
                                orderby pm.VoucherSerial
                                select pm.VoucherSerial).FirstOrDefault()
                      let count = (
                                from pd in context.InvGiftVoucherPurchaseOrderDetails
                                join pm in context.InvGiftVoucherMasters
                                on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
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
                                from pd in context.InvGiftVoucherPurchaseOrderDetails
                                join pm in context.InvGiftVoucherMasters
                                on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                where qs.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
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
                          
                      }).ToArray();
            
            return queryResult.ToDataTable();

        }

        public DataTable GetPurchaseOrdersDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            
            //StringBuilder selectFields = new StringBuilder();
            var query = context.InvGiftVoucherPurchaseOrderHeaders.Where("DocumentStatus = @0 AND DocumentId =@1", 1, autoGenerateInfo.DocumentID);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()).AddDays(1).AddSeconds(-1)); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (long)))
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

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
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

            var queryResult = (from h in query
                               let bookName = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                               join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                               join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                               where h.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                               select bc.BookName).FirstOrDefault()
                               let bookValue = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                                join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                                join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                                where h.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                                && pm.VoucherType == pd.VoucherType && pd.VoucherType == 1
                                                select bc.GiftVoucherValue).FirstOrDefault()
                               let firstV = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                             join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                             join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                             where h.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                             orderby pm.VoucherSerial
                                             select pm.VoucherSerial).FirstOrDefault()
                               let lastV = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                            join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                            join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                            where h.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                            orderby pm.VoucherSerial descending
                                            select pm.VoucherSerial).FirstOrDefault()

                               let count = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                            join pm in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals pm.InvGiftVoucherMasterID
                                            join bc in context.InvGiftVoucherBookCodes on pm.InvGiftVoucherBookCodeID equals bc.InvGiftVoucherBookCodeID
                                            where h.InvGiftVoucherPurchaseOrderHeaderID == pd.InvGiftVoucherPurchaseOrderHeaderID
                                            select pm.VoucherSerial).Count()
                               join s in context.LgsSuppliers on h.SupplierID equals s.LgsSupplierID
                               join l in context.Locations on h.LocationID equals l.LocationID
                               select
                                   new
                                   {
                                       FieldString1 = DbFunctions.TruncateTime(h.DocumentDate),
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
            IQueryable qryResult = context.InvGiftVoucherPurchaseOrderHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID); //.GroupBy("" + reportDataStruct.DbColumnName.Trim() + "", "" + reportDataStruct.DbColumnName.Trim() + "");            

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSupplierID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "SupplierCode")
                    {
                        qryResult = qryResult.Join(context.LgsSuppliers, "SupplierID", reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierCode)")
                            .GroupBy("new(SupplierCode)", "new(SupplierCode)")
                            .OrderBy("Key.SupplierCode").Select("Key.SupplierCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "SupplierName")
                    {
                        qryResult = qryResult.Join(context.LgsSuppliers, "SupplierID", reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierName)")
                            .GroupBy("new(SupplierName)", "new(SupplierName)")
                            .OrderBy("Key.SupplierName").Select("Key.SupplierName");
                    }
                    break;
                case "LocationID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "LocationName")
                    {
                        qryResult = qryResult.Join(context.Locations, "LocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                                .GroupBy("new(LocationName)", "new(LocationName)")
                                  .OrderBy("Key.LocationName").Select("Key.LocationName");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "LocationCode")
                    {
                        qryResult = qryResult.Join(context.Locations, "LocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)")
                                .GroupBy("new(LocationCode)", "new(LocationCode)")
                                  .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                    }
                    break;
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
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
            else
            {
                foreach (var item in qryResult)
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
