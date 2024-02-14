using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class InvGiftVoucherTransferNoteValidator
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        /// <summary>
        /// Validate VoucherQty
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherQty"></param>
        /// <returns></returns>
        public bool ValidateVoucherQty(int bookID, int voucherQty, int voucherPageStatus, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableBookCount = 0, bookCount = 0, voucherStatus = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }
                if (voucherPageStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == voucherStatus && s.IsDelete == false).Count();

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
                        availableBookCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.VoucherStatus == voucherStatus && s.IsDelete == false).Count();

                        if (availableBookCount >= voucherQty)
                        {
                            isValidated = true;
                        }
                    }
                }
            }

            return isValidated;
        }

        public bool ValidatePurchaseVoucherQty(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, int bookID, int voucherQty, int voucherPageStatus, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableBookCount = 0, bookCount = 0, voucherStatus = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }

                if (voucherPageStatus == 1 && invGiftVoucherBookCode.PageCount == 0)
                {
                    availableBookCount = (from gv in context.InvGiftVoucherMasters
                                          join pd in context.InvGiftVoucherPurchaseDetails on
                                              gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                          where
                                              gv.InvGiftVoucherBookCodeID == bookID && gv.VoucherStatus == voucherStatus &&
                                              gv.IsDelete == false
                                              && pd.IsTransfer == false &&
                                              pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                          select gv.VoucherSerial).Count();

                    if (availableBookCount >= voucherQty)
                    {
                        isValidated = true;
                    }
                }
                else
                {
                    bookCount = voucherQty % invGiftVoucherBookCode.PageCount;
                    if (bookCount == 0 &&
                        invGiftVoucherBookCode.CurrentSerialNo >= (voucherQty / invGiftVoucherBookCode.PageCount))
                    {
                        availableBookCount = (from gv in context.InvGiftVoucherMasters
                                              join pd in context.InvGiftVoucherPurchaseDetails on
                                                  gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                              where
                                                  gv.InvGiftVoucherBookCodeID == bookID && gv.VoucherStatus == voucherStatus &&
                                                  gv.IsDelete == false
                                                  && pd.IsTransfer == false &&
                                              pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID
                                              select gv.VoucherSerial).Count();

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
        public bool ValidateVoucherNoFromTo(string voucherNoFrom, string voucherNoTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int voucherNoSerialFrom = 0, voucherNoSerialTo = 0, voucherStatus = 0;
            
            if (isHeadOffice)
            {
                voucherStatus = 2;
            }
            else
            {
                voucherStatus = 3;
            }
            voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
            voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
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
        public bool ValidateVoucherNoRange(int bookID, string voucherNoFrom, string voucherNoTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, validBookCount = 0, bookCount = 0, voucherNoSerialFrom = 0, voucherNoSerialTo = 0, voucherStatus = 0;

            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }

                voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
                voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherNoSerial >= voucherNoSerialFrom && s.VoucherNoSerial <= voucherNoSerialTo) && s.VoucherStatus == voucherStatus && s.IsDelete == false).Count();

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

        public bool ValidatePurchaseVoucherNoRange(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, int bookID, string voucherNoFrom, string voucherNoTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, validBookCount = 0, bookCount = 0, voucherNoSerialFrom = 0, voucherNoSerialTo = 0, voucherStatus = 0;

            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();

            if (invGiftVoucherBookCode != null)
            {
                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }

                voucherNoSerialFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();
                voucherNoSerialTo = context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNoTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherNoSerial).FirstOrDefault();

                availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                         where pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID &&
                                         gv.InvGiftVoucherBookCodeID == bookID && (gv.VoucherNoSerial >= voucherNoSerialFrom && gv.VoucherNoSerial <= voucherNoSerialTo) &&
                                         gv.VoucherStatus == voucherStatus && gv.IsDelete == false
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
        public bool ValidateVoucherSerialFromTo(string voucherSerialFrom, string voucherSerialTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int voucherSerialNoFrom = 0;
            int voucherSerialNoTo = 0, voucherStatus = 0;
            if (isHeadOffice)
            {
                voucherStatus = 2;
            }
            else
            {
                voucherStatus = 3;
            }
            voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
            voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
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
        public bool ValidateVoucherSerialRange(int bookID, string voucherSerialFrom, string voucherSerialTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            InvGiftVoucherMaster invGiftVoucherMaster;
            if (invGiftVoucherBookCode != null)
            {
                int voucherSerialNoFrom = 0, voucherSerialNoTo = 0, voucherStatus = 0;

                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }
                voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
                voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();

                availableVoucherCount = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerialNo >= voucherSerialNoFrom && s.VoucherSerialNo <= voucherSerialNoTo) && voucherSerialFrom.Substring(0, 2) == s.VoucherPrefix && s.VoucherStatus == voucherStatus && s.IsDelete == false).Count();

                invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && (s.VoucherSerialNo >= voucherSerialNoFrom && s.VoucherSerialNo <= voucherSerialNoTo) && s.VoucherStatus == voucherStatus && s.IsDelete == false).FirstOrDefault();

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
                        bookCount = availableVoucherCount%invGiftVoucherBookCode.PageCount;
                        if (bookCount == 0) // && invGiftVoucherBookCode.CurrentSerialNo >= voucherNoTo)
                        {
                            if (((voucherSerialNoTo - voucherSerialNoFrom) + 1) == availableVoucherCount)
                            {
                                isValidated = true;
                            }
                        }
                    }
                }
            }

            return isValidated;
        }

        public bool ValidatePurchaseVoucherSerialRange(InvGiftVoucherPurchaseHeader invGiftVoucherPurchaseHeader, int bookID, string voucherSerialFrom, string voucherSerialTo, bool isHeadOffice)
        {
            bool isValidated = false;
            int availableVoucherCount = 0, bookCount = 0;
            InvGiftVoucherBookCode invGiftVoucherBookCode = context.InvGiftVoucherBookCodes.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
            InvGiftVoucherMaster invGiftVoucherMaster;
            if (invGiftVoucherBookCode != null)
            {
                int voucherSerialNoFrom = 0, voucherSerialNoTo = 0, voucherStatus = 0;

                if (isHeadOffice)
                {
                    voucherStatus = 2;
                }
                else
                {
                    voucherStatus = 3;
                }
                voucherSerialNoFrom = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialFrom) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();
                voucherSerialNoTo = context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerialTo) == 0 && s.VoucherStatus == voucherStatus && s.IsDelete == false).Select(s => s.VoucherSerialNo).FirstOrDefault();

                availableVoucherCount = (from gv in context.InvGiftVoucherMasters
                                         join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                         where pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID &&
                                         gv.InvGiftVoucherBookCodeID == bookID && (gv.VoucherSerialNo >= voucherSerialNoFrom && gv.VoucherSerialNo <= voucherSerialNoTo) &&
                                         gv.VoucherStatus == voucherStatus && gv.IsDelete == false
                                         select gv.VoucherNo).Count();

                invGiftVoucherMaster = (from gv in context.InvGiftVoucherMasters
                                        join pd in context.InvGiftVoucherPurchaseDetails on gv.InvGiftVoucherMasterID equals pd.InvGiftVoucherMasterID
                                        where pd.InvGiftVoucherPurchaseHeaderID == invGiftVoucherPurchaseHeader.InvGiftVoucherPurchaseHeaderID &&
                                        gv.InvGiftVoucherBookCodeID == bookID && (gv.VoucherSerialNo >= voucherSerialNoFrom && gv.VoucherSerialNo <= voucherSerialNoTo) &&
                                        gv.VoucherStatus == voucherStatus && gv.IsDelete == false
                                        select gv).FirstOrDefault();

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
            }

            return isValidated;
        }
        #endregion
    }
}
