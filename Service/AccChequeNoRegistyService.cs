using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Service
{
    public class AccChequeNoRegistyService
    {
        #region Methods

        public string GetNextChequeNo(long bankBookID)
        {
            string NextChequeNo = string.Empty;
            ////using (ERPDbContext context = new ERPDbContext())
            ////{
            ////    NextChequeNo = context.AccChequeNoRegisties.Where(c => c.BankBookID == bankBookID).Max(int.Parse(c.CardNo));
            ////}
            return NextChequeNo;
        }
        #endregion
    }
}
