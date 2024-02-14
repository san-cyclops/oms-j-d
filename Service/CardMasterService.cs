using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data.Entity;
using Utility;

namespace Service
{
    public class CardMasterService
    {
        ERPDbContext context = new ERPDbContext();


        #region public methods

        public void AddCardMasters(CardMaster cardMaster)
        {
            context.CardMasters.Add(cardMaster);
            context.SaveChanges();
        }

        public void UpdateCardMasters(CardMaster cardMaster)
        {
            cardMaster.ModifiedUser = Common.LoggedUser;
            cardMaster.ModifiedDate = Common.GetSystemDateWithTime(); 
            this.context.Entry(cardMaster).State = EntityState.Modified;
            this.context.SaveChanges();
        }


        public CardMaster GetCardMasterByCode(string Code)
        {
            return context.CardMasters.Where(s => s.CardCode == Code && s.IsDelete.Equals(false)).FirstOrDefault();
        }


        public CardMaster GetCardMasterByName(string Name)
        {
            return context.CardMasters.Where(s => s.CardName == Name && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        public CardMaster GetCardMasterById(long CardMasterID)
        {
            return context.CardMasters.Where(s => s.CardMasterID == CardMasterID && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<CardMaster> GetAllCardMasters()
        {
            return context.CardMasters.Where(s => s.IsDelete == false).ToList();
        }

        public string[] GetAllCardNames()
        {
            List<string> CardList = context.CardMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.CardName).ToList();
            return CardList.ToArray();

        }
        public string[] GetAllCardCodes()
        {
            List<string> CardList = context.CardMasters.Where(c => c.IsDelete.Equals(false)).Select(c => c.CardCode).ToList();
            return CardList.ToArray();

        }

        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.CardMasters.Max(c => c.CardCode);
            if (getNewCode != null)
                getNewCode = getNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            else
                getNewCode = "0";

            getNewCode = (int.Parse(getNewCode) + 1).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode;
        }
        #endregion
    }
}
