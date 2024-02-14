﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain
{
    public class InvPosConfiguration
    {
        public long InvPosConfigurationID { get; set; }
        public int LocationID { get; set; }
        //public string LocationCode { get; set; }
        //public string LocationName { get; set; }
        //public long Zno { get; set; }
        //public string LogPath { get; set; }
        //public string JnlPath { get; set; }
        //public long Receipt { get; set; }
        //public long Suspend { get; set; }
                    public long DocNo2 { get; set; }
                    public long DocNo3 { get; set; }
                    public long DocNo4 { get; set; }
                    public long DocNo5 { get; set; }
                    //public long Tog { get; set; }
                    //public long Dmg { get; set; }
                    //public long Ret { get; set; }
                    //public long Van { get; set; }
                    //public long CrNote { get; set; }
                    //public long Invoice { get; set; }
                    //public int UnitNo { get; set; }
                    //public decimal StDisc1 { get; set; }
                    //public decimal StDisc2 { get; set; }
                    //public decimal LoyaltyScale { get; set; }
                    //public decimal GoldCardScale { get; set; }
                    //public decimal StpScale { get; set; }
                    //public string Head1 { get; set; }
                    //public string Head2 { get; set; }
                    //public string Head3 { get; set; }
                    //public string Head4 { get; set; }
                    //public string Head5 { get; set; }
                    //public string Head6 { get; set; }
                    //public string Head7 { get; set; }
                    //public string Head8 { get; set; }
                    //public string Head9 { get; set; }
                    //public string Head10 { get; set; }
                    //public string Tail1 { get; set; }
                    //public string Tail2 { get; set; }
                    //public string Tail3 { get; set; }
                    //public string Tail4 { get; set; }
                    //public string Tail5 { get; set; }
                    //public string Tail6 { get; set; }
                    //public string Tail7 { get; set; }
                    //public string Tail8 { get; set; }
                    //public string Tail9 { get; set; }
                    //public string Tail10 { get; set; }
                    //public int lpc { get; set; }
                    //public int lpc2 { get; set; }
                    //public bool DispDate { get; set; }
                    //public bool DispTime { get; set; }
                    //public string PPort { get; set; }
                    //public string DPort { get; set; }
                    //public int BRate { get; set; }
                    //public bool AutoCut { get; set; }
                    //public decimal OpMsgValue { get; set; }
                    //public string OPMsg { get; set; }
                    //public string OpMsg2 { get; set; }
                    //public string DisplayMsg { get; set; }
                    //public int DisplayTime { get; set; }
                    //public bool Display { get; set; }
                    //public bool Printer { get; set; }
                    //public bool CashDrawer { get; set; }
                    //public decimal RelId { get; set; }
                    //public string Localhost { get; set; }
                    //public string Exchange { get; set; }
                    //public string Outlet { get; set; }
                    //public string Loyalty { get; set; }
                    //public bool NetWork { get; set; }
                    //public int Sleep { get; set; }
                    //public int InitSpace { get; set; }
                    //public bool ZnoOnRpt { get; set; }
                    //public bool ZOnJnl { get; set; }
                    //public int LFAftePrint { get; set; }
                    //public int LFAfteAutoCut { get; set; }
                    //public bool MultipleExch { get; set; }
                    //public bool KeyBoard { get; set; }
                    //public bool BcodeOnAdv { get; set; }
                    //public bool BcodeOnSus { get; set; }
                    //public bool PrintItemsOnSus { get; set; }
                    //public bool PrintItemsOnCnl { get; set; }
                    //public bool PrintOffLine { get; set; }
                    //public bool StockOnLIne { get; set; }
                    //public bool ReceiptInit { get; set; }
                    //public bool SwpCcd { get; set; }
                    //public bool UseCardBank { get; set; }
                    //public decimal RAcc { get; set; }
                    //public int LoyaltyLen { get; set; }
                    //public string LoyaltyPreFix { get; set; }
                    //public int VoucherLen { get; set; }
                    //public string VoucherPreFix { get; set; }
                    //public bool IsVoucherSaleWithItem { get; set; }
                    //public bool LogoPrint { get; set; }
                    //public bool ExisSys { get; set; }
                    //public bool AllowMinus { get; set; }
                    //public bool AutoBackup { get; set; }
                    //public int BackupTime { get; set; }
                    //public string QtyFmt { get; set; }
                    //public bool AutoSOff { get; set; }
                    //public int BackGround { get; set; }
                    //public bool ValidateCCD { get; set; }
                    //public bool ValidateVou { get; set; }
                    //public bool DispDiscount { get; set; }
                    //public bool PrintLPT { get; set; }
                    //public string reppath { get; set; }
                    //public bool IsTouchVersion { get; set; }
                    //public DateTime Rdate { get; set; }
                    //public bool ExchangeCounter { get; set; }
                    //public bool LogStatus { get; set; }
                    //public bool CRCashDrawer { get; set; }
                    //public bool USBDisplay { get; set; }
                    //public bool IsKot { get; set; }
                    //public string KotPrinterPort { get; set; }
                    //public decimal KotInvoice { get; set; }
                    //public string KHead1 { get; set; }
                    //public string KHead2 { get; set; }
                    //public string KHead3 { get; set; }
                    //public string KHead4 { get; set; }
                    //public string KHead5 { get; set; }
                    //public decimal ExLoyalAmount { get; set; }
                    //public decimal ExLoyalPoints { get; set; }
                    //public decimal ExMinLoyalAmount { get; set; }
                    //public bool SelectPrinter { get; set; }
                    //public decimal BronzeScale { get; set; }
                    //public bool SuspendPrint { get; set; }
                    //public bool SharedKotInvoice { get; set; }
                    //public bool SelectLoyaltyType { get; set; }
                    //public bool VOUonCashSale { get; set; }
                    //public bool SmallChar { get; set; }
                    //public bool Tax { get; set; }
                    //public bool IsPromotion { get; set; }
                    //public long Xno { get; set; }
                    //public string VidPath { get; set; }
                    //public decimal Type { get; set; }
                    //public string DateFormat { get; set; }
                    //public int KeyboardLayer { get; set; }
                    //public int CashierPwdMaxLen { get; set; }
                    //public bool BoldSubTotal { get; set; }

    }
}