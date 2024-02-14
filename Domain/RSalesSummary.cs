using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class RSalesSummary : BaseEntity
    {
        public long RSalesSummaryID { get; set; }

        [DefaultValue(0)]
        public long LocationID { get; set; }

        [DefaultValue(0)]
        public int UnitNo { get; set; }

        [DefaultValue(0)]
        public long CashierID { get; set; }

        public decimal vouchersalegross { get; set; }

        [DefaultValue(0)]
        public long nvouchersale { get; set; }

        public decimal voudiscount { get; set; }

        [DefaultValue(0)]
        public long nvoudiscount { get; set; }

        public decimal vouRedeem { get; set; }

        [DefaultValue(0)]
        public long nvouRedeem { get; set; }

        public decimal loyaltyRedeem { get; set; }

        [DefaultValue(0)]
        public long nloyaltyRedeem { get; set; }

        public decimal vouchernet { get; set; }

        public decimal vouchercash { get; set; }

        public decimal vouchercredit { get; set; }

        public decimal grosssale { get; set; }

        public decimal refunds { get; set; }

        [DefaultValue(0)]
        public long nrefunds { get; set; }

        public decimal Idiscount { get; set; }

        [DefaultValue(0)]
        public long nidiscount { get; set; }

        public decimal sdiscount { get; set; }

        [DefaultValue(0)]
        public long nsdiscount { get; set; }

        public decimal voids { get; set; }

        [DefaultValue(0)]
        public long nvoids { get; set; }

        public decimal error { get; set; }

        [DefaultValue(0)]
        public long nerror { get; set; }

        public decimal cancel { get; set; }

        [DefaultValue(0)]
        public long ncancel { get; set; }

        public decimal loyalty { get; set; }

        [DefaultValue(0)]
        public long nloyalty { get; set; }

        public decimal nett { get; set; }

        public decimal credpay { get; set; }

        [DefaultValue(0)]
        public long ncredpay { get; set; }

        public decimal paidout { get; set; }

        [DefaultValue(0)]
        public long npaidout { get; set; }

        public decimal cashsale { get; set; }

        public decimal staffcashsale { get; set; }

        [DefaultValue(0)]
        public long nstaffcashsale { get; set; }

        public decimal cashrefund { get; set; }

        [DefaultValue(0)]
        public long ncashrefund { get; set; }

        public decimal advancereceive { get; set; }

        [DefaultValue(0)]
        public long nadvancereceive { get; set; }

        public decimal advancerefund { get; set; }

        [DefaultValue(0)]
        public long nadvancerefund { get; set; }

        public decimal advancereceivecrd { get; set; }

        [DefaultValue(0)]
        public long nadvancereceivecrd { get; set; }

        public decimal advancerefundcrd { get; set; }

        [DefaultValue(0)]
        public long nadvancerefundcrd { get; set; }

        public decimal advancesettlement { get; set; }

        [DefaultValue(0)]
        public long nadvancesettlement { get; set; }

        [DefaultValue(0)]
        public long noofbills { get; set; }

        public decimal voucher { get; set; }

        [DefaultValue(0)]
        public long nvoucher { get; set; }

        public decimal staffcred { get; set; }

        [DefaultValue(0)]
        public long nstaffcred { get; set; }

        public decimal cheque { get; set; }

        [DefaultValue(0)]
        public long ncheque { get; set; }

        public decimal starpoint { get; set; }

        public decimal starpointErn { get; set; }

        public decimal starpointErnVal { get; set; }

        [DefaultValue(0)]
        public long nstarpoint { get; set; }

        public decimal mcredit { get; set; }

        [DefaultValue(0)]
        public long nmcredit { get; set; }

        public decimal credit { get; set; }

        [DefaultValue(0)]
        public long ncredit { get; set; }

        public decimal crdnote { get; set; }

        [DefaultValue(0)]
        public long ncrdnote { get; set; }

        public decimal crdnotestle { get; set; }

        [DefaultValue(0)]
        public long ncrdnotestle { get; set; }

        public decimal CashInHand { get; set; }

        public decimal DeclaredAmount { get; set; }

        public decimal ReceivedOnAccount { get; set; }

        [MaxLength(25)]
        public string ReportNo { get; set; }

        public decimal Vstaffcashsale { get; set; }

        public decimal VstaffCreditSale { get; set; }

        public decimal MasterCard { get; set; }

        public decimal VisaCard { get; set; }

        public decimal AmexCard { get; set; }

        public decimal DebitCard { get; set; }

         [DefaultValue(0)]
        public long NMasterCard { get; set; }

         [DefaultValue(0)]
         public long NVisaCard { get; set; }

         [DefaultValue(0)]
         public long NAmexCard { get; set; }

         [DefaultValue(0)]
         public long NDebitCard { get; set; }

        [DefaultValue(0)]
        public long LogedUser { get; set; }

        [MaxLength(10)]
        public string LocationCode { get; set; }

        [MaxLength(50)]
        public string LocationName { get; set; }

        public decimal MCash { get; set; }

        public decimal eZCash { get; set; }

        public decimal SMSVoucher { get; set; }

 

		 
    }
}

  