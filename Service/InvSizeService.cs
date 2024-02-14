using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace Service
{
    public class InvSizeService
    {
        ERPDbContext context = new ERPDbContext();


        #region public methods
        /// <summary>
        /// Save InvSize
        /// </summary>
        /// <param name="invSize"></param>
        public void AddInvSizes(decimal exrate)
        {
            context.AutoGenerateInfos.Update(ps => new AutoGenerateInfo { ExchangeRate = exrate });

        }
        /// <summary>
        /// Update InvSize
        /// </summary>
        /// <param name="invSize"></param>
        public void UpdateInvSizes(InvSize invSize)
        {
            this.context.Entry(invSize).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Get Customer By Code
        /// </summary>
        /// <param name="sizeCode"></param>
        /// <returns></returns>
        public InvSize GetInvSizeByCode(string sizeCode)
        {
            return context.InvSizes.Where(s => s.SizeCode == sizeCode && s.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Get Inv Sizes
        /// </summary>
        /// <returns></returns>
        public List<InvSize> GetAllInvSizes()
        {
            return context.InvSizes.Where(s => s.IsDelete == false).ToList();
        }


        public string GetNewCode(string formName)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            string preFix = autoGenerateInfo.Prefix;
            int codeLength = autoGenerateInfo.CodeLength;

            getNewCode = context.Customers.Max(c => c.CustomerCode);
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
