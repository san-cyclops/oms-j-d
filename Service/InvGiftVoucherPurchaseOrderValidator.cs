using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;

namespace Service
{
    public class InvGiftVoucherPurchaseOrderValidator
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Validate VoucherQty
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherQty"></param>
        /// <returns></returns>
        public bool ValidateVoucherQty(int bookID, int voucherQty, int voucherPageStatus)
        {
            bool isValidated = false;
            int availableBookCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            
            if (invGiftVoucherBookCode != null)
            {
                if (voucherPageStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    availableBookCount =
                        context.InvGiftVoucherMasters.Where(
                            s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 0 && s.IsDelete == false)
                               .Count();

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
                        availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == 0 && s.IsDelete == false).Count();

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
            int voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
            int voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

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
        public bool ValidateVoucherNoRange(int bookID, string voucherNoFrom, string voucherNoTo)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, validBookCount = 0, bookCount = 0, voucherNoSerialFrom = 0, voucherNoSerialTo = 0;

            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            
            if (invGiftVoucherBookCode != null)
            {
                voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
                voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNoSerial >= voucherNoSerialFrom && s.VoucherNoSerial <= voucherNoSerialTo) && s.VoucherStatus == 0 && s.IsDelete == false).Count();

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
            int voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
            int voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();

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
        public bool ValidateVoucherSerialRange(int bookID, int voucherSerialFrom, int voucherSerialTo)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            InvGiftVoucherMaster invGiftVoucherMaster;
            if (invGiftVoucherBookCode != null)
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerialNo.CompareTo(voucherSerialFrom) >= 0 && s.VoucherSerialNo.CompareTo(voucherSerialTo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).Count();
                invGiftVoucherMaster =
                    context.InvGiftVoucherMasters.Where(
                        s =>
                        s.InvGiftVoucherBookCodeID == bookID &&
                        (s.VoucherSerialNo >= voucherSerialFrom && s.VoucherSerialNo <= voucherSerialTo) && s.VoucherStatus == 0 &&
                        s.IsDelete == false).FirstOrDefault();
                if (invGiftVoucherMaster != null)
                {
                    if (invGiftVoucherMaster.PageCount == 0)
                    {

                        if (((voucherSerialTo - voucherSerialFrom) + 1) == availableVoucherCount)
                        {
                            isValidated = true;
                        }
                    }
                    else
                    {
                        bookCount = availableVoucherCount%invGiftVoucherBookCode.PageCount;
                        if (bookCount == 0) // && invGiftVoucherBookCode.CurrentSerialNo >= voucherNoTo)
                        {
                            if (((voucherSerialTo - voucherSerialFrom) + 1) == availableVoucherCount)
                            {
                                isValidated = true;
                            }
                        }
                    }
                }
            }

            return isValidated;
        }

        public bool ValidateVoucherSerialRange(int bookID, string voucherSerialFrom, string voucherSerialTo)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            InvGiftVoucherMaster invGiftVoucherMaster;
            if (invGiftVoucherBookCode != null)
            {
                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && s.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).Count();
                invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && s.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).FirstOrDefault();

                int availableVoucherSelectedRangeCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && s.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && s.VoucherStatus == 0 && s.IsDelete == false).Count();

                if (invGiftVoucherMaster != null)
                {
                    if (invGiftVoucherMaster.PageCount == 0)
                    {
                        if ((availableVoucherSelectedRangeCount) == availableVoucherCount)
                        {
                            isValidated = true;
                        }
                    }
                    else
                    {
                        bookCount = availableVoucherCount % invGiftVoucherBookCode.PageCount;
                        if (bookCount == 0) // && invGiftVoucherBookCode.CurrentSerialNo >= voucherNoTo)
                        {
                            if ((availableVoucherSelectedRangeCount) == availableVoucherCount)
                            {
                                isValidated = true;
                            }
                        }
                    }
                }
            }

            return isValidated;
        }

        public bool ValidateExistsPausedVoucherSerial(List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseDetailsTemp, string documentNo, int voucherType)
        {
            bool isValidated = false;
            InvGiftVoucherMaster invGiftVoucherMaster;
            List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailList = new List<InvGiftVoucherPurchaseOrderDetail>();
            
            int invGiftVoucherPurchaseDetailsGroupedList = 0;
            InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetail = (from m in invGiftVoucherPurchaseDetailsTemp
                                                            join pd in context.InvGiftVoucherPurchaseOrderDetails on m.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                                            join ph in context.InvGiftVoucherPurchaseOrderHeaders on pd.InvGiftVoucherPurchaseOrderHeaderID equals ph.InvGiftVoucherPurchaseOrderHeaderID
                                                            where m.VoucherType == voucherType && ph.DocumentNo != documentNo
                                                            select pd).FirstOrDefault();

            if (invGiftVoucherPurchaseOrderDetail != null)
            {
                isValidated = false;
                return isValidated;
            }
            else
            {
                isValidated = true;
            }

            return isValidated;
        }

        public bool ValidateVoucherSerialPageType(List<InvGiftVoucherPurchaseOrderDetail> invGiftVoucherPurchaseOrderDetailsTemp, string documentNo, int voucherType, int bookID)
        {
            bool isValidated = false;
            InvGiftVoucherBookCode invGiftVoucherBookCodeTemp = context.InvGiftVoucherBookCodes.Where(b => b.InvGiftVoucherBookCodeID == bookID).FirstOrDefault();

            if (invGiftVoucherBookCodeTemp != null)
            {
                if (invGiftVoucherPurchaseOrderDetailsTemp.Count > 0)
                {
                    InvGiftVoucherPurchaseOrderDetail invGiftVoucherPurchaseOrderDetail = invGiftVoucherPurchaseOrderDetailsTemp.Where(u => u.DocumentNo == documentNo).FirstOrDefault();

                    if (invGiftVoucherPurchaseOrderDetail == null)
                    {
                        isValidated = true;
                        return isValidated;
                    }
                    InvGiftVoucherBookCode invGiftVoucherBookCode = (from gm in context.InvGiftVoucherMasters 
                                                                     join gb in context.InvGiftVoucherBookCodes on gm.InvGiftVoucherBookCodeID equals gb.InvGiftVoucherBookCodeID
                                                                     where gm.VoucherType == voucherType && gm.InvGiftVoucherMasterID == invGiftVoucherPurchaseOrderDetail.InvGiftVoucherMasterID
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
