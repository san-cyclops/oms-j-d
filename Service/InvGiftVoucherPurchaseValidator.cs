using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;

namespace Service
{
    public class InvGiftVoucherPurchaseValidator
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Validate VoucherQty
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherQty"></param>
        /// <param name="voucherPageStatus"></param>
        /// <returns></returns>
        public bool ValidateVoucherQty(int bookID, int voucherQty, int voucherPageStatus, long referenceDocumentID)
        {
            bool isValidated = false;
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (voucherPageStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    if (referenceDocumentID == 0)
                    {
                        availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 1 && s.IsDelete == false).Count();
                    }
                    else
                    {
                        availableBookCount = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                             join gv in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals
                                                 gv.InvGiftVoucherMasterID
                                             where
                                                 gv.InvGiftVoucherBookCodeID == bookID && pd.DocumentStatus == 1 && gv.VoucherStatus == 1 &&
                                                 pd.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID && pd.IsPurchase == false
                                             select pd).Count();
                    }
                    if (availableBookCount >= voucherQty)
                    {
                        isValidated = true;
                    }
                }
                else
                {
                    bookCount = voucherQty % invGiftVoucherBookCode.PageCount;
                    if (bookCount == 0 && invGiftVoucherBookCode.CurrentSerialNo >= (voucherQty / invGiftVoucherBookCode.PageCount))
                    {
                        if (referenceDocumentID == 0)
                        {
                            availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 1 && s.IsDelete == false).Count();
                        }
                        else
                        {
                            availableBookCount = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                                  join gv in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals
                                                      gv.InvGiftVoucherMasterID
                                                  where
                                                      gv.InvGiftVoucherBookCodeID == bookID && pd.DocumentStatus == 1 && gv.VoucherStatus == 1 &&
                                                      pd.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID && pd.IsPurchase == false
                                                  select pd).Count();
                        }

                        if (availableBookCount >= voucherQty)
                        {
                            isValidated = true;
                        }
                    }
                }
            }

            return isValidated;
        }

        /// <summary>
        /// Validate VoucherNo From & To
        /// </summary>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        /// <returns></returns>
        public bool ValidateVoucherNoFromTo(string voucherNoFrom, string voucherNoTo)
        {
            bool isValidated = false;
            int voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
            int voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

            if (voucherNoSerialFrom > voucherNoSerialTo)
            { isValidated = false; }
            else
            { isValidated = true; }

            return isValidated;
        }

        /// <summary>
        /// Validate VoucherNo From & To Range
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        /// <returns></returns>
        public bool ValidateVoucherNoRange(int bookID, string voucherNoFrom, string voucherNoTo, long referenceDocumentID)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, validBookCount = 0, bookCount = 0, voucherNoSerialFrom = 0, voucherNoSerialTo = 0;

            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            
            if (invGiftVoucherBookCode != null)
            {
                voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
                voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

                availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                         where pd.InvGiftVoucherPurchaseHeaderID == referenceDocumentID &&
                                         gv.InvGiftVoucherBookCodeID == bookID && (gv.VoucherNoSerial >= voucherNoSerialFrom && gv.VoucherNoSerial <= voucherNoSerialTo) &&
                                         gv.VoucherStatus == 1 && gv.IsDelete == false
                                         select gv.VoucherNo).Count();

                validBookCount = availableVoucherCount % invGiftVoucherBookCode.PageCount;
                if (validBookCount == 0 && invGiftVoucherBookCode.CurrentSerialNo >= voucherNoSerialTo)
                {
                    bookCount = availableVoucherCount / invGiftVoucherBookCode.PageCount;
                    if (((voucherNoSerialTo - voucherNoSerialFrom) + 1) == bookCount)
                    {
                        isValidated = true;
                    }
                }
            }

            return isValidated;
        }

        /// <summary>
        /// Validate VoucherSerial From & To
        /// </summary>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        /// <returns></returns>
        public bool ValidateVoucherSerialFromTo(string voucherSerialFrom, string voucherSerialTo)
        {
            bool isValidated = false;
            int voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
            int voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();

            if (voucherSerialNoFrom > voucherSerialNoTo)
            { isValidated = false; }
            else
            { isValidated = true; }

            return isValidated;
        }

        /// <summary>
        /// Validate VoucherSerial From & To Range
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        /// <returns></returns>
        public bool ValidateVoucherSerialRange(int bookID, string voucherSerialFrom, string voucherSerialTo, long referenceDocumentID)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            InvGiftVoucherMaster invGiftVoucherMaster;
            if (invGiftVoucherBookCode != null)
            {
                int voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
                int voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();

                if (referenceDocumentID == 0)
                {
                    availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && 
                        (s.VoucherSerialNo >= voucherSerialNoFrom && s.VoucherSerialNo <= voucherSerialNoTo) && s.VoucherStatus == 1 && s.IsDelete == false).Count();

                    invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerialNo >= voucherSerialNoFrom && s.VoucherSerialNo <= voucherSerialNoTo) && s.VoucherStatus == 1 && s.IsDelete == false).FirstOrDefault();
                }
                else
                {
                    availableVoucherCount = (from pd in context.InvGiftVoucherPurchaseOrderDetails
                                          join gv in context.InvGiftVoucherMasters on pd.InvGiftVoucherMasterID equals gv.InvGiftVoucherMasterID
                                          where gv.InvGiftVoucherBookCodeID == bookID && pd.DocumentStatus == 1 && gv.VoucherStatus == 1 &&
                                              pd.InvGiftVoucherPurchaseOrderHeaderID == referenceDocumentID && pd.IsPurchase == false
                                          select pd).Count();

                    invGiftVoucherMaster = (from gv in context.InvGiftVoucherMasters
                                        join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                        where pd.InvGiftVoucherPurchaseHeaderID == referenceDocumentID &&
                                        gv.InvGiftVoucherBookCodeID == bookID && (gv.VoucherSerialNo >= voucherSerialNoFrom && gv.VoucherSerialNo <= voucherSerialNoTo) &&
                                        gv.VoucherStatus == 1 && gv.IsDelete == false
                                        select gv).FirstOrDefault();
                }

                if (invGiftVoucherMaster != null)
                {
                    if (invGiftVoucherMaster.PageCount == 0)
                    {
                        if (((voucherSerialNoTo - voucherSerialNoFrom) + 1) == availableVoucherCount)
                        {
                            isValidated = true;
                        }
                    }
                    else
                    {
                        bookCount = availableVoucherCount % invGiftVoucherBookCode.PageCount;
                        if (bookCount == 0) // && invGiftVoucherBookCode.CurrentSerialNo >= voucherNoTo)
                        {
                            if (((voucherSerialNoTo - voucherSerialNoFrom) + 1) == availableVoucherCount)
                            {
                                isValidated = true;
                            }
                        }
                    }
                }
                else
                {
                    
                }

            }

            return isValidated;
        }
        
        public bool ValidateExistsPausedVoucherSerial(List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp, string documentNo, int voucherType)
        {
            bool isValidated = false;
            InvGiftVoucherMaster invGiftVoucherMaster;
            List<InvGiftVoucherPurchaseDetail> invGiftVoucherPurchaseDetailList = new List<InvGiftVoucherPurchaseDetail>();

            var invGiftVoucherPurchaseDetailsGroupedList = (from m in invGiftVoucherPurchaseDetailsTemp
                                                            join pd in context.InvGiftVoucherPurchaseDetails on m.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                                            where m.VoucherType == voucherType && pd.DocumentNo != documentNo
                                                            select pd).ToList();

            if (!invGiftVoucherPurchaseDetailsGroupedList.Contains(null) && invGiftVoucherPurchaseDetailsGroupedList.Count >= 1)
            {
                isValidated = false;
                return isValidated;
            }
            else
            {
                isValidated = true;
            }

            //if (voucherType == 1)
            //{
                

                //var invGiftVoucherPurchaseDetailsGroupedList = (from m in invGiftVoucherPurchaseDetailsTemp
                //                                                join pd in context.InvGiftVoucherMasters on m.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                //                                                 where m.VoucherType == voucherType // m.DocumentStatus == 0
                //                                                group pd by new { pd.InvGiftVoucherGroupID, pd.GiftVoucherValue } into refGroup
                //                                                select new { keyVal = refGroup.Key, details = refGroup }).ToList();

                //foreach (var group in invGiftVoucherPurchaseDetailsGroupedList)
                //{

                //    foreach (InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp in invGiftVoucherPurchaseDetailsTemp)
                //    {
                //        invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == group.keyVal.InvGiftVoucherGroupID && u.GiftVoucherValue == group.keyVal.GiftVoucherValue && u.InvGiftVoucherMasterID == invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID && u.VoucherType == voucherType).FirstOrDefault();
                //        invGiftVoucherPurchaseDetailList.Add(context.InvGiftVoucherPurchaseDetails.Where(u => u.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID && u.DocumentNo != documentNo && u.VoucherType == invGiftVoucherMaster.VoucherType).FirstOrDefault());

                //        if (!invGiftVoucherPurchaseDetailList.Contains(null) && invGiftVoucherPurchaseDetailList.Count >= 1)
                //        {
                //            isValidated = false;
                //            return isValidated;
                //        }
                //        else
                //        {
                //            isValidated = true;
                //        }
                //    }
                //}
            //}
            //else
            //{
            //    //var invGiftVoucherPurchaseDetailsGroupedList = (from m in invGiftVoucherPurchaseDetailsTemp
                //                                                join pd in context.InvGiftVoucherMasters on m.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                //                                                where m.VoucherType == voucherType //m.DocumentStatus == 0
                //                                                group pd by new { pd.InvGiftVoucherGroupID, pd.GiftVoucherPercentage } into refGroup
                //                                                select new { keyVal = refGroup.Key, details = refGroup }).ToList();

                //foreach (var group in invGiftVoucherPurchaseDetailsGroupedList)
                //{

                //    foreach (InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetailTemp in invGiftVoucherPurchaseDetailsTemp)
                //    {
                //        invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == group.keyVal.InvGiftVoucherGroupID && u.GiftVoucherPercentage == group.keyVal.GiftVoucherPercentage && u.InvGiftVoucherMasterID == invGiftVoucherPurchaseDetailTemp.InvGiftVoucherMasterID && u.VoucherType == voucherType).FirstOrDefault();
                //        invGiftVoucherPurchaseDetailList.Add(context.InvGiftVoucherPurchaseDetails.Where(u => u.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID && u.DocumentNo != documentNo && u.VoucherType == invGiftVoucherMaster.VoucherType).FirstOrDefault());

                //        //if (invGiftVoucherPurchaseDetailList.Count >= 1)
                //        if (!invGiftVoucherPurchaseDetailList.Contains(null) && invGiftVoucherPurchaseDetailList.Count >= 1)
                //        {
                //            isValidated = false;
                //            return isValidated;
                //        }
                //        else
                //        {
                //            isValidated = true;
                //        }
                //    }
                //}
            //}
            

            

            return isValidated;
        }

        public bool ValidateExistsSavedVoucherSerial(int groupID, decimal voucherValue, int startingNo, int noOfVouchers)
        {
            bool isValidated = false;

            InvGiftVoucherMaster invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == voucherValue).FirstOrDefault();
            InvGiftVoucherPurchaseDetail invGiftVoucherPurchaseDetail;

            if (invGiftVoucherMaster != null)
            {
                int endSerialNo = startingNo + noOfVouchers;
                for (int s = startingNo; s <= endSerialNo; s++)
                {
                    invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherGroupID == groupID && u.GiftVoucherValue == voucherValue && u.VoucherSerialNo == s).FirstOrDefault();

                    if (invGiftVoucherMaster != null)
                    {
                        invGiftVoucherPurchaseDetail = context.InvGiftVoucherPurchaseDetails.Where(u => u.InvGiftVoucherMasterID == invGiftVoucherMaster.InvGiftVoucherMasterID && u.DocumentStatus == 1).FirstOrDefault();
                        if (invGiftVoucherPurchaseDetail != null)
                        {
                            isValidated = false;
                            return isValidated;
                        }
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
            }
            else
            {
                isValidated = true;
            }

            return isValidated;
        }

        public bool ValidateVoucherSerialPageType(List<InvGiftVoucherPurchaseDetailTemp> invGiftVoucherPurchaseDetailsTemp, string documentNo, int voucherType, int bookID)
        {
            bool isValidated = false;
            InvGiftVoucherBookCode invGiftVoucherBookCodeTemp = context.InvGiftVoucherBookCodes.Where(b => b.InvGiftVoucherBookCodeID == bookID).FirstOrDefault();

            if (invGiftVoucherBookCodeTemp != null)
            {
                if (invGiftVoucherPurchaseDetailsTemp.Count > 0)
                {
                    InvGiftVoucherPurchaseDetailTemp invGiftVoucherPurchaseDetail = invGiftVoucherPurchaseDetailsTemp.Where(u => u.DocumentNo == documentNo).FirstOrDefault();

                    if (invGiftVoucherPurchaseDetail == null)
                    {
                        isValidated = true;
                        return isValidated;
                    }

                    InvGiftVoucherBookCode invGiftVoucherBookCode = (from gm in context.InvGiftVoucherMasters
                                                                     join gb in context.InvGiftVoucherBookCodes on gm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                                                                     where gm.VoucherType == voucherType && gm.InvGiftVoucherMasterID == invGiftVoucherPurchaseDetail.InvGiftVoucherMasterID
                                                                     select gb).FirstOrDefault();
                    if (invGiftVoucherBookCode != null)
                    {
                        if (invGiftVoucherBookCode.PageCount == 0 && invGiftVoucherBookCodeTemp.PageCount == 0)
                        {
                            isValidated = true;
                            return isValidated;
                        }
                        else if (invGiftVoucherBookCode.PageCount != 0 && invGiftVoucherBookCodeTemp.PageCount != 0)
                        {
                            isValidated = true;
                            return isValidated;
                        }
                        else
                        {
                            isValidated = false;
                            return isValidated;
                        }
                    }
                }
            }

            return isValidated;
        }
        #endregion
    }
}
