using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using Utility;
using MoreLinq;

namespace Service
{
    public class InvGiftVoucherMasterService
    {
        ERPDbContext context = new ERPDbContext();

        public enum VouhcherTypes
        {
            Voucher = 1,
            Coupon = 2
        }

        public enum VoucherStatus
        {
            //  [Description("0 - Default, 1 - Request, 2 - Reeceived, 3 - Transfered, 4 - Issued, 5 - Lost / Damage, 6 - Redeemed ")]
            Default = 0,
            Request = 1,
            Received = 2,
            Transfered = 3,
            Issued = 4,
            LostOrDamage = 5,
            Redeemed = 6
        }

        #region Methods

        /// <summary>
        /// Save new InvGiftVoucherMaster
        /// </summary>
        /// <param name="invGiftVoucherMasterDetailsTemp"></param>
        public bool AddInvGiftVoucherMaster(List<InvGiftVoucherMasterTemp> invGiftVoucherMasterDetailsTemp)
        {
            //foreach (InvGiftVoucherMasterTemp invGiftVoucherMasterTemp in invGiftVoucherMasterDetailsTemp)
            //{
            //    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
            //    invGiftVoucherMaster.InvGiftVoucherBookCodeID = invGiftVoucherMasterTemp.InvGiftVoucherBookCodeID;
            //    invGiftVoucherMaster.InvGiftVoucherGroupID = invGiftVoucherMasterTemp.InvGiftVoucherGroupID;
            //    invGiftVoucherMaster.VoucherNo = invGiftVoucherMasterTemp.VoucherNo;
            //    invGiftVoucherMaster.VoucherNoSerial = invGiftVoucherMasterTemp.VoucherNoSerial;
            //    invGiftVoucherMaster.VoucherSerial = invGiftVoucherMasterTemp.VoucherSerial;
            //    invGiftVoucherMaster.VoucherSerialNo = invGiftVoucherMasterTemp.VoucherSerialNo;
            //    invGiftVoucherMaster.GiftVoucherValue = invGiftVoucherMasterTemp.GiftVoucherValue;
            //    invGiftVoucherMaster.VoucherCount = invGiftVoucherMasterTemp.VoucherCount;
            //    invGiftVoucherMaster.VoucherPrefix = invGiftVoucherMasterTemp.VoucherPrefix;
            //    invGiftVoucherMaster.SerialLength = invGiftVoucherMasterTemp.SerialLength;
            //    invGiftVoucherMaster.StartingNo = invGiftVoucherMasterTemp.StartingNo;
            //    invGiftVoucherMaster.PageCount = invGiftVoucherMasterTemp.PageCount;
            //    invGiftVoucherMaster.VoucherStatus = invGiftVoucherMasterTemp.VoucherStatus;
            //    invGiftVoucherMaster.IsDelete = invGiftVoucherMasterTemp.IsDelete;

            //    context.InvGiftVoucherMasters.Add(invGiftVoucherMaster);
            //    context.SaveChanges();
            //}

            var invGiftVoucherMasterSaveQuery = (from pd in invGiftVoucherMasterDetailsTemp
                                              select new
                                              {
                                                  InvGiftVoucherMasterID = 0,
                                                  InvGiftVoucherBookCodeID = pd.InvGiftVoucherBookCodeID,
                                                  InvGiftVoucherGroupID = pd.InvGiftVoucherGroupID,
                                                  CompanyID = pd.CompanyID,
                                                  LocationID = pd.LocationID,
                                                  VoucherNo = pd.VoucherNo.ToString().Trim(),
                                                  VoucherNoSerial = pd.VoucherNoSerial,
                                                  VoucherPrefix = pd.VoucherPrefix.ToString().Trim(),
                                                  SerialLength = pd.SerialLength,
                                                  GiftVoucherValue = pd.GiftVoucherValue,
                                                  GiftVoucherPercentage = pd.GiftVoucherPercentage,
                                                  StartingNo = pd.StartingNo,
                                                  VoucherCount = pd.VoucherCount,
                                                  PageCount = pd.PageCount,
                                                  VoucherSerial = pd.VoucherSerial.ToString().Trim(),
                                                  VoucherSerialNo = pd.VoucherSerialNo,
                                                  VoucherType = pd.VoucherType,
                                                  VoucherStatus = pd.VoucherStatus,
                                                  ToLocationID = pd.LocationID,
                                                  SoldLocationID = 0,
                                                  SoldCashierID = 0,
                                                  SoldReceiptNo = "",
                                                  SoldUnitID = 0,
                                                  SoldZNo = 0,
                                                  SoldDate = Common.GetSystemDate(),
                                                  RedeemedLocationID = 0,
                                                  RedeemedCashierID = 0,
                                                  RedeemedReceiptNo = "",
                                                  RedeemedUnitID = 0,
                                                  RedeemedZNo = 0,
                                                  RedeemedDate = Common.GetSystemDate(), 
                                                  IsDelete = pd.IsDelete,
                                                  GroupOfCompanyID = Common.GroupOfCompanyID,
                                                  CreatedUser = Common.LoggedUser,
                                                  CreatedDate = Common.GetSystemDate(),
                                                  ModifiedUser = Common.LoggedUser,
                                                  ModifiedDate = Common.GetSystemDate(),
                                                  DataTransfer = 0
                                              }).ToList();

            //GroupOfCompanyID = invPurchaseHeader.GroupOfCompanyID,
            //CreatedUser = invPurchaseHeader.CreatedUser,
            //CreatedDate = invPurchaseHeader.CreatedDate,
            //ModifiedUser = invPurchaseHeader.ModifiedUser,
            //ModifiedDate = invPurchaseHeader.ModifiedDate,

            DataTable DTITEM = new DataTable();
            DTITEM = invGiftVoucherMasterSaveQuery.ToDataTable();

            CommonService.BulkInsert(DTITEM,"InvGiftVoucherMaster");

            var bookId = invGiftVoucherMasterDetailsTemp.LastOrDefault().InvGiftVoucherBookCodeID;
            var lastRecord = invGiftVoucherMasterDetailsTemp.LastOrDefault().CurrentSerial;

            InvGiftVoucherBookCode invGiftVoucherBookCode = (from s in context.InvGiftVoucherBookCodes
                                                             where s.InvGiftVoucherBookCodeID == bookId
                                                             select s).FirstOrDefault<InvGiftVoucherBookCode>();

            invGiftVoucherBookCode.CurrentSerialNo = lastRecord+1;
            context.SaveChanges();

            return true;
        }
        
        /// <summary>
        /// Add or Update Gift Voucher of grid view
        /// </summary>
        /// <param name="invGiftVoucherMasterDetailsTemp"></param>
        /// <param name="invGiftVoucherMasterTemp"></param>
        /// <returns></returns>
        public List<InvGiftVoucherMasterTemp> GetInvGiftVoucherMasterDetailTempList(List<InvGiftVoucherMasterTemp> invGiftVoucherMasterDetailsTemp, InvGiftVoucherMasterTemp invGiftVoucherMasterTemp)
        {
            invGiftVoucherMasterDetailsTemp.Add(invGiftVoucherMasterTemp);
            return invGiftVoucherMasterDetailsTemp;
        }

        /// <summary>
        /// Search Gift Voucher Master by code
        /// </summary>
        /// <param name="bookCode"></param>
        /// <returns></returns>
        //public InvGiftVoucherMaster GetInvGiftVoucherMasterByCode(string bookCode)
        //{
        //    InvGiftVoucherMaster invGiftVoucherMaster = new InvGiftVoucherMaster();
        //    return invGiftVoucherMaster; // context.InvGiftVoucherMasters.Where(s => s.BookCode == bookCode && s.IsDelete == false).FirstOrDefault();
        //}

        /// <summary>
        /// Search Gift Voucher Master by name
        /// </summary>
        /// <param name="bookDescription"></param>
        /// <returns></returns>
        //public InvGiftVoucherMaster GetInvGiftVoucherMasterByName(string bookDescription)
        //{
        //    return context.InvGiftVoucherMasters.Where(s => s.Description == bookDescription && s.IsDelete == false).FirstOrDefault();
        //}

        /// <summary>
        /// Search Gift Voucher Master by ID
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public InvGiftVoucherMaster GetInvGiftVoucherMasterByBookID(int bookID)
        {
            return context.InvGiftVoucherMasters.Where(s => s.InvGiftVoucherBookCodeID == bookID && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToPOByVoucherSerial(string voucherSerial)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerial) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToPOByVoucherNo(string voucherNo)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNo) == 0 && s.VoucherStatus == 0 && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToGRNByVoucherSerial(string voucherSerial)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerial) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToGRNByVoucherNo(string voucherNo)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNo) == 0 && s.VoucherStatus == 1 && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToTOGByVoucherSerial(string voucherSerial)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherSerial.CompareTo(voucherSerial) == 0 && (s.VoucherStatus == 2 || s.VoucherStatus == 3) && s.IsDelete == false).FirstOrDefault();
        }

        public InvGiftVoucherMaster GetInvGiftVoucherMasterToTOGByVoucherNo(string voucherNo)
        {
            return context.InvGiftVoucherMasters.Where(s => s.VoucherNo.CompareTo(voucherNo) == 0 && (s.VoucherStatus == 2 || s.VoucherStatus == 3) && s.IsDelete == false).FirstOrDefault();
        }

        public long GetStartingNoByVoucherType(int voucherType, int bookID, string voucherPrefix)
        {
            long currentSerialNo = 1;
            InvGiftVoucherMaster invGiftVoucherMaster = context.InvGiftVoucherMasters.Where(a => a.VoucherType == voucherType && a.InvGiftVoucherBookCodeID == bookID && a.VoucherPrefix == voucherPrefix).FirstOrDefault();
            
            if(invGiftVoucherMaster != null)
            {
                currentSerialNo = context.InvGiftVoucherMasters.Where(a => a.VoucherType == voucherType && a.InvGiftVoucherBookCodeID == bookID && a.VoucherPrefix == voucherPrefix).Max(a => a.VoucherSerialNo + 1);
            }

            return currentSerialNo;
        }

        /// <summary>
        /// Get GiftVoucherMasterBook Codes
        /// </summary>
        /// <returns></returns>
        //public string[] GetGiftVoucherMasterBookCodes()
        //{
        //    List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(s => s.IsDelete.Equals(false)).Select(s => s.BookCode).ToList();
        //    string[] giftVoucherMasterBookCodes = giftVoucherMasterBooksList.ToArray();
        //    return giftVoucherMasterBookCodes;
        //}

        /// <summary>
        /// Get GiftVoucherMasterBook Descriptions
        /// </summary>
        /// <returns></returns>
        //public string[] GetGiftVoucherMasterBookDescriptions()
        //{
        //    List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(s => s.IsDelete.Equals(false)).Select(s => s.Description).ToList();
        //    string[] giftVoucherMasterBookNames = giftVoucherMasterBooksList.ToArray();
        //    return giftVoucherMasterBookNames;
        //}
        
        ///?????

        /// <summary>
        /// Get all InvGiftVoucherMaster details 
        /// </summary>
        /// <param name="bookCode"></param>
        /// <returns>InvGiftVoucherMaster</returns>
        //public List<InvGiftVoucherMaster> GetInvGiftVoucherMasterByBookCode(string bookCode)
        //{
        //    return context.InvGiftVoucherMasters.Where(u => u.BookCode == bookCode && u.IsDelete == false).ToList();
        //}

        /// <summary>
        /// Get all InvGiftVoucherMaster details 
        /// </summary>
        /// <param name="bookID"></param>
        public List<InvGiftVoucherMaster> GetInvGiftVoucherMastersByBookID(int bookID)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && u.IsDelete == false).ToList();
        }
        
        /// <summary>
        /// Get all InvGiftVoucherMaster details for Purchase Order
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>bookID</returns>
        public List<InvGiftVoucherMaster> GetAllGiftVouchersByBookIDWithQty(int bookID, int voucherQty)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && u.VoucherStatus == 0 && u.IsDelete == false).Take(voucherQty).OrderBy(u=>u.VoucherSerial).ToList();
        }
        
        /// <summary>
        /// Get all InvGiftVoucherMaster details for Purchase Order
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        public List<InvGiftVoucherMaster> GetAllGiftVouchersByBookIDWithVoucherNoRange(int bookID, int voucherNoFrom, int voucherNoTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNoSerial >= voucherNoFrom && u.VoucherNoSerial <= voucherNoTo) && u.VoucherStatus == 0 && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetAllGiftVouchersByBookIDWithVoucherNoRange(int bookID, string voucherNoFrom, string voucherNoTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNo.CompareTo(voucherNoFrom) >= 0 && u.VoucherNo.CompareTo(voucherNoTo) <= 0) && u.VoucherStatus == 0 && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList();
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for Purchase Order
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        public List<InvGiftVoucherMaster> GetAllGiftVouchersByBookIDWithVoucherSerialRange(long bookID, int voucherSerialFrom, int voucherSerialTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && u.VoucherStatus == 0 && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetAllGiftVouchersByBookIDWithVoucherSerialRange(int bookID, string voucherSerialFrom, string voucherSerialTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && u.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && u.VoucherStatus == 0 && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList();
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for purchase
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>bookID</returns>
        public List<InvGiftVoucherMaster> GetGeneratedGiftVouchersByBookIDWithQtyForPurchase(int bookID, int voucherQty)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).Take(voucherQty).OrderBy(u => u.VoucherSerial).ToList();
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for purchase
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        public List<InvGiftVoucherMaster> GetGeneratedGiftVouchersByBookIDWithVoucherNoRangeForPurchase(int bookID, int voucherNoFrom, int voucherNoTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNoSerial >= voucherNoFrom && u.VoucherNoSerial <= voucherNoTo) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetGeneratedGiftVouchersByBookIDWithVoucherNoRangeForPurchase(int bookID, string voucherNoFrom, string voucherNoTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNo.CompareTo(voucherNoFrom) >= 0 && u.VoucherNo.CompareTo(voucherNoTo) <= 0) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList();
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for purchase
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        public List<InvGiftVoucherMaster> GetGeneratedGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(int bookID, int voucherSerialFrom, int voucherSerialTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetGeneratedGiftVouchersByBookIDWithVoucherSerialRangeForPurchase(int bookID, string voucherSerialFrom, string voucherSerialTo)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && u.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && (u.VoucherStatus == 0 || u.VoucherStatus == 1) && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList();
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for Transfer
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>bookID</returns>
        public List<InvGiftVoucherMaster> GetPurchasedGiftVouchersByBookIDWithQty(int bookID, int voucherQty, int locationID, bool isHeadOffice)
        {
            if (isHeadOffice)
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherStatus == 2) && u.ToLocationID == locationID && u.IsDelete == false).Take(voucherQty).OrderBy(u => u.VoucherSerial).ToList(); }
            else
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherStatus == 3) && u.ToLocationID == locationID && u.IsDelete == false).Take(voucherQty).OrderBy(u => u.VoucherSerial).ToList(); }
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for Transfer
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        public List<InvGiftVoucherMaster> GetPurchasedGiftVouchersByBookIDWithVoucherNoRange(int bookID, int voucherNoFrom, int voucherNoTo, int locationID)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNoSerial >= voucherNoFrom && u.VoucherNoSerial <= voucherNoTo) && (u.VoucherStatus == 2 || u.VoucherStatus == 3) && u.ToLocationID == locationID && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetPurchasedGiftVouchersByBookIDWithVoucherNoRange(int bookID, string voucherNoFrom, string voucherNoTo, int locationID, bool isHeadOffice)
        {
            if (isHeadOffice)
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNo.CompareTo(voucherNoFrom) >= 0 && u.VoucherNo.CompareTo(voucherNoTo) <= 0) && (u.VoucherStatus == 2) && u.ToLocationID == locationID && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList(); }
            else
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNo.CompareTo(voucherNoFrom) >= 0 && u.VoucherNo.CompareTo(voucherNoTo) <= 0) && (u.VoucherStatus == 3) && u.ToLocationID == locationID && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList(); }
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for Transfer
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        public List<InvGiftVoucherMaster> GetPurchasedGiftVouchersByBookIDWithVoucherSerialRange(int bookID, int voucherSerialFrom, int voucherSerialTo, int locationID)
        {
            return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && (u.VoucherStatus == 2 || u.VoucherStatus == 3) && u.ToLocationID == locationID && u.IsDelete == false).ToList();
        }

        public List<InvGiftVoucherMaster> GetPurchasedGiftVouchersByBookIDWithVoucherSerialRange(int bookID, string voucherSerialFrom, string voucherSerialTo, int locationID, bool isHeadOffice)
        {
            if (isHeadOffice)
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && u.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && (u.VoucherStatus == 2) && u.ToLocationID == locationID && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList(); }
            else
            { return context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerial.CompareTo(voucherSerialFrom) >= 0 && u.VoucherSerial.CompareTo(voucherSerialTo) <= 0) && (u.VoucherStatus == 3) && u.ToLocationID == locationID && u.IsDelete == false).OrderBy(u => u.VoucherSerial).ToList(); }
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details 
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherQty"></param>
        public string[] GetAllVoucherNosByBookIDForQty(int bookID, int voucherQty)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && u.VoucherStatus == 0 && u.IsDelete == false).Take(voucherQty).Select(s => s.VoucherNo).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }
        
        /// <summary>
        /// Get all InvGiftVoucherMaster details for Qty
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherQty"></param>
        public string[] GetAllVoucherSerialsByBookIDForQty(int bookID, int voucherQty)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && u.VoucherStatus == 0 && u.IsDelete == false).Take(voucherQty).Select(s => s.VoucherSerial).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher no range 
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        public string[] GetAllVoucherNosByBookIDForVoucherNoRange(int bookID, int voucherNoFrom, int voucherNoTo)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNoSerial >= voucherNoFrom && u.VoucherNoSerial <= voucherNoTo) && u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherNo).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher serial range
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        public string[] GetAllVoucherNosByBookIDForVoucherSerialRange(int bookID, int voucherSerialFrom, int voucherSerialTo)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherNo).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher serial range
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherNoFrom"></param>
        /// <param name="voucherNoTo"></param>
        public string[] GetAllVoucherSerialsByBookIDForVoucherNoRange(int bookID, int voucherNoFrom, int voucherNoTo)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherNoSerial >= voucherNoFrom && u.VoucherNoSerial <= voucherNoTo) && u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherSerial).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher serial range
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="voucherSerialFrom"></param>
        /// <param name="voucherSerialTo"></param>
        public string[] GetAllVoucherSerialsByBookIDForVoucherSerialRange(int bookID, int voucherSerialFrom, int voucherSerialTo)
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.InvGiftVoucherBookCodeID == bookID && (u.VoucherSerialNo >= voucherSerialFrom && u.VoucherSerialNo <= voucherSerialTo) && u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherSerial).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher serial range
        /// </summary>
        public string[] GetAllGiftVoucherNos()
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherNo).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        /// <summary>
        /// Get all InvGiftVoucherMaster details for voucher serial range
        /// </summary>
        public string[] GetAllGiftVoucherSerials()
        {
            List<string> giftVoucherMasterBooksList = context.InvGiftVoucherMasters.Where(u => u.VoucherStatus == 0 && u.IsDelete == false).Select(s => s.VoucherSerial).ToList();
            string[] giftVoucherMastervouchers = giftVoucherMasterBooksList.ToArray();
            return giftVoucherMastervouchers;
        }

        

        /// <summary>
        /// Get active Gift Voucher Master DataTable
        /// </summary>
        /// <returns></returns>
        //public DataTable GetAllActiveGiftVoucherMastersDataTable()
        //{
        //    DataTable tblGiftVoucherMasters = new DataTable();
        //    var query = from v in context.InvGiftVoucherMasters where v.IsDelete == false select new { v.BookCode, v.GiftVoucherValue };

        //    return tblGiftVoucherMasters = Common.LINQToDataTable(query);
        //}

        public DataTable GetGiftVoucherRegisterDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList, AutoGenerateInfo autoGenerateInfo)
        {
            try
            {
                //StringBuilder selectFields = new StringBuilder();
                var query = context.InvGiftVoucherMasters.Where("IsDelete = @0", false);

                // Insert Conditions to query
                foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
                {
                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (string)))
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.IsManualRecordFilter)
                        {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @0 ", reportConditionsDataStruct.ConditionFrom.Trim());}
                        else
                        {query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim());}
                    }

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
                                case "EmployeeID":
                                    if (reportConditionsDataStruct.ReportDataStruct.ReportFieldName == "Issued Cashier")
                                    {
                                        query = (from qr in query
                                                 join jt in context.Employees.Where(
                                                     "" +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " >= " + "@0 AND " +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " <= @1",
                                                     (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                     (reportConditionsDataStruct.ConditionTo.Trim())
                                                     ) on qr.SoldCashierID equals jt.EmployeeID
                                                 select qr
                                           );
                                    }
                                    else if (reportConditionsDataStruct.ReportDataStruct.ReportFieldName == "Redeemed Cashier")
                                    {
                                        query = (from qr in query
                                                 join jt in context.Employees.Where(
                                                     "" +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " >= " + "@0 AND " +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " <= @1",
                                                     (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                     (reportConditionsDataStruct.ConditionTo.Trim())
                                                     ) on qr.RedeemedCashierID equals jt.EmployeeID
                                                 select qr
                                          );
                                    }
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
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                        }
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                    {
                        if (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() == "VoucherType")
                        {
                           query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals(VouhcherTypes.Coupon.ToString())?(int)VouhcherTypes.Coupon:(int)VouhcherTypes.Voucher, reportConditionsDataStruct.ConditionTo.Trim().Equals(VouhcherTypes.Coupon.ToString())?(int)VouhcherTypes.Coupon:(int)VouhcherTypes.Voucher);
                        }
                        else if (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() == "VoucherStatus")
                        {
                            int selectedFromStatus = 0, selectedToStatus = 0;

                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.Request.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.Request;}
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.Received.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.Received;}
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.Transfered.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.Transfered;}
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.Issued.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.Issued;}
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.LostOrDamage.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.LostOrDamage;}
                            if (reportConditionsDataStruct.ConditionFrom.Trim().Equals(VoucherStatus.Redeemed.ToString()))
                            { selectedFromStatus = (int)VoucherStatus.Redeemed;}

                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.Request.ToString()))
                            { selectedToStatus = (int)VoucherStatus.Request; }
                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.Received.ToString()))
                            { selectedToStatus = (int)VoucherStatus.Received; }
                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.Transfered.ToString()))
                            { selectedToStatus = (int)VoucherStatus.Transfered; }
                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.Issued.ToString()))
                            { selectedToStatus = (int)VoucherStatus.Issued; }
                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.LostOrDamage.ToString()))
                            { selectedToStatus = (int)VoucherStatus.LostOrDamage; }
                            if (reportConditionsDataStruct.ConditionTo.Trim().Equals(VoucherStatus.Redeemed.ToString()))
                            { selectedToStatus = (int)VoucherStatus.Redeemed; }

                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", (selectedFromStatus), (selectedToStatus));

                        }
                        else if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                        {
                            switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                            {
                                case "InvGiftVoucherGroupID":
                                    query = (from qr in query
                                             join jt in context.InvGiftVoucherGroups.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.InvGiftVoucherGroupID equals jt.InvGiftVoucherGroupID
                                             select qr
                                            );
                                    break;
                                case "InvGiftVoucherBookCodeID":
                                    query = (from qr in query
                                             join jt in context.InvGiftVoucherBookCodes.Where(
                                                 "" +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " >= " + "@0 AND " +
                                                 reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                 " <= @1",
                                                 (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                 (reportConditionsDataStruct.ConditionTo.Trim())
                                                 ) on qr.InvGiftVoucherBookCodeID equals jt.InvGiftVoucherBookCodeID
                                             select qr
                                            );
                                    break;
                                case "LocationID":
                                    if (reportConditionsDataStruct.ReportDataStruct.ReportFieldName == "Issued Location")
                                    {
                                        query = (from qr in query
                                                 join jt in context.Locations.Where(
                                                     "" +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " >= " + "@0 AND " +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " <= @1",
                                                     (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                     (reportConditionsDataStruct.ConditionTo.Trim())
                                                     ) on qr.SoldLocationID equals jt.LocationID
                                                 select qr
                                           );
                                    }
                                    else if (reportConditionsDataStruct.ReportDataStruct.ReportFieldName == "Redeemed Location")
                                    {
                                        query = (from qr in query
                                                 join jt in context.Locations.Where(
                                                     "" +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " >= " + "@0 AND " +
                                                     reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                                     " <= @1",
                                                     (reportConditionsDataStruct.ConditionFrom.Trim()),
                                                     (reportConditionsDataStruct.ConditionTo.Trim())
                                                     ) on qr.RedeemedLocationID equals jt.LocationID
                                                 select qr
                                          );
                                    }
                                    else if (reportConditionsDataStruct.ReportDataStruct.ReportFieldName == "Transfered Location")
                                    {
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
                                    }
                                    break;
                                default:
                                    query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                                    break;
                            }
                        }
                        else
                        {
                            query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); 
                        }
                    }

                    if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }
                }

                #region dynamic select
                ////// Set fields to be selected
                //foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
                //{ selectFields.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", "); }

                // Remove last two charators (", ")
                //selectFields.Remove(selectFields.Length - 2, 2);
                //var selectedqry = query.Select("new(" + selectFields.ToString() + ")");
                #endregion

                //int aa = query.ToDataTable().Rows.Count;
                //DataTable nn2 = query.ToDataTable();

                var queryResult = (from h in query.AsNoTracking()
                                   join li in context.Locations on new { SoldLocationID = h.SoldLocationID } equals new { SoldLocationID = li.LocationID } into li_join
                                   from li in li_join.DefaultIfEmpty()
                                   join cs in context.Employees on new { SoldCashierID = h.SoldCashierID } equals new { SoldCashierID = cs.EmployeeID } into cs_join
                                   from cs in cs_join.DefaultIfEmpty()
                                   join lr in context.Locations on new { RedeemedLocationID = h.RedeemedLocationID } equals new { RedeemedLocationID = lr.LocationID } into lr_join
                                   from lr in lr_join.DefaultIfEmpty()
                                   join cr in context.Employees on new { RedeemedCashierID = h.RedeemedCashierID } equals new { RedeemedCashierID = cr.EmployeeID } into cr_join
                                   from cr in cr_join.DefaultIfEmpty()
                                   //join lt in context.Locations on new { TransferedLocationID = h.ToLocationID, VoucherTransferStatus = h.VoucherStatus } equals new { TransferedLocationID = lt.LocationID, VoucherTransferStatus = 3 } into lt_join
                                   join lt in context.Locations on new { TransferedLocationID = h.ToLocationID } equals new { TransferedLocationID = lt.LocationID } into lt_join
                                   from lt in lt_join.DefaultIfEmpty()

                                   select
                                       new
                                       {
                                           FieldString1 = h.VoucherSerial,
                                           FieldString2 = h.InvGiftVoucherBookCode.BookName, //h.InvGiftVoucherBookCode.BookCode, //gb.BookCode,
                                           //FieldString3 = h.InvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName, //gb.BookName,
                                           //FieldString4 = //h.InvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupCode, //gg.GiftVoucherGroupCode,
                                           //FieldString5 =  //gg.GiftVoucherGroupName,
                                           //FieldString3 = (h.VoucherStatus >= 4 ? DbFunctions.TruncateTime(h.SoldDate) : null),
                                           
                                           FieldString3 = (h.VoucherStatus >= 4 ? (DbFunctions.TruncateTime(h.SoldDate)) : null),

                                           FieldString4 = (h.SoldUnitID != 0 ? SqlFunctions.StringConvert((double)h.SoldUnitID) : ""),
                                           FieldString5 = h.SoldReceiptNo,
                                           FieldString6 = cs.EmployeeName,
                                           FieldString7 = li.LocationPrefixCode,
                                           FieldString8 = (h.VoucherStatus >= 6 ? (DbFunctions.TruncateTime(h.RedeemedDate)) : null),
                                           FieldString9 = (h.RedeemedUnitID != 0 ? SqlFunctions.StringConvert((double)h.RedeemedUnitID) : ""),
                                           FieldString10 = h.RedeemedReceiptNo,
                                           FieldString11 = cr.EmployeeName,
                                           FieldString12 = lr.LocationPrefixCode,
                                           FieldString13 = h.InvGiftVoucherBookCode.InvGiftVoucherGroup.GiftVoucherGroupName,
                                           FieldString14 = (h.VoucherType == 1 ? "Voucher" : "Coupon"),
                                           FieldString15 = (h.VoucherStatus == 1 ? "Request" : (h.VoucherStatus == 2 ? "Received" : (h.VoucherStatus == 3 ? "Transfered" : (h.VoucherStatus == 4 ? "Issued" : (h.VoucherStatus == 5 ? "Lost / Damage" : (h.VoucherStatus == 6 ? "Redeemed" : "")))))),
                                           FieldString16 =  (h.VoucherStatus == 1 ? "" : (h.VoucherStatus == 2 ? "" : (h.VoucherStatus == 3 ?  lt.LocationPrefixCode : (h.VoucherStatus == 4 ?  lt.LocationPrefixCode : (h.VoucherStatus == 5 ?  lt.LocationPrefixCode : (h.VoucherStatus == 6 ?  lt.LocationPrefixCode : "")))))),
                                           FieldDecimal1 = 1,
                                           FieldDecimal2 = (h.VoucherType == 1 ? h.GiftVoucherValue : h.GiftVoucherPercentage),
                                           //FieldDecimal2 = "", //h.GiftVoucherPercentage,
                                           //FieldDecimal3 = "",
                                           //FieldDecimal4 = "",
                                           //FieldDecimal5 = "",
                                       }).Select(a=>a.FieldString3.ToString()); //.ToArray();

                DataTable nn = queryResult.ToDataTable();
                //int val = queryResult.ToDataTable().Rows.Count;
                ////.Select(o => new {scheduleDate = <span class="skimlinks-unlinked">o.scheduleDate.Value.ToShortDateString</span>()})
                
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

                return dtQueryResult; // queryResult.ToDataTable();
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

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.InvGiftVoucherMasters
                                          .Where("IsDelete == @0", false);
                                         // .Select("new(" + reportDataStruct.DbColumnName.Trim() + ")" );

            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "LgsSupplierID":
                    qryResult = qryResult.Join(context.LgsSuppliers, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.SupplierCode)").
                                OrderBy("SupplierCode").Select("SupplierCode");
                    break;

                case "LocationID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "LocationName")
                    {
                        if (reportDataStruct.ReportFieldName == "Issued Location")
                        {
                            qryResult = qryResult.Join(context.Locations, "SoldLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                                .GroupBy("new(LocationName)", "new(LocationName)")
                                  .OrderBy("Key.LocationName").Select("Key.LocationName");
                        }
                        else if (reportDataStruct.ReportFieldName == "Redeemed Location")
                        {
                            qryResult = qryResult.Join(context.Locations, "RedeemedLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                                .GroupBy("new(LocationName)", "new(LocationName)")
                                  .OrderBy("Key.LocationName").Select("Key.LocationName");
                        }
                        else if (reportDataStruct.ReportFieldName == "Transfered Location")
                        {
                            qryResult = qryResult.Join(context.Locations, "ToLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationName)")
                             .GroupBy("new(LocationName)", "new(LocationName)")
                               .OrderBy("Key.LocationName").Select("Key.LocationName");
                        }
                    }
                    else
                    {
                        if (reportDataStruct.DbJoinColumnName.Trim() == "LocationCode")
                        {
                            if (reportDataStruct.ReportFieldName == "Issued Location")
                            {
                                qryResult = qryResult.Join(context.Locations, "SoldLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)")
                                  .GroupBy("new(LocationCode)", "new(LocationCode)")
                                    .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                            }
                            else if (reportDataStruct.ReportFieldName == "Redeemed Location")
                            {
                                qryResult = qryResult.Join(context.Locations, "RedeemedLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)")
                                 .GroupBy("new(LocationCode)", "new(LocationCode)")
                                   .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                            }
                            else if (reportDataStruct.ReportFieldName == "Transfered Location")
                            {
                                qryResult = qryResult.Join(context.Locations, "ToLocationID", reportDataStruct.DbColumnName.Trim(), "new(inner.LocationCode)")
                                 .GroupBy("new(LocationCode)", "new(LocationCode)")
                                   .OrderBy("Key.LocationCode").Select("Key.LocationCode");
                            }
                        }
                    }
                    break;
                case "EmployeeID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "EmployeeName")
                    {
                        if (reportDataStruct.ReportFieldName == "Issued Cashier")
                        {
                            qryResult = qryResult.Join(context.Employees, "SoldCashierID",
                                                   reportDataStruct.DbColumnName.Trim(), "new(inner.EmployeeName)")
                                             .GroupBy("new(EmployeeName)", "new(EmployeeName)")
                                             .OrderBy("Key.EmployeeName").Select("Key.EmployeeName");
                        }
                        else if (reportDataStruct.ReportFieldName == "Redeemed Cashier")
                        {
                            qryResult = qryResult.Join(context.Employees, "RedeemedCashierID",
                                                   reportDataStruct.DbColumnName.Trim(), "new(inner.EmployeeName)")
                                             .GroupBy("new(EmployeeName)", "new(EmployeeName)")
                                             .OrderBy("Key.EmployeeName").Select("Key.EmployeeName");
                        }
                    }
                    else
                    {
                        if (reportDataStruct.DbJoinColumnName.Trim() == "EmployeeCode")
                        {
                            if (reportDataStruct.ReportFieldName == "Issued Cashier")
                            {
                                qryResult = qryResult.Join(context.Employees, "SoldCashierID",
                                                       reportDataStruct.DbColumnName.Trim(), "new(inner.EmployeeCode)")
                                                 .GroupBy("new(EmployeeCode)", "new(EmployeeCode)")
                                                 .OrderBy("Key.EmployeeCode").Select("Key.EmployeeCode");
                            }
                            else if (reportDataStruct.ReportFieldName == "Redeemed Cashier")
                            {
                                qryResult = qryResult.Join(context.Employees, "RedeemedCashierID",
                                                       reportDataStruct.DbColumnName.Trim(), "new(inner.EmployeeCode)")
                                                 .GroupBy("new(EmployeeCode)", "new(EmployeeCode)")
                                                 .OrderBy("Key.EmployeeCode").Select("Key.EmployeeCode");
                            }
                        }
                    }
                    break;
                case "InvGiftVoucherGroupID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "GiftVoucherGroupCode")
                    {
                        qryResult = qryResult.Join(context.InvGiftVoucherGroups, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.GiftVoucherGroupCode)")
                                           .GroupBy("new(GiftVoucherGroupCode)", "new(GiftVoucherGroupCode)")
                                           .OrderBy("Key.GiftVoucherGroupCode")
                                           .Select("Key.GiftVoucherGroupCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "GiftVoucherGroupName")
                    {
                        qryResult = qryResult.Join(context.InvGiftVoucherGroups, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.GiftVoucherGroupName)")
                                           .GroupBy("new(GiftVoucherGroupName)", "new(GiftVoucherGroupName)")
                                           .OrderBy("Key.GiftVoucherGroupName")
                                           .Select("Key.GiftVoucherGroupName");
                    }
                    break;
                case "InvGiftVoucherBookCodeID":
                    if (reportDataStruct.DbJoinColumnName.Trim() == "BookCode")
                    {
                        qryResult = qryResult.Join(context.InvGiftVoucherBookCodes, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.BookCode)")
                               .GroupBy("new(BookCode)", "new(BookCode)")
                                           .OrderBy("Key.BookCode")
                                           .Select("Key.BookCode");
                    }
                    else if (reportDataStruct.DbJoinColumnName.Trim() == "BookName")
                    {
                        qryResult = qryResult.Join(context.InvGiftVoucherBookCodes, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.BookName)")
                               .GroupBy("new(BookName)", "new(BookName)")
                                           .OrderBy("Key.BookName")
                                           .Select("Key.BookName");
                    }
                    break;
                case "VoucherType":
                case "VoucherStatus":
                case "SoldUnitID":
                case "RedeemedUnitID":
                case "SoldReceiptNo":
                case "RedeemedReceiptNo":
                    qryResult = qryResult.GroupBy("new(" + reportDataStruct.DbColumnName + ")", "new(" + reportDataStruct.DbColumnName + ")")
                                         .OrderBy("key." + reportDataStruct.DbColumnName + "")
                                         .Select("key." + reportDataStruct.DbColumnName + "");
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
            else if (reportDataStruct.ValueDataType.Equals(typeof(int)))
            {
                if (reportDataStruct.DbColumnName.Trim() == "VoucherType")
                {
                    foreach (var item in qryResult)
                    {
                        if (item.Equals((int)VouhcherTypes.Coupon))
                        { selectionDataList.Add(VouhcherTypes.Coupon.ToString()); }
                        else if (item.Equals((int)VouhcherTypes.Voucher))
                        { selectionDataList.Add(VouhcherTypes.Voucher.ToString()); }
                    }
                }
                else if (reportDataStruct.DbColumnName.Trim() == "VoucherStatus")
                {
                    foreach (var item in qryResult)
                    {
                        if (item.Equals((int)VoucherStatus.Request))
                        { selectionDataList.Add(VoucherStatus.Request.ToString()); }
                        else if (item.Equals((int)VoucherStatus.Received))
                        { selectionDataList.Add(VoucherStatus.Received.ToString()); }
                        else if (item.Equals((int)VoucherStatus.Transfered))
                        { selectionDataList.Add(VoucherStatus.Transfered.ToString()); }
                        else if (item.Equals((int)VoucherStatus.Issued))
                        { selectionDataList.Add(VoucherStatus.Issued.ToString()); }
                        else if (item.Equals((int)VoucherStatus.LostOrDamage))
                        { selectionDataList.Add(VoucherStatus.LostOrDamage.ToString()); }
                        else if (item.Equals((int)VoucherStatus.Redeemed))
                        { selectionDataList.Add(VoucherStatus.Redeemed.ToString()); }
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
            }
            else if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
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
        //public string GetNewCode(string preFix, int suffix, int codeLength)
        //{
        //    string GetNewCode = string.Empty;
        //    GetNewCode = context.InvGiftVoucherMasters.Max(a => a.BookCode);
        //    if (GetNewCode != null)
        //    {
        //        GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
        //    }
        //    else
        //    {
        //        GetNewCode = "0";
        //    }
        //    GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
        //    GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
        //    return GetNewCode;
        //}
        #endregion
        #endregion
    }
}
