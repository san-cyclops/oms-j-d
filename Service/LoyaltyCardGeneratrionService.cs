using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Data;
using Domain;
using Utility;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Transactions;

namespace Service
{
    public class LoyaltyCardGeneratrionService
    {
        ERPDbContext context = new ERPDbContext();


        public void UpdateCardGeneratrion(LoyaltyCardGenerationHeader loyaltyCardGenerationHeader)
        {
            loyaltyCardGenerationHeader.ModifiedUser = Common.LoggedUser;
            loyaltyCardGenerationHeader.ModifiedDate = Common.GetSystemDateWithTime();
            context.Entry(loyaltyCardGenerationHeader).State = EntityState.Modified;
            context.SaveChanges();
        }


        public bool Save(LoyaltyCardGenerationHeader loyaltyCardGenerationHeader, List<LoyaltyCardGenerationDetailTemp> loyaltyCardGenerationDetailTemps)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (loyaltyCardGenerationHeader.LoyaltyCardGenerationHeaderID.Equals(0))
                {
                    context.LoyaltyCardGenerationHeaders.Add(loyaltyCardGenerationHeader);
                    context.SaveChanges();
                    loyaltyCardGenerationHeader.CardGenerationHeaderID = loyaltyCardGenerationHeader.LoyaltyCardGenerationHeaderID;
                    context.Entry(loyaltyCardGenerationHeader).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.Entry(loyaltyCardGenerationHeader).State = EntityState.Modified;
                }

                context.SaveChanges();


                var headerId = loyaltyCardGenerationHeader.LoyaltyCardGenerationHeaderID;

                foreach (LoyaltyCardGenerationDetailTemp loyaltyCardGenerationDetailTemp in loyaltyCardGenerationDetailTemps)
                {
                    LoyaltyCardGenerationDetail loyaltyCardGenerationDetailSave = new LoyaltyCardGenerationDetail();

                    loyaltyCardGenerationDetailSave.SerialNo = loyaltyCardGenerationDetailTemp.SerialNo;
                    loyaltyCardGenerationDetailSave.LoyaltyCardGenerationHeaderID =
                    loyaltyCardGenerationHeader.LoyaltyCardGenerationHeaderID;
                    loyaltyCardGenerationDetailSave.CardPrefix = loyaltyCardGenerationHeader.CardPrefix;
                    loyaltyCardGenerationDetailSave.CardLength = loyaltyCardGenerationHeader.CardLength;
                    loyaltyCardGenerationDetailSave.CardStartingNo = loyaltyCardGenerationDetailTemp.CardStartingNo;
                    loyaltyCardGenerationDetailSave.SerialPrefix = loyaltyCardGenerationHeader.SerialPrefix;
                    loyaltyCardGenerationDetailSave.SerialLength = loyaltyCardGenerationHeader.SerialLength;
                    loyaltyCardGenerationDetailSave.SerialStartingNo = loyaltyCardGenerationDetailTemp.SerialStartingNo;
                    loyaltyCardGenerationDetailSave.EncodePrefix = loyaltyCardGenerationHeader.EncodePrefix;
                    loyaltyCardGenerationDetailSave.EncodeLength = loyaltyCardGenerationHeader.EncodeLength;
                    loyaltyCardGenerationDetailSave.EncodeStartingNo = loyaltyCardGenerationDetailTemp.EncodeStartingNo;
                    loyaltyCardGenerationDetailSave.IsActive = true;

                    loyaltyCardGenerationDetailSave.CardNo = loyaltyCardGenerationDetailTemp.CardNo;
                    loyaltyCardGenerationDetailSave.SerialNo = loyaltyCardGenerationDetailTemp.SerialNo;
                    loyaltyCardGenerationDetailSave.EncodeNo = loyaltyCardGenerationDetailTemp.EncodeNo;

                    //maxCardNo = Convert.ToInt16(loyaltyCardGenerationDetailTemp.CardNo.Substring(loyaltyCardGenerationHeader.CardPrefix.Length, 
                    //    loyaltyCardGenerationHeader.CardLength));


                    loyaltyCardGenerationDetailSave.GeneratedDate = loyaltyCardGenerationHeader.GeneratedDate;

                    context.LoyaltyCardGenerationDetails.Add(loyaltyCardGenerationDetailSave);
                    context.SaveChanges();
                    loyaltyCardGenerationDetailSave.CardGenerationDetailID = loyaltyCardGenerationDetailSave.LoyaltyCardGenerationDetailID;
                    context.Entry(loyaltyCardGenerationDetailSave).State = EntityState.Modified;
                    context.SaveChanges();

                }

                var maxCardNo = context.LoyaltyCardGenerationDetails.Where(l => l.LoyaltyCardGenerationHeaderID.Equals(headerId)).Max(l => l.CardStartingNo);
                var maxSerialNo = context.LoyaltyCardGenerationDetails.Where(l => l.LoyaltyCardGenerationHeaderID.Equals(headerId)).Max(l => l.SerialStartingNo);
                var maxEncodeNo = context.LoyaltyCardGenerationDetails.Where(l => l.LoyaltyCardGenerationHeaderID.Equals(headerId)).Max(l => l.EncodeStartingNo);




                CardGenerationSetting cardGenerationSetting = new CardGenerationSetting();
                cardGenerationSetting = GetCardGenerationSetting();
                if (cardGenerationSetting != null)
                    cardGenerationSetting.CardStartingNo = maxCardNo + 1;
                cardGenerationSetting.SerialStartingNo = maxSerialNo + 1;
                cardGenerationSetting.EncodeStartingNo = maxEncodeNo + 1;
                context.Entry(cardGenerationSetting).State = EntityState.Modified;
                context.SaveChanges();

                transaction.Complete();
                return true;
            }
        }

        

        public string[] GetAllSerialNos()
        {
            List<string> SerialNoList = context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.SerialNo).ToList();
            return SerialNoList.ToArray();
        }

        public CardGenerationSetting GetCardGenerationSetting()
        {
            return context.CardGenerationSettings.Where(c=>c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public string[] GetAllCardNos()
        {
            List<string> CardNoList = context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true)).Select(c => c.CardNo).ToList();
            return CardNoList.ToArray();
        }

        public string[] GetIssuedCardNos()
        {
            return context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsIssued.Equals(true)  && c.IsActive.Equals(true)).Select(c => c.CardNo).ToList().ToArray();
        }

        public string[] GetIssuedNotAssignedCardNos()
        {
            return (from a in context.LoyaltyCardGenerationDetails
                     where !(from b in context.LoyaltyCustomers
                             where b.IsDelete.Equals(false)
                             select b.CardNo).Contains(a.CardNo) && a.IsDelete.Equals(false) && a.IsActive.Equals(true) && a.IsIssued.Equals(true) 
                     select a.CardNo).ToList().ToArray();
        }
                
        public LoyaltyCardGenerationDetail GetLoyaltyCardGenerationDetailByCardNo(string cardno)
        {
            return context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true) &&  c.CardNo == cardno).FirstOrDefault();
        }

        public LoyaltyCardGenerationDetail GetIssuedLoyaltyCardGenerationDetailByCardNo(string cardno)
        {
            return context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsIssued.Equals(true) && c.IsActive.Equals(true) && c.CardNo == cardno).FirstOrDefault();
        }

        public LoyaltyCardGenerationDetail GetIssuedNotAssignedLoyaltyCardGenerationDetailByCardNo(string cardno)
        {
            return (from a in context.LoyaltyCardGenerationDetails
                     where !(from b in context.LoyaltyCustomers
                             where b.IsDelete.Equals(false)
                             select b.CardNo).Contains(a.CardNo) && a.CardNo.Equals(cardno) && a.IsDelete.Equals(false) && a.IsActive.Equals(true) && a.IsIssued.Equals(true) 
                     select a).FirstOrDefault();            
        }

        public LoyaltyCardGenerationDetail GetUnIssuedLoyaltyCardGenerationDetailByCardNo(string cardno)
        {
            return context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true) && c.IsIssued.Equals(false) && c.CardNo == cardno).FirstOrDefault();
        }

        public LoyaltyCardGenerationDetail GetLoyaltyCardGenerationDetailBySerialNo(string serialNo)
        {
            return context.LoyaltyCardGenerationDetails.Where(c => c.IsDelete.Equals(false) && c.IsActive.Equals(true) && c.SerialNo == serialNo).FirstOrDefault();
        }

        public List<LoyaltyCardGenerationDetail> GetPendigLoyaltyCardList(string cardFrom, string cardTo)
        {
            if (string.IsNullOrEmpty(cardFrom.Trim()) && string.IsNullOrEmpty(cardTo.Trim()))
            {
                return context.LoyaltyCardGenerationDetails.Where(cd => cd.IsIssued.Equals(false) && cd.IsActive.Equals(true) && cd.IsDelete.Equals(false)).ToList();
            }
            else
            {
                return context.LoyaltyCardGenerationDetails.Where(cd => cd.CardNo.CompareTo(cardFrom) >= 0 && cd.CardNo.CompareTo(cardTo) <= 0 && cd.IsIssued.Equals(false) && cd.IsDelete.Equals(false) && cd.IsActive.Equals(true)).ToList();
            }
        }

        public string[] GetPendigLoyaltyCardNos()
        {
            return context.LoyaltyCardGenerationDetails.Where(cd => cd.IsIssued.Equals(false) && cd.IsDelete.Equals(false) && cd.IsActive.Equals(true)).Select(cd => cd.CardNo).ToArray();
        }
    }
}
