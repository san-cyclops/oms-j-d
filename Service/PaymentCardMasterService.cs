using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;

namespace Service
{
    public class PaymentCardMasterService
    {
        ERPDbContext context = new ERPDbContext();

        public List<InvPaymentCardType> GetAllCardMastersByProvider(long BankId)
        {
            return context.InvPaymentCardType.Where(s => s.BankID == BankId && s.IsDelete == false).ToList();
        }

        public List<InvPaymentCardType> GetAllCards()
        {
            return context.InvPaymentCardType.Where(s => s.IsDelete.Equals(false)).ToList();
        }
    }
}
