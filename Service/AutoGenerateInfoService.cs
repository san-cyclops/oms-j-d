using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;

namespace Service
{
    public static class AutoGenerateInfoService
    {
        static ERPDbContext context = new ERPDbContext();

       
        public static AutoGenerateInfo GetAutoGenerateInfoByForm(string formName)  
        {
            AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
            if (context.AutoGenerateInfos.Where(ai => ai.FormName == formName).FirstOrDefault() != null)
                autoGenerateInfo = context.AutoGenerateInfos.Where(ai => ai.FormName == formName).FirstOrDefault();

           
            if(string.IsNullOrEmpty(autoGenerateInfo.FormText))

                autoGenerateInfo.FormText = "Select Caption";
            return autoGenerateInfo;
        }

        public static List<AutoGenerateInfo> GetAllTransactions()
        {
            return context.AutoGenerateInfos.OrderBy(a => a.ModuleType).ToList();
        }

        public static List<AutoGenerateInfo> GetAllTransactionsByEntryStatus()
        {
            return context.AutoGenerateInfos.Where(a=>a.IsEntry.Equals(true) && a.IsActive.Equals(true)).OrderBy(a => a.ModuleType).ToList();
        }

        public static string[] GetTransactionFormTexts()

        {
            List<string> formTextsList = context.AutoGenerateInfos.Where(a => a.IsEntry.Equals(true) && a.IsActive.Equals(true)).OrderBy(a => a.FormText).Select(a => a.FormText).ToList();
            string[] formTexts = formTextsList.ToArray();
            return formTexts;
        }

        public static decimal GetExchangeRate()
        {
            return context.AutoGenerateInfos.Max(c => c.ExchangeRate);
        }

        //#region GetNewCode

        //public string GetNewCode(string preFix, int suffix, int codeLength,object tbl)
        //{
        //    string GetNewCode = string.Empty;
        //    GetNewCode = context.CustomerGroups.Max(c => c.CustomerGroupCode);
        //    if (GetNewCode != null)
        //        GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
        //    else
        //        GetNewCode = "0";

       

        //    GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
        //    GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
        //    return GetNewCode;
        //}

        //#endregion

    }
}
